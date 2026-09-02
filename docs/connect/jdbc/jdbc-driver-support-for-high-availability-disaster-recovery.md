---
title: Support for High Availability, disaster recovery
description: This article discusses Microsoft JDBC Driver for SQL Server support for high-availability, disaster recovery (Always On Availability Groups).
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# JDBC driver support for High Availability, disaster recovery

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  This article discusses [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] support for high-availability, disaster recovery: [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. For more information about [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], see [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Books Online.

 Beginning in version 4.0 of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you can specify the availability group listener of a (high-availability, disaster-recovery) availability group (AG) in the connection property. If a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application is connected to an Always On database that fails over, the original connection is broken, and the application must open a new connection to continue work after the failover. The following [connection properties](setting-the-connection-properties.md) were added in [!INCLUDE[jdbc-40](../../includes/jdbc-40-md.md)]:

- **multiSubnetFailover**

- **applicationIntent**

Set **multiSubnetFailover=true** when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a Failover Cluster Instance.

>[!Note]
>**multiSubnetFailover** is false by default. Use **applicationIntent** to declare the application workload type. For more information, see the following sections.

Beginning in version 6.0 of the Microsoft JDBC Driver for SQL Server, a new connection property **transparentNetworkIPResolution** (TNIR) is added for transparent connection to Always On availability groups or to a server that has multiple IP addresses associated. When **transparentNetworkIPResolution** is true, the driver attempts to connect to the first IP address available. If the first attempt fails, the driver tries to connect to all IP addresses in parallel until the timeout expires, discarding any pending connection attempts when one of them succeeds.

Note:

- transparentNetworkIPResolution is true by default
- transparentNetworkIPResolution is ignored if multiSubnetFailover is true
- transparentNetworkIPResolution is ignored if database mirroring is used
- transparentNetworkIPResolution is ignored if there are more than 64 IP addresses
- When transparentNetworkIPResolution is true, the first connection attempt uses a timeout value of 500 milliseconds. The rest of the connection attempts follow the same logic as the multiSubnetFailover feature.

> [!NOTE]
> If you are using Microsoft JDBC Driver 4.2 (or lower) for SQL Server and if **multiSubnetFailover** is false, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] attempts to connect to the first IP address. If the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] cannot establish a connection with first IP address, the connection fails. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will not attempt to connect to any subsequent IP address associated with the server.

> [!NOTE]
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.

## Connecting with multiSubnetFailover

Always specify **multiSubnetFailover=true** when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener of a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] availability group, or a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Failover Cluster Instance.

When the server name in your connection string resolves to more than one IP address, **multiSubnetFailover=true** makes the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] open connections to all of those addresses at the same time and use the first one that answers. Without it, the driver falls back to Transparent Network IP Resolution (TNIR), which is enabled by default. TNIR tries a single address with a 500-millisecond timeout, and opens connections to all of the resolved addresses only on the next attempt. If you also set **transparentNetworkIPResolution=false**, the driver tries a single address with the full **loginTimeout** and never tries the others, so a connection that would succeed against another address fails instead. After a failover moves the database to a replica at a different address, **multiSubnetFailover=true** reaches the new address on the first attempt.

**multiSubnetFailover=true** changes how quickly the client finds the replica that serves the database. It doesn't change how long the server takes to fail over.

