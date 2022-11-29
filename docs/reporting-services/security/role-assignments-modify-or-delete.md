---
description: "Modify or Delete a Role Assignment (SSRS web portal)"
title: "Modify or Delete a Role Assignment (SSRS web portal) | Microsoft Docs"
ms.date: 05/07/2019
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "removing role assignments"
  - "roles [Reporting Services], assignments"
  - "system roles [Reporting Services]"
  - "modifying role assignments"
  - "deleting role assignments"
ms.assetid: 523bdd32-92cb-4b48-a3a9-d58b2385bde7
author: maggiesMSFT
ms.author: maggies
---

# Role Assignments - Modify or Delete

A role assignment maps a group or user account to a predefined role that defines the tasks that can be done. It determines the types of tasks that a user does to a folder, report, model, or other content type. To create, modify, or delete role assignments, you use the SSRS web portal. After you create a role assignment for a particular user or group, you can modify it later by selecting a different role. If you want to revoke permissions to a report server, you can delete a role assignment from the report server.  

Depending on your objective, alternative approaches might be more appropriate. Examples include customizing or creating a new role definition, or modifying the membership of a group account in Active Directory.  

For example, suppose you have a group of users who need to manage their content, but shouldn't have the full set of permissions associated with Content Manager. You could create a new role definition called Department Content Manager. It could include all of the tasks in Content Manager, except **Set security policies for items**.

Similarly, if you are a system or network administrator, it is probably easier for you to manage Active Directory group accounts than role assignments in the web portal. You can reduce the overhead of managing role assignments by creating a single role assignment for a group account. Then you can modify the group membership when users no longer require access to reports.
  
 If you determine that modifying or deleting a role assignment is the best approach, remember to check for both system role and item role assignments. Each type of role assignment is configured through different pages in the web portal.
  
## To modify or delete a system role assignment
  
1. Access [the web portal of a report server &#40;SSRS Native Mode&#41;](../../reporting-services/web-portal-ssrs-native-mode.md).

2. Select **Site Settings** > **Security**. All system-level role assignments currently defined for the server or scale-out deployment are listed by account name.

3. Find the role assignment that you want to modify or delete.

4. To add or remove the role for a particular user or group, select **Edit**.

5. To delete a role assignment, select the check box next to the user or group name, then select **Delete**.

### To modify or delete an item role assignment

1. Access the web portal and locate the item for which you want to edit or delete a role assignment.

2. Hover over the item, and select the drop-down arrow.

3. In the drop-down menu, select **Security**.

4. Find the role assignment that you want to modify or delete.

5. To add or remove the role for a particular user or group, select **Edit**.

6. To delete a role assignment, select the check box next to the user or group name, then select **Delete**.

## See also

[Create and Manage Role Assignments](../../reporting-services/security/create-and-manage-role-assignments.md)  
[Role Assignments](../../reporting-services/security/role-assignments.md)  
