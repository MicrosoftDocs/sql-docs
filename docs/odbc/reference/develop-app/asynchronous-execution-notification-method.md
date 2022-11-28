---
description: "Asynchronous Execution (Notification Method)"
title: "Asynchronous Execution (Notification Method) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
ms.assetid: e509dad9-5263-4a10-9a4e-03b84b66b6b3
author: David-Engel
ms.author: v-davidengel
---
# Asynchronous Execution (Notification Method)
ODBC allows asynchronous execution of connection and statement operations. An application thread can call an ODBC function in asynchronous mode and the function can return before the operation is complete, allowing the application thread to perform other tasks. In the Windows 7 SDK, for asynchronous statement or connection operations, an application determined that the asynchronous operation was complete using the polling method. For more information, see [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md). Beginning in the Windows 8 SDK, you can determine that an asynchronous operation is complete using the notification method.  
  
 In the polling method, applications need to call the asynchronous function each time it wants the status of the operation. The notification method is similar to callback and wait in ADO.NET. ODBC, however, uses Win32 events as the notification object.  
  
 The ODBC Cursor Library and ODBC asynchronous notification cannot be used at the same time. Setting both attributes will return an error with SQLSTATE S1119 (Cursor Library and Asynchronous Notification cannot be enabled at the same time).  
  
 See [Notification of Asynchronous Function Completion](../../../odbc/reference/develop-driver/notification-of-asynchronous-function-completion.md) for information for driver developers.  
  
> [!NOTE]  
>  The notification method is not supported with cursor library. An application will receive error message if it attempts to enable cursor library via SQLSetConnectAttr, when the notification method is enabled.  
  
## Overview  
 When an ODBC function is called in asynchronous mode, the control is returned to the calling application immediately with the return code SQL_STILL_EXECUTING. The application must repeatedly poll the function until it returns something other than SQL_STILL_EXECUTING. The polling loop increases CPU utilization, causing poor performance in many asynchronous scenarios.  
  
 Whenever the notification model is used, the polling model is disabled. Applications should not call the original function again. Call [SQLCompleteAsync Function](../../../odbc/reference/syntax/sqlcompleteasync-function.md) to complete the asynchronous operation. If an application calls the original function again before the asynchronous operation is complete, the call will return SQL_ERROR with SQLSTATE IM017 (Polling is disabled in Asynchronous Notification Mode).  
  
 When using the notification model, the application can call **SQLCancel** or **SQLCancelHandle** to cancel a statement or connection operation. If the cancel request is successful, ODBC will return SQL_SUCCESS. This message does not indicate that the function was actually canceled; it indicates that the cancel request was processed. Whether the function is actually canceled is driver-dependent and data source-dependent. When an operation is canceled, the Driver Manager will still signal the event. The Driver Manager returns SQL_ERROR in the return code buffer and the state is SQLSTATE HY008 (Operation canceled) to indicate the cancelation is successful. If the function completed its normal processing, the Driver Manager returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO.  
  
### Downlevel Behavior  
 The ODBC Driver Manager version supporting this notification on complete is ODBC 3.81.  
  
|Application ODBC Version|Driver Manager Version|Driver Version|Behavior|  
|------------------------------|----------------------------|--------------------|--------------|  
|New application of any ODBC version|ODBC 3.81|ODBC 3.80 Driver|Application can use this feature if the driver supports this feature, otherwise the Driver Manager will error out.|  
|New application of any ODBC version|ODBC 3.81|Pre-ODBC 3.80 Driver|The Driver Manager will error out if the driver does not support this feature.|  
|New application of any ODBC version|Pre-ODBC 3.81|Any|When the application uses this feature, an old Driver Manager will regard the new attributes as driver-specific attributes, and the driver should error out. A new Driver Manager will not pass these attributes to the driver.|  
  
 An application should check the Driver Manager version before using this feature. Otherwise, if a poorly written driver does not error out and the Driver Manager version is pre ODBC 3.81, behavior is undefined.  
  
## Use Cases  
 This section shows use cases for asynchronous execution and the polling mechanism.  
  
