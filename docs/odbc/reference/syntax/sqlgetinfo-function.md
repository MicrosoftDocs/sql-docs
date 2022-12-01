---
description: "SQLGetInfo Function"
title: "SQLGetInfo Function | Microsoft Docs"
ms.custom: ""
ms.date: "05/28/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetInfo"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetInfo"
helpviewer_keywords: 
  - "SQLGetInfo function [ODBC]"
ms.assetid: 49dceccc-d816-4ada-808c-4c6138dccb64
author: David-Engel
ms.author: v-davidengel
---

# SQLGetInfo Function

**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetInfo** returns general information about the driver and data source associated with a connection.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLGetInfo(  
     SQLHDBC         ConnectionHandle,  
     SQLUSMALLINT    InfoType,  
     SQLPOINTER      InfoValuePtr,  
     SQLSMALLINT     BufferLength,  
     SQLSMALLINT *   StringLengthPtr);  
```  
  
## Arguments  

 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *InfoType*  
 [Input] Type of information.  
  
 *InfoValuePtr*  
 [Output] Pointer to a buffer in which to return the information. Depending on the *InfoType* requested, the information returned will be one of the following: a null-terminated character string, an SQLUSMALLINT value, an SQLUINTEGER bitmask, an SQLUINTEGER flag, a SQLUINTEGER binary value, or a SQLULEN value.  
  
 If the *InfoType* argument is SQL_DRIVER_HDESC or SQL_DRIVER_HSTMT, the *InfoValuePtr* argument is both input and output. (See the SQL_DRIVER_HDESC or SQL_DRIVER_HSTMT descriptors later in this function description for more information.)  
  
 If *InfoValuePtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *InfoValuePtr*.  
  
 *BufferLength*  
 [Input] Length of the \**InfoValuePtr* buffer. If the value in *\*InfoValuePtr* is not a character string or if *InfoValuePtr* is a null pointer, the *BufferLength* argument is ignored. The driver assumes that the size of *\*InfoValuePtr* is SQLUSMALLINT or SQLUINTEGER, based on the *InfoType*. If *\*InfoValuePtr* is a Unicode string (when calling **SQLGetInfoW**), the *BufferLength* argument must be an even number; if not, SQLSTATE HY090 (Invalid string or buffer length) is returned.  
  
 *StringLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of bytes (excluding the null-termination character for character data) available to return in **InfoValuePtr*.  
  
 For character data, if the number of bytes available to return is greater than or equal to *BufferLength*, the information in \**InfoValuePtr* is truncated to *BufferLength* bytes minus the length of a null-termination character and is null-terminated by the driver.  
  
 For all other types of data, the value of *BufferLength* is ignored and the driver assumes the size of \**InfoValuePtr* is SQLUSMALLINT or SQLUINTEGER, depending on the *InfoType*.  
  
## Returns  

 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  

 When **SQLGetInfo** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values typically returned by **SQLGetInfo** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**InfoValuePtr* was not large enough to return all the requested information. Therefore, the information was truncated. The length of the requested information in its untruncated form is returned in **StringLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|(DM) The type of information requested in *InfoType* requires an open connection. Of the information types reserved by ODBC, only SQL_ODBC_VER can be returned without an open connection.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY024|Invalid attribute value|(DM) The *InfoType* argument was SQL_DRIVER_HSTMT, and the value pointed to by *InfoValuePtr* was not a valid statement handle.<br /><br /> (DM) The *InfoType* argument was SQL_DRIVER_HDESC, and the value pointed to by *InfoValuePtr* was not a valid descriptor handle.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *BufferLength* was less than 0.<br /><br /> (DM) The value specified for *BufferLength* was an odd number, and *\*InfoValuePtr* was of a Unicode data type.|  
|HY096|Information type out of range|The value specified for the argument *InfoType* was not valid for the version of ODBC supported by the driver.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](sqlendtran-function.md).|  
|HYC00|Optional field not implemented|The value specified for the argument *InfoType* was a driver-specific value that is not supported by the driver.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver that corresponds to the *ConnectionHandle* does not support the function.|  
  
## Comments  

 The currently defined information types are shown in "Information Types," later in this section; it is expected that more will be defined to take advantage of different data sources. A range of information types is reserved by ODBC; driver developers must reserve values for their own driver-specific use from Open Group. **SQLGetInfo** performs no Unicode conversion or *thunking* (see [Appendix A: ODBC Error Codes](../appendixes/appendix-a-odbc-error-codes.md) of the *ODBC Programmer's Reference*) for driver-defined *InfoTypes*. For more information, see [Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes](../develop-app/driver-specific-data-types-descriptor-information-diagnostic.md). The format of the information returned in \**InfoValuePtr* depends on the *InfoType* requested. **SQLGetInfo** will return information in one of five different formats:  
  
- A null-terminated character string  
  
- An SQLUSMALLINT value  
  
- An SQLUINTEGER bitmask  
  
- An SQLUINTEGER value  
  
- A SQLUINTEGER binary value  
  
 The format of each of the following information types is noted in the type's description. The application must cast the value returned in **InfoValuePtr* accordingly. For an example of how an application could retrieve data from a SQLUINTEGER bitmask, see "Code Example."  
  
 A driver must return a value for each information type that is defined in the following tables. If an information type does not apply to the driver or data source, the driver returns one of the values listed in the following table.  

|Information type|Value|
|-|-|
|Character string ("Y" or "N")|"N"|
|Character string (not "Y" or "N")|Empty string|
|SQLUSMALLINT|0|
|SQLUINTEGER bitmask or SQLUINTEGER binary value|0L|
  
 For example, if a data source does not support procedures, **SQLGetInfo** returns the values listed in the following table for the values of *InfoType* that are related to procedures.  

|InfoType|Value|
|-|-|
|SQL_PROCEDURES|"N"|
|SQL_ACCESSIBLE_PROCEDURES|"N"|
|SQL_MAX_PROCEDURE_NAME_LEN|0|
|SQL_PROCEDURE_TERM|Empty string|
  
 **SQLGetInfo** returns SQLSTATE HY096 (Invalid argument value) for values of *InfoType* that are in the range of information types reserved for use by ODBC but are not defined by the version of ODBC supported by the driver. To determine what version of ODBC a driver complies with, an application calls **SQLGetInfo** with the SQL_DRIVER_ODBC_VER information type. **SQLGetInfo** returns SQLSTATE HYC00 (Optional feature not implemented) for values of *InfoType* that are in the range of information types reserved for driver-specific use but are not supported by the driver.  
  
 All calls to **SQLGetInfo** require an open connection, except when the *InfoType* is SQL_ODBC_VER, which returns the version of the Driver Manager.  
  
## Information Types  

 This section lists the information types supported by **SQLGetInfo**. Information types are grouped categorically and listed alphabetically. Information types that were added or renamed for ODBC 3*.x* are also listed.  
  
## Driver Information  

 The following values of the *InfoType* argument return information about the ODBC driver, such as the number of active statements, the data source name, and the interface standards compliance level:  

:::row:::
    :::column:::
        SQL_ACTIVE_ENVIRONMENTS  
        SQL_ASYNC_DBC_FUNCTIONS  
        SQL_ASYNC_MODE  
        SQL_ASYNC_NOTIFICATION  
        SQL_BATCH_ROW_COUNT  
        SQL_BATCH_SUPPORT  
        SQL_DATA_SOURCE_NAME  
        SQL_DRIVER_AWARE_POOLING_SUPPORTED  
        SQL_DRIVER_HDBC  
        SQL_DRIVER_HDESC  
        SQL_DRIVER_HENV  
        SQL_DRIVER_HLIB  
        SQL_DRIVER_HSTMT  
        SQL_DRIVER_NAME  
        SQL_DRIVER_ODBC_VER  
        SQL_DRIVER_VER  
        SQL_DYNAMIC_CURSOR_ATTRIBUTES1  
        SQL_DYNAMIC_CURSOR_ATTRIBUTES2  
        SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1  
    :::column-end:::
    :::column:::
        SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2  
        SQL_FILE_USAGE  
        SQL_GETDATA_EXTENSIONS  
        SQL_INFO_SCHEMA_VIEWS  
        SQL_KEYSET_CURSOR_ATTRIBUTES1  
        SQL_KEYSET_CURSOR_ATTRIBUTES2  
        SQL_MAX_ASYNC_CONCURRENT_STATEMENTS  
        SQL_MAX_CONCURRENT_ACTIVITIES  
        SQL_MAX_DRIVER_CONNECTIONS  
        SQL_ODBC_INTERFACE_CONFORMANCE  
        SQL_ODBC_STANDARD_CLI_CONFORMANCE  
        SQL_ODBC_VER  
        SQL_PARAM_ARRAY_ROW_COUNTS  
        SQL_PARAM_ARRAY_SELECTS  
        SQL_ROW_UPDATES  
        SQL_SEARCH_PATTERN_ESCAPE  
        SQL_SERVER_NAME  
        SQL_STATIC_CURSOR_ATTRIBUTES1  
        SQL_STATIC_CURSOR_ATTRIBUTES2  
    :::column-end:::
:::row-end:::

> [!NOTE]  
> When implementing **SQLGetInfo**, a driver can improve performance by minimizing the number of times that information is sent or requested from the server.  
  
## DBMS Product Information  

 The following values of the *InfoType* argument return information about the DBMS product, such as the DBMS name and version:  

:::row:::
    :::column:::
        SQL_DATABASE_NAME  
        SQL_DBMS_NAME  
    :::column-end:::
    :::column:::
        SQL_DBMS_VER  
    :::column-end:::
:::row-end:::

## Data Source Information  

 The following values of the *InfoType* argument return information about the data source, such as cursor characteristics and transaction capabilities:  

:::row:::
    :::column:::
        SQL_ACCESSIBLE_PROCEDURES  
        SQL_ACCESSIBLE_TABLES  
        SQL_BOOKMARK_PERSISTENCE  
        SQL_CATALOG_TERM  
        SQL_COLLATION_SEQ  
        SQL_CONCAT_NULL_BEHAVIOR  
        SQL_CURSOR_COMMIT_BEHAVIOR  
        SQL_CURSOR_ROLLBACK_BEHAVIOR  
        SQL_CURSOR_SENSITIVITY  
        SQL_DATA_SOURCE_READ_ONLY  
        SQL_DEFAULT_TXN_ISOLATION  
        SQL_DESCRIBE_PARAMETER  
    :::column-end:::
    :::column:::
        SQL_MULT_RESULT_SETS  
        SQL_MULTIPLE_ACTIVE_TXN  
        SQL_NEED_LONG_DATA_LEN  
        SQL_NULL_COLLATION  
        SQL_PROCEDURE_TERM  
        SQL_SCHEMA_TERM  
        SQL_SCROLL_OPTIONS  
        SQL_TABLE_TERM  
        SQL_TXN_CAPABLE  
        SQL_TXN_ISOLATION_OPTION  
        SQL_USER_NAME  
    :::column-end:::
:::row-end:::

## Supported SQL  

 The following values of the *InfoType* argument return information about the SQL statements supported by the data source. The SQL syntax of each feature described by these information types is the SQL-92 syntax. These information types do not exhaustively describe the entire SQL-92 grammar. Instead, they describe those parts of the grammar for which data sources typically offer different levels of support. Specifically, most of the DDL statements in SQL-92 are covered.  
  
 Applications should determine the general level of supported grammar from the SQL_SQL_CONFORMANCE information type and use the other information types to determine variations from the stated standards compliance level.  

:::row:::
    :::column:::
        SQL_AGGREGATE_FUNCTIONS  
        SQL_ALTER_DOMAIN  
        SQL_ALTER_SCHEMA  
        SQL_ALTER_TABLE  
        SQL_ANSI_SQL_DATETIME_LITERALS  
        SQL_CATALOG_LOCATION  
        SQL_CATALOG_NAME  
        SQL_CATALOG_NAME_SEPARATOR  
        SQL_CATALOG_USAGE  
        SQL_COLUMN_ALIAS  
        SQL_CORRELATION_NAME  
        SQL_CREATE_ASSERTION  
        SQL_CREATE_CHARACTER_SET  
        SQL_CREATE_COLLATION  
        SQL_CREATE_DOMAIN  
        SQL_CREATE_SCHEMA  
        SQL_CREATE_TABLE  
        SQL_CREATE_TRANSLATION  
        SQL_DDL_INDEX  
        SQL_DROP_ASSERTION  
        SQL_DROP_CHARACTER_SET  
        SQL_DROP_COLLATION  
        SQL_DROP_DOMAIN  
        SQL_DROP_SCHEMA  
    :::column-end:::
    :::column:::
        SQL_DROP_TABLE  
        SQL_DROP_TRANSLATION  
        SQL_DROP_VIEW  
        SQL_EXPRESSIONS_IN_ORDERBY  
        SQL_GROUP_BY  
        SQL_IDENTIFIER_CASE  
        SQL_IDENTIFIER_QUOTE_CHAR  
        SQL_INDEX_KEYWORDS  
        SQL_INSERT_STATEMENT  
        SQL_INTEGRITY  
        SQL_KEYWORDS  
        SQL_LIKE_ESCAPE_CLAUSE  
        SQL_NON_NULLABLE_COLUMNS  
        SQL_OJ_CAPABILITIES  
        SQL_ORDER_BY_COLUMNS_IN_SELECT  
        SQL_OUTER_JOINS  
        SQL_PROCEDURES  
        SQL_QUOTED_IDENTIFIER_CASE  
        SQL_SCHEMA_USAGE  
        SQL_SPECIAL_CHARACTERS  
        SQL_SQL_CONFORMANCE  
        SQL_SUBQUERIES  
        SQL_UNION  
    :::column-end:::
:::row-end:::

## SQL Limits  

 The following values of the *InfoType* argument return information about the limits applied to identifiers and clauses in SQL statements, such as the maximum lengths of identifiers and the maximum number of columns in a select list. Limitations can be imposed by either the driver or the data source.  

:::row:::
    :::column:::
        SQL_MAX_BINARY_LITERAL_LEN  
        SQL_MAX_CATALOG_NAME_LEN  
        SQL_MAX_CHAR_LITERAL_LEN  
        SQL_MAX_COLUMN_NAME_LEN  
        SQL_MAX_COLUMNS_IN_GROUP_BY  
        SQL_MAX_COLUMNS_IN_INDEX  
        SQL_MAX_COLUMNS_IN_ORDER_BY  
        SQL_MAX_COLUMNS_IN_SELECT  
        SQL_MAX_COLUMNS_IN_TABLE  
        SQL_MAX_CURSOR_NAME_LEN  
    :::column-end:::
    :::column:::
        SQL_MAX_IDENTIFIER_LEN  
        SQL_MAX_INDEX_SIZE  
        SQL_MAX_PROCEDURE_NAME_LEN  
        SQL_MAX_ROW_SIZE  
        SQL_MAX_ROW_SIZE_INCLUDES_LONG  
        SQL_MAX_SCHEMA_NAME_LEN  
        SQL_MAX_STATEMENT_LEN  
        SQL_MAX_TABLE_NAME_LEN  
        SQL_MAX_TABLES_IN_SELECT  
        SQL_MAX_USER_NAME_LEN  
    :::column-end:::
:::row-end:::

## Scalar Function Information  

 The following values of the *InfoType* argument return information about the scalar functions supported by the data source and the driver. For more information about scalar functions, see [Appendix E: Scalar Functions](../appendixes/appendix-e-scalar-functions.md).  

:::row:::
    :::column:::
        SQL_CONVERT_FUNCTIONS  
        SQL_NUMERIC_FUNCTIONS  
        SQL_STRING_FUNCTIONS  
        SQL_SYSTEM_FUNCTIONS  
    :::column-end:::
    :::column:::
        SQL_TIMEDATE_ADD_INTERVALS  
        SQL_TIMEDATE_DIFF_INTERVALS  
        SQL_TIMEDATE_FUNCTIONS  
    :::column-end:::
:::row-end:::

## Conversion Information  

 The following values of the *InfoType* argument return a list of the SQL data types to which the data source can convert the specified SQL data type with the **CONVERT** scalar function:  

:::row:::
    :::column:::
        SQL_CONVERT_BIGINT  
        SQL_CONVERT_BINARY  
        SQL_CONVERT_BIT  
        SQL_CONVERT_CHAR  
        SQL_CONVERT_DATE  
        SQL_CONVERT_DECIMAL  
        SQL_CONVERT_DOUBLE  
        SQL_CONVERT_FLOAT  
        SQL_CONVERT_INTEGER  
        SQL_CONVERT_INTERVAL_DAY_TIME  
        SQL_CONVERT_INTERVAL_YEAR_MONTH  
    :::column-end:::
    :::column:::
        SQL_CONVERT_LONGVARBINARY  
        SQL_CONVERT_LONGVARCHAR  
        SQL_CONVERT_NUMERIC  
        SQL_CONVERT_REAL  
        SQL_CONVERT_SMALLINT  
        SQL_CONVERT_TIME  
        SQL_CONVERT_TIMESTAMP  
        SQL_CONVERT_TINYINT  
        SQL_CONVERT_VARBINARY  
        SQL_CONVERT_VARCHAR  
    :::column-end:::
:::row-end:::

## Information Types Added for ODBC 3.x  

 The following values of the *InfoType* argument have been added for ODBC 3.x:  

:::row:::
    :::column:::
        SQL_ACTIVE_ENVIRONMENTS  
        SQL_AGGREGATE_FUNCTIONS  
        SQL_ALTER_DOMAIN  
        SQL_ALTER_SCHEMA  
        SQL_ANSI_SQL_DATETIME_LITERALS  
        SQL_ASYNC_DBC_FUNCTIONS  
        SQL_ASYNC_MODE  
        SQL_ASYNC_NOTIFICATION  
        SQL_BATCH_ROW_COUNT  
        SQL_BATCH_SUPPORT  
        SQL_CATALOG_NAME  
        SQL_COLLATION_SEQ  
        SQL_CONVERT_INTERVAL_DAY_TIME  
        SQL_CONVERT_INTERVAL_YEAR_MONTH  
        SQL_CREATE_ASSERTION  
        SQL_CREATE_CHARACTER_SET  
        SQL_CREATE_COLLATION  
        SQL_CREATE_DOMAIN  
        SQL_CREATE_SCHEMA  
        SQL_CREATE_TABLE  
        SQL_CREATE_TRANSLATION  
        SQL_CURSOR_SENSITIVITY  
        SQL_DDL_INDEX  
        SQL_DESCRIBE_PARAMETER  
        SQL_DM_VER  
    :::column-end:::
    :::column:::
        SQL_DRIVER_AWARE_POOLING_SUPPORTED  
        SQL_DRIVER_HDESC  
        SQL_DROP_ASSERTION  
        SQL_DROP_CHARACTER_SET  
        SQL_DROP_COLLATION  
        SQL_DROP_DOMAIN  
        SQL_DROP_SCHEMA  
        SQL_DROP_TABLE  
        SQL_DROP_TRANSLATION  
        SQL_DROP_VIEW  
        SQL_DYNAMIC_CURSOR_ATTRIBUTES1  
        SQL_DYNAMIC_CURSOR_ATTRIBUTES2  
        SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1  
        SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2  
        SQL_INFO_SCHEMA_VIEWS  
        SQL_INSERT_STATEMENT  
        SQL_KEYSET_CURSOR_ATTRIBUTES1  
        SQL_KEYSET_CURSOR_ATTRIBUTES2  
        SQL_MAX_ASYNC_CONCURRENT_STATEMENTS  
        SQL_MAX_IDENTIFIER_LEN  
        SQL_PARAM_ARRAY_ROW_COUNTS  
        SQL_PARAM_ARRAY_SELECTS  
        SQL_STATIC_CURSOR_ATTRIBUTES1  
        SQL_STATIC_CURSOR_ATTRIBUTES2  
        SQL_XOPEN_CLI_YEAR  
    :::column-end:::
:::row-end:::

## Information Types Renamed for ODBC 3.x  

 The following values of the *InfoType* argument have been renamed for ODBC 3.x.  

|Old Name|New Name|  
|-|-|  
|SQL_ACTIVE_CONNECTIONS|SQL_MAX_DRIVER_CONNECTIONS|
|SQL_ACTIVE_STATEMENTS|SQL_MAX_CONCURRENT_ACTIVITIES|
|SQL_MAX_OWNER_NAME_LEN|SQL_MAX_SCHEMA_NAME_LEN|
|SQL_MAX_QUALIFIER_NAME_LEN|SQL_MAX_CATALOG_NAME_LEN|
|SQL_ODBC_SQL_OPT_IEF|SQL_INTEGRITY|
|SQL_OWNER_TERM|SQL_SCHEMA_TERM|
|SQL_OWNER_USAGE|SQL_SCHEMA_USAGE|
|SQL_QUALIFIER_LOCATION|SQL_CATALOG_LOCATION|
|SQL_QUALIFIER_NAME_SEPARATOR|SQL_CATALOG_NAME_SEPARATOR|
|SQL_QUALIFIER_TERM|SQL_CATALOG_TERM|
|SQL_QUALIFIER_USAGE|SQL_CATALOG_USAGE|
  
## Information Types Deprecated in ODBC 3.x  

 The following values of the *InfoType* argument have been deprecated in ODBC 3.x. ODBC 3.x drivers must continue to support these information types for backward compatibility with ODBC 2.x applications. (For more information about these types, see [SQLGetInfo Support](../appendixes/sqlgetinfo-support.md) in Appendix G: Driver Guidelines for Backward Compatibility.)  

:::row:::
    :::column:::
        SQL_FETCH_DIRECTION  
        SQL_LOCK_TYPES  
        SQL_ODBC_API_CONFORMANCE  
        SQL_ODBC_SQL_CONFORMANCE  
    :::column-end:::
    :::column:::
        SQL_POS_OPERATIONS  
        SQL_POSITIONED_STATEMENTS  
        SQL_SCROLL_CONCURRENCY  
        SQL_STATIC_SENSITIVITY  
    :::column-end:::
:::row-end:::

## Information Type Descriptions  

The following table alphabetically lists each information type, the version of ODBC in which it was introduced, and its description.  
  
|Information Type|ODBC Version|Description|
|-|-|-|
|SQL_ACCESSIBLE_PROCEDURES|1.0|A character string: "Y" if the user can execute all procedures returned by **SQLProcedures**; "N" if there may be procedures returned that the user cannot execute.|
|SQL_ACCESSIBLE_TABLES|1.0|A character string: "Y" if the user is guaranteed **SELECT** privileges to all tables returned by **SQLTables**; "N" if there may be tables returned that the user cannot access.|
|SQL_ACTIVE_ENVIRONMENTS|3.0|An SQLUSMALLINT value that specifies the maximum number of active environments that the driver can support. If there is no specified limit or the limit is unknown, this value is set to zero.|
|SQL_AGGREGATE_FUNCTIONS|3.0|An SQLUINTEGER bitmask enumerating support for aggregation functions:<br/>SQL_AF_ALL<br/>SQL_AF_AVG<br/>SQL_AF_COUNT<br/>SQL_AF_DISTINCT<br/>SQL_AF_MAX<br/>SQL_AF_MIN<br/>SQL_AF_SUM<br/><br/>An SQL-92 Entry level-conformant driver will always return all of these options as supported.|
|SQL_ALTER_DOMAIN|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **ALTER DOMAIN** statement, as defined in SQL-92, supported by the data source. An SQL-92 Full level-compliant driver will always return all the bitmasks. A return value of "0" means that the **ALTER DOMAIN** statement is not supported.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_AD_ADD_DOMAIN_CONSTRAINT = Adding a domain constraint is supported (Full level)<br/>SQL_AD_ADD_DOMAIN_DEFAULT = \<alter domain> \<set domain default clause> is supported (Full level)<br/>SQL_AD_CONSTRAINT_NAME_DEFINITION = \<constraint name definition clause> is supported for naming domain constraint (Intermediate level)<br/>SQL_AD_DROP_DOMAIN_CONSTRAINT = \<drop domain constraint clause> is supported (Full level)<br/>SQL_AD_DROP_DOMAIN_DEFAULT = \<alter domain> \<drop domain default clause> is supported (Full level)<br/><br/>The following bits specify the supported \<constraint attributes> if \<add domain constraint> is supported (the SQL_AD_ADD_DOMAIN_CONSTRAINT bit is set):<br/>SQL_AD_ADD_CONSTRAINT_DEFERRABLE (Full level)<br/>SQL_AD_ADD_CONSTRAINT_NON_DEFERRABLE (Full level)<br/>SQL_AD_ADD_CONSTRAINT_INITIALLY_DEFERRED (Full level)<br/>SQL_AD_ADD_CONSTRAINT_INITIALLY_IMMEDIATE (Full level)|
|SQL_ALTER_TABLE|2.0|An SQLUINTEGER bitmask enumerating the clauses in the **ALTER TABLE** statement supported by the data source.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_AT_ADD_COLUMN_COLLATION = \<add column> clause is supported, with facility to specify column collation (Full level) (ODBC 3.0)<br/>SQL_AT_ADD_COLUMN_DEFAULT = \<add column> clause is supported, with facility to specify column defaults (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_ADD_COLUMN_SINGLE = \<add column> is supported (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_ADD_CONSTRAINT = \<add column> clause is supported, with facility to specify column constraints (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_ADD_TABLE_CONSTRAINT = \<add table constraint> clause is supported (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_CONSTRAINT_NAME_DEFINITION = \<constraint name definition> is supported for naming column and table constraints (Intermediate level) (ODBC 3.0)<br/>SQL_AT_DROP_COLUMN_CASCADE = \<drop column> CASCADE is supported (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_DROP_COLUMN_DEFAULT = \<alter column> \<drop column default clause> is supported (Intermediate level) (ODBC 3.0)<br/>SQL_AT_DROP_COLUMN_RESTRICT = \<drop column> RESTRICT is supported (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_DROP_TABLE_CONSTRAINT_CASCADE (ODBC 3.0)<br/>SQL_AT_DROP_TABLE_CONSTRAINT_RESTRICT = \<drop column> RESTRICT is supported (FIPS Transitional level) (ODBC 3.0)<br/>SQL_AT_SET_COLUMN_DEFAULT = \<alter column> \<set column default clause> is supported (Intermediate level) (ODBC 3.0)<br/><br/>The following bits specify the support \<constraint attributes> if specifying column or table constraints is supported (the SQL_AT_ADD_CONSTRAINT bit is set):<br/>SQL_AT_CONSTRAINT_INITIALLY_DEFERRED (Full level) (ODBC 3.0)<br/>SQL_AT_CONSTRAINT_INITIALLY_IMMEDIATE (Full level) (ODBC 3.0)<br/>SQL_AT_CONSTRAINT_DEFERRABLE (Full level) (ODBC 3.0)<br/>SQL_AT_CONSTRAINT_NON_DEFERRABLE (Full level) (ODBC 3.0)|
|SQL_ASYNC_DBC_FUNCTIONS|3.8|A SQLUINTEGER value that indicates if the driver can execute functions asynchronously on the connection handle.<br/><br/>SQL_ASYNC_DBC_CAPABLE = The driver can execute connection functions asynchronously.<br/>SQL_ASYNC_DBC_NOT_CAPABLE = The driver cannot execute connection functions asynchronously.|
|SQL_ASYNC_MODE|3.0|A SQLUINTEGER value that indicates the level of asynchronous support in the driver:<br/><br/>SQL_AM_CONNECTION = Connection level asynchronous execution is supported. Either all statement handles associated with a given connection handle are in asynchronous mode or all are in synchronous mode. A statement handle on a connection cannot be in asynchronous mode while another statement handle on the same connection is in synchronous mode, and vice versa.<br/>SQL_AM_STATEMENT = Statement level asynchronous execution is supported. Some statement handles associated with a connection handle can be in asynchronous mode, while other statement handles on the same connection are in synchronous mode.<br/>SQL_AM_NONE = Asynchronous mode is not supported.|
|SQL_ASYNC_NOTIFICATION|3.8|A SQLUINTEGER value that indicates if the driver supports asynchronous notification:<br/><br/>SQL_ASYNC_NOTIFICATION_CAPABLE = Asynchronous execution notification is supported by the driver.<br/>SQL_ASYNC_NOTIFICATION_NOT_CAPABLE = Asynchronous execution notification is not supported by the driver.<br/><br/>There are two categories of ODBC asynchronous operations: connection level asynchronous operations and statement level asynchronous operations.  If a driver returns SQL_ASYNC_NOTIFICATION_CAPABLE, it must support notification for all APIs that it can execute asynchronously.|
|SQL_BATCH_ROW_COUNT|3.0|A SQLUINTEGER bitmask that enumerates the behavior of the driver with respect to the availability of row counts. The following bitmasks are used together with the information type:<br/><br/>SQL_BRC_ROLLED_UP = Row counts for consecutive INSERT, DELETE, or UPDATE statements are rolled up into one. If this bit is not set, row counts are available for each statement.<br/>SQL_BRC_PROCEDURES = Row counts, if any, are available when a batch is executed in a stored procedure. If row counts are available, they can be rolled up or individually available, depending on the SQL_BRC_ROLLED_UP bit.<br/>SQL_BRC_EXPLICIT = Row counts, if any, are available when a batch is executed directly by calling **SQLExecute** or **SQLExecDirect**. If row counts are available, they can be rolled up or individually available, depending on the SQL_BRC_ROLLED_UP bit.|
|SQL_BATCH_SUPPORT|3.0|An SQLUINTEGER bitmask enumerating the driver's support for batches. The following bitmasks are used to determine which level is supported:<br/><br/>SQL_BS_SELECT_EXPLICIT = The driver supports explicit batches that can have result-set generating statements.<br/>SQL_BS_ROW_COUNT_EXPLICIT = The driver supports explicit batches that can have row-count generating statements.<br/>SQL_BS_SELECT_PROC = The driver supports explicit procedures that can have result-set generating statements.<br/>SQL_BS_ROW_COUNT_PROC = The driver supports explicit procedures that can have row-count generating statements.|
|SQL_BOOKMARK_PERSISTENCE|2.0|An SQLUINTEGER bitmask enumerating the operations through which bookmarks persist. The following bitmasks are used together with the flag to determine through which options bookmarks persist:<br/><br/>SQL_BP_CLOSE = Bookmarks are valid after an application calls **SQLFreeStmt** with the SQL_CLOSE option, or **SQLCloseCursor** to close the cursor associated with a statement.<br/>SQL_BP_DELETE = The bookmark for a row is valid after that row has been deleted.<br/>SQL_BP_DROP = Bookmarks are valid after an application calls **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_STMT to drop a statement.<br/>SQL_BP_TRANSACTION = Bookmarks are valid after an application commits or rolls back a transaction.<br/>SQL_BP_UPDATE = The bookmark for a row is valid after any column in that row has been updated, including key columns.<br/>SQL_BP_OTHER_HSTMT = A bookmark associated with one statement can be used with another statement. Unless SQL_BP_CLOSE or SQL_BP_DROP is specified, the cursor on the first statement must be open.|
|SQL_CATALOG_LOCATION|2.0|An SQLUSMALLINT value that indicates the position of the catalog in a qualified table name:<br/><br/>SQL_CL_START<br/>SQL_CL_END<br/>For example, an Xbase driver returns SQL_CL_START because the directory (catalog) name is at the start of the table name, as in \EMPDATA\EMP.DBF. An ORACLE Server driver returns SQL_CL_END because the catalog is at the end of the table name, as in ADMIN.EMP@EMPDATA.<br/><br/>An SQL-92 Full level-conformant driver will always return SQL_CL_START. A value of 0 is returned if catalogs are not supported by the data source. To determine whether catalogs are supported, an application calls **SQLGetInfo** with the SQL_CATALOG_NAME information type.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_QUALIFIER_LOCATION.|
|SQL_CATALOG_NAME|3.0|A character string: "Y" if the server supports catalog names, or "N" if it does not.<br/><br/>An SQL-92 Full level-conformant driver will always return "Y".|
|SQL_CATALOG_NAME_SEPARATOR|1.0|A character string: the character or characters that the data source defines as the separator between a catalog name and the qualified name element that follows or precedes it.<br/><br/>An empty string is returned if catalogs are not supported by the data source. To determine whether catalogs are supported, an application calls **SQLGetInfo** with the SQL_CATALOG_NAME information type. An SQL-92 Full level-conformant driver will always return ".".<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_QUALIFIER_NAME_SEPARATOR.|
|SQL_CATALOG_TERM|1.0|A character string with the data source vendor's name for a catalog; for example, "database" or "directory". This string can be in upper, lower, or mixed case.<br/><br/>An empty string is returned if catalogs are not supported by the data source. To determine whether catalogs are supported, an application calls **SQLGetInfo** with the SQL_CATALOG_NAME information type. An SQL-92 Full level-conformant driver will always return "catalog".<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_QUALIFIER_TERM.|
|SQL_CATALOG_USAGE|2.0|An SQLUINTEGER bitmask enumerating the statements in which catalogs can be used.<br/><br/>The following bitmasks are used to determine where catalogs can be used:<br/>SQL_CU_DML_STATEMENTS = Catalogs are supported in all Data Manipulation Language statements: **SELECT**, **INSERT**, **UPDATE**, **DELETE**, and if supported, **SELECT FOR UPDATE** and positioned update and delete statements.<br/>SQL_CU_PROCEDURE_INVOCATION = Catalogs are supported in the ODBC procedure invocation statement.<br/>SQL_CU_TABLE_DEFINITION = Catalogs are supported in all table definition statements: **CREATE TABLE**, **CREATE VIEW**, **ALTER TABLE**, **DROP TABLE**, and **DROP VIEW**.<br/>SQL_CU_INDEX_DEFINITION = Catalogs are supported in all index definition statements: **CREATE INDEX** and **DROP INDEX**.<br/>SQL_CU_PRIVILEGE_DEFINITION = Catalogs are supported in all privilege definition statements: **GRANT** and **REVOKE**.<br/><br/>A value of 0 is returned if catalogs are not supported by the data source. To determine whether catalogs are supported, an application calls **SQLGetInfo** with the SQL_CATALOG_NAME information type. An SQL-92 Full level-conformant driver will always return a bitmask with all of these bits set.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_QUALIFIER_USAGE.|
|SQL_COLLATION_SEQ|3.0|The name of the collation sequence. This is a character string that indicates the name of the default collation for the default character set for this server (for example, 'ISO 8859-1' or EBCDIC). If this is unknown, an empty string will be returned. An SQL-92 Full level-conformant driver will always return a non-empty string.|
|SQL_COLUMN_ALIAS|2.0|A character string: "Y" if the data source supports column aliases; otherwise, "N".<br/><br/>A column alias is an alternative name that can be specified for a column in the select list by using an AS clause. An SQL-92 Entry level-conformant driver will always return "Y".|
|SQL_CONCAT_NULL_BEHAVIOR|1.0|An SQLUSMALLINT value that indicates how the data source handles the concatenation of NULL valued character data type columns with non-NULL valued character data type columns:<br/>SQL_CB_NULL = Result is NULL valued.<br/>SQL_CB_NON_NULL = Result is concatenation of non-NULL valued column or columns.<br/><br/>An SQL-92 Entry level-conformant driver will always return SQL_CB_NULL.|
|SQL_CONVERT_BIGINT<br/>SQL_CONVERT_BINARY<br/>SQL_CONVERT_BIT<br/>SQL_CONVERT_CHAR<br/>SQL_CONVERT_GUID<br/>SQL_CONVERT_DATE<br/>SQL_CONVERT_DECIMAL<br/>SQL_CONVERT_DOUBLE<br/>SQL_CONVERT_FLOAT<br/>SQL_CONVERT_INTEGER<br/>SQL_CONVERT_INTERVAL_YEAR_MONTH<br/>SQL_CONVERT_INTERVAL_DAY_TIME<br/>SQL_CONVERT_LONGVARBINARY<br/>SQL_CONVERT_LONGVARCHAR<br/>SQL_CONVERT_NUMERIC<br/>SQL_CONVERT_REAL<br/>SQL_CONVERT_SMALLINT<br/>SQL_CONVERT_TIME<br/>SQL_CONVERT_TIMESTAMP<br/>SQL_CONVERT_TINYINT<br/>SQL_CONVERT_VARBINARY<br/>SQL_CONVERT_VARCHAR|1.0|An SQLUINTEGER bitmask. The bitmask indicates the conversions supported by the data source with the **CONVERT** scalar function for data of the type named in the *InfoType*. If the bitmask equals zero, the data source does not support any conversions from data of the named type, including conversion to the same data type.<br/><br/>For example, to determine if a data source supports the conversion of SQL_INTEGER data to the SQL_BIGINT data type, an application calls **SQLGetInfo** with the *InfoType* of SQL_CONVERT_INTEGER. The application performs an **AND** operation with the returned bitmask and SQL_CVT_BIGINT. If the resulting value is nonzero, the conversion is supported.<br/><br/>The following bitmasks are used to determine which conversions are supported:<br/>SQL_CVT_BIGINT (ODBC 1.0)<br/>SQL_CVT_BINARY (ODBC 1.0)<br/>SQL_CVT_BIT (ODBC 1.0)<br/>SQL_CVT_GUID (ODBC 3.5)<br/>SQL_CVT_CHAR (ODBC 1.0)<br/>SQL_CVT_DATE (ODBC 1.0)<br/>SQL_CVT_DECIMAL (ODBC 1.0)<br/>SQL_CVT_DOUBLE (ODBC 1.0)<br/>SQL_CVT_FLOAT (ODBC 1.0)<br/>SQL_CVT_INTEGER (ODBC 1.0)<br/>SQL_CVT_INTERVAL_YEAR_MONTH (ODBC 3.0)<br/>SQL_CVT_INTERVAL_DAY_TIME (ODBC 3.0)<br/>SQL_CVT_LONGVARBINARY (ODBC 1.0)<br/>SQL_CVT_LONGVARCHAR (ODBC 1.0)<br/>SQL_CVT_NUMERIC (ODBC 1.0)<br/>SQL_CVT_REAL (ODBC 1.0)<br/>SQL_CVT_SMALLINT (ODBC 1.0)<br/>SQL_CVT_TIME (ODBC 1.0)<br/>SQL_CVT_TIMESTAMP (ODBC 1.0)<br/>SQL_CVT_TINYINT (ODBC 1.0)<br/>SQL_CVT_VARBINARY (ODBC 1.0)<br/>SQL_CVT_VARCHAR (ODBC 1.0)|
|SQL_CONVERT_FUNCTIONS|1.0|An SQLUINTEGER bitmask enumerating the scalar conversion functions supported by the driver and associated data source.<br/><br/>The following bitmask is used to determine which conversion functions are supported:<br/>SQL_FN_CVT_CASTSQL_FN_CVT_CONVERT|
|SQL_CORRELATION_NAME|1.0|An SQLUSMALLINT value that indicates whether table correlation names are supported:<br/>SQL_CN_NONE = Correlation names are not supported.<br/>SQL_CN_DIFFERENT = Correlation names are supported but must differ from the names of the tables they represent.<br/>SQL_CN_ANY = Correlation names are supported and can be any valid user-defined name.<br/><br/>An SQL-92 Entry level-conformant driver will always return SQL_CN_ANY.|
|SQL_CREATE_ASSERTION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE ASSERTION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CA_CREATE_ASSERTION<br/><br/>The following bits specify the supported constraint attribute if the ability to specify constraint attributes explicitly is supported (see the SQL_ALTER_TABLE and SQL_CREATE_TABLE information types):<br/>SQL_CA_CONSTRAINT_INITIALLY_DEFERRED<br/>SQL_CA_CONSTRAINT_INITIALLY_IMMEDIATE<br/>SQL_CA_CONSTRAINT_DEFERRABLE<br/>SQL_CA_CONSTRAINT_NON_DEFERRABLE<br/><br/>An SQL-92 Full level-conformant driver will always return all of these options as supported. A return value of "0" means that the **CREATE ASSERTION** statement is not supported.|
|SQL_CREATE_CHARACTER_SET|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE CHARACTER SET** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CCS_CREATE_CHARACTER_SET<br/>SQL_CCS_COLLATE_CLAUSE<br/>SQL_CCS_LIMITED_COLLATION<br/><br/>An SQL-92 Full level-conformant driver will always return all of these options as supported. A return value of "0" means that the **CREATE CHARACTER SET** statement is not supported.|
|SQL_CREATE_COLLATION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE COLLATION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_CCOL_CREATE_COLLATION<br/><br/>An SQL-92 Full level-conformant driver will always return this option as supported. A return value of "0" means that the **CREATE COLLATION** statement is not supported.|
|SQL_CREATE_DOMAIN|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE DOMAIN** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CDO_CREATE_DOMAIN = The CREATE DOMAIN statement is supported (Intermediate level).<br/>SQL_CDO_CONSTRAINT_NAME_DEFINITION = \<constraint name definition> is supported for naming domain constraints (Intermediate level).<br/><br/>The following bits specify the ability to create column constraints:<br/>SQL_CDO_DEFAULT = Specifying domain constraints is supported (Intermediate level)<br/>SQL_CDO_CONSTRAINT = Specifying domain defaults is supported (Intermediate level)<br/>SQL_CDO_COLLATION = Specifying domain collation is supported (Full level)<br/><br/>The following bits specify the supported constraint attributes if specifying domain constraints is supported (SQL_CDO_DEFAULT is set):<br/>SQL_CDO_CONSTRAINT_INITIALLY_DEFERRED (Full level)<br/>SQL_CDO_CONSTRAINT_INITIALLY_IMMEDIATE (Full level)<br/>SQL_CDO_CONSTRAINT_DEFERRABLE (Full level)<br/>SQL_CDO_CONSTRAINT_NON_DEFERRABLE (Full level)<br/><br/>A return value of "0" means that the **CREATE DOMAIN** statement is not supported.|
|SQL_CREATE_SCHEMA|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE SCHEMA** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CS_CREATE_SCHEMA<br/>SQL_CS_AUTHORIZATION<br/>SQL_CS_DEFAULT_CHARACTER_SET<br/><br/>An SQL-92 Intermediate level-conformant driver will always return the SQL_CS_CREATE_SCHEMA and SQL_CS_AUTHORIZATION options as supported. These must also be supported at the SQL-92 Entry level, but not necessarily as SQL statements. An SQL-92 Full level-conformant driver will always return all of these options as supported.|
|SQL_CREATE_TABLE|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE TABLE** statement, as defined in SQL-92, supported by the data source.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CT_CREATE_TABLE = The CREATE TABLE statement is supported. (Entry level)<br/>SQL_CT_TABLE_CONSTRAINT = Specifying table constraints is supported (FIPS Transitional level)<br/>SQL_CT_CONSTRAINT_NAME_DEFINITION = The \<constraint name definition> clause is supported for naming column and table constraints (Intermediate level)<br/><br/>The following bits specify the ability to create temporary tables:<br/>SQL_CT_COMMIT_PRESERVE = Deleted rows are preserved on commit. (Full level)<br/>SQL_CT_COMMIT_DELETE = Deleted rows are deleted on commit. (Full level)<br/>SQL_CT_GLOBAL_TEMPORARY = Global temporary tables can be created. (Full level)<br/>SQL_CT_LOCAL_TEMPORARY = Local temporary tables can be created. (Full level)<br/><br/>The following bits specify the ability to create column constraints:<br/>SQL_CT_COLUMN_CONSTRAINT = Specifying column constraints is supported (FIPS Transitional level)<br/>SQL_CT_COLUMN_DEFAULT = Specifying column defaults is supported (FIPS Transitional level)<br/>SQL_CT_COLUMN_COLLATION = Specifying column collation is supported (Full level)<br/><br/>The following bits specify the supported constraint attributes if specifying column or table constraints is supported:<br/>SQL_CT_CONSTRAINT_INITIALLY_DEFERRED (Full level)<br/>SQL_CT_CONSTRAINT_INITIALLY_IMMEDIATE (Full level)<br/>SQL_CT_CONSTRAINT_DEFERRABLE (Full level)<br/>SQL_CT_CONSTRAINT_NON_DEFERRABLE (Full level)|
|SQL_CREATE_TRANSLATION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE TRANSLATION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_CTR_CREATE_TRANSLATION<br/><br/>An SQL-92 Full level-conformant driver will always return these options as supported. A return value of "0" means that the **CREATE TRANSLATION** statement is not supported.|
|SQL_CREATE_VIEW|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **CREATE VIEW** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_CV_CREATE_VIEW<br/>SQL_CV_CHECK_OPTION<br/>SQL_CV_CASCADED<br/>SQL_CV_LOCAL<br/><br/>A return value of "0" means that the **CREATE VIEW** statement is not supported.<br/><br/>An SQL-92 Entry level-conformant driver will always return the SQL_CV_CREATE_VIEW and SQL_CV_CHECK_OPTION options as supported.<br/><br/>An SQL-92 Full level-conformant driver will always return all of these options as supported.|
|SQL_CURSOR_COMMIT_BEHAVIOR|1.0|An SQLUSMALLINT value that indicates how a **COMMIT** operation affects cursors and prepared statements in the data source (the behavior of the data source when you commit a transaction).<br/><br/>The value of this attribute will reflect the current state of the next setting: SQL_COPT_SS_PRESERVE_CURSORS.<br/>SQL_CB_DELETE = Close cursors and delete prepared statements. To use the cursor again, the application must reprepare and re-execute the statement.<br/>SQL_CB_CLOSE = Close cursors. For prepared statements, the application can call **SQLExecute** on the statement without calling **SQLPrepare** again. The default for the SQL ODBC driver is SQL_CB_CLOSE. This means that the SQL ODBC driver will close your cursor(s) when you commit a transaction.<br/>SQL_CB_PRESERVE = Preserve cursors in the same position as before the **COMMIT** operation. The application can continue to fetch data, or it can close the cursor and re-execute the statement without repreparing it.|
|SQL_CURSOR_ROLLBACK_BEHAVIOR|1.0|An SQLUSMALLINT value that indicates how a **ROLLBACK** operation affects cursors and prepared statements in the data source:<br/>SQL_CB_DELETE = Close cursors and delete prepared statements. To use the cursor again, the application must reprepare and re-execute the statement.<br/>SQL_CB_CLOSE = Close cursors. For prepared statements, the application can call **SQLExecute** on the statement without calling **SQLPrepare** again.<br/>SQL_CB_PRESERVE = Preserve cursors in the same position as before the **ROLLBACK** operation. The application can continue to fetch data, or it can close the cursor and re-execute the statement without repreparing it.|
|SQL_CURSOR_SENSITIVITY|3.0|An SQLUINTEGER value that indicates the support for cursor sensitivity:<br/>SQL_INSENSITIVE = All cursors on the statement handle show the result set without reflecting any changes that were made to it by any other cursor within the same transaction.<br/>SQL_UNSPECIFIED = It is unspecified whether cursors on the statement handle make visible the changes that were made to a result set by another cursor within the same transaction. Cursors on the statement handle may make visible none, some, or all such changes.<br/>SQL_SENSITIVE = Cursors are sensitive to changes that were made by other cursors within the same transaction.<br/><br/>An SQL-92 Entry level-conformant driver will always return the SQL_UNSPECIFIED option as supported.<br/><br/>An SQL-92 Full level-conformant driver will always return the SQL_INSENSITIVE option as supported.|
|SQL_DATA_SOURCE_NAME|1.0|A character string with the data source name that was used during connection. If the application called **SQLConnect**, this is the value of the *szDSN* argument. If the application called **SQLDriverConnect** or **SQLBrowseConnect**, this is the value of the DSN keyword in the connection string passed to the driver. If the connection string did not contain the **DSN** keyword (such as when it contains the **DRIVER** keyword), this is an empty string.|
|SQL_DATA_SOURCE_READ_ONLY|1.0|A character string. "Y" if the data source is set to READ ONLY mode, "N" if it is otherwise.<br/><br/>This characteristic pertains only to the data source itself; it is not a characteristic of the driver that enables access to the data source. A driver that is read/write can be used with a data source that is read-only. If a driver is read-only, all of its data sources must be read-only and must return SQL_DATA_SOURCE_READ_ONLY.|
|SQL_DATABASE_NAME|1.0|A character string with the name of the current database in use, if the data source defines a named object called "database".<br/><br/>In ODBC 3.x, the value returned for this *InfoType* can also be returned by calling **SQLGetConnectAttr** with an *Attribute* argument of SQL_ATTR_CURRENT_CATALOG.|
|SQL_DATETIME_LITERALS|3.0|An SQLUINTEGER bitmask enumerating the SQL-92 datetime literals supported by the data source. Note that these are the datetime literals listed in the SQL-92 specification and are separate from the datetime literal escape clauses defined by ODBC. For more information about the ODBC datetime literal escape clauses, see [Date, Time, and Timestamp Literals](../develop-app/date-time-and-timestamp-literals.md).<br/><br/> A FIPS Transitional level-conformant driver will always return the "1" value in the bitmask for the bits in the following list. A value of "0" means that SQL-92 datetime literals are not supported.<br/><br/>The following bitmasks are used to determine which literals are supported:<br/>SQL_DL_SQL92_DATE<br/>SQL_DL_SQL92_TIME<br/>SQL_DL_SQL92_TIMESTAMP<br/>SQL_DL_SQL92_INTERVAL_YEAR<br/>SQL_DL_SQL92_INTERVAL_MONTH<br/>SQL_DL_SQL92_INTERVAL_DAY<br/>SQL_DL_SQL92_INTERVAL_HOUR<br/>SQL_DL_SQL92_INTERVAL_MINUTE<br/>SQL_DL_SQL92_INTERVAL_SECOND<br/>SQL_DL_SQL92_INTERVAL_YEAR_TO_MONTH<br/>SQL_DL_SQL92_INTERVAL_DAY_TO_HOUR<br/>SQL_DL_SQL92_INTERVAL_DAY_TO_MINUTE<br/>SQL_DL_SQL92_INTERVAL_DAY_TO_SECOND<br/>SQL_DL_SQL92_INTERVAL_HOUR_TO_MINUTE<br/>SQL_DL_SQL92_INTERVAL_HOUR_TO_SECOND<br/>SQL_DL_SQL92_INTERVAL_MINUTE_TO_SECOND|
|SQL_DBMS_NAME|1.0|A character string with the name of the DBMS product accessed by the driver.|
|SQL_DBMS_VER|1.0|A character string that indicates the version of the DBMS product accessed by the driver. The version is of the form ##.##.####, where the first two digits are the major version, the next two digits are the minor version, and the last four digits are the release version. The driver must render the DBMS product version in this form but can also append the DBMS product-specific version. For example, "04.01.0000 Rdb 4.1".|
|SQL_DDL_INDEX|3.0|An SQLUINTEGER value that indicates support for creation and dropping of indexes:<br/>SQL_DI_CREATE_INDEX<br/>SQL_DI_DROP_INDEX|
|SQL_DEFAULT_TXN_ISOLATION|1.0|An SQLUINTEGER value that indicates the default transaction isolation level supported by the driver or data source, or zero if the data source does not support transactions. The following terms are used to define transaction isolation levels:<br/>**Dirty Read** Transaction 1 changes a row. Transaction 2 reads the changed row before transaction 1 commits the change. If transaction 1 rolls back the change, transaction 2 will have read a row that is considered to have never existed.<br/>**Non-repeatable Read** Transaction 1 reads a row. Transaction 2 updates or deletes that row and commits this change. If transaction 1 tries to reread the row, it will receive different row values or discover that the row has been deleted.<br/>**Phantom** Transaction 1 reads a set of rows that satisfy some search criteria. Transaction 2 generates one or more rows (through either inserts or updates) that match the search criteria. If transaction 1 re-executes the statement that reads the rows, it receives a different set of rows.<br/><br/>If the data source supports transactions, the driver returns one of the following bitmasks:<br/>SQL_TXN_READ_UNCOMMITTED = Dirty reads, non-repeatable reads, and phantoms are possible.<br/>SQL_TXN_READ_COMMITTED = Dirty reads are not possible. Non-repeatable reads and phantoms are possible.<br/>SQL_TXN_REPEATABLE_READ = Dirty reads and non-repeatable reads are not possible. Phantoms are possible.<br/>SQL_TXN_SERIALIZABLE = Transactions are serializable. Serializable transactions do not allow dirty reads, non-repeatable reads, or phantoms.|
|SQL_DESCRIBE_PARAMETER|3.0|A character string: "Y" if parameters can be described; "N", if not.<br/><br/>An SQL-92 Full level-conformant driver will usually return "Y" because it will support the **DESCRIBE INPUT** statement. Because this does not directly specify the underlying SQL support, however, describing parameters might not be supported, even in a SQL-92 Full level-conformant driver.|
|SQL_DM_VER|3.0|A character string with the version of the Driver Manager. The version is of the form ##.##.####.####, where:<br/>The first set of two digits is the major ODBC version, as given by the constant SQL_SPEC_MAJOR.<br/>The second set of two digits is the minor ODBC version, as given by the constant SQL_SPEC_MINOR.<br/>The third set of four digits is the Driver Manager major build number.<br/>The last set of four digits is the Driver Manager minor build number.<br/>The Windows 7 Driver Manager version is 03.80. The Windows 8 Driver Manager version is 03.81.|
|SQL_DRIVER_AWARE_POOLING_SUPPORTED|3.8|A SQLUINTEGER value that indicates if the driver support driver-aware pooling. (For more information, see [Driver-Aware Connection Pooling](../develop-app/driver-aware-connection-pooling.md).<br/><br/>SQL_DRIVER_AWARE_POOLING_CAPABLE indicates that the driver can support driver-aware pooling mechanism.<br/>SQL_DRIVER_AWARE_POOLING_NOT_CAPABLE indicates that the driver cannot support driver-aware pooling mechanism.<br/><br/>A driver does not need to implement SQL_DRIVER_AWARE_POOLING_SUPPORTED and the Driver Manager will not honor to the driver's return value.|
|SQL_DRIVER_HDBCSQL_DRIVER_HENV|1.0|An SQLULEN value, the driver's environment handle or connection handle, determined by the argument *InfoType*.<br/><br/>These information types are implemented by the Driver Manager alone.|
|SQL_DRIVER_HDESC|3.0|An SQLULEN value, the driver's descriptor handle determined by the Driver Manager's descriptor handle, which must be passed on input in \**InfoValuePtr* from the application. In this case, *InfoValuePtr* is both an input and output argument. The input descriptor handle passed in \**InfoValuePtr* must have been either explicitly or implicitly allocated on the *ConnectionHandle*.<br/><br/>The application should make a copy of the Driver Manager's descriptor handle before it calls **SQLGetInfo** with this information type, to make sure that the handle is not overwritten on output.<br/><br/>This information type is implemented by the Driver Manager alone.|
|SQL_DRIVER_HLIB|2.0|An SQLULEN value, the *hinst* from the load library returned to the Driver Manager when it loaded the driver DLL on a Microsoft Windows operating system, or its equivalent on another operating system. The handle is valid only for the connection handle specified in the call to **SQLGetInfo**.<br/><br/>This information type is implemented by the Driver Manager alone.|
|SQL_DRIVER_HSTMT|1.0|An SQLULEN value, the driver's statement handle determined by the Driver Manager statement handle, which must be passed on input in \**InfoValuePtr* from the application. In this case, *InfoValuePtr* is both an input and an output argument. The input statement handle passed in \**InfoValuePtr* must have been allocated on the argument *ConnectionHandle*.<br/><br/>The application should make a copy of the Driver Manager's statement handle before it calls **SQLGetInfo** with this information type, to ensure that the handle is not overwritten on output.<br/><br/>This information type is implemented by the Driver Manager alone.|
|SQL_DRIVER_NAME|1.0|A character string with the file name of the driver used to access the data source.|
|SQL_DRIVER_ODBC_VER|2.0|A character string with the version of ODBC that the driver supports. The version is of the form ##.##, where the first two digits are the major version and the next two digits are the minor version. SQL_SPEC_MAJOR and SQL_SPEC_MINOR define the major and minor version numbers. For the version of ODBC described in this manual, these are 3 and 0, and the driver should return "03.00".<br/><br/>The ODBC Driver Manager will not modify the return value of SQLGetInfo(SQL_DRIVER_ODBC_VER) to maintain backward compatibility for existing applications. The driver specifies which value will be returned. However, a driver that supports C data type extensibility must return 3.8 (or higher) when an application calls **SQLSetEnvAttr** to set SQL_ATTR_ODBC_VERSION to 3.8. For more information, see [C Data Types in ODBC](../develop-app/c-data-types-in-odbc.md).|
|SQL_DRIVER_VER|1.0|A character string with the version of the driver and optionally, a description of the driver. At a minimum, the version is of the form ##.##.####, where the first two digits are the major version, the next two digits are the minor version, and the last four digits are the release version.|
|SQL_DROP_ASSERTION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP ASSERTION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_DA_DROP_ASSERTION<br/><br/>An SQL-92 Full level-conformant driver will always return this option as supported.|
|SQL_DROP_CHARACTER_SET|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP CHARACTER SET** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_DCS_DROP_CHARACTER_SET<br/><br/>An SQL-92 Full level-conformant driver will always return this option as supported.|
|SQL_DROP_COLLATION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP COLLATION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_DC_DROP_COLLATION<br/><br/>An SQL-92 Full level-conformant driver will always return this option as supported.|
|SQL_DROP_DOMAIN|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP DOMAIN** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_DD_DROP_DOMAIN<br/>SQL_DD_CASCADE<br/>SQL_DD_RESTRICT<br/><br/>An SQL-92 Intermediate level-conformant driver will always return all of these options as supported.|
|SQL_DROP_SCHEMA|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP SCHEMA** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_DS_DROP_SCHEMA<br/>SQL_DS_CASCADE<br/>SQL_DS_RESTRICT<br/><br/>An SQL-92 Intermediate level-conformant driver will always return all of these options as supported.|
|SQL_DROP_TABLE|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP TABLE** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_DT_DROP_TABLE<br/>SQL_DT_CASCADE<br/>SQL_DT_RESTRICT<br/><br/>An FIPS Transitional level-conformant driver will always return all of these options as supported.|
|SQL_DROP_TRANSLATION|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP TRANSLATION** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmask is used to determine which clauses are supported:<br/>SQL_DTR_DROP_TRANSLATION<br/><br/>An SQL-92 Full level-conformant driver will always return this option as supported.|
|SQL_DROP_VIEW|3.0|An SQLUINTEGER bitmask enumerating the clauses in the **DROP VIEW** statement, as defined in SQL-92, supported by the data source.<br/><br/>The following bitmasks are used to determine which clauses are supported:<br/>SQL_DV_DROP_VIEW<br/>SQL_DV_CASCADE<br/>SQL_DV_RESTRICT<br/><br/>An FIPS Transitional level-conformant driver will always return all of these options as supported.|
|SQL_DYNAMIC_CURSOR_ATTRIBUTES1|3.0|An SQLUINTEGER bitmask that describes the attributes of a dynamic cursor that are supported by the driver. This bitmask contains the first subset of attributes; for the second subset, see SQL_DYNAMIC_CURSOR_ATTRIBUTES2.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA1_NEXT = A *FetchOrientation* argument of SQL_FETCH_NEXT is supported in a call to **SQLFetchScroll** when the cursor is a dynamic cursor.<br/>SQL_CA1_ABSOLUTE = *FetchOrientation* arguments of SQL_FETCH_FIRST, SQL_FETCH_LAST, and SQL_FETCH_ABSOLUTE are supported in a call to **SQLFetchScroll** when the cursor is a dynamic cursor. (The rowset that will be fetched is independent of the current cursor position.)<br/>SQL_CA1_RELATIVE = *FetchOrientation* arguments of SQL_FETCH_PRIOR and SQL_FETCH_RELATIVE are supported in a call to **SQLFetchScroll** when the cursor is a dynamic cursor. (The rowset that will be fetched depends on the current cursor position. Note that this is separated from SQL_FETCH_NEXT because in a forward-only cursor, only SQL_FETCH_NEXT is supported.)<br/>SQL_CA1_BOOKMARK = A *FetchOrientation* argument of SQL_FETCH_BOOKMARK is supported in a call to **SQLFetchScroll** when the cursor is a dynamic cursor.<br/>SQL_CA1_LOCK_EXCLUSIVE = A *LockType* argument of SQL_LOCK_EXCLUSIVE is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_LOCK_NO_CHANGE = A *LockType* argument of SQL_LOCK_NO_CHANGE is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_LOCK_UNLOCK = A *LockType* argument of SQL_LOCK_UNLOCK is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_POS_POSITION = An *Operation* argument of SQL_POSITION is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_POS_UPDATE = An *Operation* argument of SQL_UPDATE is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_POS_DELETE = An *Operation* argument of SQL_DELETE is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_POS_REFRESH = An *Operation* argument of SQL_REFRESH is supported in a call to **SQLSetPos** when the cursor is a dynamic cursor.<br/>SQL_CA1_POSITIONED_UPDATE = An UPDATE WHERE CURRENT OF SQL statement is supported when the cursor is a dynamic cursor. (An SQL-92 Entry level-conformant driver will always return this option as supported.)<br/>SQL_CA1_POSITIONED_DELETE = A DELETE WHERE CURRENT OF SQL statement is supported when the cursor is a dynamic cursor. (An SQL-92 Entry level-conformant driver will always return this option as supported.)<br/>SQL_CA1_SELECT_FOR_UPDATE = A SELECT FOR UPDATE SQL statement is supported when the cursor is a dynamic cursor. (An SQL-92 Entry level-conformant driver will always return this option as supported.)<br/>SQL_CA1_BULK_ADD = An *Operation* argument of SQL_ADD is supported in a call to **SQLBulkOperations** when the cursor is a dynamic cursor.<br/>SQL_CA1_BULK_UPDATE_BY_BOOKMARK = An *Operation* argument of SQL_UPDATE_BY_BOOKMARK is supported in a call to **SQLBulkOperations** when the cursor is a dynamic cursor.<br/>SQL_CA1_BULK_DELETE_BY_BOOKMARK = An *Operation* argument of SQL_DELETE_BY_BOOKMARK is supported in a call to **SQLBulkOperations** when the cursor is a dynamic cursor.<br/>SQL_CA1_BULK_FETCH_BY_BOOKMARK = An *Operation* argument of SQL_FETCH_BY_BOOKMARK is supported in a call to **SQLBulkOperations** when the cursor is a dynamic cursor.<br/><br/>An SQL-92 Intermediate level-conformant driver will usually return the SQL_CA1_NEXT, SQL_CA1_ABSOLUTE, and SQL_CA1_RELATIVE options as supported, because it supports scrollable cursors through the embedded SQL FETCH statement. Because this does not directly determine the underlying SQL support, however, scrollable cursors may not be supported, even for an SQL-92 Intermediate level-conformant driver.|
|SQL_DYNAMIC_CURSOR_ATTRIBUTES2|3.0|An SQLUINTEGER bitmask that describes the attributes of a dynamic cursor that are supported by the driver. This bitmask contains the second subset of attributes; for the first subset, see SQL_DYNAMIC_CURSOR_ATTRIBUTES1.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA2_READ_ONLY_CONCURRENCY = A read-only dynamic cursor, in which no updates are allowed, is supported. (The SQL_ATTR_CONCURRENCY statement attribute can be SQL_CONCUR_READ_ONLY for a dynamic cursor).<br/>SQL_CA2_LOCK_CONCURRENCY = A dynamic cursor that uses the lowest level of locking sufficient to make sure that the row can be updated is supported. (The SQL_ATTR_CONCURRENCY statement attribute can be SQL_CONCUR_LOCK for a dynamic cursor.) These locks must be consistent with the transaction isolation level set by the SQL_ATTR_TXN_ISOLATION connection attribute.<br/>SQL_CA2_OPT_ROWVER_CONCURRENCY = A dynamic cursor that uses the optimistic concurrency control comparing row versions is supported. (The SQL_ATTR_CONCURRENCY statement attribute can be SQL_CONCUR_ROWVER for a dynamic cursor.)<br/>SQL_CA2_OPT_VALUES_CONCURRENCY = A dynamic cursor that uses the optimistic concurrency control comparing values is supported. (The SQL_ATTR_CONCURRENCY statement attribute can be SQL_CONCUR_VALUES for a dynamic cursor.)<br/>SQL_CA2_SENSITIVITY_ADDITIONS = Added rows are visible to a dynamic cursor; the cursor can scroll to those rows. (Where these rows are added to the cursor is driver-dependent.)<br/>SQL_CA2_SENSITIVITY_DELETIONS = Deleted rows are no longer available to a dynamic cursor, and do not leave a "hole" in the result set; after the dynamic cursor scrolls from a deleted row, it cannot return to that row.<br/>SQL_CA2_SENSITIVITY_UPDATES = Updates to rows are visible to a dynamic cursor; if the dynamic cursor scrolls from and returns to an updated row, the data returned by the cursor is the updated data, not the original data.<br/>SQL_CA2_MAX_ROWS_SELECT = The SQL_ATTR_MAX_ROWS statement attribute affects **SELECT** statements when the cursor is a dynamic cursor.<br/>SQL_CA2_MAX_ROWS_INSERT = The SQL_ATTR_MAX_ROWS statement attribute affects **INSERT** statements when the cursor is a dynamic cursor.<br/>SQL_CA2_MAX_ROWS_DELETE = The SQL_ATTR_MAX_ROWS statement attribute affects **DELETE** statements when the cursor is a dynamic cursor.<br/>SQL_CA2_MAX_ROWS_UPDATE = The SQL_ATTR_MAX_ROWS statement attribute affects **UPDATE** statements when the cursor is a dynamic cursor.<br/>SQL_CA2_MAX_ROWS_CATALOG = The SQL_ATTR_MAX_ROWS statement attribute affects **CATALOG** result sets when the cursor is a dynamic cursor.<br/>SQL_CA2_MAX_ROWS_AFFECTS_ALL = The SQL_ATTR_MAX_ROWS statement attribute affects **SELECT**, **INSERT**, **DELETE**, and **UPDATE** statements, and **CATALOG** result sets, when the cursor is a dynamic cursor.<br/>SQL_CA2_CRC_EXACT = The exact row count is available in the SQL_DIAG_CURSOR_ROW_COUNT diagnostic field when the cursor is a dynamic cursor.<br/>SQL_CA2_CRC_APPROXIMATE = An approximate row count is available in the SQL_DIAG_CURSOR_ROW_COUNT diagnostic field when the cursor is a dynamic cursor.<br/>SQL_CA2_SIMULATE_NON_UNIQUE = The driver does not guarantee that simulated positioned update or delete statements will affect only one row when the cursor is a dynamic cursor; it is the application's responsibility to guarantee this. (If a statement affects more than one row, **SQLExecute** or **SQLExecDirect** returns SQLSTATE 01001 [Cursor operation conflict].) To set this behavior, the application calls **SQLSetStmtAttr** with the SQL_ATTR_SIMULATE_CURSOR attribute set to SQL_SC_NON_UNIQUE.<br/>SQL_CA2_SIMULATE_TRY_UNIQUE = The driver tries to guarantee that simulated positioned update or delete statements will affect only one row when the cursor is a dynamic cursor. The driver always executes such statements, even if they might affect more than one row, such as when there is no unique key. (If a statement affects more than one row, **SQLExecute** or **SQLExecDirect** returns SQLSTATE 01001 [Cursor operation conflict].) To set this behavior, the application calls **SQLSetStmtAttr** with the SQL_ATTR_SIMULATE_CURSOR attribute set to SQL_SC_TRY_UNIQUE.<br/>SQL_CA2_SIMULATE_UNIQUE = The driver guarantees that simulated positioned update or delete statements will affect only one row when the cursor is a dynamic cursor. If the driver cannot guarantee this for a given statement, **SQLExecDirect** or **SQLPrepare** return SQLSTATE 01001 (Cursor operation conflict). To set this behavior, the application calls **SQLSetStmtAttr** with the SQL_ATTR_SIMULATE_CURSOR attribute set to SQL_SC_UNIQUE.|
|SQL_EXPRESSIONS_IN_ORDERBY|1.0|A character string: "Y" if the data source supports expressions in the **ORDER BY** list; "N" if it does not.|
|SQL_FILE_USAGE|2.0|An SQLUSMALLINT value that indicates how a single-tier driver directly treats files in a data source:<br/>SQL_FILE_NOT_SUPPORTED = The driver is not a single-tier driver. For example, an ORACLE driver is a two-tier driver.<br/>SQL_FILE_TABLE = A single-tier driver treats files in a data source as tables. For example, an Xbase driver treats each Xbase file as a table.<br/>SQL_FILE_CATALOG = A single-tier driver treats files in a data source as a catalog. For example, a Microsoft Access driver treats each Microsoft Access file as a complete database.<br/><br/>An application might use this to determine how users will select data. For example, Xbase users often think of data as stored in files, whereas ORACLE and Microsoft Access users generally think of data as stored in tables.<br/><br/>When a user selects an Xbase data source, the application could display the Windows **File Open** common dialog box; when the user selects a Microsoft Access or ORACLE data source, the application could display a custom **Select Table** dialog box.|
|SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1|3.0|An SQLUINTEGER bitmask that describes the attributes of a forward-only cursor that are supported by the driver. This bitmask contains the first subset of attributes; for the second subset, see SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA1_NEXT<br/>SQL_CA1_LOCK_EXCLUSIVE<br/>SQL_CA1_LOCK_NO_CHANGE<br/>SQL_CA1_LOCK_UNLOCK<br/>SQL_CA1_POS_POSITION<br/>SQL_CA1_POS_UPDATE<br/>SQL_CA1_POS_DELETE<br/>SQL_CA1_POS_REFRESH<br/>SQL_CA1_POSITIONED_UPDATE<br/>SQL_CA1_POSITIONED_DELETE<br/>SQL_CA1_SELECT_FOR_UPDATE<br/>SQL_CA1_BULK_ADD<br/>SQL_CA1_BULK_UPDATE_BY_BOOKMARK<br/>SQL_CA1_BULK_DELETE_BY_BOOKMARK<br/>SQL_CA1_BULK_FETCH_BY_BOOKMARK<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES1 (and substitute "forward-only cursor" for "dynamic cursor" in the descriptions).|
|SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2|3.0|An SQLUINTEGER bitmask that describes the attributes of a forward-only cursor that are supported by the driver. This bitmask contains the second subset of attributes; for the first subset, see SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA2_READ_ONLY_CONCURRENCY<br/>SQL_CA2_LOCK_CONCURRENCY<br/>SQL_CA2_OPT_ROWVER_CONCURRENCY<br/>SQL_CA2_OPT_VALUES_CONCURRENCY<br/>SQL_CA2_SENSITIVITY_ADDITIONS<br/>SQL_CA2_SENSITIVITY_DELETIONS<br/>SQL_CA2_SENSITIVITY_UPDATES<br/>SQL_CA2_MAX_ROWS_SELECT<br/>SQL_CA2_MAX_ROWS_INSERT<br/>SQL_CA2_MAX_ROWS_DELETE<br/>SQL_CA2_MAX_ROWS_UPDATE<br/>SQL_CA2_MAX_ROWS_CATALOG<br/>SQL_CA2_MAX_ROWS_AFFECTS_ALL<br/>SQL_CA2_CRC_EXACT<br/>SQL_CA2_CRC_APPROXIMATE<br/>SQL_CA2_SIMULATE_NON_UNIQUE<br/>SQL_CA2_SIMULATE_TRY_UNIQUE<br/>SQL_CA2_SIMULATE_UNIQUE<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES2 (and substitute "forward-only cursor" for "dynamic cursor" in the descriptions).|
|SQL_GETDATA_EXTENSIONS|2.0|An SQLUINTEGER bitmask enumerating extensions to **SQLGetData**.<br/><br/>The following bitmasks are used together with the flag to determine what common extensions the driver supports for **SQLGetData**:<br/>SQL_GD_ANY_COLUMN = **SQLGetData** can be called for any unbound column, including those before the last bound column. Note that the columns must be called in order of ascending column number unless SQL_GD_ANY_ORDER is also returned.<br/>SQL_GD_ANY_ORDER = **SQLGetData** can be called for unbound columns in any order. Note that **SQLGetData** can be called only for columns after the last bound column unless SQL_GD_ANY_COLUMN is also returned.<br/>SQL_GD_BLOCK = **SQLGetData** can be called for an unbound column in any row in a block (where the rowset size is greater than 1) of data after positioning to that row with **SQLSetPos**.<br/>SQL_GD_BOUND = **SQLGetData** can be called for bound columns in addition to unbound columns. A driver cannot return this value unless it also returns SQL_GD_ANY_COLUMN.<br/>SQL_GD_OUTPUT_PARAMS = **SQLGetData** can be called to return output parameter values. For more information, see [Retrieving Output Parameters Using SQLGetData](../develop-app/retrieving-output-parameters-using-sqlgetdata.md).<br/><br/>**SQLGetData** is required to return data only from unbound columns that occur after the last bound column, are called in order of increasing column number, and are not in a row in a block of rows.<br/><br/>If a driver supports bookmarks (either fixed-length or variable-length), it must support calling **SQLGetData** on column 0. This support is required regardless of what the driver returns for a call to **SQLGetInfo** with the SQL_GETDATA_EXTENSIONS *InfoType*.|
|SQL_GROUP_BY|2.0|An SQLUSMALLINT value that specifies the relationship between the columns in the **GROUP BY** clause and the nonaggregated columns in the select list:<br/>SQL_GB_COLLATE = A **COLLATE** clause can be specified at the end of each grouping column. (ODBC 3.0)<br/>SQL_GB_NOT_SUPPORTED = **GROUP BY** clauses are not supported. (ODBC 2.0)<br/>SQL_GB_GROUP_BY_EQUALS_SELECT = The **GROUP BY** clause must contain all nonaggregated columns in the select list. It cannot contain any other columns. For example, **SELECT DEPT, MAX(SALARY) FROM EMPLOYEE GROUP BY DEPT**. (ODBC 2.0)<br/>SQL_GB_GROUP_BY_CONTAINS_SELECT = The **GROUP BY** clause must contain all nonaggregated columns in the select list. It can contain columns that are not in the select list. For example, **SELECT DEPT, MAX(SALARY) FROM EMPLOYEE GROUP BY DEPT, AGE**. (ODBC 2.0)<br/>SQL_GB_NO_RELATION = The columns in the **GROUP BY** clause and the select list are not related. The meaning of nongrouped, nonaggregated columns in the select list is data source-dependent. For example, **SELECT DEPT, SALARY FROM EMPLOYEE GROUP BY DEPT, AGE**. (ODBC 2.0)<br/><br/>An SQL-92 Entry level-conformant driver will always return the SQL_GB_GROUP_BY_EQUALS_SELECT option as supported. An SQL-92 Full level-conformant driver will always return the SQL_GB_COLLATE option as supported. If none of the options are supported, the **GROUP BY** clause is not supported by the data source.|
|SQL_IDENTIFIER_CASE|1.0|An SQLUSMALLINT value as follows:<br/>SQL_IC_UPPER = Identifiers in SQL are not case-sensitive and are stored in uppercase in system catalog.<br/>SQL_IC_LOWER = Identifiers in SQL are not case-sensitive and are stored in lowercase in system catalog.<br/>SQL_IC_SENSITIVE = Identifiers in SQL are case sensitive and are stored in mixed case in system catalog.<br/>SQL_IC_MIXED = Identifiers in SQL are not case-sensitive and are stored in mixed case in system catalog.<br/><br/>Because identifiers in SQL-92 are never case-sensitive, a driver that conforms strictly to SQL-92 (any level) will never return the SQL_IC_SENSITIVE option as supported.|
|SQL_IDENTIFIER_QUOTE_CHAR|1.0|The character string that is used as the starting and ending delimiter of a quoted (delimited) identifier in SQL statements. (Identifiers passed as arguments to ODBC functions do not have to be quoted.) If the data source does not support quoted identifiers, a blank is returned.<br/><br/>This character string can also be used for quoting catalog function arguments when the connection attribute SQL_ATTR_METADATA_ID is set to SQL_TRUE.<br/><br/>Because the identifier quote character in SQL-92 is the double quotation mark ("), a driver that conforms strictly to SQL-92 will always return the double quotation mark character.|
|SQL_INDEX_KEYWORDS|3.0|An SQLUINTEGER bitmask that enumerates keywords in the CREATE INDEX statement that are supported by the driver:<br/>SQL_IK_NONE = None of the keywords is supported.<br/>SQL_IK_ASC = ASC keyword is supported.<br/>SQL_IK_DESC = DESC keyword is supported.<br/>SQL_IK_ALL = All keywords are supported.<br/><br/>To see whether the CREATE INDEX statement is supported, an application calls **SQLGetInfo** with the SQL_DLL_INDEX information type.|
|SQL_INFO_SCHEMA_VIEWS|3.0|An SQLUINTEGER bitmask enumerating the views in the INFORMATION_SCHEMA that are supported by the driver. The views in, and the contents of, INFORMATION_SCHEMA are as defined in SQL-92.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which views are supported:<br/>SQL_ISV_ASSERTIONS = Identifies the catalog's assertions that are owned by a given user. (Full level)<br/>SQL_ISV_CHARACTER_SETS = Identifies the catalog's character sets that can be accessed by a given user. (Intermediate level)<br/>SQL_ISV_CHECK_CONSTRAINTS = Identifies the CHECK constraints that are owned by a given user. (Intermediate level)<br/>SQL_ISV_COLLATIONS = Identifies the character collations for the catalog that can be accessed by a given user. (Full level)<br/>SQL_ISV_COLUMN_DOMAIN_USAGE = Identifies columns for the catalog that depend on domains defined in the catalog and are owned by a given user. (Intermediate level)<br/>SQL_ISV_COLUMN_PRIVILEGES = Identifies the privileges on columns of persistent tables that are available to or granted by a given user. (FIPS Transitional level)<br/>SQL_ISV_COLUMNS = Identifies the columns of persistent tables that can be accessed by a given user. (FIPS Transitional level)<br/>SQL_ISV_CONSTRAINT_COLUMN_USAGE = Similar to CONSTRAINT_TABLE_USAGE view, columns are identified for the various constraints that are owned by a given user. (Intermediate level)<br/>SQL_ISV_CONSTRAINT_TABLE_USAGE = Identifies the tables that are used by constraints (referential, unique, and assertions), and are owned by a given user. (Intermediate level)<br/>SQL_ISV_DOMAIN_CONSTRAINTS = Identifies the domain constraints (of the domains in the catalog) that can be accessed by a given user. (Intermediate level)<br/>SQL_ISV_DOMAINS = Identifies the domains defined in a catalog that can be accessed by the user. (Intermediate level)<br/>SQL_ISV_KEY_COLUMN_USAGE = Identifies columns defined in the catalog that are constrained as keys by a given user. (Intermediate level)<br/>SQL_ISV_REFERENTIAL_CONSTRAINTS = Identifies the referential constraints that are owned by a given user. (Intermediate level)<br/>SQL_ISV_SCHEMATA = Identifies the schemas that are owned by a given user. (Intermediate level)<br/>SQL_ISV_SQL_LANGUAGES = Identifies the SQL conformance levels, options, and dialects supported by the SQL implementation. (Intermediate level)<br/>SQL_ISV_TABLE_CONSTRAINTS = Identifies the table constraints that are owned by a given user. (Intermediate level)<br/>SQL_ISV_TABLE_PRIVILEGES = Identifies the privileges on persistent tables that are available to or granted by a given user. (FIPS Transitional level)<br/>SQL_ISV_TABLES = Identifies the persistent tables defined in a catalog that can be accessed by a given user. (FIPS Transitional level)<br/>SQL_ISV_TRANSLATIONS = Identifies character translations for the catalog that can be accessed by a given user. (Full level)<br/>SQL_ISV_USAGE_PRIVILEGES = Identifies the USAGE privileges on catalog objects that are available to or owned by a given user. (FIPS Transitional level)<br/>SQL_ISV_VIEW_COLUMN_USAGE = Identifies the columns on which the catalog's views that are owned by a given user are dependent. (Intermediate level)<br/>SQL_ISV_VIEW_TABLE_USAGE = Identifies the tables on which the catalog's views that are owned by a given user are dependent. (Intermediate level)<br/>SQL_ISV_VIEWS = Identifies the viewed tables defined in this catalog that can be accessed by a given user. (FIPS Transitional level)|
|SQL_INSERT_STATEMENT|3.0|An SQLUINTEGER bitmask that indicates support for **INSERT** statements:<br/>SQL_IS_INSERT_LITERALS<br/>SQL_IS_INSERT_SEARCHED<br/>SQL_IS_SELECT_INTO<br/><br/>An SQL-92 Entry level-conformant driver will always return all of these options as supported.|
|SQL_INTEGRITY|1.0|A character string: "Y" if the data source supports the Integrity Enhancement Facility; "N" if it does not.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_ODBC_SQL_OPT_IEF.|
|SQL_KEYSET_CURSOR_ATTRIBUTES1|3.0|An SQLUINTEGER bitmask that describes the attributes of a keyset cursor that are supported by the driver. This bitmask contains the first subset of attributes; for the second subset, see SQL_KEYSET_CURSOR_ATTRIBUTES2.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA1_NEXT<br/>SQL_CA1_ABSOLUTE<br/>SQL_CA1_RELATIVE<br/>SQL_CA1_BOOKMARK<br/>SQL_CA1_LOCK_EXCLUSIVE<br/>SQL_CA1_LOCK_NO_CHANGE<br/>SQL_CA1_LOCK_UNLOCK<br/>SQL_CA1_POS_POSITION<br/>SQL_CA1_POS_UPDATE<br/>SQL_CA1_POS_DELETE<br/>SQL_CA1_POS_REFRESH<br/>SQL_CA1_POSITIONED_UPDATE<br/>SQL_CA1_POSITIONED_DELETE<br/>SQL_CA1_SELECT_FOR_UPDATE<br/>SQL_CA1_BULK_ADD<br/>SQL_CA1_BULK_UPDATE_BY_BOOKMARK<br/>SQL_CA1_BULK_DELETE_BY_BOOKMARK<br/>SQL_CA1_BULK_FETCH_BY_BOOKMARK<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES1 (and substitute "keyset-driven cursor" for "dynamic cursor" in the descriptions).<br/><br/>An SQL-92 Intermediate level-conformant driver will usually return the SQL_CA1_NEXT, SQL_CA1_ABSOLUTE, and SQL_CA1_RELATIVE options as supported, because the driver supports scrollable cursors through the embedded SQL FETCH statement. Because this does not directly determine the underlying SQL support, however, scrollable cursors may not be supported, even for an SQL-92 Intermediate level-conformant driver.|
|SQL_KEYSET_CURSOR_ATTRIBUTES2|3.0|An SQLUINTEGER bitmask that describes the attributes of a keyset cursor that are supported by the driver. This bitmask contains the second subset of attributes; for the first subset, see SQL_KEYSET_CURSOR_ATTRIBUTES1.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA2_READ_ONLY_CONCURRENCY<br/>SQL_CA2_LOCK_CONCURRENCY<br/>SQL_CA2_OPT_ROWVER_CONCURRENCY<br/>SQL_CA2_OPT_VALUES_CONCURRENCY<br/>SQL_CA2_SENSITIVITY_ADDITIONS<br/>SQL_CA2_SENSITIVITY_DELETIONS<br/>SQL_CA2_SENSITIVITY_UPDATES<br/>SQL_CA2_MAX_ROWS_SELECT<br/>SQL_CA2_MAX_ROWS_INSERT<br/>SQL_CA2_MAX_ROWS_DELETE<br/>SQL_CA2_MAX_ROWS_UPDATE<br/>SQL_CA2_MAX_ROWS_CATALOG<br/>SQL_CA2_MAX_ROWS_AFFECTS_ALL<br/>SQL_CA2_CRC_EXACT<br/>SQL_CA2_CRC_APPROXIMATE<br/>SQL_CA2_SIMULATE_NON_UNIQUE<br/>SQL_CA2_SIMULATE_TRY_UNIQUE<br/>SQL_CA2_SIMULATE_UNIQUE<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES1 (and substitute "keyset-driven cursor" for "dynamic cursor" in the descriptions).|
|SQL_KEYWORDS|2.0|A character string that contains a comma-separated list of all data source-specific keywords. This list does not contain keywords specific to ODBC or keywords used by both the data source and ODBC. This list represents all the reserved keywords; interoperable applications should not use these words in object names.<br/><br/>For a list of ODBC keywords, see [Reserved Keywords](../appendixes/reserved-keywords.md) in [Appendix C: SQL Grammar](../appendixes/appendix-c-sql-grammar.md). The **#define** value SQL_ODBC_KEYWORDS contains a comma-separated list of ODBC keywords.|
|SQL_LIKE_ESCAPE_CLAUSE|2.0|A character string: "Y" if the data source supports an escape character for the percent character (%) and underscore character (_) in a **LIKE** predicate and the driver supports the ODBC syntax for defining a **LIKE** predicate escape character; "N" otherwise.|
|SQL_MAX_ASYNC_CONCURRENT_STATEMENTS|3.0|An SQLUINTEGER value that specifies the maximum number of active concurrent statements in asynchronous mode that the driver can support on a given connection. If there is no specific limit or the limit is unknown, this value is zero.|
|SQL_MAX_BINARY_LITERAL_LEN|2.0|An SQLUINTEGER value that specifies the maximum length (number of hexadecimal characters, excluding the literal prefix and suffix returned by **SQLGetTypeInfo**) of a binary literal in an SQL statement. For example, the binary literal 0xFFAA has a length of 4. If there is no maximum length or the length is unknown, this value is set to zero.|
|SQL_MAX_CATALOG_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a catalog name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.<br/><br/>An FIPS Full level-conformant driver will return at least 128.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_MAX_QUALIFIER_NAME_LEN.|
|SQL_MAX_CHAR_LITERAL_LEN|2.0|An SQLUINTEGER value that specifies the maximum length (number of characters, excluding the literal prefix and suffix returned by **SQLGetTypeInfo**) of a character literal in an SQL statement. If there is no maximum length or the length is unknown, this value is set to zero.|
|SQL_MAX_COLUMN_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a column name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 18. An FIPS Intermediate level-conformant driver will return at least 128.|
|SQL_MAX_COLUMNS_IN_GROUP_BY|2.0|An SQLUSMALLINT value that specifies the maximum number of columns allowed in a **GROUP BY** clause. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 6. An FIPS Intermediate level-conformant driver will return at least 15.|
|SQL_MAX_COLUMNS_IN_INDEX|2.0|An SQLUSMALLINT value that specifies the maximum number of columns allowed in an index. If there is no specified limit or the limit is unknown, this value is set to zero.|
|SQL_MAX_COLUMNS_IN_ORDER_BY|2.0|An SQLUSMALLINT value that specifies the maximum number of columns allowed in an **ORDER BY** clause. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 6. An FIPS Intermediate level-conformant driver will return at least 15.|
|SQL_MAX_COLUMNS_IN_SELECT|2.0|An SQLUSMALLINT value that specifies the maximum number of columns allowed in a select list. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 100. An FIPS Intermediate level-conformant driver will return at least 250.|
|SQL_MAX_COLUMNS_IN_TABLE|2.0|An SQLUSMALLINT value that specifies the maximum number of columns allowed in a table. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 100. An FIPS Intermediate level-conformant driver will return at least 250.|
|SQL_MAX_CONCURRENT_ACTIVITIES|1.0|An SQLUSMALLINT value that specifies the maximum number of active statements that the driver can support for a connection. A statement is defined as active if it has results pending, with the term "results" meaning rows from a **SELECT** operation or rows affected by an **INSERT**, **UPDATE**, or **DELETE** operation (such as a row count), or if it is in a NEED_DATA state. This value can reflect a limitation imposed by either the driver or the data source. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_ACTIVE_STATEMENTS.|
|SQL_MAX_CURSOR_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a cursor name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 18. An FIPS Intermediate level-conformant driver will return at least 128.|
|SQL_MAX_DRIVER_CONNECTIONS|1.0|An SQLUSMALLINT value that specifies the maximum number of active connections that the driver can support for an environment. This value can reflect a limitation imposed by either the driver or the data source. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_ACTIVE_CONNECTIONS.|
|SQL_MAX_IDENTIFIER_LEN|3.0|An SQLUSMALLINT that indicates the maximum size in characters that the data source supports for user-defined names.<br/><br/>An FIPS Entry level-conformant driver will return at least 18. An FIPS Intermediate level-conformant driver will return at least 128.|
|SQL_MAX_INDEX_SIZE|2.0|An SQLUINTEGER value that specifies the maximum number of bytes allowed in the combined fields of an index. If there is no specified limit or the limit is unknown, this value is set to zero.|
|SQL_MAX_PROCEDURE_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a procedure name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.|
|SQL_MAX_ROW_SIZE|2.0|An SQLUINTEGER value that specifies the maximum length of a single row in a table. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 2,000. An FIPS Intermediate level-conformant driver will return at least 8,000.|
|SQL_MAX_ROW_SIZE_INCLUDES_LONG|3.0|A character string: "Y" if the maximum row size returned for the SQL_MAX_ROW_SIZE information type includes the length of all SQL_LONGVARCHAR and SQL_LONGVARBINARY columns in the row; "N" otherwise.|
|SQL_MAX_SCHEMA_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a schema name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 18. An FIPS Intermediate level-conformant driver will return at least 128.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_MAX_OWNER_NAME_LEN.|
|SQL_MAX_STATEMENT_LEN|2.0|An SQLUINTEGER value that specifies the maximum length (number of characters, including white space) of an SQL statement. If there is no maximum length or the length is unknown, this value is set to zero.|
|SQL_MAX_TABLE_NAME_LEN|1.0|An SQLUSMALLINT value that specifies the maximum length of a table name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 18. An FIPS Intermediate level-conformant driver will return at least 128.|
|SQL_MAX_TABLES_IN_SELECT|2.0|An SQLUSMALLINT value that specifies the maximum number of tables allowed in the **FROM** clause of a **SELECT** statement. If there is no specified limit or the limit is unknown, this value is set to zero.<br/><br/>An FIPS Entry level-conformant driver will return at least 15. An FIPS Intermediate level-conformant driver will return at least 50.|
|SQL_MAX_USER_NAME_LEN|2.0|An SQLUSMALLINT value that specifies the maximum length of a user name in the data source. If there is no maximum length or the length is unknown, this value is set to zero.|
|SQL_MULT_RESULT_SETS|1.0|A character string: "Y" if the data source supports multiple result sets, "N" if it does not.<br/><br/>For more information about multiple result sets, see [Multiple Results](../develop-app/multiple-results.md).|
|SQL_MULTIPLE_ACTIVE_TXN|1.0|A character string: "Y" if the driver supports more than one active transaction at the same time, "N" if only one transaction can be active at any time.<br/><br/>The information returned for this information type does not apply in the case of distributed transactions.|
|SQL_NEED_LONG_DATA_LEN|2.0|A character string: "Y" if the data source needs the length of a long data value (the data type is SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type) before that value is sent to the data source, "N" if it does not. For more information, see [SQLBindParameter Function](sqlbindparameter-function.md) and [SQLSetPos Function](sqlsetpos-function.md).|
|SQL_NON_NULLABLE_COLUMNS|1.0|An SQLUSMALLINT value that specifies whether the data source supports NOT NULL in columns:<br/>SQL_NNC_NULL = All columns must be nullable.<br/>SQL_NNC_NON_NULL = Columns cannot be nullable. (The data source supports the **NOT NULL** column constraint in **CREATE TABLE** statements.)<br/><br/>An SQL-92 Entry level-conformant driver will return SQL_NNC_NON_NULL.|
|SQL_NULL_COLLATION|2.0|An SQLUSMALLINT value that specifies where NULLs are sorted in a result set:<br/>SQL_NC_END = NULLs are sorted at the end of the result set, regardless of the ASC or DESC keywords.<br/>SQL_NC_HIGH = NULLs are sorted at the high end of the result set, depending on the ASC or DESC keywords.<br/>SQL_NC_LOW = NULLs are sorted at the low end of the result set, depending on the ASC or DESC keywords.<br/>SQL_NC_START = NULLs are sorted at the start of the result set, regardless of the ASC or DESC keywords.|
|SQL_NUMERIC_FUNCTIONS|1.0|Note: The information type was introduced in ODBC 1.0; each bitmask is labeled with the version in which it was introduced.<br/><br/>An SQLUINTEGER bitmask enumerating the scalar numeric functions supported by the driver and associated data source.<br/><br/>The following bitmasks are used to determine which numeric functions are supported:<br/>SQL_FN_NUM_ABS (ODBC 1.0)<br/>SQL_FN_NUM_ACOS (ODBC 1.0)<br/>SQL_FN_NUM_ASIN (ODBC 1.0)<br/>SQL_FN_NUM_ATAN (ODBC 1.0)<br/>SQL_FN_NUM_ATAN2 (ODBC 1.0)<br/>SQL_FN_NUM_CEILING (ODBC 1.0)<br/>SQL_FN_NUM_COS (ODBC 1.0)<br/>SQL_FN_NUM_COT (ODBC 1.0)<br/>SQL_FN_NUM_DEGREES (ODBC 2.0)<br/>SQL_FN_NUM_EXP (ODBC 1.0)<br/>SQL_FN_NUM_FLOOR (ODBC 1.0)<br/>SQL_FN_NUM_LOG (ODBC 1.0)<br/>SQL_FN_NUM_LOG10 (ODBC 2.0)<br/>SQL_FN_NUM_MOD (ODBC 1.0)<br/>SQL_FN_NUM_PI (ODBC 1.0)<br/>SQL_FN_NUM_POWER (ODBC 2.0)<br/>SQL_FN_NUM_RADIANS (ODBC 2.0)<br/>SQL_FN_NUM_RAND (ODBC 1.0)<br/>SQL_FN_NUM_ROUND (ODBC 2.0)<br/>SQL_FN_NUM_SIGN (ODBC 1.0)<br/>SQL_FN_NUM_SIN (ODBC 1.0)<br/>SQL_FN_NUM_SQRT (ODBC 1.0)<br/>SQL_FN_NUM_TAN (ODBC 1.0)<br/>SQL_FN_NUM_TRUNCATE (ODBC 2.0)|
|SQL_ODBC_INTERFACE_CONFORMANCE|3.0|An SQLUINTEGER value that indicates the level of the ODBC 3*.x* interface that the driver complies with.<br/><br/>SQL_OIC_CORE: The minimum level that all ODBC drivers are expected to comply with. This level includes basic interface elements such as connection functions, functions for preparing and executing an SQL statement, basic result set metadata functions, basic catalog functions, and so on.<br/>SQL_OIC_LEVEL1: A level including the core standards compliance level functionality, plus scrollable cursors, bookmarks, positioned updates and deletes, and so on.<br/>SQL_OIC_LEVEL2: A level including level 1 standards compliance level functionality, plus advanced features such as sensitive cursors; update, delete, and refresh by bookmarks; stored procedure support; catalog functions for primary and foreign keys; multi-catalog support; and so on.<br/><br/>For more information, see [Interface Conformance Levels](../develop-app/interface-conformance-levels.md).|
|SQL_ODBC_VER|1.0|A character string with the version of ODBC to which the Driver Manager conforms. The version is of the form ##.##.0000, where the first two digits are the major version and the next two digits are the minor version. This is implemented only in the Driver Manager.|
|SQL_OJ_CAPABILITIES|2.01|An SQLUINTEGER bitmask enumerating the types of outer joins supported by the driver and data source. The following bitmasks are used to determine which types are supported:<br/>SQL_OJ_LEFT = Left outer joins are supported.<br/>SQL_OJ_RIGHT = Right outer joins are supported.<br/>SQL_OJ_FULL = Full outer joins are supported.<br/>SQL_OJ_NESTED = Nested outer joins are supported.<br/>SQL_OJ_NOT_ORDERED = The column names in the ON clause of the outer join do not have to be in the same order as their respective table names in the **OUTER JOIN** clause.<br/>SQL_OJ_INNER = The inner table (the right table in a left outer join or the left table in a right outer join) can also be used in an inner join. This does not apply to full outer joins, which do not have an inner table.<br/>SQL_OJ_ALL_COMPARISON_OPS = The comparison operator in the ON clause can be any of the ODBC comparison operators. If this bit is not set, only the equals (=) comparison operator can be used in outer joins.<br/><br/>If none of these options is returned as supported, no outer join clause is supported.<br/><br/>For information about the support of relational join operators in a SELECT statement, as defined by SQL-92, see SQL_SQL92_RELATIONAL_JOIN_OPERATORS.|
|SQL_ORDER_BY_COLUMNS_IN_SELECT|2.0|A character string: "Y" if the columns in the **ORDER BY** clause must be in the select list; otherwise, "N".|
|SQL_PARAM_ARRAY_ROW_COUNTS|3.0|An SQLUINTEGER enumerating the driver's properties regarding the availability of row counts in a parameterized execution. Has the following values:<br/>SQL_PARC_BATCH = Individual row counts are available for each set of parameters. This is conceptually equivalent to the driver generating a batch of SQL statements, one for each parameter set in the array. Extended error information can be retrieved by using the SQL_PARAM_STATUS_PTR descriptor field.<br/>SQL_PARC_NO_BATCH = There is only one row count available, which is the cumulative row count resulting from the execution of the statement for the entire array of parameters. This is conceptually equivalent to treating the statement together with the complete parameter array as one atomic unit. Errors are handled the same as if one statement were executed.|
|SQL_PARAM_ARRAY_SELECTS|3.0|An SQLUINTEGER enumerating the driver's properties regarding the availability of result sets in a parameterized execution. Has the following values:<br/>SQL_PAS_BATCH = There is one result set available per set of parameters. This is conceptually equivalent to the driver generating a batch of SQL statements, one for each parameter set in the array.<br/>SQL_PAS_NO_BATCH = There is only one result set available, which represents the cumulative result set resulting from the execution of the statement for the complete array of parameters. This is conceptually equivalent to treating the statement together with the complete parameter array as one atomic unit.<br/>SQL_PAS_NO_SELECT = A driver does not allow a result-set generating statement to be executed with an array of parameters.|
|SQL_POS_OPERATIONS|2.0|An SQLINTEGER bitmask enumerating the support operations in **SQLSetPos**.<br/><br/>The following bitmasks are used together with the flag to determine which options are supported.<br/>SQL_POS_POSITION (ODBC 2.0)<br/>SQL_POS_REFRESH (ODBC 2.0)<br/>SQL_POS_UPDATE (ODBC 2.0)<br/>SQL_POS_DELETE (ODBC 2.0)<br/>SQL_POS_ADD (ODBC 2.0)|
|SQL_PROCEDURE_TERM|1.0|A character string with the data source vendor's name for a procedure; for example, "database procedure", "stored procedure", "procedure", "package", or "stored query".|
|SQL_PROCEDURES|1.0|A character string: "Y" if the data source supports procedures and the driver supports the ODBC procedure invocation syntax; "N" otherwise.|
|SQL_QUOTED_IDENTIFIER_CASE|2.0|An SQLUSMALLINT value as follows:<br/>SQL_IC_UPPER = Quoted identifiers in SQL are not case-sensitive and are stored in uppercase in the system catalog.<br/>SQL_IC_LOWER = Quoted identifiers in SQL are not case-sensitive and are stored in lowercase in the system catalog.<br/>SQL_IC_SENSITIVE = Quoted identifiers in SQL are case sensitive and are stored in mixed case in the system catalog. (In an SQL-92-compliant database, quoted identifiers are always case-sensitive.)<br/>SQL_IC_MIXED = Quoted identifiers in SQL are not case-sensitive and are stored in mixed case in the system catalog.<br/><br/>An SQL-92 Entry level-conformant driver will always return SQL_IC_SENSITIVE.|
|SQL_ROW_UPDATES|1.0|A character string: "Y" if a keyset-driven or mixed cursor maintains row versions or values for all fetched rows and therefore can detect any updates that were made to a row by any user since the row was last fetched. (This applies only to updates, not to deletions or insertions.) The driver can return the SQL_ROW_UPDATED flag to the row status array when **SQLFetchScroll** is called. Otherwise, "N".|
|SQL_SCHEMA_TERM|1.0|A character string with the data source vendor's name for a schema; for example, "owner", "Authorization ID", or "Schema".<br/><br/>The character string can be returned in upper, lower, or mixed case.<br/><br/>An SQL-92 Entry level-conformant driver will always return "schema".<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_OWNER_TERM.|
|SQL_SCHEMA_USAGE|2.0|An SQLUINTEGER bitmask enumerating the statements in which schemas can be used:<br/>SQL_SU_DML_STATEMENTS = Schemas are supported in all Data Manipulation Language statements: **SELECT**, **INSERT**, **UPDATE**, **DELETE**, and if supported, **SELECT FOR UPDATE** and positioned update and delete statements.<br/>SQL_SU_PROCEDURE_INVOCATION = Schemas are supported in the ODBC procedure invocation statement.<br/>SQL_SU_TABLE_DEFINITION = Schemas are supported in all table definition statements: **CREATE TABLE**, **CREATE VIEW**, **ALTER TABLE**, **DROP TABLE**, and **DROP VIEW**.<br/>SQL_SU_INDEX_DEFINITION = Schemas are supported in all index definition statements: **CREATE INDEX** and **DROP INDEX**.<br/>SQL_SU_PRIVILEGE_DEFINITION = Schemas are supported in all privilege definition statements: **GRANT** and **REVOKE**.<br/><br/>An SQL-92 Entry level-conformant driver will always return the SQL_SU_DML_STATEMENTS, SQL_SU_TABLE_DEFINITION, and SQL_SU_PRIVILEGE_DEFINITION options, as supported.<br/><br/>This *InfoType* has been renamed for ODBC 3.0 from the ODBC 2.0 *InfoType* SQL_OWNER_USAGE.|
|SQL_SCROLL_OPTIONS|1.0|Note: The information type was introduced in ODBC 1.0; each bitmask is labeled with the version in which it was introduced.<br/><br/>An SQLUINTEGER bitmask enumerating the scroll options supported for scrollable cursors.<br/><br/>The following bitmasks are used to determine which options are supported:<br/>SQL_SO_FORWARD_ONLY = The cursor only scrolls forward. (ODBC 1.0)<br/>SQL_SO_STATIC = The data in the result set is static. (ODBC 2.0)<br/>SQL_SO_KEYSET_DRIVEN = The driver saves and uses the keys for every row in the result set. (ODBC 1.0)<br/>SQL_SO_DYNAMIC = The driver keeps the keys for every row in the rowset (the keyset size is the same as the rowset size). (ODBC 1.0)<br/>SQL_SO_MIXED = The driver keeps the keys for every row in the keyset, and the keyset size is greater than the rowset size. The cursor is keyset-driven inside the keyset and dynamic outside the keyset. (ODBC 1.0)<br/><br/>For information about scrollable cursors, see [Scrollable Cursors](../develop-app/scrollable-cursors.md).|
|SQL_SEARCH_PATTERN_ESCAPE|1.0|A character string specifying what the driver supports as an escape character that allows the use of the pattern match metacharacters underscore (_) and percent sign (%) as valid characters in search patterns. This escape character applies only for those catalog function arguments that support search strings. If this string is empty, the driver does not support a search-pattern escape character.<br/><br/>Because this information type does not indicate general support of the escape character in the **LIKE** predicate, SQL-92 does not include requirements for this character string.<br/><br/>This *InfoType* is limited to catalog functions. For a description of the use of the escape character in search pattern strings, see [Pattern Value Arguments](../develop-app/pattern-value-arguments.md).|
|SQL_SERVER_NAME|1.0|A character string with the actual data source-specific server name; useful when a data source name is used during **SQLConnect**, **SQLDriverConnect**, and **SQLBrowseConnect**.|
|SQL_SPECIAL_CHARACTERS|2.0|A character string that contains all special characters (that is, all characters except a through z, A through Z, 0 through 9, and underscore) that can be used in an identifier name, such as a table name, column name, or index name, on the data source. For example, "#$^". If an identifier contains one or more of these characters, the identifier must be a delimited identifier.|
|SQL_SQL_CONFORMANCE|3.0|An SQLUINTEGER value that indicates the level of SQL-92 supported by the driver:<br/>SQL_SC_SQL92_ENTRY = Entry level SQL-92 compliant.<br/>SQL_SC_FIPS127_2_TRANSITIONAL = FIPS 127-2 transitional level compliant.<br/>SQL_SC_SQL92_FULL = Full level SQL-92 compliant.<br/>SQL_SC_ SQL92_INTERMEDIATE = Intermediate level SQL-92 compliant.|
|SQL_SQL92_DATETIME_FUNCTIONS|3.0|An SQLUINTEGER bitmask enumerating the datetime scalar functions that are supported by the driver and the associated data source, as defined in SQL-92.<br/><br/>The following bitmasks are used to determine which datetime functions are supported:<br/>SQL_SDF_CURRENT_DATE<br/>SQL_SDF_CURRENT_TIME<br/>SQL_SDF_CURRENT_TIMESTAMP|
|SQL_SQL92_FOREIGN_KEY_DELETE_RULE|3.0|An SQLUINTEGER bitmask enumerating the rules supported for a foreign key in a **DELETE** statement, as defined in SQL-92.<br/><br/>The following bitmasks are used to determine which clauses are supported by the data source:<br/>SQL_SFKD_CASCADE<br/>SQL_SFKD_NO_ACTION<br/>SQL_SFKD_SET_DEFAULT<br/>SQL_SFKD_SET_NULL<br/><br/>An FIPS Transitional level-conformant driver will always return all of these options as supported.|
|SQL_SQL92_FOREIGN_KEY_UPDATE_RULE|3.0|An SQLUINTEGER bitmask enumerating the rules supported for a foreign key in an **UPDATE** statement, as defined in SQL-92.<br/><br/>The following bitmasks are used to determine which clauses are supported by the data source:<br/>SQL_SFKU_CASCADE<br/>SQL_SFKU_NO_ACTION<br/>SQL_SFKU_SET_DEFAULT<br/>SQL_SFKU_SET_NULL<br/><br/>An SQL-92 Full level-conformant driver will always return all of these options as supported.|
|SQL_SQL92_GRANT|3.0|An SQLUINTEGER bitmask enumerating the clauses supported in the **GRANT** statement, as defined in SQL-92.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which clauses are supported by the data source:<br/>SQL_SG_DELETE_TABLE (Entry level)<br/>SQL_SG_INSERT_COLUMN (Intermediate level)<br/>SQL_SG_INSERT_TABLE (Entry level)<br/>SQL_SG_REFERENCES_TABLE (Entry level)<br/>SQL_SG_REFERENCES_COLUMN (Entry level)<br/>SQL_SG_SELECT_TABLE (Entry level)<br/>SQL_SG_UPDATE_COLUMN (Entry level)<br/>SQL_SG_UPDATE_TABLE (Entry level)<br/>SQL_SG_USAGE_ON_DOMAIN (FIPS Transitional level)<br/>SQL_SG_USAGE_ON_CHARACTER_SET (FIPS Transitional level)<br/>SQL_SG_USAGE_ON_COLLATION (FIPS Transitional level)<br/>SQL_SG_USAGE_ON_TRANSLATION (FIPS Transitional level)<br/>SQL_SG_WITH_GRANT_OPTION (Entry level)|
|SQL_SQL92_NUMERIC_VALUE_FUNCTIONS|3.0|An SQLUINTEGER bitmask enumerating the numeric value scalar functions that are supported by the driver and the associated data source, as defined in SQL-92.<br/><br/>The following bitmasks are used to determine which numeric functions are supported:<br/>SQL_SNVF_BIT_LENGTH<br/>SQL_SNVF_CHAR_LENGTH<br/>SQL_SNVF_CHARACTER_LENGTH<br/>SQL_SNVF_EXTRACT<br/>SQL_SNVF_OCTET_LENGTH<br/>SQL_SNVF_POSITION|
|SQL_SQL92_PREDICATES|3.0|An SQLUINTEGER bitmask enumerating the predicates supported in a **SELECT** statement, as defined in SQL-92.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which options are supported by the data source:<br/>SQL_SP_BETWEEN (Entry level)<br/>SQL_SP_COMPARISON (Entry level)<br/>SQL_SP_EXISTS (Entry level)<br/>SQL_SP_IN (Entry level)<br/>SQL_SP_ISNOTNULL (Entry level)<br/>SQL_SP_ISNULL (Entry level)<br/>SQL_SP_LIKE (Entry level)<br/>SQL_SP_MATCH_FULL (Full level)<br/>SQL_SP_MATCH_PARTIAL(Full level)<br/>SQL_SP_MATCH_UNIQUE_FULL (Full level)<br/>SQL_SP_MATCH_UNIQUE_PARTIAL (Full level)<br/>SQL_SP_OVERLAPS (FIPS Transitional level)<br/>SQL_SP_QUANTIFIED_COMPARISON (Entry level)<br/>SQL_SP_UNIQUE (Entry level)|
|SQL_SQL92_RELATIONAL_JOIN_OPERATORS|3.0|An SQLUINTEGER bitmask enumerating the relational join operators supported in a **SELECT** statement, as defined in SQL-92.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which options are supported by the data source:<br/>SQL_SRJO_CORRESPONDING_CLAUSE (Intermediate level)<br/>SQL_SRJO_CROSS_JOIN (Full level)<br/>SQL_SRJO_EXCEPT_JOIN (Intermediate level)<br/>SQL_SRJO_FULL_OUTER_JOIN (Intermediate level)<br/>SQL_SRJO_INNER_JOIN (FIPS Transitional level)<br/>SQL_SRJO_INTERSECT_JOIN (Intermediate level)<br/>SQL_SRJO_LEFT_OUTER_JOIN (FIPS Transitional level)<br/>SQL_SRJO_NATURAL_JOIN (FIPS Transitional level)<br/>SQL_SRJO_RIGHT_OUTER_JOIN (FIPS Transitional level)<br/>SQL_SRJO_UNION_JOIN (Full level)<br/><br/>SQL_SRJO_INNER_JOIN indicates support for the **INNER JOIN** syntax, not for the inner join capability. Support for the **INNER JOIN** syntax is FIPS TRANSITIONAL, whereas support for the inner join capability is **ENTRY**.|
|SQL_SQL92_REVOKE|3.0|An SQLUINTEGER bitmask enumerating the clauses supported in the **REVOKE** statement, as defined in SQL-92, supported by the data source.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which clauses are supported by the data source:<br/>SQL_SR_CASCADE (FIPS Transitional level)<br/>SQL_SR_DELETE_TABLE (Entry level)<br/>SQL_SR_GRANT_OPTION_FOR (Intermediate level)<br/>SQL_SR_INSERT_COLUMN (Intermediate level)<br/>SQL_SR_INSERT_TABLE (Entry level)<br/>SQL_SR_REFERENCES_COLUMN (Entry level)<br/>SQL_SR_REFERENCES_TABLE (Entry level)<br/>SQL_SR_RESTRICT (FIPS Transitional level)<br/>SQL_SR_SELECT_TABLE (Entry level)<br/>SQL_SR_UPDATE_COLUMN (Entry level)<br/>SQL_SR_UPDATE_TABLE (Entry level)<br/>SQL_SR_USAGE_ON_DOMAIN (FIPS Transitional level)<br/>SQL_SR_USAGE_ON_CHARACTER_SET (FIPS Transitional level)<br/>SQL_SR_USAGE_ON_COLLATION (FIPS Transitional level)<br/>SQL_SR_USAGE_ON_TRANSLATION (FIPS Transitional level)|
|SQL_SQL92_ROW_VALUE_CONSTRUCTOR|3.0|An SQLUINTEGER bitmask enumerating the row value constructor expressions supported in a **SELECT** statement, as defined in SQL-92. The following bitmasks are used to determine which options are supported by the data source:<br/>SQL_SRVC_VALUE_EXPRESSION<br/>SQL_SRVC_NULL<br/>SQL_SRVC_DEFAULT<br/>SQL_SRVC_ROW_SUBQUERY|
|SQL_SQL92_STRING_FUNCTIONS|3.0|An SQLUINTEGER bitmask enumerating the string scalar functions that are supported by the driver and the associated data source, as defined in SQL-92.<br/><br/>The following bitmasks are used to determine which string functions are supported:<br/>SQL_SSF_CONVERT<br/>SQL_SSF_LOWERSQL_SSF_UPPER<br/>SQL_SSF_SUBSTRING<br/>SQL_SSF_TRANSLATE<br/>SQL_SSF_TRIM_BOTH<br/>SQL_SSF_TRIM_LEADING<br/>SQL_SSF_TRIM_TRAILING|
|SQL_SQL92_VALUE_EXPRESSIONS|3.0|An SQLUINTEGER bitmask enumerating the value expressions supported, as defined in SQL-92.<br/><br/>The SQL-92 or FIPS conformance level at which this feature must be supported is shown in parentheses next to each bitmask.<br/><br/>The following bitmasks are used to determine which options are supported by the data source:<br/>SQL_SVE_CASE (Intermediate level)<br/>SQL_SVE_CAST (FIPS Transitional level)<br/>SQL_SVE_COALESCE (Intermediate level)<br/>SQL_SVE_NULLIF (Intermediate level)|
|SQL_STANDARD_CLI_CONFORMANCE|3.0|An SQLUINTEGER bitmask enumerating the CLI standard or standards to which the driver conforms. The following bitmasks are used to determine which levels the driver complies with:<br/>SQL_SCC_XOPEN_CLI_VERSION1: The driver complies with the Open Group CLI version 1.<br/>SQL_SCC_ISO92_CLI: The driver complies with the ISO 92 CLI.|
|SQL_STATIC_CURSOR_ATTRIBUTES1|3.0|An SQLUINTEGER bitmask that describes the attributes of a static cursor that are supported by the driver. This bitmask contains the first subset of attributes; for the second subset, see SQL_STATIC_CURSOR_ATTRIBUTES2.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA1_NEXT<br/>SQL_CA1_ABSOLUTE<br/>SQL_CA1_RELATIVE<br/>SQL_CA1_BOOKMARK<br/>SQL_CA1_LOCK_NO_CHANGE<br/>SQL_CA1_LOCK_EXCLUSIVE<br/>SQL_CA1_LOCK_UNLOCK<br/>SQL_CA1_POS_POSITION<br/>SQL_CA1_POS_UPDATE<br/>SQL_CA1_POS_DELETE<br/>SQL_CA1_POS_REFRESH<br/>SQL_CA1_POSITIONED_UPDATE<br/>SQL_CA1_POSITIONED_DELETE<br/>SQL_CA1_SELECT_FOR_UPDATE<br/>SQL_CA1_BULK_ADD<br/>SQL_CA1_BULK_UPDATE_BY_BOOKMARK<br/>SQL_CA1_BULK_DELETE_BY_BOOKMARK<br/>SQL_CA1_BULK_FETCH_BY_BOOKMARK<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES1 (and substitute "static cursor" for "dynamic cursor" in the descriptions).<br/><br/>An SQL-92 Intermediate level-conformant driver will usually return the SQL_CA1_NEXT, SQL_CA1_ABSOLUTE, and SQL_CA1_RELATIVE options as supported, because the driver supports scrollable cursors through the embedded SQL FETCH statement. Because this does not directly determine the underlying SQL support, however, scrollable cursors may not be supported, even for an SQL-92 Intermediate level-conformant driver.|
|SQL_STATIC_CURSOR_ATTRIBUTES2|3.0|An SQLUINTEGER bitmask that describes the attributes of a static cursor that are supported by the driver. This bitmask contains the second subset of attributes; for the first subset, see SQL_STATIC_CURSOR_ATTRIBUTES1.<br/><br/>The following bitmasks are used to determine which attributes are supported:<br/>SQL_CA2_READ_ONLY_CONCURRENCY<br/>SQL_CA2_LOCK_CONCURRENCY<br/>SQL_CA2_OPT_ROWVER_CONCURRENCY<br/>SQL_CA2_OPT_VALUES_CONCURRENCY<br/>SQL_CA2_SENSITIVITY_ADDITIONS<br/>SQL_CA2_SENSITIVITY_DELETIONS<br/>SQL_CA2_SENSITIVITY_UPDATES<br/>SQL_CA2_MAX_ROWS_SELECT<br/>SQL_CA2_MAX_ROWS_INSERT<br/>SQL_CA2_MAX_ROWS_DELETE<br/>SQL_CA2_MAX_ROWS_UPDATE<br/>SQL_CA2_MAX_ROWS_CATALOG<br/>SQL_CA2_MAX_ROWS_AFFECTS_ALL<br/>SQL_CA2_CRC_EXACT<br/>SQL_CA2_CRC_APPROXIMATE<br/>SQL_CA2_SIMULATE_NON_UNIQUE<br/>SQL_CA2_SIMULATE_TRY_UNIQUE<br/>SQL_CA2_SIMULATE_UNIQUE<br/><br/>For descriptions of these bitmasks, see SQL_DYNAMIC_CURSOR_ATTRIBUTES2 (and substitute "static cursor" for "dynamic cursor" in the descriptions).|
|SQL_STRING_FUNCTIONS|1.0|Note: The information type was introduced in ODBC 1.0; each bitmask is labeled with the version in which it was introduced.<br/><br/>An SQLUINTEGER bitmask enumerating the scalar string functions supported by the driver and associated data source.<br/><br/>The following bitmasks are used to determine which string functions are supported:<br/>SQL_FN_STR_ASCII (ODBC 1.0)<br/>SQL_FN_STR_BIT_LENGTH (ODBC 3.0)<br/>SQL_FN_STR_CHAR (ODBC 1.0)<br/>SQL_FN_STR_CHAR_LENGTH (ODBC 3.0)<br/>SQL_FN_STR_CHARACTER_LENGTH (ODBC 3.0)<br/>SQL_FN_STR_CONCAT (ODBC 1.0)<br/>SQL_FN_STR_DIFFERENCE (ODBC 2.0)<br/>SQL_FN_STR_INSERT (ODBC 1.0)<br/>SQL_FN_STR_LCASE (ODBC 1.0)<br/>SQL_FN_STR_LEFT (ODBC 1.0)<br/>SQL_FN_STR_LENGTH (ODBC 1.0)<br/>SQL_FN_STR_LOCATE (ODBC 1.0)<br/>SQL_FN_STR_LTRIM (ODBC 1.0)<br/>SQL_FN_STR_OCTET_LENGTH (ODBC 3.0)<br/>SQL_FN_STR_POSITION (ODBC 3.0)<br/>SQL_FN_STR_REPEAT (ODBC 1.0)<br/>SQL_FN_STR_REPLACE (ODBC 1.0)<br/>SQL_FN_STR_RIGHT (ODBC 1.0)<br/>SQL_FN_STR_RTRIM (ODBC 1.0)<br/>SQL_FN_STR_SOUNDEX (ODBC 2.0)<br/>SQL_FN_STR_SPACE (ODBC 2.0)<br/>SQL_FN_STR_SUBSTRING (ODBC 1.0)<br/>SQL_FN_STR_UCASE (ODBC 1.0)<br/><br/>If an application can call the **LOCATE** scalar function with the *string_exp1*, *string_exp2*, and *start* arguments, the driver returns the SQL_FN_STR_LOCATE bitmask. If an application can call the LOCATE scalar function with only the *string_exp1* and *string_exp2* arguments, the driver returns the SQL_FN_STR_LOCATE_2 bitmask. Drivers that fully support the **LOCATE** scalar function return both bitmasks.<br/><br/>(For more information, see [String Functions](../appendixes/string-functions.md) in Appendix E, "Scalar Functions.")|
|SQL_SUBQUERIES|2.0|An SQLUINTEGER bitmask enumerating the predicates that support subqueries:<br/>SQL_SQ_CORRELATED_SUBQUERIES<br/>SQL_SQ_COMPARISON<br/>SQL_SQ_EXISTS<br/>SQL_SQ_INSQL_SQ_QUANTIFIED<br/><br/>The SQL_SQ_CORRELATED_SUBQUERIES bitmask indicates that all predicates that support subqueries support correlated subqueries.<br/><br/>An SQL-92 Entry level-conformant driver will always return a bitmask in which all of these bits are set.|
|SQL_SYSTEM_FUNCTIONS|1.0|An SQLUINTEGER bitmask enumerating the scalar system functions supported by the driver and associated data source.<br/><br/>The following bitmasks are used to determine which system functions are supported:<br/>SQL_FN_SYS_DBNAME<br/>SQL_FN_SYS_IFNULL<br/>SQL_FN_SYS_USERNAME|
|SQL_TABLE_TERM|1.0|A character string with the data source vendor's name for a table; for example, "table" or "file".<br/><br/>This character string can be in upper, lower, or mixed case.<br/><br/>An SQL-92 Entry level-conformant driver will always return "table".|
|SQL_TIMEDATE_ADD_INTERVALS|2.0|An SQLUINTEGER bitmask enumerating the timestamp intervals supported by the driver and associated data source for the TIMESTAMPADD scalar function.<br/><br/>The following bitmasks are used to determine which intervals are supported:<br/>SQL_FN_TSI_FRAC_SECOND<br/>SQL_FN_TSI_SECOND<br/>SQL_FN_TSI_MINUTE<br/>SQL_FN_TSI_HOUR<br/>SQL_FN_TSI_DAY<br/>SQL_FN_TSI_WEEK<br/>SQL_FN_TSI_MONTH<br/>SQL_FN_TSI_QUARTER<br/>SQL_FN_TSI_YEAR<br/><br/>An FIPS Transitional level-conformant driver will always return a bitmask in which all of these bits are set.|
|SQL_TIMEDATE_DIFF_INTERVALS|2.0|An SQLUINTEGER bitmask enumerating the timestamp intervals supported by the driver and associated data source for the TIMESTAMPDIFF scalar function.<br/><br/>The following bitmasks are used to determine which intervals are supported:<br/>SQL_FN_TSI_FRAC_SECOND<br/>SQL_FN_TSI_SECOND<br/>SQL_FN_TSI_MINUTE<br/>SQL_FN_TSI_HOUR<br/>SQL_FN_TSI_DAY<br/>SQL_FN_TSI_WEEK<br/>SQL_FN_TSI_MONTH<br/>SQL_FN_TSI_QUARTER<br/>SQL_FN_TSI_YEAR<br/><br/>An FIPS Transitional level-conformant driver will always return a bitmask in which all of these bits are set.|
|SQL_TIMEDATE_FUNCTIONS|1.0|Note: The information type was introduced in ODBC 1.0; each bitmask is labeled with the version in which it was introduced.<br/><br/>An SQLUINTEGER bitmask enumerating the scalar date and time functions supported by the driver and associated data source.<br/><br/>The following bitmasks are used to determine which date and time functions are supported:<br/>SQL_FN_TD_CURRENT_DATE (ODBC 3.0)<br/>SQL_FN_TD_CURRENT_TIME (ODBC 3.0)<br/>SQL_FN_TD_CURRENT_TIMESTAMP (ODBC 3.0)<br/>SQL_FN_TD_CURDATE (ODBC 1.0)<br/>SQL_FN_TD_CURTIME (ODBC 1.0)<br/>SQL_FN_TD_DAYNAME (ODBC 2.0)<br/>SQL_FN_TD_DAYOFMONTH (ODBC 1.0)<br/>SQL_FN_TD_DAYOFWEEK (ODBC 1.0)<br/>SQL_FN_TD_DAYOFYEAR (ODBC 1.0)<br/>SQL_FN_TD_EXTRACT (ODBC 3.0)<br/>SQL_FN_TD_HOUR (ODBC 1.0)<br/>SQL_FN_TD_MINUTE (ODBC 1.0)<br/>SQL_FN_TD_MONTH (ODBC 1.0)<br/>SQL_FN_TD_MONTHNAME (ODBC 2.0)<br/>SQL_FN_TD_NOW (ODBC 1.0)<br/>SQL_FN_TD_QUARTER (ODBC 1.0)<br/>SQL_FN_TD_SECOND (ODBC 1.0)<br/>SQL_FN_TD_TIMESTAMPADD (ODBC 2.0)<br/>SQL_FN_TD_TIMESTAMPDIFF (ODBC 2.0)<br/>SQL_FN_TD_WEEK (ODBC 1.0)<br/>SQL_FN_TD_YEAR (ODBC 1.0)|
|SQL_TXN_CAPABLE|1.0|Note: The information type was introduced in ODBC 1.0; each return value is labeled with the version in which it was introduced.<br/><br/>An SQLUSMALLINT value describing the transaction support in the driver or data source:<br/>SQL_TC_NONE = Transactions not supported. (ODBC 1.0)<br/>SQL_TC_DML = Transactions can contain only Data Manipulation Language (DML) statements (**SELECT**, **INSERT**, **UPDATE**, **DELETE**). Data Definition Language (DDL) statements encountered in a transaction cause an error. (ODBC 1.0)<br/>SQL_TC_DDL_COMMIT = Transactions can contain only DML statements. DDL statements (**CREATE TABLE**, **DROP INDEX**, and so on) encountered in a transaction cause the transaction to be committed. (ODBC 2.0)<br/>SQL_TC_DDL_IGNORE = Transactions can contain only DML statements. DDL statements encountered in a transaction are ignored. (ODBC 2.0)<br/>SQL_TC_ALL = Transactions can contain DDL statements and DML statements in any order. (ODBC 1.0)<br/><br/>(Because support of transactions is mandatory in SQL-92, an SQL-92 conformant driver [any level] will never return SQL_TC_NONE.)|
|SQL_TXN_ISOLATION_OPTION|1.0|An SQLUINTEGER bitmask enumerating the transaction isolation levels available from the driver or data source.<br/><br/>The following bitmasks are used together with the flag to determine which options are supported:<br/>SQL_TXN_READ_UNCOMMITTED<br/>SQL_TXN_READ_COMMITTED<br/>SQL_TXN_REPEATABLE_READ<br/>SQL_TXN_SERIALIZABLE<br/><br/>For descriptions of these isolation levels, see the description of SQL_DEFAULT_TXN_ISOLATION.<br/><br/>To set the transaction isolation level, an application calls **SQLSetConnectAttr** to set the SQL_ATTR_TXN_ISOLATION attribute. For more information, see [SQLSetConnectAttr Function](sqlsetconnectattr-function.md).<br/><br/>An SQL-92 Entry level-conformant driver will always return SQL_TXN_SERIALIZABLE as supported. A FIPS Transitional level-conformant driver will always return all of these options as supported.|
|SQL_UNION|2.0|An SQLUINTEGER bitmask enumerating the support for the **UNION** clause:<br/>SQL_U_UNION = The data source supports the **UNION** clause.<br/>SQL_U_UNION_ALL = The data source supports the **ALL** keyword in the **UNION** clause. (**SQLGetInfo** returns both SQL_U_UNION and SQL_U_UNION_ALL in this case.)<br/><br/>An SQL-92 Entry level-conformant driver will always return both of these options as supported.|
|SQL_USER_NAME|1.0|A character string with the name used in a particular database, which can be different from the login name.|
|SQL_XOPEN_CLI_YEAR|3.0|A character string that indicates the year of publication of the Open Group specification with which the version of the ODBC Driver Manager fully complies.|
  
## Example  

 **SQLGetInfo** returns lists of supported options as an SQLUINTEGER bitmask in **InfoValuePtr*. The bitmask for each option is used together with the flag to determine whether the option is supported.  
  
 For example, an application could use the following code to determine whether the SUBSTRING scalar function is supported by the driver associated with the connection.  
  
 For another example of using **SQLGetInfo**, see [SQLTables Function](sqltables-function.md).  
  
```cpp  
SQLUINTEGER fFuncs;  
  
SQLGetInfo(hdbc,  
           SQL_STRING_FUNCTIONS,  
           (SQLPOINTER)&fFuncs,  
           sizeof(fFuncs),  
           NULL);  
  
// SUBSTRING supported  
if (fFuncs & SQL_FN_STR_SUBSTRING)  
   ;   // do something  
  
// SUBSTRING not supported  
else  
   ;   // do something else  
```  
  
## Related Functions  

 Returning the setting of a connection attribute  
 [SQLGetConnectAttr Function](sqlgetconnectattr-function.md)  
  
 Determining whether a driver supports a function  
 [SQLGetFunctions Function](sqlgetfunctions-function.md)  
  
 Returning the setting of a statement attribute  
 [SQLGetStmtAttr Function](sqlgetstmtattr-function.md)  
  
 Returning information about a data source's data types  
 [SQLGetTypeInfo Function](sqlgettypeinfo-function.md)  
  
## See Also  

 [ODBC API Reference](odbc-api-reference.md)  
 [ODBC Header Files](../install/odbc-header-files.md)
