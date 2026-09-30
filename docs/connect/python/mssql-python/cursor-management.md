---
title: Manage Cursors and Result Sets in mssql-python
description: Learn best practices for managing cursors, handling multiple result sets, and efficient memory usage with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Manage cursors and result sets

The mssql-python driver provides cursor objects for executing queries, handling multiple result sets, and managing memory efficiently.

## Cursor basics

### Create and use cursors

Call `conn.cursor()` to create a cursor, then use `execute()` and fetch methods to run queries and retrieve results:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes"
)

# Create cursor
cursor = conn.cursor()

# Execute query
cursor.execute("SELECT TOP 10 Name, ListPrice FROM Production.Product")

# Process results
for row in cursor:
    print(row.Name)

# Close cursor when done
cursor.close()
```

### Context manager pattern

Use the `with` statement to implement a context manager for automatic cleanup:

```python
with mssql_python.connect(connection_string) as conn:
    with conn.cursor() as cursor:
        cursor.execute("SELECT TOP 10 Name, ListPrice FROM Production.Product")
        products = cursor.fetchall()
    # Cursor automatically closed on exit
# Connection automatically closed on exit
```

### Multiple cursors

> [!IMPORTANT]
> The mssql-python driver doesn't support Multiple Active Result Sets (MARS). You can create multiple cursors on a single connection, but only one cursor can have an active query at a time. Always fetch all results from a cursor before executing on another cursor on the same connection.

```python
conn = mssql_python.connect(connection_string)

# Multiple cursors on same connection
cursor1 = conn.cursor()
cursor2 = conn.cursor()

# Fetch results completely from cursor1 before using cursor2
cursor1.execute("SELECT TOP 5 ProductID, Name FROM Production.Product")
products = cursor1.fetchall()

cursor2.execute("SELECT TOP 5 ProductCategoryID, Name FROM Production.ProductCategory")
categories = cursor2.fetchall()

cursor1.close()
cursor2.close()
```

If you need to run queries concurrently, use separate connections instead:

```python
conn1 = mssql_python.connect(connection_string)
conn2 = mssql_python.connect(connection_string)

cursor1 = conn1.cursor()
cursor2 = conn2.cursor()

cursor1.execute("SELECT TOP 5 ProductID, Name FROM Production.Product")
cursor2.execute("SELECT TOP 5 ProductCategoryID, Name FROM Production.ProductCategory")

products = cursor1.fetchall()
categories = cursor2.fetchall()

cursor1.close()
cursor2.close()
conn1.close()
conn2.close()
```

## Fetch strategies

### Fetch all vs iterative fetch

Use `fetchall()` to load the entire result set into memory at once, or iterate over the cursor to process rows one at a time without buffering.

```python
# Fetch all at once - loads entire result into memory
cursor.execute("SELECT * FROM Production.Product")
all_products = cursor.fetchall()
print(f"Loaded {len(all_products)} products")

# Iterative fetch - memory efficient
cursor.execute("SELECT * FROM Production.Product")
count = 0
for row in cursor:
    count += 1
print(f"Processed {count} products")
```

### Fetch in batches

Use `fetchmany()` with a batch size to process large result sets in chunks without loading everything into memory.

```python
def process_batch(rows):
    # Example: print each row. Replace with your own logic.
    for row in rows:
        print(row)

def fetch_in_batches(cursor, batch_size: int = 1000):
    """Fetch results in batches to manage memory."""
    while True:
        batch = cursor.fetchmany(batch_size)
        if not batch:
            break
        yield batch

cursor.execute("SELECT * FROM LargeTable")
for batch in fetch_in_batches(cursor, batch_size=5000):
    process_batch(batch)
    print(f"Processed batch of {len(batch)} rows")
```

### Use fetchval for single values

Use `fetchval()` for scalar queries that return a single value. It returns the first column of the first row.

```python
# Efficient for scalar queries
cursor.execute("SELECT COUNT(*) FROM Production.Product")
count = cursor.fetchval()  # Returns single value directly

