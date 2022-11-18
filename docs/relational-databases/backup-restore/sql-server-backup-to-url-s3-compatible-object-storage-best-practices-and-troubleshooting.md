---
title: "Back up to URL best practices & troubleshooting for S3-compatible object storage"
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to S3-compatible object storage.
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# SQL Server back up to URL for S3-compatible object storage best practices and troubleshooting

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

  This article includes best practices and troubleshooting tips for SQL Server backup and restores to S3-compatible object storage.
  
> [!NOTE]
> SQL Server backup and restore with S3-compatible object storage is in preview as a feature of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

 For more information about using Azure Blob Storage for SQL Server backup or restore operations, see:  
  
- [SQL Server backup and restore with S3-compatible object storage preview](sql-server-backup-and-restore-with-s3-compatible-object-storage.md)  
- [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md)

## Troubleshooting and common error causes

Following are some quick ways to troubleshoot errors when backing up to or restoring from the S3-compatible object storage. To avoid errors due to unsupported options or limitations, and to review the list of limitations and support for BACKUP and RESTORE commands, see [SQL Backup and Restore with S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md#limitations).

### Ensure a correctly formed URL

Here's an example of a virtual host URL formed correctly when issuing a T-SQL backup query such as follows:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL = 's3://<bucketName>.<virtualHost>/<pathToBackup>/<backupFileName>' 
```

Or for URL path style:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL = 's3://<domainName>/<bucketName>/<pathToBackup>/<backupFileName>';
```

Ensure the following:

1. The URL starts with `s3://` scheme.
1. The S3 storage virtual host `<virtualHost>` or server domain `<domainName>` exists and is running using HTTPS. The endpoint will be validated by a CA installed on the SQL Server OS Host.
1. `<bucketName>` is the name of this bucket where the backup will be placed. This must be created before running the backup T-SQL. The backup T-SQL doesn't create the bucket for the customer. For example, if the user doesn't create the bucket 'nonExistingBucket' beforehand and runs a T-SQL statement as follows:
    
    ```sql
    BACKUP DATABASE AdventureWorks2019
    TO URL = 's3://<your-endpoint>/nonExistingBucket/AdventureWorks2019.bak';
    ```
    
    A URL that is not correctly formed may return the following:
    
    ```
    Msg 3201, Level 16, State 1, Line 50
    Cannot open backup device 's3://<your-endpoint>/nonExistingBucket/AdventureWorks2019.bak'. Operating system error 50(The request is not supported.).
    Msg 3013, Level 16, State 1, Line 50
    BACKUP DATABASE is terminating abnormally.
    ```

1. The `<pathToBackup>` need not exist before running the backup T-SQL. It will be created in the storage server automatically. For example, if the user creates the bucket 'existingBucket' beforehand and not the path `'existingBucket/sqlbackups'`, the following will still run successfully:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL =  's3://<your-endpoint>/existingBucket/sqlbackups/AdventureWorks2019.bak';
```

### Create a server-level credential prior to running backup/restore

Before running backup/restore TSQL queries to S3-compatible storage, you must create a server level credential. This credential needs to contain the Access key and Secret Key set up by customers on their S3-compatible object storage server prior to issuing backup/restore queries.

An example of a credential that needs to be created for URL: `s3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2019.bak` would be the following:

```sql
CREATE CREDENTIAL [s3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2019.bak]
WITH IDENTITY = 'S3 Access Key',
SECRET = '<AccessKeyID>:<SecretKeyID>';
```

In this statement `<AccessKeyID>` is not allowed to contain a `:` character. If the credential is not created prior to running the backup/restore query, the user will see the following error message:

```
Msg 3201, Level 16, State 1, Line 50
Cannot open backup device 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2019.bak'. Operating system error 50(The request is not supported.).
Msg 3013, Level 16, State 1, Line 50
BACKUP DATABASE is terminating abnormally.
```

The name of the credential is not required to match the exact URL path. Here is an example how credential lookup will work. If we need to query path `s3://10.193.16.183:9000/myS3Bucket/sqlbackups/AdventureWorks2019.bak`, the following credential names will be tried:

1. `s3://10.193.16.183:8787/myS3Bucket/sqlbackups/AdventureWorks2019.bak`
1. `s3://10.193.16.183:8787/myS3Bucket/sqlbackups`
1. `s3://10.193.16.183:8787/myS3Bucket`

If there are multiple credentials matching search, such as more specific `s3://10.193.16.183:8787/myS3Bucket/sqlbackups` and more generic `s3://10.193.16.183:8787/myS3Bucket`, we will choose the most specific one. This will allow you to set up more granular access control at directory level for what folders may be accessed from SQL Server.

### Unsupported option FILE_SNAPSHOT

Currently, the BACKUP TSQL option FILE_SNAPSHOT is not supported for S3-compatible object storage. This is an Azure Blob Storage-specific option.

If the user runs the following TSQL for example:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL = 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2019.bak'
WITH FILE_SNAPSHOT;
```

The following error message will be returned:

```
Msg 3073, Level 16, State 1, Line 62
The option WITH FILE_SNAPSHOT is only permitted if all database files are in Azure Storage.
Msg 3013, Level 16, State 1, Line 62
BACKUP DATABASE is terminating abnormally.
```

### Backup stripe exceeding 100,000 MB

Currently, the size of a single backup file created by customers in S3-compatible object storage during a backup cannot exceed 100,000 MB per file, using default `MAXTRANSFERSIZE`. If the backup stripe goes beyond 100,000 MB, the backup T-SQL syntax statement will throw the following error message:

```
Msg 3202, Level 16, State 1, Line 161
Write on 's3://<endpoint>:<port>/<bucket>/<path>/<db_name>.bak' failed: 87(The parameter is incorrect.)
Msg 3013, Level 16, State 1, Line 161
BACKUP DATABASE is terminating abnormally.
```

Current guidance for user's backup large databases is use multiple stripes for the database backup, each of allowable sizes less than or equal to 100,000 MB. The BACKUP T-SQL supports striping up to 64 URLs, for example:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL = 's3://<endpoint>:<port>/<bucket>/<path>/<db_file>_1.bak',
URL = 's3://<endpoint>:<port>/<bucket>/<path>/<db_file>_2.bak';
```

An alternative option for users is to use the 'COMPRESSION' option:

```sql
BACKUP DATABASE AdventureWorks2019
TO URL = 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2019.bak'
WITH COMPRESSION;
```

### Maximum length of URL

The total URL length is limited to 259 bytes by the Backup and Restore engine. This means that `s3://hostname/objectkey` shouldn't exceed 259 characters. Leaving aside `s3://` the user can input the path length (hostname + object key) to be 259 – 5 = 254 characters. Refer to [SQL Server Backup to URL - SQL Server](sql-server-backup-to-url.md). The backup T-SQL syntax statement will throw the following error message:

```
SQL Server has a maximum limit of 259 characters for a backup device name. The BACKUP TO URL consumes 36 characters for the required elements used to specify the URL - 'https://.blob.core.windows.net//.bak', leaving 223 characters for account, container, and blob names put together'
```

### Clock-skew correction

The S3 storage might reject connection, giving a "InvalidSignatureException" error back to SQL Server whenever the time difference between SQL Host and S3 server is bigger than 15 minutes. On SQL Server it will show as:

```
Msg 3201, Level 16, State 1, Line 28
Cannot open backup device '<path>'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 28
BACKUP DATABASE is terminating abnormally.
```

### SQL Server on Linux support

SQL Server uses `WinHttp` to implement client of HTTP REST APIs it uses. It relies on OS certificate store for validations of the TLS certificates being presented by HTTP(s) endpoint. However, SQL Server on Linux delegates the certificate validation to SQLPAL, which validates the endpoints' HTTPS certificates with the certificate shipped with PAL. Thus, customer-provided self-signed certificates cannot be used on Linux for HTTPS validation.

During backup/restore the customer will get the following error message on Linux:

```
Msg 3201, Level 16, State 1, Line 20
Cannot open backup device 's3://<endpoint>/<bucket>/testingDB.bak'. Operating system error 12175(failed to retrieve text for this error. Reason: 15105).
Msg 3013, Level 16, State 1, Line 20
BACKUP DATABASE is terminating abnormally.
```

To get past this problem the following predefined location must be created `/var/opt/mssql/security/ca-certificates`
where the self-signed certificates, or certificates not shipped with PAL, should be placed by the user. SQL Server will read the certificates from the folder during startup and add them to the PAL trust store.

Up to 50 files can be stored in this location, if the folder is not created, when SQL Server is started, the SQL Server error log will show:

```
2022-02-05 00:32:10.86 Server      Installing Client TLS certificates to the store.
2022-02-05 00:32:10.88 Server      Error searching first file in /var/opt/mssql/security/ca-certificates: 3(The system cannot find the path specified.)
```

## Next steps

 - [SQL Server backup and restore with S3-compatible object storage preview](sql-server-backup-and-restore-with-s3-compatible-object-storage.md)  
 - [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md)
 - [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)  
 - [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
