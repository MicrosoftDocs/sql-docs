---
title: "Monitoring Database Mirroring (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring [SQL Server], database mirroring"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: a7b1b9b0-7c19-4acc-9de3-3a7c5e70694d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Monitoring Database Mirroring (SQL Server)
  This section introduces Database Mirroring Monitor and the **sp_dbmmonitor** system stored procedures, explains how database mirroring monitoring works (including the **Database Mirroring Monitor Job)**, and summarizes the information that you can monitor about database mirroring sessions. Additionally, this section introduces how to define warning thresholds for a set of predefined database mirroring events and how to set up alerts on any database mirroring event.  
  
 You can monitor a mirrored database during a mirroring session to verify whether and how well data is flowing. To set up and manage monitoring for one or more of the mirrored databases on a server instance, you can use either Database Mirroring Monitor or the **sp_dbmmonitor** system stored procedures.  
  
 A database mirroring monitoring job, **Database Mirroring Monitor Job**, operates in the background, independently of Database Mirroring Monitor. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent calls **Database Mirroring Monitor Job** at regular intervals, the default is once a minute, and the job calls a stored procedure that updates mirroring status. If you use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to start a mirroring session, **Database Mirroring Monitor Job** is created automatically. However, if you only use ALTER DATABASE *<database_name>* SET PARTNER to start mirroring, you must create the job by running a stored procedure.  
  
 **In this Topic:**  
  
-   [Monitoring Mirroring Status](#MonitoringStatus)  
  
-   [Additional Sources of Information About a Mirrored Database](#AdditionalSources)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="MonitoringStatus"></a> Monitoring Mirroring Status  
 To set up and manage monitoring for one or more of the mirrored databases on a server instance, you can use either Database Mirroring Monitor or the **dbmmonitor** system stored procedures. You can monitor a mirrored database during a mirroring session to verify whether and how well data is flowing.  
  
 Specifically, monitoring a mirrored database enables you to:  
  
-   Verify that mirroring is functioning.  
  
     Basic status includes knowing if the two server instances are up, that the servers are connected, and that the log is being moved from the principal to the mirror.  
  
-   Determine whether the mirror database is keeping up with the principal database.  
  
     During high-performance mode, a principal server can develop a backlog of unsent log records that still need to be sent from the principal server to the mirror server. Furthermore, in any operating mode, the mirror server can develop a backlog of unrestored log records that have been written to the log file but still need to be restored on the mirror database.  
  
-   Determine how much data was lost when the principal server instance becomes unavailable during high-performance mode.  
  
     You can determine data loss by looking at the amount of unsent transaction log (if any) and the time interval in which the lost transactions were committed at the principal.  
  
-   Compare current performance with past performance.  
  
     When problems are occurring, a database administrator can view a history of the mirroring performance to help in understanding the current state. Looking at the history can allow the user to detect trends in performance, identify patterns of performance problems (such as times of day when the network is slow or the number of commands entering the log is very large).  
  
-   Troubleshoot the cause of reduced data flow between mirroring partners.  
  
-   Set warning thresholds on key performance metrics.  
  
     If a new status row contains a value that exceeds a threshold, an informational event is sent to the Windows event log. A system administrator can then manually configure alerts based on these events. For more information, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).  
  
###  <a name="tools_for_monitoring_dbm_status"></a> Tools for Monitoring Database Mirroring Status  
 Mirroring status can be monitored using either Database Mirroring Monitor or the **sp_dbmmonitorresults** system stored procedure. These tools can be used to monitor database mirroring on any mirrored database on the local server instance by both System administrators, that is, members of the **sysadmin** fixed server role, and user who have been added to the **dbm_monitor** fixed database role in the **msdb** database by a system administrator. When using either tool, a system administrator can also manually refresh mirroring status.  
  
> [!NOTE]  
>  System administrators can also configure and view warning thresholds for key performance metrics. For more information, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).  
  
