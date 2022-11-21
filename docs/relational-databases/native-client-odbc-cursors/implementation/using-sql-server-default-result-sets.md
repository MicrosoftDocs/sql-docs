---
description: "Using SQL Server Default Result Sets"
title: "Using SQL Server Default Result Sets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLSetStmtAttr function"
  - "ODBC cursors, default result sets"
  - "cursors [ODBC], default result sets"
  - "default result sets"
  - "result sets [ODBC], default"
  - "ODBC applications, cursors"
ms.assetid: ee1db3e5-60eb-4425-8a6b-977eeced3f98
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using SQL Server Default Result Sets
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The default ODBC cursor attributes are:  
  
```  
SQLSetStmtAttr(hstmt, SQL_ATTR_CURSOR_TYPE, SQL_CURSOR_FORWARD_ONLY, SQL_IS_INTEGER);  
SQLSetStmtAttr(hstmt, SQL_ATTR_CONCURRENCY, SQL_CONCUR_READ_ONLY, SQL_IS_INTEGER);  
SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, 1, SQL_IS_INTEGER);  
```  
  
 Whenever these attributes are set to their defaults, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver uses a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] default result set. Default result sets can be used for any SQL statement supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and are the most efficient method of transferring an entire result set to the client.  
  
 [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced support for multiple active result sets (MARS); applications can now have more than one active default result set per connection. MARS is not enabled by default.  
  
 Before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], default result sets did not support multiple active statements on the same connection. After an SQL statement is executed on a connection, the server does not accept commands (except a request to cancel the rest of the result set) from the client on that connection until all the rows in the result set have been processed. To cancel the remainder of a partially processed result set, call [SQLCloseCursor](../../../relational-databases/native-client-odbc-api/sqlclosecursor.md) or [SQLFreeStmt](../../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with the *fOption* parameter set to SQL_CLOSE. To finish a partially processed result set and test for the presence of another result set, call [SQLMoreResults](../../../relational-databases/native-client-odbc-api/sqlmoreresults.md). If an ODBC application attempts a command on a connection handle before a default result set has been completely processed, the call generates SQL_ERROR and a call to **SQLGetDiagRec** returns:  
  
```  
szSqlState: "HY000", pfNativeError: 0  
szErrorMsg: "[Microsoft][SQL Server Native Client]  
                Connection is busy with results for another hstmt."  
```  
  
## See Also  
 [How Cursors Are Implemented](../../../relational-databases/native-client-odbc-cursors/implementation/how-cursors-are-implemented.md)  
  
  
