---
title: Overview of Log Replay Service
description: Learn about Log Replay Service with Azure SQL Managed Instance.
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: azure-sql-managed-instance
ms.subservice: migration
ms.topic: conceptual
ms.custom:
  - sql-migration-content
---

# Overview of Log Replay Service with Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of Log Replay Service (LRS), which you can use to migrate databases from SQL Server to Azure SQL Managed Instance. LRS is a free cloud service available for Azure SQL Managed Instance and based on SQL Server log-shipping technology. 

Since LRS restores standard SQL Server backup files, you can use it to migrate from SQL Server *hosted anywhere* (either on-premises, or any cloud) to Azure SQL Managed Instance.

To start your migration with LRS, review [Migrate databases by using Log Replay Service](log-replay-service-migrate.md). 

## When to use Log Replay Service

[Azure Database Migration Service](/azure/dms/tutorial-sql-server-managed-instance-online), the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension), and LRS all use the same underlying migration technology and APIs. LRS further enables complex custom migrations and hybrid architectures between on-premises SQL Server instances and SQL Managed Instance deployments. 

When you can't use Azure Database Migration Service, or the Azure SQL extension for migration, you can use LRS directly with PowerShell, Azure CLI cmdlets, or APIs to manually build and orchestrate database migrations to SQL Managed Instance. 

Consider using LRS in the following cases, when:

- You need more control for your database migration project.
- There's little tolerance for downtime during migration cutover.
- The Database Migration Service executable file can't be installed to your environment.
- The Database Migration Service executable file doesn't have file access to your database backups.
- The Azure SQL migration extension can't be installed to your environment, or it can't access your database backups. 
- No access to the host operating system is available, or there are no administrator privileges.
- You can't open network ports from your environment to Azure.
- Network throttling, or proxy blocking issues, exist in your environment.
- Backups are stored directly in Azure Blob Storage accounts through the `TO URL` option.
- You need to use differential backups.

Since LRS works by restoring standard SQL Server backup files, it should support migrations from any source. The following sources have been tested: 

- SQL Server on-premises/box
- SQL Server on Virtual Machines
- Amazon EC2 (Elastic Compute Cloud)
- Amazon RDS (Relational Database Service) for SQL Server
- Google Compute Engine
- Cloud SQL for SQL Server - GCP (Google Cloud Platform)
- Alibaba Cloud RDS for SQL Server

If you encounter unexpected issues migrating from an unlisted source, open a support ticket for assistance.

> [!NOTE]
> - We recommend that you automate the migration of databases from SQL Server to Azure SQL Managed Instance by using the Azure SQL migration extension for Azure Data Studio. Consider using LRS to orchestrate migrations when the Azure SQL migration extension doesn't fully support your scenarios.
> - LRS is the only method to restore differential backups on managed instances. It isn't possible to manually restore differential backups on managed instances or to manually set the `NORECOVERY` mode by using T-SQL.

## How LRS works

Building a custom solution to migrate databases to the cloud with LRS requires several orchestration steps, as shown in the diagram and table later in this section.

Migration consists of taking database backups on SQL Server, and copying backup files to an Azure Blob Storage account. Full, log, and differential backups are supported. You then use the LRS cloud service to restore backup files from the Azure Blob Storage account to SQL Managed Instance. The Blob Storage account serves as intermediary storage for backup files between SQL Server and SQL Managed Instance.

LRS monitors your Blob Storage account for any new differential or log backups that are added after the full backup has been restored. LRS then automatically restores these new files. You can use the service to monitor the progress of backup files that are being restored to SQL Managed Instance, and stop the process if necessary.

LRS doesn't require a specific naming convention for backup files. It scans all files placed in the Azure Blob Storage account and constructs the backup chain from reading the file headers only. Databases are in a *restoring* state during the migration process. Databases are restored in [NORECOVERY](/sql/t-sql/statements/restore-statements-transact-sql#comparison-of-recovery-and-norecovery) mode, so they can't be used for read or write workloads until the migration process finishes.

If you're migrating several databases, you need to:

- Place backup files for each database in a separate folder on the Blob Storage account in a flat-file structure. For example, use separate database folders: *blobcontainer/database1/files*, *blobcontainer/database2/files*, and so on.
- Don't use nested folders inside database folders, because nested-folder structure isn't supported. For example, don't use subfolders such as *blobcontainer/database1/subfolder/files*.
- Start LRS separately for each database.
- Specify different URI paths to separate database folders on the Blob Storage account. 

Although having `CHECKSUM` enabled for backups isn't required, we highly recommend it. Restoring databases without `CHECKSUM` takes longer, because SQL Managed Instance performs an integrity check on backups that are restored without `CHECKSUM` enabled. 

For more information, see [Migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service](log-replay-service-migrate.md). 

> [!CAUTION]
> Taking backups on SQL Server with `CHECKSUM` enabled is highly recommended as there is a risk to restoring a corrupt database to Azure without it. 


### Autocomplete vs. continuous mode migration

You can start LRS in either *autocomplete* or *continuous* mode.

