---
title: "Upgrade: Installation Wizard (Setup)"
description: The SQL Server Installation Wizard provides a single feature tree for an in-place upgrade of SQL Server components to the latest version of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
helpviewer_keywords:
  - "upgrading Database Engine"
  - "Database Engine [SQL Server], upgrading"
monikerRange: ">=sql-server-2017"
---
# Upgrade SQL Server Using the Installation Wizard (Setup)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard provides a single feature tree for an in-place upgrade of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components to the latest version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!WARNING]  
> When you upgrade [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is overwritten, and no longer exists on your computer. Before upgrading, back up SQL Server databases and other objects associated with the previous SQL Server instance.

For many production and some development environments, a new installation upgrade or a rolling upgrade is more appropriate than an in-place upgrade. For more information regarding upgrade methods, see:

- [Choose a Database Engine upgrade method](choose-a-database-engine-upgrade-method.md)
- [Upgrade Data Quality Services](upgrade-data-quality-services.md)
- [Upgrade Integration Services](../../integration-services/install-windows/upgrade-integration-services.md)
- [Upgrade Master Data Services](upgrade-master-data-services.md)
- [Upgrade and migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)
- [Upgrade Analysis Services](upgrade-analysis-services.md)
- [Upgrade Power Pivot for SharePoint](upgrade-power-pivot-for-sharepoint.md).

## Prerequisites

You must run Setup as an administrator. If you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share, and is a local administrator.

> [!WARNING]  
> You can't change the features to be upgraded, and you can't add features during the upgrade operation. For more information about how to add features to an upgraded instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md).

If you're upgrading the [!INCLUDE [ssDE](../../includes/ssde-md.md)], review [Plan and test the Database Engine upgrade plan](plan-and-test-the-database-engine-upgrade-plan.md) and then perform the following tasks, as appropriate for your environment:

- Back up all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database files from the instance to be upgraded, so that you can restore them, if it's required.

- Run the appropriate Database Console Commands (DBCC) on databases to be upgraded to ensure that they are in a consistent state.

- Estimate the disk space that is required to upgrade SQL Server components, in addition to user databases. For disk space that is required by SQL Server components, see [Hardware and software requirements for SQL Server 2025](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025.md).

- Ensure that existing SQL Server system databases - master, model, `msdb`, and `tempdb` - are configured to autogrow, and ensure that they have sufficient hard disk space.

- Ensure that all database servers have logon information in the `master` database. This is important for restoring a database, as system logon information resides in `master`.

- Disable all startup stored procedures, as the upgrade process stops and starts services on the SQL Server instance being upgraded. Stored procedures processed at startup time might block the upgrade process.

- When upgrading instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent is enlisted in MSX/TSX relationships, upgrade target servers before you upgrade master servers. If you upgrade master servers before target servers, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent isn't able to connect to master instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Quit all applications, including all services that have SQL Server dependencies. Upgrade might fail if local applications are connected to the instance being upgraded.

- Make sure that Replication is current and then stop Replication.
    For detailed steps for performing a rolling upgrade in a replicated environment, see [Upgrade or patch replicated databases](upgrade-replicated-databases.md).

## Procedure

<a id="to-upgrade-include-ssnoversionincludesssnoversion-mdmd"></a>

### Upgrade SQL Server

1. Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media, and from the root folder, double-click Setup.exe. To install from a network share, move to the root folder on the share, and then double-click Setup.exe.

1. The Installation Wizard starts the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To upgrade an existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Installation** in the left-hand navigation area, and then select **Upgrade from...** previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. On the Product Key page, select an option to indicate whether you're upgrading to a free edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or whether you have a PID key for a production version of the product. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

1. On the License Terms page, review the license agreement and, if you agree, select the **I accept the license terms** check box, and then select **Next**. To help improve [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE [msCoName](../../includes/msconame-md.md)].

1. In the Global Rules window, the setup procedure automatically advances to the Product Updates window if there are no rule errors.

1. The [!INCLUDE [msCoName](../../includes/msconame-md.md)] Update page appears next if the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Update check box in Control Panel\All Control Panel Items\Windows Update\Change settings isn't checked. Putting a check in the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Update page changes the computer settings to include the latest updates when you scan for Windows Update.

1. On the Product Updates page, the latest available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If you don't want to include the updates, clear the **Include [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product updates** check box. If no product updates are discovered, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup doesn't display this page and auto advances to the **Install Setup Files** page.

1. On the Install Setup Files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup is found, and is specified to be included, that update is also installed.

1. In the Upgrade Rules window, the setup procedure automatically advances to the Select instance window if there are no rule errors.

1. On the Select Instance page, specify the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to upgrade. To upgrade Management tools and shared features, select **Upgrade shared features only**.

1. On the Select Features page, the features to upgrade are preselected. A description for each component group appears in the right pane after you select the feature name.

   The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the prerequisites that aren't already installed during the installation step described later in this procedure.

   > [!NOTE]  
   > If you elect to upgrade the shared features by selecting **\<Upgrade shared features only>** on the **Select Instance** page, all the shared features are preselected on the Feature Selection page. All the shared components are upgraded at the same time.

1. On the Instance Configuration page, specify the Instance ID for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a non-default instance ID, provide a value for the **Instance ID** textbox.

   All [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades apply to every component of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   **Installed instances** - The grid shows instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

1. Work flow for the rest of this article depends on the features that you have specified for your installation. You might not see all the pages, depending on your selections.

1. On the Server Configuration - Service Accounts page, the default service accounts are displayed for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features that you're upgrading.

   Authentication and login information is carried forward from the previous instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can assign the same login account to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE [msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually so that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they have to complete their tasks. For more information, see [Configure Windows service accounts and permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).

   To specify the same login account for all service accounts in this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.

   > [!CAUTION]  
   > [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

     When you're finished specifying login information for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, select **Next**.

1. On the Full-Text Search Upgrade Options page, specify the upgrade options for the databases being upgraded. For more information, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).

1. The Feature Rules window automatically advances if all rules pass.

1. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, select **Install**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup first installs the required prerequisites for the selected features, followed by the feature installation.

1. During installation, the progress page provides status so that you can monitor installation progress as Setup continues.

1. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation process, select **Close**.

1. If you're instructed to restart the computer, do so now. You must read the message from the Installation Wizard when you finish with Setup. For more information about Setup log files, see [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md).

## Related content

- [Upgrade SQL Server](upgrade-sql-server.md)
- [Issues when upgrading to SQL Server 2022](/troubleshoot/sql/database-engine/install/windows/issues-upgrading-sql-server-2022)
- [Backward Compatibility](/previous-versions/sql/sql-server-2016/cc280407(v=sql.130))
