---
title: Management operations overview
titleSuffix: Azure SQL Managed Instance
description: Learn about Azure SQL Managed Instance management operations duration and best practices.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma
ms.date: 08/20/2021
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: overview
ms.custom: ignite-2023
---

# Overview of Azure SQL Managed Instance management operations
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Azure SQL Managed Instance provides management operations that you can use to automatically deploy new managed instances, update instance properties, and delete instances when no longer needed.

## What are management operations?

All management operations can be categorized as follows:

- Instance deployment (new instance creation)
- Instance update (changing instance properties, such as vCores or reserved storage)
- Instance deletion

To support [deployments within Azure virtual networks](/azure/virtual-network/virtual-network-for-azure-services) and provide isolation and security for customers, SQL Managed Instance relies on [virtual clusters](virtual-cluster-architecture.md). The virtual cluster represents a dedicated set of isolated virtual machines deployed inside the customer's virtual network subnet and organized in virtual machine groups. Essentially, every managed instance deployed to an empty subnet results in a new virtual cluster buildout which builds the very first virtual machine group.

Subsequent management operations on managed instances can impact the underlying [virtual machine groups](virtual-cluster-architecture.md#role-in-management-operations). Changes that impact the underlying virtual machine groups might affect the duration of management operations, as deploying additional virtual machines to the virtual cluster comes with an overhead that you need to consider when you plan new deployments or updates to existing managed instances.

## Fast provisioning

Instances with certain configurations can benefit from fast SQL Managed Instance provisioning, which reduces the time it takes to create your first instance in a subnet to 30 minutes (down from an average of 45-60 minutes). To learn more about operation duration times, review [management operations](management-operations-overview.md).

Fast provisioning only applies: 

- to the first instance provisioned in the subnet. 
- to instances with 4-8 vCores. 
- to instances that use the default maintenance window. 
- to instances that are not zone redundant.

## Duration

The duration of operations on the virtual cluster can vary, but typically have the longest duration. 

The following table lists the long running steps that can be triggered as part of the create, update, or delete operation. Table also lists the durations that you can typically expect, based on existing service telemetry data:

|Step|Description|Estimated duration|
|---------|---------|---------|
|**Virtual cluster creation (fast provisioning)**<sup>1</sup>|Fast provisioning is a synchronous step in instance management operations during which the very first virtual machine group is instantly available.|**90% of operations finish in 30 minutes**|
|**Virtual cluster creation**|Creation is a synchronous step in instance management operations during which the very first virtual machine group is created.|**90% of operations finish in less than 4 hours**|
|**Virtual cluster resizing (expansion or shrinking)**|Adding new machines to the existing virtual machine group, removing unused virtual machines, adding or removing the entire virtual machine group. Expansion is a synchronous step, while shrinking is performed asynchronously (without impact on the duration of instance management operations).|**90% of cluster expansions with creation of new virtual machine group finish in less than 4 hours** <br /><br /> **90% of cluster expansions with expansion of existing virtual machine group finish in 60 minutes**|
|**Virtual cluster deletion**|Virtual cluster deletion is triggered when the very last instance is deleted from the subnet.|**90% of cluster deletions finish in 1.5 hours**|
|**Seeding database files**<sup>2</sup>|A synchronous step, triggered during compute (vCores), or storage scaling in the Business Critical service tier as well as in changing the service tier from General Purpose to Business Critical (or vice versa). Duration of this operation is proportional to the total database size as well as current database activity (number of active transactions). Database activity when updating an instance can introduce significant variance to the total duration.|**90% of these operations execute at 220 GB/hour or higher**|

<sup>1</sup> Fast provisioning is currently supported only for the first instance in the subnet, with 4 or 8 vCores, and with default maintenance window configuration.   
<sup>2</sup> When scaling compute (vCores) or storage in Business Critical service tier, or switching service tier from General Purpose to Business Critical, seeding also includes Always On availability group seeding.


> [!IMPORTANT]
> Scaling storage up or down in the General Purpose service tier consists of updating meta data and propagating response for submitted request. It is a fast operation that completes in up to 5 minutes, without a downtime and failover.

### Management operations long running segments

The following tables summarize operations and typical overall durations, based on the category of the operation:

**Category: Deployment**

|Operation  |Long-running segment  |Estimated duration  |
|---------|---------|---------|
|First instance in an empty subnet<sup>1</sup>|Virtual cluster creation (fast provisioning)|90% of operations finish in 30 minutes.|
|First instance in an empty subnet|Virtual cluster creation|90% of operations finish in less than 4 hours.|
|First instance with a different hardware generation or maintenance window in a non-empty subnet (for example, the first Premium-series instance in a subnet with Standard-series instances)|Adding new [virtual machine group](virtual-cluster-architecture.md#role-in-management-operations) to the virtual cluster<sup>2</sup>|90% of operations finish in less than 4 hours.|
|Subsequent instance creation within the non-empty subnet (2nd, 3rd, etc. instance)|Virtual cluster resizing|90% of operations finish in 60 minutes.|

<sup>1</sup> Fast provisioning is currently supported only for the first instance in the subnet, with 4 or 8 vCores, and with default maintenance window configuration.
<sup>2</sup> A separate [virtual machine group](virtual-cluster-architecture.md#role-in-management-operations) is created for each hardware generation and maintenance window configuration.

**Category: Update**

|Operation  |Long-running segment  |Estimated duration  |
|---------|---------|---------|
|Instance property change  <br /> (admin password, Microsoft Entra login, Azure Hybrid Benefit flag)|N/A|Up to 1 minute.|
|Instance storage scaling up/down <br /> (General Purpose)|No long-running segment|99% of operations finish in 5 minutes.|
|Instance storage scaling up/down <br /> (Business Critical)|- Virtual cluster resizing<br />- Always On availability group seeding|90% of operations finish in 60 minutes + time to seed all databases (220 GB/hour).|
|Instance storage scaling up/down <br /> (Next-gen General Purpose)|- Virtual cluster creation / virtual machine group resizing <br /> - Always On availability group seeding| 90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) + time to seed all databases (220 GB/hour) + failover + cleaning up old instance |
|Instance compute (vCores) scaling up and down  <br />(General Purpose)|- Virtual cluster resizing|90% of operations finish in 60 minutes.|
|Instance compute (vCores) scaling up and down  <br />(Business Critical)|- Virtual cluster resizing<br />- Always On availability group seeding|90% of operations finish in 60 minutes + time to seed all databases (220 GB/hour).|
|Instance compute (vCores) scaling up and down  <br />(Next-gen General Purpose)| Virtual cluster creation / virtual machine group resizing <br />- Always On availability group seeding| 90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) + time to seed all databases (220 GB/hour) + failover + cleaning up old instance|
|Instance service tier change  <br />(General Purpose to Business Critical and vice versa)|- Virtual cluster resizing<br />- Always On availability group seeding|90% of operations finish in 60 minutes + time to seed all databases (220 GB/hour).|
|Instance service tier change  <br />(General Purpose or Business Critical to Next-gen General Purpose and vice versa)| Virtual cluster creation / virtual machine group resizing <br />- Always On availability group seeding| 90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) + time to seed all databases (220 GB/hour) + failover + cleaning up old instance| 
|Instance hardware or maintenance window change  <br />(General Purpose)|- Virtual cluster resizing<sup>1</sup>|90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) .|
|Instance hardware or maintenance window change  <br />(Business Critical)|- Virtual cluster resizing<sup>1</sup><br />- Always On availability group seeding|90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) + time to seed all databases (220 GB/hour).|
|Instance hardware or maintenance window change <br /> (Next-gen General Purpose)|- Virtual cluster creation / virtual machine group resizing <br />- Always On availability group seeding|90% of operations finish in less than 4 hours (virtual machine group creation) or 60 minutes (virtual machine group resizing) + time to seed all databases (220 GB/hour) + failover + cleaning up old instance |

