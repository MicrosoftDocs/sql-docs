---
title: Connect to Azure SQL with Microsoft Entra authentication and SqlClient
description: Describes how to use supported Microsoft Entra authentication modes to connect to Azure SQL data sources with SqlClient
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 02/28/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---

# Connect to Azure SQL with Microsoft Entra authentication and SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This article describes how to connect to Azure SQL data sources by using Microsoft Entra authentication from a .NET application with SqlClient.

[!INCLUDE [entra-id](../../../includes/entra-id-hard-coded.md)]

## Overview

Microsoft Entra authentication uses identities in Microsoft Entra ID to access data sources such as Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. The **Microsoft.Data.SqlClient** namespace allows client applications to specify Microsoft Entra credentials in different authentication modes when they're connecting to Azure SQL Database and Azure SQL Managed Instance. To use Microsoft Entra authentication with Azure SQL, you must [configure and manage Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

When you set the `Authentication` connection property in the connection string, the client can choose a preferred Microsoft Entra authentication mode according to the value provided:

- The earliest **Microsoft.Data.SqlClient** version supports `Active Directory Password` for .NET Framework, .NET Core, and .NET Standard. It also supports `Active Directory Integrated` authentication and `Active Directory Interactive` authentication for .NET Framework.
- Starting with **Microsoft.Data.SqlClient** 2.0.0, support for `Active Directory Integrated` authentication and `Active Directory Interactive` authentication is extended across .NET Framework, .NET Core, and .NET Standard.

  A new `Active Directory Service Principal` authentication mode is also added in SqlClient 2.0.0. It makes use of the client ID and secret of a service principal identity to accomplish authentication.
- More authentication modes are added in **Microsoft.Data.SqlClient** 2.1.0, including `Active Directory Device Code Flow` and `Active Directory Managed Identity` (also known as `Active Directory MSI`). These new modes enable the application to acquire an access token to connect to the server.

For information about Microsoft Entra authentication beyond what the following sections describe, see [Use Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).

<a name='setting-azure-active-directory-authentication'></a>

## Setting Microsoft Entra authentication

When the application is connecting to Azure SQL data sources by using Microsoft Entra authentication, it needs to provide a valid authentication mode. The following table lists the supported authentication modes. The application specifies a mode by using the `Authentication` connection property in the connection string.

| Value | Description  | Microsoft.Data.SqlClient version |
|:--|:--|:--:|
| Active Directory Password | Authenticate with a Microsoft Entra identity's username and password | 1.0+ |
| Active Directory Integrated | Authenticate with a Microsoft Entra identity by using Integrated Windows Authentication (IWA) | 2.0.0+<sup>1</sup> |
| Active Directory Interactive | Authenticate with a Microsoft Entra identity by using interactive authentication | 2.0.0+<sup>1</sup> |
| Active Directory Service Principal | Authenticate with a Microsoft Entra service principal, using its client ID and secret | 2.0.0+ |
| Active Directory Device Code Flow | Authenticate with a Microsoft Entra identity by using Device Code Flow mode | 2.1.0+ |
| Active Directory Managed Identity, <br>Active Directory MSI | Authenticate using a Microsoft Entra system-assigned or user-assigned managed identity | 2.1.0+ |
| Active Directory Default | Authenticate with a Microsoft Entra identity by using password-less and non-interactive mechanisms including managed identities, Visual Studio Code, Visual Studio, Azure CLI, etc. | 3.0.0+ |
| Active Directory Workload Identity| Authenticate with a Microsoft Entra identity by using a federated User Assigned Managed Identity to connect to SQL Database from Azure client environments that have enabled support for Workload Identity. | 5.2.0+ |

<sup>1</sup> Before **Microsoft.Data.SqlClient** 2.0.0, `Active Directory Integrated`, and `Active Directory Interactive` authentication modes are supported only on .NET Framework.

## Using password authentication

`Active Directory Password` authentication mode supports authentication to Azure data sources with Microsoft Entra ID for native or federated Microsoft Entra users. When you're using this mode, user credentials must be provided in the connection string. The following example shows how to use `Active Directory Password` authentication.

