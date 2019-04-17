---
title: "Read Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Stream::raw_Read"
  - "_Stream::Read"
helpviewer_keywords: 
  - "Read method [ADO]"
ms.assetid: 838502de-80f1-4eeb-8838-dd3d9403e567
author: MightyPen
ms.author: genemi
manager: craigg
---
# Read Method
Reads a specified number of bytes from a binary [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object.  
  
## Syntax  
  
```  
  
Variant = Stream.Read ( NumBytes)  
```  
  
#### Parameters  
 *NumBytes*  
 Optional. A **Long** value that specifies the number of bytes to read from the file or the [StreamReadEnum](../../../ado/reference/ado-api/streamreadenum.md) value **adReadAll**, which is the default.  
  
## Return Value  
 The **Read** method reads a specified number of bytes or the entire stream from a **Stream** object and returns the resulting data as a **Variant**.  
  
## Remarks  
 If *NumBytes* is more than the number of bytes left in the **Stream**, only the bytes remaining are returned. The data read is not padded to match the length specified by *NumBytes*. If there are no bytes left to read, a variant with a null value is returned. **Read** cannot be used to read backwards.  
  
> [!NOTE]
>  *NumBytes* always measures bytes. For text **Stream** objects ([Type](../../../ado/reference/ado-api/type-property-ado-stream.md) is **adTypeText**), use [ReadText](../../../ado/reference/ado-api/readtext-method.md).  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)  
  
## See Also  
 [ReadText Method](../../../ado/reference/ado-api/readtext-method.md)
