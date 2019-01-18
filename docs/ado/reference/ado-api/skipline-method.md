---
title: "SkipLine Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::raw_SkipLine"
  - "_Stream::SkipLine"
helpviewer_keywords: 
  - "Skipline method [ADO]"
ms.assetid: 0abc00fe-ee09-4c8e-b1f2-48ee9c5f3329
author: MightyPen
ms.author: genemi
manager: craigg
---
# SkipLine Method
Skips one entire line when reading a text [Stream](../../../ado/reference/ado-api/stream-object-ado.md).  
  
## Syntax  
  
```  
  
Stream.SkipLine  
```  
  
## Remarks  
 All characters up to and including the next line separator are skipped. By default, the [LineSeparator](../../../ado/reference/ado-api/lineseparator-property-ado.md) is **adCRLF**. If you attempt to skip past [EOS](../../../ado/reference/ado-api/eos-property.md), the current position will remain at **EOS**.  
  
 The **SkipLine** method is used with text streams ([Type](../../../ado/reference/ado-api/type-property-ado-stream.md) is **adTypeText**).  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
