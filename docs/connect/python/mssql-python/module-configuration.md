---
title: Configure mssql-python Module Settings
description: Learn how to configure global module settings in the mssql-python driver, including lowercase queries, decimal separator, and custom settings.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto
ms.date: 10/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Configure mssql-python module settings

The mssql-python driver provides a `Settings` class that controls module-wide behavior. These settings affect all connections and cursor operations. Configure them once at application startup, before you create any connections.

## Access settings

Retrieve the current `Settings` object and inspect or modify its properties:

```python
import mssql_python

# Get settings object
settings = mssql_python.get_settings()

# Check current values
print(settings.lowercase)
print(settings.decimal_separator)
```

## Available settings

The following settings control how the driver returns data and formats results.

### lowercase

The `lowercase` setting controls whether column names in `cursor.description` appear in lowercase. Enable this setting when your application accesses columns by name and you want to avoid casing mismatches. Web frameworks such as Flask and FastAPI often convert rows to dictionaries, which makes consistent casing important:

```python
settings = mssql_python.get_settings()

# Enable lowercase column names (default: False)
settings.lowercase = True

# Column names in cursor.description are now lowercased:
# ('productid', ...) instead of ('ProductID', ...)
```

| Value | Description |
| --- | --- |
| `False` | Default. Column names preserve the original casing. |
| `True` | Column names in `cursor.description` are converted to lowercase. |

### Decimal separator

The driver provides module-level functions to control the decimal separator for numeric conversions. Change this setting only if your SQL Server instance uses a locale with a comma as the decimal separator, such as French or German locales. Most applications don't need to change this setting:

```python
import mssql_python

# Get current separator
sep = mssql_python.getDecimalSeparator()
print(f"Current separator: {sep}")  # Usually "."

# Set custom separator (for locales using comma)
mssql_python.setDecimalSeparator(",")
```

