---
description: "SQLEndTran (Cursor Library)"
title: "SQLEndTran (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLEndTran function [ODBC], Cursor Library"
ms.assetid: 92340b87-9084-4838-a509-e9ca22d5fd5c
author: David-Engel
ms.author: v-davidengel
---
# SQLEndTran (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLEndTran** function in the cursor library. For general information about **SQLEndTran**, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).  
  
 The cursor library does not support transactions and passes calls to **SQLEndTran** directly to the driver. However, the cursor library does support the cursor commit and rollback behaviors as returned by the data source with the SQL_CURSOR_ROLLBACK_BEHAVIOR and SQL_CURSOR_COMMIT_BEHAVIOR information types:  
  
-   For data sources that preserve cursors across transactions, changes that are rolled back in the data source are not rolled back in the cursor library's cache. To make the cache match the data in the data source, the application must close and reopen the cursor.  
  
-   For data sources that close cursors at transaction boundaries, the cursor library closes the cursors and deletes the caches for all statements on the connection.  
  
-   For data sources that delete prepared statements at transaction boundaries, the application must reprepare all prepared statements on the connection before reexecuting them.
