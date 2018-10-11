---
title: "Use the Always On Availability Group dashboard (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.agdashboard.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
  - "Availability Groups [SQL Server], dashboard"
ms.assetid: c9ba2589-139e-42bc-99e1-94546717c64d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use the Always On Availability Group dashboard (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Database administrators use the Always On Availability Group dashboard to obtain an at-a-glance view the health of an availability group and its availability replicas and databases in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. Some of the typical uses for the availability group dashboard are:  
  
-   Choosing a replica for a manual failover.    
-   Estimating data loss if you force failover.  
-   Evaluating data-synchronization performance.   
-   Evaluating the performance impact of a synchronous-commit secondary replica 
  -  The dashboard provides key availability group states and performance indicators allowing you to easily make high availability operational decisions using the following types of information.  
-   Replica roll-up state    
-   Synchronization mode and state   
-   Estimate Data Loss    
-   Estimated Recovery Time (redo catch up)    
-   Database Replica details    
-   Synchronization mode and state    
-   Time to restore log  
  
##  Before You Begin  
  
### Prerequisites  
 You must be connected to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (server instance) that hosts either the primary replica or a secondary replica of an availability group.  
  
### Security  
  
#### Permissions  
 Requires CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions.  
  
##  To start the Always On dashboard  
  
1.  In Object Explorer, connect to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on which you want to run the Always On Dashboard.  
  
2.  Expand the **Always On High Availability** node, right-click the **Availability Groups** node, and then click **Show Dashboard**.  
  
###  Change Always On Dashboard Options  
 You can use the [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]**Options** dialog box to configure the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Always On Dashboard behavior for automatic refreshing and enabling an auto-defined Always On policy.  
  
1.  From the **Tools** menu, click **Options**.  
  
2.  To automatically refresh the dashboard, in the **Options** dialog box, select **Turn on automatic refresh**, enter the refresh interval in seconds, and then enter the number of times you want to retry the connection.  
  
3.  To enable a user-defined policy, select **Enable user-defined Always On policy**.  
  
##  Availability Group Summary  
 The availability group screen displays a summary line for each availability group for which the connected server instance hosts a replica. This pane displays the following columns.  
  
 **Availability Group Name**  
 The name of an availability group for which the connected server instance hosts a replica.  
  
 **Primary Instance**  
 Name of the server instance that is hosting the primary replica of the availability group.  
  
 **Failover Mode**  
 Displays the failover mode for which the replica is configured. The possible failover mode values are:  
  
-   **Automatic**. Indicates that one or more replicas is in automatic-failover mode.  
  
-   **Manual**. Indicates that no replica is automatic-failover mode.  
  
 **Issues**  
 Click the **Issues** link to open troubleshooting documentation for a given issue. For a list of all the Always On policy issues, see [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md).  
  
> [!TIP]  
>  Click the column headings to sort the availability group information by the name of the availability group, primary instance, failover mode, or Issue.  
  
##  <a name="AvGroupDetails"></a> Availability Group Details  
 The following detail information is displayed for the availability group that you select from the summary screen:  
  
 **Availability group state**  
 Displays the state of health for the availability group.  
  
 **Primary instance**  
 Name of the server instance that is hosting the primary replica of the availability group.  
  
 **Failover mode**  
 Displays the failover mode for which the replica is configured. The possible failover mode values are:  
  
-   **Automatic**. Indicates that one or more replicas is in automatic-failover mode.  
  
-   **Manual**. Indicates that no replica is automatic-failover mode.  
  
 **Cluster state**  
 Name and state of the cluster where the instance of the connected server and the availability group is a member node.  
  
##  <a name="AvReplicaDetails"></a> Availability Replica Details  

When connected to the primary replica, **Availability replica details** shows information from all replicas in the availability group. When connected to a secondary replica, the display only shows information from the connected replica.  

The **Availability replica** pane displays the following columns:  
  
 **Name**  
 The name of the server instance that hosts the availability replica. This column is shown by default.  
  
 **Role**  
 Indicates the current role of the availability replica, either **Primary** or **Secondary**. For information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] roles, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md). This column is shown by default.  
  
 **Failover Mode**  
 Displays the failover mode for which the replica is configured. The possible failover mode values are:  
  
-   **Automatic**. Indicates that one or more replicas is in automatic-failover mode.  
  
-   **Manual**. Indicates that no replica is automatic-failover mode.  
  
 **Synchronization State**  
 Indicates whether a secondary replica is currently synchronized with primary replica. This column is shown by default. The possible values are:  
  
-   **Not Synchronized**. One or more databases in the replica are not synchronized or have not yet been joined to the availability group.  
  
-   **Synchronizing**. One or more databases in the replica are being synchronized.  
  
