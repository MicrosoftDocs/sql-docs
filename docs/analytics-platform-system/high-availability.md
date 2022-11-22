---
title: High availability
description: Learn how Analytics Platform System (APS) is architected for high availability.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Analytics Platform System high availability
Learn how Analytics Platform System (APS) is architected for high availability.  
  
## High Availability Architecture  
![Appliance architecture](media/appliance-architecture.png "Appliance architecture")  
  
## Network  
For network availability, the APS appliance has two InfiniBand networks. If one of the InfiniBand networks goes down, the other one is still available. Also, Active Directory has replicated domain controllers to resolve incoming requests to the correct InfiniBand network.  
  
For more information, see [Configure InfiniBand network adapters](configure-infiniband-network-adapters.md).  
  
## Storage  
To keep data safe, APS uses RAID 1 mirroring to maintain two copies of all user data. When a disk fails, the hardware system rebuilds the data onto a spare disk and sets an alert that there is a disk failure.  
  
To keep data available online, APS uses Windows Storage Spaces and clustered shared volumes to manage the user data disks in the direct attached storage. There is one storage pool per Data Scale unit organized into Cluster Shared Volumes which are available to the Compute node hosts through mount points.  
  
To ensure the storage pool stays online, each host in the Data Scale unit has an ISCSI virtual machine that does not failover. This architecture is important because if a host fails, the data is still accessible through the other host(s) in the Data Scale unit.  
  
## Hosts  
For host availability, all of the hosts are configured into a Windows Failover Cluster. Every rack has a passive host. Optionally the first rack, which controls SQL Server Parallel Data Warehouse (PDW) and the appliance fabric, can have a second passive host. If a host fails, virtual machines that are configured for failover, will fail over to an available passive host.  
  
## PDW nodes and appliance fabric  
For high availability of the PDW nodes and the appliance fabric, APS uses virtualization. Each of the PDW and appliance fabric components run in a virtual machine.  
  
Each virtual machine is defined as a role in the Windows failover cluster. When a virtual machine fails, the cluster restarts it on an available passive host. The virtual machines are deployed using System Center Virtual Machine Manager. When a failover occurs, the virtual machine running on the passive host is still able to access its user data through the InfiniBand network.  
  
The Control node and Compute node virtual machines are each configured as a single-node cluster. The single-node cluster manages the InfiniBand networks as a cluster resource to ensure the cluster is always using the active InfiniBand IP. The single-node cluster manages the PDW processes that run within the virtual machine. For example, the single-node cluster has SQL Server and Data Movement Service (DMS) as resources so that it can start them in the proper order. The Control node VM also controls the start order for the other VMs that run on the orchestration host.  
  
