---
title: "SQLGetInfo (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetInfo function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: fbc39e3d-67d9-4331-bf5f-76dbd74c4c45
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetInfo (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Returns general information about the Visual FoxPro ODBC Driver and data source associated with a connection handle, *hdbc*. The following list shows the value returned by the Visual FoxPro ODBC Driver for each *fInfoType* argument and comments regarding the returned values.  
  
 For more information, see [SQLGetInfo](../../odbc/reference/syntax/sqlgetinfo-function.md) in the *ODBC Programmer's Reference*.  
  
## A  
 SQL_ACCESSIBLE_PROCEDURES returns 'N'.  
  
 SQL_ACCESSIBLE_TABLES returns 'Y'.  
  
 SQL_ACTIVE_CONNECTIONS returns 0.  
  
 SQL_ACTIVE_STATEMENTS returns 0.  
  
 SQL_ALTER_TABLE returns either SQL_AT_ADD_COLUMN or SQL_AT_DROP_COLUMN.  
  
## B  
 SQL_BOOKMARK_PERSISTENCE returns SQL_BP_SCROLL.  
  
## C  
 SQL_COLUMN_ALIAS returns 'Y'.  
  
 SQL_CONCAT_NULL_BEHAVIOR returns SQL_CB_NULL.  
  
 SQL_CONVERT_BIGINT returns 0. The Visual FoxPro ODBC Driver does not support *BigInt*.  
  
 SQL_CONVERT_BINARY returns 0.  
  
 SQL_CONVERT_BIT returns 0.  
  
 SQL_CONVERT_CHAR returns 0.  
  
 SQL_CONVERT_DATE returns 0.  
  
 SQL_CONVERT_DECIMAL returns 0.  
  
 SQL_CONVERT_DOUBLE returns 0.  
  
 SQL_CONVERT_FLOAT returns 0.  
  
 SQL_CONVERT_INTEGER returns 0.  
  
 SQL_CONVERT_LONGVARBINARY returns 0.  
  
 SQL_CONVERT_LONGVARCHAR returns 0.  
  
 SQL_CONVERT_NUMERIC returns 0.  
  
 SQL_CONVERT_REAL returns 0.  
  
 SQL_CONVERT_SMALLINT returns 0.  
  
 SQL_CONVERT_TIME returns 0.  
  
 SQL_CONVERT_TIMESTAMP returns 0.  
  
 SQL_CONVERT_TINYINT returns 0.  
  
 SQL_CONVERT_VARBINARY returns 0.  
  
 SQL_CONVERT_VARCHAR returns 0.  
  
 SQL_CONVERT_FUNCTIONS returns 0.  
  
 SQL_CORRELATION_NAME returns SQL_CN_ANY.  
  
 SQL_CURSOR_COMMIT_BEHAVIOR returns SQL_CB_PRESERVE.  
  
 SQL_CURSOR_ROLLBACK_BEHAVIOR returns SQL_CB_PRESERVE.  
  
## D  
 SQL_DATA_SOURCE_NAME returns the value passed as DSN to [SQLConnect](../../odbc/microsoft/sqlconnect-visual-foxpro-odbc-driver.md), or [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md); returns an empty string if no DSN is specified.  
  
 SQL_DATA_SOURCE_READ_ONLY returns 'N'.  
  
 SQL_DATABASE_NAME returns a full UNC path to the current database if the data source is a [database](../../odbc/microsoft/visual-foxpro-terminology.md). If the data source connects to a directory of [tables](../../odbc/microsoft/visual-foxpro-terminology.md), the function returns the path to the directory.  
  
 SQL_DBMS_NAME returns "Visual FoxPro".  
  
 SQL_DBMS_VER returns "03.00.0000".  
  
 SQL_DEFAULT_TXN_ISOLATION returns SQL_TXN_READ_COMMITTED. Dirty reads are not possible, but nonrepeatable reads and phantoms are possible.  
  
 SQL_DRIVER_HDBC is implemented by the Driver Manager.  
  
 SQL_DRIVER_HENV is implemented by the Driver Manager.  
  
 SQL_DRIVER_HLIB is implemented by the Driver Manager.  
  
 SQL_DRIVER_HSTMT is implemented by the Driver Manager.  
  
 SQL_DRIVER_NAME returns "vfpodbc.dll".  
  
 SQL_DRIVER_ODBC_VER returns "02.50" (SQL_SPEC_MAJOR, SQL_SPEC_MINOR).  
  
 SQL_DRIVER_VER returns "01.00.0000".  
  
## E  
 SQL_EXPRESSIONS_IN_ORDERBY returns 'N'.  
  
## F  
 SQL_FETCH_DIRECTION returns:  
  
-   SQL_FD_FETCH_NEXT  
  
-   SQL_FD_FETCH_FIRST  
  
-   SQL_FD_FETCH_LAST  
  
-   SQL_FD_FETCH_PRIOR  
  
-   SQL_FD_FETCH_ABSOLUTE  
  
-   SQL_FD_FETCH_RELATIVE  
  
-   SQL_FD_FETCH_BOOKMARK.  
  
 SQL_FILE_USAGE returns SQL_FILE_QUALIFIER both for database (.dbc file) and for free table (.dbf file) data sources.  
  
## G-H  
 SQL_GETDATA_EXENSIONS returns:  
  
-   SQL_GD_ANY_COLUMN  
  
-   SQL_GD_ANY_BLOCK  
  
-   SQL_GD_ANY_BOUND  
  
-   SQL_GD_ANY_ORDER  
  
 SQL_GROUP_BY returns SQL_GB_NO_RELATION.  
  
## I-J  
 SQL_IDENTIFIER_CASE returns SQL_IC_MIXED.  
  
 SQL_IDENTIFIER_QUOTE_CHAR returns `.  
  
## K  
 SQL_KEYWORDS returns "".  
  
## L  
 SQL_LIKE_ESCAPE_CLAUSE returns 'N'.  
  
 SQL_LOCK_TYPES returns SQL_LCK_NO_CHANGE.  
  
## M  
 SQL_MAX_BINARY_LITERAL_LEN returns 0.  
  
 SQL_MAX_CHAR_LITERAL_LEN returns 254.  
  
 SQL_MAX_COLUMN_NAME_LEN returns 128.  
  
 SQL_MAX_COLUMNS_IN_GROUP_BY returns 16.  
  
 SQL_MAX_COLUMNS_IN_ORDER_BY returns 16.  
  
 SQL_MAX_COLUMNS_IN_INDEX returns 0.  
  
 SQL_MAX_COLUMNS_IN_SELECT returns 254.  
  
 SQL_MAX_COLUMNS_IN_TABLE returns 254.  
  
 SQL_MAX_CURSOR_NAME_LEN returns 254.  
  
 SQL_MAX_INDEX_SIZE returns 0.  
  
 SQL_MAX_OWNER_NAME_LEN returns 0.  
  
 SQL_MAX_PROCEDURE_NAME_LEN returns 0. The Visual FoxPro ODBC Driver does not allow direct access to Visual FoxPro stored procedures.  
  
 SQL_MAX_QUALIFIER_NAME_LEN returns the maximum operating system path length.  
  
 SQL_MAX_ROW_SIZE returns 254^2.  
  
 SQL_MAX_ROW_SIZE_INCLUDES_LONG returns 'N'.  
  
 SQL_MAX_STATEMENT_LEN returns 8192.  
  
 SQL_MAX_TABLE_NAME_LEN returns 128.  
  
 SQL_MAX_TABLES_IN_SELECT returns 16.  
  
 SQL_MAX_USER_NAME_LEN returns 0.  
  
 SQL_MULT_RESULT_SETS returns 'Y'.  
  
 SQL_MULTIPLE_ACTIVE_TXN returns 'Y'. Multiple connections can have several transactions open at once.  
  
## N  
 SQL_NEED_LONG_DATA_LEN returns 'N'.  
  
 SQL_NON_NULLABLE_COLUMNS returns SQL_NNC_NON_NULL.  
  
 SQL_NULL_COLLATION returns SQL_NC_LOW.  
  
 SQL_NUMERIC_FUNCTIONS returns all functions except SQL_FN_NUM_POWER, which is not supported by the Visual FoxPro ODBC Driver. The following functions are supported:  
  
-   SQL_FN_NUM_ABS  
  
-   SQL_FN_NUM_ACOS  
  
-   SQL_FN_NUM_ASIN  
  
-   SQL_FN_NUM_ATAN  
  
-   SQL_FN_NUM_ATAN2  
  
-   SQL_FN_NUM_CELING  
  
-   SQL_FN_NUM_COS  
  
-   SQL_FN_NUM_COT  
  
-   SQL_FN_NUM_DEGREES  
  
-   SQL_FN_NUM_EXP  
  
-   SQL_FN_NUM_FLOOR  
  
-   SQL_FN_NUM_LOG  
  
-   SQL_FN_NUM_LOG10  
  
-   SQL_FN_NUM_MOD  
  
-   SQL_FN_NUM_PI  
  
-   SQL_FN_NUM_RADIANS  
  
-   SQL_FN_NUM_RAND  
  
-   SQL_FN_NUM_ROUND  
  
-   SQL_FN_NUM_SIGN  
  
-   SQL_FN_NUM_SIN  
  
-   SQL_FN_NUM_SQRT  
  
-   SQL_FN_NUM_TAN  
  
## O  
 SQL_ODBC_API_CONFORMANCE returns SQL_OAC_LEVEL1.  
  
 SQL_ODBC_SAG_CLI_CONFORMANCE returns SQL_OSCC_COMPLIANT.  
  
 SQL_ODBC_SQL_CONFORMANCE returns SQL_OSC_MINIMUM. Minimum SQL syntax is supported.  
  
 SQL_ODBC_SQL_OPT_IEF returns "N".  
  
 SQL_ODBC_VER is implemented by the Driver Manager.  
  
 SQL_ORDER_BY_COLUMNS_IN_SELECT returns "N".  
  
 SQL_OUTER_JOINS returns "N".  
  
 SQL_OWNER_TERM returns "". The Visual FoxPro ODBC Driver does not support owners for its objects.  
  
 SQL_OWNER_USAGE returns 0. The Visual FoxPro ODBC Driver does not support owners for its objects.  
  
## P  
 SQL_POS_OPERATIONS returns SQL_POS_POSITION.  
  
 SQL_POSITIONED_STATEMENTS returns 0.  
  
 SQL_PROCEDURE_TERM returns "".  
  
 SQL_PROCEDURES returns 'N'.  
  
## Q  
 SQL_QUALIFIER_LOCATION returns SQL_QL_START.  
  
 SQL_QUALIFIER_NAME_SEPARATOR returns '!' or '\\'. The separator between database and table is '!' for data sources connected to [databases](../../odbc/microsoft/visual-foxpro-terminology.md), and '\\' for data sources that are directories of [free tables](../../odbc/microsoft/visual-foxpro-terminology.md).  
  
 SQL_QUALIFIER_TERM returns "database" or "directory". The qualifier is "database" for data sources connected to [databases](../../odbc/microsoft/visual-foxpro-terminology.md), and "directory" for data sources that are directories of [free tables](../../odbc/microsoft/visual-foxpro-terminology.md).  
  
 SQL_QUALIFIER_USAGE does not support SQL_QU_PRIVILEGE_DEFINITION; it returns either SQL_QU_DML_STATEMENT or SQL_QU_TABLE_DEFINITION.  
  
 SQL_QUOTED_IDENTIFIER_CASE returns SQL_IC_MIXED.  
  
## R  
 SQL_ROW_UPDATES returns "N". The Visual FoxPro ODBC Driver supports only static and forward cursors.  
  
## S  
 SQL_SCROLL_CONCURRENCY returns SQL_SCCO_READ_ONLY.  
  
 SQL_SCROLL_OPTIONS returns either SQL_SO_STATIC or SQL_SO_READONLY.  
  
 SQL_SEARCH_PATTERN_ESCAPE returns "\\".  
  
 SQL_SERVER_NAME returns "".  
  
 SQL_SPECIAL_CHARACTERS returns "~@#$%^".  
  
 SQL_STATIC_SENSITIVITY returns 0. The Visual FoxPro ODBC Driver does not support positional updates.  
  
 SQL_STRING_FUNCTIONS does not support SQL_FN_STR_INSERT, SQL_FN_STR_LOCATE, SQL_FN_STR_LOCATE_2, or SQL_FN_STR_SOUNDEX.  
  
 It returns:  
  
-   SQL_FN_STR_ASCII  
  
-   SQL_FN_STR_CHAR  
  
-   SQL_FN_STR_CONCAT  
  
-   SQL_FN_STR_DIFFERENCE  
  
-   SQL_FN_STR_LCASE  
  
-   SQL_FN_STR_LEFT  
  
-   SQL_FN_STR_LENGTH  
  
-   SQL_FN_STR_LTRIM  
  
-   SQL_FN_STR_REPEAT  
  
-   SQL_FN_STR_REPLACE  
  
-   SQL_FN_STR_RIGHT  
  
-   SQL_FN_STR_RTRIM  
  
-   SQL_FN_STR_SUBSTRING  
  
-   SQL_FN_STR_UCASE  
  
-   SQL_FN_STR_SPACE.  
  
 SQL_SUBQUERIES returns:  
  
-   SQL_SQ_CORRELATED_SUBQUERIES  
  
-   SQL_SQ_COMPARISON  
  
-   SQL_SQ_EXISTS  
  
-   SQL_SQ_IN  
  
-   SQL_SQ_QUANTIFIED.  
  
 SQL_SYSTEM_FUNCTIONS returns:  
  
-   SQL_FN_SYS_DBNAME  
  
-   SQL_FN_SYS_IFNULL  
  
 but not:  
  
-   SQL_FN_SYS_USERNAME  
  
## T  
 SQL_TABLE_TERM returns "table".  
  
 SQL_TIMEDATE_ADD_INTERVALS returns:  
  
-   SQL_FN_TSI_ SECOND  
  
-   SQL_FN_TSI_MINUTE  
  
-   SQL_FN_TSI_HOUR  
  
-   SQL_FN_TSI_DAY  
  
-   SQL_FN_TSI_MONTH  
  
-   SQL_FN_TSI_YEAR  
  
 but not:  
  
-   SQL_FN_TSI_FRAC_SECOND  
  
-   SQL_FN_TSI_WEEK  
  
-   SQL_FN_TSI_QUARTER  
  
 SQL_TIMEDATE_DIFF_INTERVALS returns:  
  
-   SQL_FN_TSI_ SECOND  
  
-   SQL_FN_TSI_MINUTE  
  
-   SQL_FN_TSI_HOUR  
  
-   SQL_FN_TSI_DAY  
  
-   SQL_FN_TSI_MONTH  
  
-   SQL_FN_TSI_YEAR  
  
 SQL_TIMEDATE_FUNCTIONS does not support SQL_FN_TD_QUARTER, SQL_FN_TD_TIMESTAMPADD, SQL_FN_TD_DAYOFYEAR, or SQL_FN_TD_WEEK.  
  
 It returns:  
  
-   SQL_FN_TD_CURDATE  
  
-   SQL_FN_TD_CURTIME  
  
-   SQL_FN_TD_DAYNAME  
  
-   SQL_FN_TD_DAYOFMONTH  
  
-   SQL_FN_TD_DAYOFWEEK  
  
-   SQL_FN_TD_HOUR  
  
-   SQL_FN_TD_MINUTE  
  
-   SQL_FN_TD_MONTH  
  
-   SQL_FN_TD_MONTHNAME  
  
-   SQL_FN_TD_NOW  
  
-   SQL_FN_TD_SECOND  
  
-   SQL_FN_TD_TIMESTAMPDIFF  
  
-   SQL_FN_TD_YEAR .  
  
 SQL_TXN_CAPABLE returns SQL_TC_DML.  
  
 SQL_TXN_ISOLATION_OPTION returns SQL_TXN_READ_COMMITTED.  
  
## U-Z  
 SQL_UNION returns either SQL_U_UNION or SQL_U_UNION_ALL.  
  
 SQL_USER_NAME returns \<blank>.