```cs
// Use your own server, database, user ID, and password.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Password; Encrypt=True; Database=testdb; User Id=user@domain.com; Password=***";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```

## Using integrated authentication

To use `Active Directory Integrated` authentication mode, you must have an on-premises Active Directory instance that is [joined](/entra/identity/devices/concept-directory-join) to Microsoft Entra ID in the cloud. You can [federate](/azure/active-directory/hybrid/connect/whatis-fed) by using Active Directory Federation Services (AD FS), for example.

When you're signed in to a domain-joined machine, you can access Azure SQL data sources without being prompted for credentials with this mode. You can't specify username and password in the connection string for .NET Framework applications. Username is optional in the connection string for .NET Core and .NET Standard applications. You can't set the `Credential` property of SqlConnection in this mode.

The following code snippet is an example of when `Active Directory Integrated` authentication is in use.

```cs
// Use your own server and database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Encrypt=True; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User ID is optional for .NET Core and .NET Standard.
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Integrated; Encrypt=True; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

## Using interactive authentication

`Active Directory Interactive` authentication supports multifactor authentication technology to connect to Azure SQL data sources. If you provide this authentication mode in the connection string, an Azure authentication screen appears and asks the user to enter valid credentials. You can't specify the password in the connection string.

You can't set the `Credential` property of SqlConnection in this mode. With **Microsoft.Data.SqlClient** 2.0.0 and later, username is allowed in the connection string when you're in interactive mode.

The following example shows how to use `Active Directory Interactive` authentication.

```cs
// Use your own server, database, and user ID.
// User ID is optional.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Encrypt=True; Database=testdb; User Id=user@domain.com";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// User ID is not provided.
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory Interactive; Encrypt=True; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

## Using service principal authentication

In `Active Directory Service Principal` authentication mode, the client application can connect to Azure SQL data sources by providing the client ID and secret of a service principal identity. Service principal authentication involves:

1. Setting up an app registration with a secret.
1. Granting permissions to the app in the Azure SQL Database instance.
1. Connecting with the correct credential.

The following example shows how to use `Active Directory Service Principal` authentication.

```cs
// Use your own server, database, app ID, and secret.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Service Principal; Encrypt=True; Database=testdb; User Id=AppId; Password=secret";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```

## Using device code flow authentication

With [Microsoft Authentication Library](/azure/active-directory/develop/msal-overview) for .NET (MSAL.NET), `Active Directory Device Code Flow` authentication enables the client application to connect to Azure SQL data sources from devices and operating systems that don't have an interactive web browser. Interactive authentication is performed on another device. For more information about device code flow authentication, see [OAuth 2.0 Device Code Flow](/azure/active-directory/develop/v2-oauth2-device-code).

When this mode is in use, you can't set the `Credential` property of `SqlConnection`. Also, the username and password must not be specified in the connection string.

The following code snippet is an example of using `Active Directory Device Code Flow` authentication.

> [!NOTE]
> The timeout for `Active Directory Device Code Flow` defaults to the connection's `Connect Timeout` setting. Make sure to specify a `Connect Timeout` that provides enough time to go through the device code flow authentication process.

```cs
// Use your own server and database and increase Connect Timeout as needed for device code flow.
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Device Code Flow; Encrypt=True; Database=testdb; Connect Timeout=180;";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```

## Using managed identity authentication

Authentication with Managed Identities for Azure resources is the recommended authentication method for programmatic access to SQL. A client application can use the system-assigned or user-assigned managed identity of a resource to authenticate to SQL with Microsoft Entra ID, by providing the identity and using it to obtain access tokens. This method eliminates the need to manage credentials and secrets, and can simplify access management.

There are two types of managed identities:

- _System-assigned managed identity_ is created as part of an Azure resource (such as your SQL managed instance or the [logical server](/azure/azure-sql/database/logical-servers)), and shares the lifecycle of that resource. System-assigned identities can only be associated with a single Azure resource.
- _User-assigned managed identity_ is created as a standalone Azure resource. It can be assigned to one or more instances of an Azure service.

For more information about managed identities, see [About managed identities for Azure resources](/azure/active-directory/managed-identities-azure-resources/overview).

