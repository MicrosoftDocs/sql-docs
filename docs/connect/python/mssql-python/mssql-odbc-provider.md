---
title: Use the mssql-odbc Driver (Preview)
description: Select the preview mssql-odbc provider in mssql-python, configure connections, and review supported features and known issues.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: tkotian
ms.date: 11/19/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Use the mssql-odbc driver (Preview)

<a id="using-the-mssql-odbc-driver-preview"></a>

`mssql-python` loads one of two native ODBC providers:

| Provider | Description | Status |
|---|---|---|
| `msodbcsql18` | Microsoft ODBC Driver 18 for SQL Server | Default |
| `mssql-odbc` | Rust-based ODBC driver | Preview, opt-in |

This article covers the `mssql-odbc` provider shipped in **`mssql-python-rs` 0.5.0**
and what differs from `msodbcsql18`.

> [!NOTE]
> Use `msodbcsql18` for production workloads. Before you opt in to the preview
> `mssql-odbc` provider, review its [known issues](#known-issues).

## Availability

The driver ships inside the `mssql-python-rs` package as prebuilt wheels for
CPython 3.10 and later versions:

| Platform | Architectures |
|---|---|
| Windows | x64; ARM64 on CPython 3.11 and later versions |
| macOS | universal2 (x86_64 and arm64), macOS 15 and later versions |
| Linux (glibc) | x86_64 and aarch64 (`manylinux_2_28` and `manylinux_2_34`) |
| Linux (musl) | x86_64 and aarch64 (`musllinux_1_2`) |

<a id="enabling-the-driver"></a>

## Enable the driver

Installing `mssql-python` also installs `mssql-python-rs`, which contains the
`mssql-odbc` driver:

```bash
python -m pip install mssql-python
```

If you install with `--no-deps` or from a private index, install
`mssql-python-rs` explicitly as well.

Select the provider before opening the first connection. Use one of the following
methods.

# [PowerShell](#tab/powershell)

```powershell
$env:MSSQL_PYTHON_NATIVE_PROVIDER = "mssql-odbc"
```

# [Bash or Zsh](#tab/bash)

```bash
export MSSQL_PYTHON_NATIVE_PROVIDER=mssql-odbc
```

# [Python](#tab/python)

```python
import mssql_python

mssql_python.native_provider = "mssql-odbc"
```

---

Set the `MSSQL_CONNECTION_STRING` environment variable to your application's
connection string. Keep certificate validation enabled
(`Encrypt=yes;TrustServerCertificate=no`). For more information, see
[Connection strings](connection-strings.md) and
[Encryption and certificates](encryption-certificates.md).

Run this example with your selected provider:

```python
import os
import mssql_python

with mssql_python.connect(os.environ["MSSQL_CONNECTION_STRING"], timeout=45) as connection:
    with connection.cursor() as cursor:
        cursor.execute("SELECT 1")
        print(cursor.fetchone()[0])
```

The example prints `1`.

Provider selection follows these rules:

- `MSSQL_PYTHON_NATIVE_PROVIDER` takes precedence over `native_provider`. Setting the attribute to a conflicting provider emits `RuntimeWarning`.
- Before the driver loads, set `native_provider` to `None` to clear the Python setting. This doesn't clear the environment variable.
- An unrecognized provider name raises `ValueError`. Assigning an invalid name to `native_provider` raises immediately; an invalid environment variable raises when you connect. The driver doesn't fall back to the default provider.
- The provider is fixed when the driver begins a native connection attempt, even if that attempt fails. Restart Python to switch providers. Changing `native_provider` to another provider emits `RuntimeWarning` and has no effect.

Connection-string validation errors and missing provider packages occur before
this point. You can correct these errors and choose another provider without
restarting.

Call `mssql_python.get_native_provider_info()` to check the provider name, how it
was selected, and whether the selection is fixed.

To return to `msodbcsql18`, remove `MSSQL_PYTHON_NATIVE_PROVIDER` and any
`native_provider` assignment from your application, then restart Python.

### Multi-subnet failover defaults

The Rust provider defaults to `MultiSubnetFailover=yes` for eligible TCP
connections. Set `MultiSubnetFailover=no` for sequential attempts. To change
the deployment default, set `MSSQL_RS_MULTISUBNET_FAILOVER_DEFAULT` to `true`
or `false` (case-insensitive) before first driver use. Other values cause an
error unless overridden for the connection.

The connection-string keyword overrides the environment default. The pre-connect
`SQL_COPT_SS_MULTISUBNET_FAILOVER` attribute (`1248`) overrides both.

The default excludes non-TCP transports, database mirroring, and named instances
that require SQL Server Browser discovery. These also reject explicit
`MultiSubnetFailover=yes`.

On Windows, the default is also disabled for bare local targets such as `localhost`.
An explicit TCP prefix or port, such as `tcp:localhost` or `localhost,1433`, avoids
this local-target exclusion. Explicit `MultiSubnetFailover=yes` also overrides it. See
[High availability and failover](connection-strings.md#high-availability-and-failover).

<a id="what-works-today"></a>

## Supported features

The following features are supported, subject to the [known issues](#known-issues).

| Area | Notes |
|---|---|
| Connection lifecycle | Connect, `close()`, `with` blocks, login timeout, diagnostics, and changing the current database. Connection-string settings include `ApplicationIntent`, `MultiSubnetFailover`, `IpAddressPreference`, `PacketSize`, `KeepAlive`, and `ConnectRetryCount` / `ConnectRetryInterval` (idle connection resiliency). |
| Network resilience | Eligible TCP connections use parallel address attempts by default. Parallel mode doesn't prioritize addresses by `IpAddressPreference` and rejects more than 64 resolved addresses. For sequential attempts and address-family ordering, see [Multi-subnet failover defaults](#multi-subnet-failover-defaults). The separate Transparent Network IP Resolution (TNIR) algorithm isn't implemented. Server-assisted Domain Name System (DNS) caching enables cached-address fallback on DNS failure after a successful login where the server permits caching. Enhanced routing is used when negotiated with the server. |
| Authentication | SQL authentication (`UID` / `PWD`); Windows authentication (`Trusted_Connection=Yes`, using Security Support Provider Interface (SSPI) on Windows or Kerberos on Linux and macOS); and Microsoft Entra ID interactive, integrated, device code, service principal, managed identity, default credential, and access token. Microsoft Entra ID password authentication isn't supported. For more information, see [Known issues](#known-issues). |
| Transport Layer Security (TLS) | `Encrypt`, `TrustServerCertificate`, `HostnameInCertificate`, and `ServerCertificate`. On macOS, `Encrypt=Strict` TCP connections can negotiate TLS 1.3 with a compatible server. |
| Query execution | Direct and prepared execution, prepared-plan reuse, `qmark` parameters, and the `pyformat` conversion `mssql-python` performs. |
| ODBC escape syntax | Function, date/time, outer-join, globally unique identifier (GUID), `LIKE ... {escape ...}`, interval, and `{encrypt ...}` escape forms. |
| Stored procedures | Call them with Transact-SQL (T-SQL) or positional `{call ...}` syntax through `execute()`, read result sets, and advance with `nextset()`. |
| Input parameters | Scalar parameters, SQL `NULL`, `setinputsizes()`, and mixed regular/streamed parameters. |
| Large parameters | Large text and binary parameters stream to the server on the ordinary binding path. Explicit `SQL_SS_XML` and `SQL_SS_UDT` bindings through `setinputsizes()` also stream when Python selects data-at-execution: the declared size must exceed 4000 for `str` or 8000 for `bytes` or `bytearray`. User-defined type (UDT) binding requires the server to describe the parameter's type. Some custom and mixed bindings still buffer. For more information, see [Known issues](#known-issues). |
| `executemany()` | Batched with ODBC parameter arrays. Rows containing streamed values fall back to row-by-row execution. |
| Row fetching | `fetchone()`, `fetchmany()`, `fetchall()`, iteration, `fetchval()`, typed conversion, and large text/binary streaming. |
| Arrow fetching | `arrow_batch()`, `arrow()`, and `arrow_reader()`. Requires `pyarrow`. |
| Multiple result sets | `nextset()`, including results produced by parameter arrays. |
| Transactions | Autocommit, commit, rollback, and the read-uncommitted, read-committed, repeatable-read, serializable, and snapshot isolation levels. With autocommit off, the connection context manager commits on successful exit and rolls back on an exception. |
| Query timeout | Set `Connection.timeout` in seconds before creating a cursor. When a statement exceeds that timeout, the driver raises `OperationalError`. `0` means no timeout. Set the login timeout separately with `connect(timeout=...)`. |
| Connection-operation timeout | `conn.set_attr(mssql_python.SQL_ATTR_CONNECTION_TIMEOUT, seconds)` applies to connection-owned network requests, including commit, rollback, and catalog and isolation changes. Pooled reset defers the server reset to the following isolation-setting request; both that request and any preceding rollback use this timeout. `0` leaves those requests unbounded. This timeout is separate from login and query timeouts and isn't a total pool-checkout deadline. Cancellation cleanup can add to elapsed call time. |
| Cursor cleanup | Closing a cursor cancels pending server work when needed instead of reading every remaining result. Prepared-statement cleanup honors the query timeout. Cancellation cleanup has an additional wait budget equal to the connection timeout, or 120 seconds when that timeout is `0`. |
| Connection pooling | The built-in `mssql-python` pool, including connection reset before reuse. For authentication-specific pool isolation, see [Identity isolation](connection-pooling.md#identity-isolation). |
| Multithreading | Concurrent connections and queries with a separate connection and cursor per worker. Don't share cursors between threads. |
| Metadata APIs | `tables()`, `columns()`, `primaryKeys()`, `foreignKeys()`, `statistics()`, `rowIdColumns()`, `rowVerColumns()`, `procedures()`, `getTypeInfo()`, and `getinfo()`. `SQL_USER_NAME` reports the database user rather than the connection-string `UID`. Capability values and packet-size-based limits can differ from `msodbcsql18`: `SQL_NEED_LONG_DATA_LEN` reports `"N"` because long input doesn't require a declared total length. Use the returned capabilities and limits. |
| Encoding and converters | Default wide-character encoding, output converters, collation-aware non-Unicode parameters, and narrow-character results in the platform's client encoding. Custom narrow-character decoding must match that encoding; a mismatch can produce incorrect text or `bytes`. Use `conn.setdecoding(mssql_python.SQL_CHAR)` to restore default wide-character decoding. For non-Unicode parameter substitution, see [Known issues](#known-issues). |

Microsoft Entra ID integrated authentication uses the signed-in Windows account on Windows. On Linux and macOS, it uses a Kerberos ticket to authenticate to the tenant's Active Directory Federation Services (AD FS) endpoint. For prerequisites, see [Windows Integrated authentication](entra-authentication.md#windows-integrated-authentication).

If a fetch is running when you close an `arrow_reader()`, `close()` cancels the fetch and returns after it stops. Closing a reader from an earlier query doesn't affect a later query on the same cursor. For more information, see [Apache Arrow integration](arrow-integration.md).

### Data types

The driver returns **xml**, **json**, and **vector** values as Python `str`
objects, with **vector** represented as a JavaScript Object Notation (JSON) array.
It returns common language runtime (CLR) types such as **geometry**,
**geography**, and **hierarchyid** as Python `bytes` objects. For more information, see
[Data type mappings](data-type-mappings.md) and [Known issues](#known-issues).

## Known issues

Check these limitations before switching a workload from `msodbcsql18`.
The following table marks intentional exclusions.

| Issue | Details | Workaround |
|---|---|---|
| **Microsoft Entra ID password authentication (intentional exclusion)** | `Authentication=ActiveDirectoryPassword` isn't supported, and support isn't planned because this method can't satisfy multifactor authentication (MFA). SQL authentication (`UID` / `PWD`) is unaffected. | Use another Microsoft Entra ID method, such as a service principal, managed identity, or access token. |
| **`attrs_before` settings** | The numeric authentication selector (`SQL_COPT_SS_AUTHENTICATION`) and application-intent attribute remain unsupported. Multi-subnet failover and integrated-security attributes are implemented. Metadata identifier matching (`SQL_ATTR_METADATA_ID`) is also unsupported. | Use the `Authentication` and `ApplicationIntent` connection-string keywords where applicable. |
| **`fork()` after connecting** | On Linux, a process forked after the parent connects can hang when it uses the driver, even with a new connection. With pooling enabled, the parent can hang too. ([#566](https://github.com/microsoft/mssql-rs/issues/566)) | Use the `spawn` start method and connect inside each worker. |
| **Native tracing on Linux** | With native tracing enabled, Python can abort during driver cleanup at process exit. | Leave `MSSQL_TDS_TRACE` unset on Linux. |
| **Memory use with custom large-parameter bindings** | Data-at-execution values bound as fixed-width `SQL_CHAR`, `SQL_WCHAR`, or `SQL_BINARY` are buffered for conversion. Mixing buffered and streamed parameters can also buffer earlier large values while waiting for a later buffered value. | Prefer the default variable-length bindings. Avoid large fixed-width overrides through `setinputsizes()` when memory use matters. |
| **Non-Unicode character substitution** | For characters outside a non-Unicode parameter's target code page, the driver can substitute `?` where `msodbcsql18` uses a best-fit character. | Prefer Unicode SQL types and the default Python string bindings when text must be preserved. |
| **Isolation changes after raw SQL** | After raw `SET TRANSACTION ISOLATION LEVEL`, setting the connection attribute to the driver's cached level can return success without restoring that level on the server. ([#798](https://github.com/microsoft/mssql-rs/issues/798)) | Avoid mixing raw SQL and attribute-based isolation changes. Use an explicit `SET TRANSACTION ISOLATION LEVEL` when restoring the level. |
| **Out-of-memory failures** | Some driver allocation failures end the Python process instead of raising `MemoryError`. ([#484](https://github.com/microsoft/mssql-rs/issues/484)) | Limit the size of values you send and fetch. |
| **Large-value read performance** | Chunked reads of **varchar(max)** and **nvarchar(max)** data can be slower than with `msodbcsql18`. Results depend on the platform and workload. ([#473](https://github.com/microsoft/mssql-rs/issues/473)) | Benchmark your workload. |

This list isn't exhaustive.

<a id="reporting-problems"></a>

## Report problems

If you encounter a bug, reproduce it with `msodbcsql18` and record whether it
occurs with both providers. For switching instructions, see
[Enable the driver](#enable-the-driver).

Include the output of `mssql_python.get_native_provider_info()`, your operating
system, Python and driver versions, and the failing T-SQL batch and error message
in your report:

- Rust driver issues: <https://github.com/microsoft/mssql-rs/issues>
- `mssql-python` issues: <https://github.com/microsoft/mssql-python/issues>

On Windows, set these variables in PowerShell before starting Python to collect
native-driver diagnostics. Reproduce the problem with `mssql-odbc` selected.
On Linux, leave `MSSQL_TDS_TRACE` unset because of the process-exit failure
described in [Known issues](#known-issues).

```powershell
$env:MSSQL_TDS_TRACE = "true"
$env:MSSQL_TDS_TRACE_LEVEL = "debug"
$env:MSSQL_TDS_TRACE_DIR = "./traces"
$env:MSSQL_TDS_TRACE_MAX_FILE_SIZE_MB = "100"
```

`MSSQL_TDS_TRACE` enables tracing, `MSSQL_TDS_TRACE_LEVEL` selects the log
filter, and `MSSQL_TDS_TRACE_DIR` selects the directory for per-process trace
files from the native ODBC driver instead of standard error output.
`MSSQL_TDS_TRACE_MAX_FILE_SIZE_MB` sets the per-file rollover threshold
(100 MiB by default; accepts 1-1024). It isn't a total disk-space cap:
individual files can exceed the threshold, and old files aren't deleted.
The Rust companion package also creates log files under `mssql_python_logs` in
the current directory. Restart Python after changing these settings.

Traces can contain T-SQL text and parameter values. Redact sensitive data before
sharing excerpts. After diagnosis, remove these variables and restart Python to
disable tracing.

## Related content

- [Install mssql-python](installation.md)
- [Connection strings](connection-strings.md)
- [Microsoft Entra ID authentication](entra-authentication.md)
- [Data type mappings](data-type-mappings.md)
- [Troubleshooting](troubleshooting.md)
- [Known issues and unsupported features](https://github.com/microsoft/mssql-python/wiki/Known-Issues-and-Unsupported-Features)
