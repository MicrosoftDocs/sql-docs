---
title: Log Replay Service overview
description: Learn about the Log Replay Service with Azure SQL Managed Instance
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: migration
ms.topic: conceptual
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
---

# Log Replay Service with Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Log Replay Service (LRS) to migrate to Azure SQL Managed Instance. LRS is a free of charge cloud service enabled for Azure SQL Managed Instance based on SQL Server log-shipping technology.

To get started, review [Migrate with LRS](log-replay-service-migrate.md). 

## When to use Log Replay Service

[Azure Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance) and LRS use the same underlying migration technology and APIs. LRS further enables complex custom migrations and hybrid architectures between on-premises SQL Server and SQL Managed Instance.
When you can't use Azure Database Migration Service for migration, you can use LRS directly with PowerShell, Azure CLI cmdlets, or APIs to manually build and orchestrate database migrations to SQL Managed Instance. 

Consider using LRS in the following cases:

- You need more control for your database migration project.
- There's little tolerance for downtime during migration cutover.
- The Database Migration Service executable file can't be installed to your environment.
- The Database Migration Service executable file doesn't have file access to your database backups.
- No access to the host OS is available, or there are no administrator privileges.
- You can't open network ports from your environment to Azure.
- Network throttling, or proxy blocking issues exist in your environment.
- Backups are stored directly to Azure Blob Storage through the `TO URL` option.
- You need to use differential backups.

The following sources are supported: 

- SQL Server hosted on-premises 
- SQL Server on Virtual Machines  
- Amazon Web Services (AWS) EC2
- Amazon Web Services (AWS) RDS
- Compute Engine (Google Cloud Platform - GCP)  
- Cloud SQL for SQL Server (Google Cloud Platform – GCP) 

> [!NOTE]
> - We recommend automating the migration of databases from SQL Server to SQL Managed Instance by using Database Migration Service. Consider using LRS to orchestrate migrations when Database Migration Service doesn't fully support your scenarios.
> - LRS is the only method to restore differential backups on managed instance. It isn't possible to manually restore differential backups on managed instance, nor to manually set the `NORECOVERY` mode using T-SQL.
>

## How it works

Building a custom solution to migrate databases to the cloud with LRS requires several orchestration steps, as shown in the diagram and a table later in this section.

Migration consists of taking database backups on SQL Server, and copying backup files to Azure Blob Storage. Full, log, and differential backups are supported. The LRS cloud service is then used to restore backup files from Azure Blob Storage to SQL Managed Instance. Blob Storage serves as an intermediary storage between SQL Server and SQL Managed Instance.

LRS monitors Blob Storage for any new differential or log backups added after the full backup has been restored. LRS then automatically restores these new files. You can use the service to monitor the progress of backup files being restored to SQL Managed Instance, and stop the process if necessary.

LRS doesn't require a specific naming convention for backup files. It scans all files placed on Azure Blob Storage and constructs the backup chain from reading the file headers only. Databases are in a **restoring** state during the migration process. Databases are restored in [NORECOVERY](/sql/t-sql/statements/restore-statements-transact-sql#comparison-of-recovery-and-norecovery) mode, so they can't be used for read or write workloads until the migration process completes.

If you're migrating several databases, you need to:

- Place backup files for each database in a separate folder on Azure Blob Storage in a flat-file structure. For example, use separate database folders: `bolbcontainer/database1/files`, `blobcontainer/database2/files`, etc.
- Don't use nested folders inside database folders as this structure isn't supported. For example, don't use subfolders: `blobcontainer/database1/subfolder/files`.
- Start LRS separately for each database.
- Specify different URI paths to separate database folders on Azure Blob Storage. 

While having `CHECKSUM` enabled for backups is not required, it is highly recommended. Restoring databases without `CHECKSUM` will take longer, as SQL Managed Instance performs an integrity check on backups restored without `CHECKSUM` enabled. 

To get started, review [Migrate with LRS](log-replay-service-migrate.md). 


### Autocomplete versus continuous mode migration

You can start LRS in either **autocomplete** or **continuous** mode.

Use autocomplete mode in cases when you have the entire backup chain generated in advance, and when you don't plan to add any more files once the migration has been started. This migration mode is recommended for passive workloads that don't require data catch-up. Upload all backup files to the Azure Blob Storage, and start the autocomplete mode migration. The migration will complete automatically when the last specified backup file has been restored. Migrated database will become available for read and write access on SQL Managed Instance.

