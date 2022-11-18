---
title: COMMIT WORK (Transact-SQL)
description: "COMMIT WORK (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "COMMIT_WORK_TSQL"
  - "WORK_TSQL"
  - "WORK"
  - "COMMIT WORK"
helpviewer_keywords:
  - "ending transactions [SQL Server]"
  - "transactions [SQL Server], ending"
  - "marking end of transactions [SQL Server]"
  - "COMMIT WORK statement"
dev_langs:
  - "TSQL"
---
# COMMIT WORK (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Marks the end of a transaction.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```syntaxsql
COMMIT [ WORK ]  
[ ; ]  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
