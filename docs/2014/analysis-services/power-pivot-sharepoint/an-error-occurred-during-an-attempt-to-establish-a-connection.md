---
title: "An error occurred during an attempt to establish a connection to the external data source. The following connections failed to refresh: PowerPivot Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 1b951da1-f62d-43d2-b40b-270a4a9ab92c
author: minewiskan
ms.author: owend
manager: craigg
---
# An error occurred during an attempt to establish a connection to the external data source. The following connections failed to refresh: PowerPivot Data
  This error occurs if you query PowerPivot data on a server that does not have PowerPivot for SharePoint installed. It also occurs if the SQL Server Analysis Services (PowerPivot) service is stopped, or if you are attempting to view PowerPivot data from an earlier version.  
  
## Details  
  
|||  
|-|-|  
|Applies to|PowerPivot for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|The data connection failed.|  
|Message Text|An error occurred during an attempt to establish a connection to the external data source. The following connections failed to refresh: PowerPivot Data|  
  
## Explanation  
 Excel Services returns this error when you query PowerPivot data in an Excel workbook that is published to SharePoint, and the SharePoint environment does not have a PowerPivot for SharePoint server, or the SQL Server Analysis Services (PowerPivot) service is stopped.  
  
 The error occurs when you slice or filter PowerPivot data when the query engine is not available.  
  
## User Action  
 Install PowerPivot for SharePoint or move the PowerPivot workbook to a SharePoint environment that has PowerPivot for SharePoint installed. For more information, see [PowerPivot for SharePoint 2010 Installation](../../sql-server/install/powerpivot-for-sharepoint-2010-installation.md).  
  
 If the software is installed, verify that the SQL Server Analysis Services (PowerPivot) instance is running. Check **Manage services on server** in Central Administration. Also check the Services console application in Administrative Tools.  
  
 For PowerPivot workbooks that were created in a SQL Server 2008 R2 version of PowerPivot for Excel, you must install the SQL Server 2008 R2 version of the Analysis Services OLE DB provider. This error will occur if you installed the provider, but did not register the Microsoft.AnalysisServices.ChannelTransport.dll file. For more information about file registration, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md).  
  
## See Also  
 [The data connection uses Windows Authentication and user credentials could not be delegated. The following connections failed to refresh: PowerPivot Data](the-data-connection-user-could-not-be-delegated.md)  
  
  
