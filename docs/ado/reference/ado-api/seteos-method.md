---
title: "SetEOS Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::raw_SetEOS"
  - "_Stream::SetEOS"
helpviewer_keywords: 
  - "SetEOS method [ADO]"
ms.assetid: 707c18ca-6a56-4970-bbd6-ae1fb86a0b8a
author: MightyPen
ms.author: genemi
manager: craigg
---
# SetEOS Method
Sets the position that is the end of the stream.  
  
## Syntax  
  
```  
  
Stream.SetEOS  
```  
  
## Remarks  
 **SetEOS** updates the value of the [EOS](../../../ado/reference/ado-api/eos-property.md) property, by making the current [Position](../../../ado/reference/ado-api/position-property-ado.md) the end of the stream. Any bytes or characters following the current position are truncated.  
  
 Because [Write](../../../ado/reference/ado-api/write-method.md), [WriteText](../../../ado/reference/ado-api/writetext-method.md), and [CopyTo](../../../ado/reference/ado-api/copyto-method-ado.md) do not truncate any extra values in existing **Stream** objects, you can truncate these bytes or characters by setting the new end-of-stream position with **SetEOS**.  
  
> [!CAUTION]
>  If you set **EOS** to a position before the actual end of the stream, you will lose all data after the new **EOS** position.  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
