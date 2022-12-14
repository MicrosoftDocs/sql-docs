---
title: "Level Object (ADO MD)"
description: "Level Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Level object [ADO MD]"
apitype: "COM"
---
# Level Object (ADO MD)
Contains a set of members, each of which has the same rank within a hierarchy.  
  
## Remarks  
 With the collections and properties of a **Level** object, you can do the following:  
  
-   Identify the **Level** with the [Name](./name-property-ado-md.md) and [UniqueName](./uniquename-property-ado-md.md) properties.  
  
-   Return a string to use when displaying the **Level** with the [Caption](./caption-property-ado-md.md) property.  
  
-   Return a meaningful string that describes the **Level** with the [Description](./description-property-ado-md.md) property.  
  
-   Return the [Member](./member-object-ado-md.md) objects that make up the **Level** with the [Members](./members-collection-ado-md.md) collection.  
  
-   Return the number of levels from the root of the **Level** with the [Depth](./depth-property-ado-md.md) property.  
  
-   Use the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection to obtain additional information about the **Level** object.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|CubeName|The name of the cube.|  
|Description|A meaningful description of the level.|  
|DimensionUniqueName|The unambiguous name of the [dimension](./dimension-object-ado-md.md).|  
|HierarchyUniqueName|The unambiguous name of the hierarchy.|  
|LevelCaption|A label or caption associated with the level.|  
|LevelCardinality|The number of members in the level.|  
|LevelGUID|The GUID of the level.|  
|LevelName|Name of the level.|  
|LevelNumber|The distance between the level and the root of the hierarchy.|  
|LevelType|The type of level.|  
|LevelUniqueName|The unambiguous name of the level.|  
|SchemaName|The name of the schema to which this cube belongs.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./level-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](./cubedef-example-vbscript.md)   
 [Hierarchy Object (ADO MD)](./hierarchy-object-ado-md.md)   
 [Levels Collection (ADO MD)](./levels-collection-ado-md.md)   
 [Members Collection (ADO MD)](./members-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)