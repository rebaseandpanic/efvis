using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ArchDemon
    {
        [Key]
        public int ArchDemonId { get; set; }

        public int DemonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Domain { get; set; } = string.Empty;

        public int SubordinateCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AuthorityLevel { get; set; }

        public bool CanCommandLegions { get; set; }

        public DateTime AppointmentDate { get; set; }

        [MaxLength(200)]
        public string SpecialPrivileges { get; set; } = string.Empty;

        public int CirclesControlled { get; set; }

        [MaxLength(500)]
        public string Responsibilities { get; set; } = string.Empty;

        public bool HasDirectAccessToLucifer { get; set; }

        public int PowerMultiplier { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}