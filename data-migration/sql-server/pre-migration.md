---
title: "Migrate from SQL Server: Pre-migration"
description: Follow these steps when migrating from SQL Server, for a successful discovery and assessment of your environment.
author: abhims14
ms.author: abhishekum
ms.reviewer: randolphwest
ms.date: 06/26/2024
ms.service: sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sql-migration-content
---
# Migrate from SQL Server: Pre-migration

[!INCLUDE [appliesto-sqlvm-sqldb-sqlmi](../includes/appliesto-sqlvm-sqldb-sqlmi.md)]

This article provides steps to prepare your environment to migrate from SQL Server to Azure SQL Database, Azure SQL Managed Instance, and SQL Server on Azure VMs.

## Supported sources and targets for migration

You can migrate SQL Server running on-premises or on:

- SQL Server on virtual machines (VMs).
- Amazon Web Services (AWS) EC2.
- Amazon Relational Database Service (AWS RDS).
- Compute Engine - Google Cloud Platform (GCP).

In this article, you learn how to *discover* and *assess* your user databases before migrating them from SQL Server to Azure SQL.

- [Migration overview: SQL Server to Azure SQL Managed Instance](managed-instance/overview.md)
- [Migration overview: SQL Server to SQL Server on Azure VMs](virtual-machines/overview.md)
- [Migration overview: SQL Server to Azure SQL Database](database/overview.md)

For other migration guides, see [Azure Database Migration Guides](/data-migration).

