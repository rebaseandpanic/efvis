namespace HauntedHouse.Models;

public class Investigation
{
    public int Id { get; set; }
    public DateTime InvestigationDate { get; set; }
    public string Findings { get; set; } = string.Empty;
    public string EVPRecordings { get; set; } = string.Empty;
    public string VideoEvidence { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    
    public int HauntedHouseId { get; set; }
    public HauntedHouse HauntedHouse { get; set; } = null!;
    
    public int ExorcistId { get; set; }
    public Exorcist Exorcist { get; set; } = null!;
    
    public ICollection<ParanormalActivity> ActivitiesFound { get; set; } = new List<ParanormalActivity>();
}