---
title: Deploy a Django App with SQL Server to Azure App Service
description: Deploy a Django application using mssql-django to Azure App Service with ODBC driver configuration and managed identity.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Deploy a Django app with SQL Server to Azure App Service

This article explains how to deploy a Django application that uses the `mssql-django` backend to Azure App Service, including ODBC driver configuration, environment-based secrets, and managed identity authentication.

## Prerequisites

- An Azure subscription
- An Azure SQL Database or SQL Server instance accessible from Azure
- A Django project configured with `mssql-django`
- [Azure CLI](/cli/azure/install-azure-cli) installed

## ODBC driver on Azure App Service

Azure App Service Linux instances include the Microsoft ODBC Driver for SQL Server. You can verify the installed driver version by running:

```bash
az webapp ssh --resource-group <your-rg> --name <your-app>
odbcinst -j
```

> [!NOTE]  
> Azure App Service typically includes ODBC Driver 17 and/or 18 pre-installed on Linux plans. Windows App Service plans also include the ODBC driver.

## Use environment variables for secrets

Don't hardcode database credentials in `settings.py`. Use environment variables and configure them as App Service application settings:

```python
import os

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ.get("DB_NAME", "<your-database>"),
        "USER": os.environ.get("DB_USER", ""),
        "PASSWORD": os.environ.get("DB_PASSWORD", ""),
        "HOST": os.environ.get("DB_HOST", "<your-server>.database.windows.net"),
        "PORT": os.environ.get("DB_PORT", "1433"),
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

> [!TIP]  
> For required values like `DB_NAME` and `DB_HOST`, consider using `os.environ["DB_NAME"]` (without a default) so the application fails immediately with a clear `KeyError` if the environment variable is missing.

Set the environment variables in Azure App Service:

```azurecli
az webapp config appsettings set \
    --resource-group <your-rg> \
    --name <your-app> \
    --settings DB_NAME=<your-database> DB_HOST=<your-server>.database.windows.net DB_USER=<your-username> DB_PASSWORD=<your-password>
```

## Use managed identity authentication

For production deployments, use managed identity to avoid storing credentials. Enable system-assigned managed identity on your App Service:

```azurecli
az webapp identity assign --resource-group <your-rg> --name <your-app>
```

Grant the managed identity access to your Azure SQL database:

```sql
CREATE USER [<your-app-name>] FOR EXTERNAL PROVIDER;

ALTER ROLE db_datareader ADD MEMBER [<your-app-name>];
ALTER ROLE db_datawriter ADD MEMBER [<your-app-name>];
ALTER ROLE db_ddladmin ADD MEMBER [<your-app-name>];
```

> [!NOTE]
> Grant only the roles your application needs. The **db_ddladmin** fixed database role is required only if the application runs migrations. For read-only workloads, `db_datareader` is sufficient.
>
> If your server is configured for [Microsoft Entra-only authentication](/azure/azure-sql/database/authentication-azure-ad-only-authentication), `FROM EXTERNAL PROVIDER` fails with `Msg 33130` because the server can't reach Microsoft Graph to resolve the identity name. Create the user manually with `CREATE USER [<your-app-name>] WITH SID = 0x<sid-hex>, TYPE = E`, where `<sid-hex>` is derived from the managed identity's **application (client) ID**, not its object ID. For the conversion steps, see [Grant the identity access in Azure SQL](microsoft-entra-authentication.md#grant-the-identity-access-in-azure-sql).

Configure `settings.py` to use managed identity:

```python
import os

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ.get("DB_NAME", "<your-database>"),
        "HOST": os.environ.get("DB_HOST", "<your-server>.database.windows.net"),
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Authentication=ActiveDirectoryMsi",
        },
    },
}
```

### Use access tokens with ManagedIdentityCredential

Alternatively, use the `TOKEN` setting with `azure.identity`:

> [!CAUTION]
> The `TOKEN` value is fetched once at process startup and expires after [60-90 minutes](/entra/identity-platform/configurable-token-lifetimes#access-tokens). For long-running App Service deployments, use this approach only if you also implement token refresh logic or short-lived worker recycling. If the direct `ActiveDirectoryMsi` pattern works in your environment, it avoids the startup-time token issue.

```python
import os
from azure.identity import ManagedIdentityCredential

