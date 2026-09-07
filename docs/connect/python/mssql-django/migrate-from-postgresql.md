---
title: Migrate Django Apps from PostgreSQL to SQL Server
description: Detailed guide for migrating Django applications from PostgreSQL to SQL Server using the mssql-django backend, covering contrib.postgres replacements, full-text search, and connection pooling.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate Django apps from PostgreSQL to SQL Server

This article is a detailed migration guide for Django applications moving from PostgreSQL (`psycopg2` or `psycopg`) to SQL Server (`mssql-django`). For a general overview of migrating from any database, see [Migrate Django apps from other databases to SQL Server](migrate-from-other-databases.md).

## Prerequisites

- Python 3.8 or later
- Microsoft ODBC Driver 17 or 18 for SQL Server. See [Install mssql-django](installation.md).
- SQL Server 2016 or later, or Azure SQL Database

## Switch the database backend

Replace your PostgreSQL configuration in `settings.py`:

```python
# Before (PostgreSQL)
DATABASES = {
    "default": {
        "ENGINE": "django.db.backends.postgresql",
        "NAME": "mydb",
        "USER": "myuser",
        "PASSWORD": "mypassword",
        "HOST": "localhost",
        "PORT": "5432",
    },
}

# After (SQL Server)
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "mydb",
        "USER": "myuser",
        "PASSWORD": "mypassword",
        "HOST": "localhost",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

Update `requirements.txt`:

```text
# Remove
# psycopg2-binary>=2.9
# or psycopg[binary]>=3.1

# Add
mssql-django>=1.5
```

## Replace django.contrib.postgres features

The `django.contrib.postgres` module provides PostgreSQL-specific fields, functions, and lookups. These don't work with SQL Server. The following sections show how to replace each feature.

### ArrayField

PostgreSQL `ArrayField` stores arrays natively. SQL Server doesn't have an array column type.

**Option 1: JSONField** (works with Django 3.2 and later versions)

```python
# Before
from django.contrib.postgres.fields import ArrayField

class Product(models.Model):
    tags = ArrayField(models.CharField(max_length=50), default=list)

# After
class Product(models.Model):
    tags = models.JSONField(default=list)
```

Querying changes:

```python
# Before (PostgreSQL)
Product.objects.filter(tags__contains=["sale"])
Product.objects.filter(tags__overlap=["sale", "new"])
Product.objects.filter(tags__len=3)

# After (SQL Server with JSONField)
# Use __contains for exact list matching
Product.objects.filter(tags__contains=["sale"])

# For overlap-style queries, use raw SQL
from django.db.models.expressions import RawSQL
Product.objects.filter(
    pk__in=RawSQL(
        """
        SELECT p.id FROM products_product p
        CROSS APPLY OPENJSON(p.tags) t
        WHERE t.value IN (%s, %s)
        """,
        ["sale", "new"],
    )
)
```

**Option 2: Related table** (normalized, better for large arrays or frequent filtering)

```python
class Product(models.Model):
    name = models.CharField(max_length=200)

class ProductTag(models.Model):
    product = models.ForeignKey(Product, on_delete=models.CASCADE, related_name="tags")
    tag = models.CharField(max_length=50, db_index=True)

    class Meta:
        unique_together = [("product", "tag")]
```

### HStoreField

Replace with `JSONField`:

```python
# Before
from django.contrib.postgres.fields import HStoreField

class Profile(models.Model):
    metadata = HStoreField(default=dict)

# After
class Profile(models.Model):
    metadata = models.JSONField(default=dict)
```

`JSONField` supports the same key lookup syntax:

```python
# Both backends support this
Profile.objects.filter(metadata__theme="dark")
```

### Range fields

PostgreSQL range types (`IntegerRangeField`, `BigIntegerRangeField`, `DateRangeField`, `DateTimeRangeField`, `DecimalRangeField`) have no SQL Server equivalent. Use two separate fields:

```python
# Before
from django.contrib.postgres.fields import DateRangeField

class Event(models.Model):
    dates = DateRangeField()

# After
class Event(models.Model):
    start_date = models.DateField()
    end_date = models.DateField()
```

Update queries to use separate field comparisons. Before, with PostgreSQL's `DateRangeField`:

```python
from django.contrib.postgres.fields import DateRangeField
from psycopg2.extras import DateRange

Event.objects.filter(dates__contains=DateRange(start, end))
```

After, with two `DateField` columns on SQL Server:

```python
from datetime import date

start = date(2026, 1, 1)
end = date(2026, 12, 31)

