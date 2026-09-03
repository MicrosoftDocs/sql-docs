---
title: Security Best Practices for mssql-django
description: Secure Django applications that use the mssql-django backend with SQL Server, including credential management, TLS configuration, least privilege, and SQL injection prevention.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Security best practices for mssql-django

This article covers security practices for Django applications that connect to SQL Server through the `mssql-django` backend. These practices complement Django's built-in security features and SQL Server's security model.

## Use Microsoft Entra authentication instead of passwords

Microsoft Entra authentication eliminates stored database passwords. Use it for all Azure SQL connections.

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

For the full list of authentication methods, current caveats, and `TOKEN` examples with `DefaultAzureCredential` and `ManagedIdentityCredential`, see [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md).

## Manage credentials securely

When you need SQL authentication, keep credentials out of source code.

### Environment variables

```python
import os

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ["DB_NAME"],
        "USER": os.environ["DB_USER"],
        "PASSWORD": os.environ["DB_PASSWORD"],
        "HOST": os.environ["DB_HOST"],
        "PORT": os.environ.get("DB_PORT", "1433"),
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

### Azure Key Vault

For production deployments, retrieve secrets from Azure Key Vault:

```python
from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient

credential = DefaultAzureCredential()
client = SecretClient(vault_url="https://<your-vault>.vault.azure.net/", credential=credential)

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": client.get_secret("db-name").value,
        "USER": client.get_secret("db-user").value,
        "PASSWORD": client.get_secret("db-password").value,
        "HOST": client.get_secret("db-host").value,
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

> [!CAUTION]  
> Never commit credentials to source control. Add `.env` files to `.gitignore`. Use `git-secrets` or pre-commit hooks to scan for accidental credential commits.

## Enforce TLS encryption

SQL Server connections should always be encrypted. The ODBC driver encrypts connections by default starting with ODBC Driver 18:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            # Encryption is on by default with Driver 18
        },
    },
}
```

If you use ODBC Driver 17, enable encryption explicitly:

```python
"extra_params": "Encrypt=yes"
```

[!INCLUDE [trust-server-certificate-caution](includes/trust-server-certificate-caution.md)]

## Apply the principle of least privilege

Create dedicated SQL Server logins with only the permissions your application needs:

```sql
-- Create a login and user for the application
CREATE LOGIN [django_app]
WITH PASSWORD = '<strong-password>';

USE [<your-database>];

CREATE USER [django_app] FOR LOGIN [django_app];

-- Grant minimum required permissions
-- Read and write data
ALTER ROLE db_datareader ADD MEMBER [django_app];
ALTER ROLE db_datawriter ADD MEMBER [django_app];

-- Allow Django to create and alter tables during migrations
GRANT ALTER ON SCHEMA::dbo TO [django_app];
GRANT CREATE TABLE TO [django_app];
GRANT REFERENCES ON SCHEMA::dbo TO [django_app];
```

For applications that don't run migrations in production, omit the `ALTER` and `CREATE TABLE` permissions:

```sql
-- Production application user (read/write only)
ALTER ROLE db_datareader ADD MEMBER [django_app];
ALTER ROLE db_datawriter ADD MEMBER [django_app];

GRANT EXECUTE ON SCHEMA::dbo TO [django_app]; -- If using stored procedures
```

Run migrations from a separate, more privileged deployment step:

```sql
-- Migration user (used only during deployments)
ALTER ROLE db_ddladmin ADD MEMBER [django_migrations];
```

### Choose the right role

SQL Server fixed database roles are ordered from least to most privileged. Pick the least-privileged role that covers your workload, and only escalate when needed:

| Role | Grants | When to use |
| --- | --- | --- |
| **db_datareader** | `SELECT` on all user tables and views | Read-only reporting users |
| **db_datawriter** | `INSERT`, `UPDATE`, `DELETE` on all user tables | Runtime application user (combine with **db_datareader**) |
| **db_ddladmin** | Create, alter, and drop schema objects | Migration or deployment user only |
| **db_owner** | All database permissions, including security | Avoid for applications; reserve for DBAs |

For finer-grained control than the fixed roles allow, create a custom database role and `GRANT` only the specific permissions on the specific schema your app uses. Keeping all application objects in a dedicated schema (for example, `app`) lets you scope grants with `GRANT ... ON SCHEMA::app` instead of relying on the database-wide **db_datareader** and **db_datawriter** roles.

> [!NOTE]  
> Don't use the `sa` account or **db_owner** fixed database role for application connections. If the application is compromised, an attacker gains full database control.

## Prevent SQL injection

Django's ORM parameterizes all queries automatically. SQL injection is only a risk when you use raw SQL:

### Safe: ORM queries

```python
# Django parameterizes these automatically
users = User.objects.filter(email=user_input)
products = Product.objects.filter(price__lte=max_price)
```

### Safe: Parameterized raw SQL

```python
from django.db import connection

