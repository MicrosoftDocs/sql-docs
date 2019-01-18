---
title: "Ordinal Property (ADO MD Cell) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Cell::Ordinal"
  - "Ordinal"
helpviewer_keywords: 
  - "Ordinal property [ADO MD]"
ms.assetid: a6001168-b954-47f0-ba0d-c05c4cc40c58
author: MightyPen
ms.author: genemi
manager: craigg
---
# Ordinal Property (ADO MD Cell)
Uniquely identifies a [cell](../../../ado/reference/ado-md-api/cell-object-ado-md.md) by its position within a cellset.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 The cell's ordinal value uniquely identifies the cell within a cellset. Conceptually, cells are numbered in a cellset as if the cellset were a *p*-dimensional array, where *p* is the number of [axes](../../../ado/reference/ado-md-api/axes-collection-ado-md.md). Cells are numbered starting from zero in row-major order. Here is the formula for calculating the ordinal number of a cell:  
  
 The cell's ordinal value can be used with the [Item](../../../ado/reference/ado-md-api/item-property-ado-md-cellset.md) property of the [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object to quickly retrieve the [Cell](../../../ado/reference/ado-md-api/cell-object-ado-md.md).  
  
## Applies To  
 [Cell Object (ADO MD)](../../../ado/reference/ado-md-api/cell-object-ado-md.md)  
  
## See Also  
 [Axis Example (VBScript)](../../../ado/reference/ado-md-api/axis-example-vbscript.md)   
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)   
 [Item Property (ADO MD Cellset)](../../../ado/reference/ado-md-api/item-property-ado-md-cellset.md)   
 [Ordinal Property (ADO MD Position)](../../../ado/reference/ado-md-api/ordinal-property-ado-md-position.md)
