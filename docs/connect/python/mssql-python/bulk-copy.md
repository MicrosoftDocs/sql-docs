---
title: Use Bulk Copy with mssql-python Driver
description: Learn how to efficiently insert large amounts of data using the bulk copy feature with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use bulk copy with mssql-python

The mssql-python driver includes a bulk copy feature that efficiently inserts large amounts of data into SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

The `cursor.bulkcopy()` method provides a high-performance path for loading large datasets:

- Minimizes network round trips.
- Optionally bypasses constraint checking during load.
- Uses the optimized TDS bulk insert protocol.
- Achieves throughput comparable to `bcp.exe` and `SqlBulkCopy`.

The Rust-based `mssql_py_core` native extension powers the bulk copy feature. It runs outside the normal cursor `execute()` pipeline.

## Basic usage

Call `bulkcopy()` on a cursor, passing the target table name and an iterable of row tuples or `Row` objects:

> [!IMPORTANT]
> If you create or alter the target table in the same session, call `conn.commit()` before `bulkcopy()`. The bulk copy protocol uses a separate internal channel to read table metadata, so an uncommitted DDL change can cause a deadlock or timeout.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create a temp table for the demo
cursor.execute("""
    CREATE TABLE ##BulkDemo (
        ID INT,
        Name NVARCHAR(50),
        Amount MONEY
    )
""")
conn.commit()

data = [
    (1, "Alice", 50000.00),
    (2, "Bob", 60000.00),
    (3, "Carol", 55000.00),
]

result = cursor.bulkcopy("##BulkDemo", data)
print(f"Copied {result['rows_copied']} rows in {result['batch_count']} batch(es)")
print(f"Elapsed: {result['elapsed_time']}")
```

### Return value

`bulkcopy()` returns a dictionary:

| Key | Type | Description |
| --- | --- | --- |
| `rows_copied` | int | Number of rows successfully copied. |
| `batch_count` | int | Number of batches processed. |
| `elapsed_time` | float | Time taken for the operation in seconds. |

## Method signature

```python
cursor.bulkcopy(
    table_name,                    # str - target table (can include schema, e.g. "dbo.MyTable")
    data,                          # Iterable[Tuple | Row] - rows to insert
    batch_size=0,                  # int - rows per batch; 0 = server optimal
    timeout=30,                    # int - operation timeout in seconds
    column_mappings=None,          # List[str] | List[Tuple[int,str]] | None
    keep_identity=False,           # bool - preserve identity values from source
    check_constraints=False,       # bool - check constraints during load
    table_lock=False,              # bool - use table-level lock
    keep_nulls=False,              # bool - preserve NULLs instead of defaults
    fire_triggers=False,           # bool - fire INSERT triggers on target
    use_internal_transaction=False, # bool - use internal transaction per batch
)
```

## Column mappings

By default, `bulkcopy()` maps columns by ordinal position. Each data column maps to the table column at the same index. Use the `column_mappings` parameter to override this behavior.

### Column name list

Each position in the list corresponds to the source data index:

```python
result = cursor.bulkcopy(
    "##BulkDemo",
    data,
    column_mappings=["ID", "Name", "Amount"],
)
```

### Advanced format: explicit index mapping

Each tuple takes the form `(source_index, target_column_name)`. Use this format to skip or reorder columns:

```python
result = cursor.bulkcopy(
    "##BulkDemo",
    data,
    column_mappings=[(0, "ID"), (1, "Name"), (2, "Amount")],
)
```

## Load from files

You can load data from CSV files and other file formats by passing a generator to `bulkcopy()`.

### CSV file

```python
import csv
import io
import mssql_python

# In production, replace io.StringIO with open("data.csv", "r", ...)
csv_data = """ID,Name,Value
1,Widget,9.99
2,Gadget,24.50
3,Gizmo,4.75
"""

