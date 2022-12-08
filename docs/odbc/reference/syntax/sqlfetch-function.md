---
description: "SQLFetch Function"
title: "SQLFetch Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLFetch"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLFetch"
helpviewer_keywords: 
  - "SQLFetch function [ODBC]"
ms.assetid: 6c6611d2-bc6a-4390-87c9-1c5dd9cfe07c
author: David-Engel
ms.author: v-davidengel
---
# SQLFetch Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLFetch** fetches the next rowset of data from the result set and returns data for all bound columns.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLFetch(  
     SQLHSTMT     StatementHandle);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLFetch** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling [SQLGetDiagRec Function](../../../odbc/reference/syntax/sqlgetdiagrec-function.md) with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLFetch** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise. If an error occurs on a single column, [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md) can be called with a *DiagIdentifier* of SQL_DIAG_COLUMN_NUMBER to determine the column the error occurred on; and **SQLGetDiagField** can be called with a *DiagIdentifier* of SQL_DIAG_ROW_NUMBER to determine the row that contains that column.  
  
 For all those SQLSTATEs that can return SQL_SUCCESS_WITH_INFO or SQL_ERROR (except 01xxx SQLSTATEs), SQL_SUCCESS_WITH_INFO is returned if an error occurs on one or more, but not all, rows of a multirow operation, and SQL_ERROR is returned if an error occurs on a single-row operation.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|String or binary data returned for a column resulted in the truncation of nonblank character or non-NULL binary data. If it was a string value, it was right-truncated.|  
