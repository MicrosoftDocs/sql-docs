---
title: "ALTER INDEX (Transact-SQL)"
description: Modifies an existing table or view index (rowstore, columnstore, or XML) by disabling, rebuilding, or reorganizing the index; or by setting options on the index.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, randolphwest, dfurman
ms.date: 02/05/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ALTER INDEX"
  - "ALTER_INDEX_TSQL"
helpviewer_keywords:
  - "indexes [SQL Server], reorganizing"
  - "ALTER INDEX statement"
  - "indexes [SQL Server], disabling"
  - "online index operations"
  - "index reorganization [SQL Server]"
  - "ALLOW_ROW_LOCKS option"
  - "ALL keyword"
  - "reorganizing indexes"
  - "constraints [SQL Server], indexes"
  - "row locks [SQL Server]"
  - "index rebuilding [SQL Server]"
  - "rebuilding indexes"
  - "locking [SQL Server], indexes"
  - "partitioned indexes [SQL Server], rebuilding"
  - "defragmenting indexes"
  - "disabling indexes"
  - "XML indexes [SQL Server], modifying"
  - "index modifications [SQL Server]"
  - "indexes [SQL Server], modifying"
  - "index options [SQL Server]"
  - "modifying indexes"
  - "index disabling [SQL Server]"
  - "MAXDOP index option, ALTER INDEX statement"
  - "spatial indexes [SQL Server], modifying"
  - "indexes [SQL Server], options"
  - "ALLOW_PAGE_LOCKS option"
  - "page locks [SQL Server]"
  - "index rebuild [SQL Server]"
  - "index reorganize [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# ALTER INDEX (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Modifies an existing table or view index (rowstore, columnstore, or XML) by disabling, rebuilding, or reorganizing the index; or by setting options on the index.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, and Azure SQL Managed Instance.

```syntaxsql
ALTER INDEX { index_name | ALL } ON <object>
{
      REBUILD [
            [ WITH ( <rebuild_index_option> [ , ...n ] ) ]
          | [ PARTITION = ALL [ WITH ( <rebuild_index_option> [ , ...n ] ) ] ]
          | [ PARTITION = partition_number [ WITH ( <single_partition_rebuild_index_option> [ , ...n ] ) ] ]
      ]
    | DISABLE
    | REORGANIZE  [ PARTITION = partition_number ] [ WITH ( <reorganize_option>  ) ]
    | SET ( <set_index_option> [ , ...n ] )
    | RESUME [ WITH (<resumable_index_option> [ , ...n ] ) ]
    | PAUSE
    | ABORT
}
[ ; ]

<object> ::=
{
    { database_name.schema_name.table_or_view_name | schema_name.table_or_view_name | table_or_view_name }
}

<rebuild_index_option> ::=
{
      PAD_INDEX = { ON | OFF }
    | FILLFACTOR = fillfactor
    | SORT_IN_TEMPDB = { ON | OFF }
    | IGNORE_DUP_KEY = { ON | OFF }
    | STATISTICS_NORECOMPUTE = { ON | OFF }
    | STATISTICS_INCREMENTAL = { ON | OFF }
    | ONLINE = { ON [ ( <low_priority_lock_wait> ) ] | OFF }
    | RESUMABLE = { ON | OFF }
    | MAX_DURATION = <time> [ MINUTES ]
    | ALLOW_ROW_LOCKS = { ON | OFF }
    | ALLOW_PAGE_LOCKS = { ON | OFF }
    | MAXDOP = max_degree_of_parallelism
    | DATA_COMPRESSION = { NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE }
        [ ON PARTITIONS ( { <partition_number> [ TO <partition_number> ] } [ , ...n ] ) ]
    | XML_COMPRESSION = { ON | OFF }
        [ ON PARTITIONS ( { <partition_number> [ TO <partition_number> ] } [ , ...n ] ) ] }

<single_partition_rebuild_index_option> ::=
{
      SORT_IN_TEMPDB = { ON | OFF }
    | MAXDOP = max_degree_of_parallelism
    | RESUMABLE = { ON | OFF }
    | MAX_DURATION = <time> [ MINUTES ]
    | DATA_COMPRESSION = { NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE }
    | XML_COMPRESSION = { ON | OFF }
    | ONLINE = { ON [ ( <low_priority_lock_wait> ) ] | OFF }
}

<reorganize_option> ::=
{
       LOB_COMPACTION = { ON | OFF }
    |  COMPRESS_ALL_ROW_GROUPS =  { ON | OFF }
}

<set_index_option> ::=
{
      ALLOW_ROW_LOCKS = { ON | OFF }
    | ALLOW_PAGE_LOCKS = { ON | OFF }
    | OPTIMIZE_FOR_SEQUENTIAL_KEY = { ON | OFF }
    | IGNORE_DUP_KEY = { ON | OFF }
    | STATISTICS_NORECOMPUTE = { ON | OFF }
    | COMPRESSION_DELAY = { 0 | delay [ Minutes ] }
}

<resumable_index_option> ::=
 {
    MAXDOP = max_degree_of_parallelism
    | MAX_DURATION = <time> [ MINUTES ]
    | <low_priority_lock_wait>
 }

<low_priority_lock_wait> ::=
{
    WAIT_AT_LOW_PRIORITY ( MAX_DURATION = <time> [ MINUTES ] ,
                          ABORT_AFTER_WAIT = { NONE | SELF | BLOCKERS } )
}
```

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
ALTER INDEX { index_name | ALL }
    ON [ schema_name. ] table_name
{
      REBUILD [
            [ WITH ( <rebuild_index_option> [ , ...n ] ) ]
          | [ PARTITION = ALL [ WITH ( <rebuild_index_option> ) ] ]
          | [ PARTITION = partition_number [ WITH ( <single_partition_rebuild_index_option> ) ] ]
      ]
    | DISABLE
    | REORGANIZE [ PARTITION = partition_number ]
}
[ ; ]

<rebuild_index_option> ::=
{
    DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }
        [ ON PARTITIONS ( { <partition_number> [ TO <partition_number> ] } [ , ...n ] ) ]
    | XML_COMPRESSION = { ON | OFF }
        [ ON PARTITIONS ( { <partition_number> [ TO <partition_number> ] } [ , ...n ] ) ]
}

<single_partition_rebuild_index_option> ::=
{
    DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }
    | XML_COMPRESSION = { ON | OFF }
}
```

## Arguments

#### *index_name*

The name of the index. Index names must be unique within a table or view but don't have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

#### ALL

Specifies all indexes associated with the table or view regardless of the index type. Specifying `ALL` causes the statement to fail if one or more indexes are in an offline or read-only filegroup or the specified operation isn't allowed on one or more index types. The following table lists the index operations and disallowed index types.

| Using the keyword `ALL` with this operation | Fails if the table has one or more |
| --- | --- |
| `REBUILD WITH ONLINE = ON` | XML index<br /><br />Spatial index<br /><br />Columnstore index in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and older versions only. Later versions support online rebuild of columnstore indexes. |
| `REBUILD PARTITION = <partition_number>` | Nonpartitioned index, XML index, spatial index, or disabled index |
| `REORGANIZE` | Indexes with `ALLOW_PAGE_LOCKS` set to `OFF` |
| `REORGANIZE PARTITION = <partition_number>` | Nonpartitioned index, XML index, spatial index, or disabled index |
| `IGNORE_DUP_KEY = ON` | XML index<br /><br />Spatial index<br /><br />Columnstore index |
| `ONLINE = ON` | XML index<br /><br />Spatial index<br /><br />Columnstore index |
| `RESUMABLE = ON` | Resumable indexes not supported with the `ALL` keyword |

If `ALL` is specified with `PARTITION = <partition_number>`, all indexes must be aligned. This means that they're partitioned based on equivalent partition functions. Using `ALL` with `PARTITION` causes all index partitions with the same `<partition_number>` to be rebuilt or reorganized. For more information about partitioned indexes, see [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).

For more information about online index operations, see [Guidelines for online index operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).

#### *database_name*

The name of the database.

#### *schema_name*

The name of the schema to which the table or view belongs.

#### *table_or_view_name*

The name of the table or view associated with the index. To view index details for a table or view, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.

[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] supports the three-part name format `<database_name>.<schema_name>.<object_name>` when `<database_name>` is the current database name, or `<database_name>` is `tempdb` and `<object_name>` starts with `#` or `##`. If the schema name is `dbo`, `<schema_name>` can be omitted.

