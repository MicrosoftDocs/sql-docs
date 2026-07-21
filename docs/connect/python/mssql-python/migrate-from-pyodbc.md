---
title: Migrate from pyodbc to mssql-python
description: Guide for migrating existing Python applications from pyodbc to the mssql-python driver for Microsoft SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate from pyodbc to mssql-python

The mssql-python driver is Microsoft's first-party Python driver for Microsoft SQL. If you prefer a Microsoft-maintained driver option, it offers:

- No external ODBC driver dependency.
- Built-in connection pooling.
- Modern Python 3.10+ support.
- Native Microsoft Entra authentication.

## Key differences

| Feature | pyodbc | mssql-python |
| --------- | -------- | -------------- |
| Parameter style | `qmark` (`?`) | `qmark` (`?`) and `pyformat` (`%(name)s`) |
| ODBC driver required | Yes | No |
| Connection pooling | External | Built-in |
| Minimum Python | 3.6 | 3.10 |
| `callproc()` | Supported | Not implemented |
| Autocommit default | Off | Off |

## Basic migration steps

The following steps cover the key changes to migrate a pyodbc application to mssql-python.

### 1. Update imports

Replace the `pyodbc` import with `mssql_python`:

Before (pyodbc):

```python
import pyodbc
```

After (mssql-python):

```python
import mssql_python
```

### 2. Update connection strings

Remove the `DRIVER=` keyword and update the authentication method:

Before (pyodbc, requires ODBC driver):

```python
conn = pyodbc.connect(
    "DRIVER={ODBC Driver 18 for SQL Server};"
    "SERVER=localhost;"
    "DATABASE=AdventureWorks2022;"
    "Trusted_Connection=yes;"
)
```

After (mssql-python, no driver needed, using Microsoft Entra auth):

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

### 3. Keep your queries as-is

The mssql-python driver supports both `?` (qmark) and `%(name)s` (pyformat) parameter styles. Your existing `?` queries work without changes:

Before (pyodbc):

```python
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = ? AND Color = ?", (1, "Red"))
```

After (mssql-python, same query):

```python
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = ? AND Color = ?", (1, "Red"))
```

### 4. Keep executemany as-is

Existing `executemany` calls with tuples and `?` markers work without changes:

Before (pyodbc):

```python
cursor.execute("CREATE TABLE #MigrateDemo (ID INT, Name NVARCHAR(50))")
data = [(1, "Alice"), (2, "Bob"), (3, "Carol")]
cursor.executemany("INSERT INTO #MigrateDemo (ID, Name) VALUES (?, ?)", data)
```

After (mssql-python, same code):

```python
cursor.execute("DROP TABLE IF EXISTS #MigrateDemo")
cursor.execute("CREATE TABLE #MigrateDemo (ID INT, Name NVARCHAR(50))")
data = [(1, "Alice"), (2, "Bob"), (3, "Carol")]
cursor.executemany("INSERT INTO #MigrateDemo (ID, Name) VALUES (?, ?)", data)
```

## Stored procedure migration

The mssql-python driver doesn't implement `callproc()`. The following sections show how to use `EXECUTE` instead.

### Use EXECUTE for stored procedures

The pyodbc driver supports `callproc()`, but the mssql-python driver doesn't. Use `EXECUTE` instead:

Before (pyodbc):

```python
cursor.callproc("dbo.uspGetEmployeeManagers", (5,))
results = cursor.fetchall()
```

After (mssql-python):

```python
cursor.execute(
    "EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = %(id)s",
    {"id": 5}
)
results = cursor.fetchall()
print(f"Got {len(results)} rows")
```

### Output parameters

Use T-SQL variables to capture output values instead of relying on `callproc()` output parameters:

Before (pyodbc, using callproc):

```python
params = (category_id, pyodbc.SQL_INTEGER)
cursor.callproc("dbo.GetProductCount", params)
count = params[1].value
```

After (mssql-python, using T-SQL variables):

```python
cursor.execute(
    """
    DECLARE @count INT;
    SELECT @count = COUNT(*) FROM Production.Product
    WHERE ProductSubcategoryID = %(cat_id)s;
    SELECT @count AS ProductCount;
    """,
    {"cat_id": 1}
)
product_count = cursor.fetchval()
print(f"Product count: {product_count}")
```

## Feature-specific migrations

