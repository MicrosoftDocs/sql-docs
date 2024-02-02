---
title: "sysmail_update_profile_sp (Transact-SQL)"
description: "Changes the description or name of a Database Mail profile."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_update_profile_sp"
  - "sysmail_update_profile_sp_TSQL"
helpviewer_keywords:
  - "sysmail_update_profile_sp"
dev_langs:
  - "TSQL"
---
# sysmail_update_profile_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the description or name of a Database Mail profile.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_update_profile_sp [ [ @profile_id = ] profile_id , ] [ [ @profile_name = ] 'profile_name' , ]
    [ [ @description = ] 'description' ]
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The profile ID to update. *@profile_id* is **int**, with a default of `NULL`. At least one of *@profile_id* or *@profile_name* must be specified. If both are specified, the procedure changes the name of the profile.

#### [ @profile_name = ] '*profile_name*'

The name of the profile to update or the new name for the profile. *@profile_name* is **sysname**, with a default of `NULL`. At least one of *@profile_id* or *@profile_name* must be specified. If both are specified, the procedure changes the name of the profile.

#### [ @description = ] '*description*'

The new description for the profile. *@description* is **nvarchar(256)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When both the profile ID and the profile name are specified, the procedure changes the name of the profile to the provided name and updates the description for the profile. When only one of these arguments is provided, the procedure updates the description for the profile.

The stored procedure `sysmail_update_profile_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Change the description of a profile

The following example changes the description for the profile named `AdventureWorks Administrator` in the `msdb` database.

```sql
EXEC msdb.dbo.sysmail_update_profile_sp
    @profile_name = 'AdventureWorks Administrator',
    @description = 'Administrative mail profile.';
```

### B. Change the name and description of a profile

The following example changes the name and description of the profile with the profile ID `750`.

```sql
EXEC msdb.dbo.sysmail_update_profile_sp
    @profile_id = 750,
    @profile_name = 'Operator',
    @description = 'Profile to send alert e-mail to operators.';
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Create a Database Mail Account](../database-mail/create-a-database-mail-account.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
