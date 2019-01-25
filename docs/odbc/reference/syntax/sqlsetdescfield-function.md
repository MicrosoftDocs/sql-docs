---
title: "SQLSetDescField Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLSetDescField"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetDescField"
helpviewer_keywords: 
  - "SQLSetDescField function [ODBC]"
ms.assetid: 8c544388-fe9d-4f94-a0ac-fa0b9c9c88a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetDescField Function

**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLSetDescField** sets the value of a single field of a descriptor record.  
  
## Syntax  
  
```cpp  
SQLRETURN SQLSetDescField(  
     SQLHDESC      DescriptorHandle,  
     SQLSMALLINT   RecNumber,  
     SQLSMALLINT   FieldIdentifier,  
     SQLPOINTER    ValuePtr,  
     SQLINTEGER    BufferLength);  
```  
  
## Arguments  
 *DescriptorHandle*  
 [Input] Descriptor handle.  
  
 *RecNumber*  
 [Input] Indicates the descriptor record containing the field that the application seeks to set. Descriptor records are numbered from 0, with record number 0 being the bookmark record. The *RecNumber* argument is ignored for header fields.  
  
 *FieldIdentifier*  
 [Input] Indicates the field of the descriptor whose value is to be set. For more information, see "*FieldIdentifier* Argument" in the "Comments" section.  
  
 *ValuePtr*  
 [Input] Pointer to a buffer containing the descriptor information, or an integer value. The data type depends on the value of *FieldIdentifier*. If *ValuePtr* is an integer value, it may be considered as 8 bytes (SQLLEN), 4 bytes (SQLINTEGER) or 2 bytes (SQLSMALLINT), depending on the value of the *FieldIdentifier* argument.  
  
 *BufferLength*  
 [Input] If *FieldIdentifier* is an ODBC-defined field and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of **ValuePtr*. For character string data, this argument should contain the number of bytes in the string.  
  
 If *FieldIdentifier* is an ODBC-defined field and *ValuePtr* is an integer, *BufferLength* is ignored.  
  
 If *FieldIdentifier* is a driver-defined field, the application indicates the nature of the field to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *ValuePtr* is a pointer to a character string, then *BufferLength* is the length of the string or SQL_NTS.  
  
-   If *ValuePtr* is a pointer to a binary buffer, then the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *ValuePtr* is a pointer to a value other than a character string or a binary string, then *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *ValuePtr* contains a fixed-length value, then *BufferLength* is either SQL_IS_INTEGER, SQL_IS_UINTEGER, SQL_IS_SMALLINT, or SQL_IS_USMALLINT, as appropriate.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLSetDescField** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DESC and a *Handle* of *DescriptorHandle*. The following table lists the SQLSTATE values commonly returned by **SQLSetDescField** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|The driver did not support the value specified in *\*ValuePtr* (if *ValuePtr* was a pointer) or the value in *ValuePtr* (if *ValuePtr* was an integer value), or *\*ValuePtr* was invalid because of implementation working conditions, so the driver substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07009|Invalid descriptor index|The *FieldIdentifier* argument was a record field, the *RecNumber* argument was 0, and the *DescriptorHandle* argument referred to an IPD handle.<br /><br /> The *RecNumber* argument was less than 0, and the *DescriptorHandle* argument referred to an ARD or an APD.<br /><br /> The *RecNumber* argument was greater than the maximum number of columns or parameters that the data source can support, and the *DescriptorHandle* argument referred to an APD or ARD.<br /><br /> (DM) The *FieldIdentifier* argument was SQL_DESC_COUNT, and *\*ValuePtr* argument was less than 0.<br /><br /> The *RecNumber* argument was equal to 0, and the *DescriptorHandle* argument referred to an implicitly allocated APD. (This error does not occur with an explicitly allocated application descriptor, because it is not known whether an explicitly allocated application descriptor is an APD or ARD until execute time.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22001|String data, right  truncated|The *FieldIdentifier* argument was SQL_DESC_NAME, and the *BufferLength* argument was a value larger than SQL_MAX_IDENTIFIER_LEN.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) The *DescriptorHandle* was associated with a *StatementHandle* for which an asynchronously executing function (not this one) was called and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* with which the *DescriptorHandle* was associated and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *DescriptorHandle*. This asynchronous function was still executing when the **SQLSetDescField** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *DescriptorHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY016|Cannot modify an implementation row descriptor|The *DescriptorHandle* argument was associated with an IRD, and the *FieldIdentifier* argument was not SQL_DESC_ARRAY_STATUS_PTR or SQL_DESC_ROWS_PROCESSED_PTR.|  
|HY021|Inconsistent descriptor information|The SQL_DESC_TYPE and SQL_DESC_DATETIME_INTERVAL_CODE fields do not form a valid ODBC SQL type or a valid driver-specific SQL type (for IPDs) or a valid ODBC C type (for APDs or ARDs).<br /><br /> Descriptor information checked during a consistency check was not consistent. (See "Consistency Check" in **SQLSetDescRec**.)|  
|HY090|Invalid string or buffer length|(DM) *\*ValuePtr* is a character string, and *BufferLength* was less than zero but was not equal to SQL_NTS.<br /><br /> (DM) The driver was an ODBC 2*.x* driver, the descriptor was an ARD, the *ColumnNumber* argument was set to 0, and the value specified for the argument *BufferLength* was not equal to 4.|  
|HY091|Invalid descriptor field identifier|The value specified for the *FieldIdentifier* argument was not an ODBC-defined field and was not an implementation-defined value.<br /><br /> The *FieldIdentifier* argument was invalid for the *DescriptorHandle* argument.<br /><br /> The *FieldIdentifier* argument was a read-only, ODBC-defined field.|  
|HY092|Invalid attribute/option identifier|The value in *\*ValuePtr* was not valid for the *FieldIdentifier* argument.<br /><br /> The *FieldIdentifier* argument was SQL_DESC_UNNAMED, and *ValuePtr* was SQL_NAMED.|  
|HY105|Invalid parameter type|(DM) The value specified for the SQL_DESC_PARAMETER_TYPE field was invalid. (For more information, see the "*InputOutputType* Argument" section in **SQLBindParameter**.)|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *DescriptorHandle* does not support the function.|  
  
## Comments  
 An application can call **SQLSetDescField** to set any descriptor field one at a time. One call to **SQLSetDescField** sets a single field in a single descriptor. This function can be called to set any field in any descriptor type, provided the field can be set. (See the table later in this section.)  
  
