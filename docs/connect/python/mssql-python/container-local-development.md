---
title: Container and Local Development with mssql-python
description: Set up local development environments, Docker containers, and CI pipelines for Python applications that connect to Microsoft SQL with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Container and local development with mssql-python

This guide covers environment setup for Python developers working with the `mssql-python` driver across Windows, Linux, macOS, Docker containers, devcontainers, and CI pipelines.

## Prerequisites

- Python 3.10 or later.
- Docker Desktop (for container-based development).
- An x64-compatible host (Intel, AMD, or x64 VM) for SQL Server Linux containers. SQL Server Linux containers don't support ARM64 hosts.

## Local SQL Server with sqlcmd (recommended)

The [go-sqlcmd](../../../tools/sqlcmd/sqlcmd-utility.md) utility can create a SQL Server container in a single command. It handles the Docker image pull, password generation, port assignment, and connection context automatically:

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

Create your application credentials, and then use them in your Python code:

```bash
sqlcmd query --database <database> "CREATE LOGIN <app-login> WITH PASSWORD = '<password>';"
sqlcmd query --database <database> "CREATE USER <app-login> FOR LOGIN <app-login>;"
sqlcmd query --database <database> "ALTER ROLE db_datareader ADD MEMBER <app-login>;"
sqlcmd query --database <database> "ALTER ROLE db_datawriter ADD MEMBER <app-login>;"
```

Replace `<database>`, `<app-login>`, and `<password>` with values from your environment.

Connect from Python using the connection details that `sqlcmd` printed at creation. Use `sqlcmd config view` to retrieve them later:

```python
import mssql_python

conn = mssql_python.connect(
    server="localhost,1433",
    uid="<app login>",
    pwd="<password>",
    encrypt="yes",
    trust_server_certificate="yes"
)

cursor = conn.cursor()
cursor.execute("SELECT @@VERSION")
print(cursor.fetchval())
conn.close()
```

When you're done, stop or delete the container:

```bash
sqlcmd stop
sqlcmd delete
```

> [!TIP]
> Run `sqlcmd create mssql --user-database <database>` to create a container with an empty user database ready for development.

## Local SQL Server from VS Code

The [MSSQL extension for Visual Studio Code](../../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) (ms-mssql.mssql) can create local SQL Server containers directly from the editor:

1. Open the **SQL Server** view in the Activity Bar.
1. Select **Add Connection** > **Create Local SQL Server** (or use the Command Palette: **MS SQL: Create Local SQL Server**).
1. Choose the SQL Server version and accept the EULA.
1. The extension pulls the container image, generates a password, and adds a connection profile automatically.

Once the container is running, you can browse databases, run queries, and manage objects right in VS Code before switching to Python code.

## Local SQL Server with Docker

If you prefer to manage containers directly, the official SQL Server container image works with two environment variables:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStr0ngP@ssword" \
  -p 1433:1433 --name sql1 \
  -d mcr.microsoft.com/mssql/server:2025-latest
```

Wait a few seconds, then connect from Python:

```python
import mssql_python

conn = mssql_python.connect(
    server="localhost,1433",
    uid="<app login>",
    pwd="<password>",
    encrypt="yes",
    trust_server_certificate="yes"
)

cursor = conn.cursor()
cursor.execute("SELECT @@VERSION")
print(cursor.fetchval())
conn.close()
```

> [!IMPORTANT]
> Use `MSSQL_SA_PASSWORD` for SQL Server containers. The older `SA_PASSWORD` variable is deprecated. The password must meet SQL Server complexity requirements: at least 8 characters, with uppercase, lowercase, digits, and special characters.

To load the AdventureWorks sample database into the container:

```bash
# Download AdventureWorks backup
curl -L -o AdventureWorks2025.bak \
  "https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2025.bak"

# Copy into container
docker cp AdventureWorks2025.bak sql1:/var/opt/mssql/backup/

# Restore
docker exec sql1 /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "YourStr0ngP@ssword" -C \
  -Q "RESTORE DATABASE AdventureWorks2025 FROM DISK='/var/opt/mssql/backup/AdventureWorks2025.bak' WITH MOVE 'AdventureWorks2025' TO '/var/opt/mssql/data/AdventureWorks2025.mdf', MOVE 'AdventureWorks2025_log' TO '/var/opt/mssql/data/AdventureWorks2025_log.ldf'"
