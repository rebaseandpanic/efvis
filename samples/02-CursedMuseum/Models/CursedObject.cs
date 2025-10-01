namespace CursedMuseum.Models;

public class CursedObject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public int AgeInYears { get; set; }
    public string Material { get; set; } = string.Empty;
    public int DangerLevel { get; set; }
    public bool IsSealed { get; set; }
    
    public int CurseId { get; set; }
    public Curse Curse { get; set; } = null!;
    
    public int? VaultId { get; set; }
    public MuseumVault? Vault { get; set; }
    
    public ICollection<Victim> Victims { get; set; } = new List<Victim>();
    public ICollection<ProtectionSpell> ProtectionSpells { get; set; } = new List<ProtectionSpell>();
}