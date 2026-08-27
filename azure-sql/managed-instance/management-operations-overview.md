---
title: Management Operations Overview
titleSuffix: Azure SQL Managed Instance
description: Learn about Azure SQL Managed Instance management operations.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, randolphwest
ms.date: 06/18/2025
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: overview
ms.custom:
  - ignite-2025
---

# Overview of management operations in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the different operations that occur when managing [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). Management operations are operations that are performed on the backend when you create, update, or delete an instance.

For a detailed description of the steps and estimated duration of each management operation, review [Management operations duration](management-operations-duration.md).

## What are management operations?

Managing Azure SQL Managed Instance involves the following operations: 

- **Create**: The operations that occur when you first create a new SQL managed instance. This includes creating the underlying virtual machine (VM) group, and deploying the SQL Database Engine process.
- **Update**: Operations that occur when you change the properties of an existing SQL managed instance, such as scaling compute or storage, changing the service tier, or updating the instance configuration. Making updates often involves resizing the VM group, as well as seeding data, and then failing over, to a new SQL Database Engine process.
- **Delete**: Operations that occur when you delete an existing SQL managed instance, including the cleanup of resources such as the VM group associated with the instance.

For a detailed description of the steps and estimated duration of each management operation, review [Management operations duration](management-operations-duration.md).

SQL Managed Instance management operations are accomplished through the following underlying processes:

