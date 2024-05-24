---
title: "sp_help_log_shipping_primary_secondary (Transact-SQL)"
description: Returns information regarding all the secondary databases for a given primary database.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_primary_secondary"
  - "sp_help_log_shipping_primary_secondary_TSQL"
helpviewer_keywords:
  - "sp_help_log_shipping_primary_secondary"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_primary_secondary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure returns information regarding all the secondary databases for a given primary database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_primary_secondary
    [ @primary_database = ] N'primary_database'
[ ; ]
```

## Arguments

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Description |
| --- | --- |
| `secondary_server` | The name of the secondary instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] in the log shipping configuration. |
| `secondary_database` | The name of the secondary database in the log shipping configuration. |

## Remarks

`sp_help_log_shipping_primary_secondary` must be run from the `master` database on the primary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example illustrates using `sp_help_log_shipping_primary_secondary` to retrieve a list of secondary databases associate with the primary database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)].

```sql
EXEC master.dbo.sp_help_log_shipping_primary_secondary
    @primary_database=N'AdventureWorks';
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
