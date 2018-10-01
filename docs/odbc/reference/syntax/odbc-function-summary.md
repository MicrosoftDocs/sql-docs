---
title: "ODBC Function Summary | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], listed by task"
ms.assetid: 7aa635da-e6b7-439f-8e9b-c3860e24de5e
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Function Summary
The following table lists ODBC functions, grouped by type of task, and includes the conformance designation and a brief description of the purpose of each function. For more information about conformance designations, see [ODBC and the Standard CLI](../../../odbc/reference/odbc-and-the-standard-cli.md). For more information about the syntax and semantics for each function, see [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md).  
  
 An application can call the **SQLGetInfo** function to obtain conformance information about a driver. To obtain information about support for a specific function in a driver, an application can call **SQLGetFunctions**.  
  
|Task|Function name|Conformance|Purpose|  
|----------|-------------------|-----------------|-------------|  
|Connecting to a data source|[SQLAllocHandle](../../../odbc/reference/syntax/sqlallochandle-function.md)|ISO 92|Obtains an environment, connection, statement, or descriptor handle.|  
||[SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md)|ISO 92|Connects to a specific driver by data source name, user ID, and password.|  
||[SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)|ODBC|Connects to a specific driver by connection string or requests that the Driver Manager and driver display connection dialog boxes for the user.|  
||[SQLBrowseConnect](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|ODBC|Returns successive levels of connection attributes and valid attribute values. When a value has been specified for each connection attribute, connects to the data source.|  
|Obtaining information about a driver and data source|[SQLDataSources](../../../odbc/reference/syntax/sqldatasources-function.md)<br /><br /> [SQLDrivers](../../../odbc/reference/syntax/sqldrivers-function.md)|ISO 92<br /><br /> ODBC|Returns the list of available data sources.<br /><br /> Returns the list of installed drivers and their attributes.|  
||[SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md)|ISO 92|Returns information about a specific driver and data source.|  
||[SQLGetFunctions](../../../odbc/reference/syntax/sqlgetfunctions-function.md)|ISO 92|Returns supported driver functions.|  
||[SQLGetTypeInfo](../../../odbc/reference/syntax/sqlgettypeinfo-function.md)|ISO 92|Returns information about supported data types.|  
|Setting and retrieving driver attributes|[SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)<br /><br /> [SQLGetConnectAttr](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|ISO 92<br /><br /> ISO 92|Sets a connection attribute.<br /><br /> Returns the value of a connection attribute.|  
||[SQLSetEnvAttr](../../../odbc/reference/syntax/sqlsetenvattr-function.md)|ISO 92|Sets an environment attribute.|  
||[SQLGetEnvAttr](../../../odbc/reference/syntax/sqlgetenvattr-function.md)|ISO 92|Returns the value of an environment attribute.|  
||[SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|ISO 92|Sets a statement attribute.|  
||[SQLGetStmtAttr](../../../odbc/reference/syntax/sqlgetstmtattr-function.md)|ISO 92|Returns the value of a statement attribute.|  
|Setting and retrieving descriptor fields|[SQLGetDescField](../../../odbc/reference/syntax/sqlgetdescfield-function.md)<br /><br /> [SQLGetDescRec](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|ISO 92<br /><br /> ISO 92|Returns the value of a single descriptor field.<br /><br /> Returns the values of multiple descriptor fields.|  
||[SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|ISO 92|Sets a single descriptor field.|  
||[SQLSetDescRec](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|ISO 92|Sets multiple descriptor fields.|  
||[SQLCopyDesc](../../../odbc/reference/syntax/sqlcopydesc-function.md)|ISO 92|Copies descriptor information from one descriptor handle to another.|  
|Preparing SQL requests|[SQLPrepare](../../../odbc/reference/syntax/sqlprepare-function.md)|ISO 92|Prepares an SQL statement for later execution.|  
||[SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md)|ODBC|Assigns storage for a parameter in an SQL statement.|  
||[SQLGetCursorName](../../../odbc/reference/syntax/sqlgetcursorname-function.md)|ISO 92|Returns the cursor name associated with a statement handle.|  
||[SQLSetCursorName](../../../odbc/reference/syntax/sqlsetcursorname-function.md)|ISO 92|Specifies a cursor name.|  
||[SQLSetScrollOptions](../../../odbc/reference/syntax/sqlsetscrolloptions-function.md)|ODBC|Sets options that control cursor behavior.|  
|Submitting requests|[SQLExecute](../../../odbc/reference/syntax/sqlexecute-function.md)<br /><br /> [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md)|ISO 92<br /><br /> ISO 92|Executes a prepared statement.<br /><br /> Executes a statement.|  
||[SQLNativeSql](../../../odbc/reference/syntax/sqlnativesql-function.md)|ODBC|Returns the text of an SQL statement as translated by the driver.|  
||[SQLDescribeParam](../../../odbc/reference/syntax/sqldescribeparam-function.md)|ODBC|Returns the description for a specific parameter in a statement.|  
||[SQLNumParams](../../../odbc/reference/syntax/sqlnumparams-function.md)|ISO 92|Returns the number of parameters in a statement.|  
||[SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md)|ISO 92|Used in conjunction with **SQLPutData** to supply parameter data at execution time. (Useful for long data values.)|  
||[SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md)|ISO 92|Sends part or all of a data value for a parameter. (Useful for long data values.)|  
|Retrieving results and information about results|[SQLRowCount](../../../odbc/reference/syntax/sqlrowcount-function.md)<br /><br /> [SQLNumResultCols](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|ISO 92<br /><br /> ISO 92|Returns the number of rows affected by an insert, update, or delete request.<br /><br /> Returns the number of columns in the result set.|  
||[SQLDescribeCol](../../../odbc/reference/syntax/sqldescribecol-function.md)|ISO 92|Describes a column in the result set.|  
||[SQLColAttribute](../../../odbc/reference/syntax/sqlcolattribute-function.md)|ISO 92|Describes attributes of a column in the result set.|  
||[SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md)|ISO 92|Assigns storage for a result column and specifies the data type.|  
||[SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md)|ISO 92|Returns multiple result rows.|  
||[SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|ISO 92|Returns scrollable result rows.|  
||[SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md)|ISO 92|Returns part or all of one column of one row of a result set. (Useful for long data values.)|  
||[SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md)|ODBC|Positions a cursor within a fetched block of data and allows an application to refresh data in the rowset or to update or delete data in the result set.|  
||[SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|ODBC|Performs bulk insertions and bulk bookmark operations, including update, delete, and fetch by bookmark.|  
||[SQLMoreResults](../../../odbc/reference/syntax/sqlmoreresults-function.md)|ODBC|Determines whether there are more result sets available and, if so, initializes processing for the next result set.|  
||[SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md)|ISO 92|Returns additional diagnostic information (a single field of the diagnostic data structure).|  
||[SQLGetDiagRec](../../../odbc/reference/syntax/sqlgetdiagrec-function.md)|ISO 92|Returns additional diagnostic information (multiple fields of the diagnostic data structure).|  
|Obtaining information about the data source's system tables (catalog functions)|[SQLColumnPrivileges](../../../odbc/reference/syntax/sqlcolumnprivileges-function.md)<br /><br /> [SQLColumns](../../../odbc/reference/syntax/sqlcolumns-function.md)|ODBC<br /><br /> Open Group|Returns a list of columns and associated privileges for one or more tables.<br /><br /> Returns the list of column names in specified tables.|  
||[SQLForeignKeys](../../../odbc/reference/syntax/sqlforeignkeys-function.md)|ODBC|Returns a list of column names that make up foreign keys, if they exist for a specified table.|  
||[SQLPrimaryKeys](../../../odbc/reference/syntax/sqlprimarykeys-function.md)|ODBC|Returns the list of column names that make up the primary key for a table.|  
||[SQLProcedureColumns](../../../odbc/reference/syntax/sqlprocedurecolumns-function.md)|ODBC|Returns the list of input and output parameters, as well as the columns that make up the result set for the specified procedures.|  
||[SQLProcedures](../../../odbc/reference/syntax/sqlprocedures-function.md)|ODBC|Returns the list of procedure names stored in a specific data source.|  
||[SQLSpecialColumns](../../../odbc/reference/syntax/sqlspecialcolumns-function.md)|Open Group|Returns information about the optimal set of columns that uniquely identifies a row in a specified table, or the columns that are automatically updated when any value in the row is updated by a transaction.|  
||[SQLStatistics](../../../odbc/reference/syntax/sqlstatistics-function.md)|ISO 92|Returns statistics about a single table and the list of indexes associated with the table.|  
||[SQLTablePrivileges](../../../odbc/reference/syntax/sqltableprivileges-function.md)|ODBC|Returns a list of tables and the privileges associated with each table.|  
||[SQLTables](../../../odbc/reference/syntax/sqltables-function.md)|Open Group|Returns the list of table names stored in a specific data source.|  
|Terminating a statement|[SQLFreeStmt](../../../odbc/reference/syntax/sqlfreestmt-function.md)|ISO 92|Ends statement processing, discards pending results, and, optionally, frees all resources associated with the statement handle.|  
||[SQLCloseCursor](../../../odbc/reference/syntax/sqlclosecursor-function.md)|ISO 92|Closes a cursor that has been opened on a statement handle.|  
||[SQLCancel](../../../odbc/reference/syntax/sqlcancel-function.md)|ISO 92|Cancels the processing on a statement.|  
||[SQLCancelHandle](../../../odbc/reference/syntax/sqlcancelhandle-function.md)|ODBC|Cancels the processing on a statement or connection.|  
||[SQLEndTran](../../../odbc/reference/syntax/sqlendtran-function.md)|ISO 92|Commits or rolls back a transaction.|  
|Terminating a connection|[SQLDisconnect](../../../odbc/reference/syntax/sqldisconnect-function.md)<br /><br /> [SQLFreeHandle](../../../odbc/reference/syntax/sqlfreehandle-function.md)|ISO 92<br /><br /> ISO 92|Closes the connection.<br /><br /> Releases an environment, connection, statement, or descriptor handle.|
