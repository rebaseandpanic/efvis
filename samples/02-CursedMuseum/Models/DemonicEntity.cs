namespace CursedMuseum.Models;

public class DemonicEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string Abilities { get; set; } = string.Empty;
    public bool IsContained { get; set; }
    
    public int DarkRitualId { get; set; }
    public DarkRitual SummonedBy { get; set; } = null!;
    
    public int? SealedRoomId { get; set; }
    public SealedRoom? ContainedIn { get; set; }
}