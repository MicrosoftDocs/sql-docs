---
title: "DAX formula compatibility in Analysis Services DirectQuery mode | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DAX formula compatibility in DirectQuery mode 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
For tabular 1200 and higher models in DirectQuery mode, many functional limitations in earlier versions no longer apply. For DAX formulas in-particular:

- DirectQuery now generates simpler queries, providing improved performance.
- Row level security (RLS) is now supported in DirectQuery mode.
- Calculated columns are now supported for tabular models in DirectQuery mode.

## DAX functions in DirectQuery mode

In short, all DAX functions are supported for DirectQuery models. However, not all functions are supported for all formula types, and not all functions have been optimized for DirectQuery models. At the most basic level, we can put DAX functions into two camps: Optimized and Non-optimized. Let's first take a closer look at optimized functions.


### Optimized for DirectQuery
These are functions that primarily return scalar or aggregate results. These functions are further divided into those that are supported in all types of formulas: measures, queries, calculated columns, row level security, and those that are supported in measure and query formulas only. These include:    

| Supported in all DAX formulas                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        | Supported in measure and query formulas only                                                                                                                                                                                                                                                                                                |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ABS</br>  ACOS</br>  ACOT</br>  AND</br>  ASIN</br>  ATAN</br>  BLANK</br>  CEILING</br>  CONCATENATE</br>  COS</br>  COT</br>  CURRENCY</br>  DATE</br>  DATEDIFF</br>  DATEVALUE</br>  DAY</br>  DEGREES</br>  DIVIDE</br>  EDATE</br>  EOMONTH</br>  EXACT</br>  EXP</br>  FALSE</br>  FIND</br>  HOUR</br>  IF</br>  INT</br>  ISBLANK</br>  ISO.CEILING</br>  KEEPFILTERS</br>  LEFT</br>  LEN</br>  LN</br>  LOG</br>  LOG10</br>  LOWER</br>  MAX</br>  MID</br>  MIN</br>  MINUTE</br>  MOD</br>  MONTH</br>  MROUND</br>  NOT</br>  NOW</br>  OR</br>  PI</br>  POWER</br>  QUOTIENT</br>  RADIANS</br>  RAND</br>  RELATED</br>  REPT</br>  RIGHT</br>  ROUND</br>  ROUNDDOWN</br>  ROUNDUP</br>  SEARCH</br>  SECOND</br>  SIGN</br>  SIN</br>  SQRT</br>  SQRTPI</br>  SUBSTITUTE</br>  SWITCH</br>  TAN</br>  TIME</br>  TIMEVALUE</br>  TODAY</br>  TRIM</br>  TRUE</br>  TRUNC</br>  UNICODE</br>  UPPER</br>  USERNAME</br>  USERELATIONSHIP</br>  VALUE</br>  WEEKDAY</br>  WEEKNUM</br>  YEAR</br> | ALL</br> ALLEXCEPT</br> ALLNOBLANKROW</br> ALLSELECTED</br> AVERAGE</br> AVERAGEA</br> AVERAGEX</br> CALCULATE</br> CALCULATETABLE</br> COUNT</br> COUNTA</br> COUNTAX</br> COUNTROWS</br> COUNTX</br> DISTINCT</br> DISTINCTCOUNT</br> FILTER</br> FILTERS</br> HASONEFILTER</br> HASONEVALUE</br> ISCROSSFILTERED</br> ISFILTERED</br> MAXA</br> MAXX</br> MIN</br> MINA</br> MINX</br> RELATEDTABLE</br> STDEV.P</br> STDEV.S</br> STDEVX.P</br> STDEVX.S</br> SUM</br> SUMX</br> VALUES</br> VAR.P</br> VAR.S</br> VARX.P</br> VARX.S |



### Non-optimized for DirectQuery
These functions have not been optimized to work with DirectQuery. These functions *are not* supported in calculated column and row-level security formulas at all. However, these functions *are supported* in measure and query formulas, albeit with uncertain performance.

 We're not going to list all of the functions here. Basically, if it's not in one of the lists of optimized functions above, it's a non-optimized function for DirectQuery.

The reasons a particular function might not be optimized for DirectQuery is because the underlying relational engine cannot perform calculations equivalent to those performed by the xVelocity engine, or the formula cannot be converted to an equivalent SQL expression. In other cases, the performance of the converted expression and the resulting calculations may be unacceptable.

