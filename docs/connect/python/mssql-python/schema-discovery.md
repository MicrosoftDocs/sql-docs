---
title: Schema Discovery with mssql-python
description: Learn how to retrieve database metadata including tables, columns, procedures, keys, and indexes using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Schema discovery with mssql-python

The mssql-python cursor class provides nine metadata methods that map to ODBC catalog functions. Use these methods to discover tables, columns, stored procedures, keys, and indexes programmatically. They help you build data-driven applications that adapt to the database schema at runtime, such as migration tools, code generators, or admin dashboards.

| Method | ODBC function | Returns | When to use |
| -------- | --------------- | --------- | ------------ |
| `tables()` | SQLTables | Table and view information. | Inventory databases. Validate table existence before queries. |
| `columns()` | SQLColumns | Column details. | Generate DDL, build dynamic queries, or map columns to code. |
| `procedures()` | SQLProcedures | Stored procedure information. | Discover available APIs. Generate procedure call wrappers. |
| `primaryKeys()` | SQLPrimaryKeys | Primary key columns. | Identify unique row identifiers for UPDATE/DELETE operations. |
| `foreignKeys()` | SQLForeignKeys | Foreign key relationships. | Map table relationships, determine delete order for cleanup scripts. |
| `statistics()` | SQLStatistics | Index and statistics information. | Verify index coverage for performance tuning. |
| `rowIdColumns()` | SQLSpecialColumns (ROWID) | Unique row identifier columns. | Find the best columns to use for identifying specific rows. |
| `rowVerColumns()` | SQLSpecialColumns (ROWVER) | Row version columns. | Implement optimistic concurrency (detect concurrent modifications). |
| `getTypeInfo()` | SQLGetTypeInfo | Data type information. | Discover supported types for cross-platform compatibility. |

Each method returns a cursor that you can iterate to access the results.

## Tables

List tables and views in the database:

```python
cursor = conn.cursor()

# List all tables
for row in cursor.tables():
    print(f"{row.table_schem}.{row.table_name} ({row.table_type})")

# Filter by name (supports wildcards % and _)
for row in cursor.tables(table="Product%"):
    print(row.table_name)

# Filter by schema
for row in cursor.tables(schema="Sales"):
    print(row.table_name)

# Filter by type
for row in cursor.tables(tableType="TABLE"):  # Excludes views
    print(row.table_name)
```

### tables() parameters

The following parameters control table discovery:

| Parameter | Description |
| ----------- | ------------- |
| `table` | Table name pattern (supports `%` and `_` wildcards). |
| `catalog` | Catalog (database) name. |
| `schema` | Schema name pattern. |
| `tableType` | Filter by type: `TABLE`, `VIEW`, `SYSTEM TABLE`, `GLOBAL TEMPORARY`, `LOCAL TEMPORARY`, `ALIAS`, `SYNONYM`. |

### tables() result columns

The `tables()` method returns the following columns for each table or view:

| Column | Description |
| -------- | ------------- |
| `table_cat` | Catalog (database) name. |
| `table_schem` | Schema name. |
| `table_name` | Table or view name. |
| `table_type` | `TABLE`, `VIEW`, `SYSTEM TABLE`, `GLOBAL TEMPORARY`, `LOCAL TEMPORARY`, `ALIAS`, `SYNONYM`. |
| `remarks` | Description or comments. |

### Check if a table exists

Verify that a table exists before querying it:

```python
if cursor.tables(table="Product", schema="Production").fetchone():
    print("Product table exists")
else:
    print("Product table not found")
```

## Columns

Retrieve column information for tables:

```python
# All columns in a table
for row in cursor.columns(table="Product", schema="Production"):
    print(f"{row.column_name}: {row.type_name}({row.column_size})")
    print(f"  Nullable: {row.nullable}, Position: {row.ordinal_position}")

# Filter by column name
for row in cursor.columns(table="Product", schema="Production", column="List%"):
    print(row.column_name)
```

### columns() parameters

Filters to refine the column discovery:

| Parameter | Description |
| ----------- | ------------- |
| `table` | Table name pattern. |
| `catalog` | Catalog (database) name. |
| `schema` | Schema name pattern. |
| `column` | Column name pattern. |

### columns() result columns

The `columns()` method returns detailed information about each column:

| Column | Description |
| -------- | ------------- |
| `table_cat`, `table_schem`, `table_name` | Location identifiers. |
| `column_name` | Column name. |
| `data_type` | SQL data type code. |
| `type_name` | Data type name (for example, `varchar`, `int`). |
| `column_size` | Maximum length or precision. |
| `buffer_length` | Buffer size for transfers. |
| `decimal_digits` | Scale for numeric types. |
| `nullable` | `0` for NOT NULL, `1` for nullable. |
| `column_def` | Default value. |
| `ordinal_position` | Column position (1-based). |
| `is_nullable` | `"YES"` or `"NO"`. |

## Stored procedures

Discover stored procedures:

```python
# List all procedures
for row in cursor.procedures():
    print(f"{row.procedure_schem}.{row.procedure_name}")

# Filter by name pattern
for row in cursor.procedures(procedure="Get%"):
    print(row.procedure_name)
```

### procedures() parameters

Filter stored procedures by name or schema:

| Parameter | Description |
| ----------- | ------------- |
| `procedure` | Procedure name pattern. |
| `catalog` | Catalog (database) name. |
| `schema` | Schema name pattern. |

### procedures() result columns

The `procedures()` method returns metadata for each stored procedure:

| Column | Description |
| -------- | ------------- |
| `procedure_cat`, `procedure_schem` | Location identifiers. |
| `procedure_name` | Procedure name. |
| `num_input_params` | Number of input parameters. |
| `num_output_params` | Number of output parameters. |
| `num_result_sets` | Number of result sets. |
| `remarks` | Description. |
| `procedure_type` | Type indicator. |

## Primary keys

Get primary key columns for a table:

```python
for row in cursor.primaryKeys(table="Product", schema="Production"):
    print(f"PK column: {row.column_name} (position {row.key_seq})")
    print(f"Constraint name: {row.pk_name}")
```

### primaryKeys() parameters

Parameters to retrieve primary key information:

| Parameter | Description |
| ----------- | ------------- |
| `table` | Table name (required). |
| `catalog` | Catalog (database) name. |
| `schema` | Schema name. |

### primaryKeys() result columns

The `primaryKeys()` method returns the following information:

| Column | Description |
| -------- | ------------- |
| `table_cat`, `table_schem`, `table_name` | Location identifiers. |
| `column_name` | Column in the primary key. |
| `key_seq` | Position in multi-column key (1-based). |
| `pk_name` | Primary key constraint name. |

## Foreign keys

Discover foreign key relationships:

```python
# Foreign keys from a table (outbound references)
for row in cursor.foreignKeys(table="SalesOrderDetail", schema="Sales"):
    print(f"FK {row.fk_name}:")
    print(f"  {row.fktable_name}.{row.fkcolumn_name}")
    print(f"  -> {row.pktable_name}.{row.pkcolumn_name}")

# Foreign keys to a table (inbound references)
for row in cursor.foreignKeys(foreignTable="Product", foreignSchema="Production"):
    print(f"{row.fktable_name} references Product")
```

### foreignKeys() parameters

Specify primary key or foreign key tables to discover relationships:

| Parameter | Description |
| ----------- | ------------- |
| `table` | Primary key table name. |
| `catalog` | Primary key catalog. |
| `schema` | Primary key schema. |
| `foreignTable` | Foreign key table name. |
| `foreignCatalog` | Foreign key catalog. |
| `foreignSchema` | Foreign key schema. |

### foreignKeys() result columns

The `foreignKeys()` method returns the following columns describing relationships:

| Column | Description |
| -------- | ------------- |
| `pktable_cat`, `pktable_schem`, `pktable_name` | Referenced (primary) table. |
| `pkcolumn_name` | Referenced column. |
| `fktable_cat`, `fktable_schem`, `fktable_name` | Referencing (foreign) table. |
| `fkcolumn_name` | Referencing column. |
| `key_seq` | Position in multi-column key. |
| `update_rule` | Action on UPDATE. |
| `delete_rule` | Action on DELETE. |
| `fk_name` | Foreign key constraint name. |
| `pk_name` | Primary key constraint name. |

## Indexes and statistics

Get index information for a table:

```python
# All indexes on a table
for row in cursor.statistics(table="Product", schema="Production"):
    if row.index_name:  # Skip table statistics row
        print(f"Index: {row.index_name}")
        print(f"  Column: {row.column_name} (position {row.ordinal_position})")
        print(f"  Unique: {not row.non_unique}")

# Only unique indexes
for row in cursor.statistics(table="Product", schema="Production", unique=True):
    print(f"Unique index: {row.index_name}")
```

### statistics() parameters

Configure index discovery with these filters:

| Parameter | Default | Description |
| ----------- | --------- | ------------- |
| `table` | (required) | Table name. |
| `catalog` | None | Catalog (database) name. |
| `schema` | None | Schema name. |
| `unique` | False | Only return unique indexes. |
| `quick` | True | Skip expensive cardinality/pages retrieval. |

### statistics() result columns

The `statistics()` method returns index and statistics information:

| Column | Description |
| -------- | ------------- |
| `table_cat`, `table_schem`, `table_name` | Location identifiers. |
| `non_unique` | `0` for unique, `1` for nonunique. |
| `index_name` | Index name. |
| `type` | Index type. |
| `ordinal_position` | Column position in index. |
| `column_name` | Column name. |
| `asc_or_desc` | `A` for ascending, `D` for descending. |
| `cardinality` | Row count estimate. |
| `pages` | Page count. |

## Row identifier columns

Find columns that uniquely identify a row:

```python
for row in cursor.rowIdColumns(table="Product", schema="Production"):
    print(f"Row ID column: {row.column_name} ({row.type_name})")
```

This method returns the best set of columns to uniquely identify a row, which might be the primary key or a unique index.

## Row version columns

Find columns that are automatically updated when any row value changes. Use row version columns for optimistic concurrency control, where you read a row's version, make changes, and then verify the current row version is the same before writing:

```python
for row in cursor.rowVerColumns(table="Product", schema="Production"):
    print(f"Version column: {row.column_name}")
```

The result typically includes `rowversion`/`timestamp` columns used for optimistic concurrency.

## Data type information

Get information about supported SQL data types:

```python
# All supported types
for row in cursor.getTypeInfo():
    print(f"{row.type_name}: {row.data_type}")
    print(f"  Max size: {row.column_size}")
    print(f"  Nullable: {row.nullable}")

# Specific type
for row in cursor.getTypeInfo(sqlType=mssql_python.SQL_VARCHAR):
    print(f"VARCHAR max size: {row.column_size}")
```

### getTypeInfo() parameters

Optional parameters to filter supported SQL types:

| Parameter | Description |
| ----------- | ------------- |
| `sqlType` | SQL type constant (omit for all types). |

## Security considerations

> [!CAUTION]
> These methods expose database schema metadata. While the methods themselves are safe to execute, the returned information reveals your database structure (table names, column names, relationships, data types).
>
> - Don't expose raw metadata to untrusted users.
> - Sanitize or filter results in multitenant applications.
> - Restrict access in externally facing applications.

## Example: Generate a schema report

```python
def describe_table(conn, table_name):
    """Generate a schema description for a table."""
    cursor = conn.cursor()
    
    print(f"\n=== {table_name} ===\n")
    
    # Columns
    print("Columns:")
    for col in cursor.columns(table=table_name):
        nullable = "NULL" if col.nullable else "NOT NULL"
        print(f"  {col.column_name}: {col.type_name}({col.column_size}) {nullable}")
    
    # Primary key
    print("\nPrimary Key:")
    pk_cols = cursor.primaryKeys(table=table_name).fetchall()
    if pk_cols:
        pk_names = ", ".join(row.column_name for row in pk_cols)
        print(f"  {pk_cols[0].pk_name}: ({pk_names})")
    else:
        print("  (none)")
    
    # Foreign keys
    print("\nForeign Keys:")
    for fk in cursor.foreignKeys(table=table_name):
        print(f"  {fk.fk_name}: {fk.fkcolumn_name} -> {fk.pktable_name}.{fk.pkcolumn_name}")
    
    # Indexes
    print("\nIndexes:")
    for idx in cursor.statistics(table=table_name):
        if idx.index_name:
            unique = "UNIQUE " if not idx.non_unique else ""
            print(f"  {unique}{idx.index_name}: {idx.column_name}")

# Usage
describe_table(conn, "Product")
```

## Related content

- [Execute queries](executing-queries.md)
- [Retrieve data](retrieving-data.md)
- [Cursor management](cursor-management.md)