#### REBUILD [ WITH ( \<rebuild_index_option> [ ,... *n* ] ) ]

**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

Specifies that the index is rebuilt using the same columns, index type, uniqueness attribute, and sort order. `REBUILD` enables a disabled index. Rebuilding a clustered index doesn't rebuild associated nonclustered indexes unless the keyword `ALL` is specified. If index options aren't specified, the existing index option values in [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) are applied. For any index option whose value doesn't appear in `sys.indexes`, the default indicated in the argument definition of the option applies.

If `ALL` is specified and the underlying table is a heap, the rebuild operation has no effect on the heap. Any nonclustered indexes associated with the table are rebuilt.

The `REBUILD` operation can be minimally logged if the database recovery model is either bulk-logged or simple.

When you rebuild a primary XML index, the underlying user table is unavailable for the duration of the index operation.

For columnstore indexes, the rebuild operation:

- Recompresses all data into the columnstore. Two copies of the columnstore index exist while the rebuild operation is in progress. When the rebuild is finished, [!INCLUDE [ssDE](../../includes/ssde-md.md)] deletes the original columnstore index.
- Doesn't preserve the column segment order introduced by the `ORDER` clause when creating the index. To rebuild a columnstore index and preserve or introduce a column segment order, use the `CREATE [CLUSTERED] COLUMNSTORE INDEX ... ORDER (...) ... WITH (DROP_EXISTING = ON)` statement. For more information, see [CREATE COLUMNSTORE INDEX](create-columnstore-index-transact-sql.md) and [Performance tuning with ordered columnstore indexes](../../relational-databases/indexes/ordered-columnstore-indexes.md).

For more information about index maintenance, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

#### PARTITION

Specifies that only one partition of an index is rebuilt or reorganized. `PARTITION` can't be specified if *index_name* isn't a partitioned index.

`PARTITION = ALL` rebuilds all partitions.

> [!WARNING]  
> Creating and rebuilding nonaligned indexes on a table with more than 1,000 partitions is possible, but isn't supported. Doing so might cause degraded performance or excessive memory consumption during these operations. Microsoft recommends using only aligned indexes when the number of partitions exceeds 1,000.

- *partition_number*

  The partition number of a partitioned index that is to be rebuilt or reorganized. *partition_number* is a constant expression that can reference variables. These include user-defined type variables or functions and user-defined functions, but can't reference a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. *partition_number* must exist or the statement fails.

- *WITH ( <single_partition_rebuild_index_option> )*

  `SORT_IN_TEMPDB`, `MAXDOP`, `DATA_COMPRESSION`, and `XML_COMPRESSION` are the options that can be specified when you rebuild a single partition using the `(PARTITION = partition_number)` syntax. XML indexes can't be specified in a single partition rebuild operation.

#### DISABLE

Marks the index as disabled and unavailable for use by the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Any index can be disabled. The index definition of a disabled index remains in the system catalog with no underlying index data. Disabling a clustered index prevents user access to the underlying table data. To enable an index, use `ALTER INDEX REBUILD` or `CREATE INDEX WITH DROP_EXISTING`. For more information, see [Disable indexes and constraints](../../relational-databases/indexes/disable-indexes-and-constraints.md) and [Enable indexes and constraints](../../relational-databases/indexes/enable-indexes-and-constraints.md).

#### REORGANIZE a rowstore index

For rowstore indexes, `REORGANIZE` specifies to reorganize the index leaf level. The `REORGANIZE` operation is:

- Always performed online. This means long-term blocking table locks aren't held and queries or updates to the data in the underlying table can continue during the `ALTER INDEX REORGANIZE` transaction.
- Not allowed for a disabled index.
- Not allowed when `ALLOW_PAGE_LOCKS` is set to `OFF`.
- Not rolled back when performed within a transaction and the transaction is rolled back.

> [!NOTE]  
> When `ALTER INDEX REORGANIZE` uses explicit transactions (for example, `ALTER INDEX` inside a `BEGIN TRAN ... COMMIT/ROLLBACK`) instead of the default implicit transaction mode, the locking behavior of `REORGANIZE` becomes more restrictive, potentially causing blocking. For more information about implicit transactions, see [SET IMPLICIT_TRANSACTIONS](set-implicit-transactions-transact-sql.md).

For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

#### REORGANIZE WITH ( LOB_COMPACTION = { ON | OFF } )

Applies to rowstore indexes.

- ON

  - Specifies to compact all pages that contain data of these large object (LOB) data types: **image**, **text**, **ntext**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml**. Compacting this data can reduce the data size on disk.
  - For a clustered index, this compacts all LOB columns that are contained in the table.
  - For a nonclustered index, this compacts all LOB columns that are nonkey (included) columns in the index.
  - `REORGANIZE ALL` performs LOB compaction on all indexes. For each index, this compacts all LOB columns in the clustered index, underlying table, or included columns in a nonclustered index.

- OFF

  - Pages that contain large object data aren't compacted.
  - OFF has no effect on a heap.

#### REORGANIZE a columnstore index

For columnstore indexes, `REORGANIZE` compresses each closed delta rowgroup into the columnstore as a compressed rowgroup. The `REORGANIZE` operation is always performed online. This means long-term blocking table locks aren't held and queries or updates to the underlying table can continue during the `ALTER INDEX REORGANIZE` transaction.

For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

- `REORGANIZE` isn't required in order to move the closed delta rowgroups into compressed rowgroups. The background tuple-mover (TM) process wakes up periodically to compress the closed delta rowgroups. We recommend using `REORGANIZE` when tuple-mover is falling behind. `REORGANIZE` can compress rowgroups more aggressively.
- To compress all open and closed rowgroups, see the [REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS)](#reorganize-with--compress_all_row_groups---on--off--).

For columnstore indexes in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)], `REORGANIZE` performs the following extra defragmentation optimizations online:

- Physically removes deleted rows from a rowgroup when 10% or more of the rows have been logically deleted. The deleted bytes are reclaimed on the physical media. For example, if a compressed row group of 1 million rows has 100,000 rows deleted, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] removes the deleted rows and recompresses the rowgroup with 900,000 rows.

- Combines one or more compressed rowgroups to increase rows per rowgroup up to the maximum of 1,048,576 rows. For example, if you bulk import 5 batches of 102,400 rows, you get 5 compressed rowgroups. If you run `REORGANIZE`, these rowgroups get merged into 1 compressed rowgroup with 512,000 rows. This assumes there are no dictionary size or memory limitations.

- For rowgroups in which 10% or more of the rows have been logically deleted, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tries to combine this rowgroup with one or more rowgroups. For example, rowgroup 1 is compressed with 500,000 rows and rowgroup 21 is compressed with the maximum of 1,048,576 rows. Rowgroup 21 has 60% of the rows deleted which leaves 409,830 rows. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] favors combining these two rowgroups to compress a new rowgroup that has 909,830 rows.

#### REORGANIZE WITH ( COMPRESS_ALL_ROW_GROUPS = { ON | OFF } )

