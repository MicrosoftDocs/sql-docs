---
title: "YEAR (Transact-SQL)"
description: "YEAR (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "YEAR"
  - "YEAR_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "dates [SQL Server], years"
  - "date and time [SQL Server], YEAR"
  - "functions [SQL Server], date and time"
  - "YEAR function [SQL Server]"
  - "dateparts [SQL Server], year"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# YEAR (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns an integer that represents the year of the specified *date*.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
YEAR ( date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *date*  
 Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. The *date* argument can be an expression, column expression, user-defined variable or string literal.  
  
## Return Types  
 **int**  
  
## Return Value  
 YEAR returns the same value as [DATEPART](../../t-sql/functions/datepart-transact-sql.md) (**year**, *date*).  
  
 If *date* only contains a time part, the return value is 1900, the base year.  
  
## Examples  
 The following statement returns `2010`. This is the number of the year.  
  
```sql  
SELECT YEAR('2010-04-30T01:01:01.1234567-07:00');  
```  
  
 The following statement returns `1900, 1, 1`. The argument for *date* is the number `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets `0` as January 1, 1900.  
  
```sql 
SELECT YEAR(0), MONTH(0), DAY(0);  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following statement returns `1900, 1, 1`. The argument for *date* is the number `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets `0` as January 1, 1900.  
  
```sql  
SELECT TOP 1 YEAR(0), MONTH(0), DAY(0);  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  

