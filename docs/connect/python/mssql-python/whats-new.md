---
title: What's New in mssql-python Driver
description: Learn about new features and changes in each version of the mssql-python driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: whats-new
ai-usage: ai-assisted
---

# What's new in mssql-python

This article lists what changed in each release of the mssql-python driver, newest first. Each section covers new features, behavior changes, and bug fixes for one version.

For the versions that Microsoft currently supports, see [Support lifecycle](support-lifecycle.md).

## mssql-python 1.14.0

**Release date**: August 2026

### Enhancements

#### Parameter detection and binding run in native code

Parameter type detection and binding now run in a single native pipeline instead of per-parameter Python calls. This change fixes a significant performance bottleneck, with higher end-to-end throughput improvements in larger operations like bulk inserts. No application change is needed.

### Bug fixes

#### The `timeout` argument to `connect()` set the query timeout instead of the authentication timeout

The `timeout` argument now sets `SQL_ATTR_LOGIN_TIMEOUT` and bounds the authentication attempt, which is what the argument name and the documentation describe. In earlier versions it became the per-statement query timeout, so `connect(timeout=30)` didn't limit how long a connection attempt could run, and it aborted queries after 30 seconds. The per-statement query timeout remains available as the `Connection.timeout` property.

> [!IMPORTANT]
> If you passed `timeout` to `connect()` to abort long-running queries, that behavior no longer happens. Set `Connection.timeout` instead. The same applies if you relied on `connect(timeout=)` to widen the connect timeout that `bulkcopy()` uses for its internal connection: set `Connection.timeout` before you create the cursor.

