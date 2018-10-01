---
title: "Predefined Roles | Microsoft Docs"
ms.date: 10/22/2015
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "security [Reporting Services], defaults"
  - "default security"
  - "role-based security [Reporting Services], defaults"
ms.assetid: 6b46db51-7c30-467d-a251-50f50647fe21
author: markingmyname
ms.author: maghan
---
# Role Definitions - Predefined Roles
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installs with predefined roles that you can use to grant access to report server operations. Each predefined role describes a collection of related tasks. You can assign groups and user accounts to predefined roles to provide immediate access to report server operations.  
  
## How to Use Predefined Roles  
  
1.  Review the predefined roles to determine whether you can use them as is. If you need to adjust the tasks or define additional roles, you should do this before you begin assigning users to specific roles.  
  
2.  Identify which users and groups require access to the report server, and at what level. Most users should be assigned to the **Browser** role or the **Report Builder** role. A smaller number of users should be assigned to the **Publisher** role. Very few users should be assigned to **Content Manager**.  
  
3.  When you are ready to assign user and group accounts to specific roles, use Report Manager. For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](../../reporting-services/security/grant-user-access-to-a-report-server-report-manager.md).  
  
##  <a name="bkmk_rolelist"></a> Predefined Role Definitions  
 Predefined roles are defined by the tasks that it supports. You can modify these roles or replace them with custom roles.  
  
 *Scope* defines the boundaries within which roles are used. Item-level roles provide varying levels of access to report server items and operations that affect those items. Item-level roles are defined on the root node (Home) and all items throughout the report server folder hierarchy. System-level roles authorize access at the site level. Item and system-level roles are mutually exclusive but are used together to provide comprehensive permissions to report server content and operations.  
  
 The following table describes the predefined roles, scope, and how they are used.  
  
