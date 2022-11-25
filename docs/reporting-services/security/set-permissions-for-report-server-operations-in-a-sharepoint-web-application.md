---
description: "Set Permissions for Report Server Operations in a SharePoint Web Application"
title: "Set Permissions for Report Server Operations in a SharePoint Web Application | Microsoft Docs"
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
  - "SharePoint integration [Report Builder]"
  - "security [Reporting Services], SharePoint integrated mode"
  - "Report Builder 1.0, SharePoint integration"
  - "model item security [Reporting Services]"
ms.assetid: 9ea71f1a-ee9e-4337-95ff-d7cef79946e7
author: maggiesMSFT
ms.author: maggies
---
# Set Permissions for Report Server Operations in a SharePoint Web Application
  For a report server that runs in SharePoint integrated mode, the security settings defined on the SharePoint site determine how you view and manage reports, report models, and shared data sources. If you are using the default SharePoint groups, permission levels, and permission assignments, you can work with reports and other documents using the current security settings.  
  
 If default security settings do not provide the level of access that you want, you can use the information provided in the following sections to learn which permissions are necessary for specific operations:  
  
-   [Permissions for viewing and managing reports](#permissionReports)  
  
-   [Permissions for creating reports and using Report Builder](#permissionReportBuilder)  
  
-   [Permissions for creating and managing shared schedules](#permissionSharedSchedules)  
  
-   [Permissions for creating and managing subscriptions](#permissionSubscriptions)  
  
-   [Permissions for creating and managing shared data sources and report models](#permissionDataSources)  
  
 A few key permissions are required to complete almost any operation on a SharePoint site. These permissions are not listed in the task and permission tables below, but you must include them if you are creating custom permission levels:  
  
-   Browse User Information  
  
-   Use Remote Interfaces  
  
-   Open  
  
-   View Application Pages  
  
 If you are using predefined permission levels, no action is required because the above permissions are already included in Full Control, Design, Contribute, Read, and Limited Access. However, if you are using custom permission levels or editing the permissions assigned to a particular user or group, you must add the permission manually.  
  
 "Browse User Information" permission allows the report server to return information about the creator of the item and the user who last modified the item. Without this permission, the report server will return the following errors. For browse operations, the error is: "Report Server has encountered a SharePoint error. ---> System.UnauthorizedAccessException: Access is denied." For publish operations, the error is: "The permissions granted to user '\<domain>\\<user\>' are insufficient for performing this operation."  
  
##  <a name="permissionReports"></a> Permissions for viewing and managing reports  
 Report definition permissions are defined through List permissions on the library that contains the report, but you can set permissions on individual reports if you want to restrict access. The following table provides a list of tasks and permissions that support each one.  
  
|Task|Permission|  
|----------|----------------|  
|View a report.|**View Items** on the library that contains the files or on the individual report.|  
|View a clickthrough report that uses a report model as a data source.|**View Items** on the library that contains the report and the report model, or on the individual report and model. If you do not have view permissions on the model, you can still open the report but you cannot perform ad hoc exploration on the data.<br /><br /> If the report model uses model item security, the user must also have **Enumerate Permissions** permission on the report model.|  
|View snapshots in report history.|**Edit Items** on the library that contains the files or on the individual report. For a specific report, you can view all or none of report history. It is not possible to set permissions on individual snapshots in report history.|  
|Upload or publish a report to library.|**Add Items** on the library that will contain the report.|  
|Set properties on a report, including data source connection information, processing options, and parameter properties.|**Edit Items** on the library that contains the report or on the individual report. You must also have view permissions on a shared data source (.rsds) to select it for use with the report.|  
|Schedule report processing.|To select a shared schedule, you must have **Open** on the site that contains the library that contains the report. To schedule data processing or cache expiration, you must have **Edit Items** on the library that contains the report or on the individual report.|  
|Delete a report.|**Delete Items** on the library that contains the report or on the individual report.|  
|Replace report definition (without affecting properties, permissions, history, or subscriptions) with a newer version.|**Edit Items** on the library that contains the report or on the individual report.|  
|Create snapshots in report history.|**Add Items** on the library that contains the report for which you are creating report history.|  
|Create snapshots in report history.|**Add Items** on the library that contains the report for which you are creating report history.|  
|Delete snapshots in report history, and delete specific versions of report definitions that have been checked out and modified over time.|**Delete Versions** on the library that contains the report for which you are deleting report history.|  
|View snapshots in report history, and view specific versions of report definitions that have been checked out and modified over time.|**Views Versions** on the library that contains the report.|  
  
##  <a name="permissionReportBuilder"></a> Permissions for creating reports and using Report Builder  
 Report Builder is a report authoring tool that you can use to create ad hoc reports. Report Builder uses report models as a data source to support ad hoc data exploration. You can load a model in Report Builder to create a report, run it, click through data in the model, and optionally save the report to a library. Users who have sufficient permission can subsequently open the same report and also perform ad hoc data exploration.  
  
> [!NOTE]  
>  Access to Report Builder can be determined by factors other than permissions. A site administrator can disable ad hoc reporting by setting server properties or limit the availability of Report Builder by not adding the Report Builder Report content type, which prevents users from creating new reports from the **New** menu on a library. In addition, a report server administrator can make Report Builder unavailable by setting properties on the report server. If any of these conditions are true for your server, you cannot use Report Builder even if you have the necessary permissions.  
  
 The following table provides a list of tasks for creating reports and using Report Builder, and permissions that support each one:  
  
|Task|Permission|  
|----------|----------------|  
|Start Report Builder.|There are no permissions that are explicitly used to control access to use Report Builder. Report Builder is available if report server integration is configured and you have permission to add items to a library. To start Report Builder from the **New** menu in library, you must register the Report Builder content type. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).|  
|Upload a model or shared data source.|**Add Items** on the library that will contain the files.|  
|View a model or dependent shared data source.|**View Items** on the library that contains the files.<br /><br /> If the model contains model item security settings, the user must also have **Enumerate Permissions** permission on the model.|  
|Generate a model from a shared data source.|**Add Items** on the library that contains the shared data source (.rsds) file from which you are generating the model.|  
|Set permissions within a model on specific model items.|**Manage Permissions** on the site that contains the library and report model (.smdl) file.|  
|Load a model in Report Builder.|**Edit Items** on the report model (.smdl) file.|  
|Create a report definition in Report Builder and save a report to a library.|**Add Items** to save the file to a library.|  
|Edit a report in Report Builder.|**Edit Items** on the report definition file.|  
  
 Permissions to create and use subscriptions, report history, and set report or data processing options on a Report Builder report are the same as those used for performing identical actions on standard report definition files.  
  
##  <a name="permissionSharedSchedules"></a> Permissions for creating and managing shared schedules  
 Shared schedules are not documents stored in a library. For this reason, creating and managing these schedules requires site permissions. You cannot restrict access to specific shared schedules. Any shared schedule that you create will be available to any user who has Open permission throughout the site.  
  
 The following table provides a list of tasks and permissions for creating, managing, and using shared schedules:  
  
|Task|Permission|  
|----------|----------------|  
|Create, edit, or delete a shared schedule.|**Manage Web Site** on the site.|  
|Select a shared schedule for subscription processing or data retrieval.|**Open** on the site that contains the library.|  
  
##  <a name="permissionSubscriptions"></a> Permissions for creating and managing subscriptions  
 SharePoint enforces a dependency between subscription and view permissions. You cannot subscribe to a report that you do not have permission to view. If you grant permissions to subscribe to a report, view permissions are granted automatically.  
  
 The following table provides a list of tasks and permissions for creating, managing, and using subscriptions:  
  
|Task|Permission|  
|----------|----------------|  
|Create, edit, or delete a user-owned subscription to a specific report.|**Edit Items** on the library that contains the report or on the report itself. View Items is a dependent permission and will be included in the permission level automatically. Users who can create a subscription can also create custom schedules to run that subscription.|  
|Select a shared schedule to use with the subscription.|**Open** on the site that contains the library.|  
|Create, edit, or delete any subscription throughout a site.|**Manage Alerts** on the site.|  
  
##  <a name="permissionDataSources"></a> Permissions for creating and managing shared data sources and report models  
 A shared data source (.rsds) file contains data source connection information that can be used by multiple reports and models. For standard reports, using an .rsds file to specify data source connection information is optional. For model-driven reports, using an .rsds file is required. A report model always uses an .rsds file to connect to external data sources.  
  
 You can set properties on shared data sources that determine whether individual users can view or manage shared data sources. Permissions to view or manage a shared data source is different from report viewing permissions; you can view a report that uses a .rsds file without having view permission on the .rsds file itself.  
  
|Tasks|Permission|  
|-----------|----------------|  
|Create a shared data source.|**Add Items** on the library that contains the shared data source. You can create new shared data sources from the New menu in a library. To do this, you must register the Report Data Source content type with the library. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).|  
|Edit a shared data source.|**Edit Items** on the library that contains the shared data source or on the shared data source itself.|  
|Delete a shared data source.|**Delete Items** on the library that contains the shared data source or on the shared data source itself.|  
|Use a shared data source (.rsds) with a report.|**Edit Items** on the report, or on the library that contains the report. Selecting a shared data source is part of setting data source properties on a report.|  
|Generate a report model from a shared data source.|**Add Items** on the library that will contain the report model.|  
|Delete a report model.|**Delete Items** on the library that contains the report model or on the report model itself.|  
|Set permissions within a model on specific model items.|**Manage Permissions** on the site that contains the library and report model (.smdl) file.|  
  
> [!NOTE]  
>  There are no permissions for editing report models. Although you can generate or delete report models, you cannot edit them from within a SharePoint site. Editing report models requires the Model Designer, a client authoring tool that is not affected by permissions you set in SharePoint.  
  
## See Also  
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Use Built-in Security in Windows SharePoint Services for Report Server Items](../../reporting-services/security/use-built-in-security-in-windows-sharepoint-services-for-report-server-items.md)  
  
  
