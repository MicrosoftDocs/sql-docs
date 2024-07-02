---
title: "SQLGetFunctions (Cursor Library)"
description: "SQLGetFunctions (Cursor Library)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetFunctions function [ODBC], Cursor Library"
---
# SQLGetFunctions (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLGetFunctions** function in the cursor library. For general information about **SQLGetFunctions**, see [SQLGetFunctions Function](../../../odbc/reference/syntax/sqlgetfunctions-function.md).  
  
 When you call **SQLGetFunctions**, the cursor library returns that it supports **SQLExtendedFetch**, **SQLFetchScroll**, **SQLSetPos**, and **SQLSetScrollOptions**, in addition to the functions supported by the driver.
