---
title: "sp_changedistributor_property (Transact-SQL)"
description: sp_changedistributor_property changes the properties of the Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedistributor_property_TSQL"
  - "sp_changedistributor_property"
helpviewer_keywords:
  - "sp_changedistributor_property"
dev_langs:
  - "TSQL"
---
# sp_changedistributor_property (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the properties of the Distributor. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changedistributor_property
    [ [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
[ ; ]
```

## Arguments

#### [ @property = ] N'*property*'

The property for a given Distributor. *@property* is **sysname**, and can be one of these values.

| Value | Description |
| --- | --- |
| `heartbeat_interval` | Maximum number of minutes that an agent can run without logging a progress message. |
| `NULL` (default) | All available *@property* values are printed. |

#### [ @value = ] N'*value*'

The value for the given Distributor property. *@value* is **nvarchar(255)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedistributor_property` is used in all types of replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-changedistributor-pro_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changedistributor_property`.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sp_adddistributor (Transact-SQL)](sp-adddistributor-transact-sql.md)
- [sp_dropdistributor (Transact-SQL)](sp-dropdistributor-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
