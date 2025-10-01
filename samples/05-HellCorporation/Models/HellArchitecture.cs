using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class HellArchitecture
    {
        [Key]
        public int HellArchitectureId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StructureName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string StructureType { get; set; } = string.Empty; // Building, Bridge, Tower, Fortress

        public int ArchitectDemonId { get; set; }

        public DateTime ConstructionStartDate { get; set; }

        public DateTime? ConstructionEndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Planning"; // Planning, Under Construction, Completed, Renovating

        [Column(TypeName = "decimal(15,2)")]
        public decimal SizeSquareMeters { get; set; }

        public int FloorCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ConstructionCost { get; set; }

        [MaxLength(200)]
        public string Materials { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ArchitecturalStyle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SpecialFeatures { get; set; } = string.Empty;

        public bool HasSupernaturalElements { get; set; }

        [MaxLength(300)]
        public string SupernaturalElements { get; set; } = string.Empty;

        [MaxLength(200)]
        public string SafetyFeatures { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaintenanceCostAnnual { get; set; }

        public DateTime LastInspectionDate { get; set; }

        [MaxLength(500)]
        public string InspectionFindings { get; set; } = string.Empty;

        public bool RequiresSpecialPermits { get; set; }

        [MaxLength(200)]
        public string PermitsRequired { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Architect { get; set; } = null!;
        public virtual ICollection<InfrastructureMaintenance> InfrastructureMaintenances { get; set; } = new List<InfrastructureMaintenance>();
    }
}