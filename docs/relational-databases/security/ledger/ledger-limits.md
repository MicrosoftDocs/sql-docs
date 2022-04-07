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

> [!NOTE]
> Azure SQL Database ledger is currently in public preview.

There are some considerations and limitations to be aware of when working with ledger tables, due to the nature of system-versioning and immutable data.

## General Limitations

Consider the following when working with ledger.
- A ledger database, this is a database with the ledger property set to on, cannot be converted to a regular database, with the ledger property set to off.
- Automated digest management with ledger tables by using [Azure Storage immutable blobs](../../storage/blobs/immutable-storage-overview.md) doesn't offer the ability for users to use [LRS](../../storage/common/storage-redundancy.md#locally-redundant-storage) accounts.
- When a ledger database is created, all new tables created by default (without specifying the `APPEND_ONLY = ON` clause) in the database will be [updatable ledger tables](ledger-updatable-ledger-tables.md). To create [append-only ledger tables](ledger-append-only-ledger-tables.md), use [CREATE TABLE (Transact-SQL)](/sql/t-sql/statements/create-table-transact-sql) statements.
- A transaction can update up to 200 ledger tables.
- Change tracking isn't allowed on ledger tables.
- Transactional replication is not supported for ledger tables.

## Ledger Table Limitations
- Existing tables in a database that are not ledger tables can't be converted to ledger tables.
- After a ledger table is created, it can't be reverted to a table that isn't a ledger table.
- Deleting older data in [append-only ledger tables](ledger-append-only-ledger-tables.md) or the history table of [updatable ledger tables](ledger-updatable-ledger-tables.md) isn't supported.
- TRUNCATE TABLE is not supported.
- When an [updatable ledger table](ledger-updatable-ledger-tables.md) is created, it adds four [GENERATED ALWAYS](/sql/t-sql/statements/create-table-transact-sql#generate-always-columns) columns to the ledger table. An [append-only ledger table](ledger-append-only-ledger-tables.md) adds two columns to the ledger table. These new columns count against the maximum supported number of columns in SQL Database (1,024).
- In-memory tables aren't supported.
- Sparse column sets aren't supported.
- Ledger tables can't have full-text indexes.
- Ledger tables cannot be graph.
- Ledger tables can't have a rowstore non-clustered index when they have a clustered columnstore index.
- For updatable ledger tables, we inherit all of the limitations of temporal tables.

### Unsupported data types
- XML
- SqlVariant
- User-defined data type
- FILESTREAM 

## Schema changes limitations
### Adding columns
Ledger is designed to ignore NULL values when computing the hash of a row version. Based on that, when a nullable column is added, ledger will modify the schema of the ledger and history tables to include the new column, however, this does not impact the hashes of existing rows. When the verification process re-computes these hashes, it will ignore the NULL values for the new column and compute a hash that matches what was originally recorded in the ledger. This technique helps us support adding new columns to a ledger table.

### Dropping columns and tables
Normally, dropping a column/table completely erases the underlying data from the database and is, therefore, fundamentally incompatible with the ledger functionality that requires data to be immutable. Instead of deleting the data, ledger simply renames the objects being dropped so that they are logically removed from the user schema but physically remain in the database. Any dropped columns are also hidden from the ledger table schema, so that they are invisible to the user application. However, the data of such dropped objects remains available for the ledger verification process, and allows users to inspect any historical data through the corresponding ledger views. 

### Altering Columns 
Any changes that do not impact the underlying data of a ledger table, such as 
- changing nullability
- collation for Unicode strings
- the length of variable length columns
 
are supported without any special handling as they do not impact the hashes being captured in the ledger. However, any operations that might affect the format of existing data, such as changing the data type is not supported. This should be handled by dropping the existing column, adding it back with the original name and, finally, re-populating it with the original data, including any conversion that is required for the type change. The logic to drop and add the column follows the semantics we presented in the previous sections. 

## Digest management considerations
### Database restore
Restoring the database back to an earlier point in time, also known as Point in Time Restore, is an operation frequently used when a mistake occurs and users need to quickly revert the state of the database back to an earlier point in time. When uploading the generated digests to Azure Storage, we will capture the “create time” of the database that these digests map to. Every time the database is restored, it is tagged with a new create time and this technique allows us to store the digests across different “incarnations” of the database. Ledger preserves the information regarding when a restore operation occurred, allowing the verification process to use all the relevant digests across the various incarnations of the database. Additionally, users can inspect all digests for different create times to identify when the database was restored and how far back it was restored to. Since this data is written in immutable storage, this information will be protected as well.

### Active geo-replication
Replication across geographic regions is asynchronous for performance reasons and, thus, allows the secondary database to be slightly behind compared to the primary. In the event of a geographic failover, any latest data that has not yet been replicated is lost. Ledger will only issue database digests for data that has been replicated to geographic secondaries to guarantee that digests will never reference data that might be lost in case of a geographic failover. 

Dropping the link between the primary and the secondaries when ledger digests are configured is not supported. You should first disable the *Enable automatic digest storage* database setting, remove the synchronization between the primary and the secondary and re-enable the *Enable automatic digest storage* database setting.

## Next steps

- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Database ledger](ledger-database-ledger.md)
- [Digest management](ledger-digest-management.md)
- [Database verification](ledger-database-verification.md)
