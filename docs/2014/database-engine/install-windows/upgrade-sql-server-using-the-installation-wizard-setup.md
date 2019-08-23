---
title: "Upgrade to SQL Server 2014 Using the Installation Wizard (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading Database Engine"
  - "Database Engine [SQL Server], upgrading"
ms.assetid: cef118a5-a7ce-4bfa-8b9d-c81996284cfc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade to SQL Server 2014 Using the Installation Wizard (Setup)
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard provides a single feature tree for upgrade of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. You can also install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] side by side with an earlier version, or migrate existing databases and configuration settings from an earlier [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version, and apply them to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 For more information, see these topics:  
  
-   [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md)  
  
-   [Work with Multiple Versions and Instances of SQL Server](../../sql-server/install/work-with-multiple-versions-and-instances-of-sql-server.md)  
  
-   [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance-setup.md)  
  
-   [Install SQL Server 2014 from the Command Prompt](install-sql-server-from-the-command-prompt.md)  
  
-   [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md)  
  
> [!NOTE]  
>  Upgrade of an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is not supported on a computer that is running [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] Server Core SP1. For more information on Server Core installations, see [Install SQL Server 2014 on Server Core](install-sql-server-on-server-core.md).  
  
## Prerequisites  
 You must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share, and is a local administrator.  
  
 Before upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review the following topics:  
  
-   [Upgrade to SQL Server 2014](upgrade-sql-server.md)  
  
-   [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
-   [Check Parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md)  
  
-   [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
-   [SQL Server Database Engine Backward Compatibility](../sql-server-database-engine-backward-compatibility.md)  
  
> [!WARNING]  
>  Be aware that you cannot change the features to be upgraded, and you cannot add features during the upgrade operation. For more information about how to add features to an upgraded instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] after the upgrade operation is complete, see [Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](add-features-to-an-instance-of-sql-server-setup.md).  
  
## Procedure  
  
#### To upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media, and from the root folder, double-click Setup.exe. To install from a network share, move to the root folder on the share, and then double-click Setup.exe.  
  
2.  The Installation Wizard starts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To upgrade an existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], click **Installation** in the left-hand navigation area, and then click **Upgrade from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]**.  
  
3.  On the Product Key page, click an option to indicate whether you are upgrading to a free edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or whether you have a PID key for a production version of the product. For more information, see [Editions and Components of SQL Server 2014](../../sql-server/editions-and-components-of-sql-server-2016.md) and [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md).  
  
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
  
     **Installed instances**  - The grid will show instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are on the computer where Setup is running. If a default instance is already installed on the computer, you must install a named instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
13. Work flow for the rest of this topic depends on the features that you have specified for your installation. You might not see all the pages, depending on your selections.  
  
14. On the Server Configuration - Service Accounts page, the default service accounts are displayed for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features that you are upgrading.  
  
     Authentication and login information will be carried forward from the previous instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can assign the same login account to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually so that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they have to have to complete their tasks. For more information, see [Configure Windows Service Accounts and Permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
     To specify the same login account for all service accounts in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.  
  
     **Security Note** [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
     When you are finished specifying login information for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, click **Next**.  
  
15. On the Full-Text Search Upgrade Options page, specify the upgrade options for the databases being upgraded. For more information, see [Full-Text Search Upgrade Options](../../sql-server/install/full-text-search-upgrade-options.md).  
  
16. The Feature Rules window will automatically advance if all rules pass.  
  
17. The Ready to Upgrade page displays a tree view of installation options that were specified during Setup. To continue, click **Install**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
18. During installation, the progress page provides status so that you can monitor installation progress as Setup continues.  
  
19. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
20. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information about Setup log files, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).  
  
## Next Steps  
 After you upgrade to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], complete the following tasks:  
  
-   **Register your servers** - Upgrade removes registry settings for the previous instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you upgrade, you must reregister your servers.  
  
-   **Update statistics** - To help optimize query performance, we recommend that you update statistics on all databases following upgrade. Use the `sp_updatestats` stored procedure to update statistics in user-defined tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
-   **Configure your new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation** - To reduce the attackable surface area of a system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] selectively installs and enables key services and features. For more information about surface area configuration, see the readme file for this release.  
  
## See Also  
 [Upgrade to SQL Server 2014](upgrade-sql-server.md)   
 [Backward Compatibility](../../getting-started/backward-compatibility.md)  
  
  
