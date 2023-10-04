---
title: Support for High Availability, disaster recovery
description: This article discusses Microsoft JDBC Driver for SQL Server support for high-availability, disaster recovery (Always On Availability Groups).
author: David-Engel
ms.author: v-davidengel
ms.date: 01/26/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# JDBC driver support for High Availability, disaster recovery

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  This article discusses [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] support for high-availability, disaster recovery: [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. For more information about [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], see [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Books Online.

 Beginning in version 4.0 of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you can specify the availability group listener of a (high-availability, disaster-recovery) availability group (AG) in the connection property. If a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application is connected to an Always On database that fails over, the original connection is broken, and the application must open a new connection to continue work after the failover. The following [connection properties](setting-the-connection-properties.md) were added in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)]:

- **multiSubnetFailover**

- **applicationIntent**

Specify multiSubnetFailover=true when connecting to the availability group listener of an availability group or a Failover Cluster Instance.

>[!Note]
>**multiSubnetFailover** is false by default. Use **applicationIntent** to declare the application workload type. For more details, see the sections below.

Beginning in version 6.0 of the Microsoft JDBC Driver for SQL Server, a new connection property **transparentNetworkIPResolution** (TNIR) is added for transparent connection to Always On availability groups or to a server that has multiple IP addresses associated. When **transparentNetworkIPResolution** is true, the driver attempts to connect to the first IP address available. If the first attempt fails, the driver tries to connect to all IP addresses in parallel until the timeout expires, discarding any pending connection attempts when one of them succeeds.

Note:

- transparentNetworkIPResolution is true by default
- transparentNetworkIPResolution is ignored if multiSubnetFailover is true
- transparentNetworkIPResolution is ignored if database mirroring is used
- transparentNetworkIPResolution is ignored if there are more than 64 IP addresses
- When transparentNetworkIPResolution is true, the first connection attempt uses a timeout value of 500 ms. Rest of the connection attempts follow the same logic as in the multiSubnetFailover feature.

> [!NOTE]
> If you are using Microsoft JDBC Driver 4.2 (or lower) for SQL Server and if **multiSubnetFailover** is false, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] attempts to connect to the first IP address. If the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] cannot establish a connection with first IP address, the connection fails. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will not attempt to connect to any subsequent IP address associated with the server.

> [!NOTE]
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.

## Connecting with multiSubnetFailover

 Always specify **multiSubnetFailover=true** when connecting to the availability group listener of a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] availability group or a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Failover Cluster Instance. **multiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instances in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and will significantly reduce failover time for single and multi-subnet Always On topologies. During a multi-subnet failover, the client will attempt connections in parallel. During a subnet failover, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will aggressively retry the TCP connection.

 The **multiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance and that the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will try to connect to the database on the primary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses. When **MultiSubnetFailover=true** is specified for a connection, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. This behavior enables faster reconnection after failover of either an Always On Availability Group or an Always On Failover Cluster Instance, and applies to both single- and multi-subnet Availability Groups and Failover Cluster Instances.

 For more information about connection string keywords in the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], see [Setting the Connection Properties](setting-the-connection-properties.md).

 Specifying **multiSubnetFailover=true** when connecting to something other than an availability group listener or Failover Cluster Instance may result in a negative performance impact, and isn't supported.

 If the security manager isn't installed, the Java Virtual Machine caches virtual IP addresses (VIPs) for a finite period of time, by default, defined by your JDK implementation and the Java properties networkaddress.cache.ttl and networkaddress.cache.negative.ttl. If the JDK security manager is installed, the Java Virtual Machine will cache VIPs, and won't refresh the cache by default. You should set "time-to-live" (networkaddress.cache.ttl) to one day for the Java Virtual Machine cache. If you don't change the default value to one day (or so), the old value won't be purged from the Java Virtual Machine cache when a VIP is added or updated. For more information about networkaddress.cache.ttl and networkaddress.cache.negative.ttl, see [https://download.oracle.com/javase/6/docs/technotes/guides/net/properties.html](https://download.oracle.com/javase/6/docs/technotes/guides/net/properties.html).

 Use the following guidelines to connect to a server in an availability group or Failover Cluster Instance:

- The driver will generate an error if the **instanceName** connection property is used in the same connection string as the **multiSubnetFailover** connection property. This error reflects the fact that SQL Browser isn't used in an availability group. However, if the **portNumber** connection property is also specified, the driver will ignore **instanceName** and use **portNumber**.

- Use the **multiSubnetFailover** connection property when connecting to a single subnet or multi-subnet, it will improve performance for both.

- To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string. For example, jdbc:sqlserver://VNN1.

- Connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses will cause a connection failure.

- Behavior of an application that uses the **multiSubnetFailover** connection property isn't affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication.

- Increase the value of **loginTimeout** to accommodate for failover time and reduce application connection retry attempts.

If read-only routing isn't in effect, connecting to a secondary replica location in an availability group will fail in the following situations:

- If the secondary replica location isn't configured to accept connections.

- If an application uses **applicationIntent=ReadWrite** (discussed below) and the secondary replica location is configured for read-only access.

A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.

## Upgrading to Use multi-subnet clusters from database mirroring

If you upgrade a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] application that currently uses database mirroring to a multi-subnet scenario, you should remove the **failoverPartner** connection property and replace it with **multiSubnetFailover** set to **true** and replace the server name in the connection string with an availability group listener. If a connection string uses **failoverPartner** and **multiSubnetFailover=true**, the driver will generate an error. However, if a connection string uses **failoverPartner** and **multiSubnetFailover=false** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.

The driver will return an error if database mirroring is used on the primary database in the AG, and if **multiSubnetFailover=true** is used in the connection string that connects to a primary database instead of to an availability group listener.

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

An availability group consists of multiple physical servers. [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)] added support for **Subject Alternate Name** in TLS/SSL certificates so multiple hosts can be associated with the same certificate. For more information on TLS, see [Understanding encryption support](understanding-ssl-support.md).

## See also

[Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)  
[Setting the connection properties](setting-the-connection-properties.md)  
