# Changelog

All notable changes to this project will be documented in this file.

## [0.2.0] - 2025-01-03

### Fixed
- Marketplace installation - analyzer now compiles in writable globalStorage location
- Permission errors when installed from VS Code marketplace
- Duplicate compilation attempts with proper async handling

### Added
- Automatic version management - recompiles analyzer when extension updates
- Better progress indicators during compilation and analysis
- Detailed logs in Output panel (View → Output → efvis)

### Changed
- Analyzer compiles on first use instead of extension activation
- Improved error messages and recovery from corrupted analyzer

## [0.1.0] - 2025-01-03
- First release of Entity Framework visualizer

