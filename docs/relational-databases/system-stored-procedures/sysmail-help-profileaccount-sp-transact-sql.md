---
title: "sysmail_help_profileaccount_sp (Transact-SQL)"
description: "Lists the accounts associated with one or more Database Mail profiles."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_profileaccount_sp_TSQL"
  - "sysmail_help_profileaccount_sp"
helpviewer_keywords:
  - "sysmail_help_profileaccount_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_profileaccount_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists the accounts associated with one or more Database Mail profiles.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_profileaccount_sp
   { [ @profile_id = ] profile_id
      | [ @profile_name = ] 'profile_name' }
   [ , { [ @account_id = ] account_id
         | [ @account_name = ] 'account_name' } ]
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The profile ID of the profile to list. *@profile_id* is **int**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @profile_name = ] '*profile_name*'

The profile name of the profile to list. *@profile_name* is **sysname**, with a default of `NULL`. Either *@profile_id* or *@profile_name* must be specified.

#### [ @account_id = ] *account_id*

The account ID to list. *@account_id* is **int**, with a default of `NULL`. When *@account_id* and *@account_name* are both NULL, lists all the accounts in the profile.

#### [ @account_name = ] '*account_name*'

The name of the account to list. *@account_name* is **sysname**, with a default of `NULL`. When *@account_id* and *@account_name* are both NULL, lists all the accounts in the profile.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns a result set with the following columns.

| Column name | Data type | Description |
| --- | --- | --- |
| `profile_id` | **int** | The profile ID of the profile. |
| `profile_name` | **sysname** | The name of the profile. |
| `account_id` | **int** | The account ID of the account. |
| `account_name` | **sysname** | The name of the account. |
| `sequence_number` | **int** | The sequence number of the account within the profile. |

## Remarks

When no *@profile_id* or *@profile_name* is specified, this stored procedure returns information for every profile in the instance.

The stored procedure `sysmail_help_profileaccount_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. List the accounts for a specific profile by name

The following example shows listing the information for the `AdventureWorks Administrator` profile by specifying the profile name.

```sql
EXEC msdb.dbo.sysmail_help_profileaccount_sp
   @profile_name = 'AdventureWorks Administrator';
```

Here is a sample result set, edited for line length:

```output
profile_id  profile_name                 account_id  account_name         sequence_number
----------- ---------------------------- ----------- -------------------- ---------------
131         AdventureWorks Administrator 197         Admin-MainServer     1
131         AdventureWorks Administrator 198         Admin-BackupServer   2
```

### B. List the accounts for a specific profile by profile ID

The following example shows listing the information for the `AdventureWorks Administrator` profile by specifying the profile ID for the profile.

```sql
EXEC msdb.dbo.sysmail_help_profileaccount_sp
    @profile_id = 131 ;
```

Here is a sample result set, edited for line length:

```output
profile_id  profile_name                 account_id  account_name         sequence_number
----------- ---------------------------- ----------- -------------------- ---------------
131         AdventureWorks Administrator 197         Admin-MainServer     1
131         AdventureWorks Administrator 198         Admin-BackupServer   2
```

### C. List the accounts for all profiles

The following example shows listing the accounts for all profiles in the instance.

```sql
EXEC msdb.dbo.sysmail_help_profileaccount_sp;
```

Here is a sample result set, edited for line length:

```output
profile_id  profile_name                 account_id  account_name         sequence_number
----------- ---------------------------- ----------- -------------------- ---------------
131         AdventureWorks Administrator 197         Admin-MainServer     1
131         AdventureWorks Administrator 198         Admin-BackupServer   2
106         AdventureWorks Operator      210         Operator-MainServer  1
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Create a Database Mail Account](../database-mail/create-a-database-mail-account.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
