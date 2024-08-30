---
title: "sysmail_help_principalprofile_sp (Transact-SQL)"
description: "Lists information about associations between Database Mail profiles and database principals."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_principalprofile_sp_TSQL"
  - "sysmail_help_principalprofile_sp"
helpviewer_keywords:
  - "sysmail_help_principalprofile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_principalprofile_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information about associations between Database Mail profiles and database principals.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_principalprofile_sp [ { [ @principal_id = ] principal_id | [ @principal_name = ] 'principal_name' } ]
    [ [ , ] { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' } ]
[ ; ]
```

## Arguments

#### [ @principal_id = ] *principal_id*

The ID of the database user or role in the `msdb` database for the association to list. *@principal_id* is **int**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @principal_name = ] '*principal_name*'

The name of the database user or role in the `msdb` database for the association to list. *@principal_name* is **sysname**, with a default of `NULL`. Either *@principal_id* or *@principal_name* must be specified.

#### [ @profile_id = ] *profile_id*

The ID of the profile for the association to list. *@profile_id* is **int**, with a default of `NULL`. Either *@profile_id* or *@profile_name* can be specified.

#### [ @profile_name = ] '*profile_name*'

The name of the profile for the association to list. *@profile_name* is **sysname**, with a default of `NULL`. Either *@profile_id* or *@profile_name* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns a result set that contains the columns listed in the following table.

| Column name | Data type | Description |
| --- | --- | --- |
| `principal_id` | **int** | The ID of the database user. |
| `principal_name` | **sysname** | The name of the database user. |
| `profile_id` | **int** | The ID number of the Database Mail profile. |
| `profile_name` | **sysname** | The name of the Database Mail profile. |
| `is_default` | **bit** | The flag that states whether the profile is the default profile for the user. |

## Remarks

If `sysmail_help_principalprofile_sp` is invoked without parameters, the result set returned lists all of the associations in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Otherwise, the result set contains information for associations that match the provided parameters. For example, the procedure lists all of the associations for a profile when the profile name is provided.

`sysmail_help_principalprofile_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. List information for a specific association

The following example shows listing the information for all associations between the `AdventureWorks Administrator` profile and the `ApplicationLogin` principal in the `msdb` database.

```sql
EXEC msdb.dbo.sysmail_help_principalprofile_sp
    @principal_name = 'danw',
    @profile_name = 'AdventureWorks Administrator';
```

Here is a sample result set, reformatted for line length.

```output
principal_id principal_name     profile_id  profile_name                   is_default
------------ ------------------ ----------- ------------------------------ ----------
5            danw               9           AdventureWorks Administrator   1
```

### B. List information for all associations

The following example shows listing the information for all associations in the instance.

```sql
EXEC msdb.dbo.sysmail_help_principalprofile_sp;
```

Here is a sample result set, reformatted for line length.

```output
principal_id principal_name     profile_id  profile_name                   is_default
------------ ------------------ ----------- ------------------------------ ----------
6            terrid             3           Product Update Profile         1
5            danw               9           AdventureWorks Administrator   1
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
