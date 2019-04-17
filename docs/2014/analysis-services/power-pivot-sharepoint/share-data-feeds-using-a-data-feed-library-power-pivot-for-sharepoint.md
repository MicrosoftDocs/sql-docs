---
title: "Share Data Feeds Using a Data Feed Library (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data feeds [Analysis Services with SharePoint]"
ms.assetid: 4ec98dec-0cd2-4727-bb79-5bf6f8a865d6
author: minewiskan
ms.author: owend
manager: craigg
---
# Share Data Feeds Using a Data Feed Library (PowerPivot for SharePoint)
  A data feed is an XML data stream that is generated from a service or application that exposes data in the Atom wire format. Increasingly, it is used to transport data between applications and to client-side viewers. In a PowerPivot for SharePoint deployment, data feeds are used to populate a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source with data from an Atom-aware application or service.  
  
 If you already use a combination of Atom-aware applications, you might never need to know how feeds are generated and consumed because the data transfer is seamless between the applications. However, organizations that use custom solutions to publish Atom feeds often need a way to make feeds available to information workers. One way to do that is to create and share data service document (.atomsvc) files that provide connections to the online sources that produce the feeds. A special-purpose library, called a data feed library, supports creating and sharing data service documents in a SharePoint web application.  
  
 This topic contains the following sections:  
  
 [Prerequisites](#prereq)  
  
 [Create a Data Service Document](#createdsdoc)  
  
 [Secure a Data Service Document](#securedsdoc)  
  
 [Modify a Data Service Document](#modifydsdoc)  
  
 [Next Step: Use a Data Service Document](#usedsdoc)  
  
> [!NOTE]  
>  Although data feeds are used to add Web data to a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source that you create in a [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)], any client application that can read an Atom feed can process a data service document.  
  
##  <a name="prereq"></a> Prerequisites  
 You must have a deployment of [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] PowerPivot for SharePoint that adds [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] query processing to a SharePoint farm. Data Feed support is deployed through the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution package.  
  
 You must have a SharePoint library that supports the data service document content type. A default Data Feed library is recommended for this purpose, but you can manually add the content type to any library. For more information, see [Create or Customize a Data Feed Library &#40;PowerPivot for SharePoint&#41;](create-or-customize-a-data-feed-library-power-pivot-for-sharepoint.md).  
  
 You must have a data service or online data source that provides XML tabular data in the Atom 1.0 format.  
  
 You must have Contribute permissions on a SharePoint site to create or manage a data service document in a SharePoint library.  
  
##  <a name="createdsdoc"></a> Create a Data Service Document  
 A data service document is a standing request to stream data upon request from an online data source or application that provides data in a feed format. When you create a data service document, you specify a pointer to one or more URL addressable data services that provide XML tabular in Atom syndicated format.  
  
 A single document can specify multiple data feeds. This is useful if you want to retrieve a set of data payloads from the same service, or even from different services, in a single import operation.  
  
1.  On a SharePoint site, open the Data Feed library or another document library to which you have added and configured the data service content type. To find a Data Feed library that was previously created, click **View All** on the Quick Launch.  
  
2.  On the ribbon at the top of the page, in Document Tools, click **Documents**.  
  
3.  Click **New Document,** and then select **Data Service Document**.  
  
4.  In the New Data Service Document page, enter the following information:  
  
    1.  A name and description for the data service document. Be sure to provide sufficient detail so that users can determine whether to use the feed.  
  
    2.  In Data Feed, enter a URL to a data service or Web application that provides data in the Atom 1.0 format.  
  
         The URL must resolve to a service that returns structured or semi-structured data in rows and columns. The service should return data anonymously or via the security credentials of the current user.  
  
         The URL must resolve to a service that supports Windows Authentication, Basic authentication, or anonymous access. The user who imports the feed specifies which scheme to use. Integrated security is selected by default.  
  
         A data feed URL can include parameters. Different types of data service technologies support advanced URL addressing schemes that allow you to precisely select the data you want to use. For example, an ADO.NET data service provides URL parameters for specifying entities, associations, and navigation paths in the underlying data. By specifying a complex URL as a source of a data feed, you can precisely specify the dataset you want to use.  
  
    3.  For the same data feed, enter a table name that subsequently identifies the dataset in a client application. In the [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)], each data feed that you import is placed in its own table control in an [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source. You must specify the name of the table that receives the imported data when you set up the data feed.  
  
5.  Click "Add another data feed" to repeat the previous steps for specifying additional feeds from the same service or a different service.  
  
     Each data service document is processed as a single operation. All of the data feeds in the document will be generated asynchronously and returned to a client application in the same operation. For this reason, only specify the URL-table pairs for data feeds that you want to use together.  
  
     Because authentication schemes are set at the data service document level, each additional data feed must originate from service or application that supports the same authentication scheme as the first feed. All feeds within the same data service document will be authenticated under the same method at run time.  
  
6.  Save the document. The data service document is stored as a physical file (.atomsvc) in a content library that is configured for this content type.  
  
 To use the data service document, you can open an [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook in the [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)] and choose the **From Data Feed** option in the Import Data wizard. When prompted, a user will specify the SharePoint URL of the data service document to start a data import operation. For more information, see [Use Data Feeds &#40;PowerPivot for SharePoint&#41;](use-data-feeds-power-pivot-for-sharepoint.md).  
  
##  <a name="securedsdoc"></a> Secure a Data Service Document  
 A data service document inherits the permissions of the library that contains it. Permissions that you set on the item will determine whether a user can open, modify or delete the data service document.  
  
 To use a data service document as a data feed import in the PowerPivot client application, a user only needs view permissions on the document. View permissions are sufficient to resolve the URL in the Import Wizard.  
  
 View permissions on a data service document are checked only when a data feed import operation begins. Post-import, permissions on the document are not checked on an ongoing basis; feeds that were added to a PowerPivot data source exist as static data, disconnected from the data service document that provided the original connection information.  
  
 Similarly, any data refresh operations that you subsequently schedule also exclude the data service document. At the time of import, connection information for each feed is copied into the PowerPivot data source for refresh purposes. As such, permissions on a data service document are not checked for data refresh, because the document itself is never referenced in a refresh operation.  
  
|Task|SharePoint permission requirements|  
|----------|----------------------------------------|  
|Import data feeds to a PowerPivot-enabled workbook.|View permissions to the data service document in a library.|  
|In the PowerPivot client application, refresh data that was previously retrieved via a feed.|Not applicable. The PowerPivot client application uses embedded HTTP connection information to connect directly to the data services and applications that provide the feed. The PowerPivot client application does not use the data service document.|  
|In a SharePoint farm, refresh data as a scheduled task, with no user input required.|Not applicable. The PowerPivot service uses embedded HTTP connection information to connect directly to the data services and applications that provide the feed. The PowerPivot service does not use the data service document.|  
|Delete a data service document in a library|Contribute permissions on the library.|  
  
##  <a name="modifydsdoc"></a> Modify a Data Service Document  
 You can add, edit, or remove individual URL-table entries in a data service document. After you save your changes, users who select the service document in a new import operation will get the data feeds you specified.  
  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that used a previous version of the document are unaffected by any changes you make. This is because a data service document is read only once during the initial import operation. During the import, service URL and table names are copied and stored internally in the workbook. These internal values are then used in subsequent refresh operations to get updated data.  
  
 Because there is no persistent link between a data service document on a SharePoint site and the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook that contains the imported feed, modifying any part of a data service document has no effect on existing [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks.  
  
> [!IMPORTANT]  
>  Although the data service document is read only once, data services that provide the actual data can be accessed at regular intervals to get newer feeds. For more information about how to refresh data, see [PowerPivot Data Refresh](power-pivot-data-refresh.md).  
  
##  <a name="usedsdoc"></a> Next Step: Use a Data Service Document  
 To use a data service document that you created in a SharePoint library, you use the **From Data Feeds** import option in a PowerPivot data source. For instructions, see [Use Data Feeds &#40;PowerPivot for SharePoint&#41;](use-data-feeds-power-pivot-for-sharepoint.md).  
  
## See Also  
 [PowerPivot Data Feeds](power-pivot-data-feeds.md)  
  
  
