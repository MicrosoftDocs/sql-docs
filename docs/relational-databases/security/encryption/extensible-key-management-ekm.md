---
title: "Extensible Key Management (EKM)"
description: Learn how to configure and use Extensible Key Management and how it fits into the data encryption capabilities for SQL Server.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 09/21/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "Key Management"
  - "Extensible Key Management"
  - "EKM, described"
---
# Extensible Key Management (EKM)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides data encryption capabilities together with *Extensible Key Management* (EKM), using the *Microsoft Cryptographic API* (MSCAPI) provider for encryption and key generation. Encryption keys for data and key encryption are created in transient key containers, and you must export them from a provider before you store them in the database. This approach lets [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] handle key management, including an encryption key hierarchy and key backup.

With the growing demand for regulatory compliance and concern for data privacy, organizations are taking advantage of encryption as a way to provide a defense-in-depth solution. This approach is often impractical using only database encryption management tools. Hardware vendors provide products that address enterprise key management by using *hardware security modules* (HSMs). HSM devices store encryption keys on hardware or software modules. This approach is more secure because the encryption keys don't reside with the encrypted data.

Several vendors offer HSMs for both key management and encryption acceleration. HSM devices use hardware interfaces with a server process as an intermediary between an application and an HSM. Vendors also implement MSCAPI providers over their modules, which might be hardware or software. MSCAPI often offers only a subset of the functionality that an HSM offers. Vendors can also provide management software for HSMs, key configuration, and key access.

HSM implementations vary from vendor to vendor, and using them with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires a common interface. Although MSCAPI provides this interface, it supports only a subset of the HSM features. It also has other limitations, such as the inability to natively persist symmetric keys, and a lack of session-oriented support.

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Extensible Key Management lets third-party EKM and HSM vendors register their modules in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. After a module is registered, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] users can use the encryption keys stored on it. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can then access the advanced encryption features these modules support, such as bulk encryption and decryption, and key management functions such as key aging and key rotation.

When you run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in an Azure VM, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can use keys stored in [Azure Key Vault](/azure/key-vault/general/basic-concepts). For more information, see [Extensible Key Management using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md).

## EKM configuration

Extensible Key Management isn't available in every edition of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. [!INCLUDE [editions-latest](../../../includes/editions-latest.md)]

