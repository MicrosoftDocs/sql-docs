---
title: "Extensible Key Management (EKM) | Microsoft Docs"
description: Learn how to configure and use Extensible Key Management and how it fits into the data encryption capabilities for SQL Server.
ms.custom: ""
ms.date: "07/25/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Key Management"
  - "Extensible Key Management"
  - "EKM, described"
ms.assetid: 9bfaf500-2d1e-4c02-b041-b8761a9e695b
author: jaszymas
ms.author: jaszymas
---
# Extensible Key Management (EKM)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides data encryption capabilities together with *Extensible Key Management* (EKM), using the *Microsoft Cryptographic API* (MSCAPI) provider for encryption and key generation. Encryption keys for data and key encryption are created in transient key containers, and they must be exported from a provider before they are stored in the database. This approach enables key management that includes an encryption key hierarchy and key backup, to be handled by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 With the growing demand for regulatory compliance and concern for data privacy, organizations are taking advantage of encryption as a way to provide a "defense in depth" solution. This approach is often impractical using only database encryption management tools. Hardware vendors provide products that address enterprise key management by using *Hardware Security Modules* (HSM). HSM devices store encryption keys on hardware or software modules. This is a more secure solution because the encryption keys do not reside with encryption data.  
  
 A number of vendors offer HSM for both key management and encryption acceleration. HSM devices use hardware interfaces with a server process as an intermediary between an application and an HSM. Vendors also implement MSCAPI providers over their modules, which might be hardware or software. MSCAPI often offers only a subset of the functionality that is offered by an HSM. Vendors can also provide management software for HSM, key configuration, and key access.  
  
 HSM implementations vary from vendor to vendor, and to use them with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires a common interface. Although the MSCAPI provides this interface, it supports only a subset of the HSM features. It also has other limitations, such as the inability to natively persist symmetric keys, and a lack of session-oriented support.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Extensible Key Management enables third-party EKM/HSM vendors to register their modules in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When registered, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] users can use the encryption keys stored on EKM modules. This enables [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to access the advanced encryption features these modules support such as bulk encryption and decryption, and key management functions such as key aging and key rotation.  
  
 When running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in an Azure VM, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can use keys stored in the [Azure Key Vault](/azure/key-vault/general/basic-concepts). For more information, see [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).  
  
## EKM Configuration  
 Extensible Key Management is not available in every edition of [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 By default, Extensible Key Management is off. To enable this feature, use the sp_configure command that has the following option and value, as in the following example:  
  
```  
sp_configure 'show advanced', 1  
GO  
RECONFIGURE  
GO  
sp_configure 'EKM provider enabled', 1  
GO  
RECONFIGURE  
GO  
```  
  
> [!NOTE]  
>  If you use the sp_configure command for this option on editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that do not support EKM, you will receive an error.  
  
 To disable the feature, set the value to **0**. For more information about how to set server options, see [sp_configure &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).  
  
## How to Use EKM  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Extensible Key Management enables the encryption keys that protect the database files to be stored in an off-box device such as a smartcard, USB device, or EKM/HSM module. This also enables data protection from database administrators (except members of the sysadmin group). Data can be encrypted by using encryption keys that only the database user has access to on the external EKM/HSM module.  
  
 Extensible Key Management also provides the following benefits:  
  
-   Additional authorization check (enabling separation of duties).  
  
-   Higher performance for hardware-based encryption/decryption.  
  
-   External encryption key generation.  
  
-   External encryption key storage (physical separation of data and keys).  
  
-   Encryption key retrieval.  
  
-   External encryption key retention (enables encryption key rotation).  
  
-   Easier encryption key recovery.  
  
-   Manageable encryption key distribution.  
  
-   Secure encryption key disposal.  
  
 You can use Extensible Key Management for a username and password combination or other methods defined by the EKM driver.  
  
> [!CAUTION]  
>  For troubleshooting, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] technical support might require the encryption key from the EKM provider. You might also need to access vendor tools or processes to help resolve an issue.  
  
### Authentication with an EKM Device  
 An EKM module can support more than one type of authentication. Each provider exposes only one type of authentication to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], that is if the module supports Basic or Other authentication types, it exposes one or the other, but not both.  
  
