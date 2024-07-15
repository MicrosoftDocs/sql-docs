---
title: Migrate databases by using Log Replay Service
description: Learn how to migrate databases from a SQL Server to Azure SQL Managed Instance by using Log Replay Service.
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: migration
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
  - sql-migration-content
---

# Migrate databases from SQL Server by using Log Replay Service - Azure SQL Managed Instance 

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article explains how to migrate databases to Azure SQL Managed Instance by using [Log Replay Service (LRS)](log-replay-service-overview.md). LRS is a free-of-charge cloud service that's available for Azure SQL Managed Instance, based on SQL Server log-shipping technology.

The following sources are supported: 

- SQL Server on Virtual Machines
- Amazon EC2 (Elastic Compute Cloud)
- Amazon RDS (Relational Database Service) for SQL Server
- Google Compute Engine
- Cloud SQL for SQL Server - GCP (Google Cloud Platform)

## Prerequisites

Before you begin, consider the following requirements for both your SQL Server instance and Azure. 

### SQL Server 

Make sure that you meet the following requirements for SQL Server: 

- SQL Server versions 2008 to 2022.
- Your SQL Server database is using the full recovery model (mandatory).
- A full backup of databases (one or multiple files).
- A differential backup (one or multiple files).
- A log backup (not split for a transaction log file).
- For SQL Server versions 2008 to 2016, take a backup locally and [manually upload](#copy-existing-backups-to-your-blob-storage-account) it to your Azure Blob Storage account. 
- For SQL Server 2016 and later, you can [take your backup directly](#take-backups-directly-to-your-blob-storage-account) to your Azure Blob Storage account. 



Although having `CHECKSUM` enabled for backups isn't required, we highly recommend it for faster restore operations. 

### Azure 

Make sure that you meet the following requirements for Azure: 

- PowerShell Az.SQL module version 4.0.0 or later ([installed](https://www.powershellgallery.com/packages/Az.Sql/) or accessed through [Azure Cloud Shell](/azure/cloud-shell/)).
- Azure CLI version 2.42.0 or later ([installed](/cli/azure/install-azure-cli)).
- A provisioned Azure Blob Storage container.
- A shared access signature (SAS) security token with Read and List permissions generated for the Blob Storage container, or a managed identity that can access the container. 
- Place backup files for an individual database inside a separate folder in a storage account by using a flat-file structure (mandatory). Nesting folders inside database folders isn't supported.

## Azure RBAC permissions

Running LRS through the provided clients requires one of the following Azure role-based access control (RBAC) roles:

- [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role
- A role with the following permission: `Microsoft.Sql/managedInstances/databases/*`


## Best practices

When you're using LRS, consider the following best practices: 

- Run [Data Migration Assistant](/sql/dma/dma-overview) to validate that your databases are ready to be migrated to SQL Managed Instance. 
- Split full and differential backups into multiple files, instead of using a single file.
- Enable backup compression to help the network transfer speeds.
- Use Cloud Shell to run PowerShell or CLI scripts, because it will always be updated to use the latest released cmdlets.
- Configure a [maintenance window](maintenance-window.md) to allow scheduling of system updates at a specific day and time. This configuration helps achieve a more predictable time for database migrations, because system upgrades can interrupt in-progress migrations.
- Plan to complete a single LRS migration job within a maximum of 30 days. On expiration of this time frame, the LRS job is automatically canceled.
- For a faster database restore, enable `CHECKSUM` when you're taking your backups. SQL Managed Instance performs an integrity check on backups without `CHECKSUM`, which increases restore time. 

System updates for SQL Managed Instance take precedence over database migrations in progress. During a system update on an instance, all pending LRS migrations are suspended and resumed only after the update is applied. This system behavior might prolong migration time, especially for large databases. 

To achieve a predictable time for database migrations, consider configuring a [maintenance window](maintenance-window.md) to schedule system updates for a specific day and time, and run and complete migration jobs outside the designated maintenance window timeframe.

> [!IMPORTANT]
> - You can't use databases that are being restored through LRS until the migration process finishes. 
> - LRS doesn't support read-only access to databases during the migration.
> - After the migration finishes, the migration process is final and can't be resumed with additional differential backups.

> [!NOTE]
> After the cutover, SQL Managed Instance with Business Critical service tier can take significantly longer than General Purpose to be available as three secondary replicas have to be seeded for the availability group. The operation duration depends on the size of data. For more information, see [Management operations duration](/azure/azure-sql/managed-instance/management-operations-overview#duration).

## Migrate multiple databases

If you're migrating multiple databases by using the same Azure Blob Storage container, you must place backup files for different databases in separate folders inside the container. All backup files for a single database must be placed in a flat-file structure inside a database folder, and the folders can't be nested. Nesting folders inside database folders isn't supported. 

Here's an example of a folder structure inside an Azure Blob Storage container, a structure that's required to migrate multiple databases by using LRS.

```URI
-- Place all backup files for database 1 in a separate "database1" folder in a flat-file structure.
-- Don't use nested folders inside the database1 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database1>/<all-database1-backup-files>

-- Place all backup files for database 2 in a separate "database2" folder in a flat-file structure.
-- Don't use nested folders inside the database2 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database2>/<all-database2-backup-files>

-- Place all backup files for database 3 in a separate "database3" folder in a flat-file structure. 
-- Don't use nested folders inside the database3 folder.
https://<mystorageaccountname>.blob.core.windows.net/<containername>/<database3>/<all-database3-backup-files>
```

## Create a storage account

You use an Azure Blob Storage account as intermediary storage for backup files between your SQL Server instance and your SQL Managed Instance deployment. To create a new storage account and a blob container inside the storage account:

1. [Create a storage account](/azure/storage/common/storage-account-create?tabs=azure-portal).
1. [Create a blob container](/azure/storage/blobs/storage-quickstart-blobs-portal) inside the storage account.

### Configure Azure storage behind a firewall

Using Azure Blob storage that's protected behind a firewall is supported, but requires additional configuration. To enable read / write access to Azure Storage with Azure Firewall turned on, you have to add the subnet of the SQL managed instance to the firewall rules of the vNet for the storage account by using MI subnet delegation and the Storage service endpoint. The storage account and the managed instance must be in the same region, or two paired regions. 

If your Azure storage is behind a firewall, you may see the following message in the SQL managed instance error log: 

```
Audit: Storage access denied user fault. Creating an email notification:
```

This generates an email that notifies you that auditing for the SQL managed instance is failing to write audit logs to the storage account.  If you see this error, or receive this email, follow the steps in this section to configure your firewall. 

To configure the firewall, follow these steps: 

1. Go to your managed instance in the [Azure portal](https://portal.azure.com) and select the subnet to open the **Subnets** page.

   :::image type="content" source="media/log-replay-service-migrate/sql-managed-instance-overview-page.png" alt-text="Screenshot of the SQL managed instance Overview page of the Azure portal, with the subnet selected.":::

1. On the **Subnets** page, select the name of the subnet to open the subnet configuration page. 

   :::image type="content" source="media/log-replay-service-migrate/sql-managed-instance-subnet.png" alt-text="Screenshot of the SQL managed instance Subnet page of the Azure portal, with the subnet selected.":::

1. Under **Subnet delegation**, choose **Microsoft.Sql/managedInstances** from the **Delegate subnet to a service** drop-down menu. Wait about an hour for permissions to propagate, and then, under **Service endpoints**, choose **Microsoft.Storage** from the **Services** drop-down. 

   :::image type="content" source="media/log-replay-service-migrate/sql-managed-instance-subnet-configuration.png" alt-text="Screenshot of the SQL managed instance Subnet configuration page of the Azure portal.":::

1. Next, go to your storage account in the Azure portal, select **Networking** under **Security + networking** and then choose the **Firewalls and virtual networks**  tab. 
1. On the **Firewalls and virtual networks** tab for your storage account, choose **+Add existing virtual network** to open the **Add networks** page. 

   :::image type="content" source="media/log-replay-service-migrate/storage-neteworking.png" alt-text="Screenshot of the Storage Account Networking page of the Azure portal, with Add existing virtual network selected.":::

1. Select the appropriate subscription, virtual network, and managed instance subnet from the drop-down menus and then select **Add** to add the virtual network of the SQL managed instance to the storage account. 


## Authenticate to your Blob Storage account

Use either a SAS token or a managed identity to access your Azure Blob Storage account. 

> [!WARNING]
> You can't use both a SAS token and a managed identity in parallel on the same storage account. You can use *either* a SAS token *or* a managed identity, but not both. 

### [SAS token](#tab/sas-token)

### Generate a Blob Storage SAS authentication token for LRS

Access your Azure Blob Storage account by using a SAS token. 

You can use an Azure Blob Storage account as intermediary storage for backup files between your SQL Server instance and your SQL Managed Instance deployment. Generate a SAS authentication token for LRS with only Read and List permissions. The token enables LRS to access your Blob Storage account, and it uses the backup files to restore them to your managed instance. 

Follow these steps to generate the token:

1. In the Azure portal, open **Storage Explorer**.
1. Expand **Blob Containers**.
1. Right-click the blob container, and then select **Get Shared Access Signature**.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-sas-token-01.png" alt-text="Screenshot that shows selections for generating a SAS authentication token.":::

1. Select the time frame for token expiration. Ensure that the token is valid during your migration.

1. Select the time zone for the token: UTC or your local time.
    
   > [!IMPORTANT]
   > The time zone of the token and your managed instance might mismatch. Ensure that the SAS token has the appropriate time validity, taking time zones into consideration. To account for time zone differences, set the validity **FROM** value well before your migration window starts, and the **TO** value well after you expect your migration to finish.

1. Select **Read** and **List** permissions only.

   > [!IMPORTANT]
   > Don't select any other permissions. If you do, LRS won't start. This security requirement is by design.

1. Select **Create**.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-sas-token-02.png" alt-text="Screenshot that shows selections for SAS token expiration, time zone, and permissions, along with the Create button.":::

The SAS authentication is generated with the time validity that you specified. You need the URI version of the token, as shown in the following screenshot:

:::image type="content" source="./media/log-replay-service-migrate/lrs-generated-uri-token.png" alt-text="Screenshot that shows an example of the URI version of a SAS token.":::

   > [!NOTE]
   > Using SAS tokens created with permissions that were set by defining a [stored access policy](/rest/api/storageservices/define-stored-access-policy) isn't supported at this time. Follow the instructions in this procedure to manually specify **Read** and **List** permissions for the SAS token.

### Copy parameters from the SAS token

Access your Azure Blob Storage account by using either a SAS token or a managed identity. 

Before you use the SAS token to start LRS, you need to understand its structure. The URI of the generated SAS token consists of two parts, separated with a question mark (`?`), as shown in this example:

:::image type="content" source="./media/log-replay-service-migrate/lrs-token-structure.png" alt-text="Example URI for a generated SAS token for Log Replay Service." border="false":::

The first part, starting with `https://` until the question mark (`?`), is used for the `StorageContainerURI` parameter that's fed as the input to LRS. It gives LRS information about the folder where the database backup files are stored.

The second part, from after the question mark (`?`) through the end of the string, is the `StorageContainerSasToken` parameter. This part is the actual signed authentication token, which is valid during the specified time. This part doesn't necessarily need to start with `sp=` as shown in the example. Your scenario might differ.

Copy the parameters as follows:

1. Copy the first part of the token, from `https://` up to but not including the question mark (`?`). Use it as the `StorageContainerUri` parameter in PowerShell or the Azure CLI when you're starting LRS.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-token-uri-copy-part-01.png" alt-text="Screenshot that shows where to copy the first part of the token.":::

1. Copy the second part of the token, from after the question mark (`?`) through the end of the string. Use it as the `StorageContainerSasToken` parameter in PowerShell or the Azure CLI when you're starting LRS.

   :::image type="content" source="./media/log-replay-service-migrate/lrs-token-uri-copy-part-02.png" alt-text="Screenshot that shows where to copy the second part of the token.":::
   
> [!NOTE]
> Don't include the question mark (`?`) when you copy either part of the token.
> 

### [Managed identity](#tab/managed-identity)

To use a managed identity, assign either a system-managed or user-managed identity to access the Azure Blob Storage container.  

To do so, follow these steps: 

1. In the [Azure portal](https://portal.azure.com), go to the Blob Storage account that you intend to authorize managed instance access to. 
1. Select **Access control (IAM)**. 
1. Select **Add**, and then select **Add role assignment**. 
1. Search for and select the existing role, **Storage Blob Data Reader**. Custom roles are supported as long as they have Read and List permissions, at minimum. 
1. Select **Managed Identity**. 
1. Use the **Select members** option to identify your subscription.
1. In the **Managed Identity** dropdown list, select **SQL Managed Instance**, and then select the managed instance you intend to migrate to. 
1. Choose **Select** to save your settings and authorize access. 
1. Complete the process by selecting **Review + assign**. 

---

## Validate your managed instance storage access

Validate that your managed instance can access your Blob Storage account. 

First, upload any database backup, such as `full_0_0.bak`, to your Azure Blob Storage container. 

Next, connect to your managed instance, and run a sample test query to determine if your managed instance is able to access the backup in the container. 

### [SAS token](#tab/sas-token)

If you're using a SAS token to authenticate to your storage account, then replace the `<sastoken>` with  your SAS token and run the following query on your instance: 

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

## Upload backups to your Blob Storage account

When your blob container is ready and you've confirmed that your managed instance can access the container, you can begin uploading your backups to your Blob Storage account. You can either:

- Copy your backups to your Blob Storage account.
- Take backups from SQL Server directly to your Blob Storage account by using the [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) command, if your environment allows it (starting with SQL Server versions 2012 SP1 CU2 and SQL Server 2014). 

### Copy existing backups to your Blob Storage account

If you're on an earlier version of SQL Server, or if your environment doesn't support backing up directly to a URL, take your backups on your SQL Server instance as you normally would, and then copy them to your Blob Storage account. 

#### Take backups on a SQL Server instance

Set databases that you want to migrate to the full recovery model to allow log backups.

```SQL
-- To permit log backups, before the full database backup, modify the database to use the full recovery
USE master
ALTER DATABASE SampleDB
SET RECOVERY FULL
GO
```

To manually make full, differential, and log backups of your database to local storage, use the following sample T-SQL scripts. `CHECKSUM` isn't required, but we do recommend it. 


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

#### Copy backups to your Blob Storage account 

After your backups are ready, and you want to start migrating databases to a managed instance by using LRS, you can use the following approaches to copy existing backups to your Blob Storage account:

- Download and install [AzCopy](/azure/storage/common/storage-use-azcopy-v10).
- Download and install [Azure Storage Explorer](/azure/vs-azure-tools-storage-manage-with-storage-explorer?tabs=windows). 
- Use [Storage Explorer in the Azure portal](/azure/storage/blobs/storage-quickstart-blobs-portal?source=recommendations).


> [!NOTE]
> To migrate multiple databases by using the same Azure Blob Storage container, place all backup files for an individual database into a separate folder inside the container. Use flat-file structure for each database folder. Nesting folders inside database folders isn't supported.
> 

### Take backups directly to your Blob Storage account

If you're on a supported version of SQL Server (starting with SQL Server 2012 SP1 CU2 and SQL Server 2014), and your corporate and network policies allow it, you can take backups from SQL Server directly to your Blob Storage account by using the native SQL Server [BACKUP TO URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url) option. If you can use `BACKUP TO URL`, you don't need to take backups to local storage and upload them to your Blob Storage account.

When you take native backups directly to your Blob Storage account, you have to authenticate to the storage account. 

Use the following command to create a credential that imports the SAS token to your SQL Server instance: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
SECRET = '<SAS_TOKEN>';  
```

For detailed instructions working with SAS tokens, review the tutorial [Use Azure Blob Storage with SQL Server](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016#1---create-stored-access-policy-and-shared-access-storage).


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


## Sign in to Azure and select a subscription

Use the following PowerShell cmdlet to sign in to Azure:

```powershell
Login-AzAccount
```

Select the subscription where your managed instance resides by using the following PowerShell cmdlet:

```powershell
Select-AzSubscription -SubscriptionId <subscription ID>
```

## Start the migration

Start the migration by starting LRS. You can start the service in either *autocomplete* or *continuous* mode. 

When you use autocomplete mode, the migration finishes automatically when the last of the specified backup files have been restored. This option requires the entire backup chain to be available in advance and uploaded to your Blob Storage account. It doesn't allow adding new backup files while migration is in progress. This option requires the `start` command to specify the file name of the last backup file. We recommend this mode for passive workloads for which data catch-up isn't required.

When you use continuous mode, the service continuously scans the Azure Blob Storage folder and restores any new backup files that get added while migration is in progress. The migration finishes only after the manual cutover has been requested. You need to use continuous mode migration when you don't have the entire backup chain in advance, and when you plan to add new backup files after the migration is in progress. We recommend this mode for active workloads for which data catch-up is required.

Plan to complete a single LRS migration job within a maximum of 30 days. When this time expires, the LRS job is automatically canceled.

> [!NOTE]
> When you're migrating multiple databases, LRS must be started separately for each database and point to the full URI path of the Azure Blob Storage container and the individual database folder.
> 

### Start LRS in autocomplete mode

Ensure that the entire backup chain has been uploaded to your Azure Blob Storage account. This option doesn't allow new backup files to be added while the migration is in progress.

To start LRS in autocomplete mode, use PowerShell or Azure CLI commands. Specify the last backup file name by using the `-LastBackupName` parameter. After the restore of the last specified backup file has finished, the service automatically initiates a cutover.

Restore your database from the storage account by using either the SAS token or a managed identity. 

> [!IMPORTANT]
> - Ensure that the entire backup chain has been uploaded to your Azure Blob Storage account before you start the migration in autocomplete mode. This mode doesn't allow new backup files to be added while the migration is in progress.
> - Ensure that you've specified the last backup file correctly, and that you haven't uploaded more files after it. If the system detects more backup files beyond the last specified backup file, the migration will fail.
>

### [SAS token](#tab/sas-token)

The following PowerShell example starts LRS in autocomplete mode by using a SAS token: 

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

The following Azure CLI example starts LRS in autocomplete mode by using a SAS token: 

```CLI
az sql midb log-replay start -g mygroup --mi myinstance -n mymanageddb -a --last-bn "backup.bak"
    --storage-uri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>"
    --storage-sas "sv=2019-02-02&ss=b&srt=sco&sp=rl&se=2023-12-02T00:09:14Z&st=2019-11-25T16:09:14Z&spr=https&sig=92kAe4QYmXaht%2Fgjocqwerqwer41s%3D"
```

### [Managed identity](#tab/managed-identity)

The following PowerShell example starts LRS in autocomplete mode by using a managed identity: 

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

Ensure that you've uploaded your initial backup chain to your Azure Blob Storage account.

> [!IMPORTANT]
> After you've started LRS in continuous mode, you'll be able to add new log and differential backups to your storage account until the manual cutover. After the manual cutover has been initiated, no additional differential files can be added or restored.
> 

### [SAS token](#tab/sas-token)

The following PowerShell example starts LRS in continuous mode by using a SAS token:

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

The following PowerShell example starts LRS in continuous mode by using a managed identity:

```PowerShell
Start-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
    -InstanceName "ManagedInstance01" `
    -Name "ManagedDatabaseName" `
    -Collation "SQL_Latin1_General_CP1_CI_AS" -StorageContainerUri "https://<mystorageaccountname>.blob.core.windows.net/<containername>/<databasefolder>" `
    -StorageContainerIdentity ManagedIdentity
```

---

### Script the migration job

PowerShell and Azure CLI clients that start LRS in continuous mode are synchronous. In this mode, PowerShell and the Azure CLI wait for the API response to report on success or failure before they start the job. 

During this wait, the command won't return control to the command prompt. If you're scripting the migration experience, and you need the LRS start command to give back control immediately to continue with the rest of the script, you can run PowerShell as a background job with the `-AsJob` switch. For example:

```PowerShell
$lrsjob = Start-AzSqlInstanceDatabaseLogReplay <required parameters> -AsJob
```

When you start a background job, a job object returns immediately, even if the job takes an extended time to finish. You can continue to work in the session without interruption while the job runs. For details on running PowerShell as a background job, see the [PowerShell Start-Job](/powershell/module/microsoft.powershell.core/start-job#description) documentation.

Similarly, to start an Azure CLI command on Linux as a background process, use the ampersand (`&`) at the end of the LRS start command:

```CLI
az sql midb log-replay start <required parameters> &
```

## Monitor migration progress

[Az.SQL 4.0.0 and later](https://www.powershellgallery.com/packages/Az.Sql/4.0.0) provides a detailed progress report. Review [Managed Database Restore Details - Get](/rest/api/sql/managed-database-restore-details/get) for a sample output.  

To monitor ongoing migration progress through PowerShell, use the following command:

```PowerShell
Get-AzSqlInstanceDatabaseLogReplay -ResourceGroupName "ResourceGroup01" `
    -InstanceName "ManagedInstance01" `
    -Name "ManagedDatabaseName"
```

To monitor ongoing migration progress through the Azure CLI, use the following command:

```CLI
az sql midb log-replay show -g mygroup --mi myinstance -n mymanageddb
```

To track additional details on a failed request, use the PowerShell command [Get-AzSqlInstanceOperation](/powershell/module/az.sql/get-azsqlinstanceoperation) or use Azure CLI command [az sql mi op show](/cli/azure/sql/mi/op?view=azure-cli-latest#az-sql-mi-op-show).

## Stop the migration (optional)

If you need to stop the migration, use PowerShell or the Azure CLI. Stopping the migration deletes the restoring database on your managed instance, so resuming the migration won't be possible.

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

If you start LRS in continuous mode, ensure that your application and SQL Server workload have been stopped to prevent any new backup files from being generated. Ensure that the last backup from your SQL Server instance has been uploaded to your Azure Blob Storage account. Monitor the restore progress on your managed instance, and ensure that the last log-tail backup has been restored.

When the last log-tail backup has been restored on your managed instance, initiate the manual cutover to complete the migration. After the cutover has finished, the database becomes available for read and write access on the managed instance.

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


## Troubleshoot LRS issues

After you start LRS, use either of the following monitoring cmdlets to see the status of the ongoing operation:

* For PowerShell: `get-azsqlinstancedatabaselogreplay`
* For the Azure CLI: `az_sql_midb_log_replay_show`

To review details about a failed operation:

* For PowerShell: [Get-AzSqlInstanceOperation](/powershell/module/az.sql/get-azsqlinstanceoperation)
* For Azure CLI: [az sql mi op show](/cli/azure/sql/mi/op?view=azure-cli-latest#az-sql-mi-op-show)

If LRS fails to start after some time and you get an error, check for the most common issues:

- Does an existing database on your managed instance have the same name as the one you're trying to migrate from your SQL Server instance? Resolve this conflict by renaming one of the databases.
- Are the permissions granted for the SAS token Read and List _only_?
- Did you copy the SAS token for LRS after the question mark (`?`), with content that looks like `sv=2020-02-10...`? 
- Is the SAS token validity time appropriate for the time window of starting and completing the migration? There might be mismatches because of the different time zones used for your SQL Managed Instance deployment and the SAS token. Try regenerating the SAS token and extending the token validity of the time window before and after the current date.
- When starting multiple Log Replay restores in parallel targeting the same storage container, ensure that the same valid SAS token is provided for every restore operation. 
- Are the database name, resource group name, and managed instance name spelled correctly?
- If you started LRS in autocomplete mode, was a valid file name for the last backup file specified?
- Does the backup URI path contain keywords `backup` or `backups`? Rename the container or folders that are using `backup` or `backups` as these are reserved keywords.


## Next steps

- Learn more about [migrating to Azure SQL Managed Instance by using the link feature](managed-instance-link-feature-overview.md).
- Learn more about [migrating from SQL Server to Azure SQL Managed Instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).
- Learn more about the [differences between SQL Server and SQL Managed Instance](transact-sql-tsql-differences-sql-server.md).
- Learn more about [best practices to cost and size workloads migrated to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs).
