---
title: "WriteText Method"
description: "WriteText Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_WriteText"
  - "_Stream::WriteText"
helpviewer_keywords:
  - "WriteText method [ADO]"
apitype: "COM"
---
# WriteText Method
Writes a specified text string to a [Stream](./stream-object-ado.md) object.  
  
## Syntax  
  
```  
  
Stream.WriteText Data, Options  
```  
  
#### Parameters  
 *Data*  
 A **String** value that contains the text in characters to be written.  
  
 *Options*  
 Optional. A [StreamWriteEnum](./streamwriteenum.md) value that specifies whether a line separator character must be written at the end of the specified string.  
  
## Remarks  
 Specified strings are written to the **Stream** object without any intervening spaces or characters between each string.  
  
 The current [Position](./position-property-ado.md) is set to the character following the written data. The **WriteText** method does not truncate the rest of the data in a stream. If you want to truncate these characters, call [SetEOS](./seteos-method.md).  
  
 If you write past the current [EOS](./eos-property.md) position, the [Size](./size-property-ado-stream.md) of the **Stream** will be increased to contain any new characters, and **EOS** will move to the new last byte in the **Stream**.  
  
> [!NOTE]
>  The **WriteText** method is used with text streams ([Type](./type-property-ado-stream.md) is **adTypeText**). For binary streams (**Type** is **adTypeBinary**), use [Write](./write-method.md).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [Write Method](./write-method.md)