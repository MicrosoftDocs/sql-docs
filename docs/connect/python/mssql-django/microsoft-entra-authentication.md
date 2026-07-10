---
title: Microsoft Entra Authentication with mssql-django
description: Configure Microsoft Entra authentication for Django applications using mssql-django with SQL Server and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Microsoft Entra authentication with mssql-django

This article explains how to configure Microsoft Entra authentication for Django applications using the `mssql-django` backend. Microsoft Entra authentication eliminates the need to store passwords in your application configuration.

## Prerequisites

- [Microsoft ODBC Driver 18 for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md) (recommended). All authentication modes in this article are supported with Microsoft ODBC Driver 18 for SQL Server. `ActiveDirectoryInteractive` is Windows-only regardless of driver version. If you must use ODBC Driver 17, see the [ODBC authentication reference](../../odbc/using-azure-active-directory.md) for per-mode minimum 17.x versions.
- For access token authentication: `pip install azure-identity`.

## Authentication methods

Configure each method by adding or editing the `DATABASES` setting in your Django project's `settings.py` file. The examples in this article show the full `DATABASES["default"]` block for clarity; copy the relevant keys into your existing configuration.

`mssql-django` supports Microsoft Entra authentication in two ways:

- ODBC driver authentication through `OPTIONS["extra_params"]`. The backend appends this string to the ODBC connection string unchanged, so the available `Authentication=` values come from the installed Microsoft ODBC Driver for SQL Server, not from `mssql-django` itself.
- Programmatic access token authentication through the `TOKEN` setting. The backend passes `TOKEN` to the ODBC driver as `SQL_COPT_SS_ACCESS_TOKEN`, which bypasses the ODBC `Authentication=` keyword.

### Authentication methods at a glance

| Method | Configure with | Best for |
| --- | --- | --- |
| Access token | `TOKEN` | Development, short-lived scripts, or apps with custom token refresh |
| `ActiveDirectoryMsi` | `extra_params` | Azure-hosted production apps (system-assigned and user-assigned managed identity) |
| `ActiveDirectoryServicePrincipal` | `USER`, `PASSWORD`, `extra_params` | App registrations when managed identity isn't available |
| `ActiveDirectoryIntegrated` | `extra_params` | Domain-joined user context |
| `ActiveDirectoryInteractive` | `USER`, `extra_params` | User sign-in with multifactor authentication (Windows) |
| `ActiveDirectoryDefault` | `extra_params` | Local development and apps that should use the ODBC driver's default Microsoft Entra credential chain |
| `ActiveDirectoryPassword` | `USER`, `PASSWORD`, `extra_params` | Last-resort legacy scenarios only (deprecated) |

> [!NOTE]
> `mssql-django` 1.7.3 and later accepts `Authentication=ActiveDirectoryDefault` through `OPTIONS["extra_params"]` when the installed Microsoft ODBC Driver for SQL Server supports that mode. If you need explicit control over token acquisition and refresh behavior, use the [TOKEN](#access-token-authentication-token) pattern with the `azure.identity.DefaultAzureCredential` class.

### Grant the identity access in Azure SQL

For managed identity or service principal authentication, create a database user and grant only the roles your application needs:

```sql
CREATE USER [<identity-name>] FOR EXTERNAL PROVIDER;

ALTER ROLE db_datareader ADD MEMBER [<identity-name>];
ALTER ROLE db_datawriter ADD MEMBER [<identity-name>];
ALTER ROLE db_ddladmin ADD MEMBER [<identity-name>];
```

The **db_ddladmin** fixed database role is required only if the application runs migrations. For read-only workloads, `db_datareader` is sufficient.

> [!NOTE]
> `FROM EXTERNAL PROVIDER` requires the SQL server to call Microsoft Graph to resolve the principal name. If the server is configured for [Microsoft Entra-only authentication](/azure/azure-sql/database/authentication-azure-ad-only-authentication) or otherwise can't reach Graph, the statement fails with `Msg 33130` (`Principal '<name>' could not be found...`). Create the user manually instead by supplying an explicit SID:
>
> ```sql
> CREATE USER [<identity-name>] WITH SID = 0x<sid-hex>, TYPE = E;
> ```
>
> For a managed identity or service principal, derive the SID from the identity's **application (client) ID**, not its object ID. Azure SQL uses the application ID for service principals and managed identities, and the object ID only for regular Entra users. Convert the GUID by reversing the first three dash-separated groups byte-by-byte and keeping the last two as-is. For example, application ID `00001111-aaaa-2222-bbbb-3333cccc4444` becomes SID `0x11110000AAAA2222BBBB3333CCCC4444`. In PowerShell:
>
> ```powershell
> $b = ([Guid]"<app-id>").ToByteArray()
> "0x" + (($b | ForEach-Object { $_.ToString('X2') }) -join '')
> ```
>
> If you use the object ID by mistake, the connection succeeds in acquiring a token, but Azure SQL returns `Login failed for user '<token-identified principal>'` because no database principal matches the token's `appid` claim.

If you see a `VIEW ANY COLUMN MASTER KEY DEFINITION permission denied` error, grant the identity additional access for Always Encrypted scenarios:

```sql
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [<identity-name>];
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [<identity-name>];
```

### Managed identity authentication (`ActiveDirectoryMsi`)

Use managed identity when your Django app runs on an Azure service such as Azure App Service, Azure Container Apps, or Azure Virtual Machines. This approach is recommended for production environments because the ODBC driver acquires and refreshes tokens automatically.

System-assigned managed identity:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryMsi",
        },
    },
}
```

User-assigned managed identity:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": (
                "Authentication=ActiveDirectoryMsi;"
                "UID=<managed-identity-client-id-or-object-id>"
            ),
        },
    },
}
```

