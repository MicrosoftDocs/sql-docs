---
title: "DECRYPTBYASYMKEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DECRYPTBYASYMKEY"
  - "DECRYPTBYASYMKEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "asymmetric keys [SQL Server], DECRYPTBYASYMKEY function"
  - "DECRYPTBYASYMKEY function"
  - "decryption [SQL Server], asymmetric keys"
ms.assetid: d9ebcd30-f01c-4cfe-b95e-ffe6ea13788b
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECRYPTBYASYMKEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Decrypts data with an asymmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByAsymKey (Asym_Key_ID , { 'ciphertext' | @ciphertext }   
    [ , 'Asym_Key_Password' ] )  
```  
  
## Arguments  
 *Asym_Key_ID*  
 Is the ID of an asymmetric key in the database. *Asym_Key_ID* is **int**.  
  
 *ciphertext*  
 Is a string of data that has been encrypted with the asymmetric key.  
  
 @ciphertext  
 Is a variable of type **varbinary** that contains data that has been encrypted with the asymmetric key.  
  
 *Asym_Key_Password*  
 Is the password that was used to encrypt the asymmetric key in the database.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 Encryption/decryption with an asymmetric key is very costly compared to encryption/decryption with a symmetric key. We do not recommend using an asymmetric key when you work with large datasets, such as user data in tables.  
  
## Permissions  
 Requires CONTROL permission on the asymmetric key.  
  
## Examples  
 The following example decrypts ciphertext that was encrypted with asymmetric key `JanainaAsymKey02`, which was stored in `AdventureWorks2012.ProtectedData04`. The returned data is decrypted using asymmetric key `JanainaAsymKey02`, which has been decrypted with password `pGFD4bb925DGvbd2439587y`. The plaintext is converted to type **nvarchar**.  
  
```  
SELECT CONVERT(nvarchar(max),  
    DecryptByAsymKey( AsymKey_Id('JanainaAsymKey02'),   
    ProtectedData, N'pGFD4bb925DGvbd2439587y' ))   
AS DecryptedData   
FROM [AdventureWorks2012].[Sales].[ProtectedData04]   
WHERE Description = N'encrypted by asym key''JanainaAsymKey02''';  
GO  
```  
  
## See Also  
 [ENCRYPTBYASYMKEY &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbyasymkey-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [ALTER ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  