By default, Extensible Key Management is off. To enable this feature, use the [sp_configure (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) stored procedure with the following option and value:

```sql
sp_configure 'show advanced', 1;
GO
RECONFIGURE;
GO
sp_configure 'EKM provider enabled', 1;
GO
RECONFIGURE;
GO
```

> [!NOTE]
> If you use `sp_configure` for this option on editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that don't support EKM, you receive an error.

To disable the feature, set the value to `0`. For more information about how to set server options, see [sp_configure (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

## How to use EKM

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Extensible Key Management lets you store the encryption keys that protect the database files in an off-box device such as a smart card, USB device, or EKM/HSM module. It also protects data from database administrators, except for members of the **sysadmin** fixed server role. You can encrypt data by using encryption keys that only the database user can access on the external EKM/HSM module.

Extensible Key Management also provides the following benefits:

- An extra authorization check, which enables separation of duties.
- Higher performance for hardware-based encryption and decryption.
- External encryption key generation.
- External encryption key storage, which physically separates data and keys.
- Encryption key retrieval.
- External encryption key retention, which enables encryption key rotation.
- Easier encryption key recovery.
- Manageable encryption key distribution.
- Secure encryption key disposal.

You can use Extensible Key Management for a username and password combination or other methods defined by the EKM driver.

> [!CAUTION]
> For troubleshooting, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] technical support might require the encryption key from the EKM provider. You might also need to access vendor tools or processes to help resolve an issue.

### Authentication with an EKM device

An EKM module can support more than one type of authentication. Each provider exposes only one type of authentication to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. That is, if the module supports both basic and other authentication types, it exposes one or the other, but not both.

#### EKM device-specific basic authentication by using a username and password

For EKM modules that support basic authentication by using a *username and password* pair, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides transparent authentication by using credentials. For more information about credentials, see [Credentials (Database Engine)](../authentication-access/credentials-database-engine.md).

You can create a credential for an EKM provider and map it to a login, either a Windows or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account, to access an EKM module on a per-login basis. The *identity* field of the credential contains the username, and the *secret* field contains a password to connect to an EKM module.

If no login-mapped credential exists for the EKM provider, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the credential mapped to the service account.

A login can have multiple credentials mapped to it, as long as each one is used for a distinct EKM provider. There must be only one mapped credential per EKM provider per login. You can map the same credential to other logins.

#### Other types of EKM device-specific authentication

For EKM modules that use authentication other than Windows or *username and password* combinations, you must perform authentication independently from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

### Encryption and decryption by an EKM device

Use the following functions and features to encrypt and decrypt data by using symmetric and asymmetric keys:

| Function or feature | Reference |
| --- | --- |
| Symmetric key encryption | [CREATE SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-symmetric-key-transact-sql.md) |
| Asymmetric key encryption | [CREATE ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-asymmetric-key-transact-sql.md) |
| `EncryptByKey(key_guid, 'cleartext', ...)` | [ENCRYPTBYKEY (Transact-SQL)](../../../t-sql/functions/encryptbykey-transact-sql.md) |
| `DecryptByKey(ciphertext, ...)` | [DECRYPTBYKEY (Transact-SQL)](../../../t-sql/functions/decryptbykey-transact-sql.md) |
| `EncryptByAsmKey(key_guid, 'cleartext')` | [ENCRYPTBYASYMKEY (Transact-SQL)](../../../t-sql/functions/encryptbyasymkey-transact-sql.md) |
| `DecryptByAsmKey(ciphertext)` | [DECRYPTBYASYMKEY (Transact-SQL)](../../../t-sql/functions/decryptbyasymkey-transact-sql.md) |

#### Database key encryption by EKM keys

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can use EKM keys to encrypt other keys in a database. You can create and use both symmetric and asymmetric keys on an EKM device. You can encrypt native (non-EKM) symmetric keys with EKM asymmetric keys.

The following example creates a database symmetric key and encrypts it by using a key on an EKM module.

```sql
CREATE SYMMETRIC KEY Key1
WITH ALGORITHM = AES_256
ENCRYPTION BY EKM_AKey1;
GO

-- Open the database key.
OPEN SYMMETRIC KEY Key1
DECRYPTION BY EKM_AKey1;
```

For more information about database and server keys in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [SQL Server and database encryption keys (Database Engine)](sql-server-and-database-encryption-keys-database-engine.md).

> [!NOTE]
> You can't encrypt one EKM key with another EKM key.
>
> [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] doesn't support signing modules with asymmetric keys generated from an EKM provider.

## Related content

- [EKM provider enabled server configuration option](../../../database-engine/configure-windows/ekm-provider-enabled-server-configuration-option.md)
- [Enable TDE on SQL Server using EKM](enable-tde-on-sql-server-using-ekm.md)
- [Extensible Key Management using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md)
- [SQL Server and database encryption keys (Database Engine)](sql-server-and-database-encryption-keys-database-engine.md)
- [CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)](../../../t-sql/statements/create-cryptographic-provider-transact-sql.md)
- [DROP CRYPTOGRAPHIC PROVIDER (Transact-SQL)](../../../t-sql/statements/drop-cryptographic-provider-transact-sql.md)
- [ALTER CRYPTOGRAPHIC PROVIDER (Transact-SQL)](../../../t-sql/statements/alter-cryptographic-provider-transact-sql.md)
- [sys.cryptographic_providers (Transact-SQL)](../../system-catalog-views/sys-cryptographic-providers-transact-sql.md)
- [sys.dm_cryptographic_provider_sessions (Transact-SQL)](../../system-dynamic-management-objects/sys-dm-cryptographic-provider-sessions-transact-sql.md)
- [sys.dm_cryptographic_provider_properties (Transact-SQL)](../../system-dynamic-management-objects/sys-dm-cryptographic-provider-properties-transact-sql.md)
- [sys.dm_cryptographic_provider_algorithms (Transact-SQL)](../../system-dynamic-management-objects/sys-dm-cryptographic-provider-algorithms-transact-sql.md)
- [sys.dm_cryptographic_provider_keys (Transact-SQL)](../../system-dynamic-management-objects/sys-dm-cryptographic-provider-keys-transact-sql.md)
- [sys.credentials (Transact-SQL)](../../system-catalog-views/sys-credentials-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../../t-sql/statements/create-credential-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../../t-sql/statements/alter-login-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)
- [ALTER ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/alter-asymmetric-key-transact-sql.md)
- [DROP ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/drop-asymmetric-key-transact-sql.md)
- [CREATE SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-symmetric-key-transact-sql.md)
- [ALTER SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/alter-symmetric-key-transact-sql.md)
- [DROP SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/drop-symmetric-key-transact-sql.md)
- [OPEN SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/open-symmetric-key-transact-sql.md)
- [Back up and restore SQL Server Reporting Services (SSRS) encryption keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)
- [Delete and Recreate Encryption Keys (Configuration Manager)](../../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)
- [Add and remove encryption keys for scale-out deployment](../../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md)
- [Back up a service master key](back-up-the-service-master-key.md)
- [Restore a service master key](restore-the-service-master-key.md)
- [Create a database master key](create-a-database-master-key.md)
- [Back up a database master key](back-up-a-database-master-key.md)
- [Restore a database master key](restore-a-database-master-key.md)
- [Create identical symmetric keys on two servers](create-identical-symmetric-keys-on-two-servers.md)
