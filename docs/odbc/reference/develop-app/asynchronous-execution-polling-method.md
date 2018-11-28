---
title: "Asynchronous Execution (Polling Method) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "asynchronous execution [ODBC]"
ms.assetid: 8cd21734-ef8e-4066-afd5-1f340e213f9c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Asynchronous Execution (Polling Method)
Prior to ODBC 3.8 and the Windows 7 SDK, asynchronous operations were permitted only on statement functions. For more information, see the **Executing Statement Operations Asynchronously**, later in this topic.  
  
 ODBC 3.8 in the Windows 7 SDK introduced asynchronous execution on connection-related operations. For more information, see the **Executing Connection Operations Asynchronously** section, later in this topic.  
  
 In the Windows 7 SDK, for asynchronous statement or connection operations, an application determined that the asynchronous operation was complete using the polling method. Beginning in the Windows 8 SDK, you can determine that an asynchronous operation is complete using the notification method. For more information, see [Asynchronous Execution (Notification Method)](../../../odbc/reference/develop-app/asynchronous-execution-notification-method.md).  
  
 By default, drivers execute ODBC functions synchronously; that is, the application calls a function and the driver does not return control to the application until it has finished executing the function. However, some functions can be executed asynchronously; that is, the application calls the function, and the driver, after minimal processing, returns control to the application. The application can then call other functions while the first function is still executing.  
  
 Asynchronous execution is supported for most functions that are largely executed on the data source, such as the functions to establish connections, prepare and execute SQL statements, retrieve metadata, fetch data, and commit transactions. It is most useful when the task being executed on the data source takes a long time, such as a login process or a complex query against a large database.  
  
 When the application executes a function with a statement or connection that is enabled for asynchronous processing, the driver performs a minimal amount of processing (such as checking arguments for errors), hands processing to the data source, and returns control to the application with the SQL_STILL_EXECUTING return code. The application then performs other tasks. To determine when the asynchronous function has finished, the application polls the driver at regular intervals by calling the function with the same arguments as it originally used. If the function is still executing, it returns SQL_STILL_EXECUTING; if it has finished executing, it returns the code it would have returned had it executed synchronously, such as SQL_SUCCESS, SQL_ERROR, or SQL_NEED_DATA.  
  
 Whether a function executes synchronously or asynchronously is driver specific. For example, suppose the result set metadata is cached in the driver. In this case, it takes very little time to execute **SQLDescribeCol** and the driver should simply execute the function rather than artificially delay execution. On the other hand, if the driver needs to retrieve the metadata from the data source, it should return control to the application while it is doing this. Therefore, the application must be able to handle a return code other than SQL_STILL_EXECUTING when it first executes a function asynchronously.  
  
## Executing Statement Operations Asynchronously  
 The following statement functions operate on a data source and can execute asynchronously:  
  
