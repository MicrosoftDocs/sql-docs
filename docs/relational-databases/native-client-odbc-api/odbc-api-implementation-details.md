---
title: "ODBC API Implementation Details | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, functions"
  - "SQL Server Native Client ODBC driver, SQL Server-specific behaviors"
  - "ODBC, SQL Server-specific behaviors"
  - "functions [ODBC]"
ms.assetid: dca92489-f179-4b1f-997c-adcc46aa17a3
caps.latest.revision: 45
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ODBC API Implementation Details
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Open Database Connectivity (ODBC) is a Microsoft Win32 application programming interface used by applications to access data in ODBC data sources.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reference does not document all of the ODBC function calls. Only those functions that have driver-specific parameters or behaviors when used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver are discussed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with the ODBC 3.51 specification. For a comprehensive reference of ODBC 3.51, download the Microsoft Data Access Components SDK from the [Data Access and Storage Developer Center](http://go.microsoft.com/fwlink?linkid=4173), or view the [ODBC Programmer's Reference](http://go.microsoft.com/fwlink/?LinkId=45250) online.  
  
## In This Section  
  
-   [SQLBindCol](../../relational-databases/extended-stored-procedures-reference/sqlbindcol.md)  
  
-   [SQLBindParameter](../../relational-databases/extended-stored-procedures-reference/sqlbindparameter.md)  
  
-   [SQLBrowseConnect](../../relational-databases/extended-stored-procedures-reference/sqlbrowseconnect.md)  
  
-   [SQLCancel](../../relational-databases/extended-stored-procedures-reference/sqlcancel.md)  
  
-   [SQLCloseCursor](../../relational-databases/extended-stored-procedures-reference/sqlclosecursor.md)  
  
-   [SQLColAttribute](../../relational-databases/extended-stored-procedures-reference/sqlcolattribute.md)  
  
-   [SQLColumnPrivileges](../../relational-databases/extended-stored-procedures-reference/sqlcolumnprivileges.md)  
  
-   [SQLColumns](../../relational-databases/extended-stored-procedures-reference/sqlcolumns.md)  
  
-   [SQLConfigDataSource](../../relational-databases/extended-stored-procedures-reference/sqlconfigdatasource.md)  
  
-   [SQLConnect](../../relational-databases/extended-stored-procedures-reference/sqlconnect.md)  
  
-   [SQLDescribeCol](../../relational-databases/extended-stored-procedures-reference/sqldescribecol.md)  
  
-   [SQLDescribeParam](../../relational-databases/extended-stored-procedures-reference/sqldescribeparam.md)  
  
-   [SQLDriverConnect](../../relational-databases/extended-stored-procedures-reference/sqldriverconnect.md)  
  
-   [SQLDrivers](../../relational-databases/extended-stored-procedures-reference/sqldrivers.md)  
  
-   [SQLEndTran](../../relational-databases/extended-stored-procedures-reference/sqlendtran.md)  
  
-   [SQLExecDirect](../../relational-databases/extended-stored-procedures-reference/sqlexecdirect.md)  
  
-   [SQLExecute](../../relational-databases/extended-stored-procedures-reference/sqlexecute.md)  
  
-   [SQLFetch](../../relational-databases/extended-stored-procedures-reference/sqlfetch.md)  
  
-   [SQLFetchScroll](../../relational-databases/extended-stored-procedures-reference/sqlfetchscroll.md)  
  
-   [SQLForeignKeys](../../relational-databases/extended-stored-procedures-reference/sqlforeignkeys.md)  
  
-   [SQLFreeHandle](../../relational-databases/extended-stored-procedures-reference/sqlfreehandle.md)  
  
-   [SQLFreeStmt](../../relational-databases/extended-stored-procedures-reference/sqlfreestmt.md)  
  
-   [SQLGetConnectAttr](../../relational-databases/extended-stored-procedures-reference/sqlgetconnectattr.md)  
  
-   [SQLGetCursorName](../../relational-databases/extended-stored-procedures-reference/sqlgetcursorname.md)  
  
-   [SQLGetData](../../relational-databases/extended-stored-procedures-reference/sqlgetdata.md)  
  
-   [SQLGetDescField](../../relational-databases/extended-stored-procedures-reference/sqlgetdescfield.md)  
  
-   [SQLGetDiagField](../../relational-databases/extended-stored-procedures-reference/sqlgetdiagfield.md)  
  
-   [SQLGetFunctions](../../relational-databases/extended-stored-procedures-reference/sqlgetfunctions.md)  
  
-   [SQLGetInfo](../../relational-databases/extended-stored-procedures-reference/sqlgetinfo.md)  
  
-   [SQLGetStmtAttr](../../relational-databases/extended-stored-procedures-reference/sqlgetstmtattr.md)  
  
-   [SQLGetTypeInfo](../../relational-databases/extended-stored-procedures-reference/sqlgettypeinfo.md)  
  
-   [SQLMoreResults](../../relational-databases/extended-stored-procedures-reference/sqlmoreresults.md)  
  
-   [SQLNativeSql](../../relational-databases/extended-stored-procedures-reference/sqlnativesql.md)  
  
-   [SQLNumParams](../../relational-databases/extended-stored-procedures-reference/sqlnumparams.md)  
  
-   [SQLNumResultCols](../../relational-databases/extended-stored-procedures-reference/sqlnumresultcols.md)  
  
-   [SQLParamData](../../relational-databases/extended-stored-procedures-reference/sqlparamdata.md)  
  
-   [SQLPrimaryKeys](../../relational-databases/extended-stored-procedures-reference/sqlprimarykeys.md)  
  
-   [SQLProcedureColumns](../../relational-databases/extended-stored-procedures-reference/sqlprocedurecolumns.md)  
  
-   [SQLProcedures](../../relational-databases/extended-stored-procedures-reference/sqlprocedures.md)  
  
-   [SQLPutData](../../relational-databases/extended-stored-procedures-reference/sqlputdata.md)  
  
-   [SQLRowCount](../../relational-databases/extended-stored-procedures-reference/sqlrowcount.md)  
  
-   [SQLSetConnectAttr](../../relational-databases/extended-stored-procedures-reference/sqlsetconnectattr.md)  
  
-   [SQLSetDescField](../../relational-databases/extended-stored-procedures-reference/sqlsetdescfield.md)  
  
-   [SQLSetDescRec](../../relational-databases/extended-stored-procedures-reference/sqlsetdescrec.md)  
  
-   [SQLSetEnvAttr](../../relational-databases/extended-stored-procedures-reference/sqlsetenvattr.md)  
  
-   [SQLSetStmtAttr](../../relational-databases/extended-stored-procedures-reference/sqlsetstmtattr.md)  
  
-   [SQLSpecialColumns](../../relational-databases/extended-stored-procedures-reference/sqlspecialcolumns.md)  
  
-   [SQLStatistics](../../relational-databases/extended-stored-procedures-reference/sqlstatistics.md)  
  
-   [SQLTablePrivileges](../../relational-databases/extended-stored-procedures-reference/sqltableprivileges.md)  
  
-   [SQLTables](../../relational-databases/extended-stored-procedures-reference/sqltables.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41; Reference](http://msdn.microsoft.com/library/06b7edee-8636-49d9-9b5c-2c710bf4fa2d)   
 [Building Applications with SQL Server Native Client](../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
  