- [Virtual machine (VM) group operations](#vm-group-operations): Operations that involve creating and managing the underlying VM group that host the SQL managed instance. This includes resizing the VM group, creating new VM groups, and managing the virtual machines within those groups.
- [Seeding](#seeding): The initialization and synchronization of data across SQL Database Engine processes, usually to prepare for a failover. 
- [Failover](#failover): Operations involved in failing traffic over to another SQL Database Engine process, either in the same, or in a new VM group. 

## VM group operations

To support [deployments within Azure virtual networks](/azure/virtual-network/virtual-network-for-azure-services) and provide isolation and security for customers, SQL Managed Instance relies on [virtual clusters](virtual-cluster-architecture.md). The virtual cluster represents a dedicated set of isolated virtual machines (VMs) deployed inside your virtual network subnet and organized within *VM groups*. Essentially, every SQL managed instance deployed to an empty subnet results in a new virtual cluster which builds the very first VM group. 

Subsequent management operations on SQL managed instances can affect the underlying [VM groups](virtual-cluster-architecture.md#role-in-management-operations). Changes that affect the underlying VM groups might affect the duration of management operations, as deploying more virtual machines to the virtual cluster comes with an overhead that you need to consider when you plan new deployments or updates to existing instances.

For detailed information about the virtual cluster architecture, see [Virtual cluster architecture](virtual-cluster-architecture.md).

## Seeding

Seeding plays a critical role in the operation of Azure SQL Managed Instance, particularly during the setup and replication of databases. Seeding is the process that initializes and synchronizes data across SQL Database Engine processes, which is a crucial part of instance management. While often the most time-consuming step in lengthy but successful operations, seeding serves as a cornerstone to establishing a healthy and functional SQL managed instance environment. 

For an estimated duration of seeding operations, see [Management operations duration](management-operations-duration.md#seeding-duration).

The seeding process typically involves the following stages, regardless of the service tier: 
- **Initialization**: The system identifies the source and destination database and starts a number of tasks that prepare the SQL Database Engine processes for data transfer.  
- **Data Transfer**: Actual data packages are transferred from the source to the target SQL Database Engine process, which includes a full or partial copy of the database, depending on the scenario. 
- **Synchronization**: Once initial data transfer completes, the system synchronizes any subsequent updates or changes through the replication of transaction log blocks to ensure data integrity. 
- **Validation and finalization**: The process is finalized, and the target SQL Database Engine process is validated to confirm successful replication and seeding. Failover occurs so that traffic is routed to the new SQL Database Engine process. 

There is no data seeding in the **General Purpose** service tier except when you change the service tier to the **Business Critical** service tier. Management operations in the **General Purpose** service tier involve detaching remote storage from the old SQL Database Engine process and attaching it to the new SQL Database Engine process.

Conversely, the **Business Critical** service tier, designed for high-performance workloads, requires local storage and the codependency of compute and storage layers. Consequently, almost every operation and scenario in this service tier necessitates seeding to ensure data availability and consistency.

Whether or not seeding is triggered depends on the particular scenario and service tier, such as: 

- **General Purpose and [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md)** service tiers:  
   - *Changing to the Business Critical service tier* – data must be transferred from the remote storage to the local storage used in the General Purpose service tier.
   - *Enabling or disabling [zone redundancy](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy)* – data must be copied to or from the zone redundant regions.
- **Business Critical** service tier: 
   - *Scaling storage*: Since storage is physically attached to the local machine, every storage change requires creating a new VM group, so data must be transferred from the old machine to the new machine (on all 4 replicas). 
   - *Scaling vCores*: Every compute scaling operation requires creating a new VM group, so data must be copied from the old machine to the new machine (on all 4 replicas).
   - *Changing hardware or maintenance window*: If a VM group already exists within the subnet with a matching configuration, that VM group is resized. If this is a new configuration, then a new VM group is created. Data must be copied from the old VM group to the new VM group (on all 4 replicas).
   - *Changing service tier*: Data must be copied from local storage to the remote storage used in the General Purpose service tier.
   - *Enabling or disabling [zone redundancy](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy)* – data must be copied to or from the zone redundant regions.

### Seeding speeds

The following factors affect the duration of the seeding process: 

- **Database size**: Larger databases require more time to transfer data and synchronize across SQL Database Engine processes.
- **Network dependencies**: Network bandwidth and latency can significantly influence seeding speeds. 
- **Backup and restore operations**: Ongoing backup operations on the source SQL Database Engine process can influence preparing data to send to another SQL Database Engine process.
- **Instance workload**: Instance workload during seeding can cause throttling and severely prolong the process. 

While most of these factors are beyond your control, you can manage instance traffic to significantly optimize seeding speeds. Seeding uses the same instance computing resources that manage instance traffic. Heavy traffic during seeding can reduce vCore availability, leading to insufficient capacity for the seeding process, causing throttling.

High traffic during seeding can affect synchronization since seeding is designed to package and transfer all currently stored data in a single operation. Subsequent data changes to the old SQL Database Engine process that arrive after seeding is initiated must be synchronized to the new SQL Database Engine process incrementally through transaction log block replication before failover can occur. If the instance is under heavy load, seeding might struggle keeping up with incoming data, leading to delays and potential failures in the synchronization phase. Continuous high traffic on the old SQL Database Engine process after seeding starts can lead to a situation where the synchronization phase never completes, as new data keeps arriving and must be transferred. This can result in a perpetual cycle of data transfer that prevents failover to the new SQL Database Engine process.

For an estimated duration of seeding operations, see [Management operations duration](management-operations-duration.md#seeding-duration).

### Azure infrastructure and notices 

Seeding is a process that can't be precisely quantified or strictly predicted, as it relies on the *shared Azure services*. The data transfer and seeding operations depend on various internal Azure services and infrastructure, which are shared across the entire Azure ecosystem. These services are utilized by numerous other unrelated services within Azure. All services within the Azure ecosystem compete for available resources, which leads to fluctuations in momentary availability influenced by multiple factors. While Microsoft can provide a range in which the infrastructure capacity operates, making precise predictions is challenging. 

## Failover

Instance failover is the moment when traffic is routed from an old SQL Database Engine process to a new SQL Database Engine process within the group of nodes in a VM group that encompass the SQL managed instance. Failover is a crucial part of most management operations, especially when updating an instance. The short moment of broken connections while traffic is redirected to the new SQL Database Engine process is referred to as *failover*. 

Your instance is *only unavailable during failover*, when traffic is rerouted to the new SQL Database Engine process. In the **Business Critical** service tier, your instance is unavailable for up to 20 seconds, while in the **General Purpose** service tier, your instance can be unavailable for up to 2 minutes. Any backend operations that occur to prepare for failover due to a management operation, such as reseeding databases in the **Business Critical** service tier, happen in the background and don't affect the availability of your instance.

> [!IMPORTANT]
> For update operations that don't complete in place but that result in reattaching the database (such as scaling vCores, scaling memory, changing hardware or the maintenance window), failover duration of databases on the [Next-gen General Purpose service tier](service-tiers-next-gen-general-purpose-use.md) scales with the number of databases, up to 10 minutes. While the instance becomes available after 2 minutes, some databases might be available after a delay. Failover duration is measured from the moment when the first database goes offline, until the moment when the last database comes online. The Next-gen General Purpose service tier increases the maximum number of databases per instance from 100 to 500.

Architectural differences between service tiers are explained in depth in [availability](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability). 

## Management operations cross-impact

Management operations on a SQL managed instance can affect the management operations of other instances placed inside the same subnet:

- **Long-running restore operations** in a virtual cluster put other operations in the same virtual cluster on hold, such as create or scale operations.

  **Example:** If there's a long-running restore operation, and also a scale request that shrinks the VM group, the shrink request takes longer to complete as it waits for the restore operation to finish before it can continue.

- **A subsequent instance creation or scaling** operation is put on hold by a previously initiated instance creation or instance scale that initiated a resize of the VM group.

  **Example:** If there are multiple create and/or scale requests in the same subnet under the same VM group, and one of them initiates a VM group resize, all requests that were submitted 1+ minutes after the initial operation request last longer than expected, as these requests must wait for the resize to complete before resuming.

- **Create/scale operations submitted in a 1-minute window** are batched and executed in parallel.

  **Example:** Only one virtual cluster resize is performed for all operations submitted in a 1-minute window (measured from the moment when the first operation request is submitted). If another request is submitted more than 1 minute after the first one is submitted, it waits for the virtual cluster resize to complete before execution starts.

> [!IMPORTANT]
> Management operations that are put on hold because of another operation that is in progress are automatically resumed once conditions to proceed are met. No user action is necessary to resume the temporarily paused management operations.

## Monitor management operations

To learn how to monitor management operation progress and status, see [Monitoring Azure SQL Managed Instance management operations](management-operations-monitor.md).

## Cancel management operations

To learn how to cancel management operation, see [Canceling Azure SQL Managed Instance management operations](management-operations-cancel.md).

## Related content

- [Quickstart: Create Azure SQL Managed Instance](instance-create-quickstart.md)
- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](../database/features-comparison.md)
- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [Virtual cluster architecture - Azure SQL Managed Instance](virtual-cluster-architecture.md)
- [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance)
