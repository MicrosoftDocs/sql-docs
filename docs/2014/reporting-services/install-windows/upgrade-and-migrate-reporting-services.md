---
title: "Upgrade and Migrate Reporting Services | Microsoft Docs"
ms.prod: "sql-server-2014"
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "SSRS, upgrading"
  - "Reporting Services, upgrades"
  - "SQL Server Reporting Services, upgrading"
  - "upgrading Reporting Services"
author: markingmyname
ms.author: maghan
manager: kfile
ms.reviewer: ""
ms.custom: ""
ms.date: "06/13/2017"
---

# Upgrade and Migrate Reporting Services

This topic is an overview of the upgrade and migration options for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. There are two general approaches to upgrading a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment:  
  
-   **Upgrade:** You upgrade the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components on the servers and instances where they are currently installed. This is commonly called an "in place" upgrade. In-place upgrade is not supported from one mode of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server to another. For example, you cannot upgrade a Native Mode report server to a SharePoint mode report server. You can migrate your report items from one mode to another. For more information, see the 'Native to SharePoint Migration' section later in this document and the related topic [Sample Reporting Services rs.exe Script to Migrate Content between Report Servers](../tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
-   **Migrate**: You install and configure a new SharePoint environment, copy your report items and resources to the new environment, and configure the new environment to use existing content. A lower level form of migration is to copy the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] databases, configuration files, and if you are using SharePoint mode, the SharePoint content databases.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
##  <a name="bkmk_top"></a> In this topic:  
  
