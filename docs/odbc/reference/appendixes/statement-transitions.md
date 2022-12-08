---
description: "Statement Transitions"
title: "Statement Transitions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "transitioning states [ODBC], statement"
  - "state transitions [ODBC], statement"
  - "statement transitions [ODBC]"
ms.assetid: 3d70e0e3-fe83-4b4d-beac-42c82495a05b
author: David-Engel
ms.author: v-davidengel
---
# Statement Transitions
ODBC statements have the following states.  
  
|State|Description|  
|-----------|-----------------|  
|S0|Unallocated statement. (The connection state must be C4, C5, or C6. For more information, see [Connection Transitions](../../../odbc/reference/appendixes/connection-transitions.md).)|  
|S1|Allocated statement.|  
|S2|Prepared statement. No result set will be created.|  
|S3|Prepared statement. A (possibly empty) result set will be created.|  
|S4|Statement executed and no result set was created.|  
|S5|Statement executed and a (possibly empty) result set was created. The cursor is open and positioned before the first row of the result set.|  
|S6|Cursor positioned with **SQLFetch** or **SQLFetchScroll**.|  
|S7|Cursor positioned with **SQLExtendedFetch**.|  
|S8|Function needs data. **SQLParamData** has not been called.|  
|S9|Function needs data. **SQLPutData** has not been called.|  
|S10|Function needs data. **SQLPutData** has been called.|  
|S11|Still executing. A statement is left in this state after a function that is executed asynchronously returns SQL_STILL_EXECUTING. A statement is temporarily in this state while any function that accepts a statement handle is executing. Temporary residence in state S11 is not shown in any state tables except the state table for **SQLCancel**. While a statement is temporarily in state S11, the function can be canceled by calling **SQLCancel** from another thread.|  
|S12|Asynchronous execution canceled. In S12, an application must call the canceled function until it returns a value other than SQL_STILL_EXECUTING. The function was canceled successfully only if the function returns SQL_ERROR and SQLSTATE HY008 (Operation canceled). If it returns any other value, such as SQL_SUCCESS, the cancel operation failed and the function executed normally.|  
  
 States S2 and S3 are known as the prepared states, states S5 through S7 as the cursor states, states S8 through S10 as the need data states, and states S11 and S12 as the asynchronous states. In each of these groups, the transitions are shown separately only when they are different for each state in the group; in most cases, the transitions for each state in each a group are the same.  
  
 The following tables show how each ODBC function affects the statement state.  
  
## SQLAllocHandle  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--[1], [5], [6]|--[5]|--[5]|--[5]|--[5]|--[5]|--[5]|  
|--[2], [5]|--[5]|--[5]|--[5]|--[5]|--[5]|--[5]|  
|S1[3]|--[5]|--[5]|--[5]|--[5]|--[5]|--[5]|  
|--[4], [5]|--[5]|--[5]|--[5]|--[5]|--[5]|--[5]|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [4]   This row shows transitions when *HandleType* was SQL_HANDLE_DESC.  
  
 [5]   Calling **SQLAllocHandle** with *OutputHandlePtr* pointing to a valid handle overwrites that handle without regard for the previous contents to that handle and might cause problems for ODBC drivers. It is incorrect ODBC application programming to call **SQLAllocHandle** twice with the same application variable defined for *\*OutputHandlePtr* without calling **SQLFreeHandle** to free the handle before reallocating it. Overwriting ODBC handles in such a manner might lead to inconsistent behavior or errors on the part of ODBC drivers.  
  
## SQLBindCol  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--|--|--|HY010|HY010|  
  
## SQLBindParameter  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--|--|--|HY010|HY010|  
  
## SQLBrowseConnect, SQLConnect, and SQLDriverConnect  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|08002|08002|08002|08002|08002|08002|08002|  
  
## SQLBulkOperations  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|24000|See next table|HY010|NS [c] HY010 o|  
  
## SQLBulkOperations (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|-- [s] S8 [d] S11 [x]|-- [s] S8 [d] S11 [x]|HY010|  
  
