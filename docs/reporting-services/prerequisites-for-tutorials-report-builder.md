---
title: "Prerequisites for Tutorials (Report Builder) | Microsoft Docs"
description: Learn about the prerequisites that you must have in place to complete the Report Builder tutorials.
ms.date: 05/30/2017
ms.service: reporting-services
ms.subservice: reporting-services

ms.topic: conceptual
ms.assetid: 9b8346a6-f4f4-4ad3-bc98-8f2be342ef2d
author: maggiesMSFT
ms.author: maggies
---

# Prerequisites for Tutorials (Report Builder)

To do the Report Builder tutorials, you need to be able to view and save [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] paginated reports on a report server or SharePoint site that is integrated with a report server. For data, all tutorials use literal queries that must be processed by an instance of SQL Server.  
  
If you do not have access to a report server or site or to a data source, you can learn about Report Builder by building an offline report. See [Tutorial: Create a Quick Chart Report Offline &#40;Report Builder&#41;](../reporting-services/report-builder/tutorial-create-a-quick-chart-report-offline-report-builder.md).  

## Requirements

You must have the following prerequisites to complete Report Builder tutorials:  
  
-   Access to Report Builder. You can run Report Builder from a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] report server or a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] report server in SharePoint integrated mode. Only the first step, how to open Report Builder, is different on the different servers.  
  
    On a report server, select **New** > **Paginated Report**.
  
    On a report server in SharePoint integrated mode, on the **Documents** tab, select **New Document**, and from the drop-down list, select **Report Builder Report**. For example, `https://<servername>/sites/mySite/reports`. The SharePoint administrator must enable the Report Builder Report feature for each document library.  
  
-   The URL to a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] report server or a SharePoint site that is integrated with a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] report server. You must have permission to save and view reports, shared data sources, shared datasets, and models. By default, the URL for a report server is `https://<servername>/reportserver`. By default, the URL for a SharePoint site is `https://<sitename>` or `https://<server>/site`.  
  
-   The name of a SQL Server instance and credentials sufficient for read-only access to any database. The dataset queries in the tutorials use literal data, but each query must be processed by a SQL Server instance to return the metadata that is required for a report dataset. For example, the following connection string specifies only a server: `data source=<servername>`. You must have read access to the default database that is assigned to you by the system administrator who grants you permission to access the server. You can also specify a database, as shown in the following connection string: `data source=<servername>;initial catalog=<database>`.  
  
-   For the [Tutorial: Map Report (Report Builder)](tutorial-map-report-report-builder.md), the report server must be configured to support Bing maps as a background. For more information, see [Plan for Map Report Support](./report-design/plan-a-map-report-report-builder-and-ssrs.md).   

-   The [Tutorial: Creating Drillthrough and Main Reports (Report Builder)](tutorial-creating-drillthrough-and-main-reports-report-builder.md) tutorial requires access to the Contoso Sales cube. See the tutorial for more information. 
  
The report server administrator must grant you the necessary permissions on the report server, configure [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] folder locations, and configure Report Builder default options. For more information, see [Install Report Builder](install-windows/install-report-builder.md).  

## Next steps

[Report Builder tutorials](../reporting-services/report-builder-tutorials.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)