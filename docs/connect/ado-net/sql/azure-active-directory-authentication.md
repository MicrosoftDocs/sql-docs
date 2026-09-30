---
title: Microsoft Entra Authentication with Microsoft.Data.SqlClient
description: Choose Microsoft Entra authentication for SqlClient, install the Azure extension, and manage identities, access tokens, and connection pools.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: integration
dev_langs:
  - csharp
ai-usage: ai-assisted
---
# Microsoft Entra authentication with Microsoft.Data.SqlClient

<a id="connect-to-azure-sql-with-microsoft-entra-authentication-and-sqlclient"></a>

[!INCLUDE [dotnet-all](../../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Use Microsoft Entra ID to connect without storing a SQL password in your application. Choose a sign-in method for the host where the application runs, provision that identity in the target database, and keep [Transport Layer Security (TLS) certificate validation](../encryption-and-certificate-validation.md) enabled.

## Overview

The driver-provided `Active Directory ...` authentication modes require both `Microsoft.Data.SqlClient` and `Microsoft.Data.SqlClient.Extensions.Azure`. Install matching versions:

```dotnetcli
dotnet add package Microsoft.Data.SqlClient --version 7.1.0
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.1.0
```

The extension registers its providers automatically. Applications that supply their own `AccessToken`, `AccessTokenCallback`, or authentication provider don't need the Azure extension solely for that purpose. Add the identity library your own token code uses.

Before connecting:

1. Configure [Microsoft Entra authentication for Azure SQL](/azure/azure-sql/database/authentication-aad-configure), or follow your target service's equivalent setup.
1. Grant the application or user identity access to the intended database. Obtaining a token doesn't grant database permissions.
1. Allow network access to the endpoint, and select the database explicitly.
1. Choose a supported authentication mode and make its credentials available on the application host.

SQL database in Microsoft Fabric supports [Microsoft Entra authentication only](/fabric/database/sql/authentication). It doesn't support SQL authentication or SQL logins. Use the connection string from the Fabric portal for the intended endpoint.

<a id="setting-azure-active-directory-authentication"></a>
<a id="setting-microsoft-entra-authentication"></a>

## Choose an authentication mode

Set `Authentication` to one of the connection-string values in this table. The release column helps you migrate from older drivers. Current applications should install the packages described in [Overview](#overview).

| Connection-string value | Use | First supported release |
| --- | --- | --- |
| `Active Directory Integrated` | Acquire a token by using Integrated Windows Authentication (IWA) in a configured domain and Microsoft Entra environment. | 1.0 on .NET Framework; 2.0 across supported targets. |
| `Active Directory Interactive` | User sign-in that supports multifactor authentication (MFA). | 1.0 on .NET Framework; 2.0 across supported targets. |
| `Active Directory Service Principal` | Application client ID and client secret. | 2.0. |
| `Active Directory Device Code Flow` | User sign-in through a browser on another device. | 2.1. |
| `Active Directory Managed Identity` or `Active Directory MSI` | System-assigned or user-assigned managed identity available to the application host. | 2.1. |
| `Active Directory Default` | Discover an available credential through an Azure Identity credential chain. | 3.0. |
| `Active Directory Workload Identity` | Federated workload identity with a projected token file. | 5.2. |
| `Active Directory Password` | Deprecated username/password flow. Don't use for new applications. | 1.0. |

`Authentication=Sql Password` is SQL authentication, not Microsoft Entra authentication. See [SQL Server and Windows authentication](authentication-sql-server.md).

### Open a connection

For a .NET console application, set `SQL_CONNECTION_STRING` to the appropriate template from this article after replacing the placeholders. This example opens the connection and prints the database name:

```csharp
using Microsoft.Data.SqlClient;

string connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Set SQL_CONNECTION_STRING before running.");

using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();
using var command = new SqlCommand("SELECT DB_NAME();", connection);
Console.WriteLine(await command.ExecuteScalarAsync());
```

Each mode has different host prerequisites. A connection string accepted by the parser doesn't prove that the host can obtain a token or that the identity can access the database.

<a id="using-integrated-authentication"></a>

## Use integrated authentication

`Active Directory Integrated` acquires a Microsoft Entra token by using the signed-in domain identity. It requires the appropriate joined or federated identity configuration. It isn't the same as `Integrated Security=true`, which uses Windows authentication directly with SQL Server.

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Integrated;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

Don't supply a password or `SqlCredential`. A username hint is optional on modern .NET; .NET Framework doesn't accept a username for this mode. IWA can't satisfy an interactive MFA challenge. Use interactive authentication when the tenant requires user interaction.

<a id="using-interactive-authentication"></a>

## Use interactive authentication

Use `Active Directory Interactive` for a person signing in to a desktop or developer application. The authentication provider prompts the user and supports MFA. An optional `User ID=<user_id>` provides a sign-in hint, not a password.

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Interactive;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

Don't provide `Password` or `SqlCredential`. Don't use an interactive mode for an unattended service.

<a id="using-service-principal-authentication"></a>

## Use service principal authentication

The built-in `Active Directory Service Principal` mode uses an application's client ID as `User ID` and its client secret as `Password`. Provision the service principal in the database and grant only the permissions it needs.

Retrieve the secret from protected configuration rather than embedding it in source. This console example reads values injected into the process:

```csharp
using Microsoft.Data.SqlClient;

static string Required(string name) =>
    Environment.GetEnvironmentVariable(name)
    ?? throw new InvalidOperationException($"Set {name} before running.");

var options = new SqlConnectionStringBuilder
{
    DataSource = Required("SQL_SERVER"),
    InitialCatalog = Required("SQL_DATABASE"),
    Authentication = SqlAuthenticationMethod.ActiveDirectoryServicePrincipal,
    UserID = Required("AZURE_CLIENT_ID"),
    Password = Required("AZURE_CLIENT_SECRET"),
    Encrypt = SqlConnectionEncryptOption.Mandatory,
    TrustServerCertificate = false,
    MultiSubnetFailover = true
};

using var connection = new SqlConnection(options.ConnectionString);
await connection.OpenAsync();
using var command = new SqlCommand("SELECT DB_NAME();", connection);
Console.WriteLine(await command.ExecuteScalarAsync());
```

Set `SQL_SERVER` to your Transmission Control Protocol (TCP) endpoint, such as `tcp:contoso.database.windows.net,1433`, and `SQL_DATABASE` to the database name. Don't log the environment values or connection string.

For certificate-based application credentials, acquire tokens with `ClientCertificateCredential` through [AccessTokenCallback](#using-accesstokencallback). For federated credentials, use workload identity or a suitable token callback. The connection-string service principal mode itself requires a client secret.

<a id="using-device-code-flow-authentication"></a>

## Use device code flow authentication

Use this mode when the application host doesn't have a browser but a person can sign in on another device. Follow the verification URL and code provided by the authentication flow.

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Device Code Flow;Connect Timeout=180;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

`Connect Timeout` bounds authentication; this example allows 180 seconds. Don't supply `User ID`, `Password`, or `SqlCredential`. Device code flow still requires a person and is unsuitable for unattended services.

<a id="using-managed-identity-authentication"></a>

## Use managed identity authentication

For an Azure-hosted application, use a managed identity when the host supports it. The identity belongs to the application host, not automatically to the database server.

- A *system-assigned managed identity* shares the host resource's lifecycle.
- A *user-assigned managed identity* is a separate resource that you can assign to supported hosts.

For a system-assigned identity, omit `User ID`:

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Managed Identity;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

For a user-assigned identity, provide its **client ID**:

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Managed Identity;User ID=<client_id>;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

`Active Directory MSI` is a compatibility spelling for the same mode. Don't provide a password or `SqlCredential`. The host must expose the selected managed identity, and that identity must have database access. A developer workstation doesn't gain a managed identity by using this connection string.

When migrating from SqlClient 2.1, replace the user-assigned identity's **object ID** with its **client ID**. SqlClient uses the client ID starting with 3.0.

<a id="using-default-authentication"></a>

## Use default authentication

`Active Directory Default` uses an Azure Identity credential chain to discover an available identity:

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

Candidates depend on the installed Azure Identity version and configuration. They include environment credentials, workload identity, managed identity, and signed-in development tools such as Visual Studio, Azure Command-Line Interface (CLI), Azure PowerShell, and Azure Developer CLI. See [DefaultAzureCredential](/dotnet/api/azure.identity.defaultazurecredential) for the current chain.

SqlClient disables `InteractiveBrowserCredential` in this mode. Choose `Active Directory Interactive` if the application itself needs to prompt for sign-in. A development-tool credential can use a session established by an earlier interactive sign-in.

> [!IMPORTANT]
> `Active Directory Default` can make the first connection slow because `DefaultAzureCredential` tries credential providers in sequence until one supplies a token. Unavailable providers can add discovery, network, or process-startup delays before the working provider is reached. Prefer a specific authentication mode in production, such as `Active Directory Managed Identity`, `Active Directory Workload Identity`, or `Active Directory Service Principal`, to avoid this discovery overhead. Credential and token caching can reduce subsequent acquisition work; don't assume every pooled connection repeats the full chain.

With `Active Directory Default`, the selected identity can change when host configuration changes.

For older deployments, workload identity within this chain and Azure Developer CLI support arrived in SqlClient 5.1.4. That feature differs from the dedicated `Active Directory Workload Identity` mode, which arrived in 5.2. Azure PowerShell support arrived in 5.0.

<a id="using-workload-identity-authentication"></a>

## Use workload identity authentication

Use `Active Directory Workload Identity` on a host configured for federated workload identity. The identity provider must trust the projected token's issuer and subject.

The credential reads:

- `AZURE_TENANT_ID`: the tenant ID.
- `AZURE_CLIENT_ID`: the application or user-assigned managed identity client ID.
- `AZURE_FEDERATED_TOKEN_FILE`: the path to the projected token file.

```text
Server=tcp:contoso.database.windows.net,1433;Database=<database>;Authentication=Active Directory Workload Identity;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

An optional `User ID=<client_id>` overrides the client ID. The connection string doesn't override the tenant ID or token-file path. Don't put the token file's contents in the connection string or logs.

<a id="using-password-authentication-deprecated"></a>

## Replace deprecated password authentication

[!INCLUDE [entra-password-auth-deprecation](../../../includes/entra-password-auth-deprecation.md)]

`Active Directory Password` uses the resource owner password credentials flow and can't satisfy MFA requirements. The corresponding `SqlAuthenticationMethod.ActiveDirectoryPassword` enum member is obsolete. Choose interactive authentication for users, or managed identity, workload identity, or an application credential for services.

<a id="customizing-microsoft-entra-authentication"></a>

## Customize Microsoft Entra authentication

Choose the smallest customization that meets your requirements from the following application programming interfaces (APIs):

| Requirement | API |
| --- | --- |
| Supply an already acquired token. | `SqlConnection.AccessToken`. |
| Acquire and renew tokens with an application-selected credential. | `SqlConnection.AccessTokenCallback`. |
| Customize device-code presentation or interactive sign-in. | `ActiveDirectoryAuthenticationProvider` from the Azure extension. |
| Implement a driver authentication provider. | Derive from `SqlAuthenticationProvider` and register the provider. |

With a custom `ActiveDirectoryAuthenticationProvider`, you can supply an application client ID, configure device-code or authorization-code callbacks, and set the parent window for interactive sign-in where supported. Use the [provider API reference](/dotnet/api/microsoft.data.sqlclient.activedirectoryauthenticationprovider) for the installed extension package's signatures.

### Supply an access token directly

Set `SqlConnection.AccessToken` before opening the connection. The core driver supports this property without the Azure extension.

The literal token is part of the connection-pool key. A different token creates a different pool. SqlClient doesn't receive an expiration time through this property and can't renew the token for you. Acquire a valid token for new connections and clear affected pools when tokens expire, especially if you set a nonzero `Min Pool Size`.

Don't combine `AccessToken` with `Authentication`, integrated security, `User ID` or `Password`, `SqlCredential`, `AccessTokenCallback`, or `SspiContextProvider`. Never log a token.

<a id="using-accesstokencallback"></a>

## Use AccessTokenCallback

Prefer `AccessTokenCallback` when your application acquires tokens itself and uses connection pooling. The callback returns a token and its expiration time so SqlClient can request renewal for pooled authentication. The API is available starting with SqlClient 5.2.

Reuse the same delegate instance and credential object for connections that should share a pool. The callback delegate is part of the pool key. Creating a fresh closure for each connection can create a separate pool for each one.

> [!IMPORTANT]
> Return the same security context for the same callback inputs. Don't select a different user from ambient request state inside a shared callback. A pooled connection could otherwise be returned under the wrong identity.

This .NET console example uses the signed-in Azure CLI identity for local development. Install `Microsoft.Data.SqlClient` and `Azure.Identity`, sign in to Azure CLI with an identity that has database access, and set `SQL_SERVER` and `SQL_DATABASE`. The Azure extension isn't required for this example.

```csharp
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

internal static class Program
{
    private static readonly TokenCredential Credential = new AzureCliCredential();

    private static readonly Func<SqlAuthenticationParameters, CancellationToken,
        Task<SqlAuthenticationToken>> TokenCallback = async (parameters, cancellationToken) =>
    {
        string scope = parameters.Resource.EndsWith("/.default", StringComparison.Ordinal)
            ? parameters.Resource
            : parameters.Resource + "/.default";
        AccessToken token = await Credential.GetTokenAsync(
            new TokenRequestContext(new[] { scope }), cancellationToken);
        return new SqlAuthenticationToken(token.Token, token.ExpiresOn);
    };

    private static async Task Main()
    {
        var options = new SqlConnectionStringBuilder
        {
            DataSource = Required("SQL_SERVER"),
            InitialCatalog = Required("SQL_DATABASE"),
            Encrypt = SqlConnectionEncryptOption.Mandatory,
            TrustServerCertificate = false,
            MultiSubnetFailover = true
        };

        using var connection = new SqlConnection(options.ConnectionString)
        {
            AccessTokenCallback = TokenCallback
        };
        await connection.OpenAsync();
        using var command = new SqlCommand("SELECT DB_NAME();", connection);
        Console.WriteLine(await command.ExecuteScalarAsync());
    }

    private static string Required(string name) =>
        Environment.GetEnvironmentVariable(name)
        ?? throw new InvalidOperationException($"Set {name} before running.");
}
```

For production, replace `AzureCliCredential` with the intended credential, such as `ManagedIdentityCredential`, `WorkloadIdentityCredential`, or `ClientCertificateCredential`, and configure its prerequisites. Don't change the identity behind a shared callback while its pooled connections remain available.

Don't combine `AccessTokenCallback` with `Authentication`, integrated security, `AccessToken`, or `SspiContextProvider`. A callback can use `User ID` as an identity selector, but your callback must interpret it consistently. The example doesn't use a selector because it has one fixed credential.

## Support for a custom SQL authentication provider

Derive from <xref:Microsoft.Data.SqlClient.SqlAuthenticationProvider>, implement its token-acquisition contract, and register it with `SqlAuthenticationProvider.SetProvider` for the authentication method you replace. Register providers during application initialization, before opening connections.

The provider must return a valid token and expiration time for the requested resource and authority. The same security-context rule used for callbacks applies to providers. Overriding a provider doesn't remove the target service's identity, tenant, or database-permission requirements.

## Migrate to Microsoft.Data.SqlClient 7.0

Applications upgrading across the package split should review dependencies and authentication configuration.

### What changed in 7.0

The core driver no longer brings in Azure and Microsoft Entra authentication dependencies. The built-in `ActiveDirectoryAuthenticationProvider` moves to `Microsoft.Data.SqlClient.Extensions.Azure`, with shared contracts in `Microsoft.Data.SqlClient.Extensions.Abstractions`.

### Step 1: Install the Azure extension package

For driver-provided Microsoft Entra modes, install matching core and Azure extension versions as shown in [Overview](#overview). Deploy the extension with the application. No manual provider registration is required for the built-in modes.

### Step 2: Replace deprecated authentication modes

Replace `Active Directory Password` with a mode that fits your application's user interaction and hosting model. For an unattended deployment, select a specific workload identity rather than relying on a developer's sign-in.

### Step 3: Review connection strings

Keep the existing supported `Authentication` value if it still fits the deployment. Confirm the database name, identity selector, encryption settings, and network access. Don't add `Integrated Security=true` to a Microsoft Entra connection string.

### Applications that don't use Entra ID authentication

SQL password and Windows integrated authentication don't require the Azure extension. Applications that already acquire their own tokens can continue using `AccessToken` or `AccessTokenCallback` with the core driver and their chosen identity library.

## Related content

- [Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-overview)
- [Application and service principal objects](/entra/identity-platform/app-objects-and-service-principals)
- [SQL Server connection pooling](../sql-server-connection-pooling.md)
- [Security best practices](application-security-scenarios-sql-server.md)
