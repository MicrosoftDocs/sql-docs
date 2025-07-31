---
title: "SQL Server Backup to URL for Microsoft Azure Blob Storage"
description: Learn about the concepts, requirements, and components necessary for SQL Server to use the Microsoft Azure Blob Storage as a backup destination.
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, wiassaf, hudequei, randolphwest
ms.date: 06/16/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
  - devx-track-azurepowershell
  - build-2025
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# SQL Server backup to URL for Microsoft Azure Blob Storage

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article introduces the concepts, requirements, and components necessary to use Microsoft Azure Blob Storage as a backup destination. The backup and restore functionality are same or similar to when using DISK or TAPE, with a few differences. These differences and a few code examples are included in this article.

> [!TIP]  
> [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces backup to URL with managed identity. Review [Back up to URL with managed identity (preview) - SQL Server enabled by Azure Arc](../../sql-server/azure-arc/backup-to-url.md).

## Overview

SQL Server 2012 Service Pack 1 CU2 and SQL Server 2014 introduced the ability to back up to a URL pointed at Azure Blob Storage, using familiar T-SQL syntax to write backups securely to Azure storage. [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] introduced [File-Snapshot Backups for Database Files in Azure](file-snapshot-backups-for-database-files-in-azure.md) and security via shared-access signature (SAS) keys, a secure and simple way to authenticate certificates to Azure Storage security policy.

It's important to understand the components and the interaction between them to perform a backup to or restore from Microsoft Azure Blob Storage.

Creating an Azure Storage account within your Azure subscription is the first step in this process. This storage account is an administrative account that has full administrative permissions on all containers and objects created with the storage account. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can either use the Azure storage account name and its access key value to authenticate and write and read blobs to Microsoft Azure Blob Storage or use a Shared Access Signature token generated on specific containers granting it read and write rights. For more information on Azure Storage Accounts, see [About Azure Storage Accounts](/azure/storage/common/storage-account-create) and for more information about Shared Access Signatures, see [Shared Access Signatures, Part 1: Understanding the SAS Model](/azure/storage/common/storage-sas-overview). The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Credential stores this authentication information and is used during the backup or restore operations.

### Azure Storage and S3-compatible storage

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces the ability to write backups to S3-compatible object storage, with backup and restore functionality conceptually similar to working with Backup to URL using Azure Blob Storage as a backup device type. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] extends the BACKUP/RESTORE TO/FROM URL syntax by adding support for a new S3 connector using the REST API.

This article contains information on using Backup to URL for Azure Blob Storage. To learn more about using Backup to URL for S3-compatible storage, see [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md).

<a id="blockbloborpageblob"></a>

### Backup to Azure Storage block blob vs. page blob

There are two types of blobs that can be stored in Microsoft Azure Blob Storage: block and page blobs. For SQL Server 2016 and later, block blob is preferred.

If the storage key is used in the credential, page blob is used; if the Shared Access Signature is used, block blob is used.

Backup to block blob is only available in SQL Server 2016 or later version for backup to Azure Blob Storage. Backup to block blob instead of page blob if you're running SQL Server 2016 or later.

The main reasons are:

