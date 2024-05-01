---
title: Transparent data encryption (TDE)
description: Learn about transparent data encryption, which encrypts SQL Server, Azure SQL Database, and Azure Synapse Analytics data, known as encrypting data at rest.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 01/19/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Transparent data encryption"
  - "database encryption key, about"
  - "TDE"
  - "database encryption key"
  - "TDE, about"
  - "Transparent data encryption, about"
  - "encryption [SQL Server], Transparent data encryption"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Transparent data encryption (TDE)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Transparent data encryption (TDE) encrypts [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuresynapse-md](../../../includes/ssazuresynapse-md.md)] data files. This encryption is known as encrypting data at rest.

To help secure a user database, you can take precautions like:

- Designing a secure system.
- Encrypting confidential assets.
- Building a firewall around the database servers.

However, a malicious party who steals physical media like drives or backup tapes can restore or attach the database and browse its data.

One solution is to encrypt sensitive data in a database and use a certificate to protect the keys that encrypt the data. This solution prevents anyone without the keys from using the data. But you must plan this kind of protection in advance.

TDE does real-time I/O encryption and decryption of data and log files. The encryption uses a database encryption key (DEK). The database boot record stores the key for availability during recovery. The DEK is a symmetric key, and is secured by a certificate that the server's `master` database stores or by an asymmetric key that an EKM module protects.

TDE protects data at rest, which is the data and log files. It lets you follow many laws, regulations, and guidelines established in various industries. This ability lets software developers encrypt data by using AES and 3DES encryption algorithms without changing existing applications.

> [!NOTE]  
> TDE isn't available for system databases. It can't be used to encrypt `master`, `model`, or `msdb`. `tempdb` is automatically encrypted when a user database enabled TDE, but can't be encrypted directly.

TDE doesn't provide encryption across communication channels. For more information about how to encrypt data across communication channels, see [Configure SQL Server Database Engine for encrypting connections](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

**Related topics:**

- [Transparent data encryption for SQL Database, SQL Managed Instance, and Azure Synapse Analytics](/azure/azure-sql/database/transparent-data-encryption-tde-overview)
- [Get started with transparent data encryption (TDE) in Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-encryption-tde-tsql)
- [Move a TDE protected database to another SQL Server](move-a-tde-protected-database-to-another-sql-server.md)
- [Enable TDE on SQL Server Using EKM](enable-tde-on-sql-server-using-ekm.md)
- [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md)
- [The SQL Server Security Blog on TDE with FAQ](/archive/blogs/sqlsecurity/feature-spotlight-transparent-data-encryption-tde)

## About TDE

Encryption of a database file is done at the page level. The pages in an encrypted database are encrypted before they're written to disk and are decrypted when read into memory. TDE doesn't increase the size of the encrypted database.

### Information applicable to [!INCLUDE [ssSDS](../../../includes/sssds-md.md)]

When you use TDE with Azure SQL Database, SQL Database automatically creates the server-level certificate stored in the `master` database. To move a TDE database on [!INCLUDE [ssSDS](../../../includes/sssds-md.md)], you don't have to decrypt the database for the move operation. For more information on using TDE with [!INCLUDE [ssSDS](../../../includes/sssds-md.md)], see [transparent data encryption with Azure SQL Database](/azure/azure-sql/database/transparent-data-encryption-tde-overview).

### Information applicable to [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]

After you secure a database, you can restore it by using the correct certificate. For more information about certificates, see [SQL Server Certificates and Asymmetric Keys](../sql-server-certificates-and-asymmetric-keys.md).

After you enable TDE, immediately back up the certificate and its associated private key. If the certificate becomes unavailable, or if you restore or attach the database on another server, you need backups of the certificate and private key. Otherwise, you can't open the database. Certificates stored in a [contained system database](../../../database-engine/availability-groups/windows/contained-availability-groups-overview.md) should also be backed up.

Keep the encrypting certificate even if you've disabled TDE on the database. Although the database isn't encrypted, parts of the transaction log might remain protected. You also might need the certificate for some operations until you do a full database backup.

You can still use a certificate that exceeds its expiration date to encrypt and decrypt data with TDE.

### Encryption hierarchy

The Windows Data Protection API (DPAPI) is at the root of the encryption tree, secures the key hierarchy at the machine level, and is used to protect the service master key (SMK) for the database server instance. The SMK protects the database master key (DMK), which is stored at the user database level and protects certificates and asymmetric keys. These keys, in turn, protect symmetric keys, which protect the data. TDE uses a similar hierarchy down to the certificate. When you use TDE, the DMK and certificate must be stored in the `master` database. A new key, used only for TDE and referred to as the database encryption key (DEK), is created and stored in the user database.

