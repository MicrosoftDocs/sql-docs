---
title: "Freeing a Statement Handle"
description: "Freeing a Statement Handle"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "reusing statement handles"
  - "freeing statement handles"
  - "statements [ODBC], statement handles"
  - "SQLFreeStmt function"
  - "ODBC applications, statements"
  - "statement handles [ODBC]"
---
# Freeing a Statement Handle
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  It is more efficient to reuse statement handles than to drop them and allocate new ones. Before executing a new SQL statement on a statement handle, applications should verify that the current statement settings are appropriate. These include statement attributes, parameter bindings, and result set bindings. Generally, parameters and result sets for the old SQL statement must be unbound by calling [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with the SQL_RESET_PARAMS and SQL_UNBIND options and then re-bound for the new SQL statement.  
  
 When the application has finished using the statement, it calls [SQLFreeHandle](../../relational-databases/native-client-odbc-api/sqlfreehandle.md) to free the statement. Note that **SQLDisconnect** automatically frees all statements on a connection.  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)  
  
  
