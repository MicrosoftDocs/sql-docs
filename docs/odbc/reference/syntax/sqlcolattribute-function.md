---
description: "SQLColAttribute Function"
title: "SQLColAttribute Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLColAttribute"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLColAttribute"
helpviewer_keywords: 
  - "SQLColAttribute function [ODBC]"
ms.assetid: 8c45c598-cb01-4789-a571-e93619a18ed9
author: David-Engel
ms.author: v-davidengel
---
# SQLColAttribute Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLColAttribute** returns descriptor information for a column in a result set. Descriptor information is returned as a character string, a descriptor-dependent value, or an integer value.  
  
> [!NOTE]  
>  For more information about what the Driver Manager maps this function to when an ODBC 3.*x* application is working with an ODBC 2.*x* driver, see [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md).  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLColAttribute (  
      SQLHSTMT        StatementHandle,  
      SQLUSMALLINT    ColumnNumber,  
      SQLUSMALLINT    FieldIdentifier,  
      SQLPOINTER      CharacterAttributePtr,  
      SQLSMALLINT     BufferLength,  
      SQLSMALLINT *   StringLengthPtr,  
      SQLLEN *        NumericAttributePtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *ColumnNumber*  
 [Input] The number of the record in the IRD from which the field value is to be retrieved. This argument corresponds to the column number of result data, ordered sequentially in increasing column order, starting at 1. Columns can be described in any order.  
  
 Column 0 can be specified in this argument, but all values except SQL_DESC_TYPE and SQL_DESC_OCTET_LENGTH will return undefined values.  
  
 *FieldIdentifier*  
 [Input] The descriptor handle. This handle defines which field in the IRD should be queried (for example, SQL_COLUMN_TABLE_NAME).  
  
 *CharacterAttributePtr*  
 [Output] Pointer to a buffer in which to return the value in the *FieldIdentifier* field of the *ColumnNumber* row of the IRD, if the field is a character string. Otherwise, the field is unused.  
  
 If *CharacterAttributePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *CharacterAttributePtr*.  
  
 *BufferLength*  
 [Input] If *FieldIdentifier* is an ODBC-defined field and *CharacterAttributePtr* points to a character string or binary buffer, this argument should be the length of \**CharacterAttributePtr*. If *FieldIdentifier* is an ODBC-defined field and \**CharacterAttribute*Ptr is an integer, this field is ignored. If the *\*CharacterAttributePtr* is a Unicode string (when calling **SQLColAttributeW**), the *BufferLength* argument must be an even number. If *FieldIdentifier* is a driver-defined field, the application indicates the nature of the field to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *CharacterAttributePtr* is a pointer to a pointer, *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *CharacterAttributePtr* is a pointer to a character string, the *BufferLength* is the length of the buffer.  
  
-   If *CharacterAttributePtr* is a pointer to a binary buffer, the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *CharacterAttributePtr* is a pointer to a fixed-length data type, *BufferLength* must be one of the following: SQL_IS_INTEGER, SQL_IS_UINTEGER, SQL_IS_SMALLINT, or SQL_IS_USMALLINT.  
  
 *StringLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of bytes (excluding the null-termination byte for character data) available to return in **CharacterAttributePtr*.  
  
 For character data, if the number of bytes available to return is greater than or equal to *BufferLength*, the descriptor information in \**CharacterAttributePtr* is truncated to *BufferLength* minus the length of a null-termination character and is null-terminated by the driver.  
  
 For all other types of data, the value of *BufferLength* is ignored and the driver assumes the size of **CharacterAttributePtr* is 32 bits.  
  
 *NumericAttributePtr*  
 [Output] Pointer to an integer buffer in which to return the value in the *FieldIdentifier* field of the *ColumnNumber* row of the IRD, if the field is a numeric descriptor type, such as SQL_DESC_COLUMN_LENGTH. Otherwise, the field is unused. Please note that some drivers may only write the lower 32-bit or 16-bit of a buffer and leave the higher-order bit unchanged. Therefore, applications should initialize the value to 0 before calling this function.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLColAttribute** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLColAttribute** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**CharacterAttributePtr* was not large enough to return the entire string value, so the string value was truncated. The length of the untruncated string value is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07005|Prepared statement not a *cursor-specification*|The statement associated with the *StatementHandle* did not return a result set and *FieldIdentifier* was not SQL_DESC_COUNT. There were no columns to describe.|  
|07009|Invalid descriptor index|(DM) The value specified for *ColumnNumber* was equal to 0, and the SQL_ATTR_USE_BOOKMARKS statement attribute was SQL_UB_OFF.<br /><br /> The value specified for the argument *ColumnNumber* was greater than the number of columns in the result set.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagField** from the diagnostic data structure describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This aynchronous function was still executing when SQLColAttribute was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) The function was called prior to calling **SQLPrepare**, **SQLExecDirect**, or a catalog function for the *StatementHandle*.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) *\*CharacterAttributePtr* is a character string, and *BufferLength* was less than 0 but not equal to SQL_NTS.|  
|HY091|Invalid descriptor field identifier|The value specified for the argument *FieldIdentifier* was not one of the defined values and was not an implementation-defined value.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Driver not capable|The value specified for the argument *FieldIdentifier* was not supported by the driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
 When called after **SQLPrepare** and before **SQLExecute**, **SQLColAttribute** can return any SQLSTATE that can be returned by **SQLPrepare** or **SQLExecute**, depending on when the data source evaluates the SQL statement associated with the *StatementHandle*.  
  
 For performance reasons, an application should not call **SQLColAttribute** before executing a statement.  
  
