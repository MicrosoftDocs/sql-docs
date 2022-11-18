---
description: "View SQL Server Agent Error Log"
title: View SQL Server Agent Error Log
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], SQL Server Agent"
  - "viewing SQL Server Agent error logs"
  - "displaying SQL Server Agent error logs"
  - "SQL Server Agent, errors"
  - "errors [SQL Server Agent]"
ms.assetid: de920425-fa44-469f-b83d-49e3f97e97f4
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# View SQL Server Agent Error Log

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to view the  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio.  
  
Log File Viewer displays log information from many different components. When Log File Viewer is open, use the **Select logs** pane to select the logs you want to display. Each log displays columns appropriate to that kind of log. The logs that are available depend on how Log File Viewer is opened.  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node if you have permission to use it.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)  
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)  
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, see [Selecting an Account for SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To view the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log that you want to view.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Error Logs** folder.  
  
4.  Right-click the error log you want to view and select **View Agent Log**.  
  
    The following options are available in the **Log File Viewer -**_server_name_ dialog box:  
  
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
    This information panel displays a summary of the log file filtering. If the file is not filtered, you will see the following text, **No filter applied**. If a filter is applied to the log, you will see the following text, **Filter log entries where:** \<filter criteria\>.  
  
    **Selected row details**  
    Select a row to display additional details about the selected event row at the bottom of the page. The columns can be reordered by dragging them to new locations in the grid. The columns can be resized by dragging the column separator bars in the grid header to the left or right. Double-click the column separator bars in the grid header to automatically size the column to the content width.  
  
    **Instance**  
    The name of the instance on which the event occurred. This is displayed as *computer name*\\*instance name*.  
  
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
  
5.  When finished, click **Close**.  
