---
title: "FilterAxis Property (ADO MD)"
description: "FilterAxis Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Cellset::FilterAxis"
  - "FilterAxis"
helpviewer_keywords:
  - "FilterAxis property [ADO MD]"
apitype: "COM"
---
# FilterAxis Property (ADO MD)
Indicates filter information about the current [cellset](./cellset-object-ado-md.md).  
  
## Return Values  
 Returns an [Axis](./axis-object-ado-md.md) object, and is read-only.  
  
## Remarks  
 Use the **FilterAxis** property to return information about the dimensions that were used to slice the data. The [DimensionCount](./dimensioncount-property-ado-md.md) property of the **Axis** returns the number of slicer dimensions. This axis usually has just one row.  
  
 The **Axis** returned by **FilterAxis** is not contained in the [Axes](./axes-collection-ado-md.md) collection for a [Cellset](./cellset-object-ado-md.md) object.  
  
## Applies To  
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)  
  
## See Also  
 [Axis Object (ADO MD)](./axis-object-ado-md.md)   
 [Dimension Object (ADO MD)](./dimension-object-ado-md.md)   
 [DimensionCount Property (ADO MD)](./dimensioncount-property-ado-md.md)