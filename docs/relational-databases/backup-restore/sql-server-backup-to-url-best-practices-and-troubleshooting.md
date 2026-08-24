---
title: Backup to URL Best Practices and Troubleshooting for Microsoft Azure Blob Storage
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to Azure Blob Storage.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/23/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: best-practice
---
# SQL Server backup to URL for Microsoft Azure Blob Storage best practices and troubleshooting

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article includes best practices and troubleshooting tips for SQL Server backup and restores to Microsoft Azure Blob Storage.

For more information about using Azure Blob Storage for SQL Server backup or restore operations, see:

- [SQL Server backup and restore with Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)
- [Quickstart: SQL backup and restore to Azure Blob Storage](../tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)

## Manage backups

The following list includes general recommendations to manage backups:

- Use a unique file name for every backup to prevent accidentally overwriting the blobs.

- When you create a container, set the access level to **private** so only users or accounts that can provide the required authentication information can read or write the blobs in the container.

- For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running in an Azure Virtual Machine, use a storage account in the same region as the virtual machine to avoid data transfer costs between regions. Using the same region also ensures optimal performance for backup and restore operations.

- Failed backup activity can result in an invalid backup file. Periodically identify failed backups and delete the blob files. For more information, see [Delete backup blob files with active leases](deleting-backup-blob-files-with-active-leases.md).

- Use the `WITH COMPRESSION` option to minimize storage costs and storage transaction costs, and to reduce backup time.

- Set the `MAXTRANSFERSIZE` and `BLOCKSIZE` arguments to the values described in [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md).

- You can back up to block blobs with every storage redundancy (for example, LRS, ZRS, GRS, RA-GRS, and RA-GZRS).

