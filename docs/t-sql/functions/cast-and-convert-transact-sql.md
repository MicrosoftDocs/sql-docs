---
title: "CAST and CONVERT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CAST_TSQL"
  - "CONVERT_TSQL"
  - "CAST"
  - "CONVERT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CAST function"
  - "automatic data type conversion"
  - "varbinary data type"
  - "CONVERT function"
  - "data types [SQL Server], converting"
  - "large value data types"
  - "implicit data type conversions"
  - "image data type, converting"
  - "explicit data type conversions [SQL Server]"
  - "binary [SQL Server], converting"
  - "text data type, converting"
  - "date and time [SQL Server], cast and convert"
  - "truncating conversions"
  - "converting data types [SQL Server], conversion functions"
  - "time zones [SQL Server]"
  - "roundtrip conversions"
ms.assetid: a87d0850-c670-4720-9ad5-6f5a22343ea8
caps.latest.revision: 136
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CAST and CONVERT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Converts an expression of one data type to another.
> [!TIP]
> Many [examples](#BKMK_examples) are at the bottom of this topic.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for CAST:  
CAST ( expression AS data_type [ ( length ) ] )  
  
-- Syntax for CONVERT:  
CONVERT ( data_type [ ( length ) ] , expression [ , style ] )  
```  
  
## Arguments  
*expression*  
Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md).
  
*data_type*  
Is the target data type. This includes **xml**, **bigint**, and **sql_variant**. Alias data types cannot be used.
  
*length*  
Is an optional integer that specifies the length of the target data type. The default value is 30.
  
*style*  
Is an integer expression that specifies how the CONVERT function is to translate *expression*. If style is NULL, NULL is returned. The range is determined by *data_type*. For more information, see the Remarks section.
  
## Return types
Returns *expression* translated to *data_type*.

[Jump to the 15 examples at the end of this topic](#BKMK_examples)
  
## Remarks  
  
## Date and Time Styles  
When *expression* is a date or time data type,  *style* can be one of the values shown in the following table. Other values are processed as 0. . Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the only styles that are supported when converting from date and time types to **datetimeoffset** are 0 or 1. All other conversion styles return error 9809.
  
>  [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the date format in Arabic style by using the Kuwaiti algorithm.
  
|Without century (yy) (<sup>1</sup>)|With century (yyyy)|Standard|Input/Output (<sup>3</sup>)|  
|---|---|--|---|
|-|**0** or **100** (<sup>1,</sup><sup>2</sup>)|Default for datetime and smalldatetime|mon dd yyyy hh:miAM (or PM)|  
|**1**|**101**|U.S.|1 = mm/dd/yy<br /> 101 = mm/dd/yyyy|  
|**2**|**102**|ANSI|2 = yy.mm.dd<br /> 102 = yyyy.mm.dd|  
|**3**|**103**|British/French|3 = dd/mm/yy<br /> 103 = dd/mm/yyyy|  
|**4**|**104**|German|4 = dd.mm.yy<br /> 104 = dd.mm.yyyy|  
|**5**|**105**|Italian|5 = dd-mm-yy<br /> 105 = dd-mm-yyyy|  
|**6**|**106** <sup>(1)</sup>|-|6 = dd mon yy<br /> 106 = dd mon yyyy|  
|**7**|**107** <sup>(1)</sup>|-|7 = Mon dd, yy<br /> 107 = Mon dd, yyyy|  
|**8**|**108**|-|hh:mi:ss|  
|-|**9** or **109** (<sup>1,</sup><sup>2</sup>)|Default + milliseconds|mon dd yyyy hh:mi:ss:mmmAM (or PM)|  
|**10**|**110**|USA|10 = mm-dd-yy<br /> 110 = mm-dd-yyyy|  
|**11**|**111**|JAPAN|11 = yy/mm/dd<br /> 111 = yyyy/mm/dd|  
|**12**|**112**|ISO|12 = yymmdd<br /> 112 = yyyymmdd|  
|-|**13** or **113** (<sup>1,</sup><sup>2</sup>)|Europe default + milliseconds|dd mon yyyy hh:mi:ss:mmm(24h)|  
|**14**|**114**|-|hh:mi:ss:mmm(24h)|  
|-|**20** or **120** (<sup>2</sup>)|ODBC canonical|yyyy-mm-dd hh:mi:ss(24h)|  
|-|**21** or **121** (<sup>2</sup>)|ODBC canonical (with milliseconds) default for time, date, datetime2, and datetimeoffset|yyyy-mm-dd hh:mi:ss.mmm(24h)|  
|-|**126** (<sup>4</sup>)|ISO8601|yyyy-mm-ddThh:mi:ss.mmm (no spaces)<br /> Note: When the value for milliseconds (mmm) is 0, the millisecond value is not displayed. For example, the value '2012-11-07T18:26:20.000 is displayed as '2012-11-07T18:26:20'.|  
|-|**127**(<sup>6, 7</sup>)|ISO8601 with time zone Z.|yyyy-mm-ddThh:mi:ss.mmmZ (no spaces)<br /> Note: When the value for milliseconds (mmm) is 0, the milliseconds value is not displayed. For example, the value '2012-11-07T18:26:20.000 is displayed as '2012-11-07T18:26:20'.|  
|-|**130** (<sup>1,</sup><sup>2</sup>)|Hijri (<sup>5</sup>)|dd mon yyyy hh:mi:ss:mmmAM<br /> In this style, mon represents a multi-token Hijri unicode representation of the full month's name. This value will not render correctly on a default US installation of SSMS.|  
|-|**131** (<sup>2</sup>)|Hijri (<sup>5</sup>)|dd/mm/yyyy hh:mi:ss:mmmAM|  
  
<sup>1</sup> These style values return nondeterministic results. Includes all (yy) (without century) styles and a subset of (yyyy) (with century) styles.
  
<sup>2</sup> The default values (*style** *0** or **100**, **9** or **109**, **13** or **113**, **20** or **120**, and **21** or **121**) always return the century (yyyy).
  
<sup>3</sup> Input when you convert to **datetime**; output when you convert to character data.
  
<sup>4</sup> Designed for XML use. For conversion from **datetime** or **smalldatetime** to character data, the output format is as described in the previous table.
  
<sup>5</sup> Hijri is a calendar system with several variations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the Kuwaiti algorithm.
  
> [!IMPORTANT]  
>  By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets two-digit years based on a cutoff year of 2049. That is, the two-digit year 49 is interpreted as 2049 and the two-digit year 50 is interpreted as 1950. Many client applications, such as those based on Automation objects, use a cutoff year of 2030. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the two digit year cutoff configuration option that changes the cutoff year used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and allows for the consistent treatment of dates. We recommend specifying four-digit years.  
  
<sup>6</sup> Only supported when casting from character data to **datetime** or **smalldatetime**. When character data that represents only date or only time components is cast to the **datetime** or **smalldatetime** data types, the unspecified time component is set to 00:00:00.000, and the unspecified date component is set to 1900-01-01.
  
<sup>7</sup>The optional time zone indicator, Z, is used to make it easier to map XML **datetime** values that have time zone information to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** values that have no time zone. Z is the indicator for time zone UTC-0. Other time zones are indicated with HH:MM offset in the + or - direction. For example: `2006-12-12T23:45:12-08:00`.
  
When you convert to character data from **smalldatetime**, the styles that include seconds or milliseconds show zeros in these positions. You can truncate unwanted date parts when you convert from **datetime** or **smalldatetime** values by using an appropriate **char** or **varchar** data type length.
  
When you convert to **datetimeoffset** from character data with a style that includes a time, a time zone offset is appended to the result.
  
## float and real styles
When *expression* is **float** or **real**, *style* can be one of the values shown in the following table. Other values are processed as 0.
  
|Value|Output|  
|---|---|
|**0** (default)|A maximum of 6 digits. Use in scientific notation, when appropriate.|  
|**1**|Always 8 digits. Always use in scientific notation.|  
|**2**|Always 16 digits. Always use in scientific notation.|  
|**3**|Always 17 digits. Use for lossless conversion. With this style, every distinct float or real value is guaranteed to convert to a distinct character string.<br /> **Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and starting in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].|  
|**126, 128, 129**|Included for legacy reasons and might be deprecated in a future release.|  
  
## money and smallmoney styles
When *expression* is **money** or **smallmoney**, *style* can be one of the values shown in the following table. Other values are processed as 0.
  
|Value|Output|  
|---|---|
|**0** (default)|No commas every three digits to the left of the decimal point, and two digits to the right of the decimal point; for example, 4235.98.|  
|**1**|Commas every three digits to the left of the decimal point, and two digits to the right of the decimal point; for example, 3,510.92.|  
|**2**|No commas every three digits to the left of the decimal point, and four digits to the right of the decimal point; for example, 4235.9819.|  
|**126**|Equivalent to style 2 when converting to char(n) or varchar(n)|  
  
## xml styles
When *expression* is **xml***, style* can be one of the values shown in the following table. Other values are processed as 0.
  
|Value|Output|  
|---|---|
|**0** (default)|Use default parsing behavior that discards insignificant white space and does not allow for an internal DTD subset.<br /> **Note:** When you convert to the **xml** data type, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] insignificant white space is handled differently than in XML 1.0. For more information, see [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md).|  
|**1**|Preserve insignificant white space. This style setting sets the default **xml:space** handling to behave the same as if **xml:space="preserve"** has been specified instead.|  
|**2**|Enable limited internal DTD subset processing.<br /><br /> If enabled, the server can use the following information that is provided in an internal DTD subset to perform nonvalidating parse operations.<br /> -Defaults for attributes are applied.<br /> -Internal entity references are resolved and expanded.<br /> -The DTD content model will be checked for syntactical correctness.<br /> The parser will ignore external DTD subsets. It also does not evaluate the XML declaration to see whether the **standalone** attribute is set **yes** or **no**, but instead parses the XML instance as if it is a stand-alone document.|  
|**3**|Preserve insignificant white space and enable limited internal DTD subset processing.|  
  
## Binary styles
When *expression* is **binary(n)**, **varbinary(n)**, **char(n)**, or **varchar(n)**, *style* can be one of the values shown in the following table. Style values that are not listed in the table return an error.
  
|Value|Output|  
|---|---|
|**0** (default)|Translates ASCII characters to binary bytes or binary bytes to ASCII characters. Each character or byte is converted 1:1.<br /> If the *data_type* is a binary type, the characters 0x are added to the left of the result.|  
|**1**, **2**|If the *data_type* is a binary type, the expression must be a character expression. The *expression* must be composed of an even number of hexadecimal digits (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F, a, b, c, d, e, f). If the *style* is set to 1 the characters 0x must be the first two characters in the expression. If the expression contains an odd number of characters or if any of the characters are invalid an error is raised.<br /> If the length of the converted expression is greater than the length of the *data_type* the result will be right truncated.<br /> Fixed length *data_types* that are larger then the converted result will have zeros added to the right of the result.<br /> If the data_type is a character type, the expression must be a binary expression. Each binary character is converted into two hexadecimal characters. If the length of the converted expression is greater than the *data_type* length it will be right truncated.<br /> If the *data_type* is a fix sized character type and the length of the converted result is less than its length of the *data_type*; spaces are added to the right of the converted expression to maintain an even number of hexadecimal digits.<br /> The characters 0x will be added to the left of the converted result for *style* 1.|  
  
## Implicit conversions
Implicit conversions are those conversions that occur without specifying either the CAST or CONVERT function. Explicit conversions are those conversions that require the CAST or CONVERT function to be specified. The following illustration shows all explicit and implicit data type conversions that are allowed for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system-supplied data types. These include **xml**, **bigint**, and **sql_variant**. There is no implicit conversion on assignment from the **sql_variant** data type, but there is implicit conversion to **sql_variant**.
  
> [!TIP]  
>  This chart is available as a downloadable PDF file at the [Microsoft Download Center](http://www.microsoft.com/download/details.aspx?id=35834).  
  
![Data type conversion table](../../t-sql/data-types/media/lrdatahd.png "Data type conversion table")
  
When you convert between **datetimeoffset** and the character types **char**, **varchar**, **nchar**, and **nvarchar** the converted time zone offset part should always be double digits for both HH and MM for example, -08:00.
  
> [!NOTE]  
>  Because Unicode data always uses an even number of bytes, use caution when you convert **binary** or **varbinary** to or from Unicode supported data types. For example, the following conversion does not return a hexadecimal value of 41; it returns 4100: `SELECT CAST(CAST(0x41 AS nvarchar) AS varbinary)`.  
  
## Large-value data types
Large-value data types exhibit the same implicit and explicit conversion behavior as their smaller counterparts, specifically the **varchar**, **nvarchar** and **varbinary** data types. However, you should consider the following guidelines:
-   Conversion from **image** to **varbinary(max)** and vice-versa is an implicit conversion, and so are conversions between **text** and **varchar(max)**, and **ntext** and **nvarchar(max)**.  
-   Conversion from large-value data types, such as **varchar(max)**, to a smaller counterpart data type, such as **varchar**, is an implicit conversion, but truncation will occur if the large value is too big for the specified length of the smaller data type.  
-   Conversion from **varchar**, **nvarchar**, or **varbinary** to their corresponding large-value data types is performed implicitly.  
-   Conversion from the **sql_variant** data type to the large-value data types is an explicit conversion.  
-   Large-value data types cannot be converted to the **sql_variant** data type.  
  
For more information about how to convert from the **xml** data type, see [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md).
  
## xml data type
When you explicitly or implicitly cast the **xml** data type to a string or binary data type, the content of the **xml** data type is serialized based on a set of rules. For information about these rules, see [Define the Serialization of XML Data](../../relational-databases/xml/define-the-serialization-of-xml-data.md). For information about how to convert from other data types to the **xml** data type, see [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md).
  
## text and image data types
Automatic data type conversion is not supported for the **text** and **image** data types. You can explicitly convert **text** data to character data, and **image** data to **binary** or **varbinary**, but the maximum length is 8000 bytes. If you try an incorrect conversion such as trying to convert a character expression that includes letters to an **int**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message.
  
## Output Collation  
When the output of CAST or CONVERT is a character string, and the input is a character string, the output has the same collation and collation label as the input. If the input is not a character string, the output has the default collation of the database, and a collation label of coercible-default. For more information, see [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md).
  
To assign a different collation to the output, apply the COLLATE clause to the result expression of the CAST or CONVERT function. For example:
  
`SELECT CAST('abc' AS varchar(5)) COLLATE French_CS_AS`
  
## Truncating and rounding results
When you convert character or binary expressions (**char**, **nchar**, **nvarchar**, **varchar**, **binary**, or **varbinary**) to an expression of a different data type, data can be truncated, only partially displayed, or an error is returned because the result is too short to display. Conversions to **char**, **varchar**, **nchar**, **nvarchar**, **binary**, and **varbinary** are truncated, except for the conversions shown in the following table.
  
|From data type|To data type|Result|  
|---|---|---|
|**int**, **smallint**, or **tinyint**|**char**|*|  
||**varchar**|*|  
||**nchar**|E|  
||**nvarchar**|E|  
|**money**, **smallmoney**, **numeric**, **decimal**, **float**, or **real**|**char**|E|  
||**varchar**|E|  
||**nchar**|E|  
||**nvarchar**|E|  
  
\* = Result length too short to display. E = Error returned because result length is too short to display.
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] guarantees that only roundtrip conversions, conversions that convert a data type from its original data type and back again, will yield the same values from version to version. The following example shows such a roundtrip conversion:
  
```sql
DECLARE @myval decimal (5, 2);  
SET @myval = 193.57;  
SELECT CAST(CAST(@myval AS varbinary(20)) AS decimal(10,5));  
-- Or, using CONVERT  
SELECT CONVERT(decimal(10,5), CONVERT(varbinary(20), @myval));  
```  
  
> [!NOTE]  
>  Do not try to construct **binary** values and then convert them to a data type of the numeric data type category. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not guarantee that the result of a **decimal** or **numeric** data type conversion to **binary** will be the same between versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
The following example shows a resulting expression that is too small to display.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT p.FirstName, p.LastName, SUBSTRING(p.Title, 1, 25) AS Title,
    CAST(e.SickLeaveHours AS char(1)) AS [Sick Leave]  
FROM HumanResources.Employee e JOIN Person.Person p 
    ON e.BusinessEntityID = p.BusinessEntityID  
WHERE NOT e.BusinessEntityID >5;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```   
FirstName   LastName      Title   Sick Leave
---------   ------------- ------- --------`
Ken         Sanchez       NULL   *
Terri       Duffy         NULL   *
Roberto     Tamburello    NULL   *
Rob         Walters       NULL   *
Gail        Erickson      Ms.    *
(5 row(s) affected)  
```
  
When you convert data types that differ in decimal places, sometimes the result value is truncated and at other times it is rounded. The following table shows the behavior.
  
|From|To|Behavior|  
|---|---|---|
|**numeric**|**numeric**|Round|  
|**numeric**|**int**|Truncate|  
|**numeric**|**money**|Round|  
|**money**|**int**|Round|  
|**money**|**numeric**|Round|  
|**float**|**int**|Truncate|  
|**float**|**numeric**|Round<br /><br /> Conversion of **float** values that use scientific notation to **decimal** or **numeric** is restricted to values of precision 17 digits only. Any value with precision higher than 17 rounds to zero.|  
|**float**|**datetime**|Round|  
|**datetime**|**int**|Round|  
  
For example, the values 10.6496 and -10.6496 may be truncated or rounded during conversion to **int** or **numeric** types:
  
```sql
SELECT  CAST(10.6496 AS int) as trunc1,
         CAST(-10.6496 AS int) as trunc2,
         CAST(10.6496 AS numeric) as round1,
         CAST(-10.6496 AS numeric) as round2;
 ```
Results of the query are shown in the following table:
 
|trunc1|trunc2|round1|round2|
|---|---|---|---|
|10|-10|11|-11|
 
When you convert data types in which the target data type has fewer decimal places than the source data type, the value is rounded. For example, the result of the following conversion is `$10.3497`:
  
`SELECT CAST(10.3496847 AS money);`
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message when nonnumeric **char**, **nchar**, **varchar**, or **nvarchar** data is converted to **int**, **float**, **numeric**, or **decimal**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also returns an error when an empty string (" ") is converted to **numeric** or **decimal**.
  
## Certain datetime conversions are nondeterministic
The following table lists the styles for which the string-to-datetime conversion is nondeterministic.
  
|||  
|-|-|  
|All styles below 100<sup>1</sup>|106|  
|107|109|  
|113|130|  
  
<sup>1</sup> With the exception of styles 20 and 21
  
## Supplementary characters (surrogate pairs)
Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], if you use supplementary character (SC) collations, a CAST operation from **nchar** or **nvarchar** to an **nchar** or **nvarchar** type of smaller length will not truncate inside a surrogate pair; it truncates before the supplementary character. For example, the following code fragment leaves `@x` holding just `'ab'`. There is not enough space to hold the supplementary character.
  
