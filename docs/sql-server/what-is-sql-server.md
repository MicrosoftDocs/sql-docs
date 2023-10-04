---
title: What is SQL Server?
description: An overview of the relational database engine and components of SQL Server
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/05/2023
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
---

# What is SQL Server?

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [msconame-md](../includes/msconame-md.md)] [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is a relational database management system (RDBMS). Applications and tools connect to a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] *instance* or *database*, and communicate using [Transact-SQL](../t-sql/language-reference.md) (T-SQL).

## Deployment options

You can install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows or [Linux](../linux/sql-server-linux-overview.md), deploy it in [a Linux container](../linux/sql-server-linux-overview.md#container-images), or deploy it on an [Azure Virtual Machine](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) or other virtual machine platform. You may previously have referred to this as the *boxed product*.

Supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] depend on your license agreement, but for the purposes of this documentation, we mean [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions. Documentation for [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] and previous versions is available at [Previous versions of SQL Server documentation](previous-versions-sql-server.md). To find out which versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] are currently supported, see [SQL Server end of support options](end-of-support/sql-server-end-of-support-overview.md).

The underlying [!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)] is also used by the following products and services:

- [[!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)]](/azure/azure-sql/database/sql-database-paas-overview)
- [[!INCLUDE [ssazuremi_md](../includes/ssazuremi_md.md)]](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)
- [Microsoft Analytics Platform System](../analytics-platform-system/home-analytics-platform-system-aps-pdw.md) (PDW)
- [[!INCLUDE [ssazuresynapse-md](../includes/ssazuresynapse-md.md)]](/azure/synapse-analytics/overview-what-is)
- [[!INCLUDE [ssazurede-md](../includes/ssazurede-md.md)]](/azure/azure-sql-edge/overview)

[!INCLUDE [editions-supported-features-windows](../includes/editions-supported-features-windows.md)]

## SQL Server components and technologies

This section describes some of the key technologies available in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Database Engine

The [!INCLUDE [ssde-md](../includes/ssde-md.md)] is the core service for storing, processing, and securing data. The [!INCLUDE [ssde-md](../includes/ssde-md.md)] provides controlled access and transaction processing to meet the requirements of the most demanding data consuming applications within your enterprise. The [!INCLUDE [ssde-md](../includes/ssde-md.md)] also provides rich support for sustaining business continuity through [Business continuity and database recovery](../database-engine/sql-server-business-continuity-dr.md).

### Machine Learning Services

[Machine Learning Services](../machine-learning/sql-server-machine-learning-services.md) supports integration of machine learning, using the popular R and Python languages, into enterprise workflows.

Machine Learning Services (In-Database) integrates R and Python with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], making it easy to build, retrain, and score models by calling stored procedures. Machine Learning Server provides enterprise-scale support for R and Python, without requiring [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Integration Services

[SQL Server Integration Services](../integration-services/sql-server-integration-services.md) (SSIS) is a platform for building high performance data integration solutions, including packages that provide extract, transform, and load (ETL) processing for data warehousing.

### Analysis Services

[SQL Server Analysis Services](/analysis-services/ssas-overview) (SSAS) is an analytical data platform and toolset for personal, team, and corporate business intelligence. Servers and client designers support traditional OLAP solutions, new tabular modeling solutions, as well as self-service analytics and collaboration using [!INCLUDE [power-pivot-md](../includes/power-pivot-md.md)], Excel, and a SharePoint Server environment. [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] also includes Data Mining so that you can uncover the patterns and relationships hidden inside large volumes of data.

### Reporting Services

[SQL Server Reporting Services](../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md) (SSRS) delivers enterprise, Web-enabled reporting functionality. You can create reports that draw content from various data sources, publish reports in various formats, and centrally manage security and subscriptions.

### Replication

[SQL Server Replication](../relational-databases/replication/sql-server-replication.md) is a set of technologies for copying and distributing data and database objects from one database to another, and then synchronizing between databases to maintain consistency. By using replication, you can distribute data to different locations and to remote or mobile users with local and wide area networks, dial-up connections, wireless connections, and the Internet.

### Data Quality Services

[Data Quality Services](../data-quality-services/data-quality-services.md) (DQS) provides you with a knowledge-driven data cleansing solution. DQS enables you to build a knowledge base, and then use that knowledge base to perform data correction and deduplication on your data, using both computer-assisted and interactive means. You can use cloud-based reference data services, and you can build a data management solution that integrates DQS with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Integration Services and Master Data Services.

### Master Data Services

[Master Data Services](../master-data-services/master-data-services-overview-mds.md) (MDS) is the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] solution for master data management. A solution built on [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] helps ensure that reporting and analysis are based on the right information. Using [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)], you create a central repository for your master data and maintain an auditable, securable record of that data as it changes over time.

