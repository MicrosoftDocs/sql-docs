---
title: "Upgrade and migrate Reporting Services"
description: Learn about the upgrade and migration options for SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/19/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "SSRS, upgrading"
  - "Reporting Services, upgrades"
  - "SQL Server Reporting Services, upgrading"
  - "upgrading Reporting Services"
---

# Upgrade and migrate Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

This article is an overview of the upgrade and migration options for SQL Server Reporting Services. Here are the general approaches to upgrading a SQL Server Reporting Services deployment:

- **Upgrade *to* Reporting Services 2016 and older *from* Reporting Services 2016 and older:** You upgrade the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components on the servers and instances where they're currently installed. This process is commonly called an "in place" upgrade. In-place upgrade isn't supported from one mode of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server to another. For example, you can't upgrade a Native Mode report server to a SharePoint mode report server. You can migrate your report items from one mode to another. For more information, see the [SharePoint mode upgrade and migration scenarios](#bkmk_sharePoint_scenarios) section later in this document.

- **Upgrade *to* Reporting Services 2017 and later *from* Reporting Services 2016 and older:** This upgrade scenario isn't the same as previous versions. When upgrading *to* Reporting Services 2016 and older versions, you could follow an in-place upgrade process using SQL Server installation media. When upgrading *to* Reporting Services 2017 and later *from* Reporting Services 2016 and older, you can't follow the same steps because the new Reporting Services installation is a standalone product. It's no longer part of the SQL Server installation media.

   To upgrade from Reporting Services 2016 and older versions to Reporting Services 2017 and later, follow the [Migrate a Reporting Services Installation (Native Mode)](migrate-a-reporting-services-installation-native-mode.md) article, with Reporting Services 2017 or later as your destination instance.

- **Upgrade *from* Reporting Services 2017 to future versions** is again an in-place upgrade scenario, because the product installation GUIDs are the same. Run the SQLServerReportingServices.exe installation file to begin the in-place upgrade on the server where Reporting Services is currently installed.

- **Migrate**: You install and configure a new SharePoint environment, copy your report items and resources to the new environment, and configure the new environment to use existing content. A lower level form of migration is to copy the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] databases, configuration files, and if you're using SharePoint mode, the SharePoint content databases.

> [!NOTE]
> Reporting Services integration with SharePoint isn't available after SQL Server 2016.

## <a name="bkmk_known_issues"></a> Known Upgrade Issues and Best Practices

 For a detailed list of the supported editions and versions you can upgrade, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).

> [!TIP]
> For the latest information regarding issues with SQL Server, see [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md).

## <a name="bkmk_side_by_side"></a> Side By Side Installations

SQL Server Reporting Services Native mode can be installed side-by-side with a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Native mode deployment.

 There's no support for side-by-side deployments of SQL Server Reporting Services in SharePoint mode and any previous versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode components.

## <a name="bkmk_inplace_upgrade"></a> In-place Upgrade

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup completes the upgrade. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup can be used to upgrade any or all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, including [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Setup detects the existing instances and prompts you to upgrade. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provides upgrade options that you can specify as a command-line argument or in the Setup wizard.

When you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, you can select the option to upgrade from one of the following versions or you can install a new instance of SQL Server Reporting Services that runs side-by-side existing installations:

- [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]

- [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]

- [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)]

- [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]

For more information on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see:

- [Upgrade to SQL Server 2016](../../database-engine/install-windows/upgrade-sql-server.md)
- [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)
- [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)

## <a name="bkmk_upgrade_checklist"></a> Pre-Upgrade Checklist

 Before upgrading to SQL Server Reporting Services:

- Review requirements to determine whether your hardware and software can support [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)]. For more information, see [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

- Use System Configuration Checker (SCC) to scan the report server computer for any conditions that might prevent a successful installation of SQL Server Reporting Services. For more information, see [Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md).

- Review security best practices and guidance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).

- Back up your symmetric key. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).

- Back up your report server databases and configuration files. For more information, see [Backup and Restore Operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md).

- Back up any customizations to existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] virtual directories in IIS.

