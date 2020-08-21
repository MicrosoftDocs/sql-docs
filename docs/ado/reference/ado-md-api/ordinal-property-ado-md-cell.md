---
description: "Ordinal Property (ADO MD Cell)"
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
author: rothja
ms.author: jroth
---
# Ordinal Property (ADO MD Cell)
Uniquely identifies a [cell](./cell-object-ado-md.md) by its position within a cellset.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 The cell's ordinal value uniquely identifies the cell within a cellset. Conceptually, cells are numbered in a cellset as if the cellset were a *p*-dimensional array, where *p* is the number of [axes](./axes-collection-ado-md.md). Cells are numbered starting from zero in row-major order. Here is the formula for calculating the ordinal number of a cell:  
  
 The cell's ordinal value can be used with the [Item](./item-property-ado-md-cellset.md) property of the [Cellset](./cellset-object-ado-md.md) object to quickly retrieve the [Cell](./cell-object-ado-md.md).  
  
## Applies To  
 [Cell Object (ADO MD)](./cell-object-ado-md.md)  
  
## See Also  
 [Axis Example (VBScript)](./axis-example-vbscript.md)   
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)   
 [Item Property (ADO MD Cellset)](./item-property-ado-md-cellset.md)   
 [Ordinal Property (ADO MD Position)](./ordinal-property-ado-md-position.md)