---
title: "SQL Server Backup to URL | Microsoft Docs"
ms.custom: ""
ms.date: "11/17/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 11be89e9-ff2a-4a94-ab5d-27d8edf9167d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Backup to URL
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  This topic introduces the concepts, requirements and components necessary to use the Microsoft Azure Blob storage service as a backup destination. The backup and restore functionality are same or similar to when using DISK or TAPE, with a few differences. These differences and a few code examples are included in this topic.  
  
## Requirements, Components, and Concepts  
 **In this section:**  
  
-   [Security](#security)  
  
-   [Introduction to Key Components and Concepts](#intorkeyconcepts)  
  
-   [Microsoft Azure Blob storage service](#Blob)  
  
-   [SQL Server Components](#sqlserver)  
  
-   [Limitations](#limitations)  
  
-   [Support for Backup/Restore Statements](#Support)  
  
-   [Using Backup Task in SQL Server Management Studio](../../relational-databases/backup-restore/sql-server-backup-to-url.md#BackupTaskSSMS)  
  
-   [SQL Server Backup to URL Using Maintenance Plan Wizard](../../relational-databases/backup-restore/sql-server-backup-to-url.md#MaintenanceWiz)  
  
-   [Restoring from Windows Azure storage Using SQL Server Management Studio](../../relational-databases/backup-restore/sql-server-backup-to-url.md#RestoreSSMS)  
  
###  <a name="security"></a> Security  
 The following are security considerations and requirements when backing up to or restoring from the Microsoft Azure Blob storage service.  
  
-   When creating a container for the Microsoft Azure Blob storage service, we recommend that you set the access to **private**. Setting the access to private restricts the access to users or accounts able to provide the necessary information to authenticate to the Windows Azure account.  
  
    > [!IMPORTANT]  
    >  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that either a Windows Azure account name and access key authentication or a Shared Access Signature and access token be stored in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Credential. This information is used to authenticate to the Windows Azure account when performing backup or restore operations.  
  
-   The user account that is used to issue BACKUP or RESTORE commands should be in the **db_backup operator** database role with **Alter any credential** permissions.  
  
###  <a name="intorkeyconcepts"></a> Introduction to Key Components and Concepts  
 The following two sections introduce the Microsoft Azure Blob storage service, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components used when backing up to or restoring from the Microsoft Azure Blob storage service. It is important to understand the components and the interaction between them to do a backup to or restore from the Microsoft Azure Blob storage service.  
  
 Creating a Windows Azure Storage account within your Azure subscription is the first step in this process. This storage account is an administrative account that has full administrative permissions on all containers and objects created with the storage account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can either use the Windows Azure storage account name and its access key value to authenticate and write and read blobs to the Microsoft Azure Blob storage service or use a Shared Access Signature token generated on specific containers granting it read and write rights. For more information on Azure Storage Accounts, see [About Azure Storage Accounts](https://azure.microsoft.com/documentation/articles/storage-create-storage-account/) and for more information about Shared Access Signatures, see [Shared Access Signatures, Part 1: Understanding the SAS Model](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-1/). The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Credential stores this authentication information and is used during the backup or restore operations.  
  
###  <a name="blockbloborpageblob"></a> Backup to block blob vs. page blob 
 There are two types of blobs that can be stored in the Microsoft Azure Blob storage service: block and page blobs. SQL Server backup can use either blob type depending upon the Transact-SQL syntax used: If the storage key is used in the credential, page blob will be used; if the Shared Access Signature is used, block blob will be used.
 
 Backup to block blob is only available in SQL Server 2016 or later version. We recommend you to backup to block blob instead of page block if you are running SQL Server 2016 or later version. The main reasons are:
- Shared Access Signature is a safer way to authorize blob access compared to storage key.
- You can backup to multiple block blobs to get better backup and restore performance, and support larger database backup.
- [Block blob](https://azure.microsoft.com/pricing/details/storage/blobs/) is cheaper than [page blob](https://azure.microsoft.com/pricing/details/storage/page-blobs/). 

When you backup to block blob, the maximum block size you can specify is 4MB. The maximum size of a single block blob file is 4MB * 50000 = 195GB. If your database is larger than 195GB, we recommend you:
- Use backup compression
- Backup to multiple block blobs

###  <a name="Blob"></a> Microsoft Azure Blob storage service  
 **Storage Account:** The storage account is the starting point for all storage services. To access the Microsoft Azure Blob storage service, first create a Windows Azure storage account. For more information, see [Create a Storage Account](https://azure.microsoft.com/documentation/articles/storage-create-storage-account/)  
  
 **Container:** A container provides a grouping of a set of blobs, and can store an unlimited number of blobs. To write a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup to the Microsoft Azure Blob storage service, you must have at least the root container created. You can generate a Shared Access Signature token on a container and grant access to objects on a specific container only.  
  
 **Blob:** A file of any type and size. There are two types of blobs that can be stored in the Microsoft Azure Blob storage service: block and page blobs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup can use either blob type depending upon the Transact-SQL syntax used. Blobs are addressable using the following URL format: https://\<storage account>.blob.core.windows.net/\<container>/\<blob>. For more information about the Microsoft Azure Blob storage service, see [How to use the Blob Storage from .NET](https://www.windowsazure.com/develop/net/how-to-guides/blob-storage/). For more information about page and block blobs, see [Understanding Block and Page Blobs](https://msdn.microsoft.com/library/windowsazure/ee691964.aspx).  
  
 ![Azure Blob Storage](../../relational-databases/backup-restore/media/backuptocloud-blobarchitecture.gif "Azure Blob Storage")  
  
 **Azure Snapshot:** A snapshot of an Azure blob taken at a point in time. For more information, see [Creating a Snapshot of a Blob](https://msdn.microsoft.com/library/azure/hh488361.aspx). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup now supports Azure snapshot backups of database files stored in the Microsoft Azure Blob storage service. For more information, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
###  <a name="sqlserver"></a> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Components  
 **URL:** A URL specifies a Uniform Resource Identifier (URI) to a unique backup file. The URL is used to provide the location and name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup file. The URL must point to an actual blob, not just a container. If the blob does not exist, it is created. If an existing blob is specified, BACKUP fails, unless the "WITH FORMAT" option is specified to overwrite the existing backup file in the blob.  
  
 Here is a sample URL value: http[s]://ACCOUNTNAME.blob.core.windows.net/\<CONTAINER>/\<FILENAME.bak>. HTTPS is not required, but is recommended.  
  
 **Credential:** A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is an object that is used to store authentication information required to connect to a resource outside of SQL Server. Here, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore processes use credential to authenticate to the Microsoft Azure Blob storage service and its container and blob objects. The Credential stores either the name of the storage account and the storage account **access key** values or container URL and its Shared Access Signature token. Once the credential is created, the syntax of the BACKUP/RESTORE statements determines the type of blob and the credential required.  
  
 For an example about how to create a Shared Access Signature, see [Create a Shared Access Signature](../../relational-databases/backup-restore/sql-server-backup-to-url.md#SAS) examples later in this topic and to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Credential, see [Create a Credential](../../relational-databases/backup-restore/sql-server-backup-to-url.md#credential) examples later in this topic.  
  
 For general information about credentials, see [Credentials](../security/authentication-access/credentials-database-engine.md)  
  
 For information on other examples where credentials are used, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).  
  
###  <a name="limitations"></a> Limitations  
  
-   Backup to premium storage is not supported.  
  
-   SQL Server limits the maximum backup size supported using a page blob to 1 TB. The maximum backup size supported using block blobs is limited to approximately 200 MB (50,000 blocks * 4MB MAXTRANSFERSIZE). Block blobs support striping to support substantially larger backup sizes.  
  
-   You can issue backup or restore statements by using TSQL, SMO, PowerShell cmdlets, SQL Server Management Studio Backup or Restore wizard.   
  
-   Creating a logical device name is not supported. So adding URL as a backup device using sp_dumpdevice or through SQL Server Management Studio is not supported.  
  
-   Appending to existing backup blobs is not supported. Backups to an existing blob can only be overwritten by using the **WITH FORMAT** option. However, when using file-snapshot backups (using the **WITH FILE_SNAPSHOT** argument), the **WITH FORMAT** argument is not permitted to avoid leaving orphaned file-snapshots that were created with the original file-snapshot backup.  
  
-   Backup to multiple blobs in a single backup operation is only supported using block blobs and using a Shared Access Signature (SAS) token rather than the storage account key for the SQL Credential.  
  
-   Specifying **BLOCKSIZE** is not supported for page blobs. 
  
-   Specifying **MAXTRANSFERSIZE** is not supported page blobs. 
  
-   Specifying backupset options - **RETAINDAYS** and **EXPIREDATE** are not supported.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has a maximum limit of 259 characters for a backup device name. The BACKUP TO URL consumes 36 characters for the required elements used to specify the URL - 'https://.blob.core.windows.net//.bak', leaving 223 characters for account, container, and blob names put together.  
  
###  <a name="Support"></a> Support for Backup/Restore Statements  
  
|Backup/Restore Statement|Supported|Exceptions|Comments|
|-|-|-|-|
|BACKUP|Y|BLOCKSIZE and MAXTRANSFERSIZE are supported for block blobs. They are not supported for page blobs. | BACKUP to a block blob requires a Shared Access Signature saved in a SQL Server credential. BACKUP to page blob requires the storage account key saved in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential, and requires the WITH CREDENTIAL argument to be specified.|  
|RESTORE|Y||Requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|RESTORE FILELISTONLY|Y||Requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|RESTORE HEADERONLY|Y||Requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|RESTORE LABELONLY|Y||Requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|RESTORE VERIFYONLY|Y||Requires a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|RESTORE REWINDONLY|−|||  
  
 For syntax and general information about backup statements, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).  
  
 For syntax and general information about restore statements, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
### Support for Backup Arguments  

|Argument|Supported|Exception|Comments|  
|-|-|-|-|  
|DATABASE|Y|||  
|LOG|Y|||  
||  
|TO (URL)|Y|Unlike DISK and TAPE, URL does not support specifying or creating a logical name.|This argument is used to specify the URL path for the backup file.|  
|MIRROR TO|Y|||  
|**WITH OPTIONS:**||||  
|CREDENTIAL|Y||WITH CREDENTIAL is only supported when using BACKUP TO URL option to back up to the Microsoft Azure Blob storage service and only if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret|  
|FILE_SNAPSHOT|Y|||  
|ENCRYPTION|Y||When the **WITH ENCRYPTION** argument is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] File-Snapshot Backup ensures that the entire database was TDE-encrypted before taking the backup and, if so, encrypts the file-snapshot backup file itself using the algorithm specified for TDE on the database. If all data in the database in the entire database is not encrypted, the backup will fail (e.g. the encryption process is not yet complete).|  
|DIFFERENTIAL|Y|||  
|COPY_ONLY|Y|||  
|COMPRESSION&#124;NO_COMPRESSION|Y|Not supported for file-snapshot backup||  
|DESCRIPTION|Y|||  
|NAME|Y|||  
|EXPIREDATE &#124; RETAINDAYS|−|||  
|NOINIT &#124; INIT|−||Appending to blobs is not possible. To overwrite a backup use the **WITH FORMAT** argument. However, when using file-snapshot backups (using the **WITH FILE_SNAPSHOT** argument), the **WITH FORMAT** argument is not permitted to avoid leaving orphaned file-snapshots that were created with the original backup.|  
|NOSKIP &#124; SKIP|−|||  
|NOFORMAT &#124; FORMAT|Y||A backup taken to an existing blob fails unless **WITH FORMAT** is specified. The existing blob is overwritten when **WITH FORMAT** is specified. However, when using file-snapshot backups (using the **WITH FILE_SNAPSHOT** argument), the FORMAT argument is not permitted to avoid leaving orphaned file-snapshots that were created with the original file-snapshot backup. However, when using file-snapshot backups (using the **WITH FILE_SNAPSHOT** argument), the **WITH FORMAT** argument is not permitted to avoid leaving orphaned file-snapshots that were created with the original backup.|  
|MEDIADESCRIPTION|Y|||  
|MEDIANAME|Y|||  
|BLOCKSIZE|Y|Not supported for page blob. Supported for block blob.| Recommend BLOCKSIZE=65536 to optimize use of the 50,000 blocks allowed in a block blob. |  
|BUFFERCOUNT|Y|||  
|MAXTRANSFERSIZE|Y|Not supported for page blob. Supported for block blob.| Default is 1048576. The value can range up to 4 MB in increments of 65536 bytes.</br> Recommend MAXTRANSFERSIZE=4194304 to optimize use of the 50,000 blocks allowed in a block blob. |  
|NO_CHECKSUM &#124; CHECKSUM|Y|||  
|STOP_ON_ERROR &#124; CONTINUE_AFTER_ERROR|Y|||  
|STATS|Y|||  
|REWIND &#124; NOREWIND|−|||  
|UNLOAD &#124; NOUNLOAD|−|||  
|NORECOVERY &#124; STANDBY|Y|||  
|NO_TRUNCATE|Y|||  
  
 For more information about backup arguments, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).  
  
### Support for Restore Arguments  
  
|Argument|Supported|Exceptions|Comments|  
|-|-|-|-|  
|DATABASE|Y|||  
|LOG|Y|||  
|FROM (URL)|Y||The FROM URL argument is used to specify the URL path for the backup file.|  
|**WITH Options:**||||  
|CREDENTIAL|Y||WITH CREDENTIAL is only supported when using RESTORE FROM URL option to restore from Microsoft Azure Blob storage service.|  
|PARTIAL|Y|||  
|RECOVERY &#124; NORECOVERY &#124; STANDBY|Y|||  
|LOADHISTORY|Y|||  
|MOVE|Y|||  
|REPLACE|Y|||  
|RESTART|Y|||  
|RESTRICTED_USER|Y|||  
|FILE|−|||  
|PASSWORD|Y|||  
|MEDIANAME|Y|||  
|MEDIAPASSWORD|Y|||  
|BLOCKSIZE|Y|||  
|BUFFERCOUNT|−|||  
|MAXTRANSFERSIZE|−|||  
|CHECKSUM &#124; NO_CHECKSUM|Y|||  
|STOP_ON_ERROR &#124; CONTINUE_AFTER_ERROR|Y|||  
|FILESTREAM|Y|Not supported for snapshot backup||  
|STATS|Y|||  
|REWIND &#124; NOREWIND|−|||  
|UNLOAD &#124; NOUNLOAD|−|||  
|KEEP_REPLICATION|Y|||  
|KEEP_CDC|Y|||  
|ENABLE_BROKER &#124; ERROR_BROKER_CONVERSATIONS &#124; NEW_BROKER|Y|||  
|STOPAT &#124; STOPATMARK &#124; STOPBEFOREMARK|Y|||  
  
 For more information about Restore arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
##  <a name="BackupTaskSSMS"></a> Using Back Up Task in SQL Server Management Studio  
You can back up a database to URL through the Back Up task in SQL Server Management Studio using a SQL Server Credential.  
  
> [!NOTE]  
>  To create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup, or overwrite an existing media set, you must use Transact-SQL, Powershell or C# rather than the Back Up task in SQL Server Management Studio.  
  
 The following steps describe the changes made to the Back Up Database task in SQL Server Management Studio to allow for backing up to Windows Azure storage:  
  
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click the desired database, point to **Tasks**, and then click **Back Up...**.
  
3.  On the **General** page in the **Destination** section the **URL** option is available in the **Back up to:** drop-down list.  The **URL** option is used to create a backup to Microsoft Azure storage. Click **Add** and the **Select Backup Destination** dialog box will open:
   
    1.  **Azure storage container:** The name of the Microsoft Azure storage container to store the backup files.  Select an existing container from the drop-down list or manually enter the container. 
  
    2.  **Shared Access Policy:** Enter the shared access signature for a manually entered container.  This field is not available if an existing container was chosen. 
  
    3.  **Backup File:** Name of the backup file.
    
    4.  **New Container:** Used to register an existing container that you do not have a shared access signature for.  See [Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md).
  
> [!NOTE] 
>  **Add** supports multiple backup files and storage containers for a single media set.
  
 When you select **URL** as the destination, certain options in the **Media Options** page are disabled.  The following topics have more information on the Back Up Database dialog:  
  
 [Back Up Database &#40;General Page&#41;](../../relational-databases/backup-restore/back-up-database-general-page.md)  
  
 [Back Up Database &#40;Media Options Page&#41;](../../relational-databases/backup-restore/back-up-database-media-options-page.md)  
  
 [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md)  
  
 [Create Credential - Authenticate to Azure Storage](../../relational-databases/backup-restore/create-credential-authenticate-to-azure-storage.md)  
  
##  <a name="MaintenanceWiz"></a> SQL Server Backup to URL Using Maintenance Plan Wizard  
 Similar to the backup task described previously, the Maintenance Plan Wizard in SQL Server Management Studio includes **URL** as one of the destination options, and other supporting objects required to backup to Windows Azure storage like the SQL Credential. It has the same For more information, see the **Define Backup Tasks** section in [Using Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md#SSMSProcedure).  
  
> [!NOTE]  
>  To create a striped backup set, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup, or a SQL credential using Shared Access token, you must use Transact-SQL, Powershell or C# rather than the Backup task in Maintenance Plan Wizard.  
  
##  <a name="RestoreSSMS"></a> Restoring from Microsoft Azure storage Using SQL Server Management Studio  
The Restore Database task includes **URL** as a device to restore from.  The following steps describe using the Restore task to restore from the Microsoft Azure Blob storage service: 
  
1.  Right-click **Databases** and select **Restore Database...**. 
  
2.  On the **General** page, select **Device** under the **Source** section.
  
3.  Click the browse (...) button to open the **Select backup devices** dialog box. 

4.  Select **URL** from the **Backup media type:** drop-down list.  Click **Add** to open the **Select a Backup File Location** dialog box.

    1.  **Azure storage container:** The fully qualified name of the Microsoft Azure storage container which contains the backup files.  Select an existing container from the drop-down list or manually enter the fully qualified container name.
      
    2.  **Shared Access Signature:**  Used to enter the shared access signature for the designated container.
      
    3.  **Add:**  Used to register an existing container that you do not have a shared access signature for.  See [Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md).
      
    4.  **OK:**	SQL Server connects to Microsoft Azure storage using the SQL Credential information you provided and opens the **Locate Backup File in Microsoft Azure** dialog. The backup files residing in the storage container are displayed on this page. Select the file you want to use to restore and click **OK**. This takes you back to the **Select Backup Devices** dialog, and clicking **OK** on this dialog takes you back to the main **Restore** dialog where you will be able complete the restore. 
  
     [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md)  
  
     [Restore Database &#40;Files Page&#41;](../../relational-databases/backup-restore/restore-database-files-page.md)  
  
     [Restore Database &#40;Options Page&#41;](../../relational-databases/backup-restore/restore-database-options-page.md)  
  
##  <a name="Examples"></a> Code Examples  
 This section contains the following examples.  
  
-   [Create a Credential](#credential)  
  
-   [Backing up a complete database](#complete)  
    
-   [Restoring to a point-in-time using STOPAT](#PITR)  
  
> [!NOTE]  
>  For a tutorial on using SQL Server 2016 with the Microsoft Azure Blob storage service, see [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)  
  
###  <a name="SAS"></a> Create a Shared Access Signature  
 The following example creates Shared Access Signatures that can be used to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Credential on a newly created container. The script  creates a Shared Access Signature that is associated with a Stored Access Policy. For more information, see [Shared Access Signatures, Part 1: Understanding the SAS Model](https://azure.microsoft.com/documentation/articles/storage-dotnet-shared-access-signature-part-1/). The script also writes the T-SQL command required to create the credential on SQL Server. 

> [!NOTE] 
> The example requires Microsoft Azure Powershell. For information about installing and using Azure Powershell, see [How to install and configure Azure PowerShell](https://azure.microsoft.com/documentation/articles/powershell-install-configure/).  
> These scripts were verified using Azure PowerShell 5.1.15063. 


**Shared Access Signature that is associated with a Stored Access Policy**  
  
```Powershell  
# Define global variables for the script  
$prefixName = '<a prefix name>'  # used as the prefix for the name for various objects  
$subscriptionName='<your subscription name>'   # the name of subscription name you will use  
$locationName = '<a data center location>'  # the data center region you will use  
$storageAccountName= $prefixName + 'storage' # the storage account name you will create or use  
$containerName= $prefixName + 'container'  # the storage container name to which you will attach the SAS policy with its SAS token  
$policyName = $prefixName + 'policy' # the name of the SAS policy  


# Set a variable for the name of the resource group you will create or use  
$resourceGroupName=$prefixName + 'rg'   

# adds an authenticated Azure account for use in the session   
Connect-AzAccount

# set the tenant, subscription and environment for use in the rest of   
Set-AzContext -SubscriptionName $subscriptionName   

# create a new resource group - comment out this line to use an existing resource group  
New-AzResourceGroup -Name $resourceGroupName -Location $locationName   

# Create a new ARM storage account - comment out this line to use an existing ARM storage account  
New-AzStorageAccount -Name $storageAccountName -ResourceGroupName $resourceGroupName -Type Standard_RAGRS -Location $locationName   

# Get the access keys for the ARM storage account  
$accountKeys = Get-AzStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName  

# Create a new storage account context using an ARM storage account  
$storageContext = New-AzureStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $accountKeys[0].value 

# Creates a new container in blob storage  
$container = New-AzureStorageContainer -Context $storageContext -Name $containerName  
$cbc = $container.CloudBlobContainer  

# Sets up a Stored Access Policy and a Shared Access Signature for the new container  
$policy = New-AzureStorageContainerStoredAccessPolicy -Container $containerName -Policy $policyName -Context $storageContext -ExpiryTime $(Get-Date).ToUniversalTime().AddYears(10) -Permission "rwld"
$sas = New-AzureStorageContainerSASToken -Policy $policyName -Context $storageContext -Container $containerName


# Gets the Shared Access Signature for the policy  
$policy = new-object 'Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy'  
$sas = $cbc.GetSharedAccessSignature($policy, $policyName)  
Write-Host 'Shared Access Signature= '$($sas.Substring(1))''  

# Outputs the Transact SQL to the clipboard and to the screen to create the credential using the Shared Access Signature  
Write-Host 'Credential T-SQL'  
$tSql = "CREATE CREDENTIAL [{0}] WITH IDENTITY='Shared Access Signature', SECRET='{1}'" -f $cbc.Uri,$sas.Substring(1)   
$tSql | clip  
Write-Host $tSql  
```  

After successfully running the script, copy the `CREATE CREDENTIAL` command to a query tool, connect to an instancance of SQL Server and run the command to create the credential with the Shared Access Signature. 

###  <a name="credential"></a> Create a Credential  
 The following examples create [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credentials for authentication to the Microsoft Azure Blob storage service. Do one of the following. 
  
1.  **Using Shared Access Signature**  

   If you ran the script to create the Shared Access Signature above, copy the `CREATE CREDENTIAL` to a query editor connected to your instance of SQL Server and run the command. 

   The following T-SQL is an example that creates the credential to use a Shared Access Signature. 

   ```sql  
   IF NOT EXISTS  
   (SELECT * FROM sys.credentials   
   WHERE name = 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>')  
   CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>] 
      WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
      SECRET = '<SAS_TOKEN>';  
   ```  
  
2.  **Using storage account identity and access key**  
  
   ```sql 
   IF NOT EXISTS  
   (SELECT * FROM sys.credentials   
   WHERE name = '<mycredentialname>')  
   CREATE CREDENTIAL [<mycredentialname>] WITH IDENTITY = '<mystorageaccountname>'  
   ,SECRET = '<mystorageaccountaccesskey>';  
   ```  
  
###  <a name="complete"></a> Perform a full database backup  
 The following examples perform a full database backup of the AdventureWorks2016 database to the Microsoft Azure Blob storage service. Do one of the following:   
  
  
2.  **To URL using Shared Access Signature**  
  
   ```sql  
   BACKUP DATABASE AdventureWorks2016   
   TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016.bak';  
   GO   
   ```  

1.  **To URL using storage account identity and access key**  
  
   ```sql
   BACKUP DATABASE AdventureWorks2016  
   TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016.bak'   
         WITH CREDENTIAL = '<mycredentialname>'   
        ,COMPRESSION  
        ,STATS = 5;  
   GO   
   ```  
  

  
  
###  <a name="PITR"></a> Restoring to a point-in-time using STOPAT  
 The following example restores the AdventureWorks2016 sample database to its state at a point in time, and shows a restore operation.  
  
1.  **From URL using Shared Access Signature**  
  
   ```sql
   RESTORE DATABASE AdventureWorks2016 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_2015_05_18_16_00_00.bak'   
   WITH MOVE 'AdventureWorks2016_data' to 'C:\Program Files\Microsoft SQL Server\<myinstancename>\MSSQL\DATA\AdventureWorks2016.mdf'  
   ,MOVE 'AdventureWorks2016_log' to 'C:\Program Files\Microsoft SQL Server\<myinstancename>\MSSQL\DATA\AdventureWorks2016.ldf'  
   ,NORECOVERY  
   ,REPLACE  
   ,STATS = 5;  
   GO   
  
   RESTORE LOG AdventureWorks2016 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_2015_05_18_18_00_00.trn'   
   WITH   
   RECOVERY   
   ,STOPAT = 'May 18, 2015 5:35 PM'   
   GO  
   ```  
  
## See Also  
 [SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)   
 [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)   
 [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)  
  
  
