---
title: Performance Tuning for mssql-python Applications
description: Learn performance optimization techniques for SQL Server applications using the mssql-python driver, including connection pooling, query optimization, and bulk operations.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Performance tuning for mssql-python applications

The mssql-python driver provides several features and patterns to optimize SQL Server application performance, including connection pooling, query optimization, and bulk operations.

## Connection management

### Use connection pooling

Connection pooling is built-in. When you call `conn.close()`, the connection returns to the pool for reuse instead of being destroyed, so subsequent `connect()` calls skip the expensive handshake:

```python
import mssql_python

def get_data():
    conn = mssql_python.connect(
        "Server=<server>.database.windows.net;Database=<database>;"
        "Authentication=ActiveDirectoryDefault;Encrypt=yes"
    )
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT TOP 10 Name, ListPrice FROM Production.Product")
        return cursor.fetchall()
    finally:
        conn.close()
```

### Configure pool size for workload

Adjust the pool size based on your concurrency requirements. If your application handles many simultaneous users, increase the pool. For lighter workloads, a smaller pool conserves server resources:

```python
import mssql_python

mssql_python.pooling(
    max_size=50,      # Default is 100; reduce or increase for your workload
    idle_timeout=600  # Seconds before idle connections are recycled
)
```

### Reuse connections within operations

Opening a new connection for every query adds overhead even with pooling. Instead, hold a single connection for the duration of a logical operation:

```python
# Bad: New connection per query
def bad_pattern(product_ids):
    for pid in product_ids:
        conn = mssql_python.connect(connection_string)
        cursor = conn.cursor()
        cursor.execute("SELECT Name, ListPrice FROM Production.Product WHERE ProductID = %(pid)s", {"pid": pid})
        row = cursor.fetchone()
        process(row)
        conn.close()

# Good: Single connection for all queries
def good_pattern(product_ids):
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    try:
        for pid in product_ids:
            cursor.execute("SELECT Name, ListPrice FROM Production.Product WHERE ProductID = %(pid)s", {"pid": pid})
            row = cursor.fetchone()
            process(row)
    finally:
        conn.close()
```

### Keep connections open in long-running services

Web servers, queue workers, and scheduled jobs that run continuously should hold connections open rather than connecting and disconnecting on every operation. Opening a connection involves a TCP handshake, TLS negotiation, and authentication, which can take 50-200 ms depending on network distance and auth method. For a queue worker processing thousands of messages per hour, that overhead adds up fast.

Keep the connection open for the lifetime of the worker and reconnect when the connection fails. Sleep between iterations to avoid hammering the server when the queue is empty:

```python
import mssql_python
import time

def run_worker(connection_string: str, poll_interval: float = 1.0):
    conn = None
    try:
        while True:
            try:
                if conn is None:
                    conn = mssql_python.connect(connection_string)
                cursor = conn.cursor()
                cursor.execute("SELECT TOP 1 * FROM dbo.JobQueue WHERE Status = 'Pending' ORDER BY CreatedDate")
                job = cursor.fetchone()
                if job:
                    try:
                        process_job(job)
                        cursor.execute("UPDATE dbo.JobQueue SET Status = 'Done' WHERE JobID = %(job_id)s", {"job_id": job[0]})
                    except Exception:
                        cursor.execute("UPDATE dbo.JobQueue SET Status = 'Failed' WHERE JobID = %(job_id)s", {"job_id": job[0]})
                    conn.commit()
                else:
                    time.sleep(poll_interval)  # No work available, wait before polling again
            except mssql_python.OperationalError:
                # Connection lost, reconnect on next iteration
                conn = None
                time.sleep(poll_interval)
    finally:
        if conn is not None:
            conn.close()
```

With connection pooling enabled (the default), the pool handles idle connections for you. But if you disable pooling or use a single dedicated connection, set `Connection Timeout` and `Command Timeout` in your connection string to detect stale connections early rather than hanging.

## Query optimization

### Fetch only needed data

Selecting only the columns your application uses reduces network transfer, memory consumption, and query execution time.