For more information, see [Connection timeout](connection-management.md#connection-timeout).

#### `bulkcopy()` rejected `timeout=0`

A `timeout` of `0` raised a validation error, even though `0` means no timeout in the underlying bulk copy API. The method now accepts `0` and disables the operation timeout. Negative, non-integer, and boolean values are still rejected.

For more information, see [Bulk copy](bulk-copy.md).

#### Cleanup replaced the original Arrow fetch exception

When a fetch from an Arrow reader failed, the driver's cleanup path raised a second error that replaced the original one, so callers saw a cleanup failure instead of the reason the fetch failed. Cleanup now checks cursor state first and preserves the original exception.

#### `executemany()` decimal conversion errors included parameter values

A **decimal** conversion failure in `executemany()` reported the offending value through the chained exception, which could place customer data in application logs and monitoring systems. The error now reports the row index, the column index, and the value type only.

For more information, see [Error handling](error-handling.md).

#### Bulk copy rejected Arrow View types

`bulkcopy_arrow()` couldn't consume variable-length Arrow View arrays, so Polars `string_view` columns had to be converted with `DataFrame.to_arrow()` first. String View values and NULLs now pass through the Arrow C Data Interface directly.

For more information, see [Polars integration](polars-integration.md).

#### Windows extension loading used the host CPU architecture

On Windows, the driver picked its native extension based on the host CPU rather than the running interpreter, so x64 Python on an ARM64 host loaded through a fallback path and wrote notices to stdout. The loader now derives the architecture from the interpreter and reports fallbacks as warnings.

## mssql-python 1.13.0

**Release date**: August 2026

### Enhancements

#### The ODBC driver binaries ship only in `mssql-python-odbc`

Version 1.13.0 removes the `libs/` fallback from the `mssql-python` wheel and declares `mssql-python-odbc==18.6.2.1` in `install_requires`. The command `pip install mssql-python` still produces a working driver. Install `mssql-python-odbc` explicitly when you install with `--no-deps`, or from a private index that doesn't mirror it.

For more information, see [Installation](installation.md).

#### Bulk copy from Apache Arrow sources

The new `cursor.bulkcopy_arrow()` method loads data that's already in Apache Arrow format, without converting each row into Python objects first. Passing an Arrow source to `bulkcopy()` now raises `TypeError`.

For more information, see [Apache Arrow integration](arrow-integration.md) and [Bulk copy](bulk-copy.md).

#### `token_provider` parameter for Microsoft Entra credentials

The `connect()` function and the `Connection` class accept a `token_provider` argument, so you can pass a credential object such as `DefaultAzureCredential` instead of naming an authentication mode in the connection string. The argument is mutually exclusive with the `Authentication` keyword and with tokens passed through `attrs_before`, and it supports only the Azure commercial cloud scope.

For more information, see [Microsoft Entra authentication](entra-authentication.md).

#### Identity-aware connection pooling

The connection pool now separates connections by Microsoft Entra identity. In earlier versions, the pool keyed only on the connection string, so the pool could hand a connection authenticated as one user to a request made by another. The driver also acquires a token only when a connection request misses the pool, and refreshes a pooled connection when its token is within 5 minutes of expiry.

For more information, see [Connection pooling](connection-pooling.md).

### Bug fixes

#### `executemany()` inserted zero rows when NULLs appeared after the first row

An `executemany()` call that mixed non-NULL and NULL numeric values inserted zero rows and raised no exception, when the first NULL appeared after the first row. This behavior affected **tinyint**, **smallint**, **int**, and **float** parameters. The driver now initializes ODBC indicators for every fixed-width numeric parameter before array execution.

#### An `SQL_WVARCHAR` output converter transformed non-string columns

Registering a single string converter also transformed **int**, **decimal**, and **date** values, because the driver fell back to the `SQL_WVARCHAR` converter for any column without its own converter. The driver now uses that fallback only when the column's mapped Python type is `str` or `bytes`.

#### Output converters registered by integer SQL type code never ran

Converters registered with an integer SQL type code, such as `SQL_DECIMAL`, were stored but never invoked, because the driver dispatched only on the Python type in `cursor.description`. The driver now dispatches on the integer code first, then the Python type, and then the `SQL_WVARCHAR` fallback.

> [!IMPORTANT]
> If you registered converters by integer SQL type code in an earlier version, those converters begin running when you upgrade. Review them before you deploy, because column values that previously passed through unchanged are now transformed.

For more information, see [Custom type converters](custom-type-converters.md).

#### Closing an Arrow reader didn't release the server-side cursor

Closing an Arrow reader left the server-side cursor allocated and the parent `Cursor` in an inconsistent state, because `cursor.arrow_reader()` returned a raw `pyarrow.RecordBatchReader`. The method now returns a wrapper whose `close()` method releases the server-side cursor and resets cursor state, and the wrapper works as a context manager.

For more information, see [Apache Arrow integration](arrow-integration.md).

#### A partially initialized cursor raised `AttributeError` in `Cursor.__del__`

A cursor whose initializer failed raised an `AttributeError` as an unraisable exception during garbage collection.

`Cursor.__init__` raised before it set the `closed` and `hstmt` attributes, which `__del__` then tried to read. The initializer now sets both attributes before any code that can raise, and `__del__` guards its logging call so that it stays safe during interpreter shutdown.

## mssql-python 1.12.0

**Release date**: July 2026

### Enhancements

#### Standalone `mssql-python-odbc` companion package

The ODBC driver binaries are now published separately as [mssql-python-odbc](https://pypi.org/project/mssql-python-odbc/), a data-only companion package pinned to version `18.6.2`. You don't need to change any code, because `pip install mssql-python` installs the companion package with it. The native loader prefers the companion package and falls back to the binaries bundled inside the `mssql-python` wheel when it isn't present.

For more information, see [Installation](installation.md).

### Bug fixes

#### `cursor.bulkcopy()` now uses the parent connection's connect timeout

`bulkcopy()` now uses the connect timeout of its parent connection, which you set with `connect(..., timeout=<seconds>)`. Previously, the separate connection that bulk copy opens used a hardcoded 15-second connect timeout that you couldn't override from Python. A parent connection created with `timeout=0` still gets the 15-second default.

For more information, see [Bulk copy](bulk-copy.md).

#### `cursor.bulkcopy()` supports CLR user-defined type columns

`cursor.bulkcopy()` previously failed with `Protocol Error: Unsupported TDS type for bulk copy: 0xF0` for any destination column that used a common language runtime (CLR) user-defined type, including the built-in **geography**, **geometry**, and **hierarchyid** types. The driver now maps CLR UDT columns to **varbinary(max)** on the wire and streams the bytes you supply as the UDT's `IBinarySerialize` payload. The fix ships in `mssql_py_core` 0.1.7.

For more information, see [Bulk copy](bulk-copy.md) and [Data type mappings](data-type-mappings.md).

## mssql-python 1.11.0

**Release date**: July 2026

### Enhancements

#### Improved context manager semantics

`with connection:` now commits the transaction when the block exits cleanly, and rolls it back when an exception leaves the block.

For more information, see [Transaction management](transaction-management.md).

### Bug fixes

- Fixed a GIL-deadlock in the ODBC teardown path (`conn.close()` and `cursor.close()`) and in `SQLDescribeParam` for `None`-valued parameters in SSH-tunnel and in-process forwarder setups.
- Fixed `BINARY` and `VARBINARY` NULL parameters in temp tables and table variables. When automatic type resolution fails, the driver now emits a Python warning with explicit `cursor.setinputsizes()` guidance.
- Fixed `import mssql_python` failing on Apple Silicon with a clean install (regression in 1.8.0). The bundled ODBC dylib dependencies are now rewritten for both `arm64` and `x86_64` architectures.
- Fixed a GIL-deadlock in the Rust core that froze bulk copy operations when authenticating with `Authentication=ActiveDirectoryServicePrincipal`.

## mssql-python 1.10.0

**Release date**: June 2026

### Enhancements

#### ActiveDirectoryServicePrincipal support for bulk copy

`cursor.bulkcopy()` now supports `Authentication=ActiveDirectoryServicePrincipal`, so you can bulk insert with service principal credentials.

For more information, see [Bulk copy](bulk-copy.md) and [Microsoft Entra authentication](entra-authentication.md).

### Bug fixes

- Fixed non-ASCII `VARCHAR` and `CHAR` data in the Arrow fetch path.
- Fixed connection timeouts during bulk load operations.

## mssql-python 1.9.0

**Release date**: June 2026

### Enhancements

#### Row objects in bulk copy

`cursor.bulkcopy()` now accepts fetched `Row` objects directly instead of requiring manual tuple conversion.

For more information, see [Bulk copy](bulk-copy.md) and [Row objects](row-objects.md).

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

For more information, see [Bulk copy](bulk-copy.md) and [Microsoft Entra authentication](entra-authentication.md).

#### Row string-key indexing

You can now access row values by column name, for example `row["col"]`, in addition to positional indexing and attribute access.

For more information, see [Row objects](row-objects.md).

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

This release includes:

- RHEL 8-compatible wheels.
- Restored macOS Python 3.10 `universal2` wheels.
- Improved UTF-16 handling through `simdutf`.
- Optimized `execute()` hot path.

**Performance impact**: Batch execution throughput improves because of hot path optimizations in the `execute()` method.

For more information, see [Installation](installation.md).

### Bug fixes

- Fixed authentication failures so they raise `mssql_python` DB-API exceptions instead of `RuntimeError`.
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

Connection string sanitization now uses a parser instead of regular expressions, so connection strings that contain special characters in password fields and braced values parse correctly.

For more information, see [Connection strings](connection-strings.md).

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

These methods don't create a Python object for each value. For complete documentation, see [Apache Arrow integration](arrow-integration.md).

#### sql_variant type support

The driver now detects `sql_variant` columns at fetch time, resolves their underlying base type, and returns correctly typed Python values instead of raw bytes.

> [!NOTE]
> `sql_variant` columns use a streaming fetch path, which might have a slight performance impact compared to fixed-type columns.

For more information, see [Data type mappings](data-type-mappings.md).

#### Native UUID support

A new `native_uuid` setting controls whether `UNIQUEIDENTIFIER` columns are returned as `uuid.UUID` objects (default) or as pyodbc-compatible uppercase strings. Configure it at the module level or per connection.

For more information, see [Module configuration](module-configuration.md#native_uuid).

#### Row class public export

The `Row` class is now exported at the top level for type annotations.

For more information, see [Row objects](row-objects.md).

### Bug fixes

- Fixed false positive `?` detection inside bracketed identifiers, string literals, and comments.
- Fixed NULL parameter binding for `VARBINARY` columns (no longer raises implicit conversion errors).
- Fixed `datetime.time` values losing microseconds on round-trips for `TIME(1)` through `TIME(7)` columns.
- Fixed Arrow fetch path to correctly include fractional seconds for `TIME` columns.
- Fixed bulk copy with Microsoft Entra ID authentication methods (stale credential fields no longer cause validation errors).
- Cached Azure Identity credential instances at module level for improved authentication performance.

## mssql-python 1.4.0

**Release date**: February 2026

### New features

#### Bulk copy support

High-performance bulk data loading is now available through `cursor.bulkcopy()`. The method accepts options for `batch_size`, `timeout`, `column_mappings`, `keep_identity`, `check_constraints`, `table_lock`, `keep_nulls`, `fire_triggers`, and `use_internal_transaction`.

For more information, see [Bulk copy](bulk-copy.md).

### Improvements

- Performance optimizations for large result sets.
- Reduced memory usage during batch operations.
- Enhanced error messages for bulk copy failures.

<!-- markdownlint-disable MD024 -->

## mssql-python 1.3.0

**Release date**: January 2026

### New features

#### Settings class

Configure module-wide behavior through the new `Settings` class, which includes the `lowercase` setting for column names in `cursor.description`.

For more information, see [Module configuration](module-configuration.md).

### Improvements

- Better handling of connection timeout during Azure SQL failover.
- Improved compatibility with Python 3.13.

## mssql-python 1.2.0

**Release date**: January 2026

### New features

#### Schema discovery methods

New cursor methods explore database metadata: `tables()`, `columns()`, `primaryKeys()`, `foreignKeys()`, `procedures()`, `statistics()`, and `getTypeInfo()`.

For more information, see [Schema discovery](schema-discovery.md).

### Improvements

- Enhanced metadata caching for repeated schema queries.
- Better handling of computed columns in `columns()` results.

## mssql-python 1.1.0

**Release date**: December 2025

### New features

#### Custom output converters

Register custom functions to transform column values during fetch, with `add_output_converter()`, `get_output_converter()`, `remove_output_converter()`, and `clear_output_converters()`.

For more information, see [Custom type converters](custom-type-converters.md).

### Improvements

- Better error messages for type conversion failures.
- Support for converter functions that return `None`.

## mssql-python 1.0.0

**Release date**: November 2025

### Initial GA release

The first general availability release of mssql-python, Microsoft's native Python driver for SQL Server.

For more information, see [mssql-python driver](python-sql-driver-mssql-python.md).

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
