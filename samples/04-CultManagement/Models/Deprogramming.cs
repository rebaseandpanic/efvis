using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Deprogramming
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string SessionName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        [MaxLength(200)]
        public string Deprogrammer { get; set; }
        
        [MaxLength(500)]
        public string Methods { get; set; }
        
        public int ProgressLevel { get; set; }
        
        [MaxLength(1000)]
        public string SessionNotes { get; set; }
        
        [MaxLength(500)]
        public string Challenges { get; set; }
        
        public bool WasSuccessful { get; set; }
        
        [MaxLength(500)]
        public string FinalOutcome { get; set; }
        
        public int FormerFollowerId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties (Note: This would be a separate entity for former followers)
        // public virtual FormerFollower FormerFollower { get; set; }
    }
}