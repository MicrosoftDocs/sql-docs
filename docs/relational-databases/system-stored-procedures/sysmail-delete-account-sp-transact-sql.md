---
title: "sysmail_delete_account_sp (Transact-SQL)"
description: "Deletes a Database Mail SMTP account."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_delete_account_sp"
  - "sysmail_delete_account_sp_TSQL"
helpviewer_keywords:
  - "sysmail_delete_account_sp"
dev_langs:
  - "TSQL"
---
# sysmail_delete_account_sp (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Deletes a Database Mail SMTP account. You can also use the Database Mail Configuration Wizard to delete an account.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_delete_account_sp { [ @account_id = ] account_id | [ @account_name = ] 'account_name' }
[ ; ]
```

## Arguments

#### [ @account_id = ] *account_id*

The ID number of the account to delete. *@account_id* is **int**, with no default. Either *@account_id* or *@account_name* must be specified.

#### [ @account_name = ] '*account_name*'

The name of the account to delete. *@account_name* is **sysname**, with no default. Either *@account_id* or *@account_name* must be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

This procedure deletes the account specified, regardless of whether the account is in use by a profile. A profile that contains no accounts can't successfully send e-mail.

The stored procedure `sysmail_delete_account_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example shows deleting  the Database Mail account named `AdventureWorks Administrator`.

```sql
EXEC msdb.dbo.sysmail_delete_account_sp
    @account_name = 'AdventureWorks Administrator';
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Create a Database Mail Account](../database-mail/create-a-database-mail-account.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [sysmail_add_account_sp (Transact-SQL)](sysmail-add-account-sp-transact-sql.md)
- [sysmail_delete_profile_sp (Transact-SQL)](sysmail-delete-profile-sp-transact-sql.md)
- [sysmail_delete_profileaccount_sp (Transact-SQL)](sysmail-delete-profileaccount-sp-transact-sql.md)
- [sysmail_help_account_sp (Transact-SQL)](sysmail-help-account-sp-transact-sql.md)
- [sysmail_help_profile_sp (Transact-SQL)](sysmail-help-profile-sp-transact-sql.md)
- [sysmail_help_profileaccount_sp (Transact-SQL)](sysmail-help-profileaccount-sp-transact-sql.md)
- [sysmail_update_profileaccount_sp (Transact-SQL)](sysmail-update-profileaccount-sp-transact-sql.md)
