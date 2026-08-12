---
title: Connection Strings for mssql-python
description: Reference for mssql-python connection string keywords, syntax, and examples for connecting to SQL Server and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/31/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Connection strings for mssql-python

The mssql-python driver supports the following connection string keywords when connecting to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

## Connection string syntax

Connection strings use semicolon-separated key-value pairs:

```text
keyword1=value1;keyword2=value2;...
```

Wrap values that contain special characters (semicolons, equals signs, or curly braces) in curly braces:

```text
PWD={my;complex=password}
```

To include a literal closing brace in a value, use two closing braces (`}}`):

```text
PWD={password}}with}}brace}
```

## Basic connection examples

The following examples show how to connect using different authentication methods. For production applications, use [Microsoft Entra authentication](#microsoft-entra-authentication-modes) whenever possible. It eliminates passwords from your code and connection strings.

### SQL Server with Microsoft Entra authentication (recommended)

This example uses `ActiveDirectoryDefault`, which tries multiple credential sources (Azure CLI, environment variables, managed identity) in order. No password is stored in code:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes;"
)
```

### SQL Server with SQL authentication

Use SQL authentication only for local development against a SQL Server instance that you control. Credentials are embedded in the connection string, so keep them in environment variables or a `.env` file rather than in source code:

```python
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "UID=<login>;"
    "PWD=<password>;"
    "Encrypt=yes;"
)
```

### Azure SQL with Microsoft Entra authentication

The connection string for Azure SQL Database is the same as SQL Server. `ActiveDirectoryDefault` works across local development, containers, and Azure-hosted environments without code changes:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

## Use keyword arguments

You can pass connection parameters as keyword arguments instead of or in addition to a connection string. Keyword arguments avoid the escaping pitfalls of connection string assembly. Passwords with special characters like `@`, `;`, `{`, or `}` don't need curly-brace wrapping when passed as keyword arguments:

```python
conn = mssql_python.connect(
    server="<server>.database.windows.net",
    database="<database>",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)
```

Compare with connection string assembly, where a password containing `@` must be wrapped:

```python
# Connection string requires escaping
conn = mssql_python.connect("Server=srv;UID=user;PWD={p@ss;word};")

# Keyword arguments - no escaping needed
conn = mssql_python.connect(server="srv", uid="user", pwd="p@ss;word")
```

The driver merges keyword arguments into the connection string after normalization. If a keyword argument matches a parameter already in the connection string, the keyword argument takes precedence and overrides the connection string value:

```python
# The keyword argument database="production" overrides Database=dev in the connection string
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;Database=<database>;Encrypt=yes;",
    database="production",
    authentication="ActiveDirectoryDefault"
)
# Connects to "production", not "dev"
```

The following example combines a connection string with keyword arguments:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;Database=<database>;",
    authentication="ActiveDirectoryDefault",
    encrypt="yes"
)
```

## Connection string keywords

### Server and database

Specify the target SQL Server instance and database for the connection.

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `Server` | `addr`, `address` | None | SQL Server hostname, IP address, or named instance. For named instances, use `server\instance`. For Azure SQL, use `server.database.windows.net`. To specify a port, use `server,port`. |
| `Database` | None | None | Database name to connect to. |

### Authentication

