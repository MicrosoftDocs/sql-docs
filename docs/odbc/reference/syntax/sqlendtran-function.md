---
title: "SQLEndTran Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLEndTran"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLEndTran"
helpviewer_keywords: 
  - "SQLEndTran function [ODBC]"
ms.assetid: ff375ce1-eb50-4693-b1e6-70181a6dbf9f
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLEndTran Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLEndTran** requests a commit or rollback operation for all active operations on all statements associated with a connection. **SQLEndTran** can also request that a commit or rollback operation be performed for all connections associated with an environment.  
  
> [!NOTE]  
>  For more information about what the Driver Manager maps this function to when an ODBC 3.*x* application is working with an ODBC 2.*x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```  
  
SQLRETURN SQLEndTran(  
     SQLSMALLINT   HandleType,  
     SQLHANDLE     Handle,  
     SQLSMALLINT   CompletionType);  
```  
  
## Arguments  
 *HandleType*  
 [Input] Handle type identifier. Contains either SQL_HANDLE_ENV (if *Handle* is an environment handle) or SQL_HANDLE_DBC (if *Handle* is a connection handle).  
  
 *Handle*  
 [Input] The handle, of the type indicated by *HandleType*, indicating the scope of the transaction. See "Comments" for more information.  
  
 *CompletionType*  
 [Input] One of the following two values:  
  
 SQL_COMMIT SQL_ROLLBACK  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLEndTran** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with the appropriate *HandleType* and *Handle*. The following table lists the SQLSTATE values commonly returned by **SQLEndTran** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|(DM) The *HandleType* was SQL_HANDLE_DBC, and the *Handle* was not in a connected state.|  
|08007|Connection failure during transaction|The *HandleType* was SQL_HANDLE_DBC, and the connection associated with the *Handle* failed during the execution of the function, and it cannot be determined whether the requested **COMMIT** or **ROLLBACK** occurred before the failure.|  
|25S01|Transaction state unknown|One or more of the connections in *Handle* failed to complete the transaction with the outcome specified, and the outcome is unknown.|  
|25S02|Transaction is still active|The driver was not able to guarantee that all work in the global transaction could be completed atomically, and the transaction is still active.|  
|25S03|Transaction is rolled back|The driver was not able to guarantee that all work in the global transaction could be completed atomically, and all work in the transaction active in *Handle* was rolled back.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40002|Integrity constraint violation|The *CompletionType* was SQL_COMMIT, and the commitment of changes caused integrity constraint violation. As a result, the transaction was rolled back.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*szMessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *ConnectionHandle*. The function was called, and before it finished executing [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) was called on the *ConnectionHandle*. Then the function was called again on the *ConnectionHandle*.<br /><br /> The function was called, and before it finished executing **SQLCancelHandle** was called on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for a statement handle associated with the *ConnectionHandle* and was still executing when **SQLEndTran** was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *ConnectionHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for a statement handle associated with the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *Handle* with *HandleType* set to SQL_HANDLE_DBC and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with *Handle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY012|Invalid transaction operation code|(DM) The value specified for the argument *CompletionType* was neither SQL_COMMIT nor SQL_ROLLBACK.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY092|Invalid attribute/option identifier|(DM) The value specified for the argument *HandleType* was neither SQL_HANDLE_ENV nor SQL_HANDLE_DBC.|  
|HY115|SQLEndTran is not allowed for an environment that contains a connection with asynchronous function execution enabled|(DM) *HandleType* cannot be set to SQL_HANDLE_ENV if asynchronous execution of connection functions is enabled for a connection in the environment.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, refer to the Comments section of this topic.|  
|HYC00|Optional feature not implemented|The driver or data source does not support the **ROLLBACK** operation.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *ConnectionHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 For an ODBC 3.*x* driver, if *HandleType* is SQL_HANDLE_ENV and *Handle* is a valid environment handle, then the Driver Manager will call **SQLEndTran** in each driver associated with the environment. The *Handle* argument for the call to a driver will be the driver's environment handle. For an ODBC 2.*x* driver, if *HandleType* is SQL_HANDLE_ENV and *Handle* is a valid environment handle, and there are multiple connections in a connected state in that environment, then the Driver Manager will call **SQLTransact** in the driver once for each connection in a connected state in that environment. The *Handle* argument in each call will be the connection's handle. In either case, the driver will attempt to commit or roll back transactions, depending on the value of *CompletionType*, on all connections that are in a connected state on that environment. Connections that are not active do not affect the transaction.  
  
> [!NOTE]  
>  **SQLEndTran** cannot be used to commit or roll back transactions on a shared environment. SQLSTATE HY092 (Invalid attribute/option identifier) will be returned if **SQLEndTran** is called with *Handle* set to either the handle of a shared environment or the handle of a connection on a shared environment.  
  
 The Driver Manager will return SQL_SUCCESS only if it receives SQL_SUCCESS for each connection. If the Driver Manager receives SQL_ERROR on one or more connections, it returns SQL_ERROR to the application, and the diagnostic information is placed in the diagnostic data structure of the environment. To determine which connection or connections failed during the commit or rollback operation, the application can call **SQLGetDiagRec** for each connection.  
  
