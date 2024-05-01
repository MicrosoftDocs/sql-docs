---
title: "sp_delete_log_shipping_secondary_database (Transact-SQL)"
description: Removes a secondary database and removes the local history and remote history.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_secondary_database_TSQL"
  - "sp_delete_log_shipping_secondary_database"
helpviewer_keywords:
  - "sp_delete_log_shipping_secondary_database"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_secondary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure removes a secondary database and removes the local history and remote history.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_log_shipping_secondary_database
    [ @secondary_database = ] N'secondary_database'
    [ , [ @ignoreremotemonitor = ] ignoreremotemonitor ]
[ ; ]
```

## Arguments

#### [ @secondary_database = ] N'*secondary_database*'

The name of the secondary database. *@secondary_database* is **sysname**, with no default.

#### [ @ignoreremotemonitor = ] *ignoreremotemonitor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_log_shipping_secondary_database` must be run from the `master` database on the secondary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
