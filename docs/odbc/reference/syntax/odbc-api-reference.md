---
title: "ODBC API reference"
description: "Comprehensive reference for ODBC API functions organized by category, including connection, statement execution, data retrieval, and catalog functions."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 02/05/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apitype: "dllExport"
ai-usage: ai-assisted
---
# ODBC API reference

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The ODBC API provides a standard set of functions for connecting to data sources, executing SQL statements, and retrieving results. Each function is a C programming language function with descriptions that include purpose, ODBC version, syntax, arguments, return values, diagnostics, and code examples.

The standard CLI conformance level can be one of the following: ISO 92, Open Group, ODBC, or Deprecated. A function tagged as ISO 92-conformant also appears in Open Group version 1, because Open Group is a pure superset of ISO 92. A function tagged as Open Group-compliant also appears in ODBC 3.x, because ODBC 3.x is a pure superset of Open Group version 1. A function tagged as ODBC-compliant doesn't appear in either standard. A function tagged as deprecated was deprecated in ODBC 3.x.

The [SQLGetDiagField](sqlgetdiagfield-function.md) function description describes how to handle diagnostic information. The text associated with SQLSTATE values provides a description of the condition but doesn't prescribe specific text.

> [!NOTE]
> For driver-specific information about ODBC functions, see the section for the driver.

## Handle allocation and management

These functions allocate and free handles for environments, connections, statements, and descriptors. Handles are the primary mechanism for tracking state in ODBC applications.

| Function | Description |
| --- | --- |
| [SQLAllocHandle](sqlallochandle-function.md) | Allocates an environment, connection, statement, or descriptor handle. This is the ODBC 3.x function that replaces the deprecated allocation functions. |
| [SQLFreeHandle](sqlfreehandle-function.md) | Frees an environment, connection, statement, or descriptor handle and releases associated resources. |
| [SQLAllocConnect](sqlallocconnect-function.md) | Allocates a connection handle. Deprecated in ODBC 3.x; use `SQLAllocHandle` instead. |
| [SQLAllocEnv](sqlallocenv-function.md) | Allocates an environment handle. Deprecated in ODBC 3.x; use `SQLAllocHandle` instead. |
| [SQLAllocStmt](sqlallocstmt-function.md) | Allocates a statement handle. Deprecated in ODBC 3.x; use `SQLAllocHandle` instead. |
| [SQLFreeConnect](sqlfreeconnect-function.md) | Frees a connection handle. Deprecated in ODBC 3.x; use `SQLFreeHandle` instead. |
| [SQLFreeEnv](sqlfreeenv-function.md) | Frees an environment handle. Deprecated in ODBC 3.x; use `SQLFreeHandle` instead. |
| [SQLFreeStmt](sqlfreestmt-function.md) | Stops statement processing, closes associated cursors, discards pending results, and optionally frees resources associated with a statement handle. |

## Connection functions

These functions establish and manage connections to data sources. They support various connection methods including standard connections, driver-specific dialogs, and iterative browsing.

| Function | Description |
| --- | --- |
| [SQLConnect](sqlconnect-function.md) | Establishes a connection to a data source using a data source name, user ID, and password. |
| [SQLDriverConnect](sqldriverconnect-function.md) | Establishes a connection using a connection string. Supports driver-specific dialogs for additional connection information. |
| [SQLBrowseConnect](sqlbrowseconnect-function.md) | Supports an iterative method of discovering and enumerating the attributes needed to connect to a data source. |
| [SQLDisconnect](sqldisconnect-function.md) | Closes a connection to a data source and releases associated resources. |
| [SQLDataSources](sqldatasources-function.md) | Returns a list of available data sources. Called on the Driver Manager, not a specific driver. |
| [SQLDrivers](sqldrivers-function.md) | Returns a list of installed drivers and their attributes. Called on the Driver Manager. |

## Statement preparation and execution

These functions prepare and execute SQL statements. ODBC supports both direct execution and prepared execution, with prepared execution offering better performance for repeatedly executed statements.

| Function | Description |
| --- | --- |
| [SQLPrepare](sqlprepare-function.md) | Prepares a SQL statement for later execution. The data source compiles and optimizes the statement. |
| [SQLExecute](sqlexecute-function.md) | Executes a prepared statement. Call `SQLPrepare` before calling this function. |
| [SQLExecDirect](sqlexecdirect-function.md) | Prepares and executes a SQL statement in a single call. Use for statements executed only once. |
| [SQLNativeSql](sqlnativesql-function.md) | Returns the SQL string as modified by the driver, showing how the driver translates ODBC SQL syntax. |
| [SQLCancel](sqlcancel-function.md) | Cancels processing on a statement. Can cancel an asynchronously executing function or a function running on another thread. |
| [SQLCancelHandle](sqlcancelhandle-function.md) | Cancels processing on a connection or statement. More flexible than `SQLCancel` for canceling connection functions. |
| [SQLCompleteAsync](sqlcompleteasync-function.md) | Determines when an asynchronous function completes. Used with notification-based asynchronous processing. |