def csv_row_generator(file_obj):
    """Generator that yields tuples from a CSV file object."""
    reader = csv.reader(file_obj)
    next(reader)  # Skip header
    for row in reader:
        if row:  # skip blank lines
            yield (
                int(row[0]),      # ID
                row[1],           # Name
                float(row[2]),    # Value
            )

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##CSVImport (ID INT, Name NVARCHAR(100), Value FLOAT)
""")
conn.commit()
result = cursor.bulkcopy("##CSVImport", csv_row_generator(io.StringIO(csv_data)))
print(f"Imported {result['rows_copied']} rows from CSV")
```

### Large files with batching

Set the `batch_size` parameter to control how many rows the driver sends per batch. This approach works well for large files:

```python
import csv
import io
import mssql_python

# In production, replace io.StringIO with open("large_file.csv", "r", ...)
csv_data = "\n".join(
    ["ID,Name,Value"] + [f"{i},Item {i},{i * 1.5}" for i in range(1, 201)]
)

def csv_rows(file_obj):
    reader = csv.reader(file_obj)
    next(reader)  # Skip header
    for row in reader:
        if row:
            yield (int(row[0]), row[1], float(row[2]))

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##LargeCSV (ID INT, Name NVARCHAR(100), Value FLOAT)
""")
conn.commit()
result = cursor.bulkcopy(
    "##LargeCSV",
    csv_rows(io.StringIO(csv_data)),
    batch_size=50,
)
print(f"Imported {result['rows_copied']} rows in {result['batch_count']} batches")
```

## Load pandas DataFrames

A DataFrame is columnar, so the fastest path is `bulkcopy_arrow()`, which consumes the Arrow table that pandas already knows how to produce. `bulkcopy()` takes row tuples, so you need to flatten the columns into Python objects first.

Cast the Arrow table to the types of the destination columns before you load it. `pyarrow` infers `float64` for a numeric column, which the driver can't map to **money**, **decimal**, or **numeric**:

```python
import pandas as pd
import pyarrow as pa
import mssql_python

df = pd.DataFrame({
    'ID': [1, 2, 3],
    'Name': ['Alice', 'Bob', 'Carol'],
    'Amount': [50000.0, 60000.0, 55000.0],
})

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##PandasDemo (ID INT, Name NVARCHAR(50), Amount MONEY)
""")
conn.commit()

target = pa.schema([
    pa.field('ID', pa.int32()),
    pa.field('Name', pa.string()),
    pa.field('Amount', pa.decimal128(19, 4)),   # MONEY
])

table = pa.Table.from_pandas(df, preserve_index=False).cast(target)
result = cursor.bulkcopy_arrow("##PandasDemo", table)
```

Without the cast, the load fails with `ValueError: Cannot map Arrow column 'Amount' (Float64) to SQL column 'Amount' (Money)`. Build the cast with `Table.cast()` rather than passing the schema to `Table.from_pandas()`, which can't convert a float column to `decimal128` directly. `NaN` values become SQL `NULL` on this path, so you don't need to replace them first.

If you need the row-tuple path instead, `itertuples()` already yields tuples when you pass `name=None`:

```python
data = list(df.itertuples(index=False, name=None))
result = cursor.bulkcopy("##PandasDemo", data)
```

## Load Apache Arrow data

Use `cursor.bulkcopy_arrow()` to load Apache Arrow data. This method reads directly from Arrow memory, so you don't build Python row tuples before calling it.

The `source` argument accepts a `pyarrow.Table`, a `pyarrow.RecordBatch`, a `pyarrow.RecordBatchReader`, or any object that exposes the Arrow C data interface. The remaining arguments are the same as `bulkcopy()`.

```python
import mssql_python
import pyarrow as pa

conn = mssql_python.connect(connection_string)

# bulkcopy_arrow() opens its own connection, so commit the table creation first.
conn.autocommit = True
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##ArrowDemo (ID INT, Name NVARCHAR(50), Amount FLOAT)
""")

table = pa.table({
    "ID": pa.array([1, 2, 3], type=pa.int32()),
    "Name": pa.array(["Alice", "Bob", "Carol"], type=pa.string()),
    "Amount": pa.array([50000.0, 60000.0, 55000.0], type=pa.float64()),
})

result = cursor.bulkcopy_arrow("##ArrowDemo", table)
print(f"Copied {result['rows_copied']} rows")
```

Each Arrow column type must be compatible with its destination SQL column type. The writer doesn't convert between type families, so passing a `float64` column to a **money** column raises `ValueError` before any rows are written. Use `decimal128` for **money**, **decimal**, and **numeric** columns.

Passing an Arrow source to `bulkcopy()` raises `TypeError` and directs you to `bulkcopy_arrow()`.

For more information about Arrow support, including how to stream a result set from one table into another, see [Apache Arrow integration](arrow-integration.md).

## Handle NULL values

Pass `None` in any column position to insert a SQL `NULL` value:

```python
cursor.execute("""
    CREATE TABLE ##NullDemo (ID INT, Name NVARCHAR(50), Amount MONEY)
""")
conn.commit()