Applies to columnstore indexes.

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

`COMPRESS_ALL_ROW_GROUPS` provides a way to force open or closed delta rowgroups into the columnstore. With this option, it isn't necessary to rebuild the columnstore index to empty the delta rowgroups. Combined with the other remove and merge defragmentation features, this makes it no longer necessary to rebuild a columnstore index in most situations.

- ON

  Forces all rowgroups into the columnstore, regardless of size and state (closed or open).

- OFF

  Forces all closed rowgroups into the columnstore.

For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

#### SET ( \<set_index option\> [ ,... *n* ] )

Modifies index options without rebuilding or reorganizing the index. `SET` can't be specified for a disabled index.

#### PAD_INDEX = { ON | OFF }

Specifies index padding. The default is `OFF`.

- ON

  The percentage of free space that is specified by fill factor is applied to the intermediate-level pages of the index. If `FILLFACTOR` isn't specified at the same time `PAD_INDEX` is set to `ON`, the fill factor value in [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) is used.

- OFF

  The intermediate-level pages are filled to near capacity, leaving sufficient space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages. This also occurs if `PAD_INDEX` is set to `ON` but fill factor isn't specified.

For more information, see [CREATE INDEX](create-index-transact-sql.md#pad_index---on--off-).

#### FILLFACTOR = *fillfactor*

Specifies a percentage that indicates how full the [!INCLUDE [ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or alteration. The value for *fillfactor* must be an integer value from 1 to 100. The default is 0. Fill factor values 0 and 100 are the same in all respects.

An explicit `FILLFACTOR` setting applies only when the index is first created or rebuilt. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] doesn't dynamically keep the specified percentage of empty space in the pages. For more information, see [CREATE INDEX](create-index-transact-sql.md#fillfactor--fillfactor).

To view the fill factor setting, use `fill_factor` in `sys.indexes`.

> [!IMPORTANT]  
> Creating an index with a `FILLFACTOR` less than 100 increases the amount of storage space the data occupies because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] redistributes the data according to the fill factor when it creates or rebuilds an index.

#### SORT_IN_TEMPDB = { ON | OFF }

Specifies whether to store temporary sort results in `tempdb`. The default is `OFF` except for Azure SQL Database Hyperscale. For all index build operations in Hyperscale, `SORT_IN_TEMPDB` is always `ON` unless a resumable index build is used. For resumable index builds, `SORT_IN_TEMPDB` is always `OFF`.

- ON

  The intermediate sort results that are used to build the index are stored in `tempdb`. This might reduce the time required to create an index. However, this increases the amount of disk space that is used during the index build.

- OFF

  The intermediate sort results are stored in the same database as the index.

If a sort operation isn't required, or if the sort can be performed in memory, the `SORT_IN_TEMPDB` option is ignored.

For more information, see [SORT_IN_TEMPDB option for indexes](../../relational-databases/indexes/sort-in-tempdb-option-for-indexes.md).

#### IGNORE_DUP_KEY = { ON | OFF }

Specifies the error response when an insert operation attempts to insert duplicate key values into a unique index. The `IGNORE_DUP_KEY` option applies only to insert operations after the index is created or rebuilt. The default is `OFF`.

- ON

  A warning message occurs when duplicate key values are inserted into a unique index. Only the rows violating the uniqueness constraint aren't inserted.

- OFF

  An error message occurs when duplicate key values are inserted into a unique index. The entire `INSERT` operation is rolled back.

`IGNORE_DUP_KEY` can't be set to `ON` for indexes created on a view, non-unique indexes, XML indexes, spatial indexes, and filtered indexes.

To view the `IGNORE_DUP_KEY` setting for an index, use the `ignore_dup_key` column in the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.

In backward compatible syntax, `WITH IGNORE_DUP_KEY` is equivalent to `WITH IGNORE_DUP_KEY = ON`.

#### STATISTICS_NORECOMPUTE = { ON | OFF }

Disable or enable the automatic statistics update option, `AUTO_STATISTICS_UPDATE`, for the statistics on the index. The default is `OFF`.

- ON

  Automatic statistics updates are disabled after the index is rebuilt.

- OFF

  Automatic statistics updates are enabled after the index is rebuilt.

To restore automatic statistics updating, set the `STATISTICS_NORECOMPUTE` to `OFF`, or execute `UPDATE STATISTICS` without the `NORECOMPUTE` clause.

> [!WARNING]  
> If you disable automatic recomputation of statistics by setting `STATISTICS_NORECOMPUTE = ON`, you might prevent the query optimizer from picking optimal execution plans for queries involving the table.

Setting `STATISTICS_NORECOMPUTE` to `ON` doesn't prevent the update of index statistics that occurs during the index rebuild operation.

#### STATISTICS_INCREMENTAL = { ON | OFF }

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

When `ON`, the statistics created on the index are per partition statistics. When `OFF`, the existing statistics is dropped and the [!INCLUDE [ssDE](../../includes/ssde-md.md)] recomputes the statistics. The default is `OFF`.

If per partition statistics aren't supported, the option is ignored and a warning is generated. Incremental statistics aren't supported in the following cases:

- Statistics created with indexes that aren't partition-aligned with the base table
- Statistics created on availability group readable secondary databases
- Statistics created on read-only databases
- Statistics created on filtered indexes
- Statistics created on views
- Statistics created on internal tables
- Statistics created with spatial indexes or XML indexes

#### ONLINE = { ON | OFF }

Specifies whether underlying tables and associated indexes are available for queries and data modification during the index operation. The default is `OFF`.

For an XML index or spatial index, only `ONLINE = OFF` is supported, and if `ONLINE` is set to `ON` an error is raised.

> [!IMPORTANT]  
> Online index operations aren't available in every edition of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

- ON

  Long-term table locks are not held for the duration of the index operation. During the main phase of the index operation, only an intent shared (`IS`) lock is held on the source table. This enables queries or updates to the underlying table and indexes to proceed. At the start of the operation, a shared (`S`) lock is held on the source object for a short period of time. At the end of the operation, for a short period of time, a shared (`S`) lock is acquired on the object if a nonclustered index is being created. A schema modification (`Sch-M`) lock is acquired when a clustered index is created or dropped online and when a clustered or nonclustered index is being rebuilt. `ONLINE` can't be set to `ON` when an index is being created on a local temporary table.

  > [!NOTE]  
  > You can use the `WAIT_AT_LOW_PRIORITY` option to reduce or avoid blocking during online index operations. For more information, see [WAIT_AT_LOW_PRIORITY with online index operations](#wait-at-low-priority).

- OFF

  Table locks are applied for the duration of the index operation. An offline index operation that creates, rebuilds, or drops a clustered, spatial, or XML index, or rebuilds or drops a nonclustered index, acquires a schema modification (`Sch-M`) lock on the table. This prevents all user access to the underlying table for the duration of the operation. An offline index operation that creates a nonclustered index initially acquires a shared (`S`) lock on the table. This prevents modifications of the underlying table definition, but allows reading and modifying the data in the table while the index build is in progress.

For more information, see [Perform index operations online](../../relational-databases/indexes/perform-index-operations-online.md) and [Guidelines for online index operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).

Indexes, including indexes on global temp tables, can be rebuilt online except for the following cases:

- XML index
- Index on a local temp table
- Initial unique clustered index on a view
- Disabled clustered indexes
- Clustered columnstore indexes in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and earlier versions
- Nonclustered columnstore indexes in [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)]) and earlier versions
- Clustered index, if the underlying table contains LOB data types (**image**, **ntext**, **text**) and spatial data types
- **varchar(max)** and **varbinary(max)** columns can't be part of an index key. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]), in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and in [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)], when a table contains **varchar(max)** or **varbinary(max)** columns, a clustered index containing other columns can be built or rebuilt using the `ONLINE` option.

