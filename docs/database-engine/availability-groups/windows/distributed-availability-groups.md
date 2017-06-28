---
title: "Distributed Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
- "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
- "Availability Groups [SQL Server], distributed"
ms.assetid: 
caps.latest.revision: 
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Distributed Availability Groups

A distributed availability group is a new feature introduced in SQL Server 2016, as a variation of the existing Always On availability groups (AG) feature. This article will clarify some aspects of distributed availability groups and complement the existing [SQL Server documentation](https://docs.microsoft.com/en-us/sql/sql-server/sql-server-technical-documentation).

> [!NOTE]
> "DAG" is not the official abbreviation for a distributed availability group since that is already used for Exchange's Database Availability Group feature, which has no relation to a SQL Server availability group or a distributed availability group.

## Understanding Distributed Availability Groups

A distributed availability group is a special type of availability group that spans two separate availability groups. The underlying availability groups are configured on two different Windows Server failover clusters (WSFCs). The availability groups participating in a distributed availability group do not need to be in the same location. They can be physical, virtual, on premises, in the public cloud, or anywhere that supports an availability group deployment. As long as two availability groups can communicate, you can configure a distributed availability group with them.

A traditional availability group has resources configured in a WSFC. A distributed availability group does not configure anything in the WSFC -- everything about it is maintained within SQL Server. For more information on how to see the information of a distributed availability group, see [Viewing Distributed Availability Group Information](#viewing-distributed-availability-group-information). 

A distributed availability group requires that the underlying availability groups have a listener. Rather than providing the underlying server name for a standalone instance (or in the case of a SQL Server failover cluster instance (FCI), the value associated with the network name resource) as you would with a traditional availability group, the configured listener is specified for the distributed availability group with the parameter ENDPOINT_URL during creation. Even though each underlying availability group of the distributed availability group has a listener, there is no listener for a distributed availability group.

The following figure shows a high-level view of a distributed availability group that spans two availability groups (AG 1 and AG 2), each configured on their own WSFC. The distributed availability group has a total of four replicas, with two in each availability group. Each availability group can support up to the maximum number of replicas, so a distributed availability group based on Standard Edition can have up to four replicas, and one based on Enterprise Edition can have up to 18 total replicas.

<a name="fig1"></a>
![High level view of a distributed availability group][1]

The data movement in distributed availability groups can be configured as synchronous or asynchronous. However, data movement is slightly different within distributed availability groups compared to a traditional availability group. Although each availability group has a primary replica, there is only one copy of the database(s) participating in a Distributed Availability Group that can accept inserts, updates, and deletes. As shown below, AG 1 is the primary availability group. Its primary replica sends transactions to both the secondary replica(s) of AG 1 and the primary replica of AG 2. The primary replica of AG 2 then keeps the secondary replica(s) of AG 2 updated. 


![Distributed availability group and its data movement][2]

The only way to make AG 2's primary accept inserts, updates, and deletes is to manually fail over the Distributed Availability Group from AG 1. In the figure above, since AG 1 contains the writeable copy of the database, issuing a failover will make AG 2 the availability group that can handle inserts, updates, and deletes. How to fail one Distributed Availability Group over to another is documented under [Failover to a secondary availability group]( https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups).

> [!NOTE]
> Distributed availability groups in SQL Server 2016 only support failover from one availability group to another using the option FORCE_FAILOVER_ALLOW_DATA_LOSS.

## SQL Server Version and Edition Requirements for Distributed Availability Groups

Distributed availability groups currently only work with availability groups that are of the same major SQL Server version. For example, all availability groups participating in a distributed availability group must currently be SQL Server 2016. Since this feature did not exist in SQL Server 2012 or 2014, it is not possible to have an availability group created with those versions participate in a distributed availability group. 

> [!NOTE]
> Distributed availability groups can be configured with either Standard or Enterprise edition, but mixing editions in a Distributed Availability Group is not supported.

To install a service pack or cumulative update on a replica participating in a Distributed Availability Group, because there are two separate availability groups, the process is slightly different than a traditional availability group:

1. Start by updating the replicas of the second availability group in the distributed availability group.

2. Patch the replica(s) of the primary availability group in the distributed availability group.

3. As with a standard availability group, fail the primary availability group to one of its own replicas (not to the primary of the second availability group) and patch it. If there is no other replica other than the primary, a manual failover to the second availability group will be necessary.

> [!NOTE]
> No announcements have been made whether future versions of SQL Server will allow previous versions to participate in the same distributed availability group. If that scenario were enabled, it would allow distributed availability groups to be part of a SQL Server version upgrade plan.

### Windows Server Versions and Distributed Availability Groups

A distributed availability group spans multiple availability groups, each on their own underlying WSFC, and a distributed availability group is a SQL Server-only construct.  This means the WSFCs housing the individual availability groups can have different major versions of Windows Server. The major versions of SQL Server must be the same, as discussed in the previous section. Similarly to [the initial figure](#fig1), the following figure shows AG1 and AG2 participating in a distributed availability group, but each of the WSFCs is a different version of Windows Server.


![Distributed availability groups with WSFCs having different versions of Windows Server][3]

The individual WSFCs and their corresponding availability groups follow traditional rules, that is, they can be joined to a domain or not joined to a domain (Windows Server 2016 or later). When combining two different availability groups in a single Distributed Availability Group, there are four scenarios:

* Both WSFCs are joined to the same domain.
* Both WSFCs are joined to different domains.
* One WSFC is joined to a domain, and one WSFC is not joined to a domain.
* Neither WSFC is joined to a domain.

When both WSFCs are joined to the same domain (not trusted domains), nothing special should have to be done when creating the distributed availability group. For availability groups and WSFCs not joined to the same domain, certificates must be used to make the distributed availability group work, similar to how an availability group is created for a Domain Independent Availability Group. To see how to configure certificates for a distributed availability group, follow Steps 3-13 under [Create a Domain Independent Availability Group]
(domain-independent-availability-groups.md#create-a-domain-independent-availability-group).

With a distributed availability group, the primary replicas in each underlying availability group must have each other's certificates. Note that if you already have endpoints that are not using certificates, you will need to reconfigure those endpoints using [ALTER ENDPOINT](https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-endpoint-transact-sql) to reflect the use of certificates.

## Distributed Availability Group Usage Scenarios

Here are the three main usage scenarios for a Distributed Availability Group: 

* [Disaster recovery and easier multi-site configurations.](#disaster-recovery-and-multi-site-scenarios)
* [Migration to new hardware or configurations, which may include new hardware or changing the underlying operating systems.](#migration-using-a-distributed-availability-group)
* [Increasing the number of readable replicas beyond eight in a single availability group by spanning multiple availability groups.](#scaling-out-readable-replicas-with-distributed-accessibility-groups)

### Disaster Recovery and Multi-Site Scenarios

A traditional availability group requires that all servers be part of the same WSFC, which can make spanning multiple data centers challenging. The following figure shows what a traditional multi-site availability group architecture looks like, including the data flow. There is one primary replica that sends transactions to all secondary replicas. This is less flexible in some ways than a distributed availability group. For example, things like Active Directory (if applicable) and the witness for quorum in the WSFC must be implemented, and other aspects of a WSFC may need to be taken into account such as altering node votes.


![Traditional multi-site availability group][4]

Distributed availability groups offer a more flexible deployment scenario for availability groups that span multiple data centers. A distributed availability group may even be used where features like [log shipping]( https://docs.microsoft.com/en-us/sql/database-engine/log-shipping/about-log-shipping-sql-server) were in the past, however unlike a traditional availability group there is no ability to have delayed application of transactions. This means that availability groups or distributed availability groups cannot help in the event of human error where data is incorrectly updated or deleted.

Since a distributed availability group is loosely coupled, which in this case means it does not require a single WSFC and is maintained by SQL Server, the ability to configure disaster recovery at another site is much easier, as the WSFCs are maintained individually and the synchronization will primarily be asynchronous between the two availability groups. The primary replicas in each availability group are synchronizing their own secondary replicas.

* Only manual failover is supported for a Distributed Availability Group. In a disaster recovery situation where you are switching data centers, automatic should not be configured (with rare exceptions). 
* You most likely will not need to set some of the traditional items or parameters for multi-site/subnet WSFCs such as CrossSubnetThreshold, but you still need to see about network latency at a different layer for the data transport. The difference is that each WSFC is maintaining its own availability; it isn't one big entity of four nodes. You have two separate two-node WSFCs as shown in the previous figure.  
* Asynchronous data movement is recommended since this would be for disaster recovery purposes.
* If synchronous data movement is configured between the primary and at least one secondary replica of the second availability group, and synchronous is configured on the distributed availability group, a distributed availability group will wait until all synchronous copies acknowledge that they have the data.

### Migration Using a Distributed Availability Group

Because a distributed availability group supports two completely different availability group configurations, it enables not only easier disaster recovery and multi-site scenarios, but also migration scenarios. Whether you are migrating to new hardware or virtual machines (on premises or IaaS in the public cloud), configuring a distributed availability group allows a migration to occur where in the past backup, copy, and restore, or log shipping may have been used. 

The ability to migrate is especially useful in scenarios where the underlying OS will be changed and/or upgraded while keeping the SQL Server version the same. While Windows Server 2016 does allow a rolling upgrade from Windows Server 2012 R2 on the same hardware, most will still choose to deploy new hardware or virtual machines. 

To complete the migration to the new configuration, at the end of the process, stop all data traffic to the original availability group, and change the distributed availability group to synchronous data movement. This will ensure that the primary replica of the second availability group is fully synchronized, so there would be no data loss. Once this has been verified, follow the steps to fail the distributed availability group to the second availability group in the section "Failover to a secondary availability group" [here](https://msdn.microsoft.com/en-US/library/mt651673.aspx).

Post-migration, where the second availability group is now the new primary availability group, one of two things may need to occur:

* The listener on the secondary availability group may need to be renamed (and the old one on the original primary availability group deleted or renamed) or possibly recreated with the listener from the original primary availability group, so that any applications and end users can access the new configuration.
* If a rename or recreation is not possible, applications and end users will need to be pointed to the listener on the second availability group.

### Scaling Out Readable Replicas with Distributed Availability Groups

A single distributed availability group can have up to 16 secondary replicas as needed, which means it could have up 18 copies for reading including the two primary replicas of the different availability groups. This means more than one site could have near-real-time access for reporting to different applications.

Distributed availability groups can help you scale out a read-only farm more than just with a single availability group. There are two ways a distributed availability group can scale out readable replicas:

* The primary replica of the second availability group in a distributed availability group can be used to create another distributed availability group even though the database is not in RECOVERY.
* The primary replica of the first availability group can also be used to create another distributed availability group.

In other words, a primary can participate in two different distributed availability groups. The following figure shows AG 1 and AG 2 both participating in Distributed AG 1, while AG 2 and AG 3 are participating in Distributed AG 2. The primary replica of AG 2 is both a secondary for Distributed AG 1 and primary of Distributed AG 2.





![Scaling out reads with distributed availability groups][5]

The following figure shows AG1 as the primary for two different distributed availability groups (Distributed AG 1 comprised of AG 1 and AG2, and Distributed AG 2 comprised of AG 1 and AG 3).


![Another scaling out of reads using distributed availability groups example][6]


In both examples above, there could be up to 27 total replicas across the three availability groups, all of which could be used for read-only queries. 

[Read Only Routing]( https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server) currently does not work with distributed availability groups. This means that all queries, if using the Listener to connect, would go to the primary replica. Otherwise, each replica would need to be configured to allow all connections as a secondary replica and be accessed directly. This behavior may be changed in an update to SQL Server 2016 or in a future version of SQL Server.

## Initializing Secondary Availability Groups in a Distributed Availability Group

Distributed availability groups were designed with [automatic seeding]( https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/automatically-initialize-always-on-availability-group) to be the main method used to initialize the primary replica on the second availability group. A full database restore on the primary replica of the second availability group is possible:

1. Restore the database backup WITH NORECOVERY.
2. If necessary, restore the proper transaction log backups WITH NORECOVERY.
3. Create the second availability group without specifying a database name and with SEEDING_MODE set to AUTOMATIC.
4. Create the distributed availability group using automatic seeding.

When the second availability group's primary replica is added to the distributed availability group, it will be checked against the first availability group's primary database(s) and seeding will catch the database up to the source. There are a few caveats:

1. The output shown in `sys.dm_hadr_automatic_seeding` on the primary replica of the second availability group will show a `current_state` of FAILED with the reason "Seeding Check Message Timeout".
2. The current SQL Server Log on the primary replica of the second availability group will show that seeding worked and that [LSNs]( https://docs.microsoft.com/en-us/sql/relational-databases/sql-server-transaction-log-architecture-and-management-guide) were synchronized.
3. The output shown in `sys.dm_hadr_automatic_seeding` on the primary replica of the first availability group will show a current_state of COMPLETED. 
3. Seeding also has different behavior with distributed availability groups. The command `ALTER AVAILABILITY GROUP [AGName] GRANT CREATE ANY DATABASE` must be issued on a secondary replica for seeding to begin on that replica. While this is still true of any secondary replica participating in the underlying availability group, the primary replica of the second availability group already has the right permissions to allow seeding to begin once it is added to the distributed availability group. If the secondary replica of the second availability group in a distributed availability group has already had the `ALTER AVAILABILITY GROUP [AGName] GRANT CREATE ANY DATABASE` issued, 

### Viewing Distributed Availability Group Information 
	
As mentioned earlier, a distributed availability group is a SQL Server-only construct, and will not be seen in the underlying WSFC. The following figure shows two different WSFCs (CLUSTER_A and CLUSTER_B), each with various availability groups. Only AG1 in CLUSTER_A and AG2 in CLUSTER_B are discussed below. 

<!-- ![Two WSFCs with multiple availability groups through Powershell Get-ClusterGroup command][7]  -->
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

All detailed information about a distributed availability group is in SQL Server, specifically in the availability group dynamic management views (DMVs). Currently, the only information shown in SQL Server Management Studio (SSMS) for a distributed availability group is on the primary replica for the availability groups. Under the Availability Groups folder, SSMS will show that there is a distributed availability group as shown in the following figure. Note that AG1 is shown as a primary replica for an individual availability group local to that instance, not for a distributed availability group.

![View in SSMS of the Primary Replica on the first WSFC of a Distributed Availability Group][8]


However, if you right click on the distributed availability group, there are no options available (see following figure), and if you expand out Availability Databases, Availability Group Listeners, and Availability Replicas, they will all be empty. This could change in a future version of SSMS, but this is how it looks as of version 16.



![No options available for action][9]



Secondary replicas will not show anything in SSMS related to the distributed availability group as shown in the following figure. Note that these availability group names map to the roles shown in the [listing above for the CLUSTER_A WSFC](#fig7).

![View in SSMS of a Secondary Replica][10]



The same concepts hold true when using the dynamic management views. Using the query below, you can see all of the availability groups (regular and Distributed) and the nodes participating in them only if querying the primary replica in one of the WSFCs participating in the Distributed Availability Group. There is a new column in the DMV `sys.availability_groups` named `is_distributed` which is 1 when the availability group is a Distributed Availability Group. To see this column:

```
SELECT ag.[name] as 'AG Name', ag.Is_Distributed, ar.replica_server_name as 'Replica Name'
FROM 	sys.availability_groups ag, 
sys.availability_replicas ar       
WHERE	ag.group_id = ar.group_id
```


An example of output from the second WSFC participating in a distributed availability group is shown in the following figure. SPAG1 is comprised of two replicas: DENNIS and JY. However, the Distributed availability group named SPDistAG has the names of the two availability groups participating (SPAG1 and SPAG2) rather than the names of the instances as with a traditional availability group. 

![Example output of the query above][11]



In SQL Server Management Studio, it is important to note that any status shown on things like the Dashboard are for local synchronization only within that availability group. To see the health of a distributed availability group, you will need to query the DMVs to see what is going on. The following example query extends and refines the previous query:

```
SELECT ag.[name] as 'AG Name', ag.is_distributed, ar.replica_server_name as 'Underlying AG', ars.role_desc as 'Role', ars.synchronization_health_desc as 'Sync Status'
FROM 	sys.availability_groups ag, 
sys.availability_replicas ar,       
sys.dm_hadr_availability_replica_states ars       
WHERE	ar.replica_id = ars.replica_id
and 	ag.group_id = ar.group_id 
and	ag.is_distributed = 1
```
       
       
![Distributed availability group status][12]


Extending the previous query further, the underlying performance can also be seen via the dynamic management views by adding in `sys.dm_hadr_database_replicas_states`. The DMV currently only stores information about the second availability group. The following example query run on the primary availability group produces the sample output shown below:

```
SELECT ag.[name] as 'Distributed AG Name', ar.replica_server_name as 'Underlying AG', dbs.[name] as 'DB', ars.role_desc as 'Role', drs.synchronization_health_desc as 'Sync Status', drs.log_send_queue_size, drs.log_send_rate, drs.redo_queue_size, drs.redo_rate
FROM 	sys.databases dbs,
	sys.availability_groups ag,
	sys.availablity_replicas ar,
	sys.dm_hadr_availability_replica_states ars,
	sys.dm_hadr_database_replica_states drs
WHERE	drs.group_id = ag.group_id
and	ar.replica_id = ars.replica_id
and	ars.replica_id = drs.replica_id
and	dbs.database_id = drs.database_id
and	ag.is_distributed = 1
```

![Performance information for a distributed availability group][13]


### Next steps 

* [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)

* [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
 
* [Create an availability group with Transact-SQL](create-an-availability-group-transact-sql.md)

>This content written by [Allan Hirt](http://mvp.microsoft.com/en-us/PublicProfile/4025254?fullName=Allan%20Hirt), Microsoft Most Valued Professional.

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

 