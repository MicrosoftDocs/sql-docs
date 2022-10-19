---
description: "sp_reset_dtc_log (Transact-SQL)"
title: "sp_reset_dtc_log (Transact-SQL)"
ms.custom: ""
ms.date: "10/18/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
author: VanMSFT
ms.author: vanto
monikerRange: ">= sql-server-ver16||>= sql-server-linux-ver16"
---
# sp_reset_dtc_log (Transact-SQL)

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

  Clears MSDTC logs.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_reset_dtc_log
```  
  
## Arguments  

There are no arguments for this Stored Procedure.
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 **What permissions are needed to execute this?** 
  
## Examples
  
```sql
EXEC sp_reset_dtc_log
```  
  
## See also  

- [sys.sp_manage_distributed_transaction (Transact-SQL)](sys-sp-manage-distributed-transaction.md)
- [sys.dm_tran_distributed_transactions_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-distributed-transactions-stats.md)