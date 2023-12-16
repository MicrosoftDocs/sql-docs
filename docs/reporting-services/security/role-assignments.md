---
title: "Role assignments"
description: "Role assignments"
author: maggiesMSFT
ms.author: maggies
ms.date: 05/07/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "users [Reporting Services]"
  - "roles [Reporting Services]"
  - "role-based security [Reporting Services], role assignments"
  - "groups [Reporting Services]"
  - "security [Reporting Services], role assignments"
---

# Role assignments

In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], *role assignments* determine access to stored items and to the report server itself. A role assignment has the following parts:  
  
- A securable item for which you want to control access. Examples of securable items include folders, reports, and resources.  
  
- A user or group account that Windows security or another authentication mechanism can authenticate.  
  
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

You can override inherited security by defining role assignments for individual items. At least one role assignment must secure all parts of the folder hierarchy. You can't create an unsecured item or manipulate settings in a way that produces an unsecured item.  
  
 The following diagram illustrates a role assignment that maps a group and a specific user to the **Publisher** role for Folder B.  

 :::image type="content" source="../../reporting-services/security/media/report-securityarch.gif" alt-text="Diagram that shows the role assignments.":::  
  
## System-level and item-level role assignments

 Role-based security in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is organized into the following levels:

- Item-level role assignments control access to items in the report server folder hierarchy such as:
  - reports
  - folders
  - report models
  - shared data sources
  - other resources

- Item-level role assignments are defined when you create a role assignment on a specific item, or on the Home folder.

- System role assignments authorize operations that are scoped to the server as a whole. For example, the ability to manage jobs is a system level operation. A system role assignment isn't the equivalent of a system administrator. It doesn't confer advanced permissions that grant full control of a report server.

A system role assignment doesn't authorize access to items in the folder hierarchy. System and item security are mutually exclusive. Sometimes, you might need to create both a system-level and item-level role assignment to provide sufficient access for a user or group.

## Users and groups in role assignments

 The users or group accounts that you specify in role assignments are domain accounts. The report server references, but doesn't create or manage, users and groups from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows domain (or another security model if you're using a custom security extension).

Of all the role assignments that apply to any given item, no two can specify the same user or group. If a user account is also a member of a group account, and you have role assignments for both, the combined set of tasks for both role assignments are available to the user.

When you add a user to a group that already has a role assignment, you must reset Internet Information Services (IIS) for the new role assignment to take effect.

## Predefined role assignments

 By default, predefined role assignments are implemented that allow local administrators to manage the report server. You can add other role assignments to grant access to other users.

 For more information about the predefined role assignments that provide default security, see [Predefined roles](../../reporting-services/security/role-definitions-predefined-roles.md).  

## Related content

 [Create, delete, or modify a role &#40;Management Studio&#41;](../../reporting-services/security/role-definitions-create-delete-or-modify.md)    
 [Modify or delete a role assignment &#40; SSRS web portal &#41;](../../reporting-services/security/role-assignments-modify-or-delete.md)    
 [Set permissions for report server items on a SharePoint site &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)    
 [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)    
