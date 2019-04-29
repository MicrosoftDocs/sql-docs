---
title: "Could not load file or assembly &#39;Microsoft.AnalysisServices.SharePoint.Integration&#39; | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6e350b67-5e18-4b90-8fb7-a0109cbb27b7
author: minewiskan
ms.author: owend
manager: craigg
---
# Could not load file or assembly &#39;Microsoft.AnalysisServices.SharePoint.Integration&#39;
  In SharePoint 2010 environments that have PowerPivot for SharePoint, this error will occur if the application-level solution for PowerPivot did not deploy correctly.  
  
## Details  
  
|||  
|-|-|  
|Applies to|PowerPivot for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Powerpivotwebapp solution is not deployed or did not deploy correctly.|  
|Message Text|Could not load file or assembly 'Microsoft.AnalysisServices.SharePoint.Integration'|  
  
## Explanation  
 PowerPivot for SharePoint uses solution packages to deploy its features on a SharePoint server. One of the solutions is not deployed correctly. As a result, this error appears whenever you try to open PowerPivot Gallery or other PowerPivot application pages on a SharePoint site.  
  
## User Action  
 Deploy the solution package.  
  
1.  In Central Administration, in System Settings, click **Manage farm solutions**.  
  
2.  Click **Powerpivotwebapp**.  
  
3.  Click **Deploy Solution**.  
  
4.  Choose the web application for which this error is occurring. If there is more than one web application, redeploy the solution for all of hem.  
  
5.  Click **OK**.  
  
## See Also  
 [Deploy PowerPivot Solutions to SharePoint](deploy-power-pivot-solutions-to-sharepoint.md)  
  
  
