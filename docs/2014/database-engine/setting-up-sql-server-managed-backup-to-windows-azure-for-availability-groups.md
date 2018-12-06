---
title: "Setting up SQL Server Managed Backup to Windows Azure for Availability Groups | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 0c4553cd-d8e4-4691-963a-4e414cc0f1ba
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Setting up SQL Server Managed Backup to Windows Azure for Availability Groups
  This topic is a tutorial on configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for databases participating in AlwaysOn Availability Groups.  
  
## Availability Group Configurations  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is supported for Availability Group databases, whether the replicas are all configured on-premises, or entirely on Windows Azure, or a Hybrid implementation between on-premises and on one or more Windows Azure virtual machines. However you might need to consider the following for one or more implementations:  
  
-   Log Backup Frequency: The frequency of log backup is both time and log growth. For example, the log backup is taken once every 2 hours unless the log space used within the two hour period is 5 MB or more. This applies to all implementations, on-premises, cloud, or a Hybrid.  
  
-   Network Bandwidth: This applies to implementations where the replicas are located in different physical locations, like in a hybrid cloud, or across different Windows Azure regions in a cloud only configuration. The network bandwidth can affect the latency of the secondaries and if the secondaries are set to synchronous replication, then this can cause log growth on the primary. If the secondaries are set to synchronous replication, the secondaries may not be able to keep up due to network latency, which can result in data loss in the event of a failover to the secondary replica.  
  
### Configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for Availability Databases.  
 **Permissions:**  
  
-   Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and `EXECUTE` permissions on **sp_delete_backuphistory**stored procedure.  
  
-   Requires **SELECT** permissions on the **smart_admin.fn_get_current_xevent_settings**function.  
  
-   Requires `EXECUTE` permissions on the **smart_admin.sp_get_backup_diagnostics** stored procedure. In addition, it requires `VIEW SERVER STATE` permissions as it internally calls other system objects that require this permission.  
  