## Comments  
 For information about how applications use the information returned by **SQLColAttribute**, see [Result Set Metadata](../../../odbc/reference/develop-app/result-set-metadata.md).  
  
 **SQLColAttribute** returns information either in \**NumericAttributePtr* or in \**CharacterAttributePtr*. Integer information is returned in \**NumericAttributePtr* as a SQLLEN value; all other formats of information are returned in \**CharacterAttributePtr*. When information is returned in \**NumericAttributePtr*, the driver ignores *CharacterAttributePtr*, *BufferLength*, and *StringLengthPtr*. When information is returned in \**CharacterAttributePtr*, the driver ignores *NumericAttributePtr*.  
  
 **SQLColAttribute** returns values from the descriptor fields of the IRD. The function is called with a statement handle rather than a descriptor handle. The values returned by **SQLColAttribute** for the *FieldIdentifier* values listed later in this section can also be retrieved by calling **SQLGetDescField** with the appropriate IRD handle.  
  
 The currently defined descriptor fields, the version of ODBC in which they were introduced, and the arguments in which information is returned for them are shown later in this section; more descriptor types may be defined by drivers to take advantage of different data sources.  
  
 An ODBC 3.*x* driver must return a value for each of the descriptor fields. If a descriptor field does not apply to a driver or data source and unless otherwise stated, the driver returns 0 in \**StringLengthPtr* or an empty string in **CharacterAttributePtr*.  
  
## Backward Compatibility  
 The ODBC 3.*x* function **SQLColAttribute** replaces the deprecated ODBC 2.*x* function **SQLColAttributes**. When mapping **SQLColAttributes** to **SQLColAttribute** (when an ODBC 2.*x* application is working with an ODBC 3.*x* driver), or mapping **SQLColAttribute** to **SQLColAttributes** (when an ODBC 3.*x* application is working with an ODBC 2.*x* driver), the Driver Manager either passes the value of *FieldIdentifier* through, maps it to a new value, or returns an error, as follows:  
  
> [!NOTE]  
>  The prefix used in *FieldIdentifier* values in ODBC 3.*x* has been changed from that used in ODBC 2.*x*. The new prefix is "SQL_DESC"; the old prefix was "SQL_COLUMN".  
  
-   If the **#define** value of the ODBC 2.*x* *FieldIdentifier* is the same as the **#define** value of the ODBC 3.*x* *FieldIdentifier*, the value in the function call is just passed through.  
  
