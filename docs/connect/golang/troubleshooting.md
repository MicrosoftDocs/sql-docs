---
title: "go-mssqldb Troubleshooting"
description: "Troubleshoot common errors, connection failures, and certificate issues with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/08/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---
# Troubleshoot the go-mssqldb driver

This article provides solutions for common errors and connectivity problems with the `go-mssqldb` driver.

## Start with the simplest checks

Before you enable verbose logging or change pool settings, work through the following list:

1. Verify basic reachability: server name, port, firewall rules, and whether SQL Server or Azure SQL is accepting connections.
1. Verify authentication inputs: driver name, user name, password, domain format, or `fedauth` configuration.
1. Verify TLS settings: `encrypt`, certificate paths, `hostnameincertificate`, and whether `TrustServerCertificate` is appropriate for the environment.
1. Only after connection setup is correct, investigate pool exhaustion, stale connections, retry logic, and slow or blocked query diagnostics.

Use the early sections of this article for connection setup failures. Use the later sections only after connections succeed at least sometimes and then fail under load, after idle time, or during failover.

## Connection errors

The following sections cover common connection-related error messages and their solutions.

### Unable to open TCP connection

**Error message**: `unable to open tcp connection with host 'localhost:1433': dial tcp 127.0.0.1:1433: connectex: No connection could be made because the target machine actively refused it.`

**Causes and solutions**:

- SQL Server isn't running. Start the SQL Server service.
- TCP/IP isn't enabled. Open SQL Server Configuration Manager and enable TCP/IP under **SQL Server Network Configuration** > **Protocols**.
- Wrong port. Verify the port in SQL Server Configuration Manager or use SQL Server Browser for named instances.
- Firewall blocking the port. Add an inbound rule for port 1433 (or your configured port).

### Login failed for user

**Error message**: `mssql: login error: Login failed for user '<user>'.`

**Causes and solutions**:

- Incorrect user name or password. Verify the credentials.
- SQL Server authentication is disabled. Enable **SQL Server and Windows Authentication mode** in server properties.
- The login doesn't exist. Create the login in SQL Server.
- The login doesn't have access to the target database. Grant database access with `CREATE USER`.

### Certificate validation errors

**Error message**: `TLS Handshake failed: x509: certificate signed by unknown authority`

**Causes and solutions**:

- The server uses a self-signed certificate. Provide the certificate path with the `certificate` or `serverCertificate` parameter, or set `TrustServerCertificate=true` for development only.
- The CA certificate isn't in the system trust store. Add the CA certificate to the OS trust store or specify it with the `certificate` parameter.
- Host name mismatch. Use `hostnameincertificate` to specify the expected name in the certificate.

For more information, see [Encryption and certificates](encryption-certificates.md).

### Connection timeout expired

**Error message**: `unable to open tcp connection with host '<server>:1433': dial tcp: i/o timeout`

**Causes and solutions**:

- Network connectivity problems. Verify you can reach the server by using `telnet <server> 1433` or `Test-NetConnection -ComputerName <server> -Port 1433`.
- DNS resolution failure. Verify the hostname resolves correctly.
- Increase `dial timeout` or `connection timeout` in the connection string.

## Authentication errors

The following sections cover authentication error messages.

### NTLM authentication failures

**Error message**: `NTLM authentication failed`

**Causes and solutions**:

- Incorrect domain format. Use `DOMAIN\user` in the `user id` parameter. In URL format, encode the backslash as `%5C`.
- Wrong password. Verify the domain password.

### Kerberos authentication failures

**Error message**: `krb5: cannot resolve KDC for realm`

**Causes and solutions**:

- Missing or misconfigured `/etc/krb5.conf`. Verify the `[realms]` section contains the correct KDC address for your domain.
- No valid ticket. Run `klist` to check for a valid ticket, or run `kinit` to obtain one.
- Keytab file not found. Verify the path in the `krb5-keytabfile` parameter.

For more information, see [SQL Server and Windows authentication](authentication.md).

### Microsoft Entra ID authentication failures

**Error message**: `clientCredentialFromCert: error reading certificate: ...` or `DefaultAzureCredential: failed to acquire a token`

**Causes and solutions**:

- Incorrect client ID, tenant ID, or client secret. Verify the values in the connection string or environment variables.
- The managed identity isn't configured on the host. Verify the identity in the Azure portal.
- Missing `azuread` package import. Import `github.com/microsoft/go-mssqldb/azuread` and use the `azuresql` driver name.

For more information, see [Microsoft Entra ID authentication](entra-authentication.md).

### Login failed for user '' (empty user name)

**Error message**: `mssql: login error: Login failed for user ''.`

