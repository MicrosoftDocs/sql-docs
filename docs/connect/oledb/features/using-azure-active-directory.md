---
title: Using Azure Active Directory
description: Learn about the Azure Active Directory authentication methods available in the Microsoft OLE DB Driver for SQL Server that enable connecting to Azure SQL databases.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 02/18/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Azure Active Directory

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Purpose

Starting with version [18.2.1](../release-notes-for-oledb-driver-for-sql-server.md#1821), Microsoft OLE DB Driver for SQL Server allows OLE DB applications to connect to an instance of Azure SQL Database using a federated identity. The new authentication methods include:

- Azure Active Directory login ID and password
- Azure Active Directory access token
- Azure Active Directory integrated authentication
- SQL login ID and password

Version [18.3.0](../release-notes-for-oledb-driver-for-sql-server.md#1830) adds support for the following authentication methods:

- Azure Active Directory interactive authentication
- Azure Active Directory Managed Identity authentication (only from within an [Azure Virtual Machine configured for Managed Identity](/azure/active-directory/managed-identities-azure-resources/qs-configure-portal-windows-vm))

Version [18.5.0](../release-notes-for-oledb-driver-for-sql-server.md#1850) adds support for the following authentication method:

- Azure Active Directory service principal authentication

> [!NOTE]
> Using the following authentication modes with `DataTypeCompatibility` (or its corresponding property) set to `80` is **not** supported:
>
> - Azure Active Directory authentication using login ID and password
> - Azure Active Directory authentication using access token
> - Azure Active Directory integrated authentication
> - Azure Active Directory interactive authentication
> - Azure Active Directory managed identities authentication
> - Azure Active Directory service principal authentication

To use Azure Active Directory authentication, you must configure your Azure SQL data source. For more information, see [Configure and manage Azure AD authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

## Connection string keywords and properties

The following connection string keywords have been introduced to support Azure Active Directory authentication:

|Connection string keyword|Connection property|Description|
|---               |---                |---        |
|Access Token|SSPROP_AUTH_ACCESS_TOKEN|Specifies an access token to authenticate to Azure Active Directory. |
|Authentication|SSPROP_AUTH_MODE|Specifies authentication method to use.|

For more information about the new keywords/properties, see the following pages:

- [Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Initialization and Authorization Properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)

## Encryption and certificate validation

See [Encryption and certificate validation](encryption-and-certificate-validation.md#major-version-18-with-new-authentication-methods) for more information.

## GUI additions

The driver graphical user interface has been enhanced to allow Azure Active Directory authentication. For more information, see:

- [SQL Server Login Dialog](../help-topics/sql-server-login-dialog.md)
- [Universal Data Link (UDL) Configuration](../help-topics/data-link-pages.md)

## Example connection strings

This section shows examples of new and existing connection string keywords to be used with `IDataInitialize::GetDataSource` and `DBPROP_INIT_PROVIDERSTRING` property.

### SQL authentication
- Using `IDataInitialize::GetDataSource`:
    - New:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=SqlPassword**;User ID=[username];Password=[password];Use Encryption for Data=Mandatory
    - Deprecated:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];User ID=[username];Password=[password];Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    - New:
        > Server=[server];Database=[database];**Authentication=SqlPassword**;UID=[username];PWD=[password];Encrypt=Mandatory
    - Deprecated:
        > Server=[server];Database=[database];UID=[username];PWD=[password];Encrypt=Mandatory

### Integrated Windows authentication using Security Support Provider Interface (SSPI)

- Using `IDataInitialize::GetDataSource`:
    - New:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryIntegrated**;Use Encryption for Data=Mandatory
    - Deprecated:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Integrated Security=SSPI**;Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    - New:
        > Server=[server];Database=[database];**Authentication=ActiveDirectoryIntegrated**;Encrypt=Mandatory
    - Deprecated:
        > Server=[server];Database=[database];**Trusted_Connection=yes**;Encrypt=Mandatory

### Azure Active Directory username and password authentication

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryPassword**;User ID=[username];Password=[password];Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryPassword**;UID=[username];PWD=[password];Encrypt=Mandatory

### Azure Active Directory integrated authentication

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryIntegrated**;Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryIntegrated**;Encrypt=Mandatory

### Azure Active Directory authentication using an access token

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Access Token=[access token]**;Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Providing access token through `DBPROP_INIT_PROVIDERSTRING` isn't supported

### Azure Active Directory interactive authentication

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryInteractive**;User ID=[username];Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryInteractive**;UID=[username];Encrypt=Mandatory

### Azure Active Directory Managed Identity authentication

- Using `IDataInitialize::GetDataSource`:
    - User-assigned managed identity:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryMSI**;User ID=[Object ID];Use Encryption for Data=Mandatory
    - System-assigned managed identity:
        > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryMSI**;Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    - User-assigned managed identity:
        > Server=[server];Database=[database];**Authentication=ActiveDirectoryMSI**;UID=[Object ID];Encrypt=Mandatory
    - System-assigned managed identity:
        > Server=[server];Database=[database];**Authentication=ActiveDirectoryMSI**;Encrypt=Mandatory

### Azure Active Directory service principal authentication

- Using `IDataInitialize::GetDataSource`:
    > Provider=MSOLEDBSQL19;Data Source=[server];Initial Catalog=[database];**Authentication=ActiveDirectoryServicePrincipal**;User ID=[Application (client) ID];Password=[Application (client) secret];Use Encryption for Data=Mandatory
- Using `DBPROP_INIT_PROVIDERSTRING`:
    > Server=[server];Database=[database];**Authentication=ActiveDirectoryServicePrincipal**;UID=[Application (client) ID];PWD=[Application (client) secret];Encrypt=Mandatory

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
    std::wstring connString = L"Provider=MSOLEDBSQL19;Data Source=" + std::wstring(azureServer) + L";Initial Catalog=" + 
                              std::wstring(azureDatabase) + L";Access Token=" + accessToken + L";Use Encryption for Data=Mandatory;";
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
    std::wstring connString = L"Provider=MSOLEDBSQL19;Data Source=" + std::wstring(azureServer) + L";Initial Catalog=" + 
                              std::wstring(azureDatabase) + L";Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=Mandatory;";

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
- [Authorize access to Azure Active Directory web applications using the OAuth 2.0 code grant flow](/azure/active-directory/azuread-dev/v1-protocols-oauth-code).

- Learn about [Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview) to SQL Server.

- Configure driver connections using [connection string keywords](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md) the OLE DB driver supports.
