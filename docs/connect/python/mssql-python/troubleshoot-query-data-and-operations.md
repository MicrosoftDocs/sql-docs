---
title: Troubleshoot Query, Data, and Operation Issues with mssql-python
description: Diagnose and resolve mssql-python query, data type, performance, transaction, and bulk copy issues.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sumitsar
ms.date: 09/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot query, data, and operation issues with mssql-python

Use this article to diagnose query execution, data type, performance, transaction, and bulk copy issues with the `mssql-python` driver.

## Query execution issues

### Table or object not found

**Symptoms:**

```text
ProgrammingError: [42S02] (208) Invalid object name 'TableName'.
```

**Possible causes and solutions:**

- **Incorrect database context**

  ```python
  cursor.execute("SELECT DB_NAME()")
  print(cursor.fetchone()[0])
  ```

- **Schema not specified**

  ```python
  cursor.execute("SELECT * FROM dbo.TableName")
  ```

- **Table doesn't exist**

  ```python
  cursor.execute("""
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME = 'TableName'
  """)
  ```

### Syntax error

**Symptoms:**

```text
ProgrammingError: [42000] (102) Incorrect syntax near '...'.
```

**Solutions:**

1. Test the SQL statement in SQL Server Management Studio (SSMS) to verify the syntax.
1. Use a parameterized query instead of string interpolation:

   ```python
   # Don't use string interpolation for query parameters.
   cursor.execute(f"SELECT * FROM Production.Product WHERE Name = '{name}'")

   # Use parameters.
   cursor.execute(
       "SELECT * FROM Production.Product WHERE Name = %(name)s",
       {"name": name},
   )
   ```

### Parameter errors

**Symptoms:**

```text
ProgrammingError: [07001] Wrong number of parameters
```

**Solutions:**

1. Count the placeholders and parameters. The counts must match.
1. Choose the correct parameter style:

   ```python
   # Qmark style: positional parameters
   cursor.execute(
       "SELECT * FROM Production.Product "
       "WHERE ProductID = ? AND Name LIKE ?",
       (1, "Adjustable%"),
   )
   print(cursor.fetchone())

   # Pyformat style: named parameters
   cursor.execute(
       "SELECT * FROM Production.Product "
       "WHERE ProductID = %(id)s AND Name LIKE %(name)s",
       {"id": 1, "name": "Adjustable%"},
   )
   print(cursor.fetchone())
   ```

## Data type issues

### Datetime conversion errors

**Symptoms:**

```text
DataError: [22007] Invalid datetime format
```

**Solution:**

Use Python `datetime` objects instead of strings.

```python
from datetime import datetime

cursor.execute("CREATE TABLE #Events (EventDate DATETIME)")

# This value raises an error because the date is invalid.
try:
    cursor.execute(
        "INSERT INTO #Events (EventDate) VALUES (%(event_date)s)",
        {"event_date": "2024-13-45"},
    )
except Exception as e:
    print(f"Expected error: {e}")

# Use a Python datetime object.
cursor.execute(
    "INSERT INTO #Events (EventDate) VALUES (%(event_date)s)",
    {"event_date": datetime(2024, 3, 15)},
)
cursor.execute("SELECT EventDate FROM #Events")
print(cursor.fetchone())
```

### Decimal precision issues

**Symptoms:**

Numbers appear truncated or rounded incorrectly.

**Solution:**

Use `decimal.Decimal` for precise numeric values:

```python
from decimal import Decimal

cursor.execute("CREATE TABLE #PriceDemo (ListPrice DECIMAL(10, 2))")
cursor.execute(
    "INSERT INTO #PriceDemo (ListPrice) VALUES (%(list_price)s)",
    {"list_price": Decimal("19.99")},
)
```

### Unicode encoding issues

**Symptoms:**

Special characters appear garbled or cause errors.

**Solutions:**

1. Use **nvarchar** columns for Unicode data in your database.
1. Pass strings directly. The driver handles encoding:

   ```python
   cursor.execute("CREATE TABLE #UnicodeDemo (Name NVARCHAR(50))")
   cursor.execute(
       "INSERT INTO #UnicodeDemo (Name) VALUES (%(name)s)",
       {"name": "日本語"},
   )
   cursor.execute("SELECT Name FROM #UnicodeDemo")
   print(cursor.fetchone())
   ```

## Performance issues

### Slow query execution

