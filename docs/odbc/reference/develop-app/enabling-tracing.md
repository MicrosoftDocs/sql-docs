---
description: "Enabling Tracing"
title: "Enabling Tracing | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "tracing options [ODBC], enabling"
ms.assetid: 48e318bd-2487-4708-a698-ea01f36a45e9
author: David-Engel
ms.author: v-davidengel
---
# Enabling Tracing
Tracing can be enabled in the following three ways:  
  
-   Set the **Trace** and **TraceFile** keywords in the Odbc.ini registry entry. This enables or disables tracing when **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV is called. These options are set in the Tracing tab of the ODBC Data Source Administrator dialog box displayed during data source setup. For more information, see [Registry Entries for Data Sources](../../../odbc/reference/install/registry-entries-for-data-sources.md).  
  
-   Call **SQLSetConnectAttr** to set the SQL_ATTR_TRACE connection attribute to SQL_OPT_TRACE_ON. This enables or disables tracing for the duration of the connection. For more information, see the [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md) function description.  
  
-   Use **ODBCSharedTraceFlag** to turn tracing on or off dynamically. (For more information, see the next topic, [Dynamic Tracing](../../../odbc/reference/develop-app/dynamic-tracing.md).)
