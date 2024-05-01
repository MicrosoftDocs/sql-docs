---
title: "sp_altermessage (Transact-SQL)"
description: Alters the state of user-defined or system messages in an instance of the SQL Server Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_altermessage_TSQL"
  - "sp_altermessage"
helpviewer_keywords:
  - "sp_altermessage"
dev_langs:
  - "TSQL"
---
# sp_altermessage (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Alters the state of user-defined or system messages in an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. User-defined messages can be viewed using the `sys.messages` catalog view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_altermessage
    [ @message_id = ] message_id
    , [ @parameter = ] N'parameter'
    , [ @parameter_value = ] 'parameter_value'
[ ; ]
```

## Arguments

#### [ @message_id = ] *message_id*

The error number of the message to alter from `sys.messages`. *@message_id* is **int**, with no default.

#### [ @parameter = ] N'*parameter*'

Used with *@parameter_value* to indicate that the message is to be written to the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows application log. *@parameter* is **sysname**, with no default.

*@parameter* must be set to `WITH_LOG` or `NULL`. If *@parameter* is set to `WITH_LOG` or `NULL`, and the value for *@parameter_value* is `true`, the message is written to the Windows application log. If *@parameter* is set to `WITH_LOG` or `NULL` and the value for *@parameter_value* is `false`, the message isn't always written to the Windows application log, but might be written depending upon how the error was raised.

If a message is written to the Windows application log, it is also written to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] error log file.

If *@parameter* is specified, *@parameter_value* must also be specified.

#### [ @parameter_value = ] '*parameter_value*'

Used with *@parameter* to indicate that the error is to be written to the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows application log. *@parameter_value* is **varchar(5)**, with no default.

- If `true`, the error is always written to the Windows application log.
- If `false`, the error isn't always written to the Windows application log, but might be written depending upon how the error was raised.

If *@parameter_value* is specified, *@parameter* must also be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The effect of `sp_altermessage` with the `WITH_LOG` option is similar to that of the `RAISERROR WITH LOG` parameter, except that `sp_altermessage` changes the logging behavior of an existing message. If a message is altered to be `WITH_LOG`, it is always written to the Windows application log, regardless of how a user invokes the error. Even if `RAISERROR` is executed without the `WITH_LOG` option, the error is written to the Windows application log.

System messages can be modified by using `sp_altermessage`.

## Permissions

Requires membership in the **serveradmin** fixed server role.

## Examples

The following example writes the existing message `55001` to the Windows application log.

```sql
EXEC sp_altermessage 55001, 'WITH_LOG', 'true';
GO
```

## Related content

- [RAISERROR (Transact-SQL)](../../t-sql/language-elements/raiserror-transact-sql.md)
- [sp_addmessage (Transact-SQL)](sp-addmessage-transact-sql.md)
- [sp_dropmessage (Transact-SQL)](sp-dropmessage-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
