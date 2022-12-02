---
title: "CREATE INDEX (Transact-SQL)"
description: CREATE INDEX (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, randolphwest
ms.date: 10/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "CREATE INDEX"
  - "INDEX"
  - "INDEX_TSQL"
  - "CREATE_INDEX_TSQL"
helpviewer_keywords:
  - "CREATE XML INDEX statement"
  - "PRIMARY XML INDEX statement"
  - "indexes [SQL Server], creating"
  - "online index operations"
  - "index keys [SQL Server]"
  - "PROPERTY INDEX statement"
  - "computed columns, index creation"
  - "clustered indexes, creating"
  - "indexed views [SQL Server], index creation"
  - "partitioned indexes [SQL Server], creating"
  - "VALUE INDEX statement"
  - "index creation [SQL Server], CREATE INDEX statement"
  - "DROP_EXISTING clause"
  - "row locks [SQL Server]"
  - "composite indexes"
  - "MAXDOP index option, CREATE INDEX statement"
  - "filtered indexes [SQL Server], creating"
  - "PATH INDEX statement"
  - "locking [SQL Server], indexes"
  - "unique indexes, creating"
  - "primary indexes [SQL Server]"
  - "CREATE PRIMARY XML INDEX statement"
  - "SET statement, index creation"
  - "index options [SQL Server]"
  - "included columns"
  - "ONLINE option"
  - "nonclustered indexes [SQL Server], creating"
  - "CREATE INDEX statement"
  - "IGNORE_DUP_KEY option"
  - "deterministic functions"
  - "keys [SQL Server], index"
  - "SECONDARY XML INDEX statement"
  - "page locks [SQL Server]"
  - "secondary indexes [SQL Server]"
  - "XML indexes [SQL Server], creating"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE INDEX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Creates a relational index on a table or view. Also called a rowstore index because it is either a clustered or nonclustered B-tree index. You can create a rowstore index before there is data in the table. Use a rowstore index to improve query performance, especially when the queries select from specific columns or require values to be sorted in a particular order.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

[!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] currently don't support unique constraints. Any examples referencing unique constraints are only applicable to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

For information on index design guidelines, refer to the [SQL Server Index Design Guide](../../relational-databases/sql-server-index-design-guide.md).

**Examples:**

1. Create a nonclustered index on a table or view

    ```sql
    CREATE INDEX index1 ON schema1.table1 (column1);
    ```

1. Create a clustered index on a table and use a 3-part name for the table

    ```sql
    CREATE CLUSTERED INDEX index1 ON database1.schema1.table1 (column1);
    ```

1. Create a nonclustered index with a unique constraint and specify the sort order

    ```sql
    CREATE UNIQUE INDEX index1 ON schema1.table1 (column1 DESC, column2 ASC, column3 DESC);
    ```

**Key scenario:**

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)], you can use a nonclustered index on a columnstore index to improve data warehousing query performance. For more information, see [Columnstore Indexes - Data Warehouse](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md).

For additional types of indexes, see:

- [CREATE XML INDEX](../../t-sql/statements/create-xml-index-transact-sql.md)
- [CREATE SPATIAL INDEX](../../t-sql/statements/create-spatial-index-transact-sql.md)
- [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

### Syntax for SQL Server, Azure SQL Database, Azure SQL Managed Instance

```syntaxsql
CREATE [ UNIQUE ] [ CLUSTERED | NONCLUSTERED ] INDEX index_name
    ON <object> ( column [ ASC | DESC ] [ ,...n ] )
    [ INCLUDE ( column_name [ ,...n ] ) ]
    [ WHERE <filter_predicate> ]
    [ WITH ( <relational_index_option> [ ,...n ] ) ]
    [ ON { partition_scheme_name ( column_name )
         | filegroup_name
         | default
         }
    ]
    [ FILESTREAM_ON { filestream_filegroup_name | partition_scheme_name | "NULL" } ]

[ ; ]

<object> ::=
{ database_name.schema_name.table_or_view_name | schema_name.table_or_view_name | table_or_view_name }

<relational_index_option> ::=
{
    PAD_INDEX = { ON | OFF }
  | FILLFACTOR = fillfactor
  | SORT_IN_TEMPDB = { ON | OFF }
  | IGNORE_DUP_KEY = { ON | OFF }
  | STATISTICS_NORECOMPUTE = { ON | OFF }
  | STATISTICS_INCREMENTAL = { ON | OFF }
  | DROP_EXISTING = { ON | OFF }
  | ONLINE = { ON [ ( <low_priority_lock_wait> ) ] | OFF }
  | RESUMABLE = { ON | OFF }
  | MAX_DURATION = <time> [MINUTES]
  | ALLOW_ROW_LOCKS = { ON | OFF }
  | ALLOW_PAGE_LOCKS = { ON | OFF }
  | OPTIMIZE_FOR_SEQUENTIAL_KEY = { ON | OFF }
  | MAXDOP = max_degree_of_parallelism
  | DATA_COMPRESSION = { NONE | ROW | PAGE }
     [ ON PARTITIONS ( { <partition_number_expression> | <range> }
     [ , ...n ] ) ]
  | XML_COMPRESSION = { ON | OFF }
     [ ON PARTITIONS ( { <partition_number_expression> | <range> }
     [ , ...n ] ) ]
}

<filter_predicate> ::=
    <conjunct> [ AND ] [ ...n ]

<conjunct> ::=
    <disjunct> | <comparison>

<disjunct> ::=
        column_name IN (constant ,...n)

<comparison> ::=
        column_name <comparison_op> constant

<comparison_op> ::=
    { IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !< }

<low_priority_lock_wait>::=
{
    WAIT_AT_LOW_PRIORITY ( MAX_DURATION = <time> [ MINUTES ] ,
                          ABORT_AFTER_WAIT = { NONE | SELF | BLOCKERS } )
}

<range> ::=
<partition_number_expression> TO <partition_number_expression>

```

### Backward compatible relational index

> [!IMPORTANT]  
> The backward compatible relational index syntax structure will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
> Avoid using this syntax structure in new development work, and plan to modify applications that currently use the feature.
> Use the syntax structure specified in \<relational_index_option\> instead.

```syntaxsql
CREATE [ UNIQUE ] [ CLUSTERED | NONCLUSTERED ] INDEX index_name
    ON <object> ( column_name [ ASC | DESC ] [ ,...n ] )
    [ WITH <backward_compatible_index_option> [ ,...n ] ]
    [ ON { filegroup_name | "default" } ]

<object> ::=
{
    [ database_name. [ owner_name ] . | owner_name. ]
    table_or_view_name
}

<backward_compatible_index_option> ::=
{
    PAD_INDEX
  | FILLFACTOR = fillfactor
  | SORT_IN_TEMPDB
  | IGNORE_DUP_KEY
  | STATISTICS_NORECOMPUTE
  | DROP_EXISTING
}
```

### Syntax for Azure Synapse Analytics and Parallel Data Warehouse

```syntaxsql

CREATE CLUSTERED COLUMNSTORE INDEX index_name
    ON [ database_name . [ schema ] . | schema . ] table_name
    [ORDER (column[,...n])]
    [WITH ( DROP_EXISTING = { ON | OFF } )]
[;]


CREATE [ CLUSTERED | NONCLUSTERED ] INDEX index_name
    ON [ database_name . [ schema ] . | schema . ] table_name
        ( { column [ ASC | DESC ] } [ ,...n ] )
    WITH ( DROP_EXISTING = { ON | OFF } )
[;]

```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### UNIQUE

Creates a unique index on a table or view. A unique index is one in which no two rows are permitted to have the same index key value. A clustered index on a view must be unique.

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't allow creating a unique index on columns that already include duplicate values, whether or not `IGNORE_DUP_KEY` is set to ON. If this is tried, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] displays an error message. Duplicate values must be removed before a unique index can be created on the column or columns. Columns that are used in a unique index should be set to NOT NULL, because multiple null values are considered duplicates when a unique index is created.

#### CLUSTERED

Creates an index in which the logical order of the key values determines the physical order of the corresponding rows in a table. The bottom, or leaf, level of the clustered index contains the actual data rows of the table. A table or view is allowed one clustered index at a time.

A view with a unique clustered index is called an indexed view. Creating a unique clustered index on a view physically materializes the view. A unique clustered index must be created on a view before any other indexes can be defined on the same view. For more information, see [Create Indexed Views](../../relational-databases/views/create-indexed-views.md).

Create the clustered index before creating any nonclustered indexes. Existing nonclustered indexes on tables are rebuilt when a clustered index is created.

If `CLUSTERED` isn't specified, a nonclustered index is created.

> [!NOTE]  
> Because the leaf level of a clustered index and the data pages are the same by definition, creating a clustered index and using the `ON partition_scheme_name` or `ON filegroup_name` clause effectively moves a table from the filegroup on which the table was created to the new partition scheme or filegroup. Before creating tables or indexes on specific filegroups, verify which filegroups are available and that they have enough empty space for the index.

In some cases creating a clustered index can enable previously disabled indexes. For more information, see [Enable Indexes and Constraints](../../relational-databases/indexes/enable-indexes-and-constraints.md) and [Disable Indexes and Constraints](../../relational-databases/indexes/disable-indexes-and-constraints.md).

#### NONCLUSTERED

Creates an index that specifies the logical ordering of a table. With a nonclustered index, the physical order of the data rows is independent of their indexed order.

Each table can have up to 999 nonclustered indexes, regardless of how the indexes are created: either implicitly with PRIMARY KEY and UNIQUE constraints, or explicitly with `CREATE INDEX`.

For indexed views, nonclustered indexes can be created only on a view that has a unique clustered index already defined.

If not otherwise specified, the default index type is nonclustered.

#### *index_name*

The name of the index. Index names must be unique within a table or view, but don't have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

#### *column*

The column or columns on which the index is based. Specify two or more column names to create a composite index on the combined values in the specified columns. List the columns to be included in the composite index, in sort-priority order, inside the parentheses after *table_or_view_name*.

Up to 32 columns can be combined into a single composite index key. All the columns in a composite index key must be in the same table or view. The maximum allowable size of the combined index values is 900 bytes for a clustered index, or 1,700 for a nonclustered index. The limits are 16 columns and 900 bytes for versions before [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].

Columns that are of the large object (LOB) data types **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or **image** can't be specified as key columns for an index. Also, a view definition can't include **ntext**, **text**, or **image** columns, even if they are not referenced in the `CREATE INDEX` statement.

You can create indexes on CLR user-defined type columns if the type supports binary ordering. You can also create indexes on computed columns that are defined as method invocations off a user-defined type column, as long as the methods are marked deterministic and don't perform data access operations. For more information about indexing CLR user-defined type columns, see [CLR User-defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).

#### [ ASC | DESC ]

Determines the ascending or descending sort direction for the particular index column. The default is **ASC**.

#### INCLUDE (*column* [ ,... *n* ] )

Specifies the non-key columns to be added to the leaf level of the nonclustered index. The nonclustered index can be unique or non-unique.

Column names can't be repeated in the INCLUDE list and can't be used simultaneously as both key and non-key columns. Nonclustered indexes always contain the clustered index columns if a clustered index is defined on the table. For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md).

