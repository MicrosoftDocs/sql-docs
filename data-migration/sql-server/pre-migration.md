---
title: "Migrate from SQL Server: Pre-migration"
description: Follow these steps when migrating from SQL Server, for a successful discovery and assessment of your environment.
author: abhims14
ms.author: abhishekum
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: azure-sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sql-migration-content
---
# Migrate from SQL Server: Pre-migration

[!INCLUDE [appliesto-sqlvm-sqldb-sqlmi](../includes/appliesto-sqlvm-sqldb-sqlmi.md)]

This article provides steps to prepare your environment to migrate from SQL Server to Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VMs.

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

## Migration steps

This section provides an overview of the steps to take to migrate your SQL Server to Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VMs.

## [Azure SQL Managed Instance](#tab/sqlmi)

To migrate your SQL Server to Azure SQL Managed Instance, follow these steps: 

- Review the SQL Server database engine features [available in Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison).
- Choose a [migration method](managed-instance/overview.md#compare-migration-options) and the corresponding tools for your method.
- Create a [performance baseline](managed-instance/performance-baseline.md#create-a-baseline) to determine resource usage on your source SQL Server instance. This step helps you deploy a properly sized managed instance so that performance after migration isn't affected.
- [Discover](#discover) all SQL Server instances and features used by your organization.
- [Assess](#assess) your SQL Server databases to identify migration blockers or compatibility issues.
- Create a target [SQL Managed Instance](/azure/azure-sql/managed-instance/instance-create-quickstart). Deploy an optimally sized managed instance where you choose technical characteristics (number of vCores, amount of memory) and performance tier (Business Critical, General Purpose) of your managed instance.
- Configure connectivity and proper permissions to access both source and target.
- [Migrate your database](managed-instance/overview.md#compare-migration-options) where you migrate your databases using offline migration or online migration options.
- [Connect your application to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/connect-application-instance).
- [Monitor and remediate applications](managed-instance/guide.md#monitor-and-remediate-applications) to ensure that you see the expected performance.


:::image type="content" source="managed-instance/media/pre-migration/migration-process-sql-managed-instance-steps.png" alt-text="Screenshot of Steps for migration to Azure SQL Managed Instance." lightbox="managed-instance/media/pre-migration/migration-process-sql-managed-instance-steps.png":::

If the assessment encounters multiple blockers to confirm that your database isn't ready for an Azure SQL Managed Instance, then alternatively consider [SQL Server on Azure Virtual Machines](virtual-machines/overview.md). 

## [SQL Server on Azure VM](#tab/sqlvm)

To migrate your SQL Server to SQL Server on Azure VMs, follow these steps: 

- [Choose an appropriate migration strategy](virtual-machines/overview.md#migrate).
- [Discover](#discover) all SQL Server instances and features used by your organization.
- [Assess](#assess) your SQL Server databases to identify migration blockers or compatibility issues.
- Prepare a target [SQL Server on Azure VM](/azure/azure-sql/virtual-machines/windows/create-sql-vm-portal) that's the same or greater version than the SQL Server source. 
- Configure connectivity and proper permissions to access both source and target, such as, for example, with [hybrid networking](/azure/architecture/reference-architectures/hybrid-networking).
- [Migrate your database](virtual-machines/overview.md#migrate) where you migrate your databases using offline migration or online migration options.
- [Monitor and remediate applications](virtual-machines/guide.md#remediate-applications) to ensure that you see the expected performance.

When migrating from SQL Server on-premises to SQL Server on Azure Virtual Machines, it's unlikely to have any compatibility or feature parity issues if the source and target SQL Server versions are the same. However, before migration, it's still a good practice to run an [assessment](#assess) of your SQL Server databases to identify migration blockers (if any). 

> [!IMPORTANT]  
> When you choose a target Azure virtual machine for your SQL Server instance, be sure to review [Checklist: Best practices for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist).

### Upgrade databases with Data Migration Assistant

If the target SQL Server on Azure VM instance is a higher version, use [Overview of Data Migration Assistant](/sql/dma/dma-overview) to assess on-premises SQL Server instances to understand the gaps between the source and target versions.

If you're upgrading the SQL Server version, you might have a series of recommendations to ensure your user databases perform and function correctly after the upgrade. Data Migration Assistant provides details on the affected objects and resources for how to resolve each issue. Make sure to resolve all breaking changes and behavior changes before you start production upgrade.

For deprecated features, you can choose to run your user databases in their original [compatibility](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) mode if you want to avoid making these changes and speed up migration. This action prevents [upgrading your database compatibility](/sql/database-engine/install-windows/compatibility-certification#compatibility-levels-and-database-engine-upgrades) until the deprecated items are resolved.

> [!CAUTION]  
> Not all SQL Server versions support all compatibility modes. Check that your [target SQL Server version](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) supports your chosen database compatibility. For example, SQL Server 2019 doesn't support databases with level 90 compatibility (which is SQL Server 2005). These databases would require, at least, an upgrade to compatibility level 100.

## [Azure SQL Database](#tab/sqldb)

For your [SQL Server migration](https://azure.microsoft.com/migration/sql-server/) to Azure SQL Database, make sure you have:

- Review the database engine features [available in Azure SQL Database](/azure/azure-sql/database/features-comparison).
- [Discover](#discover) all SQL Server instances and features used by your organization.
- [Assess](#assess) your SQL Server databases to identify migration blockers or compatibility issues.
- Choose your [migration method](database/overview.md#compare-migration-options) and corresponding tools.
- Create a target [Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).
- Configure connectivity and proper permissions to access both source and target.
- [Monitor and remediate applications](database/guide.md#remediate-applications).

---

## Discover

In the *discover* phase, scan the network to identify all SQL Server instances and features used by your organization.

Use the following tools to discover your SQL Server instances:
- [Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure.
- [Microsoft Assessment and Planning Toolkit](https://www.microsoft.com/download/details.aspx?id=7826) (MAP Toolkit) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

For more information about tools available to use for the *discover* phase, see [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix).


## Assess

[!INCLUDE [azure-migrate-to-assess-sql-data-estate](../includes/azure-migrate-to-assess-sql-data-estate.md)]

If your assessment encounters multiple blockers, consider migrating to one of the Azure SQL targets as an alternative, such as Azure SQL Managed Instance or SQL Server on Azure Virtual Machines. 

### Assess with Azure Data Studio 

The [Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) provides a seamless wizard-based experience to assess, get Azure recommendations and migrate your SQL Server databases on-premises to Azure. Besides highlighting any migration blockers or warnings, the extension also includes an option for Azure recommendations to collect your databases' performance data and [ recommends a right-sized Azure SQL target](/azure/dms/ads-sku-recommend) to meet the performance needs of your workload (with the lowest price).

You can use the Azure SQL Migration extension for Azure Data Studio to assess databases to get:

- [Assessment rules for SQL Server to Azure SQL migration targets](managed-instance/assessment-rules.md)
- [Get Azure recommendations to migrate your SQL Server database](/azure/dms/ads-sku-recommend)

> [!IMPORTANT]  
> To assess databases using the Azure SQL migration extension, ensure that the logins used to connect the source SQL Server are members of the sysadmin server role or have CONTROL SERVER permission.

To assess your environment using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**.
1. Select your Azure SQL target. 
1. Select **View/Select** to review the assessment report.
1. Look for migration blocking and feature parity issues. The assessment report can also be exported to a file that can be shared with other teams or personnel in your organization.
1. Determine the database compatibility level that minimizes post-migration efforts.

To get an Azure recommendation using the Azure SQL Migration extension, follow these steps:

1. Open the [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Connect to your source SQL Server instance.
1. Select **Migrate to Azure SQL**, in the Azure SQL Migration wizard in Azure Data Studio.
1. Select databases for assessment, then select **Next**.
1. Select your Azure SQL target. 
1. Navigate to the Azure recommendations sections, and select **Get Azure recommendation**.
1. Select **Collect performance data now**. Choose a folder on your local computer to store the performance logs, and then select **Start**.
1. After 10 minutes, Azure Data Studio indicates that a recommendation is available for Azure SQL Managed Instance.
1. Check the Azure SQL Managed Instance card, in the Azure SQL target panel to review your Azure SQL Managed Instance SKU recommendation.

For specific Azure SQL migration target tutorials, see: 
- [Tutorial: Migrate SQL Server to Azure SQL Managed Instance with DMS](managed-instance/database-migration-service.md)
- [Tutorial: Migrate SQL Server to Azure SQL Database (offline)](database/database-migration-service.md)


### Assess with SQL Server enabled by Arc

To assess your SQL Server instances for migration to Azure, use SQL Server enabled by Azure Arc. This feature, currently in preview, automatically produces an assessment for migration to Azure, simplifying the discovery process and readiness assessment for migration. 

To assess your instances using SQL Server enabled by Azure Arc, follow these steps:

1. [Automatically connect SQL Server machines to Azure Arc](/sql/sql-server/azure-arc/automatically-connect). 
1. [Verify](/azure/azure-arc/servers/manage-vm-extensions-portal#list-extensions-installed) your Azure Extension for SQL Server (WindowsAgent.SqlServer)  version is 1.1.2594.118 or later.
1. Go your SQL Server enabled by Azure Arc resource in the Azure portal.
1. Under **Migration**, select **Assessments (Preview)** to open the Assessments page and review results. 

For details, see [Assess instances for migration with SQL Server enabled by Azure arc](/sql/sql-server/azure-arc/migration-assessment).

### Scaled assessments and analysis

The [Azure SQL migration extension for Azure Data Studio](/azure-data-studio/extensions/azure-sql-migration-extension) and [Azure Migrate](https://azure.microsoft.com/services/azure-migrate) can perform scaled assessments and consolidate assessment reports for analysis.

If you have multiple servers and databases that need to be assessed and analyzed at scale to provide a wider view of the data estate, see the following links to learn more:

- [Migrate databases at scale using automation with DMS (Preview)](/azure/dms/migration-dms-powershell-cli)
- [Performing scaled assessments using PowerShell - Azure Migrate](/sql/dma/dma-consolidatereports)
- [Analyzing assessment reports using Power BI - Azure Migrate](/sql/dma/dma-consolidatereports#dma-reports)


Running assessments at scale for multiple databases can also be automated using [Run Data Migration Assistant from the command line](/sql/dma/dma-commandline) which also allows the results to be uploaded to [Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb#view-target-readiness-assessment-results) for further analysis and target readiness.


You can automate the process by using scripts with one of the following options. To learn more about using scripting, see [Migrate databases at scale using automation (Preview)](/azure/dms/migration-dms-powershell-cli).

- [Az.DataMigration PowerShell module](/powershell/module/az.datamigration)
- [az datamigration CLI extension](/cli/azure/datamigration)
- [Run Data Migration Assistant from the command line](/sql/dma/dma-commandline)

For summary reporting across large estates, Data Migration Assistant assessments can also be [consolidated into Azure Migrate](/sql/dma/dma-assess-sql-data-estate-to-sqldb).


### Assess the applications

Typically, an application layer accesses user databases to persist and modify data. Data Migration Assistant can assess the data access layer of an application in two ways:

- By using captured [extended events](/sql/relational-databases/extended-events/extended-events) or [SQL Server Profiler traces](/sql/tools/sql-server-profiler/create-a-trace-sql-server-profiler) of your user databases. You can also use the [Capture a trace in Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-capture-trace) to create a trace log that can also be used for A/B testing.
- By using the [Data Access Migration Toolkit (preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit), which provides discovery and assessment of SQL queries within the code and is used to migrate application source code from one database platform to another. This tool supports popular file types like C#, Java, XML, and plain text. For a guide on how to perform a Data Access Migration Toolkit assessment, see the [Use Data Migration Assistant](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/using-data-migration-assistant-to-assess-an-application-s-data/ba-p/990430) blog post.

During the assessment of user databases, use Data Migration Assistant to [import](/sql/dma/dma-assesssqlonprem#add-databases-and-extended-events-trace-to-assess) captured trace files or Data Access Migration Toolkit files.


## Related content

- [Migration guide: SQL Server to Azure SQL Managed Instance](managed-instance/guide.md)
- [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](virtual-machines/guide.md)
- [Migration guide: SQL Server to Azure SQL Database](database/guide.md)
