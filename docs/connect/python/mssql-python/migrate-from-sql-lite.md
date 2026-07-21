---
title: Migrate from SQLite to Microsoft SQL with mssql-python
description: Guide for migrating Python applications from SQLite to Microsoft SQL using the mssql-python driver, covering SQL dialect differences and data migration.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate from SQLite to Microsoft SQL with mssql-python

SQLite is the default database for many Python projects, FastAPI, and Flask apps. It lets you build your application quickly by delaying the data platform decision until later. When it's time to take your application to production, you need support for concurrent users, role-based security, high availability, disaster recovery, and other enterprise features. You need to migrate to Microsoft SQL using the mssql-python driver.

## SQL dialect differences

When you migrate from SQLite to Microsoft SQL, you need to address two things: rewrite your SQL statements for Transact-SQL (T-SQL) and migrate your data.

The following table maps common SQLite patterns to their Microsoft SQL equivalents:

| SQLite | SQL Server (T-SQL) | Notes |
| --- | --- | --- |
| `INTEGER PRIMARY KEY AUTOINCREMENT` | `int IDENTITY(1,1) PRIMARY KEY` | Microsoft SQL uses `IDENTITY` for autoincrement. |
| `TEXT` | `nvarchar(255)` or `nvarchar(max)` | Always specify a length. Use `nvarchar` for Unicode. |
| `REAL` | `float` or `decimal(18,2)` | Use `decimal` for exact values like money. |
| `BLOB` | `varbinary(max)` | Same behavior, different name. |
| `BOOLEAN` (stored as INTEGER) | `bit` | Neither database has a native boolean. Both store `0`/`1`. |
| `DATETIME('now')` | `GETDATE()` or `SYSDATETIME()` | `SYSDATETIME()` gives higher precision. |
| `LIMIT 10 OFFSET 20` | `OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY` | Requires an `ORDER BY` clause. |
| `\|\|` (string concat) | `+` or `CONCAT()` | `CONCAT()` handles `NULL` values. |
| `IFNULL(a, b)` | `ISNULL(a, b)` or `COALESCE(a, b)` | `COALESCE` is ANSI-standard. |
| `GROUP_CONCAT(col)` | `STRING_AGG(col, ',')` | Available in SQL Server 2017+. |
| `INSERT OR REPLACE INTO` | `MERGE` statement | SQLite deletes and reinserts; `MERGE` updates in place. See example that follows. |
| `last_insert_rowid()` | `OUTPUT INSERTED.id` | Use `OUTPUT` in the `INSERT` statement. `SCOPE_IDENTITY()` also works but requires a separate `SELECT`. |
| `typeof(x)` | `SQL_VARIANT_PROPERTY(x, 'BaseType')` | Rarely needed with strict typing. |

### CREATE TABLE example

```sql
-- SQLite
CREATE TABLE IF NOT EXISTS products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price REAL DEFAULT 0.0,
    created_at TEXT DEFAULT (datetime('now')),
    is_active BOOLEAN DEFAULT 1
);

-- SQL Server
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'products')
CREATE TABLE products (
    id int IDENTITY(1,1) PRIMARY KEY,
    name nvarchar(100) NOT NULL,
    price decimal(10,2) DEFAULT 0.0,
    created_at datetime2 DEFAULT SYSDATETIME(),
    is_active bit DEFAULT 1
);
```

### Query examples

**Pagination:**

SQLite:

```python
cursor.execute("SELECT * FROM products ORDER BY name LIMIT ? OFFSET ?", (10, 20))
```

mssql-python:

```python
cursor.execute(
    "SELECT * FROM Production.Product ORDER BY Name OFFSET ? ROWS FETCH NEXT ? ROWS ONLY",
    (20, 10)
)
```

The parameter order is reversed. Transact-SQL puts `OFFSET` before `FETCH NEXT`.

**Upsert (insert or update):**

SQLite:

```python
cursor.execute("""
    INSERT OR REPLACE INTO settings (key, value)
    VALUES (?, ?)
""", (key, value))
```

mssql-python:

```python
cursor.execute("""
    MERGE #Settings AS target
    USING (SELECT ? AS [key], ? AS value) AS source
    ON target.[key] = source.[key]
    WHEN MATCHED THEN UPDATE SET value = source.value
    WHEN NOT MATCHED THEN INSERT ([key], value) VALUES (source.[key], source.value);
""", (key, value))
```

The `USING` clause defines `source.[key]` and `source.value` as column aliases. The `WHEN` clauses reference those aliases. Only two `?` markers are needed.

**Last inserted ID:**

SQLite:

```python
cursor.execute("INSERT INTO products (name) VALUES (?)", ("Widget",))
product_id = cursor.lastrowid
```

mssql-python:

```python
cursor.execute(
    "INSERT INTO #Products (Name) OUTPUT INSERTED.ProductID VALUES (?)",
    ("Widget",)
)
product_id = cursor.fetchval()
```

### Update connection code

Replace `sqlite3.connect()` with `mssql_python.connect()`:

SQLite:

```python
import sqlite3

def get_connection():
    conn = sqlite3.connect("myapp.db")
    conn.row_factory = sqlite3.Row
    return conn
```

mssql-python:

```python
import mssql_python

def get_connection():
    return mssql_python.connect(
        "Server=<server>.database.windows.net;"
        "Database=<database>;"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes;"
    )
```

Row access works similarly. SQLite's `Row` factory returns dict-like rows with `row["column"]` syntax. The mssql-python driver returns `Row` objects that support the same string-key access, plus attribute and index access:

SQLite (with `row_factory`):

```python
row["name"]
```

mssql-python:

```python
row["name"]   # String-key access, like SQLite
row.name      # Attribute access
row[0]        # Index access
```

### Update parameter style

Both SQLite and mssql-python use `?` as the parameter marker, so most queries work without changes.

```python
# Works with both sqlite3 and mssql-python
cursor.execute("SELECT * FROM Production.Product WHERE Name = ?", ("Adjustable Race",))
```

The one difference: SQLite allows named parameters with `:name` syntax. The mssql-python driver uses `%(name)s` instead.

SQLite:

```python
cursor.execute("SELECT * FROM products WHERE id = :id", {"id": 42})
```

mssql-python:

```python
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = %(id)s", {"id": 42})
```

## Migrate existing data

Important considerations when migrating data from SQLite to Microsoft SQL:

- Microsoft SQL stores **nvarchar(max)** and **varbinary(max)** values as large objects (LOBs), which are slower to read and write than in-row data. Keep string columns at **nvarchar(4000)** or less whenever your data allows. SQL Server stores these values directly in the data row, avoiding the LOB overhead.
- The type mapping follows SQLite's [type affinity rules](https://www.sqlite.org/datatype3.html). Review the generated tables after migration to tighten column sizes (for example, **nvarchar(100)** instead of **nvarchar(4000)**) or add constraints that SQLite didn't enforce.
- For SQLite tables that use **TEXT** to store dates, you might need to parse the values into Python `datetime` objects before inserting. Microsoft SQL expects proper datetime values, not text strings.

Use this script to read the schema and data from SQLite and create matching tables in Microsoft SQL:

```python
import sqlite3
import mssql_python

# SQLite type affinity -> Microsoft SQL type
# Keywords follow SQLite's type affinity rules
TYPE_MAP = {
    "INT": "bigint",
    "CHAR": "nvarchar(4000)",
    "CLOB": "nvarchar(max)",
    "TEXT": "nvarchar(4000)",
    "BLOB": "varbinary(max)",
    "REAL": "float",
    "FLOA": "float",
    "DOUB": "float",
}


def map_type(sqlite_type: str) -> str:
    """Map a SQLite column type to a Microsoft SQL type."""
    upper = (sqlite_type or "TEXT").upper()
    for prefix, sql_type in TYPE_MAP.items():
        if prefix in upper:
            return sql_type
    return "decimal(18,6)"  # NUMERIC affinity (default)


# Connect to both databases
sqlite_conn = sqlite3.connect("myapp.db")
sql_conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)

# Get list of tables from SQLite
sqlite_cur = sqlite_conn.cursor()
sqlite_cur.execute(
    "SELECT name FROM sqlite_master "
    "WHERE type='table' AND name NOT LIKE 'sqlite_%'"
)
tables = [row[0] for row in sqlite_cur.fetchall()]

sql_cursor = sql_conn.cursor()

for table in tables:
    # Read column info from SQLite
    sqlite_cur.execute(f"PRAGMA table_info([{table}])")
    columns = sqlite_cur.fetchall()
    # columns: (cid, name, type, notnull, default_value, pk)

    # Build CREATE TABLE statement
    col_defs = []
    for col in columns:
        name, col_type, notnull, pk = col[1], col[2], col[3], col[5]
        sql_type = map_type(col_type)
        parts = [f"[{name}] {sql_type}"]
        if notnull:
            parts.append("NOT NULL")
        if pk:
            parts.append("PRIMARY KEY")
        col_defs.append(" ".join(parts))

    create_sql = f"CREATE TABLE [{table}] ({', '.join(col_defs)})"
    sql_cursor.execute(
        f"IF OBJECT_ID('{table}','U') IS NULL " + create_sql
    )
    sql_conn.commit()

    # Read all rows from SQLite
    sqlite_cur.execute(f"SELECT * FROM [{table}]")
    rows = sqlite_cur.fetchall()

    if not rows:
        print(f"  {table}: created (empty)")
        continue

    # Use bulkcopy for fast insert
    result = sql_cursor.bulkcopy(table, rows)
    print(f"  {table}: {result['rows_copied']} rows copied")

sql_conn.commit()
sqlite_conn.close()
sql_conn.close()
```

## Feature differences

After migration, your application gains access to Microsoft SQL features that SQLite doesn't support:

| Feature | SQLite | SQL Server |
| --- | --- | --- |
| Concurrent writes | Single writer at a time | Full concurrency with row-level locking |
| Authentication | File permissions only | SQL auth, Windows auth, Microsoft Entra ID |
| Stored procedures | Not supported | Full T-SQL programmability |
| Encryption | Not built in | TLS in transit, TDE at rest |
| Transactions | Savepoints, basic isolation levels | Full isolation levels, distributed transactions |
| JSON support | `json_extract()` | `OPENJSON()`, `JSON_VALUE()`, `FOR JSON` |
| Full-text search | FTS5 extension | Built-in full-text indexing |
| Max database size | ~281 TB (practical limit lower) | 524 PB |
| Connection pooling | N/A (in-process) | Built-in with mssql-python |

## Related content

- [Install mssql-python](installation.md)
- [Connection strings](connection-strings.md)
- [Bulk copy](bulk-copy.md)
- [Parameterized queries](parameterized-queries.md)
- [Pagination](pagination.md)
- [Use mssql-python with Flask](flask-integration.md)