All data types are allowed except **text**, **ntext**, and **image**. Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], if any one of the specified non-key columns are **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types, the index can be built or rebuilt using the ONLINE option.

Computed columns that are deterministic and either precise or imprecise can be included columns. Computed columns derived from **image**, **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml** data types can be included in non-key columns as long as the computed column data types is allowable as an included column. For more information, see [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md).

For information on creating an XML index, see [CREATE XML INDEX](../../t-sql/statements/create-xml-index-transact-sql.md).

#### WHERE <filter_predicate>

Creates a filtered index by specifying which rows to include in the index. The filtered index must be a nonclustered index on a table. Creates filtered statistics for the data rows in the filtered index.

The filter predicate uses simple comparison logic and can't reference a computed column, a UDT column, a spatial data type column, or a hierarchyID data type column. Comparisons using `NULL` literals are not allowed with the comparison operators. Use the `IS NULL` and `IS NOT NULL` operators instead.

Here are some examples of filter predicates for the `Production.BillOfMaterials` table:

```sql
WHERE StartDate > '20000101' AND EndDate <= '20000630'

WHERE ComponentID IN (533, 324, 753)

WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL
```

Filtered indexes don't apply to XML indexes and full-text indexes. For UNIQUE indexes, only the selected rows must have unique index values. Filtered indexes don't allow the `IGNORE_DUP_KEY` option.

#### ON *partition_scheme_name* ( *column_name* )

Specifies the partition scheme that defines the filegroups onto which the partitions of a partitioned index will be mapped. The partition scheme must exist within the database by executing either [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md) or [ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md). *column_name* specifies the column against which a partitioned index will be partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using. *column_name* isn't restricted to the columns in the index definition. Any column in the base table can be specified, except when partitioning a UNIQUE index, *column_name* must be chosen from among those used as the unique key. This restriction allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to verify uniqueness of key values within a single partition only.

> [!NOTE]  
> When you partition a non-unique, clustered index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by default adds the partitioning column to the list of clustered index keys, if it is not already specified. When partitioning a non-unique, nonclustered index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] adds the partitioning column as a non-key (included) column of the index, if it is not already specified.

If *partition_scheme_name* or *filegroup* isn't specified and the table is partitioned, the index is placed in the same partition scheme, using the same partitioning column, as the underlying table.

> [!NOTE]  
> You cannot specify a partitioning scheme on an XML index. If the base table is partitioned, the XML index uses the same partition scheme as the table.

For more information about partitioning indexes, [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).

#### ON *filegroup_name*

Creates the specified index on the specified filegroup. If no location is specified and the table or view isn't partitioned, the index uses the same filegroup as the underlying table or view. The filegroup must already exist.

#### ON "default"

Creates the specified index on the same filegroup or partition scheme as the table or view.

The term default, in this context, isn't a keyword. It is an identifier for the default filegroup and must be delimited, as in `ON "default"` or `ON [default]`. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

> [!NOTE]  
> "default" does not indicate the database default filegroup in the context of `CREATE INDEX`. This differs from `CREATE TABLE`, where "default" locates the table on the database default filegroup.

#### [ FILESTREAM_ON { *filestream_filegroup_name* | *partition_scheme_name* | "NULL" } ]

Specifies the placement of FILESTREAM data for the table when a clustered index is created. The `FILESTREAM_ON` clause allows FILESTREAM data to be moved to a different FILESTREAM filegroup or partition scheme.

_filestream_filegroup_name_ is the name of a FILESTREAM filegroup. The filegroup must have one file defined for the filegroup by using a [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) or [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement; otherwise, an error is raised.

If the table is partitioned, the `FILESTREAM_ON` clause must be included and must specify a partition scheme of FILESTREAM filegroups that uses the same partition function and partition columns as the partition scheme for the table. Otherwise, an error is raised.

If the table isn't partitioned, the FILESTREAM column can't be partitioned. FILESTREAM data for the table must be stored in a single filegroup that is specified in the `FILESTREAM_ON` clause.

`FILESTREAM_ON NULL` can be specified in a `CREATE INDEX` statement if a clustered index is being created and the table doesn't contain a FILESTREAM column.

For more information, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).

#### \<object>::=

The fully qualified or nonfully qualified object to be indexed.

#### *database_name*

The name of the database.

#### *schema_name*

The name of the schema to which the table or view belongs.

#### *table_or_view_name*

The name of the table or view to be indexed.

The view must be defined with SCHEMABINDING to create an index on it. A unique clustered index must be created on a view before any nonclustered index is created. For more information about indexed views, see the Remarks section.

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the object can be a table stored with a clustered columnstore index.

