---
title: "Backup Encryption | Microsoft Docs"
description: This article describes encryption options for SQL Server backups, including the usage, benefits, and recommended practices for encrypting during backup.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 334b95a8-6061-4fe0-9e34-b32c9f1706ce
author: MashaMSFT
ms.author: mathoma
---
# Backup Encryption
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic provides an overview of the encryption options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. It includes details of the usage, benefits, and recommended practices for encrypting during backup.  

## <a name="Overview"></a> Overview  
 Starting in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], SQL Server has the ability to encrypt the data while creating a backup. By specifying the encryption algorithm and the encryptor (a Certificate or Asymmetric Key) when creating a backup, you can create an encrypted backup file. All storage destinations: on-premises and Window Azure storage are supported. In addition, encryption options can be configured for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] operations, a new feature introduced in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
 To encrypt during backup, you must specify an encryption algorithm, and an encryptor to secure the encryption key. The following are the supported encryption options:  
  
- **Encryption Algorithm:** The supported encryption algorithms are: AES 128, AES 192, AES 256, and Triple DES  
  
- **Encryptor:** A certificate or asymmetric Key  
  
> [!CAUTION]  
> It is very important to back up the certificate or asymmetric key, and preferably to a different location than the backup file it was used to encrypt. Without the certificate or asymmetric key, you cannot restore the backup, rendering the backup file unusable.  
  
 **Restoring the encrypted backup:** SQL Server restore does not require any encryption parameters to be specified during restores. It does require that the certificate or the asymmetric key used to encrypt the backup file be available on the instance that you are restoring to. The user account performing the restore must have **VIEW DEFINITION** permissions on the certificate or key. If you are restoring the encrypted backup to a different instance, you must make sure that the certificate is available on that instance.  
The sequence to restore an encrypted database to a new location is to:

1. [BACKUP CERTIFICATE (Transact-SQL)](../../t-sql/statements/backup-certificate-transact-sql.md) in the old database
1. [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md) in the new location master database
1. [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md) from the backup certificate of the old database imported to a location on the new server
1. [Restore a Database to a New Location (SQL Server)](../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)

 If you are restoring a backup from a TDE encrypted database, the TDE certificate should be available on the instance you are restoring to. For more information, see [Move a TDE Protected Database to Another SQL Server](../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md).
  
##  <a name="Benefits"></a> Benefits  
  
1. Encrypting the database backups helps secure the data: SQL Server provides the option to encrypt the backup data while creating a backup.  
  
1. Encryption can also be used for databases that are encrypted using TDE.  
  
1. Encryption is supported for backups done by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)], which provides additional security for off-site backups.  
  
1. This feature supports multiple encryption algorithms up to AES 256 bit. This gives you the option to select an algorithm that aligns with your requirements.  
  
1. You can integrate encryption keys with Extended Key Management (EKM) providers.  
 
##  <a name="Prerequisites"></a> Prerequisites  
 The following are prerequisites for encrypting a backup:  
  
1. **Create a Database Master Key for the master database:** The database master key is a symmetric key that is used to protect the private keys of certificates and asymmetric keys that are present in the database. For more information, see [SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md).  
  
1. Create a certificate or asymmetric Key to use for backup encryption. For more information on creating a certificate, see [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md). For more information on creating an asymmetric key, see [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md).  
  
    > [!IMPORTANT]  
    >  Only asymmetric keys residing in an Extended Key Management (EKM) are supported.  
  
##  <a name="Restrictions"></a> Restrictions  
 The following are restrictions that apply to the encryption options:  
  
- If you are using asymmetric key to encrypt the backup data, only asymmetric keys residing in the EKM provider are supported.  
  
- SQL Server Express and SQL Server Web do not support encryption during backup. However restoring from an encrypted backup to an instance of SQL Server Express or SQL Server Web is supported.  
  
- Previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot read encrypted backups.  
  
- Appending to an existing backup set option is not supported for encrypted backups.  

##  <a name="Permissions"></a> Permissions  

The account that does backup operations on an encrypted database requires specific permissions. 

- **db_backupoperator** database level role on the database being backed up. This is required regardless of encryption. 
- **VIEW DEFINITION** permission on the certificate in `master` database.

   The following example grants the appropriate permissions for the certificate. 
   
   ```tsql
   USE [master]
   GO
   GRANT VIEW DEFINITION ON CERTIFICATE::[<SERVER_CERT>] TO [<db_account>]
   GO
   ```

> [!NOTE]  
> Access to the TDE certificate is not required to back up or restore a TDE protected database.  
  
## <a name="Methods"></a> Backup Encryption Methods  
 The sections below provide a brief introduction to the steps to encrypting the data during backup. For a complete walkthrough of the different steps of encrypting your backup using Transact-SQL, see [Create an Encrypted Backup](../../relational-databases/backup-restore/create-an-encrypted-backup.md).  
  
### Using SQL Server Management Studio  
 You can encrypt a backup when creating the backup of a database in any of the following dialog boxes:  
  
1. [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md) On the **Backup Options** page, you can select **Encryption**, and specify the encryption algorithm and the certificate or asymmetric key to use for the encryption.  
  
1. [Using Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md#SSMSProcedure) When you select a backup task, on the **Options** tab of the **Define Backup ()Task** page, you can select **Backup Encryption**, and specify the encryption algorithm and the certificate or key to use for the encryption.  
  
### Using Transact-SQL  
 Following is a sample TSQL statement to encrypt the backup file:  
  
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
  
 For the full Transact-SQL statement syntax, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).  
  
### Using PowerShell  
 This example creates the encryption options and uses it as a parameter value in **Backup-SqlDatabase** cmdlet to create an encrypted backup.  
  
```powershell
$encryptionOption = New-SqlBackupEncryptionOption -Algorithm Aes256 -EncryptorType ServerCertificate -EncryptorName "BackupCert"  

Backup-SqlDatabase -ServerInstance . -Database "<myDatabase>" -BackupFile "<myDatabase>.bak" -CompressionOption On -EncryptionOption $encryptionOption  
```  
  
##  <a name="RecommendedPractices"></a> Recommended Practices  
 Create a backup of the encryption certificate and keys to a location other than your local machine where the instance is installed. To account for disaster recovery scenarios, consider storing a backup of the certificate or key to an off-site location. You cannot restore an encrypted backup without the certificate used to encrypt the backup.  
  
 To restore an encrypted backup, the original certificate used when the backup was taken with the matching thumbprint should be available on the instance you are restoring to. Therefore, the certificate should not be renewed on expiry or changed in any way. Renewal can result in updating the certificate triggering the change of the thumbprint, therefore making the certificate invalid for the backup file. The account performing the restore should have VIEW DEFINITION permissions on the certificate or the asymmetric key used to encrypt during backup.  
  
 Availability Group database backups are typically performed on the preferred backup replica.  If restoring a backup on a replica other than where the backup was taken from, ensure that the original certificate used for backup is available on the replica you are restoring to.  
  
 If the database is TDE enabled, choose different certificates or asymmetric keys for encrypting the database and the backup to increase security.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
|Topic/Task|Description|  
|-----------------|-----------------|  
|[Create an Encrypted Backup](../../relational-databases/backup-restore/create-an-encrypted-backup.md)|Describes the basic steps required to create an encrypted backup|  
|[Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)|Provides an example of creating an encrypted backup protected by keys in the Azure Key Vault.|  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)  
