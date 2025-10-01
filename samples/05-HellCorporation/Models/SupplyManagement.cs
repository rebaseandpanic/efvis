using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SupplyManagement
    {
        [Key]
        public int SupplyManagementId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public int ManagerDemonId { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        
        public DateTime CreatedDate { get; set; }
        
        [MaxLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public virtual Demon Manager { get; set; } = null!;
    }
}
