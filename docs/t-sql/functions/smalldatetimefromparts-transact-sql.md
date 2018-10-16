---
title: "SMALLDATETIMEFROMPARTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SMALLDATETIMEFROMPARTS"
  - "SMALLDATETIMEFROMPARTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SMALLDATETIMEFROMPARTS function"
ms.assetid: 7467fdab-e588-419c-9e29-42caec34a9ea
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SMALLDATETIMEFROMPARTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

  Returns a **smalldatetime** value for the specified date and time.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SMALLDATETIMEFROMPARTS ( year, month, day, hour, minute )  
```  
  
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
  
 This function is capable of being remoted to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] servers and above. It is not remoted to servers that have a version below [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Examples  
  
```  
SELECT SMALLDATETIMEFROMPARTS ( 2010, 12, 31, 23, 59 ) AS Result  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------------------  
2011-01-01 00:00:00  
  
(1 row(s) affected)  
```  
  

