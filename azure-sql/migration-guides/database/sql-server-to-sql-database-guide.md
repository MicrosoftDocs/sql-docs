---
title: "SQL Server to Azure SQL Database: Migration guide"
description: Follow this guide to migrate your SQL Server databases to Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/06/2023
ms.service: sql-database
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sql-migration-content
---
# Migration guide: SQL Server to Azure SQL Database

[!INCLUDE [appliesto-sqlserver-sqldb](../../includes/appliesto-sqlserver-sqldb.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your SQL Server instance to Azure SQL Database.

You can migrate SQL Server running on-premises or on:

- SQL Server on Virtual Machines
- Amazon EC2 (Elastic Compute Cloud)
- Amazon RDS (Relational Database Service) for SQL Server
- Google Compute Engine
- Cloud SQL for SQL Server - GCP (Google Cloud Platform)

For more migration information, see the [migration overview](sql-server-to-sql-database-overview.md). For other migration guides, see [Database Migration](/data-migration).

:::image type="content" source="media/sql-server-to-database-overview/migration-process-flow-small.png" alt-text="Diagram of migration process flow.":::

## Prerequisites

For your [SQL Server migration](https://azure.microsoft.com/migration/sql-server/) to Azure SQL Database, make sure you have:

- Chosen your [migration method](sql-server-to-sql-database-overview.md#compare-migration-options) and corresponding tools.
- Install the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
- Created a target [Azure SQL Database](../../database/single-database-create-quickstart.md).
- Configured connectivity and proper permissions to access both source and target.
- Reviewed the database engine features [available in Azure SQL Database](../../database/features-comparison.md).

## Pre-migration

After you've verified that your source environment is supported, start with the pre-migration stage. Discover all of the existing data sources, assess migration feasibility, and identify any blocking issues that might prevent your [Azure cloud migration](https://azure.microsoft.com/migration).

### Discover

In the discover phase, scan the network to identify all SQL Server instances and features used by your organization.

Use [Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure.

Alternatively, use the [Microsoft Assessment and Planning Toolkit (the "MAP Toolkit")](https://www.microsoft.com/download/details.aspx?id=7826) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

For more information about tools available to use for the discover phase, see [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix).

### Assess

[!INCLUDE [assess-estate-with-azure-migrate](../../includes/azure-migrate-to-assess-sql-data-estate.md)]

After data sources have been discovered, assess any on-premises SQL Server database(s) that can be migrated to Azure SQL Database to identify migration blockers or compatibility issues.

The [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) provides a seamless wizard based experience to assess, get Azure recommendations and migrate your SQL Server databases on-premises to SQL Server on Azure Virtual Machines. Besides, highlighting any migration blockers or warnings, the extension also includes an option for Azure recommendations to collect your databases' performance data [to recommend a right-sized Azure SQL Managed Instance](/azure/dms/ads-sku-recommend) to meet the performance needs of your workload (with the least price).

You can use the Azure SQL Migration extension for Azure Data Studio to assess databases to get:

- [Migration readiness - Assessment rules](./sql-server-to-sql-database-assessment-rules.md)
- [Azure right-sized recommendations](/azure/dms/ads-sku-recommend)

To assess your environment using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL Migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance
1. Click the *Migrate to Azure SQL* button, in the Azure SQL Migration wizard in Azure Data Studio
1. Select databases for assessment, then click on next
1. Select your Azure SQL target, in this case, Azure SQL Database (Preview)
1. Click on *View/Select* to review the assessment report
1. Look for migration blocking and feature parity issues. The assessment report can also be exported to a file that can be shared with other teams or personnel in your organization.
1. Determine the database compatibility level that minimizes post-migration efforts.

To get an Azure recommendation using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL Migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance
1. Click the *Migrate to Azure SQL* button, in the Azure SQL Migration wizard in Azure Data Studio
1. Select databases for assessment, then click on next
1. Select your Azure SQL target, in this case, Azure SQL Database (Preview)
1. Navigate to the Azure recommendations sections, click on *Get Azure recommendation*
1. Select Collect performance data now. Select a folder on your local computer to store the performance logs, and then select Start.
1. After 10 minutes, Azure Data Studio indicates that a recommendation is available for Azure SQL Database.
1. Check the Azure SQL Database card, in the Azure SQL target panel to review your Azure SQL Database SKU recommendation

To learn more, see [Tutorial: Migrate SQL Server to Azure SQL Database (preview) offline in Azure Data Studio](/azure/dms/tutorial-sql-server-azure-sql-database-offline-ads).

If the assessment encounters multiple blockers to confirm that your database is not ready for an Azure SQL Database migration, then alternatively consider:

- [Azure SQL Managed Instance](../managed-instance/sql-server-to-managed-instance-overview.md) if there are multiple instance-scoped dependencies
- [SQL Server on Azure Virtual Machines](../virtual-machines/sql-server-to-sql-on-azure-vm-migration-overview.md) if both SQL Database and SQL Managed Instance fail to be suitable targets.

#### Scaled assessments and analysis

The [Azure SQL Migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension) and  [Azure Migrate](https://azure.microsoft.com/services/azure-migrate) supports performing scaled assessments and consolidation of the assessment reports for analysis.

If you have multiple servers and databases that need to be assessed and analyzed at scale to provide a wider view of the data estate, see the following links to learn more:

- [Migrate databases at scale using automation (Preview) - Azure SQL Migration extension](/azure/dms/migration-dms-powershell-cli)
- [Performing scaled assessments using PowerShell - Azure Migrate](/sql/dma/dma-consolidatereports)
- [Analyzing assessment reports using Power BI - Azure Migrate](/sql/dma/dma-consolidatereports#dma-reports)

> [!IMPORTANT]  
> Running assessments at scale for multiple databases, especially large ones, can also be automated using the [DMA Command Line Utility](/sql/dma/dma-commandline) and uploaded to [Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb#view-target-readiness-assessment-results) for further analysis and target readiness.

### Deploy to an optimally sized managed instance

You can use the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension) to get right-sized Azure SQL Managed Instance recommendation. The extension collects performance data from your source SQL Server instance to provide right-sized Azure recommendation that meets your workload's performance needs with minimal cost. To learn more, see [Get right-sized Azure recommendation for your on-premises SQL Server database(s)](/azure/dms/ads-sku-recommend)

Based on the information in the discover and assess phase, create an appropriately sized target Azure SQL Database. You can do so by using the [Quickstart: Create a single database - Azure SQL Database](../../database/single-database-create-quickstart.md).

## Migrate

After you have completed tasks associated with the pre-migration stage, you are ready to perform the schema and data migration.

Migrate your data using your chosen [migration method](sql-server-to-sql-database-overview.md#compare-migration-options).


### Migrate using the Azure SQL migration extension for Azure Data Studio

To perform an offline migration using Azure Data Studio, follow the high-level steps below. For a detailed step-by-step tutorial, see [Tutorial: Migrate SQL Server to Azure SQL Database (preview) offline in Azure Data Studio](/azure/dms/tutorial-sql-server-azure-sql-database-offline-ads).

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

After you verify that data is same on both the source and the target, you can cut over from the source to the target environment. It is important to plan the cutover process with business / application teams to ensure minimal interruption during cutover doesn't affect business continuity.

> [!IMPORTANT]  
> For details on the specific steps associated with performing a cutover as part of migrations using DMS, see [Performing migration cutover](/azure/dms/tutorial-sql-server-to-azure-sql).

### Migrate using transactional replication

When you can't afford to remove your SQL Server database from production while the migration is occurring, you can use SQL Server transactional replication as your migration solution. To use this method, the source database must meet the [requirements for transactional replication](../../database/replication-to-sql-database.md) and be compatible for Azure SQL Database. For information about SQL replication with availability groups, see [Configure replication for Always On availability groups (SQL Server)](/sql/database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server).

To use this solution, you configure your database in Azure SQL Database as a subscriber to the SQL Server instance that you wish to migrate. The transactional replication distributor synchronizes data from the database to be synchronized (the publisher) while new transactions continue.

With transactional replication, all changes to your data or schema show up in your database in Azure SQL Database. Once the synchronization is complete and you're ready to migrate, change the connection string of your applications to point them to your database. Once transactional replication drains any changes left on your source database and all your applications point to Azure SQL Database, you can uninstall transactional replication. Your database in Azure SQL Database is now your production system.

> [!TIP]  
> You can also use transactional replication to migrate a subset of your source database. The publication that you replicate to Azure SQL Database can be limited to a subset of the tables in the database being replicated. For each table being replicated, you can limit the data to a subset of the rows and/or a subset of the columns.

#### Transaction replication workflow

> [!IMPORTANT]  
> Use the latest version of SQL Server Management Studio to remain synchronized with updates to Azure and SQL Database. Older versions of SQL Server Management Studio cannot set up SQL Database as a subscriber. [Get the latest version of SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms).

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
| **Source (typically on premises)** | The primary bottleneck during migration from the source is data file I/O and latency, which needs to be monitored carefully. | Based on data file I/O and latency, and depending on whether it's a virtual machine or physical server, you may have to engage your storage admin and explore options to mitigate the bottleneck. |
| **Target (Azure SQL Database)** | The biggest limiting factor is the log generation rate and latency on your database log file. With Azure SQL Database, you can get a maximum log generation rate of 96 MB/s. | To speed up migration, scale up the target Azure SQL database to Business Critical Gen5 8 vCore to get the maximum log generation rate of 96 MB/s, which also provides low latency for log files. The [Hyperscale](../../database/service-tier-hyperscale.md) service tier provides a log rate of 100 MB/s regardless of chosen service level. |
| **Network** | The network bandwidth needed is equal to the maximum log ingestion rate 96 MB/s (768 Mb/s) | Depending on network connectivity from your on-premises data center to Azure, check your network bandwidth (typically [Azure ExpressRoute](/azure/expressroute/expressroute-introduction#bandwidth-options)) to accommodate for the maximum log ingestion rate. |

You can also consider these recommendations for best performance during the migration process.

- Choose the highest service tier and compute size that your budget allows to maximize the transfer performance. You can scale down after the migration completes to save money.
- If you use BACPAC files, minimize the distance between your BACPAC file and the destination data center.
- Disable auto update and auto create statistics during migration.
- Partition tables and indexes.
- Drop indexed views, and recreate them once finished.
- Remove rarely queried historical data to another database and migrate this historical data to a separate database in Azure SQL Database. You can then query this historical data using [elastic queries](../../database/elastic-query-overview.md).

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

Be sure to take advantage of the advanced cloud-based features offered by SQL Database, such as [built-in high availability](../../database/high-availability-sla-local-zone-redundancy.md), [threat detection](../../database/azure-defender-for-sql.md), and [monitoring and tuning your workload](../../database/monitor-tune-overview.md).

Some SQL Server features are only available once the [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) is changed to the latest compatibility level.

To learn more, see [managing Azure SQL Database after migration](../../database/manage-data-after-migrating-to-database.md).

## Resolve database migration compatibility issues

You may encounter a wide variety of compatibility issues, depending both on the version of SQL Server in the source database and the complexity of the database you're migrating. Older versions of SQL Server have more compatibility issues. Use the following resources, in addition to a targeted Internet search using your search engine of choices:

- [Transact-SQL differences between SQL Server and Azure SQL Database](../../database/transact-sql-tsql-differences-sql-server.md)
- [Discontinued Database Engine Functionality in SQL Server](/sql/database-engine/discontinued-database-engine-functionality-in-sql-server)

> [!IMPORTANT]  
> Azure SQL Managed Instance enables you to migrate an existing SQL Server instance and its databases with minimal to no compatibility issues. See [What is Azure SQL Managed Instance?](../../managed-instance/sql-managed-instance-paas-overview.md)

## Next steps

See [Service and tools for data migration](/azure/dms/dms-tools-matrix) for a matrix of the Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios as well as specialty tasks.

To learn more about the Azure SQL Migration extension see:

   - [Migrate databases with Azure SQL Migration extension for Azure Data Studio](/azure/dms/dms-overview#migrate-databases-with-azure-sql-migration-extension-for-azure-data-studio)

   - [Tutorial: Migrate SQL Server to Azure SQL Database (preview) offline in Azure Data Studio](/azure/dms/tutorial-sql-server-azure-sql-database-offline-ads).

To learn more about [Azure Migrate](https://azure.microsoft.com/services/azure-migrate) see:

- [Azure Migrate](/azure/migrate/migrate-services-overview)

To learn more about SQL Database see:

- [An Overview of Azure SQL Database](../../database/sql-database-paas-overview.md)
- [Azure total Cost of Ownership Calculator](https://azure.microsoft.com/pricing/tco/calculator/)

To learn more about the framework and adoption cycle for Cloud migrations, see:

- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads for migration to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Cloud Migration Resources](https://azure.microsoft.com/migration/resources)

To assess the Application access layer, see [Data Access Migration Toolkit (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)

For details on how to perform Data Access Layer A/B testing see [Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview).
