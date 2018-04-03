---
title: "Freeing a Statement Handle | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "reusing statement handles"
  - "freeing statement handles"
  - "statements [ODBC], statement handles"
  - "SQLFreeStmt function"
  - "ODBC applications, statements"
  - "statement handles [ODBC]"
ms.assetid: 96fdff84-0ca7-460a-a240-94ee826ea41c
caps.latest.revision: 31
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Freeing a Statement Handle
  It is more efficient to reuse statement handles than to drop them and allocate new ones. Before executing a new SQL statement on a statement handle, applications should verify that the current statement settings are appropriate. These include statement attributes, parameter bindings, and result set bindings. Generally, parameters and result sets for the old SQL statement must be unbound by calling [SQLFreeStmt](../../../2014/database-engine/dev-guide/sqlfreestmt.md) with the SQL_RESET_PARAMS and SQL_UNBIND options and then re-bound for the new SQL statement.  
  
 When the application has finished using the statement, it calls [SQLFreeHandle](../../../2014/database-engine/dev-guide/sqlfreehandle.md) to free the statement. Note that **SQLDisconnect** automatically frees all statements on a connection.  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../../../2014/database-engine/dev-guide/executing-queries-odbc.md)  
  
  