---
title: "Log File Viewer F1 Help | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
f1_keywords: 
  - "sql13.swb.configurelogs.errorlog.f1"
helpviewer_keywords: 
  - "Log File Viewer"
ms.assetid: 2243845c-4880-4aa0-9ee8-0a97a128996b
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Log File Viewer F1 Help
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Log File Viewer displays log information from many different components. When Log File Viewer is open, use the **Select logs** pane to select the logs you want to display. Each log displays columns appropriate to that kind of log.  
  
 The logs that are available depend on how Log File Viewer is opened. For more information, see [Open Log File Viewer](../../relational-databases/logs/open-log-file-viewer.md).  
  
 The number of rows that are displayed for audit logs can be configured on the **SQL Server Object Explorer/Commands** page of the **Tools/Options** dialog box. For descriptions of the columns that are displayed for audit logs, see [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md).  
  
## Options  
 **Load Log**  
 Open a dialog box where you can specify a log file to load.  
  
 **Export**  
 Open a dialog box that lets you export the information that is shown in the **Log file summary** grid to a text file.  
  
 **Refresh**  
 Refresh the view of the selected logs. The **Refresh** button rereads the selected logs from the target server while applying any filter settings.  
  
 **Filter**  
 Open a dialog box that lets you specify settings that are used to filter the log file, such as **Connection**, **Date**, or other **General** filter criteria.  
  
 **Search**  
 Search the log file for specific text. Searching with wildcard characters is not supported.  
  
 **Stop**  
 Stops loading the log file entries. For example, you can use this option if a remote or offline log file takes a long time to load, and you only want to view the most recent entries.  
  
 **Log file summary**  
 This information panel displays a summary of the log file filtering. If the file is not filtered, you will see the following text, **No filter applied**. If a filter is applied to the log, you will see the following text, **Filter log entries where:** \<filter criteria>.  
  
 **Selected row details**  
 Select a row to display additional details about the selected event row at the bottom of the page. The columns can be reordered by dragging them to new locations in the grid. The columns can be resized by dragging the column separator bars in the grid header to the left or right. Double-click the column separator bars in the grid header to automatically size the column to the content width.  
  
 **Instance**  
 The name of the instance on which the event occurred. This is displayed as *computer name*\\*instance name*.  
  
## Frequently Displayed Columns  
 **Date**  
 Displays the date of the event.  
  
 **Source**  
 Displays the source feature from which the event is created, such as the name of the service (MSSQLSERVER, for example). This does not appear for all log types.  
  
 **Message**  
 Displays any messages associated with the event.  
  
 **Log Type**  
 Displays the type of log to which the event belongs. All selected logs appear in the log file summary window.  
  
 **Log Source**  
 Displays a description of the source log in which the event is captured.  
  
## Permissions  
 To access log files for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are online, this requires membership in the securityadmin fixed server role.  
  
 To access log files for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are offline, you must have read access to both the **Root\Microsoft\SqlServer\ComputerManagement10** WMI namespace, and to the folder where the log files are stored. For more information, see the Security section of the topic [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md).  
  
## See Also  
 [Log File Viewer](../../relational-databases/logs/log-file-viewer.md)   
 [Open Log File Viewer](../../relational-databases/logs/open-log-file-viewer.md)   
 [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md)  
  
  
