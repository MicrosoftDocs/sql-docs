---
title: "sp_dropmessage (Transact-SQL)"
description: Drops a specified user-defined error message from an instance of the Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropmessage_TSQL"
  - "sp_dropmessage"
helpviewer_keywords:
  - "sp_dropmessage"
dev_langs:
  - "TSQL"
---
# sp_dropmessage (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a specified user-defined error message from an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. User-defined messages can be viewed using the `sys.messages` catalog view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmessage
    [ [ @msgnum = ] msgnum ]
    [ , [ @lang = ] N'lang' ]
[ ; ]
```

## Arguments

#### [ @msgnum = ] *msgnum*

The message number to drop. *@msgnum* is **int**, with a default of `NULL`. *@msgnum* must be a user-defined message that's a message number greater than `50000` (50,000).

#### [ @lang = ] N'*lang*'

The language of the message to drop. *@lang* is **sysname**, with a default of `NULL`. If `all` is specified, all language versions of *@msgnum* are dropped.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Requires membership in the **sysadmin** and **serveradmin** fixed server roles.

## Remarks

Unless `all` is specified for *@lang*, all localized versions of a message must be dropped before the U.S. English version of the message can be dropped.

## Examples

### A. Drop a user-defined message

The following example drops a user-defined message, number `50001`, from `sys.messages`.

```sql
USE master;
GO
EXEC sp_dropmessage 50001;
```

### B. Drop a user-defined message that includes a localized version

The following example adds a user-defined message, number `60000`, which includes a localized version, then drops both variations of the message.

```sql
USE master;
GO

-- Create a user-defined message in U.S. English
EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext = N'The item named %s already exists in %s.',
    @lang = 'us_english';

-- Create a localized version of the same message.
EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext = N'L''élément nommé %1! existe déjà dans %2!',
    @lang = 'French';
GO

-- This statement will fail as long as the localized version
-- of the message exists.
EXEC sp_dropmessage 60000;
GO

-- This statement will drop the message.
EXEC sp_dropmessage @msgnum = 60000,
    @lang = 'all';
GO
```

### C. Drop a localized version of a user-defined message

The following example drops a localized version of a user-defined message, number `60000`, without dropping the whole message.

```sql
USE master;
GO

-- Create a user-defined message in U.S. English
EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext = N'The item named %s already exists in %s.',
    @lang = 'us_english';

-- Create a localized version of the same message.
EXEC sp_addmessage
    @msgnum = 60000,
    @severity = 16,
    @msgtext = N'L''élément nommé %1! existe déjà dans %2!',
    @lang = 'French';
GO

-- This statement will remove only the localized version of the
-- message.
EXEC sp_dropmessage
    @msgnum = 60000,
    @lang = 'French';
GO
```

## Related content

- [RAISERROR (Transact-SQL)](../../t-sql/language-elements/raiserror-transact-sql.md)
- [sp_addmessage (Transact-SQL)](sp-addmessage-transact-sql.md)
- [sp_altermessage (Transact-SQL)](sp-altermessage-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](../../t-sql/functions/formatmessage-transact-sql.md)
- [Messages (for errors) Catalog Views - sys.messages](../system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
