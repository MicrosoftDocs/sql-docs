---
title: "Dimension Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Dimension"
helpviewer_keywords: 
  - "Dimension object [ADO MD]"
ms.assetid: 66adbbd2-23a3-4c19-a91b-84c31309aa1b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Dimension Object (ADO MD)
Represents one of the dimensions of a multidimensional cube, containing one or more hierarchies of members.  
  
## Remarks  
 With the collections and properties of a **Dimension** object, you can do the following:  
  
-   Identify the **Dimension** with the [Name](../../../ado/reference/ado-md-api/name-property-ado-md.md) and [UniqueName](../../../ado/reference/ado-md-api/uniquename-property-ado-md.md) properties.  
  
-   Return a meaningful string that describes the **Dimension** with the [Description](../../../ado/reference/ado-md-api/description-property-ado-md.md) property.  
  
-   Return the [Hierarchy](../../../ado/reference/ado-md-api/hierarchy-object-ado-md.md) objects that make up the **Dimension** with the [Hierarchies](../../../ado/reference/ado-md-api/hierarchies-collection-ado-md.md) collection.  
  
-   Use the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection to obtain additional information about the **Dimension** object.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|CubeName|The name of the cube.|  
|DefaultHierarchy|The unique name of the default hierarchy.|  
|Description|A meaningful description of the cube.|  
|DimensionCaption|A label or caption associated with the dimension.|  
|DimensionCardinality|The number of members in the dimension.|  
|DimensionGUID|The GUID of the dimension.|  
|DimensionName|The name of the dimension.|  
|DimensionOrdinal|The ordinal number of the dimension among the group of dimensions that form the cube.|  
|DimensionType|The dimension type.|  
|DimensionUniqueName|The unambiguous name of the dimension.|  
|SchemaName|The name of the schema to which this cube belongs.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/dimension-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](../../../ado/reference/ado-md-api/cubedef-example-vbscript.md)   
 [CubeDef Object (ADO MD)](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md)   
 [Dimensions Collection (ADO MD)](../../../ado/reference/ado-md-api/dimensions-collection-ado-md.md)   
 [Hierarchies Collection (ADO MD)](../../../ado/reference/ado-md-api/hierarchies-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
