using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SacredText
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [MaxLength(100)]
        public string Author { get; set; }
        
        public DateTime WrittenDate { get; set; }
        
        [MaxLength(5000)]
        public string Content { get; set; }
        
        [MaxLength(100)]
        public string Language { get; set; }
        
        public int ChapterCount { get; set; }
        
        [MaxLength(500)]
        public string MainTeachings { get; set; }
        
        public bool IsSecretText { get; set; }
        
        public int AccessLevel { get; set; }
        
        [MaxLength(200)]
        public string PhysicalLocation { get; set; }
        
        public bool HasBeenTranslated { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<PropagandaMaterial> RelatedPropaganda { get; set; } = new List<PropagandaMaterial>();
    }
}