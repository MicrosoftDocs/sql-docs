---
title: What's New in mssql-python Driver
description: Learn about new features and changes in each version of the mssql-python driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: whats-new
ai-usage: ai-assisted
---

# What's new in mssql-python

Each release of the mssql-python driver introduces new features, performance improvements, and bug fixes. The following sections detail every version.

## mssql-python 1.12.0

**Release date**: July 2026

### Enhancements

#### Standalone `mssql-python-odbc` companion package

The ODBC driver binaries that `mssql-python` needs at runtime are now also published as a separate, data-only companion package: [mssql-python-odbc](https://pypi.org/project/mssql-python-odbc/) (import name `mssql_python_odbc`, currently pinned to version `18.6.2`). The `mssql-python` package declares `mssql-python-odbc==18.6.2` in `install_requires`, so `pip install mssql-python` transparently installs the companion package alongside it. No code changes are required.

The native loader prefers the external `mssql_python_odbc` package when it's present, and falls back to the ODBC binaries still bundled inside the `mssql-python` wheel when it isn't. The fallback is safe with the Python Global Interpreter Lock (GIL) and works on musl-based Linux distributions such as Alpine.

This split lets you pin or update the driver binaries independently of the Python code, gives redistributors a smaller `mssql-python` wheel over time, and avoids duplicate-ownership issues from bundled ODBC files.

> [!IMPORTANT]
> This split ships without a breaking change. In a future major release (v2.0.0), the bundled `libs/` tree might be removed, in which case `mssql-python-odbc` becomes a hard runtime requirement.

### Bug fixes

#### `cursor.bulkcopy()` now uses the parent connection's connect timeout

The bulk copy operation opens a separate connection through the `mssql_py_core` native extension. Previously, this operation always used a hardcoded 15-second connect timeout with no way to override it from Python. If you set a connect timeout on the parent connection (`connect(..., timeout=<seconds>)`), `bulkcopy()` now forwards that value into the internal connection. Setting `timeout=0` preserves the *no override* behavior and keeps the internal 15-second default. The cursor's timeout at the time of the `bulkcopy()` call is used for the operation, so later changes to the parent connection don't affect an in-flight bulk copy.

The following example uses the `Production.Culture` lookup table from the [AdventureWorks](/sql/samples/adventureworks-install-configure) sample database. Adjust the connection string and database name for your environment:

```python
import mssql_python
from datetime import datetime

# The 60-second timeout applies to both the initial connection and
# the internal connection that bulkcopy() opens.
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=AdventureWorks2022;"
    "Encrypt=yes",
    timeout=60,
)
conn.autocommit = True
cursor = conn.cursor()

# Bulk-copy two rows into Production.Culture (CultureID, Name, ModifiedDate).
now = datetime.now()
rows = [
    ("xx", "Demo culture 1", now),
    ("yy", "Demo culture 2", now),
]
result = cursor.bulkcopy("Production.Culture", rows)
print(f"Copied {result['rows_copied']} rows")

# Remove the demo rows so the sample is re-runnable.
cursor.execute("DELETE FROM Production.Culture WHERE CultureID IN ('xx','yy')")
```

#### `cursor.bulkcopy()` supports CLR user-defined type columns

Previously, `cursor.bulkcopy()` failed with `Protocol Error: Unsupported TDS type for bulk copy: 0xF0` for any destination column that used a common language runtime (CLR) user-defined type, including the built-in **geography**, **geometry**, and **hierarchyid** types and any custom, assembly-registered CLR UDT. The native `mssql_py_core` wire path had no handler for the UDT type token (`0xF0`) and errored while writing column metadata, before any rows were sent. CLR UDT columns are now mapped to **varbinary(max)** on the wire, and the supplied bytes are streamed as the UDT's `IBinarySerialize` payload, matching how `pyodbc` and `python-tds` load UDT columns. SQL Server materializes the UDT on insert. Delivered via the `mssql_py_core` 0.1.6 to 0.1.7 bump.

The following example archives the org-chart column from `HumanResources.Employee` (a **hierarchyid** column, one of SQL Server's built-in CLR UDTs) into a new table. In a real workflow, the UDT bytes might come from another SQL Server instance, a serialized file, or your CLR type's `IBinarySerialize.Write()` output; this example reads them from an existing column via `CAST(... AS varbinary(max))` so the sample is self-contained. The bulk copy includes the row whose `OrganizationNode` is NULL:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>;"
    "Database=AdventureWorks2022;"
    "Encrypt=yes",
)
conn.autocommit = True  # bulkcopy uses a separate connection; the destination must be visible
cursor = conn.cursor()

cursor.execute(
    "IF OBJECT_ID('dbo.EmployeeOrgArchive','U') IS NOT NULL "
    "DROP TABLE dbo.EmployeeOrgArchive;"
    "CREATE TABLE dbo.EmployeeOrgArchive (BusinessEntityID int, OrganizationNode hierarchyid);"
)

# Casting a hierarchyid column to varbinary(max) yields the UDT's
# serialized IBinarySerialize payload.
cursor.execute(
    "SELECT BusinessEntityID, CAST(OrganizationNode AS varbinary(max)) "
    "FROM HumanResources.Employee;"
)
rows = cursor.fetchall()

# Stream the (id, bytes) tuples into the destination's hierarchyid column.
result = cursor.bulkcopy("dbo.EmployeeOrgArchive", rows)
print(f"Copied {result['rows_copied']} rows")

cursor.execute("DROP TABLE dbo.EmployeeOrgArchive")
```

For a custom, assembly-registered CLR UDT, use the type in the destination table and supply the bytes produced by the type's `IBinarySerialize.Write()` method.

## mssql-python 1.11.0

**Release date**: July 2026

### Enhancements

#### Improved context manager semantics

`with connection:` now properly commits transactions on clean exit and rolls back on exception, making it more Pythonic and predictable.

```python
import mssql_python

# On clean exit, transaction commits
with mssql_python.connect(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("INSERT INTO MyTable (Name) VALUES ('Alice')")
    # Automatically committed on exit

# On exception, transaction rolls back
try:
    with mssql_python.connect(connection_string) as conn:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO MyTable (Name) VALUES ('Bob')")
        raise ValueError("Oops!")
except ValueError:
    pass
# Changes rolled back on exit
```

### Bug fixes

- Fixed a GIL-deadlock in the ODBC teardown path (`conn.close()` and `cursor.close()`) and in `SQLDescribeParam` for `None`-valued parameters in SSH-tunnel and in-process forwarder setups.
- Fixed `BINARY` and `VARBINARY` NULL parameters in temp tables and table variables. When automatic type resolution fails, the driver now emits a Python warning with explicit `cursor.setinputsizes()` guidance.
- Fixed `import mssql_python` failing on Apple Silicon with a clean install (regression in 1.8.0). The bundled ODBC dylib dependencies are now rewritten for both `arm64` and `x86_64` architectures.
- Fixed a GIL-deadlock in the Rust core that froze bulk copy operations when authenticating with `Authentication=ActiveDirectoryServicePrincipal`.

## mssql-python 1.10.0

**Release date**: June 2026

### Enhancements

#### ActiveDirectoryServicePrincipal support for bulk copy

`cursor.bulkcopy()` now supports `Authentication=ActiveDirectoryServicePrincipal`, allowing bulk inserts by using service principal credentials.

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryServicePrincipal;"
    "UID=<application-client-id>;"
    "PWD=<client-secret>;"
    "Encrypt=yes"
)
cursor = conn.cursor()
cursor.execute("CREATE TABLE ##SpDemo (ID INT, Value FLOAT)")
conn.commit()

result = cursor.bulkcopy("##SpDemo", [(1, 1.5), (2, 2.5)])
print(f"Copied {result['rows_copied']} rows")
```

### Bug fixes

- Fixed non-ASCII `VARCHAR` and `CHAR` data in the Arrow fetch path.
- Fixed connection timeouts during bulk load operations.

## mssql-python 1.9.0

**Release date**: June 2026

### Enhancements

#### Row objects in bulk copy

`cursor.bulkcopy()` now accepts fetched `Row` objects directly instead of requiring manual tuple conversion.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Fetch rows from source table
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")
rows = cursor.fetchall()

# Pass fetched Row objects directly to bulkcopy
cursor.execute("CREATE TABLE ##RowBulkDemo (ProductID INT, Name NVARCHAR(50), ListPrice MONEY)")
conn.commit()
result = cursor.bulkcopy("##RowBulkDemo", rows)
print(f"Copied {result['rows_copied']} rows")
```

### Bug fixes

- Fixed wheel packaging so `simdutf` is always statically linked.
- Fixed large `DECIMAL` inserts in `executemany()`.
- Fixed incorrect type fallback for NULL parameters.
- Fixed exception pickle and unpickle round-trips.
- Fixed `nextset()` so it preserves `PRINT` messages across result sets.
- Fixed `Row` handling in the `executemany()` data-at-execution fallback path.
- Fixed fetch method type checking for static analysis tools.

## mssql-python 1.8.0

**Release date**: May 2026

### Enhancements

#### ActiveDirectoryMSI support for bulk copy

`cursor.bulkcopy()` now supports `Authentication=ActiveDirectoryMSI` for system-assigned and user-assigned managed identities.

```python
import mssql_python

# System-assigned managed identity
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryMSI;"
    "Encrypt=yes"
)
cursor = conn.cursor()
cursor.execute("CREATE TABLE ##MsiDemo (ID INT, Name NVARCHAR(50))")
conn.commit()

result = cursor.bulkcopy("##MsiDemo", [(1, "Alice"), (2, "Bob")])
print(f"Copied {result['rows_copied']} rows")
```

#### Row string-key indexing

You can now access row values by column name, for example `row["col"]`, in addition to positional indexing and attribute access.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

# Access by column name (new in 1.8.0)
print(row["ProductID"]) # Access by key
print(row["Name"])

# Still supports positional indexing
print(row[0])           # Positional access

# And attribute access
print(row.Name)         # Attribute access
```

#### Bundled ODBC driver upgrade

The bundled Microsoft ODBC Driver for SQL Server was updated to 18.6.2.1.

### Bug fixes

- Fixed deferred connect-attribute lifetime problems in token-based authentication.
- Fixed repeated connection string parsing in the authentication path.
- Fixed `executemany()` type annotations for sequence inputs.

## mssql-python 1.7.1

**Release date**: May 2026

### Enhancements

#### Expanded wheel coverage and performance improvements

This release adds RHEL 8 compatible wheels, restores macOS Python 3.10 `universal2` wheels, improves UTF-16 handling through `simdutf`, and optimizes the `execute()` hot path.

**Performance impact**: Batch execution throughput improves by ~15% on typical workloads due to hot path optimizations in the `execute()` method.

### Bug fixes

- Fixed login failures so they raise `mssql_python` DB-API exceptions instead of `RuntimeError`.
- Extended GIL release across blocking ODBC execution, fetch, transaction, and connection attribute calls.
- Fixed `executemany()` failures when decimal values change sign.
- Fixed inconsistent CP1252 `VARCHAR` decoding across platforms.
- Fixed `cursor.bulkcopy()` failures for empty strings in `NVARCHAR(MAX)` and `VARCHAR(MAX)` columns.

> [!NOTE]
> Version 1.7.0 was withdrawn due to publication issues. Use version 1.7.1 or later.

## mssql-python 1.6.0

**Release date**: April 2026

### Enhancements

#### Parser-based connection string sanitization 

This enhancement ensures correct parsing of special characters in password fields and braced values.

```python
import mssql_python

# Complex passwords with special characters now parse correctly
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "UID=user@contoso;"
    "PWD={p@ssw0rd;with{braces}};"  # Braced values now handled correctly
    "Encrypt=yes"
)
```

Connection string sanitization moved from regex-based logic to parser-based processing for correct handling of ODBC connection string syntax.

### Bug fixes

- Fixed GIL release during blocking ODBC connect and disconnect operations.
- Fixed `setinputsizes()` crashes with `SQL_DECIMAL` and `SQL_NUMERIC` hints.
- Fixed incorrect `fetchone()` behavior for ODBC catalog methods.
- Fixed invalid cursor state errors when `reset_cursor=False` is used.
- Fixed `executemany()` type hints for mapping-based parameter sequences.
- Added a path traversal guard for `setup_logging(log_file_path=...)`.

## mssql-python 1.5.0

**Release date**: April 2026

### New features

#### Apache Arrow fetch support

Three new cursor methods provide high-performance columnar data retrieval through the Arrow C Data Interface:

- `cursor.arrow()` returns a complete `pyarrow.Table`.
- `cursor.arrow_batch()` returns a single `pyarrow.RecordBatch`.
- `cursor.arrow_reader()` returns a `pyarrow.RecordBatchReader` for streaming.

The implementation bypasses Python object creation in the hot path for improved performance. For complete documentation, see [Apache Arrow integration](arrow-integration.md).

#### sql_variant type support

The driver now detects `sql_variant` columns at fetch time, resolves their underlying base type, and returns correctly typed Python values instead of raw bytes.

> [!NOTE]
> `sql_variant` columns use a streaming fetch path, which might have a slight performance impact compared to fixed-type columns.

#### Native UUID support

A new `native_uuid` setting controls whether `UNIQUEIDENTIFIER` columns are returned as `uuid.UUID` objects (default) or as pyodbc-compatible uppercase strings. Configure it at the module level or per connection:

```python
# Module-level default
settings = mssql_python.get_settings()
settings.native_uuid = True  # default

# Per-connection override
conn = mssql_python.connect(connection_string, native_uuid=False)
```

For more information, see [Module configuration](module-configuration.md#native_uuid).

#### Row class public export

The `Row` class is now exported at the top level for type annotations:

```python
from mssql_python import Row
```

### Bug fixes

- Fixed false positive `?` detection inside bracketed identifiers, string literals, and comments.
- Fixed NULL parameter binding for `VARBINARY` columns (no longer raises implicit conversion errors).
- Fixed `datetime.time` values losing microseconds on round-trips for `TIME(1)` through `TIME(7)` columns.
- Fixed Arrow fetch path to correctly include fractional seconds for `TIME` columns.
- Fixed bulk copy with Microsoft Entra ID authentication methods (stale credential fields no longer cause validation errors).
- Cached Azure Identity credential instances at module level for improved authentication performance.

## mssql-python 1.4.0

**Release date**: March 2025

### New features

#### Bulk copy support

High-performance bulk data loading is now available through `cursor.bulkcopy()`:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("CREATE TABLE ##BulkDemo (ID INT, Name NVARCHAR(50), Price DECIMAL(10,2))")
conn.commit()

data = [
    (1, "Item 1", 10.50),
    (2, "Item 2", 20.75),
    # ... potentially millions of rows
]

result = cursor.bulkcopy("##BulkDemo", data)
print(f"Copied {result['rows_copied']} rows")
```

The method accepts options for `batch_size`, `timeout`, `column_mappings`, `keep_identity`, `check_constraints`, `table_lock`, `keep_nulls`, `fire_triggers`, and `use_internal_transaction`.

See [Bulk copy](bulk-copy.md) for complete documentation.

### Improvements

- Performance optimizations for large result sets.
- Reduced memory usage during batch operations.
- Enhanced error messages for bulk copy failures.

<!-- markdownlint-disable MD024 -->

## mssql-python 1.3.0

**Release date**: January 2025

### New features

#### Settings class

Configure module-wide behavior through the new `Settings` class:

```python
import mssql_python

settings = mssql_python.get_settings()
settings.lowercase = True       # Lowercase column names in cursor.description
```

See [Module configuration](module-configuration.md) for details.

### Improvements

- Better handling of connection timeout during Azure SQL failover.
- Improved compatibility with Python 3.13.

## mssql-python 1.2.0

**Release date**: November 2024

### New features

#### Schema discovery methods

New cursor methods for database metadata exploration:

```python
cursor = conn.cursor()

# List all tables
cursor.tables(schema="dbo")

# Get column information
cursor.columns(table="Product", schema="Production")

# Get primary keys
cursor.primaryKeys(table="Product", schema="Production")

# Get foreign key relationships
cursor.foreignKeys(table="SalesOrderDetail", schema="Sales")

# Get stored procedures
cursor.procedures(schema="dbo")

# Get index statistics
cursor.statistics(table="Product", schema="Production")

# Get type information
cursor.getTypeInfo()
```

See [Schema discovery](schema-discovery.md) for complete documentation.

### Improvements

- Enhanced metadata caching for repeated schema queries.
- Better handling of computed columns in `columns()` results.

## mssql-python 1.1.0

**Release date**: September 2024

### New features

#### Custom output converters

Register custom functions to transform column values during fetch:

```python
import mssql_python
from decimal import Decimal

conn = mssql_python.connect(connection_string)

# Convert decimals to float (converter receives Decimal)
def decimal_to_float(value):
    if value is None:
        return None
    return float(value)  # value is already a Decimal object

conn.add_output_converter(mssql_python.SQL_DECIMAL, decimal_to_float)

# Custom money formatting
def format_money(value):
    if value is None:
        return "$0.00"
    return f"${float(value):,.2f}"  # value is already a Decimal object

conn.add_output_converter(mssql_python.SQL_DECIMAL, format_money)
```

Management methods:

- `add_output_converter(sql_type, converter_func)`
- `get_output_converter(sql_type)`
- `remove_output_converter(sql_type)`
- `clear_output_converters()`

For complete documentation, see [Custom type converters](custom-type-converters.md).

### Improvements

- Better error messages for type conversion failures.
- Support for converter functions that return `None`.

## mssql-python 1.0.0

**Release date**: July 2024

### Initial GA release

The first general availability release of mssql-python, Microsoft's native Python driver for SQL Server.

#### Core features

- **DDBC architecture**: Direct Database Connectivity without requiring ODBC driver installation.
- **DB-API 2.0 compliance**: Standard Python database interface.
- **Connection pooling**: Built-in connection pool management.
- **Microsoft Entra authentication**: Full support for Azure identity-based authentication.
- **TLS encryption**: Secure connections with certificate validation.

#### Connection features

- 21 connection string keywords.
- 9 authentication modes (SQL, Windows, and 7 Microsoft Entra ID methods).
- Autocommit control.
- Execution methods: `execute()`, `executemany()`, and `batch_execute()`.
- Connection attributes through `set_attr()` and `getinfo()`.
- Context manager support.

#### Cursor features

- Standard fetch methods: `fetchone()`, `fetchmany()`, `fetchall()`.
- Extended methods: `fetchval()`, `skip()`.
- Execution methods: `execute()` and `executemany()`.
- Row objects with attribute and index access.
- Multiple result set navigation with `nextset()`.

#### Data type support

- All SQL Server native types.
- Python↔SQL type mappings.
- SQL type constants for explicit typing (for example, `mssql_python.SQL_DECIMAL`).
- NULL handling as Python `None`.

#### Transaction support

- Manual commit and rollback.
- Autocommit mode.
- Isolation level control.
- Deadlock detection and handling.

#### Authentication modes

| Mode | Description |
| ------ | ------------- |
| SQL Server authentication | Username and password |
| Windows authentication | `Trusted_Connection` |
| ActiveDirectoryDefault | `DefaultAzureCredential` |
| ActiveDirectoryInteractive | Browser-based sign-in |
| ActiveDirectoryDeviceCode | Device code flow |
| ActiveDirectoryPassword | Microsoft Entra username and password (deprecated; [uses ROPC](/entra/identity-platform/v2-oauth-ropc)) |
| ActiveDirectoryMSI | Managed identity |
| ActiveDirectoryServicePrincipal | Service principal |
| ActiveDirectoryIntegrated | Windows Kerberos |

## Upgrade

### From pyodbc

For detailed migration guidance, see [Migrate from pyodbc](migrate-from-pyodbc.md).  

Key differences:

- Both `?` (qmark) and `%(name)s` (pyformat) parameter styles are supported. Your existing `?` queries work without changes.
- No `callproc()` method. Use `EXECUTE` statements instead.
- Built-in connection pooling.
- No external ODBC driver dependency.

### From pymssql

For detailed migration guidance, see [Migrate from pymssql](migrate-from-pymssql.md).  

Key differences:

- Replace `%s` and `%d` parameter markers with `?` or `%(name)s`.
- Use a connection string instead of positional arguments.
- No FreeTDS dependency.
- Multiple concurrent cursors per connection.
- Row objects with attribute access replace `as_dict=True`.

### Between mssql-python versions

Upgrade the driver to get new features and fixes.  

```bash
pip install --upgrade mssql-python
```

Check the release notes for any breaking changes before upgrading production systems.

## Roadmap

For upcoming features and the development roadmap, see the [GitHub repository](https://github.com/microsoft/mssql-python).

## Related content

- [Microsoft Python Driver for SQL Server - mssql-python](python-sql-driver-mssql-python.md)
- [Support lifecycle for mssql-python](support-lifecycle.md)
- [Install mssql-python](installation.md)
- [GitHub releases](https://github.com/microsoft/mssql-python/releases)
