---
title: "Ordinal Property (ADO MD Position)"
description: "Ordinal Property (ADO MD Position)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Ordinal property [ADO MD]"
apitype: "COM"
---
# Ordinal Property (ADO MD Position)
Uniquely identifies a [position](./position-object-ado-md.md) along an axis.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 The **Ordinal** property of a [Position](./position-object-ado-md.md) object corresponds to the index of the **Position** within the [Positions](./positions-collection-ado-md.md) collection.  
  
 A cell can quickly be retrieved using the **Ordinal** of the **Position** along each axis with the [Item](./item-property-ado-md-cellset.md) property of the [Cellset](./cellset-object-ado-md.md) object.  
  
## Applies To  
 [Position Object (ADO MD)](./position-object-ado-md.md)  
  
## See Also  
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)   
 [Item Property (ADO MD Cellset)](./item-property-ado-md-cellset.md)   
 [Ordinal Property (ADO MD Cell)](./ordinal-property-ado-md-cell.md)