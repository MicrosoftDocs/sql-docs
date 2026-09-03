---
title: Reverse-Engineer Models with Inspectdb
description: Generate Django models from existing SQL Server databases using the inspectdb management command with mssql-django.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Reverse-engineer models with inspectdb

This article explains how to use Django's `inspectdb` management command to generate model code from an existing SQL Server database.

## Prerequisites

Add `mssql` to your `INSTALLED_APPS` in `settings.py`. This step registers the `mssql-django` management command override for `inspectdb`, which adds the `--schema` flag for inspecting non-default schemas:

```python
INSTALLED_APPS = [
    "django.contrib.admin",
    "django.contrib.auth",
    "django.contrib.contenttypes",
    "django.contrib.sessions",
    "django.contrib.messages",
    "django.contrib.staticfiles",
    "mssql",
    "myapp",
]
```

> [!NOTE]  
> The `mssql` database backend works without this step. Adding `"mssql"` to `INSTALLED_APPS` is only required to enable the `--schema` flag on `inspectdb`. Without it, `inspectdb` inspects only the default schema (`dbo`).

## Basic usage

Generate models for all tables in the configured database:

```bash
python manage.py inspectdb
```

Save the output directly to a models file:

```bash
python manage.py inspectdb > myapp/models.py
```

## Inspect specific tables

Generate models for specific tables only:

```bash
python manage.py inspectdb MyTable AnotherTable
```

## Multi-schema support

The `mssql-django` backend extends `inspectdb` to support multiple schemas. Specify a schema with the `--schema` flag:

```bash
python manage.py inspectdb --schema "dbo"
```

```bash
python manage.py inspectdb --schema "sales"
```

This feature is useful for SQL Server databases that organize tables across multiple schemas.

### Work with models from multiple schemas

When your database uses multiple schemas, use `db_table` in the model's `Meta` class to qualify the table with its schema name. Generated models from `inspectdb` might not include schema prefixes, so add them manually:

```python
class Customer(models.Model):
    customer_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=200)

    class Meta:
        managed = False
        db_table = "[sales].[Customer]"

class Product(models.Model):
    product_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)
    price = models.DecimalField(max_digits=10, decimal_places=2)

    class Meta:
        managed = False
        db_table = "[inventory].[Product]"
```

> [!IMPORTANT]  
> Use a bracketed two-part name such as `"[schema].[table]"`. The backend treats `db_table` as a single identifier, so bracketed names are preserved correctly while double-quoted forms are re-quoted as a literal table name.

### Cross-schema foreign keys

Django can follow foreign key relationships across schemas as long as both tables are in the same database and the `db_table` values are set correctly:

```python
class OrderItem(models.Model):
    order_item_id = models.AutoField(primary_key=True)
    product = models.ForeignKey("Product", on_delete=models.CASCADE)
    quantity = models.IntegerField()

    class Meta:
        managed = False
        db_table = "[sales].[OrderItem]"
```

Django resolves the `ForeignKey` through the referenced model's `db_table` value, so no extra schema configuration is needed on the relationship field.

## Review generated models

The `inspectdb` command generates model code that might need manual adjustments:

1. **Set `managed = False`**: Generated models include `managed = False` in the `Meta` class, which means Django doesn't manage the table schema. Remove this line if you want Django to manage migrations for the table.

1. **Add primary keys**: If a table doesn't have a primary key that `inspectdb` can detect, you might need to add one manually.

1. **Fix relationship fields**: Foreign key relationships might need adjustment, especially for cross-schema references.

Example of generated output:

```python
class Product(models.Model):
    product_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)
    price = models.DecimalField(max_digits=10, decimal_places=2)
    created_at = models.DateTimeField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = "Product"
```

## Limitations

The `inspectdb` command has the following limitations when used with SQL Server.

### Composite primary key inspection

Tables with composite primary keys might not generate complete model definitions. In Django 5.2 and later, define `CompositePrimaryKey` manually after running `inspectdb`, or use a surrogate primary key.

### Views

The `inspectdb` command can inspect views, but the generated models might require manual adjustment of field types and null constraints.

## Related content

- [Django field to SQL Server type mappings](data-type-mappings.md)
- [Database migrations with mssql-django](migrations.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Django inspectdb documentation](https://docs.djangoproject.com/en/stable/ref/django-admin/#inspectdb)
