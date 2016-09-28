---
title: "PDW High Availability (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5ab245e9-0316-4d25-a626-4745ce856925
caps.latest.revision: 9
author: BarbKess
---
# PDW High Availability (Analytics Platform System)
Describes how PDW and the Analytics Platform System appliance fabric are architected for high availability.  
  
## High Availability Architecture  
![Appliance Architecture](../architecture/media/SQL_Server_PDW_HW_ApplianceArchitectureV2.png "SQL_Server_PDW_HW_ApplianceArchitectureV2")  
  
## Network  
For network availability, the appliance has two InfiniBand networks. If one of the InfiniBand networks goes down, the other one is still available. Also, Active Directory has replicated domain controllers to resolve incoming requests to the correct InfiniBand network.  
  
For more information, see [Configure InfiniBand Network Adapters &#40;SQL Server PDW&#41;](../sqlpdw/configure-infiniband-network-adapters-sql-server-pdw.md).  
  
## Storage  
To keep data safe, Analytics Platform System uses RAID 1 mirroring to maintain two copies of all user data. When a disk fails, the hardware system rebuilds the data onto a spare disk and sets an alert that there is a disk failure.  
  
To keep data available online, Analytics Platform System uses Windows Storage Spaces and clustered shared volumes to manage the user data disks in the direct attached storage. There is one storage pool per Data Scale Unit organized into Cluster Shared Volumes which are available to the Compute node hosts through mount points.  
  
To ensure the storage pool stays online, each host in the Data Scale Unit has an ISCSI virtual machine that does not failover. This architecture is important because if a host fails, the data is still accessible through the other host(s) in the Data Scale Unit.  
  
## Hosts  
For host availability, all of the hosts are configured into a Windows Failover Cluster. Every rack has a passive host. Optionally the first rack, which controls PDW and the appliance fabric, can have a second passive host. If a host fails, virtual machines that are configured for failover, will fail over to an available passive host.  
  
## PDW Nodes and Appliance Fabric  
For high availability of the PDW nodes and the appliance fabric, Analytics Platform System uses virtualization. Each of the PDW and appliance fabric components run in a virtual machine.  
  
Each virtual machine is defined as a role in the Windows failover cluster. When a virtual machine fails, the cluster restarts it on an available passive host. The virtual machines are deployed using System Center Virtual Machine Manager. When a failover occurs, the virtual machine running on the passive host is still able to access its user data through the InfiniBand network.  
  
More details, the Control node and Compute node virtual machines are each configured as a single-node cluster. The single-node cluster manages the InfiniBand networks as a cluster resource to ensure the cluster is always using the active InfiniBand IP. The single-node cluster manages the PDW processes that run within the virtual machine. For example, the single-node cluster has SQL Server and Data Movement Service (DMS) as resources so that it can start them in the proper order. The Control node VM also controls the start order for the other VMs that run on the orchestration host.  
  
