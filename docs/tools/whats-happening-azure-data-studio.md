---
title: What's Happening with Azure Data Studio
description: Learn about the Azure Data Studio retirement, and the recommended replacement options.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: tsiddique, roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - deprecation-announcement
---

# What's happening with Azure Data Studio

Azure Data Studio is retired as of **February 28, 2026**. Migrate to [Visual Studio Code](https://code.visualstudio.com/download) for ongoing support and new features. This change consolidates SQL development tools into Visual Studio Code and other supported tools.

This article provides recommendations and resources to help you transition from Azure Data Studio to Visual Studio Code or alternative tools. You can continue to access all local files and database projects in Azure Data Studio, but it no longer receives updates or security fixes after **February 28, 2026**.

## Replacement options

The following replacement options are available for Azure Data Studio.

### [App / SQL developer](#tab/dev)

Use Visual Studio Code with the [MSSQL extension for Visual Studio Code](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) for daily work. Queries, scripts, and SQL database projects work without requiring conversion.

- Visual Studio Code includes schema design tools, IntelliSense, built-in Git integration, and CI/CD workflows.

- Continue storing SQL database projects in source control. Open them directly in Visual Studio Code with the MSSQL extension, or in Visual Studio with SSDT.

- [Schema Compare](visual-studio-code-extensions/mssql/mssql-schema-compare.md) and [Schema Designer](visual-studio-code-extensions/mssql/mssql-schema-designer.md) are available in the MSSQL extension for Visual Studio Code.

### [Database administrator (DBA)](#tab/dba)

The MSSQL extension for Visual Studio Code includes:

- [Database Operations (Preview)](visual-studio-code-extensions/mssql/mssql-database-operations.md): Create, back up, restore, rename, and drop databases. Search database objects, and [import flat files](visual-studio-code-extensions/mssql/mssql-database-operations.md#import-flat-file).

- [Query Profiler (Preview)](visual-studio-code-extensions/mssql/mssql-query-profiler.md): Capture real-time database activity using Extended Events.

Keep job scheduling and classic administration tasks in [SQL Server Management Studio (SSMS)](/ssms), which remains the supported home for SQL Server Agent and general administration.

For import, export, and assessment work:

- Use [SqlPackage](sqlpackage/sqlpackage.md) and the [Data-tier Application (Preview)](visual-studio-code-extensions/mssql/mssql-data-tier-application.md) for DACPAC tasks.
- Run the [migration assessment](../sql-server/azure-arc/migration-assessment.md) for SQL Server enabled by Azure Arc.

### [Cross-database developer](#tab/xplat)

Replace Azure Data Studio extensions with their Visual Studio Code equivalents:

- **PostgreSQL**: [PostgreSQL extension for Visual Studio Code preview](/azure/postgresql/extensions/vs-code-extension/overview)
- **Azure Cosmos DB**: [Azure Databases for Visual Studio Code](/azure/cosmos-db/visual-studio-code-extension) (Mongo API)
- **MySQL**: Watch the Azure Marketplace for a forthcoming MySQL extension

---

### Migration options

Use the dedicated migration tooling for your target: Azure SQL Managed Instance, SQL Server on Azure VMs, or Azure SQL Database. These tools replace the Azure SQL migration extension in Azure Data Studio.

## Why retire Azure Data Studio?

Visual Studio Code is a widely used platform with community support and an extensive ecosystem of extensions. It provides schema management tools and other capabilities for database development.

Retiring Azure Data Studio simplifies the SQL tooling portfolio. It helps the product team focus investment on Visual Studio Code and other supported tools.

## Key benefits of migrating to Visual Studio Code

Visual Studio Code provides a modern, extensible environment for SQL development when you use it with the MSSQL extension and related tools.

### Modern development environment

- Visual Studio Code is a widely used code editor.
- Lightweight design with a large extension ecosystem.
- Regular updates and an active open-source community.
- Built-in features for editing, debugging, and source control.

### Feature set for SQL development

- **Query execution**: Execute queries with result grids, filtering, and other options.
- **Schema management**: Tools for visualizing, designing, and managing database schemas.
- **CI/CD integration**: Support for automated database deployments and updates.
- **Editor experience**: Query editor with suggestions and error highlighting.

### Cross-platform compatibility

- Like Azure Data Studio, Visual Studio Code runs on **Windows**, **macOS**, and **Linux**.
- Provides a consistent development environment across platforms.

### Streamlined workflows

- **CI/CD integration**: Supports workflows from development through deployment.
- **Cloud-related development**: Extensions are available for Azure services.
- **Collaboration tools**: Real-time collaboration with [Live Share](https://marketplace.visualstudio.com/items?itemName=MS-vsliveshare.vsliveshare).
- **Extension marketplace**: Large catalog of extensions for customizing workflows.

## Migration plan

Azure Data Studio users have diverse needs, from connecting to Azure SQL databases to using extensions for non-SQL Server-related capabilities.

> [!NOTE]  
> Visual Studio Code with the MSSQL extension primarily supports Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

1. **Install Visual Studio Code and the MSSQL extension**:

   - Download and install Visual Studio Code from the official website.
   - Install the MSSQL extension from the Visual Studio Code marketplace.

1. **Migrate database projects and queries**:

   - Open SQL database projects directly in Visual Studio Code without migration steps.
   - Queries and scripts from Azure Data Studio are compatible with Visual Studio Code.

1. **Explore additional features in Visual Studio Code**:

   - Use advanced schema management tools.
   - Use DevOps workflows, including CI/CD integration.

### Recommended alternatives for SQL Server capabilities in Azure Data Studio

| Azure Data Studio extension | Description | Replacement |
| --- | --- | --- |
| SQL Server Agent | Manage and automate SQL Server Agent jobs. | [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms). |
| SQL Server Profiler | Trace and monitor SQL Server activity. | [Query Profiler (Preview)](visual-studio-code-extensions/mssql/mssql-query-profiler.md) in the MSSQL extension for Visual Studio Code, and [XEvent Profiler](../relational-databases/extended-events/use-the-ssms-xe-profiler.md) in SSMS. |
| Database administration | Tools for managing databases on Windows. | [Database Operations (Preview)](visual-studio-code-extensions/mssql/mssql-database-operations.md) in the MSSQL extension for Visual Studio Code (create, back up, restore, rename, drop, search, and scripting). [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) for full administration. |
| Schema management | Compare and synchronize database schemas. | [Schema Compare (Preview)](visual-studio-code-extensions/mssql/mssql-schema-compare.md) and [Schema Designer](visual-studio-code-extensions/mssql/mssql-schema-designer.md) in the MSSQL extension for Visual Studio Code. Also available in [SQL Database Projects](visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md) and SQL Server Data Tools (SSDT). |
| Flat-file import | Import `.txt` and `.csv` files into databases. | [Import flat file (Preview)](visual-studio-code-extensions/mssql/mssql-database-operations.md#import-flat-file) in the MSSQL extension for Visual Studio Code. Bulk insert and PowerShell are also available. |
| DACPAC import/export | Deploy and extract DACPAC files. | [Data-tier Application (Preview)](visual-studio-code-extensions/mssql/mssql-data-tier-application.md) in the MSSQL extension for Visual Studio Code, and SqlPackage CLI from the command line. |
| SQL Server assessment | Assess an existing SQL Server data estate to prepare for migration. | [Assess migration readiness with SQL Server enabled by Azure Arc](../sql-server/azure-arc/migration-assessment.md). |
| Azure SQL migration | Migrate SQL Server to Azure SQL. | Alternative migration tools for [Azure SQL Managed Instance](/data-migration/sql-server/managed-instance/overview#migration-tools), [SQL Server on Azure VMs](/data-migration/sql-server/virtual-machines/overview#migrate), and [Azure SQL Database](/data-migration/sql-server/database/overview#migration-tools). |
| SQL database projects | Create, manage, and deploy SQL database projects. | Fully supported in the [MSSQL extension for Visual Studio Code](visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md) and Visual Studio. |

### Alternative Azure Data Studio capabilities

For users who relied on Azure Data Studio for non-SQL Server-related tasks (for example, **PostgreSQL**, **MySQL**, or **Azure Cosmos DB**), suitable Visual Studio Code alternatives are available.

#### Migration steps

1. Install the relevant Visual Studio Code extensions from the Marketplace.

1. Explore the recommended replacements:

   | Azure Data Studio extension | Description | Replacement |
   | --- | --- | --- |
   | **PostgreSQL** | Manage PostgreSQL databases. | [PostgreSQL extension for Visual Studio Code](/azure/postgresql/extensions/vs-code-extension/overview) |
   | **MySQL** | Manage MySQL databases. | Pending announcement |
   | **Azure Cosmos DB** | Manage Azure Cosmos DB API for MongoDB. | [Azure Databases for Visual Studio Code](/azure/cosmos-db/visual-studio-code-extension) |
   | **Azure Cosmos DB Migration for MongoDB** | Migrate MongoDB to Azure Cosmos DB. | Pending announcement |

### Available resources

| Resource | Description |
| --- | --- |
| [Documentation](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) | Access the MSSQL extension for Visual Studio Code documentation for tutorials and guides. |
| [Community support](https://stackoverflow.com/questions/tagged/visual-studio-code) | Join the Visual Studio Code community and explore forums like Stack Overflow. |
| [GitHub issues](https://github.com/microsoft/azuredatastudio/issues) | Submit feature requests or report bugs via the Azure Data Studio GitHub repository. |
| [Microsoft support](https://support.microsoft.com/topic/customer-service-phone-numbers-c0389ade-5640-e588-8b0e-28de8afeb3f2) | Contact Microsoft support for critical issues. |

## Frequently asked questions (FAQ)

Here are answers to questions about the Azure Data Studio deprecation and migration to Visual Studio Code.

### What happens to Azure Data Studio after February 28, 2026?

After **February 28, 2026**, Azure Data Studio is no longer supported. Azure Data Studio stops receiving updates, security patches, and maintenance. Migrate to an alternative solution to ensure continued support and security.

### What happens to existing database projects in Azure Data Studio?

You can open projects in Visual Studio Code without migration.

### Is there a deadline for migrating?

The Azure Data Studio retirement was announced on **February 6, 2025**. After **February 28, 2026**, Azure Data Studio is retired and no longer supported.

### Can my queries and scripts work in Visual Studio Code?

Yes. You can use the same queries and scripts in Visual Studio Code.

### What about extensions not yet available in Visual Studio Code?

Refer to the Migration Plan for alternatives.

### Are there plans for missing features like SQL Server Agent?

Features under development include DACPAC support. For other features, use the [MSSQL extension for Visual Studio Code](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) or [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).

### How do I install the MSSQL extension for Visual Studio Code?

Install it from the Visual Studio Code Marketplace. Detailed steps are available in the documentation.

### What benefits does Visual Studio Code offer over Azure Data Studio?

Visual Studio Code provides an extensible environment with community support and integrations for source control and DevOps workflows.

## Related content

- [What is the MSSQL extension for Visual Studio Code?](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)
- [Download Visual Studio Code](https://code.visualstudio.com/download)
- [Visual Studio Code extensions](https://marketplace.visualstudio.com/VSCode)