### Integrate Data from Multiple ODBC Sources  
 A data integration application asynchronously fetches data from multiple data sources. Some of the data are from remote data sources and some data are from local files. The application cannot continue until the asynchronous operations are completed.  
  
 Instead of repeatedly polling an operation to determine if it is complete, the application can create an event object and associate it with an ODBC connection handle or an ODBC statement handle. The application then calls operating system synchronization APIs to wait on one event object or many event objects (both ODBC events and other Windows events). ODBC will signal the event object when the corresponding ODBC asynchronous operation is completed.  
  
 On Windows, Win32 event objects will be used and that will provide the user a unified programming model. Driver Managers on other platforms can use the event object implementation specific to those platforms.  
  
 The following code sample demonstrates the use of connection and statement asynchronous notification:  
  
```  
// This function opens NUMBER_OPERATIONS connections and executes one query on statement of each connection.  
// Asynchronous Notification is used  
  
#define NUMBER_OPERATIONS 5  
int AsyncNotificationSample(void)  
{  
    RETCODE     rc;  
  
    SQLHENV     hEnv              = NULL;  
    SQLHDBC     arhDbc[NUMBER_OPERATIONS]         = {NULL};  
    SQLHSTMT    arhStmt[NUMBER_OPERATIONS]        = {NULL};  
  
    HANDLE      arhDBCEvent[NUMBER_OPERATIONS]    = {NULL};  
    RETCODE     arrcDBC[NUMBER_OPERATIONS]        = {0};  
    HANDLE      arhSTMTEvent[NUMBER_OPERATIONS]   = {NULL};  
    RETCODE     arrcSTMT[NUMBER_OPERATIONS]       = {0};  
  
    rc = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &hEnv);  
    if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
  
    rc = SQLSetEnvAttr(hEnv,  
        SQL_ATTR_ODBC_VERSION,  
        (SQLPOINTER) SQL_OV_ODBC3_80,  
        SQL_IS_INTEGER);  
    if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
  
    // Connection operations begin here  
  
    // Alloc NUMBER_OPERATIONS connection handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &arhDbc[i]);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Enable DBC Async on all connection handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetConnectAttr(arhDbc[i], SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE, (SQLPOINTER)SQL_ASYNC_DBC_ENABLE_ON, SQL_IS_INTEGER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Application must create event objects  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        arhDBCEvent[i] = CreateEvent(NULL, FALSE, FALSE, NULL); // Auto-reset, initial state is not-signaled  
        if (!arhDBCEvent[i]) goto Cleanup;  
    }  
  
    // Enable notification on all connection handles  
    // Event  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetConnectAttr(arhDbc[i], SQL_ATTR_ASYNC_DBC_EVENT, arhDBCEvent[i], SQL_IS_POINTER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Initiate connect establishing  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLDriverConnect(arhDbc[i], NULL, (SQLTCHAR*)TEXT("Driver={ODBC Driver 11 for SQL Server};SERVER=dp-srv-sql2k;DATABASE=pubs;UID=sa;PWD=XYZ;"), SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhDBCEvent, TRUE, INFINITE); // Wait All  
  
    // Complete connect API calls  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_DBC, arhDbc[i], & arrcDBC[i]);  
    }  
  
    BOOL fFail = FALSE; // Whether some connection openning fails.  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcDBC[i]) )   
            fFail = TRUE;  
    }  
  
    // If some SQLDriverConnect() fail, clean up.  
    if (fFail)  
    {  
        for (int i=0; i<NUMBER_OPERATIONS; i++)  
        {  
            if (SQL_SUCCEEDED(arrcDBC[i]) )   
            {  
                SQLDisconnect(arhDbc[i]); // This is also async  
            }  
            else  
            {  
                SetEvent(arhDBCEvent[i]); // Previous SQLDriverConnect() failed. No need to call SQLDisconnect().  
            }  
        }  
        WaitForMultipleObjects(NUMBER_OPERATIONS, arhDBCEvent, TRUE, INFINITE);   
        for (int i=0; i<NUMBER_OPERATIONS; i++)  
        {  
            if (SQL_SUCCEEDED(arrcDBC[i]) )   
            {     
                SQLCompleteAsync(SQL_HANDLE_DBC, arhDbc[i], &arrcDBC[i]);; // To Complete  
            }  
        }  
  
        goto Cleanup;  
    }  
  
    // Statement Operations begin here  
  
    // Alloc statement handle  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLAllocHandle(SQL_HANDLE_STMT, arhDbc[i], &arhStmt[i]);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Enable STMT Async on all statement handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLSetStmtAttr(arhStmt[i], SQL_ATTR_ASYNC_ENABLE, (SQLPOINTER)SQL_ASYNC_ENABLE_ON, SQL_IS_INTEGER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Create event objects  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        arhSTMTEvent[i] = CreateEvent(NULL, FALSE, FALSE, NULL); // Auto-reset, initial state is not-signaled  
        if (!arhSTMTEvent[i]) goto Cleanup;  
    }  
  
    // Enable notification on all statement handles  
    // Event  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetStmtAttr(arhStmt[i], SQL_ATTR_ASYNC_STMT_EVENT, arhSTMTEvent[i], SQL_IS_POINTER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Initiate SQLExecDirect() calls  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLExecDirect(arhStmt[i], (SQLTCHAR*)TEXT("select au_lname, au_fname from authors"), SQL_NTS);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhSTMTEvent, TRUE, INFINITE); // Wait All  
  
    // Now, call SQLCompleteAsync to complete the operation and get return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_STMT, arhStmt[i], &arrcSTMT[i]);  
    }  
  
    // Check return values  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcSTMT[i]) ) goto Cleanup;  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        //Do some binding jobs here, set SQL_ATTR_ROW_ARRAY_SIZE   
    }  
  
    // Now, initiate fetching  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLFetch(arhStmt[i]);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhSTMTEvent, TRUE, INFINITE);   
  
    // Now, to complete the operations and get return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_STMT, arhStmt[i], &arrcSTMT[i]);  
    }  
  
    // Check return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcSTMT[i]) ) goto Cleanup;  
    }  
  
    // USE fetched data here!!  
  
Cleanup:  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhStmt[NUMBER_OPERATIONS])  
        {  
            SQLFreeHandle(SQL_HANDLE_STMT, arhStmt[i]);  
            arhStmt[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhSTMTEvent[i])  
        {  
            CloseHandle(arhSTMTEvent[i]);  
            arhSTMTEvent[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhDbc[i])  
        {  
            SQLFreeHandle(SQL_HANDLE_DBC, arhDbc[i]);  
            arhDbc[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhDBCEvent[i])  
        {  
            CloseHandle(arhDBCEvent[i]);  
            arhDBCEvent[i] = NULL;  
        }  
    }  
  
    if (hEnv)  
    {  
        SQLFreeHandle(SQL_HANDLE_ENV, hEnv);  
        hEnv = NULL;  
    }  
  
    return 0;  
}  
  
```  
  
