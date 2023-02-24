---
title: "LineSeparator Property (ADO)"
description: "LineSeparator Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::LineSeparator"
helpviewer_keywords:
  - "LineSeparator property [ADO]"
apitype: "COM"
---
# LineSeparator Property (ADO)
Indicates the binary character to be used as the line separator in text [Stream](./stream-object-ado.md) objects.  
  
## Settings and Return Values  
 Sets or returns a [LineSeparatorsEnum](./lineseparatorsenum.md) value that indicates the line separator character used in the **Stream**. The default value is **adCRLF**.  
  
## Remarks  
 **LineSeparator** is used to interpret lines when reading the content of a text **Stream**. Lines can be skipped with the [SkipLine](./skipline-method.md) method.  
  
 **LineSeparator** is used only with text **Stream** objects ([Type](./type-property-ado-stream.md) is **adTypeText**). This property is ignored if **Type** is **adTypeBinary**.  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [Stream Object (ADO)](./stream-object-ado.md)