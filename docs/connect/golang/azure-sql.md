---
title: "go-mssqldb with Azure SQL Database"
description: "Connect Go applications to Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric using the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/08/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Use go-mssqldb with Azure SQL Database

The `go-mssqldb` driver supports connecting to Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. This article covers Azure-specific configuration, authentication, connection limits, and troubleshooting that differs from on-premises SQL Server.

## Connect to Azure SQL Database

Azure SQL Database requires encrypted connections by default. Specify `encrypt=true` and `TrustServerCertificate=false` explicitly so the connection uses TLS and validates the server certificate:

```go
db, err := sql.Open("sqlserver",
    "sqlserver://<user>:<password>@<server>.database.windows.net?database=<database>&encrypt=true&TrustServerCertificate=false")
if err != nil {
    panic(err)
}
```

> [!NOTE]
> When you omit `encrypt`, the driver doesn't automatically add Azure-specific TLS settings. Keep `encrypt=true&TrustServerCertificate=false` in Azure SQL connection strings.

## Use passwordless authentication (recommended)

Microsoft Entra ID authentication eliminates passwords from your connection strings. `ActiveDirectoryDefault` automatically selects the best available credential for the environment, which makes it convenient for development:

```go
import (
    "database/sql"
    "log"

    _ "github.com/microsoft/go-mssqldb/azuread"
)

func main() {
    db, err := sql.Open("azuresql",
        "sqlserver://<server>.database.windows.net?database=<database>&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
    if err != nil {
        log.Fatal(err)
    }
    defer db.Close()
}
```

> [!IMPORTANT]
> `ActiveDirectoryDefault` is convenient for development, but it can add connection latency because it probes multiple credential sources. For production services, prefer an explicit method such as `ActiveDirectoryManagedIdentity` or `ActiveDirectoryServicePrincipal`.

### How ActiveDirectoryDefault resolves credentials

`ActiveDirectoryDefault` tries the following credential sources in order and uses the first one that succeeds:

