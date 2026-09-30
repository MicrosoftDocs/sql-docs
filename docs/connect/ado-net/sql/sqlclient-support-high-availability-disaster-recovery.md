---
title: High Availability and Disaster Recovery with Microsoft.Data.SqlClient
description: Configure MultiSubnetFailover, application intent, and read-only routing, and understand reconnect behavior and platform limits in SqlClient.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom: sfi-ropc-nochange
---
# High availability and disaster recovery with SqlClient

<a id="sqlclient-support-for-high-availability-disaster-recovery"></a>

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Connect to a stable service endpoint rather than a particular replica when your application must survive server failover. Microsoft.Data.SqlClient can accelerate connection attempts and request read-only routing, but it doesn't make an in-flight transaction survive a broken connection.

| SQL Server deployment | Endpoint to use |
| --- | --- |
| Always On availability group (AG). | The AG *listener*, a network name that directs connections to the appropriate replica. |
| Failover cluster instance (FCI). | The virtual server name of the clustered SQL Server instance. |
| Legacy database mirroring. | The principal and `Failover Partner`, with the mirrored database specified. |

For the server architecture, see [Always On availability groups](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).

<a id="connecting-with-multisubnetfailover"></a>

## Connect with MultiSubnetFailover

Set `MultiSubnetFailover=true` for supported Microsoft SQL family Transmission Control Protocol (TCP) endpoints, including AG listeners, FCIs, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. Use a hostname and port, not named-instance discovery.

