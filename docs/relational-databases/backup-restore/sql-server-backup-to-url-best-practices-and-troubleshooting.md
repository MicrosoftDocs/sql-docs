---
title: "SQL Server Backup to URL Best Practices and Troubleshooting | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: de676bea-cec7-479d-891a-39ac8b85664f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Backup to URL Best Practices and Troubleshooting
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic includes best practices and troubleshooting tips for SQL Server backup and restores to the Windows Azure Blob service.  
  
 For more information about using Windows Azure Blob storage service for SQL Server backup or restore operations, see:  
  
-   [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)  
  
-   [Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service](../../relational-databases/tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)  
  
## Managing Backups  
 The following list includes general recommendations to manage backups:  
  
-   Unique file name for every backup is recommended to prevent accidentally overwriting the blobs.  
  
-   When creating a container, it is recommended that you set the access level to **private**, so only users or accounts that can provide the required authentication information can read or write the blobs in the container.  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in a Windows Azure Virtual Machine, use a storage account in the same region as the virtual machine to avoid data transfer costs between regions. Using the same region also ensures optimal performance for backup and restore operations.  
  
-   Failed backup activity can result in an invalid backup file. We recommend periodic identification of failed backups and deleting the blob files. For more information, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md)  
  
-   Using the `WITH COMPRESSION` option during backup can minimize your storage costs and storage transaction costs. It can also decrease the time taken to complete the backup process.  

- Set `MAXTRANSFERSIZE` and `BLOCKSIZE` arguments as reccomended at [SQL Server Backup to URL](./sql-server-backup-to-url.md).
  
## Handling Large Files  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup operation uses multiple threads to optimize data transfer to Windows Azure Blob storage services.  However the performance depends on various factors, such as ISV bandwidth and size of the database. If you plan to back up large databases or filegroups from an on-premise SQL Server database, it is recommended that you do some throughput testing first. Azure [SLA for Storage](https://azure.microsoft.com/support/legal/sla/storage/v1_0/) has maximum processing times for blobs that you can take into consideration.  
  
