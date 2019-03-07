---
title: "SQLExtendedFetch Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLExtendedFetch"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLExtendedFetch"
helpviewer_keywords: 
  - "SQLExtendedFetch function [ODBC]"
ms.assetid: 940b5cf7-581c-4ede-8533-c67d5e9ef488
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLExtendedFetch Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: Deprecated  
  
 **Summary**  
 **SQLExtendedFetch** fetches the specified rowset of data from the result set and returns data for all bound columns. Rowsets can be specified at an absolute or relative position or by bookmark.  
  
> [!NOTE]
>  In ODBC 3*.x*, **SQLExtendedFetch** has been replaced by **SQLFetchScroll**. ODBC 3*.x* applications should not call **SQLExtendedFetch**; instead they should call **SQLFetchScroll**. The Driver Manager maps **SQLFetchScroll** to **SQLExtendedFetch** when working with an ODBC 2*.x* driver. ODBC 3*.x* drivers should support **SQLExtendedFetch** if they want to work with ODBC 2*.x* applications that call it. For more information, see "Comments" and [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
## Syntax  
  
```  
  
SQLRETURN SQLExtendedFetch(  
      SQLHSTMT         StatementHandle,  
      SQLUSMALLINT     FetchOrientation,  
      SQLLEN           FetchOffset,  
      SQLULEN *        RowCountPtr,  
      SQLUSMALLINT *   RowStatusArray);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *FetchOrientation*  
 [Input] Type of fetch. This is the same as *FetchOrientation* in **SQLFetchScroll**.  
  
 *FetchOffset*  
 [Input] Number of the row to fetch. This is the same as *FetchOffset* in **SQLFetchScroll**, with one exception. When *FetchOrientation* is SQL_FETCH_BOOKMARK, *FetchOffset* is a fixed-length bookmark, not an offset from a bookmark. In other words, **SQLExtendedFetch** retrieves the bookmark from this argument, not the SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute. It does not support variable-length bookmarks and does not support fetching a rowset at an offset (other than 0) from a bookmark.  
  
 *RowCountPtr*  
 [Output] Pointer to a buffer in which to return the number of rows actually fetched. This buffer is used in the same manner as the buffer specified by the SQL_ATTR_ROWS_FETCHED_PTR statement attribute. This buffer is used only by **SQLExtendedFetch**. It is not used by **SQLFetch** or **SQLFetchScroll**.  
  
 *RowStatusArray*  
 [Output] Pointer to an array in which to return the status of each row. This array is used in the same manner as the array specified by the SQL_ATTR_ROW_STATUS_PTR statement attribute.  
  
 However, the address of this array is not stored in the SQL_DESC_STATUS_ARRAY_PTR field in the IRD. Furthermore, this array is used only by **SQLExtendedFetch** and by **SQLBulkOperations** with an *Operation* of SQL_ADD or **SQLSetPos** when it is called after **SQLExtendedFetch**. It is not used by **SQLFetch** or **SQLFetchScroll**, and it is not used by **SQLBulkOperations** or **SQLSetPos** when they are called after **SQLFetch** or **SQLFetchScroll**. It is also not used when **SQLBulkOperations** with an *Operation* of SQL_ADD is called before any fetch function is called. In other words, it is used only in statement state S7. It is not used in statement states S5 or S6. For more information, see [Statement Transitions](../../../odbc/reference/appendixes/statement-transitions.md) in Appendix B: ODBC State Transition Tables.  
  
 Applications should provide a valid pointer in the *RowStatusArray* argument; if not, the behavior of **SQLExtendedFetch** and the behavior of calls to **SQLBulkOperations** or **SQLSetPos** after a cursor has been positioned by **SQLExtendedFetch** are undefined.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLExtendedFetch** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLError**. The following table lists the SQLSTATE values commonly returned by **SQLExtendedFetch** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise. If an error occurs on a single column, **SQLGetDiagField** can be called with a *DiagIdentifier* of SQL_DIAG_COLUMN_NUMBER to determine the column the error occurred on; and **SQLGetDiagField** can be called with a *DiagIdentifier* of SQL_DIAG_ROW_NUMBER to determine the row containing that column.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|String or binary data returned for a column resulted in the truncation of nonblank character or non-NULL binary data. If it was a string value, it was right-truncated. If it was a numeric value, the fractional part of the number was truncated.  (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S01|Error in row|An error occurred while fetching one or more rows. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S06|Attempt to fetch before the result set returned the first rowset|The requested rowset overlapped the start of the result set when the current position was beyond the first row, and either *FetchOrientation* was SQL_PRIOR or *FetchOrientation* was SQL_RELATIVE with a negative *FetchOffset* whose absolute value was less than or equal to the current SQL_ROWSET_SIZE. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S07|Fractional truncation|The data returned for a column was truncated. For numeric data types, the fractional part of the number was truncated. For time, timestamp, and interval data types containing a time component, the fractional portion of the time was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|A data value could not be converted to the C data type specified by *TargetType* in **SQLBindCol**.|  
|07009|Invalid descriptor index|Column 0 was bound with **SQLBindCol**, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22002|Indicator variable required but not supplied|NULL data was fetched into a column whose *StrLen_or_IndPtr* set by **SQLBindCol** was a null pointer.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22003|Numeric value out of range|Returning the numeric value (as numeric or string) for one or more columns would have caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> For more information, see [Guidelines for Interval and Numeric Data Types](../../../odbc/reference/appendixes/guidelines-for-interval-and-numeric-data-types.md) in Appendix D: Data Types.|  
|22007|Invalid datetime format|A character column in the result set was bound to a date, time, or timestamp C structure, and a value in the column was, respectively, an invalid date, time, or timestamp.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22012|Division by zero|A value from an arithmetic expression was returned, which resulted in division by zero.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22015|Interval field overflow|Assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> When fetching data to an interval C type, there was no representation of the value of the SQL type in the interval C type.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22018|Invalid character value for cast specification|The C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|24000|Invalid cursor state|The *StatementHandle* was in an executed state, but no result set was associated with the *StatementHandle*.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLError** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLExtendedFetch** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute**, or a catalog function.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) **SQLExtendedFetch** was called for the *StatementHandle* after **SQLFetch** or **SQLFetchScroll** was called and before **SQLFreeStmt** was called with the SQL_CLOSE option.<br /><br /> (DM) **SQLBulkOperations** was called for a statement before **SQLFetch**, **SQLFetchScroll**, or **SQLExtendedFetch** was called, and then **SQLExtendedFetch** was called before **SQLFreeStmt** was called with the SQL_CLOSE option.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY106|Fetch type out of range|(DM) The value specified for the argument *FetchOrientation* was invalid. (See "Comments.")<br /><br /> The argument *FetchOrientation* was SQL_FETCH_BOOKMARK, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.<br /><br /> The value of the SQL_CURSOR_TYPE statement option was SQL_CURSOR_FORWARD_ONLY, and the value of argument *FetchOrientation* was not SQL_FETCH_NEXT.<br /><br /> The argument *FetchOrientation* was SQL_FETCH_RESUME.|  
|HY107|Row value out of range|The value specified with the SQL_CURSOR_TYPE statement option was SQL_CURSOR_KEYSET_DRIVEN, but the value specified with the SQL_KEYSET_SIZE statement attribute was greater than 0 and less than the value specified with the SQL_ROWSET_SIZE statement attribute.|  
|HY111|Invalid bookmark value|The argument *FetchOrientation* was SQL_FETCH_BOOKMARK, and the bookmark specified in the *FetchOffset* argument was not valid.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|Driver or data source does not support the specified fetch type.<br /><br /> The driver or data source does not support the conversion specified by the combination of the *TargetType* in **SQLBindCol** and the SQL data type of the corresponding column. This error applies only when the SQL data type of the column was mapped to a driver-specific SQL data type.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtOption**, SQL_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 The behavior of **SQLExtendedFetch** is identical to that of **SQLFetchScroll**, with the following exceptions:  
  
-   **SQLExtendedFetch** and **SQLFetchScroll** use different methods to return the number of rows fetched. **SQLExtendedFetch** returns the number of rows fetched in *\*RowCountPtr*; **SQLFetchScroll** returns the number of rows fetched directly to the buffer pointed to by SQL_ATTR_ROWS_FETCHED_PTR. For more information, see the *RowCountPtr* argument.  
  
-   **SQLExtendedFetch** and **SQLFetchScroll** return the status of each row in different arrays. For more information, see the *RowStatusArray* argument.  
  
-   **SQLExtendedFetch** and **SQLFetchScroll** use different methods to retrieve the bookmark when *FetchOrientation* is SQL_FETCH_BOOKMARK. **SQLExtendedFetch** does not support variable-length bookmarks or fetching rowsets at an offset other than 0 from a bookmark. For more information, see the *FetchOffset* argument.  
  
-   **SQLExtendedFetch** and **SQLFetchScroll** use different rowset sizes. **SQLExtendedFetch** uses the value of the SQL_ROWSET_SIZE statement attribute, and **SQLFetchScroll** uses the value of the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.  
  
-   **SQLExtendedFetch** has slightly different error handling semantics than **SQLFetchScroll**. For more information, see "Error Handling" in the "Comments" section of [SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md).  
  
-   **SQLExtendedFetch** does not support binding offsets (the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute).  
  
-   Calls to **SQLExtendedFetch** cannot be mixed with calls to **SQLFetch** or **SQLFetchScroll**, and if **SQLBulkOperations** is called before any fetch function is called, **SQLExtendedFetch** cannot be called until the cursor is closed and reopened. That is, **SQLExtendedFetch** can be called only in statement state S7. For more information, see [Statement Transitions](../../../odbc/reference/appendixes/statement-transitions.md) in Appendix B: ODBC State Transition Tables.  
  
 When an application calls **SQLFetchScroll** while using an ODBC 2*.x* driver, the Driver Manager maps this call to **SQLExtendedFetch**. For more information, see "SQLFetchScroll and ODBC 2*.x* Drivers" in [SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md).  
  
 In ODBC 2*.x*, **SQLExtendedFetch** was called to fetch multiple rows and **SQLFetch** was called to fetch a single row. In ODBC 3*.x*, on the other hand, **SQLFetch** can be called to fetch multiple rows.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Performing bulk insert, update, or delete operations|[SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Returning the number of result set columns|[SQLNumResultCols Function](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|  
|Positioning the cursor, refreshing data in the rowset, or updating or deleting data in the result set|[SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
