---
title: Connection Pooling in mssql-django
description: Configure and manage connection pooling behavior for the mssql-django Django database backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Connection pooling in mssql-django

This article explains how connection pooling works in `mssql-django` and how to configure it for your Django application.

## How connection pooling works

By default, `mssql-django` uses pyodbc's built-in connection pooling. When a connection is closed by Django, pyodbc returns it to a pool instead of closing the underlying ODBC connection. Subsequent connection requests reuse pooled connections, which reduces the overhead of establishing new database connections.

## Configure connection pooling

Connection pooling is controlled by the `DATABASE_CONNECTION_POOLING` setting, which is placed at the module level in `settings.py` (outside the `DATABASES` dictionary):

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
    },
}

# Set to False to disable pyodbc's connection pooling
DATABASE_CONNECTION_POOLING = False
```

| Value | Behavior |
| --- | --- |
| `True` (default) | Connection pooling is enabled. Closed connections are returned to the pool. |
| `False` | Connection pooling is disabled. Each connection is fully closed when released. |

## When to disable connection pooling

Consider disabling connection pooling in these scenarios:

- **Token-based authentication**: When using access tokens that expire, pooled connections might hold stale tokens.
- **Debugging connection issues**: Disabling pooling simplifies troubleshooting by ensuring each request creates a fresh connection.
- **Short-lived processes**: For scripts or management commands that make a few queries and exit, pooling adds no benefit.

## Connection retry settings

Regardless of the pooling setting, you can configure retry behavior for failed connection attempts. For the full list of retry and timeout options, see [Configuration reference](configuration-reference.md#options).

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
            "connection_retries": 3,
            "connection_retry_backoff_time": 10,
            "connection_timeout": 30,
        },
    },
}
```

## Django's CONN_MAX_AGE

Django also provides a `CONN_MAX_AGE` setting that controls how long Django keeps a database connection open before closing it. This setting works alongside pyodbc's connection pooling:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "CONN_MAX_AGE": 600,  # Keep connections open for 10 minutes
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

For more information about `CONN_MAX_AGE`, see the [Django database settings documentation](https://docs.djangoproject.com/en/stable/ref/settings/#conn-max-age).

Practical starting points:

- `CONN_MAX_AGE=0`: safest for debugging and short-lived jobs.
- `CONN_MAX_AGE=600`: good default for many web apps.
- `CONN_MAX_AGE=3600`: reasonable for steady high-throughput services after load testing.

> [!NOTE]  
> When using ASGI servers (such as Daphne or Uvicorn) or threaded deployments, persistent connections can leak across async contexts. If you use `CONN_MAX_AGE` with an ASGI server, set `CONN_HEALTH_CHECKS = True` (Django 4.1 and later) and test under realistic concurrency. For more information, see the Django documentation on [connection management](https://docs.djangoproject.com/en/stable/ref/databases/#connection-management).

`CONN_HEALTH_CHECKS` validates pooled connections before reuse. If Django detects a stale connection, it transparently opens a fresh one. This adds a small per-request check cost and is usually worth enabling for long-lived processes.

## Related content

- [mssql-django configuration reference](configuration-reference.md)
- [Connection options for mssql-django](connection-options.md)
- [Retry logic and connection resilience with mssql-django](retry-logic.md)
- [Performance tuning for mssql-django](performance-tuning.md)
- [Troubleshoot mssql-django](troubleshooting.md)
