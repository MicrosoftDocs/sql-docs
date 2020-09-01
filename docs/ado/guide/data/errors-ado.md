---
description: "Errors (ADO)"
title: "Errors (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "OLE DB providers [ADO]"
  - "ADO, OLE DB providers"
ms.assetid: 8ae6611b-3069-4155-b014-c0c9da37be39
author: rothja
ms.author: jroth
---
# Errors (ADO)
Any operation involving ADO objects can generate one or more provider errors. As each error occurs, one or more **Error** objects are placed in the **Errors** collection of the **Connection** object. For details about handling warnings and errors in your ADO application, see [Error Handling](./error-handling.md).  
  
 Application errors can be raised by a separate mechanism. For example, in Visual Basic, the **Err** object will contain application-level errors.