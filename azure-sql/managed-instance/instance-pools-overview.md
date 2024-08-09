---
title: "What is an instance pool?"
titleSuffix: Azure SQL Managed Instance
description: Learn about instance pools (preview) of Azure SQL Managed Instance, a feature that provides a convenient and cost-efficient way to migrate smaller SQL Server databases to the cloud at scale, and manage multiple managed instances.
author: MariDjo
ms.author: dmarinkovic
ms.reviewer: mathoma
ms.date: 01/31/2024
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
---
# What is an instance pool (preview)? - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the instance pool deployment option for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). 

Instance pools make it possible to deploy multiple instances with shared resources, which provides a convenient and cost-effective infrastructure to migrate multiple databases from SQL Server instances without having to consolidate smaller and less compute-intensive workloads onto a larger SQL Managed Instance.

To get started, review [Create an instance pool](instance-pools-configure.md). 

> [!NOTE]
> Instance pools for Azure SQL Managed Instance are currently in preview. 

## Overview

Instance pools in Azure SQL Managed Instance allow you to deploy multiple instances with shared resources onto a single underlying virtual machine within a [virtual cluster](virtual-cluster-architecture.md). 

Instance pools provide the following core benefits:

- Ability to host 2-vCore instances, which are only available within instance pools
- Predictable and fast instance deployment time (up to 5 minutes).
- Cost-saving infrastructure when migrating multiple SQL Server instances.

The following diagram illustrates an instance pool with multiple managed instances deployed to a virtual cluster in a virtual network subnet: 

![Diagram of instance pool with multiple instances in a single pool.](./media/instance-pools-overview/instance-pool.png)

## What's new?

The 2024 preview refresh of instance pools brings the following new capabilities: 

- Pool configuration (such as compute size, license, properties) can be updated by using PowerShell or the Azure CLI. 
- Premium-series hardware is now supported. 
- You can move an instance in and out of the pool by using PowerShell or the Azure CLI. 

## Architecture

Instance pools have a similar architecture to single managed instances. To support [deployments within Azure virtual networks](/azure/virtual-network/virtual-network-for-azure-services) and provide isolation and security for customers, instance pools also rely on [virtual clusters](virtual-cluster-architecture.md). Virtual clusters represent a dedicated set of isolated virtual machines deployed inside the customer's virtual network subnet. All single instances and instance pools belong to the same virtual cluster. Instances within a pool and single instances deployed to the same subnet don't share compute resources allocated to SQL Server processes and gateway components, which ensures performance predictability. After initial pool deployment, management operations on instances in a pool are faster as the virtual cluster has already been extended when the pool was provisioned. 

The compute size of the virtual machine is based on the total number of vCores allocated to the pool, which are distributed between instances in the pool. This architecture allows *partitioning* of the virtual machine into multiple instances that can be any supported size, including 2 vCores (exclusive to instance pools). For example, when you deploy an 8-vCore instance pool you can deploy two 2-vCore and one 4-vCore instance. You can then migrate your SQL Server databases to the instances within the pool. And since instance pools support native virtual network integration, you can deploy multiple instance pools, as well as multiple single instances, to the same subnet.

The main difference between the two deployment models is that you can create multiple SQL Server processes within the same virtual machine when you use an instance pool, which are resource governed using [Windows job objects](/windows/desktop/ProcThread/job-objects). Single instances have only a single SQL Server process on the virtual machine node. 

The following diagram illustrates the main architectural difference between the two deployment models: 

![Diagram showing Instance pool and two individual instances in the virtual cluster.](./media/instance-pools-overview/instance-pools-multiple-instances.png)

## Application scenarios

Consider using instance pools for the following scenarios: 

- Migrating *a group of small SQL Server instances* at the same time, where the instances are 2- or 4-vCores. 
- You need *quick and predictable instance creation or scaling*. For example, deployment of a new tenant in a multitenant SaaS application environment that requires instance-level capabilities.
- Having a *fixed cost* or *spending limit* is important. For example, running shared dev-test or demo environments of a fixed (or infrequently changing) size, where you periodically deploy managed instances when needed.

