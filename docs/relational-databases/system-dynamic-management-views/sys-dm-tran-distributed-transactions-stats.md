---
title: "sys.dm_tran_distributed_transactions_stats (Transact-SQL)"
description: sys.dm_tran_distributed_transactions_stats (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "11/16/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.technology: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# sys.dm_tran_distributed_transactions_stats (Transact-SQL)
[!INCLUDE [SQL Server 2022 ](../../includes/applies-to-version/sqlserver2022.md)]

  Returns information about MSDTC statistics in SQL Server.

## Table returned  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|aborted  |  int|  When 1, indicates that the transaction has an aborted transaction.  |
|aborted_max  |  int|  **Need info**  |
|forced_abort |   int | **Need info** |
|committed |  int | **Need info**  |
|committed_max |  int | **Need info**  |
|forced_commit |  int | **Need info**  |
|heuristic |  int | **Need info**  |
|heuristic_max |  int | **Need info**  |
|in_doubt |  int | **Need info**  |
|in_doubt_max |  int | **Need info**  |
|open |  int | **Need info**  |
|open_max |  int | **Need info**  |
|single_phase_in_doubt |  int | **Need info**  |
  
## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission.

## Remarks   

**Place holder**

## See also

- [sys.sp_manage_distributed_transaction (Transact-SQL)](../system-stored-procedures/sys-sp-manage-distributed-transaction.md)
- [sp_reset_dtc_log (Transact-SQL)](../system-stored-procedures/sp-reset-dtc-log.md)