For more information, see [How online index operations work](../../relational-databases/indexes/how-online-index-operations-work.md).

#### RESUMABLE = { ON | OFF}

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

Specifies whether an online index operation is resumable.

- ON

  Index operation is resumable.

- OFF

  Index operation isn't resumable.

#### MAX_DURATION = *time* [ MINUTES ] used with `RESUMABLE = ON` (requires `ONLINE = ON`)

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

Specifies for how long, in integer minutes, a resumable index operation is executed before it's paused.

#### ALLOW_ROW_LOCKS = { ON | OFF }

Specifies whether row locks are allowed. The default is `ON`.

- ON

  Row locks are allowed when accessing the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when row locks are used.

- OFF

  Row locks aren't used.

#### ALLOW_PAGE_LOCKS = { ON | OFF }

Specifies whether page locks are allowed. The default is `ON`.

- ON

  Page locks are allowed when you access the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines when page locks are used.

- OFF

  Page locks aren't used.

#### OPTIMIZE_FOR_SEQUENTIAL_KEY = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

Specifies whether or not to optimize to avoid last-page insert contention. The default is `OFF`. For more information, see [Sequential keys](create-index-transact-sql.md#sequential-keys).

#### MAXDOP = max_degree_of_parallelism

Overrides the **max degree of parallelism** configuration option for the index operation. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use `MAXDOP` to limit the degree of parallelism and the resulting resource consumption for an index build operation.

Although the `MAXDOP` option is syntactically supported for all XML indexes and spatial indexes, `ALTER INDEX` currently uses only a single processor.

*max_degree_of_parallelism* can be:

- 1

  Suppresses parallel plan generation.

- \>1

  Restricts the maximum degree of parallelism used in a parallel index operation to the specified number or less based on the current system workload.

- 0 (default)

  Uses the degree of parallelism specified at the server, database, or workload group level, unless reduced based on the current system workload.

For more information, see [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

#### COMPRESSION_DELAY = { 0 | *duration* [ minutes ] }

**Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

For a disk-based table with a columnstore index, specifies the minimum number of minutes a delta rowgroup in the closed state must remain in the delta store before the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can compress it into a compressed rowgroup. Since disk-based tables don't track insert and update times on individual rows, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] applies this delay only to delta store rowgroups in the closed state.

The default is 0 minutes.

For recommendations on when to use `COMPRESSION_DELAY`, see [Get started with columnstore indexes for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).

#### DATA_COMPRESSION

Specifies the data compression option for the specified index, partition number, or range of partitions. The options are as follows:

- NONE

  Index or specified partitions aren't compressed. This doesn't apply to columnstore indexes.

- ROW

  Index or specified partitions are compressed by using row compression. This doesn't apply to columnstore indexes.

- PAGE

  Index or specified partitions are compressed by using page compression. This doesn't apply to columnstore indexes.

- COLUMNSTORE

  **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

  Applies only to columnstore indexes, including both nonclustered columnstore and clustered columnstore indexes. Specifying `COLUMNSTORE` removes all other data compression including `COLUMNSTORE_ARCHIVE`.

- COLUMNSTORE_ARCHIVE

  **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]

  Applies only to columnstore indexes, including both nonclustered columnstore and clustered columnstore indexes. `COLUMNSTORE_ARCHIVE` further compresses the specified partition to a smaller size. This can be used for archival, or for other situations that require a smaller storage size and can afford more time for storage and retrieval.

For more information about compression, see [Data compression](../../relational-databases/data-compression/data-compression.md).

#### XML_COMPRESSION

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Specifies the XML compression option for the specified index that contains one or more **xml** data type columns. The options are as follows:

- ON

  Index or specified partitions are compressed by using XML compression.

- OFF

  Index or specified partitions aren't compressed.

#### ON PARTITIONS ( { \<partition_number_expression> | \<range\> } [ ,...n ] )

Specifies the partitions to which the `DATA_COMPRESSION` or `XML_COMPRESSION` settings apply. If the index isn't partitioned, the `ON PARTITIONS` argument generates an error. If the `ON PARTITIONS` clause isn't provided, the `DATA_COMPRESSION` or `XML_COMPRESSION` option applies to all partitions of a partitioned index.

`<partition_number_expression>` can be specified in the following ways:

- Provide the number for a partition, for example: `ON PARTITIONS (2)`.
- Provide the partition numbers for several individual partitions separated by commas, for example: `ON PARTITIONS (1, 5)`.
- Provide both ranges and individual partitions: `ON PARTITIONS (2, 4, 6 TO 8)`.

`<range>` can be specified as partition numbers separated by the word `TO`, for example: `ON PARTITIONS (6 TO 8)`.

To set different types of data compression for different partitions, specify the `DATA_COMPRESSION` option more than once, for example:

```sql
REBUILD WITH
(
DATA_COMPRESSION = NONE ON PARTITIONS (1),
DATA_COMPRESSION = ROW ON PARTITIONS (2, 4, 6 TO 8),
DATA_COMPRESSION = PAGE ON PARTITIONS (3, 5)
);
```

You can also specify the `XML_COMPRESSION` option more than once, for example:

```sql
REBUILD WITH
(
XML_COMPRESSION = OFF ON PARTITIONS (1),
XML_COMPRESSION = ON ON PARTITIONS (2, 4, 6 TO 8),
XML_COMPRESSION = OFF ON PARTITIONS (3, 5)
);
```

#### RESUME

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Resumes an index operation that is paused manually, because the maximum duration is reached, or because of a failure.

- MAX_DURATION

  Specifies for how long, in integer minutes, a resumable index operation is executed after being resumed before it's paused again.

- WAIT_AT_LOW_PRIORITY

  Resuming an index build operation after a pause has to acquire the necessary locks. `WAIT_AT_LOW_PRIORITY` indicates that the index build operation acquires low priority locks, which allow other operations to proceed while the index build operation is waiting. Omitting the `WAIT_AT_LOW_PRIORITY` option is equivalent to `WAIT_AT_LOW_PRIORITY (MAX_DURATION = 0 minutes, ABORT_AFTER_WAIT = NONE)`. For more information, see [WAIT_AT_LOW_PRIORITY](alter-index-transact-sql.md#wait-at-low-priority).

#### PAUSE

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Pauses a resumable index build operation.

#### ABORT

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Aborts a running or paused index build operation that was started as resumable. You must explicitly execute an `ABORT` command to terminate a resumable index build operation. A failure or a pause in a resumable index operation doesn't terminate its execution; rather, it leaves the operation in an indefinite pause state.

## Remarks

`ALTER INDEX` can't be used to repartition an index or move it to a different filegroup. This statement can't be used to modify the index definition, such as adding or deleting columns or changing the column order. Use `CREATE INDEX` with the `DROP_EXISTING` clause to perform these operations.

When an option isn't explicitly specified, the current setting is applied. For example, if a `FILLFACTOR` setting isn't specified in the `REBUILD` clause, the fill factor value stored in the system catalog is used during the rebuild process. To view the current index option settings, use [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md).

The values for `ONLINE`, `MAXDOP`, and `SORT_IN_TEMPDB` aren't stored in the system catalog. Unless specified in the index statement, the default value for the option is used.

On multiprocessor computers, just like other queries do, `ALTER INDEX REBUILD` automatically uses more processors to perform the scan and sort operations that are associated with modifying the index. Conversely, `ALTER INDEX REORGANIZE` is a single threaded operation. For more information, see [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], `ALTER INDEX ALL` is not supported, but `ALTER INDEX <index name>` is.

<a id="rebuilding-indexes"></a>

## Rebuild indexes

Rebuilding an index drops and re-creates the index. This removes fragmentation, reclaims disk space by compacting the pages based on the specified or existing fill factor setting, and reorders the index rows in contiguous pages. When `ALL` is specified, all indexes on the table are dropped and rebuilt in a single transaction. Foreign key constraints don't have to be dropped in advance. When indexes with 128 extents or more are rebuilt, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. For more information, see [Deferred deallocation](drop-index-transact-sql.md#deferred-deallocation).

For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

<a id="reorganizing-indexes"></a>

## Reorganize indexes

Reorganizing an index uses minimal system resources. It defragments the leaf level of clustered and nonclustered indexes on tables and views by physically reordering the leaf-level pages to match the logical, left to right, order of the leaf nodes. Reorganizing also compacts the index pages. Compaction is based on the existing fill factor value.

When `ALL` is specified, relational indexes, both clustered and nonclustered, and XML indexes on the table are reorganized. Some [restrictions](#all) apply when specifying `ALL`.

For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

> [!NOTE]  
> For a table with an ordered columnstore index, `ALTER INDEX REORGANIZE` doesn't re-sort the data. To resort the data use `CREATE [CLUSTERED] COLUMNSTORE INDEX ... ORDER (...) ... WITH (DROP_EXISTING = ON)`.

<a id="disabling-indexes"></a>

## Disable indexes

Disabling an index prevents user access to the index, and for clustered indexes, to the underlying table data. The index definition remains in the system catalog. Disabling a nonclustered index or clustered index on a view physically deletes the index data. Disabling a clustered index prevents access to the data, but the data remains unmaintained in the B-tree until the index is dropped or rebuilt. To see if an index is disabled, use the `is_disabled` column in the `sys.indexes` catalog view.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

If a table is in a transactional replication publication, you can't disable an index that is associated with a primary key constraint. These indexes are required by replication. To disable such an index, you must first drop the table from the publication. For more information, see [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md).

Use the `ALTER INDEX REBUILD` statement or the `CREATE INDEX WITH DROP_EXISTING` statement to enable the index. Rebuilding a disabled clustered index can't be performed with the `ONLINE` option set to `ON`. For more information, see [Disable indexes and constraints](../../relational-databases/indexes/disable-indexes-and-constraints.md).

<a id="setting-options"></a>

## Set options

You can set the options `ALLOW_ROW_LOCKS`, `ALLOW_PAGE_LOCKS`, `OPTIMIZE_FOR_SEQUENTIAL_KEY`, `IGNORE_DUP_KEY`, and `STATISTICS_NORECOMPUTE` for a specified index without rebuilding or reorganizing that index. The modified values are immediately applied to the index. To view these settings, use `sys.indexes`. For more information, see [Set Index Options](../../relational-databases/indexes/set-index-options.md).

### Row and page locks options

When `ALLOW_ROW_LOCKS = ON` and `ALLOW_PAGE_LOCK = ON`, row-level, page-level, and table-level locks are allowed when you access the index. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] chooses the appropriate lock and can escalate the lock from a row or page lock to a table lock.

When `ALLOW_ROW_LOCKS = OFF` and `ALLOW_PAGE_LOCK = OFF`, only a table-level lock is allowed when you access the index.

If `ALL` is specified when the row or page lock options are set, the settings are applied to all indexes. When the underlying table is a heap, the settings are applied in the following ways:

| Option | Applies to |
| --- | --- |
| `ALLOW_ROW_LOCKS = ON` or `OFF` | The heap and all associated nonclustered indexes. |
| `ALLOW_PAGE_LOCKS = ON` | The heap and all associated nonclustered indexes. |
| `ALLOW_PAGE_LOCKS = OFF` | The nonclustered indexes, where all page locks aren't allowed. For the heap, only the shared (`S`), update (`U`) and exclusive (`X`) page locks aren't allowed. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] can still acquire intent page locks (`IS`, `IU`, or `IX`) for internal purposes. |

> [!WARNING]
> It is not recommended to disable row or page locks on an index. Concurrency-related problems might occur, and certain functionality might be unavailable. For example, an index can't be reorganized when `ALLOW_PAGE_LOCKS` is set to `OFF`.

<a id="online-index-operations"></a>

## Online index operations

When rebuilding an index and the `ONLINE` option is set to `ON`, data in the index, its associated table, and other indexes on the same table is available for queries and modification. You can also rebuild online a portion of an index residing on a single partition. Exclusive table locks are held only for a short amount of time at the end of the index rebuild.

Reorganizing an index is always performed online. The process holds locks only for short periods of time and is unlikely to block queries or updates.

You can perform concurrent online index operations on the same table or table partition only when doing the following operations:

- Creating multiple nonclustered indexes.
- Reorganizing different indexes on the same table.
- Reorganizing different indexes while rebuilding nonoverlapping indexes on the same table.

All other online index operations performed at the same time fail. For example, you can't rebuild two or more indexes on the same table concurrently, or create a new index while rebuilding an existing index on the same table.

For more information, see [Perform index operations online](../../relational-databases/indexes/perform-index-operations-online.md).

<a id="resumable-indexes"></a>

### Resumable index operations

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

You can make an online index rebuild resumable. That means that the index rebuild can be stopped and later restarted from the point where it stopped. To run an index rebuild as resumable, specify the `RESUMABLE = ON` option.

The following guidelines apply to resumable index operations:

- To use the `RESUMABLE` option you must also use the `ONLINE` option.
- The `RESUMABLE` option isn't persisted in the metadata for a given index and applies only to the duration of the current DDL statement. Therefore, the `RESUMABLE = ON` clause must be specified explicitly to enable resumability.
- The `MAX_DURATION` option can be specified in two contexts:
  - `MAX_DURATION` for the `RESUMABLE` option specifies the time interval for an index being built. After this time elapses, and if the index build is still running, it is paused. You decide when the build for a paused index can be resumed. The **time** in minutes for `MAX_DURATION` must be greater than 0 minutes and less than or equal to one week (7 \* 24 \* 60 = 10080 minutes). A long pause in an index operation might noticeably impact the DML performance on a specific table as well as the database disk capacity since both the original index and the newly created index require disk space and need to be updated by DML operations. If `MAX_DURATION` option is omitted, the index operation continues until completion or until a failure occurs.
  - `MAX_DURATION` for the `WAIT_AT_LOW_PRIORITY` option specifies the time to wait using low priority locks if the index operation is blocked, before taking action. For more information, see [WAIT_AT_LOW_PRIORITY with online index operations](#wait-at-low-priority).
- To pause the index operation immediately, you can execute the `ALTER INDEX PAUSE` command, or execute the `KILL <session_id>` command.
- Re-executing the original `ALTER INDEX REBUILD` statement with the same parameters resumes a paused index rebuild operation. You can also resume a paused index rebuild operation by executing the `ALTER INDEX RESUME` statement.
- The `ABORT` command kills the session that is running an index build and cancels the index operation. You cannot resume an index operation that has been aborted.
- When resuming an index rebuild operation that is paused, you can change the `MAXDOP` value to a new value. If `MAXDOP` isn't specified when resuming an index operation that is paused, the `MAXDOP` value used for the last resume is used. If the `MAXDOP` option isn't specified at all for an index rebuild operation, then the default value is used.

A resumable index operation runs until it completes, pauses, or fails. In case the operation pauses, an error is issued indicating that the operation was paused and that the index rebuild did not complete. In case the operation fails, an error is issued as well.

To see if an index operation is executed as a resumable operation and to check its current execution state, use the [sys.index_resumable_operations](../../relational-databases/system-catalog-views/sys-index-resumable-operations.md) catalog view.

#### Resources

The following resources are required for resumable index operations:

- Additional space required to keep the index being built, including the time when build is paused.
- Additional log throughput during the sorting phase. The overall log space usage for resumable index is less compared to regular online index rebuild and allows log truncation during this operation.
- DDL statements attempting to modify an index that is being rebuild or its associated table while the index operation is paused aren't allowed.
- Ghost cleanup is blocked on the in-build index for the duration of the operation both while paused and while the operation is running.
- If the table contains LOB columns, a resumable clustered index build requires a schema modification (`Sch-M`) lock at the start of the operation.

#### Current functional limitations

Resumable index rebuild operations have the following limitations:

- The `SORT_IN_TEMPDB = ON` option isn't supported for resumable index operations.
- The DDL command with `RESUMABLE = ON` can't be executed inside an explicit transaction.
- You cannot rebuild an index using a resumable index operation if the index contains:
  - A computed or `timestamp` (`rowversion`) column as a key column.
  - A computed or LOB column as an included column.
- Resumable index operations aren't supported for:
  - The `ALTER INDEX REBUILD ALL` command
  - The `ALTER TABLE REBUILD` command
  - Columnstore indexes
  - Filtered indexes
  - Disabled indexes

<a id="wait-at-low-priority"></a>

### WAIT_AT_LOW_PRIORITY with online index operations

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

When you don't use the `WAIT_AT_LOW_PRIORITY` option, all active blocking transactions holding locks on the table or index must complete for the index rebuild operation to start and to complete. When the online index operation starts and before it completes, it needs to acquire a shared (`S`) or a schema modification (`Sch-M`) lock on the table and hold it for a short time. Even though the lock is held for a short time only, it might significantly affect workload throughput, increase query latency, or cause execution time-outs.

To avoid these problems, the `WAIT_AT_LOW_PRIORITY` option allows you to manage the behavior of `S` or `Sch-M` locks required for an online index operation to start and complete, selecting from three options. In all cases, if during the wait time specified by `MAX_DURATION = n [minutes]` there is no blocking that involves the index operation, the index operation proceeds immediately.

`WAIT_AT_LOW_PRIORITY` makes the online index operation wait using low priority locks, allowing other operations using normal priority locks to proceed in the meantime. Omitting the `WAIT_AT_LOW_PRIORITY` option is equivalent to `WAIT_AT_LOW_PRIORITY (MAX_DURATION = 0 minutes, ABORT_AFTER_WAIT = NONE)`.

`MAX_DURATION` = *time* [`MINUTES`]

The wait time (an integer value specified in minutes) that the online index operation waits using low priority locks. If the operation is blocked for the `MAX_DURATION` time, the specified `ABORT_AFTER_WAIT` action is executed. `MAX_DURATION` time is always in minutes, and the word `MINUTES` can be omitted.

`ABORT_AFTER_WAIT` = [`NONE` | `SELF` | `BLOCKERS` ]

- `NONE`: Continue waiting for the lock with normal priority.
- `SELF`: Exit the online index operation currently being executed, without taking any action. The option `SELF` can't be used when `MAX_DURATION` is 0.
- `BLOCKERS`: Kill all user transactions that block the online index operation so that the operation can continue. The `BLOCKERS` option requires the principal executing the `CREATE INDEX` or `ALTER INDEX` statement to have the `ALTER ANY CONNECTION` permission.

You can use the following extended events to monitor index operations that wait for locks at low priority:

- `lock_request_priority_state`
- `process_killed_by_abort_blockers`
- `ddl_with_wait_at_low_priority`

## Spatial index restrictions

When you rebuild a spatial index, the underlying user table is unavailable during the index operation.

The `PRIMARY KEY` constraint in the user table can't be modified while a spatial index is defined on a column of that table. To change the `PRIMARY KEY` constraint, first drop every spatial index of the table. After modifying the `PRIMARY KEY` constraint, you can re-create each of the spatial indexes.

In a single partition rebuild operation, you can't specify any spatial indexes. However, you can specify spatial indexes in a table rebuild.

To change options that are specific to a spatial index, such as `BOUNDING_BOX` or `GRID`, you can either use a `CREATE SPATIAL INDEX` statement that specifies `DROP_EXISTING = ON`, or drop the spatial index and create a new one. For an example, see [CREATE SPATIAL INDEX](create-spatial-index-transact-sql.md).

## Data compression

For a more information about data compression, see [Data compression](../../relational-databases/data-compression/data-compression.md).

The following are the key points to consider in the context of index build operations when data compression is used:

- Compression can allow more rows to be stored on a page, but doesn't change the maximum row size.
- Non-leaf pages of an index are not page compressed but can be row compressed.
- Each nonclustered index has an individual compression setting, and doesn't inherit the compression setting of the underlying table.
- When a clustered index is created on a heap, the clustered index inherits the compression state of the heap unless an alternative compression state is specified.

The following considerations apply rebuilding partitioned indexes:

- You can't change the compression setting of a single partition if the table has nonaligned indexes.
- The `ALTER INDEX <index> ... REBUILD PARTITION ... WITH DATA_COMPRESSION = ...` syntax rebuilds the specified partition of the index with the specified compression option. If the `WITH DATA_COMPRESSION` clause is omitted, the existing compression option is used.
- The `ALTER INDEX <index> ... REBUILD PARTITION = ALL` syntax rebuilds all partitions of the index using the existing compression options.
- The `ALTER INDEX <index> ... REBUILD PARTITION = ALL (WITH ...)` syntax rebuilds all partitions of the index. You can choose different compression for different partitions using the `DATA_COMPRESSION = ... ON PARTITIONS ( ...)` clause.

To evaluate how changing `PAGE` and `ROW` compression affects a table, an index, or a partition, use the [sp_estimate_data_compression_savings](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md) stored procedure.

## Statistics

When you rebuild an index, the statistics on the index are updated with full scan for non-partitioned indexes, and with the default sampling ratio for partitioned indexes. No other statistics on the table are updated as a part of index rebuild.

## Permissions

The `ALTER` permission on the table or view is required.

## Version notes

- [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] doesn't support filegroups other than `PRIMARY`.
- [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] don't support `FILESTREAM` options.
- Columnstore indexes aren't available before [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].
- Resumable index operations are available in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)].

