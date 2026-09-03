---
title: JSONField with SQL Server
description: Use Django JSONField with SQL Server through the mssql-django backend, including supported lookups and limitations.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# JSONField with SQL Server

This article explains how Django's `JSONField` works with SQL Server through the `mssql-django` backend, including supported lookups and limitations.

## Prerequisites

- SQL Server 2016 or later (JSON functions are required)
- `mssql-django` 1.2 or later

## How JSONField maps to SQL Server

Django's `JSONField` maps to **nvarchar(max)** with a JSON check constraint in SQL Server. The backend uses SQL Server's built-in JSON functions (`JSON_VALUE`, `JSON_QUERY`, `ISJSON`) to implement lookups and queries.

## Define a model with JSONField

Add the following model to `myapp/models.py`. The examples in this article use an `Item` model so they don't conflict with the `Product` model from the [Django quickstart](quickstart.md).

```python
from django.db import models

class Item(models.Model):
    name = models.CharField(max_length=100)
    metadata = models.JSONField(default=dict)
    tags = models.JSONField(null=True, blank=True)
```

Generate and apply the migration so the underlying table exists in SQL Server:

```bash
python manage.py makemigrations myapp
python manage.py migrate myapp
```

## Store and retrieve JSON data

Open the Django shell with `python manage.py shell`. At the `>>>` prompt, import the model:

```python
from myapp.models import Item
```

Create a record with JSON data:

```python
item = Item.objects.create(
    name="Widget",
    metadata={"color": "blue", "weight": 1.5, "dimensions": {"height": 10, "width": 5}},
    tags=["sale", "new"],
)
```

Retrieve the record and access JSON values:

```python
item = Item.objects.get(name="Widget")
print(item.metadata["color"])  # "blue"
print(item.tags)  # ["sale", "new"]
```

## Supported lookups

The `mssql-django` backend supports the following JSONField lookups:

### Key/index lookups

Access nested JSON values using Django's double-underscore syntax:

```python
# Filter by nested key value
Item.objects.filter(metadata__color="blue").values()

# Access nested objects
Item.objects.filter(metadata__dimensions__height=10).values()
```

### contains

> [!NOTE]  
> The `contains` lookup isn't supported on the `mssql-django` backend. Use `has_key` with key-path lookups as an alternative:

```python
# Instead of: Item.objects.filter(metadata__contains={"color": "blue"})
# Use key-path lookup:
Item.objects.filter(metadata__color="blue").values()
```

### has_key

Check if a specific key exists:

```python
Item.objects.filter(metadata__has_key="color").values()
```

### has_keys

Check if all specified keys exist:

```python
Item.objects.filter(metadata__has_keys=["color", "weight"]).values()
```

### has_any_keys

Check if any of the specified keys exist:

```python
Item.objects.filter(metadata__has_any_keys=["color", "size"]).values()
```

### isnull

The `isnull` lookup has specific behavior with SQL Server:

```python
# Returns objects where the key doesn't exist AND keys with None value
Item.objects.filter(metadata__color__isnull=True).values()

# Returns objects where the key exists and has a non-null value
Item.objects.filter(metadata__color__isnull=False).values()
```

> [!NOTE]  
> On the `mssql-django` backend, if a key exists but has a JSON `null` value, `has_key` returns an empty QuerySet. This differs from PostgreSQL, where `has_key` returns `True` regardless of value. The `isnull=True` lookup returns objects where the key doesn't exist and objects where the value is `null`.

### exact with None

The `exact` lookup doesn't support `None` values. The following query returns an empty QuerySet:

```python
# Returns empty QuerySet - use isnull lookup instead
Item.objects.filter(metadata__color=None).values()
```

Use the `isnull` lookup instead to find null values.

## Limitations

- **Bulk updates with JSONField**: Some edge cases exist when using `bulk_update` with JSONField values, particularly on Django 5.2 and later versions. For more information, see [Limitations and unsupported features in mssql-django](limitations.md).
- **CASE WHEN expressions**: On Django 5.2 and later versions, certain JSONField operations inside CASE WHEN expressions might produce unexpected results.
- **exact with None**: Use `isnull` instead of `exact` to filter for null JSON values.
- **has_key with null values**: `has_key` returns an empty QuerySet for keys that exist but have a `null` value.
- **Literal quote characters in JSON string values**: Equality lookups on JSON string values that contain literal `"` characters (for example, `metadata={"description": '"quoted"'}`) might not match the stored row. Values containing quote characters store correctly but aren't reliably retrievable through field lookups.

## Related content

- [Migrate Django apps from PostgreSQL to SQL Server](migrate-from-postgresql.md)
- [Django field to SQL Server type mappings](data-type-mappings.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [JSONField wiki](https://github.com/microsoft/mssql-django/wiki/JSONField)
- [Django JSONField documentation](https://docs.djangoproject.com/en/stable/ref/models/fields/#jsonfield)
