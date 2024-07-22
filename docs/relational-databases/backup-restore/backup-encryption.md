---
title: "Backup encryption"
description: This article describes encryption options for SQL Server backups, including the usage, benefits, and recommended practices for encrypting during backup.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/19/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
---
# Backup encryption

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article provides an overview of the encryption options for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups. It includes details of the usage, benefits, and recommended practices for encrypting during backup.

## Overview

Starting in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], SQL Server has the ability to encrypt the data while creating a backup. By specifying the encryption algorithm and the encryptor (a Certificate or Asymmetric Key) when creating a backup, you can create an encrypted backup file. All storage destinations: on-premises and Azure storage are supported. In addition, encryption options can be configured for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] operations, a new feature introduced in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)].

To encrypt during backup, you must specify an encryption algorithm, and an encryptor to secure the encryption key. The following are the supported encryption options:

- **Encryption Algorithm:** The supported encryption algorithms are: AES 128, AES 192, AES 256, and Triple DES

- **Encryptor:** A certificate or asymmetric Key

> [!CAUTION]  
> It's very important to back up the certificate or asymmetric key, and preferably to a different location than the backup file it was used to encrypt. Without the certificate or asymmetric key, you can't restore the backup, rendering the backup file unusable. Certificates stored in a [contained system database](../../database-engine/availability-groups/windows/contained-availability-groups-overview.md) should also be backed up.

**Restoring the encrypted backup:** SQL Server restore doesn't require any encryption parameters to be specified during restores. It does require that the certificate or the asymmetric key used to encrypt the backup file is available on the instance that you're restoring to. The user account performing the restore must have `VIEW DEFINITION` permissions on the certificate or key. If you're restoring the encrypted backup to a different instance, you must make sure that the certificate is available on that instance.  
The sequence to restore an encrypted database to a new location is to:

1. [BACKUP CERTIFICATE (Transact-SQL)](../../t-sql/statements/backup-certificate-transact-sql.md) in the old database
1. [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md) in the new location `master` database
1. [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md) from the backup certificate of the old database imported to a location on the new server
1. [Restore a database to a new location (SQL Server)](restore-a-database-to-a-new-location-sql-server.md)

If you're restoring a backup from a TDE encrypted database, the TDE certificate should be available on the instance you're restoring to. For more information, see [Move a TDE protected database to another SQL Server](../security/encryption/move-a-tde-protected-database-to-another-sql-server.md).

## Benefits

1. Encrypting the database backups helps secure the data: SQL Server provides the option to encrypt the backup data while creating a backup.

1. Encryption can also be used for databases that are encrypted using TDE.

1. Encryption is supported for backups done by [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)], which provides additional security for off-site backups.

1. This feature supports multiple encryption algorithms up to AES 256 bit. This gives you the option to select an algorithm that aligns with your requirements.

1. You can integrate encryption keys with Extensible Key Management (EKM) providers.

## Prerequisites

The following are prerequisites for encrypting a backup:

1. **Create a database master key for the `master` database:** The database master key (DMK) is a symmetric key that is used to protect the private keys of certificates and asymmetric keys that are present in the database. For more information, see [SQL Server and Database Encryption Keys (Database Engine)](../security/encryption/sql-server-and-database-encryption-keys-database-engine.md).

1. Create a certificate or asymmetric key to use for backup encryption. For more information on creating a certificate, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md). For more information on creating an asymmetric key, see [CREATE ASYMMETRIC KEY (Transact-SQL)](../../t-sql/statements/create-asymmetric-key-transact-sql.md).

   > [!IMPORTANT]  
   > Only asymmetric keys residing in an Extensible Key Management (EKM) are supported.

## <a id="Restrictions"></a> Limitations

The following are restrictions that apply to the encryption options:

- If you're using asymmetric key to encrypt the backup data, only asymmetric keys residing in the EKM provider are supported.

- SQL Server Express and SQL Server Web don't support encryption during backup. However restoring from an encrypted backup to an instance of SQL Server Express or SQL Server Web is supported.

- Previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't read encrypted backups.

- Appending to an existing backup set option isn't supported for encrypted backups.

## Permissions

The account that does backup operations on an encrypted database requires specific permissions.

- **db_backupoperator** database level role on the database being backed up. This is required regardless of encryption.
- `VIEW DEFINITION` permission on the certificate in `master` database.

   The following example grants the appropriate permissions for the certificate.

   ```sql
   USE [master]
   GO
   GRANT VIEW DEFINITION ON CERTIFICATE::[<SERVER_CERT>] TO [<db_account>]
   GO
   ```

> [!NOTE]  
> Access to the TDE certificate isn't required to back up or restore a TDE protected database.

## <a id="Methods"></a> Backup encryption methods

The following sections provide a brief introduction to the steps to encrypting the data during backup. For a complete walkthrough of the different steps of encrypting your backup using Transact-SQL, see [Create an Encrypted Backup](create-an-encrypted-backup.md).

### Use SQL Server Management Studio

You can encrypt a backup when creating the backup of a database in any of the following dialog boxes:

1. [Back Up Database (Backup Options Page)](back-up-database-backup-options-page.md) On the **Backup Options** page, you can select **Encryption**, and specify the encryption algorithm and the certificate or asymmetric key to use for the encryption.

1. [Using Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md#SSMSProcedure) When you select a backup task, on the **Options** tab of the **Define Backup ()Task** page, you can select **Backup Encryption**, and specify the encryption algorithm and the certificate or key to use for the encryption.

### Use Transact-SQL

Following is a sample Transact-SQL statement to encrypt the backup file:

```sql
BACKUP DATABASE [MYTestDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\MyTestDB.bak'
WITH
  COMPRESSION,
  ENCRYPTION
   (
   ALGORITHM = AES_256,
   SERVER CERTIFICATE = BackupEncryptCert
   ),
  STATS = 10
GO
```

For the full Transact-SQL statement syntax, see [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md).

### Use PowerShell

This example creates the encryption options and uses it as a parameter value in `Backup-SqlDatabase` cmdlet to create an encrypted backup.

```powershell
$encryptionOption = New-SqlBackupEncryptionOption -Algorithm Aes256 -EncryptorType ServerCertificate -EncryptorName "BackupCert"

Backup-SqlDatabase -ServerInstance . -Database "<myDatabase>" -BackupFile "<myDatabase>.bak" -CompressionOption On -EncryptionOption $encryptionOption
```

## Recommended practices

Create a backup of the encryption certificate and keys to a location other than your local machine where the instance is installed. To account for disaster recovery scenarios, consider storing a backup of the certificate or key to an off-site location. You can't restore an encrypted backup without the certificate used to encrypt the backup.

To restore an encrypted backup, the original certificate used when the backup was taken with the matching thumbprint should be available on the instance you're restoring to. Therefore, the certificate shouldn't be renewed on expiry or changed in any way. Renewal can result in updating the certificate triggering the change of the thumbprint, therefore making the certificate invalid for the backup file. The account performing the restore should have VIEW DEFINITION permissions on the certificate or the asymmetric key used to encrypt during backup.

Availability Group database backups are typically performed on the preferred backup replica. If you restore a backup on a replica other than where the backup was taken from, ensure that the original certificate used for backup is available on the replica you're restoring to.

If the database is TDE enabled, choose different certificates or asymmetric keys for encrypting the database and the backup to increase security.

## Related content

- [Create an Encrypted Backup](create-an-encrypted-backup.md)
- [Extensible Key Management Using Azure Key Vault (SQL Server)](../security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)
- [Backup overview (SQL Server)](backup-overview-sql-server.md)
