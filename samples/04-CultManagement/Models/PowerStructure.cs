using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class PowerStructure
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string HierarchyName { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public int Level { get; set; }
        
        [MaxLength(300)]
        public string Authority { get; set; }
        
        [MaxLength(300)]
        public string Responsibilities { get; set; }
        
        public bool CanGiveOrders { get; set; }
        
        public bool CanPunishMembers { get; set; }
        
        public int? ParentStructureId { get; set; }
        public int CultRankId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual PowerStructure ParentStructure { get; set; }
        public virtual CultRank CultRank { get; set; }
        public virtual ICollection<PowerStructure> SubStructures { get; set; } = new List<PowerStructure>();
    }
}