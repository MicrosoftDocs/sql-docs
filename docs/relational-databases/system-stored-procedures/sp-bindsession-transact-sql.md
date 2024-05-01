---
title: "sp_bindsession (Transact-SQL)"
description: sp_bindsession binds or unbinds a session to other sessions in the same instance of the SQL Server Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_bindsession"
  - "sp_bindsession_TSQL"
helpviewer_keywords:
  - "sp_bindsession"
dev_langs:
  - "TSQL"
---
# sp_bindsession (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Binds or unbinds a session to other sessions in the same instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. Binding sessions allows two or more sessions to participate in the same transaction and share locks until a `ROLLBACK TRANSACTION` or `COMMIT TRANSACTION` is issued.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Multiple Active Results Sets (MARS) or distributed transactions instead. For more information, see [Using Multiple Active Result Sets (MARS) in SQL Server Native Client](../native-client/features/using-multiple-active-result-sets-mars.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_bindsession { 'bind_token' | NULL }
```

## Arguments

#### '*bind_token*'

The token that identifies the transaction originally obtained by using `sp_getbindtoken` or the Open Data Services `srv_getbindtoken` function. *bind_token* is **varchar(255)**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Two sessions that are bound share only a transaction and locks. Each session retains its own isolation level, and setting a new isolation level on one session doesn't affect the isolation level of the other session. Each session remains identified by its security account and can only access the database resources to which the account is granted permission.

`sp_bindsession` uses a bind token to bind two or more existing client sessions. These client sessions must be on the same instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] from which the binding token was obtained. A session is a client executing a command. Bound database sessions share a transaction and lock space.

A bind token obtained from one instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can't be used for a client session connected to another instance, even for DTC transactions. A bind token is valid only locally inside each instance and can't be shared across multiple instances. To bind client sessions on another instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], you must obtain a different bind token by executing `sp_getbindtoken`.

`sp_bindsession` fails with an error if it uses a token that isn't active.

Unbind from a session either by using `sp_bindsession` without specifying *bind_token* or by passing `NULL` in *bind_token*.

## Permissions

Requires membership in the **public** role.

## Examples

The following example binds the specified bind token to the current session.

> [!NOTE]  
> The bind token shown in the example was obtained by executing `sp_getbindtoken` before executing `sp_bindsession`.

```sql
USE master;
GO
EXEC sp_bindsession 'BP9---5---->KB?-V'<>1E:H-7U-]ANZ';
GO
```

## Related content

- [sp_getbindtoken (Transact-SQL)](sp-getbindtoken-transact-sql.md)
- [srv_getbindtoken (Extended Stored Procedure API)](../extended-stored-procedures-reference/srv-getbindtoken-extended-stored-procedure-api.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
