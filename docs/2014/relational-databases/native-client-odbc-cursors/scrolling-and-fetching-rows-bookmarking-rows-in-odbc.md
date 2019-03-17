---
title: "Bookmarking Rows in ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], fetching rows"
  - "ODBC cursors, fetching rows"
  - "cursors [ODBC], scrolling rows"
  - "ODBC cursors, scrolling rows"
  - "bookmarks [ODBC]"
ms.assetid: 9cfcd243-c9d4-4c2a-abc4-399dbabe5f6b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bookmarking Rows in ODBC
  A bookmark is a value used to identify a row of data. The meaning of the bookmark value is known only to the driver or data source. For example, it might be as simple as a row number or as complex as a disk address. In ODBC, the application requests a bookmark for a particular row, stores it, and passes it back to the cursor to return to the row.  
  
 When fetching rows with [SQLFetchScroll](../native-client-odbc-api/sqlfetchscroll.md), an application can use a bookmark as a basis for selecting the starting row. This is a form of absolute addressing because it does not depend on the current cursor position. To scroll to a bookmarked row, the application calls **SQLFetchScroll** with a *FetchOrientation* of SQL_FETCH_BOOKMARK. This operation uses the bookmark pointed to by the SQL_ATTR_FETCH_BOOKMARK_PTR option attribute. It returns the rowset starting with the row identified by that bookmark. An application can specify an offset for this operation in the *FetchOffset* argument of the call to **SQLFetchScroll**. When an offset is specified, the first row of the returned rowset is determined by adding the number in the FetchOffset argument to the number of the row identified by the bookmark. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver only supports bookmarks on static and keyset cursors. If a dynamic cursor is requested when bookmarks are set on, a keyset cursor is opened instead.  
  
 Bookmarks can also be used with the **SQLBulkOperations** function to perform operations on a set of rows starting at the bookmark.  
  
## See Also  
 [Scrolling and Fetching Rows](../native-client-ole-db-rowsets/fetching-rows.md)  
  
  
