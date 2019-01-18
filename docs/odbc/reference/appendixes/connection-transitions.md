---
title: "Connection Transitions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transitioning states [ODBC], connection"
  - "connection transitions [ODBC]"
  - "state transitions [ODBC], connection"
ms.assetid: 6b6e1a47-4a52-41c8-bb9e-7ddeae09913e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Transitions
ODBC connections have the following states.  
  
|State|Description|  
|-----------|-----------------|  
|C0|Unallocated environment, unallocated connection|  
|C1|Allocated environment, unallocated connection|  
|C2|Allocated environment, allocated connection|  
|C3|Connection function needs data|  
|C4|Connected connection|  
|C5|Connected connection, allocated statement|  
|C6|Connected connection, transaction in progress. It is possible for a connection to be in state C6 with no statements allocated on the connection. For example, suppose the connection is in manual commit mode and is in state C4. If a statement is allocated, executed (starting a transaction), and then freed, the transaction remains active but there are no statements on the connection.|  
  
 The following tables show how each ODBC function affects the connection state.  
  
## SQLAllocHandle  
  
|C0<br /><br /> No Env.|C1 Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|--------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|C1[1]|--[5]|--[5]|--[5]|--[5]|--[5]|--[5]|  
|(IH)[2]|C2|--[5]|--[5]|--[5]|--[5]|--[5]|  
|(IH)[3]|(IH)|(08003)|(08003)|C5|--[5]|--[5]|  
|(IH)[4]|(IH)|(08003)|(08003)|--[5]|--[5]|--[5]|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [4]   This row shows transitions when *HandleType* was SQL_HANDLE_DESC.  
  
 [5]   Calling **SQLAllocHandle** with *OutputHandlePtr* pointing to a valid handle overwrites that handle without regard for the previous contents ofthat handle, and might cause problems for ODBC drivers. It is incorrect ODBC application programming to call **SQLAllocHandle** twice with the same application variable defined for *\*OutputHandlePtr* without calling **SQLFreeHandle** to free the handle before reallocating it. Overwriting ODBC handles in such a manner can lead to inconsistent behavior or errors on the part of ODBC drivers.  
  
## SQLBrowseConnect  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|C3 [d] C4 [s]|-- [d] C2 [e] C4 [s]|(08002)|(08002)|(08002)|  
  
## SQLCloseCursor  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--|--[1] C5[2]|  
  
 [1]   The connection was in manual-commit mode.  
  
 [2]   The connection was in auto-commit mode.  
  
## SQLColumnPrivileges, SQLColumns, SQLForeignKeys, SQLGetTypeInfo, SQLPrimaryKeys, SQLProcedureColumns, SQLProcedures, SQLSpecialColumns, SQLStatistics, SQLTablePrivileges, and SQLTables  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--[1] C6[2]|--|  
  
 [1]   The connection was in auto-commit mode, or the data source did not begin a transaction.  
  
 [2]   The connection was in manual-commit mode, and the data source began a transaction.  
  
## SQLConnect  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|C4|(08002)|(08002)|(08002)|(08002)|  
  
## SQLCopyDesc, SQLGetDescField, SQLGetDescRec, SQLSetDescField, and SQLSetDescRec  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|--[1]|--|--|  
  
 [1]   In this state, the only descriptors available to the application are explicitly allocated descriptors.  
  
## SQLDataSources and SQLDrivers  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|--|--|--|--|--|--|  
  
## SQLDisconnect  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(08003)|C2|C2|C2|25000|  
  
## SQLDriverConnect  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|C4 s -- n[f]|(08002)|(08002)|(08002)|(08002)|  
  
## SQLEndTran  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)[1]|--[3]|--[3]|--[3]|--|--|--[4] or ([5], [6], and [8]) C4[5] and [7] C5[5], [6], and [9]|  
|(IH)[2]|(IH)|(08003)|(08003)|--|--|C5|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   Because the connection is not in a connected state, it is unaffected by the transaction.  
  
 [4]   The commit or rollback failed on the connection. The function returns SQL_ERROR in this case.  
  
 [5]   The commit or rollback succeeded on the connection. The function returns SQL_ERROR if the commit or rollback failed on another connection, or the function returns SQL_SUCCESS if the commit or rollback succeeded on all connections.  
  
 [6]   There was at least one statement allocated on the connection.  
  
 [7]   There were no statements allocated on the connection.  
  
 [8]   The connection had at least one statement for which there was an open cursor, and the data source preserves cursors when transactions are committed or rolled back, whichever applies (depending on whether *CompletionType* was SQL_COMMIT or SQL_ROLLBACK). For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR attributes in [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md).  
  
 [9]   If the connection had any statements for which there were open cursors, the cursors were not preserved when the transaction was committed or rolled back.  
  
## SQLExecDirect and SQLExecute  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--[1] C6[2] C6[3]|--|  
  
 [1]   The connection was in auto-commit mode, and the statement executed was not a *cursor* *specification* (such as a SELECT statement); or the connection was in manual-commit mode, and the statement executed did not begin a transaction.  
  
 [2]   The connection was in auto-commit mode, and the statement executed was a *cursor* *specification* (such as a SELECT statement).  
  
 [3]   The connection was in manual-commit mode, and the data source began a transaction.  
  
