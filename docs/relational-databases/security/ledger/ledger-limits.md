---
title: "Ledger considerations and limitations"
description: Limitations and considerations for the ledger feature
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.custom:
- event-tier1-build-2022
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Ledger considerations and limitations

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

There are some considerations and limitations to be aware of when working with ledger tables due to the nature of system-versioning and immutable data.

## General considerations and limitations

Consider the following when working with ledger.

- A [ledger database](ledger-database-ledger.md), a database with the ledger property set to on, can't be converted to a regular database, with the ledger property set to off.
- Automatic generation and storage of database digests is currently available in Azure SQL Database, but not supported on SQL Server.
- Automated digest management with ledger tables by using [Azure Storage immutable blobs](/azure/storage/blobs/immutable-storage-overview) doesn't offer the ability for users to use [locally redundant storage (LRS)](/azure/storage/common/storage-redundancy#locally-redundant-storage) accounts.
- When a ledger database is created, all new tables created by default (without specifying the `APPEND_ONLY = ON` clause) in the database will be [updatable ledger tables](ledger-updatable-ledger-tables.md). To create [append-only ledger tables](ledger-append-only-ledger-tables.md), use the `APPEND_ONLY = ON` clause in the [CREATE TABLE (Transact-SQL)](../../../t-sql/statements/create-table-transact-sql.md) statements.
- A transaction can update up to 200 ledger tables.

## Ledger table considerations and limitations

