---
title: Django Backend for SQL Server - mssql-django
description: Overview and task hub for installing, configuring, querying, authenticating, deploying, and troubleshooting Django applications with the mssql-django backend for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# Django backend for SQL Server - mssql-django

`mssql-django` is Microsoft's Django database backend for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. Set `ENGINE` to `"mssql"` in your Django `DATABASES` configuration to connect. The backend builds on [pyodbc](https://pypi.org/project/pyodbc/) and the [Microsoft ODBC Driver for SQL Server](../../odbc/microsoft-odbc-driver-for-sql-server.md), and supports Django 3.2 through 6.0, Python 3.8 through 3.14, and SQL Server 2016 through 2025.

## Choose your starting point

- To get a Django project talking to SQL Server quickly, start with [Quickstart: Connect Django to SQL Server](quickstart.md).
- To connect Django to Azure SQL with passwordless authentication, start with [Microsoft Entra authentication](microsoft-entra-authentication.md) and [Configuration reference](configuration-reference.md).
- To bring an existing SQL Server database into Django, go to [Reverse engineer models with inspectdb](inspectdb.md).
- To deploy a Django site to Azure, go to [Deploy to Azure App Service](deploy-azure-app-service.md) and [Container and local development](container-local-development.md).
- To migrate from another Django backend or database, go to [Migrate from django-mssql-backend](migrate-from-django-mssql-backend.md), [Migrate from other databases](migrate-from-other-databases.md), or [Migrate from PostgreSQL](migrate-from-postgresql.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL configuration. It combines four files: `settings.py` (the Django database configuration, middleware registration, and logging), `myproject/retry.py` (the transient-error catalog and `retry_on_transient` decorator), `myproject/middleware.py` (the request-level retry middleware), and `myapp/views.py` (an example transactional view).

```python
# settings.py

import logging.config
import os

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ["SQL_DATABASE"],          # for example, appdb
        "HOST": os.environ["SQL_SERVER"],            # for example, contoso.database.windows.net
        "PORT": "1433",
        "CONN_MAX_AGE": 300,                         # reuse pooled connections for 5 minutes
        "CONN_HEALTH_CHECKS": True,                  # validate connections before reuse (Django 4.1 and later)
        "ATOMIC_REQUESTS": False,                    # wrap mutating views in transactions explicitly (see the following view example)
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": (
                "Authentication=ActiveDirectoryMsi;"
                "Encrypt=yes;"
                "TrustServerCertificate=no;"
                # Parallel dials to all resolved IPs; safe on single-IP targets.
                "MultiSubnetFailover=Yes;"
                # ODBC driver reconnects connections dropped while idle.
                "ConnectRetryCount=3;"
                "ConnectRetryInterval=10;"
            ),
            # Backend-level retry for the initial connect call. Complements
            # ConnectRetryCount, which only covers idle drops on an
            # already-established connection.
            # See Retry logic and connection resilience for the recognized error list.
            "connection_retries": 3,
            "connection_retry_backoff_time": 5,
        },
    },
}

MIDDLEWARE = [
    # Defined in myproject/middleware.py. Catches transient OperationalErrors
    # and retries the request. Add "1205" (deadlock victim) and "1222"
    # (lock-request timeout) to TRANSIENT_ERROR_CODES to also retry
    # statement-level failures.
    "myproject.middleware.DatabaseRetryMiddleware",
    "django.middleware.security.SecurityMiddleware",
    # ... your other middleware
]

LOGGING_CONFIG = None
logging.config.dictConfig({
    "version": 1,
    "disable_existing_loggers": False,
    "formatters": {
        "json": {
            "format": (
                '{"time":"%(asctime)s","level":"%(levelname)s",'
                '"logger":"%(name)s","message":"%(message)s"}'
            ),
        },
    },
    "handlers": {
        "console": {
            "class": "logging.StreamHandler",
            "formatter": "json",
        },
    },
    "loggers": {
        "django.db.backends": {
            "handlers": ["console"],
            "level": "WARNING",      # raise to INFO or DEBUG to capture SQL
            "propagate": False,
        },
        "django.request": {
            "handlers": ["console"],
            "level": "WARNING",
            "propagate": False,
        },
        "mssql": {
            "handlers": ["console"],
            "level": "INFO",
            "propagate": False,
        },
    },
})
```

Define the shared transient-error catalog and the `retry_on_transient` decorator in `myproject/retry.py`:

```python
# myproject/retry.py
import functools
import logging
import random
import re
import time

from django.db import OperationalError, connection

logger = logging.getLogger(__name__)

TRANSIENT_ERROR_CODES = {
    "64", "233", "4221",
    "10053", "10054", "10928", "10929",
    "40197", "40501", "40613",
    "49918", "49919", "49920",
    # Add "4060" only if targeting Azure SQL with geo-replication failover.
    # Add "1205" (deadlock victim) and "1222" (lock-request timeout) to
    # also retry statement-level failures.
}

# Microsoft ODBC driver formats native error codes as "(<number>)" in the
# message. Parenthesized matches avoid false positives for short codes like "64".
_CODE_RE = re.compile(r"\((\d+)\)")


def is_transient(error):
    codes_in_message = set(_CODE_RE.findall(str(error)))
    return bool(codes_in_message & TRANSIENT_ERROR_CODES)


def retry_on_transient(max_retries=3, base_delay=1, max_delay=30):
    """Retry on transient database errors with exponential backoff and full jitter."""

    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(max_retries + 1):
                try:
                    return func(*args, **kwargs)
                except OperationalError as e:
                    if attempt < max_retries and is_transient(e):
                        capped = min(max_delay, base_delay * (2 ** attempt))
                        delay = random.uniform(0, capped)
                        logger.warning(
                            "Transient error in %s (attempt %d/%d), retrying in %.2fs: %s",
                            func.__name__, attempt + 1, max_retries, delay, e
                        )
                        connection.close()
                        time.sleep(delay)
                        continue
                    raise
        return wrapper
    return decorator
```

Define the request-level middleware in `myproject/middleware.py`. It reuses `is_transient` so both layers recognize the same set of error codes:

```python
# myproject/middleware.py
import logging
import random
import time

from django.db import OperationalError, connection

from myproject.retry import is_transient

logger = logging.getLogger(__name__)


class DatabaseRetryMiddleware:
    """Retry the entire request on transient database errors."""

    def __init__(self, get_response):
        self.get_response = get_response
        self.max_retries = 3
        self.base_delay = 1   # seconds; doubled each attempt
        self.max_delay = 30   # cap on a single sleep, regardless of attempt

    def __call__(self, request):
        for attempt in range(self.max_retries + 1):
            try:
                return self.get_response(request)
            except OperationalError as e:
                if attempt < self.max_retries and is_transient(e):
                    capped = min(self.max_delay, self.base_delay * (2 ** attempt))
                    delay = random.uniform(0, capped)
                    logger.warning(
                        "Transient DB error (attempt %d/%d), retrying in %.2fs: %s",
                        attempt + 1, self.max_retries, delay, e
                    )
                    connection.close()
                    time.sleep(delay)
                    continue
                raise
```

Because `ATOMIC_REQUESTS` is `False`, mutating views must open their own transaction. Wrap the `atomic()` block with `@retry_on_transient` so each retry runs a fresh transaction:

```python
# myapp/views.py
from django.db import transaction
from django.http import JsonResponse

from myproject.retry import retry_on_transient
from .models import Order


# Exponential backoff with full jitter: sleeps random within [0,2], [0,4], [0,8] seconds.
@retry_on_transient(max_retries=3, base_delay=2)
def submit_order(request, order_id):
    with transaction.atomic():
        order = Order.objects.select_for_update().get(id=order_id)
        order.status = "submitted"
        order.save()
    return JsonResponse({"id": order.id, "status": order.status})
```

> [!NOTE]  
> This baseline registers retry at two layers. The middleware acts as a backstop for database access outside of decorated views, such as the admin, signals, or other middleware. The `@retry_on_transient` decorator gives view authors finer control over which operations retry. If a transient error escapes the decorator, the middleware retries the whole request, so the worst case is up to nine attempts before the client sees an error. If that ceiling is too high for your latency budget, drop one layer or lower `max_retries` on the layer you keep.

For more information about each part of this configuration, see [Configuration reference](configuration-reference.md), [Connection options](connection-options.md), [Connection pooling](connection-pooling.md), [Retry logic and connection resilience](retry-logic.md), and [Microsoft Entra authentication](microsoft-entra-authentication.md).

## Key features

- **Drop-in Django backend**: Set `ENGINE` to `"mssql"` and Django's ORM, migrations, admin, and management commands work against SQL Server.
- **Built on pyodbc and ODBC Driver 18**: TLS-encrypted connections by default and broad platform support on Windows, Linux, and macOS.
- **Wide version matrix**: Django 3.2 through 6.0, Python 3.8 through 3.14, and SQL Server 2016 through 2025.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, and integrated flows via `extra_params`.
- **Django migrations**: Schema migrations against SQL Server, including SQL Server-specific column types.
- **JSONField support**: Native `JSONField` backed by the **nvarchar(max)** storage and Django lookups.
- **Always Encrypted**: Client-side encryption for sensitive columns.
- **Bulk operations**: `bulk_create` and `bulk_update` against SQL Server with sensible batch sizes.
- **Transient retry**: Built-in handling for common Azure SQL transient errors during connection and query execution.
- **`inspectdb`**: Generate Django models from existing SQL Server schemas.

## Get started

| Article | Description |
| --- | --- |
| [Installation](installation.md) | Install `mssql-django` and the Microsoft ODBC Driver for SQL Server. |
| [Quickstart: Connect Django to SQL Server](quickstart.md) | Connect a Django project to SQL Server and run your first migration. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Configuration reference](configuration-reference.md) | Full reference for the Django `DATABASES` dictionary with mssql-django. |
| [Connection options](connection-options.md) | `OPTIONS`, `extra_params`, timeouts, and ODBC driver configuration. |
| [Connection pooling](connection-pooling.md) | `CONN_MAX_AGE`, `CONN_HEALTH_CHECKS`, and external pool integration. |
| [Retry logic and connection resilience](retry-logic.md) | Detect transient errors and retry connections and queries. |
| [Microsoft Entra authentication](microsoft-entra-authentication.md) | Passwordless authentication with managed identity, service principal, interactive, and integrated flows. |
| [Security best practices](security-best-practices.md) | Parameterization, secrets management, least privilege, and encryption. |
| [Always Encrypted](always-encrypted.md) | Configure client-side encryption for sensitive columns. |

