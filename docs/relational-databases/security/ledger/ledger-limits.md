---
title: "Limitations for Azure SQL Database ledger"
description: Limitations of the ledger feature in Azure SQL Database
ms.date: "04/05/2022"
ms.service: sql-database
ms.subservice: security
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
---

# Ledger considerations and limitations

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

There are some considerations and limitations to be aware of when working with ledger tables, due to the nature of system-versioning and immutable data.

## General Limitations

Consider the following when working with ledger.

- A ledger database, a database with the ledger property set to on, can't be converted to a regular database, with the ledger property set to off.
- Automated digest management with ledger tables by using [Azure Storage immutable blobs](/azure/storage/blobs/immutable-storage-overview) doesn't offer the ability for users to use [LRS](/azure/storage/common/storage-redundancy#locally-redundant-storage) accounts.
- When a ledger database is created, all new tables created by default (without specifying the `APPEND_ONLY = ON` clause) in the database will be [updatable ledger tables](ledger-updatable-ledger-tables.md). To create [append-only ledger tables](ledger-append-only-ledger-tables.md), use [CREATE TABLE (Transact-SQL)](/sql/t-sql/statements/create-table-transact-sql) statements.
- A transaction can update up to 200 ledger tables.
- Change tracking isn't allowed on ledger tables.
- Transactional replication isn't supported for ledger tables.

## Ledger Table Limitations