[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] supports the three-part name format *database_name*.[*schema_name*]._object_name_ when the *database_name* is the current database or the *database_name* is `tempdb` and the *object_name* starts with #.

#### \<relational_index_option>::=

Specifies the options to use when you create the index.

#### PAD_INDEX = { ON | OFF }

Specifies index padding. The default is **OFF**.

ON  
The percentage of free space that is specified by *fillfactor* is applied to the intermediate-level pages of the index.

OFF or *fillfactor* isn't specified  
The intermediate-level pages are filled to near capacity, leaving sufficient space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages.

The `PAD_INDEX` option is useful only when FILLFACTOR is specified, because `PAD_INDEX` uses the percentage specified by FILLFACTOR. If the percentage specified for FILLFACTOR isn't large enough to allow for one row, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] internally overrides the percentage to allow for the minimum. The number of rows on an intermediate index page is never less than two, regardless of how low the value of *fillfactor*.

In backward compatible syntax, `WITH PAD_INDEX` is equivalent to `WITH PAD_INDEX = ON`.

#### FILLFACTOR = *fillfactor*

Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or rebuild. The value for *fillfactor* must be an integer value from 1 to 100. Fill factor values 0 and 100 are the same in all respects. If *fillfactor* is 100, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates indexes with leaf pages filled to capacity.

The `FILLFACTOR` setting applies only when the index is created or rebuilt. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't dynamically keep the specified percentage of empty space in the pages.

To view the fill factor setting, use `fill_factor` in `sys.indexes`.

> [!IMPORTANT]  
> Creating a clustered index with a `FILLFACTOR` less than 100 affects the amount of storage space the data occupies because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] redistributes the data when it creates the clustered index.

For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).

#### SORT_IN_TEMPDB = { ON | OFF }

Specifies whether to store temporary sort results in **tempdb**. The default is **OFF** except for Azure SQL Database Hyperscale. For all index build operations in Hyperscale, `SORT_IN_TEMPDB` is always ON, regardless of the option specified unless resumable index rebuild is used.

ON  
The intermediate sort results that are used to build the index are stored in **tempdb**. This may reduce the time required to create an index if **tempdb** is on a different set of disks than the user database. However, this increases the amount of disk space that is used during the index build.

OFF  
The intermediate sort results are stored in the same database as the index.

In addition to the space required in the user database to create the index, **tempdb** must have about the same amount of additional space to hold the intermediate sort results. For more information, see [SORT_IN_TEMPDB Option For Indexes](../../relational-databases/indexes/sort-in-tempdb-option-for-indexes.md).

In backward compatible syntax, `WITH SORT_IN_TEMPDB` is equivalent to `WITH SORT_IN_TEMPDB = ON`.

#### IGNORE_DUP_KEY = { ON | OFF }

Specifies the error response when an insert operation attempts to insert duplicate key values into a unique index. The `IGNORE_DUP_KEY` option applies only to insert operations after the index is created or rebuilt. The option has no effect when executing [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md), [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md), or [UPDATE](../../t-sql/queries/update-transact-sql.md). The default is **OFF**.

ON  
A warning message will occur when duplicate key values are inserted into a unique index. Only the rows violating the uniqueness constraint will fail.

OFF  
An error message will occur when duplicate key values are inserted into a unique index. The entire INSERT operation will be rolled back.

`IGNORE_DUP_KEY` can't be set to ON for indexes created on a view, non-unique indexes, XML indexes, spatial indexes, and filtered indexes.

To view `IGNORE_DUP_KEY`, use [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md).

In backward compatible syntax, `WITH IGNORE_DUP_KEY` is equivalent to `WITH IGNORE_DUP_KEY = ON`.

#### STATISTICS_NORECOMPUTE = { ON | OFF}

Specifies whether distribution statistics are recomputed. The default is **OFF**.

ON  
Out-of-date statistics are not automatically recomputed.

OFF  
Automatic statistics updating are enabled.

To restore automatic statistics updating, set the `STATISTICS_NORECOMPUTE` to OFF, or execute `UPDATE STATISTICS` without the `NORECOMPUTE` clause.

> [!IMPORTANT]  
> Disabling automatic recomputation of distribution statistics may prevent the query optimizer from picking optimal execution plans for queries involving the table.

In backward compatible syntax, `WITH STATISTICS_NORECOMPUTE` is equivalent to `WITH STATISTICS_NORECOMPUTE = ON`.

#### STATISTICS_INCREMENTAL = { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

When **ON**, the statistics created are per partition statistics. When **OFF**, the statistics tree is dropped and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] re-computes the statistics. The default is **OFF**.

If per partition statistics are not supported the option is ignored and a warning is generated. Incremental stats are not supported for following statistics types:

- Statistics created with indexes that are not partition-aligned with the base table.
- Statistics created on Always On readable secondary databases.
- Statistics created on read-only databases.
- Statistics created on filtered indexes.
- Statistics created on views.
- Statistics created on internal tables.
- Statistics created with spatial indexes or XML indexes.

#### DROP_EXISTING = { ON | OFF }

Is an option to drop and rebuild the existing clustered or nonclustered index with modified column specifications, and keep the same name for the index. The default is **OFF**.

ON  
Specifies to `DROP` and `REBUILD` the existing index, which must have the same name as the parameter *index_name*.

OFF  
Specifies not to `DROP` and `REBUILD` the existing index. SQL Server displays an error if the specified index name already exists.

With `DROP_EXISTING`, you can change:

- A nonclustered rowstore index to a clustered rowstore index.

With `DROP_EXISTING`, you can't change:

- A clustered rowstore index to a nonclustered rowstore index.
- A clustered columnstore index to any type of rowstore index.

In backward compatible syntax, `WITH DROP_EXISTING` is equivalent to `WITH DROP_EXISTING = ON`.

#### ONLINE = { ON | OFF }

Specifies whether underlying tables and associated indexes are available for queries and data modification during the index operation. The default is **OFF**.

> [!IMPORTANT]  
> Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).

ON  
Long-term table locks are not held for the duration of the index operation. During the main phase of the index operation, only an Intent Share (IS) lock is held on the source table. This enables queries or updates to the underlying table and indexes to proceed. At the start of the operation, a Shared (S) lock is held on the source object for a very short period of time. At the end of the operation, for a short period of time, an S (Shared) lock is acquired on the source if a nonclustered index is being created. A Sch-M (Schema Modification) lock is acquired when a clustered index is created or dropped online and when a clustered or nonclustered index is being rebuilt. ONLINE can't be set to ON when an index is being created on a local temporary table.

> [!NOTE]  
> Online index creation can set the `low_priority_lock_wait` options, see [WAIT_AT_LOW_PRIORITY with online index operations](#wait-at-low-priority).

OFF  
Table locks are applied for the duration of the index operation. An offline index operation that creates, rebuilds, or drops a clustered index, or rebuilds or drops a nonclustered index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table for the duration of the operation. An offline index operation that creates a nonclustered index acquires a Shared (S) lock on the table. This prevents updates to the underlying table but allows read operations, such as SELECT statements.

For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).

Indexes, including indexes on global temp tables, can be created online except for the following cases:

- XML index
- Index on a local temp table
- Initial unique clustered index on a view
- Disabled clustered indexes
- Columnstore indexes
- Clustered index, if the underlying table contains LOB data types (**image**, **ntext**, **text**) and spatial data types
- **varchar(max)** and **varbinary(max)** columns can't be part of an index key. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], when a table contains **varchar(max)** or **varbinary(max)** columns, a clustered index containing other columns can be built or rebuilt using the `ONLINE` option.

For more information, see [How Online Index Operations Work](../../relational-databases/indexes/how-online-index-operations-work.md).

#### RESUMABLE = { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

 Specifies whether an online index operation is resumable.

 ON      
Index operation is resumable.

 OFF      
Index operation isn't resumable.

