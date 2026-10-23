---
title: Programming Guidelines
description: Learn how to choose a driver version, build an ODBC application, and handle character data with the Microsoft ODBC Driver for SQL Server on Linux or macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 10/19/2026
ms.service: sql
ms.subservice: connectivity
ms.custom: linux-related-content
ms.topic: concept-article
ai-usage: ai-assisted
---
# Programming guidelines

[!INCLUDE [Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article describes programming considerations for applications that use the Microsoft ODBC Driver for SQL Server on Linux or macOS. Some links point to SQL Server Native Client material. Those pages provide historical API context.

## Choose a driver version

Use Microsoft ODBC Driver 18 for SQL Server, the current driver line, for new development. Microsoft ODBC Driver 17 for SQL Server remains supported for applications that already deploy it, as described in the [support lifecycle](../support-lifecycle.md). ODBC Driver 13, which shipped with SQL Server 2017, is eligible for extended support through October 12, 2027, only under an extended support agreement.

Specify the installed driver name in your connection string, enclosed in braces:

```text
Driver={ODBC Driver 18 for SQL Server}
```

```text
Driver={ODBC Driver 17 for SQL Server}
```

In a data source name (DSN) in `odbc.ini`, use the name without braces, for example `Driver = ODBC Driver 18 for SQL Server`. For DSN syntax, see [Connection string keywords and data source names (DSNs)](connection-string-keywords-and-data-source-names-dsns.md).

For version-specific connection settings, see [Major version differences](../major-version-differences.md). For support dates and operating system compatibility, see [Support lifecycle](../support-lifecycle.md) and [System requirements](system-requirements.md).

## Build an application

For header locations, include order, compiler commands, library names, and a C++ sample, see [Develop C and C++ applications with the ODBC driver](../develop-cpp-applications.md) and [Connect and query with C++ and ODBC](../cpp-code-example-app-connect-access-sql-db.md).

An ODBC application can use Multiple Active Result Sets (MARS) and other SQL Server-specific features by including `msodbcsql.h` after the unixODBC headers (`sql.h`, `sqlext.h`, `sqltypes.h`, and `sqlucode.h`). Use the same symbolic names for SQL Server-specific items that you use in Windows ODBC applications.

<a id="available-features"></a>

## Find driver capabilities

Start with the [ODBC Programmer's Reference](../../../odbc/reference/odbc-programmer-s-reference.md) for the standard application programming interface (API). Then use the current driver documentation for driver-specific behavior.

The [Microsoft ODBC Driver for SQL Server](../microsoft-odbc-driver-for-sql-server.md) overview links to the driver documentation. The following SQL Server Native Client articles provide API context for applications that use the driver on Linux and macOS:

- [Communicating with SQL Server (ODBC)](../../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)
- [Connection and query timeout support](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)
- [Cursors](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)
- [Date/time improvements (ODBC)](../../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)
- [Executing queries (ODBC)](../../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)
- [Handling errors and messages](../../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)
- [Kerberos authentication](../../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md)
- [Large common language runtime (CLR) user-defined types (ODBC)](../../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md)
- [Performing transactions (ODBC)](../../../relational-databases/native-client/odbc/performing-transactions-in-odbc.md). For distributed transactions on Linux and macOS, use [XA transactions with DTC](../use-xa-with-dtc.md).
- [Processing results (ODBC)](../../../relational-databases/native-client-odbc-results/processing-results-odbc.md)
- [Running stored procedures](../../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)
- [Sparse columns support (ODBC)](../../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md)
- [Using encryption without validation](../../../relational-databases/native-client/features/using-encryption-without-validation.md)
- [Table-valued parameters](../../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)
- [UTF-8 and UTF-16 for the command and data API](../../../relational-databases/native-client/features/utf-16-support-in-sql-server-native-client-11-0.md)
- [Using catalog functions](../../../relational-databases/native-client/odbc/using-catalog-functions.md)

<a id="unsupported-features"></a>

## Limitations

The following features haven't been verified to work correctly in the ODBC driver on Linux and macOS:

- Failover cluster connection.
- [Transparent Network IP Resolution](../using-transparent-network-ip-resolution.md) before ODBC Driver 17.
- [Linux and macOS ODBC Driver Built-In Diagnostics (BID) tracing](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/Collect-a-SQL-Driver-BID-Trace#linux-and-mac-odbc-driver-bid-tracing) before ODBC Driver 17.3.

### Unsupported features

The following features aren't available in the ODBC driver on Linux and macOS:

- Distributed transactions through the `SQL_ATTR_ENLIST_IN_DTC` attribute. The driver supports XA transactions through the Microsoft Distributed Transaction Coordinator (DTC). For more information, see [Use XA transactions with DTC](../use-xa-with-dtc.md).
- Database mirroring.
- FILESTREAM.
- Profiling ODBC driver performance, discussed in [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), and these performance-related connection attributes:

  - `SQL_COPT_SS_PERF_DATA`
  - `SQL_COPT_SS_PERF_DATA_LOG`
  - `SQL_COPT_SS_PERF_DATA_LOG_NOW`
  - `SQL_COPT_SS_PERF_QUERY`
  - `SQL_COPT_SS_PERF_QUERY_INTERVAL`
  - `SQL_COPT_SS_PERF_QUERY_LOG`

- `SQLBrowseConnect` before version 17.2.
- C interval types such as `SQL_C_INTERVAL_YEAR_TO_MONTH`, documented in [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md).
- The `SQL_CUR_USE_ODBC` value of the `SQL_ATTR_ODBC_CURSORS` attribute of `SQLSetConnectAttr`.

<a id="character-set-support"></a>

## Handle character data

On Linux and macOS, use UTF-16 `SQLWCHAR` buffers with ODBC wide-character APIs instead of the native `wchar_t` type. The driver interface uses 16-bit code units.

### Character encodings

For ODBC Driver 13 and 13.1, you must use UTF-8 for `SQLCHAR` data. These versions don't support any other encodings.

Starting with ODBC Driver 17, supported character sets and encodings for `SQLCHAR` data include:

> [!NOTE]
> Due to `iconv` differences between `musl` and `glibc`, many of these locales aren't supported on Alpine Linux. For more information, see [Functional differences from glibc](https://wiki.musl-libc.org/functional-differences-from-glibc.html).

| Name | Description |
| --- | --- |
| UTF-8 | Unicode |
| CP437 | MS-DOS Latin US |
| CP850 | MS-DOS Latin 1 |
| CP874 | Latin/Thai |
| CP932 | Japanese (Windows code page 932) |
| CP936 | Simplified Chinese, GBK |
| CP949 | Korean (Windows code page 949) |
| CP950 | Traditional Chinese, Big5 |
| CP1250 | Central European |
| CP1251 | Cyrillic |
| CP1252 | Western European |
| CP1253 | Greek |
| CP1254 | Turkish |
| CP1255 | Hebrew |
| CP1256 | Arabic |
| CP1257 | Baltic |
| CP1258 | Vietnamese |
| ISO-8859-1 | Latin-1 |
| ISO-8859-2 | Latin-2 |
| ISO-8859-3 | Latin-3 |
| ISO-8859-4 | Latin-4 |
| ISO-8859-5 | Latin/Cyrillic |
| ISO-8859-6 | Latin/Arabic |
| ISO-8859-7 | Latin/Greek |
| ISO-8859-8 | Hebrew |
| ISO-8859-9 | Turkish |
| ISO-8859-13 | Latin-7 |
| ISO-8859-15 | Latin-9 |

When you connect, the driver detects the current locale of the process it's loaded in. If the locale uses one of the listed encodings, the driver uses that encoding for `SQLCHAR` (narrow-character) data. Otherwise, it defaults to UTF-8. Processes start in the `C` locale by default, which causes the driver to default to UTF-8. If an application needs one of the listed encodings, call `setlocale` before connecting. Specify the locale explicitly, or use an empty string (for example, `setlocale(LC_ALL, "")`) to use the locale settings of the environment.

In a typical Linux or macOS environment where the encoding is UTF-8, users of ODBC Driver 17 upgrading from 13 or 13.1 won't observe differences. Applications that use a listed non-UTF-8 encoding through `setlocale` need to use that encoding for data to and from the driver instead of UTF-8.

You must use UTF-16LE (little endian) for `SQLWCHAR` data.

When you bind input parameters by using `SQLBindParameter`, if you specify a narrow character SQL type such as `SQL_VARCHAR`, the driver converts the supplied data from the client encoding to the server-side encoding for that parameter. The encoding depends on the collation; it isn't always code page 1252. For output parameters, the driver converts from the encoding specified in the collation information associated with the data to the client encoding. Characters in the source encoding that aren't representable in the target encoding convert to a question mark (`?`).

To avoid this data loss when binding input parameters, specify a Unicode SQL character type such as `SQL_WVARCHAR`. The driver converts the client encoding to UTF-16, which can represent all Unicode characters. The target column or parameter must also be a Unicode type (**nchar**, **nvarchar**, or **ntext**) or a type with a collation or encoding that can represent all the original source data. For output parameters, specify a Unicode SQL type and either a Unicode C type (`SQL_C_WCHAR`), which causes the driver to return data as UTF-16, or a narrow C type with a client encoding that can represent the source data. UTF-8 can always represent this data.

For more information about collations and encodings, see [Collation and Unicode support](../../../relational-databases/collations/collation-and-unicode-support.md).

There are conversion differences between Windows and Linux or macOS `iconv` versions:

- For code page 1255, byte `0xCA` converts to UTF-16 code point `0x05BA` on Windows and `0x00CA` on macOS and Linux with `libiconv` versions earlier than 1.15.
- Linux `iconv` libraries without the 2003 revision of Big5/CP950 (`BIG5-2003`) don't convert characters added in that revision correctly.
- For code page 932 (Japanese, Shift-JIS), decoding characters not defined in the encoding standard differs. For example, byte `0x80` converts to U+0080 on Windows and might become U+30FB on Linux and macOS, depending on the `iconv` version.

In ODBC Driver 13 and 13.1, splitting UTF-8 multibyte characters or UTF-16 surrogates across `SQLPutData` buffers results in data corruption. Use streaming buffers that don't end in partial character encodings. This limitation was removed in ODBC Driver 17.

For character-encoding, conversion, parameter-binding, and buffer-sizing guidance, see [Known issues](known-issues-in-this-version-of-the-driver.md).

## <a id="bkmk-openssl"></a> OpenSSL

Starting with ODBC Driver 17.4, the driver loads OpenSSL dynamically, which allows it to run on systems that have version 1.0 or 1.1 without separate driver files. Starting with version 17.9, the driver supports OpenSSL 3.0 in addition to the previous versions. When multiple versions of OpenSSL are present, the driver attempts to load a supported version from its library search paths.

| Driver version | Supported OpenSSL versions |
| --- | --- |
| 17.4 through 17.8 | 1.0, 1.1 |
| 17.9 and later 17.x versions, and all Driver 18 versions | 1.0, 1.1, 3.0 |

OpenSSL 4 isn't supported. On macOS, a Homebrew `openssl` link that points to OpenSSL 4 can prevent the driver from loading OpenSSL even when OpenSSL 3 is installed. Check the library path as well as the installed versions.

> [!NOTE]
> A potential conflict might occur if the application that uses the driver, or one of its components, links with or dynamically loads a different version of OpenSSL. If several versions of OpenSSL are present on the system and the application uses it, ensure that the version loaded by the application and the driver don't mismatch. The errors could corrupt memory and might not manifest in obvious or consistent ways.

## <a id="bkmk-alpine"></a> Alpine Linux

The default stack size for new threads in `musl` is 128 KB unless the executable requests a larger default. A thread can exceed this limit when it calls the driver. Link an ODBC application on Alpine Linux with `-Wl,-z,stack-size=<size_in_bytes>` to request a larger default thread stack. Applications that create threads with an explicit stack size must also choose an adequate size for those threads.

The main thread's stack and the default stack for new threads aren't the same setting. On `glibc` systems, the default for new threads normally follows the process's stack resource limit, with an architecture-specific fallback when that limit is unlimited. Don't assume a fixed 2 MB default across Linux systems.

<a id="additional-notes"></a>

## Additional notes

To make a dedicated administrator connection (DAC) from an ODBC application, set `Server=tcp:<server>,<dac_port>` in your connection string. Use the discovered DAC port, not the instance's regular connection port. For example, if the DAC port is 33000, set `Server=tcp:<server>,33000`. These connection-string fragments specify only the endpoint, not authentication. Configure authentication and server-certificate validation separately.

The login must be a member of the `sysadmin` server role. Remote access requires the server's remote admin connections option to be enabled. For access requirements, server configuration, and command-line administration procedures, see [Diagnostic connection for database administrators](../../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md). For port discovery, see [DAC port](../../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md#dac-port).

The unixODBC driver manager handles statement attributes passed through `SQLSetConnectAttr` the same way as the Windows Driver Manager. ODBC 2.x statement options, such as `SQL_ATTR_QUERY_TIMEOUT` and `SQL_ATTR_MAX_ROWS`, apply to existing and new statements on the connection. ODBC 3.x statement-only attributes, such as `SQL_ATTR_ROW_ARRAY_SIZE` and `SQL_ATTR_PARAMSET_SIZE`, return SQLSTATE HY092. Attributes defined at both the connection and statement levels, such as `SQL_ATTR_METADATA_ID`, can be set at either level. Setting statement-only attributes at the connection level is deprecated in ODBC 3.x, so call `SQLSetStmtAttr` instead. For more information, see [SQLSetConnectAttr function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md).

In highly multithreaded applications, unixODBC handle validation might become a performance bottleneck. Compiling unixODBC with `--enable-fastvalidate` might improve performance. However, this option might cause applications that pass invalid handles to ODBC APIs to crash instead of returning `SQL_INVALID_HANDLE`.

For platform installation and configuration steps, see:

- [Install the ODBC driver on Linux](installing-the-microsoft-odbc-driver-for-sql-server.md).
- [Install the ODBC driver on macOS](install-microsoft-odbc-driver-sql-server-macos.md).
- [Install the unixODBC driver manager](installing-the-driver-manager.md).
- [Configure connection strings and DSNs](connection-string-keywords-and-data-source-names-dsns.md).
- [Review system requirements](system-requirements.md).

## Related content

- [Frequently asked questions for ODBC on Linux and macOS](frequently-asked-questions-faq-for-odbc-linux.yml)
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md)
