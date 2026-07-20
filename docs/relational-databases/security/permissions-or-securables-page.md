---
title: "Permissions or Securables Page"
description: Use the Permissions page or the Securables page to view or set the permissions for securables in SQL Server.
author: VanMSFT
ms.author: vanto
ms.date: 05/01/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.common.permissions.f1"
  - "sql13.swb.SecurableAndEffectPermissions.f1"
  - "sql13.swb.common.columnperm.f1"
  - "sql13.swb.availabilitygroupproperties.permission.f1"
  - "sql13.swb.SecurableAndEffectivePermission.f1"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Permissions or Securables Page

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Use the **Permissions** page or the **Securables** page to view or set the permissions for securables. This page can be opened from many locations. The contents of the page can change slightly, depending on how the page is opened and what it contains. The top grid of the page might be populated when the page opens, or it might be empty. To add items to the upper grid, select **Search**. In the upper grid, select an item, and then set the appropriate permissions on the **Explicit** tab. To view aggregated permissions, use the **Effective** tab.

To understand the possible combinations of securables and principals, see the securable-specific syntax links in the article [GRANT](../../t-sql/statements/grant-transact-sql.md). For more information, see [Securables](securables.md).

## Page Header

The header of the **Permissions** or **Securables** page varies depending on the securable or principal. It displays information relevant to the item, such as its name.

## Upper Grid

The upper grid contains one or more items for which permissions can be set. This dialog box provides the **Search** button for selecting objects or principals to add to the upper grid. The name of the grid might display **Securables** or one or more types of securables or principals. The columns that are displayed in the upper grid vary depending on the principal or securable.

**Name**  
The name of each principal or securable that is added to the grid.

**Type**  
Describes the type of each item.

## Explicit Tab

The **Explicit** tab lists the possible permissions for the securable that are selected in the upper grid. To configure the permissions, select or clear the **Grant** (or **Allow**), **With Grant**, and **Deny** check boxes. All options aren't available for all explicit permissions.

**Permissions**  
The name of the permission.

**Grantor**  
The principal that granted the permission.

**Grant**  
Select to grant this permission to the login. Clear to revoke this permission.

**With Grant**  
Reflects the state of the WITH GRANT option for the listed permission. This box is read-only. To apply this permission, use the [GRANT](../../t-sql/statements/grant-transact-sql.md) statement.

**Deny**  
Select to deny this permission to the login. Clear to revoke this permission.

**Column Permissions**  
For objects that contain columns (such as tables, views, or table-valued functions), the **Column Permissions** button opens the **Column Permissions** dialog box. In this dialog box, you can set **Grant**, **Allow**, or **Deny** permissions on individual columns of a table or view. This option isn't available for all object types or permissions.

## Effective Tab

The permissions that a principal has related to a securable might come from permissions that are set for several different principals. For example, a login might be granted permissions individually and also as a member of a group. The **Effective** tab shows the result of combining explicit permissions and the permissions that are received from group or role memberships. Grant permissions are aggregated. A deny permission overrides all grant permissions.

**Permissions**  
The name of the permission.

**Column**  
The names of columns that are affected by the permission.

## Related content

- [Database-level roles](authentication-access/database-level-roles.md)
- [Security for SQL Server Database Engine and Azure SQL Database](security-center-for-sql-server-database-engine-and-azure-sql-database.md)
