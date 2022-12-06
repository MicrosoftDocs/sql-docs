---
title: "CREATE COLUMNSTORE INDEX (Transact-SQL)"
description: "CREATE COLUMNSTORE INDEX converts a rowstore table to a clustered columnstore index, or creates a nonclustered columnstore index."
author: markingmyname
ms.author: maghan
ms.date: 10/14/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_COLUMNSTORE_INDEX_TSQL"
  - "COLUMNSTORE INDEX"
  - "COLUMNSTORE_INDEX_TSQL"
  - "CREATE CLUSTERED COLUMNSTORE INDEX"
  - "COLUMNSTORE_TSQL"
  - "CREATE NONCLUSTERED COLUMNSTORE INDEX"
  - "CREATE_NONCLUSTERED_COLUMNSTORE_INDEX_TSQL"
  - "CREATE COLUMNSTORE INDEX"
  - "CREATE_CLUSTERED_COLUMNSTORE_INDEX_TSQL"
  - "COLUMNSTORE"
helpviewer_keywords:
  - "index creation [SQL Server], columnstore indexes"
  - "columnstore index, creating"
  - "CREATE COLUMNSTORE INDEX statement"
  - "CREATE INDEX statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE COLUMNSTORE INDEX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Convert a rowstore table to a clustered columnstore index, or create a nonclustered columnstore index. Use a columnstore index to efficiently run real-time operational analytics on an OLTP workload, or to improve data compression and query performance for data warehousing workloads.

Follow [What's new in columnstore indexes](../../relational-databases/indexes/columnstore-indexes-what-s-new.md) for the latest improvements to this feature.

 - Ordered clustered columnstore indexes were introduced in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]. For more information, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md#order).

 - Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], you can create the table as a clustered columnstore index. It's no longer necessary to first create a rowstore table and then convert it to a clustered columnstore index.

 - For information on columnstore index design guidelines, see [Columnstore indexes - Design guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md).

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
-- Syntax for SQL Server and Azure SQL Database
  
-- Create a clustered columnstore index on disk-based table.
CREATE CLUSTERED COLUMNSTORE INDEX index_name
    ON { database_name.schema_name.table_name | schema_name.table_name | table_name }
    [ WITH ( < with_option> [ ,...n ] ) ]
    [ ON <on_option> ] | [ ORDER <on_option> ]
    
[ ; ]
  
--Create a nonclustered columnstore index on a disk-based table.
CREATE [NONCLUSTERED]  COLUMNSTORE INDEX index_name
    ON { database_name.schema_name.table_name | schema_name.table_name | table_name }
        ( column  [ ,...n ] )
    [ WHERE <filter_expression> [ AND <filter_expression> ] ]
    [ WITH ( < with_option> [ ,...n ] ) ]
    [ ON <on_option> ]
[ ; ]
  
<with_option> ::=
      DROP_EXISTING = { ON | **OFF** } -- default is OFF
    | MAXDOP = max_degree_of_parallelism
    | ONLINE = { ON | OFF }
    | COMPRESSION_DELAY  = { 0 | delay [ Minutes ] }
    | DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }
      [ ON PARTITIONS ( { partition_number_expression | range } [ ,...n ] ) ]
  
<on_option>::=
      partition_scheme_name ( column_name )
    | filegroup_name
    | "default"
  
<filter_expression> ::=
      column_name IN ( constant [ ,...n ]
    | column_name { IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !< } constant
```

```syntaxsql
-- Syntax for Azure Synapse Analytics, Parallel Data Warehouse, [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later
  
CREATE CLUSTERED COLUMNSTORE INDEX index_name
    ON { database_name.schema_name.table_name | schema_name.table_name | table_name }
    [ ORDER (column [,...n] ) ]
    [ WITH ( DROP_EXISTING = { ON | **OFF** } ) ] -- default is OFF
[;]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

Some of the options aren't available in all database engine versions. The following table shows the versions when the options are introduced in CLUSTERED COLUMNSTORE and NONCLUSTERED COLUMNSTORE indexes:

|Option| CLUSTERED | NONCLUSTERED |
|---|---|---|
| COMPRESSION_DELAY | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] |
| DATA_COMPRESSION | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] |
| ONLINE | [!INCLUDE[ssSQLv15_md](../../includes/sssql19-md.md)] | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] |
| WHERE clause | N/A | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] |

All options are available in Azure SQL Database.

### CREATE CLUSTERED COLUMNSTORE INDEX

Create a clustered columnstore index in which all of the data is compressed and stored by column. The index includes all of the columns in the table, and stores the entire table. If the existing table is a heap or clustered index, then it will be converted to a clustered columnstore index. If the table is already stored as a clustered columnstore index, then the existing index is dropped and rebuilt.

#### *index_name*

Specifies the name for the new index.

If the table already has a clustered columnstore index, you can specify the same name as the existing index, or you can use the DROP EXISTING option to specify a new name.

#### ON [*database_name*. [*schema_name* ] . | *schema_name* . ] *table_name*

Specifies the one-, two-, or three-part name of the table to be stored as a clustered columnstore index. If the table is a heap or has a clustered index, then the table is converted from a rowstore to a columnstore. If the table is already a columnstore, this statement rebuilds the clustered columnstore index.