cursor.execute("SELECT MAX(ListPrice) FROM Production.Product")
max_price = cursor.fetchval()
```

## Multiple result sets

### Process multiple result sets

Use `nextset()` to advance past the current result set to the next one after fetching all rows from the previous set.

```python
# Query returns multiple results
cursor.execute("""
    SELECT TOP 3 CustomerID, AccountNumber FROM Sales.Customer;
    SELECT TOP 3 SalesOrderID, OrderDate FROM Sales.SalesOrderHeader;
    SELECT TOP 3 ProductID, Name FROM Production.Product;
""")

# First result set
print("Customers:")
customers = cursor.fetchall()
for c in customers:
    print(f"  {c.AccountNumber}")

# Move to second result set
if cursor.nextset():
    print("Orders:")
    orders = cursor.fetchall()
    for o in orders:
        print(f"  Order #{o.SalesOrderID}")

# Move to third result set
if cursor.nextset():
    print("Products:")
    products = cursor.fetchall()
    for p in products:
        print(f"  {p.Name}")
```

### Iterate all result sets

Loop until `nextset()` returns `False` to consume all result sets from a single execute call:

```python
def process_all_result_sets(cursor):
    """Process all result sets from a query."""
    result_sets = []
    
    while True:
        # Fetch current result set
        rows = cursor.fetchall()
        result_sets.append(rows)
        
        # Try to move to next result set
        if not cursor.nextset():
            break
    
    return result_sets

cursor.execute("""
    SELECT TOP 3 ProductID, Name FROM Production.Product ORDER BY ProductID;
    SELECT TOP 3 SalesOrderID, TotalDue FROM Sales.SalesOrderHeader ORDER BY SalesOrderID;
""")
all_results = process_all_result_sets(cursor)
print(f"Retrieved {len(all_results)} result sets")
```

### Check if more result sets exist

Check the return value of `nextset()` in a loop to consume all result sets without knowing in advance how many there are:

```python
cursor.execute("""
    SELECT COUNT(*) AS ProductCount FROM Production.Product;
    SELECT COUNT(*) AS PersonCount FROM Person.Person;
""")

result_num = 1
while True:
    count = cursor.fetchval()
    print(f"Result set {result_num}: {count}")
    
    result_num += 1
    if not cursor.nextset():
        break
```

## Cursor description

### Access column metadata

After executing a query, `cursor.description` contains a sequence of 7-item tuples — one per column — with name, type code, display size, internal size, precision, scale, and nullability:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 10")

# Get column information
for col in cursor.description:
    print(f"Column: {col[0]}, Type: {col[1]}")

# description structure: (name, type_code, display_size, internal_size, 
#                        precision, scale, null_ok)
```

### Build dynamic result handlers

Build result handlers that work with any query by constructing the column list from `cursor.description` at runtime:

```python
def query_to_dicts(cursor) -> list[dict]:
    """Convert query results to list of dictionaries."""
    columns = [col[0] for col in cursor.description]
    return [dict(zip(columns, row)) for row in cursor.fetchall()]

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 10")
products = query_to_dicts(cursor)
for p in products:
    print(p["Name"])
```

### Handle queries with no results

`cursor.description` is `None` after non-SELECT statements such as INSERT, UPDATE, and DELETE. Check it before calling fetch methods:

```python
cursor.execute("CREATE TABLE #UpdDemo (Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)")
cursor.execute("INSERT INTO #UpdDemo VALUES ('Widget', 10.0, 5), ('Gadget', 20.0, 5)")
cursor.execute("UPDATE #UpdDemo SET Price = Price * 1.1 WHERE CategoryID = 5")

# description is None for non-SELECT statements
if cursor.description is None:
    print(f"Updated {cursor.rowcount} rows")
else:
    results = cursor.fetchall()
```

## Row count

### Track affected rows

After INSERT, UPDATE, or DELETE, `cursor.rowcount` returns the number of rows affected by the statement:

```python
cursor.execute("CREATE TABLE #RowDemo (Name NVARCHAR(50), Stock INT)")
cursor.execute("INSERT INTO #RowDemo VALUES ('A', 0), ('B', 5), ('C', 0)")
cursor.execute("UPDATE #RowDemo SET Stock = -1 WHERE Stock = 0")
print(f"Rows affected: {cursor.rowcount}")

cursor.execute("DELETE FROM #RowDemo WHERE Stock = -1")
print(f"Deleted {cursor.rowcount} rows")
```

### Handle unknown row count

```python
# Some operations might not return row count
cursor.execute("EXEC dbo.uspGetEmployeeManagers @BusinessEntityID = 5")

if cursor.rowcount == -1:
    print("Row count not available")
else:
    print(f"Affected {cursor.rowcount} rows")
```

## Skip rows

### Use skip for pagination alternative

`cursor.skip()` advances the cursor position without fetching rows. For large datasets, prefer SQL-level OFFSET-FETCH pagination for better performance:

```python
def get_page_using_skip(cursor, page: int, page_size: int):
    """Get a page of results using skip."""
    cursor.execute("SELECT * FROM Production.Product ORDER BY ProductID")
    
    # Skip rows from previous pages
    cursor.skip((page - 1) * page_size)
    
    # Fetch this page
    return cursor.fetchmany(page_size)

# Get page 3
page_3 = get_page_using_skip(cursor, page=3, page_size=20)
```

> [!NOTE]
> For large datasets, use SQL-level pagination (OFFSET-FETCH) instead of client-side skip, as it's more efficient.

## Diagnostic messages

### Access cursor.messages