data = [
    (1, "Alice", 50000.00),
    (2, "Bob", None),       # NULL Amount
    (3, None, 55000.00),    # NULL Name
]

cursor.bulkcopy("##NullDemo", data)
```

## Identity columns

To insert explicit identity values, set `keep_identity=True`:

```python
cursor.execute("""
    CREATE TABLE ##IdentDemo (ID INT, Name NVARCHAR(50), Amount MONEY)
""")
conn.commit()

data = [
    (100, "Alice", 50000.00),
    (200, "Bob", 60000.00),
]

cursor.bulkcopy("##IdentDemo", data, keep_identity=True)
```

When `keep_identity=False` (the default), omit the identity column from your data and use `column_mappings` to target the nonidentity columns.

## Bulk copy options

| Parameter | Default | Description |
| --- | --- | --- |
| `batch_size` | `0` | Rows per batch. `0` lets the server choose the optimal size. |
| `timeout` | `30` | Operation timeout in seconds. Applies to the bulk copy operation itself, not to the internal connection. Use `0` to disable the operation timeout. |
| `keep_identity` | `False` | Preserve identity values from source data. |
| `check_constraints` | `False` | Check table constraints during the load. |
| `table_lock` | `False` | Acquire a table-level lock instead of row-level locks. |
| `keep_nulls` | `False` | Preserve NULL values instead of inserting column defaults. |
| `fire_triggers` | `False` | Fire INSERT triggers on the target table. |
| `use_internal_transaction` | `False` | Wrap each batch in an internal transaction. |

> [!NOTE]
> `bulkcopy()` opens a separate internal connection to the server. That internal connection inherits the cursor's query timeout: set `Connection.timeout` to a positive value before you create the cursor, and the same value bounds the bulk copy connection attempt. If the cursor's query timeout is `0`, the internal connection uses its default 15-second connect timeout. A cursor takes the value when it's created, so changing `Connection.timeout` afterward doesn't affect an existing cursor or an in-flight bulk copy. Raise the query timeout before you create the cursor for slow, throttled, or high-latency endpoints (for example, over a VPN or across regions).

## Handle errors

`bulkcopy()` raises an exception if the load fails, so wrap the call in a `try`/`except` block to catch errors. Keep in mind that `bulkcopy()` runs on its own internal connection and commits the copied rows independently, so a `conn.rollback()` on your main connection can't undo them. To make a batch atomic, set `use_internal_transaction=True`, which wraps each batch in its own transaction that rolls back automatically if the batch fails:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##ImportDemo (ID INT, Name NVARCHAR(50), Value FLOAT)
""")
conn.commit()

data = [
    (1, "Alice", 50000.00),
    (2, "Bob", 60000.00),
    (3, "Carol", 55000.00),
]

try:
    result = cursor.bulkcopy("##ImportDemo", data, use_internal_transaction=True)
    print(f"Successfully copied {result['rows_copied']} rows")
except (mssql_python.DatabaseError, ValueError) as e:
    # bulkcopy() commits on its own connection, so there's nothing to roll back
    # here. With use_internal_transaction=True, a failed batch is already rolled
    # back on the bulk copy connection.
    print(f"Bulk copy failed: {e}")
```

To gate a load behind your own validation logic, bulk copy into a staging table, then promote the rows to the target table with an `INSERT ... SELECT` inside a transaction on your main connection. That `INSERT` runs on your connection, so `conn.rollback()` undoes it if validation fails.

## Authentication

Bulk copy uses a separate internal channel that requires its own token. The driver handles token acquisition automatically for the supported authentication methods.

### Managed identity (ActiveDirectoryMSI)

Use `Authentication=ActiveDirectoryMSI` for system-assigned or user-assigned managed identity. This authentication method is recommended for Azure-hosted services such as Azure VMs, App Service, Functions, and AKS.

```python
import mssql_python

# System-assigned managed identity
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryMSI;"
    "Encrypt=yes"
)
cursor = conn.cursor()

cursor.execute("CREATE TABLE ##MsiDemo (ID INT, Name NVARCHAR(50))")
conn.commit()

result = cursor.bulkcopy("##MsiDemo", [(1, "Alice"), (2, "Bob")])
print(f"Copied {result['rows_copied']} rows")
```