To learn about all DAX functions, see the [DAX Function Reference].(https://msdn.microsoft.com/library/ee634396.aspx)

## DAX operators in DirectQuery mode
All DAX comparison and arithmetic operators are fully supported in DirectQuery mode. To learn more, see [DAX Operator Reference](https://msdn.microsoft.com/library/ee634237.aspx).


 
## Differences between in-memory and DirectQuery mode  
Queries on a model deployed in DirectQuery mode can return different results than the same model deployed in in-memory mode. This is because with DirectQuery, data is queried directly from a relational data store and aggregations required by formulas are performed using the relevant relational engine (SQL, Oracle, Teradata), rather than using the xVelocity in-memory analytics engine for storage and calculation.  
  
For example, there are differences in the way that certain relational data stores handle numeric values, dates, nulls, and so forth.  
  
In contrast, the DAX language is intended to emulate as closely as possible the behavior of functions in Microsoft Excel. For example, when handling nulls, empty strings and zero values, Excel attempts to provide the best answer regardless of the precise data type, and therefore the xVelocity engine does the same. However, when a tabular model is deployed in DirectQuery mode and passes formulas to a relational data source, the data must be handled according to the semantics of the relational data source, which typically require distinct handling of empty strings vs. nulls. For this reason, the same formula might return a different result when evaluated against cached data and against data returned solely from the relational store.  
  
Additionally, some functions aren't optimized for DirectQuery mode because the calculation would require the data in the current context be sent to the relational data source as a parameter. For example, measures using time-intelligence functions that reference date ranges in a calendar table. A relational data source might not have a calendar table, or at least one with .  
  
## Semantic differences  
This section lists the types of semantic differences that you can expect, and describes any limitations that might apply to the usage of functions or to query results.  
  
### Comparisons  
DAX in in-memory models support comparisons of two expressions that resolve to scalar values of different data types. However, models that are deployed in DirectQuery mode use the data types and comparison operators of the relational engine, and therefore might return different results.  
  
The following comparisons will always return an error when used in a calculation on a DirectQuery data source:  
  
-   Numeric data type compared to any string data type  
  
-   Numeric data type compared to a Boolean value  
  
-   Any string data type compared to a Boolean value  
  
In general, DAX is more forgiving of data type mismatches in in-memory models and will attempt an implicit cast of values up to two times, as described in this section. However, formulas sent to a relational data store in DirectQuery mode are evaluated more strictly, following the rules of the relational engine, and are more likely to fail.  
  
**Comparisons of strings and numbers**  
EXAMPLE: `"2" < 3`  
  
The formula compares a text string to a number. The expression is **true** in both DirectQuery mode and in-memory models.  
  
In an in-memory model, the result is **true** because numbers as strings are implicitly cast to a numerical data type for comparisons with other numbers. SQL also implicitly casts text numbers as numbers for comparison to numerical data types.  
  
Note that this represents a change in behavior from the first version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)], which would return **false**, because the text "2" would always be considered larger than any number.  
  
**Comparison of text with Boolean**  
EXAMPLE: `"VERDADERO" = TRUE`  
  
This expression compares a text string with a Boolean value. In general, for DirectQuery or In-Memory models, comparing a string value to a Boolean value results in an error. The only exceptions to the rule are when the string contains the word **true** or the word **false**; if the string contains any of true or false values, a conversion to Boolean is made and the comparison takes place giving the logical result.  
  
**Comparison of nulls**  
EXAMPLE: `EVALUATE ROW("X", BLANK() = BLANK())`  
  
This formula compares the SQL equivalent of a null to a null. It returns **true** in in-memory and DirectQuery models; a provision is made in DirectQuery model to guarantee similar behavior to in-memory model.  
  
Note that in Transact-SQL, a null is never equal to a null. However, in DAX, a blank is equal to another blank. This behavior is the same for all in-memory models. It is important to note that DirectQuery mode uses, most of, the semantics of SQL Server; but, in this case it separates from it giving a new behavior to NULL comparisons.  
  
### Casts  
  
There is no cast function as such in DAX, but implicit casts are performed in many comparison and arithmetic operations. It is the comparison or arithmetic operation that determines the data type of the result. For example,  
  
