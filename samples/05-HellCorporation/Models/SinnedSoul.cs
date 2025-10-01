using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SinnedSoul
    {
        [Key]
        public int SinnedSoulId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FormerName { get; set; } = string.Empty;

        public DateTime DateOfDeath { get; set; }

        public DateTime ArrivalInHell { get; set; }

        [MaxLength(500)]
        public string PrimarySins { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal SinScore { get; set; }

        [MaxLength(50)]
        public string LifetimeOccupation { get; set; } = string.Empty;

        public int Age { get; set; }

        [MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ProcessingStatus { get; set; } = "Pending";

        public int AssignedCircle { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MarketValue { get; set; }

        public bool IsContracted { get; set; }

        [MaxLength(200)]
        public string SpecialAbilities { get; set; } = string.Empty;

        public int TormentResistanceLevel { get; set; }

        [MaxLength(500)]
        public string NotableTransgressions { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<SoulContract> SoulContracts { get; set; } = new List<SoulContract>();
        public virtual ICollection<SoulTransfer> SoulTransfers { get; set; } = new List<SoulTransfer>();
        public virtual ICollection<TortureSession> TortureSessions { get; set; } = new List<TortureSession>();
    }
}