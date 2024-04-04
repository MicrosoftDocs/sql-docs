---
title: "sysmail_update_principalprofile_sp (Transact-SQL)"
description: "Updates the information for an association between a principal and a profile."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_update_principalprofile_sp"
  - "sysmail_update_principalprofile_sp_TSQL"
helpviewer_keywords:
  - "sysmail_update_principalprofile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_update_principalprofile_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates the information for an association between a principal and a profile.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_update_principalprofile_sp { @principal_id = principal_id | @principal_name = 'principal_name' } ,
    { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' } ,
    [ @is_default = ] 'is_default'
[ ; ]
```

## Arguments

#### [ @principal_id = ] *principal_id*

The ID of the database user or role in the `msdb` database for the association to change. *principal_id* is **int**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @principal_name = ] '*principal_name*'

The name of the database user or role in the `msdb` database for the association to update. *principal_name* is **sysname**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @profile_id = ] *profile_id*

The ID of the profile for the association to change. *@profile_id* is **int**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @profile_name = ] '*profile_name*'

The name of the profile for the association to change. *@profile_name* is **sysname**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @is_default = ] *is_default*

Whether this profile is the default profile for the database user. A database user may only have one default profile. *@is_default* is **bit**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

This stored procedure changes whether the profile specified is the default profile for the database user. A database user may only have one default private profile.

When the principal name for the association is **public** or the principal ID for the association is `0`, this stored procedure changes the public profile. There can only be one default public profile.

When *@is_default* is `1` and the principal is associated with more than one profile, the specified profile becomes the default profile for the principal. The profile that was previously the default profile is still associated with the principal, but is no longer the default profile.

The stored procedure `sysmail_update_principalprofile_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Set a profile to be the default public profile for a database

The following example sets the profile `General Use Profile` to be the default public profile for users in the `msdb` database.

```sql
EXEC msdb.dbo.sysmail_update_principalprofile_sp
    @principal_name = 'public',
    @profile_name = 'General Use Profile',
    @is_default = '1';
```

### B. Set a profile to be the default private profile for a user

The following example sets the profile `AdventureWorks Administrator` to be the default profile for the principal `ApplicationUser` in the `msdb` database. The profile must already be associated with the principal. The profile that was previously the default profile is still associated with the principal, but is no longer the default profile.

```sql
EXEC msdb.dbo.sysmail_update_principalprofile_sp
    @principal_name = 'ApplicationUser',
    @profile_name = 'AdventureWorks Administrator',
    @is_default = '1' ;
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
