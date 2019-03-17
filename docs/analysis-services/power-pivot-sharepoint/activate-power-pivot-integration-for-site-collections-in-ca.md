---
title: "Activate Power Pivot Integration for Site Collections in CA | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Activate Power Pivot Integration for Site Collections in CA
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Activating [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] feature integration for specific site collections is required if you used the Existing Farm installation option to install SQL Server [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint. If you installed [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint using the New Server option, you can skip this task because SQL Server Setup already activated [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] feature integration for the root site collection when it configured your deployment.  
  
 Feature activation at the site collection level is necessary to make application pages and templates available to your sites, including configuration pages for scheduled data refresh and application pages for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery and Data Feed libraries.  
  
 You must activate [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] integration for each site collection that supports [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] query processing.  
  
## Prerequisites  
 You must be a site collection administrator.  
  
## Activate Power Pivot Features  
  
1.  On a SharePoint site, click **Site Actions**.  
  
     By default, SharePoint web applications are accessed through port 80. This means that you can often access a SharePoint site by entering http://\<computer name> to open the root site collection.  
  
2.  Click **Site Settings**.  
  
3.  In Site Collection Administration, click **Site collection features**.  
  
4.  Scroll down the page until you find **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Integration Site Collection Feature**.  
  
5.  Click **Activate**.  
  
6.  Repeat for additional site collections by opening each site and clicking **Site Actions**.  
  
## See Also  
 [Power Pivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md)   
 [Initial Configuration (Power Pivot for SharePoint)](http://msdn.microsoft.com/3a0ec2eb-017a-40db-b8d4-8aa8f4cdc146)   
 [Power Pivot for SharePoint 2010 Installation](http://msdn.microsoft.com/8d47dde7-c941-4280-a934-e2fe3f9a938f)  
  
  
