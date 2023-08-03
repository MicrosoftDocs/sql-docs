---
title: High availability and disaster recovery on Linux and macOS
description: Learn about how the Microsoft ODBC Driver for Linux and macOS supports Always On availability groups and failover clusters.
author: David-Engel
ms.author: v-davidengel
ms.date: 05/06/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# High availability and disaster recovery on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC drivers for Linux and macOS support Always On availability groups. For more information about Always On availability groups, see:

- [Availability group listeners, client connectivity, and application failover (SQL Server)](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)

- [Creation and configuration of availability groups (SQL Server)](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)

- [Failover clustering and Always On availability groups (SQL Server)](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)

- [Active secondaries: Readable secondary replicas (Always On availability groups)](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)

You can specify the availability group listener of a particular availability group in the connection string. If an ODBC application on Linux or macOS is connected to a database in an availability group that fails over, the original connection is broken. The application must open a new connection to continue work after the failover.

The ODBC drivers on Linux and macOS iterate sequentially through all IP addresses associated with a DNS hostname, if you aren't connecting to an availability group listener. If the DNS server's first returned IP address isn't connectable, these iterations can be time consuming. 

When you're connecting to an availability group listener, the driver attempts to establish connections to all IP addresses in parallel. If a connection attempt succeeds, the driver discards any pending connection attempts.

> [!NOTE]
> Because a connection can fail due to an availability group failover, you should implement connection retry logic. Retry a failed connection until it reconnects. Increasing the connection timeout and implementing connection retry logic increases the chance of connecting to an availability group.

## Connect with MultiSubnetFailover

Always specify `MultiSubnetFailover=Yes` when you're connecting to a [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] availability group listener or [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] failover cluster instance. `MultiSubnetFailover` enables faster failover for all availability groups and failover cluster instances in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].

This connection property also significantly reduces failover time for single and multi-subnet Always On topologies. During a multi-subnet failover, the client attempts connections in parallel. During a subnet failover, the driver aggressively retries the TCP connection.

The `MultiSubnetFailover` connection property indicates that the application is being deployed in an availability group or failover cluster instance. The driver tries to connect to the database on the primary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses.

When you connect with `MultiSubnetFailover=Yes`, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. `MultiSubnetFailover=Yes` enables faster reconnection after failover of either an Always On availability group, or an Always On failover cluster instance. `MultiSubnetFailover=Yes` applies to both single- and multi-subnet availability groups and failover cluster instances.

Use `MultiSubnetFailover=Yes` when you're connecting to an availability group listener or failover cluster instance. Otherwise, your application's performance can be negatively affected.

### Recommendations

When you're connecting to a server in an availability group or failover cluster instance:

- Specify `MultiSubnetFailover=Yes` to improve performance when you're connecting to a single subnet or multi-subnet availability group.

- Specify the availability group listener of the availability group as the server in your connection string.

- You can't connect to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses.

- Both [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication or Kerberos Authentication can be used with `MultiSubnetFailover=Yes`, without affecting the behavior of the application.

- You can increase the value of `loginTimeout` to accommodate for failover time and reduce the application's connection retry attempts.

- Distributed transactions aren't supported.

If read-only routing isn't in effect, connecting to a secondary replica location in an availability group fails in the following situations:

- If the secondary replica location isn't configured to accept connections.

- If an application uses `ApplicationIntent=ReadWrite` and the secondary replica location is configured for read-only access.

A connection fails if a primary replica is configured to reject read-only workloads, and the connection string contains `ApplicationIntent=ReadOnly`.

[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]

## ODBC syntax

Two ODBC connection string keywords support Always On availability groups:

- `ApplicationIntent`

- `MultiSubnetFailover`

For more information about ODBC connection string keywords, see [Using connection string keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).

The equivalent connection attributes are:

- `SQL_COPT_SS_APPLICATION_INTENT`

- `SQL_COPT_SS_MULTISUBNET_FAILOVER`

For more information about ODBC connection attributes, see [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).

An ODBC application that uses Always On availability groups can use one of two functions to make the connection:

| Function | Description |
|--|--|
| [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md) | `SQLConnect` supports both `ApplicationIntent` and `MultiSubnetFailover` via a data source name (DSN) or connection attribute. |
| [SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md) | `SQLDriverConnect` supports `ApplicationIntent` and `MultiSubnetFailover` via DSN, connection string keyword, or connection attribute. |

## See also

[Connection string keywords and data source names (DSNs)](connection-string-keywords-and-data-source-names-dsns.md)

[Programming guidelines](programming-guidelines.md)

[Release notes](release-notes-odbc-sql-server-linux-mac.md)