Provide credentials for SQL authentication or specify a Microsoft Entra authentication mode. For passwordless options, see [Microsoft Entra authentication modes](#microsoft-entra-authentication-modes).

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `UID` | `uid` | None | Username for SQL authentication. |
| `PWD` | `pwd` | None | Password for SQL authentication. |
| `Trusted_Connection` | `trusted_connection` | `no` | Use Windows Integrated authentication. Set to `yes` to enable. |
| `Authentication` | `authentication` | None | Microsoft Entra authentication mode. See [Microsoft Entra authentication](#microsoft-entra-authentication-modes). |

### Encryption and security

All connections use `Encrypt=yes` by default. For most applications, the default is sufficient. Use `strict` only when your SQL Server instance supports TDS 8.0 and you require TLS 1.3. Use `TrustServerCertificate=yes` only in development environments with self-signed certificates.

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `Encrypt` | `encrypt` | `yes` | Enable TLS encryption. Values: `yes`, `no`, `strict`. Use `strict` for [TDS 8.0](/sql/relational-databases/security/networking/tds-8) with mandatory TLS 1.3. |
| `TrustServerCertificate` | `trust_server_certificate`, `trustservercertificate` | `no` | Trust self-signed server certificates without validation. Set to `yes` for development only. |
| `HostnameInCertificate` | `hostnameincertificate` | None | Expected hostname in the server's TLS certificate. |
| `ServerCertificate` | `servercertificate` | None | Path to a PEM file containing the trusted certificate authority. |
| `ServerSPN` | `serverspn` | None | [Server Service Principal Name](/sql/database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections) for Kerberos authentication. |

### High availability and failover

These keywords cover Always On availability groups, Azure SQL targets, and idle connection resiliency. Set `ApplicationIntent=ReadOnly` to route read-heavy workloads (reports, analytics) to secondary replicas, reducing load on the primary. Set `MultiSubnetFailover=yes` when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance. `MultiSubnetFailover=yes` is safe on single-IP targets: when DNS resolves to one address, the driver does a single connect attempt with no measurable overhead compared to `MultiSubnetFailover=no`, so you can leave it on for all Microsoft SQL family TCP endpoints.

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `MultiSubnetFailover` | `multisubnetfailover` | `no` | Try all resolved endpoints in parallel and complete authentication with the first responsive one. Speeds up failover recovery. |
| `ApplicationIntent` | `applicationintent` | `ReadWrite` | Declare application workload type. Use `ReadOnly` for read-only routing to secondary replicas. |
| `ConnectRetryCount` | `connectretrycount` | `1` | Number of automatic reconnection attempts for [idle connection resiliency](/sql/connect/odbc/connection-resiliency). This is a driver-level feature for dropped idle connections, not a substitute for [application-level retry logic](retry-logic.md). |
| `ConnectRetryInterval` | `connectretryinterval` | `10` | Seconds between idle connection resiliency reconnection attempts. |

### Performance and network

The defaults work for most applications. Increase `PacketSize` (up to 32767) for bulk data transfers. Configure `KeepAlive` if connections cross firewalls or load balancers that drop idle TCP sessions.

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `PacketSize` | `packet size`, `packetsize` | `4096` | Network packet size in bytes (512–32767). |
| `KeepAlive` | `keepalive` | None | TCP keep-alive interval in seconds. |
| `KeepAliveInterval` | `keepaliveinterval` | None | TCP keep-alive retry interval in seconds. |
| `IpAddressPreference` | `ipaddresspreference` | None | IP address family preference: `IPv4First`, `IPv6First`, `UsePlatformDefault`. |

### Reserved keywords

| Keyword | Description |
| --------- | ------------- |
| `Driver` | Reserved for internal use. The driver manages this value automatically. |
| `APP` | Reserved. Always set to `"MSSQL-Python"` by the driver. |

## Microsoft Entra authentication modes

The `Authentication` keyword supports the following values. Choose the mode that matches your deployment:

| Value | Description | When to use |
| ------- | ------------- | ------------- |
| `ActiveDirectoryDefault` | Uses `DefaultAzureCredential` from the Azure Identity SDK. Tries multiple authentication methods in sequence. | Local development across Azure CLI, Azure PowerShell, and Azure Developer CLI. For production, use a specific mode (`ActiveDirectoryMSI`, `ActiveDirectoryServicePrincipal`) to avoid the slow credential chain walk. |
| `ActiveDirectoryInteractive` | Browser-based interactive sign-in. On Windows, delegates to the ODBC driver natively. | Local development and tools where a user is present to authenticate in a browser. |
| `ActiveDirectoryDeviceCode` | Device code flow for headless environments. Displays a code to enter at `https://microsoft.com/devicelogin`. | SSH sessions, Docker containers, or other environments without a browser. |
| `ActiveDirectoryPassword` | **Deprecated.** Username and password authentication with Microsoft Entra ID. Requires `UID` and `PWD`. Uses the [ROPC flow](/entra/identity-platform/v2-oauth-ropc), which is incompatible with MFA. | Not recommended. Use `ActiveDirectoryMSI` or `ActiveDirectoryServicePrincipal` instead. |
| `ActiveDirectoryMSI` | Managed Service Identity for Azure-hosted applications. | Azure VMs, App Service, or Azure Functions where managed identity is configured. No credentials needed. |
| `ActiveDirectoryServicePrincipal` | Service principal authentication. Requires `UID` (client ID) and `PWD` (client secret). | CI/CD pipelines and background services that use a registered application identity. |
| `ActiveDirectoryIntegrated` | Windows Integrated authentication with Microsoft Entra ID (Kerberos). | Domain-joined Windows machines in enterprise environments with Kerberos configured. |

For reproducible Docker, devcontainer, and CI environment setup, see [Container and local development](container-local-development.md). That article centralizes Python runtime selection and shows how to use digest-pinned images in shared environments.

### Example: DefaultAzureCredential

`ActiveDirectoryDefault` maps to the Azure Identity `DefaultAzureCredential` chain. It tries the Azure CLI token first during local development, then managed identity when deployed to Azure:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

### Example: Device code flow

Use device code flow when running in environments without a browser, such as SSH sessions or Docker containers. The driver displays a URL and a code to enter on a separate device:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDeviceCode;"
    "Encrypt=yes;"
)
# Follow the prompt to authenticate at https://microsoft.com/devicelogin
```

### Example: Service principal

Service principal authentication uses a registered application identity with a client ID and secret. Use this approach for CI/CD pipelines and background services that run without user interaction:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryServicePrincipal;"
    "UID=<client-id>;"
    "PWD=<client-secret>;"
    "Encrypt=yes;"
)
```

To register the application and grant it database access, see [Microsoft Entra service principals with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal?view=azuresql&preserve-view=true). For the full setup in `mssql-python`, see [Service principal authentication](entra-authentication.md#service-principal-authentication).

## Connection timeout

Set the connection timeout using the `timeout` parameter. Use a timeout to prevent your application from hanging indefinitely when the server is unreachable:

```python
# 30-second connection timeout
conn = mssql_python.connect(connection_string, timeout=30)
```

You can also change the timeout on an existing connection:

```python
conn.timeout = 60
```

If the target is [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use at least `60`. An auto-paused database resumes on the first connect, and the resume can take 30 to 60 seconds or more. A shorter timeout expires before the resume completes and the connect attempt fails.

## Autocommit mode

By default, `autocommit` is `False`, which requires explicit `commit()` calls. Enable autocommit for DDL statements or read-only queries that don't need transaction control:

```python
# Via parameter
conn = mssql_python.connect(connection_string, autocommit=True)

# Or after connection
conn.setautocommit(True)
```

## Connection attributes

Set ODBC connection attributes before the connection is established by using `attrs_before`:

```python
import mssql_python

conn = mssql_python.connect(
    connection_string,
    attrs_before={
        mssql_python.SQL_ATTR_LOGIN_TIMEOUT: 30,
        mssql_python.SQL_ATTR_CONNECTION_TIMEOUT: 60,
    }
)
```

## Programmatic connection string building

To prevent connection string injection, don't use string concatenation or f-strings with user input. Use keyword arguments or environment variables instead. For more construction patterns including JSON/YAML config files, Azure Key Vault, and a builder class, see [Build connection strings programmatically](build-connection-strings.md).

```python
import os

conn = mssql_python.connect(
    server=os.environ["DB_SERVER"],
    database=os.environ["DB_NAME"],
    authentication=os.environ.get("DB_AUTH", "ActiveDirectoryDefault"),
    encrypt="yes"
)
```

## Connection string validation

The driver validates connection strings and raises `ConnectionStringParseError` for unknown or misspelled keywords:

```python
try:
    conn = mssql_python.connect("Servr=localhost;")  # Typo
except mssql_python.ConnectionStringParseError as e:
    print(f"Invalid connection string: {e}")
    # Output: Unknown keyword 'Servr'
```

## Related content

- [Build connection strings programmatically](build-connection-strings.md)
- [Install mssql-python](installation.md)
- [Microsoft Entra authentication](entra-authentication.md)
- [Connection pooling](connection-pooling.md)
- [Connection management](connection-management.md)
