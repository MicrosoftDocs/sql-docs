---
title: "Using Azure Active Directory with the OLE DB Driver | Microsoft Docs for SQL Server"
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
# Using Azure Active Directory with the OLE DB driver
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Purpose

The Microsoft OLE DB Driver for SQL Server with version 18.2 or above allows OLE DB applications to connect to an instance of Azure SQL DB using a federated identity. The new authentication methods include login ID and password, an Azure Active Directory access token, or Integrated Authentication. 

## New and modified connection string keywords

The `Authentication` keyword can be used when connecting using connection string to control the authentication mode. 

|Name|Values|Default|Description|
|--- |---   |---    |---        |
|`Access Token`|Any valid access token string|(not set) |if not empty, specifies the Azure AD Access Token to use. It's an error to specify an access token and also `UID`, `PWD`, `Trusted_Connection`, or `Authentication` connection string keywords or their equivalent attributes/keywords.<br/><br/>**NOTE:** Using `AccessToken` connection string keyword through `DBPROP_INIT_PROVIDERSTRING` property is not supported. Instead you can use the `Access Token` keyword in connection strings passed through `IDataInitialize::GetDataSource`. For more information, please view [Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).|
|`Authentication`|(not set), (empty string), `"SqlPassword"`, `"ActiveDirectoryPassword"`, `"ActiveDirectoryIntegrated"`|(not set)|Controls the authentication mode.<table><tr><th>Value<th>Description<tr><td>(not set)<td>Authentication mode determined by other keywords (existing legacy connection options.)<tr><td>(empty string)<td>Override and unset an `Authentication` value.<tr><td>`"SqlPassword"`<td>Directly authenticate to a SQL Server instance using login ID and password. <br/><br/>**IMPORTANT NOTE:** It's **recommended** that applications using `SQL Server` authentication set the value of `Authentication` keyword (or its equivalent property) to `SqlPassword` in order to enable encryption and certificate validation by default.<tr><td>`"ActiveDirectoryPassword"`<td>Authenticate with an Azure Active Directory identity using login ID and password.<tr><td>`"ActiveDirectoryIntegrated"`<td>Authenticate using integrated authentication.</table>|
|`Encrypt` (Or `Use Encryption for Data`)|(not set), `Yes`, `No`|(see description)|It Controls encryption for a connection. <br/><br/>In ADO applications or in applications that obtain `IDBInitialize` interface through `IDataInitialize::GetDataSource`, the default is `No`.<br/><br/> In non-ADO applications or in applications that **do not** obtain `IDBInitialize` interface through `IDataInitialize::GetDataSource`, and `Authentication` keyword (or its equivalent property) is set to an acceptable and non-empty value, the default is `Yes`. In all other cases, the default is `No`.<br/><br/> **IMPORTANT NOTE:** In ADO application and in applications that obtain  `IDBInitialize` interface through `IDataInitialize::GetDataSource`, it's **recommended** to explicitly set `encrypt=Yes` to override the default and improve security.|
|`TrustServerCertificate` (Or `Trust Server Certificate`)|(not set), `Yes`, `No`|(see description)|Controls server certificate validation for a connection. The default is `No` (That is, Server certificate is validated). If one of the `Windows` or `SQL Server` authentication methods are used, server certificates will be validated **only** if encryption is enabled. However, in the new authentication methods, that is, if the value of at least one of the `Authentication` or `Access Token` keywords (or their equivalent connection properties) is set to an acceptable and non-empty value, the value of this option is considered **regardless of encryption setting**. This feature was added since OLE DB Driver 18.2 for improvement in security.<br/><br/>**IMPORTANT NOTE:** It's **recommended** that applications using `Windows` or `SQL Server` authentication methods switch to the new `ActiveDirectoryIntegrated` and `SqlPassword` authentication methods to improve security.|

## New and modified connection attributes

The following connection attributes have either been introduced or modified to support Azure Active Directory authentication.