## Models, migrations, and data types

| Article | Description |
| --- | --- |
| [Database migrations](migrations.md) | Run Django migrations against SQL Server, including SQL Server-specific column types. |
| [Django field to SQL Server type mappings](data-type-mappings.md) | How Django model fields map to SQL Server data types. |
| [JSONField support](json-field.md) | Use `JSONField` with SQL Server and Django lookups. |
| [Reverse engineer models with inspectdb](inspectdb.md) | Generate Django models from existing SQL Server schemas. |
| [Time zone support](timezone-support.md) | `USE_TZ`, **datetimeoffset**, and time zone-aware datetimes. |

## Query and work with data

| Article | Description |
| --- | --- |
| [Bulk operations](bulk-operations.md) | `bulk_create`, `bulk_update`, and batch size tuning. |
| [Transaction management](transactions.md) | `atomic`, isolation levels, savepoints, and deadlock handling. |
| [Raw SQL queries](raw-sql.md) | `RawSQL`, `connection.cursor()`, and SQL Server-specific syntax. |
| [Stored procedures](stored-procedures.md) | Call SQL Server stored procedures from Django. |

## Deploy, test, and tune

| Article | Description |
| --- | --- |
| [Deploy to Azure App Service](deploy-azure-app-service.md) | Ship a Django site to Azure App Service with mssql-django. |
| [Container and local development](container-local-development.md) | Docker containers, devcontainers, and CI pipelines for Django + SQL Server. |
| [Testing](testing.md) | Run Django test suites against SQL Server. |
| [Performance tuning](performance-tuning.md) | Indexes, query patterns, connection reuse, and batch sizes. |
| [Troubleshooting](troubleshooting.md) | Common errors, ODBC diagnostics, and logging. |

