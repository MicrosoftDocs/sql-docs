---
description: "ODBC Functions and the Visual FoxPro ODBC Driver"
title: "ODBC Functions and the Visual FoxPro ODBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
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
ms.assetid: 512f9cee-ffad-439b-b612-b49c34c32658
author: David-Engel
ms.author: v-davidengel
---
# ODBC Functions and the Visual FoxPro ODBC Driver
The topics in this section provide a brief summary of ODBC API functions and any Visual FoxPro-specific details.  
  
> [!NOTE]  
>  For general information about ODBC functions, see [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md) in "ODBC Programmer's Guide".  
  
 The ODBC API functions have been divided into three main categories here: Core Level API functions, Level 1 API functions, and Level 2 API functions.  
  
> [!NOTE]  
>  Several of the functions behave differently depending on whether the data source is defined as a connection to a directory of [free tables](../../odbc/microsoft/visual-foxpro-terminology.md) (.dbf files) or to a Visual FoxPro [database](../../odbc/microsoft/visual-foxpro-terminology.md) (.dbc file). Certain operations are supported only for database connections.  
  
## Core Level API Support  
 The ODBC Core Level API functions are listed in the following table. All of these functions are supported by the Visual FoxPro ODBC Driver.  

:::row:::
    :::column:::
        [SQLAllocConnect](../../odbc/microsoft/sqlallocconnect-visual-foxpro-odbc-driver.md)  
        [SQLAllocEnv](../../odbc/microsoft/sqlallocenv-visual-foxpro-odbc-driver.md)  
        [SQLAllocStmt](../../odbc/microsoft/sqlallocstmt-visual-foxpro-odbc-driver.md)  
        [SQLBindCol](../../odbc/microsoft/sqlbindcol-visual-foxpro-odbc-driver.md)  
        [SQLCancel](../../odbc/microsoft/sqlcancel-visual-foxpro-odbc-driver.md)  
        [SQLColAttributes](../../odbc/microsoft/sqlcolattributes-visual-foxpro-odbc-driver.md)  
        [SQLConnect](../../odbc/microsoft/sqlconnect-visual-foxpro-odbc-driver.md)  
        [SQLDescribeCol](../../odbc/microsoft/sqldescribecol-visual-foxpro-odbc-driver.md)  
        [SQLDisconnect](../../odbc/microsoft/sqldisconnect-visual-foxpro-odbc-driver.md)  
        [SQLError](../../odbc/microsoft/sqlerror-visual-foxpro-odbc-driver.md)  
        [SQLExecDirect](../../odbc/microsoft/sqlexecdirect-visual-foxpro-odbc-driver.md)  
    :::column-end:::
    :::column:::
        [SQLExecute](../../odbc/microsoft/sqlexecute-visual-foxpro-odbc-driver.md)  
        [SQLFetch](../../odbc/microsoft/sqlfetch-visual-foxpro-odbc-driver.md)  
        [SQLFreeConnect](../../odbc/microsoft/sqlfreeconnect-visual-foxpro-odbc-driver.md)  
        [SQLFreeEnv](../../odbc/microsoft/sqlfreeenv-visual-foxpro-odbc-driver.md)  
        [SQLFreeStmt](../../odbc/microsoft/sqlfreestmt-visual-foxpro-odbc-driver.md)  
        [SQLGetCursorName](../../odbc/microsoft/sqlgetcursorname-visual-foxpro-odbc-driver.md)  
        [SQLNumResultCols](../../odbc/microsoft/sqlnumresultcols-visual-foxpro-odbc-driver.md)  
        [SQLPrepare](../../odbc/microsoft/sqlprepare-visual-foxpro-odbc-driver.md)  
        [SQLRowCount](../../odbc/microsoft/sql-row-count-visual-foxpro-odbc-driver.md)  
        [SQLSetCursorName](../../odbc/microsoft/sqlsetcursorname-visual-foxpro-odbc-driver.md)  
        [SQLTransact](../../odbc/microsoft/sqltransact-visual-foxpro-odbc-driver.md)  
    :::column-end:::
