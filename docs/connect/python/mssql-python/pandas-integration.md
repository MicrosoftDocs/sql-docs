---
title: Use mssql-python with pandas
description: Learn how to integrate the mssql-python driver with pandas DataFrames for data analysis with Microsoft SQL and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with pandas

The pandas library is Python's primary data analysis tool. By combining pandas with the mssql-python driver, you can:

- Load SQL query results directly into DataFrames.
- Write DataFrames back to Microsoft SQL efficiently.
- Perform ETL operations.
- Create data pipelines.

The examples in this article query the `Production.Product` table and other tables in the [AdventureWorks sample database](/sql/samples/adventureworks-install-configure). Examples that write data use temporary tables to avoid modifying sample data.

Other tables referenced in analysis examples (`Sales.SalesOrderHeader`, `Sales.SalesOrderDetail`, `Production.ProductSubcategory`) are part of AdventureWorks. Substitute your own tables when adapting these patterns.

## Read data into DataFrames

The mssql-python driver returns rows as Python objects, which you convert to pandas DataFrames by reading column names from `cursor.description` and row values from `fetchall()`. The helper functions in this section wrap that conversion into reusable patterns.

### Basic query to DataFrame

This function executes a [parameterized query](parameterized-queries.md) and builds a DataFrame from the full result set. It works well for result sets that fit comfortably in memory.

```python
import pandas as pd
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

def query_to_dataframe(cursor, query: str, params: dict = None) -> pd.DataFrame:
    """Execute query and return results as DataFrame."""
    cursor.execute(query, params or {})
    
    # cursor.description is a list of tuples, one per column.
    # Each tuple's first element is the column name.
    columns = [col[0] for col in cursor.description]
    
    # Fetch all rows
    rows = cursor.fetchall()
    
    # Convert to DataFrame
    data = [tuple(row) for row in rows]
    return pd.DataFrame(data, columns=columns)

# Usage: %(cat)s is a parameterized placeholder. The driver safely substitutes
# the value from the dict, which prevents SQL injection.
df = query_to_dataframe(cursor, "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 5})
print(df.head())
```

> [!NOTE]
> If your connection string uses `Authentication=ActiveDirectoryDefault`, the driver uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

### Stream large datasets

For tables with millions of rows, loading everything at once can exhaust memory. The chunked approach fetches rows in batches with `fetchmany()` and concatenates the results, keeping peak memory usage proportional to `chunksize` rather than the full result set.

```python
def query_to_dataframe_chunked(cursor, query: str, params: dict = None, 
                                chunksize: int = 10000) -> pd.DataFrame:
    """Load large query results in chunks for memory efficiency."""
    cursor.execute(query, params or {})
    columns = [col[0] for col in cursor.description]
    
    chunks = []
    while True:
        rows = cursor.fetchmany(chunksize)
        if not rows:
            break
        data = [tuple(row) for row in rows]
        chunks.append(pd.DataFrame(data, columns=columns))
    
    return pd.concat(chunks, ignore_index=True) if chunks else pd.DataFrame(columns=columns)

# Usage for large tables
df = query_to_dataframe_chunked(cursor, "SELECT * FROM Production.TransactionHistory", chunksize=50000)
```

### Generator for large datasets

When you need to process data incrementally without holding the entire result in memory, use a generator. Each `yield` produces one DataFrame chunk that you can process and discard before fetching the next.

```python
def query_to_dataframe_generator(cursor, query: str, params: dict = None,
                                  chunksize: int = 10000):
    """Yield DataFrame chunks for processing without loading all data."""
    cursor.execute(query, params or {})
    columns = [col[0] for col in cursor.description]
    
    while True:
        rows = cursor.fetchmany(chunksize)
        if not rows:
            break
        data = [tuple(row) for row in rows]
        yield pd.DataFrame(data, columns=columns)

# Process chunks without loading entire dataset
huge_query = """
    SELECT * FROM Production.TransactionHistory
    UNION ALL SELECT * FROM Production.TransactionHistory
    UNION ALL SELECT * FROM Production.TransactionHistory
"""
for chunk_df in query_to_dataframe_generator(cursor, huge_query):
    # Process each chunk, then discard it before the next fetch
    print(f"Processing chunk of {len(chunk_df)} rows")
    total_cost = chunk_df["ActualCost"].sum()
    print(f"Chunk total cost: {total_cost}")
```

## Write DataFrames to Microsoft SQL

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

The simplest approach iterates over DataFrame rows and issues one `INSERT` per row. The simple approach works for small DataFrames but is slow for large volumes because each row requires a separate round trip to the server.

