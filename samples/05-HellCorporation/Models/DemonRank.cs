using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class DemonRank
    {
        [Key]
        public int DemonRankId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RankName { get; set; } = string.Empty;

        public int HierarchyLevel { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int MinPowerLevel { get; set; }

        public decimal BaseSalary { get; set; }

        public bool CanCommandLesserDemons { get; set; }

        public int MaxSubordinates { get; set; }

        [MaxLength(100)]
        public string RequiredAbilities { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();
    }
}