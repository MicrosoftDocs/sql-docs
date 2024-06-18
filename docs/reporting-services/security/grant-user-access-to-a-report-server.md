---
title: "Grant user access to a report server"
description: "Grant user access to a report server"
author: maggiesMSFT
ms.author: maggies
ms.date: 06/18/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "removing role assignments"
  - "permissions [Reporting Services], granting report server access"
  - "roles [Reporting Services], assignments"
  - "modifying role assignments"
  - "deleting role assignments"
---
# Grant user access to a report server

[!INCLUDE[ssrs-appliesto-sql2016-preview](../../includes/ssrs-appliesto-sql2016-preview.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses role-based security to grant user access to a report server. On a new report server installation, only users who are members of the local Administrators group have permissions to report server content and operations. To make the report server available to other users, you must create role assignments that map user or group accounts to a predefined role that specifies a collection of tasks.

**SharePoint mode report servers:** For a report server that is configured for SharePoint integrated mode, you configure access from a SharePoint site by using SharePoint permissions. Permission levels on the SharePoint site determine access to report server content and operations. You must be a site administrator to grant permissions on a SharePoint site. For more information, see [Grant permissions on report server items on a SharePoint site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md).

**Native mode report servers:** This article is focused on a report server that is configured for native mode and the use of the web portal to assign users to a role. There are two types of roles:

- Item-level roles are used to view, add, and manage report server content, subscriptions, report processing, and report history. Item-level role assignments are defined on the root node (the Home folder) or on specific folders or items farther down the hierarchy.

- System-level roles grant access to site-wide operations that aren't bound to any specific item. Examples include how to use Report Builder and shared schedules.

The two types of roles complement each other and should be used together. For this reason, adding a user to a report server is a two-part operation. If you assign a user to an item-level role, you should also assign them to a system-level role. When assigning a user to a role, you must select a role that is already defined. To create, modify, or delete roles, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For more information, see [Create, Delete, or Modify a Role (Management Studio)](../../reporting-services/security/role-definitions-create-delete-or-modify.md).

## Before you start

Review the following list before adding users to a native mode report server.

- You must be a member of the local Administrators group on the report server computer. If you're deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on [!INCLUDE[winvista](../../includes/winvista-md.md)] or Windows Server 2008, more configuration is required before you can administer a report server locally. For more information, see [Configure a native mode report server for local administration](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

- To delegate this task to other users, create role assignments that map user accounts to Content Manager and System Administrator roles. Users who have Content Manager and System Administrator permissions can add users to a report server.

- In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], view the predefined roles for System Roles and User Roles so that you're familiar with the kinds of tasks in each role. Task descriptions aren't visible in the web portal, so you should be familiar with the roles before you begin adding users.

- Optionally, customize the roles or define other roles to include the collection of tasks that you require. For example, if you plan to use custom security settings for individual items, you might want to create a new role definition that grants view-access to folders.

## Add a user or group to a system role

1. Start the [web portal](../web-portal-ssrs-native-mode.md).

1. Select the **Gear** icon in the upper right and then select **Site Settings** from the dropdown menu.

    :::image type="content" source="../../reporting-services/security/media/settings-icon-and-menu.png" alt-text="Screenshot that shows the report server web portal gear icon and dropdown menu.":::

1. Select **Security**.

1. Select **Add group or user**.

1. In **Group or user**, enter a Windows domain user or group account in this format: \<domain>\\<account\>.

    > [!NOTE]
    > If you are using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.

1. Choose a system role, and then select **OK**.

    Roles are cumulative, so if you select both System Administrator and System User, a user or group is able to perform the tasks in both roles.

1. Repeat to create assignments for more users or groups.

## Add a user or group to an item role

1. Start the **web portal** and locate the report item for which you want to add a user or group.

1. Select the **...** (ellipse) on an item.

1. In the menu, select **Manage**.

1. Select **Security**.

1. Select **Add group or user**.

    > [!NOTE]
    > If an item currently inherits security from a parent item, select **Customize security** in the toolbar to change the security settings. Then select **Add group or user**.

1. In **Group or user**, enter a Windows domain user or group account in this format: \<domain>\\<account\>. If you're using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.

1. Select one or more role definitions that describe how the user or group should access the item, and then select **OK**.

1. Repeat to create assignments for more users or groups.

## Related content

- [Create and manage role assignments](../../reporting-services/security/create-and-manage-role-assignments.md)
- [Role assignments](../../reporting-services/security/role-assignments.md)
- [Role definitions](../../reporting-services/security/role-definitions.md)

More questions? Try asking the [Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
