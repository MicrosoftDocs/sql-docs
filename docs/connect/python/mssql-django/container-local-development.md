---
title: Container and Local Development with mssql-django
description: Set up local development environments, Docker containers, devcontainers, and CI pipelines for Django applications that use the mssql-django backend with SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sharmag, sumitsar
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Container and local development with mssql-django

This guide covers environment setup for Django developers working with the `mssql-django` backend across Windows, Linux, macOS, Docker containers, devcontainers, and CI pipelines.

## Prerequisites

- Python 3.10 through 3.14. Django 6.0 and 6.1 require Python 3.12 and later versions.
- Docker Desktop (for container-based development)
- Microsoft ODBC Driver 17 or 18 for SQL Server when you use the default pyodbc path. See [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).
- A base image compatible with the required `mssql-python` package: Windows x64, Windows ARM64 with Python 3.11 and later versions, macOS 15 and later versions, or Linux x64/ARM64 with glibc 2.28 and later versions or musl 1.2 and later versions. SUSE Linux on ARM64 isn't supported.

The mssql-python path doesn't require a separate Microsoft ODBC Driver for SQL Server install. It still needs the unixODBC runtime, because the backend imports pyodbc when Django loads it. For more information, see [Select the database driver for mssql-django](select-database-driver.md).

## Local SQL Server with sqlcmd (recommended)

The [**sqlcmd**](../../../tools/sqlcmd/sqlcmd-utility.md) (Go) utility can create a SQL Server container in a single command. It handles the Docker image pull, password generation, port assignment, and connection context automatically:

```bash
sqlcmd create mssql --accept-eula
```

To create a container with a sample database already attached:

```bash
sqlcmd create mssql --accept-eula --using https://aka.ms/AdventureWorksLT.bak
```

After creation, `sqlcmd` stores the connection context so you can query immediately:

```bash
sqlcmd query "SELECT @@VERSION"
```

Configure Django to connect using the connection details that `sqlcmd` printed at creation. Use `sqlcmd config view` to retrieve them later:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "master",
        "USER": "sa",
        "PASSWORD": "<password from sqlcmd output>",
        "HOST": "localhost",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "TrustServerCertificate=yes",
        },
    },
}
```

When you're done, stop or delete the container:

```bash
sqlcmd stop
sqlcmd delete
```

> [!TIP]  
> Run `sqlcmd create mssql --user-database mydb` to create a container with an empty user database ready for development.

## Local SQL Server in Visual Studio Code

The [MSSQL extension for Visual Studio Code](../../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) can create local SQL Server containers directly from the editor:

1. Open the **SQL Server** view in the Activity Bar.
1. Select **Add Connection** > **Create Local SQL Server** (or use the Command Palette: **MS SQL: Create Local SQL Server**).
1. Choose the SQL Server version and accept the EULA.
1. The extension pulls the container image, generates a password, and adds a connection profile automatically.

Once the container is running, you can browse databases, run queries, and manage objects in Visual Studio Code before switching to your Django code.

## Local SQL Server with Docker

If you prefer to manage containers directly, the official SQL Server container image works with two environment variables:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<strong_password>" \
  -p 1433:1433 --name sql1 \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

> [!IMPORTANT]  
> Use `MSSQL_SA_PASSWORD` for SQL Server containers. The older `SA_PASSWORD` variable is deprecated. The password must meet SQL Server complexity requirements: at least 8 characters, with uppercase, lowercase, digits, and special characters.

Wait a few seconds for the container to start, then run migrations:

```bash
python manage.py migrate
python manage.py createsuperuser
```

## Dockerfile for Django applications

Create a minimal Dockerfile for a Django application that connects to SQL Server through the default pyodbc path. The ODBC driver is the key dependency that doesn't come with the Python base image:

```dockerfile
FROM python:3.12-slim

# Install ODBC Driver 18 for SQL Server
RUN apt-get update && \
    apt-get install -y --no-install-recommends curl gnupg2 && \
    curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | \
        gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg && \
    echo "deb [signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/debian/12/prod bookworm main" > \
        /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y --no-install-recommends msodbcsql18 unixodbc-dev && \
    apt-get purge -y curl gnupg2 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

