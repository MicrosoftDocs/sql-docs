---
title: "Read Method"
description: "Read Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Stream::raw_Read"
  - "_Stream::Read"
helpviewer_keywords:
  - "Read method [ADO]"
apitype: "COM"
---
# Read Method
Reads a specified number of bytes from a binary [Stream](./stream-object-ado.md) object.  
  
## Syntax  
  
```  
  
Variant = Stream.Read ( NumBytes)  
```  
  
#### Parameters  
 *NumBytes*  
 Optional. A **Long** value that specifies the number of bytes to read from the file or the [StreamReadEnum](./streamreadenum.md) value **adReadAll**, which is the default.  
  
## Return Value  
 The **Read** method reads a specified number of bytes or the entire stream from a **Stream** object and returns the resulting data as a **Variant**.  
  
## Remarks  
 If *NumBytes* is more than the number of bytes left in the **Stream**, only the bytes remaining are returned. The data read is not padded to match the length specified by *NumBytes*. If there are no bytes left to read, a variant with a null value is returned. **Read** cannot be used to read backwards.  
  
> [!NOTE]
>  *NumBytes* always measures bytes. For text **Stream** objects ([Type](./type-property-ado-stream.md) is **adTypeText**), use [ReadText](./readtext-method.md).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [ReadText Method](./readtext-method.md)