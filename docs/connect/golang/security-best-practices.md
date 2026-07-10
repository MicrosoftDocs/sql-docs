---
title: "go-mssqldb Security Best Practices"
description: "Prevent SQL injection, manage secrets, configure encryption, and follow security best practices with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: best-practice
ai-usage: ai-assisted
---
# Security best practices with go-mssqldb

This article covers security practices for Go applications that connect to SQL Server by using the `go-mssqldb` driver. It addresses SQL injection prevention, credential management, encryption configuration, and the principle of least privilege.

## Prevent SQL injection

SQL injection is the most common database security vulnerability. The `go-mssqldb` driver provides parameterized queries that separate SQL code from data values. Always use parameters for user-supplied input.

### Use parameterized queries

Parameters are sent separately from the SQL text, so user input can never be interpreted as SQL code:

```go
// CORRECT: Parameters are sent separately from the SQL text.
rows, err := db.QueryContext(ctx,
    "SELECT * FROM Sales.vSalesPerson WHERE FirstName = @name AND CountryRegionName = @loc",
    sql.Named("name", userName),
    sql.Named("loc", userLocation))
```

### Never concatenate user input into SQL

String concatenation allows attackers to inject arbitrary SQL:

```go
// WRONG: SQL injection vulnerability.
query := fmt.Sprintf("SELECT * FROM Sales.vSalesPerson WHERE FirstName = '%s'", userName)
rows, err := db.QueryContext(ctx, query) // If userName is "'; DROP TABLE HumanResources.Department;--" ...
```

### Dynamic table or column names

You can't use parameterized queries for table names, column names, or other identifiers. If your application requires dynamic identifiers, validate them against an allow list:

```go
// Validate against known-safe values.
var validColumns = map[string]bool{
    "FirstName": true, "CountryRegionName": true, "TerritoryName": true,
}

func queryByColumn(ctx context.Context, db *sql.DB, column, value string) (*sql.Rows, error) {
    if !validColumns[column] {
        return nil, fmt.Errorf("invalid column name: %q", column)
    }
    // Safe to use column directly because it was validated against the allowlist.
    query := fmt.Sprintf("SELECT * FROM Sales.vSalesPerson WHERE [%s] = @p1", column)
    return db.QueryContext(ctx, query, sql.Named("p1", value))
}
```

> [!CAUTION]
> Never place user-supplied input directly in SQL identifiers, even with bracket escaping. Always validate against an allow list of permitted values.

### Stored procedures and SQL injection

Stored procedures don't automatically prevent SQL injection. If the procedure uses `EXECUTE` or [sp_executesql](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md) with concatenated strings internally, it can still be vulnerable. Use parameterized calls:

```go
// Parameters are passed safely to the procedure.
_, err := db.ExecContext(ctx, "dbo.SearchEmployees",
    sql.Named("searchTerm", userInput))
```

## Manage connection string secrets

Connection strings can contain passwords, client secrets, and certificate paths. Never store them in source code.

### Use environment variables

Read the full connection string from an environment variable:

```go
connString := os.Getenv("MSSQL_CONNECTION_STRING")
if connString == "" {
    log.Fatal("MSSQL_CONNECTION_STRING environment variable is required")
}
db, err := sql.Open("sqlserver", connString)
```

### Build connection strings from individual secrets

Assemble the connection string from separate environment variables for each component:

```go
import (
    "net/url"
    "os"
)

query := url.Values{}
query.Add("database", os.Getenv("DB_NAME"))
query.Add("encrypt", "true")

password := os.Getenv("DB_PASSWORD")

u := &url.URL{
    Scheme:   "sqlserver",
    User:     url.UserPassword(os.Getenv("DB_USER"), password),
    Host:     os.Getenv("DB_HOST"),
    RawQuery: query.Encode(),
}
db, err := sql.Open("sqlserver", u.String())
```

### Eliminate passwords entirely

The most secure connection string has no password at all. Use Microsoft Entra ID authentication with managed identity:

```go
import _ "github.com/microsoft/go-mssqldb/azuread"

// No password, no secret. The managed identity handles authentication.
db, err := sql.Open("azuresql",
    "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
```

### Secret storage options

| Environment | Recommended storage | Notes |
| --- | --- | --- |
| Local development | Environment variables or user secrets file | Don't check `.env` files into source control. |
| Azure App Service / Container Apps | App settings or Key Vault references | Key Vault references inject secrets as environment variables at runtime. |
| Kubernetes | Kubernetes Secrets or Azure Key Vault with CSI driver | Secrets are mounted as files or environment variables. |
| CI/CD pipelines | Pipeline secrets / GitHub Actions secrets | Use `ActiveDirectoryServicePrincipal` or `ActiveDirectoryAzurePipelines` for Azure SQL. |

> [!IMPORTANT]
> Add `.env`, `*.pfx`, and `*.pem` to your `.gitignore` file. Accidentally committing secrets to a repository is one of the most common sources of credential leaks.

## Configure encryption

### Use encryption for all remote connections

Set `encrypt=true` for all remote connections. Encryption that is only required by the server is vulnerable to adversary-in-the-middle attacks. The client must enforce encryption. Azure SQL enables encryption by default:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true
```

### Use TDS 8.0 for the strongest encryption

TDS 8.0 (strict mode) performs the TLS handshake before any TDS protocol data is exchanged, preventing protocol downgrade attacks:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=strict
```

TDS 8.0 requires SQL Server 2022 or Azure SQL.

