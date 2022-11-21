---
title: "The Transaction Log (SQL Server) | Microsoft Docs"
description: Learn about the transaction log. Every SQL Server database records all transactions and database modifications that you need if there is a system failure.
ms.custom: ""
ms.date: "10/23/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction logs [SQL Server], about"
  - "databases [SQL Server], transaction logs"
  - "logs [SQL Server], transaction logs"
ms.assetid: d7be5ac5-4c8e-4d0a-b114-939eb97dac4d
author: "MashaMSFT"
ms.author: "mathoma"
---
# The Transaction Log (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
Every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database has a transaction log that records all transactions and the database modifications made by each transaction.
  
The transaction log is a critical component of the database. If there is a system failure, you will need that log to bring your database back to a consistent state. 

For information about the transaction log architecture and internals, see the [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md).

> [!WARNING] 
> Never delete or move this log unless you fully understand the ramifications of doing so. 

> [!TIP]
> Known good points from which to begin applying transaction logs during database recovery are created by checkpoints. For more information, see [Database Checkpoints (SQL Server)](../../relational-databases/logs/database-checkpoints-sql-server.md).  
  
## Operations supported by the transaction log  
 The transaction log supports the following operations:  
  
-   Individual transaction recovery.  
-   Recovery of all incomplete transactions when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started. 
-   Rolling a restored database, file, filegroup, or page forward to the point of failure.  
-   Supporting transactional replication.  
-   Supporting high availability and disaster recovery solutions: [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], database mirroring, and log shipping.

### Individual transaction recovery
If an application issues a `ROLLBACK` statement, or if the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects an error such as the loss of communication with a client, the log records are used to roll back the modifications made by an incomplete transaction. 

### Recovery of all incomplete transactions when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started
If a server fails, the databases may be left in a state where some modifications were never written from the buffer cache to the data files, and there may be some modifications from incomplete transactions in the data files. When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, it runs a recovery of each database. Every modification recorded in the log that may not have been written to the data files is rolled forward. Every incomplete transaction found in the transaction log is then rolled back to make sure the integrity of the database is preserved. For more information, see [Restore and Recovery Overview (SQL Server)](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#TlogAndRecovery).

### Rolling a restored database, file, filegroup, or page forward to the point of failure
After a hardware loss or disk failure affecting the database files, you can restore the database to the point of failure. You first restore the last full database backup and the last differential database backup, and then restore the subsequent sequence of the transaction log backups to the point of failure. 

As you restore each log backup, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reapplies all the modifications recorded in the log to roll forward all the transactions. When the last log backup is restored, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] then uses the log information to roll back all transactions that were not complete at that point. For more information, see [Restore and Recovery Overview (SQL Server)](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#TlogAndRecovery).

### Supporting transactional replication
The Log Reader Agent monitors the transaction log of each database configured for transactional replication and copies the transactions marked for replication from the transaction log into the distribution database. For more information, see [How Transactional Replication Works](/previous-versions/sql/sql-server-2008-r2/ms151706(v=sql.105)).

### Supporting high availability and disaster recovery solutions
The standby-server solutions, [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], database mirroring, and log shipping, rely heavily on the transaction log. 

In an **[!INCLUDE[ssHADR](../../includes/sshadr-md.md)] scenario**, every update to a database, the primary replica, is immediately reproduced in separate, full copies of the database, the secondary replicas. The primary replica sends each log record immediately to the secondary replicas, that applies the incoming log records to availability group databases, continually rolling it forward. For more information, see [Always On Failover Cluster Instances](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)

In a **log shipping scenario**, the primary server sends the transaction log backups of the primary database to one or more destinations. Each secondary server restores the log backups to its local secondary database. For more information, see [About Log Shipping](../../database-engine/log-shipping/about-log-shipping-sql-server.md). 

In a **database mirroring scenario**, every update to a database, the principal database, is immediately reproduced in a separate, full copy of the database, the mirror database. The principal server instance sends each log record immediately to the mirror server instance, which applies the incoming log records to the mirror database, continually rolling it forward. For more information, see [Database Mirroring](../../database-engine/database-mirroring/database-mirroring-sql-server.md).

##  <a name="Characteristics"></a>Transaction log characteristics
Characteristics of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] transaction log: 
-  The transaction log is implemented as a separate file or set of files in the database. The log cache is managed separately from the buffer cache for data pages, which results in simple, fast, and robust code within the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. For more information, see [Transaction Log Physical Architecture](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).

-  The format of log records and pages is not constrained to follow the format of data pages.

-  The transaction log can be implemented in several files. The files can be defined to expand automatically by setting the `FILEGROWTH` value for the log. This reduces the potential of running out of space in the transaction log, while at the same time reducing administrative overhead. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41; File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

-  The mechanism to reuse the space within the log files is quick and has minimal effect on transaction throughput.

For information about the transaction log architecture and internals, see the [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md).

