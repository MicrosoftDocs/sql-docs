---
title: "DATEFROMPARTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATEFROMPARTS_TSQL"
  - "DATEFROMPARTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DATEFROMPARTS function"
ms.assetid: 5b885376-87aa-41f1-9e18-04987aead250
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DATEFROMPARTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

This function returns a **date** value that maps to the specified year, month, and day values.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATEFROMPARTS ( year, month, day )  
```  
  
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
  
```sql
Result  
----------------------------------  
2010-12-31  
  
(1 row(s) affected)  
```  
  
## See also
[date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md)
  
  

