---
description: "Retrieving Bookmarks"
title: "Retrieving Bookmarks | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "retrieving bookmarks [ODBC]"
  - "result sets [ODBC], bookmarks"
  - "bookmarks [ODBC]"
ms.assetid: a34c8f09-b786-4835-a44b-b7294c970aff
author: David-Engel
ms.author: v-davidengel
---
# Retrieving Bookmarks
If the application will use bookmarks, it must set the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE before preparing or executing the statement. This is necessary because building and maintaining bookmarks can be an expensive operation, so bookmarks should be enabled only when an application can make good use of them.  
  
 Bookmarks are returned as column 0 of the result set. There are three ways an application can retrieve them:  
  
-   Bind column 0 of the result set. **SQLFetch** or **SQLFetchScroll** returns the bookmarks for each row in the rowset along with the data for other bound columns.  
  
-   Call **SQLSetPos** to position to a row in the rowset and then call **SQLGetData** for column 0. If a driver supports bookmarks, it must always support the ability to call **SQLGetData** for column 0, even if it does not allow applications to call **SQLGetData** for other columns before the last bound column.  
  
-   Call **SQLBulkOperations** with the *Operation* argument set to SQL_ADD, and column 0 bound. The cursor inserts the row and returns the bookmark for the row in the bound buffer.
