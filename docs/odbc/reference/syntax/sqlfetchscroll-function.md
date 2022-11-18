---
description: "SQLFetchScroll Function"
title: "SQLFetchScroll Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLFetchScroll"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLFetchScroll"
helpviewer_keywords: 
  - "SQLFetchScroll function [ODBC]"
ms.assetid: c0243667-428c-4dda-ae91-3c307616a1ac
author: David-Engel
ms.author: v-davidengel
---
# SQLFetchScroll Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLFetchScroll** fetches the specified rowset of data from the result set and returns data for all bound columns. Rowsets can be specified at an absolute or relative position or by bookmark.  
  
 When working with an ODBC 2.x driver, the Driver Manager maps this function to **SQLExtendedFetch**. For more information, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLFetchScroll(  
      SQLHSTMT      StatementHandle,  
      SQLSMALLINT   FetchOrientation,  
      SQLLEN        FetchOffset);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *FetchOrientation*  
 [Input]  
  
 Type of fetch:  
  
 SQL_FETCH_NEXT  
  
 SQL_FETCH_PRIOR  
  
 SQL_FETCH_FIRST  
  
 SQL_FETCH_LAST  
  
 SQL_FETCH_ABSOLUTE  
  
 SQL_FETCH_RELATIVE  
  
 SQL_FETCH_BOOKMARK  
  
 For more information, see "Positioning the Cursor" in the "Comments" section.  
  
 *FetchOffset*  
 [Input]  
  
 Number of the row to fetch. The interpretation of this argument depends on the value of the *FetchOrientation* argument. For more information, see "Positioning the Cursor" in the "Comments" section.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLFetchScroll** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a HandleType of SQL_HANDLE_STMT and a Handle of StatementHandle. The following table lists the SQLSTATE values commonly returned by **SQLFetchScroll** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise. If an error occurs on a single column, **SQLGetDiagField** can be called with a DiagIdentifier of SQL_DIAG_COLUMN_NUMBER to determine the column the error occurred on; and **SQLGetDiagField** can be called with a DiagIdentifier of SQL_DIAG_ROW_NUMBER to determine the row containing that column.  
  
 For all those SQLSTATEs that can return SQL_SUCCESS_WITH_INFO or SQL_ERROR (except 01xxx SQLSTATEs), SQL_SUCCESS_WITH_INFO is returned if an error occurs on one or more, but not all, rows of a multirow operation, and SQL_ERROR is returned if an error occurs on a single-row operation.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|String or binary data returned for a column resulted in the truncation of nonblank character or non-NULL binary data. If it was a string value, it was right-truncated.|  