|Attribute|Type|Values|Default|Description|
|-|-|-|-|-|
|`SSPROP_AUTH_ACCESS_TOKEN`|`VT_BSTR`|Any valid access token|`VT_EMPTY`|If not an empty string, specifies the AzureAD Access Token to use. It is an error to specify an access token and also `UID`, `PWD`, `Trusted_Connection`, or `Authentication` connection string keywords or their equivalent attributes/keywords.|
|`DBPROP_AUTH_INTEGRATED`|`VT_BSTR`|(not set), `"SSPI"`|(not set)|_Deprecated_; Set `SSPROP_AUTH_MODE` to `ActiveDirectoryIntegrated` instead. <br><br>Forces use of Windows Authentication for access validation on server login.|
|`SSPROP_AUTH_MODE`|`VT_BSTR`|(empty string), `"ActiveDirectoryPassword"`, `"ActiveDirectoryIntegrated"`, `"SqlPassword"`|`VT_EMPTY`|See description of `Authentication` keyword above|
|`SSPROP_AUTH_OLD_PASSWORD`|`VT_BSTR`|(not set)|`VT_EMPTY`|Not supported with Azure Active Directory, since password changes to AAD principals cannot be accomplished through an OLE DB connection.|
|`SSPROP_INIT_ENCRYPT`|`VT_BOOL`|`VARIANT_FALSE`, `VARIANT_TRUE`|(see description)|Controls encryption for a connection. `VARIANT_FALSE` and `VARIANT_TRUE` disable and enable encryption, respectively. <br/><br/>In applications that obtain `IDBInitialize` interface through `IDataInitialize::GetDataSource`, the default is `VARIANT_FALSE`.<br/><br/> In applications that **do not** obtain `IDBInitialize` interface through `IDataInitialize::GetDataSource`, **and** at least one of `SSPROP_AUTH_MODE` or `SSPROP_AUTH_ACCESS_TOKEN` properties  are set to an acceptable and non-empty value, the default is `VARIANT_TRUE`. <br/><br/>In all other cases, the default is `VARIANT_FALSE`.<br/><br/> **IMPORTANT NOTE:** If `IDBInitialize` interface is obtained through `IDataInitialize::GetDataSource`, it's **recommended** that client applications explicitly set `SSPROP_INIT_ENCRYPT` to `VARIANT_TRUE` in order to override the default and improve security.|
|`SSPROP_INIT_TRUST_SERVER_CERTIFICATE`|`VT_BOOL`|`VARIANT_FALSE`, `VARIANT_TRUE`|(See description)|Controls server certificate validation for a connection. The default is `VARIANT_FALSE`. If one of the `Windows` or `SQL Server` authentication methods are used, server certificates will be validated **only** if encryption is enabled. However, in the new authentication methods, that is, if the value of  at least one of the `SSPROP_AUTH_MODE` (or its equivalent connection string keyword) or `SSPROP_AUTH_ACCESS_TOKEN` properties is set to an acceptable and non-empty value, the value of this option is considered **regardless of encryption setting**. This feature was added since OLE DB Driver 18.2 for improvement in security.<br/><br/>**IMPORTANT NOTE:** It is **recommended** that applications using `Windows` or `SQL Server` authentication methods switch to the new `ActiveDirectoryIntegrated` and `SqlPassword` authentication methods to improve security.|


## UI additions for Azure Active Directory
The driver user interfaces have been enhanced with the additional options necessary for using authentication with Azure AD. For more information please visit:
- [SQL Server Login Dialog](../help-topics/sql-server-login-dialog.md)
- [Universal Data Link (UDL) Configuration](../help-topics/data-link-pages.md)

## Example connection strings:
This section shows examples of new and existing connection string keywords to be used with `IDataInitialize::GetDataSource` and `IDBInitialize::Initialize`.

