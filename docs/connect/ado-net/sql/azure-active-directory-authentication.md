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
ms.reviewer: v-daenge
---

# Using Azure Active Directory authentication with SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This article describes how to connect to Azure SQL data sources by using Azure Active Directory (Azure AD) authentication from a .NET application with SqlClient.

Azure AD authentication uses identities in Azure AD to access Azure SQL data sources such as Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. The **Microsoft.Data.SqlClient** namespace allows client applications to specify Azure AD credentials in different authentication modes when they're connecting to Azure SQL Database.

When you set the `Authentication` connection property in the connection string, the client can choose a preferred Azure AD authentication mode according to the value provided:

- The earliest **Microsoft.Data.SqlClient** version supports `Active Directory Password` for .NET Framework, .NET Core, and .NET Standard. It also supports `Active Directory Integrated` authentication and `Active Directory Interactive` authentication for .NET Framework. 

- Starting with **Microsoft.Data.SqlClient** 2.0.0, support for `Active Directory Integrated` authentication and `Active Directory Interactive` authentication has been extended across .NET Framework, .NET Core, and .NET Standard. 

  A new `Active Directory Service Principal` authentication mode is also added in SqlClient 2.0.0. It makes use of the client ID and secret of a service principal identity to accomplish authentication. 

- More authentication modes are added in **Microsoft.Data.SqlClient** 2.1.0, including `Active Directory Device Code Flow` and `Active Directory Managed Identity` (also known as `Active Directory MSI`). These new modes enable the application to acquire an access token to connect to the server. 

For information about Azure AD authentication beyond what the following sections describe, see [Connecting to SQL Database by using Azure Active Directory authentication](/azure/azure-sql/database/authentication-aad-overview).

## Setting Azure Active Directory authentication

When the application is connecting to Azure SQL data sources by using Azure AD authentication, it needs to provide a valid authentication mode. The following table lists the supported authentication modes. The application specifies a mode by using the `Authentication` connection property in the connection string.

| Value | Description  | Framework    | Microsoft.Data.SqlClient version |
|:--|:--|:--|:--:|
| Active Directory Password | Authenticate with an Azure AD identity by using a username and password | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+  | 1.0+|
| Active Directory Integrated |Authenticate with an Azure AD identity by using integrated authentication | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Interactive | Authenticate with an Azure AD identity by using interactive authentication | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+<sup>1</sup> |
| Active Directory Service Principal | Authenticate with an Azure AD identity by using the client ID and secret of a service principal identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.0.0+ |
| Active Directory Device Code Flow | Authenticate with an Azure AD identity by using Device Code Flow mode | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |
| Active Directory Managed Identity, <br>Active Directory MSI | Authenticate with an Azure AD identity by using system-assigned or user-assigned managed identity | .NET Framework 4.6+, .NET Core 2.1+, .NET Standard 2.0+ | 2.1.0+ |

<sup>1</sup> Before **Microsoft.Data.SqlClient** 2.0.0, `Active Directory Integrated` and `Active Directory Interactive` authentication modes are supported only on .NET Framework 4.6+.

## Using Active Directory Password authentication

`Active Directory Password` authentication mode supports authentication to Azure data sources with Azure AD for native or federated Azure AD users. When you're using this mode, user credentials must be provided in the connection string. The following example shows how to use `Active Directory Password` authentication.

```c#
// Use your own server, database, user ID, and password.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Password; Database=testdb; User Id=user@domain.com; Password=***";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Integrated authentication

To use `Active Directory Integrated` authentication mode, you need to federate the on-premises Active Directory instance with Azure AD in the cloud. You can do federation by using Active Directory Federation Services (AD FS), for example.

When you're signed in to a domain-joined machine, you can access Azure SQL data sources without being prompted for credentials with this mode. You can't specify username and password in the connection string for .NET Framework applications. Username is optional in the connection string for .NET Core and .NET Standard applications. You can't set the `Credential` property of SqlConnection in this mode. 

The following code snippet is an example of when `Active Directory Integrated` authentication is in use.

```c#
// Use your own server and database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User ID is optional for .NET Core and .NET Standard.
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Using Active Directory Interactive authentication

`Active Directory Interactive` authentication supports multifactor authentication technology to connect to Azure SQL data sources. If you provide this authentication mode in the connection string, an Azure authentication screen will appear and ask the user to enter valid credentials. You can't specify the password in the connection string. 

You can't set the `Credential` property of SqlConnection in this mode. With **Microsoft.Data.SqlClient** 2.0.0 and later, username is allowed in the connection string when you're in interactive mode. 

The following example shows how to use `Active Directory Interactive` authentication.

```c#
// Use your own server, database, and user ID.
// User ID is optional.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User ID is not provided.
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```


## Using Active Directory Service Principal authentication

In `Active Directory Service Principal` authentication mode, the client application can connect to Azure SQL data sources by providing the client ID and secret of a service principal identity. Service principal authentication involves:
1. Setting up an app registration with a secret.
1. Granting permissions to the app in the Azure SQL Database instance.
1. Connecting with the correct credential. 

The following example shows how to use `Active Directory Service Principal` authentication.

```c#
// Use your own server, database, app ID, and secret.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Service Principal; Database=testdb; User Id=AppId; Password=secret";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Device Code Flow authentication

With [Microsoft Authentication Library](/azure/active-directory/develop/msal-overview) for .NET (MSAL.NET), `Active Directory Device Code Flow` authentication enables the client application to connect to Azure SQL data sources from devices and operating systems that don't have an interactive web browser. Interactive authentication will be performed on another device. For more information about device code flow authentication, see [OAuth 2.0 Device Code Flow](/azure/active-directory/develop/v2-oauth2-device-code). 

When this mode is in use, you can't set the `Credential` property of `SqlConnection`. Also, the username and password must not be specified in the connection string. 

The following code snippet is an example of using `Active Directory Device Code Flow` authentication.

```c#
// Use your own server and database.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Device Code Flow; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```


