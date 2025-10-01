using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DemonicRitual
    {
        [Key]
        public int DemonicRitualId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string RitualName { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string RitualType { get; set; } = string.Empty;
        
        public int LeaderDemonId { get; set; }
        
        public DateTime ScheduledDate { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;
        
        public int ParticipantCount { get; set; }
        
        [MaxLength(1000)]
        public string Purpose { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string RequiredMaterials { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Status { get; set; } = "Planned";

        public virtual Demon Leader { get; set; } = null!;
    }
}
