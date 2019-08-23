---
title: "SQLFetchScroll | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLFetchScroll function"
ms.assetid: 524a3985-a08d-4445-99e0-bb551a666615
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLFetchScroll
  **SQLFetchScroll** returns one row set of data to the application. The size of the row set is set using [SQLSetStmtAttr](sqlsetstmtattr.md). The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports all defined fetch instructions (for example, SQL_FETCH_RELATIVE) with the following limitations:  
  
-   If a forward-only cursor is defined for the statement, SQL_FETCH_NEXT is required and attempts to fetch in any other fashion will result in an error return.  
  
-   SQL_FETCH_BOOKMARK is supported for static and keyset-driven cursors only.  
  
## SQLFetchScroll Support for Enhanced Date and Time Features  
 Result column values of date/time types are converted as described in [Conversions from SQL to C](../native-client-odbc-date-time/datetime-data-type-conversions-from-sql-to-c.md).  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLFetchScroll Support for Large CLR UDTs  
 **SQLFetchScroll** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLFetchScroll Function](https://go.microsoft.com/fwlink/?LinkId=59343)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
