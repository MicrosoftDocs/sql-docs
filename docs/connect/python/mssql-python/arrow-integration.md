---
title: Use mssql-python with Apache Arrow
description: Learn how to use Apache Arrow fetch methods in mssql-python for high-performance columnar data retrieval from Microsoft SQL and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with Apache Arrow

The mssql-python driver provides Apache Arrow fetch methods for high-performance columnar data retrieval from Microsoft SQL and Azure SQL Database.

Apache Arrow is a cross-language development platform for in-memory columnar data. The driver converts ODBC result sets directly into Arrow format in C++, bypassing Python object creation for improved performance.

Arrow integration enables:

- **Zero-copy data transfer** to Polars, pandas, and DuckDB. "Zero-copy" means the data stays in a single memory buffer that the driver writes and consuming libraries read directly, so no rows are duplicated into intermediate Python objects.
- Streaming result sets through `RecordBatchReader` without loading everything into memory.
- Columnar data format ideal for analytics and machine learning workloads.
- Reduced memory usage compared to row-by-row Python object creation.

## Cursor methods

The `pyarrow` package is required to use Arrow fetch methods. Install it with `pip install pyarrow`. If `pyarrow` isn't installed, calling any Arrow method raises an `ImportError`.

The mssql-python driver adds three methods to the cursor object for Arrow data access. All three methods convert ODBC result sets to Arrow format in the driver's C++ layer, which avoids creating intermediate Python objects.

- **`arrow()`** returns the entire result set as one in-memory table. Simplest to use.
- **`arrow_batch()`** returns one batch of rows at a time, giving you manual control over the loop.
- **`arrow_reader()`** returns an iterator that yields batches automatically. Best for streaming large results.

### Using `cursor.arrow(batch_size=8192)`

Fetch the entire result set as a single `pyarrow.Table`. This method is the simplest and works well when the full result set fits in memory.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")
table = cursor.arrow()

print(type(table))       # <class 'pyarrow.lib.Table'>
print(table.num_rows)    # Number of rows fetched
print(table.num_columns) # Number of columns
print(table.schema)      # Column names and Arrow types
print(table.to_pandas()) # Convert to pandas DataFrame
```

> [!NOTE]
> If your connection string uses `Authentication=ActiveDirectoryDefault`, the driver uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

### Using `cursor.arrow_batch(batch_size=8192)`

Fetch a single `pyarrow.RecordBatch` containing up to `batch_size` rows. Use this method for custom batch processing loops where you need fine-grained control over how many rows are fetched at a time.

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")

while True:
    batch = cursor.arrow_batch(batch_size=10000)
    if batch.num_rows == 0:
        break
    # Process each batch
    print(f"Fetched {batch.num_rows} rows")
```

### Using `cursor.arrow_reader(batch_size=8192)`

Return a reader that yields `RecordBatch` objects until the result set is exhausted. This method is the most memory-efficient option for large result sets.

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")
reader = cursor.arrow_reader(batch_size=50000)

for batch in reader:
    # Process streaming batches without loading all data
    print(f"Batch: {batch.num_rows} rows")
```

The reader streams results over the connection, so while an unread reader is open, that connection can't start another statement. Attempting one fails with a `Connection is busy with results for another command` error.

Three things release the reader: iterating it to the end, closing the parent cursor, or closing the reader. If you stop reading before the result set is exhausted and keep using the cursor, close the reader. Closing it also resets the parent cursor, so you can run another statement on it.

Use the reader as a context manager so that it closes even if an exception interrupts the loop:

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")

rows_seen = 0
with cursor.arrow_reader(batch_size=50000) as reader:
    for batch in reader:
        rows_seen += batch.num_rows
        if rows_seen >= 100000:
            break

# The reader is closed here, and the cursor is ready for the next statement.
cursor.execute("SELECT COUNT(*) FROM Production.TransactionHistory")
```

You can also call `reader.close()` directly. Calling it more than once is safe, and the `reader.closed` property reports whether you closed it.

## Common patterns

Arrow tables integrate directly with popular Python data libraries. The following examples show how to pass Arrow data to pandas, Polars, DuckDB, and file formats without copying data.

### Load results into pandas

```python
cursor.execute("SELECT * FROM Production.Product")
table = cursor.arrow()

# Convert to pandas with zero-copy where possible
df = table.to_pandas()
print(df.head())
```

