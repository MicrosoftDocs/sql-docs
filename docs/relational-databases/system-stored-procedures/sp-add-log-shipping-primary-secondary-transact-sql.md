---
title: "sys.sp_add_log_shipping_primary_secondary (Transact-SQL)"
description: "This stored procedure adds an entry for a secondary database on the primary server."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_log_shipping_primary_secondary_TSQL"
  - "sp_add_log_shipping_primary_secondary"
helpviewer_keywords:
  - "sp_add_log_shipping_primary_secondary"
dev_langs:
  - "TSQL"
---
# sys.sp_add_log_shipping_primary_secondary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure adds an entry for a secondary database on the primary server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_add_log_shipping_primary_secondary
    [ @primary_database = ] N'primary_database'
    , [ @secondary_server = ] N'secondary_server'
    , [ @secondary_database = ] N'secondary_database'
    [ , [ @overwrite = ] overwrite ]
[ ; ]
```

## Arguments

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

#### [ @secondary_server = ] N'*secondary_server*'

The name of the secondary server. *@secondary_server* is **sysname**, with no default.

#### [ @secondary_database = ] N'*secondary_database*'

The name of the secondary database. *@secondary_database* is **sysname**, with no default.

#### [ @overwrite = ] *overwrite*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_log_shipping_primary_secondary` must be run from the `master` database on the primary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example uses `sp_add_log_shipping_primary_secondary` to add an entry for the secondary database `LogShipAdventureWorks` to the secondary server `FLATIRON`.

```sql
EXECUTE master.dbo.sp_add_log_shipping_primary_secondary
    @primary_database = N'AdventureWorks',
    @secondary_server = N'flatiron',
    @secondary_database = N'LogShipAdventureWorks';
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
