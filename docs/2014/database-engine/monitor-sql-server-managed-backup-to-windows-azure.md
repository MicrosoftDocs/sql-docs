---
title: "Monitor SQL Server Managed Backup to Windows Azure | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: cfb9e431-7d4c-457c-b090-6f2528b2f315
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Monitor SQL Server Managed Backup to Windows Azure
  [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] has built-in measures to identify problems and errors during backup processes and remedy with corrective action when possible.  However there are certain situations where user intervention is required. This topic describes the tools that you can use to determine the overall health status of backups, and identify any errors that need to be addressed.  
  
## Overview of [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] Built-in Debugging  
 The [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] periodically reviews scheduled backups and attempts to reschedule any failed backups. It polls the storage account periodically to identify breaks in log chains affecting recoverability of the database, and schedules new backups accordingly. It also takes into account Windows Azure throttling policies, and has mechanisms in place to manage multiple database backups. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] uses extended events to track all activity. The Extended Event channels used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] agent include, Admin, Operational, Analytical, and Debug. Events that fall under the Admin category usually are related to errors and require user intervention and are enabled by default. Analytical events are also turned on by default, but usually are not related to errors that require user intervention. Operation events are typically informational. For example, operational events include scheduling a backup, a successful completion of backup, etc. The Debug is the most verbose and is used internally by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] to determine issues and correct them if required.  
  
### Configure Monitoring Parameters for [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]  
 The **smart_admin.sp_set_parameter** system stored procedure allows you to specify monitoring settings. The following sections walks through the process of enabling Extended Events,  and enabling email notification for errors and warnings.  
  
 The **smart_admin.fn_get_parameter** function can be used to get the current setting for a specific parameter or all configured parameters. If the parameters have never been configured, the function does not return any values.  
  
1.  Connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and then click **Execute**. This will return the current configuration for Extended Events, and e-mail notifications.  
  
```  
Use msdb  
Go  
SELECT * FROM smart_admin.fn_get_parameter (NULL)  
GO  
```  
  
 For more information, see [smart_admin.fn_get_parameter &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-get-parameter-transact-sql)  
  
### Extended Events for Monitoring  
 By default, Admin, Operational and Analytical events are turned on. Admin events are most critical, useful when identifying errors that require manual intervention to solve the problem. You may want to turn on the operational and debug events but consider that these events are verbose and may require some filtering. The following procedures describe how to monitor events logged through Extended Events.  
  
> [!IMPORTANT]  
>  Unlike Extended Events for SQL Engine, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] does not support Extended Event session concepts. System stored procedures and functions are the tools used to interact with extended events. You can also view the extended event log from the log directory.  
  
##### To Configure and View Extended Events  
  
1.  To view available Extended Event channels and their current status by running the following query:  
  
    ```  
    SELECT * FROM smart_admin.fn_get_current_xevent_settings()  
    ```  
  
     The output from this query will display the event_name, whether it is configurable or not, and whether it is currently enabled.  For more information, see [smart_admin.fn_get_current_xevent_settings &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-get-current-xevent-settings-transact-sql).  
  
2.  To enable debug events, run the following query:  
  
    ```  
    --  to enable debug events  
    Use msdb;  
    Go  
             EXEC smart_admin.sp_set_parameter 'FileRetentionDebugXevent', 'True'  
  
    ```  
  
     For more information about the stored procedure, see [smart_admin.sp_set_parameter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-set-parameter-transact-sql).  
  
3.  To view the logged events run the following query:  
  
    ```  
    --  View all events in the current week  
    Use msdb;  
    Go  
    DECLARE @startofweek datetime  
    DECLARE @endofweek datetime  
    SET @startofweek = DATEADD(Day, 1-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)   
    SET @endofweek = DATEADD(Day, 7-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)  
  
    EXEC smart_admin.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek;  
  
    ```  
  
    ```  
    --  view all admin events  
    Use msdb;  
    Go  
    DECLARE @startofweek datetime  
    DECLARE @endofweek datetime  
    SET @startofweek = DATEADD(Day, 1-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)   
    SET @endofweek = DATEADD(Day, 7-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)  
  
    DECLARE @eventresult TABLE  
    (event_type nvarchar(512),  
    event nvarchar (512),  
    timestamp datetime  
    )  
  
    INSERT INTO @eventresult  
  
    EXEC smart_admin.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek  
  
    SELECT * from @eventresult  
    WHERE event_type LIKE '%admin%'  
  
    ```  
  
