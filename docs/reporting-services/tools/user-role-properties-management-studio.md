---
title: "User Role Properties page (Management Studio)"
description: Learn about the User Role Properties page in SQL Server Management Studio where you view item-level role definition tasks, change the task list, or modify a role description.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.userroleproperties.f1"
---
# User Role Properties page (Management Studio)
  Use this page to view which tasks are included in an item-level role definition. You can also use this page to change the task list or modify a role description.  
  
 An item-level role definition is a named collection of tasks that users perform relative to a specific item (that is, a folder, report, resource, or shared data source). Role definitions are assigned to a user or group to create a role assignment in Report Manager. The tasks in the role definition describe what the user or group can do.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes many predefined item-level role definitions that you can work with. You can modify the role definitions by changing the task list of each one. Editing a role definition affects all role assignments that include the role definition.  
  
> [!NOTE]  
>  User role assignments are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page displays read-only information about the roles and permission levels that are defined on the SharePoint site.  
  
## Options  
 **Name**  
 Specifies the name of the role definition.  
  
 **Description**  
 Shows a description of the role definition. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], this description is only visible in this page. In Report Manager, this description helps users decide whether to use the role in a role assignment.  
  
 **Task**  
 Lists all item-level tasks that can be selected for this role definition. You can add or remove items from the predefined task list to define how users access a given item through this role. You can't create new tasks, and you can't modify existing tasks. The task list of a role definition appears only in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **Task Description**  
 Provides information about each task. You can't modify task descriptions.  
  
## Related content

- [Item-level tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md)
- [Role definitions](../../reporting-services/security/role-definitions.md)
- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [Tasks and permissions](../../reporting-services/security/tasks-and-permissions.md)
- [Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md)
