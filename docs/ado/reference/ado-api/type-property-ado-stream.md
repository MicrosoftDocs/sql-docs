---
title: "Type Property (ADO Stream)"
description: "Type Property (ADO Stream)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::Type"
  - "_Stream::get_Type"
  - "_Stream::put_Type"
helpviewer_keywords:
  - "Type property [ADO Stream]"
apitype: "COM"
---
# Type Property (ADO Stream)
Indicates the type of data contained in the [Stream](./stream-object-ado.md) (binary or text).  
  
## Settings and Return Values  
 Sets or returns a [StreamTypeEnum](./streamtypeenum.md) value that specifies the type of data contained in the **Stream** object. The default value is **adTypeText**. However, if binary data is initially written to a new, empty **Stream**, the **Type** will be changed to **adTypeBinary**.  
  
## Remarks  
 The **Type** property is read/write only when the current position is at the beginning of the **Stream** ([Position](./position-property-ado.md) is 0), and read-only at any other position.  
  
 The**Type** property determines which methods should be used for reading and writing the **Stream**. For text **Streams**, use [ReadText](./readtext-method.md) and [WriteText](./writetext-method.md). For binary **Streams**, use [Read](./read-method.md) and [Write](./write-method.md).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [RecordType Property (ADO)](./recordtype-property-ado.md)   
 [Type Property (ADO)](./type-property-ado.md)