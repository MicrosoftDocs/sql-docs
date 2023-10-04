---
title: "Database Checkpoints (SQL Server)"
description: Learn about checkpoints, known good points from which the SQL Server Database Engine can start applying changes contained in the log during recovery.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 07/21/2022
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "automatic checkpoints"
  - "transaction logs [SQL Server], checkpoints"
  - "logs [SQL Server], active"
  - "pages [SQL Server], dirty"
  - "MinLSN"
  - "checkpoints [SQL Server]"
  - "pages [SQL Server], flushing"
  - "dirty pages"
  - "transaction logs [SQL Server], active logs"
  - "recovery interval option [SQL Server]"
  - "buffer cache [SQL Server]"
  - "logs [SQL Server], checkpoints"
  - "Minimum Recovery LSN"
  - "flushing pages"
  - "active logs"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database checkpoints (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

A *checkpoint* creates a known good point from which the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] can start applying changes contained in the log during recovery after an unexpected shutdown or crash.

## Overview

For performance reasons, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] performs modifications to database pages in memory, in the buffer cache, and doesn't write these pages to disk after every change. Rather, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] periodically issues a *checkpoint* on each database. A checkpoint writes the current in-memory modified pages (known as *dirty pages*) and transaction log information from memory to disk, and also records the information in the transaction log.

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] supports several types of checkpoints: automatic, indirect, manual, and internal. The following table summarizes the types of checkpoints.

|Name|[!INCLUDE[tsql](../../includes/tsql-md.md)] interface|Description|
| --- | --- | --- |
|**Automatic**|EXEC sp_configure 'recovery interval', '*seconds*'|Issued automatically in the background to meet the upper time limit suggested by the **recovery interval** server configuration option. Automatic checkpoints run to completion.  Automatic checkpoints are throttled based on the number of outstanding writes and whether the [!INCLUDE[ssDE](../../includes/ssde-md.md)] detects an increase in write latency above 50 milliseconds.<br /><br />For more information, see [Configure the recovery interval Server Configuration Option](../../database-engine/configure-windows/configure-the-recovery-interval-server-configuration-option.md).|
|**Indirect**|ALTER DATABASE ... SET TARGET_RECOVERY_TIME = *target_recovery_time* { SECONDS \| MINUTES }|Issued in the background to meet a user-specified target recovery time for a given database. Beginning with [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)], the default value is 1 minute. The default is 0 for older versions, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance.<br /><br />For more information, see [Change the Target Recovery Time of a Database &#40;SQL Server&#41;](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md).|
|**Manual**|CHECKPOINT [ *checkpoint_duration* ]|Issued when you execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] CHECKPOINT command. The manual checkpoint occurs in the current database for your connection. By default, manual checkpoints run to completion. Throttling works the same way as for automatic checkpoints.  Optionally, the *checkpoint_duration* parameter specifies a requested amount of time, in seconds, for the checkpoint to complete.<br /><br />For more information, see [CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md).|
|**Internal**|None|Issued by various server operations such as backup and database-snapshot creation to guarantee that disk images match the current state of the log.|

The `-k` [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] advanced setup option enables a database administrator to throttle checkpoint I/O behavior based on the throughput of the I/O subsystem for some types of checkpoints. The `-k` setup option applies to automatic checkpoints and any otherwise unthrottled manual and internal checkpoints.

For automatic, manual, and internal checkpoints, only modifications made after the latest checkpoint need to be rolled forward during database recovery. This reduces the time required to recover a database.

> [!IMPORTANT]  
> Long-running, uncommitted transactions increase recovery time for all checkpoint types.

## <a id="InteractionBwnSettings"></a> Interaction of the TARGET_RECOVERY_TIME and 'recovery interval' options

The following table summarizes the interaction between the server-wide `sp_configure 'recovery interval'` setting and the database-specific `ALTER DATABASE ... TARGET_RECOVERY_TIME` setting.

|TARGET_RECOVERY_TIME|'recovery interval'|Type of checkpoint used|
| --- | --- | --- |
|0|0|automatic checkpoints whose target recovery interval is 1 minute.|
|0|> 0|Automatic checkpoints whose target recovery interval is specified by the user-defined setting of the `sp_configure 'recovery interval'` option.|
|> 0|Not applicable|Indirect checkpoints whose target recovery time is determined by the TARGET_RECOVERY_TIME setting, expressed in seconds.|

## <a id="AutomaticChkpt"></a> Automatic checkpoints

An automatic checkpoint occurs each time the number of log records reaches the number the [!INCLUDE[ssDE](../../includes/ssde-md.md)] estimates it can process during the time specified in the **recovery interval** server configuration option. For more information, see [Configure the recovery interval Server Configuration Option](../../database-engine/configure-windows/configure-the-recovery-interval-server-configuration-option.md).

In every database without a user-defined target recovery time, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] generates automatic checkpoints. The frequency depends on the **recovery interval** advanced server configuration option, which specifies the maximum time that a given server instance should use to recover a database during a system restart. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] estimates the maximum number of log records it can process within the recovery interval. When a database using automatic checkpoints reaches this maximum number of log records, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] issues a checkpoint on the database.

