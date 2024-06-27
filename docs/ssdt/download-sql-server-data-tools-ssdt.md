---
title: Install SQL Server Data Tools (SSDT)
description: "Learn about SQL Server Data Tools (SSDT). See how to install this database development tool set with Visual Studio 2019 and 2022."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 05/30/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
keywords:
  - install ssdt
  - download ssdt
  - latest ssdt
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=azuresqldb-mi-current"
---

# Install SQL Server Data Tools (SSDT) for Visual Studio

[!INCLUDE [sql-asdb-asa](../includes/applies-to-version/sql-asdb-asa.md)]

**SQL Server Data Tools (SSDT)** is a set of development tooling for building SQL Server databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy SQL objects with the same project concept as other application development tools.  The **SQL projects** capability extends to CI/CD pipelines, enabling you to automate the build and deployment of your database projects with the [SqlPackage CLI](../tools/sqlpackage/sqlpackage.md).

The release notes for SSDT and its components are available for [Visual Studio 2017, 2019, and 2022](sql-server-data-tools.md#release-notes).  An overview of the core SSDT functionality is provided in the [SSDT Overview](sql-server-data-tools.md#core-sql-server-data-tools).

> [!NOTE]  
> There's no SSDT standalone installer for Visual Studio. SSDT is installed as a Visual Studio component, both for [online installation](#install-ssdt-with-visual-studio-2022) and [offline installation](#offline-installation). Analysis Services, Integration Services, and Reporting Services projects are available as separate extensions for each version.


## Install SSDT for SQL projects with Visual Studio

If [Visual Studio 2022](/visualstudio/install/install-visual-studio?preserve-view=true&view=vs-2022) or [Visual Studio 2019](/visualstudio/install/install-visual-studio?preserve-view=true&view=vs-2019) is already installed, you can edit the list of workloads to include SSDT. If you don't have Visual Studio 2019/2022 installed, then you can download and install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/).

To modify the installed Visual Studio workloads to include SSDT, use the Visual Studio Installer.

1. Launch the Visual Studio Installer. In the Windows Start menu, you can search for "installer".

2. In the installer, select for the edition of Visual Studio that you want to add SSDT to, and then choose **Modify**.

3. Select **SQL Server Data Tools** under **Data storage and processing** in the list of workloads.

   :::image type="content" source="../ssdt/media/download-sql-server-data-tools-ssdt/data-workload-2022.png" alt-text="Screenshot of Data storage and processing workload 2022." lightbox="../ssdt/media/download-sql-server-data-tools-ssdt/data-workload-2022.png":::

### Visual Studio for Arm64

Visual Studio is available as a [native Arm64 application](/visualstudio/install/visual-studio-on-arm-devices) on Windows 11 Arm64. In Visual Studio 17.10 and later, SSDT is available for Arm64. To install or configure Visual Studio to include SSDT on an Arm64 device:

1. Install Visual Studio 17.10 or later on your Arm64 device.

2. In the installer, select the **Individual components** tab and search for **SQL Server Data Tools**.
   
      :::image type="content" source="../ssdt/media/download-sql-server-data-tools-ssdt/ssdt-component-install.png" alt-text="Screenshot of SQL Server Data Tools for Arm64." lightbox="../ssdt/media/download-sql-server-data-tools-ssdt/ssdt-component-install.png":::

3. Select **SQL Server Data Tools** and then choose **Modify**.

## Install extensions for Analysis Services, Integration Services, and Reporting Services

For Analysis Services, Integration Services, or Reporting Services projects, you can install the appropriate [extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions) from within Visual Studio with **Extensions** > **Manage Extensions** or from the [Marketplace](https://marketplace.visualstudio.com/search?term=services&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance).

### Extensions for Visual Studio 2022

- [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects2022)
- [Integration Services](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices)
- [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio2022)

### Extensions for Visual Studio 2019

- [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects)
- [Integration Services](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects)
- [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio)

## Supported SQL versions

### Supported SQL versions in Visual Studio 2022

| Project Templates | SQL Platforms Supported |
| --- | --- |
| Relational databases | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]<br /><br />Azure SQL Database, Azure SQL Managed Instance<br /><br />Azure Synapse Analytics Dedicated Pools <br /> Azure Synapse Analytics Serverless Pools (requires VS2022 17.7 see [the release notes](/visualstudio/releases/2022/release-notes-v17.7#support-for-serverless-sql-pool-in-ssdt)) |
| Analysis Services models<br /><br />Reporting Services reports | SQL Server 2016 - SQL Server 2022 |
| Integration Services packages | SQL Server 2019 - SQL Server 2022 |

### Supported SQL versions in Visual Studio 2019

| Project Templates | SQL Platforms Supported |
| --- | --- |
| Relational databases | SQL Server 2012 - SQL Server 2019<br /><br />Azure SQL Database, Azure SQL Managed Instance<br /><br />Azure Synapse Analytics (dedicated pools only) |
| Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 - SQL Server 2019 |
| Integration Services packages | SQL Server 2012 - SQL Server 2022 |

## Offline installation

For scenarios where offline installation is required, such as low bandwidth or isolated networks, SSDT is available for offline installation.  Two approaches are available:

- For a single machine, [Download All, then install](/visualstudio/install/create-an-offline-installation-of-visual-studio#use-the-download-all-then-install-feature)
- For installation on one or more machines, [use the Visual Studio bootstrapper from the command line](/visualstudio/install/create-an-offline-installation-of-visual-studio#use-the-command-line-to-create-a-local-layout)

For more details you can follow the [Step-by-Step Guidelines for Offline Installation](/visualstudio/install/create-an-offline-installation-of-visual-studio)

## License terms for Visual Studio

To understand the license terms and use cases for Visual Studio, refer to [Visual Studio License Directory](https://visualstudio.microsoft.com/license-terms/). For example, if you are using the Community Edition of Visual Studio for SQL Server Data Tools, review the EULA for that specific edition of Visual Studio in the Visual Studio License Directory.

## Previous versions

To download and install SSDT for Visual Studio 2017, or an older version of SSDT, see [Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)](previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md).

## See also

- [SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)

- [SSDT Team Blog](/archive/blogs/ssdt/)

- [DACFx API Reference](/previous-versions/sql/sql-server-2014/dn645454(v=sql.120))

- [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)

## Next steps

After installation of SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT.

- [Project-Oriented Offline Database Development](project-oriented-offline-database-development.md)

- [SSIS Tutorial: Create a Simple ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md)

- [Analysis Services tutorials](/analysis-services/analysis-services-tutorials-ssas)

- [Create a Basic Table Report (SSRS Tutorial)](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
