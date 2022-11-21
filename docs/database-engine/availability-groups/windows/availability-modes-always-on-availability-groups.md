---
title: "Availability modes for an availability group"
description: "Learn the three different availability modes for an Always On availability group: asynchronous-commit mode, synchronous-commit mode, and configuration only mode."
author: MashaMSFT
ms.author: mathoma
ms.date: "10/16/2017"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], asynchronous commit"
  - "synchronous-commit availability mode"
  - "Availability Groups [SQL Server], synchronous commit"
  - "asynchronous-commit availability mode"
  - "Availability Groups [SQL Server], availability modes"
---
# Differences between availability modes for an Always On availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  In [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], the *availability mode* is a replica property that determines whether a given availability replica can run in synchronous-commit mode. For each availability replica, the availability mode must be configured for either synchronous-commit mode, asynchronous-commit, or configuration only mode.  If the primary replica is configured for *asynchronous-commit mode*, it does not wait for any secondary replica to write incoming transaction log records to disk (to *harden the log*). If a given secondary replica is configured for asynchronous-commit mode, the primary replica does not wait for that secondary replica to harden the log. If both the primary replica and a given secondary replica are both configured for *synchronous-commit mode*, the primary replica waits for the secondary replica to confirm that it has hardened the log (unless the secondary replica fails to ping the primary replica within the primary's *session-timeout period*). 
  

> [!NOTE]  
>  If primary's session-timeout period is exceeded by a secondary replica, the primary replica temporarily shifts into asynchronous-commit mode for that secondary replica. When the secondary replica reconnects with the primary replica, they resume synchronous-commit mode.  
  
##  <a name="SupportedAvModes"></a> Supported Availability Modes  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] supports three availability modes-asynchronous-commit mode, synchronous-commit mode, and configuration only mode as follows:  
  
