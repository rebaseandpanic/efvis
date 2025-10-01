using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class TortureSchedule
    {
        [Key]
        public int TortureScheduleId { get; set; }

        public int TortureDepartmentId { get; set; }

        public int SinnedSoulId { get; set; }

        public int TorturerDemonId { get; set; }

        public DateTime ScheduledDate { get; set; }

        public DateTime ScheduledStartTime { get; set; }

        public DateTime ScheduledEndTime { get; set; }

        public int PunishmentMethodId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, InProgress, Completed, Cancelled

        public int Priority { get; set; } // 1-10

        [MaxLength(500)]
        public string SpecialInstructions { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RequiredEquipment { get; set; } = string.Empty;

        public bool RequiresSupervisor { get; set; }

        public int? SupervisorDemonId { get; set; }

        [MaxLength(300)]
        public string PrepTime { get; set; } = string.Empty;

        [MaxLength(300)]
        public string CleanupTime { get; set; } = string.Empty;

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [MaxLength(500)]
        public string CompletionNotes { get; set; } = string.Empty;

        public bool QualityCheckRequired { get; set; }

        public bool QualityCheckPassed { get; set; }

        // Navigation properties
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon Torturer { get; set; } = null!;
        public virtual PunishmentMethod PunishmentMethod { get; set; } = null!;
        public virtual Demon? Supervisor { get; set; }
    }
}