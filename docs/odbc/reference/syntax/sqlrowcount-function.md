---
title: "SQLRowCount Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLRowCount"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRowCount"
helpviewer_keywords: 
  - "SQLRowCount function [ODBC]"
ms.assetid: 61e00a8a-9b3b-45b9-b397-7fe818822416
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRowCount Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLRowCount** returns the number of rows affected by an **UPDATE**, **INSERT**, or **DELETE** statement; an SQL_ADD, SQL_UPDATE_BY_BOOKMARK, or SQL_DELETE_BY_BOOKMARK operation in **SQLBulkOperations**; or an SQL_UPDATE or SQL_DELETE operation in **SQLSetPos**.  
  
## Syntax  
  
```  
  
SQLRETURN SQLRowCount(  
      SQLHSTMT   StatementHandle,  
      SQLLEN *   RowCountPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *RowCountPtr*  
 [Output] Points to a buffer in which to return a row count. For **UPDATE**, **INSERT**, and **DELETE** statements, for the SQL_ADD, SQL_UPDATE_BY_BOOKMARK, and SQL_DELETE_BY_BOOKMARK operations in **SQLBulkOperations**, and for the SQL_UPDATE or SQL_DELETE operations in **SQLSetPos**, the value returned in **RowCountPtr* is either the number of rows affected by the request or -1 if the number of affected rows is not available.  
  
 When **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, **SQLSetPos, or SQLMoreResults** is called, the SQL_DIAG_ROW_COUNT field of the diagnostic data structure is set to the row count, and the row count is cached in an implementation-dependent way. **SQLRowCount** returns the cached row count value. The cached row count value is valid until the statement handle is set back to the prepared or allocated state, the statement is reexecuted, or **SQLCloseCursor** is called. Note that if a function has been called since the SQL_DIAG_ROW_COUNT field was set, the value returned by **SQLRowCount** might be different from the value in the SQL_DIAG_ROW_COUNT field because the SQL_DIAG_ROW_COUNT field is reset to 0 by any function call.  
  
 For other statements and functions, the driver may define the value returned in \**RowCountPtr*. For example, some data sources may be able to return the number of rows returned by a **SELECT** statement or a catalog function before fetching the rows.  
  
> [!NOTE]  
>  Many data sources cannot return the number of rows in a result set before fetching them; for maximum interoperability, applications should not rely on this behavior.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLRowCount** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLRowCount** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLRowCount** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The function was called prior to calling **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** for the *StatementHandle*.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 If the last SQL statement executed on the statement handle was not an **UPDATE**, **INSERT**, or **DELETE** statement or if the *Operation* argument in the previous call to **SQLBulkOperations** was not SQL_ADD, SQL_UPDATE_BY_BOOKMARK, or SQL_DELETE_BY_BOOKMARK, or if the *Operation* argument in the previous call to **SQLSetPos** was not SQL_UPDATE or SQL_DELETE, the value of **RowCountPtr* is driver-defined. For more information, see [Determining the Number of Affected Rows](../../../odbc/reference/develop-app/determining-the-number-of-affected-rows.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
