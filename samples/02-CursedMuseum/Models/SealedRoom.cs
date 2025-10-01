namespace CursedMuseum.Models;

public class SealedRoom
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string SealType { get; set; } = string.Empty;
    public DateTime SealDate { get; set; }
    public int DangerLevel { get; set; }
    public bool IsAccessible { get; set; }
    
    public int MuseumVaultId { get; set; }
    public MuseumVault Vault { get; set; } = null!;
    
    public ICollection<DemonicEntity> ContainedEntities { get; set; } = new List<DemonicEntity>();
}