- Shared Access Signature is a safer way to authorize blob access compared to storage key.
- You can back up to multiple block blobs to get better backup and restore performance, and support larger database backup.
- [Block blob](https://azure.microsoft.com/pricing/details/storage/blobs/) is cheaper than [page blob](https://azure.microsoft.com/pricing/details/storage/page-blobs/).
- Customers that need to back up to page blobs via a proxy server need to use `backuptourl.exe`.

Taking a backup of a large database to Azure Blob Storage is subject to the limitations listed in [Azure SQL Managed Instance T-SQL differences, limitations, and known issues](/azure/sql-database/sql-database-managed-instance-transact-sql-information#backup).

If the database is too large, either:

- Use backup compression
   or
- Back up to multiple block blobs

#### Support on Linux, containers, and SQL Managed Instance enabled by Azure Arc

If the SQL Server instance is hosted on Linux, including:

- Stand-alone operating system
- Containers
- SQL Managed Instance enabled by Azure Arc
- Any other Linux-based environment

The only supported backup to URL for Azure Blob Storage is to block blobs, using the Shared Access Signature.

<a id="Blob"></a>

### Microsoft Azure Blob Storage 

**Storage Account:** The storage account is the starting point for all storage services. To access Microsoft Azure Blob Storage, first create an Azure storage account. For more information, see [Create a Storage Account](/azure/storage/common/storage-account-create)

**Container:** A container provides a grouping of a set of blobs, and can store an unlimited number of blobs. To write a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup to Azure Blob Storage, you must have at least the root container created. You can generate a Shared Access Signature token on a container and grant access to objects on a specific container only.

**Blob:** A file of any type and size. There are two types of blobs that can be stored in Azure Blob Storage: block and page blobs. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup can use either blob type depending upon the Transact-SQL syntax used. Blobs are addressable using the following URL format: https://\<storage account>.blob.core.windows.net/\<container>/\<blob>. For more information about Azure Blob Storage, see [Introduction to Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction). For more information about page and block blobs, see [Understanding Block and Page Blobs](/rest/api/storageservices/Understanding-Block-Blobs--Append-Blobs--and-Page-Blobs).  
:::image type="content" source="media/sql-server-backup-to-url/backup-to-cloud-blob-architecture.png" alt-text="Diagram of Azure Blob Storage accounts, containers, and blobs.":::

**Azure Snapshot:** A snapshot of an Azure blob taken at a point in time. For more information, see [Creating a Snapshot of a Blob](/rest/api/storageservices/Creating-a-Snapshot-of-a-Blob). [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup now supports Azure snapshot backups of database files stored in Azure Blob Storage. For more information, see [File-Snapshot Backups for Database Files in Azure](file-snapshot-backups-for-database-files-in-azure.md).

<a id="sqlserver"></a>

### SQL Server components

**URL:** A URL specifies a Uniform Resource Identifier (URI) to a unique backup file. The URL is used to provide the location and name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup file. The URL must point to an actual blob, not just a container. If the blob doesn't exist, it is created. If an existing blob is specified, BACKUP fails, unless the "WITH FORMAT" option is specified to overwrite the existing backup file in the blob.

Here's a sample URL value: `https://ACCOUNTNAME.blob.core.windows.net/<CONTAINER>/FILENAME.bak`.

> [!NOTE]  
> Backup to URL using HTTP is NOT supported.

**Credential:** A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is an object that is used to store authentication information required to connect to a resource outside of SQL Server. Here, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore processes use credential to authenticate to Azure Blob Storage and its container and blob objects. The Credential stores either the name of the storage account and the storage account **access key** values or container URL and its Shared Access Signature token. Once the credential is created, the syntax of the BACKUP/RESTORE statements determines the type of blob and the credential required.

For an example about how to create a Shared Access Signature, see [Create a Shared Access Signature](#create-a-shared-access-signature) examples later in this article and to create a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Credential, see [Create a credential](#create-a-credential) examples later in this article.

For more information about credentials, see [Credentials (Database Engine)](../security/authentication-access/credentials-database-engine.md).

For information on other examples where credentials are used, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).

<a id="security"></a>

## Azure immutable storage support

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces support for [Azure immutable storage](/azure/storage/blobs/immutable-storage-overview), which protects against ransomware attacks. Files written to immutable storage can't be modified or deleted, as defined by immutability. 

Typically, SQL Server backups are created in two steps. Initially, the `.bak` backup file is created with zeroes, and then the file is updated with data. Since file modification on immutable storage isn't allowed once the file is written and committed, the backup process now skips the initial step to create the backup file with zeroes. Instead, the entire backup is created in one step when written to block blobs.

During preview, [trace flag 3012](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3012) is required to enable immutable storage support for backups to URL. 

To use immutable storage with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] backup to URL, follow these steps: 

1. Configure [immutability for your Azure storage container](/azure/storage/blobs/immutable-policy-configure-container-scope). 
1. Enable trace flag 3012 for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance by running the following DBCC command:   
   `DBCC TRACEON(3012,-1)`.
1. Issue the [BACKUP](../../t-sql/statements/backup-transact-sql.md) to back up your database to the Azure storage container:   
   `BACKUP DATABASE [<Database>] TO URL = '<url>'`.

## Security for Azure Blob Storage

The following are security considerations and requirements when backing up to or restoring from Azure Blob Storage.

- When creating a container for Azure Blob Storage, we recommend that you set the access to **private**. Setting the access to private restricts the access to users or accounts able to provide the necessary information to authenticate to the Azure account.

    > [!IMPORTANT]  
    > [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] requires that either an Azure account name and access key authentication or a Shared Access Signature and access token be stored in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Credential. This information is used to authenticate to the Azure account when performing backup or restore operations.

    > [!WARNING]  
    > Azure Storage supports [disabling](/azure/storage/common/shared-key-authorization-prevent) Shared Key authorization for a storage account. If Shared Key authorization is disabled, SQL Server Backup To URL won't work.

- The user account that is used to issue BACKUP or RESTORE commands should be in the **db_backup operator** database role with **Alter any credential** permissions.

<a id="limitations"></a>

## Limitations of backup/restore to Azure Blob Storage

- SQL Server limits the maximum backup size supported using a page blob to 1 TB. The maximum backup size supported using block blobs is limited to approximately 200 GB (50,000 blocks * 4 MB MAXTRANSFERSIZE). Block blobs support striping to support substantially larger backup sizes - the limit is a maximum of 64 URLs, which results in the following formula: `64 stripes * 50,000 blocks * 4MB maxtransfersize = 12.8 TB`.

    > [!IMPORTANT]  
    > Although the maximum backup size supported by a single block blob is 200 GB, it's possible for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to write in smaller block sizes, which can lead [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to reach the 50,000 block limit before the entire backup is transferred. Stripe backups (even if they're smaller than 200 GB) to avoid the block limit, especially when if you use differential or uncompressed backups.

- You can issue backup or restore statements by using Transact-SQL, SMO, PowerShell cmdlets, or the SQL Server Management Studio Backup or Restore wizard.

- Backup to Azure Storage account only supports authentication with Shared Access Signature (SAS) tokens or storage account keys. All other authentication methods, including authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), aren't supported.

