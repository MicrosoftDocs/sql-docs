---
title: Connection Options for Microsoft.Data.SqlClient
description: Reference for every Microsoft.Data.SqlClient connection string keyword, with aliases, builder properties, defaults, accepted values, and validation rules.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Connection options for Microsoft.Data.SqlClient

This article lists every connection string keyword that Microsoft.Data.SqlClient accepts. Each keyword also has a matching <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> property. For an introduction, common patterns, and parsing rules, see [Connection strings](connection-strings.md).

Keywords and aliases aren't case-sensitive. When you set a value through a builder property, the builder writes the canonical keyword in the **Keyword** column.

## Set options with SqlConnectionStringBuilder

Start from a base connection string that holds the server, database, and authentication settings. Then set workload-specific options through builder properties. The **.NET Framework** tabs use C# 7.3. To create a project that runs the samples, see [Create the project](connecting-to-data-source.md#create-the-project).

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

var builder = new SqlConnectionStringBuilder(connectionString)
{
    ApplicationName = "Orders.Worker",
    ConnectTimeout = 30,
    CommandTimeout = 60,
    MaxPoolSize = 50,
};

await using var connection = new SqlConnection(builder.ConnectionString);
await connection.OpenAsync();

await using var command = new SqlCommand("SELECT APP_NAME();", connection);
Console.WriteLine($"APP_NAME(): {await command.ExecuteScalarAsync()}");
Console.WriteLine($"Connection timeout: {connection.ConnectionTimeout}");
Console.WriteLine($"Command timeout: {command.CommandTimeout}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        var builder = new SqlConnectionStringBuilder(connectionString)
        {
            ApplicationName = "Orders.Worker",
            ConnectTimeout = 30,
            CommandTimeout = 60,
            MaxPoolSize = 50,
        };

        using (var connection = new SqlConnection(builder.ConnectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT APP_NAME();", connection))
            {
                Console.WriteLine($"APP_NAME(): {await command.ExecuteScalarAsync()}");
                Console.WriteLine($"Connection timeout: {connection.ConnectionTimeout}");
                Console.WriteLine($"Command timeout: {command.CommandTimeout}");
            }
        }
    }
}
```

---

The output is:

```output
APP_NAME(): Orders.Worker
Connection timeout: 30
Command timeout: 60
```

A new `SqlCommand` takes its default timeout from the `Command Timeout` keyword of its connection.

## Check default values

A new `SqlConnectionStringBuilder` reports the default for every option. This program prints the defaults that most applications review:

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

var defaults = new SqlConnectionStringBuilder();

Console.WriteLine($"Connect Timeout: {defaults.ConnectTimeout}");
Console.WriteLine($"Command Timeout: {defaults.CommandTimeout}");
Console.WriteLine($"Connect Retry Count: {defaults.ConnectRetryCount}");
Console.WriteLine($"Connect Retry Interval: {defaults.ConnectRetryInterval}");
Console.WriteLine($"Encrypt: {defaults.Encrypt}");
Console.WriteLine($"Trust Server Certificate: {defaults.TrustServerCertificate}");
Console.WriteLine($"Pooling: {defaults.Pooling}");
Console.WriteLine($"Min Pool Size: {defaults.MinPoolSize}");
Console.WriteLine($"Max Pool Size: {defaults.MaxPoolSize}");
Console.WriteLine($"Connection Idle Timeout: {defaults.IdleTimeout}");
Console.WriteLine($"Load Balance Timeout: {defaults.LoadBalanceTimeout}");
Console.WriteLine($"Packet Size: {defaults.PacketSize}");
Console.WriteLine($"Multi Subnet Failover: {defaults.MultiSubnetFailover}");
Console.WriteLine($"IP Address Preference: {defaults.IPAddressPreference}");
Console.WriteLine($"Pool Blocking Period: {defaults.PoolBlockingPeriod}");
Console.WriteLine($"Application Intent: {defaults.ApplicationIntent}");
Console.WriteLine($"Application Name: {defaults.ApplicationName}");
Console.WriteLine($"Type System Version: {defaults.TypeSystemVersion}");
Console.WriteLine($"Transaction Binding: {defaults.TransactionBinding}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        var defaults = new SqlConnectionStringBuilder();

        Console.WriteLine($"Connect Timeout: {defaults.ConnectTimeout}");
        Console.WriteLine($"Command Timeout: {defaults.CommandTimeout}");
        Console.WriteLine($"Connect Retry Count: {defaults.ConnectRetryCount}");
        Console.WriteLine($"Connect Retry Interval: {defaults.ConnectRetryInterval}");
        Console.WriteLine($"Encrypt: {defaults.Encrypt}");
        Console.WriteLine($"Trust Server Certificate: {defaults.TrustServerCertificate}");
        Console.WriteLine($"Pooling: {defaults.Pooling}");
        Console.WriteLine($"Min Pool Size: {defaults.MinPoolSize}");
        Console.WriteLine($"Max Pool Size: {defaults.MaxPoolSize}");
        Console.WriteLine($"Connection Idle Timeout: {defaults.IdleTimeout}");
        Console.WriteLine($"Load Balance Timeout: {defaults.LoadBalanceTimeout}");
        Console.WriteLine($"Packet Size: {defaults.PacketSize}");
        Console.WriteLine($"Multi Subnet Failover: {defaults.MultiSubnetFailover}");
        Console.WriteLine($"IP Address Preference: {defaults.IPAddressPreference}");
        Console.WriteLine($"Pool Blocking Period: {defaults.PoolBlockingPeriod}");
        Console.WriteLine($"Application Intent: {defaults.ApplicationIntent}");
        Console.WriteLine($"Application Name: {defaults.ApplicationName}");
        Console.WriteLine($"Type System Version: {defaults.TypeSystemVersion}");
        Console.WriteLine($"Transaction Binding: {defaults.TransactionBinding}");
    }
}
```