|Predefined role|Scope|Description|  
|---------------------|-----------|-----------------|  
|[Content Manager Role](#bkmk_content)|Item|Includes all item-level tasks. Users who are assigned to this role have full permission to manage report server content, including the ability to grant permissions to other users, and to define the folder structure for storing reports and other items.|  
|[Publisher Role](#bkmk_publisher)|Item|Users who are assigned to this role can add items to a report server, including the ability to create and manage folders that contain those items.|  
|[Browser Role](#bkmk_browser)|Item|Users who are assigned to this role can run reports, subscribe to reports, and navigate through the folder structure.|  
|[Report Builder Role](#bkmk_reportbuilder)|Item|Users who are assigned to this role can create and edit reports in Report Builder.|  
|[My Reports Role](#bkmk_myreports)|Item|Users who are assigned to this role can manage a personal workspace for storing and using reports and other items.|  
|[System Administrator Role](#bkmk_systemadministrator)|System|Users who are assigned to this role can enable features and set defaults, set site-wide security, create role definitions in Management Studio, and manage jobs.|  
|[System User Role](#bkmk_systemuser)|System|Users who are assigned to this role can view basic information about the report server such as the schedule information in a shared schedule.|  
  
##  <a name="bkmk_content"></a> Content Manager Role  
 The **Content Manager** role is a predefined role that includes tasks that are useful for a user who manages reports and Web content, but does not necessarily author reports or manage a Web server or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. A content manager deploys reports, manages report models and data source connections, and makes decisions about how reports are used. All item-level tasks are selected by default for the **Content Manager** role definition.  
  
 The **Content Manager** role is often used with the **System Administrator** role. Together, the two role definitions provide a complete set of tasks for users who require full access to all items on a report server. Although the **Content Manager** role provides full access to reports, report models, folders, and other items within the folder hierarchy, it does not provide access to site-level items or operations. Tasks such as creating and managing shared schedules, setting server properties, and managing role definitions are system-level tasks that are included in the **System Administrator** role. For this reason, we recommend that you create a second role assignment at the site level that provides access to shared schedules.  
  
### Content Manager Tasks  
 The following table lists the tasks that are included in the **Content Manager** role.  
  
|Task|Description|  
|----------|-----------------|  
|Consume reports|Reads report definitions.|  
|Create linked reports|Create linked reports that are based on a non-linked report.|  
|Manage all subscriptions|View, modify, and delete any subscription for reports and linked reports, regardless of who owns the subscription. This task also supports the creation of data-driven subscriptions.|  
|Manage data sources|Create and delete shared data source items, view and modify data source properties and content.|  
|Manage folders|Create, view, and delete folders, and view and modify folder properties.|  
|Manage models|Create, view, and delete models, and view and modify model properties.|  
|Manage individual subscriptions|Create, view, modify, and delete user-owned subscriptions to reports and linked reports.|  
|Manage report history|Create, view, and delete report history, view report history properties, and view and modify settings that determine snapshot history limits and how caching works.|  
|Manage reports|Add and delete reports, modify report parameters, view and modify report properties, view and modify data sources that provide content to the report, view and modify report definitions, and set security policies at the report level.|  
|Manage resources|Create, modify, and delete resources, and view and modify resource properties.|  
|Set security policies for items|Define security policies for reports, linked reports, folders, resources, and data sources. For more information, see [Securable Items](../../reporting-services/security/securable-items.md).|  
|View data sources|View shared data source items in the folder hierarchy.|  
|View reports|Run reports and view report properties.|  
|View models|View models in the folder hierarchy, use models as data sources for a report, and run queries against the model to retrieve data.|  
|View resources|View resources and resource properties.|  
|View folders|View folder contents and navigate through the folder hierarchy.|  
  
### Customizing the Content Manager Role  
 This role is intended for trusted users who have overall responsibility for managing and maintaining report server content. You can remove tasks from this definition, but doing so may introduce ambiguity into what can be managed. For example, removing the "View reports" task from this role definition would prevent a **Content Manager** from viewing report contents and therefore be unable to verify changes to parameter and credential settings.  
  
 The **Content Manager** role is used in default security.  
  
##  <a name="bkmk_publisher"></a> Publisher Role  
 The **Publisher** role is a built-in role definition that includes tasks that enable users to add content to a report server. This role is predefined for your convenience. It is not used until you create role assignments that include it. This role is intended for users who author reports or models in Report Designer or Model Designer and then publish those items to a report server.  
  
> [!CAUTION]  
>  Permission to publish items to a report server should be granted only to trusted users. The Publisher role grants wide-ranging permissions that allow users to upload any type of file to a report server. If an uploaded report or HTML file contains malicious script, any user who clicks on the report or HTML document will run the script under his or her credentials.  
  
 Report definitions can include script and other elements that are vulnerable to HTML injection attacks when the report is rendered in HTML at run time. If a published report contains malicious script, any user who runs that report will accidentally cause the script to run when the report is opened. If the user has elevated permissions, the script will run with those permissions.  
  
 To reduce the risk of users accidentally running malicious scripts, limit the number of users who have permission to publish content, and make sure that users only publish documents and reports that come from trusted sources. If you are not sure whether a report definition is safe to publish, you should open the .rdl file in a text editor and search for script tags. Malicious script can be hidden in expressions and URLs (for example, a URL in a navigation action).  
  
### Publisher Tasks  
 The following table lists the tasks that are included in the **Publisher** role.  
  
|Task|Description|  
|----------|-----------------|  
|Create linked reports|Create linked reports and publish them to a report server folder.|  
|Manage data sources|Create and delete shared data source items, view and modify data source properties and content.|  
|Manage folders|Create, view, and delete folders; view and modify folder properties.|  
|Manage reports|Add and delete reports, modify report parameters, view and modify report properties, view and modify data sources that provide content to the report, view and modify report definitions.|  
|Manage models|Create, view, and delete report models; view and modify report model properties.|  
|Manage resources|Create, modify, and delete resources; view and modify resource properties.|  
  
### Customizing the Publisher Role  
 You can modify the **Publisher** role to suit your needs. For example, you can remove the "Create linked reports" task if you do not want users to be able to create and publish linked reports, or you can add the "View folders" task so that users can navigate through the folder hierarchy when selecting a location for a new item.  
  
 At a minimum, users who publish reports from Report Designer need the "Manage reports" task to be able to add a report to the report server. If the user must publish reports that use shared data sources or external files, you should also include "Manage data sources" and "Manage resources." If the user also requires the ability to create a folder as part of the publishing process, you must also include "Manage folders."  
  
##  <a name="bkmk_browser"></a> Browser Role  
 The **Browser** role is a predefined role that includes tasks that are useful for a user who views reports but does not necessarily author or manage them. This role provides basic capabilities for conventional use of a report server. Without these tasks, it may be difficult for users to use a report server.  
  
 The **Browser** role should be used with the **System User** role. Together, the two role definitions provide a complete set of tasks for users who interact with items on a report server. Although the **Browser** role provides view access to reports, report models, folders, and other items within the folder hierarchy, it does not provide access to site-level items such as shared schedules, which are useful to have when creating subscriptions. For this reason, we recommend that you create a second role assignment at the site level that provides access to shared schedules.  
  
### Browser Tasks  
 The following table describes the tasks that are included in the **Browser** role definition.  
  
|Task|Description|  
|----------|-----------------|  
|View reports|Run a report and view report properties.|  
|View resources|View resources and resource properties.|  
|View folders|View folder contents and navigate the folder hierarchy.|  
|View models|View models in the folder hierarchy, use models as data sources for a report, and run queries against the model to retrieve data.|  
|Manage individual subscriptions|Create, view, modify, and delete user-owned subscriptions to reports and linked reports, and create schedules in support of those subscriptions.|  
  
### Customizing the Browser Role  
 You can modify the **Browser** role to suit your needs. For example, you can remove the "Manage individual subscriptions" task if you do not want to support subscriptions, or you can remove the "View resources" task if you do not want users to see collateral documentation or other items that might be uploaded to the report server.  
  
 At a minimum, this role should support both the "View reports" task and the "View folders" tasks to support viewing and folder navigation. You should not remove the "View folders" task unless you want to eliminate folder navigation. Likewise, you should not remove the "View reports task" unless you want to prevent users from seeing reports. These kinds of modifications suggest the need for a custom role definition that is applied selectively for a specific group of users.  
  
##  <a name="bkmk_reportbuilder"></a> Report Builder Role  
 The **Report Builder** role is a predefined role that includes tasks for loading reports in Report Builder as well as viewing and navigating the folder hierarchy. To create and modify reports in Report Builder, you must also have a system role assignment that includes the "Execute report definitions" task, required for processing reports locally in Report Builder.  
  
### Report Builder Tasks  
 The following table describes the tasks that are included in the **Report Builder** role definition.  
  
|Task|Description|  
|----------|-----------------|  
|Consume reports|Reads report definitions.|  
|View reports|Run a report and view report properties.|  
|View resources|View resources and resource properties.|  
|View folders|View folder contents and navigate the folder hierarchy.|  
|View models|View models in the folder hierarchy, use models as data sources for a report, and run queries against the model to retrieve data.|  
|Manage individual subscriptions|Create, view, modify, and delete user-owned subscriptions to reports and linked reports, and create schedules in support of those subscriptions.|  
  
### Customizing the Report Builder Role  
 You can modify the **Report Builder** role to suit your needs. The recommendations are generally the same as for the **Browser** role: remove the "Manage individual subscriptions" task if you do not want to support subscriptions, remove the "View resources" task if you do not want users to see resources, and keep "View reports" task and the "View folders" tasks to support viewing and folder navigation.  
  
 The most important task in this role definition is "Consume reports", which allows a user to load a report definition from the report server into a local Report Builder instance. If you do not want to support this task, you can delete this role definition and use the **Browser** role to support general access to a report server.  
  
##  <a name="bkmk_myreports"></a> My Reports Role  
 The **My Reports** role is a predefined role that includes a set of tasks that are useful for users of the My Reports feature. This role definition includes tasks that grant administrative permissions to users over the My Reports folder that they own.  
  
 Although you can choose another role to use with the My Reports feature, it is recommended that you choose one that is used exclusively for My Reports security. For more information, see [Secure My Reports](../../reporting-services/security/secure-my-reports.md).  
  
### My Reports Tasks  
 The following table lists tasks that are included in the **My Reports** role.  
  
|Task|Description|  
|----------|-----------------|  
|Create linked reports|Create linked reports that are based on reports that are stored in the user's My Reports folder.|  
|Manage folders|Create, view, and delete folders, and view and modify folder properties.|  
|Manage data sources|Create and delete shared data source items, view and modify data source properties and content.|  
|Manage individual subscriptions|Create, view, modify, and delete subscriptions for reports and linked reports.|  
|Manage reports|Add and delete reports, modify report parameters, view and modify report properties, view and modify data sources that provide content to the report, view and modify report definitions, and set security policies at the report level.|  
|Manage resources|Create, modify, and delete resources, and view and modify resource properties.|  
|View reports|Run reports that are stored in the user's My Reports folder and view report properties.|  
|View data sources|View shared data source items in the folder hierarchy.|  
|View resources|View resources and resource properties.|  
|View folders|View folder contents.|  
  
### Customizing the My Reports Role  
 You can modify this role to suit your needs. However, it is recommended that you keep the "Manage reports" task and the "Manage folders" task to enable basic content management. In addition, this role should support all view-based tasks so that users can see folder contents and run the reports that they manage.  
  
 Although the "Set security policies for items" task is not part of the role definition by default, you can add this task to the **My Reports** role so that users can customize security settings for subfolders and reports.  
  
##  <a name="bkmk_systemadministrator"></a> System Administrator Role  
 The **System Administrator** role is a predefined role that includes tasks that are useful for a report server administrator who has overall responsibility for a report server, but not necessarily for the content within it.  
  
 To create a role assignment that includes this role, use the Site Settings page in Report Manager or use the right-click commands on the report server node in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 The **System Administrator** role does not convey the same full range of permissions that a local administrator might have on a computer. Rather, the **System Administrator** role includes operations that are performed at the site level, and not the item level. For users who require access to both site-wide operations and items stored on the report server, create a second role assignment on the Home folder that includes the **Content Manager** role. Together, the two role definitions provide a complete set of tasks for users who require full access to all items on a report server.  
  
### System Administrator Tasks  
 The following table lists tasks that are included in the **System Administrator** role.  
  
|Task|Description|  
|----------|-----------------|  
|Execute report definitions|Start execution for report definition without publishing it to a report server.|  
|Manage jobs|View and cancel jobs that are running. For more information, see [Manage a Running Process](../../reporting-services/subscriptions/manage-a-running-process.md).|  
|Manage report server properties|View and modify properties that apply to the report server and to items that the report server manages.<br /><br /> This task supports renaming Report Manager, enabling My Reports, and setting report history defaults.|  
|Manage roles|Create, view, and modify, and delete role definitions.<br /><br /> Members of the **System Administrator** role can use the Site Settings page to manage roles.|  
|Manage shared schedules|Create, view, modify, and delete shared schedules that are used to run or refresh reports.|  
|Manage report server security|View and modify system-wide role assignments|  
  
 The **System Administrator** role is used in default security.  
  
##  <a name="bkmk_systemuser"></a> System User Role  
 The **System User** role is a predefined role that includes tasks that allow users to view basic information about the report server. It also includes support for loading a report in Report Builder. Report Builder is a client application that can process a report independently of a report server. The "Execute report definitions" task is intended for use with Report Builder. If you are not using Reporting Builder, you can remove this task from the **System User** role. The following table lists tasks that are included in the **System User** role definition.  
  
### System User Tasks  
  
|Task|Description|  
|----------|-----------------|  
|Execute report definitions|Run a report without publishing it to a report server.|  
|View report server properties|View properties that apply to the report server, such as the application name, whether My Reports is enabled, and report history defaults.<br /><br /> If you remove this task from the **System User** role, the Site Settings page is not available. Also, the application title is not displayed at the top of each page. By default, the title for Report Manager is "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]."|  
|View shared schedules|View shared schedules that are used to run reports or refresh a report.<br /><br /> If you remove this task from the **System User** role, users cannot select shared schedules to use with subscriptions and other scheduled operations.|  
  
 The **System User** role can be used to supplement default security. You can include the role in new role assignments that extend report server access to report users. For more information, see [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md).  
  
## See Also  
 [Create, Delete, or Modify a Role &#40;Management Studio&#41;](../../reporting-services/security/role-definitions-create-delete-or-modify.md)   
 [Grant User Access to a Report Server &#40;Report Manager&#41;](../../reporting-services/security/grant-user-access-to-a-report-server-report-manager.md)   
 [Modify or Delete a Role Assignment &#40;Report Manager&#41;](../../reporting-services/security/role-assignments-modify-or-delete.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)   
 [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md)  
  
  
