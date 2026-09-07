---
title: Migrate from PostgreSQL to Microsoft SQL with mssql-python
description: Guide for migrating Python applications from PostgreSQL to Microsoft SQL using the mssql-python driver, covering installation, SQL dialect differences, and data migration.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate from PostgreSQL to Microsoft SQL with mssql-python

Many Python teams first learn PostgreSQL. When your workload needs features like [temporal tables](/sql/relational-databases/tables/temporal-tables), full MERGE semantics, or [columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview), migrate to Microsoft SQL. This guide covers the main decisions and code changes for moving a Python application from PostgreSQL (using psycopg2 or psycopg3) to Microsoft SQL by using the `mssql-python` driver.

> [!NOTE]
> If you're migrating from Azure Database for PostgreSQL, both services support Microsoft Entra authentication and managed identity. The code changes in this guide apply regardless of whether your PostgreSQL source is self-managed or Azure-hosted.

## What you gain by moving to Microsoft SQL

Microsoft SQL includes capabilities that simplify security, compliance, and operations for production workloads. Understand these features before you start migrating so you can take advantage of them during the transition:

- **[Dynamic data masking](/sql/relational-databases/security/dynamic-data-masking) and [row-level security](/sql/relational-databases/security/row-level-security).** Mask columns for users who don't need full access, and restrict row visibility by security policy. These features work with any driver.
- **[Temporal tables](/sql/relational-databases/tables/temporal-tables) (system-versioned).** Microsoft SQL tracks row history automatically. No triggers, no audit tables, no application code.
- **Full MERGE semantics.** A single statement handles INSERT, UPDATE, and DELETE with an OUTPUT clause for audit trails. PostgreSQL's `ON CONFLICT` clause covers only insert-or-update on a single constraint.
- **[Columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview).** Add columnar storage to existing tables for hybrid OLTP/analytics workloads. No separate analytics database needed.
- **Microsoft Entra ID authentication.** Connect with managed identities, service principals, or interactive sign-in. Azure Database for PostgreSQL also supports Microsoft Entra authentication, so if you're already using it, the transition is straightforward.

## Install the driver

Before you begin, ensure you have Python 3.10 or later, and a target SQL database.

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

PostgreSQL drivers require external native libraries.  

```bash
# psycopg2 requires pg_config, libpq-dev, and platform-specific build tools
sudo apt-get install libpq-dev  # Debian/Ubuntu
pip install psycopg2
```

The `mssql-python` driver bundles its native layer. On Windows, you don't need an external driver manager or system packages.  

```bash
pip install mssql-python
```

