---
title: "sp_requestpeerresponse (Transact-SQL)"
description: sp_requestpeerresponse requests a response from every other node in a peer-to-peer topology.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_requestpeerresponse_TSQL"
  - "sp_requestpeerresponse"
helpviewer_keywords:
  - "sp_requestpeerresponse"
dev_langs:
  - "TSQL"
---
# sp_requestpeerresponse (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When executed from a node in a peer-to-peer topology, this procedure requests a response from every other node in the topology. By executing this procedure and reviewing the corresponding responses, you can guarantee that all previous commands are delivered to the responding nodes. This stored procedure is executed at the requesting node on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_requestpeerresponse
    [ @publication = ] N'publication'
    [ , [ @description = ] N'description' ]
    [ , [ @request_id = ] request_id OUTPUT ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in a peer-to-peer topology for which the status is being verified. *@publication* is **sysname**, with no default.

#### [ @description = ] N'*description*'

User-defined information that can be used to identify individual status requests. *@description* is **nvarchar(4000)**, with a default of `NULL`.

#### [ @request_id = ] *request_id* OUTPUT

Returns the ID of the new request. *@request_id* is an OUTPUT parameter of type **int**. This value can be used when executing [sp_helppeerresponses](sp-helppeerresponses-transact-sql.md) to view all responses to a status request.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_requestpeerresponse` is used in peer-to-peer transactional replication.

`sp_requestpeerresponse` is used to ensure that all commands are received by all other nodes, before restoring a database published in a peer-to-peer topology. You can also use this stored procedure when replicating data definition language (DDL) changes made while a node was offline, to estimate when these changes arrive at the other nodes.

`sp_requestpeerresponse` can't be executed within a user-defined transaction.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_requestpeerresponse`.

## Related content

- [sp_deletepeerrequesthistory (Transact-SQL)](sp-deletepeerrequesthistory-transact-sql.md)
- [sp_helppeerrequests (Transact-SQL)](sp-helppeerrequests-transact-sql.md)
