---
title: "sysmail_delete_principalprofile_sp (Transact-SQL)"
description: "Removes permission for a database user or role to use a public or private Database Mail profile."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_delete_principalprofile_sp_TSQL"
  - "sysmail_delete_principalprofile_sp"
helpviewer_keywords:
  - "sysmail_delete_principalprofile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_delete_principalprofile_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes permission for a database user or role to use a public or private Database Mail profile.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_delete_principalprofile_sp { [ @principal_id = ] principal_id | [ @principal_name = ] 'principal_name' } ,
    { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' }
[ ; ]
```

## Arguments

#### [ @principal_id = ] *principal_id*

The ID of the database user or role in the `msdb` database for the association to delete. *@principal_id* is **int**, with a default of `NULL`. To make a public profile into a private profile, provide the principal ID `0` or the principal name `public`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @principal_name = ] '*principal_name*'

The name of the database user or role in the `msdb` database for the association to delete. *@principal_name* is **sysname**, with a default of `NULL`. To make a public profile into a private profile, provide the principal ID `0` or the principal name `public`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @profile_id = ] *profile_id*

The ID of the profile for the association to delete. *@profile_id* is **int**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @profile_name = ] '*profile_name*'

The name of the profile for the association to delete. *@profile_name* is **sysname**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To make a public profile into a private profile, provide **'public'** for the principal name or `0` for the principal ID.

Use caution when removing permissions for the default private profile for a user or the default public profile. When no default profile is available, `sp_send_dbmail` requires the name of a profile as an argument. Therefore, removing a default profile causes calls to `sp_send_dbmail` to fail. For more information, see [sp_send_dbmail](sp-send-dbmail-transact-sql.md).

The stored procedure `sysmail_delete_principalprofile_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example shows deleting the association between the profile `AdventureWorks Administrator` and the login `ApplicationUser` in the `msdb` database.

```sql
EXEC msdb.dbo.sysmail_delete_principalprofile_sp
    @principal_name = 'ApplicationUser',
    @profile_name = 'AdventureWorks Administrator';
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