### Determining if a Driver Supports Asynchronous Notification  
 An ODBC application can determine if an ODBC driver supports asynchronous notification by calling [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md). The ODBC Driver Manager will consequently call the **SQLGetInfo** of the driver with SQL_ASYNC_NOTIFICATION.  
  
```  
SQLUINTEGER InfoValue;  
SQLLEN      cbInfoLength;  
  
SQLRETURN retcode;  
retcode = SQLGetInfo (hDbc,   
                      SQL_ASYNC_NOTIFICATION,   
                      &InfoValue,  
                      sizeof(InfoValue),  
                      NULL);  
if (SQL_SUCCEEDED(retcode))  
{  
if (SQL_ASYNC_NOTIFICATION_CAPABLE == InfoValue)  
      {  
          // The driver supports asynchronous notification  
      }  
      else if (SQL_ASYNC_NOTIFICATION_NOT_CAPABLE == InfoValue)  
      {  
          // The driver does not support asynchronous notification  
      }  
}  
```  
  
### Associating a Win32 Event Handle with an ODBC Handle  
 Applications are responsible for creating Win32 event objects using the corresponding Win32 functions. An application can associate one Win32 event handle with one ODBC connection handle or one ODBC statement handle.  
  
 Connection attributes SQL_ATTR_ASYNC_DBC_FUNCTION_ENABLE and SQL_ATTR_ASYNC_DBC_EVENT determine whether ODBC executes in asynchronous mode and whether ODBC enables notification mode for a connection handle. Statement attributes SQL_ATTR_ASYNC_ENABLE and SQL_ATTR_ASYNC_STMT_EVENT determine whether ODBC executes in asynchronous mode and whether ODBC enables notification mode for a statement handle.  
  
