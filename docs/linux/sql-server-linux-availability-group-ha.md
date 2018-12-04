---
title: "SQL Server Always On availability group deployment patterns | Microsoft Docs"
ms.custom: "sql-linux"
ms.date: "10/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: linux
ms.topic: conceptual
ms.assetid: edd75f68-dc62-4479-a596-57ce8ad632e5
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: craigg
---
# High availability and data protection for availability group configurations

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article presents supported deployment configurations for SQL Server Always On availability groups on Linux servers. An availability group supports high availability and data protection. Automatic failure detection, automatic failover, and transparent reconnection after failover provide high availability. Synchronized replicas provide data protection. 

On a Windows Server Failover Cluster (WSFC), a common configuration for high availability uses two synchronous replicas and a third server or file share to provide quorum. The file-share witness validates the availability group configuration - status of synchronization, and the role of the replica, for example. This configuration ensures that the secondary replica chosen as the failover target has the latest data and availability group configuration changes. 

The WSFC synchronizes configuration metadata for failover arbitration between the availability group replicas and the file-share witness. When an availability group is not on a WSFC, the SQL Server instances store configuration metadata in the master database.

For example, an availability group on a Linux cluster has `CLUSTER_TYPE = EXTERNAL`. There is no WSFC to arbitrate failover. In this case the configuration metadata is managed and maintained by the SQL Server instances. Because there is no witness server in this cluster, a third SQL Server instance is required to store configuration state metadata. All three SQL Server instances together provide distributed metadata storage for the cluster. 

The cluster manager can query the instances of SQL Server in the availability group, and orchestrate failover to maintain high availability. In a Linux cluster, Pacemaker is the cluster manager. 

SQL Server 2017 CU 1 enables high availability for an availability group with `CLUSTER_TYPE = EXTERNAL` for two synchronous replicas plus a configuration only replica. The configuration only replica can be hosted on any edition of SQL Server 2017 CU1 or later - including SQL Server Express edition. The configuration only replica maintains configuration information about the availability group in the master database but does not contain the user databases in the availability group. 

## How the configuration affects default resource settings

SQL Server 2017 introduces the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` cluster resource setting. This setting guarantees the specified number of secondary replicas write the transaction data to log before the primary replica commits each transaction. When you use an external cluster manager, this setting affects both high availability and data protection. The default value for the setting depends on the architecture at the time the cluster resource is created. When you install the SQL Server resource agent - `mssql-server-ha` - and create a cluster resource for the availability group, the cluster manager detects the availability group configuration and sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` accordingly. 

