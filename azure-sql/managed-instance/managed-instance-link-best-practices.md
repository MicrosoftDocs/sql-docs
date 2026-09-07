---
title: Managed Instance link best practices
titleSuffix: Azure SQL Managed Instance
description: Learn about best practices when using the link feature for Azure SQL Managed Instance.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, danil
ms.date: 01/28/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---
# Managed Instance link best practices - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article outlines best practices for using the [Managed Instance link](managed-instance-link-feature-overview.md) to replicate data between [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) and your SQL Server instances hosted anywhere. The link provides near real-time data replication between the linked replicas.

## Take log backups regularly

If SQL Server is your initial primary, take the first log backup on SQL Server *after* initial seeding finishes, when the database is no longer in the *Restoring...* state on Azure SQL Managed Instance. Then take **SQL Server transaction log backups regularly** to keep the transaction log file size healthy while SQL Server is in the primary role. 

The link feature replicates data by using the [distributed availability groups](/sql/database-engine/availability-groups/windows/distributed-availability-groups) technology based on Always On availability groups. Distributed availability group data replication is based on replicating transaction log records. The primary SQL Server instance can't truncate any transaction log records from the database until they're replicated to the database on the secondary replica. If network connection issues cause transaction log record replication to be slow or blocked, the log file keeps growing on the primary instance. The intensity of workload and the network speed determine the growth speed. If a network connection outage is prolonged and the workload on primary instance is heavy, the log file can take all available storage space.

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

In this example, the database has used 76% of the available log, with an absolute log file size of approximately 27 GB (27,971 MB). The thresholds for action vary based on your workload. In the previous example, the transaction log size and the percentage of use of the log typically indicates that you should take a transaction log backup to truncate the log file and free up some space, or, you should take more frequent log backups. It could also be an indication that the transaction log truncation is being blocked by open transactions. For more on troubleshooting a transaction log in SQL Server, see [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002). For more on troubleshooting a transaction log in Azure SQL Managed Instance, see [Troubleshoot transaction log errors with Azure SQL Managed Instance](../managed-instance/troubleshoot-transaction-log-errors-issues.md?view=azuresql-mi&preserve-view=true).


> [!NOTE]
> When participating in a link, SQL Managed Instance takes automated full and transaction log backups whether or not it's the primary replica. Differential backups aren't taken, which can lead to longer restore times. 

## Match performance capacity between replicas

When you use the link feature, match the performance capacity between SQL Server and SQL Managed Instance. This matching helps you avoid performance problems if the secondary replica can't keep up with replication from the primary replica, or after failover. Performance capacity includes CPU cores (or vCores in Azure), memory, and I/O throughput.  

You can monitor the performance of replication by checking the redo queue size on the secondary replica. The redo queue size shows the number of log records that are waiting to be redone on the secondary replica. A consistently high redo queue size shows that the secondary replica can't keep up with the primary replica. You can check the redo queue size in the following ways:
 
- The `redo_queue_size` value in the [sys.dm_hadr_database_replica_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql) dynamic management view on the primary replica. 
- The `InstanceRedoLagReplicationSeconds` value in [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) on the primary replica. 

If the redo queue size is consistently high, consider increasing resources on the secondary replica.

## Monitor replication lag

Monitoring replication lag helps you determine the speed of which the secondary replica synchronizes with the primary replica. A large discrepancy indicates that the secondary replica is having trouble keeping up with the primary replica, which is typically caused by slow network throughput in the link between the two instances, mismatched resource allocation between the two replicas, or by an excessively high workload on the primary replica.

Monitoring replication lag is especially important when performing a planned failover, which requires the secondary replica to be fully synchronized with the primary replica before the failover can be executed. If replication lag is high, the failover might take longer to complete, and in some cases, it might even fail.

Use the following T-SQL query on both SQL Server and SQL Managed Instance to monitor replication lag between the replicas:

```sql
-- Execute on SQL Server and SQL Managed Instance 
USE master
DECLARE @link_name varchar(max) = '<DAGname>'
SELECT
   ag.name [Link name], 
   ars1.role_desc [Link role],
   ars2.connected_state_desc [Link connected state],
   ars2.synchronization_health_desc [Link sync health],
   drs.secondary_lag_seconds [Link replication latency (seconds)]
FROM
   sys.availability_groups ag 
   JOIN sys.dm_hadr_availability_replica_states ars1
   ON ag.group_id = ars1.group_id
   JOIN sys.dm_hadr_availability_replica_states ars2
   ON ag.group_id = ars2.group_id
   JOIN sys.dm_hadr_database_replica_states drs
   ON ars2.replica_id = drs.replica_id
WHERE 
   ag.is_distributed = 1 AND ag.name = @link_name AND ars1.is_local = 1 AND ars2.is_local = 0
GO
```

## Rotate certificate

You might need to manually rotate the certificate used to secure the database mirroring endpoint on SQL Server. Since the service manages and automatically rotates the certificate used to secure the database mirroring endpoint on SQL Managed Instance, you don't need to manually rotate it. 

### SQL Server

The certificate that you use to secure the database mirroring endpoint on SQL Server can expire. If the certificate expires, it can lead link degradation. To prevent this problem, *rotate the certificate* before it expires.

