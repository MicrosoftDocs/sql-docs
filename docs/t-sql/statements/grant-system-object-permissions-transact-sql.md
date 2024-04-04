---
title: "GRANT system object permissions (Transact-SQL)"
description: Grants permissions on system objects such as system stored procedures, extended stored procedures, functions, and views.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "encryption [SQL Server], system objects"
  - "system objects [SQL Server]"
  - "GRANT statement, system objects"
dev_langs:
  - "TSQL"
---
# GRANT system object permissions (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Grants permissions on system objects such as system stored procedures, extended stored procedures, functions, and views.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
GRANT { SELECT | EXECUTE } ON [ sys. ] system_object TO principal
[ ; ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### [ sys. ]

The sys qualifier is required only when you're referring to catalog views and dynamic management views.

#### *system_object*

Specifies the object on which permission is being granted.

#### *principal*

Specifies the principal to which the permission is being granted.

## Remarks

This statement can be used to grant permissions on certain stored procedures, extended stored procedures, table-valued functions, scalar functions, views, catalog views, compatibility views, `INFORMATION_SCHEMA` views, dynamic management views, and system tables that are installed by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Each of these system objects exists as a unique record in the resource database of the server (mssqlsystemresource). The resource database is read-only. A link to the object is exposed as a record in the sys schema of every database. Permission to execute or select a system object can be granted, denied, and revoked.

Granting permission to execute or select an object doesn't necessarily convey all the permissions required to use the object. Most objects perform operations for which extra permissions are required. For example, a user that is granted `EXECUTE` permission on `sp_addlinkedserver` can't create a linked server unless the user is also a member of the **sysadmin** fixed server role.

Default name resolution resolves unqualified procedure names to the resource database. Therefore, the sys qualifier is only required when you're specifying catalog views and dynamic management views.

Granting permissions on triggers and on columns of system objects isn't supported.

Permissions on system objects are preserved during upgrades of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

You must be in the `master` database to grant permissions, and the principal you grant the permissions to must be a user in the `master` database. That is, if they're server-level permissions, you can't grant them to server principals, only database principals.

System objects are visible in the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) catalog view. The permissions on system objects are visible in the [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md) catalog view in the `master` database.

The following query returns information about permissions of system objects:

```sql
SELECT *
FROM master.sys.database_permissions AS dp
INNER JOIN sys.system_objects AS so
    ON dp.major_id = so.object_id
WHERE dp.class = 1 AND so.parent_object_id = 0;
GO
```

## Permissions

Requires CONTROL SERVER permission.

## Examples

### A. Grant SELECT permission on a view

The following example grants the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `Sylvester1` permission to select a view that lists [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The example then grants the extra permission that is required to view metadata on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins that the user doesn't own.

```sql
USE master;
GO
GRANT SELECT ON sys.sql_logins TO Sylvester1;
GRANT VIEW SERVER STATE to Sylvester1;
GO
```

### B. Grant EXECUTE permission on an extended stored procedure

The following example grants `EXECUTE` permission on `xp_readmail` to `Sylvester1`.

```sql
USE master;
GO
GRANT EXECUTE ON xp_readmail TO Sylvester1;
GO
```

## Related content

- [sys.system_objects (Transact-SQL)](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md)
- [sys.database_permissions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)
- [REVOKE System Object Permissions (Transact-SQL)](revoke-system-object-permissions-transact-sql.md)
- [DENY System Object Permissions (Transact-SQL)](deny-system-object-permissions-transact-sql.md)
