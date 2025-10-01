using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TrainingProgram
    {
        [Key]
        public int TrainingProgramId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProgramName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public int InstructorDemonId { get; set; }

        public int HellHRId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MaxParticipants { get; set; }

        public int CurrentEnrollment { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public bool IsMandatory { get; set; }

        [MaxLength(200)]
        public string Prerequisites { get; set; } = string.Empty;

        [MaxLength(200)]
        public string LearningOutcomes { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Instructor { get; set; } = null!;
        public virtual HellHR HellHR { get; set; } = null!;
    }
}