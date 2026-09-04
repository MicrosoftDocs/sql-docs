---
title: Connection Strings for mssql-python
description: Reference for mssql-python connection string keywords, syntax, and examples for connecting to SQL Server and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
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

These keywords cover Always On availability groups, Azure SQL targets, and idle connection resiliency. Set `ApplicationIntent=ReadOnly` to route read-heavy workloads (reports, analytics) to secondary replicas, reducing load on the primary. Set `MultiSubnetFailover=yes` when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance. When the server name resolves to more than one IP address, the driver connects to all of those addresses at the same time and uses the first one that answers. Without it, the driver tries the addresses one at a time. An address that doesn't answer stalls until the operating system's TCP connect timeout expires, which can exhaust the login timeout before the driver reaches an address that answers. When DNS resolves to a single address, the driver makes a single connection attempt, so the setting is safe to leave on.

`MultiSubnetFailover=yes` has the following limits. You can't use it over a protocol other than TCP, connecting to a SQL Server instance configured with more than 64 IP addresses fails, and you can't use it with database mirroring. Database mirroring is deprecated in all supported versions of SQL Server. Use Always On availability groups instead.

| Keyword | Aliases | Default | Description |
| --------- | --------- | --------- | ------------- |
| `MultiSubnetFailover` | `multisubnetfailover` | `no` | Connect to all resolved addresses at the same time and use the first connection that succeeds. |
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

If the target is [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use at least `60`. An auto-paused database resumes on the first connection attempt, and a shorter timeout expires before the resume completes. The attempt can also fail with error 40613 while the database resumes, so the application must retry. For more information, see [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume).

## Autocommit mode

By default, `autocommit` is `False`, which requires explicit `commit()` calls. Enable autocommit for DDL statements or read-only queries that don't need transaction control:

```python
# Via parameter
conn = mssql_python.connect(connection_string, autocommit=True)

# Or after connection
conn.setautocommit(True)
```

## Credential objects

Instead of naming an authentication mode in the connection string, you can hand the driver a credential object with the `token_provider` parameter. This parameter accepts any object with a `get_token(scope)` method, including every credential in the `azure-identity` package:

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

Don't combine `token_provider` with the `Authentication` keyword in the same connection. The driver raises `InterfaceError` when both are present. For more information, see [Microsoft Entra authentication](entra-authentication.md).

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

### Keywords from other drivers

Validation runs before the driver opens a connection, so a keyword that other SQL Server drivers accept fails immediately here. Connection strings ported from ADO.NET, ODBC, or pyodbc usually need these substitutions:

| Keyword in other drivers | mssql-python equivalent |
| --- | --- |
| `Data Source` | `Server`, or its `addr` and `address` aliases |
| `Initial Catalog` | `Database` |
| `User ID` | `UID` |
| `Password` | `PWD` |
| `Connection Timeout`, `Connect Timeout`, `Timeout`, `Login Timeout` | The `timeout` parameter of `connect()`. For more information, see [Connection timeout](#connection-timeout). |
| `Application Name` | None. The driver sets this value and reports `Application Name` as an unknown keyword. |
| `APP` | None. The driver sets this value and reports `APP` as a reserved keyword. For more information, see [Reserved keywords](#reserved-keywords). |
| `Pooling`, `Max Pool Size` | None. Configure pooling in code. For more information, see [Connection pooling](connection-pooling.md). |
| `Workstation ID`, `WSID` | None. Remove the keyword from the connection string. |
| `MultipleActiveResultSets`, `MARS_Connection` | None. Remove the keyword. To run queries concurrently, use separate connections. For more information, see [Multiple cursors](cursor-management.md#multiple-cursors). |

For `APP` and `Driver`, the driver reports a reserved keyword error rather than an unknown keyword error, because it controls both values.

## Related content

- [Build connection strings programmatically](build-connection-strings.md)
- [Install mssql-python](installation.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Manage connections with mssql-python](connection-management.md)
