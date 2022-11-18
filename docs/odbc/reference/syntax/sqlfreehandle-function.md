---
description: "SQLFreeHandle Function"
title: "SQLFreeHandle Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLFreeHandle"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLFreeHandle"
helpviewer_keywords: 
  - "SQLFreeHandle function [ODBC]"
ms.assetid: 17a6fcdc-b05a-4de7-be93-a316f39696a1
author: David-Engel
ms.author: v-davidengel
---
# SQLFreeHandle Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLFreeHandle** frees resources associated with a specific environment, connection, statement, or descriptor handle.  
  
> [!NOTE]
>  This function is a generic function for freeing handles. It replaces the ODBC 2.0 functions **SQLFreeConnect** (for freeing a connection handle) and **SQLFreeEnv** (for freeing an environment handle). **SQLFreeConnect** and **SQLFreeEnv** are both deprecated in ODBC 3*.x*. **SQLFreeHandle** also replaces the ODBC 2.0 function **SQLFreeStmt** (with the SQL_DROP *Option*) for freeing a statement handle. For more information, see "Comments." For more information about what the Driver Manager maps this function to when an ODBC 3*.x* application is working with an ODBC 2*.x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLFreeHandle(  
     SQLSMALLINT   HandleType,  
     SQLHANDLE     Handle);  
```  
  
## Arguments  
 *HandleType*  
 [Input] The type of handle to be freed by **SQLFreeHandle**. Must be one of the following values:  
  
-   SQL_HANDLE_DBC  
  
-   SQL_HANDLE_DBC_INFO_TOKEN  
  
-   SQL_HANDLE_DESC  
  
-   SQL_HANDLE_ENV  
  
-   SQL_HANDLE_STMT  
  
 SQL_HANDLE_DBC_INFO_TOKEN handle is used only by the Driver Manager and driver. Applications should not use this handle type. For more information about SQL_HANDLE_DBC_INFO_TOKEN, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).  
  
 If *HandleType* is not one of these values, **SQLFreeHandle** returns SQL_INVALID_HANDLE.  
  
 *Handle*  
 [Input] The handle to be freed.  
  
## Returns  
 SQL_SUCCESS, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
 If **SQLFreeHandle** returns SQL_ERROR, the handle is still valid.  
  
## Diagnostics  
 When **SQLFreeHandle** returns SQL_ERROR, an associated SQLSTATE value may be obtained from the diagnostic data structure for the handle that **SQLFreeHandle** tried to free but could not. The following table lists the SQLSTATE values typically returned by **SQLFreeHandle** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) The *HandleType* argument was SQL_HANDLE_ENV, and at least one connection was in an allocated or connected state. **SQLDisconnect** and **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC must be called for each connection before calling **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_ENV.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_DBC, and the function was called before calling **SQLDisconnect** for the connection.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_DBC. An asynchronously executing function was called with *Handle* and the function was still executing when this function was called.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_STMT. **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called with the statement handle and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_STMT. An asynchronously executing function was called on the statement handle or on the associated connection handle and the function was still executing when this function was called.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_DESC. An asynchronously executing function was called on the associated connection handle; and the function was still executing when this function was called.<br /><br /> (DM) All subsidiary handles and other resources were not released before **SQLFreeHandle** was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *Handle* and *HandleType* was set to SQL_HANDLE_STMT or SQL_HANDLE_DESC returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The *HandleType* argument was SQL_HANDLE_STMT or SQL_HANDLE_DESC, and the function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY017|Invalid use of an automatically allocated descriptor handle.|(DM) The *Handle* argument was set to the handle for an automatically allocated descriptor.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The *HandleType* argument was SQL_HANDLE_DESC, and the driver was an ODBC 2*.x* driver.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_STMT, and the driver was not a valid ODBC driver.|  
  
## Comments  
 **SQLFreeHandle** is used to free handles for environments, connections, statements, and descriptors, as described in the following sections. For general information about handles, see [Handles](../../../odbc/reference/develop-app/handles.md).  
  
 An application should not use a handle after it has been freed; the Driver Manager does not check the validity of a handle in a function call.  
  
## Freeing an Environment Handle  
 Before it calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_ENV, an application must call **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC for all connections allocated under the environment. Otherwise, the call to **SQLFreeHandle** returns SQL_ERROR and the environment and any active connection remains valid. For more information, see [Environment Handles](../../../odbc/reference/develop-app/environment-handles.md) and [Allocating the Environment Handle](../../../odbc/reference/develop-app/allocating-the-environment-handle.md).  
  
 If the environment is a shared environment, the application that calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_ENV no longer has access to the environment after the call, but the environment's resources are not necessarily freed. The call to **SQLFreeHandle** decrements the reference count of the environment. The reference count is maintained by the Driver Manager. If it does not reach zero, the shared environment is not freed, because it is still being used by another component. If the reference count reaches zero, the resources of the shared environment are freed.  
  
## Freeing a Connection Handle  
 Before it calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC, an application must call **SQLDisconnect** for the connection if there is a connection on this handle*.* Otherwise, the call to **SQLFreeHandle** returns SQL_ERROR and the connection remains valid.  
  
 For more information, see [Connection Handles](../../../odbc/reference/develop-app/connection-handles.md) and [Disconnecting from a Data Source or Driver](../../../odbc/reference/develop-app/disconnecting-from-a-data-source-or-driver.md).  
  
## Freeing a Statement Handle  
 A call to **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_STMT frees all resources that were allocated by a call to **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_STMT. When an application calls **SQLFreeHandle** to free a statement that has pending results, the pending results are deleted. When an application frees a statement handle, the driver frees the four automatically allocated descriptors associated with that handle. For more information, see [Statement Handles](../../../odbc/reference/develop-app/statement-handles.md) and [Freeing a Statement Handle](../../../odbc/reference/develop-app/freeing-a-statement-handle-odbc.md).  
  
 Notice that **SQLDisconnect** automatically drops any statements and descriptors open on the connection.  
  
## Freeing a Descriptor Handle  
 A call to **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DESC frees the descriptor handle in *Handle*. The call to **SQLFreeHandle** does not release any memory allocated by the application that may be referenced by a pointer field (including SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR) of any descriptor record of *Handle*. The memory allocated by the driver for fields that are not pointer fields is freed when the handle is freed. When a user-allocated descriptor handle is freed, all statements that the freed handle had been associated with revert to their respective automatically allocated descriptor handles.  
  
> [!NOTE]
>  ODBC 2*.x* drivers do not support freeing descriptor handles, just as they do not support allocating descriptor handles.  
  
 Notice that **SQLDisconnect** automatically drops any statements and descriptors open on the connection. When an application frees a statement handle, the driver frees all the automatically generated descriptors associated with that handle.  
  
 For more information about descriptors, see [Descriptors](../../../odbc/reference/develop-app/descriptors.md).  
  
## Code Example  
 For additional code samples, see [SQLBrowseConnect](../../../odbc/reference/syntax/sqlbrowseconnect-function.md) and [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
### Code  
  
```cpp  
// SQLFreeHandle.cpp  
// compile with: user32.lib odbc32.lib  
#include <windows.h>  
#include <sqlext.h>  
#include <stdio.h>  
  
