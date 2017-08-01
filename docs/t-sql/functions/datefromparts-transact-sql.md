---
title: "DATEFROMPARTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATEFROMPARTS_TSQL"
  - "DATEFROMPARTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DATEFROMPARTS function"
ms.assetid: 5b885376-87aa-41f1-9e18-04987aead250
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATEFROMPARTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all_md](../../includes/tsql-appliesto-ss2012-all-md.md)]

Returns a **date** value for the specified year, month, and day.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DATEFROMPARTS ( year, month, day )  
```  
  
## Arguments  
*year*  
Integer expression specifying a year.
  
*month*  
Integer expression specifying a month, from 1 to 12.
  
*day*  
Integer expression specifying a day.
  
## Return types
**date**
  
## Remarks  
**DATEFROMPARTS** returns a **date** value with the date portion set to the specified year, month and day, and the time portion set to the default. If the arguments are not valid, then an error is raised. If required arguments are null, then null is returned.
  
This function is capable of being remoted to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] servers and above. It will not be remoted to servers with a version below [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].
  
## Examples  
The following example demonstrates the **DATEFROMPARTS** function.
  
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
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
The following example demonstrates the **DATEFROMPARTS** function.
  
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
  
  

