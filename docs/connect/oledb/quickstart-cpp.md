---
title: "Quickstart: Connect and Query with the Microsoft OLE DB Driver"
description: Connect a C++ application to a SQL database, run a parameterized query, and verify the result with the Microsoft OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ai-usage: ai-assisted
---

# Quickstart: Connect and query with the Microsoft OLE DB Driver

In this quickstart, you build a Windows C++ console application with Visual Studio 2022 and later versions. The application connects to Azure SQL Database, SQL database in Microsoft Fabric, or SQL Server with Microsoft OLE DB Driver 19 for SQL Server. It executes a parameterized query against the `AdventureWorksLT` sample data and verifies the result.

## Prerequisites

- [Visual Studio 2022 and later versions](https://visualstudio.microsoft.com/downloads/) with the **Desktop development with C++** workload. This workload installs the x64 Native Tools Command Prompt used in this quickstart. For instructions to open it, see [Use a 64-bit hosted developer command prompt shortcut](/cpp/build/how-to-enable-a-64-bit-visual-cpp-toolset-on-the-command-line#use-a-64-bit-hosted-developer-command-prompt-shortcut).
- [Microsoft OLE DB Driver 19 for SQL Server](download-oledb-driver-for-sql-server.md). Install the x64 driver and select both the client components and the software development kit (SDK).

[!INCLUDE [prereq-create-sql-database](../../includes/paragraph-content/prereq-create-sql-database.md)]

For this quickstart, select or load the `AdventureWorksLT` sample data.

For a SQL Server container, create the container and load the sample data in one command:

```console
sqlcmd create mssql --accept-eula --using https://aka.ms/AdventureWorksLT.bak
```

For an existing SQL Server instance, restore an `AdventureWorksLT` backup from [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

The OLE DB client application in this quickstart runs on Windows. A SQL Server container can run on another supported host.

## Verify the driver

Open an x64 Native Tools Command Prompt for your Visual Studio version, and then run the following command:

> [!IMPORTANT]
> The commands in this quickstart use Command Prompt syntax. Run them in an x64 Native Tools Command Prompt, where the prompt ends with `>`. Don't run them in PowerShell, where the prompt starts with `PS`.

```console
reg query HKCR\MSOLEDBSQL19
```

The command displays the registered `MSOLEDBSQL19` provider.

## Configure the connection

Set `OLEDB_CONNECTION_STRING` in the x64 Native Tools Command Prompt. The application reads the connection string from the environment and doesn't display it.

For Azure SQL Database or SQL database in Fabric, use Microsoft Entra interactive authentication. Replace the placeholders with the server, database, and Microsoft Entra user ID from your SQL resource:

```console
set "OLEDB_CONNECTION_STRING=Provider=MSOLEDBSQL19;Data Source=tcp:<server>,1433;Initial Catalog=<database>;Authentication=ActiveDirectoryInteractive;User ID=<user_id>;Use Encryption for Data=Mandatory;Trust Server Certificate=false;"
```

For SQL database in Fabric, your identity needs **Read** permission for the database item. SQL authentication isn't supported. For more information, see [Authentication in SQL database in Microsoft Fabric](/fabric/database/sql/authentication).

For an existing SQL Server instance that accepts Windows Authentication, use `Integrated Security=SSPI`:

```console
set "OLEDB_CONNECTION_STRING=Provider=MSOLEDBSQL19;Data Source=tcp:<server>,1433;Initial Catalog=<database>;Integrated Security=SSPI;Use Encryption for Data=Mandatory;Trust Server Certificate=false;"
```

For a SQL Server instance or container that accepts SQL authentication, use `Authentication=SqlPassword`:

```console
set "OLEDB_USER_ID=<user_id>"
set "OLEDB_PASSWORD=<password>"
set "OLEDB_CONNECTION_STRING=Provider=MSOLEDBSQL19;Data Source=tcp:<server>,1433;Initial Catalog=<database>;Authentication=SqlPassword;User ID=%OLEDB_USER_ID%;Password=%OLEDB_PASSWORD%;Use Encryption for Data=Mandatory;Trust Server Certificate=false;"
```

The SQL Server certificate must match the server name and chain to a certification authority (CA) that the Windows client trusts. For a SQL Server container, configure Transport Layer Security (TLS) in the container and register the issuing CA on the Windows client before you run the application. For more information, see [Encrypt connections to SQL Server on Linux](../../linux/security/encrypted-connections.md) and [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

## Create the application

1. Create a project directory:

   ```console
   mkdir oledb-quickstart
   cd oledb-quickstart
   ```

1. Create a file named `oledb-quickstart.cpp` with the following code:

   ```cpp
   #include <windows.h>
   #include <oledb.h>
   #include <msdasc.h>
   #include <msoledbsql.h>

   #include <cstddef>
   #include <iomanip>
   #include <iostream>
   #include <string>

   template <typename T>
   void Release(T*& pointer)
   {
       if (pointer != nullptr)
       {
           pointer->Release();
           pointer = nullptr;
       }
   }

   struct ParameterData
   {
       DBSTATUS status;
       DBLENGTH length;
       LONG value;
   };

   constexpr std::size_t productNameCharacters = 51;

   struct RowData
   {
       DBSTATUS productIdStatus;
       DBLENGTH productIdLength;
       LONG productId;
       DBSTATUS nameStatus;
       DBLENGTH nameLength;
       wchar_t name[productNameCharacters];
   };

   std::wstring ReadEnvironmentVariable(const wchar_t* name)
   {
       const DWORD length = GetEnvironmentVariableW(name, nullptr, 0);
       if (length == 0)
           return {};

       std::wstring value(length, L'\0');
       const DWORD copied = GetEnvironmentVariableW(
           name,
           value.data(),
           length);
       if (copied == 0 || copied >= length)
           return {};

       value.resize(copied);
       return value;
   }

   int wmain()
   {
       const std::wstring connectionString =
           ReadEnvironmentVariable(L"OLEDB_CONNECTION_STRING");
       if (connectionString.empty())
       {
           std::wcerr << L"Set OLEDB_CONNECTION_STRING before running.\n";
           return 1;
       }

       HRESULT result = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
       if (FAILED(result))
       {
           std::wcerr << L"COM initialization failed: 0x"
                      << std::hex << result << L'\n';
           return 1;
       }

       IDataInitialize* dataInitialize = nullptr;
       IDBInitialize* dbInitialize = nullptr;
       IDBCreateSession* createSession = nullptr;
       IDBCreateCommand* createCommand = nullptr;
       ICommandText* commandText = nullptr;
       ICommandWithParameters* commandParameters = nullptr;
       IAccessor* parameterAccessor = nullptr;
       IRowset* rowset = nullptr;
       IAccessor* rowAccessor = nullptr;
       HACCESSOR parameterHandle = DB_NULL_HACCESSOR;
       HACCESSOR rowHandle = DB_NULL_HACCESSOR;
       HROW* rows = nullptr;
       DBCOUNTITEM rowCount = 0;
       bool initialized = false;

       do
       {
           result = CoCreateInstance(
               CLSID_MSDAINITIALIZE,
               nullptr,
               CLSCTX_INPROC_SERVER,
               IID_IDataInitialize,
               reinterpret_cast<void**>(&dataInitialize));
           if (FAILED(result))
               break;

           result = dataInitialize->GetDataSource(
               nullptr,
               CLSCTX_INPROC_SERVER,
               connectionString.c_str(),
               IID_IDBInitialize,
               reinterpret_cast<IUnknown**>(&dbInitialize));
           if (FAILED(result))
               break;

           result = dbInitialize->Initialize();
           if (FAILED(result))
               break;
           initialized = true;

           result = dbInitialize->QueryInterface(
               IID_IDBCreateSession,
               reinterpret_cast<void**>(&createSession));
           if (FAILED(result))
               break;

           result = createSession->CreateSession(
               nullptr,
               IID_IDBCreateCommand,
               reinterpret_cast<IUnknown**>(&createCommand));
           if (FAILED(result))
               break;

           result = createCommand->CreateCommand(
               nullptr,
               IID_ICommandText,
               reinterpret_cast<IUnknown**>(&commandText));
           if (FAILED(result))
               break;

           result = commandText->SetCommandText(
               DBGUID_DBSQL,
               const_cast<wchar_t*>(
                   L"SELECT TOP (5) ProductID, Name "
                   L"FROM SalesLT.Product "
                   L"WHERE ProductID > ? "
                   L"ORDER BY ProductID;"));
           if (FAILED(result))
               break;

           result = commandText->QueryInterface(
               IID_ICommandWithParameters,
               reinterpret_cast<void**>(&commandParameters));
           if (FAILED(result))
               break;

           DB_UPARAMS parameterOrdinal = 1;
           wchar_t parameterType[] = L"int";
           DBPARAMBINDINFO parameterInfo = {};
           parameterInfo.pwszDataSourceType = parameterType;
           parameterInfo.ulParamSize = sizeof(LONG);
           parameterInfo.dwFlags = DBPARAMFLAGS_ISINPUT;
           parameterInfo.bPrecision = 10;

           result = commandParameters->SetParameterInfo(
               1,
               &parameterOrdinal,
               &parameterInfo);
           if (FAILED(result))
               break;

           result = commandText->QueryInterface(
               IID_IAccessor,
               reinterpret_cast<void**>(&parameterAccessor));
           if (FAILED(result))
               break;

           DBBINDING parameterBinding = {};
           parameterBinding.iOrdinal = 1;
           parameterBinding.obStatus = offsetof(ParameterData, status);
           parameterBinding.obLength = offsetof(ParameterData, length);
           parameterBinding.obValue = offsetof(ParameterData, value);
           parameterBinding.dwPart = DBPART_STATUS | DBPART_LENGTH | DBPART_VALUE;
           parameterBinding.dwMemOwner = DBMEMOWNER_CLIENTOWNED;
           parameterBinding.eParamIO = DBPARAMIO_INPUT;
           parameterBinding.cbMaxLen = sizeof(LONG);
           parameterBinding.wType = DBTYPE_I4;
           parameterBinding.bPrecision = 10;

           DBBINDSTATUS parameterBindStatus = DBBINDSTATUS_OK;
           result = parameterAccessor->CreateAccessor(
               DBACCESSOR_PARAMETERDATA,
               1,
               &parameterBinding,
               sizeof(ParameterData),
               &parameterHandle,
               &parameterBindStatus);
           if (FAILED(result) || parameterBindStatus != DBBINDSTATUS_OK)
           {
               if (SUCCEEDED(result))
                   result = E_FAIL;
               break;
           }

           ParameterData parameter = {
               DBSTATUS_S_OK,
               sizeof(LONG),
               0
           };
           DBPARAMS parameters = {
               &parameter,
               1,
               parameterHandle
           };

           result = commandText->Execute(
               nullptr,
               IID_IRowset,
               &parameters,
               nullptr,
               reinterpret_cast<IUnknown**>(&rowset));
           if (FAILED(result))
               break;

           result = rowset->QueryInterface(
               IID_IAccessor,
               reinterpret_cast<void**>(&rowAccessor));
           if (FAILED(result))
               break;

           DBBINDING rowBindings[2] = {};
           rowBindings[0].iOrdinal = 1;
           rowBindings[0].obStatus = offsetof(RowData, productIdStatus);
           rowBindings[0].obLength = offsetof(RowData, productIdLength);
           rowBindings[0].obValue = offsetof(RowData, productId);
           rowBindings[0].dwPart =
               DBPART_STATUS | DBPART_LENGTH | DBPART_VALUE;
           rowBindings[0].dwMemOwner = DBMEMOWNER_CLIENTOWNED;
           rowBindings[0].eParamIO = DBPARAMIO_NOTPARAM;
           rowBindings[0].cbMaxLen = sizeof(LONG);
           rowBindings[0].wType = DBTYPE_I4;
           rowBindings[0].bPrecision = 10;

           rowBindings[1].iOrdinal = 2;
           rowBindings[1].obStatus = offsetof(RowData, nameStatus);
           rowBindings[1].obLength = offsetof(RowData, nameLength);
           rowBindings[1].obValue = offsetof(RowData, name);
           rowBindings[1].dwPart =
               DBPART_STATUS | DBPART_LENGTH | DBPART_VALUE;
           rowBindings[1].dwMemOwner = DBMEMOWNER_CLIENTOWNED;
           rowBindings[1].eParamIO = DBPARAMIO_NOTPARAM;
           rowBindings[1].cbMaxLen =
               productNameCharacters * sizeof(wchar_t);
           rowBindings[1].wType = DBTYPE_WSTR;

           DBBINDSTATUS rowBindStatus[2] = {
               DBBINDSTATUS_OK,
               DBBINDSTATUS_OK
           };
           result = rowAccessor->CreateAccessor(
               DBACCESSOR_ROWDATA,
               2,
               rowBindings,
               sizeof(RowData),
               &rowHandle,
               rowBindStatus);
           if (FAILED(result) ||
               rowBindStatus[0] != DBBINDSTATUS_OK ||
               rowBindStatus[1] != DBBINDSTATUS_OK)
           {
               if (SUCCEEDED(result))
                   result = E_FAIL;
               break;
           }

           DBCOUNTITEM productsPrinted = 0;
           while (true)
           {
               result = rowset->GetNextRows(
                   DB_NULL_HCHAPTER,
                   0,
                   1,
                   &rowCount,
                   &rows);
               if (FAILED(result) || rowCount == 0)
                   break;

               RowData row = {};
               result = rowset->GetData(rows[0], rowHandle, &row);
               if (FAILED(result) ||
                   row.productIdStatus != DBSTATUS_S_OK ||
                   row.productIdLength != sizeof(LONG) ||
                   row.nameStatus != DBSTATUS_S_OK ||
                   row.nameLength == 0 ||
                   row.nameLength % sizeof(wchar_t) != 0 ||
                   row.nameLength >= sizeof(row.name))
               {
                   result = E_FAIL;
                   break;
               }
               row.name[row.nameLength / sizeof(wchar_t)] = L'\0';

               if (productsPrinted == 0)
               {
                   std::wcout << L"Connected with MSOLEDBSQL19.\n\n";
                   std::wcout << std::left
                              << std::setw(12) << L"Product ID"
                              << L"Name\n";
                   std::wcout << std::setw(12) << L"----------"
                              << L"----\n";
               }

               std::wcout << std::left
                          << std::setw(12) << row.productId
                          << row.name << L'\n';
               ++productsPrinted;

               result = rowset->ReleaseRows(
                   rowCount,
                   rows,
                   nullptr,
                   nullptr,
                   nullptr);
               CoTaskMemFree(rows);
               rows = nullptr;
               rowCount = 0;
               if (FAILED(result))
                   break;
           }

           if (FAILED(result))
               break;
           if (productsPrinted == 0)
           {
               result = E_FAIL;
               break;
           }
       }
       while (false);

       if (rows != nullptr)
       {
           if (rowset != nullptr && rowCount != 0)
               rowset->ReleaseRows(rowCount, rows, nullptr, nullptr, nullptr);
           CoTaskMemFree(rows);
       }
       if (rowHandle != DB_NULL_HACCESSOR && rowAccessor != nullptr)
           rowAccessor->ReleaseAccessor(rowHandle, nullptr);
       if (parameterHandle != DB_NULL_HACCESSOR && parameterAccessor != nullptr)
           parameterAccessor->ReleaseAccessor(parameterHandle, nullptr);

       Release(rowAccessor);
       Release(rowset);
       Release(parameterAccessor);
       Release(commandParameters);
       Release(commandText);
       Release(createCommand);
       Release(createSession);
       if (initialized)
           dbInitialize->Uninitialize();
       Release(dbInitialize);
       Release(dataInitialize);
       CoUninitialize();

       if (FAILED(result))
       {
           std::wcerr << L"OLE DB operation failed: 0x"
                      << std::hex << result << L'\n';
           return 1;
       }

       return 0;
   }
   ```

The connection string is passed to `IDataInitialize::GetDataSource`. This API uses the spaced keyword names `Use Encryption for Data` and `Trust Server Certificate`. The connection strings request encryption and require certificate validation.

The query uses a question mark as the parameter marker. The application binds the minimum product ID `0` as a SQL Server **int**, reads up to five rows from `SalesLT.Product`, and prints the product ID and name.

## Build and run the application

1. In the same x64 Native Tools Command Prompt, find the installed OLE DB SDK header and store its directory in `OLEDB_INCLUDE`:

   ```console
   for /f "delims=" %i in ('where /r "%ProgramFiles%\Microsoft SQL Server\Client SDK\OLEDB" msoledbsql.h') do for %j in ("%~dpi.") do set "OLEDB_INCLUDE=%~fj"
   ```

1. Display the selected directory, and verify that it contains the header:

   ```console
   echo %OLEDB_INCLUDE%
   dir "%OLEDB_INCLUDE%\msoledbsql.h"
   ```

1. Compile the application:

   ```console
   cl /std:c++17 /EHsc /W4 /I"%OLEDB_INCLUDE%" oledb-quickstart.cpp /link ole32.lib oleaut32.lib
   ```

1. Run the application:

   ```console
   oledb-quickstart.exe
   ```

1. Clear the connection string from the current Command Prompt:

   ```console
   set OLEDB_CONNECTION_STRING=
   set OLEDB_USER_ID=
   set OLEDB_PASSWORD=
   ```

The product rows can vary by AdventureWorksLT version. The output resembles the following example:

```output
Connected with MSOLEDBSQL19.

Product ID  Name
----------  ----
680         HL Road Frame - Black, 58
706         HL Road Frame - Red, 58
707         Sport-100 Helmet, Red
708         Sport-100 Helmet, Black
709         Mountain Bike Socks, M
```

## Related content

- [Use Microsoft Entra ID](features/using-azure-active-directory.md)
- [Encryption and certificate validation](features/encryption-and-certificate-validation.md)
- [Using connection string keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Command parameters](ole-db-commands/command-parameters.md)
- [Creating an OLE DB Driver for SQL Server application](ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md)