```python
def dataframe_to_sql(cursor, conn, df: pd.DataFrame, table: str, 
                     if_exists: str = "append") -> int:
    """Write DataFrame to Microsoft SQL table."""
    if if_exists == "replace":
        cursor.execute(f"TRUNCATE TABLE {quote_id(table)}")
    
    columns = df.columns.tolist()
    placeholders = ", ".join([f"%({col})s" for col in columns])
    col_list = ", ".join([quote_id(col) for col in columns])
    
    query = f"INSERT INTO {quote_id(table)} ({col_list}) VALUES ({placeholders})"
    
    rows_inserted = 0
    for _, row in df.iterrows():
        params = {col: (None if pd.isna(val) else val) for col, val in row.items()}
        cursor.execute(query, params)
        rows_inserted += 1
    
    conn.commit()
    return rows_inserted

# Usage
cursor.execute("""
    CREATE TABLE #Products (
        Name NVARCHAR(100),
        ListPrice DECIMAL(10,2),
        ProductSubcategoryID INT
    )
""")
df = pd.DataFrame({
    "Name": ["Product A", "Product B"],
    "ListPrice": [29.99, 49.99],
    "ProductSubcategoryID": [1, 2]
})
rows = dataframe_to_sql(cursor, conn, df, "#Products")
print(f"Inserted {rows} rows")
```

### Bulk insert with BCP (recommended for large DataFrames)

For large DataFrames, use the driver's `bulkcopy()` method, which sends rows in bulk over the TDS (Tabular Data Stream) protocol, the native wire protocol Microsoft SQL uses. This approach is faster than row-by-row inserts because it minimizes round trips.

```python
def dataframe_to_sql_bulk(conn, df: pd.DataFrame, table: str) -> int:
    """Bulk insert DataFrame using BCP for better performance."""
    # Convert DataFrame to list of tuples, handling NaN
    rows = []
    for _, row in df.iterrows():
        row_data = tuple(None if pd.isna(v) else v for v in row)
        rows.append(row_data)
    
    cursor = conn.cursor()
    result = cursor.bulkcopy(table, rows)
    conn.commit()
    return result["rows_copied"]

# Usage
cursor.execute("CREATE TABLE ##PandasProducts (Name NVARCHAR(50), ListPrice DECIMAL(10,2), ProductSubcategoryID INT)")
conn.commit()

df = pd.DataFrame({
    "Name": ["Product A", "Product B", "Product C"],
    "ListPrice": [29.99, 49.99, 19.99],
    "ProductSubcategoryID": [1, 2, 1]
})

rows = dataframe_to_sql_bulk(conn, df, "##PandasProducts")
```

### Update existing rows from DataFrame

To update rows that already exist in the table, iterate over the DataFrame and issue parameterized `UPDATE` statements. The `key_column` identifies which row to update.

```python
def update_from_dataframe(cursor, conn, df: pd.DataFrame, table: str,
                          key_column: str) -> int:
    """Update existing rows based on key column."""
    columns = [col for col in df.columns if col != key_column]
    set_clause = ", ".join([f"{quote_id(col)} = %({col})s" for col in columns])
    
    query = f"UPDATE {quote_id(table)} SET {set_clause} WHERE {quote_id(key_column)} = %({key_column})s"
    
    rows_updated = 0
    for _, row in df.iterrows():
        params = {col: (None if pd.isna(val) else val) for col, val in row.items()}
        cursor.execute(query, params)
        rows_updated += cursor.rowcount
    
    conn.commit()
    return rows_updated

# Usage
cursor.execute("""
    CREATE TABLE #ProductPrices (
        ProductID INT PRIMARY KEY,
        ListPrice DECIMAL(10,2)
    );
    INSERT INTO #ProductPrices VALUES (1, 29.99), (2, 49.99), (3, 19.99);
""")
conn.commit()

df_updates = pd.DataFrame({
    "ProductID": [1, 2, 3],
    "ListPrice": [31.99, 52.99, 21.99]
})
updated = update_from_dataframe(cursor, conn, df_updates, "#ProductPrices", "ProductID")
```

### Upsert (merge) pattern

When some rows might be new and others might already exist, use a SQL `MERGE` statement to insert or update in a single operation. `MERGE` compares each incoming row against the target table using the key columns. If a match is found, it updates; otherwise it inserts. `MERGE` avoids checking for existence separately.