- Remove invalid TLS/SSL certificates, including certificates that are expired and you don't plan to update before upgrading Reporting Services. Invalid certificates cause upgrades to fail and an error similar to the following message is written to the Reporting Services Log file: **Microsoft.ReportingServices.WmiProvider.WMIProviderException: A Secure Sockets Layer (SSL) certificate is not configured on the Web site.**.

Before you upgrade a production environment, always run a test upgrade in a preproduction environment that has the same configuration as your production environment.

> [!IMPORTANT]
> These steps must be completed in full for a later rollback to be possible. Microsoft Support cannot recover backups, encryption keys, or configuration files that were not backed up.

## Overview of Migration Scenarios

If you're upgrading from a supported version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can usually run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Wizard to upgrade the report server program files, database, and all application data.

However, **migrating** a report server installation manually is required if you encounter any of the following conditions:

- You want to change the type of report server used in your deployment. For example, you can't upgrade or convert a native mode report server to SharePoint mode. For more information, see [Native to SharePoint Migration &#40;SSRS&#41;](../../reporting-services/install-windows/native-to-sharepoint-migration-ssrs.md).

- You want to minimize the amount of time the report server is taken offline during the upgrade process. Your current installation remains online while you copy content data to a new report server instance and test the installation without changing the state of your existing report server installation.

- You want to migrate a SharePoint 2010 deployment of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to SharePoint 2013/2016. SharePoint 2013/2016 doesn't support in-place upgrade from SharePoint 2010. For more information, see [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md).

## <a name="bkmk_native_scenarios"></a> Native Mode Upgrade and Migration Scenarios

**Upgrade:** In-place upgrade for native mode is the same process for each of the supported versions that are listed earlier in this article. Run the SQL Server installation wizard or a command line installation. Following installation, the report server database automatically upgrades to the new report server database schema. For more information, see [In-place Upgrade](#bkmk_inplace_upgrade) in this article.

 The upgrade process begins when you select an existing report server instance to upgrade.

1. If the report server database is on a remote computer and you don't have permission to update that database, Setup prompts you to provide credentials to update to a remote report server database. Be sure to provide credentials that have **sysadmin** or database update permissions.

1. Setup checks for conditions or settings that prevent upgrade and reads configuration settings. Examples include custom extensions deployed on the report server. If upgrade is blocked, you must either modify your installation so that upgrade is no longer blocked, or migrate to a new SQL Server Reporting Services instance. For more information, see the Upgrade Advisor documentation.

1. If upgrade can proceed, Setup prompts you to continue with the upgrade process.

1. Setup creates new folders for SQL Server Reporting Services program files. The program folders for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation include MSRS13.\<*instance name*>.

1. Setup adds the SQL Server Reporting Services report server program files, configuration tools, and command line utilities that are part of the report server feature.

   1. Program files from the previous version are removed.

   1. Report server configuration tools and utilities that are upgraded to the new version include the Native Mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, command line utilities such as RS.exe, and Report Builder.

   1. Other client tools such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] are a separate download and need to be upgraded separately. For more information, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

   1. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] is a separate download. For more information, see [SQL Server Data Tools in Visual Studio 2015](/previous-versions/mt186501(v=msdn.10)).

1. Setup reuses the service entry in Service Control Manager for the SQL Server Reporting Services Report Server service. This service entry includes the Report Server Windows service account.

1. Setup reserves new URLs based on existing virtual directory settings in IIS. Setup might not remove virtual directories in IIS, so be sure to remove those directories manually after upgrade is finished.

