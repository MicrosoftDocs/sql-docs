---
title: "Choose an encryption algorithm"
description: Use this guidance to choose an encryption algorithm to help secure an instance of SQL Server, which supports several common algorithms.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 09/22/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "cryptography [SQL Server], algorithms"
  - "encryption [SQL Server], algorithms"
  - "security [SQL Server], encryption"
  - "algorithms [SQL Server encryption]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Choose an encryption algorithm

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Encryption is one of several defenses available to the administrator who wants to secure an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

Encryption algorithms define data transformations that can't be easily reversed by unauthorized users. Administrators and developers can choose from among several algorithms in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], including DES, Triple DES, TRIPLE_DES_3KEY, RC2, RC4, 128-bit RC4, DESX, 128-bit AES, 192-bit AES, and 256-bit AES.

Beginning with [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)], all algorithms other than `AES_128`, `AES_192`, and `AES_256` are deprecated. To use older algorithms (not recommended), you must set the database to database compatibility level 120 or lower.

## How to choose the right algorithm

No single algorithm is ideal for all situations, and guidance on the merits of each is beyond the scope of this article. However, the following general principles apply:

- Strong encryption generally consumes more CPU resources than weak encryption.

- Long keys generally yield stronger encryption than short keys.

- Asymmetric encryption is slower than symmetric encryption.

- Long, complex passwords are stronger than short passwords.

- Symmetric encryption is recommended when the key is only stored locally. Asymmetric encryption is recommended when keys need to be shared across the wire.

- If you're encrypting lots of data, you should encrypt the data using a symmetric key, and encrypt the symmetric key with an asymmetric key.

- Encrypted data can't be compressed, but compressed data can be encrypted. If you use compression, you should compress data before encrypting it.

For more information about encryption algorithms and encryption technology, see [Key Security Concepts](/dotnet/standard/security/key-security-concepts).

### Deprecated RC4 algorithm

The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using `RC4` or `RC4_128` when the database is in compatibility level 90 or 100 (not recommended). Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE [ssSQL11](../../../includes/sssql11-md.md)] and later versions, material encrypted using `RC4` or `RC4_128` can be decrypted in any compatibility level.  

Repeated use of the same `RC4` or `RC4_128` `KEY_GUID` on different blocks of data results in the same RC4 key because [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] doesn't provide a salt automatically. Using the same RC4 key repeatedly is a well-known error that results in weak encryption. Therefore, we have deprecated the `RC4` and `RC4_128` keywords. [!INCLUDE [ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]

### Clarification regarding DES algorithms

DESX was incorrectly named. Symmetric keys created with `ALGORITHM = DESX` actually use the Triple DES cipher with a 192-bit key. The DESX algorithm isn't provided. [!INCLUDE [ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]

Symmetric keys created with `ALGORITHM = TRIPLE_DES_3KEY` use Triple DES with a 192-bit key.

Symmetric keys created with `ALGORITHM = TRIPLE_DES` use Triple DES with a 128-bit key.

## Related content

- [CREATE SYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-symmetric-key-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../../t-sql/statements/create-certificate-transact-sql.md)
- [Transparent Data Encryption (TDE)](transparent-data-encryption.md)
- [Encrypt a Column of Data](encrypt-a-column-of-data.md)
- [SQL Server Encryption](sql-server-encryption.md)
- [Encryption Hierarchy](encryption-hierarchy.md)
