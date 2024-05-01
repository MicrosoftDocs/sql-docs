---
title: "sysmail_configure_sp (Transact-SQL)"
description: "Changes configuration settings for Database Mail."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_configure_sp_TSQL"
  - "sysmail_configure_sp"
helpviewer_keywords:
  - "sysmail_configure_sp"
dev_langs:
  - "TSQL"
---
# sysmail_configure_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes configuration settings for Database Mail. The configuration settings specified with `sysmail_configure_sp` apply to the entire [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_configure_sp [ [ @parameter_name = ] 'parameter_name' ]
    [ , [ @parameter_value = ] 'parameter_value' ]
    [ , [ @description = ] 'description' ]
[ ; ]
```

## Arguments

#### [ @parameter_name = ] '*parameter_name*'

The name of the parameter to change.

#### [ @parameter_value = ] '*parameter_value*'

The new value of the parameter.

#### [ @description = ] '*description*'

A description of the parameter.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Database Mail uses the following parameters:

| Parameter name | Description | Default value |
| --- | --- | --- |
| `AccountRetryAttempts` | The number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. | `1` |
| `AccountRetryDelay` | The amount of time, in seconds, for the external mail process to wait between attempts to send a message. | `5000` |
| `DatabaseMailExeMinimumLifeTime` | The minimum amount of time, in seconds, that the external mail process remains active. When Database Mail is sending many messages, increase this value to keep Database Mail active and avoid the overhead of frequent starts and stops. | `600` |
| `DefaultAttachmentEncoding` | The default encoding for e-mail attachments. | `MIME` |
| `MaxFileSize` | The maximum size of an attachment, in bytes. | `1000000` |
| `ProhibitedExtensions` | A comma-separated list of extensions that can't be sent as an attachment to an e-mail message. | `exe,dll,vbs,js` |
| `LoggingLevel` | Specify which messages are recorded in the Database Mail log. One of the following numeric values:<br /><br />1 - This is normal mode. Logs only errors.<br /><br />2 - This is extended mode. Logs errors, warnings, and informational messages.<br /><br />3 - This is verbose mode. Logs errors, warnings, informational messages, success messages, and additional internal messages. Use this mode for troubleshooting. | `2` |

The stored procedure `sysmail_configure_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Set Database Mail to retry each account 10 times

The following example shows setting Database Mail to retry each account 10 times before considering the account to be unreachable.

```sql
EXEC msdb.dbo.sysmail_configure_sp
    'AccountRetryAttempts', '10';
```

### B. Set the maximum attachment size to 2 megabytes

The following example shows setting the maximum attachment size to 2 megabytes.

```sql
EXEC msdb.dbo.sysmail_configure_sp
    'MaxFileSize', '2097152';
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [sysmail_help_configure_sp (Transact-SQL)](sysmail-help-configure-sp-transact-sql.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
