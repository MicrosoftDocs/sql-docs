---
title: "SetEOS Method"
description: "SetEOS Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_SetEOS"
  - "_Stream::SetEOS"
helpviewer_keywords:
  - "SetEOS method [ADO]"
apitype: "COM"
---
# SetEOS Method
Sets the position that is the end of the stream.  
  
## Syntax  
  
```  
  
Stream.SetEOS  
```  
  
## Remarks  
 **SetEOS** updates the value of the [EOS](./eos-property.md) property, by making the current [Position](./position-property-ado.md) the end of the stream. Any bytes or characters following the current position are truncated.  
  
 Because [Write](./write-method.md), [WriteText](./writetext-method.md), and [CopyTo](./copyto-method-ado.md) do not truncate any extra values in existing **Stream** objects, you can truncate these bytes or characters by setting the new end-of-stream position with **SetEOS**.  
  
> [!CAUTION]
>  If you set **EOS** to a position before the actual end of the stream, you will lose all data after the new **EOS** position.  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)