```

> [!TIP]
> The `sqlcmd create mssql --using` approach in the previous section handles the download and restore automatically.

## Dockerfile for Python applications

Keep the Python base image reference in one place so local builds, devcontainers, and CI pipelines don't drift. For local experimentation, a broad supported tag such as `python:3-slim` works well. For shared devcontainers, CI, and production, replace that tag with an approved digest-pinned image from your organization's allowlist.

Create a minimal Dockerfile for a Python application that connects to Microsoft SQL:

```dockerfile
ARG PYTHON_BASE=python:3-slim
FROM ${PYTHON_BASE}

# Install system libraries required by mssql-python on Linux
RUN apt-get update && \
    apt-get install -y --no-install-recommends libltdl7 libkrb5-3 libgssapi-krb5-2 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .
CMD ["python", "app.py"]
```

Your `requirements.txt`:

```text
mssql-python>=1.13.0
```

Build and run:

```bash
docker build -t myapp .
docker run -e SQL_SERVER=host.docker.internal,1433 myapp
```

In shared environments, pass an approved immutable base image reference with `--build-arg PYTHON_BASE=python:3-slim@sha256:<approved-digest>`.

> [!NOTE]
> Use `host.docker.internal` on Docker Desktop (Windows and macOS) to reach a SQL Server on the host machine. On Linux, use `--network host` instead.

### Alpine Linux

Alpine uses `musl` instead of `glibc`. Install the required packages:

```dockerfile
ARG PYTHON_BASE=python:3-alpine
FROM ${PYTHON_BASE}

RUN apk add --no-cache libltdl krb5-libs

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .
CMD ["python", "app.py"]
```

## Devcontainer setup

Reuse the same Dockerfile that your application builds with. This approach keeps the devcontainer aligned with your runtime image and prevents scattering Python version pins across multiple files.

Create a `.devcontainer/devcontainer.json` for VS Code:

```json
{
    "name": "Python + SQL Server",
    "build": {
        "dockerfile": "../Dockerfile",
        "context": ".."
    },
    "features": {
        "ghcr.io/devcontainers/features/docker-in-docker:2": {}
    },
    "workspaceFolder": "/workspaces/${localWorkspaceFolderBasename}",
    "postCreateCommand": "pip install --no-cache-dir -r requirements.txt",
    "forwardPorts": [1433],
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

To include SQL Server as a service in the devcontainer, use Docker Compose:

`.devcontainer/docker-compose.yml`:

```yaml
services:
  app:
    build:
      context: ..
      dockerfile: Dockerfile
    volumes:
      - ..:/workspace:cached
    command: sleep infinity
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2025-latest
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "YourStr0ngP@ssword"
    ports:
      - "1433:1433"
```

`.devcontainer/devcontainer.json` (Compose version):

```json
{
    "name": "Python + SQL Server",
    "dockerComposeFile": "docker-compose.yml",
    "service": "app",
    "workspaceFolder": "/workspace",
    "postCreateCommand": "pip install -r requirements.txt",
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

For shared workspaces, pin the SQL Server service image to an approved digest instead of relying on a floating tag. Load `MSSQL_SA_PASSWORD` from a local `.env` file or a platform secret store instead of checking it into source control.

Connect to the SQL Server service by name:

```python
conn = mssql_python.connect(
    server="db,1433",
    uid="<app login>",
    pwd="<password>",
    encrypt="yes",
    trust_server_certificate="yes"
)
```

## Platform-specific dependencies

The `mssql-python` driver bundles its native components. You don't need to install an external ODBC driver manager. However, the driver requires a small set of system libraries on Linux and macOS.

| Platform | Required packages | Install command |
| --- | --- | --- |
| Windows | None | Included in the wheel. |
| Ubuntu / Debian | `libltdl7`, `libkrb5-3`, `libgssapi-krb5-2` | `sudo apt-get install libltdl7 libkrb5-3 libgssapi-krb5-2` |
| Red Hat / CentOS / Fedora | `libtool-ltdl`, `krb5-libs` | `sudo dnf install libtool-ltdl krb5-libs` |
| Alpine | `libltdl`, `krb5-libs` | `apk add libltdl krb5-libs` |
| macOS | OpenSSL (via Homebrew) | `brew install openssl` |

For macOS, if you encounter SSL errors, set the linker flags:

```bash
export LDFLAGS="-L/opt/homebrew/opt/openssl/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl/include"
```

For complete installation instructions, see [Install mssql-python](installation.md).

## Authentication for development

### Local development against Azure SQL

Use `ActiveDirectoryDefault` for passwordless authentication. This option chains through Azure CLI, Visual Studio, environment variables, and managed identity automatically:

```python
conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)
```

Make sure you're signed in by using Azure CLI:

```bash
az login
```

### Local development against SQL Server

Use SQL authentication with a local instance.

```python
conn = mssql_python.connect(
    server="localhost,1433",
    uid="<app login>",
    pwd="<password>",
    encrypt="yes",
    trust_server_certificate="yes"
)
```

### Container development against Azure SQL

For containers running in Azure (App Service, Container Apps, AKS), use managed identity.

```python
conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryMSI",
    encrypt="yes"
)
```

For containers running locally that need to connect to Azure SQL, make sure the container has a credential source that `ActiveDirectoryDefault` can use. The most reliable options are:

- Install Azure CLI in the container and sign in there. Mount `~/.azure` from the host only if the container image already includes Azure CLI and you intend to reuse that credential cache.
- Provide service principal credentials through environment variables such as `AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, and `AZURE_CLIENT_SECRET`.

