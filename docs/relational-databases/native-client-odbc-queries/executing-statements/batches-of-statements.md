---
description: "Batches of Statements"
title: "Batches of Statements | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "statements [ODBC], batches"
  - "batches [ODBC]"
  - "ODBC applications, statements"
  - "multiple statements, batches"
  - "SQLMoreResults function"
  - "SQLExecDirect function"
ms.assetid: 057d7c1c-1428-4780-9447-a002ea741188
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Batches of Statements
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  A batch of [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements contains two or more statements, separated by a semicolon (;), built into a single string passed to **SQLExecDirect** or [SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md). For example:  
  
```  
SQLExecDirect(hstmt,   
    "SELECT * FROM Authors; SELECT * FROM Titles",  
    SQL_NTS);  
```  
  
 Batches can be more efficient than submitting statements separately because network traffic is often reduced. Use [SQLMoreResults](../../../relational-databases/native-client-odbc-api/sqlmoreresults.md) to get positioned on the next result set when finished with the current result set.  
  
 Batches can always be used when the ODBC cursor attributes are set to the defaults of a forward-only, read-only cursor with a rowset size of 1.  
  
 If a batch is executed when using server cursors against [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the server cursor is implicitly converted to a default result set. **SQLExecDirect** or **SQLExecute** return SQL_SUCCESS_WITH_INFO, and a call to **SQLGetDiagRec** returns:  
  
```  
szSqlState = "01S02", pfNativeError = 0  
szErrorMsg = "[Microsoft][SQL Server Native Server Native Client]Cursor type changed."  
```  
  
## See Also  
 [Executing Statements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-statements/executing-statements-odbc.md)  
  
