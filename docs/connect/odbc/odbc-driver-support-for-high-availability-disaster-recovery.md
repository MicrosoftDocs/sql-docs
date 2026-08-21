---
title: High Availability and Disaster Recovery
description: Learn about how the Microsoft ODBC Driver for SQL Server supports Always On availability groups and failover clusters.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, randolphwest
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# High availability and disaster recovery

[!INCLUDE [Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

The Microsoft ODBC Driver for SQL Server supports Always On availability groups. For more information about Always On availability groups, see:

- [Connect to an Always On availability group listener](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)

- [Reference for the creation and configuration of Always On availability groups](../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)

- [Failover Clustering and Always On availability groups (SQL Server)](../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)

- [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)

You can specify the availability group listener of a particular availability group in the connection string. If an ODBC application connects to a database in an availability group that fails over, the original connection breaks. The application must open a new connection to continue work after the failover.

Without `MultiSubnetFailover=Yes`, the driver's legacy multi-IP fallback can be slow when the first resolved IP address isn't reachable. For more information about the Windows fallback behavior, see [Use transparent network ip resolution with the ODBC driver](using-transparent-network-ip-resolution.md).

When you connect to an availability group listener by using `MultiSubnetFailover=Yes`, the driver attempts connections to all resolved IP addresses in parallel. If a connection attempt succeeds, the driver discards any pending connection attempts.

> [!NOTE]  
> Because a connection can fail due to an availability group failover, you should implement connection retry logic. Retry a failed connection until it reconnects. Increasing the connection timeout and implementing connection retry logic increases the chance of connecting to an availability group.

## Connect with MultiSubnetFailover

Set `MultiSubnetFailover=Yes` when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance. `MultiSubnetFailover` enables faster failover recovery by having the driver attempt TCP connections to all resolved IP addresses in parallel and use the first connection that succeeds.

This connection property also significantly reduces failover time for single and multi-subnet Always On topologies. During a multi-subnet failover, the client attempts connections in parallel. During a subnet failover, the driver aggressively retries the TCP connection.

The `MultiSubnetFailover` connection property indicates that the application is being deployed against a topology where the target hostname might resolve to more than one endpoint. The driver tries to connect to the database on the primary [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses.

When you connect by using `MultiSubnetFailover=Yes`, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. `MultiSubnetFailover=Yes` enables faster reconnection after failover of either an Always On availability group, or an Always On failover cluster instance. `MultiSubnetFailover=Yes` applies to both single- and multi-subnet availability groups and failover cluster instances.

`MultiSubnetFailover=Yes` is safe on single-IP targets. When DNS resolves to one address, `MultiSubnetFailover=Yes` doesn't create additional parallel connection attempts.

### Recommendations

When you connect to a highly available or multi-endpoint target (Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance):

- Specify `MultiSubnetFailover=Yes`. It's the recommended setting for these targets, and it's safe to leave on when the target resolves to a single IP address, because the driver then makes a single connect attempt.

- Specify the availability group listener of the availability group as the server in your connection string.

- You can't use `MultiSubnetFailover=Yes` over a protocol other than TCP.

- You can't connect to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses.

- You can't use `MultiSubnetFailover=Yes` with database mirroring. The driver returns an error when the connection string specifies `Failover_Partner`, and also when the server reports that the database is mirrored. Database mirroring is deprecated in all supported versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use Always On availability groups instead.

- Use both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication or Kerberos Authentication with `MultiSubnetFailover=Yes` without affecting the behavior of the application.

- Increase the login timeout to accommodate for failover time and reduce the application's connection retry attempts. The driver has no connection string keyword for this setting. Set it before you connect by calling [SQLSetConnectAttr](../../odbc/reference/syntax/sqlsetconnectattr-function.md) with the `SQL_ATTR_LOGIN_TIMEOUT` attribute. For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use at least 60 seconds. An auto-paused database resumes on the first connect attempt, and that attempt can fail with error 40613 while the database resumes, so the application must retry. For more information, see [Auto-pause and auto-resume in the serverless compute tier for Azure SQL Database](/azure/azure-sql/database/serverless-tier-auto-pause-resume).

- Distributed transactions aren't supported.

If read-only routing isn't in effect, connecting to a secondary replica location in an availability group fails in the following situations:

- If the secondary replica location isn't configured to accept connections.

- If an application uses `ApplicationIntent=ReadWrite` and the secondary replica location is configured for read-only access.

A connection fails if a primary replica is configured to reject read-only workloads, and the connection string contains `ApplicationIntent=ReadOnly`.

[!INCLUDE [specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]

## ODBC syntax

Two ODBC connection string keywords support Always On availability groups:

- `ApplicationIntent`

- `MultiSubnetFailover`

For more information about ODBC connection string keywords, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).

The equivalent connection attributes are:

- `SQL_COPT_SS_APPLICATION_INTENT`

- `SQL_COPT_SS_MULTISUBNET_FAILOVER`

For more information about ODBC connection attributes, see [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).

An ODBC application that uses Always On availability groups can use one of two functions to make the connection:

| Function | Description |
| --- | --- |
| [SQLConnect Function](../../odbc/reference/syntax/sqlconnect-function.md) | `SQLConnect` supports both `ApplicationIntent` and `MultiSubnetFailover` via a data source name (DSN) or connection attribute. |
| [SQLDriverConnect Function](../../odbc/reference/syntax/sqldriverconnect-function.md) | `SQLDriverConnect` supports `ApplicationIntent` and `MultiSubnetFailover` via DSN, connection string keyword, or connection attribute. |

## Related content

- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md)
- [Use transparent network IP resolution with the ODBC driver](using-transparent-network-ip-resolution.md)
- [Connecting from Linux or macOS](linux-mac/connection-string-keywords-and-data-source-names-dsns.md)
- [Programming Guidelines](linux-mac/programming-guidelines.md)