|SQL_ATTR_ASYNC_ENABLE or SQL_ATTR_ASYNC_DBC_FUNCTION_ENABLE|SQL_ATTR_ASYNC_STMT_EVENT or SQL_ATTR_ASYNC_DBC_EVENT|Mode|  
|-------------------------------------------------------------------------|-------------------------------------------------------------------|----------|  
|Enable|non-null|Asynchronous Notification|  
|Enable|null|Asynchronous Polling|  
|Disable|any|Synchronous|  
  
 An application can temporally disable asynchronous operation mode. ODBC ignores values of SQL_ATTR_ASYNC_DBC_EVENT if the connection level asynchronous operation is disabled. ODBC ignores values of SQL_ATTR_ASYNC_STMT_EVENT if the statement level asynchronous operation is disabled.  
  
 Synchronous call of **SQLSetStmtAttr** and **SQLSetConnectAttr**  
 -   **SQLSetConnectAttr** supports asynchronous operations but the invocation of **SQLSetConnectAttr** to set SQL_ATTR_ASYNC_DBC_EVENT is always synchronous.  
  
-   **SQLSetStmtAttr** does not support asynchronous execution.  
  
 Error-out scenario  
 When **SQLSetConnectAttr** is called before making a connection, the Driver Manager cannot determine which driver to use. Therefore, the Driver Manager returns success for **SQLSetConnectAttr** but the attribute may not be ready to set in the driver. The Driver Manager will set these attributes when the application calls a connection function. The Driver Manager may error-out because driver does not support asynchronous operations.  
  
 Inheritance of connection attributes  
 Usually, the statements of a connection will inherit the connection attributes. However, the attribute SQL_ATTR_ASYNC_DBC_EVENT is not inheritable and only affects the connection operations.  
  
 To associate an event handle with an ODBC connection handle, an ODBC application calls ODBC API **SQLSetConnectAttr** and specifies SQL_ATTR_ASYNC_DBC_EVENT as the attribute and the event handle as the attribute value. The new ODBC attribute SQL_ATTR_ASYNC_DBC_EVENT is of type SQL_IS_POINTER.  
  
```  
HANDLE hEvent;  
hEvent = CreateEvent(   
            NULL,                // default security attributes  
            FALSE,               // auto-reset event  
            FALSE,               // initial state is non-signaled  
            NULL                 // no name  
            );  
```  
  
 Usually, applications create auto-reset event objects. ODBC will not reset the event object. Applications must make sure that the object is not in signaled state before calling any asynchronous ODBC function.  
  