The following illustration shows the architecture of TDE encryption. Only the database-level items (the database encryption key and `ALTER DATABASE` portions) are user-configurable when you use TDE on [!INCLUDE [ssSDS](../../../includes/sssds-md.md)].

:::image type="content" source="media/transparent-data-encryption/tde-architecture.png" alt-text="Diagram showing the transparent data encryption architecture." lightbox="media/transparent-data-encryption/tde-architecture.png":::

## Enable TDE

To use TDE, follow these steps.

**Applies to**: [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

1. Create a master key.
1. Create or obtain a certificate protected by the master key.
1. Create a database encryption key and protect it by using the certificate.
1. Set the database to use encryption.

The following example shows encryption and decryption of the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] database using a certificate named `MyServerCert` that's installed on the server.

```sql
USE master;
GO
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<UseStrongPasswordHere>';
GO
CREATE CERTIFICATE MyServerCert WITH SUBJECT = 'My DEK Certificate';
GO
USE AdventureWorks2022;
GO
CREATE DATABASE ENCRYPTION KEY
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE MyServerCert;
GO
ALTER DATABASE AdventureWorks2022
SET ENCRYPTION ON;
GO
```

The encryption and decryption operations are scheduled on background threads by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. To view the status of these operations, use the catalog views and dynamic management views in the table that appears later in this article.

> [!CAUTION]  
> Backup files for databases that have TDE enabled are also encrypted with the DEK. As a result, when you restore these backups, the certificate that protects the DEK must be available. Therefore, in addition to backing up the database, make sure to maintain backups of the server certificates. Data loss results if the certificates are no longer available.
>  
> For more information, see [SQL Server Certificates and Asymmetric Keys](../sql-server-certificates-and-asymmetric-keys.md).

## Commands and functions

For the following statements to accept TDE certificates, use a database master key to encrypt them. If you encrypt them by password only, the statements reject them as encryptors.

> [!IMPORTANT]  
> If you make the certificates password protected after TDE uses them, the database becomes inaccessible after a restart.

The following table provides links and explanations of TDE commands and functions:

| Command or function | Purpose |
| --- | --- |
| [CREATE DATABASE ENCRYPTION KEY](../../../t-sql/statements/create-database-encryption-key-transact-sql.md) | Creates a key that encrypts a database |
| [ALTER DATABASE ENCRYPTION KEY](../../../t-sql/statements/alter-database-encryption-key-transact-sql.md) | Changes the key that encrypts a database |
| [DROP DATABASE ENCRYPTION KEY](../../../t-sql/statements/drop-database-encryption-key-transact-sql.md) | Removes the key that encrypts a database |
| [ALTER DATABASE SET Options](../../../t-sql/statements/alter-database-transact-sql-set-options.md) | Explains the `ALTER DATABASE` option that is used to enable TDE |

## Catalog views and dynamic management views

The following table shows TDE catalog views and dynamic management views (DMV).

| Catalog view or dynamic management view | Purpose |
| --- | --- |
| [sys.databases](../../system-catalog-views/sys-databases-transact-sql.md) | Catalog view that displays database information |
| [sys.certificates](../../system-catalog-views/sys-certificates-transact-sql.md) | Catalog view that shows the certificates in a database |
| [sys.dm_database_encryption_keys](../../system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) | Dynamic management view that provides information about a database's encryption keys and state of encryption |

## Permissions

Each TDE feature and command has individual permission requirements as described in the tables shown earlier.

Viewing the metadata involved with TDE requires the VIEW DEFINITION permission on a certificate.

## Considerations

While a re-encryption scan for a database encryption operation is in progress, maintenance operations to the database are disabled. You can use the single-user mode setting for the database to do maintenance operations. For more information, see [Set a database to single-user mode](../../databases/set-a-database-to-single-user-mode.md).

