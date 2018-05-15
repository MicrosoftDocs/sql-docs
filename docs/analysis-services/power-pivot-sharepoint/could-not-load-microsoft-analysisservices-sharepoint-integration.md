---

title: "Could not load 'Microsoft.AnalysisServices.SharePoint.Integration' | Microsoft Docs"
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
# Could not load Microsoft.AnalysisServices.SharePoint.Integration
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In SharePoint 2010 environments that have [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, this error will occur if the application-level solution for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] did not deploy correctly.  
  
## Details  
  
|||  
|-|-|  
|Applies to|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Powerpivotwebapp solution is not deployed or did not deploy correctly.|  
|Message Text|Could not load file or assembly 'Microsoft.AnalysisServices.SharePoint.Integration'|  
  
## Explanation  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint uses solution packages to deploy its features on a SharePoint server. One of the solutions is not deployed correctly. As a result, this error appears whenever you try to open [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery or other [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] application pages on a SharePoint site.  
  
## User Action  
 Deploy the solution package.  
  
1.  In Central Administration, in System Settings, click **Manage farm solutions**.  
  
2.  Click **Powerpivotwebapp**.  
  
3.  Click **Deploy Solution**.  
  
4.  Choose the web application for which this error is occurring. If there is more than one web application, redeploy the solution for all of hem.  
  
5.  Click **OK**.  
  
## See Also  
 [Deploy Power Pivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md)  
  
  
