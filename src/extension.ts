import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { exec } from 'child_process';
import { promisify } from 'util';

const execAsync = promisify(exec);

export function activate(context: vscode.ExtensionContext) {
    console.log('efvis extension activated');

    // Check and prepare analyzer on activation
    prepareAnalyzer(context).then(
        () => console.log('Analyzer ready'),
        (err) => console.error('Analyzer preparation failed:', err)
    );

    // Register the efvis.show command
    const disposable = vscode.commands.registerCommand('efvis.show', async () => {
        await showEfvisWebview(context);
    });

    context.subscriptions.push(disposable);
}

async function prepareAnalyzer(context: vscode.ExtensionContext): Promise<boolean> {
    try {
        // Check if .NET SDK is installed
        const { stdout } = await execAsync('dotnet --version');
        console.log('Found .NET SDK:', stdout.trim());

        const analyzerDir = path.join(context.extensionPath, 'src', 'analyzer');
        const analyzerDll = path.join(analyzerDir, 'bin', 'Release', 'net9.0', 'analyzer.dll');

        // Check if analyzer is already compiled
        if (fs.existsSync(analyzerDll)) {
            console.log('Analyzer already compiled');
            return true;
        }

        // Compile analyzer
        return await vscode.window.withProgress({
            location: vscode.ProgressLocation.Notification,
            title: "efvis: Compiling analyzer...",
            cancellable: false
        }, async (progress) => {
            try {
                console.log('Compiling analyzer in:', analyzerDir);
                const { stderr } = await execAsync('dotnet build -c Release', { 
                    cwd: analyzerDir 
                });
                
                if (stderr) {
                    console.warn('Build warnings:', stderr);
                }
                
                return fs.existsSync(analyzerDll);
            } catch (error) {
                console.error('Failed to compile analyzer:', error);
                return false;
            }
        });

    } catch (error) {
        vscode.window.showErrorMessage(
            'efvis requires .NET SDK 6.0 or later. Please install it and restart VS Code.',
            'Download .NET'
        ).then(selection => {
            if (selection === 'Download .NET') {
                vscode.env.openExternal(vscode.Uri.parse('https://dotnet.microsoft.com/download'));
            }
        });
        return false;
    }
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

        // Run analyzer on each project
        const analyzerPath = path.join(context.extensionPath, 'src', 'analyzer', 'bin', 'Release', 'net9.0', 'analyzer.dll');
        
        // Check if analyzer exists
        if (!fs.existsSync(analyzerPath)) {
            logs.push(`❌ Analyzer not found at: ${analyzerPath}`);
            logs.push(`Extension path: ${context.extensionPath}`);
            webview.postMessage({
                type: 'analysisResult',
                data: {
                    contexts: [],
                    logs: logs
                }
            });
            return;
        }
        
        logs.push(`✓ Analyzer found at: ${analyzerPath}`);
        
        const allContexts = [];
        
        for (const csproj of csprojFiles) {
            const projectPath = csproj.fsPath;
            const projectName = path.basename(projectPath);
            logs.push(`Analyzing ${projectName}...`);

            try {
                const command = `dotnet "${analyzerPath}" --project "${projectPath}"`;
                logs.push(`Running: ${command}`);
                
                const { stdout, stderr } = await execAsync(
                    command,
                    { maxBuffer: 1024 * 1024 * 10 } // 10MB buffer
                );
                
                if (stderr) {
                    logs.push(`Analyzer stderr: ${stderr}`);
                }
                
                logs.push(`Raw output length: ${stdout.length} chars`);
                
                // Try to parse JSON
                try {
                    const result = JSON.parse(stdout);
                    logs.push(`Parsed JSON successfully`);
                    
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