- Creating a logical device name isn't supported. So adding URL as a backup device using `sp_dumpdevice` or through SQL Server Management Studio isn't supported.

- Appending to existing backup blobs isn't supported. Backups to an existing blob can only be overwritten by using the `WITH FORMAT` option. However, when using file-snapshot backups (using the `WITH FILE_SNAPSHOT` argument), the `WITH FORMAT` argument isn't permitted to avoid leaving orphaned file-snapshots that were created with the original file-snapshot backup.

- Backup to multiple blobs in a single backup operation is only supported using block blobs and using a Shared Access Signature (SAS) token rather than the storage account key for the SQL Credential.

- Specifying `BLOCKSIZE` isn't supported for page blobs.
- Specifying `MAXTRANSFERSIZE` isn't supported page blobs.
- Specifying backupset options - `RETAINDAYS` and `EXPIREDATE` aren't supported.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has a maximum limit of 259 characters for a backup device name. The BACKUP TO URL consumes 36 characters for the required elements used to specify the URL `https://.blob.core.windows.net//.bak`, leaving 223 characters for account, container, and blob names put together.

- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions have a limit of 256 characters for Shared Access Signature (SAS) tokens, which limits the type of tokens that can be used (for example, User Delegation Key tokens aren't supported).

- If your server accesses Azure via a proxy server, you must use trace flag 1819 and then set the WinHTTP proxy configuration via one of the following methods:
  - The [proxycfg.exe](/windows/win32/winhttp/proxycfg-exe--a-proxy-configuration-tool) utility on Windows XP or Windows Server 2003 and earlier.
  - The [netsh.exe](/windows/win32/winsock/netsh-exe) utility on Windows Vista and Windows Server 2008 or later.

- [Immutable storage for Azure Blob Storage](/azure/storage/blobs/storage-blob-immutable-storage) isn't supported. Set the **Immutable Storage** policy to false.

- Backup to URL isn't supported to [premium storage](/azure/storage/blobs/storage-blob-block-blob-premium). 

## Supported arguments and statements in Azure Blob Storage

<a id="Support"></a>

### Support for backup/restore statements in Azure Blob Storage

| Backup/Restore Statement | Supported | Exceptions | Comments |
| --- | --- | --- | --- |
| BACKUP | Yes | BLOCKSIZE and MAXTRANSFERSIZE are supported for block blobs. They aren't supported for page blobs. | BACKUP to a block blob requires a Shared Access Signature saved in a SQL Server credential. BACKUP to page blob requires the storage account key saved in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential, and requires the WITH CREDENTIAL argument to be specified. |
| RESTORE | Yes | | Requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| RESTORE FILELISTONLY | Yes | | Requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| RESTORE HEADERONLY | Yes | | Requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| RESTORE LABELONLY | Yes | | Requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| RESTORE VERIFYONLY | Yes | | Requires a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential to be defined, and requires the WITH CREDENTIAL argument to be specified if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| RESTORE REWINDONLY | No | | |

For syntax and general information about backup statements, see [BACKUP](../../t-sql/statements/backup-transact-sql.md).

For syntax and general information about restore statements, see [RESTORE Statements](../../t-sql/statements/restore-statements-transact-sql.md).

### Support for backup arguments in Azure Blob Storage

| Argument | Supported | Exception | Comments |
| --- | --- | --- | --- |
| DATABASE | Yes | | |
| LOG | Yes | | |
| |
| TO (URL) | Yes | Unlike DISK and TAPE, URL doesn't support specifying or creating a logical name. | This argument is used to specify the URL path for the backup file. |
| MIRROR TO | Yes | | |
| **`WITH` options:** | | | |
| CREDENTIAL | Yes | | WITH CREDENTIAL is only supported when using BACKUP TO URL option to back up to Azure Blob Storage and only if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credential is defined using the storage account key as the secret |
| FILE_SNAPSHOT | Yes | | |
| ENCRYPTION | Yes | | When the `WITH ENCRYPTION` argument is specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] File-Snapshot Backup ensures that the entire database was TDE-encrypted before taking the backup and, if so, encrypts the file-snapshot backup file itself using the algorithm specified for TDE on the database. If all data in the database in the entire database isn't encrypted, the backup fails (for example, the encryption process isn't yet complete). |
| DIFFERENTIAL | Yes | | |
| COPY_ONLY | Yes | | |
| COMPRESSION&#124;NO_COMPRESSION | Yes | Not supported for file-snapshot backup | |
| DESCRIPTION | Yes | | |
| NAME | Yes | | |
| EXPIREDATE &#124; RETAINDAYS | No | | |
| NOINIT &#124; INIT | No | | Appending to blobs isn't possible. To overwrite a backup, use the `WITH FORMAT` argument. However, when using file-snapshot backups (using the `WITH FILE_SNAPSHOT` argument), the `WITH FORMAT` argument isn't permitted to avoid leaving orphaned file-snapshots that were created with the original backup. |
| NOSKIP &#124; SKIP | No | | |
| NOFORMAT &#124; FORMAT | Yes | | A backup taken to an existing blob fails unless `WITH FORMAT` is specified. The existing blob is overwritten when `WITH FORMAT` is specified. However, when using file-snapshot backups (using the `WITH FILE_SNAPSHOT` argument), the FORMAT argument isn't permitted to avoid leaving orphaned file-snapshots that were created with the original file-snapshot backup. However, when using file-snapshot backups (using the `WITH FILE_SNAPSHOT` argument), the `WITH FORMAT` argument isn't permitted to avoid leaving orphaned file-snapshots that were created with the original backup. |
| MEDIADESCRIPTION | Yes | | |
| MEDIANAME | Yes | | |
| BLOCKSIZE | Yes | Not supported for page blob. Supported for block blob. | Recommend BLOCKSIZE=65536 to optimize use of the 50,000 blocks allowed in a block blob. |
| BUFFERCOUNT | Yes | | |
| MAXTRANSFERSIZE | Yes | Not supported for page blob. Supported for block blob. | Default is 1048576. The value can range up to 4 MB in increments of 65536 bytes.<br />Recommend MAXTRANSFERSIZE=4194304 to optimize use of the 50,000 blocks allowed in a block blob. |
| NO_CHECKSUM &#124; CHECKSUM | Yes | | |
| STOP_ON_ERROR &#124; CONTINUE_AFTER_ERROR | Yes | | |
| STATS | Yes | | |
| REWIND &#124; NOREWIND | No | | |
| UNLOAD &#124; NOUNLOAD | No | | |
| NORECOVERY &#124; STANDBY | Yes | | |
| NO_TRUNCATE | Yes | | |

