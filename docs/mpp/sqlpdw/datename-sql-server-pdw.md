---
title: "DATENAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 646454b3-f899-48cf-aba4-9d39b2d952c4
caps.latest.revision: 5
author: BarbKess
---
# DATENAME (SQL Server PDW)
Returns a character string that represents the specified *datepart* of the specified *date*  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATENAME (datepart ,date )  
```  
  
## Arguments  
*datepart*  
Is the part of the *date* to return. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.  
  
|*datepart*|Abbreviations|  
|--------------|-----------------|  
|**year**|**yy, yyyy**|  
|**quarter**|**qq, q**|  
|**month**|**mm, m**|  
|**dayofyear**|**dy, y**|  
|**day**|**dd, d**|  
|**week**|**wk, ww**|  
|**weekday**|**dw, w**|  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss, s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  
|**TZoffset**|**tz**|  
|**ISO_WEEK**|**ISOWK, ISOWW**|  
  
*date*  
Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, user-defined variable, or string literal.  
  
To avoid ambiguity, use four-digit years. For information about two-digit years, see [Configure the two digit year cutoff Server Configuration Option](http://technet.microsoft.com/en-us/library/ms191004.aspx).  
  
## Return Type  
**nvarchar**  
  
## Return Value  
  
-   Each *datepart* and its abbreviations return the same value.  
  
The return value depends on the language environment which cannot be changed. The return value is dependent on [SET DATEFORMAT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/set-dateformat-sql-server-pdw.md) if *date* is a string literal of some formats. SET DATEFORMAT does not affect the return value when the date is a column expression of a date or time data type.  
  
When the *date* parameter has a **date** data type argument, the return value depends on the setting specified by using [SET DATEFIRST &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/set-datefirst-sql-server-pdw.md).  
  
## TZoffset datepart Argument  
If *datepart* argument is **TZoffset** (**tz**) and the *date* argument has no time zone offset, 0 is returned.  
  
## smalldatetime date Argument  
When *date* is smalldatetime, seconds are returned as 00.  
  
## Default Returned for a datepart That Is Not in the date Argument  
If the data type of the *date* argument does not have the specified *datepart*, the default for that *datepart* will be returned only when a literal is specified for *date*.  
  
For example, the default year-month-day for any **date** data type is 1900-01-01. The following statement has date part arguments for *datepart*, a time argument for *date*, and returns `1900, January, 1, 1, Monday`.  
  
```  
SELECT DATENAME(year, '12:10:30.123')  
    ,DATENAME(month, '12:10:30.123')  
    ,DATENAME(day, '12:10:30.123')  
    ,DATENAME(dayofyear, '12:10:30.123')  
    ,DATENAME(weekday, '12:10:30.123');  
```  
  
If *date* is specified as a variable or table column and the data type for that variable or column does not have the specified *datepart*, error 9810 is returned. The following code example fails because the date part year is not a valid for the **time** data type that is declared for the variable *@t*.  
  
```  
DECLARE @t time = '12:10:30.123';   
SELECT DATENAME(year, @t);  
```  
  
## Remarks  
DATENAME can be used in the select list, WHERE, HAVING, GROUP BY, and ORDER BY clauses.  
  
In SQL Server 2014, DATENAME implicitly casts string literals as a **datetime2** type. This means that DATENAME does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.  
  
## Examples  
The following example returns the date parts for the specified date.  
  
```  
SELECT DATENAME(datepart,'2007-10-30 12:15:32.1234567 +05:10');  
```  
  
Here is the result set.  
  
|*datepart*|Return value|  
|--------------|----------------|  
|**year, yyyy, yy**|2007|  
|**quarter, qq, q**|4|  
|**month, mm, m**|October|  
|**dayofyear, dy, y**|303|  
|**day, dd, d**|30|  
|**week, wk, ww**|44|  
|**weekday, dw**|Tuesday|  
|**hour, hh**|12|  
|**minute, n**|15|  
|**second, ss, s**|32|  
|**millisecond, ms**|123|  
|**microsecond, mcs**|123456|  
|**nanosecond, ns**|123456700|  
|**TZoffset, tz**|310|  
|**ISO_WEEK, ISOWK, ISOWW**|44|  
  
## See Also  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