|01S01|Error in row|An error occurred while fetching one or more rows.<br /><br /> (If this SQLSTATE is returned when an ODBC 3*.x* application is working with an ODBC 2*.x* driver, it can be ignored.)|  
|01S06|Attempt to fetch before the result set returned the first rowset|The requested rowset overlapped the start of the result set when FetchOrientation was SQL_FETCH_PRIOR, the current position was beyond the first row, and the number of the current row is less than or equal to the rowset size.<br /><br /> The requested rowset overlapped the start of the result set when FetchOrientation was SQL_FETCH_PRIOR, the current position was beyond the end of the result set, and the rowset size was greater than the result set size.<br /><br /> The requested rowset overlapped the start of the result set when FetchOrientation was SQL_FETCH_RELATIVE, FetchOffset was negative, and the absolute value of FetchOffset was less than or equal to the rowset size.<br /><br /> The requested rowset overlapped the start of the result set when FetchOrientation was SQL_FETCH_ABSOLUTE, FetchOffset was negative, and the absolute value of FetchOffset was greater than the result set size but less than or equal to the rowset size.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S07|Fractional truncation|The data returned for a column was truncated. For numeric data types, the fractional part of the number was truncated. For time, timestamp, and interval data types containing a time component, the fractional portion of the time was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data value of a column in the result set could not be converted to the data type specified by *TargetType* in **SQLBindCol**.<br /><br /> Column 0 was bound with a data type of SQL_C_BOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE.<br /><br /> Column 0 was bound with a data type of SQL_C_VARBOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was not set to SQL_UB_VARIABLE.|  
|07009|Invalid descriptor index|The driver was an ODBC 2*.x* driver that does not support **SQLExtendedFetch**, and a column number specified in the binding for a column was 0.<br /><br /> Column 0 was bound, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22001|String data, right truncated|A variable-length bookmark returned for a column was truncated.|  
|22002|Indicator variable required but not supplied|NULL data was fetched into a column whose *StrLen_or_IndPtr* set by **SQLBindCol** (or SQL_DESC_INDICATOR_PTR set by **SQLSetDescField** or **SQLSetDescRec**) was a null pointer.|  
|22003|Numeric value out of range|Returning the numeric value (as numeric or string) for one or more bound columns would have caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> For more information, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md) in [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).|  
|22007|Invalid datetime format|A character column in the result set was bound to a date, time, or timestamp C structure, and a value in the column was, respectively, an invalid date, time, or timestamp.|  
|22012|Division by zero|A value from an arithmetic expression was returned, which resulted in division by zero.|  
|22015|Interval field overflow|Assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> When fetching data to an interval C type, there was no representation of the value of the SQL type in the interval C type.|  
|22018|Invalid character value for cast specification|A character column in the result set was bound to a character C buffer, and the column contained a character for which there was no representation in the character set of the buffer.<br /><br /> The C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.|  
|24000|Invalid cursor state|The *StatementHandle* was in an executed state but no result set was associated with the *StatementHandle*.|  
|40001|Serialization failure|The transaction in which the fetch was executed was terminated to prevent deadlock.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLFetchScroll** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute** or a catalog function.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) **SQLFetch** was called for the *StatementHandle* after **SQLExtendedFetch** was called and before **SQLFreeStmt** with the SQL_CLOSE option was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|The SQL_ATTR_USE_BOOKMARK statement attribute was set to SQL_UB_VARIABLE, and column 0 was bound to a buffer whose length was not equal to the maximum length for the bookmark for this result set. (This length is available in the SQL_DESC_OCTET_LENGTH field of the IRD and can be obtained by calling **SQLDescribeCol**, **SQLColAttribute**, or **SQLGetDescField**.)|  
|HY106|Fetch type out of range|DM) The value specified for the argument FetchOrientation was invalid.<br /><br /> (DM) The argument FetchOrientation was SQL_FETCH_BOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.<br /><br /> The value of the SQL_ATTR_CURSOR_TYPE statement attribute was SQL_CURSOR_FORWARD_ONLY, and the value of argument FetchOrientation was not SQL_FETCH_NEXT.<br /><br /> The value of the SQL_ATTR_CURSOR_SCROLLABLE statement attribute was SQL_NONSCROLLABLE, and the value of argument FetchOrientation was not SQL_FETCH_NEXT.|  
|HY107|Row value out of range|The value specified with the SQL_ATTR_CURSOR_TYPE statement attribute was SQL_CURSOR_KEYSET_DRIVEN, but the value specified with the SQL_ATTR_KEYSET_SIZE statement attribute was greater than 0 and less than the value specified with the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.|  
|HY111|Invalid bookmark value|The argument FetchOrientation was SQL_FETCH_BOOKMARK, and the bookmark pointed to by the value in the SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute was not valid or was a null pointer.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the conversion specified by the combination of the *TargetType* in **SQLBindCol** and the SQL data type of the corresponding column.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the requested result set. The timeout period is set through SQLSetStmtAttr, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLFetchScroll** returns a specified rowset from the result set. Rowsets can be specified by absolute or relative position or by bookmark. **SQLFetchScroll** can be called only while a result set exists - that is, after a call that creates a result set and before the cursor over that result set is closed. If any columns are bound, it returns the data in those columns. If the application has specified a pointer to a row status array or a buffer in which to return the number of rows fetched, **SQLFetchScroll** returns this information as well. Calls to **SQLFetchScroll** can be mixed with calls to **SQLFetch** but cannot be mixed with calls to **SQLExtendedFetch**.  
  
 For more information, see [Using Block Cursors](../../../odbc/reference/develop-app/using-block-cursors.md) and [Using Scrollable Cursors](../../../odbc/reference/develop-app/using-scrollable-cursors.md).  
  
