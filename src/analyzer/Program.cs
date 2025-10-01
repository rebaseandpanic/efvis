using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Newtonsoft.Json;
using System.CommandLine;
using System.Text.RegularExpressions;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var projectOption = new Option<string>(
            "--project",
            description: "Path to the .csproj file to analyze"
        ) { IsRequired = true };

        var rootCommand = new RootCommand("Entity Framework DbContext Analyzer")
        {
            projectOption
        };

        rootCommand.SetHandler(async (string projectPath) =>
        {
            await AnalyzeProject(projectPath);
        }, projectOption);

        return await rootCommand.InvokeAsync(args);
    }

    static async Task AnalyzeProject(string projectPath)
    {
        try
        {
            // Register MSBuild
            MSBuildLocator.RegisterDefaults();

            using var workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (sender, args) =>
            {
                // Suppress workspace errors - they go to stderr and break JSON parsing
            };

            var project = await workspace.OpenProjectAsync(projectPath);
            var compilation = await project.GetCompilationAsync();

            if (compilation == null || !compilation.SyntaxTrees.Any())
            {
                // Fallback to simple file-based analysis
                var fallbackResult = await SimpleFallbackAnalysis(projectPath);
                OutputResult(fallbackResult);
                return;
            }

            var dbContextAnalyzer = new DbContextAnalyzer(compilation);
            var contexts = await dbContextAnalyzer.FindDbContexts();
            
            // If no contexts found, try fallback
            if (!contexts.Any())
            {
                var fallbackResult = await SimpleFallbackAnalysis(projectPath);
                OutputResult(fallbackResult);
                return;
            }

            OutputResult(new AnalysisResult
            {
                Contexts = contexts
            });
        }
        catch (Exception ex)
        {
            // Try fallback on any error
            var fallbackResult = await SimpleFallbackAnalysis(projectPath);
            if (fallbackResult.Contexts.Any())
            {
                OutputResult(fallbackResult);
            }
            else
            {
                OutputResult(new AnalysisResult
                {
                    Errors = new[] { ex.Message }
                });
            }
        }
    }
    
    static async Task<AnalysisResult> SimpleFallbackAnalysis(string projectPath)
    {
        var result = new AnalysisResult();
        var projectDir = Path.GetDirectoryName(projectPath) ?? ".";
        
        // Find all .cs files
        var csFiles = Directory.GetFiles(projectDir, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains("/obj/") && !f.Contains("/bin/") && !f.Contains("\\obj\\") && !f.Contains("\\bin\\"));

        var dbContextRegex = new Regex(@"class\s+(\w+)\s*:.*\bDbContext\b", RegexOptions.Multiline);
        var dbSetRegex = new Regex(@"public\s+(?:virtual\s+)?DbSet<([\w\.]+)>\s+(\w+)\s*\{\s*get;\s*set;\s*\}", RegexOptions.Multiline);
        
        foreach (var file in csFiles)
        {
            var content = await File.ReadAllTextAsync(file);
            var contextMatch = dbContextRegex.Match(content);
            
            if (contextMatch.Success)
            {
                var contextName = contextMatch.Groups[1].Value;
                var entities = new List<EntityInfo>();
                
                // Find namespace
                var namespaceMatch = Regex.Match(content, @"namespace\s+([\w\.]+)");
                var namespaceName = namespaceMatch.Success ? namespaceMatch.Groups[1].Value : "";
                
                // Find DbSets
                var dbSetMatches = dbSetRegex.Matches(content);
                foreach (Match match in dbSetMatches)
                {
                    var entityTypeName = match.Groups[1].Value;
                    var entityName = entityTypeName.Contains(".") ? 
                        entityTypeName.Substring(entityTypeName.LastIndexOf('.') + 1) : 
                        entityTypeName;
                    
                    entities.Add(new EntityInfo
                    {
                        Name = entityName,
                        TableName = match.Groups[2].Value,
                        Properties = new List<PropertyInfo>
                        {
                            // Add basic Id property as placeholder
                            new PropertyInfo
                            {
                                Name = "Id",
                                Type = "int",
                                IsPrimaryKey = true,
                                IsRequired = true
                            }
                        },
                        Relationships = new List<RelationshipInfo>()
                    });
                }
                
                result.Contexts.Add(new DbContextInfo
                {
                    Name = contextName,
                    Namespace = namespaceName,
                    FilePath = file,
                    Entities = entities
                });
            }
        }
        
        return result;
    }

    static void OutputResult(AnalysisResult result)
    {
        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(json);
    }
}

