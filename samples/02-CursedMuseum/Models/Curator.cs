namespace CursedMuseum.Models;

public class Curator
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string Expertise { get; set; } = string.Empty;
    public bool HasMysticTraining { get; set; }
    
    public ICollection<MuseumVault> ManagedVaults { get; set; } = new List<MuseumVault>();
    public ICollection<AncientText> CataloguedTexts { get; set; } = new List<AncientText>();
}