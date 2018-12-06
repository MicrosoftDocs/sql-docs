---
title: "Support for High Availability, Disaster Recovery for the Microsoft Drivers for PHP for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 73a80821-d345-4fea-b076-f4aabeb4af3e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Support for High Availability, Disaster Recovery
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic discusses [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support (added in version 3.0) for high-availability, disaster recovery -- [!INCLUDE[ssHADR](../../includes/sshadr_md.md)].  [!INCLUDE[ssHADR](../../includes/sshadr_md.md)] support was added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. For more information about [!INCLUDE[ssHADR](../../includes/sshadr_md.md)], see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
In version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], you can specify the availability group listener of a (high-availability, disaster-recovery) availability group (AG) in the connection property. If a [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] application is connected to an AlwaysOn database that fails over, the original connection is broken and the application must open a new connection to continue work after the failover.  
  
If you are not connecting to an availability group listener, and if multiple IP addresses are associated with a hostname, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will iterate sequentially through all IP addresses associated with DNS entry. This can be time consuming if the first IP address returned by DNS server is not bound to any network interface card (NIC). When connecting to an availability group listener, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] attempts to establish connections to all IP addresses in parallel and if a connection attempt succeeds, the driver will discard any pending connection attempts.  
  
> [!NOTE]  
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
## Connecting With MultiSubnetFailover  
The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance and that the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will try to connect to the database on the primary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses. When **MultiSubnetFailover=true** is specified for a connection, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. This enables faster reconnection after failover of either an AlwaysOn Availability Group or an AlwaysOn Failover Cluster Instance, and is applicable to both single- and multi-subnet Availability Groups and Failover Cluster Instances.  
  
Always specify **MultiSubnetFailover=True** when connecting to a SQL Server 2012 availability group listener or SQL Server 2012 Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instance in SQL Server 2012 and significantly reduces failover time for single and multi-subnet AlwaysOn topologies. During a multi-subnet failover, the client will attempt connections in parallel. During a subnet failover, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] will aggressively retry the TCP connection.  
  
For more information about connection string keywords in [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], see [Connection Options](../../connect/php/connection-options.md).  
  
Specifying **MultiSubnetFailover=true** when connecting to something other than an availability group listener or Failover Cluster Instance may result in a negative performance impact, and is not supported.  
  
Use the following guidelines to connect to a server in an availability group:  
  
-   Use the **MultiSubnetFailover** connection property when connecting to a single subnet or multi-subnet; it will improve performance for both.  
  
-   To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.  
  
-   Connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses will cause a connection failure.  
  
-   Behavior of an application that uses the **MultiSubnetFailover** connection property is not affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication.  
  
-   Increase the value of **loginTimeout** to accommodate for failover time and reduce application connection retry attempts.  
  
-   Distributed transactions are not supported.  
  
If read-only routing is not in effect, connecting to a secondary replica location in an availability group will fail in the following situations:  
  
- If the secondary replica location is not configured to accept connections.  
  
- If an application uses **ApplicationIntent=ReadWrite** (discussed below) and the secondary replica location is configured for read-only access.  
  
A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  

## Transparent Network IP Resolution (TNIR)

Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. It affects the connection sequence of the driver when the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname. Together with MultiSubnetFailover they provide the following four connection sequences: 

- TNIR Enabled & MultiSubnetFailover Disabled: One IP is attempted, followed by all IPs in parallel
- TNIR Enabled & MultiSubnetFailover Enabled: All IPs are attempted in parallel
- TNIR Disabled & MultiSubnetFailover Disabled: All IPs are attempted one after another
- TNIR Disabled & MultiSubnetFailover Enabled: All IPs are attempted in parallel

TNIR is enabled by default, and MultiSubnetFailover is Disabled by default.

This is an example of enabling both TNIR and MultiSubnetFailover using the PDO_SQLSRV driver:

```
<?php
$serverName = "yourservername";
$username = "yourusername";
$password = "yourpassword";
$connectionString = "sqlsrv:Server=$serverName; TransparentNetworkIPResolution=Enabled; MultiSubnetFailover=yes";
try {
    $conn = new PDO($connectionString, $username, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
    // your code 
    // more of your code
    // when done, close the connection
    unset($conn);
} catch(PDOException $e) {
    print_r($e->errorInfo);
}
?>
```

## Upgrading to Use Multi-Subnet Clusters from Database Mirroring  
A connection error will occur if the **MultiSubnetFailover** and **Failover_Partner** connection keywords are present in the connection string. An error will also occur if **MultiSubnetFailover** is used and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns a failover partner response indicating it is part of a database mirroring pair.  
  
If you upgrade a [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] application that currently uses database mirroring to a multi-subnet scenario, you should remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **Yes** and replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=true**, the driver will generate an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=false** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.  
  
The driver will return an error if database mirroring is used on the primary database in the AG, and if **MultiSubnetFailover=true** is used in the connection string that connects to a primary database instead of to an availability group listener.  


[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]


## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)  
  
