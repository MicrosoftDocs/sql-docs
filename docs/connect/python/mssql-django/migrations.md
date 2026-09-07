---
title: Database Migrations with mssql-django
description: Run Django database migrations with SQL Server using the mssql-django backend, including known edge cases.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Database migrations with mssql-django

This article explains how Django's migration system works with SQL Server through the `mssql-django` backend and documents known edge cases.

## Create and apply migrations

Django's migration workflow works the same way with SQL Server as with other databases:

1. Generate migrations from model changes:

   ```bash
   python manage.py makemigrations myapp
   ```

1. Review the generated migration files in `<app>/migrations/`.

1. Apply migrations to the database:

   ```bash
   python manage.py migrate myapp
   ```

1. Check migration status:

   ```bash
   python manage.py showmigrations myapp
   ```

## Initial project setup

When you set up a new Django project with SQL Server, run migrations to create Django's built-in tables (authentication, sessions, admin):

```bash
python manage.py migrate
```

This command creates all tables required by apps listed in `INSTALLED_APPS`.

## Custom SQL in migrations

Use `migrations.RunSQL` to execute raw SQL statements during migrations. This approach is useful for creating stored procedures, triggers, or other SQL Server-specific objects:

```python
from django.db import migrations

class Migration(migrations.Migration):

    dependencies = [
        ("myapp", "0001_initial"),
    ]

    operations = [
        migrations.RunSQL(
            sql="CREATE INDEX IX_myapp_product_name ON myapp_product (name);",
            reverse_sql="DROP INDEX IX_myapp_product_name ON myapp_product;",
        ),
    ]
```

## Known migration edge cases

The following migration operations require workarounds when targeting SQL Server.

### AutoField alteration

Altering a model field from or to `AutoField` at migration time isn't supported. SQL Server doesn't allow adding or removing the `IDENTITY` property from an existing column.

**Workaround**: Create a new model with the desired field type. Migrate data from the old table to the new table, then drop the old table.

### Rename field or model with foreign key constraints

Renaming a field or model that has foreign key constraints can fail. SQL Server requires dropping and recreating FK constraints during rename operations.

**Workaround**: Use `migrations.SeparateDatabaseAndState` to drop the FK constraint, rename the column, and recreate the constraint, while telling Django to update its model state. The following example renames the `product` foreign key on an `Order` model to `item`:

```python
from django.db import migrations

class Migration(migrations.Migration):

    dependencies = [
        ("myapp", "0002_previous"),
    ]

    operations = [
        migrations.SeparateDatabaseAndState(
            database_operations=[
                migrations.RunSQL(
                    sql="ALTER TABLE myapp_order DROP CONSTRAINT FK_order_product;",
                    reverse_sql="ALTER TABLE myapp_order ADD CONSTRAINT FK_order_product FOREIGN KEY (product_id) REFERENCES myapp_product(id);",
                ),
                migrations.RunSQL(
                    sql="EXECUTE sp_rename 'myapp_order.product_id', 'item_id', 'COLUMN';",
                    reverse_sql="EXECUTE sp_rename 'myapp_order.item_id', 'product_id', 'COLUMN';",
                ),
                migrations.RunSQL(
                    sql="ALTER TABLE myapp_order ADD CONSTRAINT FK_order_item FOREIGN KEY (item_id) REFERENCES myapp_product(id);",
                    reverse_sql="ALTER TABLE myapp_order DROP CONSTRAINT FK_order_item;",
                ),
            ],
            state_operations=[
                migrations.RenameField(
                    model_name="order",
                    old_name="product",
                    new_name="item",
                ),
            ],
        ),
    ]
```

Look up the actual constraint name in your database before running this T-SQL code. Django generates constraint names that include a short hash, so the name in your schema doesn't match the placeholder shown here.

## Squash migrations

After many migrations accumulate, you can squash them into fewer files:

```bash
python manage.py squashmigrations myapp 0001 0010
```

> [!TIP]  
> Always test squashed migrations against a fresh database to ensure they produce the correct schema.