> [!NOTE]  
>  If a call to **SQLSetDescField** fails, the contents of the descriptor record identified by the *RecNumber* argument are undefined.  
  
 Other functions can be called to set multiple descriptor fields with a single call of the function. The **SQLSetDescRec** function sets a variety of fields that affect the data type and buffer bound to a column or parameter (the SQL_DESC_TYPE, SQL_DESC_DATETIME_INTERVAL_CODE, SQL_DESC_OCTET_LENGTH, SQL_DESC_PRECISION, SQL_DESC_SCALE, SQL_DESC_DATA_PTR, SQL_DESC_OCTET_LENGTH_PTR, and SQL_DESC_INDICATOR_PTR fields). **SQLBindCol** or **SQLBindParameter** can be used to make a complete specification for the binding of a column or parameter. These functions set a specific group of descriptor fields with one function call.  
  
 **SQLSetDescField** can be called to change the binding buffers by adding an offset to the binding pointers (SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, or SQL_DESC_OCTET_LENGTH_PTR). This changes the binding buffers without calling **SQLBindCol** or **SQLBindParameter**, which allows an application to change SQL_DESC_DATA_PTR without changing other fields, such as SQL_DESC_DATA_TYPE.  
  
 If an application calls **SQLSetDescField** to set any field other than SQL_DESC_COUNT or the deferred fields SQL_DESC_DATA_PTR, SQL_DESC_OCTET_LENGTH_PTR, or SQL_DESC_INDICATOR_PTR, the record becomes unbound.  
  
 Descriptor header fields are set by calling **SQLSetDescField** with the appropriate *FieldIdentifier*. Many header fields are also statement attributes, so they can also be set by a call to **SQLSetStmtAttr**. This allows applications to set a descriptor field without first obtaining a descriptor handle. When **SQLSetDescField** is called to set a header field, the *RecNumber* argument is ignored.  
  
 A *RecNumber* of 0 is used to set bookmark fields.  
  
> [!NOTE]  
>  The statement attribute SQL_ATTR_USE_BOOKMARKS should always be set before calling **SQLSetDescField** to set bookmark fields. While this is not mandatory, it is strongly recommended.  
  
## Sequence of Setting Descriptor Fields  
 When setting descriptor fields by calling **SQLSetDescField**, the application must follow a specific sequence:  
  
1.  The application must first set the SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE, or SQL_DESC_DATETIME_INTERVAL_CODE field.  
  
2.  After one of these fields has been set, the application can set an attribute of a data type, and the driver sets data type attribute fields to the appropriate default values for the data type. Automatic defaulting of type attribute fields ensures that the descriptor is always ready to use once the application has specified a data type. If the application explicitly sets a data type attribute, it is overriding the default attribute.  
  
3.  After one of the fields listed in step 1 has been set, and data type attributes have been set, the application can set SQL_DESC_DATA_PTR. This prompts a consistency check of descriptor fields. If the application changes the data type or attributes after setting the SQL_DESC_DATA_PTR field, the driver sets SQL_DESC_DATA_PTR to a null pointer, unbinding the record. This forces the application to complete the proper steps in sequence, before the descriptor record is usable.  
  
## Initialization of Descriptor Fields  
 When a descriptor is allocated, the fields in the descriptor can be initialized to a default value, be initialized without a default value, or be undefined for the type of descriptor. The following tables indicate the initialization of each field for each type of descriptor, with "D" indicating that the field is initialized with a default, and "ND" indicating that the field is initialized without a default. If a number is shown, the default value of the field is that number. The tables also indicate whether a field is read/write (R/W) or read-only (R).  
  
 The fields of an IRD have a default value only after the statement has been prepared or executed and the IRD has been populated, not when the statement handle or descriptor has been allocated. Until the IRD has been populated, any attempt to gain access to a field of an IRD will return an error.  
  
 Some descriptor fields are defined for one or more, but not all, of the descriptor types (ARDs and IRDs, and APDs and IPDs). When a field is undefined for a type of descriptor, it is not needed by any of the functions that use that descriptor.  
  
 The fields that can be accessed by **SQLGetDescField** cannot necessarily be set by **SQLSetDescField**. Fields that can be set by **SQLSetDescField** are listed in the following tables.  
  
 The initialization of header fields is outlined in the table that follows.  
  
|Header field name|Type|R/W|Default|  
|-----------------------|----------|----------|-------------|  
|SQL_DESC_ALLOC_TYPE|SQLSMALLINT|ARD: R APD: R IRD: R IPD: R|ARD: SQL_DESC_ALLOC_AUTO for implicit or SQL_DESC_ALLOC_USER for explicit<br /><br /> APD: SQL_DESC_ALLOC_AUTO for implicit or SQL_DESC_ALLOC_USER for explicit<br /><br /> IRD: SQL_DESC_ALLOC_AUTO<br /><br /> IPD: SQL_DESC_ALLOC_AUTO|  
|SQL_DESC_ARRAY_SIZE|SQLULEN|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD:[1] APD:[1] IRD: Unused IPD: Unused|  
|SQL_DESC_ARRAY_STATUS_PTR|SQLUSMALLINT*|ARD: R/W APD: R/W IRD: R/W IPD: R/W|ARD: Null ptr APD: Null ptr IRD: Null ptr IPD: Null ptr|  
|SQL_DESC_BIND_OFFSET_PTR|SQLLEN*|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD: Null ptr APD: Null ptr IRD: Unused IPD: Unused|  
|SQL_DESC_BIND_TYPE|SQLINTEGER|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD: SQL_BIND_BY_COLUMN<br /><br /> APD: SQL_BIND_BY_COLUMN<br /><br /> IRD: Unused<br /><br /> IPD: Unused|  

SQL_DESC_COUNT|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: 0 APD: 0 IRD: D IPD: 0|  

|SQL_DESC_ROWS_PROCESSED_PTR|SQLULEN*|ARD: Unused APD: Unused IRD: R/W IPD: R/W|ARD: Unused APD: Unused IRD: Null ptr IPD: Null ptr|  
  
 [1]   These fields are defined only when the IPD is automatically populated by the driver. If not, they are undefined. If an application attempts to set these fields, SQLSTATE HY091 (Invalid descriptor field identifier) will be returned.  
  
 The initialization of record fields is as shown in the following table.  
  