In case that you plan to keep adding new backup files while migration is in progress, use continuous mode. This mode is recommended for active workloads requiring data catch-up. Upload the currently available backup chain to Azure Blob Storage, start the migration in continuous mode, and keep adding new backup files from your workload as needed. The system will periodically scan Azure Blob Storage folder and restore any new log or differential backup files found. When you're ready to cutover, stop the workload on your SQL Server, generate and upload the last backup file. Ensure that the last backup file has restored by watching that the final log-tail backup is shown as restored on SQL Managed Instance. Then, initiate manual cutover. The final cutover step makes the database come online and available for read and write access on SQL Managed Instance.

After LRS is stopped, either automatically through autocomplete, or manually through cutover, you can't resume the restore process for a database that was brought online on SQL Managed Instance. For example, once migration completes, you're no longer able to restore more differential backups for an online database. To restore more backup files after migration completes, you need to delete the database from the managed instance and restart the migration from the beginning. 


## Migration workflow

Typical migration workflow is shown in the image below, and steps outlined in the table.

Autocomplete mode needs to be used only when all backup chain files are available in advance. This mode is recommended for passive workloads for which no data catch-up is required.

Continuous mode migration needs to be used when you don't have the entire backup chain in advance, and when you plan to add new backup files once the migration is in progress. This mode is recommended for active workloads for which data catch-up is required.

:::image type="content" source="./media/log-replay-service-migrate/log-replay-service-conceptual.png" alt-text="Diagram that explains the Log Replay Service orchestration steps for SQL Managed Instance." border="false":::
	
| Operation | Details |
| :----------------------------- | :------------------------- |
| **1. Copy database backups from SQL Server to Blob Storage**. | Copy full, differential, and log backups from SQL Server to a Blob Storage container by using [AzCopy](/azure/storage/common/storage-use-azcopy-v10) or [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/). <br /><br />Use any file names. LRS doesn't require a specific file-naming convention.<br /><br />Use a separate folder for each database when migrating several databases. |
| **2. Start LRS in the cloud**. | You can start the service with PowerShell ([start-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/start-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_start cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-start)). Choose between **autocomplete** or **continuous** migration modes. <br /><br /> Start LRS separately for each database that points to a backup folder on Blob Storage. <br /><br /> After the service starts, it will take backups from the Blob Storage container and start restoring them to SQL Managed Instance.<br /><br /> When started in autocomplete mode, LRS restores all backups until the specified last backup file. All backup files must be uploaded in advance, and it isn't possible to add any new backup files while migration is in progress. This mode is recommended for passive workloads for which no data catch-up is required. <br /><br /> When started in continuous mode, LRS restores all the  backups initially uploaded and then watches for any new files uploaded to the folder. The service will continuously apply logs based on the log sequence number (LSN) chain until it's stopped manually. This mode is recommended for active workloads for which data catch-up is required. |
| **2.1. Monitor the operation's progress**. | You can monitor progress of the restore operation with PowerShell ([get-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/get-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_show cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-show)). |
| **2.2. Stop the operation if required (optional)**. | If you need to stop the migration process, use PowerShell ([stop-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/stop-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_stop](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-stop)). <br /><br /> Stopping the operation deletes the database that you're restoring to SQL Managed Instance. After you stop an operation, you can't resume LRS for a database. You need to restart the migration process from the beginning. |
| **3. Cut over to the cloud when you're ready**. | If LRS was started in autocomplete mode, the migration will automatically complete once the specified last backup file has been restored. <br /><br />  If LRS was started in continuous mode, stop the application and workload. Take the last log-tail backup and upload it to Azure Blob Storage. Ensure that the last log-tail backup has been restored on managed instance. Complete the cutover by initiating an LRS `complete` operation with PowerShell ([complete-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/complete-azsqlinstancedatabaselogreplay)) or the Azure CLI [az_sql_midb_log_replay_complete](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-complete). This operation stops LRS and brings the database online for read and write workloads on SQL Managed Instance. <br /><br /> Repoint the application connection string from SQL Server to SQL Managed Instance. You'll need to orchestrate this step yourself, either through a manual connection string change in your application, or automatically (for example, if your application can read the connection string from a property, or a database). |

