---
description: "Concurrency Types"
title: "Concurrency Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [ODBC], concurrency control"
  - "concurrency control [ODBC]"
  - "locking concurrency control [ODBC]"
  - "optimistic concurrency [ODBC]"
  - "read-only concurrency control [ODBC]"
ms.assetid: 46762ae5-17dd-4777-968e-58156f470fe1
author: David-Engel
ms.author: v-davidengel
---
# Concurrency Types
To solve the problem of reduced concurrency in cursors, ODBC exposes four different types of cursor concurrency:  
  
-   **Read-only** The cursor can read data but cannot update or delete data. This is the default concurrency type. Although the DBMS might lock rows to enforce the Repeatable Read and Serializable isolation levels, it can use read locks instead of write locks. This results in higher concurrency because other transactions can at least read the data.  
  
-   **Locking** The cursor uses the lowest level of locking necessary to make sure it can update or delete rows in the result set. This usually results in very low concurrency levels, especially at the Repeatable Read and Serializable transaction isolation levels.  
  
-   **Optimistic concurrency using row versions and optimistic concurrency using values** The cursor uses optimistic concurrency: It updates or deletes rows only if they have not changed since they were last read. To detect changes, it compares row versions or values. There is no guarantee that the cursor will be able to update or delete a row, but concurrency is much higher than when locking is used. For more information, see the following section, [Optimistic Concurrency](../../../odbc/reference/develop-app/optimistic-concurrency.md).  
  
 An application specifies what type of concurrency it wants the cursor to use with the SQL_ATTR_CONCURRENCY statement attribute. To determine what types are supported, it calls **SQLGetInfo** with the SQL_SCROLL_CONCURRENCY option.