### Load results into Polars

```python
import polars as pl

cursor.execute("SELECT * FROM Production.Product")
table = cursor.arrow()

df = pl.from_arrow(table)
print(df)
```

### Query results with DuckDB

DuckDB can query Arrow tables directly in SQL without copying data. This capability is useful when you need SQL-style analysis on result sets that are already in Arrow format.

```python
import duckdb

cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
arrow_table = cursor.arrow()

# Query the Arrow table with DuckDB SQL
result = duckdb.sql("SELECT CustomerID, SUM(TotalDue) FROM arrow_table GROUP BY CustomerID")
print(result.fetchall())
```

### Stream large result sets to Parquet

For large result sets, stream Arrow batches directly to a Parquet file without loading the entire dataset into memory. The `ParquetWriter` writes each batch incrementally.

```python
import pyarrow.parquet as pq

cursor.execute("SELECT * FROM Production.TransactionHistory")
reader = cursor.arrow_reader(batch_size=100000)

# Write streaming batches to a Parquet file
writer = None
for batch in reader:
    if writer is None:
        writer = pq.ParquetWriter("output.parquet", batch.schema)
    writer.write_batch(batch)

if writer:
    writer.close()
```

### Export to other formats

PyArrow provides built-in writers for CSV and the Arrow IPC file format (also known as Feather V2). Arrow IPC files preserve Arrow types exactly and are fast to read back.

```python
import pyarrow as pa
import pyarrow.csv as pcsv

cursor.execute("SELECT * FROM Production.Product")
table = cursor.arrow()

# Write to CSV
pcsv.write_csv(table, "products.csv")

# Write to an Arrow IPC file
with pa.ipc.new_file("products.arrow", table.schema) as writer:
    writer.write_table(table)
```

## Load Arrow data into SQL Server

The `cursor.bulkcopy_arrow()` method writes Arrow data to a table without converting it to Python row tuples first. The `source` argument accepts any of the following:

- A `pyarrow.Table`.

- A `pyarrow.RecordBatch`.

- A `pyarrow.RecordBatchReader`, including the reader returned by `cursor.arrow_reader()`.

- Any object that exposes the Arrow C data interface through `__arrow_c_stream__` or `__arrow_c_array__`.

```python
import mssql_python
import pyarrow as pa

conn = mssql_python.connect(connection_string)

# bulkcopy_arrow() opens its own connection, so commit the table creation first.
conn.autocommit = True
cursor = conn.cursor()

cursor.execute("""
    CREATE TABLE ##SensorArchive (
        SensorID int NOT NULL,
        Reading float NULL,
        Location nvarchar(50) NULL
    )
""")

table = pa.table({
    "SensorID": pa.array([1, 2, 3], type=pa.int32()),
    "Reading": pa.array([20.5, None, 22.1], type=pa.float64()),
    "Location": pa.array(["Plant A", "Plant B", None], type=pa.string()),
})

result = cursor.bulkcopy_arrow("##SensorArchive", table)
print(f"Copied {result['rows_copied']} rows in {result['batch_count']} batches")
```

Arrow null values are written as SQL NULL values.

### Stream a result set into another table

Because `bulkcopy_arrow()` accepts a reader, you can move a large result set between tables without materializing it in memory:

```python
cursor.execute("""
    CREATE TABLE ##ProductArchive (
        ProductID int NOT NULL,
        Name nvarchar(50) NOT NULL,
        ListPrice money NOT NULL
    )
""")

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")

with cursor.arrow_reader(batch_size=100000) as reader:
    result = cursor.bulkcopy_arrow("##ProductArchive", reader, batch_size=100000)

print(f"Copied {result['rows_copied']} rows")
```

### Match Arrow types to the destination columns

The Arrow writer requires each Arrow column type to be compatible with its destination SQL column type. It doesn't convert between families, so a mismatch raises `ValueError` before any rows are written:

```output
ValueError: Cannot map Arrow column 'ListPrice' (Float64) to SQL column 'ListPrice'
(Money): Usage Error: type combination is not supported by the Arrow row-major writer
```