The following sections cover specific pyodbc features and their mssql-python equivalents.

### Connection strings

| pyodbc keyword | mssql-python keyword | Notes |
| --------------- | --------------------- | ------- |
| `DRIVER={...}` | Not needed | The ODBC driver is bundled internally. |
| `SERVER=` | `Server=` | No behavior change. |
| `DATABASE=` | `Database=` | No behavior change. |
| `Trusted_Connection=` | `Trusted_Connection=` | No behavior change. |
| `UID=` / `PWD=` | `UID=` / `PWD=` | No behavior change. |
| `Authentication=` | `Authentication=` | Accepts the same values. |

### Autocommit

The autocommit behavior is identical in both drivers:

pyodbc:

```python
conn.autocommit = True
pyodbc.connect(connection_string, autocommit=True)
```

mssql-python:

```python
conn.autocommit = True
```

### Bulk inserts

To speed up large `INSERT` batches, pyodbc users set `fast_executemany = True`. The mssql-python driver already optimizes `executemany` for parameterized batches, so moderate inserts need no special flag. For large data loads, prefer `bulkcopy()`, which streams rows over the bulk-copy protocol and is much faster than issuing individual `INSERT` statements. For the full workflow, see [Use bulk copy](bulk-copy.md).

pyodbc:

```python
cursor.fast_executemany = True
cursor.executemany(query, data)
```

After (mssql-python), moderate batches with `executemany`:

```python
cursor.execute("DROP TABLE IF EXISTS #BulkTarget")
cursor.execute("CREATE TABLE #BulkTarget (ID INT, Name NVARCHAR(50))")
data = [(i, f"Item {i}") for i in range(100)]
cursor.executemany("INSERT INTO #BulkTarget (ID, Name) VALUES (?, ?)", data)
conn.commit()
```

After (mssql-python), large loads with `bulkcopy` (preferred):

```python
cursor.execute("IF OBJECT_ID('##BulkTarget') IS NOT NULL DROP TABLE ##BulkTarget")
cursor.execute("CREATE TABLE ##BulkTarget (ID INT, Name NVARCHAR(50))")
conn.commit()  # Commit DDL before bulkcopy
data = [(i, f"Item {i}") for i in range(100)]
result = cursor.bulkcopy("##BulkTarget", data)
print(f"Bulk copied {result['rows_copied']} rows")
cursor.execute("DROP TABLE ##BulkTarget")
conn.commit()
```

### Row factory

The mssql-python driver returns `Row` objects that support attribute access by default, without requiring a custom row factory:

pyodbc (custom row factory):

```python
def namedtuple_row_factory(cursor):
    from collections import namedtuple
    columns = [col[0] for col in cursor.description]
    Row = namedtuple("Row", columns)
    return Row
```

mssql-python (attribute access by default):

```python
cursor.execute("SELECT Name, ListPrice FROM Production.Product")
row = cursor.fetchone()
print(row.Name)   # Attribute access works directly
print(row[0])     # Index access also works
```

## Error handling

The mssql-python driver uses the same exception hierarchy as pyodbc, so most exception handlers require only a module name change.

### Exception hierarchy

The exception class names map directly between drivers:

pyodbc:

```python
try:
    cursor.execute(query)
except pyodbc.Error as e:
    pass
except pyodbc.DatabaseError as e:
    pass
except pyodbc.OperationalError as e:
    pass
```

mssql-python:

```python
try:
    cursor.execute("SELECT TOP 1 * FROM Production.Product")
    print(cursor.fetchone())
except mssql_python.Error as e:
    pass
except mssql_python.DatabaseError as e:
    pass
except mssql_python.OperationalError as e:
    pass
```

### Error details

Both drivers expose error details through exception arguments:

pyodbc:

```python
try:
    cursor.execute(query)
except pyodbc.Error as e:
    sqlstate = e.args[0]
    message = e.args[1]
```

mssql-python:

```python
try:
    cursor.execute("SELECT TOP 1 * FROM NonExistentTable_XYZ")
except mssql_python.Error as e:
    # Error message contains SQLSTATE and details
    print(str(e))
```

## Connection pooling

The mssql-python driver includes connection pooling by default, so external pooling libraries are no longer required.

### Remove external pooling

If you used external pooling with pyodbc, the mssql-python driver has it built in:

Before (pyodbc external pool):