After you verify that your source environment is supported, start with the pre-migration stage. Discover all of the existing data sources, assess migration feasibility, and identify any blocking issues that might prevent your [Azure cloud migration](https://azure.microsoft.com/migration).

## Prerequisites

## [Azure SQL Managed Instance](#tab/sqlmi)

To migrate your SQL Server to Azure SQL Managed Instance, make sure you have:

- Chosen a [migration method](managed-instance/overview.md#compare-migration-options) and the corresponding tools for your method.
- Install the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
- Created a target [Quickstart: Create Azure SQL Managed Instance](/azure/azure-sql/managed-instance/instance-create-quickstart).
- Configured connectivity and proper permissions to access both source and target.
- Reviewed the SQL Server database engine features [available in Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison).

## [SQL Server on Azure VM](#tab/sqlvm)

Migrating to SQL Server on Azure Virtual Machines requires the following resources:

- [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
- An [Create and manage projects](/azure/migrate/create-manage-projects) (only required for SQL Server discovery in your data estate).
- A prepared target [Provision SQL Server on Azure VM (Azure portal)](/azure/azure-sql/virtual-machines/windows/create-sql-vm-portal) instance that's the same or greater version than the SQL Server source.
- [Connectivity between Azure and on-premises](/azure/architecture/reference-architectures/hybrid-networking).
- [Choosing an appropriate migration strategy](virtual-machines/overview.md#migrate).

## [Azure SQL Database](#tab/sqldb)

For your [SQL Server migration](https://azure.microsoft.com/migration/sql-server/) to Azure SQL Database, make sure you have:

- Chosen your [migration method](database/overview.md#compare-migration-options) and corresponding tools.
- Install the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
- Created a target [Quickstart: Create a single database - Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).
- Configured connectivity and proper permissions to access both source and target.
- Reviewed the database engine features [available in Azure SQL Database](/azure/azure-sql/database/features-comparison).

---

## Discover

In the *discover* phase, scan the network to identify all SQL Server instances and features used by your organization.

## [Azure SQL Managed Instance](#tab/sqlmi)

Use [About Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure.

Alternatively, use the [Microsoft Assessment and Planning Toolkit](https://www.microsoft.com/download/details.aspx?id=7826) (MAP Toolkit) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

For more information about tools available to use for the *discover* phase, see [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix).

After data sources are discovered, assess any on-premises SQL Server instances that can be migrated to Azure SQL Managed Instance to identify migration blockers or compatibility issues.
Proceed to the following steps to assess and migrate databases to Azure SQL Managed Instance:

:::image type="content" source="managed-instance/media/pre-migration/migration-process-sql-managed-instance-steps.png" alt-text="Screenshot of Steps for migration to Azure SQL Managed Instance." lightbox="managed-instance/media/pre-migration/migration-process-sql-managed-instance-steps.png":::

- [Assess SQL Managed Instance compatibility](#assess) where you should ensure that there are no blocking issues that can prevent your migrations.
  This step also includes creating a [performance baseline](managed-instance/performance-baseline.md#create-a-baseline) to determine resource usage on your source SQL Server instance. This step is needed if you want to deploy a properly sized managed instance and verify that performance after migration isn't affected.
- [Connect your application to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/connect-application-instance).
- Deploy to an optimally sized managed instance where you choose technical characteristics (number of vCores, amount of memory) and performance tier (Business Critical, General Purpose) of your managed instance.
- [Select migration method and migrate](managed-instance/overview.md#compare-migration-options) where you migrate your databases using offline migration or online migration options.
- [Monitor and remediate applications](managed-instance/guide.md#monitor-and-remediate-applications) to ensure that you see the expected performance.

## [SQL Server on Azure VM](#tab/sqlvm)

Azure Migrate assesses migration suitability of on-premises computers, performs performance-based sizing, and provides cost estimations for running on-premises. To plan for the migration, use Azure Migrate to [identify existing data sources and details about the features](/azure/migrate/concepts-assessment-calculation) your SQL Server instances use. This process involves scanning the network to identify all of your SQL Server instances in your organization with the version and features in use.

> [!IMPORTANT]  
> When you choose a target Azure virtual machine for your SQL Server instance, be sure to review [Checklist: Best practices for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist).

For more discovery tools, see the [services and tools](/azure/dms/dms-tools-matrix#business-justification-phase) available for data migration scenarios.

## [Azure SQL Database](#tab/sqldb)

Use [About Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure.

Alternatively, use the [Microsoft Assessment and Planning Toolkit (the "MAP Toolkit")](https://www.microsoft.com/download/details.aspx?id=7826) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

For more information about tools available to use for the *discover* phase, see [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix).

---

## Assess

[!INCLUDE [azure-migrate-to-assess-sql-data-estate](../includes/azure-migrate-to-assess-sql-data-estate.md)]

## [Azure SQL Managed Instance](#tab/sqlmi)

Determine whether SQL Managed Instance is compatible with the database requirements of your application. SQL Managed Instance is designed to provide easy lift and shift migration for most existing applications that use SQL Server. However, you might sometimes require features or capabilities that aren't yet supported and the cost of implementing a workaround is too high.

The [Migrate databases by using the Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) provides a seamless wizard based experience to assess, get Azure recommendations and migrate your SQL Server databases on-premises to SQL Server on Azure Virtual Machines. Besides highlighting any migration blockers or warnings, the extension also includes an option for Azure recommendations to collect your databases' performance data [to recommend a right-sized Azure SQL Managed Instance](/azure/dms/ads-sku-recommend) to meet the performance needs of your workload (with the least price).

You can use the Azure SQL Migration extension for Azure Data Studio to assess databases to get:

- [Assessment rules for SQL Server to Azure SQL Managed Instance migration](managed-instance/assessment-rules.md)
- [Get Azure recommendations to migrate your SQL Server database](/azure/dms/ads-sku-recommend)

To assess your environment using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**.
1. Select your Azure SQL target, in this case, **Azure SQL Managed Instance**.
1. Select **View/Select** to review the assessment report.
1. Look for migration blocking and feature parity issues. The assessment report can also be exported to a file that can be shared with other teams or personnel in your organization.
1. Determine the database compatibility level that minimizes post-migration efforts.

To get an Azure recommendation using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**.
1. Select your Azure SQL target, in this case, **Azure SQL Managed Instance**.
1. Navigate to the Azure recommendations sections, and select **Get Azure recommendation**.
1. Select **Collect performance data now**. Choose a folder on your local computer to store the performance logs, and then select **Start**.
1. After 10 minutes, Azure Data Studio indicates that a recommendation is available for Azure SQL Managed Instance.
1. Check the Azure SQL Managed Instance card, in the Azure SQL target panel to review your Azure SQL Managed Instance SKU recommendation.

To learn more, see [Tutorial: Migrate SQL Server to Azure SQL Managed Instance with DMS](managed-instance/database-migration-service.md).

If the assessment encounters multiple blockers to confirm that your database isn't ready for an Azure SQL Managed Instance, then alternatively consider:

- [SQL Server on Azure Virtual Machines](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-migration-overview) if both SQL Database and SQL Managed Instance fail to be suitable targets.

### Scaled assessments and analysis

The [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension) and [Azure Migrate](https://azure.microsoft.com/services/azure-migrate) supports performing scaled assessments and consolidation of the assessment reports for analysis.

If you have multiple servers and databases that need to be assessed and analyzed at scale to provide a wider view of the data estate, see the following links to learn more:

- [Migrate databases at scale using automation (Preview)](/azure/dms/migration-dms-powershell-cli)
- [Performing scaled assessments using PowerShell - Azure Migrate](/sql/dma/dma-consolidatereports)
- [Analyzing assessment reports using Power BI - Azure Migrate](/sql/dma/dma-consolidatereports#dma-reports)

> [!IMPORTANT]  
> Running assessments at scale for multiple databases can also be automated using [Run Data Migration Assistant from the command line](/sql/dma/dma-commandline) which also allows the results to be uploaded to [Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb#view-target-readiness-assessment-results) for further analysis and target readiness.

## [SQL Server on Azure VM](#tab/sqlvm)

When migrating from SQL Server on-premises to SQL Server on Azure Virtual Machines, it's unlikely that you have any compatibility or feature parity issues, if the source and target SQL Server versions are the same. If you're *not* upgrading the version of SQL Server, skip this step and move to the [Migrate](virtual-machines/guide.md#migrate) section.

Before migration, it's still a good practice to run an assessment of your SQL Server databases to identify migration blockers (if any) and the [Migrate databases by using the Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) does that before migration.

### Assess user databases

The [Migrate databases by using the Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) provides a seamless wizard based experience to assess, get Azure recommendations and migrate your SQL Server databases on-premises to SQL Server on Azure Virtual Machines. Besides highlighting any migration blockers or warnings, the extension also includes an option for Azure recommendations to collect your databases' performance data to recommend a right-sized SQL Server on Azure Virtual Machines to meet the performance needs of your workload (with the least price).

To learn more about Azure recommendations, see [Get Azure recommendations to migrate your SQL Server database](/azure/dms/ads-sku-recommend).

> [!IMPORTANT]  
> To assess databases using the Azure SQL migration extension, ensure that the logins used to connect the source SQL Server are members of the sysadmin server role or have CONTROL SERVER permission.

For a version upgrade, use [Overview of Data Migration Assistant](/sql/dma/dma-overview) to assess on-premises SQL Server instances if you're upgrading to an instance of SQL Server on Azure Virtual Machines with a higher version to understand the gaps between the source and target versions.

### Assess the applications

Typically, an application layer accesses user databases to persist and modify data. Data Migration Assistant can assess the data access layer of an application in two ways:

- By using captured [extended events](/sql/relational-databases/extended-events/extended-events) or [SQL Server Profiler traces](/sql/tools/sql-server-profiler/create-a-trace-sql-server-profiler) of your user databases. You can also use the [Capture a trace in Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-capture-trace) to create a trace log that can also be used for A/B testing.
- By using the [Data Access Migration Toolkit (preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit), which provides discovery and assessment of SQL queries within the code and is used to migrate application source code from one database platform to another. This tool supports popular file types like C#, Java, XML, and plain text. For a guide on how to perform a Data Access Migration Toolkit assessment, see the [Use Data Migration Assistant](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/using-data-migration-assistant-to-assess-an-application-s-data/ba-p/990430) blog post.

During the assessment of user databases, use Data Migration Assistant to [import](/sql/dma/dma-assesssqlonprem#add-databases-and-extended-events-trace-to-assess) captured trace files or Data Access Migration Toolkit files.

### Assessments at scale

If you have multiple servers that require Azure readiness assessment, you can automate the process by using scripts using one of the following options. To learn more about using scripting see [Migrate databases at scale using automation (Preview)](/azure/dms/migration-dms-powershell-cli).

- [Az.DataMigration PowerShell module](/powershell/module/az.datamigration)
- [az datamigration CLI extension](/cli/azure/datamigration)
- [Run Data Migration Assistant from the command line](/sql/dma/dma-commandline)

For summary reporting across large estates, Data Migration Assistant assessments can also be [consolidated into Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb).

### Upgrade databases with Data Migration Assistant

For upgrade scenario, you might have a series of recommendations to ensure your user databases perform and function correctly after upgrade. Data Migration Assistant provides details on the affected objects and resources for how to resolve each issue. Make sure to resolve all breaking changes and behavior changes before you start production upgrade.

For deprecated features, you can choose to run your user databases in their original [compatibility](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) mode if you want to avoid making these changes and speed up migration. This action prevents [upgrading your database compatibility](/sql/database-engine/install-windows/compatibility-certification#compatibility-levels-and-database-engine-upgrades) until the deprecated items are resolved.

> [!CAUTION]  
> Not all SQL Server versions support all compatibility modes. Check that your [target SQL Server version](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) supports your chosen database compatibility. For example, SQL Server 2019 doesn't support databases with level 90 compatibility (which is SQL Server 2005). These databases would require, at least, an upgrade to compatibility level 100.

## [Azure SQL Database](#tab/sqldb)

After data sources are discovered, assess any on-premises SQL Server databases that can be migrated to Azure SQL Database to identify migration blockers or compatibility issues.

The [Migrate databases by using the Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) provides a seamless wizard based experience to assess, get Azure recommendations and migrate your SQL Server databases on-premises to SQL Server on Azure Virtual Machines. Besides highlighting any migration blockers or warnings, the extension also includes an option for Azure recommendations to collect your databases' performance data [to recommend a right-sized Azure SQL Managed Instance](/azure/dms/ads-sku-recommend) to meet the performance needs of your workload (with the least price).

You can use the Azure SQL Migration extension for Azure Data Studio to assess databases to get:

- [Assessment rules for SQL Server to Azure SQL Database migration](database/assessment-rules.md)
- [Get Azure recommendations to migrate your SQL Server database](/azure/dms/ads-sku-recommend)

To assess your environment using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**.
1. Select your Azure SQL target, in this case, **Azure SQL Database**.
1. Select **View/Select** to review the assessment report.
1. Look for migration blocking and feature parity issues. The assessment report can also be exported to a file that can be shared with other teams or personnel in your organization.
1. Determine the database compatibility level that minimizes post-migration efforts.

To get an Azure recommendation using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**
1. Select your Azure SQL target, in this case, **Azure SQL Database**.
1. Navigate to the Azure recommendations sections, select **Get Azure recommendation**.
1. Select **Collect performance data now**. Select a folder on your local computer to store the performance logs, and then select **Start**.
1. After 10 minutes, Azure Data Studio indicates that a recommendation is available for Azure SQL Database.
1. Check the Azure SQL Database card, in the Azure SQL target panel to review your Azure SQL Database SKU recommendation.

To learn more, see [Tutorial: Migrate SQL Server to Azure SQL Database (offline)](database/database-migration-service.md).

If the assessment encounters multiple blockers to confirm that your database isn't ready for an Azure SQL Database migration, then alternatively consider:

- [Migration overview: SQL Server to Azure SQL Managed Instance](managed-instance/overview.md) if there are multiple instance-scoped dependencies
- [SQL Server on Azure Virtual Machines](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-migration-overview) if both SQL Database and SQL Managed Instance fail to be suitable targets.

### Scaled assessments and analysis

The [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension) and [Azure Migrate](https://azure.microsoft.com/services/azure-migrate) supports performing scaled assessments and consolidation of the assessment reports for analysis.

If you have multiple servers and databases that need to be assessed and analyzed at scale to provide a wider view of the data estate, see the following links to learn more:

- [Migrate databases at scale using automation (Preview)](/azure/dms/migration-dms-powershell-cli)
- [Performing scaled assessments using PowerShell - Azure Migrate](/sql/dma/dma-consolidatereports)
- [Analyzing assessment reports using Power BI - Azure Migrate](/sql/dma/dma-consolidatereports#dma-reports)

> [!IMPORTANT]  
> Running assessments at scale for multiple databases, especially large ones, can also be automated using the [Run Data Migration Assistant from the command line](/sql/dma/dma-commandline) and uploaded to [Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb#view-target-readiness-assessment-results) for further analysis and target readiness.

---

## Related content

- [Migration guide: SQL Server to Azure SQL Managed Instance](managed-instance/guide.md)
- [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](virtual-machines/guide.md)
- [Migration guide: SQL Server to Azure SQL Database](database/guide.md)