#### ORDER

*Applies to [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later*

Use the `column_store_order_ordinal` column in [sys.index_columns](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md) to determine the order of the column(s) for a clustered columnstore index. This aids with [segment elimination](../../relational-databases/indexes/columnstore-indexes-query-performance.md#segment-elimination), especially with string data. For more information, see [Performance tuning with ordered clustered columnstore index](/azure/synapse-analytics/sql-data-warehouse/performance-tuning-ordered-cci) and [Columnstore indexes design guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md).

To convert to an ordered clustered column store index, the existing index must be a clustered columnstore index. Use the `DROP_EXISTING` option.

LOB data types (the (max) length data types) cannot be the key of an ordered clustered columnstore index.

When creating an ordered clustered columnstore index, use `OPTION(MAXDOP = 1)` for the highest quality sorting with the `CREATE INDEX` statement, in exchange for a significantly longer duration of the `CREATE INDEX` statement. To create the index as fast as possible, do not limit MAXDOP, and use all the parallel threading the server can provide. The highest quality of compression and sorting could aid queries on the columnstore index.

When an ordered clustered columnstore index is created, the key columns are indicated by the `column_store_order_ordinal` column in `sys.index_columns`.

### WITH options

##### DROP_EXISTING = [OFF] | ON

`DROP_EXISTING = ON` specifies to drop the existing index, and create a new columnstore index.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
WITH (DROP_EXISTING = ON);
```

The default, DROP_EXISTING = OFF, expects the index name is the same as the existing name. An error occurs if the specified index name already exists.

##### MAXDOP = *max_degree_of_parallelism*

This option can override the existing maximum degree of parallelism server configuration during the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* values can be:

- 1, which means to suppress parallel plan generation.  
- \>1, which means to restrict the maximum number of processors used in a parallel index operation to the specified number, or fewer, based on the current system workload. For example, when MAXDOP = 4, the number of processors used is 4 or less.  
- 0 (default), which means to use the actual number of processors, or fewer, based on the current system workload.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
WITH (MAXDOP = 2);
```

For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md), and [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

###### COMPRESSION_DELAY = 0 | *delay* [ Minutes ]

For a disk-based table, *delay* specifies the minimum number of minutes that a delta rowgroup in the closed state must remain in the delta rowgroup. SQL Server can then compress it into the compressed rowgroup. Because disk-based tables don't track insert and update times on individual rows, SQL Server applies the delay to delta rowgroups in the closed state.

The default is 0 minutes.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
WITH ( COMPRESSION_DELAY = 10 MINUTES );
```

For recommendations on when to use COMPRESSION_DELAY, see [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).

##### DATA_COMPRESSION = COLUMNSTORE | COLUMNSTORE_ARCHIVE

Specifies the data compression option for the specified table, partition number, or range of partitions. The options are as follows:

- `COLUMNSTORE` is the default, and specifies to compress with the most performant columnstore compression. This option is the typical choice.  
- `COLUMNSTORE_ARCHIVE` further compresses the table or partition to a smaller size. Use this option for situations such as archival, which requires a smaller storage size and can afford more time for storage and retrieval.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
WITH ( DATA_COMPRESSION = COLUMNSTORE_ARCHIVE );
```
For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).

###### ONLINE = [ON | OFF]

- `ON` specifies that the columnstore index remains online and available, while the new copy of the index is being built.
- `OFF` specifies that the index isn't available for use while the new copy is being built.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
WITH ( ONLINE = ON );
```

#### ON options

With these options, you can specify options for data storage, such as a partition scheme, a specific filegroup, or the default filegroup. If the ON option isn't specified, the index uses the settings partition or filegroup settings of the existing table.

*partition_scheme_name* **(** _column_name_ **)** specifies the partition scheme for the table. The partition scheme must already exist in the database. To create the partition scheme, see [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md).

*column_name* specifies the column against which a partitioned index is partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using.

*filegroup_name* specifies the filegroup for storing the clustered columnstore index. If no location is specified and the table isn't partitioned, the index uses the same filegroup as the underlying table or view. The filegroup must already exist.

To create the index on the default filegroup, use "default" or [ default ]. If you specify "default", the QUOTED_IDENTIFIER option must be ON for the current session. QUOTED_IDENTIFIER is ON by default. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

### CREATE [NONCLUSTERED] COLUMNSTORE INDEX

Create a nonclustered columnstore index on a rowstore table stored as a heap or clustered index. The index can have a filtered condition, and doesn't need to include all of the columns of the underlying table. The columnstore index requires enough space to store a copy of the data. You can update the index, and it's updated as the underlying table is changed. The nonclustered columnstore index on a clustered index enables real-time analytics.

#### *index_name*

Specifies the name of the index. *index_name*  must be unique within the table, but doesn't have to be unique within the database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

#### ( *column* [ ,...*n* ] )

Specifies the columns to store. A nonclustered columnstore index is limited to 1024 columns.
Each column must be of a supported data type for columnstore indexes. See [Limitations and restrictions](../../t-sql/statements/create-columnstore-index-transact-sql.md#LimitRest) for a list of the supported data types.

#### ON [*database_name*. [*schema_name* ] . | *schema_name* . ] *table_name*

Specifies the one-, two-, or three-part name of the table that contains the index.

#### WITH options

##### DROP_EXISTING = [OFF] | ON

DROP_EXISTING = ON
The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, or index options.

DROP_EXISTING = OFF  
An error is shown if the specified index name already exists. The index type can't be changed by using DROP_EXISTING. In backward-compatible syntax, WITH DROP_EXISTING is equivalent to WITH DROP_EXISTING = ON.

##### MAXDOP = *max_degree_of_parallelism*

Overrides the [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) configuration option during the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* values can be:

- 1, which means to suppress parallel plan generation.  
- \>1, which means to restrict the maximum number of processors used in a parallel index operation to the specified number, or fewer, based on the current system workload. For example, when MAXDOP = 4, the number of processors used is 4 or less.  
- 0 (default), which means to use the actual number of processors or fewer based on the current system workload.

For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations aren't available in every edition of [!INCLUDE[msC](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).

##### ONLINE = [ON | OFF]

- `ON` specifies that the columnstore index remains online and available, while the new copy of the index is being built.
- `OFF` specifies that the index isn't available for use while the new copy is being built. In a nonclustered index, the base table remains available. Only the nonclustered columnstore index isn't used to satisfy queries until the new index is complete.

```sql
CREATE COLUMNSTORE INDEX ncci ON Sales.OrderLines (StockItemID, Quantity, UnitPrice, TaxRate)
WITH ( ONLINE = ON );
```

##### COMPRESSION_DELAY = 0 | \<delay>[Minutes]

Specifies a lower bound on how long a row should stay in a delta rowgroup, before it's eligible for migration to a compressed rowgroup. For example, you can say that if a row is unchanged for 120 minutes, that row is eligible for compressing into columnar storage format.

For a columnstore index on disk-based tables, the time when a row was inserted or updated isn't tracked. Instead, the delta rowgroup closed time is used as a proxy for the row. The default duration is 0 minutes. A row is migrated to columnar storage after 1 million rows have accumulated in the delta rowgroup, and it has been marked closed.

##### DATA_COMPRESSION

Specifies the data compression option for the specified table, partition number, or range of partitions. Applies only to columnstore indexes, including both nonclustered and clustered. The options are as follows:

- `COLUMNSTORE` is the default, and specifies to compress with the most performant columnstore compression. This option is the typical choice.  
- `COLUMNSTORE_ARCHIVE` further compresses the table or partition to a smaller size. You can use this option for archival, or for other situations that require a smaller storage size and can afford more time for storage and retrieval.

For more information about compression, see [Data compression](../../relational-databases/data-compression/data-compression.md).

##### WHERE \<filter_expression> [ AND \<filter_expression> ]

Called a filter predicate, this option specifies which rows to include in the index. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates filtered statistics on the data rows in the filtered index.

The filter predicate uses simple comparison logic. Comparisons that use NULL literals aren't allowed with the comparison operators. Use the IS NULL and IS NOT NULL operators instead.

Here are some examples of filter predicates for the `Production.BillOfMaterials` table:  
`WHERE StartDate > '20000101' AND EndDate <= '20000630'`  
`WHERE ComponentID IN (533, 324, 753)`  
`WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL`

For guidance on filtered indexes, see [Create filtered indexes](../../relational-databases/indexes/create-filtered-indexes.md).

#### ON options

The following options specify the filegroups on which the index is created.

##### *partition_scheme_name* ( *column_name* )

Specifies the partition scheme that defines the filegroups onto which the partitions of a partitioned index are mapped. The partition scheme must exist within the database by executing [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md).

*column_name* specifies the column against which a partitioned index is partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using. *column_name* isn't restricted to the columns in the index definition. When partitioning a columnstore index, [!INCLUDE[ssDE](../../includes/ssde-md.md)] adds the partitioning column as a column of the index, if it isn't already specified.

If the table is partitioned, and *partition_scheme_name* or *filegroup* aren't specified, then the index is placed in the same partition scheme and uses the same partitioning column as the underlying table.

A columnstore index on a partitioned table must be partition aligned. For more information about partitioning indexes, see [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).

##### *filegroup_name*

Specifies a filegroup name on which to create the index. If *filegroup_name* isn't specified and the table isn't partitioned, the index uses the same filegroup as the underlying table. The filegroup must already exist.

##### "default"

Creates the specified index on the default filegroup.

The term default, in this context, isn't a keyword. It's an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session, which is the default setting. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

## Permissions

Requires ALTER permission on the table.

## <a id="GenRemarks"></a> Remarks

You can create a columnstore index on a temporary table. When the table is dropped or the session ends, the index is also dropped.

## Filtered indexes

A *filtered index* is an optimized, nonclustered index, suited for queries that select a small percentage of rows from a table. It uses a filter predicate to index a portion of the data in the table. A well-designed filtered index can improve query performance, reduce storage costs, and reduce maintenance costs.

### Required SET options for filtered indexes

The SET options in the required value column are required whenever any of the following conditions occur:

- You create a filtered index.  
- An INSERT, UPDATE, DELETE, or MERGE operation modifies the data in a filtered index.  
- The query optimizer uses the filtered index to produce the query plan.

|SET options|Required value|Default server value|Default OLE DB and ODBC value|Default DB-Library value|
|-----------------|--------------------|--------------------------|---------------------------------------|-----------------------------------|
|ANSI_NULLS|ON|ON|ON|OFF|
|ANSI_PADDING|ON|ON|ON|OFF|
|ANSI_WARNINGS*|ON|ON|ON|OFF|
|ARITHABORT|ON|ON|OFF|OFF|
|CONCAT_NULL_YIELDS_NULL|ON|ON|ON|OFF|
|NUMERIC_ROUNDABORT|OFF|OFF|OFF|OFF|
|QUOTED_IDENTIFIER|ON|ON|ON|OFF|

*Setting ANSI_WARNINGS to ON implicitly sets ARITHABORT to ON when the database compatibility level is set to 90 or later. If the database compatibility level is set to 80 or earlier, you must explicitly set the ARITHABORT option to ON.

If the SET options are incorrect, the following conditions can occur:

- The filtered index isn't created.

- The [!INCLUDE[ssDE](../../includes/ssde-md.md)] generates an error, and rolls back INSERT, UPDATE, DELETE, or MERGE statements that change data in the index.

- The query optimizer doesn't consider the index in the execution plan for any Transact-SQL statements.

For more information about filtered indexes, see [Create filtered indexes](../../relational-databases/indexes/create-filtered-indexes.md).

## <a id="LimitRest"></a> Limitations and restrictions

Each column in a columnstore index must be of one of the following common business data types:

- datetimeoffset [ ( *n* ) ]  
- datetime2 [ ( *n* ) ]  
- datetime  
- smalldatetime  
- date  
- time [ ( *n* ) ]  
- float [ ( *n* ) ]  
- real [ ( *n* ) ]  
- decimal [ ( *precision* [ *, scale* ] ) ]
- numeric [ ( *precision* [ *, scale* ] ) ]    
- money  
- smallmoney  
- bigint  
- int  
- smallint  
- tinyint  
- bit  
- nvarchar [ ( *n* ) ]
- nvarchar(max) (Applies to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and Premium tier, Standard tier [S3 and above], and all vCore offerings tiers, in clustered columnstore indexes only.)  
- nchar [ ( *n* ) ]  
- varchar [ ( *n* ) ]  
- varchar(max) (Applies to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and Premium tier, Standard tier [S3 and above], and all vCore offerings tiers, in clustered columnstore indexes only.)
- char [ ( *n* ) ]  
- varbinary [ ( *n* ) ]
- varbinary (max) (Applies to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and Azure SQL Database at Premium tier, Standard tier [S3 and above], and all vCore offerings tiers, in clustered columnstore indexes only.)
- binary [ ( *n* ) ]  
- uniqueidentifier (Applies to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.)

If the underlying table has a column of a data type that isn't supported for columnstore indexes, you must omit that column from the nonclustered columnstore index.

Large object (LOB) data greater than 8 kilobytes is stored in off-row, LOB storage, with just a pointer to the physical location stored within the column segment. The size of the data stored isn't reported in [sys.column_store_segments](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md), [sys.column_store_dictionaries](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md), or [sys.dm_db_column_store_row_group_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md).

Columns that use any of the following data types can't be included in a columnstore index:

- ntext, text, and image  
- nvarchar(max), varchar(max), and varbinary(max) (Applies to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and prior versions, and nonclustered columnstore indexes.)
- rowversion (and timestamp)  
- sql_variant  
- CLR types (hierarchyid and spatial types)  
- xml  
- uniqueidentifier (Applies to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].)

**Nonclustered columnstore indexes:**

- Can't have more than 1024 columns.
- Can't be created as a constraint-based index. It's possible to have unique constraints, primary key constraints, and foreign key constraints on a table with a columnstore index. Constraints are always enforced with a row-store index. Constraints can't be enforced with a columnstore (clustered or nonclustered) index.
- Can't include a sparse column.  
- Can't be changed by using the ALTER INDEX statement. To change the nonclustered index, you must drop and re-create the columnstore index instead. You can use ALTER INDEX to disable and rebuild a columnstore index.  
- Can't be created by using the INCLUDE keyword.  
- Can't include the ASC or DESC keywords for sorting the index. Columnstore indexes are ordered according to the compression algorithms. Sorting would eliminate many of the performance benefits. In [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], and starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can specify an order for the columns in a columnstore index. For more information, see [Performance tuning with ordered clustered columnstore index](/azure/synapse-analytics/sql-data-warehouse/performance-tuning-ordered-cci).
- Can't include LOB columns of type nvarchar(max), varchar(max), and varbinary(max) in nonclustered columnstore indexes. Only clustered columnstore indexes support LOB types, beginning in the  [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] version, Azure SQL Database (configured at Premium tier, Standard tier [S3 and above], and all vCore offerings tiers). Note that prior versions don't support LOB types in clustered and nonclustered columnstore indexes.
- Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], you can create a nonclustered columnstore index on an indexed view.

**Columnstore indexes can't be combined with the following features:**

- Computed columns. Starting with SQL Server 2017, a clustered columnstore index can contain a non-persisted computed column. However, in SQL Server 2017, clustered columnstore indexes can't contain persisted computed columns, and you can't create nonclustered indexes on computed columns.
- Page and row compression, and the vardecimal storage format. (A columnstore index is already compressed in a different format.)  
- Replication.  
- Filestream.

You can't use cursors or triggers on a table with a clustered columnstore index. This restriction doesn't apply to nonclustered columnstore indexes. You can use cursors and triggers on a table with a nonclustered columnstore index.

**[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] specific limitations:**

The following limitations apply only to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. In this release, you can use updateable, clustered columnstore indexes. Nonclustered columnstore indexes are still read-only.

- Change tracking. You can't use change tracking with columnstore indexes.  
- Change data capture. This feature can't be enabled on tables with a clustered columnstore index. Starting with SQL Server 2016, change data capture can be enabled on tables with a nonclustered columnstore index.  
- Readable secondary. You can't access a clustered columnstore index (CCI) from a readable secondary of an Always On readable availability group. You can access a nonclustered columnstore index (NCCI) from a readable secondary.  
- Multiple Active Result Sets (MARS). [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] uses this feature for read-only connections to tables with a columnstore index. However, [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] doesn't support this feature for concurrent data manipulation language (DML) operations on a table with a columnstore index. If you try to use the feature for this purpose, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] terminates the connections and cancels the transactions.  
- Nonclustered columnstore indexes can't be created on a view or indexed view.

For information about the performance benefits and limitations of columnstore indexes, see [Columnstore indexes overview](../../relational-databases/indexes/columnstore-indexes-overview.md).

## Metadata

All of the columns in a columnstore index are stored in the metadata as included columns. The columnstore index doesn't have key columns. The following system views provide information about columnstore indexes:

- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.partitions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)
- [sys.column_store_segments (Transact-SQL)](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md)
- [sys.column_store_dictionaries (Transact-SQL)](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md)
- [sys.column_store_row_groups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql.md)

## <a id="convert"></a> Examples: convert table from rowstore to columnstore

### A. Convert a heap to a clustered columnstore index

This example creates a table as a heap, and then converts it to a clustered columnstore index named `cci_Simple`. The creation of the clustered columnstore index changes the storage for the entire table from rowstore to columnstore.

```sql
CREATE TABLE dbo.SimpleTable(
    ProductKey [INT] NOT NULL,
    OrderDateKey [INT] NOT NULL,
    DueDateKey [INT] NOT NULL,
    ShipDateKey [INT] NOT NULL);
GO
CREATE CLUSTERED COLUMNSTORE INDEX cci_Simple ON dbo.SimpleTable;
GO
```

### B. Convert a clustered index to a clustered columnstore index with the same name

This example creates a table with clustered index, and then demonstrates the syntax of converting the clustered index to a clustered columnstore index. The creation of the clustered columnstore index changes the storage for the entire table from rowstore to columnstore.

```sql
CREATE TABLE dbo.SimpleTable2 (
    ProductKey [INT] NOT NULL,
    OrderDateKey [INT] NOT NULL,
    DueDateKey [INT] NOT NULL,
    ShipDateKey [INT] NOT NULL);
GO
CREATE CLUSTERED INDEX cl_simple ON dbo.SimpleTable2 (ProductKey);
GO
CREATE CLUSTERED COLUMNSTORE INDEX cl_simple ON dbo.SimpleTable2
WITH (DROP_EXISTING = ON);
GO
```

### C. Handle nonclustered indexes when converting a rowstore table to a columnstore index

This example shows how to handle nonclustered indexes when you convert a rowstore table to a columnstore index. Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], no special action is required. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically defines and rebuilds the nonclustered indexes on the new, clustered columnstore index.

If you want to drop the nonclustered indexes, use the DROP INDEX statement prior to creating the columnstore index. The DROP EXISTING option only drops the clustered index that is being converted. It doesn't drop the nonclustered indexes.

In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], you can't create a nonclustered index on a columnstore index.

```sql
--Create the table for use with this example.
CREATE TABLE dbo.SimpleTable (
    ProductKey [INT] NOT NULL,
    OrderDateKey [INT] NOT NULL,
    DueDateKey [INT] NOT NULL,
    ShipDateKey [INT] NOT NULL);
GO
  
--Create two nonclustered indexes for use with this example
CREATE INDEX nc1_simple ON dbo.SimpleTable (OrderDateKey);
CREATE INDEX nc2_simple ON dbo.SimpleTable (DueDateKey);
GO
```

Note that only for [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)], you must drop the nonclustered indexes in order to create the columnstore index.

```sql
DROP INDEX dbo.SimpleTable.nc1_simple;
DROP INDEX dbo.SimpleTable.nc2_simple;
  
--Convert the rowstore table to a columnstore index.
CREATE CLUSTERED COLUMNSTORE INDEX cci_simple ON dbo.SimpleTable;
GO
```

### D. Convert a large fact table from rowstore to columnstore

This example explains how to convert a large fact table from a rowstore table to a columnstore table.

1. Create a small table to use in this example.

    ```sql
    --Create a rowstore table with a clustered index and a nonclustered index.
    CREATE TABLE dbo.MyFactTable (
        ProductKey [INT] NOT NULL,
        OrderDateKey [INT] NOT NULL,
        DueDateKey [INT] NOT NULL,
        ShipDateKey [INT] NOT NULL
    INDEX IDX_CL_MyFactTable CLUSTERED  ( ProductKey )
    );
  
    --Add a nonclustered index.
    CREATE INDEX my_index ON dbo.MyFactTable ( ProductKey, OrderDateKey );
    ```

1. Drop all nonclustered indexes from the rowstore table. You might want to [script out the indexes to re-create them later](../../ssms/tutorials/scripting-ssms.md#script-a-database-by-using-the-generate-scripts-option).

    ```sql
    --Drop all nonclustered indexes
    DROP INDEX my_index ON dbo.MyFactTable;
    ```

1. Convert the rowstore table to a columnstore table with a clustered columnstore index.

    First, look up the name of the existing clustered rowstore index. In Step 1, we set the name of the index to `IDX_CL_MyFactTable`. If the index name wasn't specified, it was given an automatically generated unique index name. You can retrieve the automatically generated name with the following sample query:

    ```sql
    SELECT i.object_id, i.name, t.object_id, t.name
    FROM sys.indexes i
    INNER JOIN sys.tables t ON i.object_id = t.object_id
    WHERE i.type_desc = 'CLUSTERED'
    AND t.name = 'MyFactTable';
    ```

    Option 1: Drop the existing clustered index `IDX_CL_MyFactTable`, and convert `MyFactTable` to columnstore. Change the name of the new clustered columnstore index.

    ```sql
    --Drop the clustered rowstore index.
    DROP INDEX [IDX_CL_MyFactTable]
    ON dbo.MyFactTable;
    GO
    --Create a new clustered columnstore index with the name MyCCI.
    CREATE CLUSTERED COLUMNSTORE
    INDEX IDX_CCL_MyFactTable ON dbo.MyFactTable;
    GO
    ```

    Option 2: Convert to columnstore, and reuse the existing rowstore clustered index name.

    ```sql
    --Create the clustered columnstore index,
    --replacing the existing rowstore clustered index of the same name
    CREATE CLUSTERED COLUMNSTORE
    INDEX [IDX_CL_MyFactTable]
    ON dbo.MyFactTable
    WITH (DROP_EXISTING = ON);
    ```

### E. Convert a columnstore table to a rowstore table with a clustered index

To convert a columnstore table to a rowstore table with a clustered index, use the CREATE INDEX statement with the DROP_EXISTING option.

```sql
CREATE CLUSTERED INDEX [IDX_CL_MyFactTable]
ON dbo.[MyFactTable] ( ProductKey )
WITH ( DROP_EXISTING = ON );
```

### F. Convert a columnstore table to a rowstore heap

To convert a columnstore table to a rowstore heap, simply drop the clustered columnstore index. This isn't typically recommended, but can some have narrow uses. For more information about heaps, see [Heaps (tables without clustered indexes)](../../relational-databases/indexes/heaps-tables-without-clustered-indexes.md).

```sql
DROP INDEX [IDX_CL_MyFactTable]
ON dbo.[MyFactTable];
```

<a id="#g-defragment-by-rebuilding-the-entire-clustered-columnstore-index"></a>

### G. Defragment by reorganizing the columnstore index

There are two ways to maintain the clustered columnstore index. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], use `ALTER INDEX...REORGANIZE` instead of REBUILD. For more information, see [Columnstore index rowgroup](../../relational-databases/indexes/columnstore-indexes-overview.md#rowgroup). In previous versions of SQL Server, you can use CREATE CLUSTERED COLUMNSTORE INDEX with DROP_EXISTING=ON, or [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md) and the REBUILD option. Both methods achieved the same results.

Start by determining the clustered columnstore index name in `MyFactTable`.

```sql
SELECT i.object_id, i.name, t.object_id, t.name
FROM sys.indexes i
INNER JOIN sys.tables t on i.object_id = t.object_id
WHERE i.type_desc = 'CLUSTERED COLUMNSTORE'
AND t.name = 'MyFactTable';
```

Remove fragmentation by performing a REORGANIZE on the columnstore index.

```sql
--Rebuild the entire index by using ALTER INDEX and the REBUILD option.
ALTER INDEX IDX_CL_MyFactTable
ON dbo.[MyFactTable]
REORGANIZE;
```

## <a id="nonclustered"></a> Examples for nonclustered columnstore indexes

### A. Create a columnstore index as a secondary index on a rowstore table

This example creates a nonclustered columnstore index on a rowstore table. Only one columnstore index can be created in this situation. The columnstore index requires extra storage, because it contains a copy of the data in the rowstore table. This example creates a simple table and a rowstore clustered index, and then demonstrates the syntax of creating a nonclustered columnstore index.

```sql
CREATE TABLE dbo.SimpleTable (
    ProductKey [INT] NOT NULL,
    OrderDateKey [INT] NOT NULL,
    DueDateKey [INT] NOT NULL,
    ShipDateKey [INT] NOT NULL);
GO

CREATE CLUSTERED INDEX cl_simple ON dbo.SimpleTable (ProductKey);
GO

CREATE NONCLUSTERED COLUMNSTORE INDEX csindx_simple
ON dbo.SimpleTable (OrderDateKey, DueDateKey, ShipDateKey);
GO
```

### B. Create a simple, nonclustered columnstore index by using all options

The following example demonstrates the syntax of creating a nonclustered columnstore index on the DEFAULT filegroup, specifying the maximum degrees of parallelism (MAXDOP) as 2.

```sql
CREATE NONCLUSTERED COLUMNSTORE INDEX csindx_simple
ON SimpleTable (OrderDateKey, DueDateKey, ShipDateKey)
WITH (DROP_EXISTING =  ON,
    MAXDOP = 2)
ON "DEFAULT";
GO
```

### C. Create a nonclustered columnstore index with a filtered predicate

The following example creates a filtered, nonclustered columnstore index on the `Production.BillOfMaterials` table in the `AdventureWorks2019` sample database. The filter predicate can include columns that aren't key columns in the filtered index. The predicate in this example selects only the rows where `EndDate` is non-NULL.

```sql
IF EXISTS (SELECT name FROM sys.indexes
    WHERE name = N'FIBillOfMaterialsWithEndDate'
    AND object_id = OBJECT_ID(N'Production.BillOfMaterials'))
DROP INDEX FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials;
GO
CREATE NONCLUSTERED COLUMNSTORE INDEX "FIBillOfMaterialsWithEndDate"
    ON Production.BillOfMaterials (ComponentID, StartDate)
    WHERE EndDate IS NOT NULL;
```

### <a id="ncDML"></a> D. Change the data in a nonclustered columnstore index

Applies to: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].

Prior to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], after you create a nonclustered columnstore index on a table, you can't directly modify the data in that table. A query with INSERT, UPDATE, DELETE, or MERGE fails and returns an error message. Here are options you can use to add or modify the data in the table:

- Disable or drop the columnstore index. You can then update the data in the table. If you disable the columnstore index, you can rebuild the columnstore index when you finish updating the data. For example:

    ```sql
    ALTER INDEX mycolumnstoreindex ON dbo.mytable DISABLE;
    -- update the data in mytable as necessary
    ALTER INDEX mycolumnstoreindex on dbo.mytable REBUILD;
    ```

- Load data into a staging table that doesn't have a columnstore index. Build a columnstore index on the staging table. Switch the staging table into an empty partition of the main table.

- Switch a partition from the table with the columnstore index into an empty staging table. If there is a columnstore index on the staging table, disable the columnstore index. Perform any updates. Build (or rebuild) the columnstore index. Switch the staging table back into the (now empty) partition of the main table.

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### A. Change a clustered index to a clustered columnstore index

By using the CREATE CLUSTERED COLUMNSTORE INDEX statement with DROP_EXISTING = ON, you can:

- Change a clustered index into a clustered columnstore index.

- Rebuild a clustered columnstore index.

This example creates the `xDimProduct` table as a rowstore table with a clustered index. Then the example uses CREATE CLUSTERED COLUMNSTORE INDEX to change the table from a rowstore table to a columnstore table.

```sql
-- Uses AdventureWorks
  
IF EXISTS (SELECT name FROM sys.tables
    WHERE name = N'xDimProduct'
    AND object_id = OBJECT_ID (N'xDimProduct'))
DROP TABLE xDimProduct;
  
--Create a distributed table with a clustered index.
CREATE TABLE xDimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey)
WITH ( DISTRIBUTION = HASH(ProductKey),
    CLUSTERED INDEX (ProductKey) )