### Migrating large databases

If migrating large databases of several terabytes in size, consider the following:
- Single LRS job can run for a maximum of 30 days. On expiry of this timeframe, the job will be automatically canceled.
- In the case of long-running jobs, system updates will interrupt and prolong migration jobs. It's highly recommended to use [maintenance window]( ../database/maintenance-window.md) to schedule planned system updates. Plan your migration around the scheduled maintenance window.
- Migration jobs interrupted by system updates will be automatically suspended and resumed for General Purpose managed instances, and restarted for Business Critical managed instances. These updates will affect the timeframe of your migration.
- To increase the upload speed of your SQL Server backup files to Azure Blob Storage, provided there's a sufficient network bandwidth from your infrastructure, consider using parallelization with multiple threads.


## Starting the migration

You start the migration by starting LRS. You can start the service in either autocomplete or continuous mode. For specific details, review [Migrate with LRS](log-replay-service-migrate.md). 

When you use autocomplete mode, the migration completes automatically when the last of the specified backup files have been restored. This option requires the entire backup chain to be available in advance, and uploaded to Azure Blob Storage. It doesn't allow adding new backup files while migration is in progress. This option requires the start command to specify the filename of the last backup file. This mode is recommended for passive workloads for which data catch-up isn't required.

When you use continuous mode, the service continuously scans Azure Blob Storage folder and restores any new backup files that keep getting added while migration is in progress. The migration completes only after the manual cutover has been requested. Continuous mode migration needs to be used when you don't have the entire backup chain in advance, and when you plan to add new backup files once the migration is in progress. This mode is recommended for active workloads for which data catch-up is required.

Plan to complete a single LRS migration job within a maximum of 30 days. On expiry of this timeframe, the LRS job will be automatically canceled.

> [!NOTE]
> When migrating multiple databases, LRS must be started separately for each database pointing to the full URI path of Azure Blob storage container and the individual database folder.
> 




## Limitations

Consider the following limitations of LRS: 

- During the migration process, databases being migrated can't be used for read-only access on SQL Managed Instance.
- Configure [maintenance window](../database/maintenance-window.md) to allow scheduling of system updates at a specific day/time. Plan to run and complete migrations outside of the scheduled maintenance window.
- Database backups taken without `CHECKSUM` take longer to restore than databases with `CHECKSUM` enabled. 
- The SAS token that LRS uses must be generated for the entire Azure Blob Storage container, and it must have **Read** and **List** permissions only. For example, if you grant **Read**, **List** and **Write** permissions, LRS won't be able to start because of the extra **Write** permission.
- Using SAS tokens created with permissions set through defining a [stored access policy](/rest/api/storageservices/define-stored-access-policy) isn't supported. Follow the instructions in this article to manually specify **Read** and **List** permissions for the SAS token.
- Backup files containing % and $ characters in the file name can't be consumed by LRS. Consider renaming such file names.
- Backup files for different databases must be placed in separate folders on Blob Storage in a flat-file structure. Nested folders inside individual database folders aren't supported.
- If using autocomplete mode, the entire backup chain needs to be available in advance on Azure Blob Storage. It isn't possible to add new backup files in autocomplete mode. Use continuous mode if you need to add new backup files while migration is in progress.
- LRS must be started separately for each database pointing to the full URI path containing an individual database folder. 
- LRS can support up to 100 simultaneous restore processes per single managed instance.
- Single LRS job can run for the maximum of 30 days, after which it will be automatically canceled.

> [!TIP]
> If you require database to be R/O accessible during the migration, a much longer time frame to perform the migration, and the best possible minimum downtime, consider the [link feature for Managed Instance](managed-instance-link-feature-overview.md) as a recommended migration solution in these cases.
>


## Next steps

To get started, review [Migrate with LRS](log-replay-service-migrate.md). 

- Learn more about [migrating to SQL Managed Instance using the link feature](managed-instance-link-feature-overview.md).
- Learn more about [migrating from SQL Server to SQL Managed instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).
- Learn more about [differences between SQL Server and SQL Managed Instance](transact-sql-tsql-differences-sql-server.md).
- Learn more about [best practices to cost and size workloads migrated to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs).
