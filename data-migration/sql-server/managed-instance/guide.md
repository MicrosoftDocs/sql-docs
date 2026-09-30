---
title: "SQL Server to Azure SQL Managed Instance: Migration Guide"
description: This guide teaches you to migrate your SQL Server databases to Azure SQL Managed Instance.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mathoma, danil
ms.date: 04/27/2026
ms.service: azure-sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Migration guide: SQL Server to Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../../includes/appliesto-sqlmi.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your user databases from SQL Server to Azure SQL Managed Instance.

Complete [pre-migration](../pre-migration.md) steps before continuing.

## Migrate

After you complete the steps for the [pre-migration stage](../pre-migration.md), you're ready to perform the schema and data migration.

Migrate your data using your chosen [migration method](overview.md#compare-migration-options).

*This section provides general migration steps for the following recommended migration options*:

- [SQL Server migration in Azure Arc](#sql-server-migration-in-azure-arc)
- [Azure Database Migration Service (Azure DMS)](#azure-database-migration-service-azure-dms), which offers migration with near-zero downtime
- [Managed Instance link](#managed-instance-link)
- [Log Replay Service (LRS)](#log-replay-service-lrs)
- [Native `RESTORE DATABASE FROM URL`](#backup-and-restore), which uses native backups from SQL Server and requires some downtime

SQL Managed Instance targets user scenarios that require mass database migration from on-premises or SQL Server on Azure Virtual Machines implementations. It's the optimal choice when you need to lift and shift the back end of applications that regularly use instance level and cross-database functionalities. If this is your scenario, you can move an entire instance to a corresponding environment in Azure without the need to rearchitect your applications.

To move SQL Server instances, you need to plan carefully:

- The migration of all databases that need to be collocated (ones running on the same instance).
- The migration of instance-level objects that your application depends on, including logins, credentials, SQL Server Agent jobs and operators, and server-level triggers.

SQL Managed Instance is a managed service that allows you to delegate some of the regular database administration activities to the platform as they're built in. Therefore, you don't need to migrate some instance-level data, such as maintenance jobs for regular backups or Always On configuration, as [high availability](/azure/azure-sql/database/high-availability-sla-local-zone-redundancy) is built in.

### SQL Server migration in Azure Arc

Migrate SQL Server instances enabled by Azure Arc to SQL Managed Instance through the Azure portal. SQL Managed Instance provides a fully managed PaaS solution for lift-and-shift migrations. The process includes assessing readiness, selecting a target, migrating data, and monitoring progress.

Two integrated methods are available:

- Managed Instance link for near real-time replication with minimal downtime,

- Log Replay Service for continuous backup and restore.

Microsoft Copilot assists throughout the migration. Migration supports SQL Server 2012 and later versions, and automates most steps.

For more information, see [Migration to Azure SQL Managed Instance - SQL Server migration in Azure Arc](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance?tabs=mi-link).

### Azure Database Migration Service (Azure DMS)

This section provides high-level steps to migrate from SQL Server to SQL Managed Instance with minimal downtime by using Azure DMS.

To migrate using DMS from the **Azure portal**, follow these steps:

1. Open the [Azure portal](https://portal.azure.com/).

1. Open Azure DMS and either select the DMS instance if you already created one, or create a new one.

1. On the DMS instance dashboard, select **Start migration**, choose your source server type, set your target server type to Azure SQL Managed Instance, and select the migration backup file storage location and migration mode.

1. Provide the source SQL Server tracking details for Azure, such as subscription, resource group, location, and SQL Server instance name. This step creates a SQL Server instance enabled by Azure Arc.

1. Provide the target subscription and resource group, then choose the target SQL managed instance.

1. Provide the backup location details, such as resource group, storage account, blob container, folder, last backup file (for offline migration mode), and target database.

1. *Optional*: If your backups are on an on-premises network share, download and install [self-hosted integration runtime](https://www.microsoft.com/download/details.aspx?id=39717) on a machine that can connect to the source SQL Server, and the location containing the backup files.

   1. You might have to provide source SQL Server instance details and credentials to connect to it.

   1. Additionally, select the databases and location of the network SMB file share where backup files are kept and credentials to connect to it.

1. Start the database migration and monitor the progress in the Azure portal from your DMS instance monitoring dashboard.

1. Complete the cutover.

   1. Stop all incoming transactions to the source database.
   1. Make application configuration changes to point to the target database in Azure SQL Managed Instance.
   1. Take any tail log backups for the source database in the backup location you specify.
   1. Ensure all database backups have the status Restored in the monitoring details page.
   1. Select Complete cutover in the monitoring details page.

For detailed instructions, see [Tutorial: Migrate SQL Server to Azure SQL Managed Instance with DMS](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance).

### Managed Instance link

This section provides high-level steps to migrate from SQL Server to Azure SQL Managed Instance with minimal downtime by using the Managed Instance link. For detailed instructions, see [Migrate with the link](/azure/azure-sql/managed-instance/managed-instance-link-migrate).

To migrate with the link, follow these steps:

1. Create your target SQL managed instance: [Azure portal](/azure/azure-sql/managed-instance/instance-create-quickstart), [PowerShell](/azure/azure-sql/managed-instance/scripts/create-configure-managed-instance-powershell), [Azure CLI](/azure/azure-sql/managed-instance/scripts/create-configure-managed-instance-cli).
1. [Prepare your environment for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation).
1. Configure the link with [SSMS](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-ssms) or [scripts](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-scripts).
1. Stop the workload.
1. Validate data on the target instance.
1. [Fail over the link](/azure/azure-sql/managed-instance/managed-instance-link-failover-how-to).

### Log Replay Service (LRS)

This section provides high-level steps to migrate from SQL Server to SQL Managed Instance with minimal downtime by using the Log Replay Service (LRS). For detailed instructions, review [Migrate databases from SQL Server by using Log Replay Service](/azure/azure-sql/managed-instance/log-replay-service-migrate).

To migrate with LRS, follow these steps:

1. Create an [Azure storage account](/azure/storage/common/storage-account-create?tabs=azure-portal) with a [blob container](/azure/storage/blobs/storage-quickstart-blobs-portal).
1. Authenticate to your Blob Storage storage account using a SAS token or a managed identity and validate access.
1. Be sure to [configure your folder structure correctly](/azure/azure-sql/managed-instance/log-replay-service-migrate#migrate-multiple-databases) if you plan to migrate multiple databases.
1. Upload your backups to your storage account by either copying your backups, or taking backups directly using [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url).
1. Determine if you want to run LRS in autocomplete or continuous mode.
1. Start LRS.
1. Monitor migration progress.
1. Complete the migration (if in continuous mode).

### Backup and restore

A key capability of SQL Managed Instance is the ability to natively restore database backup (`.bak`) files stored in [Azure Storage](https://azure.microsoft.com/services/storage/). This feature makes database migration straightforward. Backing up and restoring are asynchronous operations, based on the size of your database.

The following diagram provides a high-level overview of the process:

:::image type="content" source="media/guide/migration-restore.png" alt-text="Diagram shows SQL Server with an arrow labeled BACKUP / Upload to URL flowing to Azure Storage and a second arrow labeled RESTORE from URL flowing from Azure Storage to a SQL managed instance." lightbox="media/guide/migration-restore.png":::

> [!NOTE]  
> The time to take the backup, upload it to Azure storage, and perform a native restore operation to SQL Managed Instance depends on the size of the database. Factor in sufficient downtime to accommodate the operation for large databases.

The following table provides more information about the methods you can use, depending on the source SQL Server version you're running:

| Step | SQL Engine and version | Backup/restore method |
| --- | --- | --- |
| **Put backup to Azure Storage** | Before 2012 with Service Pack 1 CU2 | Upload `.bak` file directly to Azure Storage |
| | 2012 SP1 CU2 - 2016 | Direct backup using deprecated [WITH CREDENTIAL](/sql/t-sql/statements/restore-statements-transact-sql) syntax |
| | 2016 and later versions | Direct backup using [WITH SAS CREDENTIAL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) |
| **Restore from Azure Storage to a managed instance** | | [RESTORE FROM URL with SAS CREDENTIAL](/azure/azure-sql/managed-instance/restore-sample-database-quickstart) |

> [!IMPORTANT]  
> When you migrate a database protected with [transparent data encryption (TDE)](/azure/azure-sql/database/transparent-data-encryption-tde-overview) to a SQL managed instance using the native restore option, you need to migrate the corresponding certificate from the SQL Server instance (on-premises, or SQL Server on an Azure VM) before restoring the database. For detailed information, see [Migrate a SQL Server TDE certificate to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/tde-certificate-migrate).
>
> Restoring system databases isn't supported. To migrate instance-level objects (stored in `master` or `msdb` databases), script them out and run Transact-SQL (T-SQL) scripts on the destination instance.

To migrate using backup and restore, follow these steps:

1. Back up your database to Azure Blob Storage. For example, use [backup to url](/sql/relational-databases/backup-restore/sql-server-backup-to-url) in [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms). Use the [Microsoft Azure Tool](https://go.microsoft.com/fwlink/?LinkID=324399) to support databases earlier than SQL Server 2012 with Service Pack 1 CU2.
1. Connect to your SQL managed instance using SQL Server Management Studio (SSMS).
1. Create a credential using a Shared Access Signature to access your Azure Blob storage account with your database backups. For example:

   ```sql
   CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/databases]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
        SECRET = '<secret>';
   ```

1. Restore the backup from the Azure storage blob container. For example:

   ```sql
   RESTORE DATABASE [TargetDatabaseName]
   FROM URL = 'https://mitutorials.blob.core.windows.net/databases/WideWorldImporters-Standard.bak';
   ```

1. When the restore completes, view the database in **Object Explorer** within SSMS.

To learn more about this migration option, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](/azure/azure-sql/managed-instance/restore-sample-database-quickstart).

> [!NOTE]  
> A database restore operation is asynchronous and can be retried. You might get an error in SSMS if the connection breaks, or a timeout expires. SQL Managed Instance keeps trying to restore the database in the background, and you can track the progress of the restore by using the [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-objects/sys-dm-exec-requests-transact-sql) and [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-objects/sys-dm-operation-status-azure-sql-database) views.

## Common migration blockers

Before starting a migration, review these frequently encountered issues.

| Issue | Description |
| -- | --- |
| **Unsupported features** | Some SQL Server features aren't available in Azure SQL Managed Instance. Run a [migration assessment](/sql/sql-server/azure-arc/migration-assessment) to identify blockers before you begin. For a complete list, see [T-SQL differences between SQL Server and Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server). |
| **Virtual network configuration** | SQL Managed Instance requires a [dedicated subnet](/azure/azure-sql/managed-instance/connectivity-architecture-overview#network-requirements) in a virtual network. Misconfigured subnets, missing service endpoints, or incorrect network security group (NSG) rules prevent instance creation or block connectivity. Verify your network configuration before provisioning the target instance. |
| **Login and user migration** | SQL Server logins and database users don't transfer automatically. Script logins from the source instance and recreate them on the target. Pay attention to [orphaned users](/sql/sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server) that might result from SID mismatches. |
| **SQL Server Agent jobs** | Agent jobs, alerts, and operators aren't migrated with the database. Script and recreate them on the target instance after migration. |
| **TDE-protected databases** | If your database uses [transparent data encryption (TDE)](/azure/azure-sql/database/transparent-data-encryption-tde-overview), you must [migrate the TDE certificate](/azure/azure-sql/managed-instance/tde-certificate-migrate) to the target instance before restoring the database. |
| **Database size and throughput limits** | Large databases might exceed the [resource limits](/azure/azure-sql/managed-instance/resource-limits) for your chosen service tier. Check that your target configuration can accommodate the database size and I/O throughput requirements. |

## Data sync and cutover

When you use migration options that continuously replicate or sync data changes from source to the target, the source data and schema can change and drift from the target. During data sync, ensure that the migration process captures and applies all changes on the source to the target.

After you verify that data is the same on both source and target, you can cut over from the source to the target environment. Plan the cutover process with business and application teams to ensure minimal interruption during cutover and that it doesn't affect business continuity.

> [!IMPORTANT]  
> For details on the specific steps associated with performing a cutover as part of migrations using DMS, see [Performing migration cutover](/azure/dms/tutorial-sql-server-managed-instance-online#performing-migration-cutover).

## Post-migration

After you successfully complete the migration stage, go through a series of post-migration tasks to ensure that everything is functioning smoothly and efficiently.

The post-migration phase is crucial for reconciling any data accuracy issues, verifying completeness, and addressing performance issues with the workload.

### Monitor and remediate applications

After you migrate to a SQL managed instance, track the application behavior and performance of your workload. This process includes the following activities:

- [Compare performance of the workload running on the managed instance](performance-baseline.md#compare-performance) with the [performance baseline that you created on the source SQL Server instance](performance-baseline.md#create-a-baseline).
- Continuously [monitor performance of your workload](performance-baseline.md#monitor-performance) to identify potential issues and improvements.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, use T-SQL queries. Create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you defined.

1. **Set up test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.

1. **Run validation tests**: Run the validation tests against the source and the target, then analyze the results.

1. **Run performance tests**: Run performance tests against the source and the target, then analyze and compare the results.

## Use advanced features

Take advantage of the advanced cloud-based features offered by SQL Managed Instance, such as [built-in high availability](/azure/azure-sql/database/high-availability-sla-local-zone-redundancy), [threat detection](/azure/azure-sql/database/azure-defender-for-sql), and [monitoring and tuning your workload](/azure/azure-sql/database/monitor-tune-overview).

Monitor a large set of SQL managed instances in a centralized manner with [Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview).

Some SQL Server features are only available when you change the [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) to the latest compatibility level.

## Related content

- [Compare SQL data migration tools](/sql/sql-server/migrate/compare-sql-migration-tools)
- [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix)
- [Service Tiers in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview#service-tiers)
- [T-SQL differences between SQL Server and Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server)
- [Tutorial: Migrate SQL Server to Azure SQL Managed Instance with DMS](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads migrate to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
