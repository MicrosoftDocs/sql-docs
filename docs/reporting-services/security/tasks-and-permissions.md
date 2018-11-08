---
title: "Tasks and Permissions | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], tasks"
  - "role-based security [Reporting Services], permissions"
  - "security [Reporting Services], tasks"
  - "security [Reporting Services], permissions"
  - "role-based security [Reporting Services], tasks"
  - "predefined tasks [Reporting Services]"
  - "tasks [Reporting Services]"
ms.assetid: d7ff90b5-b976-4270-b9ad-9d7b801d8263
author: markingmyname
ms.author: maghan
---
# Tasks and Permissions
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], *tasks* are actions that a user or administrator can perform. Tasks are predefined. You cannot create custom tasks or modify the ones provided either programmatically or through a tool. There are twenty-five tasks in all. These tasks comprise the entire set of operations that are available in role-based security. Some examples of tasks include "View reports," "Manage reports," and "Manage report server properties."  
  
 Each task consists of a set of permissions, which are also predefined. For example, the "Manage folders" task contains permissions to create and delete folders and view and update folder properties. Permissions for each task are documented to provide a more exact description of each task. It is not possible to interact with permissions directly or to specify them in role assignments. Users are granted permissions indirectly through the tasks that are included in role definitions.  
  
 Tasks can be performed only if they are part of a role and that role is included in a role assignment. Thus, if the View Models task is not included in a role, or that role is not included in a role assignment, users cannot view report models. The following diagram shows how permissions are combined into tasks, and how tasks are combined into roles that can be used for specific role assignments.  
  
 ![Permissions and task diagram](../../reporting-services/security/media/report-securityobjects.gif "Permissions and task diagram")  
Permissions and task diagram  
  
## System and Item Level Tasks  
 Tasks fall into two categories: system level and item level. A role can include tasks only from a single category. The following table describes each category of tasks.  
  
|Category|Description|  
|--------------|-----------------|  
|[Item-Level Tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md)|Actions that are performed on items managed by a report server, such as folders, reports, report models, and resources.<br /><br /> Item-level tasks are scoped to the report server folder namespace. All items that you access through the folders on a report server or through URL access are secured by role assignments that include item-level tasks.|  
|[System-Level Tasks](../../reporting-services/security/tasks-and-permissions-system-level-tasks.md)|Actions that are performed at the system level, such as managing jobs or shared schedules that can be used with many items. System-level tasks are scoped outside of the report server folder namespace.|  
  
## See Also  
 [Role Definitions](../../reporting-services/security/role-definitions.md)   
 [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
