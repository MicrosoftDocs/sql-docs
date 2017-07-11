---
title: "DECRYPTBYPASSPHRASE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DECRYPTBYPASSPHRASE"
  - "DECRYPTBYPASSPHRASE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "decryption [SQL Server], symmetric keys"
  - "symmetric keys [SQL Server], DECRYPTBYPASSPHRASE function"
  - "DECRYPTBYPASSPHRASE function"
ms.assetid: ca34b5cd-07b3-4dca-b66a-ed8c6a826c95
caps.latest.revision: 28
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECRYPTBYPASSPHRASE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Decrypts data that was encrypted with a passphrase.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DecryptByPassPhrase ( { 'passphrase' | @passphrase }   
    , { 'ciphertext' | @ciphertext }  
  [ , { add_authenticator | @add_authenticator }  
    , { authenticator | @authenticator } ] )  
```  
  
## Arguments  
 *passphrase*  
 Is the passphrase that will be used to generate the key for decryption.  
  
 @passphrase  
 Is a variable of type **nvarchar**, **char**, **varchar**, or **nchar** that contains the passphrase that will be used to generate the key for decryption.  
  
 '*ciphertext*'  
 Is the ciphertext to be decrypted.  
  
 @ciphertext  
 Is a variable of type **varbinary** that contains the ciphertext. The maximum size is 8,000 bytes.  
  
 *add_authenticator*  
 Indicates whether an authenticator was encrypted together with the plaintext. Is 1 if an authenticator was used. **int**.  
  
 @add_authenticator  
 Indicates whether an authenticator was encrypted together with the plaintext. Is 1 if an authenticator was used. **int**.  
  
 *authenticator*  
 Is the authenticator data. **sysname**.  
  
 @authenticator  
 Is a variable that contains data from which to derive an authenticator.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 No permissions are required for executing this function.  
  
 Returns NULL if the wrong passphrase or authenticator information is used.  
  
 The passphrase is used to generate a decryption key, which will not be persisted.  
  
 If an authenticator was included when the ciphertext was encrypted, the authenticator must be provided at decryption time. If the authenticator value provided at decryption time does not match the authenticator value encrypted with the data, the decryption will fail.  
  
## Examples  
 The following example decrypts the record updated in [EncryptByPassPhrase](../../t-sql/functions/encryptbypassphrase-transact-sql.md).  
  
```  
USE AdventureWorks2012;  
-- Get the pass phrase from the user.  
DECLARE @PassphraseEnteredByUser nvarchar(128);  
SET @PassphraseEnteredByUser   
= 'A little learning is a dangerous thing!';  
  
-- Decrypt the encrypted record.  
SELECT CardNumber, CardNumber_EncryptedbyPassphrase   
    AS 'Encrypted card number', CONVERT(nvarchar,  
    DecryptByPassphrase(@PassphraseEnteredByUser, CardNumber_EncryptedbyPassphrase, 1   
    , CONVERT(varbinary, CreditCardID)))  
    AS 'Decrypted card number' FROM Sales.CreditCard   
    WHERE CreditCardID = '3681';  
GO  
```  
  
## See Also  
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [ENCRYPTBYPASSPHRASE &#40;Transact-SQL&#41;](../../t-sql/functions/encryptbypassphrase-transact-sql.md)  
  
  