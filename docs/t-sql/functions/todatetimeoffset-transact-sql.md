---
title: "TODATETIMEOFFSET (Transact-SQL)"
description: "TODATETIMEOFFSET (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "04/22/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "TO_DATETIMEOFFSET_TSQL"
  - "SWITCH_TZ_TSQL"
  - "SWITCH_TZ"
  - "TO_DATETIMEOFFSET"
helpviewer_keywords:
  - "date and time [SQL Server], TODATETIMEOFFSET"
  - "TODATETIMEOFFSET function"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# TODATETIMEOFFSET (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a **datetimeoffset** value that is translated from a **datetime2** expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
TODATETIMEOFFSET ( datetime_expression , timezoneoffset_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *datetime_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) that resolves to a [datetime2](../../t-sql/data-types/datetime2-transact-sql.md) value.  
  
> [!NOTE]  
>  The expression cannot be of type **text**, **ntext**, or **image** because these types cannot be implicitly converted to **varchar** or **nvarchar**.  
  
 *timezoneoffset_expression*  
 Is an expression that represents the time zone offset in minutes (if an integer), for example -120, or hours and minutes (if a string), for example '+13:00'. The range is +14 to -14 (in hours). The expression is interpreted in local time for the specified timezoneoffset_expression.  

  
> [!NOTE]  
>  If expression is a character string, it must be in the format {+|-}TZH:THM.  
  
## Return Type  
 **datetimeoffset**. The fractional precision is the same as the *datetime_expression* argument.  
  
## Examples  
  
### A. Changing the time zone offset of the current date and time  
 The following example changes the zone offset of the current date and time to time zone `-07:00`.  
  
```sql  
DECLARE @todaysDateTime DATETIME2;  
SET @todaysDateTime = GETDATE();  
SELECT TODATETIMEOFFSET (@todaysDateTime, '-07:00');  
-- RETURNS 2019-04-22 16:23:51.7666667 -07:00  
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
  
  