## Positioning the Cursor  
 When the result set is created, the cursor is positioned before the start of the result set. **SQLFetchScroll** positions the block cursor based on the values of the *FetchOrientation* and *FetchOffset* arguments as shown in the following table. The exact rules for determining the start of the new rowset are shown in the next section.  
  
|FetchOrientation|Meaning|  
|----------------------|-------------|  
|SQL_FETCH_NEXT|Return the next rowset. This is equivalent to calling **SQLFetch**.<br /><br /> **SQLFetchScroll** ignores the value of *FetchOffset*.|  
|SQL_FETCH_PRIOR|Return the prior rowset.<br /><br /> **SQLFetchScroll** ignores the value of *FetchOffset*.|  
|SQL_FETCH_RELATIVE|Return the rowset *FetchOffset* from the start of the current rowset.|  
|SQL_FETCH_ABSOLUTE|Return the rowset starting at row *FetchOffset*.|  
|SQL_FETCH_FIRST|Return the first rowset in the result set.<br /><br /> **SQLFetchScroll** ignores the value of *FetchOffset*.|  
|SQL_FETCH_LAST|Return the last complete rowset in the result set.<br /><br /> **SQLFetchScroll** ignores the value of *FetchOffset*.|  
|SQL_FETCH_BOOKMARK|Return the rowset FetchOffset rows from the bookmark specified by the SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute.|  
  
 Drivers are not required to support all fetch orientations; an application calls **SQLGetInfo** with an information type of SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 (depending on the type of the cursor) to determine which fetch orientations are supported by the driver. The application should look at the SQL_CA1_NEXT, SQL_CA1_RELATIVE, SQL_CA1_ABSOLUTE, and WQL_CA1_BOOKMARK bitmasks in these information types. Furthermore, if the cursor is forward-only and FetchOrientation is not SQL_FETCH_NEXT, **SQLFetchScroll** returns SQLSTATE HY106 (Fetch type out of range).  
  
 The SQL_ATTR_ROW_ARRAY_SIZE statement attribute specifies the number of rows in the rowset. If the rowset being fetched by **SQLFetchScroll** overlaps the end of the result set, **SQLFetchScroll** returns a partial rowset. That is, if S + R - 1 is greater than L, where S is the starting row of the rowset being fetched, R is the rowset size, and L is the last row in the result set, then only the first L - S + 1 rows of the rowset are valid. The remaining rows are empty and have a status of SQL_ROW_NOROW.  
  
 After **SQLFetchScroll** returns, the current row is the first row of the rowset.  
  
## Cursor Positioning Rules  
 The following sections describe the exact rules for each value of FetchOrientation. These rules use the following notation.  
  
|Notation|Meaning|  
|--------------|-------------|  
|*Before start*|The block cursor is positioned before the start of the result set. If the first row of the new rowset is before the start of the result set, **SQLFetchScroll** returns SQL_NO_DATA.|  
|*After end*|The block cursor is positioned after the end of the result set. If the first row of the new rowset is after the end of the result set, **SQLFetchScroll** returns SQL_NO_DATA.|  
|*CurrRowsetStart*|The number of the first row in the current rowset.|  
|*LastResultRow*|The number of the last row in the result set.|  
|*RowsetSize*|The rowset size.|  
|*FetchOffset*|The value of the *FetchOffset* argument.|  
|*BookmarkRow*|The row corresponding to the bookmark specified by the SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute.|  
  