<sup>1</sup> Managed instance must be placed in a virtual machine group with the same corresponding hardware and maintenance window. If there is no such group in the virtual cluster, a new one must be created first to accommodate the instance configuration.

**Category: Delete**

|Operation  |Long-running segment  |Estimated duration  |
|---------|---------|---------|
|Non-last instance deletion|Log tail backup for all databases|90% of operations finish in up to 1 minute.<sup>1</sup>|
|Last instance deletion |- Log tail backup for all databases <br /> - Virtual cluster deletion|90% of operations finish in up to 1.5 hours.<sup>2</sup>|


<sup>1</sup> If there are multiple virtual machine groups in the cluster, deleting the last instance in the group immediately triggers deleting the virtual machine group **asynchronously**.   
<sup>2</sup> Deleting the last instance in the subnet immediately triggers deleting the virtual cluster **synchronously**.

> [!IMPORTANT]
> As soon as delete operation is triggered, billing for SQL Managed Instance is disabled. Duration of the delete operation will not impact the billing.

## Instance availability

SQL Managed Instance **is available during update operations**, except a short downtime caused by the failover that happens at the end of the update. It typically lasts up to 10 seconds even in case of interrupted long-running transactions, thanks to [accelerated database recovery](../accelerated-database-recovery.md).

> [!NOTE]
> Scaling General Purpose managed instance storage will not cause a failover at the end of update.

