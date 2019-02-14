---
title: "GETANSINULL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GETANSINULL"
  - "GETANSINULL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "null values [SQL Server], default"
  - "GETANSINULL function"
  - "default nullability"
  - "database nullability [SQL Server]"
ms.assetid: 189399e4-428d-4902-b3a8-94f07fdefc6a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# GETANSINULL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the default nullability for the database for this session.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
GETANSINULL ( [ 'database' ] )  
```  
  
## Arguments  
 '*database*'  
 Is the name of the database for which to return nullability information. *database is either **char** or **nchar**. If **char**, *database* is implicitly converted to **nchar**.  
  
## Return Types  
 **int**  
  
## Remarks  
GETANSINULL returns 1 if the database's nullability allows for null values. This return value also requires that the column or data type nullability isn't explicitly defined. The ANSI NULL default is 1. 
  
 To enable the ANSI NULL default behavior, one of these conditions must be set:  
  
-   ALTER DATABASE *database_name* SET ANSI_NULL_DEFAULT ON  
  
-   SET ANSI_NULL_DFLT_ON ON  
  
-   SET ANSI_NULL_DFLT_OFF OFF  
  
## Examples  
 The following example returns the default nullability for the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT GETANSINULL('AdventureWorks2012')  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 ------  
1  

(1 row(s) affected)
 ```  
  
## See Also  
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
  
  
