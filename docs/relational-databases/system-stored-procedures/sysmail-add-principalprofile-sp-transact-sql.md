---
title: "sysmail_add_principalprofile_sp (Transact-SQL)"
description: "Grants permission for an msdb database principal to use a Database Mail profile."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_add_principalprofile_sp_TSQL"
  - "sysmail_add_principalprofile_sp"
helpviewer_keywords:
  - "sysmail_add_principalprofile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_add_principalprofile_sp (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Grants permission for an `msdb` database principal to use a Database Mail profile. The database principal must map to a SQL Server authentication user, a Windows User, or a Windows Group.

In Azure SQL Managed Instance and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the database principal can also map to a Microsoft Entra user.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_add_principalprofile_sp
    { [ @principal_id = ] principal_id | [ @principal_name = ] 'principal_name' }
    , { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' }
    [ , [ @is_default ] = 'is_default' ]
[ ; ]
```

## Arguments

#### [ @principal_id = ] *principal_id*

The ID of the database user or role in the `msdb` database for the association. *@principal_id* is **int**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified. A *@principal_id* of `0` makes this profile a public profile, granting access to all principals in the database.

#### [ @principal_name = ] '*principal_name*'

The name of the database user or role in the `msdb` database for the association. *@principal_name* is **sysname**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified. A *@principal_name* of `public` makes this profile a public profile, granting access to all principals in the database.

#### [ @profile_id = ] *profile_id*

The ID of the profile for the association. *@profile_id* is **int**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @profile_name = ] '*profile_name*'

The name of the profile for the association. *@profile_name* is **sysname**, with no default. Either *@profile_id* or *@profile_name* must be specified.

#### [ @is_default = ] *is_default*

Specifies whether this profile is the default profile for the principal. A principal must have exactly one default profile. *@is_default* is **bit**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To make a profile public, specify a *@principal_id* of `0` or a *@principal_name* of `public`. A public profile is available to all users in the `msdb` database, though users must also be a member of **DatabaseMailUserRole** to execute `sp_send_dbmail`.

A database user can only have one default profile. When *@is_default* is `1` and the user is already associated with one or more profiles, the specified profile becomes the default profile for the user. The profile that was previously the default profile is still associated with the user, but is no longer the default profile.

When *@is_default* is `0` and no other association exists, the stored procedure returns an error.

The stored procedure `sysmail_add_principalprofile_sp` is in the `msdb` database and is owned by the `dbo` schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Create an association and set the default profile

The following example creates an association between the profile named `AdventureWorks Administrator Profile` and the `msdb` database user `ApplicationUser`. The profile is the default profile for the user.

```sql
EXEC msdb.dbo.sysmail_add_principalprofile_sp
    @principal_name = 'ApplicationUser',
    @profile_name = 'AdventureWorks Administrator Profile',
    @is_default = 1;
```

### B. Make a profile the default public profile

The following example makes the profile `AdventureWorks Public Profile` the default public profile for users in the `msdb` database.

```sql
EXEC msdb.dbo.sysmail_add_principalprofile_sp
    @principal_name = 'public',
    @profile_name = 'AdventureWorks Public Profile',
    @is_default = 1;
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
