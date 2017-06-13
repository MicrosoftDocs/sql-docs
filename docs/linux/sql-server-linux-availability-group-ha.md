---
title: "SQL Server Always On availability group deployment patterns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: sql-linux
ms.reviewer: ""
ms.suite: ""
ms.technology: database-engine
ms.tgt_pltfrm: ""
ms.topic: article
ms.assetid: edd75f68-dc62-4479-a596-57ce8ad632e5
caps.latest.revision: 34
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"

---

# High availability and data protection for availability group configurations

This article presents supported deployment configurations for SQL Server Always On availability groups on Linux servers. An availability group supports high availability and data protection. Automatic failure detection, automatic failover, and transparent reconnection after failover provide high availability. Synchronized replicas provide data protection. 

>[!NOTE]
>In addition to high availability and data protection, an availability group can also provide disaster recovery, cross platform migration, and read scale-out. This article primarily discusses implementations for high availability and data protection. 

With Windows Server failover clustering, a common configuration for high availability uses two synchronous replicas and a [file-share witness](http://technet.microsoft.com/library/cc731739.aspx). The file-share witness validates the availability group configuration - status of synchronization, and the role of the replica, for example. This configuration ensures that the secondary replica chosen as the failover target has the latest data and availability group configuration changes. 

The current high availability solutions for Linux do not accommodate an external witness like the file share witness in a Windows Server failover cluster. When there is no Windows Server failover cluster, the availability group configuration is stored in the master database on participating SQL Server instances. Therefore, the availability group requires at least three synchronous replicas for high availability and data protection. After you create an availability group on Linux servers, create a cluster resource. The cluster resource settings determine the configuration for high availability.

Choose an availability group design to meet specific business requirements for high availability, data protection, and read scale-out.

>[!IMPORTANT]
>The following configurations describe three availability group design patterns and the capabilities of each pattern. These design patterns apply to availability groups with `CLUSTER_TYPE = EXTERNAL` for high availability solutions. 

The design patters are three availability group configuarations. The configurations include:

- **Three synchronous replicas**

- **Two synchronous replicas and a witness**

- **Two synchronous replicas availability group**

## How the configuration affects default resource settings

The cluster resource setting `required_synchronized_secondaries_to_commit` guarantees that each transaction is written to a minimum number of secondary replica logs before committing the transaction on the primary replica. This setting can affect both high availability and data protection, depending on the configuration. When you install the SQL Server resource agent - `mssql-server-ha` - and create a cluster resource for the availability group, the cluster manager detects the availability group configuration and sets `required_synchronized_secondaries_to_commit` accordingly. 

If supported by the configuration, the resource agent parameter `required_synchronized_secondaries_to_commit` is set to the value that provides high availability and data protection. For more information, see [Understand SQL Server resource agent for pacemaker](#pacemakerNotify).

The following sections explain the default behavior for the cluster resource. 

<a name="threeSynch"></a>

## Three synchronous replicas

This configuration consists of three synchronous replicas. By default, it provides high availability and data protection. It can also provide read scale-out.

![Three replicas][3]

The following table describes the high availability and data protection behavior based on the settings for three synchronous replicas. 

|`required_synchronized_secondaries_to_commit`|0 |1 \*|2
| --- |:---:|:---:|:---:
|Automatic failover after primary replica outage| |✔| 
|Primary replica available after one secondary replica outage|✔|✔| 
|Primary replica available after two secondary replica outages|✔| |
|Manual failover after primary replica outage - possible data loss|✔| | 

\* Default setting when availability group is added as a resource in a cluster.

<a name="witness"></a>

## Two synchronous replicas and a witness

This configuration has two synchronous replicas and a witness to enable high availability and data protection. The witness is introduced in SQL Server 2017 CTP 2.2. In this configuration, the servers have the roles of primary replica, secondary replica, or witness. The witness contains configuration data about the availability group, but not a copy of the availability group user databases. The witness requires a SQL Server instance with SQL Server Express edition or higher. 

By default, it provides high availability but not data protection.

![witness availability group][2]

The following table describes the high availability and data protection behavior of this configuration:

|`required_synchronized_secondaries_to_commit`|0 \*|1
| --- |:---:|:---:
|Automatic failover after primary replica outage| |✔| 
|Primary replica available after secondary replica outage| |✔|
|Primary replica available after witness outage|✔| |

\* Default setting when availability group is added as a resource in a cluster.

For more information, see [More about a witness](#WitnessReplica).

<a name="twoSynch"></a>

## Two synchronous replicas

A two synchronous replicas availability group enables data protection. Like the other availability group configurations, it can enable read scale-out. Two synchronous replicas does not provide high availability. 

![Two synchronous replicas][1]

This configuration requires two servers. These servers fill the role of primary replica and secondary replica.

The two synchronous replicas configuration is optimized for data protection and distributing the read workload for databases. By default, the resource is configured for data protection. This configuration does not provide high availability because if either instance of SQL Server fails, either the database is not fully available or there is risk of data loss. 

The following table describes the data protection behavior according to the possible values for two synchronous replicas availability group. 

|`required_synchronized_secondaries_to_commit`|0 \*|1 
| --- |:---|:---
|Automatic failover after primary replica outage| |✔ \*\* | 
|Primary replica available after secondary replica outage|✔| |

\* Default setting when availability group is added as a resource in a cluster.

\*\* In this configuration, after the primary replica outage occurs the availability group automatic fails over. Applications cannot connect to the availability group until the primary replica is back on line - now as a secondary replica. 

The two synchronous replicas configuration may be the most economical because it only requires two instances of SQL Server on two servers.


<a name="WitnessReplica"></a>

## More about a witness

A witness in a SQL Server Always On availability group enables a configuration with high availability and data protection without using a third replica - a *witness*. Any edition of SQL Server can host a witness. A witness contains availability group configuration data like availability group roles, and synchronization status data in the `master` database. It does not include replicated user databases. The witness uses the `WITNESS_COMMIT` availability mode. It is never the primary replica.

Use a witness in an availability group with two synchronous replicas. The group can include additional asynchronous replicas. An availability group can only have one witness.

An availability group with a witness requires two synchronous replicas and can have additional asynchronous replicas. An availability group cannot have more than one witness. 

You can use any edition of SQL Server 2017 to host a witness. 

The witness has an availability mode - like a replica. A witness availability mode is `WITNESS_COMMIT`. In this mode, configuration information is synchronously committed to the witness. You cannot modify the availability mode of a witness. 

On a witness you cannot:

- Failover an availability group to a witness.
- Change the availability mode.

To create an availability group with two synchronous replicas and a witness, see [Create availability group with two synchronous replicas and a witness](sql-server-linux-availability-group-configure-ha.md#witnessScript).

<a name="pacemakerNotify"></a>

## Understand SQL Server resource agent for pacemaker

SQL Server 2017 CTP 1.4 added `sequence_number` to `sys.availability_groups` to allow Pacemaker to identify how up-to-date secondary replicas are with the primary replica. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Pacemaker updates the `sequence_number` with each availability group configuration change. Examples of configuration changes include failover, replica addition, or removal. The number is updated on the primary, then replicated to secondary replicas. Thus a secondary replica that has up-to-date configuration has the same sequence number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a *pre-promote* notification to all replicas and the witness - if applicable. The replicas return the sequence number. Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers. The replica and rejects the promote operation if its own sequence number does not match the highest sequence number. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

This process requires at least one replica available for promotion with the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `required_synchronized_secondaries_to_commit` such that at least one synchronous secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `required_synchronized_secondaries_to_commit` is computed (and updated if necessary). The `required_synchronized_secondaries_to_commit` value is 'number of synchronous replicas' divided by 2. At failover time, the resource agent will require (`total number of replicas` - `required_synchronized_secondaries_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary. 

For example, An availability group with three synchronous replicas - one primary replica and two synchronous secondary replicas.

- `required_synchronized_secondaries_to_commit` is 1; (3 / 2 -> 1).

- The required number of replicas to respond to pre-promote action is 2; (3 - 1 = 2). 

In this scenario, 2 replicas have to respond for the failover to be triggered. For successful automatic failover after a primary replica outage both secondary replicas need to be up-to-date and respond to the pre-promote notification. If they are online they will have the same sequence number. The availability group will promote one of them. If one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

A user can choose to override the default behavior, and prevent the availability group resource from setting `required_synchronized_secondaries_to_commit` automatically as above.

>[!IMPORTANT]
>When `required_synchronized_secondaries_to_commit` is 0 there is risk of data loss. In the case of an outage of the primary, the resource agent will not automatically trigger a failover. The user has to decide if they want to wait for primary to recover or manually fail over using `FORCE_FAILOVER_ALLOW_DATA_LOSS`.

The following script sets `required_synchronized_secondaries_to_commit` to 0 on an availability group named `<**ag1**>`. Before you run replace `<**ag1**>` with the name of your availability group.

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=0
```

To revert to default value, based on the availability group configuration run:

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=
```

>[!NOTE]
>Updating resource properties causes all replicas to stop and restart. This means primary will temporarily be demoted to secondary, then promoted again which will cause temporary write unavailability. The new value for`required_synchronized_secondaries_to_commit` will only be set once replicas are restarted, so it won't be instantaneous with running the pcs command.

<!--Image references-->
[1]: ./media/sql-server-linux-availability-group-ha/1-read-scale-out.png
[2]: ./media/sql-server-linux-availability-group-ha/2-witness.png
[3]: ./media/sql-server-linux-availability-group-ha/3-three-replica.png