**Possible causes and solutions:**

- **Missing indexes:** Check the query execution plan in SSMS.
- **Large result sets:** Use `fetchmany()` instead of `fetchall()`:

  ```python
  cursor.arraysize = 1000
  while True:
      rows = cursor.fetchmany()
      if not rows:
          break
      process_rows(rows)
  ```

- **Connection pooling disabled:** Enable pooling:

  ```python
  import mssql_python

  mssql_python.pooling(max_size=20, idle_timeout=300)
  ```

### Memory issues with large results

**Symptoms:**

The Python process runs out of memory.

**Solutions:**

1. Stream results instead of loading all rows into memory.

   ```python
   cursor.execute("SELECT * FROM LargeTable")
   for row in cursor:
       process_row(row)
   ```

1. Use server-side pagination.

   ```python
   page_size = 1000
   offset = 0

   while True:
       cursor.execute(
           "SELECT * FROM LargeTable ORDER BY ID "
           "OFFSET ? ROWS FETCH NEXT ? ROWS ONLY",
           (offset, page_size),
       )
       rows = cursor.fetchall()
       if not rows:
           break
       process_rows(rows)
       offset += page_size
   ```

## Transaction issues

### Temp table scoping with autocommit

Session temp tables (`#tablename`) you create inside a transaction disappear when the transaction rolls back. This behavior commonly causes confusion when autocommit is off, which is the default:

```python
conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("CREATE TABLE #TempData (ID INT, Name NVARCHAR(50))")
cursor.execute("INSERT INTO #TempData VALUES (1, 'test')")

# An explicit rollback or an error removes #TempData.
conn.rollback()

# This statement fails with "Invalid object name '#TempData'".
cursor.execute("SELECT * FROM #TempData")
```

Commit immediately after you create a temp table, or use autocommit mode:

```python
cursor.execute("CREATE TABLE #TempData (ID INT, Name NVARCHAR(50))")
conn.commit()

cursor.execute("INSERT INTO #TempData VALUES (1, 'test')")
conn.commit()
```

DDL statements that require autocommit mode, such as `CREATE DATABASE`, fail inside an open transaction. Set autocommit before you run them:

```python
conn.autocommit = True
cursor.execute("CREATE DATABASE TestDB")
conn.autocommit = False
```

### Transaction not committed

**Symptoms:**

Data changes don't persist after you close the connection.

**Solution:**

With `autocommit=False`, which is the default, call `commit()`:

```python
cursor.execute("CREATE TABLE #Products (Name NVARCHAR(100))")
cursor.execute(
    "INSERT INTO #Products (Name) VALUES (%(name)s)",
    {"name": "Widget"},
)
conn.commit()
```

Alternatively, use autocommit mode:

```python
conn = mssql_python.connect(connection_string, autocommit=True)
```

### Deadlock errors

**Symptoms:**

```text
OperationalError: [40001] (1205) Transaction ... was deadlocked on lock resources with another process
```

**Solution:**

[Retry logic](retry-logic.md) handles the immediate failure, but recurring deadlocks indicate a design problem. Capture the deadlock graph, and analyze the statements and lock types. Common fixes include these changes:

- Reorder operations so competing transactions acquire locks in the same sequence.
- Reduce the transaction scope.
- Add appropriate indexes to reduce lock duration.

For a full walkthrough of deadlock analysis, see [Deadlocks guide](../../../relational-databases/sql-server-deadlocks-guide.md). If you use Azure SQL Database, see [Analyze and prevent deadlocks](/azure/azure-sql/database/analyze-prevent-deadlocks).

## Bulk load issues

### Constraint violations during bulkcopy

**Symptoms:**

```text
RuntimeError: CHECK constraint ... Conflict occurred in database ...
RuntimeError: Cannot insert duplicate key ... violation of PRIMARY KEY constraint
```

**Cause:**

Data in your batch violates table constraints such as primary key, unique, check, or foreign key constraints.

**Solution:**

Validate data before you load it. For large datasets, load the data into a staging table, and then merge it into the target:

```python
cursor.execute("CREATE TABLE ##Staging (ID INT, Name NVARCHAR(100))")
cursor.bulkcopy("##Staging", rows)

# Check for duplicate rows before the merge.
cursor.execute("""
    SELECT s.ID
    FROM ##Staging AS s
    INNER JOIN dbo.Target AS t
        ON s.ID = t.ID
""")
dupes = cursor.fetchall()
if dupes:
    print(f"Skipping {len(dupes)} duplicate rows")

# Insert rows that don't exist in the target.
cursor.execute("""
    INSERT INTO dbo.Target (ID, Name)
    SELECT s.ID, s.Name
    FROM ##Staging AS s
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Target AS t
        WHERE t.ID = s.ID
    )
""")
conn.commit()
```

For upsert patterns with staging tables, see [Data loading and movement patterns](data-loading-movement-patterns.md).

### Column mapping errors

**Symptoms:**

```text
RuntimeError: Bulk copy failure - column count mismatch
```

**Cause:**

The number of columns in your data doesn't match the target table column count, or the columns are in the wrong order.

**Solution:**

Ensure that your data matches the table schema in order and count:

```python
from decimal import Decimal

cursor.execute("""
    SELECT COLUMN_NAME, DATA_TYPE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'MyTable'
    ORDER BY ORDINAL_POSITION
""")
for col in cursor.fetchall():
    print(col)

rows = [
    (1, "Widget", Decimal("19.99")),
    (2, "Gadget", Decimal("29.99")),
]
cursor.bulkcopy("dbo.MyTable", rows)
```

### Type mismatches during bulkcopy

**Symptoms:**

Data loads, but values are truncated, rounded, or incorrect.

**Cause:**

Python values don't map cleanly to the target column types. Common examples include `float` values loaded into **decimal** columns, which can lose precision, and oversized strings loaded into fixed-length columns.

**Solution:**

Use Python types that match your schema:

```python
from decimal import Decimal

rows = [
    # Use Decimal for decimal and numeric columns.
    (1, "Widget", Decimal("19.99")),
    # Avoid float values because they can lose precision.
    # (1, "Widget", 19.99),
]
cursor.bulkcopy("dbo.Products", rows)
```

## NumPy type binding failures

**Symptoms:**

Parameters silently fail or raise data type errors when you use NumPy integer or float types.

**Cause:**

NumPy types such as `numpy.int64` and `numpy.int32` don't pass `isinstance(x, int)` in NumPy 2.x. The driver's type inference doesn't recognize them, which causes unexpected behavior.

**Solution:**

Convert NumPy values to native Python types before you bind them:

```python
import numpy as np

cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductID = %(product_id)s",
    {"product_id": int(np.int64(42))},
)

for _, row in df.iterrows():
    cursor.execute(
        "INSERT INTO #Orders (ProductID, Qty) "
        "VALUES (%(product_id)s, %(qty)s)",
        {
            "product_id": int(row["ProductID"]),
            "qty": int(row["Qty"]),
        },
    )
```

For larger datasets, use the [Arrow](arrow-integration.md) or [pandas](pandas-integration.md) integration paths. These paths handle type conversion internally.

## Bulk copy with temp tables

**Symptoms:**

`cursor.bulkcopy("#TempTable", data)` raises `RuntimeError: Invalid object name '#TempTable'`.

**Cause:**

`bulkcopy()` can't resolve session temp tables (`#tablename`) because of metadata lookup limitations. Global temp tables (`##tablename`) and permanent tables work.

**Solution:**

Use a global temp table or a regular staging table:

```python
# A global temp table is visible to all sessions and is dropped
# when the last session disconnects.
cursor.execute("CREATE TABLE ##Staging (ID INT, Name NVARCHAR(50))")
cursor.bulkcopy("##Staging", rows)

# Alternatively, use a permanent staging table.
cursor.execute("CREATE TABLE dbo.Staging (ID INT, Name NVARCHAR(50))")
cursor.bulkcopy("dbo.Staging", rows)
```

For small datasets where you prefer a session temp table, use `executemany()`:

```python
cursor.execute("CREATE TABLE #Staging (ID INT, Name NVARCHAR(50))")
cursor.executemany(
    "INSERT INTO #Staging (ID, Name) VALUES (?, ?)",
    rows,
)
```

## Related content

- [Execute queries with mssql-python](executing-queries.md)
- [Data type mappings with mssql-python](data-type-mappings.md)
- [Manage transactions with mssql-python](transaction-management.md)
- [Performance tuning with mssql-python](performance-tuning.md)
- [Bulk copy with mssql-python](bulk-copy.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Troubleshoot mssql-python](troubleshooting.md)
