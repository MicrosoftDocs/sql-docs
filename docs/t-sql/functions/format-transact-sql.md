---
title: "FORMAT (Transact-SQL)"
description: "Transact-SQL reference for the FORMAT function."
author: markingmyname
ms.author: maghan
ms.date: "08/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FORMAT_TSQL"
  - "FORMAT"
helpviewer_keywords:
  - "FORMAT function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azure-sqldw-latest"
---
# FORMAT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns a value formatted with the specified format and optional culture. Use the FORMAT function for locale-aware formatting of date/time and number values as strings. For general data type conversions, use CAST or CONVERT.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
FORMAT( value, format [, culture ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

 *value*  
 Expression of a supported data type to format. For a list of valid types, see the table in the following Remarks section.  
  
 *format*  
 **nvarchar** format pattern.  
  
 The *format* argument must contain a valid .NET Framework format string, either as a standard format string (for example, "C" or "D"), or as a pattern of custom characters for dates and numeric values (for example, "MMMM DD, yyyy (dddd)"). Composite formatting is not supported. For a full explanation of these formatting patterns, consult the .NET Framework documentation on string formatting in general, custom date and time formats, and custom number formats. A good starting point is the topic, "[Formatting Types](/dotnet/standard/base-types/formatting-types)."  
  
 *culture*  
 Optional **nvarchar** argument specifying a culture.  
  
 If the *culture* argument is not provided, the language of the current session is used. This language is set either implicitly, or explicitly by using the SET LANGUAGE statement. *culture* accepts any culture supported by the .NET Framework as an argument; it is not limited to the languages explicitly supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the *culture* argument is not valid, FORMAT raises an error.  
  
## Return Types

 **nvarchar** or null  
  
 The length of the return value is determined by the *format*.  
  
## Remarks

 FORMAT returns NULL for errors other than a *culture* that is not *valid*. For example, NULL is returned if the value specified in *format* is not valid.  

 The FORMAT function is nondeterministic.
  
 FORMAT relies on the presence of the .NET Framework Common Language Runtime (CLR).  
  
 This function cannot be remoted since it depends on the presence of the CLR. Remoting a function that requires the CLR, could cause an error on the remote server.  
  
 FORMAT relies upon CLR formatting rules, which dictate that colons and periods must be escaped. Therefore, when the format string (second parameter) contains a colon or period, the colon or period must be escaped with backslash when an input value (first parameter) is of the **time** data type. See [D. FORMAT with time data types](#ExampleD).  
  
 The following table lists the acceptable data types for the *value* argument together with their .NET Framework mapping equivalent types.  
  
|Category|Type|.NET type|  
|--------------|----------|---------------|  
|Numeric|bigint|Int64|  
|Numeric|int|Int32|  
|Numeric|smallint|Int16|  
|Numeric|tinyint|Byte|  
|Numeric|decimal|SqlDecimal|  
|Numeric|numeric|SqlDecimal|  
|Numeric|float|Double|  
|Numeric|real|Single|  
|Numeric|smallmoney|Decimal|  
|Numeric|money|Decimal|  
|Date and Time|date|DateTime|  
|Date and Time|time|TimeSpan|  
|Date and Time|datetime|DateTime|  
|Date and Time|smalldatetime|DateTime|  
|Date and Time|datetime2|DateTime|  
|Date and Time|datetimeoffset|DateTimeOffset|  
  
## Examples  
  
### A. Simple FORMAT example

 The following example returns a simple date formatted for different cultures.  
  
```sql  
DECLARE @d DATE = '11/22/2020';
SELECT FORMAT( @d, 'd', 'en-US' ) 'US English'  
      ,FORMAT( @d, 'd', 'en-gb' ) 'British English'  
      ,FORMAT( @d, 'd', 'de-de' ) 'German'  
      ,FORMAT( @d, 'd', 'zh-cn' ) 'Chinese Simplified (PRC)';  
  
SELECT FORMAT( @d, 'D', 'en-US' ) 'US English'  
      ,FORMAT( @d, 'D', 'en-gb' ) 'British English'  
      ,FORMAT( @d, 'D', 'de-de' ) 'German'  
      ,FORMAT( @d, 'D', 'zh-cn' ) 'Chinese Simplified (PRC)';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
US English  British English       German     Simplified Chinese (PRC)  
----------  --------------------- ---------- ------------------------  
11/22/2020  22/11/2020            22.11.2020 2020/11/22 
  
US English                  British English        German                      Chinese (Simplified PRC)  
--------------------------- ---------------------- --------------------------  ---------------------------------------  
Sunday, November 22, 2020   22 November 2020       Sonntag, 22. November 2020  2020年11月22日  
  
```  
  
### B. FORMAT with custom formatting strings

 The following example shows formatting numeric values by specifying a custom format. The example assumes that the current date is September 27, 2012. For more information about these and other custom formats, see [Custom Numeric Format Strings](/dotnet/standard/base-types/custom-numeric-format-strings).  
  
```sql  
DECLARE @d DATE = GETDATE();  
SELECT FORMAT( @d, 'dd/MM/yyyy', 'en-US' ) AS 'Date'  
       ,FORMAT(123456789,'###-##-####') AS 'Custom Number';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Date        Custom Number  
----------  -------------  
22/11/2020  123-45-6789  
  
```  
  
### C. FORMAT with numeric types

 The following example returns 5 rows from the **Sales.CurrencyRate** table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The column **EndOfDateRate** is stored as type **money** in the table. In this example, the column is returned unformatted and then formatted by specifying the .NET Number format, General format, and Currency format types. For more information about these and other numeric formats, see [Standard Numeric Format Strings](/dotnet/standard/base-types/standard-numeric-format-strings).  
  
```sql  
SELECT TOP(5) CurrencyRateID, EndOfDayRate  
            ,FORMAT(EndOfDayRate, 'N', 'en-us') AS 'Numeric Format'  
            ,FORMAT(EndOfDayRate, 'G', 'en-us') AS 'General Format'  
            ,FORMAT(EndOfDayRate, 'C', 'en-us') AS 'Currency Format'  
FROM Sales.CurrencyRate  
ORDER BY CurrencyRateID;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
CurrencyRateID EndOfDayRate  Numeric Format  General Format  Currency Format  
-------------- ------------  --------------  --------------  ---------------  
1              1.0002        1.00            1.0002          $1.00  
2              1.55          1.55            1.5500          $1.55  
3              1.9419        1.94            1.9419          $1.94  
4              1.4683        1.47            1.4683          $1.47  
5              8.2784        8.28            8.2784          $8.28  
  
```  
  
 This example specifies the German culture (de-de).  
  
```sql  
SELECT TOP(5) CurrencyRateID, EndOfDayRate  
      ,FORMAT(EndOfDayRate, 'N', 'de-de') AS 'Numeric Format'  
      ,FORMAT(EndOfDayRate, 'G', 'de-de') AS 'General Format'  
      ,FORMAT(EndOfDayRate, 'C', 'de-de') AS 'Currency Format'  
FROM Sales.CurrencyRate  
ORDER BY CurrencyRateID;  
```  
  
```
CurrencyRateID EndOfDayRate  Numeric Format  General Format  Currency Format  
-------------- ------------  --------------  --------------  ---------------  
1              1.0002        1,00            1,0002          1,00 &euro;  
2              1.55          1,55            1,5500          1,55 &euro;  
3              1.9419        1,94            1,9419          1,94 &euro;  
4              1.4683        1,47            1,4683          1,47 &euro;  
5              8.2784        8,28            8,2784          8,28 &euro;  
  
```  
  
### <a name="ExampleD"></a> D. FORMAT with time data types

 FORMAT returns NULL in these cases because `.` and `:` are not escaped.  
  
```sql  
SELECT FORMAT(cast('07:35' as time), N'hh.mm');   --> returns NULL  
SELECT FORMAT(cast('07:35' as time), N'hh:mm');   --> returns NULL  
```  
  
 Format returns a formatted string because the `.` and `:` are escaped.  
  
```sql  
SELECT FORMAT(cast('07:35' as time), N'hh\.mm');  --> returns 07.35  
SELECT FORMAT(cast('07:35' as time), N'hh\:mm');  --> returns 07:35  
```  

Format returns a formatted current time with AM or PM specified

```sql
SELECT FORMAT(SYSDATETIME(), N'hh:mm tt'); -- returns 03:46 PM
SELECT FORMAT(SYSDATETIME(), N'hh:mm t'); -- returns 03:46 P
```

Format returns the specified time, displaying AM

```sql
select FORMAT(CAST('2018-01-01 01:00' AS datetime2), N'hh:mm tt') -- returns 01:00 AM
select FORMAT(CAST('2018-01-01 01:00' AS datetime2), N'hh:mm t')  -- returns 01:00 A
```

Format returns the specified time, displaying PM

```sql
select FORMAT(CAST('2018-01-01 14:00' AS datetime2), N'hh:mm tt') -- returns 02:00 PM
select FORMAT(CAST('2018-01-01 14:00' AS datetime2), N'hh:mm t') -- returns 02:00 P
```
  
Format returns the specified time in 24h format

```sql
select FORMAT(CAST('2018-01-01 14:00' AS datetime2), N'HH:mm') -- returns 14:00
```
  
## See Also

- [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
- [STR &#40;Transact-SQL&#41;](../../t-sql/functions/str-transact-sql.md)  
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
