---
title: "Back up to URL best practices & troubleshooting for Microsoft Azure Blob Storage"
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to Azure Blob Storage.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 11/06/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
---
# SQL Server back up to URL for Microsoft Azure Blob Storage best practices and troubleshooting

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article includes best practices and troubleshooting tips for SQL Server backup and restores to Microsoft Azure Blob Storage.

For more information about using Azure Blob Storage for SQL Server backup or restore operations, see:

- [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)

- [Tutorial: SQL Server Backup and Restore to Azure Blob Storage](../../relational-databases/tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)

## Manage backups

The following list includes general recommendations to manage backups:

- Unique file name for every backup is recommended to prevent accidentally overwriting the blobs.

- When creating a container, you should set the access level to **private**, so only users or accounts that can provide the required authentication information can read or write the blobs in the container.

- For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running in an Azure Virtual Machine, use a storage account in the same region as the virtual machine to avoid data transfer costs between regions. Using the same region also ensures optimal performance for backup and restore operations.

- Failed backup activity can result in an invalid backup file. We recommend periodic identification of failed backups and deleting the blob files. For more information, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md).

- Using the `WITH COMPRESSION` option during backup can minimize your storage costs and storage transaction costs. It can also decrease the time taken to complete the backup process.

- Set `MAXTRANSFERSIZE` and `BLOCKSIZE` arguments as recommended at [SQL Server Backup to URL](sql-server-backup-to-url.md).

- SQL Server is agnostic to the type of storage redundancy used. Backing up to page blobs and block blobs is supported for every storage redundancy (LRS/ZRS/GRS/RA-GRS/RA-GZRS, etc.).

