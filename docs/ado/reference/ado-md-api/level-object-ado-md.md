---
title: "Level Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Level"
helpviewer_keywords: 
  - "Level object [ADO MD]"
ms.assetid: 37815869-ed30-45fd-9aea-0a986c1b305c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Level Object (ADO MD)
Contains a set of members, each of which has the same rank within a hierarchy.  
  
## Remarks  
 With the collections and properties of a **Level** object, you can do the following:  
  
-   Identify the **Level** with the [Name](../../../ado/reference/ado-md-api/name-property-ado-md.md) and [UniqueName](../../../ado/reference/ado-md-api/uniquename-property-ado-md.md) properties.  
  
-   Return a string to use when displaying the **Level** with the [Caption](../../../ado/reference/ado-md-api/caption-property-ado-md.md) property.  
  
-   Return a meaningful string that describes the **Level** with the [Description](../../../ado/reference/ado-md-api/description-property-ado-md.md) property.  
  
-   Return the [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) objects that make up the **Level** with the [Members](../../../ado/reference/ado-md-api/members-collection-ado-md.md) collection.  
  
-   Return the number of levels from the root of the **Level** with the [Depth](../../../ado/reference/ado-md-api/depth-property-ado-md.md) property.  
  
-   Use the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection to obtain additional information about the **Level** object.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|CubeName|The name of the cube.|  
|Description|A meaningful description of the level.|  
|DimensionUniqueName|The unambiguous name of the [dimension](../../../ado/reference/ado-md-api/dimension-object-ado-md.md).|  
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
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/level-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](../../../ado/reference/ado-md-api/cubedef-example-vbscript.md)   
 [Hierarchy Object (ADO MD)](../../../ado/reference/ado-md-api/hierarchy-object-ado-md.md)   
 [Levels Collection (ADO MD)](../../../ado/reference/ado-md-api/levels-collection-ado-md.md)   
 [Members Collection (ADO MD)](../../../ado/reference/ado-md-api/members-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
