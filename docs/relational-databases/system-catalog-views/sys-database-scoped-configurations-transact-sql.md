---
title: sys.database_scoped_configurations (Transact-SQL)
description: sys.database_scoped_configurations contains one row per database scoped configuration.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "database_scoped_configurations"
  - "database_scoped_configurations_TSQL"
  - "sys.database_scoped_configurations"
  - "sys.database_scoped_configurations_TSQL"
helpviewer_keywords:
  - "sys.database_scoped_configurations catalog view"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# sys.database_scoped_configurations (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabric](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `sys.database_scoped_configurations` catalog view contains one row per configuration.

| Column name | Data type | Description |
| --- | --- | --- |
| `configuration_id` | **int** | ID of the configuration option. |
| `name` | **nvarchar(60)** | The name of the configuration option. For information about the possible configurations, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). |
| `value` | **sqlvariant** | The value set for this configuration option for the primary replica. |
| `value_for_secondary` | **sqlvariant** | The value set for this configuration option for the secondary replicas. |
| `is_value_default` | **bit** | Specifies whether the value set is the default value. Added in SQL Server 2017. |

<a id="Permissions"></a>

## Permissions

Requires membership in the **public** fixed database role.

## Remarks

When `NULL` is returned as the value for `value_for_secondary`, this means that the secondary is set to `PRIMARY`.

Database scoped configuration settings will be carried over with the database. This means that when a given database is restored or attached, the existing configuration settings remain.

## Related content

- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
