---
title: "Using Azure Active Directory Authentication with SqlClient"
description: "Describes how to use supported Azure Active Directory Authentication modes to connect to Azure SQL Database with SqlClient"
ms.date: "11/10/2020"
dev_langs: 
  - "csharp"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: karinazhou
ms.author: v-jizho2
ms.reviewer: 
---


# Using Azure Active Directory Authentication with SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This article gives an introduction on how to connect to Azure SQL data sources using Azure Active Directory authentication from a .NET application with SqlClient. 

The Azure Active Directory (Azure AD) authentication is a mechanism using identities in Azure Active Directory to access Azure SQL data sources such as Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. The **Microsoft.Data.SqlClient** allows client applications to specify Azure AD credentials in different kinds of supported Azure AD authentication modes when connecting to Azure SQL Database. By setting the `Authentication` connection property in the connection string, the client can choose a preferred Azure AD authentication mode according to the values provided. For more information about Azure AD authentication, see [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).

Starting from **Microsoft.Data.SqlClient** 2.0.0, the support for `Active Directory Password` authentication, `Active Directory Integrated authentication`, and `Active Directory Interactive` authentication has been extended across .NET Framework, .NET Core, and .NET Standard. A new `Active Directory Service Principal` authentication mode is also added in SqlClient 2.0.0 that makes use of the client ID and secret of a service principal identity to accomplish the authentication. More authentication modes are now brought in SqlClient 2.1.0 including `Active Directory Device Code Flow` and `Active Directory Managed Identity` (also known as `Active Directory MSI`). These new modes enable the application to acquire access token to establish connection to the SQL server. More information about all the Active Directory authentications are covered in the following sections.


## Setting Azure Active Directory authentication in connection string

When connecting to Azure SQL data sources, the client application needs to provide a valid Azure AD authentication name. This table lists the supported authentication modes, which can be specified with `Authentication` connection property.

| Value | Description  | Framework    | Microsoft.Data.SqlClient Version |
|:--|:--|:--|:--:|
| Active Directory Password | Authenticate with an Azure AD identity using username and password | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+  | 1.0+|
| Active Directory Integrated |Authenticate with an Azure AD identity using integrated authentication | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Interactive | Authenticate with an Azure AD identity using interactive mode  | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Service Principal | Authenticate with an Azure AD identity using the client ID and secret of a service principal identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+ |
| Active Directory Device Code Flow | Authenticate with an Azure AD identity using Device Code Flow mode | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |
| Active Directory Managed Identity, <br>Active Directory MSI | Authenticate with an Azure AD identity using system-assigned or user-assigned managed identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |

> [!NOTE]
> <sup>1</sup> Before **Microsoft.Data.SqlClient** 2.0.0, the `Active Directory Integrated` and `Active Directory Interactive` authentications are only supported on .NET Framework 4.6+. 


## Connecting with Active Directory Password authentication

The `Active Directory Password` authentication mode supports authentication to Azure data sources with Azure AD for native or federated Azure AD users. When using this mode, user credentials must be provided in the connection string. The following example shows how to use the `Active Directory Password` authentication.

```c#
// Use your own Server, Database, User Id, and Password.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Password; Database=testdb; User Id=user@domain.com; Password=***";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Connecting with Active Directory Integrated authentication

To use the `Active Directory Integrated` authentication mode, you need to federate the on-premise Active Directory Federation Service (ADFS) with Azure AD in the cloud. When logged in to a domain-joined machine, you can access Azure SQL data sources without being prompted for credentials with this mode. The username and password cannot be specified in the connection string. The Credential property of SqlConnection cannot be set in this mode. The following code snippet is an example when the `Active Directory Integrated` authentication is in use.

```c#
// Use your own Server and Database.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Connecting with Active Directory Interactive authentication

The `Active Directory Interactive` authentication uses Azure Multi-factor authentication technology to set up connections to Azure SQL data sources. If this authentication mode is provided in the connection string, an Azure authentication screen will be triggered and ask the client to enter the valid credential. The password cannot be specified in the connection string. The Credential property of SqlConnection cannot be set in this mode. With **Microsoft.Data.SqlClient** 2.0.0 and above, the username is allowed in the connection string when in the interactive mode. The following example displays how to use `Active Directory Interactive` authentication.

```c#
// Use your own Server, Database, and User Id.
// User Id is optional.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Connecting with Active Directory Service Principal authentication

In `Active Directory Service Principal` authentication mode, the client application can connect to Azure SQL data sources by providing the client ID and secret of a service principal identity. The Service Principal authentication involves setting up an App registration with a secret, granting permissions to the App in the Azure SQL Database instance, and then connecting with the correct credential. The following example shows how to use the `Active Directory Service Principal` authentication.

```c#
// Use your own Server, Database, AppId , and secret.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Service Principal; Database=testdb; User Id=AppId; Password=secret";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Connecting with Active Directory Device Code Flow authentication

With [Microsoft Authentication Library](/azure/active-directory/develop/msal-overview) for .NET (MSAL.NET), the `Active Directory Device Code Flow` authentication enables the client application to connect to Azure SQL data sources from devices and operating systems that do not provide a web browser. The interactive authentication will be performed on another device. For more information about device authentication grant flow, see [OAuth2.0 Device Code Flow](/azure/active-directory/develop/v2-oauth2-device-code) . When this mode is in use, the Credential property of SqlConnection cannot be set. Also, the username and password must not be specified in the connection string. The following code snippet gives an example when the `Active Directory Device Code Flow` authentication is in use.

