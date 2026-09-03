---
title: Call Stored Procedures from Django
description: Create and call SQL Server stored procedures from Django applications using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Call stored procedures from Django

This article explains how to create and call SQL Server stored procedures from Django applications using the `mssql-django` backend.

## Create a stored procedure in a migration

Use `migrations.RunSQL` to create a stored procedure as part of your Django migration workflow:

```python
from django.db import migrations

class Migration(migrations.Migration):

    dependencies = [
        ("myapp", "0001_initial"),
    ]

    operations = [
        migrations.RunSQL(
            sql="""
                CREATE PROCEDURE GetProductsByCategory
                    @CategoryName NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT id, name, price
                    FROM myapp_product
                    WHERE category = @CategoryName;
                END;
            """,
            reverse_sql="DROP PROCEDURE IF EXISTS GetProductsByCategory;",
        ),
    ]
```

Apply the migration:

```bash
python manage.py migrate
```

## Call a stored procedure

Use Django's `connection.cursor()` to call stored procedures with raw SQL:

```python
from django.db import connection

def get_products_by_category(category_name):
    with connection.cursor() as cursor:
        cursor.execute("EXECUTE GetProductsByCategory @CategoryName = %s;", [category_name])
        results = cursor.fetchall()
    return results
```

## Call a stored procedure with multiple parameters

Pass multiple parameters as a list:

```python
from django.db import connection

def search_products(category_name, min_price):
    with connection.cursor() as cursor:
        cursor.execute(
            "EXECUTE SearchProducts @CategoryName = %s, @MinPrice = %s;",
            [category_name, min_price],
        )
        results = cursor.fetchall()
    return results
```

## Read column names from results

Access column names from `cursor.description`:

```python
from django.db import connection

def get_products_as_dicts(category_name):
    with connection.cursor() as cursor:
        cursor.execute("EXECUTE GetProductsByCategory @CategoryName = %s;", [category_name])
        columns = [col[0] for col in cursor.description]
        results = [dict(zip(columns, row)) for row in cursor.fetchall()]
    return results
```

## Handle multiple result sets

Some stored procedures return multiple result sets. Use `cursor.nextset()` to navigate between them:

```python
from django.db import connection

def get_dashboard_data():
    with connection.cursor() as cursor:
        cursor.execute("EXECUTE GetDashboardData;")

        # First result set
        products = cursor.fetchall()

        # Move to second result set
        cursor.nextset()
        categories = cursor.fetchall()

    return products, categories
```

## Wrap in a Django manager

Encapsulate stored procedure calls in a custom manager for cleaner code:

```python
from django.db import connection, models

class ProductManager(models.Manager):
    def by_category(self, category_name):
        with connection.cursor() as cursor:
            cursor.execute(
                "EXECUTE GetProductsByCategory @CategoryName = %s;",
                [category_name],
            )
            columns = [col[0] for col in cursor.description]
            return [dict(zip(columns, row)) for row in cursor.fetchall()]

class Product(models.Model):
    name = models.CharField(max_length=100)
    price = models.DecimalField(max_digits=10, decimal_places=2)
    category = models.CharField(max_length=100)

    objects = ProductManager()
```

Usage:

```python
products = Product.objects.by_category("Electronics")
```

## Related content

- [Raw SQL queries in mssql-django](raw-sql.md)
- [Database migrations with mssql-django](migrations.md)
- [Transaction management in mssql-django](transactions.md)
- [Stored procedures wiki](https://github.com/microsoft/mssql-django/wiki/Create-and-Run-stored-procedures)
