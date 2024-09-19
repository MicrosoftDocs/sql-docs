---
title: "sys.database_automatic_tuning_configuration (Transact-SQL)"
description: sys.database_automatic_tuning_configuration returns the automatic tuning configuration settings that are enabled for the current database.
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 09/18/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "database_automatic_tuning_configuration_tsql"
  - "database_automatic_tuning_configuration"
  - "sys.database_automatic_tuning_configuration_tsql"
  - "sys.database_automatic_tuning_configuration"
helpviewer_keywords:
  - "database_automatic_tuning_configuration catalog view"
  - "sys.database_automatic_tuning_configuration catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azuresqldb-current"
---
# sys.database_automatic_tuning_configuration (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Returns the [Automatic plan correction](../automatic-tuning/automatic-tuning.md#automatic-plan-correction) component of the [Automatic tuning](../automatic-tuning/automatic-tuning.md) configuration settings that are enabled for the current database.

| Column name | Data type | Description |
| --- | --- | --- |
| `option` | **nvarchar(60)** | The name of the automatic tuning configuration option. See [sp_configure_automatic_tuning](../system-stored-procedures/sp-configure-automatic-tuning-transact-sql.md) for the list of available configuration options. |
| `option_value` | **nvarchar(60)** | Indicates the desired option for the automatic tuning configuration. |
| `type` | **nvarchar(60)** | Describes the automatic tuning configuration target type. |
| `type_value` | **nvarchar(60)** | Indicates the query ID from Query Store that the automatic tuning configuration option is operating on. |
| `details` | **nvarchar(4000)** | Textual description of the automatic tuning configuration option. |
| `state` | **bit** | Indicates the state of the automatic tuning configuration option. |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Related content

- [Automatic tuning](../automatic-tuning/automatic-tuning.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [sys.database_query_store_options (Transact-SQL)](sys-database-query-store-options-transact-sql.md)
- [sys.dm_db_tuning_recommendations (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
- [sys.database_automatic_tuning_mode](sys-database-automatic-tuning-mode-transact-sql.md)
- [sp_configure_automatic_tuning (Transact-SQL)](../system-stored-procedures/sp-configure-automatic-tuning-transact-sql.md)
