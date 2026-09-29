---
title: Troubleshoot mssql-django
description: Diagnose and resolve common issues when using the mssql-django Django backend with SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sharmag, sumitsar
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot mssql-django

Diagnose and resolve common issues with the `mssql-django` backend for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

`mssql-django` 2.0 supports the default pyodbc driver path and an opt-in mssql-python driver path. For more information, see [Select the database driver for mssql-django](select-database-driver.md).

## Connection issues

This section covers the most common connection errors and how to resolve them.

### ODBC driver not found on the pyodbc path

**Symptoms**:

```output
django.core.exceptions.ImproperlyConfigured: 'ODBC Driver 18 for SQL Server' is not a recognized ODBC driver
```

Or:

```output
Error: ('01000', "[01000] [unixODBC][Driver Manager]Can't open lib 'ODBC Driver 18 for SQL Server'")
```

**Possible causes and solutions**:

- **ODBC driver not installed**

  Install the Microsoft ODBC Driver for SQL Server when you use the default pyodbc path. For download links, see [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md). The mssql-python path doesn't use an externally installed ODBC driver.

- **Multiple driver versions installed**

  Specify the exact driver name or path in `settings.py`:

  ```python
  DATABASES = {
      "default": {
          "ENGINE": "mssql",
          "NAME": "<database>",
          "USER": "<user_id>",
          "PASSWORD": "<password>",
          "HOST": "<server>",
          "PORT": "1433",
          "OPTIONS": {
              "driver": "ODBC Driver 17 for SQL Server",
          },
      },
  }
  ```

  On Linux, specify the full path:

  ```python
  "OPTIONS": {
      "driver": "/opt/microsoft/msodbcsql17/lib64/libmsodbcsql-17.10.so.6.1",
  },
  ```

- **Check installed drivers**

  - On Linux/macOS, run `odbcinst -q -d`.
  - On Windows, check **ODBC Data Sources** in **Administrative Tools**.

### mssql-python rejects a connection option

**Symptoms**:

An alias that sets `"python_driver": "mssql_python"` fails during connection setup after you move pyodbc connection-string keywords into `OPTIONS["extra_params"]`, with one of these errors:

```output
mssql_python.exceptions.ConnectionStringParseError: Connection string parsing failed:
  Unknown keyword 'longasmax' is not recognized
```

```output
mssql_python.exceptions.ConnectionStringParseError: Connection string parsing failed:
  Reserved keyword 'driver' is controlled by the driver and cannot be specified by the user
```

The keyword name in the message is lowercased, so a keyword you wrote as `LongAsMax` appears as `longasmax`. `ConnectionStringParseError` isn't part of the DB-API exception hierarchy, so Django doesn't rewrap it as a `django.db.utils` error.

**Possible causes and solutions**:

- **pyodbc-only keyword in `extra_params`**

  The mssql-python path validates `extra_params` against an allow list. `DRIVER` and `APP` are reserved for the driver and produce the `Reserved keyword` form. `DSN`, `SERVERNAME`, `MARS_Connection`, and pyodbc-only keywords such as `LongAsMax`, `ColumnEncryption`, `WSID`, `AnsiNPW`, `QuotedId`, `Regional`, `UseFMTONLY`, `Current Language`, `Network Library`, `Description`, and `Connect Timeout` aren't in the allow list and produce the `Unknown keyword` form. Remove the keyword, or use the default pyodbc path for an alias that needs that ODBC option.

- **Driver option expected to control mssql-python**

  The mssql-python path ignores `driver`, `dsn`, `host_is_server`, and `unicode_results`. `HOST` and `PORT` become `SERVER=<server>,<port>`, and an empty `HOST` becomes `localhost`.

### mssql-python dependency is too old

**Symptoms**:

An alias that sets `"python_driver": "mssql_python"` fails at connection setup with one of these errors:

```output
django.core.exceptions.ImproperlyConfigured: mssql-python 1.15.0 or newer is required; you have 1.14.0
```

```output
django.core.exceptions.ImproperlyConfigured: The 'python_driver' connection option requests mssql-python, but the module could not be imported: No module named 'mssql_python'. Install it with 'pip install "mssql-python>=1.15.0"'.
```

The second form means the `mssql_python` module isn't importable at all.

