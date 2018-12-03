---
title: "Database Requirements (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: fe731839-c5c4-4884-bb6a-644eca28bb30
author: leolimsft
ms.author: lle
manager: craigg
---
# Database Requirements (Master Data Services)
  All master data is stored in a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database. The computer that hosts this database must run an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 Use [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] to create and configure the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database on either a local or a remote computer. If you move the database from one environment to another, you can maintain the information in a new environment by associating the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] web service and [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] to the database in its new location.  
  
> [!NOTE]  
>  Any computer on which you install components of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] must be licensed. For more information, refer to the End User License Agreement (EULA).  
  
## Requirements  
 Before you create a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database, ensure the following requirements are met.  
  
### SQL Server Edition  
 The [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database can be hosted on the following editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Business Intelligence (64-bit) x64  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Enterprise (64-bit) x64  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Developer (64-bit) x64  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Business Intelligence (64-bit) x64  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Enterprise (64-bit) x64 - Upgrade from [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Enterprise only  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Developer (64-bit) x64  
  
-   Microsoft SQL Server 2008 R2 Enterprise (64-bit) x64  
  
-   Microsoft SQL Server 2008 R2 Developer (64-bit) x64  
  
 For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
### Operating System  
 For information about the supported Windows operating systems and other requirements for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)], see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
### Accounts and Permissions  
  
|Type|Description|  
|----------|-----------------|  
|User account|In [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)], you can use a Windows account or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database. The user account must belong to the **sysadmin** server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For more information about the **sysadmin** role, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md).|  
|[!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] administrator account|When you create a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database, you must specify a domain user account to be the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] system administrator. For all [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web applications associated with this database, this user can update all models and all data in all functional areas. For more information, see [Administrators &#40;Master Data Services&#41;](../administrators-master-data-services.md).|  
  
### Database Backup  
 As a best practice, back up the full database daily at a time of low activity and back up transaction logs more frequently depending on the needs of your environment. For more information about database backups, see [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md).  
  
## See Also  
 [Install Master Data Services](install-master-data-services.md)   
 [Create a Master Data Services Database](create-a-master-data-services-database.md)   
 [Master Data Services Database](../master-data-services-database.md)   
 [Connect to a Master Data Services Database Dialog Box](../connect-to-a-master-data-services-database-dialog-box.md)   
 [Create Database Wizard &#40;Master Data Services Configuration Manager&#41;](../create-database-wizard-master-data-services-configuration-manager.md)  
  
  
