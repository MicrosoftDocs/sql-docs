---
title: "Database Mirroring History | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dbmmonitor.databasemirroringhistory.f1"
ms.assetid: 1d6e4b10-4a23-47d7-9918-c417992f09d3
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring History
  Use this dialog box to view the history of mirroring status for a mirrored database on a specified server instance.  
  
 **To use SQL Server Management Studio to monitor database mirroring**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## Options  
 **Server instance**  
 Name of the server instance from which the history is being reported.  
  
 **Database**  
 Name of the database whose history is being reported.  
  
 **Filter list by**  
 Lists filter values. Choosing a new value refreshes the grid automatically.  
  
 The possible filter values are:  
  
-   Last two hours  
  
-   Last four hours  
  
-   Last eight hours  
  
-   Last day  
  
-   Last two days  
  
-   Last 100 records  
  
-   Last 500 records  
  
-   Last 1000 records  
  
-   All records  
  
 **Refresh**  
 Click to refresh the history list.  
  
> [!NOTE]  
>  This dialog box does not automatically refresh the history list. To refresh the list, either click **Refresh** or choose another filter option. Only members of the **sysadmin** fixed server role can update the mirroring history.  
  
 **History**  
 Displays the history list. Clicking on a column header sorts the grid by that column. The list contains the following columns:  
  
|Column name|Description|  
|-----------------|-----------------|  
|**Time Recorded**|Timestamp of the history row.|  
|**Role**|Current mirroring role of the server instance for this database, either Principal or Mirror.|  
|**Mirroring State**|State of the database:<br /><br /> Disconnected<br /><br /> Pending Failover<br /><br /> Suspended<br /><br /> Synchronized<br /><br /> Synchronizing<br /><br /> Unknown|  
|**Witness Connection**|State of the witness connection in the mirroring session of the database, either Connected or Disconnected. If there is no witness, the value is NULL.|  
|**Unsent Log**|Size, in kilobytes (KB), of the unsent log in the send queue on the principal server instance.|  
|**Time to Send**|Approximate amount of time the principal server instance will require to send the log that is currently in the send queue to the mirror server instance (the *send rate*). Because the rate of incoming transactions can vary significantly, the time to send log is an estimate. However, the send rate can be useful for roughly estimating the time required for a manual failover.|  
|**Send Rate**|Rate at which transactions are being sent to the mirror server instance, in KB per second.|  
|**New Transaction Rate**|Rate at which incoming transactions are being entered into the principal's log, in KB per second. To determine whether mirroring is falling behind, staying up, or catching up, compare this value to the **Time to Send** value.|  
|**Oldest Unsent Transaction**|Age of the oldest unsent transaction in the send queue. The age of this transaction indicates how many minutes of transactions have not yet been sent to the mirror server instance. This value helps measure the potential for data loss in terms of time.|  
|**Unrestored Log**|The amount of log waiting in the redo queue, in KB.|  
|**Time to Restore**|Approximate number of minutes required for the log currently in the redo queue to be applied to the mirror database.|  
|**Restore Rate**|Rate at which transactions are being restored into the mirror database, in KB per second.|  
|**Mirror Commit Overhead**|Average delay per transaction in milliseconds (only in synchronous modes). This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue.|  
  
## See Also  
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
  
