---
title: "Read-scale Availability Groups | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 9
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Read-scale availability groups
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

In addition to bringing together the best in class HA capabilities for SQL Server, an availability group is a comprehensive solution offering integrated scaling solutions as well. In a typical database application, there are multiple clients running various types of workloads and sometimes that can lead to bottlenecks due to resource constraints. You can free up resources and achieve higher throughput for the OLTP workload or deliver higher performance and scale on read-only workloads. This can be achieved by leveraging the fastest replication technology for SQL Server - create a group of replicated databases to offload reporting and analytics workloads to the read-only replicas. 

With Availability Groups, one or more secondary replicas can be configured to support read-only access to secondary databases.

The client applications running analytics or reporting workloads can directly connect to the secondary databases. Or the customer can setup a read only routing list and connect to the primary that will forward the connection request to each of the secondary replicas from the routing list in a round robin fashion.

## Read-scale availability groups without cluster

In [!INCLUDE[sssql15-md](..\..\..\includes\sssql15-md.md)] and before, all availability groups required a cluster. The cluster provided business continuity - high availability and disaster recovery (HADR). In addition, secondary replicas could be configured for read operations. Configuring and operating a cluster implied a lot of operational overhead, if HA was not the goal. SQL Server 2017 introduces read-scale availability groups without a cluster. 

If the business requirement is to conserve resources for mission-critical workloads running on the primary, users can now use read-only routing or directly connect to readable secondary replicas, without depending on integration with any clustering technology. These new capabilities are available for SQL Server 2017 running on both Windows and Linux platforms.

>[!IMPORTANT]
>This is not a high-availability setup. There is no infrastructure to monitor and coordinate failure detection and automatic failover. For users who need HADR capabilities, use a cluster manager (WSFC on Windows or Pacemaker on Linux). 

## Use distributed availability groups for geographic read-scale

Geographically separated solutions can implement read-scale solutions with distributed availability groups. This allows offloading read workloads from the primary replica to readable secondary replicas to sites that are closer to the source of the read workloads. This not only reduces the utilization of resources on the primary, but also helps with read throughput by reducing network latency and leveraging dedicated resources.

A single distributed availability group can have up to 17 readable secondary replicas. For increased scaling capacity, daisy chain multiple availability groups and increase the number of readable replicas even further. You can also deploy two distributed availability groups from the same availability group for low latency reads in geographically dispersed environments.




## Next Steps 

[Configure read-scale availability group on Linux](../../../linux/sql-server-linux-availability-group-configure-rs.md)

## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
