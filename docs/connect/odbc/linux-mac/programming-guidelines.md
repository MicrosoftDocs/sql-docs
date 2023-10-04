---
title: Programming guidelines
description: The programming features of the ODBC Driver for SQL Server on macOS and Linux contain some differences from the Windows versions.
author: v-makouz
ms.author: v-makouz
ms.reviewer: v-davidengel
ms.date: 02/17/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Programming Guidelines

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The programming features of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on macOS and Linux are based on ODBC in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ([SQL Server Native Client (ODBC)](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is based on ODBC in Windows Data Access Components ([ODBC Programmer's Reference](../../../odbc/reference/odbc-programmer-s-reference.md)).

An ODBC application can use Multiple Active Result Sets (MARS) and other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] specific features by including `/usr/local/include/msodbcsql.h` after including the unixODBC headers (`sql.h`, `sqlext.h`, `sqltypes.h`, and `sqlucode.h`). Then use the same symbolic names for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific items that you would use in your Windows ODBC applications.

## Available Features

The following sections from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client documentation for ODBC ([SQL Server Native Client (ODBC)](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)) are valid when using the ODBC driver on macOS and Linux:

- [Communicating with SQL Server (ODBC)](../../../relational-databases/native-client-odbc-communication/communicating-with-sql-server-odbc.md)
- [Connection and query timeout support](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)
- [Cursors](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)
- [Date/Time Improvements (ODBC)](../../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)
- [Executing Queries (ODBC)](../../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)
- [Handling Errors and Messages](../../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)
- [Kerberos authentication](../../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md)
- [Large CLR User-Defined Types (ODBC)](../../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md)
- [Performing Transactions (ODBC) (except distributed transactions)](../../../relational-databases/native-client/odbc/performing-transactions-in-odbc.md)
- [Processing Results (ODBC)](../../../relational-databases/native-client-odbc-results/processing-results-odbc.md)
- [Running Stored Procedures](../../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)
- [Sparse Columns Support (ODBC)](../../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md)
- [Using Encryption Without Validation](../../../relational-databases/native-client/features/using-encryption-without-validation.md)
- [Table Valued Parameters](../../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)
- [UTF-8 and UTF-16 for command and data API](../../../relational-databases/native-client/features/utf-16-support-in-sql-server-native-client-11-0.md)
- [Using Catalog Functions](../../../relational-databases/native-client/odbc/using-catalog-functions.md)

## Unsupported Features

The following features haven't been verified to work correctly in the ODBC driver on macOS and Linux:

- Failover Cluster Connection
- [Transparent Network IP Resolution](../using-transparent-network-ip-resolution.md) (before ODBC Driver 17)
- [Linux and macOS ODBC Driver BID Tracing](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/Collect-a-SQL-Driver-BID-Trace#linux-and-mac-odbc-driver-bid-tracing) (before ODBC Driver 17.3)

The following features aren't available in the ODBC driver on macOS and Linux:

- Distributed Transactions (SQL_ATTR_ENLIST_IN_DTC attribute isn't supported)
- Database Mirroring
- FILESTREAM
- Profiling ODBC driver performance, discussed in [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), and the following performance-related connection attributes:
  - SQL_COPT_SS_PERF_DATA
  - SQL_COPT_SS_PERF_DATA_LOG
  - SQL_COPT_SS_PERF_DATA_LOG_NOW
  - SQL_COPT_SS_PERF_QUERY
  - SQL_COPT_SS_PERF_QUERY_INTERVAL
  - SQL_COPT_SS_PERF_QUERY_LOG
- SQLBrowseConnect (before version 17.2)
- C interval types such as SQL_C_INTERVAL_YEAR_TO_MONTH (documented in [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md))
- The SQL_CUR_USE_ODBC value of the SQL_ATTR_ODBC_CURSORS attribute of the SQLSetConnectAttr function.

## Character Set Support

For ODBC Driver 13 and 13.1, SQLCHAR data must be UTF-8. No other encodings are supported.

For ODBC Driver 17, SQLCHAR data in one of the following character sets/encodings is supported:

> [!NOTE]
> Due to `iconv` differences in `musl` and `glibc` many of these locales are not supported on Alpine Linux.
>
> For more information, see [Functional differences from glibc](https://wiki.musl-libc.org/functional-differences-from-glibc.html)

| Name                | Description               |
|---------------------|---------------------------|
| UTF-8               | Unicode                   |
| CP437               | MS-DOS Latin US           |
| CP850               | MS-DOS Latin 1            |
| CP874               | Latin/Thai                |
| CP932               | Japanese, Shift-JIS       |
| CP936               | Simplified Chinese, GBK   |
| CP949               | Korean, EUC-KR            |
| CP950               | Traditional Chinese, Big5 |
| CP1251              | Cyrillic                  |
| CP1253              | Greek                     |
| CP1256              | Arabic                    |
| CP1257              | Baltic                    |
| CP1258              | Vietnamese                |
| ISO-8859-1 / CP1252 | Latin-1                   |
| ISO-8859-2 / CP1250 | Latin-2                   |
| ISO-8859-3          | Latin-3                   |
| ISO-8859-4          | Latin-4                   |
| ISO-8859-5          | Latin/Cyrillic            |
| ISO-8859-6          | Latin/Arabic              |
| ISO-8859-7          | Latin/Greek               |
| ISO-8859-8 / CP1255 | Hebrew                    |
| ISO-8859-9 / CP1254 | Turkish                   |
| ISO-8859-13         | Latin-7                   |
| ISO-8859-15         | Latin-9                   |

Upon connection, the driver detects the current locale of the process it's loaded in. If it uses one of the encodings above, the driver uses that encoding for SQLCHAR (narrow-character) data; otherwise, it defaults to UTF-8. Since all processes start in the "C" locale by default (and cause the driver to default to UTF-8), if an application needs to use one of the encodings above, it should use the **setlocale** function to set the locale appropriately before connecting; either by specifying the locale explicitly, or using an empty string for example `setlocale(LC_ALL, "")` to use the locale settings of the environment.

Therefore, in a typical Linux or macOS environment where the encoding is UTF-8, users of ODBC Driver 17 upgrading from 13 or 13.1 won't observe any differences. However, applications that use a non-UTF-8 encoding in the above list via `setlocale()` need to use that encoding for data to/from the driver instead of UTF-8.

SQLWCHAR data must be UTF-16LE (Little Endian).

When binding input parameters with SQLBindParameter, if a narrow character SQL type such as SQL_VARCHAR is specified, the driver converts the supplied data from the client encoding to the default (typically codepage 1252) [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encoding. For output parameters, the driver converts from the encoding specified in the collation information associated with the data to the client encoding. However, data loss is possible --- characters in the source encoding not representable in the target encoding will convert to a question mark ('?').

To avoid this data loss when binding input parameters, specify a Unicode SQL character type such as SQL_NVARCHAR. In this case, the driver converts from the client encoding to UTF-16, which can represent all Unicode characters. Furthermore, the target column or parameter on the server must also be either a Unicode type (**nchar**, **nvarchar**, **ntext**) or one with a collation/encoding, which can represent all the characters of the original source data. For avoiding data loss with output parameters, specify a Unicode SQL type, and either a Unicode C type (SQL_C_WCHAR), causing the driver to return data as UTF-16, or a narrow C type, and ensure that the client encoding can represent all the characters of the source data (this representation is always possible with UTF-8.)

For more information about collations and encodings, see [Collation and Unicode Support](../../../relational-databases/collations/collation-and-unicode-support.md).

There are some encoding conversion differences between Windows and several versions of the iconv library on Linux and macOS. Text data in codepage 1255 (Hebrew) has one code point (0xCA) that behaves differently upon conversion to Unicode. On Windows, this character converts to the UTF-16 code point of 0x05BA. On macOS and Linux with libiconv versions earlier than 1.15, it converts to 0x00CA. On Linux with iconv libraries that don't support the 2003 revision of Big5/CP950 (named `BIG5-2003`), characters added with that revision won't convert correctly. In codepage 932 (Japanese, Shift-JIS), the result of decoding of characters not originally defined in the encoding standard also differs. For example, the byte 0x80 converts to U+0080 on Windows but may become U+30FB on Linux and macOS, depending on iconv version.

In ODBC Driver 13 and 13.1, when UTF-8 multibyte characters or UTF-16 surrogates are split across SQLPutData buffers, it results in data corruption. Use buffers for streaming SQLPutData that don't end in partial character encodings. This limitation has been removed with ODBC Driver 17.

## <a name="bkmk-openssl"></a>OpenSSL

Starting with version 17.4, the driver loads OpenSSL dynamically, which allows it to run on systems that have either version 1.0 or 1.1 without a need for separate driver files. Starting with version 17.9, the driver supports OpenSSL 3.0 in addition to the previous versions. When multiple versions of OpenSSL are present, the driver will attempt to load the latest one.

| Driver version      | Supported OpenSSL versions |
|---------------------|----------------------------|
| 17.4+               | 1.0, 1.1                   |
| 17.9, 18.0+         | 1.0, 1.1, 3.0              |


> [!NOTE]
> A potential conflict may occur if the application that uses the driver (or one of its components) is linked with or dynamically loads a different version of OpenSSL. If several versions of OpenSSL are present on the system and the application uses it, it is highly recommended that one be extra careful in making sure that the version loaded by the application and the driver do not mismatch, as the errors could corrupt memory and thus will not necessarily manifest in obvious or consistent ways.

## <a name="bkmk-alpine"></a>Alpine Linux

At the time of this writing the default stack size in MUSL is 128K, which is enough for basic ODBC driver functionality, however depending on what the application does it isn't difficult to exceed this limit, especially when calling the driver from multiple threads. It's recommended that an ODBC application on Alpine Linux is compiled with `-Wl,-z,stack-size=<VALUE IN BYTES>` to increase the stack size. For reference, the default stack size on most GLIBC systems is 2MB.

## Additional Notes

* You can make a dedicated administrator connection (DAC) using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication and **host,port**. A member of the Sysadmin role first needs to discover the DAC port. See [Diagnostic Connection for Database Administrators](../../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md#dac-port) to discover how. For example, if the DAC port were 33000, you could connect to it with `sqlcmd` as follows:


    ```bash
    sqlcmd -U <user> -P <pwd> -S <host>,33000
    ```

    > [!NOTE]
    > DAC connections must use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.

* The UnixODBC driver manager returns "invalid attribute/option identifier" for all statement attributes when they're passed through SQLSetConnectAttr. On Windows, when SQLSetConnectAttr receives a statement attribute value, it causes the driver to set that value on all active statements, which are children of the connection handle.


* When using the driver with highly multithreaded applications, unixODBC's handle validation may become a performance bottleneck. In such scenarios, higher performance may be obtained by compiling unixODBC with the `--enable-fastvalidate` option. However, beware that this option may cause applications that pass invalid handles to ODBC APIs to crash instead of returning `SQL_INVALID_HANDLE` errors.


## See Also

[Frequently Asked Questions](frequently-asked-questions-faq-for-odbc-linux.yml)  
[Known Issues in this Version of the Driver](known-issues-in-this-version-of-the-driver.md)  
[Release Notes](release-notes-odbc-sql-server-linux-mac.md)  
