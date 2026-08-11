---
title: mssql-django Configuration Reference
description: Review the complete settings.py reference for the mssql-django Django database backend for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 08/03/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# mssql-django configuration reference

This article is the complete `settings.py` configuration reference for the `mssql-django` Django database backend. Configure SQL Server connectivity by editing the `DATABASES` dictionary in your Django project's `settings.py` file.

## Minimal configuration

The following example shows the minimum required configuration:

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

## Database connection settings

These settings configure your `DATABASES` connection. Most are standard Django settings; exceptions are noted:

| Setting | Type | Description |
| --- | --- | --- |
| `ENGINE` | String | Must be `"mssql"`. |
| `NAME` | String | Database name. Required. |
| `HOST` | String | Server hostname or IP address. Use `"server\\instance"` format for named instances. |
| `PORT` | String | Server instance port. Empty string means default port. |
| `USER` | String | Database user name. If not given, Windows Integrated Security is used. |
| `PASSWORD` | String | Database user password. |
| `AUTOCOMMIT` | Boolean | Set to `False` to disable Django's transaction management. Default is `True`. |
| `Trusted_Connection` | String | Set to `"yes"` (default) to use Windows Integrated Security when `USER` isn't provided. When `USER` and `PASSWORD` are both set, those credentials take precedence and `Trusted_Connection` is ignored. Set to `"no"` to disable Windows authentication explicitly. This is an `mssql-django` extension, not a standard Django setting. |
| `TOKEN` | String | Access token for Microsoft Entra authentication (for example, via `azure.identity`). This is an `mssql-django` extension, not a standard Django setting. |

## TEST settings

These settings control the test database used by Django's test runner:

| Setting | Type | Description |
| --- | --- | --- |
| `NAME` | String | Test database name. Default: `"test_" + NAME`. |
| `COLLATION` | String | Collation for the test database. Default: instance default. |
| `DEPENDENCIES` | List | Creation-order dependencies of the database. |
| `MIRROR` | String | Alias of a database to mirror during testing. |

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
        },
        "TEST": {
            "NAME": "test_mydb",
            "COLLATION": "SQL_Latin1_General_CP1_CI_AS",
        },
    },
}
```

## OPTIONS

The `OPTIONS` dictionary provides backend-specific configuration. Place these settings inside the `DATABASES["default"]["OPTIONS"]` dictionary:

| Option | Type | Default | Description |
| --- | --- | --- | --- |
| `driver` | String | `"ODBC Driver 18 for SQL Server"` | ODBC driver to use. Automatically falls back to Driver 17 if 18 isn't installed. |
| `isolation_level` | String | `None` | Transaction isolation level: `READ UNCOMMITTED`, `READ COMMITTED`, `REPEATABLE READ`, `SNAPSHOT`, or `SERIALIZABLE`. |
| `dsn` | String | `None` | Named DSN. Can be used instead of `HOST`. |
| `host_is_server` | Boolean | `False` | Set to `True` to use `HOST`/`PORT` directly with FreeTDS instead of a `freetds.conf` dataserver name. |
| `unicode_results` | Boolean | `False` | Activate pyodbc's `unicode_results` feature. |
| `extra_params` | String | `None` | Additional ODBC parameters in `"param=value;param=value"` format. Used for [Microsoft Entra authentication](microsoft-entra-authentication.md). |
| `collation` | String | `None` | Collation for text field lookups (for example, `"Chinese_PRC_CI_AS"`). |
| `connection_timeout` | Integer | `0` | Connection timeout in seconds (`0` = disabled). Use at least 60 for [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled. An auto-paused database resumes on the first connect, and the resume can take 30 to 60 seconds or more. |
| `connection_retries` | Integer | `5` | Number of connection retry attempts. |
| `connection_retry_backoff_time` | Integer | `5` | Back-off time in seconds between retries. |
| `query_timeout` | Integer | `0` | Query timeout in seconds (`0` = disabled). |
| `setencoding` / `setdecoding` | List | `None` | pyodbc [encoding](https://github.com/mkleehammer/pyodbc/wiki/Connection#setencoding) / [decoding](https://github.com/mkleehammer/pyodbc/wiki/Connection#setdecoding) configuration. |
| `return_rows_bulk_insert` | Boolean | `False` | Allow returning rows from bulk insert. Must be `False` if tables have triggers. |
| `datefirst` | Integer | `7` | First day of week for `SET DATEFIRST`. `7` is Sunday (SQL Server default), `1` is Monday. |
| `driver_needs_utf8` | Presence-only option | Not set | Enable UTF-8 encoding for the ODBC driver. This option is enabled when the key is present in `OPTIONS`; the value itself is ignored. Typically needed with FreeTDS or older unixODBC configurations that default to Latin-1. |
| `driver_charset` | String | `None` | Explicit character set for the ODBC driver (for example, `"UTF-8"`). Use with `driver_needs_utf8` when the driver doesn't auto-detect encoding. |
| `connection_recovery_interval_msec` | Float | `0.0` | Milliseconds to wait between retry attempts in the backend's connection recovery loop after a network error. Increase this value for unstable network links. |

Example with common options:

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
            "extra_params": "Encrypt=yes",
            "isolation_level": "READ COMMITTED",
            "connection_timeout": 30,
            "connection_retries": 3,
            "connection_retry_backoff_time": 5,
            "query_timeout": 60,
        },
    },
}
```

## Backend-specific settings

This setting is placed at the module level in `settings.py`, outside the `DATABASES` dictionary:

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `DATABASE_CONNECTION_POOLING` | Boolean | `True` | Set to `False` to disable pyodbc's connection pooling. |

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
        },
    },
}

# Set this to False to disable pyodbc's connection pooling
DATABASE_CONNECTION_POOLING = False
```

## Related content

- [Connection options for mssql-django](connection-options.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Connection pooling in mssql-django](connection-pooling.md)
- [Django database settings](https://docs.djangoproject.com/en/stable/ref/settings/#databases)