## Generated columns (computed columns)

The `mssql-django` backend supports Django's `GeneratedField` (Django 5.0 and later), which maps to SQL Server computed columns.

### Stored (PERSISTED) generated columns

A stored generated column is physically written to disk and updated when the source columns change:

```python
from django.db import models
from django.db.models import F

class Product(models.Model):
    price = models.DecimalField(max_digits=10, decimal_places=2)
    tax_rate = models.DecimalField(max_digits=5, decimal_places=4)
    total_price = models.GeneratedField(
        expression=F("price") * (1 + F("tax_rate")),
        output_field=models.DecimalField(max_digits=10, decimal_places=2),
        db_persist=True,
    )
```

This generates: `total_price AS ([price] * (1 + [tax_rate])) PERSISTED`.

### Virtual generated columns

A virtual generated column is computed at query time and doesn't consume storage:

```python
from django.db import models
from django.db.models import F, Value
from django.db.models.functions import Concat

class Employee(models.Model):
    first_name = models.CharField(max_length=50)
    last_name = models.CharField(max_length=50)
    full_name = models.GeneratedField(
        expression=Concat(F("first_name"), Value(" "), F("last_name")),
        output_field=models.CharField(max_length=101),
        db_persist=False,
    )
```

> [!NOTE]  
> SQL Server restricts indexes on non-persisted computed columns. Use `db_persist=True` if you need to index the generated column.

## Table and column comments

The `mssql-django` backend supports Django's `db_comment` feature (Django 4.2 and later). Comments are stored as `MS_Description` extended properties on the SQL Server object.

### Table comments

```python
class AuditLog(models.Model):
    action = models.CharField(max_length=50)
    timestamp = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table_comment = "Tracks user actions for compliance auditing."
```

### Column comments

```python
class Measurement(models.Model):
    value = models.FloatField(db_comment="Sensor reading in Celsius")
    recorded_at = models.DateTimeField(db_comment="UTC timestamp from the data logger")
```

Comments are visible in SQL Server Management Studio under column/table properties and via `sys.extended_properties`.

## Composite primary keys

Django 5.2 introduced `CompositePrimaryKey`. The `mssql-django` backend has partial support for composite primary keys, but some Django test cases are still excluded. Validate composite-key migrations and queries against your application before adopting them in production.

- `inspectdb` doesn't generate composite primary keys correctly. Define them manually after inspection.
- Tuple lookups aren't supported. The backend decomposes composite key comparisons into individual column conditions.
- Tuple comparison against subqueries requires Django 5.2.4 and later versions.
- Some migration operations still have known exclusions. See [Limitations and unsupported features in mssql-django](limitations.md) for the current status.

```python
from django.db import models
from django.db.models import CompositePrimaryKey

class OrderItem(models.Model):
    pk = CompositePrimaryKey("order_id", "product_id")
    order = models.ForeignKey("Order", on_delete=models.CASCADE)
    product = models.ForeignKey("Product", on_delete=models.CASCADE)
    quantity = models.IntegerField()
```

## IDENTITY_INSERT handling

When you insert explicit values into an `AutoField` (for example, restoring data from a backup with specific IDs), the backend automatically wraps the insert in `SET IDENTITY_INSERT ON` / `SET IDENTITY_INSERT OFF`. No manual SQL is needed.

```python
# The backend handles IDENTITY_INSERT automatically
Product.objects.create(id=42, name="Restored Widget", price=9.99)
```

> [!NOTE]  
> SQL Server allows only one table per session to have `IDENTITY_INSERT ON` at a time. If you insert explicit IDs into multiple tables in a single `atomic()` block, the backend handles the toggle per statement. However, concurrent sessions that also use `IDENTITY_INSERT` on the same table can conflict.

## Related content

- [Django field to SQL Server type mappings](data-type-mappings.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Call stored procedures from Django](stored-procedures.md)
- [Django migrations documentation](https://docs.djangoproject.com/en/stable/topics/migrations)
