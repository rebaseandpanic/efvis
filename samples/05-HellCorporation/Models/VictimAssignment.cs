using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class VictimAssignment
    {
        [Key]
        public int VictimAssignmentId { get; set; }

        public int SinnedSoulId { get; set; }

        public int TorturerDemonId { get; set; }

        public int TortureDepartmentId { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime? UnassignmentDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Transferred, Suspended

        [MaxLength(500)]
        public string AssignmentReason { get; set; } = string.Empty;

        [MaxLength(200)]
        public string SpecialRequirements { get; set; } = string.Empty;

        public int Priority { get; set; } // 1-10

        [MaxLength(500)]
        public string HandlingInstructions { get; set; } = string.Empty;

        public bool RequiresSpecialTraining { get; set; }

        [MaxLength(300)]
        public string TrainingRequired { get; set; } = string.Empty;

        public int AssignedByDemonId { get; set; }

        public DateTime? LastSessionDate { get; set; }

        public int TotalSessionsCompleted { get; set; }

        [MaxLength(500)]
        public string PerformanceNotes { get; set; } = string.Empty;

        public bool IsExclusive { get; set; }

        [MaxLength(200)]
        public string Restrictions { get; set; } = string.Empty;

        public DateTime? NextScheduledSession { get; set; }

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon Torturer { get; set; } = null!;
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual Demon AssignedBy { get; set; } = null!;
    }
}