Then use `ActiveDirectoryDefault` in your connection code.

### Supported Microsoft SQL endpoints

The `mssql-python` driver connects to all Microsoft SQL endpoints:

| Endpoint | Authentication |
| --- | --- |
| SQL Server (on-premises or in a VM) | SQL auth, Windows auth |
| Azure SQL Database | Microsoft Entra ID (recommended), SQL auth |
| Azure SQL Managed Instance | Microsoft Entra ID (recommended), SQL auth |
| Azure Synapse Analytics (dedicated pools) | Microsoft Entra ID, SQL auth |
| SQL database in Fabric | Microsoft Entra ID |
| Fabric Data Warehouse | Microsoft Entra ID |
| SQL analytics endpoint (Lakehouse) | Microsoft Entra ID |
| SQL analytics endpoint (mirrored database) | Microsoft Entra ID |

See [Microsoft Entra authentication](entra-authentication.md) for all seven authentication modes and [Support lifecycle](support-lifecycle.md) for the complete compatibility matrix.

## CI pipeline setup

### GitHub Actions

Keep the Python runtime in one variable so you can review and update it in one place. Use `3.x` for fast-moving validation pipelines, or replace it with an organization-approved exact version for release pipelines.

```yaml
name: Test with SQL Server
on: [push, pull_request]

env:
  PYTHON_VERSION: "3.x"

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      sqlserver:
        image: mcr.microsoft.com/mssql/server:2025-latest
        env:
          ACCEPT_EULA: Y
          MSSQL_SA_PASSWORD: YourStr0ngP@ssword
        ports:
          - 1433:1433
        options: >-
          --health-cmd "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P YourStr0ngP@ssword -C -Q 'SELECT 1'"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
      - uses: actions/checkout@v4

      - uses: actions/setup-python@v5
        with:
          python-version: ${{ env.PYTHON_VERSION }}
          check-latest: true

      - name: Install dependencies
        run: |
          sudo apt-get update
          sudo apt-get install -y libltdl7 libkrb5-3 libgssapi-krb5-2
          pip install -r requirements.txt

      - name: Run tests
        env:
          SQL_SERVER: localhost,1433
          SQL_UID: sa
          SQL_PWD: YourStr0ngP@ssword
        run: pytest
```

For shared pipelines, replace the inline placeholder password with an encrypted secret, pin the SQL Server service image to a digest, and keep the Python version in an organization-managed variable or reusable workflow input.

### Azure Pipelines

Use a container resource to run SQL Server as a service alongside your test job:

```yaml
trigger:
  - main

variables:
  python.version: "3.x"

resources:
  containers:
    - container: sqlserver
      image: mcr.microsoft.com/mssql/server:2025-latest
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
      versionSpec: "$(python.version)"

  - script: |
      sudo apt-get update
      sudo apt-get install -y libltdl7 libkrb5-3 libgssapi-krb5-2
      pip install -r requirements.txt
    displayName: Install dependencies

  - script: pytest
    displayName: Run tests
    env:
      SQL_SERVER: localhost,1433
      SQL_UID: sa
      SQL_PWD: YourStr0ngP@ssword
```

As with GitHub Actions, replace the inline placeholder password with a secret variable before you use this pattern outside a disposable demo pipeline.

