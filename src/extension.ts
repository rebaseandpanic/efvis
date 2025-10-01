import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { exec } from 'child_process';
import { promisify } from 'util';

const execAsync = promisify(exec);

// Global flag to prevent concurrent compilation
let isCompiling = false;
let compilationPromise: Promise<boolean> | null = null;

export function activate(context: vscode.ExtensionContext) {
    console.log('efvis extension activated');

    // Register the efvis.show command
    const disposable = vscode.commands.registerCommand('efvis.show', async () => {
        await showEfvisWebview(context);
    });

    context.subscriptions.push(disposable);
}

async function prepareAnalyzer(context: vscode.ExtensionContext): Promise<boolean> {
    // Check if compilation is already in progress
    if (isCompiling && compilationPromise) {
        console.log('Compilation already in progress, waiting for it to complete...');
        return await compilationPromise;
    }

    isCompiling = true;

    // Create promise that will be resolved when compilation finishes
    compilationPromise = (async () => {
    const outputChannel = vscode.window.createOutputChannel('efvis');

    try {
        // Check if .NET SDK is installed
        outputChannel.appendLine('=== Checking for .NET SDK ===');
        outputChannel.appendLine('Running: dotnet --version');
        const { stdout } = await execAsync('dotnet --version');
        const dotnetVersion = stdout.trim();
        outputChannel.appendLine(`✓ Found .NET SDK: ${dotnetVersion}`);
        console.log('Found .NET SDK:', dotnetVersion);

        const sourceAnalyzerDir = path.join(context.extensionPath, 'src', 'analyzer');
        const workDir = path.join(context.globalStorageUri.fsPath, 'analyzer-build');
        const outputDir = path.join(context.globalStorageUri.fsPath, 'analyzer');
        const targetDll = path.join(outputDir, 'analyzer.dll');

        outputChannel.appendLine('');
        outputChannel.appendLine('=== Paths ===');
        outputChannel.appendLine(`Source: ${sourceAnalyzerDir}`);
        outputChannel.appendLine(`Work dir: ${workDir}`);
        outputChannel.appendLine(`Output dir: ${outputDir}`);
        outputChannel.appendLine(`Target DLL: ${targetDll}`);

        // Check if analyzer is already compiled
        if (fs.existsSync(targetDll)) {
            outputChannel.appendLine('');
            outputChannel.appendLine('=== Version Check ===');

            // Get current analyzer version
            try {
                const { stdout: analyzerVersion } = await execAsync(`dotnet "${targetDll}" --version`);
                const currentVersion = analyzerVersion.trim();
                const extensionVersion = context.extension.packageJSON.version || "1.0.0";

                outputChannel.appendLine(`Current analyzer version: ${currentVersion}`);
                outputChannel.appendLine(`Extension version: ${extensionVersion}`);

                if (currentVersion === extensionVersion) {
                    outputChannel.appendLine('✓ Versions match, using existing analyzer');
                    console.log('Analyzer version matches, using existing');
                    return true;
                } else {
                    outputChannel.appendLine('⚠️ Version mismatch detected');
                    outputChannel.appendLine('Removing old analyzer...');

                    // Delete old analyzer directories
                    if (fs.existsSync(outputDir)) {
                        fs.rmSync(outputDir, { recursive: true, force: true });
                        outputChannel.appendLine(`Deleted: ${outputDir}`);
                    }
                    if (fs.existsSync(workDir)) {
                        fs.rmSync(workDir, { recursive: true, force: true });
                        outputChannel.appendLine(`Deleted: ${workDir}`);
                    }

                    outputChannel.appendLine('Old analyzer removed, will recompile');
                }
            } catch (error: any) {
                outputChannel.appendLine(`⚠️ Could not check analyzer version: ${error.message}`);

                // If version check fails, the analyzer might be corrupted
                // Delete and recompile
                if (error.message.includes('An item with the same key') ||
                    error.message.includes('--version')) {
                    outputChannel.appendLine('⚠️ Analyzer appears corrupted, removing...');

                    // Delete old analyzer directories
                    if (fs.existsSync(outputDir)) {
                        fs.rmSync(outputDir, { recursive: true, force: true });
                        outputChannel.appendLine(`Deleted: ${outputDir}`);
                    }
                    if (fs.existsSync(workDir)) {
                        fs.rmSync(workDir, { recursive: true, force: true });
                        outputChannel.appendLine(`Deleted: ${workDir}`);
                    }

                    outputChannel.appendLine('Will recompile analyzer');
                } else {
                    outputChannel.appendLine('Will try to use existing analyzer');
                    return true;
                }
            }
        }

        outputChannel.appendLine('');
        outputChannel.appendLine('=== Preparing directories ===');

        // Ensure work directories exist
        if (!fs.existsSync(workDir)) {
            outputChannel.appendLine(`Creating work directory: ${workDir}`);
            fs.mkdirSync(workDir, { recursive: true });
        } else {
            outputChannel.appendLine(`Work directory exists: ${workDir}`);
        }

        if (!fs.existsSync(outputDir)) {
            outputChannel.appendLine(`Creating output directory: ${outputDir}`);
            fs.mkdirSync(outputDir, { recursive: true });
        } else {
            outputChannel.appendLine(`Output directory exists: ${outputDir}`);
        }

        // Copy source files to writable location
        outputChannel.appendLine('');
        outputChannel.appendLine('=== Copying source files ===');
        outputChannel.appendLine(`From: ${sourceAnalyzerDir}`);
        outputChannel.appendLine(`To: ${workDir}`);

        const filesToCopy = fs.readdirSync(sourceAnalyzerDir);
        outputChannel.appendLine(`Found ${filesToCopy.length} items to copy`);

        let copiedCount = 0;
        for (const file of filesToCopy) {
            const sourcePath = path.join(sourceAnalyzerDir, file);
            const destPath = path.join(workDir, file);

            if (fs.statSync(sourcePath).isFile()) {
                fs.copyFileSync(sourcePath, destPath);
                outputChannel.appendLine(`  ✓ ${file}`);
                copiedCount++;
            }
        }
        outputChannel.appendLine(`Copied ${copiedCount} files`);
        outputChannel.show();

        // Compile analyzer
        return await vscode.window.withProgress({
            location: vscode.ProgressLocation.Notification,
            title: "efvis: Compiling analyzer...",
            cancellable: false
        }, async () => {
            try {
                outputChannel.appendLine('');
                outputChannel.appendLine('=== Compiling analyzer ===');
                outputChannel.appendLine(`Working directory: ${workDir}`);
                outputChannel.appendLine(`Output directory: ${outputDir}`);

                // Get extension version
                const extensionVersion = context.extension.packageJSON.version || "1.0.0";
                outputChannel.appendLine(`Extension version: ${extensionVersion}`);

                // Build from writable location, output to writable location with version
                const publishCommand = outputDir.includes(' ')
                    ? `dotnet publish -c Release -o "${outputDir}" --self-contained false /p:Version=${extensionVersion}`
                    : `dotnet publish -c Release -o ${outputDir} --self-contained false /p:Version=${extensionVersion}`;

                outputChannel.appendLine('');
                outputChannel.appendLine(`Running command: ${publishCommand}`);
                outputChannel.appendLine('Please wait...');

                const { stdout, stderr } = await execAsync(publishCommand, {
                    cwd: workDir
                });

                outputChannel.appendLine('');
                if (stdout) {
                    outputChannel.appendLine('=== Build output ===');
                    outputChannel.appendLine(stdout);
                }

                if (stderr) {
                    outputChannel.appendLine('');
                    outputChannel.appendLine('=== Build warnings/errors ===');
                    outputChannel.appendLine(stderr);
                }

                // Check if publish succeeded
                outputChannel.appendLine('');
                outputChannel.appendLine('=== Verifying output ===');
                outputChannel.appendLine(`Checking for: ${targetDll}`);

                if (fs.existsSync(targetDll)) {
                    outputChannel.appendLine('✓ Analyzer DLL found');
                    outputChannel.appendLine('');
                    outputChannel.appendLine('=== SUCCESS ===');
                    outputChannel.appendLine('Analyzer compiled and published successfully!');

                    vscode.window.showInformationMessage('efvis analyzer compiled successfully!');
                    isCompiling = false;
                    compilationPromise = null;
                    return true;
                }

                outputChannel.appendLine('✗ Analyzer DLL NOT found');
                outputChannel.appendLine('');
                outputChannel.appendLine('=== FAILURE ===');
                outputChannel.appendLine('Compilation may have failed. Check output above.');

                vscode.window.showErrorMessage('Failed to compile analyzer. Check Output panel.', 'Show Output').then(selection => {
                    if (selection === 'Show Output') {
                        outputChannel.show();
                    }
                });

                isCompiling = false;
                compilationPromise = null;
                return false;
            } catch (error: any) {
                outputChannel.appendLine('');
                outputChannel.appendLine('=== EXCEPTION ===');
                outputChannel.appendLine(`Error: ${error.message}`);
                outputChannel.appendLine('');
                outputChannel.appendLine('Full error details:');
                outputChannel.appendLine(JSON.stringify(error, null, 2));

                console.error('Failed to compile analyzer:', error);

                vscode.window.showErrorMessage(
                    `Failed to compile analyzer: ${error.message}`,
                    'Show Output'
                ).then(selection => {
                    if (selection === 'Show Output') {
                        outputChannel.show();
                    }
                });

                isCompiling = false;
                compilationPromise = null;
                return false;
            }
        });

    } catch (error: any) {
        outputChannel.appendLine('');
        outputChannel.appendLine('=== FATAL ERROR ===');
        outputChannel.appendLine('.NET SDK check failed');
        outputChannel.appendLine(`Error: ${error.message}`);
        outputChannel.show();

        vscode.window.showErrorMessage(
            'efvis requires .NET SDK 6.0 or later. Please install it and restart VS Code.',
            'Download .NET',
            'Show Output'
        ).then(selection => {
            if (selection === 'Download .NET') {
                vscode.env.openExternal(vscode.Uri.parse('https://dotnet.microsoft.com/download'));
            } else if (selection === 'Show Output') {
                outputChannel.show();
            }
        });
        isCompiling = false;
        compilationPromise = null;
        return false;
    }
    })();

    return await compilationPromise;
}

