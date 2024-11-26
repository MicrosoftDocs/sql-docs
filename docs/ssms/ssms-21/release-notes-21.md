---
title: Release notes for SQL Server Management Studio 21 Preview
description: Release notes for SQL Server Management Studio 21 Preview (SSMS).
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 11/12/2024
ms.service: sql
ms.subservice: ssms
ms.topic: whats-new
---

# Release notes for SQL Server Management Studio 21 Preview

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article details updates, improvements, and bug fixes for the current and previous versions of [!INCLUDE [ssms-21-md](../includes/ssms-21-md.md)].

[!INCLUDE [ssms-connect-aazure-ad](../../includes/ssms-connect-azure-ad.md)]

## Current SQL Server Management Studio 21 preview release

**[Download SQL Server Management Studio 21 Preview](https://aka.ms/ssms/21/preview/vs_SSMS.exe)**

[!INCLUDE [ssms-21-md](../includes/ssms-21-md.md)] is the latest preview release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](../release-notes-ssms.md#previous-ssms-releases).

### 21.0 Preview 1

- Release number: 21.0 Preview 1
- Build number: 21.0.47
- Release date: November 12, 2024

#### What's new in 21.0 Preview 1

| Feature | Details |
| --- | --- |
| 64-bit | SSMS 21 is a Visual Studio 2022-based application that supports 64-bit, ensuring smoother operations and reducing out-of-memory errors. |
| Always Encrypted | An assessment for Always Encrypted has been added to the wizard. It can now be found under the **Always Encrypted Wizard** menu (formerly named **Encrypt Columns...**) after right-clicking a database and selecting **Tasks**. For more information, see [Always Encrypted Assessment in SQL Server Management Studio 21](https://techcommunity.microsoft.com/blog/azuresqlblog/always-encrypted-assessment-in-sql-server-management-studio-21/4286148). |
| Azure Authentication | The Azure Authentication contains a subset of dialogs where authentication is required, including the dialogs for adding an Azure firewall rule, configuring the Service Level Objective (SLO) when creating an Azure database, the Azure storage browser, creating a new column master key, creating a Linked Server, and creating a new Audit. |
| Azure SQL Database | Introduced UI support for creating logins and database users. |
| Dark theme | SSMS 21 supports dark theme for selected dialogs, including Object Explorer, Query Editor, Results pane, and Template Explorer. |
| Database Properties | Added a page to list Database Scoped Configuration values on the **Database Properties** dialog. |
| Drivers | Updated Microsoft.Data.SqlClient (MDS) to version 5.1.6. |
| Git | Support for Git integration returns in SSMS 21 to support the growing emphasis on CI/CD, which can be found in the **Git** menu. Select **Settings** to configure options specific to Git. The Git integration allows developers and database administrators to track, manage, and collaborate on SQL scripts seamlessly within the SSMS environment, including support for creating and cloning repositories and creating and managing branches. |
| Installation | SSMS 21 is installed using the Visual Studio Installer. For more information, see [Install SQL Server Management Studio 21 Preview](../install/install.md). |
| Language support | Introduced support for SSMS in Czech, Polish, and Turkish. |
| Libraries | Updated Azure.Core to version 1.41.0. |
| Libraries | Updated DacFx to version 162.4.92. |
| Libraries | Updated Server Management Objects (SMO) to version 17.100.52. |
| Libraries | Updated System.Text.Json to version 8.0.4. |
| Query Editor | The scroll bar for the query editor in SSMS 21 defaults to **map mode**. To change the option, right-click the scroll bar and select **Scroll Bar Options...**. Within the **Options** dialog, you can change the display to **Use bar mode for vertical scroll bar**. |
| Query Store | The Query Store reports feature a **Replica** dropdown list, allowing users to view Query Store data across various replica sets or roles. For additional details, refer to [sys.query_store_replicas](../../relational-databases/system-catalog-views/sys-query-store-replicas.md) to understand the current association between a replica and its role. When the [Query Store for readable secondaries](../../relational-databases/performance/query-store-for-secondary-replicas.md) feature is enabled, data populates exclusively for roles that have been deployed and designated as replicas. |
| Sign in | Users can now access their Azure and GitHub accounts from SSMS 21. Sign-in isn't required to install or use SSMS 21. For more information, see [Access multiple accounts in SQL Server Management Studio 21 Preview](sign-in-access-multiple-accounts.md). |
| Terminal | Introduced integrated terminal access from the **View** menu to support writing and executing command line and PowerShell commands. |
| User Interface | The updated SSMS interface has a refreshed, modernized design optimized for a streamlined experience that integrates with high-DPI displays. The new design includes updated icons, a modern dark theme, and customizable layouts to suit your preferences. |
| Visual Studio | Updated to Visual Studio 17.13 Preview 1. |

#### Bug fixes in 21.0 Preview 1

| Feature | Details |
| --- | --- |
| Azure SQL Database | Addressed issue of SSMS hanging when trying to connect to an Azure SQL Database that has been deleted. |
| Azure SQL Database | Resolved the problem of SSMS becoming inaccessible when a user doesn't have permissions to access all the databases on a logical server. |
| Azure SQL Database | Fixed the behavior of SSMS hanging and generating the error "An error occurred while changing the current database" when an invalid database name is entered in the database name dropdown list for the editor window. |
| Azure SQL Managed Instance | Removed the ability to select an alternate option for **Login auditing** within the **Server Properties** dialog. |
| Azure SQL Managed Instance | Removed the ability to configure the number of error log files in the **Configure SQL Server Error Logs** dialog. |
| Central Management Server | Added ability to save the **Trust Server Certificate** connection option. |

#### Known issues 21.0 Preview 1

See [Known issues in SQL Server Management Studio 21 Preview](known-issues.md) for known issues using [!INCLUDE [ssms-21-md](../includes/ssms-21-md.md)].

[!INCLUDE [support](../includes/support.md)]

## Related content

- [Install SQL Server Management Studio 21 Preview](../install/install.md)
- [SQL Server Management Studio 21 Preview FAQ](faq.yml)