`ActiveDirectoryMsi` is the ODBC mode for both system-assigned managed identity (SAMI) and user-assigned managed identity (UAMI). For UAMI, the ODBC driver expects `UID` to identify the managed identity: use the client ID for Azure App Service or Azure Container Instance, otherwise use the object ID. Put that `UID` inside `extra_params`, because `extra_params` is passed directly to the ODBC driver.

If you use managed identity, create the test database manually and pass `--keepdb` when you run unit tests.

### Service principal authentication (`ActiveDirectoryServicePrincipal`)

Use a Microsoft Entra app registration (service principal) when your app runs without user context and managed identity isn't available.

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<application-client-id>",
        "PASSWORD": "<client-secret>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryServicePrincipal",
        },
    },
}
```

Don't hardcode client secrets in `settings.py`. Use environment variables or a secret manager such as Azure Key Vault to provide credentials at runtime.

### Integrated authentication (`ActiveDirectoryIntegrated`)

Use integrated authentication when the Django process runs under a domain-joined user context and you want the ODBC driver to redeem that Windows or Kerberos identity for Microsoft Entra authentication.

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryIntegrated",
        },
    },
}
```

The ODBC authentication reference documents this mode on Windows, and on Linux or macOS with ODBC Driver 17.6 and later versions for federated environments.

### Interactive authentication (`ActiveDirectoryInteractive`)

Use interactive authentication for local user sign-in when you want the driver to prompt for credentials and handle multifactor authentication.

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<user@email.com>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryInteractive",
        },
    },
}
```

The main ODBC authentication reference documents `ActiveDirectoryInteractive` as Windows-only. If you plan to use it on another platform, validate the behavior with your exact driver version first.

### Default credential chain authentication (`ActiveDirectoryDefault`)

Use this mode when you want the ODBC driver to apply its default Microsoft Entra credential chain.

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryDefault",
        },
    },
}
```

`mssql-django` 1.7.3 and later versions pass this mode through to the ODBC driver. If you need explicit control over credential source or token refresh behavior, use [access token authentication](#access-token-authentication-token).

### Access token authentication (`TOKEN`)

Use `TOKEN` when you want your Python code to acquire the Microsoft Entra token itself.

```python
from azure.identity import DefaultAzureCredential

credential = DefaultAzureCredential()
token = credential.get_token("https://database.windows.net/.default").token

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "TOKEN": token,
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

This path works with any Python credential class, including `DefaultAzureCredential`, `ManagedIdentityCredential`, and `ClientSecretCredential`.

Access tokens fetched in `settings.py` are evaluated once at process startup and usually expire after 60 to 90 minutes. If your Django process stays alive longer than the token lifetime, you must refresh the token in application code. For most long-running production apps, use an ODBC driver mode that refreshes tokens automatically, such as `ActiveDirectoryMsi` or `ActiveDirectoryServicePrincipal`.

### Password authentication (`ActiveDirectoryPassword`, deprecated)

[!INCLUDE [entra-password-auth-deprecation](../../../includes/entra-password-auth-deprecation.md)]

If you must use it for a legacy scenario, configure it explicitly:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<user@email.com>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryPassword",
        },
    },
}
```

## Related content

- [Security best practices for mssql-django](security-best-practices.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Connection options for mssql-django](connection-options.md)
- [Deploy a Django app with SQL Server to Azure App Service](deploy-azure-app-service.md)
- [Using Microsoft Entra ID with the ODBC Driver](../../odbc/using-azure-active-directory.md)
- [Configure and manage Microsoft Entra authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure)
- [Microsoft Entra authentication wiki](https://github.com/microsoft/mssql-django/wiki/Azure-AD-Authentication)
- [Always Encrypted with mssql-django](always-encrypted.md)
