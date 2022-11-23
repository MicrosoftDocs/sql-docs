---
description: "SQLCloseCursor"
title: "SQLCloseCursor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLCloseCursor function"
ms.assetid: e7134d65-5c1c-4ae2-b119-d9b4b9a42483
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLCloseCursor
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLCloseCursor** replaces [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with an *Option* value of SQL_CLOSE. On receipt of **SQLCloseCursor**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver discards pending result set rows. Note that the statement's column and parameter bindings (if any exist) are left unaltered by **SQLCloseCursor**.  
  
## See Also  
 [SQLCloseCursor]()   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
