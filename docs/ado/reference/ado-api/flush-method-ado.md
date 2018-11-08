---
title: "Flush Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::Flush"
  - "_Stream::raw_Flush"
helpviewer_keywords: 
  - "Flush method [ADO]"
ms.assetid: 938522b4-f836-4c80-8d27-a598a000f0ee
author: MightyPen
ms.author: genemi
manager: craigg
---
# Flush Method (ADO)
Forces the contents of the [Stream](../../../ado/reference/ado-api/stream-object-ado.md) remaining in the ADO buffer to the underlying object with which the **Stream** is associated.  
  
## Syntax  
  
```  
  
Stream.Flush  
```  
  
## Remarks  
 This method can be used to send the contents of the stream buffer to the underlying object: for example, the node or file represented by the URL that is the source of the **Stream** object. This method should be called when you want to ensure that all changes that were made to the contents of a **Stream** have been written. However, with ADO it is not usually necessary to call **Flush**, as ADO continuously flushes its buffer as much as possible in the background. Changes to the content of a **Stream** are made automatically, not cached until **Flush** is called.  
  
 Closing a **Stream** with the [Close](../../../ado/reference/ado-api/close-method-ado.md) method flushes the contents of a **Stream** automatically; there is no need to explicitly call **Flush** immediately before **Close**.  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
