---
title: "Database Configuration Page (Master Data Services Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.mds.configmanager.dbpg.f1"
ms.assetid: dd72220e-a599-465d-8b84-9bb6a7433216
author: leolimsft
ms.author: lle
manager: craigg
---
# Database Configuration Page (Master Data Services Configuration Manager)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Use the **Database Configuration** page to edit system settings of a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. System settings affect all web applications and web services associated with the selected [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. You must select or create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database before system settings are enabled and available for configuration.  
  
## Current Database  
 Select an existing [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database or create a new database for which to edit system settings. The new database will be selected after it is created.  
  
|Control Name|Description|  
|------------------|-----------------|  
|**SQL Server instance**|Displays the name of the selected [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. This is blank until you connect to an instance, and then select or create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|  
|**Master Data Services database**|Displays the name of the selected [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. This is blank until you connect to an instance, and then select or create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|  
|**Master Data Services database version**|The version of the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database schema.|  
|**Create Database**|Opens the **Create Database** wizard from which you connect to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and create a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database for that instance.|  
|**Select Database**|Opens the **Connect to Database** dialog box from which you connect to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and select a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|  
|**Upgrade Database**|Opens a wizard from which you can upgrade a specified [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. This button is enabled only when the specified database requires upgrade.|  
|**Repair Database**|Click this button to ensure the MDS database is installed correctly. This can be useful if you backup and restore an MDS database to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that has never hosted an MDS database.|  
  
## System Settings  
 Edit system settings for all the web applications and web services associated with the selected [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
 These settings are available in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] and are stored in the database in the System Settings table (mdm.tblSystemSetting). For a list of all settings, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
## See Also  
[Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md)
 [Database Requirements &#40;Master Data Services&#41;](../master-data-services/install-windows/database-requirements-master-data-services.md)  
  
  
