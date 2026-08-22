---
title: "Always Encrypted cryptography"
description: Learn about encryption algorithms and mechanisms that derive cryptographic material in the Always Encrypted feature in SQL Server and Azure SQL Database.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "Always Encrypted, cryptography system"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Always Encrypted cryptography
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This article describes encryption algorithms and mechanisms to derive cryptographic material used in the [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)]. These algorithms apply to all editions and versions of Always Encrypted; both Always Encrypted with and without secure enclaves use the same encryption algorithm.  
  
## Keys, key stores, and key encryption algorithms
 Always Encrypted uses two types of keys: Column master keys and column encryption keys.  
  
 A column master key (CMK) is a key encrypting key (for example, a key that is used to encrypt other keys) that is always in a client's control, and is stored in an external key store. An Always Encrypted-enabled client driver interacts with the key store via a CMK store provider, which can be either part of the driver library (a [!INCLUDE[msCoName](../../../includes/msconame-md.md)]/system provider) or part of the client application (a custom provider). Client driver libraries currently include [!INCLUDE[msCoName](../../../includes/msconame-md.md)] key store providers for [Windows Certificate Store](/windows/desktop/SecCrypto/using-certificate-stores) and hardware security modules (HSMs). For the current list of providers, see [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-column-master-key-transact-sql.md). An application developer can supply a custom provider for an arbitrary store.  
  
 A column encryption key (CEK), is a content encryption key (for example, a key that is used to protect data) that is protected by a CMK.  
  
 All [!INCLUDE[msCoName](../../../includes/msconame-md.md)] CMK store providers encrypt CEKs by using RSA with Optimal Asymmetric Encryption Padding (RSA-OAEP). The key store provider that supports Microsoft Cryptography API: Next Generation (CNG) in .NET Framework ([SqlColumnEncryptionCngProvider Class](/dotnet/api/system.data.sqlclient.sqlcolumnencryptioncngprovider)) uses the default parameters specified by RFC 8017 in Section A.2.1. Those default parameters are using a hash function of SHA-1 and a mask generation function of MGF1 with SHA-1. All other key store providers use SHA-256. 

Always Encrypted internally uses FIPS 140-2 validated cryptographic modules. 

