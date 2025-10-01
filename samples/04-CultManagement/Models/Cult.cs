using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Cult
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime FoundedDate { get; set; }
        
        [MaxLength(500)]
        public string Ideology { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public int EstimatedMemberCount { get; set; }
        
        [MaxLength(200)]
        public string HeadquartersLocation { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<CultLeader> Leaders { get; set; } = new List<CultLeader>();
        public virtual ICollection<Follower> Followers { get; set; } = new List<Follower>();
        public virtual ICollection<CultBranch> Branches { get; set; } = new List<CultBranch>();
        public virtual ICollection<Ritual> Rituals { get; set; } = new List<Ritual>();
        public virtual ICollection<CultAsset> Assets { get; set; } = new List<CultAsset>();
        public virtual ICollection<CompoundLocation> Compounds { get; set; } = new List<CompoundLocation>();
    }
}