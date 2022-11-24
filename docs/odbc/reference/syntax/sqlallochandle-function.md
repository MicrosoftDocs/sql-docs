---
description: "SQLAllocHandle Function"
title: "SQLAllocHandle Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLAllocHandle"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLAllocHandle"
helpviewer_keywords: 
  - "SQLAllocHandle function [ODBC]"
ms.assetid: 6e7fe420-8cf4-4e72-8dad-212affaff317
author: David-Engel
ms.author: v-davidengel
---
# SQLAllocHandle Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLAllocHandle** allocates an environment, connection, statement, or descriptor handle.  
  
> [!NOTE]  
>  This function is a generic function for allocating handles that replaces the ODBC 2.0 functions **SQLAllocConnect**, **SQLAllocEnv**, and **SQLAllocStmt**. To allow applications calling **SQLAllocHandle** to work with ODBC 2.*x* drivers, a call to **SQLAllocHandle** is mapped in the Driver Manager to **SQLAllocConnect**, **SQLAllocEnv**, or **SQLAllocStmt**, as appropriate. For more information, see "Comments." For more information about what the Driver Manager maps this function to when an ODBC 3.*x* application is working with an ODBC 2.*x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLAllocHandle(  
      SQLSMALLINT   HandleType,  
      SQLHANDLE     InputHandle,  
      SQLHANDLE *   OutputHandlePtr);  