```python
from dbutils.pooled_db import PooledDB

pool = PooledDB(pyodbc, 5, driver="{ODBC Driver 18 for SQL Server}",
                server="your_server", database="your_database",
                uid="your_username", pwd="your_password")
conn = pool.connection()
```

After (mssql-python built-in pooling):

```python
conn = mssql_python.connect(connection_string)
conn.close()
```

### Configure pool

Override the default pool size and timeout with `mssql_python.pooling()`:

```python
import mssql_python

mssql_python.pooling()
```

## Complete migration example

The following shows the same function written with pyodbc and then rewritten with mssql-python.

### Before (pyodbc)

This version uses the pyodbc connection string with a `DRIVER` keyword:

```python
import pyodbc
from datetime import date

def get_orders(customer_id: int, start_date: date):
    conn = pyodbc.connect(
        "DRIVER={ODBC Driver 18 for SQL Server};"
        "SERVER=localhost;"
        "DATABASE=AdventureWorks2022;"
        "Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT SalesOrderID, OrderDate, TotalDue
        FROM Sales.SalesOrderHeader
        WHERE CustomerID = ? AND OrderDate >= ?
        ORDER BY OrderDate DESC
    """, (customer_id, start_date))

    orders = []
    for row in cursor:
        orders.append({
            "id": row.SalesOrderID,
            "date": row.OrderDate,
            "total": row.TotalDue
        })

    cursor.close()
    conn.close()
    return orders
```

### After (mssql-python)

This version removes the `DRIVER` keyword. All queries, parameters, and row access patterns remain identical:

```python
import mssql_python
from datetime import date

def get_orders(customer_id: int, start_date: date):
    conn = mssql_python.connect(
        "Server=localhost;"
        "Database=AdventureWorks2022;"
        "Trusted_Connection=yes;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT SalesOrderID, OrderDate, TotalDue
        FROM Sales.SalesOrderHeader
        WHERE CustomerID = ? AND OrderDate >= ?
        ORDER BY OrderDate DESC
    """, (customer_id, start_date))

    orders = []
    for row in cursor:
        orders.append({
            "id": row.SalesOrderID,
            "date": row.OrderDate,
            "total": row.TotalDue
        })

    cursor.close()
    conn.close()
    return orders
```

The only changes are the import statement and the connection string (no `DRIVER` keyword needed). Every query, parameter, fetch pattern, and row access stays identical.

## Testing migration

Before completing the migration, run the same queries against both drivers and compare results to confirm equivalent behavior.

### Verify equivalent behavior

Use a comparison function that runs the same query against both drivers and asserts the results match:

```python
import pyodbc
import mssql_python

def compare_results(pyodbc_conn_str: str, mssql_conn_str: str, query: str):
    """Compare results from both drivers."""
    # pyodbc query
    pyodbc_conn = pyodbc.connect(pyodbc_conn_str)
    pyodbc_cursor = pyodbc_conn.cursor()
    pyodbc_cursor.execute(query)
    pyodbc_results = pyodbc_cursor.fetchall()
    pyodbc_conn.close()
    
    # mssql-python query
    mssql_conn = mssql_python.connect(mssql_conn_str)
    mssql_cursor = mssql_conn.cursor()
    mssql_cursor.execute(query)
    mssql_results = mssql_cursor.fetchall()
    mssql_conn.close()
    
    # Compare
    assert len(pyodbc_results) == len(mssql_results)
    for p_row, m_row in zip(pyodbc_results, mssql_results):
        assert tuple(p_row) == tuple(m_row)
    
    print(f"Results match: {len(pyodbc_results)} rows")
```

## Checklist

- [ ] Update imports from `pyodbc` to `mssql_python`.
- [ ] Remove `DRIVER=` from connection strings.
- [ ] Keep existing `?` parameter queries (they work as-is).
- [ ] Use `EXECUTE` statements for stored procedure calls.
- [ ] Remove external connection pooling configuration.
- [ ] Update exception handling class names.
- [ ] Test all queries and stored procedures.
- [ ] Verify data type handling (especially decimals and dates).
- [ ] Remove ODBC driver from deployment requirements.

## Related content

- [Migrate from pymssql](migrate-from-pymssql.md)
- [Installation](installation.md)
- [Connection strings](connection-strings.md)
- [Executing queries](executing-queries.md)
- [Stored procedures](stored-procedures.md)
