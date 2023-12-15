---
title: "Create and manage role assignments"
description: "Create and manage role assignments"
author: maggiesMSFT
ms.author: maggies
ms.date: 05/07/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "removing role assignments"
  - "roles [Reporting Services], assignments"
  - "security [Reporting Services], role assignments"
  - "modifying role assignments"
  - "deleting role assignments"
---
# Create and manage role assignments

A *role assignment* is a security policy that determines a user's or group's permissions. Permissions decide whether the user or group can access or modify a specific report server item, or do a task. A role assignment consists of a single user or group account name and one or more role definitions.

Role assignments are scoped to the *item level* or *system level*.

- An item-level role assignment is created for a specific item or branch of the folder hierarchy on the report server. You navigate to a specific folder or item to create a role assignment for it.

- System-level role assignments give selected users the capability to do tasks that affect the report server site as a whole. These tasks include:
  - Creating shared schedules
  - Managing jobs
  - Processing reports
  - Setting properties

System-level security doesn't convey access to items in the report server folder hierarchy.

## Create an item-level role assignment

From here, you can create a separate role assignment for each user or group account that requires access to the report server. If the account is on a domain other than the one that contains the report server, include the domain name. After you specify an account, you choose one or more role definitions. The role definitions are additive. The combined set of all tasks from all definitions is supported in the assignment for a particular user or group.

To enable widespread access, you choose an item that is high in the folder hierarchy (for example, the root folder Home). Later, you can create role assignments to lock down specific areas of the folder hierarchy.

You must be a member of the local Administrator's group on the report server computer to create a role assignment. You can delegate that responsibility by assigning other users to the **Content Manager** role.

To create or manage role assignments, or for more information, see [Grant user access to a report server](../../reporting-services/security/grant-user-access-to-a-report-server.md)
  
## Create a system-level role assignment

System-level and item-level role assignments go together. You create a system-level role assignment for each user or group that has an item-level role assignment.

System-level role assignments include a wide range of permissions, but they don't include permissions that are part of an item-level role assignment.

In contrast with system permissions on a computer, system roles in reporting servers don't convey overarching permissions that include all possible tasks. Instead, system-level role assignments are simply a set of tasks that are scoped to the report server site. System role assignments determine whether users can view application properties, such as the image or title of the Home page. They also determine whether users can view or manage shared schedules or use Report Builder.

To create or manage a system-level role assignment or for more information, see [Grant user access to a report server](../../reporting-services/security/grant-user-access-to-a-report-server.md) and [Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md).  

## Modify a role assignment

You can modify a role assignment at any time. Your changes take effect when you save the role assignment. Role assignment changes don't affect user sessions. If a user has a report open, and you modify a role assignment to deny access, the user can continue to use the report for that active session.

If you add a user account to a group that is already part of a role assignment, there's a delay before the user account is able to access items from the change. The Internet Information Services (IIS) causes the delay by caching authentication tokens. You can either wait for the tokens to refresh (typically 15 minutes), or you can reset IIS to update the cache immediately.

You can only modify one role assignment at a time. You can't perform a global search-and-replace operation to change role definition names, role assignment settings, or to find all the role assignments that include a specific user or group.

## Delete a role assignment

You can delete role assignments by selecting the checkbox by each assignment you want to delete, and then clicking **Delete**. You can also delete role assignments by clicking **Revert to Parent Security**. When you select this button, the existing role assignments for the item are deleted, and replaced with the assignments inherited from the parent item.

## Related content

[Grant user access to a report server](../../reporting-services/security/grant-user-access-to-a-report-server.md)  
[Role assignments](../../reporting-services/security/role-assignments.md)  
[Role definitions](../../reporting-services/security/role-definitions.md)  
[Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md)  
[Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)
