---
description: "Updating, Deleting, or Fetching by Bookmark"
title: "Updating, Deleting, or Fetching by Bookmark | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "updating by bookmarks [ODBC]"
  - "result sets [ODBC], bookmarks"
  - "fetches [ODBC], by bookmarks [ODBC]"
  - "deleting by bookmarks [ODBC]"
  - "bookmarks [ODBC]"
ms.assetid: e2ee58d7-c28f-435f-b537-06207215dd2f
author: David-Engel
ms.author: v-davidengel
---
# Updating, Deleting, or Fetching by Bookmark
Bookmarks can be used to identify data to be updated in the result set, deleted from the result set, or fetched from the result set to the rowset buffers. These operations are performed by a call to **SQLBulkOperations** with an *Option* argument of SQL_UPDATE_BY_BOOKMARK, SQL_DELETE_BY_BOOKMARK, or SQL_FETCH_BY_BOOKMARK. The bookmarks used in these operations are stored in column 0 of the rowset buffers. When updating by bookmark, the data that result set columns are updated to is retrieved from the rowset buffers. For more information, see [Updating Data with SQLBulkOperations](../../../odbc/reference/develop-app/updating-data-with-sqlbulkoperations.md).