```sql
DECLARE @x NVARCHAR(10) = 'ab' + NCHAR(0x10000);  
SELECT CAST (@x AS NVARCHAR(3));  
```  
  
When using SC collations the behavior of `CONVERT`, is analogous to that of `CAST`.
  
## Compatibility support
In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default style for CAST and CONVERT operations on **time** and **datetime2** data types is 121 except when either type is used in a computed column expression. For computed columns, the default style is 0. This behavior impacts computed columns when they are created, used in queries involving auto-parameterization, or used in constraint definitions.
  
Under compatibility level 110 and higher, the default style for CAST and CONVERT operations on **time** and **datetime2** data types is always 121. If your query relies on the old behavior, use a compatibility level less than 110, or explicitly specify the 0 style in the affected query.
  
Upgrading the database to compatibility level 110 and higher will not change user data that has been stored to disk. You must manually correct this data as appropriate. For example, if you used SELECT INTO to create a table from a source that contained a computed column expression described above, the data (using style 0) would be stored rather than the computed column definition itself. You would need to manually update this data to match style 121.
  
## <a name="BKMK_examples"></a> Examples  
  
### A. Using both CAST and CONVERT  
Each example retrieves the name of the product for those products that have a `3` in the first digit of their list price and converts their `ListPrice` to `int`.
  
