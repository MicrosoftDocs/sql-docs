---
title: "Backup and Restore Operations for Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [Reporting Services], backing up"
  - "databases [Reporting Services], restoring"
  - "databases [Reporting Services], moving"
  - "backing up databases [Reporting Services]"
  - "moving databases"
  - "restoring databases [Reporting Services]"
  - "files [Reporting Services], restoring"
  - "files [Reporting Services], backing up"
ms.assetid: 157bc376-ab72-4c99-8bde-7b12db70843a
author: markingmyname
ms.author: maghan
manager: craigg
---
# Backup and Restore Operations for Reporting Services
  This topic provides an overview of all data files used in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation and describes when and how you should back up the files. Developing a backup and restore plan for the report server database files is the most important part of a recovery strategy. However, a more comprehensive recovery strategy would include backups of the encryption keys, custom assemblies or extensions, configuration files, and source files for reports and models.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Mode  
  
 Backup and restore operations are often used to move all or part of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation:  
  
-   If you are moving just the report server databases, you can use backup and restore or attach and detach to relocate the databases on a different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Moving the Report Server Databases to Another Computer &#40;SSRS Native Mode&#41;](../report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
-   Moving a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to a new computer is called a migration. When you migrate an installation, you run Setup to install a new report server instance and then copy instance data to the new computer. For more information about migrating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, see the following topics:  
  
    -   [Upgrade and Migrate Reporting Services](upgrade-and-migrate-reporting-services.md)  
  
    -   [Migrate a Reporting Services Installation &#40;SharePoint Mode&#41;](migrate-a-reporting-services-installation-sharepoint-mode.md)  
  
    -   [Migrate a Reporting Services Installation &#40;Native Mode&#41;](migrate-a-reporting-services-installation-native-mode.md)  
  
## Backing Up the Report Server Databases  
 Because a report server is a stateless server, all application data is stored in the **reportserver** and **reportservertempdb** databases that run on a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance. You can backup the **reportserver** and **reportservertempdb** databases using one of the supported methods for backing up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. Recommendations that are specific to the report server databases include the following:  
  
-   Use the full recovery model to backup the **reportserver** database.  
  
-   Use the simple recovery model to backup the **reportservertempdb** database.  
  
-   You can use different backup schedules for each database. The only reason to backup the **reportservertempdb** is to avoid having to recreate it if there is a hardware failure. In the event of hardware failure, it is not necessary to recover the data in **reportservertempdb**, but you do need the table structure. If you lose **reportservertempdb**, the only way to get it back is to recreate the report server database. If you recreate the **reportservertempdb**, it is important that it have the same name as the primary report server database.  
  
 For more information about backup and recovery of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases, see [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
> [!IMPORTANT]  
>  If your [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server is in SharePoint mode, there are additional databases to be concerned with, including SharePoint configuration databases and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] alerting database. In SharePoint mode, three databases are created for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. The **reportserver**, **reportservertempdb**, and **dataalerting** databases. For more information see [Backup and Restore Reporting Services SharePoint Service Applications](../backup-and-restore-reporting-services-sharepoint-service-applications.md)  
  
## Backing Up the Encryption Keys  
 You should backup the encryption keys when you configure a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation for the first time. You should also backup the keys any time you change the identity of the service accounts or rename the computer. For more information, see [Back Up and Restore Reporting Services Encryption Keys](ssrs-encryption-keys-back-up-and-restore-encryption-keys.md). For SharePoint mode report servers, see the "Key Management" section of [Manage a Reporting Services SharePoint Service Application](../manage-a-reporting-services-sharepoint-service-application.md).  
  
## Backing Up the Configuration Files  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses configuration files to store application settings. You should backup the files when you first configure the server and after you deploy any custom extensions. Files to back up include:  
  
-   Rsreportserver.config  
  
-   Rssvrpolicy.config  
  
-   Rsmgrpolicy.config  
  
-   Reportingservicesservice.exe.config  
  
-   Web.config for both the Report Server and Report Manager [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications  
  
-   Machine.config for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]  
  
## Backing Up Data Files  
 Backup the files that you create and maintain in Report Designer and Model Designer. These include report definition (.rdl) files, report model (.smdl) files, shared data source (.rds) files, data view (.dv) files, data source (.ds) files, report server project (.rptproj) files, and report solution (.sln) files.  
  
 Remember to backup any script files (.rss) that you created for administration or deployment tasks.  
  
 Verify that you have a backup copy of any custom extensions and custom assemblies you are using.  
  
## See Also  
 [Report Server Database &#40;SSRS Native Mode&#41;](../report-server/report-server-database-ssrs-native-mode.md)   
 [Reporting Services Configuration Files](../report-server/reporting-services-configuration-files.md)   
 [rskeymgmt Utility &#40;SSRS&#41;](../tools/rskeymgmt-utility-ssrs.md)   
 [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)   
 [Administer a Report Server Database &#40;SSRS Native Mode&#41;](../report-server/administer-a-report-server-database-ssrs-native-mode.md)   
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-manage-encryption-keys.md)  
  
  
