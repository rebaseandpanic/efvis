using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TorturerShift
    {
        [Key]
        public int TorturerShiftId { get; set; }

        public int TorturerDemonId { get; set; }

        public int TortureDepartmentId { get; set; }

        public DateTime ShiftDate { get; set; }

        public DateTime ShiftStartTime { get; set; }

        public DateTime ShiftEndTime { get; set; }

        public int ScheduledDurationHours { get; set; }

        public int ActualDurationHours { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, InProgress, Completed, Cancelled

        public int ScheduledSessions { get; set; }

        public int CompletedSessions { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PainPointsGenerated { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ProductivityRating { get; set; }

        [MaxLength(500)]
        public string ShiftNotes { get; set; } = string.Empty;

        public bool OvertimeWorked { get; set; }

        public int OvertimeHours { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OvertimePay { get; set; }

        public bool BreaksTaken { get; set; }

        public int BreakDurationMinutes { get; set; }

        [MaxLength(200)]
        public string EquipmentUsed { get; set; } = string.Empty;

        public bool IncidentsOccurred { get; set; }

        [MaxLength(500)]
        public string IncidentDetails { get; set; } = string.Empty;

        public int SupervisorDemonId { get; set; }

        // Navigation properties
        public virtual Demon Torturer { get; set; } = null!;
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual Demon Supervisor { get; set; } = null!;
    }
}