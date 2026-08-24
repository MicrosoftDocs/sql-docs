---
title: Choose a Data Access and Analytics Pattern with mssql-python
description: Learn when to use cursor fetch, Arrow, pandas, Polars, or DuckDB for reading and analyzing data from Microsoft SQL with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# Choose a data access and analytics pattern with mssql-python

The `mssql-python` driver provides multiple paths for reading data from Microsoft SQL. Each path fits different workloads. This guide helps you choose the right one based on your data size, analysis needs, and performance requirements.

## Decide by workload

Use this table to find your starting point:

| Workload | Recommended path | Why |
| --- | --- | --- |
| Application row access (web API, CRUD) | [Cursor fetch methods](#cursor-fetch-methods) | Low overhead, row-at-a-time processing, no extra dependencies. |
| Small to medium reporting queries | [pandas](#pandas) | Familiar API for filtering, grouping, and visualization. |
| Large result sets or wide tables | [Arrow extraction](#arrow-extraction) | Zero-copy columnar transfer, minimal memory overhead. |
| High-performance analytics | [Polars with Arrow](#polars-with-arrow) | Multithreaded execution on columnar data, no GIL contention. |
| Ad hoc SQL over local and remote data | [DuckDB with Arrow](#duckdb-with-arrow) | SQL analytics on Arrow tables, join with local CSV/Parquet files. |
| Notebook exploration | [pandas](#pandas) or [Polars with Arrow](#polars-with-arrow) | Choose based on team familiarity and data size. |

## Cursor fetch methods

Use standard cursor methods when you need row-oriented access with no extra dependencies. This method is the right choice for application code that processes one row at a time, returns API responses, or feeds application logic.

```python
import mssql_python

conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)

cursor = conn.cursor()

# fetchone(): Process rows one at a time
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ListPrice > %(threshold)s", {"threshold": 100})
row = cursor.fetchone()
while row:
    print(f"{row.Name}: ${row.ListPrice:.2f}")
    row = cursor.fetchone()

# fetchmany(): Process in batches
cursor.execute("SELECT ProductID, Name FROM Production.Product")
while True:
    batch = cursor.fetchmany(100)
    if not batch:
        break
    for row in batch:
        print(row.Name)

# fetchval(): Get a single scalar value
cursor.execute("SELECT COUNT(*) FROM Production.Product")
count = cursor.fetchval()
```

Use `fetchmany()` for memory-efficient batch processing of large result sets. Use `fetchval()` when you need a single value like a count, max, or exists check.

For complete fetch method documentation, see [Retrieve data](retrieving-data.md).

## Arrow extraction

Use Arrow extraction when you need columnar data for analytics, DataFrame construction, or export to Parquet. Arrow provides zero-copy data transfer from the driver, which avoids the row-by-row conversion overhead of building a DataFrame from `fetchall()`.

Tables with columnstore indexes are already stored in columnar format in the database engine, making Arrow extraction a natural fit for those workloads.

```python
cursor.execute("""
    SELECT ProductID, Name, ListPrice, Color
    FROM Production.Product
    WHERE ListPrice > 0
""")

# Get a single Arrow table
arrow_table = cursor.arrow()
print(f"{arrow_table.num_rows} rows, {arrow_table.num_columns} columns")
```

For large result sets, use `arrow_reader()` to stream batches without loading everything into memory:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")

# Stream Arrow record batches
reader = cursor.arrow_reader(batch_size=10000)
for batch in reader:
    # Each batch is a pyarrow.RecordBatch
    print(f"Batch: {batch.num_rows} rows")
```

Arrow tables are the starting point for pandas, Polars, and DuckDB. Extract once, then convert:

```python
cursor.execute("""
    SELECT ProductID, Name, ListPrice, Color
    FROM Production.Product
    WHERE ListPrice > 0
""")
arrow_table = cursor.arrow()

# Arrow -> pandas
df = arrow_table.to_pandas()

# Arrow -> Polars (zero-copy)
import polars as pl
df = pl.from_arrow(arrow_table)
```

For complete Arrow documentation, see [Apache Arrow integration](arrow-integration.md).

## pandas

Use pandas when you need a familiar DataFrame API for reporting, ad hoc analysis, or data cleanup. pandas works best with result sets that fit in memory (up to a few million rows, depending on column width).

```python
cursor.execute("""
    SELECT p.Name, p.ListPrice, pc.Name AS Category
    FROM Production.Product p
    JOIN Production.ProductSubcategory ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
    JOIN Production.ProductCategory pc ON ps.ProductCategoryID = pc.ProductCategoryID
    WHERE p.ListPrice > 0
""")

import pandas as pd
rows = cursor.fetchall()
columns = [desc[0] for desc in cursor.description]
df = pd.DataFrame.from_records(rows, columns=columns)

# Analyze
print(df.groupby("Category")["ListPrice"].agg(["mean", "count"]))
```

For larger result sets, build the DataFrame from Arrow instead of `fetchall()`:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
arrow_table = cursor.arrow()
df = arrow_table.to_pandas()
```

For complete pandas patterns including ETL, time series, and write-back, see [pandas integration](pandas-integration.md).

## Polars with Arrow

Use Polars when you need faster DataFrame operations on larger result sets. Polars uses Apache Arrow as its memory format, so the transfer from `cursor.arrow()` is zero-copy. Polars also runs operations on multiple threads, which avoids GIL contention on CPU-heavy transformations.

```python
import polars as pl

cursor.execute("""
    SELECT ProductID, Name, ListPrice, Color
    FROM Production.Product
    WHERE ListPrice > 0
""")

arrow_table = cursor.arrow()
df = pl.from_arrow(arrow_table)

# Filter and aggregate
result = (
    df.filter(pl.col("ListPrice") > 100)
    .group_by("Color")
    .agg(pl.col("ListPrice").mean().alias("AvgPrice"))
    .sort("AvgPrice", descending=True)
)
print(result)
```

For streaming large result sets:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")

reader = cursor.arrow_reader(batch_size=50000)
frames = []
for batch in reader:
    frames.append(pl.from_arrow(batch))

df = pl.concat(frames)
```

For complete Polars patterns, see [Polars integration](polars-integration.md).

## DuckDB with Arrow

Use DuckDB when you need to run SQL analytics on extracted data, join server data with local CSV or Parquet files, or export results to file formats. DuckDB operates on Arrow tables with zero-copy access.

```python
import duckdb

cursor.execute("""
    SELECT ProductID, Name, ListPrice, Color
    FROM Production.Product
    WHERE ListPrice > 0
""")

products = cursor.arrow()

# Run DuckDB SQL on the Arrow table
result = duckdb.sql("""
    SELECT Color, AVG(ListPrice) AS AvgPrice, COUNT(*) AS Count
    FROM products
    WHERE Color IS NOT NULL
    GROUP BY Color
    ORDER BY AvgPrice DESC
""")
print(result.fetchdf())
```

Join server data with a local file:

```python
cursor.execute("SELECT CustomerID, TerritoryID FROM Sales.Customer")
customers = cursor.arrow()

# Join with a local CSV file
result = duckdb.sql("""
    SELECT c.CustomerID, c.TerritoryID, l.Region
    FROM customers c
    JOIN read_csv_auto('regions.csv') l ON c.TerritoryID = l.TerritoryID
""")
```

Export to Parquet:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
orders = cursor.arrow()

duckdb.sql("COPY orders TO 'orders.parquet' (FORMAT PARQUET)")
```

For complete DuckDB patterns, see [DuckDB integration](duckdb-integration.md).

## Microsoft SQL features that affect read-path decisions

The database engine has features that directly affect which read path works best. Consider these features when choosing your approach:

### Columnstore indexes

Tables with [columnstore indexes](../../../relational-databases/indexes/columnstore-indexes-overview.md) store data in columnar format. Arrow extraction is the natural handoff for these tables because the data is already columnar in the engine. If your analytics queries scan wide tables with millions of rows, a nonclustered columnstore index on the server side combined with Arrow extraction on the client side gives the best end-to-end throughput.

### Indexed views

[Indexed views](../../../relational-databases/views/create-indexed-views.md) precompute and store aggregated or joined results on the server. If your pandas or Polars analysis repeatedly computes the same aggregation, consider creating an indexed view and querying that view instead. The server automatically maintains the view as underlying data changes.

### Query Store

[Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) tracks query execution statistics over time. Use it to identify which queries are expensive enough to justify an Arrow extract and local DataFrame analysis versus a direct cursor read. If a query runs in milliseconds, cursor fetch is fine. If it scans millions of rows, Arrow extraction and local analysis might reduce server load.

### Intelligent query processing

Microsoft SQL's [intelligent query processing](../../../relational-databases/performance/intelligent-query-processing.md) features, such as adaptive joins, batch mode on rowstore, and memory grant feedback, automatically optimize query execution. These features work regardless of which client read path you choose, but they benefit large analytical queries the most. You don't need to tune hints or execution plans for most workloads.

## Stream large result sets

For result sets that don't fit in memory, use streaming patterns:

**Cursor-based streaming** with `fetchmany()`:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
while True:
    batch = cursor.fetchmany(5000)
    if not batch:
        break
    for row in batch:
        print(row[0])  # Process each row
```

**Arrow-based streaming** to Parquet:

```python
import pyarrow.parquet as pq

cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
reader = cursor.arrow_reader(batch_size=50000)
writer = None

for batch in reader:
    if writer is None:
        writer = pq.ParquetWriter("orders.parquet", batch.schema)
    writer.write_batch(batch)

if writer:
    writer.close()
```

## Anti-patterns to avoid

| Anti-pattern | Problem | Better approach |
| --- | --- | --- |
| `fetchall()` then `pd.DataFrame()` for large tables | Loads all rows into memory twice (once as tuples, once as DataFrame). | Use `cursor.arrow()` then `arrow_table.to_pandas()`. |
| Converting Arrow to pandas just to filter rows | Wastes memory on the full pandas copy. | Filter in SQL (`WHERE` clause) or use Polars/DuckDB on the Arrow table directly. |
| `SELECT *` when you need three columns | Transfers unnecessary data from the server. | List only the columns you need. |
| Building a DataFrame to compute `COUNT(*)` | The server computes aggregates faster than Python. | Use `SELECT COUNT(*)` and `fetchval()`. |
| Opening a new connection per query | Connection creation is expensive even with pooling overhead. | Reuse connections within a logical unit of work. |
| Chaining Arrow -> pandas -> Polars | Each conversion copies data. | Go directly to your target format: Arrow -> Polars or Arrow -> pandas. |

## Related content

- [Retrieve data with mssql-python](retrieving-data.md)
- [Use mssql-python with Apache Arrow](arrow-integration.md)
- [Use mssql-python with pandas](pandas-integration.md)
- [Use mssql-python with Polars](polars-integration.md)
- [Use mssql-python with DuckDB](duck-db-integration.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
- [Choose a data loading and movement pattern with mssql-python](data-loading-movement-patterns.md)
