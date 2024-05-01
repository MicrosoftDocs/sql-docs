---
title: "Tasks and permissions"
description: "Tasks and permissions"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "permissions [Reporting Services], tasks"
  - "role-based security [Reporting Services], permissions"
  - "security [Reporting Services], tasks"
  - "security [Reporting Services], permissions"
  - "role-based security [Reporting Services], tasks"
  - "predefined tasks [Reporting Services]"
  - "tasks [Reporting Services]"
---
# Tasks and permissions
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], *tasks* are actions that a user or administrator can perform. Tasks are predefined. You can't create custom tasks or modify the ones provided either programmatically or through a tool. There are 25 tasks in all. These tasks comprise the entire set of operations that are available in role-based security. Some examples of tasks include "View reports," "Manage reports," and "Manage report server properties."  
  
 Each task consists of a set of permissions, which are also predefined. For example, the "Manage folders" task contains permissions to create and delete folders and view and update folder properties. Permissions for each task are documented to provide a more exact description of each task. It isn't possible to interact with permissions directly or to specify them in role assignments. Users are granted permissions indirectly through the tasks that are included in role definitions.  
  
 Tasks can be performed only if they're part of a role and that role is included in a role assignment. Thus, if the View Models task isn't included in a role, or that role isn't included in a role assignment, users can't view report models. The following diagram shows how permissions are combined into tasks. It also shows how tasks are combined into roles that can be used for specific role assignments.  
 
 :::image type="content" source="../../reporting-services/security/media/report-securityobjects.gif" alt-text="Diagram that shows permissions and tasks."::: 
  
## System and item level tasks  
 Tasks fall into two categories: system level and item level. A role can include tasks only from a single category. The following table describes each category of tasks.  
  
|Category|Description|  
|--------------|-----------------|  
|[Item-level tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md)|Actions that are performed on items managed by a report server, such as folders, reports, report models, and resources.<br /><br /> Item-level tasks are scoped to the report server folder namespace. Role assignments that include item-level tasks secure all items accessed through the folders on a report server or through URL access.|  
|[System-level tasks](../../reporting-services/security/tasks-and-permissions-system-level-tasks.md)|Actions that are performed at the system level, such as managing jobs or shared schedules that can be used with many items. System-level tasks are scoped outside of the report server folder namespace.|  
  
## Related content  
 [Role definitions](../../reporting-services/security/role-definitions.md)   
 [Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md)   
 [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
