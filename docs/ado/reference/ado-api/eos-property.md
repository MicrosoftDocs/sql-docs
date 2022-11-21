---
title: "EOS Property"
description: "EOS Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "EOS"
  - "Stream::EOS"
helpviewer_keywords:
  - "EOS property"
apitype: "COM"
---
# EOS Property
Indicates whether the current position is at the end of the [stream](../../../ado/reference/ado-api/stream-object-ado.md).  
  
## Return Values  
 Returns a **Boolean** value that indicates whether the current position is at the end of the stream. **EOS** returns **True** if there are no more bytes in the stream; it returns **False** if there are more bytes following the current position.  
  
 To set the end of stream position, use the [SetEOS](../../../ado/reference/ado-api/seteos-method.md) method. To determine the current position, use the [Position](../../../ado/reference/ado-api/position-property-ado.md) property.  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)  
  
## See Also  
 [EOS and LineSeparator Properties and SkipLine Method Example (VB)](../../../ado/reference/ado-api/eos-and-lineseparator-properties-and-skipline-method-example-vb.md)   
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
