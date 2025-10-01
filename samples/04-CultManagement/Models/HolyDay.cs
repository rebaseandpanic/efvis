using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class HolyDay
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime Date { get; set; }
        
        [MaxLength(100)]
        public string Frequency { get; set; }
        
        [MaxLength(500)]
        public string Significance { get; set; }
        
        [MaxLength(500)]
        public string TraditionalActivities { get; set; }
        
        public bool IsMandatoryAttendance { get; set; }
        
        [MaxLength(300)]
        public string DressCode { get; set; }
        
        [MaxLength(300)]
        public string SpecialRules { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}