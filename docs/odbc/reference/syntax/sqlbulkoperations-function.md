---
description: "SQLBulkOperations Function"
title: "SQLBulkOperations Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLBulkOperations"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLBulkOperations"
helpviewer_keywords: 
  - "SQLBulkOperations function [ODBC]"
ms.assetid: 7029d0da-b0f2-44e6-9114-50bd96f47196
author: David-Engel
ms.author: v-davidengel
---
# SQLBulkOperations Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLBulkOperations** performs bulk insertions and bulk bookmark operations, including update, delete, and fetch by bookmark.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLBulkOperations(  
     SQLHSTMT       StatementHandle,  
     SQLUSMALLINT   Operation);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *Operation*  
 [Input] Operation to perform:  
  
 SQL_ADD SQL_UPDATE_BY_BOOKMARK SQL_DELETE_BY_BOOKMARK SQL_FETCH_BY_BOOKMARK  
  
 For more information, see "Comments."  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NEED_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLBulkOperations** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLBulkOperations** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
 For all those SQLSTATEs that can return SQL_SUCCESS_WITH_INFO or SQL_ERROR (except 01xxx SQLSTATEs), SQL_SUCCESS_WITH_INFO is returned if an error occurs on one or more, but not all, rows of a multirow operation, and SQL_ERROR is returned if an error occurs on a single-row operation.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data right truncation|The *Operation* argument was SQL_FETCH_BY_BOOKMARK, and string or binary data returned for a column or columns with a data type of SQL_C_CHAR or SQL_C_BINARY resulted in the truncation of nonblank character or non-NULL binary data.|  