Since **Microsoft.Data.SqlClient** 2.1.0, the driver supports authentication to Azure SQL Database, Azure Synapse Analytics, and Azure SQL Managed Instance by acquiring access tokens via managed identity. To use this authentication, specify either `Active Directory Managed Identity` or `Active Directory MSI` in the connection string, and no password is required. You can't set the `Credential` property of `SqlConnection` in this mode either.

For a user-assigned managed identity, the **client id** of the managed identity must be provided when using Microsoft.Data.SqlClient v3.0 or newer. If using Microsoft.Data.SqlClient v2.1, the **object id** of the managed identity must be provided.

The following example shows how to use `Active Directory Managed Identity` authentication with a system-assigned managed identity.

```cs
// For system-assigned managed identity
// Use your own values for Server and Database.
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Encrypt=True; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Encrypt=True; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

The following example demonstrates `Active Directory Managed Identity` authentication with a user-assigned managed identity with **Microsoft.Data.SqlClient v3.0 onwards**.

```cs
// For user-assigned managed identity
// Use your own values for Server, Database, and User Id.

// With Microsoft.Data.SqlClient v3.0+
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Encrypt=True; User Id=ClientIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// With Microsoft.Data.SqlClient v3.0+
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Encrypt=True; User Id=ClientIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

The following example demonstrates `Active Directory Managed Identity` authentication with a user-assigned managed identity with **Microsoft.Data.SqlClient v2.1**.

```cs
// For user-assigned managed identity
// Use your own values for Server, Database, and User Id.

// With Microsoft.Data.SqlClient v2.1
string ConnectionString1 = @"Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Encrypt=True; User Id=ObjectIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString1)) {
    conn.Open();
}

// With Microsoft.Data.SqlClient v2.1
string ConnectionString2 = @"Server=demo.database.windows.net; Authentication=Active Directory MSI; Encrypt=True; User Id=ObjectIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString2)) {
    conn.Open();
}
```

## Using default authentication

Available starting in version 3.0, this authentication mode widens the possibilities of user authentication. This mode extends login solutions to the client environment, Visual Studio Code, Visual Studio, Azure CLI etc.

With this authentication mode, the driver acquires a token by passing "[DefaultAzureCredential](/dotnet/api/azure.identity.defaultazurecredential)" from the Azure Identity library to acquire an access token. This mode attempts to use a set of credential types to acquire an access token in order. Depending on the version of the Azure Identity library used, the credential set varies. Version specific differences are noted in the list. For Azure Identity version specific behavior, see the [Azure.Identity API docs](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.3.0/api/Azure.Identity/Azure.Identity.DefaultAzureCredential.html).

- **EnvironmentCredential**
  - Enables authentication with Microsoft Entra ID using client and secret, or username and password, details configured in the following environment variables: AZURE_TENANT_ID, AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, AZURE_CLIENT_CERTIFICATE_PATH, AZURE_USERNAME, AZURE_PASSWORD ([More details](/dotnet/api/azure.identity.environmentcredential))
- **WorkloadIdentityCredential**
  - Enables Microsoft Entra Workload ID authentication on Kubernetes and other hosts supporting workload identity. For more information, see [Microsoft Entra Workload ID](/azure/aks/workload-identity-overview). Available starting in Azure Identity version 1.10 and Microsoft.Data.SqlClient 5.1.4.
- **ManagedIdentityCredential**
  - Attempts authentication with Microsoft Entra ID using a managed identity that is assigned to the deployment environment. **"Client Id" of "User Assigned Managed Identity"** is read from the **"User Id" connection property**.
- **SharedTokenCacheCredential**
  - Authenticates using tokens in the local cache shared between Microsoft applications.
- **VisualStudioCredential**
  - Enables authentication with Microsoft Entra ID using data from Visual Studio
- **VisualStudioCodeCredential**
  - Enables authentication with Microsoft Entra ID using data from Visual Studio Code.
- **AzurePowerShellCredential**
  - Enables authentication with Microsoft Entra ID using the Azure PowerShell. Available starting in Azure Identity version 1.6 and Microsoft.Data.SqlClient 5.0.
