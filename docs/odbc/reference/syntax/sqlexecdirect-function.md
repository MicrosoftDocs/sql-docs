---
title: "SQLExecDirect Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLExecDirect"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLExecDirect"
helpviewer_keywords: 
  - "SQLExecDirect function [ODBC]"
ms.assetid: 985fcee1-f204-425c-bdd1-deb0e7d7bbd9
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLExecDirect Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLExecDirect** executes a preparable statement, using the current values of the parameter marker variables if any parameters exist in the statement. **SQLExecDirect** is the fastest way to submit an SQL statement for one-time execution.  
  
## Syntax  
  
```  
  
SQLRETURN SQLExecDirect(  
     SQLHSTMT     StatementHandle,  
     SQLCHAR *    StatementText,  
     SQLINTEGER   TextLength);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *StatementText*  
 [Input] SQL statement to be executed.  
  
 *TextLength*  
 [Input] Length of **StatementText* in characters.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NEED_DATA, SQL_STILL_EXECUTING, SQL_ERROR, SQL_NO_DATA, SQL_INVALID_HANDLE, or SQL_PARAM_DATA_AVAILABLE.  
  
## Diagnostics  
 When **SQLExecDirect** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLExecDirect** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01001|Cursor operation conflict|\**StatementText* contained a positioned update or delete statement, and no rows or more than one row were updated or deleted. (For more information about updates to more than one row, see the description of the SQL_ATTR_SIMULATE_CURSOR *Attribute* in **SQLSetStmtAttr**.)<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01003|NULL value eliminated in set function|The argument *StatementText* contained a set function (such as **AVG**, **MAX**, **MIN**, and so on), but not the **COUNT** set function, and NULL argument values were eliminated before the function was applied. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|String or binary data returned for an input/output or output parameter resulted in the truncation of nonblank character or non-NULL binary data. If it was a string value, it was right-truncated. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01006|Privilege not revoked|\**StatementText* contained a **REVOKE** statement, and the user did not have the specified privilege. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01007|Privilege not granted|*\*StatementText* was a **GRANT** statement, and the user could not be granted the specified privilege.|  
|01S02|Option value changed|A specified statement attribute was invalid because of implementation working conditions, so a similar value was temporarily substituted. (**SQLGetStmtAttr** can be called to determine what the temporarily substituted value is.) The substitute value is valid for the *StatementHandle* until the cursor is closed, at which point the statement attribute reverts to its previous value. The statement attributes that can be changed are:<br /><br /> SQL_ ATTR_CONCURRENCY SQL_ ATTR_CURSOR_TYPE SQL_ ATTR_KEYSET_SIZE SQL_ ATTR_MAX_LENGTH SQL_ ATTR_MAX_ROWS SQL_ ATTR_QUERY_TIMEOUT  SQL_ ATTR_SIMULATE_CURSOR<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S07|Fractional truncation|The data returned for an input/output or output parameter was truncated such that the fractional part of a numeric data type was truncated or the fractional portion of the time component of a time, timestamp, or interval data type was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07002|COUNT field incorrect|The number of parameters specified in **SQLBindParameter** was less than the number of parameters in the SQL statement contained in \**StatementText*.<br /><br /> **SQLBindParameter** was called with *ParameterValuePtr* set to a null pointer, *StrLen_or_IndPtr* not set to SQL_NULL_DATA or SQL_DATA_AT_EXEC, and *InputOutputType* not set to SQL_PARAM_OUTPUT, so that the number of parameters specified in **SQLBindParameter** was greater than the number of parameters in the SQL statement contained in **StatementText*.|  
|07006|Restricted data type attribute violation|The data value identified by the *ValueType* argument in **SQLBindParameter** for the bound parameter could not be converted to the data type identified by the *ParameterType* argument in **SQLBindParameter**.<br /><br /> The data value returned for a parameter bound as SQL_PARAM_INPUT_OUTPUT or SQL_PARAM_OUTPUT could not be converted to the data type identified by the *ValueType* argument in **SQLBindParameter**.<br /><br /> (If the data values for one or more rows could not be converted but one or more rows were successfully returned, this function returns SQL_SUCCESS_WITH_INFO.)|  
|07007|Restricted parameter value violation|The parameter type SQL_PARAM_INPUT_OUTPUT_STREAM is only used for a parameter that sends and receives data in parts. An input bound buffer is not allowed for this parameter type.<br /><br /> This error will occur when the parameter type is SQL_PARAM_INPUT_OUTPUT, and when the \**StrLen_or_IndPtr* specified in **SQLBindParameter** is not equal to SQL_NULL_DATA, SQL_DEFAULT_PARAM, SQL_LEN_DATA_AT_EXEC(len), or SQL_DATA_AT_EXEC.|  
|07S01|Invalid use of default parameter|A parameter value, set with **SQLBindParameter**, was SQL_DEFAULT_PARAM, and the corresponding parameter did not have a default value.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|21S01|Insert value list does not match column list|\**StatementText* contained an **INSERT** statement, and the number of values to be inserted did not match the degree of the derived table.|  
|21S02|Degree of derived table does not match column list|\**StatementText* contained a **CREATE VIEW** statement, and the unqualified column list (the number of columns specified for the view in the *column-identifier* arguments of the SQL statement) contained more names than the number of columns in the derived table defined by the *query-specification* argument of the SQL statement.|  
|22001|String data, right truncation|The assignment of a character or binary value to a column resulted in the truncation of nonblank character data or non-null binary data.|  
|22002|Indicator variable required but not supplied|NULL data was bound to an output parameter whose *StrLen_or_IndPtr* set by **SQLBindParameter** was a null pointer.|  
|22003|Numeric value out of range|**StatementText* contained an SQL statement that contained a bound numeric parameter or literal, and the value caused the whole (as opposed to fractional) part of the number to be truncated when assigned to the associated table column.<br /><br /> Returning a numeric value (as numeric or string) for one or more input/output or output parameters would have caused the whole (as opposed to fractional) part of the number to be truncated.|  
|22007|Invalid datetime format|**StatementText* contained an SQL statement that contained a date, time, or timestamp structure as a bound parameter, and the parameter was, respectively, an invalid date, time, or timestamp.<br /><br /> An input/output or output parameter was bound to a date, time, or timestamp C structure, and a value in the returned parameter was, respectively, an invalid date, time, or timestamp. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22008|Datetime field overflow|**StatementText* contained an SQL statement that contained a datetime expression that, when computed, resulted in a date, time, or timestamp structure that was invalid.<br /><br /> A datetime expression computed for an input/output or output parameter resulted in a date, time, or timestamp C structure that was invalid.|  
|22012|Division by zero|**StatementText* contained an SQL statement that contained an arithmetic expression that caused division by zero.<br /><br /> An arithmetic expression calculated for an input/output or output parameter resulted in division by zero.|  
|22015|Interval field overflow|*\*StatementText* contained an exact numeric or interval parameter that, when converted to an interval SQL data type, caused a loss of significant digits.<br /><br /> *\*StatementText* contained an interval parameter with more than one field that, when converted to a numeric data type in a column, had no representation in the numeric data type.<br /><br /> *\*StatementText* contained parameter data that was assigned to an interval SQL type, and there was no representation of the value of the C type in the interval SQL type.<br /><br /> Assigning an input/output or output parameter that was an exact numeric or interval SQL type to an interval C type caused a loss of significant digits.<br /><br /> When an input/output or output parameter was assigned to an interval C structure, there was no representation of the data in the interval data structure.|  
|22018|Invalid character value for cast specification|*\*StatementText* contained a C type that was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.<br /><br /> When an input/output or output parameter was returned, the SQL type was an exact or approximate numeric, a datetime, or an interval data type; the C type was SQL_C_CHAR; and the value in the column was not a valid literal of the bound SQL type.|  
|22019|Invalid escape character|\**StatementText* contained an SQL statement that contained a **LIKE** predicate with an **ESCAPE** in the **WHERE** clause, and the length of the escape character following **ESCAPE** was not equal to 1.|  
|22025|Invalid escape sequence|\**StatementText* contained an SQL statement that contained "**LIKE** _pattern value_ **ESCAPE** _escape character_" in the **WHERE** clause, and the character following the escape character in the pattern value was not one of "%" or "_".|  
|23000|Integrity constraint violation|**StatementText* contained an SQL statement that contained a parameter or literal. The parameter value was NULL for a column defined as NOT NULL in the associated table column, a duplicate value was supplied for a column constrained to contain only unique values, or some other integrity constraint was violated.|  
|24000|Invalid cursor state|A cursor was positioned on the *StatementHandle* by **SQLFetch** or **SQLFetchScroll**. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open but not positioned on the *StatementHandle*.<br /><br /> **StatementText* contained a positioned update or delete statement, and the cursor was positioned before the start of the result set or after the end of the result set.|  
|34000|Invalid cursor name|**StatementText* contained a positioned update or delete statement, and the cursor referenced by the statement being executed was not open.|  
|3D000|Invalid catalog name|The catalog name specified in *StatementText* was invalid.|  
|3F000|Invalid schema name|The schema name specified in *StatementText* was invalid.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|42000|Syntax error or access violation|\**StatementText* contained an SQL statement that was not preparable or contained a syntax error.<br /><br /> The user did not have permission to execute the SQL statement contained in **StatementText*.|  
|42S01|Base table or view already exists|\**StatementText* contained a **CREATE TABLE** or **CREATE VIEW** statement, and the table name or view name specified already exists.|  
|42S02|Base table or view not found|\**StatementText* contained a **DROP TABLE** or a **DROP VIEW** statement, and the specified table name or view name did not exist.<br /><br /> \**StatementText* contained an **ALTER TABLE** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **CREATE VIEW** statement, and a table name or view name defined by the query specification did not exist.<br /><br /> \**StatementText* contained a **CREATE INDEX** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **GRANT** or **REVOKE** statement, and the specified table name or view name did not exist.<br /><br /> \**StatementText* contained a **SELECT** statement, and a specified table name or view name did not exist.<br /><br /> \**StatementText* contained a **DELETE**, **INSERT**, or **UPDATE** statement, and the specified table name did not exist.<br /><br /> \**StatementText* contained a **CREATE TABLE** statement, and a table specified in a constraint (referencing a table other than the one being created) did not exist.<br /><br /> \**StatementText* contained a **CREATE SCHEMA** statement, and a specified table name or view name did not exist.|  
|42S11|Index already exists|\**StatementText* contained a **CREATE INDEX** statement, and the specified index name already existed.<br /><br /> \**StatementText* contained a **CREATE SCHEMA** statement, and the specified index name already existed.|  
|42S12|Index not found|\**StatementText* contained a **DROP INDEX** statement, and the specified index name did not exist.|  
|42S21|Column already exists|\**StatementText* contained an **ALTER TABLE** statement, and the column specified in the **ADD** clause is not unique or identifies an existing column in the base table.|  
|42S22|Column not found|\**StatementText* contained a **CREATE INDEX** statement, and one or more of the column names specified in the column list did not exist.<br /><br /> \**StatementText* contained a **GRANT** or **REVOKE** statement, and a specified column name did not exist.<br /><br /> \**StatementText* contained a **SELECT**, **DELETE**, **INSERT**, or **UPDATE** statement, and a specified column name did not exist.<br /><br /> \**StatementText* contained a **CREATE TABLE** statement, and a column specified in a constraint (referencing a table other than the one being created) did not exist.<br /><br /> \**StatementText* contained a **CREATE SCHEMA** statement, and a specified column name did not exist.|  
|44000|WITH CHECK OPTION violation|The argument *StatementText* contained an **INSERT** statement performed on a viewed table or a table derived from the viewed table that was created by specifying **WITH CHECK OPTION**, such that one or more rows affected by the **INSERT** statement will no longer be present in the viewed table.<br /><br /> The argument *StatementText* contained an **UPDATE** statement performed on a viewed table or a table derived from the viewed table that was created by specifying **WITH CHECK OPTION**, such that one or more rows affected by the **UPDATE** statement will no longer be present in the viewed table.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|(DM) **StatementText* was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLExecDirect** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The argument *TextLength* was less than or equal to 0 but not equal to SQL_NTS.<br /><br /> A parameter value, set with **SQLBindParameter**, was a null pointer, and the parameter length value was not 0, SQL_NULL_DATA, SQL_DATA_AT_EXEC, SQL_DEFAULT_PARAM, or less than or equal to SQL_LEN_DATA_AT_EXEC_OFFSET.<br /><br /> A parameter value, set with **SQLBindParameter**, was not a null pointer; the C data type was SQL_C_BINARY or SQL_C_CHAR; and the parameter length value was less than 0 but was not SQL_NTS, SQL_NULL_DATA, SQL_DATA_AT_EXEC, SQL_DEFAULT_PARAM, or less than or equal to SQL_LEN_DATA_AT_EXEC_OFFSET.<br /><br /> A parameter length value bound by **SQLBindParameter** was set to SQL_DATA_AT_EXEC; the SQL type was either SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type; and the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y".|  
|HY105|Invalid parameter type|The value specified for the argument *InputOutputType* in **SQLBindParameter** was SQL_PARAM_OUTPUT, and the parameter was an input parameter.|  
|HY109|Invalid cursor position|\**StatementText* contained a positioned update or delete statement, and the cursor was positioned (by **SQLSetPos** or **SQLFetchScroll**) on a row that had been deleted or could not be fetched.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 The application calls **SQLExecDirect** to send an SQL statement to the data source. For more information about direct execution, see [Direct Execution](../../../odbc/reference/develop-app/direct-execution-odbc.md). The driver modifies the statement to use the form of SQL used by the data source and then submits it to the data source. In particular, the driver modifies the escape sequences used to define certain features in SQL. For the syntax of escape sequences, see [Escape Sequences in ODBC](../../../odbc/reference/develop-app/escape-sequences-in-odbc.md).  
  
 The application can include one or more parameter markers in the SQL statement. To include a parameter marker, the application embeds a question mark (?) into the SQL statement at the appropriate position. For information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md).  
  
 If the SQL statement is a **SELECT** statement and if the application called **SQLSetCursorName** to associate a cursor with a statement, then the driver uses the specified cursor. Otherwise, the driver generates a cursor name.  
  
 If the data source is in manual-commit mode (requiring explicit transaction initiation) and a transaction has not already been initiated, the driver initiates a transaction before it sends the SQL statement. For more information, see [Manual-Commit Mode](../../../odbc/reference/develop-app/manual-commit-mode.md).  
  
 If an application uses **SQLExecDirect** to submit a **COMMIT** or **ROLLBACK** statement, it will not be interoperable between DBMS products. To commit or roll back a transaction, an application calls **SQLEndTran**.  
  
 If **SQLExecDirect** encounters a data-at-execution parameter, it returns SQL_NEED_DATA. The application sends the data using **SQLParamData** and **SQLPutData**. See [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md), [SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md), [SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md), and [Sending Long Data](../../../odbc/reference/develop-app/sending-long-data.md).  
  
 If **SQLExecDirect** executes a searched update, insert, or delete statement that does not affect any rows at the data source, the call to **SQLExecDirect** returns SQL_NO_DATA.  
  
 If the value of the SQL_ATTR_PARAMSET_SIZE statement attribute is greater than 1 and the SQL statement contains at least one parameter marker, **SQLExecDirect** will execute the SQL statement once for each set of parameter values from the arrays pointed to by the *ParameterValuePointer* argument in the call to **SQLBindParameter**. For more information, see [Arrays of Parameter Values](../../../odbc/reference/develop-app/arrays-of-parameter-values.md).  
  
 If bookmarks are turned on and a query is executed that cannot support bookmarks, the driver should attempt to coerce the environment to one that supports bookmarks by changing an attribute value and returning SQLSTATE 01S02 (Option value changed). If the attribute cannot be changed, the driver should return SQLSTATE HY024 (Invalid attribute value).  
  
> [!NOTE]  
>  When using connection pooling, an application must not execute SQL statements that change the database or the context of the database, such as the **USE** _database_ statement in SQL Server, which changes the catalog used by a data source.  
  
## Code Example  
 See [SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md), [SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md), and [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Executing a commit or rollback operation|[SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Fetching multiple rows of data|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Returning a cursor name|[SQLGetCursorName Function](../../../odbc/reference/syntax/sqlgetcursorname-function.md)|  
|Fetching part or all of a column of data|[SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md)|  
|Returning the next parameter to send data for|[SQLParamData Function](../../../odbc/reference/syntax/sqlparamdata-function.md)|  
|Preparing a statement for execution|[SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md)|  
|Sending parameter data at execution time|[SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md)|  
|Setting a cursor name|[SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md)|  
|Setting a statement attribute|[SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