-   Boolean values are treated as numeric in arithmetic operations, such as TRUE + 1, or the function MIN applied to a column of Boolean values. A NOT operation also returns a numeric value.  
  
-   Boolean values are always treated as logical values in comparisons and when used with EXACT, AND, OR, &amp;&amp;, or ||.  
  
**Cast from string to Boolean**  
In in-memory and DirectQuery models, casts are permitted to Boolean values from these strings only: **""** (empty string), **"true"**, **"false"**; where an empty string casts to false value.  
  
Casts to the Boolean data type of any other string results in an error.  
  
**Cast from string to date/time**  
In DirectQuery mode, casts from string representations of dates and times to actual **datetime** values behave the same way as they do in SQL Server.   
  
Models that use the in-memory data store support a more limited range of text formats for dates than the string formats for dates that are supported by SQL Server. However, DAX supports custom date and time formats.  
  
**Cast from string to other non Boolean values**  
When casting from strings to non-Boolean values, DirectQuery mode behaves the same as SQL Server. For more information, see [CAST and CONVERT (Transact-SQL)](http://msdn.microsoft.com/a87d0850-c670-4720-9ad5-6f5a22343ea8).  
  
**Cast from numbers to string not allowed**  
EXAMPLE: `CONCATENATE(102,",345")`  
  
Casting from numbers to strings is not allowed in SQL Server.  
  
This formula returns an error in tabular models and in DirectQuery mode; however, the formula produces a result in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)].  
  
**No support for two-try casts in DirectQuery**  
In-memory models often attempt a second cast when the first one fails. This never happens in DirectQuery mode.  
  
EXAMPLE: `TODAY() + "13:14:15"`  
  
In this expression, the first parameter has type **datetime** and second parameter has type **string**. However, the casts when combining the operands are handled differently. DAX will perform an implicit cast from **string** to **double**. In in-memory models, the formula engine attempts to cast directly to **double**, and if that fails, it will try to cast the string to **datetime**.  
  
In DirectQuery mode, only the direct cast from **string** to **double** will be applied. If this cast fails, the formula will return an error.  
  
### Math functions and arithmetic operations  
Some mathematical functions will return different results in DirectQuery mode because of differences in the underlying data type or the casts that can be applied in operations. Also, the restrictions described above on the allowed range of values might affect the outcome of arithmetic operations.  
  
**Order of addition**  
When you create a formula that adds a series of numbers, an in-memory model might process the numbers in a different order than a DirectQuery model.  Therefore, when you have many very large positive numbers and very large negative numbers, you may get an error in one operation and results in another operation.  
  
**Use of the POWER function**  
EXAMPLE: `POWER(-64, 1/3)`  
  
In DirectQuery mode, the POWER function cannot use negative values as the base when raised to a fractional exponent. This is the expected behavior in SQL Server.  
  
In an in-memory model, the formula returns -4.  
  
**Numerical overflow operations**  
In Transact-SQL, operations that result in a numerical overflow return an overflow error; therefore, formulas that result in an overflow also raise an error in DirectQuery mode.  
  
However, the same formula when used in an in-memory model returns an eight-byte integer. That is because the formula engine does not perform checks for numerical overflows.  
  
**LOG functions with blanks return different results**  
SQL Server handles nulls and blanks differently than the xVelocity engine. As a result, the following formula returns an error in DirectQuery mode, but return infinity (-inf) in in-memory mode.  
  
`EXAMPLE: LOG(blank())`  
  
The same limitations apply to the other logarithmic functions: LOG10 and LN.  
  
For more information about the **blank** data type in DAX, see [DAX Syntax Reference](https://msdn.microsoft.com/library/ee634217.aspx).  
  
**Division by 0 and division by Blank**  
In DirectQuery mode, division by zero (0) or division by BLANK will always result in an error. SQL Server does not support the notion of infinity, and because the natural result of any division by 0 is infinity, the result is an error. However, SQL Server supports division by nulls, and the result must always equal null.  
  
Rather than return different results for these operations, in DirectQuery mode, both types of operations (division by zero and division by null) return an error.  
  
Note that, in Excel and in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] models, division by zero also returns an error. Division by a blank returns a blank.  
  
The following expressions are all valid in in-memory models, but will fail in DirectQuery mode:  
  
`1/BLANK`  
  
`1/0`  
  
`0.0/BLANK`  
  
`0/0`  
  