---

On .NET, the output is:

```output
Connect Timeout: 15
Command Timeout: 30
Connect Retry Count: 1
Connect Retry Interval: 10
Encrypt: True
Trust Server Certificate: False
Pooling: True
Min Pool Size: 0
Max Pool Size: 100
Connection Idle Timeout: 300
Load Balance Timeout: 0
Packet Size: 8000
Multi Subnet Failover: False
IP Address Preference: IPv4First
Pool Blocking Period: Auto
Application Intent: ReadWrite
Application Name: Core Microsoft SqlClient Data Provider
Type System Version: Latest
Transaction Binding: Implicit Unbind
```

`Encrypt: True` is the `Mandatory` option. On .NET Framework, the default application name is `Framework Microsoft SqlClient Data Provider`.

## Server and database options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Data Source` | `Server`, `Address`, `Addr`, `Network Address` | `DataSource` | Empty | The server to connect to. Accepts a host name, `host,port`, `host\instance`, `(localdb)\instance`, and a `tcp:` or `np:` protocol prefix. For the accepted forms, see [Specify the server and database](connection-strings.md#specify-the-server-and-database). |
| `Initial Catalog` | `Database` | `InitialCatalog` | Empty | The database to use after login. When it's empty, the server uses the login's default database. |
| `AttachDbFilename` | `Extended Properties`, `Initial File Name` | `AttachDBFilename` | Empty | The full path of a primary database file to attach, for SQL Server Express and LocalDB. |
| `User Instance` | None | `UserInstance` | `false` | Redirects the connection to a SQL Server Express user instance. For more information, see [SQL Server Express user instances](sql/sql-server-express-user-instances.md). |
| `Failover Partner` | `FailoverPartner` | `FailoverPartner` | Empty | The database mirroring partner to try when the primary server is unavailable. Database mirroring is deprecated. Use availability groups for new deployments. |
| `Current Language` | `Language` | `CurrentLanguage` | Empty | The SQL Server language name for the session, such as `us_english`. Controls date formats and system message language. |

## Application identity options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Application Name` | `App` | `ApplicationName` | `Core Microsoft SqlClient Data Provider` | The name that the server reports through `APP_NAME()` and in session views. Use one stable name for each deployed workload. Distinct values create distinct connection pools. |
| `Workstation ID` | `WorkstationID`, `WSID` | `WorkstationID` | The local computer name | The client computer name that the server reports through `HOST_NAME()`. |
| `Application Intent` | `ApplicationIntent` | `ApplicationIntent` | `ReadWrite` | `ReadOnly` requests read-intent routing when the target supports it. It doesn't make statements read-only. For more information, see [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md). |