:::row-end:::

## Level 1 API Support  
 The ODBC Level 1 API functions are listed in the following table. All of these functions are fully or partially supported by the Visual FoxPro ODBC Driver.  

:::row:::
    :::column:::
        [SQLBindParameter](../../odbc/microsoft/sqlbindparameter-visual-foxpro-odbc-driver.md)  
        [SQLColumns](../../odbc/microsoft/sqlcolumns-visual-foxpro-odbc-driver.md)  
        [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md)  
        [SQLGetConnectOption](../../odbc/microsoft/sqlgetconnectoption-visual-foxpro-odbc-driver.md)  
        [SQLGetData](../../odbc/microsoft/sqlgetdata-visual-foxpro-odbc-driver.md)  
        [SQLGetFunctions](../../odbc/microsoft/sqlgetfunctions-visual-foxpro-odbc-driver.md)  
        [SQLGetInfo](../../odbc/microsoft/sqlgetinfo-visual-foxpro-odbc-driver.md)  
        [SQLGetStmtOption](../../odbc/microsoft/sqlgetstmtoption-visual-foxpro-odbc-driver.md)  
    :::column-end:::
    :::column:::
        [SQLGetTypeInfo](../../odbc/microsoft/sqlgettypeinfo-visual-foxpro-odbc-driver.md)  
        [SQLParamData](../../odbc/microsoft/sqlparamdata-visual-foxpro-odbc-driver.md)  
        [SQLPutData](../../odbc/microsoft/sqlputdata-visual-foxpro-odbc-driver.md)  
        [SQLSetConnectOption](../../odbc/microsoft/sqlsetconnectoption-visual-foxpro-odbc-driver.md)  
        [SQLSetStmtOption](../../odbc/microsoft/sqlsetstmtoption-visual-foxpro-odbc-driver.md)  
        [SQLSpecialColumns](../../odbc/microsoft/sqlspecialcolumns-visual-foxpro-odbc-driver.md)  
        [SQLStatistics](../../odbc/microsoft/sqlstatistics-visual-foxpro-odbc-driver.md)  
        [SQLTables](../../odbc/microsoft/sqltables-visual-foxpro-odbc-driver.md)  
    :::column-end:::
:::row-end:::

## Level 2 API Support  
 The following ODBC Level 2 API functions are fully or partially supported:  
  
-   [SQLDataSources](../../odbc/microsoft/sqldatasources-visual-foxpro-odbc-driver.md)  
  
-   [SQLDrivers](../../odbc/microsoft/sqldrivers-visual-foxpro-odbc-driver.md)  
  
-   [SQLExtendedFetch](../../odbc/microsoft/sqlextendedfetch-visual-foxpro-odbc-driver.md)  
  
-   [SQLMoreResults](../../odbc/microsoft/sqlmoreresults-visual-foxpro-odbc-driver.md)  
  
-   [SQLNumParams](../../odbc/microsoft/sqlnumparams-visual-foxpro-odbc-driver.md)  
  
-   [SQLParamOptions](../../odbc/microsoft/sqlparamoptions-visual-foxpro-odbc-driver.md)  
  
-   [SQLPrimaryKeys](../../odbc/microsoft/sqlprimarykeys-visual-foxpro-odbc-driver.md)  
  
-   [SQLSetPos](../../odbc/microsoft/sqlsetpos-visual-foxpro-odbc-driver.md)  
  
-   [SQLSetScrollOptions](../../odbc/microsoft/sqlsetscrolloptions-visual-foxpro-odbc-driver.md) (partial support)  
  
 The following Level 2 API functions are not supported:  
  
-   SQLBrowseConnect  
  
-   SQLColumnPrivileges  
  
-   SQLDescribeParam  
  
-   SQLForeignKeys  
  
-   SQLNativeSql  
  
-   SQLProcedureColumns  
  
-   SQLProcedures  
  
-   SQLTablePrivileges