## SQL_FETCH_NEXT  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*Before start*|1|  
|*CurrRowsetStart + RowsetSize*[1] *\<= LastResultRow*|*CurrRowsetStart + RowsetSize*[1]|  
|*CurrRowsetStart + RowsetSize*[1]*> LastResultRow*|*After end*|  
|*After end*|*After end*|  
  
 [1]   If the rowset size has been changed since the previous call to fetch rows, this is the rowset size that was used with the previous call.  
  
## SQL_FETCH_PRIOR  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*Before start*|*Before start*|  
|*CurrRowsetStart = 1*|*Before start*|  
|*1 < CurrRowsetStart <= RowsetSize* <sup>[2]</sup>|*1* <sup>[1]</sup>|  
|*CurrRowsetStart > RowsetSize* <sup>[2]</sup>|*CurrRowsetStart - RowsetSize* <sup>[2]</sup>|  
|*After end AND LastResultRow < RowsetSize* <sup>[2]</sup>|*1* <sup>[1]</sup>|  
|*After end AND LastResultRow >= RowsetSize* <sup>[2]</sup>|*LastResultRow - RowsetSize + 1* <sup>[2]</sup>|  
  
 [1]   **SQLFetchScroll** returns SQLSTATE 01S06 (Attempt to fetch before the result set returned the first rowset) and SQL_SUCCESS_WITH_INFO.  
  
 [2]   If the rowset size has been changed since the previous call to fetch rows, this is the new rowset size.  
  
## SQL_FETCH_RELATIVE  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*(Before start AND FetchOffset > 0) OR (After end AND FetchOffset < 0)*|*--* <sup>[1]</sup>|  
|*BeforeStart AND FetchOffset <= 0*|*Before start*|  
|*CurrRowsetStart = 1 AND FetchOffset < 0*|*Before start*|  
|*CurrRowsetStart > 1 AND CurrRowsetStart + FetchOffset < 1 AND &#124; FetchOffset &#124; > RowsetSize* <sup>[3]</sup>|*Before start*|  
|*CurrRowsetStart > 1 AND CurrRowsetStart + FetchOffset < 1 AND &#124; FetchOffset &#124; <= RowsetSize* <sup>[3]</sup>|*1* <sup>[2]</sup>|  
|*1 <= CurrRowsetStart + FetchOffset \<= LastResultRow*|*CurrRowsetStart + FetchOffset*|  
|*CurrRowsetStart + FetchOffset > LastResultRow*|*After end*|  
|*After end AND FetchOffset >= 0*|*After end*|  
  
 [1]   ***SQLFetchScroll*** returns the same rowset as if it was called with FetchOrientation set to SQL_FETCH_ABSOLUTE. For more information, see the "SQL_FETCH_ABSOLUTE" section.  
  
 [2]   **SQLFetchScroll** returns SQLSTATE 01S06 (Attempt to fetch before the result set returned the first rowset) and SQL_SUCCESS_WITH_INFO.  
  
 [3]   If the rowset size has been changed since the previous call to fetch rows, this is the new rowset size.  
  
## SQL_FETCH_ABSOLUTE  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*FetchOffset < 0 AND &#124; FetchOffset &#124; <= LastResultRow*|*LastResultRow + FetchOffset + 1*|  
|*FetchOffset < 0 AND &#124; FetchOffset &#124; > LastResultRow AND &#124; FetchOffset &#124; > RowsetSize* <sup>[2]</sup>|*Before start*|  
|*FetchOffset < 0 AND &#124; FetchOffset &#124; > LastResultRow AND &#124; FetchOffset &#124; <= RowsetSize* <sup>[2]</sup>|*1* <sup>[1]</sup>|  
|*FetchOffset = 0*|*Before start*|  
|*1 <= FetchOffset \<= LastResultRow*|*FetchOffset*|  
|*FetchOffset > LastResultRow*|*After end*|  
  
 [1]   **SQLFetchScroll** returns SQLSTATE 01S06 (Attempt to fetch before the result set returned the first rowset) and SQL_SUCCESS_WITH_INFO.  
  
 [2]   If the rowset size has been changed since the previous call to fetch rows, this is the new rowset size.  
  
 An absolute fetch performed against a dynamic cursor cannot provide the required result because row positions in a dynamic cursor are undetermined. Such an operation is equivalent to a fetch first followed by a fetch relative; it is not an atomic operation, as is an absolute fetch on a static cursor.  
  
