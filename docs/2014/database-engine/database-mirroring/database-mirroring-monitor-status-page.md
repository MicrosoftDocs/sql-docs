---
title: "Database Mirroring Monitor (Status Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dbmmonitor.status.f1"
ms.assetid: 4f64b4e1-89e9-4827-98fa-b92c3dc73b48
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring Monitor (Status Page)
  This read-only page displays the most recent mirroring status for the principal and mirror server instances of the database currently selected in the navigation tree. If information about an instance is currently unavailable, some of the cells in the **Status** grid corresponding to that instance are grayed out and display **Unknown**.  
  
 **To use SQL Server Management Studio to monitor database mirroring**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## Options  
 **Status**  
 Displays a grid containing the most recent high-level mirroring status of each of the principal and mirror server instances. The rows of the **Status** grid are in the following order:  
  
-   Principal server instance  
  
-   Mirror server instance  
  
 The columns are as follows:  
  
|Column name|Description|  
|-----------------|-----------------|  
|**Server Instance**|Name of the server instance whose status is displayed in the **Status** row.|  
|**Current Role**|Current role of the server instance, either **Principal** or **Mirror**.|  
|**Mirroring State**|The mirroring state reported by the server instance and an icon that indicates the severity of the state. The possible statuses and their associated icons are as follows:<br /><br /> -: Status **Unknown**. The monitor is not connected to either partner. The only available information is what has been cached by the monitor.<br /><br /> Warning icon: <br />                            Status **Synchronizing**.<br />                          The contents of the mirror database are lagging behind the contents of the principal database. The principal server instance is sending log records to the mirror server instance, which is applying the changes to the mirror database to roll it forward. At the start of a database mirroring session, the mirror and principal databases are in this state.<br /><br /> Standard database cylinder: Status<br />                            **Synchronized**.<br />                          When the mirror server becomes sufficiently caught up to the principal server, the database state changes to **Synchronized**. The database remains in this state as long as the principal server is sending changes to the mirror server and the mirror server is applying changes to the mirror database. For high-safety mode, automatic failover and manual failover are both possible, without any data loss. For high-performance mode, some data loss is always possible, even in the **Synchronized** state.<br /><br /> Warning icon: Status<br />                            **Suspended**.<br />                            The principal database is available but is not sending any logs to the mirror server.<br /><br /> Error icon: Status <br />                            **Disconnected**.<br />                          The server instance cannot connect to its partner.|  
|**Witness Connection**|Connection status of the witness, preceded by a status icon, **Unknown**, **Connected**, or **Disconnected**.|  
|**History**|Click to display the history of mirroring on the server instance. This opens the **Database Mirroring History** dialog box, which displays the history of mirroring status and statistics for a mirrored database on a given server instance.<br /><br /> The **History** button is dimmed if the monitor is not connected to the server instance.|  
  
 **Principal log (** *\<time>* **)**  
 Status of the log on the principal server instance as of the local time on the server instance, indicated by *\<time>*. The following parameters are displayed:  
  
 **Unsent log**  
 The amount of log waiting in the send queue (in kilobytes).  
  
 **Oldest unsent transaction**  
 Age of the oldest unsent transaction in the send queue. The age of this transaction indicates how many minutes of transactions have not yet been sent to the mirror server instance. This value helps measure the potential for data loss in terms of time.  
  
 **Time to send log (estimated)**  
 Approximate amount of time the principal server instance requires to send the log that is currently in the send queue to the mirror server instance (the *send rate*). Because the rate of incoming transactions can vary significantly, the time to send log is an estimate. However, the send rate can be useful for roughly estimating the time required for a manual failover.  
  
 **Current send rate**  
 Rate at which transactions are being sent to the mirror server instance in kilobytes (KB) per second.  
  
 **Current rate of new transactions**  
 Rate at which incoming transactions are being entered into the principal's log in KB per second. To determine whether mirroring is falling behind, staying up, or catching up, compare this value to the **Estimated time to send log** value.  
  
 **Mirror log (** *\<time>* **)**  
 Log status on the mirror server instance as of the local time on the server instance, indicated by *\<time>*. The following parameters are displayed:  
  
 **Unrestored log**  
 The amount of log waiting in the redo queue (in KB).  
  
 **Time to restore log (estimated)**  
 Approximate number of minutes required for the log currently in the redo queue to be applied to the mirror database.  
  
 **Current restore rate**  
 Rate at which transactions are being restored into the mirror database (in KB per second).  
  
 **Mirroring commit overhead**  
 Number of milliseconds of average delay per transaction tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode.  
  
 **Time to send and restore all current log (estimated)**  
 Time needed to send and restore all of the log that has been committed at the principal as of the current time. This time may be less than the sum of the values of the **Time to send log (estimated)** and **Time to restore log (estimated)** fields, because sending and restoring can operate in parallel. This estimate does predict the time required to send and restore new transactions committed at the principal while working through backlogs in the send queue.  
  
 **Witness address**  
 Network address of the witness server instance. For information about the format of this address, see [Specify a Server Network Address &#40;Database Mirroring&#41;](specify-a-server-network-address-database-mirroring.md).  
  
 **Operating mode**  
 The operating mode of the database mirroring session:  
  
-   **High performance (asynchronous)**  
  
-   **High safety without automatic failover (synchronous)**  
  
-   **High safety with automatic failover (synchronous)**  
  
## Remarks  
 Members of the **dbm_monitor** fixed database role can view the existing mirroring status by using either Database Mirroring Monitor or the **sp_dbmmonitorresults** stored procedure. But these users cannot update the status table. They depend on the **Database Mirroring Monitor Job**to update the status table at regular intervals. To learn the age of the displayed status a user can look at the times in the **Principal log (***\<time>***)** and **Mirror log (***\<time>***)** labels.  
  
 If this job does not exist or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is stopped, the status becomes increasingly stale and may no longer reflect the configuration of the mirroring session. For example, after a failover, the partners might appear to share the same role-principal or mirror, or the current principal server might be shown as the mirror, while the current mirror server is shown as the principal.  
  
## See Also  
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
  
