import React, { useState, useEffect, useCallback, useRef } from 'react';
import ReactFlow, {
  Node,
  Edge,
  Controls,
  Background,
  BackgroundVariant,
  useNodesState,
  useEdgesState,
  Connection,
  addEdge,
  Position,
  MarkerType,
  ConnectionMode
} from 'reactflow';
import dagre from 'dagre';
import ELK from 'elkjs/lib/elk.bundled.js';
import * as d3Force from 'd3-force';
import EntityNode from './components/EntityNode';
import CustomEdge from './components/CustomEdge';
import MxGraphView from './components/MxGraphView';
import { VSCodeAPI, DbContextInfo, EntityInfo } from './types';
import './styles/app.css';

const elk = new ELK();

declare const acquireVsCodeApi: () => VSCodeAPI;
const vscode = acquireVsCodeApi();

const nodeTypes = {
  entity: EntityNode,
};

const edgeTypes = {
  custom: CustomEdge,
};

function App() {
  const [nodes, setNodes, onNodesChange] = useNodesState([]);
  const [edges, setEdges, onEdgesChange] = useEdgesState([]);
  const [contexts, setContexts] = useState<DbContextInfo[]>([]);
  const [selectedContext, setSelectedContext] = useState<string | null>(null);
  const [visualizationEngine, setVisualizationEngine] = useState<'reactflow' | 'mxgraph'>('reactflow');
  const [logs, setLogs] = useState<string[]>([]);
  const [edgeStyle, setEdgeStyle] = useState<string>('smoothstep');
  const [edgeAnimated, setEdgeAnimated] = useState<boolean>(false);
  const [hasLoadedLayout, setHasLoadedLayout] = useState<boolean>(false);
  
  // Layout algorithm selection
  const [layoutAlgorithm, setLayoutAlgorithm] = useState<string>('dagre');
  
  // Dagre layout settings
  const [layoutDirection, setLayoutDirection] = useState<string>('TB');
  const [nodeSpacing, setNodeSpacing] = useState<number>(100);
  const [rankSpacing, setRankSpacing] = useState<number>(150);
  const [edgeSpacing, setEdgeSpacing] = useState<number>(10);
  const [layoutAlign, setLayoutAlign] = useState<string>('UL');
  const [ranker, setRanker] = useState<string>('network-simplex');
  const [acyclicer, setAcyclicer] = useState<string>('');
  const [marginX, setMarginX] = useState<number>(0);
  const [marginY, setMarginY] = useState<number>(0);
  
  // ELK layout settings
  const [elkAlgorithm, setElkAlgorithm] = useState<string>('layered');
  const [elkDirection, setElkDirection] = useState<string>('DOWN');
  const [elkNodeSpacing, setElkNodeSpacing] = useState<number>(50);
  const [elkLayerSpacing, setElkLayerSpacing] = useState<number>(50);
  const [elkEdgeSpacing, setElkEdgeSpacing] = useState<number>(20);
  const [elkHierarchyHandling, setElkHierarchyHandling] = useState<string>('INCLUDE_CHILDREN');
  const [elkNodePlacement, setElkNodePlacement] = useState<string>('BRANDES_KOEPF');
  const [elkCrossingMinimization, setElkCrossingMinimization] = useState<string>('LAYER_SWEEP');
  const [elkInteractive, setElkInteractive] = useState<boolean>(false);
  const [elkPortConstraints, setElkPortConstraints] = useState<string>('UNDEFINED');
  
  // ELK edge routing settings
  const [elkEdgeRouting, setElkEdgeRouting] = useState<string>('UNDEFINED');
  const [elkSplineMode, setElkSplineMode] = useState<string>('CONSERVATIVE');
  const [elkPolylineSloped, setElkPolylineSloped] = useState<number>(2.0);
  const [elkEdgeNodeSpacing, setElkEdgeNodeSpacing] = useState<number>(15);
  const [elkEdgeEdgeSpacing, setElkEdgeEdgeSpacing] = useState<number>(10);
  const [elkEdgeLabelSpacing, setElkEdgeLabelSpacing] = useState<number>(5);
  
  // D3-Force layout settings
  const [forceStrength, setForceStrength] = useState<number>(-3000);
  const [forceDistance, setForceDistance] = useState<number>(300);
  const [forceIterations, setForceIterations] = useState<number>(100);
  const [forceCollideRadius, setForceCollideRadius] = useState<number>(150);
  const [forceCollideStrength, setForceCollideStrength] = useState<number>(0.7);
  const [forceLinkStrength, setForceLinkStrength] = useState<number>(0.5);
  const [forceXStrength, setForceXStrength] = useState<number>(0.05);
  const [forceYStrength, setForceYStrength] = useState<number>(0.05);
  const [forceChargeDistanceMax, setForceChargeDistanceMax] = useState<number>(1000);
  
  // mxGraph settings
  const [mxLayoutDirection, setMxLayoutDirection] = useState<string>('NORTH');
  const [mxIntraCellSpacing, setMxIntraCellSpacing] = useState<number>(50);
  const [mxInterRankCellSpacing, setMxInterRankCellSpacing] = useState<number>(100);
  const [mxParallelEdgeSpacing, setMxParallelEdgeSpacing] = useState<number>(10);
  const [mxFineTuning, setMxFineTuning] = useState<boolean>(true);
  const [mxTightenToSource, setMxTightenToSource] = useState<boolean>(true);
  const [mxEdgeStyle, setMxEdgeStyle] = useState<string>('orthogonal');
  const [mxMultigraph, setMxMultigraph] = useState<boolean>(false);
  const [mxGridEnabled, setMxGridEnabled] = useState<boolean>(true);
  const [mxGridSize, setMxGridSize] = useState<number>(10);
  const [mxEntityNameFontSize, setMxEntityNameFontSize] = useState<number>(12);
  const [mxPropertyFontSize, setMxPropertyFontSize] = useState<number>(11);
  const [mxNavigationPropertyFontSize, setMxNavigationPropertyFontSize] = useState<number>(11);
  const [currentMxGraphLayout, setCurrentMxGraphLayout] = useState<any>(null);
  const [mxGraphKey, setMxGraphKey] = useState<number>(0);
  const [mxSettingsJustSaved, setMxSettingsJustSaved] = useState<boolean>(false);

  // UI state
  const [showSettings, setShowSettings] = useState<boolean>(false);
  const [showEntitiesPanel, setShowEntitiesPanel] = useState<boolean>(false);
  const [entitiesSearchQuery, setEntitiesSearchQuery] = useState<string>('');
  const [isAnalyzing, setIsAnalyzing] = useState<boolean>(true);
  const [showLogs, setShowLogs] = useState<boolean>(true);
  const [showSidebar, setShowSidebar] = useState<boolean>(true);
  const [showDbContexts, setShowDbContexts] = useState<boolean>(true);
  const [showGraphControls, setShowGraphControls] = useState<boolean>(true);
  
  // Display settings
  const [showEdges, setShowEdges] = useState<boolean>(true);
  const [connectionMode, setConnectionMode] = useState<string>('loose');
  const [pathType, setPathType] = useState<string>('smoothstep');
  const [showHandles, setShowHandles] = useState<boolean>(true);

  useEffect(() => {
    if (contexts.length > 0) {
      setShowLogs(false);
    }
  }, [contexts]);

  // Initial requests on mount
  useEffect(() => {
    vscode.postMessage({ command: 'analyze' });
  }, []);

  // Message handler with selectedContext dependency
  useEffect(() => {
    const handleMessage = (event: MessageEvent) => {
      const message = event.data;

      switch (message.type) {
        case 'analysisResult':
          setContexts(message.data.contexts || []);
          setLogs(message.data.logs || []);
          setIsAnalyzing(false);
          break;

        case 'reactFlowLayoutData':
          console.log('[App] Received ReactFlow layout for context:', message.context, 'Current context:', selectedContext, 'Layout:', message.data);
          // Apply layout only if it's for the current context
          if (message.context === selectedContext) {
            if (message.data) {
              console.log('[App] Applying ReactFlow layout');
              applyReactFlowLayout(message.data);
            } else {
              console.log('[App] No saved layout, will use default');
              setHasLoadedLayout(false);
            }
          }
          break;

        case 'mxGraphLayoutData':
          console.log('[App] Received layout for context:', message.context, 'Current context:', selectedContext, 'Layout:', message.data);
          // Update layout only if it's for the current context
          if (message.context === selectedContext) {
            console.log('[App] Setting currentMxGraphLayout');

            // Apply saved settings if they exist
            if (message.data && message.data.settings) {
              console.log('[App] Applying saved settings from layout file:', message.data.settings);
              const settings = message.data.settings;

              // Apply all settings
              if (settings.edgeStyle !== undefined) setMxEdgeStyle(settings.edgeStyle);
              if (settings.entityNameFontSize !== undefined) setMxEntityNameFontSize(settings.entityNameFontSize);
              if (settings.propertyFontSize !== undefined) setMxPropertyFontSize(settings.propertyFontSize);
              if (settings.navigationPropertyFontSize !== undefined) setMxNavigationPropertyFontSize(settings.navigationPropertyFontSize);
              if (settings.layoutDirection !== undefined) setMxLayoutDirection(settings.layoutDirection);
              if (settings.intraCellSpacing !== undefined) setMxIntraCellSpacing(settings.intraCellSpacing);
              if (settings.interRankCellSpacing !== undefined) setMxInterRankCellSpacing(settings.interRankCellSpacing);
              if (settings.parallelEdgeSpacing !== undefined) setMxParallelEdgeSpacing(settings.parallelEdgeSpacing);
              if (settings.fineTuning !== undefined) setMxFineTuning(settings.fineTuning);
              if (settings.tightenToSource !== undefined) setMxTightenToSource(settings.tightenToSource);
              if (settings.multigraph !== undefined) setMxMultigraph(settings.multigraph);
              if (settings.gridEnabled !== undefined) setMxGridEnabled(settings.gridEnabled);
              if (settings.gridSize !== undefined) setMxGridSize(settings.gridSize);
            }

            setCurrentMxGraphLayout(message.data);
          } else {
            console.log('[App] Ignoring layout - context mismatch');
          }
          break;

        case 'error':
          setLogs(prev => [...prev, `Error: ${message.message}`]);
          setIsAnalyzing(false);
          break;
      }
    };

    window.addEventListener('message', handleMessage);

    return () => {
      window.removeEventListener('message', handleMessage);
    };
  }, [selectedContext]);

  // Load layouts when selectedContext changes
  useEffect(() => {
    if (selectedContext) {
      console.log('[App] Loading layouts for context:', selectedContext);
      
      // Load mxGraph layout
      vscode.postMessage({
        command: 'getMxGraphLayout',
        context: selectedContext
      });
      
      // Load ReactFlow layout
      vscode.postMessage({
        command: 'getLayout',
        context: selectedContext
      });
    }
  }, [selectedContext]);

  // Save layout AND settings when opening settings panel, restore when closing
  const prevShowSettings = useRef(false);
  const savedStateRef = useRef<any>(null);
  const settingsSavedToFile = useRef(false);

  // Track if we just changed showSettings vs other params changed
  useEffect(() => {
    const showSettingsChanged = showSettings !== prevShowSettings.current;

    if (showSettingsChanged && showSettings && visualizationEngine === 'mxgraph') {
      // Save current state to restore if user cancels
      console.log('[App] Saving state before opening settings');
      settingsSavedToFile.current = false; // Reset flag
      savedStateRef.current = {
        layout: currentMxGraphLayout,
        layoutDirection: mxLayoutDirection,
        intraCellSpacing: mxIntraCellSpacing,
        interRankCellSpacing: mxInterRankCellSpacing,
        parallelEdgeSpacing: mxParallelEdgeSpacing,
        fineTuning: mxFineTuning,
        tightenToSource: mxTightenToSource,
        edgeStyle: mxEdgeStyle,
        multigraph: mxMultigraph,
        gridEnabled: mxGridEnabled,
        gridSize: mxGridSize
      };
    } else if (showSettingsChanged && !showSettings && visualizationEngine === 'mxgraph' && savedStateRef.current !== null) {
      // Only restore if settings were NOT saved to file
      if (!settingsSavedToFile.current) {
        console.log('[App] Restoring state after closing settings (not saved)');
        const saved = savedStateRef.current;
        setMxLayoutDirection(saved.layoutDirection);
        setMxIntraCellSpacing(saved.intraCellSpacing);
        setMxInterRankCellSpacing(saved.interRankCellSpacing);
        setMxParallelEdgeSpacing(saved.parallelEdgeSpacing);
        setMxFineTuning(saved.fineTuning);
        setMxTightenToSource(saved.tightenToSource);
        setMxEdgeStyle(saved.edgeStyle);
        setMxMultigraph(saved.multigraph);
        setMxGridEnabled(saved.gridEnabled);
        setMxGridSize(saved.gridSize);
        // Layout will be restored after graph rebuilds with original settings
        setTimeout(() => {
          setCurrentMxGraphLayout(saved.layout);
          savedStateRef.current = null;
        }, 200);
      } else {
        console.log('[App] Settings were saved to file, not restoring');
        savedStateRef.current = null;
      }
    }

    if (showSettingsChanged) {
      prevShowSettings.current = showSettings;
    }
  }, [showSettings, visualizationEngine, currentMxGraphLayout, mxLayoutDirection, mxIntraCellSpacing, mxInterRankCellSpacing, mxParallelEdgeSpacing, mxFineTuning, mxTightenToSource, mxEdgeStyle, mxMultigraph, mxGridEnabled, mxGridSize]);

  // Callbacks for mxGraph layout management
  const handleSaveMxGraphLayout = useCallback((layout: any) => {
    console.log('[App] Saving layout for context:', selectedContext, 'Layout:', layout);

    // Preserve settings if they exist in current layout
    let layoutToSave = layout;
    if (currentMxGraphLayout && currentMxGraphLayout.settings) {
      console.log('[App] Preserving existing settings during save');
      layoutToSave = {
        ...layout,
        settings: currentMxGraphLayout.settings
      };
    }

    // Update state synchronously first
    setCurrentMxGraphLayout(layoutToSave);

    // Then save to file
    vscode.postMessage({
      command: 'saveMxGraphLayout',
      data: {
        context: selectedContext,
        layout: layoutToSave
      }
    });
  }, [selectedContext, currentMxGraphLayout]);

  // Auto layout function using dagre
  const getLayoutedElements = (nodes: Node[], edges: Edge[]) => {
    const nodeWidth = 250;
    const nodeHeight = 180;
    
    const dagreGraph = new dagre.graphlib.Graph();
    dagreGraph.setDefaultEdgeLabel(() => ({}));
    
    const graphOptions: any = { 
      rankdir: layoutDirection, 
      nodesep: nodeSpacing, 
      ranksep: rankSpacing,
      edgesep: edgeSpacing,
      align: layoutAlign,
      ranker: ranker,
      marginx: marginX,
      marginy: marginY
    };
    
    // Only add acyclicer if it's set
    if (acyclicer) {
      graphOptions.acyclicer = acyclicer;
    }
    
    dagreGraph.setGraph(graphOptions);

    nodes.forEach((node) => {
      dagreGraph.setNode(node.id, { width: nodeWidth, height: nodeHeight });
    });

    edges.forEach((edge) => {
      dagreGraph.setEdge(edge.source, edge.target);
    });

    dagre.layout(dagreGraph);

    const layoutedNodes = nodes.map((node) => {
      const nodeWithPosition = dagreGraph.node(node.id);
      node.targetPosition = Position.Top;
      node.sourcePosition = Position.Bottom;
      node.position = {
        x: nodeWithPosition.x - nodeWidth / 2,
        y: nodeWithPosition.y - nodeHeight / 2,
      };
      return node;
    });

    return { nodes: layoutedNodes, edges };
  };

  // Auto layout function using D3-Force
  const getForceLayoutedElements = (nodes: Node[], edges: Edge[]) => {
    const nodeWidth = 250;
    const nodeHeight = 180;
    
    // Clone nodes to avoid mutating original array
    // D3-force will mutate these objects directly
    const simulationNodes = nodes.map((node, i) => ({
      id: node.id,
      x: node.position?.x + nodeWidth / 2 || Math.random() * 1000,
      y: node.position?.y + nodeHeight / 2 || Math.random() * 600,
      vx: 0,
      vy: 0,
      index: i,
      originalNode: node
    }));
    
    // Filter edges to only include those between existing nodes
    const nodeIds = new Set(nodes.map(n => n.id));
    const validEdges = edges.filter(edge => 
      nodeIds.has(edge.source) && nodeIds.has(edge.target)
    );
    
    // Create links array for d3-force
    const simulationLinks = validEdges.map(edge => ({
      source: edge.source, // D3 will replace these with node references
      target: edge.target,
      index: 0
    }));
    
    // Create simulation - D3 will mutate simulationNodes directly
    const simulation = d3Force.forceSimulation(simulationNodes)
      .force('charge', d3Force.forceManyBody()
        .strength(forceStrength)
        .distanceMax(forceChargeDistanceMax))
      .force('link', d3Force.forceLink(simulationLinks)
        .id((d: any) => d.id)
        .distance(forceDistance)
        .strength(forceLinkStrength))
      .force('center', d3Force.forceCenter(800, 400))
      .force('collide', d3Force.forceCollide()
        .radius(forceCollideRadius)
        .strength(forceCollideStrength))
      .force('x', d3Force.forceX(800).strength(forceXStrength))
      .force('y', d3Force.forceY(400).strength(forceYStrength))
      .stop();
    
    // Run simulation for specified iterations
    simulation.tick(forceIterations);
    
    // Map simulation results back to ReactFlow nodes
    const layoutedNodes = nodes.map((node) => {
      const simNode = simulationNodes.find(n => n.id === node.id);
      if (!simNode) {
        console.warn(`Node ${node.id} not found in simulation results`);
        return node;
      }
      
      return {
        ...node,
        position: {
          x: simNode.x - nodeWidth / 2,
          y: simNode.y - nodeHeight / 2,
        },
        targetPosition: Position.Top,
        sourcePosition: Position.Bottom,
      };
    });
    
    return { nodes: layoutedNodes, edges: validEdges };
  };

  // Auto layout function using ELK
  const getElkLayoutedElements = async (nodes: Node[], edges: Edge[]) => {
    console.log('ELK Layout: Starting with', nodes.length, 'nodes and', edges.length, 'edges');
    console.log('ELK Settings:', { elkAlgorithm, elkDirection, elkNodeSpacing, elkLayerSpacing, elkEdgeSpacing });
    
    const nodeWidth = 250;
    const nodeHeight = 180;
    
    // Create a set of node IDs for quick lookup
    const nodeIds = new Set(nodes.map(n => n.id));
    
    const elkNodes = nodes.map((node) => ({
      id: node.id,
      width: nodeWidth,
      height: nodeHeight,
    }));

    // Filter edges to only include those between existing nodes
    const validEdges = edges.filter(edge => 
      nodeIds.has(edge.source) && nodeIds.has(edge.target)
    );
    
    console.log(`Filtered edges: ${edges.length} -> ${validEdges.length} (removed edges to non-entity types)`);

    const elkEdges = validEdges.map((edge) => ({
      id: edge.id,
      sources: [edge.source],
      targets: [edge.target],
    }));

    const layoutOptions: any = {
      'elk.algorithm': elkAlgorithm,
      'elk.direction': elkDirection,
      'elk.spacing.nodeNode': elkNodeSpacing.toString(),
      'elk.layered.spacing.nodeNodeBetweenLayers': elkLayerSpacing.toString(),
      'elk.spacing.edgeNode': elkEdgeNodeSpacing.toString(),
      'elk.hierarchyHandling': elkHierarchyHandling,
      'elk.layered.nodePlacement.strategy': elkNodePlacement,
      'elk.layered.crossingMinimization.strategy': elkCrossingMinimization,
      'elk.interactive': elkInteractive.toString(),
      'elk.portConstraints': elkPortConstraints,
      'elk.edgeRouting': elkEdgeRouting,
      'elk.spacing.edgeEdge': elkEdgeEdgeSpacing.toString(),
      'elk.spacing.edgeLabel': elkEdgeLabelSpacing.toString(),
    };
    
    // Add algorithm-specific edge routing options
    if (elkEdgeRouting === 'SPLINES') {
      layoutOptions['elk.layered.edgeRouting.splines.mode'] = elkSplineMode;
    } else if (elkEdgeRouting === 'POLYLINE') {
      layoutOptions['elk.layered.edgeRouting.polyline.slopedEdgeZoneWidth'] = elkPolylineSloped.toString();
    }

    const graph = {
      id: 'root',
      layoutOptions,
      children: elkNodes,
      edges: elkEdges
    };

    console.log('ELK Graph input:', JSON.stringify(graph, null, 2));

    try {
      const layoutedGraph = await elk.layout(graph);
      console.log('ELK Layout result:', layoutedGraph);
      
      const layoutedNodes = nodes.map((node) => {
        const elkNode = layoutedGraph.children?.find((n: any) => n.id === node.id);
        if (elkNode) {
          console.log(`Node ${node.id}: x=${elkNode.x}, y=${elkNode.y}`);
          node.position = {
            x: elkNode.x || 0,
            y: elkNode.y || 0,
          };
        } else {
          console.log(`Node ${node.id}: NOT FOUND in ELK result`);
        }
        return node;
      });

      return { nodes: layoutedNodes, edges };
    } catch (error) {
      console.error('ELK Layout error:', error);
      // Fallback to dagre on error
      const { nodes: layoutedNodes, edges: layoutedEdges } = getLayoutedElements(nodes, edges);
      return { nodes: layoutedNodes, edges: layoutedEdges };
    }
  };

  const selectedContextRef = useRef<string | null>(null);
  
  useEffect(() => {
    selectedContextRef.current = selectedContext;
  }, [selectedContext]);

  const showDiagram = useCallback((contextName: string, engine: 'reactflow' | 'mxgraph' = 'reactflow') => {
    const previousContext = selectedContextRef.current;
    console.log('[App] showDiagram called:', contextName, engine, 'Previous context:', previousContext);
    const context = contexts.find(c => c.Name === contextName);
    if (!context) return;

    // Reset layout when switching to a different context
    if (previousContext !== contextName) {
      console.log('[App] Context changed, resetting layouts');
      setCurrentMxGraphLayout(null);
      setHasLoadedLayout(false);
      
      // Force MxGraphView to remount when switching to a different context
      if (engine === 'mxgraph') {
        console.log('[App] Context changed, incrementing mxGraphKey');
        setMxGraphKey(prev => prev + 1);
      }
    } else {
      console.log('[App] Same context, keeping layouts');
    }

    setSelectedContext(contextName);
    setVisualizationEngine(engine);
    
    // Reload layout from file when switching engines
    console.log('[App] Requesting layout for engine:', engine);
    if (engine === 'mxgraph') {
      vscode.postMessage({
        command: 'getMxGraphLayout',
        context: contextName
      });
    } else {
      vscode.postMessage({
        command: 'getLayout',
        context: contextName
      });
    }
    
    // Create nodes from entities
    const newNodes: Node[] = [];
    const newEdges: Edge[] = [];
    
    context.Entities.forEach((entity) => {
      newNodes.push({
        id: entity.Name,
        type: 'entity',
        position: { x: 0, y: 0 }, // Will be calculated by dagre
        data: entity,
      });
    });

    // Create edges from relationships
    context.Entities.forEach(entity => {
      entity.Relationships.forEach(rel => {
        const edgeId = `${entity.Name}-${rel.TargetEntity}-${rel.NavigationProperty}`;
        
        // Convert relationship type to display format
        let relationshipSymbol = '';
        let sourceLabel = '';
        let targetLabel = '';
        
        switch(rel.Type) {
          case 'OneToOne':
            relationshipSymbol = '1:1';
            sourceLabel = '1';
            targetLabel = '1';
            break;
          case 'OneToMany':
            relationshipSymbol = '1:*';
            sourceLabel = '1';
            targetLabel = '*';
            break;
          case 'ManyToOne':
            relationshipSymbol = '*:1';
            sourceLabel = '*';
            targetLabel = '1';
            break;
          case 'ManyToMany':
            relationshipSymbol = '*:*';
            sourceLabel = '*';
            targetLabel = '*';
            break;
          default:
            relationshipSymbol = rel.Type;
        }
        
        newEdges.push({
          id: edgeId,
          source: entity.Name,
          target: rel.TargetEntity,
          type: 'custom',
          animated: edgeAnimated,
          label: rel.NavigationProperty,
          labelStyle: { 
            fill: '#1A535C',
            fontWeight: 700,
            fontSize: 11,
            background: 'rgba(241, 237, 225, 0.9)',
            padding: 2,
            borderRadius: 2
          },
          style: {
            strokeWidth: 2,
            stroke: '#4ECDC4',
          },
          markerEnd: {
            type: MarkerType.ArrowClosed,
            color: '#4ECDC4',
          },
          data: {
            sourceLabel,
            targetLabel,
            relationshipType: relationshipSymbol,
            pathType
          }
        });
      });
    });

    // Apply layout based on selected algorithm - always use full edges for layout calculation
    // Skip auto-layout if we have loaded positions from file
    if (hasLoadedLayout) {
      console.log('Skipping auto-layout, using saved positions');
      setNodes(newNodes);
      setEdges(newEdges);
      return;
    }
    
    if (layoutAlgorithm === 'elk') {
      getElkLayoutedElements(newNodes, newEdges).then(({ nodes: layoutedNodes, edges: layoutedEdges }) => {
        setNodes(layoutedNodes);
        setEdges(layoutedEdges);
      });
    } else if (layoutAlgorithm === 'd3force') {
      const { nodes: layoutedNodes, edges: layoutedEdges } = getForceLayoutedElements(
        newNodes,
        newEdges
      );
      setNodes(layoutedNodes);
      setEdges(layoutedEdges);
    } else {
      const { nodes: layoutedNodes, edges: layoutedEdges } = getLayoutedElements(
        newNodes,
        newEdges
      );
      setNodes(layoutedNodes);
      setEdges(layoutedEdges);
    }
  }, [contexts, edgeStyle, edgeAnimated, pathType, layoutAlgorithm, layoutDirection, nodeSpacing, rankSpacing, edgeSpacing, layoutAlign, ranker, acyclicer, marginX, marginY, elkAlgorithm, elkDirection, elkNodeSpacing, elkLayerSpacing, elkEdgeSpacing, elkHierarchyHandling, elkNodePlacement, elkCrossingMinimization, elkInteractive, elkPortConstraints, forceStrength, forceDistance, forceIterations, forceCollideRadius, forceCollideStrength, forceLinkStrength, forceXStrength, forceYStrength, forceChargeDistanceMax]);

  const onNodeDragStop = useCallback(() => {
    if (!selectedContext) return;

    // Do NOT autosave when Settings panel is open - save only via "Save Settings" button
    if (showSettings) {
      console.log('[App] Skipping autosave - Settings panel is open');
      return;
    }

    // Save layout after drag - context-specific format
    const layoutData = {
      entities: nodes.reduce((acc, node) => ({
        ...acc,
        [node.id]: { x: node.position.x, y: node.position.y }
      }), {}),
      zoom: 1.0,
      center: { x: 0, y: 0 }
    };
    
    vscode.postMessage({ 
      command: 'saveLayout',
      context: selectedContext,
      data: {
        context: selectedContext,
        layout: layoutData
      }
    });
  }, [nodes, selectedContext, showSettings]);

  const applyReactFlowLayout = useCallback((layoutData: any) => {
    if (!layoutData || !layoutData.entities) {
      setHasLoadedLayout(false);
      return;
    }
    
    setNodes(nodes => nodes.map(node => {
      const savedPosition = layoutData.entities[node.id];
      if (savedPosition) {
        return {
          ...node,
          position: { x: savedPosition.x, y: savedPosition.y }
        };
      }
      return node;
    }));
    setHasLoadedLayout(true);
  }, []);

  const handleRefresh = () => {
    setIsAnalyzing(true);
    setLogs([]);
    setContexts([]);
    setSelectedContext(null);
    setNodes([]);
    setEdges([]);
    setShowLogs(true);
    vscode.postMessage({ command: 'analyze' });
  };

  // Update edge style for existing edges
  useEffect(() => {
    setEdges((eds) =>
      eds.map((edge) => ({
        ...edge,
        type: 'custom',
        animated: edgeAnimated,
        data: {
          ...edge.data,
          pathType: pathType
        }
      }))
    );
  }, [pathType, edgeAnimated, setEdges]);
  
  // Toggle edges visibility
  useEffect(() => {
    if (selectedContext) {
      // Re-run the diagram creation with current edge visibility
      showDiagram(selectedContext);
    }
  }, [showEdges]);

  // Re-layout when layout algorithm changes
  useEffect(() => {
    console.log('Layout algorithm changed to:', layoutAlgorithm);
    if (nodes.length > 0 && edges.length > 0) {
      if (layoutAlgorithm === 'elk') {
        console.log('Applying ELK layout...');
        getElkLayoutedElements(nodes, edges).then(({ nodes: layoutedNodes }) => {
          console.log('ELK layout applied, setting nodes');
          setNodes(layoutedNodes);
        });
      } else if (layoutAlgorithm === 'd3force') {
        console.log('Applying D3-Force layout...');
        const { nodes: layoutedNodes } = getForceLayoutedElements(nodes, edges);
        setNodes(layoutedNodes);
      } else {
        console.log('Applying Dagre layout...');
        const { nodes: layoutedNodes } = getLayoutedElements(nodes, edges);
        setNodes(layoutedNodes);
      }
    } else {
      console.log('No nodes or edges to layout');
    }
  }, [layoutAlgorithm]);

  // Re-layout when ELK settings change
  useEffect(() => {
    if (layoutAlgorithm === 'elk' && nodes.length > 0 && edges.length > 0) {
      getElkLayoutedElements(nodes, edges).then(({ nodes: layoutedNodes }) => {
        setNodes(layoutedNodes);
      });
    }
  }, [elkAlgorithm, elkDirection, elkNodeSpacing, elkLayerSpacing, elkEdgeSpacing, elkHierarchyHandling, elkNodePlacement, elkCrossingMinimization, elkInteractive, elkPortConstraints, elkEdgeRouting, elkSplineMode, elkPolylineSloped, elkEdgeNodeSpacing, elkEdgeEdgeSpacing, elkEdgeLabelSpacing]);

  // Re-layout when Dagre settings change
  useEffect(() => {
    if (layoutAlgorithm === 'dagre' && nodes.length > 0 && edges.length > 0) {
      const { nodes: layoutedNodes } = getLayoutedElements(nodes, edges);
      setNodes(layoutedNodes);
    }
  }, [layoutDirection, nodeSpacing, rankSpacing, edgeSpacing, layoutAlign, ranker, acyclicer, marginX, marginY]);

  // Re-layout when D3-Force settings change
  useEffect(() => {
    if (layoutAlgorithm === 'd3force' && nodes.length > 0 && edges.length > 0) {
      const { nodes: layoutedNodes } = getForceLayoutedElements(nodes, edges);
      setNodes(layoutedNodes);
    }
  }, [forceStrength, forceDistance, forceIterations, forceCollideRadius, forceCollideStrength, forceLinkStrength, forceXStrength, forceYStrength, forceChargeDistanceMax]);

  // Re-layout when layout settings change
  const reapplyLayout = useCallback(() => {
    if (nodes.length > 0 && edges.length > 0) {
      if (layoutAlgorithm === 'elk') {
        getElkLayoutedElements(nodes, edges).then(({ nodes: layoutedNodes }) => {
          setNodes(layoutedNodes);
        });
      } else if (layoutAlgorithm === 'd3force') {
        const { nodes: layoutedNodes } = getForceLayoutedElements(nodes, edges);
        setNodes(layoutedNodes);
      } else {
        const { nodes: layoutedNodes } = getLayoutedElements(nodes, edges);
        setNodes(layoutedNodes);
      }
    }
  }, [nodes, edges, layoutAlgorithm, layoutDirection, nodeSpacing, rankSpacing, edgeSpacing, layoutAlign, ranker, acyclicer, marginX, marginY, elkAlgorithm, elkDirection, elkNodeSpacing, elkLayerSpacing, elkEdgeSpacing, elkHierarchyHandling, elkNodePlacement, elkCrossingMinimization, elkInteractive, elkPortConstraints, forceStrength, forceDistance, forceIterations, forceCollideRadius, forceCollideStrength, forceLinkStrength, forceXStrength, forceYStrength, forceChargeDistanceMax]);

  return (
    <div className="efvis-container">
      {showSidebar && (
      <div className="efvis-sidebar" onWheel={(e) => e.stopPropagation()}>
        <div className="sidebar-header">
          <h2>🗃️ EF Visualizer</h2>
          <div className="header-buttons">
            <button className="btn-refresh" onClick={handleRefresh}>
              🔄 Refresh
            </button>
            <button 
              className="btn-settings" 
              onClick={() => setShowSettings(!showSettings)}
              title="Toggle Settings"
            >
              ⚙️
            </button>
          </div>
        </div>
        
        {!showSettings ? (
          <>
            {!showEntitiesPanel ? (
              <>
            <div className="sidebar-content-sections">
            <div className="contexts-section">
              <div className="section-header" onClick={() => setShowDbContexts(!showDbContexts)}>
                <h3>🗂️ DbContexts</h3>
                <button className="btn-toggle-section">
                  {showDbContexts ? '▼' : '▶'}
                </button>
              </div>
              {showDbContexts && (
              <>
              {isAnalyzing && contexts.length === 0 && (
                <div className="analyzing-message">
                  <p>Searching for Entity Framework DbContexts...</p>
                  <p style={{fontSize: '11px', opacity: 0.7}}>This may take a moment for large projects</p>
                </div>
              )}
              {contexts.sort((a, b) => a.Name.localeCompare(b.Name)).map(context => (
                <div key={context.Name} className="context-card">
                  <div className="context-name">{context.Name}</div>
                  <div className="context-info">
                    {context.Entities?.length || 0} entities
                  </div>
                  <div className="visualization-buttons">
                    <button 
                      className="btn-show-diagram"
                      onClick={() => showDiagram(context.Name, 'mxgraph')}
                      title="Open with mxGraph"
                    >
                      MXGRAPH
                    </button>
                    <button 
                      className="btn-show-diagram"
                      onClick={() => showDiagram(context.Name, 'reactflow')}
                      title="Open with ReactFlow"
                    >
                      REACTFLOW
                    </button>
                  </div>
                </div>
              ))}
              </>
              )}
            </div>

            {visualizationEngine === 'mxgraph' && selectedContext && (
              <div className="entities-button-section">
                <button
                  className="btn-show-entities"
                  onClick={() => setShowEntitiesPanel(true)}
                >
                  📦 ENTITIES
                </button>
              </div>
            )}

            {visualizationEngine === 'mxgraph' && selectedContext && (
              <div className="graph-controls-section">
                <div className="section-header" onClick={() => setShowGraphControls(!showGraphControls)}>
                  <h3>🎮 Controls</h3>
                  <button className="btn-toggle-section">
                    {showGraphControls ? '▼' : '▶'}
                  </button>
                </div>
                {showGraphControls && (
                <div className="graph-controls-buttons">
                  <button
                    className="btn-graph-control"
                    onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'zoomIn' }, '*')}
                    title="Zoom In"
                  >
                    ➕ Zoom In
                  </button>
                  <button
                    className="btn-graph-control"
                    onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'zoomOut' }, '*')}
                    title="Zoom Out"
                  >
                    ➖ Zoom Out
                  </button>
                  <button
                    className="btn-graph-control"
                    onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'fit' }, '*')}
                    title="Fit View"
                  >
                    🔍 Fit View
                  </button>
                  <button
                    className="btn-graph-control"
                    onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'center' }, '*')}
                    title="Center"
                  >
                    🎯 Center
                  </button>
                  <button
                    className="btn-graph-control"
                    onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'saveLayout' }, '*')}
                    title="Save Layout"
                  >
                    💾 Save Layout
                  </button>
                </div>
                )}
              </div>
            )}
            </div>

            <div className="logs-section">
              <div className="logs-header" onClick={() => setShowLogs(!showLogs)}>
                <h3>📋 Analysis Logs</h3>
                <button className="btn-toggle-logs">
                  {showLogs ? '▼' : '▶'}
                </button>
              </div>
              {showLogs && (
                <div className="logs-content">
                  {isAnalyzing && logs.length === 0 && (
                    <div className="analyzing-indicator">
                      <div className="spinner"></div>
                      <span>Analyzing C# projects...</span>
                    </div>
                  )}
                  {logs.map((log, i) => (
                    <div key={i} className="log-entry">{log}</div>
                  ))}
                </div>
              )}
            </div>
              </>
            ) : (
              <div className="entities-panel-container">
                <div className="entities-panel-header">
                  <h3>📦 Entities</h3>
                  <button
                    className="btn-close-entities-panel"
                    onClick={() => {
                      setShowEntitiesPanel(false);
                      setEntitiesSearchQuery('');
                    }}
                  >
                    ← Back
                  </button>
                </div>
                <div className="entities-search-box">
                  <input
                    type="text"
                    className="entities-search-input"
                    placeholder="🔍 Search entities..."
                    value={entitiesSearchQuery}
                    onChange={(e) => setEntitiesSearchQuery(e.target.value)}
                  />
                  {entitiesSearchQuery && (
                    <button
                      className="btn-clear-search"
                      onClick={() => setEntitiesSearchQuery('')}
                      title="Clear search"
                    >
                      ✕
                    </button>
                  )}
                </div>
                <div className="entities-panel-content" onWheel={(e) => e.stopPropagation()}>
                  <div className="entities-list">
                    {contexts.find(c => c.Name === selectedContext)?.Entities
                      ?.filter(entity =>
                        entity.Name.toLowerCase().includes(entitiesSearchQuery.toLowerCase())
                      )
                      .map(entity => {
                        const entityName = entity.Name;
                        const query = entitiesSearchQuery.toLowerCase();
                        const index = entityName.toLowerCase().indexOf(query);

                        let displayName;
                        if (query && index !== -1) {
                          const before = entityName.substring(0, index);
                          const match = entityName.substring(index, index + query.length);
                          const after = entityName.substring(index + query.length);
                          displayName = (
                            <>
                              {before}
                              <span className="highlight-match">{match}</span>
                              {after}
                            </>
                          );
                        } else {
                          displayName = entityName;
                        }

                        return (
                          <button
                            key={entity.Name}
                            className="entity-item"
                            onClick={() => window.postMessage({ type: 'mxGraphControl', command: 'focusEntity', entityName: entity.Name }, '*')}
                            title={`Jump to ${entity.Name}`}
                          >
                            <span className="entity-icon">🔷</span>
                            <span className="entity-name">{displayName}</span>
                          </button>
                        );
                      })}
                  </div>
                </div>
              </div>
            )}
          </>
        ) : (
          <div className="settings-container" onWheel={(e) => e.stopPropagation()}>
            <div className="settings-header">
              <h3>⚙️ Settings</h3>
              <button 
                className="btn-close-settings"
                onClick={() => setShowSettings(false)}
              >
                ← Back
              </button>
            </div>

            {visualizationEngine === 'reactflow' ? (
              <div className="reactflow-settings-section">
                <h3>⚛️ ReactFlow Settings</h3>

                <div style={{
                  padding: '12px',
                  background: '#fff3cd',
                  border: '2px solid #ff9800',
                  borderRadius: '4px',
                  marginBottom: '20px'
                }}>
                  <div style={{ color: '#e65100', fontWeight: 'bold', marginBottom: '4px' }}>
                    ⚠️ Experimental Features
                  </div>
                  <div style={{ fontSize: '12px', color: '#e65100' }}>
                    These settings are experimental and may be unstable.
                  </div>
                </div>

                <div className="reactflow-settings">
                
                <div className="setting-group">
                  <h4>Display Options</h4>
                  <label className="checkbox-label stylish">
                    <input 
                      type="checkbox" 
                      checked={showEdges}
                      onChange={(e) => setShowEdges(e.target.checked)}
                    />
                    <span className="checkbox-custom"></span>
                    <span>Show Relationships</span>
                  </label>
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px', marginLeft: '28px' }}>
                    Show/hide all connection lines between entities
                  </div>
                  
                  <label className="checkbox-label stylish" style={{ marginTop: '15px' }}>
                    <input 
                      type="checkbox" 
                      checked={showHandles}
                      onChange={(e) => setShowHandles(e.target.checked)}
                    />
                    <span className="checkbox-custom"></span>
                    <span>Show Connection Points</span>
                  </label>
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px', marginLeft: '28px' }}>
                    Show handles on hover for manual edge connections
                  </div>
                </div>

                <div className="setting-group" style={{ marginTop: '20px' }}>
                  <h4>Connection Mode</h4>
                  <div className="radio-group">
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="connectionMode" 
                        value="strict"
                        checked={connectionMode === 'strict'}
                        onChange={(e) => setConnectionMode(e.target.value)}
                      />
                      <span>Strict (Source to Target only)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="connectionMode" 
                        value="loose"
                        checked={connectionMode === 'loose'}
                        onChange={(e) => setConnectionMode(e.target.value)}
                      />
                      <span>Loose (Flexible connections)</span>
                    </label>
                  </div>
                </div>

                <div className="setting-group" style={{ marginTop: '20px' }}>
                  <h4>Edge Path Style</h4>
                  <div className="radio-group">
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="pathType" 
                        value="smoothstep"
                        checked={pathType === 'smoothstep'}
                        onChange={(e) => setPathType(e.target.value)}
                      />
                      <span>Smooth Step</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="pathType" 
                        value="straight"
                        checked={pathType === 'straight'}
                        onChange={(e) => setPathType(e.target.value)}
                      />
                      <span>Straight</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="pathType" 
                        value="bezier"
                        checked={pathType === 'bezier'}
                        onChange={(e) => setPathType(e.target.value)}
                      />
                      <span>Bezier Curve</span>
                    </label>
                  </div>
                  
                  <label className="checkbox-label" style={{ marginTop: '15px' }}>
                    <input 
                      type="checkbox" 
                      checked={edgeAnimated}
                      onChange={(e) => setEdgeAnimated(e.target.checked)}
                    />
                    <span>Animated Edges</span>
                  </label>
                </div>
              </div>
            </div>
            ) : (
              <div className="mxgraph-settings-section">
                <h3>📐 mxGraph Settings</h3>

                <div style={{
                  padding: '12px',
                  background: '#fff3cd',
                  border: '2px solid #ff9800',
                  borderRadius: '4px',
                  marginBottom: '20px'
                }}>
                  <div style={{ color: '#e65100', fontWeight: 'bold', marginBottom: '4px' }}>
                    ⚠️ Experimental Features
                  </div>
                  <div style={{ fontSize: '12px', color: '#e65100' }}>
                    These settings are experimental and may be unstable.
                  </div>
                </div>

                <div className="mxgraph-settings">

                  <div className="setting-group">
                    <label className="setting-label">
                      Entity Name Font Size: <span className="value-display">{mxEntityNameFontSize}px</span>
                    </label>
                    <input
                      type="range"
                      min="10"
                      max="50"
                      value={mxEntityNameFontSize}
                      onChange={(e) => {
                        setMxEntityNameFontSize(Number(e.target.value));
                      }}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Font size of entity name in card header
                    </div>
                  </div>

                  <div className="setting-group">
                    <label className="setting-label">
                      Property Font Size: <span className="value-display">{mxPropertyFontSize}px</span>
                    </label>
                    <input
                      type="range"
                      min="8"
                      max="50"
                      value={mxPropertyFontSize}
                      onChange={(e) => {
                        setMxPropertyFontSize(Number(e.target.value));
                      }}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Font size of entity properties
                    </div>
                  </div>

                  <div className="setting-group">
                    <label className="setting-label">
                      Navigation Property Font Size: <span className="value-display">{mxNavigationPropertyFontSize}px</span>
                    </label>
                    <input
                      type="range"
                      min="8"
                      max="50"
                      value={mxNavigationPropertyFontSize}
                      onChange={(e) => {
                        setMxNavigationPropertyFontSize(Number(e.target.value));
                      }}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Font size of navigation properties
                    </div>
                  </div>

                  <div className="setting-group">
                    <h4>Layout Direction</h4>
                    <div className="radio-group-horizontal">
                      <label className="radio-label-compact">
                        <input 
                          type="radio" 
                          name="mxDirection" 
                          value="NORTH"
                          checked={mxLayoutDirection === 'NORTH'}
                          onChange={(e) => setMxLayoutDirection(e.target.value)}
                        />
                        <span>↑</span>
                      </label>
                      <label className="radio-label-compact">
                        <input 
                          type="radio" 
                          name="mxDirection" 
                          value="SOUTH"
                          checked={mxLayoutDirection === 'SOUTH'}
                          onChange={(e) => setMxLayoutDirection(e.target.value)}
                        />
                        <span>↓</span>
                      </label>
                      <label className="radio-label-compact">
                        <input 
                          type="radio" 
                          name="mxDirection" 
                          value="EAST"
                          checked={mxLayoutDirection === 'EAST'}
                          onChange={(e) => setMxLayoutDirection(e.target.value)}
                        />
                        <span>→</span>
                      </label>
                      <label className="radio-label-compact">
                        <input 
                          type="radio" 
                          name="mxDirection" 
                          value="WEST"
                          checked={mxLayoutDirection === 'WEST'}
                          onChange={(e) => setMxLayoutDirection(e.target.value)}
                        />
                        <span>←</span>
                      </label>
                    </div>
                  </div>

                  <div className="setting-group">
                    <label className="setting-label">
                      Cell Spacing: <span className="value-display">{mxIntraCellSpacing}</span>
                    </label>
                    <input 
                      type="range" 
                      min="10" 
                      max="200" 
                      value={mxIntraCellSpacing}
                      onChange={(e) => setMxIntraCellSpacing(Number(e.target.value))}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Spacing between nodes in the same rank
                    </div>
                  </div>

                  <div className="setting-group">
                    <label className="setting-label">
                      Rank Spacing: <span className="value-display">{mxInterRankCellSpacing}</span>
                    </label>
                    <input 
                      type="range" 
                      min="20" 
                      max="300" 
                      value={mxInterRankCellSpacing}
                      onChange={(e) => setMxInterRankCellSpacing(Number(e.target.value))}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Spacing between different hierarchy levels
                    </div>
                  </div>

                  <div className="setting-group" style={{ marginTop: '20px' }}>
                    <h4>Grid Settings</h4>
                    <label className="checkbox-label">
                      <input
                        type="checkbox"
                        checked={mxGridEnabled}
                        onChange={(e) => setMxGridEnabled(e.target.checked)}
                      />
                      <span>Enable Grid</span>
                    </label>
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px', marginLeft: '24px' }}>
                      Show background grid and snap nodes to grid
                    </div>

                    {mxGridEnabled && (
                      <div style={{ marginTop: '10px' }}>
                        <label className="setting-label">
                          Grid Size: <span className="value-display">{mxGridSize}px</span>
                        </label>
                        <input
                          type="range"
                          min="5"
                          max="50"
                          value={mxGridSize}
                          onChange={(e) => setMxGridSize(Number(e.target.value))}
                          className="range-slider"
                        />
                        <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                          Size of grid cells in pixels
                        </div>
                      </div>
                    )}
                  </div>

                  <div className="setting-group" style={{ marginTop: '20px' }}>
                    <h4>Display Options</h4>

                    <label className="setting-label">
                      Parallel Edge Spacing: <span className="value-display">{mxParallelEdgeSpacing}</span>
                    </label>
                    <input
                      type="range"
                      min="0"
                      max="50"
                      value={mxParallelEdgeSpacing}
                      onChange={(e) => setMxParallelEdgeSpacing(Number(e.target.value))}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Distance between parallel edges
                    </div>

                    <div style={{ marginTop: '15px' }}>
                      <label className="checkbox-label">
                        <input
                          type="checkbox"
                          checked={mxFineTuning}
                          onChange={(e) => setMxFineTuning(e.target.checked)}
                        />
                        <span>Fine Tuning</span>
                      </label>
                      <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginTop: '2px' }}>
                        Local optimization for better layout quality
                      </div>
                    </div>

                    <div style={{ marginTop: '10px' }}>
                      <label className="checkbox-label">
                        <input
                          type="checkbox"
                          checked={mxTightenToSource}
                          onChange={(e) => setMxTightenToSource(e.target.checked)}
                        />
                        <span>Tighten To Source</span>
                      </label>
                      <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginTop: '2px' }}>
                        Pull ranks closer to source nodes (more compact)
                      </div>
                    </div>

                    <div style={{ marginTop: '15px' }}>
                      <h4>Edge Routing Style</h4>
                      <div className="radio-group-vertical">
                        <label className="radio-label">
                          <input
                            type="radio"
                            name="mxEdgeStyle"
                            value="straight"
                            checked={mxEdgeStyle === 'straight'}
                            onChange={(e) => setMxEdgeStyle(e.target.value)}
                          />
                          <span>Straight Lines</span>
                        </label>
                        <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginBottom: '8px' }}>
                          Direct diagonal connections
                        </div>

                        <label className="radio-label">
                          <input
                            type="radio"
                            name="mxEdgeStyle"
                            value="orthogonal"
                            checked={mxEdgeStyle === 'orthogonal'}
                            onChange={(e) => setMxEdgeStyle(e.target.value)}
                          />
                          <span>Orthogonal</span>
                        </label>
                        <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginBottom: '8px' }}>
                          Right angles (90°)
                        </div>

                        <label className="radio-label">
                          <input
                            type="radio"
                            name="mxEdgeStyle"
                            value="elbow"
                            checked={mxEdgeStyle === 'elbow'}
                            onChange={(e) => setMxEdgeStyle(e.target.value)}
                          />
                          <span>Elbow</span>
                        </label>
                        <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginBottom: '8px' }}>
                          Rounded corners
                        </div>

                        <label className="radio-label">
                          <input
                            type="radio"
                            name="mxEdgeStyle"
                            value="entityRelation"
                            checked={mxEdgeStyle === 'entityRelation'}
                            onChange={(e) => setMxEdgeStyle(e.target.value)}
                          />
                          <span>Entity Relation</span>
                        </label>
                        <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px' }}>
                          Database diagram style
                        </div>
                      </div>
                    </div>

                    <div style={{ marginTop: '10px' }}>
                      <label className="checkbox-label">
                        <input
                          type="checkbox"
                          checked={mxMultigraph}
                          onChange={(e) => setMxMultigraph(e.target.checked)}
                        />
                        <span>Multigraph</span>
                      </label>
                      <div style={{ fontSize: '11px', color: '#666', marginLeft: '24px', marginTop: '2px' }}>
                        Allow multiple connections between same nodes
                      </div>
                    </div>
                  </div>

                  <div className="setting-group" style={{ marginTop: '20px' }}>
                    <button
                      className="btn-save-settings"
                      onClick={() => {
                        if (selectedContext && currentMxGraphLayout) {
                          // Save current layout WITH all settings
                          const layoutWithSettings = {
                            vertices: currentMxGraphLayout.vertices || {},
                            edges: currentMxGraphLayout.edges || [],
                            settings: {
                              edgeStyle: mxEdgeStyle,
                              entityNameFontSize: mxEntityNameFontSize,
                              propertyFontSize: mxPropertyFontSize,
                              navigationPropertyFontSize: mxNavigationPropertyFontSize,
                              layoutDirection: mxLayoutDirection,
                              intraCellSpacing: mxIntraCellSpacing,
                              interRankCellSpacing: mxInterRankCellSpacing,
                              parallelEdgeSpacing: mxParallelEdgeSpacing,
                              fineTuning: mxFineTuning,
                              tightenToSource: mxTightenToSource,
                              multigraph: mxMultigraph,
                              gridEnabled: mxGridEnabled,
                              gridSize: mxGridSize
                            }
                          };
                          vscode.postMessage({
                            command: 'saveMxGraphLayout',
                            data: {
                              context: selectedContext,
                              layout: layoutWithSettings
                            }
                          });
                          // Update current layout in state so settings are applied immediately
                          setCurrentMxGraphLayout(layoutWithSettings);
                          // Mark that settings were saved - don't restore on close
                          settingsSavedToFile.current = true;
                          // Force graph to recreate with new settings
                          setTimeout(() => {
                            setMxGraphKey(prev => prev + 1);
                          }, 50);
                          // Show success notification
                          console.log('[App] Settings saved to layout file!');
                          setMxSettingsJustSaved(true);
                          setTimeout(() => {
                            setMxSettingsJustSaved(false);
                          }, 2000);
                        }
                      }}
                      style={{
                        width: '100%',
                        padding: '12px',
                        background: mxSettingsJustSaved ? '#2e7d32' : '#4CAF50',
                        color: 'white',
                        border: '3px solid #1A535C',
                        fontWeight: 700,
                        fontSize: '13px',
                        textTransform: 'uppercase',
                        boxShadow: '4px 4px 0px #1A535C',
                        cursor: 'pointer',
                        marginBottom: '12px',
                        transition: 'background 0.3s ease'
                      }}
                    >
                      {mxSettingsJustSaved ? '✅ Saved!' : '💾 Save Settings to File'}
                    </button>
                    <div style={{ fontSize: '11px', color: '#666', marginBottom: '16px' }}>
                      Permanently save current positions, fonts, and layout settings
                    </div>

                    <button
                      className="btn-reset-layout"
                      onClick={() => {
                        if (selectedContext) {
                          setCurrentMxGraphLayout(null);
                          setMxGraphKey(prev => prev + 1);
                          vscode.postMessage({
                            command: 'saveMxGraphLayout',
                            data: {
                              context: selectedContext,
                              layout: null
                            }
                          });
                        }
                      }}
                      style={{
                        width: '100%',
                        padding: '12px',
                        background: '#FF6B35',
                        color: 'white',
                        border: '3px solid #1A535C',
                        fontWeight: 700,
                        fontSize: '13px',
                        textTransform: 'uppercase',
                        boxShadow: '4px 4px 0px #1A535C',
                        cursor: 'pointer'
                      }}
                    >
                      🔄 Reset to Auto Layout
                    </button>
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '8px' }}>
                      Clear saved positions and apply current settings
                    </div>
                  </div>
                </div>
              </div>
            )}

            {visualizationEngine === 'reactflow' && (
            <div className="algorithm-selector-section">
              <h3>🧮 Layout Algorithm</h3>
              <div className="algorithm-selector">
                <label className="radio-label">
                  <input 
                    type="radio" 
                    name="layoutAlgorithm" 
                    value="dagre"
                    checked={layoutAlgorithm === 'dagre'}
                    onChange={(e) => setLayoutAlgorithm(e.target.value)}
                  />
                  <span>Dagre (Fast & Simple)</span>
                </label>
                <label className="radio-label">
                  <input 
                    type="radio" 
                    name="layoutAlgorithm" 
                    value="elk"
                    checked={layoutAlgorithm === 'elk'}
                    onChange={(e) => setLayoutAlgorithm(e.target.value)}
                  />
                  <span>ELK (Advanced)</span>
                </label>
                <label className="radio-label">
                  <input 
                    type="radio" 
                    name="layoutAlgorithm" 
                    value="d3force"
                    checked={layoutAlgorithm === 'd3force'}
                    onChange={(e) => setLayoutAlgorithm(e.target.value)}
                  />
                  <span>D3-Force (Organic)</span>
                </label>
              </div>
            </div>
            )}

            {visualizationEngine === 'reactflow' && layoutAlgorithm === 'dagre' && (
            <div className="layout-settings-section">
              <h3>📐 Dagre Settings</h3>
              <div className="layout-settings">
            <div className="setting-group">
              <label className="setting-label">Direction:</label>
              <div className="radio-group-horizontal">
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="direction" 
                    value="TB"
                    checked={layoutDirection === 'TB'}
                    onChange={(e) => setLayoutDirection(e.target.value)}
                  />
                  <span>↓</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="direction" 
                    value="BT"
                    checked={layoutDirection === 'BT'}
                    onChange={(e) => setLayoutDirection(e.target.value)}
                  />
                  <span>↑</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="direction" 
                    value="LR"
                    checked={layoutDirection === 'LR'}
                    onChange={(e) => setLayoutDirection(e.target.value)}
                  />
                  <span>→</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="direction" 
                    value="RL"
                    checked={layoutDirection === 'RL'}
                    onChange={(e) => setLayoutDirection(e.target.value)}
                  />
                  <span>←</span>
                </label>
              </div>
            </div>

            <div className="setting-group">
              <label className="setting-label">
                Node Spacing: <span className="value-display">{nodeSpacing}</span>
              </label>
              <input 
                type="range" 
                min="50" 
                max="300" 
                value={nodeSpacing}
                onChange={(e) => setNodeSpacing(Number(e.target.value))}
                
                className="range-slider"
              />
            </div>

            <div className="setting-group">
              <label className="setting-label">
                Rank Spacing: <span className="value-display">{rankSpacing}</span>
              </label>
              <input 
                type="range" 
                min="50" 
                max="300" 
                value={rankSpacing}
                onChange={(e) => setRankSpacing(Number(e.target.value))}
                
                className="range-slider"
              />
            </div>

            <div className="setting-group">
              <label className="setting-label">Align:</label>
              <div className="radio-group-horizontal">
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="align" 
                    value="UL"
                    checked={layoutAlign === 'UL'}
                    onChange={(e) => setLayoutAlign(e.target.value)}
                  />
                  <span>↖</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="align" 
                    value="UR"
                    checked={layoutAlign === 'UR'}
                    onChange={(e) => setLayoutAlign(e.target.value)}
                  />
                  <span>↗</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="align" 
                    value="DL"
                    checked={layoutAlign === 'DL'}
                    onChange={(e) => setLayoutAlign(e.target.value)}
                  />
                  <span>↙</span>
                </label>
                <label className="radio-label-compact">
                  <input 
                    type="radio" 
                    name="align" 
                    value="DR"
                    checked={layoutAlign === 'DR'}
                    onChange={(e) => setLayoutAlign(e.target.value)}
                  />
                  <span>↘</span>
                </label>
              </div>
            </div>

            <div className="setting-group">
              <label className="setting-label">Ranker:</label>
              <select 
                value={ranker} 
                onChange={(e) => setRanker(e.target.value)}
                className="setting-select"
              >
                <option value="network-simplex">Network Simplex</option>
                <option value="tight-tree">Tight Tree</option>
                <option value="longest-path">Longest Path</option>
              </select>
            </div>

            <div className="setting-group">
              <label className="setting-label">
                Edge Spacing: <span className="value-display">{edgeSpacing}</span>
              </label>
              <input 
                type="range" 
                min="5" 
                max="50" 
                value={edgeSpacing}
                onChange={(e) => setEdgeSpacing(Number(e.target.value))}
                className="range-slider"
              />
              <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                Horizontal separation between edges
              </div>
            </div>

            <div className="setting-group">
              <label className="setting-label">Acyclicer:</label>
              <select 
                value={acyclicer} 
                onChange={(e) => setAcyclicer(e.target.value)}
                className="setting-select"
              >
                <option value="">None</option>
                <option value="greedy">Greedy</option>
              </select>
              <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                Algorithm to remove cycles in graph
              </div>
            </div>

            <div className="setting-group">
              <label className="setting-label">
                Margin X: <span className="value-display">{marginX}</span>
              </label>
              <input 
                type="range" 
                min="0" 
                max="100" 
                value={marginX}
                onChange={(e) => setMarginX(Number(e.target.value))}
                className="range-slider"
              />
              <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                Horizontal margin around graph
              </div>
            </div>

            <div className="setting-group">
              <label className="setting-label">
                Margin Y: <span className="value-display">{marginY}</span>
              </label>
              <input 
                type="range" 
                min="0" 
                max="100" 
                value={marginY}
                onChange={(e) => setMarginY(Number(e.target.value))}
                className="range-slider"
              />
              <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                Vertical margin around graph
              </div>
            </div>
          </div>
            </div>
            )}

            {visualizationEngine === 'reactflow' && layoutAlgorithm === 'elk' && (
            <div className="layout-settings-section">
              <h3>🔧 ELK Settings</h3>
              <div className="layout-settings">
                <div className="setting-group">
                  <label className="setting-label">Algorithm:</label>
                  <div className="radio-group">
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="layered"
                        checked={elkAlgorithm === 'layered'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>Layered (Hierarchical)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="stress"
                        checked={elkAlgorithm === 'stress'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>Stress (Force-based)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="mrtree"
                        checked={elkAlgorithm === 'mrtree'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>MR-Tree (Compact)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="radial"
                        checked={elkAlgorithm === 'radial'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>Radial (Circular)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="force"
                        checked={elkAlgorithm === 'force'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>Force (Physics)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkAlgorithm" 
                        value="disco"
                        checked={elkAlgorithm === 'disco'}
                        onChange={(e) => setElkAlgorithm(e.target.value)}
                      />
                      <span>Disco (Disconnected)</span>
                    </label>
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">Direction:</label>
                  <div className="radio-group-horizontal">
                    <label className="radio-label-compact">
                      <input 
                        type="radio" 
                        name="elkDirection" 
                        value="DOWN"
                        checked={elkDirection === 'DOWN'}
                        onChange={(e) => setElkDirection(e.target.value)}
                      />
                      <span>↓</span>
                    </label>
                    <label className="radio-label-compact">
                      <input 
                        type="radio" 
                        name="elkDirection" 
                        value="UP"
                        checked={elkDirection === 'UP'}
                        onChange={(e) => setElkDirection(e.target.value)}
                      />
                      <span>↑</span>
                    </label>
                    <label className="radio-label-compact">
                      <input 
                        type="radio" 
                        name="elkDirection" 
                        value="RIGHT"
                        checked={elkDirection === 'RIGHT'}
                        onChange={(e) => setElkDirection(e.target.value)}
                      />
                      <span>→</span>
                    </label>
                    <label className="radio-label-compact">
                      <input 
                        type="radio" 
                        name="elkDirection" 
                        value="LEFT"
                        checked={elkDirection === 'LEFT'}
                        onChange={(e) => setElkDirection(e.target.value)}
                      />
                      <span>←</span>
                    </label>
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Node Spacing: <span className="value-display">{elkNodeSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="20" 
                    max="500" 
                    value={elkNodeSpacing}
                    onChange={(e) => setElkNodeSpacing(Number(e.target.value))}
                    
                    className="range-slider"
                  />
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Layer Spacing: <span className="value-display">{elkLayerSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="20" 
                    max="500" 
                    value={elkLayerSpacing}
                    onChange={(e) => setElkLayerSpacing(Number(e.target.value))}
                    
                    className="range-slider"
                  />
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Edge Spacing: <span className="value-display">{elkEdgeSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="5" 
                    max="200" 
                    value={elkEdgeSpacing}
                    onChange={(e) => setElkEdgeSpacing(Number(e.target.value))}
                    className="range-slider"
                  />
                </div>

                <div className="setting-group">
                  <label className="setting-label">Node Placement:</label>
                  <select 
                    value={elkNodePlacement} 
                    onChange={(e) => setElkNodePlacement(e.target.value)}
                    className="setting-select"
                  >
                    <option value="BRANDES_KOEPF">Brandes Koepf</option>
                    <option value="LINEAR_SEGMENTS">Linear Segments</option>
                    <option value="NETWORK_SIMPLEX">Network Simplex</option>
                    <option value="SIMPLE">Simple</option>
                  </select>
                </div>

                <div className="setting-group">
                  <label className="setting-label">Crossing Minimization:</label>
                  <select 
                    value={elkCrossingMinimization} 
                    onChange={(e) => setElkCrossingMinimization(e.target.value)}
                    className="setting-select"
                  >
                    <option value="LAYER_SWEEP">Layer Sweep</option>
                    <option value="INTERACTIVE">Interactive</option>
                    <option value="NONE">None</option>
                  </select>
                </div>

                <div className="setting-group">
                  <label className="setting-label">Hierarchy Handling:</label>
                  <select 
                    value={elkHierarchyHandling} 
                    onChange={(e) => setElkHierarchyHandling(e.target.value)}
                    className="setting-select"
                  >
                    <option value="INCLUDE_CHILDREN">Include Children</option>
                    <option value="SEPARATE_CHILDREN">Separate Children</option>
                    <option value="INHERIT">Inherit</option>
                  </select>
                </div>

                <div className="setting-group">
                  <label className="checkbox-label">
                    <input 
                      type="checkbox" 
                      checked={elkInteractive}
                      onChange={(e) => setElkInteractive(e.target.checked)}
                    />
                    <span>Interactive Mode</span>
                  </label>
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Use existing positions as hints
                  </div>
                </div>

                <div className="setting-group" style={{ marginTop: '20px', borderTop: '2px solid var(--retro-navy)', paddingTop: '15px' }}>
                  <h4 style={{ color: 'var(--retro-navy)', marginBottom: '10px' }}>🔀 Edge Routing</h4>
                  
                  <label className="setting-label">Edge Routing Type:</label>
                  <div className="radio-group">
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkEdgeRouting" 
                        value="UNDEFINED"
                        checked={elkEdgeRouting === 'UNDEFINED'}
                        onChange={(e) => setElkEdgeRouting(e.target.value)}
                      />
                      <span>Undefined (Auto)</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkEdgeRouting" 
                        value="ORTHOGONAL"
                        checked={elkEdgeRouting === 'ORTHOGONAL'}
                        onChange={(e) => setElkEdgeRouting(e.target.value)}
                      />
                      <span>Orthogonal</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkEdgeRouting" 
                        value="POLYLINE"
                        checked={elkEdgeRouting === 'POLYLINE'}
                        onChange={(e) => setElkEdgeRouting(e.target.value)}
                      />
                      <span>Polyline</span>
                    </label>
                    <label className="radio-label">
                      <input 
                        type="radio" 
                        name="elkEdgeRouting" 
                        value="SPLINES"
                        checked={elkEdgeRouting === 'SPLINES'}
                        onChange={(e) => setElkEdgeRouting(e.target.value)}
                      />
                      <span>Splines</span>
                    </label>
                  </div>
                </div>

                {elkEdgeRouting === 'SPLINES' && (
                  <div className="setting-group">
                    <label className="setting-label">Spline Mode:</label>
                    <div className="radio-group">
                      <label className="radio-label">
                        <input 
                          type="radio" 
                          name="elkSplineMode" 
                          value="CONSERVATIVE"
                          checked={elkSplineMode === 'CONSERVATIVE'}
                          onChange={(e) => setElkSplineMode(e.target.value)}
                        />
                        <span>Conservative</span>
                      </label>
                      <label className="radio-label">
                        <input 
                          type="radio" 
                          name="elkSplineMode" 
                          value="SLOPPY"
                          checked={elkSplineMode === 'SLOPPY'}
                          onChange={(e) => setElkSplineMode(e.target.value)}
                        />
                        <span>Sloppy (Faster)</span>
                      </label>
                    </div>
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Conservative: Avoids overlaps, Sloppy: Curvier but may overlap
                    </div>
                  </div>
                )}

                {elkEdgeRouting === 'POLYLINE' && (
                  <div className="setting-group">
                    <label className="setting-label">
                      Sloped Edge Zone Width: <span className="value-display">{elkPolylineSloped.toFixed(1)}</span>
                    </label>
                    <input 
                      type="range" 
                      min="0" 
                      max="10" 
                      step="0.5"
                      value={elkPolylineSloped}
                      onChange={(e) => setElkPolylineSloped(Number(e.target.value))}
                      className="range-slider"
                    />
                    <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                      Width where polyline can avoid horizontal routing
                    </div>
                  </div>
                )}

                <div className="setting-group">
                  <label className="setting-label">
                    Edge-Node Spacing: <span className="value-display">{elkEdgeNodeSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="5" 
                    max="50" 
                    value={elkEdgeNodeSpacing}
                    onChange={(e) => setElkEdgeNodeSpacing(Number(e.target.value))}
                    className="range-slider"
                  />
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Edge-Edge Spacing: <span className="value-display">{elkEdgeEdgeSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="5" 
                    max="50" 
                    value={elkEdgeEdgeSpacing}
                    onChange={(e) => setElkEdgeEdgeSpacing(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Spacing between parallel edges
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Edge-Label Spacing: <span className="value-display">{elkEdgeLabelSpacing}</span>
                  </label>
                  <input 
                    type="range" 
                    min="2" 
                    max="20" 
                    value={elkEdgeLabelSpacing}
                    onChange={(e) => setElkEdgeLabelSpacing(Number(e.target.value))}
                    className="range-slider"
                  />
                </div>
              </div>
            </div>
            )}

            {visualizationEngine === 'reactflow' && layoutAlgorithm === 'd3force' && (
            <div className="layout-settings-section">
              <h3>⚛️ D3-Force Settings</h3>
              <div className="layout-settings">
                <div className="setting-group">
                  <label className="setting-label">
                    Charge Strength: <span className="value-display">{forceStrength}</span>
                  </label>
                  <input 
                    type="range" 
                    min="-10000" 
                    max="-100" 
                    value={forceStrength}
                    onChange={(e) => setForceStrength(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Negative values push nodes apart (repulsion)
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Charge Max Distance: <span className="value-display">{forceChargeDistanceMax}</span>
                  </label>
                  <input 
                    type="range" 
                    min="100" 
                    max="2000" 
                    value={forceChargeDistanceMax}
                    onChange={(e) => setForceChargeDistanceMax(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Maximum distance for charge effect
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Link Distance: <span className="value-display">{forceDistance}</span>
                  </label>
                  <input 
                    type="range" 
                    min="50" 
                    max="500" 
                    value={forceDistance}
                    onChange={(e) => setForceDistance(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Target distance between linked nodes
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Link Strength: <span className="value-display">{forceLinkStrength.toFixed(2)}</span>
                  </label>
                  <input 
                    type="range" 
                    min="0" 
                    max="1" 
                    step="0.05"
                    value={forceLinkStrength}
                    onChange={(e) => setForceLinkStrength(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    How strongly links pull nodes together
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Collision Radius: <span className="value-display">{forceCollideRadius}</span>
                  </label>
                  <input 
                    type="range" 
                    min="50" 
                    max="300" 
                    value={forceCollideRadius}
                    onChange={(e) => setForceCollideRadius(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Minimum distance between nodes
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Collision Strength: <span className="value-display">{forceCollideStrength.toFixed(2)}</span>
                  </label>
                  <input 
                    type="range" 
                    min="0" 
                    max="1" 
                    step="0.05"
                    value={forceCollideStrength}
                    onChange={(e) => setForceCollideStrength(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    How strongly nodes avoid overlap
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    X Position Strength: <span className="value-display">{forceXStrength.toFixed(2)}</span>
                  </label>
                  <input 
                    type="range" 
                    min="0" 
                    max="0.5" 
                    step="0.01"
                    value={forceXStrength}
                    onChange={(e) => setForceXStrength(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Horizontal centering force
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Y Position Strength: <span className="value-display">{forceYStrength.toFixed(2)}</span>
                  </label>
                  <input 
                    type="range" 
                    min="0" 
                    max="0.5" 
                    step="0.01"
                    value={forceYStrength}
                    onChange={(e) => setForceYStrength(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    Vertical centering force
                  </div>
                </div>

                <div className="setting-group">
                  <label className="setting-label">
                    Iterations: <span className="value-display">{forceIterations}</span>
                  </label>
                  <input 
                    type="range" 
                    min="10" 
                    max="300" 
                    value={forceIterations}
                    onChange={(e) => setForceIterations(Number(e.target.value))}
                    className="range-slider"
                  />
                  <div style={{ fontSize: '11px', color: '#666', marginTop: '4px' }}>
                    More iterations = more stable layout
                  </div>
                </div>
              </div>
            </div>
            )}
          </div>
        )}
      </div>
      )}

      <div className={`efvis-main ${showHandles ? 'show-handles' : ''}`}>
        <button 
          className="btn-toggle-sidebar"
          onClick={() => setShowSidebar(!showSidebar)}
          title={showSidebar ? 'Hide sidebar' : 'Show sidebar'}
        >
          {showSidebar ? '◀' : '▶'}
        </button>
        {selectedContext ? (
          visualizationEngine === 'reactflow' ? (
            <ReactFlow
              nodes={nodes}
              edges={showEdges ? edges : []}
              onNodesChange={onNodesChange}
              onEdgesChange={onEdgesChange}
              onNodeDragStop={onNodeDragStop}
              nodeTypes={nodeTypes}
              edgeTypes={edgeTypes}
              connectionMode={connectionMode as ConnectionMode}
              fitView
              minZoom={0.05}
              maxZoom={2}
              proOptions={{ hideAttribution: true }}
            >
              <Background 
                variant={BackgroundVariant.Dots}
                gap={20} 
                size={1}
                color="#1A535C20"
              />
              <Controls />
            </ReactFlow>
          ) : selectedContext && contexts.find(c => c.Name === selectedContext) ? (
            <MxGraphView
              key={mxGraphKey}
              context={contexts.find(c => c.Name === selectedContext)!}
              layoutDirection={mxLayoutDirection}
              intraCellSpacing={mxIntraCellSpacing}
              interRankCellSpacing={mxInterRankCellSpacing}
              gridEnabled={mxGridEnabled}
              gridSize={mxGridSize}
              parallelEdgeSpacing={mxParallelEdgeSpacing}
              fineTuning={mxFineTuning}
              tightenToSource={mxTightenToSource}
              edgeStyle={mxEdgeStyle}
              entityNameFontSize={mxEntityNameFontSize}
              propertyFontSize={mxPropertyFontSize}
              navigationPropertyFontSize={mxNavigationPropertyFontSize}
              multigraph={mxMultigraph}
              onSaveLayout={handleSaveMxGraphLayout}
              savedLayout={currentMxGraphLayout}
              showSettings={showSettings}
            />
          ) : (
            <div style={{ padding: '20px', color: '#666' }}>Loading context...</div>
          )
        ) : (
          <div className="no-context-selected">
            <h2>👈 Select a DbContext to visualize</h2>
          </div>
        )}
      </div>
    </div>
  );
}

export default App;