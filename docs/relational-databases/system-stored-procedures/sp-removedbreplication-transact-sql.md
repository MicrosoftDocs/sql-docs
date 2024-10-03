---
title: "sp_removedbreplication (T-SQL)"
description: sp_removedbreplication removes all replication objects on the publication database on the Publisher instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_removedbreplication"
  - "sp_removedbreplication_TSQL"
helpviewer_keywords:
  - "sp_removedbreplication"
dev_langs:
  - "TSQL"
---
# sp_removedbreplication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This stored procedure removes all replication objects on the publication database on the Publisher instance of SQL Server, or on the subscription database on the Subscriber instance of SQL Server. Execute `sp_removedbreplication` in the appropriate database, or, if the execution is in the context of another database on the same instance, specify the database where the replication objects should be removed. This procedure doesn't remove objects from other databases, such as the distribution database.

This procedure should be used only if other methods of removing replication objects have failed.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_removedbreplication
    [ [ @dbname = ] N'dbname' ]
    [ , [ @type = ] N'type' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database. *@dbname* is **sysname**, with a default of `NULL`. When `NULL`, the current database is used.

#### [ @type = ] N'*type*'

The type of replication for which database objects are being removed. *@type* is **nvarchar(5)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `tran` | Removes transactional replication publishing objects. |
| `merge` | Removes merge replication publishing objects. |
| `both` (default) | Removes all replication publishing objects. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_removedbreplication` is used in all types of replication.

`sp_removedbreplication` is useful when restoring a replicated database that's no replication objects needing to be restored.

`sp_removedbreplication` can't be used against a database that is marked as read-only.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_removedbreplication`.

## Examples

Remove replication objects in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] replica subscription database.

:::code language="sql" source="../replication/codesnippet/tsql/sp-removedbreplication-t_1.sql":::

## Related content

- [Disable Publishing and Distribution](../replication/disable-publishing-and-distribution.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
