---
title: "SQLBrowseConnect Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLBrowseConnect"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll" 
apitype: "dllExport"
f1_keywords: 
  - "SQLBrowseConnect"
helpviewer_keywords: 
  - "SQLBrowseConnect function [ODBC]"
ms.assetid: b7f1be66-e6c7-4790-88ec-62b7662103c0
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLBrowseConnect Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLBrowseConnect** supports an iterative method of discovering and enumerating the attributes and attribute values required to connect to a data source. Each call to **SQLBrowseConnect** returns successive levels of attributes and attribute values. When all levels have been enumerated, a connection to the data source is completed and a complete connection string is returned by **SQLBrowseConnect**. A return code of SQL_SUCCESS or SQL_SUCCESS_WITH_INFO indicates that all connection information has been specified and the application is now connected to the data source.  
  
## Syntax  
  
```  
  
SQLRETURN SQLBrowseConnect(  
     SQLHDBC         ConnectionHandle,  
     SQLCHAR *       InConnectionString,  
     SQLSMALLINT     StringLength1,  
     SQLCHAR *       OutConnectionString,  
     SQLSMALLINT     BufferLength,  
     SQLSMALLINT *   StringLength2Ptr);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *InConnectionString*  
 [Input] Browse request connection string (see "*InConnectionString* Argument" in "Comments").  
  
 *StringLength1*  
 [Input] Length of **InConnectionString* in characters.  
  
 *OutConnectionString*  
 [Output] Pointer to a character buffer in which to return the browse result connection string (see "*OutConnectionString* Argument" in "Comments").  
  
 If *OutConnectionString* is NULL, *StringLength2Ptr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *OutConnectionString*.  
  
 *BufferLength*  
 [Input] Length, in characters, of the **OutConnectionString* buffer.  
  
 *StringLength2Ptr*  
 [Output] The total number of characters (excluding null-termination) available to return in \**OutConnectionString*. If the number of characters available to return is greater than or equal to *BufferLength*, the connection string in \**OutConnectionString* is truncated to *BufferLength* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NEED_DATA, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLBrowseConnect** returns SQL_ERROR, SQL_SUCCESS_WITH_INFO, or SQL_NEED_DATA, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle of ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLBrowseConnect** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**OutConnectionString* was not large enough to return the entire browse result connection string, so the string was truncated. The buffer **StringLength2Ptr* contains the length of the untruncated browse result connection string. (Function returns SQL_NEED_DATA.)|  
|01S00|Invalid connection string attribute|An invalid attribute keyword was specified in the browse request connection string (*InConnectionString*). (Function returns SQL_NEED_DATA.)<br /><br /> An attribute keyword was specified in the browse request connection string (*InConnectionString*) that does not apply to the current connection level. (Function returns SQL_NEED_DATA.)|  
|01S02|Value changed|The driver did not support the specified value of the *ValuePtr* argument in **SQLSetConnectAttr** and substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08001|Client unable to establish connection|The driver was unable to establish a connection with the data source.|  
|08002|Connection name in use|(DM) The specified connection had already been used to establish a connection with a data source, and the connection was open.|  
|08004|Server rejected the connection|The data source rejected the establishment of the connection for implementation-defined reasons.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was attempting to connect failed before the function completed processing.|  
|28000|Invalid authorization specification|Either the user identifier or the authorization string or both, as specified in the browse request connection string (*InConnectionString*), violated restrictions defined by the data source.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|(DM) The Driver Manager was unable to allocate memory required to support execution or completion of the function.<br /><br /> The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|An asynchronous operation was canceled by calling [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md). Then, the original function was called again on the *ConnectionHandle*.<br /><br /> An operation was canceled by calling **SQLCancelHandle** on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function (not this one) was called for the *ConnectionHandle* and was still executing when this function was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *StringLength1* was less than 0 and was not equal to SQL_NTS.<br /><br /> (DM) The value specified for argument *BufferLength* was less than 0.|  
|HY114|Driver does not support connection level asynchronous function execution|(DM) The application enabled the asynchronous operation on the connection handle before making the connection. However, the driver does not support asynchronous operation on connection handle.|  
|HYT00|Timeout expired|The login timeout period expired before the connection to the data source completed. The timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_LOGIN_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the specified data source name does not support the function.|  
|IM002|Data source not found and no default driver specified|(DM) The data source name specified in the browse request connection string (*InConnectionString*) was not found in the system information, nor was there a default driver specification.<br /><br /> (DM) ODBC data source and default driver information could not be found in the system information.|  
|IM003|Specified driver could not be loaded|(DM) The driver listed in the data source specification in the system information or specified by the **DRIVER** keyword was not found or could not be loaded for some other reason.|  
|IM004|Driver's **SQLAllocHandle** on SQL_HANDLE _ENV failed|(DM) During **SQLBrowseConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with a *HandleType* of SQL_HANDLE_ENV and the driver returned an error.|  
|IM005|Driver's **SQLAllocHandle** on SQL_HANDLE_DBC failed|(DM) During **SQLBrowseConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with a *HandleType* of SQL_HANDLE_DBC and the driver returned an error.|  
|IM006|Driver's **SQLSetConnectAttr** failed|(DM) During **SQLBrowseConnect**, the Driver Manager called the driver's **SQLSetConnectAttr** function and the driver returned an error.|  
|IM009|Unable to load translation DLL|The driver was unable to load the translation DLL that was specified for the data source or for the connection.|  
|IM010|Data source name too long|(DM) The attribute value for the DSN keyword was longer than SQL_MAX_DSN_LENGTH characters.|  
|IM011|Driver name too long|(DM) The attribute value for the DRIVER keyword was longer than 255 characters.|  
|IM012|DRIVER keyword syntax error|(DM) The keyword-value pair for the DRIVER keyword contained a syntax error.|  
|IM014|The specified DSN contains an architecture mismatch between the Driver and Application|(DM) 32-bit application uses a DSN connecting to a 64-bit driver; or vice versa.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
|S1118|Driver does not support asynchronous notification|When the driver does not support asynchronous notification, you cannot set SQL_ATTR_ASYNC_DBC_EVENT or SQL_ATTR_ASYNC_DBC_RETCODE_PTR.|  
  
## InConnectionString Argument  
 A browse request connection string has the following syntax:  
  
 *connection-string* ::= *attribute*[`;`] &#124; *attribute* `;` *connection-string*;<br>
 *attribute* ::= *attribute-keyword*`=`*attribute-value* &#124; `DRIVER=`[`{`]*attribute-value*[`}`]<br>
 *attribute-keyword* ::= `DSN` &#124; `UID` &#124; `PWD` &#124; *driver-defined-attribute-keyword*<br>
 *attribute-value* ::= *character-string*<br>
 *driver-defined-attribute-keyword* ::= *identifier*<br>
  
 where *character-string* has zero or more characters; *identifier* has one or more characters; *attribute-keyword* is not case-sensitive; *attribute-value* may be case-sensitive; and the value of the **DSN** keyword does not consist solely of blanks. Because of connection string and initialization file grammar, keywords and attribute values that contain the characters **[]{}(),;?\*=!@** should be avoided. Because of the grammar in the system information, keywords and data source names cannot contain the backslash (\\) character. For an ODBC 2.*x* driver, braces are required around the attribute value for the DRIVER keyword.  
  
 If any keywords are repeated in the browse request connection string, the driver uses the value associated with the first occurrence of the keyword. If the **DSN** and **DRIVER** keywords are included in the same browse request connection string, the Driver Manager and driver use whichever keyword appears first.  
  
 For information about how an application chooses a data source or driver, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
## OutConnectionString Argument  
 The browse result connection string is a list of connection attributes. A connection attribute consists of an attribute keyword and a corresponding attribute value. The browse result connection string has the following syntax:  
  
 *connection-string* ::= *attribute*[`;`] &#124; *attribute* `;` *connection-string*<br>
 *attribute* ::= [`*`]*attribute-keyword*`=`*attribute-value*<br>
 *attribute-keyword* ::= *ODBC-attribute-keyword* &#124; *driver-defined-attribute-keyword*<br>
 *ODBC-attribute-keyword* = {`UID` &#124; `PWD`}[`:`*localized-identifier*]
 *driver-defined-attribute-keyword* ::= *identifier*[`:`*localized-identifier*]
 *attribute-value* ::= `{` *attribute-value-list* `}` &#124; `?` (The braces are literal; they are returned by the driver.)<br>
 *attribute-value-list* ::= *character-string* [`:`*localized-character string*] &#124; *character-string* [`:`*localized-character string*] `,` *attribute-value-list*<br>
  
 where *character-string* and *localized-character string* have zero or more characters; *identifier* and *localized-identifier* have one or more characters; *attribute-keyword* is not case-sensitive; and *attribute-value* may be case-sensitive. Because of connection string and initialization file grammar, keywords, localized identifiers, and attribute values that contain the characters **[]{}(),;?\*=!@** should be avoided. Because of the grammar in the system information, keywords and data source names cannot contain the backslash (\\) character.  
  
 The browse result connection string syntax is used according to the following semantic rules:  
  
-   If an asterisk (\*) precedes an *attribute-keyword*, the *attribute* is optional and can be omitted in the next call to **SQLBrowseConnect**.  
  
-   The attribute keywords **UID** and **PWD** have the same meaning as defined in **SQLDriverConnect**.  
  
-   A *driver-defined-attribute-keyword* names the kind of attribute for which an attribute value may be supplied. For example, it might be **SERVER**, **DATABASE**, **HOST**, or **DBMS**.  
  
-   *ODBC-attribute-keywords* and *driver-defined-attribute-keywords* include a localized or user-friendly version of the keyword. This might be used by applications as a label in a dialog box. However, **UID**, **PWD**, or the *identifier* alone must be used when passing a browse request string to the driver.  
  
-   The {*attribute-value-list*} is an enumeration of actual values valid for the corresponding *attribute-keyword*. Note that the braces ({}) do not indicate a list of choices; they are returned by the driver. For example, it might be a list of server names or a list of database names.  
  
-   If the *attribute-value* is a single question mark (?), a single value corresponds to the *attribute-keyword*. For example, UID=JohnS; PWD=Sesame.  
  
-   Each call to **SQLBrowseConnect** returns only the information required to satisfy the next level of the connection process. The driver associates state information with the connection handle so that the context can always be determined on each call.  
  
## Using SQLBrowseConnect  
 **SQLBrowseConnect** requires an allocated connection. The Driver Manager loads the driver that was specified in or that corresponds to the data source name specified in the initial browse request connection string; for information about when this occurs, see the "Comments" section in [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md). The driver may establish a connection with the data source during the browsing process. If **SQLBrowseConnect** returns SQL_ERROR, outstanding connections are terminated and the connection is returned to an unconnected state.  
  
> [!NOTE]  
>  **SQLBrowseConnect** does not support connection pooling. If **SQLBrowseConnect** is called while connection pooling is enabled, SQLSTATE HY000 (General error) will be returned.  
  
 When **SQLBrowseConnect** is called for the first time on a connection, the browse request connection string must contain the **DSN** keyword or the **DRIVER** keyword. If the browse request connection string contains the **DSN** keyword, the Driver Manager locates a corresponding data source specification in the system information:  
  
-   If the Driver Manager finds the corresponding data source specification, it loads the associated driver DLL; the driver can retrieve information about the data source from the system information.  
  
-   If the Driver Manager cannot find the corresponding data source specification, it locates the default data source specification and loads the associated driver DLL; the driver can retrieve information about the default data source from the system information. "DEFAULT" is passed to the driver for the DSN.  
  
-   If the Driver Manager cannot find the corresponding data source specification and there is no default data source specification, it returns SQL_ERROR with SQLSTATE IM002 (Data source not found and no default driver specified).  
  
 If the browse request connection string contains the **DRIVER** keyword, the Driver Manager loads the specified driver; it does not attempt to locate a data source in the system information. Because the **DRIVER** keyword does not use information from the system information, the driver must define enough keywords so that a driver can connect to a data source using only the information in the browse request connection strings.  
  
 On each call to **SQLBrowseConnect**, the application specifies the connection attribute values in the browse request connection string. The driver returns successive levels of attributes and attribute values in the browse result connection string; it returns SQL_NEED_DATA as long as there are connection attributes that have not yet been enumerated in the browse request connection string. The application uses the contents of the browse result connection string to build the browse request connection string for the next call to **SQLBrowseConnect**. All mandatory attributes (those not preceded by an asterisk in the *OutConnectionString* argument) must be included in the next call to **SQLBrowseConnect**. Note that the application cannot use the contents of previous browse result connection strings when building the current browse request connection string; that is, it cannot specify different values for attributes set in previous levels.  
  
 When all levels of connection and their associated attributes have been enumerated, the driver returns SQL_SUCCESS, the connection to the data source is complete, and a complete connection string is returned to the application. The connection string is suitable to use, in conjunction with **SQLDriverConnect**, with the SQL_DRIVER_NOPROMPT option to establish another connection. The complete connection string cannot be used in another call to **SQLBrowseConnect**, however; if **SQLBrowseConnect** were called again, the entire sequence of calls would have to be repeated.  
  
 **SQLBrowseConnect** also returns SQL_NEED_DATA if there are recoverable, nonfatal errors during the browse process; for example, an invalid password or attribute keyword supplied by the application. When SQL_NEED_DATA is returned and the browse result connection string is unchanged, an error has occurred and the application can call **SQLGetDiagRec** to return the SQLSTATE for browse-time errors. This permits the application to correct the attribute and continue the browse.  
  
 An application can terminate the browse process at any time by calling **SQLDisconnect**. The driver will terminate any outstanding connections and return the connection to an unconnected state.  
  
 If asynchronous operations are enabled on the connection handle, **SQLBrowseConnect** might also return SQL_STILL_EXECUTING. When it returns SQL_NEED_DATA, an application must use **SQLDisconnect** to cancel the browse process. If **SQLBrowseConnect** returns SQL_STILL_EXECUTING, an application should use **SQLCancelHandle** to cancel the operation in progress. Calling **SQLCancelHandle** after the function returns SQL_NEED_DATA has no effect.  
  
 For more information, see [Connecting with SQLBrowseConnect](../../../odbc/reference/develop-app/connecting-with-sqlbrowseconnect.md).  
  
 If a driver supports **SQLBrowseConnect**, the driver keyword section in the system information for the driver must contain the **ConnectFunctions** keyword with the third character set to "Y."  
  
## Code Example  
  
> [!NOTE]  
>  If you are connecting to a data source provider that supports Windows authentication, you should specify `Trusted_Connection=yes` instead of user ID and password information in the connection string.  
  
 In the following example, an application calls **SQLBrowseConnect** repeatedly. Each time **SQLBrowseConnect** returns SQL_NEED_DATA, it passes back information about the data it needs in \**OutConnectionString*. The application passes *OutConnectionString* to its routine **GetUserInput** (not shown). **GetUserInput** parses the information, builds and displays a dialog box, and returns the information entered by the user in \**InConnectionString*. The application passes the user's information to the driver in the next call to **SQLBrowseConnect**. After the application has provided all necessary information for the driver to connect to the data source, **SQLBrowseConnect** returns SQL_SUCCESS and the application proceeds.  
  
 For a more detailed example of connecting to a SQL Server driver by calling **SQLBrowseConnect**, see [SQL Server Browsing Example](../../../odbc/reference/develop-app/sql-server-browsing-example.md).  
  
 For example, to connect to the data source Sales, the following actions might occur. First, the application passes the following string to **SQLBrowseConnect**:  
  
```  
"DSN=Sales"  
```  
  
 The Driver Manager loads the driver associated with the data source Sales. It then calls the driver's **SQLBrowseConnect** function with the same arguments it received from the application. The driver returns the following string in **OutConnectionString*:  
  
```  
"HOST:Server={red,blue,green};UID:ID=?;PWD:Password=?"  
```  
  
 The application passes this string to its **GetUserInput** routine, which builds a dialog box that asks the user to select the red, blue, or green server and to enter a user ID and password. The routine passes the following user-specified information back in \**InConnectionString*, which the application passes to **SQLBrowseConnect**:  
  
```  
"HOST=red;UID=Smith;PWD=Sesame"  
```  
  
 **SQLBrowseConnect** uses this information to connect to the red server as Smith with the password Sesame, and then returns the following string in **OutConnectionString*:  
  
```  
"*DATABASE:Database={SalesEmployees,SalesGoals,SalesOrders}"  
```  
  
 The application passes this string to its **GetUserInput** routine, which builds a dialog box that asks the user to select a database. The user selects empdata and the application calls **SQLBrowseConnect** a final time with this string:  
  
```  
"DATABASE=SalesOrders"  
```  
  
 This is the final piece of information the driver needs to connect to the data source; **SQLBrowseConnect** returns SQL_SUCCESS, and **OutConnectionString* contains the completed connection string:  
  
```  
// SQLBrowseConnect_Function.cpp  
// compile with: odbc32.lib  
#include <windows.h>  
#include <sqltypes.h>  
#include <sqlext.h>  
  
