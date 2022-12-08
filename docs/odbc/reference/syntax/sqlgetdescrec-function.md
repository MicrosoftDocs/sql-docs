---
description: "SQLGetDescRec Function"
title: "SQLGetDescRec Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetDescRec"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetDescRec"
helpviewer_keywords: 
  - "SQLGetDescRec function [ODBC]"
ms.assetid: 325e0907-8e87-44e8-a111-f39e636a9cbc
author: David-Engel
ms.author: v-davidengel
---
# SQLGetDescRec Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetDescRec** returns the current settings or values of multiple fields of a descriptor record. The fields returned describe the name, data type, and storage of column or parameter data.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLGetDescRec(  
      SQLHDESC        DescriptorHandle,  
      SQLSMALLINT     RecNumber,  
      SQLCHAR *       Name,  
      SQLSMALLINT     BufferLength,  
      SQLSMALLINT *   StringLengthPtr,  
      SQLSMALLINT *   TypePtr,  
      SQLSMALLINT *   SubTypePtr,  
      SQLLEN *        LengthPtr,  
      SQLSMALLINT *   PrecisionPtr,  
      SQLSMALLINT *   ScalePtr,  
      SQLSMALLINT *   NullablePtr);  
```  
  
## Arguments  
 *DescriptorHandle*  
 [Input] Descriptor handle.  
  
 *RecNumber*  
 [Input] Indicates the descriptor record from which the application seeks information. Descriptor records are numbered from 1, with record number 0 being the bookmark record. The *RecNumber* argument must be less than or equal to the value of SQL_DESC_COUNT. If *RecNumber* is less than or equal to SQL_DESC_COUNT but the row does not contain data for a column or parameter, a call to **SQLGetDescRec** will return the default values of the fields. (For more information, see "Initialization of Descriptor Fields" in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).)  
  
 *Name*  
 [Output] A pointer to a buffer in which to return the SQL_DESC_NAME field for the descriptor record.  
  
 If *Name* is NULL, *StringLengthPtr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *Name*.  
  
 *BufferLength*  
 [Input] Length of the **Name* buffer, in characters.  
  
 *StringLengthPtr*  
 [Output] A pointer to a buffer in which to return the number of characters of data available to return in the \**Name* buffer, excluding the null-termination character. If the number of characters was greater than or equal to *BufferLength*, the data in \**Name* is truncated to *BufferLength* minus the length of a null-termination character, and is null-terminated by the driver.  
  
 *TypePtr*  
 [Output] A pointer to a buffer in which to return the value of the SQL_DESC_TYPE field for the descriptor record.  
  
 *SubTypePtr*  
 [Output] For records whose type is SQL_DATETIME or SQL_INTERVAL, this is a pointer to a buffer in which to return the value of the SQL_DESC_DATETIME_INTERVAL_CODE field.  
  
 *LengthPtr*  
 [Output] A pointer to a buffer in which to return the value of the SQL_DESC_OCTET_LENGTH field for the descriptor record.  
  
 *PrecisionPtr*  
 [Output] A pointer to a buffer in which to return the value of the SQL_DESC_PRECISION field for the descriptor record.  
  
 *ScalePtr*  
 [Output] A pointer to a buffer in which to return the value of the SQL_DESC_SCALE field for the descriptor record.  
  
 *NullablePtr*  
 [Output] A pointer to a buffer in which to return the value of the SQL_DESC_NULLABLE field for the descriptor record.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_NO_DATA, or SQL_INVALID_HANDLE.  
  
 SQL_NO_DATA is returned if *RecNumber* is greater than the current number of descriptor records.  
  
 SQL_NO_DATA is returned if *DescriptorHandle* is an IRD handle and the statement is in the prepared or executed state but there was no open cursor associated with it.  
  
## Diagnostics  
 When **SQLGetDescRec** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DESC and a *Handle* of *DescriptorHandle*. The following table lists the SQLSTATE values typically returned by **SQLGetDescRec** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**Name* was not large enough to return the entire descriptor field. Therefore, the field was truncated. The length of the untruncated descriptor field is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07009|Invalid descriptor index|The *FieldIdentifier* argument was a record field, the *RecNumber* argument was set to 0, and the *DescriptorHandle* argument was an IPD handle.<br /><br /> (DM) The *RecNumber* argument was set to 0, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF, and the *DescriptorHandle* argument was an IRD handle.<br /><br /> The *RecNumber* argument was less than 0.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate the memory that is required to support execution or completion of the function.|  
|HY007|Associated statement is not prepared|*DescriptorHandle* was associated with an IRD, and the associated statement handle was not in the prepared or executed state.|  
|HY010|Function sequence error|(DM) *DescriptorHandle* was associated with a *StatementHandle* for which an asynchronously executing function (not this one) was called and was still executing when this function was called.<br /><br /> (DM) *DescriptorHandle* was associated with a *StatementHandle* for which **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *DescriptorHandle*. This asynchronous function was still executing when **SQLGetDescRec** was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with *DescriptorHandle* does not support the function.|  
  
## Comments  
 An application can call **SQLGetDescRec** to retrieve the values of the following descriptor fields for a single column or parameter:  
  
-   SQL_DESC_NAME  
  
-   SQL_DESC_TYPE  
  
-   SQL_DESC_DATETIME_INTERVAL_CODE (for records whose type is SQL_DATETIME or SQL_INTERVAL)  
  
-   SQL_DESC_OCTET_LENGTH  
  
-   SQL_DESC_PRECISION  
  
-   SQL_DESC_SCALE  
  
-   SQL_DESC_NULLABLE  
  
 **SQLGetDescRec** does not retrieve the values for header fields.  
  
 An application can prevent the return of a field's setting by setting the argument that corresponds to the field to a null pointer.  
  
 When an application calls **SQLGetDescRec** to retrieve the value of a field that is undefined for a particular descriptor type, the function returns SQL_SUCCESS but the value returned for the field is undefined. For example, calling **SQLGetDescRec** for the SQL_DESC_NAME or SQL_DESC_NULLABLE field of an APD or ARD will return SQL_SUCCESS but an undefined value for the field.  
  
 When an application calls **SQLGetDescRec** to retrieve the value of a field that is defined for a particular descriptor type but that has no default value and has not been set yet, the function returns SQL_SUCCESS but the value returned for the field is undefined. For more information, see "Initialization of Descriptor Fields" in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).  
  
 The values of fields can also be retrieved individually by a call to **SQLGetDescField**. For a description of the fields in a descriptor header or record, see [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md). For more information about descriptors, see [Descriptors](../../../odbc/reference/develop-app/descriptors.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a column|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Binding a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Getting a descriptor field|[SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)|  
|Setting multiple descriptor fields|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
