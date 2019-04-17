---
title: "SQLSetScrollOptions (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetScrollOptions function [ODBC], Cursor Library"
ms.assetid: c5c0ac6d-a6c1-4077-8186-1644df1944f8
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetScrollOptions (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetScrollOptions** function in the cursor library. For general information about **SQLSetScrollOptions**, see [SQLSetScrollOptions Function](../../../odbc/reference/syntax/sqlsetscrolloptions-function.md).  
  
 The cursor library supports **SQLSetScrollOptions** only for backward compatibility; applications should use the SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, and SQL_ATTR_ROW_ARRAY_SIZE statement attributes instead.