## SQLCancel  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--|--|--|S1[1] S2 [nr] and [2] S3 [r]and [2] S5[3] and [5] S6([3] or [4]) and [6] S7[4] and [7]|See next table|  
  
 [1]   **SQLExecDirect** returned SQL_NEED_DATA.  
  
 [2]   **SQLExecute** returned SQL_NEED_DATA.  
  
 [3]   **SQLBulkOperations** returned SQL_NEED_DATA.  
  
 [4]   **SQLSetPos** returned SQL_NEED_DATA.  
  
 [5]   **SQLFetch**, **SQLFetchScroll**, or **SQLExtendedFetch** had not been called.  
  
 [6]   **SQLFetch** or **SQLFetchScroll** had been called.  
  
 [7]   **SQLExtendedFetch** had been called.  
  
## SQLCancel (Asynchronous States)  
  
|S11<br /><br /> Still executing|S12<br /><br /> Asynch canceled|  
|-----------------------------|-----------------------------|  
|NS[1] S12[2]|S12|  
  
 [1]   The statement was temporarily in state S11 while a function was executing. **SQLCancel** was called from a different thread.  
  
 [2]   The statement was in state S11 because a function called asynchronously returned SQL_STILL_EXECUTING.  
  
## SQLCloseCursor  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|24000|24000|24000|S1 [np] S3 [p]|HY010|HY010|  
  
## SQLColAttribute  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|See next table|24000|-- [s] S11 [x]|HY010|NS [c] HY010 o|  
  
## SQLColAttribute (Prepared States)  
  
|S2<br /><br /> No Results|S3<br /><br /> Results|  
|-----------------------|--------------------|  
|--[1] 07005[2]|-- [s] S11 x|  
  
 [1]   *FieldIdentifier* was SQL_DESC_COUNT.  
  
 [2]   *FieldIdentifier* was not SQL_DESC_COUNT.  
  
## SQLColumnPrivileges, SQLColumns, SQLForeignKeys, SQLGetTypeInfo, SQLPrimaryKeys, SQLProcedureColumns, SQLProcedures, SQLSpecialColumns, SQLStatistics, SQLTablePrivileges, and SQLTables  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|S5 [s]  S11 [x]|S1 [e] S5 [s]  S11 [x]|S1 [e] and [1] S5 [s] and [1] S11 [x] and [1] 24000[2]|See next table|HY010|NS [c] HY010 o|  
  
 [1]   The current result is the last or only result, or there are no current results. For more information about multiple results, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 [2]   The current result is not the last result.  
  
## SQLColumnPrivileges, SQLColumns, SQLForeignKeys, SQLGetTypeInfo, SQLPrimaryKeys, SQLProcedureColumns, SQLProcedures, SQLSpecialColumns, SQLStatistics, SQLTablePrivileges, and SQLTables (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000|24000[1]|24000|  
  
 [1]   This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.  
  
## SQLCopyDesc  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH[1]|--|--|--|--|HY010|NS [c] and [3] HY010 [o] or [4]|  
|IH[2]|HY010|See next table|24000|-- [s]  S11 x|HY010|NS [c] and [3] HY010 [o] or [4]|  
  
 [1]   This row shows transitions when the *SourceDescHandle* argument was an ARD, APD, or IPD.  
  
 [2]   This row shows transitions when the *SourceDescHandle* argument was an IRD.  
  
 [3]   The *SourceDescHandle* and *TargetDescHandle* arguments were the same as in the **SQLCopyDesc** function that is running asynchronously.  
  
 [4]   Either the *SourceDescHandle* argument or the *TargetDescHandle* argument (or both) were different than in the **SQLCopyDesc** function that is running asynchronously.  
  
## SQLCopyDesc (Prepared States)  
  
|S2<br /><br /> No Results|S3<br /><br /> Results|  
|-----------------------|--------------------|  
|24000[1]|-- [s]  S11 [x]|  
  
 [1]This row shows transitions when the *SourceDescHandle* argument was an IRD.  
  
## SQLDataSources and SQLDrivers  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLDescribeCol  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|See next table|24000|-- [s]  S11 [x]|HY010|NS [c] HY010 o|  
  
## SQLDescribeCol (Prepared States)  
  