## Basic syntax example

```sql
ALTER INDEX index1 ON table1 REBUILD;
ALTER INDEX ALL ON table1 REBUILD;
ALTER INDEX ALL ON dbo.table1 REBUILD;
```

## Examples: Columnstore indexes

These examples apply to columnstore indexes.

### A. REORGANIZE demo

This example demonstrates how the `ALTER INDEX REORGANIZE` command works. It creates a table that has multiple rowgroups, and then demonstrates how `REORGANIZE` merges the rowgroups.

```sql
-- Create a database
CREATE DATABASE [columnstore];
GO

-- Create a rowstore staging table
CREATE TABLE [staging] (
    AccountKey INT NOT NULL,
    AccountDescription NVARCHAR(50),
    AccountType NVARCHAR(50),
    AccountCodeAlternateKey INT
);

-- Insert 10 million rows into the staging table.
DECLARE @loop INT;
DECLARE @AccountDescription VARCHAR(50);
DECLARE @AccountKey INT;
DECLARE @AccountType VARCHAR(50);
DECLARE @AccountCode INT;

SELECT @loop = 0

BEGIN TRANSACTION

WHILE (@loop < 300000)
BEGIN
    SELECT @AccountKey = CAST(RAND() * 10000000 AS INT);
    SELECT @AccountDescription = 'accountdesc ' + CONVERT(VARCHAR(20), @AccountKey);
    SELECT @AccountType = 'AccountType ' + CONVERT(VARCHAR(20), @AccountKey);
    SELECT @AccountCode = CAST(RAND() * 10000000 AS INT);

    INSERT INTO staging
    VALUES (
        @AccountKey,
        @AccountDescription,
        @AccountType,
        @AccountCode
     );

    SELECT @loop = @loop + 1;
END

COMMIT

-- Create a table for the clustered columnstore index
CREATE TABLE cci_target (
    AccountKey INT NOT NULL,
    AccountDescription NVARCHAR(50),
    AccountType NVARCHAR(50),
    AccountCodeAlternateKey INT
);

-- Convert the table to a clustered columnstore index named inxcci_cci_target;
CREATE CLUSTERED COLUMNSTORE INDEX idxcci_cci_target ON cci_target;
```

