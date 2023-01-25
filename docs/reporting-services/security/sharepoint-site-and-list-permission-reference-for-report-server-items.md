---
description: "SharePoint Site and List Permission Reference for Report Server Items"
title: "SharePoint Site and List Permission Reference for Report Server Items | Microsoft Docs"
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
  - "security [Reporting Services], SharePoint integrated mode"
  - "permission sets [Reporting Services]"
ms.assetid: 1fcb27bd-4c4a-43f4-bfff-e42a59c87c49
author: maggiesMSFT
ms.author: maggies
---
# SharePoint Site and List Permission Reference for Report Server Items
  This topic provides a reference of the permissions in SharePoint that can be used to grant access to report server operations for a report server that runs in SharePoint integrated mode. If you are creating custom permission levels, this topic can help you choose which permissions to use.  
  
 SharePoint provides thirty-three permissions that you can use to control access to content and operations. Some but not all of those permissions apply to documents and operations that involve a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. You can use the permission reference tables in this article to find out which permissions support specific reporting tasks.  
  
 Each table starts with a list of SharePoint permissions and a description. The table includes three columns that indicate how a permission is used in predefined permission levels. The predefined permission levels include the following:  
  
|Permission level|Abbreviation|  
|----------------------|------------------|  
|Full Control|**F**|  
|Contribute|**C**|  
|Visitor|**V**|  
  
 Permissions that do not affect a report server are not listed. All personalization permissions are excluded from this reference article. Although you can include report server items in a personalized Web site, the report server does not directly handle personalization requests or operations.  

[!INCLUDE[applies](../../includes/applies-md.md)]

:::row:::
    :::column:::
        [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode  
    :::column-end:::
    :::column:::
        SharePoint 2010 and SharePoint 2013  
    :::column-end:::
:::row-end:::

## List Permissions  
 Permissions that you set on the library that contains report server items determine how users access those items.  
  
|Permission|Description|F|C|V|Report Server Operation|  
|----------------|-----------------|-------|-------|-------|-----------------------------|  
|Manage Lists|Create and delete lists, add or remove columns in a list, and add or remove public views of a list.|X|||Create a folder in a SharePoint library during a publish operation from an authoring tool. This permission is also required for managing report history.|  
|Add Items|Add items to lists, add documents to document libraries, and add Web discussion comments.|X|X||Add reports, report models, shared data sources, and resources (external image files) to SharePoint libraries. Create shared data sources. Generate report models from shared data sources. Start Report Builder and create a new report or load a model into Report Builder.|  
|Edit Items|Edit items in lists, edit documents in document libraries, edit Web discussion comments in documents, and customize Web Part Pages in document libraries.<br /><br /> Create subscriptions and edit subscriptions you created.|X|X||View past versions of a document, including report history snapshots. Edit item properties for reports and other documents. Set report processing options. Set parameters on a report. Edit data source properties. Create report history snapshots. Open a report model or a model-based report in Report Builder and save your changes to the file. Assign clickthrough reports to entities in a model. Replace a report definition, shared data source, report model, or resource with a newer version (replace file, preserve metadata). Manage dependent items that are referenced in a report or model. Customize the Report Viewer Web Part relative to a specific report.<br /><br /> Create, change and delete subscriptions that use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] delivery extensions to deliver reports to target locations. Only the subscription owner and users who have Manage Alerts permission can perform these actions.|  
|Delete Items|Delete items from a list, documents from a document library, and Web discussion comments in documents.|X|X||Delete reports, report models, shared data sources, and other documents from a library.|  
|View Items|View items in lists, documents in document libraries, and Web discussion comments.|X|X|X|Open a report, report model, and other document and have it processed on the report server.|  
|Open Items|View the source of documents with server-side file handlers.|X|X|X|View a list of shared data sources. Download a copy of the source file for a report definition or report model. View clickthrough reports that use a report model as a data source.|  
|View Versions|View past versions of a list item or document.|X|X|X|View past versions of a document and report snapshots.|  
|Delete Versions|Delete past versions of a list item or document.|X|X||Delete past versions of a document and report snapshots.|  
  
> [!NOTE]  
>  Other list permissions include Override Checkout, Approve Items, and View Application Pages. Those permissions are not evaluated by the report server. The report server does not handle those operations.  
  
## Site Permissions  
 Site permissions determine access to report server operations that are not directly related to items stored in a specific library. Examples include creating and managing shared schedules, which can be used by items in multiple libraries, and configuring the Report Viewer Web Part, which can be used throughout a site.  
  
|Permission|Description|F|C|V|Report Server Operation|  
|----------------|-----------------|-------|-------|-------|-----------------------------|  
|Manage Permissions|Create and change permission levels on the Web site and assign permissions to users and groups.|X|||You can change permissions for all report server items and operations. You can set model item security.|  
|Manage Web Site|Perform all administration tasks for the Web site as well as manage content.|X|||Create, change, and delete shared schedules.|  
|Add and Customize Pages|Add, change, or delete HTML pages or Web Part pages, and edit the Web site using a [!INCLUDE[winSPServ](../../includes/winspserv-md.md)]-compatible editor.|X|||Add or remove a Report Viewer Web Part.|  
|Browse User Information|View information about the users of the Web site.|X|X|X|Browse for reports and other items across different sites, libraries, and folders. Publish reports and other items to a library.|  
|Enumerate Permissions|Enumerate permissions on the Web site, list, folder, document, or list item.|X|||Read permissions for all report server items. View a clickthrough report that uses a report model that contains model item security settings.|  
|Manage Alerts|Manage alerts for all users of the Web site.|X|||Create, change, and delete any subscription on a site.|  
|Use Remote Interfaces|Use SOAP, Web DAV, or SharePoint Designer interfaces to access the Web site.|X|X|X|Used to call the URL proxy endpoint to the report server.|  
|Open|Open a Web site, list, or folder to access items inside that container.|X|X|X|Read schedules and item properties.|  
  
## See Also  
 [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
  
  