#define BRWS_LEN 100  
SQLHENV henv;  
SQLHDBC hdbc;  
SQLHSTMT hstmt;  
SQLRETURN retcode;  
SQLCHAR szConnStrIn[BRWS_LEN], szConnStrOut[BRWS_LEN];  
SQLSMALLINT cbConnStrOut;  
  
void GetUserInput(SQLCHAR * szConnStrOut, SQLCHAR * szConnStrIn) {}  
  
int main() {  
   // Allocate the environment handle.  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);        
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
  
      // Set the version environment attribute.  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
  
         // Allocate the connection handle.  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            // Call SQLBrowseConnect until it returns a value other than SQL_NEED_DATA   
            // (pass data source name the first time).  If SQL_NEED_DATA is returned, call GetUserInput   
            // (not shown) to build a dialog from the values in szConnStrOut.  The user-supplied values   
            // are returned in szConnStrIn, which is passed in the next call to SQLBrowseConnect.  
  
            strcpy_s((char*)szConnStrIn, _countof(szConnStrIn), "DSN=Sales");  
            do {  
               retcode = SQLBrowseConnect(hdbc, szConnStrIn, SQL_NTS,  
                  szConnStrOut, BRWS_LEN, &cbConnStrOut);  
               if (retcode == SQL_NEED_DATA)  
                  GetUserInput(szConnStrOut, szConnStrIn);  
            } while (retcode == SQL_NEED_DATA);  
  
            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO){  
  
               // Allocate the statement handle.  
               retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
                  // Process data after successful connection  
                  SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
               SQLDisconnect(hdbc);  
            }  
         }  
         SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
      }  
   }  
   SQLFreeHandle(SQL_HANDLE_ENV, henv);  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a connection handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Connecting to a data source|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|  
|Disconnecting from a data source|[SQLDisconnect Function](../../../odbc/reference/syntax/sqldisconnect-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
|Returning driver descriptions and attributes|[SQLDrivers Function](../../../odbc/reference/syntax/sqldrivers-function.md)|  
|Freeing a connection handle|[SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
