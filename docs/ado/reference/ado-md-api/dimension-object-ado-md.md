---
title: "Dimension Object (ADO MD)"
description: "Dimension Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Dimension"
helpviewer_keywords:
  - "Dimension object [ADO MD]"
apitype: "COM"
---
# Dimension Object (ADO MD)
Represents one of the dimensions of a multidimensional cube, containing one or more hierarchies of members.  
  
## Remarks  
 With the collections and properties of a **Dimension** object, you can do the following:  
  
-   Identify the **Dimension** with the [Name](./name-property-ado-md.md) and [UniqueName](./uniquename-property-ado-md.md) properties.  
  
-   Return a meaningful string that describes the **Dimension** with the [Description](./description-property-ado-md.md) property.  
  
-   Return the [Hierarchy](./hierarchy-object-ado-md.md) objects that make up the **Dimension** with the [Hierarchies](./hierarchies-collection-ado-md.md) collection.  
  
-   Use the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection to obtain additional information about the **Dimension** object.  
  
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
  
-   [Properties, Methods, and Events](./dimension-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](./cubedef-example-vbscript.md)   
 [CubeDef Object (ADO MD)](./cubedef-object-ado-md.md)   
 [Dimensions Collection (ADO MD)](./dimensions-collection-ado-md.md)   
 [Hierarchies Collection (ADO MD)](./hierarchies-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)