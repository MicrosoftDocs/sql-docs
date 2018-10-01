---
title: "SQLGetInfo Returned Values for dBASE | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], DBasedriver"
  - "desktop database drivers [ODBC], DBasedriver"
  - "SQLGetInfo function [ODBC], dBASE Driver"
  - "DBase driver [ODBC], SQLGetInfo"
  - "ODBC desktop database drivers [ODBC], DBasedriver"
ms.assetid: af64753c-c758-4b68-954b-2c84e3bbd93f
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetInfo Returned Values for dBASE
The following table lists the C-language #defines for the *fInfoType* argument and the corresponding values returned by **SQLGetInfo**. This information can be retrieved by passing the listed C-language #defines to **SQLGetInfo** in the *fInfoType* argument. For more information about the values returned by **SQLGetInfo**, see the *ODBC Programmer's Reference*.  
  
> [!NOTE]  
>  Where **SQLGetInfo** returns a 32-bit bitmask, a vertical bar (&#124;) represents a bitwise OR.  
  
|InfoType|Returned value|  
|--------------|--------------------|  
|SQL_ACCESSIBLE_PROCEDURES|"N"|  
|SQL_ACCESSIBLE_TABLES|"Y"|  
|SQL_ACTIVE_ENVIRONMENTS|0|  
|SQL_AGGREGATE_FUNCTIONS|All set|  
|SQL_ALTER_DOMAIN|0|  
|SQL_ALTER_TABLE|Multiple values|  
|SQL_ASYNC_MODE|0|  
|SQL_BATCH_ROW_COUNT|0|  
|SQL_BATCH_SUPPORT|0|  
|SQL_BOOKMARK_PERSISTENCE|Multiple values|  
|SQL_CATALOG_LOCATION|SQL_QL_START|  
|SQL_CATALOG_NAME|"Y"|  
|SQL_CATALOG_NAME_SEPARATOR|"\\"|  
|SQL_CATALOG_TERM|"Directory"|  
|SQL_CATALOG_USAGE|Multiple values|  
|SQL_COLLATION_SEQ|""|  
|SQL_COLUMN_ALIAS|"Y"|  
|SQL_CONCAT_NULL_BEHAVIOR|SQL_CB_NON_NULL|  
|SQL_CONVERT_BIGINT|0|  
|SQL_CONVERT_BINARY|Multiple values|  
|SQL_CONVERT_BIT|0|  
|SQL_CONVERT_CHAR|Multiple values|  
|SQL_CONVERT_DATE|Multiple values|  
|SQL_CONVERT_DECIMAL|0|  
|SQL_CONVERT_DOUBLE|Multiple values|  
|SQL_CONVERT_FLOAT|Multiple values|  
|SQL_CONVERT_FUNCTIONS|SQL_FN_CVT_CONVERT|  
|SQL_CONVERT_INTEGER|Multiple values|  
|SQL_CONVERT_LONGVARBINARY|Multiple values|  
|SQL_CONVERT_LONGVARCHAR|Multiple values|  
|SQL_CONVERT_NUMERIC|Multiple values|  
|SQL_CONVERT_REAL|Multiple values|  
|SQL_CONVERT_SMALLINT|Multiple values|  
|SQL_CONVERT_TIME|Multiple values|  
|SQL_CONVERT_TIMESTAMP|Multiple values|  
|SQL_CONVERT_TINYINT|Multiple values|  
|SQL_CONVERT_VARBINARY|Multiple values|  
|SQL_CONVERT_VARCHAR|Multiple values|  
|SQL_CORRELATION_NAME|SQL_CN_ANY|  
|SQL_CREATE_ASSERTION|0|  
|SQL_CREATE_CHARACTER_SET|0|  
|SQL_CREATE_COLLATION|0|  
|SQL_CREATE_DOMAIN|0|  
|SQL_CREATE_SCHEMA|0|  
|SQL_CREATE_TABLE|SQL_CT_CREATE_TABLE|  
|SQL_CREATE_TRANSLATION|0|  
|SQL_CREATE_VIEW|0|  
|SQL_CURSOR_COMMIT_BEHAVIOR|SQL_CB_CLOSE|  
|SQL_CURSOR_ROLLBACK_BEHAVIOR|SQL_CB_CLOSE|  
|SQL_CURSOR_SENSITIVITY|SQL_UNSPECIFIED|  
|SQL_DATA_SOURCE_NAME|The DSN from Odbc.ini, or "" if DRIVER keyword is used in Odbc.ini|  
|SQL_DATA_SOURCE_READ_ONLY|"N" (This depends on the data source.)|  
|SQL_DATABASE_NAME|Current database directory|  
|SQL_DATETIME_LITERALS|0|  
|SQL_DBMS_NAME|"DBASE"|  
|SQL_DBMS_VER|Multiple values|  
|SQL_DDL_INDEX|Multiple values|  
|SQL_DEFAULT_TXN_ISOLATION|0|  
|SQL_DESCRIBE_PARAMETER|0|  
|SQL_DRIVER_HDBC|Handled by the Driver Manager.|  
|SQL_DRIVER_HENV|Handled by the Driver Manager.|  
|SQL_DRIVER_HLIB|Handled by the Driver Manager.|  
|SQL_DRIVER_HSTMT|Handled by the Driver Manager.|  
|SQL_DRIVER_NAME|"OdbcJt32.dll"|  
|SQL_DRIVER_ODBC_VER|"3.51.0000"|  
|SQL_DRIVER_VER|"4.00.*nnnn*" (*nnnn* specifies the build date)|  
|SQL_DROP_ASSERTION|0|  
|SQL_DROP_CHARACTER_SET|0|  
|SQL_DROP_COLLATION|0|  
|SQL_DROP_DOMAIN|0|  
|SQL_DROP_SCHEMA|0|  
|SQL_DROP_TABLE|SQL_DT_DROP_TABLE|  
|SQL_DROP_TRANSLATION|0|  
|SQL_DROP_VIEW|SQL_DV_DROP_VIEW|  
|SQL_EXPRESSIONS_IN_ORDERBY|"Y"|  
|SQL_FILE_USAGE|SQL_FILE_TABLE|  
|SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1|SQL_CA1_NEXT|  
|SQL_GETDATA_EXTENSIONS|Multiple values|  
|SQL_GROUP_BY|SQL_GB_GROUP_BY_CONTAINS_SELECT|  
|SQL_IDENTIFIER_CASE|SQL_IC_UPPER (The qualifier is returned in mixed case so that Windows NT can locate the directory.)|  
|SQL_IDENTIFIER_QUOTE_CHAR|"`" (back quote)|  
|SQL_KEYWORDS|Multiple values|  
|SQL_LIKE_ESCAPE_CLAUSE|"N"|  
|SQL_MAX_BINARY_LITERAL_LEN|255|  
|SQL_MAX_CATALOG_NAME_LEN|66|  
|SQL_MAX_CHAR_LITERAL_LEN|254|  
|SQL_MAX_COLUMN_NAME_LEN|10|  
|SQL_MAX_COLUMNS_IN_GROUP_BY|10|  
|SQL_MAX_COLUMNS_IN_INDEX|0 (Limit Unknown or Not Applicable)|  
|SQL_MAX_COLUMNS_IN_ORDER_BY|10|  
|SQL_MAX_COLUMNS_IN_SELECT|255|  
|SQL_MAX_COLUMNS_IN_TABLE|255|  
|SQL_MAX_CONCURRENT_ACTIVITIES|0|  
|SQL_MAX_CURSOR_NAME_LEN|64|  
|SQL_MAX_DRIVER_CONNECTIONS|64|  
|SQL_MAX_INDEX_SIZE|220|  
|SQL_MAX_PROCEDURE_NAME_LEN|0|  
|SQL_MAX_ROW_SIZE|4000|  
|SQL_MAX_ROW_SIZE_INCLUDES_LONG|"N"|  
|SQL_MAX_SCHEMA_NAME_LEN|0|  
|SQL_MAX_STATEMENT_LEN|65000|  
|SQL_MAX_TABLE_NAME_LEN|12|  
|SQL_MAX_TABLES_IN_SELECT|16|  
|SQL_MAX_USER_NAME_LEN|0|  
|SQL_MULT_RESULT_SETS|"N"|  
|SQL_MULTIPLE_ACTIVE_TXN|"Y"|  
|SQL_NEED_LONG_DATA_LEN|"N"|  
|SQL_NON_NULLABLE_COLUMNS|SQL_NNC_NON_NULL|  
|SQL_NULL_COLLATION|SQL_NC_LOW|  
|SQL_NUMERIC_FUNCTIONS|Multiple values|  
|SQL_ODBC_SAG_CLI_ CONFORMANCE|SQL_OSCC_COMPLIANT|  
|SQL_ODBC_SQL_INTEGRITY|"N"|  
|SQL_ODBC_VER|From Driver Manager|  
|SQL_OJ_CAPABILITIES|Multiple values|  
|SQL_ORDER_BY_COLUMNS_IN_SELECT|"N"|  
|SQL_OUTER_JOINS|"Y"|  
|SQL_PROCEDURE_TERM|""|  
|SQL_PROCEDURES|"N"|  
|SQL_QUOTED_IDENTIFIER_CASE|SQL_IC_MIXED|  
|SQL_ROW_UPDATES|"N"|  
|SQL_SCHEMA_TERM|""|  
|SQL_SCHEMA_USAGE|0|  
|SQL_SCROLL_OPTIONS|Multiple values|  
|SQL_SEARCH_PATTERN_ESCAPE|"\\"|  
|SQL_SERVER_NAME|"DBASE"|  
|SQL_SPECIAL_CHARACTERS|"~`@#$%^&*_-+=\\}{"';:?/><,.!'[]&#124;"|  
|SQL_STRING_FUNCTIONS|Multiple values|  
|SQL_SUBQUERIES|Multiple values|  
|SQL_SYSTEM_FUNCTIONS|0|  
|SQL_TABLE_TERM|"TABLE"|  
|SQL_TIMEDATE_ADD_INTERVALS|0|  
|SQL_TIMEDATE_DIFF_INTERVALS|0|  
|SQL_TIMEDATE_FUNCTIONS|Multiple values|  
|SQL_TXN_CAPABLE|SQL_TC_NONE|  
|SQL_TXN_ISOLATION_OPTION|0|  
|SQL_UNION|Multiple values|  
|SQL_USER_NAME|""|