**Cause**: You used `sql.Open("sqlserver", ...)` with a `fedauth` parameter. Entra ID authentication requires the `azuresql` driver name registered by the `azuread` package. With the standard `sqlserver` driver, the `fedauth` parameter is ignored and the driver attempts SQL authentication with no user name.

**Solution**: Import the `azuread` package and use the `azuresql` driver name:

```go
import _ "github.com/microsoft/go-mssqldb/azuread"

db, err := sql.Open("azuresql",
    "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
if err != nil {
    panic(err)
}
```

For more information, see [Microsoft Entra ID authentication](entra-authentication.md).

## Query errors

The following sections cover query execution error messages.

### LastInsertId not supported

**Error message**: `LastInsertId is not supported. Please use the OUTPUT clause or add 'select ID = convert(bigint, SCOPE_IDENTITY())' to the end of your query.`

**Solution**: The `go-mssqldb` driver doesn't support `LastInsertId()`. Use an `OUTPUT` clause or query `SCOPE_IDENTITY()` separately.

### Temporary table not found

**Error message**: `mssql: Invalid object name '#TempTable'.`

**Cause**: Temporary tables are per-connection. If you create a temp table in one call and query it in another, they might use different connections from the pool.

**Solution**: Use `db.Conn(ctx)` to pin to a single connection, or wrap operations in a transaction.

For more information, see [Stored procedures](stored-procedures.md).

## Azure SQL errors

The following sections cover errors specific to Azure SQL Database.

### Transient connection error numbers

Use the following shared list as the reference for transient connection-establishment errors and request-path transport failures that are eligible for bounded retry:

[!INCLUDE [transient-connection-errors](../includes/transient-connection-errors.md)]

### Cannot open server (firewall)

**Error message**: `mssql: login error: Cannot open server '<server>' requested by the login. Client with IP address '203.0.113.42' is not allowed to access the server.`

**Causes and solutions**:

- Your client IP isn't in the Azure SQL firewall rules. Add a firewall rule in the Azure portal: **SQL server** > **Networking** > **Add a firewall rule**.
- If your application runs in Azure, enable **Allow Azure services and resources to access this server**.
- For private connectivity, configure a private endpoint.

### Resource limit reached

**Error message**: `mssql: Resource ID: 1. The session limit for the database is 300 and has been reached.`

**Causes and solutions**:

- Too many concurrent connections for the Azure SQL tier. Lower `MaxOpenConns` in your pool configuration.
- Connection leaks (unclosed rows or transactions). Check for missing `defer rows.Close()` or `defer tx.Rollback()` calls.
- Multiple applications sharing the database. Divide the connection limit across all clients.

For Azure SQL connection limits by tier, see [Azure SQL Database](azure-sql.md).

### The service is currently busy (throttling)

**Error message**: `mssql: The service is currently busy. Retry the request after 10 seconds. Code: 40501.`

**Causes and solutions**:

- The database is under heavy load. Implement retry logic with exponential backoff.
- The workload exceeds the tier's DTU or vCore capacity. Consider scaling up.

For retry implementation patterns, see [Error handling and retry patterns](error-handling.md).

### Database not currently available

**Error message**: `mssql: Database 'AdventureWorks2025' on server '<server>' is not currently available. Code: 40613.`

**Cause**: Azure SQL is reconfiguring the database (failover, update, or scaling operation). This condition is a transient error.

**Solution**: Retry the operation. The database typically becomes available within seconds. For more information, see [Error handling and retry patterns](error-handling.md).

## Bad connection errors

A `driver: bad connection` error means the driver detected that an existing connection is no longer usable. The `database/sql` pool automatically retries the operation on a fresh connection for non-transactional calls, but operations inside an active transaction fail immediately.

Don't start with this section if the application never connected successfully. `driver: bad connection` usually points to connection reuse, failover, idle timeout, or network interruptions after the initial connection was already working.

### Common causes

| Cause | Typical scenario | Fix |
| --- | --- | --- |
| Azure SQL gateway idle timeout | Connection idle for 30+ minutes behind the Azure gateway. | Set `db.SetConnMaxIdleTime(2 * time.Minute)` to recycle idle connections before the gateway drops them. |
| Network interruption | Transient network failure between the client and server. | Implement retry logic for non-transactional operations. See [Error handling](error-handling.md). |
| Server-side session kill | DBA killed the session, or the server was restarted. | Retry. Set `db.SetConnMaxLifetime` to rotate connections. |
| Azure SQL reconfiguration | Failover, scaling, or patching event dropped the connection. | Set `ConnMaxLifetime` to 5 minutes or less. Implement retry logic. |
| Long-running transaction timeout | Azure SQL terminated the session (error 40549). | Keep transactions short. Break large operations into smaller batches. |

