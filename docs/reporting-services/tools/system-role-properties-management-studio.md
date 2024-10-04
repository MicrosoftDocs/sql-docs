---
title: "System Role properties (Management Studio)"
description: Learn about the options on the System Roles page where you can view the system role definitions that are currently defined for the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.systemroleproperties.f1"
---
# System Role properties (Management Studio)
  Use the System Roles page to view the system role definitions that are currently defined for the report server. A system role definition contains a named collection of tasks that are performed relative to the entire site, instead of an individual item. Role definitions are assigned to a user or groups to create a resulting role assignment. The tasks in the role definition specify what the user or group can do.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] has two predefined system role definitions: **System Administrator** and **System User**. You can modify these role definitions by changing the task list, or you can create a new system role that supports a different combination of tasks. Editing a role definition affects all role assignments that include the role definition.  
  
> [!NOTE]  
>  System role assignments are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page is not available.  
  
## Options  
 **Name**  
 Specifies the name of the system role definition.  
  
 **Description**  
 Shows a description of the system role definition. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], this description is only visible in this page. Users who view this item through Report Manager might see this description when browsing the folder hierarchy.  
  
 **Task**  
 Lists all system-level tasks that can be selected for this role definition. You can add or remove items from the predefined task list to define how users access a given item through this role. You can't create new tasks, and you can't modify existing tasks.  
  
 **Description**  
 Provides information about each task. You can't modify task descriptions.  
  
## Related content

- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [System-level tasks](../../reporting-services/security/tasks-and-permissions-system-level-tasks.md)
- [Tasks and permissions](../../reporting-services/security/tasks-and-permissions.md)
- [Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md)
