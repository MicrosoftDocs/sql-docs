---
title: "Functions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b26e4192-3835-4ef1-a2ea-68e58af4b4cb
caps.latest.revision: 49
author: BarbKess
---
# Functions (SQL Server PDW)
SQL Server PDW provides many built-in functions that you can use to perform operations such as calculations and string manipulations. System functions can be used in most places where expressions are allowed, including a **SELECT** list or a **WHERE** clause. In situations where a system function is not supported, the statement will return an error. SQL includes the following functions:  
  
## Contents  
  
-   [Window Functions](#Window)  
  
    -   [Aggregate Functions](#AggregateFunctions)  
  
    -   [Analytic Functions](#AnalyticFunctions)  
  
    -   [Ranking Functions](#RankingFunctions)  
  
-   [Scalar Functions](#Scalar)  
  
    -   [Casting Functions](#CastingFunctions)  
  
    -   [Cryptographic Functions](#CryptoFunctions)  
  
    -   [Date and Time Functions](#DateAndTimeFunctions)  
  
    -   [Error Handling Functions](#ErrorFunctions)  
  
    -   [Mathematical Functions](#MathematicalFunctions)  
  
    -   [Metadata Functions](#MetadataFunctions)  
  
    -   [Null and Comparison Functions](#NullComparisonFunctions)  
  
    -   [ODBC Scalar Functions](#ODBCFunctions)  
  
    -   [String Functions](#StringFunctions)  
  
## <a name="Window"></a>Window Functions  
Window functions can be applied to row groups (windows). An OVER clause can be applied to aggregate, analytic, and ranking functions  
  
### <a name="AggregateFunctions"></a>Aggregate Functions  
Aggregate functions perform a calculation on a set of values and return a scalar value. Except for COUNT, aggregate functions ignore null values. Aggregate functions are frequently used with the GROUP BY clause of the SELECT statement.  
  
Aggregate functions can be used as expressions only in the select list and HAVING clauses of the SELECT statement.  
  
The following table lists the aggregate functions in SQL Server PDW.  
  
|||  
|-|-|  
|[AVG](../../mpp/sqlpdw/avg-sql-server-pdw.md)|[MAX](../../mpp/sqlpdw/max-sql-server-pdw.md)|  
|[COUNT](../../mpp/sqlpdw/count-sql-server-pdw.md)|[MIN](../../mpp/sqlpdw/min-sql-server-pdw.md)|  
|[COUNT_BIG](../../mpp/sqlpdw/count-big-sql-server-pdw.md)|[SUM](../../mpp/sqlpdw/sum-sql-server-pdw.md)|  
  
### <a name="AnalyticFunctions"></a>Analytic Functions  
Analytic functions perform an aggregate calculation on a group of rows in a result set and return a scalar numeric value. The group of rows is called a window; analytic functions are also referred to as windowing functions. Analytic functions enable you to calculate rankings and percentiles, perform moving window calculations, and lag or lead analysis.  
  
Analytic functions can be used as expressions only in the select list or HAVING clause of a SELECT statement. An analytic function is evaluated over the initial result set produced after the FROM, WHERE, GROUP BY, and HAVING clauses of the query have been evaluated. Only an ORDER BY clause applied to the full query (not the analytic function) is run after an analytic function.  
  
The syntactic characteristic that differentiates analytic functions from all other functions is the presence of an [OVER() clause](../../mpp/sqlpdw/over-clause-sql-server-pdw.md). The OVER() clause serves two purposes. First, for functions that have both an analytic and an aggregate form (for example, SUM), the OVER() clause indicates that the analytic form is being invoked. Second, the contents of the OVER() clause specify how the analytic function computation itself behaves. The OVER() clause can contain an optional PARTITION BY clause and an optional ORDER BY clause. Not all analytic functions support these optional clauses. For example, the ROW_NUMBER function requires an ORDER BY clause; however, the COUNT function, in comparison, does not support an ORDER BY clause. More than one analytic function can be used in a single query. When more than one analytic function is specified, the OVER() clause for each function can differ in partitioning and also ordering.  
  
**Restrictions:** In the presence of an **OVER()** clause as a windowing function, the following restrictions apply.  
  
The following functions require the ORDER BY clause and are valid as a windowing function in both PDW and SQL Server.  
  
-   LAG  
  
-   LEAD  
  
-   PERCENTILE_COUNT<sup>1</sup>  
  
-   PERCENTILE_DISC<sup>1</sup>  
  
-   MIN  
  
-   MAX  
  
-   STDEV  
  
-   STDEVP  
  
-   VAR  
  
-   VARP  
  
-   AVG  
  
-   SUM  
  
The following functions cannot be used in PDW with the ORDER BY clause as a windowing function.  
  
-   COUNT  
  
-   COUNT_BIG  
  
<sup>1</sup>PERCENTILE_CONT and PERCENTILE_DISC require an ORDER BY with a single data value in the WITHIN GROUP clause. They do not support ORDER BY in the OVER() clause. The result of the percentile analytic is calculated from the domain of the ORDER BY data value (either discretely or continuously). PERCENTILE_CONT and PERCENTILE_DISC without an OVER() clause are aggregate functions that are not supported by SQL Server PDW or SQL Server.  
  
In OVER clause for windowing aggregates, PDW supports **PARTITION BY** clause and **RANGE** clause, however PDW does not support **ROWS** clause. SQL Server 2012 and newer supports all that and also supports **ROWS** range with some limitations  
  
### <a name="RankingFunctions"></a>Ranking Functions  
Ranking functions return a ranking value for each row in a partition. Depending on the function that is used, some rows might receive the same value as other rows. Ranking functions are nondeterministic.  
  
||||  
|-|-|-|  
|[DENSE_RANK](../../mpp/sqlpdw/dense-rank-sql-server-pdw.md)|[NTILE](../../mpp/sqlpdw/ntile-sql-server-pdw.md)|[ROW_NUMBER](../../mpp/sqlpdw/row-number-sql-server-pdw.md)|  
|[ROW_NUMBER](../../mpp/sqlpdw/row-number-sql-server-pdw.md)|||  
  
## <a name="Scalar"></a>Scalar Functions  
Scalar functions return the same results each time they are called with a specific set of input values.  
  
### <a name="CastingFunctions"></a>Casting Functions  
Casting functions convert a value of one data type to another data type, typically for presentation or comparison against a value of another data type.  
  
The following table lists the casting functions in SQL Server PDW.  
  
[CAST and CONVERT](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md)  
  
### <a name="CryptoFunctions"></a>Cryptographic Functions  
The following table lists the cryptographic functions in SQL Server PDW.  
  
[HASHBYTES](../../mpp/sqlpdw/hashbytes-sql-server-pdw.md)  
  
### <a name="DateAndTimeFunctions"></a>Date and Time Functions  
The following tables list the date and time functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[DATEADD](../../mpp/sqlpdw/dateadd-sql-server-pdw.md)|[DAY](../../mpp/sqlpdw/day-sql-server-pdw.md)|[SYSDATETIME](../../mpp/sqlpdw/sysdatetime-sql-server-pdw.md)|  
|[DATEDIFF](../../mpp/sqlpdw/datediff-sql-server-pdw.md)|[EOMONTH](../../mpp/sqlpdw/eomonth-sql-server-pdw.md)|[SYSDATETIMEOFFSET](../../mpp/sqlpdw/sysdatetimeoffset-sql-server-pdw.md)|  
|[DATEFROMPARTS](../../mpp/sqlpdw/datefromparts-sql-server-pdw.md)|[GETDATE](../../mpp/sqlpdw/getdate-sql-server-pdw.md)|[SYSUTCDATETIME](../../mpp/sqlpdw/sysutcdatetime-sql-server-pdw.md)|  
|[DATENAME](../../mpp/sqlpdw/datename-sql-server-pdw.md)|[GETUTCDATE](../../mpp/sqlpdw/getutcdate-sql-server-pdw.md)|[TIMEFROMPARTS](../../mpp/sqlpdw/timefromparts-sql-server-pdw.md)|  
|[DATEPART](../../mpp/sqlpdw/datepart-sql-server-pdw.md)|[ISDATE](../../mpp/sqlpdw/isdate-sql-server-pdw.md)|[TODATETIMEOFFSET](../../mpp/sqlpdw/todatetimeoffset-sql-server-pdw.md)|  
|[DATETIME2FROMPARTS](../../mpp/sqlpdw/datetime2fromparts-sql-server-pdw.md)|[MONTH](../../mpp/sqlpdw/month-sql-server-pdw.md)|[YEAR](../../mpp/sqlpdw/year-sql-server-pdw.md)|  
|[DATETIMEFROMPARTS](../../mpp/sqlpdw/datetimefromparts-sql-server-pdw.md)|[SMALLDATETIMEFROMPARTS](../../mpp/sqlpdw/smalldatetimefromparts-sql-server-pdw.md)||  
|[DATETIMEOFFSETFROMPARTS](../../mpp/sqlpdw/datetimeoffsetfromparts-sql-server-pdw.md)|[SWITCHOFFSET](../../mpp/sqlpdw/switchoffset-sql-server-pdw.md)||  
  
### <a name="ErrorFunctions"></a>Error Handling Functions  
The following table lists the error handling functions in SQL Server PDW. Also, see [PRINT](../../mpp/sqlpdw/print-sql-server-pdw.md), [RAISERROR](../../mpp/sqlpdw/raiserror-sql-server-pdw.md), [THROW](../../mpp/sqlpdw/throw-sql-server-pdw.md), [TRY...CATCH](../../mpp/sqlpdw/try-catch-sql-server-pdw.md).  
  
||||  
|-|-|-|  
|[@@ERROR](../../mpp/sqlpdw/error-sql-server-pdw.md)|[ERROR_PROCEDURE](../../mpp/sqlpdw/error-procedure-sql-server-pdw.md)|[ERROR_STATE](../../mpp/sqlpdw/error-state-sql-server-pdw.md)|  
|[ERROR_MESSAGE](../../mpp/sqlpdw/error-message-sql-server-pdw.md)|[ERROR_SEVERITY](../../mpp/sqlpdw/error-severity-sql-server-pdw.md)|[XACT_STATE](../../mpp/sqlpdw/xact-state-sql-server-pdw.md)|  
|[ERROR_NUMBER](../../mpp/sqlpdw/error-number-sql-server-pdw.md)|||  
  
### <a name="MathematicalFunctions"></a>Mathematical Functions  
Mathematical functions perform a calculation based on input values that are provided as arguments and return a scalar numeric value. Scalar means that they return the same results each time they are called with a specific set of input values. Arithmetic functions, such as ABS, FLOOR, and SIGN, return a value having the same data type as the input value. Trigonometric and other functions such as EXP, LOG, LOG10, and SQRT, cast their input values to the **float** type and return a **float** value.  
  
The following table lists the mathematical functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[ABS](../../mpp/sqlpdw/abs-sql-server-pdw.md)|[DEGREES](../../mpp/sqlpdw/degrees-sql-server-pdw.md)|[RADIANS](../../mpp/sqlpdw/radians-sql-server-pdw.md)|  
|[ACOS](../../mpp/sqlpdw/acos-sql-server-pdw.md)|[EXP](../../mpp/sqlpdw/exp-sql-server-pdw.md)|[ROUND](../../mpp/sqlpdw/round-sql-server-pdw.md)|  
|[ASIN](../../mpp/sqlpdw/asin-sql-server-pdw.md)|[FLOOR](../../mpp/sqlpdw/floor-sql-server-pdw.md)|[SIGN](../../mpp/sqlpdw/sign-sql-server-pdw.md)|  
|[ATAN](../../mpp/sqlpdw/atan-sql-server-pdw.md)|[LOG](../../mpp/sqlpdw/log-sql-server-pdw.md)|[SIN](../../mpp/sqlpdw/sin-sql-server-pdw.md)|  
|[ATN2](../../mpp/sqlpdw/atn2-sql-server-pdw.md)|[LOG10](../../mpp/sqlpdw/log10-sql-server-pdw.md)|[SQUARE](../../mpp/sqlpdw/square-sql-server-pdw.md)|  
|[CEILING](../../mpp/sqlpdw/ceiling-sql-server-pdw.md)|[PI](../../mpp/sqlpdw/pi-sql-server-pdw.md)|[SQRT](../../mpp/sqlpdw/sqrt-sql-server-pdw.md)|  
|[COS](../../mpp/sqlpdw/cos-sql-server-pdw.md)|[POWER](../../mpp/sqlpdw/power-sql-server-pdw.md)|[TAN](../../mpp/sqlpdw/tan-sql-server-pdw.md)|  
|[COT](../../mpp/sqlpdw/cot-sql-server-pdw.md)|||  
  
### <a name="MetadataFunctions"></a>Metadata Functions  
Metadata functions return a scalar value providing information about the SQL Server PDW software and objects. The following table lists the metadata functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[COL_NAME](../../mpp/sqlpdw/col-name-sql-server-pdw.md)|[OBJECT_ID](../../mpp/sqlpdw/object-id-sql-server-pdw.md)|[SESSION_USER](../../mpp/sqlpdw/session_user-sql-server-pdw.md)|  
|[COLLATIONPROPERTY](../../mpp/sqlpdw/collationproperty-sql-server-pdw.md)|[OBJECT_NAME](../../mpp/sqlpdw/object-name-sql-server-pdw.md)|[SQL_VARIANT_PROPERTY](../../mpp/sqlpdw/sql-variant-property-sql-server-pdw.md)|  
|[CURRENT_USER](../../mpp/sqlpdw/current-user-sql-server-pdw.md)|[OBJECTPROPERTY](../../mpp/sqlpdw/objectproperty-sql-server-pdw.md)|[SYSTEM_USER](../../mpp/sqlpdw/system-user-sql-server-pdw.md)|  
|[DATABASEPROPERTYEX](../../mpp/sqlpdw/databasepropertyex-sql-server-pdw.md)|[OBJECTPROPERTYEX](../../mpp/sqlpdw/objectpropertyex-sql-server-pdw.md)|[TYPE_ID](../../mpp/sqlpdw/type-id-sql-server-pdw.md)|  
|[DATALENGTH](../../mpp/sqlpdw/datalength-sql-server-pdw.md)|[SCHEMA_ID](../../mpp/sqlpdw/schema-id-sql-server-pdw.md)|[TYPE_NAME](../../mpp/sqlpdw/type-name-sql-server-pdw.md)|  
|[DB_ID (SQL Server PDW)](../../mpp/sqlpdw/db-id-sql-server-pdw.md)|[SCHEMA_NAME](../../mpp/sqlpdw/schema_name-sql-server-pdw.md)|[TYPEPROPERTY](../../mpp/sqlpdw/typeproperty-sql-server-pdw.md)|  
|[DB_NAME](../../mpp/sqlpdw/db-name-sql-server-pdw.md)|[SERVERPROPERTY](../../mpp/sqlpdw/serverproperty-sql-server-pdw.md)|[VERSION](../../mpp/sqlpdw/version-sqlserver-pdw.md)|  
|[INDEXPROPERTY](../../mpp/sqlpdw/indexproperty-sql-server-pdw.md)|[SESSION_ID](../../mpp/sqlpdw/session-id-sql-server-pdw.md)||  
  
### <a name="NullComparisonFunctions"></a>Null and Comparison Functions  
Null and comparison functions perform a calculation based on input values that are provided as arguments and return a scalar numeric value. The following table lists the null and comparison functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[CASE](../../mpp/sqlpdw/case-sql-server-pdw.md)|[ISNULL](../../mpp/sqlpdw/isnull-sql-server-pdw.md)|[NULLIF](../../mpp/sqlpdw/nullif-sql-server-pdw.md)|  
  
### <a name="ODBCFunctions"></a>ODBC Scalar Functions  
ODBC scalar functions can be called from Transact\-SQL. The supported ODBC scalar functions are: BIT_LENGTH, CONCAT, CUR_DATE, CUR_TIME, CURRENT_DATE, CURRENT_TIME, DAYNAME, DAYOFMONTH, DAYOFWEEK, HOUR, MINUTE, MONTHNAME, QUARTER, SECOND, and WEEK. For more information, see [ODBC Scalar Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/odbc-scalar-functions-sql-server-pdw.md).  
  
### <a name="StringFunctions"></a>String Functions  
String functions perform an operation on a string input value and return a scalar string or numeric value. All built-in string functions are deterministic. This means they return the same value any time they are called with a specific set of input values. The following table lists the string functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[ASCII](../../mpp/sqlpdw/ascii-sql-server-pdw.md)|[LTRIM](../../mpp/sqlpdw/ltrim-sql-server-pdw.md)|[RTRIM](../../mpp/sqlpdw/rtrim-sql-server-pdw.md)|  
|[CHAR](../../mpp/sqlpdw/char-sql-server-pdw.md)|[NCHAR](../../mpp/sqlpdw/nchar-sql-server-pdw.md)|[SOUNDEX](../../mpp/sqlpdw/soundex-sql-server-pdw.md)|  
|[CHARINDEX](../../mpp/sqlpdw/charindex-sql-server-pdw.md)|[PARSENAME](../../mpp/sqlpdw/parsename-sql-server-pdw.md)|[SPACE](../../mpp/sqlpdw/space-sql-server-pdw.md)|  
|[CONCAT](../../mpp/sqlpdw/concat-sql-server-pdw.md)|[PATINDEX](../../mpp/sqlpdw/patindex-sql-server-pdw.md)|[STUFF](../../mpp/sqlpdw/stuff-sql-server-pdw.md)|  
|[DIFFERENCE](../../mpp/sqlpdw/difference-sql-server-pdw.md)|[REPLACE](../../mpp/sqlpdw/replace-sql-server-pdw.md)|[SUBSTRING](../../mpp/sqlpdw/substring-sql-server-pdw.md)|  
|[LEFT](../../mpp/sqlpdw/left-sql-server-pdw.md)|[REPLICATE](../../mpp/sqlpdw/replicate-sql-server-pdw.md)|[TERTIARY_WEIGHTS](../../mpp/sqlpdw/tertiary-weights-sql-server-pdw.md)|  
|[LEN](../../mpp/sqlpdw/len-sql-server-pdw.md)|[REVERSE](../../mpp/sqlpdw/reverse-sql-server-pdw.md)|[UNICODE](../../mpp/sqlpdw/unicode-sql-server-pdw.md)|  
|[LOWER](../../mpp/sqlpdw/lower-sql-server-pdw.md)|[RIGHT](../../mpp/sqlpdw/right-sql-server-pdw.md)|[UPPER](../../mpp/sqlpdw/upper-sql-server-pdw.md)|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
  
