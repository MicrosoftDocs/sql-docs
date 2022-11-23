---
description: "Tasks and Permissions - System-Level Tasks"
title: "System-Level Tasks | Microsoft Docs"
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "system-level tasks [Reporting Services]"
ms.assetid: 7023b388-40b2-4590-b227-115cf380a1e7
author: maggiesMSFT
ms.author: maggies
---
# Tasks and Permissions - System-Level Tasks
  A system-level task is a collection of permissions that relate to operations that apply to the report server site as a whole. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] also includes item-level tasks that apply to specific items. For more information, see [Item-Level Tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md). For more information about tasks and permissions in general, see [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md).  
  
> [!NOTE]  
>  If you are working with these tasks programmatically, you must use methods that support system-level tasks. For more information, see <xref:ReportService2010.ReportingService2010.ListTasks%2A> and <xref:ReportService2010.ReportingService2010.ListRoles%2A>.  
  
## Permissions in System-Level Tasks  
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
  
## See Also  
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
