---
title: Use mssql-python with Polars
description: Learn how to integrate the mssql-python driver with Polars DataFrames for high-performance data analysis with Microsoft SQL and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with Polars

Polars is a high-performance DataFrame library written in Rust that provides a fast, memory-efficient alternative to pandas. Polars combined with the mssql-python driver lets you:

- Load SQL query results directly into Polars DataFrames.
- Use Apache Arrow for zero-copy data transfer from Microsoft SQL.
- Write Polars DataFrames back to Microsoft SQL efficiently.
- Build high-performance data pipelines with lazy evaluation.

The examples in this article query the `AdventureWorks` sample database. If you don't already have it, see [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure).

## Read data into Polars DataFrames

You can load Microsoft SQL data into Polars two ways: row-by-row conversion through standard cursor methods, or zero-copy transfer through Apache Arrow. "Zero-copy" means the data stays in a single memory buffer that the driver, Arrow, and Polars all read directly, so no rows get duplicated into intermediate Python objects. Use the Arrow approach for most workloads because of this efficiency.

### Basic query to DataFrame

This approach fetches all rows with the standard cursor and manually builds a Polars DataFrame. It accepts [parameterized queries](parameterized-queries.md) for safe value substitution. It works without PyArrow but is slower for large result sets because every value passes through Python.

```python
import polars as pl
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()


def query_to_polars(cursor, query: str, params: dict = None) -> pl.DataFrame:
    """Execute query and return results as Polars DataFrame."""
    cursor.execute(query, params or {})

    columns = [col[0] for col in cursor.description]
    rows = cursor.fetchall()

    data = {col: [row[i] for row in rows] for i, col in enumerate(columns)}
    return pl.DataFrame(data)

# Usage: %(color)s is a parameterized placeholder. The driver safely substitutes
# the value from the dict, which prevents SQL injection.
df = query_to_polars(cursor, "SELECT TOP 5 Name, ListPrice FROM Production.Product WHERE Color = %(color)s", {"color": "Black"})
print(df)
```

> [!NOTE]
> If your connection string uses `Authentication=ActiveDirectoryDefault`, the driver uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

### Use Arrow for zero-copy transfer (recommended)

The most efficient way to load Microsoft SQL data into Polars is through Apache Arrow. The mssql-python driver's `arrow()` method returns a `pyarrow.Table` that Polars can consume with zero-copy overhead.

```python
def query_to_polars_arrow(cursor, query: str, params: dict = None) -> pl.DataFrame:
    """Execute query and load results through Arrow for best performance."""
    cursor.execute(query, params or {})
    arrow_table = cursor.arrow()
    return pl.from_arrow(arrow_table)

# Usage
df = query_to_polars_arrow(cursor, "SELECT ProductID, Name, ListPrice FROM Production.Product")
print(df)
```

### Stream large datasets with Arrow batches

For datasets that don't fit in memory, use `arrow_reader()` to process data in streaming batches. Each batch is a `pyarrow.RecordBatch` that Polars can consume independently, so memory usage stays proportional to `batch_size` rather than the full result set.

```python
def process_large_query(cursor, query: str, params: dict = None, batch_size: int = 50000) -> pl.DataFrame:
    """Process large query results as streaming Arrow batches."""
    cursor.execute(query, params or {})
    reader = cursor.arrow_reader(batch_size=batch_size)

    results = []
    for batch in reader:
        chunk_df = pl.from_arrow(batch)
        # Process each chunk
        results.append(chunk_df)

    return pl.concat(results) if results else pl.DataFrame()

# Usage
df = process_large_query(cursor, "SELECT * FROM Production.TransactionHistory")
```

### Use LazyFrames for deferred execution

Polars LazyFrames let you build a chain of operations (filter, group, sort) without running them immediately. Polars optimizes the full chain before executing, which can be faster than applying each step individually.

```python
def query_to_lazy(cursor, query: str, params: dict = None) -> pl.LazyFrame:
    """Execute query and return a Polars LazyFrame."""
    cursor.execute(query, params or {})
    arrow_table = cursor.arrow()
    return pl.from_arrow(arrow_table).lazy()

# Build a query plan without executing immediately
lf = query_to_lazy(cursor, "SELECT SalesOrderID, CustomerID, TotalDue, OrderDate FROM Sales.SalesOrderHeader")
result = (
    lf.filter(pl.col("TotalDue") > 100)
    .group_by("CustomerID")
    .agg([
        pl.col("TotalDue").sum().alias("TotalSpent"),
        pl.col("SalesOrderID").count().alias("OrderCount")
    ])
    .sort("TotalSpent", descending=True)
    .collect()  # Execute the optimized plan
)
print(result)
```

## Write Polars DataFrames to Microsoft SQL

### Quote identifiers to prevent SQL injection

Table and column names can't be passed as query parameters in SQL. When you build SQL statements with dynamic identifiers, wrap each name in square brackets and escape any embedded `]` characters to prevent SQL injection.