#### MAX_DURATION = *time* [MINUTES] used with `RESUMABLE = ON` (requires `ONLINE = ON`)

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Indicates time (an integer value specified in minutes) that a resumable online index operation is executed before being paused.

> [!IMPORTANT]  
> For more detailed information about index operations that can be performed online, see [Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).

> [!NOTE]  
> Resumable online index rebuilds are not supported on columnstore indexes.

#### ALLOW_ROW_LOCKS = { ON | OFF }

Specifies whether row locks are allowed. The default is **ON**.

ON  
Row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.

OFF  
Row locks are not used.

#### ALLOW_PAGE_LOCKS = { ON | OFF }

Specifies whether page locks are allowed. The default is **ON**.

ON  
Page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.

OFF  
Page locks are not used.

#### OPTIMIZE_FOR_SEQUENTIAL_KEY = { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Specifies whether or not to optimize for last-page insert contention. The default is **OFF**. See the [Sequential Keys](#sequential-keys) section for more information.

#### MAXDOP = *max_degree_of_parallelism*

Overrides the **max degree of parallelism** configuration option for the duration of the index operation. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* can be:

1  
Suppresses parallel plan generation.

\>1  
Restricts the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload.

0 (default)  
Uses the actual number of processors or fewer based on the current system workload.

 For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md) and [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md).

#### DATA_COMPRESSION

Specifies the data compression option for the specified index, partition number, or range of partitions. The options are as follows:

NONE  
Index or specified partitions are not compressed.

ROW  
Index or specified partitions are compressed by using row compression.

PAGE  
Index or specified partitions are compressed by using page compression.

For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).

#### XML_COMPRESSION

**Applies to**: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

Specifies the XML compression option for the specified index that contains one or more **xml** data type columns. The options are as follows:

ON  
Index or specified partitions are compressed by using XML compression.

OFF  
Index or specified partitions are not compressed.

#### ON PARTITIONS ( { \<partition_number_expression> | \<range> } [ ,...*n* ] )

Specifies the partitions to which the `DATA_COMPRESSION` or `XML_COMPRESSION` settings apply. If the index isn't partitioned, the `ON PARTITIONS` argument will generate an error. If the `ON PARTITIONS` clause isn't provided, the `DATA_COMPRESSION` or `XML_COMPRESSION` option applies to all partitions of a partitioned index.

> [!NOTE]  
> `XML_COMPRESSION` is only available starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

`<partition_number_expression>` can be specified in the following ways:

- Provide the number for a partition, for example: `ON PARTITIONS (2)`.
- Provide the partition numbers for several individual partitions separated by commas, for example: `ON PARTITIONS (1, 5)`.
- Provide both ranges and individual partitions, for example: `ON PARTITIONS (2, 4, 6 TO 8)`.

`<range>` can be specified as partition numbers separated by the word TO, for example: `ON PARTITIONS (6 TO 8)`.

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

## Remarks

The `CREATE INDEX` statement is optimized like any other query. To save on I/O operations, the query processor may choose to scan another index instead of performing a table scan. The sort operation may be eliminated in some situations. On multiprocessor computers `CREATE INDEX` can use more processors to perform the scan and sort operations associated with creating the index, in the same way as other queries do. For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

The `CREATE INDEX` operation can be minimally logged if the database recovery model is set to either bulk-logged or simple.

Indexes can be created on a temporary table. When the table is dropped or the session ends, the indexes are dropped.

A clustered index can be built on a table variable when a Primary Key is created. When the query completes or the session ends, the index is dropped.

Indexes support extended properties.

## Clustered indexes

Creating a clustered index on a table (heap) or dropping and re-creating an existing clustered index requires additional workspace to be available in the database to accommodate data sorting and a temporary copy of the original table or existing clustered index data. For more information about clustered indexes, see [Create Clustered Indexes](../../relational-databases/indexes/create-clustered-indexes.md) and the [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md).

## Nonclustered indexes

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can create a nonclustered index on a table stored as a clustered columnstore index. If you first create a nonclustered index on a table stored as a heap or clustered index, the index will persist if you later convert the table to a clustered columnstore index. It is also not necessary to drop the nonclustered index when you rebuild the clustered columnstore index.

Limitations and Restrictions:

- The `FILESTREAM_ON` option isn't valid when you create a nonclustered index on a table stored as a clustered columnstore index.

## Unique indexes

When a unique index exists, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for duplicate values each time data is added by insert operations. Insert operations that would generate duplicate key values are rolled back, and the [!INCLUDE[ssDE](../../includes/ssde-md.md)] displays an error message. This is true even if the insert operation changes many rows but causes only one duplicate. If an attempt is made to enter data for which there is a unique index and the `IGNORE_DUP_KEY` clause is set to ON, only the rows violating the UNIQUE index fail.

## Partitioned indexes

Partitioned indexes are created and maintained in a similar manner to partitioned tables, but like ordinary indexes, they are handled as separate database objects. You can have a partitioned index on a table that isn't partitioned, and you can have a nonpartitioned index on a table that is partitioned.

If you are creating an index on a partitioned table, and don't specify a filegroup on which to place the index, the index is partitioned in the same manner as the underlying table. This is because indexes, by default, are placed on the same filegroups as their underlying tables, and for a partitioned table in the same partition scheme that uses the same partitioning columns. When the index uses the same partition scheme and partitioning column as the table, the index is *aligned* with the table.

> [!WARNING]  
> Creating and rebuilding nonaligned indexes on a table with more than 1,000 partitions is possible, but is not supported. Doing so may cause degraded performance or excessive memory consumption during these operations. We recommend using only aligned indexes when the number of partitions exceed 1,000.

When partitioning a non-unique, clustered index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by default adds any partitioning columns to the list of clustered index keys, if not already specified.

Indexed views can be created on partitioned tables in the same manner as indexes on tables. For more information about partitioned indexes, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md) and the [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md).

In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], statistics are not created by scanning all the rows in the table when a partitioned index is created or rebuilt. Instead, the query optimizer uses the default sampling algorithm to generate statistics. To obtain statistics on partitioned indexes by scanning all the rows in the table, use `CREATE STATISTICS` or `UPDATE STATISTICS` with the `FULLSCAN` clause.

## Filtered indexes

A filtered index is an optimized nonclustered index, suited for queries that select a small percentage of rows from a table. It uses a filter predicate to index a portion of the data in the table. A well-designed filtered index can improve query performance, reduce storage costs, and reduce maintenance costs.

### Required SET options for filtered indexes

The SET options in the Required Value column are required whenever any of the following conditions occur:

- Create a filtered index.
- INSERT, UPDATE, DELETE, or MERGE operation modifies the data in a filtered index.
- The filtered index is used by the query optimizer to produce the query plan.

    |SET options|Required value|Default server value|Default<br /><br />OLE DB and ODBC value|Default<br /><br />DB-Library value|
    |-----------------|--------------------|--------------------------|---------------------------------------|-----------------------------------|
    |ANSI_NULLS|ON|ON|ON|OFF|
    |ANSI_PADDING|ON|ON|ON|OFF|
    |ANSI_WARNINGS*|ON|ON|ON|OFF|
    |ARITHABORT|ON|ON|OFF|OFF|
    |CONCAT_NULL_YIELDS_NULL|ON|ON|ON|OFF|
    |NUMERIC_ROUNDABORT|OFF|OFF|OFF|OFF|
    |QUOTED_IDENTIFIER|ON|ON|ON|OFF|

     * Setting ANSI_WARNINGS to ON implicitly sets ARITHABORT to ON when the database compatibility level is set to 90 or higher. If the database compatibility level is set to 80 or earlier, the ARITHABORT option must explicitly be set to ON.

If the SET options are incorrect, the following conditions can occur:

- The filtered index isn't created.
- The [!INCLUDE[ssDE](../../includes/ssde-md.md)] generates an error and rolls back INSERT, UPDATE, DELETE, or MERGE statements that change data in the index.
- Query optimizer doesn't consider the index in the execution plan for any Transact-SQL statements.

 For more information about Filtered Indexes, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md) and the [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md).

