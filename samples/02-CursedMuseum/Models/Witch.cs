namespace CursedMuseum.Models;

public class Witch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string GrimoireName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public ICollection<DarkRitual> PerformedRituals { get; set; } = new List<DarkRitual>();
    public ICollection<ProtectionSpell> CastSpells { get; set; } = new List<ProtectionSpell>();
    public ICollection<Hex> CastHexes { get; set; } = new List<Hex>();
}