## Parameter binding

These functions bind application variables to parameter markers in SQL statements. Parameters enable dynamic values in prepared statements.

| Function | Description |
| --- | --- |
| [SQLBindParameter](sqlbindparameter-function.md) | Binds an application variable to a parameter marker in a SQL statement. Supports input, output, and input/output parameters. |
| [SQLNumParams](sqlnumparams-function.md) | Returns the number of parameters in a SQL statement. |
| [SQLDescribeParam](sqldescribeparam-function.md) | Returns the description of a parameter marker, including data type, size, and precision. |
| [SQLParamData](sqlparamdata-function.md) | Used with `SQLPutData` to supply parameter data at execution time. Returns the parameter needing data. |
| [SQLPutData](sqlputdata-function.md) | Sends part or all of a data value for a parameter at execution time. Supports large data in chunks. |
| [SQLSetParam](sqlsetparam-function.md) | Binds a parameter. Deprecated in ODBC 3.x; use `SQLBindParameter` instead. |
| [SQLParamOptions](sqlparamoptions-function.md) | Sets options for parameter arrays. Deprecated in ODBC 3.x; use statement attributes instead. |

## Result set binding and retrieval

These functions bind application buffers to result set columns and retrieve data from query results.

| Function | Description |
| --- | --- |
| [SQLBindCol](sqlbindcol-function.md) | Binds an application variable to a result set column for subsequent fetch operations. |
| [SQLFetch](sqlfetch-function.md) | Fetches the next rowset of data from the result set into bound columns. |
| [SQLFetchScroll](sqlfetchscroll-function.md) | Fetches the specified rowset from a result set. Supports scrolling to first, last, next, prior, absolute, and relative positions. |
| [SQLGetData](sqlgetdata-function.md) | Retrieves data for a single column after `SQLFetch` or `SQLFetchScroll`. Useful for large data or unbound columns. |
| [SQLExtendedFetch](sqlextendedfetch-function.md) | Fetches the specified rowset of data. Deprecated in ODBC 3.x; use `SQLFetchScroll` instead. |
| [SQLMoreResults](sqlmoreresults-function.md) | Determines whether more results are available on a statement and advances to the next result set. |
| [SQLRowCount](sqlrowcount-function.md) | Returns the number of rows affected by an UPDATE, INSERT, or DELETE statement. |

## Cursor operations

These functions manage cursor behavior, positioning, and bulk operations on rowsets.

| Function | Description |
| --- | --- |
| [SQLSetPos](sqlsetpos-function.md) | Sets the cursor position within a rowset and allows applications to refresh, update, or delete data at that position. |
| [SQLBulkOperations](sqlbulkoperations-function.md) | Performs bulk insert, update, delete, or fetch-by-bookmark operations on rowsets. |
| [SQLCloseCursor](sqlclosecursor-function.md) | Closes a cursor that has been opened on a statement and discards pending results. |
| [SQLGetCursorName](sqlgetcursorname-function.md) | Returns the cursor name associated with a statement. |
| [SQLSetCursorName](sqlsetcursorname-function.md) | Specifies a cursor name for positioned UPDATE and DELETE statements. |
| [SQLSetScrollOptions](sqlsetscrolloptions-function.md) | Sets options for cursor behavior. Deprecated in ODBC 3.x; use statement attributes instead. |

## Catalog functions

These functions retrieve metadata about the database structure, including tables, columns, indexes, privileges, and stored procedures.

| Function | Description |
| --- | --- |
| [SQLTables](sqltables-function.md) | Returns a list of table names in the data source. Supports filtering by catalog, schema, and table type. |
| [SQLColumns](sqlcolumns-function.md) | Returns a list of column names and their attributes for specified tables. |
| [SQLPrimaryKeys](sqlprimarykeys-function.md) | Returns the columns that make up the primary key for a table. |
| [SQLForeignKeys](sqlforeignkeys-function.md) | Returns foreign keys in a table or foreign keys in other tables that reference a table's primary key. |
| [SQLStatistics](sqlstatistics-function.md) | Returns statistics about a table and a list of indexes associated with it. |
| [SQLSpecialColumns](sqlspecialcolumns-function.md) | Returns columns that uniquely identify a row or columns that are automatically updated when any value in the row is updated. |
| [SQLColumnPrivileges](sqlcolumnprivileges-function.md) | Returns a list of columns and associated privileges for a table. |
| [SQLTablePrivileges](sqltableprivileges-function.md) | Returns a list of tables and the privileges associated with each table. |
| [SQLProcedures](sqlprocedures-function.md) | Returns a list of stored procedure names in the data source. |
| [SQLProcedureColumns](sqlprocedurecolumns-function.md) | Returns the list of input/output parameters and columns in the result set for specified procedures. |
| [SQLGetTypeInfo](sqlgettypeinfo-function.md) | Returns information about data types supported by the data source. |