|S2<br /><br /> No Results|S3<br /><br /> Results|  
|-----------------------|--------------------|  
|07005|-- [s]  S11 [x]|  
  
## SQLDescribeParam  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|-- [s]  S11 [x]|HY010|HY010|HY010|NS [c] HY010 [o]|  
  
## SQLDisconnect  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--[1]|S0[1]|S0[1]|S0[1]|S0[1]|(HY010)|(HY010)|  
  
 [1]   Calling **SQLDisconnect** frees all statements associated with the connection. Furthermore, this returns the connection state to C2; the connection state must be C4 before the statement state is S0.  
  
## SQLEndTran  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--[2] or [3] S1[1]|--[3]  S1 [np] and ([1] or [2]) S1 [p] and [1] S2 [p] and [2]|--[3]  S1 [np] and ([1] or [2]) S1 [p] and [1] S3 [p] and [2]|(HY010)|(HY010)|  
  
 [1]   The *CompletionType* argument is SQL_COMMIT and **SQLGetInfo** returns SQL_CB_DELETE for the SQL_CURSOR_COMMIT_BEHAVIOR information type, or the *CompletionType* argument is SQL_ROLLBACK and **SQLGetInfo** returns SQL_CB_DELETE for the SQL_CURSOR_ROLLBACK_BEHAVIOR information type.  
  
 [2]   The *CompletionType* argument is SQL_COMMIT and **SQLGetInfo** returns SQL_CB_CLOSE for the SQL_CURSOR_COMMIT_BEHAVIOR information type, or the *CompletionType* argument is SQL_ROLLBACK and **SQLGetInfo** returns SQL_CB_CLOSE for the SQL_CURSOR_ROLLBACK_BEHAVIOR information type.  
  
 [3]   The *CompletionType* argument is SQL_COMMIT and **SQLGetInfo** returns SQL_CB_PRESERVE for the SQL_CURSOR_COMMIT_BEHAVIOR information type, or the *CompletionType* argument is SQL_ROLLBACK and **SQLGetInfo** returns SQL_CB_PRESERVE for the SQL_CURSOR_ROLLBACK_BEHAVIOR information type.  
  
## SQLExecDirect  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|S4 [s] and [nr] S5 [s] and [r] S8 [d]  S11 [x]|-- [e] and [1] S1 [e] and [2] S4 [s] and [nr] S5 [s] and [r] S8 [d]  S11 [x]|-- [e], [1], and [3] S1 [e], [2], and [3] S4 [s], [nr], and [3] S5 [s], [r], and [3] S8 [d] and [3] S11 [x] and [3] 24000 [4]|See next table|HY010|NS [c] HY010 [o]|  
  
 [1]   The error was returned by the Driver Manager.  
  
 [2]   The error was not returned by the Driver Manager.  
  
 [3]   The current result is the last or only result, or there are no current results. For more information about multiple results, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 [4]   The current result is not the last result.  
  
## SQLExecDirect (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000|24000 [1]|24000|  
  
 [1]   This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.  
  
## SQLExecute  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|(HY010)|See next table|S2 [e], p, and [1]  S4 [s], [p], [nr], and [1] S5 [s], [p], [r], and [1] S8 [d], [p], and [1]  S11 [x], [p], and [1] 24000 [p] and [2] HY010 [np]|See cursor states table|HY010|NS [c] HY010 [o]|  
  
 [1]   The current result is the last or only result, or there are no current results. For more information about multiple results, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 [2]   The current result is not the last result.  
  
## SQLExecute (Prepared States)  
  
|S2<br /><br /> No Results|S3<br /><br /> Results|  
|-----------------------|--------------------|  
|S4 [s]  S8 [d]  S11 [x]|S5 [s]  S8 [d]  S11 [x]|  
  
## SQLExecute (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000 [p]  HY010 [np]|24000 [p], [1] HY010 [np]|24000 [p]  HY010 [np]|  
  
 [1]   This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.  
  
## SQLExtendedFetch  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|S1010|S1010|24000|See next table|S1010|NS [c] S1010 [o]|  
  
## SQLExtendedFetch (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|S7 [s] or [nf]  S11 [x]|S1010|-- [s] or [nf]  S11 [x]|  
  
