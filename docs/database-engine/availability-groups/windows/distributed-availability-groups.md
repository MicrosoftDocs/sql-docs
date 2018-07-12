---
title: "Distributed availability groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "01/12/2018"
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.technology: high-availability
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
- "Availability Groups [SQL Server], distributed"
ms.assetid: 
caps.latest.revision: 
author: "MashaMSFT"
ms.author: mathoma
manager: craigg
---
# Distributed availability groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Distributed availability groups are a new feature introduced in SQL Server 2016, as a variation of the existing Always On availability groups feature. This article clarifies some aspects of distributed availability groups and complements the existing [SQL Server documentation](https://docs.microsoft.com/sql/sql-server/sql-server-technical-documentation).

> [!NOTE]
> "DAG" is not the official abbreviation for *distributed availability group*, because the abbreviation is already used for the Exchange Database Availability Group feature. This Exchange feature has no relation to SQL Server availability groups or distributed availability groups.

To configure a distributed availability group, see [Configure distributed availability groups](configure-distributed-availability-groups.md).

## Understand distributed availability groups

A distributed availability group is a special type of availability group that spans two separate availability groups. The underlying availability groups are configured on two different Windows Server Failover Clustering (WSFC) clusters. The availability groups that participate in a distributed availability group do not need to be in the same location. They can be physical, virtual, on-premises, in the public cloud, or anywhere that supports an availability-group deployment. As long as two availability groups can communicate, you can configure a distributed availability group with them.

