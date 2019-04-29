---
title: "Power Pivot Data Refresh | Microsoft Docs"
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
# Power Pivot Data Refresh
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  After you create a workbook that contains [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data, you might want to periodically refresh the data by rerunning a query or command to get updated information from the sources you used originally to create the workbook. This process is called **data refresh**, and you can refresh data on demand in [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)], or as a scheduled operation that runs as an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] process on an application server in a SharePoint farm. For more information, see:  
  
-   [Power Pivot Data Refresh with SharePoint 2010](http://msdn.microsoft.com/01b54e6f-66e5-485c-acaa-3f9aa53119c9)  
  
-   [Configure the Power Pivot Unattended Data Refresh Account (Power Pivot for SharePoint)](http://msdn.microsoft.com/81401eac-c619-4fad-ad3e-599e7a6f8493)  
  
-   [Configure Stored Credentials for Power Pivot Data Refresh (Power Pivot for SharePoint)](http://msdn.microsoft.com/987eff0f-bcfe-4bbd-81e0-9aca993a2a75)  
  
-   [Schedule a Data Refresh (Power Pivot for SharePoint)](http://msdn.microsoft.com/8571208f-6aae-4058-83c6-9f916f5e2f9b)  
  
-   [View Data Refresh History &#40;Power Pivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/view-data-refresh-history-power-pivot-for-sharepoint.md)  
  
> [!NOTE]
>  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and SharePoint Server 2013 Excel Services use a different architecture for data refresh of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data models. The SharePoint 2013 supported architecture utilizes Excel Services as the primary component to load [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data models. The previous data refresh architecture used relied on a server running [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode to load data models. For more information, see the following:  
> 
>  -   [Power Pivot Data Refresh with SharePoint 2013](../../analysis-services/power-pivot-sharepoint/power-pivot-data-refresh-with-sharepoint-2013.md)  
> -   [Upgrade Workbooks and Scheduled Data Refresh &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/upgrade-workbooks-and-scheduled-data-refresh-sharepoint-2013.md)  
  
  
