---
title: ALTER TABLE (Transact-SQL)
description: ALTER TABLE modifies a table definition by altering, adding, or dropping columns and constraints. ALTER TABLE also reassigns and rebuilds partitions, or disables and enables constraints and triggers.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/5/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "WAIT_AT_LOW_PRIORITY"
  - "ABORT_AFTER_WAIT"
  - "ABORT_AFTER_WAIT_TSQL"
  - "ALTER_TABLE_TSQL"
  - "ALTER TABLE"
  - "WAIT_AT_LOW_PRIORITY_TSQL"
  - "ALTER_COLUMN_TSQL"
helpviewer_keywords:
  - "columns [SQL Server], resizing"
  - "changing column size"
  - "MAXDOP index option, ALTER TABLE statement"
  - "table modifications [SQL Server], ALTER TABLE"
  - "ALTER TABLE statement"
  - "modifying tables"
  - "partitioned tables [SQL Server], lock escalation"
  - "resizing columns"
  - "removing columns"
  - "switching partitions"
  - "reassigning partitions"
  - "removing constraints"
  - "triggers [SQL Server], disabling"
  - "columns [SQL Server], adding"
  - "LOCK_ESCALATION option of ALTER TABLE"
  - "constraints [SQL Server], deleting"
  - "constraints [SQL Server], disabling"
  - "triggers [SQL Server], enabling"
  - "re-enabling constraints"
  - "index modifications [SQL Server]"
  - "disabling constraints"
  - "columns [SQL Server], removing"
  - "max degree of parallelism option"
  - "locking [SQL Server], tables"
  - "ONLINE option"
  - "disabling triggers"
  - "constraints [SQL Server], adding"
  - "deleting constraints"
  - "adding constraints"
  - "adding columns"
  - "SWITCH partitions"
  - "partitioned tables [SQL Server], switching"
  - "lock escalation [SQL Server], option of ALTER TABLE"
  - "constraints [SQL Server], enabling"
  - "dropping constraints"
  - "dropping columns"
  - "data retention policy"
  - "table changes [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# ALTER TABLE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

Modifies a table definition by altering, adding, or dropping columns and constraints. `ALTER TABLE` also reassigns and rebuilds partitions, or disables and enables constraints and triggers.