|Record field name|Type|R/W|Default|  
|-----------------------|----------|----------|-------------|  
|SQL_DESC_AUTO_UNIQUE_VALUE|SQLINTEGER|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_BASE_COLUMN_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_BASE_TABLE_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_CASE_SENSITIVE|SQLINTEGER|ARD: Unused APD: Unused IRD: R IPD: R|ARD: Unused APD: Unused IRD: D IPD: D[1]|  
|SQL_DESC_CATALOG_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_CONCISE_TYPE|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: SQL_C_ DEFAULT APD: SQL_C_ DEFAULT IRD: D IPD: ND|  
|SQL_DESC_DATA_PTR|SQLPOINTER|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD: Null ptr APD: Null ptr IRD: Unused IPD: Unused[2]|  
|SQL_DESC_DATETIME_INTERVAL_CODE|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|SQLINTEGER|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_DISPLAY_SIZE|SQLLEN|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_FIXED_PREC_SCALE|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: R|ARD: Unused APD: Unused IRD: D IPD: D[1]|  
|SQL_DESC_INDICATOR_PTR|SQLLEN *|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD: Null ptr APD: Null ptr IRD: Unused IPD: Unused|  
|SQL_DESC_LABEL|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_LENGTH|SQLULEN|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_LITERAL_PREFIX|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_LITERAL_SUFFIX|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_LOCAL_TYPE_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: R|ARD: Unused APD: Unused IRD: D IPD: D[1]|  
|SQL_DESC_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_NULLABLE|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: R|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_NUM_PREC_RADIX|SQLINTEGER|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_OCTET_LENGTH|SQLLEN|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_OCTET_LENGTH_PTR|SQLLEN *|ARD: R/W APD: R/W IRD: Unused IPD: Unused|ARD: Null ptr APD: Null ptr IRD: Unused IPD: Unused|  
|SQL_DESC_PARAMETER_TYPE|SQLSMALLINT|ARD: Unused APD: Unused IRD: Unused IPD: R/W|ARD: Unused APD: Unused IRD: Unused IPD: D=SQL_PARAM_INPUT|  
|SQL_DESC_PRECISION|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_ROWVER|SQLSMALLINT|ARD: Unused<br /><br /> APD: Unused<br /><br /> IRD: R<br /><br /> IPD: R|ARD: Unused<br /><br /> APD: Unused<br /><br /> IRD: ND<br /><br /> IPD: ND|  
|SQL_DESC_SCALE|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_SCHEMA_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_SEARCHABLE|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_TABLE_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
|SQL_DESC_TYPE|SQLSMALLINT|ARD: R/W APD: R/W IRD: R IPD: R/W|ARD: SQL_C_DEFAULT APD: SQL_C_DEFAULT IRD: D IPD: ND|  
|SQL_DESC_TYPE_NAME|SQLCHAR *|ARD: Unused APD: Unused IRD: R IPD: R|ARD: Unused APD: Unused IRD: D IPD: D[1]|  
|SQL_DESC_UNNAMED|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: R/W|ARD: ND APD: ND IRD: D IPD: ND|  
|SQL_DESC_UNSIGNED|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: R|ARD: Unused APD: Unused IRD: D IPD: D[1]|  
|SQL_DESC_UPDATABLE|SQLSMALLINT|ARD: Unused APD: Unused IRD: R IPD: Unused|ARD: Unused APD: Unused IRD: D IPD: Unused|  
  
 [1]   These fields are defined only when the IPD is automatically populated by the driver. If not, they are undefined. If an application attempts to set these fields, SQLSTATE HY091 (Invalid descriptor field identifier) will be returned.  
  
 [2]   The SQL_DESC_DATA_PTR field in the IPD can be set to force a consistency check. In a subsequent call to **SQLGetDescField** or **SQLGetDescRec**, the driver is not required to return the value that SQL_DESC_DATA_PTR was set to.  
  
## FieldIdentifier Argument  
 The *FieldIdentifier* argument indicates the descriptor field to be set. A descriptor contains the *descriptor header,* consisting of the header fields described in the next section, "Header Fields," and zero or more *descriptor records,* consisting of the record fields described in the section following the "Header Fields" section.  
  
## Header Fields  
 Each descriptor has a header consisting of the following fields:  
  
 **SQL_DESC_ALLOC_TYPE [All]**  
 This read-only SQLSMALLINT header field specifies whether the descriptor was allocated automatically by the driver or explicitly by the application. The application can obtain, but not modify, this field. The field is set to SQL_DESC_ALLOC_AUTO by the driver if the descriptor was automatically allocated by the driver. It is set to SQL_DESC_ALLOC_USER by the driver if the descriptor was explicitly allocated by the application.  
  
 **SQL_DESC_ARRAY_SIZE [Application descriptors]**  
 In ARDs, this SQLULEN header field specifies the number of rows in the rowset. This is the number of rows to be returned by a call to **SQLFetch** or **SQLFetchScroll** or to be operated on by a call to **SQLBulkOperations** or **SQLSetPos**.  
  
 In APDs, this SQLULEN header field specifies the number of values for each parameter.  
  
 The default value of this field is 1. If SQL_DESC_ARRAY_SIZE is greater than 1, SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR of the APD or ARD point to arrays. The cardinality of each array is equal to the value of this field.  
  
 This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROW_ARRAY_SIZE attribute. This field in the APD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAMSET_SIZE attribute.  
  
 **SQL_DESC_ARRAY_STATUS_PTR [All]**  
 For each descriptor type, this SQLUSMALLINT * header field points to an array of SQLUSMALLINT values. These arrays are named as follows: row status array (IRD), parameter status array (IPD), row operation array (ARD), and parameter operation array (APD).  
  
 In the IRD, this header field points to a row status array containing status values after a call to **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos**. The array has as many elements as there are rows in the rowset. The application must allocate an array of SQLUSMALLINTs and set this field to point to the array. The field is set to a null pointer by default. The driver will populate the array - unless the SQL_DESC_ARRAY_STATUS_PTR field is set to a null pointer, in which case no status values are generated and the array is not populated.  
  
> [!CAUTION]  
>  Driver behavior is undefined if the application sets the elements of the row status array pointed to by the SQL_DESC_ARRAY_STATUS_PTR field of the IRD.  
  
 The array is initially populated by a call to **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos**. If the call did not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the array pointed to by this field are undefined. The elements in the array can contain the following values:  
  
-   SQL_ROW_SUCCESS: The row was successfully fetched and has not changed since it was last fetched.  
  
-   SQL_ROW_SUCCESS_WITH_INFO: The row was successfully fetched and has not changed since it was last fetched. However, a warning was returned about the row.  
  
-   SQL_ROW_ERROR: An error occurred while fetching the row.  
  
-   SQL_ROW_UPDATED: The row was successfully fetched and has been updated since it was last fetched. If the row is fetched again, its status is SQL_ROW_SUCCESS.  
  
-   SQL_ROW_DELETED: The row has been deleted since it was last fetched.  
  
-   SQL_ROW_ADDED: The row was inserted by **SQLBulkOperations**. If the row is fetched again, its status is SQL_ROW_SUCCESS.  
  
-   SQL_ROW_NOROW: The rowset overlapped the end of the result set, and no row was returned that corresponded to this element of the row status array.  
  
 This field in the IRD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROW_STATUS_PTR attribute.  
  
 The SQL_DESC_ARRAY_STATUS_PTR field of the IRD is valid only after SQL_SUCCESS or SQL_SUCCESS_WITH_INFO has been returned. If the return code is not one of these, the location pointed to by SQL_DESC_ROWS_PROCESSED_PTR is undefined.  
  
 In the IPD, this header field points to a parameter status array containing status information for each set of parameter values after a call to **SQLExecute** or **SQLExecDirect**. If the call to **SQLExecute** or **SQLExecDirect** did not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the array pointed to by this field are undefined. The application must allocate an array of SQLUSMALLINTs and set this field to point to the array. The driver will populate the array - unless the SQL_DESC_ARRAY_STATUS_PTR field is set to a null pointer, in which case no status values are generated and the array is not populated. The elements in the array can contain the following values:  
  
