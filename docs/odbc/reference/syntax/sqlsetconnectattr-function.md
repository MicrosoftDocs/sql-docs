---
title: "SQLSetConnectAttr Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLSetConnectAttr"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetConnectAttr"
helpviewer_keywords: 
  - "SQLSetConnectAttr function [ODBC]"
ms.assetid: 97fc7445-5a66-4eb9-8e77-10990b5fd685
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetConnectAttr Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLSetConnectAttr** sets attributes that govern aspects of connections.  
  
> [!NOTE]
>  For more information about what the Driver Manager maps this function to when an ODBC 3*.x* application is working with an ODBC 2*.x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```  
  
SQLRETURN SQLSetConnectAttr(  
     SQLHDBC       ConnectionHandle,  
     SQLINTEGER    Attribute,  
     SQLPOINTER    ValuePtr,  
     SQLINTEGER    StringLength);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *Attribute*  
 [Input] Attribute to set, listed in "Comments."  
  
 *ValuePtr*  
 [Input] Pointer to the value to be associated with *Attribute*. Depending on the value of *Attribute*, *ValuePtr* will be an unsigned integer value or will point to a null-terminated character string. Note that the integral type of the *Attribute* argument may not be fixed length, see the Comments section for detail.  
  
 *StringLength*  
 [Input] If *Attribute* is an ODBC-defined attribute and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of **ValuePtr*. For character string data, this argument should contain the number of bytes in the string.  
  
 If *Attribute* is an ODBC-defined attribute and *ValuePtr* is an integer, *StringLength* is ignored.  
  
 If *Attribute* is a driver-defined attribute, the application indicates the nature of the attribute to the Driver Manager by setting the *StringLength* argument. *StringLength* can have the following values:  
  
-   If *ValuePtr* is a pointer to a character string, then *StringLength* is the length of the string or SQL_NTS.  
  
-   If *ValuePtr* is a pointer to a binary buffer, then the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *StringLength*. This places a negative value in *StringLength*.  
  
-   If *ValuePtr* is a pointer to a value other than a character string or a binary string, then *StringLength* should have the value SQL_IS_POINTER.  
  
-   If *ValuePtr* contains a fixed-length value, then *StringLength* is either SQL_IS_INTEGER or SQL_IS_UINTEGER, as appropriate.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLSetConnectAttr** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLSetConnectAttr** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
 The driver can return SQL_SUCCESS_WITH_INFO to provide information about the result of setting an option.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|The driver did not support the value specified in *ValuePtr* and substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08002|Connection name in use|The *Attribute* argument was SQL_ATTR_ODBC_CURSORS, and the driver was already connected to the data source.|  
|08003|Connection not open|(DM) An *Attribute* value was specified that required an open connection, but the *ConnectionHandle* was not in a connected state.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|The *Attribute* argument was SQL_ATTR_CURRENT_CATALOG, and a result set was pending.|  
|25000|Illegal operation while in a local transaction|A connection was in a local transaction while attempting to enlist in a distributed transaction connection (DTC) by setting the connection attribute SQL_ATTR_ENLIST_IN_DTC.<br /><br /> A connection is already enlisted in a DTC.<br /><br /> A connection has been enlisted in a distributed transaction connection and a local transaction was started by setting SQL_ATTR_AUTOCOMMIT to SQL_AUTOCOMMIT_OFF.|  
|3D000|Invalid catalog name|The *Attribute* argument was SQL_CURRENT_CATALOG, and the specified catalog name was invalid.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *ConnectionHandle*. The **SQLSetConnectAttr** function was called, and before it completed execution, the [SQLCancelHandle function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) was called on the *ConnectionHandle*, and then the **SQLSetConnectAttr** function was called again on the *ConnectionHandle*.<br /><br /> Or, the  **SQLSetConnectAttr** function was called, and before it completed execution, **SQLCancelHandle** was called on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|The *Attribute* argument identified a connection attribute that required a string value, and the *ValuePtr* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for a *StatementHandle* associated with the *ConnectionHandle* and was still executing when **SQLSetConnectAttr** was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *ConnectionHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *ConnectionHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for a *StatementHandle* associated with the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) **SQLBrowseConnect** was called for the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before **SQLBrowseConnect** returned SQL_SUCCESS_WITH_INFO or SQL_SUCCESS.|  
|HY011|Attribute cannot be set now|The *Attribute* argument was SQL_ATTR_TXN_ISOLATION, and a transaction was open.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY024|Invalid attribute value|Given the specified *Attribute* value, an invalid value was specified in *ValuePtr*. (The Driver Manager returns this SQLSTATE only for connection and statement attributes that accept a discrete set of values, such as SQL_ATTR_ACCESS_MODE or SQL_ATTR_ASYNC_ENABLE. For all other connection and statement attributes, the driver must verify the value specified in *ValuePtr*.)<br /><br /> The *Attribute* argument was SQL_ATTR_TRACEFILE or SQL_ATTR_TRANSLATE_LIB, and *ValuePtr* was an empty string.|  
|HY090|Invalid string or buffer length|*(DM) \*ValuePtr* is a character string, and the *StringLength* argument was less than 0 but was not SQL_NTS.|  
|HY092|Invalid attribute/option identifier|(DM) The value specified for the argument *Attribute* was not valid for the version of ODBC supported by the driver.<br /><br /> (DM) The value specified for the argument *Attribute* was a read-only attribute.|  
|HY114|Driver does not support connection-level asynchronous function execution|(DM) An application attempted to enable asynchronous function execution with SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE for a driver that does not support asynchronous connection operations.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about the suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HY121|Cursor Library and Driver-Aware Pooling cannot be enabled at the same time|For more information, see [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md).|  
|HYC00|Optional feature not implemented|The value specified for the argument *Attribute* was a valid ODBC connection or statement attribute for the version of ODBC supported by the driver but was not supported by the driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *ConnectionHandle* does not support the function.|  
|IM009|Unable to load translation DLL|The driver was unable to load the translation DLL that was specified for the connection. This error can be returned only when *Attribute* is SQL_ATTR_TRANSLATE_LIB.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
|S1118|Driver does not support asynchronous notification|SQL_ATTR_ASYNC_DBC_EVENT was set (after the connection was made) but asynchronous notification is not supported by the driver.|  
  
 When *Attribute* is a statement attribute, **SQLSetConnectAttr** can return any SQLSTATEs returned by **SQLSetStmtAttr**.  
  
## Comments  
 For general information about connection attributes, see [Connection Attributes](../../../odbc/reference/develop-app/connection-attributes.md).  
  
 The currently defined attributes and the version of ODBC in which they were introduced are shown in the table later in this section; it is expected that more attributes will be defined to take advantage of different data sources. A range of attributes is reserved by ODBC; driver developers must reserve values for their own driver-specific use from Open Group.  
  
> [!NOTE]
>  The ability to set statement attributes at the connection level by calling **SQLSetConnectAttr** has been deprecated in ODBC 3*.x*. ODBC 3*.x* applications should never set statement attributes at the connection level. ODBC 3*.x* statement attributes cannot be set at the connection level, with the exception of the SQL_ATTR_METADATA_ID and SQL_ATTR_ASYNC_ENABLE attributes, which are both connection attributes and statement attributes and can be set at either the connection level or the statement level.  
> 
>  ODBC 3*.x* drivers need only support this functionality if they should work with ODBC 2*.x* applications that set ODBC 2*.x* statement options at the connection level. For more information, see [SQLSetConnectOption Mapping](../../../odbc/reference/appendixes/sqlsetconnectoption-mapping.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
 An application can call **SQLSetConnectAttr** at any time between the time the connection is allocated and freed. All connection and statement attributes successfully set by the application for the connection persist until **SQLFreeHandle** is called on the connection. For example, if an application calls **SQLSetConnectAttr** before connecting to a data source, the attribute persists even if **SQLSetConnectAttr** fails in the driver when the application connects to the data source; if an application sets a driver-specific attribute, the attribute persists even if the application connects to a different driver on the connection.  
  
 Some connection attributes can be set only before a connection has been made; others can be set only after a connection has been made. The following table indicates those connection attributes that must be set either before or after a connection has been made. *Either* indicates that the attribute can be set either before or after connection.  
  
|Attribute|Set before or after connection?|  
|---------------|-------------------------------------|  
|SQL_ATTR_ACCESS_MODE|Either[1]|  
|SQL_ATTR_ASYNC_DBC_EVENT|Either|  
|SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE|Either[4]|  
|SQL_ATTR_ASYNC_DBC_PCALLBACK|Either|  
|SQL_ATTR_ASYNC_DBC_PCONTEXT|Either|  
|SQL_ATTR_ASYNC_ENABLE|Either[2]|  
|SQL_ATTR_AUTO_IPD|Either|  
|SQL_ATTR_AUTOCOMMIT|Either[5]|  
|SQL_ATTR_CONNECTION_DEAD|After|  
|SQL_ATTR_CONNECTION_TIMEOUT|Either|  
|SQL_ATTR_CURRENT_CATALOG|Either[1]|  
|SQL_ATTR_DBC_INFO_TOKEN|After|  
|SQL_ATTR_ENLIST_IN_DTC|After|  
|SQL_ATTR_LOGIN_TIMEOUT|Before|  
|SQL_ATTR_METADATA_ID|Either|  
|SQL_ATTR_ODBC_CURSORS|Before|  
|SQL_ATTR_PACKET_SIZE|Before|  
|SQL_ATTR_QUIET_MODE|Either|  
|SQL_ATTR_TRACE|Either|  
|SQL_ATTR_TRACEFILE|Either|  
|SQL_ATTR_TRANSLATE_LIB|After|  
|SQL_ATTR_TRANSLATE_OPTION|After|  
|SQL_ATTR_TXN_ISOLATION|Either[3]|  
  
 [1]   SQL_ATTR_ACCESS_MODE and SQL_ATTR_CURRENT_CATALOG can be set before or after connecting, depending on the driver. However, interoperable applications set them before connecting because some drivers do not support changing these after connecting.  
  
 [2]   SQL_ATTR_ASYNC_ENABLE must be set before there is an active statement.  
  
 [3]   SQL_ATTR_TXN_ISOLATION can be set only if there are no open transactions on the connection. Some connection attributes support substitution of a similar value if the data source does not support the value specified in \**ValuePtr*. In such cases, the driver returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01S02 (Option value changed). For example, if *Attribute* is SQL_ATTR_PACKET_SIZE and \**ValuePtr* exceeds the maximum packet size, the driver substitutes the maximum size. To determine the substituted value, an application calls **SQLGetConnectAttr**.  
  
 [4]    If SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE is set before a connection is open, the Driver Manager will set the driver's attribute when the driver is loaded during a call to **SQLBrowseConnect**, **SQLConnect**, or **SQLDriverConnect**. Before a call to **SQLBrowseConnect**, **SQLConnect**, or **SQLDriverConnect**, the Driver Manager does not know which driver to connect to and does not know whether the driver supports asynchronous connection operations. Therefore, the Driver Manager always returns SQL_SUCCESS. But, in case the driver does not support asynchronous connection operations, the call to **SQLBrowseConnect**, **SQLConnect**, or **SQLDriverConnect** will fail.  
  
 [5]    When SQL_ATTR_AUTOCOMMIT is set to FALSE, applications should call SQLEndTran(SQL_ROLLBACK) if any API returns SQL_ERROR to ensure transactional consistency.  
  
 The format of information set in the \**ValuePtr* buffer depends on the specified *Attribute*. **SQLSetConnectAttr** will accept attribute information in one of two different formats: a null-terminated character string or an integer value. The format of each is noted in the attribute's description. Character strings pointed to by the *ValuePtr* argument of **SQLSetConnectAttr** have a length of *StringLength* bytes.  
  
 The *StringLength* argument is ignored if the length is defined by the attribute, as is the case for all attributes introduced in ODBC 2*.x* or earlier.  
  
|*Attribute*|*ValuePtr* contents|  
|-----------------|-------------------------|  
|SQL_ATTR_ACCESS_MODE (ODBC 1.0)|An SQLUINTEGER value. SQL_MODE_READ_ONLY is used by the driver or data source as an indicator that the connection is not required to support SQL statements that cause updates to occur. This mode can be used to optimize locking strategies, transaction management, or other areas as appropriate to the driver or data source. The driver is not required to prevent such statements from being submitted to the data source. The behavior of the driver and data source when asked to process SQL statements that are not read-only during a read-only connection is implementation-defined. SQL_MODE_READ_WRITE is the default.|  
|SQL_ATTR_ASYNC_DBC_EVENT (ODBC 3.8)|A SQLPOINTER value that is an event handle.<br /><br /> Notification of the completion of asynchronous functions is enabled by calling **SQLSetConnectAttr** with the SQL_ATTR_ASYNC_STMT_EVENT attribute and specifying the event handle. **Note:**  The notification method is not supported with cursor library. An application will receive error message if it attempts to enable cursor library via SQLSetConnectAttr, when the notification method is enabled.|  
|SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE (ODBC 3.8)|A SQLUINTEGER value that enables or disables asynchronous execution of selected functions on the connection handle. For more information, see [Asynchronous Execution (Polling Method)](../../../odbc/reference/develop-app/asynchronous-execution-polling-method.md).<br /><br /> SQL_ASYNC_DBC_ENABLE_ON = Enable asynchronous operation for specified connection-related functions.<br /><br /> SQL_ASYNC_DBC_ENABLE_OFF = (Default) Disable asynchronous operation for specified connection-related functions.<br /><br /> Setting SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE is always synchronous (that is, it will never return SQL_STILL_EXECUTING).<br /><br /> Asynchronous execution of statement operations are enabled with SQL_ATTR_ASYNC_ENABLE.|  
|SQL_ATTR_ASYNC_DBC_PCALLBACK (ODBC 3.8)|A SQLPOINTER value that points to context structure.<br /><br /> Only the Driver Manager can call a driver's **SQLSetStmtAttr** function with this attribute.|  
|SQL_ATTR_ASYNC_DBC_PCONTEXT (ODBC 3.8)|A SQLPOINTER value that points to the context structure.<br /><br /> Only the Driver Manager can call a driver's **SQLSetStmtAttr** function with this attribute.|  
|SQL_ATTR_ASYNC_ENABLE (ODBC 3.0)|A SQLULEN value that specifies whether a function called with a statement on the specified connection is executed asynchronously:<br /><br /> SQL_ASYNC_ENABLE_OFF = Disable connection level asynchronous execution support for statement operations (the default).<br /><br /> SQL_ASYNC_ENABLE_ON = Enable connection level asynchronous execution support for statement operations.<br /><br /> This attribute can be set whether **SQLGetInfo** with the SQL_ASYNC_MODE information type returns SQL_AM_CONNECTION or SQL_AM_STATEMENT.|  
|SQL_ATTR_AUTO_IPD (ODBC 3.0)|A read-only SQLUINTEGER value that specifies whether automatic population of the IPD after a call to **SQLPrepare** is supported:<br /><br /> SQL_TRUE = Automatic population of the IPD after a call to **SQLPrepare** is supported by the driver.<br /><br /> SQL_FALSE = Automatic population of the IPD after a call to **SQLPrepare** is not supported by the driver. Servers that do not support prepared statements will not be able to populate the IPD automatically.<br /><br /> If SQL_TRUE is returned for the SQL_ATTR_AUTO_IPD connection attribute, the statement attribute SQL_ATTR_ENABLE_AUTO_IPD can be set to turn automatic population of the IPD on or off. If SQL_ATTR_AUTO_IPD is SQL_FALSE, SQL_ATTR_ENABLE_AUTO_IPD cannot be set to SQL_TRUE. The default value of SQL_ATTR_ENABLE_AUTO_IPD is equal to the value of SQL_ATTR_AUTO_IPD.<br /><br /> This connection attribute can be returned by **SQLGetConnectAttr** but cannot be set by **SQLSetConnectAttr**.|  
|SQL_ATTR_AUTOCOMMIT (ODBC 1.0)|A SQLUINTEGER value that specifies whether to use autocommit or manual-commit mode:<br /><br /> SQL_AUTOCOMMIT_OFF = The driver uses manual-commit mode, and the application must explicitly commit or roll back transactions with **SQLEndTran**.<br /><br /> SQL_AUTOCOMMIT_ON = The driver uses autocommit mode. Each statement is committed immediately after it is executed. This is the default. Any open transactions on the connection are committed when SQL_ATTR_AUTOCOMMIT is set to SQL_AUTOCOMMIT_ON to change from manual-commit mode to autocommit mode.<br /><br /> For more information, see [Commit Mode](../../../odbc/reference/develop-app/commit-mode.md). **Important:**  Some data sources delete the access plans and close the cursors for all statements on a connection each time a statement is committed; autocommit mode can cause this to happen after each nonquery statement is executed or when the cursor is closed for a query. For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR information types in [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) and [Effect of Transactions on Cursors and Prepared Statements](../../../odbc/reference/develop-app/effect-of-transactions-on-cursors-and-prepared-statements.md). <br /><br /> When a batch is executed in autocommit mode, two things are possible. The entire batch can be treated as an autocommitable unit, or each statement in a batch is treated as an autocommitable unit. Certain data sources can support both these behaviors and may provide a way of choosing one or the other. It is driver-defined whether a batch is treated as an autocommitable unit or whether each individual statement within the batch is autocommitable.|  
|SQL_ATTR_CONNECTION_DEAD<br /><br /> (ODBC 3.5)|A read-only SQLUINTEGER value that indicates the state of the connection. If SQL_CD_TRUE, the connection has been lost. If SQL_CD_FALSE, the connection is still active.|  
|SQL_ATTR_CONNECTION_TIMEOUT (ODBC 3.0)|An SQLUINTEGER value corresponding to the number of seconds to wait for any request on the connection to complete before returning to the application. The driver should return SQLSTATE HYT00 (Timeout expired) anytime that it is possible to time out in a situation not associated with query execution or login.<br /><br /> If *ValuePtr* is equal to 0 (the default), there is no timeout.|  
|SQL_ATTR_CURRENT_CATALOG (ODBC 2.0)|A character string containing the name of the catalog to be used by the data source. For example, in SQL Server, the catalog is a database, so the driver sends a **USE** _database_ statement to the data source, where *database* is the database specified in \**ValuePtr*. For a single-tier driver, the catalog might be a directory, so the driver changes its current directory to the directory specified in **ValuePtr*.|  
|SQL_ATTR_DBC_INFO_TOKEN (ODBC 3.8|A SQLPOINTER value used to set back the connection info token into the DBC handle when [SQLRateConnection](../../../odbc/reference/syntax/sqlrateconnection-function.md)'s (\**pRating*) parameter is not equal to 100.<br /><br /> SQL_ATTR_DBC_INFO_TOKEN is set-only. It is not possible to use **SQLGetConnectAttr** or **SQLGetConnectOption** to retrieve this value. The Driver Manager's **SQLSetConnectAttr** will not accept SQL_ATTR_DBC_INFO_TOKEN, since an application should not set this attribute.<br /><br /> If a driver returns SQL_ERROR after setting SQL_ATTR_DBC_INFO_TOKEN, the connection just obtained from the pool will be freed. The Driver Manager will then try to obtain another connection from the pool. See [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md) for more information.|  
|SQL_ATTR_ENLIST_IN_DTC (ODBC 3.0)|A SQLPOINTER value that specifies whether to use the ODBC driver in distributed transactions coordinated by Microsoft Component Services.<br /><br /> Pass a DTC OLE transaction object that specifies the transaction to export to SQL Server, or SQL_DTC_DONE to end the connection's DTC association.<br /><br /> The client calls the Microsoft Distributed Transaction Coordinator (MS DTC) OLE ITransactionDispenser::BeginTransaction method to begin an MS DTC transaction and create an MS DTC transaction object that represents the transaction. The application then calls SQLSetConnectAttr with the SQL_ATTR_ENLIST_IN_DTC option to associate the transaction object with the ODBC connection. All related database activity will be performed under the protection of the MS DTC transaction. The application calls SQLSetConnectAttr with SQL_DTC_DONE to end the connection's DTC association. For more information, see the MS DTC documentation.|  
|SQL_ATTR_LOGIN_TIMEOUT (ODBC 1.0)|An SQLUINTEGER value corresponding to the number of seconds to wait for a login request to complete before returning to the application. The default is driver-dependent. If *ValuePtr* is 0, the timeout is disabled and a connection attempt will wait indefinitely.<br /><br /> If the specified timeout exceeds the maximum login timeout in the data source, the driver substitutes that value and returns SQLSTATE 01S02 (Option value changed).|  
|SQL_ATTR_METADATA_ID (ODBC 3.0)|An SQLUINTEGER value that determines how the string arguments of catalog functions are treated.<br /><br /> If SQL_TRUE, the string argument of catalog functions are treated as identifiers. The case is not significant. For nondelimited strings, the driver removes any trailing spaces and the string is folded to uppercase. For delimited strings, the driver removes any leading or trailing spaces and takes literally whatever is between the delimiters. If one of these arguments is set to a null pointer, the function returns SQL_ERROR and SQLSTATE HY009 (Invalid use of null pointer).<br /><br /> If SQL_FALSE, the string arguments of catalog functions are not treated as identifiers. The case is significant. They can either contain a string search pattern or not, depending on the argument.<br /><br /> The default value is SQL_FALSE.<br /><br /> The *TableType* argument of **SQLTables**, which takes a list of values, is not affected by this attribute.<br /><br /> SQL_ATTR_METADATA_ID can also be set on the statement level. (It is the only connection attribute that is also a statement attribute.)<br /><br /> For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).|  
|SQL_ATTR_ODBC_CURSORS (ODBC 2.0)|An SQLULEN  value specifying how the Driver Manager uses the ODBC cursor library:<br /><br /> SQL_CUR_USE_IF_NEEDED = The Driver Manager uses the ODBC cursor library only if it is needed. If the driver supports the SQL_FETCH_PRIOR option in **SQLFetchScroll**, the Driver Manager uses the scrolling capabilities of the driver. Otherwise, it uses the ODBC cursor library.<br /><br /> SQL_CUR_USE_ODBC = The Driver Manager uses the ODBC cursor library.<br /><br /> SQL_CUR_USE_DRIVER = The Driver Manager uses the scrolling capabilities of the driver. This is the default setting.<br /><br /> For more information about the ODBC cursor library, see [Appendix F: ODBC Cursor Library](../../../odbc/reference/appendixes/appendix-f-odbc-cursor-library.md). **Warning:**  The cursor library will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.|  
|SQL_ATTR_PACKET_SIZE (ODBC 2.0)|An SQLUINTEGER value specifying the network packet size in bytes. **Note:**  Many data sources either do not support this option or only can return but not set the network packet size. <br /><br /> If the specified size exceeds the maximum packet size or is smaller than the minimum packet size, the driver substitutes that value and returns SQLSTATE 01S02 (Option value changed).<br /><br /> If the application sets packet size after a connection has already been made, the driver will return SQLSTATE HY011 (Attribute cannot be set now).|  
|SQL_ATTR_QUIET_MODE (ODBC 2.0)|A window handle (HWND).<br /><br /> If the window handle is a null pointer, the driver does not display any dialog boxes.<br /><br /> If the window handle is not a null pointer, it should be the parent window handle of the application. This is the default. The driver uses this handle to display dialog boxes. **Note:**  The SQL_ATTR_QUIET_MODE connection attribute does not apply to dialog boxes displayed by **SQLDriverConnect**.|  
|SQL_ATTR_TRACE (ODBC 1.0)|An SQLUINTEGER value telling the Driver Manager whether to perform tracing:<br /><br /> SQL_OPT_TRACE_OFF = Tracing off (the default)<br /><br /> SQL_OPT_TRACE_ON = Tracing on<br /><br /> When tracing is on, the Driver Manager writes each ODBC function call to the trace file. **Note:**  When tracing is on, the Driver Manager can return SQLSTATE IM013 (Trace file error) from any function. <br /><br /> An application specifies a trace file with the SQL_ATTR_TRACEFILE option. If the file already exists, the Driver Manager appends to the file. Otherwise, it creates the file. If tracing is on and no trace file has been specified, the Driver Manager writes to the file SQL.LOG in the root directory.<br /><br /> An application can set the variable **ODBCSharedTraceFlag** to enable tracing dynamically. Tracing is then enabled for all ODBC applications currently running. If an application turns tracing off, it is turned off only for that application.<br /><br /> If the **Trace** keyword in the system information is set to 1 when an application calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV, tracing is enabled for all handles. It is enabled only for the application that called **SQLAllocHandle**.<br /><br /> Calling **SQLSetConnectAttr** with an *Attribute* of SQL_ATTR_TRACE does not require that the *ConnectionHandle* argument be valid and will not return SQL_ERROR if *ConnectionHandle* is NULL. This attribute applies to all connections.|  
|SQL_ATTR_TRACEFILE (ODBC 1.0)|A null-terminated character string containing the name of the trace file.<br /><br /> The default value of the SQL_ATTR_TRACEFILE attribute is specified with the **TraceFile** keyword in the system information. For more information, see [ODBC Subkey](../../../odbc/reference/install/odbc-subkey.md).<br /><br /> Calling **SQLSetConnectAttr** with an *Attribute* of SQL_ATTR_ TRACEFILE does not require the *ConnectionHandle* argument to be valid and will not return SQL_ERROR if *ConnectionHandle* is invalid. This attribute applies to all connections.|  
|SQL_ATTR_TRANSLATE_LIB (ODBC 1.0)|A null-terminated character string containing the name of a library containing the functions **SQLDriverToDataSource** and **SQLDataSourceToDriver** that the driver accesses to perform tasks such as character set translation. This option may be specified only if the driver has connected to the data source. The setting of this attribute will persist across connections. For more information about translating data, see [Translation DLLs](../../../odbc/reference/develop-app/translation-dlls.md) and [Translation DLL Function Reference](../../../odbc/reference/syntax/translation-dll-api-reference.md).|  
|SQL_ATTR_TRANSLATE_OPTION (ODBC 1.0)|A 32-bit flag value that is passed to the translation DLL. This attribute can be specified only if the driver has connected to the data source. For information about translating data, see [Translation DLLs](../../../odbc/reference/develop-app/translation-dlls.md).|  
|SQL_ATTR_TXN_ISOLATION (ODBC 1.0)|A 32-bit bitmask that sets the transaction isolation level for the current connection. An application must call **SQLEndTran** to commit or roll back all open transactions on a connection, before calling **SQLSetConnectAttr** with this option.<br /><br /> The valid values for *ValuePtr* can be determined by calling **SQLGetInfo** with *InfoType* equal to SQL_TXN_ISOLATION_OPTIONS.<br /><br /> For a description of transaction isolation levels, see the description of the SQL_DEFAULT_TXN_ISOLATION information type in [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) and [Transaction Isolation Levels](../../../odbc/reference/develop-app/transaction-isolation-levels.md).|  
  
 [1]   These functions can be called asynchronously only if the descriptor is an implementation descriptor, not an application descriptor.  
  
## Code Example  
 See [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Returning the setting of a connection  attribute|[SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
