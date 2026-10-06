---
title: "SQL Server & database encryption keys"
description: Learn about the service master key and database master key used by the SQL Server database engine to encrypt and secure data.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 09/21/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "keys [SQL Server], database encryption"
---
# SQL Server and database encryption keys (Database Engine)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses encryption keys to help secure data, credentials, and connection information that is stored in a server database. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two kinds of keys: *symmetric* and *asymmetric*. Symmetric keys use the same password to encrypt and decrypt data. Asymmetric keys use one password to encrypt data (called the *public* key) and another to decrypt data (called the *private* key).

In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], encryption keys include a combination of public, private, and symmetric keys that are used to protect sensitive data. The symmetric key is created during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] initialization when you first start the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the key to encrypt sensitive data that is stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The operating system creates the public and private keys, and they protect the symmetric key. A public and private key pair is created for each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that stores sensitive data in a database.

## Applications for SQL Server and database keys

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has two primary applications for keys: a *service master key* (SMK) generated on and for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance, and a *database master key* (DMK) used for a database.

### Service master key

The service master key is the root of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption hierarchy. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] generates the SMK the first time the instance starts, and uses it to encrypt linked server passwords, credentials, and the database master key in each database.

The SMK is encrypted by using the local machine key or the Windows Data Protection API (DPAPI). DPAPI uses a key derived from the Windows credentials of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account. Because the key is protected both ways, the service master key can be opened by the service account under which it was created, or by a principal that has access to the machine credentials.

[!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] uses the AES encryption algorithm to protect the service master key and the database master key. AES replaced the 3DES algorithm used in versions earlier than [!INCLUDE [sssql11-md](../../../includes/sssql11-md.md)]. After you upgrade an instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] from one of those earlier versions, regenerate the SMK and DMK to upgrade the master keys to AES. For more information about regenerating the SMK, see [ALTER SERVICE MASTER KEY (Transact-SQL)](../../../t-sql/statements/alter-service-master-key-transact-sql.md) and [ALTER MASTER KEY (Transact-SQL)](../../../t-sql/statements/alter-master-key-transact-sql.md).

### Database master key

The database master key is a symmetric key that protects the private keys of certificates and asymmetric keys that are present in the database. It can also encrypt data, but it has length limitations that make it less practical for data than an asymmetric key. To enable automatic decryption of the database master key, a copy of the key is encrypted by using the SMK. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stores that copy in both the database where the key is used and in the `master` system database.

The copy of the DMK stored in the `master` system database is silently updated whenever the DMK changes. You can change this default by using the `DROP ENCRYPTION BY SERVICE MASTER KEY` option of [ALTER MASTER KEY (Transact-SQL)](../../../t-sql/statements/alter-master-key-transact-sql.md). A DMK that isn't encrypted by the service master key must be opened by using [OPEN MASTER KEY (Transact-SQL)](../../../t-sql/statements/open-master-key-transact-sql.md) and a password.

## Managing SQL Server and database keys

Managing encryption keys involves creating new database keys, backing up the server and database keys, and knowing when and how to restore, delete, or change the keys.

To manage symmetric keys, use the tools included in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to perform the following tasks:

- Back up a copy of the server and database keys so you can use them to recover a server installation or as part of a planned migration.
- Restore a previously saved key to a database. Restoring the key lets a new server instance access existing data that it didn't originally encrypt.
- Delete the encrypted data in a database in the unlikely event that you can no longer access encrypted data.
- Recreate keys and re-encrypt data in the unlikely event that the key is compromised. As a security best practice, recreate the keys periodically, such as every few months, to protect the server from attacks that try to decipher the keys.
- Add or remove a server instance from a server scale-out deployment where multiple servers share both a single database and the key that provides reversible encryption for that database.

## Important security information

To access objects secured by the service master key, you need either the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account that you used to create the key or the computer (machine) account tied to the system where you created the key. You can change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account *or* the computer account without losing access to the key. However, if you change both accounts, you lose access to the service master key. If you lose access to the service master key without one of these two elements, you can't decrypt data and objects that the original key encrypted.

You can't restore connections secured with the service master key unless you have that key.

To access objects and data secured with the database master key, you only need the password that helps secure the key.

> [!CAUTION]
> If you lose all access to the keys described in this article, you lose access to the objects, connections, and data secured by those keys. You can restore the service master key as described in [Restore the service master key](restore-the-service-master-key.md), or you can go back to the original encrypting system to recover the access. There's no back door to recover the access.

## Related content

- [Extensible Key Management (EKM)](extensible-key-management-ekm.md)
- [Extensible Key Management using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md)
- [Enable TDE on SQL Server using EKM](enable-tde-on-sql-server-using-ekm.md)
- [Create a database master key](create-a-database-master-key.md)
- [Back up a database master key](back-up-a-database-master-key.md)
- [Restore a database master key](restore-a-database-master-key.md)
- [Back up the service master key](back-up-the-service-master-key.md)
- [Restore the service master key](restore-the-service-master-key.md)
- [Create identical symmetric keys on two servers](create-identical-symmetric-keys-on-two-servers.md)
- [Encrypt a column of data](encrypt-a-column-of-data.md)
- [Transparent data encryption (TDE)](transparent-data-encryption.md)
- [CREATE MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-master-key-transact-sql.md)
- [ALTER SERVICE MASTER KEY (Transact-SQL)](../../../t-sql/statements/alter-service-master-key-transact-sql.md)
- [Back up and restore SQL Server Reporting Services (SSRS) encryption keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)
- [Delete and Recreate Encryption Keys (Configuration Manager)](../../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)
- [Add and remove encryption keys for scale-out deployment](../../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md)