-   Requires `EXECUTE` permissions on the `smart_admin.sp_set_instance_backup` and `smart_admin.sp_backup_master_switch` stored procedures.  
  
 The following are the basic steps to setting up an AlwaysOn Availability Group with [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. A detailed step-by step tutorial is described later in this topic.  
  
1.  Once you have created Availability Group, configure the preferred backup replica. This setting for the Availability Group is also used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] to determine which replica to use for backup. For step by step instructions about how to set up the backup preference, see [Configure Backup on Availability Replicas &#40;SQL Server&#41;](availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).  If you are creating a new AlwaysOn Availability Group, see [Getting Started with AlwaysOn Availability Groups &#40;SQL Server&#41;](availability-groups/windows/getting-started-with-always-on-availability-groups-sql-server.md).  
  
2.  Configure read only connection access to the secondary replicas. For step by step instructions about how to configure read only access, see [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
3.  Specify Backup replica. The preferred backup replica setting is used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] to determine which database to use for scheduling backups from..  To determine whether the current replica is the preferred backup replica, use the [sys.fn_hadr_backup_is_preferred_replica  &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql) function.  
  
4.  On each replica run [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configuration for the database using the **smart-admin.sp_set_db_backup** stored procedure.  
  
     **[!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] behavior after a failover:** [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will continue to work and maintain backup copies and recoverability after a failover event. No specific action is required after a failover.  
  
#### Considerations and Requirements:  
 Configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for databases participating in AlwaysOn Availability Group requires specific considerations and requirements. Following is a list of considerations and requirements:  
  
-   The [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configuration settings should be the same for all the databases on all the nodes of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] participating in the same Availability Group. You can achieve this either by setting  the same [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configurations for the primary and all the replicas at the database level, or by setting the same default [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] settings on all the nodes participating in the Availability Groups. We recommend setting [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] at the database because configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] at the database level lets you isolate the settings to the databases and changes to default settings affect all other databases on the instance.  
  
-   Specify Backup replica. The preferred backup replica setting is used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] to schedule the backups. To determine whether the current replica is the preferred backup replica, use the [sys.fn_hadr_backup_is_preferred_replica  &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql) function.  
  
-   If the secondary replica is configured as the preferred replica, it should be configured to have at least read only connection access. Availability groups that have no connection access to secondary databases are not supported.  For more information, see [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
-   If you are configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] after you configure the Availability Group, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will attempt to copy any existing backups based and copy them over to the storage container.  If [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is unable to find or access existing backups, it will schedule a full database backup. This is done specifically to optimize the backup operations for Availability Group databases.  
  
-   You may want to consider disabling the instance level settings if you are creating a new availability database, and you don't intend to apply the instance level settings to the database  
  
-   When using encryption, use the same certificate on all the replicas. This facilitates continued and uninterrupted backup operations in the event of a failover or restores to a different replica.  
  
#### Enable and Configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for an Availability Database  
 This tutorial describes the steps to enable and configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for a database (AGTestDB) on computers Node1 and Node2, followed by steps to enable monitoring the [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] health status.  
  
1.  **Create a Windows Azure storage account:** The backups are stored in the Windows Azure Blob storage service. You must first create a Windows Azure storage account, if you do not already have one. For more information, see [Creating a Windows Azure Storage Account](http://www.windowsazure.com/manage/services/storage/how-to-create-a-storage-account/). Make a note of the storage account name, access keys, and URL of the storage account. The storage account name and access key information is used to create a SQL Credential. The SQL Credential is used by [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] during backup operations to authenticate to the storage account.  
  
2.  **Create a SQL Credential:** Create a SQL Credential using the name of the storage account as the Identity and the storage access key as the password.  
  
3.  **Ensure SQL Server Agent service is Started and Running:** Start SQL Server Agent if it is not currently running. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] requires SQL Server Agent to be running on the instance to perform backup operations.  You may want to set SQL Agent to run automatically to make sure that backup operations can occur regularly.  
  
4.  **Determine the retention period:** Determine the retention period that you want for the backup files. The retention period is specified in days and can range from 1 to 30. The retention period determines the recoverability time frame for the database.  
  
5.  **Create a Certificate or Asymmetric key to use for encryption during back up:** Create the certificate on the first node Node1, and then export it to a file using [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-certificate-transact-sql).. On Node 2, create a certificate using the file exported from Node 1 . For more information on creating a certificate from a file, see the example in [CREATE CERTIFICATE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-certificate-transact-sql).  
  
6.  **Enable and configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for AGTestDB on Node1:** Start SQL Server Management Studio  and connect to the instance on Node1 where the availability database  is installed. From the query window run the following statement after you modify the values for the database name, storage URL, SQL Credential and retention period per your requirements:  
  
    ```  
    Use msdb;  
    GO  
    EXEC smart_admin.sp_set_db_backup   
                    @database_name='AGTestDB'   
                    ,@retention_days=30   
                    ,@credential_name='MyCredential'  
                    ,@encryption_algorithm ='AES_128'  
                    ,@encryptor_type= 'Certificate'  
                    ,@encryptor_name='MyBackupCert'  
                    ,@enable_backup=1;  
    GO  
  
    ```  
  
     For more information on creating a certificate for encryption, see the **Create a Backup Certificate** step in [Create an Encrypted Backup](../relational-databases/backup-restore/create-an-encrypted-backup.md).  
  
7.  **Enable and configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for AGTestDB on Node2:** Start SQL Server Management Studio and connect to the instance on Node2 where the availability database  is installed. From the query window run the following statement after you modify the values for the database name, storage URL, SQL Credential and retention period per your requirements:  
  
    ```  
    Use msdb;  
    GO  
    EXEC smart_admin.sp_set_db_backup   
                    @database_name='AGTestDB'   
                    ,@retention_days=30   
                    ,@credential_name='MyCredential'  
                    ,@encryption_algorithm ='AES_128'  
                    ,@encryptor_type= 'Certificate'  
                    ,@encryptor_name='MyBackupCert'  
                    ,@enable_backup=1;  
    GO  
  
    ```  
  
     [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is now enabled on the database you specified. It may take up to 15 minutes for the backup operations on the database to start to run. The backup will occur on the preferred backup replica.  
  
8.  **Review Extended Event Default Configuration:**  Review the Extended Event configuration by running the following transact-SQL statement on the replica that [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is using to schedule the backups from. This is usually the preferred backup replica setting for the Availability Group that the database belongs to.  
  
    ```  
    SELECT * FROM smart_admin.fn_get_current_xevent_settings()  
    ```  
  
     You should see that Admin, Operational  and Analytical channel events are enabled by default and cannot be disabled. This should be sufficient to monitor the events that require manual intervention.  You can enable the debug events, but these channels include informational and debug events that [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] uses to detect issues and solve them. For more information, see [Monitor SQL Server Managed Backup to Windows Azure](../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
9. **Enable and Configure Notification for Health Status:** [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] has a stored procedure that creates an agent job to send out e-mail notifications of errors or warnings that may require attention.  To receive such notifications, you must enable run the stored procedure which creates a SQL Server Agent Job. The following steps describe the process to enable and configure e-mail notifications:  
  
    1.  Setup Database Mail if it is not already enabled on the instance. For more information, see [Configure Database Mail](../relational-databases/database-mail/configure-database-mail.md).  
  
    2.  Configure SQL Server Agent Notification to use Database Mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
    3.  **Enable e-mail notifications to receive backup errors and warnings:** From the query window, run the following Transact-SQL statements:  
  
        ```  
        EXEC msdb.smart_admin.sp_set_parameter  
        @parameter_name = 'SSMBackup2WANotificationEmailIds',  
        @parameter_value = '<email>'  
  
        ```  
  
         For more information and a full sample script see [Monitor SQL Server Managed Backup to Windows Azure](../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
10. **View backup files in the Windows Azure Storage Account:** Connect to the storage account from SQL Server Management Studio or the Azure Management Portal. You will see a container for the instance of SQL Server that hosts the database you configured to use [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. You may also see a database and a log backup within 15 minutes of enabling [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for the database.  
  
11. **Monitor the Health Status:**  You can monitor through e-mail notifications you configured previously, or actively monitor the events logged. The following are some example Transact-SQL Statements used to view the events:  
  
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
    event varchar (512),  
    timestamp datetime  
    )  
  
    INSERT INTO @eventresult  
  
    EXEC smart_admin.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek  
  
    SELECT * from @eventresult  
    WHERE event_type LIKE '%admin%'  
  
    ```  
  
    ```  
    -- to enable debug events  
    Use msdb;  
    Go  
             EXEC smart_admin.sp_set_parameter 'FileRetentionDebugXevent', 'True'  
  
    ```  
  
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
  
 The steps described in this section are specifically for configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for the first time on the database. You can modify the existing configurations using the same system stored procedure **smart_admin.sp_set_db_backup** and provide the new values. For more information, see [SQL Server Managed Backup to Windows Azure - Retention and Storage Settings](../../2014/database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md).  
  
### Considerations when Removing a Database from AlwaysOn Availability Group Configuration  
 If a database is removed from the AlwaysOn Availability Group configuration and is now a stand-alone database, we recommend doing backup using [smart_admin.sp_backup_on_demand &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-on-demand-transact-sql). When you create a database backup  this way, it establishes  a new backup chain and the file will be placed in the instance specific container as opposed to Availability container where the backups were stored when the database was part of Availability Group.  
  
> [!WARNING]  
>  The recoverability of the database in this scenario from backups previous to the change in the Availability Group status is not guaranteed.  
  
  