## SQLFetch and SQLFetchScroll  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|24000|See next table|HY010|NS [c] HY010 [o]|  
  
## SQLFetch and SQLFetchScroll (Cursor states)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|S6 [s] or [nf]  S11 [x]|-- [s] or [nf]  S11 [x]|HY010|  
  
## SQLFreeHandle  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|-- [1]|HY010|HY010|HY010|HY010|HY010|HY010|  
|IH [2]|S0|S0|S0|S0|HY010|HY010|  
|-- [3]|--|--|--|--|--|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV or SQL_HANDLE_DBC.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_DESC and the descriptor was explicitly allocated.  
  
## SQLFreeStmt  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH [1]|--|--|S1 [np]  S2 [p]|S1 [np]  S3 [p]|HY010|HY010|  
|IH [2]|--|--|--|--|HY010|HY010|  
  
 [1]   This row shows transitions when *Option* was SQL_CLOSE.  
  
 [2]   This row shows transitions when *Option* was SQL_UNBIND or SQL_RESET_PARAMS. If the *Option* argument was SQL_DROP and the underlying driver is an ODBC 3*.x* driver, the Driver Manager maps this to a call to **SQLFreeHandle** with *HandleType* set to SQL_HANDLE_STMT. For more information, see the transition table for [SQLFreeHandle](../../../odbc/reference/syntax/sqlfreehandle-function.md).  
  
## SQLGetConnectAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLGetCursorName  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--|--|--|HY010|HY010|  
  
## SQLGetData  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|24000|See next table|HY010|NS [c] HY010 [o]|  
  
## SQLGetData (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000|-- [s] or [nf]  S11 [x]  24000 [b]  HY109 [i]|-- [s] or [nf]  S11 [x]  24000 [b]  HY109 [i]|  
  
## SQLGetDescField and SQLGetDescRec  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|-- [1] or [2] HY010 [3]|See next table|-- [1] or [2] 24000 [3]|-- [1], [2], or [3] S11 [3] and [x]|HY010|NS [c] or [4] HY010 [o] and [5]|  
  
 [1]   The *DescriptorHandle* argument was an APD or ARD.  
  
 [2]   The *DescriptorHandle* argument was an IPD.  
  
 [3]   The *DescriptorHandle* argument was an IRD.  
  
 [4]   The *DescriptorHandle* argument was the same as the *DescriptorHandle* argument in the **SQLGetDescField** or **SQLGetDescRec** function that is running asynchronously.  
  
 [5]   The *DescriptorHandle* argument was different than the *DescriptorHandle* argument in the **SQLGetDescField** or **SQLGetDescRec** function that is running asynchronously.  
  
## SQLGetDescField and SQLGetDescRec (Prepared States)  
  
|S2<br /><br /> No Results|S3<br /><br /> Results|  
|-----------------------|--------------------|  
|--[1], [2], or [3] S11[2] and [x]|--[1], [2], or [3] S11 [x]|  
  
 [1]   The *DescriptorHandle* argument was an APD or ARD.  
  
 [2]   The *DescriptorHandle* argument was an IPD.  
  
 [3]   The *DescriptorHandle* argument was an IRD. Note that these functions always return SQL_NO_DATA in state S2 when *DescriptorHandle* was an IRD.  
  
## SQLGetDiagField and SQLGetDiagRec  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--[1]|--|--|--|--|--|--|  
|IH[2]|--[3]|--[3]|--|--|--[3]|--[3]|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV, SQL_HANDLE_DBC, or SQL_HANDLE_DESC.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT.  
  
 [3]   **SQLGetDiagField** always returns an error in this state when *DiagIdentifier* is SQL_DIAG_ROW_COUNT.  
  
## SQLGetEnvAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLGetFunctions  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLGetInfo  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLGetStmtAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--[1] 24000[2]|--[1] 24000[2]|--[1] 24000[2]|See next table|HY010|HY010|  
  
 [1]   The statement attribute was not SQL_ATTR_ROW_NUMBER.  
  
 [2]   The statement attribute was SQL_ATTR_ROW_NUMBER.  
  