-   The **#define** values of the ODBC 2.*x* *FieldIdentifiers* SQL_COLUMN_LENGTH, SQL_COLUMN_PRECISION, and SQL_COLUMN_SCALE are different from the **#define** values of the ODBC 3.*x* *FieldIdentifiers* SQL_DESC_PRECISION, SQL_DESC_SCALE, and SQL_DESC_LENGTH. An ODBC 2.*x* driver need only support the ODBC 2.*x* values. An ODBC 3.*x* driver must support both "SQL_COLUMN" and "SQL_DESC" values for these three *FieldIdentifiers*. These values are different because precision, scale, and length are defined differently in ODBC 3.*x* than they were in ODBC 2.*x*. For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md).  
  
-   If the **#define** value of the ODBC 2.*x* *FieldIdentifier* is different from the **#define** value of the ODBC 3.*x* *FieldIdentifier*, as occurs with the COUNT, NAME, and NULLABLE values, the value in the function call is mapped to the corresponding value. For example, SQL_COLUMN_COUNT is mapped to SQL_DESC_COUNT, and SQL_DESC_COUNT is mapped to SQL_COLUMN_COUNT, depending on the direction of the mapping.  
  
-   If *FieldIdentifier* is a new value in ODBC 3.*x*, for which there was no corresponding value in ODBC 2.*x*, it will not be mapped when an ODBC 3.*x* application uses it in a call to **SQLColAttribute** in an ODBC 2.*x* driver, and the call will return SQLSTATE HY091 (Invalid descriptor field identifier).  
  
 The following table lists the descriptor types returned by **SQLColAttribute**. The type for *NumericAttributePtr* values is **SQLLEN \***.  
  
