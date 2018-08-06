---
title: "Could not load file or assembly Microsoft-Data-Services | Microsoft Docs"
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
# Could not load file or assembly Microsoft-Data-Services
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In SharePoint 2010 environments that have [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, this error will occur if you attempt a data feed export and the system is missing the required version of Microsoft ADO.NET Data Services.  
  
## Details  
  
|||  
|-|-|  
|Applies to|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|ADO.NET Data Services 3.5 SP1 was not found.|  
|Message Text|Could not load file or assembly 'Microsoft.Data.Services, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' or one of its dependencies. The system cannot find the file specified|  
  
## Explanation  
 SharePoint 2010 includes an Export as Data Feed button that you can use to export a SharePoint list as an XML data feed. In addition, both Reporting Services in SharePoint mode and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint support data feed features that require ADO.NET Data Services.  
  
 If ADO.NET Data Services is not installed, this error will occur when you request a data feed.  
  
## User Action  
  
1.  Go to the hardware and software requirements documentation for SharePoint 2010, [Determine Hardware and Software Requirements (SharePoint 2010)](http://go.microsoft.com/fwlink/?LinkId=169734) (http://go.microsoft.com/fwlink/?LinkId=169734).  
  
2.  In **Installing software prerequisites**, find the link for ADO.NET Data Services 3.5 that corresponds to the operating system you are using.  
  
3.  Click the link and run the setup program that installs the service.  
  
## See Also  
 [Deploy Power Pivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md)  
  
  
