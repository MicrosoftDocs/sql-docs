---
title: "sys.dm_db_persisted_sku_features (Transact-SQL)"
description: sys.dm_db_persisted_sku_features (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/23/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_persisted_sku_features_TSQL"
  - "sys.dm_db_persisted_sku_features"
  - "dm_db_persisted_sku_features_TSQL"
  - "dm_db_persisted_sku_features"
helpviewer_keywords:
  - "editions [SQL Server]"
  - "sys.dm_db_persisted_sku_features dynamic management view"
dev_langs:
  - "TSQL"
ms.custom: linux-related-content
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# sys.dm_db_persisted_sku_features (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Some features of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] change the way that information is stored in the database files. These features are restricted to specific editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. A database that contains these features can't be moved to an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that doesn't support them. Use the `sys.dm_db_persisted_sku_features` dynamic management view to list edition-specific features that are enabled in the current database.

| Column name | Data type | Description |
| --- | --- | --- |
| `feature_name` | **sysname** | External name of the feature that is enabled in the database but not supported on the all the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This feature must be removed before the database can be migrated to all available editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `feature_id` | **int** | Feature ID that is associated with the feature. [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]. |

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW DATABASE STATE permission on the database.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW DATABASE PERFORMANCE STATE permission on the database.

## Remarks

If there are no features that may be restricted by a specific edition in the database, the view returns no rows.

`sys.dm_db_persisted_sku_features` may list the following database-changing features as restricted to specific [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] editions:

- **ChangeCapture**: Indicates that a database has change data capture enabled. To remove change data capture, use the [sys.sp_cdc_disable_db](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) stored procedure. For more information, see [About Change Data Capture (SQL Server)](../../relational-databases/track-changes/about-change-data-capture-sql-server.md).

- **ColumnStoreIndex**: Indicates that at least one table has a columnstore index. To enable a database to be moved to an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that doesn't support this feature, use the [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statement to remove the columnstore index. For more information, see [Columnstore indexes](../../relational-databases/indexes/columnstore-indexes-overview.md).

- **Compression**: Indicates that at least one table or index uses data compression or the vardecimal storage format. To enable a database to be moved to an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that doesn't support this feature, use the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statement to remove data compression. To remove vardecimal storage format, use the `sp_tableoption` statement. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md).

- **MultipleFSContainers**: Indicates that the database uses multiple FILESTREAM containers. The database has a FILESTREAM filegroup with multiple containers (files). For more information, see [FILESTREAM (SQL Server)](../../relational-databases/blob/filestream-sql-server.md).

- **InMemoryOLTP**: Indicates that the database uses In-Memory OLTP. The database has a MEMORY_OPTIMIZED_DATA filegroup. For more information, see [In-Memory OLTP (In-Memory Optimization)](../in-memory-oltp/overview-and-usage-scenarios.md).

- **Partitioning.** Indicates that the database contains partitioned tables, partitioned indexes, partition schemes, or partition functions. To enable a database to be moved to an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] other than Enterprise or Developer, it is insufficient to modify the table to be on a single partition. You must remove the partitioned table. If the table contains data, use SWITCH PARTITION to convert each partition into a nonpartitioned table. Then delete the partitioned table, the partition scheme, and the partition function.

- **TransparentDataEncryption.** Indicates that a database is encrypted by using transparent data encryption. To remove transparent data encryption, use the ALTER DATABASE statement. For more information, see [Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md).

> [!NOTE]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 1, these features, except **TransparentDataEncryption** are available across multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] editions, and not limited to Enterprise or Developer editions only.

To determine whether a database uses any features that are restricted to specific editions, execute the following statement in the database:

```sql
SELECT feature_name
FROM sys.dm_db_persisted_sku_features;
GO
```

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

[!INCLUDE [editions-supported-features-linux](../../includes/editions-supported-features-linux.md)]

## Related content

- [System Dynamic Management Views](system-dynamic-management-views.md)
- [Database Related Dynamic Management Views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
