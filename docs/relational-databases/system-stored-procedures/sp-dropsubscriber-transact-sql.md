---
title: "sp_dropsubscriber (Transact-SQL)"
description: sp_dropsubscriber removes the Subscriber designation from a registered server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropsubscriber_TSQL"
  - "sp_dropsubscriber"
helpviewer_keywords:
  - "sp_dropsubscriber"
dev_langs:
  - "TSQL"
---
# sp_dropsubscriber (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes the Subscriber designation from a registered server. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> This stored procedure has been deprecated. You're no longer required to explicitly register a Subscriber at the Publisher.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropsubscriber
    [ @subscriber = ] N'subscriber'
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber to be dropped. *@subscriber* is **sysname**, with no default.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @ignore_distributor = ] *ignore_distributor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropsubscriber` is used in all types of replication.

This stored procedure removes the server `sub` option, and removes the remote login mapping of system administrator to **repl_subscriber**.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_dropsubscriber`.

## Related content

- [Delete a Push Subscription](../replication/delete-a-push-subscription.md)
- [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md)
- [sp_addsubscriber (Transact-SQL)](sp-addsubscriber-transact-sql.md)
- [sp_changesubscriber (Transact-SQL)](sp-changesubscriber-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [sp_helpsubscriberinfo (Transact-SQL)](sp-helpsubscriberinfo-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