Use the mappings in [Data type mappings](#data-type-mappings) in reverse to choose the Arrow type. **money**, **decimal**, and **numeric** columns need `decimal128`, not `float64`. Data read back with `cursor.arrow()` already carries the correct types, so a table read from SQL Server loads into a matching table without conversion.

### Map columns by name

When the Arrow column order doesn't match the destination table, pass `column_mappings` with the destination column names in Arrow column order:

```python
from decimal import Decimal

table = pa.table({
    "Name": pa.array(["Widget"], type=pa.string()),
    "ProductID": pa.array([9001], type=pa.int32()),
    "ListPrice": pa.array([Decimal("12.34")], type=pa.decimal128(19, 4)),
})

cursor.bulkcopy_arrow(
    "##ProductArchive",
    table,
    column_mappings=["Name", "ProductID", "ListPrice"],
)
```

The method accepts the same options as `cursor.bulkcopy()`, including `batch_size`, `timeout`, `keep_identity`, `table_lock`, and `keep_nulls`. For more information about those options, see [Bulk copy](bulk-copy.md).

> [!NOTE]
> Passing an Arrow source to `cursor.bulkcopy()` raises `TypeError` and directs you to `cursor.bulkcopy_arrow()`.

## Data type mappings

The Arrow fetch methods map Microsoft SQL types to Arrow types at the C++ level.

| Microsoft SQL type | Arrow type |
| --- | --- |
| **int**, **smallint**, **tinyint**, **bigint** | `int32`, `int16`, `int8`, `int64` |
| **float**, **real** | `float64`, `float32` |
| **decimal**, **numeric** | `decimal128` |
| **bit** | `bool` |
| **char**, **varchar**, **nchar**, **nvarchar** | `utf8` |
| **text**, **ntext** | `large_utf8` |
| **binary**, **varbinary** | `binary`, `large_binary` |
| **date** | `date32` |
| **time** | `time64[us]` |
| **datetime**, **datetime2**, **smalldatetime** | `timestamp[us]` |
| **datetimeoffset** | `timestamp[us, tz=UTC]` |
| **uniqueidentifier** | `utf8` (uppercase string) |
| **xml** | `utf8` |

> [!NOTE]
> The driver converts the `datetimeoffset` type to UTC because Arrow columns require a fixed timezone. The driver normalizes per-cell timezone information from Microsoft SQL to UTC during conversion.
>
> The `sql_variant` type isn't supported by Arrow fetch methods and raises an unsupported data type exception. Use standard `fetchone()`, `fetchmany()`, or `fetchall()` for queries that return `sql_variant` columns.

## Performance considerations

Arrow fetch methods are fastest for analytics and bulk data operations, while standard cursor methods are better suited for transactional patterns with small result sets.

### When to use Arrow versus standard fetch

| Scenario | Recommended approach |
| --- | --- |
| Fetch a few rows for display | `fetchone()` / `fetchall()` |
| Load data into pandas or Polars | `cursor.arrow()` |
| Process large datasets in chunks | `cursor.arrow_reader()` |
| Single-row lookups or small result sets | `fetchone()` / `fetchval()` |
| Analytics or aggregation pipelines | `cursor.arrow()` + Polars/DuckDB |
| Write results to Parquet or Arrow IPC | `cursor.arrow_reader()` + PyArrow I/O |

### Memory management for large datasets

For result sets that might exceed available memory, use `arrow_reader()` with a reasonable `batch_size`.

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")

# Process in batches of 100K rows
reader = cursor.arrow_reader(batch_size=100000)
total_rows = 0

for batch in reader:
    # Work with each batch individually
    total_rows += batch.num_rows
    # batch goes out of scope and memory is freed

print(f"Processed {total_rows} rows")
```

### Tune batch size

The `batch_size` parameter controls how many rows are fetched in each batch. The optimal size depends on your row width and available memory. Wider rows with large columns like **nvarchar(max)** or **varbinary(max)** benefit from smaller batch sizes, while narrow rows benefit from larger ones.

- **Default (8192)**: Good balance for most workloads.
- **Smaller (1000-5000)**: Use for wide tables with large columns.
- **Larger (50000-100000)**: Use for narrow tables or when throughput matters more than memory.

```python
# Narrow table with many rows - use larger batches
cursor.execute("SELECT ProductID, ListPrice FROM Production.Product")
table = cursor.arrow(batch_size=100000)

# Wide table with LOB columns - use smaller batches
cursor.execute("SELECT * FROM Production.Document")
table = cursor.arrow(batch_size=1000)
```

## Related content

- [Use mssql-python with Polars](polars-integration.md)
- [Use mssql-python with pandas](pandas-integration.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
