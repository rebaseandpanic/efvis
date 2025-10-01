using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class RecruitmentCenter
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Address { get; set; }
        
        [MaxLength(100)]
        public string CoverBusiness { get; set; }
        
        public DateTime OpenedDate { get; set; }
        
        [MaxLength(500)]
        public string TargetDemographics { get; set; }
        
        [MaxLength(500)]
        public string RecruitmentMethods { get; set; }
        
        public int MonthlyRecruits { get; set; }
        
        public int SuccessfulConversions { get; set; }
        
        [MaxLength(200)]
        public string Manager { get; set; }
        
        [MaxLength(300)]
        public string OperatingHours { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool HasBeenExposed { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Recruit> Recruits { get; set; } = new List<Recruit>();
    }
}