-   SQL_PARAM_SUCCESS: The SQL statement was successfully executed for this set of parameters.  
  
-   SQL_PARAM_SUCCESS_WITH_INFO: The SQL statement was successfully executed for this set of parameters; however, warning information is available in the diagnostics data structure.  
  
-   SQL_PARAM_ERROR: An error occurred in processing this set of parameters. Additional error information is available in the diagnostics data structure.  
  
-   SQL_PARAM_UNUSED: This parameter set was unused, possibly due to the fact that some previous parameter set caused an error that aborted further processing, or because SQL_PARAM_IGNORE was set for that set of parameters in the array specified by the SQL_DESC_ARRAY_STATUS_PTR field of the APD.  
  
-   SQL_PARAM_DIAG_UNAVAILABLE: Diagnostic information is not available. An example of this is when the driver treats arrays of parameters as a monolithic unit and so does not generate this level of error information.  
  
 This field in the IPD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAM_STATUS_PTR attribute.  
  
 In the ARD, this header field points to a row operation array of values that can be set by the application to indicate whether this row is to be ignored for **SQLSetPos** operations. The elements in the array can contain the following values:  
  
-   SQL_ROW_PROCEED: The row is included in the bulk operation using **SQLSetPos**. (This setting does not guarantee that the operation will occur on the row. If the row has the status SQL_ROW_ERROR in the IRD row status array, the driver might not be able to perform the operation in the row.)  
  
-   SQL_ROW_IGNORE: The row is excluded from the bulk operation using **SQLSetPos**.  
  
 If no elements of the array are set, all rows are included in the bulk operation. If the value in the SQL_DESC_ARRAY_STATUS_PTR field of the ARD is a null pointer, all rows are included in the bulk operation; the interpretation is the same as if the pointer pointed to a valid array and all elements of the array were SQL_ROW_PROCEED. If an element in the array is set to SQL_ROW_IGNORE, the value in the row status array for the ignored row is not changed.  
  
 This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROW_OPERATION_PTR attribute.  
  
 In the APD, this header field points to a parameter operation array of values that can be set by the application to indicate whether this set of parameters is to be ignored when **SQLExecute** or **SQLExecDirect** is called. The elements in the array can contain the following values:  
  
-   SQL_PARAM_PROCEED: The set of parameters is included in the **SQLExecute** or **SQLExecDirect** call.  
  
-   SQL_PARAM_IGNORE: The set of parameters is excluded from the **SQLExecute** or **SQLExecDirect** call.  
  
 If no elements of the array are set, all sets of parameters in the array are used in the **SQLExecute** or **SQLExecDirect** calls. If the value in the SQL_DESC_ARRAY_STATUS_PTR field of the APD is a null pointer, all sets of parameters are used; the interpretation is the same as if the pointer pointed to a valid array and all elements of the array were SQL_PARAM_PROCEED.  
  
 This field in the APD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAM_OPERATION_PTR attribute.  
  
 **SQL_DESC_BIND_OFFSET_PTR [Application descriptors]**  
 This SQLLEN * header field points to the binding offset. It is set to a null pointer by default. If this field is not a null pointer, the driver dereferences the pointer and adds the dereferenced value to each of the deferred fields that has a non-null value in the descriptor record (SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR) at fetch time and uses the new pointer values when binding.  
  
 The binding offset is always added directly to the values in the SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR fields. If the offset is changed to a different value, the new value is still added directly to the value in each descriptor field. The new offset is not added to the field value plus any earlier offset.  
  
 This field is a *deferred field*: It is not used at the time it is set but is used at a later time by the driver when it needs to determine addresses for data buffers.  
  
 This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROW_BIND_OFFSET_PTR attribute. This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAM_BIND_OFFSET_PTR attribute.  
  
 For more information, see the description of row-wise binding in [SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md) and [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md).  
  
 **SQL_DESC_BIND_TYPE [Application descriptors]**  
 This SQLUINTEGER header field sets the binding orientation to be used for binding either columns or parameters.  
  
 In ARDs, this field specifies the binding orientation when **SQLFetchScroll** or **SQLFetch** is called on the associated statement handle.  
  
 To select column-wise binding for columns, this field is set to SQL_BIND_BY_COLUMN (the default).  
  
 This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROW_BIND_TYPE *Attribute*.  
  
 In APDs, this field specifies the binding orientation to be used for dynamic parameters.  
  
 To select column-wise binding for parameters, this field is set to SQL_BIND_BY_COLUMN (the default).  
  
 This field in the APD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAM_BIND_TYPE *Attribute*.  
  
 **SQL_DESC_COUNT [All]**  
 This SQLSMALLINT header field specifies the 1-based index of the highest-numbered record that contains data. When the driver sets the data structure for the descriptor, it must also set the SQL_DESC_COUNT field to show how many records are significant. When an application allocates an instance of this data structure, it does not have to specify how many records to reserve room for. As the application specifies the contents of the records, the driver takes any required action to ensure that the descriptor handle refers to a data structure of the adequate size.  
  
 SQL_DESC_COUNT is not a count of all data columns that are bound (if the field is in an ARD) or of all parameters that are bound (if the field is in an APD), but the number of the highest-numbered record. If the highest-numbered column or parameter is unbound, then SQL_DESC_COUNT is changed to the number of the next highest-numbered column or parameter. If a column or a parameter with a number that is less than the number of the highest-numbered column is unbound (by calling **SQLBindCol** with the *TargetValuePtr* argument set to a null pointer, or **SQLBindParameter** with the *ParameterValuePtr* argument set to a null pointer), SQL_DESC_COUNT is not changed. If additional columns or parameters are bound with numbers greater than the highest-numbered record that contains data, the driver automatically increases the value in the SQL_DESC_COUNT field. If all columns are unbound by calling **SQLFreeStmt** with the SQL_UNBIND option, the SQL_DESC_COUNT fields in the ARD and IRD are set to 0. If **SQLFreeStmt** is called with the SQL_RESET_PARAMS option, the SQL_DESC_COUNT fields in the APD and IPD are set to 0.  
  
 The value in SQL_DESC_COUNT can be set explicitly by an application by calling **SQLSetDescField**. If the value in SQL_DESC_COUNT is explicitly decreased, all records with numbers greater than the new value in SQL_DESC_COUNT are effectively removed. If the value in SQL_DESC_COUNT is explicitly set to 0 and the field is in an ARD, all data buffers except a bound bookmark column are released.  
  
 The record count in this field of an ARD does not include a bound bookmark column. The only way to unbind a bookmark column is to set the SQL_DESC_DATA_PTR field to a null pointer.  
  
 **SQL_DESC_ROWS_PROCESSED_PTR [Implementation descriptors]**  
 In an IRD, this SQLULEN \* header field points to a buffer containing the number of rows fetched after a call to **SQLFetch** or **SQLFetchScroll**, or the number of rows affected in a bulk operation performed by a call to **SQLBulkOperations** or **SQLSetPos**, including error rows.  
  
 In an IPD, this SQLUINTEGER * header field points to a buffer containing the number of sets of parameters that have been processed, including error sets. No number will be returned if this is a null pointer.  
  
 SQL_DESC_ROWS_PROCESSED_PTR is valid only after SQL_SUCCESS or SQL_SUCCESS_WITH_INFO has been returned after a call to **SQLFetch** or **SQLFetchScroll** (for an IRD field) or **SQLExecute**, **SQLExecDirect**, or **SQLParamData** (for an IPD field). If the call that fills in the buffer pointed to by this field does not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the buffer are undefined, unless it returns SQL_NO_DATA, in which case the value in the buffer is set to 0.  
  
 This field in the ARD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_ROWS_FETCHED_PTR attribute. This field in the APD can also be set by calling **SQLSetStmtAttr** with the SQL_ATTR_PARAMS_PROCESSED_PTR attribute.  
  
 The buffer pointed to by this field is allocated by the application. It is a deferred output buffer that is set by the driver. It is set to a null pointer by default.  
  
