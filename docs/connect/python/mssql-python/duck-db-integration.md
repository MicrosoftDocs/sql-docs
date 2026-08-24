---
title: Use mssql-python with DuckDB
description: Learn how to integrate the mssql-python driver with DuckDB for in-process SQL analytics on Microsoft SQL data using Apache Arrow.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with DuckDB

DuckDB is an in-process SQL analytics engine that can query Apache Arrow tables directly without copying data. Combining DuckDB with the mssql-python driver lets you:

- Run analytical SQL queries on Microsoft SQL result sets without loading data into pandas or Polars.
- Query Arrow tables in memory with zero-copy overhead.
- Join Microsoft SQL data with local files (CSV, Parquet, JSON) in a single DuckDB query.
- Export Microsoft SQL data to Parquet, CSV, or other formats through DuckDB.

## Prerequisites

- Python 3.10 or later.
- The `mssql-python`, `duckdb`, and `pyarrow` packages. Install all with `pip install mssql-python duckdb pyarrow`.
- [!INCLUDE [prereq-linux-macos](includes/prereq-linux-macos.md)]

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

The examples in this article query the `AdventureWorks` sample database. If you don't already have it, see [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure).

## Install dependencies

```bash
pip install mssql-python duckdb pyarrow
```

## Query Microsoft SQL data with DuckDB

The basic workflow is: execute a query with mssql-python, fetch the results as an Arrow table, then query that Arrow table with DuckDB SQL.

### Basic pattern

Start by establishing a connection and fetching data as an Arrow table.

```python
import duckdb
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes"
)
cursor = conn.cursor()
```

```python
# Fetch Microsoft SQL data as Arrow
cursor.execute("SELECT * FROM Production.Product WHERE ListPrice > 0")
products = cursor.arrow()

# Query the Arrow table with DuckDB
result = duckdb.sql("""
    SELECT Color, COUNT(*) AS ProductCount, AVG(ListPrice) AS AvgPrice
    FROM products
    GROUP BY Color
    ORDER BY ProductCount DESC
""")
print(result.fetchdf())
```

DuckDB references the `products` Arrow table by its Python variable name. No data is copied into DuckDB's storage.

### Aggregate and filter

Use DuckDB's SQL to group and aggregate Arrow data.

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
orders = cursor.arrow()

# Top customers by total spend
top_customers = duckdb.sql("""
    SELECT
        CustomerID,
        COUNT(*) AS OrderCount,
        SUM(TotalDue) AS TotalSpent,
        AVG(TotalDue) AS AvgOrderValue
    FROM orders
    GROUP BY CustomerID
    HAVING SUM(TotalDue) > 10000
    ORDER BY TotalSpent DESC
    LIMIT 20
""")
print(top_customers.fetchdf())
```

### Join multiple Microsoft SQL results

Fetch multiple tables from Microsoft SQL and join them in DuckDB without writing a cross-server query.

```python
# Fetch two tables
cursor.execute("SELECT * FROM Production.Product")
products = cursor.arrow()

cursor.execute("SELECT * FROM Production.ProductSubcategory")
subcategories = cursor.arrow()

# Join in DuckDB
result = duckdb.sql("""
    SELECT
        s.Name AS Subcategory,
        COUNT(*) AS ProductCount,
        ROUND(AVG(p.ListPrice), 2) AS AvgPrice
    FROM products p
    JOIN subcategories s ON p.ProductSubcategoryID = s.ProductSubcategoryID
    GROUP BY s.Name
    ORDER BY AvgPrice DESC
""")
print(result.fetchdf())
```

## Join Microsoft SQL data with local files

DuckDB can read CSV, Parquet, and JSON files natively. Combine SQL Server data with local files in a single query.

### Join with a CSV file

Load a CSV file and join it with data from Microsoft SQL.

```python
import csv
from pathlib import Path

cursor.execute("SELECT CustomerID, PersonID FROM Sales.Customer")
customers = cursor.arrow()