- Existing tables in a database that aren't ledger tables can't be converted to ledger tables.
- After a ledger table is created, it can't be reverted to a table that isn't a ledger table.
- Deleting older data in [append-only ledger tables](ledger-append-only-ledger-tables.md) or the history table of [updatable ledger tables](ledger-updatable-ledger-tables.md) isn't supported.
- TRUNCATE TABLE isn't supported.
- When an [updatable ledger table](ledger-updatable-ledger-tables.md) is created, it adds four [GENERATED ALWAYS](/sql/t-sql/statements/create-table-transact-sql#generate-always-columns) columns to the ledger table. An [append-only ledger table](ledger-append-only-ledger-tables.md) adds two columns to the ledger table. These new columns count against the maximum supported number of columns in SQL Database (1,024).
- In-memory tables aren't supported.
- Sparse column sets aren't supported.
- Ledger tables can't have full-text indexes.
- Ledger tables can't be graph table.
- Ledger tables can't have a rowstore non-clustered index when they have a clustered columnstore index.
- For updatable ledger tables, we inherit all of the limitations of temporal tables.

### Unsupported data types

- XML
- SqlVariant
- User-defined data type
- FILESTREAM

### Temporal Table Limitations
Ledger tables are based on the technology of [Temporal Tables](https://docs.microsoft.com/en-us/sql/relational-databases/tables/temporal-tables?view=sql-server-ver15) and inherits most of the [limitations](https://docs.microsoft.com/en-us/sql/relational-databases/tables/temporal-table-considerations-and-limitations?view=sql-server-ver15) but not all of them. Below is a list of limitations that is inherited from Temporal tables.

- Temporal syntax works on tables or views that are stored locally in the database. If it is a remote object like a table on linked server or external table then you cannot use the FOR clause or period predicates directly in the query. 
- If the name of a history table is specified during history table creation, you must specify the schema and table name and also the name of the ledger view.
- By default, the history table is PAGE compressed.
- If current table is partitioned, the history table is created on default file group because partitioning configuration is not replicated automatically from the current table to the history table.
- Temporal and history tables cannot be FILETABLE and can contain columns of any supported datatype other than FILESTREAM since FILETABLE and FILESTREAM allow data manipulation outside of SQL Server and thus system versioning cannot be guaranteed.
- A node or edge table cannot be created as or altered to a temporal table. Graph is not supported with ledger.
- While temporal tables support blob data types, such as (n)varchar(max), varbinary(max), (n)text, and image, they will incur significant storage costs and have performance implications due to their size. As such, when designing your system, care should be taken when using these data types.
- History table must be created in the same database as the current table. Temporal querying over Linked Server is not supported.
- History table cannot have constraints (primary key, foreign key, table or column constraints).
- Online option (WITH (ONLINE = ON) has no effect on ALTER TABLE ALTER COLUMN in case of system-versioned temporal table. ALTER column is not performed as online regardless of which value was specified for ONLINE option.
- INSERT and UPDATE statements cannot reference the [GENERATED ALWAYS](/sql/t-sql/statements/create-table-transact-sql#generate-always-columns) columns. Attempts to insert values directly into these columns will be blocked.
- UPDATETEXT and WRITETEXT are not supported
- Triggers on the history table are not allowed
- Usage of replication technologies is limited:
    - Always On: Fully supported
    - Change Data Capture and Change Tracking: Supported only on the current table
    - Snapshot, merge and transactional replication: Not supported for temporal tables
- A history table cannot be configured as current table in a chain of history tables.
- The following objects/properties are not replicated from the current to the history table when the history table is created:
    - Period definition
    - Identity definition
    - Indexes
    - Statistics
    - Check constraints
    - Triggers
    - Partitioning configuration
    - Permissions
    - Row-level security predicates 

## Schema changes limitations

### Adding columns

Ledger is designed to ignore NULL values when computing the hash of a row version. Based on that, when a nullable column is added, ledger will modify the schema of the ledger and history tables to include the new column, however, this doesn't impact the hashes of existing rows. When the verification process recomputes these hashes, it will ignore the NULL values for the new column and compute a hash that matches what was originally recorded in the ledger. This technique helps us support adding new columns to a ledger table.

### Dropping columns and tables

Normally, dropping a column/table completely erases the underlying data from the database and is, therefore, fundamentally incompatible with the ledger functionality that requires data to be immutable. Instead of deleting the data, ledger simply renames the objects being dropped so that they're logically removed from the user schema but physically remain in the database. Any dropped columns are also hidden from the ledger table schema, so that they're invisible to the user application. However, the data of such dropped objects remains available for the ledger verification process, and allows users to inspect any historical data through the corresponding ledger views.

### Altering Columns

Any changes that don't impact the underlying data of a ledger table, such as:

- changing nullability
- collation for Unicode strings
- the length of variable length columns

are supported without any special handling as they don't impact the hashes being captured in the ledger. However, any operations that might affect the format of existing data, such as changing the data type isn't supported. This should be handled by dropping the existing column, adding it back with the original name and, finally, repopulating it with the original data, including any conversion that is required for the type change. The logic to drop and add the column follows the semantics we presented in the previous sections.

## Digest management considerations

### Database restore

Restoring the database back to an earlier point in time, also known as Point in Time Restore, is an operation frequently used when a mistake occurs and users need to quickly revert the state of the database back to an earlier point in time. When uploading the generated digests to Azure Storage, we'll capture the *create time* of the database that these digests map to. Every time the database is restored, it's tagged with a new *create time* and this technique allows us to store the digests across different “incarnations” of the database. Ledger preserves the information regarding when a restore operation occurred, allowing the verification process to use all the relevant digests across the various incarnations of the database. Additionally, users can inspect all digests for different create times to identify when the database was restored and how far back it was restored to. Since this data is written in immutable storage, this information will be protected as well.

### Active geo-replication

Replication across geographic regions is asynchronous for performance reasons and, thus, allows the secondary database to be slightly behind compared to the primary. In the event of a geographic failover, any latest data that hasn't yet been replicated is lost. Ledger will only issue database digests for data that has been replicated to geographic secondaries to guarantee that digests will never reference data that might be lost in case of a geographic failover.

Dropping the link between the primary and the secondaries when ledger digests are configured isn't supported. You should first disable the *Enable automatic digest storage* database setting, remove the synchronization between the primary and the secondary and re-enable the *Enable automatic digest storage* database setting.

## Next steps

- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Database ledger](ledger-database-ledger.md)
- [Digest management](ledger-digest-management.md)
- [Database verification](ledger-database-verification.md)
