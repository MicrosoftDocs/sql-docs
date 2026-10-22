---
title: SQL Server and Windows Authentication with Microsoft.Data.SqlClient
description: Choose Windows or SQL authentication, configure Kerberos prerequisites, and protect SQL credentials in Microsoft.Data.SqlClient applications.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ms.custom: sfi-ropc-nochange
ai-usage: ai-assisted
---
# SQL Server and Windows authentication

<a id="authentication-in-sql-server"></a>

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Authentication establishes which identity connects to SQL Server. Database permissions determine what that identity can do after connecting. Choose authentication for the environment where your application runs, and configure [Transport Layer Security (TLS) encryption and certificate validation](../encryption-and-certificate-validation.md) independently.

SQL Server's *server authentication mode* is distinct from the credentials your client sends:

- **Windows authentication mode** accepts Windows identities.
- **Mixed mode** accepts Windows identities and SQL Server usernames and passwords.

Setting a client connection string doesn't change the server's authentication mode. For server configuration, see [Choose an authentication mode](../../../relational-databases/security/choose-an-authentication-mode.md).

## Authentication scenarios

Prefer authentication that doesn't require an application-managed password when your environment supports it.

| Application environment | Authentication choice |
| --- | --- |
| Domain environment with an appropriate Windows identity. | `Integrated Security=true`. SQL Server can be on another computer in the trusted environment. |
| Linux or macOS application using a configured domain identity. | Integrated security with Kerberos prerequisites on the client and server. |
| Windows local development with LocalDB. | Integrated security under the local developer's identity. |
| Azure-hosted application with Microsoft Entra support at the SQL endpoint. | Managed identity or another suitable [Microsoft Entra authentication mode](azure-active-directory-authentication.md). |
| Environment that requires SQL credentials. | SQL authentication with least-privileged credentials retrieved from protected configuration or a secret store. |

An internet-facing application doesn't inherently require SQL authentication. The web user's sign-in and the application's database identity are separate decisions.

### Connect with Windows authentication

Use the identity under which the application runs:

```text
Server=tcp:<server>,1433;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

`Trusted_Connection=true` is a synonym for `Integrated Security=true`. With integrated security enabled, `User ID` and `Password` in the connection string are ignored. They don't select a different Windows user.

A service normally connects as its service identity, not as the person using its web interface. Grant database permissions to the intended service identity. If you require delegation of an end user's identity across computers, configure and review Kerberos delegation explicitly.

### Platform prerequisites

| Client | Requirements and behavior |
| --- | --- |
| .NET Framework on Windows. | Uses native Windows networking and the Security Support Provider Interface (SSPI). |
| Modern .NET on Windows. | Uses native networking by default. Windows Negotiate selects Kerberos when configured; it can fall back to NT LAN Manager (NTLM). |
| Modern .NET on Linux or macOS. | Uses managed networking and .NET `NegotiateAuthentication`. Configure the Kerberos client, realm, service principal name (SPN), and valid credentials or ticket cache. |

Integrated security isn't a substitute for configuring domain trust, Domain Name System (DNS) resolution, time synchronization, and the SQL Server SPN. On Linux, obtain a ticket with the platform's Kerberos tools, such as `kinit`, before starting the application. Don't assume a Windows NTLM fallback is available on a non-Windows client. See [Register an SPN for Kerberos connections](../../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

To inspect the authentication scheme for your current SQL Server connection, execute this Transact-SQL (T-SQL) query:

```sql
SELECT auth_scheme
FROM sys.dm_exec_connections
WHERE session_id = @@SPID;
```

Access to this diagnostic view depends on the server's permissions. A successful connection alone doesn't prove that Kerberos, rather than NTLM, was used.

For applications that must control security-context negotiation, <xref:Microsoft.Data.SqlClient.SqlConnection.SspiContextProvider> supports a custom SSPI implementation. This is an advanced extensibility point for scenarios such as custom Kerberos or explicit NTLM credentials. It isn't a connection-string authentication mode and can't be combined with `AccessToken` or `AccessTokenCallback`.

## Custom Security Support Provider Interface (SSPI) context providers

The <xref:Microsoft.Data.SqlClient.SspiContextProvider> abstract class is the base for all SSPI context providers. To create a custom provider, derive from `SspiContextProvider` and override the `GenerateContext` method. Assign an instance of your provider to the <xref:Microsoft.Data.SqlClient.SqlConnection.SspiContextProvider> property on a `SqlConnection` before opening the connection.

The `GenerateContext` method is called during the SSPI authentication handshake. It receives the incoming authentication blob from the server, a buffer writer for the outgoing response blob, and an <xref:Microsoft.Data.SqlClient.SspiAuthenticationParameters> object containing connection metadata such as:

- `Resource` - The server's Service Principal Name (SPN).
- `ServerName` - The data source name.
- `DatabaseName` - The target database, if specified.
- `UserId` - The user ID, if specified in the connection string.
- `Password` - The password, if specified in the connection string.

Important considerations:

- Set the `SspiContextProvider` property before opening the connection. Attempting to set it on an open or connecting connection throws an `InvalidOperationException`.
- The `SspiContextProvider` instance is part of the key used to identify connection pools. Avoid creating a new `SspiContextProvider` instance for every `SqlConnection` since each new provider creates a new pool. Reference the same instance of a provider for connections you want to be considered for pooling.
- A custom `SspiContextProvider` must authenticate with the same security context for the same input parameters. If the security context is different, a pooled connection with the wrong security context might be returned for a connection request.
- `SspiContextProvider` is mutually exclusive with token-based authentication. Setting it alongside `AccessToken` or `AccessTokenCallback` throws an `InvalidOperationException`, regardless of which property you set first.

### Example

`NegotiateAuthentication` requires .NET 7 or later, so this example doesn't compile on .NET Framework. For a .NET Framework provider, implement `GenerateContext` against a platform SSPI API instead.

The following example shows a custom SSPI context provider that uses <xref:System.Net.Security.NegotiateAuthentication> to perform the authentication:

```csharp
using System;
using System.Buffers;
using System.Net.Security;
using Microsoft.Data.SqlClient;

class CustomSspiContextProvider : SspiContextProvider
{
    private NegotiateAuthentication? _auth;

    protected override bool GenerateContext(
        ReadOnlySpan<byte> incomingBlob,
        IBufferWriter<byte> outgoingBlobWriter,
        SspiAuthenticationParameters authParams)
    {
        _auth ??= new NegotiateAuthentication(
            new NegotiateAuthenticationClientOptions
            {
                Package = "Negotiate",
                TargetName = authParams.Resource,
            });

        byte[]? blob = _auth.GetOutgoingBlob(
            incomingBlob, out NegotiateAuthenticationStatusCode statusCode);

        if (statusCode is not NegotiateAuthenticationStatusCode.Completed
            and not NegotiateAuthenticationStatusCode.ContinueNeeded)
        {
            return false;
        }

        if (blob is not null)
        {
            outgoingBlobWriter.Write(blob);
        }

        return true;
    }
}
```

Assign the provider to a connection before opening it:

```csharp
using var connection = new SqlConnection(
    "Server=myServer;Database=myDatabase;Integrated Security=true;Encrypt=True;");