```sql
-- Use CAST  
USE AdventureWorks2012;  
GO  
SELECT SUBSTRING(Name, 1, 30) AS ProductName, ListPrice  
FROM Production.Product  
WHERE CAST(ListPrice AS int) LIKE '3%';  
GO  
  
-- Use CONVERT.  
USE AdventureWorks2012;  
GO  
SELECT SUBSTRING(Name, 1, 30) AS ProductName, ListPrice  
FROM Production.Product  
WHERE CONVERT(int, ListPrice) LIKE '3%';  
GO  
```  
  
### B. Using CAST with arithmetic operators  
The following example calculates a single column computation (`Computed`) by dividing the total year-to-date sales (`SalesYTD`) by the commission percentage (`CommissionPCT`). This result is converted to an `int` data type after being rounded to the nearest whole number.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT CAST(ROUND(SalesYTD/CommissionPCT, 0) AS int) AS Computed  
FROM Sales.SalesPerson   
WHERE CommissionPCT != 0;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
```  
Computed
------
379753754
346698349
257144242
176493899
281101272
0  
301872549
212623750
298948202
250784119
239246890
101664220
124511336
97688107
(14 row(s) affected)  
```  
  
### C. Using CAST to concatenate  
The following example concatenates noncharacter, nonbinary expressions by using `CAST`.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT 'The list price is ' + CAST(ListPrice AS varchar(12)) AS ListPrice  
FROM Production.Product  
WHERE ListPrice BETWEEN 350.00 AND 400.00;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
ListPrice
------------------
The list price is 357.06
The list price is 364.09
The list price is 364.09
The list price is 364.09
The list price is 364.09
(5 row(s) affected)  
```
  
### D. Using CAST to produce more readable text  
The following example uses `CAST` in the select list to convert the `Name` column to a `char(10)` column.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT DISTINCT CAST(p.Name AS char(10)) AS Name, s.UnitPrice  
FROM Sales.SalesOrderDetail AS s   
JOIN Production.Product AS p   
    ON s.ProductID = p.ProductID  
WHERE Name LIKE 'Long-Sleeve Logo Jersey, M';  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Name       UnitPrice
---------- -----------
Long-Sleev 31.2437
Long-Sleev 32.4935
Long-Sleev 49.99
(3 row(s) affected)  
```
  
