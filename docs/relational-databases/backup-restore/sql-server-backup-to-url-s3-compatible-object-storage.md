---
title: "SQL Server back up to URL for S3-compatible object storage"
description: Learn about the concepts, requirements, and components necessary for SQL Server to use the S3-compatible object storage as a backup destination.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: hudequei
ms.date: 05/01/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# SQL Server back up to URL for S3-compatible object storage

[!INCLUDE [SQL Server 2022 and later](../../includes/applies-to-version/sqlserver2022-and-later.md)]

This article introduces the concepts, requirements, and components necessary to [use S3-compatible object storage as a backup destination](sql-server-backup-and-restore-with-s3-compatible-object-storage.md). The backup and restore functionality is conceptually similar to working with [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md) as a backup device type.

For information on supported platforms, see [providers of S3-compatible object storage](sql-server-backup-and-restore-with-s3-compatible-object-storage.md#providers-of-s3-compatible-object-storage).

## Overview

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces object storage integration to the data platform, enabling you to integrate SQL Server with S3-compatible object storage in addition to Azure Storage. To provide this integration, SQL Server supports an S3 connector, which uses the S3 REST API to connect to any provider of S3-compatible object storage. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] extends the existing BACKUP/RESTORE TO/FROM URL syntax by adding support for the new S3 connector using the REST API.

URLs pointing to S3-compatible resources are prefixed with `s3://` to denote that the S3 connector is being used. URLs beginning with `s3://` always assume that the underlying protocol is `https`.

## Part numbers and file size limitations

To store data, the S3-compatible object storage provider must split files in multiple blocks called parts, similar to [block blobs](/rest/api/storageservices/understanding-block-blobs--append-blobs--and-page-blobs) in Azure Blob Storage.

