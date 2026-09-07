---
title: Migrate from django-mssql-backend to mssql-django
description: Migrate a Django project from django-mssql-backend to mssql-django with updated ENGINE and package configuration.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Migrate from django-mssql-backend to mssql-django

This article explains how to migrate a Django project from the `django-mssql-backend` package (which used the `sql_server.pyodbc` engine) to the `mssql-django` package.

## Overview

The `mssql-django` package is the successor to `django-mssql-backend`. It continues to provide a Django database backend for SQL Server using pyodbc, but with an updated package name, engine identifier, and support for newer Django versions.

| Feature | django-mssql-backend | mssql-django |
| --- | --- | --- |
| Package name | `django-mssql-backend` | `mssql-django` |
| ENGINE | `sql_server.pyodbc` | `mssql` |
| Django support | 2.2, 3.0, 3.1 | 3.2, 4.0, 4.1, 4.2, 5.0, 5.1, 5.2, 6.0 |
| Maintained by | Microsoft | Microsoft |
| PyPI | `pip install django-mssql-backend` | `pip install mssql-django` |

## Step 1: Update the Python package

Uninstall the old package and install the new one:

```bash
pip uninstall django-mssql-backend
pip install mssql-django
```

If you use a `requirements.txt` file, replace the package reference:

```text
# Before
django-mssql-backend>=2.8.1

# After
mssql-django>=1.7
```

## Step 2: Update the DATABASE ENGINE

In your `settings.py`, change the `ENGINE` value from `sql_server.pyodbc` to `mssql`:

```python
# Before
DATABASES = {
    "default": {
        "ENGINE": "sql_server.pyodbc",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 17 for SQL Server",
        },
    },
}

# After
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

> [!NOTE]  
> `mssql-django` v1.7 defaults to ODBC Driver 18. You can continue to use Driver 17 by specifying it explicitly in the `driver` option.

## Step 3: Update INSTALLED_APPS

If your `INSTALLED_APPS` references `sql_server`, update it:

```python
# Before
INSTALLED_APPS = [
    "sql_server",
    # ...
]

# After - only needed if using inspectdb
INSTALLED_APPS = [
    "mssql",
    # ...
]
```

## Step 4: Verify configuration

Run a quick verification to ensure the new backend connects successfully:

```bash
python manage.py check
python manage.py showmigrations
```

## Step 5: Run your test suite

Run your project's test suite to verify everything works with the new backend:

```bash
python manage.py test
```

## Breaking changes to watch for

<!-- Inline TrustServerCertificate warning kept inside the table cell; [!INCLUDE] directives don't render inside table cells in DocFx. See includes/trust-server-certificate-caution.md for the shared callout used elsewhere. -->

| Area | Change |
| --- | --- |
| Default ODBC driver | v1.7 defaults to `ODBC Driver 18 for SQL Server`, which sets `Encrypt=yes` by default (Driver 17 defaulted to `Encrypt=no`). Connections to servers with self-signed or untrusted certificates fail unless you add `TrustServerCertificate=yes` to `extra_params`. Alternatively, specify Driver 17 or install a trusted certificate on the server. |
| `return_rows_bulk_insert` | Defaults to `False`. Set to `True` to enable returning IDs after `bulk_create`. Must remain `False` for tables with triggers. See [Bulk operations with mssql-django](bulk-operations.md). |

## Related content

- [Install mssql-django](installation.md)
- [What's new in mssql-django](whats-new.md)
- [mssql-django configuration reference](configuration-reference.md)
