---
title: "sp_addmessage (Transact-SQL)"
description: Stores a new user-defined error message in an instance of the SQL Server Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addmessage"
  - "sp_addmessage_TSQL"
helpviewer_keywords:
  - "sp_addmessage"
dev_langs:
  - "TSQL"
---
# sp_addmessage (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stores a new user-defined error message in an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. Messages stored by using `sp_addmessage` can be viewed by using the `sys.messages` catalog view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmessage
    [ [ @msgnum = ] msgnum ]
    [ , [ @severity = ] severity ]
    [ , [ @msgtext = ] N'msgtext' ]
    [ , [ @lang = ] N'lang' ]
    [ , [ @with_log = ] { 'true' | 'false' } ]
    [ , [ @replace = ] 'replace' ]
[ ; ]
```

## Arguments

#### [ @msgnum = ] *msgnum*

The ID of the message. *@msgnum* is **int**, with a default of `NULL`. *@msgnum* for user-defined error messages can be an integer between 50,001 and 2,147,483,647. The combination of *@msgnum* and *@lang* must be unique; an error is returned if the ID already exists for the specified language.

#### [ @severity = ] *severity*

The severity level of the error. *@severity* is **smallint**, with a default of `NULL`. Valid levels are from `1` through `25`. For more information about severities, see [Database Engine error severities](../errors-events/database-engine-error-severities.md).

#### [ @msgtext = ] N'*msgtext*'

The text of the error message. *@msgtext* is **nvarchar(255)**, with a default of `NULL`.

#### [ @lang = ] N'*lang*'

The language for this message. *@lang* is **sysname**, with a default of `NULL`. Because multiple languages can be installed on the same server, *@lang* specifies the language in which each message is written. When *@lang* is omitted, the language is the default language for the session.

#### [ @with_log = ] '*with_log*'

Specifies whether the message is to be written to the Windows application log when it occurs. *@with_log* is **varchar(5)**, with a default of `NULL`.

- If `true`, the error is always written to the Windows application log.
- If `false`, the error isn't always written to the Windows application log but can be written, depending on how the error was raised.

Only members of the **sysadmin** server role can use this option.

If a message is written to the Windows application log, it's also written to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] error log file.

#### [ @replace = ] '*replace*'

If specified as the string *@replace*, an existing error message is overwritten with new message text and severity level. *@replace* is **varchar(7)**, with a default of `NULL`. This option must be specified if *@msgnum* already exists. If you replace a U.S. English message, the severity level is replaced for all messages in all other languages that have the same *@msgnum*.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

For non-English versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the U.S. English version of a message must already exist before the message can be added using another language. The severity of the two versions of the message must match.

When localizing messages that contain parameters, use parameter numbers that correspond to the parameters in the original message. Insert an exclamation point (!) after each parameter number.

| Original message | Localized message |
| --- | --- |
| `Original message param 1: %s,<br />param 2: %d` | `Localized message param 1: %1!,<br />param 2: %2!` |

Because of language syntax differences, the parameter numbers in the localized message might not occur in the same sequence as in the original message.

## Permissions

Requires membership in the **sysadmin** or **serveradmin** fixed server roles.

## Examples

### A. Define a custom message

The following example adds a custom message to `sys.messages`.

```sql
USE master;
GO
EXEC sp_addmessage 50001, 16,
    N'Percentage expects a value between 20 and 100.
    Re-run with a more appropriate value.';
GO
```

### B. Add a message in two languages

The following example first adds a message in U.S. English and then adds the same message in French.

```sql
USE master;
GO
EXEC sp_addmessage @msgnum = 60000, @severity = 16,
   @msgtext = N'The item named %s already exists in %s.',
   @lang = 'us_english';

EXEC sp_addmessage @msgnum = 60000, @severity = 16,
   @msgtext = N'L''élément nommé %1! existe déjà dans %2!',
   @lang = 'French';
GO
```

### C. Change the order of parameters

The following example first adds a message in U.S. English, and then adds a localized message in which the parameter order is changed. In the localized version of the message, the parameter order has changed. The string parameters are in first and second place in the message, and the numeric parameter is third place.

```sql
USE master;
GO

EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext =
        N'This is a test message with one numeric parameter (%d), one string parameter (%s), and another string parameter (%s).',
    @lang = 'us_english';

EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext =
        N'Dies ist eine Testmeldung mit einem Zeichenfolgenparameter (%3!), einem weiteren Zeichenfolgenparameter (%2!), und einem numerischen Parameter (%1!).',
    @lang = 'German';
GO

-- Changing the session language to use the U.S. English
-- version of the error message.
SET LANGUAGE us_english;
GO

RAISERROR(60000, 1, 1, 15, 'param1', 'param2')
GO

-- Changing the session language to use the German
-- version of the error message.
SET LANGUAGE German;
GO

RAISERROR(60000, 1, 1, 15, 'param1', 'param2');
GO
```

## Related content

- [RAISERROR (Transact-SQL)](../../t-sql/language-elements/raiserror-transact-sql.md)
- [sp_altermessage (Transact-SQL)](sp-altermessage-transact-sql.md)
- [sp_dropmessage (Transact-SQL)](sp-dropmessage-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
