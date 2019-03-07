---
title: "SQLNativeSql (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLNativeSql function [ODBC], Cursor Library"
ms.assetid: c4459092-1177-4b2a-b7f5-e0083d3bf2b2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLNativeSql (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLNativeSql** function in the cursor library. For general information about **SQLNativeSql**, see [SQLNativeSql Function](../../../odbc/reference/syntax/sqlnativesql-function.md).  
  
 If the driver supports this function, the cursor library calls **SQLNativeSql** in the driver and passes it the SQL statement. For positioned update, positioned delete, and **SELECT FOR UPDATE** statements, the cursor library modifies the statement before passing it to the driver.  
  
> [!NOTE]  
>  The cursor library incorrectly returns SQLSTATE 34000 (Invalid cursor name) if the cursor name is invalid in a positioned update or delete statement that is passed in the *InStatementText* argument of **SQLNativeSql**. **SQLNativeSql** is not intended to return syntax errors, which are returned only upon statement preparation or execution.
