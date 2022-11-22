---
title: "Position Property (ADO)"
description: "Position Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::Position"
helpviewer_keywords:
  - "Position property [ADO]"
apitype: "COM"
---
# Position Property (ADO)
Indicates the current position within a [Stream](./stream-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that specifies the offset, in number of bytes, of the current position from the beginning of the stream. The default is 0, which represents the first byte in the stream.  
  
## Remarks  
 The current position can be moved to a point after the end of the stream. If you specify the current position beyond the end of the stream, the [Size](./size-property-ado-stream.md) of the **Stream** object will be increased accordingly. Any new bytes added in this way will be null.  
  
> [!NOTE]
>  **Position** always measures bytes. For text streams using multibyte character sets, multiply the position by the character size to determine the character number. For example, for a two-byte character set, the first character is at position 0, the second character at position 2, the third character at position 4, and so on.  
  
> [!NOTE]
>  Negative values cannot be used to change the current position in a **Stream**. Only positive numbers can be used for **Position**.  
  
> [!NOTE]
>  For read-only **Stream** objects, ADO will not return an error if **Position** is set to a value greater than the **Size** of the **Stream**. This does not change the size of the **Stream**, or alter the **Stream** contents in any way. However, doing this should be avoided because it results in a meaningless **Position**value.  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [Charset Property (ADO)](./charset-property-ado.md)