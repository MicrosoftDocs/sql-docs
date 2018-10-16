---
title: "SharePoint Integration with 2008 and 2008 R2  Report Servers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d9f51c37-b071-45d0-baec-f82fa6db366f
author: markingmyname
ms.author: maghan
manager: craigg
---
# SharePoint Integration with 2008 and 2008 R2  Report Servers
  The [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] release of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] introduced a architecture where SharePoint mode is now based on a SharePoint Shared service. Management of the new functionality is completed in SharePoint Central administration on the **Manage Services** and **Manager Service Applications** pages. The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] previous architecture for SharePoint Integration is still supported with the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in for SharePoint 2010 products so you can integrate SharePoint 2010 with previous versions of a report server.  
  
 SharePoint Central Administration pages you would use to administer the older architecture are found in the following:  
  
1.  From SharePoint Central Administration click **General Application Settings**.  
  
2.  The group **SQL Server Reporting Services (2008 and 2008 R2)** contains the links and management pages for the older architecture  
  
## Server Integration Architecture  
 When you integrate a report server with an instance of a SharePoint product, items and properties are stored in the SharePoint content databases. This provides a deeper level of integration between the server technologies that effects how content is stored, secured, and accessed.  
  
 Storing report items and properties in SharePoint content databases allows you to browse SharePoint libraries for report server content types, secure items using the same permission levels and authentication provider that controls access to other business documents hosted on a SharePoint site, use the collaboration and document management features to check reports in and out for modification, use alerts to find out if an item has changed, and embed or customize the Report Viewer Web part on pages and sites within the application. If you have sufficient permissions within a SharePoint site, you can also generate report models from shared data sources and use Report Builder to create reports.  
  
 The report server continues to provide all data processing, rendering, and delivery. It also supports all scheduled report processing for snapshots and report history. When you open a report from a SharePoint site, the Report Server endpoint connects to a report server, creates a session, prepares the report for processing, retrieves data, merges the report into the report layout, and displays it in the Report Viewer Web part. While the report is open, you can export it to different application formats, or interact with data by drilling into underlying numbers or clicking through to a related report. Export and report interaction operations are performed on the report server.  
  
 The report server synchronizes operations and data with SharePoint and tracks information about the files it processes. When you modify properties or settings for any report server item, the change is stored in a SharePoint database and then copied to a report server database that provides internal storage to a report server.  
  
## Related Content  
 [Activate the Report Server and Power View Integration Features in SharePoint](activate-the-report-server-and-power-view-integration-features-in-sharepoint.md)  
 Describes how to activate the Report Server feature needed for integration with report servers from previous releases.  
  
  