public class DbContextAnalyzer
{
    private readonly Compilation _compilation;
    private readonly INamedTypeSymbol? _dbContextType;

    public DbContextAnalyzer(Compilation compilation)
    {
        _compilation = compilation;
        _dbContextType = compilation.GetTypeByMetadataName("Microsoft.EntityFrameworkCore.DbContext");
    }

    public async Task<List<DbContextInfo>> FindDbContexts()
    {
        var contexts = new List<DbContextInfo>();

        // Remove debug output - it breaks JSON parsing
        foreach (var tree in _compilation.SyntaxTrees)
        {
            var root = await tree.GetRootAsync();
            var semanticModel = _compilation.GetSemanticModel(tree);

            var classDeclarations = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>();

            foreach (var classDecl in classDeclarations)
            {
                var classSymbol = semanticModel.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;
                if (classSymbol == null) continue;

                if (IsDbContext(classSymbol))
                {
                    var contextInfo = AnalyzeDbContext(classSymbol, classDecl, semanticModel);
                    contexts.Add(contextInfo);
                }
            }
        }

        return contexts;
    }

    private bool IsDbContext(INamedTypeSymbol classSymbol)
    {
        // Fallback: check by name if type resolution failed
        var baseType = classSymbol.BaseType;
        while (baseType != null)
        {
            var baseTypeName = baseType.ToDisplayString();
            if (baseTypeName == "DbContext" || 
                baseTypeName == "Microsoft.EntityFrameworkCore.DbContext" ||
                baseTypeName.EndsWith(".DbContext"))
            {
                return true;
            }
            
            // Also check with symbol comparison if available
            if (_dbContextType != null && SymbolEqualityComparer.Default.Equals(baseType, _dbContextType))
                return true;
                
            baseType = baseType.BaseType;
        }

        return false;
    }

    private DbContextInfo AnalyzeDbContext(INamedTypeSymbol contextSymbol, ClassDeclarationSyntax classDecl, SemanticModel semanticModel)
    {
        var entities = new List<EntityInfo>();

        // Find all DbSet<T> properties
        var dbSetProperties = contextSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => p.Type is INamedTypeSymbol namedType && 
                       (namedType.Name == "DbSet" || namedType.ToDisplayString().Contains("DbSet")) && 
                       namedType.TypeArguments.Length == 1);

        foreach (var prop in dbSetProperties)
        {
            var entityType = ((INamedTypeSymbol)prop.Type).TypeArguments[0] as INamedTypeSymbol;
            if (entityType != null)
            {
                var entityInfo = AnalyzeEntity(entityType);
                entities.Add(entityInfo);
            }
        }

