---
title: "SqlClient support for high availability, disaster recovery"
description: "Describes SqlClient support for high-availability, disaster recovery (Always On) availability groups."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom: sfi-ropc-nochange
---
# SqlClient support for high availability, disaster recovery

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This article discusses Microsoft SqlClient Data Provider for SQL Server support for high availability and disaster recovery, including Always On Availability Groups. For more information, see [Always On availability groups](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
You can now specify the availability group listener of a high availability and disaster recovery (HADR) availability group (AG) or failover cluster instance (FCI) in the connection property. If a SqlClient application connects to an Always On database that fails over, the original connection breaks and the application must open a new connection to continue work after the failover.  
  
If you don't connect to an availability group listener or FCI, and if multiple IP addresses are associated with a hostname, SqlClient iterates sequentially through all IP addresses associated with the DNS entry. This process can be time consuming if the first IP address returned by the DNS server isn't bound to any network interface card (NIC). When you connect to an AG listener or FCI, SqlClient attempts to establish connections to all IP addresses in parallel. If a connection attempt succeeds, the driver discards any pending connection attempts.  
  
> [!NOTE]
>  Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of a failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
The following connection properties are supported in the Microsoft SqlClient Data Provider for SQL Server:  
  
- `ApplicationIntent`  
  
- `MultiSubnetFailover`  
  
You can programmatically modify these connection string keywords with:  
  
- <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.ApplicationIntent%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.MultiSubnetFailover%2A>  
  
## Connecting With MultiSubnetFailover  
Always specify `MultiSubnetFailover=True` when you connect to a Microsoft SQL family TCP endpoint. This setting applies to availability group listeners, failover cluster instances, and multi-IP endpoints such as Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

When the server name in your connection string resolves to more than one IP address, `MultiSubnetFailover=True` makes SqlClient open connections to all of those addresses at the same time and use the first one that answers. Without it, SqlClient tries the addresses one at a time. An address that doesn't answer stalls until the operating system's TCP connect timeout expires, which can exhaust `Connect Timeout` before SqlClient reaches an address that answers. After a failover, the address SqlClient tries first can be one that no longer serves the database, so a connection that would succeed against another address fails with a timeout instead.

`MultiSubnetFailover=True` changes how quickly the client finds the replica that serves the database. It doesn't change how long the server takes to fail over. With the setting on, SqlClient also retries TCP connection attempts faster than the operating system's default TCP retransmit intervals, which speeds up reconnection for both single- and multi-subnet availability groups and failover cluster instances.

`MultiSubnetFailover=True` is safe on single-IP targets. When DNS resolves to a single address, SqlClient makes a single connection attempt, so the setting costs nothing when it isn't needed.  
  
For more information about connection string keywords in SqlClient, see <xref:Microsoft.Data.SqlClient.SqlConnection.ConnectionString%2A>. For guidance on the related Transparent Network IP Resolution (TNIR) setting on .NET Framework, and to troubleshoot slow connections caused by multi-IP DNS names, see [Disabling Transparent Network IP Resolution](../appcontext-switches.md#disabling-transparent-network-ip-resolution) and [Long connect delays with pre-login handshake timeout](../sqlclient-troubleshooting-guide.md#long-connect-delays-with-pre-login-handshake-timeout).  
  
Use the following guidelines when configuring `MultiSubnetFailover`:  
  
- Set `MultiSubnetFailover=True`.

- To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.

- You can't use `MultiSubnetFailover` when you connect to a named instance.

- You can't use `MultiSubnetFailover` over a protocol other than TCP.

- Connecting to a SQL Server instance configured with more than 64 IP addresses causes a connection failure.  

- You can't use `MultiSubnetFailover` with database mirroring. For more information, see [Upgrading to use multi-subnet clusters from database mirroring](#upgrading-to-use-multi-subnet-clusters-from-database-mirroring). Database mirroring is deprecated in all supported versions of SQL Server. Use Always On availability groups instead.
  
- Behavior of an application that uses the `MultiSubnetFailover` connection property is not affected based on the type of authentication: SQL Server Authentication, Kerberos Authentication, or Windows Authentication.  
  
- Increase the value of `Connect Timeout` to accommodate for failover time and reduce application connection retry attempts.  
  
- Distributed transactions are not supported.  
  
 If read-only routing is not in effect, connecting to a secondary replica location will fail in the following situations:  
  
- If the secondary replica location is not configured to accept connections.  
  
- If an application uses `ApplicationIntent=ReadWrite` (discussed below) and the secondary replica location is configured for read-only access.  
  
<xref:Microsoft.Data.SqlClient.SqlDependency> is not supported on read-only secondary replicas.  
  
A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains `ApplicationIntent=ReadOnly`.  
  
## Upgrading to use multi-subnet clusters from database mirroring  
A connection error (<xref:System.ArgumentException>) will occur if the `MultiSubnetFailover` and `Failover Partner` connection keywords are present in the connection string, or if `MultiSubnetFailover=True` and a protocol other than TCP is used. An error (<xref:Microsoft.Data.SqlClient.SqlException>) will also occur if `MultiSubnetFailover` is used and the SQL Server returns a failover partner response indicating it is part of a database mirroring pair.  
  
If you upgrade a SqlClient application that currently uses database mirroring to a multi-subnet scenario, you should remove the `Failover Partner` connection property and replace it with `MultiSubnetFailover` set to `True` and replace the server name in the connection string with an availability group listener. If a connection string uses `Failover Partner` and `MultiSubnetFailover=True`, the driver will generate an error. However, if a connection string uses `Failover Partner` and `MultiSubnetFailover=False` (or `ApplicationIntent=ReadWrite`), the application will use database mirroring.  
  
The driver returns an error if you use database mirroring on the primary replica in the availability group, and if `MultiSubnetFailover=True` is set in the connection string that connects to a primary replica instead of to an availability group listener.  
  
## Specifying application intent  
When you set `ApplicationIntent=ReadOnly`, the client requests a read workload when you connect to an Always On enabled database. The server enforces the intent at connection time and during a `USE` database statement but only to an Always On enabled database.  
  
The `ApplicationIntent` keyword does not work with legacy, read-only databases.  
  
A database can allow or disallow read workloads on the targeted Always On database. (This is done with the `ALLOW_CONNECTIONS` clause of the `PRIMARY_ROLE` and `SECONDARY_ROLE` Transact-SQL statements.)  
  
The `ApplicationIntent` keyword is used to enable read-only routing.  
  
## Read-only routing  
Read-only routing is a feature that can ensure the availability of a read only replica of a database. To enable read-only routing:  
  
- You must connect to an Always On Availability Group availability group listener.  
  
- The `ApplicationIntent` connection string keyword must be set to `ReadOnly`.  
  
- The Availability Group must be configured by the database administrator to enable read-only routing.  
  
It is possible that multiple connections using read-only routing will not all connect to the same read-only replica. Changes in database synchronization or changes in the server's routing configuration can result in client connections to different read-only replicas. To ensure that all read-only requests connect to the same read-only replica, do not pass an availability group listener to the `Data Source` connection string keyword. Instead, specify the name of the read-only instance.  
  
Read-only routing may take longer than connecting to the primary because read only routing first connects to the primary and then looks for the best available readable secondary. Because of this, you should increase your login timeout.  
  
## Related content

- [SQL Server features and ADO.NET](sql-server-features-adonet.md)
