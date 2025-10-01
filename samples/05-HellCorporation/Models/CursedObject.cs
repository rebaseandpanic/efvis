using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class CursedObject
    {
        [Key]
        public int CursedObjectId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string ObjectName { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string ObjectType { get; set; } = string.Empty;
        
        public int CreatorDemonId { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        [MaxLength(500)]
        public string CurseDescription { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string CurrentLocation { get; set; } = string.Empty;
        
        public int PowerLevel { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public virtual Demon Creator { get; set; } = null!;
    }
}