## Spatial indexes

For information about spatial indexes, see [CREATE SPATIAL INDEX](../../t-sql/statements/create-spatial-index-transact-sql.md) and [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).

## XML indexes

For information about XML indexes see, [CREATE XML INDEX](../../t-sql/statements/create-xml-index-transact-sql.md) and [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md).

## Index key size

The maximum size for an index key is 900 bytes for a clustered index and 1,700 bytes for a nonclustered index. (Before [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] the limit was always 900 bytes.) Indexes on **varchar** columns that exceed the byte limit can be created if the existing data in the columns don't exceed the limit at the time the index is created; however, subsequent insert or update actions on the columns that cause the total size to be greater than the limit will fail. The index key of a clustered index can't contain **varchar** columns that have existing data in the ROW_OVERFLOW_DATA allocation unit. If a clustered index is created on a **varchar** column and the existing data is in the IN_ROW_DATA allocation unit, subsequent insert or update actions on the column that would push the data off-row will fail.

Nonclustered indexes can include non-key columns in the leaf level of the index. These columns are not considered by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] when calculating the index key size . For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md) and the [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md).

> [!NOTE]  
> When tables are partitioned, if the partitioning key columns are not already present in a non-unique clustered index, they are added to the index by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The combined size of the indexed columns (not counting included columns), plus any added partitioning columns cannot exceed 1800 bytes in a non-unique clustered index.

## Computed columns

Indexes can be created on computed columns. In addition, computed columns can have the property PERSISTED. This means that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] stores the computed values in the table, and updates them when any other columns on which the computed column depends are updated. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses these persisted values when it creates an index on the column, and when the index is referenced in a query.

To index a computed column, the computed column must be deterministic and precise. However, using the PERSISTED property expands the type of indexable computed columns to include:

- Computed columns based on [!INCLUDE[tsql](../../includes/tsql-md.md)] and CLR functions and CLR user-defined type methods that are marked deterministic by the user.
- Computed columns based on expressions that are deterministic as defined by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] but imprecise.

Persisted computed columns require the following SET options to be set as shown in the previous section [Required SET Options for Filtered Indexes](#required-set-options-for-filtered-indexes).

The UNIQUE or PRIMARY KEY constraint can contain a computed column as long as it satisfies all conditions for indexing. Specifically, the computed column must be deterministic and precise or deterministic and persisted. For more information about determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

Computed columns derived from **image**, **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml** data types can be indexed either as a key or included non-key column as long as the computed column data type is allowable as an index key column or non-key column. For example, you can't create a primary XML index on a computed **xml** column. If the index key size exceeds 900 bytes, a warning message is displayed.

Creating an index on a computed column may cause the failure of an insert or update operation that previously worked. Such a failure may take place when the computed column results in arithmetic error. For example, in the following table, although computed column `c` results in an arithmetic error, the INSERT statement works.

```sql
CREATE TABLE t1 (a INT, b INT, c AS a/b);
INSERT INTO t1 VALUES (1, 0);
```

If, instead, after creating the table, you create an index on computed column `c`, the same `INSERT` statement will now fail.

```sql
CREATE TABLE t1 (a INT, b INT, c AS a/b);
CREATE UNIQUE CLUSTERED INDEX Idx1 ON t1(c);
INSERT INTO t1 VALUES (1, 0);
```

For more information, see [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md).

## Included columns in indexes

Non-key columns, called included columns, can be added to the leaf level of a nonclustered index to improve query performance by covering the query. That is, all columns referenced in the query are included in the index as either key or non-key columns. This allows the query optimizer to locate all the required information from an index scan; the table or clustered index data isn't accessed. For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md) and the [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md).

## Specifying index options

[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced new index options and also modifies the way in which options are specified. In backward compatible syntax, `WITH option_name` is equivalent to `WITH (option_name = ON)`. When you set index options, the following rules apply:

- New index options can only be specified by using `WITH (<option_name> = <ON | OFF>)`.
- Options can't be specified by using both the backward compatible and new syntax in the same statement. For example, specifying `WITH (DROP_EXISTING, ONLINE = ON)` causes the statement to fail.
- When you create an XML index, the options must be specified by using `WITH (<option_name> = <ON | OFF>)`.

## DROP_EXISTING clause

You can use the `DROP_EXISTING` clause to rebuild the index, add or drop columns, modify options, modify column sort order, or change the partition scheme or filegroup.

If the index enforces a PRIMARY KEY or UNIQUE constraint and the index definition isn't altered in any way, the index is dropped and re-created preserving the existing constraint. However, if the index definition is altered the statement fails. To change the definition of a PRIMARY KEY or UNIQUE constraint, drop the constraint and add a constraint with the new definition.

`DROP_EXISTING` enhances performance when you re-create a clustered index, with either the same or different set of keys, on a table that also has nonclustered indexes. `DROP_EXISTING` replaces the execution of a `DROP INDEX` statement on the old clustered index followed by the execution of a `CREATE INDEX` statement for the new clustered index. The nonclustered indexes are rebuilt once, and then only if the index definition has changed. The `DROP_EXISTING` clause doesn't rebuild the nonclustered indexes when the index definition has the same index name, key and partition columns, uniqueness attribute, and sort order as the original index.

Whether the nonclustered indexes are rebuilt or not, they always remain in their original filegroups or partition schemes and use the original partition functions. If a clustered index is rebuilt to a different filegroup or partition scheme, the nonclustered indexes are not moved to coincide with the new location of the clustered index. Therefore, even the nonclustered indexes previously aligned with the clustered index, they may no longer be aligned with it. For more information about partitioned index alignment, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).

The `DROP_EXISTING` clause will not sort the data again if the same index key columns are used in the same order and with the same ascending or descending order, unless the index statement specifies a nonclustered index and the ONLINE option is set to OFF. If the clustered index is disabled, the `CREATE INDEX WITH DROP_EXISTING` operation must be performed with ONLINE set to OFF. If a nonclustered index is disabled and isn't associated with a disabled clustered index, the `CREATE INDEX WITH DROP_EXISTING` operation can be performed with ONLINE set to OFF or ON.

> [!NOTE]  
> When indexes with 128 extents or more are dropped or rebuilt, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits.

## ONLINE option

The following guidelines apply for performing index operations online:

- The underlying table can't be altered, truncated, or dropped while an online index operation is in process.
- Additional temporary disk space is required during the index operation.
- Online operations can be performed on partitioned indexes and indexes that contain persisted computed columns, or included columns.
- The `low_priority_lock_wait` argument option allows you to decide how the index operation can proceed when blocked on the Sch-M lock.

For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).

#### Resources

The following resources are required for resumable online index create operation:

- Additional space required to keep the index being built, including the time when index is being paused
- Additional log throughput during the sorting phase. The overall log space usage for resumable index is less compared to regular online index create and allows log truncation during this operation.
- A DDL state preventing any DDL modification
- Ghost cleanup is blocked on the in-build index for the duration of the operation both while paused and while the operation is running.

#### Current functional limitations

The following functionality is disabled for resumable index create operations:

- After a resumable online index create operation is paused, the initial value of MAXDOP can't be changed
- Create an index that contains:

  - Computed or TIMESTAMP column(s) as key columns
  - LOB column as included column for resumable index create
  - Filtered index

### <a name="resumable-indexes"></a>Resumable index operations

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

The following guidelines apply for resumable index operations:

- Online index create is specified as resumable using the `RESUMABLE = ON` option.
- The RESUMABLE option isn't persisted in the metadata for a given index and applies only to the duration of a current DDL statement. Therefore, the `RESUMABLE = ON` clause must be specified explicitly to enable resumability.
- `MAX_DURATION` option is only supported for `RESUMABLE = ON` option.
- `MAX_DURATION` for RESUMABLE option specifies the time interval for an index being built. Once this time is used the index build is either paused or it completes its execution. User decides when a build for a paused index can be resumed. The **time** in minutes for `MAX_DURATION` must be greater than 0 minutes and less or equal one week (7 \* 24 \* 60 = 10080 minutes). Having a long pause for an index operation may impact the DML performance on a specific table as well as the database disk capacity since both indexes the original one and the newly created one require disk space and need to be updated during DML operations. If `MAX_DURATION` option is omitted, the index operation will continue until its completion or until a failure occurs.
- To pause immediately the index operation, you can stop (Ctrl-C) the ongoing command, execute the [ALTER INDEX](alter-index-transact-sql.md) PAUSE command, or execute the `KILL <session_id>` command. Once the command is paused, it can be resumed using [ALTER INDEX](alter-index-transact-sql.md) command.
- Re-executing the original `CREATE INDEX` statement for resumable index, automatically resumes a paused index create operation.
- The `SORT_IN_TEMPDB = ON` option isn't supported for resumable index.
- The DDL command with `RESUMABLE = ON` can't be executed inside an explicit transaction (can't be part of begin `TRAN ... COMMIT` block).
- To resume/abort an index create/rebuild, use the [ALTER INDEX](alter-index-transact-sql.md) T-SQL syntax

> [!NOTE]  
> The DDL command runs until it completes, pauses or fails. In case the command pauses, an error will be issued indicating that the operation was paused and that the index creation did not complete. More information about the current index status can be obtained from [sys.index_resumable_operations](../../relational-databases/system-catalog-views/sys-index-resumable-operations.md). As before in case of a failure an error will be issued as well.

To indicate that an index create is executed as resumable operation and to check its current execution state, see [sys.index_resumable_operations](../../relational-databases/system-catalog-views/sys-index-resumable-operations.md).

### <a name="wait-at-low-priority"></a> WAIT_AT_LOW_PRIORITY with online index operations

**Applies to**: This syntax for `CREATE INDEX` currently applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] only. For `ALTER INDEX`, this syntax applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. For more information, see [ALTER INDEX](alter-index-transact-sql.md).

 The `low_priority_lock_wait` syntax allows for specifying `WAIT_AT_LOW_PRIORITY` behavior. `WAIT_AT_LOW_PRIORITY` can be used with `ONLINE=ON` only.

 The `WAIT_AT_LOW_PRIORITY` option allows DBAs to manage the Sch-S and Sch-M locks required for online index creation and allows them to select one of 3 options. In all 3 cases, if during the wait time `MAX_DURATION = n [minutes]`, there are no blocking activities, the online index rebuild is executed immediately without waiting and the DDL statement is completed.

 `WAIT_AT_LOW_PRIORITY` indicates that the online index create operation will wait for low priority locks, allowing other operations to proceed while the online index build operation is waiting. Omitting the `WAIT AT LOW PRIORITY` option is equivalent to `WAIT_AT_LOW_PRIORITY (MAX_DURATION = 0 minutes, ABORT_AFTER_WAIT = NONE)`.

 MAX_DURATION = *time* [**MINUTES**]

 The wait time (an integer value specified in minutes) that the online index create locks will wait with low priority when executing the DDL command. If the operation is blocked for the `MAX_DURATION` time, the specified `ABORT_AFTER_WAIT` action will be executed. `MAX_DURATION` time is always in minutes, and the word **MINUTES** can be omitted.

 ABORT_AFTER_WAIT = [**NONE** | **SELF** | **BLOCKERS** } ]

 NONE
 Continue waiting for the lock with normal (regular) priority.

 SELF
 Exit the online index create DDL operation currently being executed, without taking any action. The option **SELF** can't be used with a `MAX_DURATION` of 0.

 BLOCKERS
 Kill all user transactions that block the online index rebuild DDL operation so that the operation can continue. The **BLOCKERS** option requires the login to have `ALTER ANY CONNECTION` permission.

## Row and page locks options

When `ALLOW_ROW_LOCKS = ON` and `ALLOW_PAGE_LOCK = ON`, row-, page-, and table-level locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] chooses the appropriate lock and can escalate the lock from a row or page lock to a table lock.

When `ALLOW_ROW_LOCKS = OFF` and `ALLOW_PAGE_LOCK = OFF`, only a table-level lock is allowed when accessing the index.

## Sequential keys

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Last-page insert contention is a common performance problem that occurs when a large number of concurrent threads attempt to insert rows into an index with a sequential key. An index is considered sequential when the leading key column contains values that are always increasing (or decreasing), such as an identity column or a date that defaults to the current date/time. Because the keys being inserted are sequential, all new rows will be inserted at the end of the index structure - in other words, on the same page. This leads to contention for the page in memory which can be observed as several threads waiting on PAGELATCH_EX for the page in question.

Enabling the `OPTIMIZE_FOR_SEQUENTIAL_KEY` index option enables an optimization within the database engine that helps improve throughput for high-concurrency inserts into the index. It is intended for indexes that have a sequential key and thus are prone to last-page insert contention, but it may also help with indexes that have hot spots in other areas of the B-Tree index structure.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Viewing index information

To return information about indexes, you can use catalog views, system functions, and system stored procedures.

## Data compression

Data compression is described in the topic [Data Compression](../../relational-databases/data-compression/data-compression.md). The following are key points to consider:

- Compression can allow more rows to be stored on a page, but doesn't change the maximum row size.
- Non-leaf pages of an index are not page compressed but can be row compressed.
- Each nonclustered index has an individual compression setting, and doesn't inherit the compression setting of the underlying table.
- When a clustered index is created on a heap, the clustered index inherits the compression state of the heap unless an alternative compression state is specified.

The following restrictions apply to partitioned indexes:

- You can't change the compression setting of a single partition if the table has nonaligned indexes.
- The `ALTER INDEX <index> ... REBUILD PARTITION ...` syntax rebuilds the specified partition of the index.
- The `ALTER INDEX <index> ... REBUILD WITH ...` syntax rebuilds all partitions of the index.

To evaluate how changing the compression state will affect a table, an index, or a partition, use the [sp_estimate_data_compression_savings](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md) stored procedure.

### XML compression

**Applies to**: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

Many of the same considerations for data compression applies to XML compression. You should also be aware of the following considerations:

- When a list of partitions is specified, XML compression can be enabled on individual partitions. If the list of partitions isn't specified, all partitions are set to use XML compression. When a table or index is created, XML data compression is disabled unless otherwise specified. When a table is modified, the existing compression is preserved unless otherwise specified.
- If you specify a list of partitions or a partition that is out of range, an error is generated.
- When a clustered index is created on a heap, the clustered index inherits the XML compression state of the heap unless an alternative compression option is specified.
- Changing the XML compression setting of a heap requires all nonclustered indexes on the table to be rebuilt so that they have pointers to the new row locations in the heap.
- You can enable or disable XML compression online or offline. Enabling compression on a heap is single threaded for an online operation.
- To determine the XML compression state of partitions in a partitioned table, query the `xml_compression` column of the `sys.partitions` catalog view.

## Permissions

Requires `ALTER` permission on the table or view or membership in the `db_ddladmin` fixed database role.

## Limitations and restrictions

In [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], you can't create:

- A clustered or nonclustered rowstore index on a data warehouse table when a columnstore index already exists. This behavior is different from SMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] which allows both rowstore and columnstore indexes to co-exist on the same table.
- You can't create an index on a view.

## Metadata

To view information on existing indexes, you can query the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.

## Version notes

[!INCLUDE[ssSDS](../../includes/sssds-md.md)] doesn't support filegroup and filestream options.

## Examples: All versions. Uses the AdventureWorks database

### A. Create a simple nonclustered rowstore index

The following examples create a nonclustered index on the `VendorID` column of the `Purchasing.ProductVendor` table.

```sql
CREATE INDEX IX_VendorID ON ProductVendor (VendorID);
CREATE INDEX IX_VendorID ON dbo.ProductVendor (VendorID DESC, Name ASC, Address DESC);
CREATE INDEX IX_VendorID ON Purchasing..ProductVendor (VendorID);
```

