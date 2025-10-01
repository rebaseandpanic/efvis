namespace CursedMuseum.Models;

public class BloodSacrifice
{
    public int Id { get; set; }
    public DateTime SacrificeDate { get; set; }
    public string SacrificeType { get; set; } = string.Empty;
    public int BloodAmount { get; set; }
    public string Purpose { get; set; } = string.Empty;
    
    public int DarkRitualId { get; set; }
    public DarkRitual Ritual { get; set; } = null!;
    
    public int? VictimId { get; set; }
    public Victim? Victim { get; set; }
}