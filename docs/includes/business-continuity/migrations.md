---
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/23/2022
ms.service: sql
ms.topic: include
---
## <a id="Migrations"></a> Migrations and upgrades

When deploying new instances or upgrading old ones, a business can't tolerate long outages. This section will discuss how the availability features of [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] can be used to minimize the downtime in a planned architecture change, server switch, platform change (such as Windows Server to Linux or vice versa), or during patching.

> [!NOTE]  
> Other methods, such as using backups and restoring them elsewhere, can also be used for migrations and upgrades. They are not discussed in this article.

### Availability groups

An existing instance containing one or more AGs can be upgraded in place to later versions of [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)]. While this will require some amount of downtime, with the right amount of planning, it can be minimized.

If the goal is to migrate to new servers and not change the configuration (including the operating system or [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] version), those servers could be added as nodes to the existing underlying cluster and added to the AG. Once the replica or replicas are in the right state, a manual failover could occur to a new server, and then the old ones could be removed from the AG, and ultimately, decommissioned.

Distributed AGs are also another method to migrate to a new configuration or upgrade [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)]. Because a distributed AG supports different underlying AGs on different architectures, for example, you could change from [!INCLUDE[sssql16-md](../sssql16-md.md)] running on Windows Server 2012 R2 to [!INCLUDE[sssql17-md](../sssql17-md.md)] running on Windows Server 2016.

:::image type="content" source="media/business-continuity/distributed-availability-group-mixed.png" alt-text="Diagram of a distributed availability group mixing WSFC and Pacemaker.":::

Finally, AGs with a cluster type of None can also be used for migration or upgrading. You can't mix and match cluster types in a typical AG configuration, so all replicas would need to be a type of None. A distributed AG can be used to span AGs configured with different cluster types. This method is also supported across the different OS platforms.

All variants of AGs for migrations and upgrades allow the most time consuming portion of the work to be done over time - data synchronization. When it comes time to initiate the switch to the new configuration, the cutover will be a brief outage versus one long period of downtime where all the work, including data synchronization, would need to be completed.

AGs can provide minimal downtime during patching of the underlying OS by manually failing over the primary to a secondary replica while the patching is being completed. From an operating system perspective, doing this would be more common on Windows Server since often, but not always, servicing the underlying OS may require a reboot. Patching Linux sometimes needs a reboot, but it can be infrequent.

[Patching SQL Server instances participating in an availability group](../../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md) can also minimize downtime depending on how complex the AG architecture is. To patch servers participating in an AG, a secondary replica is patched first. Once the right number of replicas are patched, the primary replica is manually failed over to another node to do the upgrade. Any remaining secondary replicas at that point can be upgraded, too.

### Failover cluster instances

FCIs on their own can't assist with a traditional migration or upgrade; an AG or log shipping would have to be configured for the databases in the FCI and all other objects accounted for. However, FCIs under Windows Server are still a popular option for when the underlying Windows Servers need to be patched. A manual failover can be initiated, which means a brief outage instead of having the instance unavailable for the entire time that Windows Server is being patched.
An FCI can be upgraded in place to later versions of [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)]. For information, see [Upgrade a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md).

### Log shipping

Log shipping is still a popular option to both migrate and upgrade databases. Similar to AGs, but this time using the transaction log as the synchronization method, the data propagation can be started well in advance of the server switch. At the time of the switch, once all traffic is stopped at the source, a final transaction log would need to be taken, copied, and applied to the new configuration. At that point, the database can be brought online. Log shipping is often more tolerant of slower networks, and while the switch may be slightly longer than one done using an AG or a distributed AG, it's usually measured in minutes - not hours, days, or weeks.

Similar to AGs, log shipping can provide a way to switch to another server in the event of patching.

### Other SQL Server deployment methods and availability