For more information about backup arguments, see [BACKUP](../../t-sql/statements/backup-transact-sql.md).

### Support for restore arguments in Azure Blob Storage

| Argument | Supported | Exceptions | Comments |
| --- | --- | --- | --- |
| DATABASE | Yes | | |
| LOG | Yes | | |
| FROM (URL) | Yes | | The FROM URL argument is used to specify the URL path for the backup file. |
| **WITH Options:** | | | |
| CREDENTIAL | Yes | | WITH CREDENTIAL is only supported when using RESTORE FROM URL option to restore from Microsoft Azure Blob Storage. |
| PARTIAL | Yes | | |
| RECOVERY &#124; NORECOVERY &#124; STANDBY | Yes | | |
| LOADHISTORY | Yes | | |
| MOVE | Yes | | |
| REPLACE | Yes | | |
| RESTART | Yes | | |
| RESTRICTED_USER | Yes | | |
| FILE | No | | |
| PASSWORD | Yes | | |
| MEDIANAME | Yes | | |
| MEDIAPASSWORD | Yes | | |
| BLOCKSIZE | Yes | | |
| BUFFERCOUNT | No | | |
| MAXTRANSFERSIZE | No | | |
| CHECKSUM &#124; NO_CHECKSUM | Yes | | |
| STOP_ON_ERROR &#124; CONTINUE_AFTER_ERROR | Yes | | |
| FILESTREAM | Yes | Not supported for snapshot backup | |
| STATS | Yes | | |
| REWIND &#124; NOREWIND | No | | |
| UNLOAD &#124; NOUNLOAD | No | | |
| KEEP_REPLICATION | Yes | | |
| KEEP_CDC | Yes | | |
| ENABLE_BROKER &#124; ERROR_BROKER_CONVERSATIONS &#124; NEW_BROKER | Yes | | |
| STOPAT &#124; STOPATMARK &#124; STOPBEFOREMARK | Yes | | |

