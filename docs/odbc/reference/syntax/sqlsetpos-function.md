---
description: "SQLSetPos Function"
title: "SQLSetPos Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLSetPos"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetPos"
helpviewer_keywords: 
  - "SQLSetPos function [ODBC]"
ms.assetid: 80190ee7-ae3b-45e5-92a9-693eb558f322
author: David-Engel
ms.author: v-davidengel
---
# SQLSetPos Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLSetPos** sets the cursor position in a rowset and allows an application to refresh data in the rowset or to update or delete data in the result set.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLSetPos(  
      SQLHSTMT        StatementHandle,  
      SQLSETPOSIROW   RowNumber,  
      SQLUSMALLINT    Operation,  
      SQLUSMALLINT    LockType);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *RowNumber*  
 [Input] Position of the row in the rowset on which to perform the operation specified with the *Operation* argument. If *RowNumber* is 0, the operation applies to every row in the rowset.  
  
 For additional information, see "Comments."  
  
 *Operation*  
 [Input] Operation to perform:  
  
 SQL_POSITION SQL_REFRESH SQL_UPDATE SQL_DELETE  
  
> [!NOTE]
>  The SQL_ADD value for the *Operation* argument has been deprecated for ODBC *3.x*. ODBC *3.x* drivers will need to support SQL_ADD for backward compatibility. This functionality has been replaced by a call to **SQLBulkOperations** with an *Operation* of SQL_ADD. When an ODBC *3.x* application works with an ODBC *2.x* driver, the Driver Manager maps a call to **SQLBulkOperations** with an *Operation* of SQL_ADD to **SQLSetPos** with an *Operation* of SQL_ADD.  
  
 For more information, see "Comments."  
  
 *LockType*  
 [Input] Specifies how to lock the row after performing the operation specified in the *Operation* argument.  
  
 SQL_LOCK_NO_CHANGE SQL_LOCK_EXCLUSIVE SQL_LOCK_UNLOCK  
  
 For more information, see "Comments."  
  
