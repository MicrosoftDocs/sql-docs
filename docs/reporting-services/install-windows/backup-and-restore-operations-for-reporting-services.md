---
title: "Backup and restore operations for Reporting Services"
description: "Backup and Restore Operations for Reporting Services"
author: maggiesMSFT
ms.author: maggies
ms.date: 12/08/2021
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Backup and restore operations for Reporting Services

  This article provides an overview of all data files used in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation and describes when and how you should back up the files. Developing a backup and restore plan for the report server database files is the most important part of a recovery strategy. However, a more complete recovery strategy would include backups of the encryption keys, custom assemblies or extensions, configuration files, and source files for reports.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Mode  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
 Backup and restore operations are often used to move all or part of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation:  
  
-   If you're moving just the report server databases, you can use backup and restore or attach and detach to relocate the databases on a different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Move the report server databases to another computer &#40;SSRS Native mode&#41;](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
-   Moving a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to a new computer is called a migration. When you migrate an installation, you run Setup to install a new report server instance and then copy instance data to the new computer. For more information about migrating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, see the following articles:  
  
    - [Upgrade and migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)  
    - [Migrate a Reporting Services installation &#40;Native mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md)  

    ::: moniker range="=sql-server-2016"
  
    - [Migrate a Reporting Services installation &#40;SharePoint mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md)  

    ::: moniker-end
  
## Back up the report server databases  
 Because a report server is a stateless server, all application data is stored in the **reportserver** and **reportservertempdb** databases that run on an [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance. You can back up the **reportserver** and **reportservertempdb** databases using one of the supported methods for backing up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. Here are some recommendations specific to the report server databases:  
  
-   Use the full recovery model to back up the **reportserver** database.  
  
-   Use the simple recovery model to back up the **reportservertempdb** database.  
  
-   You can use different backup schedules for each database. The only reason to back up the **reportservertempdb** is to avoid having to recreate it if there's a hardware failure. If you experience hardware failure, it isn't necessary to recover the data in **reportservertempdb**, but you do need the table structure. If you lose **reportservertempdb**, the only way to get it back is to recreate the report server database. If you recreate the **reportservertempdb**, it's important that it has the same name as the primary report server database.  
  
 For more information about backup and recovery of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases, see [Back up and restore of SQL Server databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  

::: moniker range="=sql-server-2016"

> [!IMPORTANT]  
>  If your report server is in SharePoint mode, there are additional databases to be concerned with, including SharePoint configuration databases and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] alerting database. In SharePoint mode, three databases are created for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. The **reportserver**, **reportservertempdb**, and **dataalerting** databases. For more information, see [Backup and restore Reporting Services SharePoint service applications](../../reporting-services/report-server-sharepoint/backup-and-restore-reporting-services-sharepoint-service-applications.md)  

::: moniker-end
  
## Back up the encryption keys  
 You should back up the encryption keys when you configure a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation for the first time. You should also back up the keys anytime you change the identity of the service accounts or rename the computer. For more information, see [Back up and restore Reporting Services encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md). 

::: moniker range="=sql-server-2016"

For SharePoint mode report servers, see the "Key Management" section of [Manage a Reporting Services SharePoint service application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md).  

::: moniker-end
  
## Back up the configuration files  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses configuration files to store application settings. You should back up the files when you first configure the server and after you deploy any custom extensions. Files to back up include:  
  
-   Rsreportserver.config  
  
-   Rssvrpolicy.config  
  
-   ReportingServicesService.exe.config  
  
-   Web.config for the Report Server  [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] application
  
-   Machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]  
  
## Back up data files  
 Back up the files that you create and maintain in Report Designer. These include report definition (.rdl) files, shared data source (.rds) files, data view (.dv) files, data source (.ds) files, report server project (.rptproj) files, and report solution (.sln) files.  
  
 Remember to back up any script files (.rss) that you created for administration or deployment tasks.  
  
 Verify that you have a backup copy of any custom extensions and custom assemblies you're using.  

## Related content

- [Report server database](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)   
- [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
- [rskeymgmt utility](../../reporting-services/tools/rskeymgmt-utility-ssrs.md)   
- [Copy databases with backup and restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)   
- [Administer a report server database](../../reporting-services/report-server/administer-a-report-server-database-ssrs-native-mode.md)   
- [Configure and manage encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
