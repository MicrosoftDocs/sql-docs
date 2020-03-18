---
title: Download SQL Server Data Tools (SSDT)
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssdt
ms.topic: conceptual
keywords: "install ssdt, download ssdt, latest ssdt"
ms.assetid: b0fc4987-d260-4d0a-9dd1-98099835b361
author: markingmyname
ms.author: maghan
manager: jroth
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 02/20/2020
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||=azuresqldb-mi-current"
---

# Download SQL Server Data Tools (SSDT) for Visual Studio

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md.md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

**SQL Server Data Tools (SSDT)** is a modern development tool for building SQL Server relational databases, Azure SQL Databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in Visual Studio.

## SSDT for Visual Studio 2019

### Changes in SSDT for Visual Studio 2019

The core SSDT functionality to create database projects has remained integral to Visual Studio.

With Visual Studio 2019, the required functionality to enable Analysis Services, Integration Services, and Reporting Services projects has moved into the respective Visual Studio (VSIX) extensions only.

> [!NOTE]
> There's no SSDT standalone installer for Visual Studio 2019.

### Install SSDT with Visual Studio 2019

If [Visual Studio 2019](https://docs.microsoft.com/visualstudio/install/install-visual-studio?view=vs-2019) is already installed, you can edit the list of workloads to include SSDT.

* For SQL Database projects, select **SQL Server Data Tools** under **Data storage and processing**.

   ![Data storage and processing workload](../ssdt/media/download-sql-server-data-tools-ssdt/data-workload-2019.png)

* For Analysis Services, Integration Services, or Reporting Services projects, you can install the appropriate [extensions](https://docs.microsoft.com/visualstudio/ide/finding-and-using-visual-studio-extensions) from either *Tools > Extensions and Updates* or from the [Marketplace](https://marketplace.visualstudio.com/search?term=services&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance).

If you don’t have Visual Studio 2019 installed, then you can download and install [Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/). 

* For SQL Database projects, select **SQL Server Data Tools** under **Data storage and processing** in the list of workloads during installation.

* For Analysis Services, Integration Services, or Reporting Services projects, you can install the appropriate [extensions](https://docs.microsoft.com/visualstudio/ide/finding-and-using-visual-studio-extensions) from either *Tools > Extensions and Updates* or from the [Marketplace](https://marketplace.visualstudio.com/search?term=services&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance).

### Extensions
* [Analysis Services Projects](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects)
* [Integration Services Projects](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects)
* [Reporting Services Projects](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio)

### Release Notes

For a complete list of changes, see [Release notes for SQL Server Data Tools (SSDT)](release-notes-ssdt.md).

### Considerations and limitations

* You can’t install the community version offline

## Offline install

To install Visual Studio when you’re not connected to the internet, see [Create a network installation of Visual Studio 2019](https://docs.microsoft.com/visualstudio/install/create-a-network-installation-of-visual-studio).

## Supported SQL versions

|Project Templates|SQL Platforms Supported|
|-------------------|--------------------|
|Relational databases| SQL Server 2005\* - SQL Server 2019<br> (use SSDT 17.x or SSDT for Visual Studio 2017 to connect to [SQL Server on Linux](../linux/sql-server-linux-overview.md))<br /><br />Azure SQL Database<br /><br />Azure SQL Data Warehouse (supports queries only; database projects aren't yet supported)<br /><br /> \* SQL Server 2005/SQL Server 2008 support  is deprecated,<br /><br /> move to an officially supported SQL version|
|Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 - SQL Server 2019|
|Integration Services packages| SQL Server 2012 - SQL Server 2019|

## Previous versions

To download and install SSDT for Visual Studio 2017, or an older version of SSDT, see [Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)](previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md).

## Next steps

After installing SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT.

* [Project-Oriented Offline Database Development](project-oriented-offline-database-development.md)

* [SSIS Tutorial: Create a Simple ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md)

* [Analysis Services tutorials](https://docs.microsoft.com/analysis-services/analysis-services-tutorials-ssas)

* [Create a Basic Table Report (SSRS Tutorial)](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

## See Also

* [SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt) 

* [SSDT Team Blog](https://blogs.msdn.com/b/ssdt/)

* [DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)

* [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)
