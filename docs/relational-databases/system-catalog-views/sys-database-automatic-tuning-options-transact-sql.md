---
title: "sys.database_automatic_tuning_options (Transact-SQL)"
description: Learn how to view automatic tuning options on SQL Server or Azure SQL Database. See required permissions and view additional available resources.
author: "danimir"
ms.author: "danil"
ms.reviewer: randolphwest
ms.date: 11/04/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "database_automatic_tuning_options_tsql"
  - "database_automatic_tuning_options"
  - "sys.database_automatic_tuning_options_tsql"
  - "sys.database_automatic_tuning_options"
helpviewer_keywords:
  - "database_automatic_tuning_options catalog view"
  - "sys.database_automatic_tuning_options catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_automatic_tuning_options (Transact-SQL)

[!INCLUDE[sqlserver2017-asdb-asdbmi](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

  Returns the Automatic Tuning options for this database.

| Column name | Data type | Description |
| --- | --- | --- |
| **name** | **nvarchar(128)** | The name of the automatic tuning option. Refer to [ALTER DATABASE SET AUTOMATIC_TUNING (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_tuning) for available options. |
| **desired_state** | **smallint** | Indicates the desired operation mode for Automatic Tuning option, explicitly set by user.<br />0 = OFF<br />1 = ON<br />2 = DEFAULT |
| **desired_state_desc** | **nvarchar(60)** | Textual description of the desired operation mode of Automatic Tuning option.<br />OFF<br />ON<br />DEFAULT |
| **actual_state** | **smallint** | Indicates the operation mode of Automatic Tuning option.<br />0 = OFF<br />1 = ON |
| **actual_state_desc** | **nvarchar(60)** | Textual description of the actual operation mode of Automatic Tuning option.<br />OFF<br />ON |
| **reason** | **smallint** | Indicates why actual and desired states are different.<br />2 = DISABLED<br />11 = QUERY_STORE_OFF<br />12 = QUERY_STORE_READ_ONLY<br />13 = NOT_SUPPORTED |
| **reason_desc** | **nvarchar(60)** | Textual description of the reason why actual and desired states are different.<br />DISABLED = Option is disabled by system<br />QUERY_STORE_OFF = Query Store is turned off<br />QUERY_STORE_READ_ONLY = Query Store is in read-only mode<br />NOT_SUPPORTED = Available only in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition |

## Permissions

 Requires the `VIEW DATABASE STATE` permission.

## See also

- [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)
- [ALTER DATABASE SET AUTOMATIC_TUNING (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [sys.database_query_store_options (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [sys.dm_db_tuning_recommendations (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
- [sys.database_automatic_tuning_mode](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-mode-transact-sql.md)