AS SELECT ProductKey, ProductAlternateKey, ProductSubcategoryKey FROM DimProduct;
```

Look up the name of the clustered index automatically created for the new table in the system metadata, using `sys.indexes`. For example:

```sql
SELECT i.object_id, i.name, t.object_id, t.name, i.type_desc
FROM sys.indexes i
INNER JOIN sys.tables t ON i.object_id = t.object_id
WHERE i.type_desc = 'CLUSTERED'
AND t.name = 'xdimProduct';
```

Now, you can choose to:

1. Drop the existing clustered columnstore index with an automatically created name, then create a new clustered columnstore index with a user-defined name. 
1. Drop and replace the existing index with a clustered columnstore index, keeping the same system-generated name, such as `ClusteredIndex_1bd8af8797f7453182903cc68df48541`.

For example:

```sql
--1. DROP the existing clustered columnstore index with an automatically-created name, for example:
DROP INDEX ClusteredIndex_1bd8af8797f7453182903cc68df48541 on xdimProduct;
GO
CREATE CLUSTERED COLUMNSTORE INDEX [<new_index_name>]
ON xdimProduct;
GO

--Or,
--2. Change the existing clustered index to a clustered columnstore index with the same name.
CREATE CLUSTERED COLUMNSTORE INDEX [ClusteredIndex_1bd8af8797f7453182903cc68df48541]
ON xdimProduct
WITH ( DROP_EXISTING = ON );
GO
```

### B. Rebuild a clustered columnstore index

Building on the previous example, this example uses CREATE CLUSTERED COLUMNSTORE INDEX to rebuild the existing clustered columnstore index, called `cci_xDimProduct`.

```sql
--Rebuild the existing clustered columnstore index.
CREATE CLUSTERED COLUMNSTORE INDEX cci_xDimProduct
ON xdimProduct
WITH ( DROP_EXISTING = ON );
```

### C. Change the name of a clustered columnstore index

To change the name of a clustered columnstore index, drop the existing clustered columnstore index, and then re-create the index with a new name.

We recommend that you limit this operation to a small or empty table. It takes a long time to drop a large, clustered columnstore index, and rebuild with a different name.

This example references the `cci_xDimProduct` clustered columnstore index from the previous example. This example drops the `cci_xDimProduct` clustered columnstore index, and then re-creates the clustered columnstore index with the name `mycci_xDimProduct`.

```sql
--For illustration purposes, drop the clustered columnstore index.
--The table continues to be distributed, but changes to a heap.
DROP INDEX cci_xdimProduct ON xDimProduct;
  