To report which framework or tool opened a connection, use the <xref:Microsoft.Data.SqlClient.SqlConnection.RegisteredApplication%2A> property. It has no connection string keyword. For more information, see [Report the application identity](connecting-to-data-source.md#report-the-application-identity).

## Authentication options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Authentication` | None | `Authentication` | Not specified | The authentication method. For accepted values, see the following table. |
| `Integrated Security` | `Trusted_Connection` | `IntegratedSecurity` | `false` | `true`, `yes`, or `sspi` uses the identity of the current process. |
| `User ID` | `UID`, `User` | `UserID` | Empty | The SQL login name, or the Microsoft Entra ID user or client identifier, depending on `Authentication`. |
| `Password` | `PWD` | `Password` | Empty | The password or client secret for methods that accept one. |
| `Persist Security Info` | `PersistSecurityInfo` | `PersistSecurityInfo` | `false` | When `false`, the driver removes the password from `SqlConnection.ConnectionString` after the connection opens. Leave it `false`. |
| `Server SPN` | `ServerSPN` | `ServerSPN` | Derived from the server name | Overrides the Service Principal Name (SPN) used for integrated authentication to the primary server. |
| `Failover Partner SPN` | `FailoverPartnerSPN` | `FailoverPartnerSPN` | Derived from the partner name | Overrides the SPN used for integrated authentication to the failover partner. |

`Authentication` accepts these values. The builder writes them without spaces, such as `ActiveDirectoryDefault`, and the parser accepts both forms.

| Value | Identity |
| --- | --- |
| `Sql Password` | A SQL login with `User ID` and `Password`. |
| `Active Directory Default` | A chain of credential sources, such as environment variables, managed identity, and developer tool sign-in. |
| `Active Directory Managed Identity` | A managed identity. Set `User ID` to the client ID of a user-assigned identity. `Active Directory MSI` is an alias. |
| `Active Directory Workload Identity` | A Kubernetes workload identity. |
| `Active Directory Service Principal` | An application with `User ID` set to the client ID and `Password` set to the client secret. |
| `Active Directory Interactive` | A user who signs in through a browser prompt. |
| `Active Directory Device Code Flow` | A user who signs in on another device with a code. |
| `Active Directory Integrated` | The signed-in Windows user, through a federated or seamless single sign-on configuration. |
| `Active Directory Password` | A user name and password. This method is obsolete. Use another method. |

Microsoft Entra ID methods need the `Microsoft.Data.SqlClient.Extensions.Azure` package. For setup and examples, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md) and [SQL Server authentication](sql/authentication-sql-server.md).

## Encryption options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Encrypt` | None | `Encrypt` | `Mandatory` | `Strict` uses TDS 8.0 and always validates the server certificate. `Mandatory`, `true`, or `yes` encrypts the connection. `Optional`, `false`, or `no` encrypts the whole connection only when the server requires it. When the server supports encryption but doesn't require it, only the login packet is encrypted. |
| `Trust Server Certificate` | `TrustServerCertificate` | `TrustServerCertificate` | `false` | `true` skips server certificate validation. Ignored when `Encrypt=Strict`. Don't use it in production. |
| `Host Name In Certificate` | `HostNameInCertificate` | `HostNameInCertificate` | Empty | A fallback name for certificate validation. SqlClient first validates the certificate against the server name in `Data Source`. It checks `Host Name In Certificate` only when that name doesn't match, so a wrong value doesn't cause a failure when the server name already matches. It doesn't pin a certificate. To require a specific certificate, use `Server Certificate`. It doesn't change the network destination or bypass trust chain and expiration checks. Leave it unset when the server name already matches the certificate. |
| `Server Certificate` | `ServerCertificate` | `ServerCertificate` | Empty | The path of a PEM, DER, or CER file that must exactly match the server certificate. The driver checks the match only when it validates the certificate, so use it with `Encrypt=Mandatory` or `Encrypt=Strict` and `Trust Server Certificate=false`. A matching file replaces chain and host name validation, so it works with a self-signed certificate that the client doesn't trust. |

For the full validation rules, see [Encryption and certificate validation](encryption-and-certificate-validation.md).

## Always Encrypted options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Column Encryption Setting` | `ColumnEncryption` | `ColumnEncryptionSetting` | `Disabled` | `Enabled` turns on Always Encrypted for the connection. |
| `Attestation Protocol` | None | `AttestationProtocol` | Not specified | The enclave attestation protocol: `AAS`, `HGS`, or `None`. |
| `Enclave Attestation Url` | None | `EnclaveAttestationUrl` | Empty | The attestation service endpoint for Always Encrypted with secure enclaves. |

