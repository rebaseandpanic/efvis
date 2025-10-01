using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class CircleOfHell
    {
        [Key]
        public int CircleOfHellId { get; set; }

        public int CircleNumber { get; set; } // 1-9

        [Required]
        [MaxLength(100)]
        public string CircleName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public int RulerDemonId { get; set; }

        [MaxLength(200)]
        public string PrimarySinCategory { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TemperatureAverage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SizeSquareKilometers { get; set; }

        public int CurrentPopulation { get; set; }

        public int MaximumCapacity { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal UtilizationRate { get; set; }

        [MaxLength(200)]
        public string ClimateConditions { get; set; } = string.Empty;

        [MaxLength(200)]
        public string GeographicFeatures { get; set; } = string.Empty;

        [MaxLength(300)]
        public string PunishmentSpecializations { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingBudget { get; set; }

        public int StaffCount { get; set; }

        [MaxLength(200)]
        public string Infrastructure { get; set; } = string.Empty;

        public DateTime LastCensusDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        // Navigation properties
        public virtual Demon Ruler { get; set; } = null!;
        public virtual ICollection<DemonDepartment> DemonDepartments { get; set; } = new List<DemonDepartment>();
        public virtual ICollection<SoulWarehouse> SoulWarehouses { get; set; } = new List<SoulWarehouse>();
        public virtual ICollection<Inferno> Infernos { get; set; } = new List<Inferno>();
    }
}