--Create a clustered index with a new name, mycci_xDimProduct.
CREATE CLUSTERED COLUMNSTORE INDEX mycci_xDimProduct
ON xdimProduct
WITH ( DROP_EXISTING = OFF );
```

### D. Convert a columnstore table to a rowstore table with a clustered index

There might be a situation for which you want to drop a clustered columnstore index, and create a clustered index. When you drop a clustered columnstore index, the table is changed to the rowstore format. This example converts a columnstore table to a rowstore table with a clustered index with the same name. None of the data is lost. All data goes to the rowstore table, and the columns listed become the key columns in the clustered index.

```sql
--Drop the clustered columnstore index and create a clustered rowstore index.
--All of the columns are stored in the rowstore clustered index.
--The columns listed are the included columns in the index.
CREATE CLUSTERED INDEX cci_xDimProduct
ON xdimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey, WeightUnitMeasureCode)
WITH ( DROP_EXISTING = ON);
```

### E. Convert a columnstore table back to a rowstore heap

Use [DROP INDEX (SQL Server PDW)](drop-index-transact-sql.md) to drop the clustered columnstore index, and convert the table to a rowstore heap. This example converts the `cci_xDimProduct` table to a rowstore heap. The table continues to be distributed, but is stored as a heap.

```sql
--Drop the clustered columnstore index. The table continues to be distributed, but changes to a heap.
DROP INDEX cci_xdimProduct ON xdimProduct;
```

### F. Create an ordered clustered columnstore index on a table with no index

An unordered columnstore index covers all columns by default, without needing to specify a column list. An ordered columnstore index allows you to specify the order of the columns. The list doesn't need to include all columns.

Ordered columnstore indexes are available in [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. For more information, see [Performance tuning with ordered, clustered columnstore index](/azure/synapse-analytics/sql-data-warehouse/performance-tuning-ordered-cci).

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
ORDER (SHIPDATE);
```