For more information about decimal handling, see [Data type mappings](data-type-mappings.md#decimal-separator).

### native_uuid

The `native_uuid` setting controls whether `UNIQUEIDENTIFIER` columns are returned as Python `uuid.UUID` objects or as pyodbc-compatible uppercase strings. This setting is useful for teams migrating from pyodbc that depend on string UUID values:

```python
settings = mssql_python.get_settings()

# Return UUIDs as uuid.UUID objects (default: True)
settings.native_uuid = True

# Return UUIDs as uppercase strings (pyodbc-compatible)
settings.native_uuid = False
```

| Value | Description |
| --- | --- |
| `True` | Default. `UNIQUEIDENTIFIER` columns return `uuid.UUID` objects. |
| `False` | `UNIQUEIDENTIFIER` columns return uppercase strings (pyodbc-compatible). |

You can also set `native_uuid` per connection:

```python
# Override for a specific connection
conn = mssql_python.connect(connection_string, native_uuid=False)
```

> [!NOTE]
> The `native_uuid` setting was introduced in mssql-python version 1.5.0.

## Select the native ODBC provider

The driver loads one native ODBC provider per process. Two providers are available:

| Provider | Description | Ships in |
| --- | --- | --- |
| `msodbcsql18` | Default. Microsoft ODBC Driver 18 for SQL Server. | `mssql-python-odbc` |
| `mssql-odbc` | Opt-in. A Rust-based ODBC provider. | `mssql-python-rs` |

Both packages install with `pip install mssql-python`, so you don't need to install anything else to try the `mssql-odbc` provider. Your application code doesn't change.

Select the provider with the `MSSQL_PYTHON_NATIVE_PROVIDER` environment variable. On Linux or macOS:

```bash
export MSSQL_PYTHON_NATIVE_PROVIDER=mssql-odbc
```

On Windows PowerShell:

```powershell
$env:MSSQL_PYTHON_NATIVE_PROVIDER = "mssql-odbc"
```

Or set the `native_provider` module attribute before you open the first connection:

```python
import mssql_python

mssql_python.native_provider = "mssql-odbc"

conn = mssql_python.connect(connection_string)
print(mssql_python.get_native_provider_info())
```

The `get_native_provider_info()` function returns the selected provider and where the selection came from:

```output
{'id': 'mssql-odbc', 'package': 'mssql_py_core', 'version': None, 'driver_path': '...', 'source': 'property', 'frozen': True}
```

The driver applies these rules when it resolves the provider:

- The environment variable takes precedence over the `native_provider` attribute. If you set the attribute to a different provider than the environment variable, the driver emits `RuntimeWarning` and uses the environment variable.

- The selection is fixed when the driver loads the provider, which happens after the connection string is parsed and before the driver contacts the server. The selection stays fixed even if the connection then fails, for example because the server is unreachable. An error raised before the provider loads, such as a connection string parsing error or a missing provider package, doesn't fix the selection. After the selection is fixed, a change to `native_provider` emits `RuntimeWarning` and has no effect for the rest of the process.

- An unrecognized provider name raises `ValueError`. Setting the attribute to an unknown name raises immediately. An unknown name in the environment variable raises when you open the first connection. The driver doesn't fall back to the default provider.

- Set `native_provider` to `None` to clear a selection before the first connection.

## Module-level constants

The driver exposes read-only DB-API 2.0 compliance constants that describe its capabilities. Use these constants to write code that adapts to different DB-API drivers:

```python
import mssql_python

# DB-API 2.0 compliance level
print(mssql_python.apilevel)      # '2.0'

# Thread safety level
print(mssql_python.threadsafety)  # 1

# Parameter style
print(mssql_python.paramstyle)    # 'pyformat'
```

### apilevel

The `apilevel` constant reports the DB-API compliance level:

| Value | Meaning |
| --- | --- |
| `'2.0'` | Full DB-API 2.0 compliance. |

### threadsafety

The `threadsafety` constant reports the thread safety level:

| Value | Meaning |
| --- | --- |
| `0` | Threads can't share the module. |
| `1` | Threads can share the module but not connections. |
| `2` | Threads can share the module and connections. |
| `3` | Threads can share the module, connections, and cursors. |

The mssql-python driver uses `threadsafety = 1`, which means:

- You can import and use the module across threads.
- Each connection must belong to only one thread at a time.
- Create a separate connection per thread, or use a connection pool (enabled by default). For more information, see [Connection pooling](connection-pooling.md).

### paramstyle

The `paramstyle` constant reports the parameter placeholder format:

| Style | Format | Example |
| --- | --- | --- |
| `'qmark'` | Question marks | `WHERE id = ?` |
| `'numeric'` | Numeric position | `WHERE id = :1` |
| `'named'` | Named | `WHERE id = :id` |
| `'format'` | ANSI C printf | `WHERE id = %s` |
| `'pyformat'` | Python format | `WHERE id = %(id)s` |

The mssql-python driver uses `paramstyle = 'pyformat'`. Always use named parameters to prevent SQL injection. Never build queries with user input through string formatting or f-strings:

```python
# Use named parameters with %(name)s syntax
cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s AND ListPrice > %(price)s",
    {"cat": 5, "price": 10.00}
)
```

## Version information

Check which version of the driver is installed:

```python
import mssql_python

# Driver version
print(mssql_python.__version__)  # For example, '1.16.0'
```

## Configure settings at startup

Set module configuration once at application startup, before you create any connections. Setting values early prevents inconsistent behavior across connections:

```python
import mssql_python

def configure_driver():
    """Configure mssql-python settings for this application."""
    settings = mssql_python.get_settings()
    
    # Use lowercase column names in cursor.description
    settings.lowercase = True

# Call at application startup
configure_driver()

# All subsequent connections use these settings
conn = mssql_python.connect(connection_string)
```

## Thread safety considerations

Module settings are global and affect all connections across all threads. If you change a setting after connections are already open, existing connections might not reflect the change consistently. Set all configuration values before you create your first connection:

```python
import mssql_python
import threading

# Settings changes affect all threads
settings = mssql_python.get_settings()
settings.lowercase = True  # Affects all connections in all threads

def worker():
    # This connection uses the global settings
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("SELECT Name FROM Production.Product")
    row = cursor.fetchone()
    print(cursor.description[0][0])  # 'name' due to global setting

threads = [threading.Thread(target=worker) for _ in range(5)]
for t in threads:
    t.start()
for t in threads:
    t.join()
```

> [!IMPORTANT]
> Configure settings before creating connections. Changing settings after connections are created can lead to inconsistent behavior.

## Connection-specific configuration

You can override some settings per connection without changing the global default. Use per-connection overrides when different parts of your application need different behavior. For example, a reporting module might need string UUIDs while the rest of the application uses `uuid.UUID` objects:

```python
# Per-connection native_uuid override
conn = mssql_python.connect(connection_string, native_uuid=False)

# Use the autocommit property
conn.autocommit = True
```

## Related content

- [Connection strings for mssql-python](connection-strings.md)
- [Manage connections with mssql-python](connection-management.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
