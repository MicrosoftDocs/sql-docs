---
title: "SQL Server to Azure SQL Database: Migration guide"
description: Follow this guide to migrate your SQL Server databases to Azure SQL Database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/26/2024
ms.service: azure-sql-database
ms.subservice: migration-guide
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Migration guide: SQL Server to Azure SQL Database

[!INCLUDE [appliesto-sqlserver-sqldb](../../includes/appliesto-sqlserver-sqldb.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your SQL Server instance to Azure SQL Database.

Complete [pre-migration](../pre-migration.md) steps before continuing.

## Migrate

After you complete the steps for the [pre-migration stage](../pre-migration.md), you're ready to perform the schema and data migration.

Migrate your data using your chosen [migration method](overview.md#compare-migration-options).

### Migrate using the Azure SQL migration extension for Azure Data Studio

To perform an offline migration using Azure Data Studio, follow the high-level steps below. For a detailed step-by-step tutorial, see [Tutorial: Migrate SQL Server to Azure SQL Database (offline)](database-migration-service.md).

1. Download and install [Azure Data Studio](/azure-data-studio/download-azure-data-studio) and the [Azure SQL migration extension](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Launch the Migrate to Azure SQL Migration wizard in the extension in Azure Data Studio.
1. Select databases for assessment and view migration readiness or issues (if any). Additionally, collect performance data and get right-sized Azure recommendation.
1. Select your Azure account and your target Azure SQL Database from your subscription.
1. Select the list of tables to migrate.
1. Create a new Azure Database Migration Service using the wizard in Azure Data Studio. If you've previously created an Azure Database Migration Service using Azure Data Studio, you can reuse the same if desired.
1. *Optional*: If your backups are on an on-premises network share, download and install [self-hosted integration runtime](https://www.microsoft.com/download/details.aspx?id=39717) on a machine that can connect to the source SQL Server, and the location containing the backup files.
1. Start the database migration and monitor the progress in Azure Data Studio. You can also monitor the progress under the Azure Database Migration Service resource in Azure portal.

## Data sync and cutover

When using migration options that continuously replicate / sync data changes from source to the target, the source data and schema can change and drift from the target. During data sync, ensure that all changes on the source are captured and applied to the target during the migration process.

After you verify that data is same on both the source and the target, you can cut over from the source to the target environment. It's important to plan the cutover process with business / application teams to ensure minimal interruption during cutover doesn't affect business continuity.

> [!IMPORTANT]  
> For details on the specific steps associated with performing a cutover as part of migrations using DMS, see [Tutorial: Migrate SQL Server to Azure SQL Database using DMS (classic)](/azure/dms/tutorial-sql-server-to-azure-sql).

### Migrate using transactional replication

When you can't afford to remove your SQL Server database from production while the migration is occurring, you can use SQL Server transactional replication as your migration solution. To use this method, the source database must meet the [requirements for transactional replication](/azure/azure-sql/database/replication-to-sql-database) and be compatible for Azure SQL Database. For information about SQL replication with availability groups, see [Configure replication with Always On availability groups](/sql/database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server).

To use this solution, you configure your database in Azure SQL Database as a subscriber to the SQL Server instance that you wish to migrate. The transactional replication distributor synchronizes data from the database to be synchronized (the publisher) while new transactions continue.

With transactional replication, all changes to your data or schema show up in your database in Azure SQL Database. Once the synchronization is complete and you're ready to migrate, change the connection string of your applications to point them to your database. Once transactional replication drains any changes left on your source database and all your applications point to Azure SQL Database, you can uninstall transactional replication. Your database in Azure SQL Database is now your production system.

> [!TIP]  
> You can also use transactional replication to migrate a subset of your source database. The publication that you replicate to Azure SQL Database can be limited to a subset of the tables in the database being replicated. For each table being replicated, you can limit the data to a subset of the rows and/or a subset of the columns.

#### Transaction replication workflow

> [!IMPORTANT]  
> Use the latest version of SQL Server Management Studio to remain synchronized with updates to Azure and SQL Database. Older versions of SQL Server Management Studio can't set up SQL Database as a subscriber. [Get the latest version of SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms).

| Step | Method |
| --- | --- |
| Set up distribution | [SQL Server Management Studio](/sql/relational-databases/replication/configure-publishing-and-distribution/) \| [Transact-SQL](/sql/relational-databases/replication/configure-publishing-and-distribution/) |
| Create publication | [SQL Server Management Studio](/sql/relational-databases/replication/configure-publishing-and-distribution/) \| [Transact-SQL](/sql/relational-databases/replication/configure-publishing-and-distribution/) |
| Create subscription | [SQL Server Management Studio](/sql/relational-databases/replication/create-a-push-subscription/) \| [Transact-SQL](/sql/relational-databases/replication/create-a-push-subscription/) |

Some tips and differences for migrating to SQL Database

- Use a local distributor
  - Doing so causes a performance impact on the server.
  - If the performance impact is unacceptable you can use another server, but it adds complexity in management and administration.
- When selecting a snapshot folder, make sure the folder you select is large enough to hold a BCP of every table you want to replicate.
- Snapshot creation locks the associated tables until it's complete, so schedule your snapshot appropriately.
- Only push subscriptions are supported in Azure SQL Database. You can only add subscribers from the source database.

## Migration recommendations

To speed up migration to Azure SQL Database, you should consider the following recommendations:

| | Resource contention | Recommendation |
| --- | --- | --- |
| **Source (typically on premises)** | The primary bottleneck during migration from the source is data file I/O and latency, which needs to be monitored carefully. | Based on data file I/O and latency, and depending on whether it's a virtual machine or physical server, you might have to engage your storage admin and explore options to mitigate the bottleneck. |
| **Target (Azure SQL Database)** | The biggest limiting factor is the log generation rate and latency on your database log file. With Azure SQL Database, you can get a maximum log generation rate of 96 MB/s. | To speed up migration, scale up the target Azure SQL database to Business Critical Gen5 8 vCore to get the maximum log generation rate of 96 MB/s, which also provides low latency for log files. The [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale) provides a log rate of 100 MB/s regardless of chosen service level. |
| **Network** | The network bandwidth needed is equal to the maximum log ingestion rate 96 MB/s (768 Mb/s) | Depending on network connectivity from your on-premises data center to Azure, check your network bandwidth (typically [Azure ExpressRoute](/azure/expressroute/expressroute-introduction#bandwidth-options)) to accommodate for the maximum log ingestion rate. |

You can also consider these recommendations for best performance during the migration process.

- Choose the highest service tier and compute size that your budget allows to maximize the transfer performance. You can scale down after the migration completes to save money.
- If you use BACPAC files, minimize the distance between your BACPAC file and the destination data center.
- Disable auto update and auto create statistics during migration.
- Partition tables and indexes.
- Drop indexed views, and recreate them once finished.
- Remove rarely queried historical data to another database and migrate this historical data to a separate database in Azure SQL Database. You can then query this historical data using [elastic queries](/azure/azure-sql/database/elastic-query-overview).

## Post-migration

After you have successfully completed the migration stage, go through the following post-migration tasks to ensure that everything is functioning smoothly and efficiently.

The post-migration phase is crucial for reconciling any data accuracy issues and verifying completeness, as well as addressing performance issues with the workload.

### Update statistics

[Update statistics](/sql/t-sql/statements/update-statistics-transact-sql) with full scan after the migration is completed.

### Remediate applications

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this will, in some cases, require changes to the applications.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you have defined.
1. **Set up test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run the validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance test against the source and the target, and then analyze and compare the results.

## Use advanced features

Be sure to take advantage of the advanced cloud-based features offered by SQL Database, such as [built-in high availability](/azure/azure-sql/database/high-availability-sla-local-zone-redundancy), [threat detection](/azure/azure-sql/database/azure-defender-for-sql), and [monitoring and tuning your workload](/azure/azure-sql/database/monitor-tune-overview).

Some SQL Server features are only available once the [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) is changed to the latest compatibility level.

To learn more, see [managing Azure SQL Database after migration](/azure/azure-sql/database/manage-data-after-migrating-to-database).

## Resolve database migration compatibility issues

You might encounter a wide variety of compatibility issues, depending both on the version of SQL Server in the source database and the complexity of the database you're migrating. Older versions of SQL Server have more compatibility issues. Use the following resources, in addition to a targeted Internet search using your search engine of choices:

- [Transact-SQL differences between SQL Server and Azure SQL Database](/azure/azure-sql/database/transact-sql-tsql-differences-sql-server)
- [Discontinued Database Engine functionality in SQL Server](/sql/database-engine/discontinued-database-engine-functionality-in-sql-server)

> [!IMPORTANT]  
> Azure SQL Managed Instance enables you to migrate an existing SQL Server instance and its databases with minimal to no compatibility issues. See [What is Azure SQL Managed Instance?](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)

## Related content

- [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix)
- [Migrate databases with Azure SQL Migration extension for Azure Data Studio](/azure/dms/dms-overview#migrate-databases-with-azure-sql-migration-extension-for-azure-data-studio)
- [Tutorial: Migrate SQL Server to Azure SQL Database (offline)](database-migration-service.md)
- [About Azure Migrate](/azure/migrate/migrate-services-overview)
- [What is Azure SQL Database?](/azure/azure-sql/database/sql-database-paas-overview)
- [Azure total Cost of Ownership Calculator](https://azure.microsoft.com/pricing/tco/calculator/)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads for migration to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Cloud Migration Resources](https://azure.microsoft.com/migration/resources)
- [Data Access Migration Toolkit (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
- [Overview of Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview)