## Migrate to mssql-django

| Article | Description |
| --- | --- |
| [Migrate from django-mssql-backend](migrate-from-django-mssql-backend.md) | Move from the community `django-mssql-backend` package to `mssql-django`. |
| [Migrate from other databases](migrate-from-other-databases.md) | Move a Django project from another database backend to SQL Server. |
| [Migrate from PostgreSQL](migrate-from-postgresql.md) | One-stop guide for Django developers moving from PostgreSQL to SQL Server. |

## Related tasks

| Article | Description |
| --- | --- |
| [Support lifecycle](support-lifecycle.md) | Supported Django, Python, and SQL Server versions. |
| [What's new](whats-new.md) | Version history and release highlights. |
| [Limitations and unsupported features in mssql-django](limitations.md) | Backend limitations and unsupported features. |
| [FAQ](faq.yml) | Frequently asked questions. |

## Related content

- [mssql-django on GitHub](https://github.com/microsoft/mssql-django)
- [mssql-django on PyPI](https://pypi.org/project/mssql-django/)
- [mssql-django wiki](https://github.com/microsoft/mssql-django/wiki)
- [Release notes](https://github.com/microsoft/mssql-django/releases)
- [Django documentation](https://docs.djangoproject.com/en/stable/)
- [Azure Python Developer Center](https://azure.microsoft.com/develop/python/)
