---
title: "Hierarchy Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Hierarchy"
helpviewer_keywords: 
  - "Hierarchy object [ADO MD]"
ms.assetid: 034af340-ac79-494e-ba5e-2b57da1cb9de
author: MightyPen
ms.author: genemi
manager: craigg
---
# Hierarchy Object (ADO MD)
Represents one way in which the members of a [dimension](../../../ado/reference/ado-md-api/dimension-object-ado-md.md) can be aggregated or "rolled up." A dimension can be aggregated along one or more hierarchies.  
  
## Remarks  
 With the collections and properties of a **Hierarchy** object, you can do the following:  
  
-   Identify the **Hierarchy** with the [Name](../../../ado/reference/ado-md-api/name-property-ado-md.md) and [UniqueName](../../../ado/reference/ado-md-api/uniquename-property-ado-md.md) properties.  
  
-   Return a meaningful string that describes the **Hierarchy** with the [Description](../../../ado/reference/ado-md-api/description-property-ado-md.md) property.  
  
-   Return the [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) objects that make up the **Hierarchy** with the [Levels](../../../ado/reference/ado-md-api/levels-collection-ado-md.md) collection.  
  
-   Use the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection to obtain additional information about the **Hierarchy** object.  
  
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
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/hierarchy-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](../../../ado/reference/ado-md-api/cubedef-example-vbscript.md)   
 [Dimension Object (ADO MD)](../../../ado/reference/ado-md-api/dimension-object-ado-md.md)   
 [Hierarchies Collection (ADO MD)](../../../ado/reference/ado-md-api/hierarchies-collection-ado-md.md)   
 [Levels Collection (ADO MD)](../../../ado/reference/ado-md-api/levels-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
