---
title: Connection Options for mssql-django
description: Configure ODBC driver selection, DSN, FreeTDS, timeouts, and connection retries in the mssql-django OPTIONS dictionary.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 07/31/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Connection options for mssql-django

This article explains the `OPTIONS` dictionary settings in your Django `DATABASES` configuration. These settings control how `mssql-django` connects to SQL Server through the ODBC driver.

## ODBC driver selection

As of `mssql-django` 1.7, the backend defaults to ODBC Driver 18 for SQL Server. If ODBC Driver 18 isn't installed, the backend automatically falls back to ODBC Driver 17.

> [!NOTE]
> ODBC Driver 18 enables `Encrypt=yes` by default and validates the server certificate. Connections that worked with Driver 17 can fail with an SSL/TLS trust error. To resolve the failure:
>
> - For on-premises SQL Server, install a server certificate from a certificate authority your clients already trust, or import the existing server certificate into each client trust store. For instructions, see [Configure SQL Server Database Engine for encrypting connections](../../../database-engine/configure-windows/configure-sql-server-encryption.md).
> - If you connect by IP address or by an alias that doesn't match the certificate's subject or subject alternative name (SAN), add `HostNameInCertificate=<name-from-certificate>` to `extra_params`.
>
> For local development against a self-signed certificate, see `TrustServerCertificate` in [Extra ODBC parameters](#extra-odbc-parameters).

You can specify the driver explicitly:

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
            "driver": "ODBC Driver 17 for SQL Server",
        },
    },
}
```

On Linux, you can also specify the full path to the driver library:

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
            "driver": "/opt/microsoft/msodbcsql18/lib64/libmsodbcsql-18.0.so.1.1",
        },
    },
}
```

## DSN vs HOST

You can connect using either a `HOST` name or a named DSN (Data Source Name).

### Connect with HOST

Most configurations use the `HOST` setting directly:

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
```

### Connect with DSN

Use a named DSN configured in your ODBC data sources:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "OPTIONS": {
            "dsn": "MyDataSourceName",
        },
    },
}
```

## FreeTDS support

To use FreeTDS as the ODBC driver, set `host_is_server` to `True`. This tells the backend to use `HOST` and `PORT` directly instead of looking up a dataserver name in `freetds.conf`:

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
            "driver": "FreeTDS",
            "host_is_server": True,
        },
    },
}
```

For more information about DSN-less connections with FreeTDS, see [FreeTDS user guide](https://www.freetds.org/userguide/dsnless.html).

## Extra ODBC parameters

Use `extra_params` to pass additional ODBC connection string parameters. The value is a semicolon-delimited string appended to the connection string:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "TrustServerCertificate=yes;ApplicationIntent=ReadOnly",
        },
    },
}
```

This setting is also used for [Microsoft Entra authentication](microsoft-entra-authentication.md) keywords.

When connecting to Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance, add `MultiSubnetFailover=Yes` to `extra_params`. The driver attempts connections to all resolved endpoints in parallel and completes authentication with the first responsive one. It's safe on single-IP targets: when DNS resolves to one address, the driver does a single connect attempt with no measurable overhead, so you can leave the setting on for all Django deployments that target a Microsoft SQL family endpoint.

[!INCLUDE [trust-server-certificate-caution](includes/trust-server-certificate-caution.md)]

## Connection timeouts and retries

Configure connection resilience with timeout and retry settings:

| Option | Default | Description |
| --- | --- | --- |
| `connection_timeout` | `0` (disabled) | Maximum seconds to wait for a connection. |
| `connection_retries` | `5` | Number of retry attempts on connection failure. |
| `connection_retry_backoff_time` | `5` | Seconds to wait between retry attempts. |
| `query_timeout` | `0` (disabled) | Maximum seconds to wait for a query to complete. |

Example:

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
            "connection_timeout": 30,
            "connection_retries": 3,
            "connection_retry_backoff_time": 10,
            "query_timeout": 120,
        },
    },
}
```

`connection_timeout=0` is the mssql-django default. Because pyodbc only calls `SQLSetConnectAttr(SQL_ATTR_LOGIN_TIMEOUT, ...)` when you supply a positive value, the [driver-dependent default](../../../odbc/reference/syntax/sqlsetconnectattr-function.md#comments) applies (15 seconds for the Microsoft ODBC Driver for SQL Server). Set an explicit value so unresponsive connect attempts fail predictably.

If the target is [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use at least `60`. An auto-paused database resumes on the first connect, and the resume can take 30 to 60 seconds or more. With a shorter timeout, the first connect attempt times out before the resume completes. `connection_retries` recovers eventually, but the first request pays multiple timeout intervals before it succeeds.

## Collation

Set a custom collation for text field lookups:

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
            "collation": "Chinese_PRC_CI_AS",
        },
    },
}
```

## Multiple database connections

Django supports connecting to multiple databases simultaneously. This is useful for read replicas, cross-database queries, or separating workloads by isolation level.

### Configure multiple databases

Define each connection in the `DATABASES` setting:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "app_db",
        "HOST": "<your-primary-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
    "readonly": {
        "ENGINE": "mssql",
        "NAME": "app_db",
        "HOST": "<your-readonly-replica>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Encrypt=yes;ApplicationIntent=ReadOnly",
        },
    },
    "analytics": {
        "ENGINE": "mssql",
        "NAME": "analytics_db",
        "HOST": "<your-analytics-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "isolation_level": "READ UNCOMMITTED",
        },
    },
}
```

> [!CAUTION]  
> `READ UNCOMMITTED` allows dirty reads. Use this isolation level only for reporting or analytics queries where absolute accuracy isn't required. For more information, see [Transaction management](transactions.md#read-data-without-blocking-nolock-equivalent).

### Route queries with a database router

Create a database router to direct read and write operations to the appropriate connection:

```python
class ReadReplicaRouter:
    """Route read queries to the readonly replica, writes to the primary."""

    def db_for_read(self, model, **hints):
        return "readonly"

    def db_for_write(self, model, **hints):
        return "default"

    def allow_relation(self, obj1, obj2, **hints):
        return True

    def allow_migrate(self, db, app_label, model_name=None, **hints):
        return db == "default"
```

Register the router in `settings.py`:

```python
DATABASE_ROUTERS = ["myproject.routers.ReadReplicaRouter"]
```

Save the router class in a file such as `myproject/routers.py`.

### Query a specific database directly

Use the `using()` method to query a specific database alias:

```python
# Explicit read from analytics database
reports = AnalyticsReport.objects.using("analytics").filter(date__gte="2025-01-01")

# Write to default
Product.objects.create(name="Widget", price=9.99)
```

For more information about isolation levels on per-connection databases, see [Read data without blocking](transactions.md#read-data-without-blocking-nolock-equivalent).

## Related content

- [mssql-django configuration reference](configuration-reference.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Retry logic and connection resilience with mssql-django](retry-logic.md)
- [Security best practices for mssql-django](security-best-practices.md)
- [Connection pooling in mssql-django](connection-pooling.md)
- [Troubleshoot mssql-django](troubleshooting.md)