```python
# Bad: Select all columns
cursor.execute("SELECT * FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s", {"customer_id": 1})

# Good: Select specific columns
cursor.execute("""
    SELECT SalesOrderID, OrderDate, TotalDue
    FROM Sales.SalesOrderHeader
    WHERE CustomerID = %(customer_id)s
""", {"customer_id": 1})
```

### Use appropriate fetch methods

The driver provides several fetch methods. Use the one that matches your result size:

- `fetchval()` returns a single scalar value with minimal overhead.
- `fetchall()` loads the entire result set into memory, which works well for small tables.
- `fetchmany(n)` retrieves rows in batches, keeping memory usage constant for large result sets.

The right batch size for `fetchmany()` depends on row width. For narrow rows (a few small columns, roughly 1 KB each), 1,000 rows keeps each batch around 1 MB of memory. For wider rows with large strings or binary columns, use a smaller batch size. Start with 1,000 and adjust based on your data.

```python
def process_batch(rows):
    # Example: print each row. Replace with your own logic.
    for row in rows:
        print(row)

# Single value
cursor.execute("SELECT COUNT(*) FROM Production.Product")
count = cursor.fetchval()

# Small result set
cursor.execute("SELECT ProductCategoryID, Name FROM Production.ProductCategory")
categories = cursor.fetchall()

# Large result set - process in batches
cursor.execute("SELECT SalesOrderID, OrderDate, TotalDue FROM Sales.SalesOrderHeader")
while True:
    batch = cursor.fetchmany(1000)
    if not batch:
        break
    process_batch(batch)
```

### Use server-side pagination

Instead of fetching all rows and slicing in Python, use `OFFSET`/`FETCH NEXT` to retrieve only the page you need.

```python
def get_page(cursor, page: int, page_size: int = 50) -> list:
    """Get paginated results efficiently."""
    offset = (page - 1) * page_size

    cursor.execute("""
        SELECT ProductID, Name, ListPrice
        FROM Production.Product
        ORDER BY ProductID
        OFFSET %(offset)s ROWS
        FETCH NEXT %(page_size)s ROWS ONLY
    """, {"offset": offset, "page_size": page_size})

    return cursor.fetchall()
```

### Use SET NOCOUNT ON

By default, SQL Server sends a "rows affected" message after every DML statement. `SET NOCOUNT ON` suppresses these messages and reduces network traffic. It's a session-level setting, so set it once after connecting rather than embedding it in every query.

```python
# Set once after connecting
cursor.execute("SET NOCOUNT ON")

# All subsequent statements on this connection skip the row-count message
cursor.execute(
    "INSERT INTO Log (Message) VALUES (%(message)s)",
    {"message": "Log entry"}
)
```

## Choose the right insert method

The driver provides three ways to insert data, each suited to a different scale:

| Method | Row count | Why |
| --- | --- | --- |
| `execute()` | 1 row per call | Use for single-row operations like form submissions or API handlers where you need the inserted ID immediately. |
| `executemany()` | ~10-1,000 rows | Uses column-wise parameter binding for better throughput than a loop. Sends each row as a parameterized statement. |
| `bulkcopy()` | Hundreds of rows and up | Uses the TDS bulk insert protocol, which streams rows instead of sending one statement per row. Best for data loads, migrations, and batch processing. |

For more details and examples, see [Data loading and movement patterns](data-loading-movement-patterns.md).

### Single inserts with execute()

Use for one-off inserts where you need the result immediately. `Production.Product` has several NOT NULL columns without defaults, so the insert lists all of them:

```python
from datetime import datetime

cursor.execute(
    """
    INSERT INTO Production.Product
        (Name, ProductNumber, SafetyStockLevel, ReorderPoint,
         StandardCost, ListPrice, DaysToManufacture, SellStartDate)
    VALUES (%(name)s, %(number)s, %(safety)s, %(reorder)s,
            %(cost)s, %(price)s, %(days)s, %(start)s)
    """,
    {
        "name": "Widget", "number": "WG-1001",
        "safety": 100, "reorder": 75,
        "cost": 12.50, "price": 19.99,
        "days": 1, "start": datetime(2024, 1, 1),
    },
)
conn.commit()
```

