---
title: "SQL Server to Azure SQL Managed Instance: Migration guide"
description: This guide teaches you to migrate your SQL Server databases to Azure SQL Managed Instance.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mathoma, danil
ms.date: 06/26/2024
ms.service: sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sql-migration-content
---
# Migration guide: SQL Server to Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../../includes/appliesto-sqlmi.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your user databases from SQL Server to Azure SQL Managed Instance.

Review the [Prerequisites](../pre-migration.md#prerequisites), and complete the [Pre-migration](../pre-migration.md) steps, before continuing.

## Migrate

After you complete the steps for the [pre-migration stage](../pre-migration.md), you're ready to perform the schema and data migration.

Migrate your data using your chosen [migration method](overview.md#compare-migration-options).

This section provides general migration steps for the following recommended migration options:

- Managed instance link
- Log Replay Service (LRS)
- Azure SQL migration extension for Azure Data Studio - migration with near-zero downtime.
- Native `RESTORE DATABASE FROM URL` - uses native backups from SQL Server and requires some downtime.

SQL Managed Instance targets user scenarios requiring mass database migration from on-premises or Azure VM database implementations. These are the optimal choice when you need to lift and shift the back end of the applications that regularly use instance level and/or cross-database functionalities. If this is your scenario, you can move an entire instance to a corresponding environment in Azure without the need to rearchitect your applications.

To move SQL instances, you need to plan carefully:

- The migration of all databases that need to be collocated (ones running on the same instance).
- The migration of instance-level objects that your application depends on, including logins, credentials, SQL Agent jobs and operators, and server-level triggers.

SQL Managed Instance is a managed service that allows you to delegate some of the regular DBA activities to the platform as they're built in. Therefore, some instance-level data doesn't need to be migrated, such as maintenance jobs for regular backups or Always On configuration, as [high availability](/azure/azure-sql/database/high-availability-sla-local-zone-redundancy) is built in.

### Azure Data Studio

This section provides high-level steps to migrate from SQL Server to Azure SQL Managed Instance with minimal downtime by using the Azure SQL migration extension in Azure Data Studio. For detailed instructions, see [Tutorial: Migrate SQL Server to Azure SQL Managed Instance online in Azure Data Studio](/azure/dms/tutorial-sql-server-managed-instance-online-ads).

To migrate with Azure Data Studio, follow these steps:

1. [Download and install Azure Data Studio](/azure-data-studio/download-azure-data-studio) and the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Launch the **Migrate to Azure SQL Migration** wizard in the extension in Azure Data Studio.
1. Select databases for assessment and view migration readiness or issues (if any). Additionally, collect performance data and get right-sized Azure recommendation.
1. Select your Azure account and your target Azure SQL Managed Instance from your subscription.
1. Select the location of your database backups. Your database backups can either be located on an on-premises network share or in Azure Blob Storage container.
1. Create a new Azure Database Migration Service using the wizard in Azure Data Studio. If you previously created an Azure Database Migration Service using Azure Data Studio, you can reuse the same if desired.
1. *Optional*: If your backups are on an on-premises network share, download and install [self-hosted integration runtime](https://www.microsoft.com/download/details.aspx?id=39717) on a machine that can connect to the source SQL Server, and the location containing the backup files.
1. Start the database migration and monitor the progress in Azure Data Studio. You can also monitor the progress under the Azure Database Migration Service resource in Azure portal.
1. Complete the cutover.
   1. Stop all incoming transactions to the source database.
   1. Make application configuration changes to point to the target database in Azure SQL Managed Instance.
   1. Take any tail log backups for the source database in the backup location specified.
   1. Ensure all database backups have the status Restored in the monitoring details page.
   1. Select Complete cutover in the monitoring details page.

### Managed Instance link

This section provides high-level steps to migrate from SQL Server to Azure SQL Managed Instance with minimal downtime by using the Managed Instance link. For detailed instructions, review [Migrate with the link](/azure/azure-sql/managed-instance/managed-instance-link-migrate).

To migrate with the link, follow these steps:

1. Create your target SQL Managed Instance: [Azure portal](/azure/azure-sql/managed-instance/instance-create-quickstart), [PowerShell](/azure/azure-sql/managed-instance/scripts/create-configure-managed-instance-powershell), [Azure CLI](/azure/azure-sql/managed-instance/scripts/create-configure-managed-instance-cli).
1. [Prepare your environment for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation).
1. Configure the link with [SSMS](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-ssms) or [scripts](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-scripts).
1. Stop the workload.
1. Validate data on the target instance.
1. [Fail over the link](/azure/azure-sql/managed-instance/managed-instance-link-failover-how-to).

### Log Replay Service (LRS)

This section provides high-level steps to migrate from SQL Server to Azure SQL Managed Instance with minimal downtime by using the Log Replay Service (LRS). For detailed instructions, review [Migrate databases from SQL Server by using Log Replay Service](/azure/azure-sql/managed-instance/log-replay-service-migrate).

To migrate with LRS, follow these steps:

1. Create an [Azure storage account](/azure/storage/common/storage-account-create?tabs=azure-portal) with a [blob container](/azure/storage/blobs/storage-quickstart-blobs-portal).
1. Authenticate to your Blob Storage storage account using an SAS token or a managed identity and validate access.
1. Be sure to [configure your folder structure correctly](/azure/azure-sql/managed-instance/log-replay-service-migrate#migrate-multiple-databases) if you plan to migrate multiple databases.
1. Upload your backups to your storage account by either copying your backups, or taking backups directly by using the [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url).
1. Determine if you want to run LRS in autocomplete or continuous mode.
1. Start LRS.
1. Monitor migration progress.
1. Complete the migration (if in continuous mode).

### Back up and restore

One key capability of Azure SQL Managed Instance that enables quick and easy database migration is the native restore to SQL managed instance of database backup (`.bak`) files stored in [Azure Storage](https://azure.microsoft.com/services/storage/). Backing up and restoring are asynchronous operations based on the size of your database.

The following diagram provides a high-level overview of the process:

:::image type="content" source="media/guide/migration-restore.png" alt-text="Diagram shows SQL Server with an arrow labeled BACKUP / Upload to URL flowing to Azure Storage and a second arrow labeled RESTORE from URL flowing from Azure Storage to a SQL Managed Instance." lightbox="media/guide/migration-restore.png":::

> [!NOTE]  
> The time to take the backup, upload it to Azure storage, and perform a native restore operation to Azure SQL Managed Instance is based on the size of the database. Factor a sufficient downtime to accommodate the operation for large databases.

The following table provides more information regarding the methods you can use depending on
source SQL Server version you're running:

| Step | SQL Engine and version | Backup/restore method |
| --- | --- | --- |
| **Put backup to Azure Storage** | Before 2012 SP1 CU2 | Upload `.bak` file directly to Azure Storage |
| | 2012 SP1 CU2 - 2016 | Direct backup using deprecated [WITH CREDENTIAL](/sql/t-sql/statements/restore-statements-transact-sql) syntax |
| | 2016 and later versions | Direct backup using [WITH SAS CREDENTIAL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) |
| **Restore from Azure Storage to a managed instance** | | [RESTORE FROM URL with SAS CREDENTIAL](/azure/azure-sql/managed-instance/restore-sample-database-quickstart) |

> [!IMPORTANT]  
> When you're migrating a database protected with [transparent data encryption](/azure/azure-sql/database/transparent-data-encryption-tde-overview) to a managed instance using native restore option, the corresponding certificate from the on-premises or Azure VM SQL Server needs to be migrated before database restore. For detailed steps, see [Migrate a certificate of a TDE-protected database to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/tde-certificate-migrate).
>
> Restore of system databases isn't supported. To migrate instance-level objects (stored in `master` or `msdb` databases), we recommend to script them out and run T-SQL scripts on the destination instance.

To migrate using backup and restore, follow these steps:

1. Back up your database to Azure Blob Storage. For example, use [backup to url](/sql/relational-databases/backup-restore/sql-server-backup-to-url) in [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms). Use the [Microsoft Azure Tool](https://go.microsoft.com/fwlink/?LinkID=324399) to support databases earlier than SQL Server 2012 SP1 CU2.
1. Connect to your Azure SQL Managed Instance using SQL Server Management Studio.
1. Create a credential using a Shared Access Signature to access your Azure Blob storage account with your database backups. For example:

   ```sql
   CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/databases]
       WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
           SECRET = '<secret>'
   ```

1. Restore the backup from the Azure storage blob container. For example:

   ```sql
   RESTORE DATABASE [TargetDatabaseName]
   FROM URL = 'https://mitutorials.blob.core.windows.net/databases/WideWorldImporters-Standard.bak'
   ```

1. Once restore completes, view the database in **Object Explorer** within SQL Server Management Studio.

To learn more about this migration option, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](/azure/azure-sql/managed-instance/restore-sample-database-quickstart).

> [!NOTE]  
> A database restore operation is asynchronous and can be retried. You might get an error in SQL Server Management Studio if the connection breaks or a time-out expires. Azure SQL Database will keep trying to restore database in the background, and you can track the progress of the restore using the [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql) and [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) views.

## Data sync and cutover

When using migration options that continuously replicate / sync data changes from source to the target, the source data and schema can change and drift from the target. During data sync, ensure that all changes on the source are captured and applied to the target during the migration process.

After you verify that data is the same on both source and target, you can cut over from the source to the target environment. It's important to plan the cutover process with business / application teams to ensure minimal interruption during cutover doesn't affect business continuity.

> [!IMPORTANT]  
> For details on the specific steps associated with performing a cutover as part of migrations using DMS, see [Performing migration cutover](/azure/dms/tutorial-sql-server-managed-instance-online#performing-migration-cutover).

## Post-migration

After you successfully complete the migration stage, go through a series of post-migration tasks to ensure that everything is functioning smoothly and efficiently.

The post-migration phase is crucial for reconciling any data accuracy issues and verifying completeness, and addressing performance issues with the workload.

### Monitor and remediate applications

Once you complete the migration to a managed instance, you should track the application behavior and performance of your workload. This process includes the following activities:

- [Compare performance of the workload running on the managed instance](performance-baseline.md#compare-performance) with the [performance baseline that you created on the source SQL Server instance](performance-baseline.md#create-a-baseline).
- Continuously [monitor performance of your workload](performance-baseline.md#monitor-performance) to identify potential issues and improvement.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you defined.
1. **Set up test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run the validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance test against the source and the target, and then analyze and compare the results.

## Use advanced features

You can take advantage of the advanced cloud-based features offered by SQL Managed Instance, such as [built-in high availability](/azure/azure-sql/database/high-availability-sla-local-zone-redundancy), [threat detection](/azure/azure-sql/database/azure-defender-for-sql), and [monitoring and tuning your workload](/azure/azure-sql/database/monitor-tune-overview).

[Azure SQL Analytics](/azure/azure-sql/database/monitor-tune-overview) allows you to monitor a large set of managed instances in a centralized manner.

Some SQL Server features are only available once the [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) is changed to the latest compatibility level (150).

## Related content

- [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix)
- [Service Tiers in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview#service-tiers)
- [T-SQL differences between SQL Server & Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server)
- [Azure total Cost of Ownership Calculator](https://azure.microsoft.com/pricing/tco/calculator/)
- [Migrate databases with Azure SQL Migration extension for Azure Data Studio](/azure/dms/dms-overview#migrate-databases-with-azure-sql-migration-extension-for-azure-data-studio)
- [Tutorial: Migrate SQL Server to Azure SQL Managed Instance with DMS](database-migration-service.md)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads migrate to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Data Access Migration Toolkit (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
- [Overview of Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview)