|01S01|Error in row|The *Operation* argument was SQL_ADD, and an error occurred in one or more rows while performing the operation but at least one row was successfully added. (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> (This error is raised only when an application is working with an ODBC 2.*x* driver.)|  
|01S07|Fractional truncation|The *Operation* argument was SQL_FETCH_BY_BOOKMARK, the data type of the application buffer was not SQL_C_CHAR or SQL_C_BINARY, and the data returned to application buffers for one or more columns was truncated. (For numeric C data types, the fractional part of the number was truncated. For time, timestamp, and interval C data types that contain a time component, the fractional portion of the time was truncated.)<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The *Operation* argument was SQL_FETCH_BY_BOOKMARK, and the data value of a column in the result set could not be converted to the data type specified by the *TargetType* argument in the call to **SQLBindCol**.<br /><br /> The *Operation* argument was SQL_UPDATE_BY_BOOKMARK or SQL_ADD, and the data value in the application buffers could not be converted to the data type of a column in the result set.|  
|07009|Invalid descriptor index|The argument *Operation* was SQL_ADD, and a column was bound with a column number greater than the number of columns in the result set.|  
|21S02|Degree of derived table does not match column list|The argument *Operation* was SQL_UPDATE_BY_BOOKMARK; and no columns were updatable because all columns were either unbound or read-only, or the value in the bound length/indicator buffer was SQL_COLUMN_IGNORE.|  
|22001|String data right truncation|The assignment of a character or binary value to a column in the result set resulted in the truncation of nonblank (for characters) or non-null (for binary) characters or bytes.|  
|22003|Numeric value out of range|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK, and the assignment of a numeric value to a column in the result set caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> The argument *Operation* was SQL_FETCH_BY_BOOKMARK, and returning the numeric value for one or more bound columns would have caused a loss of significant digits.|  
|22007|Invalid datetime format|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK, and the assignment of a date or timestamp value to a column in the result set caused the year, month, or day field to be out of range.<br /><br /> The argument *Operation* was SQL_FETCH_BY_BOOKMARK, and returning the date or timestamp value for one or more bound columns would have caused the year, month, or day field to be out of range.|  
|22008|Date/time field overflow|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK, and the performance of datetime arithmetic on data being sent to a column in the result set resulted in a datetime field (the year, month, day, hour, minute, or second field) of the result falling outside the permissible range of values for the field or being invalid based on the Gregorian calendar's natural rules for datetimes.<br /><br /> The *Operation* argument was SQL_FETCH_BY_BOOKMARK, and the performance of datetime arithmetic on data being retrieved from the result set resulted in a datetime field (the year, month, day, hour, minute, or second field) of the result falling outside the permissible range of values for the field or being invalid based on the Gregorian calendar's natural rules for datetimes.|  
|22015|Interval field overflow|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK, and the assignment of an exact numeric or interval C type to an interval SQL data type caused a loss of significant digits.<br /><br /> The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK; when assigning to an interval SQL type, there was no representation of the value of the C type in the interval SQL type.<br /><br /> The *Operation* argument was SQL_FETCH_BY_BOOKMARK, and assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> The *Operation* argument was SQL_FETCH_BY_BOOKMARK; when assigning to an interval C type, there was no representation of the value of the SQL type in the interval C type.|  
|22018|Invalid character value for cast specification|The *Operation* argument was SQL_FETCH_BY_BOOKMARK; the C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.<br /><br /> The argument *Operation* was SQL_ADD or SQL_UPDATE_BY_BOOKMARK; the SQL type was an exact or approximate numeric, a datetime, or an interval data type; the C type was SQL_C_CHAR; and the value in the column was not a valid literal of the bound SQL type.|  
|23000|Integrity constraint violation|The *Operation* argument was SQL_ADD, SQL_DELETE_BY_BOOKMARK, or SQL_UPDATE_BY_BOOKMARK, and an integrity constraint was violated.<br /><br /> The *Operation* argument was SQL_ADD, and a column that was not bound is defined as NOT NULL and has no default.<br /><br /> The *Operation* argument was SQL_ADD, the length specified in the bound *StrLen_or_IndPtr* buffer was SQL_COLUMN_IGNORE, and the column did not have a default value.|  
|24000|Invalid cursor state|The *StatementHandle* was in an executed state, but no result set was associated with the *StatementHandle*.|  
|40001|Serialization failure|The transaction was rolled back because of a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|42000|Syntax error or access violation|The driver was unable to lock the row as needed to perform the operation requested in the *Operation* argument.|  
|44000|WITH CHECK OPTION violation|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK, and the insert or update was performed on a viewed table (or a table derived from the viewed table) that was created by specifying **WITH CHECK OPTION**, in such a way that one or more rows affected by the insert or update will no longer be present in the viewed table.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLBulkOperations** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute**, or a catalog function.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) The driver was an ODBC 2.*x* driver, and **SQLBulkOperations** was called for a *StatementHandle* before **SQLFetchScroll** or **SQLFetch** was called.<br /><br /> (DM) **SQLBulkOperations** was called after **SQLExtendedFetch** was called on the *StatementHandle*.|  
|HY011|Attribute cannot be set now|(DM) The driver was an ODBC 2.*x* driver, and the SQL_ATTR_ROW_STATUS_PTR statement attribute was set between calls to **SQLFetch** or **SQLFetchScroll** and **SQLBulkOperations**.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|The *Operation* argument was SQL_ADD or SQL_UPDATE_BY_BOOKMARK; a data value was not a null pointer; the C data type was SQL_C_BINARY or SQL_C_CHAR; and the column length value was less than 0, but not equal to SQL_DATA_AT_EXEC, SQL_COLUMN_IGNORE, SQL_NTS, or SQL_NULL_DATA, or less than or equal to SQL_LEN_DATA_AT_EXEC_OFFSET.<br /><br /> The value in a length/indicator buffer was SQL_DATA_AT_EXEC; the SQL type was either SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type; and the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y".<br /><br /> The *Operation* argument was SQL_ADD, the SQL_ATTR_USE_BOOKMARK statement attribute was set to SQL_UB_VARIABLE, and column 0 was bound to a buffer whose length was not equal to the maximum length for the bookmark for this result set. (This length is available in the SQL_DESC_OCTET_LENGTH field of the IRD and can be obtained by calling **SQLDescribeCol**, **SQLColAttribute**, or **SQLGetDescField**.)|  
|HY092|Invalid attribute identifier|(DM) The value specified for the *Operation* argument was invalid.<br /><br /> The *Operation* argument was SQL_ADD, SQL_UPDATE_BY_BOOKMARK, or SQL_DELETE_BY_BOOKMARK, and the SQL_ATTR_CONCURRENCY statement attribute was set to SQL_CONCUR_READ_ONLY.<br /><br /> The *Operation* argument was SQL_DELETE_BY_BOOKMARK, SQL_FETCH_BY_BOOKMARK, or SQL_UPDATE_BY_BOOKMARK, and the bookmark column was not bound or the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the operation requested in the *Operation* argument.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
  