### Batch inserts with executemany()

`executemany()` binds parameters column-wise and sends them efficiently. Use it for moderate batches instead of calling `execute()` in a loop. Note that `executemany()` requires positional `?` markers with a list of tuples, while `execute()` supports both `?` and named `%(name)s` parameters with dicts. See [Parameterized queries](parameterized-queries.md) for details on each style.

```python
rows = [
    ("Widget A", "WG-1001", 100, 75, 12.50, 19.99, 1, datetime(2024, 1, 1)),
    ("Widget B", "WG-1002", 100, 75, 15.00, 24.99, 1, datetime(2024, 1, 1)),
    ("Widget C", "WG-1003", 100, 75, 18.00, 29.99, 1, datetime(2024, 1, 1)),
]

cursor.executemany(
    """
    INSERT INTO Production.Product
        (Name, ProductNumber, SafetyStockLevel, ReorderPoint,
         StandardCost, ListPrice, DaysToManufacture, SellStartDate)
    VALUES (?, ?, ?, ?, ?, ?, ?, ?)
    """,
    rows,
)
conn.commit()
```

### Bulk copy for large loads

When throughput matters more than per-row control, switch to `bulkcopy()`. It streams rows through the TDS bulk insert protocol and avoids the per-row overhead of parameterized statements. The exact crossover where `bulkcopy()` outperforms `executemany()` depends on row width and network latency, but it's typically in the low hundreds of rows. For very small batches, `executemany()` is simpler because `bulkcopy()` creates a separate internal connection and commits automatically.

Unlike `execute()` and `executemany()`, `bulkcopy()` maps values to columns by position, not by an `INSERT` column list. Pass `column_mappings` to name the destination columns you're loading, so the source tuples line up with the right columns instead of the table's leading identity column:

```python
result = cursor.bulkcopy(
    "Production.Product",
    rows,
    column_mappings=["Name", "ProductNumber", "SafetyStockLevel", "ReorderPoint",
                     "StandardCost", "ListPrice", "DaysToManufacture", "SellStartDate"],
)
print(f"Copied {result['rows_copied']} rows")
```

For very large loads, use a generator to avoid loading the entire dataset into memory and set `batch_size` to commit periodically:

```python
import csv

def csv_rows(path):
    with open(path, newline="") as f:
        reader = csv.reader(f)
        next(reader)  # Skip header
        for row in reader:
            yield tuple(row)

cursor.bulkcopy(
    "Production.Product",
    csv_rows("products.csv"),
    column_mappings=["Name", "ProductNumber", "SafetyStockLevel", "ReorderPoint",
                     "StandardCost", "ListPrice", "DaysToManufacture", "SellStartDate"],
    batch_size=5000,
)
```

## Caching strategies

For reference data that rarely changes (categories, lookup tables, configuration), cache the results in your application instead of querying on every request.

Python's [`functools.lru_cache`](https://docs.python.org/3/library/functools.html#functools.lru_cache) provides simple memoization, but it caches indefinitely until the process restarts. If the underlying data can change, use [`cachetools.TTLCache`](https://pypi.org/project/cachetools/) to automatically refresh after a time limit:

```python
from cachetools import TTLCache, cached

category_cache = TTLCache(maxsize=1, ttl=300)  # Refresh every 5 minutes

@cached(category_cache)
def get_categories(connection_string: str) -> tuple:
    conn = mssql_python.connect(connection_string)
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT ProductCategoryID, Name FROM Production.ProductCategory")
        return cursor.fetchall()
    finally:
        conn.close()
```

## Network optimization

### Minimize round trips

Each query is a network round trip to the server. Combine related queries into a single batch and use `nextset()` to advance through the result sets:

```python
# Bad: Multiple round trips
cursor.execute("SELECT CustomerID, AccountNumber FROM Sales.Customer WHERE CustomerID = %(customer_id)s", {"customer_id": 1})
customer = cursor.fetchone()
cursor.execute("SELECT SalesOrderID, OrderDate, TotalDue FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s", {"customer_id": 1})
orders = cursor.fetchall()
cursor.execute("SELECT COUNT(*) FROM Sales.SalesOrderDetail WHERE SalesOrderID IN (SELECT SalesOrderID FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s)", {"customer_id": 1})
detail_count = cursor.fetchval()

# Good: Single round trip
cursor.execute("""
    SELECT CustomerID, AccountNumber FROM Sales.Customer WHERE CustomerID = %(customer_id)s;
    SELECT SalesOrderID, OrderDate, TotalDue FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s;
    SELECT COUNT(*) FROM Sales.SalesOrderDetail WHERE SalesOrderID IN (SELECT SalesOrderID FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s);
""", {"customer_id": 1})

customer = cursor.fetchone()
cursor.nextset()
orders = cursor.fetchall()
cursor.nextset()
detail_count = cursor.fetchval()
```

### Use server-side processing for complex logic

Push aggregation and filtering to SQL Server instead of fetching raw rows and processing them in Python. The server returns a single summary row instead of potentially thousands of detail rows:

```python
cursor.execute("""
    SELECT p.Name, COUNT(sod.SalesOrderDetailID) AS OrderCount, SUM(sod.LineTotal) AS TotalSales
    FROM Production.Product p
    JOIN Sales.SalesOrderDetail sod ON p.ProductID = sod.ProductID
    WHERE p.ProductID = %(product_id)s
    GROUP BY p.Name
""", {"product_id": 707})
```

### Avoid interleaved cursor operations

The mssql-python driver doesn't support Multiple Active Result Sets (MARS). Only one cursor can have an active query per connection. Fetch the first result set completely before running the next query, or use a second connection:

```python
connection_string = (
    "Server=<server>.database.windows.net;Database=<database>;"
    "Authentication=ActiveDirectoryDefault;Encrypt=yes"
)

# Option 1: Fetch first, then query (single connection)
# Warning: This is an N+1 pattern. Each iteration is a round trip.
# Use this only when the JOIN in Option 2 is not possible.
conn = mssql_python.connect(connection_string)
cursor = conn.cursor()
cursor.execute("SELECT ProductID FROM Production.Product WHERE ProductSubcategoryID = 1")
product_ids = [row[0] for row in cursor.fetchall()]

for pid in product_ids:
    cursor.execute("SELECT ProductID, LocationID, Quantity FROM Production.ProductInventory WHERE ProductID = %(product_id)s", {"product_id": pid})
    inventory = cursor.fetchone()

conn.close()

# Option 2: Use a JOIN instead of N+1 queries (preferred)
conn = mssql_python.connect(connection_string)
cursor = conn.cursor()
cursor.execute("""
    SELECT p.ProductID, p.Name, i.Quantity
    FROM Production.Product p
    LEFT JOIN Production.ProductInventory i ON p.ProductID = i.ProductID
    WHERE p.ProductSubcategoryID = 1
""")
results = cursor.fetchall()
conn.close()
```

## Memory management

### Process large results in chunks

Loading a multimillion-row table into a list consumes memory proportional to the full result set. Use `OFFSET` and `FETCH NEXT` to paginate through the data server-side and process one chunk at a time.

```python
def quote_id(identifier: str) -> str:
    """Quote a possibly schema-qualified SQL identifier to prevent SQL injection."""
    return ".".join("[" + part.replace("]", "]]") + "]" for part in identifier.split("."))

def process_large_table(cursor, table: str, columns: list[str], key_column: str, processor, chunk_size: int = 10000):
    """Process large table without loading all data."""
    safe_table = quote_id(table)
    safe_key = quote_id(key_column)
    col_list = ", ".join(quote_id(c) for c in columns)
    cursor.execute(f"SELECT COUNT(*) FROM {safe_table}")
    total = cursor.fetchval()

    offset = 0
    while offset < total:
        cursor.execute(f"""
            SELECT {col_list} FROM {safe_table}
            ORDER BY {safe_key}
            OFFSET ? ROWS
            FETCH NEXT ? ROWS ONLY
        """, (offset, chunk_size))

        chunk = cursor.fetchall()
        processor(chunk)

        offset += chunk_size
        print(f"Processed {min(offset, total)}/{total}")

# key_column must be unique, otherwise rows can be duplicated or skipped across pages
process_large_table(
    cursor,
    "Production.TransactionHistory",
    ["TransactionID", "ProductID", "Quantity", "ActualCost"],
    "TransactionID",
    lambda chunk: None,  # replace with your row-processing logic
)
```