## Network options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Multi Subnet Failover` | `MultiSubnetFailover` | `MultiSubnetFailover` | `false` | Tries all IP addresses that the server name resolves to in parallel. Use it for availability group listeners and failover cluster instances reached over TCP. Not supported with database mirroring. Named instances depend on the networking implementation. The managed implementation, which .NET uses on Linux and macOS and on Windows when [managed networking](appcontext-switches.md#enable-managed-networking-on-windows) is on, supports a named instance and sends the SQL Server Browser request to every resolved address in parallel. The native implementation on Windows can reject a named instance with SNI error 48. |
| `IP Address Preference` | `IPAddressPreference` | `IPAddressPreference` | `IPv4First` | The order for trying resolved addresses: `IPv4First`, `IPv6First`, or `UsePlatformDefault`. |
| `Packet Size` | `PacketSize` | `PacketSize` | `8000` | The Tabular Data Stream (TDS) packet size in bytes, from 512 through 32,768. |

A process-wide AppContext switch can change the `Multi Subnet Failover` default to `true`. For more information, see [AppContext switches in SqlClient](appcontext-switches.md).

## Timeout options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Connect Timeout` | `Connection Timeout`, `ConnectTimeout`, `Timeout` | `ConnectTimeout` | `15` | Seconds to wait for a connection, including the wait for a pooled connection when the pool is full. `0` waits indefinitely. |
| `Command Timeout` | None | `CommandTimeout` | `30` | The default <xref:Microsoft.Data.SqlClient.SqlCommand.CommandTimeout%2A> in seconds for commands created on the connection. `0` waits indefinitely. |

`Connect Timeout` doesn't limit query execution. `Command Timeout` doesn't limit login or the wait for a pooled connection. Pass a <xref:System.Threading.CancellationToken> to asynchronous calls so the caller can stop waiting before either timeout expires.

## Connection recovery options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Connect Retry Count` | `ConnectRetryCount` | `ConnectRetryCount` | `1` | Retries after a qualifying transient failure during the first open and when restoring a broken idle connection. `0` turns retries off. Valid values are 0 through 255. |
| `Connect Retry Interval` | `ConnectRetryInterval` | `ConnectRetryInterval` | `10` | Seconds between retry attempts. Valid values are 1 through 60. |

When `Connect Retry Count` is `1`, which is the default, the driver uses `2` for Azure SQL endpoints and `5` for Azure Synapse Analytics serverless SQL pool endpoints when it restores a broken idle connection. The first open uses the value as set, so the default allows one retry on every endpoint. The driver uses any other value as set in both cases.

These keywords don't retry a command that fails while it's running. To skip the built-in retry for one open, call an `Open` overload with <xref:Microsoft.Data.SqlClient.SqlConnectionOverrides.OpenWithoutRetry>. For custom policies, see [Configurable retry logic](configurable-retry-logic.md). Retry a command only when repeating it is safe.

## Pooling options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Pooling` | None | `Pooling` | `true` | Reuses physical connections for matching connection strings. |
| `Min Pool Size` | None | `MinPoolSize` | `0` | Physical connections that the pool keeps open after it's created. |
| `Max Pool Size` | None | `MaxPoolSize` | `100` | The most physical connections in one pool. Must be at least 1. |
| `Connection Idle Timeout` | None | `IdleTimeout` | `300` | Seconds that a connection can stay idle in the pool before the pool closes it. `0` turns off idle expiration. Enforced only when the `Switch.Microsoft.Data.SqlClient.UseLegacyIdleTimeoutBehavior` AppContext switch is `false`. By default, it's `true`. |
| `Load Balance Timeout` | `Connection Lifetime` | `LoadBalanceTimeout` | `0` | The maximum age in seconds of a connection returned to the pool. An older connection is closed. `0` turns off the limit. |
| `Pool Blocking Period` | `PoolBlockingPeriod` | `PoolBlockingPeriod` | `Auto` | Whether the pool rethrows a cached login failure for a short period: `Auto`, `AlwaysBlock`, or `NeverBlock`. `Auto` turns blocking off for Azure SQL endpoints. |

