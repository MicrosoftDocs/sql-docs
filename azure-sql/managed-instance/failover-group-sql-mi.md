---
title: Failover Groups Overview & Best Practices
description: Failover groups let you manage geo-replication and coordinated failover of all user databases on Azure SQL Managed Instance.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, randolphwest
ms.date: 09/11/2025
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: concept-article
ms.custom:
  - azure-sql-split
---

# Failover groups overview & best practices - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/failover-group-sql-db.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](failover-group-sql-mi.md?view=azuresql-mi&preserve-view=true)

This article provides an overview of the failover group feature with best practices and recommendations to use with Azure SQL Managed Instance. The failover groups feature allows you to manage the replication and failover of all user databases in a SQL managed instance to another Azure region. 

To get started, review [Configure a failover group for Azure SQL Managed Instance](failover-group-configure-sql-mi.md).

## Overview

The failover groups feature allows you to manage the replication and failover of user databases from one SQL managed instance to another SQL managed instance in a different Azure region. Failover groups are designed to simplify deployment and management of geo-replicated databases at scale.

For more information, see [High availability for Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md). For geo-failover RPO and RTO, see [overview of business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md#rto-and-rpo).

[!INCLUDE [failover-groups-overview](../includes/failover-group-overview.md)]

<a id="terminology-and-capabilities"></a>

## Terminology and capabilities

<!--
There's some overlap of content in the following articles, be sure to make changes to all if necessary:
/azure-sql/database/failover-group-sql-db.md
/azure-sql/database/failover-group-configure-sql-db.md
/azure-sql/managed-instance/failover-group-sql-mi.md
/azure-sql/managed-instance/failover-group-configure-sql-mi.md
-->

- **Failover group (FOG)**

  A failover group allows for all user databases within a SQL managed instance to fail over as a unit to another Azure region in case the primary SQL managed instance becomes unavailable due to a primary region outage. Since failover groups for SQL Managed Instance contain all user databases within the instance, only one failover group can be configured on an instance.

  > [!IMPORTANT]  
  > The name of the failover group must be globally unique within the `.database.windows.net` domain.

- **Primary**

  The SQL managed instance that hosts the primary databases in the failover group.

- **Secondary**

  The SQL managed instance that hosts the secondary databases in the failover group. The secondary can't be in the same Azure region as the primary.

  > [!IMPORTANT]  
  > If a database contains in-memory OLTP objects, the primary and secondary geo-replica instance must have matching service tiers, as in-memory OLTP objects reside in memory. A lower service tier on the geo-replica instance can result in out-of-memory issues. If this occurs, the secondary replica might fail to recover the database, causing unavailability of the secondary database along with in-memory OLTP objects on the geo-secondary. This, in turn, could cause failover to be unsuccessful as well. To avoid this, ensure the service tier of the geo-secondary instance matches that of the primary database. Service tier upgrades can be size-of-data operations and can take a while to finish.

[!INCLUDE [failover-group-terminology](../includes/failover-group-terminology.md)]

- **DNS zone**

  A unique ID that is automatically generated when a new SQL Managed Instance is created. A multi-domain (SAN) certificate for this instance is provisioned to authenticate the client connections to any instance in the same DNS zone. The two SQL managed instances in the same failover group must share the DNS zone.

- **Failover group read-write listener**

  A DNS CNAME record that points to the current primary. It's created automatically when the failover group is created and allows the read-write workload to transparently reconnect to the primary when the primary changes after failover. When the failover group is created on a SQL managed instance, the DNS CNAME record for the listener URL is formed as `<fog-name>.<zone_id>.database.windows.net`.

- **Failover group read-only listener**

  A DNS CNAME record that points to the current secondary. It's created automatically when the failover group is created and allows the read-only SQL workload to transparently connect to the secondary when the secondary changes after failover. When the failover group is created on a SQL managed instance, the DNS CNAME record for the listener URL is formed as `<fog-name>.secondary.<zone_id>.database.windows.net`. By default, failover of the read-only listener is disabled as it ensures the performance of the primary isn't affected when the secondary is offline. However, it also means the read-only sessions won't be able to connect until the secondary is recovered. 

## Failover group architecture

The failover group must be configured on the primary instance and connects it to the secondary instance in a different Azure region. All user databases on the primary instance are replicated to the secondary instance. System databases like `master` and `msdb` aren't replicated.

The following diagram illustrates a typical configuration of a geo-redundant cloud application using a SQL managed instance and failover group:

:::image type="content" source="media/failover-group-sql-mi/failover-group-mi.png" alt-text="diagram of a failover group for Azure SQL Managed Instance.":::

If your application uses SQL Managed Instance as the data tier, follow the general guidelines and best practices outlined in this article when designing for business continuity.

## Create the geo-secondary instance

To ensure uninterrupted connectivity to the primary SQL Managed Instance after failover, both the primary and secondary instances must be in the same DNS zone. It guarantees that the same multi-domain (SAN) certificate can be used to authenticate client connections to either of the two instances in the failover group. When your application is ready for production deployment, create a secondary SQL Managed Instance in a different region, and make sure it shares the DNS zone with the primary SQL Managed Instance. You can do so by specifying an optional parameter when you create the instance. If you're using PowerShell or the REST API, the name of the optional parameter is `DNSZonePartner`. The name of the corresponding optional field in the Azure portal is *Primary Managed Instance*.

> [!IMPORTANT]  
> The first SQL managed instance created in the subnet determines the DNS zone for all subsequent instances in the same subnet. This means that two instances from the same subnet can't belong to different DNS zones.

For more information about creating the secondary SQL Managed Instance in the same DNS zone as the primary instance, see [Configure a failover group for Azure SQL Managed Instance](failover-group-configure-sql-mi.md).

## Use paired regions

Deploy both SQL managed instances to [paired regions](/azure/reliability/cross-region-replication-azure) for performance reasons. SQL Managed Instance failover groups in paired regions have better performance compared to unpaired regions.

Azure SQL Managed Instance follows a safe deployment practice where Azure paired regions are generally not deployed to at the same time. However, it's not possible to predict which region is upgraded first, so the order of deployment isn't guaranteed. Sometimes, your primary instance is upgraded first, and sometimes the secondary instance is upgraded first.

In situations where Azure SQL Managed Instance is part of a [failover group](failover-group-sql-mi.md), and the instances in the group aren't in [Azure paired regions](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies), select different maintenance window schedules for your primary and secondary database. For example, select a **Weekday** maintenance window for your geo-secondary database and a **Weekend** maintenance window for your geo-primary database.

<a id="enabling-replication-traffic-between-two-instances"></a>

## Enable and optimize geo-replication traffic flow between the instances

Connectivity between the virtual network subnets hosting the primary and secondary instance must be established and maintained for uninterrupted geo-replication traffic flow. There are multiple ways to provide connectivity between the instances that you can choose from based on your network topology and policies:

- [Global virtual network peering](/azure/virtual-network/virtual-network-peering-overview)
- [VPN gateways](/azure/vpn-gateway/vpn-gateway-about-vpngateways)
- [Azure ExpressRoute](/azure/expressroute/expressroute-howto-circuit-portal-resource-manager)

[Global virtual network peering (VNet peering)](/azure/virtual-network/virtual-network-peering-overview) is the recommended way to establish connectivity between two instances in a failover group. It provides a low-latency, high-bandwidth private connection between the peered virtual networks using the Microsoft backbone infrastructure. No public internet, gateways, or additional encryption is required in the communication between the peered virtual networks.

## Initial seeding

When establishing a failover group between SQL managed instances, there's an initial seeding phase before data replication starts. The initial seeding phase is the longest and most expensive part of the operation. Once initial seeding completes, data is synchronized, and only subsequent data changes are replicated. The time it takes for the initial seeding to complete depends on the size of data, number of replicated databases, workload intensity on the primary databases, and the speed of the link between the virtual networks that hosts the primary and secondary instance, which mostly depends on the way connectivity is established. Under normal circumstances, and when connectivity is established using recommended global virtual network peering, seeding speed is up to 360 GB an hour for SQL Managed Instance. Seeding is performed for a batch of user databases in parallel but not necessarily for all databases at the same time. Multiple batches might be needed if there are many databases hosted on the instance.

If the speed of the link between the two instances is slower than what is necessary, the time to seed is likely to be noticeably affected. You can use the stated seeding speed, number of databases, total size of data, and the link speed to estimate how long the initial seeding phase takes before data replication starts. For example, for a single 100-GB database, the initial seed phase would take about 1.2 hours if the link is capable of pushing 84 GB per hour, and if there are no other databases being seeded. If the link can only transfer 10 GB per hour, then seeding a 100-GB database can take about 10 hours. If there are multiple databases to replicate, seeding is executed in parallel. When combined with a slow link speed, the initial seeding phase might take considerably longer, especially if the parallel seeding of data from all databases exceeds the available link bandwidth.

> [!IMPORTANT]  
> The initial seeding phase can take days with extremely low-speed or busy links. In this case, creating the failover group can time out. Creating the failover group is automatically canceled after six days.

## Manage geo-failover to a geo-secondary instance

The failover group manages geo-failover of all databases on the primary SQL managed instance. When a group is created, each database on the instance is automatically geo-replicated to the geo-secondary instance. You can't use failover groups to initiate a partial failover of a subset of databases.

> [!IMPORTANT]  
> If a database is dropped on the primary SQL managed instance, it's also automatically dropped on the geo-secondary SQL managed instance.

## Use the read-write listener (primary MI)

For read-write workloads, use `<fog-name>.zone_id.database.windows.net` as the server name. Connections are automatically directed to the primary. This name doesn't change after failover. The geo-failover involves updating the DNS record, so new client connections are routed to the new primary only after the client DNS cache is refreshed. Because the secondary instance shares the DNS zone with the primary, the client application will be able to reconnect to it using the same server-side SAN certificate. The existing client connections need to be terminated and then recreated to be routed to the new primary. The read-write listener and read-only listener can't be reached via the [Public endpoint for SQL managed instance](public-endpoint-configure.md).

## Use the read-only listener (secondary MI)

If you have logically isolated read-only workloads that are tolerant to data latency, you can run them on the geo-secondary. To connect directly to the geo-secondary, use `<fog-name>.secondary.<zone_id>.database.windows.net` as the server name.

In the Business Critical tier, SQL Managed Instance supports the use of [read-only replicas](../database/read-scale-out.md) to offload read-only query workloads, using the `ApplicationIntent=ReadOnly` parameter in the connection string. When you have configured a geo-replicated secondary, you can use this capability to connect to either a read-only replica in the primary location or in the geo-replicated location:

- To connect to a read-only replica in the primary location, use `ApplicationIntent=ReadOnly` and `<fog-name>.<zone_id>.database.windows.net`.
- To connect to a read-only replica in the secondary location, use `ApplicationIntent=ReadOnly` and `<fog-name>.secondary.<zone_id>.database.windows.net`.

The read-write listener and read-only listener can't be reached via [Public endpoint for SQL managed instance](public-endpoint-configure.md).

## Potential performance degradation after failover

A typical Azure application uses multiple Azure services and consists of multiple components. Geo-failover of the group is triggered based on the state of the Azure SQL components alone. An outage might not affect other Azure services in the primary region, and their components might still be available in that region. Once the primary databases switch to the secondary region, the latency between the dependent components can increase. Ensure the redundancy of all the application's components in the secondary region and fail over application components together with the database so that higher cross-region latency doesn't affect an application's performance.

## Potential data loss after forced failover

If an outage occurs in the primary region, recent transactions might not have been replicated to the geo-secondary, and there might be data loss if a forced failover is performed.

## DNS update

The DNS update of the read-write listener will happen immediately after the failover is initiated. This operation won't result in data loss. However, the process of switching database roles can take up to five minutes under normal conditions. Until it's completed, some databases in the new primary instance will still be read-only. If a failover is initiated using PowerShell, the operation to switch the primary replica role is synchronous. If it's initiated using the Azure portal, the UI indicates completion status. If it's initiated using the REST API, use standard Azure Resource Manager's polling mechanism to monitor for completion.

> [!IMPORTANT]  
> Use manual planned failover to move the primary back to the original location once the outage that caused the geo-failover is mitigated.

## Save costs with a license-free DR replica

You can save on SQL Server license costs by configuring your secondary SQL managed instance to be used for disaster recovery (DR) only. To set this up, see [Configure a license-free standby replica for Azure SQL Managed Instance](failover-group-standby-replica-how-to-configure.md).

As long as the secondary instance isn't used for read-workloads, Microsoft provides you with a free number of vCores to match the primary instance. You're still charged for compute and storage used by the secondary instance. Failover groups support only one replica, and the replica must either be a readable replica or designated as a DR-only replica.

## Enable scenarios dependent on objects from the system databases

<!--
This section is duplicated in /managed-instance/failover-group-configure-sql-mi.md. Please ensure changes are made to both documents.
-->

System databases are **not** replicated to the secondary instance in a failover group. To enable scenarios that depend on objects from the system databases, make sure to create the same objects on the secondary instance. Keep them synchronized with the primary instance.

For example, if you plan to use the same logins on the secondary instance, make sure to create them with the identical `SID`.

```SQL
-- Code to create login on the secondary instance
CREATE LOGIN foo WITH PASSWORD = '<password>', SID = <login_sid>;
```

To learn more, see [Replication of logins and agent jobs](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/azure-sql-managed-instance-sync-agent-jobs-and-logins-in/ba-p/2860495).

## Synchronize instance properties and retention policies instances

<!--
This section is duplicated in /managed-instance/failover-group-configure-sql-mi.md. Please ensure changes are made to both documents.
-->

Instances in a failover group remain separate from Azure resources, and no changes made to the configuration of the primary instance are automatically replicated to the secondary instance. Make sure to perform all relevant changes on both the primary *and* secondary instance. For example, if you change backup storage redundancy or long-term backup retention policy on the primary instance, be sure to change it on the secondary instance as well.

## Scale instances

The configuration of your primary and secondary instance should be the same. This includes the compute size, storage size, and service tier. If you need to change the configuration of your failover group, you can do so by scaling each instance to the same configuration accordingly. To learn more, review [Scaling instances in a failover group](failover-group-configure-sql-mi.md#scaling-instances).

## Prevent loss of critical data

<!--
There's some overlap in the following content, be sure to update all that's necessary:
/azure-sql/database/failover-group-sql-db.md
/azure-sql/managed-instance/failover-group-sql-mi.md
-->

Due to the high latency of wide area networks, geo-replication uses an asynchronous replication mechanism. Asynchronous replication makes the possibility of data loss unavoidable if the primary fails. To learn how you can protect your data, review [Prevent data loss](failover-group-configure-sql-mi.md#prevent-loss-of-critical-data).

## Failover group status

Failover group reports its status describing the current state of data replication:

- **Seeding**: [Initial seeding](failover-group-sql-mi.md#initial-seeding) takes place after the failover group is created, until all user databases are initialized on the secondary instance. Failover can't be initiated while the failover group is in the *Seeding* state, since user databases aren't copied to the secondary instance yet.
- **Synchronizing**: The usual status of failover group. It means that data changes on the primary instance are being replicated asynchronously to the secondary instance. This status doesn't guarantee that the data is fully synchronized at every moment. There can be data changes from the primary still to be replicated to the secondary due to the asynchronous nature of the replication process between instances in a failover group. Both automatic and manual failovers can be initiated while the failover group is in the *Synchronizing* state.
- **Failover in progress**: This status indicates that either automatically or manually initiated failover is in progress. No changes to the failover group or additional failovers can be initiated while the failover group is in this state.

## Failback

When failover groups are configured with a Microsoft-managed failover policy, forced failover to the geo-secondary server is initiated during a disaster scenario as per the defined grace period. Failback to the old primary must be initiated manually.

## Feature interoperability

### Backups

A full backup is taken in the following scenarios:
- Before initial seeding starts when you create a failover group.
- After a failover.

A full backup is a size of data operation that can't be skipped or deferred, and can take some time complete. The time it takes to complete depends on the size of data, the number of databases, and the workload intensity on the primary databases. A full backup can noticeably delay initial seeding, and can either delay or prevent a failover operation on a new instance shortly after a failover.

Consider the following:

- Databases hosted on the secondary instance of a failover group aren't backed up until that instance becomes primary after a failover, or until the failover group is dropped.
- After a database changes to the primary role after a failover, or becomes standalone after a failover group is dropped, a full database backup is automatically initiated to facilitate point-in-time restores.
- A database can't be restored from an instance to a point in time when that instance was a secondary replica in a failover group. To restore to a point in time, you must restore the database from the instance that was primary during that point in time.
- To recreate a dropped failover group on the same pair of SQL managed instances, all user databases need to be removed from the intended secondary after the failover group is dropped. A database is only fully removed after all pending backup operations complete, including a full backup if one wasn't taken (which is size-of-data operation). Allow time to clean up the secondary instance after dropping a failover group with very large databases, as each database can have a pending full backup operation in progress.

A full backup is a size of data operation that can't be skipped or deferred and can take some time complete. The time it takes to complete depends on the size of data, the number of databases, and the workload intensity on the primary databases. A full backup can noticeably delay initial seeding and can either delay or prevent a failover operation on a new instance shortly after a failover.

### Log Replay Service

Databases migrated to Azure SQL Managed Instance by using the [Log Replay Service (LRS)](log-replay-service-overview.md) can't be added to a failover group until the cutover step is executed. A database migrated with LRS is in a restoring state until cutover, and databases in a restoring state can't be added to a failover group. Attempting to create a failover group with a database in a restoring state delays creating the failover group until the database restore completes.

### Transactional replication

Using transactional replication with instances that are in a failover group is supported. However, if you configure replication before adding your SQL managed instance into a failover group, replication pauses when you start to create your failover group. Replication monitor shows a status of `Replicated transactions are waiting for the next log backup or for mirroring partner to catch up`. Replication resumes once the failover group is created successfully.

If a **publisher** or **distributor** SQL managed instance is in a failover group, the SQL managed instance administrator must clean up all publications on the old primary and reconfigure them on the new primary after a failover occurs. Review the [transactional replication guide](replication-transactional-overview.md#with-failover-groups) for the step of activities that are needed in this scenario.

## Permissions and limitations

Review a list of [permissions](failover-group-configure-sql-mi.md#permissions) and [limitations](failover-group-configure-sql-mi.md#limitations) before configuring a failover group.

## Programmatically manage failover groups

Failover groups can also be managed programmatically using Azure PowerShell, Azure CLI, and REST API. Review [Configure failover group](failover-group-configure-sql-mi.md) to learn more.

## Disaster recovery drills

The recommended way to perform a DR drill is using the manual planned failover, as per the following tutorial: [Test failover](failover-group-configure-sql-mi.md#test-failover).

Performing a drill using forced failover is **not recommended**, as this operation doesn't provide guardrails against data loss. Nevertheless, it's possible to achieve data lossless forced failover by ensuring the following conditions are met prior to initiating the forced failover:

- The workload is stopped on the primary SQL managed instance.
- All long running transactions have completed.
- All client connections to the primary SQL managed instance have been disconnected.
- [Failover group status](failover-group-sql-mi.md#failover-group-status) is `Synchronizing` on **both** the primary and secondary instances.

Please ensure the two SQL managed instances have switched roles. Also that the failover group status has switched from *'Failover in progress'* to *'Synchronizing'* before optionally establishing connections to the new primary SQL managed instance and starting read-write workload.

To perform a data lossless failback to the original SQL managed instance roles, using manual planned failover instead of forced failover is **strongly recommended**. 

If forced failback is used:

- Follow the same steps as for the data lossless failover.
- Forced failback is expected to fail if a previous forced failover did not succeed on both SQL managed instances. Ensure failover group status is `Synchronizing` on **both** instances before executing the forced failback.
- Longer failback execution time is expected if the forced failback is executed **shortly after** the initial forced failover is completed, as it has to wait for completion of outstanding automatic backup operations on the former primary SQL managed instance.
- Any outstanding automatic backup operations on an instance transitioning from the primary to the secondary role can affect database availability on this instance.
- Please use the failover group status to determine whether both instances have successfully changed their roles and are ready to accept client connections.

## Related content

- [Configure a failover group for Azure SQL Managed Instance](failover-group-configure-sql-mi.md)
- [Use PowerShell to add a SQL managed instance to a failover group](scripts/add-to-failover-group-powershell.md)
- [Configure a license-free standby replica for Azure SQL Managed Instance](failover-group-standby-replica-how-to-configure.md)
- [Overview of business continuity with Azure SQL Managed Instance](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Automated backups in Azure SQL Managed Instance](automated-backups-overview.md)
- [Restore a database from a backup in Azure SQL Managed Instance](recovery-using-backups.md)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