### Don't use TrustServerCertificate in production

Setting `TrustServerCertificate=true` disables certificate validation, which allows adversary-in-the-middle attacks:

```go
// DEVELOPMENT ONLY. Never deploy to production.
connString := "sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&TrustServerCertificate=true"
```

For production, configure proper certificate validation. See [Encryption and certificates](encryption-certificates.md).

### Set a minimum TLS version

Enforce TLS 1.2 or later to prevent protocol downgrade attacks.

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&tlsmin=1.2
```

## Apply the principle of least privilege

### Use database-scoped users

Create contained database users scoped to a single database instead of server-level logins that access multiple databases.

```sql
-- Create a contained database user (no server-level login needed).
CREATE USER [app_user] WITH PASSWORD = '<password>';

-- Grant only the permissions the application needs.
ALTER ROLE db_datareader ADD MEMBER [app_user];
ALTER ROLE db_datawriter ADD MEMBER [app_user];
GRANT EXECUTE ON SCHEMA::dbo TO [app_user];
```

### Separate read and write connections

If your app has distinct read and write paths, create separate database users with appropriate permissions.

```go
// Read-only queries use a restricted user.
readDB, _ := sql.Open("sqlserver",
    "sqlserver://app_reader:password@<server>?database=AdventureWorks2025&ApplicationIntent=ReadOnly&encrypt=true")

// Write operations use a user with write permissions.
writeDB, _ := sql.Open("sqlserver",
    "sqlserver://app_writer:password@<server>?database=AdventureWorks2025&encrypt=true")
```

### Avoid using sa or dbo in applications

The `sa` login and `dbo` user have unrestricted access. If an application connected as `sa` has a SQL injection vulnerability, the attacker gains full control over the database server.

## Protect Always Encrypted keys

When you use Always Encrypted, the column master key (CMK) protects the column encryption keys (CEK). Secure the CMK appropriately:

| Key provider | Best practice |
| --- | --- |
| Local certificate (PFX) | Store the PFX file outside the application directory. Set restrictive file permissions. Use an environment variable for the password. |
| Windows Certificate Store | Use `CurrentUser\My` for service accounts. Set certificate permissions by using `certutil`. |
| Azure Key Vault | Use managed identity for access. Set key vault access policies with least privilege. Enable soft delete and purge protection. |

For key provider configuration, see [Always Encrypted](always-encrypted.md).

## Log safely

### Don't log connection strings or credentials

Connection strings can contain passwords that end up in log files:

```go
// WRONG: Logs the password.
log.Printf("Connecting to %s", connString)

// CORRECT: Log only the server and database.
log.Printf("Connecting to server=%s database=%s", serverName, dbName)
```

If you must record connection details for diagnostics, redact the connection string first:

```go
func redactSQLServerURL(raw string) string {
    u, err := url.Parse(raw)
    if err != nil {
        return "<invalid connection string>"
    }

    if u.User != nil {
        username := u.User.Username()
        if username != "" {
            u.User = url.UserPassword(username, "REDACTED")
        }
    }

    q := u.Query()
    for _, key := range []string{"password", "clientassertion", "systemtoken"} {
        if q.Has(key) {
            q.Set(key, "REDACTED")
        }
    }
    u.RawQuery = q.Encode()
    return u.String()
}
```

Log the redacted value only when you need it for short-lived diagnostics. Prefer logging the server name, database name, and authentication mode separately.

### Don't log sensitive query parameters

Avoid logging parameter values that contain personal or sensitive data:

```go
// WRONG: Logs sensitive parameter values.
log.Printf("Query: SELECT * FROM HumanResources.Employee WHERE NationalIDNumber = %s", nationalIDNumber)

// CORRECT: Log the query structure without parameter values.
log.Printf("Querying HumanResources.Employee by NationalIDNumber")
```

### Use log flags carefully in production

The driver's `log` parameter can output SQL statements and parameter values:

| Flag | Risk | Production safe? |
| --- | --- | --- |
| `1` (errors) | Low | Yes |
| `2` (messages) | Low | Yes |
| `4` (rows) | Medium, exposes data | No |
| `8` (SQL) | Medium, exposes queries | Conditional |
| `16` (params) | High, exposes parameter values | No |
| `32` (transactions) | Low | Yes |
| `64` (debug) | High, exposes protocol details | No |

For production, use `log=1` (errors only) or `log=3` (errors + messages).

## Security checklist

| Area | Recommendation |
| --- | --- |
| SQL injection | Use parameterized queries for all user input. Validate dynamic identifiers against an allow list. |
| Credentials | Use Microsoft Entra managed identity when possible. Never hard-code passwords in source. |
| Encryption | Set `encrypt=true` or `encrypt=strict` for all production connections. Set `tlsmin=1.2`. |
| Certificates | Don't use `TrustServerCertificate=true` in production. Validate server certificates. |
| Least privilege | Create application-specific database users with only the required permissions. |
| Secrets | Store connection strings in environment variables, Key Vault, or platform secret stores. |
| Logging | Don't log connection strings, passwords, or sensitive parameter values. |
| Source control | Add `.env`, `*.pfx`, and `*.pem` to `.gitignore`. |

## Related content

- [Encryption and certificates](encryption-certificates.md)
- [Microsoft Entra ID authentication](entra-authentication.md)
- [Always Encrypted](always-encrypted.md)
- [Connection strings](connection-strings.md)
- [Azure SQL Database](azure-sql.md)