credential = ManagedIdentityCredential()
token = credential.get_token("https://database.windows.net/.default").token

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ.get("DB_NAME", "<your-database>"),
        "HOST": os.environ.get("DB_HOST", "<your-server>.database.windows.net"),
        "PORT": "1433",
        "TOKEN": token,
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

> [!TIP]  
> `DefaultAzureCredential` is convenient for development and shared codebases because it tries multiple credential types automatically, so the same code works on a laptop, in CI, and in Azure. However, each credential type that doesn't apply adds several seconds of timeout, which slows down startup. Since App Service always has managed identity available, `ManagedIdentityCredential` authenticates immediately without the probe chain. Install `azure-identity` in your requirements file: `pip install azure-identity`.

## Run migrations during deployment

Add a post-deployment script or startup command to run migrations automatically:

```azurecli
az webapp config set \
    --resource-group <your-rg> \
    --name <your-app> \
    --startup-file "python manage.py migrate && gunicorn myproject.wsgi"
```

## Collect static files

Configure static file handling for production:

```python
STATIC_URL = "/static/"
STATIC_ROOT = os.path.join(BASE_DIR, "staticfiles")
```

Run `collectstatic` as part of your deployment:

```bash
python manage.py collectstatic --noinput
```

## Deploy to App Service

App Service can host a Django app in two ways:

- **Built-in Linux Python image**: App Service builds your code from source and provides the Python runtime.
- **Custom Docker image**: you build the image yourself and reference it from a container registry.

Both paths can use a system-assigned managed identity to authenticate to Azure SQL, but the recipe differs. Pick the tab that matches your deployment.

# [Built-in Python image](#tab/builtin)

With the built-in Linux Python image, App Service's Oryx builder installs `requirements.txt` and starts your Django app under `gunicorn` automatically. The local managed-identity HTTP proxy makes `Authentication=ActiveDirectoryMsi` work directly from the ODBC connection string. Use the `settings.py` shown in [Use managed identity authentication](#use-managed-identity-authentication).

Deploy your code with `az webapp up`, enable system-assigned managed identity, and set the database environment variables:

```azurecli
az webapp up \
    --resource-group <your-rg> \
    --name <your-app> \
    --runtime "PYTHON:3.12" \
    --sku B1

az webapp identity assign --resource-group <your-rg> --name <your-app>

az webapp config appsettings set \
    --resource-group <your-rg> \
    --name <your-app> \
    --settings DB_NAME=<your-database> DB_HOST=<your-server>.database.windows.net
```