On Linux and macOS, install a small set of system libraries documented in [Installation](installation.md#platform-specific-notes). There's no equivalent to `pg_config` or `libpq-dev`.

## Update connection code

The following sections cover the key changes to connection strings, authentication, context managers, and pooling.

### Connection strings

psycopg2 uses a DSN string or keyword arguments.  

```python
import psycopg2

conn = psycopg2.connect(
    host="<server>",
    dbname="<database>",
    user="<username>",
    password="<password>"
)
```

mssql-python also supports keyword arguments, which avoids the URL-encoding problems that SQLAlchemy connection strings often have when passwords contain `@`, `;`, or `{}` characters.  

```python
import mssql_python

conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)
```

Or use a connection string.  

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

For the full set of connection string keywords, see [Connection strings](connection-strings.md).

### Authentication

PostgreSQL authentication typically uses `pg_hba.conf` rules with username and password. Azure Database for PostgreSQL also supports Microsoft Entra authentication. Microsoft SQL supports multiple authentication modes through a single connection keyword:

| PostgreSQL approach | mssql-python equivalent |
| --- | --- |
| Username and password | `UID=...;PWD=...;` |
| SSL/TLS encryption | `Encrypt=yes;` (enabled by default for Azure SQL) |
| Entra auth (Azure PostgreSQL) | `Authentication=ActiveDirectoryDefault;` (passwordless) |
| Managed identity (Azure PostgreSQL) | `Authentication=ActiveDirectoryMSI;` |
| Service principal (Azure PostgreSQL) | `Authentication=ActiveDirectoryServicePrincipal;` |

Use `ActiveDirectoryDefault` for local development. It chains through Azure CLI, environment variables, and managed identity automatically. For production, use a specific mode like `ActiveDirectoryMSI` (managed identity) or `ActiveDirectoryServicePrincipal` to avoid the slow credential chain walk. See [Microsoft Entra authentication](entra-authentication.md) for all seven authentication modes.

### Context managers

Both drivers support context managers, but the behavior differs:

psycopg2's `with conn:` commits on success and rolls back on exception, but does **not** close the connection:

```python
with psycopg2.connect(...) as conn:
    with conn.cursor() as cur:
        cur.execute("INSERT INTO ...")
    # conn.commit() happens automatically on success
# Connection is still open here
conn.close()  # Must close explicitly
```

mssql-python's `with conn:` closes the connection on exit. Uncommitted work is rolled back:

```python
with mssql_python.connect(...) as conn:
    with conn.cursor() as cursor:
        cursor.execute("INSERT INTO ...")
    conn.commit()
# Connection is closed here
```

### Connection pooling

psycopg2 requires explicit setup and management of a connection pool.

```python
from psycopg2 import pool

connection_pool = pool.ThreadedConnectionPool(1, 10, dsn="...")
conn = connection_pool.getconn()
# ... use conn ...
connection_pool.putconn(conn)
```

The `mssql-python` driver has built-in pooling enabled by default. No setup is needed.

```python
# Pooling is automatic. Each connect() call reuses pooled connections.
conn = mssql_python.connect(...)
```

Configure the pool size if the defaults don't fit your workload.

```python
import mssql_python

mssql_python.pooling(max_size=20, idle_timeout=300)
```

For guidance on pool sizing and troubleshooting pool exhaustion, see [Connection pooling](connection-pooling.md).

## SQL dialect differences

The following table maps common PostgreSQL patterns to their Transact-SQL (T-SQL) equivalents:

| PostgreSQL | SQL Server (T-SQL) | Notes |
| --- | --- | --- |
| `SERIAL` / `BIGSERIAL` | `int IDENTITY(1,1)` | Microsoft SQL uses `IDENTITY` for autoincrement. |
| `TEXT` | `nvarchar(max)` | Use `nvarchar` for Unicode. Prefer `nvarchar(4000)` or shorter when the data allows. |
| `BOOLEAN` | `bit` | PostgreSQL accepts `true`/`false`; Microsoft SQL uses `1`/`0`. |
| `BYTEA` | `varbinary(max)` | Same concept, different name. |
| `JSONB` | `nvarchar(max)` with JSON functions | Microsoft SQL stores JSON as text and validates with `ISJSON()`. See [JSON data](json-data.md). |
| `TIMESTAMP WITH TIME ZONE` | `datetimeoffset` | Both store offset. See [Datetime handling](datetime-handling.md). |
| `INTERVAL` | No direct equivalent | Calculate with `DATEADD()` and `DATEDIFF()`. |
| `ARRAY` | No direct equivalent | Use a separate table, a JSON array, or `STRING_SPLIT()`. |
| `UUID` | `uniqueidentifier` | The `mssql-python` driver maps `uuid.UUID` natively. See [Module configuration](module-configuration.md#native_uuid). |
| `NOW()` / `CURRENT_TIMESTAMP` | `GETDATE()` or `SYSDATETIME()` | `SYSDATETIME()` gives higher precision. |
| `LIMIT 10 OFFSET 20` | `OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY` | Requires an `ORDER BY` clause. |
| `\|\|` (string concat) | `+` or `CONCAT()` | `CONCAT()` handles `NULL` values. |
| `COALESCE(a, b)` | `COALESCE(a, b)` or `ISNULL(a, b)` | `COALESCE` is identical in both. |
| `string_agg(col, ',')` | `STRING_AGG(col, ',')` | Available in SQL Server 2017+. |
| `RETURNING id` | `OUTPUT INSERTED.id` | Use `OUTPUT` in the `INSERT`, `UPDATE`, or `DELETE` statement. |
| `ON CONFLICT ... DO UPDATE` | `MERGE` statement | `MERGE` supports INSERT + UPDATE + DELETE in one statement. See [Query rewrite patterns](#query-rewrite-patterns). |
| `EXPLAIN ANALYZE` | `SET STATISTICS IO ON; SET STATISTICS TIME ON;` | Or use execution plans in SSMS / Azure Data Studio. |
| `\d tablename` | `sp_help 'tablename'` | Or query `INFORMATION_SCHEMA.COLUMNS`. |
| `pg_dump` | `bcp`, `BACKUP DATABASE` | Use `bulkcopy()` for programmatic data loading from Python. |

### CREATE TABLE example

PostgreSQL:

```sql
CREATE TABLE IF NOT EXISTS products (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    price NUMERIC(10, 2) DEFAULT 0.0,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    metadata JSONB,
    is_active BOOLEAN DEFAULT TRUE
);
```

SQL Server:

```sql
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'products')
CREATE TABLE products (
    id int IDENTITY(1,1) PRIMARY KEY,
    name nvarchar(100) NOT NULL,
    price decimal(10,2) DEFAULT 0.0,
    created_at datetimeoffset DEFAULT SYSDATETIMEOFFSET(),
    metadata nvarchar(max),
    is_active bit DEFAULT 1
);
```

## Query rewrite patterns

The following sections show common PostgreSQL query patterns and their T-SQL equivalents.

### Pagination

PostgreSQL:

```python
cursor.execute("SELECT * FROM products ORDER BY name LIMIT %s OFFSET %s", (10, 20))
```

mssql-python:

```python
cursor.execute(
    "SELECT * FROM Production.Product ORDER BY Name OFFSET ? ROWS FETCH NEXT ? ROWS ONLY",
    (20, 10)
)
```

The parameter order is reversed. Microsoft SQL puts `OFFSET` before `FETCH NEXT`.

### Upsert (insert or update)

PostgreSQL's `ON CONFLICT` handles insert-or-update on a single constraint:

```python
cursor.execute("""
    INSERT INTO settings (key, value)
    VALUES (%s, %s)
    ON CONFLICT (key) DO UPDATE SET value = EXCLUDED.value
""", (key, value))
```

Microsoft SQL's `MERGE` handles INSERT, UPDATE, and DELETE in one statement. Use a `USING` clause with parameter aliases:

```python
cursor.execute("""
    MERGE #Settings AS target
    USING (SELECT ? AS [key], ? AS value) AS source
    ON target.[key] = source.[key]
    WHEN MATCHED THEN UPDATE SET value = source.value
    WHEN NOT MATCHED THEN INSERT ([key], value) VALUES (source.[key], source.value);
""", (key, value))
```

For bulk upserts, stage the rows in a temporary table by using `bulkcopy()`, and then `MERGE` from it. For more information, see [Bulk upsert with a staging table](data-loading-movement-patterns.md#bulk-upsert-with-a-staging-table).

### Get inserted ID

PostgreSQL:

```python
cursor.execute(
    "INSERT INTO products (name) VALUES (%s) RETURNING id",
    ("Widget",)
)
product_id = cursor.fetchone()[0]
```

mssql-python:

```python
cursor.execute(
    "INSERT INTO #Products (Name) OUTPUT INSERTED.ProductID VALUES (%(name)s)",
    {"name": "Widget"}
)
product_id = cursor.fetchval()
```

`OUTPUT INSERTED` works with `INSERT`, `UPDATE`, and `DELETE` statements. It can return multiple columns.

### Parameter markers

psycopg2 uses `%s` for positional parameters and `%(name)s` for named parameters. The `mssql-python` driver uses `?` for positional and `%(name)s` for named:

psycopg2:

```python
cursor.execute("SELECT * FROM products WHERE id = %s", (42,))
cursor.execute("SELECT * FROM products WHERE id = %(id)s", {"id": 42})
```

mssql-python:

```python
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = ?", (42,))
cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductID = %(id)s", {"id": 42}
)
```

## Transaction and autocommit differences

PostgreSQL (psycopg2) opens a transaction automatically on the first command and requires an explicit `commit()`:

```python
conn = psycopg2.connect(...)
cursor = conn.cursor()
cursor.execute("INSERT INTO ...")
conn.commit() 
```

The `mssql-python` driver works the same way by default. Autocommit is off, and you call `commit()` explicitly:

```python
conn = mssql_python.connect(...)
cursor = conn.cursor()
cursor.execute("INSERT INTO ...")
conn.commit()
```

To enable autocommit:

psycopg2:

```python
conn = psycopg2.connect(...)
conn.autocommit = True
```

mssql-python:

```python
conn = mssql_python.connect(..., autocommit=True)
# or: conn.autocommit = True
```

See [Transaction management](transaction-management.md) for isolation levels, savepoints, and deadlock retry patterns.

## Type considerations

The following sections cover the most common type mapping differences between PostgreSQL and Microsoft SQL.

### JSON

PostgreSQL has native `JSONB` with indexing and query operators (`->`, `->>`, `@>`). Microsoft SQL stores JSON as **nvarchar(max)** and provides functions for querying:

| PostgreSQL | SQL Server |
| --- | --- |
| `data->>'name'` | `JSON_VALUE(data, '$.name')` |
| `data->'items'` | `JSON_QUERY(data, '$.items')` |
| `data @> '{"active": true}'` | `JSON_VALUE(data, '$.active') = 'true'` |
| `jsonb_array_length(data)` | `(SELECT COUNT(*) FROM OPENJSON(data))` |

In Python, both approaches use `json.dumps()` to serialize:

```python
import json

cursor.execute(
    "INSERT INTO #Settings ([key], data) VALUES (%(key)s, %(data)s)",
    {"key": "config", "data": json.dumps({"theme": "dark", "lang": "en"})}
)
```

See [JSON data](json-data.md) for full guidance on JSON storage and querying patterns.

### UUID

Both PostgreSQL and mssql-python map `uuid.UUID` natively:

```python
import uuid

cursor.execute(
    "INSERT INTO #Events (EventID, Name) VALUES (%(event_id)s, %(name)s)",
    {"event_id": uuid.uuid4(), "name": "signup"}
)
```

See [Module configuration](module-configuration.md#native_uuid) for the `native_uuid` connection option.

### Datetime and timezone

PostgreSQL's `TIMESTAMPTZ` converts to UTC on storage. Microsoft SQL's `datetimeoffset` preserves the original offset:

```python
from datetime import datetime, timezone, timedelta

eastern = timezone(timedelta(hours=-5))
dt = datetime(2025, 6, 15, 14, 30, tzinfo=eastern)

# PostgreSQL stores as UTC: 2025-06-15 19:30:00+00
# SQL Server stores as-is: 2025-06-15 14:30:00-05:00
cursor.execute("INSERT INTO #Events (EventTime) VALUES (%(event_time)s)", {"event_time": dt})
```

If you need consistent UTC storage, convert in Python before inserting:

```python
dt_utc = dt.astimezone(timezone.utc)
cursor.execute("INSERT INTO #Events (EventTime) VALUES (%(event_time)s)", {"event_time": dt_utc})
```

See [Datetime handling](datetime-handling.md) for the full type mapping.

### Arrays

PostgreSQL supports native array columns (`INTEGER[]`, `TEXT[]`). Microsoft SQL doesn't have an array type. Common alternatives:

1. **Separate table** (normalized). Best for queryable, indexed data.
1. **JSON array** stored in **nvarchar(max)**. Good for opaque metadata.
1. **Comma-separated string** with `STRING_SPLIT()`. Simple but limited.

```python
# Option 1: Normalized table
cursor.execute("INSERT INTO #ProductTags (ProductID, Tag) VALUES (%(product_id)s, %(tag)s)", {"product_id": 1, "tag": "electronics"})
cursor.execute("INSERT INTO #ProductTags (ProductID, Tag) VALUES (%(product_id)s, %(tag)s)", {"product_id": 1, "tag": "sale"})

# Option 2: JSON array
import json
tags = json.dumps(["electronics", "sale"])
cursor.execute("INSERT INTO #Products (Name, Tags) VALUES (%(name)s, %(tags)s)", {"name": "Widget", "tags": tags})
```

### Unicode

PostgreSQL stores all text as UTF-8 by default. Microsoft SQL distinguishes between **varchar** (code page encoding) and **nvarchar** (UTF-16). The `mssql-python` driver sends Python `str` values as **nvarchar** by default, so Unicode text works without extra configuration. If your schema uses **varchar** columns and you need to avoid the implicit conversion, use `setinputsizes()` to specify the column type. See [String and Unicode data](string-unicode.md) for encoding details.

## Bulk loading and data movement

PostgreSQL uses `COPY` for bulk operations. mssql-python provides `bulkcopy()`:

psycopg2:

```python
with open("data.csv") as f:
    cursor.copy_expert("COPY products FROM STDIN CSV HEADER", f)
```

mssql-python:

```python
import csv

with open("data.csv", newline="") as f:
    reader = csv.reader(f)
    next(reader)  # Skip header
    rows = [tuple(row) for row in reader]

cursor.bulkcopy("##Products", rows)
```

For large files, use a generator to avoid loading the entire file into memory:

```python
import csv

def csv_rows(path):
    with open(path, newline="") as f:
        reader = csv.reader(f)
        next(reader)  # Skip header
        for row in reader:
            yield tuple(row)

cursor.bulkcopy("##Products", csv_rows("data.csv"), batch_size=5000)
```

See [Bulk copy operations](bulk-copy.md) for column mappings, identity handling, and performance tips.

## Schema and data migration

Use this approach to migrate an existing PostgreSQL database:

1. **Export the schema.** Use `pg_dump --schema-only` to get DDL. For option details and edge cases (ownership, privileges, extensions, and filtering), see the PostgreSQL [`pg_dump` reference](https://www.postgresql.org/docs/current/app-pgdump.html). Rewrite the DDL using the [SQL dialect differences](#sql-dialect-differences) table.
1. **Create tables in Microsoft SQL.** Run the rewritten DDL against your target database.
1. **Export data.** Use `pg_dump --data-only --format=csv` or query each table with psycopg2. For large datasets and compatibility switches, review the PostgreSQL [`pg_dump` documentation](https://www.postgresql.org/docs/current/app-pgdump.html), especially the options section.
1. **Load data with bulkcopy.** Read the destination column order from the catalog so you don't hardcode a column list per table, then stream each table into Microsoft SQL. Here's an example script:

```python
import json
import psycopg2
from psycopg2 import sql
import mssql_python

pg_conn = psycopg2.connect(host="<pgserver>", dbname="<database>", user="<username>", password="<password>")
sql_conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)

def table_columns(cursor, table):
    """Return the ordered column names and identity column from the catalog."""
    cursor.execute(
        "SELECT c.name, c.is_identity FROM sys.columns AS c "
        "WHERE c.object_id = OBJECT_ID(?) ORDER BY c.column_id",
        (table,)
    )
    columns, identity = [], None
    for name, is_identity in cursor.fetchall():
        columns.append(name)
        if is_identity:
            identity = name
    return columns, identity

def parse_pg_table_name(qualified_name):
    """Split a PostgreSQL table name into schema and table parts."""
    if "." in qualified_name:
        schema_name, table_name = qualified_name.split(".", 1)
    else:
        schema_name, table_name = "public", qualified_name
    return schema_name, table_name

def parse_sql_table_name(qualified_name):
    """Split a SQL Server table name into schema and table parts."""
    if "." in qualified_name:
        schema_name, table_name = qualified_name.split(".", 1)
    else:
        schema_name, table_name = "dbo", qualified_name
    return schema_name, table_name

def dependency_order(pg_cursor, table_names, schema_name="public"):
    """Topologically sort tables by foreign key dependencies."""
    table_set = set(table_names)
    incoming = {name: 0 for name in table_set}
    edges = {name: set() for name in table_set}

    pg_cursor.execute(
        """
        SELECT
            child.relname AS child_table,
            parent.relname AS parent_table
        FROM pg_constraint c
        JOIN pg_class child ON c.conrelid = child.oid
        JOIN pg_namespace child_ns ON child.relnamespace = child_ns.oid
        JOIN pg_class parent ON c.confrelid = parent.oid
        JOIN pg_namespace parent_ns ON parent.relnamespace = parent_ns.oid
        WHERE c.contype = 'f'
          AND child_ns.nspname = %s
          AND parent_ns.nspname = %s
        """,
        (schema_name, schema_name),
    )

    for child, parent in pg_cursor.fetchall():
        if child in table_set and parent in table_set and child != parent:
            if child not in edges[parent]:
                edges[parent].add(child)
                incoming[child] += 1

    ready = sorted([name for name, degree in incoming.items() if degree == 0])
    ordered = []

    while ready:
        current = ready.pop(0)
        ordered.append(current)
        for neighbor in sorted(edges[current]):
            incoming[neighbor] -= 1
            if incoming[neighbor] == 0:
                ready.append(neighbor)
        ready.sort()

    # If cycles remain, process remaining tables alphabetically.
    if len(ordered) < len(table_set):
        remaining = sorted(table_set - set(ordered))
        ordered.extend(remaining)

    return ordered

def discover_table_pairs(pg_cursor, sql_cursor, pg_schema="public", sql_schema="dbo"):
    """Find tables that exist in both PostgreSQL and SQL Server, in dependency order."""
    pg_cursor.execute(
        """
        SELECT table_name
        FROM information_schema.tables
        WHERE table_schema = %s AND table_type = 'BASE TABLE'
        """,
        (pg_schema,),
    )
    pg_tables = {row[0] for row in pg_cursor.fetchall()}

    sql_cursor.execute(
        """
        SELECT t.name
        FROM sys.tables AS t
        JOIN sys.schemas AS s ON t.schema_id = s.schema_id
        WHERE s.name = ?
        """,
        (sql_schema,),
    )
    sql_tables = {row[0] for row in sql_cursor.fetchall()}

    common_tables = sorted(pg_tables & sql_tables)
    ordered_tables = dependency_order(pg_cursor, common_tables, schema_name=pg_schema)

    return [(f"{pg_schema}.{name}", f"{sql_schema}.{name}") for name in ordered_tables]

def source_columns(pg_cursor, source_table):
    """Return ordered source columns from PostgreSQL information_schema."""
    schema_name, table_name = parse_pg_table_name(source_table)
    pg_cursor.execute(
        """
        SELECT column_name
        FROM information_schema.columns
        WHERE table_schema = %s AND table_name = %s
        ORDER BY ordinal_position
        """,
        (schema_name, table_name),
    )
    return [row[0] for row in pg_cursor.fetchall()]

def migrate_table(pg_cursor, sql_cursor, source_table, dest_table):
    # The destination defines the authoritative column order for positional bulkcopy().
    dest_columns, identity = table_columns(sql_cursor, dest_table)
    if not dest_columns:
        raise RuntimeError(
            f"No destination columns found for {dest_table}. "
            "Make sure the destination table exists before migration."
        )

    src_columns = source_columns(pg_cursor, source_table)
    if not src_columns:
        raise RuntimeError(
            f"No source columns found for {source_table}. "
            "Check the source table name and schema."
        )

    # Load only columns present on both sides and keep destination column order.
    src_column_set = set(src_columns)
    load_columns = [c for c in dest_columns if c in src_column_set]
    if not load_columns:
        raise RuntimeError(
            f"No shared columns between {source_table} and {dest_table}."
        )

    source_schema, source_name = parse_pg_table_name(source_table)
    select_query = sql.SQL("SELECT {cols} FROM {schema}.{table}").format(
        cols=sql.SQL(", ").join(sql.Identifier(c) for c in load_columns),
        schema=sql.Identifier(source_schema),
        table=sql.Identifier(source_name),
    )
    pg_cursor.execute(select_query)

    copied = 0
    while True:
        batch = pg_cursor.fetchmany(10000)
        if not batch:
            break
        # Serialize JSONB or array values (dict/list) for nvarchar(max) columns.
        rows = [
            tuple(json.dumps(v) if isinstance(v, (dict, list)) else v for v in row)
            for row in batch
        ]
        # keep_identity preserves source primary keys so foreign keys still line up.
        result = sql_cursor.bulkcopy(
            dest_table,
            rows,
            batch_size=10000,
            keep_identity=identity in load_columns,
        )
        copied += result["rows_copied"]
    return copied

pg_cursor = pg_conn.cursor()
sql_cursor = sql_conn.cursor()

# Leave TABLE_MAPPINGS as None to migrate every table that exists in both schemas.
# To migrate only selected tables, replace None with explicit mappings.
TABLE_MAPPINGS = None

if TABLE_MAPPINGS is None:
    tables = discover_table_pairs(pg_cursor, sql_cursor, pg_schema="public", sql_schema="dbo")
else:
    tables = TABLE_MAPPINGS

if not tables:
    raise RuntimeError(
        "No shared tables found between source and destination schemas. "
        "Check schema names and table creation on SQL Server."
    )

print(f"Migrating {len(tables)} table(s)...")
for source_table, dest_table in tables:
    count = migrate_table(pg_cursor, sql_cursor, source_table, dest_table)
    print(f"{dest_table}: copied {count} rows")

# bulkcopy() bypasses constraint checks, so foreign keys are left untrusted.
# Re-validate each table to mark them trusted and surface any orphaned rows.
for _, dest_table in tables:
    dest_schema, dest_name = parse_sql_table_name(dest_table)
    sql_cursor.execute(
        f"ALTER TABLE [{dest_schema}].[{dest_name}] WITH CHECK CHECK CONSTRAINT ALL"
    )
sql_conn.commit()

pg_conn.close()
sql_conn.close()
```

By default, this script migrates every table that exists in both `public` (PostgreSQL) and `dbo` (SQL Server), ordered by foreign key dependencies. Set `TABLE_MAPPINGS` to an explicit list if you want to migrate only a subset.

This assumes the source and destination use the same column names, which is the usual case after you rewrite the DDL. The helper handles the identity column automatically: `keep_identity` preserves the source primary keys when the destination has an `IDENTITY` column, so foreign key references stay intact. To let SQL Server assign new keys instead, exclude the identity column from `columns` and pass `keep_identity=False`.

### Foreign keys and constraints

`bulkcopy()` uses the TDS bulk insert protocol, which doesn't enforce foreign key or check constraints during the load. Without an explicit request to check them, SQL Server ignores `CHECK` and `FOREIGN KEY` constraints during a bulk import and marks them not trusted afterward, as described in [BULK INSERT](../../../t-sql/statements/bulk-insert-transact-sql.md#check_constraints). This behavior has two practical consequences for migration:

- **Load order doesn't matter.** You can load a child table before its parent without hitting foreign key violations. Preserve the primary keys with `keep_identity=True`, as the helper does, so parent and child key values still match after the load.
- **Constraints end up untrusted.** After a bulk load, each foreign key is marked *not trusted* (`sys.foreign_keys.is_not_trusted = 1`) because SQL Server didn't verify it. The final step in the script re-validates every loaded table with `ALTER TABLE ... WITH CHECK CHECK CONSTRAINT ALL`. This step marks the constraints trusted so the query optimizer can use them, and it surfaces bad data. If a child row references a missing parent, the statement fails with an integrity constraint violation that names the constraint, so you can fix the orphaned rows before you go live.

## Limitations

Review these differences before migrating:

| Topic | PostgreSQL | mssql-python / SQL Server |
| --- | --- | --- |
| `callproc()` | Supported | Raises `NotSupportedError`. Use `cursor.execute("EXECUTE ...")` instead. |
| Table-valued parameters (TVPs) | No direct equivalent | Not supported in the current driver. Use temp tables or JSON for multi-row parameters. |
| Native `ARRAY` columns | Supported | No array type. Use normalized tables, JSON arrays, or `STRING_SPLIT()`. |
| `LISTEN`/`NOTIFY` | Supported | No direct equivalent. Use Service Broker or application-level polling. |
| `COPY` streaming | Supported | Use `bulkcopy()` for bulk data loading. |
| Returning modified rows | `RETURNING` clause | `OUTPUT INSERTED` / `OUTPUT DELETED` clause in DML statements. |
| Async driver | `psycopg3` has native async | `mssql-python` async support is workaround-oriented (thread pool). |
| Full-text search | `tsvector` / `tsquery` | `CONTAINS()` / `FREETEXT()` with full-text indexes. |
| ORM (SQLAlchemy) | Fully supported | Supported via the built-in mssql-python dialect in [SQLAlchemy 2.1.0b2+](sqlalchemy-integration.md) (pre-release). |

## Validation checklist

Use this checklist to verify your migration:

1. Replace all `%s` parameter markers with `?` or `%(name)s` parameters.
1. Ensure all `%(name)s` parameters still work (both drivers support this format).
1. Rewrite `LIMIT`/`OFFSET` to `OFFSET`/`FETCH NEXT`.
1. Rewrite `RETURNING` to `OUTPUT INSERTED`.
1. Rewrite `ON CONFLICT` to `MERGE`.
1. Replace `SERIAL` / `BIGSERIAL` with `IDENTITY`.
1. `BOOLEAN` columns replaced with **bit**.
1. Replace array columns with normalized tables or JSON.
1. Replace `JSONB` operators with `JSON_VALUE()` / `JSON_QUERY()`.
1. Update connection string for Microsoft SQL authentication.
1. Test application against AdventureWorks or your target schema.

## Authentication and deployment

Self-managed PostgreSQL applications typically deploy with connection strings containing passwords, or use `.pgpass` files and `PGPASSWORD` environment variables. Azure Database for PostgreSQL supports Microsoft Entra authentication, so if you're already using passwordless auth, the same identity model carries over to Azure SQL.

For production workloads against Azure SQL, use managed identity:

```python
conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="AdventureWorks",
    authentication="ActiveDirectoryMSI",
    encrypt="yes"
)
```

For local development and CI, see [Container and local development](container-local-development.md) for Docker, devcontainer, and CI pipeline setup patterns.

## Related content

- [Install mssql-python](installation.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Use bulk copy with mssql-python](bulk-copy.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Transaction management with mssql-python](transaction-management.md)
- [Container and local development with mssql-python](container-local-development.md)
- [Migrate from SQLite to Microsoft SQL with mssql-python](migrate-from-sqlite.md)
