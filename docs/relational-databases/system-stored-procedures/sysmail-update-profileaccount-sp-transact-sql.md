---
title: "sysmail_update_profileaccount_sp (Transact-SQL)"
description: "Updates the sequence number of an account within a Database Mail profile."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_update_profileaccount_sp_TSQL"
  - "sysmail_update_profileaccount_sp"
helpviewer_keywords:
  - "sysmail_update_profileaccount_sp"
dev_langs:
  - "TSQL"
---
# sysmail_update_profileaccount_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates the sequence number of an account within a Database Mail profile.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_update_profileaccount_sp { [ @profile_id = ] profile_id
| [ @profile_name = ] 'profile_name' } ,
    { [ @account_id = ] account_id | [ @account_name = ] 'account_name' } ,
    [ @sequence_number = ] sequence_number
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The profile ID of the profile to update. *@profile_id* is **int**, with a default of `NULL`. Either the *@profile_id* or the *@profile_name* must be specified.

#### [ @profile_name = ] '*profile_name*'

The profile name of the profile to update. *@profile_name* is **sysname**, with a default of `NULL`. Either the *@profile_id* or the *@profile_name* must be specified.

#### [ @account_id = ] *account_id*

The account ID to update. *@account_id* is **int**, with a default of `NULL`. Either the *@account_id* or the *@account_name* must be specified.

#### [ @account_name = ] '*account_name*'

The name of the account to update. *@account_name* is **sysname**, with a default of `NULL`. Either the *@account_id* or the *@account_name* must be specified.

#### [ @sequence_number = ] *sequence_number*

The new sequence number for the account. *@sequence_number* is **int**, with no default. The sequence number determines the order in which accounts are used in the profile.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Returns an error if the account specified isn't associated with the profile specified.

The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that's the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on, until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the e-mail message fails.

If more than one account exists with the same sequence number, Database Mail only uses one of those accounts for a given e-mail message. In this case, Database Mail makes no guarantees as to which of the accounts is used for that sequence number or that the same account is used from message to message.

The stored procedure `sysmail_update_profileaccount_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example changes the sequence number of the account `Admin-BackupServer` within the profile `AdventureWorks Administrator` in the `msdb` database. After executing this code, the sequence number for the account is `3`, indicating it will be tried if the first two accounts fail.

```sql
EXEC msdb.dbo.sysmail_update_profileaccount_sp
    @profile_name = 'AdventureWorks Administrator',
    @account_name = 'Admin-BackupServer',
    @sequence_number = 3;
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Create a Database Mail Account](../database-mail/create-a-database-mail-account.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
