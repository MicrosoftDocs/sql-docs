---
title: "sp_script_synctran_commands (Transact-SQL)"
description: Generates a script that contains the sp_addsynctrigger calls to be applied at Subscribers for updatable subscriptions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_script_synctran_commands"
  - "sp_script_synctran_commands_TSQL"
helpviewer_keywords:
  - "sp_script_synctran_commands"
dev_langs:
  - "TSQL"
---
# sp_script_synctran_commands (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Generates a script that contains the `sp_addsynctrigger` calls to be applied at Subscribers for updatable subscriptions. There's one `sp_addsynctrigger` call for each article in the publication. The generated script also contains the `sp_addqueued_artinfo` calls that create the `MSsubsciption_articles` table that is needed to process queued publications. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_script_synctran_commands
    [ @publication = ] N'publication'
    [ , [ @article = ] N'article' ]
    [ , [ @trig_only = ] trig_only ]
    [ , [ @usesqlclr = ] usesqlclr ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to be scripted. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article to be scripted. *@article* is **sysname**, with a default of `all`, which specifies all articles are scripted.

#### [ @trig_only = ] *trig_only*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @usesqlclr = ] *usesqlclr*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result sets

`sp_script_synctran_commands` returns a result set that consists of a single **nvarchar(4000)** column. The result set forms the complete scripts necessary to create both the `sp_addsynctrigger` and `sp_addqueued_artinfo` calls to be applied at Subscribers.

## Remarks

`sp_script_synctran_commands` is used in snapshot and transactional replication.

`sp_addqueued_artinfo` is used for queued updatable subscriptions.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_script_synctran_commands`.

## Related content

- [sp_addsynctriggers (Transact-SQL)](sp-addsynctriggers-transact-sql.md)
- [sp_addqueued_artinfo (Transact-SQL)](sp-addqueued-artinfo-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