Use the following Transact-SQL (T-SQL) command to check the expiration date of the current certificate: 

```sql
-- Run on SQL Server
USE MASTER
GO
SELECT * FROM sys.certificates WHERE pvt_key_encryption_type = 'MK' 
```

If your certificate is about to expire, or has already expired, [create a new certificate](managed-instance-link-configure-how-to-scripts.md#create-a-certificate-on-sql-server-and-import-its-public-key-to-sql-managed-instance), and then alter the existing endpoint to [replace the current certificate](managed-instance-link-configure-how-to-scripts.md#alter-an-existing-endpoint). 

After you configure the endpoint to use the new certificate, you can [drop](/sql/t-sql/statements/drop-certificate-transact-sql) the expired certificate.

### SQL Managed Instance

The database mirroring endpoint certificate on SQL Managed Instance is automatically rotated periodically. You don't need to monitor the expiration date for the database mirroring endpoint certificate on SQL Managed Instance, as long as you can [validate the certificate chain on SQL Server](managed-instance-link-best-practices.md#validate-the-certificate-chain-on-sql-server) successfully.

## Validate the certificate chain on SQL Server

> [!NOTE]
> Periodically validate the certificate chain for existing links or to troubleshoot problems with a degraded link. If you're configuring a new link or recently completed the steps in sections [Get the certificate public key from SQL Managed Instance and import it to SQL Server](managed-instance-link-configure-how-to-scripts.md#get-the-certificate-public-key-from-sql-managed-instance-and-import-it-to-sql-server) and [Import Azure-trusted root certificate authority keys to SQL Server](managed-instance-link-configure-how-to-scripts.md#import-azure-trusted-root-certificate-authority-keys-to-sql-server), skip this section.

Problems with the certificate chain can degrade the link. To prevent this problem, *regularly validate the certificate chain on SQL Server*.

The following scenarios can cause problems with the certificate chain on SQL Server:
- Scheduled certificate rotation on SQL Managed Instance.
- Unintentional or accidental changes to the certificates on SQL Server, such as dropping or altering the certificate used to secure the database mirroring endpoint.

First, determine the `certificate_id` of the imported MI endpoint certificate by replacing the value of `<ManagedInstanceFQDN>` and then running the following query on SQL Server:

```sql
-- Run on SQL Server 
USE master 
SELECT name, subject, certificate_id, start_date, expiry_date 
FROM sys.certificates 
WHERE issuer_name LIKE '%Microsoft Corporation%' AND name = '<ManagedInstanceFQDN>' 
GO 
```

Next, validate the certificate by replacing the value of `<certificate_id>` from the result of the previous query and then running the following query on SQL Server:

```sql
-- Run on SQL Server 
USE master
EXEC sp_validate_certificate_ca_chain <certificate_id> 
GO 
```

A response of `Commands completed successfully. Completion time: ...` indicates the MI endpoint certificate is successfully validated. 

> [!IMPORTANT]
> The stored procedure `sp_validate_certificate_ca_chain` relies on host OS services to perform certificate validation, which might involve an online certificate revocation check. If the host OS isn't configured to access the internet, the execution fails even if the certificate chain is valid. 

If you encounter an error, the most reliable mitigation is to restore the certificate chain by first [dropping](/sql/t-sql/statements/drop-certificate-transact-sql) all certificates created in sections [Get the certificate public key from SQL Managed Instance and import it to SQL Server](managed-instance-link-configure-how-to-scripts.md#get-the-certificate-public-key-from-sql-managed-instance-and-import-it-to-sql-server) and [Import Azure-trusted root certificate authority keys to SQL Server](managed-instance-link-configure-how-to-scripts.md#import-azure-trusted-root-certificate-authority-keys-to-sql-server), and then reimporting them again.

## Add startup trace flags

In SQL Server, there are two trace flags (`-T1800` and `-T9567`) that, when added as startup parameters, can optimize the performance of data replication through the link. See [Enable startup trace flags](managed-instance-link-preparation.md#enable-startup-trace-flags) to learn more. 

## Use synchronous commit with caution 

The default commit mode for the link is asynchronous. While it's possible to change the commit mode to synchronous, it's not recommended and not necessary to secure against potential data loss. 

During a planned linked failover, replication temporarily switches to synchronous commit mode until the failover completes. After failover, the commit mode switches back to asynchronous, even if it's explicitly set to synchronous commit mode before the failover.

Using synchronous commit mode for the link can affect the performance of your primary replica, especially if there's high network latency between the replicas. In synchronous commit mode, transactions on the primary replica must wait for confirmation that the transaction log records are hardened on the secondary replica before the transaction can be committed on the primary. This waiting time increases with higher network latency, which can lead to increased transaction response times and reduced throughput on the primary replica.

## Related content

To use the link: 
- [Prepare environment for the Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Fail over the link](managed-instance-link-failover-how-to.md)
- [Migrate with the link](managed-instance-link-migrate.md)
- [Troubleshoot issues with the link](managed-instance-link-troubleshoot-how-to.md)

To learn more about the link: 
- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)

For other replication and migration scenarios, consider:
- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)

