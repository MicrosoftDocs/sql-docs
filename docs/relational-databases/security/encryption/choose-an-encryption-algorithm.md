---
title: "Choose an Encryption Algorithm | Microsoft Docs"
description: Use this guidance to choose an encryption algorithm to help secure an instance of SQL Server, which supports several common algorithms.
ms.custom: ""
ms.date: "08/14/2018"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "cryptography [SQL Server], algorithms"
  - "encryption [SQL Server], algorithms"
  - "security [SQL Server], encryption"
  - "algorithms [SQL Server encryption]"
ms.assetid: 8227028c-a9c9-489d-bd27-fbf8242634ae
author: jaszymas
ms.author: jaszymas
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Choose an Encryption Algorithm
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Encryption is one of several defenses-in-depth that are available to the administrator who wants to secure an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 Encryption algorithms define data transformations that cannot be easily reversed by unauthorized users. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] allows administrators and developers to choose from among several algorithms, including DES, Triple DES, TRIPLE_DES_3KEY, RC2, RC4, 128-bit RC4, DESX, 128-bit AES, 192-bit AES, and 256-bit AES.  
  
> [!NOTE]  
>  Beginning with [!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)], all algorithms other than AES_128, AES_192, and AES_256 are deprecated. To use older algorithms (not recommended) you must set the database to database compatibility level 120 or lower.  
  
 No single algorithm is ideal for all situations, and guidance on the merits of each is beyond the scope of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online. However, the following general principles apply:  
  
-   Strong encryption generally consumes more CPU resources than weak encryption.  
  
-   Long keys generally yield stronger encryption than short keys.  
  
-   Asymmetric encryption is slower than symmetric encryption.  
  
-   Long, complex passwords are stronger than short passwords.  

-   Symmetric encryption is generally recommended when they key is only stored locally, asymmetric encryption is recommended when keys need to be shared across the wire.
  
-   If you are encrypting lots of data, you should encrypt the data using a symmetric key, and encrypt the symmetric key with an asymmetric key.  
  
-   Encrypted data cannot be compressed, but compressed data can be encrypted. If you use compression, you should compress data before encrypting it.  
  
> [!IMPORTANT]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and higher material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
>   
>  Repeated use of the same RC4 or RC4_128 KEY_GUID on different blocks of data will result in the same RC4 key because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not provide a salt automatically. Using the same RC4 key repeatedly is a well-known error that will result in very weak encryption. Therefore, we have deprecated the RC4 and RC4_128 keywords. [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 For more information about encryption algorithms and encryption technology, see [Key Security Concepts](/previous-versions/aa720225(v=vs.71)) in the .NET Framework Developer's Guide on MSDN.  
  
 **Clarification regarding DES algorithms:**  
  
-   DESX was incorrectly named. Symmetric keys created with ALGORITHM = DESX actually use the TRIPLE DES cipher with a 192-bit key. The DESX algorithm is not provided. [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
-   Symmetric keys created with ALGORITHM = TRIPLE_DES_3KEY use TRIPLE DES with a 192-bit key.  
  
-   Symmetric keys created with ALGORITHM = TRIPLE_DES use TRIPLE DES with a 128-bit key.  
  
## Related Tasks  
  
| Task | Type |
| ---- | ---- |
|Encrypting using a symmetric key.|[CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)|  
|Encrypting using an asymmetric key.|[CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)|  
|Encrypting using a certificate.|[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-certificate-transact-sql.md)|  
|Encrypting database files using transparent data encryption.|[Transparent Data Encryption &#40;TDE&#41;](../../../relational-databases/security/encryption/transparent-data-encryption.md)|  
|How to encrypt one column of a table.|[Encrypt a Column of Data](../../../relational-databases/security/encryption/encrypt-a-column-of-data.md)|  
  
## See Also  
 [SQL Server Encryption](../../../relational-databases/security/encryption/sql-server-encryption.md)   
 [Encryption Hierarchy](../../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
