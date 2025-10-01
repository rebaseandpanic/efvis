using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class LesserDemon
    {
        [Key]
        public int LesserDemonId { get; set; }

        public int DemonId { get; set; }

        [MaxLength(50)]
        public string JobFunction { get; set; } = string.Empty;

        public int SupervisorDemonId { get; set; }

        public bool IsTrainee { get; set; }

        public DateTime TrainingCompletionDate { get; set; }

        [MaxLength(200)]
        public string AssignedTasks { get; set; } = string.Empty;

        public int PerformanceRating { get; set; }

        public bool EligibleForPromotion { get; set; }

        public int YearsOfService { get; set; }

        [MaxLength(200)]
        public string SkillsBeingDeveloped { get; set; } = string.Empty;

        public DateTime LastPerformanceReview { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual Demon Supervisor { get; set; } = null!;
    }
}