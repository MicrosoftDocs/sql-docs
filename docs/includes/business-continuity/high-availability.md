---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/11/2025
ms.service: sql
ms.topic: include
ai-usage: ai-assisted
---

## High availability

It's important to ensure that [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instances or databases are available if a problem occurs that's local to a data center or single region in the cloud. This section explains how the [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] availability features can help. All of the features described are available both on Windows Server and on Linux.

### Availability groups

Availability groups (AGs) provide database-level protection by sending each transaction of a database to another instance, or *replica*, which contains a copy of that database in a special state. You can deploy an AG on Standard or Enterprise editions. The instances participating in an AG can be either standalone or failover cluster instances (FCIs, described in the next section). Since the transactions are sent to a replica as they happen, AGs are recommended where there are requirements for lower recovery point and recovery time objectives. Data movement between replicas can be synchronous or asynchronous, with Enterprise edition allowing up to three replicas (including the primary) as synchronous. An AG has one fully read/write copy of the database that is on the primary replica, while all secondary replicas can't receive transactions directly from end users or applications.

> [!NOTE]  
> Always On is an umbrella term for the availability features in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] and covers both AGs and FCIs. Always On isn't the name of the AG feature.

Before [!INCLUDE [sssql22-md](../sssql22-md.md)], AGs only provide database-level, and not instance-level protection. Anything not captured in the transaction log or configured in the database must be manually synchronized for each secondary replica. Some examples of objects that must be synchronized manually are logins at the instance level, linked servers, and [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] Agent jobs.

In [!INCLUDE [sssql22-md](../sssql22-md.md)] and later versions, you can manage metadata objects including users, logins, permissions, and [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] Agent jobs at the AG level in addition to the instance level. For more information, see [What is a contained availability group?](../../database-engine/availability-groups/windows/contained-availability-groups-overview.md)