### E. Using CAST with the LIKE clause  
The following example converts the `money` column `SalesYTD` to an `int` and then to a `char(20)` column so that it can be used with the `LIKE` clause.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT p.FirstName, p.LastName, s.SalesYTD, s.BusinessEntityID  
FROM Person.Person AS p   
JOIN Sales.SalesPerson AS s   
    ON p.BusinessEntityID = s.BusinessEntityID  
WHERE CAST(CAST(s.SalesYTD AS int) AS char(20)) LIKE '2%';  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
FirstName        LastName            SalesYTD         SalesPersonID
---------------- ------------------- ---------------- -------------
Tsvi             Reiter              2811012.7151      279
Syed             Abbas               219088.8836       288
Rachel           Valdez              2241204.0424      289
(3 row(s) affected)  
```
  
### F. Using CONVERT or CAST with typed XML  
The following are several examples that show using CONVERT to convert to typed XML by using the [XML Data Type and Columns &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-type-and-columns-sql-server.md).
  
This example converts a string with white space, text and markup into typed XML and removes all insignificant white space (boundary white space between nodes):
  
```sql
CONVERT(XML, '<root><child/></root>')  
```  
  
This example converts a similar string with white space, text and markup into typed XML and preserves insignificant white space (boundary white space between nodes):
  
```sql
CONVERT(XML, '<root>          <child/>         </root>', 1)  
```  
  
This example casts a string with white space, text, and markup into typed XML:
  
```sql
CAST('<Name><FName>Carol</FName><LName>Elliot</LName></Name>'  AS XML)  
```  
  
For more examples, see [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md).
  
### G. Using CAST and CONVERT with datetime data  
The following example displays the current date and time, uses `CAST` to change the current date and time to a character data type, and then uses `CONVERT` display the date and time in the `ISO 8901` format.
  
```sql
SELECT   
   GETDATE() AS UnconvertedDateTime,  
   CAST(GETDATE() AS nvarchar(30)) AS UsingCast,  
   CONVERT(nvarchar(30), GETDATE(), 126) AS UsingConvertTo_ISO8601  ;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
