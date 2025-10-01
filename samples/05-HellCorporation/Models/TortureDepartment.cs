using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TortureDepartment
    {
        [Key]
        public int TortureDepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        public int DemonDepartmentId { get; set; }

        public int ChiefTorturerDemonId { get; set; }

        public int TotalTorturers { get; set; }

        public int CurrentCapacity { get; set; }

        public int DailyQuota { get; set; }

        public int QuotaAchievedToday { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal EfficiencyRating { get; set; }

        [MaxLength(200)]
        public string Specialization { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualBudget { get; set; }

        public int EquipmentCount { get; set; }

        public int ActiveSessions { get; set; }

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        public DateTime LastInspectionDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        [MaxLength(500)]
        public string SafetyProtocols { get; set; } = string.Empty;

        public int IncidentsThisMonth { get; set; }

        // Navigation properties
        public virtual DemonDepartment DemonDepartment { get; set; } = null!;
        public virtual Demon ChiefTorturer { get; set; } = null!;
        public virtual ICollection<TortureSession> TortureSessions { get; set; } = new List<TortureSession>();
        public virtual ICollection<TortureEquipment> TortureEquipments { get; set; } = new List<TortureEquipment>();
    }
}