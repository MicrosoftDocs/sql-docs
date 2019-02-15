---
title: "SQLColumns Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLColumns"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLColumns"
helpviewer_keywords: 
  - "SQLColumns function [ODBC]"
ms.assetid: 4a3618b7-d2b8-43c6-a1fd-7a4e6fa8c7d0
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLColumns Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: Open Group  
  
 **Summary**  
 **SQLColumns** returns the list of column names in specified tables. The driver returns this information as a result set on the specified *StatementHandle*.  
  
## Syntax  
  
```  
  
SQLRETURN SQLColumns(  
     SQLHSTMT       StatementHandle,  
     SQLCHAR *      CatalogName,  
     SQLSMALLINT    NameLength1,  
     SQLCHAR *      SchemaName,  
     SQLSMALLINT    NameLength2,  
     SQLCHAR *      TableName,  
     SQLSMALLINT    NameLength3,  
     SQLCHAR *      ColumnName,  
     SQLSMALLINT    NameLength4);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CatalogName*  
 [Input] Catalog name. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") indicates those tables that do not have catalogs. *CatalogName* cannot contain a string search pattern.  
  
> [!NOTE]  
>  If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *CatalogName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *CatalogName* is an ordinary argument; it is treated literally, and its case is significant. For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).  
  
 *NameLength1*  
 [Input] Length in characters of **CatalogName*.  
  
 *SchemaName*  
 [Input] String search pattern for schema names. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") indicates those tables that do not have schemas.  
  
> [!NOTE]  
>  If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *SchemaName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *SchemaName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength2*  
 [Input] Length in characters of **SchemaName*.  
  
 *TableName*  
 [Input] String search pattern for table names.  
  
> [!NOTE]  
>  If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *TableName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *TableName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength3*  
 [Input] Length in characters of **TableName*.  
  
 *ColumnName*  
 [Input] String search pattern for column names.  
  
> [!NOTE]  
>  If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *ColumnName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *ColumnName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength4*  
 [Input] Length in characters of **ColumnName*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLColumns** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLColumns** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open on the *StatementHandle* but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back because of a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, the *CatalogName* argument was a null pointer, and the SQL_CATALOG_NAME *InfoType* returns that catalog names are supported.<br /><br /> (DM) The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, and the *SchemaName*, *TableName*, or *ColumnName* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLColumns** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value of one of the name length arguments was less than 0 but not equal to SQL_NTS.|  
|||The value of one of the name length arguments exceeded the maximum length value for the corresponding catalog or name. The maximum length of each catalog or name can be obtained by calling **SQLGetInfo** with the *InfoType* values. (See "Comments.")|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|A catalog name was specified, and the driver or data source does not support catalogs.<br /><br /> A schema name was specified, and the driver or data source does not support schemas.<br /><br /> A string search pattern was specified for the schema name, table name, or column name, and the data source does not support search patterns for one or more of those arguments.<br /><br /> The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 This function typically is used before statement execution to retrieve information about columns for a table or tables from the data source's catalog. **SQLColumns** can be used to retrieve data for all types of items returned by **SQLTables**. In addition to base tables, this may include (but is not limited to) views, synonyms, system tables, and so on. By contrast, the functions **SQLColAttribute** and **SQLDescribeCol** describe the columns in a result set and the function **SQLNumResultCols** returns the number of columns in a result set. For more information, see [Uses of Catalog Data](../../../odbc/reference/develop-app/uses-of-catalog-data.md).  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 **SQLColumns** returns the results as a standard result set, ordered by TABLE_CAT, TABLE_SCHEM, TABLE_NAME, and ORDINAL_POSITION.  
  
> [!NOTE]  
>  When an application works with an ODBC 2.*x* driver, no ORDINAL_POSITION column is returned in the result set. As a result, when working with ODBC 2.*x* drivers, the order of the columns in the column list returned by **SQLColumns** is not necessarily the same as the order of the columns returned when the application performs a SELECT statement on all columns in that table.  
  
> [!NOTE]  
>  **SQLColumns** might not return all columns. For example, a driver might not return information about pseudo-columns, such as Oracle ROWID. Applications can use any valid column, whether it is returned by **SQLColumns**.  
>   
>  Some columns that can be returned by **SQLStatistics** are not returned by **SQLColumns**. For example, **SQLColumns** does not return the columns in an index created over an expression or filter, such as SALARY + BENEFITS or DEPT = 0012.  
  
 The lengths of VARCHAR columns are not shown in the table; the actual lengths depend on the data source. To determine the actual lengths of the TABLE_CAT, TABLE_SCHEM, TABLE_NAME, and COLUMN_NAME columns, an application can call **SQLGetInfo** with the SQL_MAX_CATALOG_NAME_LEN, SQL_MAX_SCHEMA_NAME_LEN, SQL_MAX_TABLE_NAME_LEN, and SQL_MAX_COLUMN_NAME_LEN options.  
  
 The following columns have been renamed for ODBC 3.*x*. The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3.*x* column|  
|---------------------|-----------------------|  
|TABLE_QUALIFIER|TABLE_CAT|  
|TABLE_OWNER|TABLE_SCHEM|  
|PRECISION|COLUMN_SIZE|  
|LENGTH|BUFFER_LENGTH|  
|SCALE|DECIMAL_DIGITS|  
|RADIX|NUM_PREC_RADIX|  
  
 The following columns have been added to the result set returned by **SQLColumns** for ODBC 3.*x*:  
  
|||  
|-|-|  
|CHAR_OCTET_LENGTH|ORDINAL_POSITION|  
|COLUMN_DEF|SQL_DATA_TYPE|  
|IS_NULLABLE|SQL_DATETIME_SUB|  
  
 The following table lists the columns in the result set. Additional columns beyond column 18 (IS_NULLABLE) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set instead of specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
|Column name|Column<br /><br /> number|Data type|Comments|  
|-----------------|-----------------------|---------------|--------------|  
|TABLE_CAT (ODBC 1.0)|1|Varchar|Catalog name; NULL if not applicable to the data source. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have catalogs.|  
|TABLE_SCHEM (ODBC 1.0)|2|Varchar|Schema name; NULL if not applicable to the data source. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have schemas.|  
|TABLE_NAME (ODBC 1.0)|3|Varchar not NULL|Table name.|  
|COLUMN_NAME (ODBC 1.0)|4|Varchar not NULL|Column name. The driver returns an empty string for a column that does not have a name.|  
|DATA_TYPE (ODBC 1.0)|5|Smallint not NULL|SQL data type. This can be an ODBC SQL data type or a driver-specific SQL data type. For datetime and interval data types, this column returns the concise data type (such as SQL_TYPE_DATE or SQL_INTERVAL_YEAR_TO_MONTH, instead of the nonconcise data type such as SQL_DATETIME or SQL_INTERVAL). For a list of valid ODBC SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) in Appendix D: Data Types. For information about driver-specific SQL data types, see the driver's documentation.<br /><br /> The data types returned for ODBC 3.*x* and ODBC 2.*x* applications may be different. For more information, see [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).|  
|TYPE_NAME (ODBC 1.0)|6|Varchar not NULL|Data source-dependent data type name; for example, "CHAR", "VARCHAR", "MONEY", "LONG VARBINAR", or "CHAR ( ) FOR BIT DATA".|  
|COLUMN_SIZE (ODBC 1.0)|7|Integer|If DATA_TYPE is SQL_CHAR or SQL_VARCHAR, this column contains the maximum length in characters of the column. For datetime data types, this is the total number of characters required to display the value when it is converted to characters. For numeric data types, this is either the total number of digits or the total number of bits allowed in the column, according to the NUM_PREC_RADIX column. For interval data types, this is the number of characters in the character representation of the interval literal (as defined by the interval leading precision, see [Interval Data Type Length](../../../odbc/reference/appendixes/interval-data-type-length.md) in Appendix D: Data Types). For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|BUFFER_LENGTH (ODBC 1.0)|8|Integer|The length in bytes of data transferred on an SQLGetData, SQLFetch, or SQLFetchScroll operation if SQL_C_DEFAULT is specified. For numeric data, this size may differ from the size of the data stored on the data source. This value might differ from COLUMN_SIZE column for character data. For more information about length, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|DECIMAL_DIGITS (ODBC 1.0)|9|Smallint|The total number of significant digits to the right of the decimal point. For SQL_TYPE_TIME and SQL_TYPE_TIMESTAMP, this column contains the number of digits in the fractional seconds component. For the other data types, this is the decimal digits of the column on the data source. For interval data types that contain a time component, this column contains the number of digits to the right of the decimal point (fractional seconds). For interval data types that do not contain a time component, this column is 0. For more information about decimal digits, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types. NULL is returned for data types where DECIMAL_DIGITS is not applicable.|  
|NUM_PREC_RADIX (ODBC 1.0)|10|Smallint|For numeric data types, either 10 or 2. If it is 10, the values in COLUMN_SIZE and DECIMAL_DIGITS give the number of decimal digits allowed for the column. For example, a DECIMAL(12,5) column would return a NUM_PREC_RADIX of 10, a COLUMN_SIZE of 12, and a DECIMAL_DIGITS of 5; a FLOAT column could return a NUM_PREC_RADIX of 10, a COLUMN_SIZE of 15, and a DECIMAL_DIGITS of NULL.<br /><br /> If it is 2, the values in COLUMN_SIZE and DECIMAL_DIGITS give the number of bits allowed in the column. For example, a FLOAT column could return a RADIX of 2, a COLUMN_SIZE of 53, and a DECIMAL_DIGITS of NULL.<br /><br /> NULL is returned for data types where NUM_PREC_RADIX is not applicable.|  
|NULLABLE (ODBC 1.0)|11|Smallint not NULL|SQL_NO_NULLS if the column could not include NULL values.<br /><br /> SQL_NULLABLE if the column accepts NULL values.<br /><br /> SQL_NULLABLE_UNKNOWN if it is not known whether the column accepts NULL values.<br /><br /> The value returned for this column differs from the value returned for the IS_NULLABLE column. The NULLABLE column indicates with certainty that a column can accept NULLs, but cannot indicate with certainty that a column does not accept NULLs. The IS_NULLABLE column indicates with certainty that a column cannot accept NULLs, but cannot indicate with certainty that a column accepts NULLs.|  
|REMARKS (ODBC 1.0)|12|Varchar|A description of the column.|  
|COLUMN_DEF (ODBC 3.0)|13|Varchar|The default value of the column. The value in this column should be interpreted as a string if it is enclosed in quotation marks.<br /><br /> If NULL was specified as the default value, this column is the word NULL, not enclosed in quotation marks. If the default value cannot be represented without truncation, this column contains TRUNCATED, without enclosing single quotation marks. If no default value was specified, this column is NULL.<br /><br /> The value of COLUMN_DEF can be used in generating a new column definition, except when it contains the value TRUNCATED.|  
|SQL_DATA_TYPE (ODBC 3.0)|14|Smallint not NULL|SQL data type, as it appears in the SQL_DESC_TYPE record field in the IRD. This can be an ODBC SQL data type or a driver-specific SQL data type. This column is the same as the DATA_TYPE column, except for datetime and interval data types. This column returns the nonconcise data type (such as SQL_DATETIME or SQL_INTERVAL), instead of the concise data type (such as SQL_TYPE_DATE or SQL_INTERVAL_YEAR_TO_MONTH) for datetime and interval data types. If this column returns SQL_DATETIME or SQL_INTERVAL, the specific data type can be determined from the SQL_DATETIME_SUB column. For a list of valid ODBC SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) in Appendix D: Data Types. For information about driver-specific SQL data types, see the driver's documentation.<br /><br /> The data types returned for ODBC 3.*x* and ODBC 2.*x* applications may be different. For more information, see [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).|  
|SQL_DATETIME_SUB (ODBC 3.0)|15|Smallint|The subtype code for datetime and interval data types. For other data types, this column returns a NULL. For more information about datetime and interval subcodes, see "SQL_DESC_DATETIME_INTERVAL_CODE" in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).|  
|CHAR_OCTET_LENGTH (ODBC 3.0)|16|Integer|The maximum length in bytes of a character or binary data type column. For all other data types, this column returns a NULL.|  
|ORDINAL_POSITION (ODBC 3.0)|17|Integer not NULL|The ordinal position of the column in the table. The first column in the table is number 1.|  
|IS_NULLABLE (ODBC 3.0)|18|Varchar|"NO" if the column does not include NULLs.<br /><br /> "YES" if the column could include NULLs.<br /><br /> This column returns a zero-length string if nullability is unknown.<br /><br /> ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS cannot return an empty string.<br /><br /> The value returned for this column differs from the value returned for the NULLABLE column. (See the description of the NULLABLE column.)|  
  
## Code Example  
 In the following example, an application declares buffers for the result set returned by **SQLColumns**. It calls **SQLColumns** to return a result set that describes each column in the EMPLOYEE table. It then calls **SQLBindCol** to bind the columns in the result set to the buffers. Finally, the application fetches each row of data with **SQLFetch** and processes it.  
  
```  
// SQLColumns_Function.cpp  
// compile with: ODBC32.lib  
#include <windows.h>  
#include <sqlext.h>  
#define STR_LEN 128 + 1  
#define REM_LEN 254 + 1  
  
