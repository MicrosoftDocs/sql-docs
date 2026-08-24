---
title: "ODBC API Implementation Details"
description: "ODBC API Implementation Details"
author: markingmyname
ms.author: maghan
ms.date: 01/26/2026
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ODBC, functions"
  - "SQL Server Native Client ODBC driver, SQL Server-specific behaviors"
  - "ODBC, SQL Server-specific behaviors"
  - "functions [ODBC]"
---

# ODBC API implementation details

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Open Database Connectivity (ODBC) is a Microsoft Win32 API that enables applications to access data in ODBC-compliant data sources. This article explains how ODBC processes function calls, manages handles, interacts with drivers, and provides diagnostics. It gives developers a clearer understanding of how ODBC operates between applications, the Driver Manager, and database drivers.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reference doesn't document every ODBC function. It only documents those functions with parameters or behaviors unique to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with the ODBC 3.51 specification. For full reference material, download the Microsoft Data Access Components SDK from the [Data Access and Storage Developer Center](https://go.microsoft.com/fwlink?linkid=4173) or view the [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md).

## How the ODBC API works

ODBC provides a standardized interface between applications and database drivers. When your application calls an ODBC function, the call passes through several layers before reaching the data source. Understanding this architecture helps you write more efficient code and troubleshoot connectivity problems.

### ODBC handle model

ODBC uses four hierarchical handle types to manage state:

| Handle type | Purpose |
| --- | --- |
| **Environment (HENV)** | Global ODBC settings and versioning |
| **Connection (HDBC)** | Represents a connection to a specific data source |
| **Statement (HSTMT)** | Manages SQL statements, parameters, and result sets |
| **Descriptor (HDESC)** | Stores metadata for parameters and columns |

### Driver manager and driver interaction

1. The application makes an ODBC API call.
1. The **Driver Manager** validates parameters and dispatches the call.
1. The **driver** interacts with the data source.
1. Results flow back through the Driver Manager to the application.

### Function call lifecycle

1. Allocate handles.
1. Set environment or connection attributes.
1. Connect to a data source.
1. Prepare or execute SQL statements.
1. Bind parameters or result columns.
1. Fetch rows.
1. Free handles.

### Diagnostics and error handling

Use the following diagnostic functions:

- `SQLGetDiagRec`
- `SQLGetDiagField`

Diagnostics might apply to environment, connection, or statement handles.

### Unicode vs. ANSI calls

ODBC provides two types of functions:

- **ANSI functions** like `SQLExecDirectA`
- **Unicode functions** like `SQLExecDirectW`

Use Unicode APIs for modern applications.

### Threading and pooling

- Thread safety depends on how the application configures the driver and Driver Manager.
- To reduce connection overhead, enable connection pooling at either level.

## ODBC API reference

The following sections group the ODBC API functions by task. Each entry links to the detailed reference page for the SQL Server Native Client driver.

### Connection and data source

Use these functions to establish, configure, and manage connections to SQL Server.

| Function | Description |
| --- | --- |
| [SQLConnect](sqlconnect.md) | Establish a connection to a data source using a DSN, user ID, and password |
| [SQLDriverConnect](sqldriverconnect.md) | Connect using a connection string with driver-specific keywords |
| [SQLBrowseConnect](sqlbrowseconnect.md) | Discover connection attributes interactively to build a connection string |
| [SQLConfigDataSource](sqlconfigdatasource.md) | Create, modify, or delete data source names (DSNs) programmatically |
| [SQLDrivers](sqldrivers.md) | List all installed ODBC drivers and their attributes |
| [SQLGetConnectAttr](sqlgetconnectattr.md) | Retrieve the current value of a connection attribute |
| [SQLSetConnectAttr](sqlsetconnectattr.md) | Configure connection behavior such as timeouts and transaction isolation |

### Execute SQL statements

Use these functions to prepare, execute, and manage SQL statements.

| Function | Description |
| --- | --- |
| [SQLExecDirect](sqlexecdirect.md) | Execute a SQL statement immediately without preparation |
| [SQLExecute](sqlexecute.md) | Execute a previously prepared SQL statement |
| [SQLCancel](sqlcancel.md) | Cancel an in-progress statement execution |
| [SQLNativeSql](sqlnativesql.md) | Translate ODBC SQL syntax to the driver's native SQL dialect |
| [SQLEndTran](sqlendtran.md) | Commit or roll back a transaction on a connection or environment |

### Bind parameters and columns

Use these functions to bind application variables to SQL parameters and result set columns.

| Function | Description |
| --- | --- |
| [SQLBindParameter](sqlbindparameter.md) | Bind an application buffer to a SQL statement parameter marker |
| [SQLBindCol](sqlbindcol.md) | Bind an application buffer to a result set column |
| [SQLParamData](sqlparamdata.md) | Get the next parameter that needs data during data-at-execution operations |
| [SQLPutData](sqlputdata.md) | Send parameter data in chunks during statement execution |
| [SQLDescribeParam](sqldescribeparam.md) | Retrieve the data type and size of a parameter marker |
| [SQLNumParams](sqlnumparams.md) | Count the number of parameters in a prepared statement |