## Record Fields  
 Each descriptor contains one or more records consisting of fields that define either column data or dynamic parameters, depending on the type of descriptor. Each record is a complete definition of a single column or parameter.  
  
 **SQL_DESC_AUTO_UNIQUE_VALUE [IRDs]**  
 This read-only SQLINTEGER record field contains SQL_TRUE if the column is an auto-incrementing column, or SQL_FALSE if the column is not an auto-incrementing column. This field is read-only, but the underlying auto-incrementing column is not necessarily read-only.  
  
 **SQL_DESC_BASE_COLUMN_NAME [IRDs]**  
 This read-only SQLCHAR * record field contains the base column name for the result set column. If a base column name does not exist (as in the case of columns that are expressions), this variable contains an empty string.  
  
 **SQL_DESC_BASE_TABLE_NAME [IRDs]**  
 This read-only SQLCHAR * record field contains the base table name for the result set column. If a base table name cannot be defined or is not applicable, this variable contains an empty string.  
  
 **SQL_DESC_CASE_SENSITIVE [Implementation descriptors]**  
 This read-only SQLINTEGER record field contains SQL_TRUE if the column or parameter is treated as case-sensitive for collations and comparisons, or SQL_FALSE if the column is not treated as case-sensitive for collations and comparisons or if it is a noncharacter column.  
  
 **SQL_DESC_CATALOG_NAME [IRDs]**  
 This read-only SQLCHAR * record field contains the catalog for the base table that contains the column. The return value is driver-dependent if the column is an expression or if the column is part of a view. If the data source does not support catalogs or the catalog cannot be determined, this variable contains an empty string.  
  
 **SQL_DESC_CONCISE_TYPE [All]**  
 This SQLSMALLINT header field specifies the concise data type for all data types, including the datetime and interval data types.  
  
 The values in the SQL_DESC_CONCISE_TYPE, SQL_DESC_TYPE, and SQL_DESC_DATETIME_INTERVAL_CODE fields are interdependent. Each time one of the fields is set, the other must also be set. SQL_DESC_CONCISE_TYPE can be set by a call to **SQLBindCol** or **SQLBindParameter**, or **SQLSetDescField**. SQL_DESC_TYPE can be set by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
 If SQL_DESC_CONCISE_TYPE is set to a concise data type other than an interval or datetime data type, the SQL_DESC_TYPE field is set to the same value and the SQL_DESC_DATETIME_INTERVAL_CODE field is set to 0.  
  
 If SQL_DESC_CONCISE_TYPE is set to the concise datetime or interval data type, the SQL_DESC_TYPE field is set to the corresponding verbose type (SQL_DATETIME or SQL_INTERVAL) and the SQL_DESC_DATETIME_INTERVAL_CODE field is set to the appropriate subcode.  
  
 **SQL_DESC_DATA_PTR [Application descriptors and IPDs]**  
 This SQLPOINTER record field points to a variable that will contain the parameter value (for APDs) or the column value (for ARDs). This field is a *deferred field*. It is not used at the time it is set but is used at a later time by the driver to retrieve data.  
  
 The column specified by the SQL_DESC_DATA_PTR field of the ARD is unbound if the *TargetValuePtr* argument in a call to **SQLBindCol** is a null pointer or if the SQL_DESC_DATA_PTR field in the ARD is set by a call to **SQLSetDescField** or **SQLSetDescRec** to a null pointer. Other fields are not affected if the SQL_DESC_DATA_PTR field is set to a null pointer.  
  
 If the call to **SQLFetch** or **SQLFetchScroll** that fills in the buffer pointed to by this field did not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the buffer are undefined.  
  
 Whenever the SQL_DESC_DATA_PTR field of an APD, ARD, or IPD is set, the driver checks that the value in the SQL_DESC_TYPE field contains one of the valid ODBC C data types or a driver-specific data type, and that all other fields affecting the data types are consistent. Prompting a consistency check is the only use of the SQL_DESC_DATA_PTR field of an IPD. Specifically, if an application sets the SQL_DESC_DATA_PTR field of an IPD and later calls **SQLGetDescField** on this field, it is not necessarily returned the value that it had set. For more information, see "Consistency Checks" in [SQLSetDescRec](../../../odbc/reference/syntax/sqlsetdescrec-function.md).  
  
 **SQL_DESC_DATETIME_INTERVAL_CODE [All]**  
 This SQLSMALLINT record field contains the subcode for the specific datetime or interval data type when the SQL_DESC_TYPE field is SQL_DATETIME or SQL_INTERVAL. This is true for both SQL and C data types. The code consists of the data type name with "CODE" substituted for either "TYPE" or "C_TYPE" (for datetime types), or "CODE" substituted for "INTERVAL" or "C_INTERVAL" (for interval types).  
  
 If SQL_DESC_TYPE and SQL_DESC_CONCISE_TYPE in an application descriptor are set to SQL_C_DEFAULT and the descriptor is not associated with a statement handle, the contents of SQL_DESC_DATETIME_INTERVAL_CODE are undefined.  
  
 This field can be set for the datetime data types listed in the following table.  
  
|Datetime types|DATETIME_INTERVAL_CODE|  
|--------------------|------------------------------|  
|SQL_TYPE_DATE/SQL_C_TYPE_DATE|SQL_CODE_DATE|  
|SQL_TYPE_TIME/SQL_C_TYPE_TIME|SQL_CODE_TIME|  
|SQL_TYPE_TIMESTAMP/ SQL_C_TYPE_TIMESTAMP|SQL_CODE_TIMESTAMP|  
  
 This field can be set for the interval data types listed in the following table.  
  
