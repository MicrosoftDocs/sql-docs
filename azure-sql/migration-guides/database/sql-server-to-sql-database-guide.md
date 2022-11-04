---
title: "SQL Server to Azure SQL Database: Migration guide"
description: Follow this guide to migrate your SQL Server databases to Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 10/14/2022
ms.service: sql-database
ms.subservice: migration-guide
ms.topic: how-to
---
# Migration guide: SQL Server to Azure SQL Database

[!INCLUDE [appliesto-sqlserver-sqldb](../../includes/appliesto-sqlserver-sqldb.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your SQL Server instance to Azure SQL Database.

You can migrate SQL Server running on-premises or on:

- SQL Server on Virtual Machines
- Amazon Web Services (AWS) EC2
- Amazon Relational Database Service (AWS RDS)
- Compute Engine (Google Cloud Platform - GCP)
- Cloud SQL for SQL Server (Google Cloud Platform – GCP)

For more migration information, see the [migration overview](sql-server-to-sql-database-overview.md). For other migration guides, see [Database Migration](/data-migration).

:::image type="content" source="media/sql-server-to-database-overview/migration-process-flow-small.png" alt-text="Diagram of migration process flow.":::

## Prerequisites

For your [SQL Server migration](https://azure.microsoft.com/migration/sql-server/) to Azure SQL Database, make sure you have:

- Chosen your [migration method](sql-server-to-sql-database-overview.md#compare-migration-options) and corresponding tools.
- Installed [Data Migration Assistant (DMA)](https://www.microsoft.com/download/details.aspx?id=53595) on a machine that can connect to your source SQL Server.
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

You can use the Data Migration Assistant (version 4.1 and later) to assess databases to get:

- [Azure target recommendations](/sql/dma/dma-assess-sql-data-estate-to-sqldb)
- [Azure SKU recommendations](/sql/dma/dma-sku-recommend-sql-db)

To assess your environment using the Database Migration Assessment, follow these steps:

1. Open the [Data Migration Assistant (DMA)](https://www.microsoft.com/download/details.aspx?id=53595).
1. Select **File** and then choose **New assessment**.
1. Specify a project name, select SQL Server as the source server type, and then select Azure SQL Database as the target server type.
1. Select the type(s) of assessment reports that you want to generate. For example, database compatibility and feature parity. Based on the type of assessment, the permissions required on the source SQL Server can be different.  DMA will highlight the permissions required for the chosen advisor before running the assessment.
    - The **feature parity** category provides a comprehensive set of recommendations, alternatives available in Azure, and mitigating steps to help you plan your migration project. (sysadmin permissions required)
    - The **compatibility issues** category identifies partially supported or unsupported feature compatibility issues that might block migration, and recommendations to address them (`CONNECT SQL`, `VIEW SERVER STATE`, and `VIEW ANY DEFINITION` permissions required).
1. Specify the source connection details for your SQL Server and connect to the source database.
1. Select **Start assessment**.
1. After the process completes, select and review the assessment reports for migration blocking and feature parity issues. The assessment report can also be exported to a file that can be shared with other teams or personnel in your organization.
1. Determine the database compatibility level that minimizes post-migration efforts.
1. Identify the best Azure SQL Database SKU for your on-premises workload.

To learn more, see [Perform a SQL Server migration assessment with Data Migration Assistant](/sql/dma/dma-assesssqlonprem).

If the assessment encounters multiple blockers to confirm that your database it not ready for an Azure SQL Database migration, then alternatively consider:

- [Azure SQL Managed Instance](../managed-instance/sql-server-to-managed-instance-overview.md) if there are multiple instance-scoped dependencies
- [SQL Server on Azure Virtual Machines](../virtual-machines/sql-server-to-sql-on-azure-vm-migration-overview.md) if both SQL Database and SQL Managed Instance fail to be suitable targets.

#### Scaled assessments and analysis

Data Migration Assistant supports performing scaled assessments and consolidation of the assessment reports for analysis.

If you have multiple servers and databases that need to be assessed and analyzed at scale to provide a wider view of the data estate, see the following links to learn more:

- [Performing scaled assessments using PowerShell](/sql/dma/dma-consolidatereports)
- [Analyzing assessment reports using Power BI](/sql/dma/dma-consolidatereports#dma-reports)

> [!IMPORTANT]  
> Running assessments at scale for multiple databases, especially large ones, can also be automated using the [DMA Command Line Utility](/sql/dma/dma-commandline) and uploaded to [Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb#view-target-readiness-assessment-results) for further analysis and target readiness.

## Migrate

After you have completed tasks associated with the pre-migration stage, you are ready to perform the schema and data migration.

Migrate your data using your chosen [migration method](sql-server-to-sql-database-overview.md#compare-migration-options).

This guide describes the two most popular options - Data Migration Assistant and Azure Database Migration Service.

### Data Migration Assistant (DMA)

To migrate a database from SQL Server to Azure SQL Database using DMA, follow these steps:

1. Download and install the [Database Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595).
1. Create a new project and select **Migration** as the project type.
1. Set the source server type to **SQL Server** and the target server type to **Azure SQL Database**, select the migration scope as **Schema and data** and select **Create**.
1. In the migration project, specify the source server details such as the server name, credentials to connect to the server and the source database to migrate.
1. In the target server details, specify the Azure SQL Database server name, credentials to connect to the server and the target database to migrate to.
1. Select the schema objects and deploy them to the target Azure SQL Database.
1. Finally, select **Start data migration** and monitor the progress of migration.

For a detailed tutorial, see [Migrate on-premises SQL Server or SQL Server on Azure VMs to Azure SQL Database using the Data Migration Assistant](/sql/dma/dma-migrateonpremsqltosqldb).

> [!NOTE]  
> The compatibility level of the imported database is based on the compatibility level of your source database.

### Azure Database Migration Service (DMS)

To migrate databases from SQL Server to Azure SQL Database using DMS, follow the steps below:

1. If you haven't already, register the **Microsoft.DataMigration** resource provider in your subscription.
1. Create an Azure Database Migration Service Instance in a desired location of your choice (preferably in the same region as your target Azure SQL Database). Select an existing virtual network or create a new one to host your DMS instance.
1. After your DMS instance is created, create a new migration project and specify the source server type as **SQL Server** and the target server type as **Azure SQL Database**. Choose **Offline data migration** as the activity type in the migration project creation blade.
1. Specify the source SQL Server details on the **Migration source** details page and the target Azure SQL Database details on the **Migration target** details page.
1. Map the source and target databases for migration and then select the tables you want to migrate.
1. Review the migration summary and select **Run migration**. You can then monitor the migration activity and check the progress of your database migration.

For a detailed tutorial, see [Migrate SQL Server to an Azure SQL Database using DMS](/azure/dms/tutorial-sql-server-to-azure-sql).

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
| **Virtual machine used for Data Migration Assistant (DMA)** | CPU is the primary bottleneck for the virtual machine running DMA | Things to consider to speed up data migration by using:<br />- Azure compute-intensive VMs<br />- Use at least F8s_v2 (8 vCore) VM for running DMA<br />- Ensure the VM is running in the same Azure region as the target. |
| **Azure Database Migration Service (DMS)** | There can be compute resource contention and database objects consideration for DMS. | Use Premium 4 vCore. DMS automatically takes care of database objects like foreign keys, triggers, constraints, and non-clustered indexes and doesn't need manual intervention. |

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

Be sure to take advantage of the advanced cloud-based features offered by SQL Database, such as [built-in high availability](../../database/high-availability-sla.md), [threat detection](../../database/azure-defender-for-sql.md), and [monitoring and tuning your workload](../../database/monitor-tune-overview.md).

Some SQL Server features are only available once the [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) is changed to the latest compatibility level.

To learn more, see [managing Azure SQL Database after migration](../../database/manage-data-after-migrating-to-database.md).

## Resolve database migration compatibility issues

You may encounter a wide variety of compatibility issues, depending both on the version of SQL Server in the source database and the complexity of the database you're migrating. Older versions of SQL Server have more compatibility issues. Use the following resources, in addition to a targeted Internet search using your search engine of choices:

- [Transact-SQL differences between SQL Server and Azure SQL Database](../../database/transact-sql-tsql-differences-sql-server.md)
- [Discontinued Database Engine Functionality in SQL Server](/sql/database-engine/discontinued-database-engine-functionality-in-sql-server)

> [!IMPORTANT]  
> Azure SQL Managed Instance enables you to migrate an existing SQL Server instance and its databases with minimal to no compatibility issues. See [What is Azure SQL Managed Instance?](../../managed-instance/sql-managed-instance-paas-overview.md)

## Next steps

For a matrix of the Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios as well as specialty tasks, see [Service and tools for data migration](/azure/dms/dms-tools-matrix).

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
