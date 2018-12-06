---
title: "Rowset Size | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "rowset size [ODBC]"
  - "cursors [ODBC], block"
  - "result sets [ODBC], rowset size"
  - "block cursors [ODBC]"
  - "result sets [ODBC], block cursors"
ms.assetid: 60366ae8-175c-456a-ae5e-bdd860786911
author: MightyPen
ms.author: genemi
manager: craigg
---
# Rowset Size
Which rowset size to use depends on the application. Screen-based applications commonly follow one of two strategies. The first is to set the rowset size to the number of rows displayed on the screen; if the user resizes the screen, the application changes the rowset size accordingly. The second is to set the rowset size to a larger number, such as 100, which reduces the number of calls to the data source. The application scrolls locally within the rowset when possible and fetches new rows only when it scrolls outside the rowset.  
  
 Other applications, such as reports, tend to set the rowset size to the largest number of rows the application can reasonably handle - with a larger rowset, the network overhead per row is sometimes reduced. Exactly how large a rowset can be depends on the size of each row and the amount of memory available.  
  
 Rowset size is set by a call to **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_ROW_ARRAY_SIZE. The application can change the rowset size, bind new rowset buffers (by calling **SQLBindCol** or specifying a binding offset) even after rows have been fetched, or both. The implications of changing the rowset size depend on the function:  
  
-   **SQLFetch** and **SQLFetchScroll** use the rowset size at the time of the call to determine how many rows to fetch. However, **SQLFetchScroll** with a *FetchOrientation* of SQL_FETCH_NEXT increments the cursor based on the rowset of the previous fetch and then fetches a rowset based on the current rowset size.  
  
-   **SQLSetPos** uses the rowset size that is in effect as of the preceding call to **SQLFetch** or **SQLFetchScroll**, because **SQLSetPos** operates on a rowset that has already been set. **SQLSetPos** also will pick up the new rowset size if **SQLBulkOperations** has been called after the rowset size was changed.  
  
-   **SQLBulkOperations** uses the rowset size in effect at the time of the call, because it performs operations on a table independent of any fetched rowset.