Then grant the managed identity access in SQL as described in [Use managed identity authentication](#use-managed-identity-authentication).

# [Custom Docker image](#tab/container)

Use a custom image when you need a specific ODBC driver version, additional system packages, or full control over the runtime.

App Service exposes managed identity to apps through the `IDENTITY_ENDPOINT` and `IDENTITY_HEADER` environment variables, not the standard Azure Instance Metadata Service (IMDS) endpoint at `169.254.169.254` that the ODBC driver expects when `Authentication=ActiveDirectoryMsi` is set. In a custom container, the ODBC driver's MSI mode can't reach IMDS, so the connection hangs until the login timer expires. Fetch the access token in Python and pass it to `mssql-django` through the `TOKEN` setting. Use the `settings.py` pattern shown in [Use access tokens with ManagedIdentityCredential](#use-access-tokens-with-managedidentitycredential).

Create a `Dockerfile` in your project root. The Dockerfile installs the ODBC driver, keeps the Kerberos runtime that the driver needs for Microsoft Entra authentication, and starts the app under `gunicorn`:

```dockerfile
FROM python:3.12-slim

# Install ODBC driver and runtime dependencies. libgssapi-krb5-2 is listed
# explicitly so apt-get --auto-remove keeps it after curl and gnupg2 are
# purged; msodbcsql18 needs it at runtime for Microsoft Entra authentication.
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    gnupg2 \
    unixodbc-dev \
    libgssapi-krb5-2 \
    && curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg \
    && curl -fsSL https://packages.microsoft.com/config/debian/12/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y --no-install-recommends msodbcsql18 \
    && apt-get purge -y --auto-remove curl gnupg2 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .
RUN python manage.py collectstatic --noinput

EXPOSE 8000
CMD ["gunicorn", "myproject.wsgi", "--bind", "0.0.0.0:8000", "--timeout", "180"]
```

Custom containers don't get Oryx's automatic install, so list `gunicorn` and `azure-identity` in your `requirements.txt`:

```text
Django
mssql-django
gunicorn
azure-identity
```

For `RUN python manage.py collectstatic --noinput` to succeed during `docker build`, `settings.py` must be importable before the runtime environment variables are set. Read `DB_NAME` and `DB_HOST` with `os.environ.get(..., "")` rather than `os.environ[...]`, and make sure `STATIC_ROOT` is configured as shown in [Collect static files](#collect-static-files).

> [!IMPORTANT]
> Two common omissions surface as misleading errors:
>
> - Without `libgssapi-krb5-2`, the first database call fails with `Can't open lib '/opt/microsoft/msodbcsql18/lib64/libmsodbcsql-18.x.so.x.x' : file not found`, even though the `.so` file is present. The driver loader is reporting a missing transitive dependency.
> - Without `gunicorn` in `requirements.txt`, the container fails to start with `exec: "gunicorn": executable file not found in $PATH`.
>
> See [Troubleshooting](troubleshooting.md#docker-and-container-issues) for more container errors.

Build, push, and deploy the image:

```azurecli
# Create a container registry (one time)
az acr create --resource-group <your-rg> --name <your-acr> --sku Basic --admin-enabled true

# Build and push the image
az acr build --registry <your-acr> --image mydjango:latest .

# Create an App Service plan for containers
az appservice plan create --name <your-plan> --resource-group <your-rg> --is-linux --sku B1

# Create a web app using the container image
az webapp create \
    --resource-group <your-rg> \
    --plan <your-plan> \
    --name <your-app> \
    --container-image-name <your-acr>.azurecr.io/mydjango:latest

# Enable system-assigned managed identity
az webapp identity assign --resource-group <your-rg> --name <your-app>

# Configure database environment variables
az webapp config appsettings set \
    --resource-group <your-rg> \
    --name <your-app> \
    --settings DB_NAME=<your-database> DB_HOST=<your-server>.database.windows.net
```

Then grant the managed identity access in SQL as described in [Use managed identity authentication](#use-managed-identity-authentication).

---

## Local development with Docker Compose

For local development and testing, use Docker Compose to run your Django app alongside a SQL Server container:

```yaml
# docker-compose.yml
services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "<password>"  # Must meet SQL Server complexity requirements
    ports:
      - "1433:1433"

  web:
    build: .
    ports:
      - "8000:8000"
    environment:
      DB_HOST: db
      DB_NAME: mydb
      DB_USER: sa
      DB_PASSWORD: "<password>"
    depends_on:
      - db
```

> [!TIP]  
> The SQL Server container doesn't create application databases automatically. After starting the containers, create the database and run migrations:
>
> ```bash
> docker compose exec db /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "<password>" -No -Q "CREATE DATABASE mydb"
> docker compose exec web python manage.py migrate
> ```

The SQL Server container image requires `ACCEPT_EULA=Y` and a strong SA password. For production environments, use Azure SQL Database with managed identity instead of SQL Server credentials. See [Use managed identity authentication](#use-managed-identity-authentication).

## Related content

- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Connection options for mssql-django](connection-options.md)
- [Container and local development with mssql-django](container-local-development.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Quickstart: Deploy a Python (Django, Flask, or FastAPI) web app to Azure App Service](/azure/app-service/quickstart-python)
