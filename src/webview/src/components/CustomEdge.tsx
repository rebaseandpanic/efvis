import React from 'react';
import { EdgeProps, getStraightPath, getBezierPath, getSmoothStepPath, EdgeLabelRenderer } from 'reactflow';

const CustomEdge: React.FC<EdgeProps> = ({
  id,
  sourceX,
  sourceY,
  targetX,
  targetY,
  sourcePosition,
  targetPosition,
  style = {},
  data,
  markerEnd,
  label,
  labelStyle
}) => {
  // Get path based on edge type
  const pathType = data?.pathType || 'smoothstep';
  let edgePath, labelX, labelY;
  
  if (pathType === 'bezier') {
    [edgePath, labelX, labelY] = getBezierPath({
      sourceX,
      sourceY,
      sourcePosition,
      targetX,
      targetY,
      targetPosition,
    });
  } else if (pathType === 'straight') {
    [edgePath, labelX, labelY] = getStraightPath({
      sourceX,
      sourceY,
      targetX,
      targetY,
    });
  } else {
    [edgePath, labelX, labelY] = getSmoothStepPath({
      sourceX,
      sourceY,
      sourcePosition,
      targetX,
      targetY,
      targetPosition,
    });
  }

  const sourceLabel = data?.sourceLabel || '';
  const targetLabel = data?.targetLabel || '';

  // Calculate positions for cardinality labels - closer to nodes
  const offset = 30; // Distance from node
  const dx = targetX - sourceX;
  const dy = targetY - sourceY;
  const length = Math.sqrt(dx * dx + dy * dy);
  
  // Normalize direction vector
  const nx = dx / length;
  const ny = dy / length;
  
  // Position labels close to nodes
  const sourceLabelX = sourceX + nx * offset;
  const sourceLabelY = sourceY + ny * offset;
  
  const targetLabelX = targetX - nx * offset;
  const targetLabelY = targetY - ny * offset;

  return (
    <>
      <path
        id={id}
        style={style}
        className="react-flow__edge-path"
        d={edgePath}
        markerEnd={markerEnd}
      />
      <EdgeLabelRenderer>
        {label && (
          <div
            style={{
              position: 'absolute',
              transform: `translate(-50%, -50%) translate(${labelX}px,${labelY}px)`,
              fontSize: 11,
              fontWeight: 700,
              background: 'rgba(241, 237, 225, 0.95)',
              padding: '2px 4px',
              borderRadius: 2,
              color: '#1A535C',
              border: '1px solid #4ECDC4',
              pointerEvents: 'all',
            }}
            className="nodrag nopan"
          >
            {label}
          </div>
        )}
        {sourceLabel && (
          <div
            style={{
              position: 'absolute',
              transform: `translate(-50%, -50%) translate(${sourceLabelX}px,${sourceLabelY}px)`,
              fontSize: 16,
              fontWeight: 'bold',
              background: 'rgba(241, 237, 225, 0.95)',
              padding: '1px 4px',
              borderRadius: '50%',
              color: '#FF6B35',
              border: '2px solid #FF6B35',
              width: '24px',
              height: '24px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              pointerEvents: 'none',
            }}
          >
            {sourceLabel}
          </div>
        )}
        {targetLabel && (
          <div
            style={{
              position: 'absolute',
              transform: `translate(-50%, -50%) translate(${targetLabelX}px,${targetLabelY}px)`,
              fontSize: 16,
              fontWeight: 'bold',
              background: 'rgba(241, 237, 225, 0.95)',
              padding: '1px 4px',
              borderRadius: '50%',
              color: '#FF6B35',
              border: '2px solid #FF6B35',
              width: '24px',
              height: '24px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              pointerEvents: 'none',
            }}
          >
            {targetLabel}
          </div>
        )}
      </EdgeLabelRenderer>
    </>
  );
};

export default CustomEdge;