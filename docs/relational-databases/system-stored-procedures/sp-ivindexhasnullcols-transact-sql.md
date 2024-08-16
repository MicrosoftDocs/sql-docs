---
title: "sp_ivindexhasnullcols (Transact-SQL)"
description: sp_ivindexhasnullcols validates that the clustered index of the indexed view is unique.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_ivindexhasnullcols"
  - "sp_ivindexhasnullcols_TSQL"
helpviewer_keywords:
  - "sp_ivindexhasnullcols"
dev_langs:
  - "TSQL"
---
# sp_ivindexhasnullcols (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Validates that the clustered index of the indexed view is unique, and doesn't contain any column that can be `NULL` when the indexed view is going to be used to create a transactional publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_ivindexhasnullcols
    [ @viewname = ] N'viewname'
    , [ @fhasnullcols = ] fhasnullcols OUTPUT
[ ; ]
```

## Arguments

#### [ @viewname = ] N'*viewname*'

The name of the view to verify. *@viewname* is **sysname**, with no default.

#### [ @fhasnullcols = ] *fhasnullcols* OUTPUT

The flag indicating whether the view index has columns that allow `NULL`. *@fhasnullcols* is an OUTPUT parameter of type **bit**.

- Returns a value of `1` if the view index has columns that allow `NULL`.
- Returns a value of `0` if the view doesn't contain columns that allow `NULL`.

> [!NOTE]  
> If the stored procedure itself returns a return code of `1`, meaning the stored procedure execution had a failure, this value is `0` and should be ignored.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_ivindexhasnullcols` is used by transactional replication.

By default, indexed view articles in a publication are created as tables at the Subscribers. However, when the indexed column allows `NULL` values, the indexed view is created as an indexed view at the Subscriber instead of a table. This stored procedure can alert the user to whether or not this problem exists with the current indexed view.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_ivindexhasnullcols`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
