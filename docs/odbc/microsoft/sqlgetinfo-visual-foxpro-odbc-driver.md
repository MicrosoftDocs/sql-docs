---
title: "SQLGetInfo (Visual FoxPro ODBC Driver)"
description: "SQLGetInfo (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetInfo function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLGetInfo (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 1.

## Remarks

Returns general information about the Visual FoxPro ODBC Driver and data source associated with a connection handle, `hdbc`. The following table shows the value returned by the Visual FoxPro ODBC Driver for each `fInfoType` argument and comments regarding the returned values.

For more information, see [SQLGetInfo Function](../reference/syntax/sqlgetinfo-function.md) in the *ODBC Programmer's Reference*.

| Argument | Return value | Comments |
| --- | --- | --- |
| **A** | | |
| `SQL_ACCESSIBLE_PROCEDURES` | `N` | |
| `SQL_ACCESSIBLE_TABLES` | `Y` | |
| `SQL_ACTIVE_CONNECTIONS` | `0` | |
| `SQL_ACTIVE_STATEMENTS` | `0` | |
| `SQL_ALTER_TABLE` | `SQL_AT_ADD_COLUMN` or<br />`SQL_AT_DROP_COLUMN` | |
| **B** | | |
| `SQL_BOOKMARK_PERSISTENCE` | `SQL_BP_SCROLL` | |
| **C** | | |
| `SQL_COLUMN_ALIAS` | `Y` | |
| `SQL_CONCAT_NULL_BEHAVIOR` | `SQL_CB_NULL` | |
| `SQL_CONVERT_BIGINT` | `0` | The Visual FoxPro ODBC Driver doesn't support `BigInt`. |
| `SQL_CONVERT_BINARY` | `0` | |
| `SQL_CONVERT_BIT` | `0` | |
| `SQL_CONVERT_CHAR` | `0` | |
| `SQL_CONVERT_DATE` | `0` | |
| `SQL_CONVERT_DECIMAL` | `0` | |
| `SQL_CONVERT_DOUBLE` | `0` | |
| `SQL_CONVERT_FLOAT` | `0` | |
| `SQL_CONVERT_INTEGER` | `0` | |
| `SQL_CONVERT_LONGVARBINARY` | `0` | |
| `SQL_CONVERT_LONGVARCHAR` | `0` | |
| `SQL_CONVERT_NUMERIC` | `0` | |
| `SQL_CONVERT_REAL` | `0` | |
| `SQL_CONVERT_SMALLINT` | `0` | |
| `SQL_CONVERT_TIME` | `0` | |
| `SQL_CONVERT_TIMESTAMP` | `0` | |
| `SQL_CONVERT_TINYINT` | `0` | |
| `SQL_CONVERT_VARBINARY` | `0` | |
| `SQL_CONVERT_VARCHAR` | `0` | |
| `SQL_CONVERT_FUNCTIONS` | `0` | |
| `SQL_CORRELATION_NAME` | `SQL_CN_ANY` | |
| `SQL_CURSOR_COMMIT_BEHAVIOR` | `SQL_CB_PRESERVE` | |
| `SQL_CURSOR_ROLLBACK_BEHAVIOR` | `SQL_CB_PRESERVE` | |
| **D** | | |
| `SQL_DATA_SOURCE_NAME` | The value passed as DSN to [SQLConnect](sqlconnect-visual-foxpro-odbc-driver.md), or [SQLDriverConnect](sqldriverconnect-visual-foxpro-odbc-driver.md); or an empty string if no DSN is specified. | |
| `SQL_DATA_SOURCE_READ_ONLY` | `N` | |
| `SQL_DATABASE_NAME` | A full UNC path to the current database if the data source is a [database](visual-foxpro-terminology.md). If the data source connects to a directory of [tables](visual-foxpro-terminology.md), the function. | The path to the directory. |
| `SQL_DBMS_NAME` | `Visual FoxPro` | |
| `SQL_DBMS_VER` | `03.00.0000` | |
| `SQL_DEFAULT_TXN_ISOLATION` | `SQL_TXN_READ_COMMITTED` | Dirty reads aren't possible, but nonrepeatable reads and phantoms are possible. |
| `SQL_DRIVER_HDBC` | Implemented by the Driver Manager. | |
| `SQL_DRIVER_HENV` | Implemented by the Driver Manager. | |
| `SQL_DRIVER_HLIB` | Implemented by the Driver Manager. | |
| `SQL_DRIVER_HSTMT` | Implemented by the Driver Manager. | |
| `SQL_DRIVER_NAME` | `vfpodbc.dll` | |
| `SQL_DRIVER_ODBC_VER` | `02.50` (`SQL_SPEC_MAJOR`, `SQL_SPEC_MINOR`) | |
| `SQL_DRIVER_VER` | `01.00.0000` | |
| **E** | | |
| `SQL_EXPRESSIONS_IN_ORDERBY` | `N` | |
| **F** | | |
| `SQL_FETCH_DIRECTION` | `SQL_FD_FETCH_NEXT`<br />`SQL_FD_FETCH_FIRST`<br />`SQL_FD_FETCH_LAST`<br />`SQL_FD_FETCH_PRIOR`<br />`SQL_FD_FETCH_ABSOLUTE`<br />`SQL_FD_FETCH_RELATIVE`<br />`SQL_FD_FETCH_BOOKMARK` | |
| `SQL_FILE_USAGE` | `SQL_FILE_QUALIFIER` both for database (`.dbc` file) and for free table (`.dbf` file) data sources. | |
| **G-H** | | |
| `SQL_GETDATA_EXENSIONS` | `SQL_GD_ANY_COLUMN`<br />`SQL_GD_ANY_BLOCK`<br />`SQL_GD_ANY_BOUND`<br />`SQL_GD_ANY_ORDER` | |
| `SQL_GROUP_BY` | `SQL_GB_NO_RELATION` | |
| **I-J** | | |
| `SQL_IDENTIFIER_CASE` | `SQL_IC_MIXED` | |
| `SQL_IDENTIFIER_QUOTE_CHAR` | `` ` `` | |
| **K** | | |
| `SQL_KEYWORDS` | `""` | |
| **L** | | |
| `SQL_LIKE_ESCAPE_CLAUSE` | `N` | |
| `SQL_LOCK_TYPES` | `SQL_LCK_NO_CHANGE` | |
| **M** | | |
| `SQL_MAX_BINARY_LITERAL_LEN` | `0` | |
| `SQL_MAX_CHAR_LITERAL_LEN` | `254` | |
| `SQL_MAX_COLUMN_NAME_LEN` | `128` | |
| `SQL_MAX_COLUMNS_IN_GROUP_BY` | `16` | |
| `SQL_MAX_COLUMNS_IN_ORDER_BY` | `16` | |
| `SQL_MAX_COLUMNS_IN_INDEX` | `0` | |
| `SQL_MAX_COLUMNS_IN_SELECT` | `254` | |
| `SQL_MAX_COLUMNS_IN_TABLE` | `254` | |
| `SQL_MAX_CURSOR_NAME_LEN` | `254` | |
| `SQL_MAX_INDEX_SIZE` | `0` | |
| `SQL_MAX_OWNER_NAME_LEN` | `0` | |
| `SQL_MAX_PROCEDURE_NAME_LEN` | `0` | The Visual FoxPro ODBC Driver doesn't allow direct access to Visual FoxPro stored procedures. |
| `SQL_MAX_QUALIFIER_NAME_LEN` | The maximum operating system path length. | |
| `SQL_MAX_ROW_SIZE` | `254^2` | |
| `SQL_MAX_ROW_SIZE_INCLUDES_LONG` | `N` | |
| `SQL_MAX_STATEMENT_LEN` | `8192` | |
| `SQL_MAX_TABLE_NAME_LEN` | `128` | |
| `SQL_MAX_TABLES_IN_SELECT` | `16` | |
| `SQL_MAX_USER_NAME_LEN` | `0` | |
| `SQL_MULT_RESULT_SETS` | `Y` | |
| `SQL_MULTIPLE_ACTIVE_TXN` | `Y` | Multiple connections can have several transactions open at once. |
| **N** | | |
| `SQL_NEED_LONG_DATA_LEN` | `N` | |
| `SQL_NON_NULLABLE_COLUMNS` | `SQL_NNC_NON_NULL` | |
| `SQL_NULL_COLLATION` | `SQL_NC_LOW` | |
| `SQL_NUMERIC_FUNCTIONS` | `SQL_FN_NUM_ABS`<br />`SQL_FN_NUM_ACOS`<br />`SQL_FN_NUM_ASIN`<br />`SQL_FN_NUM_ATAN`<br />`SQL_FN_NUM_ATAN2`<br />`SQL_FN_NUM_CEILING`<br />`SQL_FN_NUM_COS`<br />`SQL_FN_NUM_COT`<br />`SQL_FN_NUM_DEGREES`<br />`SQL_FN_NUM_EXP`<br />`SQL_FN_NUM_FLOOR`<br />`SQL_FN_NUM_LOG`<br />`SQL_FN_NUM_LOG10`<br />`SQL_FN_NUM_MOD`<br />`SQL_FN_NUM_PI`<br />`SQL_FN_NUM_RADIANS`<br />`SQL_FN_NUM_RAND`<br />`SQL_FN_NUM_ROUND`<br />`SQL_FN_NUM_SIGN`<br />`SQL_FN_NUM_SIN`<br />`SQL_FN_NUM_SQRT`<br />`SQL_FN_NUM_TAN` | All functions except `SQL_FN_NUM_POWER`, which isn't supported by the Visual FoxPro ODBC Driver. |
| **O** | | |
| `SQL_ODBC_API_CONFORMANCE` | `SQL_OAC_LEVEL1` | |
| `SQL_ODBC_SAG_CLI_CONFORMANCE` | `SQL_OSCC_COMPLIANT` | |
| `SQL_ODBC_SQL_CONFORMANCE` | `SQL_OSC_MINIMUM` | Minimum SQL syntax is supported. |
| `SQL_ODBC_SQL_OPT_IEF` | `N` | |
| `SQL_ODBC_VER` | Implemented by the Driver Manager. | |
| `SQL_ORDER_BY_COLUMNS_IN_SELECT` | `N` | |
| `SQL_OUTER_JOINS` | `N` | |
| `SQL_OWNER_TERM` | `""` | The Visual FoxPro ODBC Driver doesn't support owners for its objects. |
| `SQL_OWNER_USAGE` | `0` | The Visual FoxPro ODBC Driver doesn't support owners for its objects. |
| **P** | | |
| `SQL_POS_OPERATIONS` | `SQL_POS_POSITION` | |
| `SQL_POSITIONED_STATEMENTS` | `0` | |
| `SQL_PROCEDURE_TERM` | `""` | |
| `SQL_PROCEDURES` | `N` | |
| **Q** | | |
| `SQL_QUALIFIER_LOCATION` | `SQL_QL_START` | |
| `SQL_QUALIFIER_NAME_SEPARATOR` | `!` or `\\` | The separator between database and table is `!` for data sources connected to [databases](visual-foxpro-terminology.md), and `\\` for data sources that are directories of [free tables](visual-foxpro-terminology.md). |
| `SQL_QUALIFIER_TERM` | `database` or `directory` | The qualifier is `database` for data sources connected to [databases](visual-foxpro-terminology.md), and `directory` for data sources that are directories of [free tables](visual-foxpro-terminology.md). |
| `SQL_QUALIFIER_USAGE` | `SQL_QU_DML_STATEMENT` or<br />`SQL_QU_TABLE_DEFINITION` | Doesn't support:<br /><br />`SQL_QU_PRIVILEGE_DEFINITION` |
| `SQL_QUOTED_IDENTIFIER_CASE` | `SQL_IC_MIXED` | |
| **R** | | |
| `SQL_ROW_UPDATES` | `N` | The Visual FoxPro ODBC Driver supports only static and forward cursors. |
| **S** | | |
| `SQL_SCROLL_CONCURRENCY` | `SQL_SCCO_READ_ONLY` | |
| `SQL_SCROLL_OPTIONS` | `SQL_SO_STATIC` or<br />`SQL_SO_READONLY` | |
| `SQL_SEARCH_PATTERN_ESCAPE` | `\\` | |
| `SQL_SERVER_NAME` | `""` | |
| `SQL_SPECIAL_CHARACTERS` | `~@#$%^` | |
| `SQL_STATIC_SENSITIVITY` | `0` | The Visual FoxPro ODBC Driver doesn't support positional updates. |
| `SQL_STRING_FUNCTIONS` | `SQL_FN_STR_ASCII`<br />`SQL_FN_STR_CHAR`<br />`SQL_FN_STR_CONCAT`<br />`SQL_FN_STR_DIFFERENCE`<br />`SQL_FN_STR_LCASE`<br />`SQL_FN_STR_LEFT`<br />`SQL_FN_STR_LENGTH`<br />`SQL_FN_STR_LTRIM`<br />`SQL_FN_STR_REPEAT`<br />`SQL_FN_STR_REPLACE`<br />`SQL_FN_STR_RIGHT`<br />`SQL_FN_STR_RTRIM`<br />`SQL_FN_STR_SUBSTRING`<br />`SQL_FN_STR_UCASE`<br />`SQL_FN_STR_SPACE` | Doesn't support:<br /><br />`SQL_FN_STR_INSERT`<br />`SQL_FN_STR_LOCATE`<br />`SQL_FN_STR_LOCATE_2`<br />`SQL_FN_STR_SOUNDEX` |
| `SQL_SUBQUERIES` | `SQL_SQ_CORRELATED_SUBQUERIES`<br />`SQL_SQ_COMPARISON`<br />`SQL_SQ_EXISTS`<br />`SQL_SQ_IN`<br />`SQL_SQ_QUANTIFIED` | |
| `SQL_SYSTEM_FUNCTIONS` | `SQL_FN_SYS_DBNAME`<br />`SQL_FN_SYS_IFNULL` | Doesn't support:<br /><br />`SQL_FN_SYS_USERNAME` |
| **T** | | |
| `SQL_TABLE_TERM` | `table` | |
| `SQL_TIMEDATE_ADD_INTERVALS` | `SQL_FN_TSI_ SECOND`<br />`SQL_FN_TSI_MINUTE`<br />`SQL_FN_TSI_HOUR`<br />`SQL_FN_TSI_DAY`<br />`SQL_FN_TSI_MONTH`<br />`SQL_FN_TSI_YEAR` | Doesn't support:<br /><br />`SQL_FN_TSI_FRAC_SECOND`<br />`SQL_FN_TSI_WEEK`<br />`SQL_FN_TSI_QUARTER` |
| `SQL_TIMEDATE_DIFF_INTERVALS` | `SQL_FN_TSI_SECOND`<br />`SQL_FN_TSI_MINUTE`<br />`SQL_FN_TSI_HOUR`<br />`SQL_FN_TSI_DAY`<br />`SQL_FN_TSI_MONTH`<br />`SQL_FN_TSI_YEAR` | |
| `SQL_TIMEDATE_FUNCTIONS` | `SQL_FN_TD_CURDATE`<br />`SQL_FN_TD_CURTIME`<br />`SQL_FN_TD_DAYNAME`<br />`SQL_FN_TD_DAYOFMONTH`<br />`SQL_FN_TD_DAYOFWEEK`<br />`SQL_FN_TD_HOUR`<br />`SQL_FN_TD_MINUTE`<br />`SQL_FN_TD_MONTH`<br />`SQL_FN_TD_MONTHNAME`<br />`SQL_FN_TD_NOW`<br />`SQL_FN_TD_SECOND`<br />`SQL_FN_TD_TIMESTAMPDIFF`<br />`SQL_FN_TD_YEAR` | Doesn't support:<br /><br />`SQL_FN_TD_QUARTER`<br />`SQL_FN_TD_TIMESTAMPADD`<br />`SQL_FN_TD_DAYOFYEAR`<br />`SQL_FN_TD_WEEK` |
| `SQL_TXN_CAPABLE` | `SQL_TC_DML` | |
| `SQL_TXN_ISOLATION_OPTION` | `SQL_TXN_READ_COMMITTED` | |
| **U-Z** | | |
| `SQL_UNION` | `SQL_U_UNION` or<br />`SQL_U_UNION_ALL` | |
| `SQL_USER_NAME` | `<blank>` | |