-   *Asynchronous-commit mode* is a disaster-recovery solution that works well when the availability replicas are distributed over considerable distances. If every secondary replica is running under asynchronous-commit mode, the primary replica does not wait for any of the secondary replicas to harden the log. Rather, immediately after writing the log record to the local log file, the primary replica sends the transaction confirmation to the client. The primary replica runs with minimum transaction latency in relation to a secondary replica that is configured for asynchronous-commit mode.  If the current primary is configured for asynchronous commit availability mode, it will commit transactions asynchronously for all secondary replicas regardless of their individual availability mode settings.  
  
     For more information, see [Asynchronous-Commit Availability Mode](#AsyncCommitAvMode), later in this topic.  
  
-   *Synchronous-commit mode* emphasizes high availability over performance, at the cost of increased transaction latency. Under synchronous-commit mode, transactions wait to send the transaction confirmation to the client until the secondary replica has hardened the log to disk. When data synchronization begins on a secondary database, the secondary replica begins applying incoming log records from the corresponding primary database. As soon as every log record has been hardened, the secondary database enters the SYNCHRONIZED state. Thereafter, every new transaction is hardened by the secondary replica before the log record is written to the local log file. When all the secondary databases of a given secondary replica are synchronized, synchronous-commit mode supports manual failover and, optionally, automatic failover.  
  
     For more information, see [Synchronous-Commit Availability Mode](#SyncCommitAvMode), later in this topic.  

-   *Configuration only mode* applies to availability groups that are not on a Windows Server Failover Cluster. A replica in configuration only mode does not contain user data. In configuration only mode, the replica master database stores availability group configuration metadata. For more information see [Availability group with configuration only replica](../../../linux/sql-server-linux-availability-group-ha.md).
  
 The following illustration shows an availability group with five availability replicas. The primary replica and one secondary replica are configured for synchronous-commit mode with automatic failover. Another secondary replica is configured for synchronous-commit mode with only planned manual failover, and two secondary replicas are configured for asynchronous-commit mode, which supports only forced manual failover (typically called *forced failover*).  
  
 ![Availability and failover modes of replicas](../../../database-engine/availability-groups/windows/media/aoag-availabilityandfailovermodes.gif "Availability and failover modes of replicas")  
  
 The synchronization and failover behavior between two availability replicas depends on the availability mode of both replicas. For example, for synchronous commit to occur, both the current primary replica and the secondary replica in question must be configured for synchronous commit. Likewise, for automatic failover to occur, both replicas need to be configured for automatic failover. Therefore, the behavior for the illustrated deployment scenario above can be summarized in the following table, which explores the behavior with each potential primary replica:  
  
|Current Primary Replica|Automatic Failover Targets|Synchronous-Commit Mode Behavior With|Asynchronous-Commit Mode Behavior With|Automatic failover possible|  
|-----------------------------|--------------------------------|--------------------------------------------|---------------------------------------------|---------------------------------|  
|01|02|02 and 03|04|Yes|  
|02|01|01 and 03|04|Yes|  
|03||01 and 02|04|No|  
|04|||01, 02, and 03|No|  
  
 Typically, Node 04 as an asynchronous-commit replica, is deployed in a disaster recovery site. The fact that Nodes 01, 02, and 03 remain at asynchronous-commit mode after failing over to Node 04 helps prevent potential performance degradation in your availability group due to high network latency between the two sites.  
  
##  <a name="AsyncCommitAvMode"></a> Asynchronous-Commit Availability Mode  
 Under *asynchronous-commit mode*, the secondary replica never becomes synchronized with the primary replica. Though a given secondary database might catch up to the corresponding primary database, any secondary database could lag behind at any point. Asynchronous-commit mode can be useful in a disaster-recovery scenario in which the primary replica and the secondary replica are separated by a significant distance and where you do not want small errors to impact the primary replica or in situations where performance is more important than synchronized data protection. Furthermore, since the primary replica does not wait for acknowledgements from the secondary replica, problems on the secondary replica never impact the primary replica.  
  
 An asynchronous-commit secondary replica attempts to keep up with the log records received from the primary replica. But asynchronous-commit secondary databases always remain unsynchronized and are likely to lag somewhat behind the corresponding primary databases. Typically the gap between an asynchronous-commit secondary database and the corresponding primary database is small. But the gap can become substantial if the server hosting the secondary replica is over loaded or the network is slow.  
  
 The only form of failover supported by asynchronous-commit mode is forced failover (with possible data loss). Forcing failover is a last resort intended only for situations in which the current primary replica will remain unavailable for an extended period and immediate availability of primary databases is more critical than the risk of possible data loss.The failover target must be a replica whose role is in the SECONDARY or RESOLVING state. The failover target transitions to the primary role, and its copies of the databases become the primary database. Any remaining secondary databases, along with the former primary databases, once they become available, are suspended until you manually resume them individually. Under asynchronous-commit mode, any transaction logs that the original primary replica had not yet sent to the former secondary replica are lost. This means that some or all of the new primary databases might be lacking recently committed transactions. For more information on how forced failover works and on best practices for using it, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
##  <a name="SyncCommitAvMode"></a> Synchronous-Commit Availability Mode  
 Under synchronous-commit availability mode (*synchronous-commit mode*), after being joined to an availability group, a secondary database catches up to the corresponding primary database and enters the SYNCHRONIZED state. The secondary database remains SYNCHRONIZED as long as data synchronization continues. This guarantees that every transaction that is committed on a given primary database has also been committed on the corresponding secondary database. When every secondary database on a given secondary replica is synchronized, the synchronization-health state of the secondary replica as a whole is HEALTHY.  
  
 **In This Section:**  
  
-   [Factors That Disrupt Data Synchronization](#DisruptSync)  
  
-   [How Synchronization Works on a Secondary Replica](#HowSyncWorks)  
  
-   [Synchronous-Commit Mode with Only Manual Failover](#SyncCommitWithManual)  
  
-   [Synchronous-Commit Mode with Automatic Failover](#SyncCommitWithAuto)  
  
###  <a name="DisruptSync"></a> Factors That Disrupt Data Synchronization  
 Once all of its databases are synchronized, a secondary replica enters the HEALTHY state. The synchronized secondary replica will remain healthy unless one of the following occurs:  
  
-   A network or computer delay or glitch causes the session between the secondary replica and primary replica to timeout.  
  
    > [!NOTE]  
    >  For information about the session-time property of availability replicas, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
-   You suspend a secondary database on the secondary replica. The secondary replica ceases to be synchronized, and its synchronization-health state is marked as NOT_HEALTHY. The secondary replica cannot become healthy again until the suspended secondary database is either resumed and resynchronized or removed from the availability group.  
  
-   You add a primary database the availability group. Previously synchronized secondary replicas enter the NOT_HEALTHY synchronization-health state. This state indicates that at least one database is in the NOT SYNCHRONIZING synchronization state. A given secondary replica cannot be HEALTHY again until a corresponding secondary database has been prepared on the replica, has been joined to the availability group, and has become synchronized with the new primary database.  
  
-   You change the primary replica or the secondary replica to asynchronous-commit availability mode. After changing to asynchronous-commit mode, the secondary replica will remain in the HEALTHY synchronization-health state as long as data synchronization continues. However, if only the primary replica is changed to asynchronous-commit mode, the synchronous-commit secondary replica will enter the PARTIALLY_HEALTHY synchronization-health state. This state indicates that at least one database is in the SYNCHRONIZING synchronization state, but none of the databases are in the NOT SYNCHRONIZING state.  
  
-   You change any secondary replica to synchronous-commit availability mode. This causes that secondary replica to be marked as in the PARTIALLY_HEALTHY synchronization-health state until all of its databases are in the SYNCHRONIZED synchronization state.  
  
> [!TIP]  
>  To view the synchronization health of an availability group, availability replica, or availability database, query the **synchronization_health** or **synchronization_health_desc** column of [sys.dm_hadr_availability_group_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md), [sys.dm_hadr_availability_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md), or [sys.dm_hadr_database_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md), respectively.  
  
###  <a name="HowSyncWorks"></a> How Synchronization Works on a Secondary Replica  
 Under the synchronous-commit mode, after a secondary replica joins the availability group and establishes a session with the primary replica, the secondary replica writes incoming log records to disk (*hardens the log*) and sends a confirmation message to the primary replica. Once the hardened log on the secondary database has caught up the end of log on the primary database, the state of the secondary database is set to SYNCHRONIZED. The time required for synchronization depends essentially on how far the secondary database was behind the primary database at the start of the session (measured by the number of log records initially received from the primary replica), the work load on the primary database, and the speed of the computer of the server instance that hosts the secondary replica.  
  
 Synchronous operation is maintained in the following manner:  
  
1.  On receiving a transaction from a client, the primary replica writes the log for the transaction to the transaction log and concurrently sends the log record to the secondary replicas.  
  
2.  Once a log record is written to the transaction log of the primary database, the transaction can be undone only if there is a failover at this point to a secondary that did not receive the log. The primary replica waits for confirmation from the synchronous-commit secondary replica.  
  
3.  The secondary replica hardens the log and returns an acknowledgement to the primary replica.  
  
4.  On receiving the confirmation from the secondary replica, the primary replica finishes the commit processing and sends a confirmation message to the client.  
  
    > [!NOTE]  
    >  If a synchronous-commit secondary replica times out without confirming that it has hardened the log, the primary marks that secondary replica as failed. The connected state of the secondary replica changes to DISCONNECTED, and the primary replica stops waiting for confirmation from the secondary replica. This behavior ensures that a failed synchronous-commit secondary replica does not prevent hardening of the transaction log on the primary replica.  
  
 Synchronous-commit mode protects your data by requiring the data to be synchronized between two places, at the cost of somewhat increasing the latency of the transaction.  
  
### <a name="SyncCommitWithManual"></a> Synchronous-Commit Mode with Only Manual Failover  
 When these replicas are connected and the database is synchronized, manual failover is supported. If the secondary replica goes down, the primary replica is unaffected. The primary replica runs exposed if no SYNCHRONIZED replicas exist (that is, without sending data to any secondary replica). If the primary replica is lost, the secondary replicas enter the RESOLVING state, but the database owner can force a failover to the secondary replica (with possible data loss). For more information, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
###  <a name="SyncCommitWithAuto"></a> Synchronous-Commit Mode with Automatic Failover  
 Automatic failover provides high availability by ensuring that the database is quickly made available again after the loss of the primary replica. To configure an availability group for automatic failover, you need to set both the current primary replica and at least one secondary replica to synchronous-commit mode with automatic failover. You can have up to five automatic failover replicas.  
  
 Furthermore, for an automatic failover to be possible at a given time, this secondary replica must be synchronized with the primary replica (that is, the secondary databases are all synchronized), and the Windows Server Failover Clustering (WSFC) cluster must have quorum. If the primary replica becomes unavailable under these conditions, automatic failover occurs. The secondary replica switches to the role of primary, and it offers its database as the primary database. For more information, see the "Automatic Failover " section of the [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) topic.  
  
> [!NOTE]  
>  For information about WSFC quorum and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see For more information, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md).  

### Data latency on secondary replica
Implementing read-only access to secondary replicas is useful if your read-only workloads can tolerate some data latency. In situations where data latency is unacceptable, consider running read-only workloads against the primary replica.

The primary replica sends log records of changes on primary database to the secondary replicas. On each secondary database, a dedicated redo thread applies the log records. On a read-access secondary database, a given data change does not appear in query results until the log record that contains the change has been applied to the secondary database and the transaction has been committed on primary database.

This means that there is some latency, usually only a matter of seconds, between the primary and secondary replicas. In unusual cases, however, for example if network issues reduce throughput, latency can become significant. Latency increases when I/O bottlenecks occur and when data movement is suspended. To monitor suspended data movement, you can use the [Always On Dashboard](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md) or the [sys.dm_hadr_database_replica_states dynamic management view](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md).

For more information on investigating redo latency on the secondary replica, please see [Troubleshoot primary changes not reflected on secondary replica](../../../database-engine/availability-groups/windows/troubleshoot-primary-changes-not-reflected-on-secondary.md).
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To change the availability mode and failover mode**  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
 **To adjust quorum votes**  
  
-   [View Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/view-cluster-quorum-nodeweight-settings.md)  
  
-   [Configure Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/configure-cluster-quorum-nodeweight-settings.md)  
  
-   [Force a WSFC Cluster to Start Without a Quorum](../../../sql-server/failover-clusters/windows/force-a-wsfc-cluster-to-start-without-a-quorum.md)  
  
 **To perform a manual failover**  
  
-   [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [Use the Fail Over Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-fail-over-availability-group-wizard-sql-server-management-studio.md)  
  
 **To view availability group, availability replica, and database states**  
  
-   [sys.dm_hadr_availability_group_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md)  
  
-   [sys.dm_hadr_availability_replica_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md)  
  
-   [sys.dm_hadr_database_replica_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)  
  
