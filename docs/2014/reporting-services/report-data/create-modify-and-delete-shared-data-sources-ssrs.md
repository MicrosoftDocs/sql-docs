---
title: "Create, Modify, and Delete Shared Data Sources (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying data source properties"
  - "shared data sources [Reporting Services]"
  - "removing shared data sources"
  - "roles [Reporting Services], shared data sources"
  - "data sources [Reporting Services], shared"
  - "data sources [Reporting Services], modifying properties"
  - "deleting shared data sources"
ms.assetid: 1e58c1c2-5ecf-4ce6-9d04-0a8acfba17be
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create, Modify, and Delete Shared Data Sources (SSRS)
  A shared data source is a set of data source connection properties that can be referenced by multiple reports, models, and data-driven subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. Shared data sources provide an easy way to manage data source properties that often change over time. If a user account or password changes, or if you move the database to a different server, you can update the connection information in one place.  
  
 Shared data sources are optional for reports and data-driven subscriptions, but required for report models. If you plan to use report models for ad hoc reporting, you must create and maintain a shared data source item to provide connection information to the model.  
  
 A shared data source consists of the following parts:  
  
|Part|Description|  
|----------|-----------------|  
|Name|A name that identifies the item within the report server folder hierarchy.|  
|Description|A description that appears with the item in Report Manager when you view the contents of the folder.|  
|Connection type|The data processing extension used with the data source. You can only use data processing extensions that are deployed on the report server. For more information about data processing extensions included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md).|  
|Connection string|The connection string for the database. For more information and to view examples of connection strings to frequently used data sources, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md).|  
|Credential type|Specifies how credentials are obtained for the connection and whether they are to be used after the connection is made. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../integration-services/connection-manager/data-sources.md).|  
  
 A shared data source does not contain query information used to retrieve data. The query is always kept within a report definition.  
  
## Creating and Modifying a Shared Data Source  
 To create a shared data source or modify its properties, you must have **Manage data sources** permissions on the report server. If the report server runs in native mode, you can use Report Manager to create and configure the shared data source. If the report server runs in SharePoint integrated mode, you can use the application pages on a SharePoint site. For any report server regardless of its mode, you can create a shared data source in Report Designer and then publish it to a target server.  
  
 For more information about creating a shared data source, see:  
  
-   [Create an Embedded or Shared Data Source &#40;SSRS&#41;](../create-an-embedded-or-shared-data-source-ssrs.md)  
  
-   [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md)  
  
 After you create a shared data source on the report server, you can create role assignments to control access to it, move it to a different location, rename it, or take it offline to prevent report processing while maintenance operations are performed on the external data source. If you rename or move a shared data source item to another location in the report server folder hierarchy, the path information in all reports or subscriptions that reference the shared data source are updated accordingly. If you take the shared data source offline, all reports, models, and subscriptions will not run until you re-enable the data source.  
  
 For more information about controlling access to shared data sources in the report server folder hierarchy, see [Secure Shared Data Source Items](../security/secure-shared-data-source-items.md).  
  
## Deleting a Shared Data Source  
 You can delete a shared data source the same way that you delete any item from the report server. In Report Manager, you open the folder in Details View, select the item, and click **Delete**. In an application page on a SharePoint site, you open the SharePoint library, select the item, and click **Delete**.  
  
 Deleting a shared data source will deactivate any report, model, or data-driven subscription that uses it. Without the data source connection information, the items will no longer run. To activate these items, you must open each one and do the following:  
  
-   For reports and data-driven subscriptions that reference the shared data source, you can specify data source connection information in report properties or subscription, or you can select a new shared data source that has the values you want to use.  
  
-   For models and Report Builder reports that use that model, you must specify a new shared data source. Models get data source connection information only through shared data sources.  
  
 To view a list of reports and models that use the data source, open the Dependent Items page for the shared data source. You can access this page when you open the data source in Report Manager or a SharePoint application page. Note that the Dependent Items page does not show data-driven subscriptions. If a shared data source is used by a subscription, the subscription will not appear in the dependent items list.  
  
 There is no Undo operation for deleting a shared data source. However, if you accidentally delete a shared data source, you can create a new one using the same property values as the one you deleted. You will have to open each report, model, and data-driven subscription to rebind the shared data source to the item that uses it, but as long as the data source properties are the same as before, the reports, models, and subscriptions will continue to function as before.  
  
## See Also  
 [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Manage Report Data Sources](manage-report-data-sources.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](../embedded-and-shared-data-connections-or-data-sources-report-builder-and-ssrs.md)   
 [Data Sources Properties Page &#40;Report Manager&#41;](../data-sources-properties-page-report-manager.md)   
 [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../create-delete-or-modify-a-shared-data-source-report-manager.md)   
 [Configure Data Source Properties for a Report  &#40;Report Manager&#41;](configure-data-source-properties-for-a-report-report-manager.md)  
  
  
