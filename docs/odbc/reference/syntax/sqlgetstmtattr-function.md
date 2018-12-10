---
title: "SQLGetStmtAttr Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetStmtAttr"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetStmtAttr"
helpviewer_keywords: 
  - "SQLGetStmtAttr function [ODBC]"
ms.assetid: e321d460-e997-4527-aee6-207cf5a498e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetStmtAttr Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetStmtAttr** returns the current setting of a statement attribute.  
  
> [!NOTE]  
>  For more information about what the Driver Manager maps this function to when an ODBC 3.*x* application is working with an ODBC 2.*x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetStmtAttr(  
     SQLHSTMT        StatementHandle,  
     SQLINTEGER      Attribute,  
     SQLPOINTER      ValuePtr,  
     SQLINTEGER      BufferLength,  
     SQLINTEGER *    StringLengthPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *Attribute*  
 [Input] Attribute to retrieve.  
  
 *ValuePtr*  
 [Output] Pointer to a buffer in which to return the value of the attribute specified in *Attribute*.  
  
 If *ValuePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *ValuePtr*.  
  
 *BufferLength*  
 [Input] If *Attribute* is an ODBC-defined attribute and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of \**ValuePtr*. If *Attribute* is an ODBC-defined attribute and \**ValuePtr* is an integer, *BufferLength* is ignored. If the value returned in *\*ValuePtr* is a Unicode string (when calling **SQLGetStmtAttrW**), the *BufferLength* argument must be an even number.  
  
 If *Attribute* is a driver-defined attribute, the application indicates the nature of the attribute to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *\*ValuePtr* is a pointer to a character string, then *BufferLength* is the length of the string or SQL_NTS.  
  
-   If *\*ValuePtr* is a pointer to a binary buffer, then the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *\*ValuePtr* is a pointer to a value other than a character string or binary string, then *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *\*ValuePtr* is contains a fixed-length data type, then *BufferLength* is either SQL_IS_INTEGER or SQL_IS_UINTEGER, as appropriate.  
  
 *StringLengthPtr*  
 [Output] A pointer to a buffer in which to return the total number of bytes (excluding the null-termination character) available to return in *\*ValuePtr*. If *ValuePtr* is a null pointer, no length is returned. If the attribute value is a character string, and the number of bytes available to return is greater than or equal to *BufferLength*, the data in *\*ValuePtr* is truncated to *BufferLength* minus the length of a null-termination character and is null-terminated by the driver.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetStmtAttr** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetStmtAttr** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The data returned in *\*ValuePtr* was truncated to be *BufferLength* minus the length of a null-termination character. The length of the untruncated string value is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|24000|Invalid cursor state|The argument *Attribute* was SQL_ATTR_ROW_NUMBER and the cursor was not open, or the cursor was positioned before the start of the result set or after the end of the result set.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the argument *MessageText* describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLGetStmtAttr** function was called.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|*(DM) \*ValuePtr* is a character string, and BufferLength was less than zero, but not equal to SQL_NTS.|  
|HY092|Invalid attribute/option identifier|The value specified for the argument *Attribute* was not valid for the version of ODBC supported by the driver.|  
|HY109|Invalid cursor position|The *Attribute* argument was SQL_ATTR_ROW_NUMBER and the row had been deleted or could not be fetched.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The value specified for the argument *Attribute* was a valid ODBC statement attribute for the version of ODBC supported by the driver, but was not supported by the driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the *StatementHandle* does not support the function.|  
  
## Comments  
 For general information about statement attributes, see [Statement Attributes](../../../odbc/reference/develop-app/statement-attributes.md).  
  
 A call to **SQLGetStmtAttr** returns in *\*ValuePtr* the value of the statement attribute specified in *Attribute*. That value can either be a SQLULEN value or a null-terminated character string. If the value is a SQLULEN value, some drivers may only write the lower 32-bit or 16-bit of a buffer and leave the higher-order bit unchanged. Therefore, applications should use a buffer of SQLULEN and initialize the value to 0 before calling this function. Also, the *BufferLength* and *StringLengthPtr* arguments are not used. If the value is a null-terminated string, the application specifies the maximum length of that string in the *BufferLength* argument, and the driver returns the length of that string in the *\*StringLengthPtr* buffer.  
  
 To allow applications calling **SQLGetStmtAttr** to work with ODBC 2.*x* drivers, a call to **SQLGetStmtAttr** is mapped in the Driver Manager to **SQLGetStmtOption**.  
  
 The following statement attributes are read-only, so can be retrieved by **SQLGetStmtAttr**, but not set by **SQLSetStmtAttr**:  
  
-   SQL_ATTR_IMP_PARAM_DESC  
  
-   SQL_ATTR_IMP_ROW_DESC  
  
-   SQL_ATTR_ROW_NUMBER  
  
 For a list of attributes that can be set and retrieved, see [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning the setting of a connection attribute|[SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