-   **Synchronized**. All databases in the secondary replica are synchronized with the corresponding primary databases on the current primary replica, if any, or on the last primary replica.  
  
    > [!NOTE]  
    >  In performance mode, the database is never in the synchronized state.  
  
-   **NULL**. Unknown state. This value occurs when the local server instance cannot communicate with the WSFC failover cluster (that is the local node is not part of WSFC quorum).  
  
 **Issues**  
 Lists the issue name. This value is shown by default. For a list of all the Always On policy issues, see [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md).  
  
 **Availability Mode**  
 Indicates the replica property that you set separately for each availability replica. This value is hidden by default. The possible values are:  
  
-   **Asynchronous**. The secondary replica never becomes synchronized with the primary replica.  
  
-   **Synchronous**. When catching up to the primary database, a secondary database enters this state, and it remains caught up as long as data synchronization continues for the database.  
  
 **Primary Connection Mode**  
 Indicates the mode that is used to connect to the primary replica.  This value is hidden by default.  
  
 **Secondary Connection Mode**  
 Indicates the mode that is used to connect to the secondary replica.  This value is hidden by default.  
  
 **Connection State**  
 Indicates whether a secondary replica is currently connected to the primary replica. This column is hidden by default. The possible values are:  
  
-   **Disconnected**. For a remote availability replica, indicates that it is disconnected from the local availability replica. The response of the local replica to the Disconnected state depends on its role, as follows:  
  
    -   On the primary replica, if a secondary replica is disconnected, the secondary databases are marked as **Not Synchronized** on the primary replica, and the primary replica waits for the secondary to reconnect.  
  
    -   On the secondary replica, upon detecting that it is disconnected, the secondary replica attempts to reconnect to the primary replica.  
  
-   **Connected**. A remote availability replica that is currently connected to the local replica.  
  
 **Operational State**  
 Indicates the current operational state of the secondary replica. This value is hidden by default. The possible values are:  
  
 **0**. Pending failover    
 **1**. Pending    
 **2**. Online    
 **3**. Offline   
 **4**. Failed    
 **5**. Failed, no quorum  
  
 **NULL**. Replica is not local  
  
 **Last Connection Error No.**  
 Number of the last connection error.  This value is hidden by default.  
  
 **Last Connection Error Description**  
 Description of the last connection error.  This value is hidden by default.  
  
 **Last Connection Error Timestamp**  
 Timestamp of the last connection error. This value is hidden by default.  
  
> [!NOTE]  
>  For information about performance counters for availability replicas, see [SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md).  
  
##  Group by Availability Group information  
 To group the information, click **Group by**, and select one of the following:  
  
-   **Availability replicas**    
-   **Availability databases** 
-   **Synchronization state**  
-   **Failover readiness**   
-   **Issues**  
  
 The pane that displays the grouped information displays the following columns:  
  
 **Name**  
 The name of the availability database. This value is shown by default.  
  
 **Replica**  
 The name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts the availability replica. This value is shown by default.  
  
 **Synchronization State**  
 Indicates whether the availability database is currently synchronized with primary replica. This value is shown by default. The possible synchronization states are:  
  
-   **Not synchronizing**:  
-   
    -   For the primary role, indicates that the database is not ready to synchronize its transaction log with the corresponding secondary databases.   
    -   For a secondary database, indicates that the database has not started log synchronization because of a connection issue, is being suspended, or is going through transition states during startup or a role switch.  
  
-   **Synchronizing**:
-   
     On a primary replica:   
    - On a primary database, indicates that this database is ready to accept a scan request from a secondary database.  
    - On a secondary replica, indicates that there is active data movement going on for that secondary database. 
  
  
-   **Synchronized**: 
  
    - For a primary database, indicates that at least one secondary database is synchronized.
    - For a secondary database, indicates that the database is synchronized with the corresponding primary database.  
  
-   **Reverting**.  
  
     Indicates the phase in the undo process when a secondary database is actively getting pages from the primary database.  
  
    > [!CAUTION]  
    >  When a database is in the REVERTING state, forcing failover to the secondary replica can leave that database in a state in which it cannot be started.  
  
-   **Initializing**.  
  
     Indicates the phase of undo when the transaction log required for a secondary database to catch up to the undo LSN is being shipped and hardened on a secondary replica.  
  
    > [!CAUTION]  
    >  When a database is in the INITIALIZING state, forcing failover to the secondary replica will always leave that database in a state in which it cannot be started.  
  
 **Failover Readiness**  
 Indicates which availability replica can be failed over with or without potential data loss. This column is shown by default. The possible values are:  
  
-   **Data Loss**   
-   **No Data Loss**  
  
 **Issues**  
 Lists the issue name. This column is shown by default. The possible values are:  
  