||||  
|-|-|-|  
|[SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|[SQLColAttribute](../../../odbc/reference/syntax/sqlcolattribute-function.md)|[SQLColumnPrivileges](../../../odbc/reference/syntax/sqlcolumnprivileges-function.md)|  
|[SQLColumns](../../../odbc/reference/syntax/sqlcolumns-function.md)|[SQLDescribeCol](../../../odbc/reference/syntax/sqldescribecol-function.md)|[SQLDescribeParam](../../../odbc/reference/syntax/sqldescribeparam-function.md)|  
|[SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md)|[SQLExecute](../../../odbc/reference/syntax/sqlexecute-function.md)|[SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|[SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|[SQLForeignKeys](../../../odbc/reference/syntax/sqlforeignkeys-function.md)|[SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md)|  
|[SQLGetTypeInfo](../../../odbc/reference/syntax/sqlgettypeinfo-function.md)|[SQLMoreResults](../../../odbc/reference/syntax/sqlmoreresults-function.md)|[SQLNumParams](../../../odbc/reference/syntax/sqlnumparams-function.md)|  
|[SQLNumResultCols](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|[SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md)|[SQLPrepare](../../../odbc/reference/syntax/sqlprepare-function.md)|  
|[SQLPrimaryKeys](../../../odbc/reference/syntax/sqlprimarykeys-function.md)|[SQLProcedureColumns](../../../odbc/reference/syntax/sqlprocedurecolumns-function.md)|[SQLProcedures](../../../odbc/reference/syntax/sqlprocedures-function.md)|  
|[SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md)|[SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md)|[SQLSpecialColumns](../../../odbc/reference/syntax/sqlspecialcolumns-function.md)|  
|[SQLStatistics](../../../odbc/reference/syntax/sqlstatistics-function.md)|[SQLTablePrivileges](../../../odbc/reference/syntax/sqltableprivileges-function.md)|[SQLTables](../../../odbc/reference/syntax/sqltables-function.md)|  
  
 Asynchronous statement execution is controlled on either a per-statement or a per-connection basis, depending on the data source. That is, the application specifies not that a particular function is to be executed asynchronously, but that any function executed on a particular statement is to be executed asynchronously. To find out which one is supported, an application calls [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) with an option of SQL_ASYNC_MODE. SQL_AM_CONNECTION is returned if connection-level asynchronous execution (for a statement handle) is supported; SQL_AM_STATEMENT if statement-level asynchronous execution is supported.  
  
 To specify that functions executed with a particular statement are to be executed asynchronously, the application calls **SQLSetStmtAttr** with the SQL_ATTR_ASYNC_ENABLE attribute and sets it to SQL_ASYNC_ENABLE_ON. If connection-level asynchronous processing is supported, the SQL_ATTR_ASYNC_ENABLE statement attribute is read-only and its value is the same as the connection attribute of the connection on which the statement was allocated. It is driver-specific whether the value of the statement attribute is set at statement allocation time or later. Attempting to set it will return SQL_ERROR and SQLSTATE HYC00 (Optional feature not implemented).  
  
 To specify that functions executed with a particular connection are to be executed asynchronously, the application calls **SQLSetConnectAttr** with the SQL_ATTR_ASYNC_ENABLE attribute and sets it to SQL_ASYNC_ENABLE_ON. All future statement handles allocated on the connection will be enabled for asynchronous execution; it is driver-defined whether existing statement handles will be enabled by this action. If SQL_ATTR_ASYNC_ENABLE is set to SQL_ASYNC_ENABLE_OFF, all statements on the connection are in synchronous mode. An error is returned if asynchronous execution is enabled while there is an active statement on the connection.  
  
 To determine the maximum number of active concurrent statements in asynchronous mode that the driver can support on a given connection, the application calls **SQLGetInfo** with the SQL_MAX_ASYNC_CONCURRENT_STATEMENTS option.  
  
 The following code demonstrates how the polling model works:  
  
```  
SQLHSTMT  hstmt1;  
SQLRETURN rc;  
  
// Specify that the statement is to be executed asynchronously.  
SQLSetStmtAttr(hstmt1, SQL_ATTR_ASYNC_ENABLE, SQL_ASYNC_ENABLE_ON, 0);  
  
// Execute a SELECT statement asynchronously.  
while ((rc=SQLExecDirect(hstmt1,"SELECT * FROM Orders",SQL_NTS))==SQL_STILL_EXECUTING) {  
   // While the statement is still executing, do something else.  
   // Do not use hstmt1, because it is being used asynchronously.  
}  
  
// When the statement has finished executing, retrieve the results.  
```  
  
 While a function is executing asynchronously, the application can call functions on any other statements. The application can also call functions on any connection, except the one associated with the asynchronous statement. But the application can only call the original function and the following functions (with the statement handle or its associated connection, environment handle), after a statement operation returns SQL_STILL_EXECUTING:  
  
-   [SQLCancel](../../../odbc/reference/syntax/sqlcancel-function.md)  
  
-   [SQLCancelHandle](../../../odbc/reference/syntax/sqlcancelhandle-function.md) (on the statement handle)  
  
-   [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md)  
  
-   [SQLGetDiagRec](../../../odbc/reference/syntax/sqlgetdiagrec-function.md)  
  
-   [SQLAllocHandle](../../../odbc/reference/syntax/sqlallochandle-function.md)  
  
-   [SQLGetEnvAttr](../../../odbc/reference/syntax/sqlgetenvattr-function.md)  
  
-   [SQLGetConnectAttr](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)  
  
-   [SQLDataSources](../../../odbc/reference/syntax/sqldatasources-function.md)  
  
-   [SQLDrivers](../../../odbc/reference/syntax/sqldrivers-function.md)  
  
-   [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md)  
  
-   [SQLGetFunctions](../../../odbc/reference/syntax/sqlgetfunctions-function.md)  
  
-   [SQLNativeSql](../../../odbc/reference/syntax/sqlnativesql-function.md)  
  
 If the application calls any other function with the asynchronous statement or with the connection associated with that statement, the function returns SQLSTATE HY010 (Function sequence error), for example.  
  
```  
SQLHDBC       hdbc1, hdbc2;  
SQLHSTMT      hstmt1, hstmt2, hstmt3;  
SQLCHAR *     SQLStatement = "SELECT * FROM Orders";  
SQLUINTEGER   InfoValue;  
SQLRETURN     rc;  
  
SQLAllocHandle(SQL_HANDLE_STMT, hdbc1, &hstmt1);  
SQLAllocHandle(SQL_HANDLE_STMT, hdbc1, &hstmt2);  
SQLAllocHandle(SQL_HANDLE_STMT, hdbc2, &hstmt3);  
  
// Specify that hstmt1 is to be executed asynchronously.  
SQLSetStmtAttr(hstmt1, SQL_ATTR_ASYNC_ENABLE, SQL_ASYNC_ENABLE_ON, 0);  
  
// Execute hstmt1 asynchronously.  
while ((rc = SQLExecDirect(hstmt1, SQLStatement, SQL_NTS)) == SQL_STILL_EXECUTING) {  
   // The following calls return HY010 because the previous call to  
   // SQLExecDirect is still executing asynchronously on hstmt1. The  
   // first call uses hstmt1 and the second call uses hdbc1, on which  
   // hstmt1 is allocated.  
   SQLExecDirect(hstmt1, SQLStatement, SQL_NTS);   // Error!  
   SQLGetInfo(hdbc1, SQL_UNION, (SQLPOINTER) &InfoValue, 0, NULL);   // Error!  
  
   // The following calls do not return errors. They use a statement  
   // handle other than hstmt1 or a connection handle other than hdbc1.  
   SQLExecDirect(hstmt2, SQLStatement, SQL_NTS);   // OK  
   SQLTables(hstmt3, NULL, 0, NULL, 0, NULL, 0, NULL, 0);   // OK  
   SQLGetInfo(hdbc2, SQL_UNION, (SQLPOINTER) &InfoValue, 0, NULL);   // OK  
}  
```  
  
 When an application calls a function to determine whether it is still executing asynchronously, it must use the original statement handle. This is because asynchronous execution is tracked on a per-statement basis. The application must also supply valid values for the other arguments - the original arguments will do - to get past error checking in the Driver Manager. However, after the driver checks the statement handle and determines that the statement is executing asynchronously, it ignores all other arguments.  
  
 While a function is executing asynchronously - that is, after it has returned SQL_STILL_EXECUTING and before it returns a different code - the application can cancel it by calling **SQLCancel** or **SQLCancelHandle** with the same statement handle. This is not guaranteed to cancel function execution. For example, the function might have already finished. Furthermore, the code returned by **SQLCancel** or **SQLCancelHandle** only indicates whether the attempt to cancel the function was successful, not whether it actually canceled the function. To determine whether the function was canceled, the application calls the function again. If the function was canceled, it returns SQL_ERROR and SQLSTATE HY008 (Operation canceled). If the function was not canceled, it returns another code, such as SQL_SUCCESS, SQL_STILL_EXECUTING, or SQL_ERROR with a different SQLSTATE.  
  
 To disable asynchronous execution of a particular statement when the driver supports statement-level asynchronous processing, the application calls **SQLSetStmtAttr** with the SQL_ATTR_ASYNC_ENABLE attribute and sets it to SQL_ASYNC_ENABLE_OFF. If the driver supports connection-level asynchronous processing, the application calls **SQLSetConnectAttr** to set SQL_ATTR_ASYNC_ENABLE to SQL_ASYNC_ENABLE_OFF, which disables asynchronous execution of all statements on the connection.  
  
 The application should process diagnostic records in the repeating loop of the original function. If **SQLGetDiagField** or **SQLGetDiagRec** is called when an asynchronous function is executing, it will return the current list of diagnostic records. Each time the original function call is repeated, it clears previous diagnostic records.  
  
## Executing Connection Operations Asynchronously  
 Before ODBC 3.8, asynchronous execution was allowed for statement-related operations such as prepare, execute, and fetch, as well as for catalog metadata operations. Starting in ODBC 3.8, asynchronous execution is also possible for connection-related operations such as connect, disconnect, commit, and rollback.  
  
 For more information on ODBC 3.8, see [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md).  
  
 Executing connection operations asynchronously is useful in the following scenarios:  
  
-   When a small number of threads manages a large number of devices with very high data rates. To maximize responsiveness and scalability it is desirable for all operations to be asynchronous.  
  
-   When you want to overlap database operations over multiple connections to reduce elapsed transfer times.  
  
-   Efficient asynchronous ODBC calls and the ability to cancel connection operations enable an application to allow the user to cancel any slow operation without having to wait for timeouts.  
  
 The following functions, which operate on connection handles, can now be executed asynchronously:  
  
||||  
|-|-|-|  
|[SQLBrowseConnect](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|[SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md)|[SQLDisconnect](../../../odbc/reference/syntax/sqldisconnect-function.md)|  
|[SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)|[SQLEndTran](../../../odbc/reference/syntax/sqlendtran-function.md)|[SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
  
 To determine whether a driver supports asynchronous operations on these functions, an application calls **SQLGetInfo** with SQL_ASYNC_DBC_FUNCTIONS. SQL_ASYNC_DBC_CAPABLE is returned if asynchronous operations are supported. SQL_ASYNC_DBC_NOT_CAPABLE is returned if asynchronous operations are not supported.  
  
 To specify that functions executed with a particular connection are to be executed asynchronously, the application calls **SQLSetConnectAttr** and sets the SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE attribute to SQL_ASYNC_DBC_ENABLE_ON. Setting a connection attribute before establishing a connection always executes synchronously. Also, the operation setting the connection attribute SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE with **SQLSetConnectAttr** always executes synchronously.  
  
 An application can enable asynchronous operation before making a connection. Because the Driver Manager cannot determine which driver to use before making a connection, the Driver Manager will always return success in **SQLSetConnectAttr**. However, it may fail to connect if the ODBC driver does not support asynchronous operations.  
  
 In general, there can be at most one asynchronously executing function associated with a particular connection handle or statement handle. However, a connection handle can have more than one associated statement handle. If there is no asynchronous operation executing on the connection handle, an associated statement handle can execute an asynchronous operation. Similarly, you can have an asynchronous operation on a connection handle if there are no asynchronous operations in progress on any associated statement handle. An attempt to execute an asynchronous operation using a handle that is currently executing an asynchronous operation will return HY010, "Function sequence error".  
  
 If a connection operation returns SQL_STILL_EXECUTING, an application can only call the original function and the following functions for that connection handle:  
  
-   **SQLCancelHandle** (on the connection handle)  
  
-   **SQLGetDiagField**  
  
-   **SQLGetDiagRec**  
  
-   **SQLAllocHandle** (allocating ENV/DBC)  
  
-   **SQLAllocHandleStd** (allocating ENV/DBC)  
  
-   **SQLGetEnvAttr**  
  
-   **SQLGetConnectAttr**  
  
-   **SQLDataSources**  
  
-   **SQLDrivers**  
  
-   **SQLGetInfo**  
  
-   **SQLGetFunctions**  
  
 The application should process diagnostic records in the repeating loop of the original function. If SQLGetDiagField or SQLGetDiagRec is called when an asynchronous function is executing, it will return the current list of diagnostic records. Each time the original function call is repeated, it clears previous diagnostic records.  
  
 If a connection is being opened or closed asynchronously, the operation is complete when the application receives SQL_SUCCESS or SQL_SUCCESS_WITH_INFO in the original function call.  
  
 A new function has been added to ODBC 3.8, **SQLCancelHandle**. This function cancels the six connection functions (**SQLBrowseConnect**, **SQLConnect**, **SQLDisconnect**, **SQLDriverConnect**, **SQLEndTran**, and **SQLSetConnectAttr**). An application should call **SQLGetFunctions** to determine if the driver supports **SQLCancelHandle**. As with **SQLCancel**, if **SQLCancelHandle** returns success, it does not mean the operation was canceled. An application should call the original function again to determine if the operation was canceled. **SQLCancelHandle** lets you cancel asynchronous operations on connection handles or statement handles. Using **SQLCancelHandle** to cancel an operation on a statement handle is the same as calling **SQLCancel**.  
  
 It is not necessary to support both **SQLCancelHandle** and asynchronous connection operations at the same time. A driver can support asynchronous connection operations but not **SQLCancelHandle**, or vice versa.  
  
 Asynchronous connection operations and **SQLCancelHandle** can also be used by ODBC 3.x and ODBC 2.x applications with an ODBC 3.8 driver and ODBC 3.8 Driver Manager. For information about for how to enable an older application to use new features in later ODBC version, see [Compatibility Matrix](../../../odbc/reference/develop-app/compatibility-matrix.md).  
  
### Connection Pooling  
 Whenever connection pooling is enabled, asynchronous operations are only minimally supported for establishing a connection (with **SQLConnect** and **SQLDriverConnect**) and closing a connection with **SQLDisconnect**. But an application should still be able to handle the SQL_STILL_EXECUTING return value from **SQLConnect**, **SQLDriverConnect**, and **SQLDisconnect**.  
  
 When connection pooling is enabled, **SQLEndTran** and **SQLSetConnectAttr** are supported for asynchronous operations.  
  
## Example  
  
### Description  
 The following example shows how to use **SQLSetConnectAttr** to enable asynchronous execution for connection-related functions.  
  
### Code  
  
```  
BOOL AsyncConnect (SQLHANDLE hdbc)   
{  
   SQLRETURN r;  
   SQLHANDLE hdbc;  
  
   // Enable asynchronous execution of connection functions.  
   // This must be executed synchronously, that is r != SQL_STILL_EXECUTING  
   r = SQLSetConnectAttr(  
         hdbc,   
         SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE,  
         reinterpret_cast<SQLPOINTER> (SQL_ASYNC_DBC_ENABLE_ON),  
         0);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO)   
   {  
      return FALSE;  
   }  
  
   TCHAR szConnStrIn[256] = _T("DSN=AsyncDemo");  
  
   r = SQLDriverConnect(hdbc, NULL, (SQLTCHAR *) szConnStrIn, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
  
   if (r == SQL_ERROR)   
   {  
      // Use SQLGetDiagRec to process the error.  
      // If SQLState is HY114, the driver does not support asynchronous execution.  
      return FALSE;  
   }  
  
   while (r == SQL_STILL_EXECUTING)   
   {  
      // Do something else.  
  
      // Check for completion, with the same set of arguments.  
      r = SQLDriverConnect(hdbc, NULL, (SQLTCHAR *) szConnStrIn, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
   }  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO)   
   {  
      return FALSE;  
   }  
  
   return TRUE;  
}  
  
```  
  
## Example  
  
### Description  
 This example shows asynchronous commit operations. Rollback operations can also be done this way.  
  
### Code  
  
```  
BOOL AsyncCommit ()   
{  
   SQLRETURN r;   
  
   // Assume that SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE is SQL_ASYNC_DBC_ENABLE_ON.  
  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   while (r == SQL_STILL_EXECUTING)   
   {  
      // Do something else.  
  
      // Check for completion with the same set of arguments.  
      r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   }  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO)   
   {  
      return FALSE;  
   }  
   return TRUE;  
}  
```  
  
## See Also  
 [Executing Statements ODBC](../../../odbc/reference/develop-app/executing-statements-odbc.md)
