---
title: Build Connection Strings Programmatically with mssql-python
description: Learn how to construct SQL Server connection strings dynamically in Python applications using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Build connection strings programmatically

Many applications need to build connection strings dynamically rather than storing them as static configuration values. Choose the approach that matches your deployment:

- **Environment variables**: Best for containers, CI/CD, and 12-factor apps. Simple and widely supported.
- **JSON/YAML config files**: Best for applications with multiple environments (dev, staging, prod) that need structured configuration.
- **Azure Key Vault**: Best for production deployments where secrets must be centrally managed and audited.
- **Builder class**: Best for libraries or frameworks that need to construct connection strings from user input with automatic escaping.

## Basic string construction

### Use f-strings

f-strings are a common approach for quick scripts and prototypes. Avoid this pattern when values come from user input, because a malicious value like `AdventureWorks;Server=evil.com` could alter the connection target:

```python
import mssql_python

server = "<server>.database.windows.net"
database = "<database>"

connection_string = f"Server={server};Database={database};Authentication=ActiveDirectoryDefault;Encrypt=yes;"

conn = mssql_python.connect(connection_string)
```

### Use join

The `join` approach separates key-value pairs into a dictionary-style function call, which is easier to read and maintain than a long f-string. It also filters out `None` values automatically, so you can pass optional parameters without extra conditional logic:

```python
def build_connection_string(**kwargs) -> str:
    """Build connection string from keyword arguments."""
    return ";".join(f"{key}={value}" for key, value in kwargs.items() if value is not None)

conn_str = build_connection_string(
    Server="<server>.database.windows.net",
    Database="<database>",
    Authentication="ActiveDirectoryDefault",
    Encrypt="yes"
)

conn = mssql_python.connect(conn_str)
```

## Connection string builder class

A builder class provides a fluent API with automatic escaping. This approach is useful in libraries or multitenant applications where connection parameters come from different sources:

```python
import mssql_python

class ConnectionStringBuilder:
    """Builder for SQL Server connection strings."""

    def __init__(self):
        self._params = {}

    def server(self, value: str) -> "ConnectionStringBuilder":
        self._params["Server"] = value
        return self

    def database(self, value: str) -> "ConnectionStringBuilder":
        self._params["Database"] = value
        return self

    def trusted_connection(self) -> "ConnectionStringBuilder":
        self._params["Trusted_Connection"] = "yes"
        return self

    def sql_auth(self, username: str, password: str) -> "ConnectionStringBuilder":
        self._params["UID"] = username
        self._params["PWD"] = password
        return self

    def entra_default(self) -> "ConnectionStringBuilder":
        self._params["Authentication"] = "ActiveDirectoryDefault"
        return self

    def entra_msi(self, client_id: str = None) -> "ConnectionStringBuilder":
        self._params["Authentication"] = "ActiveDirectoryMSI"
        if client_id:
            self._params["UID"] = client_id
        return self

    def encrypt(self, value: bool = True) -> "ConnectionStringBuilder":
        self._params["Encrypt"] = "yes" if value else "no"
        return self

    def trust_server_certificate(self, value: bool = True) -> "ConnectionStringBuilder":
        self._params["TrustServerCertificate"] = "yes" if value else "no"
        return self

    def connect_timeout(self, seconds: int) -> "ConnectionStringBuilder":
        self._timeout = seconds
        return self

    def build(self) -> str:
        """Build the connection string."""
        return ";".join(f"{k}={v}" for k, v in self._params.items())

    def connect(self) -> mssql_python.Connection:
        """Build and connect."""
        return mssql_python.connect(self.build(), timeout=getattr(self, '_timeout', 0))


# Usage examples
# Microsoft Entra authentication (recommended)
conn = (ConnectionStringBuilder()
    .server("<server>.database.windows.net")
    .database("<database>")
    .entra_default()
    .encrypt()
    .connect())

# Azure with managed identity
conn = (ConnectionStringBuilder()
    .server("<server>.database.windows.net")
    .database("<database>")
    .entra_msi()
    .encrypt()
    .connect())
```

## Environment-based configuration

### From environment variables

Reading connection parameters from environment variables keeps credentials out of source code and works across local development, containers, and CI/CD pipelines. The function checks which authentication method to use based on the variables that are set:

