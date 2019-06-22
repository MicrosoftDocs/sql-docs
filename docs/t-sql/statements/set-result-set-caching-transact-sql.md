---
title: "SET RESULT SET CACHING  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/03/2019"
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
dev_langs:
  - "TSQL"
helpviewer_keywords: 
author: XiaoyuL-Preview
ms.author: xiaoyul
manager: craigg
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---
# SET RESULT SET CACHING (Transact-SQL) 

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Controls the result set caching behavior for the current client session.  

Applies to Azure SQL Data Warehouse (preview) 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```
SET RESULT_SET_CACHING { ON | OFF };
```  
  
## Remarks  

**ON**   
Enables result set caching for the current client session.  Result set caching cannot be turned ON for a session if it is turned OFF at the database level.

**OFF**   
Disable result set caching for the current client session.

## Permissions

Requires membership in the public role

## See also

[SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)</br>
[ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)