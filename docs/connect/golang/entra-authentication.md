---
title: "go-mssqldb Microsoft Entra ID Authentication"
description: "Configure Microsoft Entra ID authentication with the go-mssqldb driver using built-in fedauth flows and custom token providers."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Microsoft Entra ID authentication with go-mssqldb

The `go-mssqldb` driver supports Microsoft Entra ID authentication through the `azuread` package. This package registers a separate driver named `azuresql` that wraps the standard `sqlserver` driver with Microsoft Entra ID credential support.

> [!CAUTION]
> All built-in `fedauth` authentication methods require the `azuresql` driver name (not `sqlserver`). If you use `sql.Open("sqlserver", ...)` with a `fedauth` parameter, authentication silently fails with `Login failed for user ''`. Import the `azuread` package and use `azuresql` as shown in the following example.

## Choose a fedauth flow

Use the following table to choose the appropriate flow for your hosting environment and credential source:

| If you need to connect from... | Start with... | Use when... |
| --- | --- | --- |
| Local development | `ActiveDirectoryDefault` | You want to reuse Azure CLI or Azure Developer CLI credentials without configuring a service principal or managed identity locally. |
| An Azure-hosted app with a managed identity | `ActiveDirectoryManagedIdentity` | You want a predictable production configuration and don't want other local credential sources in the chain. |
| A CI/CD pipeline in Azure DevOps | `ActiveDirectoryAzurePipelines` | Your pipeline already uses an Azure service connection and exposes `SYSTEM_ACCESSTOKEN`. |
| Kubernetes with Azure Workload Identity | `ActiveDirectoryWorkloadIdentity` | Your pod receives an OIDC token file and you want workload identity instead of a client secret. |
| A service principal with a secret or certificate | `ActiveDirectoryServicePrincipal` | Your app authenticates as an app registration and you manage the client secret or certificate. |
| A tool that already has an access token | `ActiveDirectoryServicePrincipalAccessToken` or a [custom token provider](#custom-token-provider) | Your app acquires and refreshes tokens outside the driver. |
| A delegated user token from an upstream web API | `ActiveDirectoryOnBehalfOf` | You need to exchange a user token for a SQL-scoped token in a middle-tier service. |
| A developer tool or interactive utility | `ActiveDirectoryInteractive`, `ActiveDirectoryDeviceCode`, `ActiveDirectoryAzCli`, or `ActiveDirectoryAzureDeveloperCli` | A human is present to sign in, or you want to reuse an existing local CLI session. |
| A Windows-only app that handles Integrated auth requirements | `ActiveDirectoryIntegrated` (advanced) | You provide custom token acquisition logic for Integrated scenarios. |

If you share one connection string across local development and Azure hosting, `ActiveDirectoryDefault` is a good starting point. For production, use `ActiveDirectoryManagedIdentity` or `ActiveDirectoryServicePrincipal` to avoid the credential chain latency.

## Install the azuread package

Download the `azuread` subpackage, which registers the `azuresql` driver:

```bash
go get github.com/microsoft/go-mssqldb/azuread
```

## Use the azuresql driver

Import the `azuread` package (instead of or in addition to the base `go-mssqldb` package) and open connections by using the `azuresql` driver name:

```go
import (
    "database/sql"

    _ "github.com/microsoft/go-mssqldb/azuread"
)

func main() {
    db, err := sql.Open("azuresql",
        "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
    // ...
}
```

All following examples target Azure SQL. Keep `encrypt=true&TrustServerCertificate=false` in the connection string so the driver validates the server certificate.

## Fedauth credential types

Set the `fedauth` connection parameter to one of the following values. Most types map to an Azure Identity credential from the `azidentity` package. `ActiveDirectoryServicePrincipalAccessToken` and custom token provider APIs use caller-supplied tokens.

### ActiveDirectoryDefault

Uses `azidentity.DefaultAzureCredential`, which tries the following credential sources in order:

1. Environment variables (`AZURE_TENANT_ID`, `AZURE_CLIENT_ID`, and so on).
1. Workload identity for Kubernetes.
1. Managed identity.
1. Azure CLI credentials.
1. Azure Developer CLI credentials.

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false
```

Use this type for local development because it picks up Azure CLI credentials automatically. For production, use `ActiveDirectoryManagedIdentity` or `ActiveDirectoryServicePrincipal` directly. `DefaultAzureCredential` walks through each credential source on the first connection, which adds latency that production workloads don't need.

### ActiveDirectoryManagedIdentity

Authenticates with a system-assigned or user-assigned managed identity. For a user-assigned identity, provide the client ID in the `user id` parameter:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryManagedIdentity&encrypt=true&TrustServerCertificate=false
```

With a user-assigned identity:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryManagedIdentity&user id=<client-id>&encrypt=true&TrustServerCertificate=false
```

> [!NOTE]
> `ActiveDirectoryMSI` is an alias for `ActiveDirectoryManagedIdentity`.

### ActiveDirectoryServicePrincipal

Authenticates as a service principal (app registration) with a client ID and client secret:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryServicePrincipal&user id=<client-id>&password=<client-secret>&encrypt=true&TrustServerCertificate=false
```

For certificate-based service principal authentication, use `clientcertpath=<path-to-certificate>` together with `password=<certificate-password>`.

> [!NOTE]
> `ActiveDirectoryApplication` is an alias for `ActiveDirectoryServicePrincipal`.

### ActiveDirectoryServicePrincipalAccessToken

Uses a pre-acquired service principal access token that your application passes directly in the connection string:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryServicePrincipalAccessToken&password=<access-token>&encrypt=true&TrustServerCertificate=false
```

Use this flow only when your application already acquires and refreshes the access token outside the driver. For most service-to-service scenarios, prefer `ActiveDirectoryServicePrincipal` or a custom token provider.

### ActiveDirectoryPassword

[!INCLUDE [entra-password-auth-deprecation](../../includes/entra-password-auth-deprecation.md)]

Authenticates with a Microsoft Entra user name and password:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryPassword&user id=<user>@mydomain.com&password=<password>&applicationclientid=<app-id>&encrypt=true&TrustServerCertificate=false
```

The `applicationclientid` parameter is required for this flow.

### ActiveDirectoryInteractive

Opens a browser-based interactive sign-in prompt for the user. Suitable for local development tools:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryInteractive&user id=<user>@mydomain.com&applicationclientid=<app-id>&encrypt=true&TrustServerCertificate=false
```

The `applicationclientid` parameter is required for this flow.

### ActiveDirectoryDeviceCode

Displays a device code for the user to enter at `https://microsoft.com/devicelogin`. Useful for environments without a browser:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDeviceCode&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryAzCli

Uses the token from the signed-in Azure CLI session:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryAzCli&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryAzureDeveloperCli

Uses the token from the signed-in Azure Developer CLI (`azd`) session:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryAzureDeveloperCli&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryEnvironment

Reads credentials from environment variables. The Azure Identity library inspects variables such as `AZURE_TENANT_ID`, `AZURE_CLIENT_ID`, and `AZURE_CLIENT_SECRET`:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryEnvironment&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryWorkloadIdentity

Authenticates by using workload identity federation. Use this method in Kubernetes pods with Azure Workload Identity configured.

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryWorkloadIdentity&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryAzurePipelines

Authenticates by using an Azure Pipelines service connection. Provide the pipeline parameters in the connection string, or let the driver read missing values from Azure Pipelines environment variables.

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryAzurePipelines&user id=<client-id>@<tenant-id>&serviceconnectionid=<service-connection-id>&systemtoken=<system-access-token>&encrypt=true&TrustServerCertificate=false
```

Set parameters as required by the driver:

| Parameter | Description |
| --- | --- |
| `user id` | Service principal client ID, optionally followed by `@tenant-id`. |
| `serviceconnectionid` | Service connection ID from Azure DevOps. |
| `systemtoken` | The pipeline system access token (`$(System.AccessToken)`). |

### ActiveDirectoryClientAssertion

Authenticates by using a client assertion (a signed JWT token) instead of a client secret. Provide the signed JWT in the `clientassertion` parameter:

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryClientAssertion&user id=<client-id>@<tenant-id>&clientassertion=<jwt-token>&encrypt=true&TrustServerCertificate=false
```

### ActiveDirectoryOnBehalfOf

Authenticates by using the On-Behalf-Of (OBO) flow. The driver exchanges an upstream user token for a new token scoped to SQL Server.

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryOnBehalfOf&user id=<client-id>@<tenant-id>&password=<client-secret>&userassertion=<user-token>&encrypt=true&TrustServerCertificate=false
```

The client authentication leg can use `password`, `clientcertpath`, or `clientassertion`, but `userassertion` is always required.

### ActiveDirectoryIntegrated

Supports an advanced Integrated-auth workflow. This mode requires custom token-acquisition logic through a token provider.

Use this mode only on Windows. On Linux and macOS, use a custom token provider for your authentication flow.

```text
sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryIntegrated&encrypt=true&TrustServerCertificate=false
```

## Custom token provider

If none of the built-in `fedauth` types fit your scenario, use one of these token provider APIs to supply your own token-acquisition logic:

### NewSecurityTokenConnector (Recommended)

Use this API when you have a pre-acquired OAuth2 access token:

```go
import (
    "context"
    "database/sql"
    "log"

    "github.com/microsoft/go-mssqldb"
)

connector, err := mssql.NewSecurityTokenConnector(
    "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false",
    func(ctx context.Context) (string, error) {
        // Return a pre-acquired OAuth2 access token.
        return myTokenProvider(ctx)
    },
)
if err != nil {
    log.Fatal(err)
}
db := sql.OpenDB(connector)
```

### NewAccessTokenConnector (Simplified API)

Use this API for simpler token acquisition without context handling.

```go
import (
    "database/sql"
    "log"

    "github.com/microsoft/go-mssqldb"
)

connector, err := mssql.NewAccessTokenConnector(
    "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false",
    func() (string, error) {
        // Return a pre-acquired OAuth2 access token.
        return mySimpleTokenProvider()
    },
)
if err != nil {
    log.Fatal(err)
}
db := sql.OpenDB(connector)
```

### NewActiveDirectoryTokenConnector (Custom ADAL Workflows)

Use this API for custom Azure AD token acquisition workflows when neither the built-in `fedauth` modes nor the SecurityToken APIs fit your scenario:

```go
import (
    "context"
    "database/sql"
    "log"

    "github.com/microsoft/go-mssqldb"
)

connector, err := mssql.NewActiveDirectoryTokenConnector(
    "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false",
    mssql.FedAuthADALWorkflowPassword,
    func(ctx context.Context, serverSPN, stsURL string) (string, error) {
        // Custom ADAL workflow using server-provided SPN and STS URL.
        return myCustomADALFlow(ctx, serverSPN, stsURL)
    },
)
if err != nil {
    log.Fatal(err)
}
db := sql.OpenDB(connector)
```

This approach is useful when you need to integrate with a custom identity provider, implement token caching, or handle a credential type not covered by the `azuread` package. Most applications should use `NewSecurityTokenConnector` with a pre-acquired token.

## Common credential options

These parameters apply across multiple fedauth types:

| Parameter | Description |
| --- | --- |
| `applicationclientid` | Client application ID. Required for `ActiveDirectoryPassword` and `ActiveDirectoryInteractive`. |
| `clientcertpath` | Path to a PEM or PFX client certificate file for certificate-based service principal or On-Behalf-Of authentication. |
| `clientassertion` | Signed JWT assertion for `ActiveDirectoryClientAssertion` or On-Behalf-Of authentication. |
| `serviceconnectionid` | Azure Pipelines service connection ID. |
| `systemtoken` | Azure Pipelines system access token. |
| `userassertion` | Upstream user token for `ActiveDirectoryOnBehalfOf`. |
| `tokenfilepath` | Path to the OIDC token file for `ActiveDirectoryWorkloadIdentity` in Kubernetes. |
| `additionallyallowedtenants` | Comma-separated list of additional tenant IDs to allow when multitenant auth is needed. |
| `disableinstancediscovery` | Set to `true` to disable instance discovery; use only if you control the authority URL. |
| `sendcertificatechain` | Set to `true` to send the certificate chain for certificate-based authentication. |

## Related content

- [SQL Server and Windows authentication](authentication.md)
- [Connection strings](connection-strings.md)
- [Encryption and certificates](encryption-certificates.md)
