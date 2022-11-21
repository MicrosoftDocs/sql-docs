---
title: Associate database and web application
description: In SQL Server, you can associate a Master Data Manager web application with a Master Data Services database to specify the database to use for web operations.
ms.custom: "seo-lt-2019"
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: ccb25672-f71d-4135-b548-f50eb45d8fa5
author: CordeliaGrey
ms.author: jiwang6
---
# Associate a Master Data Services Database and Web Application

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Associate your [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application with a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database to specify the database to use for web operations.  
  
## Prerequisites  
  
-   [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] must be installed on the local computer. For more information, see [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md).  
  
-   A local [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application must exist. For more information, see [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](../../master-data-services/install-windows/create-a-master-data-manager-web-application-master-data-services.md).  
  
-   Either a local or remote [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database must exist. For more information, see [Create a Master Data Services Database](../../master-data-services/install-windows/create-a-master-data-services-database.md).  
  
### To associate a Master Data Services database and web application  
  
1.  Open [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
2.  In the left pane, click **Web Configuration**.  
  
3.  On the **Web Configuration** page, under **Web application**, from the **Website** list, select the website that contains your [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application.  
  
4.  In the **Web application** box, select the web application that hosts [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)].  
  
5.  Under **Associate Application with Database**, click **Select**. The **Connect to Database** dialog box opens.  
  
6.  Specify connection information for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database, and click **Connect**.  
  
7.  From the **Master Data Services database** list, select the database you want to associate the web application with and then click **OK**.  
  
8.  Under **Associate Application with Database**, verify that the instance and database information are correct, and then click **Apply**.  
  
## Next Steps  
  
-   Programmatic access to [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] web services is automatically enabled when the web application is created. To let developers access the service metadata to easily generate proxy classes for programmatic access, enable metadata publishing. For more information, see [Create Master Data Manager Web Service Proxy Classes](../../master-data-services/develop/create-master-data-manager-web-service-proxy-classes.md).  
  
-   Add users and groups to [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)]. If no users or groups have been granted access to [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)], you must open [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] using the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] system administrator credentials. For more information, see [Administrators &#40;Master Data Services&#41;](../../master-data-services/administrators-master-data-services.md) and [Users and Groups &#40;Master Data Services&#41;](../../master-data-services/users-and-groups-master-data-services.md).  
  
## See Also  
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)   
 [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/web-configuration-page-master-data-services-configuration-manager.md)  
  
  
