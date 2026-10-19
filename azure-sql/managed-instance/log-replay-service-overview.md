---
title: Overview of Log Replay Service
description: Learn about Log Replay Service with Azure SQL Managed Instance.
author: danimir
ms.author: danil
ms.reviewer: vanto
ms.date: 01/26/2026
ms.service: azure-sql-managed-instance
ms.subservice: migration
ms.topic: concept-article
ms.collection:
  - sql-migration-content
---

# Overview of Log Replay Service with Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of Log Replay Service (LRS), which you can use to migrate databases from SQL Server to Azure SQL Managed Instance. LRS is a free cloud service available for Azure SQL Managed Instance and is based on SQL Server log-shipping technology.

> [!NOTE]  
> You can now migrate your SQL Server instance enabled by Azure Arc to Azure SQL Managed Instance directly through the Azure portal. For more information, see [Migrate to Azure SQL Managed Instance](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance).

Since LRS restores standard SQL Server backup files, you can use it to migrate from SQL Server *hosted anywhere* (either on-premises or any cloud) to Azure SQL Managed Instance.

To start your migration with LRS, review [Migrate databases from SQL Server by using Log Replay Service](log-replay-service-migrate.md).

> [!IMPORTANT]  
> Before you migrate databases to the **Business Critical** service tier, consider [these limitations](log-replay-service-migrate.md#limitations-when-migrating-to-the-business-critical-service-tier), which don't apply to the **General Purpose** service tier.

## When to use Log Replay Service

[Azure Database Migration Service](/azure/dms/tutorial-sql-server-managed-instance-online) and LRS use the same underlying migration technology and APIs. LRS further enables complex custom migrations and hybrid architectures between on-premises SQL Server instances and SQL Managed Instance deployments.

When you can't use Azure Database Migration Service, or the Azure SQL extension for migration, you can use LRS directly with PowerShell, Azure CLI cmdlets, or APIs to manually build and orchestrate database migrations to SQL Managed Instance.

Consider using LRS in the following cases:

- You need more control for your database migration project.
- There's little tolerance for downtime during migration cutover.
- You can't install the Database Migration Service executable file to your environment.
- The Database Migration Service executable file doesn't have file access to your database backups.
- You can't install the Azure SQL migration extension to your environment, or it can't access your database backups.
- You have no access to the host operating system or administrator privileges.
- You can't open network ports from your environment to Azure.
- Network throttling or proxy blocking issues exist in your environment.
- Backups are stored directly in Azure Blob Storage accounts through the `TO URL` option.
- You need to use differential backups.

Since LRS works by restoring standard SQL Server backup files, it supports migrations from any source. The following sources have been tested:

- SQL Server on-premises/box
- SQL Server on Virtual Machines
- Amazon EC2 (Elastic Compute Cloud)
- Amazon RDS (Relational Database Service) for SQL Server
- Google Compute Engine
- Cloud SQL for SQL Server - GCP (Google Cloud Platform)
- Alibaba Cloud RDS for SQL Server

If you encounter unexpected issues migrating from an unlisted source, open a support ticket for assistance.

> [!NOTE]  
>
> - LRS is the only method to restore differential backups on SQL managed instances. It's not possible to manually restore differential backups on managed instances or to manually set the `NORECOVERY` mode by using T-SQL.

## How LRS works

Building a custom solution to migrate databases to the cloud with LRS requires several orchestration steps, as shown in the diagram and table later in this section.

Migration consists of taking database backups on SQL Server, and copying backup files to an Azure Blob Storage account. LRS supports full, log, and differential backups. You then use the LRS cloud service to restore backup files from the Azure Blob Storage account to SQL Managed Instance. The Blob Storage account serves as intermediary storage for backup files between SQL Server and SQL Managed Instance.

LRS monitors your Blob Storage account for any new differential or log backups that you add after the full backup is restored. LRS then automatically restores these new files. You can use the service to monitor the progress of backup files that are being restored to SQL Managed Instance, and stop the process if necessary.

LRS doesn't require a specific naming convention for backup files. It scans all files placed in the Azure Blob Storage account and constructs the backup chain from reading the file headers only. Databases are in a *restoring* state during the migration process. LRS restores databases in [NORECOVERY](/sql/t-sql/statements/restore-statements-transact-sql#comparison-of-recovery-and-norecovery) mode, so they can't be used for read or write workloads until the migration process finishes.

If you're migrating several databases, you need to:

- Place backup files for each database in a separate folder on the Blob Storage account in a flat-file structure. For example, use separate database folders: *blobcontainer/database1/files*, *blobcontainer/database2/files*, and so on.
- Don't use nested folders inside database folders, because nested-folder structure isn't supported. For example, don't use subfolders such as *blobcontainer/database1/subfolder/files*.
- Start LRS separately for each database.
- Specify different URI paths to separate database folders on the Blob Storage account.

Although having `CHECKSUM` enabled for backups isn't required, we highly recommend it. Restoring databases without `CHECKSUM` takes longer, because SQL Managed Instance performs an integrity check on backups that are restored without `CHECKSUM` enabled.

For more information, see [Migrate databases from SQL Server by using Log Replay Service](log-replay-service-migrate.md).

> [!CAUTION]  
> Taking backups on SQL Server with `CHECKSUM` enabled is highly recommended as there's a risk to restoring a corrupt database to Azure without it.

### Autocomplete vs. continuous mode migration

You can start LRS in either *autocomplete* or *continuous* mode.

Use *autocomplete* mode when you have the entire backup chain generated in advance, and when you don't plan to add anymore files after the migration has started. This migration mode is recommended for passive workloads that don't require data catch-up. Upload all backup files to the Blob Storage account, and start the autocomplete mode migration. The migration finishes automatically when the last specified backup file is restored. The migrated database becomes available for read/write access on SQL Managed Instance.

If you plan to keep adding new backup files while the migration is in progress, use *continuous* mode. We recommend this mode for active workloads that require data catch-up. Upload the currently available backup chain to the Blob Storage account, start the migration in continuous mode, and keep adding new backup files from your workload as needed. The system periodically scans the Azure Blob Storage folder and restores any new log or differential backup files it finds.

When you're ready for the cutover, stop the workload on your SQL Server instance, generate the last backup file, and then upload it. Ensure the last backup file is restored by verifying that the final log-tail backup is shown as restored on SQL Managed Instance. Then, initiate manual cutover. The final cutover step makes the database come online and available for read/write access on SQL Managed Instance.

After LRS is stopped, either automatically through autocomplete, or manually through cutover, you can't resume the restore process for a database that you brought online on SQL Managed Instance. For example, after the migration finishes, you're no longer able to restore more differential backups for an online database. To restore more backup files after the migration finishes, you need to delete the database from the managed instance and restart the migration from the beginning.

## Migration workflow

The image in this section shows a typical migration workflow while the table outlines the steps.

Use autocomplete mode only when all backup chain files are available in advance. We recommend this mode for passive workloads that don't require data catch-up.

Use continuous mode migration when you don't have the entire backup chain in advance, and when you plan to add new backup files after the migration is in progress. We recommend this mode for active workloads that require data catch-up.

:::image type="content" source="media/log-replay-service-migrate/log-replay-service-conceptual.png" alt-text="Diagram that illustrates the Log Replay Service orchestration steps for SQL Managed Instance.":::

| Operation | Details |
| --- | --- |
| **1. Copy database backups from the SQL Server instance to the Blob Storage account**. | Copy full, differential, and log backups from the SQL Server instance to the Blob Storage container by using [AzCopy](/azure/storage/common/storage-use-azcopy-v10) or [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/).<br /><br />Use any file names. LRS doesn't require a specific file-naming convention.<br /><br />Use a separate folder for each database when you're migrating several databases. |
| **2. Start LRS in the cloud**. | Start the service with PowerShell ([start-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/start-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_start cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-start)). Choose between autocomplete or continuous migration mode.<br /><br />Start LRS separately for each database that points to a backup folder on the Blob Storage account.<br /><br />After the service starts, it takes backups from the Blob Storage container and starts restoring them to SQL Managed Instance.<br /><br />When you start LRS in autocomplete mode, it restores all backups through the specified last backup file. You must upload all backup files in advance, and you can't add any new backup files while the migration is in progress. This mode is recommended for passive workloads that don't require data catch-up.<br /><br />When you start LRS in continuous mode, it restores all the backups that you initially uploaded and then watches for any new files that you upload to the folder. The service continuously applies logs based on the log sequence number (LSN) chain until it's stopped manually. We recommend this mode for active workloads that require data catch-up. |
| **2.1. Monitor the operation's progress**. | Monitor the progress of the ongoing restore operation with PowerShell ([get-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/get-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_show cmdlets](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-show)). To track additional details on a failed request, use the PowerShell command [Get-AzSqlInstanceOperation](/powershell/module/az.sql/get-azsqlinstanceoperation) or use Azure CLI command [az sql mi op show](/cli/azure/sql/mi/op#az-sql-mi-op-show). |
| **2.2. Stop the operation if required (optional)**. | If you need to stop the migration process, use PowerShell ([stop-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/stop-azsqlinstancedatabaselogreplay)) or the Azure CLI ([az_sql_midb_log_replay_stop](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-stop)).<br /><br />Stopping the operation deletes the database that you're restoring to SQL Managed Instance. After you stop an operation, you can't resume LRS for a database. You need to restart the migration process from the beginning. |
| **3. Cut over to the cloud when you're ready**. | If you start LRS in autocomplete mode, the migration automatically finishes after the specified last backup file is restored.<br /><br />If you start LRS in continuous mode, stop the application and workload. Take the last log-tail backup and upload it to the Azure Blob Storage deployment. Ensure that the last log-tail backup is restored on the SQL managed instance. Complete the cutover by initiating an LRS `complete` operation with PowerShell ([complete-azsqlinstancedatabaselogreplay](/powershell/module/az.sql/complete-azsqlinstancedatabaselogreplay)) or the Azure CLI [az_sql_midb_log_replay_complete](/cli/azure/sql/midb/log-replay#az-sql-midb-log-replay-complete). This operation stops LRS and brings the database online for read/write workloads on SQL Managed Instance.<br /><br />Repoint the application connection string from the SQL Server instance to SQL Managed Instance. You need to orchestrate this step yourself, either through a manual connection string change in your application, or automatically (for example, if your application can read the connection string from a property, or a database). |

> [!IMPORTANT]  
> After the cutover, SQL Managed Instance with **Business Critical** service tier can take significantly longer than **General Purpose** to be available as three secondary replicas have to be seeded for the availability group. The operation duration depends on the size of data. For more information, see [Management operations duration](/azure/azure-sql/managed-instance/management-operations-overview#duration).

<a id="migrating-large-databases"></a>

### Migrate large databases

If you're migrating large databases of several terabytes in size, consider the following points:

- A single LRS job can run for a maximum of 30 days. When this period expires, the job is automatically canceled.
- For long-running jobs, system updates can interrupt and prolong migration jobs. We highly recommend that you use a [maintenance window](maintenance-window.md) to schedule planned system updates. Plan your migration around the scheduled maintenance window.
- Migration jobs that are interrupted by system updates automatically suspend and resume for **General Purpose** SQL managed instances, and they restart for **Business Critical** SQL managed instances. These updates affect the timeframe of your migration.
- To increase the upload speed of your SQL Server backup files to the Blob Storage account, if your infrastructure has sufficient network bandwidth, consider using parallelization with multiple threads.

## Start the migration

Start the migration by starting LRS. You can start the service in either autocomplete or continuous mode. For specific details, review [Migrate databases from SQL Server by using Log Replay Service](log-replay-service-migrate.md).

- **Autocomplete mode**. When you use autocomplete mode, the migration finishes automatically when the last of the specified backup files is restored. This option:
  - Requires the entire backup chain to be available in advance and uploaded to the Azure Blob Storage account.
  - Doesn't allow adding new backup files while the migration is in progress.
  - Requires the start command to specify the file name of the last backup file.

  We recommend using autocomplete mode for passive workloads for which data catch-up isn't required.

- **Continuous mode**. When you use continuous mode, the service continuously scans the Azure Blob Storage folder and restores any new backup files that are added while the migration is in progress.

  The migration finishes only after you request the manual cutover.

  Use continuous mode migration when you don't have the entire backup chain in advance, and when you plan to add new backup files after the migration is in progress.

  We recommend using continuous mode for active workloads for which data catch-up is required.

Plan to finish a single LRS migration job within a maximum of 30 days. When this period expires, the LRS job is automatically canceled.

> [!NOTE]  
> When you're migrating multiple databases, each database must be in its own folder. Start LRS separately for each database, pointing to the full URI path of the Azure Blob Storage container and the individual database folder. Nested folders inside database folders aren't supported.

## Limitations of LRS

For information, review [limitations](log-replay-service-migrate.md#limitations) when using LRS.

## Related content

- [Migrate databases from SQL Server by using Log Replay Service](log-replay-service-migrate.md)
- [Overview of the Managed Instance link](managed-instance-link-feature-overview.md)
- [Compare LRS with Managed Instance link](log-replay-service-compare-mi-link.md)
- [Migrate databases from SQL Server to SQL Managed Instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md)
- [differences between SQL Server and SQL Managed Instance](transact-sql-tsql-differences-sql-server.md)
- [best practices to cost and size workloads that are migrated to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
