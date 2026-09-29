---
title: Select the Database Driver for mssql-django
description: Learn how to opt a Django database alias into the mssql-python driver instead of the default pyodbc driver in mssql-django.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sharmag, sumitsar
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Select the database driver for mssql-django

Starting with version 2.0, `mssql-django` connects through either of two Python database drivers:

- [pyodbc](https://pypi.org/project/pyodbc/) with an externally installed Microsoft ODBC Driver for SQL Server. This driver is the default.
- [mssql-python](../mssql-python/python-sql-driver-mssql-python.md), Microsoft's Python driver, which doesn't need a separately installed ODBC driver.

You choose the driver for each database alias. One alias can use `mssql-python` while the rest of the project stays on `pyodbc`. The `ENGINE` value remains `"mssql"` in both cases.

## Opt an alias into mssql-python

Set the `python_driver` option in that alias's `OPTIONS` dictionary:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<database>",
        "USER": "<user_id>",
        "PASSWORD": "<password>",
        "HOST": "<server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "python_driver": "mssql_python",
            "extra_params": "Encrypt=yes",
        },
    },
}
```

The backend accepts `"mssql_python"`, `"mssql-python"`, and `"python"`, and the comparison ignores case. Omit `python_driver`, leave it empty, or set it to `"pyodbc"` to keep the default driver. Because the setting is per alias, you roll back one database at a time by removing the option.

The `mssql-python` module is imported only when an alias selects it. If the installed version is earlier than 1.15.0, the backend raises `ImproperlyConfigured` with the required version.

## Install requirements

`pip install mssql-django` installs both drivers. The `mssql-python` path has no separate ODBC driver install. A `--no-deps` install, or a private index that doesn't mirror `mssql-python`, leaves the package missing and the alias fails at import time.

Install the [platform prerequisites for mssql-python](https://github.com/microsoft/mssql-python#installation), including OpenSSL on macOS and the required libraries on Linux.

Because `mssql-python` is a required dependency, `mssql-django` 2.0 installs only on platforms that have a compatible `mssql-python` distribution. For the platform list, see [mssql-django support and lifecycle](support-lifecycle.md#operating-system-compatibility).

## Behavior differences

The two drivers build different connection strings and expose different connection keywords. Review this section before you switch an alias.

### Connection settings

| Setting | pyodbc | mssql-python |
| --- | --- | --- |
| `HOST` and `PORT` | Emitted as `SERVER`, `SERVERNAME`, or `SERVER` plus `PORT`, depending on the driver and `host_is_server`. | Always emitted as `SERVER=<host>,<port>`. An empty `HOST` becomes `localhost`. |
| `driver` | Selects the ODBC driver. Defaults to Microsoft ODBC Driver 18 for SQL Server, with automatic fallback to Driver 17. | Ignored. There's no Driver 17 fallback. |
| `dsn` | Supported. | Ignored. |
| `host_is_server` | Supported for FreeTDS. | Ignored. |
| `unicode_results` | Supported. | Ignored. |
| `TOKEN` | Supported. | Supported. Supply `TOKEN` without `USER`, `PASSWORD`, or an `Authentication` keyword. Your application acquires and renews the token. |
| `DATABASE_CONNECTION_POOLING` | Applies. | Applies. |

Timeouts, retries, isolation level, collation, and `return_rows_bulk_insert` behave the same on both paths.

### Extra connection parameters

`mssql-python` 1.15 validates `extra_params` against an allow list and rejects anything outside it. Supported keywords include `Authentication`, `Encrypt`, `TrustServerCertificate`, `HostnameInCertificate`, `ServerCertificate`, `ServerSPN`, `MultiSubnetFailover`, `ApplicationIntent`, `ConnectRetryCount`, `ConnectRetryInterval`, `KeepAlive`, `KeepAliveInterval`, `IpAddressPreference`, and `PacketSize`.

The driver rejects `DRIVER`, `DSN`, `SERVERNAME`, and `MARS_Connection`, along with pyodbc-only keywords such as `APP`, `LongAsMax`, `ColumnEncryption`, `WSID`, `AnsiNPW`, `QuotedId`, `Regional`, `UseFMTONLY`, `Current Language`, `Network Library`, `Description`, and `Connect Timeout`. Remove those keywords before you switch an alias, and use the `connection_timeout` option in place of `Connect Timeout`.

When `extra_params` sets a keyword that the backend also generates, the explicit value wins.

### Multiple Active Result Sets

On the `pyodbc` path, the backend adds `MARS_Connection=yes` when the alias uses a Microsoft ODBC driver on Windows. An explicit `MARS_Connection` value in `extra_params` is honored instead, and the match ignores case.

The `mssql-python` path never enables MARS, and it rejects the `MARS_Connection` keyword, so you can't turn MARS on for that alias.

Without MARS, `QuerySet.iterator()` reads the full result into memory before yielding rows so that a nested query can reuse the connection, and `chunk_size` doesn't change that. Account for the memory cost on large querysets.

For endpoints that reject MARS, such as Microsoft Fabric Warehouse, see [Disable MARS](connection-options.md#disable-mars).

### Encoding configuration

Both drivers accept `setencoding` and `setdecoding`, and each entry goes to the connection method of the selected driver. Every `setdecoding` entry needs a `sqltype` key on both paths, and the same entry works on either driver. One difference: `mssql-python` accepts `-99` for `SQL_WMETADATA`, and `pyodbc` rejects it.

## Choose between the drivers

For new development, use `mssql-python`. It removes the ODBC driver installation step from container images and app service deployments.

Use `pyodbc` when your deployment depends on a named DSN, FreeTDS, Always Encrypted through the `ColumnEncryption` keyword, an ODBC driver version that you manage yourself, or MARS. For what MARS requires on each path, see [Multiple Active Result Sets](#multiple-active-result-sets).

Existing projects can stay on `pyodbc`. It remains the default and is fully supported. When you do move, switch one alias at a time and run your test suite against it before you move the rest.

## Related content

- [Connection options for mssql-django](connection-options.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Connection pooling in mssql-django](connection-pooling.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [mssql-python driver for SQL Server](../mssql-python/python-sql-driver-mssql-python.md)
