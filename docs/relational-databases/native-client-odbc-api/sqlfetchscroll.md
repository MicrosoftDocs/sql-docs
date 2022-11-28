---
description: "SQLFetchScroll"
title: "SQLFetchScroll | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLFetchScroll function"
ms.assetid: 524a3985-a08d-4445-99e0-bb551a666615
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLFetchScroll
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLFetchScroll** returns one row set of data to the application. The size of the row set is set using [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md). The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports all defined fetch instructions (for example, SQL_FETCH_RELATIVE) with the following limitations:  
  
-   If a forward-only cursor is defined for the statement, SQL_FETCH_NEXT is required and attempts to fetch in any other fashion will result in an error return.  
  
-   SQL_FETCH_BOOKMARK is supported for static and keyset-driven cursors only.  
  
## SQLFetchScroll Support for Enhanced Date and Time Features  
 Result column values of date/time types are converted as described in [Conversions from SQL to C](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-sql-to-c.md).  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLFetchScroll Support for Large CLR UDTs  
 **SQLFetchScroll** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLFetchScroll Function](../../odbc/reference/syntax/sqlfetchscroll-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
