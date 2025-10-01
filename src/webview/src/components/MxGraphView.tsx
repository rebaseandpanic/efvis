import React, { useEffect, useRef, useState, useCallback } from 'react';
import { EntityInfo, DbContextInfo } from '../types';

// mxGraph imports - we'll use require due to lack of proper TypeScript support
const mx = require('mxgraph')({
  mxImageBasePath: '',
  mxBasePath: ''
});

interface MxGraphViewProps {
  context: DbContextInfo;
  layoutDirection?: string;
  intraCellSpacing?: number;
  interRankCellSpacing?: number;
  gridEnabled?: boolean;
  gridSize?: number;
  parallelEdgeSpacing?: number;
  fineTuning?: boolean;
  tightenToSource?: boolean;
  edgeStyle?: string;
  entityNameFontSize?: number;
  propertyFontSize?: number;
  navigationPropertyFontSize?: number;
  multigraph?: boolean;
  onSaveLayout?: (layout: any) => void;
  savedLayout?: any;
  showSettings?: boolean;
}

const MxGraphView: React.FC<MxGraphViewProps> = ({
  context,
  layoutDirection = 'NORTH',
  intraCellSpacing = 50,
  interRankCellSpacing = 100,
  gridEnabled = true,
  gridSize = 10,
  parallelEdgeSpacing = 10,
  fineTuning = true,
  tightenToSource = true,
  edgeStyle = 'orthogonal',
  entityNameFontSize = 12,
  propertyFontSize = 11,
  navigationPropertyFontSize = 11,
  multigraph = false,
  onSaveLayout,
  savedLayout,
  showSettings = false
}) => {
  const containerRef = useRef<HTMLDivElement>(null);
  const graphRef = useRef<any>(null);
  const vertexMapRef = useRef<{ [key: string]: any }>({});
  const saveTimeoutRef = useRef<any>(null);
  const saveLayoutCallbackRef = useRef<(() => void) | null>(null);
  const [isInitialized, setIsInitialized] = useState(false);
  const [selectedRelationship, setSelectedRelationship] = useState<any>(null);

  useEffect(() => {
    if (!containerRef.current || isInitialized) return;

    // Disable automatic line breaks to BR tags conversion BEFORE creating graph
    if (typeof mx.mxText !== 'undefined') {
      mx.mxText.prototype.replaceLinefeeds = false;
    }

    // Initialize mxGraph
    const graph = new mx.mxGraph(containerRef.current);
    graphRef.current = graph;

    // Configure graph settings
    graph.setConnectable(true);
    graph.setAllowDanglingEdges(false);
    graph.setDisconnectOnMove(false);
    graph.resetEdgesOnMove = true; // Auto-reset edge waypoints when moving vertices
    graph.setPanning(true);
    graph.panningHandler.useLeftButtonForPanning = true;
    graph.panningHandler.ignoreCell = false;
    graph.setTooltips(true);
    graph.setHtmlLabels(true);
    graph.centerZoom = false;
    
    // Configure grid
    graph.setGridEnabled(gridEnabled);
    graph.gridSize = gridSize;

    // Set grid background
    if (gridEnabled) {
      graph.view.gridColor = '#e0e0e0';
      containerRef.current.style.backgroundImage = `
        linear-gradient(#e0e0e0 1px, transparent 1px),
        linear-gradient(90deg, #e0e0e0 1px, transparent 1px)
      `;
      containerRef.current.style.backgroundSize = `${gridSize}px ${gridSize}px`;
      containerRef.current.style.backgroundPosition = '0 0';
    } else {
      containerRef.current.style.backgroundImage = 'none';
    }

    // Enable snap to grid
    graph.graphHandler.guidesEnabled = !gridEnabled;
    if (gridEnabled) {
      mx.mxGraphHandler.prototype.gridSize = gridSize;
    }

    // Add click handler for edges
    graph.addListener(mx.mxEvent.CLICK, (sender: any, evt: any) => {
      const cell = evt.getProperty('cell');

      if (cell && graph.model.isEdge(cell) && cell.userData) {
        setSelectedRelationship(cell.userData);
      } else if (!cell || !graph.model.isEdge(cell)) {
        setSelectedRelationship(null);
      }
    });

    // Auto-select connected edges when selecting vertices
    graph.getSelectionModel().addListener(mx.mxEvent.CHANGE, () => {
      const selected = graph.getSelectionCells();
      const vertices = selected.filter((c: any) => graph.model.isVertex(c));

      if (vertices.length > 0) {
        // Get all edges connected to selected vertices
        const cellsWithEdges = graph.addAllEdges(vertices);
        const edges = cellsWithEdges.filter((c: any) => graph.model.isEdge(c));

        // Only add edges if they connect between selected vertices
        const connectedEdges = edges.filter((edge: any) => {
          const source = edge.source;
          const target = edge.target;
          return source && target &&
                 vertices.includes(source) &&
                 vertices.includes(target);
        });

        // Add connected edges to selection
        if (connectedEdges.length > 0) {
          const allCells = [...vertices, ...connectedEdges];
          if (allCells.length !== selected.length) {
            graph.setSelectionCells(allCells);
          }
        }
      }
    });

    // Listen for cell movement to auto-save layout
    graph.addListener(mx.mxEvent.CELLS_MOVED, (sender: any, evt: any) => {
      const cells = evt.getProperty('cells') || [];
      const movedVertices = cells.filter((c: any) => graph.model.isVertex(c));

      // Reset edges connected to moved vertices
      if (movedVertices.length > 0) {
        const allEdges = graph.addAllEdges(movedVertices)
          .filter((c: any) => graph.model.isEdge(c));

        if (allEdges.length > 0) {
          graph.resetEdges(allEdges);
        }
      }

      // Debounce save to avoid excessive saves during drag
      if (saveTimeoutRef.current) {
        clearTimeout(saveTimeoutRef.current);
      }
      saveTimeoutRef.current = setTimeout(() => {
        if (saveLayoutCallbackRef.current) {
          saveLayoutCallbackRef.current();
        }
      }, 500);
    });

    // Listen for cell resize to auto-save layout
    graph.addListener(mx.mxEvent.CELLS_RESIZED, () => {
      if (saveTimeoutRef.current) {
        clearTimeout(saveTimeoutRef.current);
      }
      saveTimeoutRef.current = setTimeout(() => {
        if (saveLayoutCallbackRef.current) {
          saveLayoutCallbackRef.current();
        }
      }, 500);
    });

    // Enable rubberband selection and edge snapping
    new mx.mxRubberband(graph);
    mx.mxEdgeHandler.prototype.snapToTerminals = true;
    
    // Disable context menu on right click
    mx.mxEvent.disableContextMenu(containerRef.current);
    graph.popupMenuHandler.enabled = false;
    
    // Enable vertex resizing
    graph.setCellsResizable(true);
    graph.setResizeContainer(false);
    
    // Enable mouse wheel zoom - proper implementation
    mx.mxEvent.addMouseWheelListener(function(evt: any, up: boolean) {
      // Don't zoom if shift is held (for scrolling)
      if (evt.shiftKey) return;
      
      // Determine zoom direction
      if (up == null) {
        up = evt.wheelDelta > 0;
      }
      
      // Calculate zoom factor
      const factor = up ? 1.1 : 0.9;
      
      // Get the mouse position relative to the graph
      const rect = graph.container.getBoundingClientRect();
      const x = evt.clientX - rect.left;
      const y = evt.clientY - rect.top;
      
      // Store current view state
      const scale = graph.view.scale;
      const translate = graph.view.translate;
      
      // Calculate the point in graph coordinates
      const graphX = x / scale - translate.x;
      const graphY = y / scale - translate.y;
      
      // Apply zoom
      graph.view.scaleAndTranslate(
        scale * factor,
        translate.x - graphX * (factor - 1),
        translate.y - graphY * (factor - 1)
      );
      
      mx.mxEvent.consume(evt);
    });

    // Configure edge style
    const style = graph.getStylesheet().getDefaultEdgeStyle();

    // Apply edge routing style
    switch (edgeStyle) {
      case 'orthogonal':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ORTHOGONAL;
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
      case 'elbow':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ELBOW;
        style[mx.mxConstants.STYLE_ROUNDED] = true;
        break;
      case 'entityRelation':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ENTITY_RELATION;
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
      case 'straight':
      default:
        // Null = straight diagonal lines (no routing)
        delete style[mx.mxConstants.STYLE_EDGE];
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
    }

    style[mx.mxConstants.STYLE_STROKECOLOR] = '#4ECDC4';
    style[mx.mxConstants.STYLE_STROKEWIDTH] = 2;
    style[mx.mxConstants.STYLE_FONTCOLOR] = '#1A535C';
    style[mx.mxConstants.STYLE_FONTSIZE] = 11;
    style[mx.mxConstants.STYLE_FONTFAMILY] = 'Courier New, monospace';
    style[mx.mxConstants.STYLE_LABEL_BACKGROUNDCOLOR] = '#FFFFFF';
    style[mx.mxConstants.STYLE_LABEL_BORDERCOLOR] = '#4ECDC4';

    // Configure node (vertex) style - Entity Developer style
    const vertexStyle = graph.getStylesheet().getDefaultVertexStyle();
    vertexStyle[mx.mxConstants.STYLE_FILLCOLOR] = '#f5f5f5';
    vertexStyle[mx.mxConstants.STYLE_STROKECOLOR] = '#808080';
    vertexStyle[mx.mxConstants.STYLE_STROKEWIDTH] = 1;
    vertexStyle[mx.mxConstants.STYLE_ROUNDED] = false; // Sharp corners
    vertexStyle[mx.mxConstants.STYLE_SHADOW] = true;
    vertexStyle[mx.mxConstants.STYLE_FONTCOLOR] = '#333333';
    vertexStyle[mx.mxConstants.STYLE_FONTSIZE] = 11;
    vertexStyle[mx.mxConstants.STYLE_FONTFAMILY] = 'Segoe UI, Arial, sans-serif';
    vertexStyle[mx.mxConstants.STYLE_ALIGN] = mx.mxConstants.ALIGN_LEFT;
    vertexStyle[mx.mxConstants.STYLE_VERTICAL_ALIGN] = mx.mxConstants.ALIGN_TOP;
    vertexStyle[mx.mxConstants.STYLE_SPACING_LEFT] = 0;
    vertexStyle[mx.mxConstants.STYLE_SPACING_TOP] = 0;
    vertexStyle[mx.mxConstants.STYLE_OVERFLOW] = 'hidden';
    vertexStyle[mx.mxConstants.STYLE_WHITE_SPACE] = 'nowrap';
    vertexStyle[mx.mxConstants.STYLE_RESIZABLE] = 1; // Allow manual resize

    // Enable edge reconnection and waypoint editing
    graph.setAllowDanglingEdges(false);
    graph.setDisconnectOnMove(false);
    mx.mxEdgeHandler.prototype.addEnabled = true; // Enable adding waypoints
    mx.mxEdgeHandler.prototype.removeEnabled = true; // Enable removing waypoints

    // Allow edge label editing
    graph.setEdgeLabelsMovable(true);

    setIsInitialized(true);
  }, []);

  // Update grid settings dynamically
  useEffect(() => {
    if (!graphRef.current || !containerRef.current) return;

    const graph = graphRef.current;
    graph.setGridEnabled(gridEnabled);
    graph.gridSize = gridSize;

    // Update grid background
    if (gridEnabled) {
      graph.view.gridColor = '#e0e0e0';
      containerRef.current.style.backgroundImage = `
        linear-gradient(#e0e0e0 1px, transparent 1px),
        linear-gradient(90deg, #e0e0e0 1px, transparent 1px)
      `;
      containerRef.current.style.backgroundSize = `${gridSize}px ${gridSize}px`;
      containerRef.current.style.backgroundPosition = '0 0';
    } else {
      containerRef.current.style.backgroundImage = 'none';
    }

    // Update snap to grid
    graph.graphHandler.guidesEnabled = !gridEnabled;
    if (gridEnabled) {
      mx.mxGraphHandler.prototype.gridSize = gridSize;
    }

    graph.view.invalidate();
    graph.view.validate();
    graph.refresh();
  }, [gridEnabled, gridSize]);

  // Update edge style dynamically
  useEffect(() => {
    if (!graphRef.current) return;

    const graph = graphRef.current;
    const style = graph.getStylesheet().getDefaultEdgeStyle();

    // Apply edge routing style to default stylesheet
    switch (edgeStyle) {
      case 'orthogonal':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ORTHOGONAL;
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
      case 'elbow':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ELBOW;
        style[mx.mxConstants.STYLE_ROUNDED] = true;
        break;
      case 'entityRelation':
        style[mx.mxConstants.STYLE_EDGE] = mx.mxConstants.EDGESTYLE_ENTITY_RELATION;
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
      case 'straight':
      default:
        delete style[mx.mxConstants.STYLE_EDGE];
        style[mx.mxConstants.STYLE_ROUNDED] = false;
        break;
    }

    // Update all existing edges
    const model = graph.getModel();
    const parent = graph.getDefaultParent();
    const edges = graph.getChildEdges(parent);

    model.beginUpdate();
    try {
      edges.forEach((edge: any) => {
        let cellStyle = '';
        switch (edgeStyle) {
          case 'orthogonal':
            cellStyle = 'edgeStyle=orthogonalEdgeStyle';
            break;
          case 'elbow':
            cellStyle = 'edgeStyle=elbowEdgeStyle;rounded=1';
            break;
          case 'entityRelation':
            cellStyle = 'edgeStyle=entityRelationEdgeStyle';
            break;
          case 'straight':
          default:
            cellStyle = '';
            break;
        }
        graph.setCellStyle(cellStyle, [edge]);
      });
    } finally {
      model.endUpdate();
    }

    graph.view.invalidate();
    graph.view.validate();
    graph.refresh();
  }, [edgeStyle]);

  // Listen for control commands from parent
  useEffect(() => {
    const handleMessage = (event: MessageEvent) => {
      const message = event.data;

      if (message.type === 'mxGraphControl' && graphRef.current) {
        switch (message.command) {
          case 'zoomIn':
            handleZoomIn();
            break;
          case 'zoomOut':
            handleZoomOut();
            break;
          case 'fit':
            handleFit();
            break;
          case 'center':
            handleCenter();
            break;
          case 'saveLayout':
            saveCurrentLayout();
            break;
          case 'focusEntity':
            if (message.entityName) {
              focusOnEntity(message.entityName);
            }
            break;
        }
      }
    };

    window.addEventListener('message', handleMessage);
    return () => window.removeEventListener('message', handleMessage);
  }, []);

  useEffect(() => {
    if (!graphRef.current || !context) return;

    const graph = graphRef.current;
    const model = graph.getModel();
    const parent = graph.getDefaultParent();

    // Save current zoom and translation before clearing
    const view = graph.getView();
    const savedScale = view.scale;
    const savedTranslate = { x: view.translate.x, y: view.translate.y };

    // Clear existing content
    graph.removeCells(graph.getChildCells(parent));

    // Declare vertexMap outside try block for later use
    let vertexMap: { [key: string]: any } = {};

    // Begin update
    model.beginUpdate();
    try {
      const nodeWidth = 280;
      const nodeHeight = 180;
      const horizontalSpacing = 400;
      const verticalSpacing = 350;

      // Create vertices for each entity
      context.Entities?.forEach((entity, index) => {
        // Calculate initial position in a grid with better distribution
        const columns = Math.ceil(Math.sqrt(context.Entities?.length || 1));
        const row = Math.floor(index / columns);
        const col = index % columns;
        const x = col * horizontalSpacing + 100;
        const y = row * verticalSpacing + 100;

        // Calculate regular and navigation properties count for height calculation
        const allProperties = entity.Properties || [];
        const navigationPropertyNames = entity.Relationships?.map(r => r.NavigationProperty).filter(Boolean) || [];

        const regularProperties = allProperties.filter(prop => {
          const isCollection = prop.Type.includes('Collection') ||
                              prop.Type.includes('List<') ||
                              prop.Type.includes('HashSet<') ||
                              prop.Type.includes('IEnumerable');
          const isNavigation = navigationPropertyNames.includes(prop.Name);
          return !isCollection && !isNavigation;
        });

        const navigationProperties = allProperties.filter(prop => {
          const isCollection = prop.Type.includes('Collection') ||
                              prop.Type.includes('List<') ||
                              prop.Type.includes('HashSet<') ||
                              prop.Type.includes('IEnumerable');
          const isNavigation = navigationPropertyNames.includes(prop.Name);
          return isCollection || isNavigation;
        });

        // Calculate dynamic height based on font sizes
        // Header height + row height for each property type
        const headerHeight = Math.max(30, entityNameFontSize + 14);
        const regularRowHeight = propertyFontSize + 8;
        const navigationRowHeight = navigationPropertyFontSize + 8;
        const dynamicHeight = Math.max(100, Math.min(
          headerHeight + regularProperties.length * regularRowHeight + navigationProperties.length * navigationRowHeight,
          600
        ));

        // Create entity content HTML
        const content = createEntityContent(entity);

        const vertex = graph.insertVertex(
          parent,
          entity.Name,
          content,
          x,
          y,
          nodeWidth,
          dynamicHeight,
          'entity'
        );

        vertexMap[entity.Name] = vertex;
      });

      // Create edges for relationships
      context.Entities?.forEach(entity => {
        entity.Relationships?.forEach(rel => {
          const sourceVertex = vertexMap[entity.Name];
          const targetVertex = vertexMap[rel.TargetEntity];

          if (sourceVertex && targetVertex) {
            // Format relationship label
            let relationshipSymbol = '';
            switch(rel.Type) {
              case 'OneToOne': relationshipSymbol = '1:1'; break;
              case 'OneToMany': relationshipSymbol = '1:*'; break;
              case 'ManyToOne': relationshipSymbol = '*:1'; break;
              case 'ManyToMany': relationshipSymbol = '*:*'; break;
              default: relationshipSymbol = rel.Type;
            }

            const edgeLabel = `${rel.NavigationProperty}\n[${relationshipSymbol}]`;

            const edge = graph.insertEdge(
              parent,
              null,
              edgeLabel,
              sourceVertex,
              targetVertex,
              'relationship'
            );
            
            edge.userData = {
              ...rel,
              sourceEntity: entity.Name,
              relationshipSymbol
            };
          }
        });
      });

      // Always apply automatic layout first (savedLayout will be applied via useEffect)
      const directionMap: { [key: string]: string } = {
        'NORTH': mx.mxConstants.DIRECTION_NORTH,
        'SOUTH': mx.mxConstants.DIRECTION_SOUTH,
        'EAST': mx.mxConstants.DIRECTION_EAST,
        'WEST': mx.mxConstants.DIRECTION_WEST
      };
      const layout = new mx.mxHierarchicalLayout(graph, directionMap[layoutDirection] || mx.mxConstants.DIRECTION_NORTH);
      layout.intraCellSpacing = intraCellSpacing;
      layout.interRankCellSpacing = interRankCellSpacing;
      layout.parallelEdgeSpacing = parallelEdgeSpacing;
      layout.fineTuning = fineTuning;
      layout.tightenToSource = tightenToSource;
      layout.disableEdgeStyle = false;
      layout.resizeParent = !multigraph;
      layout.execute(parent);

    } finally {
      model.endUpdate();
    }

    // Save vertexMap for later use
    vertexMapRef.current = vertexMap;

    // Restore zoom and position if we had them (not first load)
    if (savedScale !== 1 || savedTranslate.x !== 0 || savedTranslate.y !== 0) {
      view.scale = savedScale;
      view.translate.x = savedTranslate.x;
      view.translate.y = savedTranslate.y;
      view.refresh();
    } else {
      // Only fit on first load
      graph.fit(30);
    }
  }, [context, layoutDirection, intraCellSpacing, interRankCellSpacing, parallelEdgeSpacing, fineTuning, tightenToSource, multigraph]);

  // Separate effect to apply saved layout when it changes
  useEffect(() => {
    if (!graphRef.current || !savedLayout || !savedLayout.vertices || !vertexMapRef.current) return;

    const graph = graphRef.current;
    const model = graph.getModel();
    const vertexMap = vertexMapRef.current;
    const parent = graph.getDefaultParent();

    // Check if we have vertices in the map
    if (Object.keys(vertexMap).length === 0) return;

    console.log('Applying saved layout:', savedLayout);

    model.beginUpdate();
    try {
      // Apply vertex positions
      Object.keys(savedLayout.vertices).forEach(vertexId => {
        const vertex = vertexMap[vertexId];
        if (vertex) {
          const position = savedLayout.vertices[vertexId];
          const geo = vertex.getGeometry();
          if (geo) {
            geo.x = position.x;
            geo.y = position.y;
            geo.width = position.width;
            geo.height = position.height;
          }
        }
      });

      // Apply edge waypoints (absolute coordinates)
      if (savedLayout.edges && savedLayout.edges.length > 0) {
        const edges = graph.getChildEdges(parent);

        savedLayout.edges.forEach((edgeLayout: any) => {
          // Find the edge connecting these vertices
          const edge = edges.find((e: any) =>
            e.source?.id === edgeLayout.source && e.target?.id === edgeLayout.target
          );

          if (edge) {
            const edgeGeo = edge.getGeometry();

            if (edgeGeo) {
              // Apply waypoints if they exist, otherwise clear them for direct connection
              if (edgeLayout.points && edgeLayout.points.length > 0) {
                edgeGeo.points = edgeLayout.points.map((p: any) =>
                  new mx.mxPoint(p.x, p.y)
                );
              } else {
                // Clear waypoints for direct connection
                edgeGeo.points = null;
              }
            }
          }
        });
      }
    } finally {
      model.endUpdate();
    }

    graph.view.invalidate();
    graph.view.validate();
    graph.refresh();
  }, [savedLayout]);

  // Separate effect to update font sizes without recreating nodes
  useEffect(() => {
    if (!graphRef.current || !vertexMapRef.current || !context) return;

    const graph = graphRef.current;
    const model = graph.getModel();
    const vertexMap = vertexMapRef.current;

    // Check if we have vertices in the map
    if (Object.keys(vertexMap).length === 0) return;

    model.beginUpdate();
    try {
      // Update each vertex with new content and height
      context.Entities?.forEach(entity => {
        const vertex = vertexMap[entity.Name];
        if (!vertex) return;

        // Recalculate properties for height calculation
        const allProperties = entity.Properties || [];
        const navigationPropertyNames = entity.Relationships?.map(r => r.NavigationProperty).filter(Boolean) || [];

        const regularProperties = allProperties.filter(prop => {
          const isCollection = prop.Type.includes('Collection') ||
                              prop.Type.includes('List<') ||
                              prop.Type.includes('HashSet<') ||
                              prop.Type.includes('IEnumerable');
          const isNavigation = navigationPropertyNames.includes(prop.Name);
          return !isCollection && !isNavigation;
        });

        const navigationProperties = allProperties.filter(prop => {
          const isCollection = prop.Type.includes('Collection') ||
                              prop.Type.includes('List<') ||
                              prop.Type.includes('HashSet<') ||
                              prop.Type.includes('IEnumerable');
          const isNavigation = navigationPropertyNames.includes(prop.Name);
          return isCollection || isNavigation;
        });

        // Calculate new dynamic height based on font sizes
        const headerHeight = Math.max(30, entityNameFontSize + 14);
        const regularRowHeight = propertyFontSize + 8;
        const navigationRowHeight = navigationPropertyFontSize + 8;
        const newHeight = Math.max(100, Math.min(
          headerHeight + regularProperties.length * regularRowHeight + navigationProperties.length * navigationRowHeight,
          600
        ));

        // Update vertex content with new HTML
        const newContent = createEntityContent(entity);
        model.setValue(vertex, newContent);

        // Update vertex geometry (preserve position, update height)
        const geo = vertex.getGeometry();
        if (geo) {
          const newGeo = geo.clone();
          newGeo.height = newHeight;
          model.setGeometry(vertex, newGeo);
        }
      });
    } finally {
      model.endUpdate();
    }

    // Refresh the graph view
    graph.view.invalidate();
    graph.view.validate();
    graph.refresh();
  }, [entityNameFontSize, propertyFontSize, navigationPropertyFontSize]);

  const createEntityContent = (entity: EntityInfo): string => {
    // Filter out empty properties first
    const allProperties = (entity.Properties || []).filter(p => p && p.Name && p.Name.trim() !== '');
    const navigationPropertyNames = entity.Relationships?.map(r => r.NavigationProperty) || [];
    
    const regularProperties = allProperties.filter(prop => {
      const isCollection = prop.Type.includes('Collection') || 
                          prop.Type.includes('List<') || 
                          prop.Type.includes('HashSet<') ||
                          prop.Type.includes('IEnumerable');
      const isNavigation = navigationPropertyNames.includes(prop.Name);
      return !isCollection && !isNavigation;
    });
    
    const navigationProperties = allProperties.filter(prop => {
      const isCollection = prop.Type.includes('Collection') || 
                          prop.Type.includes('List<') || 
                          prop.Type.includes('HashSet<') ||
                          prop.Type.includes('IEnumerable');
      const isNavigation = navigationPropertyNames.includes(prop.Name);
      return isCollection || isNavigation;
    });

    // Build properties as simple divs
    let propertiesHtml = '';
    
    // Add regular properties
    regularProperties.forEach(prop => {
      const isPK = prop.IsPrimaryKey;
      const isRequired = prop.IsRequired;
      const type = simplifyType(prop.Type);
      const icon = isPK ? '🔑' : '📁';
      const req = isRequired && !isPK ? '*' : '';
      propertiesHtml += `<div style="display:table-row;"><div style="display:table-cell;width:18px;padding:0 2px;text-align:center;vertical-align:middle;font-size:10px;">${icon}</div><div style="display:table-cell;padding:1px 4px;font-size:${propertyFontSize}px;color:#2c3e50;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;${isPK ? 'font-weight:bold;' : ''}">${prop.Name}${req}</div><div style="display:table-cell;width:80px;padding:1px 4px;font-size:10px;color:#888;text-align:right;white-space:nowrap;">${type}</div></div>`;
    });
    
    // Add navigation properties without header
    navigationProperties.forEach(prop => {
      const relationship = entity.Relationships?.find(r => r.NavigationProperty === prop.Name);
      const relType = relationship ? relationship.Type.replace('OneToMany', '1:*').replace('ManyToOne', '*:1').replace('OneToOne', '1:1').replace('ManyToMany', '*:*') : '';
      propertiesHtml += `<div style="display:table-row;"><div style="display:table-cell;width:18px;padding:0 2px;text-align:center;vertical-align:middle;font-size:10px;">📋</div><div style="display:table-cell;padding:1px 4px;font-size:${navigationPropertyFontSize}px;color:#16a085;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;">${prop.Name}</div><div style="display:table-cell;width:80px;padding:1px 4px;font-size:10px;color:#888;text-align:right;white-space:nowrap;">${relType}</div></div>`;
    });

    return `<div style="width:100%;height:100%;background:#f5f5f5;font-family:Segoe UI,Arial,sans-serif;"><div style="background:#e0e0e0;padding:2px 5px;display:flex;align-items:center;justify-content:space-between;"><span style="font-family:monospace;color:#666;font-size:11px;">▣</span><span style="font-size:${entityNameFontSize}px;font-weight:bold;color:#333;flex:1;margin:0 3px;">${entity.Name}</span><span style="font-size:10px;color:#666;">⚙</span></div><div style="display:table;width:100%;table-layout:fixed;border-collapse:collapse;">${propertiesHtml}</div></div>`;
  };

  const simplifyType = (type: string): string => {
    type = type.replace(/System\.Collections\.Generic\./g, '');
    type = type.replace(/ICollection<(.+?)>/g, '[$1]');
    type = type.replace(/List<(.+?)>/g, '[$1]');
    type = type.replace(/HashSet<(.+?)>/g, '[$1]');
    type = type.replace(/IEnumerable<(.+?)>/g, '[$1]');
    type = type.replace(/[\w\.]+\.(\w+)/g, '$1');
    
    type = type.replace('System.String', 'string');
    type = type.replace('System.Int32', 'int');
    type = type.replace('System.Int64', 'long');
    type = type.replace('System.Boolean', 'bool');
    type = type.replace('System.DateTime', 'DateTime');
    type = type.replace('System.Decimal', 'decimal');
    type = type.replace('System.Double', 'double');
    type = type.replace('System.Guid', 'Guid');
    
    return type;
  };

  // Save layout function with useCallback to ensure onSaveLayout is current
  const saveCurrentLayout = useCallback(() => {
    if (!graphRef.current) return;

    // Do NOT autosave when Settings panel is open - save only via "Save Settings" button
    if (showSettings) {
      console.log('[MxGraphView] Skipping autosave - Settings panel is open');
      return;
    }

    const graph = graphRef.current;
    const model = graph.getModel();
    const parent = graph.getDefaultParent();
    const cells = graph.getChildCells(parent);

    const layout: any = {
      vertices: {},
      edges: []
    };

    cells.forEach((cell: any) => {
      if (model.isVertex(cell)) {
        const geo = cell.getGeometry();
        layout.vertices[cell.id] = {
          x: geo.x,
          y: geo.y,
          width: geo.width,
          height: geo.height
        };
      } else if (model.isEdge(cell)) {
        const edgeGeo = cell.getGeometry();
        const points = edgeGeo?.points || [];

        // Store all edges, even without waypoints (to preserve direct connections)
        layout.edges.push({
          source: cell.source?.id,
          target: cell.target?.id,
          points: points.map((p: any) => ({ x: p.x, y: p.y }))
        });
      }
    });

    console.log('Saving layout:', layout);

    if (onSaveLayout) {
      onSaveLayout(layout);
    }
  }, [onSaveLayout, showSettings]);

  // Update ref when callback changes
  useEffect(() => {
    saveLayoutCallbackRef.current = saveCurrentLayout;
  }, [saveCurrentLayout]);


  // Handle entity focus and highlight
  const focusOnEntity = (entityName: string) => {
    if (!graphRef.current || !vertexMapRef.current[entityName]) return;

    const graph = graphRef.current;
    const cell = vertexMapRef.current[entityName];

    // Get original style
    const originalStrokeColor = graph.getCellStyle(cell)[mx.mxConstants.STYLE_STROKECOLOR] || '#808080';
    const originalStrokeWidth = graph.getCellStyle(cell)[mx.mxConstants.STYLE_STROKEWIDTH] || 1;

    // Highlight with thick orange border
    graph.setCellStyles(mx.mxConstants.STYLE_STROKECOLOR, '#FF6B35', [cell]);
    graph.setCellStyles(mx.mxConstants.STYLE_STROKEWIDTH, '4', [cell]);

    // Select and scroll to cell
    graph.setSelectionCell(cell);
    graph.scrollCellToVisible(cell, true);

    // Remove highlight after 3 seconds
    setTimeout(() => {
      if (graphRef.current) {
        graph.setCellStyles(mx.mxConstants.STYLE_STROKECOLOR, originalStrokeColor, [cell]);
        graph.setCellStyles(mx.mxConstants.STYLE_STROKEWIDTH, originalStrokeWidth, [cell]);
        graph.clearSelection();
      }
    }, 3000);
  };

  // Handle zoom controls
  const handleZoomIn = () => {
    if (graphRef.current) {
      graphRef.current.zoomIn();
    }
  };

  const handleZoomOut = () => {
    if (graphRef.current) {
      graphRef.current.zoomOut();
    }
  };

  const handleFit = () => {
    if (graphRef.current) {
      graphRef.current.fit();
    }
  };

  const handleCenter = () => {
    if (graphRef.current) {
      graphRef.current.center();
    }
  };

  return (
    <div className="mxgraph-container" style={{ position: 'relative', width: '100%', height: '100%' }}>
      <div 
        ref={containerRef} 
        style={{ 
          width: '100%', 
          height: '100%',
          background: 'var(--retro-bg)',
          backgroundImage: `
            repeating-linear-gradient(90deg, transparent, transparent 2px, rgba(255,255,255,.15) 2px, rgba(255,255,255,.15) 4px),
            repeating-linear-gradient(180deg, transparent, transparent 2px, rgba(255,255,255,.15) 2px, rgba(255,255,255,.15) 4px)
          `
        }}
      />
      
      {selectedRelationship && (
        <div style={{
          position: 'absolute',
          top: '20px',
          right: '20px',
          background: '#fff',
          border: '2px solid #333',
          borderRadius: '4px',
          padding: '12px 16px',
          minWidth: '250px',
          boxShadow: '0 4px 6px rgba(0,0,0,0.1)',
          fontFamily: 'Segoe UI, Arial, sans-serif',
          fontSize: '13px',
          zIndex: 1000
        }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '8px' }}>
            <span style={{ fontWeight: 'bold', color: '#333' }}>Relationship Info</span>
            <button onClick={() => setSelectedRelationship(null)} style={{
              background: 'none',
              border: 'none',
              cursor: 'pointer',
              fontSize: '16px',
              color: '#666',
              padding: '0 4px'
            }}>×</button>
          </div>
          <div style={{ borderTop: '1px solid #e0e0e0', paddingTop: '8px' }}>
            <div style={{ marginBottom: '6px' }}>
              <span style={{ color: '#888', fontSize: '11px' }}>Type:</span>
              <div style={{ color: '#333', fontWeight: 'bold' }}>{selectedRelationship.relationshipSymbol} ({selectedRelationship.Type})</div>
            </div>
            <div style={{ marginBottom: '6px' }}>
              <span style={{ color: '#888', fontSize: '11px' }}>From:</span>
              <div style={{ color: '#333' }}>{selectedRelationship.sourceEntity}</div>
            </div>
            <div style={{ marginBottom: '6px' }}>
              <span style={{ color: '#888', fontSize: '11px' }}>To:</span>
              <div style={{ color: '#333' }}>{selectedRelationship.TargetEntity}</div>
            </div>
            {selectedRelationship.NavigationProperty && (
              <div style={{ marginBottom: '6px' }}>
                <span style={{ color: '#888', fontSize: '11px' }}>Navigation:</span>
                <div style={{ color: '#333', fontFamily: 'monospace', fontSize: '12px' }}>{selectedRelationship.NavigationProperty}</div>
              </div>
            )}
            {selectedRelationship.ForeignKey && (
              <div style={{ marginBottom: '6px' }}>
                <span style={{ color: '#888', fontSize: '11px' }}>Foreign Key:</span>
                <div style={{ color: '#333', fontFamily: 'monospace', fontSize: '12px' }}>{selectedRelationship.ForeignKey}</div>
              </div>
            )}
            {selectedRelationship.PrincipalKey && (
              <div>
                <span style={{ color: '#888', fontSize: '11px' }}>Principal Key:</span>
                <div style={{ color: '#333', fontFamily: 'monospace', fontSize: '12px' }}>{selectedRelationship.PrincipalKey}</div>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default MxGraphView;