```text
Server=tcp:<listener>,1433;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

Replace the authentication settings for your environment. The certificate must validate for the name used by the client; see [Encryption and certificate validation](../encryption-and-certificate-validation.md).

When the Domain Name System (DNS) returns several Internet Protocol (IP) addresses, `MultiSubnetFailover=true` attempts connections in parallel and uses the first successful connection. This feature reduces delays when some addresses aren't reachable or no longer serve the database. It also accelerates TCP retries. For a single-IP endpoint, only one address is attempted.

The setting doesn't shorten the server's failover or database recovery time. Allow an appropriate `Connect Timeout` and a bounded application retry policy for the availability requirements of your service.

### Connection option compatibility

| Option | Default | Behavior and limitations |
| --- | --- | --- |
| `MultiSubnetFailover` | `false`, unless a process-wide override is enabled. | Parallel TCP attempts. Not supported with named-instance discovery, non-TCP protocols, database mirroring, or more than 64 server IP addresses. |
| `TransparentNetworkIPResolution` | `true` on .NET Framework, subject to automatic endpoint and authentication handling. | Tries an initial address before parallel attempts to alternatives. .NET Framework only; obsolete. Modern .NET rejects the keyword. `MultiSubnetFailover=true` takes precedence. |
| `Failover Partner` | Empty. | Legacy database mirroring only. Requires `Initial Catalog` or `Database`. Incompatible with `MultiSubnetFailover=true` and `ApplicationIntent=ReadOnly`. |
| `ApplicationIntent` | `ReadWrite`. | Sends workload intent to the server. Read-only routing requires server configuration and doesn't apply to arbitrary read-only databases. |

The default for `MultiSubnetFailover` is still `false` in the connection string. A process-wide AppContext switch can enable it for all connections. Prefer explicit connection configuration when an application also uses incompatible targets, such as LocalDB or database mirroring. See [AppContext switches](../appcontext-switches.md).

Transparent Network IP Resolution (TNIR) is obsolete starting with SqlClient 7.1. Don't copy the TNIR keyword into modern .NET connection strings. On .NET Framework, explicit TNIR configuration can override the driver's automatic handling for some endpoints and authentication modes. Don't assume every connection without `MultiSubnetFailover` always tries addresses strictly sequentially.

## Reconnect after a failure

A failover can break an existing connection. Dispose of the failed connection and open a new one against the service endpoint. Connection recovery and retry features have limits; they don't replay an interrupted business operation automatically.

1. Distinguish a transient connectivity error from an invalid credential, denied permission, or certificate failure.
1. Retry connection establishment with bounded delays and an overall time limit.
1. Retry a failed operation only when you can establish whether it committed, or when the operation is designed to be idempotent.
1. Recreate transaction and session state that belonged to a lost session.

A lost response after a commit can leave the client uncertain whether a write succeeded. Retrying that write without duplicate protection can apply it twice. See [Configurable retry logic](../configurable-retry-logic.md) and [SQL Server connection pooling](../sql-server-connection-pooling.md).

<a id="upgrading-to-use-multi-subnet-clusters-from-database-mirroring"></a>

## Upgrade to multi-subnet clusters from database mirroring

Database mirroring is deprecated. `Failover Partner` belongs to database mirroring; it isn't an AG secondary address or an Azure failover-group setting.

When migrating a mirrored database to an AG:

1. Configure and verify the AG listener and database on the server.
1. Replace the old server name with the listener.
1. Remove `Failover Partner`.
1. Set `MultiSubnetFailover=true` and specify the AG database.
1. Exercise failover and retry behavior before deploying the change.

SqlClient rejects `Failover Partner` combined with `MultiSubnetFailover=true`. Merely setting `MultiSubnetFailover=false` doesn't configure mirroring; a valid mirrored pair and database are still required. A server-provided mirroring partner is also incompatible with multi-subnet failover.

<a id="specifying-application-intent"></a>

## Specify application intent

Set `ApplicationIntent=ReadOnly` to request a read workload on an AG database:

```text
Server=tcp:<listener>,1433;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;ApplicationIntent=ReadOnly;
```

The AG's primary and secondary connection policies determine whether the requested workload is accepted. A primary configured to reject read-only workloads can reject this connection. A secondary configured for read-only access rejects a read-write connection.

`ApplicationIntent` doesn't turn a database read-only, replace permissions, or route ordinary read-only databases. Grant read-only permissions when the application must be unable to write.

## Read-only routing

For AG read-only routing, configure all of the following settings:

- Connect to the AG listener.
- Set `Database` to a database in the AG.
- Set `ApplicationIntent=ReadOnly`.
- Configure a readable secondary and its read-only routing URL.
- Configure the primary replica's read-only routing list.

The client first contacts the primary through the listener, then connects to the routed target. Allow network access and valid certificate names for both connection stages. Routing can add connection time.

Separate opens can reach different readable replicas as routing configuration and availability change. A pooled open can reuse an existing connection instead of performing routing again. Connecting directly to a secondary chooses that instance, but bypasses listener-based routing and its failover behavior.

<xref:Microsoft.Data.SqlClient.SqlDependency> isn't supported on read-only secondary replicas. Review the SQL Server version and deployment requirements separately for features such as distributed transactions; `MultiSubnetFailover` alone doesn't establish their support.

## Azure SQL and Microsoft Fabric

For Azure SQL, use the service's configured endpoint, including its failover-group listener when applicable. Don't use `Failover Partner` for an Azure SQL failover group. Service-managed failover differs from configuring a SQL Server AG yourself.

For SQL database in Microsoft Fabric:

- Use its database connection string with Microsoft Entra authentication and `MultiSubnetFailover=true`.
- Use the separate SQL analytics endpoint for its read-only analytics workload. Don't assume `ApplicationIntent=ReadOnly` redirects the writable database endpoint.
- Don't configure `Failover Partner` or SQL Server AG routing for the database service.
- Account for the current `Default` connection policy: allow outbound TCP port 1433 to gateways and ports 11000 through 11999 to the regional Azure SQL addresses.

Fabric provides automatic zone redundancy. Active geo-replication, failover groups, and geo-restore aren't currently supported for SQL database in Fabric. Microsoft Distributed Transaction Coordinator, the elastic database client library, and elastic query also aren't supported. See [Connect to a SQL database in Fabric](/fabric/database/sql/connect) and [Fabric SQL database limitations](/fabric/database/sql/limitations).

## Related content

- [SQL Server features and ADO.NET](sql-server-features-adonet.md)
- [Connection options](../connection-options.md)
- [Troubleshoot SqlClient](../sqlclient-troubleshooting-guide.md)
