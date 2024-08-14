---
title: "ODBC API Reference"
description: "ODBC API Reference"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apitype: "dllExport"
---
# ODBC API Reference

The articles in this section describe each ODBC function in alphabetical order. Each function is defined as a C programming language function. Descriptions include the following information:

- Purpose
- ODBC version
- Standard CLI conformance level
- Syntax
- Arguments
- Return values
- Diagnostics
- Comments about usage and implementation
- Code example
- References to related functions

The standard CLI conformance level can be one of the following: ISO 92, Open Group, ODBC, or Deprecated. A function tagged as ISO 92-conformant also appears in Open Group version 1, because Open Group is a pure superset of ISO 92. A function tagged as Open Group-compliant also appears in ODBC 3.*x*, because ODBC 3.*x* is a pure superset of Open Group version 1. A function tagged as ODBC-compliant appears in neither standard. A function tagged as deprecated has been deprecated in ODBC 3.*x*.

Handling of diagnostic information is described in the [SQLGetDiagField Function](sqlgetdiagfield-function.md) function description. The text associated with SQLSTATE values is included to provide a description of the condition but isn't intended to prescribe specific text.

> [!NOTE]  
> For driver-specific information about ODBC functions, see the section for the driver.

This section contains articles for the following functions:

- [SQLAllocConnect Function](sqlallocconnect-function.md)
- [SQLAllocEnv Function](sqlallocenv-function.md)
- [SQLAllocHandle Function](sqlallochandle-function.md)
- [SQLAllocStmt Function](sqlallocstmt-function.md)
- [SQLBindCol Function](sqlbindcol-function.md)
- [SQLBindParameter Function](sqlbindparameter-function.md)
- [SQLBrowseConnect Function](sqlbrowseconnect-function.md)
- [SQLBulkOperations Function](sqlbulkoperations-function.md)
- [SQLCancel Function](sqlcancel-function.md)
- [SQLCancelHandle Function](sqlcancelhandle-function.md)
- [SQLCloseCursor Function](sqlclosecursor-function.md)
- [SQLColAttribute Function](sqlcolattribute-function.md)
- [SQLColAttributes Function](sqlcolattributes-function.md)
- [SQLColumnPrivileges Function](sqlcolumnprivileges-function.md)
- [SQLColumns Function](sqlcolumns-function.md)
- [SQLCompleteAsync Function](sqlcompleteasync-function.md)
- [SQLConnect Function](sqlconnect-function.md)
- [SQLCopyDesc Function](sqlcopydesc-function.md)
- [SQLDataSources Function](sqldatasources-function.md)
- [SQLDescribeCol Function](sqldescribecol-function.md)
- [SQLDescribeParam Function](sqldescribeparam-function.md)
- [SQLDisconnect Function](sqldisconnect-function.md)
- [SQLDriverConnect Function](sqldriverconnect-function.md)
- [SQLDrivers Function](sqldrivers-function.md)
- [SQLEndTran Function](sqlendtran-function.md)
- [SQLError Function](sqlerror-function.md)
- [SQLExecDirect Function](sqlexecdirect-function.md)
- [SQLExecute Function](sqlexecute-function.md)
- [SQLExtendedFetch Function](sqlextendedfetch-function.md)
- [SQLFetch Function](sqlfetch-function.md)
- [SQLFetchScroll Function](sqlfetchscroll-function.md)
- [SQLForeignKeys Function](sqlforeignkeys-function.md)
- [SQLFreeConnect Function](sqlfreeconnect-function.md)
- [SQLFreeEnv Function](sqlfreeenv-function.md)
- [SQLFreeHandle Function](sqlfreehandle-function.md)
- [SQLFreeStmt Function](sqlfreestmt-function.md)
- [SQLGetConnectAttr Function](sqlgetconnectattr-function.md)
- [SQLGetConnectOption Function](sqlgetconnectoption-function.md)
- [SQLGetCursorName Function](sqlgetcursorname-function.md)
- [SQLGetData Function](sqlgetdata-function.md)
- [SQLGetDescField Function](sqlgetdescfield-function.md)
- [SQLGetDescRec Function](sqlgetdescrec-function.md)
- [SQLGetDiagField Function](sqlgetdiagfield-function.md)
- [SQLGetDiagRec Function](sqlgetdiagrec-function.md)
- [SQLGetEnvAttr Function](sqlgetenvattr-function.md)
- [SQLGetFunctions Function](sqlgetfunctions-function.md)
- [SQLGetInfo Function](sqlgetinfo-function.md)
- [SQLGetStmtAttr Function](sqlgetstmtattr-function.md)
- [SQLGetStmtOption Function](sqlgetstmtoption-function.md)
- [SQLGetTypeInfo Function](sqlgettypeinfo-function.md)
- [SQLMoreResults Function](sqlmoreresults-function.md)
- [SQLNativeSql Function](sqlnativesql-function.md)
- [SQLNumParams Function](sqlnumparams-function.md)
- [SQLNumResultCols Function](sqlnumresultcols-function.md)
- [SQLParamData Function](sqlparamdata-function.md)
- [SQLParamOptions Function](sqlparamoptions-function.md)
- [SQLPrepare Function](sqlprepare-function.md)
- [SQLPrimaryKeys Function](sqlprimarykeys-function.md)
- [SQLProcedureColumns Function](sqlprocedurecolumns-function.md)
- [SQLProcedures Function](sqlprocedures-function.md)
- [SQLPutData Function](sqlputdata-function.md)
- [SQLRowCount Function](sqlrowcount-function.md)
- [SQLSetConnectAttr Function](sqlsetconnectattr-function.md)
- [SQLSetConnectOption Function](sqlsetconnectoption-function.md)
- [SQLSetCursorName Function](sqlsetcursorname-function.md)
- [SQLSetDescField Function](sqlsetdescfield-function.md)
- [SQLSetDescRec Function](sqlsetdescrec-function.md)
- [SQLSetEnvAttr Function](sqlsetenvattr-function.md)
- [SQLSetParam Function](sqlsetparam-function.md)
- [SQLSetPos Function](sqlsetpos-function.md)
- [SQLSetScrollOptions Function](sqlsetscrolloptions-function.md)
- [SQLSetStmtAttr Function](sqlsetstmtattr-function.md)
- [SQLSetStmtOption Function](sqlsetstmtoption-function.md)
- [SQLSpecialColumns Function](sqlspecialcolumns-function.md)
- [SQLStatistics Function](sqlstatistics-function.md)
- [SQLTablePrivileges Function](sqltableprivileges-function.md)
- [SQLTables Function](sqltables-function.md)
- [SQLTransact Function](sqltransact-function.md)
