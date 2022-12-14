---
description: "Limitations of Using Keyset-Driven Cursors"
title: "Limitations of Using Keyset-Driven Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC driver for Oracle [ODBC], cursors"
  - "keyset-driven cursors [ODBC]"
ms.assetid: 59d86fed-387c-4719-9550-36343e74da44
author: David-Engel
ms.author: v-davidengel
---
# Limitations of Using Keyset-Driven Cursors
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 You must be able to retrieve a single ROWID column for the table queried. A keyset-driven cursor cannot be used on joins, queries, or statements that contain DISTINCT, GROUP BY, UNION, INTERSECT, or MINUS clauses.  
  
 Also, if your application uses table aliases, keyset-driven cursors will not work; forward-only or static cursor types are required. Using the keyset cursor type with table aliases will cause the following error: "[Microsoft][ODBC driver for Oracle]Cannot use Keyset-driven cursor on join, with union, intersect or minus or on read only result set."  
  
> [!NOTE]  
>  Because of the way the driver handles the SQL statement that is sent to the Oracle server, Oracle internally returns the following error message: "ORA-00964: table name not in FROM list."
