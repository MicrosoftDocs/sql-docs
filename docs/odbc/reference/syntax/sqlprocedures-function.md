---
description: "SQLProcedures Function"
title: "SQLProcedures Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLProcedures"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLProcedures"
helpviewer_keywords: 
  - "SQLProcedures function [ODBC]"
ms.assetid: d0d9ef10-2fd4-44a5-9334-649f186f4ba0
author: David-Engel
ms.author: v-davidengel
---
# SQLProcedures Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLProcedures** returns the list of procedure names stored in a specific data source. *Procedure* is a generic term used to describe an *executable object*, or a named entity that can be invoked using input and output parameters. For more information on procedures, see the [Procedures](../../../odbc/reference/develop-app/procedures-odbc.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLProcedures(  
     SQLHSTMT       StatementHandle,  
     SQLCHAR *      CatalogName,  
     SQLSMALLINT    NameLength1,  
     SQLCHAR *      SchemaName,  
     SQLSMALLINT    NameLength2,  
     SQLCHAR *      ProcName,  
     SQLSMALLINT    NameLength3);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CatalogName*  
 [Input] Procedure catalog. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have catalogs. *CatalogName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *CatalogName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *CatalogName* is an ordinary argument; it is treated literally, and its case is significant. For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).  
  
 *NameLength1*  
 [Input] Length in characters of **CatalogName*.  
  
 *SchemaName*  
 [Input] String search pattern for procedure schema names. If a driver supports schemas for some procedures but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those procedures that do not have schemas.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *SchemaName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *SchemaName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength2*  
 [Input] Length in characters of **SchemaName*.  
  
 *ProcName*  
 [Input] String search pattern for procedure names.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *ProcName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *ProcName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength3*  
 [Input] Length in characters of **ProcName*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLProcedures** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLProcedures** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, the *CatalogName* argument was a null pointer, and the SQL_CATALOG_NAME *InfoType* returns that catalog names are supported.<br /><br /> (DM) The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, and the *SchemaName* or *ProcName* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value of one of the name length arguments was less than 0 but not equal to SQL_NTS.<br /><br /> The value of one of the name length arguments exceeded the maximum length value for the corresponding name.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|A procedure catalog was specified, and the driver or data source does not support catalogs.<br /><br /> A procedure schema was specified, and the driver or data source does not support schemas.<br /><br /> A string search pattern was specified for the procedure schema or procedure name, and the data source does not support search patterns for one or more of those arguments.<br /><br /> The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the requested result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support this function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLProcedures** lists all procedures in the requested range. A user may or may not have permission to execute any of these procedures. To check accessibility, an application can call **SQLGetInfo** and check the SQL_ACCESSIBLE_PROCEDURES information value. Otherwise, the application must be able to handle a situation where the user selects a procedure that it cannot execute. For information about how this information might be used, see [Procedures](../../../odbc/reference/develop-app/procedures-odbc.md).  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 **SQLProcedures** returns the results as a standard result set, ordered by PROCEDURE_CAT, PROCEDURE_SCHEMA, and PROCEDURE_NAME.  
  
> [!NOTE]  
>  **SQLProcedures** might not return all procedures. Applications can use any valid procedure, regardless of whether it is returned by **SQLProcedures**.  
  
 The following columns have been renamed for ODBC 3*.x*. The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3*.x* column|  
|---------------------|-----------------------|  
|PROCEDURE_QUALIFIER|PROCEDURE_CAT|  
|PROCEDURE _OWNER|PROCEDURE _SCHEM|  
  
 To determine the actual lengths of the PROCEDURE_CAT, PROCEDURE_SCHEM, and PROCEDURE_NAME columns, an application can call **SQLGetInfo** with the SQL_MAX_CATALOG_NAME_LEN, SQL_MAX_SCHEMA_NAME_LEN, and SQL_MAX_PROCEDURE_NAME_LEN options.  
  
 The following table lists the columns in the result set. Additional columns beyond column 8 (PROCEDURE_TYPE) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set rather than specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
|Column name|Column number|Data type|Comments|  
|-----------------|-------------------|---------------|--------------|  
|PROCEDURE_CAT (ODBC 2.0)|1|Varchar|Procedure catalog identifier; NULL if not applicable to the data source. If a driver supports catalogs for some procedures but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those procedures that do not have catalogs.|  
|PROCEDURE_SCHEM (ODBC 2.0)|2|Varchar|Procedure schema identifier; NULL if not applicable to the data source. If a driver supports schemas for some procedures but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those procedures that do not have schemas.|  
|PROCEDURE_NAME (ODBC 2.0)|3|Varchar not NULL|Procedure identifier.|  
|NUM_INPUT_PARAMS (ODBC 2.0)|4|N/A|Reserved for future use. Applications should not rely on the data returned in these result columns.|  
|NUM_OUTPUT_PARAMS (ODBC 2.0)|5|N/A|Reserved for future use. Applications should not rely on the data returned in these result columns.|  
|NUM_RESULT_SETS (ODBC 2.0)|6|N/A|Reserved for future use. Applications should not rely on the data returned in these result columns.|  
|REMARKS (ODBC 2.0)|7|Varchar|A description of the procedure.|  
|PROCEDURE_TYPE (ODBC 2.0)|8|Smallint|Defines the procedure type:<br /><br /> SQL_PT_UNKNOWN: It cannot be determined whether the procedure returns a value.<br /><br /> SQL_PT_PROCEDURE: The returned object is a procedure; that is, it does not have a return value.<br /><br /> SQL_PT_FUNCTION: The returned object is a function; that is, it has a return value.|  
  
 The *SchemaName* and *ProcName* arguments accept search patterns. For more information about valid search patterns, see [Pattern Value Arguments](../../../odbc/reference/develop-app/pattern-value-arguments.md).  
  
## Code Example  
 See [Procedure Calls](../../../odbc/reference/develop-app/procedure-calls.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Returning information about a driver or data source|[SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md)|  
|Returning the parameters and result set columns of a procedure|[SQLProcedureColumns Function](../../../odbc/reference/syntax/sqlprocedurecolumns-function.md)|  
|Syntax for invoking stored procedures|[Executing Statements](../../../odbc/reference/develop-app/executing-statements-odbc.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
