---
title: "SQL Server Documentation | Microsoft Docs"
ms.date: "08/08/2019"
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
ms.technology: ""
ms.topic: conceptual
f1_keywords: 
  - "sql13.portal.f1"
helpviewer_keywords: 
  - "documentation [SQL Server], home page"
  - "Help [SQL Server]"
  - "SQL Server, documentation"
  - "home page [SQL Server]"
  - "Help [SQL Server], documentation home page"
  - "Books Online [SQL Server], home page"
  - "portal page [SQL Server]"
ms.assetid: 674933a8-e423-4d44-a39b-2a997e2c2333
author: craigg-msft
ms.author: jroth
monikerRange: ">=sql-server-linux-2017||>=sql-server-2016||=sqlallproducts-allversions"
---
# SQL Server Documentation

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

::: moniker range=">= sql-server-linux-2017 || >= sql-server-2017 || = sqlallproducts-allversions"
SQL Server is a central part of the Microsoft data platform. SQL Server is an industry leader in operational database management systems (ODBMS). This documentation helps you install, configure, and use SQL Server on both Windows and Linux. The content includes end-to-end examples, code samples, and videos. For SQL Server language topics, see [Language Reference](../t-sql/language-reference.md).
::: moniker-end

::: moniker range="= sql-server-2016"
SQL Server is a central part of the Microsoft data platform. SQL Server is an industry leader in operational database management systems (ODBMS). This documentation helps you install, configure, and use SQL Server on Windows. The content includes end-to-end examples, code samples, and videos. For SQL Server language topics, see [Language Reference](../t-sql/language-reference.md).
::: moniker-end

SQL Server 2014, and older versions including SQL Server 2005, have documentation available [here](#previous-versions-gm66).

<!-- Moniker assignments nicely designed so that exactly 1 of these next 3 little WhatsNew tables will be displayed
 at any one time, regardless of user's moniker choice.
-->

::: moniker range="= sqlallproducts-allversions"

|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md)     | [SQL Server 2019 Release Notes](../sql-server/sql-server-ver15-release-notes.md)        |
|[What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)     | [SQL Server 2017 Release Notes](../sql-server/sql-server-2017-release-notes.md)        |
|[What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)     | [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md)        |
| &nbsp; | &nbsp; |

![info_tip](../sql-server/media/info-tip.png) The SQL Server **2014** documentation is available [here](https://docs.microsoft.com/sql/2014-toc/index?view=sql-server-2014).
::: moniker-end

::: moniker range="= sql-server-ver15"

|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md)     | [SQL Server 2019 Release Notes](../sql-server/sql-server-ver15-release-notes.md)        |
| &nbsp; | &nbsp; |

::: moniker-end

::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)     | [SQL Server 2017 Release Notes](../sql-server/sql-server-2017-release-notes.md)        |
| &nbsp; | &nbsp; |

> [!NOTE]
> SQL Server 2019 preview is now available. For more information, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md?view=sql-server-ver15).
::: moniker-end

::: moniker range="=sql-server-2016"

|What's new  | Release notes  |
|---------|---------|
|[What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)     | [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md)        |
| &nbsp; | &nbsp; |

::: moniker-end

