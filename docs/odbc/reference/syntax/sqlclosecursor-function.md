---
title: "SQLCloseCursor Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLCloseCursor"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLCloseCursor"
helpviewer_keywords: 
  - "SQLCloseCursor function [ODBC]"
ms.assetid: 05b0a054-e28d-4e16-b5b0-07418486b372
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLCloseCursor Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLCloseCursor** closes a cursor that has been opened on a statement and discards pending results.  
  
## Syntax  
  
```  
  
SQLRETURN SQLCloseCursor(  
     SQLHSTMT     StatementHandle);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLCloseCursor** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLCloseCursor** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|24000|Invalid cursor state|No cursor was open on the *StatementHandle*. (This is returned only by an ODBC 3.*x* driver.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle associated with the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 **SQLCloseCursor** returns SQLSTATE 24000 (Invalid cursor state) if no cursor is open. Calling **SQLCloseCursor** is equivalent to calling **SQLFreeStmt** with the SQL_CLOSE option, with the exception that **SQLFreeStmt** with SQL_CLOSE has no effect on the application if no cursor is open on the statement, while **SQLCloseCursor** returns SQLSTATE 24000 (Invalid cursor state).  
  
> [!NOTE]  
>  If an ODBC 3.*x* application working with an ODBC 2.*x* driver calls **SQLCloseCursor** when no cursor is open, SQLSTATE 24000 (Invalid cursor state) is not returned, because the Driver Manager maps **SQLCloseCursor** to **SQLFreeStmt** with SQL_CLOSE.  
  
 For more information, see [Closing the Cursor](../../../odbc/reference/develop-app/closing-the-cursor.md).  
  
## Code Example  
 See [SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md) and [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Freeing a handle|[SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)|  
|Processing multiple result sets|[SQLMoreResults Function](../../../odbc/reference/syntax/sqlmoreresults-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
