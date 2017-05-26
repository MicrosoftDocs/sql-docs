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

# High availability and data protection for availability group deployment architectures

This article presents supported deployment architectures for SQL Server Always On availability groups on Linux servers. An availability group supports high availability and data protection. Automatic failover provides high availability and disaster recovery. Synchronized replicas provide data protection. 

>[!NOTE]
>In addition to high availability and data protection, an availability group can also provide disaster recovery, cross platform migration, and read scale-out. This article primarily discusses implementations for high availability and data protection. 

With Windows Server failover clustering a common architecture for high availability uses two synchronous replicas and a [file-share witness](http://technet.microsoft.com/library/cc731739.aspx). The file-share witness validates the availability group configuration - status of synchronization, the role of the replica, for example. This ensures that the most current member owns the clustered resources. 

The current high availability solutions for Linux do not accommodate an external witness like like the file share witness in a Windows Server failover cluster. When there is no Windows Server failover cluster, the availability group configuration is stored in the SQL Server instances, hence the availability group requires at least three synchronous replicas for high availability and data protection. After you create an availability group on Linux servers you create a cluster resource. The cluster resource settings determine the configuration for high availability. By default the SQL Server cluster manager will configure the availability group for high availability when you create a cluster resource for an availability group.

Choose an availability group design to meet specific business requirements for high availability, data protection, and read scale-out.

>[!IMPORTANT]
>The following architectures describe three availability group design patterns and the capabilities of each pattern. These design patterns apply to availability groups with `CLUSTER_TYPE = EXTERNAL` or `CLUSTER_TYPE = NONE`.

The architectures include:

- **Three synchronous replicas**

- **Two synchronous replicas and a witness replica**

- **Read scale-out only**

The architectures allow you to balance costs with business requirements. 

## How the architecture affects default settings

The cluster resource setting `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` guarantees that each transaction is written to a minimum number of secondary replica logs before committing the transaction on the primary replica. This setting can affect both high availability and data protection, depending on the architecture. When you install the SQL Server resource agent - `mssql-sserver-ha` - and create the availability group resource, the cluster manager detects the availability group architecture and sets `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` accordingly. 

If supported by the architecture, the resource agent parameter `REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT` is set to the value that provides high availability and data protection.  If the architecture cannot support both, the default is set for high availability. If the architecture cannot support high availability, the default is set for data protection. For more information, see [Understand SQL Server resource agent for pacemaker](#pacemakerNotify).

The following sections explain the default behavior for the cluster resource. 

## Three synchronous replicas

This architecture consists of three synchronous replicas. By default, it provides high availability and data protection. It can also provide read scale-out.

![Three replicas][3]

Each instance of SQL Server requires an appropriate license. 

The following table describes the high availability and data protection behavior based on the settings for three synchronous replicas. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|0 |1 \*|2
| --- |:---:|:---:|:---:
|High availability| |✔| 
|Data protection | |✔|✔|
|Automatic failover after primary replica outage| |✔| 
|Continue after one secondary replica outage| |✔| 
|Continue after two secondary replica outages|✔| |  
\* Default setting.

## Two synchronous replicas and a witness replica

This architecture has two synchronous replicas and a witness replica to enable high availability. The witness replica is introduced in SQL Server 2017 CTP 2.4. In this architecture, the servers have the roles of primary replica, secondary replica, or witness replica. The witness replica contains configuration data about the availability group, but not a copy of the availability group user databases. The witness replica requires a SQL Server instance with SQL Server Express edition or higher. 

By default, it provides high availability but not data protection.

![Witness replica availability group][2]

The Two synchronous replicas and a witness replica availability group may reduce licensing costs because the witness replica can be SQL Server Express Edition. 

The following table describes the high availability and data protection behavior according to the possible values for an availability group with two synchronous replicas and a witness replica. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|1|0 \*
| --- |:---:|:---:
|High availability | |✔| 
|Data protection |✔| |
|Automatic failover after primary replica outage| |✔| 
|Continue after secondary replica outage| |✔|
|Continue after witness replica outage|✔| |

\* Default setting.

For additional information, see [More about a witness replica](#WitnessReplica).

## Read scale-out only availability group

A read scale-out only availability group has two synchronous replicas. The secondary replica allows reading. Read scale-out can provide data protection, but not high availability. Read only connections to the availability group are directed to secondary replicas.

![Read scale-out availability group][1]

This architecture requires two servers. These servers fill the role of primary replica and secondary replica.

The read scale-out architecture is optimized for distributing the read workload for databases. By default, the read scale-out architecture includes data protection. You can create the read scale-out architecture without data protection. This architecture does not provide high availability because if either instance of SQL Server fails, either the database will not be fully available or there is risk of data loss. You can configure the read scale-out architecture with or without data protection.

>[!IMPORTANT]
>This architecture does not require a cluster manager. You can configure the availability group and not create it as a cluster resource. The `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` value only applies to clustered resources. Because two synchronous replicas cannot support high availability on Linux server, the cluster component does not add to the functionality of the availability group.

The following table describes the high availability and data protection behavior according to the possible values for read scale-out only availability group. 

|`REQUIRED_ SYNCHRONIZED_SECONDARIES_TO_COMMIT`|1 \*|0 
| --- |:---:|:---:
|High availability | | | 
|Data protection |✔| |
|Automatic failover after primary replica outage| | | 
|Continue after secondary replica outage| |✔|

\* Default setting.

The read scale-out architecture may be the most economical because it only requires two instances of SQL Server on two servers.

<a name="pacemakerNotify"></a>

## Understand SQL Server resource agent for pacemaker

Before the CTP 1.4 release, the Pacemaker resource agent for availability groups could not know if a replica marked as `SYNCHRONOUS_COMMIT` was really up-to-date or not. It was possible that the replica had stopped synchronizing with the primary but was not aware. Thus the agent could promote an out-of-date replica to primary - which, if successful, would cause data loss. 

SQL Server 2017 CTP 1.4 added `sequence_number` to `sys.availability_groups` to solve this issue. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Performing failovers, adding or removing replicas, and other availability group operations update this number. The number is updated on the primary, then pushed to secondary replicas. Thus a secondary replica that is up-to-date will have the same sequence_number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it (we call this the pre-promote notification). Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

Note that this is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_COPIES_TO_COMMIT` such that at least one synchronous secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `REQUIRED_COPIES_TO_COMMIT` is computed (and updated if necessary)  as ('number of synchronous replicas' / 2). Then, at failover time, the resource agent will require (`total number of replicas` - `required_copies_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary. 

For example, let's consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous secondary replicas.

- `REQUIRED_COPIES_TO_COMMIT`  is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So 2 replicas have to be up for the failover to be triggered. This means that, in the case of primary outage, if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

A user can choose to override the default behavior, and configure the availability group resource to not set `REQUIRED_COPIES_TO_COMMIT` automatically as above.

>[!IMPORTANT]
>When `REQUIRED_COPIES_TO_COMMIT` is 0 there is risk of data loss. In the case of an outage of the primary, the resource agent will not automatically trigger a failover. The user has to decide if they want to wait for primary to recover or manually fail over.

To set `REQUIRED_COPIES_TO_COMMIT` to 0, run:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=0
```

To revert to default computed value, run:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=
```

>[!NOTE]
>Updating resource properties causes all replicas to stop and restart. This means primary will temporarily be demoted to secondary, then promoted again which will casue temporary write unavailability. The new value for REQUIRED_COPIES_TO_COMMIT will only be set once replicas are restarted, so it won't be instantaneous with running the pcs command.

<a name="WitnessReplica"></a>

## More about a witness replica

A witness replica in a SQL Server Always On availability group enables an architecture with high availability and data protection without using a third synchronous secondary replica. Because any edition of SQL Server can host a witness replica, it can save some licensing costs over other availability group architectures. A witness replica contains availability group configuration data, availability group roles, and synchronization status data; it does not include replicated user databases.The witness replica uses the `WITNESS-COMMIT` availability mode. It is never the primary replica.

Use a witness replica in an availability group with two synchronous replicas - one primary replica and one secondary replica. If an availability group does not include two synchronous replicas, you cannot use a witness replica. You can also include additional asynchronous replicas. The DDL `CREATE AVAILABILITY GROUP` will fail if the group does not include two synchronous replicas. Likewise, you cannot create an availability group with only one primary replica and a witness replica. An availability group cannot include more than one witness replica.

You can use any edition of SQL Server 2017 to host a witness replica. This includes SQL Server Enterprise Edition, Standard Edition, or Express Edition. SQL Server Express edition is available for free. For SQL Server Enterprise Edition, or Standard Edition, if the instance of SQL Server is used exclusively for availability group witness replica, licensing fees are not charged.

You cannot fail over an availability group to a witness replica. The witness replica has specific limitations. Specifically, the witness replica cannot:

- Create an availability group on a SQL Express instance.

- Manually or automatically become the primary replica.

A witness replica has availability mode `WITNESS_COMMIT`. In `WITNESS_COMMIT` availability group configuration information is synchronously committed to the witness replica. You cannot modify the availability mode of a witness replica. To change the availability mode of a witness replica, remove the replica and recreate it as a secondary replica with an appropriate availability mode.

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

<!--Image references-->
[1]: ./media/sql-server-linux-availability-group-ha/1-read scale-out-out.pn
[2]: ./media/sql-server-linux-availability-group-ha/2-witness.png
[3]: ./media/sql-server-linux-availability-group-ha/3-three-replica.png