# Collect static files
RUN python manage.py collectstatic --noinput

EXPOSE 8000
CMD ["gunicorn", "myproject.wsgi:application", "--bind", "0.0.0.0:8000"]
```

> [!IMPORTANT]  
> Don't add `apt-get autoremove -y` after the purge. It removes `libgssapi-krb5-2`, which the ODBC driver loads at run time but doesn't declare as a dependency. The build still succeeds, and every connection then fails. The pyodbc error is misleading: version 18 fails to load, mssql-django falls back to version 17, and the error names the missing version 17 rather than the version 18 that failed.

Your `requirements.txt`:

```text
django>=5.2,<6.2
mssql-django>=2.0
gunicorn>=22.0
```

If your database alias uses the mssql-python driver path with `"python_driver": "mssql_python"`, you still need unixODBC, because the backend imports pyodbc when Django loads it. You don't need the Microsoft package repository or `msodbcsql18`, so the ODBC installation block shrinks to:

```dockerfile
RUN apt-get update && \
    apt-get install -y --no-install-recommends unixodbc libkrb5-3 libgssapi-krb5-2 && \
    rm -rf /var/lib/apt/lists/*
```

Build and run:

```bash
docker build -t mydjango .
docker run -e "DB_HOST=host.docker.internal" -e "DB_NAME=<database>" \
  -e "DB_USER=<user_id>" -e "DB_PASSWORD=<password>" \
  -p 8000:8000 mydjango
```

> [!NOTE]  
> Use `host.docker.internal` on Docker Desktop (Windows and macOS) to reach a SQL Server on the host machine. On Linux, use `--network host` instead.

## Devcontainer setup

Create a `.devcontainer/devcontainer.json` for Visual Studio Code that includes SQL Server as a sidecar service:

```json
{
    "name": "Django + SQL Server",
    "image": "mcr.microsoft.com/devcontainers/python:3",
    "features": {
        "ghcr.io/devcontainers/features/docker-in-docker:2": {}
    },
    "workspaceFolder": "/workspaces/${localWorkspaceFolderBasename}",
    "postCreateCommand": "bash .devcontainer/post-create.sh",
    "forwardPorts": [1433, 8000],
    "customizations": {
        "vscode": {
            "extensions": [
                "ms-python.python",
                "ms-mssql.mssql"
            ]
        }
    }
}
```

This devcontainer installs the ODBC driver for the default pyodbc path and Python dependencies but doesn't include a SQL Server instance. Start one inside the devcontainer using `sqlcmd create mssql --accept-eula` (since Docker-in-Docker is available) or use the [Docker Compose approach](#include-sql-server-with-docker-compose) for a built-in SQL Server service. If you use the mssql-python path, replace the `msodbcsql18` install in the post-create script with `sudo apt-get install -y unixodbc libkrb5-3 libgssapi-krb5-2`.

Create `.devcontainer/post-create.sh` to install the ODBC driver for pyodbc and Python dependencies:

```bash
#!/bin/bash
set -e

# Install ODBC Driver 18
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | \
    sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
echo "deb [signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/debian/12/prod bookworm main" | \
    sudo tee /etc/apt/sources.list.d/mssql-release.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18 unixodbc-dev

pip install -r requirements.txt
```

### Include SQL Server with Docker Compose

To include SQL Server as a service in the devcontainer, use Docker Compose:

`.devcontainer/docker-compose.yml`:

```yaml
services:
  app:
    image: mcr.microsoft.com/devcontainers/python:3
    volumes:
      - ..:/workspace:cached
    command: sleep infinity
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "<strong_password>"
    ports:
      - "1433:1433"
```

`.devcontainer/devcontainer.json` (Compose version):

```json
{
    "name": "Django + SQL Server",
    "dockerComposeFile": "docker-compose.yml",
    "service": "app",
    "workspaceFolder": "/workspace",
    "postCreateCommand": "bash .devcontainer/post-create.sh",
    "customizations": {
        "vscode": {
            "extensions": [
                "ms-python.python",
                "ms-mssql.mssql"
            ]
        }
    }
}
```

Connect Django to the SQL Server service by name:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "mydb",
        "USER": "sa",
        "PASSWORD": "<password>",
        "HOST": "db",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "TrustServerCertificate=yes",
        },
    },
}
```

## Authentication for development

Choose an authentication approach based on where your application runs and where the database is hosted.

### Local development against Azure SQL

For local development against Azure SQL, use either `Authentication=ActiveDirectoryDefault` in `OPTIONS["extra_params"]` on the pyodbc path, or the `TOKEN` setting with `DefaultAzureCredential`. `DefaultAzureCredential` automatically picks up your `az login` session:

```python
from azure.identity import DefaultAzureCredential

credential = DefaultAzureCredential()
token = credential.get_token("https://database.windows.net/.default").token

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "mydb",
        "HOST": "<server>.database.windows.net",
        "PORT": "1433",
        "TOKEN": token,
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

For the full authentication matrix and cautions, see [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md).

### Container development against Azure SQL

For containers running in Azure, use the `TOKEN` setting with `ManagedIdentityCredential` to acquire a Microsoft Entra access token explicitly:

```python
from azure.identity import ManagedIdentityCredential

credential = ManagedIdentityCredential()
token = credential.get_token("https://database.windows.net/.default").token

DATABASES = {
  "default": {
    "ENGINE": "mssql",
    "NAME": "mydb",
    "HOST": "<server>.database.windows.net",
    "PORT": "1433",
    "TOKEN": token,
    "OPTIONS": {
      "driver": "ODBC Driver 18 for SQL Server",
    },
  },
}
```

For a complete list of authentication methods, see [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md).

## CI pipeline setup

Run your Django test suite against a SQL Server service container in your CI pipeline.

### GitHub Actions

```yaml
name: Django Tests
on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      sqlserver:
        image: mcr.microsoft.com/mssql/server:2022-latest
        env:
          ACCEPT_EULA: Y
          MSSQL_SA_PASSWORD: "<strong_password>"
        ports:
          - 1433:1433
        options: >-
          --health-cmd "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P \"$$MSSQL_SA_PASSWORD\" -C -Q 'SELECT 1'"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
      - uses: actions/checkout@v4

      - uses: actions/setup-python@v5
        with:
          python-version: "3.12"

      - name: Install ODBC Driver for pyodbc
        run: |
          curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | \
              sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
          echo "deb [signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/ubuntu/$(lsb_release -rs)/prod $(lsb_release -cs) main" | \
              sudo tee /etc/apt/sources.list.d/mssql-release.list
          sudo apt-get update
          sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18 unixodbc-dev

      - name: Install dependencies
        run: pip install -r requirements.txt

      - name: Run tests
        env:
          DB_HOST: localhost
          DB_NAME: "master"
          DB_USER: "<user_id>"
          DB_PASSWORD: "<password>"
        run: python manage.py test
```

> [!TIP]  
> For shared pipelines, replace the inline placeholder password with an encrypted secret (`${{ secrets.SQL_PWD }}`) and pin the SQL Server service image to a digest.

### Azure Pipelines

```yaml
trigger:
  - main

resources:
  containers:
    - container: sqlserver
      image: mcr.microsoft.com/mssql/server:2022-latest
      env:
        ACCEPT_EULA: Y
        MSSQL_SA_PASSWORD: "<strong_password>"
      ports:
        - 1433:1433

pool:
  vmImage: ubuntu-latest

services:
  sqlserver: sqlserver

steps:
  - task: UsePythonVersion@0
    inputs:
      versionSpec: "3.12"

  - script: |
      curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | \
          sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
      echo "deb [signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/ubuntu/$(lsb_release -rs)/prod $(lsb_release -cs) main" | \
          sudo tee /etc/apt/sources.list.d/mssql-release.list
      sudo apt-get update
      sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18 unixodbc-dev
      pip install -r requirements.txt
    displayName: Install dependencies

  - script: python manage.py test
    displayName: Run tests
    env:
      DB_HOST: "localhost"
      DB_NAME: "master"
      DB_USER: "<user_id>"
      DB_PASSWORD: "<password>"
```

## Environment-based settings.py

Configure `settings.py` to read database credentials from environment variables. This single configuration works across local development, Docker, and CI:

```python
import os

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": os.environ.get("DB_NAME", "mydb"),
        "USER": os.environ.get("DB_USER", ""),
        "PASSWORD": os.environ.get("DB_PASSWORD", ""),
        "HOST": os.environ.get("DB_HOST", "localhost"),
        "PORT": os.environ.get("DB_PORT", "1433"),
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": os.environ.get("DB_EXTRA_PARAMS", "TrustServerCertificate=yes"),
        },
    },
}
```

Store credentials in a `.env` file for local development (add `.env` to `.gitignore`):

```text
DB_HOST=localhost
DB_NAME=mydb
DB_USER=<user_id>
DB_PASSWORD=<password>
```

Load environment variables with `django-environ` or `python-dotenv`:

```bash
pip install django-environ
```

```python
import environ

env = environ.Env()
environ.Env.read_env()  # Reads .env from the directory holding this settings file

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": env("DB_NAME"),
        "USER": env("DB_USER", default=""),
        "PASSWORD": env("DB_PASSWORD", default=""),
        "HOST": env("DB_HOST", default="localhost"),
        "PORT": env("DB_PORT", default="1433"),
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": env("DB_EXTRA_PARAMS", default="TrustServerCertificate=yes"),
        },
    },
}
```

> [!CAUTION]  
> Never commit `.env` files to source control. Add `.env` to your `.gitignore` file.

## Troubleshoot common container issues

<!-- Inline TrustServerCertificate guidance kept inside the table cell; [!INCLUDE] directives don't render inside table cells in DocFx. See includes/trust-server-certificate-caution.md for the shared callout used elsewhere. -->

| Symptom | Cause | Fix |
| --- | --- | --- |
| `Can't open lib 'ODBC Driver 18 for SQL Server'` | ODBC driver not installed in the container for the pyodbc path, or `apt-get autoremove` stripped `libgssapi-krb5-2` after the install. | Install `msodbcsql18` in your Dockerfile or post-create script, and don't run `apt-get autoremove` afterward. |
| `Can't open lib 'ODBC Driver 17 for SQL Server'` when you installed version 18 | Version 18 is registered but fails to load, so mssql-django falls back to version 17, which isn't installed. The usual cause is a missing `libgssapi-krb5-2`. | Install `libgssapi-krb5-2`, and don't run `apt-get autoremove` after purging `curl`. |
| `Error loading pyodbc module: libodbc.so.2` | The container has no unixODBC runtime. The backend imports pyodbc when Django loads it, even on the mssql-python path. | Install `unixodbc` (or `unixodbc-dev`). |
| `DDBC Error: Failed to load the driver` | The mssql-python driver can't load its own dependencies. | Install `libkrb5-3` and `libgssapi-krb5-2`. |
| Connection refused on port 1433 | SQL Server container not ready. | Add a health check or wait for the service to start. |
| `Login failed for user '<user_id>'` | Credentials are incorrect or password doesn't meet complexity requirements. On the mssql-python path, a database that doesn't exist raises this same message. | Use the correct SQL login for your container, and ensure the password meets complexity requirements. If the login is correct, confirm the database in `NAME` exists. |
| `Cannot open database` | Database doesn't exist yet. The pyodbc path reports this case; the mssql-python path reports `Login failed` instead. | Create the database before running `migrate`, or use `master` for initial setup. |
| Slow first connection in container | DNS resolution or credential chain startup. | For local SQL Server, use `localhost` instead of a hostname. |
| `SSL Provider: [error:0A000086]` | TLS certificate validation failure with self-signed cert. | Add `TrustServerCertificate=yes` to `extra_params` for development only. |

## Related content

- [Install mssql-django](installation.md)
- [Connection options for mssql-django](connection-options.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Deploy a Django app with SQL Server to Azure App Service](deploy-azure-app-service.md)
- [Test Django apps with SQL Server](testing.md)
