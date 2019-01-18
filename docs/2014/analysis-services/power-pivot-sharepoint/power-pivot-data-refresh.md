---
title: "PowerPivot Data Refresh | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "unattended data refresh [Analysis Services with SharePoint]"
  - "scheduled data refresh [Analysis Services with SharePoint]"
  - "data refresh [Analysis Services with SharePoint]"
ms.assetid: ac8358a3-ee71-44c7-8ee6-ac7afe3ebaa4
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Data Refresh
  After you create a workbook that contains PowerPivot data, you might want to periodically refresh the data by rerunning a query or command to get updated information from the sources you used originally to create the workbook. This process is called `data refresh`, and you can refresh data on demand in [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)], or as a scheduled operation that runs as an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] process on an application server in a SharePoint farm. For more information, see:  
  
-   [PowerPivot Data Refresh with SharePoint 2010](../powerpivot-data-refresh-with-sharepoint-2010.md)  
  
-   [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md)  
  
-   [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](../configure-stored-credentials-data-refresh-powerpivot-sharepoint.md)  
  
-   [Schedule a Data Refresh &#40;PowerPivot for SharePoint&#41;](../schedule-a-data-refresh-powerpivot-for-sharepoint.md)  
  
-   [View Data Refresh History &#40;PowerPivot for SharePoint&#41;](view-data-refresh-history-power-pivot-for-sharepoint.md)  
  
> [!NOTE]
>  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and SharePoint Server 2013 Excel Services use a different architecture for data refresh of PowerPivot data models. The SharePoint 2013 supported architecture utilizes Excel Services as the primary component to load PowerPivot data models. The previous data refresh architecture used relied on a server running PowerPivot System Service and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode to load data models. For more information, see the following:  
> 
>  -   [PowerPivot Data Refresh with SharePoint 2013](power-pivot-data-refresh-with-sharepoint-2013.md)  
> -   [Upgrade Workbooks and Scheduled Data Refresh &#40;SharePoint 2013&#41;](../instances/install-windows/upgrade-workbooks-and-scheduled-data-refresh-sharepoint-2013.md)  
  
  