// Declare buffers for result set data  
SQLCHAR szSchema[STR_LEN];  
SQLCHAR szCatalog[STR_LEN];  
SQLCHAR szColumnName[STR_LEN];  
SQLCHAR szTableName[STR_LEN];  
SQLCHAR szTypeName[STR_LEN];  
SQLCHAR szRemarks[REM_LEN];  
SQLCHAR szColumnDefault[STR_LEN];  
SQLCHAR szIsNullable[STR_LEN];  
  
SQLINTEGER ColumnSize;  
SQLINTEGER BufferLength;  
SQLINTEGER CharOctetLength;  
SQLINTEGER OrdinalPosition;  
  
SQLSMALLINT DataType;  
SQLSMALLINT DecimalDigits;  
SQLSMALLINT NumPrecRadix;  
SQLSMALLINT Nullable;  
SQLSMALLINT SQLDataType;  
SQLSMALLINT DatetimeSubtypeCode;  
  
SQLHSTMT hstmt = NULL;  
  
// Declare buffers for bytes available to return  
SQLINTEGER cbCatalog;  
SQLINTEGER cbSchema;  
SQLINTEGER cbTableName;  
SQLINTEGER cbColumnName;  
SQLINTEGER cbDataType;  
SQLINTEGER cbTypeName;  
SQLINTEGER cbColumnSize;  
SQLLEN cbBufferLength;  
SQLINTEGER cbDecimalDigits;  
SQLINTEGER cbNumPrecRadix;  
SQLINTEGER cbNullable;  
SQLINTEGER cbRemarks;  
SQLINTEGER cbColumnDefault;  
SQLINTEGER cbSQLDataType;  
SQLINTEGER cbDatetimeSubtypeCode;  
SQLINTEGER cbCharOctetLength;  
SQLINTEGER cbOrdinalPosition;  
SQLINTEGER cbIsNullable;  
  
