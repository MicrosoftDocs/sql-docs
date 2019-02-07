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
> When using the Azure Active Directory options with the OLE DB driver, ensure that the [Active Directory Authentication Library for SQL Server](https://go.microsoft.com/fwlink/?LinkID=513072) has been installed. ADAL isn't  required for the other authentication methods or OLE DB operations.

## New connection string keywords and properties
The following connection string keywords have been introduced to support Azure Active Directory authentication:

|Connection keyword|Connection property|Description|
|---               |---                |---        |
|Access Token|SSPROP_AUTH_ACCESS_TOKEN|Specifies an access token to authenticate to Azure Active Directory. |
|Authentication|SSPROP_AUTH_MODE|Specifies authentication method to use.|

For more information about the new keywords/properties, see the following pages:
- [Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Initialization and Authorization Properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)

## New encryption and certificate validation behavior
This section discusses the changes in encryption and certificate validation behavior. These changes are **only** effective when using the new Authentication and Access Token connection string keywords (or their corresponding properties).

### Encryption:
To improve security, the new connection attributes/keywords change encryption setting to `yes`,  **unless** the value is already explicitly set, in which case the value previously set is respected and not overridden.

> [!NOTE]   
> in ADO applications and in applications that obtain `IDBInitialize` interface through `IDataInitialize::GetDataSource`, the Core Component implementing the interface explicitly sets encryption to its default value (that is, `no`). As a result, the new authentication attributes/keywords respect this setting and encryption **isn't** enabled. Therefore, it is **recommended** that these applications explicitly set `Use Encryption for Data=true` to override the default value.


### Certificate validation:
In legacy connection attributes/keywords, server certificate was validated only when encryption was set to `yes` by the **client** application. As a result, server certificate **wasn't** validated even if `Force Protocol Encryption` setting was enabled on the server and encryption was used for data.

To improve security, the new connection attributes/keywords respect the `TrustServerCertificate` setting (or its corresponding connection string keywords/properties) **independent of encryption setting** on the client **and** server. As a result, server certificate is validated by default.

> [!NOTE]   
> Certificate validation can also be controlled through the `Value` field of the `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI18.0\GeneralFlags\Flag2` registry entry. Valid values are `0` or `1`. The OLE DB driver picks the most secure option between the registry setting and the connection setting set by client applications. That is, if one of the registry or the connection setting specify that server certificate should not be trusted, driver will **not** trust server certificate and the certificate is validated.

## GUI additions for Azure Active Directory
The driver graphical user interface has been enhanced to allow Azure Active Directory authentication. For more information, see:
- [SQL Server Login Dialog](../help-topics/sql-server-login-dialog.md)
- [Universal Data Link (UDL) Configuration](../help-topics/data-link-pages.md)

## Example connection strings:
This section shows examples of new and existing connection string keywords to be used with `IDataInitialize::GetDataSource` and `DBPROP_INIT_PROVIDERSTRING` property.

> [!NOTE]   
> When using `IDataInitialize::GetDataSource` or when using the Microsoft OLE DB Driver for SQL Server through ADO, the Microsoft Data Access Components (MDAC) explicitly sets the value of `Use Encryption for Data` to `false`, which overrides the default even in the new authentication modes. In addition, server certificate is **not** validated even if encryption is forced by the server. In such cases it's **recommended** that user applications explicitly specify `Use Encryption for Data=true` in the connection string to improve security.

### SQL authentication

#### New syntax
The client requests encryption by default and the server certificate gets validated independent of the encryption setting (unless `TrustServerCertificate` is set to `true`). The login ID and password are passed in the connection string:<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=SqlPassword;User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];Authentication=SqlPassword;UID=[username];PWD=[password];encrypt=yes`

#### Deprecated syntax

Server certificate is **only** validated when encryption is enabled by the client application. Additionally, when using this method encryption is disabled by default. The login ID and password are passed in the connection string:<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];UID=[username];PWD=[password];encrypt=yes`

### Integrated windows authentication using Security Support Provider Interface  (SSPI)

#### New syntax
The client requests encryption by default and the server certificate gets validated independent of the encryption setting (unless `TrustServerCertificate` is set to `true`). The credentials of the logged-in user are used for authentication:<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];Authentication=ActiveDirectoryIntegrated;encrypt=yes`

#### Deprecated syntax

Server certificate is **only** validated when encryption is enabled by the client application. Additionally, encryption is disabled by default. The credentials of the logged-in user are used for authentication:<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Integrated Security=SSPI;Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];Trusted_Connection=yes;encrypt=yes`

### AAD username and password authentication using ADAL
Server certificate gets validated independent of the encryption setting (unless `TrustServerCertificate` is set to `true`). The login ID and password are passed in the connection string:<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryPassword;User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];Authentication=ActiveDirectoryPassword;UID=[username];PWD=[password];encrypt=yes`


### Integrated Azure Active Directory authentication using ADAL
Involves redeeming Windows account credentials for an AAD-issued access token, assuming the target database is in Azure SQL Database. Server certificate gets validated independent of the encryption setting (unless `TrustServerCertificate` is set to `true`):<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > `Server=[server];Database=[database];Authentication=ActiveDirectoryIntegrated;encrypt=yes`

### Azure Active Directory authentication using an Access Token 
Server certificate gets validated independent of the encryption setting (unless `TrustServerCertificate` is set to `true`):<br/>

- Using `IDataInitialize::GetDataSource`:<br/>
    > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Access Token=[access token];Use Encryption for Data=true`<br/>

- Using `DBPROP_INIT_PROVIDERSTRING`:<br/>
    > Providing access token through `DBPROP_INIT_PROVIDERSTRING` isn't supported

## Azure Active Directory authentication code samples

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

## See Also
[Authorize access to Azure Active Directory web applications using the OAuth 2.0 code grant flow](https://docs.microsoft.com/azure/active-directory/develop/v1-protocols-oauth-code)

[Token-based authentication support for Azure SQL DB using Azure AD auth](https://go.microsoft.com/fwlink/?linkid=2068937)

[Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
