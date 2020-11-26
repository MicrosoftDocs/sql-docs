---
title: "Using Azure Active Directory authentication with SqlClient"
description: "Describes how to use supported Azure Active Directory authentication modes to connect to Azure SQL data sources with SqlClient"
ms.date: "11/20/2020"
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

# Using Azure Active Directory authentication with SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This article describes how to connect to Azure SQL data sources using Azure Active Directory authentication from a .NET application with SqlClient.

Azure Active Directory (Azure AD) authentication uses identities in Azure Active Directory to access Azure SQL data sources such as Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. **Microsoft.Data.SqlClient** allows client applications to specify Azure AD credentials in different authentication modes when connecting to Azure SQL Database. By setting the `Authentication` connection property in the connection string, the client can choose a preferred Azure AD authentication mode according to the value provided. For more information about Azure AD authentication, see [Connecting to SQL Database By Using Azure Active Directory authentication](/azure/azure-sql/database/authentication-aad-overview).

The early **Microsoft.Data.SqlClient** supports `Active Directory Password` for .NET Framework, .NET Core, and .NET Standard. It also supports `Active Directory Integrated` authentication and `Active Directory Interactive` authentication for .NET Framework. Starting with **Microsoft.Data.SqlClient** 2.0.0, support for `Active Directory Integrated authentication` and `Active Directory Interactive` authentication has been extended across .NET Framework, .NET Core, and .NET Standard. A new `Active Directory Service Principal` authentication mode is also added in SqlClient 2.0.0 that makes use of the client ID and secret of a service principal identity to accomplish authentication. More authentication modes are added in SqlClient 2.1.0 including `Active Directory Device Code Flow` and `Active Directory Managed Identity` (also known as `Active Directory MSI`). These new modes enable the application to acquire an access token to connect to the server. More information about all the Active Directory authentications are covered in the following sections.


## Setting Azure Active Directory authentication

When connecting to Azure SQL data sources with Azure AD authentication, the application needs to provide a valid authentication mode. This table lists the supported authentication modes, which can be specified with the `Authentication` connection property in the connection string.

| Value | Description  | Framework    | Microsoft.Data.SqlClient Version |
|:--|:--|:--|:--:|
| Active Directory Password | Authenticate with an Azure AD identity using a username and password | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+  | 1.0+|
| Active Directory Integrated |Authenticate with an Azure AD identity using integrated authentication | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Interactive | Authenticate with an Azure AD identity using interactive authentication | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Service Principal | Authenticate with an Azure AD identity using the client ID and secret of a service principal identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+ |
| Active Directory Device Code Flow | Authenticate with an Azure AD identity using Device Code Flow mode | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |
| Active Directory Managed Identity, <br>Active Directory MSI | Authenticate with an Azure AD identity using system-assigned or user-assigned managed identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |

> [!NOTE]
> <sup>1</sup> Before **Microsoft.Data.SqlClient** 2.0.0, `Active Directory Integrated` and `Active Directory Interactive` authentications are only supported on .NET Framework 4.6+. 


## Using Active Directory Password authentication

`Active Directory Password` authentication mode supports authentication to Azure data sources with Azure AD for native or federated Azure AD users. When using this mode, user credentials must be provided in the connection string. The following example shows how to use `Active Directory Password` authentication.

```c#
// Use your own Server, Database, User Id, and Password.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Password; Database=testdb; User Id=user@domain.com; Password=***";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Integrated authentication

To use `Active Directory Integrated` authentication mode, you need to federate the on-premise Active Directory with Azure AD in the cloud. Federation can be done using Active Directory Federation Services (ADFS), for example. When logged in to a domain-joined machine, you can access Azure SQL data sources without being prompted for credentials with this mode. Username and password cannot be specified in the connection string for .NET framework applications. Username is optional in the connection string for .NET Core and .NET Standard applications. The Credential property of SqlConnection cannot be set in this mode. The following code snippet is an example of when `Active Directory Integrated` authentication is in use.

```c#
// Use your own Server and Database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User Id is optional for .NET Core and .NET Standard
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Using Active Directory Interactive authentication

`Active Directory Interactive` authentication supports multi-factor authentication technology to connect to Azure SQL data sources. If this authentication mode is provided in the connection string, an Azure authentication screen will be displayed and ask the user to enter valid credentials. The password cannot be specified in the connection string. The Credential property of SqlConnection cannot be set in this mode. With **Microsoft.Data.SqlClient** 2.0.0 and above, username is allowed in the connection string when in interactive mode. The following example displays how to use `Active Directory Interactive` authentication.

```c#
// Use your own Server, Database, and User Id.
// User Id is optional.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User Id is not provided.
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Using Active Directory Service Principal authentication

In `Active Directory Service Principal` authentication mode, the client application can connect to Azure SQL data sources by providing the client ID and secret of a service principal identity. Service Principal authentication involves setting up an App registration with a secret, granting permissions to the App in the Azure SQL Database instance, and then connecting with the correct credential. The following example shows how to use `Active Directory Service Principal` authentication.

```c#
// Use your own Server, Database, AppId , and secret.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Service Principal; Database=testdb; User Id=AppId; Password=secret";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Device Code Flow authentication

With [Microsoft Authentication Library](/azure/active-directory/develop/msal-overview) for .NET (MSAL.NET), `Active Directory Device Code Flow` authentication enables the client application to connect to Azure SQL data sources from devices and operating systems that do not have an interactive web browser. Interactive authentication will be performed on another device. For more information about device code flow authentication, see [OAuth2.0 Device Code Flow](/azure/active-directory/develop/v2-oauth2-device-code). When this mode is in use, the Credential property of SqlConnection cannot be set. Also, the username and password must not be specified in the connection string. The following code snippet is an example of using `Active Directory Device Code Flow` authentication.