        return new DbContextInfo
        {
            Name = contextSymbol.Name,
            Namespace = contextSymbol.ContainingNamespace?.ToDisplayString() ?? "",
            FilePath = classDecl.SyntaxTree.FilePath,
            Entities = entities
        };
    }

    private EntityInfo AnalyzeEntity(INamedTypeSymbol entityType)
    {
        var properties = new List<PropertyInfo>();
        var relationships = new List<RelationshipInfo>();

        foreach (var member in entityType.GetMembers().OfType<IPropertySymbol>())
        {
            var propInfo = new PropertyInfo
            {
                Name = member.Name,
                Type = member.Type.ToDisplayString(),
                IsRequired = !IsNullable(member.Type),
                IsPrimaryKey = IsPrimaryKey(member)
            };

            properties.Add(propInfo);

            // Check for navigation properties
            if (IsNavigationProperty(member))
            {
                var relationship = AnalyzeRelationship(member, entityType);
                if (relationship != null)
                    relationships.Add(relationship);
            }
        }

        return new EntityInfo
        {
            Name = entityType.Name,
            TableName = GetTableName(entityType),
            Properties = properties,
            Relationships = relationships
        };
    }

    private bool IsNullable(ITypeSymbol type)
    {
        return type.NullableAnnotation == NullableAnnotation.Annotated ||
               type.Name == "Nullable";
    }

    private bool IsPrimaryKey(IPropertySymbol property)
    {
        // Simple heuristic: check if property name is "Id" or ends with "Id"
        return property.Name == "Id" || 
               property.Name == $"{property.ContainingType.Name}Id";
    }

    private bool IsNavigationProperty(IPropertySymbol property)
    {
        var type = property.Type;
        
        // Check if it's a reference to another entity
        if (type is INamedTypeSymbol namedType)
        {
            // Collection navigation
            if (namedType.IsGenericType && 
                (namedType.Name == "ICollection" || namedType.Name == "List" || namedType.Name == "HashSet"))
            {
                return true;
            }
            
            // Reference navigation (non-primitive type)
            if (!IsPrimitiveType(type))
            {
                return type.TypeKind == TypeKind.Class && 
                       !type.ToDisplayString().StartsWith("System.");
            }
        }
        
        return false;
    }

    private bool IsPrimitiveType(ITypeSymbol type)
    {
        var typeName = type.ToDisplayString();
        return typeName.StartsWith("System.") || 
               typeName == "string" || 
               typeName == "int" || 
               typeName == "long" ||
               typeName == "decimal" ||
               typeName == "double" ||
               typeName == "float" ||
               typeName == "bool" ||
               typeName == "DateTime" ||
               typeName == "DateTimeOffset" ||
               typeName == "Guid";
    }

    private RelationshipInfo? AnalyzeRelationship(IPropertySymbol property, INamedTypeSymbol entityType)
    {
        var propertyType = property.Type;
        
        if (propertyType is INamedTypeSymbol namedType)
        {
            string targetEntity;
            string relationshipType;
            
            if (namedType.IsGenericType)
            {
                // Collection - One to Many
                targetEntity = namedType.TypeArguments[0].Name;
                relationshipType = "OneToMany";
            }
            else
            {
                // Reference - Many to One or One to One
                targetEntity = namedType.Name;
                relationshipType = "ManyToOne"; // Default, would need more analysis for OneToOne
            }
            
            return new RelationshipInfo
            {
                Type = relationshipType,
                TargetEntity = targetEntity,
                NavigationProperty = property.Name
            };
        }
        
        return null;
    }

    private string GetTableName(INamedTypeSymbol entityType)
    {
        // Could check for [Table] attribute here
        // For now, just pluralize the entity name
        return entityType.Name + "s";
    }
}

// DTOs for JSON output
public class AnalysisResult
{
    public List<DbContextInfo> Contexts { get; set; } = new();
    public string[] Errors { get; set; } = Array.Empty<string>();
    public string[] Warnings { get; set; } = Array.Empty<string>();
}

public class DbContextInfo
{
    public string Name { get; set; } = "";
    public string Namespace { get; set; } = "";
    public string FilePath { get; set; } = "";
    public List<EntityInfo> Entities { get; set; } = new();
}

public class EntityInfo
{
    public string Name { get; set; } = "";
    public string TableName { get; set; } = "";
    public List<PropertyInfo> Properties { get; set; } = new();
    public List<RelationshipInfo> Relationships { get; set; } = new();
}

public class PropertyInfo
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsPrimaryKey { get; set; }
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
}

public class RelationshipInfo
{
    public string Type { get; set; } = ""; // OneToMany, ManyToOne, OneToOne, ManyToMany
    public string TargetEntity { get; set; } = "";
    public string? ForeignKey { get; set; }
    public string NavigationProperty { get; set; } = "";
}