The expression `BLANK/BLANK` is a special case that returns `BLANK` in both for in-memory models, and in DirectQuery mode.  
  
### Supported numeric and date-time ranges  
Formulas in in-memory tabular model are subject to the same limitations as Excel with regard to maximum allowed values for real numbers and dates. However, differences can arise when the maximum value is returned from a calculation or query, or when values are converted, cast, rounded, or truncated.  
  
-   If values of types **Currency** and **Real** are multiplied, and the result is larger than the maximum possible value, in DirectQuery mode, no error is raised, and a null is returned.  
  
-   In in-memory models, no error is raised, but the maximum value is returned.  
  
In general, because the accepted date ranges are different for Excel and SQL Server, results can be guaranteed to match only when dates are within the common date range, which is inclusive of the following dates:  
  
-   Earliest date: March 1, 1990  
  
-   Latest date: December 31, 9999  
  
If any dates used in formulas fall outside this range, either the formula will result in an error, or the results will not match.  
  
**Floating point values supported by CEILING**  
EXAMPLE: `EVALUATE ROW("x", CEILING(-4.398488E+30, 1))`  
  
The Transact-SQL equivalent of the DAX CEILING function only supports values with magnitude of 10^19 or less. A rule of thumb is that floating point values should be able to fit into **bigint**.  
  
**Datepart functions with dates that are out of range**  
Results in DirectQuery mode are guaranteed to match those in in-memory models only when the date used as the argument is in the valid date range. If these conditions are not satisfied, either an error will be raised, or the formula will return different results in DirectQuery than in in-memory mode.  
  
EXAMPLE: `MONTH(0)` or `YEAR(0)`  
  
In DirectQuery mode, the expressions return 12 and 1899, respectively.  
  
In in-memory models, the expressions return 1 and 1900, respectively.  
  
EXAMPLE:  `EOMONTH(0.0001, 1)`  
  
The results of this expression will match only when the data supplied as a parameter is within the valid date range.  
  
EXAMPLE: `EOMONTH(blank(), blank())` or `EDATE(blank(), blank())`  
  
The results of this expression should be the same in DirectQuery mode and in-memory mode.  
  
**Truncation of time values**  
EXAMPLE: `SECOND(1231.04097222222)`  
  
In DirectQuery mode, the result is truncated, following the rules of SQL Server, and the expression evaluates to 59.  
  
In in-memory models, the results of each interim operation are rounded; therefore, the expression evaluates to 0.  
  
The following example demonstrates how this value is calculated:  
  
1.  The fraction of the input (0.04097222222) is multiplied by 24.  
  
2.  The resulting hour value (0.98333333328) is multiplied by 60.  
  
3.  The resulting minute value is 58.9999999968.  
  
4.  The fraction of the minute value (0.9999999968) is multiplied by 60.  
  
5.  The resulting second value (59.999999808) rounds up to 60.  
  
6.  60 is equivalent to 0.  
  
**SQL Time data type not supported**  
In-memory models do not support use of the new SQL **Time** data type. In DirectQuery mode, formulas that reference columns with this data type will return an error. Time data columns cannot be imported into an in-memory model.  
  
However, sometimes the engine casts the time value to an acceptable data type, and the formula returns a result.  
  
This behavior affects all functions that use a date column as a parameter.  
  
### <a name="bkmk_Currency"></a>Currency  
In DirectQuery mode, if the result of an arithmetic operation has the type **Currency**, the value must be within the following range:  
  
-   Minimum: -922337203685477.5808  
  
-   Maximum: 922337203685477.5807  
  
**Combining currency and REAL data types**  
EXAMPLE: `Currency sample 1`  
  
If **Currency** and **Real** types are multiplied, and the result is larger than 9223372036854774784 (0x7ffffffffffffc00), DirectQuery mode will not raise an error.  
  
In an in-memory model, an error is raised if the absolute value of the result is larger than 922337203685477.4784.  
  
**Operation results in an out-of-range value**  
EXAMPLE: `Currency sample 2`  
  
If operations on any two currency values result in a value that is outside the specified range, an error is raised in in-memory models, but not in DirectQuery models.  
  
**Combining currency with other data types**  
Division of currency values by values of other numeric types can result in different results.  
  
### <a name="bkmk_Aggregations"></a>Aggregation functions  
Statistical functions on a table with one row return different results. Aggregation functions over empty tables also behave differently in in-memory models than they do in DirectQuery mode.  
  