## SQLFreeHandle  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)[1]|C0|(HY010)|(HY010)|(HY010)|(HY010)|(HY010)|  
|(IH)[2]|(IH)|(C1)|(HY010)|(HY010)|(HY010)|(HY010)|  
|(IH)[3]|(IH)|(IH)|(IH)|(IH)|C4[5] --[6]|--[7] C4[5] and [8] C5[6] and [8]|  
|(IH)[4]|(IH)|(IH)|(IH)|--|--|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [4]   This row shows transitions when *HandleType* was SQL_HANDLE_DESC.  
  
 [5]   There was only one statement allocated on the connection.  
  
 [6]   There were multiple statements allocated on the connection.  
  
 [7]   The connection was in manual-commit mode.  
  
 [8]   The connection was in auto-commit mode.  
  
## SQLFreeStmt  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)[1]|(IH)|(IH)|(IH)|(IH)|--|C5[3] --[4]|  
|(IH)[2]|(IH)|(IH)|(IH)|(IH)|--|--|  
  
 [1]   This row shows transactions when the *Option* argument is SQL_CLOSE.  
  
 [2]   This row shows transactions when the *Option* argument is SQL_UNBIND or SQL_RESET_PARAMS.  
  
 [3]   The connection was in auto-commit mode, and no cursors were open on any statements except this one.  
  
 [4]   The connection was in manual-commit mode, or it was in auto-commit mode and a cursor was open on at least one other statement.  
  
## SQLGetConnectAttr  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|IH|IH|--[1] 08003[2]|HY010|--|--|--|  
  
 [1]   The *Attribute* argument was SQL_ATTR_ACCESS_MODE, SQL_ATTR_AUTOCOMMIT, SQL_ATTR_LOGIN_TIMEOUT, SQL_ATTR_ODBC_CURSORS, SQL_ATTR_TRACE, or SQL_ATTR_TRACEFILE, or a value had been set for the connection attribute.  
  
 [2]   The *Attribute* argument was not SQL_ATTR_ACCESS_MODE, SQL_ATTR_AUTOCOMMIT, SQL_ATTR_LOGIN_TIMEOUT, SQL_ATTR_ODBC_CURSORS, SQL_ATTR_TRACE, or SQL_ATTR_TRACEFILE, and a value had not been set for the connection attribute.  
  
## SQLGetDiagField and SQLGetDiagRec  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)[1]|--|--|--|--|--|--|  
|(IH)[2]|(IH)|--|--|--|--|--|  
|(IH)[3]|(IH)|(IH)|(IH)|(IH)|--|--|  
|(IH)[4]|(IH)|(IH)|(IH)|--|--|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [4]   This row shows transitions when *HandleType* was SQL_HANDLE_DESC.  
  
## SQLGetEnvAttr  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|IH|--|--|--|--|--|--|  
  
## SQLGetFunctions  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|IH|IH|HY010|HY010|--|--|--|  
  
## SQLGetInfo  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|IH|IH|--[1] 08003[2]|08003|--|--|--|  
  
 [1]   The *InfoType* argument was SQL_ODBC_VER.  
  
 [2]   The *InfoType* argument was not SQL_ODBC_VER.  
  
## SQLMoreResults  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--[1] C6[2]|--[3] C5[1]|  
  
 [1]   The connection was in auto-commit mode, and the call to **SQLMoreResults** has not initialized the processing of a result set of a cursor specification.  
  
 [2]   The connection was in auto-commit mode, and the call to **SQLMoreResults** has initialized the processing of a result set of a cursor specification.  
  
 [3]   The connection was in manual-commit mode.  
  
## SQLNativeSql  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(08003)|(08003)|--|--|--|  
  
## SQLPrepare  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--[1] C6[2]|--|  
  
 [1]   The connection was in auto-commit mode, or the data source did not begin a transaction.  
  
 [2]   The connection was in manual-commit mode, and the data source began a transaction.  
  
## SQLSetConnectAttr  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|IH|IH|--[1] 08003[2]|HY010|--[3] 08002[4] HY011[5]|--[3] 08002[4] HY011[5]|--[3] and [6] C5[8] 08002[4] HY011[5] or [7]|  
  
 [1]   The *Attribute* argument was not SQL_ATTR_TRANSLATE_LIB or SQL_ATTR_TRANSLATE_OPTION.  
  
 [2]   The *Attribute* argument was SQL_ATTR_TRANSLATE_LIB or SQL_ATTR_TRANSLATE_OPTION.  
  
 [3]   The *Attribute* argument was not SQL_ATTR_ODBC_CURSORS or SQL_ATTR_PACKET_SIZE.  
  
 [4]   The *Attribute* argument was SQL_ATTR_ODBC_CURSORS.  
  
 [5]   The *Attribute* argument was SQL_ATTR_PACKET_SIZE.  
  
 [6]   The *Attribute* argument was not SQL_ATTR_AUTOCOMMIT, or the *Attribute* argument was SQL_ATTR_AUTOCOMMIT and setting this attribute did not commit the transaction.  
  
 [7]   The *Attribute* argument was SQL_ATTR_TXN_ISOLATION.  
  
 [8]   The *Attribute* argument was SQL_ATTR_AUTOCOMMIT, and setting this attribute committed the transaction.  
  
## SQLSetEnvAttr  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|--|--|(HY010)|--|--|--|  
  
## All Other ODBC Functions  
  
|C0<br /><br /> No Env.|C1<br /><br /> Unallocated|C2<br /><br /> Allocated|C3<br /><br /> Need Data|C4<br /><br /> Connected|C5<br /><br /> Statement|C6<br /><br /> Transaction|  
|--------------------|------------------------|----------------------|----------------------|----------------------|----------------------|------------------------|  
|(IH)|(IH)|(IH)|(IH)|(IH)|--|--|
