---
title: "ODBC Functions and the Visual FoxPro ODBC Driver"
description: "ODBC Functions and the Visual FoxPro ODBC Driver"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC level 2 API functions [ODBC]"
  - "ODBC level 1 API functions [ODBC]"
  - "functions [ODBC], API"
  - "ODBC API functions [ODBC]"
  - "level 1 API functions [ODBC]"
  - "API functions [ODBC]"
  - "core level API functions [ODBC]"
  - "level 2 API functions [ODBC]"
  - "ODBC core level API functions [ODBC]"
---
# ODBC Functions and the Visual FoxPro ODBC Driver

The articles in this section provide a brief summary of ODBC API functions and any Visual FoxPro-specific details.

> [!NOTE]  
> For general information about ODBC functions, see [ODBC API Reference](../reference/syntax/odbc-api-reference.md) in the *ODBC Programmer's Guide*.

The ODBC API functions are divided into three main categories here: Core level API functions, Level 1 API functions, and Level 2 API functions.

> [!NOTE]  
> Several functions behave differently depending on whether the data source is defined as a connection to a directory of [free tables](visual-foxpro-terminology.md) (.dbf files) or to a Visual FoxPro [database](visual-foxpro-terminology.md) (.dbc file). Certain operations are supported only for database connections.

## Core level API support

The ODBC Core level API functions are listed in the following table. All of these functions are supported by the Visual FoxPro ODBC Driver.

- [SQLAllocConnect](sqlallocconnect-visual-foxpro-odbc-driver.md)
- [SQLAllocEnv](sqlallocenv-visual-foxpro-odbc-driver.md)
- [SQLAllocStmt](sqlallocstmt-visual-foxpro-odbc-driver.md)
- [SQLBindCol](sqlbindcol-visual-foxpro-odbc-driver.md)
- [SQLCancel](sqlcancel-visual-foxpro-odbc-driver.md)
- [SQLColAttributes](sqlcolattributes-visual-foxpro-odbc-driver.md)
- [SQLConnect](sqlconnect-visual-foxpro-odbc-driver.md)
- [SQLDescribeCol](sqldescribecol-visual-foxpro-odbc-driver.md)
- [SQLDisconnect](sqldisconnect-visual-foxpro-odbc-driver.md)
- [SQLError](sqlerror-visual-foxpro-odbc-driver.md)
- [SQLExecDirect](sqlexecdirect-visual-foxpro-odbc-driver.md)
- [SQLExecute](sqlexecute-visual-foxpro-odbc-driver.md)
- [SQLFetch](sqlfetch-visual-foxpro-odbc-driver.md)
- [SQLFreeConnect](sqlfreeconnect-visual-foxpro-odbc-driver.md)
- [SQLFreeEnv](sqlfreeenv-visual-foxpro-odbc-driver.md)
- [SQLFreeStmt](sqlfreestmt-visual-foxpro-odbc-driver.md)
- [SQLGetCursorName](sqlgetcursorname-visual-foxpro-odbc-driver.md)
- [SQLNumResultCols](sqlnumresultcols-visual-foxpro-odbc-driver.md)
- [SQLPrepare](sqlprepare-visual-foxpro-odbc-driver.md)
- [SQL Row Count](sql-row-count-visual-foxpro-odbc-driver.md)
- [SQLSetCursorName](sqlsetcursorname-visual-foxpro-odbc-driver.md)
- [SQLTransact](sqltransact-visual-foxpro-odbc-driver.md)

## Level 1 API Support

The ODBC Level 1 API functions are listed in the following table. All of these functions are fully or partially supported by the Visual FoxPro ODBC Driver.

- [SQLBindParameter](sqlbindparameter-visual-foxpro-odbc-driver.md)
- [SQLColumns](sqlcolumns-visual-foxpro-odbc-driver.md)
- [SQLDriverConnect](sqldriverconnect-visual-foxpro-odbc-driver.md)
- [SQLGetConnectOption](sqlgetconnectoption-visual-foxpro-odbc-driver.md)
- [SQLGetData](sqlgetdata-visual-foxpro-odbc-driver.md)
- [SQLGetFunctions](sqlgetfunctions-visual-foxpro-odbc-driver.md)
- [SQLGetInfo](sqlgetinfo-visual-foxpro-odbc-driver.md)
- [SQLGetStmtOption](sqlgetstmtoption-visual-foxpro-odbc-driver.md)
- [SQLGetTypeInfo](sqlgettypeinfo-visual-foxpro-odbc-driver.md)
- [SQLParamData](sqlparamdata-visual-foxpro-odbc-driver.md)
- [SQLPutData](sqlputdata-visual-foxpro-odbc-driver.md)
- [SQLSetConnectOption](sqlsetconnectoption-visual-foxpro-odbc-driver.md)
- [SQLSetStmtOption](sqlsetstmtoption-visual-foxpro-odbc-driver.md)
- [SQLSpecialColumns](sqlspecialcolumns-visual-foxpro-odbc-driver.md)
- [SQLStatistics](sqlstatistics-visual-foxpro-odbc-driver.md)
- [SQLTables](sqltables-visual-foxpro-odbc-driver.md)

## Level 2 API Support

The following ODBC Level 2 API functions are fully or partially supported:

- [SQLDataSources](sqldatasources-visual-foxpro-odbc-driver.md)
- [SQLDrivers](sqldrivers-visual-foxpro-odbc-driver.md)
- [SQLExtendedFetch](sqlextendedfetch-visual-foxpro-odbc-driver.md)
- [SQLMoreResults](sqlmoreresults-visual-foxpro-odbc-driver.md)
- [SQLNumParams](sqlnumparams-visual-foxpro-odbc-driver.md)
- [SQLParamOptions](sqlparamoptions-visual-foxpro-odbc-driver.md)
- [SQLPrimaryKeys](sqlprimarykeys-visual-foxpro-odbc-driver.md)
- [SQLSetPos](sqlsetpos-visual-foxpro-odbc-driver.md)
- [SQLSetScrollOptions](sqlsetscrolloptions-visual-foxpro-odbc-driver.md) (partial support)

The following Level 2 API functions aren't supported:

- `SQLBrowseConnect`
- `SQLColumnPrivileges`
- `SQLDescribeParam`
- `SQLForeignKeys`
- `SQLNativeSql`
- `SQLProcedureColumns`
- `SQLProcedures`
- `SQLTablePrivileges`
