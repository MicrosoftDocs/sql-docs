---
description: "SQLPrepare Function"
title: "SQLPrepare Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLPrepare"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLPrepare"
helpviewer_keywords: 
  - "SQLPrepare function [ODBC]"
ms.assetid: 332e1b4b-b0ed-4e7a-aa4d-4f35f4f4476b
author: David-Engel
ms.author: v-davidengel
---
# SQLPrepare Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLPrepare** prepares an SQL string for execution.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLPrepare(  
     SQLHSTMT      StatementHandle,  
     SQLCHAR *     StatementText,  
     SQLINTEGER    TextLength);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *StatementText*  
 [Input] SQL text string.  
  
 *TextLength*  
 [Input] Length of **StatementText* in characters.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLPrepare** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLPrepare** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|A specified statement attribute was invalid because of implementation working conditions, so a similar value was temporarily substituted. (**SQLGetStmtAttr** can be called to determine what the temporarily substituted value is.) The substitute value is valid for the *StatementHandle* until the cursor is closed. The statement attributes that can be changed are: SQL_ATTR_CONCURRENCY SQL_ATTR_CURSOR_TYPE SQL_ATTR_KEYSET_SIZE SQL_ATTR_MAX_LENGTH SQL_ATTR_MAX_ROWS SQL_ATTR_QUERY_TIMEOUT SQL_ATTR_SIMULATE_CURSOR<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|21S01|Insert value list does not match column list|\**StatementText* contained an **INSERT** statement, and the number of values to be inserted did not match the degree of the derived table.|  
|21S02|Degree of derived table does not match column list|\**StatementText* contained a **CREATE VIEW** statement, and the number of names specified is not the same degree as the derived table defined by the query specification.|  
|22018|Invalid character value for cast specification|**StatementText* contained an SQL statement that contained a literal or parameter, and the value was incompatible with the data type of the associated table column.|  
|22019|Invalid escape character|The argument *StatementText* contained a **LIKE** predicate with an **ESCAPE** in the **WHERE** clause, and the length of the escape character following **ESCAPE** was not equal to 1.|  
|22025|Invalid escape sequence|The argument *StatementText* contained "**LIKE** _pattern value_ **ESCAPE** _escape character_" in the **WHERE** clause, and the character following the escape character in the pattern value was neither "%" nor "_".|  
|24000|Invalid cursor state|(DM) A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called.<br /><br /> A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|34000|Invalid cursor name|\**StatementText* contained a positioned **DELETE** or a positioned **UPDATE**, and the cursor referenced by the statement being prepared was not open.|  
|3D000|Invalid catalog name|The catalog name specified in *StatementText* was invalid.|  
|3F000|Invalid schema name|The schema name specified in *StatementText* was invalid.|  
|42000|Syntax error or access violation|\**StatementText* contained an SQL statement that was not preparable or contained a syntax error.<br /><br /> **StatementText* contained a statement for which the user did not have the required privileges.|  
|42S01|Base table or view already exists|\**StatementText* contained a **CREATE TABLE** or **CREATE VIEW** statement, and the table name or view name specified already exists.|  
|42S02|Base table or view not found|\**StatementText* contained a **DROP TABLE** or a **DROP VIEW** statement, and the specified table name or view name did not exist.<br /><br /> \**StatementText* contained an **ALTER TABLE** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **CREATE VIEW** statement, and a table name or view name defined by the query specification did not exist.<br /><br /> \**StatementText* contained a **CREATE INDEX** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **GRANT** or **REVOKE** statement, and the specified table name or view name did not exist.<br /><br /> \**StatementText* contained a **SELECT** statement, and a specified table name or view name did not exist.<br /><br /> \**StatementText* contained a **DELETE**, **INSERT**, or **UPDATE** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **CREATE TABLE** statement, and a table specified in a constraint (referencing a table other than the one being created) did not exist.|  
|42S11|Index already exists|\**StatementText* contained a **CREATE INDEX** statement, and the specified index name already existed.|  
|42S12|Index not found|\**StatementText* contained a **DROP INDEX** statement, and the specified index name did not exist.|  
|42S21|Column already exists|\**StatementText* contained an **ALTER TABLE** statement, and the column specified in the **ADD** clause is not unique or identifies an existing column in the base table.|  
|42S22|Column not found|\**StatementText* contained a **CREATE INDEX** statement, and one or more of the column names specified in the column list did not exist.<br /><br /> \**StatementText* contained a **GRANT** or **REVOKE** statement, and a specified column name did not exist.<br /><br /> \**StatementText* contained a **SELECT**, **DELETE**, **INSERT**, or **UPDATE** statement, and a specified column name did not exist.<br /><br /> \**StatementText* contained a **CREATE TABLE** statement, and a column specified in a constraint (referencing a table other than the one being created) did not exist.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|(DM) *StatementText* was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLPrepare** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The argument *TextLength* was less than or equal to 0 but not equal to SQL_NTS.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The concurrency setting was invalid for the type of cursor defined.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 The application calls **SQLPrepare** to send an SQL statement to the data source for preparation. For more information about prepared execution, see [Prepared Execution](../../../odbc/reference/develop-app/prepared-execution-odbc.md). The application can include one or more parameter markers in the SQL statement. To include a parameter marker, the application embeds a question mark (?) into the SQL string at the appropriate position. For information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md).  
  