### Fetch and process results

Use these functions to retrieve data from result sets and process query results.

| Function | Description |
| --- | --- |
| [SQLFetch](sqlfetch.md) | Fetch the next rowset of data and return bound column values |
| [SQLFetchScroll](sqlfetchscroll.md) | Fetch a rowset at an absolute or relative position in the result set |
| [SQLGetData](sqlgetdata.md) | Retrieve data for a single unbound column or large data in chunks |
| [SQLMoreResults](sqlmoreresults.md) | Move to the next result set when a statement returns multiple results |
| [SQLRowCount](sqlrowcount.md) | Get the number of rows affected by INSERT, UPDATE, or DELETE statements |
| [SQLCloseCursor](sqlclosecursor.md) | Close the cursor and discard pending results |
| [SQLGetCursorName](sqlgetcursorname.md) | Retrieve the name associated with a statement's cursor |

### Discover schema and metadata

Use these functions to query database schema information such as tables, columns, and keys.

| Function | Description |
| --- | --- |
| [SQLTables](sqltables.md) | List tables, views, and other table-like objects in the data source |
| [SQLColumns](sqlcolumns.md) | List columns and their attributes for specified tables |
| [SQLPrimaryKeys](sqlprimarykeys.md) | Retrieve the primary key columns for a table |
| [SQLForeignKeys](sqlforeignkeys.md) | List foreign keys for a table or foreign keys in other tables that reference it |
| [SQLSpecialColumns](sqlspecialcolumns.md) | Identify columns that uniquely identify a row or update automatically |
| [SQLStatistics](sqlstatistics.md) | Retrieve index information and table statistics |
| [SQLProcedures](sqlprocedures.md) | List stored procedures available in the data source |
| [SQLProcedureColumns](sqlprocedurecolumns.md) | Describe input/output parameters and result columns for stored procedures |

### Column and result set metadata

Use these functions to examine the structure of result sets and column attributes.

| Function | Description |
| --- | --- |
| [SQLDescribeCol](sqldescribecol.md) | Get the column name, type, size, and nullability for a result column |
| [SQLColAttribute](sqlcolattribute.md) | Retrieve a specific attribute of a result set column |
| [SQLNumResultCols](sqlnumresultcols.md) | Count the number of columns in a result set |
| [SQLGetTypeInfo](sqlgettypeinfo.md) | List the SQL data types supported by the data source |

### Privileges and security

Use these functions to retrieve permission information for database objects.

| Function | Description |
| --- | --- |
| [SQLTablePrivileges](sqltableprivileges.md) | List privileges granted on tables in the data source |
| [SQLColumnPrivileges](sqlcolumnprivileges.md) | List privileges granted on specific columns of a table |

### Environment and statement attributes

Use these functions to configure ODBC environment and statement behavior.

| Function | Description |
| --- | --- |
| [SQLSetEnvAttr](sqlsetenvattr.md) | Set environment attributes such as ODBC version and connection pooling |
| [SQLGetStmtAttr](sqlgetstmtattr.md) | Retrieve the current value of a statement attribute |
| [SQLSetStmtAttr](sqlsetstmtattr.md) | Configure statement behavior such as cursor type and query timeout |

### Descriptors

Use these functions to directly manipulate descriptor records for advanced parameter and column handling.

| Function | Description |
| --- | --- |
| [SQLGetDescField](sqlgetdescfield.md) | Retrieve a single field from a descriptor record |
| [SQLSetDescField](sqlsetdescfield.md) | Set a single field in a descriptor record |
| [SQLSetDescRec](sqlsetdescrec.md) | Set multiple fields in a descriptor record with a single call |

### Diagnostics and driver information

Use these functions to retrieve error information and query driver capabilities.

| Function | Description |
| --- | --- |
| [SQLGetDiagField](sqlgetdiagfield.md) | Retrieve a diagnostic field from an environment, connection, or statement |
| [SQLGetInfo](sqlgetinfo.md) | Get general information about the driver and data source capabilities |
| [SQLGetFunctions](sqlgetfunctions.md) | Determine which ODBC functions the driver supports |

### Resource cleanup

Use these functions to release handles and free resources.

| Function | Description |
| --- | --- |
| [SQLFreeHandle](sqlfreehandle.md) | Release an environment, connection, statement, or descriptor handle |
| [SQLFreeStmt](sqlfreestmt.md) | Free statement resources, close cursors, or unbind parameters and columns |

## Related content

- [SQL Server Native Client (ODBC)](../native-client/odbc/sql-server-native-client-odbc.md)
