---
title: "DATEDIFF_BIG (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "12/01/2015"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATEDIFF_BIG"
  - "DATEDIFF_BIG_TSQL"
helpviewer_keywords: 
  - "DATEDIFF_BIG function [SQL Server]"
  - "dates [SQL Server]. functions"
  - "date and time [SQL Server], DATEDIFF_BIG"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
ms.assetid: 19ac1693-3cfa-400d-bf83-20a9cb46599a
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATEDIFF_BIG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Returns the count (signed big integer) of the specified *datepart* boundaries crossed between the specified *startdate* and *enddate*.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DATEDIFF_BIG ( datepart , startdate , enddate )  
```  
  
## Arguments  
 *datepart*  
 Is the part of *startdate* and *enddate* that specifies the type of boundary crossed. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.  
  
|*datepart*|Abbreviations|  
|----------------|-------------------|  
|**year**|**yy, yyyy**|  
|**quarter**|**qq, q**|  
|**month**|**mm, m**|  
|**dayofyear**|**dy, y**|  
|**day**|**dd, d**|  
|**week**|**wk, ww**|  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss, s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  
  
 *startdate*  
 Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, user-defined variable or string literal. *startdate* is subtracted from *enddate*.  
  
 To avoid ambiguity, use four-digit years. For information about two digits years, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).  
  
 *enddate*  
 See *startdate*.  
  
## Return Type  
 Signed   
        **bigint**  
  
## Return Value  
 Returns count (signed bigint) of the specified datepart boundaries crossed between the specified startdate and enddate.  
  
-   Each *datepart* and its abbreviations return the same value.  
  
 If the return value is out of range for **bigint** (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807), an error is returned. For **millisecond**, the maximum difference between *startdate* and *enddate* is 24 days, 20 hours, 31 minutes and 23.647 seconds. For **second**, the maximum difference is 68 years.  
  
 If *startdate* and *enddate* are both assigned only a time value and the *datepart* is not a time *datepart*, 0 is returned.  
  
 A time zone offset component of *startdate* or *endate* is not used in calculating the return value.  
  
 Because [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) is accurate only to the minute, when a **smalldatetime** value is used for *startdate* or *enddate*, seconds and milliseconds are always set to 0 in the return value.  
  
 If only a time value is assigned to a variable of a date data type, the value of the missing date part is set to the default value: 1900-01-01. If only a date value is assigned to a variable of a time or date data type, the value of the missing time part is set to the default value: 00:00:00. If either *startdate* or *enddate* have only a time part and the other only a date part, the missing time and date parts are set to the default values.  
  
 If *startdate* and *enddate* are of different date data types and one has more time parts or fractional seconds precision than the other, the missing parts of the other are set to 0.  
  
## datepart Boundaries  
 The following statements have the same *startdate* and the same *endate*. Those dates are adjacent and differ in time by .0000001 second. The difference between the *startdate* and *endate* in each statement crosses one calendar or time boundary of its *datepart*. Each statement returns 1. If different years are used for this example and if both *startdate* and *endate* are in the same calendar week, the return value for **week** would be 0.  
  
 `SELECT DATEDIFF_BIG(year, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(quarter, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(month, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(dayofyear, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(day, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(week, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(hour, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(minute, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(second, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
 `SELECT DATEDIFF_BIG(millisecond, '2005-12-31 23:59:59.9999999'`  
  
 `, '2006-01-01 00:00:00.0000000');`  
  
## Remarks  
 DATEDIFF_BIG can be used in the select list, WHERE, HAVING, GROUP BY and ORDER BY clauses.  
  
 DATEDIFF_BIG implicitly casts string literals as a **datetime2** type. This means that DATEDIFF_BIG does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.  
  
 Specifying SET DATEFIRST has no effect on DATEDIFF_BIG. DATEDIFF_BIG always uses Sunday as the first day of the week to ensure the function is deterministic.  
  
## Examples  
 The following examples use different types of expressions as arguments for the *startdate* and *enddate* parameters.  
  
### Specifying columns for startdate and enddate  
 The following example calculates the number of day boundaries that are crossed between dates in two columns in a table.  
  
```  
CREATE TABLE dbo.Duration  
    (  
    startDate datetime2  
    ,endDate datetime2  
    );  
INSERT INTO dbo.Duration(startDate,endDate)  
    VALUES('2007-05-06 12:10:09','2007-05-07 12:10:09');  
SELECT DATEDIFF_BIG(day,startDate,endDate) AS 'Duration'  
FROM dbo.Duration;  
-- Returns: 1  
```  
  
 For many additional examples, see the closely related examples in [DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md).  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md)  
  
  
