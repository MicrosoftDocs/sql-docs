---
title: Execute Queries with mssql-python
description: Learn how to execute parameterized SQL queries, batch operations, and prepared statements using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Execute queries with mssql-python

The mssql-python driver provides cursor methods for SQL query execution, parameterized queries, batch operations, and prepared statements.

## Basic query execution

Use a cursor's `execute()` method to run SQL statements:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("SELECT Name, ListPrice FROM Production.Product WHERE Color = 'Black'")
rows = cursor.fetchall()

for row in rows:
    print(row.Name, row.ListPrice)

cursor.close()
conn.close()
```

## Parameterized queries

Always use parameterized queries to prevent SQL injection. The driver's default paramstyle is `pyformat` (named placeholders), but it also supports `qmark` (positional placeholders). Use `qmark` for ODBC `{CALL}` escape sequences.

### Pyformat style (default)

Use named placeholders with `%(name)s` syntax and pass a dictionary:

```python
cursor.execute(
    "SELECT Name, ListPrice FROM Production.Product WHERE Color = %(color)s AND ListPrice > %(price)s",
    {"color": "Black", "price": 10.00}
)
```

### Qmark style

Use positional placeholders with `?` and pass a tuple or list:

```python
cursor.execute(
    "SELECT Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = ? AND ListPrice > ?",
    (1, 10.00)
)
```

The driver automatically detects the parameter style based on your SQL query and parameter types.

## INSERT, UPDATE, DELETE operations

For data modification statements, use parameterized queries and commit the transaction:

```python
cursor.execute("CREATE TABLE #ExecDemo (Name NVARCHAR(50), CategoryID INT, Price DECIMAL(10,2))")
cursor.execute(
    "INSERT INTO #ExecDemo (Name, CategoryID, Price) VALUES (%(name)s, %(category)s, %(price)s)",
    {"name": "New Product", "category": 1, "price": 19.99}
)
conn.commit()

print(f"Rows affected: {cursor.rowcount}")
```

## Batch execution with executemany()

Use `executemany()` to efficiently insert multiple rows. The driver uses column-wise parameter binding for high performance:

```python
products = [
    {"name": "Product A", "category": 1, "price": 10.00},
    {"name": "Product B", "category": 1, "price": 15.00},
    {"name": "Product C", "category": 2, "price": 20.00},
]

cursor.execute("CREATE TABLE #BatchDemo (Name NVARCHAR(50), CategoryID INT, Price DECIMAL(10,2))")
cursor.executemany(
    "INSERT INTO #BatchDemo (Name, CategoryID, Price) VALUES (%(name)s, %(category)s, %(price)s)",
    products
)
conn.commit()

print(f"Rows inserted: {cursor.rowcount}")
```

With qmark style:

```python
products = [
    ("Product A", 1, 10.00),
    ("Product B", 1, 15.00),
    ("Product C", 2, 20.00),
]

cursor.execute("CREATE TABLE #QmarkDemo (Name NVARCHAR(50), CategoryID INT, Price DECIMAL(10,2))")
cursor.executemany(
    "INSERT INTO #QmarkDemo (Name, CategoryID, Price) VALUES (?, ?, ?)",
    products
)
conn.commit()
```

## Multi-statement batch execution

Use `batch_execute()` on the connection to execute multiple different statements in a single call:

```python
results, cursor = conn.batch_execute(
    [
        "CREATE TABLE #BatchExec (Name NVARCHAR(50), CategoryID INT)",
        "INSERT INTO #BatchExec (Name, CategoryID) VALUES (%(name)s, %(cat)s)",
        "SELECT COUNT(*) FROM #BatchExec"
    ],
    [
        None,                                 # No params for CREATE
        {"name": "New Item", "cat": 1},       # Params for INSERT
        None                                  # No params for SELECT
    ]
)

print(f"CREATE result: {results[0]}")
print(f"INSERT affected: {results[1]} rows")
print(f"Row count: {results[2][0][0]}")
```

## Prepared statements

The driver prepares queries by default (`use_prepare=True`). When you execute the same SQL string multiple times on the same cursor, the driver automatically reuses the prepared statement on subsequent calls:

```python
# First execution prepares the statement
cursor.execute(
    "SELECT Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = %(subcategory_id)s",
    {"subcategory_id": 1},
)
rows1 = cursor.fetchall()

# Same SQL string on same cursor → driver reuses the prepared plan
cursor.execute(
    "SELECT Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = %(subcategory_id)s",
    {"subcategory_id": 2},
)
rows2 = cursor.fetchall()
```

To skip preparation and use direct execution instead:

```python
cursor.execute(
    "SELECT Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = 1",
    use_prepare=False  # Uses SQLExecDirectW instead of SQLPrepareW
)
```

## Connection-level execute

For simple one-off queries, use `execute()` directly on the connection:

```python
# Creates cursor, executes, and returns cursor
cursor = conn.execute("SELECT TOP 10 Name, ListPrice FROM Production.Product")
rows = cursor.fetchall()
cursor.close()
```

## Stored procedures

Call stored procedures by using `EXECUTE` or the ODBC `{CALL}` escape syntax. For information about output parameters, multiple result sets, and transaction patterns, see [Stored procedures](stored-procedures.md).

```python
cursor.execute(
    "EXECUTE dbo.uspGetManagerEmployees @BusinessEntityID = %(business_entity_id)s",
    {"business_entity_id": 16}
)
rows = cursor.fetchall()
```

## Set input sizes

Use `setinputsizes()` to declare parameter types explicitly, which can improve performance for batch operations:

```python
cursor.setinputsizes([
    (mssql_python.SQL_WVARCHAR, 50, 0),   # NVARCHAR(50)
    (mssql_python.SQL_INTEGER, 0, 0),     # INT
])

cursor.executemany(
    "SELECT ProductID, Name FROM Production.Product WHERE Name LIKE ? AND ProductSubcategoryID = ?",
    [("Road%", 2), ("Mountain%", 1)]
)
```

> [!NOTE]
> Not all SQL type constants work with `setinputsizes()`. `SQL_WVARCHAR` and `SQL_INTEGER` are reliable. For decimal values, use the driver's automatic type inference rather than `SQL_DECIMAL`, which has a known issue ([GitHub #503](https://github.com/microsoft/mssql-python/issues/503)).

## Error handling

Wrap database operations in try-except blocks:

```python
try:
    cursor.execute("CREATE TABLE #ErrDemo (Name NVARCHAR(50) NOT NULL)")
    cursor.execute("INSERT INTO #ErrDemo (Name) VALUES (%(name)s)", {"name": None})
    conn.commit()
except mssql_python.IntegrityError as e:
    print(f"Constraint violation: {e}")
    conn.rollback()
except mssql_python.ProgrammingError as e:
    print(f"SQL error: {e}")
    conn.rollback()
```

## Best practices

1. **Always use parameterized queries** to prevent SQL injection.
1. **Use [bulk copy](bulk-copy.md)** for bulk inserts instead of multiple `execute()` calls.
1. **Commit transactions explicitly** when you disable autocommit.
1. **Close cursors and connections** when done to release resources.
1. **Use context managers** for automatic resource cleanup:

```python
with mssql_python.connect(connection_string) as conn:
    with conn.cursor() as cursor:
        cursor.execute("SELECT TOP 5 Name, ListPrice FROM Production.Product")
        rows = cursor.fetchall()
# Connection and cursor automatically closed
```

## Related content

- [Retrieve data with mssql-python](retrieving-data.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Transaction management with mssql-python](transaction-management.md)
- [Manage cursors and result sets](cursor-management.md)
