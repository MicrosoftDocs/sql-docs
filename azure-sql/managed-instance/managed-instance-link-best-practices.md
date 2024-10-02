---
title: Managed Instance link best practices
titleSuffix: Azure SQL Managed Instance
description: Learn about best practices when using the link feature for Azure SQL Managed Instance.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, danil
ms.date: 10/09/2024
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---
# Managed Instance link best practices - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article outlines best practices when using the [Managed Instance link](managed-instance-link-feature-overview.md) to replicate data between [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) and your SQL Server instances hosted anywhere, providing near real-time data replication between the linked replicas.

## Take log backups regularly

If SQL Server is your initial primary, it's important to take the first log backup on SQL Server *after* initial seeding completes, when the database is no longer in the *Restoring...* state on Azure SQL Managed Instance. Then take **SQL Server transaction log backups regularly** to maintain a healthy transaction log file size while SQL Server is in the primary role. 

The link feature replicates data using the [distributed availability groups](/sql/database-engine/availability-groups/windows/distributed-availability-groups) technology based on Always On availability groups. Data replication with distributed availability groups is based on replicating transaction log records. No transaction log records can be truncated from the database on the primary SQL Server instance until they're replicated to the database on the secondary replica. If transaction log record replication is slow or blocked due to network connection issues, the log file keeps growing on the primary instance. Growth speed depends on the intensity of workload and the network speed. If there's a prolonged network connection outage and heavy workload on primary instance, the log file can take all available storage space.

Taking regular transaction log backups truncates the transaction log and minimizes the risk of running out of space on the primary SQL Server instance due to log file growth. No extra action is necessary when SQL Managed Instance is the primary since [log backups are already taken automatically](automated-backups-overview.md). By taking log backups regularly on your SQL Server primary, you make your database more resilient to unplanned log growth events. Consider scheduling daily log backup tasks by using a SQL Server Agent job.

You can use a Transact-SQL (T-SQL) script to back up the log file, such as the sample provided in this section. Replace the placeholders in the sample script with name of your database, name and path of the backup file, and the description.

To back up your transaction log, use the following sample Transact-SQL (T-SQL) script on SQL Server: 

```sql
-- Execute on SQL Server
-- Take log backup
BACKUP LOG [<DatabaseName>]
TO DISK = N'<DiskPathandFileName>'
WITH NOFORMAT, NOINIT,
NAME = N'<Description>', SKIP, NOREWIND, NOUNLOAD, COMPRESSION, STATS = 1
```

Use the following Transact-SQL (T-SQL) command to check the log spaced used by your database on SQL Server: 

```sql
-- Execute on SQL Server
DBCC SQLPERF(LOGSPACE); 
```

The query output looks like the following example for sample database `tpcc`:

:::image type="content" source="./media/managed-instance-link-best-practices/database-log-file-size.png" alt-text="Screenshot with results of the command showing log file size and space used":::

In this example, the database has used 76% of the available log, with an absolute log file size of approximately 27 GB (27,971 MB). The thresholds for action vary based on your workload. In the previous example, the transaction log size and the percentage of use of the log is typically an indication that you should take a transaction log backup to truncate the log file and free up some space, or, you should take more frequent log backups. It could also be an indication that the transaction log truncation is being blocked by open transactions. For more on troubleshooting a transaction log in SQL Server, see [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002). For more on troubleshooting a transaction log in Azure SQL Managed Instance, see [Troubleshoot transaction log errors with Azure SQL Managed Instance](../managed-instance/troubleshoot-transaction-log-errors-issues.md?view=azuresql-mi&preserve-view=true).


> [!NOTE]
> When participating in a link, automated full and transaction log backups are taken from SQL Managed Instance whether or not it's the primary replica. Differential backups aren't taken, which can lead to longer restore times. 

## Match performance capacity between replicas

When you're using the link feature, it's important to match performance capacity between SQL Server and SQL Managed Instance to avoid performance issues if the secondary replica can't keep up with replication from the primary replica, or after failover. Performance capacity includes CPU cores (or vCores in Azure), memory, and I/O throughput. 

You can check the performance of replication with the redo queue size on the secondary replica. The redo queue size indicates the number of log records that are waiting to be redone on the secondary replica. A consistently high redo queue size indicates that the secondary replica can't keep up with the primary replica. You can check the redo queue size in the following ways:
 
- The `redo_queue_size` value in the [sys.dm_hadr_database_replica_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql) dynamic management view on the primary replica. 
- The `InstanceRedoLagReplicationSeconds` value in [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) on the primary replica. 

If the redo queue size is consistently high, consider increasing resources on the secondary replica.

## Rotate certificate 

It's possible for the certificate that you use to secure the database mirroring endpoint to expire, which can lead to the degradation of the link. To prevent this issue, *rotate the certificate* before it expires.

Use the following Transact-SQL (T-SQL) command to check the expiration date of the current certificate: 

```sql
-- Run on SQL Server
USE MASTER
GO
SELECT * FROM sys.certificates WHERE pvt_key_encryption_type = 'MK' 
```

If your certificate is about to expire, or has already expired, you can [create a new certificate](managed-instance-link-configure-how-to-scripts.md#create-a-certificate-on-sql-server-and-import-its-public-key-to-sql-managed-instance), and then alter the existing endpoint to [replace the current certificate](managed-instance-link-configure-how-to-scripts.md#alter-an-existing-endpoint). 

Once the endpoint is configured to use the new certificate, you can [drop](/sql/t-sql/statements/drop-certificate-transact-sql) the expired certificate. 


## Add startup trace flags

In SQL Server, there are two trace flags (`-T1800` and `-T9567`) that, when added as startup parameters, can optimize the performance of data replication through the link. See [Enable startup trace flags](managed-instance-link-preparation.md#enable-startup-trace-flags) to learn more. 

## Related content

To use the link: 
- [Prepare environment for the Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Fail over the link](managed-instance-link-failover-how-to.md)
- [Migrate with the link](managed-instance-link-migrate.md)

To learn more about the link: 
- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)

For other replication and migration scenarios, consider:

- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)
