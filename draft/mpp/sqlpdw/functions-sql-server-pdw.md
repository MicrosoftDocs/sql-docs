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
|[AVG](../sqlpdw/avg-sql-server-pdw.md)|[MAX](../sqlpdw/max-sql-server-pdw.md)|  
|[COUNT](../sqlpdw/count-sql-server-pdw.md)|[MIN](../sqlpdw/min-sql-server-pdw.md)|  
|[COUNT_BIG](../sqlpdw/count-big-sql-server-pdw.md)|[SUM](../sqlpdw/sum-sql-server-pdw.md)|  
  
### <a name="AnalyticFunctions"></a>Analytic Functions  
Analytic functions perform an aggregate calculation on a group of rows in a result set and return a scalar numeric value. The group of rows is called a window; analytic functions are also referred to as windowing functions. Analytic functions enable you to calculate rankings and percentiles, perform moving window calculations, and lag or lead analysis.  
  
Analytic functions can be used as expressions only in the select list or HAVING clause of a SELECT statement. An analytic function is evaluated over the initial result set produced after the FROM, WHERE, GROUP BY, and HAVING clauses of the query have been evaluated. Only an ORDER BY clause applied to the full query (not the analytic function) is run after an analytic function.  
  
The syntactic characteristic that differentiates analytic functions from all other functions is the presence of an [OVER() clause](../sqlpdw/over-clause-sql-server-pdw.md). The OVER() clause serves two purposes. First, for functions that have both an analytic and an aggregate form (for example, SUM), the OVER() clause indicates that the analytic form is being invoked. Second, the contents of the OVER() clause specify how the analytic function computation itself behaves. The OVER() clause can contain an optional PARTITION BY clause and an optional ORDER BY clause. Not all analytic functions support these optional clauses. For example, the ROW_NUMBER function requires an ORDER BY clause; however, the COUNT function, in comparison, does not support an ORDER BY clause. More than one analytic function can be used in a single query. When more than one analytic function is specified, the OVER() clause for each function can differ in partitioning and also ordering.  
  
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
|[DENSE_RANK](../sqlpdw/dense-rank-sql-server-pdw.md)|[NTILE](../sqlpdw/ntile-sql-server-pdw.md)|[ROW_NUMBER](../sqlpdw/row-number-sql-server-pdw.md)|  
|[ROW_NUMBER](../sqlpdw/row-number-sql-server-pdw.md)|||  
  
## <a name="Scalar"></a>Scalar Functions  
Scalar functions return the same results each time they are called with a specific set of input values.  
  
### <a name="CastingFunctions"></a>Casting Functions  
Casting functions convert a value of one data type to another data type, typically for presentation or comparison against a value of another data type.  
  
The following table lists the casting functions in SQL Server PDW.  
  
