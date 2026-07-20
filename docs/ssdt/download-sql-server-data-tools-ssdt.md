---
title: "Install SQL Server Data Tools (SSDT)"
description: "Learn about SQL Server Data Tools (SSDT). See how to install this database development tool set with Visual Studio 2019, 2022, and 2026."
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 02/06/2026
ms.service: sql
ms.subservice: ssdt
ms.topic: install-set-up-deploy
ms.custom:
  - ignite-2025
keywords:
  - install ssdt
  - download ssdt
  - latest ssdt
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Install SQL Server Data Tools (SSDT) for Visual Studio

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Synapse Analytics FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

**SQL Server Data Tools (SSDT)** is a set of development tooling for building SQL Server databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy SQL objects with the same project concept as other application development tools. The **SQL projects** capability extends to CI/CD pipelines, enabling you to automate the build and deployment of your database projects with the [SqlPackage CLI](../tools/sqlpackage/sqlpackage.md).

The release notes for SSDT and its components are available for [Visual Studio 2017, 2019, 2022, and 2026](sql-server-data-tools.md#release-notes). An overview of the core SSDT functionality is provided in the [SSDT Overview](sql-server-data-tools.md#core-sql-server-data-tools).

:::image type="content" source="media/download-sql-server-data-tools-ssdt/install-layout.png" alt-text="Screenshot of graphic with SQL Server Data Tools component and three extensions.":::

SSDT is installed as a Visual Studio component, both for [online installation](#install-ssdt-with-visual-studio) and [offline installation](#offline-installation). Analysis Services, Integration Services, and Reporting Services projects are available as separate extensions for each version.

> [!NOTE]  
> SDK-style SQL projects in Visual Studio are available as part of the **SQL Server Data Tools, SDK-style (preview)** feature for Visual Studio 2022, separate from the original SSDT. The SDK-style project format is based on the new SDK-style projects introduced in .NET Core and is the format used by the SQL Database Projects extension for Visual Studio Code. For more information, see [SQL Server Data Tools, SDK-style (preview)](sql-server-data-tools-sdk-style.md).

## Install SSDT with Visual Studio

If [Visual Studio 2026](/visualstudio/install/install-visual-studio?preserve-view=true&view=vs-2026) or [Visual Studio 2022](/visualstudio/install/install-visual-studio?preserve-view=true&view=vs-2022) is already installed, you can edit the list of workloads to include SSDT. If you don't have Visual Studio 2022 or 2026 installed, then you can download and install [Visual Studio 2026](https://visualstudio.microsoft.com/downloads/).

To modify the installed Visual Studio workloads to include SSDT, use the Visual Studio Installer.

1. Launch the Visual Studio Installer. In the Windows Start menu, you can search for "installer."

1. In the installer, select **Modify** for the version of Visual Studio to which you want to add SSDT.

1. Select **SQL Server Data Tools** under **Data storage and processing** in the list of workloads.

   :::image type="content" source="media/download-sql-server-data-tools-ssdt/data-workload-2022.png" alt-text="Screenshot of Data storage and processing workload 2022." lightbox="media/download-sql-server-data-tools-ssdt/data-workload-2022.png":::

### Visual Studio for Arm64

Visual Studio is available as a [native Arm64 application](/visualstudio/install/visual-studio-on-arm-devices) on Windows 11 Arm64. In Visual Studio 2026, SSDT is available for Arm64 with some limitations:

- IntelliSense and code completion aren't available for T-SQL files in SQL projects
- The T-SQL debugger isn't available
- Visual Studio can't connect to LocalDB

To install or configure Visual Studio to include SSDT on an Arm64 device:

1. Install Visual Studio 2026 or later on your Arm64 device.

1. In the installer, select the **Individual components** tab and search for **SQL Server Data Tools**.

   :::image type="content" source="media/download-sql-server-data-tools-ssdt/ssdt-component-install.png" alt-text="Screenshot of SQL Server Data Tools for Arm64." lightbox="media/download-sql-server-data-tools-ssdt/ssdt-component-install.png":::

1. Select **SQL Server Data Tools** and then choose **Modify**.

## Install extensions for Analysis Services, Integration Services, and Reporting Services

For Analysis Services (SSAS), Integration Services (SSIS), or Reporting Services (SSRS) projects, you can install the appropriate [extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions) from within Visual Studio with **Extensions** > **Manage Extensions** or from the [Marketplace](https://marketplace.visualstudio.com/search?term=services&target=VS&category=All%20categories&vsVersion=&sortBy=Relevance).

### [Visual Studio 2026 extensions](#tab/vs2026)

The extensions for Visual Studio 2022 and 2026 are shared:

- [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects2022)
- [Integration Services](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices)
- [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio2022)

### [Visual Studio 2022 extensions](#tab/vs2022)

The extensions for Visual Studio 2022 and 2026 are shared:

- [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects2022)
- [Integration Services](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices)
- [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio2022)

### [Visual Studio 2019 extensions](#tab/vs2019)

Extensions for Visual Studio 2019:

- [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects)
- [Integration Services](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects)
- [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio)

---

## Supported SQL versions

### [Supported SQL versions in Visual Studio 2026](#tab/vs2026)

Supported SQL versions in Visual Studio 2026:

| Project templates | SQL platforms supported |
| --- | --- |
| Relational databases | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql25-md.md)]<br /><br />Azure SQL Database, Azure SQL Managed Instance<br /><br />Azure Synapse Analytics Dedicated Pools<br />Azure Synapse Analytics Serverless Pools<br /><br />Warehouse in Microsoft Fabric<br />SQL database in Microsoft Fabric |
| Analysis Services models<br /><br />Reporting Services reports | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] - [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] |
| Integration Services packages | [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] - [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] |

