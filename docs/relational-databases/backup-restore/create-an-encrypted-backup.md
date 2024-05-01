---
title: "Create an encrypted backup"
description: This article shows you how to create an encrypted backup in SQL Server using Transact-SQL. You can back up to disk or to Azure Storage.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
---
# Create an encrypted backup

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the steps necessary to create an encrypted backup using Transact-SQL. For an example using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [Create a Full Database Backup](create-a-full-database-backup-sql-server.md).

> [!CAUTION]  
> To restore an encrypted database, you need access to the certificate or asymmetric key used to encrypt that database. Without the certificate or asymmetric key, you can't restore that database. Save the certificate used to encrypt the database encryption key for as long as you need to save the backup. For more information, see [SQL Server Certificates and Asymmetric Keys](../security/sql-server-certificates-and-asymmetric-keys.md).

## Prerequisites

- Storage for the encrypted backup. Depending on which option you choose, one of:

  - A local disk or to storage with adequate space to create a backup of the database.
  - An Azure Storage account and a container. For more information, see [Create a storage account](/azure/storage/common/storage-account-create).

- A database master key (DMK) for the `master` database, and a certificate or asymmetric key on the instance of SQL Server. For encryption requirements and permissions, see [Backup encryption](backup-encryption.md).

## Create a database master key (DMK)

Choose a password for encrypting the copy of the DMK that will be stored in the database. Connect to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], start a new query window, copy and paste the following example, and select **Execute**.

Replace `<master key password>` with a strong password, and make sure you keep a copy of both the DMK and the password in a secure location.

```sql
USE master;
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master key password>';
GO
```

## Create a backup certificate

Create a backup certificate in the `master` database. Copy and paste the following example into the query window and select **Execute**.

```sql
Use master;
GO

CREATE CERTIFICATE MyTestDBBackupEncryptCert
    WITH SUBJECT = 'MyTestDB Backup Encryption Certificate';
GO
```

## Back up the database with encryption

There are two main options for creating an encrypted backup:

- Back up to disk
- Back up to Azure Storage

## [Back up to disk](#tab/local)

Use the following steps to create an encrypted backup of a database to a local disk. This example uses a user database called `MyTestDB`.

Specify the encryption algorithm and certificate to use. Copy and paste the following example into the query window and select **Execute**.

Replace `<path_to_local_backup>` to a local path that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] has permission to write to. For example, this path might be `D:\SQLBackup`.

```sql
BACKUP DATABASE [MyTestDB]
TO DISK = N'<path_to_local_backup>\MyTestDB.bak'
WITH
COMPRESSION,
ENCRYPTION (
    ALGORITHM = AES_256,
    SERVER CERTIFICATE = MyTestDBBackupEncryptCert
),
STATS = 10;
GO
```

For an example of encrypting a backup protected by Extensible Key Management (EKM), see [Extensible Key Management Using Azure Key Vault (SQL Server)](../security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).

## [Back up to Azure Storage](#tab/azure)

If you're creating a backup to Azure storage using the **SQL Server Backup to URL** option, the encryption steps are the same, but you must use URL as the destination and a SQL Credential to authenticate to the Azure storage. If you want to configure [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] with encryption options, see [Enable SQL Server managed backup to Azure](enable-sql-server-managed-backup-to-microsoft-azure.md).

### Create SQL Server credential

To create a SQL Server credential, connect to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], open a new query window, and copy and paste the following example and select **Execute**.

Replace `<mystorageaccount>` with the name of the storage account you specified when creating a storage account, and `<storage account access key>` with either the Primary or Secondary Access Key for the storage account.

```sql
CREATE CREDENTIAL mycredential
WITH IDENTITY = '<mystorageaccount>',
SECRET = '<storage account access key>';
```

### Back up the database

Specify the encryption algorithm and the certificate to use. Copy and paste the following example into the query window and select **Execute**.

In this example, `mycredential` is the name of the credential created previously, `<mystorageaccountname>` is the name of your storage account, and `<mycontainername>` is your storage container name.

```sql
BACKUP DATABASE AdventureWorks2022
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2022.bak'
WITH CREDENTIAL = 'mycredential',
COMPRESSION,
ENCRYPTION (
    ALGORITHM = AES_256,
    SERVER CERTIFICATE = MyTestDBBackupEncryptCert
),
STATS = 10;
GO
```

---

## Related content

- [Restore a Database Backup Using SSMS](restore-a-database-backup-using-ssms.md)
- [Encryption Hierarchy](../security/encryption/encryption-hierarchy.md)
- [Backup overview (SQL Server)](backup-overview-sql-server.md)
