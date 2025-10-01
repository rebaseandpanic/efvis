namespace CursedMuseum.Models;

public class ProtectionSpell
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Incantation { get; set; } = string.Empty;
    public int EffectiveDays { get; set; }
    public DateTime CastDate { get; set; }
    public bool IsActive { get; set; }
    
    public int WitchId { get; set; }
    public Witch CastBy { get; set; } = null!;
    
    public int CursedObjectId { get; set; }
    public CursedObject ProtectedObject { get; set; } = null!;
}