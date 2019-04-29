---
title: "SQLConnect Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLConnect"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLConnect"
helpviewer_keywords: 
  - "SQLConnect function [ODBC]"
ms.assetid: 59075e46-a0ca-47bf-972a-367b08bb518d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConnect Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLConnect** establishes connections to a driver and a data source. The connection handle references storage of all information about the connection to the data source, including status, transaction state, and error information.  
  
## Syntax  
  
```  
  
SQLRETURN SQLConnect(  
     SQLHDBC        ConnectionHandle,  
     SQLCHAR *      ServerName,  
     SQLSMALLINT    NameLength1,  
     SQLCHAR *      UserName,  
     SQLSMALLINT    NameLength2,  
     SQLCHAR *      Authentication,  
     SQLSMALLINT    NameLength3);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *ServerName*  
 [Input] Data source name. The data might be located on the same computer as the program, or on another computer somewhere on a network. For information about how an application chooses a data source, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
 *NameLength1*  
 [Input] Length of **ServerName* in characters.  
  
 *UserName*  
 [Input] User identifier.  
  
 *NameLength2*  
 [Input] Length of **UserName* in characters.  
  
 *Authentication*  
 [Input] Authentication string (typically the password).  
  
 *NameLength3*  
 [Input] Length of **Authentication* in characters.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLConnect** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values typically returned by **SQLConnect** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|The driver did not support the specified value of the *ValuePtr* argument in **SQLSetConnectAttr** and substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08001|Client unable to establish connection|The driver was unable to establish a connection with the data source.|  
|08002|Connection name in use|(DM) The specified *ConnectionHandle* had already been used to establish a connection with a data source, and the connection was still open or the user was browsing for a connection.|  
|08004|Server rejected the connection|The data source rejected the establishment of the connection for implementation-defined reasons.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was trying to connect failed before the function completed processing.|  
|28000|Invalid authorization specification|The value specified for the argument *UserName* or the value specified for the argument *Authentication* violated restrictions defined by the data source.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|(DM) The Driver Manager was unable to allocate memory that is required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *ConnectionHandle*. The **SQLConnect** function was called, and before it completed execution, [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) was called on the *ConnectionHandle*, and then the **SQLConnect** function was called again on the *ConnectionHandle*.<br /><br /> Or, the **SQLConnect** function was called, and before it completed execution, **SQLCancelHandle** was called on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function (not this one) was called for the *ConnectionHandle* and was still executing when this function was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *NameLength1*, *NameLength2*, or *NameLength3* was less than 0 but not equal to SQL_NTS.<br /><br /> (DM) The value specified for argument *NameLength1* exceeded the maximum length for a data source name.|  
|HYT00|Timeout expired|The query timeout period expired before the connection to the data source completed. The timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_LOGIN_TIMEOUT.|  
|HY114|Driver does not support connection level asynchronous function execution|(DM) The application enabled the asynchronous operation on the connection handle before making the connection. However, the driver does not support asynchronous operations on the connection handle.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver specified by the data source name does not support the function.|  
|IM002|Data source not found and no default driver specified|(DM) The data source name specified in the argument *ServerName* was not found in the system information, nor was there a default driver specification.|  
|IM003|Specified driver could not be connected to|(DM) The driver listed in the data source specification in system information was not found or could not be connected to for some other reason.|  
|IM004|Driver's SQLAllocHandle on SQL_HANDLE_ENV failed|(DM) During **SQLConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with a *HandleType* of SQL_HANDLE_ENV and the driver returned an error.|  
|IM005|Driver's SQLAllocHandle on SQL_HANDLE_DBC failed|(DM) During **SQLConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with a *HandleType* of SQL_HANDLE_DBC and the driver returned an error.|  
|IM006|Driver's SQLSetConnectAttr failed|During **SQLConnect**, the Driver Manager called the driver's **SQLSetConnectAttr** function and the driver returned an error. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|IM009|Unable to connect to translation DLL|The driver was unable to connect to the translation DLL that was specified for the data source.|  
|IM010|Data source name too long|(DM) *\*ServerName* was longer than SQL_MAX_DSN_LENGTH characters.|  
|IM014|The specified DSN contains an architecture mismatch between the Driver and Application|(DM) 32-bit application uses a DSN connecting to a 64-bit driver; or vice versa.|  
|IM015|Driver's SQLConnect on SQL_HANDLE_DBC_INFO_HANDLE failed|If a driver returns SQL_ERROR, the Driver Manager will return SQL_ERROR to the application and the connection will fail.<br /><br /> For more information about SQL_HANDLE_DBC_INFO_TOKEN, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
|S1118|Driver does not support asynchronous notification|When the driver does not support asynchronous notification, you cannot set SQL_ATTR_ASYNC_DBC_EVENT or SQL_ATTR_ASYNC_DBC_RETCODE_PTR.|  
  
## Comments  
 For information about why an application uses **SQLConnect**, see [Connecting with SQLConnect](../../../odbc/reference/develop-app/connecting-with-sqlconnect.md).  
  
 The Driver Manager does not connect to a driver until the application calls a function (**SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**) to connect to the driver. Until that point, the Driver Manager works with its own handles and manages connection information. When the application calls a connection function, the Driver Manager checks whether a driver is currently connected to for the specified *ConnectionHandle*:  
  
-   If a driver is not connected to, the Driver Manager connects to the driver and calls **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV, **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC, **SQLSetConnectAttr** (if the application specified any connection attributes), and the connection function in the driver. The Driver Manager returns SQLSTATE IM006 (Driver's **SQLSetConnectOption** failed) and SQL_SUCCESS_WITH_INFO for the connection function if the driver returned an error for **SQLSetConnectAttr**. For more information, see [Connecting to a Data Source or Driver](../../../odbc/reference/develop-app/connecting-to-a-data-source-or-driver.md).  
  
-   If the specified driver is already connected to on the *ConnectionHandle*, the Driver Manager calls only the connection function in the driver. In this case, the driver must make sure that all connection attributes for the *ConnectionHandle* maintain their current settings.  
  
-   If a different driver is connected to, the Driver Manager calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC, and then, if no other driver is connected to in that environment, it calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_ENV in the connected driver and then disconnects that driver. It then performs the same operations as when a driver is not connected to.  
  
 The driver then allocates handles and initializes itself.  
  
 When the application calls **SQLDisconnect**, the Driver Manager calls **SQLDisconnect** in the driver. However, it does not disconnect the driver. This keeps the driver in memory for applications that repeatedly connect to and disconnect from a data source. When the application calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC, the Driver Manager calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_DBC and then **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_ENV in the driver, and then disconnects the driver.  
  
 An ODBC application can establish more than one connection.  
  
## Driver Manager Guidelines  
 The contents of **ServerName* affect how the Driver Manager and a driver work together to establish a connection to a data source.  
  
-   If \**ServerName* contains a valid data source name, the Driver Manager locates the corresponding data source specification in the system information and connects to the associated driver. The Driver Manager passes each **SQLConnect** argument to the driver.  
  
-   If the data source name cannot be found or *ServerName* is a null pointer, the Driver Manager locates the default data source specification and connects to the associated driver. The Driver Manager passes to the driver the *UserName* and *Authentication* arguments unmodified, and "DEFAULT" for the *ServerName* argument.  
  
-   If the *ServerName* argument is "DEFAULT", the Driver Manager locates the default data source specification and connects to the associated driver. The Driver Manager passes each **SQLConnect** argument to the driver.  
  
-   If the data source name cannot be found or *ServerName* is a null pointer, and the default data source specification does not exist, the Driver Manager returns SQL_ERROR with SQLSTATE IM002 (Data source name not found and no default driver specified).  
  
 After it is connected to by the Driver Manager, a driver can locate its corresponding data source specification in the system information and use driver-specific information from the specification to complete its set of required connection information.  
  
 If a default translation library is specified in the system information for the data source, the driver connects to it. A different translation library can be connected to by calling **SQLSetConnectAttr** with the SQL_ATTR_TRANSLATE_LIB attribute. A translation option can be specified by calling **SQLSetConnectAttr** with the SQL_ATTR_TRANSLATE_OPTION attribute.  
  
 If a driver supports **SQLConnect**, the driver keyword section of the system information for the driver must contain the **ConnectFunctions** keyword with the first character set to "Y."  
  
### Connection Pooling  
 Connection pooling allows an application to reuse a connection that has already been created. When connection pooling is enabled and **SQLConnect** is called, the Driver Manager tries to make the connection using a connection that is part of a pool of connections in an environment that has been designated for connection pooling. This environment is a shared environment that is used by all applications that use the connections in the pool.  
  
 Connection pooling is enabled before the environment is allocated by calling **SQLSetEnvAttr** to set SQL_ATTR_CONNECTION_POOLING to SQL_CP_ONE_PER_DRIVER (which specifies a maximum of one pool per driver) or SQL_CP_ONE_PER_HENV (which specifies a maximum of one pool per environment). **SQLSetEnvAttr** in this case is called with *EnvironmentHandle* set to null, which makes the attribute a process-level attribute. If SQL_ATTR_CONNECTION_POOLING is set to SQL_CP_OFF, connection pooling is disabled.  
  
 After connection pooling has been enabled, **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV is called to allocate an environment. The environment allocated by this call is a shared environment because connection pooling has been enabled. However, the environment that will be used is not determined until **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC is called.  
  
 **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC is called to allocate a connection. The Driver Manager tries to find an existing shared environment that matches the environment attributes set by the application. If no such environment exists, one is created as an implicit *shared environment*. If a matching shared environment is found, the environment handle is returned to the application and its reference count is incremented.  
  
 However, the connection that will be used is not determined until **SQLConnect** is called. At that point, the Driver Manager tries to find an existing connection in the connection pool that matches the criteria requested by the application. These criteria include the connection options requested in the call to **SQLConnect** (the values of the *ServerName*, *UserName*, and *Authentication* keywords) and any connection attributes set since **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC was called. The Driver Manager checks these criteria against the corresponding connection keywords and attributes in connections in the pool. If a match is found, the connection in the pool is used. If no match is found, a new connection is created.  
  
 If the SQL_ATTR_CP_MATCH environment attribute is set to SQL_CP_STRICT_MATCH, the match must be exact for a connection in the pool to be used. If the SQL_ATTR_CP_MATCH environment attribute is set to SQL_CP_RELAXED_MATCH, the connection options in the call to **SQLConnect** must match but not all the connection attributes must match.  
  
 The following rules are applied when a connection attribute, as set by the application before **SQLConnect** is called, does not match the connection attribute of the connection in the pool:  
  
-   If the connection attribute must be set before the connection is made:  
  
     If SQL_ATTR_CP_MATCH is SQL_CP_STRICT_MATCH, SQL_ATTR_PACKET_SIZE in the pooled connection must be identical to the attribute set by the application. If SQL_CP_RELAXED_MATCH, the values of SQL_ATTR_PACKET_SIZE can be different.  
  
     The value of SQL_ATTR_LOGIN_VALUE does not affect the match.  
  
-   If the connection attribute can be set either before or after the connection is made:  
  
     If the connection attribute has not been set by the application but has been set on the connection in the pool, and there is a default, the connection attribute in the pooled connection is set back to the default and a match is declared. If there is no default, the pooled connection is not considered a match.  
  
     If the connection attribute has been set by the application but has not been set on the connection in the pool, the connection attribute on the pool is changed to that set by the application and a match is declared.  
  
     If the connection attribute has been set by the application, and has also been set on the connection in the pool but the values are different, the value of the application's connection attribute is used and a match is declared.  
  
-   If the values of driver-specific connection attributes are not identical and SQL_ATTR_CP_MATCH is set to SQL_CP_STRICT_MATCH, the connection in the pool is not used.  
  
 When the application calls **SQLDisconnect** to disconnect, the connection is returned to the connection pool and is available for reuse.  
  
### Optimizing Connection Pooling Performance  
 When distributed transactions are involved, it is possible to optimize connection pooling performance by using **SQL_DTC_TRANSITION_COST**, which is a SQLUINTEGER bitmask. The transitions referred to are the transitions of the connection attribute SQL_ATTR_ENLIST_IN_DTC going from value 0 to nonzero, and vice versa. This is a connection going from not enlisted in a distributed transaction to enlisted in a distributed transaction, and vice versa. Depending on how the driver has implemented enlistment (setting connection attribute SQL_ATTR_ENLIST_IN_DTC), these transitions may be expensive and should therefore be avoided for best performance.  
  
 The value returned by the driver contains any combination of the following bits:  
  
-   **SQL_DTC_ENLIST_EXPENSIVE**, when set, implies the zero to nonzero transition is significantly more expensive than a transition from nonzero to another nonzero value (enlisting a previously enlisted connection in its next transaction).  
  
-   **SQL_DTC_UNENLIST_EXPENSIVE**, when set, implies the nonzero to zero transition is significantly more expensive than using a connection whose SQL_ATTR_ENLIST_IN_DTC attribute is already set to zero.  
  
 There is a performance versus connection usage tradeoff. If a driver indicates that one or more of these transitions are expensive, the driver manager's connection pooler responds to this by keeping more connections in the pool. Some of the connections in the pool are preferred for nontransactional use, and some are preferred for transactional use. However, if the driver indicates that these transitions are not expensive, fewer connections can be used, perhaps alternating between nontransactional and transactional use.  
  
 Drivers that do not support SQL_ATTR_ENLIST_IN_DTC do not need to support SQL_DTC_TRANSITION_COST. For drivers that support SQL_ATTR_ENLIST_IN_DTC but not SQL_DTC_TRANSITION_COST, it is assumed that the transitions are not expensive, as if the driver returned 0 (no bits set) for this value.  
  
 Although SQL_DTC_TRANSITION_COST was introduced in ODBC 3.5, an ODBC 2.*x* driver can also support it because the driver manager will query this information regardless of the driver version.  
  
### Code Example  
 In the following example, an application allocates environment and connection handles. It then connects to the SalesOrders data source with the user ID JohnS and the password Sesame and processes data. When it has finished processing data, it disconnects from the data source and frees the handles.  
  
```  
// SQLConnect_ref.cpp  
// compile with: odbc32.lib  
#include <windows.h>  
#include <sqlext.h>  
  
int main() {  
   SQLHENV henv;  
   SQLHDBC hdbc;  
   SQLHSTMT hstmt;  
   SQLRETURN retcode;  
  
   SQLCHAR * OutConnStr = (SQLCHAR * )malloc(255);  
   SQLSMALLINT * OutConnStrLen = (SQLSMALLINT *)malloc(255);  
  
   // Allocate environment handle  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set the ODBC version environment attribute  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (void*)SQL_OV_ODBC3, 0);   
  
      // Allocate connection handle  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
  
         // Set login timeout to 5 seconds  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
            // Connect to data source  
            retcode = SQLConnect(hdbc, (SQLCHAR*) "NorthWind", SQL_NTS, (SQLCHAR*) NULL, 0, NULL, 0);  
  
            // Allocate statement handle  
            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
               retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);   
  
               // Process data  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
                  SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
               }  
  
               SQLDisconnect(hdbc);  
            }  
  
            SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
         }  
      }  
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
   }  
}  
```  
  
### Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Discovering and enumerating values required to connect to a data source|[SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|  
|Disconnecting from a data source|[SQLDisconnect Function](../../../odbc/reference/syntax/sqldisconnect-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
|Returning the setting of a connection attribute|[SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
