namespace HauntedHouse.Models;

public class HauntedHouse
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int YearBuilt { get; set; }
    public int ActivityLevel { get; set; }
    public string History { get; set; } = string.Empty;
    public bool IsAbandoned { get; set; }
    
    public ICollection<Ghost> Ghosts { get; set; } = new List<Ghost>();
    public ICollection<ParanormalActivity> Activities { get; set; } = new List<ParanormalActivity>();
    public ICollection<Investigation> Investigations { get; set; } = new List<Investigation>();
}