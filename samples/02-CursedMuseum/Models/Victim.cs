namespace CursedMuseum.Models;

public class Victim
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime ContactDate { get; set; }
    public string Consequences { get; set; } = string.Empty;
    public bool IsAlive { get; set; }
    public string CurrentCondition { get; set; } = string.Empty;
    
    public int CursedObjectId { get; set; }
    public CursedObject CursedObject { get; set; } = null!;
    
    public ICollection<BloodSacrifice> RelatedSacrifices { get; set; } = new List<BloodSacrifice>();
}