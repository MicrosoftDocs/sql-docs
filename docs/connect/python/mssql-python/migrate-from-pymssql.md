---
title: Migrate from pymssql to mssql-python
description: Guide for migrating existing Python applications from pymssql to the mssql-python driver for Microsoft SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate from pymssql to mssql-python

The mssql-python driver is Microsoft's first-party Python driver for Microsoft SQL. If you prefer a Microsoft-maintained driver option, it offers:

- No FreeTDS dependency.
- Multiple concurrent cursors per connection.
- Built-in connection pooling.
- Modern Python 3.10+ support.
- Native Microsoft Entra authentication.
- Row objects with attribute access by default.

## Key differences

| Feature | pymssql | mssql-python |
| --------- | --------- | -------------- |
| Parameter style | `format` (`%s`, `%d`) | `qmark` (`?`) and `pyformat` (`%(name)s`) |
| Native library | FreeTDS | Direct Database Connectivity (DDBC), installed as a package dependency |
| Connection pooling | External | Built-in |
| Cursors per connection | 1 | Multiple |
| Minimum Python | 3.6 | 3.10 |
| `callproc()` | Supported | Not implemented |
| `as_dict` cursor | Extension | Row objects (default) |
| Bulk copy | `conn.bulk_copy()` | `cursor.bulkcopy()` |
| Autocommit default | Off | Off |

## Basic migration steps

The following steps walk through the most common changes needed to migrate a pymssql application to mssql-python.

### 1. Update imports

Replace the `pymssql` import with `mssql_python`:

Before (pymssql):

```python
import pymssql
```

After (mssql-python):

```python
import mssql_python
```

### 2. Update connection calls

pymssql uses positional arguments. The mssql-python driver uses a connection string or keyword arguments:

Before (pymssql, positional arguments):

```python
conn = pymssql.connect("<server>", "<user>", "<password>", "<database>")
```

Before (pymssql, keyword arguments):

```python
conn = pymssql.connect(
    host=r"<server>\<instance>",
    user="<login>",
    password="<password>",
    database="<database>"
)
```

After (mssql-python, connection string):

```python
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "UID=<username>;"
    "PWD=<password>;"
    "Encrypt=yes;"
)
```

After (mssql-python, Microsoft Entra recommended):

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

### 3. Update parameter markers

pymssql uses `%s` and `%d` format placeholders. The mssql-python driver uses `?` (qmark) or `%(name)s` (pyformat):

Before (pymssql):

```python
cursor.execute("SELECT * FROM Person.Person WHERE BusinessEntityID = %d AND FirstName = %s", (user_id, name))
```

After (mssql-python, qmark style):

```python
user_id, name = 1, "Ken"
cursor.execute("SELECT * FROM Person.Person WHERE BusinessEntityID = ? AND FirstName = ?", (user_id, name))
print(cursor.fetchone())

cursor.execute(
    "SELECT * FROM Person.Person WHERE BusinessEntityID = %(id)s AND FirstName = %(name)s",
    {"id": user_id, "name": name}
)
print(cursor.fetchone())
```

### 4. Update executemany

Update the SQL placeholders from `%s`/`%d` to `?` or `%(name)s`:

Before (pymssql):

```python
cursor.execute("CREATE TABLE #Persons (ID INT, Name NVARCHAR(50), Department NVARCHAR(50))")
cursor.executemany(
    "INSERT INTO #Persons VALUES (%d, %s, %s)",
    [(1, "John", "Sales"), (2, "Jane", "Marketing")]
)
```

After (mssql-python, qmark style):

```python
cursor.execute("IF OBJECT_ID('#Persons') IS NOT NULL DROP TABLE #Persons")
cursor.execute("CREATE TABLE #Persons (ID INT, Name NVARCHAR(50), Department NVARCHAR(50))")
cursor.executemany(
    "INSERT INTO #Persons VALUES (?, ?, ?)",
    [(1, "John", "Sales"), (2, "Jane", "Marketing")]
)
cursor.execute("SELECT * FROM #Persons")
for row in cursor:
    print(row)
```

### 5. Use Row attributes instead of as_dict cursors

pymssql requires `as_dict=True` to access columns by name. The mssql-python driver returns `Row` objects that support both attribute and index access by default:

Before (pymssql):

```python
cursor = conn.cursor(as_dict=True)
cursor.execute("SELECT BusinessEntityID, FirstName FROM Person.Person WHERE FirstName = %s", ("John",))
for row in cursor:
    print("ID=%d, Name=%s" % (row["BusinessEntityID"], row["FirstName"]))
```

After (mssql-python, attribute access by default):

```python
cursor = conn.cursor()
cursor.execute("SELECT BusinessEntityID, FirstName FROM Person.Person WHERE FirstName = ?", ("John",))
for row in cursor:
    print(f"ID={row.BusinessEntityID}, Name={row.FirstName}")
    # Index access also works: row[0], row[1]
```

## Stored procedure migration

The mssql-python driver doesn't implement `callproc()`. Use `EXECUTE` statements instead.

### Use EXECUTE for stored procedures

pymssql supports `callproc()`, but the mssql-python driver doesn't. Use `EXECUTE` instead:

Before (pymssql):

```python
cursor.callproc("uspGetEmployeeManagers", (5,))
for row in cursor:
    print(row)
```

After (mssql-python):

```python
cursor.execute("EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = ?", (5,))
for row in cursor:
    print(row)
```

### Output parameters

Use T-SQL variables to capture output values instead of relying on `callproc()` output parameters:

Before (pymssql):

```python
cursor.callproc("GetProductCount", (category_id,))
count = cursor.fetchval()
```

After (mssql-python, T-SQL variables):