Use the TABLOCK option to insert rows in parallel. Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the `INSERT INTO` operation can run in parallel when `TABLOCK` is used.

```sql
INSERT INTO cci_target WITH (TABLOCK)
SELECT TOP 300000 * FROM staging;
```

Run this command to see the `OPEN` delta rowgroups. The number of rowgroups depends on the degree of parallelism.

```sql
SELECT *
FROM sys.dm_db_column_store_row_group_physical_stats
WHERE object_id  = object_id('cci_target');
```

Run this command to force all `CLOSED` and `OPEN` rowgroups into the columnstore.

```sql
ALTER INDEX idxcci_cci_target ON cci_target REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS = ON);
```

Run this command again and you see that smaller rowgroups are merged into one compressed rowgroup.

```sql
ALTER INDEX idxcci_cci_target ON cci_target REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS = ON);
```

### B. Compress CLOSED delta rowgroups into the columnstore

This example uses the `REORGANIZE` option to compress each `CLOSED` delta rowgroup into the columnstore as a compressed rowgroup. This isn't necessary, but is useful when the tuple-mover isn't compressing `CLOSED` rowgroups fast enough.

You can run both example in the [!INCLUDE [sssampledbdwobject-md](../../includes/sssampledbdwobject-md.md)] sample database.

