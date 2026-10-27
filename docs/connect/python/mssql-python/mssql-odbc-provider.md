---
title: Using the mssql-odbc driver (Preview)
description: Learn how to opt in to the Rust-based mssql-odbc native ODBC provider in mssql-python, what works today, and the known differences from Microsoft ODBC Driver 18.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Using the mssql-odbc driver (Preview)

`mssql-python` loads one of two native ODBC providers:

| Provider | What it is | Status |
|---|---|---|
| `msodbcsql18` | Microsoft ODBC Driver 18 for SQL Server, the established C++ driver | Default, unchanged |
| `mssql-odbc` | A new ODBC driver written in Rust | **Preview**, opt-in |

This article covers `mssql-odbc` **0.3.0**, the version currently used by
`mssql-python`, and what differs from `msodbcsql18`.

> [!NOTE]
> `mssql-odbc` is under active development and validation. It's not
> yet a complete replacement for Microsoft ODBC Driver 18. Keep
> `msodbcsql18` as your production default for now. The development team plans to close the preview
> gaps in upcoming releases, except for intentional exclusions explicitly
> identified in this article.

## Availability

The driver ships inside the `mssql-python-rs` package as prebuilt wheels for
CPython 3.10 and later:

| Platform | Architectures |
|---|---|
| Windows | x64; ARM64 on CPython 3.11+ |
| macOS | universal2 (x86_64 + arm64), macOS 15 or later |
| Linux (glibc) | x86_64, aarch64 — `manylinux_2_28` and `manylinux_2_34` |
| Linux (musl) | x86_64, aarch64 — `musllinux_1_2` |

## Enabling the driver

The `mssql-python` package depends on `mssql-python-rs`, so a normal installation
brings the Rust driver with it:

```bash
pip install mssql-python
```

If you install with `--no-deps` or from a private index, install
`mssql-python-rs` explicitly as well.

Select the provider **before the first connection**, either with an environment
variable:

PowerShell:

```powershell
$env:MSSQL_PYTHON_NATIVE_PROVIDER = "mssql-odbc"
```

Bash or Zsh:

```bash
export MSSQL_PYTHON_NATIVE_PROVIDER=mssql-odbc
```

or from Python:

```python
import mssql_python

mssql_python.native_provider = "mssql-odbc"
```

Connect by using your application's existing connection string. Keep certificate validation enabled (`Encrypt=yes;TrustServerCertificate=no`). For authentication and certificate setup, see [Connection strings](connection-strings.md) and [Encryption and certificates](encryption-certificates.md).

```python
connection = mssql_python.connect(connection_string)
```

The driver applies these rules when selecting the provider:

- The `MSSQL_PYTHON_NATIVE_PROVIDER` environment variable takes precedence over `native_provider`. A conflicting attribute assignment emits `RuntimeWarning`.
- Selection becomes fixed after connection-string validation and immediately before the native connection is constructed. A subsequent connection failure doesn't clear the selection. An earlier connection-string parsing error or missing provider package doesn't fix the selection. After selection is fixed, changing `native_provider` to a different provider emits `RuntimeWarning` and has no effect. Restart the process to switch providers.
- An unrecognized provider name raises `ValueError`; the driver doesn't fall back to the default provider. An unknown name assigned to `native_provider` raises immediately. An unknown name in the environment variable raises when you attempt to connect.
- Set `native_provider` to `None` to clear the attribute selection before it becomes fixed. The environment variable still takes precedence.

Call `mssql_python.get_native_provider_info()` to inspect the selected provider, selection source, and whether selection is fixed. Before connecting, this reports the selection, not proof that the native driver has loaded successfully.

To go back, set the variable to `msodbcsql18`, or unset it and remove any
`native_provider` assignment from your code (or set it to `"msodbcsql18"`).
Then restart the process.

## What works today

