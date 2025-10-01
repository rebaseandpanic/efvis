using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class AccessControl
    {
        [Key]
        public int AccessControlId { get; set; }

        public int DemonId { get; set; }

        public int? HellGateId { get; set; }

        public int? SecuritySystemId { get; set; }

        [MaxLength(50)]
        public string AccessType { get; set; } = string.Empty; // Entry, Exit, Administrative, Emergency

        public int ClearanceLevel { get; set; } // 1-10

        [MaxLength(200)]
        public string AuthorizedAreas { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RestrictedAreas { get; set; } = string.Empty;

        public DateTime GrantedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Suspended, Revoked, Expired

        public int GrantedByDemonId { get; set; }

        [MaxLength(500)]
        public string GrantReason { get; set; } = string.Empty;

        [MaxLength(200)]
        public string TimeRestrictions { get; set; } = string.Empty;

        public bool RequiresEscort { get; set; }

        public int? EscortDemonId { get; set; }

        [MaxLength(200)]
        public string SpecialConditions { get; set; } = string.Empty;

        public DateTime LastAccessDate { get; set; }

        public int AccessCount { get; set; }

        [MaxLength(500)]
        public string AccessLog { get; set; } = string.Empty;

        public bool RequiresBiometric { get; set; }

        public bool RequiresKeycard { get; set; }

        [MaxLength(50)]
        public string KeycardNumber { get; set; } = string.Empty;

        public DateTime? LastReviewDate { get; set; }

        public DateTime NextReviewDue { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual HellGate? HellGate { get; set; }
        public virtual SecuritySystem? SecuritySystem { get; set; }
        public virtual Demon GrantedBy { get; set; } = null!;
        public virtual Demon? Escort { get; set; }
    }
}