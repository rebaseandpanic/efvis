namespace CursedMuseum.Models;

public class DarkRitual
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public string MoonPhase { get; set; } = string.Empty;
    public bool RequiresBloodSacrifice { get; set; }
    
    public int? WitchId { get; set; }
    public Witch? PerformedBy { get; set; }
    
    public ICollection<Artifact> RequiredArtifacts { get; set; } = new List<Artifact>();
    public ICollection<DemonicEntity> SummonedEntities { get; set; } = new List<DemonicEntity>();
    public ICollection<BloodSacrifice> Sacrifices { get; set; } = new List<BloodSacrifice>();
}