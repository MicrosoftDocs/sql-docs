---
title: "SQLGetEnvAttr Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetEnvAttr"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetEnvAttr"
helpviewer_keywords: 
  - "SQLGetEnvAttr function [ODBC]"
ms.assetid: 01f4590f-427a-4280-a1c3-18de9f7d86c1
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetEnvAttr Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetEnvAttr** returns the current setting of an environment attribute.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetEnvAttr(  
     SQLHENV        EnvironmentHandle,  
     SQLINTEGER     Attribute,  
     SQLPOINTER     ValuePtr,  
     SQLINTEGER     BufferLength,  
     SQLINTEGER *   StringLengthPtr);  
```  
  
## Arguments  
 *EnvironmentHandle*  
 [Input] Environment handle.  
  
 *Attribute*  
 [Input] Attribute to retrieve.  
  
 *ValuePtr*  
 [Output] Pointer to a buffer in which to return the current value of the attribute specified by *Attribute*.  
  
 If *ValuePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *ValuePtr*.  
  
 *BufferLength*  
 [Input] If *ValuePtr* points to a character string, this argument should be the length of \**ValuePtr*. If \**ValuePtr* is an integer, *BufferLength* is ignored. If *\*ValuePtr* is a Unicode string (when calling **SQLGetEnvAttrW**), the *BufferLength* argument must be an even number. If the attribute value is not a character string, *BufferLength* is unused.  
  
 *StringLengthPtr*  
 [Output] A pointer to a buffer in which to return the total number of bytes (excluding the null-termination character) available to return in *\*ValuePtr*. If *ValuePtr* is a null pointer, no length is returned. If the attribute value is a character string and the number of bytes available to return is greater than or equal to *BufferLength*, the data in \**ValuePtr* is truncated to *BufferLength* minus the length of a null-termination character and is null-terminated by the driver.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetEnvAttr** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_ENV and a *Handle* of *EnvironmentHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetEnvAttr** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The data returned in \**ValuePtr* was truncated to be *BufferLength* minus the null-termination character. The length of the untruncated string value is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQL_ATTR_ODBC_VERSION** has not yet been set via **SQLSetEnvAttr**. You do not need to set **SQL_ATTR_ODBC_VERSION** explicitly if you are using **SQLAllocHandleStd**.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY092|Invalid attribute/option identifier|The value specified for the argument *Attribute* was not valid for the version of ODBC supported by the driver.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The value specified for the argument *Attribute* was a valid ODBC environment attribute for the version of ODBC supported by the driver but was not supported by the driver.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the *EnvironmentHandle* does not support the function.|  
  
## Comments  
 For a list of attributes, see [SQLSetEnvAttr](../../../odbc/reference/syntax/sqlsetenvattr-function.md). There are no driver-specific environment attributes. If *Attribute* specifies an attribute that returns a string, *ValuePtr* must be a pointer to a buffer in which to return the string. The maximum length of the string, including the null-termination byte, will be *BufferLength* bytes.  
  
 **SQLGetEnvAttr** can be called at any time between the allocation and the freeing of an environment handle. All environment attributes successfully set by the application for the environment persist until **SQLFreeHandle** is called on the *EnvironmentHandle* with a *HandleType* of SQL_HANDLE_ENV. More than one environment handle can be allocated simultaneously in ODBC 3*.x*. An environment attribute on one environment is not affected when another environment has been allocated.  
  
> [!NOTE]
>  The SQL_ATTR_OUTPUT_NTS environment attribute is supported by standards-compliant applications. When **SQLGetEnvAttr** is called, the ODBC 3*.x* Driver Manager always returns SQL_TRUE for this attribute. SQL_ATTR_OUTPUT_NTS can be set to SQL_TRUE only by a call to **SQLSetEnvAttr**.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning the setting of a connection attribute|[SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Returning the setting of a statement attribute|[SQLGetStmtAttr Function](../../../odbc/reference/syntax/sqlgetstmtattr-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
|Setting an environment attribute|[SQLSetEnvAttr Function](../../../odbc/reference/syntax/sqlsetenvattr-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