> [!TIP]  
> The syntax of `ALTER TABLE` varies in different versions of the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md). Use the version selector dropdown list to [choose the appropriate product version](../../sql-server/sql-docs-navigation-guide.md#what-the-applies-to-options-mean).

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"

The syntax for `ALTER TABLE` is different for disk-based tables and memory-optimized tables. Use the following links to take you directly to the appropriate syntax block for your table types and to the appropriate syntax examples:

**Disk-based tables:**

- [Syntax](#syntax-for-disk-based-tables)
- [Examples](#Example_Top)

**Memory-optimized tables:**

- [Syntax](#syntax-for-memory-optimized-tables)
- [Altering Memory-Optimized Tables](../../relational-databases/in-memory-oltp/altering-memory-optimized-tables.md)

For more information about the syntax conventions, see [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax for disk-based tables

```syntaxsql
ALTER TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
{
    ALTER COLUMN column_name
    {
        [ type_schema_name. ] type_name
            [ (
                {
                   precision [ , scale ]
                 | max
                 | xml_schema_collection
                }
            ) ]
        [ COLLATE collation_name ]
        [ NULL | NOT NULL ] [ SPARSE ]
      | { ADD | DROP }
          { ROWGUIDCOL | PERSISTED | NOT FOR REPLICATION | SPARSE | HIDDEN }
      | { ADD | DROP } MASKED [ WITH ( FUNCTION = ' mask_function ') ]
    }
    [ WITH ( ONLINE = ON | OFF ) ]
    | [ WITH { CHECK | NOCHECK } ]

    | ADD
    {
        <column_definition>
      | <computed_column_definition>
      | <table_constraint>
      | <column_set_definition>
    } [ ,...n ]
      | [ system_start_time_column_name datetime2 GENERATED ALWAYS AS ROW START
                [ HIDDEN ] [ NOT NULL ] [ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES] ,
                system_end_time_column_name datetime2 GENERATED ALWAYS AS ROW END
                   [ HIDDEN ] [ NOT NULL ][ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES] ,
                start_transaction_id_column_name bigint GENERATED ALWAYS AS TRANSACTION_ID START
                   [ HIDDEN ] NOT NULL [ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES],
                  end_transaction_id_column_name bigint GENERATED ALWAYS AS TRANSACTION_ID END
                   [ HIDDEN ] NULL [ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES],
                  start_sequence_number_column_name bigint GENERATED ALWAYS AS SEQUENCE_NUMBER START
                   [ HIDDEN ] NOT NULL [ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES],
                  end_sequence_number_column_name bigint GENERATED ALWAYS AS SEQUENCE_NUMBER END
                   [ HIDDEN ] NULL [ CONSTRAINT constraint_name ]
            DEFAULT constant_expression [WITH VALUES]
        ]
       PERIOD FOR SYSTEM_TIME ( system_start_time_column_name, system_end_time_column_name )
    | DROP
     [ {
         [ CONSTRAINT ][ IF EXISTS ]
         {
              constraint_name
              [ WITH
               ( <drop_clustered_constraint_option> [ ,...n ] )
              ]
          } [ ,...n ]
          | COLUMN [ IF EXISTS ]
          {
              column_name
          } [ ,...n ]
          | PERIOD FOR SYSTEM_TIME
     } [ ,...n ] ]
    | [ WITH { CHECK | NOCHECK } ] { CHECK | NOCHECK } CONSTRAINT
        { ALL | constraint_name [ ,...n ] }

    | { ENABLE | DISABLE } TRIGGER
        { ALL | trigger_name [ ,...n ] }

    | { ENABLE | DISABLE } CHANGE_TRACKING
        [ WITH ( TRACK_COLUMNS_UPDATED = { ON | OFF } ) ]

    | SWITCH [ PARTITION source_partition_number_expression ]
        TO target_table
        [ PARTITION target_partition_number_expression ]
        [ WITH ( <low_priority_lock_wait> ) ]

    | SET
        (
            [ FILESTREAM_ON =
                { partition_scheme_name | filegroup | "default" | "NULL" } ]
            | SYSTEM_VERSIONING =
                  {
                    OFF
                  | ON
                      [ ( HISTORY_TABLE = schema_name . history_table_name
                          [, DATA_CONSISTENCY_CHECK = { ON | OFF } ]
                          [, HISTORY_RETENTION_PERIOD =
                          {
                              INFINITE | number {DAY | DAYS | WEEK | WEEKS
                  | MONTH | MONTHS | YEAR | YEARS }
                          }
                          ]
                        )
                      ]
                  }
            | DATA_DELETION =
                {
                      OFF
                    | ON
                        [(  [ FILTER_COLUMN = column_name ]
                            [, RETENTION_PERIOD = { INFINITE | number { DAY | DAYS | WEEK | WEEKS
                                    | MONTH | MONTHS | YEAR | YEARS } } ]
                        )]
                    } )
    | REBUILD
      [ [PARTITION = ALL]
        [ WITH ( <rebuild_option> [ ,...n ] ) ]
      | [ PARTITION = partition_number
           [ WITH ( <single_partition_rebuild_option> [ ,...n ] ) ]
        ]
      ]

    | <table_option>
    | <filetable_option>
    | <stretch_configuration>
}
[ ; ]

-- ALTER TABLE options

<column_set_definition> ::=
    column_set_name XML COLUMN_SET FOR ALL_SPARSE_COLUMNS

<drop_clustered_constraint_option> ::=
    {
        MAXDOP = max_degree_of_parallelism
      | ONLINE = { ON | OFF }
      | MOVE TO
         { partition_scheme_name ( column_name ) | filegroup | "default" }
    }
<table_option> ::=
    {
        SET ( LOCK_ESCALATION = { AUTO | TABLE | DISABLE } )
    }

<filetable_option> ::=
    {
       [ { ENABLE | DISABLE } FILETABLE_NAMESPACE ]
       [ SET ( FILETABLE_DIRECTORY = directory_name ) ]
    }

<stretch_configuration> ::=
    {
      SET (
        REMOTE_DATA_ARCHIVE
        {
            = ON (<table_stretch_options>)
          | = OFF_WITHOUT_DATA_RECOVERY ( MIGRATION_STATE = PAUSED )
          | ( <table_stretch_options> [, ...n] )
        }
            )
    }

<table_stretch_options> ::=
    {
     [ FILTER_PREDICATE = { null | table_predicate_function } , ]
       MIGRATION_STATE = { OUTBOUND | INBOUND | PAUSED }
    }

<single_partition_rebuild__option> ::=
{
      SORT_IN_TEMPDB = { ON | OFF }
    | MAXDOP = max_degree_of_parallelism
    | DATA_COMPRESSION = { NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE}
    | ONLINE = { ON [( <low_priority_lock_wait> ) ] | OFF }
}

<low_priority_lock_wait>::=
{
    WAIT_AT_LOW_PRIORITY ( MAX_DURATION = <time> [ MINUTES ],
        ABORT_AFTER_WAIT = { NONE | SELF | BLOCKERS } )
}
```

For more information, see:

- [ALTER TABLE column_constraint (Transact-SQL)](alter-table-column-constraint-transact-sql.md)
- [ALTER TABLE column_definition (Transact-SQL)](alter-table-column-definition-transact-sql.md)
- [ALTER TABLE computed_column_definition (Transact-SQL)](alter-table-computed-column-definition-transact-sql.md)
- [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md)
- [ALTER TABLE table_constraint (Transact-SQL)](alter-table-table-constraint-transact-sql.md)

## Syntax for memory-optimized tables

```syntaxsql
ALTER TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
{
    ALTER COLUMN column_name
    {
        [ type_schema_name. ] type_name
            [ (
                {
                   precision [ , scale ]
                }
            ) ]
        [ COLLATE collation_name ]
        [ NULL | NOT NULL ]
    }

    | ALTER INDEX index_name
    {
        [ type_schema_name. ] type_name
        REBUILD
        [ [ NONCLUSTERED ] WITH ( BUCKET_COUNT = bucket_count )
        ]
    }

    | ADD
    {
        <column_definition>
      | <computed_column_definition>
      | <table_constraint>
      | <table_index>
      | <column_index>
    } [ ,...n ]

    | DROP
     [ {
         CONSTRAINT [ IF EXISTS ]
         {
              constraint_name
          } [ ,...n ]
        | INDEX [ IF EXISTS ]
      {
         index_name
       } [ ,...n ]
          | COLUMN [ IF EXISTS ]
          {
              column_name
          } [ ,...n ]
          | PERIOD FOR SYSTEM_TIME
     } [ ,...n ] ]
    | [ WITH { CHECK | NOCHECK } ] { CHECK | NOCHECK } CONSTRAINT
        { ALL | constraint_name [ ,...n ] }

    | { ENABLE | DISABLE } TRIGGER
        { ALL | trigger_name [ ,...n ] }

    | SWITCH [ [ PARTITION ] source_partition_number_expression ]
        TO target_table
        [ PARTITION target_partition_number_expression ]
        [ WITH ( <low_priority_lock_wait> ) ]

}
[ ; ]

-- ALTER TABLE options

< table_constraint > ::=
 [ CONSTRAINT constraint_name ]
{
   {PRIMARY KEY | UNIQUE }
     {
       NONCLUSTERED (column [ ASC | DESC ] [ ,... n ])
       | NONCLUSTERED HASH (column [ ,... n ] ) WITH ( BUCKET_COUNT = bucket_count )
     }
    | FOREIGN KEY
        ( column [ ,...n ] )
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ]
    | CHECK ( logical_expression )
}

<column_index> ::=
  INDEX index_name
{ [ NONCLUSTERED ] | [ NONCLUSTERED ] HASH WITH (BUCKET_COUNT = bucket_count) }

<table_index> ::=
  INDEX index_name
{ [ NONCLUSTERED ] HASH (column [ ,... n ] ) WITH (BUCKET_COUNT = bucket_count)
  | [ NONCLUSTERED ] (column [ ASC | DESC ] [ ,... n ] )
      [ ON filegroup_name | default ]
  | CLUSTERED COLUMNSTORE [ WITH ( COMPRESSION_DELAY = { 0 | delay [MINUTES] } ) ]
      [ ON filegroup_name | default ]
}
```

::: moniker-end

::: moniker range=">=aps-pdw-2016 || =azure-sqldw-latest"

## Syntax for Azure Synapse Analytics and Parallel Data Warehouse

```syntaxsql
ALTER TABLE { database_name.schema_name.source_table_name | schema_name.source_table_name | source_table_name }
{
    ALTER COLUMN column_name
        {
            type_name [ ( precision [ , scale ] ) ]
            [ COLLATE Windows_collation_name ]
            [ NULL | NOT NULL ]
        }
    | ADD { <column_definition> | <column_constraint> FOR column_name} [ ,...n ]
    | DROP { COLUMN column_name | [CONSTRAINT] constraint_name } [ ,...n ]
    | REBUILD {
            [ PARTITION = ALL [ WITH ( <rebuild_option> ) ] ]
          | [ PARTITION = partition_number [ WITH ( <single_partition_rebuild_option> ] ]
      }
    | { SPLIT | MERGE } RANGE (boundary_value)
    | SWITCH [ PARTITION source_partition_number
        TO target_table_name [ PARTITION target_partition_number ] [ WITH ( TRUNCATE_TARGET = ON | OFF ) ] ]
}
[ ; ]

<column_definition>::=
{
    column_name
    type_name [ ( precision [ , scale ] ) ]
    [ <column_constraint> ]
    [ COLLATE Windows_collation_name ]
    [ NULL | NOT NULL ]
}

<column_constraint>::=
    [ CONSTRAINT constraint_name ]
    {
        DEFAULT constant_expression
        | PRIMARY KEY NONCLUSTERED (column_name [ ,... n ]) NOT ENFORCED -- Applies to Azure Synapse Analytics only
        | UNIQUE (column_name [ ,... n ]) NOT ENFORCED -- Applies to Azure Synapse Analytics only
    }
<rebuild_option > ::=
{
    DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }
        [ ON PARTITIONS ( {<partition_number> [ TO <partition_number>] } [ , ...n ] ) ]
    | XML_COMPRESSION = { ON | OFF }
        [ ON PARTITIONS ( {<partition_number> [ TO <partition_number>] } [ , ...n ] ) ]
}

<single_partition_rebuild_option > ::=
{
    DATA_COMPRESSION = { COLUMNSTORE | COLUMNSTORE_ARCHIVE }
}
```

[!INCLUDE [synapse-analytics-od-supported-tables](../../includes/synapse-analytics-od-supported-tables.md)]

::: moniker-end

::: moniker range="=fabric"

## Syntax for Warehouse in Fabric

```syntaxsql
ALTER TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
{
    ALTER COLUMN column_name
    {
        [ type_schema_name. ] type_name
            [ (
                {
                    precision [ , scale ]
                }
            ) ]
        [ COLLATE collation_name ] 
        [ NULL | NOT NULL ]
      | { ADD | DROP } MASKED [ WITH ( FUNCTION = ' mask_function ') ]
    }
}
{
  ADD  { column_name <data_type> [COLLATE collation_name] [ <column_options> ] } [ ,...n ]
| ADD { <column_constraint> FOR column_name} [ ,...n ]
| DROP { COLUMN column_name | [CONSTRAINT] constraint_name } [ ,...n ]
}
[ ; ]

<column_options> ::=
[ NULL ] -- default is NULL

<data type> ::= type_name [ ( precision [ , scale ] ) ]

<column_constraint>::=
    [ CONSTRAINT constraint_name ]
    {
       PRIMARY KEY NONCLUSTERED (column_name [ ,... n ]) NOT ENFORCED
        | UNIQUE NONCLUSTERED (column_name [ ,... n ]) NOT ENFORCED
    | FOREIGN KEY
        ( column [ ,...n ] )
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ] NOT ENFORCED
    }
```

::: moniker-end

## Arguments

#### database_name

The name of the database where you created the table.

#### schema_name

The name of the schema to which the table belongs.

#### table_name

The name of the table to alter. If the table isn't in the current database or if the table's schema isn't owned by the current user, you must explicitly specify the database and schema.

#### ALTER COLUMN

Specifies the named column to alter.

::: moniker range=">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"

`ALTER TABLE ... ALTER COLUMN` for Fabric Data Warehouse has different capabilities. For more information, see [the Fabric Data Warehouse version of this article](alter-table-transact-sql.md?view=fabric&preserve-view=true#alter-column).

> [!NOTE]  
> `ALTER TABLE ... ALTER COLUMN` is in preview for Fabric Data Warehouse.

The modified column can't be:

- A column with a **timestamp** data type.
- The `ROWGUIDCOL` for the table.
- A computed column or used in a computed column.
- Used in statistics generated by the `CREATE STATISTICS` statement. To drop these statistics, run `DROP STATISTICS` before `ALTER COLUMN` can succeed. Run this query to get all the user-created statistics and statistics columns for a table.

   ```sql
   SELECT s.name AS statistics_name,
         c.name AS column_name,
         sc.stats_column_id
   FROM sys.stats AS s
       INNER JOIN sys.stats_columns AS sc
           ON s.object_id = sc.object_id
          AND s.stats_id = sc.stats_id
       INNER JOIN sys.columns AS c
           ON sc.object_id = c.object_id
          AND c.column_id = sc.column_id
   WHERE s.object_id = OBJECT_ID('<table_name>');
   ```

- Used in a `PRIMARY KEY` or `[FOREIGN KEY] REFERENCES` constraint.
- Used in a `CHECK` or `UNIQUE` constraint. However, you can change the length of a variable-length column used in a `CHECK` or `UNIQUE` constraint.
- Associated with a default definition. However, you can change the length, precision, or scale of a column if you don't change the data type.

`ALTER COLUMN` drops statistics that the query optimizer automatically generates.

You can change the data type of **text**, **ntext**, and **image** columns only in the following ways:

- **text** to **varchar(max)**, **nvarchar(max)**, or **xml**
- **ntext** to **varchar(max)**, **nvarchar(max)**, or **xml**
- **image** to **varbinary(max)**

Some data type changes might cause a change in the data. For example, changing a **nchar** or **nvarchar** column to **char** or **varchar** might cause the conversion of extended characters. For more information, see [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md). 

- Reducing the precision or scale of a column can cause data truncation.
- You can't change the data type of a column in a partitioned table.
- You can't change the data type of columns included in an index unless the column is a **varchar**, **nvarchar**, or **varbinary** data type, and the new size is equal to or larger than the old size.
- You can't change a column included in a primary key constraint from `NOT NULL` to `NULL`.

When you use Always Encrypted (without secure enclaves), if you modify a column that's encrypted with `ENCRYPTED WITH`, you can change the data type to a compatible data type (such as **int** to **bigint**), but you can't change any encryption settings.

When you use Always Encrypted with secure enclaves, you can change any encryption setting if the column encryption key protecting the column (and the new column encryption key, if you're changing the key) supports enclave computations (encrypted with enclave-enabled column `master` keys). For details, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

When you modify a column, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] keeps track of each modification by adding a row in a system table and marking the previous column modification as a dropped column. In the rare case that you modify a column too many times, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] might reach the record size limit. If this happens, you get error [MSSQLSERVER_511](../../relational-databases/errors-events/mssqlserver-511-database-engine-error.md) or 1708. To avoid these errors, either rebuild the clustered index on the table periodically or reduce the number of column modifications.

::: moniker-end

::: moniker range="=fabric"

Supported `ALTER TABLE ... ALTER COLUMN` scenarios allow certain column definition changes to be applied *without modifying the underlying stored data files*. These operations update table metadata and enable compatible interpretation of existing data during subsequent reads.

> [!NOTE]  
> `ALTER TABLE ... ALTER COLUMN` is in preview for Fabric Data Warehouse.

The updated column definition must remain compatible with the existing stored data to ensure correctness during query execution.

Any ALTER TABLE DDL operation (including ALTER COLUMN) acquires a schema modification (SCH M) lock for the duration of execution. As a result, it can block, or be blocked by, concurrently executing workloads. For more information, see [Locking Transactions in Fabric Data Warehouse](/fabric/data-warehouse/transactions#understand-locking-and-blocking-in-fabric-data-warehouse).

##### Supported schema changes

Currently, `ALTER COLUMN` in Fabric Data Warehouse supports metadata only schema evolution scenarios where the updated column definition does not require validation of existing stored data or rewriting the underlying Parquet data files.

Only the following column type changes are supported:

| **Type Category** | **Source Type** |**Allowed Target Types**    |
|---|--|-----|
| **Integer widening** | **smallint** | - **int**<br> - **bigint** |
|                       | **int** |  **bigint** |
| **Floating-point widening** | **real** | **float** |
|                             | - **smallint**<br> - **int** | **float**|
| **Decimal widening** | **decimal**(p, s) | **decimal**(p + k1, s + k2) where k1 ≥ k2 ≥ 0 |
|                             | - **smallint**<br> - **int** | **decimal**(10 + k1, k2) where k1 ≥ k2 ≥ 0 |
| **Decimal/numeric interchange** | **decimal**(p, s) | **numeric**(p,s) |
|                             | **numeric**(p,s) | **decimal**(p,s)|
| **Float/real interchange<sup>1</sup>** | **float**(n < 25) | **real** |
|                             | **float**(n)  | **float**(n+m) |
|                             | **real**  | **float**(n) |
| **Time widening<sup>2</sup>**           | **time**  | **datetime2** |
|                             | **datetime2**(n) | **datetime2**(n+m) |
| **String widening**         | **char**(n)   | - **varchar**(n + m)<br> - **char**(n + m)|
|                             | **varchar**(n) | **varchar**(n + m)<br> - **char**(n + m)|
| **Binary widening**         | **varbinary**(n) | **varbinary**(n + m)|

<sup>1</sup> In the SQL Database Engine, **float** precision &lt; 25 is stored as **float/real** (4 bytes), and precision ≥ 25 is stored as **double** (8 bytes). Widening from **float** to **double** is supported, but narrowing from **double** to **float** is not supported.

<sup>2</sup> When converting a column from **time** to **datetime2**, the date component is populated with the value `1970 01 01` during query interpretation. This behavior differs from SQL Server, which uses `1900 01 01`. This occurs because timestamps in Fabric Data Warehouse are stored using the Delta Lake TIMESTAMP type, which uses the Unix epoch (1970 01 01).

##### ALTER COLUMN cross-engine interoperability

In Fabric Data Warehouse, some `ALTER COLUMN` operations may enable **type widening** at the storage layer. When this occurs, external engines accessing the same Delta tables must support compatible read time type interpretation. **Type widening** is described in the [public Delta documentation](https://github.com/delta-io/delta/blob/master/PROTOCOL.md#type-widening), and occurs when an existing data type is compatible with a wider type. For more information on engines that support type widening, see [Delta Lake table format interoperability](/fabric/fundamentals/delta-lake-interoperability#delta-lake-features-and-fabric-experiences).
If you need to remove type widening from a table schema, you can create a new table using `CREATE TABLE AS SELECT`. For more information, see [CREATE TABLE AS SELECT](create-table-as-select-azure-sql-data-warehouse.md?view=fabric&preserve-view=true).

##### Limitations of ALTER COLUMN in Fabric Data Warehouse

> [!NOTE]  
> `ALTER TABLE ... ALTER COLUMN` is in preview for Fabric Data Warehouse.

In Fabric Data Warehouse, these `ALTER COLUMN` operations are not supported:

- Changing a column type from `NULL` to `NOT NULL`
- Altering an IDENTITY column
- Altering a column to a different collation
- Reducing the size of a column of the same type
- Decrease precision when converting from **time** to **datetime2**
- Altering a column with manually created stats
- Altering a column that is a part of the [data clustering](/fabric/data-warehouse/data-clustering) index

::: moniker-end

#### column_name

The name of the column to alter, add, or drop. The *column_name* maximum is 128 characters. 

For new columns, you can omit *column_name* for columns created with a **timestamp** data type. The name **timestamp** is used if you don't specify *column_name* for a **timestamp** data type column.

> [!NOTE]  
> New columns are added after all existing columns in the table being altered.

#### [ *type_schema_name*. ] *type_name*

The new data type for the altered column, or the data type for the added column. You can't specify *type_name* for existing columns of partitioned tables. *type_name* can be any one of the following types:

- A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system data type.
- An alias data type based on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system data type. You create alias data types with the `CREATE TYPE` statement before they can be used in a table definition.
- A [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type, and the schema to which it belongs. You create user-defined types with the `CREATE TYPE` statement before they can be used in a table definition.

The following criteria apply to *type_name* of an altered column:

- The previous data type must be implicitly convertible to the new data type.
- *type_name* can't be **timestamp**.
- `ANSI_NULL` defaults are always on for `ALTER COLUMN`; if not specified, the column is nullable.
- `ANSI_PADDING` padding is always `ON` for `ALTER COLUMN`.
- If the modified column is an identity column, *new_data_type* must be a data type that supports the identity property.
- The current setting for `SET ARITHABORT` is ignored. `ALTER TABLE` operates as if `ARITHABORT` is set to `ON`.

> [!NOTE]  
> If you don't specify the `COLLATE` clause, changing the data type of a column causes a collation change to the default collation of the database.

#### precision

The precision for the specified data type. For more information about valid precision values, see [Precision, scale, and length (Transact-SQL)](../data-types/precision-scale-and-length-transact-sql.md).

#### scale

The scale for the specified data type. For more information about valid scale values, see [Precision, scale, and length (Transact-SQL)](../data-types/precision-scale-and-length-transact-sql.md).

#### max

Applies only to the **varchar**, **nvarchar**, and **varbinary** data types for storing 2^31-1 bytes of character, binary data, and of Unicode data.

#### xml_schema_collection

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Applies only to the **xml** data type for associating an XML schema with the type. Before typing an **xml** column to a schema collection, you first create the schema collection in the database by using [CREATE XML SCHEMA COLLECTION (Transact-SQL)](create-xml-schema-collection-transact-sql.md).

#### COLLATE \<*collation_name*>

Specifies the new collation for the altered column. If you don't specify a collation, the column is assigned the default collation of the database. Collation name can be either a Windows collation name or a SQL collation name. For a list and more information, see [Windows collation name (Transact-SQL)](windows-collation-name-transact-sql.md) and [SQL Server Collation Name (Transact-SQL)](sql-server-collation-name-transact-sql.md).

The `COLLATE` clause changes the collations only of columns of the **char**, **varchar**, **nchar**, and **nvarchar** data types. To change the collation of a user-defined alias data type column, use separate `ALTER TABLE` statements to change the column to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system data type. Then, change its collation and change the column back to an alias data type.

`ALTER COLUMN` can't have a collation change if one or more of the following conditions exist:

- A `CHECK` constraint, `FOREIGN KEY` constraint, or computed columns reference the column changed.
- Any index, statistics, or full-text index are created on the column. Statistics created automatically on the column changed are dropped if the column collation is changed.
- A schema-bound view or function references the column.

For more information on supported collations, see [COLLATE (Transact-SQL)](collations.md).

#### NULL | NOT NULL

Specifies whether the column can accept null values. You can add columns that don't allow null values by using `ALTER TABLE` only if they have a default specified or if the table is empty. You can specify `NOT NULL` for computed columns only if you also specify `PERSISTED`. If the new column allows null values and you don't specify a default, the new column contains a null value for each row in the table. If the new column allows null values and you add a default definition with the new column, you can use `WITH VALUES` to store the default value in the new column for each existing row in the table.

If the new column doesn't allow null values and the table isn't empty, you must add a `DEFAULT` definition with the new column. The new column automatically loads with the default value in the new columns in each existing row.

You can specify `NULL` in `ALTER COLUMN` to force a `NOT NULL` column to allow null values, except for columns in `PRIMARY KEY` constraints. You can specify `NOT NULL` in `ALTER COLUMN` only if the column contains no null values. You must update the null values to some value before the `ALTER COLUMN` `NOT NULL` is allowed, for example:

```sql
UPDATE MyTable SET NullCol = N'some_value' WHERE NullCol IS NULL;

ALTER TABLE MyTable ALTER COLUMN NullCOl NVARCHAR (20) NOT NULL;
```

When you create or alter a table by using the `CREATE TABLE` or `ALTER TABLE` statements, database and session settings can influence and possibly override the nullability of the data type that you specify in a column definition. Always explicitly define a column as `NULL` or `NOT NULL` for noncomputed columns.

If you add a column with a user-defined data type, be sure to define the column with the same nullability as the user-defined data type. And, specify a default value for the column. For more information, see [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md).

> [!NOTE]  
> If you specify `NULL` or `NOT NULL` with `ALTER COLUMN`, you must also specify *new_data_type* [(*precision* [, *scale* ])]. If the data type, precision, and scale aren't changing, specify the current column values.



#### [ {ADD | DROP} ROWGUIDCOL ]

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies that the `ROWGUIDCOL` property is added to or dropped from the specified column. `ROWGUIDCOL` indicates that the column is a row GUID column. You can set only one **uniqueidentifier** column per table as the `ROWGUIDCOL` column. You can only assign the `ROWGUIDCOL` property to a **uniqueidentifier** column. You can't assign `ROWGUIDCOL` to a column of a user-defined data type.

`ROWGUIDCOL` doesn't enforce uniqueness of the values stored in the column and doesn't automatically generate values for new rows that are inserted into the table. To generate unique values for each column, either use the `NEWID()` or `NEWSEQUENTIALID()` function in `INSERT` statements. Or, specify the `NEWID()` or `NEWSEQUENTIALID()` function as the default for the column.

#### [ {ADD | DROP} PERSISTED ]

Specifies that the `PERSISTED` property is added to or dropped from the specified column. The column must be a computed column that's defined with a deterministic expression. For columns specified as `PERSISTED`, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] physically stores the computed values in the table and updates the values when any other columns on which the computed column depends are updated. By marking a computed column as `PERSISTED`, you can create indexes on computed columns defined on expressions that are deterministic, but not precise. For more information, see [Indexes on computed columns](../../relational-databases/indexes/indexes-on-computed-columns.md).

`SET QUOTED_IDENTIFIER` must be `ON` when you're creating or changing indexes on computed columns or indexed views. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](set-quoted-identifier-transact-sql.md).

Any computed column that's used as a partitioning column of a partitioned table must be explicitly marked `PERSISTED`.

> [!NOTE]  
> In Fabric SQL database, computed columns are allowed, but currently aren't mirrored to Fabric OneLake.

#### DROP NOT FOR REPLICATION

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies that values are incremented in identity columns when replication agents carry out insert operations. You can specify this clause only if *column_name* is an identity column.

#### SPARSE

Indicates that the column is a sparse column. The storage of sparse columns is optimized for null values. You can't set sparse columns as `NOT NULL`. When you convert a column from sparse to nonsparse, or from nonsparse to sparse, this option locks the table for the duration of the command execution. You might need to use the `REBUILD` clause to reclaim any space savings. For additional restrictions and more information about sparse columns, see [Use sparse columns](../../relational-databases/tables/use-sparse-columns.md).

#### ADD MASKED WITH ( FUNCTION = '*mask_function*')

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies a dynamic data mask. *mask_function* is the name of the masking function with the appropriate parameters. Three functions are available:

- default()
- email()
- partial()
- random()

Requires `ALTER ANY MASK` permission.

To drop a mask, use `DROP MASKED`. For function parameters, see [Dynamic data masking](../../relational-databases/security/dynamic-data-masking.md).

Add and drop a mask require `ALTER ANY MASK` permission.

#### WITH ( ONLINE = ON | OFF) \<as applies to altering a column>

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Allows many alter column actions to be carried out while the table remains available. Default is `OFF`. You can run alter column online for column changes related to data type, column length or precision, nullability, sparseness, and collation.

Online `ALTER COLUMN` allows user-created and autostatistics to reference the altered column for the duration of the `ALTER COLUMN` operation, which allows queries to run as usual. At the end of the operation, autostats that reference the column are dropped and user-created stats are invalidated. The user must manually update user-generated statistics after the operation is completed. If the column is part of a filter expression for any statistics or indexes, you can't perform an `ALTER COLUMN` operation.

- While the online `ALTER COLUMN` operation is running, any DDL operation that could depend on that column (such as creating or modifying indexes or views) is blocked, or fails with an appropriate error. This behavior guarantees that online `ALTER COLUMN` won't fail because of dependencies introduced while the operation was running.

- Altering a column from `NOT NULL` to `NULL` isn't supported as an online operation when the altered column is referenced by nonclustered indexes.

- Online `ALTER` isn't supported when the column is referenced by a check constraint and the `ALTER` operation is restricting the precision of the column (**numeric** or **datetime**).

- The `WAIT_AT_LOW_PRIORITY` option can't be used with online `ALTER COLUMN`.

- `ALTER COLUMN ... ADD/DROP PERSISTED` isn't supported for online `ALTER COLUMN`.

- `ALTER COLUMN ... ADD/DROP ROWGUIDCOL/NOT FOR REPLICATION` isn't affected by online `ALTER COLUMN`.

- Online `ALTER COLUMN` doesn't support altering a table where change tracking is enabled or that's a publisher of merge replication.

- Online `ALTER COLUMN` doesn't support altering from or to CLR data types.

- Online `ALTER COLUMN` doesn't support altering to an XML data type that has a schema collection different than the current schema collection.

- Online `ALTER COLUMN` doesn't reduce the restrictions on when a column can be altered. References by index, stats, and so on, might cause the alter to fail.

- Online `ALTER COLUMN` doesn't support altering more than one column concurrently.

- Online `ALTER COLUMN` has no effect in a system-versioned temporal table. `ALTER` column isn't run as online regardless of which value was specified for `ONLINE` option.

Online `ALTER COLUMN` has similar requirements, restrictions, and functionality as online index rebuild, which includes:

- Online index rebuild isn't supported when the table contains legacy LOB or filestream columns or when the table has a columnstore index. The same limitations apply for online `ALTER COLUMN`.
- An existing column being altered requires twice the space allocation, for the original column and for the newly created hidden column.
- The locking strategy during an alter column online operation follows the same locking pattern used for online index build.

#### WITH CHECK | WITH NOCHECK

Specifies whether the data in the table is or isn't validated against a newly added or re-enabled `FOREIGN KEY` or `CHECK` constraint. If you don't specify, `WITH CHECK` is assumed for new constraints, and `WITH NOCHECK` is assumed for re-enabled constraints.

If you don't want to verify new `CHECK` or `FOREIGN KEY` constraints against existing data, use `WITH NOCHECK`. This is generally never recommended, but might be required in some circumstances. The new constraint is evaluated in all later data updates. Any constraint violations that are suppressed by `WITH NOCHECK` when the constraint is added might cause future updates to fail if they update rows with data that doesn't follow the constraint. The query optimizer doesn't consider constraints that are defined `WITH NOCHECK`. Such constraints are ignored until they're re-enabled by using `ALTER TABLE table WITH CHECK CHECK CONSTRAINT ALL`. For more information, see [Disable foreign key constraints with INSERT and UPDATE statements](../../relational-databases/tables/disable-foreign-key-constraints-with-insert-and-update-statements.md).

#### ALTER INDEX *index_name*

Specifies that the bucket count for *index_name* is to be changed or altered.

The syntax `ALTER TABLE ... ADD/DROP/ALTER INDEX` is supported only for memory-optimized tables.

> [!IMPORTANT]  
> Without using an `ALTER TABLE` statement, the statements [CREATE INDEX (Transact-SQL)](create-index-transact-sql.md), [DROP INDEX (Transact-SQL)](drop-index-transact-sql.md), [ALTER INDEX (Transact-SQL)](alter-index-transact-sql.md), and [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md) aren't supported for indexes on memory-optimized tables.

#### ADD

Specifies that one or more column definitions, computed column definitions, or table constraints are added. Or, the columns that the system uses for system versioning are added. For memory-optimized tables, you can add an index.

> [!NOTE]  
> New columns are added after all existing columns in the table being altered.

> [!IMPORTANT]  
> Without using an `ALTER TABLE` statement, the statements [CREATE INDEX (Transact-SQL)](create-index-transact-sql.md), [DROP INDEX (Transact-SQL)](drop-index-transact-sql.md), [ALTER INDEX (Transact-SQL)](alter-index-transact-sql.md), and [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md) aren't supported for indexes on memory-optimized tables.

#### PERIOD FOR SYSTEM_TIME ( system_start_time_column_name, system_end_time_column_name )

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies the names of the columns that the system uses to record the period of time for which a record is valid. You can specify existing columns or create new columns as part of the `ADD PERIOD FOR SYSTEM_TIME` argument. Set up the columns with the data type of **datetime2** and define them as `NOT NULL`. If you define a period column as `NULL`, an error results. You can define a [column_constraint](alter-table-column-constraint-transact-sql.md) and/or [Specify default values for columns](../../relational-databases/tables/specify-default-values-for-columns.md) for the system_start_time and system_end_time columns. See Example A in the following [System Versioning](#system_versioning) examples that demonstrates using a default value for the system_end_time column.

Use this argument with the `SET SYSTEM_VERSIONING` argument to make an existing table a temporal table. For more information, see [Temporal tables](../../relational-databases/tables/temporal-tables.md).

As of [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], users can mark one or both period columns with `HIDDEN` flag to implicitly hide these columns such that `SELECT * FROM <table_name>` doesn't return a value for the columns. By default, period columns aren't hidden. In order to be used, hidden columns must be explicitly included in all queries that directly reference the temporal table.

#### DROP

Specifies that one or more column definitions, computed column definitions, or table constraints are dropped, or to drop the specification for the columns that the system uses for system versioning.

> [!NOTE]  
> Columns dropped in ledger tables are only soft deleted. A dropped column remains in the ledger table, but it's marked as a dropped column by setting the `dropped_ledger_table` column in `sys.tables` to `1`. The ledger view of the dropped ledger table is also marked as dropped by setting the `dropped_ledger_view` column in `sys.tables` to `1`. A dropped ledger table, its history table, and its ledger view are renamed by adding a prefix (`MSSQL_DroppedLedgerTable`, `MSSQL_DroppedLedgerHistory`, `MSSQL_DroppedLedgerView`), and appending a GUID to the original name.

#### CONSTRAINT *constraint_name*

Specifies that *constraint_name* is removed from the table. Multiple constraints can be listed.

You can determine the user-defined or system-supplied name of the constraint by querying the `sys.check_constraint`, `sys.default_constraints`, `sys.key_constraints`, and `sys.foreign_keys` catalog views.

A `PRIMARY KEY` constraint can't be dropped if an XML index exists on the table.

#### INDEX *index_name*

Specifies that *index_name* is removed from the table.

The syntax `ALTER TABLE` ... `ADD`/`DROP`/`ALTER INDEX` is supported only for memory-optimized tables.

> [!IMPORTANT]  
> Without using an `ALTER TABLE` statement, the statements [CREATE INDEX (Transact-SQL)](create-index-transact-sql.md), [DROP INDEX (Transact-SQL)](drop-index-transact-sql.md), [ALTER INDEX (Transact-SQL)](alter-index-transact-sql.md), and [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md) aren't supported for indexes on memory-optimized tables.

#### COLUMN *column_name*

Specifies that *constraint_name* or *column_name* is removed from the table. Multiple columns can be listed.

A column can't be dropped when it's:

- Used in an index, whether as a key column or as an `INCLUDE`
- Used in a `CHECK`, `FOREIGN KEY`, `UNIQUE`, or `PRIMARY KEY` constraint.
- Associated with a default that's defined with the `DEFAULT` keyword, or bound to a default object.
- Bound to a rule.

> [!NOTE]  
> Dropping a column doesn't reclaim the disk space of the column. You might have to reclaim the disk space of a dropped column when the row size of a table is near, or has exceeded, its limit. Reclaim space by creating a clustered index on the table or rebuilding an existing clustered index by using [ALTER INDEX (Transact-SQL)](alter-index-transact-sql.md). For information about the impact of dropping LOB data types, see this [CSS blog entry](/archive/blogs/psssql/how-it-works-gotcha-varcharmax-caused-my-queries-to-be-slower).

#### PERIOD FOR SYSTEM_TIME

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Drops the specification for the columns that the system uses for system versioning.

#### WITH \<drop_clustered_constraint_option>

Specifies that one or more drop clustered constraint options are set.

#### MAXDOP = *max_degree_of_parallelism*

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Overrides the **max degree of parallelism** configuration option only for the duration of the operation. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

Use the `MAXDOP` option to limit the number of processors used in parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* can be one of the following values:

- `1`

  Suppresses parallel plan generation.

- `>1`

  Restricts the maximum number of processors used in a parallel index operation to the specified number.

- `0` (default)

  Uses the actual number of processors or fewer based on the current system workload.

For more information, see [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

#### ONLINE = { ON | OFF } \<as applies to drop_clustered_constraint_option>

Specifies whether underlying tables and associated indexes are available for queries and data modification during the index operation. The default is `OFF`. You can run `REBUILD` as an `ONLINE` operation.

- ON

  Long-term table locks aren't held for the duration of the index operation. During the main phase of the index operation, only an Intent Share (`IS`) lock is held on the source table. This behavior enables queries or updates to the underlying table and indexes to continue. At the start of the operation, a Shared (S) lock is held on the source object for a short time. At the end of the operation, for a short time, an S (Shared) lock is acquired on the source if a nonclustered index is being created. Or, a Sch-M (Schema Modification) lock is acquired when a clustered index is created or dropped online and when a clustered or nonclustered index is being rebuilt. `ONLINE` can't be set to `ON` when an index is being created on a local temporary table. Only single-threaded heap rebuild operation is allowed.

  To run the DDL for `SWITCH` or online index rebuild, all active blocking transactions running on a particular table must be completed. When executing, the `SWITCH` or rebuild operation prevents new transactions from starting and might significantly affect the workload throughput and temporarily delay access to the underlying table.

- OFF

  Table locks apply for the duration of the index operation. An offline index operation that creates, rebuilds, or drops a clustered index, or rebuilds or drops a nonclustered index, acquires a Schema modification (Sch-M) lock on the table. This lock prevents all user access to the underlying table for the duration of the operation. An offline index operation that creates a nonclustered index acquires a Shared (S) lock on the table. This lock prevents updates to the underlying table but allows read operations, such as `SELECT` statements. Multi-threaded heap rebuild operations are allowed.

  For more information, see [How online index operations work](../../relational-databases/indexes/how-online-index-operations-work.md).

  > [!NOTE]  
  > Online index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

#### MOVE TO { *partition_scheme_name*(*column_name* [ ,...*n* ] ) | *filegroup* | "default" }

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies a location to move the data rows currently in the leaf level of the clustered index. The table is moved to the new location. This option applies only to constraints that create a clustered index.

> [!NOTE]  
> In this context, `default` isn't a keyword. It's an identifier for the default filegroup and must be delimited, as in `MOVE TO "default"` or `MOVE TO [default]`. If `"default"` is specified, the `QUOTED_IDENTIFIER` option must be `ON` for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](set-quoted-identifier-transact-sql.md).

#### { CHECK | NOCHECK } CONSTRAINT

Specifies that *constraint_name* is enabled or disabled. This option can only be used with `FOREIGN KEY` and `CHECK` constraints. When `NOCHECK` is specified, the constraint is disabled and future inserts or updates to the column aren't validated against the constraint conditions. `DEFAULT`, `PRIMARY KEY`, and `UNIQUE` constraints can't be disabled.

- ALL

  Specifies that all constraints are either disabled with the `NOCHECK` option or enabled with the `CHECK` option.

#### { ENABLE | DISABLE } TRIGGER

Specifies that *trigger_name* is enabled or disabled. When a trigger is disabled, it's still defined for the table. However, when `INSERT`, `UPDATE`, or `DELETE` statements run against the table, the actions in the trigger aren't carried out until the trigger is re-enabled.

- ALL

  Specifies that all triggers in the table are enabled or disabled.

- *trigger_name*

  Specifies the name of the trigger to disable or enable.

#### { ENABLE | DISABLE } CHANGE_TRACKING

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies whether change tracking is enabled disabled for the table. By default, change tracking is disabled.

This option is available only when change tracking is enabled for the database. For more information, see [ALTER DATABASE SET options (Transact-SQL)](alter-database-transact-sql-set-options.md).

To enable change tracking, the table must have a primary key.

#### WITH ( TRACK_COLUMNS_UPDATED = { ON | OFF } )

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies whether the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tracks, which change tracked columns were updated. The default value is `OFF`.

#### SWITCH [ PARTITION *source_partition_number_expression* ] TO [ *schema_name*. ] *target_table* [ PARTITION *target_partition_number_expression* ]

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Switches a block of data in one of the following ways:

- Reassigns all data of a table as a partition to an already-existing partitioned table.
- Switches a partition from one partitioned table to another.
- Reassigns all data in one partition of a partitioned table to an existing non-partitioned table.

If *table* is a partitioned table, you must specify *source_partition_number_expression*. If *target_table* is partitioned, you must specify *target_partition_number_expression*. When reassigning a table's data as a partition to an already-existing partitioned table, or switching a partition from one partitioned table to another, the target partition must exist and it must be empty.

When reassigning one partition's data to form a single table, the target table must already exist and it must be empty. Both the source table or partition, and the target table or partition, must be located in the same filegroup. The corresponding indexes, or index partitions, must also be located in the same filegroup. Many additional restrictions apply to switching partitions. *table* and *target_table* can't be the same. *target_table* can be a multi-part identifier.

Both *source_partition_number_expression* and *target_partition_number_expression* are constant expressions that can reference variables and functions. These include user-defined type variables and user-defined functions. They can't reference [!INCLUDE [tsql](../../includes/tsql-md.md)] expressions.

A partitioned table with a clustered columnstore index behaves like a partitioned heap:

- The primary key must include the partition key.
- A unique index must include the partition key. But, including the partition key with an existing unique index can change the uniqueness.
- To switch partitions, all nonclustered indexes must include the partition key.

For `SWITCH` restriction when using replication, see [Replicate Partitioned Tables and Indexes](../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).

Nonclustered columnstore indexes were built in a read-only format before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and for SQL Database before version V12. You must rebuild nonclustered columnstore indexes to the current format (which is updatable) before any `PARTITION` operations can be run.

**Limitations**

If both tables are partitioned identically, including nonclustered indexes, and the target table doesn't have any nonclustered indexes, you might receive a [4907 Error](../../relational-databases/errors-events/database-engine-events-and-errors-4000-to-4999.md).

Example output:

```output
Msg 4907, Level 16, State 1, Line 38
'ALTER TABLE SWITCH' statement failed. The table 'MyDB.dbo.PrtTable1' has 4 partitions while index 'MS1' has 6 partitions.
```

#### SET ( FILESTREAM_ON = { *partition_scheme_name* | *filestream_filegroup_name* | "default" | "NULL" })

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] doesn't support `FILESTREAM`.

Specifies where FILESTREAM data is stored.

`ALTER TABLE` with the `SET FILESTREAM_ON` clause succeeds only if the table has no FILESTREAM columns. You can add FILESTREAM columns by using a second `ALTER TABLE` statement.

If you specify *partition_scheme_name*, the rules for [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md) apply. Be sure the table is already partitioned for row data, and its partition scheme uses the same partition function and columns as the FILESTREAM partition scheme.

*filestream_filegroup_name* specifies the name of a FILESTREAM filegroup. The filegroup must have one file that's defined for the filegroup by using a [CREATE DATABASE](create-database-transact-sql.md) or [ALTER DATABASE (Transact-SQL)](alter-database-transact-sql.md) statement, or you get an error.

`"default"` specifies the FILESTREAM filegroup with the `DEFAULT` property set. If there's no FILESTREAM filegroup, you get an error.

`"NULL"` specifies that all references to FILESTREAM filegroups for the table are removed. All FILESTREAM columns must be dropped first. Use `SET FILESTREAM_ON = "NULL"` to delete all FILESTREAM data that's associated with a table.

#### SET ( SYSTEM_VERSIONING = { OFF | ON [ ( HISTORY_TABLE = schema_name . history_table_name [ , DATA_CONSISTENCY_CHECK = { ON | OFF } ] ) ] } )

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Either disables or enables system versioning of a table. To enable system versioning of a table, the system verifies that the data type, nullability constraint, and primary key constraint requirements for system versioning are met. The system records the history of each record in the system-versioned table in a separate history table. If the `HISTORY_TABLE` argument isn't used, the name of this history table is `MSSQL_TemporalHistoryFor<primary_table_object_id>`. If the history table doesn't exists, the system generates a new history table matching the schema of the current table, creates a link between the two tables, and enables the system to record the history of each record in the current table in the history table. If you use the HISTORY_TABLE argument to create a link to and use an existing history table, the system creates a link between the current table and the specified table. When creating a link to an existing history table, you can choose to do a data consistency check. This data consistency check ensures that existing records don't overlap. Running the data consistency check is the default. Use the `SYSTEM_VERSIONING = ON` argument on a table that is defined with the `PERIOD FOR SYSTEM_TIME` clause to make the existing table a temporal table. For more information, see [Temporal tables](../../relational-databases/tables/temporal-tables.md).

#### HISTORY_RETENTION_PERIOD = { INFINITE | number { DAY | DAYS | WEEK | WEEKS | MONTH | MONTHS | YEAR | YEARS } }

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies finite or infinite retention for historical data in a temporal table. If omitted, infinite retention is assumed.

#### DATA_DELETION

**Applies to:** Azure SQL Edge *only*

Enables retention policy based cleanup of old or aged data from tables within a database. For more information, see [Enable and Disable Data Retention](/azure/azure-sql-edge/data-retention-enable-disable). The following parameters must be specified for data retention to be enabled.

- FILTER_COLUMN = { column_name }

  Specifies the column that should be used to determine if the rows in the table are obsolete or not. The following data types are allowed for the filter column.

  - **date**
  - **datetime**
  - **datetime2**
  - **smalldatetime**
  - **datetimeoffset**

- RETENTION_PERIOD = { INFINITE | number { DAY | DAYS | WEEK | WEEKS | MONTH | MONTHS | YEAR | YEARS } }

  Specifies the retention period policy for the table. The retention period is specified as a combination of a positive integer value and the date part unit.

#### SET ( LOCK_ESCALATION = { AUTO | TABLE | DISABLE } )

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies the allowed methods of lock escalation for a table.

- AUTO

  This option allows [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] to select the lock escalation granularity that's appropriate for the table schema.

  - If the table is partitioned, lock escalation is allowed to the heap or B-tree (HoBT) granularity. In other words, escalation is allowed to the partition level. After the lock is escalated to the HoBT level, the lock won't be escalated later to `TABLE` granularity.

  - If the table isn't partitioned, the lock escalation is done to the `TABLE` granularity.

- TABLE

  Lock escalation is done at table-level granularity whether the table is partitioned or not partitioned. `TABLE` is the default value.

- DISABLE

  Prevents lock escalation in most cases. Table-level locks aren't completely disallowed. For example, when you're scanning a table that has no clustered index under the serializable isolation level, [!INCLUDE [ssDE](../../includes/ssde-md.md)] must take a table lock to protect data integrity.

#### REBUILD

Use the `REBUILD WITH` syntax to rebuild an entire table including all the partitions in a partitioned table. If the table has a clustered index, the `REBUILD` option rebuilds the clustered index. `REBUILD` can be run as an `ONLINE` operation.

Use the `REBUILD PARTITION` syntax to rebuild a single partition in a partitioned table.

#### PARTITION = ALL

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Rebuilds all partitions when changing the partition compression settings.

#### REBUILD WITH ( \<rebuild_option> )

All options apply to a table with a clustered index. If the table doesn't have a clustered index, the heap structure is only affected by some of the options.

When a specific compression setting isn't specified with the `REBUILD` operation, the current compression setting for the partition is used. To return the current setting, query the `data_compression` column in the `sys.partitions` catalog view.

For complete descriptions of the rebuild options, see [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md).

#### DATA_COMPRESSION

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Specifies the data compression option for the specified table, partition number, or range of partitions. The options are as follows:

- NONE

  Table or specified partitions aren't compressed. This option doesn't apply to columnstore tables.

- ROW

  Table or specified partitions are compressed by using row compression. This option doesn't apply to columnstore tables.

- PAGE

  Table or specified partitions are compressed by using page compression. This option doesn't apply to columnstore tables.

- COLUMNSTORE

  **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

  Applies only to columnstore tables. `COLUMNSTORE` specifies to decompress a partition that was compressed with the `COLUMNSTORE_ARCHIVE` option. When the data is restored, it continues to be compressed with the columnstore compression that's used for all columnstore tables.

- COLUMNSTORE_ARCHIVE

  **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

  Applies only to columnstore tables, which are tables stored with a clustered columnstore index. `COLUMNSTORE_ARCHIVE` further compresses the specified partition to a smaller size. Use this option for archival or other situations that require less storage and can afford more time for storage and retrieval.

  To rebuild multiple partitions at the same time, see [index_option](alter-table-index-option-transact-sql.md). If the table doesn't have a clustered index, changing the data compression rebuilds the heap and the nonclustered indexes. For more information about compression, see [Data compression](../../relational-databases/data-compression/data-compression.md).

  `ALTER TABLE REBUILD PARTITION WITH DATA COMPRESSION = ROW` or `PAGE` isn't allowed on [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

#### XML_COMPRESSION

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

Specifies the XML compression option for any **xml** data type columns in the table. The options are as follows:

- ON

  Columns using the **xml** data type are compressed.

- OFF

  Columns using the **xml** data type aren't compressed.

#### ONLINE = { ON | OFF } \<as applies to single_partition_rebuild_option>

Specifies whether a single partition of the underlying tables and associated indexes is available for queries and data modification during the index operation. The default is `OFF`. You can run `REBUILD` as an `ONLINE` operation.

- ON

  Long-term table locks aren't held for the duration of the index operation. S-lock on the table is required in the beginning of the index rebuild and a Sch-M lock on the table at the end of the online index rebuild. Although both locks are short metadata locks, the Sch-M lock must wait for all blocking transactions to be completed. During the wait time, the Sch-M lock blocks all other transactions that wait behind this lock when accessing the same table.

  > [!NOTE]  
  > Online index rebuild can set the `low_priority_lock_wait` options described later in this section.

- OFF

  Table locks are applied for the duration of the index operation. This prevents all user access to the underlying table for the duration of the operation.

#### *column_set_name* XML COLUMN_SET FOR ALL_SPARSE_COLUMNS

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

The name of the column set. A column set is an untyped XML representation that combines all of the sparse columns of a table into a structured output. A column set can't be added to a table that contains sparse columns. For more information about column sets, see [Use column sets](../../relational-databases/tables/use-column-sets.md).

#### { ENABLE | DISABLE } FILETABLE_NAMESPACE

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Enables or disables the system-defined constraints on a FileTable. Can only be used with a FileTable.

#### SET ( FILETABLE_DIRECTORY = *directory_name* )

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] doesn't support FileTable.

Specifies the Windows-compatible FileTable directory name. This name should be unique among all the FileTable directory names in the database. Uniqueness comparison is case-insensitive, despite the SQL collation settings. Can only be used with a FileTable.

#### REMOTE_DATA_ARCHIVE

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions.

Enables or disables Stretch Database for a table. For more information, see [Stretch Database](/previous-versions/sql/sql-server/stretch-database/stretch-database).

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

**Enable Stretch Database for a table**

When you enable Stretch for a table by specifying `ON`, you also have to specify `MIGRATION_STATE = OUTBOUND` to begin migrating data immediately, or `MIGRATION_STATE = PAUSED` to postpone data migration. The default value is `MIGRATION_STATE = OUTBOUND`. For more information about enabling Stretch for a table, see [Enable Stretch Database for a table](/previous-versions/sql/sql-server/stretch-database/enable-stretch-database-for-a-table).

**Prerequisites**. Before you enable Stretch for a table, you have to enable Stretch on the server and on the database. For more information, see [Enable Stretch Database for a database](/previous-versions/sql/sql-server/stretch-database/enable-stretch-database-for-a-database).

**Permissions**. Enabling Stretch for a database or a table requires **db_owner** permissions. Enabling Stretch for a table also requires `ALTER` permissions on the table.

**Disable Stretch Database for a table**

When you disable Stretch for a table, you have two options for the remote data that's already been migrated to Azure. For more information, see [Disable Stretch Database and bring back remote data](/previous-versions/sql/sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data).

- To disable Stretch for a table and copy the remote data for the table from Azure back to SQL Server, run the following command. This command can't be canceled.

  ```sql
  ALTER TABLE <table_name>
     SET ( REMOTE_DATA_ARCHIVE ( MIGRATION_STATE = INBOUND ) ) ;
  ```

This operation incurs data transfer costs, and it can't be canceled. For more information, see [Data Transfers Pricing Details](https://azure.microsoft.com/pricing/details/data-transfers/).

After all the remote data has been copied from Azure back to SQL Server, Stretch is disabled for the table.

- To disable Stretch for a table and abandon the remote data, run the following command.

  ```sql
  ALTER TABLE <table_name>
     SET ( REMOTE_DATA_ARCHIVE = OFF_WITHOUT_DATA_RECOVERY ( MIGRATION_STATE = PAUSED ) ) ;
  ```

After you disable Stretch Database for a table, data migration stops and query results no longer include results from the remote table.

Disabling Stretch doesn't remove the remote table. If you want to delete the remote table, you drop it by using the Azure portal.

#### [ FILTER_PREDICATE = { null | *predicate* } ]

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions.

Optionally specifies a filter predicate to select rows to migrate from a table that contains both historical and current data. The predicate must call a deterministic inline table-valued function. For more information, see [Enable Stretch Database for a table](/previous-versions/sql/sql-server/stretch-database/enable-stretch-database-for-a-table) and [Select rows to migrate by using a filter function - Stretch Database](/previous-versions/sql/sql-server/stretch-database/select-rows-to-migrate-by-using-a-filter-function-stretch-database).

> [!IMPORTANT]  
> If you provide a filter predicate that performs poorly, data migration also performs poorly. Stretch Database applies the filter predicate to the table by using the `CROSS APPLY` operator.

If you don't specify a filter predicate, the entire table is migrated.

When you specify a filter predicate, you also have to specify `MIGRATION_STATE`.

#### MIGRATION_STATE = { OUTBOUND | INBOUND | PAUSED }

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions.

- Specify `OUTBOUND` to migrate data from SQL Server to Azure.
- Specify `INBOUND` to copy the remote data for the table from Azure back to SQL Server and to disable Stretch for the table. For more information, see [Disable Stretch Database and bring back remote data](/previous-versions/sql/sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data).

  This operation incurs data transfer costs, and it can't be canceled.

- Specify `PAUSED` to pause or postpone data migration. For more information, see [Pause and resume data migration - Stretch Database](/previous-versions/sql/sql-server/stretch-database/pause-and-resume-data-migration-stretch-database).

#### WAIT_AT_LOW_PRIORITY

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

An online index rebuild has to wait for blocking operations on this table. `WAIT_AT_LOW_PRIORITY` indicates that the online index rebuild operation waits for low-priority locks, allowing other operations to carry on while the online index build operation is waiting. Omitting the `WAIT AT LOW PRIORITY` option is the same as `WAIT_AT_LOW_PRIORITY ( MAX_DURATION = 0 minutes, ABORT_AFTER_WAIT = NONE)`.

#### MAX_DURATION = *time* [ MINUTES ]

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

The wait time, which is an integer value specified in minutes, that the `SWITCH` or online index rebuild locks wait with low priority when running the DDL command. If the operation is blocked for the `MAX_DURATION` time, one of the `ABORT_AFTER_WAIT` actions runs. `MAX_DURATION` time is always in minutes, and you can omit the word `MINUTES`.

#### ABORT_AFTER_WAIT = { NONE | SELF | BLOCKERS }

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

- NONE

  Continue waiting for the lock with normal (regular) priority.

- SELF

  Exit the `SWITCH` or online index rebuild DDL operation currently being run without taking any action.

- BLOCKERS

  Kill all user transactions that currently block the `SWITCH` or online index rebuild DDL operation so that the operation can continue.

  Requires `ALTER ANY CONNECTION` permission.

#### IF EXISTS

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Conditionally drops the column or constraint only if it already exists.

<a id="resumable"></a>

#### RESUMABLE = { ON | OFF}

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

Specifies whether an `ALTER TABLE ADD CONSTRAINT` operation is resumable. Add table constraint operation is resumable when `ON`. Add table constraint operation isn't resumable when `OFF`. Default is `OFF`. The `RESUMABLE` option can be used as part of the [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md) in the [ALTER TABLE table_constraint (Transact-SQL)](alter-table-table-constraint-transact-sql.md).

`MAX_DURATION` when used with `RESUMABLE = ON` (requires `ONLINE = ON`) indicates time (an integer value specified in minutes) that a resumable online add constraint operation is executed before being paused. If not specified, the operation continues until completion.

For more information on enabling and using resumable `ALTER TABLE ADD CONSTRAINT` operations, see [Resumable add table constraints](../../relational-databases/security/resumable-add-table-constraints.md).

## Remarks

To add new rows of data, use [INSERT (Transact-SQL)](insert-transact-sql.md). To remove rows of data, use [DELETE (Transact-SQL)](delete-transact-sql.md) or [TRUNCATE TABLE (Transact-SQL)](truncate-table-transact-sql.md). To change the values in existing rows, use [UPDATE (Transact-SQL)](../queries/update-transact-sql.md).

If there are any execution plans in the procedure cache that reference the table, `ALTER TABLE` marks them to be recompiled on their next execution.

::: moniker range="=fabric-sqldb"

Currently, in-memory, ledger, ledger history, and Always Encrypted tables cannot be created in [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. For more information, see [Limitations in SQL database in Microsoft Fabric](/fabric/database/sql/limitations).

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], some table features can be created but aren't [mirrored into the Fabric OneLake](/fabric/database/sql/mirroring-overview). For more information, see [Limitations for Fabric SQL database mirroring](/fabric/database/sql/mirroring-limitations).

::: moniker-end

::: moniker range="=fabric"

In Fabric Data Warehouse, supported `ALTER TABLE` Transact-SQL operations can execute inside an explicit user-defined transaction. For more information, see [Transactions in Fabric Data Warehouse](/fabric/data-warehouse/transactions#explicit-transactions).

In Fabric Data Warehouse, you can alter distributed #temp tables with `ALTER TABLE`, but not MDF-backed temp tables. For more information, see [#temp tables in Fabric Data Warehouse](/fabric/data-warehouse/tables#temp-tables).

::: moniker-end

## Change the size of a column

You can change the length, precision, or scale of a column by specifying a new size for the column data type. Use the `ALTER COLUMN` clause. If data exists in the column, the new size can't be smaller than the maximum size of the data. Also, you can't define the column in an index, unless the column is a **varchar**, **nvarchar**, or **varbinary** data type and the index isn't the result of a `PRIMARY KEY` constraint. See the example in the short section titled [Altering a Column Definition](#alter_column).

## Locks and ALTER TABLE

Changes you specify in `ALTER TABLE` take effect immediately. If the changes require modifications to the rows in the table, `ALTER TABLE` updates the rows. `ALTER TABLE` acquires a schema modify (Sch-M) lock on the table to ensure that no other connections reference even the metadata for the table during the change, except online index operations that require a short Sch-M lock at the end. In an `ALTER TABLE...SWITCH` operation, the lock is acquired on both the source and target tables. The modifications made to the table are logged and fully recoverable. Changes that affect all the rows in large tables, such as dropping a column or, on some editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], adding a `NOT NULL` column with a default value, can take a long time to complete and generate many log records. Run these `ALTER TABLE` statements with the same care as any `INSERT`, `UPDATE`, or `DELETE` statement that affects many rows.

::: moniker range=">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"

### Extended Events (XEvents) for partition switch

The following XEvents are related to `ALTER TABLE ... SWITCH PARTITION` and [online index rebuilds](alter-index-transact-sql.md#online---on--off-).

- lock_request_priority_state
- process_killed_by_abort_blockers
- ddl_with_wait_at_low_priority

<a id="adding-not-null-columns-as-an-online-operation"></a>

### Add NOT NULL columns as an online operation

In [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] Enterprise edition and later versions, adding a `NOT NULL` column with a default value is an online operation when the default value is a *runtime constant*. This default behavior means that the operation finishes almost instantaneously despite the number of rows in the table, because the existing rows in the table aren't updated during the operation. Instead, the default value is stored only in the metadata of the table and the value is looked up, as needed, in queries that access these rows. This behavior is automatic. No additional syntax is required to implement the online operation beyond the `ADD COLUMN` syntax. A runtime constant is an expression that produces the same value at runtime for each row in the table despite its determinism. For example, the constant expression `"My temporary data"`, or the system function `GETUTCDATETIME()` are runtime constants. In contrast, the functions `NEWID()` or `NEWSEQUENTIALID()` aren't runtime constants, because a unique value is produced for each row in the table. Adding a `NOT NULL` column with a default value that's not a runtime constant is always run offline and an exclusive (Sch-M) lock is acquired for the duration of the operation.

While the existing rows reference the value stored in metadata, the default value is stored on the row for any new rows that are inserted and don't specify another value for the column. The default value stored in metadata moves to an existing row when the row is updated (even if the actual column isn't specified in the `UPDATE` statement), or if the table or clustered index is rebuilt.

You can't add columns of type **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **text**, **ntext**, **image**, **hierarchyid**, **geometry**, **geography**, or CLR user-defined types in an online operation. You can't add a column online if doing so causes the maximum possible row size to exceed the 8,060-byte limit. The column is added as an offline operation in this case.

## Parallel plan execution

In [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] Enterprise edition and later versions, the `max degree of parallelism` configuration option and the current workload determine the number of processors that run a single `ALTER TABLE ADD` (index-based) `CONSTRAINT` or `DROP` (clustered index) `CONSTRAINT` statement. If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] detects that the system is busy, it automatically reduces the degree of parallelism of the operation before statement execution starts. You can manually configure the number of processors that run the statement by specifying the `MAXDOP` option. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

## Partitioned tables

In addition to performing `SWITCH` operations that involve partitioned tables, use `ALTER TABLE` to change the state of the columns, constraints, and triggers of a partitioned table, just like you use it for nonpartitioned tables. However, you can't use this statement to change the way the table itself is partitioned. To repartition a partitioned table, use [ALTER PARTITION SCHEME (Transact-SQL)](alter-partition-scheme-transact-sql.md) and [ALTER PARTITION FUNCTION (Transact-SQL)](alter-partition-function-transact-sql.md). Additionally, you can't change the data type of a column of a partitioned table.

## Restrictions on tables with schema-bound views

The restrictions that apply to `ALTER TABLE` statements on tables with schema-bound views are the same as the restrictions currently applied when modifying tables with a simple index. You can add a column. However, you can't remove or change a column that participates in any schema-bound view. If the `ALTER TABLE` statement requires changing a column used in a schema-bound view, `ALTER TABLE` fails and the [!INCLUDE [ssDE](../../includes/ssde-md.md)] raises an error message. For more information about schema binding and indexed views, see [CREATE VIEW (Transact-SQL)](create-view-transact-sql.md).

Adding or removing triggers on base tables isn't affected by creating a schema-bound view that references the tables.

## Indexes and ALTER TABLE

Indexes created as part of a constraint are dropped when the constraint is dropped. To drop indexes that you created by using `CREATE INDEX`, use `DROP INDEX`. Use the `ALTER INDEX` statement to rebuild an index that's part of a constraint definition. You don't need to drop and add the constraint again by using `ALTER TABLE`.

You must remove all indexes and constraints that are based on a column before you can remove that column.

When you delete a constraint that created a clustered index, the data rows that were stored in the leaf level of the clustered index are stored in a nonclustered table. You can drop the clustered index and move the resulting table to another filegroup or partition scheme in a single transaction by specifying the `MOVE TO` option. The `MOVE TO` option has the following restrictions:

- `MOVE TO` isn't valid for indexed views or nonclustered indexes.

- The partition scheme or filegroup must already exist.

- If you don't specify `MOVE TO`, the table is located in the same partition scheme or filegroup as was defined for the clustered index.

When you drop a clustered index, specify the `ONLINE = ON` option so the `DROP INDEX` transaction doesn't block queries and modifications to the underlying data and associated nonclustered indexes.

`ONLINE = ON` has the following restrictions:

- `ONLINE = ON` isn't valid for clustered indexes that are also disabled. You must drop disabled indexes by using `ONLINE = OFF`.
- You can only drop one index at a time.
- `ONLINE = ON` isn't valid for indexed views, nonclustered indexes, or indexes on local temp tables.
- `ONLINE = ON` isn't valid for columnstore indexes.

Dropping a clustered index requires temporary disk space that's equal to the size of the existing clustered index. This operation releases the additional space as soon as it completes.

> [!NOTE]  
> The options listed under `<drop_clustered_constraint_option>` apply to clustered indexes on tables. You can't apply these options to clustered indexes on views or nonclustered indexes.

## Replicate schema changes

When you run `ALTER TABLE` on a published table at a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher, the change propagates to all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers by default. This functionality has some restrictions. You can disable it. For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).

## Data compression

You can't enable compression for system tables. If the table is a heap, the rebuild operation for `ONLINE` mode is single-threaded. Use `OFFLINE` mode for a multithreaded heap rebuild operation. For more information about data compression, see [Data compression](../../relational-databases/data-compression/data-compression.md).

To evaluate how changing the compression state affects a table, an index, or a partition, use the [sp_estimate_data_compression_savings](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md) system stored procedure.

The following restrictions apply to partitioned tables:

- You can't change the compression setting of a single partition if the table has nonaligned indexes.
- The `ALTER TABLE <table> REBUILD PARTITION` ... syntax rebuilds the specified partition.
- The `ALTER TABLE <table> REBUILD WITH` ... syntax rebuilds all partitions.

## Drop ntext columns

When you drop columns that use the deprecated **[ntext](../data-types/ntext-text-and-image-transact-sql.md)** data type, the cleanup of the deleted data occurs as a serialized operation on all rows. The cleanup can require a large amount of time. When you drop an **ntext** column in a table with lots of rows, update the **ntext** column to `NULL` value first, and then drop the column. You can run this option with parallel operations and make it much faster.

## Online index rebuild

To run the DDL statement for an online index rebuild, all active blocking transactions running on a particular table must complete. When the online index rebuild launches, it blocks all new transactions that are ready to start running on this table. Although the duration of the lock for online index rebuild is short, waiting for all open transactions on a given table to complete and blocking the new transactions to start might significantly affect the throughput. This lock wait can cause a workload slow-down or timeout and significantly limit access to the underlying table. The `WAIT_AT_LOW_PRIORITY` option allows DBAs to manage the S-lock and Sch-M locks required for online index rebuilds. In all three cases: `NONE`, `SELF`, and `BLOCKERS`, if during the wait time (`(MAX_DURATION = n [minutes])`) there are no blocking activities, the online index rebuild runs immediately without waiting and the DDL statement is completed.

## Compatibility support

The `ALTER TABLE` statement supports only two-part (`schema.object`) table names. In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], specifying a table name by using the following formats fails at compile time with error 117.

- `server.database.schema.table`
- `.database.schema.table`
- `..schema.table`

In earlier versions, specifying the format `server.database.schema.table` returned error 4902. Specifying the format `.database.schema.table` or the format `..schema.table` succeeded.

To resolve the problem, remove the use of a four-part prefix.

::: moniker-end

## Permissions

Requires `ALTER` permission on the table.

`ALTER TABLE` permissions apply to both tables involved in an `ALTER TABLE SWITCH` statement. Any data that's switched inherits the security of the target table.

If you define any columns in the `ALTER TABLE` statement to be of a common language runtime (CLR) user-defined type or alias data type, `REFERENCES` permission on the type is required.

Adding or altering a column that updates the rows of the table requires `UPDATE` permission on the table. For example, adding a `NOT NULL` column with a default value or adding an identity column when the table isn't empty.

<a id="Example_Top"></a>

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

| Category | Featured syntax elements |
| --- | --- |
| [Adding columns and constraints](#add) | `ADD`; `PRIMARY KEY` with index options, sparse columns, and column sets |
| [Dropping columns and constraints](#Drop) | `DROP` |
| [Altering a column definition](#alter_column) | Change data type; change column size; collation |
| [Altering a table definition](#alter_table) | `DATA_COMPRESSION`; `SWITCH PARTITION`; `LOCK ESCALATION`; change tracking |
| [Disabling and enabling constraints and triggers](#disable_enable) | `CHECK`; `NO CHECK`; `ENABLE TRIGGER`; `DISABLE TRIGGER` |
| [Online operations](#online) | `ONLINE` |
| [System versioning](#system_versioning) | `SYSTEM_VERSIONING` |

<a id="add"></a>

### Add columns and constraints

Examples in this section demonstrate adding columns and constraints to a table.

#### A. Add a new column

The following example adds a column that allows null values and doesn't include a `DEFAULT` definition. In the new column, each row has `NULL`.

```sql
CREATE TABLE dbo.doc_exa (column_a INT);
GO

ALTER TABLE dbo.doc_exa
    ADD column_b VARCHAR (20) NULL;
GO
```

#### B. Add a column with a constraint

The following example adds a new column with a `UNIQUE` constraint.

```sql
CREATE TABLE dbo.doc_exc (column_a INT);
GO

ALTER TABLE dbo.doc_exc
    ADD column_b VARCHAR (20) NULL
        CONSTRAINT exb_unique UNIQUE;
GO

EXECUTE sp_help doc_exc;
GO

DROP TABLE dbo.doc_exc;
GO
```

#### C. Add an unverified CHECK constraint to an existing column

The following example adds a constraint to an existing column in the table. The column has a value that violates the constraint. Therefore, the example uses `WITH NOCHECK` to prevent the constraint from being validated against existing rows, and to allow the constraint to be added.

```sql
CREATE TABLE dbo.doc_exd (column_a INT);
GO

INSERT INTO dbo.doc_exd VALUES (-1);
GO

ALTER TABLE dbo.doc_exd WITH NOCHECK
    ADD CONSTRAINT exd_check CHECK (column_a > 1);
GO

EXECUTE sp_help doc_exd;
GO

DROP TABLE dbo.doc_exd;
GO
```

#### D. Add a DEFAULT constraint to an existing column

The following example creates a table with two columns and inserts a value into the first column, while the other column remains `NULL`. The example then adds a `DEFAULT` constraint to the second column. To verify that the default is applied, the example inserts another value into the first column and queries the table.

```sql
CREATE TABLE dbo.doc_exz
(
    column_a INT,
    column_b INT
);
GO

INSERT INTO dbo.doc_exz (column_a) VALUES (7);
GO

ALTER TABLE dbo.doc_exz
    ADD CONSTRAINT col_b_def
        DEFAULT 50 FOR column_b;
GO

INSERT INTO dbo.doc_exz (column_a) VALUES (10);
GO

SELECT * FROM dbo.doc_exz;
GO

DROP TABLE dbo.doc_exz;
GO
```

#### E. Add several columns with constraints

The following example adds several columns with constraints defined with the new column. The first new column has an `IDENTITY` property. Each row in the table has new incremental values in the identity column.

```sql
CREATE TABLE dbo.doc_exe
(
    column_a INT
        CONSTRAINT column_a_un UNIQUE
);
GO

ALTER TABLE dbo.doc_exe

    -- Add a PRIMARY KEY identity column.
    ADD column_b INT IDENTITY
        CONSTRAINT column_b_pk PRIMARY KEY,

    -- Add a column that references another column in the same table.
        column_c INT NULL
            CONSTRAINT column_c_fk FOREIGN KEY REFERENCES doc_exe (column_a),

    -- Add a column with a constraint to enforce that
    -- nonnull data is in a valid telephone number format.
        column_d VARCHAR (16) NULL
            CONSTRAINT column_d_chk CHECK (column_d LIKE '[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
                                       OR column_d LIKE '([0-9][0-9][0-9]) [0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'),

    -- Add a nonnull column with a default.
        column_e DECIMAL (3, 3)
            CONSTRAINT column_e_default DEFAULT .081;
GO

EXECUTE sp_help doc_exe;
GO

DROP TABLE dbo.doc_exe;
GO
```

#### F. Add a nullable column with default values

The following example adds a nullable column with a `DEFAULT` definition, and uses `WITH VALUES` to provide values for each existing row in the table. If you don't use `WITH VALUES`, each row has the value `NULL` in the new column.

```sql
CREATE TABLE dbo.doc_exf (column_a INT);
GO

INSERT INTO dbo.doc_exf VALUES (1);
GO

ALTER TABLE dbo.doc_exf
    ADD AddDate SMALLDATETIME
        CONSTRAINT AddDateDflt DEFAULT GETDATE() WITH VALUES NULL;
GO

DROP TABLE dbo.doc_exf;
GO
```

#### G. Create a PRIMARY KEY constraint with index or data compression options

The following example creates the `PRIMARY KEY` constraint `PK_TransactionHistoryArchive_TransactionID` and sets the options `FILLFACTOR`, `ONLINE`, and `PAD_INDEX`. The resulting clustered index has the same name as the constraint.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
USE AdventureWorks2022;
GO

ALTER TABLE Production.TransactionHistoryArchive WITH NOCHECK
    ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID
        PRIMARY KEY CLUSTERED (TransactionID) WITH (FILLFACTOR = 75, ONLINE = ON, PAD_INDEX = ON);
GO
```

This similar example applies page compression while applying the clustered primary key.

```sql
USE AdventureWorks2022;
GO

ALTER TABLE Production.TransactionHistoryArchive WITH NOCHECK
    ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID
        PRIMARY KEY CLUSTERED (TransactionID) WITH (DATA_COMPRESSION = PAGE);
GO
```

#### H. Add a sparse column

The following examples show adding and modifying sparse columns in table T1. The code to create table `T1` is as follows.

```sql
CREATE TABLE T1
(
    C1 INT PRIMARY KEY,
    C2 VARCHAR (50) SPARSE NULL,
    C3 INT SPARSE NULL,
    C4 INT
);
GO
```

To add an additional sparse column `C5`, execute the following statement.

```sql
ALTER TABLE T1
    ADD C5 CHAR (100) SPARSE NULL;
GO
```

To convert the `C4` non-sparse column to a sparse column, execute the following statement.

```sql
ALTER TABLE T1
    ALTER COLUMN C4 ADD SPARSE;
GO
```

To convert the `C4` sparse column to a nonsparse column, execute the following statement.

```sql
ALTER TABLE T1
    ALTER COLUMN C4 DROP SPARSE;
GO
```

#### I. Add a column set

The following examples show how to add a column to table `T2`. You can't add a column set to a table that already contains sparse columns. The following code creates table `T2`.

```sql
CREATE TABLE T2
(
    C1 INT PRIMARY KEY,
    C2 VARCHAR (50) NULL,
    C3 INT NULL,
    C4 INT
);
GO
```

The following three statements add a column set named `CS`, and then modify columns `C2` and `C3` to SPARSE.

```sql
ALTER TABLE T2
    ADD CS XML COLUMN_SET FOR ALL_SPARSE_COLUMNS;
GO

ALTER TABLE T2
    ALTER COLUMN C2 ADD SPARSE;
GO

ALTER TABLE T2
    ALTER COLUMN C3 ADD SPARSE;
GO
```

#### J. Add an encrypted column

The following statement adds an encrypted column named `PromotionCode`.

```sql
ALTER TABLE Customers
    ADD PromotionCode NVARCHAR (100)
        ENCRYPTED WITH (
            COLUMN_ENCRYPTION_KEY = MyCEK,
            ENCRYPTION_TYPE = RANDOMIZED,
            ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
        );
```

#### K. Add a primary key with resumable operation

Resumable `ALTER TABLE` operation for adding a primary key clustered on column (a) with `MAX_DURATION` of 240 minutes.

```sql
ALTER TABLE table1
ADD CONSTRAINT PK_Constrain PRIMARY KEY CLUSTERED (a)
WITH (ONLINE = ON, MAXDOP = 2, RESUMABLE = ON, MAX_DURATION = 240);
```

<a id="Drop"></a>

### Drop columns and constraints

The examples in this section demonstrate how to drop columns and constraints.

#### A. Drop a column or columns

The first example modifies a table to remove a column. The second example removes multiple columns.

```sql
CREATE TABLE dbo.doc_exb
(
    column_a INT,
    column_b VARCHAR (20) NULL,
    column_c DATETIME,
    column_d INT
);
GO

-- Remove a single column.
ALTER TABLE dbo.doc_exb DROP COLUMN column_b;
GO

-- Remove multiple columns.
ALTER TABLE dbo.doc_exb DROP COLUMN column_c, column_d;
```

#### B. Drop constraints and columns

The first example removes a `UNIQUE` constraint from a table. The second example removes two constraints and a single column.

```sql
CREATE TABLE dbo.doc_exc
(
    column_a INT NOT NULL
        CONSTRAINT my_constraint UNIQUE
);
GO

-- Example 1. Remove a single constraint.
ALTER TABLE dbo.doc_exc DROP my_constraint;
GO

DROP TABLE dbo.doc_exc;
GO

CREATE TABLE dbo.doc_exc
(
    column_a INT NOT NULL
        CONSTRAINT my_constraint UNIQUE,
    column_b INT NOT NULL
        CONSTRAINT my_pk_constraint PRIMARY KEY
);
GO

-- Example 2. Remove two constraints and one column
-- The keyword CONSTRAINT is optional. The keyword COLUMN is required.
ALTER TABLE dbo.doc_exc
    DROP CONSTRAINT my_constraint, my_pk_constraint, COLUMN column_b;
GO
```

#### C. Drop a PRIMARY KEY constraint in the ONLINE mode

The following example deletes a `PRIMARY KEY` constraint with the `ONLINE` option set to `ON`.

```sql
ALTER TABLE Production.TransactionHistoryArchive
    DROP CONSTRAINT PK_TransactionHistoryArchive_TransactionID
    WITH (ONLINE = ON);
GO
```

#### D. Add and drop a FOREIGN KEY constraint

The following example creates the table `ContactBackup`, and then alters the table. First, it adds a `FOREIGN KEY` constraint that references the table `Person.Person`. Then, it drops the `FOREIGN KEY` constraint.

```sql
CREATE TABLE Person.ContactBackup (ContactID INT);
GO

ALTER TABLE Person.ContactBackup
    ADD CONSTRAINT FK_ContactBackup_Contact
        FOREIGN KEY (ContactID) REFERENCES Person.Person (BusinessEntityID);
GO

ALTER TABLE Person.ContactBackup
    DROP CONSTRAINT FK_ContactBackup_Contact;
GO

DROP TABLE Person.ContactBackup;
```

<a id="alter_column"></a>

### Alter a column definition

#### A. Change the data type of a column

The following example changes a column of a table from `INT` to `DECIMAL`.

```sql
CREATE TABLE dbo.doc_exy (column_a INT);
GO

INSERT INTO dbo.doc_exy (column_a) VALUES (10);
GO

ALTER TABLE dbo.doc_exy ALTER COLUMN column_a DECIMAL (5, 2);
GO

DROP TABLE dbo.doc_exy;
GO
```

#### B. Change the size of a column

The following example increases the size of a **varchar** column and the precision and scale of a **decimal** column. Because the columns contain data, you can only increase the column size. Also notice that `col_a` is defined in a unique index. You can still increase the size of `col_a` because the data type is a **varchar** and the index isn't the result of a `PRIMARY KEY` constraint.

```sql
-- Create a two-column table with a unique index on the varchar column.
CREATE TABLE dbo.doc_exy
(
    col_a VARCHAR (5) UNIQUE NOT NULL,
    col_b DECIMAL (4, 2)
);
GO

INSERT INTO dbo.doc_exy VALUES ('Test', 99.99);
GO

-- Verify the current column size.
SELECT name,
       TYPE_NAME(system_type_id),
       max_length,
       precision,
       scale
FROM sys.columns
WHERE object_id = OBJECT_ID(N'dbo.doc_exy');
GO

-- Increase the size of the varchar column.
ALTER TABLE dbo.doc_exy ALTER COLUMN col_a VARCHAR (25);
GO

-- Increase the scale and precision of the decimal column.
ALTER TABLE dbo.doc_exy ALTER COLUMN col_b DECIMAL (10, 4);
GO

-- Insert a new row.
INSERT INTO dbo.doc_exy VALUES ('MyNewColumnSize', 99999.9999);
GO

-- Verify the current column size.
SELECT name,
       TYPE_NAME(system_type_id),
       max_length,
       precision,
       scale
FROM sys.columns
WHERE object_id = OBJECT_ID(N'dbo.doc_exy');
```

#### C. Change column collation

The following example shows how to change the collation of a column. First, you create a table with the default user collation.

```sql
CREATE TABLE T3
(
    C1 INT PRIMARY KEY,
    C2 VARCHAR (50) NULL,
    C3 INT NULL,
    C4 INT
);
GO
```

Next, change the collation of column `C2` to `Latin1_General_BIN`. You must specify the data type, even if it isn't changed.

```sql
ALTER TABLE T3
    ALTER COLUMN C2 VARCHAR (50) COLLATE Latin1_General_BIN;
GO
```

#### D. Encrypt a column

The following example shows how to encrypt a column by using [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

First, you create a table without any encrypted columns.

```sql
CREATE TABLE T3
(
    C1 INT PRIMARY KEY,
    C2 VARCHAR (50) NULL,
    C3 INT NULL,
    C4 INT
);
GO
```

Next, encrypt column `C2` with a column encryption key, named `CEK1`, and randomized encryption. For the following statement to succeed:

- The column encryption key must be enclave-enabled. This requirement means it must be encrypted by using a column `master` key (CMK) that allows enclave computations.
- The target SQL Server instance must support Always Encrypted with secure enclaves.
- The statement must be issued over a connection set up for Always Encrypted with secure enclaves, and using a supported client driver.
- The calling application must have access to the CMK, protecting `CEK1`.

```sql
ALTER TABLE T3 ALTER COLUMN C2 VARCHAR (50)  ENCRYPTED WITH (
     COLUMN_ENCRYPTION_KEY = [CEK1],
     ENCRYPTION_TYPE = RANDOMIZED,
     ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
    ) NULL;
GO
```

<a id="alter_table"></a>

### Alter a table definition

The examples in this section demonstrate how to alter the definition of a table.

#### A. Modify a table to change the compression

The following example changes the compression of a nonpartitioned table. The heap or clustered index is rebuilt. If the table is a heap, all nonclustered indexes are rebuilt.

```sql
ALTER TABLE T1 REBUILD
    WITH (DATA_COMPRESSION = PAGE);
```

The following example changes the compression of a partitioned table. The `REBUILD PARTITION = 1` syntax causes only partition number `1` to be rebuilt.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

```sql
ALTER TABLE PartitionTable1 REBUILD PARTITION = 1
    WITH (DATA_COMPRESSION = NONE);
GO
```

The same operation using the following alternate syntax causes all partitions in the table to be rebuilt.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

```sql
ALTER TABLE PartitionTable1 REBUILD PARTITION = ALL
    WITH (DATA_COMPRESSION = PAGE ON PARTITIONS (1));
```

For additional data compression examples, see [Data compression](../../relational-databases/data-compression/data-compression.md).

#### B. Modify a columnstore table to change archival compression

The following example further compresses a columnstore table partition by applying an additional compression algorithm. This compression reduces the table to a smaller size, but also increases the time required for storage and retrieval. This compression is useful for archiving or for situations that require less space and can afford more time for storage and retrieval.

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
ALTER TABLE PartitionTable1 REBUILD PARTITION = 1
    WITH (DATA_COMPRESSION = COLUMNSTORE_ARCHIVE);
GO
```

The following example decompresses a columnstore table partition that was compressed with `COLUMNSTORE_ARCHIVE` option. When the data is restored, it continues to be compressed with the columnstore compression that's used for all columnstore tables.

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
ALTER TABLE PartitionTable1 REBUILD PARTITION = 1
    WITH (DATA_COMPRESSION = COLUMNSTORE);
GO
```

#### C. Switch partitions between tables

The following example creates a partitioned table, assuming that partition scheme `myRangePS1` is already created in the database. Next, a non-partitioned table is created with the same structure as the partitioned table and on the same filegroup as `PARTITION 2` of table `PartitionTable`. The data of `PARTITION 2` of table `PartitionTable` is then switched into table `NonPartitionTable`.

```sql
CREATE TABLE PartitionTable
(
    col1 INT,
    col2 CHAR (10)
) ON myRangePS1 (col1);
GO

CREATE TABLE NonPartitionTable
(
    col1 INT,
    col2 CHAR (10)
) ON test2fg;
GO

ALTER TABLE PartitionTable SWITCH PARTITION 2 TO NonPartitionTable;
GO
```

#### D. Allow lock escalation on partitioned tables

The following example enables lock escalation to the partition level on a partitioned table. If the table isn't partitioned, lock escalation is set at the `TABLE` level.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
ALTER TABLE dbo.T1 SET (LOCK_ESCALATION = AUTO);
GO
```

#### E. Configure change tracking on a table

The following example enables change tracking on the `Person.Person` table.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
USE AdventureWorks2022;

ALTER TABLE Person.Person ENABLE CHANGE_TRACKING;
```

The following example enables change tracking and enables the tracking of the columns that are updated during a change.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

```sql
USE AdventureWorks2022;
GO

ALTER TABLE Person.Person ENABLE CHANGE_TRACKING
    WITH (TRACK_COLUMNS_UPDATED = ON);
```

The following example disables change tracking on the `Person.Person` table.

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
USE AdventureWorks2022;
GO

ALTER TABLE Person.Person DISABLE CHANGE_TRACKING;
```

<a id="disable_enable"></a>

### Disable and enable constraints and triggers

#### A. Disable and re-enable a constraint

The following example disables a constraint that limits the salaries accepted in the data. Use `NOCHECK CONSTRAINT` with `ALTER TABLE` to disable the constraint and allow an insert that would typically violate the constraint. Use `CHECK CONSTRAINT` to re-enable the constraint.

```sql
CREATE TABLE dbo.cnst_example
(
    id INT NOT NULL,
    name VARCHAR (10) NOT NULL,
    salary MONEY NOT NULL
        CONSTRAINT salary_cap CHECK (salary < 100000)
);

-- Valid inserts
INSERT INTO dbo.cnst_example VALUES (1, 'Joe Brown', 65000);
INSERT INTO dbo.cnst_example VALUES (2, 'Mary Smith', 75000);

-- This insert violates the constraint.
INSERT INTO dbo.cnst_example VALUES (3, 'Pat Jones', 105000);

-- Disable the constraint and try again.
ALTER TABLE dbo.cnst_example NOCHECK CONSTRAINT salary_cap;
INSERT INTO dbo.cnst_example VALUES (3, 'Pat Jones', 105000);

-- Re-enable the constraint and try another insert; this fails.
ALTER TABLE dbo.cnst_example CHECK CONSTRAINT salary_cap;
INSERT INTO dbo.cnst_example VALUES (4, 'Eric James', 110000);
```

#### B. Disable and re-enable a trigger

The following example uses the `DISABLE TRIGGER` option of `ALTER TABLE` to disable the trigger and allow an insert that would typically violate the trigger. Use `ENABLE TRIGGER` to re-enable the trigger.

```sql
CREATE TABLE dbo.trig_example
(
    id INT,
    name VARCHAR (12),
    salary MONEY
);
GO

-- Create the trigger.
CREATE TRIGGER dbo.trig1
    ON dbo.trig_example
    FOR INSERT
    AS IF (SELECT COUNT(*)
           FROM INSERTED
           WHERE salary > 100000) > 0
           BEGIN
               PRINT 'TRIG1 Error: you attempted to insert a salary > $100,000';
               ROLLBACK;
           END
GO

-- Try an insert that violates the trigger.
INSERT INTO dbo.trig_example VALUES (1, 'Pat Smith', 100001);
GO

-- Disable the trigger.
ALTER TABLE dbo.trig_example DISABLE TRIGGER trig1;
GO

-- Try an insert that would typically violate the trigger.
INSERT INTO dbo.trig_example VALUES (2, 'Chuck Jones', 100001);
GO

-- Re-enable the trigger.
ALTER TABLE dbo.trig_example ENABLE TRIGGER trig1;
GO

-- Try an insert that violates the trigger.
INSERT INTO dbo.trig_example VALUES (3, 'Mary Booth', 100001);
GO
```

<a id="online"></a>

### Online operations

#### A. Online index rebuild using low-priority wait options

The following example shows how to perform an online index rebuild specifying the low-priority wait options.

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
ALTER TABLE T1 REBUILD WITH (
    PAD_INDEX = ON,
    ONLINE = ON (
        WAIT_AT_LOW_PRIORITY (MAX_DURATION = 4 MINUTES, ABORT_AFTER_WAIT = BLOCKERS)
    )
);
```

#### B. Online alter column

The following example shows how to run an alter column operation with the `ONLINE` option.

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```sql
CREATE TABLE dbo.doc_exy (column_a INT);
GO

INSERT INTO dbo.doc_exy (column_a) VALUES (10);
GO

ALTER TABLE dbo.doc_exy
    ALTER COLUMN column_a DECIMAL (5, 2) WITH (ONLINE = ON);
GO

EXECUTE sp_help doc_exy;
DROP TABLE dbo.doc_exy;
GO
```

<a id="system_versioning"></a>

### System versioning

The following four examples help you become familiar with the syntax for using system versioning. For additional assistance, see [Get started with system-versioned temporal tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md).

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

#### A. Add system versioning to existing tables

The following example shows how to add system versioning to an existing table and create a future history table. This example assumes that there's an existing table called `InsurancePolicy` with a primary key defined. This example populates the newly created period columns for system versioning using default values for the start and end times because these values can't be null. This example uses the `HIDDEN` clause to ensure no effect on existing applications interacting with the current table. It also uses `HISTORY_RETENTION_PERIOD` that's available on [!INCLUDE [sssds](../../includes/sssds-md.md)] only.

```sql
--Alter non-temporal table to define periods for system versioning
ALTER TABLE InsurancePolicy
    ADD ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN
            DEFAULT SYSUTCDATETIME() NOT NULL,
        ValidTo DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN
            DEFAULT CONVERT (DATETIME2, '9999-12-31 23:59:59.99999999') NOT NULL,
        PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

--Enable system versioning with 1 year retention for historical data
ALTER TABLE InsurancePolicy SET (
    SYSTEM_VERSIONING = ON (
        HISTORY_RETENTION_PERIOD=1 YEAR
    )
);
```

#### B. Migrate an existing solution to use system versioning

The following example shows how to migrate to system versioning from a solution that uses triggers to mimic temporal support. The example assumes there's an existing solution that uses a `ProjectTask` table and a `ProjectTaskHistory` table for its existing solution, that's uses the `Changed Date` and `Revised Date` columns for its periods, that these period columns don't use the **datetime2** data type and that the `ProjectTask` table has a primary key defined.

```sql
-- Drop existing trigger
DROP TRIGGER ProjectTask_HistoryTrigger;

-- Adjust the schema for current and history table
-- Change data types for existing period columns
ALTER TABLE ProjectTask ALTER COLUMN [Changed Date] DATETIME2 NOT NULL;
ALTER TABLE ProjectTask ALTER COLUMN [Revised Date] DATETIME2 NOT NULL;
ALTER TABLE ProjectTaskHistory ALTER COLUMN [Changed Date] DATETIME2 NOT NULL;
ALTER TABLE ProjectTaskHistory ALTER COLUMN [Revised Date] DATETIME2 NOT NULL;

-- Add SYSTEM_TIME period and set system versioning with linking two existing tables
-- (a certain set of data checks happen in the background)
ALTER TABLE ProjectTask
    ADD PERIOD FOR SYSTEM_TIME ([Changed Date], [Revised Date]);

ALTER TABLE ProjectTask SET (
    SYSTEM_VERSIONING = ON (
        HISTORY_TABLE=dbo.ProjectTaskHistory, DATA_CONSISTENCY_CHECK=ON
    )
);
```

#### C. Disable and re-enable system versioning to change table schema

This example shows how to disable system versioning on the `Department` table, add a column, and re-enable system versioning. Disabling system versioning is required to modify the table schema. Do these steps within a transaction to prevent updates to both tables while updating the table schema, which enables the DBA to skip the data consistency check when re-enabling system versioning and gain a performance benefit. Tasks such as creating statistics, switching partitions, or applying compression to one or both tables doesn't require disabling system versioning.

```sql
BEGIN TRAN
/* Takes schema lock on both tables */
ALTER TABLE Department
    SET (SYSTEM_VERSIONING = OFF) ;
/* expand table schema for temporal table */
ALTER TABLE Department
     ADD Col5 int NOT NULL DEFAULT 0 ;
/* Expand table schema for history table */
ALTER TABLE DepartmentHistory
    ADD Col5 int NOT NULL DEFAULT 0 ;
/* Re-establish versioning again*/
ALTER TABLE Department
    SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE=dbo.DepartmentHistory,
                                 DATA_CONSISTENCY_CHECK = OFF)) ;
COMMIT
```

#### D. Remove system versioning

This example shows how to completely remove system versioning from the Department table and drop the `DepartmentHistory` table. Optionally, you might also want to drop the period columns used by the system to record system versioning information. You can't drop either the `Department` or the `DepartmentHistory` tables while system versioning is enabled.

```sql
ALTER TABLE Department
    SET (SYSTEM_VERSIONING = OFF);

ALTER TABLE Department
    DROP PERIOD FOR SYSTEM_TIME;

DROP TABLE DepartmentHistory;
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

The following examples A through C use the `FactResellerSales` table in the [!INCLUDE [ssawPDW](../../includes/ssawpdw-md.md)] database.

### A. Determine if a table is partitioned

The following query returns one or more rows if the table `FactResellerSales` is partitioned. If the table isn't partitioned, the query returns no rows.

```sql
SELECT *
FROM sys.partitions AS p
     INNER JOIN sys.tables AS t
         ON p.object_id = t.object_id
WHERE p.partition_id IS NOT NULL
      AND t.name = 'FactResellerSales';
```

### B. Determine boundary values for a partitioned table

The following query returns the boundary values for each partition in the `FactResellerSales` table.

```sql
SELECT t.name AS TableName,
       i.name AS IndexName,
       p.partition_number,
       p.partition_id,
       i.data_space_id,
       f.function_id,
       f.type_desc,
       r.boundary_id,
       r.value AS BoundaryValue
FROM sys.tables AS t
     INNER JOIN sys.indexes AS i
         ON t.object_id = i.object_id
     INNER JOIN sys.partitions AS p
         ON i.object_id = p.object_id
        AND i.index_id = p.index_id
     INNER JOIN sys.partition_schemes AS s
         ON i.data_space_id = s.data_space_id
     INNER JOIN sys.partition_functions AS f
         ON s.function_id = f.function_id
     LEFT OUTER JOIN sys.partition_range_values AS r
         ON f.function_id = r.function_id
        AND r.boundary_id = p.partition_number
WHERE t.name = 'FactResellerSales'
      AND i.type <= 1
ORDER BY p.partition_number;
```

### C. Determine the partition column for a partitioned table

The following query returns the name of the partitioning column for the `FactResellerSales` table.

```sql
SELECT t.object_id AS Object_ID,
       t.name AS TableName,
       ic.column_id AS PartitioningColumnID,
       c.name AS PartitioningColumnName
FROM sys.tables AS t
     INNER JOIN sys.indexes AS i
         ON t.object_id = i.object_id
     INNER JOIN sys.columns AS c
         ON t.object_id = c.object_id
     INNER JOIN sys.partition_schemes AS ps
         ON ps.data_space_id = i.data_space_id
     INNER JOIN sys.index_columns AS ic
         ON ic.object_id = i.object_id
        AND ic.index_id = i.index_id
        AND ic.partition_ordinal > 0
WHERE t.name = 'FactResellerSales'
      AND i.type <= 1
      AND c.column_id = ic.column_id;
```

### D. Merge two partitions

The following example merges two partitions on a table.

The `Customer` table has the following definition:

```sql
CREATE TABLE Customer
(
    id INT NOT NULL,
    lastName VARCHAR (20),
    orderCount INT,
    orderDate DATE
)
WITH (
    DISTRIBUTION = HASH(id),
    PARTITION(orderCount RANGE LEFT
        FOR VALUES (1, 5, 10, 25, 50, 100)
    )
);
```

The following command combines the 10 and 25 partition boundaries.

```sql
ALTER TABLE Customer MERGE RANGE (10);
```

The new DDL for the table is:

```sql
CREATE TABLE Customer
(
    id INT NOT NULL,
    lastName VARCHAR (20),
    orderCount INT,
    orderDate DATE
)
WITH (
    DISTRIBUTION = HASH(id),
    PARTITION(orderCount RANGE LEFT
        FOR VALUES (1, 5, 25, 50, 100)
    )
);
```

### E. Split a partition

The following example splits a partition on a table.

The `Customer` table has the following DDL:

```sql
DROP TABLE Customer;

CREATE TABLE Customer
(
    id INT NOT NULL,
    lastName VARCHAR (20),
    orderCount INT,
    orderDate DATE
)
WITH (
    DISTRIBUTION = HASH(id),
    PARTITION(orderCount RANGE LEFT
        FOR VALUES (1, 5, 10, 25, 50, 100)
    )
);
```

The following command creates a new partition bound by the value 75, between 50 and 100.

```sql
ALTER TABLE Customer SPLIT RANGE (75);
```

The new DDL for the table is:

```sql
CREATE TABLE Customer (
   id INT NOT NULL,
   lastName VARCHAR(20),
   orderCount INT,
   orderDate DATE)
   WITH DISTRIBUTION = HASH(id),
   PARTITION ( orderCount (RANGE LEFT
      FOR VALUES (1, 5, 10, 25, 50, 75, 100))) ;
```

### F. Use SWITCH to move a partition to a history table

The following example moves the data in a partition of the `Orders` table to a partition in the `OrdersHistory` table.

The `Orders` table has the following DDL:

```sql
CREATE TABLE Orders
(
    id INT,
    city VARCHAR (25),
    lastUpdateDate DATE,
    orderDate DATE
)
WITH (
    DISTRIBUTION = HASH(id),
    PARTITION(orderDate RANGE RIGHT
        FOR VALUES ('2004-01-01', '2005-01-01', '2006-01-01', '2007-01-01')
    )
);
```

In this example, the `Orders` table has the following partitions. Each partition contains data.

| Partition | Has data? | Boundary range |
| --- | --- | --- |
| `1` | Yes | `OrderDate < '2004-01-01'` |
| `2` | Yes | `'2004-01-01' <= OrderDate < '2005-01-01'` |
| `3` | Yes | `'2005-01-01' <= OrderDate< '2006-01-01'` |
| `4` | Yes | `'2006-01-01'<= OrderDate < '2007-01-01'` |
| `5` | Yes | `'2007-01-01' <= OrderDate` |

- Partition 1 (has data): `OrderDate < '2004-01-01'`
- Partition 2 (has data): `'2004-01-01' <= OrderDate < '2005-01-01'`
- Partition 3 (has data): `'2005-01-01' <= OrderDate< '2006-01-01'`
- Partition 4 (has data): `'2006-01-01'<= OrderDate < '2007-01-01'`
- Partition 5 (has data): `'2007-01-01' <= OrderDate`

The `OrdersHistory` table has the following DDL, which has identical columns and column names as the `Orders` table. Both are hash-distributed on the `id` column.

```sql
CREATE TABLE OrdersHistory
(
    id INT,
    city VARCHAR (25),
    lastUpdateDate DATE,
    orderDate DATE
)
WITH (
    DISTRIBUTION = HASH(id),
    PARTITION(orderDate RANGE RIGHT
        FOR VALUES ('2004-01-01')
    )
);
```

Although the columns and column names must be the same, the partition boundaries don't need to be the same. In this example, the `OrdersHistory` table has the following two partitions and both partitions are empty:

- Partition 1 (no data): `OrderDate < '2004-01-01'`
- Partition 2 (empty): `'2004-01-01' <= OrderDate`

For the previous two tables, the following command moves all rows with `OrderDate < '2004-01-01'` from the `Orders` table to the `OrdersHistory` table.

```sql
ALTER TABLE Orders SWITCH PARTITION 1 TO OrdersHistory PARTITION 1;
```

As a result, the first partition in `Orders` is empty and the first partition in `OrdersHistory` contains data. The tables now appear as follows:

`Orders` table

- Partition 1 (empty): `OrderDate < '2004-01-01'`
- Partition 2 (has data): `'2004-01-01' <= OrderDate < '2005-01-01'`
- Partition 3 (has data): `'2005-01-01' <= OrderDate< '2006-01-01'`
- Partition 4 (has data): `'2006-01-01'<= OrderDate < '2007-01-01'`
- Partition 5 (has data): `'2007-01-01' <= OrderDate`

`OrdersHistory` table

- Partition 1 (has data): `OrderDate < '2004-01-01'`
- Partition 2 (empty): `'2004-01-01' <= OrderDate`

To clean up the `Orders` table, you can remove the empty partition by merging partitions `1` and `2` as follows:

```sql
ALTER TABLE Orders MERGE RANGE ('2004-01-01');
```

After the merge, the `Orders` table has the following partitions:

`Orders` table

- Partition 1 (has data): `OrderDate < '2005-01-01'`
- Partition 2 (has data): `'2005-01-01' <= OrderDate< '2006-01-01'`
- Partition 3 (has data): `'2006-01-01'<= OrderDate < '2007-01-01'`
- Partition 4 (has data): `'2007-01-01' <= OrderDate`

Suppose another year passes and you're ready to archive the year 2005. You can allocate an empty partition for the year 2005 in the `OrdersHistory` table by splitting the empty partition as follows:

```sql
ALTER TABLE OrdersHistory SPLIT RANGE ('2005-01-01');
```

After the split, the `OrdersHistory` table has the following partitions:

`OrdersHistory` table

- Partition 1 (has data): `OrderDate < '2004-01-01'`
- Partition 2 (empty): `'2004-01-01' < '2005-01-01'`
- Partition 3 (empty): `'2005-01-01' <= OrderDate`

## Related content

- [sys.tables](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)
- [sp_rename](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)
- [sp_help](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [DROP TABLE (Transact-SQL)](drop-table-transact-sql.md)
- [ALTER TABLE column_constraint (Transact-SQL)](alter-table-column-constraint-transact-sql.md)
- [ALTER TABLE column_definition (Transact-SQL)](alter-table-column-definition-transact-sql.md)
- [ALTER TABLE computed_column_definition (Transact-SQL)](alter-table-computed-column-definition-transact-sql.md)
- [ALTER TABLE index_option (Transact-SQL)](alter-table-index-option-transact-sql.md)
- [ALTER TABLE table_constraint (Transact-SQL)](alter-table-table-constraint-transact-sql.md)