- **AzureCliCredential**
  - Enables authentication with Microsoft Entra ID using the Azure CLI to obtain an access token.
- **AzureDeveloperCliCredential**
  - Enables authentication to Microsoft Entra ID using Azure Developer CLI to obtain an access token. Available starting in Azure Identity version 1.10 and Microsoft.Data.SqlClient 5.1.4.

> [!NOTE]
> _InteractiveBrowserCredential_ is disabled in the driver implementation of "Active Directory Default", and "Active Directory Interactive" is the only option available to acquire a token using MFA/Interactive authentication.
>
> Further customization options are not available at the moment.

The following example shows how to use **Active Directory Default** authentication.

```cs
// Use your own server, database
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Default; Encrypt=True; Database=testdb;";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```

## Using workload identity authentication

Available starting in version 5.2, like with managed identities, [workload identity](/azure/aks/workload-identity-overview) authentication mode uses the value of the User Id parameter in the connection string for its Client Id if specified. But unlike managed identity, WorkloadIdentityCredentialOptions defaults its value from environment variables: AZURE_TENANT_ID, AZURE_CLIENT_ID, and AZURE_FEDERATED_TOKEN_FILE. However, only the Client Id may be overridden by the connection string.

The following example demonstrates `Active Directory Workload Identity` authentication with a user-assigned managed identity with **Microsoft.Data.SqlClient v5.2 onwards**.

```cs
// Use your own values for Server, Database, and User Id.
// With Microsoft.Data.SqlClient v5.2+
string ConnectionString = @"Server=demo.database.windows.net; Authentication=Active Directory Workload Identity; Encrypt=True; User Id=ClientIdOfManagedIdentity; Database=testdb";

using (SqlConnection conn = new SqlConnection(ConnectionString)) {
    conn.Open();
}
```

## Customizing Microsoft Entra authentication

Besides using the Microsoft Entra authentication built into the driver, **Microsoft.Data.SqlClient** 2.1.0 and later provide applications the option to customize Microsoft Entra authentication. The customization is based on the `ActiveDirectoryAuthenticationProvider` class, which is derived from the [`SqlAuthenticationProvider`](/dotnet/api/system.data.sqlclient.sqlauthenticationprovider) abstract class.

During Microsoft Entra authentication, the client application can define its own `ActiveDirectoryAuthenticationProvider` class by either:

- Using a customized callback method.
- Passing an application client ID to the MSAL library via SqlClient driver for fetching access tokens.

The following example displays how to use a custom callback when `Active Directory Device Code Flow` authentication is in use.

[!code-csharp [AADAuthenticationCustomDeviceFlowCallback#1](~/../sqlclient/doc/samples/AADAuthenticationCustomDeviceFlowCallback.cs#1)]

With a customized `ActiveDirectoryAuthenticationProvider` class, a user-defined application client ID can be passed to SqlClient when a supported Microsoft Entra authentication mode is in use. Supported Microsoft Entra authentication modes include `Active Directory Password`, `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Service Principal`, and `Active Directory Device Code Flow`.

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

Given more flexibility, the client application can also use its own provider for Microsoft Entra authentication instead of using the `ActiveDirectoryAuthenticationProvider` class. The custom authentication provider needs to be a subclass of `SqlAuthenticationProvider` with overridden methods. It then must register the custom provider, overriding one or more of the existing `Active Directory*` authentication methods.

The following example shows how to use a new authentication provider for `Active Directory Device Code Flow` authentication.

[!code-csharp [CustomDeviceCodeFlowAzureAuthenticationProvider#1](~/../sqlclient/doc/samples/CustomDeviceCodeFlowAzureAuthenticationProvider.cs#1)]

In addition to improving the `Active Directory Interactive` authentication experience, **Microsoft.Data.SqlClient** 2.1.0 and later provide the following APIs for client applications to customize interactive authentication and device code flow authentication.

```cs
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

- [Application and service principal objects in Microsoft Entra ID](/azure/active-directory/develop/app-objects-and-service-principals)
- [Authentication flows](/azure/active-directory/develop/msal-authentication-flows)