with connection.cursor() as cursor:
    cursor.execute(
        "SELECT * FROM products WHERE category = %s AND price < %s",
        [category, max_price],
    )
```

### Unsafe: String formatting in raw SQL

```python
# NEVER do this - vulnerable to SQL injection
cursor.execute(f"SELECT * FROM products WHERE category = '{category}'")
cursor.execute("SELECT * FROM products WHERE category = '%s'" % category)
```

### Extra and RawSQL

Django's `extra()` and `RawSQL()` accept raw SQL fragments. Always use parameters:

```python
# Safe - parameterized
Product.objects.extra(where=["category = %s"], params=[category])

from django.db.models.expressions import RawSQL
Product.objects.annotate(
    discount=RawSQL("price * %s", [discount_rate])
)
```

> [!IMPORTANT]  
> Never use `extra()` or `RawSQL()` with string formatting. These bypass the ORM's automatic parameterization.

## Configure Django security middleware

Enable Django's built-in security middleware to protect the web layer. While these aren't database-specific, they protect the application that connects to your database:

```python
# settings.py

# HTTPS enforcement
SECURE_SSL_REDIRECT = True
SECURE_HSTS_SECONDS = 31536000  # 1 year
SECURE_HSTS_INCLUDE_SUBDOMAINS = True
SECURE_HSTS_PRELOAD = True

# Cookie security
SESSION_COOKIE_SECURE = True
CSRF_COOKIE_SECURE = True
SESSION_COOKIE_HTTPONLY = True

# Content security
SECURE_CONTENT_TYPE_NOSNIFF = True
```

## Audit database access

Enable SQL Server auditing to track database operations from your Django application:

```sql
-- Create a server audit (Azure SQL uses Azure SQL Auditing instead)
CREATE SERVER AUDIT [DjangoAudit]
TO FILE (FILEPATH = 'C:\Audits\')
WITH (ON_FAILURE = CONTINUE);

ALTER SERVER AUDIT [DjangoAudit] WITH (STATE = ON);

-- Create a database audit specification
USE [<your-database>];

CREATE DATABASE AUDIT SPECIFICATION [DjangoDbAudit]
FOR SERVER AUDIT [DjangoAudit]
ADD (SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo BY [django_app])
WITH (STATE = ON);
```

For Azure SQL Database, enable auditing through the Azure portal or Azure CLI:

```azurecli
az sql db audit-policy update --resource-group <rg> --server <server> \
    --name <database> --state Enabled \
    --storage-account <storage-account>
```

## Secure sensitive columns with Always Encrypted

For column-level encryption of sensitive data like SSNs, credit card numbers, or salary data, use Always Encrypted. The ODBC driver handles encryption and decryption transparently:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "ColumnEncryption=Enabled",
        },
    },
}
```

For detailed setup including key management with Azure Key Vault, see [Always Encrypted with mssql-django](always-encrypted.md).

## Security checklist

| Category | Practice | Priority |
| --- | --- | --- |
| Authentication | Use Microsoft Entra authentication for Azure SQL. | High |
| Credentials | Store secrets in environment variables or Azure Key Vault. | High |
| Encryption | Use ODBC Driver 18 (encryption on by default) or `Encrypt=yes`. | High |
| Injection | Use ORM queries or parameterized raw SQL. Never string-format SQL. | High |
| Least privilege | Create dedicated logins with minimum required permissions. | High |
| TLS | Don't use `TrustServerCertificate=yes` in production. | High |
| Django | Enable `SECURE_SSL_REDIRECT`, secure cookies, HSTS. | Medium |
| Auditing | Enable SQL Server auditing or Azure SQL Auditing. | Medium |
| Column encryption | Use Always Encrypted for highly sensitive columns. | Low |
| Connection | Set `CONN_MAX_AGE` and `CONN_HEALTH_CHECKS` to prevent stale connections. | Low |

## Related content

- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Always Encrypted with mssql-django](always-encrypted.md)
- [Connection options for mssql-django](connection-options.md)
- [Raw SQL queries in mssql-django](raw-sql.md)
- [Retry logic and connection resilience with mssql-django](retry-logic.md)
- [Deploy a Django app with SQL Server to Azure App Service](deploy-azure-app-service.md)
