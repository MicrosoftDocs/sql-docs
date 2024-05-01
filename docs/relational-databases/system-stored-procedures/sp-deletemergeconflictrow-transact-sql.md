---
title: "sp_deletemergeconflictrow (Transact-SQL)"
description: "Deletes rows from a conflict table or the MSmerge_conflicts_info table."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_deletemergeconflictrow"
  - "sp_deletemergeconflictrow_TSQL"
helpviewer_keywords:
  - "sp_deletemergeconflictrow"
dev_langs:
  - "TSQL"
---
# sp_deletemergeconflictrow (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes rows from a conflict table or the [MSmerge_conflicts_info (Transact-SQL)](../system-tables/msmerge-conflicts-info-transact-sql.md) table. This stored procedure is executed at the computer where the conflict table is stored, in any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_deletemergeconflictrow
    [ [ @conflict_table = ] N'conflict_table' ]
    [ , [ @source_object = ] N'source_object' ]
    , [ @rowguid = ] 'rowguid'
    , [ @origin_datasource = ] 'origin_datasource'
    [ , [ @drop_table_if_empty = ] 'drop_table_if_empty' ]
[ ; ]
```

## Arguments

#### [ @conflict_table = ] N'*conflict_table*'

The name of the conflict table. *@conflict_table* is **sysname**, with a default of `%`. If the *@conflict_table* is specified as `NULL` or `%`, the conflict is assumed to be a delete conflict and the row matching *@rowguid*, *@origin_datasource*, and *@source_object*, is deleted from the [MSmerge_conflicts_info (Transact-SQL)](../system-tables/msmerge-conflicts-info-transact-sql.md) table.

#### [ @source_object = ] N'*source_object*'

The name of the source table. *@source_object* is **nvarchar(386)**, with a default of `NULL`.

#### [ @rowguid = ] '*rowguid*'

The row identifier for the delete conflict. *@rowguid* is **uniqueidentifier**, with no default.

#### [ @origin_datasource = ] '*origin_datasource*'

The origin of the conflict. *@origin_datasource* is **varchar(255)**, with no default.

#### [ @drop_table_if_empty = ] '*drop_table_if_empty*'

A flag indicating that the *@conflict_table* is to be dropped if it's empty. *@drop_table_if_empty* is **varchar(10)**, with a default of `false`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_deletemergeconflictrow` is used in merge replication.

[MSmerge_conflicts_info (Transact-SQL)](../system-tables/msmerge-conflicts-info-transact-sql.md) table is a system table and isn't deleted from the database, even if it's empty.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_deletemergeconflictrow`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
