---
title: "Enable SQL Server Managed Backup to Microsoft Azure | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 68ebb53e-d5ad-4622-af68-1e150b94516e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Enable SQL Server Managed Backup to Microsoft Azure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to enable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] with default settings at both the database and instance level. It also describes how to enable email notifications and how to monitor backup activity.  
  
 This tutorial uses Azure PowerShell. Before starting the tutorial, [download and install Azure PowerShell](https://azure.microsoft.com/documentation/articles/powershell-install-configure/).  
  
> [!IMPORTANT]  
>  If you also want to enable advanced options or use a custom schedule, configure those settings first before enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. For more information, see [Configure Advanced Options for SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/configure-advanced-options-for-sql-server-managed-backup-to-microsoft-azure.md).  
  
## Enable and Configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] with Default Settings  
  
#### Create the Azure Blob Container  
  
1.  **Sign up for Azure:** If you already have an Azure subscription, go to the next step. Otherwise, you can get started with a [free trial](https://azure.microsoft.com/pricing/free-trial/) or explore [purchase options](https://azure.microsoft.com/pricing/purchase-options/).  
  
2.  **Create an Azure storage account:** If you already have a storage account, go to the next step. Otherwise, you can use the [Azure Management Portal](https://manage.windowsazure.com/) or Azure PowerShell to create the storage account. The following `New-AzureStorageAccount` command creates a storage account named `managedbackupstorage` in the East US region.  
  
    ```powershell  
    New-AzureStorageAccount -StorageAccountName "managedbackupstorage" -Location "EAST US"  
    ```  
  
     For more information about storage accounts, see [About Azure Storage Accounts](https://azure.microsoft.com/documentation/articles/storage-create-storage-account/).  
  
3.  **Create a blob container for the backup files:** You can create a blob container in the Azure Management Portal or with Azure PowerShell. The following `New-AzureStorageContainer` command creates a blob container named `backupcontainer` in the `managedbackupstorage` storage account.  
  
    ```powershell  
    $context = New-AzureStorageContext -StorageAccountName managedbackupstorage -StorageAccountKey (Get-AzureStorageKey -StorageAccountName managedbackupstorage).Primary  
    New-AzureStorageContainer -Name backupcontainer -Context $context  
    ```  
  
4.  **Generate a Shared Access Signature (SAS):** To access the container, you must create a SAS. This can be done in some tools, code, and Azure PowerShell. The following `New-AzureStorageContainerSASToken` command creates SAS token for the `backupcontainer` blob container that expires in one year.  
  
    ```powershell  
    $context = New-AzureStorageContext -StorageAccountName managedbackupstorage -StorageAccountKey (Get-AzureStorageKey -StorageAccountName managedbackupstorage).Primary   
    New-AzureStorageContainerSASToken -Name backupcontainer -Permission rwdl -ExpiryTime (Get-Date).AddYears(1) -FullUri -Context $context  
    ```  
    For Azure, use the following command:
       ```powershell
    Connect-AzAccount
    Set-AzContext -SubscriptionId "YOURSUBSCRIPTIONID"
    $StorageAccountKey = (Get-AzStorageAccountKey -ResourceGroupName YOURRESOURCEGROUPFORTHESTORAGE -Name managedbackupstorage)[0].Value
    $context = New-AzureStorageContext -StorageAccountName managedbackupstorage -StorageAccountKey $StorageAccountKey 
    New-AzureStorageContainerSASToken -Name backupcontainer -Permission rwdl -ExpiryTime (Get-Date).AddYears(1) -FullUri -Context $context
   ```  
  
     The output for this command will contain both the URL to the container and the SAS token. The following is an example:  
  
    ```  
    https://managedbackupstorage.blob.core.windows.net/backupcontainer?sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl  
    ```  
  
     In the previous example, separate the container URL from the SAS token at the question mark (do not include the question mark. For example, the previous output would result in the following two values.  
  
    |||  
    |-|-|  
    |**Container URL:**|https://managedbackupstorage.blob.core.windows.net/backupcontainer|  
    |**SAS token:**|sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl|  
  
     Record the container URL and SAS for use in creating a SQL CREDENTIAL. For more information about SAS, see [Shared Access Signatures, Part 1: Understanding the SAS Model](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-1/).  
  
#### Enable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]  
  
1.  **Create a SQL Credential for the SAS URL:** Use the SAS token to create a SQL Credential for the blob container URL. In SQL Server Management Studio, use the following Transact-SQL query to create the credential for your blob container URL based on the following example:  
  
    ```sql  
    CREATE CREDENTIAL [https://managedbackupstorage.blob.core.windows.net/backupcontainer]   
    WITH IDENTITY = 'Shared Access Signature',  
    SECRET = 'sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl'  
    ```  
  
2.  **Ensure SQL Server Agent service is Started and Running:** Start SQL Server Agent if it is not currently running.  [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] requires SQL Server Agent to be running on the instance to perform backup operations.  You may want to set SQL Server Agent to run automatically to make sure that backup operations can occur regularly.  
  
3.  **Determine the retention period:** Determine the retention period for the backup files. The retention period is specified in days and can range from 1 to 30.  
  
4.  **Enable and configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] :** Start SQL Server Management Studio and connect to the target SQL Server instance. From the query window run the following statement after you modify the values for the database name, container url, and retention period per your requirements:  
  
    > [!IMPORTANT]  
    >  To enable managed backup at the instance level, specify `NULL` for the `database_name` parameter.  
  
    ```sql  
    Use msdb;  
    GO  
    EXEC msdb.managed_backup.sp_backup_config_basic   
     @enable_backup = 1,   
     @database_name = 'yourdatabasename',  
     @container_url = 'https://managedbackupstorage.blob.core.windows.net/backupcontainer',   
     @retention_days = 30  
    GO  
    ```  
  
     [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is now enabled on the database you specified. It may take up to 15 minutes for the backup operations on the database to start to run.  
  
5.  **Review Extended Event Default Configuration:** Review the Extended Event settings by running the following transact-SQL statement.  
  
    ```  
    SELECT * FROM msdb.managed_backup.fn_get_current_xevent_settings()  
    ```  
  
     You should see that Admin, Operational, and Analytical channel events are enabled by default and cannot be disabled. This should be sufficient to monitor the events that require manual intervention.  You can enable debug events, but the debug channels include informational and debug events that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] uses to detect issues and solve them.  
  
6.  **Enable and Configure Notification for Health Status:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has a stored procedure that creates an agent job to send out e-mail notifications of errors or warnings that may require attention. The following steps describe the process to enable and configure e-mail notifications:  
  
    1.  Setup Database Mail if it is not already enabled on the instance. For more information, see [Configure Database Mail](../../relational-databases/database-mail/configure-database-mail.md).  
  
    2.  Configure SQL Server Agent Notification to use Database Mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
    3.  **Enable e-mail notifications to receive backup errors and warnings:** From the query window, run the following Transact-SQL statements:  
  
        ```  
        EXEC msdb.managed_backup.sp_set_parameter  
        @parameter_name = 'SSMBackup2WANotificationEmailIds',  
        @parameter_value = '<email1;email2>'  
  
        ```  
  
7.  **View backup files in the Microsoft Azure Storage Account:** Connect to the storage account from SQL Server Management Studio or the Azure Management Portal. You will see any backup files in the container you specified. Note that you might see a database and a log backup within 5 minutes of enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the database.  
  
8.  **Monitor the Health Status:**  You can monitor through e-mail notifications you configured previously, or actively monitor the events logged. The following are some example Transact-SQL Statements used to view the events:  
  
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
  
    EXEC managed_backup.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek  
  
    SELECT * from @eventresult  
    WHERE event_type LIKE '%admin%'  
  
    ```  
  
    ```  
    -- to enable debug events  
    Use msdb;  
    Go  
             EXEC managed_backup.sp_set_parameter 'FileRetentionDebugXevent', 'True'  
  
    ```  
  
    ```  
    --  View all events in the current week  
    Use msdb;  
    Go  
    DECLARE @startofweek datetime  
    DECLARE @endofweek datetime  
    SET @startofweek = DATEADD(Day, 1-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)   
    SET @endofweek = DATEADD(Day, 7-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)  
  
    EXEC managed_backup.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek;  
  
    ```  
  
 The steps described in this section are specifically for configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time on the database. You can modify the existing configurations using the same system stored procedures and provide the new values.  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