UnconvertedDateTime     UsingCast              UsingConvertTo_ISO8601
----------------------- ---------------------- ------------------------------
2006-04-18 09:58:04.570 Apr 18 2006  9:58AM    2006-04-18T09:58:04.570
(1 row(s) affected)  
```
  
The following example is approximately the opposite of the previous example. The example displays a date and time as character data, uses `CAST` to change the character data to the `datetime` data type, and then uses `CONVERT` to change the character data to the `datetime` data type.
  
```sql
SELECT   
   '2006-04-25T15:50:59.997' AS UnconvertedText,  
   CAST('2006-04-25T15:50:59.997' AS datetime) AS UsingCast,  
   CONVERT(datetime, '2006-04-25T15:50:59.997', 126) AS UsingConvertFrom_ISO8601 ;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
UnconvertedText         UsingCast               UsingConvertFrom_ISO8601
----------------------- ----------------------- ------------------------
2006-04-25T15:50:59.997 2006-04-25 15:50:59.997 2006-04-25 15:50:59.997
(1 row(s) affected)  
```
  
### H. Using CONVERT with binary and character data  
The following examples show the results of converting binary and character data by using different styles.
  
```sql
--Convert the binary value 0x4E616d65 to a character value.  
SELECT CONVERT(char(8), 0x4E616d65, 0) AS [Style 0, binary to character];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 0, binary to character
----------------------------
Name  
(1 row(s) affected)  
```
 
The following example shows how Style 1 can force the result to be truncated. The truncation is caused by including the characters 0x in the result.  
```sql  
SELECT CONVERT(char(8), 0x4E616d65, 1) AS [Style 1, binary to character];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 1, binary to character
------------------------------
0x4E616D
(1 row(s) affected)  
```  
 
The following example shows that Style 2 does not truncate the result because the characters 0x are not included in the result.  
```sql  
SELECT CONVERT(char(8), 0x4E616d65, 2) AS [Style 2, binary to character];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 2, binary to character
------------------------------
4E616D65
(1 row(s) affected)  
```
  
Convert the character value 'Name' to a binary value.  
```sql
SELECT CONVERT(binary(8), 'Name', 0) AS [Style 0, character to binary];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 0, character to binary
----------------------------
0x4E616D6500000000
(1 row(s) affected)  
```
  
```sql
SELECT CONVERT(binary(4), '0x4E616D65', 1) AS [Style 1, character to binary];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 1, character to binary
---------------------------- 
0x4E616D65
(1 row(s) affected)  
```  

```sql
SELECT CONVERT(binary(4), '4E616D65', 2) AS [Style 2, character to binary];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Style 2, character to binary  
----------------------------------  
0x4E616D65
(1 row(s) affected)  
```  
  
### I. Converting date and time data types  
The following example demonstrates the conversion of date, time, and datetime data types.
  
```sql
DECLARE @d1 date, @t1 time, @dt1 datetime;  
SET @d1 = GETDATE();  
SET @t1 = GETDATE();  
SET @dt1 = GETDATE();  
SET @d1 = GETDATE();  
-- When converting date to datetime the minutes portion becomes zero.  
SELECT @d1 AS [date], CAST (@d1 AS datetime) AS [date as datetime];  
-- When converting time to datetime the date portion becomes zero   
-- which converts to January 1, 1900.  
SELECT @t1 AS [time], CAST (@t1 AS datetime) AS [time as datetime];  
-- When converting datetime to date or time non-applicable portion is dropped.  
SELECT @dt1 AS [datetime], CAST (@dt1 AS date) AS [datetime as date], 
   CAST (@dt1 AS time) AS [datetime as time];  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### J. Using CAST and CONVERT  
