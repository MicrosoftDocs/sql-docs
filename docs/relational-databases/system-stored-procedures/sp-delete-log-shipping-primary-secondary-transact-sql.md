---
title: "sp_delete_log_shipping_primary_secondary (Transact-SQL)"
description: Removes the entry for a secondary database on the primary server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_primary_secondary_TSQL"
  - "sp_delete_log_shipping_primary_secondary"
helpviewer_keywords:
  - "sp_delete_log_shipping_primary_secondary"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_primary_secondary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the entry for a secondary database on the primary server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_log_shipping_primary_secondary
    [ @primary_database = ] N'primary_database'
    , [ @secondary_server = ] N'secondary_server'
    , [ @secondary_database = ] N'secondary_database'
[ ; ]
```

## Arguments

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

#### [ @secondary_server = ] N'*secondary_server*'

The name of the secondary server. *@secondary_server* is **sysname**, with no default.

#### [ @secondary_database = ] N'*secondary_database*'

The name of the secondary database. *@secondary_database* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_log_shipping_primary_secondary` must be run from the `master` database on the primary server. This stored procedure removes the entry for a secondary database from `log_shipping_primary_secondaries` on the primary server.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

In the following example, `sp_delete_log_shipping_primary_secondary` is used to delete the secondary database `LogShipAdventureWorks` from the secondary server `FLATIRON`.

```sql
EXEC master.dbo.sp_delete_log_shipping_primary_secondary
    @primary_database = N'AdventureWorks',
    @secondary_server = N'FLATIRON',
    @secondary_database = N'LogShipAdventureWorks';
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