**Solution**: Install `mssql-python>=1.15.0`. `mssql-django` 2.0 declares `mssql-python>=1.15.0`, so a normal `pip install mssql-django` resolves a compatible version on supported platforms.

### Driver 17 fallback doesn't apply to mssql-python

**Symptoms**:

An alias that sets `"python_driver": "mssql_python"` still fails even though Microsoft ODBC Driver 17 for SQL Server is installed.

There's no distinctive error for this case. The mssql-python path ignores the `driver` option silently, so the connection fails with whatever underlying error applies. If you moved the driver name into `extra_params` instead, you get a `Reserved keyword 'driver'` error. See [mssql-python rejects a connection option](#mssql-python-rejects-a-connection-option).

**Solution**: Use the default pyodbc path if the alias must use an externally installed ODBC Driver 17. The mssql-python path doesn't fall back to Driver 17, and it ignores the `driver` option. That path doesn't need a separately installed ODBC driver.

### Connection refused

**Symptoms**:

```output
django.db.utils.OperationalError: ('08001', '[08001] ... TCP Provider: Error code 0x2749 ...')
```

**Possible causes and solutions**:

- **TCP/IP not enabled on SQL Server**

  - Open **SQL Server Configuration Manager**.
  - Under **SQL Server Network Configuration**, enable **TCP/IP**.
  - In **TCP/IP Properties**, activate the IP address used for the connection.
  - Restart the SQL Server service.

- **Firewall blocking port 1433**

  - Verify that firewall rules allow inbound connections on port 1433.
  - For Azure SQL, add your client IP in the Azure portal firewall settings.

- **Wrong server name or port**

  Verify the `HOST` and `PORT` values in your configuration.

### Login failed

**Symptoms**:

```output
django.db.utils.OperationalError: ('28000', "[28000] [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Login failed for user '<user_id>'. (18456) (SQLDriverConnect); [28000] [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Login failed for user '<user_id>'. (18456)")
```

On the mssql-python path:

```output
django.db.utils.OperationalError: Driver Error: Invalid authorization specification; DDBC Error: [Microsoft][SQL Server]Login failed for user '<user_id>'.
```

**Possible causes and solutions**:

- **Incorrect credentials**

  Verify the username and password.

- **The database in `NAME` doesn't exist**

  On SQL Server, the mssql-python path raises the same `OperationalError` with the same message as a bad password, so the message alone doesn't tell you which one you hit. Confirm that the database exists before you change credentials. Point `NAME` at `master` to test the login on its own: if that connects, the credentials are correct and the database is the problem. The pyodbc path reports this case separately as `Cannot open database "<database>" requested by the login. The login failed. (4060)`.

  Azure SQL Database reports this case differently. The mssql-python path raises `Driver Error: General error; DDBC Error: [Microsoft][SQL Server]Cannot open server "<server>" requested by the login.  The login failed.` The message names the server, but the server name is fine. Check `NAME` instead.

- **User doesn't exist**

  Confirm that the login is mapped to a user in the target database.

- **SQL Server authentication disabled**

  Enable mixed mode authentication, or use Windows or Microsoft Entra authentication.

### Connection timeout

**Symptoms**:

```output
django.db.utils.OperationalError: ('HYT00', '[HYT00] [Microsoft][ODBC Driver 18 for SQL Server]Login timeout expired')
```

**Possible causes and solutions**:

- **Network latency**

  Increase `connection_timeout` in OPTIONS.

- **Azure SQL Database serverless with auto-pause enabled**

  An auto-paused database resumes on the first connect attempt, and that attempt can fail with error 40613 while the database resumes. Set `connection_timeout` to at least 60 and retry the first connection. For more information, see [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) and [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume).

- **Server overloaded**

  Increase `connection_retries` and `connection_retry_backoff_time`.

  ```python
  "OPTIONS": {
      "driver": "ODBC Driver 18 for SQL Server",
      "connection_timeout": 30,
      "connection_retries": 5,
      "connection_retry_backoff_time": 10,
  },
  ```

## Migration issues

These errors occur during Django migration operations against SQL Server.

## Raw SQL and GROUP BY issues

These errors occur when raw or annotated queries with a `GROUP BY` clause pass through the backend's placeholder-rewriting step.

### `IndexError` on GROUP BY with escaped `%%` and real params

**Symptoms**:

```output
IndexError: Replacement index N out of range for positional args tuple
```