### [Supported SQL versions in Visual Studio 2022](#tab/vs2022)

Supported SQL versions in Visual Studio 2022:

| Project templates | SQL platforms supported |
| --- | --- |
| Relational databases | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]<br /><br />Azure SQL Database, Azure SQL Managed Instance<br /><br />Azure Synapse Analytics Dedicated Pools<br />Azure Synapse Analytics Serverless Pools (requires VS2022 17.7) |
| Analysis Services models<br /><br />Reporting Services reports | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] |
| Integration Services packages | [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] |

### [Supported SQL versions in Visual Studio 2019](#tab/vs2019)

Supported SQL versions in Visual Studio 2019:

| Project templates | SQL platforms supported |
| --- | --- |
| Relational databases | [!INCLUDE [sssql11-md](../includes/sssql11-md.md)] - [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]<br /><br />Azure SQL Database, Azure SQL Managed Instance<br /><br />Azure Synapse Analytics (dedicated pools only) |
| Analysis Services models<br /><br />Reporting Services reports | [!INCLUDE [sql2008-md](../includes/sql2008-md.md)] - [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] |
| Integration Services packages | [!INCLUDE [sssql11-md](../includes/sssql11-md.md)] - [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] |

---

## Offline installation

For scenarios where offline installation is required, such as low bandwidth or isolated networks, SSDT is available for offline installation. Two approaches are available:

- For a single machine, [Download All, then install](/visualstudio/install/create-an-offline-installation-of-visual-studio#use-the-download-all-then-install-feature)
- For installation on one or more machines, [use the Visual Studio bootstrapper from the command line](/visualstudio/install/create-an-offline-installation-of-visual-studio#use-the-command-line-to-create-a-local-layout)

For more details, you can follow the [Step-by-Step Guidelines for Offline Installation](/visualstudio/install/create-an-offline-installation-of-visual-studio)

## License terms for Visual Studio

To understand the license terms and use cases for Visual Studio, refer to [Visual Studio License Directory](https://visualstudio.microsoft.com/license-terms/). For example, if you're using the Community Edition of Visual Studio for SQL Server Data Tools, review the end user licensing agreement (EULA) for that specific edition of Visual Studio in the Visual Studio License Directory.

## Previous versions

To download and install SSDT for Visual Studio 2017, or an older version of SSDT, see [Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)](previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md).

## Related content

- [SSDT Team Blog](/archive/blogs/ssdt/)
- [DACFx API Reference](/dotnet/api/microsoft.sqlserver.dac)
- [SQL Database Projects extension](../tools/visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)
- [What are SQL database projects?](../tools/sql-database-projects/sql-database-projects.md)
- [SSIS How to Create an ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md)
- [Analysis Services tutorials](/analysis-services/analysis-services-tutorials-ssas)
- [Create a basic table report (SSRS tutorial)](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