async function showEfvisWebview(context: vscode.ExtensionContext) {
    // Create webview panel
    const panel = vscode.window.createWebviewPanel(
        'efvis',
        'Entity Framework Visualizer',
        vscode.ViewColumn.One,
        {
            enableScripts: true,
            retainContextWhenHidden: true,
            localResourceRoots: [
                vscode.Uri.file(path.join(context.extensionPath, 'src', 'webview', 'dist')),
                vscode.Uri.file(path.join(context.extensionPath, 'out'))
            ]
        }
    );

    // Set webview HTML content
    panel.webview.html = getWebviewContent(panel.webview, context.extensionPath);

    // Handle messages from webview
    panel.webview.onDidReceiveMessage(
        async message => {
            switch (message.command) {
                case 'analyze':
                    await analyzeWorkspace(panel.webview, context);
                    break;
                case 'saveLayout':
                    await saveReactFlowLayout(message.data);
                    break;
                case 'getLayout':
                    await loadReactFlowLayoutForContext(panel.webview, message.context);
                    break;
                case 'saveMxGraphLayout':
                    await saveMxGraphLayout(message.data);
                    break;
                case 'getMxGraphLayout':
                    await loadMxGraphLayoutForContext(panel.webview, message.context);
                    break;
            }
        },
        undefined,
        context.subscriptions
    );

    // Trigger initial analysis
    setTimeout(() => {
        analyzeWorkspace(panel.webview, context);
    }, 500);
}