### How database/sql handles bad connections

For calls outside a transaction (`db.QueryContext`, `db.ExecContext`), the `database/sql` pool automatically retries the operation on a new connection when the driver reports a bad connection. This retry is transparent to your code.

For calls inside a transaction (`tx.QueryContext`, `tx.ExecContext`), the pool can't retry because the transaction state is lost. Your code must catch the error, roll back, and retry the entire transaction.

### Recommended pool settings for Azure SQL

Configure the pool to handle Azure gateway timeouts and failovers:

```go
db.SetConnMaxLifetime(5 * time.Minute)  // Rotate connections to recover from failovers.
db.SetConnMaxIdleTime(2 * time.Minute)  // Recycle before Azure gateway drops idle connections (30 min).
db.SetMaxIdleConns(10)                  // Keep warm connections for quick recovery.
db.SetMaxOpenConns(20)                  // Stay below your tier's connection limit.
```

For on-premises SQL Server, `ConnMaxIdleTime` is less critical because there's no gateway idle timeout. However, setting it prevents stale connections after network disruptions.

For detailed configuration guidance, see [Azure SQL Database](azure-sql.md).

## Pool exhaustion

Pool exhaustion occurs when all connections in the pool are in use and new callers block waiting for a connection.

### Symptoms

- Requests slow down or time out under load.
- `db.Stats().WaitCount` grows continuously.
- `db.Stats().InUse` equals `MaxOpenConns`.
- Context deadline exceeded errors during peak traffic.

### Diagnosis

Add pool monitoring to your application:

```go
stats := db.Stats()
log.Printf("Pool: open=%d inUse=%d idle=%d waitCount=%d waitDuration=%v",
    stats.OpenConnections, stats.InUse, stats.Idle,
    stats.WaitCount, stats.WaitDuration)
```

### Common causes and solutions

| Cause | How to identify | Fix |
| --- | --- | --- |
| `rows.Close()` not called | `InUse` grows over time, never decreases. | Add `defer rows.Close()` after every `QueryContext`. |
| Long-running transactions | `InUse` stays high during batch processing. | Keep transactions short. Process large batches in smaller chunks. |
| `MaxOpenConns` too low | `WaitCount` grows steadily under normal load after you rule out pinned resources and leaks. | Increase `MaxOpenConns`. |
| `MaxOpenConns` not set | Hundreds of open connections under spike load. | Set `MaxOpenConns` to a bounded value. |
| Goroutine leak calling `db.Conn` | `InUse` grows without corresponding request growth. | Ensure every `db.Conn()` result is closed with `defer conn.Close()`. |

For detailed pool configuration guidance, see [Connection pooling](connection-pooling.md).

## Slow or blocked query diagnostics

### Set query timeouts

Use context deadlines to identify slow queries and prevent blocked SQL calls from pinning connections and stalling callers:

```go
ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
defer cancel()

rows, err := db.QueryContext(ctx, "SELECT * FROM LargeTable WHERE Status = @s",
    sql.Named("s", "active"))
if err != nil {
    // Check if the error was a timeout.
    if ctx.Err() == context.DeadlineExceeded {
        log.Println("Query exceeded 5-second timeout")
    }
    return err
}
defer rows.Close()
```

For a full performance investigation workflow, including Query Store, DMVs, missing-index analysis, and benchmarking, see [Performance tuning](performance-tuning.md).

## Deadlock diagnostics

**Error message**: `mssql: Transaction (Process ID 52) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.`

**Error number**: 1205

**Solution**: Deadlocks occur in concurrent systems. Implement automatic retry logic for error 1205. For a deadlock retry wrapper function, see [Transactions](transactions.md).

**Prevention strategies**:

- Access tables in the same order across all queries.
- Keep transactions short and avoid user interaction during transactions.
- Use `READ COMMITTED SNAPSHOT` isolation to reduce lock contention.

