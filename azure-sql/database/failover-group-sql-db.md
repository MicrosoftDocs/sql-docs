---
title: Failover groups overview & best practices
description: Failover groups let you manage geo-replication and automatic / coordinated failover of a group of databases on a server for both single and pooled database in Azure SQL Database.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: wiassaf, mathoma
ms.date: 01/11/2024
ms.service: sql-database
ms.subservice: high-availability
ms.topic: conceptual
ms.custom:
  - azure-sql-split
---

# Failover groups overview & best practices (Azure SQL Database)
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](failover-group-sql-db.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/failover-group-sql-mi.md?view=azuresql-mi&preserve-view=true)

The failover groups feature allows you to manage the replication and failover of some or all databases on a [logical server](logical-servers.md) to a logical server in another region. This article provides an overview of the failover group feature with best practices and recommendations for using it with Azure SQL Database.  

To get started using the feature, review [Configure failover group](failover-group-configure-sql-db.md).

> [!NOTE]
> This article covers failover groups for Azure SQL Database. For Azure SQL Managed Instance, see [Failover groups in Azure SQL Managed Instance](../managed-instance/failover-group-sql-mi.md).

To learn more about Azure SQL Database disaster recovery, watch this video:

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=f02c00dc-de7d-4d27-a087-41c562c214d7]

## Overview

The failover groups feature allows you to manage the replication and failover of databases to another Azure region. You can choose all, or a subset of, user databases in a logical server to be replicated to another logical server. It's a declarative abstraction on top of the [active geo-replication](../database/active-geo-replication-overview.md) feature, designed to simplify deployment and management of geo-replicated databases at scale.

