---
title: "sys.dm_tran_distributed_transaction_stats (Transact-SQL)"
description: sys.dm_tran_distributed_transaction_stats (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "02/24/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# sys.dm_tran_distributed_transaction_stats (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asmi.md)]

  Returns information about MSDTC statistics in SQL Server.

## Table returned  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|aborted  |  int|  The number of transactions that were shut down before they were completed.  |
|aborted_max  |  int|  The highest number of aborted transactions since DTC last started.  |
|forced_abort |   int | The number of aborted transactions that were manually shut down before they were completed. |
|committed |  int | The number of committed transactions for the instance.  |
|committed_max |  int | The highest number of committed transactions since DTC last started.  |
|forced_commit |  int | The number of committed transactions that were manually committed.  |
|heuristic |  int | TBD  |
|heuristic_max |  int | TBD  |
|in_doubt |  int | The number of in doubt transactions.  |
|in_doubt_max |  int | The highest number of in doubt transactions since DTC last started.  |
|open |  int | The number of running transactions for the instance.  |
|open_max |  int | The highest number of concurrently running transactions since DTC last started.  |
|single_phase_in_doubt |  int | TBD  |
  
## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission.

## Remarks

This view will always return one row with statistics data.

## See also

- [sys.sp_manage_distributed_transaction (Transact-SQL)](../system-stored-procedures/sys-sp-manage-distributed-transaction.md)
- [sp_reset_dtc_log (Transact-SQL)](../system-stored-procedures/sp-reset-dtc-log.md)
