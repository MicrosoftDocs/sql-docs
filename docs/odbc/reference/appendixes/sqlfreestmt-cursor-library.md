---
description: "SQLFreeStmt (Cursor Library)"
title: "SQLFreeStmt (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLFreeStmt function [ODBC], Cursor Library"
ms.assetid: 47bfbd4d-9453-4609-958d-1e05794cb223
author: David-Engel
ms.author: v-davidengel
---
# SQLFreeStmt (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLFreeStmt** function in the cursor library. For general information about **SQLFreeStmt**, see [SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md).  
  
 If an application calls **SQLFreeStmt** with the SQL_UNBIND option after it calls **SQLExtendedFetch**, **SQLFetch**, or **SQLFetchScroll**, the cursor library returns an error. Before it can unbind result set columns, an application must call **SQLCloseCursor** or **SQLFreeStmt** with the SQL_CLOSE option.