The time interval between automatic checkpoints can be *highly* variable. A database with a substantial transaction workload will have more frequent checkpoints than a database used primarily for read-only operations. Under the simple recovery model, an automatic checkpoint is also queued if the log becomes 70 percent full.

Under the simple recovery model, unless some factor is delaying log truncation, an automatic checkpoint truncates the unused section of the transaction log. By contrast, under the full and bulk-logged recovery models, once a log backup chain has been established, automatic checkpoints don't cause log truncation. For more information, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md).

After a system crash, the length of time required to recover a given database depends largely on the amount of random I/O needed to redo pages that were dirty at the time of the crash. This means that the **recovery interval** setting is unreliable. It can't determine an accurate recovery duration. Furthermore, when an automatic checkpoint is in progress, the general I/O activity for data increases significantly and unpredictably.

### <a id="PerformanceImpact"></a> Effect of recovery interval on recovery performance

For an online transaction processing (OLTP) system using short transactions, **recovery interval** is the primary factor determining recovery time. However, the **recovery interval** option doesn't affect the time required to undo a long-running transaction. Recovery of a database with a long-running transaction can take much longer than the time specified in the **recovery interval** setting.

For example, if a long-running transaction took two hours to perform updates before the server instance became disabled, the actual recovery takes considerably longer than the **recovery interval** value to recover the long transaction. For more information about the impact of a long running transaction on recovery time, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md). For more information about the recovery process, see [Restore and Recovery Overview (SQL Server)](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#TlogAndRecovery).

Typically, the default values provide optimal recovery performance. However, changing the recovery interval might improve performance in the following circumstances:

- If recovery routinely takes significantly longer than 1 minute when long-running transactions aren't being rolled back.

- If you notice that frequent checkpoints are impairing performance on a database.

If you decide to increase the **recovery interval** setting, we recommend increasing it gradually by small increments and evaluating the effect of each incremental increase on recovery performance. This approach is important because as the **recovery interval** setting increases, database recovery takes that many times longer to complete. For example, if you change **recovery interval** to 10 minutes, recovery takes approximately 10 times longer to complete than when **recovery interval** is set to 1 minute.

## <a id="IndirectChkpt"></a> Indirect checkpoints

Indirect checkpoints, introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], provide a configurable database-level alternative to automatic checkpoints. This can be configured by specifying the **target recovery time** database configuration option. For more information, see [Change the Target Recovery Time of a Database &#40;SQL Server&#41;](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md).
In the event of a system crash, indirect checkpoints provide potentially faster, more predictable recovery time than automatic checkpoints.

Indirect checkpoints offer the following advantages:

- Indirect checkpoints ensure that the number of dirty pages are below a certain threshold so the database recovery completes within the target recovery time.

  The **recovery interval** configuration option uses the number of transactions to determine the recovery time, as opposed to **indirect checkpoints** which makes use of the number of dirty pages. When indirect checkpoints are enabled on a database receiving a large number of DML operations, the background writer can start aggressively flushing dirty buffers to disk to ensure that the time required to perform recovery is within the target recovery time set of the database. This can cause additional I/O activity on certain systems, which can contribute to a performance bottleneck if the disk subsystem is operating above or nearing the I/O threshold.

- Indirect checkpoints enable you to reliably control database recovery time by factoring in the cost of random I/O during REDO. This enables a server instance to stay within an upper-bound limit on recovery times for a given database (except when a long-running transaction causes excessive UNDO times).

- Indirect checkpoints reduce checkpoint-related I/O spiking by continually writing dirty pages to disk in the background.

However, an online transactional workload on a database configured for indirect checkpoints can experience performance degradation. This is because the background writer used by indirect checkpoint sometimes increases the total write load for a server instance.

> [!IMPORTANT]  
> Indirect checkpoint is the default behavior for new databases created in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], including the `model` and `tempdb` databases.
>
> Databases that were upgraded in-place, or restored from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], will use the previous automatic checkpoint behavior unless explicitly altered to use indirect checkpoint.

### Improved indirect checkpoint scalability

Prior to [!INCLUDE[ssNoVersion](../../includes/sssql19-md.md)], you may experience non-yielding scheduler errors when there is a database that generates a large number of dirty pages, such as `tempdb`. [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] introduces improved scalability for indirect checkpoint, which should help avoid these errors on databases that have a heavy `UPDATE`/`INSERT` workload.

## <a id="EventsCausingChkpt"></a> Internal checkpoints

Internal Checkpoints are generated by various server components to guarantee that disk images match the current state of the log. Internal checkpoints are generated in response to the following events:

- Database files have been added or removed by using ALTER DATABASE.

- A database backup is taken.

- A database snapshot is created, whether explicitly or internally for DBCC CHECKDB.

- An activity requiring a database shutdown is performed. For example, AUTO_CLOSE is ON and the last user connection to the database is closed, or a database option change is made that requires a restart of the database.

- An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is stopped by stopping the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER) service. This action causes a checkpoint in each database in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- Bringing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance (FCI) offline.

## Next steps

- [Configure the recovery interval Server Configuration Option](../../database-engine/configure-windows/configure-the-recovery-interval-server-configuration-option.md)
- [Change the Target Recovery Time of a Database &#40;SQL Server&#41;](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md)
- [CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md)

## See also

- [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)
- [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)
