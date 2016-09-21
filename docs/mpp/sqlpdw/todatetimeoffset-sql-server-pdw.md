---
title: "TODATETIMEOFFSET (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1ee8202c-7579-49a8-b3a4-7d88ff9ab9e6
caps.latest.revision: 5
author: BarbKess
---
# TODATETIMEOFFSET (SQL Server PDW)
Returns a **datetimeoffset** value that is translated from a **datetime2** expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TODATETIMEOFFSET (expression,time_zone)  
```  
  
## Arguments  
*expression*  
Is an expression that resolves to a datetime2 value.  
  
> [!NOTE]  
> The expression cannot be of type **text**, **ntext**, or **image** because these types cannot be implicitly converted to **varchar** or **nvarchar**.  
  
*time_zone*  
Is an expression that represents the time zone offset in minutes (if an integer), for example -120, or hours and minutes (if a string), for example ‘+13.00’. The range is +14 to -14 (in hours). The expression is interpreted in local time for the specified time_zone.  
  
> [!NOTE]  
> If expression is a character string, it must be in the format {+|-}TZH:THM.  
  
## Return Type  
**datetimeoffset**. The fractional precision is the same as the *datetime* argument.  
  
## Examples  
  
### A. Changing the time zone offset of the current date and time  
The following example changes the zone offset of the current date and time to time zone `-07:00`.  
  
```  
DECLARE @todaysDateTime datetime2;  
SET @todaysDateTime = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDateTime, '-07:00');  
-- RETURNS 2007-08-30 15:51:34.7030000 -07:00  
```  
  
### B. Changing the time zone offset in minutes  
The following example changes the current time zone to `-120` minutes.  
  
```  
DECLARE @todaysDate datetime2;  
SET @todaysDate = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDate, -120);  
-- RETURNS 2007-08-30 15:52:37.8770000 -02:00  
```  
  
### C. Adding a 13-hour time zone offset  
The following example adds a 13-hour time zone offset to a date and time.  
  
```  
DECLARE @dateTime datetimeoffset(7)= '2007-08-28 18:00:30';  
SELECT TODATETIMEOFFSET (@dateTime, '+13:00');  
-- RETURNS 2007-08-28 18:00:30.0000000 +13:00  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