Repeated deadlocks on the same query indicate a design problem. Use the deadlock graph (captured through Extended Events or the system health session) to identify the competing statements and lock types. For a full walkthrough, see the [Deadlocks guide](../../relational-databases/sql-server-deadlocks-guide.md). For deadlock handling strategies in Go, see [Deadlock handling](transactions.md#deadlock-handling) and [Handle deadlocks](error-handling.md#handle-deadlocks).

## Certificate errors with containers (Go 1.23 and later versions)

**Error message**: `x509: negative serial number`

**Cause**: Go 1.23 strictly enforces RFC 5280. The self-signed certificate that SQL Server generates in Docker containers uses a negative serial number, which Go rejects.  

**Solutions**:

- For test environments, add `TrustServerCertificate=true` to skip certificate validation, or `encrypt=disable` to turn off encryption entirely.
- For CI/CD, set the `GODEBUG=x509negativeserial=1` environment variable to restore the pre-Go 1.23 behavior without changing your connection string.
- In `go.mod` (Go 1.23 and later versions), add a `godebug x509negativeserial=1` directive to apply the override at build time.

> [!CAUTION]
> Don't use `TrustServerCertificate=true` or `encrypt=disable` in production. These options disable security checks. For production, use a properly signed certificate.

### SHA-1 certificate errors (Go 1.24 and later versions)

**Error message**: `tls: handshake failure` or `TLS Handshake failed: EOF` when connecting to older SQL Server instances.

**Cause**: Go 1.24 disallows SHA-1 signature algorithms in TLS certificates by default. Older SQL Server versions and some on-premises installations use certificates signed with SHA-1.

**Solutions**:

- Reissue the server certificate with SHA-256 or later (recommended).
- Set the `GODEBUG=tlssha1=1` environment variable to temporarily re-enable SHA-1 support.
- In `go.mod` (Go 1.23 and later versions), add a `godebug tlssha1=1` directive.

### When to use `encrypt=disable` vs. `TrustServerCertificate=true`

| Setting | What it does | When to use |
| --- | --- | --- |
| `TrustServerCertificate=true` | Encrypts traffic but skips certificate validation. | Local development and testing where the server uses a self-signed certificate. |
| `encrypt=disable` | Sends traffic in plaintext (no TLS). | Legacy environments where TLS isn't available. Not recommended. |
| `encrypt=strict` | TDS 8.0 with full TLS validation from the first byte. | Production on SQL Server 2022 or Azure SQL. |

For more information, see [Testing](testing.md) and [Encryption and certificates](encryption-certificates.md).

## Encoding and collation issues

### Implicit conversion warnings

If you pass `string` parameters (sent as `nvarchar`) to `varchar` columns, SQL Server performs an implicit conversion that can prevent index usage.

This example continues the `database/sql` and `mssql` setup from earlier snippets in this article.

**Solution**: Use `mssql.VarChar` for `varchar` columns:

```go
db.QueryContext(ctx, "SELECT * FROM Production.Product WHERE ProductNumber = @p1",
    mssql.VarChar("FR-R92B-58"))
```

### CharsetToUTF8 error with non-Latin characters

**Error message**: `CharsetToUTF8: ...` when querying `varchar` columns containing Chinese, Japanese, or other non-Latin characters stored in a collation like `SQL_Latin1_General_CP1_CI_AS`.

**Cause**: The driver attempts to convert the column's code page to UTF-8, but the stored bytes don't match the collation's expected encoding.

**Solutions**:

- Use `nvarchar` instead of `varchar` for columns that store non-Latin text. `nvarchar` stores data as UTF-16 and avoids code page conversion.
- If you can't change the column type, verify the database collation supports the character set you're storing.

## Enable diagnostic logging

Use the `log` connection parameter to enable driver-level logging:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=63
```

Log flags are bitmask values: `1` (errors), `2` (messages), `4` (rows), `8` (SQL), `16` (params), `32` (transactions), `64` (debug). Combine values by adding them (for example, `63` = all except debug, `127` = all).

For programmatic logging, use `SetLogger` or `SetContextLogger`. See [Logging and diagnostics](logging-diagnostics.md).

## Troubleshooting checklist

| Symptom | First step |
| --- | --- |
| Connection refused | Verify SQL Server is running and TCP/IP is enabled. |
| Login failed | Check credentials and authentication mode. |
| Certificate error | Check server certificate or set `TrustServerCertificate=true` (dev only). |
| Connection timeout | Verify network path with `Test-NetConnection`. Check firewall rules. |
| Azure SQL firewall | Add your IP to Azure SQL firewall rules. |
| Throttling errors | Implement retry with exponential backoff. Scale up the tier. |
| Bad connection | Set `ConnMaxIdleTime` below 30 minutes for Azure SQL. Implement retry logic. |
| Pool exhaustion | Monitor `db.Stats()`. Fix unclosed rows/transactions. Increase `MaxOpenConns`. |
| Slow queries | Set context timeouts. Query DMVs for expensive queries. |
| Deadlocks | Implement retry on error 1205. Access tables in consistent order. |
| Implicit conversion | Use `mssql.VarChar` for `varchar` columns. |

## Related content

- [Error handling and retry patterns](error-handling.md)
- [Connection pooling](connection-pooling.md)
- [Azure SQL Database](azure-sql.md)
- [Transactions](transactions.md)
- [Logging and diagnostics](logging-diagnostics.md)
- [Known limitations](known-limitations.md)