```python
def upsert_from_dataframe(cursor, conn, df: pd.DataFrame, table: str,
                          key_columns: list[str]) -> int:
    """Insert or update rows based on key columns. Returns total rows affected."""
    all_columns = df.columns.tolist()
    value_columns = [c for c in all_columns if c not in key_columns]
    
    total_affected = 0
    
    for _, row in df.iterrows():
        params = {col: (None if pd.isna(val) else val) for col, val in row.items()}
        
        # Build MERGE statement with quoted identifiers
        key_match = " AND ".join([f"t.{quote_id(k)} = s.{quote_id(k)}" for k in key_columns])
        update_set = ", ".join([f"{quote_id(c)} = s.{quote_id(c)}" for c in value_columns])
        all_cols = ", ".join([quote_id(c) for c in all_columns])
        all_vals = ", ".join([f"%({c})s" for c in all_columns])
        
        cursor.execute(f"""
            MERGE {quote_id(table)} AS t
            USING (SELECT {', '.join([f'%({c})s AS {quote_id(c)}' for c in all_columns])}) AS s
            ON {key_match}
            WHEN MATCHED THEN UPDATE SET {update_set}
            WHEN NOT MATCHED THEN INSERT ({all_cols}) VALUES ({all_vals});
        """, params)
        
        total_affected += cursor.rowcount
    
    conn.commit()
    return total_affected
```

## Data analysis patterns

The following examples show common analysis tasks that combine Microsoft SQL queries with pandas transformations.

### Aggregate queries to DataFrame

```python
def get_sales_summary(cursor) -> pd.DataFrame:
    """Get sales summary by category."""
    return query_to_dataframe(cursor, """
        SELECT 
            pc.Name AS CategoryName,
            COUNT(*) AS ProductCount,
            AVG(p.ListPrice) AS AvgPrice,
            MIN(p.ListPrice) AS MinPrice,
            MAX(p.ListPrice) AS MaxPrice
        FROM Production.Product p
        JOIN Production.ProductSubcategory pc ON p.ProductSubcategoryID = pc.ProductSubcategoryID
        GROUP BY pc.Name
        ORDER BY ProductCount DESC
    """)

df = get_sales_summary(cursor)
print(df.to_string())
```

### Time series data

Use pandas date indexing and resampling to work with time series data from Microsoft SQL. To enable operations like rolling averages and resampling, set the date column as the DataFrame index.

```python
def get_daily_sales(cursor, start_date: str, end_date: str) -> pd.DataFrame:
    """Get daily sales time series."""
    df = query_to_dataframe(cursor, """
        SELECT 
            CAST(OrderDate AS DATE) AS Date,
            COUNT(*) AS OrderCount,
            SUM(TotalDue) AS Revenue
        FROM Sales.SalesOrderHeader
        WHERE OrderDate BETWEEN %(start)s AND %(end)s
        GROUP BY CAST(OrderDate AS DATE)
        ORDER BY Date
    """, {"start": start_date, "end": end_date})
    
    # Set date as index for time series operations
    df["Date"] = pd.to_datetime(df["Date"])
    df.set_index("Date", inplace=True)
    
    return df

# Usage
sales_df = get_daily_sales(cursor, "2024-01-01", "2024-12-31")

# Resample to weekly
weekly = sales_df.resample("W").sum()

# Calculate rolling average
sales_df["RollingAvg"] = sales_df["Revenue"].rolling(window=7).mean()
```

### Pivot tables from SQL data

Pivot tables reshape data from rows into a matrix format. To reorganize it by dimensions like year, month, and category, pull the raw data from Microsoft SQL, then use `pivot_table()`.

```python
def get_sales_pivot(cursor) -> pd.DataFrame:
    """Get sales data and create pivot table."""
    df = query_to_dataframe(cursor, """
        SELECT 
            YEAR(soh.OrderDate) AS Year,
            MONTH(soh.OrderDate) AS Month,
            pc.Name AS CategoryName,
            SUM(sod.OrderQty * sod.UnitPrice) AS Revenue
        FROM Sales.SalesOrderHeader soh
        JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID
        JOIN Production.Product p ON sod.ProductID = p.ProductID
        JOIN Production.ProductSubcategory pc ON p.ProductSubcategoryID = pc.ProductSubcategoryID
        GROUP BY YEAR(soh.OrderDate), MONTH(soh.OrderDate), pc.Name
    """)
    
    # Create pivot table
    pivot = df.pivot_table(
        values="Revenue",
        index=["Year", "Month"],
        columns="CategoryName",
        aggfunc="sum",
        fill_value=0
    )
    
    return pivot

pivot_df = get_sales_pivot(cursor)
print(pivot_df)
```

## ETL patterns

To build extract, transform, and load pipelines, combine Microsoft SQL queries with pandas transformations to build extract, transform, and load pipelines. The driver handles extraction and loading while pandas handles the transformation step.

### Extract, transform, load

This example extracts active customer data, applies business rules to segment customers, and loads the results into a destination table.

