---
title: "Set up the Database and Website for Master Data Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "master-data-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.mds.configmanager.general.f1"
ms.assetid: d50863e7-50d9-4ab8-aabb-fd68e2d132a1
author: leolimsft
ms.author: lle
manager: craigg
---
# Set up the Database and Website for Master Data Services
  Use the [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] to set up the database and website for [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] (MDS)  
  
 To set up the database and website, complete the following tasks.  
  
1.  Create a database using the **Database Configuration** page in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)].  
  
     For information, see [Database Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/database-configuration-page-master-data-services-configuration-manager.md) and [Create Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/create-database-wizard-master-data-services-configuration-manager.md).  
  
2.  Create a new website, select the default website, or select another existing website, using the **Web Configuration** page in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)]. Then associate the MDS database with the web application you select or create.  
  
     For information, see [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/web-configuration-page-master-data-services-configuration-manager.md) and [Create Website Dialog Box &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/create-website-dialog-box-master-data-services-configuration-manager.md).  
  
3.  (Optional) Enable integration with Data Quality Services using the **Web Configuration** page in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)].  
  
     For more information, see [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../2014/master-data-services/web-configuration-page-master-data-services-configuration-manager.md) and [Enable Data Quality Services Integration with Master Data Services](install-windows/enable-data-quality-services-integration-with-master-data-services.md).  
  
 You can also use [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] to specify settings for the web applications and services associated with the MDS database. For example, you can specify how frequently data is loaded or how often validation emails are sent. For more information, see [System Settings &#40;Master Data Services&#41;](../../2014/master-data-services/system-settings-master-data-services.md).  
  
## See Also  
 [Master Data Services Database](../../2014/master-data-services/master-data-services-database.md)   
 [Master Data Manager Web Application](../../2014/master-data-services/master-data-manager-web-application.md)  
  
  
