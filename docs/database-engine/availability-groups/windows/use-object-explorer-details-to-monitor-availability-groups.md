---
title: "Use Object Explorer Details to Monitor Availability Groups | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.OEdetails.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], databases"
  - "Availability Groups [SQL Server]"
ms.assetid: 84affc47-40e0-43d9-855e-468967068c35
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use Object Explorer Details to Monitor Availability Groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to use the **Object Explorer Details** pane of [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] to monitor and manage existing Always On availability groups, availability replicas, and availability databases.  
  
> [!NOTE]  
>  For information about using the Object Explorer Details pane, see [Object Explorer Details Pane](../../../ssms/object/object-explorer-details-pane.md).  
  
  
##  <a name="Prerequisites"></a> Prerequisites  
 You must be connected to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (server instance) that hosts either the primary replica or a secondary replica.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To monitor availability groups, availability replicas, and availability databases**  
  
1.  On the View menu, click **Object Explorer Details**, or press the **F7** key.  
  
2.  In Object Explorer, connect to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on which you want to monitor an availability group, and click the server name to expand the server tree.  
  
3.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
4.  The **Object Explorer Details** pane displays every availability group for which the connected server instance hosts a replica. For each availability group, the **Server Instance (Primary)** column displays the name of the server instance that is currently hosting the primary replica.  To display more information about a given availability group, select it in Object Explorer.  
  
5.  The **Object Explorer Details** pane then displays the **Availability Replicas** and **Availability Databases** nodes for this availability group:  
  
    -   When you expand the **Availability Group** node in Object Explorer and select the **Availability Replicas** node, the **Object Explorer Details** pane displays information about each of the availability replicas in the group. For more information, see [Availability Replica Details](#AvReplicaDetails), later in this topic.  
  
         To perform operations on multiple availability replicas, select them, and right-click them to open up a context menu that lists the available commands.  
  
    -   When you expand the **Availability Group** node in Object Explorer and select the **Availability Databases** node, the **Object Explorer Details** pane displays information about each of the availability databases in the group. For more information, see [Availability Database Details](#AvDbDetails), later in this topic.  
  
         To perform operations on multiple availability databases, select them, and right-click them to open up a context menu that lists the available commands.  
  
##  <a name="AvGroupsDetails"></a> Availability Groups Details  
 The **Availability Groups** details screen displays the following columns:  
  
 **Name**  
 Lists the **Availability Replicas**, **Availability Databases**, and **Availability Group** Listeners folders of the selected availability group.  
  
##  <a name="AvReplicaDetails"></a> Availability Replica Details  
 The **Availability Replica** details screen displays the following columns:  
  
 **Server Instance**  
 Displays the name of the server instance that hosts the availability replica, along with an icon that indicates the current connection state of the server instance to the local server instance.  
  
 **Role**  
 Indicates the current role of the availability replica, either **Primary** or **Secondary**. For information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] roles, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
 **Connection Mode in Secondary Role**  
 Indicates whether the databases of a given availability replica that is performing the secondary role (that is, is acting as a secondary replica) can accept connections from clients.  
  
 The possible values are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|**Disallow Connections**|No direct connections are allowed to the availability databases when this availability replica is acting as a secondary replica. Secondary databases are not available for read access.|  
|**Allow Only Read Intent Connections**|Only direct read-only connections are allowed  when this replica acting as a secondary replica. All database(s) in the replica are available for read access.|  
|**Allow All Connections**|All connections are allowed to these databases for read-only access when this replica acting as a secondary replica.|  
  
 **Connection State**  
 Indicates whether a secondary replica is currently connected to the primary replica. The possible values are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|**Disconnected**|For a remote availability replica, indicates that it is disconnected from the local availability replica. The response of the local replica to the Disconnected state depends on its role, as follows:<br /><br /> On the primary replica, if a secondary replica is disconnected, the secondary databases are marked as **Not Synchronized** on the primary replica, and the primary replica waits for the secondary to reconnect.<br /><br /> On the secondary replica, upon detecting that it is disconnected, the secondary replica attempts to reconnect to the primary replica.|  
|**Connected**|A remote availability replica that is currently connected to the local replica.|  
|**NULL**|If the local replica is a secondary replica, this value is NULL for other secondary replicas.|  
  
 **Synchronization State**  
 Indicates whether a secondary replica is currently synchronized with primary replica. The possible values are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|**Not Synchronized**|The database is not synchronized or has not yet been joined to the availability group.|  
|**Synchronized**|The database is synchronized with the primary database on the current primary replica, if any, or on the last primary replica.<br /><br /> Note: In performance mode, the database is never in the Synchronized state.|  
|**NULL**|Unknown state. This value occurs when the local server instance cannot communicate with the WSFC failover cluster (that is the local node is not part of WSFC quorum).|  
  
> [!NOTE]  
>  For information about performance counters for availability replicas, see [SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md).  
  
##  <a name="AvDbDetails"></a> Availability Database Details  
 The **Availability Database** details screen displays the following properties of the availability databases in a given availability group:  
  
 **Name**  
 The name of the availability database.  
  
 **Synchronization State**  
 Indicates whether the availability database is currently synchronized with primary replica.  
  
 The possible synchronization states are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|Synchronizing|The secondary database has received the transaction log records for the primary database that are not yet written to disk (hardened).<br /><br /> Note: In asynchronous-commit mode, the synchronization state is always **Synchronizing**.|  
|||  
  
 **Suspended**  
 Indicates whether the availability database is currently online. The possible values are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|**Suspended**|This state indicates that the database is suspended locally and needs to be manually resumed.<br /><br /> On the primary replica, the value is unreliable for a secondary database. To reliably determine whether a secondary database is suspended, query it on the secondary replica that hosts the database.|  
|**Not Joined**|Indicates that the secondary database either has not been joined to the availability group or has been removed from the group.|  
|**Online**|Indicates that the database is not suspended on the local availability replica and that the database is connected.|  
|**Not Connected**|Indicates that the secondary replica is currently unable to connect to the primary replica.|  
  
> [!NOTE]  
>  For information about performance counters for availability databases, see [SQL Server, Database Replica](../../../relational-databases/performance-monitor/sql-server-database-replica.md).  
  
## See Also  
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)   
 [View Availability Group Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)   
 [View Availability Replica Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-replica-properties-sql-server.md)  
  
  