int main() {  
   SQLRETURN retCode;  
   HWND desktopHandle = GetDesktopWindow();   // desktop's window handle  
   SQLCHAR connStrbuffer[1024];  
   SQLSMALLINT connStrBufferLen;  
  
   // Initialize the environment, connection, statement handles.  
   SQLHENV henv = NULL;   // Environment     
   SQLHDBC hdbc = NULL;   // Connection handle  
   SQLHSTMT hstmt = NULL;   // Statement handle  
  
   // Allocate the environment.  
   retCode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set environment attributes.  
   retCode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (void*)SQL_OV_ODBC3, -1);  
  
   // Allocate the connection.  
   retCode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
  
   // Set the login timeout.  
   retCode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)10, 0);  
  
   // Let the user select the data source and connect to the database.  
   retCode = SQLDriverConnect(hdbc, desktopHandle, (SQLCHAR *)"Driver={SQL Server}", SQL_NTS, connStrbuffer, 1025, &connStrBufferLen, SQL_DRIVER_PROMPT);  
  
   retCode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
   // Free handles, and disconnect.     
   if (hstmt) {   
      SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
      hstmt = NULL;   
   }  
   if (hdbc) {   
      SQLDisconnect(hdbc);  
      SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
      hdbc = NULL;   
   }  
   if (henv) {   
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
      henv = NULL;   
   }  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Canceling statement processing|[SQLCance Functionl](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Setting a cursor name|[SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md)