### Aggregated Error Counts/Health Status  
 The **smart_admin.fn_get_health_status** function returns a table of aggregated error counts for each category that can be used to monitor the health status of [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. This same function is also used by the system configured e-mail notification mechanism described later in this topic.   
These aggregated counts can be used to monitor system health. For example, if the number_ of_retention_loops column is 0 in 30 minutes, it is possible that retention management is taking long time or even not working correctly. Non-zero error columns may indicate problems and Extended Events logs should be checked to discover the problem. Alternatively, call **smart_admin.sp_get_backup_diagnostics** stored procedure to find the details of the error.  
  
### Using Agent Notification for Assessing Backup Status and Health  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] includes a notification mechanism that is based on SQL Server policy based management policies.  
  
 **Prerequisites:**  
  
-   Database Mail is required to use this functionality. For more information, about how to enable DB Mail for the instance of SQL Server, see [Configure Database Mail](../relational-databases/database-mail/configure-database-mail.md).  
  
-   SQL Server Agent Alert System properties should be set to use Database Mail.  
  
 **Notification Architecture:**  
  
-   **Policy Based Management:** Two policies are set to monitor backup health: **Smart Admin System Health Policy**, and the **Smart Admin User Action Health Policy**. The Smart Admin System Health Policy evaluates critical errors like lack of or invalid SQL Credentials, connectivity errors and reports the health of the system. These typically require a manual action to correct the underlying issue. The Smart Admin User Action Health Policy evaluates warnings such as corrupted backups, and such.  These may not require any action but just a warning of an event. It is expected that such issues will be automatically taken care of by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] agent.  
  
-   **SQL Server Agent** Job: The notification is performed using a SQL Server Agent job that has three job steps. In the first job step it detects to see whether [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is configured for a database or instance. If it finds [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] enabled and configured, it executes the second step: executes a PowerShell cmdlet that assess the health status by evaluating the SQL Server policy based management policies. If it finds either an error or warning, it fails which then triggers the third step: The third step sends out an e-mail notification with the error/warning report.  However this SQL Server Agent job is not enabled by default. To enable the e-mail notification job, use the **smart_admin.sp_set_backup_parameter** system stored procedure.  The following procedure describes the steps in more detail:  
  
##### Enabling Email Notification  
  
1.  If Database Mail is not already configured, use the steps described in [Configure Database Mail](../relational-databases/database-mail/configure-database-mail.md).  
  
2.  Set database as the Mail System for SQL Server Alert System: Right Click on **SQL Server Agent**, select **Alert System**, check the **Enable mail profile** box, Select **Database Mail** as the **Mail System**, and select a previously created Mail profile.  
  
3.  Run the following query in a query window and provide the e-mail address where you want the notification to be sent to:  
  
    ```  
    Use msdb  
    Go  
    EXEC smart_admin.sp_set_parameter @parameter_name = 'SSMBackup2WANotificationEmailIds', @parameter_value = '<email address>'  
  
    ```  
  
     This creates a SQL Server Agent job that is used to gather health status and send notifications when there is an error or an issue with backups.  
  
 Following is a sample script  to enable DB Mail and set up the e-mail notification through SQL Server Agent Job  
  
```  
-- Prereq: Make sure that SQL Server service runs in a service account that has  
--  access to SMTP Server   
-- set SQL Server service account as domain account   
  
EXEC sp_configure 'show advanced', 1  
RECONFIGURE  
GO  
  
-- Enable DBMail  
EXEC sp_configure 'Database Mail XPs',1  
GO  
RECONFIGURE  
GO  
  
-- Configure DBMail  
DECLARE @emailid NVARCHAR(255)  
-- Note: change this email to your own email id  
SET @emailid = N'emailalias@mycompany.com'  
  
DECLARE @smtpserver NVARCHAR(255)  
SET @smtpserver = N'smtphost.domainname.com'  
  
DECLARE @mailprofile NVARCHAR(255)  
SET @mailprofile = N'my_profile'  
  
EXEC msdb.dbo.sysmail_add_account_sp @account_name=N'myaccount',  
                @email_address = @emailid,  
                @replyto_address = @emailid,  
                @mailserver_name = @smtpserver,  
                @use_default_credentials = 1  
  
EXEC msdb.dbo.sysmail_add_profile_sp @profile_name=@mailprofile  
EXEC msdb.dbo.sysmail_add_profileaccount_sp @profile_name=@mailprofile, @account_name=N'myaccount', @sequence_number=1  
  
-- Set SQL Agent to use DBMail profile  
EXEC msdb.dbo.sp_set_sqlagent_properties @databasemail_profile = @mailprofile  
  
-- Configure Notifications   
EXEC msdb.smart_admin.sp_set_parameter  
@parameter_name = 'SSMBackup2WANotificationEmailIds',  
@parameter_value = @emailid  
  
-- To test is you are receiving notifications  
-- delete few backup files from your storage container, Wait for 15 minutes & see if you get any email notification  
  
```  
  
### Using PowerShell to Setup Custom Health Monitoring  
 The **Test-SqlSmartAdmin** cmdlet can be used to create custom health monitoring. For example, the notification option described in the previous section can be configured at the instance level.  If you have several instances of SQL Server configured to use [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)], the PowerShell cmdlet can be used to create scripts in gather the status and health of backups for all the instances.  
  
 The **Test-SqlSmartAdmin** cmdlet assesses the errors and warnings returned by the SQL Server Policy Based Management policies and reports out a rolled up status.  By default this cmdlet uses the system policies. To include any custom policy use the `-AllowUserPolicies` parameter.  
  
 Following is a sample PowerShell script that returns a report of errors and warnings based on the system policies and any user policies created:  
  
