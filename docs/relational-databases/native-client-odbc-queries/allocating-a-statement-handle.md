---
description: "Allocating a Statement Handle"
title: "Allocating a Statement Handle | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLSetStmtAttr function"
  - "statements [ODBC], statement handles"
  - "ODBC applications, executing queries"
  - "SQLGetStmtAttr function"
  - "SQL Server Native Client ODBC driver, statements"
  - "allocating statement handles"
  - "ODBC applications, statements"
  - "statement handles [ODBC]"
  - "SQLAllocHandle function"
ms.assetid: 9ee207f3-2667-45f5-87ca-e6efa1fd7a5c
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Allocating a Statement Handle
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Before an application can execute a statement, it must allocate a statement handle. It does this by calling **SQLAllocHandle** with the *HandleType* parameter set to SQL_HANDLE_STMT and *InputHandle* pointing to a connection handle.  
  
 Statement attributes are characteristics of the statement handle. Sample statement attributes can include using bookmarks and the kind of cursor to use with the statement's result set. Statement attributes are set with [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md), and their current settings are retrieved by using [SQLGetStmtAttr](../../relational-databases/native-client-odbc-api/sqlgetstmtattr.md). There is no requirement that an application set any statement attributes; all statement attributes have defaults, and some are driver specific.  
  
 Use caution in the use of several ODBC statement and connection options. Calling [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) with *fOption* set to SQL_ATTR_LOGIN_TIMEOUT controls the time that an application waits for a connection attempt to time-out while waiting to establish a connection (0 specifies an infinite wait). Sites that have slow response times can set this value high to make sure that connections have sufficient time to be completed. However, the interval should always be low enough to give the user a response in a reasonable time if the driver cannot connect.  
  
 Calling **SQLSetStmtAttr** with *fOption* set to SQL_ATTR_QUERY_TIMEOUT sets a query time-out interval to help protect the server and the user from long-running queries.  
  
 Calling **SQLSetStmtAttr** with *fOption* set to SQL_ATTR_MAX_LENGTH limits the amount of **text** and **image** data that an individual statement can retrieve. Calling **SQLSetStmtAttr** with *fOption* set to SQL_ATTR_MAX_ROWS also limits a rowset to the first *n* rows if that is all the application requires. Note that setting SQL_ATTR_MAX_ROWS causes the driver to issue a SET ROWCOUNT statement to the server. This affects all [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statements, including triggers and updates.  
  
 Use caution when you are setting these options. It is best if all statement handles on a connection handle have the same settings for SQL_ATTR_MAX_LENGTH and SQL_ATTR_MAX_ROWS. If the driver switches from a statement handle to another with different values for these options, the driver must generate the appropriate SET TEXTSIZE and SET ROWCOUNT statements to change the settings. The driver cannot put these statements in the same batch as the user SQL statement because the user SQL statement can contain a statement that must be the first statement in a batch. The driver must send the SET TEXTSIZE and SET ROWCOUNT statements in a separate batch, which automatically generates an additional roundtrip to the server.  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)  
  
  