This sample runs `REORGANIZE` on all partitions.

```sql
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REORGANIZE;
```

This sample runs `REORGANIZE` on a specific partition.

```sql
-- REORGANIZE a specific partition
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REORGANIZE PARTITION = 0;
```

### C. Compress all OPEN AND CLOSED delta rowgroups into the columnstore

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

The command `REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS = ON)` compresses each `OPEN` and `CLOSED` delta rowgroup into the columnstore as a compressed rowgroup. This empties the deltastore and forces all rows to get compressed into the columnstore. This is useful especially after performing many insert operations since these operations store the rows in one or more delta rowgroups.

`REORGANIZE` combines rowgroups to fill rowgroups up to a maximum number of rows <= 1,024,576. Therefore, when you compress all `OPEN` and `CLOSED` rowgroups you don't end up with lots of compressed rowgroups that only have a few rows in them. You want rowgroups to be as full as possible to reduce the compressed size and improve query performance.

The following examples use the [!INCLUDE [sssampledbdwobject-md](../../includes/sssampledbdwobject-md.md)] database.

This example moves all `OPEN` and `CLOSED` delta rowgroups into the columnstore index.

```sql
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS = ON);
```

This example moves all `OPEN` and `CLOSED` delta rowgroups into the columnstore index for a specific partition.

```sql
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REORGANIZE PARTITION = 0 WITH (COMPRESS_ALL_ROW_GROUPS = ON);
```

### D. Defragment a columnstore index online

**Doesn't apply to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)].

Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], `REORGANIZE` does more than compress delta rowgroups into the columnstore. It also performs online defragmentation. First, it reduces the size of the columnstore by physically removing deleted rows when 10% or more of the rows in a rowgroup have been deleted. Then, it combines rowgroups together to form larger rowgroups that have up to the maximum of 1,024,576 rows per rowgroups. All rowgroups that are changed get recompressed.

> [!NOTE]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], rebuilding a columnstore index is no longer necessary in most situations since `REORGANIZE` physically removes deleted rows and merges rowgroups. The `COMPRESS_ALL_ROW_GROUPS` option forces all `OPEN` or `CLOSED` delta rowgroups into the columnstore which previously could only be done with a rebuild. `REORGANIZE` is online and occurs in the background so queries can continue as the operation happens.

The following example performs a `REORGANIZE` to defragment the index by physically removing rows that have been logically deleted from the table, and merging rowgroups.

```sql
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REORGANIZE;
```

### E. Rebuild a clustered columnstore index offline

Applies to: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

> [!TIP]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], we recommend using `ALTER INDEX REORGANIZE` instead of `ALTER INDEX REBUILD` for columnstore indexes.

> [!NOTE]  
> In [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], `REORGANIZE` is only used to compress `CLOSED` rowgroups into the columnstore. The only way to perform defragmentation operations and to force all delta rowgroups into the columnstore is to rebuild the index.

This example shows how to rebuild a clustered columnstore index and force all delta rowgroups into the columnstore. This first step prepares a table `FactInternetSales2` in the [!INCLUDE [sssampledbdwobject-md](../../includes/sssampledbdwobject-md.md)] database with a clustered columnstore index, and inserts data from the first four columns.

```sql
CREATE TABLE dbo.FactInternetSales2 (
    ProductKey [int] NOT NULL,
    OrderDateKey [int] NOT NULL,
    DueDateKey [int] NOT NULL,
    ShipDateKey [int] NOT NULL);

CREATE CLUSTERED COLUMNSTORE INDEX cci_FactInternetSales2
ON dbo.FactInternetSales2;

INSERT INTO dbo.FactInternetSales2
SELECT ProductKey, OrderDateKey, DueDateKey, ShipDateKey
FROM dbo.FactInternetSales;

SELECT * FROM sys.column_store_row_groups;
```

The results show one `OPEN` rowgroup, which means [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] waits for more rows to be added before it closes the rowgroup and moves the data to the columnstore. This next statement rebuilds the clustered columnstore index, which forces all rows into the columnstore.

```sql
ALTER INDEX cci_FactInternetSales2 ON FactInternetSales2 REBUILD;
SELECT * FROM sys.column_store_row_groups;
```

The results of the `SELECT` statement show the rowgroup is `COMPRESSED`, which means the column segments of the rowgroup are now compressed and stored in the columnstore.

### F. Rebuild a partition of a clustered columnstore index offline

**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

To rebuild a partition of a large clustered columnstore index, use `ALTER INDEX REBUILD` with the partition option. This example rebuilds partition 12. Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], we recommend replacing `REBUILD` with `REORGANIZE`.

```sql
ALTER INDEX cci_fact3
ON fact3
REBUILD PARTITION = 12;
```

### G. Change a clustered columnstore index to use archival compression

**Doesn't apply to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]

You can choose to reduce the size of a clustered columnstore index even further by using the `COLUMNSTORE_ARCHIVE` data compression option. This is practical for older data that you want to keep on cheaper storage. We recommend only using this on data that isn't accessed often since decompress is slower than with the normal `COLUMNSTORE` compression.

The following example rebuilds a clustered columnstore index to use archival compression, and then shows how to remove the archival compression. The final result uses only columnstore compression.

First, prepare the example by creating a table with a clustered columnstore index. Then, compress the table further by using archival compression.

```sql
--Prepare the example by creating a table with a clustered columnstore index.
CREATE TABLE SimpleTable (
    ProductKey [int] NOT NULL,
    OrderDateKey [int] NOT NULL,
    DueDateKey [int] NOT NULL,
    ShipDateKey [int] NOT NULL
);

CREATE CLUSTERED INDEX cci_SimpleTable ON SimpleTable (ProductKey);

CREATE CLUSTERED COLUMNSTORE INDEX cci_SimpleTable
ON SimpleTable
WITH (DROP_EXISTING = ON);

--Compress the table further by using archival compression.
ALTER INDEX cci_SimpleTable ON SimpleTable
REBUILD
WITH (DATA_COMPRESSION = COLUMNSTORE_ARCHIVE);
GO
```

