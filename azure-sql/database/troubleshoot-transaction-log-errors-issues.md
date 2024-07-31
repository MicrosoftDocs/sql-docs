---
title: Troubleshoot transaction log issues
titleSuffix: Azure SQL Database
description: Provides steps to troubleshoot transaction log issues in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma
ms.date: 09/08/2023
ms.service: azure-sql-database
ms.subservice: development
ms.topic: troubleshooting
monikerRange: "=azuresql-db"
---

# Troubleshoot transaction log errors with Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](troubleshoot-transaction-log-errors-issues.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/troubleshoot-transaction-log-errors-issues.md?view=azuresql-mi&preserve-view=true)

You may see errors 9002 or 40552 when the transaction log is full and cannot accept new transactions. These errors occur when the database transaction log, managed by Azure SQL Database, exceeds thresholds for space and cannot continue to accept transactions. These errors are similar to issues with a full transaction log in SQL Server, but have different resolutions in SQL Server, Azure SQL Database, and Azure SQL Managed Instance.

> [!NOTE]
> **This article is focused on Azure SQL Database.** Azure SQL Database is based on the latest stable version of the Microsoft SQL Server database engine, so much of the content is similar, though troubleshooting options and tools may differ from SQL Server.
>
> For more on troubleshooting a transaction log in Azure SQL Managed Instance, see [Troubleshoot transaction log errors with Azure SQL Managed Instance](../managed-instance/troubleshoot-transaction-log-errors-issues.md?view=azuresql-mi&preserve-view=true).
>
> For more on troubleshooting a transaction log in SQL Server, see [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002).

## Automated backups and the transaction log

In Azure SQL Database, transaction log backups are taken automatically. For frequency, retention, and more information, see [Automated backups](automated-backups-overview.md?view=azuresql-db&preserve-view=true). 

Free disk space, database file growth, and file location are also managed, so the typical causes and resolutions of transaction log issues are different from SQL Server. 

Similar to SQL Server, the transaction log for each database is truncated whenever a log backup completes successfully. Truncation leaves empty space in the log file, which can then be used for new transactions. When the log file cannot be truncated by log backups, the log file grows to accommodate new transactions. If the log file grows to its maximum limit in Azure SQL Database, new write transactions fail.

For information on transaction log sizes, see:

- For vCore resource limits for a single database, see [resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md?view=azuresql-db&preserve-view=true).
- For vCore resource limits for elastic pools, see [resource limits for elastic pools using the vCore purchasing model](resource-limits-vcore-elastic-pools.md?view=azuresql-db&preserve-view=true).
- For DTU resource limits for a single database, see [resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md?view=azuresql-db&preserve-view=true).
- For DTU resource limits for elastic pools, see [resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md?view=azuresql-db&preserve-view=true).

## Prevented transaction log truncation

To discover what is preventing log truncation in a given case, refer to  `log_reuse_wait_desc` in `sys.databases`. The log reuse wait informs you to what conditions or causes are preventing the transaction log from being truncated by a regular log backup. For more information, see [sys.databases (Transact-SQL)](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=azuresqldb-current&preserve-view=true).

```sql
SELECT [name], log_reuse_wait_desc FROM sys.databases;
```

For Azure SQL Database, it is recommended to connect to a specific user database, rather than the `master` database, to execute this query.

The following values of `log_reuse_wait_desc` in `sys.databases` may indicate the reason why the database's transaction log truncation is being prevented:

