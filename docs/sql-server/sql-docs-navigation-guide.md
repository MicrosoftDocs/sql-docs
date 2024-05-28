---
title: SQL Server docs navigation tips
description: Tips and tricks for navigating the SQL Server technical documentation - explains such things as the hub page, the table of contents, the header, as well as how to use the breadcrumbs and how to use the version filter.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest, wiassaf
ms.date: 05/28/2024
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
---
# SQL Server docs navigation guide

This article provides some tips and tricks for navigating the SQL Server technical documentation space.

## Hub page

The SQL Server hub page can be found at [https://aka.ms/sqldocs](../index.yml?WT.mc_id=akams) and is the entry point for finding relevant SQL Server content.

You can always navigate back to this page by selecting **SQL Docs** from the header at the top of every page within the SQL Server technical documentation set:

:::image type="content" source="media/sql-server-docs-navigation-guide/sql-docs-in-header.png" alt-text="Screenshot of the SQL Docs menu option in the header.":::

## Offline documentation

If you would like to view the SQL Server documentation on an offline system, you have two options to do so. You can either create a PDF wherever you are in the SQL Server technical documentation, or you can download the offline content using [SQL Server offline Help Viewer](./sql-server-offline-documentation.md).

If you'd like to create a PDF, select the **Download PDF** link found at the bottom of every table of contents.

:::image type="content" source="media/sql-server-docs-navigation-guide/download-pdf.png" alt-text="Screenshot showing the option to download content as a PDF.":::

## TOC symbols

Entries in the table of contents (TOC) that have a `>` at the end of the entry indicate that you'll be taken to technical documentation with a different table of contents.

:::image type="content" source="media/sql-server-docs-navigation-guide/single-carrots-in-sql-docs-toc.png" alt-text="Screenshot showing a single chevron item in table of contents.":::

Entries in the TOC marked with `>>` indicate that you'll be taken to a different website.

:::image type="content" source="media/sql-server-docs-navigation-guide/double-carrots-in-sql-docs-toc.png" alt-text="Screenshot showing double-chevron table of contents navigation markers.":::

If you navigate to one of these pages, you can come back to the main SQL Server technical page, and table of contents, by selecting the **Welcome to SQL Server >** entry found at the top of each of the table of contents.

:::image type="content" source="media/sql-server-docs-navigation-guide/navigate-back-to-sql-toc.png" alt-text="Screenshot showing the option to navigate back to SQL Docs table of contents.":::

## TOC search

You can search the entries in the table of contents using the filter search box at the top:

:::image type="content" source="media/sql-server-docs-navigation-guide/sql-docs-toc-filter.gif" alt-text="Screenshot showing the option to use the filter box.":::

## Version filter

The SQL Server technical documentation provides content for several supported versions and flavors of SQL Server. Features can vary between versions and flavors of SQL Server, and as such, sometimes the content itself can vary.

You can use the [version filter](versioning-system-monikers-ui-sql-server.md) to ensure that you're seeing content for the appropriate version and flavor of SQL Server:

:::image type="content" source="media/sql-server-docs-navigation-guide/sql-docs-version-filter.gif" alt-text="Screenshot showing the SQL Docs version filter.":::

## Breadcrumbs

Breadcrumbs can be found below the header and above the table of contents, and indicate where the current article is located in the table of contents.  Not only does this help set the context to what type of content you're reading, but it also allows you to navigate back up the table of contents tree:

:::image type="content" source="media/sql-server-docs-navigation-guide/sql-docs-bread-crumbs.gif" alt-text="Screenshot showing the SQL Docs breadcrumbs.":::

## Article section navigation

The right-hand navigation pane allows you to quickly navigate to sections within an article, and identify your location within the article.

:::image type="content" source="media/sql-server-docs-navigation-guide/sql-docs-right-hand-navigation.gif" alt-text="Screenshot showing right-hand navigation.":::

## <a id="applies-to"></a> What the "Applies to" options mean

At the top of every article in SQL Docs, you'll see a section after the heading to explain which products the article applies to.

The SQL Docs content covers several product lines that work on-premises and in the cloud, and it can be confusing to differentiate between products. The following table describes several common options, but isn't exhaustive.

| Product | Deployment model | Description |
| --- | --- | --- |
| **SQL Server** | On-premises <sup>1</sup>, Azure Virtual Machines <sup>3</sup>, Linux containers <sup>1</sup> | This is [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] that you have full control over. You can install SQL Server on Windows or [Linux](../linux/sql-server-linux-overview.md), deploy it in [a Linux container](../linux/sql-server-linux-overview.md#container-images), or deploy it on an [Azure Virtual Machine](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) or other virtual machine platform. You may previously have referred to this as the *boxed product*.<br /><br />Supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] depend on your license agreement, but for the purposes of this documentation, we mean [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions. Documentation for [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] and previous versions is available at [Previous versions of SQL Server documentation](previous-versions-sql-server.md). To find out which versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] are currently supported, see [SQL Server end of support options](end-of-support/sql-server-end-of-support-overview.md).<br /><br />**Important:** Doesn't include Azure SQL support. If an article relates to [Azure SQL products](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview), those products are listed separately in the "Applies to" section. |
| **Azure SQL Database** | Fully managed <sup>2</sup> | [Azure SQL Database](/azure/azure-sql/database/) is a single database, or part of an elastic pool. SQL Database is a fully managed platform-as-a-service (PaaS) database engine that handles most database management functions such as upgrading, patching, backups, and monitoring, without user involvement. An Azure SQL logical server provide server-level principals such as logins to multiple Azuze SQL databases.|
| **Azure SQL Managed Instance** | Fully managed <sup>2</sup> | [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/) is a fully managed database instance. SQL Managed Instance combines the broadest SQL Server database engine compatibility with all the benefits of a fully managed and evergreen platform-as-a-service (PaaS). SQL Managed Instance has near 100 percent compatibility with the latest SQL Server (Enterprise Edition) database engine.<br /><br />**Important:** Although SQL Managed Instance shares many features with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], there are [some incompatibilities](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server), especially prior to [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. |
| **Analytics Platform System (PDW)** | On-premises, Fully managed <sup>2</sup> | [Microsoft Analytics Platform System (APS)](../analytics-platform-system/home-analytics-platform-system-aps-pdw.md) is a data platform designed for data warehousing and Big Data analytics, offers deep data integration, high-speed query processing, highly scalable storage, and simple maintenance for your end-to-end business intelligence solutions. Analytics Platform System hosts SQL Server Parallel Data Warehouse (PDW), which is the software that runs the massively parallel processing (MPP) data warehouse. |
| **Azure SQL Edge** | *Connected* mode using Azure IoT Edge, or *disconnected* mode in a Linux container | [Azure SQL Edge](/azure/azure-sql-edge/overview) is an optimized relational database engine geared for IoT and IoT Edge deployments. It provides capabilities to create a high-performance data storage and processing layer for IoT applications and solutions. It shares the same database engine as [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and includes additional time-series and analytics features. |
| **Azure Database for MariaDB** | Fully managed <sup>2</sup> | A relational database service based on the MariaDB open-source database engine. A fully managed database-as-a-service (DBaaS) that can handle mission-critical workloads with predictable performance, security, high availability, and dynamic scalability. |
| **Azure Database for MySQL** | Fully managed <sup>2</sup> | A relational database service based on the MySQL open-source database engine. A fully managed database-as-a-service (DBaaS) that can handle mission-critical workloads with predictable performance, security, high availability, and dynamic scalability. |
| **Azure Database for PostgreSQL** | Fully managed <sup>2</sup> | A relational database service based on the PostgreSQL open-source database engine. A fully managed database-as-a-service (DBaaS) that can handle mission-critical workloads with predictable performance, security, high availability, and dynamic scalability. |
| **Azure Synapse Analytics** | Fully managed <sup>2</sup> | [Azure Synapse Analytics](/azure/synapse-analytics/overview-what-is) is an enterprise analytics service that accelerates time to insight across data warehouses and big data systems. Azure Synapse Analytics brings together the best of SQL technologies used in enterprise data warehousing, Spark technologies used for big data, Data Explorer for log and time series analytics, Pipelines for data integration and ETL/ELT, and deep integration with other Azure services such as Power BI, Cosmos DB, and Azure Machine Learning. The [Azure Synapse SQL](/azure/synapse-analytics/sql/overview-architecture) features provide scale-out architecture for data processing in the form of dedicated SQL pools (formerly SQL DW) and serverless SQL pools. <br /><br />The behavior and Azure portal experience can differ between a dedicated SQL pool in Azure Synapse workspaces, or a standalone dedicated SQL pool (formerly SQL DW) in a logical SQL server. |
| **[!INCLUDE [fabric](../includes/fabric.md)]** | Fully managed <sup>4</sup> | [Microsoft Fabric](/fabric/get-started/microsoft-fabric-overview) is an all-in-one, Software-as-a-Service analytics solution for enterprises that covers everything from data movement to data science, real-time analytics, data warehousing, and business intelligence. [[!INCLUDE [fabric](../includes/fabric.md)]](/fabric/data-warehouse/data-warehousing) provides two distinct data warehousing experiences. Each [!INCLUDE [fabric](../includes/fabric.md)] Lakehouse automatically includes a **[!INCLUDE [fabric-se](../includes/fabric-se.md)]** to enable data engineers to access a relational layer on top of physical data in the Lakehouse, thanks to automatic schema discovery. A Synapse Data Warehouse or **[!INCLUDE [fabric-dw](../includes/fabric-dw.md)]** in [!INCLUDE [fabric](../includes/fabric.md)] provides a "traditional" data warehouse and supports the full transactional T-SQL capabilities you would expect from an enterprise data warehouse. Either data warehousing experience exposes data to analysis and reporting tools using T-SQL/TDS end-point. |

<sup>1</sup> Can be Azure Arc-enabled

<sup>2</sup> Platform-as-a-service (PaaS)

<sup>3</sup> Infrastructure-as-a-service (IaaS)

<sup>4</sup> Software-as-a-service (SaaS)

## Submit docs feedback

If you find something wrong within an article, you can submit feedback to the SQL Content team for that article by scrolling down to the bottom of the page and selecting **Content feedback**.

:::image type="content" source="media/sql-server-get-help/git-issues.png" alt-text="Screenshot showing GitHub issue content feedback.":::

You can also submit general documentation feedback and suggestions at <https://aka.ms/sqldocsfeedback>.

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

:::image type="content" source="media/sql-server-docs-navigation-guide/edit-sql-docs.gif" alt-text="Screenshot showing the option to edit SQL Docs.":::

## Next steps

- Get started with the [SQL Server technical documentation](index.yml).
- For more information about submitting feedback for or getting help with SQL Server, see the [Get help](sql-server-get-help.md) page.
- To quickly access all the quickstarts and tutorials, see [Educational SQL resources](../sql-server/educational-sql-resources.yml).