```c#
// Use your own Server and Database.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Device Code Flow; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Connecting with Active Directory Managed Identity authentication

**Managed Identities** for Azure resources is the new name for the service formerly known as **Managed Service Identity (MSI)**. When the client application uses any Azure resources to access any Azure services that support Azure AD authentication, **Managed Identities** can be used to authenticate to Azure services by providing an identity for the Azure resource in Azure AD and use it to obtain Azure AD tokens. This can eliminate the need for developers having to manage credentials and secrets. There are two types of **Managed Identities: _System-assigned Managed Identity_ and _User-assigned Managed Identity_. The _System-assigned Managed Identity_ is an identity created on a service instance in Azure AD. It is tied to the lifecycle of that service instance. _User-assigned Managed Identity_ is created as a standalone Azure resource. It can be assigned to one or more instances of an Azure service. For more information about **Managed Identities**, see [About managed identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/overview).

Since **Microsoft.Data.SqlClient** 2.1.0, the driver now supports authentication to Azure SQL Database, Azure Synapse, and Azure SQL Managed Instance by acquiring access token from configured Managed Identity. To use this authentication, specify either `Active Directory Managed Identity` or `Active Directory MSI` in the connection string and no password is required. The Credential property of SqlConnection cannot be set in this mode either. For _User-assigned Managed Identity_, username must be provided to set up connection. The following example displays how to use the `Active Directory Managed Identity` authentication with _System-assigned Managed Identity_.

```c#
// For System Assigned Managed Identity
// Use your own Server and Database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Database=testdb";

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

The following code snippet shows an example when the `Active Directory Managed Identity` authentication is in use with _User-assigned Managed Identity_.

```c#
// For User Assigned Managed Identity
// Use your own Server, Database, and User Id.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; User Id=ObjectIdOfManagedIdentity; Database=testdb";

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; User Id=ObjectIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Customizing Active Directory authentication with ActiveDirectoryAuthenticationProvider class

Besides using the default Active Directory authentication implemented by the driver, **Microsoft.Data.SqlClient** 2.1.0 and later now provide the option for the client application to customize their authentication procedure. The customization is based on the enhanced _ActiveDirectoryAuthenticationProvider_ class, which is derived from the [_SqlAuthenticationProvider_](/dotnet/api/system.data.sqlclient.sqlauthenticationprovider) abstract class. During Active Directory authentication, the client application can define its own _ActiveDirectoryAuthencationProvider_ by either using customized callback method or passing `Application Client Id` to the MSAL library via SqlClient driver for fetching access tokens.

The following example displays how to provide custom callback logic when the `Active Directory Device Code Flow` authentication is in use. 

[!code-csharp [AADAuthenticationCustomDeviceFlowCallback#1](~/../sqlclient/doc/samples/AADAuthenticationCustomDeviceFlowCallback.cs#1)]

With a customized _ActiveDirectoryAuthenticationProvider_, a user-defined application client ID can be passed to SqlClient driver when a supported AD authentication<sup>1</sup> is in use. Also, the application client ID is also configurable under `SqlAuthenticationProviderConfigurationSection` and `SqlClientAuthenticationProviderConfigurationSection`<sup>2</sup>. 

> [!NOTE]
> 
> <sup>1</sup> The supported AD authentication modes include `Active Directory Password`, `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Service Principal`, and `Active Directory Device Code Flow`. 
> 
> <sup>2</sup> The new configuration property `applicationClientId` applies to .NET Frameowrk 4.6+ and .NET Core 2.1+.

The following code snippet is an example of using a customized _ActiveDirectoryAuthenticationProvider_ with user-defined application client ID when `Active Directory Interactive` authentication is in use.

[!code-csharp [ApplicationClientIdAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/ApplicationClientIdAzureAuthenticationProvider.cs#1)]

The following example is about how to set application client ID in configuration section.

```xml
<configuration>
  <configSections>
    <section name="SqlClientAuthenticationProviders"
             type="Microsoft.Data.SqlClient.SqlClientAuthenticationProviderConfigurationSection, Microsoft.Data.SqlClient" />
  </configSections>
  <SqlClientAuthenticationProviders applicationClientId ="<GUID>" />
</configuration>

<!--or-->

<configuration>
  <configSections>
    <section name="SqlAuthenticationProviders"
             type="Microsoft.Data.SqlClient.SqlAuthenticationProviderConfigurationSection, Microsoft.Data.SqlClient" />
  </configSections>
  <SqlAuthenticationProviders applicationClientId ="<GUID>" />
</configuration>
```

Given more flexibility, the client application can also use its own provider for AD authentication instead of using the _ActiveDirectoryAuthenticationProvider_ class. The custom authentication provider needs to be a subclass of _SqlAuthenticationProvider_ with overridden methods. The following example displays how to use a new authentication provider for `Active Directory Device Code Flow` authentication.

[!code-csharp [CustomDeviceCodeFlowAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/CustomDeviceCodeFlowAzureAuthenticationProvider.cs#1)]

In addition, to improve the `Active Directory Interactive` authentication experience, Microsoft.Data.SqlClient 2.1.0 and later provide the following new APIs to allow the client application to determine the interactive way based on their requirement. 

```c#
public class ActiveDirectoryAuthenticationProvider
{
    // For .NET Framework targeted applications only
    public void SetIWin32WindowFunc(Func<IWin32Window> iWin32WindowFunc);

    // For .NET Standard targeted applications only
    public void SetParentActivityOrWindowFunc(Func<object> parentActivityOrWindowFunc);

    // For .NET Framework, .NET Core and .NET Standard targeted applications
    public void SetAcquireAuthorizationCodeAsyncCallback(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback);
}
```


## See Also
[Application and service principal objects in Azure Active Directory](/azure/active-directory/develop/app-objects-and-service-principals)