The `mssql-odbc` driver implements every ODBC function that `mssql-python` calls, so most
applications can switch providers without code changes. Check
[Known issues](#known-issues) for the remaining differences.

| Area | Notes |
|---|---|
| Connection lifecycle | Connect, `close()`, `with` blocks, login timeout, diagnostics, and changing the current database. Connection-string settings include `ApplicationIntent`, `MultiSubnetFailover`, `IpAddressPreference`, `PacketSize`, `KeepAlive`, and `ConnectRetryCount` / `ConnectRetryInterval` (idle connection resiliency). |
| Authentication | SQL authentication (`UID` / `PWD`); Windows authentication (`Trusted_Connection=Yes`, using SSPI on Windows or Kerberos on Linux and macOS); and Microsoft Entra interactive, device code, service principal, managed identity, default credential, and access token. Entra integrated and password authentication aren't supported; see Known issues. |
| TLS | `Encrypt`, `TrustServerCertificate`, `HostnameInCertificate`, and `ServerCertificate`. |
| Query execution | Direct and prepared execution, prepared-plan reuse, `qmark` parameters, and the `pyformat` conversion `mssql-python` performs. |
| ODBC escape syntax | Function, date/time, outer-join, GUID, `LIKE ... {escape ...}`, interval, and `{encrypt ...}` escape forms. |
| Stored procedures | Call them with T-SQL or positional `{call ...}` syntax through `execute()`, read result sets, and advance with `nextset()`. |
| Input parameters | Scalar parameters, SQL `NULL`, `setinputsizes()`, and mixed regular/streamed parameters. |
| Large parameters | Large text and binary parameters stream to the server on the ordinary binding path; XML and UDT parameter limitations are listed below. |
| `executemany()` | Batched with ODBC parameter arrays. Rows containing streamed values fall back to row-by-row execution. |
| Row fetching | `fetchone()`, `fetchmany()`, `fetchall()`, iteration, `fetchval()`, typed conversion, and large text/binary streaming. |
| Arrow fetching | `arrow_batch()`, `arrow()`, and `arrow_reader()`. Requires `pyarrow`. |
| Multiple result sets | `nextset()`, including results produced by parameter arrays. |
| Transactions | Autocommit, commit, rollback, the automatic commit (or rollback, if an error escaped) that happens when a `with connect(...)` block ends, and the read-uncommitted, read-committed, repeatable-read, serializable, and snapshot isolation levels. |
| Query timeout | Set `Connection.timeout` (in seconds) to bound statements run by that connection's cursors; expiry raises `OperationalError`. `0` means no timeout. This setting is separate from the login timeout passed as `connect(timeout=...)`. |
| Connection pooling | The built-in `mssql-python` pool, including connection reset on checkout and separate pools per Microsoft Entra identity. |
| Metadata APIs | `tables()`, `columns()`, `primaryKeys()`, `foreignKeys()`, `statistics()`, `rowIdColumns()`, `rowVerColumns()`, `procedures()`, and `getTypeInfo()`. |
| Encoding and converters | Default wide-character encoding and output converters. Custom narrow-character decoding and non-Unicode parameter conversion have limitations listed in [Known issues](#known-issues). |

### Data types

You can read all SQL Server data types, and the driver supports every parameter
type that `mssql-python` binds. The driver returns `xml`, `json`, and `vector`
values as `str` (`vector` as a JSON array), and CLR types such as `geometry`,
`geography`, and `hierarchyid` as `bytes`. For other Python mappings, see
[Data type mappings](data-type-mappings.md).
For type-specific limitations, see [Known issues](#known-issues).

## Known issues

Check these differences before switching a workload from `msodbcsql18`.

| Issue | Details | Workaround |
|---|---|---|
| **Microsoft Entra integrated authentication** | `Authentication=ActiveDirectoryIntegrated` isn't supported. Windows authentication (`Trusted_Connection=Yes`) is unaffected. | Use another Microsoft Entra method, or keep `msodbcsql18`. |
| **Microsoft Entra password authentication** | `Authentication=ActiveDirectoryPassword` isn't supported, and support isn't planned because this method can't satisfy MFA. SQL authentication (`UID` / `PWD`) is unaffected. | Use another Microsoft Entra method, such as a service principal, managed identity, or access token. |
| **`attrs_before` settings** | Authentication, application intent, and multi-subnet failover can't be set as numeric attributes. Statement attributes other than query timeout are rejected. | Use the equivalent connection-string keywords. |
| **Connection-operation timeout** | `set_attr(SQL_ATTR_CONNECTION_TIMEOUT, ...)` accepts and stores the value, but it doesn't enforce a timeout in 0.3.0. | Use `connect(timeout=...)` for login and `Connection.timeout` for statements; neither is a general connection-operation deadline. |
| **Multi-address hosts** | `msodbcsql18` enables Transparent Network IP Resolution (TNIR) by default; `mssql-odbc` doesn't support it. `mssql-python` rejects the `TransparentNetworkIPResolution` connection-string keyword with either provider. Without `MultiSubnetFailover=Yes`, addresses are tried one at a time, so an unreachable address can delay the connection. Server-assisted DNS caching and enhanced routing aren't negotiated. | Set `MultiSubnetFailover=Yes` for availability group listeners and other multi-address hosts. |
| **Transient DNS failures** | If a DNS lookup fails, the driver doesn't fall back to the last successful address. ([#457](https://github.com/microsoft/mssql-rs/issues/457)) | Retry the connection. |
| **Data classification** | Sensitivity classification metadata isn't available, and `SQL_COPT_SS_DATACLASSIFICATION_VERSION` (`1400`) isn't supported. | Query `sys.sensitivity_classifications`. |
| **Multithreading** | Multithreaded use hasn't been validated. | Keep `msodbcsql18` for multithreaded workloads. |
| **`fork()` after connecting** | On Linux, a process forked after the parent connects can hang when it uses the driver, even with a new connection. With pooling enabled, the parent can hang too. ([#566](https://github.com/microsoft/mssql-rs/issues/566)) | Use the `spawn` start method and connect inside each worker. |
| **Canceling a running statement** | A running query or fetch can't be interrupted. For example, closing an `arrow_reader()` doesn't stop a read in progress on another thread. | Use `Connection.timeout` to limit long-running statements. |
| **Unread results** | Closing a cursor or running another statement reads and discards remaining results instead of canceling them, which can be slow. Closing a cursor that used a prepared statement ignores `Connection.timeout` and can hang if the server stops responding. ([#546](https://github.com/microsoft/mssql-rs/issues/546)) | Fetch only the rows you need, for example with `TOP`. |
| **Large `xml` and UDT parameters** | Values bound as `SQL_SS_XML` or `SQL_SS_UDT` through `setinputsizes()` are buffered in memory instead of streamed. | Bind `xml` values as ordinary strings. |
| **Empty `image` values** | An empty, non-NULL SQL Server `image` value is returned as `None` instead of `b""`. | Read `CAST(image_column AS varbinary(max))`, or use `msodbcsql18` when the empty/NULL distinction matters. |
| **Non-Unicode string parameters** | Strings bound as `char` / `varchar` through `setinputsizes()` can be encoded incorrectly for code page 437, code page 850, and UTF-8 collations. | Use the default string binding. |
| **Legacy code-page decoding on Windows** | With `setdecoding(SQL_CHAR, encoding="cp1252")` or another legacy code page, text can be decoded incorrectly or returned as `bytes`, because the driver returns `char` / `varchar` data as UTF-8. | Use the default: `conn.setdecoding(mssql_python.SQL_CHAR)`. |
| **`Connection.getinfo()`** | `SQL_USER_NAME` returns the connection-string `UID`, not the database user. It's empty when no `UID` reaches the driver. Some capability values and packet-size-based limits differ from `msodbcsql18`. | Use `SELECT USER_NAME()` for the database user. |
| **Out-of-memory failures** | Some driver allocation failures end the Python process instead of raising `MemoryError`. ([#484](https://github.com/microsoft/mssql-rs/issues/484)) | Limit the size of values you send and fetch. |
| **Large-value read performance** | In one benchmark, chunked reads of `varchar(max)` / `nvarchar(max)` data were slightly slower than with `msodbcsql18`. ([#473](https://github.com/microsoft/mssql-rs/issues/473)) | Benchmark your workload. |

This list isn't exhaustive.

## Reporting problems

If you encounter a bug, select `msodbcsql18`, restart the process, and check whether
the behavior still occurs. Also remove any conflicting Python-side provider
assignment. The comparison helps isolate the problem; it doesn't by itself
prove its cause. Include the output of
`mssql_python.get_native_provider_info()`, your platform, and the failing SQL or
error message:

- Rust driver issues: <https://github.com/microsoft/mssql-rs/issues>
- `mssql-python` issues: <https://github.com/microsoft/mssql-python/issues>

For Rust-driver diagnostics, set these variables before starting Python and
reproduce the problem with `mssql-odbc` selected. For example, in PowerShell:

```powershell
$env:MSSQL_TDS_TRACE = "true"
$env:MSSQL_TDS_TRACE_LEVEL = "warn,mssqlodbc=debug"
$env:MSSQL_TDS_TRACE_DIR = "./traces"
```

`MSSQL_TDS_TRACE` enables tracing, `MSSQL_TDS_TRACE_LEVEL` selects the log
filter, and `MSSQL_TDS_TRACE_DIR` writes per-process files instead of stderr.
Restart Python after changing these settings. Traces can contain SQL text and
parameter values. Review and redact sensitive data before sharing relevant
excerpts, and disable tracing after diagnosis.

## Related content

- [Install mssql-python](installation.md)
- [Connection strings](connection-strings.md)
- [Microsoft Entra authentication](entra-authentication.md)
- [Data type mappings](data-type-mappings.md)
- [Troubleshooting](troubleshooting.md)
