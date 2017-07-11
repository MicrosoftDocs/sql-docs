---
title: "PHP Driver for SQL Server High Availability, Disaster Recovery | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 73a80821-d345-4fea-b076-f4aabeb4af3e
caps.latest.revision: 15
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PHP Driver for SQL Server Support for High Availability, Disaster Recovery
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic discusses [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support (added in version 3.0) for high-availability, disaster recovery -- [!INCLUDE[ssHADR](../../includes/sshadr_md.md)].  [!INCLUDE[ssHADR](../../includes/sshadr_md.md)] support was added in [!INCLUDE[ssSQL11](../../includes/sssql11_md.md)]. For more information about [!INCLUDE[ssHADR](../../includes/sshadr_md.md)], see [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Books Online.  
  
In version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], you can specify the availability group listener of a (high-availability, disaster-recovery) availability group (AG) in the connection property. If a [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] application is connected to an AlwaysOn database that fails over, the original connection is broken and the application must open a new connection to continue work after the failover.  
  
If you are not connecting to an availability group listener, and if multiple IP addresses are associated with a hostname, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will iterate sequentially through all IP addresses associated with DNS entry. This can be time consuming if the first IP address returned by DNS server is not bound to any network interface card (NIC). When connecting to an availability group listener, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] attempts to establish connections to all IP addresses in parallel and if a connection attempt succeeds, the driver will discard any pending connection attempts.  
  
> [!NOTE]  
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
## Connecting With MultiSubnetFailover  
The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance and that the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will try to connect to the database on the primary [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance by trying to connect to all the IP addresses. When **MultiSubnetFailover=true** is specified for a connection, the client retries TCP connection attempts faster than the operating system’s default TCP retransmit intervals. This enables faster reconnection after failover of either an AlwaysOn Availability Group or an AlwaysOn Failover Cluster Instance, and is applicable to both single- and multi-subnet Availability Groups and Failover Cluster Instances.  
  
Always specify **MultiSubnetFailover=True** when connecting to a SQL Server 2012 availability group listener or SQL Server 2012 Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instance in SQL Server 2012 and will significantly reduce failover time for single and multi-subnet AlwaysOn topologies. During a multi-subnet failover, the client will attempt connections in parallel. During a subnet failover, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will aggressively retry the TCP connection.  
  
For more information about connection string keywords in [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], see [Connection Options](../../connect/php/connection-options.md).  
  
Specifying **MultiSubnetFailover=true** when connecting to something other than a availability group listener or Failover Cluster Instance may result in a negative performance impact, and is not supported.  
  
Use the following guidelines to connect to a server in an availability group:  
  
-   Use the **MultiSubnetFailover** connection property when connecting to a single subnet or multi-subnet; it will improve performance for both.  
  
-   To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.  
  
-   Connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance configured with more than 64 IP addresses will cause a connection failure.  
  
-   Behavior of an application that uses the **MultiSubnetFailover** connection property is not affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Authentication, Kerberos Authentication, or Windows Authentication.  
  
-   Increase the value of **loginTimeout** to accommodate for failover time and reduce application connection retry attempts.  
  
-   Distributed transactions are not supported.  
  
If read-only routing is not in effect, connecting to a secondary replica location in an availability group will fail in the following situations:  
  
1.  If the secondary replica location is not configured to accept connections.  
  
2.  If an application uses **ApplicationIntent=ReadWrite** (discussed below) and the secondary replica location is configured for read-only access.  
  
A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  
  
## Upgrading to Use Multi-Subnet Clusters from Database Mirroring  
A connection error will occur if the **MultiSubnetFailover** and **Failover_Partner** connection keywords are present in the connection string. An error will also occur if **MultiSubnetFailover** is used and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] returns a failover partner response indicating it is part of a database mirroring pair.  
  
If you upgrade a [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] application that currently uses database mirroring to a multi-subnet scenario, you should remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **Yes** and replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=true**, the driver will generate an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=false** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.  
  
The driver will return an error if database mirroring is used on the primary database in the AG, and if **MultiSubnetFailover=true** is used in the connection string that connects to a primary database instead of to an availability group listener.  
  
## Specifying Application Intent  
When **ApplicationIntent=ReadOnly**, the client requests a read workload when connecting to an AlwaysOn enabled database. The server will enforce the intent at connection time and during a USE database statement but only to an Always On enabled database.  
  
The **ApplicationIntent** keyword does not work with legacy, read-only databases.  
  
A database can allow or disallow read workloads on the targeted AlwaysOn database. (This is done with the **ALLOW_CONNECTIONS** clause of the **PRIMARY_ROLE** and **SECONDARY_ROLE**[!INCLUDE[tsql](../../includes/tsql_md.md)] statements.)  
  
The **ApplicationIntent** keyword is used to enable read-only routing.  
  
## Read-Only Routing  
Read-only routing is a feature that can ensure the availability of a read only replica of a database. To enable read-only routing:  
  
1.  You must connect to an Always On Availability Group availability group listener.  
  
2.  The **ApplicationIntent** connection string keyword must be set to **ReadOnly**.  
  
3.  The Availability Group must be configured by the database administrator to enable read-only routing.  
  
It is possible that multiple connections using read-only routing will not all connect to the same read-only replica. Changes in database synchronization or changes in the server's routing configuration can result in client connections to different read-only replicas. To ensure that all read-only requests connect to the same read-only replica, do not pass an availability group listener to the **Server** connection string keyword. Instead, specify the name of the read-only instance.  
  
Read-only routing may take longer than connecting to the primary because read only routing first connects to the primary and then looks for the best available readable secondary. Because of this, you should increase your login timeout.  
  
## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)  
  
