---
title: "Support for High Availability, Disaster Recovery for the Microsoft Drivers for PHP for SQL Server"
description: "Support for High Availability, Disaster Recovery for the Microsoft Drivers for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "07/31/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Support for High Availability, Disaster Recovery
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic discusses [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support (added in version 3.0) for high-availability, disaster recovery.

Starting with version 3.0 of the Microsoft Drivers for PHP for SQL Server, you can specify the availability group listener of a high-availability, disaster-recovery availability group or a failover cluster instance as the server in the connection string.

The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance and that the driver will try to connect to the database on the primary SQL Server instance by trying to connect to all the IP addresses. Always specify **MultiSubnetFailover=True** when connecting to a SQL Server availability group listener or SQL Server Failover Cluster Instance. If the application is connected to an Always On database that fails over, the original connection is broken and the application must open a new connection to continue working after the failover.

Full details on Always On availability groups can be found at the [High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md) Docs page.

## Transparent Network IP Resolution (TNIR)

Transparent Network IP Resolution (TNIR) is a revision of the existing **MultiSubnetFailover** feature. It affects the connection sequence of the driver when the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname. The corresponding connection option is **TransparentNetworkIPResolution**. Together with **MultiSubnetFailover** it provides the following four connection sequences: 

- TNIR Enabled & **MultiSubnetFailover** Disabled: One IP is attempted, followed by all IPs in parallel
- TNIR Enabled & **MultiSubnetFailover** Enabled: All IPs are attempted in parallel
- TNIR Disabled & **MultiSubnetFailover** Disabled: All IPs are attempted one after another
- TNIR Disabled & **MultiSubnetFailover** Enabled: All IPs are attempted in parallel

TNIR is enabled by default, and **MultiSubnetFailover** is Disabled by default.

This is an example of enabling both TNIR and **MultiSubnetFailover** using the PDO_SQLSRV driver:

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
A connection error will occur if the **MultiSubnetFailover** and **Failover_Partner** connection keywords are present in the connection string. An error will also occur if **MultiSubnetFailover** is used and the SQL Server returns a failover partner response indicating it is part of a database mirroring pair.  
  
When upgrading a PHP application that currently uses database mirroring to a multi-subnet scenario, remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **True** and replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=true**, the driver will generate an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=false** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.  
  
The driver will return an error if database mirroring is used on the primary database in the AG, and if **MultiSubnetFailover=true** is used in the connection string that connects to a primary database instead of to an availability group listener.  

[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]


## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)  
