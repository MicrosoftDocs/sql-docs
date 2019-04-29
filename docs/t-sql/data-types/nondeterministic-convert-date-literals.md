---
title: "Nondeterministic conversion of date literals | Microsoft Docs"
ms.custom: ""
ms.date: "11/19/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: "genemi"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Nondeterministic conversion of literal date strings into DATE values

Use caution when allowing conversion of your CHARACTER strings into DATE data types. The reason is that such conversions are often _nondeterministic_.

You control these nondeterministic conversions by accounting for the settings of [SET LANGUAGE](../statements/set-language-transact-sql.md) and [SET DATEFORMAT](../statements/set-dateformat-transact-sql.md).



## SET LANGUAGE example: Month name in Polish

- `SET LANGUAGE Polish;`

A character string can be the name of a month. But is the name in English, or Polish, or Croatian, or in another language? And, will the user's session be set to the correct corresponding LANGUAGE?

For example, consider the word _listopad_, which is the name of a month. But which month it is depends on the language the SQL system believes is being used:
- If Polish, then _listopad_ translates to month 11 (_November_ in English).
- If Croatian, then _listopad_ translates to month 10 (_October_ in English).

#### Code example of SET LANGUAGE

```sql
--SELECT alias FROM sys.syslanguages ORDER BY alias;

DECLARE @yourInputDate  NVARCHAR(32) = '28 listopad 2018';

SET LANGUAGE Polish;
SELECT CONVERT(DATE, @yourInputDate) AS [SL_Polish];

SET LANGUAGE Croatian;
SELECT CONVERT(DATE, @yourInputDate) AS [SL_Croatian];

SET LANGUAGE English;


/***  Actual output:  For the two months, note the 11 versus the 10.
SL_Polish
2018-11-28

SL_Croatian
2018-10-28
***/
```



## SET DATEFORMAT example

- `SET DATEFORMAT dmy;`

The preceding **dmy** format says that an example date string of '01-03-2018' would be interpreted to mean _the first day of March in the year 2018_.

If instead **mdy** was specified, then the same '01-03-2018' string would mean _the third day of January in 2018_.

And if **ymd** was specified, there is no guarantee of what the output would be. The numeric value of '2018' is too large to be a day.
<!--
The preceding claim of "no guarantee" might be incorrect, in the minds of the SQL query engine Developer team?
-->

#### Specific countries

In Japan and China, the DATEFORMAT of **ymd** is used. The format's parts are in a sensible sequence of largest unit to smallest. Consequently, this format sorts well. This format is considered to be the _international_ format. It is international because the four digits of the year are unambiguous, and no country on Earth uses the archaic format of **ydm**.

In other countries such as Germany and France, the DATEFORMAT is **dmy**, meaning **'dd-mm-yyyy'**. The **dmy** format does not sort well, but it is a sensible sequence of smallest unit to largest.

The United States, and the Federated States of Micronesia, are the only countries that use **mdy**, which does not sort. The format's mixed sequence matches a pattern of verbal speech in spoken dates.

#### Code example of SET DATEFORMAT: *mdy* versus *dmy*

The following Transact-SQL code example uses the same date character string with three different DATEFORMAT settings. A run of the code produces the output shown in the comment:

```sql
DECLARE @yourDateString NVARCHAR(10) = '12-09-2018';
PRINT @yourDateString + '  = the input.';

SET DATEFORMAT dmy;
SELECT CONVERT(DATE, @yourDateString) AS [DMY-Interpretation-of-input-format];

SET DATEFORMAT mdy;
SELECT CONVERT(DATE, @yourDateString) AS [MDY-Interpretation-of-input-format];

SET DATEFORMAT ymd;
SELECT CONVERT(DATE, @yourDateString) AS [YMD-Interpretation--?--NotGuaranteed];


/***  Actual output:
12-09-2018  = the input.

DMY-Interpretation-of-input-format
2018-09-12

MDY-Interpretation-of-input-format
2018-12-09

YMD-Interpretation--?--NotGuaranteed
2018-12-09
***/
```

In the preceding code example, the final example has a mismatch between format **ymd** versus the input string. The third node of the input string represents a numeric value that is too large to be a day. Microsoft does not guarantee the output value from such mismatches.

#### CONVERT offers explicit codes for _deterministic_ control of date formats

Our CAST and CONVERT documentation article lists explicit codes that can you can use with the CONVERT function to _deterministically_ control date conversions. Every month the article has one of our highest pageview counts.

- [CAST and CONVERT (Transact-SQL): Date and time styles](../functions/cast-and-convert-transact-sql.md#date-and-time-styles)
- [CAST and CONVERT (Transact-SQL): Certain datetime conversions are nondeterministic](../functions/cast-and-convert-transact-sql.md#certain-datetime-conversions-are-nondeterministic)



## Compatibility level 90 and above

In SQL Server 2000, the compatibility level was 80. For level settings of 80 or below, implicit date conversions were deterministic.

Starting with SQL Server 2005 and its compatibility level of 90, implicit date conversions became nondeterministic. Date conversions became dependent on SET LANGUAGE and SET DATEFORMAT starting with level 90.

#### Unicode

<!-- The next live sentence needs an explanatory example!  N'somethingHere?'.
-->
Conversion of non-Unicode character data between collations is also considered nondeterministic.



## See also

- [Set a Session Language](../../relational-databases/collations/set-a-session-language.md)
- [Date and Time Data Types and Functions (Transact-SQL)](../functions/date-and-time-data-types-and-functions-transact-sql.md)
- [FORMAT (Transact-SQL)](../functions/format-transact-sql.md)
- [ISDATE (Transact-SQL)](../functions/isdate-transact-sql.md)



<!--
This new article is linked-to by the following articles (at least initially on 2018/11/19).....
...
* docs/relational-databases/views/create-indexed-views.md
* docs/relational-databases/indexes/indexes-on-computed-columns.md
* docs/t-sql/functions/cast-and-convert-transact-sql.md
...
As a reaction to public PR 1279, this approach of creating a new article to link to is a better alternative than a docs/includes/ approach.
GeneMi (MightyPen), 2018/11/19
-->

