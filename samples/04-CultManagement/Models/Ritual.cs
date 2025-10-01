using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Ritual
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
        
        public DateTime ScheduledDate { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        public int ParticipantCount { get; set; }
        
        [MaxLength(500)]
        public string RequiredItems { get; set; }
        
        [MaxLength(1000)]
        public string Procedure { get; set; }
        
        public bool IsSecretRitual { get; set; }
        
        public int MinimumRankRequired { get; set; }
        
        public int CultId { get; set; }
        public int? CultLeaderId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual CultLeader CultLeader { get; set; }
        public virtual ICollection<RitualArtifact> RequiredArtifacts { get; set; } = new List<RitualArtifact>();
        public virtual ICollection<Sacrifice> Sacrifices { get; set; } = new List<Sacrifice>();
    }
}