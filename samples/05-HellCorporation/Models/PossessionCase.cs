using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class PossessionCase
    {
        [Key]
        public int PossessionCaseId { get; set; }
        
        public int PossessorDemonId { get; set; }
        
        [MaxLength(100)]
        public string TargetName { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string TargetLocation { get; set; } = string.Empty;
        
        public DateTime PossessionDate { get; set; }
        
        public DateTime? ExorcismDate { get; set; }
        
        [MaxLength(500)]
        public string Objectives { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;

        public virtual Demon Possessor { get; set; } = null!;
    }
}
