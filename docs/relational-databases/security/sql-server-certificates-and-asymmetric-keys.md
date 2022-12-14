---
title: "SQL Server Certificates and Asymmetric Keys | Microsoft Docs"
description: Learn about certificates and asymmetric keys in SQL Server, including externally generated or SQL Server generated certificates, tools, and related tasks.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "security [SQL Server], certificates and asymmetric keys"
ms.assetid: 8519aa2f-f09c-4c1c-96b5-abc24811e60c
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Certificates and Asymmetric Keys
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

 Public Key Cryptography is a form of message secrecy in which a user creates a *public* key and a *private* key. The private key is kept secret, whereas the public key can be distributed to others. Although the keys are mathematically related, the private key cannot be easily derived by using the public key. The public key can be used to encrypt data which only the corresponding private key will be able to decrypt. This can be used for encrypting messages to the owner of the private key. Similarly the owner of a private key can encrypt data which can only be decrypted with the public key. This use forms the basis of digital certificates in which information contained in the certificate is encrypted by the owner of a private key, assuring the author of the contents. Since the encrypting and decrypting keys are different they are known as *asymmetric* keys.
  
 Certificates and asymmetric keys are both ways to use asymmetric encryption. Certificates are often used as containers for asymmetric keys because they can contain more information such as expiry dates and issuers. There is no difference between the two mechanisms for the cryptographic algorithm, and no difference in strength given the same key length. Generally, you use a certificate to encrypt other types of encryption keys in a database, or to sign code modules.  
  
 Certificates and asymmetric keys can decrypt data that the other encrypts. Generally, you use asymmetric encryption to encrypt a symmetric key for storage in a database.  
  
 A public key does not have a particular format like a certificate would have, and you cannot export it to a file.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] contains features that enable you to create and manage certificates and keys for use with the server and database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be used to create and manage certificates and keys with other applications or in the operating system.  
  
## Certificates  
 A certificate is a digitally signed security object that contains a public (and optionally a private) key for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can use externally generated certificates or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can generate certificates.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] certificates comply with the IETF X.509v3 certificate standard.  
  
 Certificates are useful because of the option of both exporting and importing keys to X.509 certificate files. The syntax for creating certificates allows for creation options for certificates such as an expiry date.  
  
### Using a Certificate in SQL Server  
 Certificates can be used to help secure connections, in database mirroring, to sign packages and other objects, or to encrypt data or connections. The following table lists additional resources for certificates in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Topic|Description|  
|-----------|-----------------|  
|[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)|Explains the command for creating certificates.|  
|[Identify the Source of Packages with Digital Signatures](../../integration-services/security/identify-the-source-of-packages-with-digital-signatures.md)|Shows information about how to use certificates to sign software packages.|  
|[Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)|Covers information about how to use certificates with Database Mirroring.|  
  
## Asymmetric Keys  
 Asymmetric keys are used for securing symmetric keys. They can also be used for limited data encryption and to digitally sign database objects. An asymmetric key consists of a private key and a corresponding public key. For more information about asymmetric keys, see [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md).  
  
 Asymmetric keys can be imported from strong name key files, but they cannot be exported. They also do not have expiry options. Asymmetric keys cannot encrypt connections.  
  
### Using an Asymmetric Key in SQL Server  
 Asymmetric keys can be used to help secure data or sign plaintext. The following table lists additional resources for asymmetric keys in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Topic|Description|  
|-----------|-----------------|  
|[CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)|Explains the command for creating asymmetric keys.|  
|[SIGNBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/signbyasymkey-transact-sql.md)|Displays the options for signing objects.|  
  
## Tools  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] provides tools and utilities that will generate certificates and strong name key files. These tools offer a richer amount of flexibility in the key generation process than the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax. You can use these tools to create RSA keys with more complex key lengths and then import them into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The following table shows where to find these tools.  
  
| Tool | Purpose |
| ---- | ------- |
|[New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate)|Creates self-signed certificates.|  
|[makecert](/windows/desktop/SecCrypto/makecert)|Creates certificates. Deprecated in favor of **New-SelfSignedCertificate**.|  
|[sn](/dotnet/framework/tools/sn-exe-strong-name-tool)|Creates strong names for symmetric keys.|  
  
## Related Tasks  
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)  
  
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
  
## See Also  
 [sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)   
 [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md)  
  
  
