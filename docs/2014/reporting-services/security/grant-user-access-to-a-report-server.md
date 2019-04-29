---
title: "Grant User Access to a Report Server (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "removing role assignments"
  - "permissions [Reporting Services], granting report server access"
  - "roles [Reporting Services], assignments"
  - "modifying role assignments"
  - "deleting role assignments"
ms.assetid: 2144c020-3253-4b47-8cda-e14c928bb471
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Grant User Access to a Report Server (Report Manager)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses role-based security to grant user access to a report server. On a new report server installation, only users who are members of the local Administrators group have permissions to report server content and operations. To make the report server available to other users, you must create role assignments that map  user or group accounts to a predefined role that specifies a collection of tasks.  
  
 **SharePoint mode report servers:** For a report server that is configured for SharePoint integrated mode, you configure access from a SharePoint site using SharePoint permissions. Permission levels on the SharePoint site determine access to report server content and operations. You must be a site administrator to grant permissions on a SharePoint site. For more information, see [Granting Permissions on Report Server Items on a SharePoint Site](granting-permissions-on-report-server-items-on-a-sharepoint-site.md).  
  
 **Native mode report servers:** This topic is focused on a report server that is configured for native mode and the use of Report Manager to assign users to a role. There are two types of roles:  
  
-   Item-level roles are used to view, add, and manage report server content, subscriptions, report processing, and report history. Item-level role assignments are defined on the root node (the Home folder) or on specific folders or items farther down the hierarchy.  
  
-   System-level roles grant access to site-wide operations that are not bound to any specific item. Examples include using Report Builder and using shared schedules.  
  
     The two types of roles complement each other and should be used together. For this reason, adding a user to a report server is a two-part operation. If you assign a user to an item-level role, you should also assign them to a system-level role. When assigning a user to a role, you must select a role that is already defined. To create, modify, or delete roles, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For more information, see [Create, Delete, or Modify a Role &#40;Management Studio&#41;](role-definitions-create-delete-or-modify.md).  
  
## Before you start  
 Review the following list before adding users to a native mode report server.  
  
-   You must be a member of the local Administrators group on the report server computer. If you are deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on [!INCLUDE[wiprlhlong](../../includes/wiprlhlong-md.md)] or Windows Server 2008, additional configuration is required before you can administer a report server locally. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
-   To delegate this task to other users, create role assignments that map user accounts to Content Manager and System Administrator roles. Users who have Content Manager and System Administrator permissions can add users to a report server.  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], view the predefined roles for System Roles and User Roles so that you are familiar with the kinds of tasks in each role. Task descriptions are not visible in Report Manager, so you will want to be familiar with the roles before you begin adding users.  
  
-   Optionally, customize the roles or define additional roles to include the collection of tasks that you require. For example, if you plan to use custom security settings for individual items, you might want to create a new role definition that grants view-access to folders.  
  
### To add a user or group to a system role  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  Click **Site Settings**.  
  
3.  Click **Security**.  
  
4.  Click **New Role Assignment**.  
  
5.  In **Group or user name**, enter a Windows domain user or group account in this format: \<domain>\\<account\>. If you are using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.  
  
6.  Select a system role, and then click **OK**.  
  
     Roles are cumulative, so if you select both System Administrator and System User, a user or group will be able to perform the tasks in both roles.  
  
7.  Repeat to create assignments for additional users or groups.  
  
### To add a user or group to an item role  
  
1.  Start **Report Manager** and locate the report item for which you want to add a user or group.  
  
2.  Hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Security**.  
  
4.  Click **New Role Assignment**.  
  
    > [!NOTE]  
    >  If an item currently inherits security from a parent item, click **Edit Item Security** in the toolbar to change the security settings. Then click **New Role Assignment**.  
  
5.  In **Group or user name**, enter a Windows domain user or group account in this format: \<domain>\\<account\>. If you are using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.  
  
6.  Select one or more role definitions that describe how the user or group should access the item, and then click **OK**.  
  
7.  Repeat to create assignments for additional users or groups.  
  
## See Also  
 (create-and-manage-role-assignments.md)   
 [New Role Assignment: Edit Role Assignment Page &#40;Report Manager&#41;](../new-role-assignment-edit-role-assignment-page-report-manager.md)   
 [Security Properties Page, Items &#40;Report Manager&#41;](../security-properties-page-items-report-manager.md)   
 [Role Assignments](role-assignments.md)   
 [Role Definitions](role-definitions.md)  
  
  
