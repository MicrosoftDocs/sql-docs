---
title: "SQL Server Always On availability group deployment patterns | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2017"
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

With Windows Server failover clustering a common configuration for high availability uses two synchronous replicas and a [file-share witness](http://technet.microsoft.com/library/cc731739.aspx). The file-share witness validates the availability group configuration - status of synchronization, and the role of the replica, for example. This ensures that the secondary replica chosen as the failover target has the latest data and availability group configuration changes. 

The current high availability solutions for Linux do not accommodate an external witness like the file share witness in a Windows Server failover cluster. When there is no Windows Server failover cluster the availability group configuration is stored in the master database on participating SQL Server instances. Therefore, the availability group requires at least three synchronous replicas for high availability and data protection. After you create an availability group on Linux servers you create a cluster resource. The cluster resource settings determine the configuration for high availability.

Choose an availability group design to meet specific business requirements for high availability, data protection, and read scale-out.

>[!IMPORTANT]
>The following configurations describe three availability group design patterns and the capabilities of each pattern. These design patterns apply to availability groups with `CLUSTER_TYPE = EXTERNAL` for high availability solutions. 

The design patters are three availability group configuarations. The configurations include:

- **Three synchronous replicas**

- **Two synchronous replicas and a witness replica**

- **Two synchronous replicas availability group**

## How the configuration affects default resource settings

The cluster resource setting `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` guarantees that each transaction is written to a minimum number of secondary replica logs before committing the transaction on the primary replica. This setting can affect both high availability and data protection, depending on the configuration. When you install the SQL Server resource agent - `mssql-sserver-ha` - and create a cluster resource for the availability group, the cluster manager detects the availability group configuration and sets `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` accordingly. 

If supported by the configuration, the resource agent parameter `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` is set to the value that provides high availability and data protection.  If the configuration cannot support both the default is set for data protection. For more information, see [Understand SQL Server resource agent for pacemaker](#pacemakerNotify).

The following sections explain the default behavior for the cluster resource. 

<a name="threeSynch"></a>

## Three synchronous replicas

This configuration consists of three synchronous replicas. By default, it provides high availability and data protection. It can also provide read scale-out.

![Three replicas][3]

The following table describes the high availability and data protection behavior based on the settings for three synchronous replicas. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|0 |1 \*|2
| --- |:---:|:---:|:---:
|High availability| |✔| 
|Data protection | |✔|✔|
|Automatic failover after primary replica outage| |✔| 
|Primary replica available after one secondary replica outage|✔|✔| 
|Primary replica available after two secondary replica outages|✔| |
|Manual failover after primary replica outage - possible data loss|✔| | 

\* Default setting when availability group is added as a resource in a cluster.

<a name="witness"></a>

## Two synchronous replicas and a witness replica

This configuration has two synchronous replicas and a witness replica to enable high availability and data protection. The witness replica is introduced in SQL Server 2017 CTP 2.2. In this configuration, the servers have the roles of primary replica, secondary replica, or witness replica. The witness replica contains configuration data about the availability group, but not a copy of the availability group user databases. The witness replica requires a SQL Server instance with SQL Server Express edition or higher. 

By default, it provides high availability but not data protection.

![Witness replica availability group][2]

The following table describes the high availability and data protection behavior according to the possible values for an availability group with two synchronous replicas and a witness replica. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|0 \*|1
| --- |:---:|:---:
|High availability |✔| | 
|Data protection |✔|✔|
|Automatic failover after primary replica outage| |✔| 
|Primary replica available after secondary replica outage| |✔|
|Primary replica available after witness replica outage|✔| |

\* Default setting when availability group is added as a resource in a cluster.

For additional information, see [More about a witness replica](#WitnessReplica).

<a name="twoSynch"></a>

## Two synchronous replicas

A two synchronous replicas availability group enables data protection. Like the other availability group configurations it can enable read scale-out. Two synchronous replicas does not provide high availability. 

![Two synchronous replicas][1]

This configuration requires two servers. These servers fill the role of primary replica and secondary replica.

The two synchronous replicas configuration is optimized for data protection and distributing the read workload for databases. By default, the resource is configured for data protection. This configuration does not provide high availability because if either instance of SQL Server fails, either the database will not be fully available or there is risk of data loss. 

The following table describes the data protection behavior according to the possible values for two synchronous replicas availability group. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|1 \*|0 
| --- |:---|:---
|Automatic failover |✔| | 
|Data protection |✔| |
|Automatic failover after primary replica outage|✔ \*\*| | 
|Primary replica available after secondary replica outage| |✔|

\* Default setting when availability group is added as a resource in a cluster.

\*\* In this configuration, after the primary replica outage occurs the availability group automatic fails over. Applications cannot connect to the availability group until the primary replica is back on line - now as a secondary replica. 

The two synchronous replicas configuration may be the most economical because it only requires two instances of SQL Server on two servers.


<a name="WitnessReplica"></a>

## More about a witness replica

A witness replica in a SQL Server Always On availability group enables a configuration with high availability and data protection without using a third replica - a *witness replica*. Any edition of SQL Server can host a witness replica. A witness replica contains availability group configuration data like availability group roles, and synchronization status data in the `master` database. It does not include replicated user databases. The witness replica uses the `WITNESS-COMMIT` availability mode. It is never the primary replica.

Use a witness replica in an availability group with two synchronous replicas - one primary replica and one secondary replica. If an availability group does not include two synchronous replicas, you cannot use a witness replica. You can also include additional asynchronous replicas. The DDL `CREATE AVAILABILITY GROUP` will fail if the group does not include two synchronous replicas. Likewise, you cannot create an availability group with only one primary replica and a witness replica. An availability group cannot include more than one witness replica.

You can use any edition of SQL Server 2017 to host a witness replica. This includes SQL Server Enterprise Edition, Standard Edition, or Express Edition. 

On a witness replica you cannot do the following things:

- Create an availability group - you can include the witness in an availability group, or join it to an existing availability group.
- Failover an availability group to a witness replica.

A witness replica availability mode `WITNESS_COMMIT`. In this mode, configuration information is synchronously committed to the witness replica. You cannot modify the availability mode of a witness replica. To change the availability mode of a witness replica, remove the replica and recreate it as a secondary replica with an appropriate availability mode.

To create an availability group with a witness replica:

1. Install and configure SQL Server on three servers.

2. Create the availability group. Include two replicas with synchronous availability mode, and a witness replica. For example, the following script creates an availability group called `ag1`. `node1` and `node2` host replicas in synchronous mode, with automatic seeding and automatic failover. `node3` is a witness replica. The script defines only the SQL Server instance name `node3`, the endpoint, and the availability mode.

   ```Transact-SQL
   CREATE AVAILABILITY GROUP [ag1]
      WITH (CLUSTER_TYPE = EXTERNAL)
      FOR REPLICA ON
      N'node1' WITH (
          ENDPOINT_URL = N'tcp://node1:5022',
          AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
          FAILOVER_MODE = EXTERNAL,
          SEEDING_MODE = AUTOMATIC
      ),
      N'node2' WITH ( 
          ENDPOINT_URL = N'tcp://node2:5022', 
          AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
          FAILOVER_MODE = EXTERNAL,
          SEEDING_MODE = AUTOMATIC
      ),
      N'node3' WITH (
          ENDPOINT_URL = N'tcp://node3:5022',
          AVAILABILITY_MODE = WITNESS_COMMIT
      )
   ```

<a name="pacemakerNotify"></a>

## Understand SQL Server resource agent for pacemaker

SQL Server 2017 CTP 1.4 added `sequence_number` to `sys.availability_groups` to solve this issue. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Performing failovers, adding or removing replicas, and other availability group configuration changes update this number. The number is updated on the primary, then replicated to secondary replicas. Thus a secondary replica that has up-to-date configuration has the same sequence number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it (we call this the pre-promote notification). Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

Note that this is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` such that at least one synchronous secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` is computed (and updated if necessary)  as ('number of synchronous replicas' / 2). Then, at failover time, the resource agent will require (`total number of replicas` - `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary. 

For example, let's consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous secondary replicas.

- `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`  is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So 2 replicas have to be up for the failover to be triggered. This means that, in the case of primary outage, if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

A user can choose to override the default behavior, and prevent the availability group resource from setting `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` automatically as above.

>[!IMPORTANT]
>When `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0 there is risk of data loss. In the case of an outage of the primary, the resource agent will not automatically trigger a failover. The user has to decide if they want to wait for primary to recover or manually fail over.

To set `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 0, run:

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=0
```

To revert to default value, based on the availability group configuration run:

```bash
sudo pcs resource update <**ag1**> required_synchronized_secondaries_to_commit=
```

>[!NOTE]
>Updating resource properties causes all replicas to stop and restart. This means primary will temporarily be demoted to secondary, then promoted again which will cause temporary write unavailability. The new value for`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` will only be set once replicas are restarted, so it won't be instantaneous with running the pcs command.

<!--Image references-->
[1]: ./media/sql-server-linux-availability-group-ha/1-read-scale-out.png
[2]: ./media/sql-server-linux-availability-group-ha/2-witness.png
[3]: ./media/sql-server-linux-availability-group-ha/3-three-replica.png