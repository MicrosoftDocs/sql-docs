---
title: "Back up to URL best practices & troubleshooting for Microsoft Azure Blob Storage"
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to Azure Blob Storage.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 03/08/2022
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
---
# SQL Server back up to URL for Microsoft Azure Blob Storage best practices and troubleshooting

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  This article includes best practices and troubleshooting tips for SQL Server backup and restores to Microsoft Azure Blob Storage.
  
 For more information about using Azure Blob Storage for SQL Server backup or restore operations, see:  
  
-   [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)  
  
-   [Tutorial: SQL Server Backup and Restore to Azure Blob Storage](../../relational-databases/tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)  
  
## <a name="managing-backups-mb1"></a> Managing Backups  
 The following list includes general recommendations to manage backups:  
  
-   Unique file name for every backup is recommended to prevent accidentally overwriting the blobs.  
  
-   When creating a container, it is recommended that you set the access level to **private**, so only users or accounts that can provide the required authentication information can read or write the blobs in the container.  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in an Azure Virtual Machine, use a storage account in the same region as the virtual machine to avoid data transfer costs between regions. Using the same region also ensures optimal performance for backup and restore operations.  
  
-   Failed backup activity can result in an invalid backup file. We recommend periodic identification of failed backups and deleting the blob files. For more information, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md)  
  
-   Using the `WITH COMPRESSION` option during backup can minimize your storage costs and storage transaction costs. It can also decrease the time taken to complete the backup process.  

- Set `MAXTRANSFERSIZE` and `BLOCKSIZE` arguments as recommended at [SQL Server Backup to URL](./sql-server-backup-to-url.md).

- SQL Server is agnostic to the type of storage redundancy used. Backup to page blobs and block blobs is supported for every storage redundancy (LRS\ZRS\GRS\RA-GRS\RA-GZRS\etc.).
  
## Handling Large Files  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup operation uses multiple threads to optimize data transfer to Azure Blob Storage.  However the performance depends on various factors, such as ISV bandwidth and size of the database. If you plan to back up large databases or filegroups from an on-premises SQL Server database, it is recommended that you do some throughput testing first. Azure [SLA for Storage](https://azure.microsoft.com/support/legal/sla/storage/v1_0/) has maximum processing times for blobs that you can take into consideration.  
  
