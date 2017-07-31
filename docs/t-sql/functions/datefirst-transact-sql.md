---
title: "@@DATEFIRST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 46
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# @@DATEFIRST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the current value, for a session, of [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md).
  
For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
@@DATEFIRST  
```  
  
## Return Type  
**tinyint**
  
## Remarks  
SET DATEFIRST specifies the first day of the week. The U.S. English default is 7, Sunday.
  
This language setting affects the interpretation of character strings as they are converted to date values for storage in the database, and the display of date values that are stored in the database. This setting does not affect the storage format of date data. In the following example, the language is first set to `Italian`. The statement `SELECT @@DATEFIRST;` returns `1`. The language is then set to `us_english`. The statement `SELECT @@DATEFIRST;` returns `7`.
  
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
The following example sets the first day of the week to `5` (Friday), and assumes the current day, `Today`, to be Saturday. The `SELECT` statement returns the `DATEFIRST` value and the number of the current day of the week.
  
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
  
  

