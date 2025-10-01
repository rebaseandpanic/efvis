# Cursed Objects Museum - Sample DbContext

**15 Entities** - Dark museum managing cursed artifacts and supernatural threats

## Entities

1. **CursedObject** - Dangerous supernatural items
2. **Curse** - Malevolent supernatural effects
3. **Victim** - People affected by curses
4. **Artifact** - Magical ancient objects
5. **DarkRitual** - Occult ceremonies
6. **Witch** - Practitioners of dark magic
7. **ProtectionSpell** - Defensive magic
8. **MuseumVault** - Secure storage areas
9. **Curator** - Museum staff managing collections
10. **Hex** - Targeted curses
11. **Talisman** - Protective charms
12. **DemonicEntity** - Summoned evil beings
13. **AncientText** - Occult manuscripts
14. **BloodSacrifice** - Ritual offerings
15. **SealedRoom** - Containment chambers

## Complex Relationships

- CursedObject → Curse → Hex
- DarkRitual ↔ Artifact (many-to-many)
- Witch → multiple relationships (Rituals, Spells, Hexes)
- MuseumVault → SealedRoom → DemonicEntity
- BloodSacrifice → Victim & DarkRitual

## Usage

```bash
dotnet build
```

Test with efvis to visualize complex entity relationships.