|Interval type|DATETIME_INTERVAL_CODE|  
|-------------------|------------------------------|  
|SQL_INTERVAL_DAY/ SQL_C_INTERVAL_DAY|SQL_CODE_DAY|  
|SQL_INTERVAL_DAY_TO_HOUR/ SQL_C_INTERVAL_DAY_TO_HOUR|SQL_CODE_DAY_TO_HOUR|  
|SQL_INTERVAL_DAY_TO_MINUTE/ SQL_C_INTERVAL_DAY_TO_MINUTE|SQL_CODE_DAY_TO_MINUTE|  
|SQL_INTERVAL_DAY_TO_SECOND/ SQL_C_INTERVAL_DAY_TO_SECOND|SQL_CODE_DAY_TO_SECOND|  
|SQL_INTERVAL_HOUR/ SQL_C_INTERVAL_HOUR|SQL_CODE_HOUR|  
|SQL_INTERVAL_HOUR_TO_MINUTE/ SQL_C_INTERVAL_HOUR_TO_MINUTE|SQL_CODE_HOUR_TO_MINUTE|  
|SQL_INTERVAL_HOUR_TO_SECOND/ SQL_C_INTERVAL_HOUR_TO_SECOND|SQL_CODE_HOUR_TO_SECOND|  
|SQL_INTERVAL_MINUTE/ SQL_C_INTERVAL_MINUTE|SQL_CODE_MINUTE|  
|SQL_INTERVAL_MINUTE_TO_SECOND/ SQL_C_INTERVAL_MINUTE_TO_SECOND|SQL_CODE_MINUTE_TO_SECOND|  
|SQL_INTERVAL_MONTH/ SQL_C_INTERVAL_MONTH|SQL_CODE_MONTH|  
|SQL_INTERVAL_SECOND/ SQL_C_INTERVAL_SECOND|SQL_CODE_SECOND|  
|SQL_INTERVAL_YEAR/ SQL_C_INTERVAL_YEAR|SQL_CODE_YEAR|  
|SQL_INTERVAL_YEAR_TO_MONTH/ SQL_C_INTERVAL_YEAR_TO_MONTH|SQL_CODE_YEAR_TO_MONTH|  
  
 For more information about the data intervals and this field, see [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md).  
  
 **SQL_DESC_DATETIME_INTERVAL_PRECISION [All]**  
 This SQLINTEGER record field contains the interval leading precision if the SQL_DESC_TYPE field is SQL_INTERVAL. When the SQL_DESC_DATETIME_INTERVAL_CODE field is set to an interval data type, this field is set to the default interval leading precision.  
  
 **SQL_DESC_DISPLAY_SIZE [IRDs]**  
 This read-only SQLINTEGER record field contains the maximum number of characters required to display the data from the column.  
  
 **SQL_DESC_FIXED_PREC_SCALE [Implementation descriptors]**  
 This read-only SQLSMALLINT record field is set to SQL_TRUE if the column is an exact numeric column and has a fixed precision and nonzero scale, or to SQL_FALSE if the column is not an exact numeric column with a fixed precision and scale.  
  
 **SQL_DESC_INDICATOR_PTR [Application descriptors]**  
 In ARDs, this SQLLEN * record field points to the indicator variable. This variable contains SQL_NULL_DATA if the column value is a NULL. For APDs, the indicator variable is set to SQL_NULL_DATA to specify NULL dynamic arguments. Otherwise, the variable is zero (unless the values in SQL_DESC_INDICATOR_PTR and SQL_DESC_OCTET_LENGTH_PTR are the same pointer).  
  
 If the SQL_DESC_INDICATOR_PTR field in an ARD is a null pointer, the driver is prevented from returning information about whether the column is NULL or not. If the column is NULL and SQL_DESC_INDICATOR_PTR is a null pointer, SQLSTATE 22002 (Indicator variable required but not supplied) is returned when the driver attempts to populate the buffer after a call to **SQLFetch** or **SQLFetchScroll**. If the call to **SQLFetch** or **SQLFetchScroll** did not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the buffer are undefined.  
  
 The SQL_DESC_INDICATOR_PTR field determines whether the field pointed to by SQL_DESC_OCTET_LENGTH_PTR is set. If the data value for a column is NULL, the driver sets the indicator variable to SQL_NULL_DATA. The field pointed to by SQL_DESC_OCTET_LENGTH_PTR is then not set. If a NULL value is not encountered during the fetch, the buffer pointed to by SQL_DESC_INDICATOR_PTR is set to zero and the buffer pointed to by SQL_DESC_OCTET_LENGTH_PTR is set to the length of the data.  
  
 If the SQL_DESC_INDICATOR_PTR field in an APD is a null pointer, the application cannot use this descriptor record to specify NULL arguments.  
  
 This field is a *deferred field*: It is not used at the time it is set but is used at a later time by the driver to indicate nullability (for ARDs) or to determine nullability (for APDs).  
  
 **SQL_DESC_LABEL [IRDs]**  
 This read-only SQLCHAR * record field contains the column label or title. If the column does not have a label, this variable contains the column name. If the column is unnamed and unlabeled, this variable contains an empty string.  
  
 **SQL_DESC_LENGTH [All]**  
 This SQLULEN record field is either the maximum or actual length of a character string in characters or a binary data type in bytes. It is the maximum length for a fixed-length data type, or the actual length for a variable-length data type. Its value always excludes the null-termination character that ends the character string. For values whose type is SQL_TYPE_DATE, SQL_TYPE_TIME, SQL_TYPE_TIMESTAMP, or one of the SQL interval data types, this field has the length in characters of the character string representation of the datetime or interval value.  
  
 The value in this field may be different from the value for "length" as defined in ODBC 2*.x*. For more information, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).  
  
 **SQL_DESC_LITERAL_PREFIX [IRDs]**  
 This read-only SQLCHAR * record field contains the character or characters that the driver recognizes as a prefix for a literal of this data type. This variable contains an empty string for a data type for which a literal prefix is not applicable.  
  
 **SQL_DESC_LITERAL_SUFFIX [IRDs]**  
 This read-only SQLCHAR * record field contains the character or characters that the driver recognizes as a suffix for a literal of this data type. This variable contains an empty string for a data type for which a literal suffix is not applicable.  
  
 **SQL_DESC_LOCAL_TYPE_NAME [Implementation descriptors]**  
 This read-only SQLCHAR * record field contains any localized (native language) name for the data type that may be different from the regular name of the data type. If there is no localized name, an empty string is returned. This field is for display purposes only.  
  
 **SQL_DESC_NAME [Implementation descriptors]**  
 This SQLCHAR * record field in a row descriptor contains the column alias, if it applies. If the column alias does not apply, the column name is returned. In either case, the driver sets the SQL_DESC_UNNAMED field to SQL_NAMED when it sets the SQL_DESC_NAME field. If there is no column name or a column alias, the driver returns an empty string in the SQL_DESC_NAME field and sets the SQL_DESC_UNNAMED field to SQL_UNNAMED.  
  
 An application can set the SQL_DESC_NAME field of an IPD to a parameter name or alias to specify stored procedure parameters by name. (For more information, see [Binding Parameters by Name (Named Parameters)](../../../odbc/reference/develop-app/binding-parameters-by-name-named-parameters.md).) The SQL_DESC_NAME field of an IRD is a read-only field; SQLSTATE HY091 (Invalid descriptor field identifier) will be returned if an application attempts to set it.  
  
 In IPDs, this field is undefined if the driver does not support named parameters. If the driver supports named parameters and is capable of describing parameters, the parameter name is returned in this field.  
  
 **SQL_DESC_NULLABLE [Implementation descriptors]**  
 In IRDs, this read-only SQLSMALLINT record field is SQL_NULLABLE if the column can have NULL values, SQL_NO_NULLS if the column does not have NULL values, or SQL_NULLABLE_UNKNOWN if it is not known whether the column accepts NULL values. This field pertains to the result set column, not the base column.  
  
 In IPDs, this field is always set to SQL_NULLABLE because dynamic parameters are always nullable and cannot be set by an application.  
  
 **SQL_DESC_NUM_PREC_RADIX [All]**  
 This SQLINTEGER field contains a value of 2 if the data type in the SQL_DESC_TYPE field is an approximate numeric data type, because the SQL_DESC_PRECISION field contains the number of bits. This field contains a value of 10 if the data type in the SQL_DESC_TYPE field is an exact numeric data type, because the SQL_DESC_PRECISION field contains the number of decimal digits. This field is set to 0 for all non-numeric data types.  
  
 **SQL_DESC_OCTET_LENGTH [All]**  
 This SQLLEN record field contains the length, in bytes, of a character string or binary data type. For fixed-length character or binary types, this is the actual length in bytes. For variable-length character or binary types, this is the maximum length in bytes. This value always excludes space for the null-termination character for implementation descriptors and always includes space for the null-termination character for application descriptors. For application data, this field contains the size of the buffer. For APDs, this field is defined only for output or input/output parameters.  
  
 **SQL_DESC_OCTET_LENGTH_PTR [Application descriptors]**  
 This SQLLEN * record field points to a variable that will contain the total length in bytes of a dynamic argument (for parameter descriptors) or of a bound column value (for row descriptors).  
  
 For an APD, this value is ignored for all arguments except character string and binary; if this field points to SQL_NTS, the dynamic argument must be null-terminated. To indicate that a bound parameter will be a data-at-execution parameter, an application sets this field in the appropriate record of the APD to a variable that, at execute time, will contain the value SQL_DATA_AT_EXEC or the result of the SQL_LEN_DATA_AT_EXEC macro. If there is more than one such field, SQL_DESC_DATA_PTR can be set to a value uniquely identifying the parameter to help the application determine which parameter is being requested.  
  
 If the OCTET_LENGTH_PTR field of an ARD is a null pointer, the driver does not return length information for the column. If the SQL_DESC_OCTET_LENGTH_PTR field of an APD is a null pointer, the driver assumes that character strings and binary values are null-terminated. (Binary values should not be null-terminated but should be given a length to avoid truncation.)  
  
 If the call to **SQLFetch** or **SQLFetchScroll** that fills in the buffer pointed to by this field did not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the contents of the buffer are undefined. This field is a *deferred field*. It is not used at the time it is set but is used at a later time by the driver to determine or indicate the octet length of the data.  
  
 **SQL_DESC_PARAMETER_TYPE [IPDs]**  
 This SQLSMALLINT record field is set to SQL_PARAM_INPUT for an input parameter, SQL_PARAM_INPUT_OUTPUT for an input/output parameter, SQL_PARAM_OUTPUT for an output parameter, SQL_PARAM_INPUT_OUTPUT_STREAM for an input/output streamed parameter, or SQL_PARAM_OUTPUT_STREAM for an output streamed parameter. It is set to SQL_PARAM_INPUT by default.  
  
 For an IPD, the field is set to SQL_PARAM_INPUT by default if the IPD is not automatically populated by the driver (the SQL_ATTR_ENABLE_AUTO_IPD statement attribute is SQL_FALSE). An application should set this field in the IPD for parameters that are not input parameters.  
  
 **SQL_DESC_PRECISION [All]**  
 This SQLSMALLINT record field contains the number of digits for an exact numeric type, the number of bits in the mantissa (binary precision) for an approximate numeric type, or the numbers of digits in the fractional seconds component for the SQL_TYPE_TIME, SQL_TYPE_TIMESTAMP, or SQL_INTERVAL_SECOND data type. This field is undefined for all other data types.  
  
 The value in this field may be different from the value for "precision" as defined in ODBC 2*.x*. For more information, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).  
  
 **SQL_DESC_ROWVER [Implementation descriptors]**  
 This SQLSMALLINTrecord field indicates whether a column is automatically modified by the DBMS when a row is updated (for example, a column of the type "timestamp" in SQL Server). The value of this record field is set to SQL_TRUE if the column is a row versioning column, and to SQL_FALSE otherwise. This column attribute is similar to calling **SQLSpecialColumns** with IdentifierType of SQL_ROWVER to determine whether a column is automatically updated.  
  
 **SQL_DESC_SCALE [All]**  
 This SQLSMALLINT record field contains the defined scale for decimal and numeric data types. The field is undefined for all other data types.  
  
 The value in this field may be different from the value for "scale" as defined in ODBC 2*.x*. For more information, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).  
  
 **SQL_DESC_SCHEMA_NAME [IRDs]**  
 This read-only SQLCHAR * record field contains the schema name of the base table that contains the column. The return value is driver-dependent if the column is an expression or if the column is part of a view. If the data source does not support schemas or the schema name cannot be determined, this variable contains an empty string.  
  
 **SQL_DESC_SEARCHABLE [IRDs]**  
 This read-only SQLSMALLINT record field is set to one of the following values:  
  