Instance pools are particularly well suited for migrating multiple SQL Server instances, since pre-provisioning shared compute resources according to your total migration requirements reduces overall cost of ownership after migration. For example, consider a scenario to migrate four small on-premises SQL Server instances to Azure SQL Managed Instance. Without an instance pool, you would provision four separate single SQL managed instances with a minimum of 4 vCores each, all with their own dedicated resources. An instance pool reduces this cost since you can deploy all instances with 2 vCores each to the pool where resources are shared by the pool. 

## Instance and pool properties

The following properties are configured at the pool level for all instances in the pool: 

- [Hardware tiers](resource-limits.md#hardware-configuration-characteristics)
- The SQL Server license, such as the [Azure Hybrid Benefit](../azure-hybrid-benefit.md)
- [Maintenance window](../database/maintenance-window.md)

Additionally, consider the following: 

- Managed instances created in pools support the same [compatibility levels and features available to single managed instances](sql-managed-instance-paas-overview.md#supported-sql-features).
- Optional features or features that require you to choose specific values (such as instance-level collation, time zone, public endpoint for data traffic, failover groups) are configured at the instance level and can be different for each instance in a pool.
- Because instances deployed to a pool share the same virtual machine, consider disabling features that introduce higher security risks, or to firmly control access permissions to these features, such  CLR integration, native backup and restore, database email, etc.
- You can configure your SQL Managed Instance to use [Microsoft Entra authentication](../database/authentication-aad-configure.md#provision-azure-ad-admin-sql-managed-instance) before or after it's added to the pool. 
- Every managed instance deployed in a pool has a separate instance of SQL Agent.

## Resource limits

When you deploy an instance to a pool, there are limits to each individual pooled instance, and limits to the resources used by the overall pool. 

The following table details limits for both pooled instances, and the pool: 

|<br />|Pool limits|Pooled instance limits|
|:---|:---|:---|
|Service tier|General Purpose|General Purpose|
|Hardware tier|Standard-series (Gen5)  <br /> Premium-series |Standard-series (Gen5)  <br />Premium-series|
|Number of vCores<sup>1</sup>|8-16-24-32-40-64-80|2-4-8-16-24-32-40-64-80|
|Max storage| 32 TB<sup>2</sup>|- 640 GB for 2 vCores  <br />- 2 TB for 4 vCores <br />- 8 TB for 8 vCores <br />-16 TB for 16+ vCores|
|Max # of databases|500|- 50 for 2 vCores  <br />- 100 for 4+ vCores|
|Max # of instances | 40 | N/A|

<sup>1</sup> vCore options for pooled instances depends on the number of available vCores in the instance pool.   
<sup>2</sup> Pool storage limit is dictated by the sum of the storage for all instances in the pool.

For all other instance level limits, review [Resource limits](resource-limits.md).

## Performance considerations

Although managed instances within pools have dedicated vCore and RAM, they share a local disk (for `tempdb`),  and network resources. Although unlikely, it's possible to experience a *noisy neighbor* effect from multiple instances in the pool have high resource consumption at the same time. 

If you're experiencing this behavior, consider increasing the pool size, or redeploying the high consuming resources as a single instance outside the pool. 

## Instance pool billing

Instance pools allow you to scale compute and storage independently. You pay for: 

- Compute allocated to the pool, measured in vCores
- Storage associated with every instance measured in gigabytes (the first 32 GB are free of charge for every instance).

vCore price for a pool is charged regardless of how many instances are deployed to that pool. Setting different pricing options isn't possible for individual instances in a pool. All instances in the pool must use the same licensing model. The license model for the pool can be altered after the pool is created.

The compute price (measured in vCores), depends on whether or not you're paying the full SQL Server license price. The following two price options are available: 

- *License included*: The price of the SQL Server licenses is included. 
- *Azure Hybrid Benefit*: A reduced price that includes the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) for SQL Server. Customers can opt into this price by using their existing SQL Server licenses with Software Assurance. 

For full instance pool pricing details, refer to the *instance pools* section on the [SQL Managed Instance pricing page](https://azure.microsoft.com/pricing/details/sql-database/managed/).

> [!NOTE]
> Instance pools created on [subscriptions eligible for the dev-test benefit](https://azure.microsoft.com/pricing/dev-test/) automatically receive discounted rates of up to 55 percent on Azure SQL Managed Instance.


## Limitations

To learn more, review [instance pool limitations](instance-pools-configure.md#limitations). 

## Next steps

[Configure an instance pool](instance-pools-configure.md) 
