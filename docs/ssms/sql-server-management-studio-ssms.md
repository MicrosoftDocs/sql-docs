---
title: SQL Server Management Studio (SSMS)
description: Learn details about SQL Server Management Studio (SSMS) and what SSMS can do, including how to manage Analysis Services Solutions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/19/2024
ms.service: sql
ms.subservice: ssms
ms.topic: overview
f1_keywords:
  - "sql13.ssms.viewhelp.f1"
helpviewer_keywords:
  - "SQL Server Management Studio"
  - "SQL Server Management Studio for Integration Services"
  - "SQL Server Management Studio for Reporting Services"
  - "SQL Server Management Studio for Analysis Services"
---

# What is SQL Server Management Studio (SSMS)?

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

SQL Server Management Studio (SSMS) is an integrated environment for managing any SQL infrastructure. Use SSMS to access, configure, manage, administer, and develop all components of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview), [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview), [SQL Server on Azure VM](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is/). SSMS provides a single comprehensive utility that combines a broad group of graphical tools with many rich script editors to provide access to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for developers and database administrators of all skill levels.

- [**Download SQL Server Management Studio (SSMS)**](download-sql-server-management-studio-ssms.md)
- [**Download SQL Server Developer**](https://my.visualstudio.com/Downloads?q=SQL%20Server%20Developer)
- [**Download Visual Studio**](https://www.visualstudio.com/downloads/)

:::image type="content" source="media/sql-server-management-studio-ssms/ssms.png" alt-text="Screenshot of SQL Server Management Studio.":::

## SQL Server Management Studio components

| Description | Component |
| --- | --- |
| Use **Object Explorer** to view and manage all objects in one or more instances of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. | [Object Explorer](../ssms/object/object-explorer.md) |
| Use **Template Explorer** to build and manage files of boilerplate text that you use to speed the development of queries and scripts. | [Template Explorer](../ssms/template/template-explorer.md) |
| Use the deprecated **Solution Explorer** to build projects to manage administration items such as scripts and queries. | [Solution Explorer](../ssms/solution/solution-explorer.md) |
| Use the visual design tools included in SSMS to build queries, tables, and diagram databases. | [Visual Database Tools](../ssms/visual-db-tools/visual-database-tools.md) |
| Use the SSMS language editors to interactively build and debug queries and scripts. | [Query and Text Editors](./f1-help/database-engine-query-editor-sql-server-management-studio.md) |

## SQL Server Management Studio for business intelligence

Use SSMS to access, configure, manage, and administer Analysis Services, Integration Services, and Reporting Services. Although all three business intelligence technologies rely on SSMS, the administrative tasks associated with each are slightly different.

> [!NOTE]  
> To create and modify Analysis Services, Integration Services solutions, and Reporting Services, use SQL Server Data Tools (SSDT), not SSMS. [SQL Server Data Tools (SSDT)](../ssdt/sql-server-data-tools.md) is a development environment that is based on [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/).

### Manage Analysis Services solutions

SQL Server Management Studio (SSMS) enables you to manage Analysis Services objects, such as performing back-ups and processing objects.

SSMS provides an Analysis Services Script project in which you can develop and save scripts written in **Multidimensional Expressions (MDX)**, **Data Analysis Expressions (DAX)**, **Data Mining Extensions (DMX)**, and **XML for Analysis (XMLA)**.

These scripts are used to perform management tasks or recreate objects such as databases and instances on Analysis Services cubes. For example, you can develop an XMLA script in an Analysis Services Script project to create new objects directly on an existing instance. These projects can be saved as part of a solution and integrated with source code control.

> [!NOTE]
> It is noted that while DAX was originally designed for tabular data models, it can also be used to query multidimensional models in SQL Server Analysis Services.
> SSMS can do DAX and MDX, but there are some considerations to keep in mind regarding the model you are working with and the type of queries you intend to run.

For more information about the Analysis Services Scripts Project in SSMS, see [Analysis Services Scripts Project](/analysis-services/instances/analysis-services-scripts-project-in-sql-server-management-studio).

### Manage Integration Services solutions

SQL Server Management Studio (SSMS) can be used to manage and monitor running SSIS packages. You can organize packages into folders, run, import, export, and upgrade Integration Services packages. However, since SSIS 2012, the storage of packages has changed. They're no longer stored in the server's `msdb` database of the default instance but are now managed through the SSIS Catalog database (`SSISDB`). This means that you can no longer manage packages in the same way as you did in previous versions of SSIS. You can still use SSMS to manage the SSIS Catalog database, but you must use the Integration Services Catalogs node in Object Explorer.

The latest version of SSMS provides an integrated environment for managing any SQL infrastructure. It also allows users to run SSIS packages stored in the SSIS Catalog from Object Explorer in SSMS.

The [Import and Export Wizard](../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md) within SSMS can be used to create SSIS packages, which is a good starting point for learning about SSIS. However, you must use [SQL Server Data Tools (SSDT)](../ssdt/sql-server-data-tools.md) to create and manage your packages for more complex packages.

### Manage Reporting Services projects

SQL Server Management Studio (SSMS) enables Reporting Services features, administers the server and databases, and manages roles and jobs. You can manage shared schedules using the Shared Schedules folder and manage report server databases (`ReportServer`, `ReportServerTempDB`). When moving a report server database to a new SQL Server instance, you must create an RSExecRole in the `master` system database.

For more information on these tasks, you can refer to the articles on Reporting Services in SSMS, administering a Report Server database, and creating the **RSExecRole**:

- [Reporting Services in SSMS](../reporting-services/tools/reporting-services-in-sql-server-management-studio-ssrs.md)
- [Administer a Report Server database](../reporting-services/report-server/administer-a-report-server-database-ssrs-native-mode.md)
- [Create the RSExecRole](../reporting-services/security/create-the-rsexecrole.md)

You also manage the server by enabling and configuring various features, setting server defaults, and managing roles and jobs. 

For more information about these tasks, see the following articles:

- [Set Report Server properties](../reporting-services/tools/set-report-server-properties-management-studio.md)
- [Create, delete, or modify a role](../reporting-services/security/role-definitions-create-delete-or-modify.md)
- [Enabling and disabling client-side printing for Reporting Services](../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md)

SSMS provides an integrated environment for managing any SQL infrastructure, including SSRS. In the web portal, you can enable features, set server defaults, manage running jobs, view custom reports, and create and manage shared schedules. 

> [!NOTE]
> While SSMS offers various management capabilities for SSRS, it is not a replacement for Report Manager online or the Report Services Configuration Manager. It is recommended to stay updated with the latest versions and documentation to ensure effective management of SSRS within SSMS. If you have any specific questions or need further assistance, feel free to ask.

## Non-English language versions

The block on mixed languages setup has been lifted. You can install SSMS in German on a French Windows machine. If the operating system language doesn't match the SSMS language, the user needs to change the language under Tools > Options > International Settings. Otherwise, SSMS shows the English UI.

For more information about different locales with previous versions, see [Install non-English language versions of SSMS](install-other-languages.md).

## Support policy

Starting with SSMS 17.0, the SQL Tools team has adopted the [Microsoft Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy).

Read the original [Modern Lifecycle Policy announcement](https://support.microsoft.com/help/447912/announcing-microsoft-modern-lifecycle-policy). For more information, see [Modern Policy FAQs](https://support.microsoft.com/help/30882/modern-lifecycle-policy-faq).

For diagnostic data collection and feature usage information, see the [SQL Server privacy supplement](../sql-server/sql-server-privacy.md) and [diagnostic data collection](sql-server-management-studio-telemetry-ssms.md).

## Cross-platform tool

[!INCLUDE [ssms-azure-data-studio-mention](../includes/ssms-azure-data-studio-mention.md)]

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]

## Related content

- [Install non-English language versions of SSMS](install-other-languages.md)
- [Connect to and query a SQL Server instance](./quickstarts/ssms-connect-query-sql-server.md)
- [Writing Transact-SQL Statements](../t-sql/tutorial-writing-transact-sql-statements.md)
- [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio)
