---
description: "SQLDisconnect Function"
title: "SQLDisconnect Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLDisconnect"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDisconnect"
helpviewer_keywords: 
  - "SQLDisconnect function [ODBC]"
ms.assetid: 9e84a58e-db48-4821-a0cd-5c711fcbe36b
author: David-Engel
ms.author: v-davidengel
---
# SQLDisconnect Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLDisconnect** closes the connection associated with a specific connection handle.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLDisconnect(  
     SQLHDBC     ConnectionHandle);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLDisconnect** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLDisconnect** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01002|Disconnect error|An error occurred during the disconnect. However, the disconnect succeeded. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|(DM) The connection specified in the argument *ConnectionHandle* was not open.|  
|25000|Invalid transaction state|There was a transaction in process on the connection specified by the argument *ConnectionHandle*. The transaction remains active.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *ConnectionHandle*. The function was called, and before it finshed executing [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) was called on the *ConnectionHandle*. Then the function was called again on the *ConnectionHandle*.<br /><br /> The function was called, and before it finished executing **SQLCancelHandle** was called on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for a *StatementHandle* associated with the *ConnectionHandle* and was still executing when **SQLDisconnect** was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *ConnectionHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for a *StatementHandle* associated with the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request, and the connection is still active. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *ConnectionHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 If an application calls **SQLDisconnect** after **SQLBrowseConnect** returns SQL_NEED_DATA and before it returns a different return code, the driver cancels the connection browsing process and returns the connection to an unconnected state.  
  
 If an application calls **SQLDisconnect** while there is an incomplete transaction associated with the connection handle, the driver returns SQLSTATE 25000 (Invalid transaction state), indicating that the transaction is unchanged and the connection is open. An incomplete transaction is one that has not been committed or rolled back with **SQLEndTran**.  
  
 If an application calls **SQLDisconnect** before it has freed all statements associated with the connection, the driver, after it successfully disconnects from the data source, frees those statements and all descriptors that have been explicitly allocated on the connection. However, if one or more of the statements associated with the connection are still executing asynchronously, **SQLDisconnect** returns SQL_ERROR with a SQLSTATE value of HY010 (Function sequence error). Also, **SQLDisconnect** will free all associated statements and all descriptors that have been explicitly allocated on the connection, if the connection is in a suspended state or if **SQLDisconnect** was successfully canceled by **SQLCancelHandle**.  
  
 For information about how an application uses **SQLDisconnect**, see [Disconnecting from a Data Source or Driver](../../../odbc/reference/develop-app/disconnecting-from-a-data-source-or-driver.md).  
  
## Disconnecting from a Pooled Connection  
 If connection pooling is enabled for a shared environment and an application calls **SQLDisconnect** on a connection in that environment, the connection is returned to the connection pool and is still available to other components using the same shared environment.  
  
## Code Example  
 See [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md), [SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md), and [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Connecting to a data source|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
|Executing a commit or rollback operation|[SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md)|  
|Freeing a connection handle|[SQLFreeConnect Function](../../../odbc/reference/syntax/sqlfreeconnect-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