Use *autocomplete* mode when you have the entire backup chain generated in advance, and when you don't plan to add any more files after the migration has been started. This migration mode is recommended for passive workloads that don't require data catch-up. Upload all backup files to the Blob Storage account, and start the autocomplete mode migration. The migration will finish automatically when the last specified backup file has been restored. The migrated database will become available for read/write access on SQL Managed Instance.

If you plan to keep adding new backup files while the migration is in progress, use *continuous* mode. We recommend this mode for active workloads that require data catch-up. Upload the currently available backup chain to the Blob Storage account, start the migration in continuous mode, and keep adding new backup files from your workload as needed. The system will periodically scan the Azure Blob Storage folder and restore any new log or differential backup files it finds. 

When you're ready for the cutover, stop the workload on your SQL Server instance, generate the last backup file, and then upload it. Ensure that the last backup file has been restored by verifying that the final log-tail backup is shown as restored on SQL Managed Instance. Then, initiate manual cutover. The final cutover step makes the database come online and available for read/write access on SQL Managed Instance.

After LRS is stopped, either automatically through autocomplete, or manually through cutover, you can't resume the restore process for a database that was brought online on SQL Managed Instance. For example, after the migration finishes, you're no longer able to restore more differential backups for an online database. To restore more backup files after the migration finishes, you need to delete the database from the managed instance and restart the migration from the beginning. 


## Migration workflow

A typical migration workflow is shown in the following image, and the steps are outlined in the table.

You need to use autocomplete mode only when all backup chain files are available in advance. We recommend this mode for passive workloads for which no data catch-up is required.

Use continuous mode migration when you don't have the entire backup chain in advance, and when you plan to add new backup files after the migration is in progress. We recommend this mode for active workloads for which data catch-up is required.

:::image type="content" source="./media/log-replay-service-migrate/log-replay-service-conceptual.png" alt-text="Diagram that illustrates the Log Replay Service orchestration steps for SQL Managed Instance." border="false":::
	
| Operation | Details |
| :----------------------------- | :------------------------- |
| **1. Copy database backups from the SQL Server instance to the Blob Storage account**. | Copy full, differential, and log backups from the SQL Server instance to the Blob Storage container by using [AzCopy](/azure/storage/common/storage-use-azcopy-v10) or [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/). <br /><br />Use any file names. LRS doesn't require a specific file-naming convention.<br /><br />Use a separate folder for each database when you're migrating several databases. |
| **2. Start LRS in the cloud**. | You can start the service with PowerShell ([start-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/start-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_start cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-start)). Choose between autocomplete or continuous migration mode. <br /><br /> Start LRS separately for each database that points to a backup folder on the Blob Storage account. <br /><br /> After the service starts, it takes backups from the Blob Storage container and starts restoring them to SQL Managed Instance.<br /><br /> When LRS is started in autocomplete mode, it restores all backups through the specified last backup file. All backup files must be uploaded in advance, and it isn't possible to add any new backup files while the migration is in progress. This mode is recommended for passive workloads for which no data catch-up is required. <br /><br /> When LRS is started in continuous mode, it restores all the backups that were initially uploaded and then watches for any new files that were uploaded to the folder. The service continuously applies logs based on the log sequence number (LSN) chain until it's stopped manually. We recommend this mode for active workloads for which data catch-up is required. |
| **2.1. Monitor the operation's progress**. | You can monitor the progress of the ongoing restore operation with PowerShell ([get-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/get-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_show cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-show)). To track additional details on a failed request, use the PowerShell command [Get-AzSqlInstanceOperation](/powershell/module/az.sql/get-azsqlinstanceoperation) or use Azure CLI command [az sql mi op show](/cli/azure/sql/mi/op#az-sql-mi-op-show). |
| **2.2. Stop the operation if required (optional)**. | If you need to stop the migration process, use PowerShell ([stop-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/stop-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_stop](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-stop)). <br /><br /> Stopping the operation deletes the database that you're restoring to SQL Managed Instance. After you stop an operation, you can't resume LRS for a database. You need to restart the migration process from the beginning. |
| **3. Cut over to the cloud when you're ready**. | If LRS was started in autocomplete mode, the migration automatically finishes after the specified last backup file has been restored. <br /><br />  If LRS was started in continuous mode, stop the application and workload. Take the last log-tail backup and upload it to the Azure Blob Storage deployment. Ensure that the last log-tail backup has been restored on the managed instance. Complete the cutover by initiating an LRS `complete` operation with PowerShell ([complete-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/complete-azsqlinstancedatabaselogreplay)) or the Azure CLI [az_sql_midb_log_replay_complete](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-complete). This operation stops LRS and brings the database online for read/write workloads on SQL Managed Instance. <br /><br /> Repoint the application connection string from the SQL Server instance to SQL Managed Instance. You need to orchestrate this step yourself, either through a manual connection string change in your application, or automatically (for example, if your application can read the connection string from a property, or a database). |