> [!NOTE]   
> When using `IDataInitialize::GetDataSource` or when using the Microsoft OLE DB Driver for SQL Server through ADO, the Microsoft Data Access Components (MDAC) explicitly sets the value of `Use Encryption for Data` to `false`, which overrides the default even in the new authentication modes. In addition, server certificate is **not** validated even if encryption is forced by the server. In such cases it's **recommended** that user applications explicitly specify `Use Encryption for Data=true` in the connection string to improve security.

1. `SQL Server Authentication` - **deprecated** syntax. Server certificate is **only** validated when encryption is enabled by the client application. Additionally, when using this method encryption is disabled by default. The login ID and password are passed in the connection string:<br/>

    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];UID=[username];PWD=[password];encrypt=yes`

2. `SQL Authentication` - **new** syntax. The client requests encryption by default and the server certificate gets validated regardless of the encryption setting. The login ID and password are passed in the connection string:<br/>

    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=SqlPassword;User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];Authentication=SqlPassword;UID=[username];PWD=[password];encrypt=yes`

3. `Integrated Windows Authentication` using Security Support Provider Interface (SSPI) - **deprecated** syntax.  Server certificate is **only** validated when encryption is enabled by the client application. Additionally, encryption is disabled by default. The credentials of the logged-in user are used for authentication:<br/>

    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Integrated Security=SSPI;Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];Trusted_Connection=yes;encrypt=yes`

4. `Integrated Windows Authentication` using Security Support Provider Interface  (SSPI) - **new** syntax. The client requests encryption by default and the server certificate gets validated regardless of the encryption setting. The credentials of the logged-in user are used for authentication:<br/>

    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];Authentication=ActiveDirectoryIntegrated;encrypt=yes`

5. `AAD Username and Password Authentication` using ADAL. Server certificate gets validated, regardless of the encryption setting. The login ID and password are passed in the connection string:<br/>

    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryPassword;User ID=[username];Password=[password];Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];Authentication=ActiveDirectoryPassword;UID=[username];PWD=[password];encrypt=yes`


6. `Integrated Azure Active Directory Authentication` using ADAL, which involves redeeming Windows account credentials for an AAD-issued access token, assuming the target database is in Azure SQL Database. Server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`):<br/>
    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Authentication=ActiveDirectoryIntegrated;Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > `Server=[server];Database=[database];Authentication=ActiveDirectoryIntegrated;encrypt=yes`

7. `Active Directory Authentication using an Access Token`. Server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`):<br/>
    - Using `IDataInitialize::GetDataSource`:<br/>
        > `Provider=MSOLEDBSQL;Data Source=[server];Initial Catalog=[database];Access Token=[access token];Use Encryption for Data=true`<br/>

    - Using `IDBInitialize::Initialize`:<br/>
        > Providing access token through `DBPROP_INIT_PROVIDERSTRING` is not supported

> [!NOTE] 
>- When using the new Active Directory options with the Windows OLE DB driver, ensure that the [Active Directory Authentication Library for SQL Server](https://go.microsoft.com/fwlink/?LinkID=513072) has been installed. For driver version 18.2 and later, this is not an explicit dependency since it's not required for the other authentication methods or OLE DB operations.
>- To connect using a SQL Server account login ID and password, you may now use the new `SqlPassword` option, which is recommended since this option enables more secure connection defaults.
>- To connect using an Azure Active Directory account login ID and password, specify `Authentication=ActiveDirectoryPassword` in the connection string and the `UID` and `PWD` keywords with the login ID and password, respectively.
>- To connect using `Windows Integrated` or `Active Directory Integrated` authentication, specify `Authentication=ActiveDirectoryIntegrated` in the connection string. The driver will choose the correct authentication mode automatically. `UID` and `PWD` must **not** be specified.

## Azure Active Directory Authentication Code Samples

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
[Token-based authentication support for Azure SQL DB using Azure AD auth](https://go.microsoft.com/fwlink/?linkid=2068937)

