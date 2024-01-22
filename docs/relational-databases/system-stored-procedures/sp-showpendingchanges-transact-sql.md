---
title: "sp_showpendingchanges (Transact-SQL)"
description: sp_showpendingchanges returns a result set showing the changes that are waiting to be replicated.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_showpendingchanges"
  - "sp_showpendingchanges_TSQL"
helpviewer_keywords:
  - "sp_showpendingchanges"
dev_langs:
  - "TSQL"
---
# sp_showpendingchanges (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a result set showing the changes that are waiting to be replicated. This stored procedure is executed at the Publisher on the publication database and at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]  
> This procedure provides an approximation of the number of changes and the rows that are involved in those changes. For example, the procedure retrieves information from either the Publisher or Subscriber, but not both at the same time. Information that is stored at the other node might result in a smaller set of changes to synchronize than the procedure estimates.

## Syntax

```syntaxsql
sp_showpendingchanges
    [ [ @destination_server = ] N'destination_server' ]
    [ , [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
    [ , [ @show_rows = ] show_rows ]
[ ; ]
```

## Arguments

#### [ @destination_server = ] N'*destination_server*'

The name of the server where the replicated changes are applied. *@destination_server* is **sysname**, with a default of `NULL`.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `NULL`. When *@publication* is specified, results are limited only to the specified publication.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with a default of `NULL`. When *@article* is specified, results are limited only to the specified article.

#### [ @show_rows = ] *show_rows*

Specifies whether the result set contains more specific information about pending changes. *@show_rows* is **int**, with a default of `0`. If a value of `1` is specified, the result set contains the columns `is_delete` and `rowguid`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `destination_server` | **sysname** | The name of the server to which the changes are being replicated. |
| `pub_name` | **sysname** | The name of the publication. |
| `destination_db_name` | **sysname** | The name of the database to which the changes are being replicated. |
| `is_dest_subscriber` | **bit** | Indicates of the changes are being replicated to a Subscriber. A value of `1` indicates that the changes are being replicated to a Subscriber. `0` means that changes are being replicated to a Publisher. |
| `article_name` | **sysname** | The name of the article for the table where changes originated. |
| `pending_deletes` | **int** | The number of deletes waiting to be replicated. |
| `pending_ins_and_upd` | **int** | The number of inserts and updates waiting to be replicated. |
| `is_delete` | **bit** | Indicates whether the pending change is a delete. A value of `1` indicates that the change is a delete. Requires a value of `1` for *@show_rows*. |
| `rowguid` | **uniqueidentifier** | The GUID that identifies the row that changed. Requires a value of `1` for *@show_rows*. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_showpendingchanges` is used in merge replication.

`sp_showpendingchanges` is used when troubleshooting merge replication.

The result of `sp_showpendingchanges` doesn't include rows in generation 0.

When an article specified for *@article* doesn't belong to the publication specified for *@publication,* a count of `0` is returned for `pending_deletes` and `pending_ins_and_upd`.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_showpendingchanges`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
