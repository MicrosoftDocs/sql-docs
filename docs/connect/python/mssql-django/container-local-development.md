---
title: Container and Local Development with mssql-django
description: Set up local development environments, Docker containers, devcontainers, and CI pipelines for Django applications that use the mssql-django backend with SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Container and local development with mssql-django

This guide covers environment setup for Django developers working with the `mssql-django` backend across Windows, Linux, macOS, Docker containers, devcontainers, and CI pipelines.

## Prerequisites

- Python 3.8 and later versions (Django 6.0 and later versions require at least Python 3.12)
- Docker Desktop (for container-based development)
- Microsoft ODBC Driver 17 or 18 for SQL Server. See [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

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
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStr0ngP@ssword" \
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

Create a minimal Dockerfile for a Django application that connects to SQL Server. The ODBC driver is the key dependency that doesn't come with the Python base image:

```dockerfile
FROM python:3-slim

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
    apt-get autoremove -y && \
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

Your `requirements.txt`:

```text
django>=5.2
mssql-django>=1.5
gunicorn>=22.0
```

Build and run:

```bash
docker build -t mydjango .
docker run -e DB_HOST=host.docker.internal -e DB_NAME=mydb \
  -e DB_USER=<your-username> -e DB_PASSWORD=<your-password> \
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

This devcontainer installs the ODBC driver and Python dependencies but doesn't include a SQL Server instance. Start one inside the devcontainer using `sqlcmd create mssql --accept-eula` (since Docker-in-Docker is available) or use the [Docker Compose approach](#include-sql-server-with-docker-compose) for a built-in SQL Server service.

Create `.devcontainer/post-create.sh` to install the ODBC driver and Python dependencies:

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
      MSSQL_SA_PASSWORD: "YourStr0ngP@ssword"
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

For local development against Azure SQL, use either `Authentication=ActiveDirectoryDefault` in `OPTIONS["extra_params"]` (with `mssql-django` 1.7.3 and later, plus a compatible Microsoft ODBC Driver) or the `TOKEN` setting with `DefaultAzureCredential`. `DefaultAzureCredential` automatically picks up your `az login` session:

```python
from azure.identity import DefaultAzureCredential

credential = DefaultAzureCredential()
token = credential.get_token("https://database.windows.net/.default").token

DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "mydb",
        "HOST": "myserver.database.windows.net",
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
    "HOST": "myserver.database.windows.net",
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
          MSSQL_SA_PASSWORD: YourStr0ngP@ssword
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
          python-version: "3.x"

      - name: Install ODBC Driver
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
          DB_NAME: master
          DB_USER: <username>
          DB_PASSWORD: <password>
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
        MSSQL_SA_PASSWORD: YourStr0ngP@ssword
      ports:
        - 1433:1433

pool:
  vmImage: ubuntu-latest

services:
  sqlserver: sqlserver

steps:
  - task: UsePythonVersion@0
    inputs:
      versionSpec: "3.x"

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
      DB_HOST: localhost
      DB_NAME: master
      DB_USER: <username>
      DB_PASSWORD: <password>
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
DB_USER=<username>
DB_PASSWORD=<password>
```

Load environment variables with `django-environ` or `python-dotenv`:

```bash
pip install django-environ
```

```python
import environ

env = environ.Env()
environ.Env.read_env()  # Reads .env file

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
| `Can't open lib 'ODBC Driver 18 for SQL Server'` | ODBC driver not installed in the container. | Install `msodbcsql18` in your Dockerfile or post-create script. |
| Connection refused on port 1433 | SQL Server container not ready. | Add a health check or wait for the service to start. |
| `Login failed for user '<username>'` | Credentials are incorrect or password doesn't meet complexity requirements. | Use the correct SQL login for your container, and ensure the password meets complexity requirements. |
| `Cannot open database` | Database doesn't exist yet. | Create the database before running `migrate`, or use `master` for initial setup. |
| Slow first connection in container | DNS resolution or credential chain startup. | For local SQL Server, use `localhost` instead of a hostname. |
| `SSL Provider: [error:0A000086]` | TLS certificate validation failure with self-signed cert. | Add `TrustServerCertificate=yes` to `extra_params` for development only. |

## Related content

- [Install mssql-django](installation.md)
- [Connection options for mssql-django](connection-options.md)
- [Microsoft Entra authentication with mssql-django](microsoft-entra-authentication.md)
- [Deploy a Django app with SQL Server to Azure App Service](deploy-azure-app-service.md)
- [Test Django apps with SQL Server](testing.md)
