---
description: "SQLParamData Function"
title: "SQLParamData Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLParamData"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLParamData"
helpviewer_keywords: 
  - "SQLParamData function [ODBC]"
ms.assetid: 68fe010d-9539-4e5b-a260-c8d32423b1db
author: David-Engel
ms.author: v-davidengel
---
# SQLParamData Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLParamData** is used together with **SQLPutData** to supply parameter data at statement execution time, and with **SQLGetData** to retrieve streamed output parameter data.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLParamData(  
     SQLHSTMT       StatementHandle,  
     SQLPOINTER *   ValuePtrPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *ValuePtrPtr*  
 [Output] Pointer to a buffer in which to return the address of the *ParameterValuePtr* buffer specified in **SQLBindParameter** (for parameter data) or the address of the *TargetValuePtr* buffer specified in **SQLBindCol** (for column data), as contained in the SQL_DESC_DATA_PTR descriptor record field.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NEED_DATA, SQL_NO_DATA, SQL_STILL_EXECUTING, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_PARAM_DATA_AVAILABLE.  
  
## Diagnostics  
 When **SQLParamData** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLParamData** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data value identified by the *ValueType* argument in **SQLBindParameter** for the bound parameter could not be converted to the data type identified by the *ParameterType* argument in **SQLBindParameter**.<br /><br /> The data value returned for a parameter bound as SQL_PARAM_INPUT_OUTPUT or SQL_PARAM_OUTPUT could not be converted to the data type identified by the *ValueType* argument in **SQLBindParameter**.<br /><br /> (If the data values for one or more rows could not be converted, but one or more rows were successfully returned, this function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22026|String data, length mismatch|The SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y", and less data was sent for a long parameter (the data type was SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type) than was specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**.<br /><br /> The SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y", and less data was sent for a long column (the data type was SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type) than was specified in the length buffer corresponding to a column in a row of data that was added or updated with **SQLBulkOperations** or updated with **SQLSetPos**.|  
|40001|Serialization failure|The transaction was rolled back because of a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*; the function was then called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) The previous function call was not a call to **SQLExecDirect**, **SQLExecute**, **SQLBulkOperations**, or **SQLSetPos** where the return code was SQL_NEED_DATA, or the previous function call was a call to **SQLPutData**.<br /><br /> The previous function call was a call to **SQLParamData**.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLParamData** function was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. **SQLCancel** was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver that corresponds to the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
 If **SQLParamData** is called while sending data for a parameter in an SQL statement, it can return any SQLSTATE that can be returned by the function called to execute the statement (**SQLExecute** or **SQLExecDirect**). If it is called while sending data for a column being updated or added with **SQLBulkOperations** or being updated with **SQLSetPos**, it can return any SQLSTATE that can be returned by **SQLBulkOperations** or **SQLSetPos**.  
  
## Comments  
 **SQLParamData** can be called to supply data-at-execution data for two uses: parameter data that will be used in a call to **SQLExecute** or **SQLExecDirect**, or column data that will be used when a row is updated or added by a call to **SQLBulkOperations** or updated by a call to **SQLSetPos**. At execution time, **SQLParamData** returns to the application an indicator of which data the driver requires.  
  
 When an application calls **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos**, the driver returns SQL_NEED_DATA if it needs data-at-execution data. An application then calls **SQLParamData** to determine which data to send. If the driver requires parameter data, the driver returns in the *\*ValuePtrPtr* output buffer the value that the application put in the rowset buffer. The application can use this value to determine which parameter data the driver is requesting. If the driver requires column data, the driver returns in the *\*ValuePtrPtr* buffer the address that the column was originally bound to, as follows:  
  
 *Bound Address* + *Binding Offset* + ((*Row Number* - 1) x *Element Size*)  
  
 where the variables are defined as indicated in the following table.  
  
|Variable|Description|  
|--------------|-----------------|  
|*Bound Address*|The address specified with the *TargetValuePtr* argument in **SQLBindCol**.|  
|*Binding Offset*|The value stored at the address specified with the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute.|  
|*Row Number*|The 1-based number of the row in the rowset. For single-row fetches, which are the default, this is 1.|  
|*Element Size*|The value of the SQL_ATTR_ROW_BIND_TYPE statement attribute for both data and length/indicator buffers.|  
  
 It also returns SQL_NEED_DATA, which is an indicator to the application that it should call **SQLPutData** to send the data.  
  
 The application calls **SQLPutData** as many times as necessary to send the data-at-execution data for the column or parameter. After all the data has been sent for the column or parameter, the application calls **SQLParamData** again. If **SQLParamData** again returns SQL_NEED_DATA, data must be sent for another parameter or column. Therefore, the application again calls **SQLPutData**. If all data-at-execution data has been sent for all parameters or columns, then **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, the value in *\*ValuePtrPtr* is undefined, and the SQL statement can be executed or the **SQLBulkOperations** or **SQLSetPos** call can be processed.  
  
 If **SQLParamData** supplies parameter data for a searched update or delete statement that does not affect any rows at the data source, the call to **SQLParamData** returns SQL_NO_DATA.  
  
 For more information about how data-at-execution parameter data is passed at statement execution time, see "Passing Parameter Values" in [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md) and [Sending Long Data](../../../odbc/reference/develop-app/sending-long-data.md). For more information about how data-at-execution column data is updated or added, see the section "Using SQLSetPos" in [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md), "Performing Bulk Updates Using Bookmarks" in [SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md), and [Long Data and SQLSetPos and SQLBulkOperations](../../../odbc/reference/develop-app/long-data-and-sqlsetpos-and-sqlbulkoperations.md).  
  
 **SQLParamData** can be called to retrieve streamed output parameters. When **SQLMoreResults**, **SQLExecute**, **SQLGetData**, or **SQLExecDirect** returns SQL_PARAM_DATA_AVAILABLE, call **SQLParamData** to determine which parameter has a value available. For more information about SQL_PARAM_DATA_AVAILABLE and streamed output parameters, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
## Code Example  
 See [SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a parameter in a statement|[SQLDescribeParam Function](../../../odbc/reference/syntax/sqldescribeparam-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Sending parameter data at execution time|[SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md)
