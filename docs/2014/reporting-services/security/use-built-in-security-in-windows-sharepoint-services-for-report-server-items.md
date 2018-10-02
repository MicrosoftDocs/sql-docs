---
title: "Use Built-in Security in Windows SharePoint Services for Report Server Items | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
  - "security [Reporting Services], SharePoint integrated mode"
ms.assetid: 9577e88d-c22b-4934-936f-e0f1400cedf5
author: markingmyname
ms.author: maghan
manager: craigg
---
# Use Built-in Security in Windows SharePoint Services for Report Server Items
  SharePoint provides built-in security features that you can use to access report server items from SharePoint sites and libraries. If you already assigned site and list permissions to users, those same users will have access to report server items and operations immediately after you configure the integration settings between SharePoint and a report server.  
  
## Securable Items  
 Permissions that are defined on the site or library can be used to grant access to report server items. However, if you want to secure individual items, you can set permissions on the following content types:  
  
|File type|Description|  
|---------------|-----------------|  
|.rdl|A report definition file that defines the report layout and the commands used to retrieve data. A report definition uses data source connection information to retrieve data when the report is processed. If the report definition is an ad hoc report that was created in Report Builder, the report is paired with a report model (.smdl) file that sets the scope on data exploration in the rendered report.|  
|.smdl|A report model file that describes data structures and how they relate. It is used to create and run Report Builder reports.|  
|.rsds|A shared data source file that specifies connection information to an external data source. It is used by report definitions (.rdl) and report model (.smdl) files. Report models always use .rsds files to get connection information to an underlying data source. Report definitions can use .rsds files, or connection information that is defined in data source properties on the report.|  
|.rsc|A report part file that defines the layout and structure of a report item or data region. It is used to publish the report part to a server so the item can be re-used by other report authors from the Report Part Gallery.|  
|.rsd|A shared dataset file that defines query syntax and properties for a dataset. Shared datasets can be shared, stored, processed, and cached external from a report.|  
  
 Schedules, subscriptions, and report history are not securable items. You can set permissions on the site or library that determine whether a user can create or use schedules, subscriptions, and report history, but you cannot secure those items directly.  
  
 To secure individual items, select the item in the library, click the down arrow, and select **Manage Permissions**. In the **Actions** menu, select **Edit Permissions**.  
  
## Using built-in groups and permission levels to access report server items  
 When you use permission inheritance and standard SharePoint groups, you can access most report server operations immediately after you configure integration settings on the report server and SharePoint instances.  
  
 SharePoint provides standard groups that map to predefined permission levels that determine how you access documents and pages on a SharePoint site. If you are using standard groups and default permission levels, and your sites are configured to inherit permissions, you can expect to work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features in the following ways:  
  
|**SharePoint Groups**|**Permission level**|**Summary**|**Report Server Access**|  
|---------------------------|--------------------------|-----------------|------------------------------|  
|**Owners**|Full Control|Owners have full permissions to create, manage, and secure report server items and operations.|Set permissions that control access to all report server items stored in libraries throughout the site. Set permissions within a report model (also referred to as model item security). Customize a Report Viewer Web Part. Add reports and other items to libraries. Edit item properties for reports and other documents. Delete reports and other items. View reports, including reports that use report models for data exploration. Set parameters on reports. Set processing options on a report. Generate report models. Create reports in Report Builder. Create and manage shared data sources. Create, change, and delete subscriptions that are owned by any user. Create and manage shared schedules used throughout the site. Create and manage versions of a document, including report history. Download the source file for a report definition or a report model. Replace a report definition, report model, shared data source, or resource (preserving item properties and permissions).|  
|**Members**|Contribute|Members can create new items and publish items reports and models from design tools to a SharePoint library.|Add reports and other items to libraries. Edit item properties for reports and other documents. Delete reports and other items. View reports, including reports that use report models for data exploration. View past versions of a document, including report history snapshots (requires that a user also has permission to open the report for which report history was created). Set parameters on reports. Set processing options on a report. Generate report models. Create reports in Report Builder. Create and manage shared data sources. Create, change, and delete subscriptions that are owned by the user. Use shared schedules with a subscription. Create and manage versions of a document, including report history. Download the source file for a report definition or a report model. Replace a report definition, report model, shared data source, or resource (preserving item properties and permissions).|  
|**Visitors** and **Viewers**|Read|Visitors can view reports|View reports, including reports that use report models for data exploration.|  
  
 If you are not using the built-in groups and permission levels, you must include specific permissions in order to access [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features. For more information, see [Set Permissions for Report Server Operations in a SharePoint Web Application](set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md).  
  
## See Also  
 [Granting Permissions on Report Server Items on a SharePoint Site](granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)   
 [Set Permissions for Report Server Operations in a SharePoint Web Application](set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
  
  
