---
title: "Flush Method (ADO)"
description: "Flush Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::Flush"
  - "_Stream::raw_Flush"
helpviewer_keywords:
  - "Flush method [ADO]"
apitype: "COM"
---
# Flush Method (ADO)
Forces the contents of the [Stream](./stream-object-ado.md) remaining in the ADO buffer to the underlying object with which the **Stream** is associated.  
  
## Syntax  
  
```  
  
Stream.Flush  
```  
  
## Remarks  
 This method can be used to send the contents of the stream buffer to the underlying object: for example, the node or file represented by the URL that is the source of the **Stream** object. This method should be called when you want to ensure that all changes that were made to the contents of a **Stream** have been written. However, with ADO it is not usually necessary to call **Flush**, as ADO continuously flushes its buffer as much as possible in the background. Changes to the content of a **Stream** are made automatically, not cached until **Flush** is called.  
  
 Closing a **Stream** with the [Close](./close-method-ado.md) method flushes the contents of a **Stream** automatically; there is no need to explicitly call **Flush** immediately before **Close**.  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)