> [!CAUTION]  
>  For information about what statement states **SQLBulkOperations** can be called in and what it must do for compatibility with ODBC 2.*x* applications, see the [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) section in Appendix G: Driver Guidelines for Backward Compatibility.  
  
 An application uses **SQLBulkOperations** to perform the following operations on the base table or view that corresponds to the current query:  
  
-   Add new rows.  
  
-   Update a set of rows where each row is identified by a bookmark.  
  
-   Delete a set of rows where each row is identified by a bookmark.  
  
-   Fetch a set of rows where each row is identified by a bookmark.  
  
 After a call to **SQLBulkOperations**, the block cursor position is undefined. The application has to call **SQLFetchScroll** to set the cursor position. An application should call **SQLFetchScroll** only with a *FetchOrientation* argument of SQL_FETCH_FIRST, SQL_FETCH_LAST, SQL_FETCH_ABSOLUTE, or SQL_FETCH_BOOKMARK. The cursor position is undefined if the application calls **SQLFetch** or **SQLFetchScroll** with a *FetchOrientation* argument of SQL_FETCH_PRIOR, SQL_FETCH_NEXT, or SQL_FETCH_RELATIVE.  
  
 A column can be ignored in bulk operations performed by a call to **SQLBulkOperations** by setting the column length/indicator buffer specified in the call to **SQLBindCol**, to SQL_COLUMN_IGNORE.  
  
 It is not necessary for the application to set the SQL_ATTR_ROW_OPERATION_PTR statement attribute when it calls **SQLBulkOperations** because rows cannot be ignored when performing bulk operations with this function.  
  
 The buffer pointed to by the SQL_ATTR_ROWS_FETCHED_PTR statement attribute contains the number of rows affected by a call to **SQLBulkOperations**.  
  
 When the *Operation* argument is SQL_ADD or SQL_UPDATE_BY_BOOKMARK and the select-list of the query specification associated with the cursor contains more than one reference to the same column, it is driver-defined whether an error is generated or the driver ignores the duplicated references and performs the requested operations.  
  
 For more information about how to use **SQLBulkOperations**, see [Updating Data with SQLBulkOperations](../../../odbc/reference/develop-app/updating-data-with-sqlbulkoperations.md).  
  
## Performing Bulk Inserts  
 To insert data with **SQLBulkOperations**, an application performs the following sequence of steps:  
  
1.  Executes a query that returns a result set.  
  
2.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows that it wants to insert.  
  
3.  Calls **SQLBindCol** to bind the data that it wants to insert. The data is bound to an array with a size equal to the value of SQL_ATTR_ROW_ARRAY_SIZE.  
  
    > [!NOTE]  
    >  The size of the array pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute should either be equal to SQL_ATTR_ROW_ARRAY_SIZE or SQL_ATTR_ROW_STATUS_PTR should be a null pointer.  
  
4.  Calls **SQLBulkOperations**(*StatementHandle,* SQL_ADD) to perform the insertion.  
  