#### EKM Device-Specific Basic Authentication Using username/password  
 For those EKM modules that support Basic authentication using a *username/password* pair, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides transparent authentication using credentials. For more information about credentials, see [Credentials &#40;Database Engine&#41;](../../../relational-databases/security/authentication-access/credentials-database-engine.md).  
  
 A credential can be created for an EKM provider and mapped to a login (both Windows and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] accounts) to access an EKM module on per-login basis. The *identity* field of the credential contains the username; the *secret* field contains a password to connect to an EKM module.  
  
 If there is no login mapped credential for the EKM provider, the credential mapped to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account is used.  
  
 A login can have multiple credentials mapped to it, as long as they are used for distinctive EKM providers. There must be only one mapped credential per EKM provider per login. The same credential can be mapped to other logins.  
  
#### Other Types of EKM Device-Specific Authentication  
 For EKM modules that have authentication other than Windows or *user/password* combinations, authentication must be performed independently from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
### Encryption and Decryption by an EKM Device  
 You can use the following functions and features to encrypt and decrypt data by using symmetric and asymmetric keys:  
  
|Function or feature|Reference|  
|-------------------------|---------------|  
|Symmetric key encryption|[CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)|  
|Asymmetric Key encryption|[CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)|  
|EncryptByKey(key_guid, 'cleartext', ...)|[ENCRYPTBYKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/encryptbykey-transact-sql.md)|  
|DecryptByKey(ciphertext, ...)|[DECRYPTBYKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/decryptbykey-transact-sql.md)|  
|EncryptByAsmKey(key_guid, 'cleartext')|[ENCRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/encryptbyasymkey-transact-sql.md)|  
|DecryptByAsmKey(ciphertext)|[DECRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/decryptbyasymkey-transact-sql.md)|  
  
#### Database Keys Encryption by EKM Keys  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can use EKM keys to encrypt other keys in a database. You can create and use both symmetric and asymmetric keys on an EKM device. You can encrypt native (non-EKM) symmetric keys with EKM asymmetric keys.  
  
 The following example creates a database symmetric key and encrypts it using a key on an EKM module.  
  
```  
CREATE SYMMETRIC KEY Key1  
WITH ALGORITHM = AES_256  
ENCRYPTION BY EKM_AKey1;  
GO  
--Open database key  
OPEN SYMMETRIC KEY Key1  
DECRYPTION BY EKM_AKey1  
```  
  
 For more information about Database and Server Keys in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md).  
  
> [!NOTE]  
>  You cannot encrypt one EKM key with another EKM key.  
>   
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not support signing modules with asymmetric keys generated from EKM provider.  
  
## Related Tasks  
 [EKM provider enabled Server Configuration Option](../../../database-engine/configure-windows/ekm-provider-enabled-server-configuration-option.md)  
  
 [Enable TDE on SQL Server Using EKM](../../../relational-databases/security/encryption/enable-tde-on-sql-server-using-ekm.md)  
  
 [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
  
## See Also  
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../../t-sql/statements/create-cryptographic-provider-transact-sql.md)   
 [DROP CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-cryptographic-provider-transact-sql.md)   
 [ALTER CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-cryptographic-provider-transact-sql.md)   
 [sys.cryptographic_providers &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-cryptographic-providers-transact-sql.md)   
 [sys.dm_cryptographic_provider_sessions &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-cryptographic-provider-sessions-transact-sql.md)   
 [sys.dm_cryptographic_provider_properties &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-cryptographic-provider-properties-transact-sql.md)   
 [sys.dm_cryptographic_provider_algorithms &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-cryptographic-provider-algorithms-transact-sql.md)   
 [sys.dm_cryptographic_provider_keys &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-cryptographic-provider-keys-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-credential-transact-sql.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-login-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-symmetric-key-transact-sql.md)   
 [DROP SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-symmetric-key-transact-sql.md)   
 [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/open-symmetric-key-transact-sql.md)   
 [Back Up and Restore Reporting Services Encryption Keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)   
 [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)   
 [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/add-and-remove-encryption-keys-for-scale-out-deployment.md)   
 [Back Up the Service Master Key](../../../relational-databases/security/encryption/back-up-the-service-master-key.md)   
 [Restore the Service Master Key](../../../relational-databases/security/encryption/restore-the-service-master-key.md)   
 [Create a Database Master Key](../../../relational-databases/security/encryption/create-a-database-master-key.md)   
 [Back Up a Database Master Key](../../../relational-databases/security/encryption/back-up-a-database-master-key.md)   
 [Restore a Database Master Key](../../../relational-databases/security/encryption/restore-a-database-master-key.md)   
 [Create Identical Symmetric Keys on Two Servers](../../../relational-databases/security/encryption/create-identical-symmetric-keys-on-two-servers.md)  
  