## SQL_FETCH_FIRST  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*Any*|*1*|  
  
## SQL_FETCH_LAST  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*RowsetSize* <sup>[1]</sup> <= LastResultRow|*LastResultRow - RowsetSize + 1* <sup>[1]</sup>|  
|*RowsetSize* <sup>[1]</sup> > LastResultRow|*1*|  
  
 [1]   If the rowset size has been changed since the previous call to fetch rows, this is the new rowset size.  
  
## SQL_FETCH_BOOKMARK  
 The following rules apply.  
  
|Condition|First row of new rowset|  
|---------------|-----------------------------|  
|*BookmarkRow + FetchOffset < 1*|*Before start*|  
|*1 <= BookmarkRow + FetchOffset \<= LastResultRow*|*BookmarkRow + FetchOffset*|  
|*BookmarkRow + FetchOffset > LastResultRow*|*After end*|  
  
 For information about bookmarks, see [Bookmarks (ODBC)](../../../odbc/reference/develop-app/bookmarks-odbc.md).  
  
## Effect of Deleted, Added, and Error Rows on Cursor Movement  
 Static and keyset-driven cursors sometimes detect rows added to the result set and remove rows deleted from the result set. By calling **SQLGetInfo** with the SQL_STATIC_CURSOR_ATTRIBUTES2 and SQL_KEYSET_CURSOR_ATTRIBUTES2 options and looking at the SQL_CA2_SENSITIVITY_ADDITIONS, SQL_CA2_SENSITIVITY_DELETIONS, and SQL_CA2_SENSITIVITY_UPDATES bitmasks, an application determines whether the cursors implemented by a particular driver do this. For drivers that can detect deleted rows and remove them, the following paragraphs describe the effects of this behavior. For drivers that can detect deleted rows but cannot remove them, deletions have no effect on cursor movements, and the following paragraphs do not apply.  
  
 If the cursor detects rows added to the result set or removes rows deleted from the result set, it appears as if it detects these changes only when it fetches data. This includes the case when **SQLFetchScroll** is called with FetchOrientation set to SQL_FETCH_RELATIVE and FetchOffset set to 0 to refetch the same rowset, but does not include the case when SQLSetPos is called with fOption set to SQL_REFRESH. In the latter case, the data in the rowset buffers is refreshed, but not refetched, and deleted rows are not removed from the result set. Thus, when a row is deleted from or inserted into the current rowset, the cursor does not modify the rowset buffers. Instead, it detects the change when it fetches any rowset that previously included the deleted row or now includes the inserted row.  
  
 For example:  
  
```cpp  
// Fetch the next rowset.  
SQLFetchScroll(hstmt, SQL_FETCH_NEXT, 0);  
// Delete third row of the rowset. Does not modify the rowset buffers.  
SQLSetPos(hstmt, 3, SQL_DELETE, SQL_LOCK_NO_CHANGE);  
// The third row has a status of SQL_ROW_DELETED after this call.  
SQLSetPos(hstmt, 3, SQL_REFRESH, SQL_LOCK_NO_CHANGE);  
// Refetch the same rowset. The third row is removed, replaced by what  
// was previously the fourth row.  
SQLFetchScroll(hstmt, SQL_FETCH_RELATIVE, 0);  
```  
  
 When **SQLFetchScroll** returns a new rowset that has a position relative to the current rowset - that is, FetchOrientation is SQL_FETCH_NEXT, SQL_FETCH_PRIOR, or SQL_FETCH_RELATIVE - it does not include changes to the current rowset when calculating the starting position of the new rowset. However, it does include changes outside the current rowset if it is capable of detecting them. Furthermore, when **SQLFetchScroll** returns a new rowset that has a position independent of the current rowset - that is, FetchOrientation is SQL_FETCH_FIRST, SQL_FETCH_LAST, SQL_FETCH_ABSOLUTE, or SQL_FETCH_BOOKMARK - it includes all changes it is capable of detecting, even if they are in the current rowset.  
  
 When determining whether newly added rows are inside or outside the current rowset, a partial rowset is considered to end at the last valid row; that is, the last row for which the row status is not SQL_ROW_NOROW. For example, suppose the cursor is capable of detecting newly added rows, the current rowset is a partial rowset, the application adds new rows, and the cursor adds these rows to the end of the result set. If the application calls **SQLFetchScroll** with FetchOrientation set to SQL_FETCH_NEXT, **SQLFetchScroll** returns the rowset starting with the first newly added row.  
  
 For example, suppose the current rowset comprises rows 21 to 30, the rowset size is 10, the cursor removes rows deleted from the result set, and the cursor detects rows added to the result set. The following table shows the rows **SQLFetchScroll** returns in various situations.  
  