5.  If the application has set the SQL_ATTR_ROW_STATUS_PTR statement attribute, it can inspect this array to see the result of the operation.  
  
 If an application binds column 0 before it calls **SQLBulkOperations** with an *Operation* argument of SQL_ADD, the driver will update the bound column 0 buffers with the bookmark values for the newly inserted row. For this to occur, the application must have set the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE before executing the statement. (This does not work with an ODBC 2.*x* driver.)  
  
 Long data can be added in parts by SQLBulkOperations, by using calls to SQLParamData and SQLPutData. For more information, see "Providing Long Data for Bulk Inserts and Updates" later in this function reference.  
  
 It is not necessary for the application to call **SQLFetch** or **SQLFetchScroll** before it calls **SQLBulkOperations** (except when going against an ODBC 2.*x* driver; see [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md)).  
  
 The behavior is driver-defined if **SQLBulkOperations**, with an *Operation* argument of SQL_ADD, is called on a cursor that contains duplicate columns. The driver can return a driver-defined SQLSTATE, add the data to the first column that appears in the result set, or perform other driver-defined behavior.  
  
## Performing Bulk Updates by Using Bookmarks  
 To perform bulk updates by using bookmarks with **SQLBulkOperations**, an application performs the following steps in sequence:  
  
1.  Sets the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE.  
  
2.  Executes a query that returns a result set.  
  
3.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows that it wants to update.  
  
4.  Calls **SQLBindCol** to bind the data that it wants to update. The data is bound to an array with a size equal to the value of SQL_ATTR_ROW_ARRAY_SIZE. It also calls **SQLBindCol** to bind column 0 (the bookmark column).  
  
5.  Copies the bookmarks for rows that it is interested in updating into the array bound to column 0.  
  
6.  Updates the data in the bound buffers.  
  
    > [!NOTE]  
    >  The size of the array pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute should be equal to SQL_ATTR_ROW_ARRAY_SIZE or SQL_ATTR_ROW_STATUS_PTR should be a null pointer.  
  
7.  Calls **SQLBulkOperations**(*StatementHandle,* SQL_UPDATE_BY_BOOKMARK).  
  
    > [!NOTE]  
    >  If the application has set the SQL_ATTR_ROW_STATUS_PTR statement attribute, it can inspect this array to see the result of the operation.  
  
8.  Optionally calls **SQLBulkOperations**(*StatementHandle*, SQL_FETCH_BY_BOOKMARK) to fetch data into the bound application buffers to verify that the update has occurred.  
  
9. If data has been updated, the driver changes the value in the row status array for the appropriate rows to SQL_ROW_UPDATED.  
  
 Bulk updates performed by **SQLBulkOperations** can include long data by using calls to **SQLParamData** and **SQLPutData**. For more information, see "Providing Long Data for Bulk Inserts and Updates" later in this function reference.  
  
 If bookmarks persist across cursors, the application does not need to call **SQLFetch** or **SQLFetchScroll** before updating by bookmarks. It can use bookmarks that it has stored from a previous cursor. If bookmarks do not persist across cursors, the application has to call **SQLFetch** or **SQLFetchScroll** to retrieve the bookmarks.  
  
 The behavior is driver-defined if **SQLBulkOperations**, with an *Operation* argument of SQL_UPDATE_BY_BOOKMARK, is called on a cursor that contains duplicate columns. The driver can return a driver-defined SQLSTATE, update the first column that appears in the result set, or perform other driver-defined behavior.  
  
## Performing Bulk Fetches Using Bookmarks  
 To perform bulk fetches using bookmarks with **SQLBulkOperations**, an application performs the following steps in sequence:  
  
1.  Sets the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE.  
  
2.  Executes a query that returns a result set.  
  
3.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows that it wants to fetch.  
  
4.  Calls **SQLBindCol** to bind the data that it wants to fetch. The data is bound to an array with a size equal to the value of SQL_ATTR_ROW_ARRAY_SIZE. It also calls **SQLBindCol** to bind column 0 (the bookmark column).  
  
5.  Copies the bookmarks for rows that it is interested in fetching into the array bound to column 0. (This assumes that the application has already obtained the bookmarks separately.)  
  
    > [!NOTE]  
    >  The size of the array pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute should be equal to SQL_ATTR_ROW_ARRAY_SIZE or SQL_ATTR_ROW_STATUS_PTR should be a null pointer.  
  
