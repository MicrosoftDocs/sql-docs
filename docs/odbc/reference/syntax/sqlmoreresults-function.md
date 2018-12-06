---
title: "SQLMoreResults Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLMoreResults"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLMoreResults"
helpviewer_keywords: 
  - "SQLMoreResults function [ODBC]"
ms.assetid: bf169ed5-4d55-412c-b184-12065a726e89
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLMoreResults Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLMoreResults** determines whether more results are available on a statement containing **SELECT**, **UPDATE**, **INSERT**, or **DELETE** statements and, if so, initializes processing for those results.  
  
## Syntax  
  
```  
  
SQLRETURN SQLMoreResults(  
     SQLHSTMT     StatementHandle);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_NO_DATA, SQL_ERROR, SQL_INVALID_HANDLE, OR SQL_PARAM_DATA_AVAILABLE.  
  
## Diagnostics  
 When **SQLMoreResults** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLMoreResults** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value has changed|The value of a statement attribute changed as the batch was being processed. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The **SQLMoreResults** function was called and, before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the **SQLMoreResults** function was called again on the *StatementHandle*.<br /><br /> The **SQLMoreResults** function was called and, before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLMoreResults** function was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SELECT** statements return result sets. **UPDATE**, **INSERT**, and **DELETE** statements return a count of affected rows. If any of these statements are batched, submitted with arrays of parameters (numbered in increasing parameter order, in the order that they appear in the batch), or in procedures, they can return multiple result sets or row counts. For information about batches of statements and arrays of parameters, see [Batches of SQL Statements](../../../odbc/reference/develop-app/batches-of-sql-statements.md) and [Arrays of Parameter Values](../../../odbc/reference/develop-app/arrays-of-parameter-values.md).  
  
 After executing the batch, the application is positioned on the first result set. The application can call **SQLBindCol**, **SQLBulkOperations**, **SQLFetch**, **SQLGetData**, **SQLFetchScroll**, **SQLSetPos**, and all the metadata functions, on the first or any subsequent result sets, just as it would if there were just a single result set. Once it is done with the first result set, the application calls **SQLMoreResults** to move to the next result set. If another result set or count is available, **SQLMoreResults** returns SQL_SUCCESS and initializes the result set or count for additional processing. If any row count-generating statements appear in between result set-generating statements, they can be stepped over by calling **SQLMoreResults**.After calling **SQLMoreResults** for **UPDATE**, **INSERT**, or **DELETE** statements, an application can call **SQLRowCount**.  
  
 If there was a current result set with unfetched rows, **SQLMoreResults** discards that result set and makes the next result set or count available. If all results have been processed, **SQLMoreResults** returns SQL_NO_DATA. For some drivers, output parameters and return values are not available until all result sets and row counts have been processed. For such drivers, output parameters and return values become available when **SQLMoreResults** returns SQL_NO_DATA.  
  
 Any bindings that were established for the previous result set still remain valid. If the column structures are different for this result set, then calling **SQLFetch** or **SQLFetchScroll** may result in an error or truncation. To prevent this, the application has to call **SQLBindCol** to explicitly rebind as appropriate (or do so by setting descriptor fields). Alternatively, the application can call **SQLFreeStmt** with an *Option* of SQL_UNBIND to unbind all the column buffers.  
  
 The values of statement attributes, such as cursor type, cursor concurrency, keyset size, or maximum length, may change as the application navigates through the batch by calls to **SQLMoreResults**. If this happens, **SQLMoreResults** will return SQL_SUCCESS_WITH_INFO and SQLSTATE 01S02 (Option value has changed).  
  
 Calling **SQLCloseCursor**, or **SQLFreeStmt** with an *Option* of SQL_CLOSE, discards all the result sets and row counts that were available as a result of the execution of the batch. The statement handle returns to either the allocated or prepared state. Calling **SQLCancel** to cancel an asynchronously executing function when a batch has been executed and the statement handle is in the executed, cursor-positioned, or asynchronous state results in all the results sets and row counts generated by the batch being discarded if the cancel call was successful. The statement then returns to the prepared or allocated state.  
  
 If a batch of statements or a procedure mixes other SQL statements with **SELECT**, **UPDATE**, **INSERT**, and **DELETE** statements, these other statements do not affect **SQLMoreResults**.  
  
 For more information, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 If a searched update, insert, or delete statement in a batch of statements does not affect any rows at the data source, **SQLMoreResults** returns SQL_SUCCESS. This is different from the case of a searched update, insert, or delete statement that is executed through **SQLExecDirect**, **SQLExecute**, or **SQLParamData**, which returns SQL_NO_DATA if it does not affect any rows at the data source. If an application calls **SQLRowCount** to retrieve the row count after a call to **SQLMoreResults** has not affected any rows, **SQLRowCount** will return SQL_NO_DATA.  
  
 For additional information about the valid sequencing of result-processing functions, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
 For more information about SQL_PARAM_DATA_AVAILABLE and streamed output parameters, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
## Availability of Row Counts  
 When a batch contains multiple consecutive row count-generating statements, it is possible that these row counts are rolled up into just one row count. For example, if a batch has five insert statements, then certain data sources are capable of returning five individual row counts. Certain other data sources return only one row count that represents the sum of the five individual row counts.  
  
 When a batch contains a combination of result set-generating and row count-generating statements, row counts may or may not be available at all. The behavior of the driver with respect to the availability of row counts is enumerated in the SQL_BATCH_ROW_COUNT information type available through a call to **SQLGetInfo**. For example, suppose that the batch contains a **SELECT**, followed by two **INSERT**s and another **SELECT**. Then the following cases are possible:  
  
-   The row counts corresponding to the two **INSERT** statements are not available at all. The first call to **SQLMoreResults** will position you on the result set of the second **SELECT** statement.  
  
-   The row counts corresponding to the two **INSERT** statements are available individually. (A call to **SQLGetInfo** does not return the SQL_BRC_ROLLED_UP bit for the SQL_BATCH_ROW_COUNT information type.) The first call to **SQLMoreResults** will position you on the row count of the first **INSERT**, and the second call will position you on the row count of the second **INSERT**. The third call to **SQLMoreResults** will position you on the result set of the second **SELECT** statement.  
  
-   The row counts corresponding to the two **INSERTs** are rolled up into one single row count that is available. (A call to **SQLGetInfo** returns the SQL_BRC_ROLLED_UP bit for the SQL_BATCH_ROW_COUNT information type.) The first call to **SQLMoreResults** will position you on the rolled-up row count, and the second call to **SQLMoreResults** will position you on the result set of the second **SELECT**.  
  
 Certain drivers make row counts available only for explicit batches and not for stored procedures.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Fetching part or all of a column of data|[SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md)