## Handle large files

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup operation uses multiple threads to optimize data transfer to Azure Blob Storage. However, performance depends on factors such as ISV bandwidth and database size. If you plan to back up large databases or filegroups from an on-premises SQL Server database, test throughput first. The Azure [SLA for Storage](https://azure.microsoft.com/support/legal/sla/storage/v1_0/) has maximum processing times for blobs to consider.

Use the `WITH COMPRESSION` option recommended in the [Manage Backups](#manage-backups) section when you back up large files.

<a id="troubleshooting-backup-to-or-restore-from-url"></a>

## Troubleshoot backup to or restore from URL

Use the following tips to troubleshoot errors when you back up to or restore from Azure Blob Storage.

To avoid errors from unsupported options or limitations, review the limitations and the supported options for the `BACKUP` and `RESTORE` commands in [SQL Server backup and restore with Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

### Initialization failed

Parallel backups to the same blob cause one of the backups to fail with an **Initialization failed** error.

### The request could not be performed because of an I/O device error

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
WITH COMPRESSION,
     MAXTRANSFERSIZE = 4194304,
     BLOCKSIZE = 65536;
```

### Message Filemark on device is not aligned

When restoring from a compressed backup, you might see the following error:

```output
SqlException 3284 occurred. Severity: 16 State: 5
Message Filemark on device 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak' is not aligned.
Re-issue the Restore statement with the same blocksize used to create the backupset: '65536' looks like a possible value.
```

To solve this error, reissue the `RESTORE` statement with `BLOCKSIZE = 65536`.

### Failed backup activity can result in blobs with active leases

Error during backup due to blobs that have active lease on them:
`Failed backup activity can result in blobs with active leases.`

If you retry a backup statement, the backup operation might fail with an error similar to the following output:

```output
Backup to URL received an exception from the remote endpoint. Exception Message:
The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request.
```

If you try to run a restore statement on a backup blob file that has an active lease, the restore operation fails with an error similar to the following message:

```output
Exception Message: The remote server returned an error: (409) Conflict.
```

When this error occurs, delete the blob files. For more information about this scenario and how to correct this problem, see [Delete backup blob files with active leases](deleting-backup-blob-files-with-active-leases.md).

### OS error 50: The request is not supported

When backing up a database, you might see error `Operating system error 50(The request is not supported.)` for the following reasons:

- The specified storage account isn't General Purpose V1/V2.
- The shared access signature (SAS) token has a `?` symbol at the beginning. If so, remove the symbol.
- You can't connect to the storage account from the current machine by using Storage Explorer or SQL Server Management Studio (SSMS).
- The policy assigned to the SAS token expired. Create a new policy by using Azure Storage Explorer, and either create a new SAS token by using the policy or alter the credential and try backing up again.
- The root certificate is missing in Trusted Root Certification store. For more information, see [Azure Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list#certificate-authority-details).

### Authentication errors

Backing up to or restoring from Azure Blob Storage requires a credential that stores the authentication information. [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions use a shared access signature (SAS) token, and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] matches the credential to the container URL automatically.

Failures related to credentials might produce the following error messages:

| Error number | Message |
| --- | --- |
| 3288 | `Credential name <mycredential> does not exist or user does not have permission to access it.` |
| 3289 | `A Backup device of type URL was specified without a Credential, Backup/Restore operation cannot proceed.` |

To avoid this issue, create the credential if it doesn't exist. For example:

```sql
IF NOT EXISTS (SELECT *
    FROM sys.credentials
    WHERE name = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>')
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = '<SAS_TOKEN>';
```

The credential exists, but the login that runs the backup command doesn't have permissions to access the credentials. Use an account in the **db_backupoperator** fixed database role with *Alter any credential* permissions.

The information stored in the credential must match the property values of the Azure storage account you're using in the backup and restore operations.

## Proxy errors

If you use Proxy Servers to access the internet, you might see the following issues:

### Connection throttling by proxy servers

Proxy servers might have settings that limit the number of connections per minute. Backup to URL is multithreaded and might exceed this limit. If this limit is exceeded, the proxy server closes the connection. To resolve this issue, change the proxy settings so SQL Server doesn't use the proxy. The following examples show error messages you might see in the error log:

```output
Write on "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak" failed: Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```

```output
A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Error could not be gathered from Remote Endpoint.

Msg 3013, Level 16, State 1, Line 2

BACKUP DATABASE is terminating abnormally.
```

```output
BackupIoRequest::ReportIoError: write failure on backup device 'https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak'. Operating system error Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```

## Common errors and solutions

| Issue | Solution |
| --- | --- |
| **Error 3063**: `Write to backup block blob device https://storageaccount/container/name.bak failed. Device has reached its limit of allowed blocks.` | To fix this issue for full or differential backups, [stripe your backup](/archive/blogs/sqlcat/backing-up-a-vldb-to-azure-blob-storage) target with multiple files. For all backup types, use the following parameters in the backup command: `COMPRESSION, MAXTRANSFERSIZE = 4194304, BLOCKSIZE = 65536`. This error can also occur if the backup reaches the maximum supported size. For example, in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, the maximum backup size is 12.8 TB, calculated as 64 stripes × 50,000 blocks × a 4-MB `MAXTRANSFERSIZE`. |
| **Error 3035**: Differential backup fails for one or multiple databases. | This error occurs if you configured [Azure Backup](/azure/backup/backup-overview) to back up SQL databases or a virtual machine (VM) snapshot, which doesn't create a copy-only backup, causing your maintenance plan or SQL agent job on-demand backups to fail. To fix this issue, [add these registry keys](/azure/backup/backup-azure-vms-troubleshoot#troubleshoot-vm-snapshot-issues) to the VMs hosting SQL Server instances at the registry key `[HKEY_LOCAL_MACHINE\SOFTWARE\MICROSOFT\BCDRAGENT]` and add `"USEVSSCOPYBACKUP"="TRUE"`. |
| **Error 3201**: `Cannot open backup device '<url>'. Operating system error 50(The request is not supported.)` | Regenerate the SAS token by using Storage Explorer: In Azure Storage Explorer, [create a new policy](/azure/storage/blobs/storage-quickstart-blobs-storage-explorer#manage-access-policies) and a new SAS token from that policy. [Re-create the credential](sql-server-backup-to-url.md#credential) by using the new SAS token and try the backup again. For more information, see [known issues with BACKUP TO URL](#troubleshooting-backup-to-or-restore-from-url). Ensure your network security group (NSG) or firewall allows inbound and outbound connection on ports 1433 and 443. |
| **Error 3290**: `Backup to URL received an exception from the remote endpoint. Exception Message: The remote name could not be resolved.` | You see this message if an incorrect credential, secret, or SAS key was used to configure the backup. Drop the credential and recreate it. For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, [use SAS](sql-server-backup-to-url.md#credential). |
| **Error 3290**: `Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (400) Bad Request.` | To resolve, change the minimum TLS version for the storage account to 1.0 (**Storage Account** > **Configuration** > **Minimum TLS Version**). |
| **Exception Message**: `The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request.` | In Azure Storage Explorer, identify the 1-TB blobs, [break the lease, delete the blob](deleting-backup-blob-files-with-active-leases.md#manage-orphaned-blobs), and retry the backup operation. |
| **Error**: `The remote server returned an error: (403) Forbidden.` | Recreate the storage account, credential, and SAS token to resolve the problem. |
| **Backup failed when using a maintenance plan.** | Maintenance plans can fail intermittently. Run the equivalent backup directly with T-SQL. If the T-SQL backup succeeds, schedule it as a SQL Server Agent job instead of using a maintenance plan. |
| **Backup failed due to VM limits reached.** | If you receive errors that the disk IOPS/VM limit was reached, backups might slow down or fail. To monitor IOPS/VM limits, use [Azure Monitor Metrics](/azure/virtual-machines/disks-metrics) and resize the VM/disk, if necessary, to fix the problem. |

## Related content

- [Restoring From Backups Stored in Microsoft Azure](restoring-from-backups-stored-in-microsoft-azure.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
