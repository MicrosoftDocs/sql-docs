---
title: "sysmail_help_account_sp (Transact-SQL)"
description: "Lists information (except passwords) about Database Mail accounts."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_account_sp_TSQL"
  - "sysmail_help_account_sp"
helpviewer_keywords:
  - "sysmail_help_account_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_account_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information (except passwords) about Database Mail accounts.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_account_sp [ [ @account_id = ] account_id | [ @account_name = ] 'account_name' ]
[ ; ]
```

## Arguments

#### [ @account_id = ] *account_id*

The account ID of the account to list information for. *@account_id* is **int**, with a default of `NULL`.

#### [ @account_name = ] '*account_name*'

The name of the account to list information for. *@account_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns a result set containing the columns listed below.

| Column name | Data type | Description |
| --- | --- | --- |
| `account_id` | **int** | The ID of the account. |
| `name` | **sysname** | The name of the account. |
| `description` | **nvarchar(256)** | The description for the account. |
| `email_address` | **nvarchar(128)** | The e-mail address to send messages from. |
| `display_name` | **nvarchar(128)** | The display name for the account. |
| `replyto_address` | **nvarchar(128)** | The address where replies to messages from this account are sent. |
| `servertype` | **sysname** | The type of e-mail server for the account. |
| `servername` | **sysname** | The name of the e-mail server for the account. |
| `port` | **int** | The port number of the e-mail server uses. |
| `username` | **nvarchar(128)** | The user name to use to sign in to the e-mail server, if the e-mail server uses authentication. When `username` is `NULL`, Database Mail doesn't use authentication for this account. |
| `use_default_credentials` | **bit** | Specifies whether to send the mail to the SMTP server using the credentials of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. When this parameter is `1`, Database Mail uses the credentials of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service. When this parameter is `0`, Database Mail uses the *@username* and *@password* for authentication on the SMTP server. If *@username* and *@password* are `NULL`, then Database Mail uses anonymous authentication. Consult your SMTP administrator before specifying this parameter. |
| `enable_ssl` | **bit** | Specifies whether Database Mail encrypts communication using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). Use this option if TLS is required on your SMTP server. `1` indicates Database Mail encrypts communication using TLS. 0 indicates Database Mail sends the mail without TLS encryption. |

## Remarks

When no *account_id* or *account_name* is provided, `sysmail_help_account` lists information on all Database Mail accounts in the Microsoft SQL Server instance.

The stored procedure `sysmail_help_account_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. List the information for all accounts

The following example shows listing the account information for all accounts in the instance.

```sql
EXEC msdb.dbo.sysmail_help_account_sp;
```

Here is a sample result set, edited for line length:

```output
account_id  name                         description                             email_address             display_name                     replyto_address servertype servername                port        username use_default_credentials enable_ssl
----------- ---------------------------- --------------------------------------- ------------------------- -------------------------------- --------------- ---------- ------------------------- ----------- -------- ----------------------- ----------
148         AdventureWorks Administrator Mail account for administrative e-mail. dba@adventure-works.com   AdventureWorks Automated Mailer  NULL            SMTP       smtp.adventure-works.com  25          NULL 0                          0
149         Audit Account                Account for audit e-mail.               audit@adventure-works.com Automated Mailer (Audit)         NULL            SMTP       smtp.adventure-works.com  25          NULL 0                          0
```

### B. List the information for a specific account

The following example shows listing the account information for the account named `AdventureWorks Administrator`.

```sql
EXEC msdb.dbo.sysmail_help_account_sp
    @account_name = 'AdventureWorks Administrator';
```

Here is a sample result set, edited for line length:

```output
account_id  name                         description                             email_address             display_name                     replyto_address servertype servername                port        username use_default_credentials enable_ssl
----------- ---------------------------- ------------------------------------------------------ ------------------------- ---------------- ---------- ------------------------- ----------- -------- ----------------------- ----------
148         AdventureWorks Administrator Mail account for administrative e-mail. dba@adventure-works.com   AdventureWorks Automated Mailer  NULL            SMTP       smtp.adventure-works.com  25          NULL     0                       0
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Create a Database Mail Account](../database-mail/create-a-database-mail-account.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