1. Setup merges settings in the configuration files. Setup uses the configuration files from the current installation as the basis to add new entries. Obsolete entries aren't removed, but they're no longer be read by the report server after the upgrade is finished. An upgrade doesn't delete old log files, the obsolete RSWebApplication.config file, or virtual directory settings in IIS. An upgrade doesn't remove older versions of Report Designer, Management Studio, or other client tools. If you no longer require them, remove these files and tools after the upgrade is finished.

 **Migration:** Migrating a previous version of a native mode installation to SQL Server Reporting Services is the same steps for all of the supported versions that are listed earlier in this article. For more information, see [Migrate a Reporting Services Installation &#40;Native Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md)

## <a name="bkmk_native_scaleout"></a> Upgrade a Reporting Services Native Mode Scale-out Deployment

The following summary explains how to upgrade a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode deployment that is scaled-out to more than one report server. This process requires downtime of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment:

1. Back up the report server databases and encryption keys. For more information, see [Backup and Restore Operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md) and [Add and Remove Encryption Keys for Scale-Out Deployment &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md).

1. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and remove all of the report servers from the scaled-out deployment. For more information, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).

1. Upgrade one of the report servers to SQL Server Reporting Services.

1. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to add the report servers back to the scale-out deployment. For more information, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).

   For each server, repeat the upgrade and Scale-out steps.

## <a name="rollback_native"></a> Roll back a Reporting Services Cumulative Update

Cumulative Updates in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] versions 2017 and later support in-place upgrade but can't be selectively uninstalled. To roll back an upgrade, you must uninstall the entire service and reinstall the prior version:

> [!IMPORTANT]
> These steps require that the pre-upgrade checklist has been followed completely. Step 2 will render existing configuration files, service configurations, and encryption keys irrecoverable. Microsoft Support cannot recover these configuration files or decrypt these encryption keys to assist in rollback.

1. Take note of any custom configurations including service credentials, email or file share settings, or report server URLs.

1. Uninstall SQL Server Reporting Services. In a scale-out deployment, repeat for all nodes in the scale-out. For more information, see [Uninstall Native Mode](../../sql-server/install/uninstall-reporting-services.md).

1. Restore backups of ReportServer database. For more information, see [Backup and Restore Operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md).

1. Reinstall the prior update of SQL Server Reporting Services.

1. Restore preupgrade configuration files.

1. Restore the encryption key backup. For more information, see [Back Up and Restore Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).

1. Recreate all of the custom configurations noted in step 1.

1. In a scale-out deployment, repeat steps 4 through 7 for all other nodes in the scale-out deployment.

## <a name="bkmk_sharePoint_scenarios"></a> SharePoint Mode Upgrade and Migration Scenarios

The following sections describe the issues and basic steps needed to upgrade or migrate from specified versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode to SQL Server Reporting Services [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.

 There are two installation components to upgrade a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Mode deployment.

- [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared Service.

   > [!TIP]
   > Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint cmdlet `Get-SPRSServiceApplicationServers` to determine servers in the SharePoint farm that are currently running the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared Service and therefore require an upgrade.

- [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products. For more information, see [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).

For detailed steps on Migrating a SharePoint mode installation, see [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md).

> [!IMPORTANT]
> Some of the following scenarios require down time of the SharePoint environment due to the different technologies that need to be upgraded. If your situation does not allow for down time, you will need to complete a migration instead of an in-place upgrade.

### [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to SQL Server Reporting Services

**Starting environment:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP1, SharePoint 2010, or SharePoint 2013.

**Ending environment:** SQL Server Reporting Services, SharePoint 2013, or SharePoint 2016.

- **SharePoint 2013/2016:** SharePoint 2013/2016 doesn't support in-place upgrade from SharePoint 2010. However the procedure of **database-attach upgrade** is supported.

   If you have a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation integrated with SharePoint 2010, you can't upgrade in-place the SharePoint server. However you can migrate content databases and service application databases from the SharePoint 2010 farm to a SharePoint 2013/2016 farm.

### [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to SQL Server Reporting Services

**Starting environment:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)], SharePoint 2010.

**Ending environment:** SQL Server Reporting Services, SharePoint 2013, or SharePoint 2016.

- **SharePoint 2013/2016:** SharePoint 2013/2016 doesn't support in-place upgrade from SharePoint 2010. However the procedure of **database-attach upgrade** is supported.

   If you have a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation integrated with SharePoint 2010, you can't upgrade in-place the SharePoint server. However you can migrate content databases and service application databases from the SharePoint 2010 farm to a SharePoint 2013/2016 farm.

### [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)] to SQL Server Reporting Services

 **Starting environment:** [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)], SharePoint 2010.

 **Ending environment:** SQL Server Reporting Services, SharePoint 2013, or SharePoint 2016.

