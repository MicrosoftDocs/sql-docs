---
description: "ODBC Dynamic Cursors"
title: "ODBC Dynamic Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ODBC], dynamic"
  - "dynamic cursors [ODBC]"
ms.assetid: de709fd3-9eb2-44e1-a2f0-786e2b9602a6
author: David-Engel
ms.author: v-davidengel
---
# ODBC Dynamic Cursors
A dynamic cursor is just that: dynamic. It can detect any changes made to the membership, order, and values of the result set after the cursor is opened. For example, suppose a dynamic cursor fetches two rows and another application then updates one of those rows and deletes the other. If the dynamic cursor then attempts to refetch those rows, it will not find the deleted row but will return the new values for the updated row.  
  
 Dynamic cursors detect all updates, deletes, and inserts, both their own and those made by others. (This is subject to the isolation level of the transaction, as set by the SQL_ATTR_TXN_ISOLATION connection attribute.) The row status array specified by the SQL_ATTR_ROW_STATUS_PTR statement attribute reflects these changes and can contain SQL_ROW_SUCCESS, SQL_ROW_SUCCESS_WITH_INFO, SQL_ROW_ERROR, SQL_ROW_UPDATED, and SQL_ROW_ADDED. It cannot return SQL_ROW_DELETED because a dynamic cursor does not return deleted rows outside the rowset and therefore no longer recognizes the existence of the deleted row in the result set or its corresponding element in the row status array. SQL_ROW_ADDED is returned only when a row is updated by a call to **SQLSetPos**, not when it is updated by another cursor.  
  
 One way of implementing dynamic cursors in the database is by creating a selective index that defines the membership and ordering of the result set. Because the index is updated when others make changes, a cursor based on such an index is sensitive to all changes. Additional selection within the result set defined by this index is possible by processing along the index.  
  
 Dynamic cursors can be simulated by requiring the result set to be ordered by a unique key. With such a restriction, fetches are made by executing a **SELECT** statement each time the cursor fetches rows. For example, suppose the result set is defined by this statement:  
  
```  
SELECT * FROM Customers ORDER BY Name, CustID  
```  
  
 To fetch the next rowset in this result set, the simulated cursor sets the parameters in the following **SELECT** statement to the values in the last row of the current rowset, and then executes it:  
  
```  
SELECT * FROM Customers WHERE (Name > ?) AND (CustID > ?)  
   ORDER BY Name, CustID  
```  
  
 This statement creates a second result set, the first rowset of which is the next rowset in the original result set - in this case, the set of rows in the Customers table. The cursor returns this rowset to the application.  
  
 It is interesting to note that a dynamic cursor implemented in this manner actually creates many result sets, which allows it to detect changes to the original result set. The application never learns of the existence of these auxiliary result sets; it simply appears as if the cursor is able to detect changes to the original result set.
