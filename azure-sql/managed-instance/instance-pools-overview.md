---
title: "What is an instance pool?"
titleSuffix: Azure SQL Managed Instance
description: Learn about instance pools of Azure SQL Managed Instance, a feature that provides a convenient and cost-efficient way to migrate smaller SQL Server instances to the cloud at scale, and manage multiple managed instances.
author: MladjoA
ms.author: mlandzic
ms.reviewer: vanto
ms.date: 03/31/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: concept-article
ms.custom:
  - ignite-2024
---
# What is an instance pool? - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the instance pool deployment option for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md), which brings substantial cost-saving benefits to customers. 

By using instance pools, you can deploy multiple instances that share resources. This setup offers a convenient and cost-effective infrastructure for migrating multiple databases from SQL Server instances. It eliminates the need to consolidate smaller, less compute-intensive workloads onto a larger SQL Managed Instance.

To get started, see [Create an instance pool](instance-pools-configure.md). 

## Overview

By using instance pools in Azure SQL Managed Instance, you can deploy multiple instances that share resources on a single underlying virtual machine within a [virtual cluster](virtual-cluster-architecture.md). 

Instance pools provide the following core benefits:

- Ability to host 2-vCore instances, which are only available within instance pools.
- Predictable and fast instance deployment time (up to 10 minutes).
- Cost-saving infrastructure when migrating multiple SQL Server instances.

The following diagram illustrates an instance pool with multiple managed instances deployed to a virtual cluster in a virtual network subnet: 

:::image type="content" source="media/instance-pools-overview/instance-pool.png" alt-text="Diagram of instance pool with multiple instances in a single pool." lightbox="media/instance-pools-overview/instance-pool.png":::

## What's new?

November 2024 brings the following capabilities: 