::: moniker range="= sql-server-2016 || = sqlallproducts-allversions"
**Try SQL Server 2016!**
- [![Download from Evaluation Center](../includes/media/download2.png)](https://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server for Windows](https://go.microsoft.com/fwlink/?LinkID=829477)
- [![Create Virtual Machine](../includes/media/azure-vm.png)](https://azure.microsoft.com/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm) [Get a Virtual Machine with SQL Server](https://azure.microsoft.com/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm)
::: moniker-end

::: moniker range=">= sql-server-2017 || >= sql-server-linux-2017 || = sqlallproducts-allversions"
**Try SQL Server!**
- [![Download from Evaluation Center](../includes/media/download2.png)](https://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server for Windows](https://go.microsoft.com/fwlink/?LinkID=829477)
- [![Install SQL Server on Linux](../includes/media/download2.png)](../linux/sql-server-linux-setup.md) [Install SQL Server on Linux](../linux/sql-server-linux-setup.md)
- [![Create Virtual Machine](../includes/media/azure-vm.png)](https://azure.microsoft.com/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm) [Get a Virtual Machine with SQL Server](https://azure.microsoft.com/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm)
::: moniker-end

[!INCLUDE[get-sql-tools](../includes/paragraph-content/get-sql-tools.md)]

## SQL Server Technologies

|||
|-|-|
|![SQL database engine](../sql-server/media/sql-database-engine.png "SQL database engine")|**[Database Engine](../database-engine/sql-server-database-engine-overview.md)**<br /><br /> The Database Engine is the core service for storing, processing, and securing data. The Database Engine provides controlled access and rapid transaction processing to meet the requirements of the most demanding data consuming applications within your enterprise. The Database Engine also provides rich support for sustaining high availability.|
|![Machine Learning Services](../sql-server/media/r-server.png "R Server")|**[Machine Learning Services](../advanced-analytics/index.yml)**<br /><br /> Machine Learning Services gives the ability to run Python and R scripts with relational data. You can use open source and Microsoft packages for predictive analytics and machine learning. The scripts are executed in-database without moving data outside SQL Server or over the network.|
|![Integration Services](../sql-server/media/integration-services.png "Integration Services")|**[Integration Services](../integration-services/sql-server-integration-services.md)**<br /><br /> [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is a platform for building high performance data integration solutions, including packages that provide extract, transform, and load (ETL) processing for data warehousing.|
|![Analysis Services](../sql-server/media/analysis-services.png "Analysis Services")|**[Analysis Services](https://docs.microsoft.com/analysis-services/analysis-services-overview)**<br /><br /> [!INCLUDE[ssASnoversion_md](../includes/ssasnoversion-md.md)] is an analytical data platform and toolset for personal, team, and corporate business intelligence. Servers and client designers support traditional OLAP solutions, new tabular modeling solutions, as well as self-service analytics and collaboration using [!INCLUDE[ssGemini](../includes/ssgemini-md.md)], Excel, and a SharePoint Server environment. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] also includes Data Mining so that you can uncover the patterns and relationships hidden inside large volumes of data.|    
|![Reporting Services](../sql-server/media/reporting-services.png "Reporting Services")|**[Reporting Services](../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md)**<br /><br /> Reporting Services delivers enterprise, Web-enabled reporting functionality.  You can create reports that draw content from a variety of data sources, publish reports in various formats, and centrally manage security and subscriptions.|
|![Replication services](../sql-server/media/replication-services.png "Replication services")|**[Replication](../relational-databases/replication/sql-server-replication.md)**<br /><br /> Replication is a set of technologies for copying and distributing data and database objects from one database to another, and then synchronizing between databases to maintain consistency. By using replication, you can distribute data to different locations and to remote or mobile users by means of local and wide area networks, dial-up connections, wireless connections, and the Internet.|
|![Data Quality Services](../sql-server/media/data-quality-services.png "Data Quality Services")|**[Data Quality Services](../data-quality-services/data-quality-services.md)**<br /><br /> SQL Server Data Quality Services (DQS) provides you with a knowledge-driven data cleansing solution. DQS enables you to build a knowledge base, and then use that knowledge base to perform data correction and deduplication on your data, using both computer-assisted and interactive means. You can use cloud-based reference data services, and you can build a data management solution that integrates DQS with SQL Server Integration Services and Master Data Services.|
|![Master Data Services](../sql-server/media/master-data-services.png)|**[Master Data Services](../master-data-services/master-data-services-installation-and-configuration.md)**<br /><br /> [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] solution for master data management. A solution built on [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] helps ensure that reporting and analysis is based on the right information. Using [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you create a central repository for your master data and maintain an auditable, securable record of that data as it changes over time.|
| &nbsp; | &nbsp; |

::: moniker range="= sql-server-2016 || = sqlallproducts-allversions"
## Migrate and move data, in version 2016

- [Import and Export Data with the SQL Server Import and Export Wizard](../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md)
- [Migrate your SQL Server database to Azure SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-migrate-your-sql-server-database)
- [Microsoft Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595)
- [Azure migration journey - assess, migrate, optimize](https://azure.microsoft.com/migration/)
::: moniker-end

::: moniker range=">= sql-server-2017 || >= sql-server-linux-2017 || = sqlallproducts-allversions"
## Migrate and move data

- [Import and Export Data with the SQL Server Import and Export Wizard](../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md)
- [Migrate data to SQL Server on Linux](../linux/sql-server-linux-migrate-overview.md)
- [Migrate your SQL Server database to Azure SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-migrate-your-sql-server-database)
- [Microsoft Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595)
- [Import data from Excel to SQL Server or SQL Database](https://docs.microsoft.com/sql/relational-databases/import-export/import-data-from-excel-to-sql?view=sql-server-2017)
::: moniker-end

## Update your version of SQL Server

- [SQL Server Update Center](https://msdn.microsoft.com/library/ff803383.aspx) links and information for all supported versions

## Samples

- [Wide World Importers sample database](https://docs.microsoft.com/sql/samples/wide-world-importers-what-is)
- [AdventureWorks sample databases and scripts for SQL Server 2016](https://docs.microsoft.com/sql/samples/sql-samples-where-are) 
- [SQL Server samples on GitHub](https://github.com/Microsoft/sql-server-samples)

## <a name="previous-versions-gm66"></a> SQL Server 2014, 2012, 2008, 2005 previous versions

[!INCLUDE[previous-versions](../includes/paragraph-content/previous-versions-archive-documentation-sql-server.md)]

## Versioning control for SQL documentation

The _versioning control_ on this :::no-loc text="Docs"::: webpage is above the table of contents. For information about how you can use the versioning control and exactly what it does, see:

- [Versioning system for SQL documentation](versioning-system-monikers-ui-sql-server.md)

<!--
The following includes/ files contain their own H2 headers.
-->

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
