---
title: "ODBC Table-Valued Parameter API Summary"
description: "ODBC Table-Valued Parameter API Summary"
author: markingmyname
ms.author: maghan
ms.date: 01/26/2026
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ODBC, API support for table-valued parameters"
  - "table-valued parameters (ODBC), API support"
---

# ODBC table-valued parameter API summary

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

ODBC supports table-valued parameters (table-valued parameters) through enhancements to several existing API functions. These functions work together to describe table-valued parameter schemas, bind and transmit row sets, fetch metadata, and manage descriptor records.  
The following sections organize the relevant ODBC functions into logical groups to help you understand how each part of the table-valued parameter  pipeline operates.

## Table-valued parameter schema discovery and metadata functions

These functions help an application discover the structure of table-valued parameters, including column metadata, parameter definitions, and available SQL data types.

| Function | Purpose |
|----------|----------|
| [SQLColumns](../../relational-databases/native-client-odbc-api/sqlcolumns.md) | Retrieves column metadata for a table-valued parameter or table type |
| [SQLDescribeParam](../../relational-databases/native-client-odbc-api/sqldescribeparam.md) | Returns metadata for a table-valued parameter  parameter, including type, precision, and scale |
| [SQLGetTypeInfo](../../relational-databases/native-client-odbc-api/sqlgettypeinfo.md) | Retrieves the SQL data types supported by the driver for table-valued parameter columns |
| [SQLPrimaryKeys](../../relational-databases/native-client-odbc-api/sqlprimarykeys.md) | Provides information about key columns in table types used with table-valued parameters |
| [SQLProcedureColumns](../../relational-databases/native-client-odbc-api/sqlprocedurecolumns.md) | Retrieves metadata for table-valued parameter-related stored procedure parameters |
| [SQLTables](../../relational-databases/native-client-odbc-api/sqltables.md) | Lists tables and table types available as sources for table-valued parameter  declarations |

## Table-valued parameter binding and data transmission

These functions handle binding structured data to parameters, passing the data to the server, or streaming rows during execution.

| Function | Purpose |
|----------|---------|
| [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md) | Binds a table-valued parameter to an application buffer or rowset |
| [SQLParamData](../../relational-databases/native-client-odbc-api/sqlparamdata.md) | Retrieves the next part of a streamed table-valued parameter  parameter when using data-at-execution |
| [SQLPutData](../../relational-databases/native-client-odbc-api/sqlputdata.md) | Sends table-valued parameter  data in chunks during data-at-execution operations |

## Statement execution for table-valued parameters

These functions execute SQL statements that reference table-valued parameters and manage the execution lifecycle.

| Function | Purpose |
|----------|---------|
| [SQLExecDirect](../../relational-databases/native-client-odbc-api/sqlexecdirect.md) | Executes a SQL statement that uses table-valued parameters without preparing it first |
| [SQLExecute](../../relational-databases/native-client-odbc-api/sqlexecute.md) | Executes a previously prepared SQL statement that includes table-valued parameter  parameters |

## Descriptor and attribute management for table-valued parameters

Use these functions to manage descriptor fields and statement attributes required to describe rowsets and structured parameters correctly.

| Function | Purpose |
|----------|---------|
| [SQLGetDescField](../../relational-databases/native-client-odbc-api/sqlgetdescfield.md) | Retrieves descriptor metadata for table-valued parameter  columns or rowsets |
| [SQLSetDescField](../../relational-databases/native-client-odbc-api/sqlsetdescfield.md) | Sets descriptor metadata for table-valued parameter  columns or rowsets |
| [SQLSetDescRec](../../relational-databases/native-client-odbc-api/sqlsetdescrec.md) | Sets a full descriptor record for structured table-valued parameter  data |
| [SQLGetStmtAttr](../../relational-databases/native-client-odbc-api/sqlgetstmtattr.md) | Retrieves statement attributes affecting table-valued parameter  execution behavior |
| [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) | Sets statement attributes such as rowset sizes or streaming flags for table-valued parameter  operations |

## Diagnostics and error handling for table-valued parameters

Use these functions to help your applications detect errors, warnings, or status messages during table-valued parameter  binding and execution.

| Function | Purpose |
|----------|---------|
| [SQLGetDiagField](../../relational-databases/native-client-odbc-api/sqlgetdiagfield.md) | Retrieves diagnostic information generated during table-valued parameter  processing |

## Related content

- [Table-Valued Parameters (ODBC)](table-valued-parameters-odbc.md)
