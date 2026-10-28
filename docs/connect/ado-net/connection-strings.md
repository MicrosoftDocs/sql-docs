---
title: Connection Strings for Microsoft.Data.SqlClient
description: Learn the syntax of Microsoft.Data.SqlClient connection strings, and choose authentication, server, encryption, and pooling settings for SQL Server, Azure SQL, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Connection strings for Microsoft.Data.SqlClient

A Microsoft.Data.SqlClient connection string is a list of `keyword=value` pairs separated by semicolons. Pass it to <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder>:

```text
Server=tcp:sql.example.com,1433;Database=Orders;Integrated Security=true;Encrypt=Strict
```

A connection string answers four questions:

1. Which server and database does the application use?
1. Which identity does the application run as?
1. How does the client validate the server certificate?
1. Which connection behavior does the workload need?

This article covers those choices and the rules that the driver uses to read the text. For every keyword, its synonyms, and its default, see [Connection options](connection-options.md). To create and change connection strings in code, see [Connection string builders](connection-string-builders.md).

<a name="windows-authentication"></a>
<a name="windows-authentication-with-sqlclient"></a>
<a name="sql-server-authentication-with-sqlclient"></a>

## Choose an authentication pattern

Use the pattern that fits the deployment. Replace each placeholder, such as `<server>`, with your value.

