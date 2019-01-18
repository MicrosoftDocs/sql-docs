---
title: "WriteText Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::raw_WriteText"
  - "_Stream::WriteText"
helpviewer_keywords: 
  - "WriteText method [ADO]"
ms.assetid: 7a669048-13f4-4574-a2b1-985e089729d5
author: MightyPen
ms.author: genemi
manager: craigg
---
# WriteText Method
Writes a specified text string to a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object.  
  
## Syntax  
  
```  
  
Stream.WriteText Data, Options  
```  
  
#### Parameters  
 *Data*  
 A **String** value that contains the text in characters to be written.  
  
 *Options*  
 Optional. A [StreamWriteEnum](../../../ado/reference/ado-api/streamwriteenum.md) value that specifies whether a line separator character must be written at the end of the specified string.  
  
## Remarks  
 Specified strings are written to the **Stream** object without any intervening spaces or characters between each string.  
  
 The current [Position](../../../ado/reference/ado-api/position-property-ado.md) is set to the character following the written data. The **WriteText** method does not truncate the rest of the data in a stream. If you want to truncate these characters, call [SetEOS](../../../ado/reference/ado-api/seteos-method.md).  
  
 If you write past the current [EOS](../../../ado/reference/ado-api/eos-property.md) position, the [Size](../../../ado/reference/ado-api/size-property-ado-stream.md) of the **Stream** will be increased to contain any new characters, and **EOS** will move to the new last byte in the **Stream**.  
  
> [!NOTE]
>  The **WriteText** method is used with text streams ([Type](../../../ado/reference/ado-api/type-property-ado-stream.md) is **adTypeText**). For binary streams (**Type** is **adTypeBinary**), use [Write](../../../ado/reference/ado-api/write-method.md).  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)  
  
## See Also  
 [Write Method](../../../ado/reference/ado-api/write-method.md)