This example retrieves the name of the product for those products that have a `3` in the first digit of their list price and converts their `ListPrice` to **int**. Uses AdventureWorksDW.
  
```sql
SELECT EnglishProductName AS ProductName, ListPrice  
FROM dbo.DimProduct  
WHERE CAST(ListPrice AS int) LIKE '3%';  
```  
  
This example shows the same query, using CONVERT instead of CAST. Uses AdventureWorksDW.
  
```sql
SELECT EnglishProductName AS ProductName, ListPrice  
FROM dbo.DimProduct  
WHERE CONVERT(int, ListPrice) LIKE '3%';  
```  
  
### K. Using CAST with arithmetic operators  
The following example calculates a single column computation by dividing the product unit price (`UnitPrice`) by the discount percentage (`UnitPriceDiscountPct`). This result is converted to an `int` data type after being rounded to the nearest whole number. Uses AdventureWorksDW.
  
```sql
SELECT ProductKey, UnitPrice,UnitPriceDiscountPct,  
       CAST(ROUND (UnitPrice*UnitPriceDiscountPct,0) AS int) AS DiscountPrice  
FROM dbo.FactResellerSales  
WHERE SalesOrderNumber = 'SO47355'   
      AND UnitPriceDiscountPct > .02;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
ProductKey  UnitPrice  UnitPriceDiscountPct  DiscountPrice
----------  ---------  --------------------  -------------
323         430.6445   0.05                  22
213         18.5043    0.05                  1
456         37.4950    0.10                  4
456         37.4950    0.10                  4
216         18.5043    0.05                  1  
```  
  
