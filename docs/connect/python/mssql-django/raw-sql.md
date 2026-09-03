---
title: Raw SQL Queries in mssql-django
description: Execute raw SQL queries against SQL Server from Django applications using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Raw SQL queries in mssql-django

This article explains how to execute raw SQL queries against SQL Server from Django applications. Raw SQL is useful for operations not exposed through the Django ORM, such as complex Transact-SQL (T-SQL), spatial queries, or performance-critical operations.

## Use connection.cursor()

Access the database cursor directly through Django's `connection` object:

```python
from django.db import connection

def get_server_version():
    with connection.cursor() as cursor:
        cursor.execute("SELECT @@VERSION;")
        row = cursor.fetchone()
    return row[0]
```

The `with` statement ensures the cursor is properly closed after use.

## Parameterized queries

Always use parameterized queries to prevent SQL injection. Pass parameters as a list:

> [!NOTE]  
> The following examples use Django's default table-naming convention `<app_label>_<model_name>` (for example, `myapp_product`). If you override `db_table` in a model's `Meta`, substitute that name. You can also read the resolved name at runtime with `Product._meta.db_table`.

```python
from django.db import connection

def get_products_by_price(min_price, max_price):
    with connection.cursor() as cursor:
        cursor.execute(
            "SELECT id, name, price FROM myapp_product WHERE price BETWEEN %s AND %s;",
            [min_price, max_price],
        )
        results = cursor.fetchall()
    return results
```

> [!IMPORTANT]  
> Never use string formatting or f-strings to embed values in SQL queries. Always use parameterized queries (`%s` placeholders with a parameter list) to prevent SQL injection.

## Fetch results

Django's cursor provides several methods for retrieving results:

```python
from django.db import connection

def demonstrate_fetch_methods():
    with connection.cursor() as cursor:
        cursor.execute("SELECT id, name FROM myapp_product;")

        # Fetch one row
        row = cursor.fetchone()

        # Fetch the next 10 rows (continues from where fetchone stopped)
        rows = cursor.fetchmany(10)

        # Fetch all remaining rows (continues from where fetchmany stopped)
        all_rows = cursor.fetchall()
```

## Return results as dictionaries

Convert rows to dictionaries using `cursor.description`:

```python
from django.db import connection

def dictfetchall(cursor):
    columns = [col[0] for col in cursor.description]
    return [dict(zip(columns, row)) for row in cursor.fetchall()]

def get_all_products():
    with connection.cursor() as cursor:
        cursor.execute("SELECT id, name, price FROM myapp_product;")
        return dictfetchall(cursor)
```

## Use Manager.raw() for model queries

When you want raw SQL but still want Django model instances, use `Manager.raw()`:

```python
from myapp.models import Product

products = Product.objects.raw(
    "SELECT id, name, price FROM myapp_product WHERE price > %s",
    [10.00],
)

for product in products:
    print(f"{product.name}: ${product.price}")
```

The query must return all fields defined in the model's primary key. Additional fields are loaded lazily.

## Execute DDL statements

Use raw SQL for schema operations that Django doesn't support directly:

```python
from django.db import connection

def create_index():
    with connection.cursor() as cursor:
        cursor.execute(
            "CREATE INDEX IX_product_name ON myapp_product (name) "
            "INCLUDE (price);"
        )
```

## Multiple database connections

If you use multiple databases, specify which connection to use:

```python
from django.db import connections

def query_reporting_db():
    with connections["reporting"].cursor() as cursor:
        cursor.execute("SELECT COUNT(*) FROM myapp_product;")
        return cursor.fetchone()[0]
```

## Related content

- [Security best practices for mssql-django](security-best-practices.md)
- [Call stored procedures from Django](stored-procedures.md)
- [Transaction management in mssql-django](transactions.md)
- [Django raw SQL documentation](https://docs.djangoproject.com/en/stable/topics/db/sql)
- [Parameterized queries](https://docs.djangoproject.com/en/stable/topics/db/sql/#passing-parameters-into-raw)
