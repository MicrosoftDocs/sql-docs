---
title: "WSFC Disaster Recovery through Forced Quorum (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], WSFC clusters"
  - "quorum [SQL Server], AlwaysOn and WSFC quorum"
  - "failover clustering [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 6cefdc18-899e-410c-9ae4-d6080f724046
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# WSFC Disaster Recovery through Forced Quorum (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Quorum failure is usually caused by a systemic disaster, or a persistent communications failure, or a misconfiguration involving several nodes in the WSFC cluster.  Manual intervention is required to recovery from a quorum failure.  
  
-   **Before you start:**  [Prerequisites](#Prerequisites), [Security](#Security)  
  
-   **WSFC Disaster Recovery through the Forced Quorum Procedure** [WSFC Disaster Recovery through the Forced Quorum Procedure](#Main)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="BeforeYouBegin"></a> Before You Start  
  
###  <a name="Prerequisites"></a> Prerequisites  
 The Forced Quorum Procedure assumes that a healthy quorum existed before the quorum failure.  
  
> [!WARNING]  
>  The user should be well-informed on the concepts and interactions of Windows Server Failover Clustering, WSFC Quorum Models, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and the environment's specific deployment configuration.  
>   
>  For more information, see:  [Windows Server Failover Clustering (WSFC) with SQL Server](https://msdn.microsoft.com/library/hh270278\(v=SQL.110\).aspx), [WSFC Quorum Modes and Voting Configuration (SQL Server)](https://msdn.microsoft.com/library/hh270280\(v=SQL.110\).aspx)  
  
###  <a name="Security"></a> Security  
 The user must be a domain account that is member of the local Administrators group on each node of the WSFC cluster.  
  
##  <a name="Main"></a> WSFC Disaster Recovery through the Forced Quorum Procedure  
 Remember that quorum failure will cause all clustered services, SQL Server instances, and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], in the WSFC cluster to be set offline, because the cluster, as configured, cannot ensure node-level fault tolerance.  A quorum failure means that healthy voting nodes in the WSFC cluster no longer satisfy the quorum model. Some nodes may have failed completely, and some may have just shut down the WSFC service and are otherwise healthy, except for the loss of the ability to communicate with a quorum.  
  
 To bring the WSFC cluster back online, you must correct the root cause of the quorum failure under the existing configuration, recover the affected databases as needed, and you may want to reconfigure the remaining nodes in the WSFC cluster to reflect the surviving cluster topology.  
  
 You can use the *forced quorum* procedure on a WSFC cluster node to override the safety controls that took the cluster offline.  This effectively tells the cluster to suspend the quorum voting checks, and lets you bring the WSFC cluster resources and SQL Server back online on any of the nodes in the cluster.  
  
 This type of disaster recovery process should include the following steps:  
  
#### To Recover from Quorum Failure:  
  
1.  **Determine the scope of the failure.** Identify which availability groups or SQL Server instances are non-responsive, which cluster nodes are online and available for post-disaster use, and examine the Windows event logs and the SQL Server system logs.  Where practical, you should preserve forensic data and system logs for later analysis.  
  
    > [!TIP]  
    >  On a responsive instance of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], you can obtain information about the health of availability groups that possess an availability replica on the local server instance by querying the [sys.dm_hadr_availability_group_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md) dynamic management view (DMV).  
  
2.  **Start the WSFC cluster by using forced quorum on a single node.** Identify a node with a minimal number of component failures, other than that the WSFC cluster service was shut down.  Verify that this node can communicate with a majority of the other nodes.  
  
     On this node, manually force the cluster to come online using the forced quorum procedure.  To minimize potential data loss, select a node that was last hosting an availability group primary replica.  
  
     For more information, see:  [Force a WSFC Cluster to Start Without a Quorum](https://msdn.microsoft.com/library/hh270275\(v=SQL.110\).aspx)  
  
    > [!NOTE]  
    >  The forced quorum setting has a cluster-wide affect to block quorum checks until the logical WSFC cluster achieves a majority of votes and automatically transitions to a regular quorum mode of operation.  
  
3.  **Start the WSFC service normally on each otherwise healthy node, one at a time.** You do not have to specify the forced quorum option when you start the cluster service on the other nodes.  
  
     As the WSFC service on each node comes back online, it negotiates with the other healthy nodes to synchronize the new cluster configuration state.  Remember to do this one node at a time to prevent potential race conditions in resolving the last known state of the cluster.  
  
    > [!WARNING]  
    >  Ensure that each node that you start can communicate with the other newly online nodes.  Consider disabling the WSFC service on the other nodes.  Otherwise,  you run the risk of creating more than one quorum node set; that is a split-brain scenario. If your findings in step 1 were accurate, this should not occur.  
  
4.  **Apply new quorum mode and node vote configuration.** If forcing quorum successfully restarted all the nodes in the cluster and the root cause of the quorum failure has been corrected, changes to the original quorum mode and node vote configuration are unnecessary.  
  
     Otherwise, you should evaluate the newly recovered cluster node and availability replica topology, and change the quorum mode and vote assignments for each node as appropriate. Un-recovered nodes should be set offline or have their node votes set to zero.  
  
    > [!TIP]  
    >  At this point, the nodes and SQL Server instances in the cluster may appear to be restored back to regular operation.  However, a healthy quorum may still not exist.  Using the Failover Cluster Manager, or the Always On Dashboard within SQL Server Management Studio, or the appropriate DMVs, verify that a quorum has been restored.  
  
5.  **Recover availability group database replicas as needed.** Non-availability group databases should recover and come back online on their own as part of the regular SQL Server startup process.  
  
     You can minimize potential data loss and recovery time for the availability group replicas by bringing them back online in this sequence:  primary replica, synchronous secondary replicas, asynchronous secondary replicas.  
  
6.  **Repair or replace failed components and re-validate cluster.** Now that you have recovered from the initial disaster and quorum failure, you should repair or replace the failed nodes and adjust related WSFC and Always On configurations accordingly.  This can include dropping availability group replicas, evicting nodes from the cluster, or flattening and re-installing software on a node.  
  
     You must repair or remove all failed availability replicas.  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will not truncate the transaction log past the last known point of the farthest behind availability replica.   If a failed replica is not repaired or removed from the availability group, the transaction logs will grow and you will run the risk of running out of transaction log space on the other replicas.  
  
    > [!NOTE]  
    >  If you run the WSFC Validate a Configuration Wizard when an availability group listener exists on the WSFC cluster, the wizard generates the following incorrect warning message:  
    >   
    >  "The RegisterAllProviderIP property for network name 'Name:<network_name>' is set to 1 For the current cluster configuration this value should be set to 0."  
    >   
    >  Please ignore this message.  
  
7.  **Repeat step 4 as needed.** The goal is to re-establish the appropriate level of fault tolerance and high availability for healthy operations.  
  
8.  **Conduct RPO/RTO analysis.** You should analyze SQL Server system logs, database timestamps, and Windows event logs to determine root cause of the failure, and to document actual recovery point and recovery time experiences.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Force a WSFC Cluster to Start Without a Quorum](../../../sql-server/failover-clusters/windows/force-a-wsfc-cluster-to-start-without-a-quorum.md)  
  
-   [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [View Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/view-cluster-quorum-nodeweight-settings.md)  
  
-   [Configure Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/configure-cluster-quorum-nodeweight-settings.md)  
  
-   [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [View Events and Logs for a Failover Cluster](https://technet.microsoft.com/library/cc772342\(WS.10\).aspx)  
  
-   [Get-ClusterLog Failover Cluster Cmdlet](https://technet.microsoft.com/library/ee461045.aspx)  
  
## See Also  
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)  
  
  
