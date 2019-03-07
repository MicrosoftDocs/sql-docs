---
title: "@@DATEFIRST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATE_FORMAT_TSQL"
  - "DATE FORMAT"
  - "@@DATEFIRST_TSQL"
  - "@@DATEFIRST"
dev_langs: 
  - "TSQL"
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
ms.assetid: a178868e-49d5-4bd5-a5e2-1283409c8ce6
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# &#x40;&#x40;DATEFIRST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the current value of [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md), for a specific session.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
@@DATEFIRST  
```  
  
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
  
```sql
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
  
  

