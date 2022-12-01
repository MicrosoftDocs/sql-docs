---
title: "Errors (ADO)"
description: "Errors (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB providers [ADO]"
  - "ADO, OLE DB providers"
---
# Errors (ADO)
Any operation involving ADO objects can generate one or more provider errors. As each error occurs, one or more **Error** objects are placed in the **Errors** collection of the **Connection** object. For details about handling warnings and errors in your ADO application, see [Error Handling](./error-handling.md).  
  
 Application errors can be raised by a separate mechanism. For example, in Visual Basic, the **Err** object will contain application-level errors.