A traditional availability group has resources configured in a WSFC cluster. A distributed availability group does not configure anything in the WSFC cluster. Everything about it is maintained within SQL Server. To learn how to view information for a distributed availability group, see [Viewing distributed availability group information](#viewing-distributed-availability-group-information). 

A distributed availability group requires that the underlying availability groups have a listener. Rather than provide the underlying server name for a standalone instance (or in the case of a SQL Server failover cluster instance [FCI], the value associated with the network name resource) as you would with a traditional availability group, you specify the configured listener for the distributed availability group with the parameter ENDPOINT_URL when you create it. Although each underlying availability group of the distributed availability group has a listener, a distributed availability group has no listener.

The following figure shows a high-level view of a distributed availability group that spans two availability groups (AG 1 and AG 2), each configured on its own WSFC cluster. The distributed availability group has a total of four replicas, with two in each availability group. Each availability group can support up to the maximum number of replicas, so a distributed availability can have up to 18 total replicas.

<a name="fig1"></a>
![High-level view of a distributed availability group][1]

You can configure the data movement in distributed availability groups as synchronous or asynchronous. However, data movement is slightly different within distributed availability groups compared to a traditional availability group. Although each availability group has a primary replica, there is only one copy of the databases participating in a distributed availability group that can accept inserts, updates, and deletions. As shown in the following figure, AG 1 is the primary availability group. Its primary replica sends transactions to both the secondary replicas of AG 1 and the primary replica of AG 2. The primary replica of AG 2 is also known as a *forwarder*. A forwarder is a primary replica in a secondary availability group in a distributed availability group. The forwarder receives transactions from the primary replica in the primary availability group and forwards them to the secondary replicas in its own availability group.  The forwarder then keeps the secondary replicas of AG 2 updated. 

![Distributed availability group and its data movement][2]

The only way to make AG 2's primary replica accept inserts, updates, and deletions is to manually fail over the distributed availability group from AG 1. In the preceding figure, because AG 1 contains the writeable copy of the database, issuing a failover makes AG 2 the availability group that can handle inserts, updates, and deletions. For information about how to fail over one distributed availability group to another, see [Failover to a secondary availability group]( https://docs.microsoft.com/sql/database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups).

> [!NOTE]
> Distributed availability groups in SQL Server 2016 support failover only from one availability group to another by using the option FORCE_FAILOVER_ALLOW_DATA_LOSS.

> [!NOTE]
> When using transactional replication with distributed availability groups the forwarder replica can't be configured as a publisher.

## SQL Server version and edition requirements for distributed availability groups

Distributed availability groups currently work only with availability groups that are created with the same major SQL Server version. For example, all availability groups that participate in a distributed availability group must currently be created with SQL Server 2016. Because the distributed availability groups feature did not exist in SQL Server 2012 or 2014, availability groups that were created with these versions cannot participate in distributed availability groups. 

> [!NOTE]
> Distributed availability groups can not be configured with Standard edition or mix of Standard and Enterprise edition.

Because there are two separate availability groups, the process of installing a service pack or cumulative update on a replica that's participating in a distributed availability group is slightly different from that of a traditional availability group:

1. Start by updating the replicas of the second availability group in the distributed availability group.

2. Patch the replicas of the primary availability group in the distributed availability group.

3. As with a standard availability group, fail over the primary availability group to one of its own replicas (not to the primary of the second availability group) and patch it. If there is no replica other than the primary, a manual failover to the second availability group will be necessary.

> [!NOTE]
> No announcements have been made as to whether future versions of SQL Server will allow previous versions to participate in the same distributed availability group. If that scenario were enabled, it would allow distributed availability groups to be part of a SQL Server version upgrade plan.

### Windows Server versions and distributed availability groups

A distributed availability group spans multiple availability groups, each on its own underlying WSFC cluster, and a distributed availability group is a SQL Server-only construct.  This means the WSFC clusters that house the individual availability groups can have different major versions of Windows Server. The major versions of SQL Server must be the same, as discussed in the previous section. Much like [the initial figure](#fig1), the following figure shows AG 1 and AG 2 participating in a distributed availability group, but each of the WSFC clusters is a different version of Windows Server.


![Distributed availability groups with WSFC clusters having different versions of Windows Server][3]

The individual WSFC clusters and their corresponding availability groups follow traditional rules. That is, they can be joined to a domain or not joined to a domain (Windows Server 2016 or later). When two different availability groups are combined in a single distributed availability group, there are four scenarios:

* Both WSFC clusters are joined to the same domain.
* Each WSFC cluster is joined to a different domain.
* One WSFC cluster is joined to a domain, and one WSFC cluster is not joined to a domain.
* Neither WSFC cluster is joined to a domain.

When both WSFC clusters are joined to the same domain (not trusted domains), you don't need to do anything special when you create the distributed availability group. For availability groups and WSFC clusters that are not joined to the same domain, use certificates to make the distributed availability group work, much in the way that you might create an availability group for a domain-independent availability group. To see how to configure certificates for a distributed availability group, follow steps 3-13 under [Create a domain-independent availability group](domain-independent-availability-groups.md#create-a-domain-independent-availability-group).

With a distributed availability group, the primary replicas in each underlying availability group must have each other's certificates. If you already have endpoints that are not using certificates, reconfigure those endpoints by using [ALTER ENDPOINT](https://docs.microsoft.com/sql/t-sql/statements/alter-endpoint-transact-sql) to reflect the use of certificates.

## Distributed availability group usage scenarios

Here are the three main usage scenarios for a distributed availability group: 

* [Disaster recovery and easier multi-site configurations](#disaster-recovery-and-multi-site-scenarios)
* [Migration to new hardware or configurations, which might include using new hardware or changing the underlying operating systems](#migration-using-a-distributed-availability-group)
* [Increasing the number of readable replicas beyond eight in a single availability group by spanning multiple availability groups](#scaling-out-readable-replicas-with-distributed-accessibility-groups)

### Disaster recovery and multi-site scenarios

A traditional availability group requires that all servers be part of the same WSFC cluster, which can make spanning multiple data centers challenging. The following figure shows what a traditional multi-site availability group architecture looks like, including the data flow. There is one primary replica that sends transactions to all secondary replicas. This configuration is less flexible in some ways than a distributed availability group. For example, you must implement such things as Active Directory (if applicable) and the witness for a quorum in the WSFC cluster. You might also need to take into account other aspects of a WSFC cluster, such as altering node votes.

![Traditional multi-site availability group][4]

Distributed availability groups offer a more flexible deployment scenario for availability groups that span multiple data centers. You can even use distributed availability groups where features such as [log shipping]( https://docs.microsoft.com/sql/database-engine/log-shipping/about-log-shipping-sql-server) were used in the past. However, unlike traditional availability groups, distributed availability groups cannot have delayed application of transactions. This means that availability groups or distributed availability groups cannot help in the event of human error in which data is incorrectly updated or deleted.

Distributed availability groups are loosely coupled, which in this case means that they don't require a single WSFC cluster and they're maintained by SQL Server. Because the WSFC clusters are maintained individually and the synchronization is primarily asynchronous between the two availability groups, it's easier to configure disaster recovery at another site. The primary replicas in each availability group synchronize their own secondary replicas.

* Only manual failover is supported for a distributed availability group. In a disaster recovery situation where you are switching data centers, you should not configure automatic failover (with rare exceptions). 
* You most likely will not need to set some of the traditional items or parameters for multi-site or subnet WSFC clusters, such as CrossSubnetThreshold, but you still need to see about network latency at a different layer for the data transport. The difference is that each WSFC cluster maintains its own availability; the cluster isn't one big entity of four nodes. You have two separate two-node WSFC clusters as shown in the previous figure.  
* We recommend asynchronous data movement, because this approach would be for disaster-recovery purposes.
* If you configure synchronous data movement between the primary replica and at least one secondary replica of the second availability group, and you configure synchronous movement on the distributed availability group, a distributed availability group will wait until all synchronous copies acknowledge that they have the data.

### Migrate by using a distributed availability group

Because distributed availability groups support two completely different availability group configurations, they enable not only easier disaster-recovery and multi-site scenarios, but also migration scenarios. Whether you are migrating to new hardware or virtual machines (on-premises or IaaS in the public cloud), configuring a distributed availability group allows a migration to occur where, in the past, you might have used backup, copy, and restore, or log shipping. 

The ability to migrate is especially useful in scenarios where you're changing or upgrading the underlying OS while you keep the same SQL Server version. Although Windows Server 2016 does allow a rolling upgrade from Windows Server 2012 R2 on the same hardware, most users choose to deploy new hardware or virtual machines. 

To complete the migration to the new configuration, at the end of the process, stop all data traffic to the original availability group, and change the distributed availability group to synchronous data movement. This action ensures that the primary replica of the second availability group is fully synchronized, so there would be no data loss. After you've verified the synchronization, fail over the distributed availability group to the secondary availability group. For more information, see [Fail over to a secondary availability group](configure-distributed-availability-groups.md#failover).

Post-migration, where the second availability group is now the new primary availability group, you might need to do either of the following:

* Rename the listener on the secondary availability group (and possibly delete or rename the old one on the original primary availability group), or re-create it with the listener from the original primary availability group, so that applications and users can access the new configuration.
* If a rename or re-creation is not possible, point applications and users to the listener on the second availability group.

### Scale out readable replicas with distributed availability groups

A single distributed availability group can have up to 16 secondary replicas, as needed. So it can have up 18 copies for reading, including the two primary replicas of the different availability groups. This approach means that more than one site can have near-real-time access for reporting to various applications.

Distributed availability groups can help you scale out a read-only farm more than you can with just a single availability group. A distributed availability group can scale out readable replicas in two ways:

* You can use the primary replica of the second availability group in a distributed availability group to create another distributed availability group, even though the database is not in RECOVERY.
* You can also use the primary replica of the first availability group to create another distributed availability group.

In other words, a primary replica can participate in two different distributed availability groups. The following figure shows AG 1 and AG 2 both participating in Distributed AG 1, while AG 2 and AG 3 are participating in Distributed AG 2. The primary replica (or forwarder) of AG 2 is both a secondary replica for Distributed AG 1 and a primary replica of Distributed AG 2.

![Scaling out reads with distributed availability groups][5]

The following figure shows AG 1 as the primary replica for two different distributed availability groups: Distributed AG 1 (composed of AG 1 and AG2) and Distributed AG 2 (composed of AG 1 and AG 3).


![Another scaling out of reads using distributed availability groups example][6]


In both preceding examples, there can be up to 27 total replicas across the three availability groups, all of which can be used for read-only queries. 

[Read-only routing]( https://docs.microsoft.com/sql/database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server) does not completely work with Distributed Availability Groups. More specifically,

1. Read-Only Routing can be configured and will work for the primary availability group of the distributed availability group. 
2. Read-Only Routing can be configured, but will not work for the secondary availability group of the distributed availability group. All queries, if they use the listener to connect to the secondary availability group, go to the primary replica of the secondary availability group. Otherwise, you need to configure each replica to allow all connections as a secondary replica and access them directly. However, read-only routing will work if the secondary availability group becomes primary after a failover. This behavior might be changed in an update to SQL Server 2016 or in a future version of SQL Server.


## Initialize secondary availability groups in a distributed availability group

Distributed availability groups were designed with [automatic seeding]( https://docs.microsoft.com/sql/database-engine/availability-groups/windows/automatically-initialize-always-on-availability-group) to be the main method used to initialize the primary replica on the second availability group. A full database restore on the primary replica of the second availability group is possible if you do the following:

1. Restore the database backup WITH NORECOVERY.
2. If necessary, restore the proper transaction log backups WITH NORECOVERY.
3. Create the second availability group without specifying a database name and with SEEDING_MODE set to AUTOMATIC.
4. Create the distributed availability group by using automatic seeding.

When you add the second availability group's primary replica to the distributed availability group, the replica is checked against the first availability group's primary databases, and seeding catches the database up to the source. There are a few caveats:

* The output shown in `sys.dm_hadr_automatic_seeding` on the primary replica of the second availability group will display a `current_state` of FAILED with the reason "Seeding Check Message Timeout."

* The current SQL Server log on the primary replica of the second availability group will show that seeding worked and that the [LSNs]( https://docs.microsoft.com/sql/relational-databases/sql-server-transaction-log-architecture-and-management-guide) were synchronized.

* The output shown in `sys.dm_hadr_automatic_seeding` on the primary replica of the first availability group will show a current_state of COMPLETED. 

* Seeding also has different behavior with distributed availability groups. For seeding to begin on the second replica, you must issue the command `ALTER AVAILABILITY GROUP [AGName] GRANT CREATE ANY DATABASE` command on the replica. Although this condition is still true of any secondary replica that participates in the underlying availability group, the primary replica of the second availability group already has the right permissions to allow seeding to begin after it is added to the distributed availability group. 

### View distributed availability group information 
	
As mentioned earlier, a distributed availability group is a SQL Server-only construct, and it is not seen in the underlying WSFC cluster. The following figure shows two different WSFC clusters (CLUSTER_A and CLUSTER_B), each with its own availability groups. Only AG1 in CLUSTER_A and AG2 in CLUSTER_B are discussed here. 

<!-- ![Two WSFC clusters with multiple availability groups through PowerShell Get-ClusterGroup command][7]  -->
<a name="fig7"></a>

```
PS C:\> Get-ClusterGroup -Cluster CLUSTER_A

Name                            OwnerNode             State
----                            ---------             -----
AG1                             DENNIS                Online
Available Storage               GLEN                  Offline
Cluster Group                   JY                    Online
New_RoR                         DENNIS                Online
Old_RoR                         DENNIS                Online
SeedingAG                       DENNIS                Online


PS C:\> Get-ClusterGroup -Cluster CLUSTER_B

Name                            OwnerNode             State
----                            ---------             -----
AG2                             TOMMY                 Online
Available Storage               JC                    Offline
Cluster Group                   JC                    Online
```

All detailed information about a distributed availability group is in SQL Server, specifically in the availability-group dynamic management views. Currently, the only information shown in SQL Server Management Studio for a distributed availability group is on the primary replica for the availability groups. As shown in the following figure, under the Availability Groups folder, SQL Server Management Studio shows that there is a distributed availability group. The figure shows AG1 as a primary replica for an individual availability group that's local to that instance, not for a distributed availability group.

![View in SQL Server Management Studio of the primary replica on the first WSFC cluster of a distributed availability group][8]


However, if you right-click the distributed availability group, no options are available (see the following figure), and the expanded Availability Databases, Availability Group Listeners, and Availability Replicas folders are all empty. SQL Server Management Studio 16 displays this result, but it might change in a future version of SQL Server Management Studio.

![No options available for action][9]

As shown in the following figure, secondary replicas show nothing in SQL Server Management Studio related to the distributed availability group. These availability group names map to the roles shown in the previous [CLUSTER_A WSFC cluster](#fig7) image.

![View in SQL Server Management Studio of a secondary replica][10]

The same concepts hold true when you use the dynamic management views. By using the following query, you can see all the availability groups (regular and distributed) and the nodes participating in them. This result is displayed only if you query the primary replica in one of the WSFC clusters that are participating in the distributed availability group. There is a new column in the dynamic management view `sys.availability_groups` named `is_distributed`, which is 1 when the availability group is a distributed availability group. To see this column:

```sql
SELECT ag.[name] as 'AG Name', 
    ag.Is_Distributed, 
    ar.replica_server_name as 'Replica Name'
FROM 	sys.availability_groups ag, 
    sys.availability_replicas ar       
WHERE	ag.group_id = ar.group_id;
GO
```

An example of output from the second WSFC cluster that's participating in a distributed availability group is shown in the following figure. SPAG1 is composed of two replicas: DENNIS and JY. However, the distributed availability group named SPDistAG has the names of the two participating availability groups (SPAG1 and SPAG2) rather than the names of the instances, as with a traditional availability group. 

![Example output of the preceding query][11]

In SQL Server Management Studio, any status shown on the Dashboard and other areas are for local synchronization only within that availability group. To display the health of a distributed availability group, query the dynamic management views. The following example query extends and refines the previous query:

```sql
SELECT ag.[name] as 'AG Name', ag.is_distributed, ar.replica_server_name as 'Underlying AG', ars.role_desc as 'Role', ars.synchronization_health_desc as 'Sync Status'
FROM 	sys.availability_groups ag, 
sys.availability_replicas ar,       
sys.dm_hadr_availability_replica_states ars       
WHERE	ar.replica_id = ars.replica_id
and 	ag.group_id = ar.group_id 
and	ag.is_distributed = 1;
GO
```
       
       
![Distributed availability group status][12]


To further extend the previous query, you can also see the underlying performance via the dynamic management views by adding in `sys.dm_hadr_database_replicas_states`. The dynamic management view currently stores information about the second availability group only. The following example query, run on the primary availability group, produces the sample output shown below:

```
SELECT ag.[name] as 'Distributed AG Name', ar.replica_server_name as 'Underlying AG', dbs.[name] as 'DB', ars.role_desc as 'Role', drs.synchronization_health_desc as 'Sync Status', drs.log_send_queue_size, drs.log_send_rate, drs.redo_queue_size, drs.redo_rate
FROM 	sys.databases dbs,
	sys.availability_groups ag,
	sys.availability_replicas ar,
	sys.dm_hadr_availability_replica_states ars,
	sys.dm_hadr_database_replica_states drs
WHERE	drs.group_id = ag.group_id
and	ar.replica_id = ars.replica_id
and	ars.replica_id = drs.replica_id
and	dbs.database_id = drs.database_id
and	ag.is_distributed = 1;
GO
```

![Performance information for a distributed availability group][13]


### Next steps 

* [Use the availability group wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)

* [Use the new availability group dialog box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
 
* [Create an availability group with Transact-SQL](create-an-availability-group-transact-sql.md)

<!--Image references-->
[1]: ./media/dag-01-high-level-view-distributed-ag.png
[2]: ./media/dag-02-distributed-ag-data-movement.png
[3]: ./media/dag-03-distributed-ags-wsfcs-different-versions-windows-server.png
[4]: ./media/dag-04-traditional-multi-site-ag.png
[5]: ./media/dag-05-scaling-out-reads-with-distributed-ags.png
[6]: ./media/dag-06-another-scaling-out-reads-using-distributed-ags-example.png
[7]: ./media/dag-07-two-wsfcs-multiple-ags-through-get-clustergroup-command.png
[8]: ./media/dag-08-view-smss-primary-replica-first-wsfc-distributed-ag.png
[9]: ./media/dag-09-no-options-available-action.png
[10]: ./media/dag-10-view-ssms-secondary-replica.png
[11]: ./media/dag-11-example-output-of-query-above.png
[12]: ./media/dag-12-distributed-ag-status.png
[13]: ./media/dag-13-performance-information-distributed-ag.png

 
