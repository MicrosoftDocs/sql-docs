---
title: "Type Property (ADO Stream) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::Type"
  - "_Stream::get_Type"
  - "_Stream::put_Type"
helpviewer_keywords: 
  - "Type property [ADO Stream]"
ms.assetid: f6a17e8c-7a28-48d0-bded-76b9e0cf7639
author: MightyPen
ms.author: genemi
manager: craigg
---
# Type Property (ADO Stream)
Indicates the type of data contained in the [Stream](../../../ado/reference/ado-api/stream-object-ado.md) (binary or text).  
  
## Settings and Return Values  
 Sets or returns a [StreamTypeEnum](../../../ado/reference/ado-api/streamtypeenum.md) value that specifies the type of data contained in the **Stream** object. The default value is **adTypeText**. However, if binary data is initially written to a new, empty **Stream**, the **Type** will be changed to **adTypeBinary**.  
  
## Remarks  
 The **Type** property is read/write only when the current position is at the beginning of the **Stream** ([Position](../../../ado/reference/ado-api/position-property-ado.md) is 0), and read-only at any other position.  
  
 The**Type** property determines which methods should be used for reading and writing the **Stream**. For text **Streams**, use [ReadText](../../../ado/reference/ado-api/readtext-method.md) and [WriteText](../../../ado/reference/ado-api/writetext-method.md). For binary **Streams**, use [Read](../../../ado/reference/ado-api/read-method.md) and [Write](../../../ado/reference/ado-api/write-method.md).  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)  
  
## See Also  
 [RecordType Property (ADO)](../../../ado/reference/ado-api/recordtype-property-ado.md)   
 [Type Property (ADO)](../../../ado/reference/ado-api/type-property-ado.md)