Event.objects.filter(start_date__lte=start, end_date__gte=end)
```

### CITextField and CIEmailField

PostgreSQL's case-insensitive text types use the `citext` extension. SQL Server's default collation (`SQL_Latin1_General_CP1_CI_AS`) is already case-insensitive, so standard `CharField` and `EmailField` behave the same way:

```python
# Before
from django.contrib.postgres.fields import CITextField

class Tag(models.Model):
    name = CITextField(max_length=100)

# After - already case-insensitive with default SQL Server collation
class Tag(models.Model):
    name = models.CharField(max_length=100)
```

### SearchVector, SearchQuery, SearchRank

PostgreSQL full-text search is deeply integrated with Django. SQL Server has its own full-text search engine but no Django ORM integration. See [Full-text search migration](#full-text-search-migration) later in this article.

### Aggregate functions

Replace PostgreSQL-specific aggregates:

```python
# Before
from django.contrib.postgres.aggregates import ArrayAgg, StringAgg

Product.objects.values("category").annotate(
    all_names=ArrayAgg("name"),
    name_list=StringAgg("name", delimiter=", "),
)

# After - use SQL Server equivalents via RawSQL
from django.db.models.expressions import RawSQL

Product.objects.values("category").annotate(
    name_list=RawSQL(
        "STRING_AGG(name, ', ') WITHIN GROUP (ORDER BY name)",
        [],
    ),
)
```

> [!NOTE]  
> `STRING_AGG` requires SQL Server 2017 or later or Azure SQL Database.

## Full-text search migration

PostgreSQL full-text search uses `tsvector`, `tsquery`, and `GIN` indexes. SQL Server has a separate full-text search engine.

### Enable full-text search in SQL Server

```sql
-- Create a full-text catalog
CREATE FULLTEXT CATALOG [MyAppCatalog] AS DEFAULT;

-- Create a full-text index (table must have a unique index)
CREATE FULLTEXT INDEX ON [products_product]([name], [description])
KEY INDEX [PK_products_product]
WITH CHANGE_TRACKING AUTO;
```

### Query full-text search from Django

Use raw SQL to access SQL Server's `CONTAINS` and `FREETEXT` functions:

```python
from django.db.models.expressions import RawSQL

# Equivalent of PostgreSQL SearchVector + SearchQuery
def search_products(query):
    return Product.objects.filter(
        pk__in=RawSQL(
            """
            SELECT p.id FROM products_product p
            WHERE CONTAINS((p.name, p.description), %s)
            """,
            [query],
        )
    )
```

For ranked results (equivalent to `SearchRank`):

```python
def search_products_ranked(query):
    return Product.objects.raw(
        """
        SELECT p.*, ft.[RANK]
        FROM products_product p
        INNER JOIN CONTAINSTABLE(products_product, (name, description), %s) ft
            ON p.id = ft.[KEY]
        ORDER BY ft.[RANK] DESC
        """,
        [query],
    )
```

### Full-text index maintenance runbook

Plan maintenance for SQL Server full-text indexes after migration:

- Use `CHANGE_TRACKING AUTO` for near-real-time updates.
- Use `CHANGE_TRACKING MANUAL` for bulk-load windows, then run a full population.
- Track crawl status and backlog through `sys.fulltext_indexes` and `sys.dm_fts_index_population`.

Check status:

```sql
SELECT
    OBJECT_NAME(i.object_id) AS table_name,
    i.change_tracking_state_desc,
    i.has_crawl_completed,
    i.crawl_type_desc
FROM sys.fulltext_indexes AS i;
```

After large data loads with manual tracking:

```sql
ALTER FULLTEXT INDEX ON [products_product] START FULL POPULATION;
```

> [!TIP]
> Rebuild or repopulate full-text indexes during low-traffic windows. Full populations can be expensive on large tables.

### Create a search manager

Wrap the raw SQL in a manager for clean access:

```python
class ProductSearchManager(models.Manager):
    def search(self, query):
        if not query:
            return self.none()
        return self.filter(
            pk__in=RawSQL(
                """
                SELECT p.id FROM products_product p
                WHERE CONTAINS((p.name, p.description), %s)
                """,
                [query],
            )
        )

class Product(models.Model):
    name = models.CharField(max_length=200)
    description = models.TextField()

    objects = ProductSearchManager()
    # Usage: Product.objects.search("mountain bike")
```

## PostGIS and spatial data

`mssql-django` doesn't include a GeoDjango GIS backend. If your PostgreSQL application uses PostGIS through `django.contrib.gis`, you can't migrate spatial queries directly to the Django ORM on SQL Server.

SQL Server does support **geography** and **geometry** data types natively. To work with spatial data after migration:

- **Store spatial data** using raw SQL or custom model fields that map to SQL Server's **geography** or **geometry** columns.
- **Query spatial data** using raw SQL with SQL Server's built-in spatial functions:

```python
from django.db import connection