There are two other deployment methods for [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux: containers and using Azure (or another public cloud provider). The general need for availability as presented throughout this paper exists regardless of how [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] is deployed. These two methods have some special considerations when it comes to making [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] highly available.

#### SQL Server containers and HA/DR options

[SQL Server container deployment](../../linux/quickstart-install-connect-docker.md) is a new way of deploying [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux. A container is a complete image of [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] that is ready to run.

Depending on the container platform you use, for example when using a container orchestrator like Kubernetes, if the container is lost, it can be deployed again and attached to the shared storage that was used. While this does provide some resiliency, there will be some downtime associated with database recovery, and isn't truly highly available as it would be if using an availability group or FCI.

If you're looking to configure high availability for [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] containers deployed on Kubernetes or non-Kubernetes platforms, you can use [DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial) as one of the clustering solutions, on top of which you can configure an AG in high availability mode. This option provides you with the recovery point objective (RPO) and recovery time objective (RTO) expected from a high availability solution.

#### Linux-based IaaS deployment

Linux IaaS virtual machines can be deployed with [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] installed using Azure. As with on premises-based installations, a supported installation requires the use of STONITH (Shoot the Other Node in the Head) which is external to the cluster agent itself. STONITH is provided via fencing availability agents. Some distributions ship them as part of the platform, while others rely on external hardware and software vendors. Check with your preferred Linux distribution to see what forms of STONITH are provided so that a supported solution can be deployed in the public cloud.

Guides for installing [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux are available for the following distributions:

- [Quickstart: Install SQL Server and create a database on Red Hat](../../linux/quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](../../linux/quickstart-install-connect-ubuntu.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](../../linux/quickstart-install-connect-suse.md)

## <a name = "ReadScaleOut"></a> Read-scale

Since their introduction in [!INCLUDE[sssql11-md](../sssql11-md.md)], secondary replicas have had the ability to be used for read-only queries. There are two ways that can be achieved with an AG: by allowing direct access to the secondary, and [configuring read only routing](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md), which requires the use of the listener. [!INCLUDE[sssql16-md](../sssql16-md.md)] introduced the ability to load balance read-only connections via the listener using a round robin algorithm, allowing read-only requests to be spread across all readable replicas.

> [!NOTE]  
> Readable secondary replicas is a feature only in Enterprise edition, and each instance hosting a readable replica would need a [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] license.

Scaling readable copies of a database via AGs was first introduced with distributed AGs in [!INCLUDE[sssql16-md](../sssql16-md.md)]. This would allow companies to have read-only copies of the database not only locally, but regionally and globally with a minimal amount of configuration and reduce network traffic and latency by having queries executed locally. Each primary replica of an AG can seed two other AGs even if it isn't the fully read/write copy, so each distributed AG can support up to 27 copies of the data that are readable.

:::image type="content" source="media/business-continuity/read-scale.png" alt-text="Diagram showing a distributed availability group related to read-scale.":::

Starting with [!INCLUDE[sssql17-md](../sssql17-md.md)], It's possible to create a near-real time, read-only solution with AGs configured with a cluster type of None. If the goal is to use AGs for readable secondary replicas and not availability, doing this removes the complexity of using a WSFC or an external cluster solution on Linux, and gives the readable benefits of an AG in a simpler deployment method.

The only major caveat is that due to no underlying cluster with a cluster type of None, configuring read-only routing is a little different. From a [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] perspective, a listener is still required to route the requests even though there's no cluster. Instead of configuring a traditional listener, the IP address or name of the primary replica is used. The primary replica is then used to route the read-only requests.

A log shipping warm standby can technically be configured for readable usage by restoring the database WITH STANDBY. However, because the transaction logs require exclusive use of the database for restoration, it means that users can't be accessing the database while that happens. This makes log shipping a less than ideal solution - especially if near real-time data is required.

One thing that should be noted for all read-scale scenarios with AGs is that unlike using transactional replication where all of the data is live, each secondary replica isn't in a state where unique indexes can be applied, the replica is an exact copy of the primary. If any indexes are required for reporting or data needs to be manipulated, they must be created on the database(s) on the primary replica. If you need that flexibility, replication is a better solution for readable data.
