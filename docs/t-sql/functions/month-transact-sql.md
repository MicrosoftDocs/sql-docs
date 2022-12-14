---
title: "MONTH (Transact-SQL)"
description: "MONTH (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "MONTH_TSQL"
  - "MONTH"
helpviewer_keywords:
  - "values [SQL Server], date and time"
  - "dates [SQL Server], functions"
  - "month of year [SQL Server]"
  - "date and time [SQL Server], MONTH"
  - "dateparts [SQL Server], month"
  - "functions [SQL Server], date and time"
  - "dates [SQL Server], MONTH"
  - "MONTH function [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# MONTH (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns an integer that represents the month of the specified *date*.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
MONTH ( date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *date*  
 Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. The *date* argument can be an expression, column expression, user-defined variable, or string literal.  
  
## Return Type  
 **int**  
  
## Return Value  
 MONTH returns the same value as [DATEPART](../../t-sql/functions/datepart-transact-sql.md) (**month**, *date*).  
  
 If *date* contains only a time part, the return value is 1, the base month.  
  
## Examples  
 The following statement returns `4`. This is the number of the month.  
  
```sql  
SELECT MONTH('2007-04-30T01:01:01.1234567 -07:00');  
```  
  
 The following statement returns `1900, 1, 1`. The argument for *date* is the number `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets `0` as January 1, 1900.  
  
```sql  
SELECT YEAR(0), MONTH(0), DAY(0);  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns `4`. This is the number of the month.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT TOP 1 MONTH('2007-04-30T01:01:01.1234')   
FROM dbo.DimCustomer;  
```  
  
 The following example returns `1900, 1, 1`. The argument for *date* is the number `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets `0` as January 1, 1900.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT TOP 1 YEAR(0), MONTH(0), DAY(0) FROM dbo.DimCustomer;  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  

