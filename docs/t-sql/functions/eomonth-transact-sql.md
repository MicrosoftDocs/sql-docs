---
title: "EOMONTH (Transact-SQL)"
description: "EOMONTH (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "EOMONTH"
  - "EOMONTH_TSQL"
helpviewer_keywords:
  - "EOMONTH function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# EOMONTH (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the last day of the month containing a specified date, with an optional offset.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
EOMONTH ( start_date [, month_to_add ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*start_date*  
A date expression that specifies the date for which to return the last day of the month.  
  
*month_to_add*  
An optional integer expression that specifies the number of months to add to *start_date*.  
  
If the *month_to_add* argument has a value, then `EOMONTH` adds the specified number of months to *start_date*, and then returns the last day of the month for the resulting date. If this addition overflows the valid range of dates, then `EOMONTH` will raise an error.  
  
## Return Type  
 **date**  
  
## Remarks  
The `EOMONTH` function can remote to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] servers and higher. It cannot be remote to servers with a version lower than [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
## Examples  
  
### A. EOMONTH with explicit datetime type  
  
```sql 
DECLARE @date DATETIME = '12/1/2011';  
SELECT EOMONTH ( @date ) AS Result;  
GO  
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
------------  
2011-12-31  
  
(1 row(s) affected)  
```  

### B. EOMONTH with string parameter and implicit conversion  
  
```sql
DECLARE @date VARCHAR(255) = '12/1/2011';  
SELECT EOMONTH ( @date ) AS Result;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
------------  
2011-12-31  
  
(1 row(s) affected)  
```  
  
### C. EOMONTH with and without the month_to_add parameter  
  
The values shown in these result sets reflect an execution date between and including `12/01/2011` and `12/31/2011`.

```sql  
DECLARE @date DATETIME = GETDATE();  
SELECT EOMONTH ( @date ) AS 'This Month';  
SELECT EOMONTH ( @date, 1 ) AS 'Next Month';  
SELECT EOMONTH ( @date, -1 ) AS 'Last Month';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
This Month  
-----------------------  
2011-12-31  
  
(1 row(s) affected)  
  
Next Month  
-----------------------  
2012-01-31  
  
(1 row(s) affected)  
  
Last Month  
-----------------------  
2011-11-30  
  
(1 row(s) affected)  
```
