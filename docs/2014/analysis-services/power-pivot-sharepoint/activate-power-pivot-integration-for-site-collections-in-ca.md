---
title: "Activate PowerPivot Feature Integration for Site Collections in Central Administration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 62a27e53-446a-42d7-b5db-c009e02d4904
author: minewiskan
ms.author: owend
manager: craigg
---
# Activate PowerPivot Feature Integration for Site Collections in Central Administration
  Activating PowerPivot feature integration for specific site collections is required if you used the Existing Farm installation option to install SQL Server PowerPivot for SharePoint. If you installed PowerPivot for SharePoint using the New Server option, you can skip this task because SQL Server Setup already activated PowerPivot feature integration for the root site collection when it configured your deployment.  
  
 Feature activation at the site collection level is necessary to make application pages and templates available to your sites, including configuration pages for scheduled data refresh and application pages for PowerPivot Gallery and Data Feed libraries.  
  
 You must activate PowerPivot integration for each site collection that supports PowerPivot query processing.  
  
## Prerequisites  
 You must be a site collection administrator.  
  
## Activate PowerPivot Features  
  
1.  On a SharePoint site, click **Site Actions**.  
  
     By default, SharePoint web applications are accessed through port 80. This means that you can often access a SharePoint site by entering http://\<computer name> to open the root site collection.  
  
2.  Click **Site Settings**.  
  
3.  In Site Collection Administration, click **Site collection features**.  
  
4.  Scroll down the page until you find **PowerPivot Integration Site Collection Feature**.  
  
5.  Click **Activate**.  
  
6.  Repeat for additional site collections by opening each site and clicking **Site Actions**.  
  
## See Also  
 [PowerPivot Server Administration and Configuration in Central Administration](power-pivot-server-administration-and-configuration-in-central-administration.md)   
 [Initial Configuration &#40;PowerPivot for SharePoint&#41;](../../sql-server/install/initial-configuration-powerpivot-for-sharepoint.md)   
 [PowerPivot for SharePoint 2010 Installation](../../sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
  
