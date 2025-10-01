import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';
import { EntityInfo } from '../types';

interface EntityNodeProps {
  data: EntityInfo;
}

const EntityNode = memo(({ data }: EntityNodeProps) => {
  // Simplify type names
  const simplifyType = (type: string): string => {
    // Remove System.Collections.Generic prefix
    type = type.replace(/System\.Collections\.Generic\./g, '');
    
    // Simplify collection types
    type = type.replace(/ICollection<(.+?)>/g, '[$1]');
    type = type.replace(/List<(.+?)>/g, '[$1]');
    type = type.replace(/HashSet<(.+?)>/g, '[$1]');
    type = type.replace(/IEnumerable<(.+?)>/g, '[$1]');
    
    // Remove namespace prefixes (keep only class name)
    type = type.replace(/[\w\.]+\.(\w+)/g, '$1');
    
    // Common type simplifications
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

  // Generate handles for each side - 10 per side
  const generateHandles = () => {
    const handles: React.ReactElement[] = [];
    const positions = [5, 15, 25, 35, 45, 55, 65, 75, 85, 95]; // 10 positions in percentages
    
    // Top handles
    positions.forEach((pos, idx) => {
      handles.push(
        <Handle
          key={`top-${idx}`}
          type={idx % 2 === 0 ? "target" : "source"}
          position={Position.Top}
          id={`handle-top-${idx}`}
          style={{ 
            background: idx % 2 === 0 ? '#4ECDC4' : '#FF6B35', 
            left: `${pos}%`,
            width: '8px',
            height: '8px'
          }}
        />
      );
    });
    
    // Bottom handles
    positions.forEach((pos, idx) => {
      handles.push(
        <Handle
          key={`bottom-${idx}`}
          type={idx % 2 === 0 ? "target" : "source"}
          position={Position.Bottom}
          id={`handle-bottom-${idx}`}
          style={{ 
            background: idx % 2 === 0 ? '#4ECDC4' : '#FF6B35', 
            left: `${pos}%`,
            width: '8px',
            height: '8px'
          }}
        />
      );
    });
    
    // Left handles
    positions.forEach((pos, idx) => {
      handles.push(
        <Handle
          key={`left-${idx}`}
          type={idx % 2 === 0 ? "target" : "source"}
          position={Position.Left}
          id={`handle-left-${idx}`}
          style={{ 
            background: idx % 2 === 0 ? '#4ECDC4' : '#FF6B35', 
            top: `${pos}%`,
            width: '8px',
            height: '8px'
          }}
        />
      );
    });
    
    // Right handles
    positions.forEach((pos, idx) => {
      handles.push(
        <Handle
          key={`right-${idx}`}
          type={idx % 2 === 0 ? "target" : "source"}
          position={Position.Right}
          id={`handle-right-${idx}`}
          style={{ 
            background: idx % 2 === 0 ? '#4ECDC4' : '#FF6B35', 
            top: `${pos}%`,
            width: '8px',
            height: '8px'
          }}
        />
      );
    });
    
    return handles;
  };

  return (
    <div className="entity-node">
      {generateHandles()}
      
      <div className="entity-header">
        <span className="entity-name">{data.Name}</span>
        <span className="table-name">[{data.TableName}]</span>
      </div>
      
      <div className="entity-properties">
        {data.Properties?.map((prop, index) => (
          <div key={index} className="property-row">
            <span className="property-name">
              {prop.IsPrimaryKey && '🔑 '}
              {prop.Name}
            </span>
            <span className="property-type">
              {simplifyType(prop.Type)}
              {prop.IsRequired && ' *'}
            </span>
          </div>
        )) || []}
      </div>
    </div>
  );
});

EntityNode.displayName = 'EntityNode';

export default EntityNode;