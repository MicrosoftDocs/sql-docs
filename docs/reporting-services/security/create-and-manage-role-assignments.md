---
description: "Create and Manage Role Assignments"
title: "Create and Manage Role Assignments | Microsoft Docs"
ms.date: 05/07/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "removing role assignments"
  - "roles [Reporting Services], assignments"
  - "security [Reporting Services], role assignments"
  - "modifying role assignments"
  - "deleting role assignments"
ms.assetid: 086d0987-b43c-4834-8372-e08fb4b432f8
author: maggiesMSFT
ms.author: maggies
---
# Create and Manage Role Assignments

A *role assignment* is a security policy that determines a user's or group's permissions. Permissions decide whether the user or group can access or modify a specific report server item, or do a task. A role assignment consists of a single user or group account name and one or more role definitions.

Role assignments are scoped to the *item level* or *system level*.

- An item-level role assignment is created for a specific item or branch of the folder hierarchy on the report server. You navigate to a specific folder or item to create a role assignment for it.

- System-level role assignments give selected users the capability to do tasks that affect the report server site as a whole. These tasks include:
  - Creating shared schedules
  - Managing jobs
  - Processing reports
  - Setting properties

System-level security doesn't convey access to items in the report server folder hierarchy.

## Creating an Item-level Role Assignment

From here, you can create a separate role assignment for each user or group account that requires access to the report server. If the account is on a domain other than the one that contains the report server, include the domain name. After you specify an account, you choose one or more role definitions. The role definitions are additive. The combined set of all tasks from all definitions is supported in the assignment for a particular user or group.

To enable widespread access, you choose an item that is high in the folder hierarchy (for example, the root folder Home). Later, you can create role assignments to lock down specific areas of the folder hierarchy.

You must be a member of the local Administrator's group on the report server computer to create a role assignment. You can delegate that responsibility by assigning other users to the **Content Manager** role.

To create or manage role assignments, or for more information, see [Grant User Access to a Report Server](../../reporting-services/security/grant-user-access-to-a-report-server.md)
  
## Creating a System-level Role Assignment

System-level and item-level role assignments go together. You create a system-level role assignment for each user or group, that has an item-level role assignment.

System-level role assignments include a wide range of permissions, but they don't include permissions that are part of an item-level role assignment.

In contrast with system permissions on a computer, system roles in reporting servers don't convey overarching permissions that include all possible tasks. Instead, system-level role assignments are simply a set of tasks that are scoped to the report server site. System role assignments determine whether users can view application properties (such as the image or title of the Home page), view or manage shared schedules, or use Report Builder.

To create or manage a system-level role assignment or for more information, see [Grant User Access to a Report Server](../../reporting-services/security/grant-user-access-to-a-report-server.md) and [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md).  

## Modifying a Role Assignment

You can modify a role assignment at any time. Your changes take effect when you save the role assignment. User sessions are not affected by role assignment changes. If a user has a report open, and you modify a role assignment to deny access, the user can continue using the report for that active session.

If you add a user account to a group that is already part of a role assignment, there will be a delay before the user account is able to access items from the change. This delay is caused by Internet Information Services (IIS) caching of authentication tokens. You can either wait for the tokens to refresh (typically 15 minutes), or you can reset IIS to update the cache immediately.

You can only modify one role assignment at a time. You can't perform a global search-and-replace operation to change role definition names, role assignment settings, or to find all the role assignments that include a specific user or group.

## Deleting a Role Assignment

You can delete role assignments by selecting the checkbox by each assignment you want to delete, and then clicking **Delete**. You can also delete role assignments by clicking **Revert to Parent Security**. When you select this button, the existing role assignments for the item are deleted, and replaced with the assignments inherited from the parent item.

## See Also

[Grant User Access to a Report Server](../../reporting-services/security/grant-user-access-to-a-report-server.md)  
[Role Assignments](../../reporting-services/security/role-assignments.md)  
[Role Definitions](../../reporting-services/security/role-definitions.md)  
[Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md)  
[Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)