For a user-assigned managed identity, pass the client ID in the connection string:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryMSI;"
    "UID=<client-id>;"
    "Encrypt=yes"
)
```

### Service principal (ActiveDirectoryServicePrincipal)

Use `Authentication=ActiveDirectoryServicePrincipal` for service principal (client credentials) authentication.

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryServicePrincipal;"
    "UID=<application-client-id>;"
    "PWD=<client-secret>;"
    "Encrypt=yes"
)
cursor = conn.cursor()

cursor.execute("CREATE TABLE ##SpDemo (ID INT, Value FLOAT)")
conn.commit()

result = cursor.bulkcopy("##SpDemo", [(1, 1.5), (2, 2.5)])
print(f"Copied {result['rows_copied']} rows")
```

### Default credential chain (ActiveDirectoryDefault)

`ActiveDirectoryDefault` tries multiple credential providers in sequence, such as environment variables, workload identity, managed identity, and more. It works for both local development and Azure-hosted services without code changes.

For more information about authentication, see [Microsoft Entra authentication](entra-authentication.md).

## Performance tips

The following techniques help you maximize bulk copy throughput.

### Start from a columnar source

`bulkcopy()` takes an iterable of row tuples, so every value has to exist as a Python object before the copy starts. When the data is already columnar, `bulkcopy_arrow()` reads the Arrow buffers directly and skips that step. A pandas or Polars DataFrame, a Parquet file, and the result of `cursor.arrow()` are all Arrow sources. For more information, see [Load Apache Arrow data](#load-apache-arrow-data).

### Use generators for large datasets

Generators minimize memory usage because `bulkcopy()` accepts any iterable:

```python
def data_generator(count):
    """Generate rows without loading all into memory."""
    for i in range(count):
        yield (i, f"Item {i}", i * 1.5)

cursor = conn.cursor()
cursor.execute("""
    CREATE TABLE ##LargeDemo (ID INT, Name NVARCHAR(50), Value FLOAT)
""")
conn.commit()
result = cursor.bulkcopy("##LargeDemo", data_generator(1000))
```

### Use table locks for faster loads

When you have no concurrent readers, set `table_lock=True` to reduce locking overhead during large initial loads.

```python
result = cursor.bulkcopy(
    "##LargeDemo",
    data,
    table_lock=True,
    batch_size=100000,
)
```

### Disable indexes during load

Temporarily disable nonclustered indexes before the bulk load and rebuild them afterward for improved performance:

```python
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##IndexDemo (ID INT, Name NVARCHAR(50), Value FLOAT)
""")
cursor.execute("CREATE NONCLUSTERED INDEX IX_Name ON ##IndexDemo(Name)")
conn.commit()

cursor.execute("ALTER INDEX IX_Name ON ##IndexDemo DISABLE")
conn.commit()

result = cursor.bulkcopy("##IndexDemo", data)
conn.commit()

cursor.execute("ALTER INDEX IX_Name ON ##IndexDemo REBUILD")
conn.commit()
```

### Load tables in parallel

Open a separate connection for each table and run the loads concurrently.

```python
import concurrent.futures

def load_table(table_name, rows):
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute(f"CREATE TABLE {table_name} (ID INT, Name NVARCHAR(50), Value FLOAT)")
    conn.commit()
    result = cursor.bulkcopy(table_name, rows)
    conn.commit()
    conn.close()
    return result["rows_copied"]

data = [(i, f"Item {i}", i * 1.5) for i in range(100)]

with concurrent.futures.ThreadPoolExecutor(max_workers=3) as executor:
    futures = [
        executor.submit(load_table, "##Load1", data),
        executor.submit(load_table, "##Load2", data),
        executor.submit(load_table, "##Load3", data),
    ]
    for future in concurrent.futures.as_completed(futures):
        print(f"Loaded {future.result()} rows")
```

## Comparison with alternatives

The following table compares bulk copy with other data insertion methods.

| Method | Use case | Performance |
| --- | --- | --- |
| `cursor.bulkcopy_arrow()` | Large datasets that are already columnar. | Fastest |
| `cursor.bulkcopy()` | Large datasets (more than 1,000 rows) from row-oriented sources. | Fast |
| `cursor.executemany()` | Medium datasets with parameters. | Moderate |
| `cursor.execute()` in a loop | Small datasets with straightforward logic. | Slowest |

## Related content

- [Execute queries with mssql-python](executing-queries.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Transaction management with mssql-python](transaction-management.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
