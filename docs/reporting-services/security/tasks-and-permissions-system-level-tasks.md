---
title: "System-level tasks"
description: "Tasks and Permissions - System-Level Tasks"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "system-level tasks [Reporting Services]"
---
# System-level tasks
  A system-level task is a collection of permissions that relate to operations that apply to the report server site as a whole. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] also includes item-level tasks that apply to specific items. For more information, see [Item-level tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md). For more information about tasks and permissions in general, see [Tasks and permissions](../../reporting-services/security/tasks-and-permissions.md).  
  
> [!NOTE]  
>  If you are working with these tasks programmatically, you must use methods that support system-level tasks. For more information, see <xref:ReportService2010.ReportingService2010.ListTasks%2A> and <xref:ReportService2010.ReportingService2010.ListRoles%2A>.  
  
## Permissions in system-level tasks  
 The following table identifies the collection of permissions for each system task. Permissions are listed for informational purposes only, to provide a more exact description of the functionality available through each task.  
  
|Task|Permissions|  
|----------|-----------------|  
|Execute report definitions|Execute Report Definitions (the permission and task name are the same)|  
|Generate events|Generate Events|  
|Manage jobs|Read System Properties<br /><br /> Update System Properties|  
|Manage report server properties|Read System Properties<br /><br /> Update System Properties|  
|Manage roles|Create Roles<br /><br /> Delete Roles<br /><br /> Read Role Properties<br /><br /> Update Role Properties|  
|Manage shared schedules|Create Schedules|  
|Manage report server security|Read System Security Policies<br /><br /> Update System Security Policies|  
|View report server properties|Read System Properties|  
|View shared schedules|Read Schedules|  
  
## Related content

- [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)
