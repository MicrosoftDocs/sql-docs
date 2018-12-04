---
title: "Create or Customize a Data Feed Library (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data feed library"
  - "data feeds [Analysis Services with SharePoint]"
ms.assetid: 699fbeb9-42ab-436b-beba-214db51ea3dd
author: minewiskan
ms.author: owend
manager: craigg
---
# Create or Customize a Data Feed Library (PowerPivot for SharePoint)
  A *data feed library* is a special-purpose SharePoint library that enables you to register and share Atom data service documents (.atomsvc). These documents provide XML data feeds to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks or other client applications that support the Atom data feed format. A data feed library is different from other SharePoint libraries because it gives you the ability to:  
  
-   Create or edit a *data service document*, used to specify an HTTP connection to a specific feed.  
  
-   Share and manage data service documents in a central location.  
  
-   Visually identify data service documents by an icon, so that you can easily distinguish service documents from other documents stored in the same library: ![GMNI_IconDataFeed](../media/gmni-icondatafeed.gif "GMNI_IconDataFeed")  
  
 A data feed library always contains data service document (.atomsvc) files, and never the data feed itself. Unlike a data feed, which consists of static XML data, the data service document specifies a URL to a service or application that generates a feed upon request, providing reusable connection information for repeatable import operations.  
  
 This topic contains the following sections:  
  
 [Prerequisites](#prereq)  
  
 [Create a New Data Feed Library](#createlib)  
  
 [Add the Data Feed Content Type to Any Library](#addtolib)  
  
##  <a name="prereq"></a> Prerequisites  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] feature integration must be activated for the sites for which you are creating the data feed library. If the data feed library template type is not available, the most likely cause is that this prerequisite has not been met. For more information, see [Activate PowerPivot Feature Integration for Site Collections in Central Administration](activate-power-pivot-integration-for-site-collections-in-ca.md).  
  
 You must be a site owner to create the library.  
  
##  <a name="createlib"></a> Create a New Data Feed Library  
 Creating a data feed library is the first step to enabling data feeds for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks. Because a data feed library provides application and management pages for data service documents, you must have this library in place before you can create a new document.  
  
 A data feed library is based on a built-in template and a preconfigured *data service document content type* that defines properties and behaviors for a data service document.  
  
1.  Click **Site Actions** at the top left corner of the page.  
  
2.  Click **More Options**...  
  
3.  Under Libraries, click **Data Feed Library**.  
  
4.  Type the name, description, launch, and version preferences. Include descriptive information to help users identify this library as storage for data service documents.  
  
5.  Click **Create**.  
  
 A link to the data feed library will appear in the navigation Quick Launch pane for the current site.  
  
 After you create a library, you can use it to create data service documents. For more information, see [Use Data Feeds &#40;PowerPivot for SharePoint&#41;](use-data-feeds-power-pivot-for-sharepoint.md).  
  
##  <a name="addtolib"></a> Add the Data Feed Content Type to Any Library  
 If you do not want to create a dedicated data feed library, but you still want to create and manage data service documents from a SharePoint site, you can manually add and configure the data service document content type for any library that you will use to share data service document (.atomsvc) files.  
  
 You must have at least the Manage Lists permission to add and configure a content type. This permission is built into the Design permission level and above.  
  
 The following steps must be repeated for each library in which you want to create or edit data feed registration documents.  
  
#### Step 1: Enable Content Type Management  
  
1.  Open the document library for which you want to enable multiple content types.  
  
2.  On the SharePoint ribbon, in Library Tools, click **Library**.  
  
3.  Click **Settings**.  
  
4.  Click **Library Settings**.  
  
5.  In General Settings, click **Advanced settings**.  
  
6.  In Content Types, in the "Allow management of content types?" section, click **Yes**.  
  
7.  Click **OK**.  
  
#### Step 2: Add the Data Service Document Content Type  
  
1.  In the Content Types section, click **Add from existing site content types**. If you do not see this page, go back to the site, click **Library** in Library Tools, and then click **Library Settings**.  
  
2.  In Content Types, click **Add from existing site content types**.  
  
3.  In Select site content types from:, select **Business Intelligence**.  
  
4.  In Available Site Content Types, click **Data Service Document**, and then click **Add** to move the selected content type to the Content types to add list.  
  
5.  Click **OK**.  
  
#### Step 3: Verify Data Service Document Configuration  
  
1.  Open the site home page.  
  
2.  Open the library.  
  
3.  On the SharePoint ribbon, click **Documents**.  
  
4.  Click the down arrow on New Document, and select **Data Service Document**. The New Data Service Document page should appear.  
  
## See Also  
 [Use Data Feeds &#40;PowerPivot for SharePoint&#41;](use-data-feeds-power-pivot-for-sharepoint.md)   
 [Delete a PowerPivot Data Feed Library](delete-a-power-pivot-data-feed-library.md)   
 [PowerPivot Server Administration and Configuration in Central Administration](power-pivot-server-administration-and-configuration-in-central-administration.md)   
 [PowerPivot Data Feeds](power-pivot-data-feeds.md)  
  
  