## SQLGetStmtAttr (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|--[1] 24000[2]|--[1] or ([v] and [2]) 24000 [b] and [2] HY109 [i] and [2]|-- [i] or ([v] and [2]) 24000 [b] and [2] HY109[1] and [2]|  
  
 [1]   The *Attribute* argument was not SQL_ATTR_ROW_NUMBER.  
  
 [2]   The *Attribute* argument was SQL_ATTR_ROW_NUMBER.  
  
## SQLMoreResults  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|--[1]|--[1]|-- [s] and [2] S1 [nf], [np], and [4] S2 [nf], [p], and [4] S5 [s] and [3] S11 [x]|S1 [nf], [np], and [4] S3 [nf], [p] and [4] S4 [s] and [2] S5 [s] and [3] S11 [x]|HY010|NS [c] HY010 [o]|  
  
 [1]   The function always returns SQL_NO_DATA in this state.  
  
 [2]   The next result is a row count.  
  
 [3]   The next result is a result set.  
  
 [4]   The current result is the last result.  
  
## SQLNativeSql  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--|--|--|--|--|--|--|  
  
## SQLNumParams  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|-- [s]  S11 [x]|-- [s]  S11 [x]|-- [s]  S11 [x]|HY010|NS [c] HY010 [o]|  
  
## SQLNumResultCols  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|-- [s]  S11 [x]|-- [s]  S11 [x]|-- [s]  S11 [x]|HY010|NS [c] HY010 [o]|  
  
## SQLParamData  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|HY010|HY010|See next table|NS [c] HY010 [o]|  
  
## SQLParamData (Need Data States)  
  
|S8<br /><br /> Need Data|S9<br /><br /> Must Put|S10<br /><br /> Can Put|  
|----------------------|---------------------|---------------------|  
|S1 [e] and [1] S2 [e], [nr], and [2] S3 [e], [r], and [2] S5 [e] and [4] S6 [e] and [5] S7 [e] and [3] S9 [d]  S11 [x]|HY010|S1 [e] and [1] S2 [e], [nr], and [2] S3 [e], [r], and [2] S4 [s], [nr], and ([1] or [2]) S5 [s], [r], and ([1] or [2]) S5 ([s] or [e]) and [4] S6 ([s] or [e]) and [5] S7 ([s] or [e]) and [3] S9 [d]  S11 [x]|  
  
 [1]   **SQLExecDirect** returned SQL_NEED_DATA.  
  
 [2]   **SQLExecute** returned SQL_NEED_DATA.  
  
 [3]   **SQLSetPos** had been called from state S7 and returned SQL_NEED_DATA.  
  
 [4]   **SQLBulkOperations** had been called from state S5 and returned SQL_NEED_DATA.  
  
 [5]   **SQLSetPos** or **SQLBulkOperations** had been called from state S6 and returned SQL_NEED_DATA.  
  
## SQLPrepare  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|S2 [s] and [nr] S3 [s] and [r] S11 [x]|-- [s] or ([e] and [1]) S1 [e] and [2] S11 [x]|S1 [e] and [3] S2 [s], [nr], and [3] S3 [s], [r], and [3] S11 [x] and [3] 24000[4]|See next table|HY010|NS [c] HY010 [o]|  
  
 [1]   The preparation fails for a reason other than validating the statement (the SQLSTATE was HY009 [Invalid argument value] or HY090 [Invalid string or buffer length]).  
  
 [2]   The preparation fails while validating the statement (the SQLSTATE was not HY009 [Invalid argument value] or HY090 [Invalid string or buffer length]).  
  
 [3]   The current result is the last or only result, or there are no current results. For more information about multiple results, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 [4]   The current result is not the last result.  
  
## SQLPrepare (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000|24000|24000|  
  
## SQLPutData  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|HY010|HY010|See next table|NS [c] HY010 [o]|  
  
## SQLPutData (Need Data States)  
  
