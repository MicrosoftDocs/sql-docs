---
description: "SQLSetConnectAttr (Cursor Library)"
title: "SQLSetConnectAttr (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLSetConnectAttr function [ODBC], Cursor Library"
ms.assetid: 6f70bbd0-a057-49ef-8b05-4c80b58fc6e6
author: David-Engel
ms.author: v-davidengel
---
# SQLSetConnectAttr (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetConnectAttr** function in the cursor library. For general information about **SQLSetConnectAttr**, see [SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md).  
  
 An application calls **SQLSetConnectAttr** with the SQL_ATTR_ODBC_CURSORS attribute to specify whether the cursor library is always used, used if the driver does not support scrollable cursors, or never used. The cursor library assumes that a driver supports scrollable cursors if it returns SQL_CA1_RELATIVE for the SQL_STATIC_CURSOR_ATTRIBUTES1 information type in **SQLGetInfo**.  
  
 The application must call **SQLSetConnectAttr** to specify the cursor library usage after it calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC to allocate the connection and before it connects to the data source. If an application calls **SQLSetConnectAttr** with the SQL_ATTR_ODBC_CURSORS attribute while the connection is still active, the cursor library returns an error.  
  
 To set a statement attribute supported by the cursor library for all statements associated with a connection, an application must call **SQLSetConnectAttr** for that statement attribute after it connects to the data source and before it opens the cursor. If an application calls **SQLSetConnectAttr** with a statement attribute and a cursor is open on a statement associated with the connection, the statement attribute will not be applied to that statement until the cursor is closed and reopened.
