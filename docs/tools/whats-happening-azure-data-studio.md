---
title: What's Happening with Azure Data Studio
description: Learn about the Azure Data Studio retirement, and the recommended replacement options.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: tsiddique, roblescarlos
ms.date: 06/09/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - deprecation-announcement
---

# What's happening with Azure Data Studio

Azure Data Studio is retired as of **February 28, 2026** and no longer receives updates or security fixes. Migrate to [Visual Studio Code](https://code.visualstudio.com/download) with the [MSSQL extension](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) for continued support. Your existing queries, scripts, and database projects work in Visual Studio Code without conversion.

The MSSQL extension for Visual Studio Code includes schema management, query execution, AI-powered assistance, and integrations for source control and CI/CD workflows. For a complete list of features, see [MSSQL extension features](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md#features).

## Replacement options

The following replacement options are available for Azure Data Studio.

### [App / SQL developer](#tab/dev)

Use the [MSSQL extension for Visual Studio Code](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) for daily work. Queries, scripts, and SQL database projects work without requiring conversion.

- Visual Studio Code includes schema design tools, IntelliSense, built-in Git integration, and CI/CD workflows.

- Continue storing SQL database projects in source control. Open them directly in Visual Studio Code with the MSSQL extension, or in Visual Studio with SSDT.

- [Schema Compare](visual-studio-code-extensions/mssql/mssql-schema-compare.md), [Schema Designer](visual-studio-code-extensions/mssql/mssql-schema-designer.md), and [GitHub Copilot integration](visual-studio-code-extensions/github-copilot/overview.md) are available in the MSSQL extension for Visual Studio Code.

For a full list of features, see [MSSQL extension features](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md#features).

### [Database administrator (DBA)](#tab/dba)

The MSSQL extension for Visual Studio Code includes:

- [Database operations](visual-studio-code-extensions/mssql/mssql-database-operations.md): Create, [back up and restore](visual-studio-code-extensions/mssql/mssql-database-operations.md#backup-database) databases, rename, and drop databases. Search database objects, and [import flat files](visual-studio-code-extensions/mssql/mssql-database-operations.md#import-flat-file).

- [Query Profiler](visual-studio-code-extensions/mssql/mssql-query-profiler.md): Capture real-time database activity using Extended Events.

- [Data-tier Application (DACPAC and BACPAC) import and export](visual-studio-code-extensions/mssql/mssql-data-tier-application.md): Deploy, extract, import, and export DACPAC and BACPAC files. Also available via [SqlPackage](sqlpackage/sqlpackage.md) CLI.

- [Schema Compare](visual-studio-code-extensions/mssql/mssql-schema-compare.md): Compare and synchronize schemas between databases, DACPACs, or SQL projects.

- [SQL Notebooks](visual-studio-code-extensions/mssql/mssql-sql-notebooks.md): Jupyter-based SQL notebooks for documenting runbooks, troubleshooting steps, and operational procedures.

Keep job scheduling and classic administration tasks in [SQL Server Management Studio (SSMS)](/ssms), which remains the supported home for SQL Server Agent and general administration.

For migration assessment, use [SQL Server enabled by Azure Arc migration assessment](../sql-server/azure-arc/migration-assessment.md).

For a full list of features, see [MSSQL extension features](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md#features).

### [Cross-database developer](#tab/xplat)

Replace Azure Data Studio extensions with their Visual Studio Code equivalents:

- **PostgreSQL**: [PostgreSQL extension for Visual Studio Code](/azure/postgresql/extensions/vs-code-extension/overview)
- **Azure Cosmos DB**: [Azure Databases for Visual Studio Code](/azure/cosmos-db/visual-studio-code-extension) (Mongo API)
- **MySQL**: Watch Azure Marketplace for a forthcoming MySQL extension

---

### Migration options

Use the dedicated migration tooling for your target: Azure SQL Managed Instance, SQL Server on Azure VMs, or Azure SQL Database. These tools replace the Azure SQL migration extension in Azure Data Studio.

<a id="key-benefits-of-migrating-to-visual-studio-code"></a>
<a id="migration-plan"></a>
<a id="modern-development-environment"></a>
<a id="feature-set-for-sql-development"></a>
<a id="cross-platform-compatibility"></a>
<a id="streamlined-workflows"></a>

## Migrate to Visual Studio Code

Like Azure Data Studio, Visual Studio Code runs on **Windows**, **macOS**, and **Linux**. It also supports real-time collaboration with [Live Share](https://marketplace.visualstudio.com/items?itemName=MS-vsliveshare.vsliveshare) and integrates with source control and CI/CD workflows.

> [!NOTE]  
> Visual Studio Code with the MSSQL extension primarily supports SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

1. **Install Visual Studio Code and the MSSQL extension**:

   - Download and install [Visual Studio Code](https://code.visualstudio.com/download).
   - Install the [MSSQL extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) from the Visual Studio Code Marketplace.

1. **Open your existing work**:

   - Open SQL database projects directly in Visual Studio Code. No conversion is needed.
   - Use the same queries and scripts from Azure Data Studio.

1. **Replace Azure Data Studio extensions**: See the following tables for equivalent tools.

<a id="recommended-alternatives-for-sql-server-capabilities-in-azure-data-studio"></a>

### Recommended alternatives for SQL Server capabilities

| Azure Data Studio extension | Description | Replacement |
| --- | --- | --- |
| SQL Server Agent | Manage and automate SQL Server Agent jobs. | [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms). |
| SQL Server Profiler | Trace and monitor SQL Server activity. | [Query Profiler](visual-studio-code-extensions/mssql/mssql-query-profiler.md) in the MSSQL extension for Visual Studio Code, and [XEvent Profiler](../relational-databases/extended-events/use-the-ssms-xe-profiler.md) in SSMS. |
| Database administration | Tools for managing databases on Windows. | [Database operations](visual-studio-code-extensions/mssql/mssql-database-operations.md) in the MSSQL extension for Visual Studio Code (create, back up, restore, rename, drop, search, and scripting). [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) for full administration. |
| Schema management | Compare and synchronize database schemas. | [Schema Compare](visual-studio-code-extensions/mssql/mssql-schema-compare.md), [Schema Designer](visual-studio-code-extensions/mssql/mssql-schema-designer.md), and [GitHub Copilot integration in Schema Designer](visual-studio-code-extensions/mssql/mssql-schema-designer-copilot.md) in the MSSQL extension for Visual Studio Code. Also available in [SQL Database Projects extension](visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md) and SQL Server Data Tools (SSDT). |
| Flat-file import | Import `.txt` and `.csv` files into databases. | [Import flat file](visual-studio-code-extensions/mssql/mssql-database-operations.md#import-flat-file) in the MSSQL extension for Visual Studio Code. Bulk insert and PowerShell are also available. |
| DACPAC import/export | Deploy and extract DACPAC files. | [Data-tier Application (DACPAC and BACPAC) import and export](visual-studio-code-extensions/mssql/mssql-data-tier-application.md) in the MSSQL extension for Visual Studio Code, and SqlPackage CLI from the command line. |
| SQL Server assessment | Assess an existing SQL Server data estate to prepare for migration. | [Assess migration readiness with SQL Server enabled by Azure Arc](../sql-server/azure-arc/migration-assessment.md). |
| Azure SQL migration | Migrate SQL Server to Azure SQL. | Alternative migration tools for [Azure SQL Managed Instance](/data-migration/sql-server/managed-instance/overview#migration-tools), [SQL Server on Azure VMs](/data-migration/sql-server/virtual-machines/overview#migrate), and [Azure SQL Database](/data-migration/sql-server/database/overview#migration-tools). |
| SQL database projects | Create, manage, and deploy SQL database projects. | Fully supported in the [SQL Database Projects extension](visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md) and Visual Studio. |

<a id="alternative-azure-data-studio-capabilities"></a>

### Alternatives for non-SQL Server capabilities

| Azure Data Studio extension | Description | Replacement |
| --- | --- | --- |
| **PostgreSQL** | Manage PostgreSQL databases. | [PostgreSQL extension for Visual Studio Code](/azure/postgresql/extensions/vs-code-extension/overview) |
| **MySQL** | Manage MySQL databases. | Pending announcement |
| **Azure Cosmos DB** | Manage Azure Cosmos DB API for MongoDB. | [Azure Databases for Visual Studio Code](/azure/cosmos-db/visual-studio-code-extension) |
| **Azure Cosmos DB Migration for MongoDB** | Migrate MongoDB to Azure Cosmos DB. | Pending announcement |

## Why retire Azure Data Studio?

Retiring Azure Data Studio consolidates SQL development tools into Visual Studio Code and other supported tools like [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms). This allows the product team to focus investment on fewer, more capable tools.

<a id="available-resources"></a>

## Resources

| Resource | Description |
| --- | --- |
| [MSSQL extension documentation](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) | Tutorials and guides for the MSSQL extension for Visual Studio Code. |
| [Community support](https://stackoverflow.com/questions/tagged/visual-studio-code) | Visual Studio Code community and Stack Overflow. |
| [GitHub issues](https://github.com/microsoft/vscode-mssql/issues) | Submit feature requests or report bugs for the MSSQL extension. |

## Frequently asked questions (FAQ)

Here are answers to questions about the Azure Data Studio deprecation and migration to Visual Studio Code.

<a id="what-happens-to-azure-data-studio-after-february-28-2026"></a>

### What happens to Azure Data Studio after retirement?

Azure Data Studio retired on **February 28, 2026** and is no longer supported. It no longer receives updates, security patches, or maintenance. Migrate to Visual Studio Code with the [MSSQL extension](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) for continued support.

<a id="can-my-queries-and-scripts-work-in-visual-studio-code"></a>
<a id="what-happens-to-existing-database-projects-in-azure-data-studio"></a>
<a id="is-there-a-deadline-for-migrating"></a>

### Can my queries, scripts, and database projects work in Visual Studio Code?

Yes. Open SQL database projects, queries, and scripts in Visual Studio Code without conversion.

<a id="are-there-plans-for-missing-features-like-sql-server-agent"></a>
<a id="what-benefits-does-visual-studio-code-offer-over-azure-data-studio"></a>

### What about extensions not yet available in Visual Studio Code?

Refer to the [alternatives table](#recommended-alternatives-for-sql-server-capabilities) for replacements. For SQL Server Agent and full administration, use [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).

### How do I install the MSSQL extension for Visual Studio Code?

Install it from the [Visual Studio Code Marketplace](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql). Detailed steps are available in the [MSSQL extension documentation](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md#install-the-mssql-extension-in-visual-studio-code).

## Related content

- [MSSQL extension for Visual Studio Code](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)
- [Download Visual Studio Code](https://code.visualstudio.com/download)
- [Visual Studio Code extensions](https://marketplace.visualstudio.com/VSCode)