- **SharePoint 2013/2016:** SharePoint 2013/2016 doesn't support in-place upgrade from SharePoint 2010. However the procedure of **database-attach upgrade** is supported.

   SharePoint must be migrated first before you can upgrade Reporting Services.

- Install the SQL Server Reporting Services version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint on each web front-end in the farm. You can install the add-in by using the SQL Server Reporting Services installation wizard or by downloading the add-in.

- Run SQL Server Reporting Services installation to upgrade SharePoint mode for each 'report server'. The SQL Server installation wizard installs the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service and creates a new Service application.

## <a name="bkmk_migration_considerations"></a> Considerations for a Migration

 When moving application data, you should be aware of the following concerns and restrictions:

- Protection of encryption key includes a hash that incorporates machine identity.

- Report server database names are fixed and can't be renamed on new computer.

### Encryption Key Considerations

Always back up the encryption keys before moving a report server database to a new computer.

Moving a report server installation to another computer invalidates the hash that protects the encryption keys used to help secure sensitive data stored in the report server database. Each report server instance that uses the database has its copy of the encryption key, which is encrypted with the identity of the service account as it is defined on the current computer. If you change computers, the service no longer has access to its key, even if you use the same account name on the new computer.

To re-establish reversible encryption on the new report server computer, you must restore the key that you previously backed up. The complete key set that is stored in the report server database consists of a symmetric key value, plus service identity information used to restrict access to the key so only the report server instance that stored it can use it. During key restoration, the report server replaces existing copies of the key with new versions. The new version includes machine and service identity values as defined on the current computer. For more information, see:

- SharePoint mode: See the "Key Management" section of [Manage a Reporting Services SharePoint Service Application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md)

- Native Mode: See [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)

### Fixed Database Name

You can't rename the report server database. The identity of the database is recorded in report server stored procedures when the database is created. Renaming either the report server primary or temporary databases cause errors when the procedures run, invalidating your report server installation.

If the database name from the existing installation isn't suited for the new installation, consider creating a new database that has the name that you prefer. Then load existing application data using the techniques in the following list:

- Write a [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] script that calls Report Server Web service SOAP methods to copy data between databases. You can use the RS.exe utility to run the script. For more information about this approach, see [Scripting and PowerShell with Reporting Services](../../reporting-services/tools/scripting-and-powershell-with-reporting-services.md).

- Write code that calls the WMI provider to copy data between databases. For more information about this approach, see [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).

- If you have just a few items, you can republish reports, and shared data sources from Report Designer, Model Designer, and Report Builder to the new report server. You must re-create role assignments, subscriptions, shared schedules, report snapshot schedules, custom properties that you set on reports or other items, model item security, and properties that you set on the report server. You lose report history and report execution log data.

## Related content

- [Overview of the upgrade process to SharePoint 2016](https://technet.microsoft.com/library/cc262483\(v=office.16\)).

- [Overview of the upgrade process to SharePoint 2013](/SharePoint/upgrade-and-update/overview-of-the-upgrade-process-from-sharepoint-2010-to-sharepoint-2013).

- [Clean up preparations before an upgrade to SharePoint 2013](/SharePoint/upgrade-and-update/clean-up-an-environment-before-an-upgrade-to-sharepoint-2013).

- [Upgrade databases from SharePoint 2013 to SharePoint 2016](https://technet.microsoft.com/library/cc303436\(v=office.16\)).

- [Upgrade databases from SharePoint 2010 to SharePoint 2013](/SharePoint/upgrade-and-update/upgrade-content-databases-from-sharepoint-2010-to-sharepoint-2013).

- [Upgrade Reports](../../reporting-services/install-windows/upgrade-reports.md)

- [Upgrade to SQL Server 2016 Using the Installation Wizard (Setup)](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)

More questions? Try asking the [Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
