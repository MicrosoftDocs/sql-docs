---
title: "Add Features to an Instance of SQL Server 2014 (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "feature adding [SQL Server]"
  - "SQL Server, features"
  - "adding features to SQL Server"
ms.assetid: 97931fdc-d943-48dd-81b9-ae8b8d2c6dad
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Add Features to an Instance of SQL Server 2014 (Setup)
  This topic provides a step-by-step procedure for adding features to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components or services are specific to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These are also known as instance-aware. They share the same version as the instance that hosts them, and are used exclusively for that instance. You can add the instance-aware components to an instance [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], along with the shared components of if they are not already installed. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 To add features to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the command prompt, see [Install SQL Server 2014 from the Command Prompt](install-sql-server-from-the-command-prompt.md).  
  
## Prerequisites  
 Before you continue, review topics in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md).  
  
> [!NOTE]  
>  For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read permissions on the remote share.  
  
> [!NOTE]  
>  When you add features to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the existing usage report settings are applied to the newly-added features. To change these settings, use the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error and Usage Reporting** tool on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**Configuration Tools** menu.  
  
## Procedures  
  
#### To add features to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
  
1.  Insert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click setup.exe. To install from a network share, navigate to the root folder on the share, and then double-click setup.exe. If the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Setup dialog box appears, [!INCLUDE[clickOK](../../includes/clickok-md.md)] to install the prerequisites, then click **Cancel** to quit [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation.  
  
2.  The Installation Wizard will launch the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center. To add a new feature to an existing instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], click **Installation** in the left-hand navigation area, and then click **New [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stand-alone installation or add features to an existing installation**.  
  
3.  The System Configuration Checker will run a discovery operation on your computer. To view the verification details click **View Details**. To continue, [!INCLUDE[clickOK](../../includes/clickok-md.md)].  
  
4.  On the Product Updates page, the latest available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates are displayed. If you don't want to include the updates, clear the **Include [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product updates** check box. If no product updates are discovered, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup does not display this page and auto advances to the **Install Setup Files** page.  
  
5.  On the Install Setup files page, Setup provides the progress of downloading, extracting, and installing the Setup files. If an update for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is found, and is specified to be included, that update will also be installed. Click **Install** to install Setup Support files.  
  
6.  The System Configuration Checker will verify the system state of your computer before Setup continues.  
  
7.  On the Installation Type page, select the option **Add features to an existing instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]**, and select the instance you would like to update.  
  
8.  On the Feature Selection page, select the components for your installation. A description for each component group appears in the right-hand pane after you select the feature name. You can select any combination of check boxes. For more information, see [Editions and Components of SQL Server 2014](../../sql-server/editions-and-components-of-sql-server-2016.md). Each component can be installed only once on a given instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To install multiple components, you must install an additional instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The prerequisites for the selected features are displayed on the right-hand pane. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will install the prerequisite that are not already installed during the installation step described later in this procedure.  
  
     The System Configuration Checker will verify the system state of your computer before Setup continues. Click **Next** to continue.  
  
9. The Disk Space Requirements page calculates the required disk space for the features you specify, and compares requirements to the available disk space on the computer where Setup is running.  
  
10. Work flow for the remainder of this topic depends on the features you have specified for your installation. You might not see all of the pages, depending on your selections.  
  
11. On the Server Configuration - Service Accounts page, specify login accounts for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. The actual services that are configured on this page depend on the features you selected to install.  
  
     You can assign the same login account to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, or you can configure each service account individually. You can also specify whether services start automatically, are started manually, or are disabled. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you configure service accounts individually to provide least privileges for each service, where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services are granted the minimum permissions they need to complete their tasks. For more information, see [Server Configuration - Service Accounts](../../sql-server/install/server-configuration-service-accounts.md) and [Configure Windows Service Accounts and Permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
     To specify the same login account for all service accounts in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], provide credentials in the fields at the bottom of the page.  
  
     **Security Note** : [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
     When you are finished specifying login information for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, click **Next**.  
  
12. Use the **Server Configuration - Collation** tab to specify non-default collations for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information, see [Server Configuration - Collation](../../sql-server/install/server-configuration-collation.md).  
  
13. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - Account Provisioning page to specify the following:  
  
    -   Security Mode - Select Windows Authentication or Mixed Mode Authentication for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Mixed Mode Authentication, you must provide a strong password for the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account.  
  
         After a device establishes a successful connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the security mechanism is the same for both Windows Authentication and Mixed Mode. For more information, see [Database Engine Configuration - Account Provisioning](../../sql-server/install/database-engine-configuration-account-provisioning.md).  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Administrators - You must specify at least one system administrator for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To add the account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is running, click **Add Current User**. To add or remove accounts from the list of system administrators, click **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Database Engine Configuration - Account Provisioning](../../sql-server/install/database-engine-configuration-account-provisioning.md).  
  
     When you are finished editing the list, click **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, click **Next**.  
  
14. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - Data Directories page to specify non-default installation directories. To install to default directories, click **Next**.  
  
    > [!IMPORTANT]  
    >  If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     For more information, see [Database Engine Configuration - Data Directories](../../sql-server/install/database-engine-configuration-data-directories.md).  
  
15. Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Configuration - FILESTREAM page to enable FILESTREAM for your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about FILESTREAM, see [Database Engine Configuration - Filestream](../../sql-server/install/database-engine-configuration-filestream.md). To continue, click Next.  
  
16. Use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Configuration - Account Provisioning page to specify the server mode and the users or accounts that will have administrator permissions for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Server mode determines which memory and storage subsystems are used on the server. Different solution types run in different server modes. If you plan to run multidimensional cube databases on the server, choose the default option, Multidimensional and Data Mining server mode. Regarding administrator permissions, you must specify at least one system administrator for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. To add the account under which SQL Server Setup is running, click **Add Current User**. To add or remove accounts from the list of system administrators, click **Add** or **Remove**, and then edit the list of users, groups, or computers that will have administrator privileges for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information about server mode and administrator permissions, see [Analysis Services Configuration - Account Provisioning](../../sql-server/install/analysis-services-configuration-account-provisioning.md).  
  
     When you are finished editing the list, click **OK**. Verify the list of administrators in the configuration dialog box. When the list is complete, click **Next**.  
  
17. Use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Configuration - Data Directories page to specify non-default installation directories. To install to default directories, click **Next**.  
  
    > [!IMPORTANT]  
    >  If you specify non-default installation directories, ensure that the installation folders are unique to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. None of the directories in this dialog box should be shared with directories from other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     For more information, see [Analysis Services Configuration - Data Directories](../../sql-server/install/analysis-services-configuration-data-directories.md).  
  
18. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration page to specify the type of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to create. For more information about [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration modes, see [Reporting Services Configuration Options &#40;SSRS&#41;](../../sql-server/install/reporting-services-configuration-options-ssrs.md).  
  
19. Use the Distributed Replay Controller Configuration page to specify the users you want to grant administrative permissions to for the Distributed Replay controller service. Users that have administrative permissions will have unlimited access to the Distributed Replay controller service.  
  
     Click the **Add Current User** button to add the users to whom you want to grant access permissions for the Distributed Replay controller service. Click the **Add** button to add access permissions for the Distributed Replay controller service. Click the **Remove** button to remove access permissions from the Distributed Replay controller service.  
  
     To continue, click **Next**.  
  
20. Use the Distributed Replay Client Configuration page to specify the users you want to grant administrative permissions to for the Distributed Replay client service. Users that have administrative permissions will have unlimited access to the Distributed Replay client service.  
  
     **Controller Name** is an optional parameter, and the default value is \<*blank*>. Enter the name of the controller that the client computer will communicate with for the Distributed Replay client service. Note the following:  
  
    -   If you have already set up a controller, enter the name of the controller while configuring each client.  
  
    -   If you have not yet set up a controller, you can leave the controller name blank. However, you must manually enter the controller name in the **client configuration** file.  
  
     Specify the **Working Directory** for the Distributed Replay client service. The default working directory is \<*drive letter*>:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\WorkingDir\\.  
  
     Specify the **Result Directory** for the Distributed Replay client service. The default result directory is \<*drive letter*>:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\ResultDir\\.  
  
     To continue, click **Next**.  
  
21. On the Error Reporting page, specify the information you would like to send to [!INCLUDE[msCoName](../../includes/msconame-md.md)] that will help to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, options for error reporting is enabled.  
  
22. The System Configuration Checker will run one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features you have specified.  
  
23. The Ready to Install page displays a tree view of installation options that were specified during Setup. On this page, Setup indicates whether the Product Update feature is enabled or disabled and the final update version. After you click install, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will first install the required prerequisites for the selected features followed by the feature installation.  
  
24. During installation, the Installation Progress page provides status so you can monitor installation progress as Setup proceeds.  
  
25. After installation, the Complete page provides a link to the summary log file for the installation and other important notes. To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation process, click **Close**.  
  
26. If you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you are done with Setup. For information about Setup log files, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).  
  
## Next Steps  
 Configure your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation  
  
-   To reduce the attackable surface area of a system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] selectively installs and activates key services and features. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).  
  
## See Also  
 [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md)   
 [Validate a SQL Server Installation](validate-a-sql-server-installation.md)   
 [Drop a SQL Server 2014 Installation](repair-a-failed-sql-server-installation.md)   
 [Upgrade to SQL Server 2014 Using the Installation Wizard &#40;Setup&#41;](upgrade-sql-server-using-the-installation-wizard-setup.md)   
 [Install SQL Server 2014 from the Command Prompt](install-sql-server-from-the-command-prompt.md)  
  
  
