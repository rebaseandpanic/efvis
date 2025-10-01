# Development Guide

## Requirements

- VS Code 1.74.0+
- .NET SDK 6.0+ (for analyzer compilation on first run)
- Node.js 16+ (for building)

## Dev Container

Project includes `.devcontainer` for consistent development environment with:

- **Claude Code extension** pre-installed for AI-assisted development
- **Port 5000** forwarded for efvis testing
- **Auto port forwarding disabled** to avoid conflicts
- **Firewall disabled** for easier development (see `devcontainer.json` line 72)

**⚠️ IMPORTANT**: Firewall is disabled in dev container for easier development. See `devcontainer.json` line 72.

## Building from Source

### Prerequisites

Install dependencies:
```bash
npm install -g vsce  # VS Code Extension CLI
```

### One-Command Build

```bash
npm run build:vsix
```

This will:
1. Install all dependencies (extension + webview)
2. Build the React webview app
3. Build the TypeScript extension
4. Create `efvis-1.0.0.vsix` package

### Step-by-Step Build

If you prefer manual control:

```bash
# 1. Install dependencies
npm run build:deps

# 2. Build webview
npm run build:webview

# 3. Build extension
npm run compile

# 4. Create VSIX package
npm run package
```

### Development Mode

For active development with auto-rebuild:

```bash
# Terminal 1: Watch extension TypeScript
npm run watch

# Terminal 2: Watch webview React app
cd src/webview && npm run watch
```

## Project Architecture

- **VS Code Extension** (`src/extension.ts`) - Manages webview, spawns analyzer
- **C# Analyzer** (`src/analyzer/`) - Roslyn-based EF model extraction
- **React Webview** (`src/webview/`) - Interactive diagram visualization

## Troubleshooting

### .NET SDK Not Found
- Install .NET SDK 6.0+: https://dotnet.microsoft.com/download
- Restart VS Code after installation

### Extension Compilation Fails
```bash
npm run build:deps  # Reinstall dependencies
npm run compile     # Try manual build
```

### Webview Not Loading
```bash
npm run build:webview  # Rebuild React app
```

Check Developer Console: `Help → Toggle Developer Tools`

## GitHub Actions

The project includes automated releases via GitHub Actions:

1. Create and push a git tag:
```bash
git tag v1.2.0
git push origin v1.2.0
```

2. GitHub Actions will automatically:
   - Build the extension
   - Create `efvis-1.2.0.vsix` (version from tag)
   - Create GitHub Release with the VSIX file

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test locally with `npm run build:vsix`
5. Submit a pull request