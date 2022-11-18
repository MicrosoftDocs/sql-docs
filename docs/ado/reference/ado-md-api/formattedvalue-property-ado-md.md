---
title: "FormattedValue Property (ADO MD)"
description: "FormattedValue Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Cell::FormattedValue"
  - "FormattedValue"
helpviewer_keywords:
  - "FormattedValue property [ADO MD]"
apitype: "COM"
---
# FormattedValue Property (ADO MD)
Indicates the formatted display of a [cell](./cell-object-ado-md.md) value.  
  
## Return Values  
 Returns a **String** and is read-only.  
  
## Remarks  
 Use the **FormattedValue** property to obtain the formatted display value of the [Value](./value-property-ado-md.md) property of a [Cell](./cell-object-ado-md.md) object. For example, if the value of a cell was 1056.87, and this value represented a dollar amount, **FormattedValue** would be $1,056.87.  
  
## Applies To  
 [Cell Object (ADO MD)](./cell-object-ado-md.md)  
  
## See Also  
 [Cellset Example (VB)](./cellset-example-vb.md)   
 [Value Property (ADO MD)](./value-property-ado-md.md)