The `messages` attribute stores informational messages generated during SQL statement execution, as described in [PEP 249](https://peps.python.org/pep-0249/). These messages include output from `PRINT` statements and `RAISERROR` with severity levels below 11.

The attribute is a list of tuples where each tuple contains a message type code and the message text:

```python
conn = mssql_python.connect(connection_string, autocommit=True)
cursor = conn.cursor()
cursor.execute("PRINT 'Hello world!'")
print(cursor.messages)
```

Output:

```output
[('[01000] (0)', '[Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Hello world!')]
```

The message text includes driver prefix information because the driver retrieves messages as diagnostic records through `SQLGetDiagRec`.

### Capture messages from stored procedures

Read `cursor.messages` after execution to retrieve any `PRINT` output or informational server messages from the previous statement:

```python
cursor.execute("EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = 5")
results = cursor.fetchall()

# Check for any informational messages
if cursor.messages:
    for msg_type, msg_text in cursor.messages:
        print(f"Server message: {msg_text}")
```

## Memory management

### Process large results efficiently

Fetch in batches using `fetchmany()` to process tables that are too large to load into memory at once:

```python
def process_large_table(cursor, batch_size: int = 10000):
    """Process large result set without loading all into memory."""
    cursor.execute("SELECT * FROM VeryLargeTable")
    
    total_processed = 0
    while True:
        rows = cursor.fetchmany(batch_size)
        if not rows:
            break
        
        for row in rows:
            process_row(row)
        
        total_processed += len(rows)
        print(f"Progress: {total_processed} rows processed")
    
    return total_processed
```

### Generator-based processing

Wrap batch fetching in a generator to process one row at a time while keeping memory usage constant regardless of result set size:

```python
def row_generator(cursor, batch_size: int = 1000):
    """Generate rows from cursor without loading all."""
    while True:
        rows = cursor.fetchmany(batch_size)
        if not rows:
            break
        for row in rows:
            yield row

cursor.execute("SELECT * FROM LargeTable")
for row in row_generator(cursor, batch_size=5000):
    # Process one row at a time
    print(row)  # Replace with your own row-handling logic
```

### Close cursors promptly

Always close cursors in a `finally` block to release server-side resources even if an exception occurs:

```python
def get_product(conn, product_id: int):
    """Get product and properly close cursor."""
    cursor = conn.cursor()
    try:
        cursor.execute(
            "SELECT * FROM Production.Product WHERE ProductID = %(id)s",
            {"id": product_id}
        )
        return cursor.fetchone()
    finally:
        cursor.close()
```

## Cursor state management

### Check if cursor has data

Test whether a query returned any rows by checking if `fetchone()` returns `None`:

```python
cursor.execute("SELECT ProductID, Name FROM Production.Product WHERE ProductID = 999")
row = cursor.fetchone()

if row is None:
    print("Product not found")
else:
    print(f"Found: {row.Name}")
```

### Reuse cursors

A single cursor can execute multiple queries sequentially. Each `execute()` call replaces the previous result set:

```python
cursor = conn.cursor()

# Execute multiple queries with same cursor
cursor.execute("SELECT TOP 5 * FROM Sales.Customer")
customers = cursor.fetchall()

cursor.execute("SELECT TOP 5 * FROM Production.Product")
products = cursor.fetchall()

cursor.execute("SELECT TOP 5 * FROM Sales.SalesOrderHeader")
orders = cursor.fetchall()

cursor.close()
```

## Best practices

### Pattern: Cursor helper class

Encapsulate cursor lifecycle management in a helper class to reduce boilerplate across your application:

```python
class CursorManager:
    """Helper for managing cursor lifecycle."""
    
    def __init__(self, connection):
        self.conn = connection
    
    def execute_and_fetch(self, query: str, params: dict = None) -> list:
        """Execute query and return all results."""
        cursor = self.conn.cursor()
        try:
            cursor.execute(query, params or {})
            return cursor.fetchall()
        finally:
            cursor.close()
    
    def execute_scalar(self, query: str, params: dict = None):
        """Execute query and return single value."""
        cursor = self.conn.cursor()
        try:
            cursor.execute(query, params or {})
            return cursor.fetchval()
        finally:
            cursor.close()
    
    def execute_non_query(self, query: str, params: dict = None) -> int:
        """Execute non-SELECT and return row count."""
        cursor = self.conn.cursor()
        try:
            cursor.execute(query, params or {})
            return cursor.rowcount
        finally:
            cursor.close()

# Usage
db = CursorManager(conn)
products = db.execute_and_fetch("SELECT TOP 5 Name FROM Production.Product")
count = db.execute_scalar("SELECT COUNT(*) FROM Production.Product")

db.execute_non_query("CREATE TABLE #Logs (LogID INT, Age INT)")
db.execute_non_query("INSERT INTO #Logs VALUES (1, 45), (2, 20), (3, 60)")
affected = db.execute_non_query("DELETE FROM #Logs WHERE Age > 30")
```

### Don't leave cursors open

A cursor that isn't explicitly closed holds server-side resources until the connection closes. Use `try/finally` to guarantee cleanup:

```python
# Bad: cursor left open
def get_data_bad(conn):
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM Data")
    return cursor.fetchall()
    # Cursor never closed!

# Good: always close cursor
def get_data_good(conn):
    cursor = conn.cursor()
    try:
        cursor.execute("SELECT * FROM Data")
        return cursor.fetchall()
    finally:
        cursor.close()
```

### Match cursor lifetime to operation

Create short-lived cursors for single operations. Reuse the same cursor only for a sequence of related operations:

```python
# Short-lived cursor for simple query
def get_user_count(conn) -> int:
    cursor = conn.cursor()
    try:
        cursor.execute("SELECT COUNT(*) FROM Person.Person")
        return cursor.fetchval()
    finally:
        cursor.close()

# Reuse cursor for related operations
def update_inventory(conn, items: list):
    cursor = conn.cursor()
    try:
        for item in items:
            cursor.execute(
                "UPDATE Inventory SET Quantity = %(qty)s WHERE ProductID = %(id)s",
                item
            )
        conn.commit()
    finally:
        cursor.close()
```

## Related content

- [Execute queries with mssql-python](executing-queries.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Implement pagination](pagination.md)