- Existing tables in a database that aren't ledger tables can't be converted to ledger tables. For more information, see [Migrate data from regular tables to ledger tables](ledger-how-to-migrate-data-to-ledger-tables.md).
- After a ledger table is created, it can't be reverted to a table that isn't a ledger table.
- Deleting older data in [append-only ledger tables](ledger-append-only-ledger-tables.md) or the history table of [updatable ledger tables](ledger-updatable-ledger-tables.md) isn't supported.
- `TRUNCATE TABLE` isn't supported.
- When an [updatable ledger table](ledger-updatable-ledger-tables.md) is created, it adds four [GENERATED ALWAYS](../../../t-sql/statements/create-table-transact-sql.md#generate-always-columns) columns to the ledger table. An [append-only ledger table](ledger-append-only-ledger-tables.md) adds two columns to the ledger table. These new columns count against the maximum supported number of columns in Azure SQL Database (1,024).
- In-memory tables aren't supported.
- Sparse column sets aren't supported.
- SWITCH IN/OUT partition isn't supported.
- Ledger tables can't have full-text indexes.
- Ledger tables can't be graph table.
- Ledger tables can't be FileTables.
- Ledger tables can't have a rowstore non-clustered index when they have a clustered columnstore index.
- Change tracking isn't allowed on the history table but is allowed on ledger tables.
- Change data capture isn't supported for ledger tables.
- Transactional replication isn't supported for ledger tables.
- Azure Synapse Link is supported but only for the ledger table, not the history table.

### Unsupported data types

- XML
- SqlVariant
- User-defined data type
- FILESTREAM

### Temporal table limitations

Updatable ledger tables are based on the technology of [temporal tables](../../tables/temporal-tables.md) and inherits most of the [limitations](../../tables/temporal-table-considerations-and-limitations.md) but not all of them. Below is a list of limitations that is inherited from temporal tables.

- If the name of a history table is specified during history table creation, you must specify the schema and table name and also the name of the ledger view.
- By default, the history table is PAGE compressed.
- If the current table is partitioned, the history table is created on the default file group because partitioning configuration isn't replicated automatically from the current table to the history table.
- Temporal and history tables can't be a FILETABLE and can contain columns of any supported datatype other than FILESTREAM. FILETABLE and FILESTREAM allow data manipulation outside of SQL Server, and thus system versioning can't be guaranteed.
- A node or edge table can't be created as or altered to a temporal table. Graph isn't supported with ledger.
- While temporal tables support blob data types, such as `(n)varchar(max)`, `varbinary(max)`, `(n)text`, and `image`, they'll incur significant storage costs and have performance implications due to their size. As such, when designing your system, care should be taken when using these data types.
- The history table must be created in the same database as the current table. Temporal querying over Linked Server isn't supported.
- The history table can't have constraints (Primary Key, Foreign Key, table, or column constraints).
- Online option (`WITH (ONLINE = ON`) has no effect on `ALTER TABLE ALTER COLUMN` in case of system-versioned temporal table. `ALTER COLUMN` isn't performed as online regardless of which value was specified for the `ONLINE` option.
- `INSERT` and `UPDATE` statements can't reference the [GENERATED ALWAYS](../../../t-sql/statements/create-table-transact-sql.md#generate-always-columns) columns. Attempts to insert values directly into these columns will be blocked.
- `UPDATETEXT` and `WRITETEXT` aren't supported.
- Triggers on the history table aren't allowed.
- Usage of replication technologies is limited:
    - Always On: Fully supported
    - Snapshot, merge and transactional replication: Not supported for temporal tables
- A history table can't be configured as current table in a chain of history tables.
- The following objects or properties aren't replicated from the current table to the history table when the history table is created:
    - Period definition
    - Identity definition
    - Indexes
    - Statistics
    - Check constraints
    - Triggers
    - Partitioning configuration
    - Permissions
    - Row-level security predicates

## Schema changes consideration

### Adding columns

Adding nullable columns is supported. Adding non-nullable columns is not supported. Ledger is designed to ignore NULL values when computing the hash of a row version. Based on that, when a nullable column is added, ledger will modify the schema of the ledger and history tables to include the new column, however, this doesn't impact the hashes of existing rows. Adding columns in ledger tables is captured in [sys.ledger_column_history](../../system-catalog-views/sys-ledger-column-history-transact-sql.md).

### Dropping columns and tables

Normally, dropping a column or table completely erases the underlying data from the database and is fundamentally incompatible with the ledger functionality that requires data to be immutable. Instead of deleting the data, ledger simply renames the objects being dropped so that they're logically removed from the user schema, but physically remain in the database. Any dropped columns are also hidden from the ledger table schema, so that they're invisible to the user application. However, the data of such dropped objects remains available for the ledger verification process, and allows users to inspect any historical data through the corresponding ledger views. Dropping columns in ledger tables is captured in [sys.ledger_column_history](../../system-catalog-views/sys-ledger-column-history-transact-sql.md). Dropping a ledger table is captured in [sys.ledger_table_history](../../system-catalog-views/sys-ledger-table-history-transact-sql.md). Dropping ledger tables and its dependent objects are marked as dropped in system catalog views and renamed:

- Dropped ledger tables are marked as dropped by setting `is_dropped_ledger_table` in **sys.tables** and renamed using the following format: `MSSQL_DroppedLedgerTable_<dropped_ledger_table_name>_<GUID>`.
- Dropped history tables for updatable ledger tables are renamed using the following format: `MSSQL_DroppedLedgerHistory_<dropped_history_table_name>_<GUID>`.
- Dropped ledger views are marked as dropped by setting `is_dropped_ledger_view` in **sys.views** and renamed using the following format: `MSSQL_DroppedLedgerView_<dropped_ledger_view_name>_<GUID>`.

> [!NOTE]  
> The name of dropped ledger tables, history tables and ledger views might be truncated if the length of the renamed table or view exceeds 128 characters. 

### Altering Columns

Any changes that don't impact the underlying data of a ledger table are supported without any special handling as they don't impact the hashes being captured in the ledger. These changes includes:

- Changing nullability
- Collation for Unicode strings
- The length of variable length columns

However, any operations that might affect the format of existing data, such as changing the data type aren't supported.

## Next steps

- [Ledger overview](ledger-overview.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Database ledger](ledger-database-ledger.md)
