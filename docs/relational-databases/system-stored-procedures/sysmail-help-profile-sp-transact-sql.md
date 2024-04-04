---
title: "sysmail_help_profile_sp (Transact-SQL)"
description: "Lists information about one or more mail profiles."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_profile_sp_TSQL"
  - "sysmail_help_profile_sp"
helpviewer_keywords:
  - "sysmail_help_profile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_profile_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information about one or more mail profiles.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_profile_sp [ [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' ]
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The profile ID to return information for. *@profile_id* is **int**, with a default of `NULL`.

#### [ @profile_name = ] '*profile_name*'

The profile name to return information for. *@profile_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns a result set with the following columns.

| Column name | Data type | Description |
| --- | --- | --- |
| `profile_id` | **int** | The profile ID for the profile. |
| `name` | **sysname** | The profile name for the profile. |
| `description` | **nvarchar(256)** | The description for the profile. |

## Remarks

When a profile name or profile ID is specified, `sysmail_help_profile_sp` returns information about that profile. Otherwise, `sysmail_help_profile_sp` returns information about every profile in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

The stored procedure `sysmail_help_profile_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. List all profiles

The following example shows listing all profiles in the instance.

```sql
EXEC msdb.dbo.sysmail_help_profile_sp;
```

Here is a sample result set, reformatted for line length:

```output
profile_id  name                          description
----------- ----------------------------- ------------------------------
56          AdventureWorks Administrator  Administrative mail profile.
57          AdventureWorks Operator       Operator mail profile.
```

### B. List a specific profile

The following example shows listing information for the profile `AdventureWorks Administrator`.

```sql
EXEC msdb.dbo.sysmail_help_profile_sp
    @profile_name = 'AdventureWorks Administrator' ;
```

Here is a sample result set, reformatted for line length:

```output
profile_id  name                          description
----------- ----------------------------- ------------------------------
56          AdventureWorks Administrator  Administrative mail profile.
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
