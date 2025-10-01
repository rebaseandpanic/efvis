# Zombie Apocalypse Survival - Sample DbContext

**30 Entities** - Comprehensive post-apocalyptic survival system

## Entity Categories

### Threats (6)
- Zombie, ZombieHorde, Mutation, Infection, InfectedAnimal, MutantBoss

### Survivors & Groups (4)
- Survivor, SurvivalGroup, Cannibal, RaiderGang

### Locations & Infrastructure (10)
- SafeHouse, MilitaryBase, Bunker, DeadCity, AbandonedHospital
- RadiationZone, BioweaponLab, QuarantineZone, EvacuationPoint, CommunicationTower

### Resources & Supplies (7)
- WeaponCache, FoodSupply, MedicalSupply, AmmunitionStockpile
- Vaccine, SupplyDrop, TrapSystem

### Transportation & Communication (3)
- Helicopter, RadioTransmission, LastHuman

## Complex Relationship Network

- **Infection Chains**: Zombie → Infection → Survivor
- **Supply Networks**: MilitaryBase → Helicopter → SupplyDrop → Supplies
- **Safe Zones**: SafeHouse contains Survivors, WeaponCaches, FoodSupplies
- **Research**: BioweaponLab → Vaccine → QuarantineZone
- **Communications**: CommunicationTower → RadioTransmission → SurvivalGroups
- **Threats**: ZombieHorde attacks DeadCity, RadiationZone spawns Mutations
- **Evacuation**: EvacuationPoint ↔ Helicopter (many-to-many)

## Usage

```bash
dotnet build
```

Visualize the complex apocalypse survival network with efvis extension.