- Create pools, create instances within a pool, and modify pool configuration settings in the Azure portal. 
- Update pool configuration settings, such as compute size, license, and properties, by using PowerShell or the Azure CLI. 
- Support for premium-series hardware.
- Move an instance in and out of the pool by using PowerShell or the Azure CLI. 
- Instance pool support for [reservations pricing](#reservations-pricing). 

## Architecture

Instance pools have a similar architecture to single managed instances. To support [deployments within Azure virtual networks](/azure/virtual-network/virtual-network-for-azure-services) and provide isolation and security for customers, instance pools also rely on [virtual clusters](virtual-cluster-architecture.md). A virtual cluster represents a dedicated set of isolated virtual machines deployed inside the customer's virtual network subnet. All single instances and instance pools within the same subnet belong to the same virtual cluster. Instances within a pool have compute resources allocated to SQL Server processes and gateway components, which ensure predictable performance. After initial pool deployment, management operations on instances in a pool are faster as the SQL processes are created on compute resources that the service already provisioned. 

The compute size of the virtual machine is based on the total number of vCores allocated to the pool, which the service distributes between instances in the pool. This architecture allows *partitioning* of the virtual machine into multiple instances that can be any supported size, including 2 vCores (exclusive to instance pools). For example, when you deploy an 8-vCore instance pool, you can deploy two 2-vCore and one 4-vCore instance. You can then migrate your SQL Server databases to the instances within the pool. Since instance pools support native virtual network integration, you can deploy multiple instance pools, as well as multiple single instances, to the same subnet.

Instance pools provide a high level of isolation as each instance gets dedicated vCores on the underlying host virtual machine. The main difference of an instance pool deployment model compared to a single managed instance is that, by using instance pools, multiple SQL Server processes are created within the same virtual machine. The service resource governs these processes by using [Windows job objects](/windows/desktop/ProcThread/job-objects). If process-level isolation isn't sufficient, consider using single instances instead of instance pools. Single instances have only one SQL Server process on the virtual machine node, which provides VM-level isolation.

The following diagram illustrates the main architectural difference between the two deployment models: 

:::image type="content" source="media/instance-pools-overview/instance-pools-multiple-instances.png" alt-text="Diagram showing Instance pool and two individual instances in the virtual cluster." lightbox="media/instance-pools-overview/instance-pools-multiple-instances.png":::

## Application scenarios

Consider using instance pools for the following scenarios: 

- Migrating *a group of small SQL Server instances* at the same time, where the instances are 2- or 4-vCores. 
- You need *quick and predictable instance creation or scaling*. For example, deployment of a new tenant in a multitenant SaaS application environment that requires instance-level capabilities.
- Having a *fixed cost* or *spending limit* is important. For example, running shared dev-test or demo environments of a fixed (or infrequently changing) size, where you periodically deploy managed instances when needed.

Instance pools work well for migrating multiple SQL Server instances. When you pre-provision shared compute resources based on your total migration requirements, you reduce the overall cost of ownership after migration. For example, consider a scenario where you migrate four small on-premises SQL Server instances to Azure SQL Managed Instance. Without an instance pool, you would provision four separate single SQL managed instances with a minimum of 4 vCores each, all with their own dedicated resources. An instance pool reduces this cost since you can deploy all instances with 2 vCores each to the pool where resources are shared by the pool. 

## Instance and pool properties

You configure the following properties at the pool level for all instances in the pool: 

- [Hardware tiers](resource-limits.md#hardware-configuration-characteristics)
- The SQL Server license, such as the [Azure Hybrid Benefit](../azure-hybrid-benefit.md)
- [Maintenance window](../database/maintenance-window.md)

Additionally, consider the following: 

- SQL managed instances that you create in pools support the same [compatibility levels and features available to single managed instances](sql-managed-instance-paas-overview.md#supported-sql-features).
- You configure optional features or features that require you to choose specific values (such as instance-level collation, time zone, public endpoint for data traffic, failover groups) at the instance level. Each instance in a pool can have different values.
- Because instances that you deploy to a pool share the same virtual machine, consider disabling features that introduce higher security risks. To firmly control access permissions to these features, disable features such as CLR integration, native backup and restore, Database Mail, and others.
- You can configure SQL Managed Instance to use [Microsoft Entra authentication](../database/authentication-aad-configure.md#provision-azure-ad-admin-sql-managed-instance) before or after you add it to the pool.  
- Every SQL managed instance that you deploy in a pool has a separate SQL Server Agent.

## Resource limits

When you deploy an instance to a pool, the individual pooled instance has limits, and the overall pool has limits on resource usage. 

The following table details limits for both pooled instances and the pool: 

|<br />|Pool limits|Pooled instance limits|
|:---|:---|:---|
|Service tier|General Purpose|General Purpose|
|Hardware tier|Standard-series (Gen5)  <br /> Premium-series |Standard-series (Gen5)  <br />Premium-series|
|Number of vCores<sup>1</sup>|8-16-24-32-40-64-80|2-4-8-16-24-32-40-64-80|
|Max storage| 32 TB<sup>2</sup>|- 640 GB for 2 vCores  <br />- 2 TB for 4 vCores <br />- 8 TB for 8 vCores <br />-16 TB for 16+ vCores|
|Max number of databases|500|- 50 for 2 vCores  <br />- 100 for 4+ vCores|
|Max number of instances | 40 | N/A|

<sup>1</sup> vCore options for pooled instances depend on the number of available vCores in the instance pool.   
<sup>2</sup> Pool storage limit is dictated by the sum of the storage for all instances in the pool.

For all other instance-level limits, see [Resource limits](resource-limits.md).

## Performance considerations

Although SQL managed instances within pools have dedicated vCore and RAM, they share a local disk (for `tempdb`) and network resources. It's possible, though unlikely, to experience a *noisy neighbor* effect when multiple instances in the pool have high resource consumption at the same time.

If you experience this behavior, consider increasing the pool size or redeploying the high-consumption instances as single instances outside the pool.

## Instance pool billing

Instance pools help you scale compute and storage independently. You pay for: 

- Compute allocated to the pool, measured in vCores.
- Storage associated with every instance, measured in gigabytes (the first 32 GB are free for each instance).

You pay the vCore price for a pool no matter how many instances you deploy to that pool. You can't set different pricing options for individual instances in a pool. All instances in the pool must use the same licensing model. You can change the license model for the pool after you create the pool.

The compute price (measured in vCores) depends on whether you're paying the full SQL Server license price. Two price options are available: 

- *License included*: The price of the SQL Server licenses is included. 
- *Azure Hybrid Benefit*: A reduced price that includes the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) for SQL Server. To use this price, you must have existing SQL Server licenses with Software Assurance. 

For full instance pool pricing details, see the *instance pools* section on the [SQL Managed Instance pricing page](https://azure.microsoft.com/pricing/details/sql-database/managed/).

> [!NOTE]
> When you create instance pools on [subscriptions eligible for the dev-test benefit](https://azure.microsoft.com/pricing/dev-test/), you automatically get discounted rates up to 55% on Azure SQL Managed Instance.

### Reservations pricing

[Azure Reservations](../database/reservations-discount-overview.md) helps you save on compute costs by providing a deep discount when you reserve the price of resources for a predetermined period. You can now allocate reservations to an instance pool, which creates the most cost-effective way to run multiple instances. 

## Limitations

To learn more, see [instance pool limitations](instance-pools-configure.md#limitations). 

## Next step

> [!div class="nextstepaction"]
> [Configure an instance pool](instance-pools-configure.md)