connection.SspiContextProvider = new CustomSspiContextProvider();
connection.Open();
```

### Security considerations

Keep the following security considerations in mind when you implement a custom SSPI context provider:

- **Validate all inputs**.
  Always validate incoming blobs and authentication parameters before processing them. Malformed or unexpected data should be rejected by returning `false` from `GenerateContext`.
- **Protect credentials**.
  The `SspiAuthenticationParameters` object might contain sensitive values such as `UserId` and `Password`. Don't log, persist, or transmit these values in an unsecured manner.
- **Use established authentication libraries**.
  Whenever possible, delegate authentication logic to well-tested libraries such as <xref:System.Net.Security.NegotiateAuthentication> rather than implementing cryptographic protocols yourself.
- **Dispose of resources**.
  If your provider allocates unmanaged resources or authentication contexts, implement <xref:System.IDisposable>, and ensure proper cleanup. SqlClient doesn't dispose the `SspiContextProvider` instance. Dispose the provider when your application no longer needs it.
- **Restrict the authentication package**.
  Only use authentication packages appropriate for your environment (for example, `Negotiate` or `Kerberos`). Only pass trusted input and configuration to the authentication package.
- **Test thoroughly**.
  Verify your provider works correctly under all expected conditions, including token renewal, connection pooling, and failover scenarios.

Microsoft doesn't validate or audit custom `SspiContextProvider` implementations, nor does it guarantee the security of connections authenticated through a custom provider. Any vulnerabilities introduced by a custom implementation are your responsibility.

## Login types

A *login* grants an identity access to the server. A *database user* represents an identity inside a database. Map the intended login or group to a database user, then grant only the required permissions through database roles.

SQL Server supports Windows account logins, Windows group logins, and SQL logins. A Windows group lets administrators manage membership without creating a separate SQL Server login for every person. Certificate and asymmetric-key logins used for code signing aren't interactive connection identities.

Contained database users and Microsoft Entra principals have different provisioning requirements. See [Principals](../../../relational-databases/security/authentication-access/principals-database-engine.md) and the endpoint's authentication documentation rather than assuming every user needs a SQL login.

## Mixed mode authentication

Use SQL authentication only on an endpoint configured to accept it. Supply `User ID` and `Password`, disable integrated security, and keep certificate validation enabled. `Authentication=Sql Password` explicitly selects SQL password authentication; it doesn't select Microsoft Entra password authentication.

The following .NET console example uses `Microsoft.Data.SqlClient`. Set `SQL_SERVER`, `SQL_DATABASE`, `SQL_USER_ID`, and `SQL_PASSWORD` through your development environment or secret-injection mechanism. `SQL_SERVER` should contain a Transmission Control Protocol (TCP) endpoint such as `tcp:sql-server.contoso.com,1433`. Don't print the password or completed connection string.

```csharp
using Microsoft.Data.SqlClient;

static string Required(string name) =>
    Environment.GetEnvironmentVariable(name)
    ?? throw new InvalidOperationException($"Set {name} before running.");

var options = new SqlConnectionStringBuilder
{
    DataSource = Required("SQL_SERVER"),
    InitialCatalog = Required("SQL_DATABASE"),
    Authentication = SqlAuthenticationMethod.SqlPassword,
    UserID = Required("SQL_USER_ID"),
    Password = Required("SQL_PASSWORD"),
    Encrypt = SqlConnectionEncryptOption.Mandatory,
    TrustServerCertificate = false,
    MultiSubnetFailover = true
};

using var connection = new SqlConnection(options.ConnectionString);
await connection.OpenAsync();
using var command = new SqlCommand("SELECT DB_NAME();", connection);
Console.WriteLine(await command.ExecuteScalarAsync());
```

The query prints the connected database name. Use [SqlConnectionStringBuilder](../connection-string-builders.md) instead of concatenating credentials into a string. The builder escapes connection-string values; it doesn't decide which server, database, or permissions your application should allow.

Keep `Persist Security Info=false`, its default. Store production secrets in a protected secret store, restrict who can read them, and rotate them. Don't use an administrator or database-owner identity for routine application requests.

## Microsoft Entra and service limitations

`Integrated Security=true` authenticates a Windows identity to SQL Server. `Authentication=Active Directory Integrated` acquires a Microsoft Entra token. These aren't interchangeable.

Azure SQL Database uses SQL or Microsoft Entra authentication rather than ordinary Windows integrated authentication. SQL database in Microsoft Fabric supports Microsoft Entra identities only; SQL authentication and logins aren't supported. Use the [Microsoft Entra article](azure-active-directory-authentication.md) for token-based connections.

## Related content

- [Principals (Database Engine)](../../../relational-databases/security/authentication-access/principals-database-engine.md)
- [Security best practices](application-security-scenarios-sql-server.md)
- [LocalDB and local development](sqlclient-support-localdb.md)
