---
title: "SQLCloseCursor"
description: "SQLCloseCursor"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLCloseCursor function"
apitype: "DLLExport"
---
# SQLCloseCursor
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLCloseCursor** replaces [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with an *Option* value of SQL_CLOSE. On receipt of **SQLCloseCursor**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver discards pending result set rows. Note that the statement's column and parameter bindings (if any exist) are left unaltered by **SQLCloseCursor**.  
  
## See Also  
 [SQLCloseCursor]()   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