> [!NOTE]  
>  The Driver Manager does not simulate a global transaction across all connections and therefore does not use two-phase commit protocols.  
  
 If *CompletionType* is SQL_COMMIT, **SQLEndTran** issues a commit request for all active operations on any statement associated with an affected connection. If *CompletionType* is SQL_ROLLBACK, **SQLEndTran** issues a rollback request for all active operations on any statement associated with an affected connection. If no transactions are active, **SQLEndTran** returns SQL_SUCCESS with no effect on any data sources. For more information, see [Committing and Rolling Back Transactions](../../../odbc/reference/develop-app/committing-and-rolling-back-transactions.md).  
  
 If the driver is in manual-commit mode (by calling **SQLSetConnectAttr** with the SQL_ATTR_AUTOCOMMIT attribute set to SQL_AUTOCOMMIT_OFF), a new transaction is implicitly started when an SQL statement that can be contained within a transaction is executed against the current data source. For more information, see [Commit Mode](../../../odbc/reference/develop-app/commit-mode.md).  
  
 To determine how transaction operations affect cursors, an application calls **SQLGetInfo** with the SQL_CURSOR_ROLLBACK_BEHAVIOR and SQL_CURSOR_COMMIT_BEHAVIOR options. For more information, see the following paragraphs and also see [Effect of Transactions on Cursors and Prepared Statements](../../../odbc/reference/develop-app/effect-of-transactions-on-cursors-and-prepared-statements.md).  
  
 If the SQL_CURSOR_ROLLBACK_BEHAVIOR or SQL_CURSOR_COMMIT_BEHAVIOR value equals SQL_CB_DELETE, **SQLEndTran** closes and deletes all open cursors on all statements associated with the connection and discards all pending results. **SQLEndTran** leaves any statement present in an allocated (unprepared) state; the application can reuse them for subsequent SQL requests or can call **SQLFreeStmt** or **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_STMT to deallocate them.  
  
 If the SQL_CURSOR_ROLLBACK_BEHAVIOR or SQL_CURSOR_COMMIT_BEHAVIOR value equals SQL_CB_CLOSE, **SQLEndTran** closes all open cursors on all statements associated with the connection. **SQLEndTran** leaves any statement present in a prepared state; the application can call **SQLExecute** for a statement associated with the connection without first calling **SQLPrepare**.  
  
 If the SQL_CURSOR_ROLLBACK_BEHAVIOR or SQL_CURSOR_COMMIT_BEHAVIOR value equals SQL_CB_PRESERVE, **SQLEndTran** does not affect open cursors associated with the connection. Cursors remain at the row they pointed to prior to the call to **SQLEndTran**.  
  
 For drivers and data sources that support transactions, calling **SQLEndTran** with either SQL_COMMIT or SQL_ROLLBACK when no transaction is active returns SQL_SUCCESS (indicating that there is no work to be committed or rolled back) and has no effect on the data source.  
  
 When a driver is in autocommit mode, the Driver Manager does not call **SQLEndTran** in the driver. **SQLEndTran** always returns SQL_SUCCESS regardless of whether it is called with a *CompletionType* of SQL_COMMIT or SQL_ROLLBACK.  
  
 Drivers or data sources that do not support transactions (**SQLGetInfo** *option* SQL_TXN_CAPABLE is SQL_TC_NONE) are effectively always in autocommit mode and therefore always return SQL_SUCCESS for **SQLEndTran** whether or not they are called with a *CompletionType* of SQL_COMMIT or SQL_ROLLBACK. Such drivers and data sources do not actually roll back transactions when requested to do so.  
  
## Suspended State  
 In Driver Managers that were released before Windows 7, a transaction was active if **SQLEndTran** returned SQL_ERROR from the driver. However, it was possible that the transaction had been successfully committed on the server, but the driver on the client had not been notified (for example, because a network error occurred). This would leave the connection in a bad state. Starting with Windows 7, when **SQLEndTran** returns SQL_ERROR, the connection might be in a suspended state. In a suspended state, it is possible to call read-only functions. Eventually, the application should call **SQLDisconnect** on a suspended connection to release resources.  
  
 If all of the following conditions are true, the connection will be put into a suspended state:  
  
-   The driver returns SQL_ERROR from **SQLEndTran**.  
  
-   The driver is ODBC version 3.8, or later.  
  
-   The application version is 3.8 or later; or the recompiled ODBC 2.x or 3.x application successfully cancels the **SQLEndTran** function through **SQLCancelHandle**.  
  
-   The driver did not return one of the following messages, which confirm that the transaction did not complete:  
  
    -   25S03: Transaction is rolled back  
  
    -   40001: Serialization failure  
  
    -   40002: Integrity constraint  
  
    -   HYC00: Optional feature not implemented  
  
 If **SQLEndTran** was called on an environment handle and one of its connections met the above conditions, all connections connecting to the same driver will be put into the suspended state.  
  
 After an application calls **SQLDisconnect** on a suspended connection, the connection can be used to reconnect to another data source or the same data source.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Canceling a function running asynchronously on a connection handle.|[SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md)|  
|Returning information about a driver or data source|[SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md)|  
|Freeing a handle|[SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)|  
|Freeing a statement handle|[SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md)