For more information about Restore arguments, see [RESTORE Statements - Arguments](../../t-sql/statements/restore-statements-arguments-transact-sql.md).

<a id="BackupTaskSSMS"></a>

## Back up with SSMS

You can back up a database to URL through the Back Up task in SQL Server Management Studio using a SQL Server Credential.

> [!NOTE]  
> To create a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup, or overwrite an existing media set, you must use Transact-SQL, PowerShell, or C# rather than the Back Up task in SQL Server Management Studio.

The following steps describe the changes made to the Back Up Database task in SQL Server Management Studio to allow for backing up to Azure storage:

1. In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

1. Expand **Databases**, right-click the desired database, point to **Tasks**, and then select **Back Up...**.

1. On the **General** page in the **Destination** section the **URL** option is available in the **Back up to:** dropdown list. The **URL** option is used to create a backup to Microsoft Azure storage. Select **Add**, and the **Select Backup Destination** dialog box opens:

   1. **Azure storage container:** The name of the Microsoft Azure storage container to store the backup files. Select an existing container from the dropdown list or manually enter the container.

   1. **Shared Access Policy:** Enter the Shared Access Signature for a manually entered container. This field isn't available if an existing container was chosen.

   1. **Backup File:** Name of the backup file.

   1. **New Container:** Used to register an existing container that you don't have a Shared Access Signature for. See [Connect to a Microsoft Azure Subscription (Backup TO URL)](connect-to-a-microsoft-azure-subscription.md).