6.  Calls **SQLBulkOperations**(*StatementHandle,* SQL_FETCH_BY_BOOKMARK).  
  
7.  If the application has set the SQL_ATTR_ROW_STATUS_PTR statement attribute, it can inspect this array to see the result of the operation.  
  
 If bookmarks persist across cursors, the application does not need to call **SQLFetch** or **SQLFetchScroll** before fetching by bookmarks. It can use bookmarks that it has stored from a previous cursor. If bookmarks do not persist across cursors, the application has to call **SQLFetch** or **SQLFetchScroll** one time to retrieve the bookmarks.  
  
## Performing Bulk Deletes Using Bookmarks  
 To perform bulk deletes using bookmarks with **SQLBulkOperations**, an application performs the following steps in sequence:  
  
1.  Sets the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE.  
  
2.  Executes a query that returns a result set.  
  
3.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows that it wants to delete.  
  
4.  Calls **SQLBindCol** to bind column 0 (the bookmark column).  
  
5.  Copies the bookmarks for rows that it is interested in deleting into the array bound to column 0.  
  
    > [!NOTE]  
    >  The size of the array pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute should be equal to SQL_ATTR_ROW_ARRAY_SIZE or SQL_ATTR_ROW_STATUS_PTR should be a null pointer.  
  
6.  Calls **SQLBulkOperations**(*StatementHandle,* SQL_DELETE_BY_BOOKMARK).  
  
7.  If the application has set the SQL_ATTR_ROW_STATUS_PTR statement attribute, it can inspect this array to see the result of the operation.  
  
 If bookmarks persist across cursors, the application does not have to call **SQLFetch** or **SQLFetchScroll** before deleting by bookmarks. It can use bookmarks that it has stored from a previous cursor. If bookmarks do not persist across cursors, the application has to call **SQLFetch** or **SQLFetchScroll** one time to retrieve the bookmarks.  
  
## Providing Long Data for Bulk Inserts and Updates  
 Long data can be provided for bulk inserts and updates performed by calls to **SQLBulkOperations**. To insert or update long data, an application performs the following steps in addition to the steps described in the "Performing Bulk Inserts" and "Performing Bulk Updates Using Bookmarks" sections earlier in this topic.  
  
1.  When it binds the data by using **SQLBindCol**, the application places an application-defined value, such as the column number, in the *\*TargetValuePtr* buffer for data-at-execution columns. The value can be used later to identify the column.  
  
     The application places the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro in the *\*StrLen_or_IndPtr* buffer. If the SQL data type of the column is SQL_LONGVARBINARY, SQL_LONGVARCHAR, or a long data source-specific data type and the driver returns "Y" for the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo**, *length* is the number of bytes of data to be sent for the parameter; otherwise, it must be a nonnegative value and is ignored.  
  
2.  When **SQLBulkOperations** is called, if there are data-at-execution columns, the function returns SQL_NEED_DATA and proceeds to step 3, which follows. (If there are no data-at-execution columns, the process is complete.)  
  
3.  The application calls **SQLParamData** to retrieve the address of the *\*TargetValuePtr* buffer for the first data-at-execution column to be processed. **SQLParamData** returns SQL_NEED_DATA. The application retrieves the application-defined value from the *\*TargetValuePtr* buffer.  
  
    > [!NOTE]  
    >  Although data-at-execution parameters resemble data-at-execution columns, the value returned by **SQLParamData** is different for each.  
  
     Data-at-execution columns are columns in a rowset for which data will be sent with **SQLPutData** when a row is updated or inserted with **SQLBulkOperations**. They are bound with **SQLBindCol**. The value returned by **SQLParamData** is the address of the row in the **TargetValuePtr* buffer that is being processed.  
  
