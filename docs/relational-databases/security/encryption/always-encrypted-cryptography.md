---
title: "Always Encrypted Cryptography | Microsoft Docs"
ms.custom: ""
ms.date: "02/29/2016"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Always Encrypted, cryptography system"
ms.assetid: ae8226ff-0853-4716-be7b-673ce77dd370
author: aliceku
ms.author: aliceku
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Always Encrypted Cryptography
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  This document describes encryption algorithms and mechanisms to derive cryptographic material used in the [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)].  
  
## Keys, Key Stores and Key Encryption Algorithms  
 Always Encrypted uses two types of keys: Column master keys and column encryption keys.  
  
 A column master key (CMK) is a key encrypting key (i.e. a key used to encrypt other keys) that is always in client's control and is stored in an external key store. An Always Encrypted-enabled client driver interacts with the key store via a CMK store provider, which can be either part of the driver library (a [!INCLUDE[msCoName](../../../includes/msconame-md.md)]/system provider) or part of the client application (a custom provider). Client driver libraries currently include [!INCLUDE[msCoName](../../../includes/msconame-md.md)] key store providers for [Windows Certificate Store](/windows/desktop/SecCrypto/using-certificate-stores) and hardware security modules (HSMs).  (For the current list of providers, see [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-column-master-key-transact-sql.md).) An application developer can supply a custom provider for an arbitrary store.  
  
 A column encryption key (CEK), is a content encryption key (i.e. a key used to protect data) that is protected by a CMK.  
  
 All [!INCLUDE[msCoName](../../../includes/msconame-md.md)] CMK store providers encrypt CEKs by using RSA with Optimal Asymmetric Encryption Padding (RSA-OAEP) with the default parameters specified by RFC 8017 in Section A.2.1. Those default parameters are using a hash function of SHA-1 and a mask generation function of MGF1 with SHA-1.  
  
## Data Encryption Algorithm  
 Always Encrypted uses the **AEAD_AES_256_CBC_HMAC_SHA_256** algorithm to encrypt data in the database.  
  
 **AEAD_AES_256_CBC_HMAC_SHA_256** is derived from the specification draft at [https://tools.ietf.org/html/draft-mcgrew-aead-aes-cbc-hmac-sha2-05](https://tools.ietf.org/html/draft-mcgrew-aead-aes-cbc-hmac-sha2-05). It uses an Authenticated Encryption scheme with Associated Data, following an Encrypt-then-MAC approach. That is, the plaintext is first encrypted, and the MAC is produced based on the resulting ciphertext.  
  
 In order to conceal patterns, **AEAD_AES_256_CBC_HMAC_SHA_256** uses the Cipher Block Chaining (CBC) mode of operation, where an initial value is fed into the system named the initialization vector (IV). The full description of the CBC mode can be found at [https://csrc.nist.gov/publications/nistpubs/800-38a/sp800-38a.pdf](https://csrc.nist.gov/publications/nistpubs/800-38a/sp800-38a.pdf).  
  
 **AEAD_AES_256_CBC_HMAC_SHA_256** computes a ciphertext value for a given plaintext value using the following steps.  
  
### Step 1: Generating the initialization vector (IV)  
 Always Encrypted supports two variations of **AEAD_AES_256_CBC_HMAC_SHA_256**:  
  
-   Randomized  
  
-   Deterministic  
  
 For randomized encryption, the IV is randomly generated. As a result, each time the same plaintext is encrypted, a different ciphertext is generated, which prevents any information disclosure.  
  
```  
When using randomized encryption: IV = Generate cryptographicaly random 128bits  
```  
  
 In the case of deterministic encryption, the IV is not randomly generated, but instead it is derived from the plaintext value using the following algorithm:  
  
```  
When using deterministic encryption: IV = HMAC-SHA-256( iv_key, cell_data ) truncated to 128 bits.  
```  
  
 Where iv_key is derived from the CEK in the following way:  
  
```  
iv_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell IV key" + algorithm + CEK_length)  
```  
  
 The HMAC value truncation is performed in order to fit 1 block of data as needed for the IV.    
As a result, deterministic encryption always produces the same ciphertext for a given plaintext value, which enables inferring whether two plaintext values are equal by comparing their corresponding ciphertext values. This limited information disclosure allows the database system to support equality comparison on encrypted column values.  
  
 Deterministic encryption is more effective in concealing patterns, compared to alternatives, such as using a pre-defined IV value.  
  
### Step 2: Computing AES_256_CBC Ciphertext  
 After computing the IV, the **AES_256_CBC** ciphertext is generated:  
  
```  
aes_256_cbc_ciphertext = AES-CBC-256(enc_key, IV, cell_data) with PKCS7 padding.  
```  
  
 Where the encryption key (enc_key) is derived from the CEK as follows.  
  
```  
enc_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell encryption key" + algorithm + CEK_length )  
```  
  
### Step 3: Computing MAC  
 Subsequently, the MAC is computed using the following algorithm:  
  
```  
MAC = HMAC-SHA-256(mac_key, versionbyte + IV + Ciphertext + versionbyte_length)  
```  
  
 Where:  
  
```  
versionbyte = 0x01 and versionbyte_length = 1   
mac_key = HMAC-SHA-256(CEK, "Microsoft SQL Server cell MAC key" + algorithm + CEK_length)  
```  
  
### Step 4: Concatenation  
 Finally, the encrypted value is produced by simply concatenating the algorithm version byte, the MAC, the IV and the AES_256_CBC ciphertext:  
  
```  
aead_aes_256_cbc_hmac_sha_256 = versionbyte + MAC + IV + aes_256_cbc_ciphertext  
```  
  
## Ciphertext Length  
 The lengths (in bytes) of particular components of **AEAD_AES_256_CBC_HMAC_SHA_256** ciphertext are:  
  
-   versionbyte: 1  
  
-   MAC: 32  
  
-   IV: 16  
  
-   aes_256_cbc_ciphertext: `(FLOOR (DATALENGTH(cell_data)/ block_size) + 1)* block_size`, where:  
  
    -   block_size is 16 bytes  
  
    -   cell_data is a plaintext value  
  
     Therefore, the minimal size of aes_256_cbc_ciphertext is 1 block, which is 16 bytes.  
  
 Thus, the length of ciphertext, resulting from encrypting a given plaintext values (cell_data), can be calculated using the following formula:  
  
```  
1 + 32 + 16 + (FLOOR(DATALENGTH(cell_data)/16) + 1) * 16  
```  
  
 For example:  
  
-   A 4-byte long **int** plaintext value becomes a 65-byte long binary value after encryption.  
  
-   A 2,000-byte long **nchar(1000)** plaintext values becomes a 2,065-byte long binary value after encryption.  
  
 The following table contains a complete list of data types and the length of ciphertext for each type.  
  
|Data Type|Ciphertext Length [bytes]|  
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
  
## .NET Reference  
 For details about the algorithms, discussed in this document, see the **SqlAeadAes256CbcHmac256Algorithm.cs** and **SqlColumnEncryptionCertificateStoreProvider.cs** files in the [.NET Reference](https://referencesource.microsoft.com/).  
  
## See Also  
 [Always Encrypted &#40;Database Engine&#41;](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [Always Encrypted &#40;client development&#41;](../../../relational-databases/security/encryption/always-encrypted-client-development.md)  
  
  
