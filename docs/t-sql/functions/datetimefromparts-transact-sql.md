---
title: "DATETIMEFROMPARTS (Transact-SQL)"
description: "DATETIMEFROMPARTS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATETIMEFROMPARTS_TSQL"
  - "DATETIMEFROMPARTS"
helpviewer_keywords:
  - "DATETIMEFROMPARTS function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATETIMEFROMPARTS (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns a **datetime** value for the specified date and time arguments.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATETIMEFROMPARTS ( year, month, day, hour, minute, seconds, milliseconds )  
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
An integer expression that specifies hours.
  
*minute*  
An integer expression that specifies minutes.
  
*seconds*  
An integer expression that specifies seconds.
  
*milliseconds*  
An integer expression that specifies milliseconds.
  
## Return types
**datetime**
  
## Remarks  
`DATETIMEFROMPARTS` returns a fully initialized **datetime** value. `DATETIMEFROMPARTS` will raise an error if at least one required argument has an invalid value. `DATETIMEFROMPARTS` returns null if at least one required argument has a null value.
  
This function is capable of being remoted to [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] servers and above. It is not remoted to servers that have a version below [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)].  
  
## Examples  
  
```sql
SELECT DATETIMEFROMPARTS ( 2010, 12, 31, 23, 59, 59, 0 ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Result  
---------------------------  
2010-12-31 23:59:59.000  
  
(1 row(s) affected)  
```  
  
## See also
[datetime &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime-transact-sql.md)
  
  

