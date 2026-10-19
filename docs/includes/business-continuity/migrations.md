---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/11/2025
ms.service: sql
ms.topic: include
ai-usage: ai-assisted
---

## Migrations and upgrades

When an organization deploys new instances or upgrades old ones, it can't tolerate long outages. This section discusses how the availability features of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] can be used to minimize the downtime in a planned architecture change, server switch, platform change (such as Windows Server to Linux or vice versa), or during patching.

> [!NOTE]  
> You can also use other methods, such as backups and restores, for migrations and upgrades. This article doesn't discuss those methods.

### Availability groups

You can upgrade an existing instance that contains one or more availability groups (AGs) in place, to later versions of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. While this upgrade requires some amount of downtime, it can be minimized with the right amount of planning.

If you want to migrate to new servers without changing the configuration (including the operating system or [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] version), add those servers as nodes to the existing underlying cluster, then add them to the AG. Once the replica or replicas are in the right state, manually fail over to a new server. Then, remove the old servers from the AG and decommission them.

Distributed AGs are also another method to migrate to a new configuration or upgrade [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. Because a distributed AG supports different underlying AGs on different architectures, you can change from [!INCLUDE [sssql19-md](../sssql19-md.md)] running on Windows Server 2019 to [!INCLUDE [sssql25-md](../sssql25-md.md)] running on Windows Server 2025.

:::image type="content" source="media/business-continuity/distributed-availability-group-mixed.png" alt-text="Diagram of a distributed availability group mixing WSFC and Pacemaker.":::

Finally, AGs with a cluster type of None are useful for migration or upgrading. You can't mix and match cluster types in a typical AG configuration, so all replicas need to be a type of None. A distributed AG can be used to span AGs configured with different cluster types. This method is also supported across the different OS platforms.

All variants of AGs for migrations and upgrades allow data synchronization, the most time-consuming portion of work, to be spread out over time. When it comes time to initiate the switch to the new configuration, the cutover is a brief outage, versus one long period of downtime where all work, including data synchronization, needs to be completed.

AGs can provide minimal downtime during patching of the underlying OS by manually failing over the primary to a secondary replica while the patching is in progress. From an operating system perspective, doing this is more common on Windows Server, because servicing the underlying OS can require a restart. Patching Linux sometimes needs a restart, but it's less common.

Another way to minimize downtime is to [patch SQL Server instances participating in an availability group](../../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md), depending on how complex the AG architecture is. You patch a secondary replica first. Once the right number of replicas are patched, manually fail over the primary replica to another node to do the upgrade. Upgrade any remaining secondary replicas at that point.

### Failover cluster instances

FCIs on their own can't assist with a traditional migration or upgrade. You have to configure an AG or log shipping for the databases in the FCI and account for all other objects. However, FCIs under Windows Server are still a popular option for when you need to patch the underlying Windows Servers. When you initiate a manual failover, the brief outage replaces having the instance unavailable for the entire time that Windows Server is being patched.

You can upgrade an FCI in place to later versions of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. For more information, see [Upgrade a failover cluster instance](../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md).

### Log shipping

Log shipping is still a popular option to both migrate and upgrade databases. Similar to AGs, but this time using the transaction log as the synchronization method, the data propagation can be started well in advance of the server switch. At the time of the switch, once all traffic is stopped at the source, a final transaction log would need to be taken, copied, and applied to the new configuration. At that point, the database can be brought online.

Log shipping is often more tolerant of slower networks, and while the switch might be slightly longer than one done using an AG or a distributed AG, it's usually measured in minutes, not hours, days, or weeks.

Similar to AGs, log shipping can provide a way to switch to another server during a maintenance window.

### Other SQL Server deployment methods and availability

There are two other deployment methods for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux: containers and using Azure (or another public cloud provider). The general need for availability exists regardless of how [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] is deployed. These two methods have some special considerations when making [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] highly available.

#### SQL Server containers and HA/DR options

[SQL Server container deployment](../../linux/quickstart-install-connect-docker.md) is a way to simplify [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]  provisioning, scaling, and lifecycle management across environments. A container is a complete ready-to-run image of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)].

