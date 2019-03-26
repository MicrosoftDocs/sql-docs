---
title: "SQLGetDescField Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetDescField"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetDescField"
helpviewer_keywords: 
  - "SQLGetDescField function [ODBC]"
ms.assetid: f09ff660-1e4a-4370-be85-90d4da0487d3
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetDescField Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetDescField** returns the current setting or value of a single field of a descriptor record.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetDescField(  
     SQLHDESC        DescriptorHandle,  
     SQLSMALLINT     RecNumber,  
     SQLSMALLINT     FieldIdentifier,  
     SQLPOINTER      ValuePtr,  
     SQLINTEGER      BufferLength,  
     SQLINTEGER *    StringLengthPtr);  
```  
  
## Arguments  
 *DescriptorHandle*  
 [Input] Descriptor handle.  
  
 *RecNumber*  
 [Input] Indicates the descriptor record from which the application seeks information. Descriptor records are numbered from 0, with record number 0 being the bookmark record. If the *FieldIdentifier* argument indicates a header field, *RecNumber* is ignored. If *RecNumber* is less than or equal to SQL_DESC_COUNT but the row does not contain data for a column or parameter, a call to **SQLGetDescField** will return the default values of the fields. (For more information, see "Initialization of Descriptor Fields" in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).)  
  
 *FieldIdentifier*  
 [Input] Indicates the field of the descriptor whose value is to be returned. For more information, see the "*FieldIdentifier* Argument" section in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).  
  
 *ValuePtr*  
 [Output] Pointer to a buffer in which to return the descriptor information. The data type depends on the value of *FieldIdentifier*.  
  
 If *ValuePtr* is integer type, applications should use a buffer of SQLULEN and initialize the value to 0 before calling this function as some drivers may only write the lower 32-bit or 16-bit of a buffer and leave the higher-order bit unchanged.  
  
 If *ValuePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *ValuePtr*.  
  
 *BufferLength*  
 [Input] If *FieldIdentifier* is an ODBC-defined field and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of \**ValuePtr*. If *FieldIdentifier* is an ODBC-defined field and \**ValuePtr* is an integer, *BufferLength* is ignored. If the value in *\*ValuePtr* is of a Unicode data type (when calling **SQLGetDescFieldW**), the *BufferLength* argument must be an even number.  
  
 If *FieldIdentifier* is a driver-defined field, the application indicates the nature of the field to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *\*ValuePtr* is a pointer to a character string, then *BufferLength* is the length of the string or SQL_NTS.  
  
-   If *\*ValuePtr* is a pointer to a binary buffer, then the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *\*ValuePtr* is a pointer to a value other than a character string or binary string, then *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *\*ValuePtr* is contains a fixed-length data type, then *BufferLength* is either SQL_IS_INTEGER, SQL_IS_UINTEGER, SQL_IS_SMALLINT, or SQL_IS_USMALLINT, as appropriate.  
  
 *StringLengthPtr*  
 [Output] Pointer to the buffer in which to return the total number of bytes (excluding the number of bytes required for the null-termination character) available to return in **ValuePtr*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_NO_DATA, or SQL_INVALID_HANDLE.  
  
 SQL_NO_DATA is returned if *RecNumber* is greater than the current number of descriptor records.  
  
 SQL_NO_DATA is returned if *DescriptorHandle* is an IRD handle and the statement is in the prepared or executed state but there was no open cursor associated with it.  
  
## Diagnostics  
 When **SQLGetDescField** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetDescField** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**ValuePtr* was not large enough to return the entire descriptor field, so the field was truncated. The length of the untruncated descriptor field is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07009|Invalid descriptor index|(DM) The *RecNumber* argument was equal to 0, the SQL_ATTR_USE_BOOKMARK statement attribute was SQL_UB_OFF, and the *DescriptorHandle* argument was an IRD handle. (This error can be returned for an explicitly allocated descriptor only if the descriptor is associated with a statement handle.)<br /><br /> The *FieldIdentifier* argument was a record field, the *RecNumber* argument was 0, and the *DescriptorHandle* argument was an IPD handle.<br /><br /> The *RecNumber* argument was less than 0.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate the memory required to support execution or completion of the function.|  
|HY007|Associated statement is not prepared|*DescriptorHandle* was associated with a *StatementHandle* as an IRD, and the associated statement handle had not been prepared or executed.|  
|HY010|Function sequence error|(DM) *DescriptorHandle* was associated with a *StatementHandle* for which an asynchronously executing function (not this one) was called and was still executing when this function was called.<br /><br /> (DM) *DescriptorHandle* was associated with a *StatementHandle* for which **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *DescriptorHandle*. This asynchronous function was still executing when the **SQLGetDescField** function was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY021|Inconsistent descriptor information|The SQL_DESC_TYPE and SQL_DESC_DATETIME_INTERVAL_CODE fields do not form a valid ODBC SQL type, a valid driver-specific SQL type (for IPDs), or a valid ODBC C type (for APDs or ARDs).|  
|HY090|Invalid string or buffer length|(DM) *\*ValuePtr* was a character string, and *BufferLength* was less than zero.|  
|HY091|Invalid descriptor field identifier|*FieldIdentifier* was not an ODBC-defined field and was not an implementation-defined value.<br /><br /> *FieldIdentifier* was undefined for the *DescriptorHandle*.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about the suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *DescriptorHandle* does not support the function.|  
  
## Comments  
 An application can call **SQLGetDescField** to return the value of a single field of a descriptor record. A call to **SQLGetDescField** can return the setting of any field in any descriptor type, including header fields, record fields, and bookmark fields. An application can obtain the settings of multiple fields in the same or different descriptors, in arbitrary order, by making repeated calls to **SQLGetDescField**. **SQLGetDescField** can also be called to return driver-defined descriptor fields.  
  
 For performance reasons, an application should not call **SQLGetDescField** for an IRD before executing a statement.  
  
 The settings of multiple fields that describe the name, data type, and storage of column or parameter data can also be retrieved in a single call to **SQLGetDescRec**. **SQLGetStmtAttr** can be called to return the setting of a single field in the descriptor header that is also a statement attribute. **SQLColAttribute**, **SQLDescribeCol**, and **SQLDescribeParam** return record or bookmark fields.  
  
 When an application calls **SQLGetDescField** to retrieve the value of a field that is undefined for a particular descriptor type, the function returns SQL_SUCCESS but the value returned for the field is undefined. For example, calling **SQLGetDescField** for the SQL_DESC_NAME or SQL_DESC_NULLABLE field of an APD or ARD will return SQL_SUCCESS but an undefined value for the field.  
  
 When an application calls **SQLGetDescField** to retrieve the value of a field that is defined for a particular descriptor type but that has no default value and has not been set yet, the function returns SQL_SUCCESS but the value returned for the field is undefined. For more information on the initialization of descriptor fields and descriptions of the fields, see "Initialization of Descriptor Fields" in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md). For more information on descriptors, see [Descriptors](../../../odbc/reference/develop-app/descriptors.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Getting multiple descriptor fields|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting a single descriptor field|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
|Setting multiple descriptor fields|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
