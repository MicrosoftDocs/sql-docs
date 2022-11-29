---
title: "DATEFROMPARTS (Transact-SQL)"
description: "DATEFROMPARTS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATEFROMPARTS_TSQL"
  - "DATEFROMPARTS"
helpviewer_keywords:
  - "DATEFROMPARTS function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATEFROMPARTS (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns a **date** value that maps to the specified year, month, and day values.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATEFROMPARTS ( year, month, day )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*year*  
An integer expression that specifies a year.
  
*month*  
An integer expression that specifies a month, from 1 to 12.
  
*day*  
An integer expression that specifies a day.
  
## Return types
**date**
  
## Remarks  
`DATEFROMPARTS` returns a **date** value, with the date portion set to the specified year, month and day, and the time portion set to the default. For invalid arguments, `DATEFROMPARTS` will raise an error. `DATEFROMPARTS` returns null if at least one required argument has a null value.
  
This function can handle remoting to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] servers and above. It cannot handle remoting to servers with a version below [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].
  
## Examples  
This example shows the `DATEFROMPARTS` function in action.
  
```sql
SELECT DATEFROMPARTS ( 2010, 12, 31 ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Result  
----------------------------------  
2010-12-31  
  
(1 row(s) affected)  
```  
  
## See also
[date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md)
  
  

