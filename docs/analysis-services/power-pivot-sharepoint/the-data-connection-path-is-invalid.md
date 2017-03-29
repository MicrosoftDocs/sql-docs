---
title: "The data connection path is invalid | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: bd22e41a-0931-4d32-888a-633a3046fc5e
caps.latest.revision: 7
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# The data connection path is invalid
  For Excel workbooks that contain [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data, Excel Services returns this error if it cannot connect to embedded data sources.  
  
## Details  
  
|||  
|-|-|  
|Applies to|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Excel Services is configured to only allow data connections from .odc files that are in a trusted data connection library.|  
|Message Text|The data connection path in the workbook points to a file on the local drive or is an invalid URI. The following connections failed to refresh: [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Data|  
  
## Explanation  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks contain embedded data connections. To support workbook interaction through slicers and filters, Excel Services must be configured to allow external data access through embedded connection information. External data access is required for retrieving [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data that is loaded on [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] servers in the farm.  
  
## User Action  
 Change the configuration settings to allow embedded data source connections.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Click **Excel Services Application**.  
  
3.  Click **Trusted File Location**.  
  
4.  Click **http://** or the location you want to configure.  
  
5.  In External Data, in Allow External Data, click **Trusted data connection libraries and embedded**.  
  
6.  Click **OK**.  
  
 Alternatively, you can create a new trusted location for sites that contain [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks, and then modify the configuration settings for just that site. For more information, see [Create a trusted location for Power Pivot sites in Central Administration](../../analysis-services/power-pivot-sharepoint/create-a-trusted-location-for-power-pivot-sites-in-central-administration.md).  
  
  