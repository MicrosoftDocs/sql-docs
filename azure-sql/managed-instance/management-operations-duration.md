---
title: Duration of Management Operations
titleSuffix: Azure SQL Managed Instance
description: Learn about the duration of Azure SQL Managed Instance management operations.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, randolphwest
ms.date: 07/10/2026
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: overview
ms.custom:
  - ignite-2025
---

# Duration of management operations in Azure SQL Managed Instance 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article details the steps and the duration of management operations in [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). 

For an overview of the underlying processes related to management operations, such as seeding, and failover, see [Management operations overview](management-operations-overview.md).

## Management operation steps 

Managing Azure SQL Managed Instance involves the following operations: 

- [Create](#create-operation): The operations that occur when you create a new SQL managed instance. This includes creating or resizing the underlying [virtual machine (VM) group](management-operations-overview.md#vm-group-operations), and deploying the SQL Database Engine process.
- [Update](#update-operation): Operations that occur when you change the properties of an existing SQL managed instance, such as scaling compute or storage, changing the service tier, or updating the instance configuration. Making updates often involves creating or resizing the underlying [virtual machine (VM) group](management-operations-overview.md#vm-group-operations), as well as [seeding data](management-operations-overview.md#seeding), and then [failing over](management-operations-overview.md#failover), to a new SQL Database Engine process.
- [Delete](#delete-operation): Operations that occur when you delete an existing SQL managed instance, including the cleanup of resources such as the VM group associated with the instance.

### Create operation

The **Create** operation initiates the deployment of a new SQL managed instance within a virtual network subnet, while setting up compute, storage, and the SQL Database Engine environment for the instance. 

The creation process typically goes through three phases: 

1. **Validate request**: The submitted parameters are syntactically and semantically validated. If parameters are invalid (such as the wrong subnet, or unsupported SKU) the operation immediately fails with an error.
1. **[Create or resize the VM group](management-operations-overview.md#vm-group-operations)**: Creates or extends a VM group to host the new instance. The duration of the operation depends on whether or not the instance is zone redundant or not.  
1. **Start up new SQL instance**: Deploys and starts the SQL Database Engine process on the allocated VMs. 

### Update operation

The **Update** operation modifies the properties of an existing SQL managed instance, such as scaling compute or storage, changing the service tier, or updating the instance configuration. 

The update process typically goes through five phases: 

1. **Validate request**: The submitted parameters are syntactically and semantically validated. Checks for supported update types based on the current instance configuration and the requested changes. If the request is invalid, the operation fails with an error.
1. **[Create or resize the VM group](management-operations-overview.md#vm-group-operations)**: Depending on the change, the existing VM group is resized or a new VM group is created, such as in the following update operations: 
   - Scaling storage up or down 
   - Scaling compute up or down
   - Changing the service tier
   - Changing the hardware
   - Adjusting the maintenance window
   - Enabling or disabling zone redundancy 
1. **Start up SQL instance**: A new SQL Database Engine process is initialized with the updated configuration. 
   - If a new VM group is created, or if the existing VM group is resized, a full SQL Database Engine deployment occurs. 
1. **[Seed / attach storage](management-operations-overview.md#seeding)**: Prepares database on the new or resized VM group. The instance is available during this process. 
1. **Prepare for, and then fail over**: Traffic is redirected to the new instance. 
   - Your instance is only unavailable during failover, when traffic is rerouted to the new SQL Database Engine process. In the **Business Critical** service tier, your instance is unavailable for up to 20 seconds, while in the **General Purpose** service tier, your instance can be unavailable for up to 2 minutes. 
1. **Clean up old SQL instance**: Deallocate the old virtual machines and delete the SQL processes that are no longer required. 

> [!IMPORTANT]
> Scaling compute or storage, or changing the service tier, at the same time as long-running transactions (such as importing data, data processing jobs, or index rebuild) isn't recommended as the failover of the database at the end of the operation cancels all ongoing transactions.

### Delete operation

The **Delete** operation removes an existing SQL managed instance and cleans up the associated resources. As soon as a delete operation is triggered, billing for SQL Managed Instance is disabled. The duration of the delete operation doesn't affect the billing.

The delete process typically goes through four phases:

1. **Validate request**: The submitted parameters are syntactically and semantically validated. If the request is invalid, the operation fails with an error.
1. **Tail-log backup**: If the instance is not empty, a tail-log backup is taken for every database to ensure that no data is lost after the instance is deleted. Backups are kept based on the retention policy of each database.
1. **SQL instance cleanup**: The SQL Database Engine process is removed from the VM group, and the resources associated with the instance are deallocated.
1. **Delete VM group**: If there are other instances in the subnet, the VM group remains intact for those instances. If the instance being deleted is the last instance in the subnet, the VM group is synchronously deleted as the last step. When the last instance in a subnet is deleted, removing the VM group automatically initiates the removal of the virtual cluster. 

## Instance pools 

[Instance pools](instance-pools-overview.md) allow you to create and manage multiple instances with shared resources, which can help reduce costs and simplify management. Deploying an individual instance within an existing pool is significantly faster than provisioning a standalone managed instance because the infrastructure is already available. 

**Create an instance pool** involves the following steps: 
- **Validate request**: The submitted parameters are syntactically and semantically validated. If the request is invalid, the operation fails with an error.
- **Create the VM group**: A new VM group is created to host the instance pool within a subnet of an Azure virtual network. The number of vCores allocated to the virtual cluster is the maximum total vCores used by all instances in the pool. This is a one-time operation that sets up the underlying infrastructure for multiple managed instances. 
- **Create instance**: Instances are created within the instance pool, which involves deploying the SQL Database Engine process on the allocated VMs. The instances share the resources of the virtual cluster, which allows for more efficient resource utilization. Instances are created by the customer as needed. 

**Create an instance inside a pool** involves the following steps:
- **Validate request**: The submitted parameters are syntactically and semantically validated. If the request is invalid, the operation fails with an error.
- **Create instance**: Instances are created within the instance pool, which involves deploying the SQL Database Engine process on the allocated VMs. 

**Moving an instance to an instance pool** involves the following steps:
- **Validate request**: The submitted parameters are syntactically and semantically validated. If the request is invalid, the operation fails with an error.
- **Allocate vCores**: The instance must be assigned adequate number of required vCores from the pool. As we already have vCores provisioned to the pool, this is easy and works the same as provisioning a new instance inside the pool.

**Moving an instance out of an instance pool** involves the following steps:
- **Validate request**: The submitted parameters are syntactically and semantically validated. If the request is invalid, the operation fails with an error.
- **[Create or resize the VM group](management-operations-overview.md#vm-group-operations)**: This requires providing an adequate number of required vCores to the instance outside of the pool. vCores aren't ready and must be provisioned, so this operation is the same as any update duration that must resize an existing VM group, or create a new VM group. 

## Zone Redundancy 

With [zone redundancy](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy) enabled, compute and storage layers are distributed across multiple availability zones to ensure high availability and data integrity.

Zone redundancy extends the duration of management operations to accommodate changes to resources across multiple availability zones. 

## Management operation duration

The duration of management operations varies based on the service tier of the SQL Managed Instance. The following sections provide detailed information about the duration of management operations for each service tier:

#### [General Purpose service tier](#tab/gp)

The following table details the duration of management operations in the **General Purpose** service tier, including the long-running segments and estimated duration for each operation: 

| Management operation | Long-running segments | Estimated duration |
|--|--|--|--|
| <center> ***Create operations*** </center> |
| Creating a new instance| [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 30 minutes |
| Creating a new zone-redundant instance | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) with [zone redundancy](#zone-redundancy) | 95% of operations finish in 4 hours | 
| Creating new instance pool | [Creating the VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 30 minutes| 
| Creating instance inside a pool | None | 95% of operations finish in less than 10 minutes |
| <center> ***Update operations*** </center> |
| Changing basic instance properties such as the license type or Microsoft Entra |  None | Up to 1 minute |
| Scaling storage | None | 99% of operations finish in 5 minutes|
| Scaling compute (vCores) | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes  |
| Changing to **Business Critical** service tier | [Resizing the VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  | 
| Changing to **Next-gen General Purpose** service tier | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration) |
| Changing hardware or maintenance window | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes  |
| Enabling zone redundancy | [Creating new VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 4 hours + [time to seed databases](#seeding-duration)   |
| Disabling zone redundancy | [Creating new VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 30 minutes + [time to seed databases](#seeding-duration)   |
| Changing update policy | [Resizing the VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  |
| Moving an instance to an instance pool | None | 95% of operations finish in 10 minutes |
| Moving an instance out of an instance pool | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes |
| <center> ***Delete operations*** </center> |
|Deleting non-last instance<sup>1</sup> |Log tail backup for all databases | 95% of operations finish in 1 minute.  |
|Deleting last instance<sup>2</sup> |Log tail backup for all databases <br /> [Deleting virtual cluster](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 90 minutes |

<sup>1</sup> If there are multiple VM groups in the cluster, deleting the last instance in the group immediately triggers deleting the VM group **asynchronously**.   
<sup>2</sup> Deleting the last instance in the subnet immediately triggers deleting the virtual cluster **synchronously**.


#### [Next-gen General Purpose service tier](#tab/ngp)

The following table details the duration of management operations in the [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) service tier, including the long-running segments and estimated duration for each operation:

| Management operation | Long-running segments | Estimated duration |
|--|--|--|--|
| <center> ***Create operations*** </center> |
| Creating a new instance| [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 30 minutes |
| <center> ***Update operations*** </center> |
| Changing basic instance properties such as the license type or Microsoft Entra |  None | Up to 1 minute |
| Scaling storage<sup>1</sup> | None | 99% complete in 5 minutes|
| Scaling compute (vCores) | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes  |
| Scaling [memory (RAM)](resource-limits.md#flexible-memory) |  [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes | 
| Scaling IOPS | None | 99% of operations finish in 5 minutes |
| Changing to **Business Critical** service tier | [Resizing the VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  | 
| Changing to **General Purpose** service tier | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration) |
| Changing hardware or maintenance window | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes  |
| Changing update policy | [Resizing the VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  | 
| <center> ***Delete operations*** </center> |
|Deleting non-last instance<sup>2</sup> |Log tail backup for all databases | 95% of operations finish in 1 minute.  |
|Deleting last instance<sup>3</sup> |Log tail backup for all databases <br /> [Deleting virtual cluster](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 90 minutes |

<sup>1</sup> When scaling storage, if the delta between the target value and the initially configured value is greater than 5.5TB, databases are seeded.
<sup>2</sup> If there are multiple VM groups in the cluster, deleting the last instance in the group immediately triggers deleting the VM group **asynchronously**.   
<sup>3</sup> Deleting the last instance in the subnet immediately triggers deleting the virtual cluster **synchronously**.


### [Business Critical service tier](#tab/bc)

The following table details the duration of management operations in the **Business Critical** service tier, including the long-running segments and estimated duration for each operation:

| Management operation | Long-running segments | Estimated duration |
|--|--|--|--|
| <center> ***Create operations*** </center> |
| Creating a new instance| [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 30 minutes |
| Creating a new zone-redundant instance | [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) with [zone redundancy](#zone-redundancy) | 95% of operations finish in 4 hours | 
| <center> ***Update operations*** </center> |
| Changing basic instance properties such as the license type or Microsoft Entra |  None | Up to 1 minute |
| Scaling storage | [Resizing VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  |
| Scaling compute (vCores) |  [Creating or resizing VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration) |
| Changing to **General Purpose** or **Next-gen General Purpose** service tier | [Creating new VM group](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)|
| Changing hardware or maintenance window | [Resizing VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  |
| Enabling zone redundancy | [Creating new VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 4 hours + [time to seed databases](#seeding-duration)   |
| Disabling zone redundancy | [Creating new VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 30 + [time to seed databases](#seeding-duration)    |
| Changing update policy | [Resizing the VM group](management-operations-overview.md#vm-group-operations) <br /> + [Database seeding](management-operations-overview.md#seeding) | 95% of operations finish in 60 minutes + [time to seed databases](#seeding-duration)  |
| <center> ***Delete operations*** </center> |
|Deleting non-last instance<sup>1</sup> |Log tail backup for all databases | 95% of operations finish in 1 minute.  |
|Deleting last instance<sup>2</sup> |Log tail backup for all databases <br /> [Deleting virtual cluster](management-operations-overview.md#vm-group-operations) | 95% of operations finish in 90 minutes |

<sup>1</sup> If there are multiple VM groups in the cluster, deleting the last instance in the group immediately triggers deleting the VM group *asynchronously*.   
<sup>2</sup> Deleting the last instance in the subnet immediately triggers deleting the virtual cluster *synchronously*.

---

Your instance is available for the duration of all management operations, except during the final [failover](management-operations-overview.md#failover) step, when traffic is redirected to the new SQL Database Engine process. In the **Business Critical** service tier, your instance is unavailable for up to 20 seconds, while in the **General Purpose** and **Next-gen General Purpose** service tiers, your instance can be unavailable for up to 2 minutes.

> [!IMPORTANT]
> For update operations that don't complete in place but that result in reattaching the database (such as scaling vCores, scaling memory, changing hardware or the maintenance window), failover duration of databases on the [Next-gen General Purpose service tier](service-tiers-next-gen-general-purpose-use.md) scales with the number of databases, up to 10 minutes. While the instance becomes available after 2 minutes, some databases might be available after a delay. Failover duration is measured from the moment when the first database goes offline, until the moment when the last database comes online. The Next-gen General Purpose service tier increases the maximum number of databases per instance from 100 to 500.

## Seeding duration 

[Seeding](management-operations-overview.md#seeding) is the process of initializing and synchronizing data across SQL Database Engine processes. The duration of seeding primarily depends on the size of the database. On average, seeding proceeds at a rate of approximately 220 GB per hour.

Seeding is executed concurrently through eight parallel channels. At any given time, eight databases are selected for data transfer. As soon as the transfer of one database completes, the next available database is assigned to the now free channel, which ensures continuous and efficient throughput.

The following table provides the following information:
- Likely estimated seeding time for the majority of cases
- Expected maximum estimated seeding time for 95% of cases

| Database size range (GB) | Likely seeding time | Expected maximum seeding time |
|---|---|---|
|0 - 32 GB | 30 minutes | 1 hour | 
|32 - 256 GB | 1.5 hours | 2 hours |
| 256 - 512 GB | 2 hours | 5 hours | 
| 512 - 1024 GB | 5 hours | 9 hours |
| 1024 - 2048 GB | 9 hours | 15 hours |
| 2048 - 3072 GB | 10 hours | 16 hours |
| 3072 - 4096 GB | 12 hours | 18 hours |
| Greater than 4096 GB | 15 hours | 20 hours |



## Related content

- [Quickstart: Create Azure SQL Managed Instance](instance-create-quickstart.md)
- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](../database/features-comparison.md)
- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [Virtual cluster architecture - Azure SQL Managed Instance](virtual-cluster-architecture.md)
- [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance)