with connection.cursor() as cursor:
    cursor.execute(
        """
        SELECT id, name
        FROM stores
        WHERE location.STDistance(geography::Point(%s, %s, 4326)) <= %s
        """,
        [latitude, longitude, radius_meters],
    )
```

- **Consider third-party libraries** that add SQL Server spatial support to Django, or keep spatial queries as raw SQL while using the ORM for everything else.

> [!NOTE]  
> If your application heavily depends on GeoDjango spatial lookups, evaluate the migration cost carefully. Moving spatial queries to raw SQL requires rewriting each GeoDjango spatial filter.

## Connection pooling migration

If your PostgreSQL application uses `pgbouncer` for connection pooling, replace it with Django's built-in connection management or ODBC connection pooling.

### Django connection reuse

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
        "CONN_MAX_AGE": 600,  # Reuse connections for 10 minutes
        "CONN_HEALTH_CHECKS": True,  # Django 4.1+
    },
}
```

For more details, see [Connection pooling in mssql-django](connection-pooling.md).

## DISTINCT ON replacement

PostgreSQL supports `DISTINCT ON` to get one row per group. SQL Server doesn't support this syntax. Use window functions instead:

```python
# Before (PostgreSQL)
Entry.objects.order_by("blog_id", "-pub_date").distinct("blog_id")

# After (SQL Server) - use raw SQL with ROW_NUMBER
Entry.objects.raw(
    """
    SELECT * FROM (
        SELECT *, ROW_NUMBER() OVER (PARTITION BY blog_id ORDER BY pub_date DESC) AS rn
        FROM blog_entry
    ) sub
    WHERE rn = 1
    """
)
```

## JSONB queries

PostgreSQL's `jsonb` type supports rich query operators. SQL Server stores JSON as **nvarchar(max)** with query functions available since SQL Server 2016.

Django's `JSONField` lookup syntax works on both backends for basic operations:

```python
# Works on both PostgreSQL and SQL Server
Config.objects.filter(data__settings__theme="dark")
Config.objects.filter(data__has_key="settings")
```

For advanced JSON queries not supported by Django's ORM, use SQL Server's `JSON_VALUE` and `OPENJSON` functions:

```python
from django.db.models.expressions import RawSQL

# Query nested JSON values
Config.objects.annotate(
    theme=RawSQL("JSON_VALUE(data, '$.settings.theme')", [])
).filter(theme="dark")
```

## Remove PostgreSQL dependencies

After migration, remove PostgreSQL packages from your project:

```bash
pip uninstall psycopg2-binary psycopg2 psycopg
```

Remove `django.contrib.postgres` from `INSTALLED_APPS` in `settings.py`:

```python
INSTALLED_APPS = [
    # Remove this line:
    # "django.contrib.postgres",
    "django.contrib.admin",
    "django.contrib.auth",
    # ...
]
```

## Migration checklist

| Step | Details |
| --- | --- |
| Switch backend | Replace `django.db.backends.postgresql` with `mssql` in `settings.py`. |
| Replace contrib.postgres | Swap `ArrayField`, `HStoreField`, range fields, and CI fields. |
| Update full-text search | Migrate from `tsvector`/`tsquery` to SQL Server `CONTAINS`/`FREETEXT`. |
| Update spatial queries | Rewrite GeoDjango lookups as raw SQL using SQL Server spatial functions. |
| Replace `DISTINCT ON` | Use `ROW_NUMBER()` window functions. |
| Update raw SQL | Change PostgreSQL syntax (`LIMIT`, `||`, `NOW()`) to SQL Server syntax. See [Update custom SQL](migrate-from-other-databases.md#step-4-update-custom-sql). |
| Enable RCSI | Set `READ_COMMITTED_SNAPSHOT ON` to match PostgreSQL MVCC behavior. See [Transaction isolation differences](migrate-from-other-databases.md#transaction-isolation-differences). |
| Test collation | Verify case-sensitivity behavior matches your expectations. See [Collation differences](migrate-from-other-databases.md#collation-differences). |
| Remove psycopg2 | Uninstall `psycopg2-binary` or `psycopg`. Remove `django.contrib.postgres`. |
| Regenerate migrations | Delete old migration files, run `makemigrations` and `migrate` fresh. |
| Migrate data | Use `dumpdata`/`loaddata` or an ETL tool for large datasets. |

## Related content

- [Migrate Django apps from other databases to SQL Server](migrate-from-other-databases.md)
- [Django field to SQL Server type mappings](data-type-mappings.md)
- [JSONField with SQL Server](json-field.md)
- [Transaction management in mssql-django](transactions.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