| Environment | Pattern | Connection string |
| --- | --- | --- |
| SQL Server on Windows with a domain or local Windows identity | Windows integrated authentication | `Server=<server>;Database=<database>;Integrated Security=true;Encrypt=true` |
| Application hosted in Azure and connecting to Azure SQL | Microsoft Entra ID managed identity | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Managed Identity;Encrypt=Strict` |
| Developer workstation connecting to Azure SQL | Microsoft Entra ID default credential chain | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict` |
| Developer workstation connecting to SQL database in Microsoft Fabric | Microsoft Entra ID default credential chain | `Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict` |
| Interactive desktop tool connecting to Azure SQL | Microsoft Entra ID interactive authentication | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Interactive;Encrypt=Strict` |
| Environment that requires SQL authentication | User name and password from a secret store | `Server=<server>;Database=<database>;User ID=<user>;Password=<password>;Encrypt=true` |

Windows integrated authentication connects as the identity of the process. For a desktop or console application, that's usually the signed-in user. For a web application or a Windows service, it's the application pool or service account. Impersonation can change the identity. Grant a login and database permissions to the identity that your application actually uses.

These three forms of Windows integrated authentication are equivalent:

```text
Server=sql.example.com;Database=Orders;Integrated Security=true;Encrypt=true
Server=sql.example.com;Database=Orders;Integrated Security=SSPI;Encrypt=true
Server=sql.example.com;Database=Orders;Trusted_Connection=yes;Encrypt=true
```

If a string sets `Integrated Security=true` and also sets `User ID` and `Password`, the driver uses Windows authentication and ignores the user name and password.

Microsoft.Data.SqlClient 7.0 and later versions need the version-matched `Microsoft.Data.SqlClient.Extensions.Azure` package for the driver-provided Microsoft Entra ID authentication methods. You don't need that package when application code supplies an access token or an access token callback.

For the accepted `Authentication` values, see [Authentication options](connection-options.md#authentication-options). Authentication also requires database users, permissions, and identity configuration on the server. For the setup steps, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md) and [SQL Server authentication](sql/authentication-sql-server.md).

<a name="connect-to-a-named-instance-of-sql-server"></a>

## Specify the server and database

`Server` and `Database` are the most readable keyword names. The builder writes them as their canonical equivalents, `Data Source` and `Initial Catalog`. All four names work in a connection string.

Set `Server` to one of these forms:

| Form | Example | Use |
| --- | --- | --- |
| Host name | `Server=sql.example.com` | The default instance on port 1433. |
| Host name and port | `Server=tcp:sql.example.com,1433` | A specific TCP port. Separate the port with a comma, not a colon. |
| Named instance | `Server=sql.example.com\SALES` | A named instance. The driver asks the SQL Server Browser service on UDP port 1434 for the port. |
| Local server | `Server=(local)` or `Server=.` | The default instance on the local computer. |
| LocalDB | `Server=(localdb)\MSSQLLocalDB` | SQL Server Express LocalDB on Windows. The connection isn't encrypted, even with `Encrypt=Mandatory`, and `Encrypt=Strict` fails. |
| Named pipe | `Server=np:\\sql.example.com\pipe\sql\query` | A named pipe connection on Windows. |

Use `tcp:`, a fully qualified host name, and an explicit port in production. Use a DNS name that matches the server certificate, not an IP address, because certificate validation compares the server name in the connection string with the names in the certificate.

When you connect to Azure SQL Database with SQL authentication and a user name in the form `user@servername`, the `servername` part must match the server in `Server`.

For an availability group listener or a failover cluster instance, also review `Multi Subnet Failover` in [Connection options](connection-options.md#network-options).

<a name="use-trustservercertificate"></a>
<a name="hostnameincertificate"></a>
<a name="servercertificate"></a>
<a name="enable-encryption"></a>

## Configure encryption and certificate validation

The `Encrypt` keyword accepts the following values, in any case:

| Value | Builder value | Meaning |
| --- | --- | --- |
| `Strict` | `Strict` | Uses TDS 8.0. Encrypts the connection before login and always validates the server certificate. |
| `Mandatory`, `true`, `yes` | `True` | Encrypts the connection. This value is the default. |
| `Optional`, `false`, `no` | `False` | Encrypts the whole connection only when the server requires encryption. When the server supports encryption but doesn't require it, encrypts only the login packet. |

Use `Strict` when the server supports TDS 8.0 and has a certificate that the client can validate. Azure SQL Database and SQL database in Microsoft Fabric support it. A LocalDB connection isn't encrypted, even with `Mandatory`, and `Strict` fails.

The `Encrypt` and `Trust Server Certificate` values combine as follows:

| Encrypt | Trust Server Certificate | Result |
| --- | --- | --- |
| `Optional` | `false` | When the server requires encryption, the whole connection is encrypted, and the server certificate must validate. Otherwise, the connection fails. When the server supports encryption but doesn't require it, only the login packet is encrypted, and the driver doesn't validate the server certificate unless the connection uses `AccessToken` or `AccessTokenCallback`. When the server doesn't support encryption, nothing is encrypted. |
| `Optional` | `true` | When the server requires encryption, the whole connection is encrypted. When the server supports encryption but doesn't require it, only the login packet is encrypted. The driver doesn't validate the server certificate. |
| `Mandatory` | `false` | Encryption occurs only when the server certificate validates. Otherwise, the connection fails. |
| `Mandatory` | `true` | Encryption always occurs, but the driver doesn't validate the server certificate. |
| `Strict` | Ignored | Encryption always occurs, and the server certificate must validate. Otherwise, the connection fails. |

Don't set `Trust Server Certificate=true` to fix a connection error in production. It encrypts the channel but doesn't confirm the server's identity. Limit it to development environments where a trusted certificate isn't available.

When the server name in `Data Source` doesn't match the certificate, set `Host Name In Certificate` to the name in the certificate. SqlClient checks it only when the server name doesn't match, and it doesn't pin a certificate. To accept only one specific certificate, set `Server Certificate` to the path of a PEM, DER, or CER file:

```text
Server=tcp:sql.example.com,1433;Encrypt=Strict;Host Name In Certificate=sqlhost.example.com
Server=tcp:sql.example.com,1433;Encrypt=Strict;Server Certificate=C:\certificates\server.cer
```

For the full validation rules, see [Encryption and certificate validation](encryption-and-certificate-validation.md).

<a name="configure-pooled-connection-idle-time"></a>

## Use connection pooling

Pooling is on by default. When you open a <xref:Microsoft.Data.SqlClient.SqlConnection>, the driver takes a physical connection from a pool that matches the connection. When you close or dispose the connection, the physical connection returns to the pool instead of closing. A pooled open skips the network connection, the TLS handshake, and the login.

Follow these rules:

- Open a connection just before you need it, and dispose it as soon as you finish, with `using` or `await using`. A connection that isn't disposed stays checked out, and the pool can run out.
- Build the connection string once at startup and reuse it. The pool key includes the exact text of the connection string, so a different keyword order, a synonym, or a per-request value such as a request ID or user name creates a separate pool. The builder writes one canonical form. For more information, see [Keep one canonical connection string](connection-string-builders.md#keep-one-canonical-connection-string).
- Expect a separate pool for each Windows identity under integrated authentication, each <xref:Microsoft.Data.SqlClient.SqlCredential> instance, and each <xref:Microsoft.Data.SqlClient.SqlConnection.AccessToken%2A> value. Use a driver-provided `Authentication` method or one reused <xref:Microsoft.Data.SqlClient.SqlConnection.AccessTokenCallback%2A> instead of setting `AccessToken` on each connection.
- Look for connections that aren't disposed before you raise `Max Pool Size`, which defaults to 100. When every connection in a pool is in use, `Open` waits up to `Connect Timeout` and then throws <xref:System.InvalidOperationException>:

  ```output
  Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.
  ```

- Plan server capacity for every pool. Each process, or each application domain on .NET Framework, has its own pools. The connections to the server can reach `Max Pool Size` multiplied by the pools in each process and by the number of application instances.
- Don't set `Pooling=false` to work around a pooling problem. Every open then performs a full login.

To close pooled connections that stay idle too long, see [Limit connection idle time](sql-server-connection-pooling.md#limit-connection-idle-time). For pool keys, sizing, clearing, and transactions, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Separate connection and command settings

A connection string controls connection establishment and session behavior. A command controls one SQL operation.

| Requirement | Configure on |
| --- | --- |
| Time allowed to establish a connection or get one from the pool | `Connect Timeout` keyword |
| Default timeout for commands on the connection | `Command Timeout` keyword |
| Timeout for one command | <xref:Microsoft.Data.SqlClient.SqlCommand.CommandTimeout%2A> |
| Cancellation from the caller | `CancellationToken` passed to asynchronous methods |
| Retry policy for opening a connection or running a command | [Configurable retry logic](configurable-retry-logic.md) on `SqlConnection` or `SqlCommand` |

A longer timeout isn't retry logic. A timeout limits one wait. A retry starts another attempt, and it must be bounded and safe to repeat.

## Report the application identity

`Application Name` sets the name that the server reports through `APP_NAME()`. Use one stable name for each deployed workload. Frameworks and tools that open connections for an application can also report themselves through the <xref:Microsoft.Data.SqlClient.SqlConnection.RegisteredApplication%2A> property, which has no connection string keyword. For more information, see [Report the application identity](connecting-to-data-source.md#report-the-application-identity).

<a name="connection-string-builders"></a>

## Build connection strings safely

Use `SqlConnectionStringBuilder` when code adds or changes a value. Don't join untrusted text into a connection string, because a semicolon in that text can add or replace a keyword. The builder validates values, maps synonyms to canonical keywords, and quotes each value so the value can't add keywords. For examples, see [Connection string builders](connection-string-builders.md).

## Store connection information outside code

Load connection strings from the application's configuration system. .NET applications commonly use environment variables, user secrets for local development, Azure App Configuration, and Azure Key Vault.

Follow these rules:

- Don't commit passwords, client secrets, access tokens, or production connection strings.
- Prefer an authentication method that doesn't need a password in the connection string.
- Restrict access to the configuration source.
- Rotate stored secrets, and restart or refresh applications that cache them.
- Don't write connection strings to logs, exceptions, traces, or telemetry.
- Leave `Persist Security Info=false`, the default, so an opened connection doesn't expose the password through its `ConnectionString` property.

For .NET configuration providers, see [Configuration in .NET](/dotnet/core/extensions/configuration). For more controls, see [Protect connection information](protecting-connection-information.md).

<a name="sqlclient-connection-strings"></a>
<a name="connection-string-aliases"></a>

## Parsing rules

The driver applies these rules:

- Keywords aren't case-sensitive. `DATABASE`, `Database`, and `database` are the same keyword.
- Many keywords have synonyms. `Server` and `Data Source` set the same value. The [Connection options](connection-options.md) article lists the synonyms for each keyword.
- When a keyword appears more than once, the last value wins. Synonyms count as the same keyword.
- The driver ignores spaces around keywords and around unquoted values.
- A trailing semicolon is optional.
- A keyword that the driver doesn't recognize throws <xref:System.ArgumentException> with the message `Keyword not supported`.

Values can be case-sensitive, depending on the keyword. A password is case-sensitive. `Encrypt=STRICT` and `Encrypt=strict` are the same.

This program shows how the builder normalizes keywords, resolves a duplicate, and quotes special characters. The **.NET Framework** tab uses C# 7.3. To create a project that runs the samples, see [Create the project](connecting-to-data-source.md#create-the-project).

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

// Synonyms and keyword case are normalized to canonical keywords.
var parsed = new SqlConnectionStringBuilder(
    "server=tcp:sql.example.com,1433;DATABASE=Orders;uid=app_user;encrypt=yes;timeout=30");
Console.WriteLine(parsed.ConnectionString);

// When a keyword repeats, the last value wins.
var duplicate = new SqlConnectionStringBuilder(
    "Server=sql.example.com;Database=Orders;Database=Archive");
Console.WriteLine($"InitialCatalog: {duplicate.InitialCatalog}");

// The builder quotes values that contain delimiters.
var quoted = new SqlConnectionStringBuilder
{
    DataSource = "sql.example.com",
    UserID = "app_user",
    Password = "a;b'c\"d",
};
Console.WriteLine(quoted.ConnectionString);
Console.WriteLine($"Round trip: {new SqlConnectionStringBuilder(quoted.ConnectionString).Password == quoted.Password}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Synonyms and keyword case are normalized to canonical keywords.
        var parsed = new SqlConnectionStringBuilder(
            "server=tcp:sql.example.com,1433;DATABASE=Orders;uid=app_user;encrypt=yes;timeout=30");
        Console.WriteLine(parsed.ConnectionString);

        // When a keyword repeats, the last value wins.
        var duplicate = new SqlConnectionStringBuilder(
            "Server=sql.example.com;Database=Orders;Database=Archive");
        Console.WriteLine($"InitialCatalog: {duplicate.InitialCatalog}");

        // The builder quotes values that contain delimiters.
        var quoted = new SqlConnectionStringBuilder
        {
            DataSource = "sql.example.com",
            UserID = "app_user",
            Password = "a;b'c\"d",
        };
        Console.WriteLine(quoted.ConnectionString);
        Console.WriteLine($"Round trip: {new SqlConnectionStringBuilder(quoted.ConnectionString).Password == quoted.Password}");
    }
}
```

