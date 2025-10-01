using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class TrainingFacility
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Location { get; set; }
        
        [MaxLength(500)]
        public string TrainingTypes { get; set; }
        
        public int Capacity { get; set; }
        
        [MaxLength(500)]
        public string Equipment { get; set; }
        
        [MaxLength(300)]
        public string Instructors { get; set; }
        
        [MaxLength(500)]
        public string Curriculum { get; set; }
        
        public TimeSpan TypicalProgramDuration { get; set; }
        
        public bool IsSecretFacility { get; set; }
        
        [MaxLength(300)]
        public string SecurityClearanceRequired { get; set; }
        
        public int GraduationRate { get; set; }
        
        public bool IsOperational { get; set; }
        
        public int? CultBranchId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CultBranch CultBranch { get; set; }
    }
}