|01S01|Error in row|An error occurred while fetching one or more rows.<br /><br /> (If this SQLSTATE is returned when an ODBC 3*.x* application is working with an ODBC 2*.x* driver, it can be ignored.)|  
|01S07|Fractional truncation|The data returned for a column was truncated. For numeric data types, the fractional part of the number was truncated. For time, timestamp, and interval data types that contain a time component, the fractional part of the time was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data value of a column in the result set could not be converted to the data type specified by *TargetType* in **SQLBindCol**.<br /><br /> Column 0 was bound with a data type of SQL_C_BOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE.<br /><br /> Column 0 was bound with a data type of SQL_C_VARBOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was not set to SQL_UB_VARIABLE.|  
|07009|Invalid descriptor index|The driver was an ODBC 2*.x* driver that does not support **SQLExtendedFetch**, and a column number specified in the binding for a column was 0.<br /><br /> Column 0 was bound, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22001|String data, right truncated|A variable-length bookmark returned for a column was truncated.|  
|22002|Indicator variable required but not supplied|NULL data was fetched into a column whose *StrLen_or_IndPtr* set by **SQLBindCol** (or SQL_DESC_INDICATOR_PTR set by **SQLSetDescField** or **SQLSetDescRec**) was a null pointer.|  
|22003|Numeric value out of range|Returning the numeric value as numeric or string for one or more bound columns would have caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> For more information, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md) in Appendix D: Data Types.|  
|22007|Invalid datetime format|A character column in the result set was bound to a date, time, or timestamp C structure, and a value in the column was, respectively, an invalid date, time, or timestamp.|  
|22012|Division by zero|A value from an arithmetic expression was returned, which resulted in division by zero.|  
|22015|Interval field overflow|Assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> When fetching data to an interval C type, there was no representation of the value of the SQL type in the interval C type.|  
|22018|Invalid character value for cast specification|A character column in the result set was bound to a character C buffer, and the column contained a character for which there was no representation in the character set of the buffer.<br /><br /> The C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.|  
|24000|Invalid cursor state|The *StatementHandle* was in an executed state but no result set was associated with the *StatementHandle*.|  
|40001|Serialization failure|The transaction in which the fetch was executed was terminated to prevent deadlock.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The **SQLFetch** function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the **SQLFetch** function was called again on the *StatementHandle*.<br /><br /> Or, the  **SQLFetch** function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLFetch** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute** or a catalog function.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) **SQLFetch** was called for the *StatementHandle* after **SQLExtendedFetch** was called and before **SQLFreeStmt** with the SQL_CLOSE option was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|The SQL_ATTR_USE_BOOKMARK statement attribute was set to SQL_UB_VARIABLE, and column 0 was bound to a buffer whose length was not equal to the maximum length for the bookmark for this result set. (This length is available in the SQL_DESC_OCTET_LENGTH field of the IRD and can be obtained by calling **SQLDescribeCol**, **SQLColAttribute**, or **SQLGetDescField**.)|  
|HY107|Row value out of range|The value specified with the SQL_ATTR_CURSOR_TYPE statement attribute was SQL_CURSOR_KEYSET_DRIVEN, but the value specified with the SQL_ATTR_KEYSET_SIZE statement attribute was greater than 0 and less than the value specified with the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the conversion specified by the combination of the *TargetType* in **SQLBindCol** and the SQL data type of the corresponding column.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the requested result set. The timeout period is set through SQLSetStmtAttr, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLFetch** returns the next rowset in the result set. It can be called only while a result set exists: that is, after a call that creates a result set and before the cursor over that result set is closed. If any columns are bound, it returns the data in those columns. If the application has specified a pointer to a row status array or a buffer in which to return the number of rows fetched, **SQLFetch** also returns this information. Calls to **SQLFetch** can be mixed with calls to **SQLFetchScroll** but cannot be mixed with calls to **SQLExtendedFetch**. For more information, see [Fetching a Row of Data](../../../odbc/reference/develop-app/fetching-a-row-of-data.md).  
  
 If an ODBC 3*.x* application works with an ODBC 2*.x* driver, the Driver Manager maps **SQLFetch** calls to **SQLExtendedFetch** for an ODBC 2*.x* driver that supports **SQLExtendedFetch**. If the ODBC 2*.x* driver does not support **SQLExtendedFetch**, the Driver Manager maps **SQLFetch** calls to **SQLFetch** in the ODBC 2*.x* driver, which can fetch only a single row.  
  
 For more information, see [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
## Positioning the Cursor  
 When the result set is created, the cursor is positioned before the start of the result set. **SQLFetch** fetches the next rowset. It is equivalent to calling **SQLFetchScroll** with *FetchOrientation* set to SQL_FETCH_NEXT. For more information about cursors, see [Cursors](../../../odbc/reference/develop-app/cursors.md) and [Block Cursors](../../../odbc/reference/develop-app/block-cursors.md).  
  
 The SQL_ATTR_ROW_ARRAY_SIZE statement attribute specifies the number of rows in the rowset. If the rowset being fetched by **SQLFetch** overlaps the end of the result set, **SQLFetch** returns a partial rowset. That is, if S + R - 1 is greater than L, where S is the starting row of the rowset being fetched, R is the rowset size, and L is the last row in the result set, then only the first L - S + 1 rows of the rowset are valid. The remaining rows are empty and have a status of SQL_ROW_NOROW.  
  
 After **SQLFetch** returns, the current row is the first row of the rowset.  
  
 The rules listed in the following table describe cursor positioning after a call to **SQLFetch**, based on the conditions listed in the second table in this section.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|Before start|1|  
|*CurrRowsetStart* \<= *LastResultRow - RowsetSize*[1]|*CurrRowsetStart* + *RowsetSize*[2]|  
|*CurrRowsetStart* > *LastResultRow - RowsetSize*[1]|After end|  
|After end|After end|  
  
 [1]   If the rowset size is changed between fetches, this is the rowset size that was used with the previous fetch.  
  
 [2]   If the rowset size is changed between fetches, this is the rowset size that was used with the new fetch.  
  
|Notation|Meaning|  
|--------------|-------------|  
|Before start|The block cursor is positioned before the start of the result set. If the first row of the new rowset is before the start of the result set, **SQLFetch** returns SQL_NO_DATA.|  
|After end|The block cursor is positioned after the end of the result set. If the first row of the new rowset is after the end of the result set, **SQLFetch** returns SQL_NO_DATA.|  
|*CurrRowsetStart*|The number of the first row in the current rowset.|  
|*LastResultRow*|The number of the last row in the result set.|  
|*RowsetSize*|The rowset size.|  
  
 For example, suppose a result set has 100 rows and the rowset size is 5. The following table shows the rowset and return code returned by **SQLFetch** for different starting positions.  
  
|Current rowset|Return code|New rowset|# of rows fetched|  
|--------------------|-----------------|----------------|------------------------|  
|Before start|SQL_SUCCESS|1 to 5|5|  
|1 to 5|SQL_SUCCESS|6 to 10|5|  
|52 to 56|SQL_SUCCESS|57 to 61|5|  
|91 to 95|SQL_SUCCESS|96 to 100|5|  
|93 to 97|SQL_SUCCESS|98 to 100. Rows 4 and 5 of the row status array are set to SQL_ROW_NOROW.|3|  
|96 to 100|SQL_NO_DATA|None.|0|  
|99 to 100|SQL_NO_DATA|None.|0|  
|After end|SQL_NO_DATA|None.|0|  
  
## Returning Data in Bound Columns  
 As **SQLFetch** returns each row, it puts the data for each bound column in the buffer bound to that column. If no columns are bound, **SQLFetch** returns no data but does move the block cursor forward. The data can still be retrieved by using **SQLGetData**. If the cursor is a multirow cursor (that is, the SQL_ATTR_ROW_ARRAY_SIZE is greater than 1), **SQLGetData** can be called only if SQL_GD_BLOCK is returned when **SQLGetInfo** is called with an *InfoType* of SQL_GETDATA_EXTENSIONS. (For more information, see [SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md).)  
  
 For each bound column in a row, **SQLFetch** does the following:  
  
1.  Sets the length/indicator buffer to SQL_NULL_DATA and proceeds to the next column if the data is NULL. If the data is NULL and no length/indicator buffer was bound, **SQLFetch** returns SQLSTATE 22002 (Indicator variable required but not supplied) for the row and proceeds to the next row. For information about how to determine the address of the length/indicator buffer, see "Buffer Addresses" in [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
     If the data for the column is not NULL, **SQLFetch** proceeds to step 2.  
  
2.  If the SQL_ATTR_MAX_LENGTH statement attribute is set to a nonzero value and the column contains character or binary data, the data is truncated to SQL_ATTR_MAX_LENGTH bytes.  
  
    > [!NOTE]  
    >  The SQL_ATTR_MAX_LENGTH statement attribute is intended to reduce network traffic. It is generally implemented by the data source, which truncates the data before returning it over the network. Drivers and data sources are not required to support it. Therefore, to guarantee that data is truncated to a particular size, an application should allocate a buffer of that size and specify the size in the *cbValueMax* argument in **SQLBindCol**.  
  
3.  Converts the data to the type specified by *TargetType* in **SQLBindCol**.  
  
4.  If the data was converted to a variable-length data type, such as character or binary, **SQLFetch** checks whether the length of the data exceeds the length of the data buffer. If the length of character data (including the null-termination character) exceeds the length of the data buffer, **SQLFetch** truncates the data to the length of the data buffer less the length of a null-termination character. It then null-terminates the data. If the length of binary data exceeds the length of the data buffer, **SQLFetch** truncates it to the length of the data buffer. The length of the data buffer is specified with *BufferLength* in **SQLBindCol**.  
  
     **SQLFetch** never truncates data converted to fixed-length data types; it always assumes that the length of the data buffer is the size of the data type.  
  
5.  Puts the converted (and possibly truncated) data in the data buffer. For information about how to determine the address of the data buffer, see "Buffer Addresses" in [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
6.  Puts the length of the data in the length/indicator buffer. If the indicator pointer and the length pointer were both set to the same buffer (as a call to **SQLBindCol** does), the length is written in the buffer for valid data and SQL_NULL_DATA is written in the buffer for NULL data. If no length/indicator buffer was bound, **SQLFetch** does not return the length.  
  
    -   For character or binary data, this is the length of the data after conversion and before truncation because of the data buffer being too small. If the driver cannot determine the length of the data after conversion, as is sometimes the case with long data, it sets the length to SQL_NO_TOTAL. If data was truncated because of the SQL_ATTR_MAX_LENGTH statement attribute, the value of this attribute is put in the length/indicator buffer instead of the actual length . This is because this attribute is designed to truncate data on the server before conversion, so that the driver has no way of figuring out what the actual length is.  
  
    -   For all other data types, this is the length of the data after conversion; that is, it is the size of the type to which the data was converted.  
  
     For information about how to determine the address of the length/indicator buffer, see "Buffer Addresses" in [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
7.  If the data is truncated during conversion without a loss of significant digits (for example, the real number 1.234 is truncated to the integer 1 when converted), **SQLFetch** returns SQLSTATE 01S07 (Fractional truncation) and SQL_SUCCESS_WITH_INFO. If the data is truncated because the length of the data buffer is too small (for example, the string "abcdef" is put in a 4-byte buffer), **SQLFetch** returns SQLSTATE 01004 (Data truncated) and SQL_SUCCESS_WITH_INFO. If data is truncated because of the SQL_ATTR_MAX_LENGTH statement attribute, **SQLFetch** returns SQL_SUCCESS and does not return SQLSTATE 01S07 (Fractional truncation) or SQLSTATE 01004 (Data truncated). If data is truncated during conversion with a loss of significant digits (for example, if an SQL_INTEGER value greater than 100,000 were converted to an SQL_C_TINYINT), **SQLFetch** returns SQLSTATE 22003 (Numeric value out of range) and SQL_ERROR (if the rowset size is 1) or SQL_SUCCESS_WITH_INFO (if the rowset size is greater than 1).  
  
 The contents of the bound data buffer and the length/indicator buffer are undefined if **SQLFetch** or **SQLFetchScroll** does not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO.  
  
## Row Status Array  
 The row status array is used to return the status of each row in the rowset. The address of this array is specified with the SQL_ATTR_ROW_STATUS_PTR statement attribute. The array is allocated by the application and must have as many elements as are specified by the SQL_ATTR_ROW_ARRAY_SIZE statement attribute. Its values are set by **SQLFetch**, **SQLFetchScroll**, and **SQLBulkOperations** or **SQLSetPos** (except when they have been called after the cursor has been positioned by **SQLExtendedFetch**). If the value of the SQL_ATTR_ROW_STATUS_PTR statement attribute is a null pointer, these functions do not return the row status.  
  
 The contents of the row status array buffer are undefined if **SQLFetch** or **SQLFetchScroll** does not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO.  
  
 The following values are returned in the row status array.  
  
|Row status array value|Description|  
|----------------------------|-----------------|  
|SQL_ROW_SUCCESS|The row was successfully fetched and has not changed since it was last fetched from this result set.|  
|SQL_ROW_SUCCESS_WITH_INFO|The row was successfully fetched and has not changed since it was last fetched from this result set. However, a warning was returned about the row.|  
|SQL_ROW_ERROR|An error occurred while fetching the row.|  
|SQL_ROW_UPDATED[1],[2], and [3]|The row was successfully fetched and has changed since it was last fetched from this result set. If the row is fetched again from this result set or is refreshed by **SQLSetPos**, the status is changed to the row's new status.|  
|SQL_ROW_DELETED[3]|The row has been deleted since it was last fetched from this result set.|  
|SQL_ROW_ADDED[4]|The row was inserted by **SQLBulkOperations**. If the row is fetched again from this result set or is refreshed by **SQLSetPos**, its status is SQL_ROW_SUCCESS.|  
|SQL_ROW_NOROW|The rowset overlapped the end of the result set, and no row was returned that corresponded to this element of the row status array.|  
  
 [1]   For keyset, mixed, and dynamic cursors, if a key value is updated, the row of data is considered to have been deleted and a new row added.  
  
 [2]   Some drivers cannot detect updates to data and therefore cannot return this value. To determine whether a driver can detect updates to refetched rows, an application calls **SQLGetInfo** with the SQL_ROW_UPDATES option.  
  
 [3]   **SQLFetch** can return this value only when it is intermixed with calls to **SQLFetchScroll**. This is because **SQLFetch** moves forward through the result set and when it is used exclusively, does not refetch any rows. Because no rows are refetched, **SQLFetch** does not detect changes that were made to previously fetched rows. However, if **SQLFetchScroll** positions the cursor before any previously fetched rows and **SQLFetch** is used to fetch those rows, **SQLFetch** can detect any changes to those rows.  
  
 [4]   Returned by SQLBulkOperations only. Not set by **SQLFetch** or **SQLFetchScroll**.  
  
### Rows Fetched Buffer  
 The rows fetched buffer is used to return the number of rows fetched, including those rows for which no data was returned because an error occurred while they were being fetched. In other words, it is the number of rows for which the value in the row status array is not SQL_ROW_NOROW. The address of this buffer is specified with the SQL_ATTR_ROWS_FETCHED_PTR statement attribute. The buffer is allocated by the application. It is set by **SQLFetch** and **SQLFetchScroll**. If the value of the SQL_ATTR_ROWS_FETCHED_PTR statement attribute is a null pointer, these functions do not return the number of rows fetched. To determine the number of the current row in the result set, an application can call **SQLGetStmtAttr** with the SQL_ATTR_ROW_NUMBER attribute.  
  
 The contents of the rows fetched buffer are undefined if **SQLFetch** or **SQLFetchScroll** does not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, except when SQL_NO_DATA is returned, in which case the value in the rows fetched buffer is set to 0.  
  
### Error Handling  
 Errors and warnings can apply to individual rows or to the whole function. For more information about diagnostic records, see [Diagnostics](../../../odbc/reference/develop-app/diagnostics.md) and [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md).  
  
#### Errors and Warnings on the Entire Function  
 If an error applies to the entire function, such as SQLSTATE HYT00 (Timeout expired) or SQLSTATE 24000 (Invalid cursor state), **SQLFetch** returns SQL_ERROR and the applicable SQLSTATE. The contents of the rowset buffers are undefined and the cursor position is unchanged.  
  
 If a warning applies to the entire function, **SQLFetch** returns SQL_SUCCESS_WITH_INFO and the applicable SQLSTATE. The status records for warnings that apply to the entire function are returned before the status records that apply to individual rows.  
  
#### Errors and Warnings in Individual Rows  
 If an error (such as SQLSTATE 22012 (Division by zero)) or a warning (such as SQLSTATE 01004 (Data truncated)) applies to a single row, **SQLFetch**does the following:  
  
-   Sets the corresponding element of the row status array to SQL_ROW_ERROR for errors or SQL_ROW_SUCCESS_WITH_INFO for warnings.  
  
-   Adds zero or more status records that contain SQLSTATEs for the error or warning.  
  
-   Sets the row and column number fields in the status records. If **SQLFetch** cannot determine a row or column number, it sets that number to SQL_ROW_NUMBER_UNKNOWN or SQL_COLUMN_NUMBER_UNKNOWN, respectively. If the status record does not apply to a particular column, **SQLFetch** sets the column number to SQL_NO_COLUMN_NUMBER.  
  
 **SQLFetch** continues fetching rows until it has fetched all the rows in the rowset. It returns SQL_SUCCESS_WITH_INFO unless an error occurs in every row of the rowset (not including rows with status SQL_ROW_NOROW), in which case it returns SQL_ERROR. In particular, if the rowset size is 1 and an error occurs in that row, **SQLFetch** returns SQL_ERROR.  
  
 **SQLFetch** returns the status records in row number order. That is, it returns all status records for unknown rows (if any); next it returns all status records for the first row (if any), and then it returns all status records for the second row (if any), and so on. The status records for each row are ordered according to the normal rules for ordering status records; for more information, see "Sequence of Status Records" in [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md).  
  
### Descriptors and SQLFetch  
 The following sections describe how **SQLFetch** interacts with descriptors.  
  
#### Argument Mappings  
 The driver does not set any descriptor fields based on the arguments of **SQLFetch**.  
  
#### Other Descriptor Fields  
 The following descriptor fields are used by **SQLFetch**.  
  
|Descriptor field|Desc.|Field in|Set through|  
|----------------------|-----------|--------------|-----------------|  
|SQL_DESC_ARRAY_SIZE|ARD|header|SQL_ATTR_ROW_ARRAY_SIZE statement attribute|  
|SQL_DESC_ARRAY_STATUS_PTR|IRD|header|SQL_ATTR_ROW_STATUS_PTR statement attribute|  
|SQL_DESC_BIND_OFFSET_PTR|ARD|header|SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute|  
|SQL_DESC_BIND_TYPE|ARD|header|SQL_ATTR_ROW_BIND_TYPE statement attribute|  
|SQL_DESC_COUNT|ARD|header|*ColumnNumber* argument of **SQLBindCol**|  
|SQL_DESC_DATA_PTR|ARD|records|*TargetValuePtr* argument of **SQLBindCol**|  
|SQL_DESC_INDICATOR_PTR|ARD|records|*StrLen_or_IndPtr* argument in **SQLBindCol**|  
|SQL_DESC_OCTET_LENGTH|ARD|records|*BufferLength* argument in **SQLBindCol**|  
|SQL_DESC_OCTET_LENGTH_PTR|ARD|records|*StrLen_or_IndPtr* argument in **SQLBindCol**|  
|SQL_DESC_ROWS_PROCESSED_PTR|IRD|header|SQL_ATTR_ROWS_FETCHED_PTR statement attribute|  
|SQL_DESC_TYPE|ARD|records|*TargetType* argument in **SQLBindCol**|  
  
 All descriptor fields can also be set through **SQLSetDescField**.  
  
#### Separate Length and Indicator Buffers  
 Applications can bind a single buffer or two separate buffers that can be used to hold length and indicator values. When an application calls **SQLBindCol**, the driver sets the SQL_DESC_OCTET_LENGTH_PTR and SQL_DESC_INDICATOR_PTR fields of the ARD to the same address, which is passed in the *StrLen_or_IndPtr* argument. When an application calls **SQLSetDescField** or **SQLSetDescRec**, it can set these two fields to different addresses.  
  
 **SQLFetch** determines whether the application has specified separate length and indicator buffers. In this case, when the data is not NULL, **SQLFetch** sets the indicator buffer to 0 and returns the length in the length buffer. When the data is NULL, **SQLFetch** sets the indicator buffer to SQL_NULL_DATA and does not modify the length buffer.  
  
### Code Example  
 See [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md), [SQLColumns](../../../odbc/reference/syntax/sqlcolumns-function.md), [SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md), and [SQLProcedures](../../../odbc/reference/syntax/sqlprocedures-function.md).  
  
### Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Closing the cursor on the statement|[SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
|Fetching part or all of a column of data|[SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md)|  
|Returning the number of result set columns|[SQLNumResultCols Function](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|  
|Preparing a statement for execution|[SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
