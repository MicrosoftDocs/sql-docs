---
title: "sys.database_automatic_tuning_mode (Transact-SQL)"
description: Learn how to view automatic tuning mode on SQL Server or Azure SQL Database.
author: "danimir"
ms.author: "danil"
ms.reviewer: wiassaf, randolphwest
ms.date: 12/08/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "database_automatic_tuning_mode_tsql"
  - "database_automatic_tuning_mode"
  - "sys.database_automatic_tuning_mode_tsql"
  - "sys.database_automatic_tuning_mode"
helpviewer_keywords:
  - "database_automatic_tuning_mode catalog view"
  - "sys.database_automatic_tuning_mode catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_automatic_tuning_mode (Transact-SQL)

[!INCLUDE[sqlserver2017-asdb-asdbmi](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

  Returns the automatic tuning mode for this database. Refer to [ALTER DATABASE SET AUTOMATIC_TUNING (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_tuning) for available options.

| Column name | Data type | Description |
| --- | --- | --- |
| **desired_state** | **smallint** | Desired state of the automatic tuning mode. |
| **desired_state_desc** | **nvarchar(60)** | Textual description of the desired operation mode of automatic tuning. |
| **actual_state** | **smallint** | Indicates the operation mode of automatic tuning mode. |
| **actual_state_desc** | **nvarchar(60)** | Textual description of the actual operation mode of automatic tuning. |

## Permissions

 Requires the `VIEW DATABASE STATE` permission.

## See also

- [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)
- [ALTER DATABASE SET AUTOMATIC_TUNING (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [sys.database_query_store_options (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [sys.dm_db_tuning_recommendations (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
- [sys.database_automatic_tuning_options](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
