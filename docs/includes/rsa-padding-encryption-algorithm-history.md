---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 11/18/2025
ms.service: sql
ms.topic: include
ms.custom:
  - ignite-2025
---

## Background

Azure SQL and SQL Server use the RSA algorithm for asymmetric encryption. The RSA algorithm can't be used in its "pure" form as it lacks semantic security and isn't secure against chosen plaintext attacks or ciphertext attacks due to its deterministic nature. Encrypting the same message twice produces the same ciphertext.

To achieve security, messages require padding. Currently, data is encrypted with RSA using the PKCS #1 v1.5 padding scheme. A specific weakness of the PKCS#1 v1.5 padding is that it isn't very redundant, with the bytes embedded being random without any specifically enforced value. A sequence of random bytes can be "properly padded" with a small probability. An attacker would need about one million aborted handshakes to recover the plaintext by brute force.

Newer versions of PKCS#1 v1.5 describe a new padding type called Optimal Asymmetric Encryption Padding (OAEP), which uses a hash function to add significant internal redundancy, making it improbable for a random string to match the padding format. OAEP introduces some hashing between the RSA encryption and the padding check. The hashing from OAEP significantly alters the attacker's ability to understand what they see.

OAEP-256 support for RSA-based encryption was introduced in [!INCLUDE [sssql25-md](sssql25-md.md)]. Switching to OAEP padding mode is driven by the database compatibility level. This feature is available for databases at the 170 level of database compatibility or higher.
