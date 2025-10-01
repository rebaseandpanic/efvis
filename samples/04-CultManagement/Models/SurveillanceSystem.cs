using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SurveillanceSystem
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string SystemName { get; set; }
        
        [MaxLength(100)]
        public string SystemType { get; set; }
        
        public DateTime InstallationDate { get; set; }
        
        public int CameraCount { get; set; }
        
        public int MicrophoneCount { get; set; }
        
        [MaxLength(500)]
        public string CoverageAreas { get; set; }
        
        [MaxLength(300)]
        public string RecordingCapability { get; set; }
        
        public int StorageDurationDays { get; set; }
        
        [MaxLength(200)]
        public string MonitoringPersonnel { get; set; }
        
        public bool HasRemoteAccess { get; set; }
        
        [MaxLength(300)]
        public string AlertSystems { get; set; }
        
        public bool IsOperational { get; set; }
        
        public int CompoundLocationId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CompoundLocation CompoundLocation { get; set; }
    }
}