An AG also has another component called the *listener*, which allows applications and end users to connect without needing to know which [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instance is hosting the primary replica. Each AG has its own listener. While the implementations of the listener are slightly different on Windows Server versus Linux, they both provide the same functionality and usability. The following diagram shows a Windows Server-based AG that's using a Windows Server Failover Cluster (WSFC). An underlying cluster at the OS layer is required for availability whether it's on Linux or Windows Server. The example shows a simple configuration with two servers, or *nodes*, with a WSFC as the underlying cluster.

:::image type="content" source="media/business-continuity/simple-availability-group.png" alt-text="Diagram of a simple availability group.":::

Standard and Enterprise edition have different maximums when it comes to replicas. An AG in Standard edition, known as a basic availability group, supports two replicas (one primary and one secondary) with only a single database in the AG. Enterprise edition not only allows multiple databases to be configured in a single AG, but also can have up to nine total replicas (one primary, eight secondary). Enterprise edition also provides other optional benefits such as readable secondary replicas, the ability to make backups off of a secondary replica, and more.

> [!NOTE]  
> Database mirroring, which was deprecated in [!INCLUDE [sssql11-md](../sssql11-md.md)], isn't available on the Linux version of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)], nor is it added. Customers still using database mirroring should plan to migrate to AGs, which is the replacement for database mirroring.

When it comes to availability, AGs can provide either automatic or manual failover. Automatic failover can occur if synchronous data movement is configured and the database on the primary and secondary replica are in a synchronized state. As long as the listener is used and the application uses a supported version of .NET Framework (3.5 with Service Pack 1, or 4.6.2 and later versions), the failover should be handled with minimal to no effect on end users if a listener is utilized. Failing over to a secondary replica to make it the new primary replica can be configured to be automatic or manual, and is generally measured in seconds.

The following list highlights some differences with AGs on Windows Server versus Linux:

Because of the way the underlying cluster works on Linux and Windows Server, all AG failovers (manual or automatic) are done via the cluster on Linux. On Windows Server-based AG deployments, manual failovers must be done via [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. Automatic failovers are handled by the underlying cluster on both Windows Server and Linux.

- For [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux, you should configure an AG with a minimum of three replicas, due to the way that the underlying clustering works.

- On Linux, the common name used by each listener is defined in DNS and not in the cluster like it's on Windows Server.

[!INCLUDE [sssql17-md](../sssql17-md.md)] introduced the following features and enhancements to AGs:

- Cluster types
- `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT`
- Enhanced Microsoft Distributor Transaction Coordinator (DTC) support for Windows Server-based configurations
- Additional scale out scenarios for read-only databases (described later in this article)

#### Availability group cluster types

The built-in availability form of clustering in Windows Server is enabled via a feature named Failover Clustering. It allows you to build a WSFC to be used with an AG or FCI. [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] ships cluster-aware resource DLLs that provide integration for AGs and FCIs.

[!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux supports multiple [clustering technologies](../../linux/sql-server-linux-ha-basics.md#sql-server-high-availability-and-disaster-recovery-partners). Microsoft supports the [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] components, while our partners support the relevant clustering technology. For example, along with Pacemaker, [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux supports [HPE Serviceguard](../../linux/sql-server-availability-group-ha-hpe.md) and [DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial) as a cluster solution.

A Windows-based failover cluster and Linux cluster solution are more similar than different. Both provide a way to take individual servers and combine them in a configuration to provide availability, and have concepts of things like resources, constraints (even if implemented differently), failover, and so on.

For example, to support Pacemaker for both AG and FCI configurations including things like automatic failover, Microsoft provides the `mssql-server-ha` package, which is similar to, but not exactly the same as the resource DLLs in a WSFC, for Pacemaker. One of the differences between a WSFC and Pacemaker is that there's no network name resource in Pacemaker, which is the component that helps to abstract the name of the listener (or the name of the FCI) on a WSFC. Use DNS for name resolution on Linux.

Because of the difference in the cluster stack, AGs in [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions need to handle some of the metadata that is natively handled by a WSFC. For example, there are three *cluster types* for an availability group, which are stored in `sys.availability_groups` in the `cluster_type` and `cluster_type_desc` columns:

- WSFC
- External
- None

All AGs that require high availability must use an underlying cluster, which in the case of [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions means WSFC or a Linux clustering agent. For Windows Server-based AGs that use an underlying WSFC, the default cluster type is WSFC and you don't need to set it. For Linux-based AGs, you must set the cluster type to External when creating the AG. The integration with an external cluster solution in Linux is configured after the AG is created, whereas on a WSFC, it's done at creation time.

A cluster type of None can be used with both Windows Server and Linux AGs. Setting the cluster type to None means that the AG doesn't require an underlying cluster. This means [!INCLUDE [sssql17-md](../sssql17-md.md)] is the first version of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] to support AGs without a cluster, but the tradeoff is that this configuration isn't supported as a high availability solution.

> [!IMPORTANT]  
> In [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions, you can't change a cluster type for an AG after it's created. This restriction means that an AG can't be switched from None to External or WSFC, and vice versa.

If you only want to add extra read-only copies of a database, or if you want what an AG provides for migration and upgrades but don't want to deal with the complexity of an underlying cluster or even replication, consider setting up an AG with a cluster type of None. For more information, see the sections [Migrations and upgrades](#migrations-and-upgrades) and [read-scale](#read-scale).

The following screenshot shows the support for the different kinds of cluster types in SQL Server Management Studio (SSMS). You must be running version 17.1 or later. The following screenshot is from version 17.2:

:::image type="content" source="media/business-continuity/availability-group-options.png" alt-text="Screenshot of SSMS AG options." lightbox="media/business-continuity/availability-group-options.png":::

#### REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT

[!INCLUDE [sssql16-md](../sssql16-md.md)] increased support for the number of synchronous replicas from two to three in Enterprise edition. However, if one secondary replica is synchronized but the other replica is having a problem, there's no way to control the behavior to tell the primary to either wait for the misbehaving replica, or to allow it to move on. In this scenario, the primary replica could still receive write traffic even though the secondary replica isn't in a synchronized state, resulting in data loss on the secondary replica.

In [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions, you can control the behavior of what happens when there are synchronous replicas with `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT`. This option works as follows:

- There are three possible values: `0`, `1`, and `2`.
- The value is the number of secondary replicas that must be synchronized, which has implications for data loss, AG availability, and failover.
- For WSFCs and a cluster type of None, the default value is `0`, and you can manually set it to `1` or `2`.
- For a cluster type of External, the cluster mechanism sets this value by default, and you can override it manually. For three synchronous replicas, the default value is `1`.

On Linux, you configure the value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` on the AG resource in the cluster. On Windows, you set it via Transact-SQL.

A value that's higher than `0` ensures higher data protection, because if the required number of secondary replicas isn't available, the primary isn't available until that condition resolves. `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` also affects failover behavior since automatic failover can't occur if the right number of secondary replicas aren't in the proper state. On Linux, a value of `0` doesn't allow automatic failover, so when using synchronous with automatic failover on Linux, you must set the value higher than `0` to achieve automatic failover. `0` on Windows Server is the behavior in [!INCLUDE [sssql16-md](../sssql16-md.md)] and earlier versions.

#### Enhanced Microsoft Distributed Transaction Coordinator support

Before [!INCLUDE [sssql16-md](../sssql16-md.md)], the only way to get availability in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] for applications that require distributed transactions, which use DTC underneath the covers, was to deploy FCIs. A distributed transaction can be done in one of two ways:

- A transaction that spans more than one database in the same [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instance.
- A transaction that spans more than one [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instance or possibly involves a non-[!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] data source.

[!INCLUDE [sssql16-md](../sssql16-md.md)] introduced partial support for DTC with AGs that covered the latter scenario. [!INCLUDE [sssql17-md](../sssql17-md.md)] completes the story by supporting both scenarios with DTC.

In [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions, you can add DTC support to an AG after it's created. In [!INCLUDE [sssql16-md](../sssql16-md.md)], you can only enable DTC support when creating the AG.

### Failover cluster instances

Failover cluster instances (FCIs) provide availability for the entire installation of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)], known as an *instance*. With FCIs, if the underlying server encounters a problem, everything inside the instance is moved to another server, including databases, [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] Agent jobs, linked servers, and more. All FCIs require some shared storage, even if it's network defined. One node can run and own the FCI's resources at any given time. In the following diagram, the first node of the cluster owns the FCI. It also owns the shared storage resources associated with it, which the solid line to the storage denotes.

:::image type="content" source="media/business-continuity/failover-cluster-instance.png" alt-text="Diagram of a failover cluster instance.":::

After a failover, ownership changes, as shown in the following diagram:

:::image type="content" source="media/business-continuity/failover-cluster-instance-post-failover.png" alt-text="Diagram of a failover cluster instance, post failover.":::

An FCI has zero data loss, but the underlying shared storage is a single point of failure since there's one copy of the data. To have redundant copies of databases, combine FCIs with another availability method, such as an AG or log shipping. The other method must use physically separate storage from the FCI. When the FCI fails over to another node, it stops on one node and starts on another. This process is similar to powering off a server and turning it on.

An FCI goes through the normal recovery process. It rolls forward any transactions that need to be rolled forward, and rolls back any incomplete transactions. Therefore, the database is consistent from a data point to the time of the failure or manual failover, so there's no data loss. Databases are only available after recovery is complete. Recovery time depends on many factors and is generally longer than failing over an AG. The tradeoff is that when you fail over an AG, there might be extra tasks required to make a database usable, such as enabling a [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] Agent job.

> [!NOTE]  
> Accelerated database recovery (ADR) can mitigate recovery time. For more information, see [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md).

Like an AG, FCIs abstract which node of the underlying cluster is hosting it. An FCI always retains the same name. Applications and end users never connect to the nodes. Instead, they use the unique name assigned to the FCI. An FCI can participate in an AG as one of the instances hosting either a primary or secondary replica.

The following list highlights some differences with FCIs on Windows Server versus Linux:

- On Windows Server, an FCI is part of the installation process. You configure an FCI on Linux after installing [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)].
- Linux only supports a single installation of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] per host, so all FCIs are a default instance. Windows Server supports up to 25 FCIs per WSFC.
- The common name used by FCIs in Linux is defined in DNS, and should be the same as the resource created for the FCI.

### Log shipping

If recovery point and recovery time objectives are more flexible, or databases aren't highly mission critical, log shipping is another proven availability feature in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. Based on [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]'s native backups, the process for log shipping automatically generates transaction log backups, copies them to one or more instances known as a warm standby, and automatically applies the transaction log backups to that standby. Log shipping uses [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] Agent jobs to automate the process of backing up, copying, and applying the transaction log backups.

:::image type="content" source="media/business-continuity/log-shipping.png" alt-text="Diagram of Log Shipping.":::

The biggest advantage of using log shipping is that it accounts for human error, because you can delay the application of transaction logs. For example, if someone issues an `UPDATE` without a `WHERE` clause, the standby might not have the change, so you can switch to that while you repair the primary system. While log shipping is easy to configure, switching from the primary to a warm standby, known as a *role change*, is always manual. You initiate a role change via Transact-SQL, and like an AG, you must manually synchronize all objects not captured in the transaction log. You need to configure log shipping per database, whereas a single AG can contain multiple databases.

Unlike an AG or FCI, log shipping has no abstraction for a role change, which applications must be able to handle. Techniques such as a DNS alias (CNAME) could be employed, but there are pros and cons, such as the time it takes for DNS to refresh after the switch.

## Disaster recovery

When your primary availability location experiences a catastrophic event like an earthquake or flood, the business must be prepared to have its systems come online elsewhere. This section covers how the [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] availability features can assist with business continuity.

### Availability groups

One of the benefits of AGs is that you configure both high availability and disaster recovery using a single feature. Without the requirement for ensuring that shared storage is also highly available, it's much easier to have replicas that are local in one data center for high availability, and remote ones in other data centers for disaster recovery each with separate storage. Having extra copies of the database is the tradeoff for ensuring redundancy. An example of an AG that spans multiple data centers is shown in the following diagram. One primary replica is responsible for keeping all secondary replicas synchronized.

:::image type="content" source="media/business-continuity/availability-group-span.png" alt-text="Diagram of an availability group spanning data centers.":::

Outside of an AG with a cluster type of None, an AG requires that all replicas are part of the same underlying cluster whether it's a WSFC or an external cluster solution. In the previous diagram, the WSFC is stretched to work in two different data centers, which adds complexity regardless of the platform (Windows Server or Linux). Stretching clusters across distance adds complexity.

Introduced in [!INCLUDE [sssql16-md](../sssql16-md.md)], a distributed availability group allows an AG to span AGs configured on different clusters. Distributed AGs decouple the requirement to have the nodes all participate in the same cluster, which makes configuring disaster recovery much easier. For more information on distributed AGs, see [Distributed availability groups](../../database-engine/availability-groups/windows/distributed-availability-groups.md).

:::image type="content" source="media/business-continuity/distributed-availability-group.png" alt-text="Diagram of a Distributed Availability Group.":::

### Failover cluster instances

You can use FCIs for disaster recovery. As with a normal AG, you must extend the underlying cluster mechanism to all locations, which adds complexity. For FCIs, you also need to consider the shared storage. The primary and secondary sites need access to the same disks. To ensure that the storage used by the FCI exists in both sites, use an external method such as functionality provided by the storage vendor at the hardware layer. Alternatively, use Storage Replica in Windows Server.

:::image type="content" source="media/business-continuity/failover-cluster-instance-span.png" alt-text="Diagram of an FCI spanning data centers.":::

### Log shipping

Log shipping is one of the oldest methods for providing disaster recovery for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] databases. Log shipping is often used with AGs and FCIs to provide cost-effective and simpler disaster recovery where other options might be challenging due to environment, administrative skills, or budget. Similar to the high availability story for log shipping, many environments delay the loading of a transaction log to account for human error.
