---
title: "Back up to URL Best Practices & Troubleshooting for S3-Compatible Object Storage"
description: Learn about best practices and troubleshooting tips for SQL Server backup and restores to S3-compatible object storage.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 03/06/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: best-practice
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---
# SQL Server back up to URL for S3-compatible object storage best practices and troubleshooting

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

This article includes best practices and troubleshooting tips for SQL Server backup and restores to S3-compatible object storage.

For more information about using Azure Blob Storage for SQL Server backup or restore operations, see:

- [Back up and restore SQL Server with S3-compatible object storage](sql-server-backup-and-restore-with-s3-compatible-object-storage.md)
- [SQL Server back up to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md)

<a id="troubleshooting-and-common-error-causes"></a>

Following are some quick ways to troubleshoot errors when backing up to or restoring from the S3-compatible object storage. To avoid errors due to unsupported options or limitations, see [SQL Backup and Restore with S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md#limitations).

## Ensure a correctly formed URL

Here's an example of a virtual host URL formed correctly when issuing a T-SQL backup query such as follows:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<bucketName>.<virtualHost>/<pathToBackup>/<backupFileName>'
```

Or for URL path style:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<domainName>/<bucketName>/<pathToBackup>/<backupFileName>';
```

Review in the URL:

1. The URL starts with `s3://` scheme.
1. The S3 storage virtual host `<virtualHost>` or server domain `<domainName>` exists and is running using HTTPS. A CA installed on the SQL Server OS host validates the endpoint.
1. `<bucketName>` is the name of the bucket where the backup is written. The bucket must be created before you run the backup T-SQL statement. The backup T-SQL doesn't create the bucket. For example, if you don't create the bucket 'nonExistingBucket' beforehand and run a T-SQL statement as follows:

   ```sql
   BACKUP DATABASE AdventureWorks2022
   TO URL = 's3://<your-endpoint>/nonExistingBucket/AdventureWorks2022.bak';
   ```

   A URL that isn't correctly formed might return the following error:

   ```
   Msg 3201, Level 16, State 1, Line 50
   Cannot open backup device 's3://<your-endpoint>/nonExistingBucket/AdventureWorks2022.bak'. Operating system error 50(The request is not supported.).
   Msg 3013, Level 16, State 1, Line 50
   BACKUP DATABASE is terminating abnormally.
   ```

1. The `<pathToBackup>` doesn't need to exist before you run the backup T-SQL. The storage server creates the path automatically. For example, if you create the bucket 'existingBucket' beforehand but not the path `'existingBucket/sqlbackups'`, the following command still runs successfully:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL =  's3://<your-endpoint>/existingBucket/sqlbackups/AdventureWorks2022.bak';
```

## Create a server-level credential before running backup/restore

Before running BACKUP or RESTORE Transact-SQL commands against S3-compatible object storage, you must create a server-level credential containing the Access Key ID and Secret Key ID that your S3 storage provider requires. There are two ways to name the credential:

- **[Set a credential name](#set-a-credential-name)**: Choose any name you want, but you must explicitly specify the `CREDENTIAL` parameter in each BACKUP/RESTORE statement.
- **[Use the S3 URL path as the credential name](#use-the-s3-url-path-as-the-credential-name)**: Name the credential with the S3 URL path, and SQL Server automatically matches the credential to the backup URL without requiring the `CREDENTIAL` parameter.

### Set a credential name

Choose any credential name, as long as the Access Key ID and Secret Key ID grant access to the S3 backup URL. When you use a credential with an arbitrary name, you must explicitly reference it in the BACKUP/RESTORE statement by adding the `CREDENTIAL` parameter.

The following example creates a credential with an arbitrary name:

```sql
CREATE CREDENTIAL [myOwnCredential]
WITH IDENTITY = 'S3 Access Key',
SECRET = '<AccessKeyID>:<SecretKeyID>';
```

The following example backs up a database using the credential:

```sql
BACKUP DATABASE [Test]
TO URL = 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak'
WITH COMPRESSION, NOFORMAT, NAME = N'FULL bkp', MAXTRANSFERSIZE = 20971520, CREDENTIAL = N'myOwnCredential'
```

### Use the S3 URL path as the credential name

Name the credential using the S3 URL path. With this approach, SQL Server automatically performs credential lookup, and you don't need to specify the `CREDENTIAL` parameter in the BACKUP/RESTORE statement. SQL Server attempts to find the most specific matching credential based on the URL.

The following example creates a credential for the URL `s3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak`:

```sql
CREATE CREDENTIAL [s3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak]
WITH IDENTITY = 'S3 Access Key',
SECRET = '<AccessKeyID>:<SecretKeyID>';
```

In this statement, `<AccessKeyID>` can't contain a `:` character. If the credential isn't created before you run the backup/restore query, SQL Server returns the following error message:

```
Msg 3201, Level 16, State 1, Line 50
Cannot open backup device 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak'. Operating system error 50(The request is not supported.).
Msg 3013, Level 16, State 1, Line 50
BACKUP DATABASE is terminating abnormally.
```

The credential name doesn't need to match the exact URL path. The following example shows how credential lookup works. If you need to query path `s3://10.193.16.183:9000/myS3Bucket/sqlbackups/AdventureWorks2022.bak`, SQL Server tries the following credential names:

1. `s3://10.193.16.183:8787/myS3Bucket/sqlbackups/AdventureWorks2022.bak`
1. `s3://10.193.16.183:8787/myS3Bucket/sqlbackups`
1. `s3://10.193.16.183:8787/myS3Bucket`

If multiple credentials match the search, such as the more specific `s3://10.193.16.183:8787/myS3Bucket/sqlbackups` and the more generic `s3://10.193.16.183:8787/myS3Bucket`, SQL Server chooses the most specific one. This behavior lets you set up granular access control at the directory level for which folders can be accessed from SQL Server.

## Unsupported option FILE_SNAPSHOT

Currently, the BACKUP Transact-SQL option FILE_SNAPSHOT isn't supported for S3-compatible object storage. FILE_SNAPSHOT is an Azure Blob Storage-specific option.

If you run the following Transact-SQL example:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak'
WITH FILE_SNAPSHOT;
```

The following error message is returned:

```
Msg 3073, Level 16, State 1, Line 62
The option WITH FILE_SNAPSHOT is only permitted if all database files are in Azure Storage.
Msg 3013, Level 16, State 1, Line 62
BACKUP DATABASE is terminating abnormally.
```

## Backup stripe exceeding 100 GB

Currently, the size of a single backup file created by customers in S3-compatible object storage during a backup can't exceed 100 GB per file, using default `MAXTRANSFERSIZE`. If the backup stripe goes beyond 100 GB, the backup T-SQL syntax statement throws the following error message:

```
Msg 3202, Level 16, State 1, Line 161
Write on 's3://<endpoint>:<port>/<bucket>/<path>/<db_name>.bak' failed: 87(The parameter is incorrect.)
Msg 3013, Level 16, State 1, Line 161
BACKUP DATABASE is terminating abnormally.
```

Current guidance for user's backup large databases is use multiple stripes for the database backup, each of allowable sizes less than or equal to 100 GB. The BACKUP T-SQL supports striping up to 64 URLs, for example:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<endpoint>:<port>/<bucket>/<path>/<db_file>_1.bak',
URL = 's3://<endpoint>:<port>/<bucket>/<path>/<db_file>_2.bak';
```

An alternative option for users is to use the 'COMPRESSION' option:

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<your-endpoint>/myS3Bucket/sqlbackups/AdventureWorks2022.bak'
WITH COMPRESSION;
```

## Maximum length of URL

The total URL length is limited to 259 bytes by the Backup and Restore engine. This limit means that `s3://hostname/objectkey` shouldn't exceed 259 characters. Leaving aside `s3://` the user can input the path length (hostname + object key) to be 259 - 5 = 254 characters. Refer to [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md). The backup T-SQL syntax statement throws the following error message:

```
SQL Server has a maximum limit of 259 characters for a backup device name. The BACKUP TO URL consumes 36 characters for the required elements used to specify the URL - 'https://.blob.core.windows.net//.bak', leaving 223 characters for account, container, and blob names put together'
```

## Clock-skew correction

The S3 storage might reject connection, giving a "InvalidSignatureException" error back to SQL Server whenever the time difference between SQL Host and S3 server is bigger than 15 minutes. On SQL Server, the error appears as:

```
Msg 3201, Level 16, State 1, Line 28
Cannot open backup device '<path>'. Operating system error 5(Access is denied.).
Msg 3013, Level 16, State 1, Line 28
BACKUP DATABASE is terminating abnormally.
```

## SQL Server on Linux support

SQL Server uses `WinHttp` to implement the client of HTTP REST APIs it uses. SQL Server relies on the OS certificate store for validation of the TLS certificates presented by the HTTPS endpoint. However, SQL Server on Linux delegates the certificate validation to SQLPAL, which validates the endpoints' HTTPS certificates with the certificate shipped with PAL. As a result, customer-provided self-signed certificates can't be used on Linux for HTTPS validation.

During backup/restore the customer gets the following error message on Linux:

```
Msg 3201, Level 16, State 1, Line 20
Cannot open backup device 's3://<endpoint>/<bucket>/testingDB.bak'. Operating system error 12175(failed to retrieve text for this error. Reason: 15105).
Msg 3013, Level 16, State 1, Line 20
BACKUP DATABASE is terminating abnormally.
```

To get past this problem, the following predefined location must be created: `/var/opt/mssql/security/ca-certificates`. Place self-signed certificates, or certificates not shipped with PAL in this location. SQL Server reads the certificates from the folder during startup and adds them to the PAL trust store.

Up to 50 files can be stored in this location. If the folder doesn't exist when SQL Server starts, the SQL Server error log shows:

```
2022-02-05 00:32:10.86 Server      Installing Client TLS certificates to the store.
2022-02-05 00:32:10.88 Server      Error searching first file in /var/opt/mssql/security/ca-certificates: 3(The system cannot find the path specified.)
```

## Object Lock - delete retention isn't supported

The SQL Server backup to S3-compatible object storage feature doesn't support Object Lock, also called the Delete Retention feature. Object Lock prevents files from being deleted or overwritten during the retention period.

The bucket and folder location targeted by your backup operation must not have Object Lock enabled. If this feature is enabled and configured in your S3-compatible object storage, the backup operation fails with the following message:

```output
Msg 3202, Level 16, State 1, Line 13
Write on 's3://<your-endpoint>/nonExistingBucket/AdventureWorks2022.bak' failed: 87 (The parameter is incorrect).
Msg 3013, Level 16, State 1, Line 13
BACKUP DATABASE is terminating abnormally.
```

## Related content

- [Back up and restore SQL Server with S3-compatible object storage](sql-server-backup-and-restore-with-s3-compatible-object-storage.md)
- [SQL Server back up to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