-   [Known Upgrade Issues and Best Practices](#bkmk_known_issues)  
  
-   [Side By Side Installations](#bkmk_side_by_side)  
  
-   [In-place upgrade](#bkmk_inplace_upgrade)  
  
-   [Pre-Upgrade Checklist](#bkmk_upgrade_checklist)  
  
-   [Native Mode Upgrade and Migration Scenarios](#bkmk_native_scenarios)  
  
-   [Upgrade a Reporting Services Native Mode Scale-out Deployment](#bkmk_native_scaleout)  
  
-   [SharePoint Mode Upgrade and Migration Scenarios](#bkmk_sharePoint_scenarios)  
  
-   [Considerations for a Migration](#bkmk_migration_considerations)  
  
-   [Additional Resources](#bkmk_additional_resources)  
  
##  <a name="bkmk_known_issues"></a> Known Upgrade Issues and Best Practices  
 For a detailed list of the supported editions and versions you can upgrade, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
> [!TIP]
>  For the latest information regarding issues with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see the following:  
> 
>  -   [SQL Server 2014 Release Notes](https://go.microsoft.com/fwlink/?LinkID=296445).  
> -   [SQL Server 2014 Reporting Services Tips, Tricks, and Troubleshooting](https://go.microsoft.com/fwlink/?LinkID=391254).  
> -   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor. For more information, see [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md) and [How to: Install Upgrade Advisor](../../../2014/sql-server/install/how-to-install-upgrade-advisor.md).  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_side_by_side"></a> Side By Side Installations  
 [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Native mode can be installed side-by-side with a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Native mode deployment.  
  
 There is no support for side-by-side deployments of [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] SharePoint mode and any previous versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode components.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_inplace_upgrade"></a> In-place upgrade  
 Upgrade is completed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup can be used to upgrade any or all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, including [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Setup detects the existing instances and prompts you to upgrade. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provides upgrade options that you can specify as a command-line argument or in the Setup wizard.  
  
 When you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, you can select the option to upgrade from one of the following versions or you can install a new instance of [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] that runs side-by-side existing installations:  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
-   [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
  
 For more information on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the following:  
  
||  
|-|  
|[Upgrade to SQL Server 2014](../../database-engine/install-windows/upgrade-sql-server.md)|  
|[Upgrade to SQL Server 2014 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)|  
|[Install SQL Server 2014 from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)|  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_upgrade_checklist"></a> Pre-Upgrade Checklist  
 Before upgrading to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], review the following:  
  
-   Review requirements to determine whether your hardware and software can support [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)]. For more information, see [Hardware and Software Requirements for Installing SQL Server 2014](../../../2014/sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
-   Use System Configuration Checker (SCC) to scan the report server computer for any conditions that might prevent a successful installation of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md).  
  
-   Review security best practices and guidance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Security Considerations for a SQL Server Installation](../../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
-   Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor on the report server computer to determine any issues that might prevent you from successfully upgrading. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../../../2014/sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
-   Back up your symmetric key. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
-   Back up your report server databases and configuration files. For more information, see [Backup and Restore Operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md).  
  
-   Back up any customizations to existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] virtual directories in IIS.  
  
-   Remove invalid SSL certificates.  This includes certificates that are expired and you do not plan to update prior to upgrading Reporting Services.  Invalid certificates will cause upgrade to fail and an error message similar to the following will be written to the Reporting Services Log file: **Microsoft.ReportingServices.WmiProvider.WMIProviderException: A Secure Sockets Layer (SSL) certificate is not configured on the Web site.**.  
  
 Before you upgrade a production environment, always run a test upgrade in a pre-production environment that has the same configuration as your production environment.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
## Overview of Migration Scenarios  
 If you are upgrading from a supported version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can usually run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Wizard to upgrade the report server program files, database, and all application data.  
  
 However, **migrating** a report server installation manually is required if you encounter any of the following conditions:  
  
-   Upgrade Advisor detected one more upgrade blockers. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../../../2014/sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
-   You want to change the type of report server used in your deployment. For example, you cannot upgrade or convert a native mode report server to SharePoint mode. For more information, see [Native to SharePoint Migration &#40;SSRS&#41;](../../reporting-services/install-windows/native-to-sharepoint-migration-ssrs.md).  
  
-   You want to minimize the amount of time the report server is taken offline during the upgrade process. Your current installation remains online while you copy content data to a new report server instance and test the installation without changing the state of your existing report server installation.  
  
-   You want to migrate a SharePoint 2010 deployment of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to SharePoint 2013. SharePoint 2013 does not support in-place upgrade from SharePoint 2010. For more information, see [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md).  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_native_scenarios"></a> Native Mode Upgrade and Migration Scenarios  
 **Upgrade:** In-place upgrade for native mode is the same process for each of the supported versions that are listed earlier in this topic. Run the SQL Server installation wizard or a command line installation. Following installation the report server database will automatically upgrade to the new report server database schema. For more information, see the [In-place upgrade](#bkmk_inplace_upgrade) section in this topic.  
  
 The upgrade process begins when you select an existing report server instance to upgrade.  
  
1.  If the report server database is on a remote computer and you do not have permission to update that database, Setup prompts you to provide credentials to update to a remote report server database. Be sure to provide credentials that have `sysadmin` or database update permissions.  
  
2.  Setup checks for conditions or settings that prevent upgrade and reads configuration settings. Examples include custom extensions deployed on the report server. If upgrade is blocked, you must either modify your installation so that upgrade is no longer blocked, or migrate to a new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance. For more information, see the Upgrade Advisor documentation.  
  
3.  If upgrade can proceed, Setup prompts you to continue with the upgrade process.  
  
4.  Setup creates new folders for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] program files. The program folders for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation include MSRS12.\<*instance name*>.  
  
5.  Setup adds the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server program files, configuration tools, and command line utilities that are part of the report server feature.  
  
    1.  Program files from the previous version are removed.  
  
    2.  Report server configuration tools and utilities that are upgraded to the new version include the Native Mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, command line utilities such as RS.exe, and Report Builder.  
  
    3.  Other client tools such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and Books Online are not upgraded. To obtain new versions of the tools, you can add them when you run Setup. Earlier versions will co-exist alongside [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] versions. If you installed samples, the earlier version will remain. Setup does not support upgrade for the SQL Server samples.  
  
    4.  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] is a separate download. For more information, see [Microsoft SQL Server 2014 Data Tools - Business Intelligence for Microsoft Visual Studio 2012](https://www.microsoft.com/download/details.aspx?id=36843).  
  
6.  Setup reuses the service entry in Service Control Manager for the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Report Server service. This service entry includes the Report Server Windows service account.  
  
7.  Setup reserves new URLs based on existing virtual directory settings in IIS. Setup might not remove virtual directories in IIS, so be sure to remove those manually after upgrade is finished.  
  
8.  Setup upgrades the report server databases to the new schema and modifies the `RSExecRole` by adding Database Owner permissions to the role. This step only occurs when you are upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] prior to SP1.  
  
9. Setup merges settings in the configuration files. Using the configuration files from the current installation as the basis, new entries are added. Obsolete entries are not removed, but they will no longer be read by the report server after upgrade is finished. Upgrade will not delete old log files, the obsolete RSWebApplication.config file, or virtual directory settings in IIS. Upgrade will not remove SQL Server 2005 Report Designer, Management Studio, or other client tools. If you no longer require them, be sure to remove these files and tools after upgrade is finished.  
  
 **Migration:** Migrating a previous version of a native mode installation to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is the same steps for all of the supported s versions that are listed earlier in this topic. For more information, see [Migrate a Reporting Services Installation &#40;Native Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md)  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_native_scaleout"></a> Upgrade a Reporting Services Native Mode Scale-out Deployment  
 The following is a summary of how to upgrade a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode deployment that is scaled-out to more than one report server. This process requires downtime of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment:  
  
1.  Backup the report server databases and encryption keys. For more information, see [Backup and Restore Operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md) and [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md).  
  
2.  Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and remove all of the report servers from the scaled-out deployment. For more information, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
3.  Upgrade one of the report servers to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
4.  Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to add the report servers back to the scale-out deployment. For more information, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
     For each server, repeat the upgrade and Scale-out steps.  
  
##  <a name="bkmk_sharePoint_scenarios"></a> SharePoint Mode Upgrade and Migration Scenarios  
 The following sections describe the issues and basic steps needed to upgrade or migrate from specified versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.  
  
 There are two installation components to upgrade a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Mode deployment.  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared Service.  
  
    > [!TIP]  
    >  Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint cmdlet `Get-SPRSServiceApplicationServers` to determine servers in the SharePoint farm that are currently running the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared Service and therefore require an upgrade.  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products. For more information, see [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).  
  
 For detailed steps on Migrating a SharePoint mode installation, see [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md).  
  
> [!IMPORTANT]  
>  Some of the following scenarios require down time of the SharePoint environment due to the different technologies that need to be upgraded. If your situation does not allow for down time, you will need to complete a migration instead of an in-place upgrade.  
  
### [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 **Starting environment:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]., SharePoint 2010.  
  
 **Ending environment:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SharePoint 2010 or SharePoint 2013.  
  
-   **SharePoint 2010:** In-place upgrade of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is supported but the upgrade scenario does require down time of the SharePoint environment.  
  
     If you also want the ending environment to run SharePoint 2013, you need to complete a database-attach upgrade of the SharePoint 2010 to SharePoint 2013.  
  
-   **SharePoint 2013:** SharePoint 2013 does not support in-place upgrade from SharePoint 2010. However the procedure of **database-attach upgrade**  is supported. The behavior is different from upgrading to SharePoint 2010, where a customer could choose between the two basic upgrade approaches, in-place upgrade and database-attach upgrade.  
  
     If you have a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation integrated with SharePoint 2010, you cannot upgrade in-place the SharePoint server. However you can migrate content databases and service application databases from the SharePoint 2010 farm to a SharePoint 2013 farm.  
  
### [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 **Starting environment:** SQL Server 2008 R2, SharePoint 2010.  
  
 **Ending environment:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SharePoint 2010.  
  
-   In-place upgrade is supported and there is no down time for your SharePoint environment.  
  
-   Install the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint on each web front-end in the farm. You can install the add-in by using the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation wizard or by downloading the add-in.  
  
-   Run [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation to upgrade SharePoint mode for each 'report server'.The SQL Server installation wizard will install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service and create a new Service application.  
  
     If you also want the ending environment to run SharePoint 2013, you need to complete a database-attach upgrade of the SharePoint 2010 to SharePoint 2013.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
### [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2 to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 **Starting environment:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2, SharePoint 2007.  
  
 **Ending environment:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SharePoint 2010.  
  
-   This in-place upgrade scenario does require down time of the SharePoint environment because both the SharePoint and SQL Server technologies need to be upgraded. You may want to consider completing a migration rather than an in-place upgrade.  
  
-   Upgrade [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] to Service Pack 2 (SP2) first, if that has not already been completed.  
  
-   Upgrade SharePoint to 2010. When you run the SharePoint 2010 prerequisite installer it will upgrade the Reporting Services add-in for SharePoint 2010 products.  
  
-   Install the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint on all SharePoint web front-ends. The SharePoint pre-requisite installer installed the SQL Server 2008 R2 version of the add-in but you need the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version to work with a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server.  
  
-   > [!WARNING]  
    >  Following the SharePoint upgrade your reporting services environment will be in a non-working state until SQL Server is upgraded.  
  
-   Upgrade [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. When you run the SQL server installation wizard, you will a dialog regarding "**SQL Server Reporting Services SharePoint Mode Authentication**" dialog. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service will be installed and the credentials from the authentication page will be used for creating a new SharePoint application pool.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
### SQL Server 2005 SP2 to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 **Starting environment:** SQL Server 2005 SP2, SharePoint 2007.  
  
 **Ending environment:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SharePoint 2010.  
  
-   This in-place upgrade scenario does require down time of the SharePoint environment because both the SharePoint and SQL Server technologies need to be upgraded. You may want to consider completing a migration rather than an in-place upgrade.  
  
-   Upgrade SQL server 2005 to Service Pack 2 (SP2) first, if that has not already been completed.  
  
-   Upgrade SharePoint to SharePoint 2010. When you run the SharePoint 2010 prerequisite installer it will upgrade the Reporting Services add-in for SharePoint 2010 products.  
  
-   > [!WARNING]  
    >  Following the SharePoint upgrade your reporting services environment will be in a non-working state until SQL Server is upgraded.  
  
-   Install the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint on all SharePoint web front-ends. The SharePoint pre-requisite installer installed the SQL Server 2008 R2 version of the add-in but you need the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version to work with a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server.  
  
-   Upgrade [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. When you run the SQL server installation wizard, you will a dialog regarding "SQL Server Reporting Services SharePoint Mode Authentication" dialog. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service will be installed and the credentials from the authentication page will be used for creating a new SharePoint application pool.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_migration_considerations"></a> Considerations for a Migration  
 When moving application data, you should be aware of the following concerns and restrictions:  
  
-   Protection of encryption key includes a hash that incorporates machine identity.  
  
-   Report server database names are fixed and cannot be renamed on new computer.  
  
### Encryption Key Considerations  
 Always back up the encryption keys before moving a report server database to a new computer.  
  
 Moving a report server installation to another computer will invalidate the hash that protects the encryption keys used to help secure sensitive data stored in the report server database. Each report server instance that uses the database has its copy of the encryption key, which is encrypted with the identity of the service account as it is defined on the current computer. If you change computers, the service will no longer have access to its key, even if you use the same account name on the new computer.  
  
 To re-establish reversible encryption on the new report server computer, you must restore the key that you previously backed up. The complete key set that is stored in the report server database consists of a symmetric key value, plus service identity information used to restrict access to the key so that it can be used only by the report server instance that stored it. During key restoration, the report server replaces existing copies of the key with new versions. The new version includes machine and service identity values as defined on the current computer. For more information, see the following topics:  
  
-   SharePoint mode: See the "Key Management" section of [Manage a Reporting Services SharePoint Service Application](../../../2014/reporting-services/manage-a-reporting-services-sharepoint-service-application.md)  
  
-   Native Mode: See [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
### Fixed Database Name  
 You cannot rename the report server database. The identity of the database is recorded in report server stored procedures when the database is created. Renaming either the report server primary or temporary databases will cause errors to occur when the procedures run, invalidating your report server installation.  
  
 If the database name from the existing installation is not suited for the new installation, you should consider creating a new database that has the name that you prefer, and then load existing application data using the techniques in the following list:  
  
-   Write a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] script that calls Report Server Web service SOAP methods to copy data between databases. You can use the RS.exe utility to run the script. For more information about this approach, see [Scripting and PowerShell with Reporting Services](../tools/scripting-and-powershell-with-reporting-services.md).  
  
-   Write code that calls the WMI provider to copy data between databases. For more information about this approach, see [Access the Reporting Services WMI Provider](../tools/access-the-reporting-services-wmi-provider.md).  
  
-   If you have just a few items, you can republish reports, report models, and shared data sources from Report Designer, Model Designer, and Report Builder to the new report server. You must re-create role assignments, subscriptions, shared schedules, report snapshot schedules, custom properties that you set on reports or other items, model item security, and properties that you set on the report server. You will lose report history and report execution log data.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
##  <a name="bkmk_additional_resources"></a> Additional Resources  
  
> [!NOTE]  
>  For more information on SharePoint database-attach upgrade, see the following:  
  
-   [Overview of the upgrade process to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256688) (https://go.microsoft.com/fwlink/p/?LinkId=256688).  
  
-   [Clean up preparations before an upgrade to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256689) (https://go.microsoft.com/fwlink/p/?LinkId=256689).  
  
-   [Upgrade databases from SharePoint 2010 to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256690) (https://go.microsoft.com/fwlink/p/?LinkId=256690).  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In this topic:](#bkmk_top)  
  
## See Also  
 [Upgrade Reports](../../reporting-services/install-windows/upgrade-reports.md)   
 [Upgrade to SQL Server 2014 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
  
  
