---
title: "DATETIMEFROMPARTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATETIMEFROMPARTS_TSQL"
  - "DATETIMEFROMPARTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DATETIMEFROMPARTS function"
ms.assetid: 6008148b-bf75-4c98-9392-68a89fa0711c
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DATETIMEFROMPARTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

This function returns a **datetime** value for the specified date and time arguments.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATETIMEFROMPARTS ( year, month, day, hour, minute, seconds, milliseconds )  
```  
  
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
  
This function supports remoting to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] servers and above. It will not support remoting to servers that have a version below [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].
  
## Examples  
  
```sql
SELECT DATETIMEFROMPARTS ( 2010, 12, 31, 23, 59, 59, 0 ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Result  
---------------------------  
2010-12-31 23:59:59.000  
  
(1 row(s) affected)  
```  
  
## See also
[datetime &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime-transact-sql.md)
  
  

