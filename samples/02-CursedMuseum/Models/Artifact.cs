namespace CursedMuseum.Models;

public class Artifact
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AgeInYears { get; set; }
    public string Material { get; set; } = string.Empty;
    public string MagicalProperties { get; set; } = string.Empty;
    public string Runes { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    
    public int? AncientTextId { get; set; }
    public AncientText? DescribedIn { get; set; }
    
    public ICollection<DarkRitual> UsedInRituals { get; set; } = new List<DarkRitual>();
    public ICollection<Talisman> Talismans { get; set; } = new List<Talisman>();
}