## Handle large files

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup operation uses multiple threads to optimize data transfer to Azure Blob Storage. However the performance depends on various factors, such as ISV bandwidth and size of the database. If you plan to back up large databases or filegroups from an on-premises SQL Server database, you should do some throughput testing first. Azure [SLA for Storage](https://azure.microsoft.com/support/legal/sla/storage/v1_0/) has maximum processing times for blobs that you can take into consideration.

Using the `WITH COMPRESSION` option as recommended in the [Manage Backups](#manage-backups) section, is important when backing up large files.

## <a id="troubleshooting-backup-to-or-restore-from-url"></a> Troubleshoot backup to or restore from URL

Following are some quick ways to troubleshoot errors when backing up to or restoring from the Azure Blob Storage.

To avoid errors due to unsupported options or limitations, review the list of limitations, and support for `BACKUP` and `RESTORE` commands information, in the article [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

#### Initialization failed

Parallel backups to the same blob cause one of the backups to fail with an **Initialization failed** error.

- In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, block blob is preferred for Backup to URL.

- If you're using page blobs with `BACKUP TO URL`, you can use [Trace Flag 3051](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3051) to turn on logging to a specific error log with the following format in: `BackupToUrl-\<instname>-\<dbname>-action-\<PID>.log`, where `\<action>` is one of the following options:

  - `DB`
  - `FILELISTONLY`
  - `LABELONLY`
  - `HEADERONLY`
  - `VERIFYONLY`

You can also find information by reviewing the Windows Event Viewer, under **Application logs** with the name `SQLBackupToUrl`.

#### The request couldn't be performed because of an I/O device error.

Consider `COMPRESSION`, `MAXTRANSFERSIZE`, `BLOCKSIZE`, and multiple URL arguments when backing up large databases. See [Backing up a VLDB to Azure Blob Storage](/archive/blogs/sqlcat/backing-up-a-vldb-to-azure-blob-storage).

The error:

```output
Msg 3202, Level 16, State 1, Line 1
Write on "https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak" failed:
1117(The request could not be performed because of an I/O device error.)
Msg 3013, Level 16, State 1, Line 1
BACKUP DATABASE is terminating abnormally.
```

An example resolution:

```sql
BACKUP DATABASE TestDb
TO URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak',
URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_1.bak',
URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_2.bak'
WITH COMPRESSION, MAXTRANSFERSIZE = 4194304, BLOCKSIZE = 65536;
```

#### Message Filemark on device isn't aligned

When restoring from a compressed backup, you might see the following error:

```output
SqlException 3284 occurred. Severity: 16 State: 5
Message Filemark on device 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak' is not aligned.
Reissue the Restore statement with the same block size used to create the backupset: '65536' looks like a possible value.
```

To solve this error, reissue the `RESTORE` statement with `BLOCKSIZE = 65536` specified.

#### Failed backup activity can result in blobs with active leases

Error during backup due to blobs that have active lease on them:
`Failed backup activity can result in blobs with active leases.`

If a backup statement is reattempted, backup operation might fail with an error similar to the following output:

```output
Backup to URL received an exception from the remote endpoint. Exception Message:
The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request.
```

If a restore statement is attempted on a backup blob file that has an active lease, the restore operation fails with an error similar to the following:

`Exception Message: The remote server returned an error: (409) Conflict..`

When such error occurs, the blob files need to be deleted. For more information on this scenario and how to correct this problem, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md).

#### OS error 50: The request isn't supported

When backing up a database, you might see error `Operating system error 50(The request is not supported)` for the following reasons:

- The specified storage account isn't General Purpose V1/V2.
- The SAS token had a `?` symbol at the beginning of the token when the credential was created. If yes, then remove it.
- The current connection is unable to connect to the storage account from the current machine using Storage Explorer or SQL Server Management Studio (SSMS).
- The policy assigned to the SAS token is expired. Create a new policy using Azure Storage Explorer and either create a new SAS token using the policy or alter the credential and try backing up again.
- The root certificate is missing in Trusted Root Certification store. For more information, see [Azure Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list#certificate-authority-details).

#### Authentication errors

The `WITH CREDENTIAL` is a new option and required to back up to or restore from the Azure Blob Storage.

Failures related to credential could be the following: `The credential specified in the **BACKUP** or **RESTORE** command does not exist. `

To avoid this issue, you can include T-SQL statements to create the credential if one doesn't exist in the backup statement. The following is an example you can use:

```sql
IF NOT EXISTS (
   SELECT *
   FROM sys.credentials
   WHERE credential_identity = 'mycredential'
)
CREATE CREDENTIAL [<credential name>]
   WITH IDENTITY = 'mystorageaccount',
      SECRET = '<storage access key>';
```

The credential exists but the login that is used to run the backup command doesn't have permissions to access the credentials. Use an account in the **db_backupoperator** role with *Alter any credential* permissions.

Verify the storage account name and key values. The information stored in the credential must match the property values of the Azure storage account you're using in the backup and restore operations.

#### 400 (Bad Request) errors

Using [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] you might encounter an error performing a backup similar to the following output:

```output
Backup to URL received an exception from the remote endpoint. Exception Message:
The remote server returned an error: (400) Bad Request.
```

This is caused by the TLS version supported by the Azure Storage Account. Changing the supported TLS version or using the workaround listed in [KB4017023](https://support.microsoft.com/help/4017023).

## Proxy errors

If you use Proxy Servers to access the internet, you might see the following issues:

#### Connection throttling by proxy servers

Proxy servers can have settings that limit the number of connections per minute. The Backup to URL process is a multi-threaded process and hence can go over this limit. If this happens, the proxy server kills the connection. To resolve this issue, change the proxy settings so SQL Server isn't using the proxy. Following are some examples of the types or error messages you might see in the error log:

```output
Write on "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak" failed: Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```

```output
A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Error could not be gathered from Remote Endpoint.

Msg 3013, Level 16, State 1, Line 2

BACKUP DATABASE is terminating abnormally.
```

```output
BackupIoRequest::ReportIoError: write failure on backup device https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak'. Operating system error Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```

If using page blobs, you turn on the verbose logging using the Trace Flag 3051, you might also see the following message in the logs: `HTTP status code 502, HTTP Status Message Proxy Error (The number of HTTP requests per minute exceeded the configured limit. Contact your ISA Server administrator.)`

#### Default proxy settings not picked up

Sometimes the default settings aren't picked up causing proxy authentication errors such as:

`A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (407)* **Proxy Authentication Required.`

To resolve this issue, create a configuration file that allows the Backup to URL process to use the default proxy settings using the following steps:

1. Create a configuration file named `BackuptoURL.exe.config` with the following xml content:

   ```xml
   <?xml version ="1.0"?>
   <configuration>
       <system.net>
           <defaultProxy enabled="true" useDefaultCredentials="true">
               <proxy usesystemdefault="true" />
           </defaultProxy>
       </system.net>
   </configuration>
   ```

1. Place the configuration file in the `Binn` folder of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Instance. For example, if my [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed on the `C` drive of the machine, place the configuration file in `C:\Program Files\Microsoft SQL Server\MSSQL13.\<InstanceName>\MSSQL\Binn`.

1. `BackuptoURL.exe` isn't called when using [SAS keys](/azure/storage/blobs/storage-blob-user-delegation-sas-create-powershell), but is triggered when using an [access key](/azure/storage/common/storage-account-keys-manage?tabs=azure-portal). Make sure you use access keys, or you can receive the following error:

   > Operating system error 50(The request is not supported.)

## Common errors and solutions

| Issue | Solution |
| --- | --- |
| **Error 3063:** Write to backup block blob device `https://storageaccount/container/name.bak` failed. Device has reached its limit of allowed blocks. | To fix this issue, [stripe your backup](/archive/blogs/sqlcat/backing-up-a-vldb-to-azure-blob-storage) target with multiple files and make sure to use the following parameters in the backup command: `COMPRESSION, MAXTRANSFERSIZE = 4194304, BLOCKSIZE = 65536`. |
| **Error 3035:** Differential backup fails for one or multiple databases. | This happens if you configured [Azure Backup service](/azure/backup/backup-overview) to back up SQL databases or a virtual machine (VM) snapshot, which doesn't create a copy-only backup, causing your maintenance plan or SQL agent job on-demand backups to fail. To fix this, [add these registry keys](/azure/backup/backup-azure-vms-troubleshoot#troubleshoot-vm-snapshot-issues) to the VMs hosting SQL Server instances at the registry key `[HKEY_LOCAL_MACHINE\SOFTWARE\MICROSOFT\BCDRAGENT]` and add `"USEVSSCOPYBACKUP"="TRUE"`. |
| **Error 3201:** Backup fails with - Operating system error 50 (The request isn't supported). | Regenerate the shared access signature (SAS) token using Storage Explorer: You can [create a new policy](/azure/storage/blobs/storage-quickstart-blobs-storage-explorer#manage-access-policies) using Azure Storage Explorer and create a new SAS token with that policy from Azure Storage Explorer. [Re-create the credential](sql-server-backup-to-url.md#credential) using this new SAS token generated from Azure Storage and try backing up again. For more information, see [known issues with BACKUP TO URL](#troubleshooting-backup-to-or-restore-from-url). Make sure your network security group (NSG) and/or firewall allows inbound and outbound connection on ports 1433 and 443. |
| **Error 3271:** Backup fails due to TLS error - Backup to URL received an exception from the remote endpoint. | This can happen on SQL Server versions 2012, 2014, and 2016. Backing up to a Microsoft Azure Blob Storage service URL isn't compatible with TLS 1.2 and can be fixed by following the instructions in [KB4017023](https://support.microsoft.com/topic/kb4017023-sql-server-2012-2014-or-2016-backup-to-microsoft-azure-blob-storage-service-url-isn-t-compatible-for-tls-1-2-e9ef6124-fc05-8128-86bc-f4f4f5ff2b78). |
| **Error 3271:** Backup to URL received an exception from the remote endpoint. Exception Message: The remote name couldn't be resolved. | You see this message if an incorrect credential, secret, or SAS key was used to configure the backup. Drop the credential and re-create it. For [SQL Server 2012/2014, use storage account identity and access key](sql-server-backup-to-url.md#credential) and for [SQL Server 2016 and later versions, use SAS](sql-server-backup-to-url.md#credential). |
| **Error 18210:** Exception: The remote server returned an error: (400) Bad Request. | To resolve, change the minimum TLS version for the storage account to 1.0 (**Storage Account** > **Configuration** > **Minimum TLS Version**), or [enable strong cryptography as documented in KB4017023](https://support.microsoft.com/help/4017023/kb4017023-sql-server-2012-2014-or-2016-backup-to-microsoft-azure-blob). |
| **Exception Message:** The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request. | Identify the blobs in Azure Storage Explorer with a size of 1 TB, [break the lease, delete the blob](deleting-backup-blob-files-with-active-leases.md#manage-orphaned-blobs), and retry the backup operation. |
| **Error:** The remote server returned an error: (403) Forbidden. | Recreate the storage account, credential, and SAS token to resolve the problem. |
| **Backup for 1-TB database fails on SQL Server 2012/2014.** | 1-TB backups are a [known limitation](sql-server-backup-to-url.md#limitations) on page blobs before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Use backup compression by adding the 'WITH COMPRESSION' clause to your T-SQL backup statement or upgrade your SQL Server instance to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions. |
| **Error:** Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (416) The page range specified is invalid. | You might see this if you're on [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], and your backup size increases to 1 TB. Stripe your backup files and/or use backup compression to resolve. |
| **Backup failed when using a maintenance plan.** | There are a few bugs with the maintenance plan. Try using T-SQL to execute your backup. If T-SQL works, then you can create a SQL Agent job to run to back up your databases. |
| **Backup failed due to VM limits reached.** | If you're getting errors that the disk IOPS/VM limit was reached, backups might slow down or fail. To monitor IOPS/VM limits, use [Azure Monitor Metrics](/azure/virtual-machines/disks-metrics) and resize the VM/disk, if necessary, to fix the problem. |
| **The remote server returned an error: (409) Conflict for SQL Server 2012/2014**" | Storage accounts with **hierarchical namespace** are equipped for block blobs, not page blobs. Storage accounts without this feature shouldn't be used for BACKUP TO URL in [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]. |

## Related content

- [Restoring From Backups Stored in Microsoft Azure](../../relational-databases/backup-restore/restoring-from-backups-stored-in-microsoft-azure.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
