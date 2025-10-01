namespace HauntedHouse.Models;

public class Exorcist
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public int SuccessfulExorcisms { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    
    public ICollection<Investigation> Investigations { get; set; } = new List<Investigation>();
}