---
title: "sysmail_help_configure_sp (Transact-SQL)"
description: "Displays configuration settings for Database Mail."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_configure_sp"
  - "sysmail_help_configure_sp_TSQL"
helpviewer_keywords:
  - "sysmail_help_configure_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_configure_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays configuration settings for Database Mail.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_configure_sp [ [ @parameter_name = ] 'parameter_name' ]
[ ; ]
```

## Arguments

#### [ @parameter_name = ] '*parameter_name*'

The name of the configuration setting to retrieve. When specified, the value of the configuration setting is returned in the *@parameter_value* OUTPUT parameter. When no *@parameter_name* is specified, this stored procedure returns a result set containing all of the Database Mail configuration settings in the instance.

## Return code values

`0` (success) or `1` (failure).

## Result set

When no *@parameter_name* is specified, `sysmail_help_configure_sp` returns a result set with the following columns.

| Column name | Data type | Description |
| --- | --- | --- |
| `paramname` | **nvarchar(256)** | The name of the configuration parameter. |
| `paramvalue` | **nvarchar(256)** | The value of the configuration parameter. |
| `description` | **nvarchar(256)** | A description of the configuration parameter. |

## Remarks

The stored procedure `sysmail_help_configure_sp` lists the current Database Mail configuration settings for the instance.

When a *@parameter_name* is specified, but no output parameter is provided for *@parameter_value*, this stored procedure produces no output.

The stored procedure `sysmail_help_configure_sp` is in the `msdb` database and is owned by the **dbo** schema. The procedure must be invoked with a three-part name if the current database isn't `msdb`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example shows listing the Database Mail configuration settings for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

```sql
EXEC msdb.dbo.sysmail_help_configure_sp;
```

Here is a sample result set, edited for line length:

```output
paramname                       paramvalue      description
------------------------------- --------------- -----------------------------------------------------------------------------
AccountRetryAttempts            1               Number of retry attempts for a mail server
AccountRetryDelay               5000            Delay between each retry attempt to mail server
DatabaseMailExeMinimumLifeTime  600             Minimum process lifetime in seconds
DefaultAttachmentEncoding       MIME            Default attachment encoding
LoggingLevel                    2               Database Mail logging level: normal - 1, extended - 2 (default), verbose - 3
MaxFileSize                     1000000         Default maximum file size
ProhibitedExtensions            exe,dll,vbs,js  Extensions not allowed in outgoing mails
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
