---
description: "SQLCancelHandle Function"
title: "SQLCancelHandle Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
f1_keywords: 
  - "SQLCancelHandle"
helpviewer_keywords: 
  - "SQLCancelHandle function [ODBC]"
ms.assetid: 16049b5b-22a7-4640-9897-c25dd0f19d21
author: David-Engel
ms.author: v-davidengel
---
# SQLCancelHandle Function
**Conformance**  
 Version Introduced: ODBC 3.8 Standards Compliance: None  
  
 It is expected that most ODBC 3.8 (and later) drivers will implement this function. If a driver does not, a call to **SQLCancelHandle** with a connection handle in the *Handle* parameter will return SQL_ERROR with a SQLSTATE of IM001 and message 'Driver does not support this function'' A call to **SQLCancelHandle** with a statement handle as the *Handle* parameter will be mapped to a call to **SQLCancel** by the Driver Manager and can be processed if the driver implements **SQLCancel**. An application can use **SQLGetFunctions** to determine if a driver supports **SQLCancelHandle**.  
  
 **Summary**  
 **SQLCancelHandle** cancels the processing on a connection or statement. The Driver Manager maps a call to **SQLCancelHandle** to a call to **SQLCancel** when *HandleType* is SQL_HANDLE_STMT.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLCancelHandle(  
      SQLSMALLINT  HandleType,  
      SQLHANDLE    Handle);  
```  
  
## Arguments  
 *HandleType*  
 [Input] The type of the handle on which to cacel processing. Valid values are SQL_HANDLE_DBC or SQL_HANDLE_STMT.  
  
 *Handle*  
 [Input] The handle on which to cancel processing.  
  
 If *Handle* is not a valid handle of the type specified by *HandleType*, **SQLCancelHandle** returns SQL_INVALID_HANDLE.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLCancelHandle** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a statement handle *Handle* or a *HandleType* of SQL_HANDLE_DBC and a connection handle *Handle*.  
  
 The following table lists the SQLSTATE values commonly returned by **SQLCancelHandle** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by [SQLGetDiagRec](../../../odbc/reference/syntax/sqlgetdiagrec-function.md) in the argument *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|An asynchronously executing, statement-related function was called for one of the statement handles associated with the *Handle*, and *HandleType* was set to SQL_HANDLE_DBC. The asynchronous function was still executing when **SQLCancelHandle** was called.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_STMT; an asynchronously executing function was called on the associated connection handle; and the function was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *Handle* and *HandleType* was set to SQL_HANDLE_DBC, and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> **SQLBrowseConnect** was called for *ConnectionHandle*, and returned SQL_NEED_DATA. This function was called before the browsing process completed.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY092|Invalid attribute/option identifier|*HandleType* was set to SQL_HANDLE_ENV or SQL_HANDLE_DESC.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *Handle* does not support the function.|  
  
 If **SQLCancelHandle** is called with *HandleType* set to SQL_HANDLE_STMT, it can return any SQLSTATE that can be returned by the function **SQLCancel**.  
  
## Comments  
 This function is similar to **SQLCancel** but may take either a connection or statement handle as a parameter rather than only a statement handle. The Driver Manager maps a call to **SQLCancelHandle** to a call to **SQLCancel** when *HandleType* is SQL_HANDLE_STMT. This allows applications to use **SQLCancelHandle** to cancel statement operations even if the driver does not implement **SQLCancelHandle**.  
  
 For more information about cancelling a statement operation, see [SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md).  
  
 If there are no operations in progress on *Handle* the call to **SQLCancelHandle** has no effect.  
  
 **SQLCancelHandle** on a connection handle can cancel the following types of processing:  
  
-   A function running asynchronously on the connection.  
  
-   A function running on the connection handle on another thread.  
  
 When **SQLCancelHandle** is called to cancel a function running asynchronously in a connection, diagnostic records posted by **SQLCancelHandle** are appended to those returned by the operation being canceled; **SQLCancelHandle** does not return diagnostic records, however, when canceling a function running on a connection on another thread.  
  
 Using **SQLCancelHandle** to cancel **SQLEndTran** may put the connection in suspended state. For more information on suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).  
  
> [!NOTE]  
>  For information about how to use **SQLCancelHandle** in an application that will be deployed on a Windows operating system older than Windows 7, see [Compatibility Matrix](../../../odbc/reference/develop-app/compatibility-matrix.md).  
  
#### Canceling Connection-Related Asynchronous Processing  
 If a function returns SQL_STILL_EXECUTING, an application can call **SQLCancelHandle** to cancel the operation. If the cancel request is successful, **SQLCancelHandle** returns SQL_SUCCESS. This does not mean that the original function was canceled; it indicates that the cancel request was processed. The driver and data source determine when or if the operation is canceled. The application must continue to call the original function until the return code is not SQL_STILL_EXECUTING. If the original function was canceled, the return code is SQL_ERROR and SQLSTATE HY008 (Operation canceled). If the original function completed its normal processing (was not cancelled), the return code is SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, or SQL_ERROR and a SQLSTATE other than HY008 (Operation canceled), if the original function failed.  
  
#### Canceling Functions Executing on Another Thread  
 In a multithread application, the application can cancel an operation that is running on another thread. To cancel the operation, the application calls **SQLCancelHandle** with the handle used by the function, but on a different thread. The driver and operating system determine how the operation is canceled. The **SQLCancelHandle** return code indicates whether the driver processed the request, returning either SQL_SUCCESS or SQL_ERROR (no diagnostic information is returned). If processing on the original function is canceled, the original function returns SQL_ERROR and SQLSTATE HY008 (Operation cancelled).  
  
 If a function is being executed when **SQLCancelHandle** is called on another thread to cancel the function, it is possible for the function to succeed and return SQL_SUCCESS before the cancel can take effect. A call to **SQLCancelHandle** has no effect if the operation completed before **SQLCancelHandle** was able to cancel the operation.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Canceling a function running asynchronously on a statement handle, canceling a function on a statement that needs data, or canceling a function running on a statement on another thread.|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md)
