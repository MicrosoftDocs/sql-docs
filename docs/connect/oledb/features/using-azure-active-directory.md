---
title: "Using Azure Active Directory| Microsoft Docs for SQL Server"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: reference
author: bazizi
ms.author: v-beaziz
---
# Using Azure Active Directory
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Purpose

Starting with version 18.2.1, Microsoft OLE DB Driver for SQL Server allows OLE DB applications to connect to an instance of Azure SQL Database using a federated identity. The new authentication methods include:
- Azure Active Directory login ID and password
- Azure Active Directory access token
- Azure Active Directory integrated authentication
- SQL login ID and password

> [!NOTE]  
> When using the following Azure Active Directory options with the OLE DB driver, ensure that the [Active Directory Authentication Library for SQL Server](https://go.microsoft.com/fwlink/?LinkID=513072) has been installed:
> - Azure Active Directory login ID and password
> - Azure Active Directory integrated authentication
>
> ADAL isn't  required for the other authentication methods or OLE DB operations.

> [!NOTE]
> Using the following authentication modes with `DataTypeCompatibility` (or its corresponding property) set to `80` is **not** supported:
> - Azure Active Directory authentication using login ID and password
> - Azure Active Directory authentication using access token
> - Azure Active Directory integrated authentication

## Connection keywords and properties
The following connection string keywords have been introduced to support Azure Active Directory authentication:

|Connection keyword|Connection property|Description|
|---               |---                |---        |
|Access Token|SSPROP_AUTH_ACCESS_TOKEN|Specifies an access token to authenticate to Azure Active Directory. |
|Authentication|SSPROP_AUTH_MODE|Specifies authentication method to use.|

For more information about the new keywords/properties, see the following pages:
- [Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Initialization and Authorization Properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)

## Encryption and certificate validation
This section discusses the changes in encryption and certificate validation behavior. These changes are **only** effective when using the new Authentication or Access Token connection string keywords (or their corresponding properties).

### Encryption
To improve security, when the new connection properties/keywords are used, the driver overrides the default encryption value by setting it to `yes`. Overriding happens at data source object initialization time. If encryption is set before initialization by any means, the value is respected and not overridden.

> [!NOTE]   
> In ADO applications and in applications that obtain the `IDBInitialize` interface through `IDataInitialize::GetDataSource`, the Core Component implementing the interface explicitly sets encryption to its default value of `no`. As a result, the new authentication properties/keywords respect this setting and the encryption value **isn't** overridden. Therefore, it is **recommended** that these applications explicitly set `Use Encryption for Data=true` to override the default value.

### Certificate validation
To improve security, the new connection properties/keywords respect the `TrustServerCertificate` setting (and its corresponding connection string keywords/properties) **independently of the client encryption setting**. As a result, server certificate is validated by default.

> [!NOTE]   
> Certificate validation can also be controlled through the `Value` field of the `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI18.0\GeneralFlags\Flag2` registry entry. Valid values are `0` or `1`. The OLE DB driver chooses the most secure option between the registry and the connection property/keyword settings. That is, the driver will validate the server certificate as long as at least one of the registry/connection settings enables server certificate validation.

## GUI additions
The driver graphical user interface has been enhanced to allow Azure Active Directory authentication. For more information, see:
- [SQL Server Login Dialog](../help-topics/sql-server-login-dialog.md)
- [Universal Data Link (UDL) Configuration](../help-topics/data-link-pages.md)

## Example connection strings
This section shows examples of new and existing connection string keywords to be used with `IDataInitialize::GetDataSource` and `DBPROP_INIT_PROVIDERSTRING` property.

### SQL authentication
- Using `IDataInitialize::GetDataSource`:
    - New:
        > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Authentication=SqlPassword**;User ID=[username];Password=[password];Use Encryption for Data=true
    - Deprecated:
        > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];User ID=[username];Password=[password];Use Encryption for Data=true
- Using `DBPROP_INIT_PROVIDERSTRING`:
    - New:
        > Server=[server];Database=[database];**Authentication=SqlPassword**;UID=[username];PWD=[password];Encrypt=yes
    - Deprecated:
        > Server=[server];Database=[database];UID=[username];PWD=[password];Encrypt=yes

### Integrated Windows authentication using Security Support Provider Interface  (SSPI)

- Using `IDataInitialize::GetDataSource`:
    - New:
        > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryIntegrated**;Use Encryption for Data=true
    - Deprecated:
        > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Integrated Security=SSPI**;Use Encryption for Data=true
- Using `DBPROP_INIT_PROVIDERSTRING`:
    - New:
        > Server=[server];Database=[database];**Authentication=ActiveDirectoryIntegrated**;Encrypt=yes
    - Deprecated:
        > Server=[server];Database=[database];**Trusted_Connection=yes**;Encrypt=yes