[CAST and CONVERT](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
  
### <a name="CryptoFunctions"></a>Cryptographic Functions  
The following table lists the cryptographic functions in SQL Server PDW.  
  
[HASHBYTES](../sqlpdw/hashbytes-sql-server-pdw.md)  
  
### <a name="DateAndTimeFunctions"></a>Date and Time Functions  
The following tables list the date and time functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[DATEADD](../sqlpdw/dateadd-sql-server-pdw.md)|[DAY](../sqlpdw/day-sql-server-pdw.md)|[SYSDATETIME](../sqlpdw/sysdatetime-sql-server-pdw.md)|  
|[DATEDIFF](../sqlpdw/datediff-sql-server-pdw.md)|[EOMONTH](../sqlpdw/eomonth-sql-server-pdw.md)|[SYSDATETIMEOFFSET](../sqlpdw/sysdatetimeoffset-sql-server-pdw.md)|  
|[DATEFROMPARTS](../sqlpdw/datefromparts-sql-server-pdw.md)|[GETDATE](../sqlpdw/getdate-sql-server-pdw.md)|[SYSUTCDATETIME](../sqlpdw/sysutcdatetime-sql-server-pdw.md)|  
|[DATENAME](../sqlpdw/datename-sql-server-pdw.md)|[GETUTCDATE](../sqlpdw/getutcdate-sql-server-pdw.md)|[TIMEFROMPARTS](../sqlpdw/timefromparts-sql-server-pdw.md)|  
|[DATEPART](../sqlpdw/datepart-sql-server-pdw.md)|[ISDATE](../sqlpdw/isdate-sql-server-pdw.md)|[TODATETIMEOFFSET](../sqlpdw/todatetimeoffset-sql-server-pdw.md)|  
|[DATETIME2FROMPARTS](../sqlpdw/datetime2fromparts-sql-server-pdw.md)|[MONTH](../sqlpdw/month-sql-server-pdw.md)|[YEAR](../sqlpdw/year-sql-server-pdw.md)|  
|[DATETIMEFROMPARTS](../sqlpdw/datetimefromparts-sql-server-pdw.md)|[SMALLDATETIMEFROMPARTS](../sqlpdw/smalldatetimefromparts-sql-server-pdw.md)||  
|[DATETIMEOFFSETFROMPARTS](../sqlpdw/datetimeoffsetfromparts-sql-server-pdw.md)|[SWITCHOFFSET](../sqlpdw/switchoffset-sql-server-pdw.md)||  
  
### <a name="ErrorFunctions"></a>Error Handling Functions  
The following table lists the error handling functions in SQL Server PDW. Also, see [PRINT](../sqlpdw/print-sql-server-pdw.md), [RAISERROR](../sqlpdw/raiserror-sql-server-pdw.md), [THROW](../sqlpdw/throw-sql-server-pdw.md), [TRY...CATCH](../sqlpdw/try-catch-sql-server-pdw.md).  
  
||||  
|-|-|-|  
|[@@ERROR](../sqlpdw/error-sql-server-pdw.md)|[ERROR_PROCEDURE](../sqlpdw/error-procedure-sql-server-pdw.md)|[ERROR_STATE](../sqlpdw/error-state-sql-server-pdw.md)|  
|[ERROR_MESSAGE](../sqlpdw/error-message-sql-server-pdw.md)|[ERROR_SEVERITY](../sqlpdw/error-severity-sql-server-pdw.md)|[XACT_STATE](../sqlpdw/xact-state-sql-server-pdw.md)|  
|[ERROR_NUMBER](../sqlpdw/error-number-sql-server-pdw.md)|||  
  
### <a name="MathematicalFunctions"></a>Mathematical Functions  
Mathematical functions perform a calculation based on input values that are provided as arguments and return a scalar numeric value. Scalar means that they return the same results each time they are called with a specific set of input values. Arithmetic functions, such as ABS, FLOOR, and SIGN, return a value having the same data type as the input value. Trigonometric and other functions such as EXP, LOG, LOG10, and SQRT, cast their input values to the **float** type and return a **float** value.  
  
The following table lists the mathematical functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[ABS](../sqlpdw/abs-sql-server-pdw.md)|[DEGREES](../sqlpdw/degrees-sql-server-pdw.md)|[RADIANS](../sqlpdw/radians-sql-server-pdw.md)|  
|[ACOS](../sqlpdw/acos-sql-server-pdw.md)|[EXP](../sqlpdw/exp-sql-server-pdw.md)|[ROUND](../sqlpdw/round-sql-server-pdw.md)|  
|[ASIN](../sqlpdw/asin-sql-server-pdw.md)|[FLOOR](../sqlpdw/floor-sql-server-pdw.md)|[SIGN](../sqlpdw/sign-sql-server-pdw.md)|  
|[ATAN](../sqlpdw/atan-sql-server-pdw.md)|[LOG](../sqlpdw/log-sql-server-pdw.md)|[SIN](../sqlpdw/sin-sql-server-pdw.md)|  
|[ATN2](../sqlpdw/atn2-sql-server-pdw.md)|[LOG10](../sqlpdw/log10-sql-server-pdw.md)|[SQUARE](../sqlpdw/square-sql-server-pdw.md)|  
|[CEILING](../sqlpdw/ceiling-sql-server-pdw.md)|[PI](../sqlpdw/pi-sql-server-pdw.md)|[SQRT](../sqlpdw/sqrt-sql-server-pdw.md)|  
|[COS](../sqlpdw/cos-sql-server-pdw.md)|[POWER](../sqlpdw/power-sql-server-pdw.md)|[TAN](../sqlpdw/tan-sql-server-pdw.md)|  
|[COT](../sqlpdw/cot-sql-server-pdw.md)|||  
  
### <a name="MetadataFunctions"></a>Metadata Functions  
Metadata functions return a scalar value providing information about the SQL Server PDW software and objects. The following table lists the metadata functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[COL_NAME](../sqlpdw/col-name-sql-server-pdw.md)|[OBJECT_ID](../sqlpdw/object-id-sql-server-pdw.md)|[SESSION_USER](../sqlpdw/session_user-sql-server-pdw.md)|  
|[COLLATIONPROPERTY](../sqlpdw/collationproperty-sql-server-pdw.md)|[OBJECT_NAME](../sqlpdw/object-name-sql-server-pdw.md)|[SQL_VARIANT_PROPERTY](../sqlpdw/sql-variant-property-sql-server-pdw.md)|  
|[CURRENT_USER](../sqlpdw/current-user-sql-server-pdw.md)|[OBJECTPROPERTY](../sqlpdw/objectproperty-sql-server-pdw.md)|[SYSTEM_USER](../sqlpdw/system-user-sql-server-pdw.md)|  
|[DATABASEPROPERTYEX](../sqlpdw/databasepropertyex-sql-server-pdw.md)|[OBJECTPROPERTYEX](../sqlpdw/objectpropertyex-sql-server-pdw.md)|[TYPE_ID](../sqlpdw/type-id-sql-server-pdw.md)|  
|[DATALENGTH](../sqlpdw/datalength-sql-server-pdw.md)|[SCHEMA_ID](../sqlpdw/schema-id-sql-server-pdw.md)|[TYPE_NAME](../sqlpdw/type-name-sql-server-pdw.md)|  
|[DB_ID (SQL Server PDW)](../sqlpdw/db-id-sql-server-pdw.md)|[SCHEMA_NAME](../sqlpdw/schema_name-sql-server-pdw.md)|[TYPEPROPERTY](../sqlpdw/typeproperty-sql-server-pdw.md)|  
|[DB_NAME](../sqlpdw/db-name-sql-server-pdw.md)|[SERVERPROPERTY](../sqlpdw/serverproperty-sql-server-pdw.md)|[VERSION](../sqlpdw/version-sqlserver-pdw.md)|  
|[INDEXPROPERTY](../sqlpdw/indexproperty-sql-server-pdw.md)|[SESSION_ID](../sqlpdw/session-id-sql-server-pdw.md)||  
  
### <a name="NullComparisonFunctions"></a>Null and Comparison Functions  
Null and comparison functions perform a calculation based on input values that are provided as arguments and return a scalar numeric value. The following table lists the null and comparison functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[CASE](../sqlpdw/case-sql-server-pdw.md)|[ISNULL](../sqlpdw/isnull-sql-server-pdw.md)|[NULLIF](../sqlpdw/nullif-sql-server-pdw.md)|  
  
### <a name="ODBCFunctions"></a>ODBC Scalar Functions  
ODBC scalar functions can be called from Transact\-SQL. The supported ODBC scalar functions are: BIT_LENGTH, CONCAT, CUR_DATE, CUR_TIME, CURRENT_DATE, CURRENT_TIME, DAYNAME, DAYOFMONTH, DAYOFWEEK, HOUR, MINUTE, MONTHNAME, QUARTER, SECOND, and WEEK. For more information, see [ODBC Scalar Functions &#40;SQL Server PDW&#41;](../sqlpdw/odbc-scalar-functions-sql-server-pdw.md).  
  
### <a name="StringFunctions"></a>String Functions  
String functions perform an operation on a string input value and return a scalar string or numeric value. All built-in string functions are deterministic. This means they return the same value any time they are called with a specific set of input values. The following table lists the string functions in SQL Server PDW.  
  
||||  
|-|-|-|  
|[ASCII](../sqlpdw/ascii-sql-server-pdw.md)|[LTRIM](../sqlpdw/ltrim-sql-server-pdw.md)|[RTRIM](../sqlpdw/rtrim-sql-server-pdw.md)|  
|[CHAR](../sqlpdw/char-sql-server-pdw.md)|[NCHAR](../sqlpdw/nchar-sql-server-pdw.md)|[SOUNDEX](../sqlpdw/soundex-sql-server-pdw.md)|  
|[CHARINDEX](../sqlpdw/charindex-sql-server-pdw.md)|[PARSENAME](../sqlpdw/parsename-sql-server-pdw.md)|[SPACE](../sqlpdw/space-sql-server-pdw.md)|  
|[CONCAT](../sqlpdw/concat-sql-server-pdw.md)|[PATINDEX](../sqlpdw/patindex-sql-server-pdw.md)|[STUFF](../sqlpdw/stuff-sql-server-pdw.md)|  
|[DIFFERENCE](../sqlpdw/difference-sql-server-pdw.md)|[REPLACE](../sqlpdw/replace-sql-server-pdw.md)|[SUBSTRING](../sqlpdw/substring-sql-server-pdw.md)|  
|[LEFT](../sqlpdw/left-sql-server-pdw.md)|[REPLICATE](../sqlpdw/replicate-sql-server-pdw.md)|[TERTIARY_WEIGHTS](../sqlpdw/tertiary-weights-sql-server-pdw.md)|  
|[LEN](../sqlpdw/len-sql-server-pdw.md)|[REVERSE](../sqlpdw/reverse-sql-server-pdw.md)|[UNICODE](../sqlpdw/unicode-sql-server-pdw.md)|  
|[LOWER](../sqlpdw/lower-sql-server-pdw.md)|[RIGHT](../sqlpdw/right-sql-server-pdw.md)|[UPPER](../sqlpdw/upper-sql-server-pdw.md)|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
