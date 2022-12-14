---
description: "SQLSetDescRec Function"
title: "SQLSetDescRec Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLSetDescRec"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetDescRec"
helpviewer_keywords: 
  - "SQLSetDescRec function [ODBC]"
ms.assetid: bf55256c-7eb7-4e3f-97ef-b0fee09ba829
author: David-Engel
ms.author: v-davidengel
---
# SQLSetDescRec Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 The **SQLSetDescRec** function sets multiple descriptor fields that affect the data type and buffer bound to a column or parameter data.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLSetDescRec(  
      SQLHDESC      DescriptorHandle,  
      SQLSMALLINT   RecNumber,  
      SQLSMALLINT   Type,  
      SQLSMALLINT   SubType,  
      SQLLEN        Length,  
      SQLSMALLINT   Precision,  
      SQLSMALLINT   Scale,  
      SQLPOINTER    DataPtr,  
      SQLLEN *      StringLengthPtr,  
      SQLLEN *      IndicatorPtr);  
```  
  
## Arguments  
 *DescriptorHandle*  
 [Input] Descriptor handle. This must not be an IRD handle.  
  
 *RecNumber*  
 [Input] Indicates the descriptor record that contains the fields to be set. Descriptor records are numbered from 0, with record number 0 being the bookmark record. This argument must be equal to or greater than 0. If *RecNumber* is greater than the value of SQL_DESC_COUNT, SQL_DESC_COUNTis changed to the value of *RecNumber*.  
  
 *Type*  
 [Input] The value to which to set the SQL_DESC_TYPE field for the descriptor record.  
  
 *SubType*  
 [Input] For records whose type is SQL_DATETIME or SQL_INTERVAL, this is the value to which to set the SQL_DESC_DATETIME_INTERVAL_CODE field.  
  
 *Length*  
 [Input] The value to which to set the SQL_DESC_OCTET_LENGTH field for the descriptor record.  
  
 *Precision*  
 [Input] The value to which to set the SQL_DESC_PRECISION field for the descriptor record.  
  
 *Scale*  
 [Input] The value to which to set the SQL_DESC_SCALE field for the descriptor record.  
  
 *DataPtr*  
 [Deferred Input or Output] The value to which to set the SQL_DESC_DATA_PTR field for the descriptor record. *DataPtr* can be set to a null pointer.  
  
 The *DataPtr* argument can be set to a null pointer to set the SQL_DESC_DATA_PTR field to a null pointer. If the handle in the *DescriptorHandle* argument is associated with an ARD, this unbinds the column.  
  
 *StringLengthPtr*  
 [Deferred Input or Output] The value to which to set the SQL_DESC_OCTET_LENGTH_PTR field for the descriptor record. *StringLengthPtr* can be set to a null pointer to set the SQL_DESC_OCTET_LENGTH_PTR field to a null pointer.  
  
 *IndicatorPtr*  
 [Deferred Input or Output] The value to which to set the SQL_DESC_INDICATOR_PTR field for the descriptor record. *IndicatorPtr* can be set to a null pointer to set the SQL_DESC_INDICATOR_PTR field to a null pointer.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLSetDescRec** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DESC and a *Handle* of *DescriptorHandle*. The following table lists the SQLSTATE values commonly returned by **SQLSetDescRec** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07009|Invalid descriptor index|The *RecNumber* argument was set to 0, and the *DescriptorHandle* referred to an IPD handle.<br /><br /> The *RecNumber* argument was less than 0.<br /><br /> The *RecNumber* argument was greater than the maximum number of columns or parameters that the data source can support, and the *DescriptorHandle* argument was an APD, IPD, or ARD.<br /><br /> The *RecNumber* argument was equal to 0, and the *DescriptorHandle* argument referred to an implicitly allocated APD. (This error does not occur with an explicitly allocated application descriptor because it is not known whether an explicitly allocated application descriptor is an APD or ARD until execute time.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) The *DescriptorHandle* was associated with a *StatementHandle* for which an asynchronously executing function (not this one) was called and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* with which the *DescriptorHandle* was associated and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *DescriptorHandle*. This aynchronous function was still executing when the **SQLSetDescRec** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *DescriptorHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY016|Cannot modify an implementation row descriptor|The *DescriptorHandle* argument was associated with an IRD.|  
|HY021|Inconsistent descriptor information|The *Type* field, or any other field associated with the SQL_DESC_TYPE field in the descriptor, was not valid or consistent.<br /><br /> Descriptor information checked during a consistency check was not consistent. (See "Consistency Checks," later in this section.)|  
|HY090|Invalid string or buffer length|(DM) The driver was an ODBC *2.x* driver, the descriptor was an ARD, the *ColumnNumber* argument was set to 0, and the value specified for the argument *BufferLength* was not equal to 4.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *DescriptorHandle* does not support the function.|  
  
## Comments  
 An application can call **SQLSetDescRec** to set the following fields for a single column or parameter:  
  
-   SQL_DESC_TYPE  
  
-   SQL_DESC_DATETIME_INTERVAL_CODE (for records whose type is SQL_DATETIME or SQL_INTERVAL)  
  
-   SQL_DESC_OCTET_LENGTH  
  
-   SQL_DESC_PRECISION  
  
-   SQL_DESC_SCALE  
  
-   SQL_DESC_DATA_PTR  
  
-   SQL_DESC_OCTET_LENGTH_PTR  
  
-   SQL_DESC_INDICATOR_PTR  
  
> [!NOTE]  
>  If a call to **SQLSetDescRec** fails, the contents of the descriptor record identified by the *RecNumber* argument are undefined.  
  
 When binding a column or parameter, **SQLSetDescRec** allows you to change multiple fields affecting the binding without calling **SQLBindCol** or **SQLBindParameter** or making multiple calls to **SQLSetDescField**. **SQLSetDescRec** can set fields on a descriptor not currently associated with a statement. Note that **SQLBindParameter** sets more fields than **SQLSetDescRec**, can set fields on both an APD and an IPD in one call, and does not require a descriptor handle.  
  
> [!NOTE]  
>  The statement attribute SQL_ATTR_USE_BOOKMARKS should always be set before calling **SQLSetDescRec** with a *RecNumber* argument of 0 to set bookmark fields. While this is not mandatory, it is strongly recommended.  
  
## Consistency Checks  
 A consistency check is performed by the driver automatically whenever an application sets the SQL_DESC_DATA_PTR field of an APD, ARD, or IPD. If any of the fields is inconsistent with other fields, **SQLSetDescRec** will return SQLSTATE HY021 (Inconsistent descriptor information).  
  
 Whenever an application sets the SQL_DESC_DATA_PTR field of an APD, ARD, or IPD, the driver checks that the value of the SQL_DESC_TYPE field and the values applicable to that SQL_DESC_TYPE field are valid and consistent. This check is always performed when **SQLBindParameter** or **SQLBindCol** is called or when **SQLSetDescRec** is called for an APD, ARD, or IPD. This consistency check includes the following checks on descriptor fields:  
  
-   The SQL_DESC_TYPE field must be one of the valid ODBC C or SQL types or a driver-specific SQL type. The SQL_DESC_CONCISE_TYPE field must be one of the valid ODBC C or SQL types or a driver-specific C or SQL type, including the concise datetime and interval types.  
  
-   If the SQL_DESC_TYPE record field is SQL_DATETIME or SQL_INTERVAL, the SQL_DESC_DATETIME_INTERVAL_CODE field must be one of the valid datetime or interval codes. (See the description of the SQL_DESC_DATETIME_INTERVAL_CODE field in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).)  
  
-   If the SQL_DESC_TYPE field indicates a numeric type, the SQL_DESC_PRECISION and SQL_DESC_SCALE fields are verified to be valid.  
  
-   If the SQL_DESC_CONCISE_TYPE field is a time or timestamp data type, an interval type with a seconds component, or one of the interval data types with a time component, the SQL_DESC_PRECISION field is verified to be a valid seconds precision.  
  
-   If the SQL_DESC_CONCISE_TYPE is an interval data type, the SQL_DESC_DATETIME_INTERVAL_PRECISION field is verified to be a valid interval leading precision value.  
  
 The SQL_DESC_DATA_PTR field of an IPD is not normally set; however, an application can do so to force a consistency check of IPD fields. A consistency check cannot be performed on an IRD. The value that the SQL_DESC_DATA_PTR field of the IPD is set to is not actually stored and cannot be retrieved by a call to **SQLGetDescField** or **SQLGetDescRec**; the setting is made only to force the consistency check.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a column|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Binding a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Getting a single descriptor field|[SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)|  
|Getting multiple descriptor fields|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting single descriptor fields|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
