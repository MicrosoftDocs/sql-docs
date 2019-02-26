---
title: "Create and Manage Role Assignments | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "removing role assignments"
  - "roles [Reporting Services], assignments"
  - "security [Reporting Services], role assignments"
  - "modifying role assignments"
  - "deleting role assignments"
ms.assetid: 086d0987-b43c-4834-8372-e08fb4b432f8
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create and Manage Role Assignments
  A *role assignment* is a security policy that determines whether a user or group can access a specific report server item or perform an operation. A role assignment consists of a single user or group account name and one or more role definitions.  
  
 Role assignments are scoped to the *item level* or *system level*.  
  
-   An item-level role assignment is always created in the context of a specific item or branch in the report server folder hierarchy. You navigate to a specific folder or item to create a role assignment for it.  
  
-   System-level role assignments give selected users the capability to perform tasks that affect the report server site as a whole. These tasks include creating shared schedules, managing jobs, processing reports in Report Builder, and setting properties. System-level security does not convey access to items in the report server folder hierarchy.  
  
## Creating an Item-level Role Assignment  
 To create or manage role assignments, use Report Manager and open the Security property pages of the item that you want to secure.  
  
 You must create a separate role assignment for each user or group account that requires access to the report server. If the account is on a domain other than the one that contains the report server, include the domain name. After you specify an account, you can choose one or more role definitions. The role definitions are additive. The combined set of all tasks from all definitions are supported in the assignment for a particular user or group.  
  
 To enable widespread access, you should choose an item that is high in the folder hierarchy (for example, the root node Home). You can then create subsequent role assignments to lock down specific areas of the folder hierarchy.  
  
 You must be a member of the local Administrator's group on the report server computer to create a role assignment. You can delegate that responsibility by assigning other users to the **Content Manager** role.  
  
 For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](grant-user-access-to-a-report-server.md).  
  
## Creating a System-level Role Assignment  
 To create or manage a system-level role assignment, use Report Manager and open the Site Settings page.  
  
 System-level and item-level role assignments go together. You should create a system-level role assignment for each user or group that has an item-level role assignment.  
  
 System-level role assignments include a wide range of permissions, but they do not include permissions that are part of an item-level role assignment. In contrast with system permissions on a computer, system roles in Reporting Servers do not convey overarching permissions that include the full set of all possible operations. Instead, system-level role assignments are simply a set of tasks that are scoped to the report server site. Permissions that are conveyed through system role assignments determine whether users can view application properties (such as the image or title of the Home page), view or manage shared schedules, or use Report Builder.  
  
 For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](grant-user-access-to-a-report-server.md) and [Predefined Roles](role-definitions-predefined-roles.md).  
  
## Modifying a Role Assignment  
 You can modify a role assignment at any time. Your changes take effect when you save the role assignment. User sessions are not affected by role assignment changes. If a user has a report open, and you modify a role assignment to deny access, the user can continue using the report as long as the session is active.  
  
 If you add a user account to a group that is already part of a role assignment, there will be a delay before the user account is able to access items through the group account policies. This delay is caused by Internet Information Services (IIS) caching of authentication tokens. You can either wait for the tokens to refresh (typically, the wait period is fifteen minutes) or you can reset IIS to update the cache immediately.  
  
 You can only modify one role assignment at a time. You cannot perform a global search-and-replace operation to change role definition names or role assignment settings, or to find all the role assignments that include a specific user or group.  
  
## Deleting a Role Assignment  
 You can delete role assignments by selecting the checkbox by each assignment you want to delete, and then clicking **Delete**. You can also delete role assignments by clicking **Revert to Parent Security**. When you click this button, the existing role assignments for the item are deleted, and those that are provided through a parent item are used instead.  
  
## See Also  
 [Grant User Access to a Report Server &#40;Report Manager&#41;](grant-user-access-to-a-report-server.md)   
 [Modify or Delete a Role Assignment &#40;Report Manager&#41;](role-assignments-modify-or-delete.md)   
 [Role Assignments](role-assignments.md)   
 [Role Definitions](role-definitions.md)   
 [Predefined Roles](role-definitions-predefined-roles.md)   
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)  
  
  
