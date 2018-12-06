---
title: "Concurrency Control | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [ODBC], concurrency control"
  - "concurrency control [ODBC]"
ms.assetid: 75e4adb3-3d43-49c5-8c5e-8df96310d912
author: MightyPen
ms.author: genemi
manager: craigg
---
# Concurrency Control
*Concurrency* is the ability of two transactions to use the same data at the same time, and with increased transaction isolation usually comes reduced concurrency. This is because transaction isolation is usually implemented by locking rows, and as more rows are locked, fewer transactions can be completed without being blocked at least temporarily by a locked row. While reduced concurrency is generally accepted as a trade-off for the higher transaction isolation levels necessary to maintain database integrity, it can become a problem in interactive applications with high read/write activity that use cursors.  
  
 For example, suppose an application executes the SQL statement **SELECT \* FROM Orders**. It calls **SQLFetchScroll** to scroll around the result set and allows the user to update, delete, or insert orders. After the user updates, deletes, or inserts an order, the application commits the transaction.  
  
 If the isolation level is Repeatable Read, the transaction might - depending on how it is implemented - lock each row returned by **SQLFetchScroll**. If the isolation level is Serializable, the transaction might lock the entire Orders table. In either case, the transaction releases its locks only when it is committed or rolled back. So if the user spends a lot of time reading orders and very little time updating, deleting, or inserting them, the transaction could easily lock a large number of rows, making them unavailable to other users.  
  
 This is a problem even if the cursor is read-only and the application allows the user to read only existing orders. In this case, the application commits the transaction, and releases locks, when it calls **SQLCloseCursor** (in auto-commit mode) or **SQLEndTran** (in manual-commit mode).  
  
 This section contains the following topics.  
  
-   [Concurrency Types](../../../odbc/reference/develop-app/concurrency-types.md)  
  
-   [Optimistic Concurrency](../../../odbc/reference/develop-app/optimistic-concurrency.md)
