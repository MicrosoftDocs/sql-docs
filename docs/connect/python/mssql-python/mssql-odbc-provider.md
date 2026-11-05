---
title: Use the mssql-odbc Driver (Preview)
description: Select the preview mssql-odbc provider in mssql-python, configure connections, and review supported features and known issues.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto
ms.date: 11/04/2026
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

The `mssql-python-rs` **0.4.0** package includes the `mssql-odbc` provider.

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

The Rust provider enables `MultiSubnetFailover` by default for applicable
Transmission Control Protocol (TCP) connections. Set `MultiSubnetFailover=no`
explicitly to request sequential connection attempts. To change the
deployment-wide default, set `MSSQL_RS_MULTISUBNET_FAILOVER_DEFAULT` to `true` or
`false` before the native driver is first used. The connection-string keyword
overrides this default.

The numeric `SQL_COPT_SS_MULTISUBNET_FAILOVER` attribute (`1248`) also overrides the keyword when set before connecting.

The driver doesn't enable this default for non-TCP transports, database
mirroring, named instances that require SQL Server Browser discovery, or local Windows
connections that omit an explicit protocol and address. Explicitly enabling
`MultiSubnetFailover` for a non-TCP transport, database mirroring, or SQL Server Browser
discovery raises a connection error. For more information, see
[High availability and failover](connection-strings.md#high-availability-and-failover).

<a id="what-works-today"></a>

## Supported features

The following features are supported, subject to the [known issues](#known-issues).

| Area | Notes |
|---|---|
| Connection lifecycle | Connect, `close()`, `with` blocks, login timeout, diagnostics, and changing the current database. Connection-string settings include `ApplicationIntent`, `MultiSubnetFailover`, `IpAddressPreference`, `PacketSize`, `KeepAlive`, and `ConnectRetryCount` / `ConnectRetryInterval` (idle connection resiliency). The native driver also negotiates server-assisted Domain Name System (DNS) caching and enhanced routing when the server supports them. |
| Authentication | SQL authentication (`UID` / `PWD`); Windows authentication (`Trusted_Connection=Yes`, using Security Support Provider Interface (SSPI) on Windows or Kerberos on Linux and macOS); and Microsoft Entra ID interactive, integrated, device code, service principal, managed identity, default credential, and access token. Microsoft Entra ID password authentication isn't supported. For more information, see [Known issues](#known-issues). |
| Transport Layer Security (TLS) | `Encrypt`, `TrustServerCertificate`, `HostnameInCertificate`, and `ServerCertificate`. |
| Query execution | Direct and prepared execution, prepared-plan reuse, `qmark` parameters, and the `pyformat` conversion `mssql-python` performs. |
| ODBC escape syntax | Function, date/time, outer-join, globally unique identifier (GUID), `LIKE ... {escape ...}`, interval, and `{encrypt ...}` escape forms. |
| Stored procedures | Call them with Transact-SQL (T-SQL) or positional `{call ...}` syntax through `execute()`, read result sets, and advance with `nextset()`. |
| Input parameters | Scalar parameters, SQL `NULL`, `setinputsizes()`, and mixed regular/streamed parameters. |
| Large parameters | Large text, binary, Extensible Markup Language (XML), and user-defined type (UDT) parameters support data-at-execution streaming. |
| `executemany()` | Batched with ODBC parameter arrays. Rows containing streamed values fall back to row-by-row execution. |
| Row fetching | `fetchone()`, `fetchmany()`, `fetchall()`, iteration, `fetchval()`, typed conversion, and large text/binary streaming. |
| Arrow fetching | `arrow_batch()`, `arrow()`, and `arrow_reader()`. Requires `pyarrow`. |
| Multiple result sets | `nextset()`, including results produced by parameter arrays. |
| Transactions | Autocommit, commit, rollback, and the read-uncommitted, read-committed, repeatable-read, serializable, and snapshot isolation levels. With autocommit off, the connection context manager commits on successful exit and rolls back on an exception. |
| Query timeout | Set `Connection.timeout` in seconds before creating a cursor. When a statement exceeds that timeout, the driver raises `OperationalError`. `0` means no timeout. Set the login timeout separately with `connect(timeout=...)`. |
| Connection pooling | The built-in `mssql-python` pool, including connection reset before reuse. For authentication-specific pool isolation, see [Identity isolation](connection-pooling.md#identity-isolation). |
| Metadata APIs | `tables()`, `columns()`, `primaryKeys()`, `foreignKeys()`, `statistics()`, `rowIdColumns()`, `rowVerColumns()`, `procedures()`, and `getTypeInfo()`. |
| Encoding and converters | Default wide-character encoding, output converters, and collation-aware non-Unicode parameters, including UTF-8 and legacy SQL sort-ID collations. Custom narrow-character decoding must match the client's code page. For more information, see [Known issues](#known-issues). |

Microsoft Entra ID integrated authentication uses the signed-in Windows account on Windows. On Linux and macOS, it uses a Kerberos ticket to authenticate to the tenant's Active Directory Federation Services (AD FS) endpoint. For prerequisites, see [Windows Integrated authentication](entra-authentication.md#windows-integrated-authentication).

Closing an `arrow_reader()` sends `SQLCancel` to interrupt an in-progress fetch.
The close call waits for cancellation cleanup to finish. For more information,
see [Apache Arrow integration](arrow-integration.md).

### Data types

The driver returns **xml**, **json**, and **vector** values as Python `str`
objects, with **vector** represented as a JavaScript Object Notation (JSON) array.
It returns common language runtime (CLR) types such as **geometry**,
**geography**, and **hierarchyid** as Python `bytes` objects. For more information, see
[Data type mappings](data-type-mappings.md) and [Known issues](#known-issues).

## Known issues

Check these differences before switching a workload from `msodbcsql18`.

| Issue | Details | Workaround |
|---|---|---|
| **Microsoft Entra ID password authentication** | `Authentication=ActiveDirectoryPassword` isn't supported. This method can't satisfy multifactor authentication (MFA). SQL authentication (`UID` / `PWD`) is unaffected. | Use another Microsoft Entra ID method, such as a service principal, managed identity, or access token. |
| **`attrs_before` settings** | Microsoft Entra ID authentication mode and application intent can't be set as numeric connection attributes. | Use the `Authentication` and `ApplicationIntent` connection-string keywords. |
| **Connection-operation timeout** | `SQL_ATTR_CONNECTION_TIMEOUT` controls cancellation cleanup. It doesn't enforce a timeout on pooled-connection reset. A connection request can remain blocked if the server stops responding during reset. | No pooled-reset timeout is available. Set `connect(timeout=...)` for login and `Connection.timeout` for statements. |
| **Multi-address hosts** | `msodbcsql18` enables Transparent Network IP Resolution (TNIR) by default; `mssql-odbc` doesn't support it. `mssql-python` rejects the `TransparentNetworkIPResolution` connection-string keyword with either provider. | Use `MultiSubnetFailover=yes` for availability group listeners and other multi-address hosts. For defaults, see [Multi-subnet failover defaults](#multi-subnet-failover-defaults). |
| **Data classification** | Sensitivity classification metadata isn't available, and `SQL_COPT_SS_DATACLASSIFICATION_VERSION` (`1400`) isn't supported. | Query `sys.sensitivity_classifications`. |
| **`fork()` after connecting** | On Linux, a process forked after the parent connects can hang when it uses the driver, even with a new connection. With pooling enabled, the parent can hang too. ([#566](https://github.com/microsoft/mssql-rs/issues/566)) | Use the `spawn` start method and connect inside each worker. |
| **Native tracing on Linux** | With native tracing enabled, Python can abort during driver cleanup at process exit. | Leave `MSSQL_TDS_TRACE` unset on Linux. |
| **Unread results** | Closing a cursor or running another statement reads and discards remaining results instead of canceling them, which can be slow. Prepared-statement cleanup uses the statement's query timeout, but a zero timeout leaves that cleanup unbounded. | Fetch only the rows you need, for example with `TOP`, and set `Connection.timeout` before creating the cursor. |
| **Custom narrow-character decoding** | The native driver uses the Windows ANSI code page on Windows and the process locale's code page on Linux and macOS for narrow text. An explicit Python decoding that doesn't match that code page can produce incorrect text or `bytes`. | Use the default wide-character decoding: `conn.setdecoding(mssql_python.SQL_CHAR)`. |
| **`Connection.getinfo()`** | Some capability values and packet-size-based limits differ from `msodbcsql18`. `SQL_USER_NAME` reports the database principal rather than the connection-string `UID`. | Query the specific server metadata when a reported capability or limit matters to your application. |
| **Out-of-memory failures** | Some driver allocation failures end the Python process instead of raising `MemoryError`. ([#484](https://github.com/microsoft/mssql-rs/issues/484)) | Limit the size of values you send and fetch. |
| **Large-value read performance** | Chunked reads of **varchar(max)** and **nvarchar(max)** data can be slower than with `msodbcsql18`. ([#473](https://github.com/microsoft/mssql-rs/issues/473)) | Benchmark your workload. |

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
native-driver diagnostics. Reproduce the problem with `mssql-odbc` selected:

```powershell
$env:MSSQL_TDS_TRACE = "true"
$env:MSSQL_TDS_TRACE_LEVEL = "debug"
$env:MSSQL_TDS_TRACE_DIR = ".\traces"
```

`MSSQL_TDS_TRACE` enables tracing, `MSSQL_TDS_TRACE_LEVEL` selects the log
level, and `MSSQL_TDS_TRACE_DIR` selects the directory for per-process trace
files from the native ODBC driver instead of standard error output. The Rust
companion package also creates log files under `mssql_python_logs` in the current
directory.

Traces can contain T-SQL text and parameter values. Redact sensitive data before
sharing excerpts. After diagnosis, remove these variables and restart Python to
disable tracing.

## Related content

- [Install mssql-python](installation.md)
- [Connection strings](connection-strings.md)
- [Microsoft Entra ID authentication](entra-authentication.md)
- [Data type mappings](data-type-mappings.md)
- [Troubleshooting](troubleshooting.md)
