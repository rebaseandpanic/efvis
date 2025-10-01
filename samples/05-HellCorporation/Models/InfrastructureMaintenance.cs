using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class InfrastructureMaintenance
    {
        [Key]
        public int InfrastructureMaintenanceId { get; set; }

        public int HellArchitectureId { get; set; }

        public int MaintenanceDemonId { get; set; }

        [MaxLength(50)]
        public string MaintenanceType { get; set; } = string.Empty; // Preventive, Corrective, Emergency, Upgrade

        public DateTime ScheduledDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, InProgress, Completed, Cancelled, Overdue

        [MaxLength(1000)]
        public string WorkDescription { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualCost { get; set; }

        public int EstimatedDurationHours { get; set; }

        public int ActualDurationHours { get; set; }

        [MaxLength(200)]
        public string MaterialsRequired { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ToolsRequired { get; set; } = string.Empty;

        public int WorkersRequired { get; set; }

        public int WorkersAssigned { get; set; }

        [MaxLength(500)]
        public string WorkNotes { get; set; } = string.Empty;

        [MaxLength(500)]
        public string CompletionNotes { get; set; } = string.Empty;

        public bool QualityInspectionPassed { get; set; }

        public int? InspectorDemonId { get; set; }

        public DateTime? InspectionDate { get; set; }

        [MaxLength(500)]
        public string InspectionComments { get; set; } = string.Empty;

        public bool FollowUpRequired { get; set; }

        public DateTime? FollowUpDate { get; set; }

        // Navigation properties
        public virtual HellArchitecture HellArchitecture { get; set; } = null!;
        public virtual Demon MaintenanceDemon { get; set; } = null!;
        public virtual Demon? Inspector { get; set; }
    }
}