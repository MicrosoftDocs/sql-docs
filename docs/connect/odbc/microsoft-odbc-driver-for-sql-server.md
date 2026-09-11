---
title: Microsoft ODBC Driver for SQL Server
description: The Microsoft ODBC Driver for SQL Server connects C and C++ applications to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, mcimfl
ms.date: 09/03/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft ODBC Driver for SQL Server

[!INCLUDE [Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

ODBC is the primary native data access API for applications written in C and C++ for SQL Server. The Microsoft ODBC Driver for SQL Server connects to SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, and SQL database in Microsoft Fabric. For the database versions each driver release supports, see [SQL version compatibility](windows/system-requirements-installation-and-driver-files.md#sql-version-compatibility).

Other languages that can use ODBC include COBOL, Perl, PHP, and Python. ODBC is widely used in data integration scenarios, and the [Microsoft Drivers for PHP for SQL Server](../php/microsoft-php-driver-for-sql-server.md) are built on this driver.

The **sqlcmd** and **bcp** utilities work with this driver, but they install separately: the `mssql-tools18` package on Linux and macOS, and the Microsoft Command Line Utilities on Windows. Use [**sqlcmd**](../../tools/sqlcmd/sqlcmd-utility.md) to run Transact-SQL (T-SQL) statements, system procedures, and script files. Use [**bcp**](../../tools/bcp/bcp-utility.md) to bulk copy data between an instance of SQL Server and a data file, in either direction.

## Choose your starting point

- To install the driver, go to [System requirements, installation, and driver files](windows/system-requirements-installation-and-driver-files.md) for Windows, or [Install the ODBC driver on Linux](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), [Install the ODBC driver on macOS](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md), and [Install the unixODBC driver manager](linux-mac/installing-the-driver-manager.md).
- To write your first application, go to [Connect to and query a database with C++](cpp-code-example-app-connect-access-sql-db.md) and [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md).
- To connect to Azure SQL with passwordless authentication, go to [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).
- To make an existing app resilient to transient failures, go to [Connection resiliency](connection-resiliency.md) and [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).
- To upgrade from version 17, go to [Major version differences](major-version-differences.md) and [Connection encryption troubleshooting](connection-troubleshooting.md).
- To diagnose a connection or query problem, go to [Connection encryption troubleshooting](connection-troubleshooting.md) and [Known issues (Linux and macOS)](linux-mac/known-issues-in-this-version-of-the-driver.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL connection. It loads the server name and database name from application configuration, authenticates with a managed identity so that no secret appears in the connection string, and enables Tabular Data Stream (TDS) 8.0 encryption with full certificate validation. It sets a per-attempt login timeout, and retries transient failures with exponential backoff and jitter.

The C++ snippet in this article omits includes, handle allocation, and the logging helper for brevity.

```cpp
std::wstring BuildConnectionString(const wchar_t* server, const wchar_t* database) {
    std::wstring cs = L"Driver={ODBC Driver 18 for SQL Server}";
    cs += L";Server=tcp:"; cs += server; cs += L",1433";
    cs += L";Database="; cs += database;
    cs += L";Authentication=ActiveDirectoryMsi";   // managed identity, no stored secret
    cs += L";Encrypt=strict";                      // TDS 8.0 with certificate validation
    cs += L";ConnectRetryCount=3";                 // idle connection resiliency, not initial connect
    cs += L";ConnectRetryInterval=10";
    return cs;
}

// Transient fault codes documented for Azure SQL, plus the resource governance
// codes. Network termination and timeout errors (64, 233, 258, 10053, 10054,
// 10060) are retried a bounded number of times, which is the documented
// guidance for them. 258 is the code the driver reports for a connect timeout.
// 10053 and 10054 can also mean the encryption handshake failed rather than a
// plain network reset, so read the error text before assuming a network fault.
bool IsTransient(SQLINTEGER nativeError) {
    switch (nativeError) {
        case 615: case 926: case 4060: case 4221:
        case 10928: case 10929: case 10936:
        case 40197: case 40501: case 40613:
        case 42108: case 42109:
        case 49918: case 49919: case 49920:
        case 40020: case 40143: case 40166: case 40540:   // failover subcodes
        case 64: case 233: case 258:
        case 10053: case 10054: case 10060:
            return true;
        default:
            return false;
    }
}

// Retries only errors that a new connection can clear, with exponential backoff
// plus jitter so that concurrent clients don't retry in lockstep.
SQLRETURN ConnectWithRetry(SQLHDBC hDbc, const std::wstring& connectionString, int maxAttempts) {
    SQLRETURN rc = SQL_ERROR;
    for (int attempt = 1; attempt <= maxAttempts; ++attempt) {
        // Set the per-attempt connect timeout through the connection attribute.
        // This works on every driver version, so the sample doesn't depend on
        // which connection string keywords a given release accepts.
        SQLSetConnectAttrW(hDbc, SQL_ATTR_LOGIN_TIMEOUT,
                           reinterpret_cast<SQLPOINTER>(static_cast<SQLLEN>(30)), 0);

        rc = SQLDriverConnectW(hDbc, nullptr,
                               const_cast<SQLWCHAR*>(reinterpret_cast<const SQLWCHAR*>(connectionString.c_str())),
                               SQL_NTS, nullptr, 0, nullptr, SQL_DRIVER_NOPROMPT);
        if (SQL_SUCCEEDED(rc)) {
            Log("INFO", "connected on attempt %d/%d", attempt, maxAttempts);
            return rc;
        }

        // Walks the diagnostic records and returns the first record that carries
        // a real SQL Server error number. Microsoft Entra failures report several
        // driver-specific records first, whose native error is 0.
        SQLINTEGER native = LogDiagnostics(SQL_HANDLE_DBC, hDbc, "connect");
        if (attempt == maxAttempts || !IsTransient(native)) return rc;

        // Cap the backoff at 64 seconds. This also keeps the shift in range
        // when a caller passes a large maxAttempts.
        int shift = (attempt - 1 < 6) ? attempt - 1 : 6;
        DWORD delayMs = (1UL << shift) * 1000UL + (DWORD)(GetTickCount64() % 500);
        Log("WARN", "retrying in %lu ms (attempt %d/%d)", delayMs, attempt + 1, maxAttempts);
        Sleep(delayMs);
    }
    return rc;
}
```

`ConnectRetryCount` and `ConnectRetryInterval` enable *idle connection resiliency*, which transparently restores a connection that dropped while idle. They don't retry the initial connect, which is why this snippet also implements application-level retry. Keep both.

ODBC reports diagnostics through `SQLGetDiagRec` rather than the return code alone, so classify failures before retrying. An authentication or configuration error then fails immediately instead of consuming the whole retry budget.

For more information about each part of this configuration, see:

- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md)
- [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md)
- [Connection encryption troubleshooting](connection-troubleshooting.md)
- [Connection resiliency](connection-resiliency.md)
- [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md)

For the catalog of Azure SQL transient errors, see [transient fault error codes](/azure/azure-sql/database/troubleshoot-common-errors-issues#list-of-transient-fault-error-codes).

## Key features

- **Cross-platform**: The same API on Windows, Linux, and macOS.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, and integrated flows.
- **Strict encryption**: TDS 8.0 connections with full certificate validation in version 18 and later versions.
- **Always Encrypted**: Client-side encryption for sensitive columns, with support for custom keystore providers.
- **Connection resiliency**: Transparent restoration of a connection that dropped while idle.
- **High availability**: Availability group listener support with `MultiSubnetFailover`.
- **Data classification**: Sensitivity metadata for classified columns.
- **Vector data type**: Native support for the **vector** type.
- **Distributed transactions**: XA transaction support through the Microsoft Distributed Transaction Coordinator (MSDTC).
- **Companion tools**: **sqlcmd** and **bcp**, installed separately.

## Get started

| Article | Description |
| --- | --- |
| [Download ODBC Driver for SQL Server](download-odbc-driver-for-sql-server.md) | Installer and package downloads for every supported driver version, on all three platforms. |
| [Connect to and query a database with C++](cpp-code-example-app-connect-access-sql-db.md) | A complete C++ sample that connects, runs a query, and reads results, so you can confirm your setup end to end. |
| [Support lifecycle](support-lifecycle.md) | Which driver versions are still supported, and the date each one leaves support. |
| [Major version differences](major-version-differences.md) | What breaks when you move from version 17 to version 18, starting with the encryption default change. |

## Install the driver

| Article | Description |
| --- | --- |
| [System requirements, installation, and driver files (Windows)](windows/system-requirements-installation-and-driver-files.md) | Supported Windows versions, the installer command line for silent deployment, and where each driver file lands on disk. |
| [System requirements (Linux and macOS)](linux-mac/system-requirements.md) | Which Linux distributions and macOS releases each driver version supports, plus SQL Server version compatibility. |
| [Install the ODBC driver on Linux](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) | Package manager steps for Alpine, Debian, Red Hat, SUSE, Ubuntu, and Azure Linux, plus offline installation and the driver file locations. |
| [Install the ODBC driver on macOS](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md) | Homebrew tap and formula steps for macOS, including how to install version 18, 17, or 13.1. |
| [Install the unixODBC driver manager (Linux and macOS)](linux-mac/installing-the-driver-manager.md) | Install or upgrade unixODBC, the driver manager that loads the ODBC driver on Linux and macOS. |

## Configure and connect

| Article | Description |
| --- | --- |
| [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md) | The full catalog of connection string keywords, DSN entries, and `SQLSetConnectAttr` attributes, with accepted values for each. |
| [Connection string keywords and data source names (Linux and macOS)](linux-mac/connection-string-keywords-and-data-source-names-dsns.md) | How `odbc.ini` and `odbcinst.ini` define a DSN on Linux and macOS, plus the TLS and TCP keep-alive settings specific to those platforms. |
| [ODBC Data Source Administrator DSN (Windows)](windows/odbc-administrator-dsn-creation.md) | Every option on the Windows DSN wizard pages, for when you configure a data source through the UI instead of a connection string. |
| [Driver-aware connection pooling (Windows)](windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md) | Which connection string keywords and attributes put a connection in its own pool, and which ones cost an extra round trip to reset. |

## Authenticate and secure

| Article | Description |
| --- | --- |
| [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md) | Every `Authentication` keyword value, from managed identity and service principal to interactive and integrated, with the setup each one needs. |
| [Use Always Encrypted with the ODBC driver](using-always-encrypted-with-the-odbc-driver.md) | Encrypt sensitive columns in the client process so plaintext never reaches the server, with the driver's API summary and its documented limitations. |
| [Data classification](data-classification.md) | Read the sensitivity labels the server attaches to classified columns, so your application can enforce its own data protection policy. |
| [Use integrated authentication (Linux and macOS)](linux-mac/using-integrated-authentication.md) | Configure Kerberos so a Linux or macOS client can connect with Windows credentials instead of a SQL Server login. |

## High availability and resiliency

| Article | Description |
| --- | --- |
| [Connection resiliency](connection-resiliency.md) | How `ConnectRetryCount` and `ConnectRetryInterval` restore a connection when the server drops it while idle, and the `IMCxx` errors the driver returns when recovery isn't possible. |
| [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md) | Connect through an availability group listener and use `MultiSubnetFailover` so failover doesn't stall on a subnet timeout. |
| [Use transparent network IP resolution](using-transparent-network-ip-resolution.md) | How the legacy `TransparentNetworkIPResolution` fallback orders connection attempts across multiple IP addresses, and why `MultiSubnetFailover` supersedes it. |

## Work with data

| Article | Description |
| --- | --- |
| [Vector data type](vector-data-type.md) | Bind, send, and retrieve the **vector** type, including its native C representation and bulk copy support. |
| [Use XA transactions with DTC](use-xa-with-dtc.md) | Enlist SQL Server in a distributed transaction through the Microsoft Distributed Transaction Coordinator on Windows, Linux, or macOS. |
| [Programming guidelines (Linux and macOS)](linux-mac/programming-guidelines.md) | Which features the driver supports on Linux and macOS, which it doesn't, and how character set and OpenSSL handling differ from Windows. |

## Diagnose and troubleshoot

| Article | Description |
| --- | --- |
| [Connection encryption troubleshooting](connection-troubleshooting.md) | Fix the certificate and encryption errors that version 18 surfaces because it encrypts by default. |
| [Data access tracing (Linux and macOS)](linux-mac/data-access-tracing-with-the-odbc-driver-on-linux.md) | Turn on driver tracing and capture a log file when you need to see the calls your application actually makes. |
| [Known issues (Linux and macOS)](linux-mac/known-issues-in-this-version-of-the-driver.md) | Confirmed defects and their workarounds. Check here before you file a support case. |
| [Frequently asked questions (Linux and macOS)](linux-mac/frequently-asked-questions-faq-for-odbc-linux.yml) | Short answers to the questions that come up most often about the driver on Linux and macOS. |

## Release notes and bug fixes

| Article | Description |
| --- | --- |
| [Release notes for Windows](windows/release-notes-odbc-sql-server-windows.md) | New features, behavior changes, and fixes in each Windows driver release. |
| [Release notes for Linux and macOS](linux-mac/release-notes-odbc-sql-server-linux-mac.md) | New features, behavior changes, and fixes in each Linux and macOS driver release. |
| [Release notes for the SQL Server tools](linux-mac/release-notes-tools.md) | Changes to the **sqlcmd** and **bcp** utilities, which install separately from the driver on Linux and macOS. |

## Reference

| Article | Description |
| --- | --- |
| [ODBC driver on Windows](windows/microsoft-odbc-driver-for-sql-server-on-windows.md) | A version-by-version summary of what the driver supports on Windows, and an index of the Windows-specific articles. |
| [Features of the ODBC driver on Windows](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md) | Which release introduced each Windows feature, plus the behavior changes that came with it. |

## Related content

- [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md): The ODBC API specification that this driver implements, documented separately from the driver.
- [SQL Server Native Client features](../../relational-databases/native-client/features/sql-server-native-client-features.md): Driver behavior documented only in the Native Client content. These articles apply to the ODBC Driver for SQL Server except where they describe OLE DB.
- [bcp utility](../../tools/bcp/bcp-utility.md): The bulk copy utility, installed separately from the driver.
- [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md): The command-line query utility, installed separately from the driver.
- [Driver feature support matrix](../driver-feature-matrix.md)
- [SQL Server Drivers blog](https://techcommunity.microsoft.com/category/sql-server/blog/sqlserver)
