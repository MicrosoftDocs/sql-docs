---
title: ISDATE (Transact-SQL)
description: "ISDATE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 03/14/2017
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ISDATETIME"
  - "ISDATE_TSQL"
  - "ISDATE"
  - "ISDATETIME_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "date and time [SQL Server], ISDATE"
  - "validate dates times [SQL Server]"
  - "formats [SQL Server], time"
  - "dates [SQL Server], validate"
  - "verify dates times [SQL Server]"
  - "functions [SQL Server], time"
  - "formats [SQL Server], dates"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "time [SQL Server], validate"
  - "ISDATE function [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# ISDATE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns 1 if the *expression* is a valid **datetime** value; otherwise, 0.  
  
 ISDATE returns 0 if the *expression* is a **datetime2** value.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md). Note that the range for datetime data is 1753-01-01 through 9999-12-31, while the range for date data is 0001-01-01 through 9999-12-31.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ISDATE ( expression )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is a character string or [expression](../../t-sql/language-elements/expressions-transact-sql.md) that can be converted to a character string. The expression must be less than 4,000 characters. Date and time data types, except datetime and smalldatetime, are not allowed as the argument for ISDATE.  
  
## Return Type  
 **int**  
  
## Remarks  
 ISDATE is deterministic only if you use it with the [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md) function, if the CONVERT style parameter is specified, and style is not equal to 0, 100, 9, or 109.  
  
 The return value of ISDATE depends on the settings set by [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md), [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) and [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md).  
  
## ISDATE expression Formats  
 For examples of valid formats for which ISDATE will return 1, see the section "Supported String Literal Formats for datetime" in the [datetime](../../t-sql/data-types/datetime-transact-sql.md) and [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) topics. For additional examples, also see the Input/Output column of the "Arguments" section of [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
 The following table summarizes input expression formats that are not valid and that return 0 or an error.  
  
|ISDATE expression|ISDATE return value|  
|-----------------------|-------------------------|  
|NULL|0|  
|Values of data types listed in [Data Types](../../t-sql/data-types/data-types-transact-sql.md) in any data type category other than character strings, Unicode character strings, or date and time.|0|  
|Values of **text**, **ntext**, or **image** data types.|0|  
|Any value that has a seconds precision scale greater than 3, (.0000 through .0000000...n). ISDATE will return 0 if the *expression* is a **datetime2** value, but will return 1 if the *expression* is a valid **datetime** value.|0|  
|Any value that mixes a valid date with an invalid value, for example 1995-10-1a.|0|  
  
## Examples  
  
### A. Using ISDATE to test for a valid datetime expression  
 The following example shows you how to use `ISDATE` to test whether a character string is a valid **datetime**.  
  
```sql  
IF ISDATE('2009-05-12 10:19:41.177') = 1  
    PRINT 'VALID'  
ELSE  
    PRINT 'INVALID';  
```  
  
### B. Showing the effects of the SET DATEFORMAT and SET LANGUAGE settings on return values  
 The following statements show the values that are returned as a result of the settings of `SET DATEFORMAT` and `SET LANGUAGE`.  
  
```sql  
/* Use these sessions settings. */  
SET LANGUAGE us_english;  
SET DATEFORMAT mdy;  
/* Expression in mdy dateformat */  
SELECT ISDATE('04/15/2008'); --Returns 1.  
/* Expression in mdy dateformat */  
SELECT ISDATE('04-15-2008'); --Returns 1.   
/* Expression in mdy dateformat */  
SELECT ISDATE('04.15.2008'); --Returns 1.   
/* Expression in myd  dateformat */  
SELECT ISDATE('04/2008/15'); --Returns 1.  
  
SET DATEFORMAT mdy;  
SELECT ISDATE('15/04/2008'); --Returns 0.  
SET DATEFORMAT mdy;  
SELECT ISDATE('15/2008/04'); --Returns 0.  
SET DATEFORMAT mdy;  
SELECT ISDATE('2008/15/04'); --Returns 0.  
SET DATEFORMAT mdy;  
SELECT ISDATE('2008/04/15'); --Returns 1.  
  
SET DATEFORMAT dmy;  
SELECT ISDATE('15/04/2008'); --Returns 1.  
SET DATEFORMAT dym;  
SELECT ISDATE('15/2008/04'); --Returns 1.  
SET DATEFORMAT ydm;  
SELECT ISDATE('2008/15/04'); --Returns 1.  
SET DATEFORMAT ymd;  
SELECT ISDATE('2008/04/15'); --Returns 1.  
  
SET LANGUAGE English;  
SELECT ISDATE('15/04/2008'); --Returns 0.  
SET LANGUAGE Hungarian;  
SELECT ISDATE('15/2008/04'); --Returns 0.  
SET LANGUAGE Swedish;  
SELECT ISDATE('2008/15/04'); --Returns 0.  
SET LANGUAGE Italian;  
SELECT ISDATE('2008/04/15'); --Returns 1.  
  
/* Return to these sessions settings. */  
SET LANGUAGE us_english;  
SET DATEFORMAT mdy;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using ISDATE to test for a valid datetime expression  
 The following example shows you how to use `ISDATE` to test whether a character string is a valid **datetime**.  
  
```sql  
IF ISDATE('2009-05-12 10:19:41.177') = 1  
    SELECT 'VALID';  
ELSE  
    SELECT 'INVALID';  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
