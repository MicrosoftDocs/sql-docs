---
title: "sp_syscollector_set_warehouse_database_name (Transact-SQL)"
description: Specifies the database name defined in the connection string used to connect to the management data warehouse.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_set_warehouse_database_name"
  - "sp_syscollector_set_warehouse_database_name_TSQL"
helpviewer_keywords:
  - "sp_syscollector_set_warehouse_database_name"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_set_warehouse_database_name (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the database name defined in the connection string used to connect to the management data warehouse.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_set_warehouse_database_name [ [ @database_name = ] N'database_name' ]
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

The name of the management data warehouse. *@database_name* is **sysname**, with a default of an empty string.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must disable the data collector before changing the data collector-wide configuration. This procedure fails if the data collector is enabled.

To view the current database name, query the [syscollector_config_store](../system-catalog-views/syscollector-config-store-transact-sql.md) system view.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example sets the name of the management data warehouse to `RemoteMDW`.

```sql
USE msdb;
GO
EXEC sp_syscollector_set_warehouse_database_name N'RemoteMDW';
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
