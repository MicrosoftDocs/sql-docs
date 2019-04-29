---
title: "Reporting Services Roles-Tasks vs. SharePoint Groups-Permissions | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "security [Reporting Services], tasks"
  - "roles [Reporting Services], predefined"
  - "SharePoint integration [Reporting Services], permissions"
  - "permissions [Reporting Services], native mode"
  - "security [Reporting Services], predefined roles"
  - "security [Reporting Services], SharePoint integrated mode"
ms.assetid: 429f1dbb-183a-4097-bd1b-693da9fe7a36
author: maggiesMSFT
ms.author: maggies
---
# Reporting Services Roles-Tasks vs. SharePoint Groups-Permissions
  This topic compares role and task based authorization features in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode to the security features in SharePoint products. This topic compares terminology and characteristics of roles, tasks, SharePoint groups, permission levels, and permissions.  
  
||  
|-|  
| [!INCLUDE[applies](../../includes/applies-md.md)]<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode &#124; SharePoint 2010 and SharePoint 2013<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode|  
  
 **In this topic:**  
  
-   [Compare Permission Tools and Terminology](#bkmk_compare_tools_terms)  
  
-   [Compare Native mode Roles and SharePoint Groups](#bkmk_compare_roles_groups)  
  
-   [Comparing Native Mode Tasks and SharePoint Permissions](#bkmk_compare_tasks_permissions)  
  
##  <a name="bkmk_compare_tools_terms"></a> Compare Permission Tools and Terminology  
 **Native mode:** The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode permission objects (roles and tasks) are created in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and configured for individual users in Report Manager.  
  
 **SharePoint mode:** [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode utilizes the SharePoint permission features. SharePoint groups and permissions are managed from the following the **Site Settings** page.  
  
 The following table compares permission related objects and concepts between [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode and SharePoint.  
  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode|SharePoint|  
|---------------------------------------------|----------------|  
|**Role:** For example "Content Manager".|**Group:** For example the default "Viewers" group.|  
|---|**Permission level group:** For example "View Only" for the "Viewers" group.|  
|**Tasks:** for example "Manage Reports".|**Permissions:** For example, within the "View Only" group there are list related permissions of view items, view versions, and view application pages.|  
  
 For more information on SharePoint permissions, see [Permission levels and permissions](https://support.office.com/en-us/article/Understand-groups-and-permissions-on-a-SharePoint-site-258E5F33-1B5A-4766-A503-D86655CF950D) and [Determine permission levels and groups in SharePoint 2013](https://technet.microsoft.com/library/cc262690.aspx).  
  
##  <a name="bkmk_compare_roles_groups"></a> Compare Native mode Roles and SharePoint Groups  
 The following table compares the predefined role definitions in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in native mode to standard SharePoint groups. If the SharePoint groups do not match the specific role that you want, you can create a custom group and assign permission levels in SharePoint.  
  
 **Note**: The default SharePoint groups available depend on the site template used to create the SharePoint site.  
  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Role|SharePoint Groups|  
|--------------------------------------|-----------------------|  
|**Browser**<br /><br /> View|Use the **Visitors** group to grant permissions to view reports. The **Visitors** group has Read level permissions, which enables group members to view pages, list items, and documents.|  
|**Content Manager**<br /><br /> Full permissions to all items and item-level operations, including permissions to set security.|Use the **Owners** group to grant full control over managing report server items on a SharePoint site. The **Owners** group has Full Control permissions, which enable group members to make changes to the site content, pages, or functionality. Full Control access should be limited to site administrators only.|  
|**My Reports**|There is no equivalent group. **My Reports** is not supported for a report server that runs in SharePoint mode. You can use the My Site features in [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] if you want to use equivalent functionality.|  
|**Publisher**<br /><br /> Add, update, view, and delete reports, report models, shared data sources, and resources.|Use the **Members** group to grant permissions to add items, edit items, and update references to dependent items on a SharePoint site. The **Members** group has Contribute level permissions, which allow group members to view pages, add and update items, and submit changes for approval.|  
|**Report Builder**<br /><br /> View reports, self-manage individual subscription, and open reports in Report Builder.|There is no predefined out of the box permission level or SharePoint group that is equivalent to the Report Builder report definition. By default, users who belong to the **Members** group or **Owners** group have permission to use Report Builder. If you want to make Report Builder available to more users, you should create custom security settings to provide a level of permission that is similar to what the Report Builder role provides. For more information, see [Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md).|  
|-|Use the **Viewers** group to grant permissions to view rendered reports. The **Viewers** group cannot download or view the contents of report items.<br /><br /> **Note:** Starting in SQL Server 2012 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], the **Viewers** group does not have permissions to create subscriptions.|  
|**System User** and **System Administrator**|These roles are not necessary for a report server that runs in SharePoint mode. **System User** and **System Administrator** correspond to SharePoint farm or Web application level permissions. The report server does not provide any functionality that requires authorization at that level.|  
  
##  <a name="bkmk_compare_tasks_permissions"></a> Comparing Native Mode Tasks and SharePoint Permissions  
 The following table compares [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode tasks to SharePoint permissions. The **Type** column indicates if the Native mode task is related to a system role or standard role and items. System roles manage permissions on a system level, for example shared schedules.  
  
|Native mode task|Role Type|Equivalent SharePoint Permission|  
|----------------------|---------------|--------------------------------------|  
|Consume reports|Item|Edit Items, View Items.|  
|Create linked reports|Item|Not supported.|  
|Manage all subscriptions|Item|Manage Alerts.|  
|Manage data sources|Item|Add items, Edit Items, Delete Items, View Items.|  
|Manage folders|Item|Add items, Edit Items, Delete Items, View Items.|  
|Manage individual subscriptions|Item|Edit Items<br /><br /> Prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the required permission level was Create alerts.|  
|Manage models|Item|Add items, Edit Items, Delete Items, View Items.|  
|Manage report history|Item|Edit Items, View Versions, Delete Versions.|  
|Manage reports|Item|Add items, Edit Items, Delete Items, View Items.|  
|Manage resources|Item|Add items, Edit Items, Delete Items, View Items.|  
|Set security for individual items|Item|Manage permissions|  
|View data sources|Item|View Items.|  
|View folders|Item|View Items.|  
|View models|Item|View Items.|  
|View reports|Item|View Items.|  
|View resources|Item|View Items.|  
||||  
|Execute report definitions|System|View items.|  
|Generate events|System|Manage Web Site.|  
|Manage jobs|System|None (not supported).|  
|Manage report server properties|System|None (not applicable). The report server does not control whether a user has permission to view integration settings in Central Administration.|  
|Manage roles|System|Manage Permissions.|  
|Manage shared schedules|System|Manage Web Site, Open.|  
|Manage report server security|System|None (not applicable). The report server does not use system-level role assignments on a server that runs in SharePoint integrated mode.|  
|View report server properties|System|None (not applicable). The report server does not control whether a user has permission to view integration settings in Central Administration.|  
|View shared schedules|System|Open Items.|  
  
## See Also  
 [Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)   
 [Set Permissions for Report Server Operations in a SharePoint Web Application](../../reporting-services/security/set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Role Definitions](../../reporting-services/security/role-definitions.md)   
 [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md)  
  
  
