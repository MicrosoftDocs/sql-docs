---
description: "SQLGetConnectAttr Function"
title: "SQLGetConnectAttr Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetConnectOption"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetConnectAttr"
helpviewer_keywords: 
  - "SQLGetConnectAttr function [ODBC]"
ms.assetid: 2cb4ffa8-19d3-4664-8c2f-6682cdcc3f33
author: David-Engel
ms.author: v-davidengel
---
# SQLGetConnectAttr Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetConnectAttr** returns the current setting of a connection attribute.  
  
> [!NOTE]
>  For more information about what the Driver Manager maps this function to when an ODBC 3*.x* application is working with an ODBC 2*.x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLGetConnectAttr(  
     SQLHDBC        ConnectionHandle,  
     SQLINTEGER     Attribute,  
     SQLPOINTER     ValuePtr,  
     SQLINTEGER     BufferLength,  
     SQLINTEGER *   StringLengthPtr);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *Attribute*  
 [Input] Attribute to retrieve.  
  
 *ValuePtr*  
 [Output] A pointer to memory in which to return the current value of the attribute specified by *Attribute*. For integer-type attributes, some drivers may only write the lower 32-bit or 16-bit of a buffer and leave the higher-order bit unchanged. Therefore, applications should use a buffer of SQLULEN and initialize the value to 0 before calling this function.  
  
 If *ValuePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *ValuePtr*.  
  
 *BufferLength*  
 [Input] If *Attribute* is an ODBC-defined attribute and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of \**ValuePtr*. If *Attribute* is an ODBC-defined attribute and *\*ValuePtr* is an integer, *BufferLength* is ignored. If the value in *\*ValuePtr* is a Unicode string (when calling **SQLGetConnectAttrW**), the *BufferLength* argument must be an even number.  
  
 If *Attribute* is a driver-defined attribute, the application indicates the nature of the attribute to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *\*ValuePtr* is a pointer to a character string, *BufferLength* is the length of the string.  
  
-   If *\*ValuePtr* is a pointer to a binary buffer, the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *\*ValuePtr* is a pointer to a value other than a character string or binary string, *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *\*ValuePtr* contains a fixed-length data type, *BufferLength* is either SQL_IS_INTEGER or SQL_IS_UINTEGER, as appropriate.  
  
 *StringLengthPtr*  
 [Output] A pointer to a buffer in which to return the total number of bytes (excluding the null-termination character) available to return in \**ValuePtr*. If the attribute value is a character string and the number of bytes available to return is greater than *BufferLength* minus the length of the null-termination character, the data in *\*ValuePtr* is truncated to *BufferLength* minus the length of the null-termination character and is null-terminated by the driver.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetConnectAttr** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained from the diagnostic data structure by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values typically returned by **SQLGetConnectAttr** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The data returned in \**ValuePtr* was truncated to be *BufferLength* minus the length of a null-termination character. The length of the untruncated string value is returned in *\*StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|(DM) An *Attribute* value that required an open connection was specified.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned from the diagnostic data structure by the argument *MessageText* in **SQLGetDiagField** describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQLBrowseConnect** was called for the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before **SQLBrowseConnect** returned SQL_SUCCESS_WITH_INFO or SQL_SUCCESS.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *ConnectionHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) *\*ValuePtr* is a character string, and BufferLength was less than zero but not equal to SQL_NTS.|  
|HY092|Invalid attribute/option identifier|The value specified for the argument *Attribute* was not valid for the version of ODBC supported by the driver.|  
|HY114|Driver does not support connection-level asynchronous function execution|(DM) An application attempted to enable asynchronous function execution with SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE for a driver that does not support asynchronous connection operations.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The value specified for the argument *Attribute* was a valid ODBC connection attribute for the version of ODBC supported by the driver, but was not supported by the driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver that corresponds to the *ConnectionHandle* does not support the function.|  
  
## Comments  
 For general information about connection attributes, see [Connection Attributes](../../../odbc/reference/develop-app/connection-attributes.md).  
  
 For a list of attributes that can be set, see [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md). Notice that if *Attribute* specifies an attribute that returns a string, *ValuePtr* must be a pointer to a buffer for the string. The maximum length of the returned string, including the null-termination character, will be *BufferLength* bytes.  
  
 Depending on the attribute, an application does not have to establish a connection before calling **SQLGetConnectAttr**. However, if **SQLGetConnectAttr** is called and the specified attribute does not have a default and has not been set by a prior call to **SQLSetConnectAttr**, **SQLGetConnectAttr** will return SQL_NO_DATA.  
  
 If *Attribute* is SQL_ATTR_ TRACE or SQL_ATTR_ TRACEFILE, *ConnectionHandle* does not have to be valid, and **SQLGetConnectAttr** will not return SQL_ERROR or SQL_INVALID_HANDLE if *ConnectionHandle* is invalid. These attributes apply to all connections. **SQLGetConnectAttr** will return SQL_ERROR or SQL_INVALID_HANDLE if another argument is invalid.  
  
 Although an application can set statement attributes by using **SQLSetConnectAttr**, an application cannot use **SQLGetConnectAttr** to retrieve statement attribute values; it must call **SQLGetStmtAttr** to retrieve the setting of statement attributes.  
  
 Both SQL_ATTR_AUTO_IPD and SQL_ATTR_CONNECTION_DEAD connection attributes can be returned by a call to **SQLGetConnectAttr** but cannot be set by a call to **SQLSetConnectAttr**.  
  
> [!NOTE]  
>  There is no asynchronous support for **SQLGetConnectAttr**. When implementing **SQLGetConnectAttr**, a driver can improve performance by minimizing the number of times that information is sent or requested from the server.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning the setting of a statement attribute|[SQLGetStmtAttr Function](../../../odbc/reference/syntax/sqlgetstmtattr-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
|Setting an environment attribute|[SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
