---
title: "ENCRYPTBYASYMKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ENCRYPTBYASYMKEY"
  - "ENCRYPTBYASYMKEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ENCRYPTBYASYMKEY function"
  - "encryption [SQL Server], asymmetric keys"
  - "asymmetric keys [SQL Server], ENCRYPTBYASYMKEY function"
ms.assetid: 86bb2588-ab13-4db2-8f3c-42c9f572a67b
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ENCRYPTBYASYMKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Encrypts data with an asymmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
EncryptByAsymKey ( Asym_Key_ID , { 'plaintext' | @plaintext } )  
```  
  
## Arguments  
 *Asym_Key_ID*  
 Is the ID of an asymmetric key in the database. **int**.  
  
 *cleartext*  
 Is a string of data that will be encrypted with the asymmetric key.  
  
 **@plaintext**  
 Is a variable of type **nvarchar**, **char**, **varchar**, **binary**, **varbinary**, or **nchar** that contains data to be encrypted with the asymmetric key.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 Encryption and decryption with an asymmetric key is very costly compared with encryption and decryption with a symmetric key. We recommend that you not encrypt large datasets, such as user data in tables, using an asymmetric key. Instead, you should encrypt the data using a strong symmetric key and encrypt the symmetric key using an asymmetric key.  
  
 **EncryptByAsymKey** return **NULL** if the input exceeds a certain number of bytes, depending on the algorithm. The limits are: a 512 bit RSA key can encrypt up to 53 bytes, a 1024 bit key can encrypt up to 117 bytes, and a 2048 bit key can encrypt up to 245 bytes. (Note that in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], both certificates and asymmetric keys are wrappers over RSA keys.)  
  
## Examples  
 The following example encrypts the text stored in `@cleartext` with the asymmetric key `JanainaAsymKey02`. The encrypted data is inserted into the `ProtectedData04` table.  
  
```  
INSERT INTO AdventureWorks2012.Sales.ProtectedData04   
    VALUES( N'Data encrypted by asymmetric key ''JanainaAsymKey02''',  
    EncryptByAsymKey(AsymKey_ID('JanainaAsymKey02'), @cleartext) );  
GO  
```  
  
## See Also  
 [DECRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbyasymkey-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  