| Order | Credential source | Typical environment |
| --- | --- | --- |
| 1 | Environment variables (`AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, `AZURE_CLIENT_SECRET`) | CI/CD pipelines, Docker containers |
| 2 | Workload identity | Kubernetes pods with Azure Workload Identity |
| 3 | Managed identity | Azure VMs, App Service, Container Apps, Azure Functions |
| 4 | Azure CLI (`az login`) | Local development |
| 5 | Azure Developer CLI (`azd auth login`) | Local development |

This credential chain makes `ActiveDirectoryDefault` convenient during development, but the sequential probing adds latency to every new connection. For production, specify the exact authentication method (such as `ActiveDirectoryManagedIdentity`) so the driver skips unnecessary checks.

### Managed identity for production (recommended)

Applications hosted in Azure (App Service, Container Apps, Azure Functions, or Azure VMs) should use a managed identity with an explicit `fedauth` value. This approach avoids the credential-chain overhead and removes any dependency on environment variables or CLI state.

**System-assigned managed identity:**

```text
sqlserver://<server>.database.windows.net?database=<database>&fedauth=ActiveDirectoryManagedIdentity&encrypt=true&TrustServerCertificate=false
```

**User-assigned managed identity** (specify the client ID):

```text
sqlserver://<server>.database.windows.net?database=<database>&fedauth=ActiveDirectoryManagedIdentity&user id=<client-id>&encrypt=true&TrustServerCertificate=false
```

#### Grant the identity access in the database

After configuring the managed identity on the Azure resource, create a contained database user:

```sql
CREATE USER [my-app-identity] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [my-app-identity];
ALTER ROLE db_datawriter ADD MEMBER [my-app-identity];
```

For system-assigned identities, use the Azure resource name. For user-assigned identities, use the identity name.

### Service principal for automation

For CI/CD pipelines or service-to-service authentication:

```text
sqlserver://<server>.database.windows.net?database=<database>&fedauth=ActiveDirectoryServicePrincipal&user id=<client-id>&password=<client-secret>&encrypt=true&TrustServerCertificate=false
```

For all credential types, see [Microsoft Entra ID authentication](entra-authentication.md).

## Configure the Azure firewall

Azure SQL Database uses a server-level firewall. You must allow your client's public IP address, or use a private endpoint.

### Error: Cannot open server

This error message indicates that the Azure firewall is blocking your client IP address:

```output
mssql: login error: Cannot open server '<server>' requested by the login.
Client with IP address '<client-ip>' is not allowed to access the server.
```

**Solutions:**

1. Add a firewall rule in the Azure portal: **SQL server** > **Networking** > **Add a firewall rule**.
1. Enable **Allow Azure services and resources to access this server** if your application runs in Azure.
1. For private connectivity, configure a [private endpoint](/azure/azure-sql/database/private-endpoint-overview).

### Error: Connection timed out

If the connection times out without a clear error, the firewall is likely blocking the connection silently. Verify the firewall rules first.

## Connection limits by service tier

Azure SQL Database enforces connection limits per database based on the service tier. Exceeding the limit causes authentication failures for new connections. For the full limits tables, see [DTU single database resource limits](/azure/azure-sql/database/resource-limits-dtu-single-databases) and [vCore single database resource limits](/azure/azure-sql/database/resource-limits-vcore-single-databases).

### Set MaxOpenConns to match your tier

Always set `MaxOpenConns` to a value below the connection limit for your Azure SQL tier:

```go
// Example for S2 tier (60 max workers).
// Leave headroom for Azure management connections and other clients.
db.SetMaxOpenConns(20)
db.SetMaxIdleConns(10)
db.SetConnMaxLifetime(5 * time.Minute)
```

> [!TIP]
> If multiple applications share the same database, divide the connection limit across all applications. For example, if three services share an S2 database (60 max workers), allocate 15-20 connections per service.

## Handle Azure SQL throttling

Azure SQL Database can throttle connections and queries when the database approaches resource limits (CPU, IO, memory, or session count). Throttling manifests as specific error numbers.

### Common throttling errors

| Error number | Message pattern | Cause |
| --- | --- | --- |
| 10928 | `Resource ID: %d. The %s limit for the database is %d and has been reached.` | Session or worker limit reached. |
| 10929 | `Resource ID: %d. The %s minimum guarantee is %d, maximum limit is %d.` | Resource governor throttling. |
| 40501 | `The service is currently busy.` | General throttling. Retry. |
| 40544 | `The database has reached its size quota.` | Database size limit reached. Increase capacity or free space before retrying. |
| 40549 | `Session is terminated because you have a long-running transaction.` | Transaction exceeded time limit. |
| 40550 | `Session is terminated because of too many locks.` | Excessive lock acquisition. |
| 40551 | `Session is terminated because of excessive tempdb usage.` | Excessive tempdb usage. |
| 40552 | `Session is terminated because of excessive transaction log usage.` | Transaction log space exceeded. |
| 40553 | `Session is terminated because of excessive memory usage.` | Excessive memory consumption. |
| 40613 | `Database '%.*ls' on server '%.*ls' is not currently available.` | Database being moved or reconfigured. |
| 49918 | `Cannot process request. Not enough resources to process request.` | Resource exhaustion. |
| 49919 | `Cannot process create or update request.` | Too many concurrent create/update operations. |
| 49920 | `Cannot process request. Too many operations in progress.` | Concurrent operation limit reached. |

### Retry throttled requests

Most Azure SQL throttling and availability errors in the preceding table are transient and should be retried with exponential backoff. Error `40544` isn't transient. It means the database reached its size quota, so the operation won't succeed until you scale up the database or delete data.

For a complete retry implementation, see [Error handling and retry patterns](error-handling.md).

```go
import (
    "errors"

    mssql "github.com/microsoft/go-mssqldb"
)

func isAzureThrottling(err error) bool {
    var mssqlErr mssql.Error
    if !errors.As(err, &mssqlErr) {
        return false
    }
    switch mssqlErr.Number {
    case 10928, 10929, 40501, 40549, 40550, 40551, 40552, 40553,
        40613, 49918, 49919, 49920:
        return true
    }
    return false
}
```

## Connection resilience

Azure SQL Database occasionally reconfigures servers for updates, failovers, and load balancing. These events drop existing connections, which surface as `driver: bad connection` errors. Configure your pool to recover automatically:

```go
db.SetConnMaxLifetime(5 * time.Minute)  // Rotate connections so stale ones are replaced.
db.SetConnMaxIdleTime(2 * time.Minute)  // Recycle before Azure gateway drops idle connections (30 min).
db.SetMaxIdleConns(10)                  // Keep warm connections for quick recovery.
```

> [!NOTE]
> The Azure SQL gateway closes connections that are idle for about 30 minutes. Set `ConnMaxIdleTime` well below this threshold to avoid `driver: bad connection` errors on the first query after an idle period. For non-transactional calls, `database/sql` retries automatically on a new connection. For transactional calls, your code must catch the error and retry the entire transaction.

### Reconnection after failover

Outside transactions, `database/sql` can transparently retry a call that starts on a bad connection when the driver marks the connection as unusable. This behavior isn't a full transient-fault retry policy for throttling, failover, or other retryable SQL errors. Wrap your database calls in a retry function to handle those scenarios:

```go
var count int
err := RetryFunc(ctx, DefaultRetryConfig, func(ctx context.Context) error {
    return db.QueryRowContext(ctx, "SELECT COUNT(*) FROM HumanResources.Employee").Scan(&count)
})
```

See [Error handling and retry patterns](error-handling.md) for the `RetryFunc` implementation.

## Azure SQL Managed Instance

Azure SQL Managed Instance supports the same driver features as on-premises SQL Server, with a few differences:

| Feature | Azure SQL Database | Azure SQL Managed Instance |
| --- | --- | --- |
| SQL Server Agent | Not available | Available |
| Cross-database queries | Not available | Available |
| Linked servers | Not available | Available |
| Named pipes | Not available | Not available (TCP only) |
| Shared memory | Not available | Not available (TCP only) |
| Windows authentication (SSPI) | Not available | Available within the managed VNet |

Connect to a Managed Instance:

```text
sqlserver://<user>:<password>@<instance>.database.windows.net?database=<database>&encrypt=true&TrustServerCertificate=false
```

## SQL database in Microsoft Fabric

> [!IMPORTANT]
> SQL database in Fabric requires Microsoft Entra ID authentication. SQL Server authentication isn't supported.
>
> For production workloads, prefer an explicit `fedauth` mode instead of `ActiveDirectoryDefault` to avoid credential-chain probing overhead on new connections.

SQL database in Fabric supports the `go-mssqldb` driver with Microsoft Entra ID authentication:

```go
db, err := sql.Open("azuresql",
    "sqlserver://<server>.database.fabric.microsoft.com?database=<database>&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
if err != nil {
    panic(err)
}
```

## Azure SQL performance tips

| Tip | Details |
| --- | --- |
| Use connection pooling | Azure SQL counts each open connection toward the tier limit. Keep `MaxOpenConns` bounded. |
| Enable `encrypt=strict` | For the strongest security, use TDS 8.0 encryption: `encrypt=strict`. Azure SQL Database supports strict mode. |
| Use `ApplicationIntent=ReadOnly` | Route read-heavy queries to read replicas: `ApplicationIntent=ReadOnly`. Available on Premium, Business Critical, and Hyperscale tiers. |
| Monitor DTU/vCore usage | High CPU, IO, or worker usage indicates your tier might be undersized. Use Azure Monitor to track resource utilization. |
| Keep transactions short | Azure SQL terminates sessions with transactions that exceed resource thresholds (error 40549). |
| Use regional endpoints | Place your application in the same Azure region as the database to minimize latency. |

## Azure SQL troubleshooting checklist

| Symptom | Likely cause | Solution |
| --- | --- | --- |
| `Cannot open server` | Firewall rule missing | Add your IP or enable Azure services access. |
| `Login failed` | Wrong credentials or missing database user | Verify the login exists and has database access. |
| Connections time out intermittently | Server reconfiguration or failover | Implement retry logic and connection rotation. |
| `Resource limit reached` | Too many concurrent connections | Lower `MaxOpenConns` and close connections promptly. |
| `The service is currently busy` | Azure SQL throttling | Retry with exponential backoff. Consider scaling up. |
| Slow queries after working fine | DTU/vCore exhaustion | Check Azure Monitor metrics. Scale up or optimize queries. |

## Related content

- [Quickstart: Use Golang to query Azure SQL Database](/azure/azure-sql/database/connect-query-go)
- [Microsoft Entra ID authentication with go-mssqldb](entra-authentication.md)
- [Error handling and retry patterns with go-mssqldb](error-handling.md)
- [Connection pooling with go-mssqldb](connection-pooling.md)
- [go-mssqldb encryption and certificates](encryption-certificates.md)
- [Performance tuning with go-mssqldb](performance-tuning.md)