Use the `sys.dm_database_encryption_keys` dynamic management view to find the state of database encryption. For more information, see the [Catalog views and dynamic management views](#catalog-views-and-dynamic-management-views) section earlier in this article.

In TDE, all files and filegroups in a database are encrypted. If any filegroup in a database is marked `READ ONLY`, the database encryption operation fails.

If you use a database in database mirroring or log shipping, both databases are encrypted. The log transactions are encrypted when sent between them.

> [!IMPORTANT]  
> Full-text indexes are encrypted when a database is set for encryption. Such indexes created in [!INCLUDE [ssversion2005-md](../../../includes/ssversion2005-md.md)] and earlier versions, are imported into the database by [!INCLUDE [sql2008-md](../../../includes/sql2008-md.md)] and later versions, and are encrypted by TDE.

> [!TIP]  
> To monitor changes in the TDE status of a database, use [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Audit or [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] auditing. For [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], TDE is tracked under the audit action group `DATABASE_OBJECT_CHANGE_GROUP`, which you can find in [SQL Server Audit Action Groups and Actions](../auditing/sql-server-audit-action-groups-and-actions.md).

## Limitations

The following operations are disallowed during initial database encryption, key change, or database decryption:

- Dropping a file from a filegroup in a database
- Dropping a database
- Taking a database offline
- Detaching a database
- Transitioning a database or filegroup into a `READ ONLY` state

The following operations are disallowed during the `CREATE DATABASE ENCRYPTION KEY`, `ALTER DATABASE ENCRYPTION KEY`, `DROP DATABASE ENCRYPTION KEY`, and `ALTER DATABASE...SET ENCRYPTION` statements:

- Dropping a file from a filegroup in a database
- Dropping a database
- Taking a database offline
- Detaching a database
- Transitioning a database or filegroup into a `READ ONLY` state
- Using an `ALTER DATABASE` command
- Starting a database or database-file backup
- Starting a database or database-file restore
- Creating a snapshot

The following operations or conditions prevent the `CREATE DATABASE ENCRYPTION KEY`, `ALTER DATABASE ENCRYPTION KEY`, `DROP DATABASE ENCRYPTION KEY`, and `ALTER DATABASE...SET ENCRYPTION` statements:

- A database is read-only or has read-only filegroups.
- An `ALTER DATABASE` command is running.
- A data backup is running.
- A database is in an offline or restore condition.
- A snapshot is in progress.
- Database maintenance tasks are running.

When database files are created, instant file initialization is unavailable when TDE is enabled.

To encrypt a DEK with an asymmetric key, the asymmetric key must be on an extensible key-management provider.

## TDE scan

To enable TDE on a database, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] must do an encryption scan. The scan reads each page from the data files into the buffer pool and then writes the encrypted pages back out to disk.

To give you more control over the encryption scan, [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)] introduces TDE scan, which has a suspend and resume syntax. You can pause the scan while the workload on the system is heavy or during business-critical hours and then resume the scan later.

Use the following syntax to pause the TDE encryption scan:

```sql
ALTER DATABASE <db_name> SET ENCRYPTION SUSPEND;
```

Similarly, use the following syntax to resume the TDE encryption scan:

```sql
ALTER DATABASE <db_name> SET ENCRYPTION RESUME;
```

The encryption_scan_state column has been added to the `sys.dm_database_encryption_keys` dynamic management view. It shows the current state of the encryption scan. There's also a new column called encryption_scan_modify_date, which contains the date and time of the last encryption-scan state change.

If the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance restarts while its encryption scan is suspended, a message is logged in the error log during startup. The message indicates that an existing scan has been paused.

> [!IMPORTANT]  
> Suspend and Resume TDE scan feature isn't currently available in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics.

## TDE and transaction logs

TDE protects data files and log files at rest. Encrypting the entire database after enabling TDE on an unencrypted database is a sizable data operation and the time it takes depends on the system resources on which this database is running. The [sys.dm_database_encryption_keys](../../system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) DMV can be used to determine the encryption state of a database.

When TDE is turned on, the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] forces the creation of a new transaction log, which will be encrypted by the database encryption key. Any log generated by previous transactions or current long running transactions interleaved between the TDE state change isn't encrypted.

The transaction logs can be monitored using the [sys.dm_db_log_info](../../system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md) DMV, which also shows whether the log file is encrypted or not using the `vlf_encryptor_thumbprint` column that is available in Azure SQL, and [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and later versions. To find the encryption status of the log file using the `encryption_state` column in the `sys.dm_database_encryption_keys` view, here is a sample query:

```sql
USE AdventureWorks2022;
GO
/* The value 3 represents an encrypted state
   on the database and transaction logs. */
SELECT *
FROM sys.dm_database_encryption_keys
WHERE encryption_state = 3;
GO
```

For more information about the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] log-file architecture, see [The transaction log](../../logs/the-transaction-log-sql-server.md).

Before a DEK changes, the previous DEK encrypts all data written to the transaction log.

If you change a DEK twice, you must do a log backup before you can change the DEK again.

## TDE and the tempdb system database

