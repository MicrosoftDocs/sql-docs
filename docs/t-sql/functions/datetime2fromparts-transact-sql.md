---
title: "DATETIME2FROMPARTS (Transact-SQL)"
description: "DATETIME2FROMPARTS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATETIME2FROMPARTS_TSQL"
  - "DATETIME2FROMPARTS"
helpviewer_keywords:
  - "DATETIME2FROMPARTS function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATETIME2FROMPARTS (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns a **datetime2** value for the specified date and time arguments. The returned value has a precision specified by the precision argument.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATETIME2FROMPARTS ( year, month, day, hour, minute, seconds, fractions, precision )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*year*  
An integer expression that specifies a year.
  
*month*  
An integer expression that specifies a month.
  
*day*  
An integer expression that specifies a day.
  
*hour*  
An integer expression that specifies the hours.
  
*minute*  
An integer expression that specifies the minutes.
  
*seconds*  
An integer expression that specifies the seconds.
  
*fractions*  
An integer expression that specifies a fractional seconds value.
  
*precision*  
An integer expression that specifies the precision of the **datetime2** value that `DATETIME2FROMPARTS` will return.
  
## Return types
**datetime2(** *precision* **)**
  
## Remarks  
`DATETIME2FROMPARTS` returns a fully initialized **datetime2** value. `DATETIME2FROMPARTS` will raise an error if at least one required argument has an invalid value. `DATETIME2FROMPARTS` returns null if at least one required argument has a null value. However, if the *precision* argument has a null value, `DATETIME2FROMPARTS` will raise an error.

The *fractions* argument depends on the *precision* argument. For example, for a *precision* value of 7, each fraction represents 100 nanoseconds; for a *precision* of 3, each fraction represents a millisecond. For a *precision* value of zero, the value of *fractions* must also be zero; otherwise, `DATETIME2FROMPARTS` will raise an error.
  
This function is capable of being remoted to [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] servers and above. It is not remoted to servers that have a version below [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)].  
  
## Examples  
  
### A. An example without fractions of a second  
  
```sql
SELECT DATETIME2FROMPARTS ( 2010, 12, 31, 23, 59, 59, 0, 0 ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Result  
---------------------------  
2010-12-31 23:59:59.0000000  
  
(1 row(s) affected)  
```  
  
### B. Example with fractions of a second  
This example shows the use of the *fractions* and *precision* parameters:
  
1.  When *fractions* has a value of 5, and *precision* has a value of 1, the value of *fractions* represents 5/10 of a second.  
  
2.  When *fractions* has a value of 50, and *precision* has a value of 2, the value of *fractions* represents 50/100 of a second.  
  
3.  When *fractions* has a value of 500, and *precision* has a value of 3, then the value of *fractions* represents 500/1000 of a second.  
  
```sql
SELECT DATETIME2FROMPARTS ( 2011, 8, 15, 14, 23, 44, 5, 1 );  
SELECT DATETIME2FROMPARTS ( 2011, 8, 15, 14, 23, 44, 50, 2 );  
SELECT DATETIME2FROMPARTS ( 2011, 8, 15, 14, 23, 44, 500, 3 );  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
----------------------  
2011-08-15 14:23:44.5  
  
(1 row(s) affected)  
  
----------------------  
2011-08-15 14:23:44.50  
  
(1 row(s) affected)  
  
----------------------  
2011-08-15 14:23:44.500  
  
(1 row(s) affected)  
```  
  
## See also
[datetime2 &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime2-transact-sql.md)
  
  