-   SQL_PRED_NONE if the column cannot be used in a **WHERE** clause. (This is the same as the SQL_UNSEARCHABLE value in ODBC 2*.x*.)  
  
-   SQL_PRED_CHAR if the column can be used in a **WHERE** clause but only with the **LIKE** predicate. (This is the same as the SQL_LIKE_ONLY value in ODBC 2*.x*.)  
  
-   SQL_PRED_BASIC if the column can be used in a **WHERE** clause with all the comparison operators except **LIKE**. (This is the same as the SQL_EXCEPT_LIKE value in ODBC 2*.x*.)  
  
-   SQL_PRED_SEARCHABLE if the column can be used in a **WHERE** clause with any comparison operator.  
  
 **SQL_DESC_TABLE_NAME [IRDs]**  
 This read-only SQLCHAR * record field contains the name of the base table that contains this column. The return value is driver-dependent if the column is an expression or if the column is part of a view.  
  
 **SQL_DESC_TYPE [All]**  
 This SQLSMALLINT record field specifies the concise SQL or C data type for all data types except datetime and interval data types. For the datetime and interval data types, this field specifies the verbose data type, which is SQL_DATETIME or SQL_INTERVAL.  
  
 Whenever this field contains SQL_DATETIME or SQL_INTERVAL, the SQL_DESC_DATETIME_INTERVAL_CODE field must contain the appropriate subcode for the concise type. For datetime data types, SQL_DESC_TYPE contains SQL_DATETIME, and the SQL_DESC_DATETIME_INTERVAL_CODE field contains a subcode for the specific datetime data type. For interval data types, SQL_DESC_TYPE contains SQL_INTERVAL and the SQL_DESC_DATETIME_INTERVAL_CODE field contains a subcode for the specific interval data type.  
  
 The values in the SQL_DESC_TYPE and SQL_DESC_CONCISE_TYPE fields are interdependent. Each time one of the fields is set, the other must also be set. SQL_DESC_TYPE can be set by a call to **SQLSetDescField** or **SQLSetDescRec**. SQL_DESC_CONCISE_TYPE can be set by a call to **SQLBindCol** or **SQLBindParameter**, or **SQLSetDescField**.  
  
 If SQL_DESC_TYPE is set to a concise data type other than an interval or datetime data type, the SQL_DESC_CONCISE_TYPE field is set to the same value and the SQL_DESC_DATETIME_INTERVAL_CODE field is set to 0.  
  
 If SQL_DESC_TYPE is set to the verbose datetime or interval data type (SQL_DATETIME or SQL_INTERVAL) and the SQL_DESC_DATETIME_INTERVAL_CODE field is set to the appropriate subcode, the SQL_DESC_CONCISE TYPE field is set to the corresponding concise type. Trying to set SQL_DESC_TYPE to one of the concise datetime or interval types will return SQLSTATE HY021 (Inconsistent descriptor information).  
  
 When the SQL_DESC_TYPE field is set by a call to **SQLBindCol**, **SQLBindParameter**, or **SQLSetDescField**, the following fields are set to the following default values, as shown in the table below. The values of the remaining fields of the same record are undefined.  
  
