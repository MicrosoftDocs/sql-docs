---
title: "Hierarchy Object (ADO MD)"
description: "Hierarchy Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Hierarchy"
helpviewer_keywords:
  - "Hierarchy object [ADO MD]"
apitype: "COM"
---
# Hierarchy Object (ADO MD)
Represents one way in which the members of a [dimension](./dimension-object-ado-md.md) can be aggregated or "rolled up." A dimension can be aggregated along one or more hierarchies.  
  
## Remarks  
 With the collections and properties of a **Hierarchy** object, you can do the following:  
  
-   Identify the **Hierarchy** with the [Name](./name-property-ado-md.md) and [UniqueName](./uniquename-property-ado-md.md) properties.  
  
-   Return a meaningful string that describes the **Hierarchy** with the [Description](./description-property-ado-md.md) property.  
  
-   Return the [Level](./level-object-ado-md.md) objects that make up the **Hierarchy** with the [Levels](./levels-collection-ado-md.md) collection.  
  
-   Use the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection to obtain additional information about the **Hierarchy** object.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|AllMember|The member at the highest level of rollup in the hierarchy.|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|CubeName|The name of the cube.|  
|DefaultMember|The unique name of the default member for this hierarchy.|  
|Description|A meaningful description of the hierarchy.|  
|DimensionType|The type of dimension to which this hierarchy belongs.|  
|DimensionUniqueName|The unambiguous name of the dimension.|  
|HierarchyCaption|A label or caption associated with the hierarchy.|  
|HierarchyCardinality|The number of members in the hierarchy.|  
|HierarchyGUID|The GUID of the hierarchy.|  
|HierarchyName|The name of the hierarchy.|  
|HierarchyUniqueName|The unambiguous name of the hierarchy.|  
|SchemaName|The name of the schema to which this cube belongs.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./hierarchy-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](./cubedef-example-vbscript.md)   
 [Dimension Object (ADO MD)](./dimension-object-ado-md.md)   
 [Hierarchies Collection (ADO MD)](./hierarchies-collection-ado-md.md)   
 [Levels Collection (ADO MD)](./levels-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)