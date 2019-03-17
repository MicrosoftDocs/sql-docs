---
title: "DAY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DAY_TSQL"
  - "DAY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "date and time [SQL Server], DAY"
  - "dates [SQL Server], functions"
  - "DAY function [SQL Server]"
  - "dates [SQL Server], days"
  - "functions [SQL Server], date and time"
  - "dateparts [SQL Server], day"
ms.assetid: 2f4410ea-fd3e-4d69-ac4b-3b0091a084bc
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DAY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns an integer that represents the day (day of the month) of the specified *date*.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DAY ( date )  
```  
  
## Arguments  
*date*  
An expression that resolves to one of the following data types:

+ **date**
+ **datetime**
+ **datetimeoffset**
+ **datetime2** 
+ **smalldatetime**
+ **time**

For *date*, `DAY` will accept a column expression, expression, string literal, or user-defined variable.
  
## Return Type  
**int**
  
## Return Value  
DAY returns the same value as [DATEPART](../../t-sql/functions/datepart-transact-sql.md) (**day**, *date*).
  
If *date* contains only a time part, `DAY` will return 1 - the base day.
  
## Examples  
This statement returns `30` - the number of the day itself.
  
```sql
SELECT DAY('2015-04-30 01:01:01.1234567');  
```  
  
This statement returns `1900, 1, 1`. The *date* argument has a number value of `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets `0` as January 1, 1900.
  
```sql
SELECT YEAR(0), MONTH(0), DAY(0);  
```  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  