**multiSubnetFailover=true** is safe on single-IP targets. When DNS resolves to a single address, the driver makes a single connection attempt and doesn't start any parallel connection threads, so the setting costs nothing when it isn't needed.

 For more information about connection string keywords in the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], see [Setting the Connection Properties](setting-the-connection-properties.md).

 If the security manager isn't installed, the Java Virtual Machine caches virtual IP addresses (VIPs) for a finite period of time, by default, defined by your JDK implementation and the Java properties networkaddress.cache.ttl and networkaddress.cache.negative.ttl. If the JDK security manager is installed, the Java Virtual Machine will cache VIPs, and won't refresh the cache by default. You should set "time-to-live" (networkaddress.cache.ttl) to one day for the Java Virtual Machine cache. If you don't change the default value to one day (or so), the old value won't be purged from the Java Virtual Machine cache when a VIP is added or updated. For more information about networkaddress.cache.ttl and networkaddress.cache.negative.ttl, see [Networking Properties](https://download.oracle.com/javase/6/docs/technotes/guides/net/properties.html).

 Use the following guidelines to connect to a server in an availability group or Failover Cluster Instance:

- Set the **multiSubnetFailover** connection property to **true**.

- To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string. For example, jdbc:sqlserver://VNN1.

- You can use the **instanceName** connection property together with **multiSubnetFailover**. The driver queries SQL Browser at every resolved address to find the port for the instance. If you also specify **portNumber**, the driver uses **portNumber** and doesn't query SQL Browser.

- Connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses fails. The driver treats it as an unsupported configuration and doesn't retry the connection.

- You can't use **multiSubnetFailover** with database mirroring. The driver returns an error when the connection string specifies **failoverPartner**, and also when the server returns a failover partner. Database mirroring is deprecated in all supported versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use Always On availability groups instead.

- Behavior of an application that uses the **multiSubnetFailover** connection property isn't affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication.

- Increase the value of **loginTimeout** to accommodate failover time and reduce application connection retry attempts. For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, **loginTimeout** also has to be large enough to hold the driver's connection retries. For more information, see [Connect to an auto-paused serverless database](connection-resiliency.md#connect-to-an-auto-paused-serverless-database).

If read-only routing isn't in effect, connecting to a secondary replica location in an availability group will fail in the following situations:

- If the secondary replica location isn't configured to accept connections.

- If an application uses **applicationIntent=ReadWrite** (described later in this article) and the secondary replica location is configured for read-only access.

A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.

## Upgrading to Use multi-subnet clusters from database mirroring

If you upgrade a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application that currently uses database mirroring to a multi-subnet scenario, you should remove the **failoverPartner** connection property and replace it with **multiSubnetFailover** set to **true** and replace the server name in the connection string with an availability group listener. If a connection string uses **failoverPartner** and **multiSubnetFailover=true**, the driver will generate an error. However, if a connection string uses **failoverPartner** and **multiSubnetFailover=false** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.

The driver returns an error if you use database mirroring on the primary replica in the AG, and if you use **multiSubnetFailover=true** in the connection string that connects to a primary replica instead of to an availability group listener.

[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]

## Connection pooling

When using the Microsoft JDBC Driver for SQL Server in combination with a connection pooling library, you should consider the following points:

- If you have read-only routing configured and a pool of read-only servers that you want to distribute load over, connection pooling will reduce the number of opportunities for new connections to spread over the target servers.
- To avoid a higher load on any single server in a pool, choose pool options that encourage an even distribution of connections across the pool.
- Make sure your connection pool is configured with a connection lifetime. In the event a read-only replica is unavailable when a read-only connection is made, the configuration should ensure that connection is eventually closed and re-established to a read-only replica when one becomes available again.

## New methods supporting multiSubnetFailover and applicationIntent

The following methods give you programmatic access to the **multiSubnetFailover**, **applicationIntent**, and **transparentNetworkIPResolution** connection string keywords:

- [SQLServerDataSource.getApplicationIntent](reference/getapplicationintent-method-sqlserverdatasource.md)

- [SQLServerDataSource.setApplicationIntent](reference/setapplicationintent-method-sqlserverdatasource.md)

- [SQLServerDataSource.getMultiSubnetFailover](reference/getmultisubnetfailover-method-sqlserverdatasource.md)

- [SQLServerDataSource.setMultiSubnetFailover](reference/setmultisubnetfailover-method-sqlserverdatasource.md)

- [SQLServerDriver.getPropertyInfo](reference/getpropertyinfo-method-sqlserverdriver.md)

- SQLServerDataSource.setTransparentNetworkIPResolution

- SQLServerDataSource.getTransparentNetworkIPResolution

The **getMultiSubnetFailover**, **setMultiSubnetFailover**, **getApplicationIntent**, **setApplicationIntent**, **getTransparentNetworkIPResolution**, and **setTransparentNetworkIPResolution** methods are also added to [SQLServerDataSource Class](reference/sqlserverdatasource-class.md), [SQLServerConnectionPoolDataSource Class](reference/sqlserverconnectionpooldatasource-class.md), and [SQLServerXADataSource Class](reference/sqlserverxadatasource-class.md).

## TLS/SSL certificate validation

An availability group consists of multiple physical servers. [!INCLUDE[jdbc-40](../../includes/jdbc-40-md.md)] added support for **Subject Alternate Name** in TLS/SSL certificates so multiple hosts can be associated with the same certificate. For more information on TLS, see [Understanding encryption support](understanding-ssl-support.md).

## Related content

- [Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)
- [Set the connection properties](setting-the-connection-properties.md)
