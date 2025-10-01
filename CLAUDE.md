# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview
**efvis** - Entity Framework Visualizer VS Code Extension that visualizes Entity Framework DbContext models as interactive diagrams with retro brutalist design aesthetics.

## Development Commands

### Full Build Process
```bash
# 1. Install dependencies
npm install
cd src/webview && npm install && cd ../..

# 2. Build C# analyzer
cd src/analyzer
dotnet restore
dotnet build -c Release
cd ../..

# 3. Build webview React app
cd src/webview && npm run build && cd ../..

# 4. Build extension
npm run compile
```

### Development Mode
```bash
# Terminal 1: Watch extension TypeScript
npm run watch

# Terminal 2: Watch webview React app
cd src/webview && npm run watch

# Terminal 3: Rebuild analyzer after C# changes
cd src/analyzer && dotnet build -c Release
```

### Testing
```bash
# Test in VS Code: Press F5 to launch Extension Development Host
# Run command: Ctrl+Shift+P → "efvis: Show EF Diagram"

# Run analyzer standalone
dotnet run --project src/analyzer/analyzer.csproj -- --project <path-to-csproj>
```

### Packaging & Publishing
```bash
# Create VSIX package
npm install -g vsce
vsce package

# Install VSIX: VS Code → Extensions → Install from VSIX
```

### Test Web App
```bash
# Test visualization in browser (testweb folder)
cd testweb
npm install
npm start  # Opens at http://localhost:8080
```

## Architecture

### Three-Component System

1. **VS Code Extension** (`src/extension.ts`)
   - Registers `efvis.show` command
   - Creates and manages webview panels
   - Spawns C# analyzer process
   - Handles message passing between components
   - Manages `.efvis-layout.json` persistence
   - Auto-compiles analyzer on first run

2. **C# Analyzer** (`src/analyzer/`)
   - Roslyn-based .NET console application
   - Analyzes `.csproj` files via MSBuild workspace
   - Finds DbContext classes and extracts entity models
   - Outputs JSON with entities, properties, relationships
   - Supports fallback regex-based analysis
   - Targets .NET 9.0 (requires .NET SDK 6.0+)

3. **Webview UI** (`src/webview/`)
   - React 18 application with ReactFlow for diagrams
   - Dagre layout algorithm for auto-positioning
   - Drag & drop with position persistence
   - Retro brutalist design (hard shadows, bright colors)
   - Communicates with extension via `vscode.postMessage`

### Data Flow
```
User Command → Extension → Analyzer Process → JSON Model → Webview → Diagram
                   ↓                                           ↓
            Layout File (.efvis-layout.json) ←─────────────────┘
```

### Key Files & Patterns

#### Extension-Webview Communication
- **Messages to Webview**: `analyzeComplete`, `layoutData`, `error`
- **Messages from Extension**: `analyze`, `saveLayout`, `selectContext`
- Pattern: Async command handlers with progress notifications

#### Analyzer Output Format
```json
{
  "Contexts": [{
    "Name": "DbContextName",
    "Namespace": "Full.Namespace",
    "FilePath": "/path/to/file.cs",
    "Entities": [{
      "Name": "EntityName",
      "TableName": "table_name",
      "Properties": [{
        "Name": "PropertyName",
        "Type": "System.String",
        "IsPrimaryKey": true,
        "IsRequired": true,
        "MaxLength": 256
      }],
      "Relationships": [{
        "Type": "OneToMany",
        "TargetEntity": "OtherEntity",
        "ForeignKey": "FK_Field",
        "NavigationProperty": "NavigationName"
      }]
    }]
  }]
}
```

#### Layout Persistence
- File: `.efvis-layout.json` in workspace root
- Stores node positions per DbContext
- Applied on diagram reload
- Updated on node drag end

## Design System

### Retro Brutalist Theme
- **Colors**: Defined in `src/webview/src/styles/theme.css`
  - Cream backgrounds (#F4E8C1)
  - Hard navy borders (#1A535C)
  - Orange/teal/coral accents
- **Shadows**: Hard 8px offsets without blur
- **Borders**: 2-3px solid, no border-radius
- **Typography**: Uppercase headers, 700-900 font weights
- **Animations**: Bounce transitions (cubic-bezier)

### ReactFlow Customization
- Custom node components in `src/webview/src/components/EntityNode.tsx`
- Edge styling for relationship types (1:1, 1:N, N:N)
- Dagre layout with rankdir='TB' (top-to-bottom)

## Common Issues & Solutions

### Analyzer Not Found
Extension auto-compiles analyzer on first run. If fails:
```bash
cd src/analyzer
dotnet clean
dotnet restore
dotnet build -c Release
```

### Webview Blank
Check webview is built:
```bash
cd src/webview
npm run build
ls dist/  # Should contain bundle.js
```

### Remote/Container Support
- Extension runs on host, reads files via VS Code API
- Analyzer executes on host with mounted paths
- Layout file saved in workspace (synced)

## Testing Different Scenarios

### Test with Sample Project
```bash
# Use test/VpnManagement project
dotnet run --project src/analyzer/analyzer.csproj -- --project test/VpnManagement/VpnManagement.csproj
```

### Generate Test Model
```bash
# Output to file for testing
dotnet run --project src/analyzer/analyzer.csproj -- --project <project> > testweb/db-model.json
```

## Important Conventions

- **No jQuery** - Use vanilla JavaScript or React
- **ES6 Modules** - Import/export syntax for JavaScript
- **TypeScript** for extension code
- **Async/Await** for all async operations
- **Progress Notifications** for long-running tasks
- **Error Boundaries** in React components
- **Retro Design** - Follow brutalist aesthetics from `docs/retro-design.md`