---
title: Microsoft ODBC Driver for SQL Server
description: The Microsoft ODBC Driver for SQL Server connects C and C++ applications to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, mcimfl
ms.date: 10/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft ODBC Driver for SQL Server

[!INCLUDE [Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

ODBC is a native data access application programming interface (API) for applications written in C and C++. The Microsoft ODBC Driver for SQL Server connects to SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, and SQL database in Microsoft Fabric. For the database versions each driver release supports, see [SQL version compatibility](windows/system-requirements-installation-and-driver-files.md#sql-version-compatibility).

Your application calls the ODBC driver manager, which loads the driver and passes ODBC calls to it. Use the Windows ODBC Driver Manager on Windows and unixODBC on Linux and macOS.

Other languages that can use ODBC include COBOL, Perl, PHP, and Python. ODBC is widely used in data integration scenarios, and the [Microsoft Drivers for PHP for SQL Server](../php/microsoft-php-driver-for-sql-server.md) are built on this driver.

The ODBC variant of **sqlcmd** and the **bcp** utility use this driver, but they install separately: the `mssql-tools18` package on Linux and macOS, and the Microsoft Command Line Utilities on Windows. The Go variant of **sqlcmd** doesn't use this driver. Use [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md) to run Transact-SQL (T-SQL) statements, system procedures, and script files. Use [bcp](../../tools/bcp/bcp-utility.md) to bulk copy data between an instance of SQL Server and a data file, in either direction.

## Choose your starting point

- To install the driver, go to [System requirements, installation, and driver files](windows/system-requirements-installation-and-driver-files.md) for Windows, or [Install the ODBC driver on Linux](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), [Install the ODBC driver on macOS](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md), and [Install the unixODBC driver manager](linux-mac/installing-the-driver-manager.md).
- To write your first application, go to [Connect to and query a database with C++](cpp-code-example-app-connect-access-sql-db.md). For data source name (DSN) and connection string options, use [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md). If your compiler can't find the headers or your build won't link, go to [Develop C and C++ applications with the ODBC driver](develop-cpp-applications.md).
- To connect to Azure SQL with passwordless authentication, go to [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).
- To make an existing app resilient to transient failures, go to [Connection resiliency](connection-resiliency.md) and [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).
- To upgrade from version 17, go to [Major version differences](major-version-differences.md) and [Connection encryption troubleshooting](connection-troubleshooting.md).
- To diagnose a connection or query problem, go to [Connection encryption troubleshooting](connection-troubleshooting.md) and [Known issues (Linux and macOS)](linux-mac/known-issues-in-this-version-of-the-driver.md).

## Production baseline for Azure SQL

Use this complete C++17 program as a starting point for an Azure-hosted application. It connects, runs `SELECT 1`, checks the result, and releases the ODBC handles. Install Driver 18 and the platform's ODBC development files before you build it. For prerequisites, see [Develop C and C++ applications with the ODBC driver](develop-cpp-applications.md).

Set `ODBC_CONNECTION_STRING` in the application's environment to your complete connection string. The following example uses a system-assigned managed identity. The host must have that identity enabled, and the database must grant it access. For host requirements, user-assigned identities, and local development authentication, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).

```text
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=ActiveDirectoryMsi;Encrypt=strict;ConnectRetryCount=3;ConnectRetryInterval=2;
```

`Encrypt=strict` requires an endpoint that supports Tabular Data Stream (TDS) 8.0. Otherwise, use `Encrypt=yes;TrustServerCertificate=no`. Configure a certificate that the client trusts and whose name matches the server. For setup and verification, see [Connection encryption troubleshooting](connection-troubleshooting.md).

Keep credentials out of source files and shell history. For commands to set the environment variable, see [Configure the connection](cpp-code-example-app-connect-access-sql-db.md#configure-the-connection). On Linux and macOS, supply its value as UTF-8. Save the following program as `odbc-baseline.cpp`:

```cpp
#ifdef _WIN32
#include <windows.h>
#else
#include <iconv.h>
#endif
#include <sql.h>
#include <sqlext.h>

#include <chrono>
#include <cstdint>
#include <cstdlib>
#include <cstring>
#include <iostream>
#include <random>
#include <stdexcept>
#include <string>
#include <thread>
#include <vector>

struct Handles {
    SQLHENV env = SQL_NULL_HENV;
    SQLHDBC dbc = SQL_NULL_HDBC;
    SQLHSTMT stmt = SQL_NULL_HSTMT;
    bool connected = false;

    ~Handles() {
        if (stmt) SQLFreeHandle(SQL_HANDLE_STMT, stmt);
        if (connected) SQLDisconnect(dbc);
        if (dbc) SQLFreeHandle(SQL_HANDLE_DBC, dbc);
        if (env) SQLFreeHandle(SQL_HANDLE_ENV, env);
    }
};

bool logDiagnostics(SQLSMALLINT type, SQLHANDLE handle) {
    bool retry = false;
    bool permanent = false;
    for (SQLSMALLINT record = 1; ; ++record) {
        SQLWCHAR state[6] = {};
        SQLWCHAR message[1024] = {};
        SQLINTEGER native = 0;
        SQLRETURN rc = SQLGetDiagRecW(type, handle, record, state, &native,
                                     message, 1024, nullptr);
        if (rc == SQL_NO_DATA) break;
        if (!SQL_SUCCEEDED(rc)) {
            std::cerr << "event=diagnostics_failed rc=" << rc << '\n';
            return false;
        }
        std::string sqlstate;
        for (int i = 0; i < 5; ++i) {
            sqlstate += static_cast<char>(state[i]);
        }
        std::cerr << "event=odbc_diagnostic sqlstate=" << sqlstate
                  << " native_error=" << native << '\n';
        permanent |= sqlstate.substr(0, 2) == "28" ||
                     sqlstate.substr(0, 2) == "IM" ||
                     sqlstate == "HY024" || sqlstate == "HY092";
        retry |= sqlstate == "08S01" || sqlstate == "HYT00" ||
                 sqlstate == "HYT01";
        switch (native) {
            case 10928: case 10929: case 10936:
            case 40197: case 40501: case 40613:
            case 49918: case 49919: case 49920:
                retry = true;
        }
    }
    return retry && !permanent;
}

void requireSuccess(SQLRETURN rc, SQLSMALLINT type, SQLHANDLE handle,
                    const char* operation) {
    if (rc == SQL_SUCCESS) return;
    logDiagnostics(type, handle);
    if (!SQL_SUCCEEDED(rc)) throw std::runtime_error(operation);
}

std::vector<SQLWCHAR> readConnectionString() {
    static_assert(sizeof(SQLWCHAR) == 2, "Use a driver manager with 16-bit SQLWCHAR");
#ifdef _WIN32
    size_t length = 0;
    if (_wgetenv_s(&length, nullptr, 0, L"ODBC_CONNECTION_STRING") != 0 ||
        length == 0) {
        throw std::runtime_error("ODBC_CONNECTION_STRING is required");
    }
    std::vector<wchar_t> raw(length);
    if (_wgetenv_s(&length, raw.data(), raw.size(), L"ODBC_CONNECTION_STRING") != 0) {
        throw std::runtime_error("Cannot read ODBC_CONNECTION_STRING");
    }
    std::vector<SQLWCHAR> result(raw.begin(), raw.end() - 1);
#else
    const char* raw = std::getenv("ODBC_CONNECTION_STRING");
    if (!raw) throw std::runtime_error("ODBC_CONNECTION_STRING is required");
    iconv_t converter = iconv_open("UTF-16LE", "UTF-8");
    if (converter == reinterpret_cast<iconv_t>(-1)) {
        throw std::runtime_error("Cannot create UTF-8 converter");
    }
    size_t inputBytes = std::strlen(raw);
    std::vector<SQLWCHAR> result(inputBytes + 1);
    char* input = const_cast<char*>(raw);
    char* output = reinterpret_cast<char*>(result.data());
    size_t outputBytes = result.size() * sizeof(SQLWCHAR);
    const size_t capacity = outputBytes;
    size_t status = iconv(converter, &input, &inputBytes, &output, &outputBytes);
    int closeStatus = iconv_close(converter);
    if (status == static_cast<size_t>(-1) || inputBytes != 0 || closeStatus != 0) {
        throw std::runtime_error("ODBC_CONNECTION_STRING must be valid UTF-8");
    }
    result.resize((capacity - outputBytes) / sizeof(SQLWCHAR));
#endif
    if (result.empty() || result.size() >= 32767) {
        throw std::runtime_error("Invalid ODBC_CONNECTION_STRING length");
    }
    result.push_back(0);
    return result;
}

void connectWithRetry(SQLHDBC dbc, std::vector<SQLWCHAR>& connectionString) {
    constexpr int maxAttempts = 4;
    std::mt19937 random{std::random_device{}()};
    std::uniform_int_distribution<int> jitter{0, 250};
    for (int attempt = 1; attempt <= maxAttempts; ++attempt) {
        requireSuccess(SQLSetConnectAttr(
            dbc, SQL_ATTR_LOGIN_TIMEOUT,
            reinterpret_cast<SQLPOINTER>(std::uintptr_t{30}), 0),
            SQL_HANDLE_DBC, dbc, "Set login timeout");
        SQLRETURN rc = SQLDriverConnectW(
            dbc, nullptr, connectionString.data(), SQL_NTS,
            nullptr, 0, nullptr, SQL_DRIVER_NOPROMPT);
        if (SQL_SUCCEEDED(rc)) {
            if (rc == SQL_SUCCESS_WITH_INFO) logDiagnostics(SQL_HANDLE_DBC, dbc);
            std::cout << "event=connected attempt=" << attempt << '\n';
            return;
        }
        bool transient = logDiagnostics(SQL_HANDLE_DBC, dbc);
        if (!transient || attempt == maxAttempts) {
            throw std::runtime_error("Connect failed");
        }
        auto delay = std::chrono::milliseconds{
            (1 << (attempt - 1)) * 10000 + jitter(random)};
        std::cerr << "event=connect_retry attempt=" << attempt
                  << " delay_ms=" << delay.count() << '\n';
        std::this_thread::sleep_for(delay);
    }
}

int main() {
    try {
        auto connectionString = readConnectionString();
        Handles handles;
        if (!SQL_SUCCEEDED(SQLAllocHandle(
                SQL_HANDLE_ENV, SQL_NULL_HANDLE, &handles.env))) {
            throw std::runtime_error("Allocate environment");
        }
        requireSuccess(SQLSetEnvAttr(
            handles.env, SQL_ATTR_ODBC_VERSION,
            reinterpret_cast<SQLPOINTER>(std::uintptr_t{SQL_OV_ODBC3}), 0),
            SQL_HANDLE_ENV, handles.env, "Set ODBC version");
        requireSuccess(SQLAllocHandle(SQL_HANDLE_DBC, handles.env, &handles.dbc),
                       SQL_HANDLE_ENV, handles.env, "Allocate connection");
        connectWithRetry(handles.dbc, connectionString);
        handles.connected = true;
        requireSuccess(SQLAllocHandle(SQL_HANDLE_STMT, handles.dbc, &handles.stmt),
                       SQL_HANDLE_DBC, handles.dbc, "Allocate statement");
        requireSuccess(SQLSetStmtAttr(
            handles.stmt, SQL_ATTR_QUERY_TIMEOUT,
            reinterpret_cast<SQLPOINTER>(std::uintptr_t{10}), 0),
            SQL_HANDLE_STMT, handles.stmt, "Set query timeout");
        SQLWCHAR query[] = {'S', 'E', 'L', 'E', 'C', 'T', ' ', '1', 0};
        requireSuccess(SQLExecDirectW(handles.stmt, query, SQL_NTS),
                       SQL_HANDLE_STMT, handles.stmt, "Execute probe");
        requireSuccess(SQLFetch(handles.stmt),
                       SQL_HANDLE_STMT, handles.stmt, "Fetch probe");
        SQLINTEGER value = 0;
        SQLLEN indicator = 0;
        requireSuccess(SQLGetData(handles.stmt, 1, SQL_C_SLONG,
                                 &value, sizeof(value), &indicator),
                       SQL_HANDLE_STMT, handles.stmt, "Read probe");
        if (indicator == SQL_NULL_DATA || value != 1) {
            throw std::runtime_error("Unexpected probe result");
        }
        std::cout << "event=ready result=" << value << '\n';
        return 0;
    } catch (const std::exception& error) {
        std::cerr << "event=failed operation=\"" << error.what() << "\"\n";
        return 1;
    }
}
```

The program requests a 30-second connection timeout for each of at most four connection attempts and a 10-second query timeout. Between failed transient connection attempts, it waits 10, 20, and 40 seconds, each with 0 to 250 milliseconds of random jitter. These limits don't impose a hard overall deadline on authentication. The example classifies selected transient connection errors; adapt the policy to your workload with the [Azure SQL transient fault error catalog](/azure/azure-sql/database/troubleshoot-common-errors-issues#list-of-transient-fault-error-codes). It doesn't retry application commands or writes.

`ConnectRetryCount` and `ConnectRetryInterval` configure [connection resiliency](connection-resiliency.md) for eligible connections that drop while idle. They don't retry the initial connection or replay application commands. Keep idle-recovery delays shorter than your query timeout. The sample logs SQLSTATE values and native error numbers instead of connection strings or full diagnostic messages. If you need message text for troubleshooting, capture it in a protected diagnostic log.

### Build and run the baseline

Use a compiler environment that matches your application's processor architecture. On Windows, run the commands in Developer PowerShell for Visual Studio. On Linux and macOS, use a shell with your compiler and unixODBC development files available.

### [Windows](#tab/baseline-windows)

```powershell
cl /nologo /std:c++17 /EHsc /W4 odbc-baseline.cpp odbc32.lib
.\odbc-baseline.exe
```

### [Linux](#tab/baseline-linux)

```bash
g++ -std=c++17 -Wall -Wextra -pthread odbc-baseline.cpp -lodbc -o odbc-baseline
./odbc-baseline
```

### [macOS](#tab/baseline-macos)

```bash
clang++ -std=c++17 -Wall -Wextra -pthread \
  -I"$(brew --prefix unixodbc)/include" -L"$(brew --prefix unixodbc)/lib" \
  odbc-baseline.cpp -lodbc -liconv -o odbc-baseline
./odbc-baseline
```

---

A successful first connection prints the following output. Additional diagnostic records might also appear.

```output
event=connected attempt=1
event=ready result=1
```

Clear `ODBC_CONNECTION_STRING` when you finish: use `Remove-Item Env:\ODBC_CONNECTION_STRING` in PowerShell, or `unset ODBC_CONNECTION_STRING` in Bash or Zsh.

For availability group listeners and failover settings, use [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).

## Key features

- **Cross-platform**: The same API on Windows, Linux, and macOS.
- **Microsoft Entra ID authentication**: Managed identity, service principal, and integrated authentication, plus interactive authentication on Windows. Available methods depend on the host and database configuration.
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
| [Develop C and C++ applications with the ODBC driver](develop-cpp-applications.md) | Which headers to include, in what order, which libraries to link, and how to choose between asynchronous execution and threads. |
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
| [Connection string keywords and data source names (Linux and macOS)](linux-mac/connection-string-keywords-and-data-source-names-dsns.md) | How `odbc.ini` and `odbcinst.ini` define a DSN on Linux and macOS, plus Transport Layer Security (TLS) and Transmission Control Protocol (TCP) keep-alive settings specific to those platforms. |
| [ODBC Data Source Administrator DSN (Windows)](windows/odbc-administrator-dsn-creation.md) | Configure a data source through the Windows user interface instead of a connection string. |
| [Driver-aware connection pooling (Windows)](windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md) | Which connection string keywords and attributes put a connection in its own pool, and which ones cost an extra round trip to reset. |

## Authenticate and secure

| Article | Description |
| --- | --- |
| [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md) | Every `Authentication` keyword value, from managed identity and service principal to interactive and integrated, with the setup each one needs. |
| [Use Always Encrypted with the ODBC driver](using-always-encrypted-with-the-odbc-driver.md) | Encrypt sensitive column values before sending them to the database, with the driver's API summary and documented limitations. |
| [Data classification](data-classification.md) | Read the sensitivity labels the server attaches to classified columns, so your application can enforce its own data protection policy. |
| [Use integrated authentication (Linux and macOS)](linux-mac/using-integrated-authentication.md) | Configure Kerberos so a Linux or macOS client can connect with Windows credentials instead of a SQL Server login. |

## High availability and resiliency

| Article | Description |
| --- | --- |
| [Connection resiliency](connection-resiliency.md) | How `ConnectRetryCount` and `ConnectRetryInterval` restore a connection when the server drops it while idle, and the `IMCxx` errors the driver returns when recovery isn't possible. |
| [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md) | Connect through an availability group listener and use `MultiSubnetFailover` so failover doesn't stall on a subnet timeout. |
| [Use transparent network IP resolution](using-transparent-network-ip-resolution.md) | How the legacy `TransparentNetworkIPResolution` fallback orders connection attempts across multiple Internet Protocol (IP) addresses, and why `MultiSubnetFailover` supersedes it. |

## Work with data

| Article | Description |
| --- | --- |
| [Vector data type](vector-data-type.md) | Bind, send, and retrieve the **vector** type, including its native C representation and bulk copy support. |
| [Use XA transactions with DTC](use-xa-with-dtc.md) | Enlist SQL Server in a distributed transaction through the Microsoft Distributed Transaction Coordinator on Windows, Linux, or macOS. |
| [Programming guidelines (Linux and macOS)](linux-mac/programming-guidelines.md) | Find Linux and macOS programming guidance, including feature status, character-set considerations, OpenSSL, and Alpine Linux. |

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

## Request a feature

To request a feature, submit an idea through [SQL Server feedback](https://aka.ms/sqlfeedback).

## Related content

- [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md): The ODBC API specification that this driver implements, documented separately from the driver.
- [SQL Server Native Client features](../../relational-databases/native-client/features/sql-server-native-client-features.md): Historical API context. Check current driver documentation for platform support, authentication, and encryption behavior. Native Client and OLE DB guidance doesn't establish current ODBC driver support.
- [bcp utility](../../tools/bcp/bcp-utility.md): The bulk copy utility, installed separately from the driver.
- [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md): The command-line query utility, installed separately from the driver.
- [Driver feature support matrix](../driver-feature-matrix.md)
- [SQL Server Drivers blog](https://techcommunity.microsoft.com/category/sql-server/blog/sqlserver)
