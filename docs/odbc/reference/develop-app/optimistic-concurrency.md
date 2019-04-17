---
title: "Optimistic Concurrency | Microsoft Docs"
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
  - "optimistic concurrency [ODBC]"
ms.assetid: 9d71e09e-bc68-4c1f-9229-ed2a7be7d324
author: MightyPen
ms.author: genemi
manager: craigg
---
# Optimistic Concurrency
*Optimistic concurrency* derives its name from the optimistic assumption that collisions between transactions will rarely occur; a collision is said to have occurred when another transaction updates or deletes a row of data between the time it is read by the current transaction and the time it is updated or deleted. It is the opposite of *pessimistic concurrency,* or locking, in which the application developer believes that such collisions are commonplace.  
  
 In optimistic concurrency, a row is left unlocked until the time comes to update or delete it. At that point, the row is reread and checked to see if it has been changed since it was last read. If the row has changed, the update or delete fails and must be tried again.  
  
 To determine whether a row has been changed, its new version is checked against a cached version of the row. This checking can be based on the row version, such as the timestamp column in SQL Server, or the values of each column in the row. Many DBMSs do not support row versions.  
  
 Optimistic concurrency can be implemented by the data source or the application. In either case, the application should use a low transaction isolation level such as Read Committed; using a higher level negates the increased concurrency gained by using optimistic concurrency.  
  
 If optimistic concurrency is implemented by the data source, the application sets the SQL_ATTR_CONCURRENCY statement attribute to SQL_CONCUR_ROWVER or SQL_CONCUR_VALUES. To update or delete a row, it executes a positioned update or delete statement or calls **SQLSetPos** just as it would with pessimistic concurrency; the driver or data source returns SQLSTATE 01001 (Cursor operation conflict) if the update or delete fails due to a collision.  
  
 If the application itself implements optimistic concurrency, it sets the SQL_ATTR_CONCURRENCY statement attribute to SQL_CONCUR_READ_ONLY to read a row. If it will compare row versions and does not know the row version column, it calls **SQLSpecialColumns** with the SQL_ROWVER option to determine the name of this column.  
  
 The application updates or deletes the row by increasing the concurrency to SQL_CONCUR_LOCK (to gain write access to the row) and executing an **UPDATE** or **DELETE** statement with a **WHERE** clause that specifies the version or values the row had when the application read it. If the row has changed since then, the statement will fail. If the **WHERE** clause does not uniquely identify the row, the statement might also update or delete other rows; row versions always uniquely identify rows, but row values uniquely identify rows only if they include the primary key.