```  
  
## Arguments  
 *HandleType*  
 [Input] The type of handle to be allocated by **SQLAllocHandle**. Must be one of the following values:  
  
-   SQL_HANDLE_DBC  
  
-   SQL_HANDLE_DBC_INFO_TOKEN  
  
-   SQL_HANDLE_DESC  
  
-   SQL_HANDLE_ENV  
  
-   SQL_HANDLE_STMT  
  
 SQL_HANDLE_DBC_INFO_TOKEN handle is used only by the Driver Manager and driver. Applications should not use this handle type. For more information about SQL_HANDLE_DBC_INFO_TOKEN, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).  
  
 *InputHandle*  
 [Input] The input handle in whose context the new handle is to be allocated. If *HandleType* is SQL_HANDLE_ENV, this is SQL_NULL_HANDLE. If *HandleType* is SQL_HANDLE_DBC, this must be an environment handle, and if it is SQL_HANDLE_STMT or SQL_HANDLE_DESC, it must be a connection handle.  
  
 *OutputHandlePtr*  
 [Output] Pointer to a buffer in which to return the handle to the newly allocated data structure.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_INVALID_HANDLE, or SQL_ERROR.  
  
 When allocating a handle other than an environment handle, if **SQLAllocHandle** returns SQL_ERROR, it sets *OutputHandlePtr* to SQL_NULL_HDBC, SQL_NULL_HSTMT, or SQL_NULL_HDESC, depending on the value of *HandleType*, unless the output argument is a null pointer. The application can then obtain additional information from the diagnostic data structure associated with the handle in the *InputHandle* argument.  
  
## Environment Handle Allocation Errors  
 Environment allocation occurs both within the Driver Manager and within each driver. The error returned by **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV depends on the level in which the error occurred.  
  
 If the Driver Manager cannot allocate memory for *\*OutputHandlePtr* when **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV is called, or the application provides a null pointer for *OutputHandlePtr*, **SQLAllocHandle** returns SQL_ERROR. The Driver Manager sets **OutputHandlePtr* to SQL_NULL_HENV (unless the application provided a null pointer, which returns SQL_ERROR). There is no handle with which to associate additional diagnostic information.  
  
 The Driver Manager does not call the driver-level environment handle allocation function until the application calls **SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect**. If an error occurs in the driver-level **SQLAllocHandle** function, then the Driver Manager-level **SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect** function returns SQL_ERROR. The diagnostic data structure contains SQLSTATE IM004 (Driver's **SQLAllocHandle** failed). The error is returned on a connection handle.  
  
 For more information about the flow of function calls between the Driver Manager and a driver, see [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
## Diagnostics  
 When **SQLAllocHandle** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with the appropriate *HandleType* and *Handle* set to the value of *InputHandle*. SQL_SUCCESS_WITH_INFO (but not SQL_ERROR) can be returned for the *OutputHandle* argument. The following table lists the SQLSTATE values typically returned by **SQLAllocHandle** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|(DM) The *HandleType* argument was SQL_HANDLE_STMT or SQL_HANDLE_DESC, but the connection specified by the *InputHandle* argument was not open. The connection process must be completed successfully (and the connection must be open) for the driver to allocate a statement or descriptor handle.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the **MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|(DM) The Driver Manager was unable to allocate memory for the specified handle.<br /><br /> The driver was unable to allocate memory for the specified handle.|  
|HY009|Invalid use of null pointer|(DM) The *OutputHandlePtr* argument was a null pointer.|  
|HY010|Function sequence error|(DM) The *HandleType* argument was SQL_HANDLE_DBC, and **SQLSetEnvAttr** has not been called to set the SQL_ODBC_VERSION environment attribute.<br /><br /> (DM) An asynchronously executing function was called for the **InputHandle** and was still executing when the **SQLAllocHandle** function was called with **HandleType** set to SQL_HANDLE_STMT or SQL_HANDLE_DESC.|  
|HY013|Memory management error|The *HandleType* argument was SQL_HANDLE_DBC, SQL_HANDLE_STMT, or SQL_HANDLE_DESC; and the function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY014|Limit on the number of handles exceeded|The driver-defined limit for the number of handles that can be allocated for the type of handle indicated by the *HandleType* argument has been reached.|  
|HY092|Invalid attribute/option identifier|(DM) The *HandleType* argument was not: SQL_HANDLE_ENV, SQL_HANDLE_DBC, SQL_HANDLE_STMT, or SQL_HANDLE_DESC.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The *HandleType* argument was SQL_HANDLE_DESC and the driver was an ODBC 2.*x* driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The *HandleType* argument was SQL_HANDLE_STMT, and the driver was not a valid ODBC driver.<br /><br /> (DM) The *HandleType* argument was SQL_HANDLE_DESC, and the driver does not support allocating a descriptor handle.|  
  
## Comments  
 **SQLAllocHandle** is used to allocate handles for environments, connections, statements, and descriptors, as described in the following sections. For general information about handles, see [Handles](../../../odbc/reference/develop-app/handles.md).  
  
 More than one environment, connection, or statement handle can be allocated by an application at a time if multiple allocations are supported by the driver. In ODBC, no limit is defined on the number of environment, connection, statement, or descriptor handles that can be allocated at any one time. Drivers may impose a limit on the number of a certain type of handle that can be allocated at a time; for more information, see the driver documentation.  
  
 If the application calls **SQLAllocHandle** with *\*OutputHandlePtr* set to an environment, connection, statement, or descriptor handle that already exists, the driver overwrites the information associated with the *handle*, unless the application is using connection pooling (see "Allocating an Environment Attribute for Connection Pooling" later in this section). The Driver Manager does not check to see whether the *handle* entered in *\*OutputHandlePtr* is already being used, nor does it check the previous contents of a handle before overwriting them.  
  
> [!NOTE]  
>  It is incorrect ODBC application programming to call **SQLAllocHandle** two times with the same application variable defined for *\*OutputHandlePtr* without calling **SQLFreeHandle** to free the handle before reallocating it. Overwriting ODBC handles in such a manner could lead to inconsistent behavior or errors on the part of ODBC drivers.  
  
 On operating systems that support multiple threads, applications can use the same environment, connection, statement, or descriptor handle on different threads. Drivers must therefore support safe, multithread access to this information; one way to achieve this, for example, is by using a critical section or a semaphore. For more information about threading, see [Multithreading](../../../odbc/reference/develop-app/multithreading.md).  
  
 **SQLAllocHandle** does not set the SQL_ATTR_ODBC_VERSION environment attribute when it is called to allocate an environment handle; the environment attribute must be set by the application, or SQLSTATE HY010 (Function sequence error) will be returned when **SQLAllocHandle** is called to allocate a connection handle.  
  
 For standards-compliant applications, **SQLAllocHandle** is mapped to **SQLAllocHandleStd** at compile time. The difference between these two functions is that **SQLAllocHandleStd** sets the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC3 when it is called with the *HandleType* argument set to SQL_HANDLE_ENV. This is done because standards-compliant applications are always ODBC 3.*x* applications. Moreover, the standards do not require the application version to be registered. This is the only difference between these two functions; otherwise, they are identical. **SQLAllocHandleStd** is mapped to **SQLAllocHandle** inside the driver manager. Therefore, third-party drivers do not have to implement **SQLAllocHandleStd**.  
  
 ODBC 3.8 applications should use:  
  
-   **SQLAllocHandle and not SQLAllocHandleStd** to allocate an environment handle.  
  
-   **SQLSetEnvAttr** to set the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC3_80.  
  
## Allocating an Environment Handle  
 An environment handle provides access to global information such as valid connection handles and active connection handles. For general information about environment handles, see [Environment Handles](../../../odbc/reference/develop-app/environment-handles.md).  
  
 To request an environment handle, an application calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV and an *InputHandle* of SQL_NULL_HANDLE. The driver allocates memory for the environment information and passes the value of the associated handle back in the *\*OutputHandlePtr* argument. The application passes the *\*OutputHandle* value in all subsequent calls that require an environment handle argument. For more information, see [Allocating the Environment Handle](../../../odbc/reference/develop-app/allocating-the-environment-handle.md).  
  
 Under a Driver Manager's environment handle, if there already exists a driver's environment handle, then **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV is not called in that driver when a connection is made, only **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC. If a driver's environment handle does not exist under the Driver Manager's environment handle, both SQLAllocHandle with a HandleType of SQL_HANDLE_ENV and SQLAllocHandle with a HandleType of SQL_HANDLE_DBC are called in the driver when the first connection handle of the environment is connected to the driver.  
  
 When the Driver Manager processes the **SQLAllocHandle** function with a *HandleType* of SQL_HANDLE_ENV, it checks the **Trace** keyword in the [ODBC] section of the system information. If it is set to 1, the Driver Manager enables tracing for the current application. If the trace flag is set, tracing starts when the first environment handle is allocated and ends when the last environment handle is freed. For more information, see [Configuring Data Sources](../../../odbc/reference/install/configuring-data-sources.md).  
  
 After allocating an environment handle, an application must call **SQLSetEnvAttr** on the environment handle to set the SQL_ATTR_ODBC_VERSION environment attribute. If this attribute is not set before **SQLAllocHandle** is called to allocate a connection handle on the environment, the call to allocate the connection will return SQLSTATE HY010 (Function sequence error). For more information, see [Declaring the Application's ODBC Version](../../../odbc/reference/develop-app/declaring-the-application-s-odbc-version.md).  
  
## Allocating Shared Environments for Connection Pooling  
 Environments can be shared among multiple components on a single process. A shared environment can be used by more than one component at the same time. When a component uses a shared environment, it can use pooled connections, which allow it to allocate and use an existing connection without re-creating that connection.  
  
 Before allocating a shared environment that can be used for connection pooling, an application must call **SQLSetEnvAttr** to set the SQL_ATTR_CONNECTION_POOLING environment attribute to SQL_CP_ONE_PER_DRIVER or SQL_CP_ONE_PER_HENV. **SQLSetEnvAttr** in this case is called with *EnvironmentHandle* set to null, which makes the attribute a process-level attribute.  
  
 After connection pooling has been enabled, an application calls **SQLAllocHandle** with the *HandleType* argument set to SQL_HANDLE_ENV. The environment allocated by this call will be an implicit shared environment because connection pooling has been enabled.  
  
 When a shared environment is allocated, the environment that will be used is not determined until **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC is called. At that point, the Driver Manager tries to find an existing environment that matches the environment attributes requested by the application. If no such environment exists, one is created as a shared environment. The Driver Manager maintains a reference count for each shared environment; the count is set to 1 when the environment is first created. If a matching environment is found, the handle of that environment is returned to the application and the reference count is incremented. An environment handle allocated in this manner can be used in any ODBC function that accepts an environment handle as an input argument.  
  
## Allocating a Connection Handle  
 A connection handle provides access to information such as the valid statement and descriptor handles on the connection and whether a transaction is currently open. For general information about connection handles, see [Connection Handles](../../../odbc/reference/develop-app/connection-handles.md).  
  
 To request a connection handle, an application calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC. The *InputHandle* argument is set to the environment handle that was returned by the call to **SQLAllocHandle** that allocated that handle. The driver allocates memory for the connection information and passes the value of the associated handle back in *\*OutputHandlePtr*. The application passes the *\*OutputHandlePtr* value in all subsequent calls that require a connection handle. For more information, see [Allocating a Connection Handle](../../../odbc/reference/develop-app/allocating-a-connection-handle-odbc.md).  
  
 The Driver Manager processes the **SQLAllocHandle** function and calls the driver's **SQLAllocHandle** function when the application calls **SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect**. (For more information, see [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).)  
  
 If the SQL_ATTR_ODBC_VERSION environment attribute is not set before **SQLAllocHandle** is called to allocate a connection handle on the environment, the call to allocate the connection will return SQLSTATE HY010 (Function sequence error).  
  
 When an application calls **SQLAllocHandle** with the *InputHandle* argument set to SQL_HANDLE_DBC and also set to a shared environment handle, the Driver Manager tries to find an existing shared environment that matches the environment attributes set by the application. If no such environment exists, one is created, with a reference count (maintained by the Driver Manager) of 1. If a matching shared environment is found, that handle is returned to the application and its reference count is incremented.  
  
 The actual connection that will be used is not determined by the Driver Manager until **SQLConnect** or **SQLDriverConnect** is called. The Driver Manager uses the connection options in the call to **SQLConnect** (or the connection keywords in the call to **SQLDriverConnect**) and the connection attributes set after connection allocation to determine which connection in the pool should be used. For more information, see [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
## Allocating a Statement Handle  
 A statement handle provides access to statement information, such as error messages, the cursor name, and status information for SQL statement processing. For general information about statement handles, see [Statement Handles](../../../odbc/reference/develop-app/statement-handles.md).  
  
 To request a statement handle, an application connects to a data source and then calls **SQLAllocHandle** before it submits SQL statements. In this call, *HandleType* should be set to SQL_HANDLE_STMT and *InputHandle* should be set to the connection handle that was returned by the call to **SQLAllocHandle** that allocated that handle. The driver allocates memory for the statement information, associates the statement handle with the specified connection, and passes the value of the associated handle back in *\*OutputHandlePtr*. The application passes the *\*OutputHandlePtr* value in all subsequent calls that require a statement handle. For more information, see [Allocating a Statement Handle](../../../odbc/reference/develop-app/allocating-a-statement-handle-odbc.md).  
  
 When the statement handle is allocated, the driver automatically allocates a set of four descriptors and assigns the handles for these descriptors to the SQL_ATTR_APP_ROW_DESC, SQL_ATTR_APP_PARAM_DESC, SQL_ATTR_IMP_ROW_DESC, and SQL_ATTR_IMP_PARAM_DESC statement attributes. These are referred to as *implicitly* allocated descriptors. To allocate an application descriptor explicitly, see the following section, "Allocating a Descriptor Handle."  
  
## Allocating a Descriptor Handle  
 When an application calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DESC, the driver allocates an application descriptor. These are referred to as *explicitly* allocated descriptors. The application directs a driver to use an explicitly allocated application descriptor instead of an automatically allocated one for a given statement handle by calling the **SQLSetStmtAttr** function with the SQL_ATTR_APP_ROW_DESC or SQL_ATTR_APP_PARAM_DESC attribute. An implementation descriptor cannot be allocated explicitly, nor can an implementation descriptor be specified in an **SQLSetStmtAttr** function call.  
  
 Explicitly allocated descriptors are associated with a connection handle instead of a statement handle (as automatically allocated descriptors are). Descriptors remain allocated only when an application is actually connected to the database. Because explicitly allocated descriptors are associated with a connection handle, an application can associate an explicitly allocated descriptor with more than one statement within a connection. An implicitly allocated application descriptor, on the other hand, cannot be associated with more than one statement handle. (It cannot be associated with any statement handle other than the one that it was allocated for.) Explicitly allocated descriptor handles can be freed explicitly either by the application or by calling **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DESC, or implicitly when the connection is closed.  
  
 When the explicitly allocated descriptor is freed, the implicitly allocated descriptor is again associated with the statement. (The SQL_ATTR_APP_ROW_DESC or SQL_ATTR_APP_PARAM_DESC attribute for that statement is again set to the implicitly allocated descriptor handle.) This is true for all statements that were associated with the explicitly allocated descriptor on the connection.  
  
 For more information about descriptors, see [Descriptors](../../../odbc/reference/develop-app/descriptors.md).  
  
## Code Example  
 See [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md), [SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md), [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md), and [SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Freeing an environment, connection, statement, or descriptor handle|[SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)|  
|Preparing a statement for execution|[SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
|Setting a descriptor field|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
|Setting an environment attribute|[SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