### AAD username and password authentication using ADAL

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryPassword**;User ID=[username];Password=[password];Use Encryption for Data=true
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryPassword**;UID=[username];PWD=[password];Encrypt=yes

### Integrated Azure Active Directory authentication using ADAL

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryIntegrated**;Use Encryption for Data=true
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryIntegrated**;Encrypt=yes

### Azure Active Directory authentication using an access token

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];**Access Token=[access token]**;Use Encryption for Data=true
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Providing access token through `DBPROP_INIT_PROVIDERSTRING` isn't supported

## Code samples

The following samples show the code required to connect to Azure Active Directory with connection keywords. 

### Access Token
```cpp
#include <string>
#include <iostream>
#include <msdasc.h>

int main()
{
    wchar_t azureServer[] = L"server";
    wchar_t azureDatabase[] = L"mydatabase";
    wchar_t accessToken[] = L"eyJ0eXAiOi...";
    IDBInitialize *pIDBInitialize = nullptr;
    IDataInitialize* pIDataInitialize = nullptr;
    HRESULT hr = S_OK;

    CoInitialize(nullptr);

    // Construct the connection string.
    std::wstring connString = L"Provider=MSOLEDBSQL;Data Source=" + std::wstring(azureServer) + L";Initial Catalog=" + 
                              std::wstring(azureDatabase) + L";Access Token=" + accessToken + L";Use Encryption for Data=true;";
    hr = CoCreateInstance(CLSID_MSDAINITIALIZE, nullptr, CLSCTX_INPROC_SERVER, 
                          IID_IDataInitialize, reinterpret_cast<LPVOID*>(&pIDataInitialize));
    if (FAILED(hr))
    {
        std::cout << "Failed to create an IDataInitialize instance." << std::endl;
        goto Cleanup;
    }
    hr = pIDataInitialize->GetDataSource(nullptr, CLSCTX_INPROC_SERVER, connString.c_str(), 
                                         IID_IDBInitialize, reinterpret_cast<IUnknown**>(&pIDBInitialize));
    if (FAILED(hr))
    {
        std::cout << "Failed to get data source object." << std::endl;
        goto Cleanup;
    }
    hr = pIDBInitialize->Initialize();
    if (FAILED(hr))
    {
        std::cout << "Failed to establish connection." << std::endl;
        goto Cleanup;
    }

Cleanup:
    if (pIDBInitialize)
    {
        pIDBInitialize->Uninitialize();
        pIDBInitialize->Release();
    }
    if (pIDataInitialize)
    {
        pIDataInitialize->Release();
    }

    CoUninitialize();
}
```
### Active Directory Integrated
```cpp
#include <string>
#include <iostream>
#include <msdasc.h>

int main()
{
    wchar_t azureServer[] = L"server";
    wchar_t azureDatabase[] = L"mydatabase";
    IDBInitialize *pIDBInitialize = nullptr;
    IDataInitialize* pIDataInitialize = nullptr;
    HRESULT hr = S_OK;

    CoInitialize(nullptr);

    // Construct the connection string.
    std::wstring connString = L"Provider=MSOLEDBSQL;Data Source=" + std::wstring(azureServer) + L";Initial Catalog=" + 
                              std::wstring(azureDatabase) + L";Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=true;";

    hr = CoCreateInstance(CLSID_MSDAINITIALIZE, nullptr, CLSCTX_INPROC_SERVER, 
                          IID_IDataInitialize, reinterpret_cast<LPVOID*>(&pIDataInitialize));
    if (FAILED(hr)) 
    {
        std::cout << "Failed to create an IDataInitialize instance." << std::endl;
        goto Cleanup;
    }
    hr = pIDataInitialize->GetDataSource(nullptr, CLSCTX_INPROC_SERVER, connString.c_str(), 
                                         IID_IDBInitialize, reinterpret_cast<IUnknown**>(&pIDBInitialize));
    if (FAILED(hr))
    {
        std::cout << "Failed to get data source object." << std::endl;
        goto Cleanup;
    }
    hr = pIDBInitialize->Initialize();
    if (FAILED(hr))
    {
        std::cout << "Failed to establish connection." << std::endl;
        goto Cleanup;
    }

Cleanup:
    if (pIDBInitialize)
    {
        pIDBInitialize->Uninitialize();
        pIDBInitialize->Release();
    }
    if (pIDataInitialize)
    {
        pIDataInitialize->Release();
    }

    CoUninitialize();
}
```

## Next steps
- [Authorize access to Azure Active Directory web applications using the OAuth 2.0 code grant flow](https://go.microsoft.com/fwlink/?linkid=2072672).

- Learn about [Azure Active Directory Authentication](https://go.microsoft.com/fwlink/?linkid=2073783) to SQL Server.

- Configure driver connections using [connection string keywords](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md) the OLE DB driver supports.
