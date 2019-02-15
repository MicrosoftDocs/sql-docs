---
title: "Core Interface Conformance | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interface conformance levels [ODBC]"
  - "conformance levels [ODBC], interface"
  - "core-level interface conformance levels [ODBC]"
ms.assetid: aaaa864a-6477-45ff-a50a-96d8db66a252
author: MightyPen
ms.author: genemi
manager: craigg
---
# Core Interface Conformance
All ODBC drivers must exhibit at least Core-level interface conformance. Because the features in the Core level are those required by most generic interoperable applications, the driver can work with such applications. The features in the Core level also correspond to the features defined in the ISO CLI specification and to the nonoptional features defined in the Open Group CLI specification. A Core-level interface-conformant ODBC driver allows the application to do all of the following:  
  
-   Allocate and free all types of handles, by calling **SQLAllocHandle** and **SQLFreeHandle**.  
  
-   Use all forms of the **SQLFreeStmt** function.  
  
-   Bind result set columns, by calling **SQLBindCol**.  
  
-   Handle dynamic parameters, including arrays of parameters, in the input direction only, by calling **SQLBindParameter** and **SQLNumParams**. (Parameters in the output direction are feature 203 in [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md).)  
  
-   Specify a bind offset.  
  
-   Use the data-at-execution dialog, involving calls to **SQLParamData** and **SQLPutData**.  
  
-   Manage cursors and cursor names, by calling **SQLCloseCursor**, **SQLGetCursorName**, and **SQLSetCursorName**.  
  
-   Gain access to the description (metadata) of result sets, by calling **SQLColAttribute**, **SQLDescribeCol**, **SQLNumResultCols**, and **SQLRowCount**. (Use of these functions on column number 0 to retrieve bookmark metadata is feature 204 in [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md).)  
  
-   Query the data dictionary, by calling the catalog functions **SQLColumns**, **SQLGetTypeInfo**, **SQLStatistics**, and **SQLTables**.  
  
     The driver is not required to support multipart names of database tables and views. (For more information, see feature 101 in [Level 1 Interface Conformance](../../../odbc/reference/develop-app/level-1-interface-conformance.md) and feature 201 in [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md).) However, certain features of the SQL-92 specification, such as column qualification and names of indexes, are syntactically comparable to multipart naming. The present list of ODBC features is not intended to introduce new options into these aspects of SQL-92.  
  
-   Manage data sources and connections, by calling **SQLConnect**, **SQLDataSources**, **SQLDisconnect**, and **SQLDriverConnect**. Obtain information on drivers, no matter which ODBC level they support, by calling **SQLDrivers**.  
  
-   Prepare and execute SQL statements, by calling **SQLExecDirect**, **SQLExecute**, and **SQLPrepare**.  
  
-   Fetch one row of a result set or multiple rows, in the forward direction only, by calling **SQLFetch** or by calling **SQLFetchScroll** with the *FetchOrientation* argument set to SQL_FETCH_NEXT.  
  
-   Obtain an unbound column in parts, by calling **SQLGetData**.  
  
-   Obtain current values of all attributes, by calling **SQLGetConnectAttr**, **SQLGetEnvAttr**, and **SQLGetStmtAttr**, and set all attributes to their default values and set certain attributes to nondefault values by calling **SQLSetConnectAttr**, **SQLSetEnvAttr**, and **SQLSetStmtAttr**.  
  
-   Manipulate certain fields of descriptors, by calling **SQLCopyDesc**, **SQLGetDescField**, **SQLGetDescRec**, **SQLSetDescField**, and **SQLSetDescRec**.  
  
-   Obtain diagnostic information, by calling **SQLGetDiagField** and **SQLGetDiagRec**.  
  
-   Detect driver capabilities, by calling **SQLGetFunctions** and **SQLGetInfo**. Also, detect the result of any text substitutions made to an SQL statement before it is sent to the data source, by calling **SQLNativeSql**.  
  
-   Use the syntax of **SQLEndTran** to commit a transaction. A Core-level driver need not support true transactions; therefore, the application cannot specify SQL_ROLLBACK nor SQL_AUTOCOMMIT_OFF for the SQL_ATTR_AUTOCOMMIT connection attribute. (For more information, see feature 109 in [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md).)  
  
-   Call **SQLCancel** to cancel the data-at-execution dialog and, in multithread environments, to cancel an ODBC function executing in another thread. Core-level interface conformance does not mandate support for asynchronous execution of functions, nor the use of **SQLCancel** to cancel an ODBC function executing asynchronously. Neither the platform nor the ODBC driver need be multithread for the driver to conduct independent activities at the same time. However, in multithread environments, the ODBC driver must be thread-safe. Serialization of requests from the application is a conformant way to implement this specification, even though it might create serious performance problems.  
  
-   Obtain the SQL_BEST_ROWID row-identifying column of tables, by calling **SQLSpecialColumns**. (Support for SQL_ROWVER is feature 208 in [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md).)  
  
    > [!IMPORTANT]  
    >  ODBC Drivers must implement the functions in the Core interface conformance level.