|S8<br /><br /> Need Data|S9<br /><br /> Must Put|S10<br /><br /> Can Put|  
|----------------------|---------------------|---------------------|  
|HY010|S1 [e] and [1] S2 [e], [nr], and [2] S3 [e], [r], and [2] S5 [e] and [4] S6 [e] and [5] S7 [e] and [3] S10 [s]  S11 [x]|-- [s]  S1 [e] and [1] S2 [e], [nr], and [2] S3 [e], [r], and [2] S5 [e] and [4] S6 [e] and [5] S7 [e] and [3] S11 [x]  HY011[6]|  
  
 [1]   **SQLExecDirect** returned SQL_NEED_DATA.  
  
 [2]   **SQLExecute** returned SQL_NEED_DATA.  
  
 [3]   **SQLSetPos** had been called from state S7 and returned SQL_NEED_DATA.  
  
 [4]   **SQLBulkOperations** had been called from state S5 and returned SQL_NEED_DATA.  
  
 [5]   **SQLSetPos** or **SQLBulkOperations** had been called from state S6 and returned SQL_NEED_DATA.  
  
 [6]   One or more calls to **SQLPutData** for a single parameter returned SQL_SUCCESS, and then a call to **SQLPutData** was made for the same parameter with *StrLen_or_Ind* set to SQL_NULL_DATA.  
  
## SQLRowCount  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|(IH)|(HY010)|(HY010)|--|--|(HY010)|(HY010)|  
  
## SQLSetConnectAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|--[1]|--|--|--|--[2] 24000[3]|HY010|HY010|  
  
 [1]   This row shows transitions when *Attribute* was a connection attribute. For transitions when *Attribute* was a statement attribute, see the statement transition table for **SQLSetStmtAttr**.  
  
 [2]   The *Attribute* argument was not SQL_ATTR_CURRENT_CATALOG.  
  
 [3]   The *Attribute* argument was SQL_ATTR_CURRENT_CATALOG.  
  
## SQLSetCursorName  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--|24000|24000|HY010|HY010|  
  
## SQLSetDescField and SQLSetDescRec  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH[1]|--|--|--|--|HY010|HY010|  
  
 [1]   This row shows transitions where the *DescriptorHandle* argument is an ARD, APD, IPD, or (for **SQLSetDescField**) an IRD when the *FieldIdentifier* argument is SQL_DESC_ARRAY_STATUS_PTR or SQL_DESC_ROWS_PROCESSED_PTR. It is an error to call **SQLSetDescField** for an IRD when *FieldIdentifier* is any other value.  
  
## SQLSetEnvAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|HY011|HY011|HY011|HY011|Y011|HY01|HY011|  
  
## SQLSetPos  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|HY010|HY010|24000|See next table|HY010|NS [c] HY010 [o]|  
  
## SQLSetPos (Cursor States)  
  
|S5<br /><br /> Opened|S6<br /><br /> SQLFetch or SQLFetchScroll|S7<br /><br /> SQLExtendedFetch|  
|-------------------|---------------------------------------|-----------------------------|  
|24000|-- [s]  S8 [d]  S11 [x]  24000 [b]  HY109 [i]|-- [s]  S8 [d]  S11 [x]  24000 [b]  HY109 [i]|  
  
## SQLSetStmtAttr  
  
|S0<br /><br /> Unallocated|S1<br /><br /> Allocated|S2-S3<br /><br /> Prepared|S4<br /><br /> Executed|S5-S7<br /><br /> Cursor|S8-S10<br /><br /> Need Data|S11-S12<br /><br /> Async|  
|------------------------|----------------------|------------------------|---------------------|----------------------|--------------------------|-----------------------|  
|IH|--|--[1] HY011[2]|--[1] 24000[2]|--[1] 24000[2]|HY010 [np] or [1] HY011 [p] and [2]|HY010 [np] or [1] HY011 [p] and [2]|  
  
 [1]   The *Attribute* argument was not SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, SQL_ATTR_SIMULATE_CURSOR, SQL_ATTR_USE_BOOKMARKS, SQL_ATTR_CURSOR_SCROLLABLE, or SQL_ATTR_CURSOR_SENSITIVITY.  
  
 [2]   The *Attribute* argument was SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, SQL_ATTR_SIMULATE_CURSOR, SQL_ATTR_USE_BOOKMARKS, SQL_ATTR_CURSOR_SCROLLABLE, or SQL_ATTR_CURSOR_SENSITIVITY.