SQL Managed Instance is not available to client applications during deployment and deletion operations.

> [!IMPORTANT]
> It's not recommended to scale compute or storage of Azure SQL Managed Instance or to change the service tier at the same time as long-running transactions (data import, data processing jobs, index rebuild, etc.). The failover of the database at the end of the operation cancels all ongoing transactions. 

## Management operations steps

Management operations consist of multiple steps. With [Operations API introduced](management-operations-monitor.md) these steps are exposed for subset of operations (deployment and update). Deployment operation consists of three steps while update operation is performed in six steps. For details on operations duration, see the [management operations duration](#duration) section. Steps are listed by order of execution.

### Managed instance deployment steps

|Step name  |Step description  |
|----|---------|
|Request validation |Submitted parameters are validated. In case of misconfiguration operation will fail with an error. |
|Virtual cluster resizing / creation |Depending on the state of the virtual cluster, the cluster goes into _creating_ or _resizing_ state. |
|New SQL instance startup |SQL process is started on the deployed virtual machines. |

### Managed instance update steps

|Step name  |Step description  |
|----|---------|
|Request validation |Submitted parameters are validated. In case of misconfiguration operation will fail with an error. |
|Virtual cluster resizing / creation |Depending on the state of the virtual cluster, the cluster goes into _creating_ or _resizing_ state. |
|New SQL instance startup |SQL process is started on the deployed virtual machines. |
|Seeding database files / attaching database files |Depending on the type of the update operation, either database seeding or attaching database files is performed. |
|Preparing failover and failover |After data has been seeded or database files reattached, system is being prepared for the failover. When everything is set, failover is performed **with a short downtime**. |
|Old SQL instance cleanup |Removing old SQL process from the virtual machines. |

### Managed instance delete steps
|Step name  |Step description  |
|----|---------|
|Request validation |Submitted parameters are validated. In case of misconfiguration operation will fail with an error. |
|SQL instance cleanup |Removing SQL process from the virtual machines. |
|Virtual cluster deletion |Depending if the instance being deleted is last in the subnet, virtual cluster is synchronously deleted as last step. |

> [!NOTE]
> As a result of scaling instances, underlying virtual cluster will go through process of releasing unused capacity and possible capacity defragmentation, which could impact instances that did not participate in creation / scaling operations. 


## Management operations cross-impact

Management operations on a managed instance can affect the management operations of other instances placed inside the same subnet:

- **Long-running restore operations** in a virtual cluster put other operations in the same virtual machine group on hold, such as creation or scaling operations. 
<br/>**Example:** If there is a long-running restore operation and also a scale request that requires shrinking the virtual machine group, the shrink request will take longer to complete as it waits for the restore operation to finish before it can continue.

- **A subsequent instance creation or scaling** operation is put on hold by a previously initiated instance creation or instance scale that initiated a resize of the virtual machine group.<br/>**Example:** If there are multiple create and/or scale requests in the same subnet under the same virtual machine group, and one of them initiates a virtual machine group resize, all requests that were submitted 5+ minutes after the initial operation request will last longer than expected, as these requests will have to wait for the resize to complete before resuming.

- **Create/scale operations submitted in a 5-minute window** will be batched and executed in parallel.<br/>**Example:** Only one virtual cluster resize will be performed for all operations submitted in a 5-minute window (measuring from the moment of executing the first operation request). If another request is submitted more than 5 minutes after the first one is submitted, it will wait for the virtual cluster resize to complete before execution starts.

> [!IMPORTANT]
> Management operations that are put on hold because of another operation that is in progress will automatically be resumed once conditions to proceed are met. No user action is necessary to resume the temporarily paused management operations.

## Monitoring management operations

To learn how to monitor management operation progress and status, see [Monitoring management operations](management-operations-monitor.md).

## Canceling management operations

To learn how to cancel management operation, see [Canceling management operations](management-operations-cancel.md).


## Next steps

- To learn how to create your first managed instance, see [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [Common SQL features](../database/features-comparison.md).
- For more information about VNet configuration, see [SQL Managed Instance VNet configuration](connectivity-architecture-overview.md).
- For more information about virtual machine groups and virtual cluster, see [Architecture of the SQL Managed Instance virtual cluster](virtual-cluster-architecture.md)
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
