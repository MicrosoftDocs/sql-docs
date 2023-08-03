---
title: "ADO MD Objects"
description: "ADO MD Objects"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ADO MD, objects"
  - "objects [ADO MD]"
---
# ADO MD Objects

|Object|Description|  
|-|-|  
|[Axis](./axis-object-ado-md.md)|Represents a positional or filter axis of a cellset, containing selected members of one or more dimensions.|  
|[Catalog](./catalog-object-ado-md.md)|Contains multidimensional schema information (that is, cubes and underlying dimensions, hierarchies, levels, and members) specific to a multidimensional data provider (MDP).|  
|[Cell](./cell-object-ado-md.md)|Represents the data at the intersection of axis coordinates, contained in a cellset.|  
|[Cellset](./cellset-object-ado-md.md)|Represents the results of a multidimensional query. It is a collection of cells selected from cubes or other cellsets.|  
|[CubeDef](./cubedef-object-ado-md.md)|Represents a cube from a multidimensional schema, containing a set of related dimensions.|  
|[Dimension](./dimension-object-ado-md.md)|Represents one of the dimensions of a multidimensional cube, containing one or more hierarchies of members.|  
|[Hierarchy](./hierarchy-object-ado-md.md)|Represents one way in which the members of a dimension can be aggregated or "rolled up." A dimension can be aggregated along one or more hierarchies.|  
|[Level](./level-object-ado-md.md)|Contains a set of members, each of which has the same rank within a hierarchy.|  
|[Member](./member-object-ado-md.md)|Represents a member of a level in a cube, the children of a member of a level, or a member of a position along an axis of a cellset.|  
|[Position](./position-object-ado-md.md)|Represents a set of one or more members of different dimensions that defines a point along an axis.|  
  
 Also, the **Catalog** object is connected to an ADO **Connection** object, which is included with the standard ADO library:  
  
|Object|Description|  
|------------|-----------------|  
|[Connection](../ado-api/connection-object-ado.md)|Represents an open connection to a data source.|  
  
 The relationships between these objects are illustrated in the [ADO MD Object Model](./ado-md-object-model.md).  
  
 Many ADO MD objects can be contained in a corresponding collection. For example, a [CubeDef](./cubedef-object-ado-md.md) object can be contained in a [CubeDefs](./cubedefs-collection-ado-md.md) collection of a **Catalog**. For more information, see [ADO MD Collections](./ado-md-collections.md).  
  
## See Also  
 [ADO MD API Reference](./ado-md-object-model.md)   
 [ADO MD Code Examples](./ado-md-code-examples.md)   
 [ADO MD Collections](./ado-md-collections.md)   
 [ADO MD Enumerated Constants](./ado-md-enumerated-constants.md)   
 [ADO MD Methods](./ado-md-methods.md)   
 [ADO MD Object Model](./ado-md-object-model.md)   
 [ADO MD Properties](./ado-md-properties.md)