## Data encryption algorithm  
 Always Encrypted uses the **AEAD_AES_256_CBC_HMAC_SHA_256** algorithm to encrypt data in the database. **AEAD** stands for Authenticated Encryption with Associated Data; **HMAC** stands for hash-based message authentication code; **MAC** stands for message authentication code.  
  
 **AEAD_AES_256_CBC_HMAC_SHA_256** is derived from the [IETF specification draft](https://tools.ietf.org/html/draft-mcgrew-aead-aes-cbc-hmac-sha2-05). It uses an Authenticated Encryption scheme with Associated Data, following an Encrypt-then-MAC approach. That is, the plaintext is first encrypted, and the MAC is produced based on the resulting ciphertext.  
  
 In order to conceal patterns, **AEAD_AES_256_CBC_HMAC_SHA_256** uses the Cipher Block Chaining (CBC) mode of operation, where an initial value is fed into the system named the initialization vector (IV). The full description of the CBC mode can be found at the [US National Institute of Standards and Technology (NIST)](https://csrc.nist.gov/publications/nistpubs/800-38a/sp800-38a.pdf).  
  
 **AEAD_AES_256_CBC_HMAC_SHA_256** computes a ciphertext value for a given plaintext value using the following steps.  
  
### Step 1: Generating the initialization vector (IV)  
 Always Encrypted supports two variations of **AEAD_AES_256_CBC_HMAC_SHA_256**:  
  
-   Randomized  
  
-   Deterministic  
  
 For randomized encryption, the IV is randomly generated. As a result, each time the same plaintext is encrypted, a different ciphertext is generated, which prevents any information disclosure.  
  
```text  
When using randomized encryption: IV = Generate cryptographically random 128bits  
```  
  
 For deterministic encryption, the IV isn't randomly generated, but instead it's derived from the plaintext value using the following algorithm:  
  
```text  
When using deterministic encryption: IV = HMAC-SHA-256( iv_key, cell_data ) truncated to 128 bits.  
```  
  
 Where iv_key is derived from the CEK in the following way:  
  
```text  
iv_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell IV key" + algorithm + CEK_length)  
```  
  
 The HMAC value truncation is performed to fit one block of data as needed for the IV.
As a result, deterministic encryption always produces the same ciphertext for a given plaintext value, which enables inferring whether two plaintext values are equal by comparing their corresponding ciphertext values. This limited information disclosure allows the database system to support equality comparison on encrypted column values.  
  
 Deterministic encryption is more effective in concealing patterns, compared to alternatives, such as using a pre-defined IV value.  
  
### Step 2: Computing AES_256_CBC ciphertext  
 For Always Encrypted's **AEAD_AES_256_CBC_HMAC_SHA_256** algorithm, after computing the IV in Step 1, the **AES_256_CBC** ciphertext is generated:  
  
```text  
aes_256_cbc_ciphertext = AES-CBC-256(enc_key, IV, cell_data) with PKCS7 padding.  
```  
  
 Where the encryption key (`enc_key`) is derived from the column encryption key (CEK) as follows:  
  
```text  
enc_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell encryption key" + algorithm + CEK_length )  
```  
  
### Step 3: Computing MAC  
 For Always Encrypted's **AEAD_AES_256_CBC_HMAC_SHA_256** algorithm, the MAC (message authentication code) is computed from the version byte, the IV (from Step 1), and the AES_256_CBC ciphertext (from Step 2), using a `mac_key` derived from the column encryption key (CEK):  
  
```text  
MAC = HMAC-SHA-256(mac_key, versionbyte + IV + Ciphertext + versionbyte_length)  
```  
  
 Where:  
  
```text  
versionbyte = 0x01 and versionbyte_length = 1
mac_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell MAC key" + algorithm + CEK_length)  
```  
  
### Step 4: Concatenation  
 For Always Encrypted's **AEAD_AES_256_CBC_HMAC_SHA_256** algorithm, the final encrypted value is produced by concatenating the algorithm version byte, the MAC (from Step 3), the IV (from Step 1), and the AES_256_CBC ciphertext (from Step 2):  
  
```text  
aead_aes_256_cbc_hmac_sha_256 = versionbyte + MAC + IV + aes_256_cbc_ciphertext  
```  
  
## Ciphertext length  
 The lengths (in bytes) of particular components of **AEAD_AES_256_CBC_HMAC_SHA_256** ciphertext are:  
  
| Component | Size (bytes) |
|---|---|
| `versionbyte` | 1 |
| `MAC` | 32 |
| `IV` | 16 |
| `aes_256_cbc_ciphertext` | `(FLOOR(DATALENGTH(cell_data) / block_size) + 1) * block_size`, where `block_size` is 16 bytes and `cell_data` is the plaintext value. The minimum size of `aes_256_cbc_ciphertext` is one block (16 bytes). |
  
 Thus, the length of ciphertext, resulting from encrypting a given plaintext values (cell_data), can be calculated using the following formula:  
  
```text  
1 + 32 + 16 + (FLOOR(DATALENGTH(cell_data)/16) + 1) * 16  
```  
  
 For example:  
  
-   A 4-byte long **int** plaintext value becomes a 65-byte long binary value after encryption.  
  
-   A 2,000-byte long **nchar(1000)** plaintext value becomes a 2,065-byte long binary value after encryption.  
  
 The ciphertext length depends on the source data type. For fixed-length types, the result is a constant; for variable-length types (`char`, `nchar`, `varchar`, `nvarchar`, `binary`, `varbinary`), use the formula above. Types marked **N/A** can't be encrypted with Always Encrypted. The following table contains a complete list of data types and the length of ciphertext for each type.  
  
|Data type|Ciphertext length [bytes]|  
|---------------|---------------------------------|  
|**bigint**|65|  
|**binary**|Varies. Use the formula above.|  
|**bit**|65|  
|**char**|Varies. Use the formula above.|  
|**date**|65|  
|**datetime**|65|  
|**datetime2**|65|  
|**datetimeoffset**|65|  
|**decimal**|81|  
|**float**|65|  
|**geography**|N/A (not supported)|  
|**geometry**|N/A (not supported)|  
|**hierarchyid**|N/A (not supported)|  
|**image**|N/A (not supported)|  
|**int**|65|  
|**money**|65|  
|**nchar**|Varies. Use the formula above.|  
|**ntext**|N/A (not supported)|  
|**numeric**|81|  
|**nvarchar**|Varies. Use the formula above.|  
|**real**|65|  
|**smalldatetime**|65|  
|**smallint**|65|  
|**smallmoney**|65|  
|**sql_variant**|N/A (not supported)|  
|**sysname**|N/A (not supported)|  
|**text**|N/A (not supported)|  
|**time**|65|  
|**timestamp**<br /><br /> (**rowversion**)|N/A (not supported)|  
|**tinyint**|65|  
|**uniqueidentifier**|81|  
|**varbinary**|Varies. Use the formula above.|  
|**varchar**|Varies. Use the formula above.|  
|**xml**|N/A (not supported)|  
  
## .NET reference  
 For details about the algorithms discussed in this article, see the **SqlAeadAes256CbcHmac256Algorithm.cs**, **SqlColumnEncryptionCertificateStoreProvider.cs**, and **SqlColumnEncryptionCngProvider.cs** files in the [.NET Reference](https://referencesource.microsoft.com/).  
  
## Related content

- [Always Encrypted](always-encrypted-database-engine.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
