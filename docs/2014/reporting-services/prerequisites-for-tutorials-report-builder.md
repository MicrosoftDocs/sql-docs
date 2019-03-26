---
title: "Prerequisites for Tutorials (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 9b8346a6-f4f4-4ad3-bc98-8f2be342ef2d
author: markingmyname
ms.author: maghan
manager: kfile
---
# Prerequisites for Tutorials (Report Builder)
  The Report Builder tutorials expect that you can view and save reports on a report server or SharePoint site that is integrated with a report server. For data, all tutorials use literal queries that must be processed by an instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
 If you do not have access to a report server or site or to a data source, you can learn about Report Builder by building an offline report. See [Tutorial: Create a Quick Chart Report Offline &#40;Report Builder&#41;](report-builder/tutorial-create-a-quick-chart-report-offline-report-builder.md).  
  
## Requirements  
 You must have the following prerequisites to complete Report Builder tutorials:  
  
-   Access to [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] Report Builder. You can run Report Builder by using the stand-alone version of Report Builder or the ClickOnce version, available from Report Manager or a SharePoint site. Only the first step, how to open Report Builder, is different for ClickOnce versions.  
  
     To use Report Manager, open Report Manager and click **Report Builder**. By default, the URL for Report Manager is http://\<*servername*>/reports.  
  
     To use a SharePoint site, navigate to the site, click the Documents tab, click New Document, and from the drop-down list, click Report Builder Report. For example, http://\<servername>/sites/mySite/reports. The SharePoint administrator must enable the Report Builder Report feature for each document library.  
  
-   The URL to a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] report server or a SharePoint site that is integrated with a [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] report server. You must have permission to save and view reports, shared data sources, shared datasets, report parts, and models. By default, the URL for a report server is http://\<servername>/reportserver. By default, the URL for a SharePoint site is http://\<sitename> or http://\<server>/site.  
  
-   The name of a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] instance and credentials sufficient for read-only access to any database. The dataset queries in the tutorials use literal data, but each query must be processed by a [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] instance to return the metadata that is required for a report dataset. For example, the following connection string specifies only a server: `data source=<servername>`. You must have read access to the default database that is assigned to you by the system administrator who grants you permission to access the server. You can also specify a database, as shown in the following connection string: `data source=<servername>;initial catalog=<database>`.  
  
-   For the tutorial that includes a map, the report server must be configured to support the Bing maps as a background. For more information, see [Plan for Map Report Support](plan-for-map-report-support.md) in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
-   The tutorial, [Tutorial: Creating Drillthrough and Main Reports &#40;Report Builder&#41;](tutorial-creating-drillthrough-and-main-reports-report-builder.md), uses the Contoso business intelligence demo dataset. This dataset is comprised of the ContosoDW data warehouse and the Contoso_Retail online analytical processing (OLAP) database. The reports you will create in this tutorial retrieve report data from the Contoso Sales cube. The Contoso_Retail OLAP database can be downloaded from [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=191575). You need only download the file ContosoBIdemoABF.exe. It contains the OLAP database.  
  
     The other file, ContosoBIdemoBAK.exe, is for the ContosoDW data warehouse, which is not used in this tutorial.  
  
     The Web site includes instructions extracting and restoring the ContosoRetail.abf backup file to the Contoso_Retail OLAP database.  
  
     You must have access to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on which to install the OLAP database.  
  
 The report server administrator must grant you the necessary permissions on the report server, configure [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] folder locations, and configure Report Builder default options. For more information, see [Install, Uninstall, and Report Builder Support](install-uninstall-and-report-builder-support.md).  
  
## See Also  
 [Tutorials &#40;Report Builder&#41;](report-builder-tutorials.md)  
  
  
