namespace CursedMuseum.Models;

public class Talisman
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProtectionType { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string Material { get; set; } = string.Empty;
    public bool IsCharged { get; set; }
    
    public int ArtifactId { get; set; }
    public Artifact LinkedArtifact { get; set; } = null!;
}