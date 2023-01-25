---
description: "ODBC API Implementation Details"
title: "ODBC API Implementation Details | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, functions"
  - "SQL Server Native Client ODBC driver, SQL Server-specific behaviors"
  - "ODBC, SQL Server-specific behaviors"
  - "functions [ODBC]"
ms.assetid: dca92489-f179-4b1f-997c-adcc46aa17a3
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ODBC API Implementation Details
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Open Database Connectivity (ODBC) is a Microsoft Win32 application programming interface used by applications to access data in ODBC data sources.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reference does not document all of the ODBC function calls. Only those functions that have driver-specific parameters or behaviors when used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver are discussed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with the ODBC 3.51 specification. For a comprehensive reference of ODBC 3.51, download the Microsoft Data Access Components SDK from the [Data Access and Storage Developer Center](https://go.microsoft.com/fwlink?linkid=4173), or view the [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md) online.  
  
## In This Section  
  
-   [SQLBindCol](../../relational-databases/native-client-odbc-api/sqlbindcol.md)  
  
-   [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md)  
  
-   [SQLBrowseConnect](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md)  
  
-   [SQLCancel](../../relational-databases/native-client-odbc-api/sqlcancel.md)  
  
-   [SQLCloseCursor](../../relational-databases/native-client-odbc-api/sqlclosecursor.md)  
  
-   [SQLColAttribute](../../relational-databases/native-client-odbc-api/sqlcolattribute.md)  
  
-   [SQLColumnPrivileges](../../relational-databases/native-client-odbc-api/sqlcolumnprivileges.md)  
  
-   [SQLColumns](../../relational-databases/native-client-odbc-api/sqlcolumns.md)  
  
-   [SQLConfigDataSource](../../relational-databases/native-client-odbc-api/sqlconfigdatasource.md)  
  
-   [SQLConnect](../../relational-databases/native-client-odbc-api/sqlconnect.md)  
  
-   [SQLDescribeCol](../../relational-databases/native-client-odbc-api/sqldescribecol.md)  
  
-   [SQLDescribeParam](../../relational-databases/native-client-odbc-api/sqldescribeparam.md)  
  
-   [SQLDriverConnect](../../relational-databases/native-client-odbc-api/sqldriverconnect.md)  
  
-   [SQLDrivers](../../relational-databases/native-client-odbc-api/sqldrivers.md)  
  
-   [SQLEndTran](../../relational-databases/native-client-odbc-api/sqlendtran.md)  
  
-   [SQLExecDirect](../../relational-databases/native-client-odbc-api/sqlexecdirect.md)  
  
-   [SQLExecute](../../relational-databases/native-client-odbc-api/sqlexecute.md)  
  
-   [SQLFetch](../../relational-databases/native-client-odbc-api/sqlfetch.md)  
  
-   [SQLFetchScroll](../../relational-databases/native-client-odbc-api/sqlfetchscroll.md)  
  
-   [SQLForeignKeys](../../relational-databases/native-client-odbc-api/sqlforeignkeys.md)  
  
-   [SQLFreeHandle](../../relational-databases/native-client-odbc-api/sqlfreehandle.md)  
  
-   [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md)  
  
-   [SQLGetConnectAttr](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md)  
  
-   [SQLGetCursorName](../../relational-databases/native-client-odbc-api/sqlgetcursorname.md)  
  
-   [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md)  
  
-   [SQLGetDescField](../../relational-databases/native-client-odbc-api/sqlgetdescfield.md)  
  
-   [SQLGetDiagField](../../relational-databases/native-client-odbc-api/sqlgetdiagfield.md)  
  
-   [SQLGetFunctions](../../relational-databases/native-client-odbc-api/sqlgetfunctions.md)  
  
-   [SQLGetInfo](../../relational-databases/native-client-odbc-api/sqlgetinfo.md)  
  
-   [SQLGetStmtAttr](../../relational-databases/native-client-odbc-api/sqlgetstmtattr.md)  
  
-   [SQLGetTypeInfo](../../relational-databases/native-client-odbc-api/sqlgettypeinfo.md)  
  
-   [SQLMoreResults](../../relational-databases/native-client-odbc-api/sqlmoreresults.md)  
  
-   [SQLNativeSql](../../relational-databases/native-client-odbc-api/sqlnativesql.md)  
  
-   [SQLNumParams](../../relational-databases/native-client-odbc-api/sqlnumparams.md)  
  
-   [SQLNumResultCols](../../relational-databases/native-client-odbc-api/sqlnumresultcols.md)  
  
-   [SQLParamData](../../relational-databases/native-client-odbc-api/sqlparamdata.md)  
  
-   [SQLPrimaryKeys](../../relational-databases/native-client-odbc-api/sqlprimarykeys.md)  
  
-   [SQLProcedureColumns](../../relational-databases/native-client-odbc-api/sqlprocedurecolumns.md)  
  
-   [SQLProcedures](../../relational-databases/native-client-odbc-api/sqlprocedures.md)  
  
-   [SQLPutData](../../relational-databases/native-client-odbc-api/sqlputdata.md)  
  
-   [SQLRowCount](../../relational-databases/native-client-odbc-api/sqlrowcount.md)  
  
-   [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md)  
  
-   [SQLSetDescField](../../relational-databases/native-client-odbc-api/sqlsetdescfield.md)  
  
-   [SQLSetDescRec](../../relational-databases/native-client-odbc-api/sqlsetdescrec.md)  
  
-   [SQLSetEnvAttr](../../relational-databases/native-client-odbc-api/sqlsetenvattr.md)  
  
-   [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md)  
  
-   [SQLSpecialColumns](../../relational-databases/native-client-odbc-api/sqlspecialcolumns.md)  
  
-   [SQLStatistics](../../relational-databases/native-client-odbc-api/sqlstatistics.md)  
  
-   [SQLTablePrivileges](../../relational-databases/native-client-odbc-api/sqltableprivileges.md)  
  
-   [SQLTables](../../relational-databases/native-client-odbc-api/sqltables.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41; Reference](../native-client/odbc/sql-server-native-client-odbc.md)   
 [Building Applications with SQL Server Native Client](../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
