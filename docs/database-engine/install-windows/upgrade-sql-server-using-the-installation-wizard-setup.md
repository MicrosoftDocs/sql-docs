---
title: "Upgrade: Installation Wizard (Setup)"
description: The SQL Server Installation Wizard provides a single feature tree for an in-place upgrade of SQL Server components to the latest version of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/13/2019"
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom:
  - seo-lt-2019
  - intro-installation
helpviewer_keywords:
  - "upgrading Database Engine"
  - "Database Engine [SQL Server], upgrading"
monikerRange: ">=sql-server-2016"
---
# Upgrade SQL Server Using the Installation Wizard (Setup)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard provides a single feature tree for an in-place upgrade of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components to the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
>[!WARNING]  
>When you upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be overwritten and will no longer exist on your computer. 
>
>Before upgrading, back up SQL Server databases and other objects associated with the previous SQL Server instance.  
  
> [!CAUTION]  
> For many production and some development environments, a new installation upgrade or a rolling upgrade is more appropriate than an in-place upgrade.  For more information regarding upgrade methods, see:
> * [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md)
> * [Upgrade Data Quality Services](../../database-engine/install-windows/upgrade-data-quality-services.md)
> * [Upgrade Integration Services](../../integration-services/install-windows/upgrade-integration-services.md)
> * [Upgrade Master Data Services](../../database-engine/install-windows/upgrade-master-data-services.md)
> * [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)
> * [Upgrade Analysis Services](../../database-engine/install-windows/upgrade-analysis-services.md)
> * [Upgrade Power Pivot for SharePoint](../../database-engine/install-windows/upgrade-power-pivot-for-sharepoint.md).  
  
## Prerequisites  
You must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share, and is a local administrator.  
  
> [!WARNING]  
>  Be aware that you cannot change the features to be upgraded, and you cannot add features during the upgrade operation. For more information about how to add features to an upgraded instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server &#40;Setup&#41;](./add-features-to-an-instance-of-sql-server-setup.md).  
  
 If you are  upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review [Plan and Test the Database Engine Upgrade Plan](../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md) and then perform the following tasks, as appropriate for your environment:  
  
-   Back up all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database files from the instance to be upgraded, so that you can restore them, if it is required.  
  
-   Run the appropriate Database Console Commands (DBCC) on databases to be upgraded to ensure that they are in a consistent state.  
  
-   Estimate the disk space that is required to upgrade SQL Server components, in addition to user databases. For disk space that is required by SQL Server components, see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Ensure that existing SQL Server system databases - master, model, msdb, and tempdb - are configured to autogrow, and ensure that they have sufficient hard disk space.  
  
-   Ensure that all database servers have logon information in the master database. This is important for restoring a database, as system logon information resides in master.  
  
-   Disable all startup stored procedures, as the upgrade process will stop and start services on the SQL Server instance being upgraded. Stored procedures processed at startup time might block the upgrade process.  
  
-   When upgrading instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is enlisted in MSX/TSX relationships, upgrade target servers before you upgrade master servers. If you upgrade master servers before target servers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will not be able to connect to master instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Quit all applications, including all services that have SQL Server dependencies. Upgrade might fail if local applications are connected to the instance being upgraded.  
  
-   Make sure that Replication is current and then stop Replication.   
    For detailed steps for performing a rolling upgrade in a replicated environment, see [Upgrade Replicated Databases](../../database-engine/install-windows/upgrade-replicated-databases.md).
  
## Procedure  
  
### To upgrade [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)]  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media, and from the root folder, double-click Setup.exe. To install from a network share, move to the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard starts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To upgrade an existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], click **Installation** in the left-hand navigation area, and then click **Upgrade from...** previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  On the Product Key page, click an option to indicate whether you are upgrading to a free edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or whether you have a PID key for a production version of the product. For more information, see [Editions and Components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md) and [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
4.  On the License Terms page, review the license agreement and, if you agree, select the **I accept the license terms** check box, and then click **Next**. To help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
5.  In the Global Rules window, the setup procedure will automatically advance to the Product Updates window if there are no rule errors.  
  
6.  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update page will appear next if the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update check box in Control Panel\All Control Panel Items\Windows Update\Change settings is not checked. Putting a check in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update page will change the computer settings to include the latest updates when you scan for Windows Update.  
  
7.  On the Product Updates page, the latest available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If you don't want to include the updates, clear the **Include [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates** check box. If no product updates are discovered, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup does not display this page and auto advances to the **Install Setup Files** page.  
  
8.  On the Install Setup Files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is found, and is specified to be included, that update will also be installed.  
  
9. In the Upgrade Rules window, the setup procedure will automatically advance to the Select instance window if there are no rule errors.  
  
10. On the Select Instance page, specify the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to upgrade. To upgrade Management tools and shared features, select **Upgrade shared features only**.  
  
11. On the Select Features page, the features to upgrade will be preselected. A description for each component group appears in the right pane after you select the feature name.  
  
     The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will install the prerequisite that are not already installed during the installation step described later in this procedure.  
  
    > [!NOTE]  
    >  If you have opted to upgrade the shared features by selecting **\<Upgrade shared features only>** on the **Select Instance** page, all the shared features are preselected on the Feature Selection page. All the shared components are upgraded at the same time.  
  
12. On the Instance Configuration page, specify the Instance ID for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     **Instance ID** - By default, the instance name is used as the Instance ID. This is used to identify installation directories and registry keys for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is the case for default instances and named instances. For a default instance, the instance name and instance ID would be MSSQLSERVER. To use a non-default instance ID, provide a value for the **Instance ID** textbox.  
  
     All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service packs and upgrades will apply to every component of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     **Installed instances**  - The grid will show instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
13. Work flow for the rest of this article depends on the features that you have specified for your installation. You might not see all the pages, depending on your selections.  
  
14. On the Server Configuration - Service Accounts page, the default service accounts are displayed for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features that you are upgrading.  
  
     Authentication and login information will be carried forward from the previous instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can assign the same login account to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually so that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they have to have to complete their tasks. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
     To specify the same login account for all service accounts in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.  
  
     **Security Note** [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
     When you are finished specifying login information for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, click **Next**.  
  
15. On the Full-Text Search Upgrade Options page, specify the upgrade options for the databases being upgraded. For more information, see [Full-Text Search Upgrade Options](./install-sql-server.md).  
  
16. The Feature Rules window will automatically advance if all rules pass.  
  
17. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, click **Install**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
18. During installation, the progress page provides status so that you can monitor installation progress as Setup continues.  
  
19. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
20. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
## Next Steps  
 After you upgrade to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], complete the following tasks:  
  
-   **Register your servers** - Upgrade removes registry settings for the previous instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade, you must reregister your servers.  
  
-   **Update statistics** - To help optimize query performance, we recommend that you update statistics on all databases following upgrade. Use the **sp_updatestats** stored procedure to update statistics in user-defined tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
-   **Configure your new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation** - To reduce the attackable surface area of a system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] selectively installs and enables key services and features. For more information about surface area configuration, see the readme file for this release.  
  
## See Also  
 [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)   
 [Backward Compatibility_deleted](/previous-versions/sql/sql-server-2016/cc280407(v=sql.130))  
  
