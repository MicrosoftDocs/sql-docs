---
title: "Use Data Feeds (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 50140fdf-6fd1-41a1-9c14-8ecfb97ba2e1
author: minewiskan
ms.author: owend
manager: craigg
---
# Use Data Feeds (PowerPivot for SharePoint)
  Data feeds are one or more data streams that are generated from an online data source and streamed to a destination document or application. If you are using PowerPivot for Excel, data feeds can help you get existing corporate or business data from arbitrary data sources into the PowerPivot window in your Excel 2010 workbook. After you import a data feed to a workbook, you can reference it later in any data refresh operations that you schedule on a SharePoint server.  
  
 How you use a data feed depends on whether you are using built-in export features in applications that support Atom data feeds, or creating and using custom data services. Applications that are able to publish and read Atom XML data provide seamless data transfer that hides the mechanics of data feeds and data services from users. To a user, he or she is simply moving data from one application to another.  
  
 [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and Microsoft SharePoint 2010 provide data feeds that can be used in PowerPivot workbooks. You can use the information in this topic to learn how to access data feeds from reports and lists that you already have.  
  
 This topic contains the following sections:  
  
 [Prerequisites](#prereq)  
  
 [Create a Data Feed from a SharePoint List](#sharepointlist)  
  
 [Create a Data Feed from a Reporting Services Report](#rsreport)  
  
 [Create a Data Feed from a Data Service Document](#dsdoc)  
  
##  <a name="prereq"></a> Prerequisites  
 You must have the PowerPivot for Excel to import a data feed into Excel 2010.  
  
 You must have a Web service or a data service that provides data in the Atom 1.0 format. Both [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and SharePoint 2010 can provide data in this format.  
  
 Before you can export a SharePoint list as a data feed, you must install ADO.NET Data Services on the SharePoint server. For more information, see [Install ADO.NET Data Services to support data feed exports of SharePoint lists](../../sql-server/install/install-ado-net-data-services-to-support-data-feed-exports-of-sharepoint-lists.md).  
  
##  <a name="sharepointlist"></a> Create a Data Feed from a SharePoint List  
 In a SharePoint 2010 farm, a SharePoint list has an Export as Data Feed button on the List ribbon. You can click this button to export the list as a feed. For best results, you should have Excel 2010 with the PowerPivot client application on your workstation. The PowerPivot client application will launch in response to the data feed export, creating a new PowerPivot table that contains the list.  
  
1.  Open the list on your SharePoint site.  
  
2.  In List Tools, click **List**.  
  
3.  In Connect and Export, click **Export as Data Feed**.  
  
    > [!NOTE]  
    >  The **Export as Data Feed** button is added to SharePoint by way of PowerPivot. If you do not have PowerPivot for SharePoint installed or you did not activate the PowerPivot feature, this button will not be available.  
  
4.  Click **Open** if PowerPivot for Excel is installed locally, or click **Save** to save the .atomsvc document to your hard drive for import operations at a later time.  
  
5.  If you chose **Open**, use the Table Import Wizard to import the data feed to a worksheet. The data feed will be added as a new table in the PowerPivot window.  
  
 An error will occur if ADO.NET Data Services 3.5.1 is not installed on the SharePoint server. For more information about the error and how to resolve it, see [Install ADO.NET Data Services to support data feed exports of SharePoint lists](../../sql-server/install/install-ado-net-data-services-to-support-data-feed-exports-of-sharepoint-lists.md).  
  
##  <a name="rsreport"></a> Create a Data Feed from a Reporting Services Report  
 If you have a deployment of SQL Server 2008 R2 Reporting Services, you can use the new Atom rendering extension to generate a data feed from an existing report. For best results, you should have Excel 2010 with the PowerPivot for Excel on your workstation. The PowerPivot client application will launch in response to the data feed export, automatically adding and relating the tables and columns as they are streamed in.  
  
 For instructions on how to export a data feed from a report, see [Generate Data Feeds from a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/generate-data-feeds-from-a-report-report-builder-and-ssrs.md) in the [Report Builder help file](https://go.microsoft.com/fwlink/?LinkId=154494).  
  
> [!NOTE]  
>  To set up a recurring data refresh schedule that re-imports report data into a PowerPivot workbook that is published to a SharePoint library, the report server must be configured for SharePoint integration. For more information about using PowerPivot for SharePoint and Reporting Services together, see [Configuration and Administration of a Report Server &#40;Reporting Services SharePoint Mode&#41;](../../reporting-services/configure-administer-report-server-reporting-services-sharepoint-mode.md).  
  
##  <a name="dsdoc"></a> Create a Data Feed from a Data Service Document  
 If you have a custom data service that generates Atom feeds, you can set up a data service document as a way to make the data available to users and applications. A *data service document* (.atomsvc) file specifies one or more connections to online sources that publish data in the Atom wire format. Data service documents can be created in a *data feed library*, which is a special-purpose library that provides a common access point for browsing data service documents that have been published to a SharePoint server. Information workers who have permission to access data service documents in the data feed library can reference the document's SharePoint URL to import the data feeds to their workbooks and applications.  
  
1.  Open a data feed library that was created by your site administrator. For more information, see [Create or Customize a Data Feed Library &#40;PowerPivot for SharePoint&#41;](create-or-customize-a-data-feed-library-power-pivot-for-sharepoint.md).  
  
2.  In Library Tools, click **Documents**.  
  
3.  Click **New Document**.  
  
4.  Provide a file name and description.  
  
5.  Specify one or more URLs that provide the feed:  
  
    1.  **Base URL** is optional. You should specify it if a data service document provides multiple feeds. Base URL should specify the portion of the URL that is common to all the feeds (for example, the server name and site). If you are creating a data service document to a Reporting Services report, the base URL would be the report server URL and report.  
  
    2.  **Web Service URL** is required. Without the Base URL, this value must include http:// or https:// in the address. If you specified a Base URL, the Web service URL is the portion that follows the Base URL. For example, if the full URL is http://adventure-works/inventory/today.aspx, the Base URL would be http://adventure-works/inventory, and the Web service URL would be /today.aspx.  
  
         The Web service URL can include parameters that filter or select a subset of data. The application or service that provides the feed must support the parameters that you specify in the URL.  
  
6.  Enter a **Table Name**, one table for each feed. This value is required. The table name is used by a client application that consumes the data feed. In PowerPivot for Excel, the table name is used to name tables in the PowerPivot window that will contain the imported data.  
  
## See Also  
 [Activate PowerPivot Feature Integration for Site Collections in Central Administration](activate-power-pivot-integration-for-site-collections-in-ca.md)   
 [Share Data Feeds Using a Data Feed Library &#40;PowerPivot for SharePoint&#41;](share-data-feeds-using-a-data-feed-library-power-pivot-for-sharepoint.md)  
  
  