> [!NOTE]  
> **Add** supports multiple backup files and storage containers for a single media set.

When you select **URL** as the destination, certain options in the **Media Options** page are disabled. The following articles have more information on the Back Up Database dialog:

- [Back Up Database (General Page)](back-up-database-general-page.md)
- [Back Up Database (Media Options page)](back-up-database-media-options-page.md)
- [Back Up Database (Backup Options Page)](back-up-database-backup-options-page.md)
- [Create Credential - Authenticate to Azure Storage](create-credential-authenticate-to-azure-storage.md)

<a id="MaintenanceWiz"></a>

## Back up with maintenance plan

Similar to the backup task described previously, the Maintenance Plan Wizard in SQL Server Management Studio includes **URL** as one of the destination options, and other supporting objects required to back up to Azure storage like the SQL Credential. It has the same For more information, see the **Define Backup Tasks** section in [Using Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md#SSMSProcedure).

> [!NOTE]  
> To create a striped backup set, a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup, or a SQL credential using Shared Access token, you must use Transact-SQL, PowerShell, or C# rather than the Backup task in Maintenance Plan Wizard.

<a id="RestoreSSMS"></a>

## Restore with SSMS

The Restore Database task includes **URL** as a device to restore from. The following steps describe using the Restore task to restore from Azure Blob Storage:

1. Right-click **Databases** and select **Restore Database...**.

1. On the **General** page, select **Device** under the **Source** section.

1. Select the browse (...) button to open the **Select backup devices** dialog box.

1. Select **URL** from the **Backup media type:** dropdown list. Select **Add** to open the **Select a Backup File Location** dialog box.

   1. **Azure storage container:** The fully qualified name of the Microsoft Azure storage container that contains the backup files. Select an existing container from the dropdown list or manually enter the fully qualified container name.

   1. **Shared Access Signature:** Used to enter the Shared Access Signature for the designated container.

   1. **Add:** Used to register an existing container that you don't have a Shared Access Signature for. See [Connect to a Microsoft Azure Subscription (Backup TO URL)](connect-to-a-microsoft-azure-subscription.md).

   1. **OK:** SQL Server connects to Microsoft Azure storage using the SQL Credential information you provided and opens the **Locate Backup File in Microsoft Azure** dialog. The backup files residing in the storage container are displayed on this page. Select the file you want to use to restore and select **OK**. This takes you back to the **Select Backup Devices** dialog, and selecting **OK** on this dialog takes you back to the main **Restore** dialog, where you can complete the restore.

      - [Restore database (General page)](restore-database-general-page.md)
      - [Restore Database (Files Page)](restore-database-files-page.md)
      - [Restore Database (Options Page)](restore-database-options-page.md)

<a id="Examples"></a>

## Code examples

This section contains the following examples.

- [Create a Shared Access Signature](#create-a-shared-access-signature)
- [Create a credential](#create-a-credential)
- [Perform a full database backup](#perform-a-full-database-backup)
- [Restore to a point-in-time using STOPAT](#restore-to-a-point-in-time-using-stopat)

> [!NOTE]  
> For a tutorial on using SQL Server 2016 with Azure Blob Storage, see [Tutorial: Use Azure Blob Storage with SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)

<a id="SAS"></a>

### Create a Shared Access Signature

The following example creates Shared Access Signatures that can be used to create a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Credential on a newly created container. The script creates a Shared Access Signature that is associated with a Stored Access Policy. For more information, see [Shared Access Signatures, Part 1: Understanding the SAS Model](/azure/storage/common/storage-sas-overview). The script also writes the T-SQL command required to create the credential on SQL Server.

> [!NOTE]  
> The example requires Microsoft Azure PowerShell. For information about installing and using Azure PowerShell, see [How to install and configure Azure PowerShell](/powershell/azure/).  
> These scripts were verified using Azure PowerShell 5.1.15063.

**Shared Access Signature that is associated with a Stored Access Policy**

```powershell
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
$storageContext = New-AzStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $accountKeys[0].value

# Creates a new container in Azure Blob Storage
$container = New-AzStorageContainer -Context $storageContext -Name $containerName
$cbc = $container.CloudBlobContainer

# Sets up a Stored Access Policy and a Shared Access Signature for the new container
$policy = New-AzStorageContainerStoredAccessPolicy -Container $containerName -Policy $policyName -Context $storageContext -ExpiryTime $(Get-Date).ToUniversalTime().AddYears(10) -Permission "rwld"
$sas = New-AzStorageContainerSASToken -Policy $policyName -Context $storageContext -Container $containerName
Write-Host 'Shared Access Signature= '$($sas.TrimStart('?'))''

# Outputs the Transact SQL to the clipboard and to the screen to create the credential using the Shared Access Signature
Write-Host 'Credential T-SQL'
$tSql = "CREATE CREDENTIAL [{0}] WITH IDENTITY='Shared Access Signature', SECRET='{1}'" -f $cbc.Uri,$sas.TrimStart('?')
$tSql | clip
Write-Host $tSql
```

After successfully running the script, copy the `CREATE CREDENTIAL` command to a query tool, connect to an instance of SQL Server and run the command to create the credential with the Shared Access Signature.

<a id="credential"></a>

### Create a credential

The following examples create [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] credentials for authentication to Azure Blob Storage. Do one of the following.

1. **Using Shared Access Signature**

   If you ran the previous script to create the Shared Access Signature, copy the `CREATE CREDENTIAL` to a query editor connected to your instance of SQL Server and run the command.

   The following T-SQL is an example that creates the credential to use a Shared Access Signature.

   ```sql
   IF NOT EXISTS (SELECT *
                  FROM sys.credentials
                  WHERE name = 'https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>')
       CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>]
           WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = '<SAS_TOKEN>';
   ```

1. **Using storage account identity and access key**

   ```sql
   IF NOT EXISTS (SELECT *
                  FROM sys.credentials
                  WHERE name = '<mycredentialname>')
       CREATE CREDENTIAL [<mycredentialname>]
           WITH IDENTITY = '<mystorageaccountname>', SECRET = '<mystorageaccountaccesskey>';
   ```

<a id="complete"></a>

### Perform a full database backup

The following examples perform a full database backup of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database to Azure Blob Storage. Use one of the following samples:

1. **To URL using Shared Access Signature**

   ```sql
   BACKUP DATABASE AdventureWorks2022
       TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2022.bak';
   GO
   ```

1. **To URL using storage account identity and access key**

   ```sql
   BACKUP DATABASE AdventureWorks2022
   TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2022.bak'
   WITH CREDENTIAL = '<mycredentialname>',
   COMPRESSION, STATS = 5;
   GO
   ```

<a id="PITR"></a>

### Restore to a point-in-time using STOPAT

The following example restores the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database to its state at a point in time, and shows a restore operation.

**From URL using Shared Access Signature**

```sql
RESTORE DATABASE AdventureWorks2022 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2022_2015_05_18_16_00_00.bak'
WITH
MOVE 'AdventureWorks2022_data' TO 'C:\Program Files\Microsoft SQL Server\<myinstancename>\MSSQL\DATA\AdventureWorks2022.mdf',
MOVE 'AdventureWorks2022_log' TO 'C:\Program Files\Microsoft SQL Server\<myinstancename>\MSSQL\DATA\AdventureWorks2022.ldf',
NORECOVERY, REPLACE, STATS = 5;
GO

RESTORE LOG AdventureWorks2022 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2022_2015_05_18_18_00_00.trn'
WITH RECOVERY, STOPAT = 'May 18, 2015 5:35 PM';
GO
```

## Related content

- [SQL Server back up to URL for Microsoft Azure Blob Storage best practices and troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)
- [Back up and restore: System databases (SQL Server)](back-up-and-restore-of-system-databases-sql-server.md)
- [Tutorial: Use Azure Blob Storage with SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)