|Change|Fetch type|FetchOffset|New rowset[1]|  
|------------|----------------|-----------------|---------------------|  
|Delete row 21|NEXT|0|31 to 40|  
|Delete row 31|NEXT|0|32 to 41|  
|Insert row between rows 21 and 22|NEXT|0|31 to 40|  
|Insert row between rows 30 and 31|NEXT|0|Inserted row, 31 to 39|  
|Delete row 21|PRIOR|0|11 to 20|  
|Delete row 20|PRIOR|0|10 to 19|  
|Insert row between rows 21 and 22|PRIOR|0|11 to 20|  
|Insert row between rows 20 and 21|PRIOR|0|12 to 20, inserted row|  
|Delete row 21|RELATIVE|0|22 to 31<sup>[2]</sup>|  
|Delete row 21|RELATIVE|1|22 to 31|  
|Insert row between rows 21 and 22|RELATIVE|0|21, inserted row, 22 to 29|  
|Insert row between rows 21 and 22|RELATIVE|1|22 to 31|  
|Delete row 21|ABSOLUTE|21|22 to 31<sup>[2]</sup>|  
|Delete row 22|ABSOLUTE|21|21, 23 to 31|  
|Insert row between rows 21 and 22|ABSOLUTE|22|Inserted row, 22 to 29|  
  
 [1]   This column uses the row numbers before any rows were inserted or deleted.  
  
 [2]   In this case, the cursor attempts to return rows starting with row 21. Because row 21 has been deleted, the first row it returns is row 22.  
  
 Error rows (that is, rows with a status of SQL_ROW_ERROR) do not affect cursor movement. For example, if the current rowset starts with row 11 and the status of row 11 is SQL_ROW_ERROR, calling **SQLFetchScroll** with FetchOrientation set to SQL_FETCH_RELATIVE and FetchOffset set to 5 returns the rowset starting with row 16, just as it would if the status for row 11 was SQL_SUCCESS.  
  
## Returning Data in Bound Columns  
 **SQLFetchScroll** returns data in bound columns in the same way as **SQLFetch**. For more information, see "Returning Data in Bound Columns" in [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md).  
  
 If no columns are bound, **SQLFetchScroll** does not return data but does move the block cursor to the specified position. Whether data can be retrieved from unbound columns of a block cursor with **SQLGetData** depends on the driver. This capability is supported if a call to **SQLGetInfo** returns the SQL_GD_BLOCK bit for the SQL_GETDATA_EXTENSIONS information type.  
  
