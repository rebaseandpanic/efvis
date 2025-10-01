# Haunted House Registry - Sample DbContext

**5 Entities** - Simple haunted property tracking system

## Entities

1. **HauntedHouse** - Properties with paranormal activity
2. **Ghost** - Spirits haunting the properties
3. **ParanormalActivity** - Recorded supernatural events
4. **Exorcist** - Professionals who investigate/cleanse
5. **Investigation** - Paranormal investigation records

## Relationships

- HauntedHouse 1:N Ghost
- HauntedHouse 1:N ParanormalActivity
- HauntedHouse 1:N Investigation
- Ghost 1:N ParanormalActivity
- Exorcist 1:N Investigation
- Investigation 1:N ParanormalActivity

## Usage

```bash
dotnet build
```

Then use with efvis extension to visualize the DbContext.