**Statistical functions over a table with a single row**  
If the table that is used as argument contains a single row, in DirectQuery mode, statistical functions such as STDEV and VARx return null.  
  
In an in-memory model, a formula that uses STDEV or VARx over a table with a single row returns a division by zero error.  
  
### <a name="bkmk_Text"></a>Text functions  
Because relational data stores provide different text data types than does Excel, you may see different results when searching strings or working with substrings. The length of strings also can be different.  
  
In general, any string manipulation functions that use fixed-size columns as arguments can have different results.  
  
Additionally, in SQL Server, some text functions support additional arguments that are not provided in Excel. If the formula requires the missing argument you can get different results or errors in the in-memory model.  
  
**Operations that return a character using LEFT, RIGHT, etc. may return the correct character but in a different case, or no results**  
EXAMPLE: `LEFT(["text"], 2)`  
  
In DirectQuery mode, the case of the character that is returned is always exactly the same as the letter that is stored in the database. However, the xVelocity engine uses a different algorithm for compression and indexing of values, to improve performance.  
  
By default, the Latin1_General collation is used, which is case-insensitive but accent-sensitive. Therefore, if there are multiple instances of a text string in lower case, upper case, or mixed case, all instances are considered the same string, and only the first instance of the string is stored in the index. All text functions that operate on stored strings will retrieve the specified portion of the indexed form. Therefore, the example formula would return the same value for the entire column, using the first instance as the input.  
  
This behavior also applies to other text functions, including RIGHT, MID, and so forth.  
  
**String length affects results**  
EXAMPLE: `SEARCH("within string", "sample target  text", 1, 1)`  
  
If you search for a string using the SEARCH function, and the target string is longer than the within string, DirectQuery mode raises an error.  
  
In an in-memory model, the searched string is returned, but with its length truncated to the length of &lt;within text&gt;.  
  
EXAMPLE: `EVALUATE ROW("X", REPLACE("CA", 3, 2, "California") )`  
  
If the length of the replacement string is greater than the length of the original string, in DirectQuery mode, the formula returns null.  
  
In in-memory models, the formula follows the behavior of Excel, which concatenates the source string and the replacement string, which returns CACalifornia.  
  
**Implicit TRIM in the middle of strings**  
EXAMPLE: `TRIM(" A sample sentence with leading white space")`  
  
DirectQuery mode translates the DAX TRIM function to the SQL statement `LTRIM(RTRIM(<column>))`. As a result, only leading and trailing white space is removed.  
  
In contrast, the same formula in an in-memory model removes spaces within the string, following the behavior of Excel.  
  
**Implicit RTRIM with use of LEN function**  
EXAMPLE: `LEN('string_column')`  
  
Like SQL Server, DirectQuery mode automatically removes white space from the end of string columns: that is, it performs an implicit RTRIM. Therefore, formulas that use the LEN function can return different values if the string has trailing spaces.  
  
**In-memory supports additional parameters for SUBSTITUTE**  
EXAMPLE: `SUBSTITUTE([Title],"Doctor","Dr.")`  
  
EXAMPLE: `SUBSTITUTE([Title],"Doctor","Dr.", 2)`  
  
In DirectQuery mode, you can use only the version of this function that has three (3) parameters: a reference to a column, the old text, and the new text. If you use the second formula, an error is raised.  
  
In in-memory models, you can use an optional fourth parameter to specify the instance number of the string to replace. For example, you can replace only the second instance, etc.  
  
**Restrictions on string lengths for REPT operations**  
In in-memory models, the length of a string resulting from an operation using REPT must be less than 32,767 characters.  
  
This limitation does not apply in DirectQuery mode.  
  
**Substring operations return different results depending on character type**  
EXAMPLE: `MID([col], 2, 5)`  
  
If the input text is **varchar** or **nvarchar**, the result of the formula is always the same.  
  
However, if the text is a fixed-length character and the value for *&lt;num_chars&gt;* is greater than the length of the target string, in DirectQuery mode, a blank is added at the end of the result string.  
  
In an in-memory model, the result terminates at the last string character, with no padding.  


## See also  
[DirectQuery Mode](http://msdn.microsoft.com/45ad2965-05ec-4fb1-a164-d8060b562ea5)  
  


