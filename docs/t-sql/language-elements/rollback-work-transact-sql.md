---
title: "ROLLBACK WORK (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ROLLBACK WORK"
  - "ROLLBACK_WORK_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transaction rollbacks [SQL Server]"
  - "erasing data modifications [SQL Server]"
  - "ROLLBACK WORK statement"
  - "roll back transactions [SQL Server]"
  - "rolling back transactions, ROLLBACK WORK"
  - "savepoints [SQL Server]"
ms.assetid: 2071dbd3-53d5-4510-be8d-26e80f2553b4
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ROLLBACK WORK (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Rolls back a user-specified transaction to the beginning of the transaction.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ROLLBACK [ WORK ]  
[ ; ]  
```  
  
## Remarks  
 This statement functions identically to ROLLBACK TRANSACTION except that ROLLBACK TRANSACTION accepts a user-defined transaction name. With or without specifying the optional WORK keyword, this ROLLBACK syntax is ISO-compatible.  
  
 When nesting transactions, ROLLBACK WORK always rolls back to the outermost BEGIN TRANSACTION statement and decrements the @@TRANCOUNT system function to 0.  
  
## Permissions  
 ROLLBACK WORK permissions default to any valid user.  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [COMMIT WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-work-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [SAVE TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/save-transaction-transact-sql.md)  
  
  