### B. Create a simple nonclustered rowstore composite index

The following example creates a nonclustered composite index on the `SalesQuota` and `SalesYTD` columns of the `Sales.SalesPerson` table.

```sql
CREATE NONCLUSTERED INDEX IX_SalesPerson_SalesQuota_SalesYTD ON Sales.SalesPerson (SalesQuota, SalesYTD);
```

### C. Create an index on a table in another database

The following example creates a clustered index on the `VendorID` column of the `ProductVendor` table in the `Purchasing` database.

```sql
CREATE CLUSTERED INDEX IX_ProductVendor_VendorID ON Purchasing..ProductVendor (VendorID);
```

### D. Add a column to an index

The following example creates index IX_FF with two columns from the dbo.FactFinance table. The next statement rebuilds the index with one more column and keeps the existing name.

```sql
CREATE INDEX IX_FF ON dbo.FactFinance (FinanceKey ASC, DateKey ASC);

-- Rebuild and add the OrganizationKey
CREATE INDEX IX_FF ON dbo.FactFinance (FinanceKey, DateKey, OrganizationKey DESC)
  WITH (DROP_EXISTING = ON);
```

## Examples: SQL Server, Azure SQL Database

### E. Create a unique nonclustered index

The following example creates a unique nonclustered index on the `Name` column of the `Production.UnitMeasure` table in the `AdventureWorks2012` database. The index will enforce uniqueness on the data inserted into the `Name` column.

```sql
CREATE UNIQUE INDEX AK_UnitMeasure_Name
  ON Production.UnitMeasure(Name);
```

The following query tests the uniqueness constraint by attempting to insert a row with the same value as that in an existing row.

```sql
-- Verify the existing value.
SELECT Name FROM Production.UnitMeasure WHERE Name = N'Ounces';
GO

INSERT INTO Production.UnitMeasure (UnitMeasureCode, Name, ModifiedDate)
  VALUES ('OC', 'Ounces', GETDATE());
```

The resulting error message is:

```output
Server: Msg 2601, Level 14, State 1, Line 1
Cannot insert duplicate key row in object 'UnitMeasure' with unique index 'AK_UnitMeasure_Name'. The statement has been terminated.
```

### F. Use the IGNORE_DUP_KEY option

The following example demonstrates the effect of the `IGNORE_DUP_KEY` option by inserting multiple rows into a temporary table first with the option set to `ON` and again with the option set to `OFF`. A single row is inserted into the `#Test` table that will intentionally cause a duplicate value when the second multiple-row `INSERT` statement is executed. A count of rows in the table returns the number of rows inserted.

```sql
CREATE TABLE #Test (C1 NVARCHAR(10), C2 NVARCHAR(50), C3 DATETIME);
GO

CREATE UNIQUE INDEX AK_Index ON #Test (C2)
  WITH (IGNORE_DUP_KEY = ON);
GO

INSERT INTO #Test VALUES (N'OC', N'Ounces', GETDATE());
INSERT INTO #Test SELECT * FROM Production.UnitMeasure;
GO

SELECT COUNT(*) AS [Number of rows] FROM #Test;
GO

DROP TABLE #Test;
GO
```

Here are the results of the second `INSERT` statement.

```output
Server: Msg 3604, Level 16, State 1, Line 5 Duplicate key was ignored.

Number of rows
--------------
38
```

Notice that the rows inserted from the `Production.UnitMeasure` table that did not violate the uniqueness constraint were successfully inserted. A warning was issued and the duplicate row ignored, but the entire transaction was not rolled back.

The same statements are executed again, but with `IGNORE_DUP_KEY` set to `OFF`.

```sql
CREATE TABLE #Test (C1 NVARCHAR(10), C2 NVARCHAR(50), C3 DATETIME);
GO

CREATE UNIQUE INDEX AK_Index ON #Test (C2)
  WITH (IGNORE_DUP_KEY = OFF);
GO

INSERT INTO #Test VALUES (N'OC', N'Ounces', GETDATE());
INSERT INTO #Test SELECT * FROM Production.UnitMeasure;
GO

SELECT COUNT(*) AS [Number of rows] FROM #Test;
GO

DROP TABLE #Test;
GO
```

Here are the results of the second `INSERT` statement.

```output
Server: Msg 2601, Level 14, State 1, Line 5
Cannot insert duplicate key row in object '#Test' with unique index
'AK_Index'. The statement has been terminated.

Number of rows
--------------
1
```

Notice that none of the rows from the `Production.UnitMeasure` table were inserted into the table even though only one row in the table violated the `UNIQUE` index constraint.

### G. Using DROP_EXISTING to drop and re-create an index

The following example drops and re-creates an existing index on the `ProductID` column of the `Production.WorkOrder` table in the `AdventureWorks2012` database by using the `DROP_EXISTING` option. The options `FILLFACTOR` and `PAD_INDEX` are also set.

```sql
CREATE NONCLUSTERED INDEX IX_WorkOrder_ProductID
  ON Production.WorkOrder(ProductID)
    WITH (FILLFACTOR = 80,
      PAD_INDEX = ON,
      DROP_EXISTING = ON);
GO
```

### H. Create an index on a view

The following example creates a view and an index on that view. Two queries are included that use the indexed view.

```sql
-- Set the options to support indexed views
SET NUMERIC_ROUNDABORT OFF;
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT,
  QUOTED_IDENTIFIER, ANSI_NULLS ON;
GO

-- Create view with schemabinding
IF OBJECT_ID ('Sales.vOrders', 'view') IS NOT NULL
  DROP VIEW Sales.vOrders;
GO

CREATE VIEW Sales.vOrders
  WITH SCHEMABINDING
AS
  SELECT SUM(UnitPrice * OrderQty * (1.00 - UnitPriceDiscount)) AS Revenue,
    OrderDate, ProductID, COUNT_BIG(*) AS COUNT
  FROM Sales.SalesOrderDetail AS od, Sales.SalesOrderHeader AS o
  WHERE od.SalesOrderID = o.SalesOrderID
  GROUP BY OrderDate, ProductID;
GO

-- Create an index on the view
CREATE UNIQUE CLUSTERED INDEX IDX_V1
  ON Sales.vOrders (OrderDate, ProductID);
GO

-- This query can use the indexed view even though the view is
-- not specified in the FROM clause.
SELECT SUM(UnitPrice * OrderQty * (1.00 - UnitPriceDiscount)) AS Rev,
  OrderDate, ProductID
FROM Sales.SalesOrderDetail AS od
  JOIN Sales.SalesOrderHeader AS o ON od.SalesOrderID = o.SalesOrderID
    AND ProductID BETWEEN 700 AND 800
    AND OrderDate >= CONVERT(DATETIME, '05/01/2002', 101)
GROUP BY OrderDate, ProductID
ORDER BY Rev DESC;
GO

-- This query can use the above indexed view
SELECT OrderDate, SUM(UnitPrice * OrderQty * (1.00 - UnitPriceDiscount)) AS Rev
FROM Sales.SalesOrderDetail AS od
  JOIN Sales.SalesOrderHeader AS o ON od.SalesOrderID = o.SalesOrderID
    AND DATEPART(mm, OrderDate) = 3
  AND DATEPART(yy, OrderDate) = 2002
GROUP BY OrderDate
ORDER BY OrderDate ASC;
GO
```

### I. Create an index with included (non-key) columns

The following example creates a nonclustered index with one key column (`PostalCode`) and four non-key columns (`AddressLine1`, `AddressLine2`, `City`, `StateProvinceID`). A query that is covered by the index follows. To display the index that is selected by the query optimizer, on the **Query** menu in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **Display Actual Execution Plan** before executing the query.