## Buffer Addresses  
 **SQLFetchScroll** uses the same formula to determine the address of data and length/indicator buffers as **SQLFetch**. For more information, see "Buffer Addresses" in [SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
## Row Status Array  
 **SQLFetchScroll** sets values in the row status array in the same manner as SQLFetch. For more information, see "Row Status Array" in [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md).  
  
## Rows Fetched Buffer  
 **SQLFetchScroll** returns the number of rows fetched in the rows fetched buffer in the same manner as **SQLFetch**. For more information, see "Rows Fetched Buffer" in [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md).  
  
## Error Handling  
 When an application calls **SQLFetchScroll** in an ODBC 3.x driver, the Driver Manager calls **SQLFetchScroll** in the driver. When an application calls **SQLFetchScroll** in an ODBC 2.x driver, the Driver Manager calls SQLExtendedFetch in the driver. Because **SQLFetchScroll** and SQLExtendedFetch handle errors in a slightly different manner, the application sees slightly different error behavior when it calls **SQLFetchScroll** in ODBC 2.x and ODBC 3.x drivers.  
  
 **SQLFetchScroll** returns errors and warnings in the same manner as **SQLFetch**; for more information, see "Error Handling" in **SQLFetch**. **SQLExtendedFetch** returns errors in the same manner as **SQLFetch**, with the following exceptions:  
  
 When a warning occurs that applies to a particular row in the rowset, SQLExtendedFetch sets the corresponding entry in the row status array to SQL_ROW_SUCCESS, not SQL_ROW_SUCCESS_WITH_INFO.  
  
 If errors occur in every row in the rowset, SQLExtendedFetch returns SQL_SUCCESS_WITH_INFO, not SQL_ERROR.  
  
 In each group of status records that applies to an individual row, the first status record returned by SQLExtendedFetch must contain SQLSTATE 01S01 (Error in row); **SQLFetchScroll** does not return this SQLSTATE. If SQLExtendedFetch is unable to return additional SQLSTATEs, it still must return this SQLSTATE.  
  
## SQLFetchScroll and Optimistic Concurrency  
 If a cursor uses optimistic concurrency - that is, the SQL_ATTR_CONCURRENCY statement attribute has a value of SQL_CONCUR_VALUES or SQL_CONCUR_ROWVER - **SQLFetchScroll** updates the optimistic concurrency values used by the data source to detect whether a row has changed. This happens whenever **SQLFetchScroll** fetches a new rowset, including when it refetches the current rowset. (It is called with FetchOrientation set to SQL_FETCH_RELATIVE and FetchOffset set to 0.)  
  
## SQLFetchScroll and ODBC 2.x Drivers  
 When an application calls **SQLFetchScroll** in an ODBC 2.x driver, the Driver Manager maps this call to **SQLExtendedFetch**. It passes the following values for the arguments of **SQLExtendedFetch**.  
  
|SQLExtendedFetch argument|Value|  
|-------------------------------|-----------|  
|StatementHandle|StatementHandle in **SQLFetchScroll**.|  
|FetchOrientation|FetchOrientation in **SQLFetchScroll**.|  
|FetchOffset|If FetchOrientation is not SQL_FETCH_BOOKMARK, the value of the FetchOffset argument in **SQLFetchScroll** is used.<br /><br /> If FetchOrientation is SQL_FETCH_BOOKMARK, the value stored at the address specified by the SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute is used.|  
|RowCountPtr|The address specified by the SQL_ATTR_ROWS_FETCHED_PTR statement attribute.|  
|RowStatusArray|The address specified by the SQL_ATTR_ROW_STATUS_PTR statement attribute.|  
  
 For more information, see [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
## Descriptors and SQLFetchScroll  
 **SQLFetchScroll** interacts with descriptors in the same manner as **SQLFetch**. For more information, see the "Descriptors and SQLFetchScroll" section in [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md).  
  
## Code Example  
 See [Column-Wise Binding](../../../odbc/reference/develop-app/column-wise-binding.md), [Row-Wise Binding](../../../odbc/reference/develop-app/row-wise-binding.md), [Positioned Update and Delete Statements](../../../odbc/reference/develop-app/positioned-update-and-delete-statements.md), and [Updating Rows in the Rowset with SQLSetPos](../../../odbc/reference/develop-app/updating-rows-in-the-rowset-with-sqlsetpos.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Performing bulk insert, update, or delete operations|[SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Closing the cursor on the statement|[SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
|Returning the number of result set columns|[SQLNumResultCols Function](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|  
|Positioning the cursor, refreshing data in the rowset, or updating or deleting data in the result set|[SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
