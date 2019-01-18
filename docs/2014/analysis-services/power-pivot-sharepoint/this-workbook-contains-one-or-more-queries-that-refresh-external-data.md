---
title: "This workbook contains one or more queries that refresh external data. | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: aa65c992-eb41-4032-9e11-a9ba871b6a3c
author: minewiskan
ms.author: owend
manager: craigg
---
# This workbook contains one or more queries that refresh external data.
  For Excel workbooks that contain PowerPivot data, Excel Services shows this warning when it detects connection information and prompts you to enable or disable queries for this workbook.  
  
## Details  
  
|||  
|-|-|  
|Product Name|PowerPivot for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Excel Services is configured to warn on data refresh.|  
|Message Text|This workbook contains one or more queries that refresh external data. A malicious user can design a query to access confidential information and distribute it to other users or perform other harmful actions.<br /><br /> If you trust the source of this workbook, click Yes to enable queries to external data in this workbook. If you are not sure, click No so that changes are not applied to your workbook.<br /><br /> Do you want to enable queries to external data in this workbook?|  
  
## Explanation  
 PowerPivot workbooks contain embedded data connection strings that are used by Excel to communicate with an external PowerPivot server that loads and calculates the data. When warn on data refresh is enabled, Excel Services detects this connection string and warns the user accordingly.  
  
 To filter and slice PowerPivot data in the workbook, queries must be enabled. Be sure that you enable queries for only those PowerPivot workbooks that you trust.  
  
## User Action  
 Click **Yes** to enable queries.  
  
 You can also change the configuration settings so that warn on refresh no longer occurs:  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Click **Excel Services Application**.  
  
3.  Click **Trusted File Location**.  
  
4.  Click **http://** or the location you want to configure.  
  
5.  In External Data, clear the checkbox for **Warn on data refresh**.  
  
6.  Click **OK**.  
  
 Alternatively, you can create a new trusted location for sites that contain PowerPivot workbooks, and then modify the configuration settings for just that site. For more information, see [Create a trusted location for PowerPivot sites in Central Administration](create-a-trusted-location-for-power-pivot-sites-in-central-administration.md).  
  
  
