---
title: Microsoft Entra Authentication with mssql-python
description: Learn how to connect to Azure SQL using Microsoft Entra ID authentication with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Microsoft Entra authentication with mssql-python

Microsoft Entra ID provides identity-based authentication for Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric through the mssql-python driver. Microsoft Entra authentication offers these capabilities over SQL authentication:

- Centralized identity management through Microsoft Entra ID.
- Token-based authentication that eliminates the need for passwords.
- Support for conditional access policies.
- Managed identities for Azure-hosted applications.

The mssql-python driver supports seven Microsoft Entra authentication modes, all configured through the `Authentication` connection string keyword.

## Authentication modes

Set the `Authentication` keyword in your connection string to one of the following values:

| Authentication value | Description |
| --------------------- | ------------- |
| `ActiveDirectoryDefault` | Uses `DefaultAzureCredential`, which tries multiple methods automatically. |
| `ActiveDirectoryInteractive` | Browser-based interactive sign-in. |
| `ActiveDirectoryDeviceCode` | Code entry at <https://microsoft.com/devicelogin>. |
| `ActiveDirectoryPassword` | Username and password with Microsoft Entra ID. **Deprecated.** |
| `ActiveDirectoryMSI` | Managed identity (system-assigned or user-assigned). |
| `ActiveDirectoryServicePrincipal` | Service principal with client ID and secret. |
| `ActiveDirectoryIntegrated` | Windows Integrated with Microsoft Entra ID (Kerberos). |

> [!NOTE]
> The `ActiveDirectoryDefault`, `ActiveDirectoryInteractive`, and `ActiveDirectoryDeviceCode` modes require the `azure-identity` package. Install it with `pip install azure-identity`.

## DefaultAzureCredential

The `ActiveDirectoryDefault` mode uses `DefaultAzureCredential` from the Azure Identity SDK, which tries these authentication methods in order:

1. Environment variables.
1. Workload identity for Kubernetes.
1. Managed identity.
1. Azure CLI credentials.
1. Azure PowerShell credentials.
1. Azure Developer CLI credentials.
1. Interactive browser, if enabled.

### Example: Default authentication

The following example connects with `ActiveDirectoryDefault`, which uses the `DefaultAzureCredential` chain to find a valid credential automatically:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)

cursor = conn.cursor()
cursor.execute("SELECT USER_NAME()")
print(f"Connected as: {cursor.fetchval()}")
```

Use this mode for local development because it picks up Azure CLI credentials automatically. For production, use a specific authentication mode (`ActiveDirectoryMSI`, `ActiveDirectoryServicePrincipal`) instead. `DefaultAzureCredential` walks through multiple credential providers on each first connection, which adds latency that production workloads don't need.

## Interactive authentication

For interactive applications, use browser-based authentication. The user must have a database account created with `CREATE USER [user@domain.com] FROM EXTERNAL PROVIDER`. For full prerequisites, see [Configure Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-configure?view=azuresql&preserve-view=true).

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryInteractive;"
    "Encrypt=yes;"
)
```

On Windows, this mode delegates to the ODBC driver's native interactive flow. On other platforms, it uses the Azure Identity SDK's browser-based authentication.

## Device code authentication

Use device code authentication for environments without a browser, such as SSH sessions or containers. The user must have a database account created with `CREATE USER [user@domain.com] FROM EXTERNAL PROVIDER`. For prerequisites, see [Configure Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-configure?view=azuresql&preserve-view=true).

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDeviceCode;"
    "Encrypt=yes;"
)
# Output: To sign in, use a web browser to open https://microsoft.com/devicelogin
# and enter the code XXXXXXX to authenticate.
```

Follow the prompt to authenticate in a browser on another device.

## Service principal authentication

Use service principal authentication for automated applications that don't require user interaction:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryServicePrincipal;"
    "UID=<client-id>;"       # Application (client) ID
    "PWD=<client-secret>;"   # Client secret
    "Encrypt=yes;"
)
```

### Create a service principal

1. [Register an application](/azure/azure-sql/database/authentication-aad-service-principal?view=azuresql&preserve-view=true) in Microsoft Entra ID.
1. Create a client secret.
1. Grant the service principal access to your database:

