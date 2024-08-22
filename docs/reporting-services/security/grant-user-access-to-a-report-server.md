---
title: Grant users access to a report server
description: See how to use roles and groups to grant members of your organization access to your report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/22/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "removing role assignments"
  - "permissions [Reporting Services], granting report server access"
  - "roles [Reporting Services], assignments"
  - "modifying role assignments"
  - "deleting role assignments"

#customer intent: As a SQL server administrator, I want to use role-based security so that I can control who has access to my report server.
---
# Grant users access to a report server

[!INCLUDE[SSRS applies to 2016 and later](../../includes/ssrs-appliesto-2016-and-later.md)]

SQL Server Reporting Services (SSRS) uses role-based security to grant users access to a report server. On a new report server installation, only users who are members of the local Administrators group have permissions to report server content and operations. To make the report server available to other users, you must create role assignments that map user or group accounts to a predefined role that specifies a collection of tasks.

To delegate this task to other users, create role assignments that map user accounts to Content Manager and System Administrator roles. Users who have Content Manager and System Administrator permissions can add users to a report server. For more information, see [Predefined roles in Reporting Services](/sql/reporting-services/security/role-definitions-predefined-roles).

## Prerequisites

- A configured native mode report server. For more information, see [Configure a native mode report server for local administration](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).
- Membership in the local **Administrators** group on the report server computer.
- Optionally, roles that you've customized or defined for tasks. For more information, see [Predefined roles in Reporting Services](/sql/reporting-services/security/role-definitions-predefined-roles).

## Understand role types

This article focuses on using the web portal to assign users to a role. This information applies to a report server that's configured for native mode.

There are two types of roles:

- Item-level roles are used to view, add, and manage report server content, subscriptions, report processing, and report history. You define item-level role assignments on the root node (the Home folder) or on specific folders or items farther down the hierarchy.

- System-level roles grant access to site-wide operations that aren't bound to any specific item. Examples include using Report Builder and shared schedules.

> [!NOTE]
> If you have a report server that's configured for SharePoint integrated mode, you configure access from a SharePoint site by using SharePoint permissions. Permission levels on the SharePoint site determine access to report server content and operations. You must be a site administrator to grant permissions on a SharePoint site. For more information, see [Grant permissions on report server items on a SharePoint site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md).

The two types of roles complement each other and should be used together. For this reason, adding a user to a report server is a two-part operation. If you assign a user to an item-level role, you should also assign them to a system-level role.

When you assign a user to a role, you must select a role that's already defined. To create, modify, or delete roles, use [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] [!INCLUDE[Management Studio](../../includes/ssmanstudio-md.md)]. For more information, see [Create, Delete, or Modify a Role (Management Studio)](../../reporting-services/security/role-definitions-create-delete-or-modify.md).

## Add a user or group to a system role

1. Go to the [report server web portal](../web-portal-ssrs-native-mode.md).

1. In the upper-right corner, select the **Gear** icon, and then select **Site settings**.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/settings-icon-menu.png" alt-text="Screenshot that shows the report server web portal gear icon with its menu open. In its menu, Site settings is highlighted.":::

1. Select **Security**.

1. Select **Add group or user**.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/site-add-group-user.png" alt-text="Screenshot that shows the report server web portal Security page. Add group or user is highlighted.":::

1. For **Group or user**, enter a Windows domain user or group account in the following format: \<domain>\\<account\>.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/site-name-group-user.png" alt-text="Screenshot that shows the Add group or user section of the report server web portal Security page. The Add group or user field is highlighted.":::

   > [!NOTE]
   > If you're using forms authentication or custom security, specify the user or group account in the format that's correct for your deployment.

1. Select a system role, and then select **OK**.

   Roles are cumulative, so if you select **System Administrator** and **System User**, the user or group is able to perform the tasks in both roles.

1. Repeat these steps to create assignments for more users or groups.

## Add a user or group to an item role

1. Go to the report server web portal and locate the report item for which you want to add a user or group.

1. On the report item, select the ellipsis to open the **More info** menu.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/report-more-info.png" alt-text="Screenshot of the web portal that shows a report item with its ellipsis highlighted and More info displayed in the ellipsis tooltip.":::

1. In the menu, select **Manage**.

1. Select **Security**.

1. If the report item currently inherits security settings from a parent item, take the following actions. Otherwise, go to the next step.

   1. On the toolbar, select **Customize security**.
   1. Confirm that you want to change the security settings.

1. Select **Add group or user**.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/report-add-group-user.png" alt-text="Screenshot of the report server web portal that shows the Security page of a report item. Add group or user is highlighted.":::

1. For **Group or user**, enter a Windows domain user or group account in the following format: \<domain>\\<account\>. If you're using forms authentication or custom security, specify the user or group account in the format that's correct for your deployment.

   :::image type="content" source="../../reporting-services/security/media/grant-user-access-to-a-report-server/report-name-group-user.png" alt-text="Screenshot of the report server web portal that shows the New role page of a report item. The Group or user field is highlighted.":::

1. Select one or more role definitions that describe how the user or group should access the item, and then select **OK**.

1. Repeat these steps to create assignments for more users or groups.

## Related content

- [Create and manage role assignments](../../reporting-services/security/create-and-manage-role-assignments.md)  
- [Role assignments](../../reporting-services/security/role-assignments.md)  
- [Role definitions](../../reporting-services/security/role-definitions.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