---

The output is:

```output
Data Source=tcp:sql.example.com,1433;Initial Catalog=Orders;User ID=app_user;Connect Timeout=30;Encrypt=True
InitialCatalog: Archive
Data Source=sql.example.com;User ID=app_user;Password="a;b'c""d"
Round trip: True
```

The first line shows each synonym replaced by its canonical keyword: `server` by `Data Source`, `DATABASE` by `Initial Catalog`, `uid` by `User ID`, and `timeout` by `Connect Timeout`.

## Quote values

Enclose a value in double quotation marks (`"`) or single quotation marks (`'`) when it contains a semicolon, starts or ends with a space, or starts or ends with a quotation mark. Inside a quoted value, write the enclosing quotation mark twice.

| Value | Connection string form |
| --- | --- |
| `a;b` | `Password="a;b"` |
| `abc'` | `Password="abc'"` |
| `say "hi"` | `Password='say "hi"'` |
| `a;b'c"d` | `Password="a;b'c""d"` |

Quoting rules are easy to get wrong. Set values through `SqlConnectionStringBuilder` properties and let the builder quote them. For more information, see [Build connection strings safely](#build-connection-strings-safely).

## Specify Boolean values

Keywords that take a Boolean value accept `true`, `false`, `yes`, and `no`, in any case. `Integrated Security` also accepts `sspi`, which means `true`. The builder writes `True` or `False`.

## Use keywords from other drivers

Microsoft.Data.SqlClient doesn't accept ODBC or OLE DB keywords such as `Driver` and `Provider`. Remove these keywords when you move a connection string from another driver. `System.Data.SqlClient` connection strings work in Microsoft.Data.SqlClient, with these differences:

- `Encrypt` defaults to `Mandatory` in Microsoft.Data.SqlClient. A server without a trusted certificate fails certificate validation. Install a trusted certificate on the server.
- On .NET, `Network Library`, `Connection Reset`, and `Transparent Network IP Resolution` aren't supported. Remove them.
- `Context Connection=true` isn't supported.
- `Asynchronous Processing` isn't a supported keyword and throws `ArgumentException`. Remove it. Asynchronous methods work without it.
- `Type System Version=SQL Server 2000` isn't supported. Creating the connection throws `ArgumentException`. Use `Latest`, `SQL Server 2012`, `SQL Server 2008`, or `SQL Server 2005`.

## Related content

- [Use Microsoft.Data.SqlClient in a .NET app](get-started-sqlclient-driver.md)
- [Connection options](connection-options.md)
- [Connection string builders](connection-string-builders.md)
- [Connect to a data source](connecting-to-data-source.md)
- [SQL Server connection pooling](sql-server-connection-pooling.md)
- [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
- [SQL Server Express user instances](sql/sql-server-express-user-instances.md)
- [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md)