Depending on your container platform, for example when using a container orchestrator like Kubernetes, if the container is lost, it can be deployed again and attached to the shared storage that was used. While this does provide some resiliency, there's some downtime associated with database recovery, and isn't truly highly available as it would be if using an availability group or FCI.

If you're looking to configure high availability for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] containers deployed on Kubernetes or non-Kubernetes platforms, you can use [DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial) as one of the clustering solutions, on top of which you can configure an AG in high availability mode. This option provides you with the recovery point objective (RPO) and recovery time objective (RTO) expected from a high availability solution.

#### Linux-based VM deployment

Linux can be deployed with [SQL Server on Linux Azure Virtual Machines](/azure/azure-sql/virtual-machines/linux/sql-server-on-linux-vm-what-is-iaas-overview). As with on premises-based installations, a supported installation requires the use of fencing a failed node that's external to the cluster agent itself. Node fencing is provided via fencing availability agents. Some distributions ship them as part of the platform, while others rely on external hardware and software vendors. Check with your preferred Linux distribution to see what forms of node fencing are provided so that a supported solution can be deployed in the public cloud.

Guides for installing [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux are available for the following distributions:

- [Quickstart: Install SQL Server and create a database on Red Hat](../../linux/quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](../../linux/quickstart-install-connect-ubuntu.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](../../linux/quickstart-install-connect-suse.md)

## Read-scale

Secondary replicas have the ability to be used for read-only queries. There are two ways that can be achieved with an AG:

- Allow direct access to the secondary
- [Configuring read only routing](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md), which requires the use of the listener. [!INCLUDE [sssql16-md](../sssql16-md.md)] introduced the ability to load balance read-only connections via the listener using a round robin algorithm, allowing read-only requests to be spread across all readable replicas.

> [!NOTE]  
> Readable secondary replicas are only available in Enterprise edition. Each instance that hosts a readable replica needs a [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] license.

Scaling readable copies of a database through AGs was first introduced with distributed AGs in [!INCLUDE [sssql16-md](../sssql16-md.md)]. This feature offers read-only copies of the database not only locally, but also regionally and globally, with minimal configuration. This setup reduces network traffic and latency by having queries execute locally. Each primary replica of an AG can seed two other AGs, even if it isn't the fully read/write copy, and each distributed AG can support up to 27 readable copies of the data.

:::image type="content" source="media/business-continuity/read-scale.png" alt-text="Diagram showing a distributed availability group related to read-scale." lightbox="media/business-continuity/read-scale.png":::

In [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions, you can create a near-real time, read-only solution with AGs configured with a cluster type of None. If your goal is to use AGs for readable secondary replicas and not availability, this approach removes the complexity of using a WSFC or an external cluster solution on Linux. It provides the readable benefits of an AG in a simpler deployment method.

The only major caveat is that, due to no underlying cluster with a cluster type of None, configuring read-only routing is a little different. From a [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] perspective, a listener is still required to route the requests even though there's no cluster. Instead of configuring a traditional listener, use the IP address or name of the primary replica. The primary replica then routes the read-only requests.

A log shipping warm standby can technically be configured for readable usage by restoring the database `WITH STANDBY`. However, because the transaction logs require exclusive use of the database for restoration, it means that users can't be accessing the database while that happens. This makes log shipping a less than ideal solution - especially if near real-time data is required.

Unlike with transactional replication where all of the data is live, each secondary replica in a read-scale scenario is an exact copy of the primary. The replica isn't in a state where unique indexes can be applied. If any indexes are required for reporting or if data needs to be manipulated, you must create those indexes on the databases on the primary replica. If you need that flexibility, replication is a better solution for readable data.
