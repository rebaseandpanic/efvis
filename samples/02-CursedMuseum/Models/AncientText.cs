namespace CursedMuseum.Models;

public class AncientText
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int AgeInYears { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsTranslated { get; set; }
    
    public int? CuratorId { get; set; }
    public Curator? CataloguedBy { get; set; }
    
    public ICollection<Artifact> DescribedArtifacts { get; set; } = new List<Artifact>();
}