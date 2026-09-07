---
title: Migrate Django Apps from Other Databases to SQL Server
description: Guidance for migrating Django applications from PostgreSQL, MySQL, or SQLite to SQL Server using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate Django apps from other databases to SQL Server

This article provides guidance for migrating Django applications from PostgreSQL, MySQL, or SQLite to SQL Server using the `mssql-django` backend.

## Overview

Django's ORM abstracts most database differences, but some behaviors and SQL dialects vary between backends. This guide covers the key differences you encounter when migrating to SQL Server.

## Step 1: Install mssql-django

Install the `mssql-django` package and its dependencies:

```bash
pip install mssql-django
```

Ensure the Microsoft ODBC Driver for SQL Server is installed. See [Install mssql-django](installation.md) for platform-specific instructions.

## Step 2: Update DATABASE configuration

Replace your existing database configuration in `settings.py`:

```python
# Example: From PostgreSQL
# DATABASES = {
#     "default": {
#         "ENGINE": "django.db.backends.postgresql",
#         "NAME": "mydb",
#     },
# }

# To SQL Server
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

## Step 3: Create fresh migrations

Start with a clean migration history for SQL Server:

```bash
# Remove existing migration files (keep __init__.py)
# Then regenerate
python manage.py makemigrations
python manage.py migrate
```

> [!IMPORTANT]  
> Transfer your data using a data migration or separate ETL process. Don't attempt to run PostgreSQL or MySQL migration files against SQL Server.

## Key differences from PostgreSQL

| Feature | PostgreSQL | SQL Server (mssql-django) |
| --- | --- | --- |
| Auto-increment | `SERIAL` / `BIGSERIAL` | `IDENTITY(1,1)` |
| Boolean type | Native `boolean` | **bit** (`0` or `1`) |
| Text fields | `text` (unlimited) | **nvarchar(max)** |
| JSON support | Native `jsonb` | **nvarchar(max)** with JSON functions (SQL Server 2016+) |
| Array fields | `ArrayField` | Not supported. Use a related table or JSON. |
| HStore fields | `HStoreField` | Not supported. Use `JSONField` instead. |
| Range fields | `IntegerRangeField`, `BigIntegerRangeField`, `DateRangeField`, `DateTimeRangeField` | Not supported. Use two separate fields. |
| Full-text search | `SearchVector`, `SearchRank` | Use raw SQL with SQL Server full-text search. |
| `DISTINCT ON` | Supported | Not supported. Use `GROUP BY` or subqueries. |
| `DateTimeField` with time zone | `timestamp with time zone` | **datetimeoffset** (when `USE_TZ=True`) or **datetime2** |

### PostgreSQL-specific features to replace

If your code uses PostgreSQL-specific features from `django.contrib.postgres`, replace them:

```python
# PostgreSQL ArrayField - replace with JSONField or related table
# Before
from django.contrib.postgres.fields import ArrayField
tags = ArrayField(models.CharField(max_length=50))

# After (using JSONField)
tags = models.JSONField(default=list)

# PostgreSQL HStoreField - replace with JSONField
# Before
from django.contrib.postgres.fields import HStoreField
metadata = HStoreField()

# After
metadata = models.JSONField(default=dict)
```

## Key differences from MySQL

| Feature | MySQL | SQL Server (mssql-django) |
| --- | --- | --- |
| Auto-increment | `AUTO_INCREMENT` | `IDENTITY(1,1)` |
| Boolean type | `tinyint(1)` | **bit** |
| Text fields | `longtext` | **nvarchar(max)** |
| JSON support | Native `JSON` (5.7 and later) | **nvarchar(max)** with JSON functions |
| Collation | Per-column configurable | Instance or database level (override with `COLLATE` option) |
| `DateTimeField` | **datetime(6)** | **datetimeoffset** or **datetime2** |

## Key differences from SQLite

| Feature | SQLite | SQL Server (mssql-django) |
| --- | --- | --- |
| Type enforcement | Flexible typing | Strict type enforcement |
| Concurrent writes | Limited | Full concurrency support |
| Maximum connections | Effectively 1 writer | Connection pooling with many concurrent connections |
| `DateTimeField` | Stored as text | **datetimeoffset** or **datetime2** |

## Collation differences

Collation controls how SQL Server compares and sorts text. This is one of the most common sources of unexpected behavior when migrating from PostgreSQL or MySQL.

### Case sensitivity

SQL Server's default collation (`SQL_Latin1_General_CP1_CI_AS`) is **case-insensitive**. PostgreSQL is case-sensitive by default.

This behavior means that after migration, queries that previously distinguished between `"Smith"` and `"smith"` treat them as equal:

```python
# On PostgreSQL: returns only exact case matches
# On SQL Server (default collation): returns both "Smith" and "smith"
User.objects.filter(last_name="Smith")
```

If your application depends on case-sensitive comparisons, you have two options:

- **Change the database or column collation** to a case-sensitive variant:

   ```sql
   -- Database-level (affects all new columns)
   ALTER DATABASE [<your-database>] COLLATE Latin1_General_CS_AS;

   -- Column-level (for specific columns)
   ALTER TABLE [<your-table>]
   ALTER COLUMN [<column-name>] NVARCHAR (150) COLLATE Latin1_General_CS_AS;
   ```

- **Use Django's `__exact` lookup with a collation override** in raw SQL for targeted queries.

### Accent sensitivity

The default SQL Server collation is accent-sensitive (`AS`), which matches PostgreSQL's behavior. Characters like `é` and `e` are treated as different. If you need accent-insensitive comparisons, use a collation ending in `_AI`.

### Configure collation in mssql-django

Override the default collation for text field lookups in your database configuration:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "collation": "Latin1_General_CS_AS",  # Case-sensitive
        },
    },
}
```

