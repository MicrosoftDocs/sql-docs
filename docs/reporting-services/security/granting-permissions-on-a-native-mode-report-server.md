---
title: "Granting Permissions on a Native Mode Report Server | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "roles [Reporting Services], creating"
  - "authorization [Reporting Services]"
  - "server security [Reporting Services]"
  - "role-based security [Reporting Services]"
  - "items [Reporting Services], security"
  - "permissions [Reporting Services], native mode"
  - "published reports [Reporting Services], security"
  - "reports [Reporting Services], security"
  - "report items [Reporting Services], security"
  - "role-based security [Reporting Services], about role-based security"
  - "security [Reporting Services], role-based"
ms.assetid: 260dc2e9-546c-4f04-9fa1-977e23c9d68c
author: maggiesMSFT
ms.author: maggies
---
# Granting Permissions on a Native Mode Report Server
  SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses role-based authorization and an authentication subsystem to determine who can perform operations and access items on a report server. Role-based authorization categorizes into roles the set of actions that a user or group can perform. Authentication is based on built-in Windows Authentication or a custom authentication module that you provide. You can use predefined or custom roles with either authentication type.  
  
## Using Roles to Grant Report Server Access  
 All users interact with a report server within the context of a role that defines a specific level of access. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes predefined roles that you can assign to users and groups to provide immediate access to a report server. **ContentManager**, **Publisher**, and **Browser** are examples of predefined roles. Each role defines a collection of related tasks. For example, a **Publisher** has permission to add reports and create folders for storing those reports.  
  
 Role assignments are typically inherited from a parent node, but you can break permission inheritance by creating a new role assignment for a particular item. A user who is a member of the **Content Manager** role for one report may be a member of the **Browser** role for another report.  
  
 To grant access to report server items and operations, follow these guidelines:  
  
1.  Review the predefined roles to determine whether you can use them as is. If you need to adjust the tasks or define additional roles, you should do this before you begin assigning users to specific roles. For more information about each role, see [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md).  
  
2.  Identify which users and groups require access to the report server, and at what level. Most users should be assigned to the **Browser** role or the **Report Builder** role. A smaller number of users should be assigned to the **Publisher** role. Very few users should be assigned to **Content Manager**.  
  
3.  Use Report Manager to assign roles on the Home folder (this is the top-level folder of the report server folder hierarchy) for each user or group who requires access.  
  
4.  At the site level, on the Site Settings page in Report Manager, create a system-level role assignment for each user and group using the predefined roles **System User** and **System Administrator**.  
  
5.  Create additional role assignments as needed for specific folders, reports, and other items. Avoid creating a large number of role assignments. If you create too many, it will be difficult to keep track of the different permission levels for each user.  
  
> [!NOTE]  
>  If you configured a report server to run in SharePoint integrated mode, you must set permissions on the SharePoint site to grant access to report server items. For more information, see [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md).  
  
## Who Sets Permissions  
 Initially, only users who are members of the local administrators group can access a report server. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is installed with two default role assignments that grant item-level and system-level access to members of the local administrators group. These built-in role assignments local Administrators to grant report server access to other users and manage report server items. The built-in role assignments cannot be deleted. A local administrator always has permission to fully manage a report server instance.  
  
 Because full permissions on a report server include item-level and system-level permissions, a local administrator is assigned to the following roles:  
  
 Additional configuration is required before you can administer a report server instance on a local computer that runs Windows Vista or Windows Server 2008. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## How Permissions are Stored  
 Role assignments and definitions are stored in the report server database. If you are using variety of client tools or programmatic interfaces, all access is subject to the permissions that are defined for the report server instance as a whole. If you are configuring multiple report servers in a scale-out-deployment, the role assignments that you define on one instance are stored in a shared database and used by all the other instances in the same scale-out deployment. Because role assignments are stored with the items they secure, you can move the database to another report server instance without losing the permissions you defined.  
  
## Tasks and tools for Managing Permissions  
 Use the following tools to manage role definitions and assignments.  
  
|Tool|Tasks|  
|----------|-----------|  
|Management Studio - Used to view, modify, create, and delete role definitions.|[Create, Delete, or Modify a Role &#40;Management Studio&#41;](../../reporting-services/security/role-definitions-create-delete-or-modify.md)|  
|Report Manager - Used to assign users and groups to roles.|[Grant User Access to a Report Server &#40;Report Manager&#41;](../../reporting-services/security/grant-user-access-to-a-report-server-report-manager.md)<br /><br /> [Modify or Delete a Role Assignment &#40;Report Manager&#41;](../../reporting-services/security/role-assignments-modify-or-delete.md)|  
  
## See Also  
 [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)   
 [Authentication with the Report Server](../../reporting-services/security/authentication-with-the-report-server.md)   
 [Create and Manage Role Assignments](../../reporting-services/security/create-and-manage-role-assignments.md)   
 [Reporting Services Security and Protection](../../reporting-services/security/reporting-services-security-and-protection.md)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)  
  
  
