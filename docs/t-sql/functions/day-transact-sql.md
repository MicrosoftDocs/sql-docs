---
title: "DAY (Transact-SQL)"
description: "DAY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DAY_TSQL"
  - "DAY"
helpviewer_keywords:
  - "date and time [SQL Server], DAY"
  - "dates [SQL Server], functions"
  - "DAY function [SQL Server]"
  - "dates [SQL Server], days"
  - "functions [SQL Server], date and time"
  - "dateparts [SQL Server], day"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DAY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns an integer that represents the day (day of the month) of the specified *date*.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DAY ( date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  