-   Using the `WITH COMPRESSION` option as recommended in the [Managing Backup](##managing-backups) section, it is very important when backing up large files.  
  
## Troubleshooting Backup To or Restore from URL  
 Following are some quick ways to troubleshoot errors when backing up to or restoring from the Windows Azure Blob storage service.  
  
 To avoid errors due to unsupported options or limitations, review the list of limitations, and support for BACKUP and RESTORE commands information in the [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) article.  
  
 **Authentication Errors:**  
  
-   The `WITH CREDENTIAL` is a new option and required to back up to or restore from the Windows Azure Blob storage service. Failures related to credential could be the following:  
  
     The credential specified in the **BACKUP** or **RESTORE** command does not exist. To avoid this issue, you can include T-SQL statements to create the credential if one does not exist in the backup statement. The following is an example you can use:  
  
    ```sql  
    IF NOT EXISTS  
    (SELECT * FROM sys.credentials   
    WHERE credential_identity = 'mycredential')  
    CREATE CREDENTIAL <credential name> WITH IDENTITY = 'mystorageaccount'  
    ,SECRET = '<storage access key> ;  
    ```  
  
-   The credential exists but the login account that is used to run the backup command does not have permissions to access the credentials. Use a login account in the **db_backupoperator** role with ***Alter any credential*** permissions.  
  
-   Verify the storage account name and key values. The information stored in the credential must match the property values of the Windows Azure storage account you are using in the backup and restore operations.  
  
 **Backup Errors/Failures:**  
  
-   Parallel backups to the same blob cause one of the backups to fail with an **Initialization failed** error.  
  
-   Use the following error logs to help with troubleshooting backup errors:  
  
    -   Set trace flag 3051 to turn on logging to a specific error log with the following format in:  
  
        `BackupToUrl-\<instname>-\<dbname>-action-\<PID>.log` Where `\<action>` is one of the following:  
  
        -   **DB**  
        -   **FILELISTONLY**  
        -   **LABELONLY**  
        -   **HEADERONLY**  
        -   **VERIFYONLY**  
  
    -   You can also find information by reviewing the Windows Event Log - Under Application logs with the name `SQLBackupToUrl`.  

    -   Consider COMPRESSION, MAXTRANSFERSIZE, BLOCKSIZE and multiple URL arguments when backing up large databases.  See [Backing up a VLDB to Azure Blob Storage](https://blogs.msdn.microsoft.com/sqlcat/2017/03/10/backing-up-a-vldb-to-azure-blob-storage/)
  
		```
		Msg 3202, Level 16, State 1, Line 1
		Write on "https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak" failed: 1117(The request could not be performed because of an I/O device error.)
		Msg 3013, Level 16, State 1, Line 1
		BACKUP DATABASE is terminating abnormally.
		```

        ```sql  
        BACKUP DATABASE TestDb
        TO URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak',
        URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_1.bak',
        URL = 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_2.bak'
        WITH COMPRESSION, MAXTRANSFERSIZE = 4194304, BLOCKSIZE = 65536;  
        ```  

-   When restoring from a compressed backup, you might see the following error:  
  
    -   `SqlException 3284 occurred. Severity: 16 State: 5  
        Message Filemark on device 'https://mystorage.blob.core.windows.net/mycontainer/TestDbBackupSetNumber2_0.bak' is not aligned.           Reissue the Restore statement with the same block size used to create the backupset: '65536' looks like a possible value.`  
  
        To solve this error, reissue the **RESTORE** statement with **BLOCKSIZE = 65536** specified.  
  
-   Error during backup due to blobs that have active lease on them: Failed backup activity can result in blobs with active leases.  
  
     If a backup statement is reattempted, backup operation might fail with an error similar to the following:  
  
     `Backup to URL received an exception from the remote endpoint. Exception Message: The remote server returned an error: (412) There is currently a lease on the blob and no lease ID was specified in the request.`  
  
     If a restore statement is attempted on a backup blob file that has an active lease, the restore operation fails with an error similar to the following:  
  
     `Exception Message: The remote server returned an error: (409) Conflict..`  
  
     When such error occurs, the blob files need to be deleted. For more information on this scenario and how to correct this problem, see [Deleting Backup Blob Files with Active Leases](../../relational-databases/backup-restore/deleting-backup-blob-files-with-active-leases.md)  
  
## Proxy Errors  
 If you are using Proxy Servers to access the internet, you may see the following issues:  
  
 **Connection throttling by Proxy Servers:**  
  
 Proxy Servers can have settings that limit the number of connections per minute. The Backup to URL process is a multi-threaded process and hence can go over this limit. If this happens, the proxy server kills the connection. To resolve this issue, change the proxy settings so SQL Server is not using the proxy. Following are some examples of the types or error messages you may see in the error log:  
  
```
Write on "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak" failed: Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```  
  
```
A nonrecoverable I/O error occurred on file "https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak:" Error could not be gathered from Remote Endpoint.  
  
Msg 3013, Level 16, State 1, Line 2  
  
BACKUP DATABASE is terminating abnormally.  
```

```
BackupIoRequest::ReportIoError: write failure on backup device https://storageaccount.blob.core.windows.net/container/BackupAzurefile.bak'. Operating system error Backup to URL received an exception from the remote endpoint. Exception Message: Unable to read data from the transport connection: The connection was closed.
```  
  
If you turn on the verbose logging using the trace flag 3051 you may also see the following message in the logs:  
  
`HTTP status code 502, HTTP Status Message Proxy Error (The number of HTTP requests per minute exceeded the configured limit. Contact your ISA Server administrator.) ` 
  
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
  
## See Also  
 [Restoring From Backups Stored in Microsoft Azure](../../relational-databases/backup-restore/restoring-from-backups-stored-in-microsoft-azure.md)  
[BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)  
[RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
  
