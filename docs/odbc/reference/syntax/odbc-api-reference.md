---
title: "ODBC API Reference | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apitype: "dllExport"
ms.assetid: b7a49774-f458-44ce-9a04-a0457501405b
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC API Reference
The topics in this section describe each ODBC function in alphabetical order. Each function is defined as a C programming language function. Descriptions include the following:  
  
-   Purpose  
  
-   ODBC version  
  
-   Standard CLI conformance level  
  
-   Syntax  
  
-   Arguments  
  
-   Return values  
  
-   Diagnostics  
  
-   Comments about usage and implementation  
  
-   Code example  
  
-   References to related functions  
  
 The standard CLI conformance level can be one of the following: ISO 92, Open Group, ODBC, or Deprecated. A function tagged as ISO 92-conformant also appears in Open Group version 1, because Open Group is a pure superset of ISO 92. A function tagged as Open Group-compliant also appears in ODBC 3.*x*, because ODBC 3.*x* is a pure superset of Open Group version 1. A function tagged as ODBC-compliant appears in neither standard. A function tagged as deprecated has been deprecated in ODBC 3.*x*.  
  
 Handling of diagnostic information is described in the [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md) function description. The text associated with SQLSTATE values is included to provide a description of the condition but is not intended to prescribe specific text.  
  
> [!NOTE]  
>  For driver-specific information about ODBC functions, see the section for the driver.  
  
 This section contains topics for the following functions:  
  
-   [SQLAllocConnect Function](../../../odbc/reference/syntax/sqlallocconnect-function.md)  
  
-   [SQLAllocEnv Function](../../../odbc/reference/syntax/sqlallocenv-function.md)  
  
-   [SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)  
  
-   [SQLAllocStmt Function](../../../odbc/reference/syntax/sqlallocstmt-function.md)  
  
-   [SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)  
  
-   [SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)  
  
-   [SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)  
  
-   [SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md)  
  
-   [SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)  
  
-   [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md)  
  
-   [SQLCloseCursor Function](../../../odbc/reference/syntax/sqlclosecursor-function.md)  
  
-   [SQLColAttribute Function](../../../odbc/reference/syntax/sqlcolattribute-function.md)  
  
-   [SQLColAttributes Function](../../../odbc/reference/syntax/sqlcolattributes-function.md)  
  
-   [SQLColumnPrivileges Function](../../../odbc/reference/syntax/sqlcolumnprivileges-function.md)  
  
-   [SQLColumns Function](../../../odbc/reference/syntax/sqlcolumns-function.md)  
  
-   [SQLCompleteAsync Function](../../../odbc/reference/syntax/sqlcompleteasync-function.md)  
  
-   [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)  
  
-   [SQLCopyDesc Function](../../../odbc/reference/syntax/sqlcopydesc-function.md)  
  
-   [SQLDataSources Function](../../../odbc/reference/syntax/sqldatasources-function.md)  
  
-   [SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)  
  
-   [SQLDescribeParam Function](../../../odbc/reference/syntax/sqldescribeparam-function.md)  
  
-   [SQLDisconnect Function](../../../odbc/reference/syntax/sqldisconnect-function.md)  
  
-   [SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)  
  
-   [SQLDrivers Function](../../../odbc/reference/syntax/sqldrivers-function.md)  
  
-   [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md)  
  
-   [SQLError Function](../../../odbc/reference/syntax/sqlerror-function.md)  
  
-   [SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)  
  
-   [SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)  
  
-   [SQLExtendedFetch Function](../../../odbc/reference/syntax/sqlextendedfetch-function.md)  
  
-   [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)  
  
-   [SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)  
  
-   [SQLForeignKeys Function](../../../odbc/reference/syntax/sqlforeignkeys-function.md)  
  
-   [SQLFreeConnect Function](../../../odbc/reference/syntax/sqlfreeconnect-function.md)  
  
-   [SQLFreeEnv Function](../../../odbc/reference/syntax/sqlfreeenv-function.md)  
  
-   [SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)  
  
-   [SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)  
  
-   [SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)  
  
-   [SQLGetConnectOption Function](../../../odbc/reference/syntax/sqlgetconnectoption-function.md)  
  
-   [SQLGetCursorName Function](../../../odbc/reference/syntax/sqlgetcursorname-function.md)  
  
-   [SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md)  
  
-   [SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)  
  
-   [SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)  
  
-   [SQLGetDiagField Function](../../../odbc/reference/syntax/sqlgetdiagfield-function.md)  
  
-   [SQLGetDiagRec Function](../../../odbc/reference/syntax/sqlgetdiagrec-function.md)  
  
-   [SQLGetEnvAttr Function](../../../odbc/reference/syntax/sqlgetenvattr-function.md)  
  
-   [SQLGetFunctions Function](../../../odbc/reference/syntax/sqlgetfunctions-function.md)  
  
-   [SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md)  
  
-   [SQLGetStmtAttr Function](../../../odbc/reference/syntax/sqlgetstmtattr-function.md)  
  
-   [SQLGetStmtOption Function](../../../odbc/reference/syntax/sqlgetstmtoption-function.md)  
  
-   [SQLGetTypeInfo Function](../../../odbc/reference/syntax/sqlgettypeinfo-function.md)  
  
-   [SQLMoreResults Function](../../../odbc/reference/syntax/sqlmoreresults-function.md)  
  
-   [SQLNativeSql Function](../../../odbc/reference/syntax/sqlnativesql-function.md)  
  
-   [SQLNumParams Function](../../../odbc/reference/syntax/sqlnumparams-function.md)  
  
-   [SQLNumResultCols Function](../../../odbc/reference/syntax/sqlnumresultcols-function.md)  
  
-   [SQLParamData Function](../../../odbc/reference/syntax/sqlparamdata-function.md)  
  
-   [SQLParamOptions Function](../../../odbc/reference/syntax/sqlparamoptions-function.md)  
  
-   [SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md)  
  
-   [SQLPrimaryKeys Function](../../../odbc/reference/syntax/sqlprimarykeys-function.md)  
  
-   [SQLProcedureColumns Function](../../../odbc/reference/syntax/sqlprocedurecolumns-function.md)  
  
-   [SQLProcedures Function](../../../odbc/reference/syntax/sqlprocedures-function.md)  
  
-   [SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md)  
  
-   [SQLRowCount Function](../../../odbc/reference/syntax/sqlrowcount-function.md)  
  
-   [SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)  
  
-   [SQLSetConnectOption Function](../../../odbc/reference/syntax/sqlsetconnectoption-function.md)  
  
-   [SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md)  
  
-   [SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)  
  
-   [SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)  
  
-   [SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md)  
  
-   [SQLSetParam Function](../../../odbc/reference/syntax/sqlsetparam-function.md)  
  
-   [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md)  
  
-   [SQLSetScrollOptions Function](../../../odbc/reference/syntax/sqlsetscrolloptions-function.md)  
  
-   [SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)  
  
-   [SQLSetStmtOption Function](../../../odbc/reference/syntax/sqlsetstmtoption-function.md)  
  
-   [SQLSpecialColumns Function](../../../odbc/reference/syntax/sqlspecialcolumns-function.md)  
  
-   [SQLStatistics Function](../../../odbc/reference/syntax/sqlstatistics-function.md)  
  
-   [SQLTablePrivileges Function](../../../odbc/reference/syntax/sqltableprivileges-function.md)  
  
-   [SQLTables Function](../../../odbc/reference/syntax/sqltables-function.md)  
  
-   [SQLTransact Function](../../../odbc/reference/syntax/sqltransact-function.md)
