namespace CursedMuseum.Models;

public class Curse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Symptoms { get; set; } = string.Empty;
    public string RemovalMethod { get; set; } = string.Empty;
    public bool IsPermanent { get; set; }
    public int StrengthLevel { get; set; }
    
    public int? DarkRitualId { get; set; }
    public DarkRitual? OriginRitual { get; set; }
    
    public ICollection<CursedObject> CursedObjects { get; set; } = new List<CursedObject>();
    public ICollection<Hex> Hexes { get; set; } = new List<Hex>();
}