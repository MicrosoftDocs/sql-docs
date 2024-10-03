---
title: "SQL Server encryption"
description: Use these resources to understand how SQL Server uses encryption to enhance security for your databases.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 09/16/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "encryption [SQL Server], about encryption"
  - "security [SQL Server], encryption"
  - "cryptography [SQL Server], about cryptography"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# SQL Server encryption

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Encryption is the process of obfuscating data by the use of a key or password. This process can make the data useless without the corresponding decryption key or password. Encryption doesn't solve access control problems. However, it enhances security by limiting data loss even if access controls are bypassed. For example, if the database host computer is misconfigured and a hacker obtains sensitive data, that stolen information might be useless if it's encrypted.

Although encryption is a valuable tool to help ensure security, it shouldn't be considered for all data or connections. When you're deciding whether to implement encryption, consider how users access data. If users access data over a public network, data encryption might be required to increase security. However, if all access involves a secure intranet configuration, encryption might not be required. Any use of encryption should also include a maintenance strategy for passwords, keys, and certificates.

> [!NOTE]  
> The latest information about Transport Level Security (TLS 1.2) is available at [TLS 1.2 support for Microsoft SQL Server](/troubleshoot/sql/database-engine/connect/tls-1-2-support-microsoft-sql-server). For more information about TLS 1.3, see [TLS 1.3 support](../networking/tls-1-3.md).

## In this section

You can use encryption in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] for connections, data, and stored procedures. The following articles contain more information about encryption in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- [Encryption Hierarchy](encryption-hierarchy.md)

  Information about the encryption hierarchy in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- [Choose an encryption algorithm](choose-an-encryption-algorithm.md)

  Information about how to select an effective encrypting algorithm.

- [Transparent data encryption (TDE)](transparent-data-encryption.md)

  General information about how to encrypt data at rest.

- [SQL Server and Database Encryption Keys (Database Engine)](sql-server-and-database-encryption-keys-database-engine.md)

  In [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], encryption keys include a combination of public, private, and symmetric keys that are used to protect sensitive data. This section explains how to implement and manage encryption keys.

- [Always Encrypted](always-encrypted-database-engine.md)

  Ensure on-premises database administrators, cloud database operators, or other high-privileged, but unauthorized users, can't access the encrypted data. Expand Always Encrypted with [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) to enable in-place encryption and richer confidential queries.

- [Dynamic data masking](../dynamic-data-masking.md)

  Limit sensitive data exposure by masking it to nonprivileged users.

- [SQL Server Certificates and Asymmetric Keys](../sql-server-certificates-and-asymmetric-keys.md)

  Information about using Public Key Cryptography.

## Related content

- [Securing SQL Server](../securing-sql-server.md)
- [An overview of Azure SQL Database security capabilities](/azure/sql-database/sql-database-security-overview)
- [Cryptographic functions (Transact-SQL)](../../../t-sql/functions/cryptographic-functions-transact-sql.md)
- [ENCRYPTBYPASSPHRASE (Transact-SQL)](../../../t-sql/functions/encryptbypassphrase-transact-sql.md)
- [ENCRYPTBYKEY (Transact-SQL)](../../../t-sql/functions/encryptbykey-transact-sql.md)
- [ENCRYPTBYASYMKEY (Transact-SQL)](../../../t-sql/functions/encryptbyasymkey-transact-sql.md)
- [ENCRYPTBYCERT (Transact-SQL)](../../../t-sql/functions/encryptbycert-transact-sql.md)
- [sys.key_encryptions (Transact-SQL)](../../system-catalog-views/sys-key-encryptions-transact-sql.md)
- [SQL Server and Database Encryption Keys (Database Engine)](sql-server-and-database-encryption-keys-database-engine.md)
- [Back up and restore SQL Server Reporting Services (SSRS) encryption keys](../../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)
- [Configure SQL Server Database Engine for encrypting connections](../../../database-engine/configure-windows/configure-sql-server-encryption.md)
- [Microsoft TechNet: SQL Server TechCenter: SQL Server 2012 Security and Protection](https://download.microsoft.com/download/8/F/A/8FABACD7-803E-40FC-ADF8-355E7D218F4C/SQL_Server_2012_Security_Best_Practice_Whitepaper_Apr2012.docx)
