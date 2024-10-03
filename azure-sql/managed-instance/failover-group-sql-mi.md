---
title: Failover groups overview & best practices
description: Failover groups let you manage geo-replication and coordinated failover of all user databases on a managed instance in Azure SQL Managed Instance.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, randolphwest
ms.date: 05/31/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
ms.custom:
  - azure-sql-split
---

# Failover groups overview & best practices - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/failover-group-sql-db.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](failover-group-sql-mi.md?view=azuresql-mi&preserve-view=true)

The failover groups feature allows you to manage the replication and failover of all user databases in a managed instance to another Azure region. This article provides an overview of the failover group feature with best practices and recommendations for using it with Azure SQL Managed Instance.

To get started using the feature, review [Configure a failover group for Azure SQL Managed Instance](failover-group-configure-sql-mi.md).

## Overview

The failover groups feature allows you to manage the replication and failover of user databases in a managed instance to a managed instance in another Azure region. Failover groups are designed to simplify deployment and management of geo-replicated databases at scale.

For more information, see [High availability for Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md). For geo-failover RPO and RTO, see [overview of business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md#rto-and-rpo).

[!INCLUDE [failover-groups-overview](../includes/failover-group-overview.md)]

## <a id="terminology-and-capabilities"></a> Terminology and capabilities

<!--
There's some overlap of content in the following articles, be sure to make changes to all if necessary:
/azure-sql/database/failover-group-sql-db.md
/azure-sql/database/failover-group-configure-sql-db.md
/azure-sql/managed-instance/failover-group-sql-mi.md
/azure-sql/managed-instance/failover-group-configure-sql-mi.md
-->

- **Failover group (FOG)**

  A failover group allows for all user databases within a managed instance to fail over as a unit to another Azure region in case the primary managed instance becomes unavailable due to a primary region outage. Since failover groups for SQL Managed Instance contain all user databases within the instance, only one failover group can be configured on an instance.

  > [!IMPORTANT]  
  > The name of the failover group must be globally unique within the `.database.windows.net` domain.

- **Primary**

  The managed instance that hosts the primary databases in the failover group.

- **Secondary**

  The managed instance that hosts the secondary databases in the failover group. The secondary can't be in the same Azure region as the primary.

  > [!IMPORTANT]  
  > If a database contains in-memory OLTP objects, the primary and secondary geo-replica instance must have matching service tiers, as in-memory OLTP objects reside in memory. A lower service tier on the geo-replica instance can result in out-of-memory issues. If this occurs, the secondary replica might fail to recover the database, causing unavailability of the secondary database along with in-memory OLTP objects on the geo-secondary. This, in turn, could cause failover to be unsuccessful as well. To avoid this, ensure the service tier of the geo-secondary instance matches that of the primary database. Service tier upgrades can be size-of-data operations and can take a while to finish.

[!INCLUDE [failover-group-terminology](../includes/failover-group-terminology.md)]

- **DNS zone**

  A unique ID that is automatically generated when a new SQL Managed Instance is created. A multi-domain (SAN) certificate for this instance is provisioned to authenticate the client connections to any instance in the same DNS zone. The two managed instances in the same failover group must share the DNS zone.

- **Failover group read-write listener**

  A DNS CNAME record that points to the current primary. It's created automatically when the failover group is created and allows the read-write workload to transparently reconnect to the primary when the primary changes after failover. When the failover group is created on a SQL Managed Instance, the DNS CNAME record for the listener URL is formed as `<fog-name>.<zone_id>.database.windows.net`.

- **Failover group read-only listener**

  A DNS CNAME record that points to the current secondary. It's created automatically when the failover group is created and allows the read-only SQL workload to transparently connect to the secondary when the secondary changes after failover. When the failover group is created on a SQL Managed Instance, the DNS CNAME record for the listener URL is formed as `<fog-name>.secondary.<zone_id>.database.windows.net`. By default, failover of the read-only listener is disabled as it ensures the performance of the primary isn't affected when the secondary is offline. However, it also means the read-only sessions won't be able to connect until the secondary is recovered. If you can't tolerate downtime for the read-only sessions and can use the primary for both read-only and read-write traffic at the expense of the potential performance degradation of the primary, you can enable failover for the read-only listener by configuring the `AllowReadOnlyFailoverToPrimary` property. In that case, the read-only traffic is automatically redirected to the primary if the secondary isn't available.

  > [!NOTE]  
  > The `AllowReadOnlyFailoverToPrimary` property only has effect if Microsoft managed failover policy is enabled and a forced failover has been triggered. In that case, if the property is set to True, the new primary will serve both read-write and read-only sessions.

## Failover group architecture

The failover group must be configured on the primary instance and will connect it to the secondary instance in a different Azure region.  All user databases in the instance will be replicated to the secondary instance. System databases like `master` and `msdb` won't be replicated.

The following diagram illustrates a typical configuration of a geo-redundant cloud application using managed instance and failover group:

:::image type="content" source="media/failover-group-sql-mi/failover-group-mi.png" alt-text="diagram of a failover group for Azure SQL Managed Instance.":::

If your application uses SQL Managed Instance as the data tier, follow the general guidelines and best practices outlined in this article when designing for business continuity.

## Create the geo-secondary instance

To ensure noninterrupted connectivity to the primary SQL Managed Instance after failover, both the primary and secondary instances must be in the same DNS zone. It guarantees that the same multi-domain (SAN) certificate can be used to authenticate client connections to either of the two instances in the failover group. When your application is ready for production deployment, create a secondary SQL Managed Instance in a different region, and make sure it shares the DNS zone with the primary SQL Managed Instance. You can do it by specifying an optional parameter during creation. If you're using PowerShell or the REST API, the name of the optional parameter is `DNSZonePartner`. The name of the corresponding optional field in the Azure portal is *Primary Managed Instance*.

> [!IMPORTANT]  
> The first managed instance created in the subnet determines DNS zone for all subsequent instances in the same subnet. This means that two instances from the same subnet can't belong to different DNS zones.

For more information about creating the secondary SQL Managed Instance in the same DNS zone as the primary instance, see [Configure a failover group for Azure SQL Managed Instance](failover-group-configure-sql-mi.md).

## Use paired regions

Deploy both managed instances to [paired regions](/azure/availability-zones/cross-region-replication-azure) for performance reasons. SQL Managed Instance failover groups in paired regions have better performance compared to unpaired regions.

Azure SQL Managed Instance follows a safe deployment practice where Azure paired regions are generally not deployed to at the same time. However, it's not possible to predict which region will be upgraded first, so the order of deployment isn't guaranteed. Sometimes, your primary instance is upgraded first, and sometimes the secondary instance is upgraded first.

In situations where Azure SQL Managed Instance is part of a [failover group](failover-group-sql-mi.md), and the instances in the group aren't in [Azure paired regions](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies), select different maintenance window schedules for your primary and secondary database. For example, select a **Weekday** maintenance window for your geo-secondary database and a **Weekend** maintenance window for your geo-primary database.

## <a id="enabling-replication-traffic-between-two-instances"></a> Enable and optimize geo-replication traffic flow between the instances

Connectivity between the virtual network subnets hosting primary and secondary instance must be established and maintained for uninterrupted geo-replication traffic flow. There are multiple ways to provide connectivity between the instances that you can choose among based on your network topology and policies:

- [Global virtual network peering](/azure/virtual-network/virtual-network-peering-overview)
- [VPN gateways](/azure/vpn-gateway/vpn-gateway-about-vpngateways)
- [Azure ExpressRoute](/azure/expressroute/expressroute-howto-circuit-portal-resource-manager)

[Global virtual network peering (VNet peering)](/azure/virtual-network/virtual-network-peering-overview) is the recommended way to establish connectivity between two instances in a failover group. It provides a low-latency, high-bandwidth private connection between the peered virtual networks using the Microsoft backbone infrastructure. No public Internet, gateways, or additional encryption is required in the communication between the peered virtual networks.

## Initial seeding

When establishing a failover group between managed instances, there's an initial seeding phase before data replication starts. The initial seeding phase is the longest and most expensive part of the operation. Once initial seeding completes data is synchronized, and only subsequent data changes are replicated. The time it takes for the initial seeding to complete depends on the size of data, number of replicated databases, workload intensity on the primary databases, and the speed of the link between the virtual networks hosting primary and secondary instance that mostly depends on the way connectivity is established. Under normal circumstances, and when connectivity is established using recommended global virtual network peering, seeding speed is up to 360 GB an hour for SQL Managed Instance. Seeding is performed for a batch of user databases in parallel - not necessarily for all databases at the same time. Multiple batches might be needed if there are many databases hosted on the instance.

If the speed of the link between the two instances is slower than what is necessary, the time to seed is likely to be noticeably affected. You can use the stated seeding speed, number of databases, total size of data, and the link speed to estimate how long the initial seeding phase will take before data replication starts. For example, for a single 100-GB database, the initial seed phase would take about 1.2 hours if the link is capable of pushing 84 GB per hour, and if there are no other databases being seeded. If the link can only transfer 10 GB per hour, then seeding a 100-GB database can take about 10 hours. If there are multiple databases to replicate, seeding will be executed in parallel, and, when combined with a slow link speed, the initial seeding phase might take considerably longer, especially if the parallel seeding of data from all databases exceeds the available link bandwidth.

> [!IMPORTANT]  
> In case of an extremely low-speed or busy link causing the initial seeding phase to take days the creation of a failover group can time out. The creation process will be automatically canceled after 6 days.

## Manage geo-failover to a geo-secondary instance

The failover group manages geo-failover of all databases on the primary managed instance. When a group is created, each database in the instance will be automatically geo-replicated to the geo-secondary instance. You can't use failover groups to initiate a partial failover of a subset of databases.

> [!IMPORTANT]  
> If a database is dropped on the primary managed instance, it will also be dropped automatically on the geo-secondary managed instance.

## Use the read-write listener (primary MI)

For read-write workloads, use `<fog-name>.zone_id.database.windows.net` as the server name. Connections are automatically directed to the primary. This name doesn't change after failover. The geo-failover involves updating the DNS record, so the new client connections are routed to the new primary only after the client DNS cache is refreshed. Because the secondary instance shares the DNS zone with the primary, the client application will be able to reconnect to it using the same server-side SAN certificate. The existing client connections need to be terminated and then recreated to be routed to the new primary. The read-write listener and read-only listener can't be reached via the [public endpoint for managed instance](public-endpoint-configure.md).

## Use the read-only listener (secondary MI)

If you have logically isolated read-only workloads that are tolerant to data latency, you can run them on the geo-secondary. To connect directly to the geo-secondary, use `<fog-name>.secondary.<zone_id>.database.windows.net` as the server name.

In the Business Critical tier, SQL Managed Instance supports the use of [read-only replicas](../database/read-scale-out.md) to offload read-only query workloads, using the `ApplicationIntent=ReadOnly` parameter in the connection string. When you have configured a geo-replicated secondary, you can use this capability to connect to either a read-only replica in the primary location or in the geo-replicated location:

- To connect to a read-only replica in the primary location, use `ApplicationIntent=ReadOnly` and `<fog-name>.<zone_id>.database.windows.net`.
- To connect to a read-only replica in the secondary location, use `ApplicationIntent=ReadOnly` and `<fog-name>.secondary.<zone_id>.database.windows.net`.

The read-write listener and read-only listener can't be reached via [public endpoint for managed instance](public-endpoint-configure.md).

## Potential performance degradation after failover

A typical Azure application uses multiple Azure services and consists of multiple components. Geo-failover of the group is triggered based on the state of the Azure SQL components alone. Other Azure services in the primary region might not be affected by the outage and their components might still be available in that region. Once the primary databases switch to the secondary region, the latency between the dependent components can increase. Ensure the redundancy of all the application's components in the secondary region and fail over application components together with the database so that application's performance isn't affected by higher cross-region latency.

## Potential data loss after forced failover

If an outage occurs in the primary region, recent transactions might not have been replicated to the geo-secondary and there might be data loss if a forced failover is performed.

## DNS update

The DNS update of the read-write listener will happen immediately after the failover is initiated. This operation won't result in data loss. However, the process of switching database roles can take up to 5 minutes under normal conditions. Until it's completed, some databases in the new primary instance will still be read-only. If a failover is initiated using PowerShell, the operation to switch the primary replica role is synchronous. If it's initiated using the Azure portal, the UI indicates completion status. If it's initiated using the REST API, use standard Azure Resource Manager's polling mechanism to monitor for completion.

> [!IMPORTANT]  
> Use manual planned failover to move the primary back to the original location once the outage that caused the geo-failover is mitigated.

## Save costs with a license-free DR replica

You can save on SQL Server license costs by configuring your secondary managed instance to be used for disaster recovery (DR) only. To set this up, see [Configure a license-free standby replica for Azure SQL Managed Instance](failover-group-standby-replica-how-to-configure.md).

As long as the secondary instance isn't used for read-workloads, Microsoft provides you with a free number of vCores to match the primary instance. You're still charged for compute and storage used by the secondary instance. Failover groups support only one replica - the replica must either be a readable replica, or designated as a DR-only replica.

## Enable scenarios dependent on objects from the system databases

<!--
This section is duplicated in /managed-instance/failover-group-configure-sql-mi.md. Please ensure changes are made to both documents.
-->

System databases are **not** replicated to the secondary instance in a failover group. To enable scenarios that depend on objects from the system databases, make sure to create the same objects on the secondary instance and keep them synchronized with the primary instance.

For example, if you plan to use the same logins on the secondary instance, make sure to create them with the identical SID.

```SQL
-- Code to create login on the secondary instance
CREATE LOGIN foo WITH PASSWORD = '<enterStrongPasswordHere>', SID = <login_sid>;
```

To learn more, see [Replication of logins and agent jobs](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/azure-sql-managed-instance-sync-agent-jobs-and-logins-in/ba-p/2860495).

## Synchronize instance properties and retention policies instances

<!--
This section is duplicated in /managed-instance/failover-group-configure-sql-mi.md. Please ensure changes are made to both documents.  
-->

Instances in a failover group remain separate Azure resources, and no changes made to the configuration of the primary instance will be automatically replicated to the secondary instance. Make sure to perform all relevant changes both on primary _and_ secondary instance. For example, if you change backup storage redundancy or long-term backup retention policy on primary instance, make sure to change it on secondary instance as well.

## Scale instances

<!--
This section is duplicated in /managed-instance/failover-group-configure-sql-mi.md. Please ensure changes are made to both documents.
-->

You can scale up or scale down the primary and secondary instance to a different compute size within the same service tier or to a different service tier. When scaling up within the same service tier, we recommend that you scale up the geo-secondary first, and then scale up the primary. When scaling down within the same service tier, reverse the order: scale down the primary first, and then scale down the secondary. When you scale instance to a different service tier, this recommendation is enforced. The sequence of operations is enforced when scaling the service tier and vCores, as well as storage.

The sequence is recommended specifically to avoid the problem where the geo-secondary at a lower SKU gets overloaded and must be reseeded during an upgrade or downgrade process.

> [!IMPORTANT] 
> - For instances inside of a failover group, changing the service tier to, or from, the Next-gen General Purpose tier is not supported. You must first delete the failover group before modifying either replica, and then re-create the failover group after the change takes effect.
> - There's a [known issue](doc-changes-updates-known-issues.md#temporary-instance-inaccessibility-using-the-failover-group-listener-during-scaling-operation) which can impact accessibility of the instance being scaled using the associated failover group listener. 


## Prevent loss of critical data

<!--
There's some overlap in the following content, be sure to update all that's necessary:
/azure-sql/database/failover-group-sql-db.md
/azure-sql/managed-instance/failover-group-sql-mi.md
-->

Due to the high latency of wide area networks, geo-replication uses an asynchronous replication mechanism. Asynchronous replication makes the possibility of data loss unavoidable if the primary fails. To protect critical transactions from data loss, an application developer can call the [sp_wait_for_database_copy_sync](/sql/relational-databases/system-stored-procedures/sp-wait-for-database-copy-sync-transact-sql) stored procedure immediately after committing the transaction. Calling `sp_wait_for_database_copy_sync` blocks the calling thread until the last committed transaction has been transmitted and hardened in the transaction log of the secondary database. However, it doesn't wait for the transmitted transactions to be replayed (redone) on the secondary. `sp_wait_for_database_copy_sync` is scoped to a specific geo-replication link. Any user with the connection rights to the primary database can call this procedure.

To prevent data loss during user-initiated, planned geo-failover, replication automatically and temporarily changes to synchronous replication, then performs a failover. Replication then returns to asynchronous mode after the geo-failover is complete.

> [!NOTE]  
> `sp_wait_for_database_copy_sync` prevents data loss after geo-failover for specific transactions, but doesn't guarantee full synchronization for read access. The delay caused by a `sp_wait_for_database_copy_sync` procedure call can be significant and depends on the size of the not yet transmitted transaction log on the primary at the time of the call.

## Failover group status

Failover group reports its status describing the current state of the data replication:

- Seeding - [Initial seeding](failover-group-sql-mi.md#initial-seeding) is taking place after creation of the failover group, until all user databases are initialized on the secondary instance. Failover process can't be initiated while failover group is in the Seeding status, since user databases aren't copied to secondary instance yet.
- Synchronizing - the usual status of failover group. It means that data changes on the primary instance are being replicated asynchronously to the secondary instance. This status doesn't guarantee that the data is fully synchronized at every moment. There can be data changes from the primary still to be replicated to the secondary due to the asynchronous nature of the replication process between instances in the failover group. Both automatic and manual failovers can be initiated while the failover group is in the Synchronizing status.
- Failover in progress - this status indicates that either automatically or manually initiated failover process is in progress. No changes to the failover group or additional failovers can be initiated while the failover group is in this status.

## Failback

When failover groups are configured with a Microsoft-managed failover policy, then forced failover to the geo-secondary server is initiated during a disaster scenario as per the defined grace period. Failback to the old primary must be initiated manually.

## Feature interoperability 

### Backups 

A full backup is taken in the following scenarios: 
- Before initial seeding starts when you create a failover group. 
- After a failover. 

A full backup is a size of data operation that can't be skipped or deferred, and can take some time complete. The time it takes to complete depends on the size of data, the number of databases, and the workload intensity on the primary databases. A full backup can noticeably delay initial seeding, and can either delay or prevent a failover operation on a new instance shortly after a failover. 

### Log Replay Service 

Databases migrated to Azure SQL Managed Instance by using the [Log Replay Service (LRS)](log-replay-service-overview.md) can't be added to a failover group until the cutover step is executed. A database migrated with LRS is in a restoring state until cutover, and databases in a restoring state can't be added to a failover group. Attempting to create a failover group with a database in a restoring state delays creating the failover group until the database restore completes. 

### Transactional replication

Using transactional replication with instances that are in a failover group is supported. However, if you configure replication before adding your SQL managed instance into a failover group, replication pauses when you start to create your failover group, and replication monitor shows a status of `Replicated transactions are waiting for the next log backup or for mirroring partner to catch up`. Replication resumes once the failover group is created successfully.

If a **publisher** or **distributor** SQL managed instance is in a failover group, the SQL managed instance administrator must clean up all publications on the old primary and reconfigure them on the new primary after a failover occurs. Review the [transactional replication guide](replication-transactional-overview.md#with-failover-groups) for the step of activities that are needed in this scenario.

## Permissions and limitations 

Review a list of [permissions](failover-group-configure-sql-mi.md#permissions) and [limitations](failover-group-configure-sql-mi.md#limitations) before configuring a failover group.

## Programmatically manage failover groups

Failover groups can also be managed programmatically using Azure PowerShell, Azure CLI, and REST API. Review [configure failover group](failover-group-configure-sql-mi.md) to learn more.

## Disaster recovery drills

The recommended way to perform a DR drill is using the manual planned failover, as per the following tutorial: [Test failover](failover-group-configure-sql-mi.md#test-failover).

Performing a drill using forced failover is **not recommended**, as this operation doesn't provide guardrails against the data loss. Nevertheless, it's possible to achieve data lossless forced failover by ensuring the following conditions are met prior to initiating the forced failover:

- The workload is stopped on the primary managed instance.
- All long running transactions have completed.
- All client connections to the primary managed instance have been disconnected.
- [Failover group status](failover-group-sql-mi.md#failover-group-status) is 'Synchronizing'.

Please ensure the two managed instances have switched roles and that the failover group status has switched from 'Failover in progress' to 'Synchronizing' before optionally establishing connections to the new primary managed instance and starting read-write workload.

To perform a data lossless failback to the original managed instance roles, using manual planned failover instead of forced failover is **strongly recommended**. To proceed with forced failback:

- Follow the same steps as for the data lossless failover.
- Longer failback execution time is expected if the forced failback is executed **shortly after** the initial forced failover is completed, as it has to wait for completion of outstanding automatic backup operations on the former primary managed instance.

## Related content

- [Configure a failover group](failover-group-configure-sql-mi.md)
- [Use PowerShell to add a managed instance to a failover group](scripts/add-to-failover-group-powershell.md)
- [Configure a license-free standby replica for Azure SQL Managed Instance](failover-group-standby-replica-how-to-configure.md)
- [Overview of business continuity with Azure SQL Managed Instance](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Automated backups in Azure SQL Managed Instance](automated-backups-overview.md)
- [Restore a database from a backup in Azure SQL Managed Instance](recovery-using-backups.md)