```python
import os
import mssql_python

def get_connection_from_env() -> mssql_python.Connection:
    """Build connection from environment variables."""
    server = os.environ.get("SQL_SERVER")
    database = os.environ.get("SQL_DATABASE")

    if not server or not database:
        raise ValueError("SQL_SERVER and SQL_DATABASE environment variables required")

    # Check for authentication method
    if os.environ.get("SQL_USE_MSI", "").lower() == "true":
        # Azure Managed Identity
        conn_str = f"Server={server};Database={database};Authentication=ActiveDirectoryMSI;Encrypt=yes;"
    elif os.environ.get("SQL_TRUSTED_CONNECTION", "").lower() == "true":
        # Windows authentication
        conn_str = f"Server={server};Database={database};Trusted_Connection=yes;Encrypt=yes;"
    else:
        # SQL authentication
        username = os.environ.get("SQL_USERNAME")
        password = os.environ.get("SQL_PASSWORD")
        if not username or not password:
            raise ValueError("SQL_USERNAME and SQL_PASSWORD required for SQL authentication")
        conn_str = f"Server={server};Database={database};UID={username};PWD={password};Encrypt=yes;"

    return mssql_python.connect(conn_str)

# Usage
conn = get_connection_from_env()
```

### With python-dotenv

The [`python-dotenv`](https://pypi.org/project/python-dotenv/) package loads key-value pairs from an `.env` file into environment variables so your code reads credentials the same way in local development and production. The `.env` file stays out of source control (add it to `.gitignore`), while deployed environments inject the same variables through their platform secret store.

Install with `pip install python-dotenv`.

Create a `.env` file in your project root with your connection parameters:

```ini
# .env - add this file to .gitignore
SQL_SERVER=<server>.database.windows.net
SQL_DATABASE=<database>
SQL_USE_MSI=true
```

Then load and use those values in your script:

```python
from dotenv import load_dotenv
import os
import mssql_python

# Load .env file into os.environ (no-op if the file doesn't exist)
load_dotenv()

server = os.getenv("SQL_SERVER")
database = os.getenv("SQL_DATABASE")

if not server or not database:
    raise ValueError("SQL_SERVER and SQL_DATABASE must be set in .env or as environment variables")

use_msi = os.getenv("SQL_USE_MSI", "false").lower() == "true"

if use_msi:
    conn_str = f"Server={server};Database={database};Authentication=ActiveDirectoryMSI;Encrypt=yes;"
else:
    conn_str = f"Server={server};Database={database};Authentication=ActiveDirectoryDefault;Encrypt=yes;"

conn = mssql_python.connect(conn_str)
```

> [!TIP]
> `load_dotenv()` doesn't overwrite variables that are already set in the environment. In production, set the same variable names through your platform (for example, App Service application settings or container environment variables) and skip the `.env` file entirely.

## File-based configuration

### From JSON config

A JSON configuration file lets you define connection settings for multiple environments (development, staging, production) in one place. The function reads the file, selects the target environment, and builds the connection string from the structured settings:

```python
import json
import io
import mssql_python

def load_connection_from_json(config_file, environment: str = "development") -> str:
    """Load connection settings from a JSON config file or file-like object."""
    config = json.load(config_file)

    env_config = config.get(environment, {})
    db_config = env_config.get("database", {})

    params = {
        "Server": db_config.get("server"),
        "Database": db_config.get("database"),
        "Encrypt": "yes" if db_config.get("encrypt", True) else "no",
    }

    auth_type = db_config.get("authentication", "sql")
    if auth_type == "msi":
        params["Authentication"] = "ActiveDirectoryMSI"
    elif auth_type == "default":
        params["Authentication"] = "ActiveDirectoryDefault"
    elif auth_type == "windows":
        params["Trusted_Connection"] = "yes"
    else:
        params["UID"] = db_config.get("username")
        params["PWD"] = db_config.get("password")

    return ";".join(f"{k}={v}" for k, v in params.items() if v)

# Example: load from an inline JSON config (in production, use open("config.json"))
sample_config = json.dumps({
    "development": {
        "database": {
            "server": "localhost",
            "database": "devdb",
            "authentication": "windows",
            "encrypt": False
        }
    },
    "production": {
        "database": {
            "server": "prod.database.windows.net",
            "database": "proddb",
            "authentication": "msi",
            "encrypt": True
        }
    }
})

conn_str = load_connection_from_json(io.StringIO(sample_config), "production")
print(f"Connection string: {conn_str}")
```

### From YAML config

YAML configuration files are a readable alternative to JSON. They're commonly used in Python projects and Kubernetes deployments. This approach reads connection settings from a structured YAML file and builds the connection string based on the authentication type defined in the config.  

Install the package by running `pip install pyyaml`.  

Create a `database.yml` file in your project:

```yaml
database:
  server: <server>.database.windows.net
  name: <database>
  authentication: msi
  encrypt: true
```

Then load and use those settings in your script:

```python
import yaml
import mssql_python

def load_from_yaml(config_path: str) -> mssql_python.Connection:
    """Load connection from YAML config."""
    with open(config_path) as f:
        config = yaml.safe_load(f)

    db = config["database"]

    parts = [
        f"Server={db['server']}",
        f"Database={db['name']}",
    ]

    if db.get("trusted_connection"):
        parts.append("Trusted_Connection=yes")
    elif db.get("authentication") == "msi":
        parts.append("Authentication=ActiveDirectoryMSI")
    else:
        parts.append(f"UID={db['username']}")
        parts.append(f"PWD={db['password']}")

    if db.get("encrypt", True):
        parts.append("Encrypt=yes")
    if db.get("trust_server_certificate"):
        parts.append("TrustServerCertificate=yes")

    return mssql_python.connect(";".join(parts))

conn = load_from_yaml("database.yml")
```

## Azure Key Vault integration

For production deployments, store connection credentials in Azure Key Vault rather than in configuration files or environment variables. Key Vault provides centralized secret management, access auditing, and automatic rotation. Install the required packages by running `pip install azure-keyvault-secrets azure-identity`. For a full walkthrough, see [Quickstart: Azure Key Vault secret client library for Python](/azure/key-vault/secrets/quick-create-python).

```python
import os
from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient
import mssql_python

def get_connection_from_keyvault(vault_url: str) -> mssql_python.Connection:
    """Build connection using secrets from Azure Key Vault."""
    credential = DefaultAzureCredential()
    client = SecretClient(vault_url=vault_url, credential=credential)

    server = client.get_secret("sql-server").value
    database = client.get_secret("sql-database").value
    username = client.get_secret("sql-username").value
    password = client.get_secret("sql-password").value

    conn_str = f"Server={server};Database={database};UID={username};PWD={password};Encrypt=yes;"
    return mssql_python.connect(conn_str)

vault_url = os.environ.get("AZURE_KEY_VAULT_URL")
if vault_url:
    conn = get_connection_from_keyvault(vault_url)
```

## Handle special characters

### Escape semicolons and braces

You need to escape connection string values that contain special characters. Wrap the value in braces `{}` and double any internal closing braces `}`:

```python
def escape_value(value: str) -> str:
    """Escape special characters in connection string values."""
    if ";" in value or "{" in value or "}" in value:
        # Wrap in braces and escape internal braces
        value = value.replace("}", "}}")
        return "{" + value + "}"
    return value

# Password with semicolon
password = "my;complex;password"
escaped_password = escape_value(password)  # {my;complex;password}

conn_str = f"Server=<server>;Database=<database>;UID=<login>;PWD={escaped_password};"
```

### Builder with automatic escape

This builder class automatically wraps every value, so callers don't need to remember escaping rules. Use it when connection parameters come from external input such as user forms, configuration APIs, or secret stores where values might contain semicolons or braces:

```python
class SafeConnectionStringBuilder:
    """Connection string builder with automatic escaping."""

    SPECIAL_CHARS = {";", "{", "}"}

    def __init__(self):
        self._params = {}

    def _escape(self, value: str) -> str:
        if any(c in value for c in self.SPECIAL_CHARS):
            value = value.replace("}", "}}")
            return "{" + value + "}"
        return value

    def set(self, key: str, value: str) -> "SafeConnectionStringBuilder":
        self._params[key] = self._escape(value)
        return self

    def build(self) -> str:
        return ";".join(f"{k}={v}" for k, v in self._params.items())

# Safely handles special characters
builder = SafeConnectionStringBuilder()
builder.set("Server", "<server>.database.windows.net")
builder.set("Database", "<database>")
builder.set("PWD", "pass;word{with}special")  # Automatically escaped

conn_str = builder.build()
```

## Validation

Before using a dynamically built connection string in your application, verify that it actually connects. This helper function attempts a lightweight query and returns a boolean result:

```python
import mssql_python

def validate_connection_string(conn_str: str) -> bool:
    """Validate a connection string by attempting to connect."""
    try:
        conn = mssql_python.connect(conn_str)
        cursor = conn.cursor()
        cursor.execute("SELECT 1")
        cursor.fetchone()
        conn.close()
        return True
    except mssql_python.Error as e:
        print(f"Connection failed: {e}")
        return False

# Test before using
conn_str = "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes;"
if validate_connection_string(conn_str):
    print("Connection string is valid")
```

## Related content

- [Connection strings for mssql-python](connection-strings.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Manage connections with mssql-python](connection-management.md)