```sql
CREATE NONCLUSTERED INDEX IX_Address_PostalCode
  ON Person.Address (PostalCode)
  INCLUDE (AddressLine1, AddressLine2, City, StateProvinceID);
GO

SELECT AddressLine1, AddressLine2, City, StateProvinceID, PostalCode
FROM Person.Address
WHERE PostalCode BETWEEN N'98000' and N'99999';
GO
```

### J. Create a partitioned index

The following example creates a nonclustered partitioned index on `TransactionsPS1`, an existing partition scheme in the `AdventureWorks2012` database. This example assumes the partitioned index sample has been installed.

```sql
CREATE NONCLUSTERED INDEX IX_TransactionHistory_ReferenceOrderID
  ON Production.TransactionHistory (ReferenceOrderID)
  ON TransactionsPS1 (TransactionDate);
GO
```

### K. Creating a filtered index

The following example creates a filtered index on the Production.BillOfMaterials table in the `AdventureWorks2012` database. The filter predicate can include columns that are not key columns in the filtered index. The predicate in this example selects only the rows where EndDate is non-NULL.

```sql
CREATE NONCLUSTERED INDEX "FIBillOfMaterialsWithEndDate"
  ON Production.BillOfMaterials (ComponentID, StartDate)
  WHERE EndDate IS NOT NULL;
```

### L. Create a compressed index

The following example creates an index on a nonpartitioned table by using row compression.

```sql
CREATE NONCLUSTERED INDEX IX_INDEX_1
  ON T1 (C2)
  WITH (DATA_COMPRESSION = ROW);
GO
```

The following example creates an index on a partitioned table by using row compression on all partitions of the index.

```sql
CREATE CLUSTERED INDEX IX_PartTab2Col1
  ON PartitionTable1 (Col1)
  WITH (DATA_COMPRESSION = ROW);
GO
```

 The following example creates an index on a partitioned table by using page compression on partition `1` of the index and row compression on partitions `2` through `4` of the index.

```sql
CREATE CLUSTERED INDEX IX_PartTab2Col1
  ON PartitionTable1 (Col1)
  WITH (
    DATA_COMPRESSION = PAGE ON PARTITIONS(1),
    DATA_COMPRESSION = ROW ON PARTITIONS (2 TO 4)
  );
GO
```

### M. Create an index with XML compression

**Applies to**: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

The following example creates an index on a nonpartitioned table by using XML compression. At least one column in the index must be the **xml** data type.

```sql
CREATE NONCLUSTERED INDEX IX_INDEX_1
  ON T1 (C2)
  WITH (XML_COMPRESSION = ON);
GO
```

The following example creates an index on a partitioned table by using XML compression on all partitions of the index.

```sql
CREATE CLUSTERED INDEX IX_PartTab2Col1
  ON PartitionTable1 (Col1)
  WITH (XML_COMPRESSION = ON);
GO
```

### N. Create, resume, pause, and abort resumable index operations

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

```sql
-- Execute a resumable online index create statement with MAXDOP=1
CREATE INDEX test_idx1 ON test_table (col1) WITH (ONLINE = ON, MAXDOP = 1, RESUMABLE = ON);

-- Executing the same command again (see above) after an index operation was paused, resumes automatically the index create operation.

-- Execute a resumable online index creates operation with MAX_DURATION set to 240 minutes. After the time expires, the resumable index create operation is paused.
CREATE INDEX test_idx2 ON test_table (col2) WITH (ONLINE = ON, RESUMABLE = ON, MAX_DURATION = 240);

-- Pause a running resumable online index creation
ALTER INDEX test_idx1 ON test_table PAUSE;
ALTER INDEX test_idx2 ON test_table PAUSE;

-- Resume a paused online index creation
ALTER INDEX test_idx1 ON test_table RESUME;
ALTER INDEX test_idx2 ON test_table RESUME;

-- Abort resumable index create operation which is running or paused
ALTER INDEX test_idx1 ON test_table ABORT;
ALTER INDEX test_idx2 ON test_table ABORT;
```

### O. CREATE INDEX with different low priority lock options

The following examples use the `WAIT_AT_LOW_PRIORITY` option to specify different strategies for dealing with blocking.

```sql
--Kill this session after waiting 5 minutes
CREATE CLUSTERED INDEX idx_1 ON dbo.T2 (a) WITH (ONLINE = ON (WAIT_AT_LOW_PRIORITY (MAX_DURATION = 5 MINUTES, ABORT_AFTER_WAIT = SELF)));
GO
```

```sql
--Kill blocker sessions
CREATE CLUSTERED INDEX idx_1 ON dbo.T2 (a) WITH (ONLINE = ON (WAIT_AT_LOW_PRIORITY (MAX_DURATION = 5 MINUTES, ABORT_AFTER_WAIT = BLOCKERS)));
GO
```

The following example uses both the `RESUMABLE` option and specifies two `MAX_DURATION` values, the first applies to the `ABORT_AFTER_WAIT` option, the second applies to the `RESUMABLE` option.

```sql
--With resumable option; default locking behavior 
CREATE CLUSTERED INDEX idx_1 ON dbo.T2 (a) WITH (ONLINE = ON (WAIT_AT_LOW_PRIORITY (MAX_DURATION = 5 MINUTES, ABORT_AFTER_WAIT = NONE)), RESUMABLE = ON, MAX_DURATION = 240 MINUTES);
```

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### P. Basic syntax

Create, resume, pause, and abort resumable index operations

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

```sql
-- Execute a resumable online index create statement with MAXDOP=1
CREATE INDEX test_idx ON test_table WITH (ONLINE = ON, MAXDOP = 1, RESUMABLE = ON);

-- Executing the same command again (see above) after an index operation was paused, resumes automatically the index create operation.

-- Execute a resumable online index creates operation with MAX_DURATION set to 240 minutes. After the time expires, the resumable index create operation is paused.
CREATE INDEX test_idx ON test_table WITH (ONLINE = ON, RESUMABLE = ON, MAX_DURATION = 240);

-- Pause a running resumable online index creation
ALTER INDEX test_idx ON test_table PAUSE;

-- Resume a paused online index creation
ALTER INDEX test_idx ON test_table RESUME;

-- Abort resumable index create operation which is running or paused
ALTER INDEX test_idx ON test_table ABORT;
```

### Q. Create a nonclustered index on a table in the current database

The following example creates a nonclustered index on the `VendorID` column of the `ProductVendor` table.

```sql
CREATE INDEX IX_ProductVendor_VendorID
  ON ProductVendor (VendorID);
```

### R. Create a clustered index on a table in another database

The following example creates a nonclustered index on the `VendorID` column of the `ProductVendor` table in the `Purchasing` database.

```sql
CREATE CLUSTERED INDEX IX_ProductVendor_VendorID
  ON Purchasing..ProductVendor (VendorID);
```

### S. Create an ordered clustered index on a table

The following example creates an ordered clustered index on the `c1` and `c2` columns of the `T1` table in the `MyDB` database.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX MyOrderedCCI ON MyDB.dbo.T1 
ORDER (c1, c2);
```

### T. Convert a CCI to an ordered clustered index on a table

The following example converts the existing clustered columnstore index to an ordered clustered columnstore index called `MyOrderedCCI` on the `c1` and `c2` columns of the `T2` table in the `MyDB` database.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX MyOrderedCCI ON MyDB.dbo.T2
ORDER (c1, c2)
WITH (DROP_EXISTING = ON);
```

## See also

- [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md)
- [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md)
- [Indexes and ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md#indexes-and-alter-table)
- [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE PARTITION FUNCTION](../../t-sql/statements/create-partition-function-transact-sql.md)
- [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md)
- [CREATE SPATIAL INDEX](../../t-sql/statements/create-spatial-index-transact-sql.md)
- [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md)
- [CREATE XML INDEX](../../t-sql/statements/create-xml-index-transact-sql.md)
- [Data Types](../../t-sql/data-types/data-types-transact-sql.md)
- [DBCC SHOW_STATISTICS](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md)
- [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md)
- [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.xml_indexes](../../relational-databases/system-catalog-views/sys-xml-indexes-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
