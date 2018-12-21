---
title: "Database Mirroring Monitor Overview | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dbmmonitor.main.f1"
helpviewer_keywords: 
  - "Database Mirroring Monitor [SQL Server], interface"
ms.assetid: 8ebbdcd6-565a-498f-b674-289c84b985eb
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring Monitor Overview
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  If you have the correct permissions, you can use Database Mirroring Monitor to monitor any subset of the mirrored databases on a server instance. Monitoring enables you to verify whether and how well data is flowing in the database mirroring session. Database Mirroring Monitor is also useful for troubleshooting the cause of reduced data flow.  
  
 You can register any of your mirrored databases for monitoring on each of the failover partners individually. When you register a database, Database Mirroring Monitor caches the following information about the database:  
  
-   Database name  
  
-   The names of the two partner server instances  
  
-   The last known roles of each partner (principal or mirror)  
  
## Permissions  
 To monitor database mirroring, you must be a member of either the **sysadmin** fixed server role or the **dbm_monitor** fixed database role in the **msdb** database on the server instance. If you are a member of **sysadmin** or **dbm_monitor** on only one of the partner server instances, the monitor can connect only to that partner; the monitor cannot retrieve information from the other partner.  
  
 If you are a member of just **dbm_monitor** on a server instance, you will have limited permissions on that server instance. You will only be able to view the most recent status row. If you connect to a server instance using **dbm_monitor** permissions, Database Mirroring Monitor informs you that you have limited permissions.  
  
> [!IMPORTANT]  
>  The **dbm_monitor** fixed database role is created in the **msdb** database when the first database is registered in Database Mirroring Monitor. The new **dbm_monitor** role has no members until a system administrator assigns users to the role.  
  
## Navigation Tree  
 If any databases have been registered for monitoring by the Database Mirroring Monitor, a list of registered databases is displayed in the navigation tree. The tree automatically refreshes every 30 seconds. To see the status of a registered database, select it. For more information, see "Detail Pane," later in this topic.  
  
 For each registered database, the following information is displayed:  
  
 _<Database_name>_ **(** _\<Status>_ **,** _<PRINCIPAL_SERVER>_ **->** _<MIRROR_SERVER>_ **)**  
  
 *<Database_name>*  
 The name of a mirrored database that is registered with the Database Mirroring Monitor.  
  
 *\<Status>*  
 The possible statuses and their associated icons are as follows:  
  
|Icon|Status|Description|  
|----------|------------|-----------------|  
|Warning icon|**Unknown**|The monitor is not connected to either partner. The only available information is what has been cached by the monitor.|  
|Warning icon|**Synchronizing**|The contents of the mirror database are lagging behind the contents of the principal database. The principal server instance is sending log records to the mirror server instance, which is applying the changes to the mirror database to roll it forward.<br /><br /> At the start of a database mirroring session, the mirror and principal databases are in this state.|  
|Standard database cylinder|**Synchronized**|When the mirror server becomes sufficiently caught up to the principal server, the database state changes to **Synchronized**. The database remains in this state as long as the principal server continues to send changes to the mirror server and the mirror server continues to apply changes to the mirror database.<br /><br /> For high-safety mode, automatic failover and manual failover are both possible, without any data loss.<br /><br /> For high-performance mode, some data loss is always possible, even in the **Synchronized** state.|  
|Warning icon|**Suspended**|The principal database is available but is not sending any logs to the mirror server.|  
|Error icon|**Disconnected**|The server instance cannot connect to its partner.|  
  
 *<PRINCIPAL_SERVER>*  
 The name of the partner that is currently the principal server instance. The name is in the following format:  
  
 *<SYSTEM_NAME>*[**\\**_<instance_name>_]  
  
 where *<SYSTEM_NAME>* is the name of the system on which the server instance resides. For a non-default server instance, the instance name is also displayed: _<SYSTEM_NAME>_**\\**_<instance_name>_.  
  
 *<MIRROR_SERVER>*  
 The name of the partner that is currently the mirror server instance. The format is the same as for the principal server.  
  
## Detail Pane  
 The appearance of the monitor depends on whether a database is selected. When you open the monitor, the detail pane displays a **Register mirrored database** link. Click this to register a database. Registered databases are listed below the **Database Mirroring Monitor** node in the navigation tree. Database Mirroring Monitor always tries to connect to every server instance for which it has stored credentials.  
  
 When you select a database, its status is displayed on the **Status** tabbed page in the detail pane. The content of this page comes from both the principal and mirror server instances. The page is filled asynchronously as status is gathered through separate connections to the principal and mirror server instances. The status automatically refreshes at 30-second intervals.  
  
> [!NOTE]  
>  You cannot change the monitor's refresh rate, but you can refresh the status table from the **Database Mirroring History** dialog box.  
  
 A system administrator can view the current configuration of warnings for the database by selecting the **Warnings** tabbed page. From there, the administrator can launch the **Set Warning Thresholds** dialog box to enable and configure one or more warning thresholds.  
  
 In the banner above the tabs, the detail pane displays the last time the monitor refreshed the status information as, **Last refresh:**_\<date>\<time>_. Usually, the Database Mirroring Monitor retrieves status information from the principal and mirror server instances at different times. The older of these two refresh times is displayed.  
  
## Action Menu  
 The **Action** menu always contains the following commands:  
  
|Command|Description|  
|-------------|-----------------|  
|**Register Mirrored Database...**|Opens the **Register Mirrored Database** dialog box. Use this dialog box to register one or more mirrored databases on a given server instance by adding the database or databases to the Database Mirroring Monitor. When a database is added, Database Mirroring Monitor locally caches information about the database, its partners, and how to connect to the partners.|  
|**Manage Server Instance Connections...**|When you select this command, the **Manage Server Connections** dialog box opens. There, you can choose a server instance for which you want to specify credentials for the monitor to use when connecting to a given partner.<br /><br /> To edit the credentials for a partner, locate its entry in the **Server instances** grid, and click **Edit** on that row. The **Connect to Server** dialog box appears with the server instance name fixed and the credential controls initialized to the current cached value. Change the authentication information as necessary and click **Connect**. If the credentials have sufficient privileges, the **Connect Using** column is updated with the new credentials.|  
  
 If you select a database, the **Action** menu also contains the following commands.  
  
|Command|Description|  
|-------------|-----------------|  
|**Unregister This Database**|Removes the selected database from Database Mirroring Monitor.|  
|**Set Warning Thresholds...**|Opens the **Set Warning Thresholds** dialog box. There a system administrator can enable or disable warnings for the database on each of the partners and change the threshold of each warning. We recommend setting a threshold for a given warning on both partners to ensure that the warning persists if the database fails over. The appropriate threshold for each partner depends on the performance capabilities of that partner's system.<br /><br /> An event is written to the event log for a performance only if its value is at or above its threshold when the status table is being updated. If a peak value reaches the threshold momentarily between status updates that peak is missed.|  
  
 **To monitor database mirroring by using SQL Server Management Studio to**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md)  
  
  
