---
title: Choose a Data Loading and Movement Pattern with mssql-python
description: Learn when to use single inserts, batched writes, bulk copy, or MERGE for loading data into Microsoft SQL with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# Choose a data loading and movement pattern with mssql-python

The `mssql-python` driver provides multiple paths for writing data into Microsoft SQL. Each path fits different workloads. This guide helps you choose the right one based on your data volume, source format, and update semantics.

## Decide by workload

| Workload | Recommended path | Why |
| --- | --- | --- |
| Load CSV files into a table | [Load CSV data with bulk copy](#load-csv-data-with-bulk-copy) | `bulkcopy()` with a generator handles files of any size without loading them into memory. |
| Insert a single row from application code | [Single row inserts](#single-row-inserts) | Low overhead, straightforward error handling, works with OUTPUT for returning generated keys. |
| Insert a small-to-moderate batch from application code | [Batched inserts](#batched-inserts) | Reduces round trips compared to single inserts. |
| Load hundreds of rows or more from any source | [Bulk copy](#bulk-copy) | TDS bulk insert is the most efficient path for large volumes. |
| Insert or update rows based on a key | [Upsert with MERGE](#upsert-with-merge) | `MERGE` handles INSERT, UPDATE, and DELETE in one statement. |
| Load a DataFrame into a table | [Load DataFrames](#load-dataframes) | Extract rows from pandas or Polars and feed to `bulkcopy()`. |
| Stage data through Parquet files | [Parquet staging](#parquet-staging) | Useful for cross-system ETL where an intermediate file format is needed. |

## Load CSV data with bulk copy

Loading CSV data is the most common ingest question for Python database work. Use `csv.reader` with a generator feeding `bulkcopy()`:

```python
import csv
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create a target table
cursor.execute("""
    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductImport')
    CREATE TABLE dbo.ProductImport (
        Name nvarchar(100),
        ProductNumber nvarchar(25),
        ListPrice decimal(10,2)
    )
""")
conn.commit()

def csv_rows(path):
    with open(path, newline="", encoding="utf-8") as f:
        reader = csv.reader(f)
        next(reader)  # Skip header
        for row in reader:
            yield (row[0], row[1], float(row[2]))

result = cursor.bulkcopy(
    "dbo.ProductImport",
    csv_rows("products.csv"),
    batch_size=5000
)
print(f"Loaded {result['rows_copied']} rows")
conn.commit()
```

The generator pattern keeps memory usage constant regardless of file size. For column mapping and identity handling, see [Bulk copy operations](bulk-copy.md).

## Single row inserts

Use single inserts for application-level writes where you process one record at a time. Use `OUTPUT INSERTED` to retrieve generated keys:

```python
cursor.execute("""
    INSERT INTO dbo.ProductImport (Name, ProductNumber, ListPrice)
    OUTPUT INSERTED.Name
    VALUES (%(name)s, %(product_number)s, %(list_price)s)
""", {"name": "Widget", "product_number": "WG-1000", "list_price": 19.99})

inserted_name = cursor.fetchval()
conn.commit()
```

Single inserts are the right choice when:

- You insert one row per user action (form submission, API call).
- You need to validate or transform each row individually before inserting.
- You need the inserted ID or other generated values immediately.

## Batched inserts

Use `executemany()` when you have a moderate number of rows and don't need bulk copy's throughput:

```python
rows = [
    {"name": "Widget A", "product_number": "WG-1001", "list_price": 19.99},
    {"name": "Widget B", "product_number": "WG-1002", "list_price": 24.99},
    {"name": "Widget C", "product_number": "WG-1003", "list_price": 29.99},
]

cursor.executemany(
    "INSERT INTO dbo.ProductImport (Name, ProductNumber, ListPrice) VALUES (%(name)s, %(product_number)s, %(list_price)s)",
    rows
)
conn.commit()
```

`executemany()` sends each row as a separate parameterized statement. When throughput matters more than per-row control, `bulkcopy()` is more efficient because it uses the TDS bulk insert protocol. The crossover depends on row width and network latency, but it's typically in the low hundreds of rows.

## Bulk copy

When throughput matters more than per-row control, use `bulkcopy()`. It uses the TDS bulk insert protocol, which is significantly more efficient than row-by-row inserts:

```python
rows = [
    ("Widget A", "WG-1001", 19.99),
    ("Widget B", "WG-1002", 24.99),
    ("Widget C", "WG-1003", 29.99),
]

result = cursor.bulkcopy("dbo.ProductImport", rows, batch_size=5000)
print(f"Loaded {result['rows_copied']} rows")
conn.commit()
```

### Performance tips for bulk copy

- **Use generators** for large datasets to keep memory usage constant.
- **Set `batch_size`** to control how many rows are sent per TDS batch. Start with 5,000 and adjust based on row width.
- **Use table locks** for exclusive loads: `cursor.bulkcopy("dbo.ProductImport", rows, table_lock=True)`.
- **Disable indexes** before loading, then rebuild after. This sequence avoids index maintenance overhead during the load.

For column mappings, identity columns, NULL handling, and parallel loading, see [Bulk copy operations](bulk-copy.md).

## Upsert with MERGE

`MERGE` is Microsoft SQL's statement for conditional INSERT, UPDATE, and DELETE in a single operation. It handles the "insert if new, update if exists" pattern that Python developers commonly need.

### Single-row upsert

For a single row, use `MERGE` with a `USING` clause that defines parameter aliases:

```python
cursor.execute("""
    MERGE dbo.ProductImport AS target
    USING (SELECT %(name)s AS Name, %(product_number)s AS ProductNumber, %(list_price)s AS ListPrice) AS source
    ON target.ProductNumber = source.ProductNumber
    WHEN MATCHED THEN
        UPDATE SET
            Name = source.Name,
            ListPrice = source.ListPrice
    WHEN NOT MATCHED THEN
        INSERT (Name, ProductNumber, ListPrice)
        VALUES (source.Name, source.ProductNumber, source.ListPrice);
""", {"name": "Widget A", "product_number": "WG-1001", "list_price": 24.99})
conn.commit()
```

### Bulk upsert with a staging table

For bulk upserts, stage data into a temp table first, then use `MERGE` to update from it. Use insert-or-update as the default pattern for DataFrame upserts and batch updates:

```python
import csv
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Step 1: Create a global temp table for staging
# Note: bulkcopy() requires global temp tables (##), not session temp tables (#)
cursor.execute("""
    IF OBJECT_ID('tempdb..##ProductImportStage') IS NOT NULL
        DROP TABLE ##ProductImportStage;
    CREATE TABLE ##ProductImportStage (
        Name nvarchar(100),
        ProductNumber nvarchar(25),
        ListPrice decimal(10,2)
    )
""")
cursor.commit()

# Step 2: Bulk load into the staging table
def csv_rows(path):
    with open(path, newline="", encoding="utf-8") as f:
        reader = csv.reader(f)
        next(reader)
        for row in reader:
            yield (row[0], row[1], float(row[2]))

cursor.bulkcopy("##ProductImportStage", csv_rows("products_update.csv"), batch_size=5000)

# Step 3: MERGE from staging into the target table
cursor.execute("""
    MERGE dbo.ProductImport AS target
    USING ##ProductImportStage AS source
    ON target.ProductNumber = source.ProductNumber
    WHEN MATCHED THEN
        UPDATE SET
            Name = source.Name,
            ListPrice = source.ListPrice
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (Name, ProductNumber, ListPrice)
        VALUES (source.Name, source.ProductNumber, source.ListPrice)
    OUTPUT $action, INSERTED.ProductNumber, DELETED.ProductNumber;
""")

# Step 4: Read the OUTPUT to see what changed
for row in cursor.fetchall():
    print(f"{row[0]}: inserted={row[1]}, deleted={row[2]}")

conn.commit()
```

This example demonstrates the default insert-or-update pattern:

- **INSERT** rows from the source that don't exist in the target (`WHEN NOT MATCHED BY TARGET`).
- **UPDATE** rows that exist in both (`WHEN MATCHED`).
- **OUTPUT** clause reports what action was taken on each row, which is useful for audit trails.

> [!CAUTION]
> Add `WHEN NOT MATCHED BY SOURCE THEN DELETE` only when the staging data is an authoritative full snapshot of the target. If the batch contains only changed rows, that clause deletes rows that were intentionally omitted from the source feed.

If you need full reconciliation, extend the `MERGE` only after you confirm the source is authoritative for the target table:

```sql
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
```

In shared environments, use a unique global temp table name per run or a permanent staging table to avoid collisions between concurrent jobs.

### When to use separate UPDATE and INSERT statements instead

`MERGE` is powerful but has edge cases. Consider using separate statements when:

- You don't need DELETE logic. A separate `UPDATE` followed by `INSERT WHERE NOT EXISTS` is more readable and straightforward to debug.
- The `MERGE` statement is complex enough that the locking behavior is hard to predict. Separate statements give you explicit control over lock granularity.
- You're updating a high-concurrency table where `MERGE` lock escalation could cause blocking.

```python
# Simpler alternative: UPDATE then INSERT
cursor.execute("""
    UPDATE dbo.ProductImport
    SET Name = %(name)s, ListPrice = %(list_price)s
    WHERE ProductNumber = %(product_number)s
""", {"name": "Widget A", "list_price": 24.99, "product_number": "WG-1001"})

if cursor.rowcount == 0:
    cursor.execute("""
        INSERT INTO dbo.ProductImport (Name, ProductNumber, ListPrice)
        VALUES (%(name)s, %(product_number)s, %(list_price)s)
    """, {"name": "Widget A", "product_number": "WG-1001", "list_price": 24.99})

conn.commit()
```

## Load DataFrames

Extract rows from a pandas or Polars DataFrame and load them by using `bulkcopy()`:

### pandas

Convert a pandas DataFrame to tuples and pass to `bulkcopy()`:

```python
import pandas as pd

df = pd.read_csv("products.csv")

# Convert DataFrame rows to tuples
rows = list(df[["Name", "ProductNumber", "ListPrice"]].itertuples(index=False, name=None))

cursor.bulkcopy("dbo.ProductImport", rows, batch_size=5000)
conn.commit()
```

### Polars

Convert a Polars DataFrame to tuples using the `.rows()` method:

```python
import polars as pl

df = pl.read_csv("products.csv")

# Convert Polars DataFrame to list of tuples
rows = df.select(["Name", "ProductNumber", "ListPrice"]).rows()

cursor.bulkcopy("dbo.ProductImport", rows, batch_size=5000)
conn.commit()
```

For complete DataFrame loading patterns, see [pandas integration](pandas-integration.md) and [Polars integration](polars-integration.md).

## Parquet staging

Use Parquet as an intermediate format when migrating data between systems or when your ETL pipeline already produces Parquet files:

```python
import pyarrow.parquet as pq

# Read Parquet file
table = pq.read_table("products.parquet")

# Convert to rows for bulkcopy
rows = [tuple(row) for row in zip(*[col.to_pylist() for col in table.columns])]

cursor.bulkcopy("dbo.ProductImport", rows, batch_size=5000)
conn.commit()
```

For large Parquet files, read in row groups to keep memory usage constant:

```python
import pyarrow.parquet as pq

parquet_file = pq.ParquetFile("products.parquet")

for batch in parquet_file.iter_batches(batch_size=10000):
    rows = [tuple(row) for row in zip(*[col.to_pylist() for col in batch.columns])]
    cursor.bulkcopy("dbo.ProductImport", rows, batch_size=10000)

conn.commit()
```

## Validate loaded data

After loading, verify row counts and spot-check data:

```python
cursor.execute("SELECT COUNT(*) FROM dbo.ProductImport")
count = cursor.fetchval()
print(f"Total rows: {count}")

cursor.execute("""
    SELECT TOP 5 Name, ProductNumber, ListPrice
    FROM dbo.ProductImport
    ORDER BY Name
""")
for row in cursor:
    print(f"  {row.Name} ({row.ProductNumber}): ${row.ListPrice:.2f}")
```

For production loads, don't rely on the calling connection's transaction to protect a `bulkcopy()` call. `bulkcopy()` opens its own internal connection and commits the copied rows independently, so a `conn.rollback()` on your main connection can't undo them. Two approaches give you atomicity:

- Set `use_internal_transaction=True` to wrap each batch in its own transaction. A batch that fails partway rolls back that batch instead of leaving it half-loaded.
- To validate data before promoting it, bulk copy into a staging table, validate it, then move the rows into the target table by using an `INSERT ... SELECT` inside a transaction on your main connection. Because that `INSERT` runs on your connection, `conn.rollback()` undoes it if validation fails.  

```python
# Stage the data. bulkcopy() runs on its own connection, so these rows
# persist regardless of the transaction below.
cursor.bulkcopy("dbo.ProductImport_Stage", rows, batch_size=5000)

try:
    cursor.execute("SELECT COUNT(*) FROM dbo.ProductImport_Stage")
    count = cursor.fetchval()

    if count < expected_count:
        raise ValueError(f"Expected {expected_count} rows, got {count}")

    # This INSERT runs on your connection, so it's covered by the transaction.
    cursor.execute("""
        INSERT INTO dbo.ProductImport (Name, ProductNumber, ListPrice)
        SELECT Name, ProductNumber, ListPrice FROM dbo.ProductImport_Stage
    """)
    conn.commit()
except Exception:
    conn.rollback()
    raise
```

## Related content

- [Bulk copy operations](bulk-copy.md)
- [pandas integration](pandas-integration.md)
- [Polars integration](polars-integration.md)
- [Performance tuning](performance-tuning.md)
- [Transaction management](transaction-management.md)
- [Data access and analytics patterns](data-access-analytics-patterns.md)
