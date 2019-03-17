---
title: "@@DBTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@DBTS_TSQL"
  - "@@DBTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@DBTS function"
  - "timestamp data type"
ms.assetid: 91842ddd-91c0-4445-a03f-116f6bc991d0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;DBTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the value of the current **timestamp** data type for the current database. The current database will have a guaranteed unique timestamp value.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
@@DBTS  
```  
  
## Return types
**varbinary**
  
## Remarks  
@@DBTS returns the last-used timestamp value of the current database. An insert or update of a row with a **timestamp** column generates a new timestamp value.
  
Changes to the transaction isolation levels do  not affect the @@DBTS function.
  
## Examples  
This example returns the current **timestamp** from the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT @@DBTS;  
```  
  
## See also
[Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)  
[Cursor Concurrency &#40;ODBC&#41;](../../relational-databases/native-client-odbc-cursors/properties/cursor-concurrency-odbc.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[MIN_ACTIVE_ROWVERSION &#40;Transact-SQL&#41;](../../t-sql/functions/min-active-rowversion-transact-sql.md)
  
  