-   Using the `WITH COMPRESSION` option as recommended in the [Managing Backups](#managing-backups-mb1) section, it is very important when backing up large files.  
  
## Troubleshooting backup to or restore from URL  

Following are some quick ways to troubleshoot errors when backing up to or restoring from the Azure Blob Storage.  
  
To avoid errors due to unsupported options or limitations, review the list of limitations, and support for BACKUP and RESTORE commands information in the [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) article.  

**Initialization failed** 

Parallel backups to the same blob cause one of the backups to fail with an **Initialization failed** error. 

- In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions, block blob is preferred for Backup to URL. 

- If you're using page blobs with BACKUP TO URL, you can use [Trace Flag 3051](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3051) to turn on logging to a specific error log with the following format in: `BackupToUrl-\<instname>-\<dbname>-action-\<PID>.log`, where `\<action>` is one of the following:   
   - **DB**  
   - **FILELISTONLY**  
   - **LABELONLY**  
   - **HEADERONLY**  
   - **VERIFYONLY**  
  
You can also find information by reviewing the Windows Event Viewer, under **Application logs** with the name `SQLBackupToUrl`.  

**The request could not be performed because of an I/O device error.**

Consider COMPRESSION, MAXTRANSFERSIZE, BLOCKSIZE, and multiple URL arguments when backing up large databases.  See [Backing up a VLDB to Azure Blob Storage](/archive/blogs/sqlcat/backing-up-a-vldb-to-azure-blob-storage)

The error: 

```console
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

**Message Filemark on device is not aligned.**

When restoring from a compressed backup, you might see the following error:  
  
```
SqlException 3284 occurred. Severity: 16 State: 5  
Message Filemark on device 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak' is not aligned.
Reissue the Restore statement with the same block size used to create the backupset: '65536' looks like a possible value.  
```
  
To solve this error, reissue the **RESTORE** statement with **BLOCKSIZE = 65536** specified.  
  
**Failed backup activity can result in blobs with active leases.**

Error during backup due to blobs that have active lease on them:
`Failed backup activity can result in blobs with active leases.`  

If a backup statement is reattempted, backup operation might fail with an error similar to the following:  

```
Backup to URL received an exception from the remote endpoint. Exception Message: 
The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request. 
```

If a restore statement is attempted on a backup blob file that has an active lease, the restore operation fails with an error similar to the following:  
  
`Exception Message: The remote server returned an error: (409) Conflict..`  
  
When such error occurs, the blob files need to be deleted. For more information on this scenario and how to correct this problem, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md)  

**OS error 50: The request is not supported**
 
When backing up a database, you may see error `Operating system error 50(The request is not supported)` for the following reasons: 

   - The specified storage account is not General Purpose V1/V2.
   - The SAS token had a `?` symbol at the beginning of the token when the credential was created. If yes, then remove it.
   - The current connection is unable to connect to the storage account from the current machine using Storage Explorer or SQL Server Management Studio (SSMS). 
   - The policy assigned to the SAS token is expired. Create a new policy using Azure Storage Explorer and either create a new SAS token using the policy or alter the credential and try backing up again. 

**Authentication errors**
  
The `WITH CREDENTIAL` is a new option and required to back up to or restore from the Azure Blob Storage.

Failures related to credential could be the following: `The credential specified in the **BACKUP** or **RESTORE** command does not exist. `

To avoid this issue, you can include T-SQL statements to create the credential if one does not exist in the backup statement. The following is an example you can use:  

  
```sql  
IF NOT EXISTS  
(SELECT * FROM sys.credentials   
WHERE credential_identity = 'mycredential')  
CREATE CREDENTIAL [<credential name>] WITH IDENTITY = 'mystorageaccount'  
, SECRET = '<storage access key>' ;  
```  
  
The credential exists but the login account that is used to run the backup command does not have permissions to access the credentials. Use a login account in the **db_backupoperator** role with ***Alter any credential*** permissions.  
  
Verify the storage account name and key values. The information stored in the credential must match the property values of the Azure storage account you are using in the backup and restore operations.  
  

**400 (Bad Request) errors**

Using SQL Server 2012 you may encounter an error performing a backup similar to the following:

```
Backup to URL received an exception from the remote endpoint. Exception Message: 
The remote server returned an error: (400) Bad Request..
```

This is caused by the TLS version supported by the Azure Storage Account. Changing the supported TLS version or using the workaround listed in [KB4017023](https://support.microsoft.com/en-us/topic/kb4017023-sql-server-2012-2014-or-2016-backup-to-microsoft-azure-blob-storage-service-url-isn-t-compatible-for-tls-1-2-e9ef6124-fc05-8128-86bc-f4f4f5ff2b78).


## Proxy Errors  
 If you are using Proxy Servers to access the internet, you may see the following issues:  
  
 **Connection throttling by Proxy Servers**  
  
 Proxy Servers can have settings that limit the number of connections per minute. The Backup to URL process is a multi-threaded process and hence can go over this limit. If this happens, the proxy server kills the connection. To resolve this issue, change the proxy settings so SQL Server is not using the proxy. Following are some examples of the types or error messages you may see in the error log:  
  
```console
Write on "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak" failed: Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```  
  
```console
A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Error could not be gathered from Remote Endpoint.  
  
Msg 3013, Level 16, State 1, Line 2  
  
BACKUP DATABASE is terminating abnormally.  
```

```console
BackupIoRequest::ReportIoError: write failure on backup device https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak'. Operating system error Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```  
  
If using page blobs, you turn on the verbose logging using the Trace Flag 3051, you may also see the following message in the logs: `HTTP status code 502, HTTP Status Message Proxy Error (The number of HTTP requests per minute exceeded the configured limit. Contact your ISA Server administrator.)` 
  
 **Default Proxy Settings not picked up:**  
  
Sometimes the default settings are not picked up causing proxy authentication errors such as:
 
 `A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (407)* **Proxy Authentication Required.`  
  
To resolve this issue, create a configuration file that allows the Backup to URL process to use the default proxy settings using the following steps:  
  
1.  Create a configuration file named `BackuptoURL.exe.config` with the following xml content:  
  
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
  
2.  Place the configuration file in the Binn folder of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Instance. For example, if my [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on the C drive of the machine, place the configuration file in `C:\Program Files\Microsoft SQL Server\MSSQL13.\<InstanceName>\MSSQL\Binn`.  

## Common Errors and Solutions

| Issue| Solution |
|--|--|
| **Error 3063:** Write to backup block blob device https://storageaccount/container/test.bak failed. Device has reached its limit of allowed blocks. | To fix this issue, [stripe your backup](https://learn.microsoft.com/archive/blogs/sqlcat/backing-up-a-vldb-to-azure-blob-storage) target with multiple files and make sure to use the following parameters in the backup command: `COMPRESSION, MAXTRANSFERSIZE = 4194304, BLOCKSIZE = 65536`. |
| **Error 3035:** Differential backup fails for one or multiple databases. | This happens if you've configured [Azure Backup service](https://learn.microsoft.com/azure/backup/backup-overview) to back up SQL databases or a virtual machine (VM) snapshot, which don't create a copy-only backup, causing your maintenance plan, SQL agent job on-demand backups to fail. To fix this, [add these registry keys](https://learn.microsoft.com/azure/backup/backup-azure-vms-troubleshoot#troubleshoot-vm-snapshot-issues) on the VMs hosting SQL Server instances at the key `[HKEY_LOCAL_MACHINE\SOFTWARE\MICROSOFT\BCDRAGENT]` and add `"USEVSSCOPYBACKUP"="TRUE"`. |
| **Error 3201:** Backup fails with - Operating system error 50 (The request is not supported). | Regenerate shared access signature (SAS) token using Storage Explorer: You can [create a new policy](https://learn.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-storage-explorer#manage-access-policies) using Azure Storage Explorer and create a new SAS token with that policy from Azure Storage Explorer. [Re-create the credential](https://learn.microsoft.com/sql/relational-databases/backup-restore/sql-server-backup-to-url?view=sql-server-ver15#credential) using this new SAS token generated from Azure Storage and try backing up again. For more information, see [known issues](https://learn.microsoft.com/sql/relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting?view=sql-server-ver16#troubleshooting-backup-to-or-restore-from-url). Make sure your network security group (NSG)/firewall allows inbound and outbound connection to ports 1433 and 443. |
| **Error 3271:** Backup fails due to TLS with error - Backup to URL received an exception from the remote endpoint.  | This can happen on SQL 2012, 2014, and 2016. Backing up to a Microsoft Azure Blob Storage service URL isn't compatible for TLS 1.2 and can be fixed by following [these instructions](https://support.microsoft.com/topic/kb4017023-sql-server-2012-2014-or-2016-backup-to-microsoft-azure-blob-storage-service-url-isn-t-compatible-for-tls-1-2-e9ef6124-fc05-8128-86bc-f4f4f5ff2b78).|
| **Error 3271:** Backup to URL received an exception from the remote endpoint. Exception Message: The remote name could not be resolved. | You'll see this message if an incorrect credential, secret, or SAS key was used to configure the backup. Drop the credential and re-create it. For [SQL 2012/2014 use storage account identity and access key](https://learn.microsoft.com/sql/relational-databases/backup-restore/sql-server-backup-to-url?view=sql-server-ver16#credential) and for [SQL 2016/2017/2019/2022 use SAS](https://learn.microsoft.com/sql/relational-databases/backup-restore/sql-server-backup-to-url?view=sql-server-ver16#credential).| 
| **Error 18210:** Exception: The remote server returned an error: (400) Bad Request.| You can change the minimum TLS version on the storage account to 1.0 (**Storage Account** > **Configuration** > **Minimum TLS Version**), or you can enable strong cryptography as [documented in this article](https://support.microsoft.com/help/4017023/kb4017023-sql-server-2012-2014-or-2016-backup-to-microsoft-azure-blob) to resolve this. |
| **Exception Message:**  The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request.| Identify the blobs in Storage Explorer with a size of 1 TB, [break the lease, delete the blob](https://learn.microsoft.com/sql/relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases?view=sql-server-ver16#manage-orphaned-blobs), and retry the backup operation. |
| **Error:**  The remote server returned an error: (403) Forbidden.| Re-creating the Storage account, credential, and SAS token should fix the problem. |  
| **Backup for 1-TB database fails on SQL 2012/2014.** | This is a [known limitation on page blobs](https://learn.microsoft.com/sql/relational-databases/backup-restore/sql-server-backup-to-url?view=sql-server-ver16#limitations). You can use compression in your TSQL to back up or upgrade your SQL to 2016 or later version. | 
| **Error:** Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (416) The page range specified is invalid. | You may see this if you're on SQL 2012/2014 and if your backup size increases 1 TB. Stripe your backup files to resolve. | 
| **Backup failed when using a maintenance plan.** | There are a few bugs with the maintenance plan. See if you can execute T-SQL to back up. If you can, then you can create a SQL agent job to run for backing up. | 
| **Backup failed due to VM limits reached.** | If you're getting disk IO/VM limit reached errors, backups may slow down or fail. To monitor IOPS/VM limits, use [Azure Monitor Metrics](https://learn.microsoft.com/azure/virtual-machines/disks-metrics) and resize the VM/disk, if required, to fix the problem.| 
| **The remote server returned an error: (409) Conflict for SQL 2012/2014**" | Storage account with **hierarchical namespace** are equipped for block blobs, not page blobs. Storage account without this feature should be used for backup to url for SQL server 2014. |

## Next steps

 - [Restoring From Backups Stored in Microsoft Azure](../../relational-databases/backup-restore/restoring-from-backups-stored-in-microsoft-azure.md)  
 - [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)  
 - [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
