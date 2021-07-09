---
description: "SMALLDATETIMEFROMPARTS (Transact-SQL)"
title: "SMALLDATETIMEFROMPARTS (Transact-SQL)"
ms.custom: ""
ms.date: "01/29/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: reference
f1_keywords: 
  - "SMALLDATETIMEFROMPARTS"
  - "SMALLDATETIMEFROMPARTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SMALLDATETIMEFROMPARTS function"
author: julieMSFT
ms.author: jrasnick
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SMALLDATETIMEFROMPARTS (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a **smalldatetime** value for the specified date and time.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
SMALLDATETIMEFROMPARTS ( year, month, day, hour, minute )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *year*  
 Integer expression specifying a year.  
  
 *month*  
 Integer expression specifying a month.  
  
 *day*  
 Integer expression specifying a day.  
  
 *hour*  
 Integer expression specifying hours.  
  
 *minute*  
 Integer expression specifying minutes.  
  
## Return Types  
 **smalldatetime**  
  
## Remarks  
 This function acts like a constructor for a fully initialized **smalldatetime** value. If the arguments are not valid, then an error is thrown. If required arguments are null, then null is returned.  
 
 This function is capable of being remoted to [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] servers and above. It is not remoted to servers that have a version below [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)].  
  
## Examples  
  
```sql  
SELECT SMALLDATETIMEFROMPARTS ( 2010, 12, 31, 23, 59 ) AS Result  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------------------  
2010-12-31 23:59:00  
  
(1 row(s) affected)  
```  
  

