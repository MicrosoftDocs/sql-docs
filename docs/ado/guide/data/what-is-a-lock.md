---
title: "What is a Lock?"
description: "What is a Lock?"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
ms.custom: intro-overview
helpviewer_keywords:
  - "cursors [ADO], locking"
  - "locks [ADO], about locking"
---
# What is a Lock?
Locking is the process by which a DBMS restricts access to a row in a multi-user environment. When a row or column is exclusively locked, other users are not permitted to access the locked data until the lock is released. This ensures that two users cannot simultaneously update the same column in a row.  
  
 Locks can be very expensive from a resource perspective and should be used only when required to preserve data integrity. In a database where hundreds or thousands of users could be trying to access a record every second - such as a database connected to the Internet - unnecessary locking could quickly result in slower performance in your application.  
  
 You can control how the data source and the ADO cursor library manage concurrency by choosing the appropriate locking option.  
  
 Set the **LockType** property before opening a **Recordset** to specify what type of locking the provider should use when opening it. Read the property to return the type of locking in use on an open **Recordset** object.  
  
 Providers might not support all lock types. If a provider cannot support the requested **LockType** setting, it will substitute another type of locking. To determine the actual locking functionality available in a **Recordset** object, use the [Supports](../../../ado/reference/ado-api/supports-method.md) method with **adUpdate** and **adUpdateBatch**.  
  
 The **adLockPessimistic** setting is not supported if the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property is set to **adUseClient.** If an unsupported value is set, no error will result; the closest supported **LockType** will be used instead.  
  
 The **LockType** property is read/write when the **Recordset** is closed, and read-only when it is open.  
  
 This section contains the following topics.  
  
-   [Types of Locks](../../../ado/guide/data/types-of-locks.md)
