---
title: "SQLGetTypeInfo Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetTypeInfo"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetTypeInfo"
helpviewer_keywords: 
  - "SQLGetTypeInfo function [ODBC]"
ms.assetid: bdedb044-8924-4ca4-85f3-8b37578e0257
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetTypeInfo Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetTypeInfo** returns information about data types supported by the data source. The driver returns the information in the form of an SQL result set. The data types are intended for use in Data Definition Language (DDL) statements.  
  
> [!IMPORTANT]  
>  Applications must use the type names returned in the TYPE_NAME column of the **SQLGetTypeInfo** result set in **ALTER TABLE** and **CREATE TABLE** statements. **SQLGetTypeInfo** may return more than one row with the same value in the DATA_TYPE column.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetTypeInfo(  
     SQLHSTMT      StatementHandle,  
     SQLSMALLINT   DataType);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle for the result set.  
  
 *DataType*  
 [Input] The SQL data type. This must be one of the values in the [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) section of Appendix D: Data Types, or a driver-specific SQL data type. SQL_ALL_TYPES specifies that information about all data types should be returned.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetTypeInfo** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetTypeInfo** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|A specified statement attribute was invalid because of implementation working conditions, so a similar value was temporarily substituted. (Call **SQLGetStmtAttr** to determine the temporarily substituted value.) The substitute value is valid for the *StatementHandle* until the cursor is closed. The statement attributes that can be changed are: SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, SQL_ATTR_KEYSET_SIZE, SQL_ATTR_MAX_LENGTH, SQL_ATTR_MAX_ROWS, SQL_ATTR_QUERY_TIMEOUT, and SQL_ATTR_SIMULATE_CURSOR. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle,* and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A result set was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY004|Invalid SQL data type|The value specified for the argument *DataType* was neither a valid ODBC SQL data type identifier nor a driver-specific data type identifier supported by the driver.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*, then the function was called and, before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called and, before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLGetTypeInfo** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLGetTypeInfo** returns the results as a standard result set, ordered by DATA_TYPE and then by how closely the data type maps to the corresponding ODBC SQL data type. Data types defined by the data source take precedence over user-defined data types. Consequently, the sort order is not necessarily consistent but can be generalized as DATA_TYPE first, followed by TYPE_NAME, both ascending. For example, suppose that a data source defined INTEGER and COUNTER data types, where COUNTER is auto-incrementing, and that a user-defined data type WHOLENUM has also been defined. These would be returned in the order INTEGER, WHOLENUM, and COUNTER, because WHOLENUM maps closely to the ODBC SQL data type SQL_INTEGER, while the auto-incrementing data type, even though supported by the data source, does not map closely to an ODBC SQL data type. For information about how this information might be used, see [DDL Statements](../../../odbc/reference/develop-app/ddl-statements.md).  
  
 If the *DataType* argument specifies a data type which is valid for the version of ODBC supported by the driver, but is not supported by the driver, then it will return an empty result set.  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 The following columns have been renamed for ODBC 3.*x*. The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3.*x* column|  
|---------------------|-----------------------|  
|PRECISION|COLUMN_SIZE|  
|MONEY|FIXED_PREC_SCALE|  
|AUTO_INCREMENT|AUTO_UNIQUE_VALUE|  
  
 The following columns have been added to the results set returned by **SQLGetTypeInfo** for ODBC 3.*x*:  
  
-   SQL_DATA_TYPE  
  
-   INTERVAL_PRECISION  
  
-   SQL_DATETIME_SUB  
  
-   NUM_PREC_RADIX  
  
 The following table lists the columns in the result set. Additional columns beyond column 19 (INTERVAL_PRECISION) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set rather than specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
> [!NOTE]  
>  **SQLGetTypeInfo** might not return all data types. For example, a driver might not return user-defined data types. Applications can use any valid data type, regardless of whether it is returned by **SQLGetTypeInfo**. The data types returned by **SQLGetTypeInfo** are those supported by the data source. They are intended for use in Data Definition Language (DDL) statements. Drivers can return result-set data using data types other than the types returned by **SQLGetTypeInfo**. In creating the result set for a catalog function, the driver might use a data type that is not supported by the data source.  
  
