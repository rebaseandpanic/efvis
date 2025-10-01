namespace CursedMuseum.Models;

public class Hex
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Effect { get; set; } = string.Empty;
    public int DurationDays { get; set; }
    public string TargetType { get; set; } = string.Empty;
    
    public int CurseId { get; set; }
    public Curse RelatedCurse { get; set; } = null!;
    
    public int WitchId { get; set; }
    public Witch CastBy { get; set; } = null!;
}