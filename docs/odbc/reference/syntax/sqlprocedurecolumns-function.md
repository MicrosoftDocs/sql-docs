---
title: "SQLProcedureColumns Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLProcedureColumns"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLProcedureColumns"
helpviewer_keywords: 
  - "SQLProcedureColumns function [ODBC]"
ms.assetid: 4ca37b28-a6df-465b-8988-d422d37fc025
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLProcedureColumns Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLProcedureColumns** returns the list of input and output parameters, as well as the columns that make up the result set for the specified procedures. The driver returns the information as a result set on the specified statement.  
  
## Syntax  
  
```  
  
SQLRETURN SQLProcedureColumns(  
     SQLHSTMT      StatementHandle,  
     SQLCHAR *     CatalogName,  
     SQLSMALLINT   NameLength1,  
     SQLCHAR *     SchemaName,  
     SQLSMALLINT   NameLength2,  
     SQLCHAR *     ProcName,  
     SQLSMALLINT   NameLength3,  
     SQLCHAR *     ColumnName,  
     SQLSMALLINT   NameLength4);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CatalogName*  
 [Input] Procedure catalog name. If a driver supports catalogs for some procedures but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those procedures that do not have catalogs. *CatalogName* cannot contain a string search pattern.  
  
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
  
 *ColumnName*  
 [Input] String search pattern for column names.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *ColumnName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *ColumnName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength4*  
 [Input] Length in characters of **ColumnName*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLProcedureColumns** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLProcedureColumns** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLError** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, the *CatalogName* argument was a null pointer, and the SQL_CATALOG_NAME *InfoType* returns that catalog names are supported.<br /><br /> (DM) The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, and the *SchemaName*, *ProcName*, or *ColumnName* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This aynschronous function was still executing when the SQLProcedureColumns function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY090|Invalid string or buffer length|(DM) The value of one of the name length arguments was less than 0 but not equal to SQL_NTS.<br /><br /> The value of one of the name length arguments exceeded the maximum length value for the corresponding catalog, schema, procedure, or column name.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|A procedure catalog was specified, and the driver or data source does not support catalogs.<br /><br /> A procedure schema was specified, and the driver or data source does not support schemas.<br /><br /> A string search pattern was specified for the procedure schema, procedure name, or column name, and the data source does not support search patterns for one or more of those arguments.<br /><br /> The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 This function is typically used before statement execution to retrieve information about procedure parameters and the columns that make up the result set or sets returned by the procedure, if any. For more information, see [Procedures](../../../odbc/reference/develop-app/procedures-odbc.md).  
  
> [!NOTE]  
>  **SQLProcedureColumns** might not return all columns used by a procedure. For example, a driver might return only information about the parameters used by a procedure and not the columns in a result set it generates.  
  
 The *SchemaName*, *ProcName*, and *ColumnName* arguments accept search patterns. For more information about valid search patterns, see [Pattern Value Arguments](../../../odbc/reference/develop-app/pattern-value-arguments.md).  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 **SQLProcedureColumns** returns the results as a standard result set, ordered by PROCEDURE_CAT, PROCEDURE_SCHEM, PROCEDURE_NAME, and COLUMN_TYPE. Column names are returned for each procedure in the following order: the name of the return value, the names of each parameter in the procedure invocation (in call order), and then the names of each column in the result set returned by the procedure (in column order).  
  
 Applications should bind driver-specific columns relative to the end of the result set. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
 To determine the actual lengths of the PROCEDURE_CAT, PROCEDURE_SCHEM, PROCEDURE_NAME, and COLUMN_NAME columns, an application can call **SQLGetInfo** with the SQL_MAX_CATALOG_NAME_LEN, SQL_MAX_SCHEMA_NAME_LEN, SQL_MAX_PROCEDURE_NAME_LEN, and SQL_MAX_COLUMN_NAME_LEN options.  
  
 The following columns have been renamed for ODBC 3.*x*. The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3.*x* column|  
|---------------------|-----------------------|  
|PROCEDURE_QUALIFIER|PROCEDURE_CAT|  
|PROCEDURE _OWNER|PROCEDURE_SCHEM|  
|PRECISION|COLUMN_SIZE|  
|LENGTH|BUFFER_LENGTH|  
|SCALE|DECIMAL_DIGITS|  
|RADIX|NUM_PREC_RADIX|  
  
 The following columns have been added to the results set returned by **SQLProcedureColumns** for ODBC 3.*x*:  
  
-   COLUMN_DEF  
  
-   DATETIME_CODE  
  
-   CHAR_OCTET_LENGTH  
  
-   ORDINAL_POSITION  
  
-   IS_NULLABLE  
  
 The following table lists the columns in the result set. Additional columns beyond column 19 (IS_NULLABLE) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set rather than specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
|Column name|Column number|Data type|Comments|  
|-----------------|-------------------|---------------|--------------|  
|PROCEDURE_CAT (ODBC 2.0)|1|Varchar|Procedure catalog name; NULL if not applicable to the data source. If a driver supports catalogs for some procedures but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those procedures that do not have catalogs.|  
|PROCEDURE_SCHEM (ODBC 2.0)|2|Varchar|Procedure schema name; NULL if not applicable to the data source. If a driver supports schemas for some procedures but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those procedures that do not have schemas.|  
|PROCEDURE_NAME (ODBC 2.0)|3|Varchar not NULL|Procedure name. An empty string is returned for a procedure that does not have a name.|  
|COLUMN_NAME (ODBC 2.0)|4|Varchar not NULL|Procedure column name. The driver returns an empty string for a procedure column that does not have a name.|  
|COLUMN_TYPE (ODBC 2.0)|5|Smallint not NULL|Defines the procedure column as a parameter or a result set column:<br /><br /> SQL_PARAM_TYPE_UNKNOWN: The procedure column is a parameter whose type is unknown. (ODBC 1.0)<br /><br /> SQL_PARAM_INPUT: The procedure column is an input parameter. (ODBC 1.0)<br /><br /> SQL_PARAM_INPUT_OUTPUT: The procedure column is an input/output parameter. (ODBC 1.0)<br /><br /> SQL_PARAM_OUTPUT: The procedure column is an output parameter. (ODBC 2.0)<br /><br /> SQL_RETURN_VALUE: The procedure column is the return value of the procedure. (ODBC 2.0)<br /><br /> SQL_RESULT_COL: The procedure column is a result set column. (ODBC 1.0)|  
|DATA_TYPE (ODBC 2.0)|6|Smallint not NULL|SQL data type. This can be an ODBC SQL data type or a driver-specific SQL data type. For datetime and interval data types, this column returns the concise data types (for example, SQL_TYPE_TIME or SQL_INTERVAL_YEAR_TO_MONTH). For a list of valid ODBC SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) in Appendix D: Data Types. For information about driver-specific SQL data types, see the driver's documentation.|  
|TYPE_NAME (ODBC 2.0)|7|Varchar not NULL|Data source-dependent data type name; for example, "CHAR", "VARCHAR", "MONEY", "LONG VARBINARY", or "CHAR ( ) FOR BIT DATA".|  
|COLUMN_SIZE (ODBC 2.0)|8|Integer|The column size of the procedure column on the data source. NULL is returned for data types where column size is not applicable. For more information concerning precision, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|BUFFER_LENGTH (ODBC 2.0)|9|Integer|The length in bytes of data transferred on an **SQLGetData** or **SQLFetch** operation if SQL_C_DEFAULT is specified. For numeric data, this size may be different than the size of the data stored on the data source. For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md), in Appendix D: Data Types.|  
|DECIMAL_DIGITS (ODBC 2.0)|10|Smallint|The decimal digits of the procedure column on the data source. NULL is returned for data types where decimal digits is not applicable. For more information concerning decimal digits, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md), in Appendix D: Data Types.|  
|NUM_PREC_RADIX (ODBC 2.0)|11|Smallint|For numeric data types, either 10 or 2.<br /><br /> If 10, the values in COLUMN_SIZE and DECIMAL_DIGITS give the number of decimal digits allowed for the column. For example, a DECIMAL(12,5) column would return a NUM_PREC_RADIX of 10, a COLUMN_SIZE of 12, and a DECIMAL_DIGITS of 5; a FLOAT column could return a NUM_PREC_RADIX of 10, a COLUMN_SIZE of 15, and a DECIMAL_DIGITS of NULL.<br /><br /> If 2, the values in COLUMN_SIZE and DECIMAL_DIGITS give the number of bits allowed in the column. For example, a FLOAT column could return a NUM_PREC_RADIX of 2, a COLUMN_SIZE of 53, and a DECIMAL_DIGITS of NULL.<br /><br /> NULL is returned for data types where NUM_PREC_RADIX is not applicable.|  
|NULLABLE (ODBC 2.0)|12|Smallint not NULL|Whether the procedure column accepts a NULL value:<br /><br /> SQL_NO_NULLS: The procedure column does not accept NULL values.<br /><br /> SQL_NULLABLE: The procedure column accepts NULL values.<br /><br /> SQL_NULLABLE_UNKNOWN: It is not known if the procedure column accepts NULL values.|  
|REMARKS (ODBC 2.0)|13|Varchar|A description of the procedure column.|  
|COLUMN_DEF (ODBC 3.0)|14|Varchar|The default value of the column.<br /><br /> If NULL was specified as the default value, this column is the word NULL, not enclosed in quotation marks. If the default value cannot be represented without truncation, this column contains TRUNCATED, with no enclosing single quotation marks. If no default value was specified, this column is NULL.<br /><br /> The value of COLUMN_DEF can be used in generating a new column definition, except when it contains the value TRUNCATED.|  
|SQL_DATA_TYPE (ODBC 3.0)|15|Smallint not NULL|The value of the SQL data type as it appears in the SQL_DESC_TYPE field of the descriptor. This column is the same as the DATA_TYPE column, except for datetime and interval data types.<br /><br /> For datetime and interval data types, the SQL_DATA_TYPE field in the result set will return SQL_INTERVAL or SQL_DATETIME, and the SQL_DATETIME_SUB field will return the subcode for the specific interval or datetime data type. (See [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).)|  
|SQL_DATETIME_SUB (ODBC 3.0)|16|Smallint|The subtype code for datetime and interval data types. For other data types, this column returns a NULL.|  
|CHAR_OCTET_LENGTH (ODBC 3.0)|17|Integer|The maximum length in bytes of a character or binary data type column. For all other data types, this column returns a NULL.|  
|ORDINAL_POSITION (ODBC 3.0)|18|Integer not NULL|For input and output parameters, the ordinal position of the parameter in the procedure definition (in increasing parameter order, starting at 1). For a return value (if any), 0 is returned. For result-set columns, the ordinal position of the column in the result set, with the first column in the result set being number 1. If there are multiple result sets, column ordinal positions are returned in a driver-specific manner.|  
|IS_NULLABLE (ODBC 3.0)|19|Varchar|"NO" if the column does not include NULLs.<br /><br /> "YES" if the column can include NULLs.<br /><br /> This column returns a zero-length string if nullability is unknown.<br /><br /> ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS cannot return an empty string.<br /><br /> The value returned for this column is different from the value returned for the NULLABLE column. (See the description of the NULLABLE column.)|  
  
## Code Example  
 See [Procedure Calls](../../../odbc/reference/develop-app/procedure-calls.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Returning a list of procedures in a data source|[SQLProcedures Function](../../../odbc/reference/syntax/sqlprocedures-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
