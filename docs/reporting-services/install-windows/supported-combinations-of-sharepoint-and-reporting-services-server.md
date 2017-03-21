---
title: "Supported Combinations of SharePoint and Reporting Services Server and Add-in (SQL Server 2016) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2016-10-07"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SharePoint mode"
  - "add-in for sharepoint"
  - "rsSharePoint"
ms.assetid: dc6a3372-db26-43f0-b7aa-f725acc635c2
caps.latest.revision: 39
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Supported Combinations of SharePoint and Reporting Services Server
[!INCLUDE[feedback_stackoverflow_msdn_connect_md](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server installed in SharePoint mode requires a version of SharePoint and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in (rsSharePoint.msi) for SharePoint products, which you install on the SharePoint servers.  This topic summarizes the supported combinations.  
  
## Supported Combinations of SharePoint and Reporting Services Components  
 The following table summarizes the supported combinations of report server, the Reporting Services add-in for SharePoint products, and SharePoint products. Combinations that are not list in the following table are not supported  
  
### Supported Combinations  
  
||Report server|Add-in|SharePoint version|  
|-|-------------------|-------------|------------------------|  
|1|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|SharePoint 2016|  
|2|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|SharePoint 2013|  
|3|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2013|  
|4|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|  
|5|SQL Server 2012 SP3|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and SQL Server 2012 SP3|SharePoint 2013|  
|6|SQL Server 2012 SP2|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and SQL Server 2012 SP2|SharePoint 2013|  
|7|[!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|SharePoint 2013|  
|8|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]*|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|  
|9|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|SharePoint 2010|  
|10|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|  
|11|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|SharePoint 2010|  
|12|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|SharePoint 2010|  
|13|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|SharePoint 2007|  
|14|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] R2|SharePoint 2010|  
|15|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|SharePoint 2007|  
  
 *Exception: Power view integration is not supported.  
  
 For links to the add-in download pages, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
 **Additional Notes:**  

- Be sure to upgrade to upgrade all of the SharePoint servers within the farm. This includes the App and Web Front End servers.

-   SharePoint 2016 support, including Power view integration, requires the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in version of SQL Server 2016 or later.  

-   SharePoint 2013 support, including Power view integration, requires the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in version of SQL Server 2012 SP1 or later.  
  
-   Power View was introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. Therefore, Power View integration with SharePoint 2010 requires the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or later of the add-in.  
  
-   The SQL Server 2008 R2 Add-In is not supported by SQL Server 2012 (or later) report servers. The SharePoint 2010 prerequisite installer automatically installs the SQL Server 2008 R2 Add-In. It must be uninstalled before installing newer versions of the add-in. In place upgrade of the add-in is not supported.  
  
-   **Upgrade:** SharePoint 2010 with the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-In installed, cannot be upgraded in-place to SharePoint 2013. SharePoint 2013 requires [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in and report server. For more information on upgrade, see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).  
  
## See Also  
 [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)   
 [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md)   
 [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
  
