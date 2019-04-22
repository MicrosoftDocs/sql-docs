---
title: "New System Role Assignments: Edit System Role Assignments Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 62a22ab9-1eb4-4ce5-8dd7-06b5ed2d9a2a
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# New System Role Assignments: Edit System Role Assignments Page (Report Manager)
  Use the New System Role Assignments or Edit System Role Assignments page to define security for the report server. All security is defined through role assignments that map specific users or groups to the tasks that they can perform. The task list is represented as a role definition that you select when making the role assignment.  
  
 At the system level, the role assignments that you create or modify apply to the report server as a whole. For example, the ability to create shared schedules is specified at the system level because shared schedules are used throughout the system.  
  
 By default, Reporting Services provides two predefined system level roles:  
  
-   System User includes tasks that allow users to view report server properties and shared schedules, and to execute report definitions, which allows users to view clickthrough reports that have been published to the report server. Most users should be assigned to this role.  
  
-   System Administrator includes tasks for creating and managing shared schedules, setting server properties, and creating system-level role assignments for other users. Few users require permissions at this level.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the New System Role Assignments or Edit System Role Assignments page  
  
1.  Open Report Manager.  
  
2.  At the top of the page, in the right-hand corner, click **Site Settings**. This opens the General properties page for the site.  
  
3.  Select the **Security** tab. You must have Content Manager and System Administrator permissions to access this page.  
  
4.  To create a new role assignment, click **New Role Assignment** in the toolbar. To edit an existing role assignment, click **Edit** next to a group or user on the Security properties page.  
  
## Options  
 **Group or User**  
 Type the name of a group or user account in your domain. If the report server is running under a local account, you must specify local groups or users. If the report server is running under a domain account, you must specify domain groups or users. Enter the account in this format: \<domain>\\<account\>.  
  
> [!NOTE]  
>  This box is available only on the New Role Assignment page.  
  
 **Roles**  
 Provides a list of system-level roles that you can assign to other users. You can specify multiple roles for a single role assignment.  
  
 Report Manager does not display the tasks in each role or provide a way to add or modify the tasks. You must use the roles as they are defined. To create, modify, or delete roles, use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. For more information, see [Create, Delete, or Modify a Role &#40;Management Studio&#41;](security/role-definitions-create-delete-or-modify.md).  
  
 Note that if you are using [!INCLUDE[ssExpressEd2005](../includes/ssexpressed2005-md.md)] with Advanced Services, you must use the default roles that are provided.  
  
 **Descriptions**  
 Shows additional information about the role. For predefined roles such as System User or System Administrator, the description summarizes the tasks that each role supports.  
  
 **Delete Role Assignment**  
 Click to delete an existing role assignment for a user or a group. You cannot delete a role assignment if it is the only one left (each item must have a minimum of one role assignment).  
  
> [!NOTE]  
>  This button is available only on the Edit Role Assignment page.  
  
## See Also  
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Role Assignments](security/role-assignments.md)   
 [Role Definitions](security/role-definitions.md)  
  
  
