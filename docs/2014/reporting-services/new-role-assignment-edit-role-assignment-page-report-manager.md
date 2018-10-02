---
title: "New Role Assignment: Edit Role Assignment Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 3319ced0-4b86-42af-b18d-da41a625113c
author: markingmyname
ms.author: maghan
manager: craigg
---
# New Role Assignment: Edit Role Assignment Page (Report Manager)
  Use the New Role Assignment or Edit Role Assignment page to grant permissions to report server items and operations. Each user who requires access to a report server must have a role assignment that defines the level of access. You can create role assignments at the root node, or on a specific report, model, folder, resource, or shared data source. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] security is enforced through role assignments that you apply to items. A role assignment matches a group or user to a role definition, where each role definition identifies the tasks that groups or users can perform with regards to a specific item.  
  
 Item-level role assignments can have a broad impact. Although they can be associated with a single report or folder, they can also be defined at a high level in the folder hierarchy and be inherited by folders and items that are lower in the tree. For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](security/grant-user-access-to-a-report-server.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the New Role Assignment or Edit Role Assignment page  
  
1.  Open Report Manager, and locate an item for which you want to add a new role assignment or edit a role assignment.  
  
2.  Hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Security**. This opens the Security properties page for the item.  
  
4.  If you want to add a new role assignment, in the toolbar, click **New Role Assignment**. If you want to edit a role assignment, click **Edit** next to the group or user name you want to edit.  
  
    > [!NOTE]  
    >  If an item currently inherits security from a parent item, click **Edit Item Security** in the toolbar to change the security settings.  
  
## Options  
 **Group or User Name**  
 Type the name of a group or user account for which the role assignment is being created. The group or user name must be a valid Windows domain account. Enter the account in this format: \<domain>\\<account\>.  
  
> [!NOTE]  
>  This box is available only on the New Role Assignment page.  
  
 **Role**  
 Shows all roles defined on the report server that can be used to define security for items. When you create or change a role assignment for a report or folder, select one or more roles until the combined set of tasks describe the actions that the user should be allowed to perform. To view the set of tasks that each role supports, use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. You cannot view, create, modify, or delete roles in Report Manager. For instructions, see [Create, Delete, or Modify a Role &#40;Management Studio&#41;](security/role-definitions-create-delete-or-modify.md).  
  
 **Description**  
 Shows additional information about the role. For predefined roles such as **Browser** or **Content Manager**, the description summarizes the tasks that each role supports.  
  
 **Delete Role Assignment**  
 Click to delete an existing role assignment for a user or a group. You cannot delete a role assignment if it is the only one left (each item must have a minimum of one role assignment).  
  
> [!NOTE]  
>  This button is available only on the Edit Role Assignment page.  
  
## See Also  
 [Create, Delete, or Modify a Role &#40;Management Studio&#41;](security/role-definitions-create-delete-or-modify.md)   
 [Granting Permissions on a Native Mode Report Server](security/granting-permissions-on-a-native-mode-report-server.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Role Assignments](security/role-assignments.md)   
 [Grant User Access to a Report Server &#40;Report Manager&#41;](security/grant-user-access-to-a-report-server.md)  
  
  
