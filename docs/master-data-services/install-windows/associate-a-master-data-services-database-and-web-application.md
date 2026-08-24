---
title: Associate Database and Web Application
description: In SQL Server, you can associate a Master Data Manager web application with a Master Data Services database to specify the database to use for web operations.
author: meetdeepak
ms.author: dkhare
ms.date: 03/05/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: how-to
ms.custom:
  - build-2025
---
# Associate a Master Data Services Database and Web Application

[!INCLUDE [SQL Server Windows Only - ASDBMI](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](../includes/support-notice.md)]

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
  
## Related content

- [Installation Tasks for Master Data Services](install-master-data-services.md)
- [Web Configuration Page (Master Data Services Configuration Manager)](../web-configuration-page-master-data-services-configuration-manager.md)
- [Create Master Data Manager Web Service Proxy Classes](../develop/create-master-data-manager-web-service-proxy-classes.md)
- [Administrators (Master Data Services)](../administrators-master-data-services.md)
- [Users and Groups (Master Data Services)](../users-and-groups-master-data-services.md)