async function analyzeWorkspace(webview: vscode.Webview, context: vscode.ExtensionContext) {
    try {
        // Find all .csproj files
        const csprojFiles = await vscode.workspace.findFiles('**/*.csproj', '**/node_modules/**');
        
        if (csprojFiles.length === 0) {
            webview.postMessage({
                type: 'analysisResult',
                data: {
                    contexts: [],
                    logs: ['No C# projects found in workspace']
                }
            });
            return;
        }

        const logs: string[] = [];
        logs.push(`Found ${csprojFiles.length} C# project(s)`);

        // Use analyzer from globalStorage (writable location)
        const analyzerPath = path.join(context.globalStorageUri.fsPath, 'analyzer', 'analyzer.dll');

        // Check if analyzer exists, if not - try to compile
        if (!fs.existsSync(analyzerPath)) {
            const extensionVersion = context.extension.packageJSON.version || "1.0.0";

            // Send compilation status to webview
            webview.postMessage({
                type: 'analysisResult',
                data: {
                    contexts: [],
                    logs: [
                        ...logs,
                        '━━━━━━━━━━━━━━━━━━━━',
                        '🔨 COMPILING ANALYZER...',
                        `📦 Version ${extensionVersion}`,
                        'This may take a moment on first run',
                        '━━━━━━━━━━━━━━━━━━━━'
                    ]
                }
            });

            const compiled = await prepareAnalyzer(context);

            if (!compiled) {
                logs.push(`❌ Failed to compile analyzer`);
                logs.push(`Make sure .NET SDK 6.0+ is installed`);
                webview.postMessage({
                    type: 'analysisResult',
                    data: {
                        contexts: [],
                        logs: logs
                    }
                });
                return;
            }

            logs.push(`✅ Analyzer compiled successfully (version ${extensionVersion})`);
        }

        // Show analysis status
        logs.push('');
        logs.push('━━━━━━━━━━━━━━━━━━━━');
        logs.push('🔍 ANALYZING PROJECTS...');
        logs.push('━━━━━━━━━━━━━━━━━━━━');
        
        const allContexts = [];
        
        for (let i = 0; i < csprojFiles.length; i++) {
            const csproj = csprojFiles[i];
            const projectPath = csproj.fsPath;
            const projectName = path.basename(projectPath);

            logs.push('');
            logs.push(`[${i + 1}/${csprojFiles.length}] Analyzing ${projectName}...`);

            try {
                const { stdout, stderr } = await execAsync(
                    `dotnet "${analyzerPath}" --project "${projectPath}"`,
                    { maxBuffer: 1024 * 1024 * 10 } // 10MB buffer
                );
                
                if (stderr) {
                    console.warn(`Analyzer stderr: ${stderr}`);
                }

                // Try to parse JSON
                try {
                    const result = JSON.parse(stdout);
                    
                    if (result.Contexts && result.Contexts.length > 0) {
                        allContexts.push(...result.Contexts);
                        logs.push(`✓ Found ${result.Contexts.length} DbContext(s) in ${projectName}`);
                        
                        // Log context names
                        result.Contexts.forEach((ctx: any) => {
                            logs.push(`  - ${ctx.Name} (${ctx.Entities.length} entities)`);
                        });
                    } else {
                        logs.push(`- No DbContext found in ${projectName}`);
                        logs.push(`Result: ${JSON.stringify(result)}`);
                    }
                } catch (parseError: any) {
                    logs.push(`JSON parse error: ${parseError.message}`);
                    logs.push(`First 500 chars of output: ${stdout.substring(0, 500)}`);
                }
            } catch (error: any) {
                logs.push(`✗ Failed to analyze ${projectName}: ${error.message}`);
                if (error.stdout) {
                    logs.push(`Stdout: ${error.stdout.substring(0, 500)}`);
                }
                if (error.stderr) {
                    logs.push(`Stderr: ${error.stderr}`);
                }
            }
        }

        // Add final status
        logs.push('');
        logs.push('━━━━━━━━━━━━━━━━━━━━');
        if (allContexts.length > 0) {
            const totalEntities = allContexts.reduce((sum, ctx) =>
                sum + (ctx.Entities?.length || 0), 0);
            logs.push(`✅ ANALYSIS COMPLETE`);
            logs.push(`Found ${allContexts.length} DbContext(s) with ${totalEntities} total entities`);
        } else {
            logs.push('⚠️ No DbContexts found');
        }
        logs.push('━━━━━━━━━━━━━━━━━━━━');

        // Send results to webview
        webview.postMessage({
            type: 'analysisResult',
            data: {
                contexts: allContexts,
                logs: logs
            }
        });

    } catch (error: any) {
        webview.postMessage({
            type: 'error',
            message: `Analysis failed: ${error.message}`
        });
    }
}

