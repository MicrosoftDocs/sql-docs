---
title: "Security Properties Page, Items (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 351b8503-354f-4b1b-a7ac-f1245d978da0
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Security Properties Page, Items (Report Manager)
  Use the Security properties page to view or modify the security settings that determine access to folders, reports, models, resources, and shared data sources. This page is available for items that you have permission to secure.  
  
 Access to items is defined through role assignments that specify the tasks that a group or user can perform. A role assignment consists of one user or group name and one or more role definitions that specify a collection of tasks.  
  
 Security settings are inherited from the root folder down to subfolders and items within those folders. Unless you explicitly break inherited security, subfolders and items inherit the security context of a parent item. If you redefine a security policy for a folder in the middle of the hierarchy, all its child items, including subfolders, assume the new security settings.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Security page for an item  
  
1.  Open Report Manager, and locate the item for which you want to configure security settings.  
  
2.  Hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, perform one of the following steps:  
  
    -   Click **Security**. This opens the Security properties page for the item.  
  
    -   Click **Manage** to open the General properties page for the item. Then select the **Security** tab.  
  
 **Edit Item Security**  
 Click to change how security is defined for the current item. If you are editing security for a folder, your changes apply to the contents of the current folder and any subfolders.  
  
 This button is not available for the Home folder.  
  
 The following buttons become available when you edit item security.  
  
 **Delete**  
 Select the check box next to the group or user name that you want to delete and click **Delete**. You cannot delete a role assignment if it is the only one remaining, or if it is a built-in role assignment (for example, "Built-in\Administrators") that defines the security baseline for the report server. Deleting a role assignment does not delete a group or user account or role definitions.  
  
 **New Role Assignment**  
 Click to open the New Role Assignment page, which is used to create additional role assignments for the current item. For more information, see [New Role Assignment: Edit Role Assignment Page &#40;Report Manager&#41;](../../2014/reporting-services/new-role-assignment-edit-role-assignment-page-report-manager.md).  
  
 **Revert to Parent Security**  
 Click to reset the security settings to that of the immediate parent folder. If inheritance is unbroken throughout the report server folder hierarchy, the security settings of the top-level folder, Home, are used.  
  
 **Group or User**  
 Lists the groups and users that are part of an existing role assignment for the current item. Existing role assignments for the current folder are defined for the groups and users that appear in this column. You can click a group or user name to view or edit role assignment details.  
  
 **Roles**  
 Lists one or more role definitions that are part of an existing role assignment. If multiple roles are assigned to a group or user account, that group or user can perform all tasks that belong to those roles. To view the tasks that are associated with a role, use SQL Server Management Studio to view the tasks in each role definition.  
  
## See Also  
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Predefined Roles](security/role-definitions-predefined-roles.md)   
 [Granting Permissions on a Native Mode Report Server](security/granting-permissions-on-a-native-mode-report-server.md)   
 [Role Assignments](security/role-assignments.md)   
 [Role Definitions](security/role-definitions.md)  
  
  
