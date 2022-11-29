---
description: "Constructing SQL Statements for Cursors"
title: "Constructing SQL Statements for Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], statement construction"
  - "ODBC applications, cursors"
  - "SQL Server Native Client ODBC driver, statements"
  - "statements [ODBC], constructing"
  - "ODBC applications, statements"
  - "statements [ODBC], cursors"
ms.assetid: 134003fd-9c93-4f5c-a988-045990933b80
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Constructing SQL Statements for Cursors
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver uses server cursors to implement the cursor functionality defined in the ODBC specification. An ODBC application controls the cursor behavior by using [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set different statement attributes. These are the attributes and their defaults.  
  
|Attribute|Default|  
|---------------|-------------|  
|SQL_ATTR_CONCURRENCY|SQL_CONCUR_READ_ONLY|  
|SQL_ATTR_CURSOR_TYPE|SQL_CURSOR_FORWARD_ONLY|  
|SQL_ATTR_CURSOR_SCROLLABLE|SQL_NONSCROLLABLE|  
|SQL_ATTR_CURSOR_SENSITIVITY|SQL_UNSPECIFIED|  
|SQL_ATTR_ROW_ARRAY_SIZE|1|  
  
 When these options are set to their defaults at the time an SQL statement is executed, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not use a server cursor to implement the result set; instead, it uses a default result set. If any of these options are changed from their defaults at the time an SQL statement is executed, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver attempts to use a server cursor to implement the result set.  
  
 Default result sets support all of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. There are no restrictions on the types of SQL statements that can be executed when using a default result set.  
  
 Server cursors do not support all [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Server cursors do not support any SQL statement that generates multiple result sets.  
  
 The following types of statements are not supported by server cursors:  
  
-   Batches  
  
     SQL statements built from two or more individual SQL SELECT statements, for example:  
  
    ```  
    SELECT * FROM Authors; SELECT * FROM Titles  
    ```  
  
-   Stored procedures with multiple SELECT statements  
  
     SQL statements that execute a stored procedure containing more than one SELECT statement. This includes SELECT statements that fill parameters or variables.  
  
-   Keywords  
  
     SQL statements containing the keywords FOR BROWSE, or INTO.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if an SQL statement that matches any of these conditions is executed with a server cursor, the server cursor is implicitly converted to a default result set. After **SQLExecDirect** or **SQLExecute** returns SQL_SUCCESS_WITH_INFO, the cursor attributes will be set back to their default settings.  
  
 SQL statements that do not fit the categories above can be executed with any statement attribute settings; they work equally well with either a default result set or a server cursor.  
  
## Errors  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 and later, an attempt to execute a statement that produces multiple result sets generates SQL_SUCCESS_WITH INFO and the following message:  
  
```  
SqlState: 01S02"  
pfNative: 0  
szErrorMsgString: "[Microsoft][SQL Server Native Client][SQL Server]  
               Cursor type changed."  
```  
  
 ODBC applications receiving this message can call [SQLGetStmtAttr](../../relational-databases/native-client-odbc-api/sqlgetstmtattr.md) to determine the current cursor settings.  
  
 An attempt to execute a procedure with multiple SELECT statements when using server cursors generates the following error:  
  
```  
SqlState: 42000  
pfNative: 16937  
szErrorMsgString: [Microsoft][SQL Server Native Client][SQL Server]  
               A server cursor is not allowed on a stored procedure  
               with more than one SELECT statement in it. Use a  
               default result set or client cursor.  
```  
  
 An attempt to execute a batch with multiple SELECT statements when using server cursors generates the following error:  
  
```  
SqlState: 42000  
pfNative: 16938  
szErrorMsgString: [Microsoft][SQL Server Native Client][SQL Server]  
               sp_cursoropen. The statement parameter can only  
               be a single SELECT statement or a single stored   
               procedure.  
```  
  
 ODBC applications receiving these errors must reset all the cursor statement attributes to their defaults before attempting to execute the statement.  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)  
  
  