```sql
-- In Azure SQL
CREATE USER [app-name] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [app-name];
ALTER ROLE db_datawriter ADD MEMBER [app-name];
```

> [!TIP]
> If `CREATE USER` fails with error 33131 (duplicate display name), use `WITH OBJECT_ID` to specify the service principal's Object ID from the **Enterprise applications** page in the Azure portal (not the App registration page):
>
> ```sql
> CREATE USER [app-name] FROM EXTERNAL PROVIDER
>     WITH OBJECT_ID = '<enterprise-app-object-id>';
> ```
>
> For details, see [Microsoft Entra logins and users with nonunique display names](../../../relational-databases/security/authentication-access/authentication-microsoft-entra-create-users-with-nonunique-names.md).

## Managed identity

Use managed identity authentication for Azure-hosted applications, such as App Service, Azure Functions, and VMs:

### System-assigned managed identity

Connect using the identity assigned directly to the Azure resource:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryMSI;"
    "Encrypt=yes;"
)
```

### User-assigned managed identity

Specify the client ID of a user-assigned managed identity in the `UID` field:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryMSI;"
    "UID=<managed-identity-client-id>;"
    "Encrypt=yes;"
)
```

### Configure database access

Grant the managed identity access in your database. A [Microsoft Entra admin must be configured](/azure/azure-sql/database/authentication-aad-configure?view=azuresql&preserve-view=true#provision-azure-ad-admin-sql-database) on the server before you can create external users. To enable managed identity on your Azure resource, see [Managed identities for Azure resources](/entra/identity/managed-identities-azure-resources/overview).

```sql
-- Replace 'my-app-service' with your Azure resource name
CREATE USER [my-app-service] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [my-app-service];
ALTER ROLE db_datawriter ADD MEMBER [my-app-service];
```

## Password authentication (deprecated)

[!INCLUDE [Microsoft Entra password authentication deprecation](../../../includes/entra-password-auth-deprecation.md)]

Use password authentication when you need a username and password with a Microsoft Entra account. The user must have a database account created with `CREATE USER [user@domain.com] FROM EXTERNAL PROVIDER`:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryPassword;"
    "UID=<login@domain.com>;"
    "PWD=<password>;"
    "Encrypt=yes;"
)
```

## Windows Integrated authentication

Use Windows Integrated authentication for domain-joined Windows environments with Kerberos. This mode requires your on-premises Active Directory to be [federated with Microsoft Entra ID](/entra/identity/hybrid/connect/whatis-fed) and a [Microsoft Entra admin configured](/azure/azure-sql/database/authentication-aad-configure?view=azuresql&preserve-view=true) on the server:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryIntegrated;"
    "Encrypt=yes;"
)
```

This mode uses the current Windows user's Kerberos credentials. On Linux and macOS, you must configure Kerberos manually (`krb5.conf` and a valid keytab or ticket). See [Active Directory authentication for SQL Server on Linux](../../../linux/security/authentication/active-directory-overview.md) for client-side Kerberos setup.

## Credential objects with token_provider

Pass a credential object directly with the `token_provider` parameter. The driver calls the object's `get_token()` method when it needs a token, so you don't pack the token into a connection attribute yourself.

Any object with a `get_token(scope)` method that returns an object with a `.token` attribute satisfies the contract. Every credential in the [azure-identity](/python/api/overview/azure/identity-readme) package qualifies, including `DefaultAzureCredential`, `AzureCliCredential`, `ManagedIdentityCredential`, and `ClientSecretCredential`.

```python
import mssql_python
from azure.identity import DefaultAzureCredential

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Encrypt=yes",
    token_provider=DefaultAzureCredential(),
)
```