int main() {  
   SQLHENV henv;  
   SQLHDBC hdbc;  
   SQLHSTMT hstmt = 0;  
   SQLRETURN retcode;  
  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
   retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
   retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
   retcode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
   retcode = SQLConnect(hdbc, (SQLCHAR*) "Northwind", SQL_NTS, (SQLCHAR*) NULL, 0, NULL, 0);  
   retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
   retcode = SQLColumns(hstmt, NULL, 0, NULL, 0, (SQLCHAR*)"CUSTOMERS", SQL_NTS, NULL, 0);  
  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
      // Bind columns in result set to buffers  
      SQLBindCol(hstmt, 1, SQL_C_CHAR, szCatalog, STR_LEN,&cbCatalog);  
      SQLBindCol(hstmt, 2, SQL_C_CHAR, szSchema, STR_LEN, &cbSchema);  
      SQLBindCol(hstmt, 3, SQL_C_CHAR, szTableName, STR_LEN,&cbTableName);  
      SQLBindCol(hstmt, 4, SQL_C_CHAR, szColumnName, STR_LEN, &cbColumnName);  
      SQLBindCol(hstmt, 5, SQL_C_SSHORT, &DataType, 0, &cbDataType);  
      SQLBindCol(hstmt, 6, SQL_C_CHAR, szTypeName, STR_LEN, &cbTypeName);  
      SQLBindCol(hstmt, 7, SQL_C_SLONG, &ColumnSize, 0, &cbColumnSize);  
      SQLBindCol(hstmt, 8, SQL_C_SLONG, &BufferLength, 0, &cbBufferLength);  
      SQLBindCol(hstmt, 9, SQL_C_SSHORT, &DecimalDigits, 0, &cbDecimalDigits);  
      SQLBindCol(hstmt, 10, SQL_C_SSHORT, &NumPrecRadix, 0, &cbNumPrecRadix);  
      SQLBindCol(hstmt, 11, SQL_C_SSHORT, &Nullable, 0, &cbNullable);  
      SQLBindCol(hstmt, 12, SQL_C_CHAR, szRemarks, REM_LEN, &cbRemarks);  
      SQLBindCol(hstmt, 13, SQL_C_CHAR, szColumnDefault, STR_LEN, &cbColumnDefault);  
      SQLBindCol(hstmt, 14, SQL_C_SSHORT, &SQLDataType, 0, &cbSQLDataType);  
      SQLBindCol(hstmt, 15, SQL_C_SSHORT, &DatetimeSubtypeCode, 0, &cbDatetimeSubtypeCode);  
      SQLBindCol(hstmt, 16, SQL_C_SLONG, &CharOctetLength, 0, &cbCharOctetLength);  
      SQLBindCol(hstmt, 17, SQL_C_SLONG, &OrdinalPosition, 0, &cbOrdinalPosition);  
      SQLBindCol(hstmt, 18, SQL_C_CHAR, szIsNullable, STR_LEN, &cbIsNullable);  
  
      while (SQL_SUCCESS == retcode) {  
         retcode = SQLFetch(hstmt);  
         /*  
         if (retcode == SQL_ERROR || retcode == SQL_SUCCESS_WITH_INFO)  
            0;   // show_error();  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
            0;   // Process fetched data  
         else  
            break;  
        */  
      }  
   }  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning privileges for a column or columns|[SQLColumnPrivileges Function](../../../odbc/reference/syntax/sqlcolumnprivileges-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching multiple rows of data|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Returning columns that uniquely identify a row, or columns automatically updated by a transaction|[SQLSpecialColumns Function](../../../odbc/reference/syntax/sqlspecialcolumns-function.md)|  
|Returning table statistics and indexes|[SQLStatistics Function](../../../odbc/reference/syntax/sqlstatistics-function.md)|  
|Returning a list of tables in a data source|[SQLTables Function](../../../odbc/reference/syntax/sqltables-function.md)|  
|Returning privileges for a table or tables|[SQLTablePrivileges Function](../../../odbc/reference/syntax/sqltableprivileges-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
