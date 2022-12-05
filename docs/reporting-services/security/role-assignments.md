---
description: "Role Assignments"
title: "Role Assignments | Microsoft Docs"
ms.date: 05/07/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "users [Reporting Services]"
  - "roles [Reporting Services]"
  - "role-based security [Reporting Services], role assignments"
  - "groups [Reporting Services]"
  - "security [Reporting Services], role assignments"
ms.assetid: 600e112c-1897-48a6-93c0-6e9f3f12dc01
author: maggiesMSFT
ms.author: maggies
---

# Role Assignments

In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], *role assignments* determine access to stored items and to the report server itself. A role assignment has the following parts:  
  
- A securable item for which you want to control access. Examples of securable items include folders, reports, and resources.  
  
- A user or group account that can be authenticated by Windows security or another authentication mechanism.  
  
- Role definitions define a set of permissible tasks and include:
  - **Browser**
  - **Content Manager**
  - **My Reports**
  - **Publisher**
  - **Report Builder**
  - **System Administrator**
  - **System User**

 Role assignments are inherited within the folder hierarchy and are automatically inherited by contained:

- **Reports**
- **Shared data sources**
- **Resources**
- **Subfolders**

You can override inherited security by defining role assignments for individual items. All parts of the folder hierarchy must be secured by at least one role assignment. You can't create an unsecured item or manipulate settings in a way that produces an unsecured item.  
  
 The following diagram illustrates a role assignment that maps a group and a specific user to the **Publisher** role for Folder B.  
  
 ![Role assignments diagram](../../reporting-services/security/media/report-securityarch.gif "Role assignments diagram")  
Role assignments diagram  
  
## System-Level and Item-Level Role Assignments

 Role-based security in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is organized into the following levels:

- Item-level role assignments control access to items in the report server folder hierarchy such as:
  - reports
  - folders
  - report models
  - shared data sources
  - other resources

- Item-level role assignments are defined when create a role assignment on a specific item, or on the Home folder.

- System role assignments authorize operations that are scoped to the server as a whole. For example, the ability to manage jobs is a system level operation. A system role assignment isn't the equivalent of a system administrator. It doesn't confer advanced permissions that grant full control of a report server.

A system role assignment doesn't authorize access to items in the folder hierarchy. System and item security are mutually exclusive. Sometimes, you might need to create both a system-level and item-level role assignment to provide sufficient access for a user or group.

## Users and Groups in Role Assignments

 The users or group accounts that you specify in role assignments are domain accounts. The report server references, but doesn't create or manage, users and groups from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows domain (or another security model if you're using a custom security extension).

Of all the role assignments that apply to any given item, no two can specify the same user or group. If a user account is also a member of a group account, and you have role assignments for both, the combined set of tasks for both role assignments are available to the user.

When you add a user to a group that already has a role assignment, you must reset Internet Information Services (IIS) for the new role assignment to take effect.

## Predefined Role Assignments

 By default, predefined role assignments are implemented that allow local administrators to manage the report server. You can add additional role assignments to grant access to other users.

 For more information about the predefined role assignments that provide default security, see [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md).  

## See Also

 [Create, Delete, or Modify a Role &#40;Management Studio&#41;](../../reporting-services/security/role-definitions-create-delete-or-modify.md)

 [Modify or Delete a Role Assignment &#40; SSRS web portal &#41;](../../reporting-services/security/role-assignments-modify-or-delete.md)

 [Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)

 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
