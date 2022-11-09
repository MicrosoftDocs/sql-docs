---
title: Migrate databases to SQL Managed Instance using Log Replay Service
description: Learn how to migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service (LRS).
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: migration
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
---

# Migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article explains how to migrate databases to Azure SQL Managed Instance by using [Log Replay Service (LRS)](log-replay-service-overview.md). LRS is a free-of-charge cloud service enabled for Azure SQL Managed Instance based on SQL Server log-shipping technology.

The following sources are supported: 

- SQL Server on-premises 
- SQL Server on Virtual Machines  
- Amazon Web Services (AWS) EC2
- Amazon Web Services (AWS) RDS
- Compute Engine (Google Cloud Platform - GCP)  
- Cloud SQL for SQL Server (Google Cloud Platform – GCP) 


## Requirements

Considering the following requirements for both SQL Server and Azure. 

### SQL Server 

Make sure you have the following requirements for SQL Server: 

- SQL Server versions from 2008 to 2022.
- The SQL Server database is using the full recovery model (mandatory).
- Full backup of databases (one or multiple files).
- Differential backup (one or multiple files).
- Log backup (not split for a transaction log file).
- For SQL Server 2008 - SQL Server 2016, take a backup locally and [manually upload](#copy-existing-backups-to-blob-storage) it to Azure Blob Storage. 
- Starting with SQL Server 2016 and later, you can [take your backup directly](#take-backups-directly-to-blob-storage) to Azure Blob Storage. Starting with SQL Server 2022, you can choose to use a managed identity instead of an SAS token to authenticate to Azure Blob Storage. 


While having `CHECKSUM` enabled for backups is not required, it is highly recommended for faster restore operations. 

### Azure 

Make sure you have the following requirements for Azure: 

- PowerShell Az.SQL module version 4.0.0 or later ([installed](https://www.powershellgallery.com/packages/Az.Sql/) or accessed through [Azure Cloud Shell](/azure/cloud-shell/)).
- Azure CLI version 2.42.0 or later ([installed](/cli/azure/install-azure-cli)).
- Provisioned Azure Blob Storage container.
- Shared access signature (SAS) security token with read and list permissions generated for the Blob Storage container or a managed identity that can access the container. 
- Place backup files for an individual database inside a separate folder in storage account using  flat-file structure (mandatory). Nested folders inside database folders aren't supported.

## Azure RBAC permissions

Running LRS through the provided clients requires one of the following Azure roles:

- Subscription Owner role
- [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role
- Custom role with the following permission: `Microsoft.Sql/managedInstances/databases/*`


## Best practices

Consider the following best practices when using LRS: 

- Run [Data Migration Assistant](/sql/dma/dma-overview) to validate that your databases are ready to be migrated to SQL Managed Instance. 
- Split full and differential backups into multiple files, instead of using a single file.
- Enable backup compression to help the network transfer speeds.
- Use Cloud Shell to run PowerShell or CLI scripts, because it will always be updated to the latest cmdlets released.
- Configure [maintenance window](../database/maintenance-window.md) to allow scheduling of system updates at a specific day/time. This configuration will help achieve a more predictable time of database migrations, as impactful system upgrades interrupt migration in progress.
- Plan to complete a single LRS migration job within a maximum of 30 days. On expiry of this time frame, the LRS job will be automatically canceled.
- Enable `CHECKSUM` when taking your backups for a faster database restore. SQL Managed Instance performs an integrity check on backups without `CHECKSUM`, increasing restore time. 

System updates on managed instance will take precedence over database migrations in progress. All pending LRS migrations in case of a system update on SQL Managed Instance will be suspended and resumed once the update has been applied. This system behavior might prolong migration time, especially in cases of large databases. To achieve a predictable time of database migrations, consider configuring [maintenance window](../database/maintenance-window.md) allowing scheduling of system updates at a specific day/time, and consider running and completing migration jobs outside of the scheduled maintenance window day/time.


> [!IMPORTANT]
> - You can't use databases being restored through LRS until the migration process completes. 
> - LRS doesn't support read-only access to databases during the migration.
> - After the migration completes, the migration process is finalized and can't be resumed with additional differential backups.

## Migrating multiple databases

If migrating multiple databases using the same Azure Blob Storage container, you must place backup files for different databases in separate folders inside the container. All backup files for a single database must be placed in a flat-file structure inside a database folder, and the folders can't be nested, as it's not supported. 

Below is an example of folder structure inside Azure Blob Storage container required to migrate multiple databases using LRS.

```URI
-- Place all backup files for database 1 in a separate "database1" folder in a flat-file structure.
-- Don't use nested folders inside database1 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database1>/<all-database1-backup-files>

-- Place all backup files for database 2 in a separate "database2" folder in a flat-file structure.
-- Don't use nested folders inside database2 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database2>/<all-database2-backup-files>

-- Place all backup files for database 3 in a separate "database3" folder in a flat-file structure. 
-- Don't use nested folders inside database3 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database3>/<all-database3-backup-files>
```


## Create a storage account

Azure Blob Storage is used as intermediary storage for backup files between SQL Server and SQL Managed Instance. To create a new storage account and a blob container inside the storage account, follow these steps:

1. [Create a storage account](/azure/storage/common/storage-account-create?tabs=azure-portal).
2. [Crete a blob container](/azure/storage/blobs/storage-quickstart-blobs-portal) inside the storage account.

## Authenticate to Blob Storage

Use either an SAS token or a managed identity to access to the storage Azure Blob Storage account. 

> [!WARNING]
> You cannot use both an SAS token and a managed identity in parallel on the same storage account. You can use EITHER an SAS token OR a managed identity but not both. 

### [SAS token](#tab/sas-token)

### Generate a Blob storage SAS authentication token for LRS

Access Azure Blob storage either using an SAS token. 

Azure Blob Storage is used as intermediary storage for backup files between SQL Server and SQL Managed Instance. Generate an SAS authentication token for LRS with only list and read permissions. The token enables LRS to access Blob Storage and uses the backup files to restore them to SQL Managed Instance. 

Follow these steps to generate the token:

1. Open **Storage Explorer** from the Azure portal.
2. Expand **Blob Containers**.
3. Right-click the blob container and select **Get Shared Access Signature**.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-sas-token-01.png" alt-text="Screenshot that shows selections for generating an S A S authentication token.":::

4. Select the time frame for token expiration. Ensure the token is valid during your migration.
5. Select the time zone for the token: UTC or your local time.
	
   > [!IMPORTANT]
   > The time zone of the token and your managed instance might mismatch. Ensure that the SAS token has the appropriate time validity, taking time zones into consideration. To account for time zone differences, set the validity time frame **FROM** well before your migration window starts, and the **TO** time frame well after you expect your migration to complete.

6. Select **Read** and **List** permissions only.

   > [!IMPORTANT]
   > Don't select any other permissions. If you do, LRS won't start. This security requirement is by-design.

7. Select **Create**.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-sas-token-02.png" alt-text="Screenshot that shows selections for S A S token expiration, time zone, and permissions, along with the Create button.":::

The SAS authentication is generated with the time validity that you specified. You need the URI version of the token, as shown in the following screenshot.

:::image type="content" source="./media/log-replay-service-migrate/lrs-generated-uri-token.png" alt-text="Screenshot that shows an example of the U R I version of an S A S token.":::

   > [!NOTE]
   > Using SAS tokens created with permissions set through defining a [stored access policy](/rest/api/storageservices/define-stored-access-policy) isn't supported at this time. Follow the instructions in this article to manually specify **Read** and **List** permissions for the SAS token.

### Copy parameters from the SAS token

Access Azure Blob storage either using an SAS token or a managed identity. 

Before you use the SAS token to start LRS, you need to understand its structure. The URI of the generated SAS token consists of two parts separated with a question mark (`?`), as shown in this example:

:::image type="content" source="./media/log-replay-service-migrate/lrs-token-structure.png" alt-text="Example U R I for a generated S A S token for Log Replay Service." border="false":::

The first part, starting with `https://` until the question mark (`?`), is used for the `StorageContainerURI` parameter that's fed as the input to LRS. It gives LRS information about the folder where the database backup files are stored.

The second part, starting after the question mark (`?`) and going all the way until the end of the string, is the `StorageContainerSasToken` parameter. This part is the actual signed authentication token, which is valid during the specified time. This part doesn't necessarily need to start with `sp=` as shown in the example. Your case may differ.

Copy the parameters as follows:

1. Copy the first part of the token, starting from `https://` all the way until the question mark (`?`). Use it as the `StorageContainerUri` parameter in PowerShell or the Azure CLI when starting LRS.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-token-uri-copy-part-01.png" alt-text="Screenshot that shows copying the first part of the token.":::

2. Copy the second part of the token, starting after the question mark (`?`) all the way until the end of the string. Use it as the `StorageContainerSasToken` parameter in PowerShell or the Azure CLI when starting LRS.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-token-uri-copy-part-02.png" alt-text="Screenshot that shows copying the second part of the token.":::
   
> [!NOTE]
> Don't include the question mark (`?`) when you copy either part of the token.
> 

### [Managed identity](#tab/managed-identity)

To use a managed identity, assign either a system-managed, or user-managed identity to access the Azure Blob Storage container.  

To do so, follow these steps: 

1. Go to your Blob storage in the [Azure portal](https://portal.azure.com) that you intend to authorize managed instance access to. 
1. Select **Access control (IAM)**. 
1. Select **+Add**, and then **Add role assignment**. 
1. Search for and select the existing role: **Storage Blob Data Reader**, however custom roles are supported as long as they have the Read and List permissions, at minimum. 
1. Choose **Managed Identity**. 
1. Use the **+Select members** option to identify:
    - Your subscription
    - In the **Managed Identity** field, choose **SQL Managed Instance**
    - Then choose the SQL Managed Instance you intend to migrate to. 
1. Choose **Select** to save your settings and authorize access. 
1. Complete by selecting **Review + assign**. 

---

## Validate MI storage access

Validate that your managed instance can access to the Azure Blob storage. 

First, upload any database backup, such as `full_0_0.bak`, to your Azure Blob Storage container. 

Next, connect to your managed instance, and run a sample test query to determine if your managed instance is able to access the backup in the container. 

### [SAS token](#tab/sas-token)

If you're using an SAS token to authenticate to your storage account, then replace the `<sastoken>` with  your SAS token and run the following query on your instance: 

```sql
CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/databases] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE' 
, SECRET = '<sastoken>' 

RESTORE HEADERONLY 
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/full_0_0.bak' 
```

### [Managed identity](#tab/managed-identity)

If you're using a managed identity to authenticate to your storage account, then update the `CREATE CREDENTIAL` with your storage account URL, and run the following sample query on your instance: 

```sql
RESTORE HEADERONLY 
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/full_0_0.bak' 

CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'MANAGED IDENTITY' 
```

---

## Upload backups from SQL Server to Blob Storage

Once your blob container is ready and you've confirmed your managed instance can access the container, you can start to upload your backups to Blob Storage. You can either copy your backups to Azure Blob Storage, or if your environment allows it, starting with SQL Server 2012 SP1 CU2 and SQL Server 2014, you can take backups from SQL Server directly to Azure Blob Storage by using the [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) command. 



### Copy existing backups to Blob Storage

If you're on an older version of SQL Server, or if your environment doesn't support backing up directly to URL, take your backups on SQL Server as you normally would, and then copy them to Azure Blob Storage. 

#### Take backup on SQL Server

Set databases that you want to migrate to the full recovery model to allow log backups.

```SQL
-- To permit log backups, before the full database backup, modify the database to use the full recovery
USE master
ALTER DATABASE SampleDB
SET RECOVERY FULL
GO
```

To manually make full, differential, and log backups of your database to local storage, use the following sample T-SQL scripts. `CHECKSUM` is not required, but recommended. 


The following example takes a full database backup to the local disk: 

```SQL
-- Take full database backup to local disk
BACKUP DATABASE [SampleDB]
TO DISK='C:\BACKUP\SampleDB_full.bak'
WITH INIT, COMPRESSION, CHECKSUM
GO
```

The following example takes a differential backup to the local disk: 

```sql
-- Take differential database backup to local disk
BACKUP DATABASE [SampleDB]
TO DISK='C:\BACKUP\SampleDB_diff.bak'
WITH DIFFERENTIAL, COMPRESSION, CHECKSUM
GO
```

The following example takes a transaction log backup to the local disk: 

```sql
-- Take transactional log backup to local disk
BACKUP LOG [SampleDB]
TO DISK='C:\BACKUP\SampleDB_log.trn'
WITH COMPRESSION, CHECKSUM
GO
```

#### Copy backups to Blob Storage 

Once your backups are ready, and you want to start migrating databases to a managed instance by using LRS, you can use the following approaches to copy existing backups to Blob Storage:

- Download and install [AzCopy](/azure/storage/common/storage-use-azcopy-v10)
- Download and install [Azure Storage Explorer](/azure/vs-azure-tools-storage-manage-with-storage-explorer?tabs=windows) 
- Use [Storage Explore in the Azure portal](/azure/storage/blobs/storage-quickstart-blobs-portal?source=recommendations)


> [!NOTE]
> To migrate multiple databases using the same Azure Blob Storage container, place all backup files of an individual database into a separate folder inside the container. Use flat-file structure for each database folder, as nested folders aren't supported.
> 

### Take backups directly to Blob Storage

If you're on a supported version of SQL Server (starting with SQL Server 2012 SP1 CU2 and SQL Server 2014), and your corporate and network policies allow it, you can take backups from SQL Server directly to Blob Storage by using the native SQL Server [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) option. If you can use `BACKUP TO URL`, you don't need to take backups to local storage and upload them to Blob Storage.

When you take native backups directly to Azure Blob Storage, you have to authenticate to the Storage account. You can do so by using an SAS token, or, if you're on SQL Server 2022, you can also use a managed identity.  



### [SAS token](#tab/sas-token)

Use the following command to create a credential that imports the SAS token to your SQL Server instance: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
SECRET = '<SAS_TOKEN>';  
```

For detailed instructions working with SAS tokens, review the tutorial [Use Azure Blob Storage with SQL Server](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016#1---create-stored-access-policy-and-shared-access-storage).



### [Managed identity](#tab/managed-identity)

SQL Server 2022 added support for managed identities. If you're on SQL Server 2022, you can authenticate to your storage account using a managed identity. 

Use the following command to create a credential that uses the managed identity on your SQL Server instance: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'MANAGED IDENTITY'
```

---

After you've created the credential to authenticate your SQL Server instance with Blob Storage, you can use the [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) command to take backups directly to the storage account. `CHECKSUM` is recommended, but not required. 



The following example takes a full database backup to a URL: 

```SQL
-- Take a full database backup to a URL
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>/SampleDB_full.bak'
WITH INIT, COMPRESSION, CHECKSUM
GO
```

The following example takes a differential database backup to a URL: 

```sql
-- Take a differential database backup to a URL
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>/SampleDB_diff.bak'  
WITH DIFFERENTIAL, COMPRESSION, CHECKSUM
GO
```

The following example takes a transaction log backup to a URL: 

```sql
-- Take a transactional log backup to a URL
BACKUP LOG [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>/SampleDB_log.trn'  
WITH COMPRESSION, CHECKSUM
```


## Log in to Azure and select a subscription

Use the following PowerShell cmdlet to log in to Azure:

```powershell
Login-AzAccount
```

Select the appropriate subscription where your managed instance resides by using the following PowerShell cmdlet:

```powershell
Select-AzSubscription -SubscriptionId <subscription ID>
```

## Start migration

Start the migration by starting LRS. You can start the service in either autocomplete or continuous mode. 

When you use autocomplete mode, the migration completes automatically when the last of the specified backup files have been restored. This option requires the entire backup chain to be available in advance, and uploaded to Azure Blob Storage. It doesn't allow adding new backup files while migration is in progress. This option requires the start command to specify the filename of the last backup file. This mode is recommended for passive workloads for which data catch-up isn't required.

When you use continuous mode, the service continuously scans Azure Blob Storage folder and restores any new backup files that keep getting added while migration is in progress. The migration completes only after the manual cutover has been requested. Continuous mode migration needs to be used when you don't have the entire backup chain in advance, and when you plan to add new backup files once the migration is in progress. This mode is recommended for active workloads for which data catch-up is required.

Plan to complete a single LRS migration job within a maximum of 30 days. On expiry of this timeframe, the LRS job will be automatically canceled.

> [!NOTE]
> When migrating multiple databases, LRS must be started separately for each database pointing to the full URI path of Azure Blob storage container and the individual database folder.
> 

### Start LRS in autocomplete mode

Ensure that the entire backup chain has been uploaded to Azure Blob Storage. This option doesn't allow new backup files to be added once the migration is in progress.

To start LRS in autocomplete mode, use PowerShell or Azure CLI commands. Specify the last backup file name by using the `-LastBackupName` parameter. After restore of the last specified backup file has completed, the service automatically initiates a cutover.

Restore your database from Azure storage either using the SAS token, or a managed identity. 

> [!IMPORTANT]
> - Ensure that the entire backup chain has been uploaded to Azure Blob Storage prior to starting the migration in autocomplete mode. This mode doesn't allow new backup files to be added once the migration is in progress.
> - Ensure that you have specified the last backup file correctly, and that you have not uploaded more files after it. If the system detects more backup files beyond the last specified backup file, the migration will fail.
>

### [SAS token](#tab/sas-token)

The following PowerShell example starts LRS in autocomplete mode using an SAS token: 

```PowerShell
Start-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName" `
	-Collation "SQL_Latin1_General_CP1_CI_AS" `
	-StorageContainerUri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>" `
	-StorageContainerSasToken "sv=2019-02-02&ss=b&srt=sco&sp=rl&se=2023-12-02T00:09:14Z&st=2019-11-25T16:09:14Z&spr=https&sig=92kAe4QYmXaht%2Fgjocqwerqwer41s%3D" `
	-AutoCompleteRestore `
	-LastBackupName "last_backup.bak"
```

The following Azure CLI example starts LRS in autocomplete mode using an SAS token: 

```CLI
az sql midb log-replay start -g mygroup --mi myinstance -n mymanageddb -a --last-bn "backup.bak"
	--storage-uri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>"
	--storage-sas "sv=2019-02-02&ss=b&srt=sco&sp=rl&se=2023-12-02T00:09:14Z&st=2019-11-25T16:09:14Z&spr=https&sig=92kAe4QYmXaht%2Fgjocqwerqwer41s%3D"
```

### [Managed identity](#tab/managed-identity)

The following PowerShell example starts LRS in autocomplete mode using a managed identity: 

```PowerShell
Start-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName" `
	-Collation "SQL_Latin1_General_CP1_CI_AS" `
	-StorageContainerUri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>" `
	-StorageContainerIdentity ManagedIdentity  `
	-AutoCompleteRestore `
	-LastBackupName "last_backup.bak"
```

---

### Start LRS in continuous mode

Ensure that you've uploaded your initial backup chain to Azure Blob Storage.

> [!IMPORTANT]
> Once LRS has been started in continuous mode, you'll be able to add new log and differential backups to Azure Blob Storage until the manual cutover. Once manual cutover has been initiated, no additional differential files can be added, nor restored.
> 

### [SAS token](#tab/sas-token)

The following PowerShell example starts LRS in continuous mode using an SAS token:

```PowerShell
Start-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName" `
	-Collation "SQL_Latin1_General_CP1_CI_AS" -StorageContainerUri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>" `
	-StorageContainerSasToken "sv=2019-02-02&ss=b&srt=sco&sp=rl&se=2023-12-02T00:09:14Z&st=2019-11-25T16:09:14Z&spr=https&sig=92kAe4QYmXaht%2Fgjocqwerqwer41s%3D"
```

The following Azure CLI example starts LRS in continuous mode:

```CLI
az sql midb log-replay start -g mygroup --mi myinstance -n mymanageddb
	--storage-uri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>"
	--storage-sas "sv=2019-02-02&ss=b&srt=sco&sp=rl&se=2023-12-02T00:09:14Z&st=2019-11-25T16:09:14Z&spr=https&sig=92kAe4QYmXaht%2Fgjocqwerqwer41s%3D"
```



### [Managed identity](#tab/managed-identity)

The following PowerShell example starts LRS in continuous mode using a managed identity:

```PowerShell
Start-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName" `
	-Collation "SQL_Latin1_General_CP1_CI_AS" -StorageContainerUri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>" `
	-StorageContainerIdentity ManagedIdentity
```

---

### Scripting the migration job

PowerShell and CLI clients that start LRS in continuous mode are synchronous. In this mode, PowerShell and CLI wait for the API response to report on success or failure to start the job. 

During this wait, the command won't return control to the command prompt. If you're scripting the migration experience, and you need the LRS start command to give back control immediately to continue with rest of the script, you can run PowerShell as a background job with the `-AsJob` switch. For example:

```PowerShell
$lrsjob = Start-AzSqlInstanceDatabaseLogReplay <required parameters> -AsJob
```

When you start a background job, a job object returns immediately, even if the job takes an extended time to complete. You can continue to work in the session without interruption while the job runs. For details on running PowerShell as a background job, see the [PowerShell Start-Job](/powershell/module/microsoft.powershell.core/start-job#description) documentation.

Similarly, to start an Azure CLI command on Linux as a background process, use the ampersand (`&`) at the end of the LRS start command:

```CLI
az sql midb log-replay start <required parameters> &
```

## Monitor migration progress

[Az.SQL 4.0.0 and later](https://www.powershellgallery.com/packages/Az.Sql/4.0.0) provides a detailed progress report. Review [Managed Database Restore Details - Get](/rest/api/sql/2022-02-01-preview/managed-database-restore-details/get?tabs=HTTP) for a sample output.  

To monitor migration progress through PowerShell, use the following command:

```PowerShell
Get-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName"
```

To monitor migration progress through the Azure CLI, use the following command:

```CLI
az sql midb log-replay show -g mygroup --mi myinstance -n mymanageddb
```

## Stop the migration (optional)

If you need to stop the migration, use PowerShell or the Azure CLI. Stopping the migration deletes the restoring database on SQL Managed Instance, so resuming the migration won't be possible.

To stop the migration process through PowerShell, use the following command:

```PowerShell
Stop-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
	-InstanceName "ManagedInstance01" `
	-Name "ManagedDatabaseName"
```

To stop the migration process through the Azure CLI, use the following command:

```CLI
az sql midb log-replay stop -g mygroup --mi myinstance -n mymanageddb
```

## Complete the migration (continuous mode)

If you started LRS in continuous mode, ensure that your application, and SQL Server workload have been stopped to prevent any new backup files from being generated. Ensure that the last backup from SQL Server has been uploaded to Azure Blob Storage. Monitor the restore progress on managed instance, ensuring that the last log-tail backup has been restored.

Once the last log-tail backup has been restored on managed instance, initiate the manual cutover to complete the migration. After the cutover has completed, the database will become available for read and write access on managed instance.

To complete the migration process in LRS continuous mode through PowerShell, use the following command:

```PowerShell
Complete-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
-InstanceName "ManagedInstance01" `
-Name "ManagedDatabaseName" `
-LastBackupName "last_backup.bak"
```

To complete the migration process in LRS continuous mode through the Azure CLI, use the following command:

```CLI
az sql midb log-replay complete -g mygroup --mi myinstance -n mymanageddb --last-backup-name "backup.bak"
```


## Troubleshooting

After you start LRS, use the monitoring cmdlet (PowerShell: `get-azsqlinstancedatabaselogreplay` or Azure CLI: `az_sql_midb_log_replay_show`) to see the status of the operation. If LRS fails to start after some time and you get an error, check for the most common issues:

- Does an existing database on SQL Managed Instance have the same name as the one you're trying to migrate from SQL Server? Resolve this conflict by renaming one of the databases.
- Are the permissions granted for the SAS **token Read** and **List** _only_?
- Did you copy the SAS token for LRS after the question mark (`?`), with content starting like this: `sv=2020-02-10...`? 
- Is the SAS token validity time applicable for the time window of starting and completing the migration? There might be mismatches due to the different time zones used for SQL Managed Instance and the SAS token. Try regenerating the SAS token and extending the token validity of the time window before and after the current date.
- Are the database name, resource group name, and managed instance name spelled correctly?
- If you started LRS in autocomplete mode, was a valid filename for the last backup file specified?


## Next steps

- Learn more about [migrating to SQL Managed Instance using the link feature](managed-instance-link-feature-overview.md).
- Learn more about [migrating from SQL Server to SQL Managed instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).
- Learn more about [differences between SQL Server and SQL Managed Instance](transact-sql-tsql-differences-sql-server.md).
- Learn more about [best practices to cost and size workloads migrated to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs).