| log_reuse_wait_desc | Diagnosis | Response required |
|--|--|--|
| **NOTHING** | Typical state. There is nothing blocking the log from truncating. | No. |
| **CHECKPOINT** | A checkpoint is needed for log truncation. Rare. | No response required unless sustained. If sustained, file a support request with [Azure Support](https://portal.azure.com/#create/Microsoft.Support). | 
| **LOG BACKUP** | A log backup is required. | No response required unless sustained. If sustained, file a support request with [Azure Support](https://portal.azure.com/#create/Microsoft.Support). | 
| **ACTIVE BACKUP OR RESTORE** | A database backup is in progress. | No response required unless sustained. If sustained, file a support request with [Azure Support](https://portal.azure.com/#create/Microsoft.Support). | 
| **ACTIVE TRANSACTION** | An ongoing transaction is preventing log truncation. | The log file cannot be truncated due to active and/or uncommitted transactions. See next section.|
| **REPLICATION** | In Azure SQL Database, this may occur if [change data capture (CDC)](/sql/relational-databases/track-changes/about-change-data-capture-sql-server) is enabled. | Query [sys.dm_cdc_errors](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors) and resolve errors. If unresolvable, file a support request with [Azure Support](https://portal.azure.com/#create/Microsoft.Support).|
| **AVAILABILITY_REPLICA** | Synchronization to the secondary replica is in progress. | No response required unless sustained. If sustained, file a support request with [Azure Support](https://portal.azure.com/#create/Microsoft.Support). |

### Log truncation prevented by an active transaction

The most common scenario for a transaction log that cannot accept new transactions is a long-running or blocked transaction.

Run this sample query to find uncommitted or active transactions and their properties.

- Returns information about transaction properties, from [sys.dm_tran_active_transactions](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-session-transactions-transact-sql?view=azuresqldb-current&preserve-view=true).
- Returns session connection information, from [sys.dm_exec_sessions](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql?view=azuresqldb-current&preserve-view=true).
- Returns request information (for active requests), from [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql?view=azuresqldb-current&preserve-view=true). This query can also be used to identify sessions being blocked, look for the `request_blocked_by`. For more information, see [Gather blocking information](understand-resolve-blocking.md?view=azuresql-db&preserve-view=true#gather-blocking-information). 
- Returns the current request's text or input buffer text, using the [sys.dm_exec_sql_text](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql?view=azuresqldb-current&preserve-view=true) or [sys.dm_exec_input_buffer](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql?view=azuresqldb-current&preserve-view=true) DMVs. If the data returned by the `text` field of `sys.dm_exec_sql_text` is NULL, the request is not active but has an outstanding transaction. In that case, the `event_info` field of `sys.dm_exec_input_buffer` contains the last statement passed to the database engine. 

```sql
SELECT [database_name] = db_name(s.database_id)
, tat.transaction_id, tat.transaction_begin_time, tst.session_id 
, session_open_transaction_count = tst.open_transaction_count
, transaction_duration_s = datediff(s, tat.transaction_begin_time, sysdatetime())
, input_buffer = ib.event_info
, request_text = CASE  WHEN r.statement_start_offset = 0 and r.statement_end_offset= 0 THEN left(est.text, 4000)
                       ELSE    SUBSTRING ( est.[text],    r.statement_start_offset/2 + 1, 
                                           CASE WHEN r.statement_end_offset = -1 THEN LEN (CONVERT(nvarchar(max), est.[text])) 
                                                ELSE r.statement_end_offset/2 - r.statement_start_offset/2 + 1
                                           END  )  END
, request_status = r.status
, request_blocked_by = r.blocking_session_id
, transaction_state = CASE tat.transaction_state    
                     WHEN 0 THEN 'The transaction has not been completely initialized yet.'
                     WHEN 1 THEN 'The transaction has been initialized but has not started.'
                     WHEN 2 THEN 'The transaction is active - has not been committed or rolled back.'
                     WHEN 3 THEN 'The transaction has ended. This is used for read-only transactions.'
                     WHEN 4 THEN 'The commit process has been initiated on the distributed transaction. This is for distributed transactions only. The distributed transaction is still active but further processing cannot take place.'
                     WHEN 5 THEN 'The transaction is in a prepared state and waiting resolution.'
                     WHEN 6 THEN 'The transaction has been committed.'
                     WHEN 7 THEN 'The transaction is being rolled back.'
                     WHEN 8 THEN 'The transaction has been rolled back.' END 
, transaction_name = tat.name
, azure_dtc_state    --Applies to: Azure SQL Database only
             =    CASE tat.dtc_state 
                 WHEN 1 THEN 'ACTIVE'
                 WHEN 2 THEN 'PREPARED'
                 WHEN 3 THEN 'COMMITTED'
                 WHEN 4 THEN 'ABORTED'
                 WHEN 5 THEN 'RECOVERED' END
, transaction_type = CASE tat.transaction_type    WHEN 1 THEN 'Read/write transaction'
                                             WHEN 2 THEN 'Read-only transaction'
                                             WHEN 3 THEN 'System transaction'
                                             WHEN 4 THEN 'Distributed transaction' END
, tst.is_user_transaction
, local_or_distributed = CASE tst.is_local WHEN 1 THEN 'Local transaction, not distributed' WHEN 0 THEN 'Distributed transaction or an enlisted bound session transaction.' END
, transaction_uow    --for distributed transactions. 
, s.login_time, s.host_name, s.program_name, s.client_interface_name, s.login_name, s.is_user_process
, session_cpu_time = s.cpu_time, session_logical_reads = s.logical_reads, session_reads = s.reads, session_writes = s.writes
, observed = sysdatetimeoffset()
FROM sys.dm_tran_active_transactions AS tat 
INNER JOIN sys.dm_tran_session_transactions AS tst  on tat.transaction_id = tst.transaction_id
INNER JOIN Sys.dm_exec_sessions AS s on s.session_id = tst.session_id 
LEFT OUTER JOIN sys.dm_exec_requests AS r on r.session_id = s.session_id
CROSS APPLY sys.dm_exec_input_buffer(s.session_id, null) AS ib 
OUTER APPLY sys.dm_exec_sql_text (r.sql_handle) AS est;
```

### File management to free more space

If the transaction log is prevented from truncating in Azure SQL Database elastic pools, freeing space for the elastic pool may be part of the solution. However, resolving the root the condition blocking transaction log file truncation is key. In some cases, temporarily creating more disk space allows long-running transactions to complete, removing the condition blocking the transaction log file from truncating with a normal transaction log backup. However, freeing up space may provide only temporary relief until the transaction log grows again.

For more information on managing the file space of databases and elastic pools, see [Manage file space for databases in Azure SQL Database](file-space-manage.md?view=azuresql-db&preserve-view=true).

### Error 40552: The session has been terminated because of excessive transaction log space usage

`40552: The session has been terminated because of excessive transaction log space usage. Try modifying fewer rows in a single transaction.`

To resolve this issue, try the following methods:

1. The issue can occur in any DML operation such as insert, update, or delete. Review the transaction to avoid unnecessary writes. Try to reduce the number of rows that are operated on immediately by implementing batching or splitting into multiple smaller transactions. For more information, see [How to use batching to improve SQL Database application performance](../performance-improve-use-batching.md?view=azuresql-db&preserve-view=true).
1. The issue can occur because of index rebuild operations. To avoid this issue, ensure the following formula is true: (number of rows that are affected in the table) multiplied by (the average size of field that's updated in bytes + 80) < 2 gigabytes (GB). For large tables, consider creating partitions and performing index maintenance only on some partitions of the table. For more information, see [Create Partitioned Tables and Indexes](/sql/relational-databases/partitions/create-partitioned-tables-and-indexes?view=azuresqldb-current&preserve-view=true).
1. If you perform bulk inserts using the `bcp.exe` utility or the `System.Data.SqlClient.SqlBulkCopy` class, try using the `-b batchsize` or `BatchSize` options to limit the number of rows copied to the server in each transaction. For more information, see [bcp Utility](/sql/tools/bcp-utility).
1. If you are rebuilding an index with the `ALTER INDEX` statement, use the `SORT_IN_TEMPDB = ON`, `ONLINE = ON`, and `RESUMABLE=ON` options. With resumable indexes, log truncation is more frequent. For more information, see [ALTER INDEX (Transact-SQL)](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true).

> [!NOTE]
> For more information on other resource governance errors, see [Resource governance errors](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#resource-governance-errors).

## Next steps

- [Understand and resolve Azure SQL Database blocking problems](understand-resolve-blocking.md?view=azuresql-db&preserve-view=true#gather-blocking-information)
- [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true)
- [Troubleshoot transient connection errors in Azure SQL Database and SQL Managed Instance](troubleshoot-common-connectivity-issues.md?view=azuresql-db&preserve-view=true)
- Video: [Data Loading Best Practices on Azure SQL Database](/shows/data-exposed/data-loading-best-practices-on-azure-sql-database?WT.mc_id=dataexposed-c9-niner)
