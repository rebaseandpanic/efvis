using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Recruit
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(200)]
        public string ContactEmail { get; set; }
        
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        
        public DateTime FirstContactDate { get; set; }
        
        [MaxLength(200)]
        public string RecruitmentMethod { get; set; }
        
        public int InterestLevel { get; set; }
        
        [MaxLength(500)]
        public string VulnerabilityProfile { get; set; }
        
        [MaxLength(100)]
        public string CurrentStatus { get; set; }
        
        public bool HasAttendedMeeting { get; set; }
        
        public int? AssignedRecruiterId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower AssignedRecruiter { get; set; }
        public virtual ICollection<RecruitmentCenter> VisitedCenters { get; set; } = new List<RecruitmentCenter>();
    }
}