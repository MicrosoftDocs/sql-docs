---
title: "ODBC API Implementation Details | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, functions"
  - "SQL Server Native Client ODBC driver, SQL Server-specific behaviors"
  - "ODBC, SQL Server-specific behaviors"
  - "functions [ODBC]"
ms.assetid: dca92489-f179-4b1f-997c-adcc46aa17a3
caps.latest.revision: 42
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ODBC API Implementation Details
  This section documents the ODBC functions that exhibit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific behaviors when used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver. Not all ODBC functions are documented here. The individual topics only discuss the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific issues for an ODBC function. They are not a complete reference for the ODBC functions.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with the ODBC 3.51 specification and, if you are using the Windows 7 SDK, with the ODBC 3.8 specification. For a comprehensive ODBC reference, view the [ODBC Programmer's Reference](http://go.microsoft.com/fwlink/?LinkId=45250) online.  
  
## In This Section  
  
-   [SQLBindCol](../../../2014/database-engine/dev-guide/sqlbindcol.md)  
  
-   [SQLBindParameter](../../../2014/database-engine/dev-guide/sqlbindparameter.md)  
  
-   [SQLBrowseConnect](../../../2014/database-engine/dev-guide/sqlbrowseconnect.md)  
  
-   [SQLCancel](../../../2014/database-engine/dev-guide/sqlcancel.md)  
  
-   [SQLCloseCursor](../../../2014/database-engine/dev-guide/sqlclosecursor.md)  
  
-   [SQLColAttribute](../../../2014/database-engine/dev-guide/sqlcolattribute.md)  
  
-   [SQLColumnPrivileges](../../../2014/database-engine/dev-guide/sqlcolumnprivileges.md)  
  
-   [SQLColumns](../../../2014/database-engine/dev-guide/sqlcolumns.md)  
  
-   [SQLConfigDataSource](../../../2014/database-engine/dev-guide/sqlconfigdatasource.md)  
  
-   [SQLConnect](../../../2014/database-engine/dev-guide/sqlconnect.md)  
  
-   [SQLDescribeCol](../../../2014/database-engine/dev-guide/sqldescribecol.md)  
  
-   [SQLDescribeParam](../../../2014/database-engine/dev-guide/sqldescribeparam.md)  
  
-   [SQLDriverConnect](../../../2014/database-engine/dev-guide/sqldriverconnect.md)  
  
-   [SQLDrivers](../../../2014/database-engine/dev-guide/sqldrivers.md)  
  
-   [SQLEndTran](../../../2014/database-engine/dev-guide/sqlendtran.md)  
  
-   [SQLExecDirect](../../../2014/database-engine/dev-guide/sqlexecdirect.md)  
  
-   [SQLExecute](../../../2014/database-engine/dev-guide/sqlexecute.md)  
  
-   [SQLFetch](../../../2014/database-engine/dev-guide/sqlfetch.md)  
  
-   [SQLFetchScroll](../../../2014/database-engine/dev-guide/sqlfetchscroll.md)  
  
-   [SQLForeignKeys](../../../2014/database-engine/dev-guide/sqlforeignkeys.md)  
  
-   [SQLFreeHandle](../../../2014/database-engine/dev-guide/sqlfreehandle.md)  
  
-   [SQLFreeStmt](../../../2014/database-engine/dev-guide/sqlfreestmt.md)  
  
-   [SQLGetConnectAttr](../../../2014/database-engine/dev-guide/sqlgetconnectattr.md)  
  
-   [SQLGetCursorName](../../../2014/database-engine/dev-guide/sqlgetcursorname.md)  
  
-   [SQLGetData](../../../2014/database-engine/dev-guide/sqlgetdata.md)  
  
-   [SQLGetDescField](../../../2014/database-engine/dev-guide/sqlgetdescfield.md)  
  
-   [SQLGetDescRec](../../../2014/database-engine/dev-guide/sqlgetdescrec.md)  
  
-   [SQLGetDiagField](../../../2014/database-engine/dev-guide/sqlgetdiagfield.md)  
  
-   [SQLGetFunctions](../../../2014/database-engine/dev-guide/sqlgetfunctions.md)  
  
-   [SQLGetInfo](../../../2014/database-engine/dev-guide/sqlgetinfo.md)  
  
-   [SQLGetStmtAttr](../../../2014/database-engine/dev-guide/sqlgetstmtattr.md)  
  
-   [SQLGetTypeInfo](../../../2014/database-engine/dev-guide/sqlgettypeinfo.md)  
  
-   [SQLMoreResults](../../../2014/database-engine/dev-guide/sqlmoreresults.md)  
  
-   [SQLNativeSql](../../../2014/database-engine/dev-guide/sqlnativesql.md)  
  
-   [SQLNumParams](../../../2014/database-engine/dev-guide/sqlnumparams.md)  
  
-   [SQLNumResultCols](../../../2014/database-engine/dev-guide/sqlnumresultcols.md)  
  
-   [SQLParamData](../../../2014/database-engine/dev-guide/sqlparamdata.md)  
  
-   [SQLPrimaryKeys](../../../2014/database-engine/dev-guide/sqlprimarykeys.md)  
  
-   [SQLProcedureColumns](../../../2014/database-engine/dev-guide/sqlprocedurecolumns.md)  
  
-   [SQLProcedures](../../../2014/database-engine/dev-guide/sqlprocedures.md)  
  
-   [SQLPutData](../../../2014/database-engine/dev-guide/sqlputdata.md)  
  
-   [SQLRowCount](../../../2014/database-engine/dev-guide/sqlrowcount.md)  
  
-   [SQLSetConnectAttr](../../../2014/database-engine/dev-guide/sqlsetconnectattr.md)  
  
-   [SQLSetDescField](../../../2014/database-engine/dev-guide/sqlsetdescfield.md)  
  
-   [SQLSetDescRec](../../../2014/database-engine/dev-guide/sqlsetdescrec.md)  
  
-   [SQLSetEnvAttr](../../../2014/database-engine/dev-guide/sqlsetenvattr.md)  
  
-   [SQLSetStmtAttr](../../../2014/database-engine/dev-guide/sqlsetstmtattr.md)  
  
-   [SQLSpecialColumns](../../../2014/database-engine/dev-guide/sqlspecialcolumns.md)  
  
-   [SQLStatistics](../../../2014/database-engine/dev-guide/sqlstatistics.md)  
  
-   [SQLTablePrivileges](../../../2014/database-engine/dev-guide/sqltableprivileges.md)  
  
-   [SQLTables](../../../2014/database-engine/dev-guide/sqltables.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41; Reference](../../../2014/database-engine/dev-guide/sql-server-native-client-odbc-reference.md)   
 [Building Applications with SQL Server Native Client](../../../2014/database-engine/dev-guide/building-applications-with-sql-server-native-client.md)  
  
  