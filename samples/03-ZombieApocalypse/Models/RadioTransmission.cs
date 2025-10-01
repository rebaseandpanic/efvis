using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class RadioTransmission
    {
        [Key]
        public int TransmissionId { get; set; }
        
        public DateTime TransmissionDate { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
        
        public string Frequency { get; set; } = string.Empty;
        
        public string Priority { get; set; } = string.Empty; // Emergency, High, Normal, Low
        
        public bool IsEncrypted { get; set; }
        
        public string TransmissionType { get; set; } = string.Empty; // SOS, Status, Coordination, Warning
        
        // Foreign Keys
        public int? SenderId { get; set; }
        public int? MilitaryBaseId { get; set; }
        public int? CommunicationTowerId { get; set; }
        
        // Navigation Properties
        public Survivor? Sender { get; set; }
        public MilitaryBase? MilitaryBase { get; set; }
        public CommunicationTower? CommunicationTower { get; set; }
    }
}