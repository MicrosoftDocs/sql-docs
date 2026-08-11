---
title: "go-mssqldb Connection Strings"
description: "Connection string formats for the go-mssqldb driver, including URL, ADO, and ODBC styles."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/31/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb connection strings

The `go-mssqldb` driver accepts three connection string formats: URL, ADO, and ODBC. All three formats support the same set of connection parameters. Choose the format that fits your application or deployment model best.

> [!TIP]
> **Use URL format for new Go applications.** It integrates with Go's `net/url` package for safe encoding and programmatic construction. Use ADO format when porting connection strings from .NET applications, and use ODBC format when brace escaping is more convenient for values that contain semicolons or other special characters.

## URL format

The URL format follows the `sqlserver://` scheme and is the most common format for Go applications:

```text
sqlserver://username:password@host:port?database=AdventureWorks2025&param=value
```

### Examples

Connect to a local default instance with SQL authentication:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025
```

Connect to a named instance:

```text
sqlserver://<user>:<password>@<server>/myinstance?database=AdventureWorks2025
```

Connect to Azure SQL Database:

```text
sqlserver://<user>:<password>@<server>.database.windows.net?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false
```

Use Windows authentication (Windows only):

```text
sqlserver://<server>?database=AdventureWorks2025&trusted_connection=yes
```

> [!NOTE]
> In the URL format, the port defaults to 1433. To specify a different port, include it after the host: `sqlserver://<server>:1434?database=AdventureWorks2025`.

### Special characters in passwords

In URL format, passwords with special characters must be percent-encoded. Common characters that require encoding:

| Character | Encoded | Example password | Encoded form |
| --- | --- | --- | --- |
| `@` | `%40` | `p@ssword` | `p%40ssword` |
| `:` | `%3A` | `p:ssword` | `p%3Assword` |
| `/` | `%2F` | `p/ssword` | `p%2Fssword` |
| `#` | `%23` | `p#ssword` | `p%23ssword` |
| `%` | `%25` | `p%ssword` | `p%25ssword` |

Use `url.UserPassword()` to handle encoding automatically:

```go
u := &url.URL{
    Scheme: "sqlserver",
    User:   url.UserPassword("<user>", "p@ss:word#1"),
    Host:   "<server>:1433",
}
u.RawQuery = url.Values{"database": {"AdventureWorks2025"}}.Encode()
// Result: sqlserver://<user>:p%40ss%3Aword%231@<server>:1433?database=AdventureWorks2025
```

In ADO format, percent-encoding isn't needed. If a value contains semicolons, wrap it in double quotes: `password="<password>"`. In ODBC format, wrap the value in braces instead: `password={<password>}`.

## ADO format

The ADO format uses semicolon-separated `key=value` pairs:

```text
server=<server>;user id=<user>;password=<password>;database=AdventureWorks2025
```

### ADO examples

Connect with a port number:

```text
server=<server>;port=1434;user id=<user>;password=<password>;database=AdventureWorks2025
```

Connect to a named instance:

```text
server=<server>\myinstance;user id=<user>;password=<password>;database=AdventureWorks2025
```

> [!NOTE]
> In the ADO format, the `server` and `port` fields are separate parameters. Don't include a colon-separated port in the `server` value.

## ODBC format

The ODBC format uses the `odbc:` prefix with semicolon-separated `key=value` pairs. Values that contain special characters can be enclosed in braces:

```text
odbc:server=<server>;user id=<user>;password={<password>};database=AdventureWorks2025
```

### Braces escaping

In ODBC format, wrap a value in `{}` to escape semicolons, equals signs, and other special characters. To include a literal `}` inside braces, double it:

```text
odbc:password={my}}password}
```

## Common parameters

The following parameters are shared across all three formats:

| Parameter | Aliases | Description |
| --- | --- | --- |
| `user id` | `user` | SQL Server login name. |
| `password` | - | Password for the SQL Server login. |
| `database` | - | Target database name. |
| `connection timeout` | - | Login timeout in seconds. The driver default is `0`, which waits indefinitely. Prefer using Go contexts to control connection and query timeouts. For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, the first connection to a paused database fails with error 40613 while the database resumes. Add retry logic rather than a longer timeout. Databases generally resume in less than one minute. See [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume). |
| `dial timeout` | - | Network dial timeout in seconds. The driver default is based on the registered protocols. Set `0` to wait indefinitely. |
| `encrypt` | - | Encryption mode: `strict`, `true`/`mandatory`, `false`/`optional`, `disable`. When omitted, the default is `false`/`optional`. For Azure SQL and production connections, set `encrypt=true` explicitly. |
| `TrustServerCertificate` | - | Skip certificate validation. When `encrypt` is specified, the default is `false`. When `encrypt` is omitted, the default is `true`. Use `false` for production and Azure SQL connections. |
| `app name` | - | Application name passed to the server. |
| `authenticator` | - | Custom authenticator. Used by the `azuread` package internally. |

For the full list of connection options, see [Connection options](connection-options.md).

## Build connection strings in code

Use the `url.URL` type or `msdsn.Config` struct to build connection strings programmatically instead of concatenating raw strings. This approach avoids injection and encoding issues.

### URL builder

Use `url.URL` from the standard library to construct the connection string safely:

```go
import "net/url"

query := url.Values{}
query.Add("database", "AdventureWorks2025")
query.Add("encrypt", "true")
query.Add("TrustServerCertificate", "false")

u := &url.URL{
    Scheme:   "sqlserver",
    User:     url.UserPassword("<user>", "<password>"),
    Host:     "<server>.database.windows.net:1433",
    RawQuery: query.Encode(),
}
connString := u.String()
```

### NewConnectorConfig

Use `NewConnectorConfig` with `msdsn.Config` for structured configuration, especially when you need to set a custom dialer, `SessionInitSQL`, or explicit TLS settings such as `TrustServerCertificate`:

```go
import (
    "database/sql"
    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
)

config := msdsn.Config{
    Host:                   "<server>",
    Port:                   1433,
    Database:               "AdventureWorks2025",
    TrustServerCertificate: false,
}

connector := mssql.NewConnectorConfig(config)
db := sql.OpenDB(connector)
```

If you still depend on the deprecated `mssql` driver name behavior that rewrites `?` placeholders, use `NewConnectorWithProcessQueryText` instead of `NewConnectorConfig` when you build the connector.

## Related content

- [Connection options](connection-options.md)
- [Encryption and certificates](encryption-certificates.md)
- [SQL Server and Windows authentication](authentication.md)
- [Microsoft Entra ID authentication](entra-authentication.md)
