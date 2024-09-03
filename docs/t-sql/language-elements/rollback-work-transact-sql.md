---
title: ROLLBACK WORK (Transact-SQL)
description: "ROLLBACK WORK (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ROLLBACK WORK"
  - "ROLLBACK_WORK_TSQL"
helpviewer_keywords:
  - "transaction rollbacks [SQL Server]"
  - "erasing data modifications [SQL Server]"
  - "ROLLBACK WORK statement"
  - "roll back transactions [SQL Server]"
  - "rolling back transactions, ROLLBACK WORK"
  - "savepoints [SQL Server]"
dev_langs:
  - "TSQL"
---
# ROLLBACK WORK (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Rolls back a user-specified transaction to the beginning of the transaction.  
  
 
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
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
  
  
