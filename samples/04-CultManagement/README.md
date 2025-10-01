# Cult Management System

A comprehensive Entity Framework-based data model for tracking and managing cult operations, members, rituals, finances, and investigations. This system provides a complete database schema for law enforcement, researchers, or fictional applications dealing with cult-related activities.

## Overview

This system models the complex organizational structure and operations of cults through 50 interconnected entities covering:

- **Core Cult Structure**: Organizational hierarchy and leadership
- **Rituals & Ceremonies**: Religious and ceremonial activities
- **Mind Control & Indoctrination**: Psychological manipulation techniques
- **Finances & Operations**: Financial schemes and money management
- **Facilities & Security**: Physical locations and security measures

## Entity Categories

### Core Cult Structure (10 entities)
1. **Cult** - Main cult organization
2. **CultLeader** - Leadership positions and authority
3. **Follower** - Regular cult members
4. **InnerCircle** - Trusted high-ranking members
5. **Recruit** - Potential new members being recruited
6. **CultRank** - Hierarchical ranking system
7. **CultBranch** - Geographic or organizational divisions
8. **CultCouncil** - Decision-making bodies
9. **SuccessionPlan** - Leadership transition planning
10. **PowerStructure** - Authority and command relationships

### Rituals & Ceremonies (10 entities)
11. **Ritual** - Ceremonial activities and events
12. **Sacrifice** - Ritual sacrificial activities
13. **BloodOath** - Binding loyalty commitments
14. **InitiationRite** - Member initiation ceremonies
15. **SecretCeremony** - Restricted ceremonial activities
16. **PropheticVision** - Religious visions and prophecies
17. **ChosenOne** - Special individuals with prophetic significance
18. **SacredText** - Religious documents and teachings
19. **RitualArtifact** - Ceremonial objects and tools
20. **HolyDay** - Religious calendar and observances

### Mind Control & Indoctrination (10 entities)
21. **BrainwashingSession** - Psychological manipulation sessions
22. **PropagandaMaterial** - Indoctrination content and media
23. **MindControlTechnique** - Psychological manipulation methods
24. **Deprogramming** - Rehabilitation from cult influence
25. **ThoughtReform** - Systematic belief modification
26. **IsolationPeriod** - Controlled isolation for indoctrination
27. **ConfessionSession** - Forced confession and control
28. **Punishment** - Disciplinary actions and consequences
29. **RewardSystem** - Incentive and recognition programs
30. **LoyaltyTest** - Loyalty verification and testing

### Finances & Operations (10 entities)
31. **MoneyLaundering** - Financial crime operations
32. **PyramidScheme** - Recruitment-based financial schemes
33. **DonationRecord** - Member financial contributions
34. **CultAsset** - Organizational assets and property
35. **FrontCompany** - Legitimate business covers
36. **BankAccount** - Financial accounts and management
37. **FinancialAudit** - Financial oversight and compliance
38. **TaxEvasion** - Tax avoidance schemes
39. **PropertyDeed** - Real estate ownership records
40. **InvestmentPortfolio** - Investment management and holdings

### Facilities & Security (10 entities)
41. **CompoundLocation** - Physical cult locations
42. **ArmsCache** - Weapon storage and inventory
43. **SurveillanceSystem** - Monitoring and security equipment
44. **SafeHouse** - Secure hideout locations
45. **UndergroundTemple** - Hidden ceremonial spaces
46. **RecruitmentCenter** - Member recruitment facilities
47. **TrainingFacility** - Member training and education
48. **SecretMeetingPlace** - Covert meeting locations
49. **EscapeRoute** - Emergency evacuation plans
50. **SecurityDetail** - Protection and security personnel

## Key Relationships

### Hierarchical Structure
- **Cult** → **CultLeader** (One-to-Many)
- **Cult** → **Follower** (One-to-Many)
- **CultLeader** → **InnerCircle** (One-to-Many)
- **Follower** → **CultRank** (Many-to-One)

### Operational Relationships
- **Ritual** → **Sacrifice** (One-to-Many)
- **Ritual** ↔ **RitualArtifact** (Many-to-Many)
- **Follower** → **BrainwashingSession** (One-to-Many)
- **Follower** → **DonationRecord** (One-to-Many)

### Facility Management
- **Cult** → **CompoundLocation** (One-to-Many)
- **CompoundLocation** → **ArmsCache** (One-to-Many)
- **CompoundLocation** → **SurveillanceSystem** (One-to-Many)

### Financial Tracking
- **FrontCompany** → **BankAccount** (One-to-Many)
- **CultAsset** → **PropertyDeed** (One-to-Many)
- **PyramidScheme** → **DonationRecord** (One-to-Many)

## Database Features

### Security & Audit Trail
- All entities include `CreatedAt` and `UpdatedAt` timestamps
- Soft delete capabilities for sensitive records
- Comprehensive audit logging for financial transactions

### Data Integrity
- Foreign key constraints with appropriate cascade rules
- Unique constraints on critical identifiers
- Check constraints for data validation
- Indexes for performance optimization

### Flexible Design
- Support for hierarchical structures (self-referencing relationships)
- Many-to-many relationships for complex associations
- Nullable foreign keys for optional relationships
- Extensible attribute system for custom properties

## Usage Examples

### Basic Cult Structure Query
```csharp
var cultWithLeaders = context.Cults
    .Include(c => c.Leaders)
    .Include(c => c.Followers)
    .ThenInclude(f => f.CultRank)
    .FirstOrDefault(c => c.Name == "Example Cult");
```

### Financial Investigation Query
```csharp
var suspiciousTransactions = context.DonationRecords
    .Where(d => d.Amount > 10000 && d.WasCoerced)
    .Include(d => d.Follower)
    .Include(d => d.PyramidScheme)
    .ToList();
```

### Facility Security Assessment
```csharp
var secureCompounds = context.CompoundLocations
    .Include(c => c.ArmsCaches)
    .Include(c => c.SurveillanceSystems)
    .Include(c => c.EscapeRoutes)
    .Where(c => c.SecurityLevel == "High")
    .ToList();
```

## Implementation Notes

### Entity Framework Configuration
- Uses Entity Framework Core with SQL Server
- Fluent API configuration for complex relationships
- Custom value converters for specialized data types
- Migration support for database evolution

### Performance Considerations
- Strategic indexing on frequently queried columns
- Lazy loading disabled for security-sensitive queries
- Pagination support for large result sets
- Connection pooling for high-throughput scenarios

### Security Considerations
- Encrypted storage for sensitive information
- Role-based access control integration points
- Audit logging for all data modifications
- Anonymization support for research applications

## Legal and Ethical Notice

This data model is designed for:
- **Law Enforcement**: Investigating and tracking cult activities
- **Academic Research**: Studying cult organization and behavior
- **Fictional Applications**: Gaming, literature, or entertainment
- **Security Analysis**: Understanding cult operational patterns

**Important**: This system should only be used for legitimate purposes such as law enforcement, academic research, or fictional applications. Any use for actual cult management or illegal activities is strictly prohibited and may violate local, state, and federal laws.

## Database Setup

1. **Install Entity Framework Core**:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. **Configure Connection String**:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CultManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Create and Apply Migrations**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Contributing

This model can be extended for specific use cases:
- Additional entity types for specialized investigations
- Enhanced security features for classified applications
- Integration with external intelligence systems
- Custom reporting and analytics capabilities

## License

This project is provided for educational and research purposes. Please ensure compliance with all applicable laws and regulations in your jurisdiction.