```c#
// Use your own Server and Database.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Device Code Flow; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Managed Identity authentication

**Managed Identities** for Azure resources is the new name for the service formerly known as **Managed Service Identity (MSI)**. When a client application uses an Azure resources to access an Azure service that support Azure AD authentication, **Managed Identities** can be used to authenticate by providing an identity for the Azure resource in Azure AD and use it to obtain access tokens. This can eliminate the need for developers having to manage credentials and secrets. There are two types of **Managed Identities**: _System-assigned Managed Identity_ and _User-assigned Managed Identity_. The _System-assigned Managed Identity_ is an identity created on a service instance in Azure AD. It is tied to the lifecycle of that service instance. _User-assigned Managed Identity_ is created as a standalone Azure resource. It can be assigned to one or more instances of an Azure service. For more information about **Managed Identities**, see [About managed identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/overview).

Since **Microsoft.Data.SqlClient** 2.1.0, the driver now supports authentication to Azure SQL Database, Azure Synapse, and Azure SQL Managed Instance by acquiring access tokens via Managed Identity. To use this authentication, specify either `Active Directory Managed Identity` or `Active Directory MSI` in the connection string and no password is required. The Credential property of SqlConnection cannot be set in this mode either. For _User-assigned Managed Identity_, username must be provided. The following example shows how to use `Active Directory Managed Identity` authentication with _System-assigned Managed Identity_.

```c#
// For System Assigned Managed Identity
// Use your own Server and Database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

The following example demonstrates `Active Directory Managed Identity` authentication with a _User-assigned Managed Identity_.

```c#
// For User Assigned Managed Identity
// Use your own Server, Database, and User Id.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; User Id=ObjectIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; User Id=ObjectIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Customizing Active Directory authentication

Besides using the Active Directory authentication built into the driver, **Microsoft.Data.SqlClient** 2.1.0 and later provide applications the option to customize AD authentication. The customization is based on the _ActiveDirectoryAuthenticationProvider_ class, which is derived from the [_SqlAuthenticationProvider_](/dotnet/api/system.data.sqlclient.sqlauthenticationprovider) abstract class. During Active Directory authentication, the client application can define its own _ActiveDirectoryAuthencationProvider_ by either using a customized callback method or passing `Application Client Id` to the MSAL library via SqlClient driver for fetching access tokens.

The following example displays how to use a custom callback when `Active Directory Device Code Flow` authentication is in use. 

[!code-csharp [AADAuthenticationCustomDeviceFlowCallback#1](~/../sqlclient/doc/samples/AADAuthenticationCustomDeviceFlowCallback.cs#1)]

With a customized _ActiveDirectoryAuthenticationProvider_, a user-defined "Application Client Id" can be passed to SqlClient when a supported Active Directory authentication mode<sup>1</sup> is in use. The "Application Client Id" is also configurable via `SqlAuthenticationProviderConfigurationSection` or `SqlClientAuthenticationProviderConfigurationSection`<sup>2</sup>. 

> [!NOTE]
> 
> <sup>1</sup> Supported AD authentication modes include `Active Directory Password`, `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Service Principal`, and `Active Directory Device Code Flow`. 
> 
> <sup>2</sup> The configuration property `applicationClientId` applies to .NET Frameowrk 4.6+ and .NET Core 2.1+.

The following code snippet is an example of using a customized _ActiveDirectoryAuthenticationProvider_ with a user-defined application client ID when `Active Directory Interactive` authentication is in use.

[!code-csharp [ApplicationClientIdAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/ApplicationClientIdAzureAuthenticationProvider.cs#1)]

The following example shows how to set an application client ID via a configuration section.

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

## Custom SQL Authentication Provider support

Given more flexibility, the client application can also use its own provider for AD authentication instead of using the _ActiveDirectoryAuthenticationProvider_ class. The custom authentication provider needs to be a subclass of _SqlAuthenticationProvider_ with overridden methods. The following example displays how to use a new authentication provider for `Active Directory Device Code Flow` authentication.

[!code-csharp [CustomDeviceCodeFlowAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/CustomDeviceCodeFlowAzureAuthenticationProvider.cs#1)]

In addition, to improving the `Active Directory Interactive` authentication experience, Microsoft.Data.SqlClient 2.1.0 and later provides the following APIs for client applications to customize interactive and device code flow authentication.

```c#
public class ActiveDirectoryAuthenticationProvider
{
    // For .NET Framework targeted applications only
    // Sets a reference to the current System.Windows.Forms.IWin32Window that triggers the browser to be shown. 
    // Used to center the browser pop-up onto this window.
    public void SetIWin32WindowFunc(Func<IWin32Window> iWin32WindowFunc);

    // For .NET Standard targeted applications only
    // Sets a reference to the ViewController (if using Xamarin.iOS), Activity (if using Xamarin.Android) IWin32Window or IntPtr (if using .NET Framework). 
    // Used for invoking the browser for Active Directory Interactive authentication.
    public void SetParentActivityOrWindowFunc(Func<object> parentActivityOrWindowFunc);

    // For .NET Framework, .NET Core and .NET Standard targeted applications
    // Sets a callback method which is invoked with a custom Web UI instance that will let the user sign-in with Azure Active Directory, present consent if needed, and get back the authorization code. 
    // Applicable when working with Active Directory Interactive authentication.
    public void SetAcquireAuthorizationCodeAsyncCallback(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback);

    // For .NET Framework, .NET Core and .NET Standard targeted applications
    // Clears cached user tokens from the token provider.
    public static void ClearUserTokenCache();
}
```


## See Also
- [Application and service principal objects in Azure Active Directory](/azure/active-directory/develop/app-objects-and-service-principals)
- [Authentication flows](/azure/active-directory/develop/msal-authentication-flows)
