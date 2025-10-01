using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class DemonDepartment
    {
        [Key]
        public int DemonDepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int ManagerDemonId { get; set; }

        public int CircleOfHellId { get; set; }

        public decimal AnnualBudget { get; set; }

        public int CurrentHeadcount { get; set; }

        public int MaxHeadcount { get; set; }

        [MaxLength(50)]
        public string PrimaryFunction { get; set; } = string.Empty;

        public DateTime EstablishedDate { get; set; }

        // Navigation properties
        public virtual CircleOfHell CircleOfHell { get; set; } = null!;
        public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();
        public virtual ICollection<TortureDepartment> TortureDepartments { get; set; } = new List<TortureDepartment>();
    }
}