> [!IMPORTANT]
> After the cutover, SQL Managed Instance with Business Critical service tier can take significantly longer than General Purpose to be available as three secondary replicas have to be seeded for the availability group. The operation duration depends on the size of data. For more information, see [Management operations duration](/azure/azure-sql/managed-instance/management-operations-overview#duration).

### Migrating large databases

If you're migrating large databases of several terabytes in size, consider the following:
- A single LRS job can run for a maximum of 30 days. When this period expires, the job is automatically canceled.
- For long-running jobs, system updates will interrupt and prolong migration jobs. We highly recommend that you use a [maintenance window]( maintenance-window.md) to schedule planned system updates. Plan your migration around the scheduled maintenance window.
- Migration jobs that are interrupted by system updates are automatically suspended and resumed for General Purpose managed instances, and they're restarted for Business Critical managed instances. These updates will affect the timeframe of your migration.
- To increase the upload speed of your SQL Server backup files to the Blob Storage account, if your infrastructure has sufficient network bandwidth, consider using parallelization with multiple threads.

## Start the migration

You start the migration by starting LRS. You can start the service in either autocomplete or continuous mode. For specific details, review [Migrate with LRS](log-replay-service-migrate.md). 

* **Autocomplete mode**. When you use autocomplete mode, the migration finishes automatically when the last of the specified backup files have been restored. This option:
  * Requires the entire backup chain to be available in advance and uploaded to the Azure Blob Storage account. 
  * Doesn't allow adding new backup files while the migration is in progress. 
  * Requires the start command to specify the file name of the last backup file. 

  We recommend using autocomplete mode for passive workloads for which data catch-up isn't required.

* **Continuous mode**. When you use continuous mode, the service continuously scans the Azure Blob Storage folder and restores any new backup files that are added while the migration is in progress. 

  The migration finishes only after the manual cutover has been requested. 

  You need to use continuous mode migration when you don't have the entire backup chain in advance, and when you plan to add new backup files after the migration is in progress. 
  
  We recommend using continuous mode for active workloads for which data catch-up is required.

Plan to finish a single LRS migration job within a maximum of 30 days. When this period expires, the LRS job is automatically canceled.

> [!NOTE]
> When you're migrating multiple databases, LRS must be started separately for each database that points to the full URI path of the Azure Blob storage container and the individual database folder.

## Limitations of LRS

Consider the following limitations of LRS: 

- Only database `.bak`, `.log`, and `.diff` files are supported by LRS. Dacpac and bacpac files are not supported. 
- During the migration process, databases that are being migrated can't be used for read-only access on SQL Managed Instance.
- You have to configure a [maintenance window](maintenance-window.md) to allow scheduling of system updates at a specific day and time. Plan to run and finish migrations outside the scheduled maintenance window.
- Database backups that are taken without `CHECKSUM` take longer to restore than do database backups with `CHECKSUM` enabled. 
- The shared access signature (SAS) token that LRS uses must be generated for the entire Azure Blob Storage container, and it must have Read and List permissions only. For example, if you grant Read, List, and Write permissions, LRS won't be able to start because of the extra Write permission.
- Using SAS tokens created with permissions that are set through defining a [stored access policy](/rest/api/storageservices/define-stored-access-policy) isn't supported. Follow the instructions in this article to manually specify Read and List permissions for the SAS token.
- You must place backup files for different databases in separate folders on the Blob Storage account in a flat-file structure. Nesting folders inside database folders isn't supported.
- If you're using autocomplete mode, the entire backup chain needs to be available in advance on the Blob Storage account. It isn't possible to add new backup files in autocomplete mode. Use continuous mode if you need to add new backup files while migration is in progress.
- You must start LRS separately for each database that points to the full URI path that contains an individual database folder. 
- The backup URI path, container name or folder names should not contain `backup` or `backups` as these are reserved keywords.
- When starting multiple Log Replay restores in parallel, targeting the same storage container, ensure that the same valid SAS token is provided for every restore operation.
- LRS can support up to 100 simultaneous restore processes per single managed instance.
- A single LRS job can run for a maximum of 30 days, after which it will be automatically canceled.
- While it's possible to use an Azure Storage account behind a firewall, extra configuration is necessary, and the storage account and managed instance must either be in the same region, or two paired regions. Review [Configure firewall](log-replay-service-migrate.md#configure-azure-storage-behind-a-firewall) to learn more. 
- The maximum number of databases you can restore in parallel is 200 per single subscription. In some cases, it's possible to increase this limit by opening a support ticket. 

   

> [!TIP]
> If you require a database to be read-only accessible during the migration, with a much longer time frame for performing the migration and with minimal downtime, consider using the [Azure SQL Managed Instance link](managed-instance-link-feature-overview.md) feature as a recommended migration solution.
>


## Next steps

- For more information, see [Migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service](log-replay-service-migrate.md). 

- Learn more about [migrating databases to SQL Managed Instance by using the link feature](managed-instance-link-feature-overview.md).
- Learn more about [migrating databases from SQL Server to SQL Managed Instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).
- Learn more about the [differences between SQL Server and SQL Managed Instance](transact-sql-tsql-differences-sql-server.md).
- Learn more about [best practices to cost and size workloads that are migrated to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs).
