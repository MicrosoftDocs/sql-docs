---
title: "Axis Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Axis"
helpviewer_keywords: 
  - "Axis object [ADO MD]"
ms.assetid: 5f498c9a-b1e7-4e6e-9ae6-71eadaf9aada
author: MightyPen
ms.author: genemi
manager: craigg
---
# Axis Object (ADO MD)
Represents a positional or filter axis of a cellset, containing selected members of one or more dimensions.  
  
## Remarks  
 An **Axis** object can be contained by an [Axes](../../../ado/reference/ado-md-api/axes-collection-ado-md.md) collection, or returned by the [FilterAxis](../../../ado/reference/ado-md-api/filteraxis-property-ado-md.md) property of a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md).  
  
 With the collections and properties of an **Axis** object, you can do the following:  
  
-   Identify the **Axis** with the [Name](../../../ado/reference/ado-md-api/name-property-ado-md.md) property.  
  
-   Iterate through each position along an **Axis** using the [Positions](../../../ado/reference/ado-md-api/positions-collection-ado-md.md) collection.  
  
-   Obtain the number of dimensions on the **Axis** with the [DimensionCount](../../../ado/reference/ado-md-api/dimensioncount-property-ado-md.md) property.  
  
-   Obtain provider-specific attributes of the **Axis** with the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/axis-object-properties-methods-and-events.md)  
  
## See Also  
 [Axis Example (VBScript)](../../../ado/reference/ado-md-api/axis-example-vbscript.md)   
 [Axes Collection (ADO MD)](../../../ado/reference/ado-md-api/axes-collection-ado-md.md)   
 [Positions Collection (ADO MD)](../../../ado/reference/ado-md-api/positions-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
