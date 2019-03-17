---
title: "FilterAxis Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Cellset::FilterAxis"
  - "FilterAxis"
helpviewer_keywords: 
  - "FilterAxis property [ADO MD]"
ms.assetid: 9c656963-531e-4cd1-b698-d5f42a9b7ba3
author: MightyPen
ms.author: genemi
manager: craigg
---
# FilterAxis Property (ADO MD)
Indicates filter information about the current [cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md).  
  
## Return Values  
 Returns an [Axis](../../../ado/reference/ado-md-api/axis-object-ado-md.md) object, and is read-only.  
  
## Remarks  
 Use the **FilterAxis** property to return information about the dimensions that were used to slice the data. The [DimensionCount](../../../ado/reference/ado-md-api/dimensioncount-property-ado-md.md) property of the **Axis** returns the number of slicer dimensions. This axis usually has just one row.  
  
 The **Axis** returned by **FilterAxis** is not contained in the [Axes](../../../ado/reference/ado-md-api/axes-collection-ado-md.md) collection for a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object.  
  
## Applies To  
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)  
  
## See Also  
 [Axis Object (ADO MD)](../../../ado/reference/ado-md-api/axis-object-ado-md.md)   
 [Dimension Object (ADO MD)](../../../ado/reference/ado-md-api/dimension-object-ado-md.md)   
 [DimensionCount Property (ADO MD)](../../../ado/reference/ado-md-api/dimensioncount-property-ado-md.md)