For geo-failover RPO and RTO, see [overview of business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md#rto-and-rpo).


[!INCLUDE [failover-groups-overview](../includes/failover-group-overview.md)]


## <a id="terminology-and-capabilities"></a> Terminology and capabilities

<!--
There is some overlap of content in the following articles, be sure to make changes to all if necessary:
/azure-sql/database/failover-group-sql-db.md
/azure-sql/managed-instance/afailover-group-sql-mi.md
-->


- **Failover group (FOG)**

  A failover group is a named group of databases managed by a single [logical server in Azure](logical-servers.md) that can fail over as a unit to another Azure region in case all or some primary databases become unavailable due to an outage in the primary region. 
  
  > [!IMPORTANT]
  > The name of the failover group must be globally unique within the `.database.windows.net` domain.

- **Servers**

  Some or all of the user databases on a logical server can be placed in a failover group. Also, a server supports multiple failover groups on a single server.

- **Primary**

  The logical server that hosts the primary databases in the failover group.

- **Secondary**

  The logical server that hosts the secondary databases in the failover group. The secondary can't be in the same Azure region as the primary.

[!INCLUDE [failover-group-terminology](../includes/failover-group-terminology.md)]

- **Adding single databases to failover group**

  You can put several single databases on the same logical server into the same failover group. If you add a single database to the failover group, it automatically creates a secondary database using the same edition and compute size on secondary server you specified when the failover group was created. If you add a database that already has a secondary database in the secondary server, that geo-replication link is inherited by the group. When you add a database that already has a secondary database in a server that isn't part of the failover group, a new secondary database is created on the secondary server.

  > [!IMPORTANT]
  > - Make sure the secondary logical server doesn't have a database with the same name unless it is an existing secondary database. 
  > - If a database contains in-memory OLTP objects, the primary database and the secondary geo-replica database must have matching service tiers, as in-memory OLTP objects reside in memory. A lower service tier on the geo-replica database might result in out-of-memory issues. If this occurs, the geo-replica might fail to recover the database, causing unavailability of the secondary database along with in-memory OLTP objects on the geo-secondary. This, in turn, can cause failovers to be unsuccessful as well. To avoid this, ensure that the service tier of the geo-secondary database matches that of the primary database. Service tier upgrades can be size-of-data operations and might take a while to finish.

- **Adding databases in elastic pool to failover group**

  You can put all or several databases within an elastic pool into the same failover group. If the primary database is in an elastic pool, the secondary is automatically created in the elastic pool with the same name (secondary pool). You must ensure that the secondary server contains an elastic pool with the same exact name and enough free capacity to host the secondary databases that will be created by the failover group. If you add a database in the pool that already has a secondary database in the secondary pool, that geo-replication link is inherited by the group. When you add a database that already has a secondary database in a server that isn't part of the failover group, a new secondary database is created in the secondary pool.
  
- **Failover group read-write listener**

  A DNS CNAME record that points to the current primary. It's created automatically when the failover group is created and allows the read-write workload to transparently reconnect to the primary when the primary changes after failover. When the failover group is created on a server, the DNS CNAME record for the listener URL is formed as `<fog-name>.database.windows.net`. After failover, the DNS record is automatically updated to redirect the listener to the new primary. 

- **Failover group read-only listener**

  A DNS CNAME record that points to the current secondary. It's created automatically when the failover group is created and allows the read-only SQL workload to transparently connect to the secondary when the secondary changes after failover. When the failover group is created on a server, the DNS CNAME record for the listener URL is formed as `<fog-name>.secondary.database.windows.net`. By default, failover of the read-only listener is disabled as it ensures the performance of the primary isn't affected when the secondary is offline. However, it also means the read-only sessions won't be able to connect until the secondary is recovered. If you can't tolerate downtime for the read-only sessions and can use the primary for both read-only and read-write traffic at the expense of the potential performance degradation of the primary, you can enable failover for the read-only listener by configuring the `AllowReadOnlyFailoverToPrimary` property. In that case, the read-only traffic is automatically redirected to the primary if the secondary isn't available.

  > [!NOTE]
  > The `AllowReadOnlyFailoverToPrimary` property only has effect if Microsoft managed failover policy is enabled and a forced failover has been triggered. In that case, if the property is set to True, the new primary will serve both read-write and read-only sessions.

- **Multiple failover groups**

  You can configure multiple failover groups for the same pair of servers to control the scope of geo-failovers. Each group fails over independently. If your tenant-per-database application is deployed in multiple regions and uses elastic pools, you can use this capability to mix primary and secondary databases in each pool. This way you might be able to reduce the impact of an outage to only some tenant databases.

## Failover group architecture

A failover group in Azure SQL Database can include one or multiple databases, typically used by the same application. A failover group must be configured on the primary server, which connects it to the secondary server in a different Azure region. The failover group can include all or some databases in the primary server. The following diagram illustrates a typical configuration of a geo-redundant cloud application using multiple databases in a failover group: 

:::image type="content" source="./media/failover-group-overview/failover-group.png" alt-text="Diagram shows a typical configuration of a geo-redundant cloud application using multiple databases and a failover group.":::

When designing a service with business continuity in mind, follow the general guidelines and best practices outlined in this article.  When configuring a failover group, ensure that authentication and network access on the secondary is set up to function correctly after geo-failover, when the geo-secondary becomes the new primary. For details, see [SQL Database security after disaster recovery](active-geo-replication-security-configure.md). For more information, see [Designing cloud solutions for disaster recovery](designing-cloud-solutions-for-disaster-recovery.md).

## <a id="using-geo-paired-regions"></a> Use paired regions

When creating your failover group between the primary and secondary server, use [paired regions](/azure/availability-zones/cross-region-replication-azure) as failover groups in paired regions have better performance compared to unpaired regions.

Following safe deployment practices, Azure SQL Database generally doesn't update paired regions at the same time. However, it isn't possible to predict which region will be upgraded first, so the order of deployment isn't guaranteed. Sometimes, your primary server is upgraded first, and sometimes it's upgraded second.

If you have [geo-replication](active-geo-replication-overview.md) or [failover groups](failover-group-sql-db.md) configured for databases that don't align with the [Azure region pairing](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies), use different maintenance window schedules for your primary and secondary databases. For example, you can select **Weekday** maintenance window for your secondary database and **Weekend** maintenance window for your primary database.

## Initial seeding

When adding databases or elastic pools to a failover group, there's an initial seeding phase before data replication starts. The initial seeding phase is the longest and most expensive operation. Once initial seeding completes, data is synchronized, and then only subsequent data changes are replicated. The time it takes for the initial seeding to complete depends on the size of your data, number of replicated databases, the load on the primary databases, and the speed of the network link between the primary and secondary database. Under normal circumstances, possible seeding speed is up to 500 GB an hour for SQL Database. Seeding is performed for all databases in parallel.

## <a id="using-one-or-several-failover-groups-to-manage-failover-of-multiple-databases"></a> Use multiple failover groups to fail over multiple databases

One or many failover groups can be created between two servers in different regions (primary and secondary servers). Each group can include one or several databases that are recovered as a unit in case all or some primary databases become unavailable due to an outage in the primary region. Creating a failover group creates geo-secondary databases with the same service objective as the primary. If you add an existing geo-replication relationship to a failover group, make sure the geo-secondary is configured with the same service tier and compute size as the primary.
  
## <a id="using-read-write-listener-for-oltp-workload"></a> Use the read-write listener (primary)

For read-write workloads, use `<fog-name>.database.windows.net` as the server name in the connection string. Connections are automatically directed to the primary. This name doesn't change after failover. Note the failover involves updating the DNS record so the client connections are redirected to the new primary only after the client DNS cache is refreshed. The time to live (TTL) of the primary and secondary listener DNS record is 30 seconds.

## <a id="using-read-only-listener-for-read-only-workload"></a> Use the read-only listener (secondary)

If you have logically isolated read-only workloads that are tolerant to data latency, you can run them on the geo-secondary. For read-only sessions, use `<fog-name>.secondary.database.windows.net` as the server name in the connection string. Connections are automatically directed to the geo-secondary. It's also recommended that you indicate read intent in the connection string by using `ApplicationIntent=ReadOnly`.

In the Premium, Business Critical, and Hyperscale service tiers, SQL Database supports the use of [read-only replicas](read-scale-out.md) to offload read-only query workloads, using the `ApplicationIntent=ReadOnly` parameter in the connection string. When you have configured a geo-secondary, you can use this capability to connect to either a read-only replica in the primary location or in the geo-secondary location: 

To connect to a read-only replica in the secondary location, use `ApplicationIntent=ReadOnly` and `<fog-name>.secondary.database.windows.net`.

## <a id="preparing-for-performance-degradation"></a> Potential performance degradation after failover

A typical Azure application uses multiple Azure services and consists of multiple components. Failover of a group is triggered based on the state of Azure SQL Database alone. Other Azure services in the primary region might not be affected by the outage and their components might still be available in that region. Once the primary databases switch to the secondary (DR) region, latency between dependent components can increase. To avoid the impact of higher latency on the application's performance, ensure the redundancy of all the application's components in the DR region, follow these [network security guidelines](failover-group-configure-sql-db.md#failover-groups-and-network-security), and orchestrate the geo-failover of relevant application components together with the database.

## <a id="preparing-for-data-loss"></a> Potential data loss after forced failover

If an outage occurs in the primary region, recent transactions might not have been replicated to the geo-secondary and there might be data loss if a forced failover is performed.

> [!IMPORTANT]
> Elastic pools with 800 or fewer DTUs or 8 or fewer vCores, and more than 250 databases can encounter issues including longer planned geo-failovers and degraded performance. These issues are more likely to occur for write intensive workloads when geo-replicas are widely separated by geography, or when multiple secondary geo-replicas are used for each database. A symptom of these issues is an increase in geo-replication lag over time, potentially leading to a more extensive data loss in an outage. This lag can be monitored using [sys.dm_geo_replication_link_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database). If these issues occur, then mitigation includes scaling up the pool to have more DTUs or vCores, or reducing the number of geo-replicated databases in the pool.


## <a id="failback"></a> Failback

When failover groups are configured with a Microsoft-managed failover policy, then forced failover to the geo-secondary server is initiated during a disaster scenario as per the defined grace period. Failback to the old primary must be initiated manually. 

## Permissions and limitations

Review the configure failover group guide for a list of [permissions](failover-group-configure-sql-db.md#permissions) and [limitations](failover-group-configure-sql-db.md#limitations).


## <a id="programmatically-managing-failover-groups"></a> Programmatically manage failover groups

Failover groups can also be managed programmatically using Azure PowerShell, Azure CLI, and REST API. For more information, review [Configure failover group](failover-group-configure-sql-db.md).

## Related content

- For sample scripts, see:
  - [Use PowerShell to configure active geo-replication for Azure SQL Database](scripts/setup-geodr-and-failover-database-powershell.md)
  - [Use PowerShell to configure active geo-replication for a pooled database in Azure SQL Database](scripts/setup-geodr-and-failover-elastic-pool-powershell.md)
  - [Use PowerShell to add an Azure SQL Database to a failover group](scripts/add-database-to-failover-group-powershell.md)
- For a business continuity overview and scenarios, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- To learn about Azure SQL Database automated backups, see [SQL Database automated backups](automated-backups-overview.md).
- To learn about using automated backups for recovery, see [Restore a database from the service-initiated backups](recovery-using-backups.md).
- To learn about authentication requirements for a new primary server and database, see [SQL Database security after disaster recovery](active-geo-replication-security-configure.md).
