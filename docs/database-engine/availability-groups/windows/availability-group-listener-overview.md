---
title: "What is an availability group listener?"
description: "An overview of the Always On availability group listener and how it functions to direct traffic automatically to the intended server. "
ms.custom: "seodec18"
ms.date: "02/27/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], listeners"
  - "read-only routing"
  - "read-intent connections [AlwaysOn Availability Groups]"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], read-only routing"
  - "Availability Groups [SQL Server], client connectivity"
ms.assetid: 76fb3eca-6b08-4610-8d79-64019dd56c44
author: MashaMSFT
ms.author: mathoma
---
# What is an availability group listener?  
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

An availability group listener is a virtual network name (VNN) that clients can connect to in order to access a database in a primary or secondary replica of an Always On availability group. A listener allows a client to connect to a replica without having to know the physical instance name of the SQL Server. Since the listener routes traffic, the client connection string does not need to be modified after a failover occurs. 

An availability group listener consists of a Domain Name System (DNS) listener name, listener port designation, and one or more IP addresses. Only the TCP protocol is supported by availability group listener.  The DNS name of the listener must be unique in the domain and in NetBIOS.  When you create a  listener, it becomes a resource in a cluster with an associated virtual network name (VNN), virtual IP (VIP), and availability group dependency. A client uses DNS to resolve the VNN into multiple IP addresses and then tries to connect to each address, until a connection request succeeds or until the connection requests time out.  
  
If read-only routing is configured for one or more [readable secondary replicas](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md), read-intent client connections to the listener are  automatically redirected to a readable secondary replica. 
  
This article provides an overview of an availability group listener. You can also [configure the listener](create-or-configure-an-availability-group-listener-sql-server.md), and then learn how to [connect to the listener](listeners-client-connectivity-application-failover.md).
  
  
##  <a name="AGlConfig"></a> Listener parameters  

 An availability group listener uses the following:
  
 **A unique DNS name**  
 This is also known as a Virtual Network Name (VNN). Active Directory naming rules for DNS host names apply. For more information, see the [Naming conventions in Active Directory for computers, domains, sites, and OUs](https://support.microsoft.com/kb/909264) KB article.  
  
**One or more Virtual IP addresses (VIPs)**  
 VIPs are configured for one or more subnets to which the availability group can fail over.  
  
**IP address configuration**  
 For a given availability group listener, the IP address can use either Dynamic Host Configuration Protocol (DHCP), or one or more static IP address. Using DHCP can cause connectivity delays during failover, and so it is not recommended for use in production environments. Availability groups that extend across multiple subnets, or use hybrid network configurations, must use a static IP address. 
 
  
##  <a name="SelectListenerPort"></a> Listener port 
 When configuring an availability group listener, you must designate a port.  You can configure the default port to 1433 in order to allow for simplicity of the client connection strings. If using 1433, you do not need to designate a port number in a connection string. Also, since each availability group listener will have a separate virtual network name, each availability group listener configured on a single WSFC can be configured to reference the same default port of 1433.  
  
 You can also designate a non-standard listener port; however this means that you will also need to explicitly specify a target port in your connection string whenever connecting to the availability group listener.  You will also need to open permission on the firewall for the non-standard port.  
  
 If you use the default port of 1433 for availability group listener VNNs, you will still need to ensure that no other services on the cluster node are using this port; otherwise this would cause a port conflict.  
  
 If one of the instances of SQL Server is already listening on TCP port 1433 via the instance listener and there are no other services (including additional instances of SQL Server) on the computer listening on port 1433, this will not cause a port conflict with the availability group listener.  This is because the availability group listener can share the same TCP port inside the same service process.  However multiple instances of SQL Server (side-by-side) should not be configured to listen on the same port.  
  
  
##  <a name="CCBehaviorOnFailover"></a> Behavior of client connections on failover  

 When an availability group failover occurs, existing persistent connections to the availability group are terminated and the client must establish a new connection in order to continue working with the same primary database or read-only secondary database.  While a failover is occurring on the server side, connectivity to the availability group may fail, forcing the client application to retry connecting until the primary is brought fully back online.  
  
 If the availability group comes back online during a client application's connection attempt but before the connect timeout period, the client driver may successfully connect during one of its internal retry attempts and no error will be surfaced to the application in this case.  


## Next steps

Now that you're familiar with how an availability group listener functions, [create your listener](create-or-configure-an-availability-group-listener-sql-server.md) and then configure your application to [connect to the listener](listeners-client-connectivity-application-failover.md). You can also review various [availability group monitoring strategies](monitoring-of-availability-groups-sql-server.md) to ensure the health of your availability group. 

For more information about availability groups, see the  [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md). 
  

  
  
