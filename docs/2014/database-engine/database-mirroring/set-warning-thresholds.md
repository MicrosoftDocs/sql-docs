---
title: "Set Warning Thresholds | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dbmmonitor.setwarningthreshold.f1"
ms.assetid: 17f93147-e7d9-4092-b4c2-c11b38051171
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set Warning Thresholds
  Use this dialog box to enable and configure one or more warning thresholds for the database selected in the navigation tree of the **Database Mirroring Monitor** dialog box.  
  
 The dialog box tries to connect to both server instances. These connections are established asynchronously. The dialog shows the connection status of each partner. If the partner is not connected, you can click **Connect**.  
  
 **To use SQL Server Management Studio to monitor database mirroring**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## Options  
 *Server instance and its connection status*  
 Name of a partner server instance in the form *SYSTEM***\\***INSTANCE_NAME*. For a default server instance, only the system name is displayed.  
  
 This field also indicates whether the monitor is currently connected to this server instance. The possible connection statuses are:  
  
-   **Not connected to**  *server_instance_name*  
  
-   **Trying to connect to**  *server_instance_name*  
  
-   **Connected to**  *server_instance_name*  
  
    > [!NOTE]  
    >  If you do are not a member of the **sysadmin** fixed server role, this status is **Connected to** *server_instance_name* **(Limited permissions)**.  
  
 The name of each of the partner server instances is displayed in a separate *Server instance and its connection status* field. The top field lists the principal server when the monitor started running.  
  
 **Connect**/**Cancel**  
 A **Connect**/**Cancel** button is associated with each *Server instance and its connection status* fields. The state of the button depends on the connection status:  
  
-   If there is no connection to the server instance, the button text is **Connect**. Click to connect to the server instance.  
  
-   When a connection attempt is in progress, the button text is **Cancel**. Click to cancel the connection attempt.  
  
-   If the server is connected, the button text is **Connected**, and the button is dimmed.  
  
 **Thresholds**  
 The **Thresholds** grid displays the warnings settings for the two server instances.  
  
> [!NOTE]  
>  If a server instance is not connected, its columns are displayed with empty cells and a gray background. When the connection opens, the grid automatically displays the content from the instance.  
  
 The grid contains the following columns:  
  
 **Warnings**  
 Lists the supported warnings:  
  
|Warning|Description|  
|-------------|-----------------|  
|**Warn if the unsent log exceeds the threshold**|The threshold indicates the number of kilobytes (KB) of the unsent log in the send queue on the principal.|  
|**Warn if the unrestored log exceeds the threshold**|The threshold indicates the number of KB of the redo queue on the mirror server instance.|  
|**Warn if the age of the oldest unsent transaction exceeds the threshold**|The threshold indicates the number of minutes of transactions that have not yet been sent from the send queue to the mirror server instance. This value helps measure the potential for data loss in terms of time.|  
|**Warn if the mirror commit overhead exceeds the threshold**|The threshold indicates the number of milliseconds of delay per transaction (relevant only in high-safety mode). This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue.|  
  
 **Enabled at '** *\<server instance>* **'**  
 A blank check box indicates that the warning is currently disabled on the server instance. To enable a warning, click its check box.  
  
 **Threshold at '** *\<server instance>* **'**  
 When a warning is enabled, set the threshold on the left side of this column. An event occurs if the specified threshold has been reached when the status table is updated. If you disable a threshold after configuring a value, your value remains in this field and will be used if you re-enable the warning.  
  
 When a warning is not enabled, this field is inactive.  
  
 **OK**  
 Clicking **OK** closes this dialog box and displays the currently specified values of warning thresholds in the **Thresholds** grid on the **Warnings**tabbed page.  
  
## Remarks  
 A threshold is only applicable at one partner at a time, but we recommend that you set a threshold for a given event on both partners to ensure that the warning persists if the database fails over. The appropriate threshold for each partner depends on the performance capabilities of that partner's system.  
  
 An event is written to the event log for a performance only if its value is at or above its threshold when the status table is being updated. If a peak value reaches the threshold momentarily between status updates that peak is missed.  
  
## See Also  
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
  