### Use generators for streaming

A Python generator wrapping `fetchmany()` keeps memory usage constant regardless of table size. The caller iterates row by row without loading the full result set. For an extra-large source, combine tables with `UNION ALL` and stream the combined result the same way.

```python
def stream_query(cursor, query: str, params: dict = None, batch_size: int = 1000):
    cursor.execute(query, params or {})
    
    while True:
        batch = cursor.fetchmany(batch_size)
        if not batch:
            break
        for row in batch:
            yield row

# Union the live and archive transaction tables into one extra-large result set
query = """
    SELECT TransactionID, ProductID, Quantity, ActualCost FROM Production.TransactionHistory
    UNION ALL
    SELECT TransactionID, ProductID, Quantity, ActualCost FROM Production.TransactionHistoryArchive
"""

count = 0
for row in stream_query(cursor, query, batch_size=5000):
    count += 1
print(f"Streamed {count} rows")
```

### Clean up resources promptly

Unclosed connections tie up server resources and can exhaust the connection pool. Use a context manager to guarantee cleanup even when exceptions occur.

```python
from contextlib import contextmanager

@contextmanager
def database_connection(connection_string: str):
    conn = mssql_python.connect(connection_string)
    try:
        yield conn
    finally:
        conn.close()

with database_connection(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("SELECT TOP 10 Name, ListPrice FROM Production.Product")
    data = cursor.fetchall()
```

### Monitor memory usage

Large result sets, long-lived caches, and connection objects all consume memory. If your application runs as a service, memory leaks from unclosed cursors or unbounded caches can eventually cause the process to be killed by the OS or container runtime.

Use Python's [`tracemalloc`](https://docs.python.org/3/library/tracemalloc.html) module to snapshot memory and find the largest allocations.

```python
import tracemalloc

tracemalloc.start()

# ... run your workload ...

snapshot = tracemalloc.take_snapshot()
top_stats = snapshot.statistics("lineno")
for stat in top_stats[:10]:
    print(stat)
```

Common sources of unexpected memory growth include:

- Calling `fetchall()` on a query that returns millions of rows. Use `fetchmany()` or a generator instead.
- Caching query results without a `maxsize` or TTL. Caches grow until the process is restarted.
- Creating cursors in a loop without closing them. Each open cursor holds its result set in memory.

## Index and query plan optimization

### Check server-side query performance

Use `SET STATISTICS TIME ON` and `SET STATISTICS IO ON` to see how long queries take on the server and how much data they read. High logical reads usually indicate a missing index. Run these statements in [SQL Server Management Studio](../../../ssms/sql-server-management-studio-ssms.md) or the [MSSQL extension for Visual Studio Code](../../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), where the output appears in the **Messages** pane:

```sql
SET STATISTICS TIME ON;
SET STATISTICS IO ON;

SELECT * FROM Production.Product WHERE ProductSubcategoryID = 1;

SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
```

You should see output like:

```
Table 'Product'. Scan count 1, logical reads 3
SQL Server Execution Times: CPU time = 0 ms, elapsed time = 1 ms.
```

If you see high logical reads or table scans, consider adding an index.

### Use query hints as a tactical fix

Query hints override the query optimizer's index and join strategy choices. In production, they're valuable as a quick, low-risk patch when a query suddenly regresses. You can deploy the hint in your application code immediately to stabilize the query while you investigate the root cause (missing indexes, stale statistics, or schema changes).

Avoid leaving hints in place permanently. When the data distribution or schema changes, a hardcoded hint can make things worse. Treat them as temporary and revisit after the underlying issue is resolved:

