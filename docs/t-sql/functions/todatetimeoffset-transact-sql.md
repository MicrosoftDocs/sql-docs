---
title: "TODATETIMEOFFSET (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TODATETIMEOFFSET (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a **datetimeoffset** value that is translated from a **datetime2** expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
TODATETIMEOFFSET ( expression , time_zone )  
```  
  
## Arguments  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) that resolves to a [datetime2](../../t-sql/data-types/datetime2-transact-sql.md) value.  
  
> [!NOTE]  
>  The expression cannot be of type **text**, **ntext**, or **image** because these types cannot be implicitly converted to **varchar** or **nvarchar**.  
  
 *time_zone*  
 Is an expression that represents the time zone offset in minutes (if an integer), for example -120, or hours and minutes (if a string), for example '+13:00'. The range is +14 to -14 (in hours). The expression is interpreted in local time for the specified time_zone.  
  
> [!NOTE]  
>  If expression is a character string, it must be in the format {+|-}TZH:THM.  
  
## Return Type  
 **datetimeoffset**. The fractional precision is the same as the *datetime* argument.  
  
## Examples  
  
### A. Changing the time zone offset of the current date and time  
 The following example changes the zone offset of the current date and time to time zone `-07:00`.  
  
```sql  
DECLARE @todaysDateTime datetime2;  
SET @todaysDateTime = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDateTime, '-07:00');  
-- RETURNS 2007-08-30 15:51:34.7030000 -07:00  
```  
  
### B. Changing the time zone offset in minutes  
 The following example changes the current time zone to `-120` minutes.  
  
```sql  
SELECT TODATETIMEOFFSET(SYSDATETIME(), -120)
-- RETURNS: 2019-04-22 11:39:21.6986813 -02:00  
```  
  
### C. Adding a 13-hour time zone offset  
 The following example adds a 13-hour time zone offset to a date and time.  
  
```sql  
SELECT TODATETIMEOFFSET(SYSDATETIME(), '+13:00')
-- RETURNS: 2019-04-22 11:39:29.0339301 +13:00
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)   
 [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)  
  
  

