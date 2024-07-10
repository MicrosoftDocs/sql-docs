---
title: "sp_changedistributiondb (Transact-SQL)"
description: sp_changedistributiondb changes the properties of the distribution database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedistributiondb_TSQL"
  - "sp_changedistributiondb"
helpviewer_keywords:
  - "sp_changedistributiondb"
dev_langs:
  - "TSQL"
---
# sp_changedistributiondb (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the properties of the distribution database. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changedistributiondb
    [ @database = ] N'database'
    [ , [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the distribution database. *@database* is **sysname**, with no default.

#### [ @property = ] N'*property*'

The property to change for the given database. *@property* is **sysname**, and can be one of these values.

| Value | Description |
| --- | --- |
| `history_retention` | History table retention period. |
| `max_distretention` | Maximum distribution retention period. |
| `min_distretention` | Minimum distribution retention period. |
| `NULL` (default) | All available *@property* values are printed. |

#### [ @value = ] N'*value*'

The new value for the specified property. *@value* is **nvarchar(255)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedistributiondb` is used in all types of replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-changedistributiondb-_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changedistributiondb`.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sp_adddistributiondb (Transact-SQL)](sp-adddistributiondb-transact-sql.md)
- [sp_dropdistributiondb (Transact-SQL)](sp-dropdistributiondb-transact-sql.md)
- [sp_helpdistributiondb (Transact-SQL)](sp-helpdistributiondb-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
