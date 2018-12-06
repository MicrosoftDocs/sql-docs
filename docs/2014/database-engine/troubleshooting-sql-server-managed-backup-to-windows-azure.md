---
title: "Troubleshooting SQL Server Managed  Backup to Windows Azure | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: a34d35b0-48eb-4ed1-9f19-ea14754650da
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Troubleshooting SQL Server Managed  Backup to Windows Azure
  This topic describes the tasks and tools you can use to troubleshoot errors that may occur during [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] operations.  
  
## Overview  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] has built in checks and troubleshooting, so in many cases internal failures are taken care of by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] process itself.  
  
 Example of one such case is a deletion of a backup file resulting in a break of the log chain affecting recoverability - [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will identify the break in log chain and schedule a backup to be taken immediately. However we recommend that you monitor the status and address any errors that require manual intervention.  
  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] logs events and errors using system stored procedures, system views and extended events. System views and stored procedures provide [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configuration information, status of backup scheduled backups, and also the errors captured by Extended Events. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] uses Extended Events to capture the errors to use for troubleshooting. In addition to logging events, SQL Server Smart Admin Policies provide a health status which is used by an email notification job to provide notification or errors and issues. For more information see [Monitor SQL Server Managed Backup to Windows Azure](../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] also uses the same logging that is used when manually backing up to Windows Azure storage (SQL Server Backup to URL). For more information on Backup to URL related issues, see the troubleshooting section in [SQL Server Backup to URL Best Practices and Troubleshooting](../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)  
  
### General Troubleshooting Steps  
  
1.  Enable Email Notification to start to receive emails for error and warnings.  
  
     Alternatively, you can also periodically run `smart_admin.fn_get_health_status` to check the aggregated errors and counts. For example, `number_of_invalid_credential_errors` is the number of times smart backup attempted a backup but got invalid credential error. `Number_of_backup_loops` and `number_of_retention_loops` are not errors; but they indicate the number of times backup thread and retention thread scanned the list of databases. Typically, when @begin_time and @end_time are not provided, the function is showing the information from last 30 minutes, then we should normally see non-zero values for these two columns. If they are zero then it implies system overloaded or even system not responding. For more information, see **Troubleshooting System Issues** section later in this topic.  
  
2.  Review the Extended Event logs to learn more details on the errors and other associated events.  
  
3.  Use the information in the logs to resolve the issue.  In case of a system issue or error, you may need to restart the service or SQL Server Agent.  
  
### Common Causes of Errors  
 Following is the list of common causes resulting in failures:  
  
1.  **Changes to SQL Credential:** If the name of the credential used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is changed or if it is deleted, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will not be able to take backups. The change should be applied to [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configuration settings.  
  
2.  **Changes to storage access key values:** If the storage key values are changed for the Windows Azure account, but SQL Credential is not updated with the new values, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will fail when authenticating to the storage, and fails to backup databases configured to use this account.  
  
3.  **Changes to Windows Azure Storage Account:** Deleting or renaming storage account without corresponding changes to the SQL Credential will cause [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] to fail and no backups will be taken. If you delete a storage account, ensure that the databases are reconfigured with valid storage account information. If a storage account is renamed or the key values are changed, ensure that these changes are reflected in the SQL Credential used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)].  
  
4.  **Changes to Database Properties:** Changes to recovery models or changing the name can cause backups to fail.  
  
5.  **Changes to Recovery Model:** If the recovery model of the database is changed to simple from full or bulk-logged, backups will stop, and the databases will be skipped by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. For more information, see [SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-interoperability-and-coexistence.md)  
  
### Most Common Error Messages and Solutions  
  