```python
def quote_id(identifier: str) -> str:
    """Quote a Microsoft SQL identifier to prevent SQL injection.
    
    Wraps the name in square brackets and escapes any embedded ] characters.
    Raises ValueError if the identifier is empty or contains null bytes.
    """
    if not identifier or "\x00" in identifier:
        raise ValueError(f"Invalid identifier: {identifier!r}")
    escaped = identifier.replace("]", "]]")
    return f"[{escaped}]"
```

The helper functions in this section use `quote_id()` for all table and column names in generated SQL.

### Insert DataFrame rows

The row-by-row approach iterates over the DataFrame with `iter_rows(named=True)` and executes one `INSERT` per row. This approach is straightforward but slow for large volumes because each row requires a round trip to the server.

```python
def polars_to_sql(cursor, conn, df: pl.DataFrame, table: str) -> int:
    """Write Polars DataFrame to Microsoft SQL table."""
    columns = df.columns
    placeholders = ", ".join([f"%({col})s" for col in columns])
    col_list = ", ".join([quote_id(col) for col in columns])
    query = f"INSERT INTO {quote_id(table)} ({col_list}) VALUES ({placeholders})"

    rows_inserted = 0
    for row in df.iter_rows(named=True):
        params = {k: (None if v is None else v) for k, v in row.items()}
        cursor.execute(query, params)
        rows_inserted += 1

    conn.commit()
    return rows_inserted

# Usage
cursor.execute("CREATE TABLE #PolarsInsert (Name NVARCHAR(100), Price DECIMAL(10,2), CategoryID INT)")
df = pl.DataFrame({
    "Name": ["Product A", "Product B"],
    "Price": [29.99, 49.99],
    "CategoryID": [1, 2]
})
rows = polars_to_sql(cursor, conn, df, "#PolarsInsert")
print(f"Inserted {rows} rows")
```

### Bulk insert (recommended for large DataFrames)

For large DataFrames, use the driver's `bulkcopy()` method to send rows in bulk over the TDS (Tabular Data Stream) protocol, the native wire protocol Microsoft SQL uses. This approach minimizes round trips and is faster than row-by-row inserts.

```python
def polars_to_sql_bulk(conn, df: pl.DataFrame, table: str) -> int:
    """Bulk insert Polars DataFrame using BCP for best performance."""
    rows = [tuple(None if v is None else v for v in row) for row in df.iter_rows()]

    cursor = conn.cursor()
    result = cursor.bulkcopy(table, rows)
    conn.commit()
    return result["rows_copied"]

# Usage
cursor.execute("CREATE TABLE ##PolarsBulk (Name NVARCHAR(50), Price FLOAT, CategoryID INT)")
conn.commit()
df = pl.DataFrame({
    "Name": ["Product A", "Product B", "Product C"],
    "Price": [29.99, 49.99, 19.99],
    "CategoryID": [1, 2, 1]
})
rows = polars_to_sql_bulk(conn, df, "##PolarsBulk")
print(f"Bulk inserted {rows} rows")
```

## Data analysis patterns

The following examples show common analysis tasks that combine Microsoft SQL queries with Polars transformations.

### Aggregate queries

This example groups products by subcategory and computes count and price statistics in SQL, then loads the summary into a Polars DataFrame:

```python
def get_sales_summary(cursor) -> pl.DataFrame:
    """Get sales summary by subcategory."""
    cursor.execute("""
        SELECT
            sc.Name AS SubcategoryName,
            COUNT(*) AS ProductCount,
            AVG(p.ListPrice) AS AvgPrice,
            MIN(p.ListPrice) AS MinPrice,
            MAX(p.ListPrice) AS MaxPrice
        FROM Production.Product p
        JOIN Production.ProductSubcategory sc ON p.ProductSubcategoryID = sc.ProductSubcategoryID
        GROUP BY sc.Name
        ORDER BY ProductCount DESC
    """)
    return pl.from_arrow(cursor.arrow())

df = get_sales_summary(cursor)
print(df)
```

### Time series analysis

Load time series data from Microsoft SQL and add computed columns like rolling averages using Polars expressions.

```python
def get_daily_sales(cursor, start_date: str, end_date: str) -> pl.DataFrame:
    """Get daily sales and compute rolling statistics."""
    cursor.execute("""
        SELECT
            CAST(OrderDate AS DATE) AS Date,
            COUNT(*) AS OrderCount,
            SUM(TotalDue) AS Revenue
        FROM Sales.SalesOrderHeader
        WHERE OrderDate BETWEEN %(start)s AND %(end)s
        GROUP BY CAST(OrderDate AS DATE)
        ORDER BY Date
    """, {"start": start_date, "end": end_date})

    df = pl.from_arrow(cursor.arrow())

    # Add rolling 7-day average
    df = df.with_columns(
        pl.col("Revenue").rolling_mean(window_size=7).alias("RollingAvg")
    )
    return df

sales_df = get_daily_sales(cursor, "2013-01-01", "2013-12-31")
print(sales_df)
```

