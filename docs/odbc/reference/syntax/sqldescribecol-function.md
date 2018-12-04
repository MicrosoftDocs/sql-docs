---
title: "SQLDescribeCol Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLDescribeCol"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDescribeCol"
helpviewer_keywords: 
  - "SQLDescribeCol function [ODBC]"
ms.assetid: eddef353-83f3-4a3c-8f24-f9ed888890a4
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLDescribeCol Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLDescribeCol** returns the result descriptor - column name,type, column size, decimal digits, and nullability - for one column in the result set. This information also is available in the fields of the IRD.  
  
## Syntax  
  
```  
  
SQLRETURN SQLDescribeCol(  
      SQLHSTMT       StatementHandle,  
      SQLUSMALLINT   ColumnNumber,  
      SQLCHAR *      ColumnName,  
      SQLSMALLINT    BufferLength,  
      SQLSMALLINT *  NameLengthPtr,  
      SQLSMALLINT *  DataTypePtr,  
      SQLULEN *      ColumnSizePtr,  
      SQLSMALLINT *  DecimalDigitsPtr,  
      SQLSMALLINT *  NullablePtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *ColumnNumber*  
 [Input] Column number of result data, ordered sequentially in increasing column order, starting at 1. The *ColumnNumber* argument can also be set to 0 to describe the bookmark column.  
  
 *ColumnName*  
 [Output] Pointer to a null-terminated buffer in which to return the column name. This value is read from the SQL_DESC_NAME field of the IRD. If the column is unnamed or the column name cannot be determined, the driver returns an empty string.  
  
 If *ColumnName* is NULL, *NameLengthPtr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *ColumnName*.  
  
 *BufferLength*  
 [Input] Length of the **ColumnName* buffer, in characters.  
  
 *NameLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null termination) available to return in \**ColumnName*. If the number of characters available to return is greater than or equal to *BufferLength*, the column name in \**ColumnName* is truncated to *BufferLength* minus the length of a null-termination character.  
  
 *DataTypePtr*  
 [Output] Pointer to a buffer in which to return the SQL data type of the column. This value is read from the SQL_DESC_CONCISE_TYPE field of the IRD. This will be one of the values in [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md), or a driver-specific SQL data type. If the data type cannot be determined, the driver returns SQL_UNKNOWN_TYPE.  
  
 In ODBC 3.*x*, SQL_TYPE_DATE, SQL_TYPE_TIME, or SQL_TYPE_TIMESTAMP is returned in *\*DataTypePtr* for date, time, or timestamp data, respectively; in ODBC 2.*x*, SQL_DATE, SQL_TIME, or SQL_TIMESTAMP is returned. The Driver Manager performs the required mappings when an ODBC 2.*x* application is working with an ODBC 3.*x* driver or when an ODBC 3.*x* application is working with an ODBC 2.*x* driver.  
  
 When *ColumnNumber* is equal to 0 (for a bookmark column), SQL_BINARY is returned in *\*DataTypePtr* for variable-length bookmarks. (SQL_INTEGER is returned if bookmarks are used by an ODBC 3.*x* application working with an ODBC 2.*x* driver or by an ODBC 2.*x* application working with an ODBC 3.*x* driver.)  
  
 For more information on these data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) in Appendix D: Data Types. For information about driver-specific SQL data types, see the driver's documentation.  
  
 *ColumnSizePtr*  
 [Output] Pointer to a buffer in which to return the size (in characters) of the column on the data source. If the column size cannot be determined, the driver returns 0. For more information on column size, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.  
  
 *DecimalDigitsPtr*  
 [Output] Pointer to a buffer in which to return the number of decimal digits of the column on the data source. If the number of decimal digits cannot be determined or is not applicable, the driver returns 0. For more information on decimal digits, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.  
  
 *NullablePtr*  
 [Output] Pointer to a buffer in which to return a value that indicates whether the column allows NULL values. This value is read from the SQL_DESC_NULLABLE field of the IRD. The value is one of the following:  
  
 SQL_NO_NULLS: The column does not allow NULL values.  
  
 SQL_NULLABLE: The column allows NULL values.  
  
 SQL_NULLABLE_UNKNOWN: The driver cannot determine if the column allows NULL values.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLDescribeCol** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLDescribeCol** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**ColumnName* was not large enough to return the entire column name, so the column name was truncated. The length of the untruncated column name is returned in **NameLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07005|Prepared statement not a *cursor-specification*|The statement associated with the *StatementHandle* did not return a result set. There were no columns to describe.|  
|07009|Invalid descriptor index|(DM) The value specified for the argument *ColumnNumber* was equal to 0, and the SQL_ATTR_USE_BOOKMARKS statement option was SQL_UB_OFF.<br /><br /> The value specified for the argument *ColumnNumber* was greater than the number of columns in the result set.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation failure|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when **SQLDescribeCol** was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) The function was called prior to calling **SQLPrepare**, **SQLExecute**, or a catalog function on the statement handle.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *BufferLength* was less than 0.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
 **SQLDescribeCol** can return any SQLSTATE that can be returned by **SQLPrepare** or **SQLExecute** when called after **SQLPrepare** and before **SQLExecute**, depending on when the data source evaluates the SQL statement associated with the statement handle.  
  
 For performance reasons, an application should not call **SQLDescribeCol** before executing a statement.  
  
## Comments  
 An application typically calls **SQLDescribeCol** after a call to **SQLPrepare** and before or after the associated call to **SQLExecute**. An application can also call **SQLDescribeCol** after a call to **SQLExecDirect**. For more information, see [Result Set Metadata](../../../odbc/reference/develop-app/result-set-metadata.md).  
  
 **SQLDescribeCol** retrieves the column name, type, and length generated by a **SELECT** statement. If the column is an expression, **ColumnName* is either an empty string or a driver-defined name.  
  
> [!NOTE]  
>  ODBC supports SQL_NULLABLE_UNKNOWN as an extension, even though the Open Group and SQL Access Group Call Level Interface specification does not specify the option for **SQLDescribeCol**.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLColAttribute](../../../odbc/reference/syntax/sqlcolattribute-function.md)|  
|Fetching multiple rows of data|[SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Returning the number of result set columns|[SQLNumResultCols](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|  
|Preparing a statement for execution|[SQLPrepare](../../../odbc/reference/syntax/sqlprepare-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