## Azure integration

Although [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is a standalone product, which can be installed on computers running Windows and Linux operating systems, you can integrate your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instances with several Azure services.

### Azure Virtual Machines

[SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) enables you to use full versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in the cloud without having to manage any on-premises hardware. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] virtual machines (VMs) also simplify licensing costs when you pay as you go.

Azure virtual machines run in many different geographic regions around the world. They also offer various machine sizes. The virtual machine image gallery allows you to create a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] VM with the right version, edition, and operating system. This makes virtual machines a good option for many different [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] workloads.

### Azure Arc

[Azure Arc](azure-arc/overview.md) simplifies governance and management by delivering a consistent multicloud and on-premises management platform. Azure Arc provides a centralized, unified way to manage your entire environment together, combining existing non-Azure and/or on-premises virtual machines, Kubernetes clusters, and databases into Azure Resource Manager.

You can use Azure services and management capabilities, introduce DevOps practices to support new cloud native patterns in your environment, and configure custom locations as an abstraction layer on top of Azure Arc-enabled Kubernetes clusters and cluster extensions, regardless of where your resources live.

### Azure Kubernetes Service (AKS)

[Azure Kubernetes Service](/azure/aks/intro-kubernetes) (AKS) is a managed Kubernetes service for deploying and managing container clusters. With [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux containers, you can [deploy a SQL Server Linux container to AKS using Helm charts](../linux/sql-server-linux-containers-deploy-helm-charts-kubernetes.md).

> [!NOTE]  
> You can also set up [Azure Arc-enabled SQL Managed Instance](/azure/azure-arc/data/managed-instance-overview) on a Kubernetes infrastructure of your choice, which allows you to manage the service in Azure while your data stays in the location you prefer.

## Migrate and move data

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] provides many opportunities to migrate and modernize your data estate.

### Migrating to the cloud

- [Migrate SQL Server workloads (FAQ)](/azure/azure-sql/migration-guides/modernization)
- [Import and Export Data with the SQL Server Import and Export Wizard](../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md)
- [Azure Database Migration Guides](/data-migration/)

### Migrating to SQL Server

- [Migrate databases and structured data to SQL Server on Linux](../linux/sql-server-linux-migrate-overview.md) <sup>1</sup>
- [Data Migration Assistant](../dma/dma-overview.md)
- [Import data from Excel to SQL Server or Azure SQL Database](../relational-databases/import-export/import-data-from-excel-to-sql.md)
- [SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md)

<sup>1</sup> [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] and later versions.

## Update your version of SQL Server

- [Latest updates and version history for SQL Server](/troubleshoot/sql/releases/download-and-install-latest-updates)

## Samples

- [Wide World Importers sample databases](../samples/wide-world-importers-what-is.md)
- [AdventureWorks sample databases](../samples/adventureworks-install-configure.md)
- [SQL Server samples on GitHub](https://github.com/Microsoft/sql-server-samples)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]

## Connect to SQL Server

- [Connect to the Database Engine](connect-to-database-engine.md)
- [What is SQL Server Management Studio (SSMS)?](../ssms/sql-server-management-studio-ssms.md)
- [What is Azure Data Studio?](../azure-data-studio/what-is-azure-data-studio.md)

## Next steps

- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md) (Windows)
- [Installation guidance for SQL Server on Linux](../linux/sql-server-linux-setup.md)
- [Configure and customize SQL Server Docker containers](../linux/sql-server-linux-docker-container-configure.md)
- [Server configuration options (SQL Server)](../database-engine/configure-windows/server-configuration-options-sql-server.md)