The `tempdb` system database is encrypted if any other database on the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance is encrypted by using TDE. This encryption might have a performance effect for unencrypted databases on the same [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance. For more information about the `tempdb` system database, see [tempdb Database](../../databases/tempdb-database.md).

## TDE and replication

Replication doesn't automatically replicate data from a TDE-enabled database in an encrypted form. Separately enable TDE if you want to protect distribution and subscriber databases.

Snapshot replication can store data in unencrypted intermediate files like BCP files. The initial data distribution for transactional and merge replication can too. During such replication, you can enable encryption to protect the communication channel.

For more information, see [Configure SQL Server Database Engine for encrypting connections](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

## TDE and availability groups

You can [add an encrypted database to an Always On availability group](../../../database-engine/availability-groups/windows/encrypted-databases-with-always-on-availability-groups-sql-server.md).

To encrypt databases that are part of an availability group, create the master key and certificates, or asymmetric key (EKM) on all secondary replicas before creating the [database encryption key](../../../t-sql/statements/create-database-encryption-key-transact-sql.md) on the primary replica.

If a certificate is used to protect the DEK, [back up the certificate](../../../t-sql/statements/backup-certificate-transact-sql.md) created on the primary replica, and then [create the certificate from a file](../../../t-sql/statements/create-certificate-transact-sql.md) on all secondary replicas before creating the DEK on the primary replica.

## TDE and FILESTREAM data

FILESTREAM data isn't encrypted, even when you enable TDE.

## <a id="scan-suspend-resume"></a> TDE and backups

Certificates are commonly used in Transparent Data Encryption to protect the DEK. The certificate must be created in the `master` database. Backup files of databases that have TDE enabled, are also encrypted by using the DEK. As a result, when you restore from these backups, the certificate protecting the DEK must be available. This means that in addition to backing up the database, you must maintain backups of the server certificates to prevent data loss. Data loss occurs if the certificate is no longer available.

## Remove TDE

Remove encryption from the database by using the `ALTER DATABASE` statement.

```sql
ALTER DATABASE <db_name> SET ENCRYPTION OFF;
```

To view the state of the database, use the [sys.dm_database_encryption_keys](../../system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

Wait for decryption to finish before removing the DEK by using [DROP DATABASE ENCRYPTION KEY](../../../t-sql/statements/drop-database-encryption-key-transact-sql.md).

> [!IMPORTANT]  
> Back up the master key and certificate that are used for TDE to a safe location. The master key and certificate are required to restore backups that were taken when the database was encrypted with TDE. After you remove the DEK, take a log backup followed by a fresh full backup of the decrypted database.

## TDE and the buffer pool extension

When you encrypt a database using TDE, files related to buffer pool extension (BPE) aren't encrypted. For those files, use encryption tools like BitLocker or EFS at the file-system level.

## TDE and In-Memory OLTP

You can enable TDE on a database that has In-Memory OLTP objects. In [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] and [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)], In-Memory OLTP log records and data are encrypted if you enable TDE. In [!INCLUDE [ssSQL14](../../../includes/sssql14-md.md)], In-Memory OLTP log records are encrypted if you enable TDE, but files in the `MEMORY_OPTIMIZED_DATA` filegroup are unencrypted.

## Guidelines on managing certificates used in TDE

You must back up the certificate and database master key when the database is enabled for TDE and is used in log shipping or database mirroring. Certificates stored in a contained system database should also be backed up.

The certificate used to protect the DEK should never be dropped from the `master` database. Doing so causes the encrypted database to become inaccessible.

A message like the following one (error 33091) is raised after executing `CREATE DATABASE ENCRYPTION KEY` if the certificate used in the command hasn't been backed up already.

> [!Warning]
> The certificate used for encrypting the database encryption key has not been backed up. You should immediately back up the certificate and the private key associated with the certificate. If the certificate ever becomes unavailable or if you must restore or attach the database on another server, you must have backups of both the certificate and the private key or you will not be able to open the database.

The following query can be used to identify the certificates used in TDE that haven't been backed up from the time it was created.

```sql
SELECT pvt_key_last_backup_date,
    Db_name(dek.database_id) AS encrypteddatabase,
    c.name AS Certificate_Name
FROM sys.certificates c
INNER JOIN sys.dm_database_encryption_keys dek
    ON c.thumbprint = dek.encryptor_thumbprint;
```

If the column `pvt_key_last_backup_date` is `NULL`, the database corresponding to that row has been enabled for TDE, but the certificate used to protect its DEK hasn't been backed up. For more information on backing up a certificate, see [BACKUP CERTIFICATE](../../../t-sql/statements/backup-certificate-transact-sql.md).

## Related content

- [Security for SQL Server Database Engine and Azure SQL Database](../security-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [FILESTREAM (SQL Server)](../../blob/filestream-sql-server.md)
- [SQL Server encryption](sql-server-encryption.md)
- [SQL Server and Database Encryption Keys (Database Engine)](sql-server-and-database-encryption-keys-database-engine.md)