1.  **Errors when enabling or configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]:**  
  
     Error: " Failed to access the storage URL.... Provide a valid SQL Credential..." : You may see this and other similar errors referring to SQL Credentials.  In such cases, review the name of the SQL Credential you provided, and also the information stored in the SQL Credential - the storage account name, and the storage access key and make sure that they are current and valid.  
  
     Error: "... cannot configure the database....because it is a system database": You will see this error if you try to enable [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for a system database.  [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] does not support backups for system databases.  To configure backup for a system database use other SQL Server Backup technologies such as maintenance plans.  
  
     Error:" ... Provide a retention period...." : You may see errors regarding the retention period if you either have not specified a retention period for the database or instance when you are configuring these values for the first time. You may also see an error if you provide a value other than a number between 1 and 30. The allowed value for the retention period is a number between 1 and 30.  
  
2.  **Email Notification Errors:**  
  
     Error: "Database Mail is not enabled..." - You will see this error if you enable e-mail notifications, but Database Mail is not configured on the instance. You must configure Database Mail on the instance to be able to receive notification of the health status of [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. For information about how to enable database mail, see [Configure Database Mail](../relational-databases/database-mail/configure-database-mail.md). You must also enable SQL Server Agent to use Database Mail for notifications. For more information, see [Before You Begin](../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md#BeforeYouBegin).  
  
     Following is a list of error numbers you might see that are associated with email notifications:  
  
    -   ErrorNumber: 45209  
  
    -   ErrorNumber: 45210  
  
    -   ErrorNumber: 45211  
  
3.  **Connectivity Errors:**  
  
    -   **Errors Related to SQL Connectivity:** These errors happen when there are issues connecting to SQL Server instance. The extended events expose these type of errors through the admin channel. Following are the two extended events that you might see for errors related to this type of connectivity issues:  
  
         FileRetentionAdminXEvent with event_type = SqlError. For details of this error, look at the error_code, error_message and stack_trace of that event. The error_code is the SqlException's error number.  
  
         SmartBackupAdminXevent with the following messages/message prefixes:  
  
         *"An internal error occured while configuring SQL Server Managed Backup to Windows Azure default settings for instance. Error might be transient."*  
  
         *"Probably experiencing connectivity issues with SQL Server. Skipping database in the current iteration."*  
  
         *"Querying log usage info failed. The failure might be transient. Skipping database in the current iteration."*  
  
         *"SQL exception encountered while loading SSMBackup2WA agent metadata. The failure might be transient. Operation will be retried."*  
  
         *"SSMBackup2WA encountered SQL exception while ... "*  
  
    -   **Errors Connecting to the storage account:**  
  
         Storage exceptions are reported in FileRetentionAdminXEvent with event_type = XstoreError. For details of the error, look at the error_message and stack_trace of that event.  
  
         Since SQL Server Managed Backup uses the underlying Backup to URL technology, the errors related to storage connectivity apply to both the features. For more information on troubleshooting steps, see **troubleshooting section** of the [SQL Server Backup to URL Best Practices and Troubleshooting](../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md) article.  
  
### Troubleshooting System Issues  
 Following are some scenarios when there is an issue with the system (SQL Server, SQL Server Agent) and its effects on [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]:  
  
-   **Sqlservr.exe stops responding or stops working when [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is running:** If SQL Server stops working, SQL Agent will gracefully shut down, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] also stops and the events are logged in SQL Agent.out file.  
  
     If SQL Server stops responding, events are logged in the admin channel.  An example of the event log:  
  
     *Sql Error (engine not responding or get sqlException: SqlException:*   
     *error code, message and stacktrace will be displayed in an admin channel xevent, together with some extra information, such as :*   
    *"Probably experiencing connectivity issues with SQL Server. Skipping database in the current iteration"*  
  
-   **SQL Agent stops responding or stops working when [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is running:**  
  
     If SQL Agent stops working, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] also stops and events are logged in the admin channel. This is similar to the scenarios when SQL Server stops responding.  
  
     If SQL Agent stops responding, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will be unable to continue with backup operations, and events are logged in the admin channel. An example of the event log:  
  
     *Job hangs: see admin channel xevents*   
    *"A progress update hasn't been received from SQL Server in more than " + Constants.DBBackupInfoMsgMaxWaitTime  + " hours for database backup.   SSM Cloud Backup will continue to wait."*  
  
 If you have enabled email notification, you will receive a notification which includes **Number of Backup Loops** and **Number of Retention Loops**. If the value returned in the notification for one or both of these two columns is zero, it could be an indication that the system is not responding.  
  
> [!WARNING]  
>  The internal processes that generate the results for the report, assumes that the engine diagnostic logs are in the same location of SQL Agent error log, which by default is in the same folder as the error logs of the SQL Server instance. If the engine diagnostic logs are moved to a location other than SQL Agent error log location, the system is unable to find the smart backup diagnostic logs, and hence the report in the email notification may not be correct. For example, you might see a value of **0** in all the fields reported including the Number of Backup Loops and Number of Retention Loops. In this case where the diagnostic logs are moved to a different location, it may not mean that the system is not responding, but that the system is unable to find the logs. Ensure that the location of the diagnostic logs and SQL Agent error logs are at the same location first. To verify the current location of the diagnostic logs, you can use [sys.dm_os_server_diagnostics_log_configurations](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-server-diagnostics-log-configurations). The `path` column returns the current location of the engine diagnostic logs.  It should be in the same folder as the SQL Agent error logs. You can get SQL Agent error log path using the `dbo.sp_get_sqlagent_properties` stored procedure.  
  
 Check the extended event logs to see details of the errors. fix the errors, or restart SQL Server Agent to correct the situation.  
  
  