### L. Using CAST to concatenate  
The following example concatenates noncharacter expressions by using CAST. Uses AdventureWorksDW.
  
```sql
SELECT 'The list price is ' + CAST(ListPrice AS varchar(12)) AS ListPrice  
FROM dbo.DimProduct  
WHERE ListPrice BETWEEN 350.00 AND 400.00;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
ListPrice
------------------------
The list price is 357.06
The list price is 364.09
The list price is 364.09
The list price is 364.09
The list price is 364.09  
```  
  
### M. Using CAST to produce more readable text  
The following example uses CAST in the SELECT list to convert the `Name` column to a **char(10)** column. Uses AdventureWorksDW.
  
```sql
SELECT DISTINCT CAST(EnglishProductName AS char(10)) AS Name, ListPrice  
FROM dbo.DimProduct  
WHERE EnglishProductName LIKE 'Long-Sleeve Logo Jersey, M';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
Name        UnitPrice
----------  ---------
Long-Sleev  31.2437
Long-Sleev  32.4935
Long-Sleev  49.99  
```  
  
### N. Using CAST with the LIKE clause  
The following example converts the **money** column `ListPrice` to an **int** type and then to a **char(20)** type so that it can be used with the LIKE clause. Uses AdventureWorksDW.
  
```sql
SELECT EnglishProductName AS Name, ListPrice  
FROM dbo.DimProduct  
WHERE CAST(CAST(ListPrice AS int) AS char(20)) LIKE '2%';  
```  
  