-   Database Mirroring Monitor  
  
     Database Mirroring Monitor is a graphical user interface tool that enables system administrators to view and update status and to configure warning thresholds on several key performance metrics. Database Mirroring Monitor can also be used by members of the **dbm_monitor** fixed database role to view the most recent row of the mirroring status table, though they cannot update the status table.  
  
     The monitor displays the status, including performance metrics, for a selected database on the **Status** tabbed page. The content of this page comes from both the principal and mirror server instances. The page is filled asynchronously as status is gathered through separate connections to the principal and mirror server instances. The monitor tries to update the status table at 30-second intervals. The update succeeds only if the table has not been updated within 15 seconds and the user is a member of the **sysadmin** fixed server role. For a summary of the information reported on the **Status** page, see [Status Displayed by the Database Mirroring Monitor](#perf_metrics_of_dbm_monitor), later in this topic.  
  
     For an introduction to the Database Mirroring Monitor interface, see [Database Mirroring Monitor Overview](database-mirroring-monitor-overview.md). For information on launching Database Mirroring Monitor, see [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md).  
  
-   System stored procedures  
  
     You can also retrieve or update the current status by running the **sp_dbmmonitorresults** system stored procedure. Other dbmmonitor stored procedures enable you to set up monitoring, change monitoring parameters, view the current update period, and drop monitoring on the server instance.  
  
     The following table introduces the stored procedures for managing and using database mirroring monitoring independently of the Database Mirroring Monitor.  
  
    |Procedure|Description|  
    |---------------|-----------------|  
    |[sp_dbmmonitoraddmonitoring](/sql/relational-databases/system-stored-procedures/sp-dbmmonitoraddmonitoring-transact-sql)|Creates a job that periodically updates the status information for every mirrored database on the server instance.|  
    |[sp_dbmmonitorchangemonitoring](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql)|Changes the value of a database mirroring monitoring parameter.|  
    |[sp_dbmmonitorhelpmonitoring](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql)|Returns the current update period.|  
    |[sp_dbmmonitorresults](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql)|Returns status rows for a monitored database and allows you to choose whether the procedure obtains the latest status beforehand.|  
    |[sp_dbmmonitordropmonitoring](/sql/relational-databases/system-stored-procedures/sp-dbmmonitordropmonitoring-transact-sql)|Stops and deletes the mirroring monitor job for all the databases on the server instance.|  
  
     The **dbmmonitor** system stored procedures can be used as an adjunct to the Database Mirroring Monitor. For example, even if monitoring was configured using **sp_dbmmonitoraddmonitoring**, Database Mirroring Monitor can be used to view the status.  
  
### How Monitoring Works  
 This section introduces the database mirroring status table, database mirroring monitor job and the monitor, how users can monitor database mirroring status, and how the monitoring job can be dropped.  
  
#### Database Mirroring Status Table  
 Database mirroring status is stored in an internal, undocumented database mirroring status table in the **msdb** database. This status table is automatically created the first time the mirroring status is updated on the server instance.  
  
 The status table may be updated either automatically or manually by a system administrator, with a minimum update interval of 15 seconds. The 15 second minimum prevents server instances from being overloaded with status requests.  
  
 The status table is updated automatically by both Database Mirroring Monitor and the database mirroring monitor job, if running. **Database Mirroring Monitor Job** updates the table once a minute by default (a system administrator can specify an update period of 1 to 120 minutes). Database Mirroring Monitor, in contrast, updates the table automatically every 30 seconds. For these updates, **Database Mirroring Monitor Job** and Database Mirroring Monitor call **sp_dbmmonitorupdate**.  
  
 The first time **sp_dbmmonitorupdate** runs, it creates the **database mirroring status** table and the **dbm_monitor** fixed database role in the **msdb** database. **sp_dbmmonitorupdate** usually updates the mirroring status by inserting a new row into the status table for every mirrored database on the server instance; for more information, see "Database Mirroring Status Table," later in this topic. This procedure also evaluates the performance metrics in the new rows and truncates rows older than the current retention period (the default is 7 days). For more information, see [sp_dbmmonitorupdate &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorupdate-transact-sql).  
  
> [!NOTE]  
>  Unless Database Mirroring Monitor is currently being used by a member of the **sysadmin** fixed server role, the status table is automatically updated only if the **Database Mirroring Monitor Job** exists and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running.  
  
#### Database Mirroring Monitor Job  
 The database mirroring monitoring job, **Database Mirroring Monitor Job**, operates independently of Database Mirroring Monitor. **Database Mirroring Monitor Job** is created automatically only if [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is used to start a mirroring session. If ALTER DATABASE *database_name* SET PARTNER commands are always used to start mirroring, the job exists only if the system administrator runs the **sp_dbmmonitoraddmonitoring** stored procedure.  
  
 After **Database Mirroring Monitor Job** is created, assuming that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running, the job is called once a minute, by default. The job then calls the **sp_dbmmonitorupdate** system stored procedure.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent calls **Database Mirroring Monitor Job** once a minute, by default, and the job calls **sp_dbmmonitorupdate** to update the status table. System administrators can change the update period by using the **sp_dbmmonitorchangemonitoring** system stored procedure, and they can view the current update period by using the **sp_dbmmonitorchangemonitoring** system stored procedure. For more information, see [sp_dbmmonitoraddmonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitoraddmonitoring-transact-sql) and [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql).  
  
#### Monitoring Database Mirroring Status (by System Administrators)  
 Members of the **sysadmin** fixed server role can view and update the status table  
  
-   Using Database Mirroring Monitor  
  
     When using Database Mirroring Monitor, a system administrator can manually refresh the **Status** page, navigation tree, or **History** page. This also updates the status table, unless it has already been updated within the previous 15 seconds.  
  
     To view the history of mirroring status on a given server instance, the system administrator can also click the **History** button for a server instance (on the **Status** page). The history is displayed in the **Database Mirroring History** dialog box. There, the system administrator can view some or all of the rows in the status table of the server instance.  
  
     For information about the **Status** page metrics, see Performance Metrics Displayed by the "Database Mirroring Monitor," later in this topic.  
  
-   Using **sp_dbmmonitorresults**  
  
     System administrators can use the **sp_dbmmonitorresults** system stored procedure to view and, optionally, to update the status table, if it has not been updated within the previous 15 seconds. This procedure calls the **sp_dbmmonitorupdate** procedure and returns one or more history rows, depending on the amount requested in the procedure call. For information about the status in its results set, see [sp_dbmmonitorresults &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql).  
  
#### Monitoring Database Mirroring Status (by dbm_monitor Members)  
 As mentioned, the first time **sp_dbmmonitorupdate** runs, it creates the **dbm_monitor** fixed database role in the **msdb** database. Members of the **dbm_monitor** fixed database role can view the existing mirroring status by using either Database Mirroring Monitor or the **sp_dbmmonitorresults** stored procedure. But these users cannot update the status table. To learn the age of the displayed status a user can look at the times in the **Principal log (***\<time>***)** and **Mirror log (***\<time>***)** labels on the **Status** page.  
  
 Members of the **dbm_monitor** fixed database role depend on the **Database Mirroring Monitor Job** to update the status table at regular intervals. If the job does not exist or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is stopped, the status becomes increasingly stale and may no longer reflect the configuration of the mirroring session. For example, after a failover, the partners might appear to share the same role-principal or mirror, or the current principal server might be shown as the mirror, while the current mirror server is shown as the principal.  
  
#### Dropping the Database Mirroring Monitor Job  
 The database mirroring monitor job, **Database Mirroring Monitor Job**, remains until it is dropped. The monitoring job must be managed by the system administrator. To drop **Database Mirroring Monitor Job**, use **sp_dbmmonitordropmonitoring**. For more information, see [sp_dbmmonitordropmonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitordropmonitoring-transact-sql).  
  
###  <a name="perf_metrics_of_dbm_monitor"></a> Status Displayed by the Database Mirroring Monitor  
 The **Status** page of the Database Mirroring Monitor describes the partners, and also the state of the mirroring session. The status includes performance metrics such as the state of the transaction log and other information that is intended to help currently estimate the time required to complete a failover and the potential of data loss, if the session is not synchronized. In addition, the **Status** page displays status and information about the mirroring session in general.  
  
> [!NOTE]  
>  For an introduction to the Database Mirroring Monitor and **Status** page, see [Tools for Monitoring Database Mirroring Status](#tools_for_monitoring_dbm_status), earlier in this topic.  
  
 The information provided for each of these is summarized in the following sections.  
  
#### Partners  
 The **Status** page displays the following information for each of the partners:  
  
-   Server instance  
  
     Name of the server instance whose status is displayed in the **Status** row.  
  
-   Current role  
  
     Current role of the server instance. The possible states are:  
  
    -   Principal  
  
    -   Mirror  
  
-   Mirroring state  
  
     The possible states are:  
  
    -   Unknown  
  
    -   Synchronizing  
  
    -   Synchronized  
  
    -   Suspended  
  
    -   Disconnected  
  
-   Witness connection  
  
     Connection status of the witness. The possible states are:  
  
    -   Unknown  
  
    -   Connected  
  
    -   Disconnected.  
  
#### Log on the Principal Server  
 The **Status** page displays the following information about the status of the log on the principal server as of the indicated time:  
  
-   Unsent log  
  
     The amount of log waiting in the send queue in kilobytes (KB).  
  
-   Oldest unsent transaction  
  
     Age of the oldest unsent transaction in the send queue. The age of this transaction indicates how many minutes of transactions have not yet been sent to the mirror server instance. This value helps measure the potential for data loss in terms of time.  
  
-   Time to send log (estimated)  
  
     Estimated number of minutes the principal server instance requires to send the log that is currently in the send queue to the mirror server instance based on the current send rate. The actual time to send the log will be affected by the rate of incoming transactions, which can vary significantly. However, the **Time to send log (estimated)** value can be useful for roughly estimating the time required for a manual failover.  
  
-   Current send rate  
  
     Rate at which transactions are being sent to the mirror server instance in KB per second.  
  
-   Current rate of new transactions  
  
     Rate at which incoming transactions are being entered into the principal's log in KB per second. To determine whether mirroring is falling behind, staying up, or catching up, compare this value to the **Estimated time to send log** value.  
  
#### Log on the Mirror Server  
 The **Status** page displays the following information about the status of the log on the mirror server as of the indicated time:  
  
-   Unrestored log  
  
     The amount of log waiting in the redo queue in KB.  
  
-   Time to restore log (estimated)  
  
     Approximate number of minutes required for the log currently in the redo queue to be applied to the mirror database.  
  
-   Current restore rate  
  
     Rate at which transactions are being restored into the mirror database (in KB per second).  
  
#### Mirroring Session  
 In addition, the **Status** page displays the following information about the mirroring session:  
  
-   Mirroring commit overhead  
  
     Average delay per transaction in milliseconds (relevant only in high-safety mode). This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue.  
  
-   Time to send and restore all current log (estimated)  
  
     Estimated time needed to send all of the unsent log that has been committed at the principal and to restore all of the log currently in the redo queue. This estimate may be less than the sum of the values of the **Time to send log (estimated)** and **Time to restore log (estimated)** fields, because sending and restoring can operate in parallel.  
  
-   Witness address  
  
     Network address of the witness server instance. For information about the format of this address, see [Specify a Server Network Address &#40;Database Mirroring&#41;](specify-a-server-network-address-database-mirroring.md).  
  
-   Operating mode  
  
     The operating mode of the database mirroring session:  
  
    -   High performance (asynchronous)  
  
    -   High safety without automatic failover (synchronous)  
  
    -   High safety with automatic failover (synchronous)  
  
##  <a name="AdditionalSources"></a> Additional Sources of Information About a Mirrored Database  
 In addition to using the Database Mirroring Monitor and dbmmonitor stored procedures to monitor a mirrored database and set up alerts on monitored performance variables, [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides catalog views, performance counters, and event notifications for database mirroring.  
  
 **In This Section:**  
  
-   [Database Mirroring Metadata](#DbmMetadata)  
  
-   [Database Mirroring Performance Counters](#DbmPerfCounters)  
  
-   [Database Mirroring Event Notifications](#DbmEventNotif)  
  
###  <a name="DbmMetadata"></a> Database Mirroring Metadata  
 Each database mirroring session is described in metadata that is exposed through the following catalog or dynamic management views:  
  
-   **sys.database_mirroring**  
  
     This view displays the database mirroring metadata for each mirrored database in a server instance. For more information, see [sys.database_mirroring &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-mirroring-transact-sql).  
  
-   **sys.database_mirroring_endpoints**  
  
     The **sys.database_mirroring_endpoints** catalog view displays information about the database mirroring endpoint of the server instance. For more information, see [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql).  
  
-   **sys.database_mirroring_witnesses**  
  
     This catalog view displays the database mirroring metadata for each session in which a server instance is the witness. For more information, see [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses).  
  
-   **sys.dm_db_mirroring_connections**  
  
     This dynamic management view returns a row for each database mirroring network connection.  
  
     For more information, see [sys.dm_db_mirroring_connections &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/database-mirroring-sys-dm-db-mirroring-connections).  
  
###  <a name="DbmPerfCounters"></a> Database Mirroring Performance Counters  
 Performance counters let you monitor database mirroring performance. For example, you can examine the **Transaction Delay** counter to see if database mirroring is impacting performance on the principal server, you can examine the **Redo Queue** and **Log Send Queue** counters to see how well the mirror database is keeping up with the principal database. You can examine the **Log Bytes Sent/sec** counter to monitor the amount of log sent per second.  
  
 In Performance Monitor on either partner, performance counters are available in the database mirroring performance object (**SQLServer:Database Mirroring**). For more information, see [SQL Server, Database Mirroring Object](../../relational-databases/performance-monitor/sql-server-database-mirroring-object.md).  
  
 **To start the performance monitor**  
  
-   [Start System Monitor &#40;Windows&#41;](../../relational-databases/performance/start-system-monitor-windows.md)  
  
###  <a name="DbmEventNotif"></a> Database Mirroring Event Notifications  
 Event notifications are a special kind of database object. Event notifications execute in response to a variety of Transact-SQL data definition language (DDL) statements and SQL Trace events and send information about server and database events to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] service.  
  
 The following events are available for database mirroring:  
  
-   **Database Mirroring State Change** event class  
  
     This indicates when the mirroring state of a mirrored database changes. For more information, see [Database Mirroring State Change Event Class](../../relational-databases/event-classes/database-mirroring-state-change-event-class.md).  
  
-   **Audit Database Mirroring Login** event class  
  
     This reports audit messages related to database mirroring transport security. For more information, see [Audit Database Mirroring Login Event Class](../../relational-databases/event-classes/audit-database-mirroring-login-event-class.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md)  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
-   [View the State of a Mirrored Database &#40;SQL Server Management Studio&#41;](view-the-state-of-a-mirrored-database-sql-server-management-studio.md)  
  
 **Stored procedures**  
  
-   [sp_dbmmonitoraddmonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitoraddmonitoring-transact-sql)  
  
-   [sp_dbmmonitorchangealert &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorchangealert-transact-sql)  
  
-   [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql)  
  
-   [sp_dbmmonitordropalert &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitordropalert-transact-sql)  
  
-   [sp_dbmmonitordropmonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitordropmonitoring-transact-sql)  
  
-   [sp_dbmmonitorhelpalert &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorhelpalert-transact-sql)  
  
-   [sp_dbmmonitorhelpmonitoring &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql)  
  
-   [sp_dbmmonitorresults &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql)  
  
-   [sp_dbmmonitorupdate &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorupdate-transact-sql)  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [WMI Provider for Server Events Concepts](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md)  
  
  