```python
cursor.execute("""
    SELECT * FROM Production.Product WITH (INDEX(AK_Product_Name))
    WHERE ProductSubcategoryID = %(subcategory_id)s
""", {"subcategory_id": 1})
```

### Use OPTION (RECOMPILE) to bypass bad cached plans

SQL Server caches query plans based on the first set of parameter values it sees. If data distribution varies widely between calls, the cached plan might perform poorly for some values. This problem, called parameter sniffing, often surfaces as a query that "used to be fast" suddenly taking seconds or minutes.

`OPTION (RECOMPILE)` forces SQL Server to build a fresh plan for each execution, which is an effective immediate fix you can deploy without any server-side changes. The trade-off is a small compilation cost per call, but for queries that run infrequently or return variable-sized result sets, that cost is negligible compared to running a bad plan.

Once you've stabilized the issue, you can take your time applying a permanent fix such as rewriting the query, adding filtered indexes, or using plan guides:

```python
cursor.execute("""
    SELECT * FROM Sales.SalesOrderHeader
    WHERE OrderDate > %(start_date)s
    OPTION (RECOMPILE)
""", {"start_date": start_date})
```

## Performance monitoring

### Time your queries

To find slow operations, wrap queries with `time.perf_counter()`:

```python
import time

start = time.perf_counter()
cursor.execute("SELECT SalesOrderID, OrderDate, TotalDue FROM Sales.SalesOrderHeader WHERE CustomerID = %(customer_id)s", {"customer_id": customer_id})
rows = cursor.fetchall()
elapsed = time.perf_counter() - start

print(f"Query returned {len(rows)} rows in {elapsed:.3f}s")
```

For a broader view of where your application spends time, use Python's built-in [`cProfile`](https://docs.python.org/3/library/profile.html) module:

```bash
python -m cProfile -s cumtime my_app.py
```

This view shows cumulative time per function call, which helps you identify whether slowness is in query execution, data processing, or network latency.

### Use Query Store for server-side analysis

Client-side timing tells you how long a query takes from your application's perspective, but it combines network latency, server execution time, and client processing. [Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) captures execution plans and runtime statistics on the server, so you can see exactly how SQL Server executed each query, how often it ran, and how its performance changed over time.

Query Store is especially useful for identifying parameter sniffing, plan regressions, and queries that consume the most server resources. You can query the `sys.query_store_runtime_stats` and `sys.query_store_plan` views directly, or use the built-in Query Store reports in [SQL Server Management Studio](../../../ssms/sql-server-management-studio-ssms.md).

### Use Performance Dashboard Reports

The [Performance Dashboard Reports](../../../relational-databases/performance/performance-dashboard.md) in SQL Server Management Studio provide a real-time overview of SQL Server health, including current wait types, active expensive queries, and CPU/IO trends. Use them to quickly spot bottlenecks without writing queries against DMVs directly.

## Performance checklist

### Connection

- [ ] Enable connection pooling.
- [ ] Size the pool for your workload.
- [ ] Reuse connections within operations.
- [ ] Keep connections open in long-running services.

### Queries

- [ ] Select only the columns you need.
- [ ] Use the appropriate fetch method for each query.
- [ ] Implement server-side pagination.
- [ ] Set `SET NOCOUNT ON` once after connecting.
- [ ] Minimize round trips by batching queries.

### Inserts

- [ ] Use `execute()` for single-row inserts.
- [ ] Use `executemany()` for small-to-moderate batches (~10-1,000 rows).
- [ ] Use `bulkcopy()` when throughput matters more than per-row control.

### Caching

- [ ] Cache reference data with a TTL to avoid serving stale results.

### Resources

- [ ] Process large results in chunks or with generators.
- [ ] Clean up connections promptly.
- [ ] Monitor memory usage with `tracemalloc` in long-running services.

## Related content

- [Connection pooling with mssql-python](connection-pooling.md)
- [Use bulk copy with mssql-python](bulk-copy.md)
- [Retrieve data with mssql-python](retrieving-data.md)