### G. Convert a clustered columnstore index to an ordered clustered columnstore index

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
ORDER (SHIPDATE)
WITH (DROP_EXISTING = ON);
```

### H. Add a column to the ordering of an ordered clustered columnstore index

In [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], and starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can specify an order for the columns in a columnstore index. The original ordered, clustered columnstore index was ordered on the `SHIPDATE` column only. The following example adds the `PRODUCTKEY` column to the ordering.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
ORDER (SHIPDATE, PRODUCTKEY)
WITH (DROP_EXISTING = ON);
```

### I. Change the ordinal of ordered columns

The original ordered, clustered columnstore index was ordered on `SHIPDATE`, `PRODUCTKEY`. The following example changes the ordering to `PRODUCTKEY`, `SHIPDATE`.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX cci ON Sales.OrderLines
ORDER (PRODUCTKEY,SHIPDATE)
WITH (DROP_EXISTING = ON);
```

### J. Create an ordered clustered columnstore index

***Applies to:*** [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)] and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]

You can create a clustered columnstore index with ordering keys. When creating an ordered clustered columnstore index, it is recommended to apply the query hint `MAXDOP = 1` for maximum quality of sorting and shortest duration.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX [OrderedCCI] ON dbo.FactResellerSalesPartCategoryFull
ORDER (EnglishProductSubcategoryName, EnglishProductName)
WITH (MAXDOP = 1, DROP_EXISTING = ON);
```

## See also

- [Columnstore indexes: Overview](../../relational-databases/indexes/columnstore-indexes-overview.md)
- [Columnstore indexes feature summary](../../relational-databases/indexes/columnstore-indexes-what-s-new.md)
- [What's new in columnstore indexes](../../relational-databases/indexes/columnstore-indexes-what-s-new.md)

## Next steps

- [Columnstore indexes for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore indexes for data warehousing](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md)
- [Columnstore indexes - Query performance](../../relational-databases/indexes/columnstore-indexes-query-performance.md)
- [Columnstore indexes - Design guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md)