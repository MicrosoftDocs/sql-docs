---
title: "Forward-Only Cursors | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ADO], forward-only"
  - "forward-only cursors [ADO]"
ms.assetid: 2b1e062f-3294-4a6f-8241-a17045c4df18
author: MightyPen
ms.author: genemi
manager: craigg
---
# Forward-Only Cursors
The typical default cursor type, called a forward-only (or non-scrollable) cursor, can move only forward through the result set. A forward-only cursor does not support scrolling (the ability to move forward and backward in the result set); it only supports fetching rows from the start to the end of the result set. With some forward-only cursors (such as with the SQL Server cursor library), all insert, update, and delete statements made by the current user (or committed by other users) that affect rows in the result set are visible as the rows are fetched. Because the cursor cannot be scrolled backward, however, changes made to rows in the database after the row was fetched are not visible through the cursor.  
  
 After the data for the current row is processed, the forward-only cursor releases the resources that were used to hold that data. Forward-only cursors are dynamic by default, meaning that all changes are detected as the current row is processed. This provides faster cursor opening and enables the result set to display updates made to the underlying tables.  
  
 While forward-only cursors do not support backward scrolling, your application can return to the beginning of the result set by closing and reopening the cursor. This is an effective way to work with small amounts of data. As an alternative, your application could read the result set once, cache the data locally, and then browse the local data cache.  
  
 If your application does not require scrolling through the result set, the forward-only cursor is the best way to retrieve data quickly with the least amount of overhead. Use the **adOpenForwardOnly CursorTypeEnum** to indicate that you want to use a forward-only cursor in ADO.  
  
## See Also  
 [Static Cursors](../../../ado/guide/data/static-cursors.md)   
 [Keyset Cursors](../../../ado/guide/data/keyset-cursors.md)   
 [Dynamic Cursors](../../../ado/guide/data/dynamic-cursors.md)