```python
def etl_pipeline(source_cursor, dest_cursor, dest_conn):
    """Simple ETL pipeline with pandas."""
    
    # Extract
    df = query_to_dataframe(source_cursor, """
        SELECT 
            c.CustomerID,
            COUNT(soh.SalesOrderID) AS OrderCount,
            SUM(soh.TotalDue) AS TotalSpent
        FROM Sales.Customer c
        JOIN Sales.SalesOrderHeader soh ON c.CustomerID = soh.CustomerID
        WHERE soh.OrderDate > DATEADD(YEAR, -1, GETDATE())
        GROUP BY c.CustomerID
    """)
    
    # Transform
    df["CustomerSegment"] = pd.cut(
        df["TotalSpent"],
        bins=[0, 100, 500, 1000, float("inf")],
        labels=["Bronze", "Silver", "Gold", "Platinum"]
    )
    df["AvgOrderValue"] = df["TotalSpent"] / df["OrderCount"].replace(0, 1)
    df["IsHighValue"] = df["TotalSpent"] > 500
    
    # Load
    dataframe_to_sql_bulk(dest_conn, df[["CustomerID", "CustomerSegment", "AvgOrderValue", "IsHighValue"]], 
                          "#CustomerAnalytics")
    
    return len(df)
```

### Incremental load pattern

For ongoing data pipelines, load only records that changed since the last run. This approach queries the destination table for the maximum timestamp, then fetches only newer records from the source.

```python
def incremental_load(cursor, conn, source_table: str, dest_table: str,
                     timestamp_col: str) -> int:
    """Load only new/changed records based on timestamp."""
    
    # Get last loaded timestamp
    cursor.execute(f"SELECT MAX({quote_id(timestamp_col)}) FROM {quote_id(dest_table)}")
    last_loaded = cursor.fetchval()
    
    # Build query for new records
    if last_loaded:
        df = query_to_dataframe(cursor, f"""
            SELECT * FROM {quote_id(source_table)}
            WHERE {quote_id(timestamp_col)} > %(last)s
        """, {"last": last_loaded})
    else:
        df = query_to_dataframe(cursor, f"SELECT * FROM {quote_id(source_table)}")
    
    if df.empty:
        return 0
    
    # Load new records
    return dataframe_to_sql_bulk(conn, df, dest_table)
```

## Performance tips

### Use appropriate data types

Pandas defaults to 64-bit types for numbers, wasting memory when smaller types suffice. Downcasting integers and floats, and converting low-cardinality string columns to [categoricals](https://pandas.pydata.org/docs/user_guide/categorical.html), can significantly reduce memory usage.

```python
def optimize_dataframe_types(df: pd.DataFrame) -> pd.DataFrame:
    """Optimize DataFrame memory usage."""
    for col in df.columns:
        col_type = df[col].dtype
        
        if col_type == "int64":
            # Downcast integers
            df[col] = pd.to_numeric(df[col], downcast="integer")
        elif col_type == "float64":
            # Downcast floats
            df[col] = pd.to_numeric(df[col], downcast="float")
        elif col_type == "object":
            # Convert to category if low cardinality
            num_unique = df[col].nunique()
            if num_unique / len(df) < 0.5:
                df[col] = df[col].astype("category")
    
    return df
```

### Use SQL for heavy lifting

Microsoft SQL is faster for aggregations, filtering, and joins than pulling all of your raw data over the wire and processing locally in Python. Let Microsoft SQL do the heavy lifting whenever possible, move only the data you need across the network and use pandas for analysis and transformations that are more convenient in Python.

```python
# Avoid: pulling all rows over the wire to aggregate locally in pandas
df_all = query_to_dataframe(cursor, "SELECT * FROM Production.Product")  # transfers entire table
summary = df_all.groupby("Color").agg({"ListPrice": "sum"})  # aggregation that SQL can do faster

# Better: push the aggregation into SQL and transfer only the summary
df = query_to_dataframe(cursor, """
    SELECT Color, SUM(ListPrice) AS TotalPrice
    FROM Production.Product
    WHERE Color IS NOT NULL
    GROUP BY Color
""")
```

### Batch writes

For large DataFrames that are too large for a single bulk insert, split the work into batches and track progress.

```python
def batch_insert(cursor, conn, df: pd.DataFrame, table: str, batch_size: int = 1000):
    """Insert in batches with progress tracking."""
    total = len(df)
    
    for i in range(0, total, batch_size):
        batch = df.iloc[i:i + batch_size]
        dataframe_to_sql(cursor, conn, batch, table)
        print(f"Inserted {min(i + batch_size, total)}/{total}")
```

## Related content

- [Use bulk copy with mssql-python](bulk-copy.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
