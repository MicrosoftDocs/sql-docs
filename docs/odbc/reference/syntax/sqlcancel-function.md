---
description: "SQLCancel Function"
title: "SQLCancel Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLCancel"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLCancel"
helpviewer_keywords: 
  - "SQLCancel function [ODBC]"
ms.assetid: ac0b5972-627f-4440-8c5a-0e8da728726d
author: David-Engel
ms.author: v-davidengel
---
# SQLCancel Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLCancel** cancels the processing on a statement.  
  
 To cancel processing on a connection or statement, use [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLCancel(  
     SQLHSTMT     StatementHandle);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLCancel** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLCancel** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by [SQLGetDiagRec](../../../odbc/reference/syntax/sqlgetdiagrec-function.md) in the argument *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence  error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLCancel** function was called.<br /><br /> (DM) Cancel operation failed because an asynchronous operation is in progress on a connection handle that is associated with *StatementHandle*.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY018|Server declined cancel request|The server declined the cancel request.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 **SQLCancel** can cancel the following types of processing on a statement:  
  
-   A function running asynchronously on the statement.  
  
-   A function on a statement that needs data.  
  
-   A function running on the statement on another thread.  
  
 In ODBC 2.*x*, if an application calls **SQLCancel** when no processing is being done on the statement, **SQLCancel** has the same effect as **SQLFreeStmt** with the SQL_CLOSE option; this behavior is defined only for completeness and applications should call **SQLFreeStmt** or **SQLCloseCursor** to close cursors.  
  
 When **SQLCancel** is called to cancel a function running asynchronously in a statement or a function on a statement that needs data, diagnostic records posted by the function being canceled are cleared, and **SQLCancel** posts its own diagnostic records; when **SQLCancel** is called to cancel a function running on a statement on another thread, however, it does not clear the diagnostic records of the being canceled function and does not post its own diagnostic records.  
  
## Canceling Asynchronous Processing  
 After an application calls a function asynchronously, it calls the function repeatedly to determine whether it has finished processing. If the function is still processing, it returns SQL_STILL_EXECUTING. If the function has finished processing, it returns a different code.  
  
 After any call to the function that returns SQL_STILL_EXECUTING, an application can call **SQLCancel** to cancel the function. If the cancel request is successful, the driver returns SQL_SUCCESS. This message does not indicate that the function was actually canceled; it indicates that the cancel request was processed. When or if the function is actually canceled is driver-dependent and data source-dependent. The application must continue to call the original function until the return code is not SQL_STILL_EXECUTING. If the function was successfully canceled, the return code is SQL_ERROR and SQLSTATE HY008 (Operation canceled). If the function completed its normal processing, the return code is SQL_SUCCESS or SQL_SUCCESS_WITH_INFO if the function succeeded or SQL_ERROR and a SQLSTATE other than HY008 (Operation canceled) if the function failed.  
  
> [!NOTE]  
>  In ODBC 3.5, a call to **SQLCancel** when no processing is being done on the statement is not treated as **SQLFreeStmt** with the SQL_CLOSE option, but has is no effect at all. To close a cursor, an application should call **SQLCloseCursor**, not **SQLCancel**.  
  
 For more information about asynchronous processing, see [Asynchronous Execution](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md).  
  
## Canceling Functions that Need Data  
 After **SQLExecute** or **SQLExecDirect** returns SQL_NEED_DATA and before data has been sent for all data-at-execution parameters, an application can call **SQLCancel** to cancel the statement execution. After the statement has been canceled, the application can call **SQLExecute** or **SQLExecDirect** again. For more information, see [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md).  
  
 After **SQLBulkOperations** or **SQLSetPos** returns SQL_NEED_DATA and before data has been sent for all data-at-execution columns, an application can call **SQLCancel** to cancel the operation. After the operation has been canceled, the application can call **SQLBulkOperations** or **SQLSetPos** again; canceling does not affect the cursor state or the current cursor position. For more information, see [SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md) or [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
## Canceling Functions Executing on Another Thread  
 In a multithread application, the application can cancel a function that is running on another thread. To cancel the function, the application calls **SQLCancel** with the same statement handle as that used by the target function, but on a different thread. How the function is canceled depends on the driver and the operating system. As in canceling a function running asynchronously, the return code of the **SQLCancel** indicates only whether the driver processed the request successfully. Only SQL_SUCCESS or SQL_ERROR can be returned; no diagnostic information is returned. If the original function is canceled, it returns SQL_ERROR and SQLSTATE HY008 (Operation canceled).  
  
 If an SQL statement is being executed when **SQLCancel** is called on another thread to cancel the statement execution, it is possible for the execution to succeed and return SQL_SUCCESS while the cancel is also successful. In this case, the Driver Manager assumes that the cursor opened by the statement execution is closed by the cancel, so the application will not be able to use the cursor.  
  
 For more information about threading, see [Multithreading](../../../odbc/reference/develop-app/multithreading.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Performing bulk insert or update operations|[SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|  
|Cancels a function running asynchronously on a connection handle, in addition to the functionality of **SQLCancel**.|[SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Freeing a statement handle|[SQLFreeStmt](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
|Obtaining a field of a diagnostic record or a field of the diagnostic header|[SQLGetDiagField Function](../../../odbc/reference/syntax/sqlgetdiagfield-function.md)|  
|Obtaining multiple fields of a diagnostic data structure|[SQLGetDiagRec Function](../../../odbc/reference/syntax/sqlgetdiagrec-function.md)|  
|Returning the next parameter to send data for|[SQLParamData Function](../../../odbc/reference/syntax/sqlparamdata-function.md)|  
|Sending parameter data at execution time|[SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md)|  
|Positioning the cursor in a rowset, refreshing data in the rowset, or updating or deleting data in the result set|[SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
