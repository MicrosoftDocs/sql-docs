---
title: "Database Mirroring Monitor (Warnings Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dbmmonitor.warningsandalerts.f1"
ms.assetid: 01936122-961d-436b-ba3c-5f79fefe5469
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring Monitor (Warnings Page)
  Displays a read-only list of warnings supported on database mirroring events and the specified warning threshold values, if available.  
  
 **To use SQL Server Management Studio to monitor database mirroring**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## Columns  
 **Warning**  
 The warnings for which you can define a threshold include:  
  
|Warning|Threshold|  
|-------------|---------------|  
|**Warn if the unsent log exceeds the threshold**|Specifies how many kilobytes (KB) of unsent log will generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|**Warn if the unrestored log exceeds the threshold**|Specifies how many KB of unrestored log will generate a warning on the mirror server instance. This warning is useful for measuring failover time in terms of kilobytes. *Failover time* consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short additional time.<br /><br /> Note: For an automatic failover, the time for the system to notice the error is independent of the failover time.<br /><br /> For more information, see [Estimate the Interruption of Service During Role Switching &#40;Database Mirroring&#41;](estimate-the-interruption-of-service-during-role-switching-database-mirroring.md).|  
|**Warn if the age of the oldest unsent transaction exceeds the threshold**|Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|**Warn if the mirror commit overhead exceeds the threshold**|Specifies the number of milliseconds of average delay per transaction tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode.|  
  
 **Threshold at '** _<server_instance>_ **'**  
 For each of the warnings, displays the current user-specified threshold, if any, for one of the server instances. The full instance name of the server instance is indicated in the corresponding column heading.  
  
 For more information, see "Remarks," later in this topic.  
  
 **Set Thresholds**  
 Click this button to set a threshold for one warning on each of the failover partners.  
  
 For more information, see "Remarks," later in this topic.  
  
## Remarks  
 If information is currently unavailable for a server instance, the cells of the corresponding **Threshold at** column display a gray background and watermark text. If the monitor is not connected to the server instance, in every cell the grid displays either **Not connected to** _<SYSTEM_NAME>_ or **Not connected to** _<SYSTEM_NAME>_**\\**_<instance_name>_, depending on whether the instance is the default instance or a named instance. If the monitor is waiting for a query to return, the grid displays **Waiting for data...** in every cell.  
  
 When information is available, the cell for each warning displays either a specified threshold value (and unit of measurement), or **Not enabled**.  
  
 If a threshold is exceeded at the time the status table is refreshed, an event is logged to the Windows event log when the status row is recorded. By default, the status row is recorded once a minute if the monitor is not running. You can configure an alert on each type of logged event by using the SQL Server Agent or another program, such as Microsoft Management Operations Manager (MOM).  
  
 On a given partner, the events logged depend on its current role, principal or mirror. However, we recommend that you set a warning threshold for a given event on both partners to ensure that the warning persists if the database fails over. The appropriate threshold for each partner depends on the performance capabilities of that partner's system.  
  
> [!NOTE]  
>  You can also use the **sp_dbmmonitorchangealert** system stored procedure to configure thresholds for the equivalent events-unsent log, unrecovered log, oldest unsent transaction, and mirror commit overhead. For more information, see [sp_dbmmonitorchangealert &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorchangealert-transact-sql).  
  
 The following table shows the event ID associated with each warning.  
  
|Database Mirroring Monitor warning|Event name|Event ID|  
|----------------------------------------|----------------|--------------|  
|**Warn if the unsent log exceeds the threshold**|Unsent log|32042|  
|**Warn if the unrestored log exceeds the threshold**|Unrestored log|32043|  
|**Warn if the age of the oldest unsent transaction exceeds the threshold**|Oldest unsent transaction|32044|  
|**Warn if the mirror commit overhead exceeds the threshold**|Mirror commit overhead|32045|  
  
## Permissions  
 For full access, requires membership in the **sysadmin** fixed server role. Only members of **sysadmin** can configure and view warning thresholds for key performance metrics.  
  
 Membership in the **dbm_monitor** role enables you to view only the most recent status row on the **Warnings** Page.  
  
## See Also  
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
  