> [!NOTE]  
> The `collation` option in `mssql-django` controls the collation used in `LIKE` and comparison operations generated by Django's ORM lookups. It doesn't change the collation of existing columns in the database. To change stored column collation, use `ALTER TABLE` / `ALTER COLUMN` statements. For more information, see [SQL Server collation documentation](../../../t-sql/statements/create-database-transact-sql.md).

## Step 4: Update custom SQL

If your code contains raw SQL, update it for SQL Server syntax:

```python
# PostgreSQL syntax
# cursor.execute("SELECT * FROM products LIMIT 10 OFFSET 20")

# SQL Server syntax
cursor.execute("SELECT * FROM products ORDER BY id OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")
```

Common SQL syntax differences:

| Operation | PostgreSQL/MySQL | SQL Server |
| --- | --- | --- |
| Limit results | `LIMIT 10` | `TOP 10` or `OFFSET ... FETCH NEXT ...` |
| String concatenation | `\|\|` (PG) / `CONCAT()` | `+` or `CONCAT()` |
| Boolean literals | `TRUE` / `FALSE` | `1` / `0` |
| Current timestamp | `NOW()` | `GETDATE()`, `SYSDATETIME()`, or `SYSDATETIMEOFFSET()` for time zone-aware values |
| IF NOT EXISTS | `CREATE TABLE IF NOT EXISTS` | Check `sys.objects` or use `IF NOT EXISTS` |

## Transaction isolation differences

PostgreSQL uses MVCC (Multi-Version Concurrency Control) for its `READ COMMITTED` isolation level. Readers never block writers and writers never block readers.

SQL Server's default `READ COMMITTED` uses locking, which means read queries can block while waiting for write transactions to complete. If your application experiences increased blocking after migration, consider enabling `READ COMMITTED SNAPSHOT` on the database:

```sql
ALTER DATABASE [<your-database>]
SET READ_COMMITTED_SNAPSHOT ON;
```

This changes SQL Server's `READ COMMITTED` to use row versioning (similar to PostgreSQL's MVCC) instead of locking. Readers see the last committed version of a row without waiting for active writers.

> [!NOTE]  
> `READ COMMITTED SNAPSHOT` requires additional `tempdb` space for row versions. Test under realistic load before enabling in production. For more information, see [Transaction management in mssql-django](transactions.md).

## Step 5: Migrate data

Data migration strategy depends on dataset size:

### Small datasets (<500 MB)

Use Django's `dumpdata`/`loaddata`:

```bash
# On the source database
python manage.py dumpdata --natural-foreign --natural-primary -o data.json

# Switch settings.py to SQL Server, then:
python manage.py migrate
python manage.py loaddata data.json
```

### Large datasets (>500 MB)

For large migrations, use specialized tools to avoid memory exhaustion and timeout issues. Django's ORM isn't the right tool for bulk loads at this scale. Bypass it for the data move and let Django manage schema and application logic afterward.

| Tool | Best for |
| --- | --- |
| [SQL Server Import and Export Wizard](../../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md) | On-premises to on-premises migrations with a GUI |
| [Azure Data Factory](/azure/data-factory/copy-activity-overview) | Any source to Azure SQL, including hybrid scenarios |
| [Azure Database Migration Service](/azure/dms/dms-overview) | Large-scale migrations with built-in validation and rollback |
| [mssql-python bulk copy with Apache Arrow](../mssql-python/python-sql-driver-mssql-python-bulk-copy-quickstart.md) | Custom Python pipelines that need maximum throughput between SQL Server, Azure SQL Database, and SQL database in Fabric |

### Post-migration validation

After migration, validate identity seed consistency for auto-increment columns:

```sql
-- Check identity seed and current value for all tables
SELECT 
    TABLE_NAME,
    IDENT_SEED(TABLE_SCHEMA + '.' + TABLE_NAME) AS IdentitySeed,
    IDENT_INCR(TABLE_SCHEMA + '.' + TABLE_NAME) AS IdentityIncrement,
    IDENT_CURRENT(TABLE_SCHEMA + '.' + TABLE_NAME) AS CurrentIdentity
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
    AND OBJECTPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), 'TableHasIdentity') = 1
ORDER BY TABLE_NAME;
```

If `CurrentIdentity` exceeds `IdentitySeed + record_count`, reseed:

```sql
DBCC CHECKIDENT ('your_table', RESEED, new_seed);
```

## Related content

- [Migrate Django apps from PostgreSQL to SQL Server](migrate-from-postgresql.md)
- [Install mssql-django](installation.md)
- [Django field to SQL Server type mappings](data-type-mappings.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Transaction management in mssql-django](transactions.md)
- [Time zone support in mssql-django](timezone-support.md)
- [JSONField with SQL Server](json-field.md)
