---
title: sys.tables (Transact-SQL)
description: sys.tables returns a row for each user table in a database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/27/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.tables_TSQL"
  - "sys.tables"
helpviewer_keywords:
  - "sys.tables catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.tables (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a row for each user table in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

| Column name | Data type | Description |
| --- | --- | --- |
| `<inherited columns>` | | For a list of columns that this view inherits, see [sys.objects (Transact-SQL)](sys-objects-transact-sql.md). |
| `lob_data_space_id` | **int** | A nonzero value is the ID of the data space (filegroup or partition scheme) that holds the large object binary (LOB) data for this table. Examples of LOB data types include **varbinary(max)**, **varchar(max)**, **geography**, or **xml**.<br /><br />0 = The table doesn't have LOB data. |
| `filestream_data_space_id` | **int** | The data space ID for a FILESTREAM filegroup or a partition scheme that consists of FILESTREAM filegroups.<br /><br />To report the name of a FILESTREAM filegroup, execute the query `SELECT FILEGROUP_NAME (filestream_data_space_id) FROM sys.tables`.<br />`sys.tables` can be joined to the following views on `filestream_data_space_id` = `data_space_id`.<br />- `sys.filegroups`<br />- `sys.partition_schemes`<br />- `sys.indexes`<br />- `sys.allocation_units`<br />- `sys.fulltext_catalogs`<br />- `sys.data_spaces`<br />- `sys.destination_data_spaces`<br />- `sys.master_files`<br />- `sys.database_files`<br />- `backupfilegroup` (join on `filegroup_id`) |
| `max_column_id_used` | **int** | Maximum column ID ever used by this table. |
| `lock_on_bulk_load` | **bit** | Table is locked on bulk load. For more information, see [sp_tableoption (Transact-SQL)](../system-stored-procedures/sp-tableoption-transact-sql.md). |
| `uses_ansi_nulls` | **bit** | Table was created with the `SET ANSI_NULLS` database option `ON`. |
| `is_replicated` | **bit** | 1 = Table is published using snapshot replication or transactional replication. |
| `has_replication_filter` | **bit** | 1 = Table has a replication filter. |
| `is_merge_published` | **bit** | 1 = Table is published using merge replication. |
| `is_sync_tran_subscribed` | **bit** | 1 = Table is subscribed using an immediate updating subscription. |
| `has_unchecked_assembly_data` | **bit** | 1 = Table contains persisted data that depends on an assembly whose definition changed during the last `ALTER ASSEMBLY`. Will be reset to 0 after the next successful `DBCC CHECKDB` or `DBCC CHECKTABLE`. |
| `text_in_row_limit` | **int** | The maximum bytes allowed for text in row.<br /><br />0 = Text in row option isn't set. For more information, see [sp_tableoption (Transact-SQL)](../system-stored-procedures/sp-tableoption-transact-sql.md). |
| `large_value_types_out_of_row` | **bit** | 1 = Large value types are stored out-of-row. For more information, see [sp_tableoption (Transact-SQL)](../system-stored-procedures/sp-tableoption-transact-sql.md). |
| `is_tracked_by_cdc` | **bit** | 1 = Table is enabled for change data capture. For more information, see [sys.sp_cdc_enable_table (Transact-SQL)](../system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md). |
| `lock_escalation` | **tinyint** | A value of the `LOCK_ESCALATION` option for the table:<br /><br />0 = `TABLE`<br />1 = `DISABLE`<br />2 = `AUTO` |
| `lock_escalation_desc` | **nvarchar(60)** | A text description of the lock_escalation option for the table. Possible values are: `TABLE`, `AUTO`, and `DISABLE`. |
| `is_filetable` | **bit** | 1 = Table is a FileTable.<br /><br />For more information about FileTables, see [FileTables (SQL Server)](../blob/filetables-sql-server.md).<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_memory_optimized` | **bit** | The following are the possible values:<br /><br />0 = not memory optimized.<br />1 = is memory optimized.<br /><br />A value of 0 is the default value.<br /><br />Memory optimized tables are in-memory user tables, the schema of which is persisted on disk similar to other user tables. Memory optimized tables can be accessed from natively compiled stored procedures.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. |
| `durability` | **tinyint** | The following are possible values:<br /><br />0 = `SCHEMA_AND_DATA`<br />1 = `SCHEMA_ONLY`<br /><br />A value of `0` is the default value.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `durability_desc` | **nvarchar(60)** | The following are the possible values:<br /><br />`SCHEMA_ONLY`<br />`SCHEMA_AND_DATA`<br /><br />A value of `SCHEMA_AND_DATA` indicates that the table is a durable, in-memory table. `SCHEMA_AND_DATA` is the default value for memory optimized tables. A value of `SCHEMA_ONLY` indicates that the table data isn't persisted upon restart of the database with memory optimized objects.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `temporal_type` | **tinyint** | The numeric value representing the type of table:<br /><br />0 = `NON_TEMPORAL_TABLE`<br />1 = `HISTORY_TABLE` (associated with a temporal table)<br />2 = `SYSTEM_VERSIONED_TEMPORAL_TABLE`<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `temporal_type_desc` | **nvarchar(60)** | The text description of the type of table:<br /><br />`NON_TEMPORAL_TABLE`<br />`HISTORY_TABLE`<br />`SYSTEM_VERSIONED_TEMPORAL_TABLE`<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `history_table_id` | **int** | When `temporal_type` is `2` or `ledger_type` is `2`, returns `object_id` of the table that maintains historical data for a temporal table, otherwise returns `NULL`.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_remote_data_archive_enabled` | **bit** | Indicates whether the table is Stretch-enabled.<br /><br />0 = The table isn't Stretch-enabled.<br />1 = The table is Stretch-enabled.<br /><br />For more info, see [Stretch Database](/previous-versions/sql/sql-server/stretch-database/stretch-database).<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_external` | **bit** | Indicates table is an external table.<br /><br />0 = The table isn't an external table.<br />1 = The table is an external table.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] |
| `history_retention_period` | **int** | The numeric value representing duration of the temporal history retention period in units specified with `history_retention_period_unit`.<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `history_retention_period_unit` | **int** | The numeric value representing type of temporal history retention period unit.<br /><br />-1: `INFINITE`<br />0: `SECOND`<br />1: `MINUTE`<br />2: `HOUR`<br />3: `DAY`<br />4: `WEEK`<br />5: `MONTH`<br />6: `YEAR`<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `history_retention_period_unit_desc` | **nvarchar(10)** | The text description of type of temporal history retention period unit.<br /><br />`INFINITE`<br />`SECOND`<br />`MINUTE`<br />`HOUR`<br />`DAY`<br />`WEEK`<br />`MONTH`<br />`YEAR`<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_node` | **bit** | 1 = Graph node table.<br />0 = Not a graph node table.<br /><br />**Applies to**: [!INCLUDE [sssql17-md.md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_edge` | **bit** | 1 = Graph edge table.<br />0 = Not a graph edge table.<br /><br />**Applies to**: [!INCLUDE [sssql17-md.md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `data_retention_period` | **int** | The numeric value representing duration of the [data retention period](/azure/azure-sql-edge/data-retention-overview) in units specified with `data_retention_period_unit`.<br /><br />**Applies to**: Azure SQL Edge only |
| `data_retention_period_unit` | **int** | The numeric value representing type of data retention period unit.<br /><br />-1: `INFINITE`<br />0: `SECOND`<br />1: `MINUTE`<br />2: `HOUR`<br />3: `DAY`<br />4: `WEEK`<br />5: `MONTH`<br />6: `YEAR`<br /><br />**Applies to**: Azure SQL Edge only |
| `data_retention_period_unit_desc` | **nvarchar(10)** | The text description of type of data retention period unit.<br /><br />`INFINITE`<br />`SECOND`<br />`MINUTE`<br />`HOUR`<br />`DAY`<br />`WEEK`<br />`MONTH`<br />`YEAR`<br /><br />**Applies to**: Azure SQL Edge only |
| `ledger_type` | **tinyint** | The numeric value indicates if the table is a ledger table.<br /><br />0 = `NON_LEDGER_TABLE`<br />1 = `HISTORY_TABLE` (associated with an updatable ledger table)<br />2 = `UPDATABLE_LEDGER_TABLE`<br />3 = `APPEND_ONLY_LEDGER_TABLE`<br /><br />For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `ledger_type_desc` | **nvarchar(60)** | The text description of a value in the `ledger_type` column:<br /><br />`NON_LEDGER_TABLE`<br />`HISTORY_TABLE`<br />`UPDATABLE_LEDGER_TABLE`<br />`APPEND_ONLY_LEDGER_TABLE`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `ledger_view_id` | **int** | When `ledger_type IN (2, 3)` returns `object_id` of the ledger view, otherwise returns `NULL`.<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `is_dropped_ledger_table` | **bit** | Indicates a ledger table that was dropped.<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. Return all user tables without a primary key

The following example returns all of the user tables that don't have a primary key.

```sql
SELECT SCHEMA_NAME(schema_id) AS schema_name, name AS table_name
FROM sys.tables
WHERE OBJECTPROPERTY(object_id, 'TableHasPrimaryKey') = 0
ORDER BY schema_name, table_name;
GO
```

### B. List temporal data related tables

The following example shows how related temporal data can be exposed.

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
SELECT T1.object_id,
    T1.name AS TemporalTableName,
    SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
    T2.name AS HistoryTableName,
    SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,
    T1.temporal_type_desc
FROM sys.tables T1
LEFT JOIN sys.tables T2
    ON T1.history_table_id = T2.object_id
ORDER BY T1.temporal_type DESC;
```

### C. List information about temporal history retention

The following example shows how information on temporal history retention can be exposed.

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
SELECT DB.is_temporal_history_retention_enabled,
    SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
    T1.name AS TemporalTableName,
    SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,
    T2.name AS HistoryTableName,
    T1.history_retention_period,
    T1.history_retention_period_unit_desc
FROM sys.tables T1
OUTER APPLY (
    SELECT is_temporal_history_retention_enabled
    FROM sys.databases
    WHERE name = DB_NAME()
) DB
LEFT JOIN sys.tables T2
    ON T1.history_table_id = T2.object_id
WHERE T1.temporal_type = 2;
```

## Related content

- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [DBCC CHECKDB (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)
- [DBCC CHECKTABLE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