The driver requests the `https://database.windows.net/.default` scope. This parameter supports the Azure commercial cloud scope only. For sovereign clouds, use [Access token authentication](#access-token-authentication) and request the scope your cloud requires.

Bulk copy operations acquire a fresh token from the provider for each operation, because they open their own connection.

You can also supply your own object, which is useful when the token comes from somewhere other than `azure-identity`, such as a notebook environment that exposes its own token helper:

```python
from types import SimpleNamespace

class NotebookTokenProvider:
    def get_token(self, scope):
        # Return any object with a .token attribute holding the raw JWT string.
        return SimpleNamespace(token=get_platform_token(scope))

conn = mssql_python.connect(connection_string, token_provider=NotebookTokenProvider())
```

The driver exports a `TokenProvider` protocol type for static type checking:

```python
from mssql_python import TokenProvider

def open_connection(credential: TokenProvider):
    return mssql_python.connect(connection_string, token_provider=credential)
```

The `token_provider` parameter is the only token source for a connection that uses it. The driver raises `InterfaceError` if you combine it with either of the following:

- The `Authentication` keyword in the connection string.

- A token passed through `attrs_before` with `SQL_COPT_SS_ACCESS_TOKEN`.

Passing an object without a `get_token()` method also raises `InterfaceError`.

If the connection string carries `UID` or `PWD`, the driver ignores them and issues a `UserWarning` that names the ignored keywords. Remove them from the connection string to silence the warning.

> [!NOTE]
> Connections that authenticate with a credential object are pooled per identity. For more information, see [Connection pooling](connection-pooling.md).

## Access token authentication

You might acquire tokens externally, for example, through a shared token cache or a sovereign cloud endpoint. In these cases, use `SQL_COPT_SS_ACCESS_TOKEN` with the `attrs_before` parameter to pass the token directly. This approach bypasses the driver's built-in token acquisition flow.

Prefer the `token_provider` parameter described in the previous section when your credential comes from `azure-identity`. It handles the token encoding for you and refreshes tokens for pooled connections.

```python
import mssql_python
from azure.identity import DefaultAzureCredential
import struct

def get_token():
    credential = DefaultAzureCredential(
        exclude_interactive_browser_credential=False
    )
    token_bytes = credential.get_token(
        "https://database.windows.net/.default"
    ).token.encode("utf-16le")
    token_struct = struct.pack(
        f'<I{len(token_bytes)}s', len(token_bytes), token_bytes
    )
    return token_struct

SQL_COPT_SS_ACCESS_TOKEN = 1256

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;",
    attrs_before={SQL_COPT_SS_ACCESS_TOKEN: get_token()}
)
```

> [!IMPORTANT]
> When using `SQL_COPT_SS_ACCESS_TOKEN`, the connection string must not include `UID`, `PWD`, `Authentication`, or `Trusted_Connection`. The token itself handles authentication.

## Choose an authentication mode

| Scenario | Recommended mode |
| ---------- | ------------------ |
| Development machine | `ActiveDirectoryDefault` (uses Azure CLI) |
| Azure App Service / Functions | `ActiveDirectoryMSI` (faster than Default) |
| Azure Kubernetes Service | `ActiveDirectoryDefault` (workload identity) |
| Automated scripts on-premises | `ActiveDirectoryServicePrincipal` |
| Interactive desktop app | `ActiveDirectoryInteractive` |
| SSH/container without browser | `ActiveDirectoryDeviceCode` |

## Troubleshoot

### "Login failed for user 'NT AUTHORITY\ANONYMOUS LOGON'"

Verify that the user or managed identity exists in the database:

```sql
CREATE USER [identity-name] FROM EXTERNAL PROVIDER;
```

### "AADSTS700016: Application not found"

The service principal or application ID is incorrect. Verify the client ID and that the app is registered in your Microsoft Entra tenant.

### "Managed Identity endpoint not reachable"

- Verify that managed identity is enabled on the Azure resource.
- For user-assigned identity, verify the client ID is correct.
- Check that the resource has network access to the identity endpoint.

### Token acquisition timeout

`ActiveDirectoryDefault` uses `DefaultAzureCredential`, which walks a chain of credential providers in sequence until one succeeds. This chain walk adds seconds of latency on the first connection, especially when earlier providers in the chain (environment variables, workload identity) fail before reaching the one that works. In production, specify the credential type directly to skip the chain:

```python
# Slow: DefaultAzureCredential tries multiple providers
conn = mssql_python.connect(connection_string, authentication="ActiveDirectoryDefault")

# Fast: Skip directly to managed identity
conn = mssql_python.connect(connection_string, authentication="ActiveDirectoryMSI")
```

## Related content

- [Connection strings](connection-strings.md)
- [Troubleshooting](troubleshooting.md)
- [Azure SQL passwordless migration](/azure/azure-sql/database/azure-sql-passwordless-migration-python)
- [Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-overview)