##  <a name="Truncation"></a> Transaction log truncation  
Log truncation frees space in the log file for reuse by the transaction log. You must regularly truncate your transaction log to keep it from filling the allotted space. Several factors can delay log truncation, so monitoring log size matters. Some operations can be minimally logged to reduce their impact on transaction log size.  
 
Log truncation deletes inactive [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) from the logical transaction log of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, freeing space in the logical log for reuse by the Physical transaction log. If a transaction log is never truncated, it will eventually fill all the disk space allocated to physical log files.  
  
To avoid running out of space, unless log truncation is delayed for some reason, truncation occurs automatically after the following events:  
  
- Under the simple recovery model, after a checkpoint.  
- Under the full recovery model or bulk-logged recovery model, if a checkpoint has occurred since the previous backup, truncation occurs after a log backup (unless it is a copy-only log backup).  
- When you first create a database using the FULL recovery model, the transaction log will be reused as needed (similar to a SIMPLE recovery database), up until the time you create a full database backup.
  
 For more information, see [Factors that can delay log truncation](#FactorsThatDelayTruncation), later in this topic.  
  
> [!NOTE]
> Log truncation does not reduce the size of the physical log file. To reduce the physical size of a physical log file, you must shrink the log file. For information about shrinking the size of the physical log file, see [Manage the Size of the Transaction Log File](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md).  
> However, keep in mind [Factors that can delay log truncation](#FactorsThatDelayTruncation). If the storage space is required again after a log shrink, the transaction log will grow again and by doing that, introduce performance overhead during log grow operations.
  
##  <a name="FactorsThatDelayTruncation"></a> Factors that can delay log truncation  
 When log records remain active for a long time, transaction log truncation is delayed, and the transaction log can fill up, as we mentioned earlier in this long topic.  
  
> [!IMPORTANT]
> For information about how to respond to a full transaction log, see [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md).  
  
 Really, Log truncation can be delayed by a variety of reasons. Learn what, if anything, is preventing your log truncation by querying the **log_reuse_wait** and **log_reuse_wait_desc** columns of the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. The following table describes the values of these columns.  
  
|log_reuse_wait value|log_reuse_wait_desc value|Description|  
|----------------------------|----------------------------------|-----------------|  
|0|NOTHING|Currently there are one or more reusable [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).|  
|1|CHECKPOINT|No checkpoint has occurred since the last log truncation, or the head of the log has not yet moved beyond a [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch). (All recovery models)<br /><br /> This is a routine reason for delaying log truncation. For more information, see [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md).|  
|2|LOG_BACKUP|A log backup is required before the transaction log can be truncated. (Full or bulk-logged recovery models only)<br /><br /> When the next log backup is completed, some log space might become reusable.|  
|3|ACTIVE_BACKUP_OR_RESTORE|A data backup or a restore is in progress (all recovery models).<br /><br /> If a data backup is preventing log truncation, canceling the backup operation might help the immediate problem.|  
|4|ACTIVE_TRANSACTION|A transaction is active (all recovery models):<br /><br /> A long-running transaction might exist at the start of the log backup. In this case, freeing the space might require another log backup. Note that long-running transactions prevent log truncation under all recovery models, including the simple recovery model, under which the transaction log is generally truncated on each automatic checkpoint.<br /><br /> A transaction is deferred. A *deferred transaction* is effectively an active transaction whose rollback is blocked because of some unavailable resource. For information about the causes of deferred transactions and how to move them out of the deferred state, see [Deferred Transactions &#40;SQL Server&#41;](../../relational-databases/backup-restore/deferred-transactions-sql-server.md).<br /> <br /> Long-running transactions might also fill up tempdb's transaction log. Tempdb is used implicitly by user transactions for internal objects such as work tables for sorting, work files for hashing, cursor work tables, and row versioning. Even if the user transaction includes only reading data (`SELECT` queries), internal objects may be created and used under user transactions. Then the tempdb transaction log can be filled.|  
|5|DATABASE_MIRRORING|Database mirroring is paused, or under high-performance mode, the mirror database is significantly behind the principal database. (Full recovery model only)<br /><br /> For more information, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).|  
|6|REPLICATION|During transactional replications, transactions relevant to the publications are still undelivered to the distribution database. (Full recovery model only)<br /><br /> For information about transactional replication, see [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md).|  
|7|DATABASE_SNAPSHOT_CREATION|A database snapshot is being created. (All recovery models)<br /><br /> This is a routine, and typically brief, cause of delayed log truncation.|  
|8|LOG_SCAN|A log scan is occurring. (All recovery models)<br /><br /> This is a routine, and typically brief, cause of delayed log truncation.|  
|9|AVAILABILITY_REPLICA|A secondary replica of an availability group is applying transaction log records of this database to a corresponding secondary database. (Full recovery model)<br /><br /> For more information, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).|  
|10|-|For internal use only|  
|11|-|For internal use only|  
|12|-|For internal use only|  
|13|OLDEST_PAGE|If a database is configured to use indirect checkpoints, the oldest page on the database might be older than the checkpoint [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch). In this case, the oldest page can delay log truncation. (All recovery models)<br /><br /> For information about indirect checkpoints, see [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md).|  
|14|OTHER_TRANSIENT|This value is currently not used.|  
|16|XTP_CHECKPOINT|An In-Memory OLTP checkpoint needs to be performed.For memory-optimized tables, an automatic checkpoint is taken when transaction log file becomes bigger than 1.5 GB since the last checkpoint (includes both disk-based and memory-optimized tables)<br /> For more information see [Checkpoint Operation for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/checkpoint-operation-for-memory-optimized-tables.md) and [Logging and Checkpoint process for In-Memory Optimized Tables] (https://blogs.msdn.microsoft.com/sqlcat/2016/05/20/logging-and-checkpoint-process-for-memory-optimized-tables-2/)
  
##  <a name="MinimallyLogged"></a> Operations that can be minimally logged  
*Minimal logging* involves logging only the information that is required to recover the transaction without supporting point-in-time recovery. This topic identifies the operations that are minimally logged under the bulk-logged [recovery model](../backup-restore/recovery-models-sql-server.md) (as well as under the simple recovery model, except when a backup is running).  
  
> [!NOTE]
> Minimal logging is not supported for memory-optimized tables.  
  
> [!NOTE]
> Under the full [recovery model](../backup-restore/recovery-models-sql-server.md), all bulk operations are fully logged. However, you can minimize logging for a set of bulk operations by switching the database to the bulk-logged recovery model temporarily for bulk operations. 
> Minimal logging is more efficient than full logging, and it reduces the possibility of a large-scale bulk operation filling the available transaction log space during a bulk transaction. However, if the database is damaged or lost when minimal logging is in effect, you cannot recover the database to the point of failure.  
  
 The following operations, which are fully logged under the full recovery model, are minimally logged under the simple and bulk-logged recovery model:  
  
-   Bulk import operations ([bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md), and [INSERT... SELECT](../../t-sql/statements/insert-transact-sql.md)). For more information about when bulk import into a table is minimally logged, see [Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).  
  
When transactional replication is enabled, `BULK INSERT` operations are fully logged even under the Bulk Logged recovery model.  
  
-   [SELECT INTO](../../t-sql/queries/select-into-clause-transact-sql.md) operations.  
  
When transactional replication is enabled, `SELECT INTO` operations are fully logged even under the Bulk Logged recovery model.  
  
-   Partial updates to large value data types, using the `.WRITE` clause in the [UPDATE](../../t-sql/queries/update-transact-sql.md) statement when inserting or appending new data. Note that minimal logging is not used when existing values are updated. For more information about large value data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
-   [WRITETEXT](../../t-sql/queries/writetext-transact-sql.md) and [UPDATETEXT](../../t-sql/queries/updatetext-transact-sql.md) statements when inserting or appending new data into the **text**, **ntext**, and **image** data type columns. Note that minimal logging is not used when existing values are updated.  
  
    > [!WARNING]
    > The `WRITETEXT` and `UPDATETEXT` statements are **deprecated**; avoid using them in new applications.  
  
-   If the database is set to the simple or bulk-logged recovery model, some index DDL operations are minimally logged whether the operation is executed offline or online. The minimally logged index operations are as follows:  
  
    -   [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) operations (including indexed views).  
  
    -   [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) REBUILD or DBCC DBREINDEX operations.  
  
        > [!WARNING]
        > The `DBCC DBREINDEX` statement is **deprecated**; Do not use it in new applications.  
  
        > [!NOTE]
        > Index build operations use minimial logging but may be delayed when there is a concurrently executing backup. This delay is caused by the synchronization requirements of minimally logged buffer pool pages when using the simple or bulk-logged recovery model. 
      
    -   [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) new heap rebuild (if applicable). Index page deallocation during a `DROP INDEX` operation is **always** fully logged.

##  <a name="RelatedTasks"></a> Related tasks  
**Managing the transaction log**  
  
-   [Manage the Size of the Transaction Log File](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md)  
  
-   [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)  
  
**Backing Up the Transaction Log (Full Recovery Model)**  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  

-   [Back Up the Transaction Log When the Database Is Damaged (SQL Server)](../../relational-databases/backup-restore/back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)

**Restoring the Transaction Log (Full Recovery Model)**  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
## See also  
[SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)   
[Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md)   
[Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md)   
[Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)     
[Restore and Recovery Overview (SQL Server)](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#TlogAndRecovery)      
[Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md)   
[View or Change the Properties of a Database](../../relational-databases/databases/view-or-change-the-properties-of-a-database.md)   
[Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)  
[Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)    
[sys.dm_db_log_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md)  
[sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)    
  