|Column name|Column<br /><br /> number|Data type|Comments|  
|-----------------|-----------------------|---------------|--------------|  
|TYPE_NAME (ODBC 2.0)|1|Varchar not NULL|Data source-dependent data-type name; for example, "CHAR()", "VARCHAR()", "MONEY", "LONG VARBINARY", or "CHAR ( ) FOR BIT DATA". Applications must use this name in **CREATE TABLE** and **ALTER TABLE** statements.|  
|DATA_TYPE (ODBC 2.0)|2|Smallint not NULL|SQL data type. This can be an ODBC SQL data type or a driver-specific SQL data type. For datetime or interval data types, this column returns the concise data type (such as SQL_TYPE_TIME or SQL_INTERVAL_YEAR_TO_MONTH). For a list of valid ODBC SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) in Appendix D: Data Types. For information about driver-specific SQL data types, see the driver's documentation.|  
|COLUMN_SIZE (ODBC 2.0)|3|Integer|The maximum column size that the server supports for this data type. For numeric data, this is the maximum precision. For string data, this is the length in characters. For datetime data types, this is the length in characters of the string representation (assuming the maximum allowed precision of the fractional seconds component). NULL is returned for data types where column size is not applicable. For interval data types, this is the number of characters in the character representation of the interval literal (as defined by the interval leading precision; see [Interval Data Type Length](../../../odbc/reference/appendixes/interval-data-type-length.md) in Appendix D: Data Types).<br /><br /> For more information on column size, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|LITERAL_PREFIX (ODBC 2.0)|4|Varchar|Character or characters used to prefix a literal; for example, a single quotation mark (') for character data types or 0x for binary data types; NULL is returned for data types where a literal prefix is not applicable.|  
|LITERAL_SUFFIX (ODBC 2.0)|5|Varchar|Character or characters used to terminate a literal; for example, a single quotation mark (') for character data types; NULL is returned for data types where a literal suffix is not applicable.|  
|CREATE_PARAMS (ODBC 2.0)|6|Varchar|A list of keywords, separated by commas, corresponding to each parameter that the application may specify in parentheses when using the name that is returned in the TYPE_NAME field. The keywords in the list can be any of the following: length, precision, or scale. They appear in the order that the syntax requires them to be used. For example, CREATE_PARAMS for DECIMAL would be "precision,scale"; CREATE_PARAMS for VARCHAR would equal "length." NULL is returned if there are no parameters for the data type definition; for example, INTEGER.<br /><br /> The driver supplies the CREATE_PARAMS text in the language of the country/region where it is used.|  
|NULLABLE (ODBC 2.0)|7|Smallint not NULL|Whether the data type accepts a NULL value:<br /><br /> SQL_NO_NULLS if the data type does not accept NULL values.<br /><br /> SQL_NULLABLE if the data type accepts NULL values.<br /><br /> SQL_NULLABLE_UNKNOWN if it is not known whether the column accepts NULL values.|  
|CASE_SENSITIVE (ODBC 2.0)|8|Smallint not NULL|Whether a character data type is case-sensitive in collations and comparisons:<br /><br /> SQL_TRUE if the data type is a character data type and is case-sensitive.<br /><br /> SQL_FALSE if the data type is not a character data type or is not case-sensitive.|  
|SEARCHABLE (ODBC 2.0)|9|Smallint not NULL|How the data type is used in a **WHERE** clause:<br /><br /> SQL_PRED_NONE if the column cannot be used in a **WHERE** clause. (This is the same as the SQL_UNSEARCHABLE value in ODBC 2.*x*.)<br /><br /> SQL_PRED_CHAR if the column can be used in a **WHERE** clause, but only with the **LIKE** predicate. (This is the same as the SQL_LIKE_ONLY value in ODBC 2.*x*.)<br /><br /> SQL_PRED_BASIC if the column can be used in a **WHERE** clause with all the comparison operators except **LIKE** (comparison, quantified comparison, **BETWEEN**, **DISTINCT**, **IN**, **MATCH**, and **UNIQUE**). (This is the same as the SQL_ALL_EXCEPT_LIKE value in ODBC 2.*x*.)<br /><br /> SQL_SEARCHABLE if the column can be used in a **WHERE** clause with any comparison operator.|  
|UNSIGNED_ATTRIBUTE (ODBC 2.0)|10|Smallint|Whether the data type is unsigned:<br /><br /> SQL_TRUE if the data type is unsigned.<br /><br /> SQL_FALSE if the data type is signed.<br /><br /> NULL is returned if the attribute is not applicable to the data type or the data type is not numeric.|  
|FIXED_PREC_SCALE (ODBC 2.0)|11|Smallint not NULL|Whether the data type has predefined fixed precision and scale (which are data source-specific), such as a money data type:<br /><br /> SQL_TRUE if it has predefined fixed precision and scale.<br /><br /> SQL_FALSE if it does not have predefined fixed precision and scale.|  
|AUTO_UNIQUE_VALUE (ODBC 2.0)|12|Smallint|Whether the data type is autoincrementing:<br /><br /> SQL_TRUE if the data type is autoincrementing.<br /><br /> SQL_FALSE if the data type is not autoincrementing.<br /><br /> NULL is returned if the attribute is not applicable to the data type or the data type is not numeric.<br /><br /> An application can insert values into a column having this attribute, but typically cannot update the values in the column.<br /><br /> When an insert is made into an auto-increment column, a unique value is inserted into the column at insert time. The increment is not defined, but is data source-specific. An application should not assume that an auto-increment column starts at any particular point or increments by any particular value.|  
|LOCAL_TYPE_NAME (ODBC 2.0)|13|Varchar|Localized version of the data source-dependent name of the data type. NULL is returned if a localized name is not supported by the data source. This name is intended for display only, such as in dialog boxes.|  
|MINIMUM_SCALE (ODBC 2.0)|14|Smallint|The minimum scale of the data type on the data source. If a data type has a fixed scale, the MINIMUM_SCALE and MAXIMUM_SCALE columns both contain this value. For example, an SQL_TYPE_TIMESTAMP column might have a fixed scale for fractional seconds. NULL is returned where scale is not applicable. For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|MAXIMUM_SCALE (ODBC 2.0)|15|Smallint|The maximum scale of the data type on the data source. NULL is returned where scale is not applicable. If the maximum scale is not defined separately on the data source, but is instead defined to be the same as the maximum precision, this column contains the same value as the COLUMN_SIZE column. For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|SQL_DATA_TYPE (ODBC 3.0)|16|Smallint NOT NULL|The value of the SQL data type as it appears in the SQL_DESC_TYPE field of the descriptor. This column is the same as the DATA_TYPE column, except for interval and datetime data types.<br /><br /> For interval and datetime data types, the SQL_DATA_TYPE field in the result set will return SQL_INTERVAL or SQL_DATETIME, and the SQL_DATETIME_SUB field will return the subcode for the specific interval or datetime data type. (See [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).)|  
|SQL_DATETIME_SUB (ODBC 3.0)|17|Smallint|When the value of SQL_DATA_TYPE is SQL_DATETIME or SQL_INTERVAL, this column contains the datetime/interval subcode. For data types other than datetime and interval, this field is NULL.<br /><br /> For interval or datetime data types, the SQL_DATA_TYPE field in the result set will return SQL_INTERVAL or SQL_DATETIME, and the SQL_DATETIME_SUB field will return the subcode for the specific interval or datetime data type. (See [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).)|  
|NUM_PREC_RADIX (ODBC 3.0)|18|Integer|If the data type is an approximate numeric type, this column contains the value 2 to indicate that COLUMN_SIZE specifies a number of bits. For exact numeric types, this column contains the value 10 to indicate that COLUMN_SIZE specifies a number of decimal digits. Otherwise, this column is NULL.|  
|INTERVAL_PRECISION (ODBC 3.0)|19|Smallint|If the data type is an interval data type, then this column contains the value of the interval leading precision. (See [Interval Data Type Precision](../../../odbc/reference/appendixes/interval-data-type-precision.md) in Appendix D: Data Types.) Otherwise, this column is NULL.|  
  
 Attribute information can apply to data types or to specific columns in a result set. **SQLGetTypeInfo** returns information about attributes associated with data types; **SQLColAttribute** returns information about attributes associated with columns in a result set.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLColAttribute Function](../../../odbc/reference/syntax/sqlcolattribute-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Returning information about a driver or data source|[SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
