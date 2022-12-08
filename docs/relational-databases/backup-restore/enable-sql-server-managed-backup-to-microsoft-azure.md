---
title: Use managed backup to Azure
description: Learn how to enable SQL Server Managed Backup to Microsoft Azure at the database and instance level, and enable notifications and monitor backup activity.
ms.custom: seo-lt-2019, devx-track-azurecli
ms.date: "12/17/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 68ebb53e-d5ad-4622-af68-1e150b94516e
author: MashaMSFT
ms.author: mathoma
---

# Enable SQL Server managed backup to Azure

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to enable [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] with default settings at both the database and instance level. It also describes how to enable email notifications and how to monitor backup activity.  
  
 This tutorial uses Azure PowerShell. Before starting the tutorial, [download and install Azure PowerShell](/powershell/azure/).  
  
> [!IMPORTANT]  
>  If you also want to enable advanced options or use a custom schedule, configure those settings first before enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. For more information, see [ Configure advanced options for SQL Server managed backup to Microsoft Azure](../../relational-databases/backup-restore/configure-advanced-options-for-sql-server-managed-backup-to-microsoft-azure.md).  
  
## Create the Azure Blob Storage container

The process requires an Azure account. If you already have an account, go to the next step. Otherwise, you can get started with a [free trial](https://azure.microsoft.com/pricing/free-trial/) or explore [purchase options](https://azure.microsoft.com/pricing/purchase-options/).

For more information about storage accounts, see [About Azure Storage Accounts](/azure/storage/common/storage-account-create). 

#### [Azure CLI](#tab/azure-cli)

1. Sign in to your Azure account.

    ```azurecli-interactive
    az login
    ```
  
1. Create an Azure storage account. If you already have a storage account, go to the next step. The following command creates a storage account named `<backupStorage>` in the East US region.  
  
    ```azurecli-interactive
    az storage account create -n <backupStorage> -l "eastus" --resource-group <resourceGroup>
    ```  
    
1. Create a blob container named `<backupContainer>` for the backup files.
  
    ```azurecli-interactive
    $keys = az storage account keys list --account-name <backupStorage> --resource-group <resourceGroup> | ConvertFrom-Json
    az storage container create --name <backupContainer> --account-name <backupStorage> --account-key $keys[0].value 
    ```  
  
1. Generate a Shared Access Signature (SAS) to access the container. The following command creates a SAS token for the `<backupContainer>` blob container that expires in one year.  
  
    ```azurecli-interactive 
    az storage container generate-sas --name <backupContainer> --account-name <backupStorage> --account-key $keys[0].value
    ```

#### [PowerShell](#tab/azure-powershell)

1. The following command logs in to your Azure account.

    ```azurepowershell-interactive
    Connect-AzAccount
    Set-AzContext -SubscriptionId "<subscriptionId>"
    ```
  
1. Create an Azure storage account. If you already have a storage account, go to the next step. The following command creates a storage account named `<backupStorage>` in the East US region.  
  
    ```azurepowershell-interactive
    New-AzStorageAccount -StorageAccountName <backupStorage> -Location "EAST US" -ResourceGroupName <resourceGroup> -SkuName Standard_GRS
    ```   
  
1. Create a blob container named `<backupContainer>` for the backup files.  
  
    ```azurepowershell-interactive
    $context = New-AzStorageContext -StorageAccountName <backupStorage> -StorageAccountKey (Get-AzStorageAccountKey -Name <backupStorage> -ResourceGroupName <resourceGroup>).Value[0]  
    New-AzStorageContainer -Name <backupContainer> -Context $context  
    ```  
  
1. Generate a Shared Access Signature (SAS) to access the container. The following command creates a SAS token for the `<backupContainer>` blob container that expires in one year.
  
    ```azurepowershell-interactive 
    New-AzStorageContainerSASToken -Name <backupContainer> -Permission rwdl -ExpiryTime (Get-Date).AddYears(1) -FullUri -Context $context 
    ```

* * *

> [!NOTE]
> These steps can also be accomplished using the [Azure portal](https://portal.azure.com/).

The output will contain either the URL to the container and/or the SAS token. The following is an example:  
  
  `https://managedbackupstorage.blob.core.windows.net/backupcontainer?sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl`
  
If the URL is included, separate it from the SAS token at the question mark (do not include the question mark). For example, the previous output would result in the following two values.  
  
|Type|Output|  
|-|-|  
|**Container URL**|https://managedbackupstorage.blob.core.windows.net/backupcontainer|  
|**SAS token**|sv=2014-02-14&sr=c&sig=xM2LXVo1Erqp7LxQ%9BxqK9QC6%5Qabcd%9LKjHGnnmQWEsDf%5Q%se=2015-05-14T14%3B93%4V20X&sp=rwdl|  
  
Record the container URL and SAS for use in creating a SQL CREDENTIAL. For more information about SAS, see [Shared Access Signatures, Part 1: Understanding the SAS Model](/azure/storage/common/storage-sas-overview).  
  
## Enable managed backup to Azure
  
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
    USE msdb;  
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
  
    ```sql
    SELECT * FROM msdb.managed_backup.fn_get_current_xevent_settings()  
    ```  
  
     You should see that Admin, Operational, and Analytical channel events are enabled by default and cannot be disabled. This should be sufficient to monitor the events that require manual intervention.  You can enable debug events, but the debug channels include informational and debug events that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] uses to detect issues and solve them.  
  
6.  **Enable and Configure Notification for Health Status:** [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has a stored procedure that creates an agent job to send out e-mail notifications of errors or warnings that may require attention. The following steps describe the process to enable and configure e-mail notifications:  
  
    1.  Setup Database Mail if it is not already enabled on the instance. For more information, see [Configure Database Mail](../../relational-databases/database-mail/configure-database-mail.md).  
  
    2.  Configure SQL Server Agent Notification to use Database Mail. For more information, see [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md).  
  
    3.  **Enable e-mail notifications to receive backup errors and warnings:** From the query window, run the following Transact-SQL statements:  
  
        ```sql
        EXEC msdb.managed_backup.sp_set_parameter  
        @parameter_name = 'SSMBackup2WANotificationEmailIds',  
        @parameter_value = '<email1;email2>'  
        ```  
  
7.  **View backup files in the Azure Storage Account:** Connect to the storage account from SQL Server Management Studio or the Azure portal. You will see any backup files in the container you specified. Note that you might see a database and a log backup within 5 minutes of enabling [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the database.  
  
8.  **Monitor the Health Status:**  You can monitor through e-mail notifications you configured previously, or actively monitor the events logged. The following are some example Transact-SQL Statements used to view the events:  
  
    ```sql  
    --  view all admin events  
    USE msdb;  
    GO  
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
  
    ```sql  
    -- to enable debug events  
    USE msdb;  
    GO  
    EXEC managed_backup.sp_set_parameter 'FileRetentionDebugXevent', 'True'  
    ```  
  
    ```sql  
    --  View all events in the current week  
    USE msdb;  
    GO  
    DECLARE @startofweek datetime  
    DECLARE @endofweek datetime  
    SET @startofweek = DATEADD(Day, 1-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)   
    SET @endofweek = DATEADD(Day, 7-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)  
  
    EXEC managed_backup.sp_get_backup_diagnostics @begin_time = @startofweek, @end_time = @endofweek;  
    ```
  
The steps described in this section are specifically for configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time on the database. You can modify the existing configurations using the same system stored procedures and provide the new values.  
  
## See also  
 [SQL Server managed backup to Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)
