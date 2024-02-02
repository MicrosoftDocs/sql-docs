---
title: "Use built-in security in Windows SharePoint services for report server items"
description: "Use built-in security in Windows SharePoint services for report server items"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
  - "security [Reporting Services], SharePoint integrated mode"
---
# Use built-in security in Windows SharePoint services for report server items
  SharePoint provides built-in security features that you can use to access report server items from SharePoint sites and libraries. If you already assigned site and list permissions to users, those same users will have access to report server items and operations immediately after you configure the integration settings between SharePoint and a report server.  
  
## Securable items  
 Permissions that are defined on the site or library can be used to grant access to report server items. However, if you want to secure individual items, you can set permissions on the following content types:  
  
|File type|Description|  
|---------------|-----------------|  
|.rdl|A report definition file that defines the report layout and the commands used to retrieve data. A report definition uses data source connection information to retrieve data when the report is processed. If you improvise the report definition in Report Builder, the report is paired with a report model (.smdl) file that sets the scope on data exploration in the rendered report.|  
|.smdl|A report model file that describes data structures and how they relate. Use this file to create and run Report Builder reports.|  
|.rsds|A shared data source file that specifies connection information to an external data source. Report definitions (.rdl) and report model (.smdl) files use this file. Report models always use .rsds files to get connection information to an underlying data source. Report definitions can use .rsds files, or connection information that is defined in data source properties on the report.|  
|.rsc|A report part file that defines the layout and structure of a report item or data region. Use the file to publish the report part to a server so other report authors can use the item from the Report Part Gallery.|  
|.rsd|A shared dataset file that defines query syntax and properties for a dataset. Shared datasets can be shared, stored, processed, and cached external from a report.|  

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]
  
 Schedules, subscriptions, and report history aren't securable items. You can set permissions on the site or library that determine whether a user can create or use schedules, subscriptions, and report history. But, you can't secure those items directly.  
  
 To secure individual items, select the item in the library, choose the down arrow, and select **Manage Permissions**. In the **Actions** menu, choose **Edit Permissions**.  
  
## Use built-in groups and permission levels to access report server items  
 When you use permission inheritance and standard SharePoint groups, you can access most report server operations immediately after you configure integration settings on the report server and SharePoint instances.  
  
 SharePoint provides standard groups that map to predefined permission levels that determine how you access documents and pages on a SharePoint site. You can expect to work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features if you use standard groups and default permission levels, and your sites are configured to inherit permissions. You can work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features in the following ways:  
  
|**SharePoint Groups**|**Permission level**|**Summary**|**Report Server Access**|  
|---------------------------|--------------------------|-----------------|------------------------------|  
|**Owners**|Full Control|Owners have full permissions to create, manage, and secure report server items and operations.|Set permissions that control access to all report server items stored in libraries throughout the site. Set permissions within a report model (also referred to as model item security). Customize a Report Viewer Web Part. Add reports and other items to libraries. Edit item properties for reports and other documents. Delete reports and other items. View reports, including reports that use report models for data exploration. Set parameters on reports. Set processing options on a report. Generate report models. Create reports in Report Builder. Create and manage shared data sources. Create, change, and delete subscriptions that other users own. Create and manage shared schedules used throughout the site. Create and manage versions of a document, including report history. Download the source file for a report definition or a report model. Replace a report definition, report model, shared data source, or resource (preserving item properties and permissions).|  
|**Members**|Contribute|Members can create new items and publish items reports and models from design tools to a SharePoint library.|Add reports and other items to libraries. Edit item properties for reports and other documents. Delete reports and other items. View reports, including reports that use report models for data exploration. View past versions of a document. You can view report history snapshots, which requires that a user also has permission to open the report for which report history was created. Set parameters on reports. Set processing options on a report. Generate report models. Create reports in Report Builder. Create and manage shared data sources. Create, change, and delete subscriptions that the user owns. Use shared schedules with a subscription. Create and manage versions of a document, including report history. Download the source file for a report definition or a report model. Replace a report definition, report model, shared data source, or resource (preserving item properties and permissions).|  
|**Visitors** and **Viewers**|Read|Visitors can view reports|View reports, including reports that use report models for data exploration.|  
  
 If you don't use the built-in groups and permission levels, you must include specific permissions in order to access [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features. For more information, see [Set permissions for report server operations in a SharePoint web application](../../reporting-services/security/set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md).  
  
## Related content  
 [Grant permissions on report server items on a SharePoint site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Compare roles and tasks in Reporting Services to SharePoint groups and permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)   
 [Set permissions for report server operations in a SharePoint web application](../../reporting-services/security/set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md)   
 [Grant permissions on report server items on a SharePoint site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
  
  