async function saveReactFlowLayout(data: any) {
    const workspaceRoot = vscode.workspace.workspaceFolders?.[0]?.uri.fsPath;
    if (!workspaceRoot) {
        vscode.window.showWarningMessage('No workspace folder found');
        return;
    }

    // Create separate file per context
    const layoutPath = path.join(workspaceRoot, `.efvis-reactflow-layout.${data.context}.json`);

    try {
        if (data.layout === null || data.layout === undefined) {
            // Delete layout file if layout is null (reset)
            if (fs.existsSync(layoutPath)) {
                await fs.promises.unlink(layoutPath);
            }
        } else {
            // Save layout to context-specific file
            await fs.promises.writeFile(layoutPath, JSON.stringify(data.layout, null, 2));
        }
    } catch (error) {
        vscode.window.showErrorMessage(`Failed to save ReactFlow layout: ${error}`);
    }
}

async function loadReactFlowLayoutForContext(webview: vscode.Webview, contextName: string) {
    const workspaceRoot = vscode.workspace.workspaceFolders?.[0]?.uri.fsPath;
    if (!workspaceRoot) {
        return;
    }

    const layoutPath = path.join(workspaceRoot, `.efvis-reactflow-layout.${contextName}.json`);

    if (fs.existsSync(layoutPath)) {
        try {
            const layoutData = await fs.promises.readFile(layoutPath, 'utf8');
            
            // Check if file is not empty before parsing
            if (!layoutData || layoutData.trim() === '') {
                console.warn(`Layout file for ${contextName} is empty`);
                await webview.postMessage({
                    type: 'reactFlowLayoutData',
                    context: contextName,
                    data: null
                });
                return;
            }
            
            await webview.postMessage({
                type: 'reactFlowLayoutData',
                context: contextName,
                data: JSON.parse(layoutData)
            });
        } catch (error) {
            console.error(`Failed to load layout for ${contextName}:`, error);
        }
    } else {
        // Send null layout if file doesn't exist
        await webview.postMessage({
            type: 'reactFlowLayoutData',
            context: contextName,
            data: null
        });
    }
}

