---
title: "Back up to URL best practices & troubleshooting for Microsoft Azure Blob Storage"
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to Azure Blob Storage.
ms.custom:
- seo-lt-2019
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
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

- SQL Server is agnostic to the type of storage redundancy used. Backup to Page blobs and block blobs is supported for every storage redundancy (LRS\ZRS\GRS\RA-GRS\RA-GZRS\etc.).
  
## Handling Large Files  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup operation uses multiple threads to optimize data transfer to Azure Blob Storage.  However the performance depends on various factors, such as ISV bandwidth and size of the database. If you plan to back up large databases or filegroups from an on-premises SQL Server database, it is recommended that you do some throughput testing first. Azure [SLA for Storage](https://azure.microsoft.com/support/legal/sla/storage/v1_0/) has maximum processing times for blobs that you can take into consideration.  
  
-   Using the `WITH COMPRESSION` option as recommended in the [Managing Backups](#managing-backups-mb1) section, it is very important when backing up large files.  
  
## Troubleshooting Backup To or Restore from URL  

Following are some quick ways to troubleshoot errors when backing up to or restoring from the Azure Blob Storage.  
  
To avoid errors due to unsupported options or limitations, review the list of limitations, and support for BACKUP and RESTORE commands information in the [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) article.  

**Initialization failed** 

Parallel backups to the same blob cause one of the backups to fail with an **Initialization failed** error. 

If you're using page blobs, for example, `BACKUP... TO URL... WITH CREDENTIAL`, use the following error logs to help with troubleshooting backup errors:  
  
Set trace flag 3051 to turn on logging to a specific error log with the following format in:  
  
`BackupToUrl-\<instname>-\<dbname>-action-\<PID>.log` Where `\<action>` is one of the following:  
  
-   **DB**  
-   **FILELISTONLY**  
-   **LABELONLY**  
-   **HEADERONLY**  
-   **VERIFYONLY**  
  
You can also find information by reviewing the Windows Event Log - Under Application logs with the name `SQLBackupToUrl`.  

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
  
If you turn on the verbose logging using the trace flag 3051, you may also see the following message in the logs:  
  
`HTTP status code 502, HTTP Status Message Proxy Error (The number of HTTP requests per minute exceeded the configured limit. Contact your ISA Server administrator.)` 
  
 **Default Proxy Settings not picked up:**  
  
Sometimes the default settings are not picked up causing proxy authentication errors such as the one shown below:
 
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
  
## Next steps

 - [Restoring From Backups Stored in Microsoft Azure](../../relational-databases/backup-restore/restoring-from-backups-stored-in-microsoft-azure.md)  
 - [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)  
 - [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
