---
title: "An error occurred during an attempt to establish a connection | Microsoft Docs"
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
# An error occurred during an attempt to establish a connection
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This error occurs if you query [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data on a server that does not have [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installed. It also occurs if the SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) service is stopped, or if you are attempting to view [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data from an earlier version.  
  
## Details  
  
|||  
|-|-|  
|Applies to|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|The data connection failed.|  
|Message Text|An error occurred during an attempt to establish a connection to the external data source. The following connections failed to refresh: [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Data|  
  
## Explanation  
 Excel Services returns this error when you query [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in an Excel workbook that is published to SharePoint, and the SharePoint environment does not have a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server, or the SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) service is stopped.  
  
 The error occurs when you slice or filter [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data when the query engine is not available.  
  
## User Action  
 Install [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint or move the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook to a SharePoint environment that has [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installed. For more information, see [Power Pivot for SharePoint 2010 Installation](http://msdn.microsoft.com/8d47dde7-c941-4280-a934-e2fe3f9a938f).  
  
 If the software is installed, verify that the SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) instance is running. Check **Manage services on server** in Central Administration. Also check the Services console application in Administrative Tools.  
  
 For [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that were created in a SQL Server 2008 R2 version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel, you must install the SQL Server 2008 R2 version of the Analysis Services OLE DB provider. This error will occur if you installed the provider, but did not register the Microsoft.AnalysisServices.ChannelTransport.dll file. For more information about file registration, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](http://msdn.microsoft.com/2c62daf9-1f2d-4508-a497-af62360ee859).  
  
## See Also  
 [The data connection uses Windows Authentication and user credentials could not be delegated. The following connections failed to refresh: Power Pivot Data](../../analysis-services/power-pivot-sharepoint/the-data-connection-user-could-not-be-delegated.md)  
  
  
