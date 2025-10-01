using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class SoulInventory
    {
        [Key]
        public int SoulInventoryId { get; set; }

        public int SoulWarehouseId { get; set; }

        public int SinnedSoulId { get; set; }

        [MaxLength(50)]
        public string StorageLocation { get; set; } = string.Empty;

        public DateTime StorageDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Stored"; // Stored, InTransit, Processing, Released

        public int CustodianDemonId { get; set; }

        [MaxLength(200)]
        public string StorageConditions { get; set; } = string.Empty;

        public bool RequiresSpecialHandling { get; set; }

        [MaxLength(300)]
        public string SpecialHandlingInstructions { get; set; } = string.Empty;

        public DateTime LastInspectionDate { get; set; }

        public DateTime NextInspectionDue { get; set; }

        [MaxLength(500)]
        public string InspectionNotes { get; set; } = string.Empty;

        public bool IsQuarantined { get; set; }

        [MaxLength(300)]
        public string QuarantineReason { get; set; } = string.Empty;

        public int Priority { get; set; } // 1-10

        [MaxLength(100)]
        public string InventoryNumber { get; set; } = string.Empty;

        // Navigation properties
        public virtual SoulWarehouse SoulWarehouse { get; set; } = null!;
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon Custodian { get; set; } = null!;
    }
}