## Security and secrets

Don't hardcode database passwords or connection strings in source code or Dockerfiles. Use environment variables and secrets management instead.

### Environment variables for local development

Store credentials in environment variables or a `.env` file that is excluded from source control:

```bash
# .env (add to .gitignore)
SQL_SERVER=localhost,1433
SQL_UID=sa
SQL_PWD=YourStr0ngP@ssword
```

```python
import os
import mssql_python

conn = mssql_python.connect(
    server=os.environ["SQL_SERVER"],
    uid=os.environ["SQL_UID"],
    pwd=os.environ["SQL_PWD"],
    encrypt="yes",
    trust_server_certificate="yes"
)
```

For Docker Compose, reference a `.env` file:

```yaml
services:
  app:
    build: .
    env_file: .env
```

> [!CAUTION]
> Never commit `.env` files to source control. Add `.env` to your `.gitignore` file.

### CI/CD secrets

In CI pipelines, use the platform's secret store instead of plaintext environment variables:

- **GitHub Actions:** Use [encrypted secrets](https://docs.github.com/actions/security-for-github-actions/security-guides/using-secrets-in-github-actions) and reference them as `${{ secrets.SQL_PWD }}`.
- **Azure Pipelines:** Use [secret variables](/azure/devops/pipelines/process/set-secret-variables) and reference them as `$(SQL_PWD)`.

### Container supply-chain hygiene

Use these practices for shared developer environments and CI:

- Keep image references in one place, such as a Docker `ARG`, a devcontainer build, or a pipeline variable.
- Pin shared container images to immutable digests instead of floating tags.
- Review and refresh pinned digests through an approved update process such as Dependabot, Renovate, or an internal image promotion workflow.
- Commit a dependency lock file such as `uv.lock`, or use hashed requirements files for reproducible Python installs.
- Prefer organization-approved base images and internal registry mirrors when your platform provides them.

### Production: passwordless authentication

For production workloads against Azure SQL, use [Microsoft Entra authentication](entra-authentication.md) with managed identity. This approach eliminates passwords entirely:

```python
conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryMSI",
    encrypt="yes"
)
```

For applications that need to store secrets such as SQL auth passwords, use [Azure Key Vault](/azure/key-vault/general/overview) and retrieve them at runtime.

## Dependency management with uv

[uv](https://docs.astral.sh/uv/) is a fast Python package installer that works well in CI and container builds:

```dockerfile
ARG PYTHON_BASE=python:3-slim
FROM ${PYTHON_BASE}

RUN apt-get update && \
    apt-get install -y --no-install-recommends libltdl7 libkrb5-3 libgssapi-krb5-2 && \
    rm -rf /var/lib/apt/lists/*

# Install uv. In shared builds, pin the source image to an approved digest.
COPY --from=ghcr.io/astral-sh/uv:latest /uv /usr/local/bin/uv

WORKDIR /app
COPY pyproject.toml uv.lock ./
RUN uv sync --frozen --no-dev

COPY . .
CMD ["uv", "run", "python", "app.py"]
```

In CI:

```bash
pip install uv
uv sync
uv run pytest
```

## Troubleshoot common container issues

| Symptom | Cause | Fix |
| --- | --- | --- |
| `ImportError: libltdl.so.7` | Missing system library. | Install `libltdl7` (Debian) or `libltdl` (Alpine). |
| `ImportError: libkrb5.so.3` | Missing Kerberos library. | Install `libkrb5-3` (Debian) or `krb5-libs` (Alpine/RHEL). |
| `SSL: CERTIFICATE_VERIFY_FAILED` | Self-signed cert on local SQL Server. | Add `trust_server_certificate="yes"` to the connection. Don't use this in production. |
| Connection refused on port 1433 | SQL Server container not ready. | Add a health check or wait for the service to start. |
| `Login failed for user 'sa'` | Password doesn't meet complexity requirements. | Use a password with uppercase, lowercase, digits, and special characters. |
| `Cannot open database` | Database doesn't exist yet. | Create or restore the database before connecting. |
| Slow first connection in container | DNS resolution or credential chain startup. | For local SQL Server, use `localhost,1433` instead of hostname. For Azure SQL, pre-authenticate with `az login`. |

## Related content

- [Install mssql-python](installation.md)
- [Support lifecycle for mssql-python](support-lifecycle.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Quickstart: Repeatable deployments with the mssql-python driver for Python](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md)