4.  The application calls **SQLPutData** one or more times to send data for the column. More than one call is needed if all the data value cannot be returned in the *\*TargetValuePtr* buffer specified in **SQLPutData**; multiple calls to **SQLPutData** for the same column are allowed only when sending character C data to a column with a character, binary, or data source-specific data type or when sending binary C data to a column with a character, binary, or data source-specific data type.  
  
5.  The application calls **SQLParamData** again to signal that all data has been sent for the column.  
  
    -   If there are more data-at-execution columns, **SQLParamData** returns SQL_NEED_DATA and the address of the *TargetValuePtr* buffer for the next data-at-execution column to be processed. The application repeats steps 4 and 5.  
  
    -   If there are no more data-at-execution columns, the process is complete. If the statement was executed successfully, **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO; if the execution failed, it returns SQL_ERROR. At this point, **SQLParamData** can return any SQLSTATE that can be returned by **SQLBulkOperations**.  
  
 If the operation is canceled or an error occurs in **SQLParamData** or **SQLPutData** after **SQLBulkOperations** returns SQL_NEED_DATA and before data is sent for all data-at-execution columns, the application can call only **SQLCancel**, **SQLGetDiagField**, **SQLGetDiagRec**, **SQLGetFunctions**, **SQLParamData**, or **SQLPutData** for the statement or the connection associated with the statement. If it calls any other function for the statement or the connection associated with the statement, the function returns SQL_ERROR and SQLSTATE HY010 (Function sequence error).  
  
 If the application calls **SQLCancel** while the driver still needs data for data-at-execution columns, the driver cancels the operation. The application can then call **SQLBulkOperations** again; canceling does not affect the cursor state or the current cursor position.  
  
## Row Status Array  
 The row status array contains status values for each row of data in the rowset after a call to **SQLBulkOperations**. The driver sets the status values in this array after a call to **SQLFetch**, **SQLFetchScroll**, **SQLSetPos**, or **SQLBulkOperations**. This array is initially populated by a call to **SQLBulkOperations** if **SQLFetch** or **SQLFetchScroll** has not been called before **SQLBulkOperations**. This array is pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute. The number of elements in the row status arrays must equal the number of rows in the rowset (as defined by the SQL_ATTR_ROW_ARRAY_SIZE statement attribute). For information about this row status array, see [SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md).  
  
## Code Example  
 The following example fetches 10 rows of data at a time from the Customers table. It then prompts the user for an action to take. To reduce network traffic, the example buffer updates, deletes, and inserts locally in the bound arrays, but at offsets past the rowset data. When the user chooses to send updates, deletes, and inserts to the data source, the code sets the binding offset appropriately and calls **SQLBulkOperations**. For simplicity, the user cannot buffer more than 10 updates, deletes, or inserts.  
  
