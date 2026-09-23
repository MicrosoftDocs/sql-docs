---
title: "Quickstart: Connect and Query with C++ and ODBC"
description: Connect a C++ application to a SQL database, run a parameterized query, and read the results with Microsoft ODBC Driver 18 for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ai-usage: ai-assisted
---
# Quickstart: Connect and query with C++ and ODBC

[!INCLUDE [sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

In this quickstart, you build a C++ console application on Windows, Linux, or macOS. The application connects to an `AdventureWorksLT` database by using Microsoft ODBC Driver 18 for SQL Server, binds a query parameter, executes the query, and reads the result rows.

## Prerequisites

- Microsoft ODBC Driver 18 for SQL Server. Install the driver for [Windows](windows/system-requirements-installation-and-driver-files.md), [Linux](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), or [macOS](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).
- A C++17 compiler and the platform ODBC development files:
  - On Windows, install Visual Studio 2022 or the Build Tools for Visual Studio 2022 with the **Desktop development with C++** workload. The Windows SDK supplies the ODBC headers and `odbc32.lib`.
  - On Linux, install a C++ compiler and the unixODBC development package for your distribution. The package supplies the ODBC headers and `libodbc`.
  - On macOS, install the Xcode command-line tools and unixODBC from Homebrew.
- A database in Azure SQL Database or SQL Server that contains the `AdventureWorksLT` sample data. For Azure SQL Database, select the **Sample** data source when you [create a single database](/azure/azure-sql/database/single-database-create-quickstart). For SQL Server, restore an `AdventureWorksLT` backup from [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

## Verify the driver

Confirm that the driver manager can find Microsoft ODBC Driver 18 for SQL Server.

### [Windows](#tab/windows)

Run this command in PowerShell:

```powershell
Get-OdbcDriver -Name "ODBC Driver 18 for SQL Server"
```

### [Linux](#tab/linux)

Run this command in your shell:

```bash
odbcinst -q -d | grep "ODBC Driver 18 for SQL Server"
```

### [macOS](#tab/macos)

Run this command in your shell:

```bash
odbcinst -q -d | grep "ODBC Driver 18 for SQL Server"
```

---

Each command should list `ODBC Driver 18 for SQL Server`. If the driver isn't listed, reinstall it before you continue.

## Configure the connection

The application reads the complete connection string from the `ODBC_CONNECTION_STRING` environment variable. It doesn't display the connection string or accept it as a command-line argument.

Use a Driver 18 connection string that's valid for your database and authentication method. The following SQL authentication example works on Windows, Linux, and macOS when the database allows SQL authentication:

```text
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<server>,1433;Database=<database>;UID=<user_id>;PWD=<password>;Encrypt=yes;TrustServerCertificate=no;
```

For Microsoft Entra authentication options, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md). For all supported settings, see [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md).

Microsoft ODBC Driver 18 enables encryption by default. Specify `Encrypt=yes` so the application's requirement is explicit. Keep `TrustServerCertificate=no` in production so the driver validates the server certificate. The certificate must match the server name and chain to a certification authority that the client trusts. For configuration guidance, see [Certificate validation failures](connection-troubleshooting.md#certificate-chain-errors).

> [!CAUTION]
> `TrustServerCertificate=yes` skips certificate validation. Use it only for isolated local development while you configure a certificate that the client trusts. Don't use it in production.

Set the environment variable without adding the connection string to your shell history.

### [Windows](#tab/windows)

Open Developer PowerShell for VS 2022. Run these commands, and then paste the connection string at the prompt:

```powershell
$secureConnectionString = Read-Host "ODBC connection string" -AsSecureString
$env:ODBC_CONNECTION_STRING = [System.Net.NetworkCredential]::new(
    "", $secureConnectionString).Password
$secureConnectionString = $null
```

### [Linux](#tab/linux)

Run these commands in your shell, and then paste the connection string at the prompt:

```bash
read -rsp "ODBC connection string: " ODBC_CONNECTION_STRING
export ODBC_CONNECTION_STRING
printf '\n'
```

### [macOS](#tab/macos)

Run these commands in your shell, and then paste the connection string at the prompt:

```zsh
read -rs 'ODBC_CONNECTION_STRING?ODBC connection string: '
export ODBC_CONNECTION_STRING
printf '\n'
```

---

An environment variable keeps the connection string out of the source file, but the process and its child processes can read it. For production applications, use Microsoft Entra authentication where possible and retrieve secrets from a secure store at runtime.

## Create the application

1. Create a project directory and change to it:

   ### [Windows](#tab/windows)

   ```powershell
   New-Item -ItemType Directory odbc-quickstart
   Set-Location odbc-quickstart
   ```

   ### [Linux](#tab/linux)

   ```bash
   mkdir odbc-quickstart
   cd odbc-quickstart
   ```

   ### [macOS](#tab/macos)

   ```bash
   mkdir odbc-quickstart
   cd odbc-quickstart
   ```

   ---

2. Create a file named `odbc-quickstart.cpp` with the following code:

   ```cpp
   #ifdef _WIN32
   #include <windows.h>
   #endif

   #include <sql.h>
   #include <sqlext.h>
   #include <sqltypes.h>

   #include <cstdlib>
   #include <iomanip>
   #include <iostream>
   #include <string>

   std::string ReadEnvironmentVariable(const char* name)
   {
   #ifdef _WIN32
       char* value = nullptr;
       std::size_t length = 0;
       if (_dupenv_s(&value, &length, name) != 0 || value == nullptr)
           return {};

       std::string result(value);
       std::free(value);
       return result;
   #else
       const char* value = std::getenv(name);
       return value == nullptr ? std::string{} : value;
   #endif
   }

   void PrintDiagnostics(SQLSMALLINT handleType, SQLHANDLE handle)
   {
       SQLCHAR state[6];
       SQLINTEGER nativeError;
       SQLCHAR message[SQL_MAX_MESSAGE_LENGTH];
       SQLSMALLINT messageLength;

       for (SQLSMALLINT record = 1;
            SQL_SUCCEEDED(SQLGetDiagRec(handleType, handle, record, state,
                                        &nativeError, message, sizeof(message),
                                        &messageLength));
            ++record)
       {
           std::cerr << '[' << state << "] (" << nativeError << ") "
                     << message << '\n';
       }
   }

   bool Succeeded(SQLRETURN result, SQLSMALLINT handleType, SQLHANDLE handle)
   {
       if (SQL_SUCCEEDED(result))
           return true;

       PrintDiagnostics(handleType, handle);
       return false;
   }

   struct OdbcHandles
   {
       SQLHENV environment = SQL_NULL_HENV;
       SQLHDBC connection = SQL_NULL_HDBC;
       SQLHSTMT statement = SQL_NULL_HSTMT;

       ~OdbcHandles()
       {
           if (statement != SQL_NULL_HSTMT)
               SQLFreeHandle(SQL_HANDLE_STMT, statement);
           if (connection != SQL_NULL_HDBC)
           {
               SQLDisconnect(connection);
               SQLFreeHandle(SQL_HANDLE_DBC, connection);
           }
           if (environment != SQL_NULL_HENV)
               SQLFreeHandle(SQL_HANDLE_ENV, environment);
       }
   };

   int main()
   {
       std::string connectionString =
           ReadEnvironmentVariable("ODBC_CONNECTION_STRING");
       if (connectionString.empty())
       {
           std::cerr << "Set ODBC_CONNECTION_STRING before running.\n";
           return 1;
       }

       OdbcHandles handles;
       SQLRETURN result = SQLAllocHandle(
           SQL_HANDLE_ENV, SQL_NULL_HANDLE, &handles.environment);
       if (!SQL_SUCCEEDED(result))
       {
           std::cerr << "Unable to allocate an ODBC environment handle.\n";
           return 1;
       }

       result = SQLSetEnvAttr(
           handles.environment,
           SQL_ATTR_ODBC_VERSION,
           reinterpret_cast<SQLPOINTER>(SQL_OV_ODBC3_80),
           0);
       if (!Succeeded(result, SQL_HANDLE_ENV, handles.environment))
           return 1;

       result = SQLAllocHandle(
           SQL_HANDLE_DBC, handles.environment, &handles.connection);
       if (!Succeeded(result, SQL_HANDLE_ENV, handles.environment))
           return 1;

       result = SQLDriverConnect(
           handles.connection,
           nullptr,
           reinterpret_cast<SQLCHAR*>(connectionString.data()),
           SQL_NTS,
           nullptr,
           0,
           nullptr,
           SQL_DRIVER_NOPROMPT);
       if (!Succeeded(result, SQL_HANDLE_DBC, handles.connection))
           return 1;

       result = SQLAllocHandle(
           SQL_HANDLE_STMT, handles.connection, &handles.statement);
       if (!Succeeded(result, SQL_HANDLE_DBC, handles.connection))
           return 1;

       SQLINTEGER minimumProductId = 0;
       SQLLEN minimumProductIdLength = 0;
       result = SQLBindParameter(
           handles.statement,
           1,
           SQL_PARAM_INPUT,
           SQL_C_SLONG,
           SQL_INTEGER,
           10,
           0,
           &minimumProductId,
           0,
           &minimumProductIdLength);
       if (!Succeeded(result, SQL_HANDLE_STMT, handles.statement))
           return 1;

       SQLCHAR query[] =
           "SELECT TOP (5) ProductID, Name "
           "FROM SalesLT.Product "
           "WHERE ProductID > ? "
           "ORDER BY ProductID;";
       result = SQLExecDirect(handles.statement, query, SQL_NTS);
       if (!Succeeded(result, SQL_HANDLE_STMT, handles.statement))
           return 1;

       std::cout << "Product ID  Name\n"
                 << "----------  ----\n";

       while (SQL_SUCCEEDED(result = SQLFetch(handles.statement)))
       {
           SQLINTEGER productId;
           SQLLEN productIdLength;
           SQLCHAR productName[256];
           SQLLEN productNameLength;

           result = SQLGetData(
               handles.statement, 1, SQL_C_SLONG, &productId,
               sizeof(productId), &productIdLength);
           if (!Succeeded(result, SQL_HANDLE_STMT, handles.statement))
               return 1;

           result = SQLGetData(
               handles.statement, 2, SQL_C_CHAR, productName,
               sizeof(productName), &productNameLength);
           if (!Succeeded(result, SQL_HANDLE_STMT, handles.statement))
               return 1;

           std::cout << std::left << std::setw(12) << productId
                     << productName << '\n';
       }

       if (result != SQL_NO_DATA)
       {
           PrintDiagnostics(SQL_HANDLE_STMT, handles.statement);
           return 1;
       }

       return 0;
   }
   ```

The application uses only the standard ODBC API, so it includes the driver-manager headers and links to the driver-manager library. The connection string selects Microsoft ODBC Driver 18 for SQL Server at runtime.

`SQL_DRIVER_NOPROMPT` prevents `SQLDriverConnect` from opening a configuration dialog. If the connection string is incomplete, the call returns an error and the application prints every diagnostic record.

The query binds `0` as a SQL Server **int** parameter, reads the first five products in `SalesLT.Product`, and retrieves each product ID and name with `SQLGetData`.

## Build and run the application

### [Windows](#tab/windows)

1. In the same Developer PowerShell window, compile the application:

   ```powershell
   cl /std:c++17 /EHsc /W4 odbc-quickstart.cpp /link odbc32.lib
   ```

1. Run the application:

   ```powershell
   .\odbc-quickstart.exe
   ```

### [Linux](#tab/linux)

1. Compile the application:

   ```bash
   g++ -std=c++17 -Wall -Wextra -Wpedantic \
       -o odbc-quickstart odbc-quickstart.cpp -lodbc
   ```

1. Run the application:

   ```bash
   ./odbc-quickstart
   ```

### [macOS](#tab/macos)

1. Compile the application:

   ```bash
   clang++ -std=c++17 -Wall -Wextra -Wpedantic \
       -I"$(brew --prefix unixodbc)/include" \
       -L"$(brew --prefix unixodbc)/lib" \
       -o odbc-quickstart odbc-quickstart.cpp -lodbc
   ```

1. Run the application:

   ```bash
   ./odbc-quickstart
   ```

---

The application's standard output is:

```output
Product ID  Name
----------  ----
680         HL Road Frame - Black, 58
706         HL Road Frame - Red, 58
707         Sport-100 Helmet, Red
708         Sport-100 Helmet, Black
709         Mountain Bike Socks, M
```

Clear the connection string when you finish.

### [Windows](#tab/windows)

```powershell
Remove-Item Env:\ODBC_CONNECTION_STRING
```

### [Linux](#tab/linux)

```bash
unset ODBC_CONNECTION_STRING
```

### [macOS](#tab/macos)

```bash
unset ODBC_CONNECTION_STRING
```

---

## Related content

- [Develop C and C++ applications with the ODBC driver](develop-cpp-applications.md)
- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md)
- [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md)
- [Connection encryption troubleshooting](connection-troubleshooting.md)
- [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md)
