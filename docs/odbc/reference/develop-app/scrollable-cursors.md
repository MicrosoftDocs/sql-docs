---
description: "Scrollable Cursors"
title: "Scrollable Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], scrollable"
ms.assetid: 2c8a5f50-9b37-452f-8160-05f42bc4d97e
author: David-Engel
ms.author: v-davidengel
---
# Scrollable Cursors
In modern screen-based applications, the user scrolls backward and forward through the data. For such applications, returning to a previously fetched row is a problem. One possibility is to close and reopen the cursor and then fetch rows until the cursor reaches the required row. Another possibility is to read the result set, cache it locally, and implement scrolling in the application. Both possibilities work well only with small result sets, and the latter possibility is difficult to implement. A better solution is to use a *scrollable cursor,* which can move backward and forward in the result set.  
  
 A *scrollable cursor* is commonly used in modern screen-based applications in which the user scrolls back and forth through the data. However, applications should use scrollable cursors only when forward-only cursors will not do the job, as scrollable cursors are generally more expensive than forward-only cursors.  
  
 The ability to move backward raises a question not applicable to forward-only cursors: Should a scrollable cursor detect changes made to rows previously fetched? That is, should it detect updated, deleted, and newly inserted rows?  
  
 This question arises because the definition of a result set - the set of rows that matches certain criteria - does not state when rows are checked to see whether they match that criteria, nor does it state whether rows must contain the same data each time they are fetched. The former omission makes it possible for scrollable cursors to detect whether rows have been inserted or deleted, while the latter makes it possible for them to detect updated data.  
  
 The ability to detect changes is sometimes useful, sometimes not. For example, an accounting application needs a cursor that ignores all changes; balancing books is impossible if the cursor shows the latest changes. On the other hand, an airline reservation system needs a cursor that shows the latest changes to the data; without such a cursor, it must continually requery the database to show the most up-to-date flight availability.  
  
 To cover the needs of different applications, ODBC defines four different types of scrollable cursors. These cursors vary both in expense and in their ability to detect changes to the result set. Note that if a scrollable cursor can detect changes to rows, it can only detect them when it attempts to refetch those rows; there is no way for the data source to notify the cursor of changes to the currently fetched rows. Note as well that visibility of changes is also controlled by the transaction isolation level; for more information, see [Transaction Isolation](../../../odbc/reference/develop-app/transaction-isolation.md).  
  
 This section contains the following topics.  
  
-   [Scrollable Cursor Types](../../../odbc/reference/develop-app/scrollable-cursor-types.md)  
  
-   [Using Scrollable Cursors](../../../odbc/reference/develop-app/using-scrollable-cursors.md)  
  
-   [Relative and Absolute Scrolling](../../../odbc/reference/develop-app/relative-and-absolute-scrolling.md)  
  
-   [Bookmarks](../../../odbc/reference/develop-app/bookmarks-odbc.md)