```  
SQLRETURN retcode;  
retcode = SQLSetConnectAttr ( hDBC,  
                              SQL_ATTR_ASYNC_DBC_EVENT, // Attribute name  
                              (SQLPOINTER) hEvent,      // Win32 Event handle  
                              SQL_IS_POINTER);          // Length Indicator  
```  
  
 SQL_ATTR_ASYNC_DBC_EVENT is a Driver Manager-only attribute that will not be set in the driver.  
  
 The default value of SQL_ATTR_ASYNC_DBC_EVENT is NULL. If the driver does not support asynchronous notification, getting or setting SQL_ATTR_ASYNC_DBC_EVENT will return SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier).  
  
 If the last SQL_ATTR_ASYNC_DBC_EVENT value set on an ODBC connection handle is not NULL and the application enabled asynchronous mode by setting attribute SQL_ATTR_ASYNC_DBC_FUNCTION_ENABLE with SQL_ASYNC_DBC_ENABLE_ON, calling any ODBC connection function that supports asynchronous mode will get a completion notification. If the last SQL_ATTR_ASYNC_DBC_EVENT value set on an ODBC connection handle is NULL, ODBC will not send the application any notification, regardless whether asynchronous mode is enabled.  
  
 An application can set SQL_ATTR_ASYNC_DBC_EVENT before or after setting the attribute SQL_ATTR_ASYNC_DBC_FUNCTION_ENABLE.  
  
 Applications can set the SQL_ATTR_ASYNC_DBC_EVENT attribute on an ODBC connection handle before calling a connection function (**SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect**). Because the ODBC Driver Manager does not know which ODBC driver the application will use, it will return SQL_SUCCESS. When the application calls a connection function, the ODBC Driver Manager will check whether the driver supports asynchronous notification. If the driver does not support asynchronous notification, the ODBC Driver Manager will return SQL_ERROR with SQLSTATE S1_118 (Driver does not support asynchronous notification). If the driver supports asynchronous notification, the ODBC Driver Manager will call the driver and set the corresponding attributes SQL_ATTR_ASYNC_DBC_NOTIFICATION_CALLBACK and SQL_ATTR_ASYNC_DBC_NOTIFICATION_CONTEXT.  
  
 Similarly, an application calls **SQLSetStmtAttr** on an ODBC statement handle and specifies the SQL_ATTR_ASYNC_STMT_EVENT attribute to enable or disable statement level asynchronous notification. Because a statement function is always called after the connection is established, **SQLSetStmtAttr** will return SQL_ERROR with SQLSTATE S1_118 (Driver does not support asynchronous notification) immediately if the corresponding driver does not support asynchronous operations or the driver supports asynchronous operation but does not support asynchronous notification.  
  
```  
SQLRETURN retcode;  
retcode = SQLSetStmtAttr ( hSTMT,  
                           SQL_ATTR_ASYNC_STMT_EVENT, // Attribute name   
                           (SQLPOINTER) hEvent,       // Win32 Event handle  
                           SQL_IS_POINTER);           // length Indicator  
```  
  
 SQL_ATTR_ASYNC_STMT_EVENT, which can be set to NULL, is a Driver Manager-only attribute that will not be set in the driver.  
  
 The default value of SQL_ATTR_ASYNC_STMT_EVENT is NULL. If the driver does not support asynchronous notification, getting or setting the SQL_ATTR_ASYNC_ STMT_EVENT attribute will return SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier).  
  
 An application should not associate the same event handle with more than one ODBC handle. Otherwise, one notification will be lost if two asynchronous ODBC function invocations complete on two handles that share the same event handle. To avoid a statement handle inheriting the same event handle from the connection handle, ODBC returns SQL_ERROR with SQLSTATE IM016 (Cannot set statement attribute into connection handle) if an application sets SQL_ATTR_ASYNC_STMT_EVENT on a connection handle.  
  
### Calling Asynchronous ODBC Functions  
 After enabling asynchronous notification and starting an asynchronous operation, the application can call any ODBC function. If the function belongs to the set of functions that support asynchronous operation, the application will get a completion notification when the operation completes, regardless of whether the function failed or succeeded.  The only exception is that the application calls an ODBC function with an invalid connection or statement handle. In this case, ODBC will not get the event handle and set it to the signaled state.  
  
 The application must ensure that the associated event object is in a non-signaled state before starting an asynchronous operation on the corresponding ODBC handle. ODBC will not reset the event object.  
  
### Getting Notification from ODBC  
 An application thread can call **WaitForSingleObject** to wait on one event handle or call **WaitForMultipleObjects** to wait on an array of event handles and be suspended until one or all of the event objects become signaled or the time-out interval elapses.  
  
```  
DWORD dwStatus = WaitForSingleObject(  
                        hEvent,  // The event associated with the ODBC handle  
                        5000     // timeout is 5000 millisecond   
);  
  
If (dwStatus == WAIT_TIMEOUT)  
{  
    // time-out interval elapsed before all the events are signaled.   
}  
Else  
{  
    // Call the corresponding Asynchronous ODBC API to complete all processing and retrieve the return code.  
}  
```
