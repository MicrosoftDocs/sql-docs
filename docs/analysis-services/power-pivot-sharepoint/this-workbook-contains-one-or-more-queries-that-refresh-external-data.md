---
title: "This workbook contains one or more queries that refresh external data | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: aa65c992-eb41-4032-9e11-a9ba871b6a3c
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# This workbook contains one or more queries that refresh external data
  For Excel workbooks that contain [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data, Excel Services shows this warning when it detects connection information and prompts you to enable or disable queries for this workbook.  
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11_md](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14_md](../../includes/sssql14-md.md)]|  
|Cause|Excel Services is configured to warn on data refresh.|  
|Message Text|This workbook contains one or more queries that refresh external data. A malicious user can design a query to access confidential information and distribute it to other users or perform other harmful actions.<br /><br /> If you trust the source of this workbook, click Yes to enable queries to external data in this workbook. If you are not sure, click No so that changes are not applied to your workbook.<br /><br /> Do you want to enable queries to external data in this workbook?|  
  
## Explanation  
 [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] workbooks contain embedded data connection strings that are used by Excel to communicate with an external [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server that loads and calculates the data. When warn on data refresh is enabled, Excel Services detects this connection string and warns the user accordingly.  
  
 To filter and slice [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] data in the workbook, queries must be enabled. Be sure that you enable queries for only those [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] workbooks that you trust.  
  
## User Action  
 Click **Yes** to enable queries.  
  
 You can also change the configuration settings so that warn on refresh no longer occurs:  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Click **Excel Services Application**.  
  
3.  Click **Trusted File Location**.  
  
4.  Click **http://** or the location you want to configure.  
  
5.  In External Data, clear the checkbox for **Warn on data refresh**.  
  
6.  Click **OK**.  
  
 Alternatively, you can create a new trusted location for sites that contain [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] workbooks, and then modify the configuration settings for just that site. For more information, see [Create a trusted location for Power Pivot sites in Central Administration](../../analysis-services/power-pivot-sharepoint/create-a-trusted-location-for-power-pivot-sites-in-central-administration.md).  
  
  