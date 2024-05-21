---
title: "sp_helprotect (Transact-SQL)"
description: sp_helprotect returns a report that's information about user permissions for an object, or statement permissions, in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helprotect"
  - "sp_helprotect_TSQL"
helpviewer_keywords:
  - "sp_helprotect"
dev_langs:
  - "TSQL"
---
# sp_helprotect (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a report that's information about user permissions for an object, or statement permissions, in the current database.

> [!IMPORTANT]  
> `sp_helprotect` doesn't return information about securables that were introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions. Use [sys.database_permissions](../system-catalog-views/sys-database-permissions-transact-sql.md) and [sys.fn_builtin_permissions](../system-functions/sys-fn-builtin-permissions-transact-sql.md) instead.

Doesn't list permissions that are always assigned to the fixed server roles or fixed database roles. Doesn't include logins or users that receive permissions based on their membership in a role.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helprotect
    [ [ @name = ] N'name' ]
    [ , [ @username = ] N'username' ]
    [ , [ @grantorname = ] N'grantorname' ]
    [ , [ @permissionarea = ] 'permissionarea' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the object in the current database, or a statement, that's the permissions to report. *@name* is **nvarchar(776)**, with a default of `NULL`, which returns all object and statement permissions. If the value is an object (table, view, stored procedure, or extended stored procedure), it must be a valid object in the current database. The object name can include an owner qualifier in the form `<owner>.<object>`.

If *@name* is a statement, it can be a `CREATE` statement.

#### [ @username = ] N'*username*'

The name of the principal for which permissions are returned. *@username* is **sysname**, with a default of `NULL`, which returns all principals in the current database. *@username* must exist in the current database.

#### [ @grantorname = ] N'*grantorname*'

The name of the principal that granted permissions. *@grantorname* is **sysname**, with a default of `NULL`, which returns all information for permissions granted by any principal in the database.

#### [ @permissionarea = ] '*permissionarea*'

A character string that indicates whether to display object permissions (character string `o`), statement permissions (character string `s`), or both (`o s`). *@permissionarea* is **varchar(10)**, with a default of `o s`. *@permissionarea* can be any combination of `o` and `s`, with or without commas or spaces between `o` and `s`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Owner` | **sysname** | Name of the object owner. |
| `Object` | **sysname** | Name of the object. |
| `Grantee` | **sysname** | Name of the principal to which permissions were granted. |
| `Grantor` | **sysname** | Name of the principal that granted permissions to the specified grantee. |
| `ProtectType` | **nvarchar(10)** | Name of the type of protection:<br /><br />`GRANT REVOKE` |
| `Action` | **nvarchar(60)** | Name of the permission. Valid permission statements depend upon the type of object. |
| `Column` | **sysname** | Type of permission:<br /><br />`All` = Permission covers all current columns of the object.<br />`New` = Permission covers any new columns that might be changed (by using the `ALTER` statement) on the object in the future.<br />`All+New` = Combination of `All` and `New`.<br /><br />Returns a period if the type of permission doesn't apply to columns. |

## Remarks

All the parameters in the following procedure are optional. If executed with no parameters, `sp_helprotect` displays all the permissions that are granted or denied in the current database.

If some but not all the parameters are specified, use named parameters to identify the particular parameter, or `NULL` as a placeholder. For example, to report all permissions for the grantor database owner (`dbo`), execute the following script:

```sql
EXEC sp_helprotect NULL, NULL, dbo;
```

Or

```sql
EXEC sp_helprotect @grantorname = 'dbo';
```

The output report is sorted by permission category, owner, object, grantee, grantor, protection type category, protection type, action, and column sequential ID.

## Permissions

Requires membership in the **public** role.

Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission don't appear. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. List the permissions for a table

The following example lists the permissions for the `titles` table.

```sql
EXEC sp_helprotect 'titles';
```

### B. List the permissions for a user

The following example lists all permissions that user `Judy` has in the current database.

```sql
EXEC sp_helprotect NULL, 'Judy';
```

### C. List the permissions granted by a specific user

The following example lists all permissions granted by user `Judy` in the current database, and uses `NULL` as a placeholder for the missing parameters.

```sql
EXEC sp_helprotect NULL, NULL, 'Judy';
```

### D. List the statement permissions only

The following example lists all the statement permissions in the current database, and uses `NULL` as a placeholder for the missing parameters.

```sql
EXEC sp_helprotect NULL, NULL, NULL, 's';
```

### e. Listing the permissions for a CREATE statement

The following example list all users who are granted the `CREATE TABLE` permission.

```sql
EXEC sp_helprotect @name = 'CREATE TABLE';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [DENY (Transact-SQL)](../../t-sql/statements/deny-transact-sql.md)
- [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md)
- [REVOKE (Transact-SQL)](../../t-sql/statements/revoke-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
