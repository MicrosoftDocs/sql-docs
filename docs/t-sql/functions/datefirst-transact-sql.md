---
title: "@@DATEFIRST (Transact-SQL)"
description: "@@DATEFIRST (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATE_FORMAT_TSQL"
  - "DATE FORMAT"
  - "@@DATEFIRST_TSQL"
  - "@@DATEFIRST"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "date and time [SQL Server], SET DATEFIRST"
  - "first day of week [SQL Server]"
  - "dates [SQL Server], first day of week"
  - "day of week [SQL Server]"
  - "SET DATEFIRST option [SQL Server]"
  - "date and time [SQL Server], DATEFIRST"
  - "DATEFIRST option [SQL Server]"
  - "date and time [SQL Server], @@DATEFIRST"
  - "weekdays [SQL Server]"
  - "@@DATEFIRST function [SQL Server]"
  - "functions [SQL Server], date and time"
  - "options [SQL Server], date"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# &#x40;&#x40;DATEFIRST (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the current value of [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md), for a specific session.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
@@DATEFIRST  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Type  
**tinyint**
  
## Remarks  
SET DATEFIRST *n* specifies the first day (SUNDAY, MONDAY, TUESDAY, etc.) of the week. The value of *n* ranges from 1 to 7.

```sql
SET DATEFIRST 3;
GO  
SELECT @@DATEFIRST; -- 3 (Wednesday)
GO
```  

For a U.S. English environment, @@DATEFIRST defaults to 7 (Sunday).
  
This language setting impacts character string interpretation as SQL Server converts those strings to date values for database storage. This setting also impacts display of date values stored in the database. This setting does not impact the storage format of date data.

This example first sets the language to `Italian`. The statement `SELECT @@DATEFIRST;` returns `1`. The next statement sets the language to  is then set to `us_english`. The final statement, `SELECT @@DATEFIRST;` returns `7`.
  
```sql
SET LANGUAGE Italian;  
GO  
SELECT @@DATEFIRST;  
GO  
SET LANGUAGE us_english;  
GO  
SELECT @@DATEFIRST;  
```  
  
## Examples  
This example sets the first day of the week to `5` (Friday), and assumes that the current day, `Today`, falls on Saturday. The `SELECT` statement returns the `DATEFIRST` value and the number of the current day of the week.
  
```sql
SET DATEFIRST 5;  
SELECT @@DATEFIRST AS 'First Day'  
    ,DATEPART(dw, SYSDATETIME()) AS 'Today';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
First Day         Today  
----------------  --------------  
5                 2  
```  
  
## Example
 [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```sql
SELECT @@DATEFIRST;  
```  
  
## See also
[Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)
  
  

