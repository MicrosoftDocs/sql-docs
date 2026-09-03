---
title: Bulk Operations with mssql-django
description: Use bulk_create and bulk_update in Django with SQL Server through the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Bulk operations with mssql-django

This article explains how to use Django's `bulk_create` and `bulk_update` methods with SQL Server through the `mssql-django` backend.

## bulk_create

Use `bulk_create` to insert multiple records in a single database operation:

```python
from myapp.models import Product

products = [
    Product(name="Widget A", price=9.99),
    Product(name="Widget B", price=14.99),
    Product(name="Widget C", price=19.99),
]

Product.objects.bulk_create(products)
```

### Return rows from bulk insert

By default, `bulk_create` doesn't return populated primary keys when using SQL Server. To enable returning primary keys, set `return_rows_bulk_insert` to `True` in your database `OPTIONS`:

```python
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
            "return_rows_bulk_insert": True,
        },
    },
}
```

> [!IMPORTANT]  
> Set `return_rows_bulk_insert` to `False` (the default) if any target table has triggers. The `OUTPUT` clause used to return rows isn't compatible with tables that have `INSERT` triggers.

## bulk_update

Use `bulk_update` to update multiple records in a single database operation:

```python
from decimal import Decimal

from myapp.models import Product

products = Product.objects.filter(category="widgets")
for product in products:
    product.price = product.price * Decimal("1.10")  # 10% price increase

Product.objects.bulk_update(products, ["price"])
```

### The default parameter

The `mssql-django` backend still accepts a `default` parameter on `bulk_update` for backward compatibility, but it doesn't affect generated SQL in current releases.

```output
At least one of the result expressions in a CASE specification must be an expression other than the NULL constant.
```

The backend now handles all-`NULL` update cases without requiring `default`.

> [!NOTE]  
> In older versions, omitting `default` could trigger the SQL Server error shown in the previous example. The parameter remains accepted only to avoid breaking older call sites.

### Batch size

For large datasets, use the `batch_size` parameter to limit the number of rows per SQL statement:

```python
from myapp.models import Product

new_products = [Product(name=f"Widget {i}") for i in range(2000)]
Product.objects.bulk_create(new_products, batch_size=500)

products = list(Product.objects.filter(name__startswith="Widget "))
Product.objects.bulk_update(products, ["price"], batch_size=500)
```

## Related content

- [Performance tuning for mssql-django](performance-tuning.md)
- [Transaction management in mssql-django](transactions.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [bulk_update wiki](https://github.com/microsoft/mssql-django/wiki/bulk_update)