If supported by the configuration, the resource agent parameter `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is set to the value that provides high availability and data protection. For more information, see [Understand SQL Server resource agent for pacemaker](#pacemakerNotify).

The following sections explain the default behavior for the cluster resource. 

Choose an availability group design to meet specific business requirements for high availability, data protection, and read-scale.

The following configurations describe the availability group design patterns and the capabilities of each pattern. These design patterns apply to availability groups with `CLUSTER_TYPE = EXTERNAL` for high availability solutions. 

- **Three synchronous replicas**
- **Two synchronous replicas**
- **Two synchronous replicas and a configuration only replica**

<a name="threeSynch"></a>

## Three synchronous replicas

This configuration consists of three synchronous replicas. By default, it provides high availability and data protection. It can also provide read-scale.

![Three replicas][3]

An availability group with three synchronous replicas can provide read-scale, high availability, and data protection. The following table describes availability behavior. 

| |read-scale|High availability & </br> data protection | Data protection
|:---|---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 |1<sup>*</sup>|2
|Primary outage | Manual failover. Might have data loss. New primary is R/W. |Automatic failover. New primary is R/W. |Automatic failover. New primary is not available for user transactions until former primary recovers and joins availability group as secondary. 
|One secondary replica outage  | Primary is R/W. No automatic failover if primary fails. |Primary is R/W. No automatic failover if primary fails as well. | Primary is not available for user transactions. 
<sup>*</sup> Default

<a name="twoSynch"></a>

## Two synchronous replicas

This configuration enables data protection. Like the other availability group configurations, it can enable read-scale. The two synchronous replicas configuration does not provide automatic high availability. 

![Two synchronous replicas][1]

An availability group with two synchronous replicas provides read-scale and data protection. The following table describes availability behavior. 

| |read-scale |Data protection
|:---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 <sup>*</sup>|1
|Primary outage | Manual failover. Might have data loss. New primary is R/W.| Automatic failover. New primary is not available for user transactions until former primary recovers and joins availability group as secondary.
|One secondary replica outage  |Primary is R/W, running exposed to data loss. |Primary is not available for user transactions until secondary recovers.
<sup>*</sup> Default

>[!NOTE]
>The preceding scenario is the behavior prior to SQL Server 2017 CU 1. 

<a name = "configOnly"></a>

## Two synchronous replicas and a configuration only replica

An availability group with two (or more) synchronous replicas and a configuration only replica provides data protection and may also provide high availability. The following diagram represents this architecture:

![Configuration only availability group][2]

1. Synchronous replication of user data to the secondary replica. It also includes availability group configuration metadata.
2. Synchronous replication of availability group configuration metadata. It does not include user data.

In the availability group diagram, a primary replica pushes configuration data to both the secondary replica and the configuration only replica. The secondary replica also receives user data. The configuration only replica does not receive user data. The secondary replica is in synchronous availability mode. The configuration only replica does not contain the databases in the availability group - only metadata about the availability group. Configuration data on the configuration only replica is committed synchronously.

>[!NOTE]
>An availabilility group with configuration only replica is new for SQL Server 2017 CU1. All instances of SQL Server in the availability group must be SQL Server 2017 CU1 or later. 

The default value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0. The following table describes availability behavior. 

| |High availability & </br> data protection | Data protection
|:---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 <sup>*</sup>|1
|Primary outage | Automatic failover. New primary is R/W. | Automatic failover. New primary is not available for user transactions. 
|Secondary replica outage | Primary is R/W, running exposed to data loss (if primary fails and cannot be recovered). No automatic failover if primary fails as well. | Primary is not available for user transactions. No replica to fail over to if primary fails as well. 
|Configuration only replica outage | Primary is R/W. No automatic failover if primary fails as well. | Primary is R/W. No automatic failover if primary fails as well. 
|Synchronous secondary + configuration only replica outage| Primary is not available for user transactions. No automatic failover. | Primary is not available for user transactions. No replica to failover to if primary fails as well. 
<sup>*</sup> Default

>[!NOTE]
>The instance of SQL Server that hosts the configuration only replica can also host other databases. It can also participate as a configuration only database for more than one availability group. 

## Requirements

* All replicas in an availability group with a configuration only replica must be SQL Server 2017 CU 1 or later.
* Any edition of SQL Server can host a configuration only replica, including SQL Server Express. 
* The availability group needs at least one secondary replica - in addition to the primary replica.
* Configuration only replicas do not count towards the maximum number of replicas per instance of SQL Server. SQL Server standard edition allows up to three replicas, SQL Server Enterprise Edition allows up to 9.

## Considerations

* No more than one configuration only replica per availability group. 
* A configuration only replica cannot be a primary replica.
* You cannot modify the availability mode of a configuration only replica. To change from a configuration only replica to a synchronous or asynchronous secondary replica, remove the configuration only replica, and add a secondary replica with the required availability mode. 
* A configuration only replica is synchronous with the availability group metadata. There is no user data. 
* An availability group with one primary replica and one configuration only replica, but no secondary replica is not valid. 
* You cannot create an availability group on an instance of SQL Server Express edition. 

<a name="pacemakerNotify"></a>

## Understand SQL Server resource agent for pacemaker

SQL Server 2017 CTP 1.4 added `sequence_number` to `sys.availability_groups` to allow Pacemaker to identify how up-to-date secondary replicas are with the primary replica. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is. Pacemaker updates the `sequence_number` with each availability group configuration change. Examples of configuration changes include failover, replica addition, or removal. The number is updated on the primary, then replicated to secondary replicas. Thus a secondary replica that has up-to-date configuration has the same sequence number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a *pre-promote* notification to all replicas. The replicas return the sequence number. Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers. If its own sequence number does not match the highest sequence number, the replica rejects the promote operation. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

This process requires at least one replica available for promotion with the same sequence number as the previous primary. The Pacemaker resource agent sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` such that at least one synchronous secondary replica is up-to-date and available to be the target of an automatic failover by default. With each monitoring action, the value of `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is computed (and updated if necessary). The `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` value is 'number of synchronous replicas' divided by 2. At failover time, the resource agent requires (`total number of replicas` - `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` replicas) to respond to the pre-promote notification. The replica with the highest `sequence_number` is promoted to primary. 

For example, An availability group with three synchronous replicas - one primary replica and two synchronous secondary replicas.

- `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 1; (3 / 2 -> 1).

- The required number of replicas to respond to pre-promote action is 2; (3 - 1 = 2). 

In this scenario, two replicas have to respond for the failover to be triggered. For successful automatic failover after a primary replica outage, both secondary replicas need to be up-to-date and respond to the pre-promote notification. If they are online and synchronous, they have the same sequence number. The availability group promotes one of them. If only one of the secondary replicas responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

>[!IMPORTANT]
>When `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0 there is risk of data loss. During a primary replica outage, the resource agent does not automatically trigger a failover. You can either wait for primary to recover, or manually fail over using `FORCE_FAILOVER_ALLOW_DATA_LOSS`.

You can choose to override the default behavior, and prevent the availability group resource from setting `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` automatically.

The following script sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 0 on an availability group named `<**ag1**>`. Before you run replace `<**ag1**>` with the name of your availability group.

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=0
```

To revert to default value, based on the availability group configuration run:

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=
```

>[!NOTE]
>When you run the preceding commands, the primary is temporarily demoted to secondary, then promoted again. The resource update causes all replicas to stop and restart. The new value for`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is only  set once replicas are restarted, not instantaneously.

## See also

[Availability groups on Linux](sql-server-linux-availability-group-overview.md)

<!--Image references-->
[1]: ./media/sql-server-linux-availability-group-ha/1-read-scale-out.png
[2]: ./media/sql-server-linux-availability-group-ha/2-configuration-only.png
[3]: ./media/sql-server-linux-availability-group-ha/3-three-replica.png
[4]: ./media/sql-server-linux-availability-group-ha/configuration-only-example.png