```python
cursor.execute("""
    DECLARE @count INT;
    SELECT @count = COUNT(*) FROM Production.Product
    WHERE ProductSubcategoryID = ?;
    SELECT @count AS ProductCount;
""", (1,))
product_count = cursor.fetchval()
print(f"Product count: {product_count}")
```

## Bulk copy migration

pymssql calls `bulk_copy()` on the connection. The mssql-python driver calls `bulkcopy()` on the cursor with more options:

Before (pymssql):

```python
conn.bulk_copy("##BulkDemo", [(1, 2)] * 1000)
conn.commit()
```

After (mssql-python):

```python
cursor = conn.cursor()
cursor.execute("CREATE TABLE ##BulkDemo (Col1 INT, Col2 INT)")
conn.commit()
result = cursor.bulkcopy("##BulkDemo", [(1, 2)] * 1000)
print(f"Copied {result['rows_copied']} rows")
conn.commit()
cursor.execute("DROP TABLE ##BulkDemo")
conn.commit()
```

The mssql-python `bulkcopy()` method supports `batch_size`, `timeout`, `column_mappings`, `keep_identity`, `check_constraints`, `table_lock`, `keep_nulls`, `fire_triggers`, and `use_internal_transaction`. See [Bulk copy](bulk-copy.md) for details.

## Multiple cursors

pymssql allows only one active cursor per connection. The mssql-python driver supports multiple concurrent cursors:

Before (pymssql):

```python
c1 = conn.cursor()
c1.execute("SELECT TOP 5 * FROM Person.Person")
c2 = conn.cursor()
c2.execute("SELECT TOP 5 * FROM Sales.SalesOrderHeader")
c1.fetchall()
```

After (mssql-python):

```python
c1 = conn.cursor()
c1.execute("SELECT TOP 5 BusinessEntityID, FirstName FROM Person.Person")
persons = c1.fetchall()

c2 = conn.cursor()
c2.execute("SELECT TOP 5 SalesOrderID FROM Sales.SalesOrderHeader")
orders = c2.fetchall()
print(f"Persons: {len(persons)}, Orders: {len(orders)}")
```

## Connection pooling

pymssql has no built-in pooling. The mssql-python driver includes it automatically:

Before (pymssql, external pool required):

```python
from dbutils.pooled_db import PooledDB
pool = PooledDB(pymssql, host="server", user="user", password="pwd", database="db")
conn = pool.connection()
```

After (mssql-python, pooling is automatic):

```python
conn = mssql_python.connect(connection_string)
conn.close()
```

## Error handling

The mssql-python driver uses the same exception hierarchy as pymssql, so most exception handlers require only a module name change:

Before (pymssql):

```python
try:
    cursor.execute(query)
except pymssql.OperationalError as e:
    print(f"Operation failed: {e}")
except pymssql.InterfaceError as e:
    print(f"Interface error: {e}")
```

After (mssql-python):

```python
try:
    cursor = conn.cursor()
    cursor.execute("SELECT TOP 1 * FROM Production.Product")
    row = cursor.fetchone()
    print(row)
except mssql_python.OperationalError as e:
    print(f"Operation failed: {e}")
except mssql_python.InterfaceError as e:
    print(f"Interface error: {e}")
```

## Complete migration example

The following shows the same function written with pymssql and then rewritten with mssql-python.

### Before (pymssql)

This version uses positional connect arguments, `as_dict=True`, and `%d` parameter markers:

```python
import pymssql

def get_orders(customer_id: int):
    conn = pymssql.connect("<server>", "<user>", "<password>", "<database>")
    cursor = conn.cursor(as_dict=True)

    cursor.execute("""
        SELECT TOP 10 SalesOrderID, OrderDate, TotalDue
        FROM Sales.SalesOrderHeader
        WHERE CustomerID = %d
        ORDER BY OrderDate DESC
    """, (customer_id,))

    orders = []
    for row in cursor:
        orders.append({
            "id": row["SalesOrderID"],
            "date": row["OrderDate"],
            "total": row["TotalDue"]
        })

    cursor.close()
    conn.close()
    return orders
```

### After (mssql-python)

The key structural changes are parameter markers, connection style, and row access:

```python
import mssql_python

def get_orders(customer_id: int):
    conn = mssql_python.connect(
        "Server=<server>;"
        "Database=<database>;"
        "UID=<username>;"
        "PWD=<password>;"
    )
    cursor = conn.cursor()

    cursor.execute("""
        SELECT TOP 10 SalesOrderID, OrderDate, TotalDue
        FROM Sales.SalesOrderHeader
        WHERE CustomerID = ?
        ORDER BY OrderDate DESC
    """, (customer_id,))

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

The structural changes are:

1. `import pymssql` → `import mssql_python`.
1. Positional connect arguments → connection string with keywords.
1. `%d` parameter marker → `?`.
1. `cursor(as_dict=True)` → `cursor()` with attribute access (`row.SalesOrderID` instead of `row["SalesOrderID"]`).

## Checklist

- [ ] Update imports from `pymssql` to `mssql_python`.
- [ ] Convert connection calls from positional arguments to connection strings.
- [ ] Convert `%s`/`%d` parameter markers to `?` or `%(name)s`.
- [ ] Use `EXECUTE` statements for stored procedure calls.
- [ ] Use `Row` attribute access instead of `as_dict=True` cursors.
- [ ] Migrate `conn.bulk_copy()` to `cursor.bulkcopy()`.
- [ ] Remove external connection pooling configuration.
- [ ] Remove FreeTDS from deployment requirements.
- [ ] Update exception handling class names.
- [ ] Test all queries and stored procedures.

## Related content

- [Migrate from pyodbc to mssql-python](migrate-from-pyodbc.md)
- [Install mssql-python](installation.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Execute queries with mssql-python](executing-queries.md)
- [Call stored procedures with mssql-python](stored-procedures.md)