This sample removes the archive compression, and only uses columnstore compression.

```sql
ALTER INDEX cci_SimpleTable ON SimpleTable
REBUILD
WITH (DATA_COMPRESSION = COLUMNSTORE);
GO
```

## Examples: Rowstore indexes

### A. Rebuild an index

The following example rebuilds a single index on the `Employee` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER INDEX PK_Employee_EmployeeID ON HumanResources.Employee REBUILD;
```

### B. Rebuild all indexes on a table and specify options

The following example specifies the keyword `ALL`. This rebuilds all indexes associated with the table `Production.Product` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Three options are specified.

```sql
ALTER INDEX ALL ON Production.Product
REBUILD WITH (FILLFACTOR = 80, SORT_IN_TEMPDB = ON, STATISTICS_NORECOMPUTE = ON);
```

The following example adds the ONLINE option including the low priority lock option, and adds the row compression option.

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

```sql
ALTER INDEX ALL ON Production.Product
REBUILD WITH
(
    FILLFACTOR = 80,
    SORT_IN_TEMPDB = ON,
    STATISTICS_NORECOMPUTE = ON,
    ONLINE = ON ( WAIT_AT_LOW_PRIORITY ( MAX_DURATION = 4 MINUTES, ABORT_AFTER_WAIT = BLOCKERS ) ),
    DATA_COMPRESSION = ROW
);
```

### C. Reorganize an index with LOB compaction

The following example reorganizes a single clustered index in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Because the index contains a LOB data type in the leaf level, the statement also compacts all pages that contain the large object data. Specifying the `WITH (LOB_COMPACTION = ON)` option isn't required because the default value is ON.

```sql
ALTER INDEX PK_ProductPhoto_ProductPhotoID ON Production.ProductPhoto REORGANIZE WITH (LOB_COMPACTION = ON);
```

### D. Set options on an index

The following example sets several options on the index `AK_SalesOrderHeader_SalesOrderNumber` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER INDEX AK_SalesOrderHeader_SalesOrderNumber ON
    Sales.SalesOrderHeader
SET (
    STATISTICS_NORECOMPUTE = ON,
    IGNORE_DUP_KEY = ON,
    ALLOW_PAGE_LOCKS = ON
    ) ;
GO
```

### E. Disable an index

The following example disables a nonclustered index on the `Employee` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER INDEX IX_Employee_ManagerID ON HumanResources.Employee DISABLE;
```

### F. Disable constraints

The following example disables a `PRIMARY KEY` constraint by disabling the `PRIMARY KEY` index in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. The `FOREIGN KEY` constraint on the underlying table is automatically disabled and warning message is displayed.

```sql
ALTER INDEX PK_Department_DepartmentID ON HumanResources.Department DISABLE;
```

The result set returns this warning message.

```output
Warning: Foreign key 'FK_EmployeeDepartmentHistory_Department_DepartmentID'
on table 'EmployeeDepartmentHistory' referencing table 'Department'
was disabled as a result of disabling the index 'PK_Department_DepartmentID'.
```

### G. Enable constraints

The following example enables the `PRIMARY KEY` and `FOREIGN KEY` constraints that were disabled in Example F.

The `PRIMARY KEY` constraint is enabled by rebuilding the `PRIMARY KEY` index.

```sql
ALTER INDEX PK_Department_DepartmentID ON HumanResources.Department REBUILD;
```

The `FOREIGN KEY` constraint is then enabled.

```sql
ALTER TABLE HumanResources.EmployeeDepartmentHistory
CHECK CONSTRAINT FK_EmployeeDepartmentHistory_Department_DepartmentID;
GO
```

### H. Rebuild a partitioned index

The following example rebuilds a single partition, partition number `5`, of the partitioned index `IX_TransactionHistory_TransactionDate` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Partition 5 is rebuilt with `ONLINE=ON` and the 10 minutes wait time for the low priority lock applies separately to every lock acquired by index rebuild operation. If during this time the lock can't be obtained to complete index rebuild, the rebuild operation statement itself is aborted, due to `ABORT_AFTER_WAIT = SELF`.

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

```sql
-- Verify the partitioned indexes.
SELECT *
FROM sys.dm_db_index_physical_stats (DB_ID(),OBJECT_ID(N'Production.TransactionHistory'), NULL , NULL, NULL);
GO
--Rebuild only partition 5.
ALTER INDEX IX_TransactionHistory_TransactionDate
ON Production.TransactionHistory
REBUILD Partition = 5
   WITH (ONLINE = ON (WAIT_AT_LOW_PRIORITY (MAX_DURATION = 10 minutes, ABORT_AFTER_WAIT = SELF)));
GO
```

### I. Change the compression setting of an index

The following example rebuilds an index on a nonpartitioned rowstore table.

```sql
ALTER INDEX IX_INDEX1
ON T1
REBUILD
WITH (DATA_COMPRESSION = PAGE);
GO
```

### J. Change the setting of an index with XML compression

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

The following example rebuilds an index on a nonpartitioned rowstore table.

```sql
ALTER INDEX IX_INDEX1
ON T1
REBUILD
WITH (XML_COMPRESSION = ON);
GO
```

For more data compression examples, see [Data compression](../../relational-databases/data-compression/data-compression.md).

### K. Online resumable index rebuild

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)]

The following examples show how to use online resumable index rebuild.

Execute an online index rebuild as resumable operation with `MAXDOP = 1`. Executing the same command again after an index operation was paused, automatically resumes the index rebuild operation.

```sql
ALTER INDEX test_idx on test_table REBUILD WITH (ONLINE = ON, MAXDOP = 1, RESUMABLE = ON);
```

Execute an online index rebuild as resumable operation with `MAX_DURATION` set to 240 minutes.

```sql
ALTER INDEX test_idx on test_table REBUILD WITH (ONLINE = ON, RESUMABLE = ON, MAX_DURATION = 240);
```

Pause a running resumable online index rebuild.

```sql
ALTER INDEX test_idx on test_table PAUSE;
```

Resume an online index rebuild for an index rebuild that was executed as resumable operation specifying a new value for `MAXDOP` set to 4.

```sql
ALTER INDEX test_idx on test_table RESUME WITH (MAXDOP = 4);
```

Resume an online index rebuild operation for an index online rebuild that was executed as resumable. Set `MAXDOP` to 2, set the execution time for the index being running as resumable to 240 minutes, and if an index is being blocked on the lock, wait 10 minutes and after that kill all blockers.

```sql
ALTER INDEX test_idx on test_table
    RESUME WITH (MAXDOP = 2, MAX_DURATION = 240 MINUTES,
    WAIT_AT_LOW_PRIORITY (MAX_DURATION = 10, ABORT_AFTER_WAIT = BLOCKERS));
```

Abort resumable index rebuild operation that is running or paused.

```sql
ALTER INDEX test_idx on test_table ABORT;
```

## Related content

- [Index architecture and design guide](../../relational-databases/sql-server-index-design-guide.md)
- [Perform index operations online](../../relational-databases/indexes/perform-index-operations-online.md)
- [CREATE INDEX (Transact-SQL)](create-index-transact-sql.md)
- [CREATE SPATIAL INDEX (Transact-SQL)](create-spatial-index-transact-sql.md)
- [CREATE XML INDEX (Transact-SQL)](create-xml-index-transact-sql.md)
- [DROP INDEX (Transact-SQL)](drop-index-transact-sql.md)
- [Disable indexes and constraints](../../relational-databases/indexes/disable-indexes-and-constraints.md)
- [XML indexes (SQL Server)](../../relational-databases/xml/xml-indexes-sql-server.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md)
- [sys.dm_db_index_physical_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
