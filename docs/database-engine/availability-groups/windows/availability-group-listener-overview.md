---
title: What is an Availability Group Listener?
description: An overview of the Always On availability group listener and how it functions to direct traffic automatically to the intended server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/17/2026
ms.service: sql
ms.subservice: availability-groups
ms.topic: overview
ms.custom:
  - intro-overview
helpviewer_keywords:
  - "Availability Groups [SQL Server], listeners"
  - "read-only routing"
  - "read-intent connections [AlwaysOn Availability Groups]"
  - "read-intent connections [Always On Availability Groups]"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], read-only routing"
  - "Availability Groups [SQL Server], client connectivity"
---
# What is an availability group listener?

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

An availability group listener is a virtual network name (VNN) that clients can connect to in order to access a database in a primary or secondary replica of an Always On availability group. A listener allows a client to connect to a replica without having to know the physical instance name of the SQL Server. Since the listener routes traffic, the client connection string doesn't need to be modified after a failover occurs.

An availability group listener consists of a Domain Name System (DNS) listener name, listener port designation, and one or more IP addresses. Only the TCP protocol is supported by availability group listener. The DNS name of the listener must be unique in the domain and in NetBIOS. When you create a listener, it becomes a resource in a cluster with an associated virtual network name (VNN), virtual IP (VIP), and availability group dependency. A client uses DNS to resolve the VNN into multiple IP addresses and then tries to connect to each address, until a connection request succeeds or until the connection requests time out.

If read-only routing is configured for one or more [readable secondary replicas](active-secondaries-readable-secondary-replicas-always-on-availability-groups.md), read-intent client connections to the listener are automatically redirected to a readable secondary replica.

This article provides an overview of an availability group listener. You can also [configure the listener](create-or-configure-an-availability-group-listener-sql-server.md), and then learn how to [connect to the listener](listeners-client-connectivity-application-failover.md).

<a id="AGlConfig"></a>

## Listener parameters

An availability group listener uses the following parameters:

### A unique DNS name

This name is also known as a Virtual Network Name (VNN). Active Directory naming rules for DNS host names apply. For more information, see the [Naming conventions in Active Directory for computers, domains, sites, and OUs](/troubleshoot/windows-server/active-directory/naming-conventions-for-computer-domain-site-ou) KB article.

### One or more virtual IP addresses

Configure virtual IP addresses for one or more subnets to which the availability group can fail over.

### IP address configuration

For a given availability group listener, the IP address can use either Dynamic Host Configuration Protocol (DHCP) or one or more static IP addresses. Using DHCP can cause connectivity delays during failover, and so it's not recommended for use in production environments. Availability groups that extend across multiple subnets or use hybrid network configurations must use a static IP address.

<a id="SelectListenerPort"></a>

## Listener port

When you configure an availability group listener, you must designate a port through SSMS. You can set the default port to 1433 to keep client connection strings basic. If you use 1433, you don't need to include a port number in a connection string for your application. Also, since each availability group listener has a separate virtual network name, each availability group listener configured on a single WSFC can reference the same default port of 1433.

If you use the default port of 1433 for availability group listener VNNs, ensure that no other services on the cluster node use this port. The usage of the port by multiple processes can lead to a conflict.

If one of the instances of SQL Server is already listening on TCP port 1433 through the instance listener and there are no other services (including additional instances of SQL Server) on the computer listening on port 1433, this configuration doesn't cause a port conflict with the availability group listener. This configuration works because the availability group listener can share the same TCP port inside the same process. However, you can't configure multiple instances of SQL Server (side-by-side) to listen on the same port because one of them fails to listen for connections.

You can also designate a non-standard availability group listener port. However, you need to explicitly use the target port in your application connection string when connecting to a listener. You also need to open permission on the firewall for this port.

You can connect to the listener by using the name and port (if not 1433). The port can be either the listener port or the underlying SQL Server port that it's configured to listen on.

### Set up

The following examples demonstrate some of the functionality of the listener:

- IP that SQL Server is listening on: 192.168.20.2
- Port that SQL Server is listening on: 50254
- Listener IP that was configured: 192.168.20.15
- Listener name that was configured: aglistener19
- Listener port that was configured: 50123

1. Connect to the listener by using the IP address and port. This connection is successful.

   ```console
   sqlcmd -S 192.168.20.15,50123
   1>
   ```

1. Connect to the listener by using only the name, with no port. This connection fails because a non-default port is used. You must specify that port.

   ```console
   sqlcmd -S aglistener19
   ```

1. Connect to the listener by using the listener name and configured port. This connection succeeds.

   ```console
   sqlcmd -S aglistener19,50123
   1>
   ```

1. Finally, connect to the listener and the SQL Server port. In this case you're using the port SQL Server is listening on, not the listener port. This connection also succeeds.

   ```console
   sqlcmd -S aglistener19,50254
   1>
   ```

<a id="CCBehaviorOnFailover"></a>

## Behavior of client connections on failover

When an availability group failover occurs, the process terminates existing persistent connections to the availability group. The client must establish a new connection to continue working with the same primary database or read-only secondary database. While a failover is occurring on the server side, connectivity to the availability group might fail. This failure forces the client application to retry connecting until the primary is fully back online.

If the availability group comes back online during a client application's connection attempt but before the connect timeout period, the client driver might successfully connect during one of its internal retry attempts. In this case, no error surfaces to the application.

## Related content

- [Configure a listener for an Always On availability group](create-or-configure-an-availability-group-listener-sql-server.md)
- [Connect to an Always On availability group listener](listeners-client-connectivity-application-failover.md)
- [Tools to monitor Always On availability groups](monitoring-of-availability-groups-sql-server.md)
- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
