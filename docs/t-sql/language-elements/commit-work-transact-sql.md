---
title: "COMMIT WORK (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COMMIT_WORK_TSQL"
  - "WORK_TSQL"
  - "WORK"
  - "COMMIT WORK"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ending transactions [SQL Server]"
  - "transactions [SQL Server], ending"
  - "marking end of transactions [SQL Server]"
  - "COMMIT WORK statement"
ms.assetid: 4de76f33-399e-4912-a617-6eb6c560a058
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# COMMIT WORK (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Marks the end of a transaction.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
COMMIT [ WORK ]  
[ ; ]  
```  
  
## Remarks  
 This statement functions identically to COMMIT TRANSACTION, except COMMIT TRANSACTION accepts a user-defined transaction name. This COMMIT syntax, with or without specifying the optional keyword WORK, is compatible with SQL-92.  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [ROLLBACK WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-work-transact-sql.md)   
 [SAVE TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/save-transaction-transact-sql.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
  
