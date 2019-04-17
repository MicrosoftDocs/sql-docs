---
title: "CubeDef Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "CubeDef"
helpviewer_keywords: 
  - "CubeDef object [ADO MD]"
ms.assetid: feb2581c-fc41-471c-bb69-29f8a55fda70
author: MightyPen
ms.author: genemi
manager: craigg
---
# CubeDef Object (ADO MD)
Represents a cube from a multidimensional schema, containing a set of related dimensions.  
  
## Remarks  
 With the collections and properties of a **CubeDef** object, you can do the following:  
  
-   Identify a **CubeDef** with the [Name](../../../ado/reference/ado-md-api/name-property-ado-md.md) property.  
  
-   Return a string that describes the cube with the [Description](../../../ado/reference/ado-md-api/description-property-ado-md.md) property.  
  
-   Return the dimensions that make up the cube with the [Dimensions](../../../ado/reference/ado-md-api/dimensions-collection-ado-md.md) collection.  
  
-   Obtain additional information about the **CubeDef** with the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|CreatedOn|Date and time of cube creation.|  
|CubeGUID|Cube GUID.|  
|CubeName|The name of the cube.|  
|CubeType|The type of the cube.|  
|DataUpdatedBy|User ID of the person doing the last data update.|  
|Description|A meaningful description of the cube.|  
|LastSchemaUpdate|Date and time of last schema update.|  
|SchemaName|The name of the schema to which this cube belongs.|  
|SchemaUpdatedBy|User ID of the person doing the last schema update.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/cubedef-object-properties-methods-and-events.md)  
  
## See Also  
 [CubeDef Example (VBScript)](../../../ado/reference/ado-md-api/cubedef-example-vbscript.md)   
 [Catalog Object (ADO MD)](../../../ado/reference/ado-md-api/catalog-object-ado-md.md)   
 [CubeDefs Collection (ADO MD)](../../../ado/reference/ado-md-api/cubedefs-collection-ado-md.md)   
 [Dimensions Collection (ADO MD)](../../../ado/reference/ado-md-api/dimensions-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
