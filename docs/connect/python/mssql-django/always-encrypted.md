---
title: Always Encrypted with mssql-django
description: Configure column-level encryption with Always Encrypted for Django applications using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Always Encrypted with mssql-django

This article explains how to use SQL Server Always Encrypted with Django applications through the `mssql-django` backend. Always Encrypted provides column-level encryption that protects sensitive data at rest and in transit.

## Prerequisites

- Microsoft ODBC Driver 17 or 18 for SQL Server
- SQL Server 2016 or later, or Azure SQL Database
- Column encryption configured on the SQL Server side (column master key and column encryption key)

## How it works

Always Encrypted is handled by the ODBC driver layer, not by Django itself. When you enable the `ColumnEncryption` ODBC parameter, the driver automatically encrypts and decrypts data as it passes between your application and SQL Server. Django models and queries work the same way whether columns are encrypted or not.

## Windows certificate store

When column master keys are stored in the Windows certificate store, enable Always Encrypted by adding `ColumnEncryption=Enabled` to `extra_params`:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "ColumnEncryption=Enabled",
        },
    },
}
```

This approach works only on Windows.

## Azure Key Vault with client ID and secret

When column master keys are stored in Azure Key Vault, provide the application credentials in `extra_params`:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": (
                "ColumnEncryption=Enabled;"
                "KeyStoreAuthentication=KeyVaultClientSecret;"
                "KeyStorePrincipalId=<application-client-id>;"
                "KeyStoreSecret=<client-secret>"
            ),
        },
    },
}
```

Replace `<application-client-id>` and `<client-secret>` with the app registration's **Application (client) ID** and client secret value.

> [!IMPORTANT]  
> Don't hardcode secrets in `settings.py`. Use environment variables or a secret manager to provide credentials at runtime.

## Azure Key Vault with managed identity

When running on Azure (for example, Azure Virtual Machines or Azure App Service), use managed identity to access Azure Key Vault.

### System-assigned managed identity

No extra configuration is needed beyond the `KeyStoreAuthentication` parameter:

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
                "ColumnEncryption=Enabled;"
                "KeyStoreAuthentication=KeyVaultManagedIdentity"
            ),
        },
    },
}
```

### User-assigned managed identity

Include the managed identity's client ID (also called application ID):

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
                "ColumnEncryption=Enabled;"
                "KeyStoreAuthentication=KeyVaultManagedIdentity;"
                "KeyStorePrincipalId=<managed-identity-client-id>"
            ),
        },
    },
}
```

### Grant managed identity permissions

1. Grant the managed identity access to your Azure SQL database:

   ```sql
   CREATE USER [<identity-name>] FOR EXTERNAL PROVIDER;

   ALTER ROLE db_datareader ADD MEMBER [<identity-name>];
   ALTER ROLE db_datawriter ADD MEMBER [<identity-name>];

   GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [<identity-name>];
   GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [<identity-name>];
   ```

1. Grant the managed identity access to the Azure Key Vault that stores the column master key, with the permissions listed in the [Always Encrypted Azure Key Vault documentation](/azure/azure-sql/database/always-encrypted-azure-key-vault-configure?tabs=azure-powershell#create-a-key-vault-to-store-your-keys).

## Let Django manage encrypted tables

When using Always Encrypted with Django, configure encryption objects on SQL Server first, then run migrations.

Recommended order:

1. Create the column master key (CMK) and column encryption key (CEK) on SQL Server or Azure SQL.
1. Configure `ColumnEncryption=Enabled` in Django connection settings.
1. Run Django migrations.
1. Encrypt target columns with SQL Server Management Studio or T-SQL.

Run migrations:

```bash
python manage.py migrate
```

`mssql-django` doesn't create or manage Always Encrypted key metadata. Key creation and column encryption policy remain SQL Server administrative tasks.

## Unsupported authentication methods

Username/password and Azure Key Vault Interactive authentication aren't supported for Always Encrypted key store access.

## Related content

- [Security best practices for mssql-django](security-best-practices.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Always Encrypted wiki](https://github.com/microsoft/mssql-django/wiki/Always-Encrypted)
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
