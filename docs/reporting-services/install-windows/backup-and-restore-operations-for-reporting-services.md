---
title: "Backup and Restore Operations for Reporting Services | Microsoft Docs"
author: markingmyname
ms.author: maghan
manager: kfile
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.topic: conceptual
ms.assetid: 157bc376-ab72-4c99-8bde-7b12db70843a
ms.date: 05/24/2018
---

# Backup and Restore Operations for Reporting Services

  This article provides an overview of all data files used in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation and describes when and how you should back up the files. Developing a backup and restore plan for the report server database files is the most important part of a recovery strategy. However, a more complete recovery strategy would include backups of the encryption keys, custom assemblies or extensions, configuration files, and source files for reports.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Mode  
  
 Backup and restore operations are often used to move all or part of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation:  
  
-   If you are moving just the report server databases, you can use backup and restore or attach and detach to relocate the databases on a different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Moving the Report Server Databases to Another Computer &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
-   Moving a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to a new computer is called a migration. When you migrate an installation, you run Setup to install a new report server instance and then copy instance data to the new computer. For more information about migrating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, see the following articles:  
  
    -   [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)  
  
    -   [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-sharepoint-mode.md)  
  
    -   [Migrate a Reporting Services Installation &#40;Native Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md)  
  
## Backing Up the Report Server Databases  
 Because a report server is a stateless server, all application data is stored in the **reportserver** and **reportservertempdb** databases that run on a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance. You can back up the **reportserver** and **reportservertempdb** databases using one of the supported methods for backing up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. Here are some recommendations specific to the report server databases:  
  
-   Use the full recovery model to back up the **reportserver** database.  
  
-   Use the simple recovery model to back up the **reportservertempdb** database.  
  
-   You can use different backup schedules for each database. The only reason to back up the **reportservertempdb** is to avoid having to recreate it if there is a hardware failure. In the event of hardware failure, it is not necessary to recover the data in **reportservertempdb**, but you do need the table structure. If you lose **reportservertempdb**, the only way to get it back is to recreate the report server database. If you recreate the **reportservertempdb**, it is important that it have the same name as the primary report server database.  
  
 For more information about backup and recovery of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases, see [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
> [!IMPORTANT]  
>  If your report server is in SharePoint mode, there are additional databases to be concerned with, including SharePoint configuration databases and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] alerting database. In SharePoint mode, three databases are created for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. The **reportserver**, **reportservertempdb**, and **dataalerting** databases. For more information, see [Backup and Restore Reporting Services SharePoint Service Applications](../../reporting-services/report-server-sharepoint/backup-and-restore-reporting-services-sharepoint-service-applications.md)  
  
## Backing Up the Encryption Keys  
 You should back up the encryption keys when you configure a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation for the first time. You should also back up the keys any time you change the identity of the service accounts or rename the computer. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md). For SharePoint mode report servers, see the "Key Management" section of [Manage a Reporting Services SharePoint Service Application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md).  
  
## Backing Up the Configuration Files  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses configuration files to store application settings. You should back up the files when you first configure the server and after you deploy any custom extensions. Files to back up include:  
  
-   Rsreportserver.config  
  
-   Rssvrpolicy.config  
  
-   Rsmgrpolicy.config  
  
-   Reportingservicesservice.exe.config  
  
-   Web.config for the Report Server  [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] application
  
-   Machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]  
  
## Backing Up Data Files  
 Back up the files that you create and maintain in Report Designer. These include report definition (.rdl) files, shared data source (.rds) files, data view (.dv) files, data source (.ds) files, report server project (.rptproj) files, and report solution (.sln) files.  
  
 Remember to back up any script files (.rss) that you created for administration or deployment tasks.  
  
 Verify that you have a backup copy of any custom extensions and custom assemblies you are using.  

## Next steps

[Report Server Database](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)   
[Reporting Services Configuration Files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
[rskeymgmt Utility](../../reporting-services/tools/rskeymgmt-utility-ssrs.md)   
[Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)   
[Administer a Report Server Database](../../reporting-services/report-server/administer-a-report-server-database-ssrs-native-mode.md)   
[Configure and Manage Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
