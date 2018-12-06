---
title: "Setting up SQL Server Managed Backup to Windows Azure | Microsoft Docs"
ms.custom: ""
ms.date: "08/04/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 68ebb53e-d5ad-4622-af68-1e150b94516e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Setting up SQL Server Managed Backup to Windows Azure
  This topic includes two tutorials:  
  
 Set up [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at the database level, enable email notification, and monitor backup activity.  
  
 Setting up  [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at the instance level, enable email notification, and monitor backup activity.  
  
 For a tutorial on setting up [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for Availability Groups, see [Setting up SQL Server Managed Backup to Microsoft Azure for Availability Groups](../../database-engine/setting-up-sql-server-managed-backup-to-windows-azure-for-availability-groups.md).  
  
## Setting Up [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]  
  
### Enable and Configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a Database  
 This tutorial describes the steps necessary to enable and configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a database (TestDB), followed by steps to enable monitoring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] health status.  
  
 **Permissions:**  
  
-   Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and `EXECUTE` permissions on **sp_delete_backuphistory**stored procedure.  
  
-   Requires **SELECT** permissions on the **smart_admin.fn_get_current_xevent_settings**function.  
  
-   Requires `EXECUTE` permissions on the **smart_admin.sp_get_backup_diagnostics** stored procedure. In addition, it requires `VIEW SERVER STATE` permissions as it internally calls other system objects that require this permission.  
  
-   Requires `EXECUTE` permissions on the `smart_admin.sp_set_instance_backup` and `smart_admin.sp_backup_master_switch` stored procedures.  


1.  **Create a Microsoft Azure storage account:** The backups are stored in the Microsoft Azure storage service. You must first create a Microsoft Azure storage account, if you do not already have an account.
    - SQL Server 2014 uses page blobs, which are different than block and append blobs. Therefore you must create a general purpose account, and not a blob account. For more information, see [About Azure storage accounts](http://azure.microsoft.com/documentation/articles/storage-create-storage-account/).
    - Make a note of the storage account name, and the access keys. The storage account name and access key information is used to create a SQL Credential. The SQL Credential is used to authenticate to the storage account.  
 
2.  **Create a SQL Credential:** Create a SQL Credential using the name of the storage account as the Identity and the storage access key as the password.  
  
3.  **Ensure SQL Server Agent service is Started and Running:**  Start SQL Server Agent if it is not currently running.  [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] requires SQL Server Agent to be running on the instance to perform backup operations.  You may want to set SQL Server Agent to run automatically to make sure that backup operations can occur regularly.  
  
4.  **Determine the retention period:** Determine the retention period for the backup files. The retention period is specified in days and can range from 1 to 30.  
  
5.  **Enable and configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] :** Start SQL Server Management Studio and connect to the instance where the database is installed. From the query window run the following statement after you modify the values for the database name, SQL Credential, retention period, and encryption options per your requirements:  
  
     For more information on creating a certificate for encryption, see the **Create a Backup Certificate** step in [Create an Encrypted Backup](create-an-encrypted-backup.md).  
  
    ```  
    Use msdb;  
    GO  
    EXEC smart_admin.sp_set_db_backup   
                    @database_name='TestDB'   
                    ,@retention_days=30   
                    ,@credential_name='MyCredential'  
                    ,@encryption_algorithm ='AES_128'  
                    ,@encryptor_type= 'Certificate'  
                    ,@encryptor_name='MyBackupCert'  
                    ,@enable_backup=1;  
    GO  
  
    ```  
  
     [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is now enabled on the database you specified. It may take up to 15 minutes for the backup operations on the database to start to run.  
  
6.  **Review Extended Event Default Configuration:** Review the Extended Event settings by running the following transact-SQL statement.  
  
    ```  
    SELECT * FROM smart_admin.fn_get_current_xevent_settings()  
    ```  
  
     You should see that Admin, Operational, and Analytical channel events are enabled by default and cannot be disabled. This should be sufficient to monitor the events that require manual intervention.  You can enable debug events, but the debug channels include informational and debug events that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] uses to detect issues and solve them. For more information, see [Monitor SQL Server Managed Backup to Microsoft Azure](sql-server-managed-backup-to-microsoft-azure.md).  
  
7.  **Enable and Configure Notification for Health Status:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has a stored procedure that creates an agent job to send out e-mail notifications of errors or warnings that may require attention. The following steps describe the process to enable and configure e-mail notifications:  
  
    1.  Setup Database Mail if it is not already enabled on the instance. For more information, see [Configure Database Mail](../database-mail/configure-database-mail.md).  
  
    2.  Configure SQL Server Agent Notification to use Database Mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
    3.  **Enable e-mail notifications to receive backup errors and warnings:** From the query window, run the following Transact-SQL statements:  
  
        ```  
        EXEC msdb.smart_admin.sp_set_parameter  
        @parameter_name = 'SSMBackup2WANotificationEmailIds',  
        @parameter_value = '<email1;email2>'  
  
        ```  
  
         For more information, and a full sample script see [Monitor SQL Server Managed Backup to Microsoft Azure](sql-server-managed-backup-to-microsoft-azure.md).  
  
8.  **View backup files in the Microsoft Azure Storage Account:** Connect to the storage account from SQL Server Management Studio or the Azure Management Portal. You will see a container for the instance of SQL Server that hosts the database you configured to use [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. You may also see a database and a log backup within 15 minutes of enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the database.  
  
9. **Monitor the Health Status:**  You can monitor through e-mail notifications you configured previously, or actively monitor the events logged. The following are some example Transact-SQL Statements used to view the events:  
  
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
  
 The steps described in this section are specifically for configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time on the database. You can modify the existing configurations using the same system stored procedure **smart_admin.sp_set_db_backup** and provide the new values. For more information, see [SQL Server Managed Backup to Microsoft Azure - Retention and Storage Settings](../../database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md).  
  
### Enable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the Instance with Default Settings  
 This tutorial describes the steps to enable and configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the instance, 'MyInstance',\\. It includes steps to enable monitoring the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] health status.  
  
 **Permissions:**  
  
-   Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and `EXECUTE` permissions on **sp_delete_backuphistory**stored procedure.  
  
-   Requires **SELECT** permissions on the **smart_admin.fn_get_current_xevent_settings**function.  
  
-   Requires `EXECUTE` permissions on the **smart_admin.sp_get_backup_diagnostics** stored procedure. In addition, it requires `VIEW SERVER STATE` permissions as it internally calls other system objects that require this permission.  


1.  **Create a Microsoft Azure storage account:** The backups are stored in the Microsoft Azure storage service. You must first create a Microsoft Azure storage account, if you do not already have an account.
    - SQL Server 2014 uses page blobs, which are different than block and append blobs. Therefore you must create a general purpose account, and not a blob account. For more information, see [About Azure storage accounts](http://azure.microsoft.com/documentation/articles/storage-create-storage-account/).
    - Make a note of the storage account name, and the access keys. The storage account name and access key information is used to create a SQL Credential. The SQL Credential is used to authenticate to the storage account.  
  
2.  **Create a SQL Credential:** Create a SQL Credential using the name of the storage account as the Identity and the storage access key as the password.  
  
3.  **Ensure SQL Server Agent service is Started and Running:** Start SQL Server Agent if it is not currently running. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] requires SQL Server Agent to be running on the instance to perform backup operations.  You may want to set SQL Server Agent to run automatically to make sure that backup operations can occur regularly.  
  
4.  **Determine the retention period:** Determine the retention period for the backup files. The retention period is specified in days and can range from 1 to 30. Once [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled at the instance level with the defaults all new databases created thereafter will inherit the settings. Only databases that are set to full or bulk-logged recovery models are supported and will be configured automatically. You may disable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for a specific database at any time if you  do not want [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configured. You can also change the configuration for a specific database by configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] at the database level.  
  
5.  **Enable and configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] :** Start SQL Server Management Studio and connect to the instance of SQL Server. From the query window run the following statement after you modify the values for the database name, SQL Credential, retention period, and the encryption options per your requirements:  
  
     For more information on creating a certificate for encryption, see the **Create a Backup Certificate** step in [Create an Encrypted Backup](create-an-encrypted-backup.md).  
  
    ```  
    Use msdb;  
    Go  
       EXEC smart_admin.sp_set_instance_backup  
                     @enable_backup=1  
                    ,@retention_days=30   
                    ,@credential_name='sqlbackuptoURL'  
                    ,@encryption_algorithm ='AES_128'  
                    ,@encryptor_type= 'Certificate'  
                    ,@encryptor_name='MyBackupCert';  
    GO  
  
    ```  
  
     [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is now enabled on the instance.  
  
6.  Verify the configuration settings by running the following Transact-SQL statement:  
  
    ```  
    Use msdb;  
    GO  
    SELECT * FROM smart_admin.fn_backup_instance_config ();  
  
    ```  
  
7.  Create a new database on the instance. Run the following Transact-SQL statement to view the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration settings for the database:  
  
    ```  
    Use msdb  
    GO  
    SELECT * FROM smart_admin.fn_backup_db_config('NewDB')  
    ```  
  
     It may take up to 15 minutes for the settings to show and backup operations on the database to start to run.  
  
8.  **Enable and Configure Notification for Health Status:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has a stored procedure that creates an agent job to send out e-mail notifications of errors or warnings that may require attention.  To receive such notifications, you must enable run the stored procedure which creates a SQL Server Agent Job. The following steps describe the process to enable and configure e-mail notifications:  
  
    1.  Setup Database Mail if it is not already enabled on the instance. For more information, see [Configure Database Mail](../database-mail/configure-database-mail.md).  
  
    2.  Configure SQL Server Agent Notification to use Database Mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
    3.  **Enable e-mail notifications to receive backup errors and warnings:** From the query window, run the following Transact-SQL statements:  
  
        ```  
        EXEC msdb.smart_admin.sp_set_parameter  
        @parameter_name = 'SSMBackup2WANotificationEmailIds',  
        @parameter_value = '<email address>'  
  
        ```  
  
         For more information about how to monitor, and a full sample script see [Monitor SQL Server Managed Backup to Microsoft Azure](sql-server-managed-backup-to-microsoft-azure.md).  
  
9. **View backup files in the Microsoft Azure Storage Account:** Connect to the storage account from SQL Server Management Studio or the Azure Management Portal. You will see a container for the instance of SQL Server that hosts the database you configured to use [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. You may also see a database and a log backup within 15 minutes of creating a new database.  
  
10. **Monitor the Health Status:**  You can monitor through e-mail notifications you configured previously, or actively monitor the events logged. The following are some example Transact-SQL Statements used to view the events:  
  
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
  
    ```  
    --  to enable debug events  
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
  
 [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] default settings can be overridden for a specific database by configuring the settings specifically at the database level. You can also pause and resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] service temporarily. For more information, see [SQL Server Managed Backup to Microsoft Azure - Retention and Storage Settings](../../database-engine/sql-server-managed-backup-to-windows-azure-retention-and-storage-settings.md)  
  
  
