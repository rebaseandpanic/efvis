namespace HauntedHouse.Models;

public class Ghost
{
    public int Id { get; set; }
    public string NameInLife { get; set; } = string.Empty;
    public DateTime DeathDate { get; set; }
    public string CauseOfDeath { get; set; } = string.Empty;
    public string GhostType { get; set; } = string.Empty;
    public bool IsHostile { get; set; }
    public string Appearance { get; set; } = string.Empty;
    
    public int HauntedHouseId { get; set; }
    public HauntedHouse HauntedHouse { get; set; } = null!;
    
    public ICollection<ParanormalActivity> Activities { get; set; } = new List<ParanormalActivity>();
}