|*FieldIdentifier*|Information<br /><br /> returned in|Description|  
|-----------------------|---------------------------------|-----------------|  
|SQL_DESC_AUTO_UNIQUE_VALUE (ODBC 1.0)|*NumericAttributePtr*|SQL_TRUE if the column is an autoincrementing column.<br /><br /> SQL_FALSE if the column is not an autoincrementing column or is not numeric.<br /><br /> This field is valid for numeric data type columns only. An application can insert values into a row containing an autoincrement column, but typically cannot update values in the column.<br /><br /> When an insert is made into an autoincrement column, a unique value is inserted into the column at insert time. The increment is not defined, but is data source-specific. An application should not assume that an autoincrement column starts at any particular point or increments by any particular value.|  
|SQL_DESC_BASE_COLUMN_NAME (ODBC 3.0)|*CharacterAttributePtr*|The base column name for the result set column. If a base column name does not exist (as in the case of columns that are expressions), then this variable contains an empty string.<br /><br /> This information is returned from the SQL_DESC_BASE_COLUMN_NAME record field of the IRD, which is a read-only field.|  
|SQL_DESC_BASE_TABLE_NAME (ODBC 3.0)|*CharacterAttributePtr*|The name of the base table that contains the column. If the base table name cannot be defined or is not applicable, then this variable contains an empty string.<br /><br /> This information is returned from the SQL_DESC_BASE_TABLE_NAME record field of the IRD, which is a read-only field.|  
|SQL_DESC_CASE_SENSITIVE (ODBC 1.0)|*NumericAttributePtr*|SQL_TRUE if the column is treated as case-sensitive for collations and comparisons.<br /><br /> SQL_FALSE if the column is not treated as case-sensitive for collations and comparisons or is noncharacter.|  
|SQL_DESC_CATALOG_NAME (ODBC 2.0)|*CharacterAttributePtr*|The catalog of the table that contains the column. The returned value is implementation-defined if the column is an expression or if the column is part of a view. If the data source does not support catalogs or the catalog name cannot be determined, an empty string is returned. This VARCHAR record field is not limited to 128 characters.|  
|SQL_DESC_CONCISE_TYPE (ODBC 1.0)|*NumericAttributePtr*|The concise data type.<br /><br /> For the datetime and interval data types, this field returns the concise data type; for example, SQL_TYPE_TIME or SQL_INTERVAL_YEAR. (For more information, see [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md) in Appendix D: Data Types.)<br /><br /> This information is returned from the SQL_DESC_CONCISE_TYPE record field of the IRD.|  
|SQL_DESC_COUNT  (ODBC 1.0)|*NumericAttributePtr*|The number of columns available in the result set. This returns 0 if there are no columns in the result set. The value in the *ColumnNumber* argument is ignored.<br /><br /> This information is returned from the SQL_DESC_COUNT header field of the IRD.|  
|SQL_DESC_DISPLAY_SIZE (ODBC 1.0)|*NumericAttributePtr*|Maximum number of characters required to display data from the column. For more information about display size, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|SQL_DESC_FIXED_PREC_SCALE (ODBC 1.0)|*NumericAttributePtr*|SQL_TRUE if the column has a fixed precision and nonzero scale that are data source-specific.<br /><br /> SQL_FALSE if the column does not have a fixed precision and nonzero scale that are data source-specific.|  
|SQL_DESC_LABEL (ODBC 2.0)|*CharacterAttributePtr*|The column label or title. For example, a column named EmpName might be labeled Employee Name or might be labeled with an alias.<br /><br /> If a column does not have a label, the column name is returned. If the column is unlabeled and unnamed, an empty string is returned.|  
|SQL_DESC_LENGTH  (ODBC 3.0)|*NumericAttributePtr*|A numeric value that is either the maximum or actual character length of a character string or binary data type. It is the maximum character length for a fixed-length data type, or the actual character length for a variable-length data type. Its value always excludes the null-termination byte that ends the character string.<br /><br /> This information is returned from the SQL_DESC_LENGTH record field of the IRD.<br /><br /> For more information about length, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|SQL_DESC_LITERAL_PREFIX (ODBC 3.0)|*CharacterAttributePtr*|This VARCHAR(128) record field contains the character or characters that the driver recognizes as a prefix for a literal of this data type. This field contains an empty string for a data type for which a literal prefix is not applicable. For more information, see [Literal Prefixes and Suffixes](../../../odbc/reference/develop-app/literal-prefixes-and-suffixes.md).|  
|SQL_DESC_LITERAL_SUFFIX (ODBC 3.0)|*CharacterAttributePtr*|This VARCHAR(128) record field contains the character or characters that the driver recognizes as a suffix for a literal of this data type. This field contains an empty string for a data type for which a literal suffix is not applicable. For more information, see [Literal Prefixes and Suffixes](../../../odbc/reference/develop-app/literal-prefixes-and-suffixes.md).|  
|SQL_DESC_LOCAL_TYPE_NAME (ODBC 3.0)|*CharacterAttributePtr*|This VARCHAR(128) record field contains any localized (native language) name for the data type that may be different from the regular name of the data type. If there is no localized name, then an empty string is returned. This field is for display purposes only. The character set of the string is locale-dependent and is typically the default character set of the server.|  
|SQL_DESC_NAME (ODBC 3.0)|*CharacterAttributePtr*|The column alias, if it applies. If the column alias does not apply, the column name is returned. In either case, SQL_DESC_UNNAMED is set to SQL_NAMED. If there is no column name or a column alias, an empty string is returned and SQL_DESC_UNNAMED is set to SQL_UNNAMED.<br /><br /> This information is returned from the SQL_DESC_NAME record field of the IRD.|  
|SQL_DESC_NULLABLE (ODBC 3.0)|*NumericAttributePtr*|SQL_ NULLABLE if the column can have NULL values; SQL_NO_NULLS if the column does not have NULL values; or SQL_NULLABLE_UNKNOWN if it is not known whether the column accepts NULL values.<br /><br /> This information is returned from the SQL_DESC_NULLABLE record field of the IRD.|  
|SQL_DESC_NUM_PREC_RADIX (ODBC 3.0)|*NumericAttributePtr*|If the data type in the SQL_DESC_TYPE field is an approximate numeric data type, this SQLINTEGER field contains a value of 2 because the SQL_DESC_PRECISION field contains the number of bits. If the data type in the SQL_DESC_TYPE field is an exact numeric data type, this field contains a value of 10 because the SQL_DESC_PRECISION field contains the number of decimal digits. This field is set to 0 for all non-numeric data types.|  
|SQL_DESC_OCTET_LENGTH (ODBC 3.0)|*NumericAttributePtr*|The length, in bytes, of a character string or binary data type. For fixed-length character or binary types, this is the actual length in bytes. For variable-length character or binary types, this is the maximum length in bytes. This value does not include the null terminator.<br /><br /> This information is returned from the SQL_DESC_OCTET_LENGTH record field of the IRD.<br /><br /> For more information about length, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.|  
|SQL_DESC_PRECISION (ODBC 3.0)|*NumericAttributePtr*|A numeric value that for a numeric data type denotes the applicable precision. For data types SQL_TYPE_TIME, SQL_TYPE_TIMESTAMP, and all the interval data types that represent a time interval, its value is the applicable precision of the fractional seconds component.<br /><br /> This information is returned from the SQL_DESC_PRECISION record field of the IRD.|  
|SQL_DESC_SCALE (ODBC 3.0)|*NumericAttributePtr*|A numeric value that is the applicable scale for a numeric data type. For DECIMAL and NUMERIC data types, this is the defined scale. It is undefined for all other data types.<br /><br /> This information is returned from the SCALE record field of the IRD.|  
|SQL_DESC_SCHEMA_NAME (ODBC 2.0)|*CharacterAttributePtr*|The schema of the table that contains the column. The returned value is implementation-defined if the column is an expression or if the column is part of a view. If the data source does not support schemas or the schema name cannot be determined, an empty string is returned. This VARCHAR record field is not limited to 128 characters.|  
|SQL_DESC_SEARCHABLE (ODBC 1.0)|*NumericAttributePtr*|SQL_PRED_NONE if the column cannot be used in a WHERE clause. (This is the same as the SQL_UNSEARCHABLE value in ODBC 2.*x*.)<br /><br /> SQL_PRED_CHAR if the column can be used in a WHERE clause but only with the LIKE predicate. (This is the same as the SQL_LIKE_ONLY value in ODBC 2.*x*.)<br /><br /> SQL_PRED_BASIC if the column can be used in a WHERE clause with all the comparison operators except LIKE. (This is the same as the SQL_EXCEPT_LIKE value in ODBC 2.*x*.)<br /><br /> SQL_PRED_SEARCHABLE if the column can be used in a WHERE clause with any comparison operator.<br /><br /> Columns of type SQL_LONGVARCHAR and SQL_LONGVARBINARY usually return SQL_PRED_CHAR.|  
|SQL_DESC_TABLE_NAME (ODBC 2.0)|*CharacterAttributePtr*|The name of the table that contains the column. The returned value is implementation-defined if the column is an expression or if the column is part of a view.<br /><br /> If the table name cannot be determined, an empty string is returned.|  
|SQL_DESC_TYPE (ODBC 3.0)|*NumericAttributePtr*|A numeric value that specifies the SQL data type.<br /><br /> When *ColumnNumber* is equal to 0, SQL_BINARY is returned for variable-length bookmarks and SQL_INTEGER is returned for fixed-length bookmarks.<br /><br /> For the datetime and interval data types, this field returns the verbose data type: SQL_DATETIME or SQL_INTERVAL. (For more information, see [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md) in Appendix D: Data Types.<br /><br /> This information is returned from the SQL_DESC_TYPE record field of the IRD. **Note:**  To work against ODBC 2.*x* drivers, use SQL_DESC_CONCISE_TYPE instead.|  
|SQL_DESC_TYPE_NAME (ODBC 1.0)|*CharacterAttributePtr*|Data source-dependent data type name; for example, "CHAR", "VARCHAR", "MONEY", "LONG VARBINARY", or "CHAR ( ) FOR BIT DATA".<br /><br /> If the type is unknown, an empty string is returned.|  
|SQL_DESC_UNNAMED (ODBC 3.0)|*NumericAttributePtr*|SQL_NAMED or SQL_UNNAMED. If the SQL_DESC_NAME field of the IRD contains a column alias or a column name, SQL_NAMED is returned. If there is no column name or column alias, SQL_UNNAMED is returned.<br /><br /> This information is returned from the SQL_DESC_UNNAMED record field of the IRD.|  
|SQL_DESC_UNSIGNED (ODBC 1.0)|*NumericAttributePtr*|SQL_TRUE if the column is unsigned (or not numeric).<br /><br /> SQL_FALSE if the column is signed.|  
|SQL_DESC_UPDATABLE (ODBC 1.0)|*NumericAttributePtr*|Column is described by the values for the defined constants:<br /><br /> SQL_ATTR_READONLY SQL_ATTR_WRITE SQL_ATTR_READWRITE_UNKNOWN<br /><br /> SQL_DESC_UPDATABLE describes the updatability of the column in the result set, not the column in the base table. The updatability of the base column on which the result set column is based may be different from the value in this field. Whether a column is updatable can be based on the data type, user privileges, and the definition of the result set itself. If it is unclear whether a column is updatable, SQL_ATTR_READWRITE_UNKNOWN should be returned.|  
  
 **SQLColAttribute** is an extensible alternative to **SQLDescribeCol**. **SQLDescribeCol** returns a fixed set of descriptor information based on ANSI-89 SQL. **SQLColAttribute** allows access to the more extensive set of descriptor information available in ANSI SQL-92 and DBMS vendor extensions.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning information about a column in a result set|[SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching multiple rows of data|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
  
## Example  
 The following sample code does not free handles and connections. See [SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md), [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md), and [SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md) for code samples to free handles and statements.  
  
```cpp  
// SQLColAttibute.cpp  
// compile with: user32.lib odbc32.lib  
  
#define UNICODE  
  
#include <windows.h>  
#include <sqlext.h>  
#include <strsafe.h>  
  
struct DataBinding {  
   SQLSMALLINT TargetType;  
   SQLPOINTER TargetValuePtr;  
   SQLINTEGER BufferLength;  
   SQLLEN StrLen_or_Ind;  
};  
  
void printStatementResult(SQLHSTMT hstmt) {  
   int bufferSize = 1024, i;  
   SQLRETURN retCode;  
   SQLSMALLINT numColumn = 0, bufferLenUsed;
   
   retCode = SQLNumResultCols(hstmt, &numColumn);  
   
   SQLPOINTER* columnLabels = (SQLPOINTER *)malloc( numColumn * sizeof(SQLPOINTER*) );  
   struct DataBinding* columnData = (struct DataBinding*)malloc( numColumn * sizeof(struct DataBinding) );  
  
   printf( "Columns from that table:\n" );  
   for ( i = 0 ; i < numColumn ; i++ ) {  
      columnLabels[i] = (SQLPOINTER)malloc( bufferSize*sizeof(char) );  
  
      retCode = SQLColAttribute(hstmt, (SQLUSMALLINT)i + 1, SQL_DESC_LABEL, columnLabels[i], (SQLSMALLINT)bufferSize, &bufferLenUsed, NULL);  
      wprintf( L"Column %d: %s\n", i, (wchar_t*)columnLabels[i] );  
   }  
  
   // allocate memory for the binding  
   for ( i = 0 ; i < numColumn ; i++ ) {  
      columnData[i].TargetType = SQL_C_CHAR;  
      columnData[i].BufferLength = (bufferSize+1);  
      columnData[i].TargetValuePtr = malloc( sizeof(unsigned char)*columnData[i].BufferLength );  
   }  
  
   // setup the binding   
   for ( i = 0 ; i < numColumn ; i++ ) {  
      retCode = SQLBindCol(hstmt, (SQLUSMALLINT)i + 1, columnData[i].TargetType,   
         columnData[i].TargetValuePtr, columnData[i].BufferLength, &(columnData[i].StrLen_or_Ind));  
   }  
  
   printf( "Data from that table:\n" );  
   // fetch the data and print out the data  
   for ( retCode = SQLFetch(hstmt) ; retCode == SQL_SUCCESS || retCode == SQL_SUCCESS_WITH_INFO ; retCode = SQLFetch(hstmt) ) {  
      int j;  
      for ( j = 0 ; j < numColumn ; j++ )  
         wprintf( L"%s: %hs\n", columnLabels[j], columnData[j].TargetValuePtr );  
      printf( "\n" );  
   }  
   printf( "\n" );   
}  
  
int main() {  
   int bufferSize = 1024, i, count = 1, numCols = 5;  
   wchar_t firstTableName[1024], * dbName = (wchar_t *)malloc( sizeof(wchar_t)*bufferSize ), * userName = (wchar_t *)malloc( sizeof(wchar_t)*bufferSize );  
   HWND desktopHandle = GetDesktopWindow();   // desktop's window handle  
   SQLWCHAR connStrbuffer[1024];  
   SQLSMALLINT connStrBufferLen, bufferLen;  
   SQLRETURN retCode;  
  
   SQLHENV henv = NULL;   // Environment     
   SQLHDBC hdbc = NULL;   // Connection handle  
   SQLHSTMT hstmt = NULL;   // Statement handle  
  
   struct DataBinding* catalogResult = (struct DataBinding*) malloc( numCols * sizeof(struct DataBinding) );  
   SQLWCHAR* selectAllQuery = (SQLWCHAR *)malloc( sizeof(SQLWCHAR) * bufferSize );  
  
   // connect to database  
   retCode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
   retCode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLCHAR *)(void*)SQL_OV_ODBC3, -1);  
   retCode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
   retCode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)10, 0);  
   retCode = SQLDriverConnect(hdbc, desktopHandle, L"Driver={SQL Server}", SQL_NTS, connStrbuffer, 1025, &connStrBufferLen, SQL_DRIVER_PROMPT);  
   retCode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
   // display the database information  
   retCode = SQLGetInfo(hdbc, SQL_DATABASE_NAME, dbName, (SQLSMALLINT)bufferSize, (SQLSMALLINT *)&bufferLen);  
   retCode = SQLGetInfo(hdbc, SQL_USER_NAME, userName, (SQLSMALLINT)bufferSize, &bufferLen);  
  
   for ( i = 0 ; i < numCols ; i++ ) {  
      catalogResult[i].TargetType = SQL_C_CHAR;  
      catalogResult[i].BufferLength = (bufferSize + 1);  
      catalogResult[i].TargetValuePtr = malloc( sizeof(unsigned char)*catalogResult[i].BufferLength );  
   }  
  
   // Set up the binding. This can be used even if the statement is closed by closeStatementHandle  
   for ( i = 0 ; i < numCols ; i++ )  
      retCode = SQLBindCol(hstmt, (SQLUSMALLINT)i + 1, catalogResult[i].TargetType, catalogResult[i].TargetValuePtr, catalogResult[i].BufferLength, &(catalogResult[i].StrLen_or_Ind));  
  
   retCode = SQLTables( hstmt, (SQLWCHAR*)SQL_ALL_CATALOGS, SQL_NTS, L"", SQL_NTS, L"", SQL_NTS, L"", SQL_NTS );  
   retCode = SQLFreeStmt(hstmt, SQL_CLOSE);  
  
   retCode = SQLTables( hstmt, dbName, SQL_NTS, userName, SQL_NTS, L"%", SQL_NTS, L"TABLE", SQL_NTS );  
  
   for ( retCode = SQLFetch(hstmt) ; retCode == SQL_SUCCESS || retCode == SQL_SUCCESS_WITH_INFO ; retCode = SQLFetch(hstmt), ++count )  
      if ( count == 1 )  
         StringCchPrintfW( firstTableName, 1024, L"%hs", catalogResult[2].TargetValuePtr );  
   retCode = SQLFreeStmt(hstmt, SQL_CLOSE);  
  
   wprintf( L"Select all data from the first table (%s)\n", firstTableName );  
   StringCchPrintfW( selectAllQuery, bufferSize, L"SELECT * FROM %s", firstTableName );  
  
   retCode = SQLExecDirect(hstmt, selectAllQuery, SQL_NTS);  
   printStatementResult(hstmt);  
}  
```  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md)
