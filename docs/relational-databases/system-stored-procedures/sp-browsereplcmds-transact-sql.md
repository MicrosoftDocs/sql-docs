---
title: "sp_browsereplcmds (Transact-SQL)"
description: Returns a result set in a readable version of the replicated commands stored in the distribution database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_browsereplcmds_TSQL"
  - "sp_browsereplcmds"
helpviewer_keywords:
  - "sp_browsereplcmds"
dev_langs:
  - "TSQL"
---
# sp_browsereplcmds (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a result set in a readable version of the replicated commands stored in the distribution database, and is used as a diagnostic tool. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_browsereplcmds
    [ [ @xact_seqno_start = ] N'xact_seqno_start' ]
    [ , [ @xact_seqno_end = ] N'xact_seqno_end' ]
    [ , [ @originator_id = ] originator_id ]
    [ , [ @publisher_database_id = ] publisher_database_id ]
    [ , [ @article_id = ] article_id ]
    [ , [ @command_id = ] command_id ]
    [ , [ @agent_id = ] agent_id ]
    [ , [ @compatibility_level = ] compatibility_level ]
[ ; ]
```

## Arguments

#### [ @xact_seqno_start = ] N'*xact_seqno_start*'

Specifies the lowest valued exact sequence number to return. *@xact_seqno_start* is **nchar(22)**, with a default of `0x00000000000000000000`.

#### [ @xact_seqno_end = ] N'*xact_seqno_end*'

Specifies the highest exact sequence number to return. *@xact_seqno_end* is **nchar(22)**, with a default of `0xFFFFFFFFFFFFFFFFFFFF`.

#### [ @originator_id = ] *originator_id*

Specifies if commands with the specified *originator_id* are returned. *@originator_id* is **int**, with a default of `NULL`.

#### [ @publisher_database_id = ] *publisher_database_id*

Specifies if commands with the specified *@publisher_database_id* are returned. *@publisher_database_id* is **int**, with a default of `NULL`.

#### [ @article_id = ] *article_id*

Specifies if commands with the specified *@article_id* are returned. *@article_id* is **int**, with a default of `NULL`.

#### [ @command_id = ] *command_id*

The location of the command in [MSrepl_commands](../system-tables/msrepl-commands-transact-sql.md) to be decoded. *@command_id* is **int**, with a default of `NULL`. If specified, all other parameters must be specified also, and *@xact_seqno_start* must be identical to *@xact_seqno_end*.

#### [ @agent_id = ] *agent_id*

Specifies that only commands for a specific replication agent are returned. *@agent_id* is **int**, with a default of `NULL`.

#### [ @compatibility_level = ] *compatibility_level*

Specifies the compatibility level of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. *@compatibility_level* is **int**, with a default of `9000000`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `xact_seqno` | **varbinary(16)** | Sequence number of the command. |
| `originator_srvname` | **sysname** | Server where the transaction originated. |
| `originator_db` | **sysname** | Database where the transaction originated. |
| `article_id` | **int** | ID of the article. |
| `type` | **int** | Type of command. |
| `partial_command` | **bit** | Indicates whether this is a partial command. |
| `hashkey` | **int** | Internal use only. |
| `originator_publication_id` | **int** | ID of the publication where the transaction originated. |
| `originator_db_version` | **int** | Version of the database where the transaction originated. |
| `originator_lsn` | **varbinary(16)** | Identifies the log sequence number (LSN) for the command in the originating publication. Used in peer-to-peer transactional replication. |
| `command` | **nvarchar(1024)** | [!INCLUDE [tsql](../../includes/tsql-md.md)] command. |
| `command_id` | **int** | ID of the command in [MSrepl_commands](../system-tables/msrepl-commands-transact-sql.md). |

Long commands can be split across several rows in the result sets.

## Remarks

`sp_browsereplcmds` is used in transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role or members of the **db_owner** or **replmonitor** fixed database roles on the distribution database can execute `sp_browsereplcmds`.

## Related content

- [sp_replcmds (Transact-SQL)](sp-replcmds-transact-sql.md)
- [sp_replshowcmds (Transact-SQL)](sp-replshowcmds-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