|Value of SQL_DESC_TYPE|Other fields implicitly set|  
|------------------------------|---------------------------------|  
|SQL_CHAR, SQL_VARCHAR, SQL_C_CHAR, SQL_C_VARCHAR|SQL_DESC_LENGTH is set to 1. SQL_DESC_PRECISION is set to 0.|  
|SQL_DATETIME|When SQL_DESC_DATETIME_INTERVAL_CODE is set to SQL_CODE_DATE or SQL_CODE_TIME, SQL_DESC_PRECISION is set to 0. When it is set to SQL_DESC_TIMESTAMP, SQL_DESC_PRECISION is set to 6.|  
|SQL_DECIMAL, SQL_NUMERIC, SQL_C_NUMERIC|SQL_DESC_SCALE is set to 0. SQL_DESC_PRECISION is set to the implementation-defined precision for the respective data type.<br /><br /> See [SQL to C: Numeric](../../../odbc/reference/appendixes/sql-to-c-numeric.md) for information on how to manually bind a SQL_C_NUMERIC value.|  
|SQL_FLOAT, SQL_C_FLOAT|SQL_DESC_PRECISION is set to the implementation-defined default precision for SQL_FLOAT.|  
|SQL_INTERVAL|When SQL_DESC_DATETIME_INTERVAL_CODE is set to an interval data type, SQL_DESC_DATETIME_INTERVAL_PRECISION is set to 2 (the default interval leading precision). When the interval has a seconds component, SQL_DESC_PRECISION is set to 6 (the default interval seconds precision).|  
  
 When an application calls **SQLSetDescField** to set fields of a descriptor rather than calling **SQLSetDescRec**, the application must first declare the data type. When it does, the other fields indicated in the previous table are implicitly set. If any of the values implicitly set are unacceptable, the application can then call **SQLSetDescField** or **SQLSetDescRec** to set the unacceptable value explicitly.  
  
 **SQL_DESC_TYPE_NAME [Implementation descriptors]**  
 This read-only SQLCHAR * record field contains the data source-dependent type name (for example, "CHAR", "VARCHAR", and so on). If the data type name is unknown, this variable contains an empty string.  
  
 **SQL_DESC_UNNAMED [Implementation descriptors]**  
 This SQLSMALLINT record field in a row descriptor is set by the driver to either SQL_NAMED or SQL_UNNAMED when it sets the SQL_DESC_NAME field. If the SQL_DESC_NAME field contains a column alias or if the column alias does not apply, the driver sets the SQL_DESC_UNNAMED field to SQL_NAMED. If an application sets the SQL_DESC_NAME field of an IPD to a parameter name or alias, the driver sets the SQL_DESC_UNNAMED field of the IPD to SQL_NAMED. If there is no column name or a column alias, the driver sets the SQL_DESC_UNNAMED field to SQL_UNNAMED.  
  
 An application can set the SQL_DESC_UNNAMED field of an IPD to SQL_UNNAMED. A driver returns SQLSTATE HY091 (Invalid descriptor field identifier) if an application attempts to set the SQL_DESC_UNNAMED field of an IPD to SQL_NAMED. The SQL_DESC_UNNAMED field of an IRD is read-only; SQLSTATE HY091 (Invalid descriptor field identifier) will be returned if an application attempts to set it.  
  
 **SQL_DESC_UNSIGNED [Implementation descriptors]**  
 This read-only SQLSMALLINT record field is set to SQL_TRUE if the column type is unsigned or non-numeric, or SQL_FALSE if the column type is signed.  
  
 **SQL_DESC_UPDATABLE [IRDs]**  
 This read-only SQLSMALLINT record field is set to one of the following values:  
  
-   SQL_ATTR_READ_ONLY if the result set column is read-only.  
  
-   SQL_ATTR_WRITE if the result set column is read-write.  
  
-   SQL_ATTR_READWRITE_UNKNOWN if it is not known whether the result set column is updatable or not.  
  
 SQL_DESC_UPDATABLE describes the updatability of the column in the result set, not the column in the base table. The updatability of the column in the base table on which this result set column is based may be different than the value in this field. Whether a column is updatable can be based on the data type, user privileges, and the definition of the result set itself. If it is unclear whether a column is updatable, SQL_ATTR_READWRITE_UNKNOWN should be returned.  
  
## Consistency Checks  
 A consistency check is performed by the driver automatically whenever an application passes in a value for the SQL_DESC_DATA_PTR field of the ARD, APD, or IPD. If any of the fields is inconsistent with other fields, **SQLSetDescField** will return SQLSTATE HY021 (Inconsistent descriptor information). For more information, see "Consistency Check" in [SQLSetDescRec](../../../odbc/reference/syntax/sqlsetdescrec-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a column|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Binding a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Getting a descriptor field|[SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)|  
|Getting multiple descriptor fields|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting multiple descriptor fields|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
  
## See Also  
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)