-   **Warnings**. Click to display the thresholds and warnings issues.   
-   **Critical**. Click to display the critical issues.  
  
 For a list of all the Always On policy issues, see [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md).  
  
 **Suspended**  
 Indicates whether the database is **Suspended** or has been **Resumed**. This value is hidden by default.  
  
 **Suspend Reason**  
 Indicates the reason for the suspended state. This value is hidden by default.  
  
 **Estimate Data Loss (seconds)**  
 Indicates the time difference of the last transaction log record in the primary replica and secondary replica. If the primary replica fails, all transaction log records within the time window will be lost. This value is hidden by default.  
  
 **Estimated Recovery Time (seconds)**  
 Indicates the time in seconds it takes to redo the catch-up time. The *catch-up time* is the time it will take for the secondary replica to catch up with the primary replica. This value is hidden by default.  
  
 **Synchronization Performance (seconds)**  
 Indicates the time in seconds it takes to synchronize between the primary and secondary replicas. This value is hidden by default.  
  
 **Log Send Queue Size (KB)**  
 Indicates the number of log records in the log files of the primary database that have not been sent to the secondary replica. This value is hidden by default.  
  
 **Log Send Rate (KB/sec)**  
 Indicates the rate in KB per second at which log records are being sent to the secondary replica. This value is hidden by default.  
  
 **Redo Queue Size (KB)**  
 Indicates the number of log records in the log files of the secondary replica that have not yet been redone. This value is hidden by default.  
  
 **Redo Rate (KB/sec)**  
 Indicates the rate in KB per second at which the log records are being redone. This value is hidden by default.  
  
 **FileStream Send Rate (KB/sec)**  
 Indicates the rate of the FileStream in KB per second at which transactions are being sent to the replica. This value is hidden by default.  
  
 **End of Log LSN**  
 Indicates the actual log sequence number (LSN) that corresponds to the last log record in the log cache on the primary and secondary replicas. This value is hidden by default.  
  
 **Recovery LSN**  
 Indicates the end of the transaction log before the replica writes any new log records after recovery or failover on the primary replica. This value is hidden by default.  
  
 **Truncation LSN**  
 Indicates the minimum log truncation value for the primary replica. This value is hidden by default.  
  
 **Last Commit LSN**  
 Indicates the actual LSN corresponding to the last commit record in the transaction log. This value is hidden by default.  
  
 **Last Commit Time**  
 Indicates the time corresponding to the last commit record. This value is hidden by default.  
  
 **Last Sent LSN**  
 Indicates the point up to which all log blocks have been sent by the primary replica. This value is hidden by default.  
  
 **Last Sent Time**  
 Indicates the time when the last log block was sent. This value is hidden by default.  
  
 **Last Received LSN**  
 Indicates the point up to which all log blocks have been received by the secondary replica that hosts the secondary database. This value is hidden by default.  
  
 **Last Received Time**  
 Indicates the time when the log block identifier in last message received was read on the secondary replica. This value is hidden by default.  
  
 **Last Hardened LSN**  
 Indicates the point up to which all log records have been flushed to disk on the secondary replica. This value is hidden by default.  
  
 **Last Hardened Time**  
 Indicates the time when the log-block identifier was received for the last hardened LSN on the secondary replica. This value is hidden by default.  
  
 **Last Redone LSN**  
 Indicates the actual LSN of the log record that was redone last on the secondary replica. This value is hidden by default.  
  
 **Last Redone Time**  
 Indicates the time when the last log record was redone on the secondary database. This value is hidden by default.  
 

   > [!NOTE]  
   >  Most data is based on sys.dm_hadr_database_replica_states, so some restriction may apply. 
   >  For more information, please see [sys.dm_hadr_database_replica_states (Transact-SQL)](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md).


## Always On Availability Group latency reports
The availability group latency report is a reporting tool built into the availability group dashboard and available in the [SQL Server Management Studio 17.4](../../../ssms/download-sql-server-management-studio-ssms.md) release. This feature provides an easy-to-understand report detailing time spent during various phases of the log transport process. This provides a way to narrow down the potential cause of latency during the synchronization process. 

SQL Agent runs the data collection and must be enabled on both the primary replica, and at least one of the secondary replicas. View the report by right-clicking the availability group > Reports > Standard Reports in **Object Explorer** of SQL Server Management Studio.  

For more information, please see [Always On Availability Group latency reports](https://blogs.msdn.microsoft.com/sql_server_team/new-in-ssms-always-on-availability-group-latency-reports/).

## Related Tasks  
  
-   [Use Always On Policies to View the Health of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/use-always-on-policies-to-view-the-health-of-an-availability-group-sql-server.md)  
  
## See Also  
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/monitoring-of-availability-groups-sql-server.md)  
  
  