The query works without the `GROUP BY` clause and works without the escaped `%%` literal, but fails when both are present alongside a real `%s` parameter.

**Solution**: Upgrade to a current `mssql-django` release. The backend narrows the placeholder-rewriting regex to `%%` and `%s` only, so escaped `%%` literals are preserved verbatim and no phantom placeholders are injected.

### `NotImplementedError` for `IntegerChoices` in raw GROUP BY queries

**Symptoms**:

```output
NotImplementedError: Not supported type <enum '...'> (StatusChoices.IN_PROGRESS)
```

The same enum value works in ORM queries and in raw queries without `GROUP BY`, but fails when passed as a parameter to a raw query that contains a `GROUP BY` clause.

**Solution**: Upgrade to a current `mssql-django` release. The backend uses `isinstance` for parameter type checks in the `GROUP BY` path, so `IntegerChoices` (an `int` subclass) binds correctly. `bool` still binds **bit**, and plain `int` is unchanged.

## Regex lookup issues

### `__regex` or `__iregex` returns no rows

**Symptoms**: The query runs without error and returns an empty result set, even though rows match the pattern.

```python
Product.objects.filter(name__regex=r"^Widget \d+$")  # no rows, though "Widget 42" exists
```

**Cause**: `dbo.REGEXP_LIKE` ignores literal whitespace in the pattern. The pattern is matched as though it were `^Widget\d+$`, which no value containing a space can satisfy. Nothing raises, so the empty result looks like a data problem.

**Solution**: Write whitespace as an escape or a character class:

```python
Product.objects.filter(name__regex=r"^Widget\s\d+$")
Product.objects.filter(name__regex=r"^Widget[ ]\d+$")
```

### `Cannot find ... dbo.REGEXP_LIKE`

**Symptoms**:

```output
django.db.utils.ProgrammingError: ('42000', '[42000] [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Cannot find either column "dbo" or the user-defined function or aggregate "dbo.REGEXP_LIKE", or the name is ambiguous. (4121) (SQLExecDirectW)')
```

On the `mssql-python` path:

```output
django.db.utils.ProgrammingError: Driver Error: Syntax error or access violation; DDBC Error: [Microsoft][SQL Server]Cannot find either column "dbo" or the user-defined function or aggregate "dbo.REGEXP_LIKE", or the name is ambiguous.
```

**Cause**: The CLR assembly isn't installed in the database you're querying. It's installed per database, not per server.