Each file can be split up to 10,000 parts, each part size ranges from 5 MB to 20 MB, this range is controlled by the T-SQL BACKUP command through the parameter [MAXTRANSFERSIZE](../../t-sql/statements/backup-transact-sql.md#with-options). The default value of `MAXTRANSFERSIZE` is 10 MB, therefore the default size of each part is 10 MB. While this value specifies the _max_ transfer size, it doesn't guarantee that each sent part is 10 MB. The size of the part is influenced by the adjacency of data. For example, if there's 4 MB of data adjacent to 2 MB of data, then 6 MB is sent after reaching the minimum size part of 5 MB. Alternatively, if there's 12 MB of adjacent dat, data up to the max size (10 MB) is sent, and the remaining 2 MB is sent in the next part. The S3 connector always tries to send the maximum size of data possible, but it never exceeds the `MAXTRANSFERSIZE` value.

The maximum supported size of a single file is the result of *10,000 parts \* `MAXTRANSFERSIZE`*, if it's required to backup a bigger file it must split/striped up to 64 URLs. The final maximum supported size of a file is *10,000 parts \* `MAXTRANSFERSIZE` \* URLs*.

> [!NOTE]  
> The use of COMPRESSION is required in order to change `MAXTRANSFERSIZE` values.

## Prerequisites for the S3 endpoint

The S3 endpoint must be configured as follows:

- TLS must be configured. It is assumed that all connections will be securely transmitted over HTTPS not HTTP. The endpoint is validated by a certificate installed on the SQL Server OS Host.
- Credentials created on the S3-compatible object storage with proper permissions to perform the operation. The user and password created on the storage layer are named the `Access Key ID` and `Secret Key ID`. You need both to authenticate against the S3 endpoint.
- At least one bucket has been configured. Buckets can't be created or configured from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## Security

### Backup permissions

To connect SQL Server to S3-compatible object storage, two sets of permissions need to be established, one on SQL Server and also on the storage layer.

On SQL Server the user account that is used to issue BACKUP or RESTORE commands should be in the **db_backupoperator** database role with **Alter any credential** permissions.

On the storage layer:

- In AWS S3, create a custom role and specifically state which S3 API requires access. Back up and restore requires these permissions: **ListBucket** (Browse), **PutObject** (Write - for backup).
- In other S3-compatible storage, the user (`Access Key ID`) must have both **ListBucket** and **WriteOnly** permissions.

### Restore permissions

If the database being restored doesn't exist, the user must have `CREATE DATABASE` permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the `sysadmin` and `dbcreator` fixed server roles and the owner (`dbo`) of the database.

RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which isn't always the case when RESTORE is executed, members of the `db_owner` fixed database role don't have RESTORE permissions.

On the storage layer:

- In AWS S3, create a custom role and specifically state which S3 API requires access. Back up and restore requires these permissions: **ListBucket** (Browse), **GetObject** (Read - for restore).
- In other S3-compatible storage, the user (`Access Key ID`) must have both **ListBucket** and **ReadOnly** permissions.

## Supported features

High-level overview of the supported features for `BACKUP` and `RESTORE`:

1. A single backup file can be up to 200,000 MiB per URL (with `MAXTRANSFERSIZE` set to 20 MB).
1. Backups can be striped across a maximum of 64 URLs.
1. Mirroring is supported, but only across S3 URLs. Mirroring using both URL and DISK or S3 and Azure Storage isn't supported.
1. Compression is supported and recommended.
1. Encryption is supported.
1. Restore from URL with S3-compatible object storage has no size limitation.
1. When you're restoring a database, the `MAXTRANSFERSIZE` is determined by value assigned during the backup phase.
1. URLs can be specified either in virtual host or path style format.
1. `WITH CREDENTIAL` is supported.
1. `REGION` is supported and the default value is `us-east-1`.
1. `MAXTRANSFERSIZE` ranges from 5 MB to 20 MB. 10 MB is the default value for the S3 connector.

### Supported backup and restore statements

| Statement | S3 Endpoint | Notes |
| --- | --- | --- |
| `BACKUP` | Yes | |
| `RESTORE` | Yes | |
| `RESTORE FILELISTONLY` | Yes | |
| `RESTORE HEADERONLY` | Yes | |
| `RESTORE LABELONLY` | Yes | |
| `RESTORE VERIFYONLY` | Yes | |
| `RESTORE REWINDONLY` | No | |

### Supported arguments for backup

| `WITH` options | S3 Endpoint | Notes |
| --- | --- | --- |
| `BLOCKSIZE` | Yes | `MAXTRANSFERSIZE` determines the Part size. |
| `BUFFERCOUNT` | Yes | |
| `COMPRESSION` | Yes | |
| `COPY_ONLY` | Yes |  |
| `CREDENTIAL` | Yes |  |
| `DESCRIPTION` | Yes |  |
| `DIFFERENTIAL` | Yes |  |
| `ENCRYPTION` | Yes |  |
| `FILE_SNAPSHOT` | No | |
| `MAXTRANSFERSIZE` | Yes | From 5 MB (5,242,880 Bytes) to 20 MB (20,971,520 Bytes), default value is 10 MB (10,485,760 Bytes).|
| `MEDIADESCRIPTION` | Yes |  |
| `MEDIANAME` | Yes |  |
| `MIRROR TO` | Yes | Only works with another URL, `MIRROR` with `URL` and `DISK` isn't supported. |
| `NAME` | Yes |  |
| `NOFORMAT` / `FORMAT` | Yes |  |
| `NOINIT` / `INIT` | No | Appending isn't supported. To overwrite a backup, use `WITH FORMAT`. |
| `NO_CHECKSUM` / `CHECKSUM` | Yes |  |
| `NO_TRUNCATE` | Yes |  |
| `REGION` | Yes | Default value is `us-east-1`. Must be used with `BACKUP_OPTIONS`.|
| `STATS` | Yes |  |

### Supported arguments for restore

| `WITH` options | S3 Endpoint | Notes |
| --- | --- | --- |
| `BLOCKSIZE` | Yes | `MAXTRANSFERSIZE` determines the Part size. |
| `BUFFERCOUNT` | No |   |
| `CHECKSUM` / `NO_CHECKSUM` | Yes |   |
| `CREDENTIAL` | Yes |  |
| `ENABLE_BROKER` / `ERROR_BROKER_CONVERSATIONS` / `NEW_BROKER` | Yes |  |
| `FILE` | No | Logical names not supported with `RESTORE FROM URL`. |
| `FILESTREAM` | Yes |  |
| `KEEP_CDC` | Yes |  |
| `KEEP_REPLICATION` | Yes |  |
| `LOADHISTORY` | Yes |  |
| `MAXTRANSFERSIZE` | No |   |
| `MEDIANAME` | Yes |  |
| `MEDIAPASSWORD` | No | Required for some backups taken in versions before SQL Server 2012. |
| `MOVE` | Yes |  |
| `PARTIAL` | Yes |  |
| `PASSWORD` | No | Required for some backups taken in versions before SQL Server 2012. |
| `RECOVERY` / `NORECOVERY` / `STANDBY` | Yes |  |
| `REGION` | Yes | Default value is `us-east-1`. Must be used with `RESTORE_OPTIONS`.|
| `REPLACE` | Yes |  |
| `RESTART` | Yes |  |
| `RESTRICTED_USER` | Yes |  |
| `REWIND` / `NOREWIND` | No |  |
| `STATS` | Yes |  |
| `STOP_ON_ERROR` / `CONTINUE_AFTER_ERROR` | Yes |  |
| `STOPAT` / `STOPATMARK` / `STOPBEFOREMARK` | Yes |  |
| `UNLOAD` / `NOUNLOAD` | No |  |

<!-- | DBREADSIZE | No | Yes | DBREADSIZE is not available |-->
<!-- | WITH options | Blob Storage | S3 Connector | Notes |
| --- | --- | --- | --- |
| `BLOCKSIZE` | Yes | Yes |  |
| `BUFFERCOUNT` | Yes | Yes |  |
| `COMPRESSION` | Yes | Yes | |
| `COPY_ONLY` | Yes | Yes |  |
| `CREDENTIAL` | Yes | Yes |  |
| `DESCRIPTION` | Yes | Yes |  |
| `DIFFERENTIAL` | Yes | Yes |  |
| `ENCRYPTION` | Yes | Yes |  |
| `FILE_SNAPSHOT` | Yes | No | FILE_SNAPSHOT is only available in Azure |
| `MAXTRANSFERSIZE` | Yes | Yes | From 5 MB (5,242,880 Bytes) to 20 MB (20,971,520 Bytes), default value is 10 MB (10,485,760 Bytes)|
| `MEDIADESCRIPTION` | Yes | Yes |  |
| `MEDIANAME` | Yes | Yes |  |
| `NAME` | Yes | Yes |  |
| `NOFORMAT/FORMAT` | Yes | Yes |  |
| `NORECOVERY/STANDBY` | Yes | Yes |  |
| `NO_CHECKSUM/CHECKSUM` | Yes | Yes |  |
| `NO_TRUNCATE` | Yes | Yes |  |
| `REGION` | No | Yes | |
| `STATS` | Yes | Yes |  |
| DBREADSIZE | No | Yes | DBREADSIZE is not available |-->

<!--An example of virtual host style format is below:

```sql
's3://<bucketName>.<virtualHost>/<pathToBackup>/<backupFileName>'
```

An example of path style format is below:

```sql
's3://<domainName>/<bucketName>/<pathToBackup>/<backupFileName>'
```

For more information see [SQL Server back up to URL for S3-compatible storage best practices and troubleshooting](sql-server-backup-to-url-s3-best-practices-and-troubleshooting.md).
|-->

### Region

Your S3-compatible object storage provider can offer the ability to determine a specific region for the bucket location. The use of this optional parameter can provide more flexibility by specifying which region that particular bucket belongs to. This parameter requires the use of `WITH` together with either `BACKUP_OPTIONS` or `RESTORE_OPTIONS`. These options require the value to be declared in JSON format. This allows scenarios in which an S3-compatible storage provider can have the same universal URL but be distributed across several regions. In this case, the backup or restore command point to the specified regions without the need to change the URL.

If no value is declared, `us-east-1` is assigned as default.

Backup example:

```sql
WITH BACKUP_OPTIONS = '{"s3": {"region":"us-west-1"}}'
```

Restore example:

```sql
WITH RESTORE_OPTIONS = '{"s3": {"region":"us-west-1"}}'
```

### Linux support

SQL Server uses `WinHttp` to implement client of HTTP REST APIs it uses. It relies on OS certificate store for validations of the TLS certificates presented by the `http(s)` endpoint. However, SQL Server on Linux the CA must be placed on a predefined location to be created at `/var/opt/mssql/security/ca-certificates`, only the first 50 certificates can be stored and supported in this folder. The CA must be in place before SQL Server process is started.

SQL Server reads the certificates from the folder during startup and adds them to the trust store.

Only super user should be able to write in the folder, while the `mssql` user must be able to read.

## Unsupported features

- Back up to S3-compatible object storage with a nonsecure `http` URL isn't supported. Customers are responsible for setting up their S3 host with an `https` URL and this endpoint is validated by a certificate installed on the SQL Server OS host.
- Back up to S3-compatible object storage isn't supported in SQL Server Express and SQL Server Express with Advanced Services editions.

<!-- ## Notebooks

- [Introducing BACKUP TO URL](nb-backup-to-url.ipynb)
- [Introducing RESTORE FROM URL](nb-restore-from-url.ipynb)
 -->

## Limitations

The following are the current limitations of backup and restore with S3-compatible object storage:

- Due to the current limitation of S3 Standard REST API, the temporary uncommitted data files that are created in the customer's S3-compatible object store (due to an ongoing multipart upload operation) while the BACKUP T-SQL command is running, aren't removed if there are backup failures. These uncommitted data blocks continue to persist in S3-compatible object storage in the case the BACKUP T-SQL command fails or is canceled. If the backup succeeds, these temporary files are automatically removed by the object store to form the final backup file. Some S3-compatible storage providers handle temporary files through their garbage collector system.
- The total URL length is limited to 259 characters. The full string is counted in this limitation, including the `s3://` connector name. So, the usable limit is 254 characters. However, we recommend sticking to a limit of 200 characters to allow for possible introduction of query parameters.
- The SQL credential name is limited by 128 characters in UTF-16 format.
- Secret key ID must not have `:` character.

### Path style and virtual host style

Back up to S3 supports the URL to be written in both path style or virtual host style.

Path style example: `s3://<endpoint>:<port>/<bucket>/<backup_file_name>`

Virtual host example: `s3://<bucket>.<domain>/<backup_file_name>`

## Examples

### Create credential

- The IDENTITY should always be `'S3 Access Key'` when using the S3 connector.
- The Access Key ID and Secret Key ID must not contain a colon. Access Key ID and Secret Key ID is the user and password created on the S3-compatible object storage.
- Only alphanumeric values are allowed.
- The Access Key ID must have proper permissions on the S3-compatible object storage.

<!-- docs\t-sql\statements\create-credential-transact-sql.md -->
Use [CREATE CREDENTIAL](sql-server-backup-to-url-s3-compatible-object-storage-best-practices-and-troubleshooting.md#create-a-server-level-credential-before-running-backuprestore) to create a server-level credential for authentication with the S3-compatible object storage endpoint.

```sql
USE [master];
CREATE CREDENTIAL [s3://<endpoint>:<port>/<bucket>]
WITH
        IDENTITY    = 'S3 Access Key',
        SECRET      = '<AccessKeyID>:<SecretKeyID>';
GO

BACKUP DATABASE [SQLTestDB]
TO      URL = 's3://<endpoint>:<port>/<bucket>/SQLTestDB.bak'
WITH    FORMAT /* overwrite any existing backup sets */
,       STATS = 10
,       COMPRESSION;
```

However, AWS S3 supports two different standards of URL.

- `S3://<BUCKET_NAME>.S3.<REGION>.AMAZONAWS.COM/<FOLDER>` (default)
- `S3://S3.<REGION>.AMAZONAWS.COM/<BUCKET_NAME>/<FOLDER>`

There are multiple approaches to successfully creating a credential for AWS S3.

```sql
-- S3 bucket name: datavirtualizationsample
-- S3 bucket region: us-west-2
-- S3 bucket folder: backup

CREATE CREDENTIAL [s3://datavirtualizationsample.s3.us-west-2.amazonaws.com/backup]
WITH    
        IDENTITY    = 'S3 Access Key'
,       SECRET      = 'accesskey:secretkey';
GO

BACKUP DATABASE [AdventureWorks2022]
TO URL  = 's3://datavirtualizationsample.s3.us-west-2.amazonaws.com/backup/AdventureWorks2022.bak'
WITH COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
GO
```

Or,

```sql
CREATE CREDENTIAL [s3://s3.us-west-2.amazonaws.com/datavirtualizationsample/backup]
WITH    
        IDENTITY    = 'S3 Access Key'
,       SECRET      = 'accesskey:secretkey';
GO

BACKUP DATABASE [AdventureWorks2022]
TO URL  = 's3://s3.us-west-2.amazonaws.com/datavirtualizationsample/backup/AdventureWorks2022.bak'
WITH COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
GO
```

### Back up to URL

The following example performs a full database backup to the object storage endpoint, striped across multiple files:

```sql
BACKUP DATABASE <db_name>
TO      URL = 's3://<endpoint>:<port>/<bucket>/<database>_01.bak'
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_02.bak'
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_03.bak'
--
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_64.bak'
WITH    FORMAT -- overwrite
,       STATS               = 10
,       COMPRESSION;
```

### Restore from URL

The following example performs a database restore from the object storage endpoint location:

```sql
RESTORE DATABASE <db_name>
FROM    URL = 's3://<endpoint>:<port>/<bucket>/<database>_01.bak'
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_02.bak'
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_03.bak'
--
,       URL = 's3://<endpoint>:<port>/<bucket>/<database>_64.bak'
WITH    REPLACE -- overwrite
,       STATS  = 10;
```

### Options for encryption and compression

The following example shows how to back up and restore the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database with encryption, `MAXTRANSFERSIZE` as 20 MB and compression:

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = <password>;
GO

CREATE CERTIFICATE AdventureWorks2022Cert
    WITH SUBJECT = 'AdventureWorks2022 Backup Certificate';
GO
-- Backup database
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<endpoint>:<port>/<bucket>/AdventureWorks2022_Encrypt.bak'
WITH FORMAT, MAXTRANSFERSIZE = 20971520, COMPRESSION,
ENCRYPTION (ALGORITHM = AES_256, SERVER CERTIFICATE = AdventureWorks2022Cert)
GO

-- Restore database
RESTORE DATABASE AdventureWorks2022
FROM URL = 's3://<endpoint>:<port>/<bucket>/AdventureWorks2022_Encrypt.bak'
WITH REPLACE
```

### Use region for backup and restore

The following example shows how to back up and restore the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database using `REGION_OPTIONS`:

You can parameterize the region within each `BACKUP` / `RESTORE` command. Note the S3-specific region string in the `BACKUP_OPTIONS` and `RESTORE_OPTIONS`, for example, `'{"s3": {"region":"us-west-2"}}'`. The default region is `us-east-1`. A simple example:

```sql
-- Backup Database
BACKUP DATABASE AdventureWorks2022
TO URL = 's3://<endpoint>:<port>/<bucket>/AdventureWorks2022.bak'
WITH BACKUP_OPTIONS = '{"s3": {"region":"us-west-2"}}'

-- Restore Database
RESTORE DATABASE AdventureWorks2022
FROM URL = 's3://<endpoint>:<port>/<bucket>/AdventureWorks2022.bak'
WITH 
  MOVE 'AdventureWorks2022' 
  TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022.mdf'
, MOVE 'AdventureWorks2022_log' 
  TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022.ldf'
, RESTORE_OPTIONS = '{"s3": {"region":"us-west-2"}}'
```

For example:

```sql
-- S3 bucket name: datavirtualizationsample
-- S3 bucket region: us-west-2
-- S3 bucket folder: backup

CREATE CREDENTIAL   [s3://datavirtualizationsample.s3.amazonaws.com/backup]
WITH    
        IDENTITY    = 'S3 Access Key'
,       SECRET      = 'accesskey:secretkey';
GO

BACKUP DATABASE [AdventureWorks2022]
TO URL  = 's3://datavirtualizationsample.s3.amazonaws.com/backup/AdventureWorks2022.bak'
WITH
    BACKUP_OPTIONS = '{"s3": {"region":"us-west-2"}}' -- REGION AS PARAMETER)
, COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
GO

RESTORE DATABASE AdventureWorks2022_1 
FROM URL = 's3://datavirtualizationsample.s3.amazonaws.com/backup/AdventureWorks2022.bak'
WITH 
  MOVE 'AdventureWorks2022' 
  TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_1.mdf'
, MOVE 'AdventureWorks2022_log' 
  TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_1.ldf'
, STATS = 10, RECOVERY
, REPLACE, RESTORE_OPTIONS = '{"s3": {"region":"us-west-2"}}'; -- REGION AS PARAMETER)
GO
```

## Related content

- [SQL Server back up to URL for S3-compatible object storage best practices and troubleshooting](sql-server-backup-to-url-s3-compatible-object-storage-best-practices-and-troubleshooting.md)
- [SQL Server back up to URL for Microsoft Azure Blob Storage best practices and troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [SQL Server backup to URL for Microsoft Azure Blob Storage](sql-server-backup-to-url.md)
