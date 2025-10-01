# EFVis Sample Projects

This folder contains sample Entity Framework DbContext projects to test and demonstrate the efvis VS Code extension.

## 📊 Sample Projects

| Project | Entities | Complexity | Theme |
|---------|----------|------------|-------|
| **01-HauntedHouse** | 5 | Simple | Paranormal property tracking |
| **02-CursedMuseum** | 15 | Medium | Dark occult artifact management |
| **03-ZombieApocalypse** | 30 | Complex | Post-apocalyptic survival system |
| **04-CultManagement** | 50 | Very Complex | Cult operations and control |
| **05-HellCorporation** | 100 | Extreme | Hell as corporate ERP system |

## 🎯 Purpose

These samples demonstrate:
- **Small diagrams** (5 entities) - Simple relationships
- **Medium diagrams** (15 entities) - Multiple relationship types
- **Large diagrams** (30 entities) - Complex interconnected systems
- **Very large diagrams** (50 entities) - Enterprise-level modeling
- **Extreme diagrams** (100 entities) - Stress testing visualization

## 🚀 Quick Start

### Build a Sample

```bash
cd 01-HauntedHouse
dotnet build
```

### Visualize with efvis

1. Open the sample project folder in VS Code
2. Press `Ctrl+Shift+P` (or `Cmd+Shift+P` on Mac)
3. Run command: `efvis: Show EF Diagram`
4. Select a DbContext to visualize
5. Choose between **ReactFlow** or **mxGraph** engines

## 📁 Project Structure

Each sample contains:
```
XX-ProjectName/
├── ProjectName.csproj          # .NET project file
├── Models/                     # Entity classes
│   ├── Entity1.cs
│   ├── Entity2.cs
│   └── ...
├── Data/
│   └── XxxDbContext.cs         # EF DbContext
└── README.md                   # Project documentation
```

## 🔧 Requirements

- **.NET SDK 8.0** or higher
- **VS Code** with efvis extension installed
- **Entity Framework Core 8.0**

## 🎨 Themes

All samples use **dark/horror themes** to make testing more interesting:
- 👻 **Haunted House Registry** - Ghost tracking
- 🔮 **Cursed Museum** - Dangerous artifacts
- 🧟 **Zombie Apocalypse** - Survival horror
- 🕯️ **Cult Management** - Dark organization control
- 😈 **Hell Corporation** - Infernal business operations

## 📝 Notes

- Samples are for **testing and demonstration only**
- All code is in **English**
- Each project is **self-contained** and buildable
- Complex relationships demonstrate **advanced EF features**
- Stress test the visualizer with **large entity counts**

## 🧪 Testing Scenarios

Use these samples to test:
- ✅ Small vs large diagram rendering
- ✅ One-to-many relationships
- ✅ Many-to-many relationships
- ✅ Self-referencing entities
- ✅ Complex navigation properties
- ✅ Layout algorithm performance
- ✅ ReactFlow vs mxGraph comparison
- ✅ Drag & drop position persistence

Enjoy visualizing Entity Framework models! 🎉