```  
$policyResults = get-sqlsmartadmin | test-sqlsmartadmin -AllowUserPolicies  
$policyResults.PolicyEvaluationDetails | select Name, Category, Expression, Result, Exception | fl  
  
```  
  
 The following script returns a detailed report of the errors and warnings for the default instance:  
  
```  
PS C:\>PS SQLSERVER:\SQL\COMPUTER\DEFAULT> (get-sqlsmartadmin ).EnumHealthStatus()  
```  
  
### Objects in MSDB database  
 There are objects that are installed to implement the functionality. These objects are reserved for internal use. However, there is one system table that can be useful in monitoring the backup status: smart_backup_files. Most of the Information   stored in this table relevant to monitoring like the type of backup, database name, first and last lsn, backup expiry dates are exposed through the system function [smart_admin.fn_available_backups &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/managed-backup-fn-available-backups-transact-sql). However the status column in the smart_backup_files table which indicates the status of the backup file is not available using the function. Following is a sample query you can use to retrieve the some information including the status from the system table:  
  
```  
USE msdb  
GO  
SELECT  
 database_name AS [Database Name]  
,backup_path AS [Backup Destination and File]  
,[Backup Type] =  
CASE backup_type  
WHEN 1 THEN 'FULL'  
WHEN 2 THEN 'LOG'  
END  
,[Backup Status] =  
CASE [status]  
WHEN 'A' THEN 'Available'  
WHEN 'B' THEN 'Copy In Progress'  
WHEN 'C' THEN 'Corrupted'  
WHEN 'D' THEN 'Deleted'  
WHEN 'F' THEN 'Copy Failed'  
WHEN 'U' THEN 'Unknown'  
END  
,first_lsn AS [First LSN]  
,last_lsn AS [Last LSN]  
,backup_start_date AS [Backup Start Time]  
,backup_finish_date AS [Backup Completion Time]  
,expiration_date AS [Backup Expiry Date/Time]  
FROM  
smart_backup_files;  
  
```  
  
 Following is a detailed explanation of the different status returned:  
  
-   **Available - A:** This is a normal backup file. The backup has been completed, and also verified that it is available in the Windows Azure storage.  
  
-   **Copy in Progress -B:** This status is specifically for Availability Group databases. If [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] detects a break in the backup log chain, it will first attempt identify the  backup that might have caused the break in backup chain. On finding the backup file it attempts to copy the file to Windows Azure storage. When the copying process is in progress it will display this status.  
  
-   **Copy Failed - F:** Similar to Copy In Progress, this is specific t Availability Group databases. If the copy process fails, the status is marked as F.  
  
-   **Corrupted - C:** If [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is unable to verify the backup file in the storage by performing a RESTORE HEADER_ONLY command even after multiple attempts, it marks this file as corrupted. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will schedule a backup to ensure that the corrupted file does not result in a break of the backup chain.  
  
-   **Deleted - D:** The corresponding file cannot be found in the Windows Azure storage. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will schedule a backup if the deleted file results in a break in the backup chain.  
  
-   **Unknown - U:** This status indicated that [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] has not yet been able to verify file existence and its properties in the Windows Azure storage. The next time the process runs, which is approximately every 15 minutes, this status will be updated.  
  
  
