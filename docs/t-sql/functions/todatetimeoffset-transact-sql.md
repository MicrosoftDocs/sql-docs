---
title: "TODATETIMEOFFSET (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "TO_DATETIMEOFFSET_TSQL"
  - "SWITCH_TZ_TSQL"
  - "SWITCH_TZ"
  - "TO_DATETIMEOFFSET"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "date and time [SQL Server], TODATETIMEOFFSET"
  - "TODATETIMEOFFSET function"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
ms.assetid: b5fafc08-efd4-4a3b-a0b3-068981a0a685
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# TODATETIMEOFFSET (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a **datetimeoffset** value that is translated from a **datetime2** expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
TODATETIMEOFFSET ( expression , time_zone )  
```  
  
## Arguments  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) that resolves to a [datetime2](../../t-sql/data-types/datetime2-transact-sql.md) value.  
  
> [!NOTE]  
>  The expression cannot be of type **text**, **ntext**, or **image** because these types cannot be implicitly converted to **varchar** or **nvarchar**.  
  
 *time_zone*  
 Is an expression that represents the time zone offset in minutes (if an integer), for example -120, or hours and minutes (if a string), for example ‘+13.00’. The range is +14 to -14 (in hours). The expression is interpreted in local time for the specified time_zone.  
  
> [!NOTE]  
>  If expression is a character string, it must be in the format {+|-}TZH:THM.  
  
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
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D. Changing the time zone offset of the current date and time  
 The following example changes the zone offset of the current date and time to time zone `-07:00`.  
  
```  
DECLARE @todaysDateTime datetime2;  
SET @todaysDateTime = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDateTime, '-07:00');  
-- RETURNS 2007-08-30 15:51:34.7030000 -07:00  
```  
  
### E. Changing the time zone offset in minutes  
 The following example changes the current time zone to `-120` minutes.  
  
```  
DECLARE @todaysDate datetime2;  
SET @todaysDate = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDate, -120);  
-- RETURNS 2007-08-30 15:52:37.8770000 -02:00  
```  
  
### F. Adding a 13-hour time zone offset  
 The following example adds a 13-hour time zone offset to a date and time.  
  
```  
DECLARE @dateTime datetimeoffset(7)= '2007-08-28 18:00:30';  
SELECT TODATETIMEOFFSET (@dateTime, '+13:00');  
-- RETURNS 2007-08-28 18:00:30.0000000 +13:00  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)   
 [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)  
  
  

