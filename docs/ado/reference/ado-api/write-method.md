---
description: "Write Method"
title: "Write Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::raw_Write"
  - "_Stream::Write"
helpviewer_keywords: 
  - "Write method [ADO]"
ms.assetid: 02982e6a-ac5f-4af2-b82e-ce12534b84b2
author: rothja
ms.author: jroth
---
# Write Method
Writes binary data to a [Stream](./stream-object-ado.md) object.  
  
## Syntax  
  
```  
  
Stream.Write Buffer  
```  
  
#### Parameters  
 *Buffer*  
 A **Variant** that contains an array of bytes to be written.  
  
## Remarks  
 Specified bytes are written to the **Stream** object without any intervening spaces between each byte.  
  
 The current [Position](./position-property-ado.md) is set to the byte following the written data. The **Write** method does not truncate the rest of the data in a stream. If you want to truncate these bytes, call [SetEOS](./seteos-method.md).  
  
 If you write past the current [EOS](./eos-property.md) position, the [Size](./size-property-ado-stream.md) of the **Stream** will be increased to contain any new bytes, and **EOS** will move to the new last byte in the **Stream**.  
  
> [!NOTE]
>  The **Write** method is used with binary streams ([Type](./type-property-ado-stream.md) is **adTypeBinary**). For text streams (**Type** is **adTypeText**), use [WriteText](./writetext-method.md).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [WriteText Method](./writetext-method.md)