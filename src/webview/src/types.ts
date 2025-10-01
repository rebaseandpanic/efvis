export interface VSCodeAPI {
  postMessage(message: any): void;
  getState(): any;
  setState(state: any): void;
}

export interface DbContextInfo {
  Name: string;
  Namespace: string;
  FilePath: string;
  Entities: EntityInfo[];
}

export interface EntityInfo {
  Name: string;
  TableName: string;
  Properties: PropertyInfo[];
  Relationships: RelationshipInfo[];
}

export interface PropertyInfo {
  Name: string;
  Type: string;
  IsPrimaryKey: boolean;
  IsRequired: boolean;
  MaxLength?: number;
}

export interface RelationshipInfo {
  Type: string; // OneToMany, ManyToOne, OneToOne, ManyToMany
  TargetEntity: string;
  ForeignKey?: string;
  NavigationProperty: string;
}