---
title: "System Role Properties (Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.systemroleproperties.f1"
ms.assetid: 0210fc2a-74fb-41dd-8e39-4830047ec417
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# System Role Properties (Management Studio)
  Use the System Roles page to view the system role definitions that are currently defined for the report server. A system role definition contains a named collection of tasks that are performed relative to the entire site, instead of an individual item. Role definitions are assigned to a user or groups to create a resulting role assignment. The tasks in the role definition specify what the user or group can do.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] has two predefined system role definitions: **System Administrator** and **System User**. You can modify these role definitions by changing the task list, or you can create a new system role that supports a different combination of tasks. Editing a role definition affects all role assignments that include the role definition.  
  
> [!NOTE]  
>  System role assignments are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page is not available.  
  
## Options  
 **Name**  
 Specifies the name of the system role definition.  
  
 **Description**  
 Shows a description of the system role definition. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], this description is only visible in this page. Users who view this item through Report Manager may see this description when browsing the folder hierarchy.  
  
 **Task**  
 Lists all system-level tasks that can be selected for this role definition. You can add or remove items from the predefined task list to define how users access a given item through this role. You cannot create new tasks, and you cannot modify existing tasks.  
  
 **Description**  
 Provides information about each task. You cannot modify task descriptions.  
  
## See Also  
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)   
 [System-Level Tasks](../security/tasks-and-permissions-system-level-tasks.md)   
 [Tasks and Permissions](../security/tasks-and-permissions.md)   
 [Predefined Roles](../security/role-definitions-predefined-roles.md)  
  
  
