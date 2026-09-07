---
title: Test Django Apps with SQL Server
description: Configure Django's test framework to work with SQL Server databases using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Test Django apps with SQL Server

This article explains how to configure Django's test framework to work with SQL Server databases using the `mssql-django` backend.

## How Django testing works with SQL Server

When you run `python manage.py test`, Django creates a separate test database, runs all tests, and then destroys the test database. The test database name defaults to `test_` followed by your configured database name.

## Configure test database settings

Customize the test database using the `TEST` dictionary in your database configuration:

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
        },
        "TEST": {
            "NAME": "test_mydb",
            "COLLATION": "SQL_Latin1_General_CP1_CI_AS",
        },
    },
}
```

### TEST settings

| Setting | Description |
| --- | --- |
| `NAME` | Name of the test database. Default: `"test_" + NAME`. |
| `COLLATION` | Collation for the test database. Default: instance default collation. |
| `DEPENDENCIES` | Creation-order dependencies when using multiple databases. |
| `MIRROR` | Alias of a database that this test database should mirror. |

## Run tests

Run the full test suite:

```bash
python manage.py test
```

Run tests for a specific app:

```bash
python manage.py test myapp
```

Run a specific test class or method:

```bash
python manage.py test myapp.tests.ProductTestCase.test_create_product
```

## Keep the test database

Use `--keepdb` to preserve the test database between test runs. This approach speeds up repeated test runs by skipping database creation and destruction:

```bash
python manage.py test --keepdb
```

> [!NOTE]  
> The `--keepdb` flag is required when using managed identity authentication (`ActiveDirectoryMsi`), because the test runner can't create databases with that authentication method.

## Transaction isolation in tests

Django's `TestCase` class wraps each test in a transaction and rolls it back after the test completes. This behavior provides test isolation without creating and destroying tables for each test.

For tests that need to commit transactions (for example, to test transaction-related behavior), use `TransactionTestCase`:

```python
from django.test import TransactionTestCase
from myapp.models import Product

class ProductTransactionTest(TransactionTestCase):
    def test_atomic_operation(self):
        # This test commits to the database
        Product.objects.create(name="Widget", price=9.99)
        self.assertEqual(Product.objects.count(), 1)
```

> [!TIP]  
> Use `TestCase` (not `TransactionTestCase`) whenever possible. `TestCase` is faster because it uses transaction rollback instead of truncating tables.

## Test with multiple databases

If your project uses multiple databases, configure test dependencies:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<primary-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
        "TEST": {
            "NAME": "test_primary",
        },
    },
    "reporting": {
        "ENGINE": "mssql",
        "NAME": "<reporting-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
        "TEST": {
            "NAME": "test_reporting",
            "DEPENDENCIES": ["default"],
        },
    },
}
```

## Related content

- [mssql-django configuration reference](configuration-reference.md)
- [Container and local development with mssql-django](container-local-development.md)
- [Transaction management in mssql-django](transactions.md)
- [Troubleshoot mssql-django](troubleshooting.md)
- [Django testing documentation](https://docs.djangoproject.com/en/stable/topics/testing)
