---
title: Troubleshoot issues with the link
titleSuffix: Azure SQL Managed Instance
description: Learn how to troubleshoot common issues with a link between SQL Server and Azure SQL Managed Instance.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, danil
ms.date: 06/25/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: 
ms.topic: how-to
---
# Troubleshoot link - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to monitor and troubleshoot issues with a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance. 

You can check the state of the link with Transact-SQL (T-SQL), Azure PowerShell or the Azure CLI. If you encounter issues, you can use the error codes to troubleshoot the problem.

Many issues with creating the link can be resolved by [checking the network](#test-network-connectivity) between the two instances, and validating the [environment has been properly prepared](managed-instance-link-preparation.md) for the link. 

## Initial seeding

When establishing a link between SQL Server and Azure SQL Managed Instance, there's an initial seeding phase before data replication starts. The initial seeding phase is the longest and most expensive part of the operation. Once initial seeding completes data is synchronized, and only subsequent data changes are replicated. The time it takes for the initial seeding to complete depends on the size of data, workload intensity on the primary databases, and the speed of the link between networks of the primary and secondary replicas. 

If the speed of the link between the two instances is slower than what is necessary, the time to seed is likely to be noticeably affected. You can use the stated seeding speed, total size of data, and the link speed to estimate how long the initial seeding phase will take before data replication starts. For example, for a single 100-GB database, the initial seed phase would take about 1.2 hours if the link is capable of pushing 84 GB per hour, and if there are no other databases being seeded to a different link. If the link can only transfer 10 GB per hour, then seeding a 100-GB database can take about 10 hours. If there are multiple databases to replicate via multiple links, seeding will be executed in parallel, and, when combined with a slow link speed, the initial seeding phase might take considerably longer, especially if the parallel seeding of data from all databases exceeds the available link bandwidth.

The initial seeding phase isn't resilient to network interruptions and instance maintenance or failover operations. If bi-directional connectivity between SQL Server and SQL Managed Instance is temporarily lost, or if either SQL Server or SQL Managed instance is restarted or failed over during the initial seeding phase, seeding is restarted. 

> [!IMPORTANT]
> The initial seeding phase can take days with extremely low-speed or busy links. In this case, creating the link can time out. Creating the link is automatically canceled after 6 days. 

## Check link state

If you run into issues with a link, you can use SQL Server Management Studio (SSMS), Transact-SQL (T-SQL), Azure PowerShell or the Azure CLI to get information about the current state of the link.

Use T-SQL for a quick status details of the link state, and then use Azure PowerShell or the Azure CLI for a comprehensive information about the current state of the link. 

### [SQL Server Management Studio (SSMS)](#tab/ssms)

Link monitoring is available starting with SQL Server Management Studio (SSMS) 21.0 (preview).

To check the link state in SSMS, follow these steps: 
1. Connect to a replica that hosts the link. 
1. In **Object Explorer**, expand **Always On High Availability**, and then expand **Availability Groups**.
1. Right-click the name of the link, and then select **Properties** to open the **Link properties** window: 

   :::image type="content" source="media/managed-instance-link-troubleshoot-how-to/access-link-properties.png" alt-text="Screenshot of the right-click menu on a link in SSMS, with properties highlighted. ":::

1. The **Link properties** window displays useful information about the link, such as replica information, link state, and the endpoint certificate expiration date: 


   :::image type="content" source="media/managed-instance-link-troubleshoot-how-to/link-properties.png" alt-text="Screenshot of the link properties window in SSMS. ":::

### [Transact-SQL (T-SQL)](#tab/tsql)

Use T-SQL to determine the state of the link during the seeding phase, or after data synchronization begins. The [sys.dm_hadr_physical_seeding_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-physical-seeding-stats) DMV can be used to track the initial seeding status. The `estimate_time_complete_utc` column is based on the current `transfer_rate_bytes_per_second` and uncompressed remaining data size (when `is_compression_enabled` = 0). For Managed Instance link, data compression is used, so `estimate_time_complete_utc` is expected to be an overestimate.

Use the following T-SQL query to determine the status of the link during the seeding phase on the SQL Server or SQL Managed Instance that hosts the database seeded through the link: 

```sql
SELECT
    ag.local_database_name AS 'Local database name',
    ar.current_state AS 'Current state',
    ar.is_source AS 'Is source',
    ag.internal_state_desc AS 'Internal state desc',
    ag.database_size_bytes / 1024 / 1024 AS 'Database size MB',
    ag.transferred_size_bytes / 1024 / 1024 AS 'Transferred MB',
    ag.transfer_rate_bytes_per_second / 1024 / 1024 AS 'Transfer rate MB/s',
    ag.total_disk_io_wait_time_ms / 1000 AS 'Total Disk IO wait (sec)',
    ag.total_network_wait_time_ms / 1000 AS 'Total Network wait (sec)',
    ag.is_compression_enabled AS 'Compression',
    ag.start_time_utc AS 'Start time UTC',
    ag.estimate_time_complete_utc as 'Estimated time complete UTC',
    ar.completion_time AS 'Completion time',
    ar.number_of_attempts AS 'Attempt No'
FROM sys.dm_hadr_physical_seeding_stats AS ag
    INNER JOIN sys.dm_hadr_automatic_seeding AS ar
    ON local_physical_seeding_id = operation_id

-- Estimated seeding completion time
SELECT DISTINCT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND, start_time_utc, estimate_time_complete_utc) ,0), 108) as 'Estimated complete time'
FROM sys.dm_hadr_physical_seeding_stats
```

If the query returns no results, then the seeding process hasn't started or has already completed.

Use the following T-SQL query on the *primary* instance to check the health of the link once data synchronization begins:

```sql
DECLARE @link_name varchar(max) = '<DAGname>'
SELECT
   rs.synchronization_health_desc [Link sync health]
FROM
   sys.availability_groups ag 
   join sys.dm_hadr_availability_replica_states rs 
   on ag.group_id = rs.group_id 
WHERE 
   rs.is_local = 0 AND rs.role = 2 AND ag.is_distributed = 1 AND ag.name = @link_name 
GO
```

The query returns the following possible values: 

- no result: The query was executed on the secondary instance.
- `HEALTHY`: The link is healthy, and data is being synchronized between the replicas.
- `NOT_HEALTHY`: The link is unhealthy, and data is not synchronizing between the replicas.

### [PowerShell](#tab/powershell)

Use [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) to get link state information with PowerShell. 

Run the following sample code in Azure Cloud Shell or install the [Az.SQL](/powershell/module/az.sql) module locally. 

```powershell-interactive
$ManagedInstanceName = "<ManagedInstanceName>" # The name of your linked SQL Managed Instance
$DAGName = "<DAGName>" # distributed availability group name

# Find out the resource group name 
$ResourceGroupName = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName 

# Show link state details 
(Get-AzSqlInstanceLink -ResourceGroupName $ResourceGroupName -InstanceName $ManagedInstanceName -Name $DAGName).Databases
```

### [Azure CLI](#tab/azure-cli)

Use [az sql mi link show](/cli/azure/sql/mi/link#az-sql-mi-link-show) to get link state information with the Azure CLI.

```azurecli-interactive
# type "az" to use Azure CLI
managedInstanceName = "<ManagedInstanceName>" # The name of your linked SQL Managed Instance
dagName = "<DAGName>" # distributed availability group name
rgName = "<RGName>" # the resource group for the linked SQL Managed Instance  

# Print link state details 
az sql mi link show --resource-group $rgName --instance-name $managedInstanceName --name $dagName  
```

---

The *replicaState* value describes the current link. If the state also includes *Error* then an error occurred during the operation listed in the state. For example, *LinkCreationError* indicates that an error occurred while creating the link.

Some possible *replicaState* values are:  
- *CreatingLink*: Initial seeding
- *LinkSynchronizing*: Data replication is in progress
- *LinkFailoverInProgress*: Failover is in progress

For a complete list of link state properties, review the [Distributed Availability Groups - GET](/rest/api/sql/distributed-availability-groups/get?view=rest-sql-2024-05-01-preview&tabs=HTTP&preserve-view=true#definitions) REST API command. 

## Planned failover times out

If the secondary replica is unable to keep up with the changes on the primary replica and lags behind, planned failover can time out and fail with an error. 

To resolve this issue, follow these steps: 
1. [Check replication lag](managed-instance-link-best-practices.md#monitor-replication-lag) between the two instances.
1. If replication lag is high, wait for the secondary replica to catch up with the primary replica. You might need to perform additional troubleshooting steps if the lag persists, such as pausing workloads on the primary replica, improving link network throughput between the two instances, or increasing resource capacity on the secondary replica.
   - The easiest way to stop workloads on a SQL Server primary replica is to cut application connections to the instance. 
1. Once the secondary replica has caught up with the primary replica, try planned failover again.

## Errors initializing a link 

The following error can occur when initializing a link (Link state: `LinkInitError`): 

- [Error 41962](/sql/relational-databases/errors-events/mssqlserver-41962-database-engine-error): Operation aborted because the link wasn't initiated within 5 minutes. Check [network connectivity](#test-network-connectivity) and try again.
- [Error 41973](/sql/relational-databases/errors-events/mssqlserver-41973-database-engine-error): Link can't be established because [endpoint certificate from SQL Server](managed-instance-link-configure-how-to-scripts.md#create-a-certificate-on-sql-server-and-import-its-public-key-to-sql-managed-instance) wasn't imported into Azure SQL Managed Instance correctly. 
- [Error 41974](/sql/relational-databases/errors-events/mssqlserver-41974-database-engine-error): Link can't be established because [endpoint certificate from SQL Managed Instance](managed-instance-link-configure-how-to-scripts.md#get-the-certificate-public-key-from-sql-managed-instance-and-import-it-to-sql-server) wasn't imported into SQL Server correctly.
- [Error 41976](/sql/relational-databases/errors-events/mssqlserver-41976-database-engine-error): The availability group isn't responding. Check names and configuration parameters and try again.
- [Error 41986](/sql/relational-databases/errors-events/mssqlserver-41986-database-engine-error): Link can't be established because the connection failed or the secondary replica isn't responsive. Check names, configuration parameters, and [network connectivity](#test-network-connectivity) and then try again.
- [Error 47521](/sql/relational-databases/errors-events/mssqlserver-47521-database-engine-error): Link can't be established because the secondary server didn't receive the request. Make sure the availability group and databases are healthy on the primary server and try again.

## Errors creating a link

The following errors can occur when creating a link (Link state: `LinkCreationError`):

- [Error 41977](/sql/relational-databases/errors-events/mssqlserver-41977-database-engine-error): The target database isn't responsive. Check link parameters and try again.
- **Premature log truncation**: If the transaction log is truncated before the initial seeding finishes, you are likely to see one of the following errors: 
   - [Error 1408](/sql/relational-databases/errors-events/database-engine-events-and-errors-1000-to-1999): Replication to remote database has failed and cannot be recovered due to a missing log file overlap between the primary and secondary replica. Delete the existing link and create a new link to restart the replication.
   - [Error 1412](/sql/relational-databases/errors-events/database-engine-events-and-errors-1000-to-1999): Replication to remote database has failed and cannot be recovered due to a log file size mismatch with the primary replica. Delete the existing link and create a new link to restart the replication.
   
### Error 1412

When you first create a [link](managed-instance-link-feature-overview.md), the first part of the process seeds a full backup of the database from the primary replica to the secondary replica. After seeding of the full backup completes, the link starts to replicate data by applying differential data from the primary replica to the secondary replica. This process continues indefinitely until a failover command is issued, or the link is removed.

If a transaction log backup occurs on the primary replica during initial seeding of the full backup, the transaction log truncates. Link creation fails with error 1412 since the data in the transaction log necessary for initial seeding is no longer available. If you see error 1412 in the SQL Server error log on Azure SQL Managed Instance, then you must [drop](managed-instance-link-configure-how-to-ssms.md#drop-a-link) and recreate the link.

On builds that support it, use [trace flag 12381](#prevent-premature-log-truncation-with-trace-flag-12381) to prevent premature log truncation. On earlier builds, pause transaction log backups during the initial seeding phase.

If transaction log backups are necessary during the initial seeding phase, especially for very large databases, you can choose to [manually prevent log truncation](#manual-prevention-of-log-truncation) or automate the process with a T-SQL script to auto-pause log backups in critical phases, and when it's safe.

#### Prevent premature log truncation with trace flag 12381

On SQL Server builds that support trace flag `12381`, enable it before creating links, especially when seeding large databases or many databases in multiple-database link mode. Errors 1408 or 1412 in the SQL Managed Instance error log during seeding can indicate that required log records were truncated. Use the flag to prevent that truncation when recreating the affected link. It doesn't repair an already failed link or resolve unrelated replication errors.

While the flag is enabled, you can continue taking transaction log backups, but the required log records remain retained and aren't made reusable by log truncation. Log truncation is different from shrinking the physical log file.

> [!WARNING]
> Monitor transaction log usage, growth rate, and free disk space on SQL Server while trace flag 12381 is enabled. Retained log records can fill the transaction log or its disk. Disable the flag as soon as seeding finishes for all links being created. Don't leave it enabled for ongoing replication.

Continue regular transaction log backups after seeding finishes. For instructions to enable and disable trace flags, see [DBCC TRACEON](/sql/t-sql/database-console-commands/dbcc-traceon-transact-sql) and [DBCC TRACEOFF](/sql/t-sql/database-console-commands/dbcc-traceoff-transact-sql).

#### Manual prevention of log truncation

The steps in this section demonstrate how to open a transaction against a dummy table to prevent log truncation during the initial seeding phase. This process requires manual monitoring of the seeding progress, and careful timing to ensure that log truncation is prevented until seeding completes.

1. Monitor seeding progress. You can use the following T-SQL query to monitor the progress of initial seeding:

   ```sql
   SELECT * FROM sys.dm_exec_requests WHERE database_id = @dbId AND command = 'VDI_CLIENT_WORKER'
   ```

1. When seeding nears 90% completion, issue a `BEGIN TRAN` command on a dummy table without a commit keep a transaction open and prevent log truncation. 
1. Carefully monitor transaction log disk space to ensure it doesn't exceed storage capacity while the transaction is open.
1. Once seeding reaches 100%, complete the transaction with `COMMIT TRAN`.

For example, any time before initial seeding reaches 90%, run the following command to create a dummy table for the purpose of avoiding error 1412:

```sql
-- Create table
CREATE TABLE Prevent1412
(
    Id        INT,
    CreatedAt DATETIME
);

```

Then, when seeding nears completion, run the following command to prevent log truncation:

```sql
BEGIN TRAN;

INSERT INTO Prevent1412 (Id, CreatedAt)
VALUES (1, GETDATE());
```

> [!CAUTION]
> After the transaction begins, the transaction log no longer truncates, so carefully monitor transaction log disk space to ensure it doesn't exceed storage capacity while the transaction is open.

Once seeding completes, you can commit the transaction to truncate the log: 

```sql
COMMIT TRAN;
```

After migration completes, you can drop the dummy table: 

```sql
DROP TABLE Prevent1412;
```

#### Automate auto-pause log backups

Alternatively, you can automate the process to auto-pause log backups in critical phases when it's safe with a T-SQL script. 

The following script can be executed before the migration begins: 

```sql
-- Get last backup date
SELECT TOP 1 @lastBackupTime = b.backup_finish_date
FROM master.sys.sysdatabases d 
LEFT OUTER JOIN msdb..backupSET b
ON b.database_name = d.name
AND b.type = 'L'
WHERE d.name = @dbName
ORDER BY backup_finish_date DESC
SELECT @diffInMins = DATEDIFF(minute, @lastBackupTime, CURRENT_TIMESTAMP);

-- Get database id and group database id
SELECT @agDbId = group_database_id, @dbId = database_id FROM sys.databases WHERE name = @dbName

-- If there is no group database id, no need for checks
IF (@agDbId IS NOT NULL)
BEGIN
              -- Get last seeding start time and check if backup (VDI client) is actually running
              SELECT TOP 1 @seedingStartTime = start_time, @state = current_state, @agDbId = ag_db_id FROM sys.dm_hadr_automatic_seeding ORDER BY start_time DESC

              IF (@state = 'PENDING' OR @state = 'CHECK_IF_SEEDING_NEEDED' OR @state = 'LIMIT_CONCURRENT_BACKUPS')
                             SET @seedingStarting = 1
              ELSE
                             SET @seedingStarting = 0
              
              SELECT @backupWorkers = COUNT(*) FROM sys.dm_exec_requests WHERE database_id = @dbId AND command = 'VDI_CLIENT_WORKER'

              -- Check if seeding is done by looking at remote replica state and health
              SELECT TOP 1 @db_state = synchronization_state_desc, @db_health = synchronization_health_desc FROM sys.dm_hadr_database_replica_states WHERE database_id = @dbId AND is_local = 0

              IF (@db_state = N'SYNCHRONIZING' AND @db_health = N'HEALTHY')
              SET @seedingDone = 1
              ELSE
              SET @seedingDone = 0
END

-- If X minutes has passed since last log backup, do it, we don't want to wait anymore
IF (@alreadyFailed = 1 or @diffInMins > {set_minutes})
BEGIN
              {do_log_backup}
              SET @alreadyFailed = 1
              CONTINUE;
END

-- If seeding has started and finished take log backups
IF ((@agDbId IS NOT NULL) AND (@seedingStartTime IS NOT NULL) AND (@startTime < @seedingStartTime) AND (@seedingDone = 1))
BEGIN
              {do_log_backup}
              SET @alreadyFailed = 1
              CONTINUE;
END

-- If database is not in ag or
-- If seeding has not started or
-- If seeding is ongoing 
-- Take log backups
IF ((@agDbId IS NULL) OR (@seedingStartTime IS NULL) OR (@startTime > @seedingStartTime) OR (@seedingStarting = 1) or (@backupWorkers > 0))
BEGIN
              {do_log_backup}
              CONTINUE;
END
```


## Inconsistent state after forced failover

Following a forced [failover](managed-instance-link-failover-how-to.md), you might encounter a split-brain scenario where both replicas are in the primary role, leaving the link in an inconsistent state. This can happen if you fail over to the secondary replica during a disaster, and then the primary replica comes back online.

First, confirm you're in a split-brain scenario. You can do so by using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).

Connect to both SQL Server and SQL managed instance in SSMS, and then in **Object Explorer**, expand **Availability replicas** under the **Availability group** node in **Always On High Availability**. If two different replicas are listed as **(Primary)**, you're in a split-brain scenario. 

Alternatively, you can run the following T-SQL script on *both* SQL Server and SQL Managed Instance to check the role of the replicas:

```sql
-- Execute on SQL Server and SQL Managed Instance 
USE master
DECLARE @link_name varchar(max) = '<DAGName>'
SELECT
   ag.name [Link name], 
   rs.role_desc [Link role] 
FROM
   sys.availability_groups ag 
   JOIN sys.dm_hadr_availability_replica_states rs 
   ON ag.group_id = rs.group_id 
WHERE 
   rs.is_local = 1 AND ag.is_distributed = 1 AND ag.name = @link_name 
GO
```

If both instances list **PRIMARY** in **Link role** column, you're in a split-brain scenario.

To resolve the split brain state, first take a backup on whichever replica was the original primary. If the original primary was SQL Server, then take a [tail log backup](/sql/relational-databases/backup-restore/tail-log-backups-sql-server). If the original primary was SQL Managed Instance, then take a [copy-only full backup](/sql/relational-databases/backup-restore/copy-only-backups-sql-server). After the backup completes, set the distributed availability group to the secondary role for the replica that used to be the original primary but will now be the new secondary.
 
For example, in the event of a true disaster, assuming you've forced a failover of your SQL Server workload to Azure SQL Managed Instance, and you intend to continue running your workload on SQL Managed Instance, take a tail log backup on SQL Server, and then set the distributed availability group to the secondary role on SQL Server such as the following example:

```sql
--Execute on SQL Server 
USE master
ALTER AVAILABILITY GROUP [<DAGName>] 
SET (ROLE = SECONDARY) 
GO 
```

Next, execute a planned manual failover from SQL Managed Instance to SQL Server by using the link, such as the following example: 

```sql
--Execute on SQL Managed Instance 
USE master
ALTER AVAILABILITY GROUP [<DAGName>] FAILOVER 
GO 
```

## Expired certificate

It's possible for the certificate used for the link to expire. If the certificate expires, the link fails. To resolve this issue, [rotate the certificate](managed-instance-link-best-practices.md#rotate-certificate). 

## Database unavailable after server restart

In rare circumstances, your database might become temporarily unavailable on SQL Managed Instance after a server restart following the failover of the link. This known issue occurs when a link is dropped before Azure completes a full backup of the database after initial failover to SQL Managed Instance.

The database automatically recovers after Microsoft's intervention, but this recovery can take some time.

The following rare sequence of events leads to this issue:
1. You establish a link between SQL Server and SQL Managed Instance. 
1. You fail over the link to SQL Managed Instance, so it becomes the primary replica. 
1. You drop the link - without knowing that Azure didn't complete the first full backup of the database after failover, which is necessary to ensure the database is healthy and fully functional on SQL Managed Instance.
1. The database is initially accessible and available.
1. The server is restarted, such as for maintenance, a planned failover, or due to an unexpected outage.
1. The database becomes unavailable on SQL Managed Instance after the restart until Microsoft mitigates the issue.

To avoid this issue, wait for Azure to complete the first full backup of the database after failover before dropping the link. You can check the status of the backup by querying the `backupset` table in the `msdb` database on SQL Managed Instance by using the following T-SQL query: 

```sql
SELECT TOP (100)
    DB_NAME(DB_ID(bs.database_name)) AS [Database Name],
    CONVERT (BIGINT, bs.backup_size / 1048576) AS [Uncompressed Backup Size (MB)],
    CONVERT (BIGINT, bs.compressed_backup_size / 1048576) AS [Compressed Backup Size (MB)],
    CONVERT (NUMERIC (20, 2),
    CASE
        WHEN bs.compressed_backup_size > 0
        THEN CONVERT (FLOAT, bs.backup_size) / CONVERT (FLOAT, bs.compressed_backup_size)
        ELSE NULL
    END
    ) AS [Compression Ratio],
    bs.is_copy_only,
    -- bs.user_name, -- Applicable only for user-initiated COPY ONLY backups.
    bs.has_backup_checksums,
    DATEDIFF(SECOND, bs.backup_start_date, bs.backup_finish_date) AS [Backup Elapsed Time (sec)],
    bs.backup_finish_date AS [Backup Finish Date],
    bmf.physical_block_size
FROM msdb.dbo.backupset AS bs WITH (NOLOCK)
     INNER JOIN msdb.dbo.backupmediafamily AS bmf WITH (NOLOCK)
         ON bs.media_set_id = bmf.media_set_id
WHERE bs.[type] = 'D'
    -- AND bs.[is_copy_only] = 1  -- If you want to filter out for user initiated COPY ONLY backups.
ORDER BY bs.backup_finish_date DESC
OPTION (RECOMPILE); -- Optimize for ad hoc execution
```

## Known issues after migrating to SQL Managed Instance

Consider the following known issues after migrating to Azure SQL Managed Instance:

[!INCLUDE [known-issues-after-migration](../includes/sql-managed-instance/known-issues-after-migration.md)]

## Test network connectivity

[!INCLUDE [azure-sql-managed-instance-link-check-network](../includes/sql-managed-instance/azure-sql-managed-instance-link-check-network.md)]

## Related content

For more information on the link feature, review the following resources:

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