async function saveMxGraphLayout(data: any) {
    const workspaceRoot = vscode.workspace.workspaceFolders?.[0]?.uri.fsPath;
    if (!workspaceRoot) {
        vscode.window.showWarningMessage('No workspace folder found');
        return;
    }

    // Create separate file per context
    const layoutPath = path.join(workspaceRoot, `.efvis-mxgraph-layout.${data.context}.json`);

    try {
        if (data.layout === null || data.layout === undefined) {
            // Delete layout file if layout is null (reset)
            if (fs.existsSync(layoutPath)) {
                await fs.promises.unlink(layoutPath);
            }
        } else {
            // Save layout to context-specific file
            await fs.promises.writeFile(layoutPath, JSON.stringify(data.layout, null, 2));
        }
    } catch (error) {
        vscode.window.showErrorMessage(`Failed to save mxGraph layout: ${error}`);
    }
}

async function loadMxGraphLayoutForContext(webview: vscode.Webview, contextName: string) {
    const workspaceRoot = vscode.workspace.workspaceFolders?.[0]?.uri.fsPath;
    if (!workspaceRoot) {
        return;
    }

    const layoutPath = path.join(workspaceRoot, `.efvis-mxgraph-layout.${contextName}.json`);

    if (fs.existsSync(layoutPath)) {
        try {
            const layoutData = await fs.promises.readFile(layoutPath, 'utf8');
            await webview.postMessage({
                type: 'mxGraphLayoutData',
                context: contextName,
                data: JSON.parse(layoutData)
            });
        } catch (error) {
            console.error(`Failed to load layout for ${contextName}:`, error);
        }
    } else {
        // Send null layout if file doesn't exist
        await webview.postMessage({
            type: 'mxGraphLayoutData',
            context: contextName,
            data: null
        });
    }
}

function getWebviewContent(webview: vscode.Webview, extensionPath: string): string {
    const scriptUri = webview.asWebviewUri(
        vscode.Uri.file(path.join(extensionPath, 'src', 'webview', 'dist', 'bundle.js'))
    );
    const styleUri = webview.asWebviewUri(
        vscode.Uri.file(path.join(extensionPath, 'src', 'webview', 'dist', 'style.css'))
    );

    return `<!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Entity Framework Visualizer</title>
        <link href="${styleUri}" rel="stylesheet">
    </head>
    <body>
        <div id="root"></div>
        <script src="${scriptUri}"></script>
    </body>
    </html>`;
}

export function deactivate() {}