### O. Using CAST and CONVERT with datetime data  
The following example displays the current date and time, uses CAST to change the current date and time to a character data type, and then uses CONVERT display the date and time in the ISO 8601 format. Uses AdventureWorksDW.
  
```sql
SELECT TOP(1)  
   SYSDATETIME() AS UnconvertedDateTime,  
   CAST(SYSDATETIME() AS nvarchar(30)) AS UsingCast,  
   CONVERT(nvarchar(30), SYSDATETIME(), 126) AS UsingConvertTo_ISO8601  
FROM dbo.DimCustomer;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
UnconvertedDateTime         UsingCast                      UsingConvertTo_ISO8601
-----------------------     ------------------------------ ------------------------------
07/20/2010 1:44:31 PM       2010-07-20 13:44:31.5879025    2010-07-20T13:44:31.5879025  
```  
  
The following example is approximately the opposite of the previous example. The example displays a date and time as character data, uses CAST to change the character data to the **datetime** data type, and then uses CONVERT to change the character data to the **datetime** data type. Uses AdventureWorksDW.
  
```sql
SELECT TOP(1)   
   '2010-07-25T13:50:38.544' AS UnconvertedText,  
CAST('2010-07-25T13:50:38.544' AS datetime) AS UsingCast,  
   CONVERT(datetime, '2010-07-25T13:50:38.544', 126) AS UsingConvertFrom_ISO8601  
FROM dbo.DimCustomer;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```  
UnconvertedText         UsingCast               UsingConvertFrom_ISO8601
----------------------- ----------------------- ------------------------
2010-07-25T13:50:38.544 07/25/2010 1:50:38 PM   07/25/2010 1:50:38 PM  
```  
  
## See also
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)  
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
[Write International Transact-SQL Statements](../../relational-databases/collations/write-international-transact-sql-statements.md)
  
