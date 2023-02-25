---
description: "sp_reset_dtc_log (Transact-SQL)"
title: "sp_reset_dtc_log (Transact-SQL)"
ms.custom: ""
ms.date: "02/24/2023"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
author: VanMSFT
ms.author: vanto
monikerRange: ">= sql-server-ver16||>= sql-server-linux-ver16"
---
# sp_reset_dtc_log (Transact-SQL)

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

  Clears the MSDTC log.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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

Requires **sysadmin** or have **CONTROL SERVER** permissions.
  
## Examples
  
```sql
EXEC sp_reset_dtc_log
```  
  
## See also  

- [sys.sp_manage_distributed_transaction (Transact-SQL)](sys-sp-manage-distributed-transaction.md)
- [sys.dm_tran_distributed_transaction_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-distributed-transaction-stats.md)