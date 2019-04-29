---
title: "ENCRYPTBYPASSPHRASE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ENCRYPTBYPASSPHRASE"
  - "ENCRYPTBYPASSPHRASE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ENCRYPTBYPASSPHRASE function"
  - "encryption [SQL Server], symmetric keys"
  - "symmetric keys [SQL Server], ENCRYPTBYPASSPHRASE function"
ms.assetid: f8dbb9e6-94d6-40d7-8b38-6833a409d597
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ENCRYPTBYPASSPHRASE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Encrypt data with a passphrase using the TRIPLE DES algorithm with a 128 key bit length.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
EncryptByPassPhrase ( { 'passphrase' | @passphrase }   
    , { 'cleartext' | @cleartext }  
  [ , { add_authenticator | @add_authenticator }  
    , { authenticator | @authenticator } ] )  
```  
  
## Arguments  
 *passphrase*  
 A passphrase from which to generate a symmetric key.  
  
 @passphrase  
 A variable of type **nvarchar**, **char**, **varchar**, **binary**, **varbinary**, or **nchar** containing a passphrase from which to generate a symmetric key.  
  
 *cleartext*  
 The cleartext to be encrypted.  
  
 @cleartext  
 A variable of type **nvarchar**, **char**, **varchar**, **binary**, **varbinary**, or **nchar** containing the cleartext. Maximum size is 8,000 bytes.  
  
 *add_authenticator*  
 Indicates whether an authenticator will be encrypted together with the cleartext. 1 if an authenticator will be added. **int**.  
  
 @add_authenticator  
 Indicates whether a hash will be encrypted together with the cleartext.  
  
 *authenticator*  
 Data from which to derive an authenticator. **sysname**.  
  
 @authenticator  
 A variable containing data from which to derive an authenticator.  
  
## Return Types  
 **varbinary** with maximum size of 8,000 bytes.  
  
## Remarks  
 A passphrase is a password that includes spaces. The advantage of using a passphrase is that it is easier to remember a meaningful phrase or sentence than to remember a comparably long string of characters.  
  
 This function does not check password complexity.  
  
## Examples  
 The following example updates a record in the `SalesCreditCard` table and encrypts the value of the credit card number stored in column `CardNumber_EncryptedbyPassphrase`, using the primary key as an authenticator.  
  
```  
USE AdventureWorks2012;  
GO  
-- Create a column in which to store the encrypted data.  
ALTER TABLE Sales.CreditCard   
    ADD CardNumber_EncryptedbyPassphrase varbinary(256);   
GO  
-- First get the passphrase from the user.  
DECLARE @PassphraseEnteredByUser nvarchar(128);  
SET @PassphraseEnteredByUser   
    = 'A little learning is a dangerous thing!';  
  
-- Update the record for the user's credit card.  
-- In this case, the record is number 3681.  
UPDATE Sales.CreditCard  
SET CardNumber_EncryptedbyPassphrase = EncryptByPassPhrase(@PassphraseEnteredByUser  
    , CardNumber, 1, CONVERT( varbinary, CreditCardID))  
WHERE CreditCardID = '3681';  
GO  
```  
  
## See Also  
 [DECRYPTBYPASSPHRASE &#40;Transact-SQL&#41;](../../t-sql/functions/decryptbypassphrase-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
