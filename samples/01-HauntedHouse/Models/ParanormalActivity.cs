namespace HauntedHouse.Models;

public class ParanormalActivity
{
    public int Id { get; set; }
    public DateTime OccurredAt { get; set; }
    public string ActivityType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IntensityLevel { get; set; }
    public string Witnesses { get; set; } = string.Empty;
    public string EvidenceRecorded { get; set; } = string.Empty;
    
    public int HauntedHouseId { get; set; }
    public HauntedHouse HauntedHouse { get; set; } = null!;
    
    public int? GhostId { get; set; }
    public Ghost? Ghost { get; set; }
    
    public int? InvestigationId { get; set; }
    public Investigation? Investigation { get; set; }
}