> [!NOTE]  
>  If an application uses **SQLPrepare** to prepare and **SQLExecute** to submit a **COMMIT** or **ROLLBACK** statement, it will not be interoperable between DBMS products. To commit or roll back a transaction, call **SQLEndTran**.  
  
 The driver can modify the statement to use the form of SQL used by the data source and then submit it to the data source for preparation. In particular, the driver modifies the escape sequences used to define SQL syntax for certain features. (For a description of SQL statement grammar, see [Escape Sequences in ODBC](../../../odbc/reference/develop-app/escape-sequences-in-odbc.md) and [Appendix C: SQL Grammar](../../../odbc/reference/appendixes/appendix-c-sql-grammar.md).) For the driver, a statement handle is similar to a statement identifier in embedded SQL code. If the data source supports statement identifiers, the driver can send a statement identifier and parameter values to the data source.  
  
 After a statement is prepared, the application uses the statement handle to refer to the statement in later function calls. The prepared statement associated with the statement handle can be re-executed by calling **SQLExecute** until the application frees the statement with a call to **SQLFreeStmt** with the SQL_DROP option or until the statement handle is used in a call to **SQLPrepare**, **SQLExecDirect**, or one of the catalog functions (**SQLColumns**, **SQLTables**, and so on). Once the application prepares a statement, it can request information about the format of the result set. For some implementations, calling **SQLDescribeCol** or **SQLDescribeParam** after **SQLPrepare** might not be as efficient as calling the function after **SQLExecute** or **SQLExecDirect**.  
  
 Some drivers cannot return syntax errors or access violations when the application calls **SQLPrepare**. A driver can handle syntax errors and access violations, only syntax errors, or neither syntax errors nor access violations. Therefore, an application must be able to handle these conditions when calling subsequent related functions such as **SQLNumResultCols**, **SQLDescribeCol**, **SQLColAttribute**, and **SQLExecute**.  
  
 Depending on the capabilities of the driver and data source, parameter information (such as data types) might be checked when the statement is prepared (if all parameters have been bound) or when it is executed (if all parameters have not been bound). For maximum interoperability, an application should unbind all parameters that applied to an old SQL statement before preparing a new SQL statement on the same statement. This prevents errors that are due to old parameter information being applied to the new statement.  
  
> [!IMPORTANT]  
>  Committing a transaction, either by explicitly calling **SQLEndTran** or by working in autocommit mode, can cause the data source to delete the access plans for all statements on a connection. For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR information types in [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) and [Effect of Transactions on Cursors and Prepared Statements](../../../odbc/reference/develop-app/effect-of-transactions-on-cursors-and-prepared-statements.md).  
  
## Code Example  
 See [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md), [SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md), and [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a statement handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Binding a buffer to a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Executing a commit or rollback operation|[SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Returning the number of rows affected by a statement|[SQLRowCount Function](../../../odbc/reference/syntax/sqlrowcount-function.md)|  
|Setting a cursor name|[SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
