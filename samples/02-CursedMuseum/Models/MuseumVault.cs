namespace CursedMuseum.Models;

public class MuseumVault
{
    public int Id { get; set; }
    public string Location { get; set; } = string.Empty;
    public int SecurityLevel { get; set; }
    public string SealType { get; set; } = string.Empty;
    public bool IsSealed { get; set; }
    public int MaxCapacity { get; set; }
    
    public int? CuratorId { get; set; }
    public Curator? AssignedCurator { get; set; }
    
    public ICollection<CursedObject> StoredObjects { get; set; } = new List<CursedObject>();
    public ICollection<SealedRoom> ConnectedRooms { get; set; } = new List<SealedRoom>();
}