---
title: "SQL Server & database encryption keys"
description: Learn about the service master key and database master key used by the SQL Server database engine to encrypt and secure data. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "keys [SQL Server], database encryption"
ms.assetid: 15c0a5e8-9177-484c-ae75-8c552dc0dac0
author: jaszymas
ms.author: jaszymas
---
# SQL Server and Database Encryption Keys (Database Engine)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses encryption keys to help secure data, credentials, and connection information that is stored in a server database. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two kinds of keys: *symmetric* and *asymmetric*. Symmetric keys use the same password to encrypt and decrypt data. Asymmetric keys use one password to encrypt data (called the *public* key) and another to decrypt data (called the *private* key).  
  
 In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], encryption keys include a combination of public, private, and symmetric keys that are used to protect sensitive data. The symmetric key is created during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] initialization when you first start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The key is used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to encrypt sensitive data that is stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Public and private keys are created by the operating system and they are used to protect the symmetric key. A public and private key pair is created for each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that stores sensitive data in a database.  
  
## Applications for SQL Server and Database Keys  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two primary applications for keys: a *service master key* (SMK) generated on and for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance, and a *database master key* (DMK) used for a database.

### Service master key
  
 The Service Master Key is the root of the SQL Server encryption hierarchy. The SMK is automatically generated the first time the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is started and is used to encrypt a linked server password, credentials, and the database master key in each database. The SMK is encrypted by using the local machine key using the Windows Data Protection API (DPAPI). The DPAPI uses a key that is derived from the Windows credentials of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account and the computer's credentials. The service master key can only be decrypted by the service account under which it was created or by a principal that has access to the machine's credentials.

The Service Master Key can only be opened by the Windows service account under which it was created or by a principal with access to both the service account name and its password.

 [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. After upgrading an instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] the SMK and DMK should be regenerated in order to upgrade the master keys to AES. For more information about regenerating the SMK, see [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-service-master-key-transact-sql.md) and [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-master-key-transact-sql.md).

### Database master key
  
 The database master key is a symmetric key that is used to protect the private keys of certificates and asymmetric keys that are present in the database. It can also be used to encrypt data, but it has length limitations that make it less practical for data than using an asymmetric key. To enable the automatic decryption of the database master key, a copy of the key is encrypted by using the SMK. It is stored in both the database where it is used and in the **master** system database.  
  
 The copy of the DMK stored in the **master** system database is silently updated whenever the DMK is changed. However, this default can be changed by using the **DROP ENCRYPTION BY SERVICE MASTER KEY** option of the **ALTER MASTER KEY** statement. A DMK that is not encrypted by the service master key must be opened by using the **OPEN MASTER KEY** statement and a password.  
  
## Managing SQL Server and Database Keys  
 Managing encryption keys consists of creating new database keys, creating a backup of the server and database keys, and knowing when and how to restore, delete, or change the keys.  
  
 To manage symmetric keys, you can use the tools included in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to do the following:  
  
-   Back up a copy of the server and database keys so that you can use them to recover a server installation, or as part of a planned migration.  
  
-   Restore a previously saved key to a database. This enables a new server instance to access existing data that it did not originally encrypt.  
  
-   Delete the encrypted data in a database in the unlikely event that you can no longer access encrypted data.  
  
-   Re-create keys and re-encrypt data in the unlikely event that the key is compromised. As a security best practice, you should re-create the keys periodically (for example, every few months) to protect the server from attacks that try to decipher the keys.  
  
-   Add or remove a server instance from a server scale-out deployment where multiple servers share both a single database and the key that provides reversible encryption for that database.  
  
## Important Security Information  
 Accessing objects secured by the service master key requires either the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service account that was used to create the key or the computer (machine) account. That is, the computer account that is tied to the system where the key was created. You can change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service account *or* the computer account without losing access to the key. However, if you change both, you will lose access to the service master key. If you lose access to the service master key without one of these two elements, you be unable to decrypt data and objects encrypted by using the original key.  
  
 Connections secured with the service master key cannot be restored without the service master key.  
  
 Access to objects and data secured with the database master key require only the password that is used to help secure the key.  
  
> [!CAUTION]  
>  If you lose all access to the keys described earlier, you will lose access to the objects, connections, and data secured by those keys. You can restore the service master key, as described in the links that are shown here, or you can go back to the original encrypting system to recover the access. There is no "back-door" to recover the access.  
  
## In This Section  
 [Service Master Key]()  
 Provides a brief explanation for the service master key and its best practices.  
  
 [Extensible Key Management &#40;EKM&#41;](../../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
 Explains how to use third-party key management systems with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Related Tasks  
 [Back Up the Service Master Key](../../../relational-databases/security/encryption/back-up-the-service-master-key.md)  
  
 [Restore the Service Master Key](../../../relational-databases/security/encryption/restore-the-service-master-key.md)  
  
 [Create a Database Master Key](../../../relational-databases/security/encryption/create-a-database-master-key.md)  
  
 [Back Up a Database Master Key](../../../relational-databases/security/encryption/back-up-a-database-master-key.md)  
  
 [Restore a Database Master Key](../../../relational-databases/security/encryption/restore-a-database-master-key.md)  
  
 [Create Identical Symmetric Keys on Two Servers](../../../relational-databases/security/encryption/create-identical-symmetric-keys-on-two-servers.md)  
  
 [Enable TDE on SQL Server Using EKM](../../../relational-databases/security/encryption/enable-tde-on-sql-server-using-ekm.md)  
  
 [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
  
 [Encrypt a Column of Data](../../../relational-databases/security/encryption/encrypt-a-column-of-data.md)  
  
## Related Content  
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-master-key-transact-sql.md)  
  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-service-master-key-transact-sql.md)  
  
 [Restore a Database Master Key](../../../relational-databases/security/encryption/restore-a-database-master-key.md)  
  
## See Also  
 [Back Up and Restore Reporting Services Encryption Keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)   
 [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)   
 [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md)   
 [Transparent Data Encryption &#40;TDE&#41;](../../../relational-databases/security/encryption/transparent-data-encryption.md)  
  
