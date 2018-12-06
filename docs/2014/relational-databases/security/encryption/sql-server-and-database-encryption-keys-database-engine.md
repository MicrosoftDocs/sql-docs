---
title: "SQL Server and Database Encryption Keys (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "keys [SQL Server], database encryption"
ms.assetid: 15c0a5e8-9177-484c-ae75-8c552dc0dac0
author: aliceku
ms.author: aliceku
manager: craigg
---
# SQL Server and Database Encryption Keys (Database Engine)
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses encryption keys to help secure data, credentials, and connection information that is stored in a server database. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two kinds of keys: *symmetric* and *asymmetric*. Symmetric keys use the same password to encrypt and decrypt data. Asymmetric keys use one password to encrypt data (called the *public* key) and another to decrypt data (called the *private* key).  
  
 In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], encryption keys include a combination of public, private, and symmetric keys that are used to protect sensitive data. The symmetric key is created during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] initialization when you first start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The key is used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to encrypt sensitive data that is stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Public and private keys are created by the operating system and they are used to protect the symmetric key. A public and private key pair is created for each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that stores sensitive data in a database.  
  
## Applications for SQL Server and Database Keys  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two primary applications for keys: a *service master key* (SMK) generated on and for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance, and a *database master key* (DMK) used for a database.  
  
 The SMK is automatically generated the first time the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is started and is used to encrypt a linked server password, credentials, and the database master key. The SMK is encrypted by using the local computer key using the Windows Data Protection API (DPAPI). The DPAPI uses a key that is derived from the Windows credentials of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account and the computer's credentials. The service master key can only be decrypted by the service account under which it was created or by a principal that has access to the machine's credentials.  
  
 The database master key is a symmetric key that is used to protect the private keys of certificates and asymmetric keys that are present in the database. It can also be used to encrypt data, but it has length limitations that make it less practical for data than using a symmetric key.  
  
 When it is created, the master key is encrypted by using the Triple DES algorithm and a user-supplied password. To enable the automatic decryption of the master key, a copy of the key is encrypted by using the SMK. It is stored in both the database where it is used and in the `master` system database.  
  
 The copy of the DMK stored in the `master` system database is silently updated whenever the DMK is changed. However, this default can be changed by using the `DROP ENCRYPTION BY SERVICE MASTER KEY` option of the `ALTER MASTER KEY` statement. A DMK that is not encrypted by the service master key must be opened by using the `OPEN MASTER KEY` statement and a password.  
  
## Managing SQL Server and Database Keys  
 Managing encryption keys consists of creating new database keys, creating a backup of the server and database keys, and knowing when and how to restore, delete, or change the keys.  
  
 To manage symmetric keys, you can use the tools included in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to do the following:  
  
-   Back up a copy of the server and database keys so that you can use them to recover a server installation, or as part of a planned migration.  
  
-   Restore a previously saved key to a database. This enables a new server instance to access existing data that it did not originally encrypt.  
  
-   Delete the encrypted data in a database in the unlikely event that you can no longer access encrypted data.  
  
-   Re-create keys and re-encrypt data in the unlikely event that the key is compromised. As a security best practice, you should re-create the keys periodically (for example, every few months) to protect the server from attacks that try to decipher the keys.  
  
-   Add or remove a server instance from a server scale-out deployment where multiple servers share both a single database and the key that provides reversible encryption for that database.  
  
## Important Security Information  
 Accessing objects secured by the service master key requires either the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service account that was used to create the key or the computer (machine) account. That is, the computer is tied to the system where the key was created. You can change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service account *or* the computer account without losing access to the key. However, if you change both, you will lose access to the service master key. If you lose access to the service master key without one of these two elements, you be unable to decrypt data and objects encrypted by using the original key.  
  
 Connections secured with the service master key cannot be restored without the service master key.  
  
 Access to objects and data secured with the database master key require only the password that is used to help secure the key.  
  
> [!CAUTION]  
>  If you lose all access to the keys described earlier, you will lose access to the objects, connections, and data secured by those keys. You can restore the service master key, as described in the links that are shown here, or you can go back to the original encrypting system to recover the access. There is no "back-door" to recover the access.  
  
## In This Section  
 [Service Master Key](service-master-key.md)  
 Provides a brief explanation for the service master key and its best practices.  
  
 [Extensible Key Management &#40;EKM&#41;](extensible-key-management-ekm.md)  
 Explains how to use third-party key management systems with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Related Tasks  
 [Back Up the Service Master Key](back-up-the-service-master-key.md)  
  
 [Restore the Service Master Key](restore-the-service-master-key.md)  
  
 [Create a Database Master Key](create-a-database-master-key.md)  
  
 [Back Up a Database Master Key](back-up-a-database-master-key.md)  
  
 [Restore a Database Master Key](restore-a-database-master-key.md)  
  
 [Create Identical Symmetric Keys on Two Servers](create-identical-symmetric-keys-on-two-servers.md)  
  
 [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](extensible-key-management-using-azure-key-vault-sql-server.md)  
  
 [Enable TDE Using EKM](enable-tde-on-sql-server-using-ekm.md)  
  
## Related Content  
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-master-key-transact-sql)  
  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-service-master-key-transact-sql)  
  
 [Restore a Database Master Key](restore-a-database-master-key.md)  
  
## See Also  
 [Back Up and Restore Reporting Services Encryption Keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)   
 [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)   
 [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md)   
 [Transparent Data Encryption &#40;TDE&#41;](transparent-data-encryption.md)  
  
  
