---
title: "SkipLine Method"
description: "SkipLine Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_SkipLine"
  - "_Stream::SkipLine"
helpviewer_keywords:
  - "Skipline method [ADO]"
apitype: "COM"
---
# SkipLine Method
Skips one entire line when reading a text [Stream](./stream-object-ado.md).  
  
## Syntax  
  
```  
  
Stream.SkipLine  
```  
  
## Remarks  
 All characters up to and including the next line separator are skipped. By default, the [LineSeparator](./lineseparator-property-ado.md) is **adCRLF**. If you attempt to skip past [EOS](./eos-property.md), the current position will remain at **EOS**.  
  
 The **SkipLine** method is used with text streams ([Type](./type-property-ado-stream.md) is **adTypeText**).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)