## Returns  
  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NEED_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLSetPos** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLSetPos** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
 For all those SQLSTATEs that can return SQL_SUCCESS_WITH_INFO or SQL_ERROR (except 01xxx SQLSTATEs), SQL_SUCCESS_WITH_INFO is returned if an error occurs on one or more, but not all, rows of a multirow operation, and SQL_ERROR is returned if an error occurs on a single-row operation.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01001|Cursor operation conflict|The *Operation* argument was SQL_DELETE or SQL_UPDATE, and no rows or more than one row were deleted or updated. (For more information about updates to more than one row, see the description of the SQL_ATTR_SIMULATE_CURSOR *Attribute* in **SQLSetStmtAttr**.) (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> The *Operation* argument was SQL_DELETE or SQL_UPDATE, and the operation failed because of optimistic concurrency. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data right truncation|The *Operation* argument was SQL_REFRESH, and string or binary data returned for a column or columns with a data type of SQL_C_CHAR or SQL_C_BINARY resulted in the truncation of nonblank character or non-NULL binary data.|  
|01S01|Error in row|The *RowNumber* argument was 0, and an error occurred in one or more rows while performing the operation specified with the *Operation* argument.<br /><br /> (SQL_SUCCESS_WITH_INFO is returned if an error occurs on one or more, but not all, rows of a multirow operation, and SQL_ERROR is returned if an error occurs on a single-row operation.)<br /><br /> (This SQLSTATE is returned only when **SQLSetPos** is called after **SQLExtendedFetch**, if the driver is an ODBC *2.x* driver and the cursor library is not used.)|  
|01S07|Fractional truncation|The *Operation* argument was SQL_REFRESH, the data type of the application buffer was not SQL_C_CHAR or SQL_C_BINARY, and the data returned to application buffers for one or more columns was truncated. For numeric data types, the fractional part of the number was truncated. For time, timestamp, and interval data types containing a time component, the fractional portion of the time was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data value of a column in the result set could not be converted to the data type specified by *TargetType* in the call to **SQLBindCol**.|  
|07009|Invalid descriptor index|The argument *Operation* was SQL_REFRESH or SQL_UPDATE, and a column was bound with a column number greater than the number of columns in the result set.|  
|21S02|Degree of derived table does not match column list|The argument *Operation* was SQL_UPDATE, and no columns were updatable because all columns were either unbound, read-only, or the value in the bound length/indicator buffer was SQL_COLUMN_IGNORE.|  
|22001|String data, right truncation|The *Operation* argument was SQL_UPDATE, and the assignment of a character or binary value to a column resulted in the truncation of nonblank (for characters) or non-null (for binary) characters or bytes.|  
|22003|Numeric value out of range|The argument *Operation* was SQL_UPDATE, and the assignment of a numeric value to a column in the result set caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> The argument *Operation* was SQL_REFRESH, and returning the numeric value for one or more bound columns would have caused a loss of significant digits.|  
|22007|Invalid datetime format|The argument *Operation* was SQL_UPDATE, and the assignment of a date or timestamp value to a column in the result set caused the year, month, or day field to be out of range.<br /><br /> The argument *Operation* was SQL_REFRESH, and returning the date or timestamp value for one or more bound columns would have caused the year, month, or day field to be out of range.|  
|22008|Date/time field  overflow|The *Operation* argument was SQL_UPDATE, and the performance of datetime arithmetic on data being sent to a column in the result set resulted in a datetime field (the year, month, day, hour, minute, or second field) of the result being outside the permissible range of values for the field, or being invalid based on the Gregorian calendar's natural rules for datetimes.<br /><br /> The *Operation* argument was SQL_REFRESH, and the performance of datetime arithmetic on data being retrieved from the result set resulted in a datetime field (the year, month, day, hour, minute, or second field) of the result being outside the permissible range of values for the field, or being invalid based on the Gregorian calendar's natural rules for datetimes.|  
|22015|Interval field overflow|The *Operation* argument was SQL_UPDATE, and the assignment of an exact numeric or interval C type to an interval SQL data type caused a loss of significant digits.<br /><br /> The *Operation* argument was SQL_UPDATE; when assigning to an interval SQL type, there was no representation of the value of the C type in the interval SQL type.<br /><br /> The *Operation* argument was SQL_REFRESH, and assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> The *Operation* argument was SQL_ REFRESH; when assigning to an interval C type, there was no representation of the value of the SQL type in the interval C type.|  
|22018|Invalid character value for cast specification|The *Operation* argument was SQL_REFRESH; the C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.<br /><br /> The argument *Operation* was SQL_UPDATE; the SQL type was an exact or approximate numeric, a datetime, or an interval data type; the C type was SQL_C_CHAR; and the value in the column was not a valid literal of the bound SQL type.|  
|23000|Integrity constraint violation|The argument *Operation* was SQL_DELETE or SQL_UPDATE, and an integrity constraint was violated.|  
|24000|Invalid cursor state|The *StatementHandle* was in an executed state, but no result set was associated with the *StatementHandle*.<br /><br /> (DM) A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.<br /><br /> A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called, but the cursor was positioned before the start of the result set or after the end of the result set.<br /><br /> The argument *Operation* was SQL_DELETE, SQL_REFRESH, or SQL_UPDATE, and the cursor was positioned before the start of the result set or after the end of the result set.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|42000|Syntax error or access violation|The driver was unable to lock the row as needed to perform the operation requested in the argument *Operation*.<br /><br /> The driver was unable to lock the row as requested in the argument *LockType*.|  
|44000|WITH CHECK OPTION violation|The *Operation* argument was SQL_UPDATE, and the update was performed on a viewed table or a table derived from the viewed table which was created by specifying **WITH CHECK OPTION**, such that one or more rows affected by the update will no longer be present in the viewed table.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the SQLSetPos function was called.<br /><br /> (DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute**, or a catalog function.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) The driver was an ODBC *2.x* driver, and **SQLSetPos** was called for a *StatementHandle* after **SQLFetch** was called.|  
|HY011|Attribute cannot be set now|(DM) The driver was an ODBC *2.x* driver; the SQL_ATTR_ROW_STATUS_PTR statement attribute was set; then **SQLSetPos** was called before **SQLFetch**, **SQLFetchScroll**, or **SQLExtendedFetch** was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|The *Operation* argument was SQL_UPDATE, a data value was a null pointer, and the column length value was not 0, SQL_DATA_AT_EXEC, SQL_COLUMN_IGNORE, SQL_NULL_DATA, or less than or equal to SQL_LEN_DATA_AT_EXEC_OFFSET.<br /><br /> The *Operation* argument was SQL_UPDATE; a data value was not a null pointer; the C data type was SQL_C_BINARY or SQL_C_CHAR; and the column length value was less than 0 but not equal to SQL_DATA_AT_EXEC, SQL_COLUMN_IGNORE, SQL_NTS, or SQL_NULL_DATA, or less than or equal to SQL_LEN_DATA_AT_EXEC_OFFSET.<br /><br /> The value in a length/indicator buffer was SQL_DATA_AT_EXEC; the SQL type was either SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type; and the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y".|  
|HY092|Invalid attribute identifier|(DM) The value specified for the *Operation* argument was invalid.<br /><br /> (DM) The value specified for the *LockType* argument was invalid.<br /><br /> The *Operation* argument was SQL_UPDATE or SQL_DELETE, and the SQL_ATTR_CONCURRENCY statement attribute was SQL_ATTR_CONCUR_READ_ONLY.|  
|HY107|Row value out of range|The value specified for the argument *RowNumber* was greater than the number of rows in the rowset.|  
|HY109|Invalid cursor position|The cursor associated with the *StatementHandle* was defined as forward-only, so the cursor could not be positioned within the rowset. See the description for the SQL_ATTR_CURSOR_TYPE attribute in **SQLSetStmtAttr**.<br /><br /> The *Operation* argument was SQL_UPDATE, SQL_DELETE, or SQL_REFRESH, and the row identified by the *RowNumber* argument had been deleted or had not been fetched.<br /><br /> (DM) The *RowNumber* argument was 0, and the *Operation* argument was SQL_POSITION.<br /><br /> **SQLSetPos** was called after **SQLBulkOperations** was called and before **SQLFetchScroll** or **SQLFetch** was called.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the operation requested in the *Operation* argument or the *LockType* argument.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr** with an *Attribute* of SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
  
> [!CAUTION]
>  For information on the statement states that **SQLSetPos** can be called in and what it needs to do for compatibility with ODBC *2.x* applications, see [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md).  
  
## RowNumber Argument  
 The *RowNumber* argument specifies the number of the row in the rowset on which to perform the operation specified by the *Operation* argument. If *RowNumber* is 0, the operation applies to every row in the rowset. *RowNumber* must be a value from 0 to the number of rows in the rowset.  
  
> [!NOTE]  
>  In the C language, arrays are 0-based and the *RowNumber* argument is 1-based. For example, to update the fifth row of the rowset, an application modifies the rowset buffers at array index 4 but specifies a *RowNumber* of 5.  
  
 All operations position the cursor on the row specified by *RowNumber*. The following operations require a cursor position:  
  
-   Positioned update and delete statements.  
  
-   Calls to **SQLGetData**.  
  
-   Calls to **SQLSetPos** with the SQL_DELETE, SQL_REFRESH, and SQL_UPDATE options.  
  
 For example, if *RowNumber* is 2 for a call to **SQLSetPos** with an *Operation* of SQL_DELETE, the cursor is positioned on the second row of the rowset and that row is deleted. The entry in the implementation row status array (pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute) for the second row is changed to SQL_ROW_DELETED.  
  
 An application can specify a cursor position when it calls **SQLSetPos**. Generally, it calls **SQLSetPos** with the SQL_POSITION or SQL_REFRESH operation to position the cursor before executing a positioned update or delete statement or calling **SQLGetData**.  
  
## Operation Argument  
 The *Operation* argument supports the following operations. To determine which options are supported by a data source, an application calls **SQLGetInfo** with the SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 information type (depending on the type of the cursor).  
  
|*Operation*<br /><br /> argument|Operation|  
|------------------------------|---------------|  
|SQL_POSITION|The driver positions the cursor on the row specified by *RowNumber*.<br /><br /> The contents of the row status array pointed to by the SQL_ATTR_ROW_OPERATION_PTR statement attribute are ignored for the SQL_POSITION *Operation*.|  
|SQL_REFRESH|The driver positions the cursor on the row specified by *RowNumber* and refreshes data in the rowset buffers for that row. For more information about how the driver returns data in the rowset buffers, see the descriptions of row-wise and column-wise binding in **SQLBindCol**.<br /><br /> **SQLSetPos** with an *Operation* of SQL_REFRESH updates the status and content of the rows within the current fetched rowset. This includes refreshing the bookmarks. Because the data in the buffers is refreshed but not refetched, the membership in the rowset is fixed. This is different from the refresh performed by a call to **SQLFetchScroll** with a *FetchOrientation* of SQL_FETCH_RELATIVE and a *RowNumber* equal to 0, which refetches the rowset from the result set so that it can show added data and remove deleted data if those operations are supported by the driver and the cursor.<br /><br /> A successful refresh with **SQLSetPos** will not change a row status of SQL_ROW_DELETED. Deleted rows within the rowset will continue to be marked as deleted until the next fetch. The rows will disappear at the next fetch if the cursor supports packing (in which a subsequent **SQLFetch** or **SQLFetchScroll** does not return deleted rows).<br /><br /> Added rows do not appear when a refresh with **SQLSetPos** is performed. This behavior is different from **SQLFetchScroll** with a *FetchType* of SQL_FETCH_RELATIVE and a *RowNumber* equal to 0, which also refreshes the current rowset but will show added records or pack deleted records if these operations are supported by the cursor.<br /><br /> A successful refresh with **SQLSetPos** will change a row status of SQL_ROW_ADDED to SQL_ROW_SUCCESS (if the row status array exists).<br /><br /> A successful refresh with **SQLSetPos** will change a row status of SQL_ROW_UPDATED to the row's new status (if the row status array exists).<br /><br /> If an error occurs in a **SQLSetPos** operation on a row, the row status is set to SQL_ROW_ERROR (if the row status array exists).<br /><br /> For a cursor opened with an SQL_ATTR_CONCURRENCY statement attribute of SQL_CONCUR_ROWVER or SQL_CONCUR_VALUES, a refresh with **SQLSetPos** might update the optimistic concurrency values used by the data source to detect that the row has changed. If this occurs, the row versions or values used to ensure cursor concurrency are updated whenever the rowset buffers are refreshed from the server. This occurs for each row that is refreshed.<br /><br /> The contents of the row status array pointed to by the SQL_ATTR_ROW_OPERATION_PTR statement attribute are ignored for the SQL_REFRESH *Operation*.|  
|SQL_UPDATE|The driver positions the cursor on the row specified by *RowNumber* and updates the underlying row of data with the values in the rowset buffers (the *TargetValuePtr* argument in **SQLBindCol**). It retrieves the lengths of the data from the length/indicator buffers (the *StrLen_or_IndPtr* argument in **SQLBindCol**). If the length of any column is SQL_COLUMN_IGNORE, the column is not updated. After updating the row, the driver changes the corresponding element of the row status array to SQL_ROW_UPDATED or SQL_ROW_SUCCESS_WITH_INFO (if the row status array exists).<br /><br /> It is driver-defined what the behavior is if **SQLSetPos** with an *Operation* argument of SQL_UPDATE is called on a cursor that contains duplicate columns. The driver can return a driver-defined SQLSTATE, can update the first column that appears in the result set, or perform other driver-defined behavior.<br /><br /> The row operation array pointed to by the SQL_ATTR_ROW_OPERATION_PTR statement attribute can be used to indicate that a row in the current rowset should be ignored during a bulk update. For more information, see "Status and Operation Arrays" later in this function reference.|  
|SQL_DELETE|The driver positions the cursor on the row specified by *RowNumber* and deletes the underlying row of data. It changes the corresponding element of the row status array to SQL_ROW_DELETED. After the row has been deleted, the following are not valid for the row: positioned update and delete statements, calls to **SQLGetData**, and calls to **SQLSetPos** with *Operation* set to anything except SQL_POSITION. For drivers that support packing, the row is deleted from the cursor when new data is retrieved from the data source.<br /><br /> Whether the row remains visible depends on the cursor type. For example, deleted rows are visible to static and keyset-driven cursors but invisible to dynamic cursors.<br /><br /> The row operation array pointed to by the SQL_ATTR_ROW_OPERATION_PTR statement attribute can be used to indicate that a row in the current rowset should be ignored during a bulk delete. For more information, see "Status and Operation Arrays" later in this function reference.|  
  
## LockType Argument  
 The *LockType* argument provides a way for applications to control concurrency. In most cases, data sources that support concurrency levels and transactions will support only the SQL_LOCK_NO_CHANGE value of the *LockType* argument. The *LockType* argument is generally used only for file-based support.  
  
 The *LockType* argument specifies the lock state of the row after **SQLSetPos** has been executed. If the driver is unable to lock the row either to perform the requested operation or to satisfy the *LockType* argument, it returns SQL_ERROR and SQLSTATE 42000 (Syntax error or access violation).  
  
 Although the *LockType* argument is specified for a single statement, the lock accords the same privileges to all statements on the connection. In particular, a lock that is acquired by one statement on a connection can be unlocked by a different statement on the same connection.  
  
 A row locked through **SQLSetPos** remains locked until the application calls **SQLSetPos** for the row with *LockType* set to SQL_LOCK_UNLOCK, or until the application calls **SQLFreeHandle** for the statement or **SQLFreeStmt** with the SQL_CLOSE option. For a driver that supports transactions, a row locked through **SQLSetPos** is unlocked when the application calls **SQLEndTran** to commit or roll back a transaction on the connection (if a cursor is closed when a transaction is committed or rolled back, as indicated by the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR information types returned by **SQLGetInfo**).  
  
 The *LockType* argument supports the following types of locks. To determine which locks are supported by a data source, an application calls **SQLGetInfo** with the SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 information type (depending on the type of the cursor).  
  
|*LockType* argument|Lock type|  
|-------------------------|---------------|  
|SQL_LOCK_NO_CHANGE|The driver or data source ensures that the row is in the same locked or unlocked state as it was before **SQLSetPos** was called. This value of *LockType* allows data sources that do not support explicit row-level locking to use whatever locking is required by the current concurrency and transaction isolation levels.|  
|SQL_LOCK_EXCLUSIVE|The driver or data source locks the row exclusively. A statement on a different connection or in a different application cannot be used to acquire any locks on the row.|  
|SQL_LOCK_UNLOCK|The driver or data source unlocks the row.|  
  
 If a driver supports SQL_LOCK_EXCLUSIVE but does not support SQL_LOCK_UNLOCK, a row that is locked will remain locked until one of the function calls described in the previous paragraph occurs.  
  
 If a driver supports SQL_LOCK_EXCLUSIVE but does not support SQL_LOCK_UNLOCK, a row that is locked will remain locked until the application calls **SQLFreeHandle** for the statement or **SQLFreeStmt** with the SQL_CLOSE option. If the driver supports transactions and closes the cursor upon committing or rolling back the transaction, the application calls **SQLEndTran**.  
  
 For the update and delete operations in **SQLSetPos**, the application uses the *LockType* argument as follows:  
  
-   To guarantee that a row does not change after it is retrieved, an application calls **SQLSetPos** with *Operation* set to SQL_REFRESH and *LockType* set to SQL_LOCK_EXCLUSIVE.  
  
-   If the application sets *LockType* to SQL_LOCK_NO_CHANGE, the driver guarantees that an update or delete operation will succeed only if the application specified SQL_CONCUR_LOCK for the SQL_ATTR_CONCURRENCY statement attribute.  
  
-   If the application specifies SQL_CONCUR_ROWVER or SQL_CONCUR_VALUES for the SQL_ATTR_CONCURRENCY statement attribute, the driver compares row versions or values and rejects the operation if the row has changed since the application fetched the row.  
  
-   If the application specifies SQL_CONCUR_READ_ONLY for the SQL_ATTR_CONCURRENCY statement attribute, the driver rejects any update or delete operation.  
  
 For more information about the SQL_ATTR_CONCURRENCY statement attribute, see [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md).  
  
## Status and Operation Arrays  
 The following status and operation arrays are used when calling **SQLSetPos**:  
  
-   The row status array (as pointed to by the SQL_DESC_ARRAY_STATUS_PTR field in the IRD and the SQL_ATTR_ROW_STATUS_ARRAY statement attribute) contains status values for each row of data in the rowset. The driver sets the status values in this array after a call to **SQLFetch**, **SQLFetchScroll**, **SQLBulkOperations**, or **SQLSetPos**. This array is pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute.  
  
-   The row operation array (as pointed to by the SQL_DESC_ARRAY_STATUS_PTR field in the ARD and the SQL_ATTR_ROW_OPERATION_ARRAY statement attribute) contains a value for each row in the rowset that indicates whether a call to **SQLSetPos** for a bulk operation is ignored or performed. Each element in the array is set to either SQL_ROW_PROCEED (the default) or SQL_ROW_IGNORE. This array is pointed to by the SQL_ATTR_ROW_OPERATION_PTR statement attribute.  
  
 The number of elements in the status and operation arrays must equal the number of rows in the rowset (as defined by the SQL_ATTR_ROW_ARRAY_SIZE statement attribute).  
  
 For information about the row status array, see [SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md). For information about the row operation array, see "Ignoring a Row in a Bulk Operation," later in this section.  
  
## Using SQLSetPos  
 Before an application calls **SQLSetPos**, it must perform the following sequence of steps:  
  
1.  If the application will call **SQLSetPos** with *Operation* set to SQL_UPDATE, call **SQLBindCol** (or **SQLSetDescRec**) for each column to specify its data type and bind buffers for the column's data and length.  
  
2.  If the application will call **SQLSetPos** with *Operation* set to SQL_DELETE or SQL_UPDATE, call **SQLColAttribute** to make sure that the columns to be deleted or updated are updatable.  
  
3.  Call **SQLExecDirect**, **SQLExecute**, or a catalog function to create a result set.  
  
4.  Call **SQLFetch** or **SQLFetchScroll** to retrieve the data.  
  
 For more information about using **SQLSetPos**, see [Updating Data with SQLSetPos](../../../odbc/reference/develop-app/updating-data-with-sqlsetpos.md).  
  
## Deleting Data Using SQLSetPos  
 To delete data with **SQLSetPos**, an application calls **SQLSetPos** with *RowNumber* set to the number of the row to delete and *Operation* set to SQL_DELETE.  
  
 After the data has been deleted, the driver changes the value in the implementation row status array for the appropriate row to SQL_ROW_DELETED (or SQL_ROW_ERROR).  
  
## Updating Data Using SQLSetPos  
 An application can pass the value for a column either in the bound data buffer or with one or more calls to **SQLPutData**. Columns whose data is passed with **SQLPutData** are known as *data-at-execution* *columns*. These are commonly used to send data for SQL_LONGVARBINARY and SQL_LONGVARCHAR columns and can be mixed with other columns.  
  
#### To update data with SQLSetPos, an application:  
  
1.  Places values in the data and length/indicator buffers bound with **SQLBindCol**:  
  
    -   For normal columns, the application places the new column value in the *\*TargetValuePtr* buffer and the length of that value in the *\*StrLen_or_IndPtr* buffer. If the row should not be updated, the application places SQL_ROW_IGNORE in that row's element of the row operation array.  
  
    -   For data-at-execution columns, the application places an application-defined value, such as the column number, in the *\*TargetValuePtr* buffer. The value can be used later to identify the column.  
  
         The application places the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro in the **StrLen_or_IndPtr* buffer. If the SQL data type of the column is SQL_LONGVARBINARY, SQL_LONGVARCHAR, or a long data source-specific data type and the driver returns "Y" for the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo**, *length* is the number of bytes of data to be sent for the parameter; otherwise, it must be a non-negative value and is ignored.  
  
2.  Calls **SQLSetPos** with the *Operation* argument set to SQL_UPDATE to update the row of data.  
  
    -   If there are no data-at-execution columns, the process is complete.  
  
    -   If there are any data-at-execution columns, the function returns SQL_NEED_DATA and proceeds to step 3.  
  
3.  Calls **SQLParamData** to retrieve the address of the *\*TargetValuePtr* buffer for the first data-at-execution column to be processed. **SQLParamData** returns SQL_NEED_DATA. The application retrieves the application-defined value from the *\*TargetValuePtr* buffer.  
  
    > [!NOTE]  
    >  Although data-at-execution parameters are similar to data-at-execution columns, the value returned by **SQLParamData** is different for each.  
  
    > [!NOTE]  
    >  Data-at-execution parameters are parameters in an SQL statement for which data will be sent with **SQLPutData** when the statement is executed with **SQLExecDirect** or **SQLExecute**. They are bound with **SQLBindParameter** or by setting descriptors with **SQLSetDescRec**. The value returned by **SQLParamData** is a 32-bit value passed to **SQLBindParameter** in the *ParameterValuePtr* argument.  
  
    > [!NOTE]  
    >  Data-at-execution columns are columns in a rowset for which data will be sent with **SQLPutData** when a row is updated with **SQLSetPos**. They are bound with **SQLBindCol**. The value returned by **SQLParamData** is the address of the row in the **TargetValuePtr* buffer that is being processed.  
  
4.  Calls **SQLPutData** one or more times to send data for the column. More than one call is needed if all the data values cannot be returned in the *\*TargetValuePtr* buffer specified in **SQLPutData**; multiple calls to **SQLPutData** for the same column are allowed only when sending character C data to a column with a character, binary, or data source-specific data type or when sending binary C data to a column with a character, binary, or data source-specific data type.  
  
5.  Calls **SQLParamData** again to signal that all data has been sent for the column.  
  
    -   If there are more data-at-execution columns, **SQLParamData** returns SQL_NEED_DATA and the address of the *TargetValuePtr* buffer for the next data-at-execution column to be processed. The application repeats steps 4 and 5.  
  
    -   If there are no more data-at-execution columns, the process is complete. If the statement was executed successfully, **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO; if the execution failed, it returns SQL_ERROR. At this point, **SQLParamData** can return any SQLSTATE that can be returned by **SQLSetPos**.  
  
 If data has been updated, the driver changes the value in the implementation row status array for the appropriate row to SQL_ROW_UPDATED.  
  
 If the operation is canceled or an error occurs in **SQLParamData** or **SQLPutData**, after **SQLSetPos** returns SQL_NEED_DATA and before data is sent for all data-at-execution columns, the application can call only **SQLCancel**, **SQLGetDiagField**, **SQLGetDiagRec**, **SQLGetFunctions**, **SQLParamData**, or **SQLPutData** for the statement or the connection associated with the statement. If it calls any other function for the statement or the connection associated with the statement, the function returns SQL_ERROR and SQLSTATE HY010 (Function sequence error).  
  
 If the application calls **SQLCancel** while the driver still needs data for data-at-execution columns, the driver cancels the operation. The application can then call **SQLSetPos** again; canceling does not affect the cursor state or the current cursor position.  
  
 When the SELECT-list of the query specification associated with the cursor contains more than one reference to the same column, whether an error is generated or the driver ignores the duplicated references and performs the requested operations is driver-defined.  
  
## Performing Bulk Operations  
 If the *RowNumber* argument is 0, the driver performs the operation specified in the *Operation* argument for every row in the rowset that has a value of SQL_ROW_PROCEED in its field in the row operation array pointed to by SQL_ATTR_ROW_OPERATION_PTR statement attribute. This is a valid value of the *RowNumber* argument for an *Operation* argument of SQL_DELETE, SQL_REFRESH, or SQL_UPDATE, but not SQL_POSITION. **SQLSetPos** with an *Operation* of SQL_POSITION and a *RowNumber* equal to 0 will return SQLSTATE HY109 (Invalid cursor position).  
  
 If an error occurs that pertains to the entire rowset, such as SQLSTATE HYT00 (Timeout expired), the driver returns SQL_ERROR and the appropriate SQLSTATE. The contents of the rowset buffers are undefined, and the cursor position is unchanged.  
  
 If an error occurs that pertains to a single row, the driver:  
  
-   Sets the element for the row in the row status array pointed to by the SQL_ATTR_ROW_STATUS_PTR statement attribute to SQL_ROW_ERROR.  
  
-   Posts one or more additional SQLSTATEs for the error in the error queue and sets the SQL_DIAG_ROW_NUMBER field in the diagnostic data structure.  
  
 After it has processed the error or warning, if the driver completes the operation for the remaining rows in the rowset, it returns SQL_SUCCESS_WITH_INFO. Thus, for each row that returned an error, the error queue contains zero or more additional SQLSTATEs. If the driver stops the operation after it has processed the error or warning, it returns SQL_ERROR.  
  
 If the driver returns any warnings, such as SQLSTATE 01004 (Data truncated), it returns warnings that apply to the entire rowset or to unknown rows in the rowset before it returns the error information that applies to specific rows. It returns warnings for specific rows along with any other error information about those rows.  
  
 If *RowNumber* is equal to 0 and *Operation* is SQL_UPDATE, SQL_REFRESH, or SQL_DELETE, the number of rows that **SQLSetPos** operates on is pointed to by the SQL_ATTR_ROWS_FETCHED_PTR statement attribute.  
  
 If *RowNumber* is equal to 0 and *Operation* is SQL_DELETE, SQL_REFRESH, or SQL_UPDATE, the current row after the operation is the same as the current row before the operation.  
  
## Ignoring a Row in a Bulk Operation  
 The row operation array can be used to indicate that a row in the current rowset should be ignored during a bulk operation using **SQLSetPos**. To direct the driver to ignore one or more rows during a bulk operation, an application should perform the following steps:  
  
1.  Call **SQLSetStmtAttr** to set the SQL_ATTR_ROW_OPERATION_PTR statement attribute to point to an array of SQLUSMALLINTs. This field can also be set by calling **SQLSetDescField** to set the SQL_DESC_ARRAY_STATUS_PTR header field of the ARD, which requires that an application obtains the descriptor handle.  
  
2.  Set each element of the row operation array to one of two values:  
  
    -   SQL_ROW_IGNORE, to indicate that the row is excluded for the bulk operation.  
  
    -   SQL_ROW_PROCEED, to indicate that the row is included in the bulk operation. (This is the default value.)  
  
3.  Call **SQLSetPos** to perform the bulk operation.  
  
 The following rules apply to the row operation array:  
  
-   SQL_ROW_IGNORE and SQL_ROW_PROCEED affect only bulk operations using **SQLSetPos** with an *Operation* of SQL_DELETE or SQL_UPDATE. They do not affect calls to **SQLSetPos** with an *Operation* of SQL_REFRESH or SQL_POSITION.  
  
-   The pointer is set to null by default.  
  
-   If the pointer is null, all rows are updated as if all elements were set to SQL_ROW_PROCEED.  
  
-   Setting an element to SQL_ROW_PROCEED does not guarantee that the operation will occur on that particular row. For example, if a certain row in the rowset has the status SQL_ROW_ERROR, the driver might not be able to update that row regardless of whether the application specified SQL_ROW_PROCEED. An application must always check the row status array to see whether the operation was successful.  
  
-   SQL_ROW_PROCEED is defined as 0 in the header file. An application can initialize the row operation array to 0 in order to process all rows.  
  
-   If element number "n" in the row operation array is set to SQL_ROW_IGNORE and **SQLSetPos** is called to perform a bulk update or delete operation, the nth row in the rowset remains unchanged after the call to **SQLSetPos**.  
  
-   An application should automatically set a read-only column to SQL_ROW_IGNORE.  
  
## Ignoring a Column in a Bulk Operation  
 To avoid unnecessary processing diagnostics generated by attempted updates to one or more read-only columns, an application can set the value in the bound length/indicator buffer to SQL_COLUMN_IGNORE. For more information, see [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
## Code Example  
 In the following example, an application allows a user to browse the ORDERS table and update order status. The cursor is keyset-driven with a rowset size of 20 and uses optimistic concurrency control comparing row versions. After each rowset is fetched, the application prints it and allows the user to select and update the status of an order. The application uses **SQLSetPos** to position the cursor on the selected row and performs a positioned update of the row. (Error handling is omitted for clarity.)  
  
```cpp  
#define ROWS 20  
#define STATUS_LEN 6  
  
SQLCHAR        szStatus[ROWS][STATUS_LEN], szReply[3];  
SQLINTEGER     cbStatus[ROWS], cbOrderID;  
SQLUSMALLINT   rgfRowStatus[ROWS];  
SQLUINTEGER    sOrderID, crow = ROWS, irow;  
SQLHSTMT       hstmtS, hstmtU;  
  
SQLSetStmtAttr(hstmtS, SQL_ATTR_CONCURRENCY, (SQLPOINTER) SQL_CONCUR_ROWVER, 0);  
SQLSetStmtAttr(hstmtS, SQL_ATTR_CURSOR_TYPE, (SQLPOINTER) SQL_CURSOR_KEYSET_DRIVEN, 0);  
SQLSetStmtAttr(hstmtS, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER) ROWS, 0);  
SQLSetStmtAttr(hstmtS, SQL_ATTR_ROW_STATUS_PTR, (SQLPOINTER) rgfRowStatus, 0);  
SQLSetCursorName(hstmtS, "C1", SQL_NTS);  
SQLExecDirect(hstmtS, "SELECT ORDERID, STATUS FROM ORDERS ", SQL_NTS);  
  
SQLBindCol(hstmtS, 1, SQL_C_ULONG, &sOrderID, 0, &cbOrderID);  
SQLBindCol(hstmtS, 2, SQL_C_CHAR, szStatus, STATUS_LEN, &cbStatus);  
  
while ((retcode == SQLFetchScroll(hstmtS, SQL_FETCH_NEXT, 0)) != SQL_ERROR) {  
   if (retcode == SQL_NO_DATA_FOUND)  
      break;  
   for (irow = 0; irow < crow; irow++) {  
      if (rgfRowStatus[irow] != SQL_ROW_DELETED)  
         printf("%2d %5d %*s\n", irow+1, sOrderID, NAME_LEN-1, szStatus[irow]);  
   }  
   while (TRUE) {  
      printf("\nRow number to update?");  
      gets_s(szReply, 3);  
      irow = atoi(szReply);  
      if (irow > 0 && irow <= crow) {  
         printf("\nNew status?");  
         gets_s(szStatus[irow-1], (ROWS * STATUS_LEN));  
         SQLSetPos(hstmtS, irow, SQL_POSITION, SQL_LOCK_NO_CHANGE);  
         SQLPrepare(hstmtU,  
          "UPDATE ORDERS SET STATUS=? WHERE CURRENT OF C1", SQL_NTS);  
         SQLBindParameter(hstmtU, 1, SQL_PARAM_INPUT,  
            SQL_C_CHAR, SQL_CHAR,  
            STATUS_LEN, 0, szStatus[irow], 0, NULL);  
         SQLExecute(hstmtU);  
      } else if (irow == 0) {  
         break;  
      }  
   }  
}  
```  
  
 For more examples, see [Positioned Update and Delete Statements](../../../odbc/reference/develop-app/positioned-update-and-delete-statements.md) and [Updating Rows in the Rowset with SQLSetPos](../../../odbc/reference/develop-app/updating-rows-in-the-rowset-with-sqlsetpos.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Performing bulk operations that do not relate to the block cursor position|[SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Getting a single field of a descriptor|[SQLGetDescField Function](../../../odbc/reference/syntax/sqlgetdescfield-function.md)|  
|Getting multiple fields of a descriptor|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting a single field of a descriptor|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
|Setting multiple fields of a descriptor|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