## Descriptor operations

These functions get and set descriptor values. Descriptors contain metadata about parameters and result set columns.

| Function | Description |
| --- | --- |
| [SQLGetDescField](sqlgetdescfield-function.md) | Returns the value of a single field of a descriptor record. |
| [SQLGetDescRec](sqlgetdescrec-function.md) | Returns multiple fields of a descriptor record in a single call. |
| [SQLSetDescField](sqlsetdescfield-function.md) | Sets the value of a single field of a descriptor record. |
| [SQLSetDescRec](sqlsetdescrec-function.md) | Sets multiple fields of a descriptor record in a single call. |
| [SQLCopyDesc](sqlcopydesc-function.md) | Copies descriptor information from one descriptor handle to another. |

## Attribute functions

These functions get and set attributes for environments, connections, and statements. Attributes control various aspects of ODBC behavior.

| Function | Description |
| --- | --- |
| [SQLSetEnvAttr](sqlsetenvattr-function.md) | Sets an environment attribute that affects all connections under that environment. |
| [SQLGetEnvAttr](sqlgetenvattr-function.md) | Returns the value of an environment attribute. |
| [SQLSetConnectAttr](sqlsetconnectattr-function.md) | Sets a connection attribute that affects the connection and statements on it. |
| [SQLGetConnectAttr](sqlgetconnectattr-function.md) | Returns the value of a connection attribute. |
| [SQLSetStmtAttr](sqlsetstmtattr-function.md) | Sets a statement attribute. Includes cursor, query timeout, and parameter settings. |
| [SQLGetStmtAttr](sqlgetstmtattr-function.md) | Returns the value of a statement attribute. |
| [SQLSetConnectOption](sqlsetconnectoption-function.md) | Sets a connection option. Deprecated in ODBC 3.x; use `SQLSetConnectAttr` instead. |
| [SQLGetConnectOption](sqlgetconnectoption-function.md) | Returns the value of a connection option. Deprecated in ODBC 3.x; use `SQLGetConnectAttr` instead. |
| [SQLSetStmtOption](sqlsetstmtoption-function.md) | Sets a statement option. Deprecated in ODBC 3.x; use `SQLSetStmtAttr` instead. |
| [SQLGetStmtOption](sqlgetstmtoption-function.md) | Returns the value of a statement option. Deprecated in ODBC 3.x; use `SQLGetStmtAttr` instead. |

## Diagnostic and information functions

These functions retrieve diagnostic information, error messages, driver capabilities, and data source information.

| Function | Description |
| --- | --- |
| [SQLGetDiagField](sqlgetdiagfield-function.md) | Returns the value of a field in a diagnostic record containing error, warning, and status information. |
| [SQLGetDiagRec](sqlgetdiagrec-function.md) | Returns several commonly used fields of a diagnostic record, including SQLSTATE, native error code, and message text. |
| [SQLError](sqlerror-function.md) | Returns error information. Deprecated in ODBC 3.x; use `SQLGetDiagRec` instead. |
| [SQLGetFunctions](sqlgetfunctions-function.md) | Returns information about whether a driver supports a specific ODBC function. |
| [SQLGetInfo](sqlgetinfo-function.md) | Returns general information about the driver and data source, including supported features and capabilities. |

## Result set metadata

These functions return information about the structure of result sets.

| Function | Description |
| --- | --- |
| [SQLNumResultCols](sqlnumresultcols-function.md) | Returns the number of columns in a result set. |
| [SQLDescribeCol](sqldescribecol-function.md) | Returns the column name, data type, precision, scale, and nullability for a result set column. |
| [SQLColAttribute](sqlcolattribute-function.md) | Returns descriptor information for a column in a result set. More flexible than `SQLDescribeCol`. |
| [SQLColAttributes](sqlcolattributes-function.md) | Returns attributes for a column. Deprecated in ODBC 3.x; use `SQLColAttribute` instead. |

## Transaction management

These functions manage transaction boundaries, controlling when changes are committed or rolled back.

| Function | Description |
| --- | --- |
| [SQLEndTran](sqlendtran-function.md) | Commits or rolls back a transaction. Can apply to all connections on an environment or a single connection. |
| [SQLTransact](sqltransact-function.md) | Commits or rolls back a transaction. Deprecated in ODBC 3.x; use `SQLEndTran` instead. |

## Related content

- [ODBC Overview](../odbc-overview.md)
- [Developing Applications](../develop-app/developing-applications.md)
- [ODBC Programmer's Reference](../odbc-programmer-s-reference.md)
