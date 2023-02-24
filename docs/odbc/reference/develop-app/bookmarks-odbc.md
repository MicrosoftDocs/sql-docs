---
description: "Bookmarks (ODBC)"
title: "Bookmarks (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "result sets [ODBC], bookmarks"
  - "bookmarks [ODBC]"
ms.assetid: 1d7cccc5-f847-4321-b240-28570854ee5c
author: David-Engel
ms.author: v-davidengel
---
# Bookmarks (ODBC)
A bookmark is a value used to identify a row of data. The meaning of the bookmark value is known only to the driver or data source. For example, it might be as simple as a row number or as complex as a disk address. Bookmarks in ODBC are a bit different from bookmarks in real books. In a real book, the reader places a bookmark at a specific page and then looks for that bookmark to return to the page. In ODBC, the application requests a bookmark for a particular row, stores it, and passes it back to the cursor to return to the row. Thus, bookmarks in ODBC are similar to a reader writing down a page number, remembering it, and then looking up the page again.  
  
 To determine a driver's support of bookmarks, an application calls **SQLGetInfo** with the SQL_BOOKMARK_PERSISTENCE option. The bits in this value describe what operations bookmarks survive, such as whether bookmarks are still valid after the cursor is closed.  
  
 This section contains the following topics.  
  
-   [Bookmark Types](../../../odbc/reference/develop-app/bookmark-types.md)  
  
-   [Retrieving Bookmarks](../../../odbc/reference/develop-app/retrieving-bookmarks.md)  
  
-   [Scrolling by Bookmark](../../../odbc/reference/develop-app/scrolling-by-bookmark.md)  
  
-   [Updating, Deleting, or Fetching by Bookmark](../../../odbc/reference/develop-app/updating-deleting-or-fetching-by-bookmark.md)  
  
-   [Comparing Bookmarks](../../../odbc/reference/develop-app/comparing-bookmarks.md)