**Solution**: Run `python manage.py install_regex_clr <database>` against that database. Rerun it after dropping and recreating a database. See [Set up regex lookups](limitations.md#set-up-regex-lookups).

## Date and time issues

### `Now()` values are shifted when `USE_TZ=True`

**Symptoms**:

Timestamps written with Django `Now()`, `auto_now`, or `auto_now_add` are shifted when the SQL Server host time zone isn't UTC.

**Solution**: Upgrade to a current `mssql-django` release. The backend generates time zone-aware `Now()` SQL, preserves **datetimeoffset** offsets, and reads time zone data through `zoneinfo` and `tzdata`.

### `AttributeError` when calling `.explain()`

**Symptoms**:

```output
AttributeError: ... explain_format ...
```

**Solution**: Upgrade to a current `mssql-django` release. The backend handles explain metadata for every supported Django version.

### Cannot alter AutoField

**Symptoms**:

```output
django.db.utils.ProgrammingError: Cannot alter column to or from an IDENTITY column
```

**Solution**: SQL Server doesn't support altering a field from or to `AutoField`. Create a new model with the desired field type, migrate the data manually, and then drop the old table. For workarounds, see [Database migrations with mssql-django](migrations.md).

### Rename fails with foreign key constraint

**Symptoms**:

```output
django.db.utils.ProgrammingError: ... could not drop constraint ...
```

**Solution**: SQL Server requires dropping foreign key constraints before renaming columns. Use `SeparateDatabaseAndState` in your migration. For an example, see [Database migrations with mssql-django](migrations.md).

## Encoding issues

Encoding errors typically occur on the pyodbc path when `pyodbc` misinterprets character data from SQL Server.

### Unicode encoding errors

**Symptoms**:

```output
UnicodeDecodeError: 'utf-8' codec can't decode byte ...
```

**Solution**: Configure `pyodbc` encoding in the `OPTIONS` dictionary. The mssql-python path ignores `unicode_results`.

```python
"OPTIONS": {
    "driver": "ODBC Driver 18 for SQL Server",
    "unicode_results": True,
},
```

## FreeTDS issues

FreeTDS requires pyodbc-specific configuration that differs from the Microsoft ODBC driver.

### host_is_server error

**Symptoms**:

Connection fails when using FreeTDS without specifying `host_is_server`.

**Solution**: Set `host_is_server` to `True` when you use FreeTDS:

```python
"OPTIONS": {
    "driver": "FreeTDS",
    "host_is_server": True,
},
```

For more information about FreeTDS configuration, see [Connection options for mssql-django](connection-options.md).

## Test database issues

Test database creation and destruction can fail depending on your authentication method.

### Can't create test database with managed identity

**Symptoms**:

```output
django.db.utils.DatabaseError: ('42000', '[42000] ... EXECUTE permission denied on object ...')
```

Or:

```output
django.db.utils.OperationalError: ('28000', ... login failed ...)
```

The test runner fails to create or destroy the test database when you use `ActiveDirectoryMsi` (managed identity) authentication. This limitation exists because:

- Managed identity credentials are obtained from the host environment (such as Azure VM and App Service).

- The test runner attempts to connect using the *test* database credentials during teardown.

- Managed identity can be granted database-level roles, but test database creation and deletion usually require server-level permissions that test runners often don't have.

**Affected authentication methods**:

- `ActiveDirectoryMsi` (Azure managed identity)
- `ActiveDirectoryServicePrincipal` (when configured at server scope only)

**Supported authentication methods** (test database creation works):
- `ActiveDirectoryPassword`
- `ActiveDirectoryIntegrated`
- SQL authentication (username/password)

### Authentication trade-offs for test environments

| Method | Secretless | Works with automatic test DB create/drop | Typical use |
| --- | --- | --- | --- |
| `ActiveDirectoryMsi` | Yes | Usually no (unless server-level rights are granted) | Azure-hosted production workloads |
| `ActiveDirectoryServicePrincipal` | No (client secret/cert) | Depends on granted server-level rights | CI/CD with explicit identity management |
| `ActiveDirectoryPassword` | No | Yes (with sufficient SQL permissions) | Developer and controlled CI environments |
| SQL authentication | No | Yes (with sufficient SQL permissions) | Local or isolated test environments |

**Solutions**:

- **For development**: Use `--keepdb` flag to skip test database teardown:

    ```bash
    python manage.py test --keepdb
    ```

- **For CI/CD pipelines**: Pre-create a dedicated test database and grant the managed identity `CREATE TABLE` and `ALTER` permissions:

    ```sql
    -- Connect as a server admin, then:
    USE [test_database_name];

    -- Grant permissions for managed identity (replace with your identity name)
    CREATE USER [your-app-identity] FROM EXTERNAL PROVIDER;
    GRANT CREATE TABLE TO [your-app-identity];
    GRANT ALTER ON SCHEMA::dbo TO [your-app-identity];
    ```

- **Alternative**: Use SQL authentication for test environments, or switch to `ActiveDirectoryPassword` for CI/CD test runners.

## Rollback procedures

When a migration fails partway through, use this rollback sequence to return to a known good state:

1. Stop application writes to avoid additional schema drift.

1. Inspect migration state:

    ```bash
    python manage.py showmigrations
    python manage.py sqlmigrate <app_label> <migration_number>
    ```

1. Roll back to the last known good migration:

    ```bash
    python manage.py migrate <app_label> <previous_migration>
    ```

1. If schema and migration history diverge, repair state carefully with `--fake` only after verifying the actual database schema.

1. Re-run migrations in a staging environment first, then retry production.

> [!IMPORTANT]
> For destructive migrations such as drop, rename, and column type changes, take a tested backup before deployment. If rollback by migration isn't possible, restore from backup and reapply validated migrations.

## Docker and container issues

Container images require explicit ODBC driver installation and build dependencies when you use the default pyodbc path. The mssql-python path has no separate ODBC driver install, but it still needs the unixODBC runtime, because the backend imports pyodbc when Django loads it.

### ODBC driver not found in container

**Symptoms**:

```output
Error: ('01000', "[01000] [unixODBC][Driver Manager]Can't open lib 'ODBC Driver 18 for SQL Server'")
```

**Possible causes and solutions**:

- **ODBC driver not installed in the container image**

  Slim or Alpine base images don't include the ODBC driver. Add the Microsoft APT repository and install `msodbcsql18` in your Dockerfile when you use pyodbc. See [Deploy to App Service](deploy-azure-app-service.md#deploy-to-app-service) for a complete Dockerfile example.

- **Missing `unixodbc-dev` package**

  The `pyodbc` wheel links against `libodbc.so`. Install `unixodbc-dev` (Debian/Ubuntu) or `unixODBC-devel` (RHEL/Fedora) before installing Python packages.

- **`apt-get autoremove` stripped `libgssapi-krb5-2` after the driver install**

  `msodbcsql18` loads `libgssapi-krb5-2` at run time without declaring it as a dependency. The library usually arrives as a dependency of `curl`, so purging `curl` with `--auto-remove`, or running `apt-get autoremove` afterward, removes it. The image builds clean and every connection then fails. Install `libgssapi-krb5-2` explicitly, and don't autoremove after the driver install.

### Driver 17 reported missing when you installed version 18

**Symptoms**:

```output
Error: ('01000', "[01000] [unixODBC][Driver Manager]Can't open lib 'ODBC Driver 17 for SQL Server' : file not found (0) (SQLDriverConnect)")
```

The error names version 17, but `odbcinst -q -d` shows version 18 registered and `dpkg -l msodbcsql18` shows it installed.

**Cause**: Version 18 is registered but fails to load, so mssql-django falls back to version 17, which isn't installed. The fallback reports the driver it tried second, not the one that failed.

**Solution**: Install `libgssapi-krb5-2` and rebuild. See the preceding autoremove note for how the library goes missing.

### Error loading pyodbc module in a container

**Symptoms**:

```output
django.core.exceptions.ImproperlyConfigured: Error loading pyodbc module: libodbc.so.2: cannot open shared object file: No such file or directory
```

**Cause**: The image has no unixODBC runtime. mssql-django imports pyodbc when Django loads the backend, so this error happens on the mssql-python path too, before any connection is attempted.

**Solution**: Install `unixodbc` (or `unixodbc-dev`).

### mssql-python driver fails to load

**Symptoms**:

```output
django.db.utils.OperationalError: Driver Error: Connection operation failed; DDBC Error: Failed to load the driver.
```

**Cause**: The driver that ships with `mssql-python` needs the Kerberos runtime libraries, which slim base images don't include.

**Solution**: Install `libkrb5-3` and `libgssapi-krb5-2`.

### pyodbc fails to build on slim images

**Symptoms**:

```output
error: command 'gcc' failed: No such file or directory
```

Or:

```output
fatal error: sql.h: No such file or directory
```

**Solution**: Install build dependencies before `pip install`:

```dockerfile
RUN apt-get update && apt-get install -y --no-install-recommends \
    gcc \
    g++ \
    unixodbc-dev
```

Alternatively, use a multi-stage build to keep the final image small:

```dockerfile
# Build stage
FROM python:3.12-slim AS builder
RUN apt-get update && apt-get install -y --no-install-recommends gcc g++ unixodbc-dev
COPY requirements.txt .
RUN pip wheel --no-cache-dir --wheel-dir /wheels -r requirements.txt

# Runtime stage
FROM python:3.12-slim
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl gnupg2 unixodbc \
    && curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg \
    && curl -fsSL https://packages.microsoft.com/config/debian/12/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y --no-install-recommends msodbcsql18 libgssapi-krb5-2 \
    && apt-get purge -y curl gnupg2 \
    && rm -rf /var/lib/apt/lists/*
COPY --from=builder /wheels /wheels
RUN pip install --no-cache-dir /wheels/*
```

### Container can't connect to SQL Server

**Symptoms**:

```output
django.db.utils.OperationalError: ('08001', '... TCP Provider: Error code 0x2749 ...')
```

**Possible causes and solutions**:

- **Docker Compose service name not used as host**

  When using Docker Compose, set `DB_HOST` to the service name (for example, `db`), not `localhost` or `127.0.0.1`.

- **SQL Server container not ready**

  The SQL Server container takes several seconds to start. Add a health check or startup delay:

  ```yaml
  services:
    db:
      image: mcr.microsoft.com/mssql/server:2022-latest
      healthcheck:
        test: /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$$MSSQL_SA_PASSWORD" -No -Q "SELECT 1" || exit 1
        # $$ escapes the $ sign in Docker Compose YAML
        interval: 10s
        retries: 10
        start_period: 10s
    web:
      depends_on:
        db:
          condition: service_healthy
  ```

- **Port mapping conflicts**

  If another instance of SQL Server is running on the host, change the exposed port (for example, `1434:1433`) and update your Django configuration accordingly.

## Azure SQL transient error recovery

The `mssql-django` backend automatically detects Azure SQL Database and Azure SQL Managed Instance connections by querying `SERVERPROPERTY('EngineEdition')`. When running against Azure SQL, the backend retries connections on transient errors (such as temporary resource limits or brief network interruptions).

You can tune this behavior with the `connection_retries` and `connection_retry_backoff_time` OPTIONS:

```python
"OPTIONS": {
    "driver": "ODBC Driver 18 for SQL Server",
    "connection_retries": 5,
    "connection_retry_backoff_time": 5,
},
```

These settings apply to initial connection establishment only. The backend doesn't retry failed queries. If a query fails with a transient error after the connection is established, the exception propagates to your application code. Use application-level retry logic (for example, [django-retry-db](https://pypi.org/project/django-retry-db/) or a custom middleware) for query-level resilience.

## Slow queries and plan regressions

These problems usually need server-side analysis along with Django-level query review.

### Query gets slower or starts timing out

**Symptoms**:

The same queryset becomes slower over time, or starts timing out after a deployment, index change, or statistics update.

**Possible causes and solutions**:

- **Start with built-in performance reports**

  For SQL Server and Azure SQL Managed Instance, open [Performance Dashboard](../../../relational-databases/performance/performance-dashboard.md) in SQL Server Management Studio. For Azure SQL Database, open [Query Performance Insight for Azure SQL Database](/azure/azure-sql/database/query-performance-insight-use). These tools are usually a better first step than ad hoc DMV queries because they quickly surface expensive queries, waits, and resource pressure.

- **Plan regression**

  Use [Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) to find the slow query and check whether it has multiple plans. Start with the **Regressed Queries** and **Top Resource Consuming Queries** views described in [Best practices for monitoring workloads with Query Store](../../../relational-databases/performance/best-practice-with-the-query-store.md).

- **Inefficient execution plan**

  Open an [actual execution plan](../../../relational-databases/performance/analyze-an-actual-execution-plan.md) for the statement and check for table or index scans, large key lookups, hash spills, or inaccurate row estimates. For background, see [Execution plan overview](../../../relational-databases/performance/execution-plans.md).

- **Wrong bottleneck identified**

  If the query isn't CPU-bound, use Query Store wait statistics and [Identify bottlenecks](../../../relational-databases/performance/identify-bottlenecks.md) to distinguish CPU, memory, disk I/O, blocking, and connection pressure.

- **Fix applied in the wrong layer**

  Apply the smallest effective fix: add or adjust indexes, update statistics, reduce selected columns and rows, or batch large writes. If you need an emergency mitigation, a DBA can temporarily force a known good plan in Query Store while you correct the root cause.

## Use dbshell for interactive queries

Django's `dbshell` management command opens an interactive SQL shell connected to your database:

```bash
python manage.py dbshell
```

The backend uses **`sqlcmd`** when you configure the Microsoft ODBC driver, or `isql` when you use FreeTDS. Verify the tool is on your PATH:

- **Windows**: **`sqlcmd`** is included with SQL Server tools, or you can [download it separately](../../../tools/sqlcmd/sqlcmd-download-install.md).
- **Linux and macOS**: Install `mssql-tools18` from the Microsoft repository.

## Related content

- [mssql-django configuration reference](configuration-reference.md)
- [Connection options for mssql-django](connection-options.md)
- [Retry logic and connection resilience with mssql-django](retry-logic.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Performance Dashboard](../../../relational-databases/performance/performance-dashboard.md)
- [Query Performance Insight for Azure SQL Database](/azure/azure-sql/database/query-performance-insight-use)
- [Monitor performance by using the Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Analyze an actual execution plan](../../../relational-databases/performance/analyze-an-actual-execution-plan.md)
- [Troubleshooting wiki](https://github.com/microsoft/mssql-django/wiki/Troubleshooting)
- [FAQ](faq.yml)