Each distinct connection string gets its own pool. For pool keys, clearing, and idle time, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Transaction and session options

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Enlist` | None | `Enlist` | `true` | Enlists the connection in the ambient `System.Transactions` transaction when it opens. |
| `Transaction Binding` | None | `TransactionBinding` | `Implicit Unbind` | `Implicit Unbind` detaches the connection when the enlisted transaction ends. `Explicit Unbind` keeps it attached until the connection closes or the code detaches it. |
| `Multiple Active Result Sets` | `MultipleActiveResultSets` | `MultipleActiveResultSets` | `false` | Allows more than one active batch on one connection. |
| `Type System Version` | None | `TypeSystemVersion` | `Latest` | The server type system that the driver exposes. Accepts `Latest`, `SQL Server 2012`, `SQL Server 2008`, and `SQL Server 2005`. Leave it at `Latest`. |
| `Replication` | None | `Replication` | `false` | Identifies the connection as a replication connection. |

## .NET Framework only options

These keywords are accepted only on .NET Framework. On .NET, the builder throws <xref:System.NotSupportedException> and `SqlConnection` throws <xref:System.ArgumentException>.

| Keyword | Aliases | Builder property | Default | Description |
| --- | --- | --- | --- | --- |
| `Network Library` | `Net`, `Network` | `NetworkLibrary` | Empty | The network library. Don't combine it with a `tcp:` or `np:` prefix in `Data Source`, because the connection fails. On .NET, prefix `Data Source` with `tcp:` or `np:` instead. |
| `Connection Reset` | None | `ConnectionReset` | `true` | Obsolete. |
| `Transparent Network IP Resolution` | `TransparentNetworkIPResolution` | `TransparentNetworkIPResolution` | `true` | Obsolete. |

## Unsupported keywords

| Keyword | Result |
| --- | --- |
| `Context Connection` | `Context Connection=true` throws <xref:System.InvalidOperationException>. Microsoft.Data.SqlClient doesn't support SQL CLR context connections. |
| `Asynchronous Processing` | Throws `ArgumentException: Keyword not supported`. Asynchronous methods work without it. |
| `Driver`, `Provider` | Throws `ArgumentException: Keyword not supported`. These keywords belong to ODBC and OLE DB. |

## Validation rules

`SqlConnectionStringBuilder` rejects unknown keywords and many invalid values when you set them. `SqlConnection` performs more checks when you set its connection string, including single values that the builder accepts and combinations of keywords. A string that the builder accepts can still fail when you create the connection.

The builder and the connection reject these values with <xref:System.ArgumentException>:

- `Connect Timeout`, `Command Timeout`, `Load Balance Timeout`, `Connection Idle Timeout`, or `Min Pool Size` less than 0.
- `Max Pool Size` less than 1.
- `Packet Size` outside 512 through 32,768.
- `Connect Retry Count` outside 0 through 255.
- `Connect Retry Interval` outside 1 through 60.
- A keyword that isn't in this article.

`SqlConnection` also rejects these values with `ArgumentException`, even though the builder accepts them:

- `Type System Version` other than `Latest`, `SQL Server 2012`, `SQL Server 2008`, or `SQL Server 2005`.
- `Transaction Binding` other than `Implicit Unbind` or `Explicit Unbind`.
- A value longer than the TDS limit for `Application Name`, `Current Language`, `Data Source`, `Failover Partner`, `Initial Catalog`, `Password`, `User ID`, `Workstation ID`, or `AttachDBFilename`.

`SqlConnection` also rejects these combinations with `ArgumentException`:

- `Min Pool Size` greater than `Max Pool Size`.
- `Failover Partner` without `Initial Catalog`.
- `Failover Partner` with `Multi Subnet Failover=true`, `User Instance=true`, or `Application Intent=ReadOnly`.
- `Authentication` with `Integrated Security=true`.
- `Password` with `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Managed Identity`, `Active Directory MSI`, `Active Directory Default`, or `Active Directory Workload Identity`.
- `User ID` or `Password` with `Active Directory Device Code Flow`.

## Review option changes

Before you change an option in production:

1. Record the current connection string, driver version, endpoint type, and the problem you're solving.
1. Change one option at a time.
1. Test connection establishment, authentication, certificate validation, pooling, failover, cancellation, and query execution.
1. Measure connection latency, pool waits, and error numbers.
1. Confirm the setting on every deployed instance.

A changed connection string creates a new pool. During a staged rollout, old and new pools exist at the same time, so the total number of physical connections to the server can rise.

## Related content

- [Connection strings](connection-strings.md)
- [Connection string builders](connection-string-builders.md)
- [Connect to a data source](connecting-to-data-source.md)
- [SQL Server connection pooling](sql-server-connection-pooling.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
