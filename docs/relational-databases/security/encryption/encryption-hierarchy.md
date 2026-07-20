---
title: "Encryption Hierarchy"
description: Learn about the hierarchical encryption and key management infrastructure in SQL Server. Store keys in an Extensible Key Management module.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: 04/30/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - sfi-image-nochange
  - ignite-2025
helpviewer_keywords:
  - "encryption [SQL Server], hierarchies"
  - "cryptography [SQL Server], hierarchies"
  - "encryption keys [SQL Server]"
  - "security [SQL Server], encryption"
  - "hierarchies [SQL Server], encryption"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Encryption hierarchy

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encrypts data with a hierarchical encryption and key management infrastructure. Each layer encrypts the layer below it by using a combination of certificates, asymmetric keys, and symmetric keys. Asymmetric keys and symmetric keys can be stored outside of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] in an Extensible Key Management (EKM) module.

The following illustration shows that each layer of the encryption hierarchy encrypts the layer beneath it, and displays the most common encryption configurations. The access to the start of the hierarchy is usually protected by a password.

:::image type="content" source="media/encryption-hierarchy/encryption-hierarchy-stack.gif" alt-text="Diagram of encryption combinations in a stack.":::

Keep in mind the following concepts:

- For best performance, encrypt data using symmetric keys instead of certificates or asymmetric keys.

- Database Master Keys (DMK) are protected by the Service Master Key (SMK). The Service Master Key is created by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setup and is encrypted with the Windows Data Protection API (DPAPI).

- Other encryption hierarchies stacking additional layers are possible.

- An Extensible Key Management (EKM) module holds symmetric or asymmetric keys outside of SQL Server.

- Transparent Data Encryption (TDE) must use a symmetric key called the database encryption key which is protected by either a certificate protected by the DMK of the `master` database, or by an asymmetric key stored in an EKM.

- The Service Master Key and all Database Master Keys are symmetric keys.

The following illustration shows the same information in an alternative manner.

:::image type="content" source="media/encryption-hierarchy/encryption-hierarchy-wheel.gif" alt-text="Diagram of encryption combinations in a wheel.":::

This diagram illustrates the following additional concepts:

- In this illustration, arrows indicate common encryption hierarchies.

- Symmetric and asymmetric keys in the EKM can protect access to the symmetric and asymmetric keys stored in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. The dotted line associated with EKM indicates that keys in the EKM could replace the symmetric and asymmetric keys stored in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

[!INCLUDE [rsa-padding-encryption-algorithm-history-md](../../../includes/rsa-padding-encryption-algorithm-history.md)]

## Encryption mechanisms

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] provides the following mechanisms for encryption:

- [!INCLUDE [tsql](../../../includes/tsql-md.md)] functions

- Asymmetric keys

- Symmetric keys

- Certificates

- Transparent Data Encryption

### Transact-SQL functions

Individual items can be encrypted as they're inserted or updated using [!INCLUDE [tsql](../../../includes/tsql-md.md)] functions. For more information, see [ENCRYPTBYPASSPHRASE](../../../t-sql/functions/encryptbypassphrase-transact-sql.md) and [DECRYPTBYPASSPHRASE](../../../t-sql/functions/decryptbypassphrase-transact-sql.md).

### Certificates

A public key certificate, usually just called a certificate, is a digitally signed statement that binds the value of a public key to the identity of the person, device, or service that holds the corresponding private key. Certificates are issued and signed by a certification authority (CA). The entity that receives a certificate from a CA is the subject of that certificate. Typically, certificates contain the following information.

- The public key of the subject.

- The identifier information of the subject, such as the name and e-mail address.

- The validity period. This is the length of time that the certificate is considered valid.

     A certificate is valid only for the period of time specified within it; every certificate contains **Valid From** and **Valid To** dates. These dates set the boundaries of the validity period. When the validity period for a certificate has passed, a new certificate must be requested by the subject of the now-expired certificate.

- Issuer identifier information.

- The digital signature of the issuer.

     This signature attests to the validity of the binding between the public key and the identifier information of the subject. (The process of digitally signing information entails transforming the information, as well as some secret information held by the sender, into a tag called a signature.)

A primary benefit of certificates is that they relieve hosts of the need to maintain a set of passwords for individual subjects. Instead, the host merely establishes trust in a certificate issuer, which might then sign an unlimited number of certificates.

When a host, such as a secure Web server, designates an issuer as a trusted root authority, the host implicitly trusts the policies that the issuer has used to establish the bindings of certificates it issues. In effect, the host trusts that the issuer has verified the identity of the certificate subject. A host designates an issuer as a trusted root authority by putting the self-signed certificate of the issuer, which contains the public key of the issuer, into the trusted root certification authority certificate store of the host computer. Intermediate or subordinate certification authorities are trusted only if they have a valid certification path from a trusted root certification authority.

The issuer can revoke a certificate before it expires. Revocation cancels the binding of a public key to an identity that is asserted in the certificate. Each issuer maintains a certificate revocation list that can be used by programs when they're checking the validity of any given certificate.

The self-signed certificates created by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] follow the X.509 standard and support the X.509 v1 fields.

### Asymmetric keys

An asymmetric key is made up of a private key and the corresponding public key. Each key can decrypt data encrypted by the other. Asymmetric encryption and decryption are relatively resource-intensive, but they provide a higher level of security than symmetric encryption. An asymmetric key can be used to encrypt a symmetric key for storage in a database.

### Symmetric keys

A symmetric key is one key that is used for both encryption and decryption. Encryption and decryption by using a symmetric key is fast, and suitable for routine use with sensitive data in the database. To protect the key material of the symmetric key, SQL Server stores the key material in encrypted form that uses asymmetric RSA encryption. Historically, this encryption utilized PKCS#1 v1.5 padding mode; starting with database compatibility level 170, the encryption uses OAEP-256 padding mode.

### Transparent Data Encryption

Transparent Data Encryption (TDE) is a special case of encryption using a symmetric key. TDE encrypts an entire database using that symmetric key called the database encryption key. The database encryption key is protected by other keys or certificates which are protected either by the DMK or by an asymmetric key stored in an EKM module. For more information, see [Transparent data encryption (TDE)](transparent-data-encryption.md).

## Fabric SQL database

In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], Always Encrypted, EKM, and TDE aren't currently supported. In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], Microsoft Entra ID for database users is the only supported authentication method. For more information, see [Authorization in SQL database in Microsoft Fabric](/fabric/database/sql/authorization) and [Features comparison](/fabric/database/sql/feature-comparison-sql-database-fabric).

## Related content

- [Securing SQL Server](../securing-sql-server.md)
- [Security Functions (Transact-SQL)](../../../t-sql/functions/security-functions-transact-sql.md)
- [Permissions Hierarchy (Database Engine)](../permissions-hierarchy-database-engine.md)
- [Securables](../securables.md)