csv_path = Path("customer_regions.csv")
with csv_path.open("w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)
    writer.writerow(["CustomerID", "Region", "Segment"])
    writer.writerows([
        (1, "West", "Premium"),
        (2, "East", "Standard"),
        (3, "Central", "Basic"),
    ])

try:
    result = duckdb.sql("""
        SELECT c.CustomerID, c.PersonID, f.Region, f.Segment
        FROM customers c
        JOIN read_csv_auto('customer_regions.csv') f ON c.CustomerID = f.CustomerID
    """)
    print(result.fetchdf())
finally:
    csv_path.unlink(missing_ok=True)
```

### Join with a Parquet file

Load a Parquet file and join it with data from Microsoft SQL.

```python
from pathlib import Path

import pyarrow as pa
import pyarrow.parquet as pq

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")
products = cursor.arrow()

parquet_path = Path("order_history.parquet")
order_history = pa.table({
    "ProductID": [1, 2, 680],
    "OrderDate": ["2024-06-01", "2024-03-15", "2024-01-10"],
    "Quantity": [10, 5, 3],
})
pq.write_table(order_history, parquet_path)

try:
    result = duckdb.sql("""
        SELECT p.Name, p.ListPrice, h.OrderDate, h.Quantity
        FROM products p
        JOIN read_parquet('order_history.parquet') h ON p.ProductID = h.ProductID
        WHERE h.OrderDate >= '2024-01-01'
    """)
    print(result.fetchdf())
finally:
    parquet_path.unlink(missing_ok=True)
```

## Export Microsoft SQL data

Use DuckDB's `COPY` statement to export Microsoft SQL data to various file formats.

### Export to Parquet

Export data to Apache Parquet format.

```python
cursor.execute("SELECT * FROM Production.Product")
products = cursor.arrow()

duckdb.sql("COPY products TO 'products.parquet' (FORMAT PARQUET)")
```

### Export to CSV

Export data to a comma-separated values file:

```python
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
orders = cursor.arrow()

duckdb.sql("COPY orders TO 'orders.csv' (FORMAT CSV, HEADER)")
```

### Export partitioned Parquet

Export data to partitioned Parquet files for distributed analytics:

```python
import shutil
from pathlib import Path

cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
orders = cursor.arrow()

output_dir = Path("sales_data")
shutil.rmtree(output_dir, ignore_errors=True)

duckdb.sql("""
    COPY (SELECT *, YEAR(OrderDate) AS OrderYear FROM orders)
    TO 'sales_data'
    (FORMAT PARQUET, PARTITION_BY (OrderYear))
""")
```

## Stream large result sets

For large datasets, use `arrow_reader()` to process data in streaming batches without loading all rows into memory at once:

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")
reader = cursor.arrow_reader(batch_size=50000)

# Process each batch with DuckDB
total_rows = 0
for batch in reader:
    result = duckdb.sql("""
        SELECT ProductID, SUM(ActualCost) AS TotalCost
        FROM batch
        GROUP BY ProductID
    """)
    total_rows += batch.num_rows
    print(f"Processed {total_rows} rows")
```

### Accumulate streaming results

To aggregate across all batches, register each batch in a persistent DuckDB connection and accumulate results incrementally.

```python
cursor.execute("SELECT * FROM Production.TransactionHistory")
reader = cursor.arrow_reader(batch_size=50000)

duck = duckdb.connect()
duck.execute("CREATE TABLE transactions (ProductID INT, ActualCost DOUBLE, Quantity INT)")

for batch in reader:
    duck.execute("INSERT INTO transactions SELECT ProductID, ActualCost, Quantity FROM batch")

# Query the accumulated data
result = duck.sql("""
    SELECT ProductID, SUM(ActualCost) AS TotalCost, SUM(Quantity) AS TotalQty
    FROM transactions
    GROUP BY ProductID
    ORDER BY TotalCost DESC
    LIMIT 10
""")
print(result.fetchdf())
duck.close()
```

## Performance tips

### Let Microsoft SQL handle heavy lifting

Microsoft SQL is faster for filtering, joins, and aggregations than pulling all raw data over the wire. Use DuckDB for secondary analysis on result sets that are already fetched, not as a replacement for SQL Server query optimization.

```python
# Suboptimal: Pull all rows, filter in DuckDB
cursor.execute("SELECT * FROM Sales.SalesOrderHeader")
orders = cursor.arrow()
result = duckdb.sql("SELECT * FROM orders WHERE TotalDue > 1000")

# Better: Filter in Microsoft SQL, analyze in DuckDB
cursor.execute("SELECT * FROM Sales.SalesOrderHeader WHERE TotalDue > 1000")
orders = cursor.arrow()
result = duckdb.sql("SELECT CustomerID, SUM(TotalDue) FROM orders GROUP BY CustomerID")
```

### Use Arrow for all read operations

Arrow-based transfer avoids creating intermediate Python objects, which reduces memory usage and improves throughput. Prefer `cursor.arrow()` over manual row-by-row conversion when passing data to DuckDB.

### Use streaming for large datasets

For result sets larger than available memory, use `arrow_reader()` with a `batch_size` parameter to process data incrementally.

## Related content

- [Use mssql-python with Apache Arrow](arrow-integration.md)
- [Use mssql-python with pandas](pandas-integration.md)
- [Use mssql-python with Polars](polars-integration.md)
- [Use bulk copy with mssql-python](bulk-copy.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
