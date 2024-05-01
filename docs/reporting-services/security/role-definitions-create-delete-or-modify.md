---
title: "Create, delete, or modify a role (Management Studio)"
description: "Create, delete, or modify a role (Management Studio)"
author: maggiesMSFT
ms.author: maggies
ms.date: 05/07/2019
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "roles [Reporting Services], creating"
  - "deleting roles"
  - "removing roles"
  - "roles [Reporting Services], definitions"
  - "modifying roles"
  - "roles [Reporting Services], deleting"
  - "roles [Reporting Services], modifying"
---
# Create, delete, or modify a role (Management Studio)

Reporting Services provides predefined roles that define levels of access to the report server. Each user or group who requires access to the report server, is assigned a role that defines the allowed tasks. Roles are defined for the report server as a whole. You must be consistent in how a role is defined and used throughout all areas of the report server.

To create, modify, or delete roles, you can use [!INCLUDE[SQL Server Management Studio](../../includes/ssmanstudiofull-md.md)]. You can only delete roles that aren't in use.

 To assign users and groups to the roles that you create, use the SSRS web portal. For more information, see [Grant User Access to a Report Server](../../reporting-services/security/grant-user-access-to-a-report-server.md).

> [!NOTE]  
>If the report server is configured for SharePoint integrated mode, and you connected to the SharePoint site that the report server is integrated with, you can view and modify the permission levels that control access to report server content and operations.

## Create a role definition

1. In Object Explorer, expand a report server node.

2. Expand the Security folder.

3. If you're creating an item-level role definition, right-click **Roles** > **New Role**.

    Or, if you're creating a system-level role definition, right-click **System Roles** > **New System Role**.

4. Enter a unique name for the role. A name must contain at least one character. It can also include spaces and certain symbols, but not the following characters `[; : \ / @ & = + , $ / * < > | "]`.

5. Optionally enter a description. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], this description is visible only on this page. Users who view this item through the web portal can see this description in that tool.

6. Select the tasks that members of this role can do.

7. Select **OK**.

### Delete or modify a role definition  

1. In Object Explorer, expand a report server node.

2. Expand the Security folder.

3. To delete or modify an item-level role definition, expand the Roles folder, then do one of the following actions:

    1. To delete a role definition, right-click the item > **Delete**. The **Delete Catalog Items** dialog box is displayed. Select **OK** to delete the role.
  
    2. To modify a role definition, right-click the item > **Properties**. The General page of the **User Role Properties** dialog box is displayed.

         Select the tasks that members of this role can do, and then select **OK**.
  
4. To delete or modify a system-level role definition, expand the **System Roles** folder. Do one of the following actions:

- To delete a system role definition, right-click the item and select **Delete**. The **Delete Catalog Items** dialog box is displayed. Select **OK** to delete the role.

- To modify a system role definition, right-click the item and select **Properties**. The General page of the **System Role Properties** dialog box is displayed. Select the tasks that members of this role can do, and select **OK** to apply the changes.

## Related content

 [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)  
 [Create and manage role assignments](../../reporting-services/security/create-and-manage-role-assignments.md)  
 [Reporting Services in SQL Server Management Studio &#40;SSRS&#41;](../../reporting-services/tools/reporting-services-in-sql-server-management-studio-ssrs.md)
