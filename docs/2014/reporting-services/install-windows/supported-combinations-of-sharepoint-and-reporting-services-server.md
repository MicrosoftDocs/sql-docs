---
title: "Supported Combinations of SharePoint and Reporting Services Server and Add-in (SQL Server 2014) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint mode"
  - "add-in for sharepoint"
ms.assetid: dc6a3372-db26-43f0-b7aa-f725acc635c2
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Supported Combinations of SharePoint and Reporting Services Server and Add-in (SQL Server 2014)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report servers can be installed in SharePoint mode and integrated with a SharePoint deployment. Not all features are supported in all combinations of report server, Reporting Services add-in for SharePoint, and SharePoint Products. This topic summarizes the supported combinations. In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] the integration is a result of the combination of the following:  
  
-   A version of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server configured for SharePoint mode.  
  
-   A SharePoint product.  
  
-   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products, which you install on the SharePoint servers.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2013 &#124; SharePoint 2010 &#124; SharePoint 2007|  
  
## Supported Combinations of SharePoint and Reporting Services Components  
 The following table summarizes the supported combinations of report server, the Reporting Services add-in for SharePoint products, and SharePoint products. Combinations that are not list in the following table are not supported  
  
### Supported Combinations  
  
||Report server|Add-in|SharePoint version|Supported|  
|-|-------------------|-------------|------------------------|---------------|  
|1|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2013|Yes|  
|2|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|Yes|  
|3|[!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|SharePoint 2013|Yes|  
|4|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|Yes<br /><br /> Exception: Power view integration is not supported.|  
|5|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|SharePoint 2010|Yes|  
|6|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|SharePoint 2010|Yes|  
|7|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]|SharePoint 2010|Yes|  
|8|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|SharePoint 2010|Yes|  
|9|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|SharePoint 2007|Yes|  
|10|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] R2|SharePoint 2010|Yes|  
|11|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] SP2|SharePoint 2007|Yes|  
  
 For more information on [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features and report server modes, see [Reporting Services Report Server](../reporting-services-report-server.md).  
  
 **Additional Notes:**  
  
-   SharePoint 2013 support, including Power view integration, requires the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in version of SQL Server 2012 SP1 or later.  
  
-   Power View was introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. Therefore, Power View integration with SharePoint 2010 requires the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or later of the add-in.  
  
-   The SQL Server 2008 R2 Add-In is not supported by SQL Server 2012 (or later) report servers. The SharePoint 2010 prerequisite installer automatically installs the SQL Server 2008 R2 Add-In. It must be uninstalled before installing newer versions of the add-in. In place upgrade of the add-in is not supported.  
  
-   **Upgrade:** SharePoint 2010 with the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-In installed, cannot be upgraded in-place to SharePoint 2013. SharePoint 2013 requires [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in and report server. For more information on upgrade, see [Upgrade and Migrate Reporting Services](upgrade-and-migrate-reporting-services.md).  
  
## See Also  
 [Where to find the Reporting Services add-in for SharePoint Products](where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)   
 [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md)   
 [Upgrade and Migrate Reporting Services](upgrade-and-migrate-reporting-services.md)  
  
  
