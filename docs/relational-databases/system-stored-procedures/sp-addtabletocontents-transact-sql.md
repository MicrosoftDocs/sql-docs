---
title: "sp_addtabletocontents (Transact-SQL)"
description: Inserts references into the merge tracking tables for any rows in a source table that aren't currently included in the tracking tables.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addtabletocontents_TSQL"
  - "sp_addtabletocontents"
helpviewer_keywords:
  - "sp_addtabletocontents"
dev_langs:
  - "TSQL"
---
# sp_addtabletocontents (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

`sp_addtabletocontents` inserts references into the merge tracking tables, for any rows in a source table that aren't currently included in the tracking tables. Use this option if you bulk-loaded a large amount of data using **bcp**, which won't fire merge tracking triggers. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addtabletocontents
    [ @table_name = ] N'table_name'
    [ , [ @owner_name = ] N'owner_name' ]
    [ , [ @filter_clause = ] N'filter_clause' ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

The name of the table. *@table_name* is **sysname**, with no default.

#### [ @owner_name = ] N'*owner_name*'

The name of the owner of the table. *@owner_name* is **sysname**, with a default of `NULL`.

#### [ @filter_clause = ] N'*filter_clause*'

Specifies a filter clause that controls which rows of the newly loaded data should be added to the merge tracking tables. *@filter_clause* is **nvarchar(4000)**, with a default of `NULL`. If *@filter_clause* is `NULL`, all bulk loaded rows are added.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addtabletocontents` is used only in merge replication.

The rows in the *@table_name* are referred to by their `rowguidcol` and the references are added to the merge tracking tables. `sp_addtabletocontents` should be used after bulk copying data into a table that is published using merge replication. The stored procedure initiates tracking of the rows that were copied and ensures that the new rows will be included in the next synchronization.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addtabletocontents`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