```cpp  
// SQLBulkOperations_Function.cpp  
// compile with: ODBC32.lib  
#include <windows.h>  
#include <sqlext.h>  
#include "stdio.h"  
  
#define UPDATE_ROW 100  
#define DELETE_ROW 101  
#define ADD_ROW 102  
#define SEND_TO_DATA_SOURCE 103  
#define UPDATE_OFFSET 10  
#define INSERT_OFFSET 20  
#define DELETE_OFFSET 30  
  
// Define structure for customer data (assume 10 byte maximum bookmark size).  
typedef struct tagCustStruct {  
   SQLCHAR Bookmark[10];  
   SQLINTEGER BookmarkLen;  
   SQLUINTEGER CustomerID;  
   SQLINTEGER CustIDInd;  
   SQLCHAR CompanyName[51];  
   SQLINTEGER NameLenOrInd;  
   SQLCHAR Address[51];  
   SQLINTEGER AddressLenOrInd;  
   SQLCHAR Phone[11];  
   SQLINTEGER PhoneLenOrInd;  
} CustStruct;  
  
// Allocate 40 of these structures. Elements 0-9 are for the current rowset,  
// elements 10-19 are for the buffered updates, elements 20-29 are for  
// the buffered inserts, and elements 30-39 are for the buffered deletes.  
CustStruct CustArray[40];  
SQLUSMALLINT RowStatusArray[10], Action, RowNum, NumUpdates = 0, NumInserts = 0,  
NumDeletes = 0;  
SQLLEN BindOffset = 0;  
SQLRETURN retcode;  
SQLHENV henv = NULL;  
SQLHDBC hdbc = NULL;  
SQLHSTMT hstmt = NULL;  
  
int main() {  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
   retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
  
   retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
   retcode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
   retcode = SQLConnect(hdbc, (SQLCHAR*) "Northwind", SQL_NTS, (SQLCHAR*) NULL, 0, NULL, 0);  
   retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
   // Set the following statement attributes:  
   // SQL_ATTR_CURSOR_TYPE:           Keyset-driven  
   // SQL_ATTR_ROW_BIND_TYPE:         Row-wise  
   // SQL_ATTR_ROW_ARRAY_SIZE:        10  
   // SQL_ATTR_USE_BOOKMARKS:         Use variable-length bookmarks  
   // SQL_ATTR_ROW_STATUS_PTR:        Points to RowStatusArray  
   // SQL_ATTR_ROW_BIND_OFFSET_PTR:   Points to BindOffset  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_CURSOR_TYPE, (SQLPOINTER)SQL_CURSOR_KEYSET_DRIVEN, 0);  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_BIND_TYPE, (SQLPOINTER)sizeof(CustStruct), 0);  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)10, 0);  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_USE_BOOKMARKS, (SQLPOINTER)SQL_UB_VARIABLE, 0);  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_STATUS_PTR, RowStatusArray, 0);  
   retcode = SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_BIND_OFFSET_PTR, &BindOffset, 0);  
  
   // Bind arrays to the bookmark, CustomerID, CompanyName, Address, and Phone columns.  
   retcode = SQLBindCol(hstmt, 0, SQL_C_VARBOOKMARK, CustArray[0].Bookmark, sizeof(CustArray[0].Bookmark), &CustArray[0].BookmarkLen);  
   retcode = SQLBindCol(hstmt, 1, SQL_C_ULONG, &CustArray[0].CustomerID, 0, &CustArray[0].CustIDInd);  
   retcode = SQLBindCol(hstmt, 2, SQL_C_CHAR, CustArray[0].CompanyName, sizeof(CustArray[0].CompanyName), &CustArray[0].NameLenOrInd);  
   retcode = SQLBindCol(hstmt, 3, SQL_C_CHAR, CustArray[0].Address, sizeof(CustArray[0].Address), &CustArray[0].AddressLenOrInd);  
   retcode = SQLBindCol(hstmt, 4, SQL_C_CHAR, CustArray[0].Phone, sizeof(CustArray[0].Phone), &CustArray[0].PhoneLenOrInd);  
  
   // Execute a statement to retrieve rows from the Customers table.  
   retcode = SQLExecDirect(hstmt, (SQLCHAR*)"SELECT CustomerID, CompanyName, Address, Phone FROM Customers", SQL_NTS);  
  
   // Fetch and display the first 10 rows.  
   retcode = SQLFetchScroll(hstmt, SQL_FETCH_NEXT, 0);  
   // DisplayCustData(CustArray, 10);  
  
   // Call GetAction to get an action and a row number from the user.  
   // while (GetAction(&Action, &RowNum)) {  
   Action = SQL_FETCH_NEXT;  
   RowNum = 2;  
   switch (Action) {  
      case SQL_FETCH_NEXT:  
      case SQL_FETCH_PRIOR:  
      case SQL_FETCH_FIRST:  
      case SQL_FETCH_LAST:  
      case SQL_FETCH_ABSOLUTE:  
      case SQL_FETCH_RELATIVE:  
         // Fetch and display the requested data.  
         SQLFetchScroll(hstmt, Action, RowNum);  
         // DisplayCustData(CustArray, 10);  
         break;  
  
      case UPDATE_ROW:  
         // Check if we have reached the maximum number of buffered updates.  
         if (NumUpdates < 10) {  
            // Get the new customer data and place it in the next available element of  
            // the buffered updates section of CustArray, copy the bookmark of the row  
            // being updated to the same element, and increment the update counter.  
            // Checking to see we have not already buffered an update for this  
            // row not shown.  
            // GetNewCustData(CustArray, UPDATE_OFFSET + NumUpdates);  
            memcpy(CustArray[UPDATE_OFFSET + NumUpdates].Bookmark,  
               CustArray[RowNum - 1].Bookmark,  
               CustArray[RowNum - 1].BookmarkLen);  
            CustArray[UPDATE_OFFSET + NumUpdates].BookmarkLen =  
               CustArray[RowNum - 1].BookmarkLen;  
            NumUpdates++;  
         } else {  
            printf("Buffers full. Send buffered changes to the data source.");  
         }  
         break;  
      case DELETE_ROW:  
         // Check if we have reached the maximum number of buffered deletes.  
         if (NumDeletes < 10) {  
            // Copy the bookmark of the row being deleted to the next available element  
            // of the buffered deletes section of CustArray and increment the delete  
            // counter. Checking to see we have not already buffered an update for  
            // this row not shown.  
            memcpy(CustArray[DELETE_OFFSET + NumDeletes].Bookmark,  
               CustArray[RowNum - 1].Bookmark,  
               CustArray[RowNum - 1].BookmarkLen);  
  
            CustArray[DELETE_OFFSET + NumDeletes].BookmarkLen =  
               CustArray[RowNum - 1].BookmarkLen;  
  
            NumDeletes++;  
         } else  
            printf("Buffers full. Send buffered changes to the data source.");  
         break;  
  
      case ADD_ROW:  
         // reached maximum number of buffered inserts?  
         if (NumInserts < 10) {  
            // Get the new customer data and place it in the next available element of  
            // the buffered inserts section of CustArray and increment insert counter.  
            // GetNewCustData(CustArray, INSERT_OFFSET + NumInserts);  
            NumInserts++;  
         } else  
            printf("Buffers full. Send buffered changes to the data source.");  
         break;  
  
      case SEND_TO_DATA_SOURCE:  
         // If there are any buffered updates, inserts, or deletes, set the array size  
         // to that number, set the binding offset to use the data in the buffered  
         // update, insert, or delete part of CustArray, and call SQLBulkOperations to  
         // do the updates, inserts, or deletes. Because we will never have more than  
         // 10 updates, inserts, or deletes, we can use the same row status array.  
         if (NumUpdates) {  
            SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)NumUpdates, 0);  
            BindOffset = UPDATE_OFFSET * sizeof(CustStruct);  
            SQLBulkOperations(hstmt, SQL_UPDATE_BY_BOOKMARK);  
            NumUpdates = 0;  
         }  
  
         if (NumInserts) {  
            SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)NumInserts, 0);  
            BindOffset = INSERT_OFFSET * sizeof(CustStruct);  
            SQLBulkOperations(hstmt, SQL_ADD);  
            NumInserts = 0;  
         }  
  
         if (NumDeletes) {  
            SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)NumDeletes, 0);  
            BindOffset = DELETE_OFFSET * sizeof(CustStruct);  
            SQLBulkOperations(hstmt, SQL_DELETE_BY_BOOKMARK);  
            NumDeletes = 0;  
         }  
  
         // If there were any updates, inserts, or deletes, reset the binding offset  
         // and array size to their original values.  
         if (NumUpdates || NumInserts || NumDeletes) {  
            SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)10, 0);  
            BindOffset = 0;  
         }  
         break;  
   }  
   // }  
  
   // Close the cursor.  
   SQLFreeStmt(hstmt, SQL_CLOSE);  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Getting a single field of a descriptor|[SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)|  
|Getting multiple fields of a descriptor|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting a single field of a descriptor|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
|Setting multiple fields of a descriptor|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
|Positioning the cursor, refreshing data in the rowset, or updating or deleting data in the rowset|[SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