## Using Active Directory Managed Identity authentication

*Managed Identities* for Azure resources is the new name for the service formerly known as Managed Service Identity (MSI). When a client application uses an Azure resource to access an Azure service that supports Azure AD authentication, you can use managed identities to authenticate by providing an identity for the Azure resource in Azure AD. You can then use that identity to obtain access tokens. This authentication method can eliminate the need to manage credentials and secrets.

There are two types of managed identities:

- _System-assigned managed identity_ is created on a service instance in Azure AD. It's tied to the lifecycle of that service instance. 
- _User-assigned managed identity_ is created as a standalone Azure resource. It can be assigned to one or more instances of an Azure service. 

For more information about managed identities, see [About managed identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/overview).

Since **Microsoft.Data.SqlClient** 2.1.0, the driver supports authentication to Azure SQL Database, Azure Synapse Analytics, and Azure SQL Managed Instance by acquiring access tokens via managed identity. To use this authentication, specify either `Active Directory Managed Identity` or `Active Directory MSI` in the connection string, and no password is required. 

You can't set the `Credential` property of `SqlConnection` in this mode either. For a user-assigned managed identity, the object id of the managed identity must be provided. 

The following example shows how to use `Active Directory Managed Identity` authentication with a system-assigned managed identity.

```c#
// For system-assigned managed identity
// Use your own server and database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

The following example demonstrates `Active Directory Managed Identity` authentication with a user-assigned managed identity.

```c#
// For user-assigned managed identity
// Use your own values for Server, Database, and User Id.
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

Besides using the Active Directory authentication built into the driver, **Microsoft.Data.SqlClient** 2.1.0 and later provide applications the option to customize Active Directory authentication. The customization is based on the `ActiveDirectoryAuthenticationProvider` class, which is derived from the [`SqlAuthenticationProvider`](/dotnet/api/system.data.sqlclient.sqlauthenticationprovider) abstract class. 

During Active Directory authentication, the client application can define its own `ActiveDirectoryAuthencationProvider` class by either:

- Using a customized callback method.
- Passing an application client ID to the MSAL library via SqlClient driver for fetching access tokens.

The following example displays how to use a custom callback when `Active Directory Device Code Flow` authentication is in use. 

[!code-csharp [AADAuthenticationCustomDeviceFlowCallback#1](~/../sqlclient/doc/samples/AADAuthenticationCustomDeviceFlowCallback.cs#1)]

With a customized `ActiveDirectoryAuthenticationProvider` class, a user-defined application client ID can be passed to SqlClient when a supported Active Directory authentication mode is in use. Supported Active Directory authentication modes include `Active Directory Password`, `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Service Principal`, and `Active Directory Device Code Flow`.

The application client ID is also configurable via `SqlAuthenticationProviderConfigurationSection` or `SqlClientAuthenticationProviderConfigurationSection`. The configuration property `applicationClientId` applies to .NET Framework 4.6+ and .NET Core 2.1+.

The following code snippet is an example of using a customized `ActiveDirectoryAuthenticationProvider` class with a user-defined application client ID when `Active Directory Interactive` authentication is in use.

[!code-csharp [ApplicationClientIdAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/ApplicationClientIdAzureAuthenticationProvider.cs#1)]

The following example shows how to set an application client ID through a configuration section.

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

## Support for a custom SQL authentication provider

Given more flexibility, the client application can also use its own provider for Active Directory authentication instead of using the `ActiveDirectoryAuthenticationProvider` class. The custom authentication provider needs to be a subclass of `SqlAuthenticationProvider` with overridden methods. 

The following example shows how to use a new authentication provider for `Active Directory Device Code Flow` authentication.

[!code-csharp [CustomDeviceCodeFlowAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/CustomDeviceCodeFlowAzureAuthenticationProvider.cs#1)]

In addition to improving the `Active Directory Interactive` authentication experience, **Microsoft.Data.SqlClient** 2.1.0 and later provide the following APIs for client applications to customize interactive authentication and device code flow authentication.

```c#
public class ActiveDirectoryAuthenticationProvider
{
    // For .NET Framework targeted applications only
    // Sets a reference to the current System.Windows.Forms.IWin32Window that triggers the browser to be shown. 
    // Used to center the browser pop-up onto this window.
    public void SetIWin32WindowFunc(Func<IWin32Window> iWin32WindowFunc);

    // For .NET Standard targeted applications only
    // Sets a reference to the ViewController (if using Xamarin.iOS), Activity (if using Xamarin.Android) IWin32Window, or IntPtr (if using .NET Framework). 
    // Used for invoking the browser for Active Directory Interactive authentication.
    public void SetParentActivityOrWindowFunc(Func<object> parentActivityOrWindowFunc);

    // For .NET Framework, .NET Core, and .NET Standard targeted applications
    // Sets a callback method that's invoked with a custom web UI instance that will let the user sign in with Azure AD, present consent if needed, and get back the authorization code. 
    // Applicable when working with Active Directory Interactive authentication.
    public void SetAcquireAuthorizationCodeAsyncCallback(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback);

    // For .NET Framework, .NET Core, and .NET Standard targeted applications
    // Clears cached user tokens from the token provider.
    public static void ClearUserTokenCache();
}
```


## See also
- [Application and service principal objects in Azure Active Directory](/azure/active-directory/develop/app-objects-and-service-principals)
- [Authentication flows](/azure/active-directory/develop/msal-authentication-flows)