### Join SQL data with local files

You can enrich Microsoft SQL data by joining it with local CSV files in Polars. Load each source into a DataFrame and join in memory.

```python
# Load SQL data via Arrow
cursor.execute("SELECT c.CustomerID, p.FirstName, p.LastName FROM Sales.Customer c JOIN Person.Person p ON c.PersonID = p.BusinessEntityID")
customers = pl.from_arrow(cursor.arrow())

# Load local CSV
orders = pl.read_csv("orders_export.csv")

# Join in Polars
result = customers.join(orders, on="CustomerID", how="inner")
print(result)
```

## ETL patterns

Build extract, transform, and load (ETL) pipelines by combining Microsoft SQL queries with Polars transformations. Polars expressions handle the transformation step, and `bulkcopy()` handles loading.

### Extract, transform, load

This example extracts active customer data through Arrow, applies business segmentation logic with Polars expressions, and loads the results using bulk copy.

```python
def etl_pipeline(source_cursor, dest_conn):
    """ETL pipeline using Polars transformations."""

    # Extract: derive a per-customer summary from order history via Arrow
    source_cursor.execute("""
        SELECT
            CustomerID,
            COUNT(*) AS OrderCount,
            SUM(TotalDue) AS TotalSpent
        FROM Sales.SalesOrderHeader
        WHERE OrderDate > DATEADD(YEAR, -1, (SELECT MAX(OrderDate) FROM Sales.SalesOrderHeader))
        GROUP BY CustomerID
    """)
    df = pl.from_arrow(source_cursor.arrow())
    df = df.with_columns(pl.col("TotalSpent").cast(pl.Float64))

    # Transform with Polars expressions
    df = df.with_columns([
        pl.when(pl.col("TotalSpent") > 1000).then(pl.lit("Platinum"))
          .when(pl.col("TotalSpent") > 500).then(pl.lit("Gold"))
          .when(pl.col("TotalSpent") > 100).then(pl.lit("Silver"))
          .otherwise(pl.lit("Bronze"))
          .alias("CustomerSegment"),
        (pl.col("TotalSpent") / pl.col("OrderCount").clip(lower_bound=1))
          .alias("AvgOrderValue"),
        (pl.col("TotalSpent") > 500).alias("IsHighValue")
    ])

    # Load via bulk copy into the destination table
    dest_cursor = dest_conn.cursor()
    dest_cursor.execute("""
        CREATE TABLE ##CustomerAnalytics (
            CustomerID INT,
            CustomerSegment NVARCHAR(20),
            AvgOrderValue FLOAT,
            IsHighValue BIT
        )
    """)
    dest_conn.commit()

    load_df = df.select(["CustomerID", "CustomerSegment", "AvgOrderValue", "IsHighValue"])
    polars_to_sql_bulk(dest_conn, load_df, "##CustomerAnalytics")

    return len(df)
```

## Performance tips

The following tips help you get the most out of the mssql-python and Polars combination.

### Let Microsoft SQL handle heavy lifting

Microsoft SQL is faster for aggregations, filtering, and joins than pulling all of your raw data over the wire and processing locally in Python. Let Microsoft SQL do the heavy lifting whenever possible, move only the data you need across the network and use Polars for analysis and transformations that are more convenient in Python.

```python
# Avoid: pulling all rows over the wire to aggregate locally in Polars
df = query_to_polars_arrow(cursor, "SELECT * FROM Production.Product WHERE Color IS NOT NULL")  # transfers entire table
summary = df.group_by("Color").agg(pl.col("ListPrice").sum())  # aggregation that SQL can do faster

# Better: push the aggregation into SQL and transfer only the summary
df = query_to_polars_arrow(cursor, """
    SELECT Color AS Category, SUM(ListPrice) AS TotalAmount
    FROM Production.Product
    WHERE Color IS NOT NULL
    GROUP BY Color
""")
```

### Use Arrow for all read operations

Arrow-based transfer avoids creating intermediate Python objects, which reduces memory usage and improves throughput. Prefer `cursor.arrow()` over manual row-by-row conversion for any result set larger than a few rows.

```python
# Suboptimal: Row-by-row conversion
cursor.execute("SELECT * FROM Production.TransactionHistory")
columns = [col[0] for col in cursor.description]
rows = cursor.fetchall()
df = pl.DataFrame({col: [row[i] for row in rows] for i, col in enumerate(columns)})

# Better: Arrow-based transfer
cursor.execute("SELECT * FROM Production.TransactionHistory")
df = pl.from_arrow(cursor.arrow())
```

## Related content

- [Use mssql-python with Apache Arrow](arrow-integration.md)
- [Use mssql-python with pandas](pandas-integration.md)
- [Use bulk copy with mssql-python](bulk-copy.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
